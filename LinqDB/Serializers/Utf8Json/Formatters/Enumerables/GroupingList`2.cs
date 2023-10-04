using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters.Enumerables;
using O = IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G = LinqDB.Enumerables;
public class GroupingList<TKey,TElement>:IJsonFormatter<G.GroupingList<TKey,TElement>>{
    public static readonly GroupingList<TKey,TElement> Instance=new();//リフレクションで使われる
    public void Serialize(ref Writer writer,G.GroupingList<TKey,TElement> value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        
        
        Resolver.GetFormatter<TKey>().Serialize(ref writer,value!.Key,Resolver);
        
        var Formatter=Resolver.GetFormatter<TElement>();
        
        foreach(var item in value!){
            writer.WriteValueSeparator();
            Formatter.Serialize(ref writer,item,Resolver);
        }
        writer.WriteEndArray();
    }
    public G.GroupingList<TKey,TElement> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var Key=Resolver.GetFormatter<TKey>().Deserialize(ref reader,Resolver);
        var value=new G.GroupingList<TKey,TElement>(Key);
        var Formatter=Resolver.GetFormatter<TElement>();
        while(!reader.ReadIsEndArray()){
            reader.ReadIsValueSeparatorWithVerify();
            var item=Formatter.Deserialize(ref reader,Resolver);
            value.Add(item);
        }
        return value;
    }
}
