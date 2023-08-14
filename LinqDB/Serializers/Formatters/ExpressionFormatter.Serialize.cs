using System;
using System.Linq.Expressions;
using MessagePack;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;

partial class ExpressionFormatter{
    public void Serialize(ref JsonWriter writer,Expression? value,IJsonFormatterResolver Resolver) {
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        writer.WriteString(value.NodeType.ToString());
        writer.WriteValueSeparator();
        switch(value.NodeType){
            case ExpressionType.Assign or ExpressionType.Coalesce or ExpressionType.ArrayIndex:{
                var Binary=(BinaryExpression)value;
                this.Serialize(ref writer,Binary.Left,Resolver);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,Binary.Right,Resolver);
                break;
            }
            case ExpressionType.Add:
            case ExpressionType.AddAssign:
            case ExpressionType.AddAssignChecked:
            case ExpressionType.AddChecked:
            case ExpressionType.And:
            case ExpressionType.AndAssign:
            case ExpressionType.AndAlso:
            case ExpressionType.Divide:
            case ExpressionType.DivideAssign:
            case ExpressionType.ExclusiveOr:
            case ExpressionType.ExclusiveOrAssign:
            case ExpressionType.LeftShift:
            case ExpressionType.LeftShiftAssign:
            case ExpressionType.Modulo:
            case ExpressionType.ModuloAssign:
            case ExpressionType.Multiply:
            case ExpressionType.MultiplyAssign:
            case ExpressionType.MultiplyAssignChecked:
            case ExpressionType.MultiplyChecked:
            case ExpressionType.Or:
            case ExpressionType.OrAssign:
            case ExpressionType.OrElse:
            case ExpressionType.Power:
            case ExpressionType.PowerAssign:
            case ExpressionType.RightShift:
            case ExpressionType.RightShiftAssign:
            case ExpressionType.Subtract:
            case ExpressionType.SubtractAssign:
            case ExpressionType.SubtractAssignChecked:
            case ExpressionType.SubtractChecked:{
                var Binary=(BinaryExpression)value;
                this.Serialize(ref writer,Binary.Left,Resolver);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,Binary.Right,Resolver);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,Binary.Method,Resolver);
                break;
            }
            case ExpressionType.Equal:
            case ExpressionType.GreaterThan:
            case ExpressionType.GreaterThanOrEqual:
            case ExpressionType.LessThan:
            case ExpressionType.LessThanOrEqual:
            case ExpressionType.NotEqual:{
                var Binary=(BinaryExpression)value;
                this.Serialize(ref writer,Binary.Left,Resolver);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,Binary.Right,Resolver);
                writer.WriteValueSeparator();
                writer.WriteBoolean(Binary.IsLiftedToNull);
                writer.WriteValueSeparator();
                this.Serialize(ref writer,Binary.Method!,Resolver);
                break;
            }

            case ExpressionType.ArrayLength        : this.Serialize_Unary(ref writer,value,Resolver);break;
            case ExpressionType.Convert            : this.Serialize_Unary_Type_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.ConvertChecked     : this.Serialize_Unary_Type_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Decrement          : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Increment          : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.IsFalse            : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.IsTrue             : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Negate             : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.NegateChecked      : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Not                : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.OnesComplement     : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.PostDecrementAssign: this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.PostIncrementAssign: this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.PreDecrementAssign : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.PreIncrementAssign : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Quote              : this.Serialize_Unary(ref writer,value,Resolver);break;
            case ExpressionType.Throw              : this.Serialize_Unary_Type(ref writer,value,Resolver);break;
            case ExpressionType.TypeAs             : this.Serialize_Unary_Type(ref writer,value,Resolver);break;
            case ExpressionType.UnaryPlus          : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Unbox              : this.Serialize_Unary_Type(ref writer,value,Resolver);break;

            case ExpressionType.TypeEqual or ExpressionType.TypeIs:this.Serialize(ref writer,(TypeBinaryExpression)value,Resolver); break;

            case ExpressionType.Conditional: this.Serialize(ref writer,(ConditionalExpression)value,Resolver);break;
            case ExpressionType.Constant: this.Serialize(ref writer,(ConstantExpression)value,Resolver);break;
            case ExpressionType.Parameter: this.Serialize(ref writer,(ParameterExpression)value,Resolver);break;
            case ExpressionType.Lambda: this.Serialize(ref writer,(LambdaExpression)value,Resolver); break;
            case ExpressionType.Call: this.Serialize(ref writer,(MethodCallExpression)value,Resolver);break;
            case ExpressionType.Invoke: this.Serialize(ref writer,(InvocationExpression)value,Resolver);break;
            case ExpressionType.New: this.Serialize(ref writer,(NewExpression)value,Resolver);break;
            case ExpressionType.NewArrayBounds or ExpressionType.NewArrayInit:this.Serialize(ref writer,(NewArrayExpression)value,Resolver); break;
            case ExpressionType.ListInit: this.Serialize(ref writer,(ListInitExpression)value,Resolver);break;
            case ExpressionType.MemberAccess: this.Serialize(ref writer,(MemberExpression)value,Resolver);break;
            case ExpressionType.MemberInit: this.Serialize(ref writer,(MemberInitExpression)value,Resolver);break;
            case ExpressionType.Block: this.Serialize(ref writer,(BlockExpression)value,Resolver);break;
            case ExpressionType.DebugInfo:
            case ExpressionType.Dynamic:
            case ExpressionType.Default: this.Serialize(ref writer,(DefaultExpression)value,Resolver);break;
            case ExpressionType.Extension:
            case ExpressionType.Goto: this.Serialize(ref writer,(GotoExpression)value,Resolver);break;
            case ExpressionType.Index:this.Serialize(ref writer,(IndexExpression)value,Resolver);break;
            case ExpressionType.Label: this.Serialize(ref writer,(LabelExpression)value,Resolver);break;
            case ExpressionType.RuntimeVariables:
                throw new ArgumentOutOfRangeException(value.NodeType.ToString());
            case ExpressionType.Loop: this.Serialize(ref writer,(LoopExpression)value,Resolver);break;
            case ExpressionType.Switch: this.Serialize(ref writer,(SwitchExpression)value,Resolver);break;
            case ExpressionType.Try: this.Serialize(ref writer,(TryExpression)value,Resolver);break;
            default:
                throw new ArgumentOutOfRangeException(value.NodeType.ToString());
        };
        writer.WriteEndArray();
    }
    public void Serialize(ref MessagePackWriter writer,Expression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        writer.Write((byte)value.NodeType);
        switch(value.NodeType){
            case ExpressionType.Assign or ExpressionType.Coalesce or ExpressionType.ArrayIndex:{
                var Binary=(BinaryExpression)value;
                this.Serialize(ref writer,Binary.Left,Resolver);
                this.Serialize(ref writer,Binary.Right,Resolver);
                break;
            }
            case ExpressionType.Add:
            case ExpressionType.AddAssign:
            case ExpressionType.AddAssignChecked:
            case ExpressionType.AddChecked:
            case ExpressionType.And:
            case ExpressionType.AndAssign:
            case ExpressionType.AndAlso:
            case ExpressionType.Divide:
            case ExpressionType.DivideAssign:
            case ExpressionType.ExclusiveOr:
            case ExpressionType.ExclusiveOrAssign:
            case ExpressionType.LeftShift:
            case ExpressionType.LeftShiftAssign:
            case ExpressionType.Modulo:
            case ExpressionType.ModuloAssign:
            case ExpressionType.Multiply:
            case ExpressionType.MultiplyAssign:
            case ExpressionType.MultiplyAssignChecked:
            case ExpressionType.MultiplyChecked:
            case ExpressionType.Or:
            case ExpressionType.OrAssign:
            case ExpressionType.OrElse:
            case ExpressionType.Power:
            case ExpressionType.PowerAssign:
            case ExpressionType.RightShift:
            case ExpressionType.RightShiftAssign:
            case ExpressionType.Subtract:
            case ExpressionType.SubtractAssign:
            case ExpressionType.SubtractAssignChecked:
            case ExpressionType.SubtractChecked:{
                var Binary=(BinaryExpression)value;
                this.Serialize(ref writer,Binary.Left,Resolver);
                this.Serialize(ref writer,Binary.Right,Resolver);
                this.Serialize(ref writer,Binary.Method,Resolver);
                break;
            }
            case ExpressionType.Equal:
            case ExpressionType.GreaterThan:
            case ExpressionType.GreaterThanOrEqual:
            case ExpressionType.LessThan:
            case ExpressionType.LessThanOrEqual:
            case ExpressionType.NotEqual:{
                var Binary=(BinaryExpression)value;
                this.Serialize(ref writer,Binary.Left,Resolver);
                this.Serialize(ref writer,Binary.Right,Resolver);
                writer.Write(Binary.IsLiftedToNull);
                this.Serialize(ref writer,Binary.Method!,Resolver);
                break;
            }

            case ExpressionType.ArrayLength        : this.Serialize_Unary(ref writer,value,Resolver);break;
            case ExpressionType.Convert            : this.Serialize_Unary_Type_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.ConvertChecked     : this.Serialize_Unary_Type_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Decrement          : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Increment          : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.IsFalse            : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.IsTrue             : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Negate             : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.NegateChecked      : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Not                : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.OnesComplement     : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.PostDecrementAssign: this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.PostIncrementAssign: this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.PreDecrementAssign : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.PreIncrementAssign : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Quote              : this.Serialize_Unary(ref writer,value,Resolver);break;
            case ExpressionType.Throw              : this.Serialize_Unary_Type(ref writer,value,Resolver);break;
            case ExpressionType.TypeAs             : this.Serialize_Unary_Type(ref writer,value,Resolver);break;
            case ExpressionType.UnaryPlus          : this.Serialize_Unary_MethodInfo(ref writer,value,Resolver);break;
            case ExpressionType.Unbox              : this.Serialize_Unary_Type(ref writer,value,Resolver);break;

            case ExpressionType.TypeEqual or ExpressionType.TypeIs:this.Serialize(ref writer,(TypeBinaryExpression)value,Resolver); break;


            case ExpressionType.Conditional: this.Serialize(ref writer,(ConditionalExpression)value,Resolver);break;
            case ExpressionType.Constant: this.Serialize(ref writer,(ConstantExpression)value,Resolver);break;
            case ExpressionType.Parameter: this.Serialize(ref writer,(ParameterExpression)value,Resolver);break;
            case ExpressionType.Lambda: this.Serialize(ref writer,(LambdaExpression)value,Resolver); break;
            case ExpressionType.Call: this.Serialize(ref writer,(MethodCallExpression)value,Resolver);break;
            case ExpressionType.Invoke: this.Serialize(ref writer,(InvocationExpression)value,Resolver);break;
            case ExpressionType.New: this.Serialize(ref writer,(NewExpression)value,Resolver);break;
            case ExpressionType.NewArrayInit or ExpressionType.NewArrayBounds:this.Serialize(ref writer,(NewArrayExpression)value,Resolver);break;//Serialize_T(ref writer,((NewArrayExpression)value).Expressions,Resolver);break;
            case ExpressionType.ListInit: this.Serialize(ref writer,(ListInitExpression)value,Resolver);break;
            case ExpressionType.MemberAccess: this.Serialize(ref writer,(MemberExpression)value,Resolver);break;
            case ExpressionType.MemberInit: this.Serialize(ref writer,(MemberInitExpression)value,Resolver);break;
            case ExpressionType.Block: this.Serialize(ref writer,(BlockExpression)value,Resolver);break;
            case ExpressionType.DebugInfo:
            case ExpressionType.Dynamic:
            case ExpressionType.Default: this.Serialize(ref writer,(DefaultExpression)value,Resolver);break;
            case ExpressionType.Extension:
            case ExpressionType.Goto: this.Serialize(ref writer,(GotoExpression)value,Resolver);break;
            case ExpressionType.Index:this.Serialize(ref writer,(IndexExpression)value,Resolver);break;
            case ExpressionType.Label: this.Serialize(ref writer,(LabelExpression)value,Resolver);break;
            case ExpressionType.RuntimeVariables:
                throw new ArgumentOutOfRangeException(value.NodeType.ToString());
            case ExpressionType.Loop: this.Serialize(ref writer,(LoopExpression)value,Resolver);break;
            case ExpressionType.Switch: this.Serialize(ref writer,(SwitchExpression)value,Resolver);break;
            case ExpressionType.Try: this.Serialize(ref writer,(TryExpression)value,Resolver);break;
            default:
                throw new ArgumentOutOfRangeException(value.NodeType.ToString());
        };
    }
}
//220 2022/06/07
