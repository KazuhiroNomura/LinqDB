using Expressions = System.Linq.Expressions;
using MemoryPack;
using System.Buffers;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using C=Serializer;
using T=Expressions.LabelTarget;
using static Extension;


public class LabelTarget:MemoryPackFormatter<T> {
    public static readonly LabelTarget Instance=new();
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    internal static T DeserializeLabelTarget(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        var s=writer.Serializer();
        if(s.Dictionary_LabelTarget_int.TryGetValue(value,out var index)){
            writer.WriteVarInt(index);
        } else{
            var Dictionary_LabelTarget_int= s.Dictionary_LabelTarget_int;
            s.LabelTargets.Add(value);
            index=Dictionary_LabelTarget_int.Count;
            Dictionary_LabelTarget_int.Add(value,index);
            writer.WriteVarInt(index);
            writer.WriteType(value.Type);
            writer.WriteString(value.Name);
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var s=reader.Serializer();
        var index=reader.ReadVarIntInt32();
        var LabelTargets= s.LabelTargets;
        T target;
        if(index<LabelTargets.Count){
            target=LabelTargets[index];
        } else{
            var type= reader.ReadType();
            var name=reader.ReadString();
            target=Expressions.Expression.Label(type,name);
            LabelTargets.Add(target);
            index=LabelTargets.Count;
            s.Dictionary_LabelTarget_int.Add(target,index);
        }
        value=target;
    }
}
