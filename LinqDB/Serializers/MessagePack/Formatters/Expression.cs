using System;
using System.Collections.Generic;
using System.Diagnostics;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;

namespace LinqDB.Serializers.MessagePack.Formatters;
using C=MessagePackCustomSerializer;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.Expression;
public class Expression:IMessagePackFormatter<Expressions.Expression>{
    public static readonly Expression Instance=new();
    public void Serialize(ref MessagePackWriter writer,Expressions.Expression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        writer.WriteArrayHeader(2);
        writer.WriteNodeType(value.NodeType);
        switch(value.NodeType){
            case Expressions.ExpressionType.Assign:
            case Expressions.ExpressionType.Coalesce:
            case Expressions.ExpressionType.ArrayIndex:{
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
                this.Serialize(ref writer,Binary.Left,Resolver);
                this.Serialize(ref writer,Binary.Right,Resolver);
                writer.Write(Binary.IsLiftedToNull);
                Method.Instance.Serialize(ref writer,Binary.Method!,Resolver);
                break;
            }

            case Expressions.ExpressionType.ArrayLength        : Unary.Serialize_Unary(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Quote              : Unary.Serialize_Unary(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Throw              : Unary.Serialize_Unary_Type(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.TypeAs             : Unary.Serialize_Unary_Type(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Unbox              : Unary.Serialize_Unary_Type(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Convert            : Unary.Serialize_Unary_Type_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.ConvertChecked     : Unary.Serialize_Unary_Type_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Decrement          : Unary.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Increment          : Unary.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.IsFalse            : Unary.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.IsTrue             : Unary.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Negate             : Unary.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.NegateChecked      : Unary.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.Not                : Unary.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.OnesComplement     : Unary.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.PostDecrementAssign: Unary.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.PostIncrementAssign: Unary.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.PreDecrementAssign : Unary.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.PreIncrementAssign : Unary.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.UnaryPlus          : Unary.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case Expressions.ExpressionType.TypeEqual          :
            case Expressions.ExpressionType.TypeIs             : TypeBinary.Instance.Serialize(ref writer,(Expressions.TypeBinaryExpression)value,Resolver); break;
            case Expressions.ExpressionType.Conditional        :Conditional.Instance.Serialize(ref writer,(Expressions.ConditionalExpression)value,Resolver);break;
            case Expressions.ExpressionType.Constant           :Constant.Instance.Serialize(ref writer,(Expressions.ConstantExpression)value,Resolver);break;
            case Expressions.ExpressionType.Parameter          :Parameter.Instance.Serialize(ref writer,(Expressions.ParameterExpression)value,Resolver);break;
            case Expressions.ExpressionType.Lambda             :Lambda.Instance.Serialize(ref writer,(Expressions.LambdaExpression)value,Resolver); break;
            case Expressions.ExpressionType.Call               :MethodCall.Instance.Serialize(ref writer,(Expressions.MethodCallExpression)value,Resolver);break;
            case Expressions.ExpressionType.Invoke             :Invocation.Instance.Serialize(ref writer,(Expressions.InvocationExpression)value,Resolver);break;
            case Expressions.ExpressionType.New                :New.Instance.Serialize(ref writer,(Expressions.NewExpression)value,Resolver);break;
            case Expressions.ExpressionType.NewArrayInit       :
            case Expressions.ExpressionType.NewArrayBounds     :NewArray.Instance.Serialize(ref writer,(Expressions.NewArrayExpression)value,Resolver);break;//this.Instance.Serialize(ref writer,(Expressions.Expressions.(NewArrayExpression)value,Resolver).Expressions);break;
            case Expressions.ExpressionType.ListInit           :ListInit.Instance.Serialize(ref writer,(Expressions.ListInitExpression)value,Resolver);break;
            case Expressions.ExpressionType.MemberAccess       :MemberAccess.Instance.Serialize(ref writer,(Expressions.MemberExpression)value,Resolver);break;
            case Expressions.ExpressionType.MemberInit         :MemberInit.Instance.Serialize(ref writer,(Expressions.MemberInitExpression)value,Resolver);break;
            case Expressions.ExpressionType.Block              :Block.Instance.Serialize(ref writer,(Expressions.BlockExpression)value,Resolver);break;
            //case Expressions.ExpressionType.DebugInfo          :
            //case Expressions.ExpressionType.Dynamic            :
            case Expressions.ExpressionType.Default            :Default.Instance.Serialize(ref writer,(Expressions.DefaultExpression)value,Resolver);break;
            //case Expressions.ExpressionType.Extension          :
            case Expressions.ExpressionType.Goto               :Goto.Instance.Serialize(ref writer,(Expressions.GotoExpression)value,Resolver);break;
            case Expressions.ExpressionType.Index              :Index.Instance.Serialize(ref writer,(Expressions.IndexExpression)value,Resolver);break;
            case Expressions.ExpressionType.Label              :Label.Instance.Serialize(ref writer,(Expressions.LabelExpression)value,Resolver);break;
            //case Expressions.ExpressionType.RuntimeVariables   :
            case Expressions.ExpressionType.Loop               :Loop.Instance.Serialize(ref writer,(Expressions.LoopExpression)value,Resolver);break;
            case Expressions.ExpressionType.Switch             :Switch.Instance.Serialize(ref writer,(Expressions.SwitchExpression)value,Resolver);break;
            case Expressions.ExpressionType.Try                :Try.Instance.Serialize(ref writer,(Expressions.TryExpression)value,Resolver);break;
            default:throw new ArgumentOutOfRangeException(value.NodeType.ToString());
        }
    }
    public Expressions.Expression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil())return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==2);
        var NodeType=reader.ReadNodeType();
        switch(NodeType){
            case Expressions.ExpressionType.ArrayIndex:
            case Expressions.ExpressionType.Assign:
            case Expressions.ExpressionType.Coalesce:{
                var (Left,Right)=Binary.Deserialize_Binary(ref reader,Resolver);
                return Expressions.Expression.MakeBinary(NodeType,Left,Right);
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
                //return Expressions.Expression.SubtractChecked(Left,Right,Method);
                return Expressions.Expression.MakeBinary(NodeType,Left,Right,false,Method);
            }
            case Expressions.ExpressionType.Equal:
            case Expressions.ExpressionType.GreaterThan:
            case Expressions.ExpressionType.GreaterThanOrEqual:
            case Expressions.ExpressionType.LessThan:
            case Expressions.ExpressionType.LessThanOrEqual:
            case Expressions.ExpressionType.NotEqual:{
                var (Left,Right,IsLiftedToNull,Method)=Binary.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.MakeBinary(NodeType,Left,Right,IsLiftedToNull,Method);
            }

            case Expressions.ExpressionType.ArrayLength        :{
                var Operand= Unary.Deserialize_Unary(ref reader,Resolver);
                return Expressions.Expression.ArrayLength(Operand);
            }
            case Expressions.ExpressionType.Convert            :
            case Expressions.ExpressionType.ConvertChecked     :{
                var(Operand,Type,Method)=Unary.Deserialize_Unary_Type_MethodInfo(ref reader,Resolver);
                return Expressions.Expression.ConvertChecked(Operand,Type,Method);
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
                return Expressions.Expression.PreIncrementAssign(Operand,Method);
            }
            case Expressions.ExpressionType.Quote:
            case Expressions.ExpressionType.Throw              :
            case Expressions.ExpressionType.TypeAs             :
            case Expressions.ExpressionType.UnaryPlus:
            case Expressions.ExpressionType.Unbox              :{
                var (Operand,Type)=Unary.Deserialize_Unary_Type(ref reader,Resolver);
                return Expressions.Expression.MakeUnary(NodeType,Operand,Type);
            }

            case Expressions.ExpressionType.TypeEqual       :
            case Expressions.ExpressionType.TypeIs          :return TypeBinary.Instance.Deserialize(ref reader,Resolver);

            case Expressions.ExpressionType.Conditional     :return Conditional.Instance.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.Constant        :return Constant.Instance.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.Parameter       :return Parameter.Instance.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.Lambda          :return Lambda.Instance.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.Call            :return MethodCall.Instance.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.Invoke          :return Invocation.Instance.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.New             :return New.Instance.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.NewArrayInit    :return NewArray.Instance.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.NewArrayBounds  :return NewArray.Instance.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.ListInit        :return ListInit.Instance.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.MemberAccess    :return MemberAccess.Instance.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.MemberInit      :return MemberInit.Instance.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.Block           :return Block.Instance.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.DebugInfo       :
            case Expressions.ExpressionType.Dynamic         :
            case Expressions.ExpressionType.Default         :return Default.Instance.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.Extension       :break;
            case Expressions.ExpressionType.Goto            :return Goto.Instance.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.Index           :return Index.Instance.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.Label           :return Label.Instance.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.RuntimeVariables:break;
            case Expressions.ExpressionType.Loop            :return Loop.Instance.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.Switch          :return Switch.Instance.Deserialize(ref reader,Resolver);
            case Expressions.ExpressionType.Try             :return Try.Instance.Deserialize(ref reader,Resolver);
        }
        //switch(NodeType){
        //    case Expressions.ExpressionType.Assign:{
        //        var (Left,Right)=Binary.Deserialize_Binary(ref reader,Resolver);
        //        return Expressions.Expression.Assign(Left,Right);
        //    }
        //    case Expressions.ExpressionType.Coalesce:{
        //        var (Left,Right)=this.Deserialize_Binary(ref reader,Resolver);
        //        return Expressions.Expression.Coalesce(Left,Right);
        //    }
        //    case Expressions.ExpressionType.Add:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.Add(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.AddAssign:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.AddAssign(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.AddAssignChecked:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.AddAssignChecked(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.AddChecked:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.AddChecked(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.And:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.And(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.AndAssign:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.AndAssign(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.AndAlso:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.AndAlso(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.Divide:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.Divide(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.DivideAssign:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.DivideAssign(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.ExclusiveOr:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.ExclusiveOr(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.ExclusiveOrAssign:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.ExclusiveOrAssign(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.LeftShift:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.LeftShift(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.LeftShiftAssign:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.LeftShiftAssign(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.Modulo:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.Modulo(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.ModuloAssign:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.ModuloAssign(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.Multiply:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.Multiply(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.MultiplyAssign:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.MultiplyAssign(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.MultiplyAssignChecked:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.MultiplyAssignChecked(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.MultiplyChecked:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.MultiplyChecked(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.Or:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.Or(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.OrAssign:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.OrAssign(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.OrElse:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.OrElse(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.Power:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.Power(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.PowerAssign:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.PowerAssign(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.RightShift:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.RightShift(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.RightShiftAssign:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.RightShiftAssign(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.Subtract:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.Subtract(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.SubtractAssign:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.SubtractAssign(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.SubtractAssignChecked:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.SubtractAssignChecked(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.SubtractChecked:{
        //        var (Left,Right,Method)=this.Deserialize_Binary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.SubtractChecked(Left,Right,Method);
        //    }
        //    case Expressions.ExpressionType.Equal:{
        //        var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.Equal(Left,Right,IsLiftedToNull,Method);
        //    }
        //    case Expressions.ExpressionType.GreaterThan:{
        //        var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.GreaterThan(Left,Right,IsLiftedToNull,Method);
        //    }
        //    case Expressions.ExpressionType.GreaterThanOrEqual:{
        //        var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.GreaterThanOrEqual(Left,Right,IsLiftedToNull,Method);
        //    }
        //    case Expressions.ExpressionType.LessThan:{
        //        var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.LessThan(Left,Right,IsLiftedToNull,Method);
        //    }
        //    case Expressions.ExpressionType.LessThanOrEqual:{
        //        var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.LessThanOrEqual(Left,Right,IsLiftedToNull,Method);
        //    }
        //    case Expressions.ExpressionType.NotEqual:{
        //        var (Left,Right,IsLiftedToNull,Method)=this.Deserialize_Binary_bool_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.NotEqual(Left,Right,IsLiftedToNull,Method);
        //    }
        //    case Expressions.ExpressionType.ArrayIndex:{
        //        var (array,index)=this.Deserialize_Binary(ref reader,Resolver);
        //        return Expressions.Expression.ArrayIndex(array,index);
        //    }

        //    case Expressions.ExpressionType.ArrayLength        :{
        //        var Operand= this.Deserialize_Unary(ref reader,Resolver);
        //        return Expressions.Expression.ArrayLength(Operand);
        //    }
        //    case Expressions.ExpressionType.Convert            :{
        //        var(Operand,Type,Method)=this.Deserialize_Unary_Type_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.Convert(Operand,Type,Method);
        //    }
        //    case Expressions.ExpressionType.ConvertChecked     :{
        //        var(Operand,Type,Method)=this.Deserialize_Unary_Type_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.ConvertChecked(Operand,Type,Method);
        //    }
        //    case Expressions.ExpressionType.Decrement          :{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.Decrement(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.Increment          :{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.Increment(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.IsFalse            :{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.IsFalse(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.IsTrue             :{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.IsTrue(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.Negate             :{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.Negate(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.NegateChecked      :{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.NegateChecked(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.Not                :{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.Not(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.OnesComplement     :{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.OnesComplement(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.PostDecrementAssign:{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.PostDecrementAssign(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.PostIncrementAssign:{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.PostIncrementAssign(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.PreDecrementAssign :{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.PreDecrementAssign(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.PreIncrementAssign :{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.PreIncrementAssign(Operand,Method);
        //}
        //    case Expressions.ExpressionType.Quote:{
        //        var result=Expressions.Expression.Quote(this.Deserialize_Unary(ref reader,Resolver));
        //        return result;
        //    }
        //    case Expressions.ExpressionType.Throw              :{
        //        var (Operand,Type)=this.Deserialize_Unary_Type(ref reader,Resolver);
        //        return Expressions.Expression.Throw(Operand,Type);
        //    }
        //    case Expressions.ExpressionType.TypeAs             :{
        //        var (Operand,Type)=this.Deserialize_Unary_Type(ref reader,Resolver);
        //        return Expressions.Expression.TypeAs(Operand,Type);
        //    }
        //    case Expressions.ExpressionType.UnaryPlus:{
        //        var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader,Resolver);
        //        return Expressions.Expression.UnaryPlus(Operand,Method);
        //    }
        //    case Expressions.ExpressionType.Unbox              :{
        //        var (Operand,Type)=this.Deserialize_Unary_Type(ref reader,Resolver);
        //        return Expressions.Expression.Unbox(Operand,Type);
        //    }

        //    case Expressions.ExpressionType.TypeEqual or Expressions.ExpressionType.TypeIs:return this.TypeBinary.Deserialize(ref reader,Resolver);

        //    case Expressions.ExpressionType.Conditional        :return this.Conditional.Deserialize(ref reader,Resolver);
        //    case Expressions.ExpressionType.Constant:return this.Constant.Deserialize(ref reader,Resolver);
        //    case Expressions.ExpressionType.Parameter:return this.MSParameter.Deserialize(ref reader,Resolver);
        //    case Expressions.ExpressionType.Lambda:return this.MSLambda.Deserialize(ref reader,Resolver);
        //    case Expressions.ExpressionType.Call               :return this.MSMethodCall.Deserialize(ref reader,Resolver);
        //    case Expressions.ExpressionType.Invoke             :return this.MSInvocation.Deserialize(ref reader,Resolver);
        //    case Expressions.ExpressionType.New:return this.MSNew.Deserialize(ref reader,Resolver);
        //    case Expressions.ExpressionType.NewArrayInit:return this.MSNewArray.Deserialize(ref reader,Resolver);
        //    case Expressions.ExpressionType.NewArrayBounds:return this.MSNewArray.Deserialize(ref reader,Resolver);
        //    case Expressions.ExpressionType.ListInit:return this.MSListInit.Deserialize(ref reader,Resolver);
        //    case Expressions.ExpressionType.MemberAccess:return this.MSMember.Deserialize(ref reader,Resolver);
        //    case Expressions.ExpressionType.MemberInit:return this.MSMemberInit.Deserialize(ref reader,Resolver);
        //    case Expressions.ExpressionType.Block:return this.Block.Deserialize(ref reader,Resolver);
        //    case Expressions.ExpressionType.DebugInfo:
        //    case Expressions.ExpressionType.Dynamic:
        //    case Expressions.ExpressionType.Default:return this.Default.Deserialize(ref reader,Resolver);
        //    case Expressions.ExpressionType.Extension:
        //        break;
        //    case Expressions.ExpressionType.Goto:return this.Goto.Deserialize(ref reader,Resolver);
        //    case Expressions.ExpressionType.Index:return this.Index.Deserialize(ref reader,Resolver);
        //    case Expressions.ExpressionType.Label:return this.LabelExpression.Deserialize(ref reader,Resolver);
        //    case Expressions.ExpressionType.RuntimeVariables:
        //        break;
        //    case Expressions.ExpressionType.Loop:return this.MSLoop.Deserialize(ref reader,Resolver);
        //    case Expressions.ExpressionType.Switch:return this.MSSwitch.Deserialize(ref reader,Resolver);
        //    case Expressions.ExpressionType.Try:return this.MSTry.Deserialize(ref reader,Resolver);
        //}
        throw new NotSupportedException(NodeType.ToString());
        //return this.Deserialize(ref reader,options);
    }
}
