using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = System.Delegate;
public class Delegate:IMessagePackFormatter<T>{
    public static readonly Delegate Instance=new();
    private const int ArrayHeader=3;
    private static void PrivateSerialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(ArrayHeader);
        Type.Instance.Serialize(ref writer,value!.GetType(),Resolver);
        Method.Instance.Serialize(ref writer,value.Method,Resolver);
        Object.Instance.Serialize(ref writer,value.Target,Resolver);
    }
    internal static void InternalSerializeNullable(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        if(value is null)writer.WriteNil();
        else PrivateSerialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        PrivateSerialize(ref writer,value,Resolver);
    }
    private static T PrivateDeserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();
        var delegateType=Type.Instance.Deserialize(ref reader,Resolver);
        var method=Method.Instance.Deserialize(ref reader,Resolver);
        var target=Object.Instance.Deserialize(ref reader,Resolver);
        return method.CreateDelegate(delegateType,target);
    }
    internal static T? InternalDeserializeNullable(ref Reader reader,MessagePackSerializerOptions Resolver){
        return reader.TryReadNil()?null:PrivateDeserialize(ref reader,Resolver);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        return PrivateDeserialize(ref reader,Resolver);
    }
}
