using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.UnaryExpression;
using Reflection;
public class Unary:IMessagePackFormatter<T> {
    public static readonly Unary Instance=new();
    internal static void WriteOperand(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(2);
        writer.WriteNodeType(value);
        
        Expression.Write(ref writer,value.Operand,Resolver);
        
    }
    internal static void WriteOperandType(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(3);
        writer.WriteNodeType(value);
        
        Expression.Write(ref writer,value.Operand,Resolver);
        
        writer.WriteType(value.Type);
        
    }
    internal static void WriteOperandMethod(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(3);
        writer.WriteNodeType(value);
        
        Expression.Write(ref writer,value.Operand,Resolver);
        
        Method.WriteNullable(ref writer,value.Method,Resolver);
        
    }
    internal static void WriteOperandTypeMethod(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(4);
        writer.WriteNodeType(value);
        
        Expression.Write(ref writer,value.Operand,Resolver);
        
        writer.WriteType(value.Type);
        
        Method.WriteNullable(ref writer,value.Method,Resolver);
        
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        switch(value!.NodeType){
            case Expressions.ExpressionType.ArrayLength   :
            case Expressions.ExpressionType.Quote         :WriteOperand(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Throw         :
            case Expressions.ExpressionType.TypeAs        :
            case Expressions.ExpressionType.Unbox         :WriteOperandType(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Convert       :
            case Expressions.ExpressionType.ConvertChecked:WriteOperandTypeMethod(ref writer,value,Resolver);break;
            default                                       :WriteOperandMethod(ref writer,value,Resolver);break;
        }
    }
    internal static Expressions.Expression ReadOperand(ref Reader reader,O Resolver){
        var operand=Expression.Read(ref reader,Resolver);
        return operand;
    }
    internal static (Expressions.Expression Operand,System.Type Type)ReadOperandType(ref Reader reader,O Resolver){
        var operand= Expression.Read(ref reader,Resolver);
        
        var type=reader.ReadType();
        return(operand,type);
    }
    internal static (Expressions.Expression Operand,MethodInfo? Method)ReadOperandMethod(ref Reader reader,O Resolver){
        var operand= Expression.Read(ref reader,Resolver);
        
        var method=Method.ReadNullable(ref reader,Resolver);
        return(operand,method);
    }
    internal static (Expressions.Expression Operand,System.Type Type,MethodInfo? Method)ReadOperandTypeMethod(ref Reader reader,O Resolver){
        var operand= Expression.Read(ref reader,Resolver);
        
        var type=reader.ReadType();
        
        var method=Method.ReadNullable(ref reader,Resolver);
        return(operand,type,method);
    }
    private static T Read(ref Reader reader,O Resolver){
        var count=reader.ReadArrayHeader();
        System.Diagnostics.Debug.Assert(count is >=2 and <=4);
        var NodeType=reader.ReadNodeType();
        
        T value;
        switch(NodeType){
            case Expressions.ExpressionType.ArrayLength: {
                var operand = ReadOperand(ref reader,Resolver);
                value=Expressions.Expression.ArrayLength(operand);break;
            }
            case Expressions.ExpressionType.Quote: {
                var operand = ReadOperand(ref reader,Resolver);
                value=Expressions.Expression.Quote(operand);break;
            }
            case Expressions.ExpressionType.Convert:{
                var (operand,type,method)=ReadOperandTypeMethod(ref reader,Resolver);
                value=Expressions.Expression.Convert(operand,type,method);break;
            }
            case Expressions.ExpressionType.ConvertChecked: {
                var (operand,type,method)=ReadOperandTypeMethod(ref reader,Resolver);
                value=Expressions.Expression.ConvertChecked(operand,type,method);break;
            }
            case Expressions.ExpressionType.Decrement: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.Decrement(operand,method);break;
            }
            case Expressions.ExpressionType.Increment: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.Increment(operand,method);break;
            }
            case Expressions.ExpressionType.IsFalse: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.IsFalse(operand,method);break;
            }
            case Expressions.ExpressionType.IsTrue: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.IsTrue(operand,method);break;
            }
            case Expressions.ExpressionType.Negate: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.Negate(operand,method);break;
            }
            case Expressions.ExpressionType.NegateChecked: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.NegateChecked(operand,method);break;
            }
            case Expressions.ExpressionType.Not: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.Not(operand,method);break;
            }
            case Expressions.ExpressionType.OnesComplement: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.OnesComplement(operand,method);break;
            }
            case Expressions.ExpressionType.PostDecrementAssign: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.PostDecrementAssign(operand,method);break;
            }
            case Expressions.ExpressionType.PostIncrementAssign: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.PostIncrementAssign(operand,method);break;
            }
            case Expressions.ExpressionType.PreDecrementAssign: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.PreDecrementAssign(operand,method);break;
            }
            case Expressions.ExpressionType.PreIncrementAssign: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.PreIncrementAssign(operand,method);break;
            }
            case Expressions.ExpressionType.Throw: {
                var (operand,type)=ReadOperandType(ref reader,Resolver);
                value=Expressions.Expression.Throw(operand,type);break;
            }
            case Expressions.ExpressionType.TypeAs: {
                var (operand,type)=ReadOperandType(ref reader,Resolver);
                value=Expressions.Expression.TypeAs(operand,type);break;
            }
            case Expressions.ExpressionType.UnaryPlus: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.UnaryPlus(operand,method);break;
            }
            default: {
                System.Diagnostics.Debug.Assert(NodeType==Expressions.ExpressionType.Unbox);
                var (operand,type)=ReadOperandType(ref reader,Resolver);
                value=Expressions.Expression.Unbox(operand,type);break;
            }
        }
        
        return value;
    }
    public T Deserialize(ref Reader reader,O Resolver)=>reader.TryReadNil()?null!:Read(ref reader,Resolver);
}
