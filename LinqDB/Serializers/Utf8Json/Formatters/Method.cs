using System;
using System.Diagnostics;
using System.Reflection;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using static Extension;
using T= MethodInfo;
using C=Serializer;
public class Method:IJsonFormatter<T> {
    public static readonly Method Instance=new();
    internal void SerializeNullable(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.WriteIsNull(value)){
        }else this.Serialize(ref writer,value,Resolver);
    }
    internal T? DeserializeNullable(ref Reader reader,IJsonFormatterResolver Resolver) {
        return reader.ReadIsNull() ?null: this.Deserialize(ref reader,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.WriteIsNull(value))return;
        writer.WriteBeginArray();
        var type=value!.ReflectedType;
        writer.WriteType(type);
        writer.WriteValueSeparator();
        writer.WriteString(value.Name);
        writer.WriteValueSeparator();
        writer.WriteInt32(Array.IndexOf(C.Instance.TypeMethods.Get(type),value));
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var type= reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var name=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var index=reader.ReadInt32();
        reader.ReadIsEndArrayWithVerify();
        return C.Instance.TypeMethods.Get(type)[index];
    }
}
