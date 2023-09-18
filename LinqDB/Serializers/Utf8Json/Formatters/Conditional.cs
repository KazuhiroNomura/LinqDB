using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.ConditionalExpression;
public class Conditional:IJsonFormatter<T> {
    public static readonly Conditional Instance=new();
    private static void PrivateSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        Expression.Write(ref writer,value.Test,Resolver);
        writer.WriteValueSeparator();
        Expression.Write(ref writer,value.IfTrue,Resolver);
        writer.WriteValueSeparator();
        Expression.Write(ref writer,value.IfFalse,Resolver);
        writer.WriteValueSeparator();
        writer.WriteType(value.Type);
    }
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver){

        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateSerialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        
        PrivateSerialize(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T Read(ref Reader reader,IJsonFormatterResolver Resolver){
        var test=Expression.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var ifTrue=Expression.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var ifFalse=Expression.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var type=reader.ReadType();
        return Expressions.Expression.Condition(test,ifTrue,ifFalse,type);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.TryReadNil())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
