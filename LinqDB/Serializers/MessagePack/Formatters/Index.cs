using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.IndexExpression;
using static Extension;
public class Index:IMessagePackFormatter<T> {
    public static readonly Index Instance=new();
    private const int ArrayHeader=4;
    private const int InternalArrayHeader=ArrayHeader+1;
    private static void PrivateSerialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        Expression.Write(ref writer,value!.Object,Resolver);
        Property.Instance.Serialize(ref writer,value.Indexer,Resolver);
        writer.SerializeReadOnlyCollection(value.Arguments,Resolver);
    }
    internal static void Write(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(InternalArrayHeader);
        writer.WriteNodeType(Expressions.ExpressionType.Index);
        PrivateSerialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        //if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(ArrayHeader);
        PrivateSerialize(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,MessagePackSerializerOptions Resolver){
        var instance= Expression.Read(ref reader,Resolver);
        var indexer= Property.Instance.Deserialize(ref reader,Resolver);
        var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
        return Expressions.Expression.MakeIndex(instance,indexer,arguments);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        //if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        return Read(ref reader,Resolver);
    }
}
