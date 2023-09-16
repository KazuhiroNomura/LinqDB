using System;
using System.Diagnostics;
using Expressions=System.Linq.Expressions;
using System.Reflection;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using T=Expressions.UnaryExpression;
using Writer=JsonWriter;
using Reader=JsonReader;
public class Unary:IJsonFormatter<T> {
    public static readonly Unary Instance=new();
    internal static void InternalSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        Expression.Instance.Serialize(ref writer,value.Operand,Resolver);
    }
    internal static void InternalSerializeType(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        Expression.Instance.Serialize(ref writer,value.Operand,Resolver);
        writer.WriteValueSeparator();
        writer.WriteType(value.Type);
    }
    internal static void InternalSerializeMethod(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        Expression.Instance.Serialize(ref writer,value.Operand,Resolver);
        writer.WriteValueSeparator();
        Method.Instance.SerializeNullable(ref writer,value.Method,Resolver);
    }
    internal static void InternalSerializeTypeMethod(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        Expression.Instance.Serialize(ref writer,value.Operand,Resolver);
        writer.WriteValueSeparator();
        writer.WriteType(value.Type);
        writer.WriteValueSeparator();
        Method.Instance.SerializeNullable(ref writer,value.Method,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        //if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        switch(value.NodeType){
            case Expressions.ExpressionType.ArrayLength        : 
            case Expressions.ExpressionType.Quote              : InternalSerialize(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Throw              : 
            case Expressions.ExpressionType.TypeAs             : 
            case Expressions.ExpressionType.Unbox              : InternalSerializeType(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Convert            : 
            case Expressions.ExpressionType.ConvertChecked     : InternalSerializeTypeMethod(ref writer,value,Resolver);break;
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
            case Expressions.ExpressionType.UnaryPlus          : InternalSerializeMethod(ref writer,value,Resolver);break;
            default:
                throw new NotSupportedException(value.NodeType.ToString());
        }
        writer.WriteEndArray();
    }
    internal static Expressions.Expression InternalDeserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        var Operand= Expression.Instance.Deserialize(ref reader,Resolver);
        return Operand;
    }
    internal static (Expressions.Expression operand,System.Type type)InternalDeserializeType(ref Reader reader,IJsonFormatterResolver Resolver){
        var operand= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var type=reader.ReadType();
        return(operand,type);
    }
    internal static (Expressions.Expression operand,MethodInfo method)InternalDeserializeMethod(ref Reader reader,IJsonFormatterResolver Resolver){
        var operand= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var method=Method.Instance.Deserialize(ref reader,Resolver);
        return(operand,method);
    }
    internal static (Expressions.Expression operand,System.Type type,MethodInfo method)InternalDeserializeTypeMethod(ref Reader reader,IJsonFormatterResolver Resolver){
        var operand= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var method=Method.Instance.Deserialize(ref reader,Resolver);
        return(operand,type,method);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        //if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var NodeTypeName=reader.ReadString();
        var NodeType=Enum.Parse<Expressions.ExpressionType>(NodeTypeName);
        reader.ReadIsValueSeparatorWithVerify();
        T value;
        switch(NodeType){
            case Expressions.ExpressionType.ArrayLength: {
                var operand = InternalDeserialize(ref reader,Resolver);
                value=Expressions.Expression.ArrayLength(operand);break;
            }
            case Expressions.ExpressionType.Quote: {
                var operand = InternalDeserialize(ref reader,Resolver);
                value=Expressions.Expression.Quote(operand );
                break;
            }
            case Expressions.ExpressionType.Convert: {
                var (operand, type, Method)=InternalDeserializeTypeMethod(ref reader,Resolver);
                value=Expressions.Expression.Convert(operand,type,Method);break;
            }
            case Expressions.ExpressionType.ConvertChecked: {
                var (operand, type, Method)=InternalDeserializeTypeMethod(ref reader,Resolver);
                value=Expressions.Expression.ConvertChecked(operand,type,Method);break;
            }
            case Expressions.ExpressionType.Decrement: {
                var (operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Decrement(operand,Method);break;
            }
            case Expressions.ExpressionType.Increment: {
                var (operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Increment(operand,Method);break;
            }
            case Expressions.ExpressionType.IsFalse: {
                var (operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.IsFalse(operand,Method);break;
            }
            case Expressions.ExpressionType.IsTrue: {
                var (operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.IsTrue(operand,Method);break;
            }
            case Expressions.ExpressionType.Negate: {
                var (operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Negate(operand,Method);break;
            }
            case Expressions.ExpressionType.NegateChecked: {
                var (operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.NegateChecked(operand,Method);break;
            }
            case Expressions.ExpressionType.Not: {
                var (operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Not(operand,Method);break;
            }
            case Expressions.ExpressionType.OnesComplement: {
                var (operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.OnesComplement(operand,Method);break;
            }
            case Expressions.ExpressionType.PostDecrementAssign: {
                var (operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.PostDecrementAssign(operand,Method);break;
            }
            case Expressions.ExpressionType.PostIncrementAssign: {
                var (operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.PostIncrementAssign(operand,Method);break;
            }
            case Expressions.ExpressionType.PreDecrementAssign: {
                var (operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.PreDecrementAssign(operand,Method);break;
            }
            case Expressions.ExpressionType.PreIncrementAssign: {
                var (operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.PreIncrementAssign(operand,Method);break;
            }
            case Expressions.ExpressionType.Throw: {
                var (operand, Type)=InternalDeserializeType(ref reader,Resolver);
                value=Expressions.Expression.Throw(operand,Type);break;
            }
            case Expressions.ExpressionType.TypeAs: {
                var (operand, Type)=InternalDeserializeType(ref reader,Resolver);
                value=Expressions.Expression.TypeAs(operand,Type);break;
            }
            case Expressions.ExpressionType.UnaryPlus: {
                var (operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.UnaryPlus(operand,Method);break;
            }
            case Expressions.ExpressionType.Unbox: {
                var (operand, Type)=InternalDeserializeType(ref reader,Resolver);
                value=Expressions.Expression.Unbox(operand,Type);break;
            }
            default:throw new NotSupportedException(NodeTypeName);
        }
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
