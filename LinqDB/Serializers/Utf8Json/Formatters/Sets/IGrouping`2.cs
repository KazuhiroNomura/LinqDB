using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters.Sets;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G=LinqDB.Sets;
public class IGrouping<TKey,TElement>:IJsonFormatter<G.IGrouping<TKey,TElement>>{
    public static readonly IGrouping<TKey,TElement> Instance=new();//リフレクションで使われる
    public void Serialize(ref Writer writer,G.IGrouping<TKey,TElement> value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        Resolver.GetFormatter<TKey>().Serialize(ref writer,value.Key,Resolver);
        writer.WriteValueSeparator();
        var Formatter = Resolver.GetFormatter<TElement>();
        var first=true;
        foreach(var item in value){
            if(first) first=false;
            else writer.WriteValueSeparator();
            Formatter.Serialize(ref writer,item,Resolver);
        }
        writer.WriteEndArray();
    }
    public G.IGrouping<TKey,TElement> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var Key= Resolver.GetFormatter<TKey>().Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var value=new G.GroupingSet<TKey,TElement>(Key);
        var Formatter = Resolver.GetFormatter<TElement>();
        var first=true;
        while(!reader.ReadIsEndArray()) {
            if(first) first=false;
            else reader.ReadIsValueSeparatorWithVerify();
            var item = Formatter.Deserialize(ref reader,Resolver);
            value.Add(item);
        }
        return value;
    }
}
