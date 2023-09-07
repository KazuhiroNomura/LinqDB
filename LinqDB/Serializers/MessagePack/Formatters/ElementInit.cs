using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.ElementInit;
using static Common;

public class ElementInit:IMessagePackFormatter<Expressions.ElementInit>{
    public static readonly ElementInit Instance=new();
    //public static readonly ElementInitFormatter Instance=new();
    public void Serialize(ref MessagePackWriter writer,Expressions.ElementInit value,MessagePackSerializerOptions Resolver){
        Method.Instance.Serialize(ref writer,value.AddMethod,Resolver);
        SerializeReadOnlyCollection(ref writer,value.Arguments,Resolver);
    }
    Expressions.ElementInit IMessagePackFormatter<Expressions.ElementInit>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var addMethod= Method.Instance.Deserialize(ref reader,Resolver);
        var arguments= DeserializeArray<Expressions.Expression>(ref reader,Resolver);
        return Expressions.Expression.ElementInit(addMethod,arguments);
    }
}
