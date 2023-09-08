using System;
using System.Buffers;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Linq;
using System.Reflection;
using LinqDB.Serializers.MemoryPack;
using MemoryPack;

using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using static Common;
using T= MethodInfo;
using C=Serializer;
public class Method:IJsonFormatter<T> {
    public static readonly Method Instance=new();
    internal void SerializeNullable(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(value is null)writer.WriteNull();
        else this.Serialize(ref writer,value,Resolver);
    }
    internal T? DeserializeNullable(ref Reader reader,IJsonFormatterResolver Resolver) {
        return reader.ReadIsNull() ?null: this.Deserialize(ref reader,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        var ReflectedType=value.ReflectedType;
        writer.WriteType(ReflectedType);
        writer.WriteValueSeparator();
        writer.WriteString(value.Name);
        writer.WriteValueSeparator();
        writer.WriteInt32(Array.IndexOf(C.Instance.TypeMethods.Get(ReflectedType),value));
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        //var ReflectedType= this.Type.Deserialize(ref reader,Resolver);
        var ReflectedType= reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var Name=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var Index=reader.ReadInt32();
        reader.ReadIsEndArrayWithVerify();
        return C.Instance.TypeMethods.Get(ReflectedType)[Index];
    }
}
