using MessagePack;
using MessagePack.Formatters;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.LoopExpression;
public class Loop:IMessagePackFormatter<T> {
    public static readonly Loop Instance=new();
    private static void PrivateWrite0(ref Writer writer,T value,int offset){
        if(value!.BreakLabel is null){//body
            writer.WriteArrayHeader(offset+1);
        } else if(value.ContinueLabel is null){//break,body
            writer.WriteArrayHeader(offset+2);
        }else{//break,continue,bo dy
            writer.WriteArrayHeader(offset+3);
        }
    }
    private static void PrivateWrite1(ref Writer writer,T value,O Resolver){
        if(value.BreakLabel is null) {//body
            Expression.Write(ref writer,value.Body,Resolver);
        } else if(value.ContinueLabel is null) {//break,body
            LabelTarget.Write(ref writer,value.BreakLabel,Resolver);
            Expression.Write(ref writer,value.Body,Resolver);
        } else {//break,continue,body
            LabelTarget.Write(ref writer,value.BreakLabel,Resolver);
            LabelTarget.Write(ref writer,value.ContinueLabel,Resolver);
            Expression.Write(ref writer,value.Body,Resolver);
        }
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        PrivateWrite0(ref writer,value,1);
        writer.WriteNodeType(Expressions.ExpressionType.Loop);
        
        PrivateWrite1(ref writer,value,Resolver);
        
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        PrivateWrite0(ref writer,value,0);
        PrivateWrite1(ref writer,value,Resolver);
        
    }
    internal static T Read(ref Reader reader,O Resolver,int ArrayHeader){
        T value;
        if(ArrayHeader==2) {//body
            var body = Expression.Read(ref reader,Resolver);
            value=Expressions.Expression.Loop(body);
        } else if(ArrayHeader==3){//break,body
        var breakLabel = LabelTarget.Read(ref reader,Resolver);
            var body = Expression.Read(ref reader,Resolver);
            value=Expressions.Expression.Loop(body,breakLabel);
        } else {//break,continue,body
            //Debug.Assert(ArrayHeader==4);
            var breakLabel = LabelTarget.Read(ref reader,Resolver);
            var continueLabel=LabelTarget.Read(ref reader,Resolver);
            var body = Expression.Read(ref reader,Resolver);
            value=Expressions.Expression.Loop(body,breakLabel,continueLabel);
        }
        return value;
    }
    
    
    
    
    
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var ArrayHeader=reader.ReadArrayHeader();
        return Read(ref reader,Resolver,ArrayHeader+1);
        
        
    }
}
