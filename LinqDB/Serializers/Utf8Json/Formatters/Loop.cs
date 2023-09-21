
using Utf8Json;

using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using O=IJsonFormatterResolver;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.LoopExpression;
public class Loop:IJsonFormatter<T>{
    public static readonly Loop Instance=new();
    
    
    
    
    
    
    
    
    
    private static void PrivateWrite(ref Writer writer,T value,O Resolver){
        if(writer.TryWriteNil(value.BreakLabel)){
            writer.WriteValueSeparator();
            Expression.Write(ref writer,value.Body,Resolver);
        } else{
            LabelTarget.Write(ref writer,value.BreakLabel,Resolver);
            writer.WriteValueSeparator();
            if(writer.TryWriteNil(value.ContinueLabel)){
                writer.WriteValueSeparator();
                Expression.Write(ref writer,value.Body,Resolver);
            } else{
                LabelTarget.Write(ref writer,value.ContinueLabel,Resolver);
                writer.WriteValueSeparator();
                Expression.Write(ref writer,value.Body,Resolver);
            }
        }
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteBeginArray();
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateWrite(ref writer,value,Resolver);
        writer.WriteEndArray(); 
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        PrivateWrite(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T Read(ref Reader reader,O Resolver){
        T value;
        if(reader.TryReadNil()){
            reader.ReadNext();
            var body=Expression.Read(ref reader,Resolver);
            value=Expressions.Expression.Loop(body);
        } else{
            var breakLabel=LabelTarget.Read(ref reader,Resolver);
            reader.ReadIsValueSeparatorWithVerify();
            if(reader.TryReadNil()){
                reader.ReadNext();
                var body=Expression.Read(ref reader,Resolver);
                value=Expressions.Expression.Loop(body,breakLabel);
            } else{
                
                var continueLabel=LabelTarget.Read(ref reader,Resolver);
                reader.ReadIsValueSeparatorWithVerify();
                var body=Expression.Read(ref reader,Resolver);
                value=Expressions.Expression.Loop(body,breakLabel,continueLabel);
            }
        }
        return value;
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}