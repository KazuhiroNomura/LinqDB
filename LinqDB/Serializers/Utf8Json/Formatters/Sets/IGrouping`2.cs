using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters.Sets;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G=LinqDB.Sets;
public class IGrouping<TKey,TElement>:IJsonFormatter<G.IGrouping<TKey,TElement>>{
    public static readonly IGrouping<TKey,TElement> Instance=new();//リフレクションで使われる
    private IGrouping(){}
    public void Serialize(ref Writer writer,G.IGrouping<TKey,TElement> value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        Resolver.GetFormatter<TKey>().Serialize(ref writer,value.Key,Resolver);
        writer.WriteValueSeparator();
        var Formatter = Resolver.GetFormatter<TElement>();
        using var Enumerator=value.GetEnumerator();
        if(Enumerator.MoveNext()){
            writer.Write(Formatter,Enumerator.Current,Resolver);
            while(Enumerator.MoveNext()){
	            writer.WriteValueSeparator();
	            writer.Write(Formatter,Enumerator.Current,Resolver);
            }
        }
        writer.WriteEndArray();
    }
    public G.IGrouping<TKey,TElement> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var Key= Resolver.GetFormatter<TKey>().Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Formatter = Resolver.GetFormatter<TElement>();
        var value=new G.Grouping<TKey,TElement>(Key);
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
