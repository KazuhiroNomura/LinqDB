using System.Diagnostics;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Reflection;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = ConstructorInfo;
public class Constructor:IMessagePackFormatter<T>{
    public static readonly Constructor Instance=new();
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(2);
        var type=value.ReflectedType;
        writer.WriteType(type);
        var array=Resolver.Serializer().TypeConstructors.Get(type);
        var index=System.Array.IndexOf(array,value);
        writer.WriteInt32(index);
    }
    internal static void WriteNullable(ref Writer writer,T?value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T value,O Resolver)=>WriteNullable(ref writer,value,Resolver);
    internal static T Read(ref Reader reader,O Resolver){
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==2);
        var type=reader.ReadType();
        var array=Resolver.Serializer().TypeConstructors.Get(type);
        var index=reader.ReadInt32();

        return array[index];
    }
    internal static T? ReadNullable(ref Reader reader,O Resolver)=>reader.TryReadNil()?null:Read(ref reader,Resolver);
    public T Deserialize(ref Reader reader,O Resolver)=>ReadNullable(ref reader,Resolver)!;
}
