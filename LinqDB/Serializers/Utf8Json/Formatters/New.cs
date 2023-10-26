using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Reflection;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.NewExpression;
public class New:IJsonFormatter<T> {
    public static readonly New Instance=new();
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteBeginArray();
        writer.WriteNodeType(Expressions.ExpressionType.New);
        writer.WriteValueSeparator();
        WriteNew(ref writer,value,Resolver);
        writer.WriteEndArray(); 
    }
    
    
    
    
    
    internal static void WriteNew(ref Writer writer,T value,O Resolver){
        if(value.Constructor is null){
            writer.WriteBoolean(true);
            writer.WriteValueSeparator();
            writer.WriteType(value.Type);
        } else{
            writer.WriteBoolean(false);
            writer.WriteValueSeparator();
            Constructor.Write(ref writer,value.Constructor,Resolver);
            writer.WriteValueSeparator();
            writer.WriteCollection(value.Arguments,Resolver);
        }
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        WriteNew(ref writer,value,Resolver);
        writer.WriteEndArray(); 
    }
    internal static T Read(ref Reader reader,O Resolver)=>ReadNew(ref reader,Resolver);
        
        
        










    internal static T ReadNew(ref Reader reader,O Resolver){
        var nullか=reader.ReadBoolean();
        reader.ReadIsValueSeparatorWithVerify();
        if(nullか){
            var type=reader.ReadType();
            return Expressions.Expression.New(type);
        } else{
            var constructor=Constructor.Read(ref reader,Resolver);
            reader.ReadIsValueSeparatorWithVerify();
            var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
            return Expressions.Expression.New(
                constructor,
                arguments
            );
        }
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=ReadNew(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
