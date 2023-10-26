using System.Diagnostics.CodeAnalysis;
using MessagePack;
using MessagePack.Formatters;
using System.Linq;
namespace LinqDB.Serializers.MessagePack.Formatters.Enumerables;
using O = MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T=System.Collections.IEnumerable;
public class IEnumerable:IMessagePackFormatter<T>{
    public static readonly IEnumerable Instance=new();
    private IEnumerable(){}
    [SuppressMessage("ReSharper","PossibleMultipleEnumeration")]
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        var List=value.Cast<object>().ToList();
        writer.WriteArrayHeader(List.Count);
        var Formatter=Resolver.GetFormatter<object>();
        foreach(var item in value)
            writer.Write(Formatter,item,Resolver);
    }
    
    
    
    
    
    
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var Count = reader.ReadArrayHeader();
        var Formatter=Resolver.GetFormatter<object>();
        var value=new LinqDB.Sets.Set<object>();
        while(Count-->0)
            value.Add(reader.Read(Formatter,Resolver));
        return value;
    }
}