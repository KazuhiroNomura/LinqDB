using System.Diagnostics.CodeAnalysis;
using MessagePack;
using MessagePack.Formatters;
using System.Linq;

namespace LinqDB.Serializers.MessagePack.Formatters.Enumerables;
using O = MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G=System.Collections.Generic;
public class IEnumerable<T>:IMessagePackFormatter<G.IEnumerable<T>>{
    internal static readonly IEnumerable<T> Instance=new();
    private IEnumerable(){}
    [SuppressMessage("ReSharper","PossibleMultipleEnumeration")]
    public void Serialize(ref Writer writer,G.IEnumerable<T>? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        
        var type=value!.GetType();
        var Formatter=Resolver.GetFormatterDynamic(type);
        if(Formatter is not null){
            writer.WriteArrayHeader(2);
            writer.WriteType(type);
            writer.Write(Formatter,value,Resolver);
        } else{
            var Count=value.Count();
            writer.WriteArrayHeader(Count+1);
            writer.WriteType(typeof(G.IEnumerable<T>));
            var FormatterT=Resolver.GetFormatter<T>();
            foreach(var item in value)
                writer.Write(FormatterT,item,Resolver);
        }
    }



    public G.IEnumerable<T> Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil())return null!;
        var Count= reader.ReadArrayHeader();
        var type=reader.ReadType();
        if(type!=typeof(G.IEnumerable<T>)){
            var value=reader.Read(type,Resolver);
            return (G.IEnumerable<T>)value;
        } else{
            var FormatterT=Resolver.GetFormatter<T>();
            Count--;
            var value=new T[Count];
            for(var a=0;a<Count;a++)
                value[a]=reader.Read(FormatterT,Resolver);
            return value;
        }
    }
}








//public class IEnumerableOther<TEnumerable,T>:IMessagePackFormatter<System.Collections.Generic.IEnumerable<T>>where TEnumerable:System.Collections.Generic.IEnumerable<T>{
//    internal static readonly IEnumerableOther<TEnumerable,T> Instance=new();
//    private IEnumerableOther(){}
//    [SuppressMessage("ReSharper","PossibleMultipleEnumeration")]
//    public void Serialize(ref Writer writer,G.IEnumerable<T>? value,O Resolver){
//        if(writer.TryWriteNil(value)) return;
//        var Count=value.Count();
//        writer.WriteArrayHeader(Count);
//        var Formatter=Resolver.GetFormatter<T>();
//        foreach(var item in value)
//            writer.Write(Formatter,item,Resolver);
//    }
//    public G.IEnumerable<T> Deserialize(ref Reader reader,O Resolver) {
//        if(reader.TryReadNil())return null!;
//        var Count = reader.ReadArrayHeader();
//        var Formatter=Resolver.GetFormatter<T>();
//        var value=new G.List<T>();
//        while(Count-->0)
//            value.Add(reader.Read(Formatter,Resolver));
//        return value;
//    }
//}
public class IEnumerableOther<TEnumerable,TElement>:IMessagePackFormatter<TEnumerable>where TEnumerable:G.IEnumerable<TElement>{
    internal static readonly IEnumerableOther<TEnumerable,TElement> Instance=new();
    private IEnumerableOther(){}
    [SuppressMessage("ReSharper","PossibleMultipleEnumeration")]
    public void Serialize(ref Writer writer,TEnumerable? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        var Count=value.Count();
        writer.WriteArrayHeader(Count);
        var Formatter=Resolver.GetFormatter<TElement>();
        foreach(var item in value)
            writer.Write(Formatter,item,Resolver);
    }
    public TEnumerable Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil())return default!;
        var Count = reader.ReadArrayHeader();
        var Formatter=Resolver.GetFormatter<TElement>();
        var value=new G.List<TElement>();
        while(Count-->0)
            value.Add(reader.Read(Formatter,Resolver));
        return (dynamic)value;
    }
}
//public class Other<TEnumerable,T>:IMessagePackFormatter<System.Collections.Generic.IEnumerable<T>>where TEnumerable:System.Collections.Generic.IEnumerable<T>{
//    internal static readonly Other<TEnumerable,T> Instance=new();
//    private Other(){}
//    [SuppressMessage("ReSharper","PossibleMultipleEnumeration")]
//    public void Serialize(ref Writer writer,G.IEnumerable<T> value,O Resolver){
//        if(writer.TryWriteNil(value)) return;
//        var Count=value.Count();
//        writer.WriteArrayHeader(Count);
//        var Formatter=Resolver.GetFormatter<T>();
//        foreach(var item in value)
//            writer.Write(Formatter,item,Resolver);
//    }
//    public G.IEnumerable<T> Deserialize(ref Reader reader,O Resolver) {
//        if(reader.TryReadNil())return null!;
//        var Count = reader.ReadArrayHeader();
//        var Formatter=Resolver.GetFormatter<T>();
//        var value=new G.List<T>();
//        while(Count-->0)
//            value.Add(reader.Read(Formatter,Resolver));
//        return value;
//    }
//}

