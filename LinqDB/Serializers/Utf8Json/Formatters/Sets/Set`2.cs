using System;
using LinqDB.Sets;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters.Sets;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G=LinqDB.Sets;
public class Set<TKey,TElement>:IJsonFormatter<G.Set<TKey,TElement>>
    where TElement:IKey<TKey>   
    where TKey : struct, IEquatable<TKey>{
    public new static readonly Set<TKey,TElement> Instance=new();
    public void Serialize(ref Writer writer,G.Set<TKey,TElement>? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        var Formatter = Resolver.GetFormatter<TElement>();
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
    public G.Set<TKey,TElement> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        G.Set<TKey,TElement>value;
        reader.ReadIsBeginArrayWithVerify();
        var Formatter = Resolver.GetFormatter<TElement>();
        value=new G.Set<TKey,TElement>();
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
