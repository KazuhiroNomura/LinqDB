using System;
using LinqDB.Databases;
using LinqDB.Sets;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters.Sets;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G=LinqDB.Sets;
public class Set<TKey,TElement, TContainer>:IJsonFormatter<G.Set<TKey,TElement,TContainer>>
    where TElement : Entity<TKey,TContainer>
    where TKey : struct, IEquatable<TKey>
    where TContainer : Container {
    public static readonly Set<TKey,TElement,TContainer> Instance = new();
    private static void WriteNullable(ref Writer writer,G.Set<TKey,TElement,TContainer>? value,O Resolver) {
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        var Formatter = Resolver.GetFormatter<TElement>();
        var first=true;
        foreach(var item in value!){
            if(first) first=false;
            else writer.WriteValueSeparator();
            Formatter.Serialize(ref writer,item,Resolver);
        }
        writer.WriteEndArray();
    }
    public void Serialize(ref Writer writer,G.Set<TKey,TElement,TContainer>? value,O Resolver) => WriteNullable(ref writer,value,Resolver);
    private static G.Set<TKey,TElement,TContainer>? ReadNullable(ref Reader reader,O Resolver) {
        if(reader.TryReadNil()) return null;
        reader.ReadIsBeginArrayWithVerify();
        //サーバーのSet<,,>は送られない。Set<,>で復元する
        var value = new G.Set<TKey,TElement,TContainer>(null!);
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
    public G.Set<TKey,TElement,TContainer> Deserialize(ref Reader reader,O Resolver) => ReadNullable(ref reader,Resolver)!;
}
