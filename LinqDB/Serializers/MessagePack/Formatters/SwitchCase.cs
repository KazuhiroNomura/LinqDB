using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using System.Diagnostics;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.SwitchCase;
using static Common;
public class SwitchCase:IMessagePackFormatter<T> {
    public static readonly SwitchCase Instance=new();
    private const int ArrayHeader=2;
    public void Serialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(ArrayHeader);
        SerializeReadOnlyCollection(ref writer,value.TestValues,Resolver);
        Expression.Instance.Serialize(ref writer,value.Body,Resolver);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        var testValues=DeserializeArray<Expressions.Expression>(ref reader,Resolver);
        var body= Expression.Instance.Deserialize(ref reader,Resolver);
        return Expressions.Expression.SwitchCase(body,testValues);
    }
}
