using System.Buffers;
using System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader = MemoryPackReader;
using static Extension;
using T = LambdaExpression;

public class Lambda:MemoryPackFormatter<T> {
    public static readonly Lambda Instance=new();
    private static void PrivateWrite<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        var ListParameter= writer.Serializer().ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Parameters=value!.Parameters;
        ListParameter.AddRange(Parameters);
        writer.WriteType(value.Type);
        writer.Serialize宣言Parameters(value.Parameters);
        Expression.Write(ref writer,value.Body);
        writer.WriteBoolean(value.TailCall);
        
        ListParameter.RemoveRange(ListParameter_Count,Parameters.Count);
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(ExpressionType.Lambda);
        PrivateWrite(ref writer,value);
    }
    internal static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        if(writer.TryWriteNil(value)) return;
        PrivateWrite(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value)=>WriteNullable(ref writer,value);
    internal static T Read(ref Reader reader){
        var ListParameter= reader.Serializer().ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var type = reader.ReadType();
        var parameters= reader.Deserialize宣言Parameters();
        ListParameter.AddRange(parameters!);
        var body = Expression.Read(ref reader);
        var tailCall = reader.ReadBoolean();
        ListParameter.RemoveRange(ListParameter_Count,parameters!.Length);
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
