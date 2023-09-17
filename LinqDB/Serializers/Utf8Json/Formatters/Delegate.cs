using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using static Extension;
using T = System.Delegate;
public class Delegate:IJsonFormatter<T> {
    public static readonly Delegate Instance =new();
    
    internal static void Write(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        writer.WriteType(value!.GetType());
        writer.WriteValueSeparator();
        Method.Write(ref writer,value.Method,Resolver);
        writer.WriteValueSeparator();
        Object.Instance.Serialize(ref writer,value.Target,Resolver);
        writer.WriteEndArray();
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        Write(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var delegateType=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var method=Method.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var target=Object.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return method.CreateDelegate(delegateType,target);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        return Read(ref reader,Resolver);
    }
}
