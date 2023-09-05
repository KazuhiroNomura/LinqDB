using System.Buffers;
using System.Linq;
using System.Reflection;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;


public class Member:MemoryPackFormatter<MemberInfo>{
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,MemberInfo? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal MemberInfo DeserializeMemberInfo(ref MemoryPackReader reader){
        MemberInfo? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref MemberInfo? value){
        if(value is null){
            //writer.WriteNil();
            return;
        }
        MemoryPackCustomSerializer.Type.Serialize(ref writer,value.ReflectedType);
        //this.Serialize(ref writer,value.ReflectedType!);
        writer.WriteString(value.Name);
        writer.WriteVarInt(value.MetadataToken);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref MemberInfo? value){
        //if(reader.TryReadNil()) return;
        var ReflectedType= MemoryPackCustomSerializer.Type.DeserializeType(ref reader);
        var Name=reader.ReadString();
        var MetadataToken=reader.ReadVarIntInt32();
        value=ReflectedType.GetMembers(BindingFlags.Instance|BindingFlags.Static|BindingFlags.Public|BindingFlags.NonPublic).Single(p=>p.Name==Name&&p.MetadataToken==MetadataToken);
    }
}
