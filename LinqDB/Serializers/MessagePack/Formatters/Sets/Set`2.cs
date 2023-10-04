using System;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Sets;
using Sets=LinqDB.Sets;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
public class Set<TKey,TElement>:IMessagePackFormatter<Sets.Set<TKey,TElement>>
    where TElement:Sets.IKey<TKey>
    where TKey : struct, IEquatable<TKey>{
    //public static readonly Set<TKey,TElement> Instance=new();//リフレクションで使われる
    private static void WriteNullable(ref Writer writer,Sets.Set<TKey,TElement>? value,O Resolver) {
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(1+value!.Count);
        writer.WriteType(value!.GetType());
        var Formatter=Resolver.Resolver.GetFormatter<TElement>()!;
        foreach(var item in value){
            Formatter.Serialize(ref writer,item,Resolver);
        }
    }
    private static Sets.Set<TKey,TElement>? ReadNullable(ref Reader reader,O Resolver){
        if(reader.TryReadNil())return null;
        var Count = reader.ReadArrayHeader();
        var type=reader.ReadType();
        var Formatter=Resolver.Resolver.GetFormatter<TElement>()!;
        var value=new Sets.Set<TKey,TElement>();
        for(long a=1;a<Count;a++){
            var item=Formatter.Deserialize(ref reader,Resolver);
            value.Add(item);
        }
        return value;
    }
    public void Serialize(ref Writer writer,Sets.Set<TKey,TElement>? value,O Resolver)=>WriteNullable(ref writer,value,Resolver);
    public Sets.Set<TKey,TElement> Deserialize(ref Reader reader,O Resolver)=>ReadNullable(ref reader,Resolver)!;
}
