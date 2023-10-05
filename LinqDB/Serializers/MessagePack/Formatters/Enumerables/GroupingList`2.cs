﻿using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Enumerables;
using O = MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G = LinqDB.Enumerables;
public class GroupingList<TKey, TElement> : IMessagePackFormatter<G.GroupingList<TKey, TElement>>{
    public static readonly GroupingList<TKey, TElement> Instance = new();
    private GroupingList(){}
    public void Serialize(ref Writer writer,G.GroupingList<TKey,TElement>? value,O Resolver){
        if (writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(1+value!.Count);
        writer.WriteValue(value!.Key!,Resolver);
        //Resolver.Resolver.GetFormatter<TKey>()!.Serialize(ref writer, value.Key, Resolver);

        var Formatter = Resolver.Resolver.GetFormatter<TElement>()!;
        foreach (var item in value){
            
            Formatter.Serialize(ref writer, item, Resolver);
        }
        
    }
    public G.GroupingList<TKey,TElement> Deserialize(ref Reader reader,O Resolver){
        if (reader.TryReadNil()) return null!;
        var Count = reader.ReadArrayHeader();
        var Key=reader.ReadValue<TKey>(Resolver);// Resolver.Resolver.GetFormatter<TKey>()!.Deserialize(ref reader, Resolver);
        var ElementFormatter = Resolver.Resolver.GetFormatter<TElement>()!;
        var value = new G.GroupingList<TKey, TElement>(Key);
        for (var a = 2; a<Count; a++){
            var item=ElementFormatter.Deserialize(ref reader, Resolver);
            value.Add(item);
        }
        return value;
    }
}
