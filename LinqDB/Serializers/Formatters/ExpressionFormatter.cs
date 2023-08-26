using System;
using System.Collections.Generic;
using Expressions=System.Linq.Expressions;
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
    internal readonly List<Expressions.ParameterExpression> ListParameter=new();
    protected readonly Dictionary<Expressions.LabelTarget,int> Dictionary_LabelTarget_int=new();
    protected readonly Dictionary<int,Expressions.LabelTarget> Dictionary_int_LabelTarget=new();
    public void Clear(){
        this.ListParameter.Clear();
        this.Dictionary_LabelTarget_int.Clear();
        this.Dictionary_int_LabelTarget.Clear();
    }
}
public partial class ExpressionJsonFormatter:ExpressionFormatter,IJsonFormatter<Expressions.Expression>{
    //public IJsonFormatter<Expressions.Expression> Expression=>this;
    public void Serialize(ref JsonWriter writer,Expressions.Expression? value,IJsonFormatterResolver Resolver) {
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        writer.WriteString(value.NodeType.ToString());
        writer.WriteValueSeparator();
        switch(value.NodeType){
            case Expressions.ExpressionType.Assign or Expressions.ExpressionType.Coalesce or Expressions.ExpressionType.ArrayIndex:{
                var Binary=(Expressions.BinaryExpression)value;
                this.Serialize(ref writer,Binary.Left,Resolver);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,Binary.Right,Resolver);
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
                var Binary=(Expressions.BinaryExpression)value;
                this.Serialize(ref writer,Binary.Left,Resolver);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,Binary.Right,Resolver);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,Binary.Method,Resolver);
                break;
            }
            case Expressions.ExpressionType.Equal:
            case Expressions.ExpressionType.GreaterThan:
            case Expressions.ExpressionType.GreaterThanOrEqual:
            case Expressions.ExpressionType.LessThan:
            case Expressions.ExpressionType.LessThanOrEqual:
            case Expressions.ExpressionType.NotEqual:{
                var Binary=(Expressions.BinaryExpression)value;
                this.Serialize(ref writer,Binary.Left,Resolver);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,Binary.Right,Resolver);
                writer.WriteValueSeparator();
                writer.WriteBoolean(Binary.IsLiftedToNull);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,Binary.Method!,Resolver);
                break;
            }

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

            case Expressions.ExpressionType.TypeEqual or Expressions.ExpressionType.TypeIs:this.Serialize(ref writer,(Expressions.TypeBinaryExpression)value,Resolver); break;

            case Expressions.ExpressionType.Conditional: this.Serialize(ref writer,(Expressions.ConditionalExpression)value,Resolver);break;
            case Expressions.ExpressionType.Constant: this.Serialize(ref writer,(Expressions.ConstantExpression)value,Resolver);break;
            case Expressions.ExpressionType.Parameter: this.Serialize(ref writer,(Expressions.ParameterExpression)value,Resolver);break;
            case Expressions.ExpressionType.Lambda: this.Serialize(ref writer,(Expressions.LambdaExpression)value,Resolver); break;
            case Expressions.ExpressionType.Call: this.Serialize(ref writer,(Expressions.MethodCallExpression)value,Resolver);break;
            case Expressions.ExpressionType.Invoke: this.Serialize(ref writer,(Expressions.InvocationExpression)value,Resolver);break;
            case Expressions.ExpressionType.New: this.Serialize(ref writer,(Expressions.NewExpression)value,Resolver);break;
            case Expressions.ExpressionType.NewArrayBounds or Expressions.ExpressionType.NewArrayInit:this.Serialize(ref writer,(Expressions.NewArrayExpression)value,Resolver); break;
            case Expressions.ExpressionType.ListInit: this.Serialize(ref writer,(Expressions.ListInitExpression)value,Resolver);break;
            case Expressions.ExpressionType.MemberAccess: this.Serialize(ref writer,(Expressions.MemberExpression)value,Resolver);break;
            case Expressions.ExpressionType.MemberInit: this.Serialize(ref writer,(Expressions.MemberInitExpression)value,Resolver);break;
            case Expressions.ExpressionType.Block: this.Serialize(ref writer,(Expressions.BlockExpression)value,Resolver);break;
            case Expressions.ExpressionType.DebugInfo:
            case Expressions.ExpressionType.Dynamic:
            case Expressions.ExpressionType.Default: this.Serialize(ref writer,(Expressions.DefaultExpression)value,Resolver);break;
            case Expressions.ExpressionType.Extension:
            case Expressions.ExpressionType.Goto: this.Serialize(ref writer,(Expressions.GotoExpression)value,Resolver);break;
            case Expressions.ExpressionType.Index:this.Serialize(ref writer,(Expressions.IndexExpression)value,Resolver);break;
            case Expressions.ExpressionType.Label: this.Serialize(ref writer,(Expressions.LabelExpression)value,Resolver);break;
            case Expressions.ExpressionType.RuntimeVariables:
                throw new ArgumentOutOfRangeException(value.NodeType.ToString());
            case Expressions.ExpressionType.Loop: this.Serialize(ref writer,(Expressions.LoopExpression)value,Resolver);break;
            case Expressions.ExpressionType.Switch: this.Serialize(ref writer,(Expressions.SwitchExpression)value,Resolver);break;
            case Expressions.ExpressionType.Try: this.Serialize(ref writer,(Expressions.TryExpression)value,Resolver);break;
            default:
                throw new ArgumentOutOfRangeException(value.NodeType.ToString());
        }
        writer.WriteEndArray();
    }
    public Expressions.Expression Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var TypeName=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var NodeType=Enum.Parse<Expressions.ExpressionType>(TypeName);
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

            case Expressions.ExpressionType.TypeEqual              :{
                var TypeEqual=this.TypeBinary.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return TypeEqual;
            }
            case Expressions.ExpressionType.TypeIs              :{
                var TypeIs=this.TypeBinary.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return TypeIs;
            }

            case Expressions.ExpressionType.Conditional        :{
                var Conditional=this.Conditional.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Conditional;
            }
            case Expressions.ExpressionType.Constant:{
                var Constant=this.Constant.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Constant;
            }
            case Expressions.ExpressionType.Parameter:{
                var Parameter=this.Parameter.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Parameter;
            }
            case Expressions.ExpressionType.Lambda:{
                var Lambda=this.Lambda.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Lambda;
            }
            case Expressions.ExpressionType.Call               :{
                var MethodCall=this.MethodCall.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return MethodCall;
            }
            case Expressions.ExpressionType.Invoke             :{
                var Invocation=this.Invocation.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Invocation;
            }
            case Expressions.ExpressionType.New:{
                var New=this.New.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return New;
            }
            case Expressions.ExpressionType.NewArrayInit:{
                var NewArray=this.NewArray.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return NewArray;
            }
            case Expressions.ExpressionType.NewArrayBounds:{
                var NewArray=this.NewArray.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return NewArray;
            }
            case Expressions.ExpressionType.ListInit:{
                var ListInit=this.ListInit.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return ListInit;
            }
            case Expressions.ExpressionType.MemberAccess:{
                var Member=this.Member.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Member;
            }
            case Expressions.ExpressionType.MemberInit:{
                var MemberInit=this.MemberInit.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return MemberInit;
            }
            case Expressions.ExpressionType.Block:{
                var Block=this.Block.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Block;
            }
            case Expressions.ExpressionType.DebugInfo:
            case Expressions.ExpressionType.Dynamic:
            case Expressions.ExpressionType.Default:
                var Default=this.Default.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Default;
            case Expressions.ExpressionType.Extension:
                break;
            case Expressions.ExpressionType.Goto:
                var Goto=this.Goto.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Goto;
            case Expressions.ExpressionType.Index:
                var Index=this.Index.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Index;
            case Expressions.ExpressionType.Label:
                var Label=this.LabelExpression.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Label;
            case Expressions.ExpressionType.RuntimeVariables:
                break;
            case Expressions.ExpressionType.Loop:{
                var Loop=this.Loop.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Loop;
            }
            case Expressions.ExpressionType.Switch:
                var Switch=this.Switch.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Switch;
            case Expressions.ExpressionType.Try:
                var Try=this.Try.Deserialize(ref reader,Resolver);
                reader.ReadIsEndArrayWithVerify();
                return Try;
        }
        throw new NotSupportedException(TypeName);
    }
}
public partial class ExpressionMessagePackFormatter:ExpressionFormatter,IMessagePackFormatter<Expressions.Expression>{
    public IMessagePackFormatter<Expressions.Expression> _Expression=>this;
    public void Serialize(ref MessagePackWriter writer,Expressions.Expression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        writer.Write((byte)value.NodeType);
        switch(value.NodeType){
            case Expressions.ExpressionType.Assign or Expressions.ExpressionType.Coalesce or Expressions.ExpressionType.ArrayIndex:{
                var Binary=(Expressions.BinaryExpression)value;
                this.Serialize(ref writer,Binary.Left,Resolver);
                this.Serialize(ref writer,Binary.Right,Resolver);
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
                var Binary=(Expressions.BinaryExpression)value;
                this.Serialize(ref writer,Binary.Left,Resolver);
                this.Serialize(ref writer,Binary.Right,Resolver);
                this.Serialize(ref writer,Binary.Method,Resolver);
                break;
            }
            case Expressions.ExpressionType.Equal:
            case Expressions.ExpressionType.GreaterThan:
            case Expressions.ExpressionType.GreaterThanOrEqual:
            case Expressions.ExpressionType.LessThan:
            case Expressions.ExpressionType.LessThanOrEqual:
            case Expressions.ExpressionType.NotEqual:{
                var Binary=(Expressions.BinaryExpression)value;
                this.Serialize(ref writer,Binary.Left,Resolver);
                this.Serialize(ref writer,Binary.Right,Resolver);
                writer.Write(Binary.IsLiftedToNull);
                this.Serialize(ref writer,Binary.Method!,Resolver);
                break;
            }

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

            case Expressions.ExpressionType.TypeEqual or Expressions.ExpressionType.TypeIs:this.Serialize(ref writer,(Expressions.TypeBinaryExpression)value,Resolver); break;


            case Expressions.ExpressionType.Conditional: this.Serialize(ref writer,(Expressions.ConditionalExpression)value,Resolver);break;
            case Expressions.ExpressionType.Constant: this.Serialize(ref writer,(Expressions.ConstantExpression)value,Resolver);break;
            case Expressions.ExpressionType.Parameter: this.Serialize(ref writer,(Expressions.ParameterExpression)value,Resolver);break;
            case Expressions.ExpressionType.Lambda: this.Serialize(ref writer,(Expressions.LambdaExpression)value,Resolver); break;
            case Expressions.ExpressionType.Call: this.Serialize(ref writer,(Expressions.MethodCallExpression)value,Resolver);break;
            case Expressions.ExpressionType.Invoke: this.Serialize(ref writer,(Expressions.InvocationExpression)value,Resolver);break;
            case Expressions.ExpressionType.New: this.Serialize(ref writer,(Expressions.NewExpression)value,Resolver);break;
            case Expressions.ExpressionType.NewArrayInit or Expressions.ExpressionType.NewArrayBounds:this.Serialize(ref writer,(Expressions.NewArrayExpression)value,Resolver);break;//Serialize_T(ref writer,((NewArrayExpression)value).Expressions,Resolver);break;
            case Expressions.ExpressionType.ListInit: this.Serialize(ref writer,(Expressions.ListInitExpression)value,Resolver);break;
            case Expressions.ExpressionType.MemberAccess: this.Serialize(ref writer,(Expressions.MemberExpression)value,Resolver);break;
            case Expressions.ExpressionType.MemberInit: this.Serialize(ref writer,(Expressions.MemberInitExpression)value,Resolver);break;
            case Expressions.ExpressionType.Block: this.Serialize(ref writer,(Expressions.BlockExpression)value,Resolver);break;
            case Expressions.ExpressionType.DebugInfo:
            case Expressions.ExpressionType.Dynamic:
            case Expressions.ExpressionType.Default: this.Serialize(ref writer,(Expressions.DefaultExpression)value,Resolver);break;
            case Expressions.ExpressionType.Extension:
            case Expressions.ExpressionType.Goto: this.Serialize(ref writer,(Expressions.GotoExpression)value,Resolver);break;
            case Expressions.ExpressionType.Index:this.Serialize(ref writer,(Expressions.IndexExpression)value,Resolver);break;
            case Expressions.ExpressionType.Label: this.Serialize(ref writer,(Expressions.LabelExpression)value,Resolver);break;
            case Expressions.ExpressionType.RuntimeVariables:
                throw new ArgumentOutOfRangeException(value.NodeType.ToString());
            case Expressions.ExpressionType.Loop: this.Serialize(ref writer,(Expressions.LoopExpression)value,Resolver);break;
            case Expressions.ExpressionType.Switch: this.Serialize(ref writer,(Expressions.SwitchExpression)value,Resolver);break;
            case Expressions.ExpressionType.Try: this.Serialize(ref writer,(Expressions.TryExpression)value,Resolver);break;
            default:
                throw new ArgumentOutOfRangeException(value.NodeType.ToString());
        }
    }
    public Expressions.Expression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil())return null!;
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

            case Expressions.ExpressionType.TypeEqual or Expressions.ExpressionType.TypeIs:return this.TypeBinary.Deserialize(ref reader,Resolver);

            case Expressions.ExpressionType.Conditional        :return this.Conditional.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.Constant:return this.Constant.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.Parameter:return this.MSParameter.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.Lambda:return this.MSLambda.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.Call               :return this.MSMethodCall.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.Invoke             :return this.MSInvocation.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.New:return this.MSNew.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.NewArrayInit:return this.MSNewArray.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.NewArrayBounds:return this.MSNewArray.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.ListInit:return this.MSListInit.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.MemberAccess:return this.MSMember.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.MemberInit:return this.MSMemberInit.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.Block:return this.Block.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.DebugInfo:
            case Expressions.ExpressionType.Dynamic:
            case Expressions.ExpressionType.Default:return this.Default.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.Extension:
                break;
            case Expressions.ExpressionType.Goto:return this.Goto.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.Index:return this.Index.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.Label:return this.LabelExpression.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.RuntimeVariables:
                break;
            case Expressions.ExpressionType.Loop:return this.MSLoop.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.Switch:return this.MSSwitch.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.Try:return this.MSTry.Deserialize(ref reader,Resolver);
        }
        throw new NotSupportedException(NodeType.ToString());
        //return this.Deserialize(ref reader,options);
    }
}
