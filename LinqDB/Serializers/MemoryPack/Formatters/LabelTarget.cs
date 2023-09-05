using Expressions = System.Linq.Expressions;
using MemoryPack;
using System.Buffers;

namespace LinqDB.Serializers.MemoryPack.Formatters;


public class LabelTarget:MemoryPackFormatter<Expressions.LabelTarget>{
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.LabelTarget? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal Expressions.LabelTarget DeserializeLabelTarget(ref MemoryPackReader reader){
        Expressions.LabelTarget? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Expressions.LabelTarget? value){
        if(value is null){
            MemoryPackCustomSerializer.WriteBoolean(ref writer,false);
            return;
        }
        MemoryPackCustomSerializer.WriteBoolean(ref writer,true);
        if(MemoryPackCustomSerializer.Dictionary_LabelTarget_int.TryGetValue(value,out var index)){
            writer.WriteVarInt(index);
        } else{
            var Dictionary_LabelTarget_int=MemoryPackCustomSerializer.Dictionary_LabelTarget_int;
            MemoryPackCustomSerializer.ListLabelTarget.Add(value);
            index=Dictionary_LabelTarget_int.Count;
            Dictionary_LabelTarget_int.Add(value,index);
            writer.WriteVarInt(index);
            MemoryPackCustomSerializer.Type.Serialize(ref writer,value.Type);
            writer.WriteString(value.Name);
        }
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.LabelTarget? value){
        if(!MemoryPackCustomSerializer.ReadBoolean(ref reader)) return;
        var index=reader.ReadVarIntInt32();
        var ListLabelTarget=MemoryPackCustomSerializer.ListLabelTarget;
        Expressions.LabelTarget target;
        if(index<ListLabelTarget.Count){
            target=ListLabelTarget[index];
        } else{
            var type=MemoryPackCustomSerializer.Type.DeserializeType(ref reader);
            var name=reader.ReadString();
            target=Expressions.Expression.Label(type,name);
            ListLabelTarget.Add(target);
            index=ListLabelTarget.Count;
            MemoryPackCustomSerializer.Dictionary_LabelTarget_int.Add(target,index);
        }
        value=target;
    }
}
