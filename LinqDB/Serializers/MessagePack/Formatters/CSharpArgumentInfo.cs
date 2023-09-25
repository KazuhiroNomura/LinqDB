using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
using MessagePack;
using MessagePack.Formatters;

namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = RuntimeBinder.CSharpArgumentInfo;
public class CSharpArgumentInfo:IMessagePackFormatter<T>{
    public static readonly CSharpArgumentInfo Instance=new();
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(2);
        var (flags,name)=value.GetFlagsName();
        writer.WriteInt32((int)flags);
        
        writer.Write(name);
        
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        var flags=(RuntimeBinder.CSharpArgumentInfoFlags)reader.ReadInt32();
        
        var name=reader.ReadString();
        
        return T.Create(flags,name);
    }
}
