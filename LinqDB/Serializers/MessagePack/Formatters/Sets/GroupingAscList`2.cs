using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Sets;
using Sets=LinqDB.Sets;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
//public class SetGroupingAscList<TKey,TElement>:IMessagePackFormatter<Sets.SetGroupingAscList<TKey,TElement>>{
//    private static void WriteNullable(ref Writer writer,Sets.SetGroupingAscList<TKey,TElement>? value,O Resolver) {
//        if(writer.TryWriteNil(value)) return;
//        writer.WriteArrayHeader(1+value!.Count);
//        writer.WriteType(value!.GetType());
//        var KeyFormatter=Resolver.Resolver.GetFormatter<TKey>()!;
//        var ElementFormatter=Resolver.Resolver.GetFormatter<TElement>()!;
//        foreach(var GroupingAscList in value){
//            writer.WriteMapHeader(2);
//            KeyFormatter.Serialize(ref writer,GroupingAscList.Key,Resolver);
//            writer.WriteArrayHeader(1+GroupingAscList.Count);
//            writer.WriteType(GroupingAscList.GetType());
//            foreach(var item in GroupingAscList) 
//                ElementFormatter.Serialize(ref writer,item,Resolver);
//        }
//    }
//    public void Serialize(ref Writer writer,Sets.SetGroupingAscList<TKey,TElement>? value,O Resolver)=>WriteNullable(ref writer,value,Resolver);
//    private static Sets.SetGroupingAscList<TKey,TElement>? ReadNullable(ref Reader reader,O Resolver){
//        if(reader.TryReadNil())return null;
//        var Count = reader.ReadArrayHeader();
//        var type=reader.ReadType();
//        var KeyFormatter=Resolver.Resolver.GetFormatter<TKey>()!;
//        var ElementFormatter=Resolver.Resolver.GetFormatter<TElement>()!;
//        var value=new Sets.SetGroupingAscList<TKey,TElement>();
//        for(var a=1;a<Count;a++){
//            var Key=KeyFormatter.Deserialize(ref reader,Resolver);
//            var Count1 = reader.ReadArrayHeader();
//            var GroupingAscList=new Sets.GroupingAscList<TKey,TElement>(Key);
//            for(var b=1;b<Count1;b++){
//                var Element=ElementFormatter.Deserialize(ref reader,Resolver);
//                GroupingAscList.Add(Element);
//            }
//            value.Add(GroupingAscList);
//        }
//        return value;
//    }
//    public Sets.SetGroupingAscList<TKey,TElement> Deserialize(ref Reader reader,O Resolver)=>ReadNullable(ref reader,Resolver)!;
//}

//public class SetGroupingSet<TKey,TElement>:IMessagePackFormatter<Sets.SetGroupingSet<TKey,TElement>>{
//    private static void WriteNullable(ref Writer writer,Sets.SetGroupingSet<TKey,TElement>? value,O Resolver) {
//        if(writer.TryWriteNil(value)) return;
//        writer.WriteArrayHeader(1+value!.Count);
//        writer.WriteType(value!.GetType());
//        var KeyFormatter=Resolver.Resolver.GetFormatter<TKey>()!;
//        var ElementFormatter=Resolver.Resolver.GetFormatter<TElement>()!;
//        foreach(var GroupingAscList in value){
//            writer.WriteMapHeader(2);
//            KeyFormatter.Serialize(ref writer,GroupingAscList.Key,Resolver);
//            writer.WriteArrayHeader(1+GroupingAscList.Count);
//            writer.WriteType(GroupingAscList.GetType());
//            foreach(var item in GroupingAscList) 
//                ElementFormatter.Serialize(ref writer,item,Resolver);
//        }
//    }
//    public void Serialize(ref Writer writer,Sets.SetGroupingSet<TKey,TElement>? value,O Resolver)=>WriteNullable(ref writer,value,Resolver);
//    private static Sets.SetGroupingSet<TKey,TElement>? ReadNullable(ref Reader reader,O Resolver){
//        if(reader.TryReadNil())return null;
//        var Count = reader.ReadArrayHeader();
//        var type=reader.ReadType();
//        var KeyFormatter=Resolver.Resolver.GetFormatter<TKey>()!;
//        var ElementFormatter=Resolver.Resolver.GetFormatter<TElement>()!;
//        var value=new Sets.SetGroupingSet<TKey,TElement>();
//        for(var a=1;a<Count;a++){
//            var Key=KeyFormatter.Deserialize(ref reader,Resolver);
//            var Count1 = reader.ReadArrayHeader();
//            var GroupingSet=new Sets.GroupingSet<TKey,TElement>(Key);
//            for(var b=1;b<Count1;b++){
//                var Element=ElementFormatter.Deserialize(ref reader,Resolver);
//                GroupingSet.Add(Element);
//            }
//            value.Add(GroupingSet);
//        }
//        return value;
//    }
//    public Sets.SetGroupingSet<TKey,TElement> Deserialize(ref Reader reader,O Resolver)=>ReadNullable(ref reader,Resolver)!;
//}
public class GroupingAscList<TKey,TElement>:IMessagePackFormatter<Sets.GroupingAscList<TKey,TElement>>{
    public static readonly GroupingAscList<TKey,TElement> Instance=new();
    private static void WriteNullable(ref Writer writer,Sets.GroupingAscList<TKey,TElement>? value,O Resolver) {
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(2+value!.Count);
        writer.WriteType(value.GetType());
        Resolver.Resolver.GetFormatter<TKey>()!.Serialize(ref writer,value.Key,Resolver);
        var ElementFormatter=Resolver.Resolver.GetFormatter<TElement>()!;
        foreach(var item in value) 
            ElementFormatter.Serialize(ref writer,item,Resolver);
    }
    private static Sets.GroupingAscList<TKey,TElement>? ReadNullable(ref Reader reader,O Resolver){
        if(reader.TryReadNil())return null;
        var Count = reader.ReadArrayHeader();
        var type=reader.ReadType();
        var Key=Resolver.Resolver.GetFormatter<TKey>()!.Deserialize(ref reader,Resolver);
        var ElementFormatter=Resolver.Resolver.GetFormatter<TElement>()!;
        var value=new Sets.GroupingAscList<TKey,TElement>(Key);
        for(var a=2;a<Count;a++){
            var Element=ElementFormatter.Deserialize(ref reader,Resolver);
            value.Add(Element);
        }
        return value;
    }
    public void Serialize(ref Writer writer,Sets.GroupingAscList<TKey,TElement>? value,O Resolver)=>WriteNullable(ref writer,value,Resolver);
    public Sets.GroupingAscList<TKey,TElement> Deserialize(ref Reader reader,O Resolver)=>ReadNullable(ref reader,Resolver)!;
}
