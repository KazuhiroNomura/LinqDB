using System;
using System.Collections.Generic;
using System.Diagnostics;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.Expression;
public class Expression:IMessagePackFormatter<T> {
    public static readonly Expression Instance=new();
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        //if(writer.TryWriteNil(value)) return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        //writer.WriteArrayHeader(2);
        //writer.WriteNodeType(value!.NodeType);
        switch(value.NodeType){
            case Expressions.ExpressionType.Assign               :
            case Expressions.ExpressionType.Coalesce             :
            case Expressions.ExpressionType.ArrayIndex           :Binary.InternalSerialize(ref writer,(Expressions.BinaryExpression)value,Resolver); break;
            case Expressions.ExpressionType.Add                  :
            case Expressions.ExpressionType.AddAssign            :
            case Expressions.ExpressionType.AddAssignChecked     :
            case Expressions.ExpressionType.AddChecked           :
            case Expressions.ExpressionType.And                  :
            case Expressions.ExpressionType.AndAssign            :
            case Expressions.ExpressionType.AndAlso              :
            case Expressions.ExpressionType.Divide               :
            case Expressions.ExpressionType.DivideAssign         :
            case Expressions.ExpressionType.ExclusiveOr          :
            case Expressions.ExpressionType.ExclusiveOrAssign    :
            case Expressions.ExpressionType.LeftShift            :
            case Expressions.ExpressionType.LeftShiftAssign      :
            case Expressions.ExpressionType.Modulo               :
            case Expressions.ExpressionType.ModuloAssign         :
            case Expressions.ExpressionType.Multiply             :
            case Expressions.ExpressionType.MultiplyAssign       :
            case Expressions.ExpressionType.MultiplyAssignChecked:
            case Expressions.ExpressionType.MultiplyChecked      :
            case Expressions.ExpressionType.Or                   :
            case Expressions.ExpressionType.OrAssign             :
            case Expressions.ExpressionType.OrElse               :
            case Expressions.ExpressionType.Power                :
            case Expressions.ExpressionType.PowerAssign          :
            case Expressions.ExpressionType.RightShift           :
            case Expressions.ExpressionType.RightShiftAssign     :
            case Expressions.ExpressionType.Subtract             :
            case Expressions.ExpressionType.SubtractAssign       :
            case Expressions.ExpressionType.SubtractAssignChecked:
            case Expressions.ExpressionType.SubtractChecked      :Binary.InternalSerializeMethod(ref writer,(Expressions.BinaryExpression)value,Resolver); break;
            case Expressions.ExpressionType.Equal                :
            case Expressions.ExpressionType.GreaterThan          :
            case Expressions.ExpressionType.GreaterThanOrEqual   :
            case Expressions.ExpressionType.LessThan             :
            case Expressions.ExpressionType.LessThanOrEqual      :
            case Expressions.ExpressionType.NotEqual             :Binary.InternalSerializeBooleanMethod(ref writer,(Expressions.BinaryExpression)value,Resolver); break;
            case Expressions.ExpressionType.ArrayLength        : 
            case Expressions.ExpressionType.Quote              :Unary.InternalSerializeOperand(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.Throw              : 
            case Expressions.ExpressionType.TypeAs             : 
            case Expressions.ExpressionType.Unbox              :Unary.InternalSerializeOperandType(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.Convert            : 
            case Expressions.ExpressionType.ConvertChecked     :Unary.InternalSerializeOperandTypeMethod(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
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
            case Expressions.ExpressionType.UnaryPlus          :Unary.InternalSerializeOperandMethod(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.TypeEqual          :
            case Expressions.ExpressionType.TypeIs             :TypeBinary.InternalSerializeExpression(ref writer,(Expressions.TypeBinaryExpression)value,Resolver); break;
            case Expressions.ExpressionType.Conditional        :Conditional.InternalSerialize(ref writer,(Expressions.ConditionalExpression)value,Resolver);break;
            case Expressions.ExpressionType.Constant           :Constant.InternalSerialize(ref writer,(Expressions.ConstantExpression)value,Resolver);break;
            case Expressions.ExpressionType.Parameter          :Parameter.InternalSerialize(ref writer,(Expressions.ParameterExpression)value,Resolver);break;
            case Expressions.ExpressionType.Lambda             :Lambda.InternalSerialize(ref writer,(Expressions.LambdaExpression)value,Resolver); break;
            case Expressions.ExpressionType.Call               :MethodCall.InternalSerialize(ref writer,(Expressions.MethodCallExpression)value,Resolver);break;
            case Expressions.ExpressionType.Invoke             :Invocation.InternalSerialize(ref writer,(Expressions.InvocationExpression)value,Resolver);break;
            case Expressions.ExpressionType.New                :New.InternalSerialize(ref writer,(Expressions.NewExpression)value,Resolver);break;
            case Expressions.ExpressionType.NewArrayInit       :
            case Expressions.ExpressionType.NewArrayBounds     :NewArray.InternalSerialize(ref writer,(Expressions.NewArrayExpression)value,Resolver);break;//this.InternalSerialize(ref writer,(Expressions.Expressions.(NewArrayExpression)value,Resolver).Expressions);break;
            case Expressions.ExpressionType.ListInit           :ListInit.InternalSerialize(ref writer,(Expressions.ListInitExpression)value,Resolver);break;
            case Expressions.ExpressionType.MemberAccess       :MemberAccess.InternalSerialize(ref writer,(Expressions.MemberExpression)value,Resolver);break;
            case Expressions.ExpressionType.MemberInit         :MemberInit.InternalSerialize(ref writer,(Expressions.MemberInitExpression)value,Resolver);break;
            case Expressions.ExpressionType.Block              :Block.InternalSerialize(ref writer,(Expressions.BlockExpression)value,Resolver);break;
            //case Expressions.ExpressionType.DebugInfo          :
            //case Expressions.ExpressionType.Dynamic            :
            case Expressions.ExpressionType.Default            :Default.InternalSerialize(ref writer,(Expressions.DefaultExpression)value,Resolver);break;
            //case Expressions.ExpressionType.Extension          :
            case Expressions.ExpressionType.Goto               :Goto.InternalSerialize(ref writer,(Expressions.GotoExpression)value,Resolver);break;
            case Expressions.ExpressionType.Index              :Index.InternalSerialize(ref writer,(Expressions.IndexExpression)value,Resolver);break;
            case Expressions.ExpressionType.Label              :Label.InternalSerialize(ref writer,(Expressions.LabelExpression)value,Resolver);break;
            //case Expressions.ExpressionType.RuntimeVariables   :
            case Expressions.ExpressionType.Loop               :Loop.InternalSerialize(ref writer,(Expressions.LoopExpression)value,Resolver);break;
            case Expressions.ExpressionType.Switch             :Switch.InternalSerialize(ref writer,(Expressions.SwitchExpression)value,Resolver);break;
            case Expressions.ExpressionType.Try                :Try.InternalSerialize(ref writer,(Expressions.TryExpression)value,Resolver);break;
            default:throw new ArgumentOutOfRangeException(value.NodeType.ToString());
        }
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        //if(reader.TryReadNil()) return null!;
        T value;
        var count=reader.ReadArrayHeader();
        var NodeType=reader.ReadNodeType();
        switch(NodeType){
            case Expressions.ExpressionType.ArrayIndex: {
                var (array, index)=Binary.InternalDeserialize(ref reader,Resolver);
                value=Expressions.Expression.ArrayIndex(array,index);break;
            }
            case Expressions.ExpressionType.Assign: {
                var (Left, Right)=Binary.InternalDeserialize(ref reader,Resolver);
                value=Expressions.Expression.Assign(Left,Right);break;
            }
            case Expressions.ExpressionType.Coalesce: {
                var (Left, Right)=Binary.InternalDeserialize(ref reader,Resolver);
                value=Expressions.Expression.Coalesce(Left,Right);break;
            }
            case Expressions.ExpressionType.Add: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Add(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.AddAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.AddAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.AddAssignChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.AddAssignChecked(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.AddChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.AddChecked(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.And: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.And(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.AndAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.AndAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.AndAlso: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.AndAlso(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.Divide: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Divide(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.DivideAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.DivideAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.ExclusiveOr: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.ExclusiveOr(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.ExclusiveOrAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.ExclusiveOrAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.LeftShift: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.LeftShift(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.LeftShiftAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.LeftShiftAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.Modulo: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Modulo(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.ModuloAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.ModuloAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.Multiply: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Multiply(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.MultiplyAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.MultiplyAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.MultiplyAssignChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.MultiplyAssignChecked(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.MultiplyChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.MultiplyChecked(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.Or: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Or(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.OrAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.OrAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.OrElse: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.OrElse(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.Power: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Power(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.PowerAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.PowerAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.RightShift: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.RightShift(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.RightShiftAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.RightShiftAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.Subtract: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.Subtract(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.SubtractAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.SubtractAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.SubtractAssignChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.SubtractAssignChecked(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.SubtractChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=Expressions.Expression.SubtractChecked(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.Equal: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.Equal(Left,Right,IsLiftedToNull,Method);break;
            }
            case Expressions.ExpressionType.GreaterThan: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.GreaterThan(Left,Right,IsLiftedToNull,Method);break;
            }
            case Expressions.ExpressionType.GreaterThanOrEqual: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.GreaterThanOrEqual(Left,Right,IsLiftedToNull,Method);break;
            }
            case Expressions.ExpressionType.LessThan: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.LessThan(Left,Right,IsLiftedToNull,Method);break;
            }
            case Expressions.ExpressionType.LessThanOrEqual: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.LessThanOrEqual(Left,Right,IsLiftedToNull,Method);break;
            }
            case Expressions.ExpressionType.NotEqual: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.NotEqual(Left,Right,IsLiftedToNull,Method);break;
            }

            case Expressions.ExpressionType.ArrayLength: {
                var Operand = Unary.InternalDeserializeOperand(ref reader,Resolver);
                value=Expressions.Expression.ArrayLength(Operand);break;
            }
            case Expressions.ExpressionType.Quote: {
                var result = Expressions.Expression.Quote(Unary.InternalDeserializeOperand(ref reader,Resolver));
                value=result;break;
            }
            case Expressions.ExpressionType.Convert: {
                var (Operand, Type, Method)=Unary.InternalDeserializeOperandTypeMethod(ref reader,Resolver);
                value=Expressions.Expression.Convert(Operand,Type,Method);break;
            }
            case Expressions.ExpressionType.ConvertChecked: {
                var (Operand, Type, Method)=Unary.InternalDeserializeOperandTypeMethod(ref reader,Resolver);
                value=Expressions.Expression.ConvertChecked(Operand,Type,Method);break;
            }
            case Expressions.ExpressionType.Decrement: {
                var (Operand, Method)=Unary.InternalDeserializeOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.Decrement(Operand,Method);break;
            }
            case Expressions.ExpressionType.Increment: {
                var (Operand, Method)=Unary.InternalDeserializeOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.Increment(Operand,Method);break;
            }
            case Expressions.ExpressionType.IsFalse: {
                var (Operand, Method)=Unary.InternalDeserializeOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.IsFalse(Operand,Method);break;
            }
            case Expressions.ExpressionType.IsTrue: {
                var (Operand, Method)=Unary.InternalDeserializeOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.IsTrue(Operand,Method);break;
            }
            case Expressions.ExpressionType.Negate: {
                var (Operand, Method)=Unary.InternalDeserializeOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.Negate(Operand,Method);break;
            }
            case Expressions.ExpressionType.NegateChecked: {
                var (Operand, Method)=Unary.InternalDeserializeOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.NegateChecked(Operand,Method);break;
            }
            case Expressions.ExpressionType.Not: {
                var (Operand, Method)=Unary.InternalDeserializeOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.Not(Operand,Method);break;
            }
            case Expressions.ExpressionType.OnesComplement: {
                var (Operand, Method)=Unary.InternalDeserializeOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.OnesComplement(Operand,Method);break;
            }
            case Expressions.ExpressionType.PostDecrementAssign: {
                var (Operand, Method)=Unary.InternalDeserializeOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.PostDecrementAssign(Operand,Method);break;
            }
            case Expressions.ExpressionType.PostIncrementAssign: {
                var (Operand, Method)=Unary.InternalDeserializeOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.PostIncrementAssign(Operand,Method);break;
            }
            case Expressions.ExpressionType.PreDecrementAssign: {
                var (Operand, Method)=Unary.InternalDeserializeOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.PreDecrementAssign(Operand,Method);break;
            }
            case Expressions.ExpressionType.PreIncrementAssign: {
                var (Operand, Method)=Unary.InternalDeserializeOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.PreIncrementAssign(Operand,Method);break;
            }
            case Expressions.ExpressionType.Throw: {
                var (Operand, Type)=Unary.InternalDeserializeOperandType(ref reader,Resolver);
                value=Expressions.Expression.Throw(Operand,Type);break;
            }
            case Expressions.ExpressionType.TypeAs: {
                var (Operand, Type)=Unary.InternalDeserializeOperandType(ref reader,Resolver);
                value=Expressions.Expression.TypeAs(Operand,Type);break;
            }
            case Expressions.ExpressionType.UnaryPlus: {
                var (Operand, Method)=Unary.InternalDeserializeOperandMethod(ref reader,Resolver);
                value=Expressions.Expression.UnaryPlus(Operand,Method);break;
            }
            case Expressions.ExpressionType.Unbox: {
                var (Operand, Type)=Unary.InternalDeserializeOperandType(ref reader,Resolver);
                value=Expressions.Expression.Unbox(Operand,Type);break;
            }

            case Expressions.ExpressionType.TypeEqual       :value=TypeBinary.InternalDeserializeTypeEqual(ref reader,Resolver);break;
            case Expressions.ExpressionType.TypeIs          :value=TypeBinary.InternalDeserializeTypeIs(ref reader,Resolver);break;

            case Expressions.ExpressionType.Conditional     :value=Conditional.InternalDeserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Constant        :value=Constant.InternalDeserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Parameter       :value=Parameter.InternalDeserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Lambda          :value=Lambda.InternalDeserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Call            :value=MethodCall.InternalDeserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Invoke          :value=Invocation.InternalDeserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.New             :value=New.InternalDeserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.NewArrayInit    :value=NewArray.InternalDeserializeNewArrayInit(ref reader,Resolver);break;
            case Expressions.ExpressionType.NewArrayBounds  :value=NewArray.InternalDeserializeNewArrayBounds(ref reader,Resolver);break;
            case Expressions.ExpressionType.ListInit        :value=ListInit.InternalDeserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.MemberAccess    :value=MemberAccess.InternalDeserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.MemberInit      :value=MemberInit.InternalDeserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Block           :value=Block.InternalDeserialize(ref reader,Resolver);break;
            //case Expressions.ExpressionType.DebugInfo       :
            //case Expressions.ExpressionType.Dynamic         :
            case Expressions.ExpressionType.Default         :value=Default.InternalDeserialize(ref reader,Resolver);break;
            //case Expressions.ExpressionType.Extension       :
            case Expressions.ExpressionType.Goto            :value=Goto.InternalDeserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Index           :value=Index.InternalDeserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Label           :value=Label.InternalDeserialize(ref reader,Resolver);break;
            //case Expressions.ExpressionType.RuntimeVariables:break;
            case Expressions.ExpressionType.Loop            :value=Loop.InternalDeserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Switch          :value=Switch.InternalDeserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Try             :value=Try.InternalDeserialize(ref reader,Resolver);break;
            default:throw new NotSupportedException(NodeType.ToString());
        }
        return value;
    }
}
