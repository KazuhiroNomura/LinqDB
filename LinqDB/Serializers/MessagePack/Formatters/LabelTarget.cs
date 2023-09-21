using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.LabelTarget;
public class LabelTarget:IMessagePackFormatter<T> {
    public static readonly LabelTarget Instance=new();
    internal static void Write(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        if(Resolver.Serializer().Dictionary_LabelTarget_int.TryGetValue(value,out var index)){
            writer.WriteArrayHeader(1);
            writer.WriteInt32(index);
        } else{
            writer.WriteArrayHeader(3);
            var Dictionary_LabelTarget_int=Resolver.Serializer().Dictionary_LabelTarget_int;
            index=Dictionary_LabelTarget_int.Count;
            Resolver.Serializer().LabelTargets.Add(value);
            Dictionary_LabelTarget_int.Add(value,index);
            writer.WriteInt32(index);
            writer.WriteType(value.Type);
            
            writer.Write(value.Name);
        }
        
    }
    internal static void WriteNullable(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value))return;
        Write(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,O Resolver)=>WriteNullable(ref writer,value,Resolver);
    internal static T Read(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        var index=reader.ReadInt32();
        var LabelTargets=Resolver.Serializer().LabelTargets;
        T target;
        if(index<LabelTargets.Count){
            Debug.Assert(count==1);
            target=LabelTargets[index];
        } else{
            Debug.Assert(count==3);
            var type=reader.ReadType();
            
            var name=reader.ReadString();
            target=Expressions.Expression.Label(type,name);
            var Dictionary_LabelTarget_int=Resolver.Serializer().Dictionary_LabelTarget_int;
            Debug.Assert(index==Dictionary_LabelTarget_int.Count);
            index=Dictionary_LabelTarget_int.Count;
            LabelTargets.Add(target);
            Dictionary_LabelTarget_int.Add(target,index);
        }
        
        return target;
    }
    internal static T? ReadNullable(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null;
        return Read(ref reader,Resolver);
    }
    public T Deserialize(ref Reader reader,O Resolver)=>ReadNullable(ref reader,Resolver)!;
}
