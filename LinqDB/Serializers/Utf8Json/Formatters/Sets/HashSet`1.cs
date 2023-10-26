using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters.Sets;
using O = IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G = LinqDB.Sets;
public class HashSet<TElement>:IJsonFormatter<G.HashSet<TElement>>{
    public static readonly HashSet<TElement> Instance=new();
    private HashSet(){}
    public void Serialize(ref Writer writer,G.HashSet<TElement> value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
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
    public G.HashSet<TElement> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var Formatter=Resolver.GetFormatter<TElement>();
        var value=new G.HashSet<TElement>();
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
