using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.DefaultExpression;
public class Default:IMessagePackFormatter<T> {
    public static readonly Default Instance=new();
    private const int ArrayHeader=1;
    private const int InternalArrayHeader=ArrayHeader+1;
    private static void PrivateSerialize(ref Writer writer,T value){
        writer.WriteType(value.Type);
    }
    internal static void Write(ref Writer writer,T value){
        writer.WriteArrayHeader(InternalArrayHeader);
        writer.WriteNodeType(Expressions.ExpressionType.Default);
        
        PrivateSerialize(ref writer,value);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(ArrayHeader);
        PrivateSerialize(ref writer,value);
        
    }
    internal static T Read(ref Reader reader){
        var type=reader.ReadType();
        return Expressions.Expression.Default(type);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        //if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        return Read(ref reader);
    }
}
