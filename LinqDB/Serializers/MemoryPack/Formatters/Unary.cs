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
        Expression.Serialize(ref writer,value.Operand);
    }
    internal static void InternalSerializeType<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter :IBufferWriter<byte> {
        Expression.Serialize(ref writer,value.Operand);
        Type.Serialize(ref writer,value.Type);
    }
    internal static void InternalSerializeMethod<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter :IBufferWriter<byte> {
        Expression.Serialize(ref writer,value.Operand);
        Method.SerializeNullable(ref writer,value.Method);
    }
    internal static void InternalSerializeOperandTypeMethod<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter :IBufferWriter<byte> {
        Expression.Serialize(ref writer,value.Operand);
        Type.Serialize(ref writer,value.Type);
        Method.SerializeNullable(ref writer,value.Method);
    }
    internal static Expressions.Expression InternalDeserialize(ref Reader reader) => Expression.Deserialize(ref reader);
    internal static (Expressions.Expression Operand, System.Type Type) InternalDeserializeType(ref Reader reader) {
        var operand = Expression.Deserialize(ref reader);
        var type = Type.Deserialize(ref reader);
        return (operand, type);
    }
    internal static (Expressions.Expression  Operand, MethodInfo? Method) InternalDeserializeMethod(ref Reader reader) {
        var operand = Expression.Deserialize(ref reader);
        var method = Method.DeserializeNullable(ref reader);
        return (operand, method);
    }
    internal static (Expressions.Expression Operand, System.Type Type, MethodInfo? Method) InternalDeserializeTypeMethod(ref Reader reader) {
        var operand = Expression.Deserialize(ref reader);
        var type = Type.Deserialize(ref reader);
        var method = Method.DeserializeNullable(ref reader);
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
           case Expressions.ExpressionType.ConvertChecked     : InternalSerializeOperandTypeMethod(ref writer,value);break;
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
       //if(reader.TryReadNil()) return;
       var NodeType=reader.ReadNodeType();
       switch(NodeType){
            //case Expressions.ExpressionType.ArrayLength        :
            //case Expressions.ExpressionType.Quote              :{
            //    var Operand= Unary.Deserialize_Unary(ref reader);
            //    value=Expressions.Expression.MakeUnary(NodeType,Operand,typeof(void));break;
            //    Expressions.Expression.Quote()
            //}
            //case Expressions.ExpressionType.Throw              :
            //case Expressions.ExpressionType.TypeAs             :
            //case Expressions.ExpressionType.Unbox              :{
            //    var (Operand,Type)=Unary.Deserialize_Unary_Type(ref reader);
            //    value=Expressions.Expression.MakeUnary(NodeType,Operand,Type);break;
            //}
            //case Expressions.ExpressionType.Convert            :
            //case Expressions.ExpressionType.ConvertChecked     :{
            //    var(Operand,Type,Method)=Unary.Deserialize_Unary_Type_MethodInfo(ref reader);
            //    value=Expressions.Expression.MakeUnary(NodeType,Operand,Type,Method);break;
            //}
            //case Expressions.ExpressionType.Decrement          :
            //case Expressions.ExpressionType.Increment          :
            //case Expressions.ExpressionType.IsFalse            :
            //case Expressions.ExpressionType.IsTrue             :
            //case Expressions.ExpressionType.Negate             :
            //case Expressions.ExpressionType.NegateChecked      :
            //case Expressions.ExpressionType.Not                :
            //case Expressions.ExpressionType.OnesComplement     :
            //case Expressions.ExpressionType.PostDecrementAssign:
            //case Expressions.ExpressionType.PostIncrementAssign:
            //case Expressions.ExpressionType.PreDecrementAssign :
            //case Expressions.ExpressionType.PreIncrementAssign :
            //case Expressions.ExpressionType.UnaryPlus          :{
            //    var (Operand,Method)=Unary.Deserialize_Unary_MethodInfo(ref reader);
            //    value=Expressions.Expression.MakeUnary(NodeType,Operand,Operand.Type,Method);break;
            //}
            case Expressions.ExpressionType.ArrayLength: {
                var Operand = InternalDeserialize(ref reader);
                value=Expressions.Expression.ArrayLength(Operand); break;
            }
            case Expressions.ExpressionType.Quote: {
                var Operand = InternalDeserialize(ref reader);
                value=Expressions.Expression.Quote(Operand); break;
            }
            case Expressions.ExpressionType.Convert:{
                var (Operand, Type, Method)=InternalDeserializeTypeMethod(ref reader);
                value=Expressions.Expression.Convert(Operand,Type,Method); break;
            }
            case Expressions.ExpressionType.ConvertChecked: {
                var (Operand, Type, Method)=InternalDeserializeTypeMethod(ref reader);
                value=Expressions.Expression.ConvertChecked(Operand,Type,Method); break;
            }
            case Expressions.ExpressionType.Decrement: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.Decrement(Operand,Method); break;
            }
            case Expressions.ExpressionType.Increment: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.Increment(Operand,Method); break;
            }
            case Expressions.ExpressionType.IsFalse: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.IsFalse(Operand,Method); break;
            }
            case Expressions.ExpressionType.IsTrue: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.IsTrue(Operand,Method); break;
            }
            case Expressions.ExpressionType.Negate: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.Negate(Operand,Method); break;
            }
            case Expressions.ExpressionType.NegateChecked: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.NegateChecked(Operand,Method); break;
            }
            case Expressions.ExpressionType.Not: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.Not(Operand,Method); break;
            }
            case Expressions.ExpressionType.OnesComplement: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.OnesComplement(Operand,Method); break;
            }
            case Expressions.ExpressionType.PostDecrementAssign: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.PostDecrementAssign(Operand,Method); break;
            }
            case Expressions.ExpressionType.PostIncrementAssign: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.PostIncrementAssign(Operand,Method); break;
            }
            case Expressions.ExpressionType.PreDecrementAssign: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.PreDecrementAssign(Operand,Method); break;
            }
            case Expressions.ExpressionType.PreIncrementAssign: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.PreIncrementAssign(Operand,Method); break;
            }
            case Expressions.ExpressionType.UnaryPlus: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.UnaryPlus(Operand,Method); break;
            }
            case Expressions.ExpressionType.Throw: {
                var (Operand, Type)=InternalDeserializeType(ref reader);
                value=Expressions.Expression.Throw(Operand,Type); break;
            }
            case Expressions.ExpressionType.TypeAs: {
                var (Operand, Type)=InternalDeserializeType(ref reader);
                value=Expressions.Expression.TypeAs(Operand,Type); break;
            }
            case Expressions.ExpressionType.Unbox: {
                var (Operand, Type)=InternalDeserializeType(ref reader);
                value=Expressions.Expression.Unbox(Operand,Type); break;
            }
            default:throw new NotSupportedException(NodeType.ToString());
       }
    }
}
