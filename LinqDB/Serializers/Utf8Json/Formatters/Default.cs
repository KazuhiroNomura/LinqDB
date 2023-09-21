using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.DefaultExpression;
public class Default:IJsonFormatter<T> {
    public static readonly Default Instance=new();
    
    
    private static void PrivateWrite(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteType(value.Type);
    }
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateWrite(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        PrivateWrite(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T Read(ref Reader reader){
        var type=reader.ReadType();
        return Expressions.Expression.Default(type);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
