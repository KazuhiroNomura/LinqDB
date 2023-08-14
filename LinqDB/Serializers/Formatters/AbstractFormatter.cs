using System;
using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;

public class AbstractFormatter:IJsonFormatter<object>,IMessagePackFormatter<object>{
    private IJsonFormatter<object> JAbstract=>this;
    private IMessagePackFormatter<object> MAbstract=>this;
    public void Serialize(ref JsonWriter writer,object? value,IJsonFormatterResolver formatterResolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        Serialize_Type(ref writer,value.GetType(),formatterResolver);
        writer.WriteValueSeparator();
        //再帰してしまう
        global::Utf8Json.Resolvers.StandardResolver.Default.GetFormatter<object>().Serialize(ref writer,value,formatterResolver);
        //Serialize_T(ref writer,value,formatterResolver);
        writer.WriteEndArray();
    }
    private readonly object[] Objects2=new object[2];
    public object Deserialize(ref JsonReader reader,IJsonFormatterResolver formatterResolver){
        if(reader.ReadIsNull())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var type=Deserialize_Type(ref reader,formatterResolver);
        reader.ReadIsValueSeparatorWithVerify();

        //return global::Utf8Json.Resolvers.StandardResolver.Default.GetFormatter<object>().Deserialize(ref reader,formatterResolver);
        var Formatter = formatterResolver.GetFormatterDynamic(type);
        var Deserialize = Formatter.GetType().GetMethod("Deserialize");
        Debug.Assert(Deserialize is not null);
        var Objects2 = this.Objects2;
        Objects2[0]=reader;
        Objects2[1]=formatterResolver;
        var value = Deserialize.Invoke(Formatter,Objects2);
        reader=(JsonReader)Objects2[0];
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
    //enum EType:sbyte{
    //    AnonymousType
    //};
    public void Serialize(ref MessagePackWriter writer,object? value,MessagePackSerializerOptions options){
        if(value is null){
            writer.WriteNil();
            return;
        }
        Serialize_Type(ref writer,value.GetType(),options);
        global::MessagePack.Resolvers.StandardResolverAllowPrivate.Instance.GetFormatter<object>().Serialize(ref writer,value,options);
    }
    public object Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions options){
        if(reader.TryReadNil()) return null!;
        var type=Deserialize_Type(ref reader,options);
        var value=MessagePackSerializer.Deserialize(type,ref reader,options);
        return value;
    }
}
public class AbstractFormatter<T>:IJsonFormatter<T>,IMessagePackFormatter<T>{
    private IJsonFormatter<T> JAbstract=>this;
    private IMessagePackFormatter<T> MAbstract=>this;
    public void Serialize(ref JsonWriter writer,T? value,IJsonFormatterResolver formatterResolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        Serialize_Type(ref writer,value.GetType(),formatterResolver);
        writer.WriteValueSeparator();
        //再帰してしまう
        global::Utf8Json.Resolvers.StandardResolver.Default.GetFormatter<object>().Serialize(ref writer,value,formatterResolver);
        //Serialize_T(ref writer,value,formatterResolver);
        writer.WriteEndArray();
    }
    private readonly object[] Objects2=new object[2];
    public T Deserialize(ref JsonReader reader,IJsonFormatterResolver formatterResolver){
        if(reader.ReadIsNull())return default!;
        //reader.ReadIsBeginObject();
        reader.ReadIsBeginArrayWithVerify();
        var type=Deserialize_Type(ref reader,formatterResolver);
        reader.ReadIsValueSeparatorWithVerify();

        var Formatter = global::Utf8Json.Resolvers.StandardResolver.Default.GetFormatterDynamic(type);
        var Deserialize = Formatter.GetType().GetMethod("Deserialize");
        Debug.Assert(Deserialize is not null);
        var Objects2 = this.Objects2;
        Objects2[0]=reader;
        Objects2[1]=formatterResolver;
        var value = Deserialize.Invoke(Formatter,Objects2);
        reader=(JsonReader)Objects2[0];
        reader.ReadIsEndArrayWithVerify();
        return (T)value;
    }
    //enum EType:sbyte{
    //    AnonymousType
    //};
    public void Serialize(ref MessagePackWriter writer,T? value,MessagePackSerializerOptions options){
        if(value is null){
            writer.WriteNil();
            return;
        }
        var type=value.GetType();
        Serialize_Type(ref writer,type,options);
        MessagePackSerializer.Serialize(type,ref writer,value,再帰しないoptions);
        //global::MessagePack.Resolvers.StandardResolverAllowPrivate.Instance.GetFormatter<T>().Serialize(ref writer,value,options);
    }
    private static readonly MessagePackSerializerOptions 再帰しないoptions=MessagePackSerializerOptions.Standard.WithResolver(
        global::MessagePack.Resolvers.CompositeResolver.Create(
            new IFormatterResolver[]{
                //global::MessagePack.Resolvers.DynamicObjectResolver.Instance,
                global::MessagePack.Resolvers.DynamicGenericResolver.Instance,
                //global::MessagePack.Resolvers.DynamicObjectResolverAllowPrivate.Instance,
                global::MessagePack.Resolvers.StandardResolverAllowPrivate.Instance,
                //global::MessagePack.Resolvers.StandardResolver.Instance,
            }
        )
    );
    public T Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions options){
        if(reader.TryReadNil()) return default!;
        var type=Deserialize_Type(ref reader,options);
        //var i=new IFormatterResolver[]{
        //    global::MessagePack.Resolvers.DynamicObjectResolver.Instance,
        //    global::MessagePack.Resolvers.DynamicGenericResolver.Instance,
        //    global::MessagePack.Resolvers.DynamicObjectResolverAllowPrivate.Instance,
        //    global::MessagePack.Resolvers.StandardResolverAllowPrivate.Instance,
        //    global::MessagePack.Resolvers.StandardResolver.Instance,
        //    global::MessagePack.Resolvers.AttributeFormatterResolver.Instance,
        //    global::MessagePack.Resolvers.BuiltinResolver.Instance,
        //    global::MessagePack.Resolvers.ContractlessStandardResolver.Instance,
        //    global::MessagePack.Resolvers.ContractlessStandardResolverAllowPrivate.Instance,
        //    //global::MessagePack.Resolvers.DynamicContractlessObjectResolver.Instance,
        //    global::MessagePack.Resolvers.DynamicContractlessObjectResolver.Instance,
        //    global::MessagePack.Resolvers.DynamicContractlessObjectResolverAllowPrivate.Instance,
        //    global::MessagePack.Resolvers.StandardResolver.Instance,
        //    global::MessagePack.Resolvers.DynamicGenericResolver.Instance,
        //    global::MessagePack.Resolvers.DynamicObjectResolverAllowPrivate.Instance,
        //    global::MessagePack.Resolvers.ExpandoObjectResolver.Instance,
        //    global::MessagePack.Resolvers.PrimitiveObjectResolver.Instance,
        //    global::MessagePack.Resolvers.TypelessContractlessStandardResolver.Instance,
        //    global::MessagePack.Resolvers.TypelessObjectResolver.Instance,
        //};
        //var f=options.Resolver.GetFormatterDynamic(type);
        //foreach(var a in i){
        //    try{
        //        var ff=a.GetFormatterDynamic(type);
        //        if(ff is not null){

        //        }
        //    } catch(TypeInitializationException){
        //        continue;
        //    }
        //    var fff=a;
        //}
        var reader0=reader;
        var reader1=reader;
        var value0=(T)MessagePackSerializer.Deserialize(type,ref reader0,再帰しないoptions);
        reader=reader0;
        //todo Anonymousはtypeヘッダーを入れることで型を指定するのだが内部のobjectフィールドには対応できない方法を考案する必要がある
        var value1=(T)MessagePackSerializer.Deserialize(type,ref reader1,options);
        reader=reader1;
        return value0;
        //return value1;
    }
}
