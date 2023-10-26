using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.BlockExpression;
public class Block:IMessagePackFormatter<T> {
    public static readonly Block Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,O Resolver){
        var Variables=value.Variables;
        writer.WriteType(value.Type);
        
        writer.Serialize宣言Parameters(Variables,Resolver);
        var Parameters=Resolver.Serializer().Parameters;
        var Parameters_Count=Parameters.Count;
        Parameters.AddRange(Variables);
        
        writer.WriteCollection(value.Expressions,Resolver);
        Parameters.RemoveRange(Parameters_Count,Variables.Count);
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(4);
        writer.WriteNodeType(Expressions.ExpressionType.Block);
        
        PrivateWrite(ref writer,value,Resolver);
        
    }
    public void Serialize(ref Writer writer,T value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(3);
        PrivateWrite(ref writer,value,Resolver);
        
    }
    internal static T Read(ref Reader reader,O Resolver){
        var type=reader.ReadType();
        
        var variables=reader.Deserialize宣言Parameters(Resolver);
        var Parameters=Resolver.Serializer().Parameters;
        var Parameters_Count=Parameters.Count;
        Parameters.AddRange(variables);
        
        var expressions=reader.ReadArray<Expressions.Expression>(Resolver);
        Parameters.RemoveRange(Parameters_Count,variables.Length);
        return Expressions.Expression.Block(type,variables,expressions);
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==3);
        return Read(ref reader,Resolver);
        
    }
}
