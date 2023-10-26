using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Reflection;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.ElementInit;
public class ElementInit:IMessagePackFormatter<T> {
    public static readonly ElementInit Instance=new();
    public void Serialize(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(2);
        Method.Write(ref writer,value.AddMethod,Resolver);
        
        writer.WriteCollection(value.Arguments,Resolver);
        
    }
    public T Deserialize(ref Reader reader,O Resolver){
        var count=reader.ReadArrayHeader();
        var addMethod= Method.Read(ref reader,Resolver);
        
        var arguments= reader.ReadArray<Expressions.Expression>(Resolver);
        
        return Expressions.Expression.ElementInit(addMethod,arguments);
    }
}
