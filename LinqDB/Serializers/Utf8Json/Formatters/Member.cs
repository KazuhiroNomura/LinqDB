using System;
using System.Diagnostics;
using System.Reflection;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=MemberInfo;
using C=Serializer;

public class Member:IJsonFormatter<T> {
    public static readonly Member Instance=new();
    internal static void Write(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        var type=value.ReflectedType;
        writer.WriteType(type);
        writer.WriteValueSeparator();
        writer.WriteString(value.Name);
        writer.WriteValueSeparator();
        writer.WriteInt32(Array.IndexOf(Resolver.Serializer().TypeMembers.Get(type),value));
        writer.WriteEndArray();
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        Write(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,IJsonFormatterResolver Resolver){
        //if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        //var ReflectedType= this.Type.Deserialize(ref reader,Resolver);
        var ReflectedType=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var Name=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var Index=reader.ReadInt32();
        reader.ReadIsEndArrayWithVerify();
        return Resolver.Serializer().TypeMembers.Get(ReflectedType)[Index];
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        return Read(ref reader,Resolver);
    }
}
