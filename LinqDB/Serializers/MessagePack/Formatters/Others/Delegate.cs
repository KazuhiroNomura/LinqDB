using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Others;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using Reflection;
public class Delegate<T>:IMessagePackFormatter<T>where T:System.Delegate{
    public static readonly Delegate<T>Instance=new();
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(3);
        writer.WriteType(value!.GetType());

        Method.Write(ref writer,value.Method,Resolver);

        Object.WriteNullable(ref writer,value.Target,Resolver);
        
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        var delegateType=reader.ReadType();

        var method=Method.Read(ref reader,Resolver);

        var target=Object.ReadNullable(ref reader,Resolver);

        return (T)method.CreateDelegate(delegateType,target);
    }
}
