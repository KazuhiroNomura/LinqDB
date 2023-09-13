using System;
using Expressions=System.Linq.Expressions;
using Utf8Json;
using System.Diagnostics;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using static Extension;
public class NewArray:IJsonFormatter<Expressions.NewArrayExpression>{
    public static readonly NewArray Instance=new();
    public void Serialize(ref Writer writer,Expressions.NewArrayExpression? value,IJsonFormatterResolver Resolver){
        //if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        writer.WriteString(value.NodeType.ToString());
        writer.WriteValueSeparator();
        writer.WriteType(value.Type.GetElementType());
        writer.WriteValueSeparator();
        writer.SerializeReadOnlyCollection(value.Expressions,Resolver);
        writer.WriteEndArray();
    }
    public Expressions.NewArrayExpression Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        //if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var NodeTypeName=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var NodeType=Enum.Parse<Expressions.ExpressionType>(NodeTypeName);
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var expressions= reader.ReadArray<Expressions.Expression>(Resolver);
        reader.ReadIsEndArrayWithVerify();
        return NodeType switch{
            Expressions.ExpressionType.NewArrayBounds=>Expressions.Expression.NewArrayBounds(type,expressions),
            Expressions.ExpressionType.NewArrayInit=>Expressions.Expression.NewArrayInit(type,expressions),
            _=>throw new NotImplementedException(NodeTypeName)
        };
    }
}
