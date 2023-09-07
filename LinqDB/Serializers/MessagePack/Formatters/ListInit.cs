using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.ListInitExpression;
using C=MessagePackCustomSerializer;
using static Common;
public class ListInit:IMessagePackFormatter<Expressions.ListInitExpression>{
    public static readonly ListInit Instance=new();
    public void Serialize(ref MessagePackWriter writer,Expressions.ListInitExpression value,MessagePackSerializerOptions Resolver){
        New.Instance.Serialize(ref writer,value.NewExpression,Resolver);
        SerializeReadOnlyCollection(ref writer,value.Initializers,Resolver);
    }
    public Expressions.ListInitExpression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var @new=New.Instance.Deserialize(ref reader,Resolver);
        var Initializers=DeserializeArray<Expressions.ElementInit>(ref reader,Resolver);
        return Expressions.Expression.ListInit(@new,Initializers);
    }
}
