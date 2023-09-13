using System;
using System.Diagnostics;
using Expressions=System.Linq.Expressions;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.UnaryExpression;
public class Unary:IMessagePackFormatter<T> {
    public static readonly Unary Instance=new();
    internal static void InternalSerializeOperand(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(2);
        writer.WriteNodeType(value.NodeType);
        Expression.Instance.Serialize(ref writer,value.Operand,Resolver);
    }
    internal static void InternalSerializeOperandType(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(3);
        writer.WriteNodeType(value.NodeType);
        Expression.Instance.Serialize(ref writer,value.Operand,Resolver);
        Type.Instance.Serialize(ref writer,value.Type,Resolver);
    }
    internal static void InternalSerializeOperandMethod(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(3);
        writer.WriteNodeType(value.NodeType);
        Expression.Instance.Serialize(ref writer,value.Operand,Resolver);
        Method.InternalSerializeNullable(ref writer,value.Method,Resolver);
    }
    internal static void InternalSerializeOperandTypeMethod(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(4);
        writer.WriteNodeType(value.NodeType);
        Expression.Instance.Serialize(ref writer,value.Operand,Resolver);
        Type.Instance.Serialize(ref writer,value.Type,Resolver);
        Method.InternalSerializeNullable(ref writer,value.Method,Resolver);
    }
    //internal readonly object[] Objects2=new object[2];
    internal static Expressions.Expression InternalDeserialize(ref Reader reader,MessagePackSerializerOptions Resolver)=>
        Expression.Instance.Deserialize(ref reader,Resolver);
    internal static (Expressions.Expression Operand,System.Type Type)InternalDeserializeType(ref Reader reader,MessagePackSerializerOptions Resolver){
        var operand= Expression.Instance.Deserialize(ref reader,Resolver);
        var type=reader.ReadType();
        return(operand,type);
    }
    internal static (Expressions.Expression Operand,MethodInfo? Method)InternalDeserializeMethod(ref Reader reader,MessagePackSerializerOptions Resolver){
        var operand= Expression.Instance.Deserialize(ref reader,Resolver);
        var method=Method.InternalDeserializeNullable(ref reader,Resolver);
        return(operand,method);
    }
    internal static (Expressions.Expression Operand,System.Type Type,MethodInfo? Method)InternalDeserializeTypeMethod(ref Reader reader,MessagePackSerializerOptions Resolver){
        var operand= Expression.Instance.Deserialize(ref reader,Resolver);
        var type=reader.ReadType();
        var method=Method.InternalDeserializeNullable(ref reader,Resolver);
        return(operand,type,method);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        //if(writer.TryWriteNil(value)) return;
        //writer.WriteNodeType(value!.NodeType);
        switch(value!.NodeType){
            case Expressions.ExpressionType.ArrayLength        :
            case Expressions.ExpressionType.Quote              :InternalSerializeOperand(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Throw              :
            case Expressions.ExpressionType.TypeAs             :
            case Expressions.ExpressionType.Unbox              :InternalSerializeOperandType(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Convert            :
            case Expressions.ExpressionType.ConvertChecked     :InternalSerializeOperandTypeMethod(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Decrement          :
            case Expressions.ExpressionType.Increment          :
            case Expressions.ExpressionType.IsFalse            :
            case Expressions.ExpressionType.IsTrue             :
            case Expressions.ExpressionType.Negate             :
            case Expressions.ExpressionType.NegateChecked      :
            case Expressions.ExpressionType.Not                :
            case Expressions.ExpressionType.OnesComplement     :
            case Expressions.ExpressionType.PostDecrementAssign:
            case Expressions.ExpressionType.PostIncrementAssign:
            case Expressions.ExpressionType.PreDecrementAssign :
            case Expressions.ExpressionType.PreIncrementAssign :
            case Expressions.ExpressionType.UnaryPlus          :InternalSerializeOperandMethod(ref writer,value,Resolver);break;
            default:
                throw new NotSupportedException(value.NodeType.ToString());
        }
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        //if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(2<=count&&count<=4);
        T value;
        var NodeType=reader.ReadNodeType();
        switch(NodeType){
            case Expressions.ExpressionType.ArrayLength: {
                var Operand = InternalDeserialize(ref reader,Resolver);
                value=Expressions.Expression.ArrayLength(Operand); break;
            }
            case Expressions.ExpressionType.Quote: {
                var Operand = InternalDeserialize(ref reader,Resolver);
                value=Expressions.Expression.Quote(Operand); break;
            }
            case Expressions.ExpressionType.Convert:{
                var (Operand, Type, Method)=InternalDeserializeTypeMethod(ref reader,Resolver);
                value=Expressions.Expression.Convert(Operand,Type,Method); break;
            }
            case Expressions.ExpressionType.ConvertChecked: {
                var (Operand, Type, Method)=InternalDeserializeTypeMethod(ref reader,Resolver);
                value=Expressions.Expression.ConvertChecked(Operand,Type,Method); break;
            }
            case Expressions.ExpressionType.Decrement: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Decrement(Operand,Method); break;
            }
            case Expressions.ExpressionType.Increment: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Increment(Operand,Method); break;
            }
            case Expressions.ExpressionType.IsFalse: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.IsFalse(Operand,Method); break;
            }
            case Expressions.ExpressionType.IsTrue: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.IsTrue(Operand,Method); break;
            }
            case Expressions.ExpressionType.Negate: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Negate(Operand,Method); break;
            }
            case Expressions.ExpressionType.NegateChecked: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.NegateChecked(Operand,Method); break;
            }
            case Expressions.ExpressionType.Not: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Not(Operand,Method); break;
            }
            case Expressions.ExpressionType.OnesComplement: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.OnesComplement(Operand,Method); break;
            }
            case Expressions.ExpressionType.PostDecrementAssign: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.PostDecrementAssign(Operand,Method); break;
            }
            case Expressions.ExpressionType.PostIncrementAssign: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.PostIncrementAssign(Operand,Method); break;
            }
            case Expressions.ExpressionType.PreDecrementAssign: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.PreDecrementAssign(Operand,Method); break;
            }
            case Expressions.ExpressionType.PreIncrementAssign: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.PreIncrementAssign(Operand,Method); break;
            }
            case Expressions.ExpressionType.UnaryPlus: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.UnaryPlus(Operand,Method); break;
            }
            case Expressions.ExpressionType.Throw: {
                var (Operand, Type)=InternalDeserializeType(ref reader,Resolver);
                value=Expressions.Expression.Throw(Operand,Type); break;
            }
            case Expressions.ExpressionType.TypeAs: {
                var (Operand, Type)=InternalDeserializeType(ref reader,Resolver);
                value=Expressions.Expression.TypeAs(Operand,Type); break;
            }
            case Expressions.ExpressionType.Unbox: {
                var (Operand, Type)=InternalDeserializeType(ref reader,Resolver);
                value=Expressions.Expression.Unbox(Operand,Type); break;
            }
            default:throw new NotSupportedException(NodeType.ToString());
        }
        return value;
    }
}
