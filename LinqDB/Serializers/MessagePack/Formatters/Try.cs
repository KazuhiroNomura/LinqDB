using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using System.Diagnostics;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.TryExpression;

using static Extension;
public class Try:IMessagePackFormatter<T>{
    public static readonly Try Instance=new();
    private const int ArrayHeader=3;
    private const int InternalArrayHeader=ArrayHeader+1;
    private static void PrivateSerialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        Expression.Instance.Serialize(ref writer,value!.Body,Resolver);
        Expression.Instance.Serialize(ref writer,value.Finally,Resolver);
        writer.SerializeReadOnlyCollection(value.Handlers,Resolver);
    }
    internal static void InternalSerialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(InternalArrayHeader);
        writer.WriteNodeType(Expressions.ExpressionType.Try);
        PrivateSerialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        //if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(ArrayHeader);
        PrivateSerialize(ref writer,value,Resolver);
    }
    internal static T InternalDeserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var body= Expression.Instance.Deserialize(ref reader,Resolver);
        var @finally=Expression.Instance.Deserialize(ref reader,Resolver);
        var handlers=reader.DeserializeArray<Expressions.CatchBlock>(Resolver);
        return handlers is null?Expressions.Expression.TryFinally(body,@finally):Expressions.Expression.TryCatchFinally(body,@finally,handlers);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        //if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        return InternalDeserialize(ref reader,Resolver);
    }
}
