using MemoryPack;

using System;
using System.Buffers;
using System.Diagnostics;
using System.Reflection;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using static Extension;
using T= System.Delegate;
using C=Serializer;
public class Delegate2:IJsonFormatter<T> {
    public static readonly Delegate2 Instance =new();
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.WriteIsNull(value))return;
        writer.WriteBeginArray();
        writer.WriteType(value!.GetType());
        writer.WriteValueSeparator();
        Method.Instance.Serialize(ref writer,value.Method,Resolver);
        writer.WriteValueSeparator();
        Object.Instance.Serialize(ref writer,value.Target,Resolver);
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var delegateType=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var method=Method.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var target=Object.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return method.CreateDelegate(delegateType,target);
    }
}
