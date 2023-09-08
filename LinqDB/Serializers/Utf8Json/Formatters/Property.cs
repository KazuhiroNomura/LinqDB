using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using static Common;
using C=Serializer;
public class Property:IJsonFormatter<PropertyInfo>{
    public static readonly Property Instance=new();
    public void Serialize(ref Writer writer,PropertyInfo? value,IJsonFormatterResolver Resolver){
        if(writer.WriteIsNull(value))return;
        writer.WriteBeginArray();
        Debug.Assert(value!=null,nameof(value)+" != null");
        var ReflectedType=value.ReflectedType;
        writer.WriteType(ReflectedType);
        writer.WriteValueSeparator();
        writer.WriteString(value.Name);
        writer.WriteValueSeparator();
        writer.WriteInt32(Array.IndexOf(C.Instance.TypeProperties.Get(ReflectedType),value));
        writer.WriteEndArray();
    }
    public PropertyInfo Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var ReflectedType= reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var Name=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var Index=reader.ReadInt32();
        reader.ReadIsEndArrayWithVerify();
        return C.Instance.TypeProperties.Get(ReflectedType)[Index];
    }
}
