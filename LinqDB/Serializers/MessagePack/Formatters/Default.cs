using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.DefaultExpression;
public class Default:IMessagePackFormatter<T> {
    public static readonly Default Instance=new();
    private static void PrivateWrite(ref Writer writer,T value){
        writer.WriteType(value.Type);
    }
    internal static void Write(ref Writer writer,T value){
        writer.WriteArrayHeader(2);
        writer.WriteNodeType(Expressions.ExpressionType.Default);
        
        PrivateWrite(ref writer,value);
        
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(1);
        PrivateWrite(ref writer,value);
        
    }
    internal static T Read(ref Reader reader){
        var type=reader.ReadType();
        return Expressions.Expression.Default(type);
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==1);
        return Read(ref reader);
        
    }
}
