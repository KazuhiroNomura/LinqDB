using System;
using System.Buffers;
using System.Diagnostics;
using Expressions = System.Linq.Expressions;
using System.Reflection;
using MemoryPack;
using LinqDB.Serializers.MessagePack;
using MessagePack;
using System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;

using T= BinaryExpression;
//using Writer=MessagePackWriter;
public class Binary:MemoryPackFormatter<T> {
    public static readonly Binary Instance=new();
    internal static void InternalSerializeBinary<TBufferWriter>(ref MemoryPackWriter<TBufferWriter>writer,T value)where TBufferWriter:IBufferWriter<byte>{

        Expression.Instance.Serialize(ref writer,value.Left);
        Expression.Instance.Serialize(ref writer,value.Right);
    }
    internal static void InternalSerializeBinaryMethod<TBufferWriter>(ref MemoryPackWriter<TBufferWriter>writer,T value)where TBufferWriter:IBufferWriter<byte>{
        Expression.Instance.Serialize(ref writer,value.Left);
        Expression.Instance.Serialize(ref writer,value.Right);
        Method.Instance.SerializeNullable(ref writer,value.Method);
    }
    internal static void InternalSerializeBinaryBooleanMethod<TBufferWriter>(ref MemoryPackWriter<TBufferWriter>writer,T value)where TBufferWriter:IBufferWriter<byte>{
        Expression.Instance.Serialize(ref writer,value.Left);
        Expression.Instance.Serialize(ref writer,value.Right);
        writer.WriteBoolean(value.IsLiftedToNull);
        Method.Instance.SerializeNullable(ref writer,value.Method);
    }
    internal static (Expressions.Expression Left, Expressions.Expression Right) InternalDeserializeBinary(ref Reader reader) {
        var Instance = Expression.Instance;
        var Left = Instance.Deserialize(ref reader);
        var Right = Instance.Deserialize(ref reader);
        Debug.Assert(Left!=null);
        return (Left, Right);
    }
    internal static (Expressions.Expression Left, Expressions.Expression Right, MethodInfo? Method) InternalDeserializeBinaryMethod(ref Reader reader) {
        var Instance = Expression.Instance;
        var Left = Instance.Deserialize(ref reader);
        var Right = Instance.Deserialize(ref reader);
        var method = Method.Instance.DeserializeNullable(ref reader);
        return (Left, Right, method);
    }
    internal static (Expressions.Expression Left, Expressions.Expression Right, bool IsLiftedToNull, MethodInfo? Method) InternalDeserializeBinaryBooleanMethod(ref Reader reader){
        var Instance = Expression.Instance;
        var Left = Instance.Deserialize(ref reader);
        var Right = Instance.Deserialize(ref reader);
        var IsLiftedToNull =reader.ReadBoolean();
        var method = Method.Instance.DeserializeNullable(ref reader);
        return (Left, Right, IsLiftedToNull, method);
    }
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> => this.Serialize(ref writer,ref value);
    internal T Deserialize(ref Reader reader) {
        T? value = default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        //writer.WriteArrayHeader
        writer.WriteNodeType(value.NodeType);
        switch(value.NodeType) {
            case Expressions.ExpressionType.Assign               :
            case Expressions.ExpressionType.Coalesce             :
            case Expressions.ExpressionType.ArrayIndex           :InternalSerializeBinary(ref writer,value); break;
            case Expressions.ExpressionType.Add                  :
            case Expressions.ExpressionType.AddAssign            :
            case Expressions.ExpressionType.AddAssignChecked     :
            case Expressions.ExpressionType.AddChecked           :
            case Expressions.ExpressionType.And                  :
            case Expressions.ExpressionType.AndAssign            :
            case Expressions.ExpressionType.AndAlso              :
            case Expressions.ExpressionType.Divide               :
            case Expressions.ExpressionType.DivideAssign         :
            case Expressions.ExpressionType.ExclusiveOr          :
            case Expressions.ExpressionType.ExclusiveOrAssign    :
            case Expressions.ExpressionType.LeftShift            :
            case Expressions.ExpressionType.LeftShiftAssign      :
            case Expressions.ExpressionType.Modulo               :
            case Expressions.ExpressionType.ModuloAssign         :
            case Expressions.ExpressionType.Multiply             :
            case Expressions.ExpressionType.MultiplyAssign       :
            case Expressions.ExpressionType.MultiplyAssignChecked:
            case Expressions.ExpressionType.MultiplyChecked      :
            case Expressions.ExpressionType.Or                   :
            case Expressions.ExpressionType.OrAssign             :
            case Expressions.ExpressionType.OrElse               :
            case Expressions.ExpressionType.Power                :
            case Expressions.ExpressionType.PowerAssign          :
            case Expressions.ExpressionType.RightShift           :
            case Expressions.ExpressionType.RightShiftAssign     :
            case Expressions.ExpressionType.Subtract             :
            case Expressions.ExpressionType.SubtractAssign       :
            case Expressions.ExpressionType.SubtractAssignChecked:
            case Expressions.ExpressionType.SubtractChecked      :InternalSerializeBinaryMethod(ref writer,value); break;
            case Expressions.ExpressionType.Equal                :
            case Expressions.ExpressionType.GreaterThan          :
            case Expressions.ExpressionType.GreaterThanOrEqual   :
            case Expressions.ExpressionType.LessThan             :
            case Expressions.ExpressionType.LessThanOrEqual      :
            case Expressions.ExpressionType.NotEqual             :InternalSerializeBinaryBooleanMethod(ref writer,value);break;
            default:throw new NotSupportedException(value.NodeType.ToString());
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var NodeType=reader.ReadNodeType();
        switch(NodeType) {
            //case Expressions.ExpressionType.Assign:
            //case Expressions.ExpressionType.Coalesce:
            //case Expressions.ExpressionType.ArrayIndex:{
            //    var (Left,Right)=InternalDeserializeBinary(ref reader);
            //    value=Expressions.Expression.MakeBinary(NodeType,Left,Right); break;
            //}
            //case Expressions.ExpressionType.Add:
            //case Expressions.ExpressionType.AddAssign:
            //case Expressions.ExpressionType.AddAssignChecked:
            //case Expressions.ExpressionType.AddChecked:
            //case Expressions.ExpressionType.And:
            //case Expressions.ExpressionType.AndAssign:
            //case Expressions.ExpressionType.AndAlso:
            //case Expressions.ExpressionType.Divide:
            //case Expressions.ExpressionType.DivideAssign:
            //case Expressions.ExpressionType.ExclusiveOr:
            //case Expressions.ExpressionType.ExclusiveOrAssign:
            //case Expressions.ExpressionType.LeftShift:
            //case Expressions.ExpressionType.LeftShiftAssign:
            //case Expressions.ExpressionType.Modulo:
            //case Expressions.ExpressionType.ModuloAssign:
            //case Expressions.ExpressionType.Multiply:
            //case Expressions.ExpressionType.MultiplyAssign:
            //case Expressions.ExpressionType.MultiplyAssignChecked:
            //case Expressions.ExpressionType.MultiplyChecked:
            //case Expressions.ExpressionType.Or:
            //case Expressions.ExpressionType.OrAssign:
            //case Expressions.ExpressionType.OrElse:
            //case Expressions.ExpressionType.Power:
            //case Expressions.ExpressionType.PowerAssign:
            //case Expressions.ExpressionType.RightShift:
            //case Expressions.ExpressionType.RightShiftAssign:
            //case Expressions.ExpressionType.Subtract:
            //case Expressions.ExpressionType.SubtractAssign:
            //case Expressions.ExpressionType.SubtractAssignChecked:
            //case Expressions.ExpressionType.SubtractChecked: {
            //    var (Left, Right, Method)=InternalDeserializeBinaryMethod(ref reader);
            //    value=Expressions.Expression.MakeBinary(NodeType,Left,Right,false,Method); break;
            //}
            //case Expressions.ExpressionType.Equal:
            //case Expressions.ExpressionType.GreaterThan:
            //case Expressions.ExpressionType.GreaterThanOrEqual:
            //case Expressions.ExpressionType.LessThan:
            //case Expressions.ExpressionType.LessThanOrEqual:
            //case Expressions.ExpressionType.NotEqual: {
            //    var (Left, Right, IsLiftedToNull, Method)=InternalDeserializeBinaryBooleanMethod(ref reader);
            //    value=Expressions.Expression.NotEqual(Left,Right,IsLiftedToNull,Method); break;
            //}
            //default: throw new NotSupportedException(NodeType.ToString());
            case Expressions.ExpressionType.ArrayIndex: {
                var (array, index)=Binary.InternalDeserializeBinary(ref reader);
                value=Expressions.Expression.ArrayIndex(array,index); break;
            }
            case Expressions.ExpressionType.Assign: {
                var (Left, Right)=Binary.InternalDeserializeBinary(ref reader);
                value=Expressions.Expression.Assign(Left,Right); break;
            }
            case Expressions.ExpressionType.Coalesce: {
                var (Left, Right)=Binary.InternalDeserializeBinary(ref reader);
                value=Expressions.Expression.Coalesce(Left,Right); break;
            }
            case Expressions.ExpressionType.Add: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.Add(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.AddAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.AddAssign(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.AddAssignChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.AddAssignChecked(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.AddChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.AddChecked(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.And: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.And(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.AndAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.AndAssign(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.AndAlso: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.AndAlso(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.Divide: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.Divide(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.DivideAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.DivideAssign(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.ExclusiveOr: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.ExclusiveOr(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.ExclusiveOrAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.ExclusiveOrAssign(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.LeftShift: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.LeftShift(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.LeftShiftAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.LeftShiftAssign(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.Modulo: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.Modulo(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.ModuloAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.ModuloAssign(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.Multiply: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.Multiply(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.MultiplyAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.MultiplyAssign(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.MultiplyAssignChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.MultiplyAssignChecked(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.MultiplyChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.MultiplyChecked(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.Or: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.Or(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.OrAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.OrAssign(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.OrElse: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.OrElse(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.Power: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.Power(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.PowerAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.PowerAssign(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.RightShift: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.RightShift(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.RightShiftAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.RightShiftAssign(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.Subtract: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.Subtract(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.SubtractAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.SubtractAssign(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.SubtractAssignChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.SubtractAssignChecked(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.SubtractChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeBinaryMethod(ref reader);
                value=Expressions.Expression.SubtractChecked(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.Equal: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBinaryBooleanMethod(ref reader);
                value=Expressions.Expression.Equal(Left,Right,IsLiftedToNull,Method); break;
            }
            case Expressions.ExpressionType.GreaterThan: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBinaryBooleanMethod(ref reader);
                value=Expressions.Expression.GreaterThan(Left,Right,IsLiftedToNull,Method); break;
            }
            case Expressions.ExpressionType.GreaterThanOrEqual: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBinaryBooleanMethod(ref reader);
                value=Expressions.Expression.GreaterThanOrEqual(Left,Right,IsLiftedToNull,Method); break;
            }
            case Expressions.ExpressionType.LessThan: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBinaryBooleanMethod(ref reader);
                value=Expressions.Expression.LessThan(Left,Right,IsLiftedToNull,Method); break;
            }
            case Expressions.ExpressionType.LessThanOrEqual: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBinaryBooleanMethod(ref reader);
                value=Expressions.Expression.LessThanOrEqual(Left,Right,IsLiftedToNull,Method); break;
            }
            case Expressions.ExpressionType.NotEqual: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBinaryBooleanMethod(ref reader);
                value=Expressions.Expression.NotEqual(Left,Right,IsLiftedToNull,Method); break;
            }
            default: throw new NotSupportedException(NodeType.ToString());
        }
    }
}
