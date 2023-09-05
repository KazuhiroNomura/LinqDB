using System.Buffers;
using MemoryPack;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;


public class SwitchCase:MemoryPackFormatter<Expressions.SwitchCase>{
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.SwitchCase? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    private Expressions.SwitchCase DeserializeSwitchCase(ref MemoryPackReader reader){
        Expressions.SwitchCase? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Expressions.SwitchCase? value){
        MemoryPackCustomSerializer.Serialize(ref writer,value!.TestValues);
        MemoryPackCustomSerializer.Expression.Serialize(ref writer,value.Body);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.SwitchCase? value){
        var testValues=reader.ReadArray<Expressions.Expression>();
        var body= MemoryPackCustomSerializer.Expression.Deserialize(ref reader);
        value=Expressions.Expression.SwitchCase(body,testValues!);
    }
}
