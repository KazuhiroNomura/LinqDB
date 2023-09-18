using System.Buffers;
using MemoryPack;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;

using Reader = MemoryPackReader;
using T = Expressions.BlockExpression;
public class Block:MemoryPackFormatter<T> {
    public static readonly Block Instance=new();


    private static void PrivateSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        var ListParameter= writer.Serializer().ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Variables=value!.Variables;
        ListParameter.AddRange(Variables);
        writer.WriteType(value.Type);

        writer.Serialize宣言Parameters(Variables);
        
        writer.WriteCollection(value.Expressions);
        ListParameter.RemoveRange(ListParameter_Count,Variables.Count);
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{

        writer.WriteNodeType(Expressions.ExpressionType.Block);
        
        PrivateSerialize(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value))return;

        PrivateSerialize(ref writer,value);
        
    }
    internal static T Read(ref Reader reader){
        var ListParameter= reader.Serializer().ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var type= reader.ReadType();
        
        var variables= reader.Deserialize宣言Parameters();
        
        ListParameter.AddRange(variables!);
        var expressions=reader.ReadArray<Expressions.Expression>();
        ListParameter.RemoveRange(ListParameter_Count,variables!.Length);
        return Expressions.Expression.Block(type,variables!,expressions!);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value)=>value=reader.TryReadNil()?null:Read(ref reader);
}
