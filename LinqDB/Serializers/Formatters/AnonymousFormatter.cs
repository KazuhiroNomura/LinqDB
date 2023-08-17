﻿//#define 匿名型にキーを入れる
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization;
using LinqDB.Helpers;
using MessagePack;
using MessagePack.Formatters;
using Microsoft.CodeAnalysis;

using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
//public abstract class AnonymousFormatter{
//    protected static readonly IJsonFormatterResolver 再帰しないFormatterResolver =Utf8Json.Resolvers.CompositeResolver.Create(
//        //順序が大事
//        //global::Utf8Json.Resolvers.DynamicObjectResolver.Default,//これが存在するとStackOverflowする
//        Utf8Json.Resolvers.DynamicGenericResolver.Instance,
//        //global::Utf8Json.Resolvers.DynamicObjectResolver.AllowPrivate,//これが存在するとTypeがシリアライズできない
//        Utf8Json.Resolvers.StandardResolver.AllowPrivate
//        //global::Utf8Json.Resolvers.StandardResolver.Default,
//        );
//    protected static readonly MessagePackSerializerOptions 再帰しないoptions = MessagePackSerializerOptions.Standard.WithResolver(
//        MessagePack.Resolvers.CompositeResolver.Create(
//            //global::MessagePack.Resolvers.DynamicObjectResolver.Instance,
//            MessagePack.Resolvers.DynamicGenericResolver.Instance,
//            //global::MessagePack.Resolvers.DynamicObjectResolverAllowPrivate.Instance,
//            MessagePack.Resolvers.StandardResolverAllowPrivate.Instance
//            //global::MessagePack.Resolvers.StandardResolver.Instance,
//        )
//    );
//}
public class AnonymousJsonFormatter<T>:IJsonFormatter<T>{
    private readonly object[] Objects3=new object[3];
    public void Serialize(ref JsonWriter writer,T? value,IJsonFormatterResolver formatterResolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        var Parameters = typeof(T).GetConstructors()[0].GetParameters();
        var Parameters_Length = Parameters.Length;
        writer.WriteBeginObject();
        var Objects3=this.Objects3;
        Objects3[2]=formatterResolver;
        for(var a = 0;a<Parameters_Length;a++) {
            var Parameter = Parameters[a];
            var Key = Parameter.Name;
            var Value=typeof(T).GetProperty(Key)!.GetMethod.Invoke(value,Array.Empty<object>());
            writer.WriteString(Key);
            writer.WriteNameSeparator();
            var Formatter=formatterResolver.GetFormatterDynamic(Parameter.ParameterType);
            var Serialize = Formatter.GetType().GetMethod("Serialize");
            Debug.Assert(Serialize is not null);
            Objects3[0]=writer;
            Objects3[1]=Value;
            Serialize.Invoke(Formatter,Objects3);
            writer=(JsonWriter)Objects3[0];
            if(a<Parameters_Length-1)
                writer.WriteValueSeparator();
            //Serialize_T(ref writer,Value,formatterResolver);
        }
        writer.WriteEndObject();
    }
    private readonly object[] Objects2=new object[2];
    public T Deserialize(ref JsonReader reader,IJsonFormatterResolver formatterResolver){
        if(reader.ReadIsNull())return default!;
        reader.ReadIsBeginObjectWithVerify();
        var ctor = typeof(T).GetConstructors()[0];
        var Parameters = ctor.GetParameters();
        var Parameters_Length = Parameters.Length;
        var args=new object[Parameters_Length];
        for(var a = 0;a<Parameters_Length;a++) {
            var Key = reader.ReadString();
            reader.ReadIsNameSeparatorWithVerify();
            Debug.Assert(Parameters[a].Name==Key);
            var Formatter=formatterResolver.GetFormatterDynamic(Parameters[a].ParameterType);
            var Deserialize = Formatter.GetType().GetMethod("Deserialize");
            Debug.Assert(Deserialize is not null);
            var Objects2 = this.Objects2;
            Objects2[0]=reader;
            Objects2[1]=formatterResolver;
            args[a]=Deserialize.Invoke(Formatter,Objects2);
            reader=(JsonReader)Objects2[0];
            if(a<Parameters_Length-1)
                reader.ReadIsValueSeparatorWithVerify();
        }
        reader.ReadIsEndObjectWithVerify();
        return (T)ctor.Invoke(args);
    }
}
public class AnonymousMessagePackFormatter<T>:IMessagePackFormatter<T>{
    public void Serialize(ref MessagePackWriter writer,T? value,MessagePackSerializerOptions options){
        if(value is null){
            writer.WriteNil();
            return;
        }
        var Parameters = typeof(T).GetConstructors()[0].GetParameters();
        var Parameters_Length = Parameters.Length;
#if 匿名型にキーを入れる
        writer.WriteMapHeader(Parameters_Length);
        for(var a = 0;a<Parameters_Length;a++) {
            var Parameter = Parameters[a];
            var Key = Parameter.Name;
            writer.Write(Key);
            SerializerConfiguration.DynamicSerialize(options.Resolver.GetFormatterDynamic(Parameter.ParameterType),ref writer,typeof(T).GetProperty(Key)!.GetMethod.Invoke(value,Array.Empty<object>()),options);
        }
#else
        writer.WriteArrayHeader(Parameters_Length);
        for(var a = 0;a<Parameters_Length;a++) {
            var Parameter = Parameters[a];
            var Key = Parameter.Name;
            SerializerConfiguration.DynamicSerialize(options.Resolver.GetFormatterDynamic(Parameter.ParameterType),ref writer,typeof(T).GetProperty(Key)!.GetMethod.Invoke(value,Array.Empty<object>()),options);
        }
#endif
    }
    public T Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions options){
        if(reader.TryReadNil()) return default!;
        var ctor = typeof(T).GetConstructors()[0];
        var Parameters = ctor.GetParameters();
#if 匿名型にキーを入れる
        var Length=reader.ReadMapHeader();
        var args=new object[Length];
        for(var a = 0;a<Length;a++) {
            var Key = reader.ReadString();
            Debug.Assert(Parameters[a].Name==Key);
            args[a]=SerializerConfiguration.DynamicDeserialize(options.Resolver.GetFormatterDynamic(Parameters[a].ParameterType),ref reader,options);
        }
#else
        var Length=reader.ReadArrayHeader();
        var args=new object[Length];
        for(var a = 0;a<Length;a++) {
            //var Key = reader.ReadString();
            //Debug.Assert(Parameters[a].Name==Key);
            args[a]=SerializerConfiguration.DynamicDeserialize(options.Resolver.GetFormatterDynamic(Parameters[a].ParameterType),ref reader,options);
        }
#endif
        Debug.Assert(Length==Parameters.Length);
        return (T)ctor.Invoke(args);
    }
}