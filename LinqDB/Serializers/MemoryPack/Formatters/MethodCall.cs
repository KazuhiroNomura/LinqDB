﻿using MemoryPack;
using System.Buffers;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reflection;


using Reader = MemoryPackReader;
using T = Expressions.MethodCallExpression;
public class MethodCall:MemoryPackFormatter<T> {
    public static readonly MethodCall Instance=new();
    private static void PrivateWrite<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        var method=value!.Method;
        Method.Write(ref writer,method);
        
        if(!method.IsStatic){
            Expression.Write(ref writer,value.Object!);
            
        }
        writer.WriteCollection(value.Arguments);
        
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{

        writer.WriteNodeType(Expressions.ExpressionType.Call);
        
        PrivateWrite(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        
        PrivateWrite(ref writer,value);
    }
    
    
    
    
    
    
    
    
    internal static T Read(ref Reader reader){
        var method= Method.Read(ref reader);
        
        if(method.IsStatic){
            var arguments=reader.ReadArray<Expressions.Expression>();
            return Expressions.Expression.Call(
                method,
                arguments!
            );
        } else{
            var instance= Expression.Read(ref reader);
            
            var arguments=reader.ReadArray<Expressions.Expression>();
            return Expressions.Expression.Call(
                instance,
                method,
                arguments!
            );
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        value=Read(ref reader);
    }
}
