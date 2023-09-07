using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.GotoExpression;
using C=MessagePackCustomSerializer;
using static Common;
public class Goto:IMessagePackFormatter<Expressions.GotoExpression>{
    public static readonly Goto Instance=new();
    public void Serialize(ref MessagePackWriter writer,Expressions.GotoExpression value,MessagePackSerializerOptions Resolver){
        writer.Write((byte)value.Kind);
        LabelTarget.Instance.Serialize(ref writer,value.Target,Resolver);
        Expression.Instance.Serialize(ref writer,value.Value,Resolver);
        writer.WriteType(value.Type);
    }
    public Expressions.GotoExpression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var kind=(Expressions.GotoExpressionKind)reader.ReadByte();
        var target= LabelTarget.Instance.Deserialize(ref reader,Resolver);
        var value=Expression.Instance.Deserialize(ref reader,Resolver);
        var type=reader.ReadType();
        return Expressions.Expression.MakeGoto(kind,target,value,type);
    }
}