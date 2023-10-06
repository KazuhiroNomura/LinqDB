using System;
using LinqDB.Databases;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Sets;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G=LinqDB.Sets;
public class Set<TKey,TElement,TContainer>:IMessagePackFormatter<G.Set<TKey,TElement,TContainer>>
    where TElement: G.Entity<TKey,TContainer>
    where TKey : struct, IEquatable<TKey>
    where TContainer : Container{
    public static readonly Set<TKey,TElement,TContainer>Instance=new();
    public void Serialize(ref Writer writer,G.Set<TKey,TElement,TContainer>? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(value!.Count);
        var Formatter=Resolver.Resolver.GetFormatter<TElement>()!;
        foreach(var item in value)
            writer.Write(Formatter,item,Resolver);
    }
    
    
    
    
    
    public G.Set<TKey,TElement,TContainer> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil())return null!;
        var Count = reader.ReadArrayHeader();
        var Formatter=Resolver.Resolver.GetFormatter<TElement>()!;
        var value=new G.Set<TKey,TElement,TContainer>(null!);
        for(var a=0;a<Count;a++)
            value.Add(reader.Read(Formatter,Resolver));
        return value;
    }
}
