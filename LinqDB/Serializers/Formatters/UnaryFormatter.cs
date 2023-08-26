using System;
using Expressions=System.Linq.Expressions;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.UnaryExpression>{
    private void Serialize_Unary(ref JsonWriter writer,Expressions.Expression value,IJsonFormatterResolver Resolver){
        var Unary=(Expressions.UnaryExpression)value;
        this.Serialize(ref writer,Unary.Operand,Resolver);
    }
    private void Serialize_Unary_Type(ref JsonWriter writer,Expressions.Expression value,IJsonFormatterResolver Resolver){
        var Unary=(Expressions.UnaryExpression)value;
        this.Serialize(ref writer,Unary.Operand,Resolver);
        writer.WriteValueSeparator();
        Serialize_Type(ref writer,Unary.Type,Resolver);
    }
    private void Serialize_Unary_MethodInfo(ref JsonWriter writer,Expressions.Expression value,IJsonFormatterResolver Resolver){
        var Unary=(Expressions.UnaryExpression)value;
        this.Serialize(ref writer,Unary.Operand,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,Unary.Method,Resolver);
    }
    private void Serialize_Unary_Type_MethodInfo(ref JsonWriter writer,Expressions.Expression value,IJsonFormatterResolver Resolver){
        var Unary=(Expressions.UnaryExpression)value;
        this.Serialize(ref writer,Unary.Operand,Resolver);
        writer.WriteValueSeparator();
        Serialize_Type(ref writer,Unary.Type,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,Unary.Method,Resolver);
    }
    public void Serialize(ref JsonWriter writer,Expressions.UnaryExpression? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        writer.WriteString(value.NodeType.ToString());
        writer.WriteValueSeparator();
        switch(value.NodeType){
            case Expressions.ExpressionType.ArrayLength        : this.Serialize_Unary(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Convert            : this.Serialize_Unary_Type_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.ConvertChecked     : this.Serialize_Unary_Type_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Decrement          : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Increment          : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.IsFalse            : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.IsTrue             : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Negate             : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.NegateChecked      : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Not                : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.OnesComplement     : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.PostDecrementAssign: this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.PostIncrementAssign: this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.PreDecrementAssign : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.PreIncrementAssign : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Quote              : this.Serialize_Unary(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Throw              : this.Serialize_Unary_Type(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.TypeAs             : this.Serialize_Unary_Type(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.UnaryPlus          : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Unbox              : this.Serialize_Unary_Type(ref writer,value,Resolver);break;
            default:
                throw new NotSupportedException(value.NodeType.ToString());
        }
        writer.WriteEndArray();
    }
    private Expressions.Expression Deserialize_Unary(ref JsonReader reader,IJsonFormatterResolver Resolver){
        var Operand= this.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Operand;
    }
    private (Expressions.Expression Operand,Type Type)Deserialize_Unary_Type(ref JsonReader reader,IJsonFormatterResolver Resolver){
        var Operand= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Type=Deserialize_Type(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return(Operand,Type);
    }
    private (Expressions.Expression Operand,MethodInfo Method)Deserialize_Unary_MethodInfo(ref JsonReader reader,IJsonFormatterResolver Resolver){
        var Operand= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Method=this.MethodInfo.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return(Operand,Method);
    }
    private (Expressions.Expression Operand,Type Type,MethodInfo Method)Deserialize_Unary_Type_MethodInfo(ref JsonReader reader,IJsonFormatterResolver Resolver){
        var Operand= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Type=Deserialize_Type(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Method=Deserialize_T<MethodInfo>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return(Operand,Type,Method);
    }
    Expressions.UnaryExpression IJsonFormatter<Expressions.UnaryExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var NodeTypeName=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var NodeType=Enum.Parse<Expressions.ExpressionType>(NodeTypeName);
        switch(NodeType){
            case Expressions.ExpressionType.ArrayLength        :{
                var Operand= this.Deserialize_Unary(ref reader,Resolver);
                return Expressions.Expression.ArrayLength(Operand);
            }
            case Expressions.ExpressionType.Convert            :{
                var(Operand,Type,Method)=this.Deserialize_Unary_Type_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Convert(Operand,Type,Method);
            }
            case Expressions.ExpressionType.ConvertChecked     :{
                var(Operand,Type,Method)=this.Deserialize_Unary_Type_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.ConvertChecked(Operand,Type,Method);
            }
            case Expressions.ExpressionType.Decrement          :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Decrement(Operand,Method);
            }
            case Expressions.ExpressionType.Increment          :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Increment(Operand,Method);
            }
            case Expressions.ExpressionType.IsFalse            :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.IsFalse(Operand,Method);
            }
            case Expressions.ExpressionType.IsTrue             :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.IsTrue(Operand,Method);
            }
            case Expressions.ExpressionType.Negate             :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Negate(Operand,Method);
            }
            case Expressions.ExpressionType.NegateChecked      :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.NegateChecked(Operand,Method);
            }
            case Expressions.ExpressionType.Not                :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Not(Operand,Method);
            }
            case Expressions.ExpressionType.OnesComplement     :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.OnesComplement(Operand,Method);
            }
            case Expressions.ExpressionType.PostDecrementAssign:{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.PostDecrementAssign(Operand,Method);
            }
            case Expressions.ExpressionType.PostIncrementAssign:{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.PostIncrementAssign(Operand,Method);
            }
            case Expressions.ExpressionType.PreDecrementAssign :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.PreDecrementAssign(Operand,Method);
            }
            case Expressions.ExpressionType.PreIncrementAssign :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.PreIncrementAssign(Operand,Method);
        }
            case Expressions.ExpressionType.Quote:{
                var result=Expressions.Expression.Quote(this.Deserialize_Unary(ref reader,Resolver));
                reader.ReadIsEndArrayWithVerify();
                return result;
            }
            case Expressions.ExpressionType.Throw              :{
                var (Operand,Type)=this.Deserialize_Unary_Type(ref reader,Resolver);
                return Expressions.Expression.Throw(Operand,Type);
            }
            case Expressions.ExpressionType.TypeAs             :{
                var (Operand,Type)=this.Deserialize_Unary_Type(ref reader,Resolver);
                return Expressions.Expression.TypeAs(Operand,Type);
            }
            case Expressions.ExpressionType.UnaryPlus:{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.UnaryPlus(Operand,Method);
            }
            case Expressions.ExpressionType.Unbox              :{
                var (Operand,Type)=this.Deserialize_Unary_Type(ref reader,Resolver);
                return Expressions.Expression.Unbox(Operand,Type);
            }
        }
        throw new NotSupportedException(NodeTypeName);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.UnaryExpression>{
    private void Serialize_Unary(ref MessagePackWriter writer,Expressions.Expression value,MessagePackSerializerOptions Resolver){
        var Unary=(Expressions.UnaryExpression)value;
        this.Serialize(ref writer,Unary.Operand,Resolver);
    }
    private void Serialize_Unary_Type(ref MessagePackWriter writer,Expressions.Expression value,MessagePackSerializerOptions Resolver){
        var Unary=(Expressions.UnaryExpression)value;
        this.Serialize(ref writer,Unary.Operand,Resolver);
        Serialize_Type(ref writer,Unary.Type,Resolver);
    }
    private void Serialize_Unary_MethodInfo(ref MessagePackWriter writer,Expressions.Expression value,MessagePackSerializerOptions Resolver){
        var Unary=(Expressions.UnaryExpression)value;
        this.Serialize(ref writer,Unary.Operand,Resolver);
        this.Serialize(ref writer,Unary.Method,Resolver);
    }
    private void Serialize_Unary_Type_MethodInfo(ref MessagePackWriter writer,Expressions.Expression value,MessagePackSerializerOptions Resolver){
        var Unary=(Expressions.UnaryExpression)value;
        this.Serialize(ref writer,Unary.Operand,Resolver);
        Serialize_Type(ref writer,Unary.Type,Resolver);
        this.Serialize(ref writer,Unary.Method,Resolver);
    }
    //private readonly object[] Objects2=new object[2];
    private Expressions.Expression Deserialize_Unary(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var Operand= this.Deserialize(ref reader,Resolver);
        return Operand;
    }
    private (Expressions.Expression Operand,Type Type)Deserialize_Unary_Type(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var Operand= this.Deserialize(ref reader,Resolver);
        var Type=Deserialize_Type(ref reader,Resolver);
        return(Operand,Type);
    }
    private (Expressions.Expression Operand,MethodInfo Method)Deserialize_Unary_MethodInfo(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var Operand= this.Deserialize(ref reader,Resolver);
        var Method=this.MSMethodInfo.Deserialize(ref reader,Resolver);
        return(Operand,Method);
    }
    private (Expressions.Expression Operand,Type Type,MethodInfo Method)Deserialize_Unary_Type_MethodInfo(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var Operand= this.Deserialize(ref reader,Resolver);
        var Type=Deserialize_Type(ref reader,Resolver);
        var Method=Deserialize_T<MethodInfo>(ref reader,Resolver);
        return(Operand,Type,Method);
    }
    public void Serialize(ref MessagePackWriter writer,Expressions.UnaryExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        writer.Write((byte)value.NodeType);
        switch(value.NodeType){
            case Expressions.ExpressionType.ArrayLength        : this.Serialize_Unary(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Convert            : this.Serialize_Unary_Type_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.ConvertChecked     : this.Serialize_Unary_Type_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Decrement          : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Increment          : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.IsFalse            : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.IsTrue             : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Negate             : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.NegateChecked      : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Not                : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.OnesComplement     : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.PostDecrementAssign: this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.PostIncrementAssign: this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.PreDecrementAssign : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.PreIncrementAssign : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Quote              : this.Serialize_Unary(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Throw              : this.Serialize_Unary_Type(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.TypeAs             : this.Serialize_Unary_Type(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.UnaryPlus          : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Unbox              : this.Serialize_Unary_Type(ref writer,value,Resolver);break;
            default:
                throw new NotSupportedException(value.NodeType.ToString());
        }
    }
    Expressions.UnaryExpression IMessagePackFormatter<Expressions.UnaryExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var NodeType=(Expressions.ExpressionType)reader.ReadByte();
        switch(NodeType){
            case Expressions.ExpressionType.ArrayLength        :{
                var Operand= this.Deserialize_Unary(ref reader,Resolver);
                return Expressions.Expression.ArrayLength(Operand);
            }
            case Expressions.ExpressionType.Convert            :{
                var(Operand,Type,Method)=this.Deserialize_Unary_Type_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Convert(Operand,Type,Method);
            }
            case Expressions.ExpressionType.ConvertChecked     :{
                var(Operand,Type,Method)=this.Deserialize_Unary_Type_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.ConvertChecked(Operand,Type,Method);
            }
            case Expressions.ExpressionType.Decrement          :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Decrement(Operand,Method);
            }
            case Expressions.ExpressionType.Increment          :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Increment(Operand,Method);
            }
            case Expressions.ExpressionType.IsFalse            :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.IsFalse(Operand,Method);
            }
            case Expressions.ExpressionType.IsTrue             :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.IsTrue(Operand,Method);
            }
            case Expressions.ExpressionType.Negate             :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Negate(Operand,Method);
            }
            case Expressions.ExpressionType.NegateChecked      :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.NegateChecked(Operand,Method);
            }
            case Expressions.ExpressionType.Not                :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.Not(Operand,Method);
            }
            case Expressions.ExpressionType.OnesComplement     :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.OnesComplement(Operand,Method);
            }
            case Expressions.ExpressionType.PostDecrementAssign:{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.PostDecrementAssign(Operand,Method);
            }
            case Expressions.ExpressionType.PostIncrementAssign:{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.PostIncrementAssign(Operand,Method);
            }
            case Expressions.ExpressionType.PreDecrementAssign :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.PreDecrementAssign(Operand,Method);
            }
            case Expressions.ExpressionType.PreIncrementAssign :{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.PreIncrementAssign(Operand,Method);
            }
            case Expressions.ExpressionType.Quote:{
                var Quote=Expressions.Expression.Quote(this.Deserialize_Unary(ref reader,Resolver));
                return Quote;
            }
            case Expressions.ExpressionType.Throw              :{
                var (Operand,Type)=this.Deserialize_Unary_Type(ref reader,Resolver);
                return Expressions.Expression.Throw(Operand,Type);
            }
            case Expressions.ExpressionType.TypeAs             :{
                var (Operand,Type)=this.Deserialize_Unary_Type(ref reader,Resolver);
                return Expressions.Expression.TypeAs(Operand,Type);
            }
            case Expressions.ExpressionType.UnaryPlus:{
                var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.UnaryPlus(Operand,Method);
            }
            case Expressions.ExpressionType.Unbox              :{
                var (Operand,Type)=this.Deserialize_Unary_Type(ref reader,Resolver);
                return Expressions.Expression.Unbox(Operand,Type);
            }
        }
        throw new NotSupportedException(NodeType.ToString());
    }
}
