using System.Diagnostics.CodeAnalysis;
using MemoryPack;

using System.Linq;

namespace LinqDB.Serializers.MemoryPack.Formatters.Enumerables;


using Reader = MemoryPackReader;
using G = System.Collections.Generic;
public class IEnumerable<T>: MemoryPackFormatter<G.IEnumerable<T>>{
    internal static readonly IEnumerable<T> Instance = new();
    private IEnumerable(){}
    [SuppressMessage("ReSharper","PossibleMultipleEnumeration")]
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.IEnumerable<T>? value){
        if(writer.TryWriteNil(value)) return;
        var type=value!.GetType();
        //if(FormatterResolver.GetRegisteredFormatter(type) is IMemoryPackFormatter Formatter) {
        //    writer.WriteType(type);
        //    writer.Write(Formatter,value);
        //    //Write(ref writer,Formatter,value);
        //} else{
        //    //Formatter=writer.GetFormatter(type);
        //    //Formatter.Serialize(ref writer, ref value);
        //    //Debug.Fail($"{type.FullName} Formatterがない");
        //    writer.WriteType(type);
        //    writer.WriteValue(type,value);
        //}
        writer.WriteType(type);
        writer.Write(type,value);
        /*
        if(writer.TryWriteNil(value)) return;
        writer.WriteVarInt(value.Count());
        var Formatter=writer.GetFormatter<T>();
        foreach(var item in value)
            writer.Write(Formatter,item);
*/
    }
    
    
    
    
    
    
    
    public override void Deserialize(ref Reader reader,scoped ref G.IEnumerable<T>? value){
        if(reader.TryReadNil()) return;
        value=(G.IEnumerable<T>?)reader.Read(reader.ReadType());
                /*
        if(reader.TryReadNil()) return;
        var Count=reader.ReadVarIntInt64();
        var Formatter=reader.GetFormatter<T>();
        var value0=new G.List<T>();
        while(Count-->0)
            value0.Add(reader.Read(Formatter));
        value=value0;
        */
    }
}
//public class IEnumerableOther<T>: MemoryPackFormatter<T>where T:G.IEnumerable<T>{
public class IEnumerableOther<T>: MemoryPackFormatter<G.IEnumerable<T>>{
    internal static readonly IEnumerableOther<T> Instance = new();
    private IEnumerableOther(){}
    [SuppressMessage("ReSharper","PossibleMultipleEnumeration")]
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.IEnumerable<T>? value){
        if(writer.TryWriteNil(value)) return;
        writer.WriteVarInt(value.Count());
        var Formatter=FormatterResolver.GetFormatterDynamic<T>()??writer.GetFormatter<T>();
        foreach(var item in value)
            writer.Write(Formatter,item);
    }
    public override void Deserialize(ref Reader reader,scoped ref G.IEnumerable<T>? value){
        if(reader.TryReadNil()) return;
        var Count=reader.ReadVarIntInt64();
        var Formatter=FormatterResolver.GetFormatterDynamic<T>()??reader.GetFormatter<T>();
        var value0=new G.List<T>();
        while(Count-->0)
            value0.Add(reader.Read(Formatter));
        value=value0;
    }
}
//public class IEnumerableOther<TEnumerable,TElement>: MemoryPackFormatter<TEnumerable>where TEnumerable:G.IEnumerable<TElement>{
//    internal static readonly IEnumerableOther<TEnumerable,TElement> Instance = new();
//    private IEnumerableOther(){}
//    [SuppressMessage("ReSharper","PossibleMultipleEnumeration")]
//    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref TEnumerable? value){
//        if(writer.TryWriteNil(value)) return;
//        writer.WriteVarInt(value.Count());
//        var Formatter=FormatterResolver.GetRegisteredFormatter<TElement>()??writer.GetFormatter<TElement>();
//        foreach(var item in value)
//            writer.Write(Formatter,item);
//    }
//    public override void Deserialize(ref Reader reader,scoped ref TEnumerable? value){
//        if(reader.TryReadNil()) return;
//        var Count=reader.ReadVarIntInt64();
//        var Formatter=FormatterResolver.GetRegisteredFormatter<TElement>()??reader.GetFormatter<TElement>();
//        var value0=new G.List<TElement>();
//        while(Count-->0)
//            value0.Add(reader.Read(Formatter));
//        value=(TEnumerable)value0;
//    }
//}
