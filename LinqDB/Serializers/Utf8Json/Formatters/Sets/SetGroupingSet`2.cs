using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters.Sets;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G=LinqDB.Sets;
public class SetGroupingSet<TKey,TElement>:IJsonFormatter<G.SetGroupingSet<TKey,TElement>>{
    internal static readonly SetGroupingSet<TKey,TElement>Instance=new();
    public void Serialize(ref Writer writer,G.SetGroupingSet<TKey,TElement>? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        var Formatter=GroupingSet<TKey,TElement>.Instance;
        using var Enumerator=value!.GetEnumerator();
        if(Enumerator.MoveNext()){
            writer.Write(Formatter,Enumerator.Current,Resolver);
            while(Enumerator.MoveNext()){
	            writer.WriteValueSeparator();
                writer.Write(Formatter,Enumerator.Current,Resolver);
            }
        }
        writer.WriteEndArray();
    }
    public G.SetGroupingSet<TKey,TElement> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var Formatter=GroupingSet<TKey,TElement>.Instance;
        var value=new G.SetGroupingSet<TKey,TElement>();
        // ReSharper disable once InvertIf
        if(!reader.ReadIsEndArray()) {
            value.Add(reader.Read(Formatter,Resolver));
	        while(!reader.ReadIsEndArray()) {
	            reader.ReadIsValueSeparatorWithVerify();
	            value.Add(reader.Read(Formatter,Resolver));
	        }
        }
        return value;
    }
}
