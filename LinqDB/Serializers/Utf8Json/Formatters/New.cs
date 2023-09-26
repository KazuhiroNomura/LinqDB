using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Reflection;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.NewExpression;
public class New:IJsonFormatter<T> {
    public static readonly New Instance=new();
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteBeginArray();
        writer.WriteNodeType(Expressions.ExpressionType.New);
        writer.WriteValueSeparator();
        Constructor.Write(ref writer,value.Constructor!,Resolver);
        writer.WriteValueSeparator();
        writer.WriteCollection(value.Arguments,Resolver);
        writer.WriteEndArray(); 
    }
    internal static void WriteNew(ref Writer writer,T value,O Resolver){
        writer.WriteBeginArray();
        Constructor.Write(ref writer,value.Constructor!,Resolver);
        writer.WriteValueSeparator();
        writer.WriteCollection(value.Arguments,Resolver);
        writer.WriteEndArray(); 
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value))return;
        WriteNew(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,O Resolver){
        var constructor= Constructor.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
        return Expressions.Expression.New(
            constructor,
            arguments
        );
    }
    internal static T ReadNew(ref Reader reader,O Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        return ReadNew(ref reader,Resolver);
        
        
    }
}
