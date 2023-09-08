using System;
using System.Collections.Generic;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
using System.Diagnostics;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.Expression;
using C=Serializer;
public class Expression:IJsonFormatter<T> {
    public static readonly Expression Instance=new();
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver) {
        if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        writer.WriteString(value.NodeType.ToString());
        writer.WriteValueSeparator();
        switch(value.NodeType){
            case Expressions.ExpressionType.Assign               :
            case Expressions.ExpressionType.Coalesce             :
            case Expressions.ExpressionType.ArrayIndex           :Binary.InternalSerializeBinary(ref writer,(Expressions.BinaryExpression)value,Resolver);break;
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
            case Expressions.ExpressionType.SubtractChecked      :Binary.InternalSerializeBinaryMethod(ref writer,(Expressions.BinaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.Equal                :
            case Expressions.ExpressionType.GreaterThan          :
            case Expressions.ExpressionType.GreaterThanOrEqual   :
            case Expressions.ExpressionType.LessThan             :
            case Expressions.ExpressionType.LessThanOrEqual      :
            case Expressions.ExpressionType.NotEqual             :Binary.InternalSerializeBinaryBooleanMethod(ref writer,(Expressions.BinaryExpression)value,Resolver);break;

            case Expressions.ExpressionType.ArrayLength        : Unary.InternalSerialize(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.Quote              : Unary.InternalSerialize(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.Throw              : Unary.InternalSerializeType(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.TypeAs             : Unary.InternalSerializeType(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.Unbox              : Unary.InternalSerializeType(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.Convert            : Unary.InternalSerializeTypeMethod(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.ConvertChecked     : Unary.InternalSerializeTypeMethod(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.Decrement          : Unary.InternalSerializeMethod(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.Increment          : Unary.InternalSerializeMethod(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.IsFalse            : Unary.InternalSerializeMethod(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.IsTrue             : Unary.InternalSerializeMethod(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.Negate             : Unary.InternalSerializeMethod(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.NegateChecked      : Unary.InternalSerializeMethod(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.Not                : Unary.InternalSerializeMethod(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.OnesComplement     : Unary.InternalSerializeMethod(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.PostDecrementAssign: Unary.InternalSerializeMethod(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.PostIncrementAssign: Unary.InternalSerializeMethod(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.PreDecrementAssign : Unary.InternalSerializeMethod(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.PreIncrementAssign : Unary.InternalSerializeMethod(ref writer,(Expressions.UnaryExpression)value,Resolver);break;
            case Expressions.ExpressionType.UnaryPlus          : Unary.InternalSerializeMethod(ref writer,(Expressions.UnaryExpression)value,Resolver);break;

            case Expressions.ExpressionType.TypeEqual          :
            case Expressions.ExpressionType.TypeIs             : TypeBinary.InternalSerialize(ref writer,(Expressions.TypeBinaryExpression)value,Resolver); break;

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
        T value;
        var NodeType=Enum.Parse<Expressions.ExpressionType>(TypeName);
        switch(NodeType){
            case Expressions.ExpressionType.ArrayIndex:
            case Expressions.ExpressionType.Assign:
            case Expressions.ExpressionType.Coalesce:{
                var (Left,Right)=Binary.InternalDeserializeBinary(ref reader,Resolver);
                value=Expressions.Expression.MakeBinary(NodeType,Left,Right);
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
                var (Left,Right,Method)=Binary.InternalDeserializeBinaryMethod(ref reader,Resolver);
                //result=Expressions.Expression.SubtractChecked(Left,Right,Method);
                value=Expressions.Expression.MakeBinary(NodeType,Left,Right,false,Method);
                break;
            }
            case Expressions.ExpressionType.Equal:
            case Expressions.ExpressionType.GreaterThan:
            case Expressions.ExpressionType.GreaterThanOrEqual:
            case Expressions.ExpressionType.LessThan:
            case Expressions.ExpressionType.LessThanOrEqual:
            case Expressions.ExpressionType.NotEqual:{
                var (Left,Right,IsLiftedToNull,Method)=Binary.InternalDeserializeBinaryBooleanMethod(ref reader,Resolver);
                value=Expressions.Expression.MakeBinary(NodeType,Left,Right,IsLiftedToNull,Method);
                break;
            }

            case Expressions.ExpressionType.ArrayLength: {
                var Operand=Unary.Deserialize_Unary(ref reader,Resolver);
                value=Expressions.Expression.ArrayLength(Operand); break;
            }
            case Expressions.ExpressionType.Quote: {
                var Operand=Unary.Deserialize_Unary(ref reader,Resolver);
                value=Expressions.Expression.Quote(Operand); break;
            }
            case Expressions.ExpressionType.Convert:{
                var (Operand, Type, Method)=Unary.Deserialize_Unary_Type_MethodInfo(ref reader,Resolver);
                value=Expressions.Expression.Convert(Operand,Type,Method); break;
            }
            case Expressions.ExpressionType.ConvertChecked: {
                var (Operand, Type, Method)=Unary.Deserialize_Unary_Type_MethodInfo(ref reader,Resolver);
                value=Expressions.Expression.ConvertChecked(Operand,Type,Method); break;
            }
            case Expressions.ExpressionType.Decrement: {
                var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                value=Expressions.Expression.Decrement(Operand,Method); break;
            }
            case Expressions.ExpressionType.Increment: {
                var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                value=Expressions.Expression.Increment(Operand,Method); break;
            }
            case Expressions.ExpressionType.IsFalse: {
                var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                value=Expressions.Expression.IsFalse(Operand,Method); break;
            }
            case Expressions.ExpressionType.IsTrue: {
                var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                value=Expressions.Expression.IsTrue(Operand,Method); break;
            }
            case Expressions.ExpressionType.Negate: {
                var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                value=Expressions.Expression.Negate(Operand,Method); break;
            }
            case Expressions.ExpressionType.NegateChecked: {
                var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                value=Expressions.Expression.NegateChecked(Operand,Method); break;
            }
            case Expressions.ExpressionType.Not: {
                var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                value=Expressions.Expression.Not(Operand,Method); break;
            }
            case Expressions.ExpressionType.OnesComplement: {
                var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                value=Expressions.Expression.OnesComplement(Operand,Method); break;
            }
            case Expressions.ExpressionType.PostDecrementAssign: {
                var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                value=Expressions.Expression.PostDecrementAssign(Operand,Method); break;
            }
            case Expressions.ExpressionType.PostIncrementAssign: {
                var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                value=Expressions.Expression.PostIncrementAssign(Operand,Method); break;
            }
            case Expressions.ExpressionType.PreDecrementAssign: {
                var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                value=Expressions.Expression.PreDecrementAssign(Operand,Method); break;
            }
            case Expressions.ExpressionType.PreIncrementAssign: {
                var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                value=Expressions.Expression.PreIncrementAssign(Operand,Method); break;
            }
            case Expressions.ExpressionType.UnaryPlus: {
                var (Operand, Method)=Unary.Deserialize_Unary_MethodInfo(ref reader,Resolver);
                value=Expressions.Expression.UnaryPlus(Operand,Method); break;
            }
            case Expressions.ExpressionType.Throw: {
                var (Operand, Type)=Unary.Deserialize_Unary_Type(ref reader,Resolver);
                value=Expressions.Expression.Throw(Operand,Type); break;
            }
            case Expressions.ExpressionType.TypeAs: {
                var (Operand, Type)=Unary.Deserialize_Unary_Type(ref reader,Resolver);
                value=Expressions.Expression.TypeAs(Operand,Type); break;
            }
            case Expressions.ExpressionType.Unbox: {
                var (Operand, Type)=Unary.Deserialize_Unary_Type(ref reader,Resolver);
                value=Expressions.Expression.Unbox(Operand,Type); break;
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
            case Expressions.ExpressionType.NewArrayInit    :value=NewArray.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.NewArrayBounds  :value=NewArray.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.ListInit        :value=ListInit.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.MemberAccess    :value=MemberAccess.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.MemberInit      :value=MemberInit.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Block           :value=Block.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.DebugInfo       :
            case Expressions.ExpressionType.Dynamic         :
            case Expressions.ExpressionType.Default         :value=Default.Instance.Deserialize(ref reader,Resolver);break;
            //case Expressions.ExpressionType.Extension       :break;
            case Expressions.ExpressionType.Goto            :value=Goto.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Index           :value=Index.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Label           :value=Label.Instance.Deserialize(ref reader,Resolver);break;
            //case Expressions.ExpressionType.RuntimeVariables:break;
            case Expressions.ExpressionType.Loop            :value=Loop.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Switch          :value=Switch.Instance.Deserialize(ref reader,Resolver);break;
            case Expressions.ExpressionType.Try             :value=Try.Instance.Deserialize(ref reader,Resolver);break;
            default                                         :throw new NotSupportedException(TypeName);
        }
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
