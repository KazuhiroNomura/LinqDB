using Expressions=System.Linq.Expressions;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.LambdaExpression;
using C=Serializer;
using static Extension;
public class Lambda:IJsonFormatter<T> {
    public static readonly Lambda Instance=new();
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver) {
        var ListParameter=C.Instance.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Parameters=value.Parameters;
        ListParameter.AddRange(Parameters);

        writer.WriteBeginArray();
        //this.Serialize(ref writer,value.Type,Resolver);
        writer.WriteType(value.Type);
        writer.WriteValueSeparator();
        writer.Serialize宣言Parameters(value.Parameters,Resolver);
        writer.WriteValueSeparator();
        Expression.Instance.Serialize(ref writer,value.Body,Resolver);
        writer.WriteValueSeparator();
        writer.WriteBoolean(value.TailCall);
        writer.WriteEndArray();
        
        ListParameter.RemoveRange(ListParameter_Count,Parameters.Count);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver) {
        var ListParameter=C.Instance.ListParameter;
        var ListParameter_Count=ListParameter.Count;

        reader.ReadIsBeginArrayWithVerify();
        //var s=reader.ReadString();
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var parameters = reader.Deserialize宣言Parameters(Resolver);
        ListParameter.AddRange(parameters);

        reader.ReadIsValueSeparatorWithVerify();
        var body =Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var tailCall = reader.ReadBoolean();
        reader.ReadIsEndArrayWithVerify();
        ListParameter.RemoveRange(ListParameter_Count,parameters.Count);
        return Expressions.Expression.Lambda(
            type,
            body,
            tailCall,
            parameters
        );
    }
}
