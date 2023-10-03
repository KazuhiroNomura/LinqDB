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

public class IEnumerable:IMessagePackFormatter<Sets.IEnumerable>{
    public static readonly IEnumerable Instance=new();
    
    public void Serialize(ref Writer writer,Sets.IEnumerable? value,O Resolver){
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


    public Sets.IEnumerable Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil())return null!;
        var Count = reader.ReadArrayHeader();
        Debug.Assert(Count==2);
        var type = reader.ReadType();
        var o=type.GetValue("InstanceMemoryPack");
        var value=reader.ReadValue(type,Resolver);
        return (Sets.IEnumerable)value;
    }
}
