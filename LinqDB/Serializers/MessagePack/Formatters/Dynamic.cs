using System;
using System.Dynamic;
using System.Diagnostics;
using Microsoft.CSharp.RuntimeBinder;
using System.Collections.ObjectModel;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.DynamicExpression;
using static Common;
public class Dynamic:IMessagePackFormatter<T> {
    public static readonly Dynamic Instance=new();
    private static void PrivateSerialize0(ref Writer writer,T value,int offset){
        switch(value.Binder){
            case DynamicMetaObjectBinder v0:{
                switch(v0){
                    case BinaryOperationBinder:writer.WriteArrayHeader(offset+5); break;
                    case ConvertBinder        :writer.WriteArrayHeader(offset+4); break;
                    case CreateInstanceBinder :writer.WriteArrayHeader(offset+3); break;
                    case DeleteIndexBinder    :writer.WriteArrayHeader(offset+3); break;
                    case DeleteMemberBinder   :writer.WriteArrayHeader(offset+2); break;
                    case GetIndexBinder       :writer.WriteArrayHeader(offset+3); break;
                    case GetMemberBinder      :writer.WriteArrayHeader(offset+4); break;
                    case InvokeBinder         :writer.WriteArrayHeader(offset+3); break;
                    case InvokeMemberBinder   :writer.WriteArrayHeader(offset+2); break;
                    case SetIndexBinder       :writer.WriteArrayHeader(offset+3); break;
                    case SetMemberBinder      :writer.WriteArrayHeader(offset+5); break;
                    case UnaryOperationBinder :writer.WriteArrayHeader(offset+4); break;
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
                        Expression.Write(ref writer,Arguments[0],Resolver);
                        Expression.Write(ref writer,Arguments[1],Resolver);
                        break;
                    }
                    case ConvertBinder v1:{
                        WriteBinderType(ref writer,BinderType.ConvertBinder);
                        Debug.Assert(v0.ReturnType==v1.Type);
                        writer.WriteType(value.Type);
                        writer.WriteType(v1.Type);
                        writer.WriteBoolean(v1.Explicit);
                        Debug.Assert(value.Arguments.Count==1);
                        Expression.Write(ref writer,value.Arguments[0],Resolver);
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
                        共通(ref writer,value.Arguments,v1.CallInfo);
                        break;
                    }
                    case GetMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.GetMemberBinder);
                        writer.WriteType(v0.ReturnType);
                        writer.Write(v1.Name);
                        Expression.Write(ref writer,value.Arguments[0],Resolver);
                        break;
                    }
                    case InvokeBinder v1:{
                        WriteBinderType(ref writer,BinderType.InvokeBinder);
                        writer.WriteType(v0.ReturnType);
                        共通(ref writer,value.Arguments,v1.CallInfo);
                        break;
                    }
                    case InvokeMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.InvokeMemberBinder);
                        writer.WriteType(v0.ReturnType);
                        writer.Write(v1.Name);
                        共通(ref writer,value.Arguments,v1.CallInfo);
                        break;
                    }
                    case SetIndexBinder v1:{
                        WriteBinderType(ref writer,BinderType.SetIndexBinder);
                        writer.WriteType(v0.ReturnType);
                        共通(ref writer,value.Arguments,v1.CallInfo);
                        break;
                    }
                    case SetMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.SetMemberBinder);
                        writer.WriteType(v0.ReturnType);
                        writer.Write(v1.Name);
                        var Arguments=value.Arguments;
                        Debug.Assert(Arguments.Count==2);
                        Expression.Write(ref writer,Arguments[0],Resolver);
                        Expression.Write(ref writer,Arguments[1],Resolver);
                        break;
                    }
                    case UnaryOperationBinder v1:{
                        WriteBinderType(ref writer,BinderType.UnaryOperationBinder);
                        writer.WriteType(v0.ReturnType);
                        writer.WriteNodeType(v1.Operation);
                        Expression.Write(ref writer,value.Arguments[0],Resolver);
                        break;
                    }
                }
                break;
            }
        }
        void 共通(ref Writer writer0,ReadOnlyCollection<Expressions.Expression>Arguments,CallInfo CallInfo){
            writer0.WriteInt32(CallInfo.ArgumentCount);
            writer0.SerializeReadOnlyCollection(CallInfo.ArgumentNames,Resolver);
            writer0.SerializeReadOnlyCollection(Arguments,Resolver);
        }
    }
    internal static void Write(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
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
    private static CSharpBinderFlags ReadBindingFlags(ref Reader reader){
        var v=reader.ReadSByte();
        return (CSharpBinderFlags)v;
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        if(writer.TryWriteNil(value)) return;
        PrivateSerialize0(ref writer,value,0);
        PrivateSerialize1(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,MessagePackSerializerOptions Resolver){
        T value;
        var BinderType=ReadBinderType(ref reader);
        switch(BinderType){
            case BinderType.BinaryOperationBinder:{
                var ReturnType=reader.ReadType();
                var Operation=reader.ReadNodeType();
                var left=Expression.Read(ref reader,Resolver);
                var right=Expression.Read(ref reader,Resolver);
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
                var operand=Expression.Read(ref reader,Resolver);
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
                var (CSharpArgumentInfos,Arguments)=共通(ref reader);
                value=Expressions.Expression.Dynamic(
                    Binder.GetIndex(
                        CSharpBinderFlags.None,
                        typeof(Expression),
                        CSharpArgumentInfos
                    ),
                    returnType,
                    Arguments
                );
                break;
            }
            case BinderType.GetMemberBinder:{
                var returnType=reader.ReadType();
                var name=reader.ReadString();
                var operand=Expression.Read(ref reader,Resolver);
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
                var (CSharpArgumentInfos,Arguments)=共通(ref reader);
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
                var (CSharpArgumentInfos,Arguments)=共通(ref reader);
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
                var (CSharpArgumentInfos,Arguments)=共通(ref reader);
                value=Expressions.Expression.Dynamic(
                    Binder.SetIndex(
                        CSharpBinderFlags.None,
                        typeof(Expression),
                        CSharpArgumentInfos
                    ),
                    returnType,
                    Arguments
                );
                break;
            }
            case BinderType.SetMemberBinder:{
                var returnType=reader.ReadType();
                var name=reader.ReadString();
                var left=Expression.Read(ref reader,Resolver);
                var right=Expression.Read(ref reader,Resolver);
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
                var operand=Expression.Read(ref reader,Resolver);
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
        (CSharpArgumentInfo[]CSharpArgumentInfos,Expressions.Expression[]Arguments)共通(ref Reader reader0){
            var ArgumentCount=reader0.ReadInt32();
            var ArgumentNames=reader0.ReadArray<string>(Resolver);
            //ArgumentCountは()内の引数。Argumentsはthisも含んでいる
            var Arguments=reader0.ReadArray<Expressions.Expression>(Resolver);
            var CSharpArgumentInfos=new CSharpArgumentInfo[Arguments.Length];
            var ArgumentNames_Length=ArgumentNames.Length;
            for(var a=0;a<ArgumentNames_Length;a++)CSharpArgumentInfos[a]=CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.NamedArgument,ArgumentNames[a]);
            var CSharpArgumentInfos_Length=CSharpArgumentInfos.Length;
            //Debug.Assert((CSharpArgumentInfos_Length==ArgumentCount||CSharpArgumentInfos_Length-1==ArgumentCount)&&ArgumentNames.Length<=ArgumentCount);
            for(var a=ArgumentNames_Length;a<CSharpArgumentInfos_Length;a++)CSharpArgumentInfos[a]=CSharpArgumentInfo1;
            return (CSharpArgumentInfos,Arguments);
        }
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        return Read(ref reader,Resolver);
    }
}