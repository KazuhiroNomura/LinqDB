using System;
using System.Buffers;
using MemoryPack;
using Expressions = System.Linq.Expressions;
using System.Linq.Expressions;
// ReSharper disable InconsistentNaming
namespace LinqDB.Serializers.MemoryPack.Formatters;
public class Expression:MemoryPackFormatter<Expressions.Expression>{
    //public static readonly Formatter Instance=new();
    //private static readonly Dictionary<int,MemberInfo>Members=new();
    //private static readonly Dictionary<int,Type>Types=new();
    //private static readonly Dictionary<int,FieldInfo>Fields=new();
    //private static readonly Dictionary<int,PropertyInfo>Properties=new();
    //private static readonly Dictionary<int,MethodInfo>Methods=new();
    //private static readonly Dictionary<int,EventInfo>Events=new();
    //static Expression(){
    //    foreach(var Assembly in AppDomain.CurrentDomain.GetAssemblies()){
    //        foreach(var Type in Assembly.GetTypes()){
    //            foreach(var Member in Type.GetMembers()){
    //                var MetadataToken=Member.MetadataToken;
    //                Members.Add(MetadataToken,Member);
    //                if     (Member.MemberType==MemberTypes.TypeInfo)Types.Add(MetadataToken,(Type)Member);
    //                else if(Member.MemberType==MemberTypes.Field)Fields.Add(MetadataToken,(FieldInfo)Member);
    //                else if(Member.MemberType==MemberTypes.Property)Properties.Add(MetadataToken,(PropertyInfo)Member);
    //                else if(Member.MemberType==MemberTypes.Method)Methods.Add(MetadataToken,(MethodInfo)Member);
    //                else if(Member.MemberType==MemberTypes.Event)Events.Add(MetadataToken,(EventInfo)Member);
    //            }
    //        }
    //    }
    //}
    //private readonly global::MemoryPack.Formatters.TypeFormatter TypeFormatter=new();
    //private static readonly ReadOnlyCollectionFormatter<Expressions.Expression>SerializeExpressionsFormatter=new();
    //private static readonly ArrayFormatter<Expression>DeserializeExpressionsFormatter=new();
    //public void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,ReadOnlyCollection<Expressions.Expression> value)where TBufferWriter:IBufferWriter<byte> =>writer.WriteArray(value);
    // private void Serialize<TBufferWriter,T>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>where where T:Expression{
    //     this.Serialize(ref writer,ref value);
    // }
    // private T DeserializeExpression<T>(ref MemoryPackReader reader)where T:Expression{
    //     T? value=default;
    //     this.Deserialize(ref reader,ref value);
    //     return value!;
    // }
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
            MemoryPackCustomSerializer.WriteBoolean(ref writer,false);
            return;
        }
        MemoryPackCustomSerializer.WriteBoolean(ref writer,true);
        writer.WriteVarInt((byte)value.NodeType);
        switch(value.NodeType){
            case ExpressionType.Assign or ExpressionType.Coalesce or ExpressionType.ArrayIndex:{
                var Binary=(BinaryExpression)value;
                this.Serialize(ref writer,Binary.Left);
                this.Serialize(ref writer,Binary.Right);
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
                this.Serialize(ref writer,Binary.Left);
                this.Serialize(ref writer,Binary.Right);
                MemoryPackCustomSerializer.Method.SerializeNullable(ref writer,Binary.Method);
                break;
            }
            case ExpressionType.Equal:
            case ExpressionType.GreaterThan:
            case ExpressionType.GreaterThanOrEqual:
            case ExpressionType.LessThan:
            case ExpressionType.LessThanOrEqual:
            case ExpressionType.NotEqual:{
                var Binary=(BinaryExpression)value;
                this.Serialize(ref writer,Binary.Left);
                this.Serialize(ref writer,Binary.Right);
                writer.WriteVarInt((byte)(Binary.IsLiftedToNull ? 1 : 0));
                MemoryPackCustomSerializer.Method.SerializeNullable(ref writer,Binary.Method!);
                break;
            }

            case ExpressionType.ArrayLength        :MemoryPackCustomSerializer.Unary.Serialize_Unary(ref writer,value);break;
            case ExpressionType.Convert            :MemoryPackCustomSerializer.Unary.Serialize_Unary_Type_MethodInfo(ref writer,value);break;
            case ExpressionType.ConvertChecked     :MemoryPackCustomSerializer.Unary.Serialize_Unary_Type_MethodInfo(ref writer,value);break;
            case ExpressionType.Decrement          :MemoryPackCustomSerializer.Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case ExpressionType.Increment          :MemoryPackCustomSerializer.Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case ExpressionType.IsFalse            :MemoryPackCustomSerializer.Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case ExpressionType.IsTrue             :MemoryPackCustomSerializer.Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case ExpressionType.Negate             :MemoryPackCustomSerializer.Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case ExpressionType.NegateChecked      :MemoryPackCustomSerializer.Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case ExpressionType.Not                :MemoryPackCustomSerializer.Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case ExpressionType.OnesComplement     :MemoryPackCustomSerializer.Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case ExpressionType.PostDecrementAssign:MemoryPackCustomSerializer.Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case ExpressionType.PostIncrementAssign:MemoryPackCustomSerializer.Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case ExpressionType.PreDecrementAssign :MemoryPackCustomSerializer.Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case ExpressionType.PreIncrementAssign :MemoryPackCustomSerializer.Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case ExpressionType.Quote              :MemoryPackCustomSerializer.Unary.Serialize_Unary(ref writer,value);break;
            case ExpressionType.Throw              :MemoryPackCustomSerializer.Unary.Serialize_Unary_Type(ref writer,value);break;
            case ExpressionType.TypeAs             :MemoryPackCustomSerializer.Unary.Serialize_Unary_Type(ref writer,value);break;
            case ExpressionType.UnaryPlus          :MemoryPackCustomSerializer.Unary.Serialize_Unary_MethodInfo(ref writer,value);break;
            case ExpressionType.Unbox              :MemoryPackCustomSerializer.Unary.Serialize_Unary_Type(ref writer,value);break;
            case ExpressionType.TypeEqual          :
            case ExpressionType.TypeIs             :MemoryPackCustomSerializer.TypeBinary.Serialize(ref writer,(TypeBinaryExpression)value); break;
            case ExpressionType.Conditional        :MemoryPackCustomSerializer.Conditional.Serialize(ref writer,(ConditionalExpression)value);break;
            case ExpressionType.Constant           :MemoryPackCustomSerializer.Constant.Serialize(ref writer,(ConstantExpression)value);break;
            case ExpressionType.Parameter          :MemoryPackCustomSerializer.Parameter.Serialize(ref writer,(ParameterExpression)value);break;
            case ExpressionType.Lambda             :MemoryPackCustomSerializer.Lambda.Serialize(ref writer,(LambdaExpression)value); break;
            case ExpressionType.Call               :MemoryPackCustomSerializer.MethodCall.Serialize(ref writer,(MethodCallExpression)value);break;
            case ExpressionType.Invoke             :MemoryPackCustomSerializer.Invocation.Serialize(ref writer,(InvocationExpression)value);break;
            case ExpressionType.New                :MemoryPackCustomSerializer.New.Serialize(ref writer,(NewExpression)value);break;
            case ExpressionType.NewArrayInit       :
            case ExpressionType.NewArrayBounds     :MemoryPackCustomSerializer.NewArray.Serialize(ref writer,(NewArrayExpression)value);break;//this.Serialize(ref writer,(Expressions.(NewArrayExpression)value).Expressions);break;
            case ExpressionType.ListInit           :MemoryPackCustomSerializer.ListInit.Serialize(ref writer,(ListInitExpression)value);break;
            case ExpressionType.MemberAccess       :MemoryPackCustomSerializer.MemberAccess.Serialize(ref writer,(MemberExpression)value);break;
            case ExpressionType.MemberInit         :MemoryPackCustomSerializer.MemberInit.Serialize(ref writer,(MemberInitExpression)value);break;
            case ExpressionType.Block              :MemoryPackCustomSerializer.Block.Serialize(ref writer,(BlockExpression)value);break;
            case ExpressionType.DebugInfo          :
            case ExpressionType.Dynamic            :
            case ExpressionType.Default            :MemoryPackCustomSerializer.Default.Serialize(ref writer,(DefaultExpression)value);break;
            case ExpressionType.Extension          :
            case ExpressionType.Goto               :MemoryPackCustomSerializer.Goto.Serialize(ref writer,(GotoExpression)value);break;
            case ExpressionType.Index              :MemoryPackCustomSerializer.Index.Serialize(ref writer,(IndexExpression)value);break;
            case ExpressionType.Label              :MemoryPackCustomSerializer.Label.Serialize(ref writer,(LabelExpression)value);break;
            case ExpressionType.RuntimeVariables   :throw new ArgumentOutOfRangeException(value.NodeType.ToString());
            case ExpressionType.Loop               :MemoryPackCustomSerializer.Loop.Serialize(ref writer,(LoopExpression)value);break;
            case ExpressionType.Switch             :MemoryPackCustomSerializer.Switch.Serialize(ref writer,(SwitchExpression)value);break;
            case ExpressionType.Try                :MemoryPackCustomSerializer.Try.Serialize(ref writer,(TryExpression)value);break;
            default:throw new ArgumentOutOfRangeException(value.NodeType.ToString());
        }
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.Expression? value){
        if(!MemoryPackCustomSerializer.ReadBoolean(ref reader)) return;
        //if(reader.TryReadNil())return;
        var NodeType=(ExpressionType)reader.ReadVarIntByte();
        switch(NodeType){
            case ExpressionType.Assign: {
                var (Left, Right)=MemoryPackCustomSerializer.Binary.Deserialize_Binary(ref reader);
                value=Expressions.Expression.Assign(Left,Right); break;
            }
            case ExpressionType.Coalesce: {
                var (Left, Right)=MemoryPackCustomSerializer.Binary.Deserialize_Binary(ref reader);
                value=Expressions.Expression.Coalesce(Left,Right); break;
            }
            case ExpressionType.Add: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.Add(Left,Right,Method); break;
            }
            case ExpressionType.AddAssign: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.AddAssign(Left,Right,Method); break;
            }
            case ExpressionType.AddAssignChecked: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.AddAssignChecked(Left,Right,Method); break;
            }
            case ExpressionType.AddChecked: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.AddChecked(Left,Right,Method); break;
            }
            case ExpressionType.And: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.And(Left,Right,Method); break;
            }
            case ExpressionType.AndAssign: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.AndAssign(Left,Right,Method); break;
            }
            case ExpressionType.AndAlso: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.AndAlso(Left,Right,Method); break;
            }
            case ExpressionType.Divide: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.Divide(Left,Right,Method); break;
            }
            case ExpressionType.DivideAssign: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.DivideAssign(Left,Right,Method); break;
            }
            case ExpressionType.ExclusiveOr: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.ExclusiveOr(Left,Right,Method); break;
            }
            case ExpressionType.ExclusiveOrAssign: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.ExclusiveOrAssign(Left,Right,Method); break;
            }
            case ExpressionType.LeftShift: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.LeftShift(Left,Right,Method); break;
            }
            case ExpressionType.LeftShiftAssign: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.LeftShiftAssign(Left,Right,Method); break;
            }
            case ExpressionType.Modulo: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.Modulo(Left,Right,Method); break;
            }
            case ExpressionType.ModuloAssign: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.ModuloAssign(Left,Right,Method); break;
            }
            case ExpressionType.Multiply: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.Multiply(Left,Right,Method); break;
            }
            case ExpressionType.MultiplyAssign: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.MultiplyAssign(Left,Right,Method); break;
            }
            case ExpressionType.MultiplyAssignChecked: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.MultiplyAssignChecked(Left,Right,Method); break;
            }
            case ExpressionType.MultiplyChecked: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.MultiplyChecked(Left,Right,Method); break;
            }
            case ExpressionType.Or: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.Or(Left,Right,Method); break;
            }
            case ExpressionType.OrAssign: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.OrAssign(Left,Right,Method); break;
            }
            case ExpressionType.OrElse: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.OrElse(Left,Right,Method); break;
            }
            case ExpressionType.Power: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.Power(Left,Right,Method); break;
            }
            case ExpressionType.PowerAssign: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.PowerAssign(Left,Right,Method); break;
            }
            case ExpressionType.RightShift: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.RightShift(Left,Right,Method); break;
            }
            case ExpressionType.RightShiftAssign: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.RightShiftAssign(Left,Right,Method); break;
            }
            case ExpressionType.Subtract: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.Subtract(Left,Right,Method); break;
            }
            case ExpressionType.SubtractAssign: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.SubtractAssign(Left,Right,Method); break;
            }
            case ExpressionType.SubtractAssignChecked: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.SubtractAssignChecked(Left,Right,Method); break;
            }
            case ExpressionType.SubtractChecked: {
                var (Left, Right, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_MethodInfo(ref reader);
                value=Expressions.Expression.SubtractChecked(Left,Right,Method); break;
            }
            case ExpressionType.Equal: {
                var (Left, Right, IsLiftedToNull, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_bool_MethodInfo(ref reader);
                value=Expressions.Expression.Equal(Left,Right,IsLiftedToNull,Method); break;
            }
            case ExpressionType.GreaterThan: {
                var (Left, Right, IsLiftedToNull, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_bool_MethodInfo(ref reader);
                value=Expressions.Expression.GreaterThan(Left,Right,IsLiftedToNull,Method); break;
            }
            case ExpressionType.GreaterThanOrEqual: {
                var (Left, Right, IsLiftedToNull, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_bool_MethodInfo(ref reader);
                value=Expressions.Expression.GreaterThanOrEqual(Left,Right,IsLiftedToNull,Method); break;
            }
            case ExpressionType.LessThan: {
                var (Left, Right, IsLiftedToNull, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_bool_MethodInfo(ref reader);
                value=Expressions.Expression.LessThan(Left,Right,IsLiftedToNull,Method); break;
            }
            case ExpressionType.LessThanOrEqual: {
                var (Left, Right, IsLiftedToNull, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_bool_MethodInfo(ref reader);
                value=Expressions.Expression.LessThanOrEqual(Left,Right,IsLiftedToNull,Method); break;
            }
            case ExpressionType.NotEqual: {
                var (Left, Right, IsLiftedToNull, Method)=MemoryPackCustomSerializer.Binary.Deserialize_Binary_bool_MethodInfo(ref reader);
                value=Expressions.Expression.NotEqual(Left,Right,IsLiftedToNull,Method); break;
            }
            case ExpressionType.ArrayIndex: {
                var (array, index)=MemoryPackCustomSerializer.Binary.Deserialize_Binary(ref reader);
                value=Expressions.Expression.ArrayIndex(array,index); break;
            }
            //case Expressions.ExpressionType.Assign:
            //case Expressions.ExpressionType.Coalesce:
            //case Expressions.ExpressionType.Add:
            //case Expressions.ExpressionType.AddAssign:
            //case Expressions.ExpressionType.AddAssignChecked:
            //case Expressions.ExpressionType.AddChecked:
            //case Expressions.ExpressionType.And:
            //case Expressions.ExpressionType.AndAssign:
            //case Expressions.ExpressionType.AndAlso:
            //case Expressions.ExpressionType.Divide:
            //case Expressions.ExpressionType.DivideAssign:
            //case Expressions.ExpressionType.ExclusiveOr:
            //case Expressions.ExpressionType.ExclusiveOrAssign:
            //case Expressions.ExpressionType.LeftShift:
            //case Expressions.ExpressionType.LeftShiftAssign:
            //case Expressions.ExpressionType.Modulo:
            //case Expressions.ExpressionType.ModuloAssign:
            //case Expressions.ExpressionType.Multiply:
            //case Expressions.ExpressionType.MultiplyAssign:
            //case Expressions.ExpressionType.MultiplyAssignChecked:
            //case Expressions.ExpressionType.MultiplyChecked:
            //case Expressions.ExpressionType.Or:
            //case Expressions.ExpressionType.OrAssign:
            //case Expressions.ExpressionType.OrElse:
            //case Expressions.ExpressionType.Power:
            //case Expressions.ExpressionType.PowerAssign:
            //case Expressions.ExpressionType.RightShift:
            //case Expressions.ExpressionType.RightShiftAssign:
            //case Expressions.ExpressionType.Subtract:
            //case Expressions.ExpressionType.SubtractAssign:
            //case Expressions.ExpressionType.SubtractAssignChecked:
            //case Expressions.ExpressionType.SubtractChecked:
            //case Expressions.ExpressionType.Equal:
            //case Expressions.ExpressionType.GreaterThan:
            //case Expressions.ExpressionType.GreaterThanOrEqual:
            //case Expressions.ExpressionType.LessThan:
            //case Expressions.ExpressionType.LessThanOrEqual:
            //case Expressions.ExpressionType.NotEqual:
            //case Expressions.ExpressionType.ArrayIndex:value=CustomSerializerMemoryPack.Binary.Deserialize(ref reader);break;
            case ExpressionType.ArrayLength: {
                var Operand=MemoryPackCustomSerializer.Unary.Deserialize_Unary(ref reader);
                value=Expressions.Expression.ArrayLength(Operand); break;
            }
            case ExpressionType.Convert: {
                var (Operand, Type, Method)=MemoryPackCustomSerializer.Unary.Deserialize_Unary_Type_MethodInfo(ref reader);
                value=Expressions.Expression.Convert(Operand,Type,Method); break;
            }
            case ExpressionType.ConvertChecked: {
                var (Operand, Type, Method)=MemoryPackCustomSerializer.Unary.Deserialize_Unary_Type_MethodInfo(ref reader);
                value=Expressions.Expression.ConvertChecked(Operand,Type,Method); break;
            }
            case ExpressionType.Decrement: {
                var (Operand, Method)=MemoryPackCustomSerializer.Unary.Deserialize_Unary_MethodInfo(ref reader);
                value=Expressions.Expression.Decrement(Operand,Method); break;
            }
            case ExpressionType.Increment: {
                var (Operand, Method)=MemoryPackCustomSerializer.Unary.Deserialize_Unary_MethodInfo(ref reader);
                value=Expressions.Expression.Increment(Operand,Method); break;
            }
            case ExpressionType.IsFalse: {
                var (Operand, Method)=MemoryPackCustomSerializer.Unary.Deserialize_Unary_MethodInfo(ref reader);
                value=Expressions.Expression.IsFalse(Operand,Method); break;
            }
            case ExpressionType.IsTrue: {
                var (Operand, Method)=MemoryPackCustomSerializer.Unary.Deserialize_Unary_MethodInfo(ref reader);
                value=Expressions.Expression.IsTrue(Operand,Method); break;
            }
            case ExpressionType.Negate: {
                var (Operand, Method)=MemoryPackCustomSerializer.Unary.Deserialize_Unary_MethodInfo(ref reader);
                value=Expressions.Expression.Negate(Operand,Method); break;
            }
            case ExpressionType.NegateChecked: {
                var (Operand, Method)=MemoryPackCustomSerializer.Unary.Deserialize_Unary_MethodInfo(ref reader);
                value=Expressions.Expression.NegateChecked(Operand,Method); break;
            }
            case ExpressionType.Not: {
                var (Operand, Method)=MemoryPackCustomSerializer.Unary.Deserialize_Unary_MethodInfo(ref reader);
                value=Expressions.Expression.Not(Operand,Method); break;
            }
            case ExpressionType.OnesComplement: {
                var (Operand, Method)=MemoryPackCustomSerializer.Unary.Deserialize_Unary_MethodInfo(ref reader);
                value=Expressions.Expression.OnesComplement(Operand,Method); break;
            }
            case ExpressionType.PostDecrementAssign: {
                var (Operand, Method)=MemoryPackCustomSerializer.Unary.Deserialize_Unary_MethodInfo(ref reader);
                value=Expressions.Expression.PostDecrementAssign(Operand,Method); break;
            }
            case ExpressionType.PostIncrementAssign: {
                var (Operand, Method)=MemoryPackCustomSerializer.Unary.Deserialize_Unary_MethodInfo(ref reader);
                value=Expressions.Expression.PostIncrementAssign(Operand,Method); break;
            }
            case ExpressionType.PreDecrementAssign: {
                var (Operand, Method)=MemoryPackCustomSerializer.Unary.Deserialize_Unary_MethodInfo(ref reader);
                value=Expressions.Expression.PreDecrementAssign(Operand,Method); break;
            }
            case ExpressionType.PreIncrementAssign: {
                var (Operand, Method)=MemoryPackCustomSerializer.Unary.Deserialize_Unary_MethodInfo(ref reader);
                value=Expressions.Expression.PreIncrementAssign(Operand,Method); break;
            }
            case ExpressionType.Quote: {
                var result = Expressions.Expression.Quote(MemoryPackCustomSerializer.Unary.Deserialize_Unary(ref reader));
                value=result; break;
            }
            case ExpressionType.Throw: {
                var (Operand, Type)=MemoryPackCustomSerializer.Unary.Deserialize_Unary_Type(ref reader);
                value=Expressions.Expression.Throw(Operand,Type); break;
            }
            case ExpressionType.TypeAs: {
                var (Operand, Type)=MemoryPackCustomSerializer.Unary.Deserialize_Unary_Type(ref reader);
                value=Expressions.Expression.TypeAs(Operand,Type); break;
            }
            case ExpressionType.UnaryPlus: {
                var (Operand, Method)=MemoryPackCustomSerializer.Unary.Deserialize_Unary_MethodInfo(ref reader);
                value=Expressions.Expression.UnaryPlus(Operand,Method); break;
            }
            case ExpressionType.Unbox: {
                var (Operand, Type)=MemoryPackCustomSerializer.Unary.Deserialize_Unary_Type(ref reader);
                value=Expressions.Expression.Unbox(Operand,Type); break;
            }
            ////readonly object[] Objects2=new object[2];

            //case Expressions.ExpressionType.ArrayLength        :
            //case Expressions.ExpressionType.Convert            :
            //case Expressions.ExpressionType.ConvertChecked     :
            //case Expressions.ExpressionType.Decrement          :
            //case Expressions.ExpressionType.Increment          :
            //case Expressions.ExpressionType.IsFalse            :
            //case Expressions.ExpressionType.IsTrue             :
            //case Expressions.ExpressionType.Negate             :
            //case Expressions.ExpressionType.NegateChecked      :
            //case Expressions.ExpressionType.Not                :
            //case Expressions.ExpressionType.OnesComplement     :
            //case Expressions.ExpressionType.PostDecrementAssign:
            //case Expressions.ExpressionType.PostIncrementAssign:
            //case Expressions.ExpressionType.PreDecrementAssign :
            //case Expressions.ExpressionType.PreIncrementAssign :
            //case Expressions.ExpressionType.Quote              :
            //case Expressions.ExpressionType.Throw              :
            //case Expressions.ExpressionType.TypeAs             :
            //case Expressions.ExpressionType.UnaryPlus          :
            //case Expressions.ExpressionType.Unbox              :value=CustomSerializerMemoryPack.Unary.Deserialize(ref reader);break;
            case ExpressionType.TypeEqual          :
            case ExpressionType.TypeIs             :value=MemoryPackCustomSerializer.TypeBinary.DeserializeTypeBinary(ref reader);break;
            case ExpressionType.Conditional        :value=MemoryPackCustomSerializer.Conditional.DeserializeConditional(ref reader);break;
            case ExpressionType.Constant           :value=MemoryPackCustomSerializer.Constant.DeserializeConstant(ref reader);break;
            case ExpressionType.Parameter          :value=MemoryPackCustomSerializer.Parameter.DeserializeParameter(ref reader);break;
            case ExpressionType.Lambda             :value=MemoryPackCustomSerializer.Lambda.DeserializeLambda(ref reader);break;
            case ExpressionType.Call               :value=MemoryPackCustomSerializer.MethodCall.DeserializeMethodCall(ref reader);break;
            case ExpressionType.Invoke             :value=MemoryPackCustomSerializer.Invocation.DeserializeInvocation(ref reader);break;
            case ExpressionType.New                :value=MemoryPackCustomSerializer.New.DeserializeNew(ref reader);break;
            case ExpressionType.NewArrayInit       :value=MemoryPackCustomSerializer.NewArray.DeserializeNewArray(ref reader);break;
            case ExpressionType.NewArrayBounds     :value=MemoryPackCustomSerializer.NewArray.DeserializeNewArray(ref reader);break;
            case ExpressionType.ListInit           :value=MemoryPackCustomSerializer.ListInit.DeserializeListInit(ref reader);break;
            case ExpressionType.MemberAccess       :value=MemoryPackCustomSerializer.MemberAccess.DeserializeMember(ref reader);break;
            case ExpressionType.MemberInit         :value=MemoryPackCustomSerializer.MemberInit.DeserializeMemberInit(ref reader);break;
            case ExpressionType.Block              :value=MemoryPackCustomSerializer.Block.DeserializeBlock(ref reader);break;
            case ExpressionType.DebugInfo          :
            case ExpressionType.Dynamic            :
            case ExpressionType.Default            :value=MemoryPackCustomSerializer.Default.DeserializeDefault(ref reader);break;
            case ExpressionType.Extension          :break;
            case ExpressionType.Goto               :value=MemoryPackCustomSerializer.Goto.DeserializeGoto(ref reader);break;
            case ExpressionType.Index              :value=MemoryPackCustomSerializer.Index.DeserializeIndex(ref reader);break;
            case ExpressionType.Label              :value=MemoryPackCustomSerializer.Label.DeserializeLabel(ref reader);break;
            case ExpressionType.RuntimeVariables   :break;
            case ExpressionType.Loop               :value=MemoryPackCustomSerializer.Loop.DeserializeLoop(ref reader);break;
            case ExpressionType.Switch             :value=MemoryPackCustomSerializer.Switch.DeserializeSwitch(ref reader);break;
            case ExpressionType.Try                :value=MemoryPackCustomSerializer.Try.DeserializeTry(ref reader);break;
            default:throw new NotSupportedException(NodeType.ToString());
        }
        
        //value=this.Deserialize(ref reader,options);
    }
}
