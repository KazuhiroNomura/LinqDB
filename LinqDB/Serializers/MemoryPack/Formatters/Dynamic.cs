using System;
using System.Buffers;
using System.Diagnostics;
using System.Dynamic;
using Expressions=System.Linq.Expressions;
using MemoryPack;
using Microsoft.CSharp.RuntimeBinder;
using System.Reflection;
using Binder=Microsoft.CSharp.RuntimeBinder.Binder;
//using Binder=System.Reflection.Binder;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;

using T= Expressions.DynamicExpression;
using C=Serializer;
using static Extension;
using static Common;
public class Dynamic:MemoryPackFormatter<T> {
    public static readonly Dynamic Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        this.Serialize(ref writer,ref value);
    internal void SerializeNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter : IBufferWriter<byte> {
        if(value is null)writer.WriteNullObjectHeader();
        else this.Serialize(ref writer,ref value);
    }
    internal T Deserialize(ref Reader reader) {
        T? value = default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    internal T? DeserializeNullable(ref Reader reader) {
        if(reader.PeekIsNull()){
            reader.Advance(1);
            return null;
        }
        return this.Deserialize(ref reader);
    }
    private static void WriteBinderType<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,BinderType value) where TBufferWriter:IBufferWriter<byte>{
        writer.WriteVarInt((byte)value);
    }
    private static BinderType ReadBinderType(ref Reader reader){
        return (BinderType)reader.ReadVarIntByte();
    }
    private static void WriteBindingFlags<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,BindingFlags value)where TBufferWriter:IBufferWriter<byte>{
        writer.WriteVarInt((sbyte)value);
    }
    private static CSharpBinderFlags ReadBindingFlags(ref Reader reader){
        var v=reader.ReadVarIntByte();
        return (CSharpBinderFlags)v;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        //Microsoft.CSharp.RuntimeBinder.CSharpInvokeConstructorBinder

        switch(value.Binder){
            case DynamicMetaObjectBinder v0:{
                switch(v0){
                    case BinaryOperationBinder v1:{
                        WriteBinderType(ref writer,BinderType.BinaryOperationBinder);
                        writer.WriteType(v0.ReturnType);
                        writer.WriteNodeType(v1.Operation);
                        var Arguments=value.Arguments;
                        Debug.Assert(Arguments.Count==2);
                        Expression.Instance.Serialize(ref writer,Arguments[0]);
                        Expression.Instance.Serialize(ref writer,Arguments[1]);
                        break;
                    }
                    case ConvertBinder v1:{
                        WriteBinderType(ref writer,BinderType.ConvertBinder);
                        Debug.Assert(v0.ReturnType==v1.Type);
                        writer.WriteType(value.Type);
                        writer.WriteType(v1.Type);
                        writer.WriteBoolean(v1.Explicit);
                        Debug.Assert(value.Arguments.Count==1);
                        Expression.Instance.Serialize(ref writer,value.Arguments[0]);
                        break;
                    }
                    case CreateInstanceBinder v1:{
                        WriteBinderType(ref writer,BinderType.CreateInstanceBinder);
                        //writer.WriteVarInt(v1.CallInfo.ArgumentCount);
                        writer.SerializeReadOnlyCollection(v1.CallInfo.ArgumentNames);
                        break;
                    }
                    case DeleteIndexBinder v1:{
                        WriteBinderType(ref writer,BinderType.DeleteIndexBinder);
                        writer.WriteType(v0.ReturnType);
                        writer.WriteVarInt(v1.CallInfo.ArgumentCount);
                        writer.SerializeReadOnlyCollection(v1.CallInfo.ArgumentNames);
                        break;
                    }
                    case DeleteMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.DeleteMemberBinder);
                        writer.WriteType(v0.ReturnType);
                        writer.WriteString(v1.Name);
                        break;
                    }
                    case GetIndexBinder v1:{
                        WriteBinderType(ref writer,BinderType.GetIndexBinder);
                        writer.WriteType(v0.ReturnType);
                        writer.SerializeReadOnlyCollection(value.Arguments);
                        break;
                    }
                    case GetMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.GetMemberBinder);
                        writer.WriteType(v0.ReturnType);
                        writer.WriteString(v1.Name);
                        Expression.Instance.Serialize(ref writer,value.Arguments[0]);
                        break;
                    }
                    case InvokeBinder v1:{
                        WriteBinderType(ref writer,BinderType.InvokeBinder);
                        writer.WriteType(v0.ReturnType);
                        writer.SerializeReadOnlyCollection(value.Arguments);
                        break;
                    }
                    case InvokeMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.InvokeMemberBinder);
                        writer.WriteType(v0.ReturnType);
                        writer.WriteString(v1.Name);
                        writer.SerializeReadOnlyCollection(value.Arguments);
                        break;
                    }
                    case SetIndexBinder v1:{
                        WriteBinderType(ref writer,BinderType.SetIndexBinder);
                        writer.WriteType(v0.ReturnType);
                        writer.SerializeReadOnlyCollection(value.Arguments);
                        break;
                    }
                    case SetMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.SetMemberBinder);
                        writer.WriteType(v0.ReturnType);
                        writer.WriteString(v1.Name);
                        var Arguments=value.Arguments;
                        Debug.Assert(Arguments.Count==2);
                        Expression.Instance.Serialize(ref writer,Arguments[0]);
                        Expression.Instance.Serialize(ref writer,Arguments[1]);
                        break;
                    }
                    case UnaryOperationBinder v1:{
                        WriteBinderType(ref writer,BinderType.UnaryOperationBinder);
                        writer.WriteType(v0.ReturnType);
                        writer.WriteNodeType(v1.Operation);
                        Expression.Instance.Serialize(ref writer,value.Arguments[0]);
                        break;
                    }
                }
                break;
            }
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var BinderType=ReadBinderType(ref reader);
        switch(BinderType){
            case BinderType.BinaryOperationBinder:{
                var returnType=reader.ReadType();
                var Operation=reader.ReadNodeType();
                var left=Expression.Instance.Deserialize(ref reader);
                var right=Expression.Instance.Deserialize(ref reader);
                value=Expressions.Expression.Dynamic(
                    Binder.BinaryOperation(
                        CSharpBinderFlags.None,
                        Operation,
                        typeof(Dynamic),
                        CSharpArgumentInfos2
                    ),
                    returnType,
                    left,
                    right
                );
                break;
            }
            case BinderType.ConvertBinder:{
                var returnType=reader.ReadType();
                var type=reader.ReadType();
                var Explicit=reader.ReadBoolean();
                var operand=Expression.Instance.Deserialize(ref reader);
                value=Expressions.Expression.Dynamic(
                    Binder.Convert(
                        Explicit?CSharpBinderFlags.ConvertExplicit:CSharpBinderFlags.None,
                        type,
                        typeof(Dynamic)
                    ),
                    returnType,
                    operand
                );
                break;
            }
            case BinderType.CreateInstanceBinder:
                break;
            case BinderType.DeleteIndexBinder:
                break;
            case BinderType.DeleteMemberBinder:
                break;
            case BinderType.GetIndexBinder:{
                var returnType=reader.ReadType();
                var Arguments=reader.DeserializeArray<Expressions.Expression>();
                value=Expressions.Expression.Dynamic(
                    Binder.GetIndex(
                        CSharpBinderFlags.None,
                        typeof(Expression),
                        CSharpArgumentInfos2
                    ),
                    returnType,
                    Arguments
                );
                break;
            }
            case BinderType.GetMemberBinder:{
                var returnType=reader.ReadType();
                var name=reader.ReadString();
                var operand=Expression.Instance.Deserialize(ref reader);
                value=Expressions.Expression.Dynamic(
                    Binder.GetMember(
                        CSharpBinderFlags.None,
                        name,
                        typeof(Expression),
                        CSharpArgumentInfos1
                    ),
                    returnType,
                    operand
                );
                break;
            }
            case BinderType.InvokeBinder:{
                var returnType=reader.ReadType();
                var Arguments=reader.DeserializeArray<Expressions.Expression>();
                var CSharpArgumentInfos=new CSharpArgumentInfo[Arguments.Length];
                for(var a=0;a<CSharpArgumentInfos.Length;a++){
                    CSharpArgumentInfos[a]=CSharpArgumentInfo1;
                }
                value=Expressions.Expression.Dynamic(
                    Binder.Invoke(
                        CSharpBinderFlags.None,
                        typeof(Expression),
                        CSharpArgumentInfos
                    ),
                    returnType,
                    Arguments
                );
                break;
            }
            case BinderType.InvokeMemberBinder:{
                var returnType=reader.ReadType();
                var name=reader.ReadString();
                var Arguments=reader.DeserializeArray<Expressions.Expression>();
                var CSharpArgumentInfos=new CSharpArgumentInfo[Arguments.Length];
                for(var a=0;a<CSharpArgumentInfos.Length;a++){
                    CSharpArgumentInfos[a]=CSharpArgumentInfo1;
                }
                value=Expressions.Expression.Dynamic(
                    Binder.InvokeMember(
                        CSharpBinderFlags.None,
                        name,
                        null,
                        typeof(Expression),
                        CSharpArgumentInfos
                    ),
                    returnType,
                    Arguments
                );
                break;
            }
            case BinderType.SetIndexBinder:{
                var returnType=reader.ReadType();
                var Arguments=reader.DeserializeArray<Expressions.Expression>();
                value=Expressions.Expression.Dynamic(
                    Binder.SetIndex(
                        CSharpBinderFlags.None,
                        typeof(Expression),
                        CSharpArgumentInfos2
                    ),
                    returnType,
                    Arguments
                );
                break;
            }
            case BinderType.SetMemberBinder:{
                var returnType=reader.ReadType();
                var name=reader.ReadString();
                var left=Expression.Instance.Deserialize(ref reader);
                var right=Expression.Instance.Deserialize(ref reader);
                value=Expressions.Expression.Dynamic(
                    Binder.SetMember(
                        CSharpBinderFlags.None,
                        name,
                        typeof(Expression),
                        CSharpArgumentInfos2
                    ),
                    returnType,
                    left,
                    right
                );
                break;
            }
            case BinderType.UnaryOperationBinder:{
                var ReturnType=reader.ReadType();
                var Operation=reader.ReadNodeType();
                var operand=Expression.Instance.Deserialize(ref reader);
                value=Expressions.Expression.Dynamic(
                    Binder.UnaryOperation(
                        CSharpBinderFlags.None,
                        Operation,
                        typeof(Dynamic),
                        CSharpArgumentInfos1
                    ),
                    ReturnType,
                    operand
                );
                break;
            }
            default:
                throw new ArgumentOutOfRangeException(BinderType.ToString());
        }
    }
}
