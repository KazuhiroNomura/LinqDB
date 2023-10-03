using System;
using LinqDB.Sets;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters.Sets;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using Sets=LinqDB.Sets;
public class SetGroupingSet<TKey,TElement>:IJsonFormatter<Sets.SetGroupingSet<TKey,TElement>>{
    public new static readonly SetGroupingSet<TKey,TElement>Instance=new();
    public void Serialize(ref Writer writer,Sets.SetGroupingSet<TKey,TElement>? value,O Resolver){
        writer.WriteBeginArray();
        
        var Formatter=Formatters.Sets.GroupingSet<TKey,TElement>.Instance;
        //Resolver.GetFormatter<Sets.GroupingSet<TKey,TElement>>()!;
        var first=true;
        foreach(var item in value!){
            if(first) first=false;
            else writer.WriteValueSeparator();
            Formatter.Serialize(ref writer,item,Resolver);
        }
        writer.WriteEndArray();
    }
    public Sets.SetGroupingSet<TKey,TElement> Deserialize(ref Reader reader,O Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var value=new Sets.SetGroupingSet<TKey,TElement>();
        var Formatter=Formatters.Sets.GroupingSet<TKey,TElement>.Instance;
        //var Formatter=Resolver.GetFormatter<Sets.GroupingSet<TKey,TElement>>()!;
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
