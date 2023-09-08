using System.Linq;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=ConstructorInfo;
using static Common;
public class Constructor:IJsonFormatter<T> {
    public static readonly Constructor Instance=new();
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        Type.Instance.Serialize(ref writer,value.ReflectedType!,Resolver);
        writer.WriteValueSeparator();
        writer.WriteInt32(value.MetadataToken);
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var ReflectedType= reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var MetadataToken=reader.ReadInt32();
        reader.ReadIsEndArrayWithVerify();
        return ReflectedType.GetConstructors(BindingFlags.Instance|BindingFlags.Public|BindingFlags.NonPublic).Single(p=>p.MetadataToken==MetadataToken);
    }
}
