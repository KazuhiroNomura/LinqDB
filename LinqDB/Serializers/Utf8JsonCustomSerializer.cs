
using System;
using Expressions=System.Linq.Expressions;
using System.Collections.Generic;
using System.IO;
using LinqDB.Helpers;
using MemoryPack_Formatters=LinqDB.Serializers.MemoryPack.Formatters;
using Utf8Json;
using LinqDB.Serializers.Formatters;
namespace LinqDB.Serializers;
//public abstract class ACustomSerializer{
//    //public abstract void Clear();
//    public abstract byte[]Serialize<T>(T value);
//    public abstract void Serialize<T>(Stream stream,T value);
//    public abstract T Deserialize<T>(byte[]bytes);
//    public abstract T Deserialize<T>(Stream stream);
//}
public static class Utf8JsonCustomSerializer{
    private static readonly ExpressionJsonFormatter ExpressionFormatter = new();
    private sealed class Utf8JsonCustomFormatterResolver:IJsonFormatterResolver{
        //private readonly ExpressionJsonFormatter ExpressionFormatter = new();
        private readonly Dictionary<System.Type,IJsonFormatter> Dictionary_Type_Formatter = new();
        //private readonly Type[] GenericArguments=new Type[1];
        public IJsonFormatter<T> GetFormatter<T>(){
            if(typeof(T).IsDefined(typeof(SerializableAttribute),true))return null!;
            if(this.Dictionary_Type_Formatter.TryGetValue(typeof(T),out var IJsonFormatter))return (IJsonFormatter<T>)IJsonFormatter;
            if(typeof(Expressions.Expression).IsAssignableFrom(typeof(T)))return Return(ExpressionFormatter);
            if(
                typeof(T)==typeof(System.Type)||
                typeof(T)==typeof(System.Reflection.MemberInfo)||
                typeof(T)==typeof(System.Reflection.MethodInfo)||
                typeof(T)==typeof(System.Reflection.FieldInfo)||
                typeof(T)==typeof(System.Reflection.PropertyInfo)||
                typeof(T)==typeof(System.Reflection.EventInfo)||
                typeof(Expressions.MemberBinding).IsAssignableFrom(typeof(T))||
                typeof(T)==typeof(System.Reflection.MethodInfo)||
                typeof(T)==typeof(System.Reflection.MemberInfo)||
                typeof(T)==typeof(Expressions.CatchBlock)||
                typeof(T)==typeof(Expressions.SwitchCase)||
                typeof(T)==typeof(Expressions.ElementInit))
                return Return(ExpressionFormatter);
            if(typeof(T).IsDisplay())return Return(new DisplayClassJsonFormatter<T>());
            if(typeof(T).IsAnonymous())return Return(new AnonymousJsonFormatter<T>());
            //var Formatter=GetFormatter(typeof(T));
            //if(Formatter!=null) Return(Formatter);
            return Return(new AbstractJsonFormatter<T>());
            IJsonFormatter<T> Return(object Formatter){
                var result=(IJsonFormatter<T>)Formatter;
                this.Dictionary_Type_Formatter.Add(typeof(T),result);
                return result;
            }
        }
        public void Clear() {
            //this.ExpressionFormatter.Clear();
            //this.Dictionary_Type_Formatter.Clear();
        }
    }
    private static readonly Utf8JsonCustomFormatterResolver Resolver=new();
    private static readonly IJsonFormatterResolver IResolver=Utf8Json.Resolvers.CompositeResolver.Create(
        //System.Array.Empty<IJsonFormatter>(),
        new IJsonFormatter[]{
            ExpressionFormatter, 
        },
        new IJsonFormatterResolver[]{
            //    this.AnonymousExpressionJsonFormatterResolver,//先頭に無いと匿名型やシリアライズ可能型がDictionaryになってしまう
            //    short
            //    int
            //    byte[]
            //    Nil
            //    Nil?
            //    List<short>
            //    List<int>
            Utf8Json.Resolvers.BuiltinResolver.Instance,//よく使う型
            //    Lazy<>
            Utf8Json.Resolvers.DynamicGenericResolver.Instance,//主にジェネリックコレクション
            Utf8Json.Resolvers.EnumResolver.Default,
            Utf8Json.Resolvers.AttributeFormatterResolver.Instance,
            //Utf8Json.Resolvers.StandardResolver.Default,

            Utf8Json.Resolvers.DynamicObjectResolver.Default,
            //Utf8Json.Resolvers.CompositeResolver.Instance,



            Resolver,



            //Utf8Json.Resolvers.DynamicObjectResolver.Default,//これが存在するとStackOverflowする
            //Utf8Json.Resolvers.DynamicObjectResolver.AllowPrivate,//これが存在するとTypeがシリアライズできない

            //BuiltinResolver.Instance,
            //EnumResolver.Default,
            //DynamicGenericResolver.Instance,
            //AttributeFormatterResolver.Instance
            //Utf8Json.Resolvers.EnumResolver.Default,
            //Utf8Json.Resolvers.DynamicGenericResolver.Instance,
            //Utf8Json.Resolvers.AttributeFormatterResolver.Instance,
            //Utf8Json.Resolvers.StandardResolver.AllowPrivate,//いくつかのリゾルバをまとめてある
            //Utf8Json.Resolvers.StandardResolver.Default,
            //this.AnonymousExpressionJsonFormatterResolver,
        }
    );
    private static void Clear()=>Resolver.Clear();
    public static byte[]Serialize<T>(T value){
        Clear();
        return JsonSerializer.Serialize(value,IResolver);
    }
    public static void Serialize<T>(Stream stream,T value){
        Clear();
        JsonSerializer.Serialize(stream,value,IResolver);
    }
    public static T Deserialize<T>(byte[] bytes){
        Clear();
        return JsonSerializer.Deserialize<T>(bytes,IResolver);
    }
    public static T Deserialize<T>(Stream stream){
        Clear();
        return JsonSerializer.Deserialize<T>(stream,IResolver);
    }
}
