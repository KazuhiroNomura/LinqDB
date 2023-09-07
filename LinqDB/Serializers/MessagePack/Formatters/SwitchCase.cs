using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.SwitchCase;
using static Common;
public class SwitchCase:IMessagePackFormatter<Expressions.SwitchCase>{
    public static readonly CatchBlock Instance=new();
    public void Serialize(ref MessagePackWriter writer,Expressions.SwitchCase value,MessagePackSerializerOptions Resolver){
        SerializeReadOnlyCollection(ref writer,value.TestValues,Resolver);
        Expression.Instance.Serialize(ref writer,value.Body,Resolver);
    }
    Expressions.SwitchCase IMessagePackFormatter<Expressions.SwitchCase>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var testValues=DeserializeArray<Expressions.Expression>(ref reader,Resolver);
        var body= Expression.Instance.Deserialize(ref reader,Resolver);
        return Expressions.Expression.SwitchCase(body,testValues);
    }
}
