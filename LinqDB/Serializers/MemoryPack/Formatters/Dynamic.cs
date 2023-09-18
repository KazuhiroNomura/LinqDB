using System;
using System.Dynamic;
using System.Diagnostics;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;

using MemoryPack;
using System.Buffers;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;

using Reader = MemoryPackReader;
using T = Expressions.DynamicExpression;
using static Common;
public class Dynamic:MemoryPackFormatter<T> {
    public static readonly Dynamic Instance=new();
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    



    
    
    
    
    private static void PrivateWrite<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{
        switch(value.Binder){
            case DynamicMetaObjectBinder v0:{
                switch(v0){
                    case BinaryOperationBinder v1:{
                        WriteBinderType(ref writer,BinderType.BinaryOperationBinder);
                        
                        var (CallingContext, CSharpArgumentInfos)=GetBinder(v1);
                        writer.WriteType(CallingContext);
                        
                        writer.WriteArray(CSharpArgumentInfos);

                        writer.WriteType(v0.ReturnType);
                        
                        writer.WriteNodeType(v1.Operation);
                        
                        var Arguments=value.Arguments;
                        Debug.Assert(Arguments.Count==2);
                        Expression.Write(ref writer,Arguments[0]);
                        
                        Expression.Write(ref writer,Arguments[1]);
                        break;
                    }
                    case ConvertBinder v1:{
                        WriteBinderType(ref writer,BinderType.ConvertBinder);
                        
                        Debug.Assert(v0.ReturnType==v1.Type);
                        var CallingContext=GetBinder(v1);
                        writer.WriteType(CallingContext);
                        
                        writer.WriteType(value.Type);

                        writer.WriteType(v1.Type);
                        
                        writer.WriteBoolean(v1.Explicit);
                        
                        Debug.Assert(value.Arguments.Count==1);
                        Expression.Write(ref writer,value.Arguments[0]);
                        break;
                    }
                    case CreateInstanceBinder v1:{
                        WriteBinderType(ref writer,BinderType.CreateInstanceBinder);
                        
                        var (CallingContext, CSharpArgumentInfos, Flags)=GetBinder(v1);
                        writer.WriteType(CallingContext);
                        
                        writer.WriteArray(CSharpArgumentInfos);
                        
                        writer.WriteVarInt(Flags);
                        break;
                    }
                    case DeleteIndexBinder v1:{
                        WriteBinderType(ref writer,BinderType.DeleteIndexBinder);
                        var (CallingContext, CSharpArgumentInfos, Flags)=GetBinder(v1);
                        writer.WriteType(CallingContext);
                        writer.WriteArray(CSharpArgumentInfos);
                        writer.WriteVarInt(Flags);
                        writer.WriteType(v0.ReturnType);
                        break;
                    }
                    case DeleteMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.DeleteMemberBinder);
                        var (CallingContext, CSharpArgumentInfos, Flags)=GetBinder(v1);
                        writer.WriteType(CallingContext);
                        writer.WriteArray(CSharpArgumentInfos);
                        writer.WriteVarInt(Flags);
                        writer.WriteType(v0.ReturnType);
                        writer.WriteString(v1.Name);
                        break;
                    }
                    case GetIndexBinder v1:{
                        WriteBinderType(ref writer,BinderType.GetIndexBinder);
                        
                        var (CallingContext,CSharpArgumentInfos)=GetBinder(v1);
                        writer.WriteType(CallingContext);
                        
                        writer.WriteArray(CSharpArgumentInfos);
                        
                        writer.WriteType(v0.ReturnType);
                        
                        writer.WriteCollection(value.Arguments);
                        break;
                    }
                    case GetMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.GetMemberBinder);

                        var (CallingContext,CSharpArgumentInfos)=GetBinder(v1);
                        writer.WriteType(CallingContext);
                        
                        writer.WriteArray(CSharpArgumentInfos);
                        
                        writer.WriteType(v0.ReturnType);
                        
                        writer.WriteString(v1.Name);
                        
                        Expression.Write(ref writer,value.Arguments[0]);
                        break;
                    }
                    case InvokeBinder v1:{
                        WriteBinderType(ref writer,BinderType.InvokeBinder);
                        
                        var (CallingContext,CSharpArgumentInfos)=GetBinder(v1);
                        writer.WriteType(CallingContext);
                        
                        writer.WriteArray(CSharpArgumentInfos);
                        
                        writer.WriteType(v0.ReturnType);
                        
                        writer.WriteCollection(value.Arguments);
                        break;
                    }
                    case InvokeMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.InvokeMemberBinder);
                        
                        var (CallingContext,CSharpArgumentInfos,Flags)=GetBinder(v1);
                        writer.WriteType(CallingContext);
                        
                        writer.WriteArray(CSharpArgumentInfos);
                        
                        writer.WriteVarInt(Flags);
                        
                        writer.WriteType(v0.ReturnType);
                        
                        writer.WriteString(v1.Name);
                        
                        writer.WriteCollection(value.Arguments);
                        break;
                    }
                    case SetIndexBinder v1:{
                        WriteBinderType(ref writer,BinderType.SetIndexBinder);
                        
                        var (CallingContext,CSharpArgumentInfos)=GetBinder(v1);
                        writer.WriteType(CallingContext);
                        
                        writer.WriteArray(CSharpArgumentInfos);
                        
                        writer.WriteType(v0.ReturnType);
                        
                        writer.WriteCollection(value.Arguments);
                        break;
                    }
                    case SetMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.SetMemberBinder);
                        
                        var (CallingContext,CSharpArgumentInfos)=GetBinder(v1);
                        writer.WriteType(CallingContext);
                        
                        writer.WriteArray(CSharpArgumentInfos);
                        
                        writer.WriteType(v0.ReturnType);
                        
                        writer.WriteString(v1.Name);
                        
                        var Arguments=value.Arguments;
                        Debug.Assert(Arguments.Count==2);
                        Expression.Write(ref writer,Arguments[0]);
                        
                        Expression.Write(ref writer,Arguments[1]);
                        break;
                    }
                    case UnaryOperationBinder v1:{
                        WriteBinderType(ref writer,BinderType.UnaryOperationBinder);

                        var (CallingContext,CSharpArgumentInfos)=GetBinder(v1);
                        writer.WriteType(CallingContext);

                        writer.WriteArray(CSharpArgumentInfos);

                        writer.WriteType(v0.ReturnType);

                        writer.WriteNodeType(v1.Operation);

                        Expression.Write(ref writer,value.Arguments[0]);
                        break;
                    }
                }
                break;
            }
        }
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(Expressions.ExpressionType.Dynamic);
        PrivateWrite(ref writer,value);
    }
    private static void WriteBinderType<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,BinderType value) where TBufferWriter:IBufferWriter<byte>{
        writer.WriteVarInt((byte)value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        PrivateWrite(ref writer,value);
    }
    private static BinderType ReadBinderType(ref Reader reader){
        return (BinderType)reader.ReadVarIntByte();
    }
    
    
    
    
    
    
    
    
    internal static T Read(ref Reader reader){
        T value;
        var BinderType=ReadBinderType(ref reader);
        
        switch(BinderType){
            case BinderType.BinaryOperationBinder:{
                var context=reader.ReadType();
                
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>();
                
                var returnType=reader.ReadType();
                
                var operation=reader.ReadNodeType();
                
                var left=Expression.Read(ref reader);
                
                var right=Expression.Read(ref reader);
                value=Expressions.Expression.Dynamic(
                    RuntimeBinder.Binder.BinaryOperation(
                        RuntimeBinder.CSharpBinderFlags.None,
                        operation,
                        context,
                        argumentInfo!
                    ),
                    returnType,
                    left,
                    right
                );
                break;
            }
            case BinderType.ConvertBinder:{
                var context=reader.ReadType();
                
                var returnType=reader.ReadType();
                
                var type=reader.ReadType();
                
                var @explicit=reader.ReadBoolean();
                
                var operand=Expression.Read(ref reader);
                value=Expressions.Expression.Dynamic(
                    RuntimeBinder.Binder.Convert(
                        @explicit?RuntimeBinder.CSharpBinderFlags.ConvertExplicit:RuntimeBinder.CSharpBinderFlags.None,
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
                
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>();
                
                var returnType=reader.ReadType();
                
                var arguments=reader.ReadArray<Expressions.Expression>();
                value=Expressions.Expression.Dynamic(
                    RuntimeBinder.Binder.GetIndex(
                        RuntimeBinder.CSharpBinderFlags.None,
                        context,
                        argumentInfo!
                    ),
                    returnType,
                    arguments!
                );
                break;
            }
            case BinderType.GetMemberBinder:{
                var context=reader.ReadType();
                
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>();
                
                var returnType=reader.ReadType();
                
                var name=reader.ReadString();
                
                var operand=Expression.Read(ref reader);
                value=Expressions.Expression.Dynamic(
                    RuntimeBinder.Binder.GetMember(
                        RuntimeBinder.CSharpBinderFlags.None,
                        name,
                        context,
                        argumentInfo!
                    ),
                    returnType,
                    operand
                );
                break;
            }
            case BinderType.InvokeBinder:{
                var context=reader.ReadType();
                
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>();
                
                var returnType=reader.ReadType();
                
                var arguments=reader.ReadArray<Expressions.Expression>();
                value=Expressions.Expression.Dynamic(
                    RuntimeBinder.Binder.Invoke(
                        RuntimeBinder.CSharpBinderFlags.None,
                        context,
                        argumentInfo!
                    ),
                    returnType,
                    arguments!
                );
                break;
            }
            case BinderType.InvokeMemberBinder:{
                var context=reader.ReadType();
                
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>();
                
                var flags=(RuntimeBinder.CSharpBinderFlags)reader.ReadVarIntInt32();
                
                var returnType=reader.ReadType();
                
                var name=reader.ReadString();
                
                var arguments=reader.ReadArray<Expressions.Expression>();
                value=Expressions.Expression.Dynamic(
                    RuntimeBinder.Binder.InvokeMember(
                        flags,
                        name,
                        null,
                        context,
                        argumentInfo!
                    ),
                    returnType,
                    arguments!
                );
                break;
            }
            case BinderType.SetIndexBinder:{
                var context=reader.ReadType();
                
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>();
                
                var returnType=reader.ReadType();
                
                var arguments=reader.ReadArray<Expressions.Expression>();
                value=Expressions.Expression.Dynamic(
                    RuntimeBinder.Binder.SetIndex(
                        RuntimeBinder.CSharpBinderFlags.None,
                        context,
                        argumentInfo!
                    ),
                    returnType,
                    arguments!
                );
                break;
            }
            case BinderType.SetMemberBinder:{
                var context=reader.ReadType();
                
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>();
                
                var returnType=reader.ReadType();
                
                var name=reader.ReadString();
                
                var left=Expression.Read(ref reader);
                
                var right=Expression.Read(ref reader);
                value=Expressions.Expression.Dynamic(
                    RuntimeBinder.Binder.SetMember(
                        RuntimeBinder.CSharpBinderFlags.None,
                        name,
                        context,
                        argumentInfo!
                    ),
                    returnType,
                    left,
                    right
                );
                break;
            }
            case BinderType.UnaryOperationBinder:{
                var context=reader.ReadType();
                
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>();
                
                var returnType=reader.ReadType();
                
                var operation=reader.ReadNodeType();
                
                var operand=Expression.Read(ref reader);
                value=Expressions.Expression.Dynamic(
                    RuntimeBinder.Binder.UnaryOperation(
                        RuntimeBinder.CSharpBinderFlags.None,
                        operation,
                        context,
                        argumentInfo!
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
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        value=Read(ref reader);



    }
}
