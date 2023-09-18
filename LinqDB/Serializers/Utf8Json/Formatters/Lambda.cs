using Expressions = System.Linq.Expressions;
using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.LambdaExpression;
using static Extension;
public class Lambda:IJsonFormatter<T> {
    public static readonly Lambda Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,IJsonFormatterResolver Resolver) {
        var ListParameter=Resolver.Serializer().ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Parameters=value.Parameters;
        ListParameter.AddRange(Parameters);
        writer.WriteType(value.Type);
        writer.WriteValueSeparator();
        writer.Serialize宣言Parameters(value.Parameters,Resolver);
        writer.WriteValueSeparator();
        Expression.Write(ref writer,value.Body,Resolver);
        writer.WriteValueSeparator();
        writer.WriteBoolean(value.TailCall);
        ListParameter.RemoveRange(ListParameter_Count,Parameters.Count);
    }
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver) {
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateWrite(ref writer,value,Resolver);
    }
    internal static void WriteNullable(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        PrivateWrite(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver) {
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        PrivateWrite(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T? ReadNullable(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.TryReadNil()) return null;
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
    internal static T Read(ref Reader reader,IJsonFormatterResolver Resolver){
        var ListParameter=Resolver.Serializer().ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var parameters = reader.Deserialize宣言Parameters(Resolver);
        ListParameter.AddRange(parameters);

        reader.ReadIsValueSeparatorWithVerify();
        var body =Expression.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var tailCall = reader.ReadBoolean();
        ListParameter.RemoveRange(ListParameter_Count,parameters.Count);
        return Expressions.Expression.Lambda(
            type,
            body,
            tailCall,
            parameters
        );
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        return ReadNullable(ref reader,Resolver)!;
    }
}
