using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
public class ExpressionT<T>:IMessagePackFormatter<T>where T:Expressions.LambdaExpression {
    public static readonly ExpressionT<T> Instance=new();
    public void Serialize(ref Writer writer,T? value,O Resolver) {
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(4);
        writer.WriteType(value!.Type);
        
        writer.Serialize宣言Parameters(value.Parameters,Resolver);
        var Parameters= Resolver.Serializer().Parameters;
        var Parameters_Count=Parameters.Count;
        var value_Parameters=value.Parameters;
        Parameters.AddRange(value_Parameters);
        
        Expression.Write(ref writer,value.Body,Resolver);
        
        writer.WriteBoolean(value.TailCall);
        
        Parameters.RemoveRange(Parameters_Count,value_Parameters.Count);
    }
    public T Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        var type = reader.ReadType();
        
        var parameters= reader.Deserialize宣言Parameters(Resolver);
        Debug.Assert(count==4);
        var Parameters= Resolver.Serializer().Parameters;
        var Parameters_Count=Parameters.Count;
        
        Parameters.AddRange(parameters!);
        var body = Expression.Read(ref reader,Resolver);
        
        var tailCall = reader.ReadBoolean();
        
        Parameters.RemoveRange(Parameters_Count,parameters.Length);
        return(T)Expressions.Expression.Lambda(
            type,
            body,
            tailCall,
            parameters!
        );
    }
}
