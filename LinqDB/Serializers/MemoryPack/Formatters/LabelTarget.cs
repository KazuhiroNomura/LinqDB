
using MemoryPack;
using System.Buffers;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;


using Reader = MemoryPackReader;
using T = Expressions.LabelTarget;
public class LabelTarget:MemoryPackFormatter<T> {
    public static readonly LabelTarget Instance=new();
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{

        var Serializer=writer.Serializer();
        if(Serializer.Dictionary_LabelTarget_int.TryGetValue(value,out var index)){

            writer.WriteVarInt(index);
        } else{
            
            var Dictionary_LabelTarget_int=Serializer.Dictionary_LabelTarget_int;
            index=Dictionary_LabelTarget_int.Count;
            Serializer.LabelTargets.Add(value);
            Dictionary_LabelTarget_int.Add(value,index);
            writer.WriteVarInt(index);
            
            writer.WriteType(value.Type);
            
            writer.WriteString(value.Name);
        }
        
    }
    private static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter>writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        if(writer.TryWriteNil(value))return;
        Write(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value)=>WriteNullable(ref writer,value);
    internal static T Read(ref Reader reader){
        var Serializer=reader.Serializer();
        
        var index=reader.ReadVarIntInt32();
        var LabelTargets=Serializer.LabelTargets;
        T value;
        if(index<LabelTargets.Count){
            
            value=LabelTargets[index];
        } else{
            
            var type= reader.ReadType();
            
            var name=reader.ReadString();
            value=Expressions.Expression.Label(type,name);
            Serializer.Dictionary_LabelTarget_int.Add(value,LabelTargets.Count);
            LabelTargets.Add(value);
        }
        
        return value;
    }
    private static T? ReadNullable(ref Reader reader)=>reader.TryReadNil()?null:Read(ref reader);
    public override void Deserialize(ref Reader reader,scoped ref T? value)=>value=ReadNullable(ref reader);
}
