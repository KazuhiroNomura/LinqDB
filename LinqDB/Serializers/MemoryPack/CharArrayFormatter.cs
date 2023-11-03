using MemoryPack;

namespace LinqDB.Serializers.MemoryPack;
public sealed class CharArrayFormatter:MemoryPackFormatter<char[]>{
    public static readonly CharArrayFormatter Instance=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref char[]? value){
        if(writer.TryWriteNil(value)) return;
        writer.WriteVarInt(value!.Length);
        foreach(var c in value) 
            writer.WriteVarInt((short)c);
    }

    public override void Deserialize(ref MemoryPackReader reader,scoped ref char[]? value){
        if(reader.TryReadNil()) return;
        var Length=reader.ReadVarIntInt32();
        var array=new char[Length];
        for(var a=0;a<Length;a++) 
            array[a]=(char)reader.ReadVarIntInt16();
        value=array;
    }
}
