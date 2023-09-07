using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.DefaultExpression;
using C=MessagePackCustomSerializer;
public class Default:IMessagePackFormatter<Expressions.DefaultExpression>{
    public static readonly Default Instance=new();
    public void Serialize(ref MessagePackWriter writer,Expressions.DefaultExpression value,MessagePackSerializerOptions Resolver){
        //options.Resolver.GetFormatter<Type>().Serialize(ref writer,value.Type,options);
        writer.WriteType(value.Type);
    }
    public Expressions.DefaultExpression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var type=reader.ReadType();
        return Expressions.Expression.Default(type);
    }
}
