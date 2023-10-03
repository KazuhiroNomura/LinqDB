using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters.Sets;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using Sets=LinqDB.Sets;
public class GroupingSet<TKey,TElement>:IJsonFormatter<Sets.GroupingSet<TKey,TElement>>{
    public static readonly GroupingSet<TKey,TElement> Instance=new();//リフレクションで使われる
    public void Serialize(ref Writer writer,Sets.GroupingSet<TKey,TElement> value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        Resolver.GetFormatter<TKey>().Serialize(ref writer,value!.Key,Resolver);
        var Formatter = Resolver.GetFormatter<TElement>();
        foreach(var item in value!){
            writer.WriteValueSeparator();
            Formatter.Serialize(ref writer,item,Resolver);
        }
        writer.WriteEndArray();
    }
    public Sets.GroupingSet<TKey,TElement> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var Key= Resolver.GetFormatter<TKey>().Deserialize(ref reader,Resolver);
        var value=new Sets.GroupingSet<TKey,TElement>(Key);
        var Formatter = Resolver.GetFormatter<TElement>();
        while(!reader.ReadIsEndArray()) {
        reader.ReadIsValueSeparatorWithVerify();
            var item = Formatter.Deserialize(ref reader,Resolver);
            value.Add(item);
        }
        return value;
    }
}
