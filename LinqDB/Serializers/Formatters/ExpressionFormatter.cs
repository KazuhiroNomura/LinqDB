using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
//public partial class ExpressionFormatter:IJsonFormatter<Expression>,IMessagePackFormatter<Expression>{
//    //public static ExpressionFormatter Instance{get;private set;}
//    //public static readonly ExpressionFormatter Instance=new();
//    internal readonly List<ParameterExpression> ListParameter=new();
//    private readonly Dictionary<LabelTarget,int> Dictionary_LabelTarget_int=new();
//    private readonly Dictionary<int,LabelTarget> Dictionary_int_LabelTarget=new();
//    public void Clear(){
//        this.ListParameter.Clear();
//        this.Dictionary_LabelTarget_int.Clear();
//        this.Dictionary_int_LabelTarget.Clear();
//    }
//    private IJsonFormatter<Expression> JExpression=>this;
//    private IMessagePackFormatter<Expression> MSExpression=>this;
//}
public class ExpressionFormatter{
    internal readonly List<ParameterExpression> ListParameter=new();
    protected readonly Dictionary<LabelTarget,int> Dictionary_LabelTarget_int=new();
    protected readonly Dictionary<int,LabelTarget> Dictionary_int_LabelTarget=new();
    public void Clear(){
        this.ListParameter.Clear();
        this.Dictionary_LabelTarget_int.Clear();
        this.Dictionary_int_LabelTarget.Clear();
    }
}
partial class ExpressionJsonFormatter:ExpressionFormatter,IJsonFormatter<Expression>{
    public void Serialize(ref JsonWriter writer,Expression? value,IJsonFormatterResolver Resolver) {
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        writer.WriteString(value.NodeType.ToString());
        writer.WriteValueSeparator();
        switch(value.NodeType){
            case ExpressionType.Assign or ExpressionType.Coalesce or ExpressionType.ArrayIndex:{
                var Binary=(BinaryExpression)value;
                this.Serialize(ref writer,Binary.Left,Resolver);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,Binary.Right,Resolver);
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
                var Binary=(BinaryExpression)value;
                this.Serialize(ref writer,Binary.Left,Resolver);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,Binary.Right,Resolver);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,Binary.Method,Resolver);
                break;
            }
            case ExpressionType.Equal:
            case ExpressionType.GreaterThan:
            case ExpressionType.GreaterThanOrEqual:
            case ExpressionType.LessThan:
            case ExpressionType.LessThanOrEqual:
            case ExpressionType.NotEqual:{
                var Binary=(BinaryExpression)value;
                this.Serialize(ref writer,Binary.Left,Resolver);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,Binary.Right,Resolver);
                writer.WriteValueSeparator();
                writer.WriteBoolean(Binary.IsLiftedToNull);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,Binary.Method!,Resolver);
                break;
            }

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

            case ExpressionType.TypeEqual or ExpressionType.TypeIs:this.Serialize(ref writer,(TypeBinaryExpression)value,Resolver); break;

            case ExpressionType.Conditional: this.Serialize(ref writer,(ConditionalExpression)value,Resolver);break;
            case ExpressionType.Constant: this.Serialize(ref writer,(ConstantExpression)value,Resolver);break;
            case ExpressionType.Parameter: this.Serialize(ref writer,(ParameterExpression)value,Resolver);break;
            case ExpressionType.Lambda: this.Serialize(ref writer,(LambdaExpression)value,Resolver); break;
            case ExpressionType.Call: this.Serialize(ref writer,(MethodCallExpression)value,Resolver);break;
            case ExpressionType.Invoke: this.Serialize(ref writer,(InvocationExpression)value,Resolver);break;
            case ExpressionType.New: this.Serialize(ref writer,(NewExpression)value,Resolver);break;
            case ExpressionType.NewArrayBounds or ExpressionType.NewArrayInit:this.Serialize(ref writer,(NewArrayExpression)value,Resolver); break;
            case ExpressionType.ListInit: this.Serialize(ref writer,(ListInitExpression)value,Resolver);break;
            case ExpressionType.MemberAccess: this.Serialize(ref writer,(MemberExpression)value,Resolver);break;
            case ExpressionType.MemberInit: this.Serialize(ref writer,(MemberInitExpression)value,Resolver);break;
            case ExpressionType.Block: this.Serialize(ref writer,(BlockExpression)value,Resolver);break;
            case ExpressionType.DebugInfo:
            case ExpressionType.Dynamic:
            case ExpressionType.Default: this.Serialize(ref writer,(DefaultExpression)value,Resolver);break;
            case ExpressionType.Extension:
            case ExpressionType.Goto: this.Serialize(ref writer,(GotoExpression)value,Resolver);break;
            case ExpressionType.Index:this.Serialize(ref writer,(IndexExpression)value,Resolver);break;
            case ExpressionType.Label: this.Serialize(ref writer,(LabelExpression)value,Resolver);break;
            case ExpressionType.RuntimeVariables:
                throw new ArgumentOutOfRangeException(value.NodeType.ToString());
            case ExpressionType.Loop: this.Serialize(ref writer,(LoopExpression)value,Resolver);break;
            case ExpressionType.Switch: this.Serialize(ref writer,(SwitchExpression)value,Resolver);break;
            case ExpressionType.Try: this.Serialize(ref writer,(TryExpression)value,Resolver);break;
            default:
                throw new ArgumentOutOfRangeException(value.NodeType.ToString());
        };
        writer.WriteEndArray();
    }
    public Expression Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var TypeName=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var NodeType=Enum.Parse<ExpressionType>(TypeName);
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

            case ExpressionType.TypeEqual              :{
                var TypeEqual=this.TypeBinary.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return TypeEqual;
            }
            case ExpressionType.TypeIs              :{
                var TypeIs=this.TypeBinary.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return TypeIs;
            }

            case ExpressionType.Conditional        :{
                var Conditional=this.Conditional.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Conditional;
            }
            case ExpressionType.Constant:{
                var Constant=this.Constant.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Constant;
            }
            case ExpressionType.Parameter:{
                var Parameter=this.Parameter.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Parameter;
            }
            case ExpressionType.Lambda:{
                var Lambda=this.Lambda.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Lambda;
            }
            case ExpressionType.Call               :{
                var MethodCall=this.MethodCall.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return MethodCall;
            }
            case ExpressionType.Invoke             :{
                var Invocation=this.Invocation.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Invocation;
            }
            case ExpressionType.New:{
                var New=this.New.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return New;
            }
            case ExpressionType.NewArrayInit:{
                var NewArray=this.NewArray.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return NewArray;
            }
            case ExpressionType.NewArrayBounds:{
                var NewArray=this.NewArray.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return NewArray;
            }
            case ExpressionType.ListInit:{
                var ListInit=this.ListInit.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return ListInit;
            }
            case ExpressionType.MemberAccess:{
                var Member=this.Member.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Member;
            }
            case ExpressionType.MemberInit:{
                var MemberInit=this.MemberInit.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return MemberInit;
            }
            case ExpressionType.Block:{
                var Block=this.Block.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Block;
            }
            case ExpressionType.DebugInfo:
            case ExpressionType.Dynamic:
            case ExpressionType.Default:
                var Default=this.Default.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Default;
            case ExpressionType.Extension:
                break;
            case ExpressionType.Goto:
                var Goto=this.Goto.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Goto;
            case ExpressionType.Index:
                var Index=this.Index.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Index;
            case ExpressionType.Label:
                var Label=this.LabelExpression.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Label;
            case ExpressionType.RuntimeVariables:
                break;
            case ExpressionType.Loop:{
                var Loop=this.Loop.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Loop;
            }
            case ExpressionType.Switch:
                var Switch=this.Switch.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Switch;
            case ExpressionType.Try:
                var Try=this.Try.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Try;
        }
        throw new NotSupportedException(TypeName);
    }
}
partial class ExpressionMessagePackFormatter:ExpressionFormatter,IMessagePackFormatter<Expression>{
    public void Serialize(ref MessagePackWriter writer,Expression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        writer.Write((byte)value.NodeType);
        switch(value.NodeType){
            case ExpressionType.Assign or ExpressionType.Coalesce or ExpressionType.ArrayIndex:{
                var Binary=(BinaryExpression)value;
                this.Serialize(ref writer,Binary.Left,Resolver);
                this.Serialize(ref writer,Binary.Right,Resolver);
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
                var Binary=(BinaryExpression)value;
                this.Serialize(ref writer,Binary.Left,Resolver);
                this.Serialize(ref writer,Binary.Right,Resolver);
                this.Serialize(ref writer,Binary.Method,Resolver);
                break;
            }
            case ExpressionType.Equal:
            case ExpressionType.GreaterThan:
            case ExpressionType.GreaterThanOrEqual:
            case ExpressionType.LessThan:
            case ExpressionType.LessThanOrEqual:
            case ExpressionType.NotEqual:{
                var Binary=(BinaryExpression)value;
                this.Serialize(ref writer,Binary.Left,Resolver);
                this.Serialize(ref writer,Binary.Right,Resolver);
                writer.Write(Binary.IsLiftedToNull);
                this.Serialize(ref writer,Binary.Method!,Resolver);
                break;
            }

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

            case ExpressionType.TypeEqual or ExpressionType.TypeIs:this.Serialize(ref writer,(TypeBinaryExpression)value,Resolver); break;


            case ExpressionType.Conditional: this.Serialize(ref writer,(ConditionalExpression)value,Resolver);break;
            case ExpressionType.Constant: this.Serialize(ref writer,(ConstantExpression)value,Resolver);break;
            case ExpressionType.Parameter: this.Serialize(ref writer,(ParameterExpression)value,Resolver);break;
            case ExpressionType.Lambda: this.Serialize(ref writer,(LambdaExpression)value,Resolver); break;
            case ExpressionType.Call: this.Serialize(ref writer,(MethodCallExpression)value,Resolver);break;
            case ExpressionType.Invoke: this.Serialize(ref writer,(InvocationExpression)value,Resolver);break;
            case ExpressionType.New: this.Serialize(ref writer,(NewExpression)value,Resolver);break;
            case ExpressionType.NewArrayInit or ExpressionType.NewArrayBounds:this.Serialize(ref writer,(NewArrayExpression)value,Resolver);break;//Serialize_T(ref writer,((NewArrayExpression)value).Expressions,Resolver);break;
            case ExpressionType.ListInit: this.Serialize(ref writer,(ListInitExpression)value,Resolver);break;
            case ExpressionType.MemberAccess: this.Serialize(ref writer,(MemberExpression)value,Resolver);break;
            case ExpressionType.MemberInit: this.Serialize(ref writer,(MemberInitExpression)value,Resolver);break;
            case ExpressionType.Block: this.Serialize(ref writer,(BlockExpression)value,Resolver);break;
            case ExpressionType.DebugInfo:
            case ExpressionType.Dynamic:
            case ExpressionType.Default: this.Serialize(ref writer,(DefaultExpression)value,Resolver);break;
            case ExpressionType.Extension:
            case ExpressionType.Goto: this.Serialize(ref writer,(GotoExpression)value,Resolver);break;
            case ExpressionType.Index:this.Serialize(ref writer,(IndexExpression)value,Resolver);break;
            case ExpressionType.Label: this.Serialize(ref writer,(LabelExpression)value,Resolver);break;
            case ExpressionType.RuntimeVariables:
                throw new ArgumentOutOfRangeException(value.NodeType.ToString());
            case ExpressionType.Loop: this.Serialize(ref writer,(LoopExpression)value,Resolver);break;
            case ExpressionType.Switch: this.Serialize(ref writer,(SwitchExpression)value,Resolver);break;
            case ExpressionType.Try: this.Serialize(ref writer,(TryExpression)value,Resolver);break;
            default:
                throw new ArgumentOutOfRangeException(value.NodeType.ToString());
        };
    }
    public Expression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil())return null!;
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

            case ExpressionType.TypeEqual or ExpressionType.TypeIs:return this.TypeBinary.Deserialize(ref reader,Resolver);

            case ExpressionType.Conditional        :return this.Conditional.Deserialize(ref reader,Resolver);
            case ExpressionType.Constant:return this.Constant.Deserialize(ref reader,Resolver);
            case ExpressionType.Parameter:return this.MSParameter.Deserialize(ref reader,Resolver);
            case ExpressionType.Lambda:return this.MSLambda.Deserialize(ref reader,Resolver);
            case ExpressionType.Call               :return this.MSMethodCall.Deserialize(ref reader,Resolver);
            case ExpressionType.Invoke             :return this.MSInvocation.Deserialize(ref reader,Resolver);
            case ExpressionType.New:return this.MSNew.Deserialize(ref reader,Resolver);
            case ExpressionType.NewArrayInit:return this.MSNewArray.Deserialize(ref reader,Resolver);
            case ExpressionType.NewArrayBounds:return this.MSNewArray.Deserialize(ref reader,Resolver);
            case ExpressionType.ListInit:return this.MSListInit.Deserialize(ref reader,Resolver);
            case ExpressionType.MemberAccess:return this.MSMember.Deserialize(ref reader,Resolver);
            case ExpressionType.MemberInit:return this.MSMemberInit.Deserialize(ref reader,Resolver);
            case ExpressionType.Block:return this.Block.Deserialize(ref reader,Resolver);
            case ExpressionType.DebugInfo:
            case ExpressionType.Dynamic:
            case ExpressionType.Default:return this.Default.Deserialize(ref reader,Resolver);
            case ExpressionType.Extension:
                break;
            case ExpressionType.Goto:return this.Goto.Deserialize(ref reader,Resolver);
            case ExpressionType.Index:return this.Index.Deserialize(ref reader,Resolver);
            case ExpressionType.Label:return this.LabelExpression.Deserialize(ref reader,Resolver);
            case ExpressionType.RuntimeVariables:
                break;
            case ExpressionType.Loop:return this.MSLoop.Deserialize(ref reader,Resolver);
            case ExpressionType.Switch:return this.MSSwitch.Deserialize(ref reader,Resolver);
            case ExpressionType.Try:return this.MSTry.Deserialize(ref reader,Resolver);
        }
        throw new NotSupportedException(NodeType.ToString());
        //return this.Deserialize(ref reader,options);
    }
}
