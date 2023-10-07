using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters.Enumerables;
using O = IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G = LinqDB.Enumerables;
public class GroupingList<TKey,TElement>:IJsonFormatter<G.GroupingList<TKey,TElement>>{
    public static readonly GroupingList<TKey,TElement> Instance=new();
    private GroupingList(){}
    public void Serialize(ref Writer writer,G.GroupingList<TKey,TElement> value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        writer.Write(value.Key,Resolver);
        var Formatter=Resolver.GetFormatter<TElement>();
        foreach(var item in value){
            writer.WriteValueSeparator();
            Formatter.Serialize(ref writer,item,Resolver);
        }
        writer.WriteEndArray();
    }
    public G.GroupingList<TKey,TElement> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var Key=reader.Read<TKey>(Resolver);
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
