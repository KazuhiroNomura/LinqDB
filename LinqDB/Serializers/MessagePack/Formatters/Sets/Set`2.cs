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
        var type=value!.GetType();
        if(typeof(G.Set<TKey,TElement>)!=type){
            writer.WriteArrayHeader(2);
            writer.WriteType(type);
            writer.Write(type,value,Resolver);
        }else{
            writer.WriteArrayHeader(1+value.Count);
            writer.WriteType(type);
            var Formatter = Resolver.GetFormatter<TElement>();
            foreach(var item in value)
                writer.Write(Formatter,item,Resolver);
        }
    }
    public G.Set<TKey,TElement> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil())return null!;
        var Count = reader.ReadArrayHeader();
        var type=reader.ReadType();
        if(typeof(G.Set<TKey,TElement>)!=type){
            return(G.Set<TKey,TElement>)reader.Read(type,Resolver);
        }else{
            var Formatter=Resolver.GetFormatter<TElement>()!;
            var value=new G.Set<TKey,TElement>();
            for(long a=1;a<Count;a++)
                value.Add(reader.Read(Formatter,Resolver));
            return value;

        }
    }
}
