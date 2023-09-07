using System;
using Expressions=System.Linq.Expressions;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.BinaryExpression;
using C=MessagePackCustomSerializer;
public class Binary:IMessagePackFormatter<T> {
    public static readonly Binary Instance=new();
    internal static void Serialize_Binary(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        Expression.Instance.Serialize(ref writer,value.Left,Resolver);
        Expression.Instance.Serialize(ref writer,value.Right,Resolver);
    }
    internal static void Serialize_Binary_MethodInfo(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        Expression.Instance.Serialize(ref writer,value.Left,Resolver);
        Expression.Instance.Serialize(ref writer,value.Right,Resolver);
        Method.Instance.Serialize(ref writer,value.Method,Resolver);
    }
    internal static void Serialize_Binary_bool_MethodInfo(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        Expression.Instance.Serialize(ref writer,value.Left,Resolver);
        Expression.Instance.Serialize(ref writer,value.Right,Resolver);
        writer.WriteBoolean(value.IsLiftedToNull);
        Method.Instance.Serialize(ref writer,value.Method,Resolver);
    }
    public static(Expressions.Expression Left,Expressions.Expression Right)Deserialize_Binary(ref Reader reader,MessagePackSerializerOptions Resolver){
        var Left= Expression.Instance.Deserialize(ref reader,Resolver);
        var Right= Expression.Instance.Deserialize(ref reader,Resolver);
        return(Left,Right);
    }
    public static(Expressions.Expression Left,Expressions.Expression Right,MethodInfo Method)Deserialize_Binary_MethodInfo(ref Reader reader,MessagePackSerializerOptions Resolver){
        var Left= Expression.Instance.Deserialize(ref reader,Resolver);
        var Right= Expression.Instance.Deserialize(ref reader,Resolver);
        var method=Method.Instance.Deserialize(ref reader,Resolver);
        return(Left,Right,method);
    }
    public static(Expressions.Expression Left,Expressions.Expression Right,bool IsLiftedToNull,MethodInfo Method)Deserialize_Binary_bool_MethodInfo(ref Reader reader,MessagePackSerializerOptions Resolver){
        var Left= Expression.Instance.Deserialize(ref reader,Resolver);
        var Right= Expression.Instance.Deserialize(ref reader,Resolver);
        var IsLiftedToNull=reader.ReadBoolean();
        var method=Method.Instance.Deserialize(ref reader,Resolver);
        return(Left,Right,IsLiftedToNull,method);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        writer.WriteNodeType(value.NodeType);
        switch(value.NodeType){
            case Expressions.ExpressionType.Assign:
            case Expressions.ExpressionType.Coalesce:
            case Expressions.ExpressionType.ArrayIndex:Serialize_Binary(ref writer,value,Resolver); break;
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
            case Expressions.ExpressionType.SubtractChecked:Serialize_Binary_MethodInfo(ref writer,value,Resolver); break;
            case Expressions.ExpressionType.Equal:
            case Expressions.ExpressionType.GreaterThan:
            case Expressions.ExpressionType.GreaterThanOrEqual:
            case Expressions.ExpressionType.LessThan:
            case Expressions.ExpressionType.LessThanOrEqual:
            case Expressions.ExpressionType.NotEqual:Serialize_Binary_bool_MethodInfo(ref writer,value,Resolver);break;
            default:throw new NotSupportedException(value.NodeType.ToString());
        }
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        if(reader.IsNil) return null!;
        var NodeType=reader.ReadNodeType();
        switch(NodeType){
            case Expressions.ExpressionType.ArrayIndex:
            case Expressions.ExpressionType.Assign:
            case Expressions.ExpressionType.Coalesce:{
                var (Left,Right)=Deserialize_Binary(ref reader,Resolver);
                return Expressions.Expression.MakeBinary(NodeType,Left,Right);
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
            case Expressions.ExpressionType.SubtractChecked:{
                var (Left,Right,Method)=Deserialize_Binary_MethodInfo(ref reader,Resolver);
                //return Expressions.Expression.SubtractChecked(Left,Right,Method);
                return Expressions.Expression.MakeBinary(NodeType,Left,Right,false,Method);
            }
            case Expressions.ExpressionType.Equal:
            case Expressions.ExpressionType.GreaterThan:
            case Expressions.ExpressionType.GreaterThanOrEqual:
            case Expressions.ExpressionType.LessThan:
            case Expressions.ExpressionType.LessThanOrEqual:
            case Expressions.ExpressionType.NotEqual:{
                var (Left,Right,IsLiftedToNull,Method)=Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.MakeBinary(NodeType,Left,Right,IsLiftedToNull,Method);
            }
            default:throw new NotSupportedException(NodeType.ToString());
        }
    }
}
