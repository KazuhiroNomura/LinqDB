using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Enumerables;
using O = MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T=System.Collections.IEnumerable;
public class IEnumerable:IMessagePackFormatter<T>{
    public static readonly IEnumerable Instance=new();
    private IEnumerable(){}

    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(2);
        var type=value!.GetType();
        writer.WriteType(type);
        
        writer.Write(type,value,Resolver);
    }
    
    
    
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var Count=reader.ReadArrayHeader();
        System.Diagnostics.Debug.Assert(Count==2);
        var type=reader.ReadType();
        var value=reader.Read(type,Resolver);
        return(T)value;
    }
}