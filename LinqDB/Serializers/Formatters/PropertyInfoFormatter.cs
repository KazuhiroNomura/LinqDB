using System;
using System.Linq;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<PropertyInfo>{
    private IJsonFormatter<PropertyInfo> PropertyInfo=>this;
    public void Serialize(ref JsonWriter writer,PropertyInfo? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        Serialize_Type(ref writer,value.ReflectedType,Resolver);
        writer.WriteValueSeparator();
        writer.WriteString(value.Name);
        writer.WriteValueSeparator();
        writer.WriteInt32(value.MetadataToken);
        writer.WriteEndArray();
    }
    PropertyInfo IJsonFormatter<PropertyInfo>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var ReflectedType= Deserialize_Type(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Name=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var MetadataToken=reader.ReadInt32();
        reader.ReadIsEndArrayWithVerify();
        return ReflectedType.GetProperties(BindingFlags.Instance|BindingFlags.Static|BindingFlags.Public|BindingFlags.NonPublic).Single(p=>p.Name==Name&&p.MetadataToken==MetadataToken);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<PropertyInfo>{
    private IMessagePackFormatter<PropertyInfo> MSPropertyInfo=>this;
    public void Serialize(ref MessagePackWriter writer,PropertyInfo? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        Serialize_Type(ref writer,value.ReflectedType,Resolver);
        writer.Write(value.Name);
        writer.WriteInt32(value.MetadataToken);
    }
    PropertyInfo IMessagePackFormatter<PropertyInfo>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var ReflectedType= Deserialize_Type(ref reader,Resolver);
        var Name=reader.ReadString();
        var MetadataToken=reader.ReadInt32();
        return ReflectedType.GetProperties(BindingFlags.Instance|BindingFlags.Static|BindingFlags.Public|BindingFlags.NonPublic).Single(p=>p.Name==Name&&p.MetadataToken==MetadataToken);
    }
}
