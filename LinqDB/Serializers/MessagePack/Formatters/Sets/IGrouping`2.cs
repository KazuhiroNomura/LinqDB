using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Sets;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G=LinqDB.Sets;

public class IGrouping<TKey,TElement>:IMessagePackFormatter<G.IGrouping<TKey,TElement>>{
    public static readonly IGrouping<TKey,TElement> Instance=new();
    private IGrouping(){}
    public void Serialize(ref Writer writer,G.IGrouping<TKey,TElement>? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(2);
        var type=value!.GetType();
        writer.WriteType(type);
        writer.Write(type,value,Resolver);
    }
    
    
    public G.IGrouping<TKey,TElement> Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil())return null!;
        var Count=reader.ReadArrayHeader();
        Debug.Assert(Count==2);
        var type=reader.ReadType();
        var value=reader.Read(type,Resolver);
        return(G.IGrouping<TKey,TElement>)value;
    }
}
