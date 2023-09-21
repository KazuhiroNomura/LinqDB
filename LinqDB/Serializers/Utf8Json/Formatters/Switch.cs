
using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Reflection;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.SwitchExpression;
public class Switch:IJsonFormatter<T> {
    public static readonly Switch Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,O Resolver){
        writer.WriteType(value.Type);
        writer.WriteValueSeparator();
        Expression.Write(ref writer,value.SwitchValue,Resolver);
        writer.WriteValueSeparator();
        Method.WriteNullable(ref writer,value.Comparison,Resolver);
        writer.WriteValueSeparator();
        writer.WriteCollection(value.Cases,Resolver);
        writer.WriteValueSeparator();
        Expression.Write(ref writer,value.DefaultBody,Resolver);
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteBeginArray();
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateWrite(ref writer,value,Resolver);
        writer.WriteEndArray(); 
    }
    public void Serialize(ref Writer writer,T? value,O Resolver) {
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        PrivateWrite(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T Read(ref Reader reader,O Resolver){
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var switchValue=Expression.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var comparison=Method.ReadNullable(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var cases=reader.ReadArray<Expressions.SwitchCase>(Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var defaultBody=Expression.Read(ref reader,Resolver);
        return Expressions.Expression.Switch(type,switchValue,defaultBody,comparison,cases);
    }
    public T Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
