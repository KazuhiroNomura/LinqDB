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
        writer.WriteArrayHeader(2);
        var type=value!.GetType();
        writer.WriteType(type);
        writer.Write(type,value,Resolver);
        /*
        var Count=value.Count();
        writer.WriteArrayHeader(Count);
        var Formatter=Resolver.GetFormatter<T>();
        foreach(var item in value)
            writer.Write(Formatter,item,Resolver);
            */
    }
    
    




    public G.IEnumerable<T> Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil())return null!;
        var Count = reader.ReadArrayHeader();
        return(G.IEnumerable<T>)reader.Read(reader.ReadType(),Resolver);
        /*
        var Count = reader.ReadArrayHeader();
        var Formatter=Resolver.GetFormatter<T>();
        var value=new G.List<T>();
        while(Count-->0)
            value.Add(reader.Read(Formatter,Resolver));
        return value;
        */
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

