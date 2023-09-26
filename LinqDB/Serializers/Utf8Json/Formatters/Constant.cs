
using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Others;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.ConstantExpression;
public class Constant:IJsonFormatter<T> {
    public static readonly Constant Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,O Resolver){
        writer.WriteType(value.Type);
        writer.WriteValueSeparator();
        Object.WriteNullable(ref writer,value.Value,Resolver);
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteBeginArray();
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateWrite(ref writer,value,Resolver);
        writer.WriteEndArray(); 
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        PrivateWrite(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T Read(ref Reader reader,O Resolver){
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var value=Object.ReadNullable(ref reader,Resolver);
        return Expressions.Expression.Constant(value,type);
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
