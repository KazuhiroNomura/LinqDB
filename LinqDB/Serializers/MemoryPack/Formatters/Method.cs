using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;

using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
public class Method:MemoryPackFormatter<MethodInfo>{
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,MethodInfo? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal void SerializeNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,MethodInfo? value) where TBufferWriter : IBufferWriter<byte> {
        CustomSerializerMemoryPack.WriteBoolean(ref writer,value is not null);
        if(value is not null) this.Serialize(ref writer,ref value);
    }
    internal MethodInfo Deserialize(ref MemoryPackReader reader) {
        MethodInfo? value = default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    internal MethodInfo? DeserializeNullable(ref MemoryPackReader reader) {
        var value0 = CustomSerializerMemoryPack.ReadBoolean(ref reader);
        return value0 ? this.Deserialize(ref reader) : null;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref MethodInfo? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        var ReflectedType=value.ReflectedType!;
        CustomSerializerMemoryPack.Type.Serialize(ref writer,ReflectedType);
        var Methods= this.Get(ReflectedType);
        writer.WriteVarInt(Array.IndexOf(Methods,value));
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref MethodInfo? value){
        var ReflectedType= CustomSerializerMemoryPack.Type.DeserializeType(ref reader);
        var Methods= this.Get(ReflectedType);
        var Index=reader.ReadVarIntInt32();
        value=Methods[Index];
    }
    private MethodInfo[] Get(System.Type ReflectedType){
        if(!CustomSerializerMemoryPack.TypeMethods.TryGetValue(ReflectedType,out var Methods)){
            Methods=ReflectedType.GetMethods(BindingFlags.Instance|BindingFlags.Static|BindingFlags.Public).ToArray();
            Array.Sort(Methods,(a,b)=>string.CompareOrdinal(a.ToString(),b.ToString()));
        }
        return Methods;
    }
}
