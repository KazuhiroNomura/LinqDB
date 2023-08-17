using System.Linq;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<ConstructorInfo>{
    private IJsonFormatter<ConstructorInfo> ConstructorInfo=>this;
    public void Serialize(ref JsonWriter writer,ConstructorInfo value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        //this.Serialize(ref writer,value.ReflectedType!,Resolver);
        Serialize_Type(ref writer,value.ReflectedType!,Resolver);
        writer.WriteValueSeparator();
        writer.WriteInt32(value.MetadataToken);
        writer.WriteEndArray();
    }
    ConstructorInfo IJsonFormatter<ConstructorInfo>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var ReflectedType= Deserialize_Type(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var MetadataToken=reader.ReadInt32();
        reader.ReadIsEndArrayWithVerify();
        return ReflectedType.GetConstructors(BindingFlags.Instance|BindingFlags.Public|BindingFlags.NonPublic).Single(p=>p.MetadataToken==MetadataToken);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<ConstructorInfo>{
    private IMessagePackFormatter<ConstructorInfo> ConstructorInfo=>this;
    public void Serialize(ref MessagePackWriter writer,ConstructorInfo value,MessagePackSerializerOptions Resolver){
        Serialize_Type(ref writer,value.ReflectedType!,Resolver);
        writer.WriteInt32(value.MetadataToken);
    }
    ConstructorInfo IMessagePackFormatter<ConstructorInfo>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var ReflectedType= Deserialize_Type(ref reader,Resolver);
        var MetadataToken=reader.ReadInt32();
        return ReflectedType.GetConstructors(BindingFlags.Instance|BindingFlags.Public|BindingFlags.NonPublic).Single(p=>p.MetadataToken==MetadataToken);
    }
}
