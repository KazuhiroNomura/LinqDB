using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Linq;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
using System.Runtime.Serialization;

namespace LinqDB.Serializers.Formatters;
using static Common;
#pragma warning disable CA1052 // スタティック ホルダー型は Static または NotInheritable でなければなりません
public class AbstractFormatter{
#pragma warning restore CA1052 // スタティック ホルダー型は Static または NotInheritable でなければなりません
    protected static bool GetInterface(Type type,out Type Interface){
        if(
            (Interface=type.GetInterface(typeof(ILookup<,>).FullName)) is not null||
            (Interface=type.GetInterface(typeof(IGrouping<,>).FullName)) is not null
        ) return true;
        return false;
    }
}
public class AbstractJsonFormatter<T>:AbstractFormatter,IJsonFormatter<T>{
    private readonly object[] Objects3=new object[3];
    private static object GetFormatter(IJsonFormatterResolver formatterResolver,Type type){
        if(typeof(Type).IsAssignableFrom(type)) type=typeof(Type);
        var Formatter=formatterResolver.GetFormatterDynamic(type);
        var Foramtter_Type=Formatter.GetType();
        if(Foramtter_Type.IsGenericType&&Foramtter_Type.GetGenericTypeDefinition()==typeof(AbstractJsonFormatter<>)&&GetInterface(type,out var Interface))
            Formatter=formatterResolver.GetFormatterDynamic(Interface);
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
        var Formatter=GetFormatter(formatterResolver,type);//
        //var Formatter=formatterResolver.GetFormatterDynamic(type);
        //var Foramtter_Type=Formatter.GetType();
        //if(Foramtter_Type.IsGenericType&&Foramtter_Type.GetGenericTypeDefinition()==typeof(AbstractJsonFormatter<>)){
        //    Type?Interface;
        //    if((Interface=type.GetInterface(typeof(ILookup<,>).FullName)) is not null)Formatter= formatterResolver.GetFormatterDynamic(Interface);
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
public class AbstractMessagePackFormatter<T>:AbstractFormatter,IMessagePackFormatter<T>{
    private static object GetFormatter(MessagePackSerializerOptions options,Type type){
        if(typeof(Type).IsAssignableFrom(type)) type=typeof(Type);
        var Formatter=options.Resolver.GetFormatterDynamic(type)!;
        var Foramtter_Type=Formatter.GetType();
        if(Foramtter_Type.IsGenericType&&Foramtter_Type.GetGenericTypeDefinition()==typeof(AbstractMessagePackFormatter<>)&&GetInterface(type,out var Interface))
            Formatter=options.Resolver.GetFormatterDynamic(Interface)!;
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
