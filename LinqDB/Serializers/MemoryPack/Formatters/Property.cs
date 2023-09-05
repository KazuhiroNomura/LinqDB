using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;


public class Property:MemoryPackFormatter<PropertyInfo>{
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,PropertyInfo? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal PropertyInfo DeserializePropertyInfo(ref MemoryPackReader reader){
        PropertyInfo? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
   public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref PropertyInfo? value){
       Debug.Assert(value!=null,nameof(value)+" != null");
       var ReflectedType=value.ReflectedType!;
       CustomSerializerMemoryPack.Type.Serialize(ref writer,ReflectedType);
       var Properties=this.Get(ReflectedType);
       writer.WriteVarInt(Array.IndexOf(Properties,value));
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref PropertyInfo? value){
        var ReflectedType= CustomSerializerMemoryPack.Type.DeserializeType(ref reader);
        var Properties=this.Get(ReflectedType);
        var Index=reader.ReadVarIntInt32();
        value=Properties[Index];
    }
    private PropertyInfo[] Get(System.Type ReflectedType){
        if(!CustomSerializerMemoryPack.TypeProperties.TryGetValue(ReflectedType,out var Properties)){
            Properties=ReflectedType.GetProperties(BindingFlags.Instance|BindingFlags.Static|BindingFlags.Public).ToArray();
            Array.Sort(Properties,(a,b)=>string.CompareOrdinal(a.Name,b.Name));
        }
        return Properties;
    }
}
