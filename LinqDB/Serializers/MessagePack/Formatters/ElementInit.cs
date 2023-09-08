using System.Diagnostics;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.ElementInit;
using static Common;

public class ElementInit:IMessagePackFormatter<T> {
    public static readonly ElementInit Instance=new();
    private const int ArrayHeader=2;
    public void Serialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(ArrayHeader);
        Method.InternalSerializeNullable(ref writer,value.AddMethod,Resolver);
        SerializeReadOnlyCollection(ref writer,value.Arguments,Resolver);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        var addMethod= Method.Instance.Deserialize(ref reader,Resolver);
        var arguments= DeserializeArray<Expressions.Expression>(ref reader,Resolver);
        return Expressions.Expression.ElementInit(addMethod,arguments);
    }
}
