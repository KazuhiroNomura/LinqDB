using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.ListInitExpression;
using static Extension;
public class ListInit:IMessagePackFormatter<T> {
    public static readonly ListInit Instance=new();
    private const int ArrayHeader=2;
    private const int InternalArrayHeader=ArrayHeader+1;
    private static void PrivateSerialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        New.Instance.Serialize(ref writer,value!.NewExpression,Resolver);
        writer.SerializeReadOnlyCollection(value.Initializers,Resolver);
    }
    internal static void InternalSerialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(InternalArrayHeader);
        writer.WriteNodeType(Expressions.ExpressionType.ListInit);
        PrivateSerialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(ArrayHeader);
        PrivateSerialize(ref writer,value,Resolver);
    }
    internal static T InternalDeserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var @new=New.Instance.Deserialize(ref reader,Resolver);
        var Initializers=reader.ReadArray<Expressions.ElementInit>(Resolver);
        return Expressions.Expression.ListInit(@new,Initializers);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        //if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        return InternalDeserialize(ref reader,Resolver);
    }
}
