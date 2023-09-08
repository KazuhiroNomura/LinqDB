using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using System.Diagnostics;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.LoopExpression;
public class Loop:IMessagePackFormatter<T> {
    public static readonly Loop Instance=new();
    private const int ArrayHeader=3;
    private const int InternalArrayHeader=ArrayHeader+1;
    private static void PrivateSerialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        LabelTarget.Instance.Serialize(ref writer,value.BreakLabel,Resolver);
        LabelTarget.Instance.Serialize(ref writer,value.ContinueLabel,Resolver);
        Expression.Instance.Serialize(ref writer,value.Body,Resolver);
    }
    internal static void InternalSerialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(InternalArrayHeader);
        writer.WriteNodeType(Expressions.ExpressionType.Loop);
        PrivateSerialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(ArrayHeader);
        PrivateSerialize(ref writer,value,Resolver);
    }
    internal static T InternalDeserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var breakLabel= LabelTarget.Instance.Deserialize(ref reader,Resolver);
        var continueLabel= LabelTarget.Instance.Deserialize(ref reader,Resolver);
        var body= Expression.Instance.Deserialize(ref reader,Resolver);
        return Expressions.Expression.Loop(body,breakLabel,continueLabel);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        return InternalDeserialize(ref reader,Resolver);
    }
}
