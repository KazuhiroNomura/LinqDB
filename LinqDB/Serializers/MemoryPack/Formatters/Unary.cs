using System;
using System.Reflection;
using MemoryPack;
using System.Buffers;
using Expressions=System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;

public class Unary:MemoryPackFormatter<Expressions.UnaryExpression>{
    //internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,UnaryExpression? value){
    //    this.Serialize(ref writer,ref value);
    //}
    //private UnaryExpression DeserializeUnary(ref MemoryPackReader reader){
    //    UnaryExpression? value=default;
    //    this.Deserialize(ref reader,ref value);
    //    return value!;
    //}
    //private void Serialize_Unary<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression value)where TBufferWriter:IBufferWriter<byte>{
    //    var Unary=(UnaryExpression)value;
    //    this.Serialize(ref writer,Unary.Operand);
    //}
    //private void Serialize_Unary_Type<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression value)where TBufferWriter:IBufferWriter<byte>{
    //   var Unary=(UnaryExpression)value;
    //   this.Serialize(ref writer,Unary.Operand);
    //   this.Serialize(ref writer,Unary.Type);
    //}
    //private void Serialize_Unary_MethodInfo<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression value)where TBufferWriter:IBufferWriter<byte>{
    //   var Unary=(UnaryExpression)value;
    //   this.Serialize(ref writer,Unary.Operand);
    //   this.Serialize(ref writer,Unary.Method);
    //}
    //private void Serialize_Unary_Type_MethodInfo<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression value)where TBufferWriter:IBufferWriter<byte>{
    //   var Unary=(UnaryExpression)value;
    //   this.Serialize(ref writer,Unary.Operand);
    //   this.Serialize(ref writer,Unary.Type);
    //   this.Serialize(ref writer,Unary.Method);
    //}
    ////private readonly object[] Objects2=new object[2];
    //private Expressions.Expression Deserialize_Unary(ref MemoryPackReader reader)=>this.DeserializeExpression(ref reader);
    //private (Expressions.Expression Operand,System.Type Type)Deserialize_Unary_Type(ref MemoryPackReader reader){
    //   var Operand= this.DeserializeExpression(ref reader);
    //   var Type=this.DeserializeType(ref reader);
    //   return(Operand,Type);
    //}
    //private (Expressions.Expression Operand,MethodInfo Method)Deserialize_Unary_MethodInfo(ref MemoryPackReader reader){
    //   var Operand= this.DeserializeExpression(ref reader);
    //   var Method=this.DeserializeMethodInfo(ref reader);
    //   return(Operand,Method);
    //}
    //private (Expressions.Expression Operand,System.Type Type,MethodInfo Method)Deserialize_Unary_Type_MethodInfo(ref MemoryPackReader reader){
    //   var Operand= this.DeserializeExpression(ref reader);
    //   var Type=this.DeserializeType(ref reader);
    //   var Method=this.DeserializeMethodInfo(ref reader);
    //   return(Operand,Type,Method);
    //}
    public void Serialize_Unary<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression value) where TBufferWriter :IBufferWriter<byte> {
        var Unary = (Expressions.UnaryExpression)value;
        CustomSerializerMemoryPack.Expression.Serialize(ref writer,Unary.Operand);
    }
    public void Serialize_Unary_Type<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression value) where TBufferWriter :IBufferWriter<byte> {
        var Unary = (Expressions.UnaryExpression)value;
        CustomSerializerMemoryPack.Expression.Serialize(ref writer,Unary.Operand);
        CustomSerializerMemoryPack.Type.Serialize(ref writer,Unary.Type);
    }
    public void Serialize_Unary_MethodInfo<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression value) where TBufferWriter :IBufferWriter<byte> {
        var Unary = (Expressions.UnaryExpression)value;
        CustomSerializerMemoryPack.Expression.Serialize(ref writer,Unary.Operand);
        CustomSerializerMemoryPack.Method.SerializeNullable(ref writer,Unary.Method);
    }
    public void Serialize_Unary_Type_MethodInfo<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression value) where TBufferWriter :IBufferWriter<byte> {
        var Unary = (Expressions.UnaryExpression)value;
        CustomSerializerMemoryPack.Expression.Serialize(ref writer,Unary.Operand);
        CustomSerializerMemoryPack.Type.Serialize(ref writer,Unary.Type);
        CustomSerializerMemoryPack.Method.SerializeNullable(ref writer,Unary.Method);
    }
    public Expressions.Expression Deserialize_Unary(ref MemoryPackReader reader) => CustomSerializerMemoryPack.Expression.Deserialize(ref reader);
    public (Expressions.Expression Operand, System.Type Type) Deserialize_Unary_Type(ref MemoryPackReader reader) {
        var Operand = CustomSerializerMemoryPack.Expression.Deserialize(ref reader);
        var Type = CustomSerializerMemoryPack.Type.DeserializeType(ref reader);
        return (Operand, Type);
    }
    public (Expressions.Expression Operand, MethodInfo? Method) Deserialize_Unary_MethodInfo(ref MemoryPackReader reader) {
        var Operand = CustomSerializerMemoryPack.Expression.Deserialize(ref reader);
        var Method = CustomSerializerMemoryPack.Method.DeserializeNullable(ref reader);
        return (Operand, Method);
    }
    public (Expressions.Expression Operand, System.Type Type, MethodInfo? Method) Deserialize_Unary_Type_MethodInfo(ref MemoryPackReader reader) {
        var Operand = CustomSerializerMemoryPack.Expression.Deserialize(ref reader);
        var Type = CustomSerializerMemoryPack.Type.DeserializeType(ref reader);
        var Method = CustomSerializerMemoryPack.Method.DeserializeNullable(ref reader);
        return (Operand, Type, Method);
    }
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.UnaryExpression? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal Expressions.UnaryExpression Deserialize(ref MemoryPackReader reader){
        Expressions.UnaryExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Expressions.UnaryExpression? value)/*where TBufferWriter : class, IBufferWriter<byte>*/{
       if(value is null){
           return;
       }
       writer.WriteVarInt((byte)value.NodeType);
       switch(value.NodeType){
           case Expressions.ExpressionType.ArrayLength        : this.Serialize_Unary(ref writer,value);break;
           case Expressions.ExpressionType.Convert            : this.Serialize_Unary_Type_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.ConvertChecked     : this.Serialize_Unary_Type_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.Decrement          : this.Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.Increment          : this.Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.IsFalse            : this.Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.IsTrue             : this.Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.Negate             : this.Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.NegateChecked      : this.Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.Not                : this.Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.OnesComplement     : this.Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.PostDecrementAssign: this.Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.PostIncrementAssign: this.Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.PreDecrementAssign : this.Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.PreIncrementAssign : this.Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.Quote              : this.Serialize_Unary(ref writer,value);break;
           case Expressions.ExpressionType.Throw              : this.Serialize_Unary_Type(ref writer,value);break;
           case Expressions.ExpressionType.TypeAs             : this.Serialize_Unary_Type(ref writer,value);break;
           case Expressions.ExpressionType.UnaryPlus          : this.Serialize_Unary_MethodInfo(ref writer,value);break;
           case Expressions.ExpressionType.Unbox              : this.Serialize_Unary_Type(ref writer,value);break;
           default:
               throw new NotSupportedException(value.NodeType.ToString());
       }
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.UnaryExpression? value){
       //if(reader.TryReadNil()) return;
       var NodeType=(Expressions.ExpressionType)reader.ReadVarIntByte();
       switch(NodeType){
           case Expressions.ExpressionType.ArrayLength        :{
               var Operand= this.Deserialize_Unary(ref reader);
               value=Expressions.Expression.ArrayLength(Operand);break;
           }
           case Expressions.ExpressionType.Convert            :{
               var(Operand,Type,Method)=this.Deserialize_Unary_Type_MethodInfo(ref reader);
               value=Expressions.Expression.Convert(Operand,Type,Method);break;
           }
           case Expressions.ExpressionType.ConvertChecked     :{
               var(Operand,Type,Method)=this.Deserialize_Unary_Type_MethodInfo(ref reader);
               value=Expressions.Expression.ConvertChecked(Operand,Type,Method);break;
           }
           case Expressions.ExpressionType.Decrement          :{
               var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader);
               value=Expressions.Expression.Decrement(Operand,Method);break;
           }
           case Expressions.ExpressionType.Increment          :{
               var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader);
               value=Expressions.Expression.Increment(Operand,Method);break;
           }
           case Expressions.ExpressionType.IsFalse            :{
               var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader);
               value=Expressions.Expression.IsFalse(Operand,Method);break;
           }
           case Expressions.ExpressionType.IsTrue             :{
               var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader);
               value=Expressions.Expression.IsTrue(Operand,Method);break;
           }
           case Expressions.ExpressionType.Negate             :{
               var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader);
               value=Expressions.Expression.Negate(Operand,Method);break;
           }
           case Expressions.ExpressionType.NegateChecked      :{
               var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader);
               value=Expressions.Expression.NegateChecked(Operand,Method);break;
           }
           case Expressions.ExpressionType.Not                :{
               var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader);
               value=Expressions.Expression.Not(Operand,Method);break;
           }
           case Expressions.ExpressionType.OnesComplement     :{
               var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader);
               value=Expressions.Expression.OnesComplement(Operand,Method);break;
           }
           case Expressions.ExpressionType.PostDecrementAssign:{
               var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader);
               value=Expressions.Expression.PostDecrementAssign(Operand,Method);break;
           }
           case Expressions.ExpressionType.PostIncrementAssign:{
               var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader);
               value=Expressions.Expression.PostIncrementAssign(Operand,Method);break;
           }
           case Expressions.ExpressionType.PreDecrementAssign :{
               var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader);
               value=Expressions.Expression.PreDecrementAssign(Operand,Method);break;
           }
           case Expressions.ExpressionType.PreIncrementAssign :{
               var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader);
               value=Expressions.Expression.PreIncrementAssign(Operand,Method);break;
           }
           case Expressions.ExpressionType.Quote:{
               var Quote=Expressions.Expression.Quote(this.Deserialize_Unary(ref reader));
               value=Quote;break;
           }
           case Expressions.ExpressionType.Throw              :{
               var (Operand,Type)=this.Deserialize_Unary_Type(ref reader);
               value=Expressions.Expression.Throw(Operand,Type);break;
           }
           case Expressions.ExpressionType.TypeAs             :{
               var (Operand,Type)=this.Deserialize_Unary_Type(ref reader);
               value=Expressions.Expression.TypeAs(Operand,Type);break;
           }
           case Expressions.ExpressionType.UnaryPlus:{
               var (Operand,Method)=this.Deserialize_Unary_MethodInfo(ref reader);
               value=Expressions.Expression.UnaryPlus(Operand,Method);break;
           }
           case Expressions.ExpressionType.Unbox              :{
               var (Operand,Type)=this.Deserialize_Unary_Type(ref reader);
               value=Expressions.Expression.Unbox(Operand,Type);break;
           }
           default:throw new NotSupportedException(NodeType.ToString());
       }
    }
}
