using Expressions = System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using System.Diagnostics.CodeAnalysis;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.TryExpression;

using static Extension;
public class Try:IMessagePackFormatter<T>{
    public static readonly Try Instance=new();
    private static void PrivateSerialize0(ref Writer writer,T? value,int offset){
        if(value!.Finally is not null){
            writer.WriteArrayHeader(offset+3);
        } else{
            if(value.Fault is null){
                writer.WriteArrayHeader(offset+4);
            } else{
                writer.WriteArrayHeader(offset+3);
            }
        }
    }
    private static void PrivateSerialize1(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        Expression.Instance.Serialize(ref writer,value!.Body,Resolver);
        Expression.InternalSerializeNullable(ref writer,value.Finally,Resolver);
        if(value.Finally is not null){
            writer.SerializeReadOnlyCollection(value.Handlers,Resolver);
        } else{
            Expression.InternalSerializeNullable(ref writer,value.Fault,Resolver);
            if(value.Fault is null){
                writer.SerializeReadOnlyCollection(value.Handlers,Resolver);
            }
        }
    }
    internal static void InternalSerialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        PrivateSerialize0(ref writer,value,1);
        writer.WriteNodeType(Expressions.ExpressionType.Try);
        PrivateSerialize1(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        PrivateSerialize0(ref writer,value,0);
        PrivateSerialize1(ref writer,value,Resolver);
    }
    [SuppressMessage("ReSharper","ConvertIfStatementToConditionalTernaryExpression")]
    internal static T InternalDeserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        T value;
        var body= Expression.Instance.Deserialize(ref reader,Resolver);
        var @finally=Expression.InternalDeserializeNullable(ref reader,Resolver);
        if(@finally is not null){
            var handlers=reader.ReadArray<Expressions.CatchBlock>(Resolver);
            if(handlers.Length>0) {
                value=Expressions.Expression.TryCatchFinally(body,@finally,handlers!);
            } else {
                value=Expressions.Expression.TryFinally(body,@finally);
            }
        } else{
            var fault= Expression.InternalDeserializeNullable(ref reader,Resolver);
            if(fault is not null){
                value=Expressions.Expression.TryFault(body,fault);
            } else{
                var handlers=reader.ReadArray<Expressions.CatchBlock>(Resolver);
                value=Expressions.Expression.TryCatch(body,handlers!);
            }
        }
        return value;
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        //if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        return InternalDeserialize(ref reader,Resolver);
    }
}
