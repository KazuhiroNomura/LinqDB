using System;
using System.Linq.Expressions;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionFormatter:IJsonFormatter<UnaryExpression>,IMessagePackFormatter<UnaryExpression>{
    private IJsonFormatter<UnaryExpression> Unary=>this;
    private IMessagePackFormatter<UnaryExpression> MSUnary=>this;
    private void Serialize_Unary(ref JsonWriter writer,Expression value,IJsonFormatterResolver Resolver){
        var Unary=(UnaryExpression)value;
        this.Serialize(ref writer,Unary.Operand,Resolver);
    }
    private void Serialize_Unary(ref MessagePackWriter writer,Expression value,MessagePackSerializerOptions Resolver){
        var Unary=(UnaryExpression)value;
        this.Serialize(ref writer,Unary.Operand,Resolver);
    }
    private void Serialize_Unary_Type(ref JsonWriter writer,Expression value,IJsonFormatterResolver Resolver){
        var Unary=(UnaryExpression)value;
        this.Serialize(ref writer,Unary.Operand,Resolver);
        writer.WriteValueSeparator();
        Serialize_Type(ref writer,Unary.Type,Resolver);
    }
    private void Serialize_Unary_Type(ref MessagePackWriter writer,Expression value,MessagePackSerializerOptions Resolver){
        var Unary=(UnaryExpression)value;
        this.Serialize(ref writer,Unary.Operand,Resolver);
        Serialize_Type(ref writer,Unary.Type,Resolver);
    }
    private void Serialize_Unary_MethodInfo(ref JsonWriter writer,Expression value,IJsonFormatterResolver Resolver){
        var Unary=(UnaryExpression)value;
        this.Serialize(ref writer,Unary.Operand,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,Unary.Method,Resolver);
    }
    private void Serialize_Unary_MethodInfo(ref MessagePackWriter writer,Expression value,MessagePackSerializerOptions Resolver){
        var Unary=(UnaryExpression)value;
        this.Serialize(ref writer,Unary.Operand,Resolver);
        this.Serialize(ref writer,Unary.Method,Resolver);
    }
    private void Serialize_Unary_Type_MethodInfo(ref JsonWriter writer,Expression value,IJsonFormatterResolver Resolver){
        var Unary=(UnaryExpression)value;
        this.Serialize(ref writer,Unary.Operand,Resolver);
        writer.WriteValueSeparator();
        Serialize_Type(ref writer,Unary.Type,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,Unary.Method,Resolver);
    }
    private void Serialize_Unary_Type_MethodInfo(ref MessagePackWriter writer,Expression value,MessagePackSerializerOptions Resolver){
        var Unary=(UnaryExpression)value;
        this.Serialize(ref writer,Unary.Operand,Resolver);
        Serialize_Type(ref writer,Unary.Type,Resolver);
        this.Serialize(ref writer,Unary.Method,Resolver);
    }
    public void Serialize(ref JsonWriter writer,UnaryExpression? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        writer.WriteString(value.NodeType.ToString());
        writer.WriteValueSeparator();
        switch(value.NodeType){
            case ExpressionType.ArrayLength        : this.Serialize_Unary(ref writer,value,Resolver);break;
            case ExpressionType.Convert            : this.Serialize_Unary_Type_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.ConvertChecked     : this.Serialize_Unary_Type_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Decrement          : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Increment          : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.IsFalse            : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.IsTrue             : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Negate             : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.NegateChecked      : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Not                : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.OnesComplement     : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.PostDecrementAssign: this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.PostIncrementAssign: this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.PreDecrementAssign : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.PreIncrementAssign : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Quote              : this.Serialize_Unary(ref writer,value,Resolver);break;
            case ExpressionType.Throw              : this.Serialize_Unary_Type(ref writer,value,Resolver);break;
            case ExpressionType.TypeAs             : this.Serialize_Unary_Type(ref writer,value,Resolver);break;
            case ExpressionType.UnaryPlus          : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Unbox              : this.Serialize_Unary_Type(ref writer,value,Resolver);break;
            default:
                throw new NotSupportedException(value.NodeType.ToString());
        }
        writer.WriteEndArray();
    }
    //private readonly object[] Objects2=new object[2];
    private Expression Deserialize_Unary(ref JsonReader reader,IJsonFormatterResolver Resolver){
        var Operand= this.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Operand;
    }
    private Expression Deserialize_Unary(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var Operand= this.Deserialize(ref reader,Resolver);
        return Operand;
    }
    private (Expression Operand,Type Type)Deserialize_Unary_Type(ref JsonReader reader,IJsonFormatterResolver Resolver){
        var Operand= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Type=Deserialize_Type(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return(Operand,Type);
    }
    private (Expression Operand,Type Type)Deserialize_Unary_Type(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var Operand= this.Deserialize(ref reader,Resolver);
        var Type=Deserialize_Type(ref reader,Resolver);
        return(Operand,Type);
    }
    private (Expression Operand,MethodInfo Method)Deserialize_Unary_MethodInfo(ref JsonReader reader,IJsonFormatterResolver Resolver){
        var Operand= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Method=this.MethodInfo.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return(Operand,Method);
    }
    private (Expression Operand,MethodInfo Method)Deserialize_Unary_MethodInfo(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var Operand= this.Deserialize(ref reader,Resolver);
        var Method=this.MSMethodInfo.Deserialize(ref reader,Resolver);
        return(Operand,Method);
    }
    private (Expression Operand,Type Type,MethodInfo Method)Deserialize_Unary_Type_MethodInfo(ref JsonReader reader,IJsonFormatterResolver Resolver){
        var Operand= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Type=Deserialize_Type(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Method=Deserialize_T<MethodInfo>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return(Operand,Type,Method);
    }
    private (Expression Operand,Type Type,MethodInfo Method)Deserialize_Unary_Type_MethodInfo(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var Operand= this.Deserialize(ref reader,Resolver);
        var Type=Deserialize_Type(ref reader,Resolver);
        var Method=Deserialize_T<MethodInfo>(ref reader,Resolver);
        return(Operand,Type,Method);
    }
    UnaryExpression IJsonFormatter<UnaryExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var NodeTypeName=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var NodeType=Enum.Parse<ExpressionType>(NodeTypeName);
        switch(NodeType){
            case ExpressionType.ArrayLength        :{
                var Operand= this.Deserialize_Unary(ref reader,Resolver);
                return Expression.ArrayLength(Operand);
            }
            case ExpressionType.Convert            :{
                var(Operand,Type,Method)=this.Deserialize_Unary_Type_MethodInfo(ref reader,Resolver);
                return Expression.Convert(Operand,Type,Method);
            }
            case ExpressionType.ConvertChecked     :{
                var(Operand,Type,Method)=this.Deserialize_Unary_Type_MethodInfo(ref reader,Resolver);
                return Expression.ConvertChecked(Operand,Type,Method);
            }
            case ExpressionType.Decrement          :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.Decrement(Operand,Method);
            }
            case ExpressionType.Increment          :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.Increment(Operand,Method);
            }
            case ExpressionType.IsFalse            :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.IsFalse(Operand,Method);
            }
            case ExpressionType.IsTrue             :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.IsTrue(Operand,Method);
            }
            case ExpressionType.Negate             :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.Negate(Operand,Method);
            }
            case ExpressionType.NegateChecked      :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.NegateChecked(Operand,Method);
            }
            case ExpressionType.Not                :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.Not(Operand,Method);
            }
            case ExpressionType.OnesComplement     :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.OnesComplement(Operand,Method);
            }
            case ExpressionType.PostDecrementAssign:{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.PostDecrementAssign(Operand,Method);
            }
            case ExpressionType.PostIncrementAssign:{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.PostIncrementAssign(Operand,Method);
            }
            case ExpressionType.PreDecrementAssign :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.PreDecrementAssign(Operand,Method);
            }
            case ExpressionType.PreIncrementAssign :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.PreIncrementAssign(Operand,Method);
        }
            case ExpressionType.Quote:{
                var result=Expression.Quote(this.Deserialize_Unary(ref reader,Resolver));
                reader.ReadIsEndArrayWithVerify();
                return result;
            }
            case ExpressionType.Throw              :{
                var (Operand,Type)=this.Deserialize_Unary_Type(ref reader,Resolver);
                return Expression.Throw(Operand,Type);
            }
            case ExpressionType.TypeAs             :{
                var (Operand,Type)=this.Deserialize_Unary_Type(ref reader,Resolver);
                return Expression.TypeAs(Operand,Type);
            }
            case ExpressionType.UnaryPlus:{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.UnaryPlus(Operand,Method);
            }
            case ExpressionType.Unbox              :{
                var (Operand,Type)=this.Deserialize_Unary_Type(ref reader,Resolver);
                return Expression.Unbox(Operand,Type);
            }
        }
        throw new NotSupportedException(NodeTypeName);
    }
    public void Serialize(ref MessagePackWriter writer,UnaryExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        writer.Write((byte)value.NodeType);
        switch(value.NodeType){
            case ExpressionType.ArrayLength        : this.Serialize_Unary(ref writer,value,Resolver);break;
            case ExpressionType.Convert            : this.Serialize_Unary_Type_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.ConvertChecked     : this.Serialize_Unary_Type_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Decrement          : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Increment          : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.IsFalse            : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.IsTrue             : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Negate             : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.NegateChecked      : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Not                : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.OnesComplement     : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.PostDecrementAssign: this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.PostIncrementAssign: this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.PreDecrementAssign : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.PreIncrementAssign : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Quote              : this.Serialize_Unary(ref writer,value,Resolver);break;
            case ExpressionType.Throw              : this.Serialize_Unary_Type(ref writer,value,Resolver);break;
            case ExpressionType.TypeAs             : this.Serialize_Unary_Type(ref writer,value,Resolver);break;
            case ExpressionType.UnaryPlus          : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Unbox              : this.Serialize_Unary_Type(ref writer,value,Resolver);break;
            default:
                throw new NotSupportedException(value.NodeType.ToString());
        }
    }
    UnaryExpression IMessagePackFormatter<UnaryExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var NodeType=(ExpressionType)reader.ReadByte();
        switch(NodeType){
            case ExpressionType.ArrayLength        :{
                var Operand= this.Deserialize_Unary(ref reader,Resolver);
                return Expression.ArrayLength(Operand);
            }
            case ExpressionType.Convert            :{
                var(Operand,Type,Method)=this.Deserialize_Unary_Type_MethodInfo(ref reader,Resolver);
                return Expression.Convert(Operand,Type,Method);
            }
            case ExpressionType.ConvertChecked     :{
                var(Operand,Type,Method)=this.Deserialize_Unary_Type_MethodInfo(ref reader,Resolver);
                return Expression.ConvertChecked(Operand,Type,Method);
            }
            case ExpressionType.Decrement          :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.Decrement(Operand,Method);
            }
            case ExpressionType.Increment          :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.Increment(Operand,Method);
            }
            case ExpressionType.IsFalse            :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.IsFalse(Operand,Method);
            }
            case ExpressionType.IsTrue             :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.IsTrue(Operand,Method);
            }
            case ExpressionType.Negate             :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.Negate(Operand,Method);
            }
            case ExpressionType.NegateChecked      :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.NegateChecked(Operand,Method);
            }
            case ExpressionType.Not                :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.Not(Operand,Method);
            }
            case ExpressionType.OnesComplement     :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.OnesComplement(Operand,Method);
            }
            case ExpressionType.PostDecrementAssign:{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.PostDecrementAssign(Operand,Method);
            }
            case ExpressionType.PostIncrementAssign:{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.PostIncrementAssign(Operand,Method);
            }
            case ExpressionType.PreDecrementAssign :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.PreDecrementAssign(Operand,Method);
            }
            case ExpressionType.PreIncrementAssign :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.PreIncrementAssign(Operand,Method);
            }
            case ExpressionType.Quote:{
                var Quote=Expression.Quote(this.Deserialize_Unary(ref reader,Resolver));
                return Quote;
            }
            case ExpressionType.Throw              :{
                var (Operand,Type)=this.Deserialize_Unary_Type(ref reader,Resolver);
                return Expression.Throw(Operand,Type);
            }
            case ExpressionType.TypeAs             :{
                var (Operand,Type)=this.Deserialize_Unary_Type(ref reader,Resolver);
                return Expression.TypeAs(Operand,Type);
            }
            case ExpressionType.UnaryPlus:{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expression.UnaryPlus(Operand,Method);
            }
            case ExpressionType.Unbox              :{
                var (Operand,Type)=this.Deserialize_Unary_Type(ref reader,Resolver);
                return Expression.Unbox(Operand,Type);
            }
        }
        throw new NotSupportedException(NodeType.ToString());
    }
}
