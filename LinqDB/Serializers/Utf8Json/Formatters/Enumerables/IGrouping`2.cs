
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters.Enumerables;
using O = IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G = System.Linq;
public class IGrouping<TKey,TElement>:IJsonFormatter<G.IGrouping<TKey,TElement>>{
    public static readonly IGrouping<TKey,TElement> Instance = new();//リフレクションで使われる
    private IGrouping(){}
    public void Serialize(ref Writer writer,G.IGrouping<TKey,TElement> value, O Resolver){
        if (writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        writer.Write(value.Key,Resolver);
        var Formatter=Resolver.GetFormatter<TElement>();
        foreach(var item in value){
            writer.WriteValueSeparator();
            writer.Write(Formatter,item,Resolver);;
        }
        writer.WriteEndArray();
    }
    public G.IGrouping<TKey,TElement> Deserialize(ref Reader reader, O Resolver)    {
        if (reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var Key=reader.Read<TKey>(Resolver);
        var Formatter=Resolver.GetFormatter<TElement>();
        var value=new LinqDB.Enumerables.GroupingList<TKey,TElement>(Key);
        while(!reader.ReadIsEndArray()){
            reader.ReadIsValueSeparatorWithVerify();
            value.Add(reader.Read(Formatter,Resolver));
        }
        return value;
    }
}
