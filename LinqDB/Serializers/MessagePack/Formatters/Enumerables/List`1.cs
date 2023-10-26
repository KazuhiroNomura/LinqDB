using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Enumerables;
using O = MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G = LinqDB.Enumerables;
public class List<TElement>:IMessagePackFormatter<G.List<TElement>>{
    public static readonly List<TElement> Instance = new();
    private List(){}
    public void Serialize(ref Writer writer,G.List<TElement>? value,O Resolver){
        if (writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(value!.Count);
        var Formatter = Resolver.GetFormatter<TElement>();
        foreach (var item in value)
            writer.Write(Formatter,item,Resolver);
    }







    public G.List<TElement> Deserialize(ref Reader reader,O Resolver){
        if (reader.TryReadNil()) return null!;
        var Count = reader.ReadArrayHeader();
        var Formatter = Resolver.GetFormatter<TElement>();
        var value = new G.List<TElement>();
        while(Count-->0)
            value.Add(reader.Read(Formatter,Resolver));
        return value;
    }
}
