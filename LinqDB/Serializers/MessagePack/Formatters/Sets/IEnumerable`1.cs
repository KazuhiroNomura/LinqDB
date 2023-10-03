using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;

using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Sets;
using Sets=LinqDB.Sets;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;

public class IEnumerable<T>:IMessagePackFormatter<Sets.IEnumerable<T>>{
    public static readonly IEnumerable<T> Instance=new();
    
    public void Serialize(ref Writer writer,Sets.IEnumerable<T>? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(2);
        var type=value!.GetType();
        writer.WriteType(type);
        writer.WriteValue(type,value,Resolver);
        //var o=type.GetValue("InstanceMemoryPack");
        //var Count=value.Count();
        //writer.WriteArrayHeader(1+Count);
        //writer.WriteCollection()
        //var type=value!.GetType();
        //writer.WriteType(type);
        //var Formatter=Resolver.Resolver.GetFormatter<T>()!;
        //foreach(var item in value)
        //    Formatter.Serialize(ref writer,item,Resolver);
    }


    public Sets.IEnumerable<T> Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil())return null!;
        var Count = reader.ReadArrayHeader();
        Debug.Assert(Count==2);
        var type = reader.ReadType();
        var o=type.GetValue("InstanceMemoryPack");
        var value=reader.ReadValue(type,Resolver);
        return (Sets.IEnumerable<T>)value;
        //var o=type.GetValue("InstanceMemoryPack");
        //var Count = reader.ReadArrayHeader();
        //var type=reader.ReadType();
        //var Formatter=Resolver.Resolver.GetFormatter<T>()!;
        //var set=Activator.CreateInstance(type)!;
        //var value=(Sets.ImmutableSet<T>)set;
        //for(long a=1;a<Count;a++)
        //    value.InternalAdd(Formatter.Deserialize(ref reader,Resolver));
        //value._LongCount=Count-1;
        //return value;
    }
    //public void Serialize(ref Writer writer,Sets.IEnumerable<T>? value,O Resolver){
    //    if(writer.TryWriteNil(value)) return;
    //    writer.WriteArrayHeader(2);
    //    var type=value!.GetType();
    //    writer.WriteType(type);
    //    //var Instance=type.GetField("InstanceMessagePack",System.Reflection.BindingFlags.Static|System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Public)!;
    //    var Formatter=type.GetValue("InstanceMessagePack");
    //    //var Formatter=(IMessagePackFormatter<Sets.IEnumerable<T>>)Value;
    //    //dynamic Formatter=Value;
    //    Serializer.DynamicSerialize(Formatter, ref writer, value, Resolver);
    //    //Formatter.Serialize(ref writer,value,Resolver);
    //}

    //public Sets.IEnumerable<T> Deserialize(ref Reader reader,O Resolver) {
    //    if(reader.TryReadNil())return null!;
    //    var Count = reader.ReadArrayHeader();
    //    var type=reader.ReadType();
    //    var Instance=type.GetField("InstanceMessagePack",System.Reflection.BindingFlags.Static|System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Public)!;
    //    var Value=Instance.GetValue(null)!;
    //    var Formatter=(IMessagePackFormatter<Sets.IEnumerable<T>>)Value;
    //    return Formatter.Deserialize(ref reader,Resolver);
    //}
}
