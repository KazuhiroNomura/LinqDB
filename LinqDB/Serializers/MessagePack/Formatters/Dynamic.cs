using System.Diagnostics;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using MemoryPack;
using System.Buffers;
using System.Dynamic;
using System;
using System.Reflection;
using Microsoft.CSharp.RuntimeBinder;
using Binder=Microsoft.CSharp.RuntimeBinder.Binder;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.DynamicExpression;
using static Extension;
using static Common;
public class Dynamic:IMessagePackFormatter<T> {
    public static readonly Dynamic Instance=new();
    private static void PrivateSerialize0(ref Writer writer,T value,int offset){
        switch(value.Binder){
            case DynamicMetaObjectBinder v0:{
                switch(v0){
                    case BinaryOperationBinder:writer.WriteArrayHeader(offset+5); break;
                    case ConvertBinder:writer.WriteArrayHeader(offset+4); break;
                    case CreateInstanceBinder:writer.WriteArrayHeader(offset+3); break;
                    case DeleteIndexBinder:writer.WriteArrayHeader(offset+3); break;
                    case DeleteMemberBinder:writer.WriteArrayHeader(offset+2); break;
                    case GetIndexBinder:writer.WriteArrayHeader(offset+3); break;
                    case GetMemberBinder:writer.WriteArrayHeader(offset+4); break;
                    case InvokeBinder:writer.WriteArrayHeader(offset+3); break;
                    case InvokeMemberBinder:writer.WriteArrayHeader(offset+2); break;
                    case SetIndexBinder:writer.WriteArrayHeader(offset+3); break;
                    case SetMemberBinder:writer.WriteArrayHeader(offset+5); break;
                    case UnaryOperationBinder:writer.WriteArrayHeader(offset+5); break;
                }
                break;
            }
        }
    }
    private static void PrivateSerialize1(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        switch(value.Binder){
            case DynamicMetaObjectBinder v0:{
                switch(v0){
                    case BinaryOperationBinder v1:{
                        WriteBinderType(ref writer,BinderType.BinaryOperationBinder);
                        writer.WriteType(v0.ReturnType);
                        writer.WriteNodeType(v1.Operation);
                        var Arguments=value.Arguments;
                        Expression.Instance.Serialize(ref writer,Arguments[0],Resolver);
                        Expression.Instance.Serialize(ref writer,Arguments[1],Resolver);
                        break;
                    }
                    case ConvertBinder v1:{
                        WriteBinderType(ref writer,BinderType.ConvertBinder);
                        Debug.Assert(v0.ReturnType==v1.Type);
                        writer.WriteType(value.Type);
                        writer.WriteType(v1.Type);
                        writer.WriteBoolean(v1.Explicit);
                        Debug.Assert(value.Arguments.Count==1);
                        Expression.Instance.Serialize(ref writer,value.Arguments[0],Resolver);
                        break;
                    }
                    case CreateInstanceBinder v1:{
                        WriteBinderType(ref writer,BinderType.CreateInstanceBinder);
                        writer.WriteInt32(v1.CallInfo.ArgumentCount);
                        writer.SerializeReadOnlyCollection(v1.CallInfo.ArgumentNames,Resolver);
                        break;
                    }
                    case DeleteIndexBinder v1:{
                        WriteBinderType(ref writer,BinderType.DeleteIndexBinder);
                        writer.WriteInt32(v1.CallInfo.ArgumentCount);
                        writer.SerializeReadOnlyCollection(v1.CallInfo.ArgumentNames,Resolver);
                        break;
                    }
                    case DeleteMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.DeleteMemberBinder);
                        writer.Write(v1.Name);
                        break;
                    }
                    case GetIndexBinder v1:{
                        WriteBinderType(ref writer,BinderType.GetIndexBinder);
                        writer.WriteType(v0.ReturnType);
                        writer.SerializeReadOnlyCollection(value.Arguments,Resolver);
                        break;
                    }
                    case GetMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.GetMemberBinder);
                        writer.WriteType(v0.ReturnType);
                        writer.Write(v1.Name);
                        Expression.Instance.Serialize(ref writer,value.Arguments[0],Resolver);
                        break;
                    }
                    case InvokeBinder v1:{
                        WriteBinderType(ref writer,BinderType.InvokeBinder);
                        writer.WriteType(v0.ReturnType);
                        writer.SerializeReadOnlyCollection(value.Arguments,Resolver);
                        //writer.SerializeReadOnlyCollection(v1.CallInfo.ArgumentNames,Resolver);
                        break;
                    }
                    case InvokeMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.InvokeMemberBinder);
                        writer.WriteType(v0.ReturnType);
                        writer.Write(v1.Name);
                        writer.SerializeReadOnlyCollection(value.Arguments,Resolver);
                        break;
                    }
                    case SetIndexBinder v1:{
                        WriteBinderType(ref writer,BinderType.SetIndexBinder);
                        writer.WriteType(v0.ReturnType);
                        writer.SerializeReadOnlyCollection(value.Arguments,Resolver);
                        break;
                    }
                    case SetMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.SetMemberBinder);
                        writer.WriteType(v0.ReturnType);
                        writer.Write(v1.Name);
                        var Arguments=value.Arguments;
                        Debug.Assert(Arguments.Count==2);
                        Expression.Instance.Serialize(ref writer,Arguments[0],Resolver);
                        Expression.Instance.Serialize(ref writer,Arguments[1],Resolver);
                        break;
                    }
                    case UnaryOperationBinder v1:{
                        WriteBinderType(ref writer,BinderType.UnaryOperationBinder);
                        writer.WriteType(v0.ReturnType);
                        writer.WriteNodeType(v1.Operation);
                        Expression.Instance.Serialize(ref writer,value.Arguments[0],Resolver);
                        break;
                    }
                }
                break;
            }
        }
    }
    internal static void InternalSerialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        PrivateSerialize0(ref writer,value,1);
        writer.WriteNodeType(Expressions.ExpressionType.Dynamic);
        PrivateSerialize1(ref writer,value,Resolver);
    }
    private static void WriteBinderType(ref Writer writer,BinderType value){
        writer.WriteInt8((sbyte)value);
    }
    private static BinderType ReadBinderType(ref Reader reader){
        var v=reader.ReadSByte();
        return (BinderType)v;
    }
    private static void WriteBindingFlags(ref Writer writer,BindingFlags value){
        writer.WriteInt8((sbyte)value);
    }
    private static CSharpBinderFlags ReadBindingFlags(ref Reader reader){
        var v=reader.ReadSByte();
        return (CSharpBinderFlags)v;
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        PrivateSerialize0(ref writer,value,0);
        PrivateSerialize1(ref writer,value,Resolver);
    }
    internal static T InternalDeserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        T value;
        var BinderType=ReadBinderType(ref reader);
        switch(BinderType){
            case BinderType.BinaryOperationBinder:{
                var ReturnType=reader.ReadType();
                var Operation=reader.ReadNodeType();
                var left=Expression.Instance.Deserialize(ref reader,Resolver);
                var right=Expression.Instance.Deserialize(ref reader,Resolver);
                value=Expressions.Expression.Dynamic(
                    Binder.BinaryOperation(
                        CSharpBinderFlags.None,
                        Operation,
                        typeof(Dynamic),
                        CSharpArgumentInfos2
                    ),
                    ReturnType,
                    left,
                    right
                );
                break;
            }
            case BinderType.ConvertBinder:{
                var returnType=reader.ReadType();
                var type=reader.ReadType();
                var Explicit=reader.ReadBoolean();
                var operand=Expression.Instance.Deserialize(ref reader,Resolver);
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
            //case BinderType.CreateInstanceBinder:
            //    break;
            //case BinderType.DeleteIndexBinder:
            //    break;
            //case BinderType.DeleteMemberBinder:
            //    break;
            case BinderType.GetIndexBinder:{
                var returnType=reader.ReadType();
                var Arguments=reader.DeserializeArray<Expressions.Expression>(Resolver);
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
                var operand=Expression.Instance.Deserialize(ref reader,Resolver);
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
                var Arguments=reader.DeserializeArray<Expressions.Expression>(Resolver);
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
                var Arguments=reader.DeserializeArray<Expressions.Expression>(Resolver);
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
                var Arguments=reader.DeserializeArray<Expressions.Expression>(Resolver);
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
                var left=Expression.Instance.Deserialize(ref reader,Resolver);
                var right=Expression.Instance.Deserialize(ref reader,Resolver);
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
                var operand=Expression.Instance.Deserialize(ref reader,Resolver);
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
        return value;
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
     //   if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        return InternalDeserialize(ref reader,Resolver);
    }
}