
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
    private const int ArrayHeader=3;
    private const int InternalArrayHeader=ArrayHeader+1;
    private static void PrivateSerialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        var ListParameter=Resolver.Serializer().ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Variables=value.Variables;
        ListParameter.AddRange(Variables);
        writer.WriteType(value.Type);
        
        writer.Serialize宣言Parameters(value.Variables,Resolver);
        
        writer.SerializeReadOnlyCollection(value.Expressions,Resolver);
        ListParameter.RemoveRange(ListParameter_Count,Variables.Count);
    }
    internal static void InternalSerialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(InternalArrayHeader);
        writer.WriteNodeType(Expressions.ExpressionType.Block);
        
        PrivateSerialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(ArrayHeader);
        PrivateSerialize(ref writer,value,Resolver);
        
    }
    internal static T InternalDeserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
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
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        return InternalDeserialize(ref reader,Resolver);
        
    }
}
