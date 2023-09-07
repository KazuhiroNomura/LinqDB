using System;
using Expressions=System.Linq.Expressions;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
using MemoryPack;

namespace LinqDB.Serializers.MessagePack.Formatters;
using C=MessagePackCustomSerializer;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.UnaryExpression;
public class Unary:IMessagePackFormatter<Expressions.UnaryExpression>{
    public static readonly CatchBlock Instance=new();
    internal static void Serialize_Unary(ref Writer writer,Expressions.Expression value,MessagePackSerializerOptions Resolver){
        var Unary=(Expressions.UnaryExpression)value;
        Expression.Instance.Serialize(ref writer,Unary.Operand,Resolver);
    }
    internal static void Serialize_Unary_Type(ref Writer writer,Expressions.Expression value,MessagePackSerializerOptions Resolver){
        var Unary=(Expressions.UnaryExpression)value;
        Expression.Instance.Serialize(ref writer,Unary.Operand,Resolver);
        Type.Instance.Serialize(ref writer,Unary.Type,Resolver);
    }
    internal static void Serialize_Unary_MethodInfo(ref Writer writer,Expressions.Expression value,MessagePackSerializerOptions Resolver){
        var Unary=(Expressions.UnaryExpression)value;
        Expression.Instance.Serialize(ref writer,Unary.Operand,Resolver);
        Method.Instance.Serialize(ref writer,Unary.Method,Resolver);
    }
    internal static void Serialize_Unary_Type_MethodInfo(ref Writer writer,Expressions.Expression value,MessagePackSerializerOptions Resolver){
        var Unary=(Expressions.UnaryExpression)value;
        Expression.Instance.Serialize(ref writer,Unary.Operand,Resolver);
        Type.Instance.Serialize(ref writer,Unary.Type,Resolver);
        Method.Instance.Serialize(ref writer,Unary.Method,Resolver);
    }
    //internal readonly object[] Objects2=new object[2];
    internal static Expressions.Expression Deserialize_Unary(ref Reader reader,MessagePackSerializerOptions Resolver){
        var Operand= Expression.Instance.Deserialize(ref reader,Resolver);
        return Operand;
    }
    internal static (Expressions.Expression Operand,System.Type Type)Deserialize_Unary_Type(ref Reader reader,MessagePackSerializerOptions Resolver){
        var Operand= Expression.Instance.Deserialize(ref reader,Resolver);
        var Type=reader.ReadType();
        return(Operand,Type);
    }
    internal static (Expressions.Expression Operand,MethodInfo Method)Deserialize_Unary_MethodInfo(ref Reader reader,MessagePackSerializerOptions Resolver){
        var Operand= Expression.Instance.Deserialize(ref reader,Resolver);
        var method=Method.Instance.Deserialize(ref reader,Resolver);
        return(Operand,method);
    }
    internal static (Expressions.Expression Operand,System.Type Type,MethodInfo Method)Deserialize_Unary_Type_MethodInfo(ref Reader reader,MessagePackSerializerOptions Resolver){
        var Operand= Expression.Instance.Deserialize(ref reader,Resolver);
        var Type=reader.ReadType();
        var method=Method.Instance.Deserialize(ref reader,Resolver);
        return(Operand,Type,method);
    }
    public void Serialize(ref Writer writer,Expressions.UnaryExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        writer.Write((byte)value.NodeType);
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
    }
    public Expressions.UnaryExpression Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var NodeType=(Expressions.ExpressionType)reader.ReadByte();
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
            default:throw new NotSupportedException(NodeType.ToString());
        }
    }
}
