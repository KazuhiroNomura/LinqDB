using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Others;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.ConstantExpression;
public class Constant:IMessagePackFormatter<T> {
    public static readonly Constant Instance=new();
    private static void PrivateWrite(ref Writer writer,T? value,O Resolver){
        writer.WriteType(value!.Type);

        Object.WriteNullable(ref writer,value.Value,Resolver);
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(2);
        writer.WriteNodeType(Expressions.ExpressionType.Constant);
        
        PrivateWrite(ref writer,value,Resolver);
        
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(2);
        PrivateWrite(ref writer,value,Resolver);
        
    }
    internal static T Read(ref Reader reader,O Resolver){
        var type=reader.ReadType();
        
        var value=Object.ReadNullable(ref reader,Resolver);
        return Expressions.Expression.Constant(value,type);
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();

        Debug.Assert(count==2);
        return Read(ref reader,Resolver);
    }
}
