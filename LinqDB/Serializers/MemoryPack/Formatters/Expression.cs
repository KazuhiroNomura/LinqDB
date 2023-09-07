using System;
using System.Buffers;
using MemoryPack;
using Expressions = System.Linq.Expressions;
// ReSharper disable InconsistentNaming
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.Expression;

public class Expression:MemoryPackFormatter<Expressions.Expression>{
    public static readonly Expression Instance=new();
    public void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    public Expressions.Expression Deserialize(ref MemoryPackReader reader){
        Expressions.Expression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Expressions.Expression? value){
        if(value is null){
            writer.WriteBoolean(false);
            return;
        }
        writer.WriteBoolean(true);
        writer.WriteNodeType(value.NodeType);
        switch(value.NodeType){
            case Expressions.ExpressionType.Assign:
            case Expressions.ExpressionType.Coalesce:
            case Expressions.ExpressionType.ArrayIndex:Binary.Serialize_Binary(ref writer,(Expressions.BinaryExpression)value); break;
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
            case Expressions.ExpressionType.SubtractChecked:Binary.Serialize_Binary_MethodInfo(ref writer,(Expressions.BinaryExpression)value); break;
            case Expressions.ExpressionType.Equal:
            case Expressions.ExpressionType.GreaterThan:
            case Expressions.ExpressionType.GreaterThanOrEqual:
            case Expressions.ExpressionType.LessThan:
            case Expressions.ExpressionType.LessThanOrEqual:
            case Expressions.ExpressionType.NotEqual:Binary.Serialize_Binary_bool_MethodInfo(ref writer,(Expressions.BinaryExpression)value); break;
            case Expressions.ExpressionType.ArrayLength        :Unary.Serialize_Unary(ref writer,value);break;
            case Expressions.ExpressionType.Quote              :Unary.Serialize_Unary(ref writer,value);break;
            case Expressions.ExpressionType.Throw              :Unary.Serialize_Unary_Type(ref writer,value);break;
            case Expressions.ExpressionType.TypeAs             :Unary.Serialize_Unary_Type(ref writer,value);break;
            case Expressions.ExpressionType.Unbox              :Unary.Serialize_Unary_Type(ref writer,value);break;
            case Expressions.ExpressionType.Convert            :Unary.Serialize_Unary_Type_MethodInfo(ref writer,value);break;
            case Expressions.ExpressionType.ConvertChecked     :Unary.Serialize_Unary_Type_MethodInfo(ref writer,value);break;
            case Expressions.ExpressionType.Decrement          :Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case Expressions.ExpressionType.Increment          :Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case Expressions.ExpressionType.IsFalse            :Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case Expressions.ExpressionType.IsTrue             :Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case Expressions.ExpressionType.Negate             :Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case Expressions.ExpressionType.NegateChecked      :Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case Expressions.ExpressionType.Not                :Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case Expressions.ExpressionType.OnesComplement     :Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case Expressions.ExpressionType.PostDecrementAssign:Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case Expressions.ExpressionType.PostIncrementAssign:Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case Expressions.ExpressionType.PreDecrementAssign :Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case Expressions.ExpressionType.PreIncrementAssign :Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case Expressions.ExpressionType.UnaryPlus          :Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case Expressions.ExpressionType.TypeEqual          :
            case Expressions.ExpressionType.TypeIs             :TypeBinary.Instance.Serialize(ref writer,(Expressions.TypeBinaryExpression)value); break;
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
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.Expression? value){
        if(!reader.ReadBoolean()) return;
        //if(reader.TryReadNil())return;
        var NodeType=reader.ReadNodeType();
        switch(NodeType){
            case Expressions.ExpressionType.Assign:
            case Expressions.ExpressionType.Coalesce:
            case Expressions.ExpressionType.ArrayIndex:{
                var (Left,Right)=Binary.Deserialize_Binary(ref reader);
                value=Expressions.Expression.MakeBinary(NodeType,Left,Right); break;
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
            case Expressions.ExpressionType.SubtractChecked: {
                var (Left, Right, Method)=Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.MakeBinary(NodeType,Left,Right,false,Method); break;
            }
            case Expressions.ExpressionType.Equal:
            case Expressions.ExpressionType.GreaterThan:
            case Expressions.ExpressionType.GreaterThanOrEqual:
            case Expressions.ExpressionType.LessThan:
            case Expressions.ExpressionType.LessThanOrEqual:
            case Expressions.ExpressionType.NotEqual: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.Deserialize_Binary_bool_MethodInfo(ref reader);
                value=Expressions.Expression.NotEqual(Left,Right,IsLiftedToNull,Method); break;
            }
            //case Expressions.Expressions.ExpressionType.Assign:
            //case Expressions.Expressions.ExpressionType.Coalesce:
            //case Expressions.Expressions.ExpressionType.Add:
            //case Expressions.Expressions.ExpressionType.AddAssign:
            //case Expressions.Expressions.ExpressionType.AddAssignChecked:
            //case Expressions.Expressions.ExpressionType.AddChecked:
            //case Expressions.Expressions.ExpressionType.And:
            //case Expressions.Expressions.ExpressionType.AndAssign:
            //case Expressions.Expressions.ExpressionType.AndAlso:
            //case Expressions.Expressions.ExpressionType.Divide:
            //case Expressions.Expressions.ExpressionType.DivideAssign:
            //case Expressions.Expressions.ExpressionType.ExclusiveOr:
            //case Expressions.Expressions.ExpressionType.ExclusiveOrAssign:
            //case Expressions.Expressions.ExpressionType.LeftShift:
            //case Expressions.Expressions.ExpressionType.LeftShiftAssign:
            //case Expressions.Expressions.ExpressionType.Modulo:
            //case Expressions.Expressions.ExpressionType.ModuloAssign:
            //case Expressions.Expressions.ExpressionType.Multiply:
            //case Expressions.Expressions.ExpressionType.MultiplyAssign:
            //case Expressions.Expressions.ExpressionType.MultiplyAssignChecked:
            //case Expressions.Expressions.ExpressionType.MultiplyChecked:
            //case Expressions.Expressions.ExpressionType.Or:
            //case Expressions.Expressions.ExpressionType.OrAssign:
            //case Expressions.Expressions.ExpressionType.OrElse:
            //case Expressions.Expressions.ExpressionType.Power:
            //case Expressions.Expressions.ExpressionType.PowerAssign:
            //case Expressions.Expressions.ExpressionType.RightShift:
            //case Expressions.Expressions.ExpressionType.RightShiftAssign:
            //case Expressions.Expressions.ExpressionType.Subtract:
            //case Expressions.Expressions.ExpressionType.SubtractAssign:
            //case Expressions.Expressions.ExpressionType.SubtractAssignChecked:
            //case Expressions.Expressions.ExpressionType.SubtractChecked:
            //case Expressions.Expressions.ExpressionType.Equal:
            //case Expressions.Expressions.ExpressionType.GreaterThan:
            //case Expressions.Expressions.ExpressionType.GreaterThanOrEqual:
            //case Expressions.Expressions.ExpressionType.LessThan:
            //case Expressions.Expressions.ExpressionType.LessThanOrEqual:
            //case Expressions.Expressions.ExpressionType.NotEqual:
            //case Expressions.Expressions.ExpressionType.ArrayIndex:value=CustomSerializerMemoryPack.Binary.Deserialize(ref reader);break;
            case Expressions.ExpressionType.ArrayLength        :
            case Expressions.ExpressionType.Quote              :{
                var Operand= Unary.Deserialize_Unary(ref reader);
                value=Expressions.Expression.ArrayLength(Operand);break;
            }
            case Expressions.ExpressionType.Throw              :
            case Expressions.ExpressionType.TypeAs             :
            case Expressions.ExpressionType.Unbox              :{
                var (Operand,Type)=Unary.Deserialize_Unary_Type(ref reader);
                value=Expressions.Expression.MakeUnary(NodeType,Operand,Type);break;
            }
            case Expressions.ExpressionType.Convert            :
            case Expressions.ExpressionType.ConvertChecked     :{
                var(Operand,Type,Method)=Unary.Deserialize_Unary_Type_MethodInfo(ref reader);
                value=Expressions.Expression.ConvertChecked(Operand,Type,Method);break;
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
                var (Operand,Method)=Unary.Deserialize_Unary_MethodInfo(ref reader);
                value=Expressions.Expression.PreIncrementAssign(Operand,Method);break;
            }
            //case Expressions.ExpressionType.ArrayLength: {
            //    var Operand=Unary.Deserialize_Unary(ref reader);
            //    value=Expressions.Expression.ArrayLength(Operand); break;
            //}
            //case Expressions.ExpressionType.Quote: {
            //    var Operand=Unary.Deserialize_Unary(ref reader);
            //    value=Expressions.Expression.Quote(Operand); break;
            //}
            //case Expressions.ExpressionType.Convert: {
            //    var (Operand, Type, Method)=Unary.Deserialize_Unary_Type_MethodInfo(ref reader);
            //    value=Expressions.Expression.Convert(Operand,Type,Method); break;
            //}
            //case Expressions.ExpressionType.ConvertChecked: {
            //    var (Operand, Type, Method)=Unary.Deserialize_Unary_Type_MethodInfo(ref reader);
            //    value=Expressions.Expression.ConvertChecked(Operand,Type,Method); break;
            //}
            //case Expressions.ExpressionType.Decrement: {
            //    var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader);
            //    value=Expressions.Expression.Decrement(Operand,Method); break;
            //}
            //case Expressions.ExpressionType.Increment: {
            //    var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader);
            //    value=Expressions.Expression.Increment(Operand,Method); break;
            //}
            //case Expressions.ExpressionType.IsFalse: {
            //    var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader);
            //    value=Expressions.Expression.IsFalse(Operand,Method); break;
            //}
            //case Expressions.ExpressionType.IsTrue: {
            //    var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader);
            //    value=Expressions.Expression.IsTrue(Operand,Method); break;
            //}
            //case Expressions.ExpressionType.Negate: {
            //    var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader);
            //    value=Expressions.Expression.Negate(Operand,Method); break;
            //}
            //case Expressions.ExpressionType.NegateChecked: {
            //    var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader);
            //    value=Expressions.Expression.NegateChecked(Operand,Method); break;
            //}
            //case Expressions.ExpressionType.Not: {
            //    var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader);
            //    value=Expressions.Expression.Not(Operand,Method); break;
            //}
            //case Expressions.ExpressionType.OnesComplement: {
            //    var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader);
            //    value=Expressions.Expression.OnesComplement(Operand,Method); break;
            //}
            //case Expressions.ExpressionType.PostDecrementAssign: {
            //    var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader);
            //    value=Expressions.Expression.PostDecrementAssign(Operand,Method); break;
            //}
            //case Expressions.ExpressionType.PostIncrementAssign: {
            //    var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader);
            //    value=Expressions.Expression.PostIncrementAssign(Operand,Method); break;
            //}
            //case Expressions.ExpressionType.PreDecrementAssign: {
            //    var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader);
            //    value=Expressions.Expression.PreDecrementAssign(Operand,Method); break;
            //}
            //case Expressions.ExpressionType.PreIncrementAssign: {
            //    var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader);
            //    value=Expressions.Expression.PreIncrementAssign(Operand,Method); break;
            //}
            //case Expressions.ExpressionType.Throw: {
            //    var (Operand, Type)=Unary.Deserialize_Unary_Type(ref reader);
            //    value=Expressions.Expression.Throw(Operand,Type); break;
            //}
            //case Expressions.ExpressionType.TypeAs: {
            //    var (Operand, Type)=Unary.Deserialize_Unary_Type(ref reader);
            //    value=Expressions.Expression.TypeAs(Operand,Type); break;
            //}
            //case Expressions.ExpressionType.UnaryPlus: {
            //    var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader);
            //    value=Expressions.Expression.UnaryPlus(Operand,Method); break;
            //}
            //case Expressions.ExpressionType.Unbox: {
            //    var (Operand, Type)=Unary.Deserialize_Unary_Type(ref reader);
            //    value=Expressions.Expression.Unbox(Operand,Type); break;
            //}
            //////readonly object[] Objects2=new object[2];

            //case Expressions.Expressions.ExpressionType.ArrayLength        :
            //case Expressions.Expressions.ExpressionType.Convert            :
            //case Expressions.Expressions.ExpressionType.ConvertChecked     :
            //case Expressions.Expressions.ExpressionType.Decrement          :
            //case Expressions.Expressions.ExpressionType.Increment          :
            //case Expressions.Expressions.ExpressionType.IsFalse            :
            //case Expressions.Expressions.ExpressionType.IsTrue             :
            //case Expressions.Expressions.ExpressionType.Negate             :
            //case Expressions.Expressions.ExpressionType.NegateChecked      :
            //case Expressions.Expressions.ExpressionType.Not                :
            //case Expressions.Expressions.ExpressionType.OnesComplement     :
            //case Expressions.Expressions.ExpressionType.PostDecrementAssign:
            //case Expressions.Expressions.ExpressionType.PostIncrementAssign:
            //case Expressions.Expressions.ExpressionType.PreDecrementAssign :
            //case Expressions.Expressions.ExpressionType.PreIncrementAssign :
            //case Expressions.Expressions.ExpressionType.Quote              :
            //case Expressions.Expressions.ExpressionType.Throw              :
            //case Expressions.Expressions.ExpressionType.TypeAs             :
            //case Expressions.Expressions.ExpressionType.UnaryPlus          :
            //case Expressions.Expressions.ExpressionType.Unbox              :value=CustomSerializerMemoryPack.Unary.Deserialize(ref reader);break;
            case Expressions.ExpressionType.TypeEqual          :
            case Expressions.ExpressionType.TypeIs             :value=TypeBinary.Instance.DeserializeTypeBinary(ref reader);break;
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
