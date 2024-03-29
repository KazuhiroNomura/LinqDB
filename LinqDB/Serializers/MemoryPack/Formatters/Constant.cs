﻿
using MemoryPack;
using System.Buffers;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Others;


using Reader = MemoryPackReader;
using T = Expressions.ConstantExpression;
public class Constant:MemoryPackFormatter<T> {
    public static readonly Constant Instance=new();
    private static void PrivateWrite<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        writer.WriteType(value!.Type);
        
        Object.WriteNullable(ref writer,value.Value);
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{

        writer.WriteNodeType(Expressions.ExpressionType.Constant);
        
        PrivateWrite(ref writer,value);
        
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;

        PrivateWrite(ref writer,value);
        
    }
    internal static T Read(ref Reader reader) {
        var type=reader.ReadType();
        
        var value=Object.ReadNullable(ref reader);
        return Expressions.Expression.Constant(value,type);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        
        value=Read(ref reader);
        
        
    }
}
