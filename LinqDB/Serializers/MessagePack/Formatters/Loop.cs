using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.LoopExpression;
public class Loop:IMessagePackFormatter<T> {
    public static readonly Loop Instance=new();
    private static void PrivateSerialize0(ref Writer writer,T? value,int offset){
        if(value!.BreakLabel is null){//body
            writer.WriteArrayHeader(offset+1);
        } else if(value.ContinueLabel is null){//break,body
            writer.WriteArrayHeader(offset+2);
        }else{//break,continue,bo dy
            writer.WriteArrayHeader(offset+3);
        }
    }
    private static void PrivateSerialize1(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        if(value.BreakLabel is null){//body
            Expression.Instance.Serialize(ref writer,value.Body,Resolver);
        } else if(value.ContinueLabel is null){//break,body
            LabelTarget.Instance.Serialize(ref writer,value.BreakLabel,Resolver);
            Expression.Instance.Serialize(ref writer,value.Body,Resolver);
        } else{//break,continue,body
            LabelTarget.Instance.Serialize(ref writer,value.BreakLabel,Resolver);
            LabelTarget.Instance.Serialize(ref writer,value.ContinueLabel,Resolver);
            Expression.Instance.Serialize(ref writer,value.Body,Resolver);
        }
    }
    internal static void Write(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        PrivateSerialize0(ref writer,value,1);
        writer.WriteNodeType(Expressions.ExpressionType.Loop);
        PrivateSerialize1(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        PrivateSerialize0(ref writer,value,0);
        PrivateSerialize1(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,MessagePackSerializerOptions Resolver,int ArrayHeader){
        T value;
        if(ArrayHeader==2) {//body
            var body = Expression.Instance.Deserialize(ref reader,Resolver);
            value=Expressions.Expression.Loop(body);
        } else if(ArrayHeader==3){//break,body
            var breakLabel = LabelTarget.Instance.Deserialize(ref reader,Resolver);
            var body = Expression.Instance.Deserialize(ref reader,Resolver);
            value=Expressions.Expression.Loop(body,breakLabel);
        } else {//break,continue,body
            Debug.Assert(ArrayHeader==4);
            var breakLabel = LabelTarget.Instance.Deserialize(ref reader,Resolver);
            var continueLabel = LabelTarget.Instance.Deserialize(ref reader,Resolver);
            var body = Expression.Instance.Deserialize(ref reader,Resolver);
            value=Expressions.Expression.Loop(body,breakLabel,continueLabel);
        }
        return value;
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var ArrayHeader=reader.ReadArrayHeader();
        return Read(ref reader,Resolver,ArrayHeader+1);
    }
}
