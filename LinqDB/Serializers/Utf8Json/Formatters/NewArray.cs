using System;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using static Common;
public class NewArray:IJsonFormatter<Expressions.NewArrayExpression>{
    public static readonly NewArray Instance=new();
    public void Serialize(ref Writer writer,Expressions.NewArrayExpression? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        writer.WriteString(value.NodeType.ToString());
        writer.WriteValueSeparator();
        Type.Instance.Serialize(ref writer,value.Type.GetElementType(),Resolver);
        writer.WriteValueSeparator();
        SerializeReadOnlyCollection(ref writer,value.Expressions,Resolver);
        writer.WriteEndArray();
    }
    public Expressions.NewArrayExpression Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var NodeTypeName=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var NodeType=Enum.Parse<Expressions.ExpressionType>(NodeTypeName);
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var expressions= DeserializeArray<Expressions.Expression>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return NodeType switch{
            Expressions.ExpressionType.NewArrayBounds=>Expressions.Expression.NewArrayBounds(type,expressions),
            Expressions.ExpressionType.NewArrayInit=>Expressions.Expression.NewArrayInit(type,expressions),
            _=>throw new NotImplementedException(NodeTypeName)
        };
    }
}
