using System.Linq;
using System.Reflection;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=ConstructorInfo;
using static Extension;
public class Constructor:IJsonFormatter<T> {
    public static readonly Constructor Instance=new();
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        writer.WriteType(value.ReflectedType);
        writer.WriteValueSeparator();
        writer.WriteInt32(value.MetadataToken);
        writer.WriteEndArray();
    }
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        Write(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var type= reader.ReadType();
        var array= Resolver.Serializer().TypeConstructors.Get(type);
        reader.ReadIsValueSeparatorWithVerify();
        var index=reader.ReadInt32();
        reader.ReadIsEndArrayWithVerify();
        return type.GetConstructors(BindingFlags.Instance|BindingFlags.Public|BindingFlags.NonPublic).Single(p=>p.MetadataToken==index);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        return Read(ref reader,Resolver);
    }
}
