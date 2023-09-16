using Expressions=System.Linq.Expressions;
using Utf8Json;
using System.Diagnostics.CodeAnalysis;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using static Extension;
using T=Expressions.TryExpression;
public class Try:IJsonFormatter<T> {
    public static readonly Try Instance=new();
    private static void PrivateSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        Expression.Write(ref writer,value.Body,Resolver);
        writer.WriteValueSeparator();
        Expression.WriteNullable(ref writer,value.Finally,Resolver);
        writer.WriteValueSeparator();
        if(value.Finally is not null){
            writer.SerializeReadOnlyCollection(value.Handlers,Resolver);
        } else{
            Expression.WriteNullable(ref writer,value.Fault,Resolver);
            if(value.Fault is null){
                writer.WriteValueSeparator();
                writer.SerializeReadOnlyCollection(value.Handlers,Resolver);
            }
        }
    }
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateSerialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        PrivateSerialize(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    [SuppressMessage("ReSharper","ConvertIfStatementToConditionalTernaryExpression")]
    [SuppressMessage("ReSharper","ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract")]
    internal static T Read(ref Reader reader,IJsonFormatterResolver Resolver){
        T value;
        var body=Expression.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var @finally=Expression.ReadNullable(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        if(@finally is not null){
            var handlers=reader.ReadArray<Expressions.CatchBlock>(Resolver)!;
            if(handlers.Length>0) {
                value=Expressions.Expression.TryCatchFinally(body,@finally,handlers!);
            } else {
                value=Expressions.Expression.TryFinally(body,@finally);
            }
        } else{
            var fault= Expression.ReadNullable(ref reader,Resolver);
            if(fault is not null){
                value=Expressions.Expression.TryFault(body,fault);
            } else{
                reader.ReadIsValueSeparatorWithVerify();
                var handlers=reader.ReadArray<Expressions.CatchBlock>(Resolver)!;
                value=Expressions.Expression.TryCatch(body,handlers!);
            }
        }
        return value;
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
