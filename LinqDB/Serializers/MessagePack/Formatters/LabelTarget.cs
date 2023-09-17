using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.LabelTarget;
public class LabelTarget:IMessagePackFormatter<T> {
    public static readonly LabelTarget Instance=new();
    private const int ArrayHeader0=1;
    private const int ArrayHeader1=3;
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        //if(writer.TryWriteNil(value)) return;
        if(Resolver.Serializer().Dictionary_LabelTarget_int.TryGetValue(value,out var index)){
            writer.WriteArrayHeader(ArrayHeader0);
            writer.WriteInt32(index);
        } else{
            writer.WriteArrayHeader(ArrayHeader1);
            var Dictionary_LabelTarget_int=Resolver.Serializer().Dictionary_LabelTarget_int;
            index=Dictionary_LabelTarget_int.Count;
            Resolver.Serializer().LabelTargets.Add(value);
            Dictionary_LabelTarget_int.Add(value,index);
            writer.WriteInt32(index);
            writer.WriteType(value.Type);
            writer.Write(value.Name);
        }
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        //if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        var index=reader.ReadInt32();
        var LabelTargets=Resolver.Serializer().LabelTargets;
        T target;
        if(index<LabelTargets.Count){
            Debug.Assert(count==ArrayHeader0);
            target=LabelTargets[index];
        } else{
            Debug.Assert(count==ArrayHeader1);
            var type=reader.ReadType();
            var name=reader.ReadString();
            target=Expressions.Expression.Label(type,name);
            var Dictionary_LabelTarget_int=Resolver.Serializer().Dictionary_LabelTarget_int;
            index=Dictionary_LabelTarget_int.Count;
            LabelTargets.Add(target);
            Dictionary_LabelTarget_int.Add(target,index);
        }
        return target;
    }
}
