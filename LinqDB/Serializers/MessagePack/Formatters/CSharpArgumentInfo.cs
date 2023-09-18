using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
using MessagePack;
using MessagePack.Formatters;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = RuntimeBinder.CSharpArgumentInfo;
using static Common;
public class CSharpArgumentInfo:IMessagePackFormatter<T>{
    public static readonly CSharpArgumentInfo Instance=new();
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(3);
        var (flags,name)=GetFlagsName(value);
        writer.WriteInt32((int)flags);
        writer.Write(name);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        var flags=(RuntimeBinder.CSharpArgumentInfoFlags)reader.ReadInt32();
        var name=reader.ReadString();
        return T.Create(flags,name);
    }
}
