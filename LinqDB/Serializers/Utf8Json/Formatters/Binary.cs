using System;
using Expressions=System.Linq.Expressions;
using System.Reflection;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T= Expressions.BinaryExpression;
public class Binary:IJsonFormatter<T> {
    public static readonly Binary Instance=new();
    internal static void InternalSerializeBooleanMethod(ref Writer writer,T value,
        IJsonFormatterResolver Resolver){
        Expression.Instance.Serialize(ref writer,value.Left,Resolver);
        writer.WriteValueSeparator();
        Expression.Instance.Serialize(ref writer,value.Right,Resolver);
        writer.WriteValueSeparator();
        writer.WriteBoolean(value.IsLiftedToNull);
        writer.WriteValueSeparator();
        Method.Instance.Serialize(ref writer,value.Method!,Resolver);
    }
    internal static void InternalSerializeMethod(ref Writer writer,T value,
        IJsonFormatterResolver Resolver){
        Expression.Instance.Serialize(ref writer,value.Left,Resolver);
        writer.WriteValueSeparator();
        Expression.Instance.Serialize(ref writer,value.Right,Resolver);
        writer.WriteValueSeparator();
        Method.Instance.Serialize(ref writer,value.Method,Resolver);
    }
    internal static void InternalSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        Expression.Instance.Serialize(ref writer,value.Left,Resolver);
        writer.WriteValueSeparator();
        Expression.Instance.Serialize(ref writer,value.Right,Resolver);
    }
    internal static(Expressions.Expression Left,Expressions.Expression Right)InternalDeserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        var Left= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Right= Expression.Instance.Deserialize(ref reader,Resolver);
        return(Left,Right);
    }
    internal static(Expressions.Expression Left,Expressions.Expression Right,MethodInfo Method)InternalDeserializeMethod(ref Reader reader,IJsonFormatterResolver Resolver){
        var Left= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Right= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var method=Method.Instance.Deserialize(ref reader,Resolver);
        return(Left,Right,method);
    }
    internal static(Expressions.Expression Left,Expressions.Expression Right,bool IsLiftedToNull,MethodInfo Method)InternalDeserializeBooleanMethod(ref Reader reader,IJsonFormatterResolver Resolver){
        var Left= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Right= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var IsLiftedToNull=reader.ReadBoolean();
        reader.ReadIsValueSeparatorWithVerify();
        var method=Method.Instance.Deserialize(ref reader,Resolver);
        return(Left,Right,IsLiftedToNull,method);
    }
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        writer.WriteString(value.NodeType.ToString());
        writer.WriteValueSeparator();
        switch(value.NodeType){
            case Expressions.ExpressionType.Assign               :
            case Expressions.ExpressionType.Coalesce             :
            case Expressions.ExpressionType.ArrayIndex           :InternalSerialize(ref writer,value,Resolver); break;
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
            case Expressions.ExpressionType.SubtractChecked      :InternalSerializeMethod(ref writer,value,Resolver); break;
            case Expressions.ExpressionType.Equal                :
            case Expressions.ExpressionType.GreaterThan          :
            case Expressions.ExpressionType.GreaterThanOrEqual   :
            case Expressions.ExpressionType.LessThan             :
            case Expressions.ExpressionType.LessThanOrEqual      :
            case Expressions.ExpressionType.NotEqual             :InternalSerializeBooleanMethod(ref writer,value,Resolver); break;
            default:
                throw new NotSupportedException(value.NodeType.ToString());
        }
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var NodeTypeName=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        T value;
        var NodeType=Enum.Parse<Expressions.ExpressionType>(NodeTypeName);
        switch(NodeType){
            case Expressions.ExpressionType.ArrayIndex: {
                var (array, index)=InternalDeserialize(ref reader,Resolver);
                value=Expressions.Expression.ArrayIndex(array,index);break;
            }
            case Expressions.ExpressionType.Assign: {
                var (Left, Right)=InternalDeserialize(ref reader,Resolver);
                value=Expressions.Expression.Assign(Left,Right);break;
            }
            case Expressions.ExpressionType.Coalesce: {
                var (Left, Right)=InternalDeserialize(ref reader,Resolver);
                value=Expressions.Expression.Coalesce(Left,Right);break;
            }
            case Expressions.ExpressionType.Add: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Add(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.AddAssign: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.AddAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.AddAssignChecked: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.AddAssignChecked(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.AddChecked: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.AddChecked(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.And: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.And(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.AndAssign: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.AndAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.AndAlso: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.AndAlso(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.Divide: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Divide(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.DivideAssign: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.DivideAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.ExclusiveOr: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.ExclusiveOr(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.ExclusiveOrAssign: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.ExclusiveOrAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.LeftShift: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.LeftShift(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.LeftShiftAssign: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.LeftShiftAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.Modulo: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Modulo(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.ModuloAssign: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.ModuloAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.Multiply: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Multiply(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.MultiplyAssign: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.MultiplyAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.MultiplyAssignChecked: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.MultiplyAssignChecked(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.MultiplyChecked: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.MultiplyChecked(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.Or: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Or(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.OrAssign: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.OrAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.OrElse: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.OrElse(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.Power: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Power(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.PowerAssign: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.PowerAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.RightShift: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.RightShift(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.RightShiftAssign: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.RightShiftAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.Subtract: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Subtract(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.SubtractAssign: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.SubtractAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.SubtractAssignChecked: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.SubtractAssignChecked(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.SubtractChecked: {
                var (Left, Right, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.SubtractChecked(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.Equal: {
                var (Left, Right, IsLiftedToNull, Method)=InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.Equal(Left,Right,IsLiftedToNull,Method);break;
            }
            case Expressions.ExpressionType.GreaterThan: {
                var (Left, Right, IsLiftedToNull, Method)=InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.GreaterThan(Left,Right,IsLiftedToNull,Method);break;
            }
            case Expressions.ExpressionType.GreaterThanOrEqual: {
                var (Left, Right, IsLiftedToNull, Method)=InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.GreaterThanOrEqual(Left,Right,IsLiftedToNull,Method);break;
            }
            case Expressions.ExpressionType.LessThan: {
                var (Left, Right, IsLiftedToNull, Method)=InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.LessThan(Left,Right,IsLiftedToNull,Method);break;
            }
            case Expressions.ExpressionType.LessThanOrEqual: {
                var (Left, Right, IsLiftedToNull, Method)=InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.LessThanOrEqual(Left,Right,IsLiftedToNull,Method);break;
            }
            case Expressions.ExpressionType.NotEqual: {
                var (Left, Right, IsLiftedToNull, Method)=InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.NotEqual(Left,Right,IsLiftedToNull,Method);break;
            }
            default:throw new NotSupportedException(NodeTypeName);
        }
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
