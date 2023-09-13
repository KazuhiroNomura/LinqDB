using System;
using System.Reflection;
using MemoryPack;
using System.Buffers;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.UnaryExpression;
using C=Serializer;

public class Unary:MemoryPackFormatter<T> {
    public static readonly Unary Instance=new();
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter :IBufferWriter<byte> {
        Expression.InternalSerialize(ref writer,value.Operand);
    }
    internal static void InternalSerializeType<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter :IBufferWriter<byte> {
        Expression.InternalSerialize(ref writer,value.Operand);
        Type.Serialize(ref writer,value.Type);
    }
    internal static void InternalSerializeMethod<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter :IBufferWriter<byte> {
        Expression.InternalSerialize(ref writer,value.Operand);
        Method.InternalSerializeNullable(ref writer,value.Method);
    }
    internal static void InternalSerializeTypeMethod<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter :IBufferWriter<byte> {
        Expression.InternalSerialize(ref writer,value.Operand);
        Type.Serialize(ref writer,value.Type);
        Method.InternalSerializeNullable(ref writer,value.Method);
    }
    internal static Expressions.Expression InternalDeserialize(ref Reader reader) => Expression.InternalDeserialize(ref reader);
    internal static (Expressions.Expression Operand, System.Type Type) InternalDeserializeType(ref Reader reader) {
        var operand = Expression.InternalDeserialize(ref reader);
        var type = Type.Deserialize(ref reader);
        return (operand, type);
    }
    internal static (Expressions.Expression  Operand, MethodInfo? Method) InternalDeserializeMethod(ref Reader reader) {
        var operand = Expression.InternalDeserialize(ref reader);
        var method = Method.InternalDeserializeNullable(ref reader);
        return (operand, method);
    }
    internal static (Expressions.Expression Operand, System.Type Type, MethodInfo? Method) InternalDeserializeTypeMethod(ref Reader reader) {
        var operand = Expression.InternalDeserialize(ref reader);
        var type = Type.Deserialize(ref reader);
        var method = Method.InternalDeserializeNullable(ref reader);
        return (operand, type, method);
    }
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value)/*where TBufferWriter : class, IBufferWriter<byte>*/{
       if(value is null){
           return;
       }
       writer.WriteNodeType(value.NodeType);
       switch(value.NodeType){
           case Expressions.ExpressionType.ArrayLength        : 
           case Expressions.ExpressionType.Quote              : InternalSerialize(ref writer,value);break;
           case Expressions.ExpressionType.Throw              : 
           case Expressions.ExpressionType.TypeAs             : 
           case Expressions.ExpressionType.Unbox              : InternalSerializeType(ref writer,value);break;
           case Expressions.ExpressionType.Convert            : 
           case Expressions.ExpressionType.ConvertChecked     : InternalSerializeTypeMethod(ref writer,value);break;
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
           case Expressions.ExpressionType.UnaryPlus          : InternalSerializeMethod(ref writer,value);break;
           default:
               throw new NotSupportedException(value.NodeType.ToString());
       }
    }
    internal static T Deserialize(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
       var NodeType=reader.ReadNodeType();
       switch(NodeType){
            case Expressions.ExpressionType.ArrayLength: {
                var operand = InternalDeserialize(ref reader);
                value=Expressions.Expression.ArrayLength(operand); break;
            }
            case Expressions.ExpressionType.Quote: {
                var operand = InternalDeserialize(ref reader);
                value=Expressions.Expression.Quote(operand); break;
            }
            case Expressions.ExpressionType.Convert:{
                var (operand, Type, method)=InternalDeserializeTypeMethod(ref reader);
                value=Expressions.Expression.Convert(operand,Type,method); break;
            }
            case Expressions.ExpressionType.ConvertChecked: {
                var (operand, Type, method)=InternalDeserializeTypeMethod(ref reader);
                value=Expressions.Expression.ConvertChecked(operand,Type,method); break;
            }
            case Expressions.ExpressionType.Decrement: {
                var (operand, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.Decrement(operand,method); break;
            }
            case Expressions.ExpressionType.Increment: {
                var (operand, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.Increment(operand,method); break;
            }
            case Expressions.ExpressionType.IsFalse: {
                var (operand, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.IsFalse(operand,method); break;
            }
            case Expressions.ExpressionType.IsTrue: {
                var (operand, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.IsTrue(operand,method); break;
            }
            case Expressions.ExpressionType.Negate: {
                var (operand, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.Negate(operand,method); break;
            }
            case Expressions.ExpressionType.NegateChecked: {
                var (operand, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.NegateChecked(operand,method); break;
            }
            case Expressions.ExpressionType.Not: {
                var (operand, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.Not(operand,method); break;
            }
            case Expressions.ExpressionType.OnesComplement: {
                var (operand, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.OnesComplement(operand,method); break;
            }
            case Expressions.ExpressionType.PostDecrementAssign: {
                var (operand, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.PostDecrementAssign(operand,method); break;
            }
            case Expressions.ExpressionType.PostIncrementAssign: {
                var (operand, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.PostIncrementAssign(operand,method); break;
            }
            case Expressions.ExpressionType.PreDecrementAssign: {
                var (operand, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.PreDecrementAssign(operand,method); break;
            }
            case Expressions.ExpressionType.PreIncrementAssign: {
                var (operand, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.PreIncrementAssign(operand,method); break;
            }
            case Expressions.ExpressionType.UnaryPlus: {
                var (operand, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.UnaryPlus(operand,method); break;
            }
            case Expressions.ExpressionType.Throw: {
                var (operand, Type)=InternalDeserializeType(ref reader);
                value=Expressions.Expression.Throw(operand,Type); break;
            }
            case Expressions.ExpressionType.TypeAs: {
                var (operand, Type)=InternalDeserializeType(ref reader);
                value=Expressions.Expression.TypeAs(operand,Type); break;
            }
            case Expressions.ExpressionType.Unbox: {
                var (operand, Type)=InternalDeserializeType(ref reader);
                value=Expressions.Expression.Unbox(operand,Type); break;
            }
            default:throw new NotSupportedException(NodeType.ToString());
       }
    }
}
