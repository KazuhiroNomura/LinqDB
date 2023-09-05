using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;


public class Field:MemoryPackFormatter<FieldInfo>{
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref FieldInfo? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        var ReflectedType=value.ReflectedType!;
        CustomSerializerMemoryPack.Type.Serialize(ref writer,ReflectedType);
        var Fields= this.Get(ReflectedType);
        writer.WriteVarInt(Array.IndexOf(Fields,value));
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref FieldInfo? value){
        var ReflectedType= CustomSerializerMemoryPack.Type.DeserializeType(ref reader);
        var Fields= this.Get(ReflectedType);
        var Index=reader.ReadVarIntInt32();
        value=Fields[Index];
    }
    private FieldInfo[] Get(System.Type ReflectedType){
        if(!CustomSerializerMemoryPack.TypeFields.TryGetValue(ReflectedType,out var Fields)){
            Fields=ReflectedType.GetFields(BindingFlags.Instance|BindingFlags.Static|BindingFlags.Public).ToArray();
            Array.Sort(Fields,(a,b)=>string.CompareOrdinal(a.Name,b.Name));
        }
        return Fields;
    }
}
