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
    internal static void Write(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(2);
        writer.WriteNodeType(value.NodeType);
        Expression.Instance.Serialize(ref writer,value.Operand,Resolver);
    }
    internal static void WriteType(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(3);
        writer.WriteNodeType(value.NodeType);
        Expression.Instance.Serialize(ref writer,value.Operand,Resolver);
        Type.Instance.Serialize(ref writer,value.Type,Resolver);
    }
    internal static void WriteMethod(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(3);
        writer.WriteNodeType(value.NodeType);
        Expression.Instance.Serialize(ref writer,value.Operand,Resolver);
        Method.InternalSerializeNullable(ref writer,value.Method,Resolver);
    }
    internal static void WriteTypeMethod(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(4);
        writer.WriteNodeType(value.NodeType);
        Expression.Instance.Serialize(ref writer,value.Operand,Resolver);
        Type.Instance.Serialize(ref writer,value.Type,Resolver);
        Method.InternalSerializeNullable(ref writer,value.Method,Resolver);
    }
    internal static Expressions.Expression InternalDeserialize(ref Reader reader,MessagePackSerializerOptions Resolver)=>
        Expression.Instance.Deserialize(ref reader,Resolver);
    internal static (Expressions.Expression Operand,System.Type Type)InternalDeserializeType(ref Reader reader,MessagePackSerializerOptions Resolver){
        var operand= Expression.Instance.Deserialize(ref reader,Resolver);
        var type=reader.ReadType();
        return(operand,type);
    }
    internal static (Expressions.Expression Operand,MethodInfo? Method)InternalDeserializeMethod(ref Reader reader,MessagePackSerializerOptions Resolver){
        var operand= Expression.Instance.Deserialize(ref reader,Resolver);
        var method=Method.ReadNullable(ref reader,Resolver);
        return(operand,method);
    }
    internal static (Expressions.Expression Operand,System.Type Type,MethodInfo? Method)InternalDeserializeTypeMethod(ref Reader reader,MessagePackSerializerOptions Resolver){
        var operand= Expression.Instance.Deserialize(ref reader,Resolver);
        var type=reader.ReadType();
        var method=Method.ReadNullable(ref reader,Resolver);
        return(operand,type,method);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        //if(writer.TryWriteNil(value)) return;
        //writer.WriteNodeType(value!.NodeType);
        switch(value!.NodeType){
            case Expressions.ExpressionType.ArrayLength        :
            case Expressions.ExpressionType.Quote              :Write(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Throw              :
            case Expressions.ExpressionType.TypeAs             :
            case Expressions.ExpressionType.Unbox              :WriteType(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Convert            :
            case Expressions.ExpressionType.ConvertChecked     :WriteTypeMethod(ref writer,value,Resolver);break;
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
            case Expressions.ExpressionType.UnaryPlus          :WriteMethod(ref writer,value,Resolver);break;
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
                var operand = InternalDeserialize(ref reader,Resolver);
                value=Expressions.Expression.ArrayLength(operand); break;
            }
            case Expressions.ExpressionType.Quote: {
                var operand = InternalDeserialize(ref reader,Resolver);
                value=Expressions.Expression.Quote(operand); break;
            }
            case Expressions.ExpressionType.Convert:{
                var (operand, Type, method)=InternalDeserializeTypeMethod(ref reader,Resolver);
                value=Expressions.Expression.Convert(operand,Type,method); break;
            }
            case Expressions.ExpressionType.ConvertChecked: {
                var (operand, Type, method)=InternalDeserializeTypeMethod(ref reader,Resolver);
                value=Expressions.Expression.ConvertChecked(operand,Type,method); break;
            }
            case Expressions.ExpressionType.Decrement: {
                var (operand, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Decrement(operand,method); break;
            }
            case Expressions.ExpressionType.Increment: {
                var (operand, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Increment(operand,method); break;
            }
            case Expressions.ExpressionType.IsFalse: {
                var (operand, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.IsFalse(operand,method); break;
            }
            case Expressions.ExpressionType.IsTrue: {
                var (operand, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.IsTrue(operand,method); break;
            }
            case Expressions.ExpressionType.Negate: {
                var (operand, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Negate(operand,method); break;
            }
            case Expressions.ExpressionType.NegateChecked: {
                var (operand, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.NegateChecked(operand,method); break;
            }
            case Expressions.ExpressionType.Not: {
                var (operand, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Not(operand,method); break;
            }
            case Expressions.ExpressionType.OnesComplement: {
                var (operand, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.OnesComplement(operand,method); break;
            }
            case Expressions.ExpressionType.PostDecrementAssign: {
                var (operand, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.PostDecrementAssign(operand,method); break;
            }
            case Expressions.ExpressionType.PostIncrementAssign: {
                var (operand, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.PostIncrementAssign(operand,method); break;
            }
            case Expressions.ExpressionType.PreDecrementAssign: {
                var (operand, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.PreDecrementAssign(operand,method); break;
            }
            case Expressions.ExpressionType.PreIncrementAssign: {
                var (operand, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.PreIncrementAssign(operand,method); break;
            }
            case Expressions.ExpressionType.UnaryPlus: {
                var (operand, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.UnaryPlus(operand,method); break;
            }
            case Expressions.ExpressionType.Throw: {
                var (operand, Type)=InternalDeserializeType(ref reader,Resolver);
                value=Expressions.Expression.Throw(operand,Type); break;
            }
            case Expressions.ExpressionType.TypeAs: {
                var (operand, Type)=InternalDeserializeType(ref reader,Resolver);
                value=Expressions.Expression.TypeAs(operand,Type); break;
            }
            case Expressions.ExpressionType.Unbox: {
                var (operand, Type)=InternalDeserializeType(ref reader,Resolver);
                value=Expressions.Expression.Unbox(operand,Type); break;
            }
            default:throw new NotSupportedException(NodeType.ToString());
        }
        return value;
    }
}
