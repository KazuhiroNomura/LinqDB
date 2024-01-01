using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters.Enumerables;
using O = IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G = LinqDB.Enumerables;
public class GroupingList<TKey,TElement>:IJsonFormatter<G.Grouping<TKey,TElement>>{
    public static readonly GroupingList<TKey,TElement> Instance=new();
    private GroupingList(){}
    public void Serialize(ref Writer writer,G.Grouping<TKey,TElement> value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        writer.Write(value.Key,Resolver);
        var Formatter=Resolver.GetFormatter<TElement>();
        foreach(var item in value){
            writer.WriteValueSeparator();
            writer.Write(Formatter,item,Resolver);
        }
        writer.WriteEndArray();
    }
    public G.Grouping<TKey,TElement> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var Key=reader.Read<TKey>(Resolver);
        var Formatter=Resolver.GetFormatter<TElement>();
        var value=new G.Grouping<TKey,TElement>(Key);
        while(!reader.ReadIsEndArray()){
            reader.ReadIsValueSeparatorWithVerify();
            value.Add(reader.Read(Formatter,Resolver));
        }
        return value;
    }
}
