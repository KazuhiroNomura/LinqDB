using Utf8Json;

using System.Diagnostics.CodeAnalysis;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using O=IJsonFormatterResolver;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.TryExpression;
public class Try:IJsonFormatter<T> {
    public static readonly Try Instance=new();
    
    
    
    
    
    
    
    
    
    
    
    private static void PrivateWrite(ref Writer writer,T value,O Resolver){
        Expression.Write(ref writer,value.Body,Resolver);
        writer.WriteValueSeparator();
        Expression.WriteNullable(ref writer,value.Finally,Resolver);
        writer.WriteValueSeparator();
        if(value.Finally is not null){
            writer.WriteCollection(value.Handlers,Resolver);
        } else{
            Expression.WriteNullable(ref writer,value.Fault,Resolver);
            if(value.Fault is null){
                writer.WriteValueSeparator();
                writer.WriteCollection(value.Handlers,Resolver);
            }
        }
        writer.WriteEndArray(); 
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteBeginArray();
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateWrite(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,O Resolver) {
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        PrivateWrite(ref writer,value,Resolver);
    }
    [SuppressMessage("ReSharper","ConvertIfStatementToConditionalTernaryExpression")]
    [SuppressMessage("ReSharper","ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract")]
    internal static T Read(ref Reader reader,O Resolver){
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
    public T Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
