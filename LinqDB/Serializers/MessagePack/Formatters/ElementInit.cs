using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Reflection;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.ElementInit;
public class ElementInit:IMessagePackFormatter<T> {
    public static readonly ElementInit Instance=new();
    private const int ArrayHeader=2;
    public void Serialize(ref Writer writer,T value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(ArrayHeader);
        Method.Write(ref writer,value.AddMethod,Resolver);
        writer.WriteCollection(value.Arguments,Resolver);
        
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();Debug.Assert(count==ArrayHeader);
        var addMethod= Method.Read(ref reader,Resolver);
        var arguments= reader.ReadArray<Expressions.Expression>(Resolver);
        
        return Expressions.Expression.ElementInit(addMethod,arguments);
    }
}
