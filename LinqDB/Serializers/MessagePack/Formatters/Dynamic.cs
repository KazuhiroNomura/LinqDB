using System.Dynamic;
using System.Diagnostics;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.DynamicExpression;
public class Dynamic:IMessagePackFormatter<T> {
    public static readonly Dynamic Instance=new();
    private static void PrivateWriteArrayHeader(ref Writer writer,T value,int offset){
        switch(value.Binder){
            case DynamicMetaObjectBinder v0:{
                switch(v0){
                    case BinaryOperationBinder:writer.WriteArrayHeader(offset+5);break;
                    case ConvertBinder        :writer.WriteArrayHeader(offset+4);break;
                    //case CreateInstanceBinder :writer.WriteArrayHeader(offset+3);break;
                    //case DeleteIndexBinder    :writer.WriteArrayHeader(offset+3);break;
                    //case DeleteMemberBinder   :writer.WriteArrayHeader(offset+2);break;
                    case GetIndexBinder       :writer.WriteArrayHeader(offset+3);break;
                    case GetMemberBinder      :writer.WriteArrayHeader(offset+4);break;
                    case InvokeBinder         :writer.WriteArrayHeader(offset+3);break;
                    case InvokeMemberBinder   :writer.WriteArrayHeader(offset+7);break;
                    case SetIndexBinder       :writer.WriteArrayHeader(offset+3);break;
                    case SetMemberBinder      :writer.WriteArrayHeader(offset+5);break;
                    case UnaryOperationBinder :writer.WriteArrayHeader(offset+4);break;
                }
                break;
            }
        }
    }
    private static void PrivateWrite(ref Writer writer,T value,O Resolver){
        switch(value.Binder){
            case DynamicMetaObjectBinder v0:{
                switch(v0){
                    case BinaryOperationBinder v1:{
                        WriteBinderType(ref writer,BinderType.BinaryOperationBinder);
                        
                        var (CallingContext, CSharpArgumentInfos)=v1.GetBinder();
                        writer.WriteType(CallingContext);
                        
                        writer.WriteArray(CSharpArgumentInfos,Resolver);

                        writer.WriteType(v0.ReturnType);

                        writer.WriteNodeType(v1.Operation);
                        
                        var Arguments=value.Arguments;
                        Debug.Assert(Arguments.Count==2);
                        Expression.Write(ref writer,Arguments[0],Resolver);
                        
                        Expression.Write(ref writer,Arguments[1],Resolver);
                        break;
                    }
                    case ConvertBinder v1:{
                        WriteBinderType(ref writer,BinderType.ConvertBinder);
                        
                        Debug.Assert(v0.ReturnType==v1.Type);
                        var CallingContext=v1.GetBinder();
                        writer.WriteType(CallingContext);
                        
                        writer.WriteType(value.Type);
                        
                        writer.WriteType(v1.Type);
                        
                        writer.WriteBoolean(v1.Explicit);
                        
                        Debug.Assert(value.Arguments.Count==1);
                        Expression.Write(ref writer,value.Arguments[0],Resolver);
                        break;
                    }
                    //case CreateInstanceBinder v1:{
                    //    WriteBinderType(ref writer,BinderType.CreateInstanceBinder);
                        
                    //    var (CallingContext, CSharpArgumentInfos, Flags)=v1.GetBinder();
                    //    writer.WriteType(CallingContext);
                        
                    //    writer.WriteArray(CSharpArgumentInfos,Resolver);
                        
                    //    writer.WriteInt32(Flags);
                    //    break;
                    //}
                    //case DeleteIndexBinder v1:{
                    //    WriteBinderType(ref writer,BinderType.DeleteIndexBinder);
                        
                    //    writer.WriteInt32(v1.CallInfo.ArgumentCount);
                        
                    //    writer.WriteCollection(v1.CallInfo.ArgumentNames,Resolver);
                    //    break;
                    //}
                    
                    //case DeleteMemberBinder v1:{
                    //    WriteBinderType(ref writer,BinderType.DeleteMemberBinder);
                        
                    //    writer.Write(v1.Name);
                    //    break;
                    //}
                    
                    
                    
                    
                    case GetIndexBinder v1:{
                        WriteBinderType(ref writer,BinderType.GetIndexBinder);
                        
                        var (CallingContext,CSharpArgumentInfos)=v1.GetBinder();
                        writer.WriteType(CallingContext);
                        
                        writer.WriteArray(CSharpArgumentInfos,Resolver);
                        
                        writer.WriteType(v0.ReturnType);
                        
                        writer.WriteCollection(value.Arguments,Resolver);
                        break;
                    }
                    case GetMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.GetMemberBinder);
                        
                        var (CallingContext,CSharpArgumentInfos)=v1.GetBinder();
                        writer.WriteType(CallingContext);
                        
                        writer.WriteArray(CSharpArgumentInfos,Resolver);
                        
                        writer.WriteType(v0.ReturnType);
                        
                        writer.Write(v1.Name);
                        
                        Expression.Write(ref writer,value.Arguments[0],Resolver);
                        break;
                    }
                    case InvokeBinder v1:{
                        WriteBinderType(ref writer,BinderType.InvokeBinder);
                        
                        var (CallingContext,CSharpArgumentInfos)=v1.GetBinder();
                        writer.WriteType(CallingContext);
                        
                        writer.WriteArray(CSharpArgumentInfos,Resolver);

                        writer.WriteType(v0.ReturnType);

                        writer.WriteCollection(value.Arguments,Resolver);
                        break;
                    }
                    case InvokeMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.InvokeMemberBinder);

                        var (CallingContext, CSharpArgumentInfos, Flags)=v1.GetBinder();
                        writer.WriteType(CallingContext);

                        writer.WriteArray(CSharpArgumentInfos,Resolver);

                        writer.WriteInt32(Flags);

                        writer.WriteType(v0.ReturnType);

                        writer.Write(v1.Name);

                        writer.WriteCollection(value.Arguments,Resolver);
                        break;
                    }
                    case SetIndexBinder v1:{
                        WriteBinderType(ref writer,BinderType.SetIndexBinder);
                        
                        var (CallingContext,CSharpArgumentInfos)=v1.GetBinder();
                        writer.WriteType(CallingContext);

                        writer.WriteArray(CSharpArgumentInfos,Resolver);
                        
                        writer.WriteType(v0.ReturnType);
                        
                        writer.WriteCollection(value.Arguments,Resolver);
                        break;
                    }
                    case SetMemberBinder v1:{
                        WriteBinderType(ref writer,BinderType.SetMemberBinder);
                        
                        var (CallingContext,CSharpArgumentInfos)=v1.GetBinder();
                        writer.WriteType(CallingContext);
                        
                        writer.WriteArray(CSharpArgumentInfos,Resolver);

                        writer.WriteType(v0.ReturnType);
                        
                        writer.Write(v1.Name);
                        
                        var Arguments=value.Arguments;
                        Debug.Assert(Arguments.Count==2);
                        Expression.Write(ref writer,Arguments[0],Resolver);
                        
                        Expression.Write(ref writer,Arguments[1],Resolver);
                        break;
                    }
                    //case UnaryOperationBinder v1:
                    default:{
                        var v1=(UnaryOperationBinder)v0;
                        WriteBinderType(ref writer,BinderType.UnaryOperationBinder);
                        
                        var (CallingContext,CSharpArgumentInfos)=v1.GetBinder();
                        writer.WriteType(CallingContext);

                        writer.WriteArray(CSharpArgumentInfos,Resolver);

                        writer.WriteType(v0.ReturnType);
                        
                        writer.WriteNodeType(v1.Operation);
                        
                        Expression.Write(ref writer,value.Arguments[0],Resolver);
                        break;
                    }
                }
                break;
            }
        }
        static void WriteBinderType(ref Writer writer,BinderType value)=>writer.WriteInt8((sbyte)value);
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        PrivateWriteArrayHeader(ref writer,value,1);
        writer.WriteNodeType(Expressions.ExpressionType.Dynamic);
        
        PrivateWrite(ref writer,value,Resolver);
        
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        PrivateWriteArrayHeader(ref writer,value,0);
        PrivateWrite(ref writer,value,Resolver);
        
    }
    internal static T Read(ref Reader reader,O Resolver){
        T value;
        var BinderType=(BinderType)reader.ReadSByte();
            
        switch(BinderType){
            case BinderType.BinaryOperationBinder:{
                var context=reader.ReadType();
                
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>(Resolver);
                
                var returnType=reader.ReadType();
                
                var operation=reader.ReadNodeType();
                
                var left=Expression.Read(ref reader,Resolver);
                
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

                var returnType=reader.ReadType();
                
                var type=reader.ReadType();
                
                var @explicit=reader.ReadBoolean();
                
                var operand=Expression.Read(ref reader,Resolver);
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
                
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>(Resolver);
                
                var returnType=reader.ReadType();
                
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
                
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>(Resolver);

                var returnType=reader.ReadType();

                var name=reader.ReadString();

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
                
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>(Resolver);
                
                var returnType=reader.ReadType();
                
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
                
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>(Resolver);
                
                var flags=(RuntimeBinder.CSharpBinderFlags)reader.ReadInt32();
                
                var returnType=reader.ReadType();
                
                var name=reader.ReadString();
                
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
                
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>(Resolver);
                
                var returnType=reader.ReadType();
                
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
                
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>(Resolver);
                
                var returnType=reader.ReadType();
                
                var name=reader.ReadString();
                
                var left=Expression.Read(ref reader,Resolver);
                
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
            //case BinderType.UnaryOperationBinder:
            default:{
                Debug.Assert(BinderType==BinderType.UnaryOperationBinder);
                var context=reader.ReadType();
                
                var argumentInfo=reader.ReadArray<RuntimeBinder.CSharpArgumentInfo>(Resolver);
                
                var returnType=reader.ReadType();
                
                var operation=reader.ReadNodeType();
                
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
        }
        return value;
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        return Read(ref reader,Resolver);


    }
}