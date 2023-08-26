using System;
using Expressions=System.Linq.Expressions;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.BinaryExpression>{
    public void Serialize(ref JsonWriter writer,Expressions.BinaryExpression? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        writer.WriteString(value.NodeType.ToString());
        writer.WriteValueSeparator();
        switch(value.NodeType){
            case Expressions.ExpressionType.Assign or Expressions.ExpressionType.Coalesce or Expressions.ExpressionType.ArrayIndex:{
                this.Serialize(ref writer,value.Left,Resolver);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,value.Right,Resolver);
                break;
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
                this.Serialize(ref writer,value.Left,Resolver);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,value.Right,Resolver);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,value.Method,Resolver);
                break;
            }
            case Expressions.ExpressionType.Equal:
            case Expressions.ExpressionType.GreaterThan:
            case Expressions.ExpressionType.GreaterThanOrEqual:
            case Expressions.ExpressionType.LessThan:
            case Expressions.ExpressionType.LessThanOrEqual:
            case Expressions.ExpressionType.NotEqual:{
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
    private (Expressions.Expression Left,Expressions.Expression Right)Deserialize_Binary(ref JsonReader reader,IJsonFormatterResolver Resolver){
        var Left= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Right= this.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return(Left,Right);
    }
    private (Expressions.Expression Left,Expressions.Expression Right,MethodInfo Method)Deserialize_Binary_MethodInfo(ref JsonReader reader,IJsonFormatterResolver Resolver){
        var Left= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Right= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Method=Deserialize_T<MethodInfo>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return(Left,Right,Method);
    }
    private (Expressions.Expression Left,Expressions.Expression Right,bool IsLiftedToNull,MethodInfo Method)Deserialize_Binary_bool_MethodInfo(ref JsonReader reader,IJsonFormatterResolver Resolver){
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
    Expressions.BinaryExpression IJsonFormatter<Expressions.BinaryExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var NodeTypeName=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var NodeType=Enum.Parse<Expressions.ExpressionType>(NodeTypeName);
        switch(NodeType){
            case Expressions.ExpressionType.Assign:{
                var (Left,Right)=this.Deserialize_Binary(ref reader,Resolver);
                return Expressions.Expression.Assign(Left,Right);
            }
            case Expressions.ExpressionType.Coalesce:{
                var (Left,Right)=this.Deserialize_Binary(ref reader,Resolver);
                return Expressions.Expression.Coalesce(Left,Right);
            }
            case Expressions.ExpressionType.Add:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Add(Left,Right,Method);
            }
            case Expressions.ExpressionType.AddAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.AddAssign(Left,Right,Method);
            }
            case Expressions.ExpressionType.AddAssignChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.AddAssignChecked(Left,Right,Method);
            }
            case Expressions.ExpressionType.AddChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.AddChecked(Left,Right,Method);
            }
            case Expressions.ExpressionType.And:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.And(Left,Right,Method);
            }
            case Expressions.ExpressionType.AndAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.AndAssign(Left,Right,Method);
            }
            case Expressions.ExpressionType.AndAlso:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.AndAlso(Left,Right,Method);
            }
            case Expressions.ExpressionType.Divide:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Divide(Left,Right,Method);
            }
            case Expressions.ExpressionType.DivideAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.DivideAssign(Left,Right,Method);
            }
            case Expressions.ExpressionType.ExclusiveOr:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.ExclusiveOr(Left,Right,Method);
            }
            case Expressions.ExpressionType.ExclusiveOrAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.ExclusiveOrAssign(Left,Right,Method);
            }
            case Expressions.ExpressionType.LeftShift:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.LeftShift(Left,Right,Method);
            }
            case Expressions.ExpressionType.LeftShiftAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.LeftShiftAssign(Left,Right,Method);
            }
            case Expressions.ExpressionType.Modulo:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Modulo(Left,Right,Method);
            }
            case Expressions.ExpressionType.ModuloAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.ModuloAssign(Left,Right,Method);
            }
            case Expressions.ExpressionType.Multiply:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Multiply(Left,Right,Method);
            }
            case Expressions.ExpressionType.MultiplyAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.MultiplyAssign(Left,Right,Method);
            }
            case Expressions.ExpressionType.MultiplyAssignChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.MultiplyAssignChecked(Left,Right,Method);
            }
            case Expressions.ExpressionType.MultiplyChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.MultiplyChecked(Left,Right,Method);
            }
            case Expressions.ExpressionType.Or:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Or(Left,Right,Method);
            }
            case Expressions.ExpressionType.OrAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.OrAssign(Left,Right,Method);
            }
            case Expressions.ExpressionType.OrElse:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.OrElse(Left,Right,Method);
            }
            case Expressions.ExpressionType.Power:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Power(Left,Right,Method);
            }
            case Expressions.ExpressionType.PowerAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.PowerAssign(Left,Right,Method);
            }
            case Expressions.ExpressionType.RightShift:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.RightShift(Left,Right,Method);
            }
            case Expressions.ExpressionType.RightShiftAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.RightShiftAssign(Left,Right,Method);
            }
            case Expressions.ExpressionType.Subtract:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Subtract(Left,Right,Method);
            }
            case Expressions.ExpressionType.SubtractAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.SubtractAssign(Left,Right,Method);
            }
            case Expressions.ExpressionType.SubtractAssignChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.SubtractAssignChecked(Left,Right,Method);
            }
            case Expressions.ExpressionType.SubtractChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.SubtractChecked(Left,Right,Method);
            }
            case Expressions.ExpressionType.Equal:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Equal(Left,Right,IsLiftedToNull,Method);
            }
            case Expressions.ExpressionType.GreaterThan:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.GreaterThan(Left,Right,IsLiftedToNull,Method);
            }
            case Expressions.ExpressionType.GreaterThanOrEqual:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.GreaterThanOrEqual(Left,Right,IsLiftedToNull,Method);
            }
            case Expressions.ExpressionType.LessThan:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.LessThan(Left,Right,IsLiftedToNull,Method);
            }
            case Expressions.ExpressionType.LessThanOrEqual:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.LessThanOrEqual(Left,Right,IsLiftedToNull,Method);
            }
            case Expressions.ExpressionType.NotEqual:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.NotEqual(Left,Right,IsLiftedToNull,Method);
            }
            case Expressions.ExpressionType.ArrayIndex:{
                var (array,index)=this.Deserialize_Binary(ref reader,Resolver);
                return Expressions.Expression.ArrayIndex(array,index);
            }
        }
        throw new NotSupportedException(NodeTypeName);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.BinaryExpression>{
    private (Expressions.Expression Left,Expressions.Expression Right)Deserialize_Binary(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var Left= this.Deserialize(ref reader,Resolver);
        var Right= this.Deserialize(ref reader,Resolver);
        return(Left,Right);
    }
    private (Expressions.Expression Left,Expressions.Expression Right,MethodInfo Method)Deserialize_Binary_MethodInfo(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var Left= this.Deserialize(ref reader,Resolver);
        var Right= this.Deserialize(ref reader,Resolver);
        var Method=Deserialize_T<MethodInfo>(ref reader,Resolver);
        return(Left,Right,Method);
    }
    private (Expressions.Expression Left,Expressions.Expression Right,bool IsLiftedToNull,MethodInfo Method)Deserialize_Binary_bool_MethodInfo(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var Left= this.Deserialize(ref reader,Resolver);
        var Right= this.Deserialize(ref reader,Resolver);
        var IsLiftedToNull=reader.ReadBoolean();
        var Method=Deserialize_T<MethodInfo>(ref reader,Resolver);
        return(Left,Right,IsLiftedToNull,Method);
    }
    public void Serialize(ref MessagePackWriter writer,Expressions.BinaryExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        writer.Write((byte)value.NodeType);
        switch(value.NodeType){
            case Expressions.ExpressionType.Assign or Expressions.ExpressionType.Coalesce or Expressions.ExpressionType.ArrayIndex:{
                this.Serialize(ref writer,value.Left,Resolver);
                this.Serialize(ref writer,value.Right,Resolver);
                break;
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
                this.Serialize(ref writer,value.Left,Resolver);
                this.Serialize(ref writer,value.Right,Resolver);
                this.Serialize(ref writer,value.Method,Resolver);
                break;
            }
            case Expressions.ExpressionType.Equal:
            case Expressions.ExpressionType.GreaterThan:
            case Expressions.ExpressionType.GreaterThanOrEqual:
            case Expressions.ExpressionType.LessThan:
            case Expressions.ExpressionType.LessThanOrEqual:
            case Expressions.ExpressionType.NotEqual:{
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
    Expressions.BinaryExpression IMessagePackFormatter<Expressions.BinaryExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.IsNil) return null!;
        var NodeType=(Expressions.ExpressionType)reader.ReadByte();
        switch(NodeType){
            case Expressions.ExpressionType.Assign:{
                var (Left,Right)=this.Deserialize_Binary(ref reader,Resolver);
                return Expressions.Expression.Assign(Left,Right);
            }
            case Expressions.ExpressionType.Coalesce:{
                var (Left,Right)=this.Deserialize_Binary(ref reader,Resolver);
                return Expressions.Expression.Coalesce(Left,Right);
            }
            case Expressions.ExpressionType.Add:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Add(Left,Right,Method);
            }
            case Expressions.ExpressionType.AddAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.AddAssign(Left,Right,Method);
            }
            case Expressions.ExpressionType.AddAssignChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.AddAssignChecked(Left,Right,Method);
            }
            case Expressions.ExpressionType.AddChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.AddChecked(Left,Right,Method);
            }
            case Expressions.ExpressionType.And:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.And(Left,Right,Method);
            }
            case Expressions.ExpressionType.AndAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.AndAssign(Left,Right,Method);
            }
            case Expressions.ExpressionType.AndAlso:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.AndAlso(Left,Right,Method);
            }
            case Expressions.ExpressionType.Divide:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Divide(Left,Right,Method);
            }
            case Expressions.ExpressionType.DivideAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.DivideAssign(Left,Right,Method);
            }
            case Expressions.ExpressionType.ExclusiveOr:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.ExclusiveOr(Left,Right,Method);
            }
            case Expressions.ExpressionType.ExclusiveOrAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.ExclusiveOrAssign(Left,Right,Method);
            }
            case Expressions.ExpressionType.LeftShift:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.LeftShift(Left,Right,Method);
            }
            case Expressions.ExpressionType.LeftShiftAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.LeftShiftAssign(Left,Right,Method);
            }
            case Expressions.ExpressionType.Modulo:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Modulo(Left,Right,Method);
            }
            case Expressions.ExpressionType.ModuloAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.ModuloAssign(Left,Right,Method);
            }
            case Expressions.ExpressionType.Multiply:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Multiply(Left,Right,Method);
            }
            case Expressions.ExpressionType.MultiplyAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.MultiplyAssign(Left,Right,Method);
            }
            case Expressions.ExpressionType.MultiplyAssignChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.MultiplyAssignChecked(Left,Right,Method);
            }
            case Expressions.ExpressionType.MultiplyChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.MultiplyChecked(Left,Right,Method);
            }
            case Expressions.ExpressionType.Or:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Or(Left,Right,Method);
            }
            case Expressions.ExpressionType.OrAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.OrAssign(Left,Right,Method);
            }
            case Expressions.ExpressionType.OrElse:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.OrElse(Left,Right,Method);
            }
            case Expressions.ExpressionType.Power:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Power(Left,Right,Method);
            }
            case Expressions.ExpressionType.PowerAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.PowerAssign(Left,Right,Method);
            }
            case Expressions.ExpressionType.RightShift:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.RightShift(Left,Right,Method);
            }
            case Expressions.ExpressionType.RightShiftAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.RightShiftAssign(Left,Right,Method);
            }
            case Expressions.ExpressionType.Subtract:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Subtract(Left,Right,Method);
            }
            case Expressions.ExpressionType.SubtractAssign:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.SubtractAssign(Left,Right,Method);
            }
            case Expressions.ExpressionType.SubtractAssignChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.SubtractAssignChecked(Left,Right,Method);
            }
            case Expressions.ExpressionType.SubtractChecked:{
                var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.SubtractChecked(Left,Right,Method);
            }
            case Expressions.ExpressionType.Equal:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Equal(Left,Right,IsLiftedToNull,Method);
            }
            case Expressions.ExpressionType.GreaterThan:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.GreaterThan(Left,Right,IsLiftedToNull,Method);
            }
            case Expressions.ExpressionType.GreaterThanOrEqual:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.GreaterThanOrEqual(Left,Right,IsLiftedToNull,Method);
            }
            case Expressions.ExpressionType.LessThan:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.LessThan(Left,Right,IsLiftedToNull,Method);
            }
            case Expressions.ExpressionType.LessThanOrEqual:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.LessThanOrEqual(Left,Right,IsLiftedToNull,Method);
            }
            case Expressions.ExpressionType.NotEqual:{
                var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.NotEqual(Left,Right,IsLiftedToNull,Method);
            }
            case Expressions.ExpressionType.ArrayIndex:{
                var (array,index)=this.Deserialize_Binary(ref reader,Resolver);
                return Expressions.Expression.ArrayIndex(array,index);
            }
        }
        throw new NotSupportedException(NodeType.ToString());
    }
}
