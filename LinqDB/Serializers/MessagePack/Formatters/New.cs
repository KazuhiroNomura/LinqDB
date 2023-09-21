using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.NewExpression;
using Reflection;
public class New:IMessagePackFormatter<T> {
    public static readonly New Instance=new();
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(3);
        writer.WriteNodeType(Expressions.ExpressionType.New);
        
        Constructor.Write(ref writer,value!.Constructor!,Resolver);
        
        writer.WriteCollection(value.Arguments,Resolver);
        
    }
    internal static void WriteNew(ref Writer writer,T? value,O Resolver){
        writer.WriteArrayHeader(2);
        Constructor.Write(ref writer,value!.Constructor!,Resolver);
        
        writer.WriteCollection(value.Arguments,Resolver);
        
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        WriteNew(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,O Resolver){
        var constructor=Constructor.Read(ref reader,Resolver);
        
        var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
        return Expressions.Expression.New(
            constructor,
            arguments
        );
    }
    internal static T ReadNew(ref Reader reader,O Resolver){
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==2);
        return Read(ref reader,Resolver);

    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==2);
        return Read(ref reader,Resolver);
    }
}
