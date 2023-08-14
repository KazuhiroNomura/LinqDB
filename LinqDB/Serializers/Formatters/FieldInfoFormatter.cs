using System;
using System.Linq;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionFormatter:IJsonFormatter<FieldInfo>,IMessagePackFormatter<FieldInfo>{
    //public static readonly MethodFormatter Instance=new();
    private IJsonFormatter<FieldInfo> FieldInfo=>this;
    public void Serialize(ref JsonWriter writer,FieldInfo? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        Serialize_Type(ref writer,value.ReflectedType,Resolver);
        //this.Serialize(ref writer,value.ReflectedType!,Resolver);
        writer.WriteValueSeparator();
        writer.WriteString(value.Name);
        writer.WriteValueSeparator();
        writer.WriteInt32(value.MetadataToken);
        writer.WriteEndArray();
    }
    FieldInfo IJsonFormatter<FieldInfo>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        //var ReflectedType= this.Type.Deserialize(ref reader,Resolver);
        var ReflectedType= Deserialize_Type(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Name=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var MetadataToken=reader.ReadInt32();
        reader.ReadIsEndArrayWithVerify();
        return ReflectedType.GetFields(BindingFlags.Instance|BindingFlags.Static|BindingFlags.Public|BindingFlags.NonPublic).Single(p=>p.Name==Name&&p.MetadataToken==MetadataToken);
    }
    public void Serialize(ref MessagePackWriter writer,FieldInfo value,MessagePackSerializerOptions Resolver){
        throw new NotImplementedException();
    }
    FieldInfo IMessagePackFormatter<FieldInfo>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        throw new NotImplementedException();
    }
}
