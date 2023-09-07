using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using MemoryPack;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.MethodCallExpression;
using static Common;
public class MethodCall:IMessagePackFormatter<T> {
    public static readonly MethodCall Instance=new();
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        var method=value.Method;
        Method.Instance.Serialize(ref writer,method,Resolver);
        if(!method.IsStatic){
            Expression.Instance.Serialize(ref writer,value.Object!,Resolver);
        }
        SerializeReadOnlyCollection(ref writer,value.Arguments,Resolver);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var method= Method.Instance.Deserialize(ref reader,Resolver);
        if(method.IsStatic){
            var arguments=DeserializeArray<Expressions.Expression>(ref reader,Resolver);
            return Expressions.Expression.Call(
                method,
                arguments
            );
        } else{
            var instance= this.Deserialize(ref reader,Resolver);
            var arguments=DeserializeArray<Expressions.Expression>(ref reader,Resolver);
            return Expressions.Expression.Call(
                instance,
                method,
                arguments
            );
        }
    }
}
