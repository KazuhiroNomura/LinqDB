using Expressions = System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.ParameterExpression;
using C=MessagePackCustomSerializer;
public class Parameter:IMessagePackFormatter<Expressions.ParameterExpression>{
    public static readonly Parameter Instance=new();
    public void Serialize(ref MessagePackWriter writer,Expressions.ParameterExpression value,MessagePackSerializerOptions Resolver){
        writer.WriteInt32(C.Instance.ListParameter.LastIndexOf(value));
    }
    public Expressions.ParameterExpression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var index=reader.ReadInt32();
        var Parameter= C.Instance.ListParameter[index];
        return Parameter;
    }
}
