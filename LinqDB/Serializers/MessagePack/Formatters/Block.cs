using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using System.Diagnostics;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.BlockExpression;
using C=MessagePackCustomSerializer;
using static Common;
public class Block:IMessagePackFormatter<Expressions.BlockExpression>{
    public static readonly Block Instance=new();
    public void Serialize(ref MessagePackWriter writer,Expressions.BlockExpression value,MessagePackSerializerOptions Resolver){
        var ListParameter=C.Instance.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Variables=value.Variables;
        ListParameter.AddRange(Variables);
        writer.WriteArrayHeader(3);
        writer.WriteType(value.Type);
        Serialize宣言Parameters(ref writer,value.Variables);
        SerializeReadOnlyCollection(ref writer,value.Expressions,Resolver);
        ListParameter.RemoveRange(ListParameter_Count,Variables.Count);
    }
    public Expressions.BlockExpression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var ListParameter=C.Instance.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==3);
        var type=reader.ReadType();
        var variables= Deserialize宣言Parameters(ref reader,Resolver);
        ListParameter.AddRange(variables);
        var expressions=DeserializeArray<Expressions.Expression>(ref reader,Resolver);
        ListParameter.RemoveRange(ListParameter_Count,variables.Length);
        return Expressions.Expression.Block(type,variables,expressions);
    }
}
