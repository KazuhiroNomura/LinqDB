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
    public void Serialize(ref Writer writer, G.IEnumerable<T> value, O Resolver)=>writer.Write(Formatter,value,Resolver);
    public G.IEnumerable<T> Deserialize(ref Reader reader, O Resolver)=>reader.Read(Formatter,Resolver)!;
}
