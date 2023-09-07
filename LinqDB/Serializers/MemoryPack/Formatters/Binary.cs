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

using T=Expressions.BinaryExpression;
//using Writer=MessagePackWriter;
public class Binary:MemoryPackFormatter<T> {
    public static readonly Binary Instance=new();
    internal static void Serialize_Binary<TBufferWriter>(ref MemoryPackWriter<TBufferWriter>writer,T value)where TBufferWriter:IBufferWriter<byte>{
        Expression.Instance.Serialize(ref writer,value.Left);
        Expression.Instance.Serialize(ref writer,value.Right);
    }
    internal static void Serialize_Binary_MethodInfo<TBufferWriter>(ref MemoryPackWriter<TBufferWriter>writer,T value)where TBufferWriter:IBufferWriter<byte>{
        Expression.Instance.Serialize(ref writer,value.Left);
        Expression.Instance.Serialize(ref writer,value.Right);
        Method.Instance.SerializeNullable(ref writer,value.Method);
    }
    internal static void Serialize_Binary_bool_MethodInfo<TBufferWriter>(ref MemoryPackWriter<TBufferWriter>writer,T value)where TBufferWriter:IBufferWriter<byte>{
        Expression.Instance.Serialize(ref writer,value.Left);
        Expression.Instance.Serialize(ref writer,value.Right);
        writer.WriteBoolean(value.IsLiftedToNull);
        Method.Instance.SerializeNullable(ref writer,value.Method);
    }
    internal static (Expressions.Expression Left, Expressions.Expression Right) Deserialize_Binary(ref Reader reader) {
        var Instance = Expression.Instance;
        var Left = Instance.Deserialize(ref reader);
        var Right = Instance.Deserialize(ref reader);
        Debug.Assert(Left!=null);
        return (Left, Right);
    }
    internal static (Expressions.Expression Left, Expressions.Expression Right, MethodInfo? Method) Deserialize_Binary_MethodInfo(ref Reader reader) {
        var Instance = Expression.Instance;
        var Left = Instance.Deserialize(ref reader);
        var Right = Instance.Deserialize(ref reader);
        var method = Method.Instance.DeserializeNullable(ref reader);
        return (Left, Right, method);
    }
    internal static (Expressions.Expression Left, Expressions.Expression Right, bool IsLiftedToNull, MethodInfo? Method) Deserialize_Binary_bool_MethodInfo(ref Reader reader){
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
        writer.WriteNodeType(value.NodeType);
        switch(value.NodeType) {
            case Expressions.ExpressionType.Assign:
            case Expressions.ExpressionType.Coalesce:
            case Expressions.ExpressionType.ArrayIndex:Serialize_Binary(ref writer,value); break;
            case Expressions.ExpressionType.Add:
            case Expressions.ExpressionType.AddAssign:
            case Expressions.ExpressionType.AddAssignChecked:
            case Expressions.ExpressionType.AddChecked:
            case Expressions.ExpressionType.And:
            case Expressions.ExpressionType.AndAssign:
            case Expressions.ExpressionType.AndAlso:
            case Expressions.ExpressionType.Divide:
            case Expressions.ExpressionType.DivideAssign:
            case Expressions.ExpressionType.ExclusiveOr:
            case Expressions.ExpressionType.ExclusiveOrAssign:
            case Expressions.ExpressionType.LeftShift:
            case Expressions.ExpressionType.LeftShiftAssign:
            case Expressions.ExpressionType.Modulo:
            case Expressions.ExpressionType.ModuloAssign:
            case Expressions.ExpressionType.Multiply:
            case Expressions.ExpressionType.MultiplyAssign:
            case Expressions.ExpressionType.MultiplyAssignChecked:
            case Expressions.ExpressionType.MultiplyChecked:
            case Expressions.ExpressionType.Or:
            case Expressions.ExpressionType.OrAssign:
            case Expressions.ExpressionType.OrElse:
            case Expressions.ExpressionType.Power:
            case Expressions.ExpressionType.PowerAssign:
            case Expressions.ExpressionType.RightShift:
            case Expressions.ExpressionType.RightShiftAssign:
            case Expressions.ExpressionType.Subtract:
            case Expressions.ExpressionType.SubtractAssign:
            case Expressions.ExpressionType.SubtractAssignChecked:
            case Expressions.ExpressionType.SubtractChecked:Serialize_Binary_MethodInfo(ref writer,value); break;
            case Expressions.ExpressionType.Equal:
            case Expressions.ExpressionType.GreaterThan:
            case Expressions.ExpressionType.GreaterThanOrEqual:
            case Expressions.ExpressionType.LessThan:
            case Expressions.ExpressionType.LessThanOrEqual:
            case Expressions.ExpressionType.NotEqual:Serialize_Binary_bool_MethodInfo(ref writer,value);break;
            default:throw new NotSupportedException(value.NodeType.ToString());
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var NodeType=reader.ReadNodeType();
        switch(NodeType) {
            case Expressions.ExpressionType.Assign:
            case Expressions.ExpressionType.Coalesce:
            case Expressions.ExpressionType.ArrayIndex:{
                var (Left,Right)=Deserialize_Binary(ref reader);
                value=Expressions.Expression.MakeBinary(NodeType,Left,Right); break;
            }
            case Expressions.ExpressionType.Add:
            case Expressions.ExpressionType.AddAssign:
            case Expressions.ExpressionType.AddAssignChecked:
            case Expressions.ExpressionType.AddChecked:
            case Expressions.ExpressionType.And:
            case Expressions.ExpressionType.AndAssign:
            case Expressions.ExpressionType.AndAlso:
            case Expressions.ExpressionType.Divide:
            case Expressions.ExpressionType.DivideAssign:
            case Expressions.ExpressionType.ExclusiveOr:
            case Expressions.ExpressionType.ExclusiveOrAssign:
            case Expressions.ExpressionType.LeftShift:
            case Expressions.ExpressionType.LeftShiftAssign:
            case Expressions.ExpressionType.Modulo:
            case Expressions.ExpressionType.ModuloAssign:
            case Expressions.ExpressionType.Multiply:
            case Expressions.ExpressionType.MultiplyAssign:
            case Expressions.ExpressionType.MultiplyAssignChecked:
            case Expressions.ExpressionType.MultiplyChecked:
            case Expressions.ExpressionType.Or:
            case Expressions.ExpressionType.OrAssign:
            case Expressions.ExpressionType.OrElse:
            case Expressions.ExpressionType.Power:
            case Expressions.ExpressionType.PowerAssign:
            case Expressions.ExpressionType.RightShift:
            case Expressions.ExpressionType.RightShiftAssign:
            case Expressions.ExpressionType.Subtract:
            case Expressions.ExpressionType.SubtractAssign:
            case Expressions.ExpressionType.SubtractAssignChecked:
            case Expressions.ExpressionType.SubtractChecked: {
                var (Left, Right, Method)=Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.MakeBinary(NodeType,Left,Right,false,Method); break;
            }
            case Expressions.ExpressionType.Equal:
            case Expressions.ExpressionType.GreaterThan:
            case Expressions.ExpressionType.GreaterThanOrEqual:
            case Expressions.ExpressionType.LessThan:
            case Expressions.ExpressionType.LessThanOrEqual:
            case Expressions.ExpressionType.NotEqual: {
                var (Left, Right, IsLiftedToNull, Method)=Deserialize_Binary_bool_MethodInfo(ref reader);
                value=Expressions.Expression.NotEqual(Left,Right,IsLiftedToNull,Method); break;
            }
            default: throw new NotSupportedException(NodeType.ToString());
        }

    }
}
