using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters.Enumerables;
using O = IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G = LinqDB.Sets;
public class SetGroupingList<TKey,TElement>:IJsonFormatter<G.SetGroupingList<TKey,TElement>>{
    public new static readonly SetGroupingList<TKey,TElement> Instance=new();
    public void Serialize(ref Writer writer,G.SetGroupingList<TKey,TElement>? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        var Formatter=GroupingList<TKey,TElement>.Instance;
        var first=true;
        foreach(var item in value!){
            if(first)first=false;
            else writer.WriteValueSeparator();
            writer.Write(Formatter,item,Resolver);
        }
        writer.WriteEndArray();
    }
    public G.SetGroupingList<TKey,TElement> Deserialize(ref Reader reader,O Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var value=new G.SetGroupingList<TKey,TElement>();
        var Formatter=GroupingList<TKey,TElement>.Instance;
        //var Formatter=Resolver.GetFormatter<Sets.GroupingSet<TKey,TElement>>()!;
        var first=true;
        while(!reader.ReadIsEndArray()){
            if(first)first=false;
            else reader.ReadIsValueSeparatorWithVerify();
            var item=Formatter.Deserialize(ref reader,Resolver);
            value.Add(item);
        }
        return value;
    }
}