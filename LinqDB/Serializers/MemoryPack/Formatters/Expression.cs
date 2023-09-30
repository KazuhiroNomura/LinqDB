using MemoryPack;
using System.Buffers;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;


using Reader = MemoryPackReader;
using T = Expressions.Expression;
public class Expression:MemoryPackFormatter<T> {
    public static readonly Expression Instance=new();
    public static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{
        switch(value.NodeType){
            case Expressions.ExpressionType.ArrayIndex           :
            case Expressions.ExpressionType.Assign               :Binary.WriteLeftRight(ref writer,(Expressions.BinaryExpression)value);break;
            case Expressions.ExpressionType.Coalesce             :Binary.WriteLeftRightLambda(ref writer,(Expressions.BinaryExpression)value);break;
            case Expressions.ExpressionType.Add                  :
            case Expressions.ExpressionType.AddChecked           :
            case Expressions.ExpressionType.And                  :
            case Expressions.ExpressionType.AndAlso              :
            case Expressions.ExpressionType.Divide               :
            case Expressions.ExpressionType.ExclusiveOr          :
            case Expressions.ExpressionType.LeftShift            :
            case Expressions.ExpressionType.Modulo               :
            case Expressions.ExpressionType.Multiply             :
            case Expressions.ExpressionType.MultiplyChecked      :
            case Expressions.ExpressionType.Or                   :
            case Expressions.ExpressionType.OrElse               :
            case Expressions.ExpressionType.Power                :
            case Expressions.ExpressionType.RightShift           :
            case Expressions.ExpressionType.Subtract             :
            case Expressions.ExpressionType.SubtractChecked      :Binary.WriteLeftRightMethod(ref writer,(Expressions.BinaryExpression)value);break;
            case Expressions.ExpressionType.AddAssign            :
            case Expressions.ExpressionType.AddAssignChecked     :
            case Expressions.ExpressionType.DivideAssign         :
            case Expressions.ExpressionType.AndAssign            :
            case Expressions.ExpressionType.ExclusiveOrAssign    :
            case Expressions.ExpressionType.LeftShiftAssign      :
            case Expressions.ExpressionType.ModuloAssign         :
            case Expressions.ExpressionType.MultiplyAssign       :
            case Expressions.ExpressionType.MultiplyAssignChecked:
            case Expressions.ExpressionType.OrAssign             :
            case Expressions.ExpressionType.PowerAssign          :
            case Expressions.ExpressionType.RightShiftAssign     :
            case Expressions.ExpressionType.SubtractAssign       :
            case Expressions.ExpressionType.SubtractAssignChecked:Binary.WriteLeftRightMethodLambda(ref writer,(Expressions.BinaryExpression)value);break;
            case Expressions.ExpressionType.Equal                :
            case Expressions.ExpressionType.GreaterThan          :
            case Expressions.ExpressionType.GreaterThanOrEqual   :
            case Expressions.ExpressionType.LessThan             :
            case Expressions.ExpressionType.LessThanOrEqual      :
            case Expressions.ExpressionType.NotEqual             :Binary.WriteLeftRightBooleanMethod(ref writer,(Expressions.BinaryExpression)value);break;
            case Expressions.ExpressionType.ArrayLength          :
            case Expressions.ExpressionType.Quote                :Unary.WriteOperand(ref writer,(Expressions.UnaryExpression)value);break;
            case Expressions.ExpressionType.Throw                :
            case Expressions.ExpressionType.TypeAs               :
            case Expressions.ExpressionType.Unbox                :Unary.WriteOperandType(ref writer,(Expressions.UnaryExpression)value);break;
            case Expressions.ExpressionType.Convert              :
            case Expressions.ExpressionType.ConvertChecked       :Unary.WriteOperandTypeMethod(ref writer,(Expressions.UnaryExpression)value);break;
            case Expressions.ExpressionType.Decrement            :
            case Expressions.ExpressionType.Increment            :
            case Expressions.ExpressionType.IsFalse              :
            case Expressions.ExpressionType.IsTrue               :
            case Expressions.ExpressionType.Negate               :
            case Expressions.ExpressionType.NegateChecked        :
            case Expressions.ExpressionType.Not                  :
            case Expressions.ExpressionType.OnesComplement       :
            case Expressions.ExpressionType.PostDecrementAssign  :
            case Expressions.ExpressionType.PostIncrementAssign  :
            case Expressions.ExpressionType.PreDecrementAssign   :
            case Expressions.ExpressionType.PreIncrementAssign   :
            case Expressions.ExpressionType.UnaryPlus            :Unary.WriteOperandMethod(ref writer,(Expressions.UnaryExpression)value);break;
            case Expressions.ExpressionType.TypeEqual            :
            case Expressions.ExpressionType.TypeIs               :TypeBinary.Write(ref writer,(Expressions.TypeBinaryExpression)value);break;
            case Expressions.ExpressionType.Conditional          :Conditional.Write(ref writer,(Expressions.ConditionalExpression)value);break;
            case Expressions.ExpressionType.Constant             :Constant.Write(ref writer,(Expressions.ConstantExpression)value);break;
            case Expressions.ExpressionType.Parameter            :Parameter.Write(ref writer,(Expressions.ParameterExpression)value);break;
            case Expressions.ExpressionType.Lambda               :Lambda.Write(ref writer,(Expressions.LambdaExpression)value);break;
            case Expressions.ExpressionType.Call                 :MethodCall.Write(ref writer,(Expressions.MethodCallExpression)value);break;
            case Expressions.ExpressionType.Invoke               :Invocation.Write(ref writer,(Expressions.InvocationExpression)value);break;
            case Expressions.ExpressionType.New                  :New.Write(ref writer,(Expressions.NewExpression)value);break;
            case Expressions.ExpressionType.NewArrayBounds       :
            case Expressions.ExpressionType.NewArrayInit         :NewArray.Write(ref writer,(Expressions.NewArrayExpression)value);break;//this.Serialize(ref writer,(Expressions.Expressions.(NewArrayExpression)value).Expressions);break;
            case Expressions.ExpressionType.ListInit             :ListInit.Write(ref writer,(Expressions.ListInitExpression)value);break;
            case Expressions.ExpressionType.MemberAccess         :MemberAccess.Write(ref writer,(Expressions.MemberExpression)value);break;
            case Expressions.ExpressionType.MemberInit           :MemberInit.Write(ref writer,(Expressions.MemberInitExpression)value);break;
            case Expressions.ExpressionType.Block                :Block.Write(ref writer,(Expressions.BlockExpression)value);break;
            case Expressions.ExpressionType.DebugInfo            :DebugInfo.Write(ref writer,(Expressions.DebugInfoExpression)value);break;
            case Expressions.ExpressionType.Dynamic              :Dynamic.Write(ref writer,(Expressions.DynamicExpression)value);break;
            case Expressions.ExpressionType.Default              :Default.Write(ref writer,(Expressions.DefaultExpression)value);break;
            //case Expressions.ExpressionType.Extension            :
            case Expressions.ExpressionType.Goto                 :Goto.Write(ref writer,(Expressions.GotoExpression)value);break;
            case Expressions.ExpressionType.Index                :Index.Write(ref writer,(Expressions.IndexExpression)value);break;
            case Expressions.ExpressionType.Label                :Label.Write(ref writer,(Expressions.LabelExpression)value);break;
            //case Expressions.ExpressionType.RuntimeVariables     :
            case Expressions.ExpressionType.Loop                 :Loop.Write(ref writer,(Expressions.LoopExpression)value);break;
            case Expressions.ExpressionType.Switch               :Switch.Write(ref writer,(Expressions.SwitchExpression)value);break;
            //case Expressions.ExpressionType.Try                  :Try.Write(ref writer,(Expressions.TryExpression)value);break;
            default                                              :Try.Write(ref writer,(Expressions.TryExpression)value);break;
        }
    }
    public static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        if(writer.TryWriteNil(value))return;
        Write(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value)=>WriteNullable(ref writer,value);
    internal static T Read(ref Reader reader) {
        
        T value = default!;
        var NodeType=reader.ReadNodeType();
        
        switch(NodeType){
            case Expressions.ExpressionType.ArrayIndex: {
                var (array, index)=Binary.ReadLeftRight(ref reader);
                value=T.ArrayIndex(array,index);break;
            }
            case Expressions.ExpressionType.Assign: {
                var (left, right)=Binary.ReadLeftRight(ref reader);
                value=T.Assign(left,right);break;
            }
            case Expressions.ExpressionType.Coalesce: {
                var (left, right,lambda)=Binary.ReadLeftRightLambda(ref reader);
                value=T.Coalesce(left,right,lambda);break;
            }
            case Expressions.ExpressionType.Add: {
                var (left, right, method)=Binary.ReadLeftRightMethod(ref reader);
                value=T.Add(left,right,method);break;
            }
            case Expressions.ExpressionType.AddChecked: {
                var (left, right, method)=Binary.ReadLeftRightMethod(ref reader);
                value=T.AddChecked(left,right,method);break;
            }
            case Expressions.ExpressionType.And: {
                var (left, right, method)=Binary.ReadLeftRightMethod(ref reader);
                value=T.And(left,right,method);break;
            }
            case Expressions.ExpressionType.AndAlso: {
                var (left, right, method)=Binary.ReadLeftRightMethod(ref reader);
                value=T.AndAlso(left,right,method);break;
            }
            case Expressions.ExpressionType.Divide: {
                var (left, right, method)=Binary.ReadLeftRightMethod(ref reader);
                value=T.Divide(left,right,method);break;
            }
            case Expressions.ExpressionType.ExclusiveOr: {
                var (left, right, method)=Binary.ReadLeftRightMethod(ref reader);
                value=T.ExclusiveOr(left,right,method);break;
            }
            case Expressions.ExpressionType.LeftShift: {
                var (left, right, method)=Binary.ReadLeftRightMethod(ref reader);
                value=T.LeftShift(left,right,method);break;
            }
            case Expressions.ExpressionType.Modulo: {
                var (left, right, method)=Binary.ReadLeftRightMethod(ref reader);
                value=T.Modulo(left,right,method);break;
            }
            case Expressions.ExpressionType.Multiply: {
                var (left, right, method)=Binary.ReadLeftRightMethod(ref reader);
                value=T.Multiply(left,right,method);break;
            }
            case Expressions.ExpressionType.MultiplyChecked: {
                var (left, right, method)=Binary.ReadLeftRightMethod(ref reader);
                value=T.MultiplyChecked(left,right,method);break;
            }
            case Expressions.ExpressionType.Or: {
                var (left, right, method)=Binary.ReadLeftRightMethod(ref reader);
                value=T.Or(left,right,method);break;
            }
            case Expressions.ExpressionType.OrElse: {
                var (left, right, method)=Binary.ReadLeftRightMethod(ref reader);
                value=T.OrElse(left,right,method);break;
            }
            case Expressions.ExpressionType.Power: {
                var (left, right, method)=Binary.ReadLeftRightMethod(ref reader);
                value=T.Power(left,right,method);break;
            }
            case Expressions.ExpressionType.RightShift: {
                var (left, right, method)=Binary.ReadLeftRightMethod(ref reader);
                value=T.RightShift(left,right,method);break;
            }
            case Expressions.ExpressionType.Subtract: {
                var (left, right, method)=Binary.ReadLeftRightMethod(ref reader);
                value=T.Subtract(left,right,method);break;
            }
            case Expressions.ExpressionType.SubtractChecked: {
                var (left, right, method)=Binary.ReadLeftRightMethod(ref reader);
                value=T.SubtractChecked(left,right,method);break;
            }
            case Expressions.ExpressionType.AddAssign: {
                var (left, right, method,conversion)=Binary.ReadLeftRightMethodLambda(ref reader);
                value=T.AddAssign(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.AddAssignChecked: {
                var (left, right, method,conversion)=Binary.ReadLeftRightMethodLambda(ref reader);
                value=T.AddAssignChecked(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.AndAssign: {
                var (left, right, method,conversion)=Binary.ReadLeftRightMethodLambda(ref reader);
                value=T.AndAssign(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.DivideAssign: {
                var (left, right, method,conversion)=Binary.ReadLeftRightMethodLambda(ref reader);
                value=T.DivideAssign(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.ExclusiveOrAssign: {
                var (left, right, method,conversion)=Binary.ReadLeftRightMethodLambda(ref reader);
                value=T.ExclusiveOrAssign(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.LeftShiftAssign: {
                var (left, right, method,conversion)=Binary.ReadLeftRightMethodLambda(ref reader);
                value=T.LeftShiftAssign(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.ModuloAssign: {
                var (left, right, method,conversion)=Binary.ReadLeftRightMethodLambda(ref reader);
                value=T.ModuloAssign(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.MultiplyAssign: {
                var (left, right, method,conversion)=Binary.ReadLeftRightMethodLambda(ref reader);
                value=T.MultiplyAssign(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.MultiplyAssignChecked: {
                var (left, right, method,conversion)=Binary.ReadLeftRightMethodLambda(ref reader);
                value=T.MultiplyAssignChecked(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.OrAssign: {
                var (left, right, method,conversion)=Binary.ReadLeftRightMethodLambda(ref reader);
                value=T.OrAssign(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.PowerAssign: {
                var (left, right, method,conversion)=Binary.ReadLeftRightMethodLambda(ref reader);
                value=T.PowerAssign(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.RightShiftAssign: {
                var (left, right, method,conversion)=Binary.ReadLeftRightMethodLambda(ref reader);
                value=T.RightShiftAssign(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.SubtractAssign: {
                var (left, right, method,conversion)=Binary.ReadLeftRightMethodLambda(ref reader);
                value=T.SubtractAssign(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.SubtractAssignChecked: {
                var (left, right, method,conversion)=Binary.ReadLeftRightMethodLambda(ref reader);
                value=T.SubtractAssignChecked(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.Equal: {
                var (left, right, isLiftedToNull, method)=Binary.ReadLeftRightBooleanMethod(ref reader);
                value=T.Equal(left,right,isLiftedToNull,method);break;
            }
            case Expressions.ExpressionType.GreaterThan: {
                var (left, right, isLiftedToNull, method)=Binary.ReadLeftRightBooleanMethod(ref reader);
                value=T.GreaterThan(left,right,isLiftedToNull,method);break;
            }
            case Expressions.ExpressionType.GreaterThanOrEqual: {
                var (left, right, isLiftedToNull, method)=Binary.ReadLeftRightBooleanMethod(ref reader);
                value=T.GreaterThanOrEqual(left,right,isLiftedToNull,method);break;
            }
            case Expressions.ExpressionType.LessThan: {
                var (left, right, isLiftedToNull, method)=Binary.ReadLeftRightBooleanMethod(ref reader);
                value=T.LessThan(left,right,isLiftedToNull,method);break;
            }
            case Expressions.ExpressionType.LessThanOrEqual: {
                var (left, right, isLiftedToNull, method)=Binary.ReadLeftRightBooleanMethod(ref reader);
                value=T.LessThanOrEqual(left,right,isLiftedToNull,method);break;
            }
            case Expressions.ExpressionType.NotEqual: {
                var (left, right, isLiftedToNull, method)=Binary.ReadLeftRightBooleanMethod(ref reader);
                value=T.NotEqual(left,right,isLiftedToNull,method);break;
            }
            case Expressions.ExpressionType.ArrayLength: {
                var operand = Unary.ReadOperand(ref reader);
                value=T.ArrayLength(operand);break;
            }
            case Expressions.ExpressionType.Quote: {
                var operand = Unary.ReadOperand(ref reader);
                value=T.Quote(operand);break;
            }
            case Expressions.ExpressionType.Convert: {
                var (operand, Type, method)=Unary.ReadOperandTypeMethod(ref reader);
                value=T.Convert(operand,Type,method);break;
            }
            case Expressions.ExpressionType.ConvertChecked: {
                var (operand, Type, method)=Unary.ReadOperandTypeMethod(ref reader);
                value=T.ConvertChecked(operand,Type,method);break;
            }
            case Expressions.ExpressionType.Decrement: {
                var (operand, method)=Unary.ReadOperandMethod(ref reader);
                value=T.Decrement(operand,method);break;
            }
            case Expressions.ExpressionType.Increment: {
                var (operand, method)=Unary.ReadOperandMethod(ref reader);
                value=T.Increment(operand,method);break;
            }
            case Expressions.ExpressionType.IsFalse: {
                var (operand, method)=Unary.ReadOperandMethod(ref reader);
                value=T.IsFalse(operand,method);break;
            }
            case Expressions.ExpressionType.IsTrue: {
                var (operand, method)=Unary.ReadOperandMethod(ref reader);
                value=T.IsTrue(operand,method);break;
            }
            case Expressions.ExpressionType.Negate: {
                var (operand, method)=Unary.ReadOperandMethod(ref reader);
                value=T.Negate(operand,method);break;
            }
            case Expressions.ExpressionType.NegateChecked: {
                var (operand, method)=Unary.ReadOperandMethod(ref reader);
                value=T.NegateChecked(operand,method);break;
            }
            case Expressions.ExpressionType.Not: {
                var (operand, method)=Unary.ReadOperandMethod(ref reader);
                value=T.Not(operand,method);break;
            }
            case Expressions.ExpressionType.OnesComplement: {
                var (operand, method)=Unary.ReadOperandMethod(ref reader);
                value=T.OnesComplement(operand,method);break;
            }
            case Expressions.ExpressionType.PostDecrementAssign: {
                var (operand, method)=Unary.ReadOperandMethod(ref reader);
                value=T.PostDecrementAssign(operand,method);break;
            }
            case Expressions.ExpressionType.PostIncrementAssign: {
                var (operand, method)=Unary.ReadOperandMethod(ref reader);
                value=T.PostIncrementAssign(operand,method);break;
            }
            case Expressions.ExpressionType.PreDecrementAssign: {
                var (operand, method)=Unary.ReadOperandMethod(ref reader);
                value=T.PreDecrementAssign(operand,method);break;
            }
            case Expressions.ExpressionType.PreIncrementAssign: {
                var (operand, method)=Unary.ReadOperandMethod(ref reader);
                value=T.PreIncrementAssign(operand,method);break;
            }
            case Expressions.ExpressionType.UnaryPlus: {
                var (operand, method)=Unary.ReadOperandMethod(ref reader);
                value=T.UnaryPlus(operand,method);break;
            }
            case Expressions.ExpressionType.Throw: {
                var (operand, Type)=Unary.ReadOperandType(ref reader);
                value=T.Throw(operand,Type);break;
            }
            case Expressions.ExpressionType.TypeAs: {
                var (operand, Type)=Unary.ReadOperandType(ref reader);
                value=T.TypeAs(operand,Type);break;
            }
            case Expressions.ExpressionType.Unbox: {
                var (operand, Type)=Unary.ReadOperandType(ref reader);
                value=T.Unbox(operand,Type);break;
            }
            case Expressions.ExpressionType.TypeEqual       :value=TypeBinary.ReadTypeEqual(ref reader);break;
            case Expressions.ExpressionType.TypeIs          :value=TypeBinary.ReadTypeIs(ref reader);break;
            case Expressions.ExpressionType.Conditional     :value=Conditional.Read(ref reader);break;
            case Expressions.ExpressionType.Constant        :value=Constant.Read(ref reader);break;
            case Expressions.ExpressionType.Parameter       :value=Parameter.Read(ref reader);break;
            case Expressions.ExpressionType.Lambda          :value=Lambda.Read(ref reader);break;
            case Expressions.ExpressionType.Call            :value=MethodCall.Read(ref reader);break;
            case Expressions.ExpressionType.Invoke          :value=Invocation.Read(ref reader);break;
            case Expressions.ExpressionType.New             :value=New.Read(ref reader);break;
            case Expressions.ExpressionType.NewArrayBounds  :value=NewArray.ReadNewArrayBounds(ref reader);break;
            case Expressions.ExpressionType.NewArrayInit    :value=NewArray.ReadNewArrayInit(ref reader);break;
            case Expressions.ExpressionType.ListInit        :value=ListInit.Read(ref reader);break;
            case Expressions.ExpressionType.MemberAccess    :value=MemberAccess.Read(ref reader);break;
            case Expressions.ExpressionType.MemberInit      :value=MemberInit.Read(ref reader);break;
            case Expressions.ExpressionType.Block           :value=Block.Read(ref reader);break;
            case Expressions.ExpressionType.DebugInfo       :value=DebugInfo.Read(ref reader);break;
            case Expressions.ExpressionType.Dynamic         :value=Dynamic.Read(ref reader);break;
            case Expressions.ExpressionType.Default         :value=Default.Read(ref reader);break;
            //case Expressions.ExpressionType.Extension       :break;
            case Expressions.ExpressionType.Goto            :value=Goto.Read(ref reader);break;
            case Expressions.ExpressionType.Index           :value=Index.Read(ref reader);break;
            case Expressions.ExpressionType.Label           :value=Label.Read(ref reader);break;
            //case Expressions.ExpressionType.RuntimeVariables:break;
            case Expressions.ExpressionType.Loop            :value=Loop.Read(ref reader);break;
            case Expressions.ExpressionType.Switch          :value=Switch.Read(ref reader);break;
            //case Expressions.ExpressionType.Try             :value=Try.Read(ref reader);break;
            default                                         :value=Try.Read(ref reader);break;
        }
        
        return value;
    }
    internal static T? ReadNullable(ref Reader reader)=>reader.TryReadNil()?null:Read(ref reader);
    public override void Deserialize(ref Reader reader,scoped ref T? value)=>value=ReadNullable(ref reader);
}
