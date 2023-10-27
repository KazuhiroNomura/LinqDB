
using Utf8Json;
using Utf8Json.Formatters;
using System.Linq;
namespace LinqDB.Serializers.Utf8Json.Formatters.Enumerables;
using O = IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G = System.Collections.Generic;
public class IEnumerable<T>:IJsonFormatter<G.IEnumerable<T>>{
    internal static readonly IEnumerable<T> Instance = new();
    private IEnumerable(){}
    
    public void Serialize(ref Writer writer, G.IEnumerable<T> value, O Resolver){
        if(writer.TryWriteNil(value)) return;
        var type=value!.GetType();
        writer.WriteBeginArray();
        writer.WriteType(type);
        writer.WriteValueSeparator();
        writer.Write(type,value,Resolver);
        /*
        var Formatter=Resolver.GetFormatter<T>();
        using var Enumerator=value.GetEnumerator();
        if(Enumerator.MoveNext()){
            writer.Write(Formatter,Enumerator.Current,Resolver);
            while(Enumerator.MoveNext()){
	            writer.WriteValueSeparator();
                writer.Write(Formatter,Enumerator.Current,Resolver);
            }
        }
        */
        writer.WriteEndArray();
    }
    public G.IEnumerable<T> Deserialize(ref Reader reader, O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var value=(G.IEnumerable<T>?)reader.Read(type,Resolver);
        reader.ReadIsEndArrayWithVerify();
        /*
        var Formatter = Resolver.GetFormatter<T>();
        var value=new G.List<T>();
        // ReSharper disable once InvertIf
        if(!reader.ReadIsEndArray()) {
            value.Add(reader.Read(Formatter,Resolver));
	        while(!reader.ReadIsEndArray()) {
	            reader.ReadIsValueSeparatorWithVerify();
                value.Add(reader.Read(Formatter,Resolver));
	        }
        }
        */
        return value!;
    }
}
