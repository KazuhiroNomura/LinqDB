﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.PortableExecutable;

using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;

//public class AbstractFormatter:IJsonFormatter<object>,IMessagePackFormatter<object>{
//    private IJsonFormatter<object> JAbstract=>this;
//    private IMessagePackFormatter<object> MAbstract=>this;
//    public void Serialize(ref JsonWriter writer,object? value,IJsonFormatterResolver formatterResolver){
//        if(value is null){
//            writer.WriteNull();
//            return;
//        }
//        writer.WriteBeginArray();
//        Serialize_Type(ref writer,value.GetType(),formatterResolver);
//        writer.WriteValueSeparator();
//        //再帰してしまう
//        global::Utf8Json.Resolvers.StandardResolver.Default.GetFormatter<object>().Serialize(ref writer,value,formatterResolver);
//        //Serialize_T(ref writer,value,formatterResolver);
//        writer.WriteEndArray();
//    }
//    private readonly object[] Objects2=new object[2];
//    public object Deserialize(ref JsonReader reader,IJsonFormatterResolver formatterResolver){
//        if(reader.ReadIsNull())return null!;
//        reader.ReadIsBeginArrayWithVerify();
//        var type=Deserialize_Type(ref reader,formatterResolver);
//        reader.ReadIsValueSeparatorWithVerify();

//        //return global::Utf8Json.Resolvers.StandardResolver.Default.GetFormatter<object>().Deserialize(ref reader,formatterResolver);
//        var Formatter = formatterResolver.GetFormatterDynamic(type);
//        var Deserialize = Formatter.GetType().GetMethod("Deserialize");
//        Debug.Assert(Deserialize is not null);
//        var Objects2 = this.Objects2;
//        Objects2[0]=reader;
//        Objects2[1]=formatterResolver;
//        var value = Deserialize.Invoke(Formatter,Objects2);
//        reader=(JsonReader)Objects2[0];
//        reader.ReadIsEndArrayWithVerify();
//        return value;
//    }
//    //enum EType:sbyte{
//    //    AnonymousType
//    //};
//    public void Serialize(ref MessagePackWriter writer,object? value,MessagePackSerializerOptions options){
//        if(value is null){
//            writer.WriteNil();
//            return;
//        }
//        Serialize_Type(ref writer,value.GetType(),options);
//        global::MessagePack.Resolvers.StandardResolverAllowPrivate.Instance.GetFormatter<object>().Serialize(ref writer,value,options);
//    }
//    public object Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions options){
//        if(reader.TryReadNil()) return null!;
//        var type=Deserialize_Type(ref reader,options);
//        var value=MessagePackSerializer.Deserialize(type,ref reader,options);
//        return value;
//    }
//}
//public abstract class AbstractFormatter{
//    protected static readonly MessagePackSerializerOptions 再帰しないoptions=
//        MessagePackSerializerOptions.Standard.WithResolver(
//            MessagePack.Resolvers.CompositeResolver.Create(
//                MessagePack.Resolvers.DynamicGenericResolver.Instance,
//                MessagePack.Resolvers.StandardResolverAllowPrivate.Instance
//            )
//        );
//}
//public class AbstractFormatter<T>:AbstractFormatter,IJsonFormatter<T>,IMessagePackFormatter<T>{
//    private IJsonFormatter<T> JAbstract=>this;
//    private IMessagePackFormatter<T> MAbstract=>this;
//    public void Serialize(ref JsonWriter writer,T? value,IJsonFormatterResolver formatterResolver){
//        if(value is null){
//            writer.WriteNull();
//            return;
//        }
//        writer.WriteBeginArray();
//        Serialize_Type(ref writer,value.GetType(),formatterResolver);
//        writer.WriteValueSeparator();
//        //再帰してしまう Serialize_T(ref writer,value,formatterResolver);
//        Utf8Json.Resolvers.StandardResolver.Default.GetFormatter<object>().Serialize(ref writer,value,formatterResolver);
//        writer.WriteEndArray();
//    }
//    private readonly object[] Objects2=new object[2];
//    public T Deserialize(ref JsonReader reader,IJsonFormatterResolver formatterResolver){
//        if(reader.ReadIsNull())return default!;
//        //reader.ReadIsBeginObject();
//        reader.ReadIsBeginArrayWithVerify();
//        var type=Deserialize_Type(ref reader,formatterResolver);
//        reader.ReadIsValueSeparatorWithVerify();

//        var Formatter = Utf8Json.Resolvers.StandardResolver.Default.GetFormatterDynamic(type);
//        var Deserialize = Formatter.GetType().GetMethod("Deserialize");
//        Debug.Assert(Deserialize is not null);
//        var Objects2 = this.Objects2;
//        Objects2[0]=reader;
//        Objects2[1]=formatterResolver;
//        var value = Deserialize.Invoke(Formatter,Objects2);
//        reader=(JsonReader)Objects2[0];
//        reader.ReadIsEndArrayWithVerify();
//        return (T)value;
//    }
//    //enum EType:sbyte{
//    //    AnonymousType
//    //};
//    public void Serialize(ref MessagePackWriter writer,T? value,MessagePackSerializerOptions options){
//        if(value is null){
//            writer.WriteNil();
//            return;
//        }
//        var type=value.GetType();
//        Serialize_Type(ref writer,type,options);
//        //再帰してしまう MessagePackSerializer.Serialize(type,ref writer,value,options);
//        MessagePackSerializer.Serialize(type,ref writer,value,options);
//        //MessagePack.Resolvers.StandardResolverAllowPrivate.Instance.GetFormatter<T>().Serialize(ref writer,value,options);
//        //MessagePack.Resolvers.StandardResolverAllowPrivate.Instance.GetFormatter<object>().Serialize(ref writer,value,options);
//    }
//    //private static readonly MessagePackSerializerOptions 再帰しないoptions=MessagePackSerializerOptions.Standard.WithResolver(
//    //    global::MessagePack.Resolvers.CompositeResolver.Create(
//    //        new IFormatterResolver[]{
//    //            //global::MessagePack.Resolvers.DynamicObjectResolver.Instance,
//    //            global::MessagePack.Resolvers.DynamicGenericResolver.Instance,
//    //            //global::MessagePack.Resolvers.DynamicObjectResolverAllowPrivate.Instance,
//    //            global::MessagePack.Resolvers.StandardResolverAllowPrivate.Instance,
//    //            //global::MessagePack.Resolvers.StandardResolver.Instance,
//    //        }
//    //    )
//    //);
//    public T Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions options){
//        if(reader.TryReadNil()) return default!;
//        var type=Deserialize_Type(ref reader,options);
//        var reader0=reader;
//        //var reader1=reader;
//        var value=MessagePackSerializer.Deserialize(type,ref reader0,options);
//        reader=reader0;
//        //todo Anonymousはtypeヘッダーを入れることで型を指定するのだが内部のobjectフィールドには対応できない方法を考案する必要がある
//       //var value1=(T)MessagePackSerializer.Deserialize(type,ref reader1,options);
//        //reader=reader1;
//        return (T)value;
//        //return value1;
//    }
//}
public class AbstractJsonFormatter<T>:IJsonFormatter<T>{
    private readonly object[] Objects3=new object[3];
    private static object GetFormatter(IJsonFormatterResolver formatterResolver,Type type){
        if(typeof(Type).IsAssignableFrom(type)) type=typeof(Type);
        var Formatter= formatterResolver.GetFormatterDynamic(type);
        var Foramtter_Type=Formatter.GetType();
        if(Foramtter_Type.IsGenericType&&Foramtter_Type.GetGenericTypeDefinition()==typeof(AbstractJsonFormatter<>)){
            Type?Interface;
            if((Interface=type.GetInterface(typeof(System.Linq.ILookup<,>).FullName)) is not null)Formatter= formatterResolver.GetFormatterDynamic(Interface);
        }
        return Formatter;
    }
    public void Serialize(ref JsonWriter writer,T? value,IJsonFormatterResolver formatterResolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        var type=value.GetType();
        Serialize_Type(ref writer,type,formatterResolver);
        writer.WriteValueSeparator();
        //再帰してしまう Serialize_T(ref writer,value,formatterResolver);
        //if(typeof(Type).IsAssignableFrom(type)) type=typeof(Type);
        var Formatter=GetFormatter(formatterResolver,type);// formatterResolver.GetFormatterDynamic(type);
        //var Foramtter_Type=Formatter.GetType();
        //if(Foramtter_Type.IsGenericType&&Foramtter_Type.GetGenericTypeDefinition()==typeof(AbstractJsonFormatter<>)){
        //    Type?Interface;
        //    if((Interface=type.GetInterface(typeof(System.Linq.ILookup<,>).FullName)) is not null)Formatter= formatterResolver.GetFormatterDynamic(Interface);
        //} 
        //formatterResolver.
        //while(true){
        //    Formatter = formatterResolver.GetFormatterDynamic(type);
        //    var Foramtter_Type=Formatter.GetType();
        //    if(Foramtter_Type.IsGenericType&&
        //       Foramtter_Type.GetGenericTypeDefinition()==typeof(AbstractJsonFormatter<>)){
        //        type=type.BaseType!;
        //    } else
        //        break;
        //}
        //var Formatter = formatterResolver.GetFormatterDynamic(type);
        var Serialize = Formatter.GetType().GetMethod("Serialize");
        Debug.Assert(Serialize is not null);
        var Objects3 = this.Objects3;
        Objects3[0]=writer;
        Objects3[1]=value;
        Objects3[2]=formatterResolver;
        Serialize.Invoke(Formatter,Objects3);
        writer=(JsonWriter)Objects3[0];
        //Utf8Json.Resolvers.StandardResolver.Default.GetFormatter<T>().Serialize(ref writer,value,formatterResolver);
        writer.WriteEndArray();
    }
    private readonly object[] Objects2=new object[2];
    public T Deserialize(ref JsonReader reader,IJsonFormatterResolver formatterResolver){
        if(reader.ReadIsNull())return default!;
        //reader.ReadIsBeginObject();
        reader.ReadIsBeginArrayWithVerify();
        var type=Deserialize_Type(ref reader,formatterResolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Formatter=GetFormatter(formatterResolver,type);
        //if(typeof(Type).IsAssignableFrom(type)) type=typeof(Type);
        //var Formatter = formatterResolver.GetFormatterDynamic(type);
        var Deserialize = Formatter.GetType().GetMethod("Deserialize");
        Debug.Assert(Deserialize is not null);
        var Objects2 = this.Objects2;
        Objects2[0]=reader;
        Objects2[1]=formatterResolver;
        var value = (T)Deserialize.Invoke(Formatter,Objects2);
        reader=(JsonReader)Objects2[0];
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
public class AbstractMessagePackFormatter<T>:IMessagePackFormatter<T>{
    private static object GetFormatter(MessagePackSerializerOptions options,Type type){
        if(typeof(Type).IsAssignableFrom(type)) type=typeof(Type);
        var Formatter= options.Resolver.GetFormatterDynamic(type)!;
        var Foramtter_Type=Formatter.GetType();
        if(Foramtter_Type.IsGenericType&&Foramtter_Type.GetGenericTypeDefinition()==typeof(AbstractMessagePackFormatter<>)){
            Type?Interface;
            if((Interface=type.GetInterface(typeof(System.Linq.ILookup<,>).FullName)) is not null)Formatter= options.Resolver.GetFormatterDynamic(Interface)!;
        }
        return Formatter;
    }
    public void Serialize(ref MessagePackWriter writer,T? value,MessagePackSerializerOptions options){
        if(value is null){
            writer.WriteNil();
            return;
        }
        //var MethodX=typeof(AbstractFormatter).GetMethod("MethodX")!;
        //var DelX=Delegate.CreateDelegate(typeof(DelegateX<>).MakeGenericType(MethodX.GetParameters()[0].ParameterType),MethodX);
        //DelX.DynamicInvoke(1);
        writer.WriteArrayHeader(2);
        var type=value.GetType();
        Serialize_Type(ref writer,type,options);
        //if(typeof(Type).IsAssignableFrom(type)) type=typeof(Type);
        //var Formatter = options.Resolver.GetFormatterDynamic(type)!;
        //var Serialize = Formatter.GetType().GetMethod("Serialize");
        //Debug.Assert(Serialize!=null,nameof(Serialize)+" != null");
        //var Delegate0=(SerializeDelegate)Delegate.CreateDelegate(typeof(SerializeDelegate<>).MakeGenericType(Serialize.GetParameters()[1].ParameterType),Formatter,Serialize);
        //Delegate0(ref writer,value,options);
        //再帰してしまう MessagePackSerializer.Serialize(type,ref writer,value,options);
        //MessagePackSerializer.Serialize(type,ref writer,value,options);
        SerializerConfiguration.DynamicSerialize(GetFormatter(options,type),ref writer,value,options);
        //Serialize_T(ref writer,value,options);
        //MessagePack.Resolvers.StandardResolver.Instance.GetFormatter<T>().Serialize(ref writer,value,options);
        //MessagePack.Resolvers.StandardResolverAllowPrivate.Instance.GetFormatter<T>().Serialize(ref writer,value,options);
    }
    //private delegate T DeserializeDelegate(ref MessagePackReader reader,MessagePackSerializerOptions options);
    public T Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions options){
        if(reader.TryReadNil()) return default!;
        var ArrayHeader=reader.ReadArrayHeader();
        Debug.Assert(ArrayHeader==2);
        var type=Deserialize_Type(ref reader,options);
        //if(typeof(Type).IsAssignableFrom(type)) type=typeof(Type);
        //var Formatter = MessagePack.Resolvers.StandardResolver.Instance.GetFormatterDynamic(type);
        //var Deserialize = Formatter.GetType().GetMethod("Deserialize");
        //Debug.Assert(Deserialize!=null,nameof(Deserialize)+" != null");
        //var Delegate0=(DeserializeDelegate)Delegate.CreateDelegate(typeof(DeserializeDelegate),Formatter,Deserialize);
        ////Debug.Assert(Deserialize is not null);
        //var value = Delegate0(ref reader,options);
        //var value=MessagePackSerializer.Deserialize(type,ref reader,options);
        var value=(T)SerializerConfiguration.DynamicDeserialize(GetFormatter(options,type),ref reader,options);
        //var value = MessagePackSerializer.Serialize(type,ref writer,value,options);
        return value;
        //var reader0=reader;
        //var reader1=reader;
        //reader=reader0;
        //todo Anonymousはtypeヘッダーを入れることで型を指定するのだが内部のTフィールドには対応できない方法を考案する必要がある
       //var value1=(T)MessagePackSerializer.Deserialize(type,ref reader1,options);
        //reader=reader1;
        //return (T)value;
        //return value1;
    }
}
