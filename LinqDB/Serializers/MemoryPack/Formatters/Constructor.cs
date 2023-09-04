﻿using System.Buffers;
using System.Linq;
using System.Reflection;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;


public class Constructor:MemoryPackFormatter<ConstructorInfo>{
    private readonly 必要なFormatters Formatters;
    public Constructor(必要なFormatters Formatters)=>this.Formatters=Formatters;
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,ConstructorInfo? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal ConstructorInfo DeserializeConstructorInfo(ref MemoryPackReader reader){
        ConstructorInfo? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref ConstructorInfo? value){
        this.Formatters.Type.Serialize(ref writer,value!.ReflectedType!);
        writer.WriteVarInt(value.MetadataToken);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref ConstructorInfo? value){
        var ReflectedType= this.Formatters.Type.DeserializeType(ref reader);
        var MetadataToken=reader.ReadVarIntInt32();
        value=ReflectedType.GetConstructors(BindingFlags.Instance|BindingFlags.Public|BindingFlags.NonPublic).Single(p=>p.MetadataToken==MetadataToken);
    }
}
