using MessagePack;
using MessagePack.Formatters;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.GotoExpression;
public class Goto:IMessagePackFormatter<T> {
    public static readonly Goto Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,O Resolver){
        writer.Write((byte)value.Kind);
        
        LabelTarget.Write(ref writer,value.Target,Resolver);
        
        Expression.WriteNullable(ref writer,value.Value,Resolver);
        
        writer.WriteType(value.Type);
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(5);
        writer.WriteNodeType(Expressions.ExpressionType.Goto);
        
        PrivateWrite(ref writer,value,Resolver);
        
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(4);
        PrivateWrite(ref writer,value,Resolver);
        
    }
    internal static T Read(ref Reader reader,O Resolver){
        var kind=(Expressions.GotoExpressionKind)reader.ReadByte();
        
        var target= LabelTarget.Read(ref reader,Resolver);
        
        var value=Expression.ReadNullable(ref reader,Resolver);
        
        var type=reader.ReadType();
        return Expressions.Expression.MakeGoto(kind,target,value,type);
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        return Read(ref reader,Resolver);
    }
}