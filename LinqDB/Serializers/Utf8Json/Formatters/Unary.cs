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
        Expression.Instance.Serialize(ref writer,value.Operand,Resolver);
    }
    internal static void InternalSerializeType(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        Expression.Instance.Serialize(ref writer,value.Operand,Resolver);
        writer.WriteValueSeparator();
        writer.WriteType(value.Type);
    }
    internal static void InternalSerializeMethod(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        Expression.Instance.Serialize(ref writer,value.Operand,Resolver);
        writer.WriteValueSeparator();
        Method.Instance.SerializeNullable(ref writer,value.Method,Resolver);
    }
    internal static void InternalSerializeTypeMethod(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        Expression.Instance.Serialize(ref writer,value.Operand,Resolver);
        writer.WriteValueSeparator();
        writer.WriteType(value.Type);
        writer.WriteValueSeparator();
        Method.Instance.SerializeNullable(ref writer,value.Method,Resolver);
    }
    //internal static Expressions.Expression InternalDeserialize(ref Reader reader,IJsonFormatterResolver Resolver) => Expression.Instance.Deserialize(ref reader,Resolver);
    //internal static (Expressions.Expression Operand, System.Type Type) InternalDeserializeType(ref Reader reader,IJsonFormatterResolver Resolver) {
    //    var operand = Expression.Instance.Deserialize(ref reader,Resolver);
    //    reader.ReadIsValueSeparatorWithVerify();
    //    var type = Type.Instance.Deserialize(ref reader,Resolver);
    //    return (operand, type);
    //}
    //internal static (Expressions.Expression Operand, MethodInfo? Method) InternalDeserializeMethod(ref Reader reader,IJsonFormatterResolver Resolver) {
    //    var operand = Expression.Instance.Deserialize(ref reader,Resolver);
    //    reader.ReadIsValueSeparatorWithVerify();
    //    var method = Method.Instance.DeserializeNullable(ref reader,Resolver);
    //    return (operand, method);
    //}
    //internal static (Expressions.Expression Operand, System.Type Type, MethodInfo? Method) InternalDeserializeTypeMethod(ref Reader reader,IJsonFormatterResolver Resolver) {
    //    var operand = Expression.Instance.Deserialize(ref reader,Resolver);
    //    reader.ReadIsValueSeparatorWithVerify();
    //    var type = Type.Instance.Deserialize(ref reader,Resolver);
    //    reader.ReadIsValueSeparatorWithVerify();
    //    var method = Method.Instance.DeserializeNullable(ref reader,Resolver);
    //    return (operand, type, method);
    //}
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        //if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        writer.WriteString(value.NodeType.ToString());
        writer.WriteValueSeparator();
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
    internal static (Expressions.Expression Operand,System.Type Type)InternalDeserializeType(ref Reader reader,IJsonFormatterResolver Resolver){
        var operand= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var type=reader.ReadType();
        return(operand,type);
    }
    internal static (Expressions.Expression Operand,MethodInfo Method)InternalDeserializeMethod(ref Reader reader,IJsonFormatterResolver Resolver){
        var operand= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var method=Method.Instance.Deserialize(ref reader,Resolver);
        return(operand,method);
    }
    internal static (Expressions.Expression Operand,System.Type Type,MethodInfo Method)InternalDeserializeTypeMethod(ref Reader reader,IJsonFormatterResolver Resolver){
        var operand= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var method=Method.Instance.Deserialize(ref reader,Resolver);
        return(operand,type,method);
    }
    //internal static Expressions.Expression InternalDeserialize(ref Reader reader,IJsonFormatterResolver Resolver) => Expression.Instance.Deserialize(ref reader,Resolver);
    //internal static (Expressions.Expression Operand, System.Type Type) InternalDeserializeType(ref Reader reader,IJsonFormatterResolver Resolver) {
    //    var operand = Expression.Instance.Deserialize(ref reader,Resolver);
    //    reader.ReadIsValueSeparatorWithVerify();
    //    var type = Type.Instance.Deserialize(ref reader,Resolver);
    //    return (operand, type);
    //}
    //internal static (Expressions.Expression Operand, MethodInfo? Method) InternalDeserializeMethod(ref Reader reader,IJsonFormatterResolver Resolver) {
    //    var operand = Expression.Instance.Deserialize(ref reader,Resolver);
    //    reader.ReadIsValueSeparatorWithVerify();
    //    var method = Method.Instance.DeserializeNullable(ref reader,Resolver);
    //    return (operand, method);
    //}
    //internal static (Expressions.Expression Operand, System.Type Type, MethodInfo? Method) InternalDeserializeTypeMethod(ref Reader reader,IJsonFormatterResolver Resolver) {
    //    var operand = Expression.Instance.Deserialize(ref reader,Resolver);
    //    reader.ReadIsValueSeparatorWithVerify();
    //    var type = Type.Instance.Deserialize(ref reader,Resolver);
    //    reader.ReadIsValueSeparatorWithVerify();
    //    var method = Method.Instance.DeserializeNullable(ref reader,Resolver);
    //    return (operand, type, method);
    //}
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        //if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var NodeTypeName=reader.ReadString();
        var NodeType=Enum.Parse<Expressions.ExpressionType>(NodeTypeName);
        reader.ReadIsValueSeparatorWithVerify();
        T value;
        switch(NodeType){
            //case Expressions.ExpressionType.ArrayLength        :
            //case Expressions.ExpressionType.Quote              :{
            //    var Operand= Deserialize_Unary(ref reader,Resolver);
            //    value=Expressions.Expression.ArrayLength(Operand);break;
            //}
            //case Expressions.ExpressionType.Throw              :
            //case Expressions.ExpressionType.TypeAs             :
            //case Expressions.ExpressionType.Unbox              :{
            //    var (Operand,Type)=Deserialize_Unary_Type(ref reader,Resolver);
            //    value=Expressions.Expression.MakeUnary(NodeType,Operand,Type);break;
            //}
            //case Expressions.ExpressionType.Convert            :
            //case Expressions.ExpressionType.ConvertChecked     :{
            //    var(Operand,Type,Method)=Deserialize_Unary_Type_MethodInfo(ref reader,Resolver);
            //    value=Expressions.Expression.MakeUnary(NodeType,Operand,Type,Method);break;
            //}
            //case Expressions.ExpressionType.Decrement          :
            //case Expressions.ExpressionType.Increment          :
            //case Expressions.ExpressionType.IsFalse            :
            //case Expressions.ExpressionType.IsTrue             :
            //case Expressions.ExpressionType.Negate             :
            //case Expressions.ExpressionType.NegateChecked      :
            //case Expressions.ExpressionType.Not                :
            //case Expressions.ExpressionType.OnesComplement     :
            //case Expressions.ExpressionType.PostDecrementAssign:
            //case Expressions.ExpressionType.PostIncrementAssign:
            //case Expressions.ExpressionType.PreDecrementAssign :
            //case Expressions.ExpressionType.PreIncrementAssign :
            //case Expressions.ExpressionType.UnaryPlus          :{
            //    var (Operand,Method)=Deserialize_Unary_MethodInfo(ref reader,Resolver);
            //    value=Expressions.Expression.MakeUnary(NodeType,Operand,Operand.Type,Method);break;
            //}
            case Expressions.ExpressionType.ArrayLength: {
                var Operand = InternalDeserialize(ref reader,Resolver);
                value=Expressions.Expression.ArrayLength(Operand);break;
            }
            case Expressions.ExpressionType.Quote: {
                var Operand = InternalDeserialize(ref reader,Resolver);
                value=Expressions.Expression.Quote(Operand );
                break;
            }
            case Expressions.ExpressionType.Convert: {
                var (Operand, Type, Method)=InternalDeserializeTypeMethod(ref reader,Resolver);
                value=Expressions.Expression.Convert(Operand,Type,Method);break;
            }
            case Expressions.ExpressionType.ConvertChecked: {
                var (Operand, Type, Method)=InternalDeserializeTypeMethod(ref reader,Resolver);
                value=Expressions.Expression.ConvertChecked(Operand,Type,Method);break;
            }
            case Expressions.ExpressionType.Decrement: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Decrement(Operand,Method);break;
            }
            case Expressions.ExpressionType.Increment: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Increment(Operand,Method);break;
            }
            case Expressions.ExpressionType.IsFalse: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.IsFalse(Operand,Method);break;
            }
            case Expressions.ExpressionType.IsTrue: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.IsTrue(Operand,Method);break;
            }
            case Expressions.ExpressionType.Negate: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Negate(Operand,Method);break;
            }
            case Expressions.ExpressionType.NegateChecked: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.NegateChecked(Operand,Method);break;
            }
            case Expressions.ExpressionType.Not: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Not(Operand,Method);break;
            }
            case Expressions.ExpressionType.OnesComplement: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.OnesComplement(Operand,Method);break;
            }
            case Expressions.ExpressionType.PostDecrementAssign: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.PostDecrementAssign(Operand,Method);break;
            }
            case Expressions.ExpressionType.PostIncrementAssign: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.PostIncrementAssign(Operand,Method);break;
            }
            case Expressions.ExpressionType.PreDecrementAssign: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.PreDecrementAssign(Operand,Method);break;
            }
            case Expressions.ExpressionType.PreIncrementAssign: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.PreIncrementAssign(Operand,Method);break;
            }
            case Expressions.ExpressionType.Throw: {
                var (Operand, Type)=InternalDeserializeType(ref reader,Resolver);
                value=Expressions.Expression.Throw(Operand,Type);break;
            }
            case Expressions.ExpressionType.TypeAs: {
                var (Operand, Type)=InternalDeserializeType(ref reader,Resolver);
                value=Expressions.Expression.TypeAs(Operand,Type);break;
            }
            case Expressions.ExpressionType.UnaryPlus: {
                var (Operand, Method)=InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.UnaryPlus(Operand,Method);break;
            }
            case Expressions.ExpressionType.Unbox: {
                var (Operand, Type)=InternalDeserializeType(ref reader,Resolver);
                value=Expressions.Expression.Unbox(Operand,Type);break;
            }
            default:throw new NotSupportedException(NodeTypeName);
        }
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
