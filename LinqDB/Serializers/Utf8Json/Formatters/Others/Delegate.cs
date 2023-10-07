using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters.Others;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using Reflection;
public class Delegate<T>:IJsonFormatter<T> where T:System.Delegate{
    public static readonly Delegate<T> Instance=new();
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        writer.WriteType(value!.GetType());
        writer.WriteValueSeparator();
        Method.Write(ref writer,value.Method,Resolver);
        writer.WriteValueSeparator();
        Object.WriteNullable(ref writer,value.Target,Resolver);
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var delegateType=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var method=Method.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var target=Object.ReadNullable(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return (T)method.CreateDelegate(delegateType,target);
    }
}
