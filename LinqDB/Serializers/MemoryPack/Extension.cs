using System;
using System.Collections.ObjectModel;
using System.Buffers;
using System.Diagnostics;
using System.Reflection;


using MemoryPack;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization;

namespace LinqDB.Serializers.MemoryPack;

using Reader = MemoryPackReader;
internal static class Extension{
    public static readonly MethodInfo SerializeMethod = typeof(Extension).GetMethod(nameof(Serialize),BindingFlags.Static|BindingFlags.NonPublic)!;
    private static void Serialize<TBufferWriter, T>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value) where TBufferWriter :IBufferWriter<byte>{
        var Formatter=FormatterResolver.GetRegisteredFormatter<T>()??writer.GetFormatter<T>();
        writer.Write(Formatter,value);
        //if(MemoryPackFormatterProvider.IsRegistered<T>())
        //    writer.WriteValue(value);
        //else {
        //    var Formatter = FormatterResolver.GetRegisteredFormatter<T>();
        //    if(Formatter is not null) writer.Write(Formatter,value);
        //    else{
        //        writer.GetRegisteredFormatterGetFormatter<T,TBufferWriter>().Serialize(ref writer,ref value);
        //        Debug.Fail($"Formatterがなかった{typeof(T).Name}");
        //    }
        //}
    }    
    public static readonly MethodInfo DeserializeMethod = typeof(Extension).GetMethod(nameof(Deserialize),BindingFlags.Static|BindingFlags.NonPublic)!;
    private static void Deserialize<T>(ref Reader reader,scoped ref T? value){
        var Formatter=FormatterResolver.GetRegisteredFormatter<T>()??reader.GetFormatter<T>();
        value=reader.Read(Formatter);
        //reader.GetRegisteredFormatterGetFormatter<T>().Deserialize(ref reader,ref value);
        //if(MemoryPackFormatterProvider.IsRegistered<T>())
        //    value=reader.ReadValue<T>();
        //else{
        //    var Formatter=FormatterResolver.GetRegisteredFormatter<T>();
        //    if(Formatter is not null) value=reader.Read(Formatter);
        //    else Debug.Fail($"Formatterがなかった{typeof(T).Name}");
        //}
    }
    public static void WriteType<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,Type value) where TBufferWriter:IBufferWriter<byte> =>writer.WriteString(value.TypeString());
    public static Type ReadType(this ref Reader reader)=>reader.ReadString().StringType();
    public static void WriteBoolean<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,bool value)where TBufferWriter :IBufferWriter<byte> =>writer.WriteVarInt((byte)(value?1:0));
    public static bool ReadBoolean(this ref Reader reader)=>reader.ReadVarIntByte()!=0;
    public static void WriteNodeType<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,ExpressionType NodeType)where TBufferWriter :IBufferWriter<byte> =>writer.WriteVarInt((byte)NodeType);
    public static void WriteNodeType<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,Expression Expression)where TBufferWriter :IBufferWriter<byte> =>writer.WriteVarInt((byte)Expression.NodeType);
    public static ExpressionType ReadNodeType(this ref Reader reader)=>(ExpressionType)reader.ReadVarIntByte();
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









    internal static void Serialize宣言Parameters<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,ReadOnlyCollection<ParameterExpression>value)where TBufferWriter :IBufferWriter<byte> {
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
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    internal static ParameterExpression[]Deserialize宣言Parameters(this ref Reader reader){
        var Count=reader.ReadVarIntInt32();
        var Serializer=reader.Serializer();
        var Serializer_Parameters=Serializer.Parameters;
        var Serializer_ラムダ跨ぎParameters=Serializer.ラムダ跨ぎParameters;
        var Parameters=new ParameterExpression[Count];
        for(var a=0;a<Count;a++){
            var index0=reader.ReadVarIntInt32();
            if(index0<0){
                var index1=reader.ReadVarIntInt32();
                if(index1<0){
                    var name=reader.ReadString();

                    var type=reader.ReadType();
                    Parameters[a]=Expression.Parameter(type,name);

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
    public static void Write<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter>writer,IMemoryPackFormatter Formatter,object? value)where TBufferWriter:IBufferWriter<byte> =>
        Formatter.Serialize(ref writer,ref value);
        
        
        
        
        
        
        
        
        
        
    public static void Write<TBufferWriter,T>(this ref MemoryPackWriter<TBufferWriter>writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        writer.GetRegisteredFormatterGetFormatter<T,TBufferWriter>().Serialize(ref writer,ref value);
        //var Formatter=FormatterResolver.GetRegisteredFormatter<T>();
        //if(Formatter is not null){
        //    Formatter.Serialize(ref writer,ref value_T);
        //} else{
        //    writer.WriteValue(value_T);
        //}
    }
    
    
    
    
    
    public static void WriteExpression <TBufferWriter>(this ref MemoryPackWriter<TBufferWriter>writer,Expression             value)where TBufferWriter:IBufferWriter<byte> =>Formatters.Expression.Write           (ref writer,value);
    //public static void WriteType       <TBufferWriter>(this ref MemoryPackWriter<TBufferWriter>writer,Type                   value)where TBufferWriter:IBufferWriter<byte> =>Formatters.Reflection.Type.Write       (ref writer,value);
    public static void WriteConstructor<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter>writer,ConstructorInfo        value)where TBufferWriter:IBufferWriter<byte> =>Formatters.Reflection.Constructor.Write(ref writer,value);
    public static void WriteMethod     <TBufferWriter>(this ref MemoryPackWriter<TBufferWriter>writer,MethodInfo             value)where TBufferWriter:IBufferWriter<byte> =>Formatters.Reflection.Method.Write     (ref writer,value);
    public static void WriteProperty   <TBufferWriter>(this ref MemoryPackWriter<TBufferWriter>writer,PropertyInfo           value)where TBufferWriter:IBufferWriter<byte> =>Formatters.Reflection.Property.Write   (ref writer,value);
    public static void WriteEvent      <TBufferWriter>(this ref MemoryPackWriter<TBufferWriter>writer,EventInfo              value)where TBufferWriter:IBufferWriter<byte> =>Formatters.Reflection.Event.Write      (ref writer,value);
    public static void WriteField      <TBufferWriter>(this ref MemoryPackWriter<TBufferWriter>writer,FieldInfo              value)where TBufferWriter:IBufferWriter<byte> =>Formatters.Reflection.Field.Write      (ref writer,value);
    public static void Write<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter>writer,Type type,object? value)where TBufferWriter:IBufferWriter<byte>{
        if(FormatterResolver.GetRegisteredFormatter(type) is IMemoryPackFormatter Formatter) {
            writer.Write(Formatter,value);
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
        reader.GetRegisteredFormatterGetFormatter<T>().Deserialize(ref reader,ref value);
        //var Formatter=FormatterResolver.GetRegisteredFormatter<T>();
        //if(Formatter is not null){
        //    Formatter.Deserialize(ref reader,ref value);
        //} else{
        //    reader.GetFormatter<T>().Deserialize(ref reader,ref value);
        //}
        return value;
    }
    public static object? Read(this ref Reader reader,Type type){
        object? value=default;
        if(FormatterResolver.GetRegisteredFormatter(type)is IMemoryPackFormatter Formatter)
            Formatter.Deserialize(ref reader,ref value);
        else 
            reader.GetFormatter(type).Deserialize(ref reader,ref value);
        return value;
    }
    //public static decimal         ReadDecimal       (this ref Reader reader)=>global::MemoryPack.Formatters.d DecimalFormatter.Deserialize         (ref reader,Resolver);
    //public static TimeSpan        ReadTimeSpan      (this ref Reader reader)=>TimeSpanFormatter.Deserialize         (ref reader,Resolver);
    //public static DateTime        ReadDateTime      (this ref Reader reader)=>DateTimeFormatter.Deserialize         (ref reader,Resolver);
    //public static DateTimeOffset  ReadDateTimeOffset(this ref Reader reader)=>DateTimeOffsetFormatter.Deserialize   (ref reader,Resolver);
    public static Expression      ReadExpression (this ref Reader reader)=>Formatters.Expression.Read            (ref reader);
    //public static Type            ReadType       (this ref Reader reader)=>Formatters.Reflection.Type.Read       (ref reader);
    public static ConstructorInfo ReadConstructor(this ref Reader reader)=>Formatters.Reflection.Constructor.Read(ref reader);
    public static MethodInfo      ReadMethod     (this ref Reader reader)=>Formatters.Reflection.Method.Read     (ref reader);
    public static PropertyInfo    ReadProperty   (this ref Reader reader)=>Formatters.Reflection.Property.Read   (ref reader);
    public static EventInfo       ReadEvent      (this ref Reader reader)=>Formatters.Reflection.Event.Read      (ref reader);
    public static FieldInfo       ReadField      (this ref Reader reader)=>Formatters.Reflection.Field.Read      (ref reader);
    
    
    
    
    
    
    


    
    





















































































    public static Serializer Serializer<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer)where TBufferWriter:IBufferWriter<byte> =>
        (Serializer)writer.Options.ServiceProvider!;
    public static Serializer Serializer(this ref Reader reader)=>
        (Serializer)reader.Options.ServiceProvider!;
    public static IMemoryPackFormatter<T> GetRegisteredFormatterGetFormatter<T,TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer)where TBufferWriter:IBufferWriter<byte> =>
        FormatterResolver.GetRegisteredFormatter<T>()??writer.GetFormatter<T>();
    public static IMemoryPackFormatter<T> GetRegisteredFormatterGetFormatter<T>(this ref Reader reader)=>
        FormatterResolver.GetRegisteredFormatter<T>()??reader.GetFormatter<T>();
}
