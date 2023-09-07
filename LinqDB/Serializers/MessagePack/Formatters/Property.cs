using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using LinqDB.Serializers.Utf8Json.Formatters;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=PropertyInfo;
using C=MessagePackCustomSerializer;
public class Property:IMessagePackFormatter<T> {
    public static readonly Property Instance=new();
    public void Serialize(ref MessagePackWriter writer,T? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        writer.WriteArrayHeader(3);
        var ReflectedType=value.ReflectedType!;
        writer.WriteType(ReflectedType);
        writer.Write(value.Name);
        writer.WriteInt32(Array.IndexOf(C.Instance.TypeProperties.Get(ReflectedType),value));
    }
    public T Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==3);
        var ReflectedType=reader.ReadType();
        var Name=reader.ReadString();
        var array= C.Instance.TypeProperties.Get(ReflectedType);
        var Index=reader.ReadInt32();
        return array[Index];
    }
}
