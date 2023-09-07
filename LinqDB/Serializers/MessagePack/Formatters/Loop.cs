using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.LoopExpression;
using C=MessagePackCustomSerializer;
public class Loop:IMessagePackFormatter<Expressions.LoopExpression>{
    public static readonly Loop Instance=new();
    public void Serialize(ref MessagePackWriter writer,Expressions.LoopExpression value,MessagePackSerializerOptions Resolver){
        LabelTarget.Instance.Serialize(ref writer,value.BreakLabel,Resolver);
        LabelTarget.Instance.Serialize(ref writer,value.ContinueLabel,Resolver);
        Expression.Instance.Serialize(ref writer,value.Body,Resolver);
    }
    public Expressions.LoopExpression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var breakLabel= LabelTarget.Instance.Deserialize(ref reader,Resolver);
        var continueLabel= LabelTarget.Instance.Deserialize(ref reader,Resolver);
        var body= Expression.Instance.Deserialize(ref reader,Resolver);
        return Expressions.Expression.Loop(body,breakLabel,continueLabel);
    }
}
