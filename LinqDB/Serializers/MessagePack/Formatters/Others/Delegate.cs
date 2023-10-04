using LinqDB.Serializers.MessagePack.Formatters.Reflection;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Others;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G = System.Delegate;
public class Delegate:IMessagePackFormatter<G>{
    public static readonly Delegate Instance=new();
    private const int ArrayHeader=3;
    internal static void Write(ref Writer writer,G? value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(ArrayHeader);
        writer.WriteType(value!.GetType());

        Method.Write(ref writer,value.Method,Resolver);

        Object.WriteNullable(ref writer,value.Target,Resolver);

    }
    public void Serialize(ref Writer writer,G? value,MessagePackSerializerOptions Resolver){
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value,Resolver);
    }
    internal static G Read(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();
        var delegateType=reader.ReadType();

        var method=Method.Read(ref reader,Resolver);

        var target=Object.ReadNullable(ref reader,Resolver);

        return method.CreateDelegate(delegateType,target);
    }
    public G Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        return Read(ref reader,Resolver);
    }
}
