using Expressions = System.Linq.Expressions;
using MemoryPack;
using System.Buffers;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader = MemoryPackReader;
using T = Expressions.LabelTarget;
using static Extension;


public class LabelTarget:MemoryPackFormatter<T> {
    public static readonly LabelTarget Instance=new();
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
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
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value);
    }
    internal static T Read(ref Reader reader){
        var s=reader.Serializer();
        var index=reader.ReadVarIntInt32();
        var LabelTargets= s.LabelTargets;
        T value;
        if(index<LabelTargets.Count){
            value=LabelTargets[index];
        } else{
            var type= reader.ReadType();
            var name=reader.ReadString();
            value=Expressions.Expression.Label(type,name);
            LabelTargets.Add(value);
            index=LabelTargets.Count;
            s.Dictionary_LabelTarget_int.Add(value,index);
        }
        return value;
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        value=Read(ref reader);
    }
}
