using System.Diagnostics.CodeAnalysis;
using MessagePack;
using MessagePack.Formatters;
using System.Linq;
namespace LinqDB.Serializers.MessagePack.Formatters.Enumerables;
using O = MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G=System.Collections.Generic;
public class IEnumerable<T>:IMessagePackFormatter<G.IEnumerable<T>>{
    internal static readonly IEnumerable<T> Instance=new();
    private IEnumerable(){}
    [SuppressMessage("ReSharper","PossibleMultipleEnumeration")]
    public void Serialize(ref Writer writer,G.IEnumerable<T>? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        var Count=value.Count();
        writer.WriteArrayHeader(Count);
        var Formatter=Resolver.GetFormatter<T>();
        foreach(var item in value)
            writer.Write(Formatter,item,Resolver);
    }
    
    




    public G.IEnumerable<T> Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil())return null!;
        var Count = reader.ReadArrayHeader();
        var Formatter=Resolver.GetFormatter<T>();
        var value=new G.List<T>();
        while(Count-->0)
            value.Add(reader.Read(Formatter,Resolver));
        return value;
    }
}
