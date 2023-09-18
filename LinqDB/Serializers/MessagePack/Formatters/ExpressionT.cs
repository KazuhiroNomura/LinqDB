using MessagePack;
using MessagePack.Formatters;
using System.Diagnostics;

using Expressions = System.Linq.Expressions;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using static Extension;

public class ExpressionT<T>:IMessagePackFormatter<T>where T:Expressions.LambdaExpression {
    public static readonly ExpressionT<T> Instance=new();
    private static void PrivateWrite(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
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
    internal static void Write(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(5);
        writer.WriteNodeType(Expressions.ExpressionType.Lambda);
        PrivateWrite(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver) {
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(4);
        PrivateWrite(ref writer,value,Resolver);
    }
    private static T PrivateRead(ref Reader reader,MessagePackSerializerOptions Resolver){
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
    internal static T Read(ref Reader reader,MessagePackSerializerOptions Resolver){
        return PrivateRead(ref reader,Resolver);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver) {
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==4);
        return PrivateRead(ref reader,Resolver);
    }
}
