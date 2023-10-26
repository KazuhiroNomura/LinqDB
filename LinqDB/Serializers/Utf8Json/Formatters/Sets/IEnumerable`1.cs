
using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters.Sets;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G=LinqDB.Sets;
public class IEnumerable<T>:IJsonFormatter<G.IEnumerable<T>>  {
    internal static readonly IEnumerable<T> Instance=new();
    public void Serialize(ref Writer writer,G.IEnumerable<T> value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        var Formatter = Resolver.GetFormatter<T>();
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
    public G.IEnumerable<T> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=new G.Set<T>();
        var Formatter = Resolver.GetFormatter<T>();
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