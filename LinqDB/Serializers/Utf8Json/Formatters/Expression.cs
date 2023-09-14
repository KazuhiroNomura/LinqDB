using System;
using Expressions=System.Linq.Expressions;
using Utf8Json;
using System.Diagnostics;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.Expression;
using C=Serializer;
public class Expression:IJsonFormatter<T> {
    public static readonly Expression Instance=new();
    internal static void SerializeNullable(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.WriteIsNull(value))return;
        Instance.Serialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver) {
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        writer.WriteString(value.NodeType.ToString());
        writer.WriteValueSeparator();
        switch(value.NodeType){
            case Expressions.ExpressionType.ArrayIndex           :
            case Expressions.ExpressionType.Assign               :Binary.InternalSerialize(ref writer,(Expressions.BinaryExpression)value,Resolver); break;
            case Expressions.ExpressionType.Coalesce             :Binary.InternalSerializeLambda(ref writer,(Expressions.BinaryExpression)value,Resolver); break;
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
            case Expressions.ExpressionType.SubtractChecked      :Binary.InternalSerializeMethod(ref writer,(Expressions.BinaryExpression)value,Resolver); break;
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
            case Expressions.ExpressionType.SubtractAssignChecked:Binary.InternalSerializeMethodLambda(ref writer,(Expressions.BinaryExpression)value,Resolver); break;
            case Expressions.ExpressionType.Equal                :
            case Expressions.ExpressionType.GreaterThan          :
            case Expressions.ExpressionType.GreaterThanOrEqual   :
            case Expressions.ExpressionType.LessThan             :
            case Expressions.ExpressionType.LessThanOrEqual      :
            case Expressions.ExpressionType.NotEqual             :Binary.InternalSerializeBooleanMethod(ref writer,(Expressions.BinaryExpression)value,Resolver); break;
            case Expressions.ExpressionType.ArrayLength          : 
            case Expressions.ExpressionType.Quote                :Unary.InternalSerialize(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.Throw                : 
            case Expressions.ExpressionType.TypeAs               : 
            case Expressions.ExpressionType.Unbox                :Unary.InternalSerializeType(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.Convert              : 
            case Expressions.ExpressionType.ConvertChecked       :Unary.InternalSerializeTypeMethod(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
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
            case Expressions.ExpressionType.UnaryPlus            :Unary.InternalSerializeMethod(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.TypeEqual            :
            case Expressions.ExpressionType.TypeIs               : TypeBinary.InternalSerialize(ref writer,(Expressions.TypeBinaryExpression)value,Resolver); break;
            case Expressions.ExpressionType.Conditional          : Conditional.Instance.Serialize(ref writer,(Expressions.ConditionalExpression)value,Resolver);break;
            case Expressions.ExpressionType.Constant             : Constant.Instance.Serialize(ref writer,(Expressions.ConstantExpression)value,Resolver);break;
            case Expressions.ExpressionType.Parameter            : Parameter.Instance.Serialize(ref writer,(Expressions.ParameterExpression)value,Resolver);break;
            case Expressions.ExpressionType.Lambda               : Lambda.Instance.Serialize(ref writer,(Expressions.LambdaExpression)value,Resolver); break;
            case Expressions.ExpressionType.Call                 : MethodCall.Instance.Serialize(ref writer,(Expressions.MethodCallExpression)value,Resolver);break;
            case Expressions.ExpressionType.Invoke               : Invocation.Instance.Serialize(ref writer,(Expressions.InvocationExpression)value,Resolver);break;
            case Expressions.ExpressionType.New                  : New.Instance.Serialize(ref writer,(Expressions.NewExpression)value,Resolver);break;
            case Expressions.ExpressionType.NewArrayBounds       :
            case Expressions.ExpressionType.NewArrayInit         : NewArray.Instance.Serialize(ref writer,(Expressions.NewArrayExpression)value,Resolver); break;
            case Expressions.ExpressionType.ListInit             : ListInit.Instance.Serialize(ref writer,(Expressions.ListInitExpression)value,Resolver);break;
            case Expressions.ExpressionType.MemberAccess         : MemberAccess.Instance.Serialize(ref writer,(Expressions.MemberExpression)value,Resolver);break;
            case Expressions.ExpressionType.MemberInit           : MemberInit.Instance.Serialize(ref writer,(Expressions.MemberInitExpression)value,Resolver);break;
            case Expressions.ExpressionType.Block                : Block.Instance.Serialize(ref writer,(Expressions.BlockExpression)value,Resolver);break;
            //case Expressions.ExpressionType.DebugInfo            :
            case Expressions.ExpressionType.Dynamic              : Dynamic.Instance.Serialize(ref writer,(Expressions.DynamicExpression)value,Resolver);break;
            case Expressions.ExpressionType.Default              : Default.Instance.Serialize(ref writer,(Expressions.DefaultExpression)value,Resolver);break;
            case Expressions.ExpressionType.Extension            :
            case Expressions.ExpressionType.Goto                 : Goto.Instance.Serialize(ref writer,(Expressions.GotoExpression)value,Resolver);break;
            case Expressions.ExpressionType.Index                : Index.Instance.Serialize(ref writer,(Expressions.IndexExpression)value,Resolver);break;
            case Expressions.ExpressionType.Label                : Label.Instance.Serialize(ref writer,(Expressions.LabelExpression)value,Resolver);break;
            //case Expressions.ExpressionType.RuntimeVariables     :
            case Expressions.ExpressionType.Loop                 : Loop.Instance.Serialize(ref writer,(Expressions.LoopExpression)value,Resolver);break;
            case Expressions.ExpressionType.Switch               : Switch.Instance.Serialize(ref writer,(Expressions.SwitchExpression)value,Resolver);break;
            case Expressions.ExpressionType.Try                  : Try.InternalSerialize(ref writer,(Expressions.TryExpression)value,Resolver);break;            
            default:
                throw new ArgumentOutOfRangeException(value.NodeType.ToString());
        }
        writer.WriteEndArray();
    }
    public static T? DeserializeNullable(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull())return null;
        return Instance.Deserialize(ref reader,Resolver);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        //if(reader.ReadIsNull())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var NodeTypeName=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        T value;
        var NodeType=Enum.Parse<Expressions.ExpressionType>(NodeTypeName);
        switch(NodeType){
            case Expressions.ExpressionType.ArrayIndex: {
                var (array, index)=Binary.InternalDeserialize(ref reader,Resolver);
                value=T.ArrayIndex(array,index);break;
            }
            case Expressions.ExpressionType.Assign: {
                var (left, right)=Binary.InternalDeserialize(ref reader,Resolver);
                value=T.Assign(left,right);break;
            }
            case Expressions.ExpressionType.Coalesce: {
                var (left, right,conversion)=Binary.InternalDeserializeLambda(ref reader,Resolver);
                value=T.Coalesce(left,right,conversion);break;
            }
            case Expressions.ExpressionType.Add: {
                var (left, right, method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.Add(left,right,method);break;
            }
            case Expressions.ExpressionType.AddChecked: {
                var (left, right, method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.AddChecked(left,right,method);break;
            }
            case Expressions.ExpressionType.And: {
                var (left, right, method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.And(left,right,method);break;
            }
            case Expressions.ExpressionType.AndAlso: {
                var (left, right, method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.AndAlso(left,right,method);break;
            }
            case Expressions.ExpressionType.Divide: {
                var (left, right, method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.Divide(left,right,method);break;
            }
            case Expressions.ExpressionType.ExclusiveOr: {
                var (left, right, method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.ExclusiveOr(left,right,method);break;
            }
            case Expressions.ExpressionType.LeftShift: {
                var (left, right, method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.LeftShift(left,right,method);break;
            }
            case Expressions.ExpressionType.Modulo: {
                var (left, right, method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.Modulo(left,right,method);break;
            }
            case Expressions.ExpressionType.Multiply: {
                var (left, right, method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.Multiply(left,right,method);break;
            }
            case Expressions.ExpressionType.MultiplyChecked: {
                var (left, right, method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.MultiplyChecked(left,right,method);break;
            }
            case Expressions.ExpressionType.Or: {
                var (left, right, method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.Or(left,right,method);break;
            }
            case Expressions.ExpressionType.OrElse: {
                var (left, right, method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.OrElse(left,right,method);break;
            }
            case Expressions.ExpressionType.Power: {
                var (left, right, method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.Power(left,right,method);break;
            }
            case Expressions.ExpressionType.RightShift: {
                var (left, right, method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.RightShift(left,right,method);break;
            }
            case Expressions.ExpressionType.Subtract: {
                var (left, right, method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.Subtract(left,right,method);break;
            }
            case Expressions.ExpressionType.SubtractChecked: {
                var (left, right, method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.SubtractChecked(left,right,method);break;
            }
            case Expressions.ExpressionType.AddAssign: {
                var (left, right, method,conversion)=Binary.InternalDeserializeMethodLambda(ref reader,Resolver);
                value=T.AddAssign(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.AddAssignChecked: {
                var (left, right, method,conversion)=Binary.InternalDeserializeMethodLambda(ref reader,Resolver);
                value=T.AddAssignChecked(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.AndAssign: {
                var (left, right, method,conversion)=Binary.InternalDeserializeMethodLambda(ref reader,Resolver);
                value=T.AndAssign(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.DivideAssign: {
                var (left, right, method,conversion)=Binary.InternalDeserializeMethodLambda(ref reader,Resolver);
                value=T.DivideAssign(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.ExclusiveOrAssign: {
                var (left, right, method,conversion)=Binary.InternalDeserializeMethodLambda(ref reader,Resolver);
                value=T.ExclusiveOrAssign(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.LeftShiftAssign: {
                var (left, right, method,conversion)=Binary.InternalDeserializeMethodLambda(ref reader,Resolver);
                value=T.LeftShiftAssign(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.ModuloAssign: {
                var (left, right, method,conversion)=Binary.InternalDeserializeMethodLambda(ref reader,Resolver);
                value=T.ModuloAssign(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.MultiplyAssign: {
                var (left, right, method,conversion)=Binary.InternalDeserializeMethodLambda(ref reader,Resolver);
                value=T.MultiplyAssign(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.MultiplyAssignChecked: {
                var (left, right, method,conversion)=Binary.InternalDeserializeMethodLambda(ref reader,Resolver);
                value=T.MultiplyAssignChecked(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.OrAssign: {
                var (left, right, method,conversion)=Binary.InternalDeserializeMethodLambda(ref reader,Resolver);
                value=T.OrAssign(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.PowerAssign: {
                var (left, right, method,conversion)=Binary.InternalDeserializeMethodLambda(ref reader,Resolver);
                value=T.PowerAssign(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.RightShiftAssign: {
                var (left, right, method,conversion)=Binary.InternalDeserializeMethodLambda(ref reader,Resolver);
                value=T.RightShiftAssign(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.SubtractAssign: {
                var (left, right, method,conversion)=Binary.InternalDeserializeMethodLambda(ref reader,Resolver);
                value=T.SubtractAssign(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.SubtractAssignChecked: {
                var (left, right, method,conversion)=Binary.InternalDeserializeMethodLambda(ref reader,Resolver);
                value=T.SubtractAssignChecked(left,right,method,conversion);break;
            }
            case Expressions.ExpressionType.Equal: {
                var (left, right, IsLiftedToNull, method)=Binary.InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=T.Equal(left,right,IsLiftedToNull,method);break;
            }
            case Expressions.ExpressionType.GreaterThan: {
                var (left, right, IsLiftedToNull, method)=Binary.InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=T.GreaterThan(left,right,IsLiftedToNull,method);break;
            }
            case Expressions.ExpressionType.GreaterThanOrEqual: {
                var (left, right, IsLiftedToNull, method)=Binary.InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=T.GreaterThanOrEqual(left,right,IsLiftedToNull,method);break;
            }
            case Expressions.ExpressionType.LessThan: {
                var (left, right, IsLiftedToNull, method)=Binary.InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=T.LessThan(left,right,IsLiftedToNull,method);break;
            }
            case Expressions.ExpressionType.LessThanOrEqual: {
                var (left, right, IsLiftedToNull, method)=Binary.InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=T.LessThanOrEqual(left,right,IsLiftedToNull,method);break;
            }
            case Expressions.ExpressionType.NotEqual: {
                var (left, right, IsLiftedToNull, method)=Binary.InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=T.NotEqual(left,right,IsLiftedToNull,method);break;
            }

            case Expressions.ExpressionType.ArrayLength: {
                var operand=Unary.InternalDeserialize(ref reader,Resolver);
                value=T.ArrayLength(operand); break;
            }
            case Expressions.ExpressionType.Quote: {
                var operand=Unary.InternalDeserialize(ref reader,Resolver);
                value=T.Quote(operand); break;
            }
            case Expressions.ExpressionType.Convert:{
                var (operand, Type, method)=Unary.InternalDeserializeTypeMethod(ref reader,Resolver);
                value=T.Convert(operand,Type,method); break;
            }
            case Expressions.ExpressionType.ConvertChecked: {
                var (operand, Type, method)=Unary.InternalDeserializeTypeMethod(ref reader,Resolver);
                value=T.ConvertChecked(operand,Type,method); break;
            }
            case Expressions.ExpressionType.Decrement: {
                var (operand, method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.Decrement(operand,method); break;
            }
            case Expressions.ExpressionType.Increment: {
                var (operand, method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.Increment(operand,method); break;
            }
            case Expressions.ExpressionType.IsFalse: {
                var (operand, method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.IsFalse(operand,method); break;
            }
            case Expressions.ExpressionType.IsTrue: {
                var (operand, method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.IsTrue(operand,method); break;
            }
            case Expressions.ExpressionType.Negate: {
                var (operand, method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.Negate(operand,method); break;
            }
            case Expressions.ExpressionType.NegateChecked: {
                var (operand, method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.NegateChecked(operand,method); break;
            }
            case Expressions.ExpressionType.Not: {
                var (operand, method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.Not(operand,method); break;
            }
            case Expressions.ExpressionType.OnesComplement: {
                var (operand, method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.OnesComplement(operand,method); break;
            }
            case Expressions.ExpressionType.PostDecrementAssign: {
                var (operand, method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.PostDecrementAssign(operand,method); break;
            }
            case Expressions.ExpressionType.PostIncrementAssign: {
                var (operand, method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.PostIncrementAssign(operand,method); break;
            }
            case Expressions.ExpressionType.PreDecrementAssign: {
                var (operand, method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.PreDecrementAssign(operand,method); break;
            }
            case Expressions.ExpressionType.PreIncrementAssign: {
                var (operand, method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.PreIncrementAssign(operand,method); break;
            }
            case Expressions.ExpressionType.UnaryPlus: {
                var (operand, method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.UnaryPlus(operand,method); break;
            }
            case Expressions.ExpressionType.Throw: {
                var (operand, Type)=Unary.InternalDeserializeType(ref reader,Resolver);
                value=T.Throw(operand,Type); break;
            }
            case Expressions.ExpressionType.TypeAs: {
                var (operand, Type)=Unary.InternalDeserializeType(ref reader,Resolver);
                value=T.TypeAs(operand,Type); break;
            }
            case Expressions.ExpressionType.Unbox: {
                var (operand, Type)=Unary.InternalDeserializeType(ref reader,Resolver);
                value=T.Unbox(operand,Type); break;
            }

            case Expressions.ExpressionType.TypeEqual       :value=TypeBinary.InternalDeserializeTypeEqual(ref reader,Resolver);break;
            case Expressions.ExpressionType.TypeIs          :value=TypeBinary.InternalDeserializeTypeIs(ref reader,Resolver);break;

            case Expressions.ExpressionType.Conditional     :value=Conditional.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Constant        :value=Constant.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Parameter       :value=Parameter.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Lambda          :value=Lambda.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Call            :value=MethodCall.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Invoke          :value=Invocation.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.New             :value=New.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.NewArrayBounds  :value=NewArray.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.NewArrayInit    :value=NewArray.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.ListInit        :value=ListInit.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.MemberAccess    :value=MemberAccess.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.MemberInit      :value=MemberInit.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Block           :value=Block.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.DebugInfo       :
            case Expressions.ExpressionType.Dynamic         :value=Dynamic.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Default         :value=Default.Instance.Deserialize(ref reader,Resolver);break;
            //case Expressions.ExpressionType.Extension       :break;
            case Expressions.ExpressionType.Goto            :value=Goto.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Index           :value=Index.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Label           :value=Label.Instance.Deserialize(ref reader,Resolver);break;
            //case Expressions.ExpressionType.RuntimeVariables:break;
            case Expressions.ExpressionType.Loop            :value=Loop.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Switch          :value=Switch.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Try             :value=Try.InternalDeserialize(ref reader,Resolver);break;
            default                                         :throw new NotSupportedException(NodeTypeName);
        }
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
