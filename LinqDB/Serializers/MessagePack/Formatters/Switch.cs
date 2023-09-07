using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.SwitchExpression;
using C=MessagePackCustomSerializer;
using static Common;
public class Switch:IMessagePackFormatter<Expressions.SwitchExpression>{
    public static readonly Switch Instance=new();
    public void Serialize(ref MessagePackWriter writer,Expressions.SwitchExpression value,MessagePackSerializerOptions Resolver){
        writer.WriteType(value.Type);
        Expression.Instance.Serialize(ref writer,value.SwitchValue,Resolver);
        Method.Instance.Serialize(ref writer,value.Comparison,Resolver);
        SerializeReadOnlyCollection(ref writer,value.Cases,Resolver);
        Expression.Instance.Serialize(ref writer,value.DefaultBody,Resolver);
    }
    public Expressions.SwitchExpression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var type=reader.ReadType();
        var switchValue= this.Deserialize(ref reader,Resolver);
        var comparison= Method.Instance.Deserialize(ref reader,Resolver);
        var cases=DeserializeArray<Expressions.SwitchCase>(ref reader,Resolver);
        var defaultBody= this.Deserialize(ref reader,Resolver);
        return Expressions.Expression.Switch(type,switchValue,defaultBody,comparison,cases);
    }
}
