using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Buffers;

using System.Reflection;


using MemoryPack;

using Expressions = System.Linq.Expressions;
using System.Collections.Concurrent;

namespace LinqDB.Serializers.MemoryPack;


using Reader = MemoryPackReader;
internal static class Extension{
    public static readonly MethodInfo SerializeMethod = typeof(Extension).GetMethod(nameof(Serialize),BindingFlags.Static|BindingFlags.NonPublic)!;
    private static void Serialize<TBufferWriter, TValue>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref TValue? value) where TBufferWriter :IBufferWriter<byte>{
        if(MemoryPackFormatterProvider.IsRegistered<TValue>())
            writer.WriteValue(value);
        else{
            var Formatter=FormatterResolver.GetRegisteredFormatter<TValue>();
            if(Formatter is not null) writer.Write(Formatter,value);
        }
    }    
    public static readonly MethodInfo DeserializeMethod = typeof(Extension).GetMethod(nameof(Deserialize),BindingFlags.Static|BindingFlags.NonPublic)!;
    private static void Deserialize<TValue>(ref Reader reader,scoped ref TValue? value) {
        if(MemoryPackFormatterProvider.IsRegistered<TValue>())
            value=reader.ReadValue<TValue>();
        else{
            var Formatter=FormatterResolver.GetRegisteredFormatter<TValue>();
            if(Formatter is not null) value=reader.Read(Formatter);
        }
    }
    public static void WriteType<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,Type value) where TBufferWriter:IBufferWriter<byte> =>writer.WriteString(value.TypeString());
    public static Type ReadType(this ref Reader reader)=>reader.ReadString().StringType();
    public static void WriteBoolean<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,bool value)where TBufferWriter :IBufferWriter<byte> =>writer.WriteVarInt((byte)(value?1:0));
    public static bool ReadBoolean(this ref Reader reader)=>reader.ReadVarIntByte()!=0;
    public static void WriteNodeType<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,Expressions.ExpressionType NodeType)where TBufferWriter :IBufferWriter<byte> =>writer.WriteVarInt((byte)NodeType);
    public static void WriteNodeType<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression Expression)where TBufferWriter :IBufferWriter<byte> =>writer.WriteVarInt((byte)Expression.NodeType);
    public static Expressions.ExpressionType ReadNodeType(this ref Reader reader)=>(Expressions.ExpressionType)reader.ReadVarIntByte();
    public static bool TryWriteNil<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,object? value)where TBufferWriter:IBufferWriter<byte>{
        if(value is null){
            writer.WriteVarInt(0);
            return true;
        }
        writer.WriteVarInt(1);
        return false;
    }
    public static bool TryReadNil(this ref Reader reader){
        var header=reader.ReadVarIntByte();
        if(header==0) return true;
        System.Diagnostics.Debug.Assert(header==1);
        return false;
    }

    
    
    private static class StaticReadOnlyCollectionFormatter<T>{
        public static readonly global::MemoryPack.Formatters.ReadOnlyCollectionFormatter<T> Formatter=new();
    }
    internal static void WriteCollection<T,TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,ReadOnlyCollection<T>? value)where TBufferWriter :IBufferWriter<byte> =>
    	writer.Write(StaticReadOnlyCollectionFormatter<T>.Formatter!,value!);










    internal static void Serialize宣言Parameters<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,ReadOnlyCollection<Expressions.ParameterExpression>value)where TBufferWriter :IBufferWriter<byte> {
        writer.WriteVarInt(value.Count);
        var Serializer=writer.Serializer();
        var Serializer_Parameters=Serializer.Parameters;
        var Serializer_ラムダ跨ぎParameters=Serializer.ラムダ跨ぎParameters;
        foreach(var Parameter in value){
            var index0=Serializer_Parameters.LastIndexOf(Parameter);
            writer.WriteVarInt(index0);
            if(index0<0){
                var index1=Serializer_ラムダ跨ぎParameters.LastIndexOf(Parameter);
                writer.WriteVarInt(index1);
                if(index1<0){
                    writer.WriteString(Parameter.Name);
                    var Type=Parameter.Type;
                    writer.WriteType(Parameter.IsByRef?Type.MakeByRefType():Type);
                }
            }
        }
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    internal static Expressions.ParameterExpression[]Deserialize宣言Parameters(this ref Reader reader){
        var Count=reader.ReadVarIntInt32();
        var Serializer=reader.Serializer();
        var Serializer_Parameters=Serializer.Parameters;
        var Serializer_ラムダ跨ぎParameters=Serializer.ラムダ跨ぎParameters;
        var Parameters=new Expressions.ParameterExpression[Count];
        for(var a=0;a<Count;a++){
            var index0=reader.ReadVarIntInt32();
            if(index0<0){
                var index1=reader.ReadVarIntInt32();
                if(index1<0){
                    var name=reader.ReadString();

                    var type=reader.ReadType();
                    Parameters[a]=Expressions.Expression.Parameter(type,name);

                } else{
                    Parameters[a]=Serializer_ラムダ跨ぎParameters[index1];
                }
            }else{
                Parameters[a]=Serializer_Parameters[index0];
            }
            
        }
        return Parameters;
    }








    public static void Write<TBufferWriter,T>(this ref MemoryPackWriter<TBufferWriter>writer,IMemoryPackFormatter<T>Formatter,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Formatter.Serialize(ref writer,ref value);
    private static void Write<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter>writer,IMemoryPackFormatter Formatter,object? value)where TBufferWriter:IBufferWriter<byte> =>
        Formatter.Serialize(ref writer,ref value);
        
        
        
        
        
        
        
        
        
        
        
        
    public static void Write<TBufferWriter,T>(this ref MemoryPackWriter<TBufferWriter>writer,T? value_T)where TBufferWriter:IBufferWriter<byte>{
        var Formatter=FormatterResolver.GetRegisteredFormatter<T>();
        if(Formatter is not null){
            Formatter.Serialize(ref writer,ref value_T);
        } else{
            writer.WriteValue(value_T);
        }
    }
    public static void Write<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter>writer,Type type,object? value)where TBufferWriter:IBufferWriter<byte>{
        if(FormatterResolver.GetRegisteredFormatter(type) is IMemoryPackFormatter Formatter) {
            Write(ref writer,Formatter,value);
        } else{
            //Formatter=writer.GetFormatter(type);
            //Formatter.Serialize(ref writer, ref value);
            //Debug.Fail($"{type.FullName} Formatterがない");
            writer.WriteValue(type,value);
        }
    }







    public static T? Read<T>(this ref Reader reader,IMemoryPackFormatter<T>Formatter){
        T? value=default;
        Formatter.Deserialize(ref reader,ref value);
        return value;
    }
    public static T? Read<T>(this ref Reader reader){
        T? value=default;
        var Formatter_T=FormatterResolver.GetRegisteredFormatter<T>();
        if(Formatter_T is not null){
            Formatter_T.Deserialize(ref reader,ref value);
        } else{
            reader.GetFormatter<T>().Deserialize(ref reader,ref value);
        }
        return value;
    }
    public static object? Read(this ref Reader reader,Type type){
        object? value=default;
        if(FormatterResolver.GetRegisteredFormatter(type)is IMemoryPackFormatter Formatter)Formatter.Deserialize(ref reader,ref value);
        else reader.GetFormatter(type).Deserialize(ref reader,ref value);
        return value;
    }
    
    
    
    
    
    
    


    
    


























    public static Serializer Serializer<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer)where TBufferWriter:IBufferWriter<byte> =>
        (Serializer)writer.Options.ServiceProvider!;
    public static Serializer Serializer(this ref Reader reader)=>
        (Serializer)reader.Options.ServiceProvider!;
}
