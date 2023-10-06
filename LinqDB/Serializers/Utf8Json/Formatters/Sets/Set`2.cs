using System;
using LinqDB.Sets;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters.Sets;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G=LinqDB.Sets;
public class Set<TKey,TElement>:IJsonFormatter<G.Set<TKey,TElement>> where TElement:IKey<TKey>
    where TKey : struct, IEquatable<TKey>{
    public new static readonly Set<TKey,TElement> Instance=new();
    public void Serialize(ref Writer writer,G.Set<TKey,TElement>? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        var type=value!.GetType();
        writer.WriteType(type);
        if(typeof(G.Set<TKey,TElement>)!=type){
            writer.WriteValueSeparator();
            writer.Write(type,value,Resolver);
        }else{
            var Formatter = Resolver.GetFormatter<TElement>();
            foreach(var item in value){
                writer.WriteValueSeparator();
                writer.Write(Formatter,item,Resolver);
            }
        }
        writer.WriteEndArray();
    }
    public G.Set<TKey,TElement> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        G.Set<TKey,TElement>value;
        reader.ReadIsBeginArrayWithVerify();
        var type=reader.ReadType();
        if(typeof(G.Set<TKey,TElement>)!=type){
            reader.ReadIsValueSeparatorWithVerify();
            value=(G.Set<TKey,TElement>)reader.Read(type,Resolver);
            reader.ReadIsEndArrayWithVerify();
        }else{
            value=new G.Set<TKey,TElement>();
            var Formatter = Resolver.GetFormatter<TElement>();
            while(!reader.ReadIsEndArray()) {
                reader.ReadIsValueSeparatorWithVerify();
                value.Add(reader.Read(Formatter,Resolver));
            }
        }
        return value;
    }
}
