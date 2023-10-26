using System.Reflection;
using MemoryPack;
using System.Buffers;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;


using Reader = MemoryPackReader;
using T = Expressions.UnaryExpression;
using Reflection;
public class Unary:MemoryPackFormatter<T> {
    public static readonly Unary Instance=new();
    internal static void WriteOperand<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter :IBufferWriter<byte> {

        writer.WriteNodeType(value);

        Expression.Write(ref writer,value.Operand);
        
    }
    internal static void WriteOperandType<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter :IBufferWriter<byte> {

        writer.WriteNodeType(value);

        Expression.Write(ref writer,value.Operand);

        writer.WriteType(value.Type);
        
    }
    internal static void WriteOperandMethod<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter :IBufferWriter<byte> {

        writer.WriteNodeType(value);
        
        Expression.Write(ref writer,value.Operand);
        
        Method.WriteNullable(ref writer,value.Method);

    }
    internal static void WriteOperandTypeMethod<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter :IBufferWriter<byte> {
        
        writer.WriteNodeType(value);
        
        Expression.Write(ref writer,value.Operand);
        
        writer.WriteType(value.Type);
        
        Method.WriteNullable(ref writer,value.Method);
        
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        switch(value!.NodeType){
            case Expressions.ExpressionType.ArrayLength   : 
            case Expressions.ExpressionType.Quote         :WriteOperand(ref writer,value);break;
            case Expressions.ExpressionType.Throw         : 
            case Expressions.ExpressionType.TypeAs        : 
            case Expressions.ExpressionType.Unbox         :WriteOperandType(ref writer,value);break;
            case Expressions.ExpressionType.Convert       : 
            case Expressions.ExpressionType.ConvertChecked:WriteOperandTypeMethod(ref writer,value);break;
            default                                       :WriteOperandMethod(ref writer,value);break;
        }
    }
    internal static Expressions.Expression ReadOperand(ref Reader reader){
        var operand=Expression.Read(ref reader);
        return operand;
    }
    internal static (Expressions.Expression Operand, System.Type Type) ReadOperandType(ref Reader reader) {
        var operand=Expression.Read(ref reader);
        
        var type=reader.ReadType();
        return (operand, type);
    }
    internal static (Expressions.Expression  Operand, MethodInfo? Method) ReadOperandMethod(ref Reader reader) {
        var operand=Expression.Read(ref reader);
        
        var method=Method.ReadNullable(ref reader);
        return (operand, method);
    }
    internal static (Expressions.Expression Operand, System.Type Type, MethodInfo? Method) ReadOperandTypeMethod(ref Reader reader) {
        var operand=Expression.Read(ref reader);
        
        var type=reader.ReadType();
        
        var method=Method.ReadNullable(ref reader);
        return (operand, type, method);
    }
    private static T Read(ref Reader reader){
        

        var NodeType=reader.ReadNodeType();
        
        T value;
        switch(NodeType){
            case Expressions.ExpressionType.ArrayLength: {
                var operand=ReadOperand(ref reader);
                value=Expressions.Expression.ArrayLength(operand);break;
            }
            case Expressions.ExpressionType.Quote: {
                var operand=ReadOperand(ref reader);
                value=Expressions.Expression.Quote(operand);break;
            }
            case Expressions.ExpressionType.Convert:{
                var (operand,Type,method)=ReadOperandTypeMethod(ref reader);
                value=Expressions.Expression.Convert(operand,Type,method);break;
            }
            case Expressions.ExpressionType.ConvertChecked: {
                var (operand,Type,method)=ReadOperandTypeMethod(ref reader);
                value=Expressions.Expression.ConvertChecked(operand,Type,method);break;
            }
            case Expressions.ExpressionType.Decrement: {
                var (operand,method)=ReadOperandMethod(ref reader);
                value=Expressions.Expression.Decrement(operand,method);break;
            }
            case Expressions.ExpressionType.Increment: {
                var (operand,method)=ReadOperandMethod(ref reader);
                value=Expressions.Expression.Increment(operand,method);break;
            }
            case Expressions.ExpressionType.IsFalse: {
                var (operand,method)=ReadOperandMethod(ref reader);
                value=Expressions.Expression.IsFalse(operand,method);break;
            }
            case Expressions.ExpressionType.IsTrue: {
                var (operand,method)=ReadOperandMethod(ref reader);
                value=Expressions.Expression.IsTrue(operand,method);break;
            }
            case Expressions.ExpressionType.Negate: {
                var (operand,method)=ReadOperandMethod(ref reader);
                value=Expressions.Expression.Negate(operand,method);break;
            }
            case Expressions.ExpressionType.NegateChecked: {
                var (operand,method)=ReadOperandMethod(ref reader);
                value=Expressions.Expression.NegateChecked(operand,method);break;
            }
            case Expressions.ExpressionType.Not: {
                var (operand,method)=ReadOperandMethod(ref reader);
                value=Expressions.Expression.Not(operand,method);break;
            }
            case Expressions.ExpressionType.OnesComplement: {
                var (operand,method)=ReadOperandMethod(ref reader);
                value=Expressions.Expression.OnesComplement(operand,method);break;
            }
            case Expressions.ExpressionType.PostDecrementAssign: {
                var (operand,method)=ReadOperandMethod(ref reader);
                value=Expressions.Expression.PostDecrementAssign(operand,method);break;
            }
            case Expressions.ExpressionType.PostIncrementAssign: {
                var (operand,method)=ReadOperandMethod(ref reader);
                value=Expressions.Expression.PostIncrementAssign(operand,method);break;
            }
            case Expressions.ExpressionType.PreDecrementAssign: {
                var (operand,method)=ReadOperandMethod(ref reader);
                value=Expressions.Expression.PreDecrementAssign(operand,method);break;
            }
            case Expressions.ExpressionType.PreIncrementAssign: {
                var (operand,method)=ReadOperandMethod(ref reader);
                value=Expressions.Expression.PreIncrementAssign(operand,method);break;
            }
            case Expressions.ExpressionType.UnaryPlus: {
                var (operand,method)=ReadOperandMethod(ref reader);
                value=Expressions.Expression.UnaryPlus(operand,method);break;
            }
            case Expressions.ExpressionType.Throw: {
                var (operand,type)=ReadOperandType(ref reader);
                value=Expressions.Expression.Throw(operand,type);break;
            }
            case Expressions.ExpressionType.TypeAs: {
                var (operand,type)=ReadOperandType(ref reader);
                value=Expressions.Expression.TypeAs(operand,type);break;
            }
            default:{
                System.Diagnostics.Debug.Assert(NodeType==Expressions.ExpressionType.Unbox);
                var (operand,type)=ReadOperandType(ref reader);
                value=Expressions.Expression.Unbox(operand,type);break;
            }
       }
       
       return value;
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value)=>value=reader.TryReadNil()?null:Read(ref reader);
}
