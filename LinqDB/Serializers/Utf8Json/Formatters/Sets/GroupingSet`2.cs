using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters.Sets;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G=LinqDB.Sets;
public class GroupingSet<TKey,TElement>:IJsonFormatter<G.GroupingSet<TKey,TElement>>{
    public static readonly GroupingSet<TKey,TElement> Instance=new();//リフレクションで使われる
    public void Serialize(ref Writer writer,G.GroupingSet<TKey,TElement> value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        Resolver.GetFormatter<TKey>().Serialize(ref writer,value.Key,Resolver);
        var Formatter = Resolver.GetFormatter<TElement>();
        foreach(var item in value){
            writer.WriteValueSeparator();
            writer.Write(Formatter,item,Resolver);;
        }
        writer.WriteEndArray();
    }
    public G.GroupingSet<TKey,TElement> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var Key= Resolver.GetFormatter<TKey>().Deserialize(ref reader,Resolver);
        var Formatter = Resolver.GetFormatter<TElement>();
        var value=new G.GroupingSet<TKey,TElement>(Key);
        while(!reader.ReadIsEndArray()) {
            reader.ReadIsValueSeparatorWithVerify();
            value.Add(reader.Read(Formatter,Resolver));
        }
        return value;
    }
}
