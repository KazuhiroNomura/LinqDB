using System;
using LinqDB.Databases;
using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters.Sets;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G=LinqDB.Sets;
public class Set<TKey,TElement, TContainer>:IJsonFormatter<G.Set<TKey,TElement,TContainer>>
    where TElement : G.Entity<TKey,TContainer>
    where TKey : struct, IEquatable<TKey>
    where TContainer : Container {
    public static readonly Set<TKey,TElement,TContainer>Instance=new();
    public void Serialize(ref Writer writer,G.Set<TKey,TElement,TContainer>? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        var Formatter = Resolver.GetFormatter<TElement>();
        var first=true;
        foreach(var item in value!){
            if(first) first=false;
            else writer.WriteValueSeparator();
            writer.Write(Formatter,item,Resolver);
        }
        writer.WriteEndArray();
    }
    public G.Set<TKey,TElement,TContainer> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        //サーバーのSet<,,>は送られない。Set<,>で復元する
        var value = new G.Set<TKey,TElement,TContainer>(null!);
        var Formatter = Resolver.GetFormatter<TElement>();
        var first=true;
        while(!reader.ReadIsEndArray()) {
            if(first) first=false;
            else reader.ReadIsValueSeparatorWithVerify();
            value.Add(reader.Read(Formatter,Resolver));
        }
        return value;
    }
}
