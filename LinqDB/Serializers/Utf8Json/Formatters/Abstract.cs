using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LinqDB.Helpers;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using C=Serializer;
using static Extension;
#pragma warning disable CA1052 // スタティック ホルダー型は Static または NotInheritable でなければなりません
/// <summary>
/// sealedではないクラスをシリアライズする
/// </summary>
public class Abstract{
#pragma warning restore CA1052 // スタティック ホルダー型は Static または NotInheritable でなければなりません
    protected static void GetInterface(ref System.Type Type){
        var Interface0=Type.GetInterface(typeof(ILookup<,>).FullName);
        if(Interface0 is not null){
            Type=Interface0;
        } else{
            var Interface1=Type.GetInterface(typeof(IGrouping<,>).FullName);
            if(Interface1 is not null) Type=Interface1;
        }
        //if(
        //    (Interface=Type.GetInterface(typeof(ILookup<,>).FullName)) is not null||
        //    (Interface=Type.GetInterface(typeof(IGrouping<,>).FullName)) is not null
        //) return true;
    }
}
public class Abstract<T>:Abstract,IJsonFormatter<T>{
    public static readonly Abstract<T> Instance=new();
    private readonly object[] Objects3=new object[3];
    private static object GetFormatter(IJsonFormatterResolver formatterResolver,System.Type type){
        if(typeof(System.Type).IsAssignableFrom(type)) type=typeof(System.Type);
        else if(typeof(LambdaExpression).IsAssignableFrom(type)) type=typeof(LambdaExpression);
        //else if(type.IsDisplay())return new DisplayClassJsonFormatter<T>();
        if(type.IsDisplay()){}
        var Formatter=formatterResolver.GetFormatterDynamic(type);
        Debug.Assert(Formatter is not null,"Formatterが見つからない");
        var Foramtter_Type=Formatter.GetType();
        if(Foramtter_Type.IsGenericType&&Foramtter_Type.GetGenericTypeDefinition()==typeof(Abstract<>)){
            GetInterface(ref type);
            Formatter=formatterResolver.GetFormatterDynamic(type);
        }
        return Formatter;
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        //if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        var type=value.GetType();
        writer.WriteType(type);
        writer.WriteValueSeparator();
        if(typeof(System.Linq.Expressions.Expression).IsAssignableFrom(type)){
            var Formatter= Resolver.GetFormatter<System.Linq.Expressions.Expression>();
            //var Formatter = formatterResolver.GetFormatter<LambdaExpression>();
            Formatter.Serialize(ref writer,(System.Linq.Expressions.Expression)(object)value, Resolver);
            //Formatter.Serialize(ref writer,(LambdaExpression)(object)value,formatterResolver);
        //}else if(typeof(T).IsDisplay()){
        //    return Return(new DisplayClassJsonFormatter<T>());
        //}else  if(typeof(T).IsAnonymous()){
        //    var Formatter=new Anonymous<T>();
        //    Formatter.Serialize(ref writer, value, Resolver);
        }else{
            if(type.GetCustomAttribute(typeof(SerializableAttribute))!=null){
              //  formatterResolver.
            }
            var Formatter= GetFormatter(Resolver, type);
            if(Formatter==this) throw new InvalidProgramException("Formatter探索で無限ループ");
            var Serialize= Formatter.GetType().GetMethod("Serialize");
            Debug.Assert(Serialize is not null);
            var Objects3=this.Objects3;
            Objects3[0]=writer;
            Objects3[1]=value;
            Objects3[2]=Resolver;
            Serialize.Invoke(Formatter, Objects3);
            writer=(Writer)Objects3[0];
        }
        //Utf8Json.Resolvers.StandardResolver.Default.GetFormatter<T>().Serialize(ref writer,value,formatterResolver);
        writer.WriteEndArray();
    }
    private readonly object[] Objects2=new object[2];
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        //if(reader.ReadIsNull())return default!;
        //reader.ReadIsBeginObject();
        reader.ReadIsBeginArrayWithVerify();
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var Formatter=GetFormatter(Resolver,type);
        var Deserialize = Formatter.GetType().GetMethod("Deserialize");
        Debug.Assert(Deserialize is not null);
        var Objects2 = this.Objects2;
        Objects2[0]=reader;
        Objects2[1]=Resolver;
        var value = (T)Deserialize.Invoke(Formatter,Objects2)!;
        reader=(Reader)Objects2[0];
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}

