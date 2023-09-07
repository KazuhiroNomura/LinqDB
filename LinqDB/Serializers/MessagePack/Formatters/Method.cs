using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

using LinqDB.Serializers.MemoryPack;
using LinqDB.Sets;

using MessagePack;
using MessagePack.Formatters;
using Microsoft.CodeAnalysis;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=MethodInfo;
using C=MessagePackCustomSerializer;
public class Method:IMessagePackFormatter<T>{
    public static readonly Method Instance=new();
    public void Serialize(ref MessagePackWriter writer,T? value,MessagePackSerializerOptions Resolver){
        Debug.Assert(value!=null,nameof(value)+" != null");
        var ReflectedType=value.ReflectedType!;
        writer.WriteType(ReflectedType);
        var array= C.Instance.TypeMethods.Get(ReflectedType!);
        writer.WriteInt32(Array.IndexOf(array,value));
    }
    public T Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var type=reader.ReadType();
        var array= C.Instance.TypeMethods.Get(type);
        var index=reader.ReadInt32();
        return array[index];
    }
}
