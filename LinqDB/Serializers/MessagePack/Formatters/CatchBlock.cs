using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using LinqDB.Serializers.MemoryPack;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using C=MessagePackCustomSerializer;
using T=Expressions.CatchBlock;
public class CatchBlock:IMessagePackFormatter<T> {
    public static readonly CatchBlock Instance=new();
    public void Serialize(ref MessagePackWriter writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteType(value.Test);
        if(value.Variable is null){
            writer.WriteNil();
            //writer.WriteInt32(0);
        } else{
            //writer.WriteInt32(1);
            writer.Write(value.Variable.Name);
            C.Instance.ListParameter.Add(value.Variable);
        }
        Expression.Instance.Serialize(ref writer,value.Body,Resolver);
        Expression.Instance.Serialize(ref writer,value.Filter,Resolver);
        if(value.Variable is not null){
            var ListParameter=C.Instance.ListParameter;
            ListParameter.RemoveAt(ListParameter.Count-1);
        }
    }
    public T Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var test=reader.ReadType();
        if(reader.TryReadNil()){
            var body= Expression.Instance.Deserialize(ref reader,Resolver);
            var @filter= Expression.Instance.Deserialize(ref reader,Resolver);
            return Expressions.Expression.Catch(test,body,@filter);
        } else{
            var name=reader.ReadString();
            var ListParameter=C.Instance.ListParameter;
            ListParameter.Add(Expressions.Expression.Parameter(test,name));
            var body= Expression.Instance.Deserialize(ref reader,Resolver);
            var @filter= Expression.Instance.Deserialize(ref reader,Resolver);
            ListParameter.RemoveAt(ListParameter.Count-1);
            return Expressions.Expression.Catch(Expressions.Expression.Parameter(test,name),body,@filter);
        }
    }
}
