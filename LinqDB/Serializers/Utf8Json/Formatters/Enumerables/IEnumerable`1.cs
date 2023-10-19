using Utf8Json;
using Utf8Json.Formatters;
namespace LinqDB.Serializers.Utf8Json.Formatters.Enumerables;
using O = IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G = System.Collections.Generic;
public class IEnumerable<T>:IJsonFormatter<G.IEnumerable<T>>{
    internal static readonly IEnumerable<T> Instance = new();
    private static readonly InterfaceEnumerableFormatter<T> Formatter=new();
    private IEnumerable(){}
    public void Serialize(ref Writer writer, G.IEnumerable<T> value, O Resolver){
        writer.Write(Formatter,value,Resolver);

        //if(writer.TryWriteNil(value)) return;
        //writer.WriteBeginArray();
        //var type = value.GetType();
        //writer.WriteType(type);
        //writer.WriteValueSeparator();
        //writer.Write(type,value,Resolver);
        //writer.WriteEndArray();

        //writer.WriteBeginArray();
        //var Formatter = Resolver.GetFormatter<T>();
        //var first=true;
        //foreach(var item in value!){
        //    if(first) first=false;
        //    else writer.WriteValueSeparator();
        //    Formatter.Serialize(ref writer,item,Resolver);
        //}
        //writer.WriteEndArray();
    }
    public G.IEnumerable<T> Deserialize(ref Reader reader, O Resolver){
        return reader.Read(Formatter,Resolver)!;

        //if(reader.TryReadNil()) return null!;
        //reader.ReadIsBeginArrayWithVerify();

        //var type = reader.ReadType();
        //reader.ReadIsValueSeparatorWithVerify();
        //var value = reader.Read(type,Resolver);
        //reader.ReadIsEndArrayWithVerify();
        //return (G.IEnumerable<T>)value;

        //reader.ReadIsBeginArrayWithVerify();
        //var value=new G.Set<T>();
        //var Formatter = Resolver.GetFormatter<T>();
        //var first=true;
        //while(!reader.ReadIsEndArray()) {
        //    if(first) first=false;
        //    else reader.ReadIsValueSeparatorWithVerify();
        //    var item = Formatter.Deserialize(ref reader,Resolver);
        //    value.Add(item);
        //}
        //return value;
    }
}
