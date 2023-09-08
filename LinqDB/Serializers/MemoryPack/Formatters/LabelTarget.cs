using Expressions = System.Linq.Expressions;
using MemoryPack;
using System.Buffers;
using System.Reflection;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using C=Serializer;
using T=Expressions.LabelTarget;



public class LabelTarget:MemoryPackFormatter<T> {
    public static readonly LabelTarget Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal T DeserializeLabelTarget(ref Reader reader){
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(C.Instance.Dictionary_LabelTarget_int.TryGetValue(value,out var index)){
            writer.WriteVarInt(index);
        } else{
            var Dictionary_LabelTarget_int= C.Instance.Dictionary_LabelTarget_int;
            C.Instance.ListLabelTarget.Add(value);
            index=Dictionary_LabelTarget_int.Count;
            Dictionary_LabelTarget_int.Add(value,index);
            writer.WriteVarInt(index);
            writer.WriteType(value.Type);
            writer.WriteString(value.Name);
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var index=reader.ReadVarIntInt32();
        var ListLabelTarget= C.Instance.ListLabelTarget;
        T target;
        if(index<ListLabelTarget.Count){
            target=ListLabelTarget[index];
        } else{
            var type= Type.Instance.Deserialize(ref reader);
            var name=reader.ReadString();
            target=Expressions.Expression.Label(type,name);
            ListLabelTarget.Add(target);
            index=ListLabelTarget.Count;
            C.Instance.Dictionary_LabelTarget_int.Add(target,index);
        }
        value=target;
    }
}
