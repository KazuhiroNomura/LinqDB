using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters.Sets;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using Sets=LinqDB.Sets;
public class Set<T>:IJsonFormatter<Sets.Set<T>>{
    public static readonly Set<T> Instance=new();
    private Set(){}
    private static void WriteNullable(ref Writer writer,Sets.IEnumerable<T>? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        var Formatter = Resolver.GetFormatter<T>();
        var first=true;
        foreach(var item in value!){
            if(first) first=false;
            else writer.WriteValueSeparator();
            Formatter.Serialize(ref writer,item,Resolver);
        }
        writer.WriteEndArray();
    }
    public void Serialize(ref Writer writer,Sets.Set<T>? value,O Resolver)=>WriteNullable(ref writer,value,Resolver);
    internal static Sets.Set<T> Read(ref Reader reader,O Resolver){
        var value=new Sets.Set<T>();
        var Formatter = Resolver.GetFormatter<T>();
        while(!reader.ReadIsEndArray()) {
            reader.ReadIsValueSeparatorWithVerify();
            var item = Formatter.Deserialize(ref reader,Resolver);
            value.Add(item);
        }
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
    private static Sets.Set<T>? ReadNullable(ref Reader reader,O Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var value=new Sets.Set<T>();
        var Formatter = Resolver.GetFormatter<T>();
        var first=true;
        while(!reader.ReadIsEndArray()) {
            if(first) first=false;
            else reader.ReadIsValueSeparatorWithVerify();
            var item = Formatter.Deserialize(ref reader,Resolver);
            value.Add(item);
        }
        return value;
    }
    public Sets.Set<T> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        return ReadNullable(ref reader,Resolver)!;
    }
}
