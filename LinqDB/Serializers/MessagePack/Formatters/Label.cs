using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.LabelExpression;
public class Label:IMessagePackFormatter<T> {
    public static readonly Label Instance=new();
    private static void PrivateWrite(ref Writer writer,T? value,O Resolver){
        LabelTarget.Write(ref writer,value!.Target,Resolver);
        
        Expression.WriteNullable(ref writer,value.DefaultValue,Resolver);
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(3);
        writer.WriteNodeType(Expressions.ExpressionType.Label);
        
        PrivateWrite(ref writer,value,Resolver);

    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(2);
        PrivateWrite(ref writer,value,Resolver);
        
    }
    internal static T Read(ref Reader reader,O Resolver){
        var target= LabelTarget.Read(ref reader,Resolver);
        var defaultValue=Expression.ReadNullable(ref reader,Resolver);
        
        return Expressions.Expression.Label(target,defaultValue);
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        
        return Read(ref reader,Resolver);
    }
}
