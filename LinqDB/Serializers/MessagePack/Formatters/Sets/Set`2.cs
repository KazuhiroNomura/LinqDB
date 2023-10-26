using System;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Sets;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G=LinqDB.Sets;
public class Set<TKey,TElement>:IMessagePackFormatter<G.Set<TKey,TElement>>
    where TElement:G.IKey<TKey>
    where TKey : struct, IEquatable<TKey>{
    public static readonly Set<TKey,TElement> Instance=new();//リフレクションで使われる
    public void Serialize(ref Writer writer,G.Set<TKey,TElement>? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(value!.Count);
        var Formatter = Resolver.GetFormatter<TElement>();
        foreach(var item in value)
            writer.Write(Formatter,item,Resolver);
    }
    
    
    
    
    
    
    
    public G.Set<TKey,TElement> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil())return null!;
        var Count = reader.ReadArrayHeader();
        var Formatter=Resolver.GetFormatter<TElement>();
        var value=new G.Set<TKey,TElement>();
        while(Count-->0)
            value.Add(reader.Read(Formatter,Resolver));
        return value;
    }
}
