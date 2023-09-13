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
                var (Left, Right)=Binary.InternalDeserialize(ref reader,Resolver);
                value=T.Assign(Left,Right);break;
            }
            case Expressions.ExpressionType.Coalesce: {
                var (Left, Right)=Binary.InternalDeserialize(ref reader,Resolver);
                value=T.Coalesce(Left,Right);break;
            }
            case Expressions.ExpressionType.Add: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.Add(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.AddAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.AddAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.AddAssignChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.AddAssignChecked(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.AddChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.AddChecked(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.And: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.And(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.AndAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.AndAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.AndAlso: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.AndAlso(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.Divide: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.Divide(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.DivideAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.DivideAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.ExclusiveOr: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.ExclusiveOr(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.ExclusiveOrAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.ExclusiveOrAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.LeftShift: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.LeftShift(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.LeftShiftAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.LeftShiftAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.Modulo: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.Modulo(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.ModuloAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.ModuloAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.Multiply: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.Multiply(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.MultiplyAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.MultiplyAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.MultiplyAssignChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.MultiplyAssignChecked(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.MultiplyChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.MultiplyChecked(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.Or: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.Or(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.OrAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.OrAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.OrElse: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.OrElse(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.Power: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.Power(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.PowerAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.PowerAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.RightShift: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.RightShift(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.RightShiftAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.RightShiftAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.Subtract: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.Subtract(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.SubtractAssign: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.SubtractAssign(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.SubtractAssignChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.SubtractAssignChecked(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.SubtractChecked: {
                var (Left, Right, Method)=Binary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.SubtractChecked(Left,Right,Method);break;
            }
            case Expressions.ExpressionType.Equal: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=T.Equal(Left,Right,IsLiftedToNull,Method);break;
            }
            case Expressions.ExpressionType.GreaterThan: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=T.GreaterThan(Left,Right,IsLiftedToNull,Method);break;
            }
            case Expressions.ExpressionType.GreaterThanOrEqual: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=T.GreaterThanOrEqual(Left,Right,IsLiftedToNull,Method);break;
            }
            case Expressions.ExpressionType.LessThan: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=T.LessThan(Left,Right,IsLiftedToNull,Method);break;
            }
            case Expressions.ExpressionType.LessThanOrEqual: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=T.LessThanOrEqual(Left,Right,IsLiftedToNull,Method);break;
            }
            case Expressions.ExpressionType.NotEqual: {
                var (Left, Right, IsLiftedToNull, Method)=Binary.InternalDeserializeBooleanMethod(ref reader,Resolver);
                value=T.NotEqual(Left,Right,IsLiftedToNull,Method);break;
            }

            case Expressions.ExpressionType.ArrayLength: {
                var Operand=Unary.InternalDeserialize(ref reader,Resolver);
                value=T.ArrayLength(Operand); break;
            }
            case Expressions.ExpressionType.Quote: {
                var Operand=Unary.InternalDeserialize(ref reader,Resolver);
                value=T.Quote(Operand); break;
            }
            case Expressions.ExpressionType.Convert:{
                var (Operand, Type, Method)=Unary.InternalDeserializeTypeMethod(ref reader,Resolver);
                value=T.Convert(Operand,Type,Method); break;
            }
            case Expressions.ExpressionType.ConvertChecked: {
                var (Operand, Type, Method)=Unary.InternalDeserializeTypeMethod(ref reader,Resolver);
                value=T.ConvertChecked(Operand,Type,Method); break;
            }
            case Expressions.ExpressionType.Decrement: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.Decrement(Operand,Method); break;
            }
            case Expressions.ExpressionType.Increment: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.Increment(Operand,Method); break;
            }
            case Expressions.ExpressionType.IsFalse: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.IsFalse(Operand,Method); break;
            }
            case Expressions.ExpressionType.IsTrue: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.IsTrue(Operand,Method); break;
            }
            case Expressions.ExpressionType.Negate: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.Negate(Operand,Method); break;
            }
            case Expressions.ExpressionType.NegateChecked: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.NegateChecked(Operand,Method); break;
            }
            case Expressions.ExpressionType.Not: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.Not(Operand,Method); break;
            }
            case Expressions.ExpressionType.OnesComplement: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.OnesComplement(Operand,Method); break;
            }
            case Expressions.ExpressionType.PostDecrementAssign: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.PostDecrementAssign(Operand,Method); break;
            }
            case Expressions.ExpressionType.PostIncrementAssign: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.PostIncrementAssign(Operand,Method); break;
            }
            case Expressions.ExpressionType.PreDecrementAssign: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.PreDecrementAssign(Operand,Method); break;
            }
            case Expressions.ExpressionType.PreIncrementAssign: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.PreIncrementAssign(Operand,Method); break;
            }
            case Expressions.ExpressionType.UnaryPlus: {
                var (Operand, Method)=Unary.InternalDeserializeMethod(ref reader,Resolver);
                value=T.UnaryPlus(Operand,Method); break;
            }
            case Expressions.ExpressionType.Throw: {
                var (Operand, Type)=Unary.InternalDeserializeType(ref reader,Resolver);
                value=T.Throw(Operand,Type); break;
            }
            case Expressions.ExpressionType.TypeAs: {
                var (Operand, Type)=Unary.InternalDeserializeType(ref reader,Resolver);
                value=T.TypeAs(Operand,Type); break;
            }
            case Expressions.ExpressionType.Unbox: {
                var (Operand, Type)=Unary.InternalDeserializeType(ref reader,Resolver);
                value=T.Unbox(Operand,Type); break;
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
