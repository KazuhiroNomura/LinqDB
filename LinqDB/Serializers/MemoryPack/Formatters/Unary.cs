using System;
using System.Reflection;
using MemoryPack;
using System.Buffers;
using Expressions=System.Linq.Expressions;
using LinqDB.Serializers.MessagePack;
using MessagePack;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.UnaryExpression;
using C=MemoryPackCustomSerializer;

public class Unary:MemoryPackFormatter<T> {
    public static readonly Unary Instance=new();
    public static void Serialize_Unary<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression value) where TBufferWriter :IBufferWriter<byte> {
        var Unary = (T)value;
        Expression.Instance.Serialize(ref writer,Unary.Operand);
    }
    public static void Serialize_Unary_Type<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression value) where TBufferWriter :IBufferWriter<byte> {
        var Unary = (T)value;
        Expression.Instance.Serialize(ref writer,Unary.Operand);
        Type.Instance.Serialize(ref writer,Unary.Type);
    }
    public static void Serialize_Unary_MethodInfo<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression value) where TBufferWriter :IBufferWriter<byte> {
        var Unary = (T)value;
        Expression.Instance.Serialize(ref writer,Unary.Operand);
        Method.Instance.SerializeNullable(ref writer,Unary.Method);
    }
    public static void Serialize_Unary_Type_MethodInfo<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression value) where TBufferWriter :IBufferWriter<byte> {
        var Unary = (T)value;
        Expression.Instance.Serialize(ref writer,Unary.Operand);
        Type.Instance.Serialize(ref writer,Unary.Type);
        Method.Instance.SerializeNullable(ref writer,Unary.Method);
    }
    public static Expressions.Expression Deserialize_Unary(ref MemoryPackReader reader) => Expression.Instance.Deserialize(ref reader);
    public static (Expressions.Expression Operand, System.Type Type) Deserialize_Unary_Type(ref MemoryPackReader reader) {
        var operand = Expression.Instance.Deserialize(ref reader);
        var type = Type.Instance.Deserialize(ref reader);
        return (operand, type);
    }
    public static (Expressions.Expression Operand, MethodInfo? Method) Deserialize_Unary_MethodInfo(ref MemoryPackReader reader) {
        var operand = Expression.Instance.Deserialize(ref reader);
        var method = Method.Instance.DeserializeNullable(ref reader);
        return (operand, method);
    }
    public static (Expressions.Expression Operand, System.Type Type, MethodInfo? Method) Deserialize_Unary_Type_MethodInfo(ref MemoryPackReader reader) {
        var operand = Expression.Instance.Deserialize(ref reader);
        var type = Type.Instance.Deserialize(ref reader);
        var method = Method.Instance.DeserializeNullable(ref reader);
        return (operand, type, method);
    }
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal T Deserialize(ref MemoryPackReader reader){
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value)/*where TBufferWriter : class, IBufferWriter<byte>*/{
       if(value is null){
           return;
       }
       writer.WriteNodeType(value.NodeType);
       switch(value.NodeType){
           case Expressions.ExpressionType.ArrayLength        : Serialize_Unary(ref writer,value);break;
           case Expressions.ExpressionType.Quote              : Serialize_Unary(ref writer,value);break;
           case Expressions.ExpressionType.Throw              : Serialize_Unary_Type(ref writer,value);break;
           case Expressions.ExpressionType.TypeAs             : Serialize_Unary_Type(ref writer,value);break;
           case Expressions.ExpressionType.Unbox              : Serialize_Unary_Type(ref writer,value);break;
           case Expressions.ExpressionType.Convert            : Serialize_Unary_Type_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.ConvertChecked     : Serialize_Unary_Type_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.Decrement          : Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.Increment          : Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.IsFalse            : Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.IsTrue             : Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.Negate             : Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.NegateChecked      : Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.Not                : Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.OnesComplement     : Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.PostDecrementAssign: Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.PostIncrementAssign: Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.PreDecrementAssign : Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.PreIncrementAssign : Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.UnaryPlus          : Serialize_Unary_MethodInfo(ref writer,value);break;
           default:
               throw new NotSupportedException(value.NodeType.ToString());
       }
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref T? value){
       //if(reader.TryReadNil()) return;
       var NodeType=reader.ReadNodeType();
       switch(NodeType){
            case Expressions.ExpressionType.ArrayLength        :
            case Expressions.ExpressionType.Quote              :{
                var Operand= Unary.Deserialize_Unary(ref reader);
                value=Expressions.Expression.ArrayLength(Operand);break;
            }
            case Expressions.ExpressionType.Throw              :
            case Expressions.ExpressionType.TypeAs             :
            case Expressions.ExpressionType.Unbox              :{
                var (Operand,Type)=Unary.Deserialize_Unary_Type(ref reader);
                value=Expressions.Expression.MakeUnary(NodeType,Operand,Type);break;
            }
            case Expressions.ExpressionType.Convert            :
            case Expressions.ExpressionType.ConvertChecked     :{
                var(Operand,Type,Method)=Unary.Deserialize_Unary_Type_MethodInfo(ref reader);
                value=Expressions.Expression.ConvertChecked(Operand,Type,Method);break;
            }
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
            case Expressions.ExpressionType.UnaryPlus          :{
                var (Operand,Method)=Unary.Deserialize_Unary_MethodInfo(ref reader);
                value=Expressions.Expression.PreIncrementAssign(Operand,Method);break;
            }
           //case Expressions.ExpressionType.ArrayLength        :{
           //    var Operand= Deserialize_Unary(ref reader);
           //    value=Expressions.Expression.ArrayLength(Operand);break;
           //}
           //case Expressions.ExpressionType.Quote:{
           //    var Operand= Deserialize_Unary(ref reader);
           //    value=Expressions.Expression.Quote(Operand);break;
           //}
           //case Expressions.ExpressionType.Convert            :
           //case Expressions.ExpressionType.ConvertChecked     :{
           //    var(Operand,Type,Method)=Deserialize_Unary_Type_MethodInfo(ref reader);
           //    value=Expressions.Expression.MakeUnary(NodeType,Operand,Type,Method);break;
           //}
           //case Expressions.ExpressionType.Decrement          :{
           //    var (Operand,Method)=Deserialize_Unary_MethodInfo(ref reader);
           //    value=Expressions.Expression.Decrement(Operand,Method);break;
           //}
           //case Expressions.ExpressionType.Increment          :{
           //    var (Operand,Method)=Deserialize_Unary_MethodInfo(ref reader);
           //    value=Expressions.Expression.Increment(Operand,Method);break;
           //}
           //case Expressions.ExpressionType.IsFalse            :{
           //    var (Operand,Method)=Deserialize_Unary_MethodInfo(ref reader);
           //    value=Expressions.Expression.IsFalse(Operand,Method);break;
           //}
           //case Expressions.ExpressionType.IsTrue             :{
           //    var (Operand,Method)=Deserialize_Unary_MethodInfo(ref reader);
           //    value=Expressions.Expression.IsTrue(Operand,Method);break;
           //}
           //case Expressions.ExpressionType.Negate             :{
           //    var (Operand,Method)=Deserialize_Unary_MethodInfo(ref reader);
           //    value=Expressions.Expression.Negate(Operand,Method);break;
           //}
           //case Expressions.ExpressionType.NegateChecked      :{
           //    var (Operand,Method)=Deserialize_Unary_MethodInfo(ref reader);
           //    value=Expressions.Expression.NegateChecked(Operand,Method);break;
           //}
           //case Expressions.ExpressionType.Not                :{
           //    var (Operand,Method)=Deserialize_Unary_MethodInfo(ref reader);
           //    value=Expressions.Expression.Not(Operand,Method);break;
           //}
           //case Expressions.ExpressionType.OnesComplement     :{
           //    var (Operand,Method)=Deserialize_Unary_MethodInfo(ref reader);
           //    value=Expressions.Expression.OnesComplement(Operand,Method);break;
           //}
           //case Expressions.ExpressionType.PostDecrementAssign:{
           //    var (Operand,Method)=Deserialize_Unary_MethodInfo(ref reader);
           //    value=Expressions.Expression.PostDecrementAssign(Operand,Method);break;
           //}
           //case Expressions.ExpressionType.PostIncrementAssign:{
           //    var (Operand,Method)=Deserialize_Unary_MethodInfo(ref reader);
           //    value=Expressions.Expression.PostIncrementAssign(Operand,Method);break;
           //}
           //case Expressions.ExpressionType.PreDecrementAssign :{
           //    var (Operand,Method)=Deserialize_Unary_MethodInfo(ref reader);
           //    value=Expressions.Expression.PreDecrementAssign(Operand,Method);break;
           //}
           //case Expressions.ExpressionType.PreIncrementAssign :{
           //    var (Operand,Method)=Deserialize_Unary_MethodInfo(ref reader);
           //    value=Expressions.Expression.PreIncrementAssign(Operand,Method);break;
           //}
           //case Expressions.ExpressionType.UnaryPlus:{
           //    var (Operand,Method)=Deserialize_Unary_MethodInfo(ref reader);
           //    value=Expressions.Expression.UnaryPlus(Operand,Method);break;
           //}
           //case Expressions.ExpressionType.Throw              :{
           //    var (Operand,Type)=Deserialize_Unary_Type(ref reader);
           //    value=Expressions.Expression.Throw(Operand,Type);break;
           //}
           //case Expressions.ExpressionType.TypeAs             :{
           //    var (Operand,Type)=Deserialize_Unary_Type(ref reader);
           //    value=Expressions.Expression.TypeAs(Operand,Type);break;
           //}
           //case Expressions.ExpressionType.Unbox              :{
           //    var (Operand,Type)=Deserialize_Unary_Type(ref reader);
           //    value=Expressions.Expression.Unbox(Operand,Type);break;
           //}
           default:throw new NotSupportedException(NodeType.ToString());
       }
    }
}
