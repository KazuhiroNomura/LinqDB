﻿using System;
using System.Buffers;
using System.Diagnostics;
using System.Reflection;
using MemoryPack;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;

using Reader=MemoryPackReader;
using T= Expressions.BinaryExpression;
public class Binary:MemoryPackFormatter<T> {
    public static readonly Binary Instance=new();
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter>writer,T value)where TBufferWriter:IBufferWriter<byte>{


        Expression.InternalSerialize(ref writer,value.Left);

        Expression.InternalSerialize(ref writer,value.Right);
    }
    internal static void InternalSerializeLambda<TBufferWriter>(ref MemoryPackWriter<TBufferWriter>writer,T value)where TBufferWriter:IBufferWriter<byte>{


        Expression.InternalSerialize(ref writer,value.Left);

        Expression.InternalSerialize(ref writer,value.Right);

        Lambda.InternalSerializeConversion(ref writer,value.Conversion);
    }
    internal static void InternalSerializeMethod<TBufferWriter>(ref MemoryPackWriter<TBufferWriter>writer,T value)where TBufferWriter:IBufferWriter<byte>{


        Expression.InternalSerialize(ref writer,value.Left);

        Expression.InternalSerialize(ref writer,value.Right);

        Method.InternalSerializeNullable(ref writer,value.Method);
    }
    internal static void InternalSerializeMethodLambda<TBufferWriter>(ref MemoryPackWriter<TBufferWriter>writer,T value)where TBufferWriter:IBufferWriter<byte>{


        Expression.InternalSerialize(ref writer,value.Left);

        Expression.InternalSerialize(ref writer,value.Right);

        Method.InternalSerializeNullable(ref writer,value.Method);

        Lambda.InternalSerializeConversion(ref writer,value.Conversion);
    }
    internal static void InternalSerializeBooleanMethod<TBufferWriter>(ref MemoryPackWriter<TBufferWriter>writer,T value)where TBufferWriter:IBufferWriter<byte>{


        Expression.InternalSerialize(ref writer,value.Left);
        
        Expression.InternalSerialize(ref writer,value.Right);
        
        writer.WriteBoolean(value.IsLiftedToNull);
        
        Method.InternalSerializeNullable(ref writer,value.Method);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteNodeType(value.NodeType);
        
        switch(value.NodeType) {
            case Expressions.ExpressionType.ArrayIndex           :
            case Expressions.ExpressionType.Assign               :InternalSerialize(ref writer,value); break;
            case Expressions.ExpressionType.Coalesce             :InternalSerializeLambda(ref writer,value); break;
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
            case Expressions.ExpressionType.SubtractChecked      :InternalSerializeMethod(ref writer,value); break;
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
            case Expressions.ExpressionType.SubtractAssignChecked:InternalSerializeMethodLambda(ref writer,value); break;
            case Expressions.ExpressionType.Equal                :
            case Expressions.ExpressionType.GreaterThan          :
            case Expressions.ExpressionType.GreaterThanOrEqual   :
            case Expressions.ExpressionType.LessThan             :
            case Expressions.ExpressionType.LessThanOrEqual      :
            case Expressions.ExpressionType.NotEqual             :InternalSerializeBooleanMethod(ref writer,value);break;
            default:throw new NotSupportedException(value.NodeType.ToString());
        }
        
    }
    internal static (Expressions.Expression left,Expressions.Expression right) InternalDeserialize(ref Reader reader) {
        var left = Expression.InternalDeserialize(ref reader);
        
        var right = Expression.InternalDeserialize(ref reader);
        return (left, right);
    }
    internal static (Expressions.Expression left,Expressions.Expression right,Expressions.LambdaExpression? conversion) InternalDeserializeLambda(ref Reader reader) {
        var left = Expression.InternalDeserialize(ref reader);

        var right = Expression.InternalDeserialize(ref reader);

        var conversion= Lambda.InternalDeserializeConversion(ref reader);
        return (left, right, conversion);
    }
    internal static (Expressions.Expression left,Expressions.Expression right,MethodInfo? method) InternalDeserializeMethod(ref Reader reader) {
        var left = Expression.InternalDeserialize(ref reader);
        
        var right = Expression.InternalDeserialize(ref reader);
        
        var method = Method.InternalDeserializeNullable(ref reader);
        return (left, right, method);
    }
    internal static (Expressions.Expression left,Expressions.Expression right,MethodInfo? method,Expressions.LambdaExpression? conversion) InternalDeserializeMethodLambda(ref Reader reader) {
        var left = Expression.InternalDeserialize(ref reader);
        
        var right = Expression.InternalDeserialize(ref reader);
        
        var method = Method.InternalDeserializeNullable(ref reader);
        
        var conversion= Lambda.InternalDeserializeConversion(ref reader);
        return (left, right, method,conversion);
    }
    internal static (Expressions.Expression left,Expressions.Expression right,bool isLiftedToNull,MethodInfo? method) InternalDeserializeBooleanMethod(ref Reader reader){
        var left = Expression.InternalDeserialize(ref reader);
        
        var right = Expression.InternalDeserialize(ref reader);
        
        var isLiftedToNull =reader.ReadBoolean();
        
        var method = Method.InternalDeserializeNullable(ref reader);
        return (left, right, isLiftedToNull, method);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){




        var NodeType=reader.ReadNodeType();
        switch(NodeType) {
            case Expressions.ExpressionType.ArrayIndex: {
                var (array, index)=InternalDeserialize(ref reader);
                value=Expressions.Expression.ArrayIndex(array,index); break;
            }
            case Expressions.ExpressionType.Assign: {
                var (left, right)=InternalDeserialize(ref reader);
                value=Expressions.Expression.Assign(left,right); break;
            }
            case Expressions.ExpressionType.Coalesce: {
                var (left, right)=InternalDeserialize(ref reader);
                value=Expressions.Expression.Coalesce(left,right); break;
            }
            case Expressions.ExpressionType.Add: {
                var (left, right, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.Add(left,right,method); break;
            }
            case Expressions.ExpressionType.AddChecked: {
                var (left, right, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.AddChecked(left,right,method); break;
            }
            case Expressions.ExpressionType.And: {
                var (left, right, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.And(left,right,method); break;
            }
            case Expressions.ExpressionType.AndAlso: {
                var (left, right, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.AndAlso(left,right,method); break;
            }
            case Expressions.ExpressionType.Divide: {
                var (left, right, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.Divide(left,right,method); break;
            }
            case Expressions.ExpressionType.ExclusiveOr: {
                var (left, right, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.ExclusiveOr(left,right,method); break;
            }
            case Expressions.ExpressionType.LeftShift: {
                var (left, right, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.LeftShift(left,right,method); break;
            }
            case Expressions.ExpressionType.Modulo: {
                var (left, right, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.Modulo(left,right,method); break;
            }
            case Expressions.ExpressionType.Multiply: {
                var (left, right, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.Multiply(left,right,method); break;
            }
            case Expressions.ExpressionType.MultiplyChecked: {
                var (left, right, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.MultiplyChecked(left,right,method); break;
            }
            case Expressions.ExpressionType.Or: {
                var (left, right, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.Or(left,right,method); break;
            }
            case Expressions.ExpressionType.OrElse: {
                var (left, right, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.OrElse(left,right,method); break;
            }
            case Expressions.ExpressionType.Power: {
                var (left, right, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.Power(left,right,method); break;
            }
            case Expressions.ExpressionType.RightShift: {
                var (left, right, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.RightShift(left,right,method); break;
            }
            case Expressions.ExpressionType.Subtract: {
                var (left, right, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.Subtract(left,right,method); break;
            }
            case Expressions.ExpressionType.SubtractChecked: {
                var (left, right, method)=InternalDeserializeMethod(ref reader);
                value=Expressions.Expression.SubtractChecked(left,right,method); break;
            }
            case Expressions.ExpressionType.AddAssign: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader);
                value=Expressions.Expression.AddAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.AddAssignChecked: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader);
                value=Expressions.Expression.AddAssignChecked(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.AndAssign: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader);
                value=Expressions.Expression.AndAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.DivideAssign: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader);
                value=Expressions.Expression.DivideAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.ExclusiveOrAssign: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader);
                value=Expressions.Expression.ExclusiveOrAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.LeftShiftAssign: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader);
                value=Expressions.Expression.LeftShiftAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.ModuloAssign: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader);
                value=Expressions.Expression.ModuloAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.MultiplyAssign: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader);
                value=Expressions.Expression.MultiplyAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.MultiplyAssignChecked: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader);
                value=Expressions.Expression.MultiplyAssignChecked(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.OrAssign: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader);
                value=Expressions.Expression.OrAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.PowerAssign: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader);
                value=Expressions.Expression.PowerAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.RightShiftAssign: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader);
                value=Expressions.Expression.RightShiftAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.SubtractAssign: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader);
                value=Expressions.Expression.SubtractAssign(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.SubtractAssignChecked: {
                var (left, right, method,conversion)=InternalDeserializeMethodLambda(ref reader);
                value=Expressions.Expression.SubtractAssignChecked(left,right,method,conversion); break;
            }
            case Expressions.ExpressionType.Equal: {
                var (left, right, isLiftedToNull, method)=InternalDeserializeBooleanMethod(ref reader);
                value=Expressions.Expression.Equal(left,right,isLiftedToNull,method); break;
            }
            case Expressions.ExpressionType.GreaterThan: {
                var (left, right, isLiftedToNull, method)=InternalDeserializeBooleanMethod(ref reader);
                value=Expressions.Expression.GreaterThan(left,right,isLiftedToNull,method); break;
            }
            case Expressions.ExpressionType.GreaterThanOrEqual: {
                var (left, right, isLiftedToNull, method)=InternalDeserializeBooleanMethod(ref reader);
                value=Expressions.Expression.GreaterThanOrEqual(left,right,isLiftedToNull,method); break;
            }
            case Expressions.ExpressionType.LessThan: {
                var (left, right, isLiftedToNull, method)=InternalDeserializeBooleanMethod(ref reader);
                value=Expressions.Expression.LessThan(left,right,isLiftedToNull,method); break;
            }
            case Expressions.ExpressionType.LessThanOrEqual: {
                var (left, right, isLiftedToNull, method)=InternalDeserializeBooleanMethod(ref reader);
                value=Expressions.Expression.LessThanOrEqual(left,right,isLiftedToNull,method); break;
            }
            case Expressions.ExpressionType.NotEqual: {
                var (left, right, isLiftedToNull, method)=InternalDeserializeBooleanMethod(ref reader);
                value=Expressions.Expression.NotEqual(left,right,isLiftedToNull,method); break;
            }
            default: throw new NotSupportedException(NodeType.ToString());
        }
        
        
    }
}
