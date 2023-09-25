using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.LambdaExpression;
public class Lambda:IMessagePackFormatter<T> {
    public static readonly Lambda Instance=new();
    private static void PrivateWrite(ref Writer writer,T? value,O Resolver){
        writer.WriteType(value!.Type);
        
        writer.Serialize宣言Parameters(value.Parameters,Resolver);
        var Parameters=Resolver.Serializer().Parameters;
        var Parameters_Count=Parameters.Count;
        var value_Parameters=value!.Parameters;
        Parameters.AddRange(value_Parameters);
        
        Expression.Write(ref writer,value.Body,Resolver);
        
        writer.WriteBoolean(value.TailCall);
        Parameters.RemoveRange(Parameters_Count,value_Parameters.Count);
        
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(5);
        writer.WriteNodeType(Expressions.ExpressionType.Lambda);
        
        PrivateWrite(ref writer,value,Resolver);
    }
    internal static void WriteNullable(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(4);
        PrivateWrite(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T value,O Resolver)=>WriteNullable(ref writer,value,Resolver);
    internal static T Read(ref Reader reader,O Resolver){
        var type=reader.ReadType();
        
        var parameters=reader.Deserialize宣言Parameters(Resolver);
        var Parameters=Resolver.Serializer().Parameters;
        var Parameters_Count=Parameters.Count;
        Parameters.AddRange(parameters);
        
        var body=Expression.Read(ref reader,Resolver);
        
        var tailCall=reader.ReadBoolean();
        Parameters.RemoveRange(Parameters_Count,parameters.Length);
        return Expressions.Expression.Lambda(
            type,
            body,
            tailCall,
            parameters
        );
    }
    internal static T? ReadNullable(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==4);
        return Read(ref reader,Resolver);
    }
    public T Deserialize(ref Reader reader,O Resolver)=>ReadNullable(ref reader,Resolver)!;
}
