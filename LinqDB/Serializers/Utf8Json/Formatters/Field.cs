using System;
using System.Diagnostics;
using System.Reflection;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=FieldInfo;
using static Common;
using C=Serializer;
public class Field:IJsonFormatter<T> {
    public static readonly Field Instance=new();
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
       // if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        var trype=value.ReflectedType;
        writer.WriteType(trype);
        writer.WriteValueSeparator();
        writer.WriteString(value.Name);
        writer.WriteValueSeparator();
        writer.WriteInt32(Array.IndexOf(C.Instance.TypeMembers.Get(trype),value));
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        //if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        //var ReflectedType= this.Type.Deserialize(ref reader,Resolver);
        var type= reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var name=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var index=reader.ReadInt32();
        reader.ReadIsEndArrayWithVerify();
        return C.Instance.TypeFields.Get(type)[index];
    }
}
