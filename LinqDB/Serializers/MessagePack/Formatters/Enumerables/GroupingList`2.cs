﻿using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Enumerables;
using O = MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G = LinqDB.Enumerables;
public class GroupingList<TKey,TElement>:IMessagePackFormatter<G.Grouping<TKey,TElement>>{
    public static readonly GroupingList<TKey,TElement> Instance = new();
    private GroupingList(){}
    public void Serialize(ref Writer writer,G.Grouping<TKey,TElement>? value,O Resolver){
        if (writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(1+value!.Count);
        writer.Write(value.Key!,Resolver);
        var Formatter = Resolver.GetFormatter<TElement>();
        foreach (var item in value){
            
            writer.Write(Formatter,item,Resolver);
        }
        
    }
    public G.Grouping<TKey,TElement> Deserialize(ref Reader reader,O Resolver){
        if (reader.TryReadNil()) return null!;
        var Count = reader.ReadArrayHeader();
        var Key=reader.Read<TKey>(Resolver);// Resolver.GetFormatter<TKey>()!.Deserialize(ref reader, Resolver);
        var Formatter = Resolver.GetFormatter<TElement>();
        var value = new G.Grouping<TKey,TElement>(Key);
        while(Count-->1)
            value.Add(reader.Read(Formatter,Resolver));


        return value;
    }
}
