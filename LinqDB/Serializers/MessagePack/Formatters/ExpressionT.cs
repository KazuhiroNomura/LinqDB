using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using static Extension;
using C = Serializer;

public class ExpressionT<T>:IMessagePackFormatter<T>where T:Expressions.LambdaExpression {
    public static readonly ExpressionT<T> Instance=new();
    public void Serialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver) {
        var ListParameter= Resolver.Serializer().ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Parameters=value!.Parameters;
        ListParameter.AddRange(Parameters);
        writer.WriteType(value.Type);
        writer.Serialize宣言Parameters(value.Parameters,Resolver);
        Expression.Write(ref writer,value.Body,Resolver);
        writer.WriteBoolean(value.TailCall);
        ListParameter.RemoveRange(ListParameter_Count,Parameters.Count);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver) {
        var ListParameter= Resolver.Serializer().ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var type = reader.ReadType();
        var parameters= reader.Deserialize宣言Parameters(Resolver);
        ListParameter.AddRange(parameters!);
        var body = Expression.Read(ref reader,Resolver);
        var tailCall = reader.ReadBoolean();
        ListParameter.RemoveRange(ListParameter_Count,parameters.Length);
        return(T)Expressions.Expression.Lambda(
            type,
            body,
            tailCall,
            parameters!
        );
    }
}
