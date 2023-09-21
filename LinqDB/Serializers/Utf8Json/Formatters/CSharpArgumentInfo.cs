using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
using Utf8Json;


namespace LinqDB.Serializers.Utf8Json.Formatters;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = RuntimeBinder.CSharpArgumentInfo;
public class CSharpArgumentInfo:IJsonFormatter<T> {
    public static readonly CSharpArgumentInfo Instance=new();
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        var (flags,name)=value.GetFlagsName();
        writer.WriteInt32((int)flags);
        writer.WriteValueSeparator();
        writer.WriteString(name);
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var flags=(RuntimeBinder.CSharpArgumentInfoFlags)reader.ReadInt32();
        reader.ReadIsValueSeparatorWithVerify();
        var name=reader.ReadString();
        reader.ReadIsEndArrayWithVerify();
        return T.Create(flags,name);
    }
}
