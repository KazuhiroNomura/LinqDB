using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.MemberInitExpression;
using C=MessagePackCustomSerializer;
using static Common;
public class MemberInit:IMessagePackFormatter<Expressions.MemberInitExpression>{
    public static readonly MemberInit Instance=new();
    public void Serialize(ref MessagePackWriter writer,Expressions.MemberInitExpression value,MessagePackSerializerOptions Resolver){
        New.Instance.Serialize(ref writer,value.NewExpression,Resolver);
        SerializeReadOnlyCollection(ref writer,value.Bindings,Resolver);
    }
    public Expressions.MemberInitExpression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var @new=New.Instance.Deserialize(ref reader,Resolver);
        var bindings=DeserializeArray<Expressions.MemberBinding>(ref reader,Resolver);
        return Expressions.Expression.MemberInit(@new,bindings);
    }
}
