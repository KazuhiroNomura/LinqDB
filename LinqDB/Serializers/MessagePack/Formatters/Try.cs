using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using C=MessagePackCustomSerializer;
using T=Expressions.TryExpression;

using static Common;
public class Try:IMessagePackFormatter<T>{
    public static readonly Try Instance=new();
    public void Serialize(ref MessagePackWriter writer,T value,MessagePackSerializerOptions Resolver){
        Expression.Instance.Serialize(ref writer,value.Body,Resolver);
        Expression.Instance.Serialize(ref writer,value.Finally,Resolver);
        SerializeReadOnlyCollection(ref writer,value.Handlers,Resolver);
        //writer.Write("ABC");
    }
    public T Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var body= Expression.Instance.Deserialize(ref reader,Resolver);
        //var s=reader.ReadString();
        var @finally=Expression.Instance.Deserialize(ref reader,Resolver);
        var handlers=DeserializeArray<Expressions.CatchBlock>(ref reader,Resolver);
        if(handlers is null)
            return Expressions.Expression.TryFinally(body,@finally);
        //return (TryExpression)body;
        return Expressions.Expression.TryCatchFinally(body,@finally,handlers);
    }
}
