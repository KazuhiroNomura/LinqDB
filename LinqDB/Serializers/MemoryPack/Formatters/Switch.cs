using MemoryPack;
using Expressions=System.Linq.Expressions;
using MemoryPack.Formatters;
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
        CustomSerializerMemoryPack.Type.Serialize(ref writer,value!.Type);
        CustomSerializerMemoryPack.Expression.Serialize(ref writer,value.SwitchValue);
        CustomSerializerMemoryPack.Method.SerializeNullable(ref writer,value.Comparison);
        //this.Serialize(ref writer,value.Cases);
        CustomSerializerMemoryPack.Serialize(ref writer,value.Cases);
        CustomSerializerMemoryPack.Expression.Serialize(ref writer,value.DefaultBody);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.SwitchExpression? value){
        var type       = CustomSerializerMemoryPack.Type.DeserializeType(ref reader);
        var switchValue= CustomSerializerMemoryPack.Expression.Deserialize(ref reader);
        var comparison = CustomSerializerMemoryPack.Method.DeserializeNullable(ref reader);
        var cases      =reader.ReadArray<Expressions.SwitchCase>();
        var defaultBody= CustomSerializerMemoryPack.Expression.Deserialize(ref reader);
        value=Expressions.Expression.Switch(type,switchValue,defaultBody,comparison,cases!);
    }
}
