using System;
using System.Reflection;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters.Sets;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using Sets=LinqDB.Sets;
public class IEnumerable<T>:IJsonFormatter<Sets.IEnumerable<T>>  {
    //public static readonly Set<T> Instance=new();
    public void Serialize(ref JsonWriter writer,Sets.IEnumerable<T> value,IJsonFormatterResolver Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        var type=value.GetType();
        writer.WriteType(type);
        writer.WriteValueSeparator();
        writer.WriteValue(type,value,Resolver);
        writer.WriteEndArray();
    }
    public Sets.IEnumerable<T> Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var value=reader.ReadValue(type,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return (Sets.IEnumerable<T>)value;
    }
}
