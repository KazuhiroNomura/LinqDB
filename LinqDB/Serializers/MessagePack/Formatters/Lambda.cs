using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.LambdaExpression;
using C=MessagePackCustomSerializer;
using static Common;
public class Lambda:IMessagePackFormatter<Expressions.LambdaExpression>{
    public static readonly Lambda Instance=new();
    public void Serialize(ref MessagePackWriter writer,Expressions.LambdaExpression value,MessagePackSerializerOptions Resolver){
        var ListParameter=C.Instance.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Parameters=value.Parameters;
        ListParameter.AddRange(Parameters);

        writer.WriteType(value.Type);
        Serialize宣言Parameters(ref writer,value.Parameters);
        Expression.Instance.Serialize(ref writer,value.Body,Resolver);
        writer.Write(value.TailCall);
        
        ListParameter.RemoveRange(ListParameter_Count,Parameters.Count);
    }
    public Expressions.LambdaExpression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var ListParameter=C.Instance.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var type=reader.ReadType();
        var parameters = Deserialize宣言Parameters(ref reader,Resolver);
        ListParameter.AddRange(parameters);

        var body =Expression.Instance.Deserialize(ref reader,Resolver);
        var tailCall = reader.ReadBoolean();
        ListParameter.RemoveRange(ListParameter_Count,parameters.Length);
        return Expressions.Expression.Lambda(
            type,
            body,
            tailCall,
            parameters
        );
    }
}
