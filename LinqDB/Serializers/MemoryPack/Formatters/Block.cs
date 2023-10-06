using MemoryPack;
using System.Buffers;

using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;


using Reader=MemoryPackReader;
using T=Expressions.BlockExpression;
public class Block:MemoryPackFormatter<T>{
    public static readonly Block Instance=new();
    private static void PrivateWrite<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        var Variables=value!.Variables;
        writer.WriteType(value.Type);

        writer.Serialize宣言Parameters(Variables);
        var Parameters=writer.Serializer().Parameters;
        var Parameters_Count=Parameters.Count;
        Parameters.AddRange(Variables);

        writer.WriteCollection(value.Expressions);
        Parameters.RemoveRange(Parameters_Count,Variables.Count);
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{

        writer.WriteNodeType(Expressions.ExpressionType.Block);

        PrivateWrite(ref writer,value);
        
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;

        PrivateWrite(ref writer,value);

    }
    internal static T Read(ref Reader reader){
        var type=reader.ReadType();

        var variables=reader.Deserialize宣言Parameters();
        var ListParameter=reader.Serializer().Parameters;
        var ListParameter_Count=ListParameter.Count;

        ListParameter.AddRange(variables);
        var expressions=reader.ReadArray<Expressions.Expression>();
        ListParameter.RemoveRange(ListParameter_Count,variables.Length);
        return Expressions.Expression.Block(type,variables,expressions!);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value)=>value=reader.TryReadNil()?null:Read(ref reader);
}
