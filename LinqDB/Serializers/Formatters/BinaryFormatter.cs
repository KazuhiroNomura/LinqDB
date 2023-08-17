using System;
using System.Linq.Expressions;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<BinaryExpression>{
    public void Serialize(ref JsonWriter writer,BinaryExpression? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        writer.WriteString(value.NodeType.ToString());
        writer.WriteValueSeparator();
        switch(value.NodeType){
            case ExpressionType.Assign or ExpressionType.Coalesce or ExpressionType.ArrayIndex:{
                this.Serialize(ref writer,value.Left,Resolver);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,value.Right,Resolver);
                break;
            }
            case ExpressionType.Add:
            case ExpressionType.AddAssign:
            case ExpressionType.AddAssignChecked:
            case ExpressionType.AddChecked:
            case ExpressionType.And:
            case ExpressionType.AndAssign:
            case ExpressionType.AndAlso:
            case ExpressionType.Divide:
            case ExpressionType.DivideAssign:
            case ExpressionType.ExclusiveOr:
            case ExpressionType.ExclusiveOrAssign:
            case ExpressionType.LeftShift:
            case ExpressionType.LeftShiftAssign:
            case ExpressionType.Modulo:
            case ExpressionType.ModuloAssign:
            case ExpressionType.Multiply:
            case ExpressionType.MultiplyAssign:
            case ExpressionType.MultiplyAssignChecked:
            case ExpressionType.MultiplyChecked:
            case ExpressionType.Or:
            case ExpressionType.OrAssign:
            case ExpressionType.OrElse:
            case ExpressionType.Power:
            case ExpressionType.PowerAssign:
            case ExpressionType.RightShift:
            case ExpressionType.RightShiftAssign:
            case ExpressionType.Subtract:
            case ExpressionType.SubtractAssign:
            case ExpressionType.SubtractAssignChecked:
            case ExpressionType.SubtractChecked:{
                this.Serialize(ref writer,value.Left,Resolver);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,value.Right,Resolver);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,value.Method,Resolver);
                break;
            }
            case ExpressionType.Equal:
            case ExpressionType.GreaterThan:
            case ExpressionType.GreaterThanOrEqual:
            case ExpressionType.LessThan:
            case ExpressionType.LessThanOrEqual:
            case ExpressionType.NotEqual:{
                this.Serialize(ref writer,value.Left,Resolver);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,value.Right,Resolver);
                writer.WriteValueSeparator();
                writer.WriteBoolean(value.IsLiftedToNull);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,value.Method!,Resolver);
                break;
            }
            default:
                throw new NotSupportedException(value.NodeType.ToString());
        }
        writer.WriteEndArray();
    }
    private (Expression Left,Expression Right)Deserialize_Binary(ref JsonReader reader,IJsonFormatterResolver Resolver){
        var Left= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Right= this.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return(Left,Right);
    }
    private (Expression Left,Expression Right,MethodInfo Method)Deserialize_Binary_MethodInfo(ref JsonReader reader,IJsonFormatterResolver Resolver){
        var Left= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Right= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Method=Deserialize_T<MethodInfo>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return(Left,Right,Method);
    }
    private (Expression Left,Expression Right,bool IsLiftedToNull,MethodInfo Method)Deserialize_Binary_bool_MethodInfo(ref JsonReader reader,IJsonFormatterResolver Resolver){
        var Left= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Right= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var IsLiftedToNull=reader.ReadBoolean();
        reader.ReadIsValueSeparatorWithVerify();
        var Method=Deserialize_T<MethodInfo>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return(Left,Right,IsLiftedToNull,Method);
    }
    BinaryExpression IJsonFormatter<BinaryExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var NodeTypeName=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var NodeType=Enum.Parse<ExpressionType>(NodeTypeName);
        switch(NodeType){
            case ExpressionType.Assign:{
                var (Left,Right)=this.Deserialize_Binary(ref reader,Resolver);
                return Expression.Assign(Left,Right);
            }
            case ExpressionType.Coalesce:{
                var (Left,Right)=this.Deserialize_Binary(ref reader,Resolver);
                return Expression.Coalesce(Left,Right);
            }
            case ExpressionType.Add:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.Add(Left,Right,Method);
            }
            case ExpressionType.AddAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.AddAssign(Left,Right,Method);
            }
            case ExpressionType.AddAssignChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.AddAssignChecked(Left,Right,Method);
            }
            case ExpressionType.AddChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.AddChecked(Left,Right,Method);
            }
            case ExpressionType.And:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.And(Left,Right,Method);
            }
            case ExpressionType.AndAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.AndAssign(Left,Right,Method);
            }
            case ExpressionType.AndAlso:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.AndAlso(Left,Right,Method);
            }
            case ExpressionType.Divide:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.Divide(Left,Right,Method);
            }
            case ExpressionType.DivideAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.DivideAssign(Left,Right,Method);
            }
            case ExpressionType.ExclusiveOr:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.ExclusiveOr(Left,Right,Method);
            }
            case ExpressionType.ExclusiveOrAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.ExclusiveOrAssign(Left,Right,Method);
            }
            case ExpressionType.LeftShift:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.LeftShift(Left,Right,Method);
            }
            case ExpressionType.LeftShiftAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.LeftShiftAssign(Left,Right,Method);
            }
            case ExpressionType.Modulo:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.Modulo(Left,Right,Method);
            }
            case ExpressionType.ModuloAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.ModuloAssign(Left,Right,Method);
            }
            case ExpressionType.Multiply:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.Multiply(Left,Right,Method);
            }
            case ExpressionType.MultiplyAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.MultiplyAssign(Left,Right,Method);
            }
            case ExpressionType.MultiplyAssignChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.MultiplyAssignChecked(Left,Right,Method);
            }
            case ExpressionType.MultiplyChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.MultiplyChecked(Left,Right,Method);
            }
            case ExpressionType.Or:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.Or(Left,Right,Method);
            }
            case ExpressionType.OrAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.OrAssign(Left,Right,Method);
            }
            case ExpressionType.OrElse:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.OrElse(Left,Right,Method);
            }
            case ExpressionType.Power:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.Power(Left,Right,Method);
            }
            case ExpressionType.PowerAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.PowerAssign(Left,Right,Method);
            }
            case ExpressionType.RightShift:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.RightShift(Left,Right,Method);
            }
            case ExpressionType.RightShiftAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.RightShiftAssign(Left,Right,Method);
            }
            case ExpressionType.Subtract:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.Subtract(Left,Right,Method);
            }
            case ExpressionType.SubtractAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.SubtractAssign(Left,Right,Method);
            }
            case ExpressionType.SubtractAssignChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.SubtractAssignChecked(Left,Right,Method);
            }
            case ExpressionType.SubtractChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.SubtractChecked(Left,Right,Method);
            }
            case ExpressionType.Equal:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expression.Equal(Left,Right,IsLiftedToNull,Method);
            }
            case ExpressionType.GreaterThan:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expression.GreaterThan(Left,Right,IsLiftedToNull,Method);
            }
            case ExpressionType.GreaterThanOrEqual:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expression.GreaterThanOrEqual(Left,Right,IsLiftedToNull,Method);
            }
            case ExpressionType.LessThan:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expression.LessThan(Left,Right,IsLiftedToNull,Method);
            }
            case ExpressionType.LessThanOrEqual:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expression.LessThanOrEqual(Left,Right,IsLiftedToNull,Method);
            }
            case ExpressionType.NotEqual:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expression.NotEqual(Left,Right,IsLiftedToNull,Method);
            }
            case ExpressionType.ArrayIndex:{
                var (array,index)=this.Deserialize_Binary(ref reader,Resolver);
                return Expression.ArrayIndex(array,index);
            }
        }
        throw new NotSupportedException(NodeTypeName);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<BinaryExpression>{
    private (Expression Left,Expression Right)Deserialize_Binary(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var Left= this.Deserialize(ref reader,Resolver);
        var Right= this.Deserialize(ref reader,Resolver);
        return(Left,Right);
    }
    private (Expression Left,Expression Right,MethodInfo Method)Deserialize_Binary_MethodInfo(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var Left= this.Deserialize(ref reader,Resolver);
        var Right= this.Deserialize(ref reader,Resolver);
        var Method=Deserialize_T<MethodInfo>(ref reader,Resolver);
        return(Left,Right,Method);
    }
    private (Expression Left,Expression Right,bool IsLiftedToNull,MethodInfo Method)Deserialize_Binary_bool_MethodInfo(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var Left= this.Deserialize(ref reader,Resolver);
        var Right= this.Deserialize(ref reader,Resolver);
        var IsLiftedToNull=reader.ReadBoolean();
        var Method=Deserialize_T<MethodInfo>(ref reader,Resolver);
        return(Left,Right,IsLiftedToNull,Method);
    }
    public void Serialize(ref MessagePackWriter writer,BinaryExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        writer.Write((byte)value.NodeType);
        switch(value.NodeType){
            case ExpressionType.Assign or ExpressionType.Coalesce or ExpressionType.ArrayIndex:{
                this.Serialize(ref writer,value.Left,Resolver);
                this.Serialize(ref writer,value.Right,Resolver);
                break;
            }
            case ExpressionType.Add:
            case ExpressionType.AddAssign:
            case ExpressionType.AddAssignChecked:
            case ExpressionType.AddChecked:
            case ExpressionType.And:
            case ExpressionType.AndAssign:
            case ExpressionType.AndAlso:
            case ExpressionType.Divide:
            case ExpressionType.DivideAssign:
            case ExpressionType.ExclusiveOr:
            case ExpressionType.ExclusiveOrAssign:
            case ExpressionType.LeftShift:
            case ExpressionType.LeftShiftAssign:
            case ExpressionType.Modulo:
            case ExpressionType.ModuloAssign:
            case ExpressionType.Multiply:
            case ExpressionType.MultiplyAssign:
            case ExpressionType.MultiplyAssignChecked:
            case ExpressionType.MultiplyChecked:
            case ExpressionType.Or:
            case ExpressionType.OrAssign:
            case ExpressionType.OrElse:
            case ExpressionType.Power:
            case ExpressionType.PowerAssign:
            case ExpressionType.RightShift:
            case ExpressionType.RightShiftAssign:
            case ExpressionType.Subtract:
            case ExpressionType.SubtractAssign:
            case ExpressionType.SubtractAssignChecked:
            case ExpressionType.SubtractChecked:{
                this.Serialize(ref writer,value.Left,Resolver);
                this.Serialize(ref writer,value.Right,Resolver);
                this.Serialize(ref writer,value.Method,Resolver);
                break;
            }
            case ExpressionType.Equal:
            case ExpressionType.GreaterThan:
            case ExpressionType.GreaterThanOrEqual:
            case ExpressionType.LessThan:
            case ExpressionType.LessThanOrEqual:
            case ExpressionType.NotEqual:{
                this.Serialize(ref writer,value.Left,Resolver);
                this.Serialize(ref writer,value.Right,Resolver);
                writer.Write(value.IsLiftedToNull);
                this.Serialize(ref writer,value.Method!,Resolver);
                break;
            }
            default:
                throw new NotSupportedException(value.NodeType.ToString());
        }
    }
    BinaryExpression IMessagePackFormatter<BinaryExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.IsNil) return null!;
        var NodeType=(ExpressionType)reader.ReadByte();
        switch(NodeType){
            case ExpressionType.Assign:{
                var (Left,Right)=this.Deserialize_Binary(ref reader,Resolver);
                return Expression.Assign(Left,Right);
            }
            case ExpressionType.Coalesce:{
                var (Left,Right)=this.Deserialize_Binary(ref reader,Resolver);
                return Expression.Coalesce(Left,Right);
            }
            case ExpressionType.Add:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.Add(Left,Right,Method);
            }
            case ExpressionType.AddAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.AddAssign(Left,Right,Method);
            }
            case ExpressionType.AddAssignChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.AddAssignChecked(Left,Right,Method);
            }
            case ExpressionType.AddChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.AddChecked(Left,Right,Method);
            }
            case ExpressionType.And:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.And(Left,Right,Method);
            }
            case ExpressionType.AndAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.AndAssign(Left,Right,Method);
            }
            case ExpressionType.AndAlso:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.AndAlso(Left,Right,Method);
            }
            case ExpressionType.Divide:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.Divide(Left,Right,Method);
            }
            case ExpressionType.DivideAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.DivideAssign(Left,Right,Method);
            }
            case ExpressionType.ExclusiveOr:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.ExclusiveOr(Left,Right,Method);
            }
            case ExpressionType.ExclusiveOrAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.ExclusiveOrAssign(Left,Right,Method);
            }
            case ExpressionType.LeftShift:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.LeftShift(Left,Right,Method);
            }
            case ExpressionType.LeftShiftAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.LeftShiftAssign(Left,Right,Method);
            }
            case ExpressionType.Modulo:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.Modulo(Left,Right,Method);
            }
            case ExpressionType.ModuloAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.ModuloAssign(Left,Right,Method);
            }
            case ExpressionType.Multiply:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.Multiply(Left,Right,Method);
            }
            case ExpressionType.MultiplyAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.MultiplyAssign(Left,Right,Method);
            }
            case ExpressionType.MultiplyAssignChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.MultiplyAssignChecked(Left,Right,Method);
            }
            case ExpressionType.MultiplyChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.MultiplyChecked(Left,Right,Method);
            }
            case ExpressionType.Or:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.Or(Left,Right,Method);
            }
            case ExpressionType.OrAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.OrAssign(Left,Right,Method);
            }
            case ExpressionType.OrElse:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.OrElse(Left,Right,Method);
            }
            case ExpressionType.Power:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.Power(Left,Right,Method);
            }
            case ExpressionType.PowerAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.PowerAssign(Left,Right,Method);
            }
            case ExpressionType.RightShift:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.RightShift(Left,Right,Method);
            }
            case ExpressionType.RightShiftAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.RightShiftAssign(Left,Right,Method);
            }
            case ExpressionType.Subtract:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.Subtract(Left,Right,Method);
            }
            case ExpressionType.SubtractAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.SubtractAssign(Left,Right,Method);
            }
            case ExpressionType.SubtractAssignChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.SubtractAssignChecked(Left,Right,Method);
            }
            case ExpressionType.SubtractChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expression.SubtractChecked(Left,Right,Method);
            }
            case ExpressionType.Equal:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expression.Equal(Left,Right,IsLiftedToNull,Method);
            }
            case ExpressionType.GreaterThan:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expression.GreaterThan(Left,Right,IsLiftedToNull,Method);
            }
            case ExpressionType.GreaterThanOrEqual:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expression.GreaterThanOrEqual(Left,Right,IsLiftedToNull,Method);
            }
            case ExpressionType.LessThan:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expression.LessThan(Left,Right,IsLiftedToNull,Method);
            }
            case ExpressionType.LessThanOrEqual:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expression.LessThanOrEqual(Left,Right,IsLiftedToNull,Method);
            }
            case ExpressionType.NotEqual:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expression.NotEqual(Left,Right,IsLiftedToNull,Method);
            }
            case ExpressionType.ArrayIndex:{
                var (array,index)=this.Deserialize_Binary(ref reader,Resolver);
                return Expression.ArrayIndex(array,index);
            }
        }
        throw new NotSupportedException(NodeType.ToString());
    }
}
