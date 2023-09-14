using System;
using Utf8Json;
using Expressions=System.Linq.Expressions;
using Microsoft.CSharp.RuntimeBinder;
using System.Diagnostics;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.Dynamic;
using MemoryPack;
using System.Buffers;
using LinqDB.Serializers.MemoryPack;
using System.Collections.ObjectModel;
using LinqDB.Helpers;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.DynamicExpression;
using static Extension;
using static Common;
public class Dynamic:IJsonFormatter<T> {
    public static readonly Dynamic Instance=new();
    private static void WriteBinderType(ref Writer writer,BinderType value){
        writer.WriteString(value.ToString());
    }
    private static BinderType ReadBinderType(ref Reader reader){
        var NodeTypeName=reader.ReadString();
        return Enum.Parse<BinderType>(NodeTypeName);
    }
    internal static void InternalSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        switch(value.Binder){
            case DynamicMetaObjectBinder v0:{
                switch(v0){
                    case BinaryOperationBinder v1:{
                        WriteBinderType(ref writer,BinderType.BinaryOperationBinder);
                        writer.WriteValueSeparator();
                        writer.WriteType(v0.ReturnType);
                        writer.WriteValueSeparator();
                        writer.WriteNodeType(v1.Operation);
                        writer.WriteValueSeparator();
                        var Arguments=value.Arguments;
                        Expression.Instance.Serialize(ref writer,Arguments[0],Resolver);
                        writer.WriteValueSeparator();
                        Expression.Instance.Serialize(ref writer,Arguments[1],Resolver);
                        break;
                    }
                    case ConvertBinder v1:{
                        WriteBinderType(ref writer,BinderType.ConvertBinder);
                        writer.WriteValueSeparator();
                        writer.WriteType(value.Type);
                        writer.WriteValueSeparator();
                        writer.WriteType(v1.Type);
                        writer.WriteValueSeparator();
                        writer.WriteBoolean(v1.Explicit);
                        writer.WriteValueSeparator();
                        Debug.Assert(value.Arguments.Count==1);
                        Expression.Instance.Serialize(ref writer,value.Arguments[0],Resolver);
                        break;
                    }
                    case CreateInstanceBinder v1:{
                        WriteBinderType(ref writer,BinderType.CreateInstanceBinder);
                        writer.WriteValueSeparator();
                        writer.WriteInt32(v1.CallInfo.ArgumentCount);
                        writer.WriteValueSeparator();
                        writer.SerializeReadOnlyCollection(v1.CallInfo.ArgumentNames,Resolver);
                        break;
                    }
                    case DeleteIndexBinder v1:{
                        WriteBinderType(ref writer,BinderType.DeleteIndexBinder);
                        writer.WriteValueSeparator();
                        writer.WriteInt32(v1.CallInfo.ArgumentCount);
                        writer.WriteValueSeparator();
                        writer.SerializeReadOnlyCollection(v1.CallInfo.ArgumentNames,Resolver);
                        break;
                    }
                    case DeleteMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.DeleteMemberBinder);
                        writer.WriteValueSeparator();
                        writer.WriteString(v1.Name);
                        break;
                    }
                    case GetIndexBinder v1:{
                        WriteBinderType(ref writer,BinderType.GetIndexBinder);
                        writer.WriteValueSeparator();
                        writer.WriteType(v1.ReturnType);
                        共通(ref writer,value.Arguments,v1.CallInfo);
                        break;
                    }
                    case GetMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.GetMemberBinder);
                        writer.WriteValueSeparator();
                        writer.WriteType(v1.ReturnType);
                        writer.WriteValueSeparator();
                        writer.WriteString(v1.Name);
                        writer.WriteValueSeparator();
                        Expression.Instance.Serialize(ref writer,value.Arguments[0],Resolver);
                        break;
                    }
                    case InvokeBinder v1:{
                        WriteBinderType(ref writer,BinderType.InvokeBinder);
                        writer.WriteValueSeparator();
                        writer.WriteType(v1.ReturnType);
                        共通(ref writer,value.Arguments,v1.CallInfo);
                        break;
                    }
                    case InvokeMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.InvokeMemberBinder);
                        writer.WriteValueSeparator();
                        writer.WriteType(v0.ReturnType);
                        writer.WriteValueSeparator();
                        writer.WriteString(v1.Name);
                        共通(ref writer,value.Arguments,v1.CallInfo);
                        break;
                    }
                    case SetIndexBinder v1:{
                        WriteBinderType(ref writer,BinderType.SetIndexBinder);
                        writer.WriteValueSeparator();
                        writer.WriteType(v0.ReturnType);
                        共通(ref writer,value.Arguments,v1.CallInfo);
                        break;
                    }
                    case SetMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.SetMemberBinder);
                        writer.WriteValueSeparator();
                        writer.WriteType(v0.ReturnType);
                        writer.WriteValueSeparator();
                        writer.WriteString(v1.Name);
                        writer.WriteValueSeparator();
                        var Arguments=value.Arguments;
                        Debug.Assert(Arguments.Count==2);
                        Expression.Instance.Serialize(ref writer,Arguments[0],Resolver);
                        writer.WriteValueSeparator();
                        Expression.Instance.Serialize(ref writer,Arguments[1],Resolver);
                        break;
                    }
                    case UnaryOperationBinder v1:{
                        WriteBinderType(ref writer,BinderType.UnaryOperationBinder);
                        writer.WriteValueSeparator();
                        writer.WriteType(v0.ReturnType);
                        writer.WriteValueSeparator();
                        writer.WriteNodeType(v1.Operation);
                        writer.WriteValueSeparator();
                        Expression.Instance.Serialize(ref writer,value.Arguments[0],Resolver);
                        break;
                    }
                    default:{
                        dynamic o=new NonPublicAccessor(v0);
                        var BindingFlags=o.BindingFlags;
                        var Name=o.Name;
                        var TypeArguments=o.TypeArguments;
                        throw new NotSupportedException(v0.ToString());
                    }
                }
                break;
            }
        }
        void 共通(ref Writer writer0,ReadOnlyCollection<Expressions.Expression>Arguments,CallInfo CallInfo){
            writer0.WriteValueSeparator();
            writer0.WriteInt32(CallInfo.ArgumentCount);
            writer0.WriteValueSeparator();
            writer0.SerializeReadOnlyCollection(CallInfo.ArgumentNames,Resolver);
            writer0.WriteValueSeparator();
            writer0.SerializeReadOnlyCollection(Arguments,Resolver);
        }
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
     //   if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        InternalSerialize(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T InternalDeserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        T value;
        var BinderType=ReadBinderType(ref reader);
        reader.ReadIsValueSeparatorWithVerify();
        switch(BinderType){
            case BinderType.BinaryOperationBinder:{
                var returnType=reader.ReadType();
                reader.ReadIsValueSeparatorWithVerify();
                var operation=reader.ReadNodeType();
                reader.ReadIsValueSeparatorWithVerify();
                var left=Expression.Instance.Deserialize(ref reader,Resolver);
                reader.ReadIsValueSeparatorWithVerify();
                var right=Expression.Instance.Deserialize(ref reader,Resolver);
                value=Expressions.Expression.Dynamic(
                    Binder.BinaryOperation(
                        CSharpBinderFlags.None,
                        operation,
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
                reader.ReadIsValueSeparatorWithVerify();
                var type=reader.ReadType();
                reader.ReadIsValueSeparatorWithVerify();
                var Explicit=reader.ReadBoolean();
                reader.ReadIsValueSeparatorWithVerify();
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
                var (CSharpArgumentInfos,Arguments)=共通(ref reader);
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
                reader.ReadIsValueSeparatorWithVerify();
                var name=reader.ReadString();
                reader.ReadIsValueSeparatorWithVerify();
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
                reader.ReadIsValueSeparatorWithVerify();
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
                reader.ReadIsValueSeparatorWithVerify();
                var name=reader.ReadString();
                reader.ReadIsValueSeparatorWithVerify();
                var left=Expression.Instance.Deserialize(ref reader,Resolver);
                reader.ReadIsValueSeparatorWithVerify();
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
                reader.ReadIsValueSeparatorWithVerify();
                var Operation=reader.ReadNodeType();
                reader.ReadIsValueSeparatorWithVerify();
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
        (CSharpArgumentInfo[]CSharpArgumentInfos,Expressions.Expression[]Arguments)共通(ref Reader reader0){
            reader0.ReadIsValueSeparatorWithVerify();
            var ArgumentCount=reader0.ReadInt32();
            reader0.ReadIsValueSeparatorWithVerify();
            var ArgumentNames=reader0.ReadArray<string>(Resolver);
            reader0.ReadIsValueSeparatorWithVerify();
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
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        //if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=InternalDeserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
