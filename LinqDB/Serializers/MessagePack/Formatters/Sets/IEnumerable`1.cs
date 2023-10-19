using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Sets;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G=LinqDB.Sets;
public class IEnumerable<T>:IMessagePackFormatter<G.IEnumerable<T>>{
    public static readonly IEnumerable<T> Instance=new();
    public void Serialize(ref Writer writer,G.IEnumerable<T>? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        //writer.WriteArrayHeader(2);
        //var type=value!.GetType();
        //writer.WriteType(type);

        //writer.Write(type,value,Resolver);
        var Count=value!.Count;
        writer.WriteArrayHeader(Count);
        var Formatter=Resolver.GetFormatter<T>()!;
        foreach(var item in value)
            Formatter.Serialize(ref writer,item,Resolver);

    }
    public G.IEnumerable<T> Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil())return null!;
        //var Count = reader.ReadArrayHeader();
        //Debug.Assert(Count==2);
        //var type = reader.ReadType();
        
        //var value=reader.Read(type,Resolver);
        //return (G.IEnumerable<T>)value;
        //if(reader.TryReadNil())return null!;
        var Count = reader.ReadArrayHeader();
        var Formatter=Resolver.GetFormatter<T>()!;
        var value=new G.Set<T>();
        for(long a=0;a<Count;a++)
            value.Add(Formatter.Deserialize(ref reader,Resolver));
        return value;
    }
}
