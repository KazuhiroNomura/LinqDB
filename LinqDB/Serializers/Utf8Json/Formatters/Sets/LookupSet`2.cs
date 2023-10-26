using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters.Sets;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G=LinqDB.Sets;
//public class LookupSet<TKey,TElement>:IJsonFormatter<G.LookupSet<TElement,TKey>>{
//    public static readonly LookupSet<TKey,TElement> Instance=new();//リフレクションで使われる
//    public void Serialize(ref Writer writer,G.LookupSet<TElement,TKey> value,O Resolver){
//        if(writer.TryWriteNil(value)) return;
//        writer.WriteBeginArray();
//        //var Formatter = Resolver.GetFormatter<TElement>();
//        var Formatter=Set<TElement>.Instance;
//        var first=true;
//        foreach(var item in value){
//            if(first) first=false;
//            else writer.WriteValueSeparator();
//            writer.Write(Formatter,item,Resolver);;
//        }
//        writer.WriteEndArray();
//    }
//    public G.LookupSet<TElement,TKey> Deserialize(ref Reader reader,O Resolver){
//        if(reader.TryReadNil()) return null!;
//        reader.ReadIsBeginArrayWithVerify();
//        var Key= Resolver.GetFormatter<TKey>().Deserialize(ref reader,Resolver);
//        var value=new G.LookupSet<TElement,TKey>(Key);
//        var Formatter = Resolver.GetFormatter<TElement>();
//        while(!reader.ReadIsEndArray()) {
//            reader.ReadIsValueSeparatorWithVerify();
//            var item = reader.Read(Formatter,Resolver);
//            value.Add(item);
//        }
//        return value;
//    }
//}
