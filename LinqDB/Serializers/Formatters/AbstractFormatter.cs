using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using LinqDB.Helpers;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
#pragma warning disable CA1052 // スタティック ホルダー型は Static または NotInheritable でなければなりません
/// <summary>
/// sealedではないクラスをシリアライズする
/// </summary>
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
        else if(typeof(LambdaExpression).IsAssignableFrom(type)) type=typeof(LambdaExpression);
        var Formatter=formatterResolver.GetFormatterDynamic(type);
        Debug.Assert(Formatter is not null,"Formatterが見つからない");
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
        if(typeof(Expression).IsAssignableFrom(type)){
            var Formatter=formatterResolver.GetFormatter<Expression>();
            //var Formatter = formatterResolver.GetFormatter<LambdaExpression>();
            Formatter.Serialize(ref writer,(Expression)(object)value,formatterResolver);
            //Formatter.Serialize(ref writer,(LambdaExpression)(object)value,formatterResolver);
        //}else if(typeof(T).IsDisplay()){
        //    return Return(new DisplayClassJsonFormatter<T>());
        //}else  if(typeof(T).IsAnonymous()){
        //    return Return(new AnonymousJsonFormatter<T>());
        }else{
            var Formatter=GetFormatter(formatterResolver,type);
            Debug.Assert(this!=Formatter);
            var Serialize=Formatter.GetType().GetMethod("Serialize");
            Debug.Assert(Serialize is not null);
            var Objects3=this.Objects3;
            Objects3[0]=writer;
            Objects3[1]=value;
            Objects3[2]=formatterResolver;
            Serialize.Invoke(Formatter,Objects3);
            writer=(JsonWriter)Objects3[0];
        }
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
        writer.WriteArrayHeader(2);
        var type=value.GetType();
        Serialize_Type(ref writer,type,options);
        SerializerConfiguration.DynamicSerialize(GetFormatter(options,type),ref writer,value,options);
    }
    public T Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions options){
        if(reader.TryReadNil()) return default!;
        var ArrayHeader=reader.ReadArrayHeader();
        Debug.Assert(ArrayHeader==2);
        var type=Deserialize_Type(ref reader,options);
        var value=(T)SerializerConfiguration.DynamicDeserialize(GetFormatter(options,type),ref reader,options);
        return value;
    }
}
