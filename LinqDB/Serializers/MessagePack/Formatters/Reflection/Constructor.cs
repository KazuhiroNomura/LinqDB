using System.Diagnostics;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Reflection;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G = ConstructorInfo;
public class Constructor:IMessagePackFormatter<G>{
    public static readonly Constructor Instance=new();
    internal static void Write(ref Writer writer,G value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(2);
        var type=value.ReflectedType;
        writer.WriteType(type);
        var array=Resolver.Serializer().TypeConstructors.Get(type);
        var index=System.Array.IndexOf(array,value);
        writer.WriteInt32(index);
    }
    public void Serialize(ref Writer writer,G value,MessagePackSerializerOptions Resolver){
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value,Resolver);
    }
    internal static G Read(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==2);
        var type=reader.ReadType();
        var array=Resolver.Serializer().TypeConstructors.Get(type);
        var index=reader.ReadInt32();

        return array[index];
    }
    public G Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        return Read(ref reader,Resolver);
    }
}
