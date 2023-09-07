using LinqDB.Serializers.Utf8Json.Formatters;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.LabelExpression;
using C=MessagePackCustomSerializer;
using static Common;
public class Label:IMessagePackFormatter<Expressions.LabelExpression>{
    public static readonly Label Instance=new();
    public void Serialize(ref MessagePackWriter writer,Expressions.LabelExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        LabelTarget.Instance.Serialize(ref writer,value.Target,Resolver);
        Expression.Instance.Serialize(ref writer,value.DefaultValue,Resolver);
    }
    public Expressions.LabelExpression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var target= LabelTarget.Instance.Deserialize(ref reader,Resolver);
        var defaultValue=Expression.Instance.Deserialize(ref reader,Resolver);
        return Expressions.Expression.Label(target,defaultValue);
    }
}
