using System.Diagnostics;
using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.LabelTarget;
public class LabelTarget:IJsonFormatter<T> {
    public static readonly LabelTarget Instance=new();
    internal static void Write(ref Writer writer,T value,O Resolver){
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
    private static void WriteNullable(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value))return;
        Write(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,O Resolver)=>WriteNullable(ref writer,value,Resolver);
    internal static T Read(ref Reader reader,O Resolver){
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
    internal static T? ReadNullable(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null;
        return Read(ref reader,Resolver);
    }
    public T Deserialize(ref Reader reader,O Resolver)=>ReadNullable(ref reader,Resolver)!;
}
