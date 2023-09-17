
using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.BlockExpression;
public class Block:IMessagePackFormatter<T> {
    public static readonly Block Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        var ListParameter=Resolver.Serializer().ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Variables=value.Variables;
        ListParameter.AddRange(Variables);
        writer.WriteType(value.Type);
        
        writer.Serialize宣言Parameters(value.Variables,Resolver);
        
        writer.SerializeReadOnlyCollection(value.Expressions,Resolver);
        ListParameter.RemoveRange(ListParameter_Count,Variables.Count);
    }
    internal static void Write(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(4);
        writer.WriteNodeType(Expressions.ExpressionType.Block);
        
        PrivateWrite(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        if(writer.TryWriteNil(value)) return;

        writer.WriteArrayHeader(3);
        PrivateWrite(ref writer,value,Resolver);
        
    }
    internal static T Read(ref Reader reader,MessagePackSerializerOptions Resolver){
        var ListParameter=Resolver.Serializer().ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var type=reader.ReadType();
        
        var variables= reader.Deserialize宣言Parameters(Resolver);
        
        ListParameter.AddRange(variables);
        var expressions=reader.ReadArray<Expressions.Expression>(Resolver);
        ListParameter.RemoveRange(ListParameter_Count,variables.Length);
        return Expressions.Expression.Block(type,variables,expressions);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==3);
        return Read(ref reader,Resolver);
        
    }
}
