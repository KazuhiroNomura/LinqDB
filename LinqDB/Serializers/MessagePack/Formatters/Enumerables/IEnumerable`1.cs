using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Enumerables;
using O = MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G=System.Collections.Generic;
public class IEnumerable<T>:IMessagePackFormatter<G.IEnumerable<T>>{
    internal static readonly IEnumerable<T> Instance=new();
    private static readonly InterfaceEnumerableFormatter<T> Formatter=new();
    private IEnumerable(){}
    public void Serialize(ref Writer writer,G.IEnumerable<T>? value,O Resolver)=>writer.Write(Formatter,value,Resolver);
    public G.IEnumerable<T> Deserialize(ref Reader reader,O Resolver)=>reader.Read(Formatter,Resolver)!;
}
