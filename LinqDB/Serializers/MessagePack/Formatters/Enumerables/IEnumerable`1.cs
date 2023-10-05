using System.Diagnostics;
using System.Xml.Linq;

using LinqDB.Serializers.MessagePack.Formatters.Sets;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Enumerables;
using O = MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G=System.Collections.Generic;
public class IEnumerable<T>:IMessagePackFormatter<G.IEnumerable<T>>{
    public static readonly IEnumerable<T> Instance=new();

    //public void Serialize(ref Writer writer,G.IEnumerable<T>? value,O Resolver){
    //    if(writer.TryWriteNil(value)) return;
    //    writer.WriteArrayHeader(2);
    //    var type=value!.GetType();
    //    writer.WriteType(type);
    //    writer.Write(type,value,Resolver);
    //}


    //public G.IEnumerable<T> Deserialize(ref Reader reader,O Resolver){
    //    if(reader.TryReadNil()) return null!;
    //    var Count=reader.ReadArrayHeader();
    //    Debug.Assert(Count==2);
    //    var type=reader.ReadType();
    //    var value=reader.Read(type,Resolver);
    //    return(G.IEnumerable<T>)value;
    //}
    private static readonly global::MessagePack.Formatters.InterfaceEnumerableFormatter<T> Formatter=new();
    public void Serialize(ref Writer writer,G.IEnumerable<T>? value,O Resolver){
        //if(writer.TryWriteNil(value)) return;
        //writer.WriteArrayHeader(2);
        writer.Write(Formatter,value,Resolver);
    }


    public G.IEnumerable<T> Deserialize(ref Reader reader,O Resolver){
        //if(reader.TryReadNil()) return null!;
        //var Count=reader.ReadArrayHeader();
        //Debug.Assert(Count==2);
        return reader.Read(Formatter,Resolver)!;
    }
}
