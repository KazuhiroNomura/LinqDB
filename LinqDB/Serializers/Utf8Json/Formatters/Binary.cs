using System;


using System.Reflection;
using Utf8Json;

using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T= Expressions.BinaryExpression;
public class Binary:IJsonFormatter<T> {
    public static readonly Binary Instance=new();
    internal static void WriteLeftRight(ref Writer writer,T value,IJsonFormatterResolver Resolver){

        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        Expression.Write(ref writer,value.Left,Resolver);
        writer.WriteValueSeparator();
        Expression.Write(ref writer,value.Right,Resolver);
    }
    internal static void WriteLeftRightLambda(ref Writer writer,T value,IJsonFormatterResolver Resolver){

        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        Expression.Write(ref writer,value.Left,Resolver);
        writer.WriteValueSeparator();
        Expression.Write(ref writer,value.Right,Resolver);
        writer.WriteValueSeparator();
        Lambda.WriteNullable(ref writer,value.Conversion,Resolver);
    }
    internal static void WriteLeftRightMethod(ref Writer writer,T value,IJsonFormatterResolver Resolver){

        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        Expression.Write(ref writer,value.Left,Resolver);
        writer.WriteValueSeparator();
        Expression.Write(ref writer,value.Right,Resolver);
        writer.WriteValueSeparator();
        Method.WriteNullable(ref writer,value.Method,Resolver);
    }
    internal static void WriteLeftRightMethodLambda(ref Writer writer,T value,IJsonFormatterResolver Resolver){

        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        Expression.Write(ref writer,value.Left,Resolver);
        writer.WriteValueSeparator();
        Expression.Write(ref writer,value.Right,Resolver);
        writer.WriteValueSeparator();
        Method.WriteNullable(ref writer,value.Method,Resolver);
        writer.WriteValueSeparator();
        Lambda.WriteNullable(ref writer,value.Conversion,Resolver);
    }
    internal static void WriteLeftRightBooleanMethod(ref Writer writer,T value,IJsonFormatterResolver Resolver){

        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        Expression.Write(ref writer,value.Left,Resolver);
        writer.WriteValueSeparator();
        Expression.Write(ref writer,value.Right,Resolver);
        writer.WriteValueSeparator();
        writer.WriteBoolean(value.IsLiftedToNull);
        writer.WriteValueSeparator();
        Method.WriteNullable(ref writer,value.Method!,Resolver);
    }
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        switch(value.NodeType){
            case Expressions.ExpressionType.ArrayIndex           :
            case Expressions.ExpressionType.Assign               :WriteLeftRight(ref writer,value,Resolver); break;
            case Expressions.ExpressionType.Coalesce             :WriteLeftRightLambda(ref writer,value,Resolver); break;
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
            case Expressions.ExpressionType.SubtractChecked      :WriteLeftRightMethod(ref writer,value,Resolver); break;
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
            case Expressions.ExpressionType.SubtractAssignChecked:WriteLeftRightMethodLambda(ref writer,value,Resolver); break;
            case Expressions.ExpressionType.Equal                :
            case Expressions.ExpressionType.GreaterThan          :
            case Expressions.ExpressionType.GreaterThanOrEqual   :
            case Expressions.ExpressionType.LessThan             :
            case Expressions.ExpressionType.LessThanOrEqual      :
            case Expressions.ExpressionType.NotEqual             :WriteLeftRightBooleanMethod(ref writer,value,Resolver);break;
            default:throw new NotSupportedException(value.NodeType.ToString());
        }
        writer.WriteEndArray();
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.WriteIsNull(value))return;
        Write(ref writer,value,Resolver);
    }
    internal static(Expressions.Expression left,Expressions.Expression right)ReadLeftRight(ref Reader reader,IJsonFormatterResolver Resolver){
        var left= Expression.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var right= Expression.Read(ref reader,Resolver);
        return(left,right);
    }
    internal static(Expressions.Expression left,Expressions.Expression right,Expressions.LambdaExpression? conversion)ReadLeftRightLambda(ref Reader reader,IJsonFormatterResolver Resolver){
        var left= Expression.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var right= Expression.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var conversion=Lambda.ReadNullable(ref reader,Resolver);
        return(left,right,conversion);
    }
    internal static(Expressions.Expression left,Expressions.Expression right,MethodInfo? method)InternalDeserializeMethod(ref Reader reader,IJsonFormatterResolver Resolver){
        var left= Expression.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var right= Expression.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var method=Method.ReadNullable(ref reader,Resolver);
        return(left,right,method);
    }
    internal static(Expressions.Expression left,Expressions.Expression right,MethodInfo? method,Expressions.LambdaExpression? conversion)ReadLeftRightMethodLambda(ref Reader reader,IJsonFormatterResolver Resolver){
        var left= Expression.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var right= Expression.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var method=Method.ReadNullable(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var conversion=Lambda.ReadNullable(ref reader,Resolver);
        return(left,right,method,conversion);
    }
    internal static(Expressions.Expression left,Expressions.Expression right,bool isLiftedToNull,MethodInfo? method)ReadLeftRightBooleanMethod(ref Reader reader,IJsonFormatterResolver Resolver){
        var left= Expression.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var right= Expression.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var isLiftedToNull=reader.ReadBoolean();
        reader.ReadIsValueSeparatorWithVerify();
        var method=Method.ReadNullable(ref reader,Resolver);
        return(left,right,isLiftedToNull,method);
    }
    internal static T Read(ref Reader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var NodeTypeName=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        T value;
        var NodeType=Enum.Parse<Expressions.ExpressionType>(NodeTypeName);
        switch(NodeType){
            case Expressions.ExpressionType.ArrayIndex: {
                var (array, index)=ReadLeftRight(ref reader,Resolver);
                value=Expressions.Expression.ArrayIndex(array,index); break;
            }
            case Expressions.ExpressionType.Assign: {
                var (left, right)=ReadLeftRight(ref reader,Resolver);
                value=Expressions.Expression.Assign(left,right); break;
            }
            case Expressions.ExpressionType.Coalesce: {
                var (left, right,conversion)=ReadLeftRightLambda(ref reader,Resolver);
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
                var (left, right, method,conversion)=ReadLeftRightMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.AddAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.AddAssignChecked: {
                var (left, right, method,conversion)=ReadLeftRightMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.AddAssignChecked(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.AndAssign: {
                var (left, right, method,conversion)=ReadLeftRightMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.AndAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.DivideAssign: {
                var (left, right, method,conversion)=ReadLeftRightMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.DivideAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.ExclusiveOrAssign: {
                var (left, right, method,conversion)=ReadLeftRightMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.ExclusiveOrAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.LeftShiftAssign: {
                var (left, right, method,conversion)=ReadLeftRightMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.LeftShiftAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.ModuloAssign: {
                var (left, right, method,conversion)=ReadLeftRightMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.ModuloAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.MultiplyAssign: {
                var (left, right, method,conversion)=ReadLeftRightMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.MultiplyAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.MultiplyAssignChecked: {
                var (left, right, method,conversion)=ReadLeftRightMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.MultiplyAssignChecked(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.OrAssign: {
                var (left, right, method,conversion)=ReadLeftRightMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.OrAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.PowerAssign: {
                var (left, right, method,conversion)=ReadLeftRightMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.PowerAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.RightShiftAssign: {
                var (left, right, method,conversion)=ReadLeftRightMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.RightShiftAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.SubtractAssign: {
                var (left, right, method,conversion)=ReadLeftRightMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.SubtractAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.SubtractAssignChecked: {
                var (left, right, method,conversion)=ReadLeftRightMethodLambda(ref reader,Resolver);
                value=Expressions.Expression.SubtractAssignChecked(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.Equal: {
                var (left, right, isLiftedToNull, method)=ReadLeftRightBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.Equal(left,right,isLiftedToNull,method); break;
            }
            case Expressions.ExpressionType.GreaterThan: {
                var (left, right, isLiftedToNull, method)=ReadLeftRightBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.GreaterThan(left,right,isLiftedToNull,method); break;
            }
            case Expressions.ExpressionType.GreaterThanOrEqual: {
                var (left, right, isLiftedToNull, method)=ReadLeftRightBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.GreaterThanOrEqual(left,right,isLiftedToNull,method); break;
            }
            case Expressions.ExpressionType.LessThan: {
                var (left, right, isLiftedToNull, method)=ReadLeftRightBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.LessThan(left,right,isLiftedToNull,method); break;
            }
            case Expressions.ExpressionType.LessThanOrEqual: {
                var (left, right, isLiftedToNull, method)=ReadLeftRightBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.LessThanOrEqual(left,right,isLiftedToNull,method); break;
            }
            case Expressions.ExpressionType.NotEqual: {
                var (left, right, isLiftedToNull, method)=ReadLeftRightBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.NotEqual(left,right,isLiftedToNull,method); break;
            }
            default:throw new NotSupportedException(NodeTypeName);
        }
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull())return null!;
        return Read(ref reader,Resolver);
    }
}
