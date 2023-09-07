using System;
using System.Collections.Generic;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.Expression;
using C=Utf8JsonCustomSerializer;
public class Expression:IJsonFormatter<T> {
    public static readonly Expression Instance=new();
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver) {
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        writer.WriteString(value.NodeType.ToString());
        writer.WriteValueSeparator();
        switch(value.NodeType){
            case Expressions.ExpressionType.Assign:
            case Expressions.ExpressionType.Coalesce:
            case Expressions.ExpressionType.ArrayIndex:{
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
                Method.Instance.Serialize(ref writer,Binary.Method,Resolver);
                break;
            }
            case Expressions.ExpressionType.Equal:
            case Expressions.ExpressionType.GreaterThan:
            case Expressions.ExpressionType.GreaterThanOrEqual:
            case Expressions.ExpressionType.LessThan:
            case Expressions.ExpressionType.LessThanOrEqual:
            case Expressions.ExpressionType.NotEqual:{
                var Binary=(Expressions.BinaryExpression)value;
                Instance.Serialize(ref writer,Binary.Left,Resolver);
                writer.WriteValueSeparator();
                Instance.Serialize(ref writer,Binary.Right,Resolver);
                writer.WriteValueSeparator();
                writer.WriteBoolean(Binary.IsLiftedToNull);
                writer.WriteValueSeparator();
                Method.Instance.Serialize(ref writer,Binary.Method!,Resolver);
                break;
            }

            case Expressions.ExpressionType.ArrayLength        : Unary.Serialize_Unary(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.Quote              : Unary.Serialize_Unary(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.Throw              : Unary.Serialize_Unary_Type(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.TypeAs             : Unary.Serialize_Unary_Type(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.Unbox              : Unary.Serialize_Unary_Type(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.Convert            : Unary.Serialize_Unary_Type_MethodInfo(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.ConvertChecked     : Unary.Serialize_Unary_Type_MethodInfo(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.Decrement          : Unary.Serialize_Unary_MethodInfo(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.Increment          : Unary.Serialize_Unary_MethodInfo(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.IsFalse            : Unary.Serialize_Unary_MethodInfo(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.IsTrue             : Unary.Serialize_Unary_MethodInfo(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.Negate             : Unary.Serialize_Unary_MethodInfo(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.NegateChecked      : Unary.Serialize_Unary_MethodInfo(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.Not                : Unary.Serialize_Unary_MethodInfo(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.OnesComplement     : Unary.Serialize_Unary_MethodInfo(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.PostDecrementAssign: Unary.Serialize_Unary_MethodInfo(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.PostIncrementAssign: Unary.Serialize_Unary_MethodInfo(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.PreDecrementAssign : Unary.Serialize_Unary_MethodInfo(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.PreIncrementAssign : Unary.Serialize_Unary_MethodInfo(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.UnaryPlus          : Unary.Serialize_Unary_MethodInfo(ref writer,(Expressions.UnaryExpression)value,Resolver);break;

            case Expressions.ExpressionType.TypeEqual          :
            case Expressions.ExpressionType.TypeIs             : TypeBinary.Instance.Serialize(ref writer,(Expressions.TypeBinaryExpression)value,Resolver); break;

            case Expressions.ExpressionType.Conditional        : Conditional.Instance.Serialize(ref writer,(Expressions.ConditionalExpression)value,Resolver);break;
            case Expressions.ExpressionType.Constant           : Constant.Instance.Serialize(ref writer,(Expressions.ConstantExpression)value,Resolver);break;
            case Expressions.ExpressionType.Parameter          : Parameter.Instance.Serialize(ref writer,(Expressions.ParameterExpression)value,Resolver);break;
            case Expressions.ExpressionType.Lambda             : Lambda.Instance.Serialize(ref writer,(Expressions.LambdaExpression)value,Resolver); break;
            case Expressions.ExpressionType.Call               : MethodCall.Instance.Serialize(ref writer,(Expressions.MethodCallExpression)value,Resolver);break;
            case Expressions.ExpressionType.Invoke             : Invocation.Instance.Serialize(ref writer,(Expressions.InvocationExpression)value,Resolver);break;
            case Expressions.ExpressionType.New                : New.Instance.Serialize(ref writer,(Expressions.NewExpression)value,Resolver);break;
            case Expressions.ExpressionType.NewArrayBounds     :
            case Expressions.ExpressionType.NewArrayInit       : NewArray.Instance.Serialize(ref writer,(Expressions.NewArrayExpression)value,Resolver); break;
            case Expressions.ExpressionType.ListInit           : ListInit.Instance.Serialize(ref writer,(Expressions.ListInitExpression)value,Resolver);break;
            case Expressions.ExpressionType.MemberAccess       : MemberAccess.Instance.Serialize(ref writer,(Expressions.MemberExpression)value,Resolver);break;
            case Expressions.ExpressionType.MemberInit         : MemberInit.Instance.Serialize(ref writer,(Expressions.MemberInitExpression)value,Resolver);break;
            case Expressions.ExpressionType.Block              : Block.Instance.Serialize(ref writer,(Expressions.BlockExpression)value,Resolver);break;
            case Expressions.ExpressionType.DebugInfo          :
            case Expressions.ExpressionType.Dynamic            :
            case Expressions.ExpressionType.Default            : Default.Instance.Serialize(ref writer,(Expressions.DefaultExpression)value,Resolver);break;
            case Expressions.ExpressionType.Extension          :
            case Expressions.ExpressionType.Goto               : Goto.Instance.Serialize(ref writer,(Expressions.GotoExpression)value,Resolver);break;
            case Expressions.ExpressionType.Index              : Index.Instance.Serialize(ref writer,(Expressions.IndexExpression)value,Resolver);break;
            case Expressions.ExpressionType.Label              : Label.Instance.Serialize(ref writer,(Expressions.LabelExpression)value,Resolver);break;
            case Expressions.ExpressionType.RuntimeVariables   :
                throw new ArgumentOutOfRangeException(value.NodeType.ToString());
            case Expressions.ExpressionType.Loop               : Loop.Instance.Serialize(ref writer,(Expressions.LoopExpression)value,Resolver);break;
            case Expressions.ExpressionType.Switch             : Switch.Instance.Serialize(ref writer,(Expressions.SwitchExpression)value,Resolver);break;
            case Expressions.ExpressionType.Try                : Try.Instance.Serialize(ref writer,(Expressions.TryExpression)value,Resolver);break;            default:
                throw new ArgumentOutOfRangeException(value.NodeType.ToString());
        }
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var TypeName=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        T result;
        var NodeType=Enum.Parse<Expressions.ExpressionType>(TypeName);
        switch(NodeType){
            case Expressions.ExpressionType.ArrayIndex:
            case Expressions.ExpressionType.Assign:
            case Expressions.ExpressionType.Coalesce:{
                var (Left,Right)=Binary.Deserialize_Binary(ref reader,Resolver);
                result=Expressions.Expression.MakeBinary(NodeType,Left,Right);
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
                var (Left,Right,Method)=Binary.Deserialize_Binary_MethodInfo(ref reader,Resolver);
                //result=Expressions.Expression.SubtractChecked(Left,Right,Method);
                result=Expressions.Expression.MakeBinary(NodeType,Left,Right,false,Method);
                break;
            }
            case Expressions.ExpressionType.Equal:
            case Expressions.ExpressionType.GreaterThan:
            case Expressions.ExpressionType.GreaterThanOrEqual:
            case Expressions.ExpressionType.LessThan:
            case Expressions.ExpressionType.LessThanOrEqual:
            case Expressions.ExpressionType.NotEqual:{
                var (Left,Right,IsLiftedToNull,Method)=Binary.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                result=Expressions.Expression.MakeBinary(NodeType,Left,Right,IsLiftedToNull,Method);
                break;
            }

            case Expressions.ExpressionType.ArrayLength        :{
                var Operand= Unary.Deserialize_Unary(ref reader,Resolver);
                result=Expressions.Expression.ArrayLength(Operand);
                break;
            }
            case Expressions.ExpressionType.Convert            :
            case Expressions.ExpressionType.ConvertChecked     :{
                var(Operand,Type,Method)=Unary.Deserialize_Unary_Type_MethodInfo(ref reader,Resolver);
                result=Expressions.Expression.ConvertChecked(Operand,Type,Method);
                break;
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
            case Expressions.ExpressionType.PreIncrementAssign :{
                var (Operand,Method)=Unary.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                result=Expressions.Expression.PreIncrementAssign(Operand,Method);
                break;
            }
            case Expressions.ExpressionType.Quote:
            case Expressions.ExpressionType.Throw              :
            case Expressions.ExpressionType.TypeAs             :
            case Expressions.ExpressionType.UnaryPlus:
            case Expressions.ExpressionType.Unbox              :{
                var (Operand,Type)=Unary.Deserialize_Unary_Type(ref reader,Resolver);
                result=Expressions.Expression.MakeUnary(NodeType,Operand,Type);
                break;
            }

            case Expressions.ExpressionType.TypeEqual       :
            case Expressions.ExpressionType.TypeIs          :result=TypeBinary.Instance.Deserialize(ref reader,Resolver);break;

            case Expressions.ExpressionType.Conditional     :result=Conditional.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Constant        :result=Constant.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Parameter       :result=Parameter.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Lambda          :result=Lambda.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Call            :result=MethodCall.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Invoke          :result=Invocation.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.New             :result=New.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.NewArrayInit    :result=NewArray.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.NewArrayBounds  :result=NewArray.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.ListInit        :result=ListInit.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.MemberAccess    :result=MemberAccess.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.MemberInit      :result=MemberInit.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Block           :result=Block.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.DebugInfo       :
            case Expressions.ExpressionType.Dynamic         :
            case Expressions.ExpressionType.Default         :result=Default.Instance.Deserialize(ref reader,Resolver);break;
            //case Expressions.ExpressionType.Extension       :break;
            case Expressions.ExpressionType.Goto            :result=Goto.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Index           :result=Index.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Label           :result=Label.Instance.Deserialize(ref reader,Resolver);break;
            //case Expressions.ExpressionType.RuntimeVariables:break;
            case Expressions.ExpressionType.Loop            :result=Loop.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Switch          :result=Switch.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Try             :result=Try.Instance.Deserialize(ref reader,Resolver);break;
            default                                         :throw new NotSupportedException(TypeName);
        }
        reader.ReadIsEndArrayWithVerify();
        return result;
    }
}
