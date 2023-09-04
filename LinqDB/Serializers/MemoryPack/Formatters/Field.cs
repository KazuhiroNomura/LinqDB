using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;


public class Field:MemoryPackFormatter<FieldInfo>{
    private readonly 必要なFormatters Formatters;
    public Field(必要なFormatters Formatters)=>this.Formatters=Formatters;
    private readonly Dictionary<System.Type,FieldInfo[]> TypeFields=new();
    internal void Clear()=>this.TypeFields.Clear();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref FieldInfo? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        var ReflectedType=value.ReflectedType!;
        this.Formatters.Type.Serialize(ref writer,ReflectedType);
        var Fields= this.Get(ReflectedType);
        writer.WriteVarInt(Array.IndexOf(Fields,value));
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref FieldInfo? value){
        var ReflectedType= this.Formatters.Type.DeserializeType(ref reader);
        var Fields= this.Get(ReflectedType);
        var Index=reader.ReadVarIntInt32();
        value=Fields[Index];
    }
    private FieldInfo[] Get(System.Type ReflectedType){
        if(!this.TypeFields.TryGetValue(ReflectedType,out var Fields)){
            Fields=ReflectedType.GetFields(BindingFlags.Instance|BindingFlags.Static|BindingFlags.Public).ToArray();
            Array.Sort(Fields,(a,b)=>string.CompareOrdinal(a.Name,b.Name));
        }
        return Fields;
    }
}
