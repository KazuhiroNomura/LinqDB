using LinqDB.Serializers.Utf8Json.Formatters;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using System.Diagnostics;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.IndexExpression;
using C=MessagePackCustomSerializer;
using static Common;
public class Index:IMessagePackFormatter<Expressions.IndexExpression>{
    public static readonly Index Instance=new();
    public void Serialize(ref MessagePackWriter writer,Expressions.IndexExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        writer.WriteArrayHeader(3);
        Expression.Instance.Serialize(ref writer,value.Object,Resolver);
        Property.Instance.Serialize(ref writer,value.Indexer,Resolver);
        SerializeReadOnlyCollection(ref writer,value.Arguments,Resolver);
    }
    public Expressions.IndexExpression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==3);
        var instance= Expression.Instance.Deserialize(ref reader,Resolver);
        var indexer= Property.Instance.Deserialize(ref reader,Resolver);
        var arguments=DeserializeArray<Expressions.Expression>(ref reader,Resolver);
        return Expressions.Expression.MakeIndex(instance,indexer,arguments);
    }
}
