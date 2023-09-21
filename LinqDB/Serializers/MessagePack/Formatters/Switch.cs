using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Reflection;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.SwitchExpression;
public class Switch:IMessagePackFormatter<T> {
    public static readonly Switch Instance=new();
    private static void PrivateWrite(ref Writer writer,T? value,O Resolver){
        writer.WriteType(value!.Type);
        
        Expression.Write(ref writer,value.SwitchValue,Resolver);
        
        Method.WriteNullable(ref writer,value.Comparison,Resolver);
        
        writer.WriteCollection(value.Cases,Resolver);
        
        Expression.Write(ref writer,value.DefaultBody,Resolver);
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(6);
        writer.WriteNodeType(Expressions.ExpressionType.Switch);
        
        PrivateWrite(ref writer,value,Resolver);
        
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(5);
        PrivateWrite(ref writer,value,Resolver);
        
    }
    internal static T Read(ref Reader reader,O Resolver){
        var type=reader.ReadType();
        
        var switchValue= Expression.Read(ref reader,Resolver);
        
        var comparison= Method.ReadNullable(ref reader,Resolver);
        
        var cases=reader.ReadArray<Expressions.SwitchCase>(Resolver);
        
        var defaultBody= Expression.Read(ref reader,Resolver);
        return Expressions.Expression.Switch(type,switchValue,defaultBody,comparison,cases);
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==5);
        
        return Read(ref reader,Resolver);
    }
}
