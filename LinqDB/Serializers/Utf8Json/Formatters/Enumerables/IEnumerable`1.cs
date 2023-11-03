
using Utf8Json;



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
        writer.WriteBeginArray();
        var type=value.GetType();
        var Formatter=Resolver.GetFormatterDynamic(type);
        if(Formatter is not null){
            writer.WriteType(type);
            writer.WriteValueSeparator();
            writer.Write(Formatter,value,Resolver);
        }else{
            writer.WriteType(typeof(G.IEnumerable<T>));
            var FormatterT=Resolver.GetFormatter<T>();
            foreach(var item in value){
                writer.WriteValueSeparator();
                writer.Write(FormatterT,item,Resolver);
            }
        }
        writer.WriteEndArray();
    }
    public G.IEnumerable<T> Deserialize(ref Reader reader, O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var type=reader.ReadType();
        if(type!=typeof(G.IEnumerable<T>)){
            reader.ReadIsValueSeparatorWithVerify();
            var value=(G.IEnumerable<T>)reader.Read(type,Resolver);
            reader.ReadIsEndArrayWithVerify();
            return value;
        }else{
            var FormatterT = Resolver.GetFormatter<T>();
            var value=new G.List<T>();
            // ReSharper disable once InvertIf
    	    while(!reader.ReadIsEndArray()) {
    	        reader.ReadIsValueSeparatorWithVerify();
                value.Add(reader.Read(FormatterT,Resolver));
    	    }
            return value!;
        }
    }
}
