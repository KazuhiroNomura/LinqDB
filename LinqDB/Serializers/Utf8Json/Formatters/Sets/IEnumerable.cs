
using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters.Sets;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G=LinqDB.Sets;
public class IEnumerable:IJsonFormatter<G.IEnumerable>  {
    public static readonly IEnumerable Instance=new();
    private IEnumerable(){}

    public void Serialize(ref Writer writer,G.IEnumerable value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        var Formatter=Resolver.GetFormatter<object>();
        var Enumerator=value.GetEnumerator();
        if(Enumerator.MoveNext()){
            writer.Write(Formatter,Enumerator.Current,Resolver);
            while(Enumerator.MoveNext()){
	            writer.WriteValueSeparator();
	            writer.Write(Formatter,Enumerator.Current,Resolver);
            }
        }
        writer.WriteEndArray();
    }
    public G.IEnumerable Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var Formatter=Resolver.GetFormatter<object>();
        var value=new G.Set<object>();
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