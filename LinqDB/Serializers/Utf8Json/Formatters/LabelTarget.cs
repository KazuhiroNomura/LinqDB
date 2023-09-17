using System.Diagnostics;
using Expressions = System.Linq.Expressions;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using static Extension;
using T = Expressions.LabelTarget;
public class LabelTarget:IJsonFormatter<T> {
    public static readonly LabelTarget Instance=new();
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        if(Resolver.Serializer().Dictionary_LabelTarget_int.TryGetValue(value,out var index)){
            writer.WriteInt32(index);
        } else{
            var Dictionary_LabelTarget_int=Resolver.Serializer().Dictionary_LabelTarget_int;
            index=Dictionary_LabelTarget_int.Count;
            Resolver.Serializer().LabelTargets.Add(value);
            Dictionary_LabelTarget_int.Add(value,index);
            writer.WriteInt32(index);
            writer.WriteValueSeparator();
            writer.WriteType(value.Type);
            writer.WriteValueSeparator();
            writer.WriteString(value.Name);
        }
        writer.WriteEndArray();
    }
    private static void WriteNullable(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.WriteIsNull(value))return;
        Write(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver)=>WriteNullable(ref writer,value,Resolver);
    internal static T Read(ref Reader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var index=reader.ReadInt32();
        var Instance=Resolver.Serializer();
        var LabelTargets=Instance.LabelTargets;
        Debug.Assert(Instance.Dictionary_LabelTarget_int.Count==Instance.LabelTargets.Count);
        T target;
        if(index<LabelTargets.Count){
            target=LabelTargets[index];
        } else{
            reader.ReadIsValueSeparatorWithVerify();
            var type=reader.ReadType();
            reader.ReadIsValueSeparatorWithVerify();
            var name=reader.ReadString();
            target=Expressions.Expression.Label(type,name);
            var Dictionary_LabelTarget_int=Instance.Dictionary_LabelTarget_int;
            Debug.Assert(index==Dictionary_LabelTarget_int.Count);
            index=Dictionary_LabelTarget_int.Count;
            LabelTargets.Add(target);
            Dictionary_LabelTarget_int.Add(target,index);
        }
        reader.ReadIsEndArrayWithVerify();
        return target;
    }
    internal static T? ReadNullable(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null;
        return Read(ref reader,Resolver);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver)=>ReadNullable(ref reader,Resolver)!;
}
