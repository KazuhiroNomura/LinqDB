﻿using System;
using System.Reflection;
using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.UnaryExpression;
public class Unary:IMessagePackFormatter<T> {
    public static readonly Unary Instance=new();
    internal static void Write(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(2);
        writer.WriteNodeType(value.NodeType);
        Expression.Write(ref writer,value.Operand,Resolver);
    }
    internal static void WriteType(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(3);
        writer.WriteNodeType(value.NodeType);
        Expression.Write(ref writer,value.Operand,Resolver);
        
        writer.WriteType(value.Type);
    }
    internal static void WriteMethod(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(3);
        writer.WriteNodeType(value.NodeType);
        Expression.Write(ref writer,value.Operand,Resolver);
        
        Method.WriteNullable(ref writer,value.Method,Resolver);
    }
    internal static void WriteTypeMethod(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(4);
        writer.WriteNodeType(value.NodeType);
        Expression.Write(ref writer,value.Operand,Resolver);
        
        writer.WriteType(value.Type);
        
        Method.WriteNullable(ref writer,value.Method,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        if(writer.TryWriteNil(value)) return;

        switch(value!.NodeType){
            case Expressions.ExpressionType.ArrayLength        :
            case Expressions.ExpressionType.Quote              :Write(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Throw              :
            case Expressions.ExpressionType.TypeAs             :
            case Expressions.ExpressionType.Unbox              :WriteType(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Convert            :
            case Expressions.ExpressionType.ConvertChecked     :WriteTypeMethod(ref writer,value,Resolver);break;
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
            case Expressions.ExpressionType.UnaryPlus          :WriteMethod(ref writer,value,Resolver);break;
            default:
                throw new NotSupportedException(value.NodeType.ToString());
        }
        
    }
    internal static Expressions.Expression ReadOperand(ref Reader reader,MessagePackSerializerOptions Resolver){
        var operand=Expression.Read(ref reader,Resolver);
        return operand;
    }
    internal static (Expressions.Expression Operand,System.Type Type)ReadOperandType(ref Reader reader,MessagePackSerializerOptions Resolver){
        var operand= Expression.Read(ref reader,Resolver);
        
        var type=reader.ReadType();
        return(operand,type);
    }
    internal static (Expressions.Expression Operand,MethodInfo? Method)ReadOperandMethod(ref Reader reader,MessagePackSerializerOptions Resolver){
        var operand= Expression.Read(ref reader,Resolver);
        
        var method=Method.ReadNullable(ref reader,Resolver);
        return(operand,method);
    }
    internal static (Expressions.Expression Operand,System.Type Type,MethodInfo? Method)ReadOperandTypeMethod(ref Reader reader,MessagePackSerializerOptions Resolver){
        var operand= Expression.Read(ref reader,Resolver);
        
        var type=reader.ReadType();
        
        var method=Method.ReadNullable(ref reader,Resolver);
        return(operand,type,method);
    }
    private static T Read(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();
        Debug.Assert(count is >=2 and <=4);
        T value;
        var NodeType=reader.ReadNodeType();
        switch(NodeType){
            case Expressions.ExpressionType.ArrayLength: {
                var operand = ReadOperand(ref reader,Resolver);
                value=Expressions.Expression.ArrayLength(operand);break;
            }
            case Expressions.ExpressionType.Quote: {
                var operand = ReadOperand(ref reader,Resolver);
                value=Expressions.Expression.Quote(operand);break;
            }
            case Expressions.ExpressionType.Convert:{
                var (operand,type,method)=ReadOperandTypeMethod(ref reader,Resolver);
                value=Expressions.Expression.Convert(operand,type,method);break;
            }
            case Expressions.ExpressionType.ConvertChecked: {
                var (operand,type,method)=ReadOperandTypeMethod(ref reader,Resolver);
                value=Expressions.Expression.ConvertChecked(operand,type,method);break;
            }
            case Expressions.ExpressionType.Decrement: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.Decrement(operand,method);break;
            }
            case Expressions.ExpressionType.Increment: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.Increment(operand,method);break;
            }
            case Expressions.ExpressionType.IsFalse: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.IsFalse(operand,method);break;
            }
            case Expressions.ExpressionType.IsTrue: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.IsTrue(operand,method);break;
            }
            case Expressions.ExpressionType.Negate: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.Negate(operand,method);break;
            }
            case Expressions.ExpressionType.NegateChecked: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.NegateChecked(operand,method);break;
            }
            case Expressions.ExpressionType.Not: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.Not(operand,method);break;
            }
            case Expressions.ExpressionType.OnesComplement: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.OnesComplement(operand,method);break;
            }
            case Expressions.ExpressionType.PostDecrementAssign: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.PostDecrementAssign(operand,method);break;
            }
            case Expressions.ExpressionType.PostIncrementAssign: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.PostIncrementAssign(operand,method);break;
            }
            case Expressions.ExpressionType.PreDecrementAssign: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.PreDecrementAssign(operand,method);break;
            }
            case Expressions.ExpressionType.PreIncrementAssign: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.PreIncrementAssign(operand,method);break;
            }
            case Expressions.ExpressionType.UnaryPlus: {
                var (operand,method)=ReadOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.UnaryPlus(operand,method);break;
            }
            case Expressions.ExpressionType.Throw: {
                var (operand,type)=ReadOperandType(ref reader,Resolver);
                value=Expressions.Expression.Throw(operand,type);break;
            }
            case Expressions.ExpressionType.TypeAs: {
                var (operand,type)=ReadOperandType(ref reader,Resolver);
                value=Expressions.Expression.TypeAs(operand,type);break;
            }
            case Expressions.ExpressionType.Unbox: {
                var (operand,type)=ReadOperandType(ref reader,Resolver);
                value=Expressions.Expression.Unbox(operand,type);break;
            }
            default:throw new NotSupportedException(NodeType.ToString());
        }
        return value;
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver)=>reader.TryReadNil()?null!:Read(ref reader,Resolver);
}
