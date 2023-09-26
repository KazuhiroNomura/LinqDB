using System;
using System.Dynamic;
using System.Diagnostics;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.DynamicExpression;
using static Common;
public class Dynamic:IJsonFormatter<T> {
    public static readonly Dynamic Instance=new();
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    private static void PrivateWrite(ref Writer writer,T value,O Resolver){
        switch(value.Binder){
            case DynamicMetaObjectBinder v0:{
                switch(v0){
                    case BinaryOperationBinder v1:{
                        WriteBinderType(ref writer,BinderType.BinaryOperationBinder);
                        writer.WriteValueSeparator();
                        var (CallingContext, CSharpArgumentInfos)=v1.GetBinder();
                        writer.WriteType(CallingContext);
                        writer.WriteValueSeparator();
                        writer.WriteArray(CSharpArgumentInfos,Resolver);
                        writer.WriteValueSeparator();
                        writer.WriteType(v0.ReturnType);
                        writer.WriteValueSeparator();
                        writer.WriteNodeType(v1.Operation);
                        writer.WriteValueSeparator();
                        var Arguments=value.Arguments;
                        Debug.Assert(Arguments.Count==2);
                        Expression.Write(ref writer,Arguments[0],Resolver);
                        writer.WriteValueSeparator();
                        Expression.Write(ref writer,Arguments[1],Resolver);
                        break;
                    }
                    case ConvertBinder v1:{
                        WriteBinderType(ref writer,BinderType.ConvertBinder);
                        writer.WriteValueSeparator();
                        Debug.Assert(v0.ReturnType==v1.Type);
                        var CallingContext=v1.GetBinder();
                        writer.WriteType(CallingContext);
                        writer.WriteValueSeparator();
                        writer.WriteType(value.Type);
                        writer.WriteValueSeparator();
                        writer.WriteType(v1.Type);
                        writer.WriteValueSeparator();
                        writer.WriteBoolean(v1.Explicit);
                        writer.WriteValueSeparator();
                        Debug.Assert(value.Arguments.Count==1);
                        Expression.Write(ref writer,value.Arguments[0],Resolver);
                        break;
                    }
                    //case CreateInstanceBinder v1:{
                    //    WriteBinderType(ref writer,BinderType.CreateInstanceBinder);
                    //    writer.WriteValueSeparator();
                    //    var (CallingContext, CSharpArgumentInfos, Flags)=v1.GetBinder();
                    //    writer.WriteType(CallingContext);
                    //    writer.WriteValueSeparator();
                    //    writer.WriteArray(CSharpArgumentInfos,Resolver);
                    //    writer.WriteValueSeparator();
                    //    writer.WriteInt32(Flags);
                    //    break;
                    //}
                    //case DeleteIndexBinder v1:{
                    //    WriteBinderType(ref writer,BinderType.DeleteIndexBinder);
                    //    writer.WriteValueSeparator();
                    //    writer.WriteInt32(v1.CallInfo.ArgumentCount);
                    //    writer.WriteValueSeparator();
                    //    writer.WriteCollection(v1.CallInfo.ArgumentNames,Resolver);
                    //    break;
                    //}
                    
                    //case DeleteMemberBinder v1:{
                    //    WriteBinderType(ref writer,BinderType.DeleteMemberBinder);
                    //    writer.WriteValueSeparator();
                    //    writer.WriteString(v1.Name);
                    //    break;
                    //}
                    
                    
                    
                    
                    case GetIndexBinder v1:{
                        WriteBinderType(ref writer,BinderType.GetIndexBinder);
                        writer.WriteValueSeparator();
                        var (CallingContext,CSharpArgumentInfos)=v1.GetBinder();
                        writer.WriteType(CallingContext);
                        writer.WriteValueSeparator();
                        writer.WriteArray(CSharpArgumentInfos,Resolver);
                        writer.WriteValueSeparator();
                        writer.WriteType(v1.ReturnType);
                        writer.WriteValueSeparator();
                        writer.WriteCollection(value.Arguments,Resolver);
                        break;
                    }
                    case GetMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.GetMemberBinder);
                        writer.WriteValueSeparator();
                        var (CallingContext,CSharpArgumentInfos)=v1.GetBinder();
                        writer.WriteType(CallingContext);
                        writer.WriteValueSeparator();
                        writer.WriteArray(CSharpArgumentInfos,Resolver);
                        writer.WriteValueSeparator();
                        writer.WriteType(v1.ReturnType);
                        writer.WriteValueSeparator();
                        writer.WriteString(v1.Name);
                        writer.WriteValueSeparator();
                        Expression.Write(ref writer,value.Arguments[0],Resolver);
                        break;
                    }
                    case InvokeBinder v1:{
                        WriteBinderType(ref writer,BinderType.InvokeBinder);
                        writer.WriteValueSeparator();
                        var (CallingContext,CSharpArgumentInfos)=v1.GetBinder();
                        writer.WriteType(CallingContext);
                        writer.WriteValueSeparator();
                        writer.WriteArray(CSharpArgumentInfos,Resolver);
                        writer.WriteValueSeparator();
                        writer.WriteType(v1.ReturnType);
                        writer.WriteValueSeparator();
                        writer.WriteCollection(value.Arguments,Resolver);
                        break;
                    }
                    case InvokeMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.InvokeMemberBinder);
                        writer.WriteValueSeparator();
                        var (CallingContext, CSharpArgumentInfos, Flags)=v1.GetBinder();
                        writer.WriteType(CallingContext);
                        writer.WriteValueSeparator();
                        writer.WriteArray(CSharpArgumentInfos,Resolver);
                        writer.WriteValueSeparator();
                        writer.WriteInt32(Flags);
                        writer.WriteValueSeparator();
                        writer.WriteType(v0.ReturnType);
                        writer.WriteValueSeparator();
                        writer.WriteString(v1.Name);
                        writer.WriteValueSeparator();
                        writer.WriteCollection(value.Arguments,Resolver);
                        break;
                    }
                    case SetIndexBinder v1:{
                        WriteBinderType(ref writer,BinderType.SetIndexBinder);
                        writer.WriteValueSeparator();
                        var (CallingContext,CSharpArgumentInfos)=v1.GetBinder();
                        writer.WriteType(CallingContext);
                        writer.WriteValueSeparator();
                        writer.WriteArray(CSharpArgumentInfos,Resolver);
                        writer.WriteValueSeparator();
                        writer.WriteType(v0.ReturnType);
                        writer.WriteValueSeparator();
                        writer.WriteCollection(value.Arguments,Resolver);
                        break;
                    }
                    case SetMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.SetMemberBinder);
                        writer.WriteValueSeparator();
                        var (CallingContext,CSharpArgumentInfos)=v1.GetBinder();
                        writer.WriteType(CallingContext);
                        writer.WriteValueSeparator();
                        writer.WriteArray(CSharpArgumentInfos,Resolver);
                        writer.WriteValueSeparator();
                        writer.WriteType(v0.ReturnType);
                        writer.WriteValueSeparator();
                        writer.WriteString(v1.Name);
                        writer.WriteValueSeparator();
                        var Arguments=value.Arguments;
                        Debug.Assert(Arguments.Count==2);
                        Expression.Write(ref writer,Arguments[0],Resolver);
                        writer.WriteValueSeparator();
                        Expression.Write(ref writer,Arguments[1],Resolver);
                        break;
                    }
                    //case UnaryOperationBinder v1:
                    default:{
                        var v1=(UnaryOperationBinder)v0;
                        WriteBinderType(ref writer,BinderType.UnaryOperationBinder);
                        writer.WriteValueSeparator();
                        var (CallingContext,CSharpArgumentInfos)=v1.GetBinder();
                        writer.WriteType(CallingContext);
                        writer.WriteValueSeparator();
                        writer.WriteArray(CSharpArgumentInfos,Resolver);
                        writer.WriteValueSeparator();
                        writer.WriteType(v0.ReturnType);
                        writer.WriteValueSeparator();
                        writer.WriteNodeType(v1.Operation);
                        writer.WriteValueSeparator();
                        Expression.Write(ref writer,value.Arguments[0],Resolver);
                        break;
                    }
                }
                break;
            }
        }
        static void WriteBinderType(ref Writer writer,BinderType value)=>writer.WriteByte((byte)value);
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteBeginArray();
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateWrite(ref writer, value,Resolver);
        writer.WriteEndArray(); 
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        PrivateWrite(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    
    
    
    
    
    
    
    
    internal static T Read(ref Reader reader,O Resolver){
        T value;
        var BinderType=(BinderType)reader.ReadByte();
        reader.ReadIsValueSeparatorWithVerify();
        switch(BinderType){
            case BinderType.BinaryOperationBinder:{
                var context=reader.ReadType();
                reader.ReadIsValueSeparatorWithVerify();
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>(Resolver);
                reader.ReadIsValueSeparatorWithVerify();
                var returnType=reader.ReadType();
                reader.ReadIsValueSeparatorWithVerify();
                var operation=reader.ReadNodeType();
                reader.ReadIsValueSeparatorWithVerify();
                var left=Expression.Read(ref reader,Resolver);
                reader.ReadIsValueSeparatorWithVerify();
                var right=Expression.Read(ref reader,Resolver);
                value=Expressions.Expression.Dynamic(
                    RuntimeBinder.Binder.BinaryOperation(
                        RuntimeBinder.CSharpBinderFlags.None,
                        operation,
                        context,
                        argumentInfo
                    ),
                    returnType,
                    left,
                    right
                );
                break;
            }
            case BinderType.ConvertBinder:{
                var context=reader.ReadType();
                reader.ReadIsValueSeparatorWithVerify();
                var returnType=reader.ReadType();
                reader.ReadIsValueSeparatorWithVerify();
                var type=reader.ReadType();
                reader.ReadIsValueSeparatorWithVerify();
                var Explicit=reader.ReadBoolean();
                reader.ReadIsValueSeparatorWithVerify();
                var operand=Expression.Read(ref reader,Resolver);
                value=Expressions.Expression.Dynamic(
                    RuntimeBinder.Binder.Convert(
                        Explicit?RuntimeBinder.CSharpBinderFlags.ConvertExplicit:RuntimeBinder.CSharpBinderFlags.None,
                        type,
                        context
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
                var context=reader.ReadType();
                reader.ReadIsValueSeparatorWithVerify();
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>(Resolver);
                reader.ReadIsValueSeparatorWithVerify();
                var returnType=reader.ReadType();
                reader.ReadIsValueSeparatorWithVerify();
                var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
                value=Expressions.Expression.Dynamic(
                    RuntimeBinder.Binder.GetIndex(
                        RuntimeBinder.CSharpBinderFlags.None,
                        context,
                        argumentInfo
                    ),
                    returnType,
                    arguments
                );
                break;
            }
            case BinderType.GetMemberBinder:{
                var context=reader.ReadType();
                reader.ReadIsValueSeparatorWithVerify();
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>(Resolver);
                reader.ReadIsValueSeparatorWithVerify();
                var returnType=reader.ReadType();
                reader.ReadIsValueSeparatorWithVerify();
                var name=reader.ReadString();
                reader.ReadIsValueSeparatorWithVerify();
                var operand=Expression.Read(ref reader,Resolver);
                value=Expressions.Expression.Dynamic(
                    RuntimeBinder.Binder.GetMember(
                        RuntimeBinder.CSharpBinderFlags.None,
                        name,
                        context,
                        argumentInfo
                    ),
                    returnType,
                    operand
                );
                break;
            }
            case BinderType.InvokeBinder:{
                var context=reader.ReadType();
                reader.ReadIsValueSeparatorWithVerify();
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>(Resolver);
                reader.ReadIsValueSeparatorWithVerify();
                var returnType=reader.ReadType();
                reader.ReadIsValueSeparatorWithVerify();
                var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
                value=Expressions.Expression.Dynamic(
                    RuntimeBinder.Binder.Invoke(
                        RuntimeBinder.CSharpBinderFlags.None,
                        context,
                        argumentInfo
                    ),
                    returnType,
                    arguments
                );
                break;
            }
            case BinderType.InvokeMemberBinder:{
                var context=reader.ReadType();
                reader.ReadIsValueSeparatorWithVerify();
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>(Resolver);
                reader.ReadIsValueSeparatorWithVerify();
                var flags=(RuntimeBinder.CSharpBinderFlags)reader.ReadInt32();
                reader.ReadIsValueSeparatorWithVerify();
                var returnType=reader.ReadType();
                reader.ReadIsValueSeparatorWithVerify();
                var name=reader.ReadString();
                reader.ReadIsValueSeparatorWithVerify();
                var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
                value=Expressions.Expression.Dynamic(
                    RuntimeBinder.Binder.InvokeMember(
                        flags,
                        name,
                        null,
                        context,
                        argumentInfo
                    ),
                    returnType,
                    arguments
                );
                break;
            }
            case BinderType.SetIndexBinder:{
                var context=reader.ReadType();
                reader.ReadIsValueSeparatorWithVerify();
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>(Resolver);
                reader.ReadIsValueSeparatorWithVerify();
                var returnType=reader.ReadType();
                reader.ReadIsValueSeparatorWithVerify();
                var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
                value=Expressions.Expression.Dynamic(
                    RuntimeBinder.Binder.SetIndex(
                        RuntimeBinder.CSharpBinderFlags.None,
                        context,
                        argumentInfo
                    ),
                    returnType,
                    arguments
                );
                break;
            }
            case BinderType.SetMemberBinder:{
                var context=reader.ReadType();
                reader.ReadIsValueSeparatorWithVerify();
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>(Resolver);
                reader.ReadIsValueSeparatorWithVerify();
                var returnType=reader.ReadType();
                reader.ReadIsValueSeparatorWithVerify();
                var name=reader.ReadString();
                reader.ReadIsValueSeparatorWithVerify();
                var left=Expression.Read(ref reader,Resolver);
                reader.ReadIsValueSeparatorWithVerify();
                var right=Expression.Read(ref reader,Resolver);
                value=Expressions.Expression.Dynamic(
                    RuntimeBinder.Binder.SetMember(
                        RuntimeBinder.CSharpBinderFlags.None,
                        name,
                        context,
                        argumentInfo
                    ),
                    returnType,
                    left,
                    right
                );
                break;
            }
            case BinderType.UnaryOperationBinder:{
                var context=reader.ReadType();
                reader.ReadIsValueSeparatorWithVerify();
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>(Resolver);
                reader.ReadIsValueSeparatorWithVerify();
                var returnType=reader.ReadType();
                reader.ReadIsValueSeparatorWithVerify();
                var operation=reader.ReadNodeType();
                reader.ReadIsValueSeparatorWithVerify();
                var operand=Expression.Read(ref reader,Resolver);
                value=Expressions.Expression.Dynamic(
                    RuntimeBinder.Binder.UnaryOperation(
                        RuntimeBinder.CSharpBinderFlags.None,
                        operation,
                        context,
                        argumentInfo
                    ),
                    returnType,
                    operand
                );
                break;
            }
            default:throw new ArgumentOutOfRangeException(BinderType.ToString());
        }
        return value;
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
