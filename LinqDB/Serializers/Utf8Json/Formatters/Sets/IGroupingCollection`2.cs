using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters.Sets;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using Sets=LinqDB.Sets;
public class IGroupingCollection<TKey,TElement>:IJsonFormatter<Sets.IGroupingCollection<TKey,TElement>>{
#pragma warning disable CA1823// 使用されていないプライベート フィールドを使用しません
    //public static readonly GroupingSet<TKey,TElement> Instance=new();//リフレクションで使われる
#pragma warning restore CA1823// 使用されていないプライベート フィールドを使用しません
    public void Serialize(ref Writer writer,Sets.IGroupingCollection<TKey,TElement> value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        Resolver.GetFormatter<TKey>().Serialize(ref writer,value!.Key,Resolver);
        writer.WriteValueSeparator();
        var Formatter = Resolver.GetFormatter<TElement>();
        var first=true;
        foreach(var item in value!){
            if(first) first=false;
            else writer.WriteValueSeparator();
            Formatter.Serialize(ref writer,item,Resolver);
        }
        writer.WriteEndArray();
    }
    public Sets.IGroupingCollection<TKey,TElement> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var Key= Resolver.GetFormatter<TKey>().Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var value=new Sets.GroupingSet<TKey,TElement>(Key);
        var Formatter = Resolver.GetFormatter<TElement>();
        var first=true;
        while(!reader.ReadIsEndArray()) {
            if(first) first=false;
            else reader.ReadIsValueSeparatorWithVerify();
            var item = Formatter.Deserialize(ref reader,Resolver);
            value.Add(item);
        }
        return value;
    }
}