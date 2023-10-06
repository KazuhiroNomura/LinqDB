using System;
using System.Collections.ObjectModel;
using System.Buffers;
using System.Diagnostics;
using System.Reflection;

using MemoryPack;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack;


using Reader = MemoryPackReader;
public static class Extension{
    public static void WriteType<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter>writer,Type value)where TBufferWriter :IBufferWriter<byte> =>writer.WriteString(value.AssemblyQualifiedName);
    public static Type ReadType(this ref Reader reader)=>System.Type.GetType(reader.ReadString())!;
    public static void WriteBoolean<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,bool value)where TBufferWriter :IBufferWriter<byte> =>writer.WriteVarInt((byte)(value?1:0));
    public static bool ReadBoolean(this ref Reader reader)=>reader.ReadVarIntByte()!=0;
    public static void WriteNodeType<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,Expressions.ExpressionType NodeType)where TBufferWriter :IBufferWriter<byte> =>writer.WriteVarInt((byte)NodeType);
    public static void WriteNodeType<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,Expressions.Expression Expression)where TBufferWriter :IBufferWriter<byte> =>writer.WriteVarInt((byte)Expression.NodeType);
    public static Expressions.ExpressionType ReadNodeType(this ref Reader reader)=>(Expressions.ExpressionType)reader.ReadVarIntByte();
    public static bool TryWriteNil<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,object? value)where TBufferWriter:IBufferWriter<byte>{
        if(value is not null)return false;
        writer.WriteNullObjectHeader();
        return true;
    }
    public static bool TryReadNil(this ref Reader reader){
        if(reader.PeekIsNull()){
            reader.Advance(1);
            return true;
        }
        return false;
    }
    
    
    
    
    
    
    
    private static class StaticReadOnlyCollectionFormatter<T>{
        public static readonly global::MemoryPack.Formatters.ReadOnlyCollectionFormatter<T> Formatter=new();
    }
    internal static void WriteCollection<T,TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,ReadOnlyCollection<T>? value)where TBufferWriter :IBufferWriter<byte> =>
    	StaticReadOnlyCollectionFormatter<T>.Formatter.Serialize(ref writer,ref value!);










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
                    writer.WriteType(Parameter.Type);
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





    public static void Write<TBufferWriter,T>(this ref MemoryPackWriter<TBufferWriter>writer,IMemoryPackFormatter<T>Formatter,T? value)where TBufferWriter:IBufferWriter<byte>{
        Formatter.Serialize(ref writer,ref value);
    }
    
    
    public static void Write<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter>writer,IMemoryPackFormatter Formatter,object? value)where TBufferWriter:IBufferWriter<byte> =>
        Formatter.Serialize(ref writer,ref value);
    public static void Write<TBufferWriter,T>(this ref MemoryPackWriter<TBufferWriter>writer,T? value_T)where TBufferWriter:IBufferWriter<byte>{
        var Formatter_T=FormatterResolver.GetRegisteredFormatter<T>();
        if(Formatter_T is not null){
            Formatter_T.Serialize(ref writer,ref value_T);
            return;
        }
        var Formatter=FormatterResolver.GetRegisteredFormatter(value_T!.GetType());
        if(Formatter is not null){
            object? value=value_T;
            ((IMemoryPackFormatter)Formatter).Serialize(ref writer,ref value);
            return;
        }
        writer.WriteValue(value_T);
    }
    public static void Write<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter>writer,Type type,object value)where TBufferWriter:IBufferWriter<byte>{
        var Formatter=FormatterResolver.GetRegisteredFormatter(type);
        if(Formatter!=null){
            Write(ref writer,(IMemoryPackFormatter)Formatter,value);
        } else{
            writer.WriteValue(type,value);
        }
    }
    public static void Write<TBufferWriter,T>(this ref MemoryPackWriter<TBufferWriter>writer,System.Collections.Generic.IEnumerable<T> value)where TBufferWriter:IBufferWriter<byte>{
        var InstanceMemoryPack=(IMemoryPackFormatter)value.GetType().GetValue("InstanceMemoryPack")!;
        Debug.Assert(InstanceMemoryPack!=null);
        object? value0=value;
        InstanceMemoryPack.Serialize(ref writer,ref value0);
    }
    public static T? Read<T>(this ref Reader reader,IMemoryPackFormatter<T>Formatter_T){
        T? value=default;
        Formatter_T.Deserialize(ref reader,ref value);
        return value;
    }
    public static T? Read<T>(this ref Reader reader){
        var Formatter_T=FormatterResolver.GetRegisteredFormatter<T>();
        if(Formatter_T is not null){
            T? value=default;
            Formatter_T.Deserialize(ref reader,ref value);
            return value;
        }
        var Formatter=FormatterResolver.GetRegisteredFormatter(typeof(T));
        if(Formatter!=null){
            object? value=default;
            ((IMemoryPackFormatter)Formatter).Deserialize(ref reader,ref value);
            return (T?)value;
        } else{
            T? value=default;
            reader.GetFormatter<T>().Deserialize(ref reader,ref value);
            return value;
        }
    }
    public static object? ReadValue(ref Reader reader,IMemoryPackFormatter Formatter){
        object? value=default;
        Formatter.Deserialize(ref reader,ref value);
        return value;
    }
    
    
    
    
    
    
    public static object? Read(this ref Reader reader,Type type){
        object? value=default;
        var Formatter=FormatterResolver.GetRegisteredFormatter(type);
        if(Formatter!=null)((IMemoryPackFormatter)Formatter).Deserialize(ref reader,ref value);
        else reader.GetFormatter(type).Deserialize(ref reader,ref value);
        return value;
    }
    //public static IMemoryPackFormatter RegisterFormatter<TBufferWriter>(this ref  MemoryPackWriter<TBufferWriter> writer,Type type)where TBufferWriter:IBufferWriter<byte>{
    //    var Formatter=writer.GetFormatter(type);
    //    ///errormessag
    //    if(Formatter is not null) return Formatter;
    //    return (IMemoryPackFormatter)FormatterResolver.Get全Formatter(type)!;
    //}
    //public static IMemoryPackFormatter RegisterFormatter(this ref Reader reader,Type type){
    //    var Formatter=reader.GetFormatter(type);
    //    if(Formatter is not null) return Formatter;
    //    return (IMemoryPackFormatter)FormatterResolver.Get全Formatter(type)!;
    //}
    public static Serializer Serializer<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer)where TBufferWriter:IBufferWriter<byte> =>
        (Serializer)writer.Options.ServiceProvider!;
    public static Serializer Serializer(this ref Reader reader)=>
        (Serializer)reader.Options.ServiceProvider!;
    //public static Serializer Container<TContainer>(this ref Reader reader)where TContainer:Container=>
    //    (Serializer)reader.Options.ServiceProvider!;
    private static void Serialize2<TBufferWriter, TValue>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref TValue? value) where TBufferWriter : IBufferWriter<byte> {
        writer.WriteValue(value);
    }
    public static readonly MethodInfo MethodSerialize = typeof(Extension).GetMethod(nameof(Serialize2),BindingFlags.Static|BindingFlags.NonPublic)!;
    private static void Deserialize2<TValue>(ref Reader reader,scoped ref TValue? value) {
        reader.ReadValue(ref value);
    }
    public static readonly MethodInfo MethodDeserialize = typeof(Extension).GetMethod(nameof(Deserialize2),BindingFlags.Static|BindingFlags.NonPublic)!;
}
