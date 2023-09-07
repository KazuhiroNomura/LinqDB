using System;
using Expressions=System.Linq.Expressions;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
using MemoryPack;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using T=Expressions.UnaryExpression;
using static Utf8JsonCustomSerializer;
using Writer=JsonWriter;
using Reader=JsonReader;
public class Unary:IJsonFormatter<T> {
    public static readonly Unary Instance=new();
    public static void Serialize_Unary(ref Writer writer,T Unary,IJsonFormatterResolver Resolver){
        Expression.Instance.Serialize(ref writer,Unary.Operand,Resolver);
    }
    public static void Serialize_Unary_Type(ref Writer writer,T Unary,IJsonFormatterResolver Resolver){
        Expression.Instance.Serialize(ref writer,Unary.Operand,Resolver);
        writer.WriteValueSeparator();
        Type.Instance.Serialize(ref writer,Unary.Type,Resolver);
    }
    public static void Serialize_Unary_MethodInfo(ref Writer writer,T Unary,IJsonFormatterResolver Resolver){
        Expression.Instance.Serialize(ref writer,Unary.Operand,Resolver);
        writer.WriteValueSeparator();
        Method.Instance.Serialize(ref writer,Unary.Method,Resolver);
    }
    public static void Serialize_Unary_Type_MethodInfo(ref Writer writer,T Unary,IJsonFormatterResolver Resolver){
        Expression.Instance.Serialize(ref writer,Unary.Operand,Resolver);
        writer.WriteValueSeparator();
        Type.Instance.Serialize(ref writer,Unary.Type,Resolver);
        writer.WriteValueSeparator();
        Method.Instance.Serialize(ref writer,Unary.Method,Resolver);
    }
    //public static Expressions.Expression Deserialize_Unary(ref Reader reader,IJsonFormatterResolver Resolve) => Expression.Instance.Deserialize(ref reader);
    //public static (Expressions.Expression Operand, System.Type Type) Deserialize_Unary_Type(ref Reader reader,IJsonFormatterResolver Resolve) {
    //    var operand = Expression.Instance.Deserialize(ref reader);
    //    var type = Type.Instance.Deserialize(ref reader);
    //    return (operand, type);
    //}
    //public static (Expressions.Expression Operand, MethodInfo? Method) Deserialize_Unary_MethodInfo(ref Reader reader,IJsonFormatterResolver Resolve) {
    //    var operand = Expression.Instance.Deserialize(ref reader);
    //    var method = Method.Instance.DeserializeNullable(ref reader);
    //    return (operand, method);
    //}
    //public static (Expressions.Expression Operand, System.Type Type, MethodInfo? Method) Deserialize_Unary_Type_MethodInfo(ref Reader reader,IJsonFormatterResolver Resolve) {
    //    var operand = Expression.Instance.Deserialize(ref reader);
    //    var type = Type.Instance.Deserialize(ref reader);
    //    var method = Method.Instance.DeserializeNullable(ref reader);
    //    return (operand, type, method);
    //}
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        writer.WriteString(value.NodeType.ToString());
        writer.WriteValueSeparator();
        switch(value.NodeType){
            case Expressions.ExpressionType.ArrayLength        : Serialize_Unary(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Quote              : Serialize_Unary(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Throw              : Serialize_Unary_Type(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.TypeAs             : Serialize_Unary_Type(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Unbox              : Serialize_Unary_Type(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Convert            : Serialize_Unary_Type_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.ConvertChecked     : Serialize_Unary_Type_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Decrement          : Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Increment          : Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.IsFalse            : Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.IsTrue             : Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Negate             : Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.NegateChecked      : Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Not                : Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.OnesComplement     : Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.PostDecrementAssign: Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.PostIncrementAssign: Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.PreDecrementAssign : Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.PreIncrementAssign : Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.UnaryPlus          : Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            default:
                throw new NotSupportedException(value.NodeType.ToString());
        }
        writer.WriteEndArray();
    }
    internal static Expressions.Expression Deserialize_Unary(ref Reader reader,IJsonFormatterResolver Resolver){
        var Operand= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Operand;
    }
    internal static (Expressions.Expression Operand,System.Type Type)Deserialize_Unary_Type(ref Reader reader,IJsonFormatterResolver Resolver){
        var Operand= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var type=reader.ReadType();
        reader.ReadIsEndArrayWithVerify();
        return(Operand,type);
    }
    internal static (Expressions.Expression Operand,MethodInfo Method)Deserialize_Unary_MethodInfo(ref Reader reader,IJsonFormatterResolver Resolver){
        var Operand= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var method=Method.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return(Operand,method);
    }
    internal static (Expressions.Expression Operand,System.Type Type,MethodInfo Method)Deserialize_Unary_Type_MethodInfo(ref Reader reader,IJsonFormatterResolver Resolver){
        var Operand= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var method=Method.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return(Operand,type,method);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var NodeTypeName=reader.ReadString();
        var NodeType=Enum.Parse<Expressions.ExpressionType>(NodeTypeName);
        reader.ReadIsValueSeparatorWithVerify();
        switch(NodeType){
            case Expressions.ExpressionType.ArrayLength        :
            case Expressions.ExpressionType.Quote              :{
                var Operand= Deserialize_Unary(ref reader,Resolver);
                return Expressions.Expression.ArrayLength(Operand);
            }
            case Expressions.ExpressionType.Throw              :
            case Expressions.ExpressionType.TypeAs             :
            case Expressions.ExpressionType.Unbox              :{
                var (Operand,Type)=Deserialize_Unary_Type(ref reader,Resolver);
                return Expressions.Expression.MakeUnary(NodeType,Operand,Type);
            }
            case Expressions.ExpressionType.Convert            :
            case Expressions.ExpressionType.ConvertChecked     :{
                var(Operand,Type,Method)=Deserialize_Unary_Type_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.ConvertChecked(Operand,Type,Method);
            }
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
            case Expressions.ExpressionType.UnaryPlus          :{
                var (Operand,Method)=Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.PreIncrementAssign(Operand,Method);
            }
        //    case Expressions.ExpressionType.ArrayLength        :{
        //        var Operand= this.Deserialize_Unary(ref reader,Resolver);
        //        return Expressions.Expression.ArrayLength(Operand);
        //    }
        //    case Expressions.ExpressionType.Convert            :{
        //        var(Operand,Type,Method)=this.Deserialize_Unary_Type_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.Convert(Operand,Type,Method);
        //    }
        //    case Expressions.ExpressionType.ConvertChecked     :{
        //        var(Operand,Type,Method)=this.Deserialize_Unary_Type_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.ConvertChecked(Operand,Type,Method);
        //    }
        //    case Expressions.ExpressionType.Decrement          :{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.Decrement(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.Increment          :{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.Increment(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.IsFalse            :{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.IsFalse(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.IsTrue             :{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.IsTrue(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.Negate             :{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.Negate(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.NegateChecked      :{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.NegateChecked(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.Not                :{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.Not(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.OnesComplement     :{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.OnesComplement(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.PostDecrementAssign:{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.PostDecrementAssign(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.PostIncrementAssign:{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.PostIncrementAssign(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.PreDecrementAssign :{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.PreDecrementAssign(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.PreIncrementAssign :{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.PreIncrementAssign(Operand,Method);
        //}
        //    case Expressions.ExpressionType.Quote:{
        //        var result=Expressions.Expression.Quote(this.Deserialize_Unary(ref reader,Resolver));
        //        reader.ReadIsEndArrayWithVerify();
        //        return result;
        //    }
        //    case Expressions.ExpressionType.Throw              :{
        //        var (Operand,Type)=this.Deserialize_Unary_Type(ref reader,Resolver);
        //        return Expressions.Expression.Throw(Operand,Type);
        //    }
        //    case Expressions.ExpressionType.TypeAs             :{
        //        var (Operand,Type)=this.Deserialize_Unary_Type(ref reader,Resolver);
        //        return Expressions.Expression.TypeAs(Operand,Type);
        //    }
        //    case Expressions.ExpressionType.UnaryPlus:{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.UnaryPlus(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.Unbox              :{
        //        var (Operand,Type)=this.Deserialize_Unary_Type(ref reader,Resolver);
        //        return Expressions.Expression.Unbox(Operand,Type);
        //    }
            default:throw new NotSupportedException(NodeTypeName);
        }
    }
}
