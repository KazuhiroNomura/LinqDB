using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Reflection;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.NewExpression;
public class New:IMessagePackFormatter<T> {
    public static readonly New Instance=new();
    internal static void Write(ref Writer writer,T value,O Resolver){
        if(value.Constructor is null){
            writer.WriteArrayHeader(2);
            writer.WriteNodeType(Expressions.ExpressionType.New);
            writer.WriteType(value.Type);
        } else{
            writer.WriteArrayHeader(3);
            writer.WriteNodeType(Expressions.ExpressionType.New);
            Constructor.Write(ref writer,value.Constructor!,Resolver);
            writer.WriteCollection(value.Arguments,Resolver);
        }
    }
    internal static void WriteNew(ref Writer writer,T value,O Resolver){
        if(value.Constructor is null){
            writer.WriteArrayHeader(1);
            writer.WriteType(value.Type);
        } else{
            writer.WriteArrayHeader(2);
            Constructor.Write(ref writer,value.Constructor!,Resolver);
            writer.WriteCollection(value.Arguments,Resolver);
        }
    }

    
    
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        WriteNew(ref writer,value,Resolver);
    }
    
    
    internal static T Read(ref Reader reader,O Resolver,int ArrayHeader){
        if(ArrayHeader==2){
            var type=reader.ReadType();
            return Expressions.Expression.New(type);
        } else{
            System.Diagnostics.Debug.Assert(ArrayHeader==3);
            var constructor=Constructor.Read(ref reader,Resolver);
            var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
            return Expressions.Expression.New(
                constructor,
                arguments
            );
        }
    }
    internal static T ReadNew(ref Reader reader,O Resolver){
        var ArrayHeader=reader.ReadArrayHeader();
        if(ArrayHeader==1){
            var type=reader.ReadType();
            return Expressions.Expression.New(type);
        } else{
            System.Diagnostics.Debug.Assert(ArrayHeader==2);
            var constructor=Constructor.Read(ref reader,Resolver);
            var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
            return Expressions.Expression.New(
                constructor,
                arguments
            );
        }
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        return ReadNew(ref reader,Resolver);
    }
}
