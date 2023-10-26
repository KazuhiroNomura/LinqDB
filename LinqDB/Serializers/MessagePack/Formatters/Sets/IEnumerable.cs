using System.Diagnostics.CodeAnalysis;
using LinqDB.Sets;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Sets;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G=LinqDB.Sets;
public class IEnumerable:IMessagePackFormatter<G.IEnumerable>{
    public static readonly IEnumerable Instance=new();
    private IEnumerable(){}
    [SuppressMessage("ReSharper","PossibleMultipleEnumeration")]
    public void Serialize(ref Writer writer,G.IEnumerable? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        var List=value.Cast<object>();
        writer.WriteArrayHeader(List.Count);
        var Formatter=Resolver.GetFormatter<object>();
        foreach(var item in value)
            writer.Write(Formatter,item,Resolver);;
    }






    public G.IEnumerable Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil()) return null!;
        var Count = reader.ReadArrayHeader();
        var Formatter=Resolver.GetFormatter<object>();
        var value=new G.Set<object>();
        while(Count-->0)
            value.Add(reader.Read(Formatter,Resolver));
        return value;
    }
}
