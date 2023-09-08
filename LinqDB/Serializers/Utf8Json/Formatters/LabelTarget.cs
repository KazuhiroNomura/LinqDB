using System.Diagnostics;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using static Common;
using C=Serializer;
using T=Expressions.LabelTarget;
public class LabelTarget:IJsonFormatter<T> {
    public static readonly LabelTarget Instance=new();
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        if(C.Instance.Dictionary_LabelTarget_int.TryGetValue(value,out var index)){
            writer.WriteInt32(index);
        } else{
            var Dictionary_LabelTarget_int=C.Instance.Dictionary_LabelTarget_int;
            index=Dictionary_LabelTarget_int.Count;
            C.Instance.ListLabelTarget.Add(value);
            Dictionary_LabelTarget_int.Add(value,index);
            writer.WriteInt32(index);
            writer.WriteValueSeparator();
            Type.Instance.Serialize(ref writer,value.Type,Resolver);
            writer.WriteValueSeparator();
            writer.WriteString(value.Name);
        }
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var index=reader.ReadInt32();
        var Instance=C.Instance;
        var ListLabelTarget=Instance.ListLabelTarget;
        Debug.Assert(Instance.Dictionary_LabelTarget_int.Count==Instance.ListLabelTarget.Count);
        T target;
        if(index<ListLabelTarget.Count){
            target=ListLabelTarget[index];
        } else{
            reader.ReadIsValueSeparatorWithVerify();
            var type=reader.ReadType();
            reader.ReadIsValueSeparatorWithVerify();
            var name=reader.ReadString();
            target=Expressions.Expression.Label(type,name);
            var Dictionary_LabelTarget_int=Instance.Dictionary_LabelTarget_int;
            Debug.Assert(index==Dictionary_LabelTarget_int.Count);
            index=Dictionary_LabelTarget_int.Count;
            ListLabelTarget.Add(target);
            Dictionary_LabelTarget_int.Add(target,index);
        }
        reader.ReadIsEndArrayWithVerify();
        return target;
    }
}
