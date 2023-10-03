using System;
using LinqDB.Databases;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Sets;
using Sets=LinqDB.Sets;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
public class Set<TKey,TElement,TContainer>:IMessagePackFormatter<Sets.Set<TKey,TElement,TContainer>>
    where TElement: Sets.Entity<TKey,TContainer>
    where TKey : struct, IEquatable<TKey>
    where TContainer : Container{
    //public static readonly Set<TKey,TElement,TContainer> Instance=new();//リフレクションで使われる
    private static void WriteNullable(ref Writer writer,Sets.Set<TKey,TElement,TContainer>? value,O Resolver) {
        if(writer.TryWriteNil(value)) return;
        var Count=value!.Count;
        writer.WriteArrayHeader(1+Count);
        writer.WriteType(value!.GetType());
        var Formatter=Resolver.Resolver.GetFormatter<TElement>()!;
        foreach(var item in value){
            Formatter.Serialize(ref writer,item,Resolver);
        }
    }
    private static Sets.Set<TKey,TElement,TContainer>? ReadNullable(ref Reader reader,O Resolver){
        if(reader.TryReadNil())return null;
        var Count = reader.ReadArrayHeader();
        var type=reader.ReadType();
        var Formatter=Resolver.Resolver.GetFormatter<TElement>()!;
        var value=new Sets.Set<TKey,TElement,TContainer>(null!);
        for(var a=1;a<Count;a++){
            var item=Formatter.Deserialize(ref reader,Resolver);
            value.Add(item);
        }
        return value;
    }
    public void Serialize(ref Writer writer,Sets.Set<TKey,TElement,TContainer>? value,O Resolver)=>WriteNullable(ref writer,value,Resolver);
    public Sets.Set<TKey,TElement,TContainer> Deserialize(ref Reader reader,O Resolver)=>ReadNullable(ref reader,Resolver)!;
}
