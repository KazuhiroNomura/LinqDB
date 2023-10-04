using System;
using MessagePack;
using MessagePack.Formatters;


namespace LinqDB.Serializers.MessagePack.Formatters.Enumerables;
using Sets = LinqDB.Sets;
using O = MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;

public class SetGroupingList<TKey,TElement>:IMessagePackFormatter<Sets.SetGroupingList<TKey,TElement>>{
    public new static readonly SetGroupingList<TKey,TElement>Instance=new();
    private SetGroupingList(){}
    public void Serialize(ref Writer writer,Sets.SetGroupingList<TKey,TElement>? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(value!.Count);
        var Formatter=Resolver.Resolver.GetFormatter<LinqDB.Enumerables.GroupingList<TKey,TElement>>()!;
        foreach(var item in value)
            Formatter.Serialize(ref writer,item,Resolver);
    }
    public Sets.SetGroupingList<TKey,TElement> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil())return null!;
        var Count = reader.ReadArrayHeader();
        var Formatter=Resolver.Resolver.GetFormatter<LinqDB.Enumerables.GroupingList<TKey,TElement>>()!;
        var value = new Sets.SetGroupingList<TKey,TElement>();
        for(long a = 0;a<Count;a++)
            value.Add(Formatter.Deserialize(ref reader,Resolver)!);
        return value;
    }
}
