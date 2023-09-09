using System;
using System.Buffers;
using MemoryPack;
using Expressions = System.Linq.Expressions;
// ReSharper disable InconsistentNaming
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.Expression;

public class Expression:MemoryPackFormatter<T> {
    public static readonly Expression Instance=new();
    public void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    public T Deserialize(ref Reader reader) {
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(value is null){
            writer.WriteBoolean(false);
            return;
        }
        writer.WriteBoolean(true);
        writer.WriteNodeType(value.NodeType);
        switch(value.NodeType){
            case Expressions.ExpressionType.Assign               :
            case Expressions.ExpressionType.Coalesce             :
            case Expressions.ExpressionType.ArrayIndex           :Binary.InternalSerialize(ref writer,(Expressions.BinaryExpression)value); break;
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
            case Expressions.ExpressionType.SubtractChecked      :Binary.InternalSerializeMethod(ref writer,(Expressions.BinaryExpression)value); break;
            case Expressions.ExpressionType.Equal                :
            case Expressions.ExpressionType.GreaterThan          :
            case Expressions.ExpressionType.GreaterThanOrEqual   :
            case Expressions.ExpressionType.LessThan             :
            case Expressions.ExpressionType.LessThanOrEqual      :
            case Expressions.ExpressionType.NotEqual             :Binary.InternalSerializeBooleanMethod(ref writer,(Expressions.BinaryExpression)value); break;

            case Expressions.ExpressionType.ArrayLength        :
            case Expressions.ExpressionType.Quote              :Unary.InternalSerialize(ref writer,(Expressions.UnaryExpression)value);break;
            case Expressions.ExpressionType.Throw              :
            case Expressions.ExpressionType.TypeAs             :
            case Expressions.ExpressionType.Unbox              :Unary.InternalSerializeType(ref writer,(Expressions.UnaryExpression)value);break;
            case Expressions.ExpressionType.Convert            :
            case Expressions.ExpressionType.ConvertChecked     :Unary.InternalSerializeOperandTypeMethod(ref writer,(Expressions.UnaryExpression)value);break;
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
            case Expressions.ExpressionType.UnaryPlus          :Unary.InternalSerializeMethod(ref writer,(Expressions.UnaryExpression)value);break;

            case Expressions.ExpressionType.TypeEqual          :
            case Expressions.ExpressionType.TypeIs             :TypeBinary.InternalSerializeExpression(ref writer,(Expressions.TypeBinaryExpression)value); break;
            case Expressions.ExpressionType.Conditional        :Conditional.Instance.Serialize(ref writer,(Expressions.ConditionalExpression)value);break;
            case Expressions.ExpressionType.Constant           :Constant.Instance.Serialize(ref writer,(Expressions.ConstantExpression)value);break;
            case Expressions.ExpressionType.Parameter          :Parameter.Instance.Serialize(ref writer,(Expressions.ParameterExpression)value);break;
            case Expressions.ExpressionType.Lambda             :Lambda.Instance.Serialize(ref writer,(Expressions.LambdaExpression)value); break;
            case Expressions.ExpressionType.Call               :MethodCall.Instance.Serialize(ref writer,(Expressions.MethodCallExpression)value);break;
            case Expressions.ExpressionType.Invoke             :Invocation.Instance.Serialize(ref writer,(Expressions.InvocationExpression)value);break;
            case Expressions.ExpressionType.New                :New.Instance.Serialize(ref writer,(Expressions.NewExpression)value);break;
            case Expressions.ExpressionType.NewArrayInit       :
            case Expressions.ExpressionType.NewArrayBounds     :NewArray.Instance.Serialize(ref writer,(Expressions.NewArrayExpression)value);break;//this.Instance.Serialize(ref writer,(Expressions.Expressions.(NewArrayExpression)value).Expressions);break;
            case Expressions.ExpressionType.ListInit           :ListInit.Instance.Serialize(ref writer,(Expressions.ListInitExpression)value);break;
            case Expressions.ExpressionType.MemberAccess       :MemberAccess.Instance.Serialize(ref writer,(Expressions.MemberExpression)value);break;
            case Expressions.ExpressionType.MemberInit         :MemberInit.Instance.Serialize(ref writer,(Expressions.MemberInitExpression)value);break;
            case Expressions.ExpressionType.Block              :Block.Instance.Serialize(ref writer,(Expressions.BlockExpression)value);break;
            //case Expressions.ExpressionType.DebugInfo          :
            //case Expressions.ExpressionType.Dynamic            :
            case Expressions.ExpressionType.Default            :Default.Instance.Serialize(ref writer,(Expressions.DefaultExpression)value);break;
            //case Expressions.ExpressionType.Extension          :
            case Expressions.ExpressionType.Goto               :Goto.Instance.Serialize(ref writer,(Expressions.GotoExpression)value);break;
            case Expressions.ExpressionType.Index              :Index.Instance.Serialize(ref writer,(Expressions.IndexExpression)value);break;
            case Expressions.ExpressionType.Label              :Label.Instance.Serialize(ref writer,(Expressions.LabelExpression)value);break;
            //case Expressions.ExpressionType.RuntimeVariables   :
            case Expressions.ExpressionType.Loop               :Loop.Instance.Serialize(ref writer,(Expressions.LoopExpression)value);break;
            case Expressions.ExpressionType.Switch             :Switch.Instance.Serialize(ref writer,(Expressions.SwitchExpression)value);break;
            case Expressions.ExpressionType.Try                :Try.Instance.Serialize(ref writer,(Expressions.TryExpression)value);break;
            default:throw new ArgumentOutOfRangeException(value.NodeType.ToString());
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(!reader.ReadBoolean()) return;
        var NodeType=reader.ReadNodeType();
        switch(NodeType){
            case Expressions.ExpressionType.ArrayIndex: {
                var (array, index)=Binary.InternalDeserialize(ref reader);
                value=T.ArrayIndex(array,index); break;
            }
            case Expressions.ExpressionType.Assign: {
                var (Left, Right)=Binary.InternalDeserialize(ref reader);
                value=T.Assign(Left,Right); break;
            }
            case Expressions.ExpressionType.Coalesce: {
                var (Left, Right)=Binary.InternalDeserialize(ref reader);
                value=T.Coalesce(Left,Right); break;
            }
            case Expressions.ExpressionType.Add: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.Add(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.AddAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.AddAssign(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.AddAssignChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.AddAssignChecked(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.AddChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.AddChecked(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.And: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.And(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.AndAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.AndAssign(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.AndAlso: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.AndAlso(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.Divide: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.Divide(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.DivideAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.DivideAssign(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.ExclusiveOr: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.ExclusiveOr(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.ExclusiveOrAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.ExclusiveOrAssign(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.LeftShift: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.LeftShift(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.LeftShiftAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.LeftShiftAssign(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.Modulo: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.Modulo(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.ModuloAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.ModuloAssign(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.Multiply: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.Multiply(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.MultiplyAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.MultiplyAssign(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.MultiplyAssignChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.MultiplyAssignChecked(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.MultiplyChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.MultiplyChecked(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.Or: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.Or(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.OrAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.OrAssign(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.OrElse: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.OrElse(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.Power: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.Power(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.PowerAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.PowerAssign(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.RightShift: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.RightShift(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.RightShiftAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.RightShiftAssign(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.Subtract: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.Subtract(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.SubtractAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.SubtractAssign(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.SubtractAssignChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.SubtractAssignChecked(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.SubtractChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader);
                value=T.SubtractChecked(Left,Right,Method); break;
            }
            case Expressions.ExpressionType.Equal: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBooleanMethod(ref reader);
                value=T.Equal(Left,Right,IsLiftedToNull,Method); break;
            }
            case Expressions.ExpressionType.GreaterThan: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBooleanMethod(ref reader);
                value=T.GreaterThan(Left,Right,IsLiftedToNull,Method); break;
            }
            case Expressions.ExpressionType.GreaterThanOrEqual: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBooleanMethod(ref reader);
                value=T.GreaterThanOrEqual(Left,Right,IsLiftedToNull,Method); break;
            }
            case Expressions.ExpressionType.LessThan: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBooleanMethod(ref reader);
                value=T.LessThan(Left,Right,IsLiftedToNull,Method); break;
            }
            case Expressions.ExpressionType.LessThanOrEqual: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBooleanMethod(ref reader);
                value=T.LessThanOrEqual(Left,Right,IsLiftedToNull,Method); break;
            }
            case Expressions.ExpressionType.NotEqual: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBooleanMethod(ref reader);
                value=T.NotEqual(Left,Right,IsLiftedToNull,Method); break;
            }
            case Expressions.ExpressionType.ArrayLength: {
                var Operand = Unary.InternalDeserialize(ref reader);
                value=T.ArrayLength(Operand); break;
            }
            case Expressions.ExpressionType.Quote: {
                var Operand = Unary.InternalDeserialize(ref reader);
                value=T.Quote(Operand); break;
            }
            case Expressions.ExpressionType.Convert: {
                var (Operand, Type, Method)=Unary.InternalDeserializeTypeMethod(ref reader);
                value=T.Convert(Operand,Type,Method); break;
            }
            case Expressions.ExpressionType.ConvertChecked: {
                var (Operand, Type, Method)=Unary.InternalDeserializeTypeMethod(ref reader);
                value=T.ConvertChecked(Operand,Type,Method); break;
            }
            case Expressions.ExpressionType.Decrement: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader);
                value=T.Decrement(Operand,Method); break;
            }
            case Expressions.ExpressionType.Increment: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader);
                value=T.Increment(Operand,Method); break;
            }
            case Expressions.ExpressionType.IsFalse: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader);
                value=T.IsFalse(Operand,Method); break;
            }
            case Expressions.ExpressionType.IsTrue: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader);
                value=T.IsTrue(Operand,Method); break;
            }
            case Expressions.ExpressionType.Negate: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader);
                value=T.Negate(Operand,Method); break;
            }
            case Expressions.ExpressionType.NegateChecked: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader);
                value=T.NegateChecked(Operand,Method); break;
            }
            case Expressions.ExpressionType.Not: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader);
                value=T.Not(Operand,Method); break;
            }
            case Expressions.ExpressionType.OnesComplement: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader);
                value=T.OnesComplement(Operand,Method); break;
            }
            case Expressions.ExpressionType.PostDecrementAssign: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader);
                value=T.PostDecrementAssign(Operand,Method); break;
            }
            case Expressions.ExpressionType.PostIncrementAssign: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader);
                value=T.PostIncrementAssign(Operand,Method); break;
            }
            case Expressions.ExpressionType.PreDecrementAssign: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader);
                value=T.PreDecrementAssign(Operand,Method); break;
            }
            case Expressions.ExpressionType.PreIncrementAssign: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader);
                value=T.PreIncrementAssign(Operand,Method); break;
            }
            case Expressions.ExpressionType.UnaryPlus: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader);
                value=T.UnaryPlus(Operand,Method); break;
            }
            case Expressions.ExpressionType.Throw: {
                var (Operand, Type)=Unary.InternalDeserializeType(ref reader);
                value=T.Throw(Operand,Type); break;
            }
            case Expressions.ExpressionType.TypeAs: {
                var (Operand, Type)=Unary.InternalDeserializeType(ref reader);
                value=T.TypeAs(Operand,Type); break;
            }
            case Expressions.ExpressionType.Unbox: {
                var (Operand, Type)=Unary.InternalDeserializeType(ref reader);
                value=T.Unbox(Operand,Type); break;
            }
            case Expressions.ExpressionType.TypeEqual          :value=TypeBinary.InternalDeserializeTypeEqual(ref reader);break;
            case Expressions.ExpressionType.TypeIs             :value=TypeBinary.InternalDeserializeTypeIs(ref reader);break;
            case Expressions.ExpressionType.Conditional        :value=Conditional.Instance.DeserializeConditional(ref reader);break;
            case Expressions.ExpressionType.Constant           :value=Constant.Instance.DeserializeConstant(ref reader);break;
            case Expressions.ExpressionType.Parameter          :value=Parameter.Instance.DeserializeParameter(ref reader);break;
            case Expressions.ExpressionType.Lambda             :value=Lambda.Instance.DeserializeLambda(ref reader);break;
            case Expressions.ExpressionType.Call               :value=MethodCall.Instance.DeserializeMethodCall(ref reader);break;
            case Expressions.ExpressionType.Invoke             :value=Invocation.Instance.DeserializeInvocation(ref reader);break;
            case Expressions.ExpressionType.New                :value=New.Instance.DeserializeNew(ref reader);break;
            case Expressions.ExpressionType.NewArrayInit       :value=NewArray.Instance.DeserializeNewArray(ref reader);break;
            case Expressions.ExpressionType.NewArrayBounds     :value=NewArray.Instance.DeserializeNewArray(ref reader);break;
            case Expressions.ExpressionType.ListInit           :value=ListInit.Instance.DeserializeListInit(ref reader);break;
            case Expressions.ExpressionType.MemberAccess       :value=MemberAccess.Instance.DeserializeMember(ref reader);break;
            case Expressions.ExpressionType.MemberInit         :value=MemberInit.Instance.DeserializeMemberInit(ref reader);break;
            case Expressions.ExpressionType.Block              :value=Block.Instance.DeserializeBlock(ref reader);break;
            case Expressions.ExpressionType.DebugInfo          :
            case Expressions.ExpressionType.Dynamic            :
            case Expressions.ExpressionType.Default            :value=Default.Instance.DeserializeDefault(ref reader);break;
            case Expressions.ExpressionType.Extension          :break;
            case Expressions.ExpressionType.Goto               :value=Goto.Instance.DeserializeGoto(ref reader);break;
            case Expressions.ExpressionType.Index              :value=Index.Instance.DeserializeIndex(ref reader);break;
            case Expressions.ExpressionType.Label              :value=Label.Instance.DeserializeLabel(ref reader);break;
            case Expressions.ExpressionType.RuntimeVariables   :break;
            case Expressions.ExpressionType.Loop               :value=Loop.Instance.DeserializeLoop(ref reader);break;
            case Expressions.ExpressionType.Switch             :value=Switch.Instance.DeserializeSwitch(ref reader);break;
            case Expressions.ExpressionType.Try                :value=Try.Instance.DeserializeTry(ref reader);break;
            default:throw new NotSupportedException(NodeType.ToString());
        }
        
        //value=this.Deserialize(ref reader,options);
    }
}
