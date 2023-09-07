using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.LabelTarget;
using C=MessagePackCustomSerializer;
public class LabelTarget:IMessagePackFormatter<Expressions.LabelTarget>{
    public static readonly LabelTarget Instance=new();
    public void Serialize(ref MessagePackWriter writer,Expressions.LabelTarget? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        if(C.Instance.Dictionary_LabelTarget_int.TryGetValue(value,out var index)){
            writer.WriteInt32(index);
        } else{
            var Dictionary_LabelTarget_int=C.Instance.Dictionary_LabelTarget_int;
            index=Dictionary_LabelTarget_int.Count;
            C.Instance.ListLabelTarget.Add(value);
            Dictionary_LabelTarget_int.Add(value,index);
            writer.WriteInt32(index);
            writer.WriteType(value.Type);
            writer.Write(value.Name);
        }
    }
    public Expressions.LabelTarget Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var index=reader.ReadInt32();
        var ListLabelTarget=C.Instance.ListLabelTarget;
        Expressions.LabelTarget target;
        if(index<ListLabelTarget.Count){
            target=ListLabelTarget[index];
        } else{
            var type=reader.ReadType();
            var name=reader.ReadString();
            target=Expressions.Expression.Label(type,name);
            var Dictionary_LabelTarget_int=C.Instance.Dictionary_LabelTarget_int;
            index=Dictionary_LabelTarget_int.Count;
            ListLabelTarget.Add(target);
            Dictionary_LabelTarget_int.Add(target,index);
        }
        return target;
    }
}
