
using MemoryPack;
using System.Buffers;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;


using Reader=MemoryPackReader;
using T=Expressions.LambdaExpression;
public class Lambda:MemoryPackFormatter<T> {
    public static readonly Lambda Instance=new();
    private static void PrivateWrite<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        writer.WriteType(value!.Type);
        
        writer.Serialize宣言Parameters(value.Parameters);
        var Parameters=writer.Serializer().Parameters;
        var Parameters_Count=Parameters.Count;
        var value_Parameters=value.Parameters;
        Parameters.AddRange(value_Parameters);

        Expression.Write(ref writer,value.Body);
        
        writer.WriteBoolean(value.TailCall);
        Parameters.RemoveRange(Parameters_Count,value_Parameters.Count);
        
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{

        writer.WriteNodeType(Expressions.ExpressionType.Lambda);
        
        PrivateWrite(ref writer,value);
    }
    internal static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        if(writer.TryWriteNil(value)) return;
        
        PrivateWrite(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value)=>WriteNullable(ref writer,value);
    internal static T Read(ref Reader reader){
        var Parameters=reader.Serializer().Parameters;
        var Parameters_Count=Parameters.Count;
        var type=reader.ReadType();
        
        var parameters=reader.Deserialize宣言Parameters();
        Parameters.AddRange(parameters);
        
        
        var body=Expression.Read(ref reader);
        var tailCall=reader.ReadBoolean();
        Parameters.RemoveRange(Parameters_Count,parameters.Length);
        return System.Linq.Expressions.Expression.Lambda(
            type,
            body,
            tailCall,
            parameters!
        );
    }
    internal static T? ReadConversion(ref Reader reader){
        if(reader.TryReadNil()) return null;
        return Read(ref reader);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        value=Read(ref reader);
    }
}
