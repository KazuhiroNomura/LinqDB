using System;
using System.Linq;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=ConstructorInfo;
using C=MessagePackCustomSerializer;
public class Constructor:IMessagePackFormatter<ConstructorInfo>{
    public static readonly Constructor Instance=new();
    public void Serialize(ref MessagePackWriter writer,ConstructorInfo value,MessagePackSerializerOptions Resolver){
        var ReflectedType=value.ReflectedType!;
        writer.WriteType(ReflectedType);
        var array= C.Instance.TypeConstructors.Get(ReflectedType!);
        writer.WriteInt32(Array.IndexOf(array,value));
    }
    public ConstructorInfo Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var type=reader.ReadType();
        var results= C.Instance.TypeConstructors.Get(type);
        var Index=reader.ReadInt32();
        return results[Index];
    }
}
