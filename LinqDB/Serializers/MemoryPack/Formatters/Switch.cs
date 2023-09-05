using MemoryPack;
using Expressions = System.Linq.Expressions;
using System.Buffers;

namespace LinqDB.Serializers.MemoryPack.Formatters;


public class Switch:MemoryPackFormatter<Expressions.SwitchExpression>{
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.SwitchExpression? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal Expressions.SwitchExpression DeserializeSwitch(ref MemoryPackReader reader){
        Expressions.SwitchExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Expressions.SwitchExpression? value){
        MemoryPackCustomSerializer.Type.Serialize(ref writer,value!.Type);
        MemoryPackCustomSerializer.Expression.Serialize(ref writer,value.SwitchValue);
        MemoryPackCustomSerializer.Method.SerializeNullable(ref writer,value.Comparison);
        //this.Serialize(ref writer,value.Cases);
        MemoryPackCustomSerializer.Serialize(ref writer,value.Cases);
        MemoryPackCustomSerializer.Expression.Serialize(ref writer,value.DefaultBody);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.SwitchExpression? value){
        var type       = MemoryPackCustomSerializer.Type.DeserializeType(ref reader);
        var switchValue= MemoryPackCustomSerializer.Expression.Deserialize(ref reader);
        var comparison = MemoryPackCustomSerializer.Method.DeserializeNullable(ref reader);
        var cases      =reader.ReadArray<Expressions.SwitchCase>();
        var defaultBody= MemoryPackCustomSerializer.Expression.Deserialize(ref reader);
        value=Expressions.Expression.Switch(type,switchValue,defaultBody,comparison,cases!);
    }
}
