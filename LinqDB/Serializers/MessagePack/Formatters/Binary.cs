using System;

using System.Diagnostics;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.BinaryExpression;
public class Binary:IMessagePackFormatter<T> {
    public static readonly Binary Instance=new();
    internal static void Write(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(3);
        writer.WriteNodeType(value.NodeType);
        
        Expression.Write(ref writer,value.Left,Resolver);

        Expression.Write(ref writer,value.Right,Resolver);
    }
    internal static void InternalSerializeLambda(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(4);
        writer.WriteNodeType(value.NodeType);
        
        Expression.Write(ref writer,value.Left,Resolver);

        Expression.Write(ref writer,value.Right,Resolver);

        Lambda.InternalSerializeConversion(ref writer,value.Conversion,Resolver);
    }
    internal static void InternalSerializeMethod(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(4);
        writer.WriteNodeType(value.NodeType);
        
        Expression.Write(ref writer,value.Left,Resolver);

        Expression.Write(ref writer,value.Right,Resolver);

        Method.InternalSerializeNullable(ref writer,value.Method,Resolver);
    }
    internal static void InternalSerializeMethodLambda(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(5);
        writer.WriteNodeType(value.NodeType);
        
        Expression.Write(ref writer,value.Left,Resolver);

        Expression.Write(ref writer,value.Right,Resolver);

        Method.InternalSerializeNullable(ref writer,value.Method,Resolver);

        Lambda.InternalSerializeConversion(ref writer,value.Conversion,Resolver);
    }
    internal static void InternalSerializeBooleanMethod(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(5);
        writer.WriteNodeType(value!.NodeType);
        
        Expression.Write(ref writer,value.Left,Resolver);
        
        Expression.Write(ref writer,value.Right,Resolver);
        
        writer.Write(value.IsLiftedToNull);
        
        Method.InternalSerializeNullable(ref writer,value.Method,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        if(writer.TryWriteNil(value)) return;
        switch(value!.NodeType){
            case Expressions.ExpressionType.ArrayIndex           :
            case Expressions.ExpressionType.Assign               :Write(ref writer,value,Resolver); break;
            case Expressions.ExpressionType.Coalesce             :InternalSerializeLambda(ref writer,value,Resolver); break;
            case Expressions.ExpressionType.Add                  :
            case Expressions.ExpressionType.AddChecked           :
            case Expressions.ExpressionType.And                  :
            case Expressions.ExpressionType.AndAlso              :
            case Expressions.ExpressionType.Divide               :
            case Expressions.ExpressionType.ExclusiveOr          :
            case Expressions.ExpressionType.LeftShift            :
            case Expressions.ExpressionType.Modulo               :
            case Expressions.ExpressionType.Multiply             :
            case Expressions.ExpressionType.MultiplyChecked      :
            case Expressions.ExpressionType.Or                   :
            case Expressions.ExpressionType.OrElse               :
            case Expressions.ExpressionType.Power                :
            case Expressions.ExpressionType.RightShift           :
            case Expressions.ExpressionType.Subtract             :
            case Expressions.ExpressionType.SubtractChecked      :InternalSerializeMethod(ref writer,value,Resolver); break;
            case Expressions.ExpressionType.AddAssign            :
            case Expressions.ExpressionType.AddAssignChecked     :
            case Expressions.ExpressionType.DivideAssign         :
            case Expressions.ExpressionType.AndAssign            :
            case Expressions.ExpressionType.ExclusiveOrAssign    :
            case Expressions.ExpressionType.LeftShiftAssign      :
            case Expressions.ExpressionType.ModuloAssign         :
            case Expressions.ExpressionType.MultiplyAssign       :
            case Expressions.ExpressionType.MultiplyAssignChecked:
            case Expressions.ExpressionType.OrAssign             :
            case Expressions.ExpressionType.PowerAssign          :
            case Expressions.ExpressionType.RightShiftAssign     :
            case Expressions.ExpressionType.SubtractAssign       :
            case Expressions.ExpressionType.SubtractAssignChecked:InternalSerializeMethodLambda(ref writer,value,Resolver); break;
            case Expressions.ExpressionType.Equal                :
            case Expressions.ExpressionType.GreaterThan          :
            case Expressions.ExpressionType.GreaterThanOrEqual   :
            case Expressions.ExpressionType.LessThan             :
            case Expressions.ExpressionType.LessThanOrEqual      :
            case Expressions.ExpressionType.NotEqual             :InternalSerializeBooleanMethod(ref writer,value,Resolver);break;
            default:throw new NotSupportedException(value.NodeType.ToString());
        }
        
    }
    internal static(Expressions.Expression left,Expressions.Expression right)InternalDeserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var left= Expression.Read(ref reader,Resolver);
        
        var right= Expression.Read(ref reader,Resolver);
        return(left,right);
    }
    internal static (Expressions.Expression left, Expressions.Expression right, Expressions.LambdaExpression? Conversion) InternalDeserializeLambda(ref Reader reader,MessagePackSerializerOptions Resolver) {
        var left = Expression.Read(ref reader,Resolver);

        var right = Expression.Read(ref reader,Resolver);

        var conversion= Lambda.InternalDeserializeConversion(ref reader,Resolver);
        return (left, right, conversion);
    }
    internal static(Expressions.Expression left,Expressions.Expression right,MethodInfo? Method)InternalDeserializeMethod(ref Reader reader,MessagePackSerializerOptions Resolver){
        var left= Expression.Read(ref reader,Resolver);
        
        var right= Expression.Read(ref reader,Resolver);
        
        var method=Method.ReadNullable(ref reader,Resolver);
        return(left,right,method);
    }
    internal static (Expressions.Expression left, Expressions.Expression right, MethodInfo? Method, Expressions.LambdaExpression? Conversion) InternalDeserializeMethodLambda(ref Reader reader,MessagePackSerializerOptions Resolver) {
        var left = Expression.Read(ref reader,Resolver);
        
        var right = Expression.Read(ref reader,Resolver);
        
        var method = Method.ReadNullable(ref reader,Resolver);
        
        var conversion= Lambda.InternalDeserializeConversion(ref reader,Resolver);
        return (left, right, method,conversion);
    }
    internal static(Expressions.Expression left,Expressions.Expression right,bool IsLiftedToNull,MethodInfo? Method)InternalDeserializeBooleanMethod(ref Reader reader,MessagePackSerializerOptions Resolver){
        var left= Expression.Read(ref reader,Resolver);
        
        var right= Expression.Read(ref reader,Resolver);
        
        var IsLiftedToNull=reader.ReadBoolean();
        
        var method=Method.ReadNullable(ref reader,Resolver);
        return(left,right,IsLiftedToNull,method);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();

        Debug.Assert(count is >=3 and <=5);
        T value;
        var NodeType=reader.ReadNodeType();
        switch(NodeType){
            case Expressions.ExpressionType.ArrayIndex: {
                var (array, index)=InternalDeserialize(ref reader,Resolver);
                value=Expressions.Expression.ArrayIndex(array,index); break;
            }
            case Expressions.ExpressionType.Assign: {
                var (left, right)=InternalDeserialize(ref reader,Resolver);
                value=Expressions.Expression.Assign(left,right); break;
            }
            case Expressions.ExpressionType.Coalesce: {
                var (left, right,conversion)=InternalDeserializeLambda(ref reader,Resolver);
                value=Expressions.Expression.Coalesce(left,right,conversion); break;
            }
            case Expressions.ExpressionType.Add: {
                var (left, right, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Add(left,right,method); break;
            }
            case Expressions.ExpressionType.AddChecked: {
                var (left, right, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.AddChecked(left,right,method); break;
            }
            case Expressions.ExpressionType.And: {
                var (left, right, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.And(left,right,method); break;
            }
            case Expressions.ExpressionType.AndAlso: {
                var (left, right, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.AndAlso(left,right,method); break;
            }
            case Expressions.ExpressionType.Divide: {
                var (left, right, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Divide(left,right,method); break;
            }
            case Expressions.ExpressionType.ExclusiveOr: {
                var (left, right, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.ExclusiveOr(left,right,method); break;
            }
            case Expressions.ExpressionType.LeftShift: {
                var (left, right, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.LeftShift(left,right,method); break;
            }
            case Expressions.ExpressionType.Modulo: {
                var (left, right, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Modulo(left,right,method); break;
            }
            case Expressions.ExpressionType.Multiply: {
                var (left, right, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Multiply(left,right,method); break;
            }
            case Expressions.ExpressionType.MultiplyChecked: {
                var (left, right, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.MultiplyChecked(left,right,method); break;
            }
            case Expressions.ExpressionType.Or: {
                var (left, right, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Or(left,right,method); break;
            }
            case Expressions.ExpressionType.OrElse: {
                var (left, right, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.OrElse(left,right,method); break;
            }
            case Expressions.ExpressionType.Power: {
                var (left, right, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Power(left,right,method); break;
            }
            case Expressions.ExpressionType.RightShift: {
                var (left, right, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.RightShift(left,right,method); break;
            }
            case Expressions.ExpressionType.Subtract: {
                var (left, right, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Subtract(left,right,method); break;
            }
            case Expressions.ExpressionType.SubtractChecked: {
                var (left, right, method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.SubtractChecked(left,right,method); break;
            }
            case Expressions.ExpressionType.AddAssign: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.AddAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.AddAssignChecked: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.AddAssignChecked(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.AndAssign: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.AndAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.DivideAssign: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.DivideAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.ExclusiveOrAssign: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.ExclusiveOrAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.LeftShiftAssign: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.LeftShiftAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.ModuloAssign: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.ModuloAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.MultiplyAssign: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.MultiplyAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.MultiplyAssignChecked: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.MultiplyAssignChecked(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.OrAssign: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.OrAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.PowerAssign: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.PowerAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.RightShiftAssign: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.RightShiftAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.SubtractAssign: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.SubtractAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.SubtractAssignChecked: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.SubtractAssignChecked(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.Equal: {
                var (left, right, isLiftedToNull, method)=InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.Equal(left,right,isLiftedToNull,method); break;
            }
            case Expressions.ExpressionType.GreaterThan: {
                var (left, right, isLiftedToNull, method)=InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.GreaterThan(left,right,isLiftedToNull,method); break;
            }
            case Expressions.ExpressionType.GreaterThanOrEqual: {
                var (left, right, isLiftedToNull, method)=InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.GreaterThanOrEqual(left,right,isLiftedToNull,method); break;
            }
            case Expressions.ExpressionType.LessThan: {
                var (left, right, isLiftedToNull, method)=InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.LessThan(left,right,isLiftedToNull,method); break;
            }
            case Expressions.ExpressionType.LessThanOrEqual: {
                var (left, right, isLiftedToNull, method)=InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.LessThanOrEqual(left,right,isLiftedToNull,method); break;
            }
            case Expressions.ExpressionType.NotEqual: {
                var (left, right, isLiftedToNull, method)=InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.NotEqual(left,right,isLiftedToNull,method); break;
            }
            default: throw new NotSupportedException(NodeType.ToString());
        }

        return value;
    }
}
