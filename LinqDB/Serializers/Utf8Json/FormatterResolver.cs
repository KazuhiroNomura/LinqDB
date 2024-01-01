using System;
using System.Diagnostics;
using System.Reflection;
using LinqDB.Helpers;
using Utf8Json;
using Generic = System.Collections.Generic;
using Expressions = System.Linq.Expressions;
using LinqDB.Enumerables;
namespace LinqDB.Serializers.Utf8Json;
internal sealed class FormatterResolver:IJsonFormatterResolver{
    //キャッシュする条件が見つからない
    private readonly Generic.Dictionary<Type,IJsonFormatter> TypeFormatter=new();
    public IJsonFormatter<T> GetFormatter<T>(){
        var type=typeof(T);
        if(type.GetCustomAttribute(typeof(SerializableAttribute))!=null)return global::Utf8Json.Resolvers.StandardResolver.AllowPrivate.GetFormatter<T>();;
        Debug.Assert(!this.TypeFormatter.ContainsKey(type));
        if(this.TypeFormatter.TryGetValue(type,out var value))return(IJsonFormatter<T>)value;
        if(type.IsArray)
            return default!;


        if(type.IsAnonymous())
            return Return(Formatters.Others.Anonymous<T>.Instance);
        if(type.IsDisplay())
            return Return(Formatters.Others.DisplayClass<T>.Instance);
        if(typeof(Delegate).IsAssignableFrom(type))
            return Return((IJsonFormatter<T>)typeof(Formatters.Others.Delegate<>).MakeGenericType(type).GetValue("Instance"));
        if(type.IsGenericType){
            if(typeof(Expressions.LambdaExpression).IsAssignableFrom(type))
                return Return((IJsonFormatter<T>)typeof(Formatters.ExpressionT<>).MakeGenericType(type).GetValue("Instance"));
            IJsonFormatter<T>? Formatter=null;
            if(type.IsInterface){
                if((Formatter=RegisterInterface(type,typeof(Sets.IGrouping       <,>),typeof(Formatters.Sets.IGrouping         <,>)))is not null)return Return(Formatter);
                if((Formatter=RegisterInterface(type,typeof(System.Linq.IGrouping<,>),typeof(Formatters.Enumerables.IGrouping  <,>)))is not null)return Return(Formatter);
                if((Formatter=RegisterInterface(type,typeof(Sets.IEnumerable     < >),typeof(Formatters.Sets.IEnumerable       < >)))is not null)return Return(Formatter);
                //if((Formatter=RegisterInterface(type,typeof(Generic.IEnumerable  < >),typeof(Formatters.Enumerables.IEnumerable< >)))is not null)return Return(Formatter);
                Formatter=RegisterInterface(type,typeof(Generic.IEnumerable<>),typeof(Formatters.Enumerables.IEnumerable<>));
                Debug.Assert(Formatter is not null);
                return Return(Formatter);
            }else{
                if((Formatter=RegisterType(type,typeof(Grouping<, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.Grouping        <, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Lookup    <, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.SetGroupingSet     <, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.Set<,,>))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.Set<,>))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.Set<>))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(List        <  >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.HashSet            <  >))) is not null) return Return(Formatter);
            }
        }
        return default!;
        IJsonFormatter<T> Return(object Formatter0){
            var result=(IJsonFormatter<T>)Formatter0;
            this.TypeFormatter.Add(typeof(T),result);
            return result;
        }
        IJsonFormatter<T>?RegisterInterface(Type type0,Type 検索したいキーGenericInterfaceDefinition,Type FormatterGenericInterfaceDefinition){
            Debug.Assert(検索したいキーGenericInterfaceDefinition.IsInterface);
            if(!type0.IsGenericType||type0.GetGenericTypeDefinition()!=検索したいキーGenericInterfaceDefinition)return null;
            var GenericArguments=type0.GetGenericArguments();
            var FormatterGenericType=FormatterGenericInterfaceDefinition.MakeGenericType(GenericArguments);
            return (IJsonFormatter<T>)FormatterGenericType.GetValue("Instance");
        }
        IJsonFormatter<T>? RegisterType(Type type0,Type 検索したいキーGenericTypeDefinition){
            Debug.Assert(type.IsGenericType);
            Debug.Assert(!検索したいキーGenericTypeDefinition.IsInterface);
            if(type0.GetGenericTypeDefinition()!=検索したいキーGenericTypeDefinition)return null;
            return (IJsonFormatter<T>)type0.GetValue("InstanceUtf8Json");
        }
    }
    public void Clear(){
        //this.DictionaryTypeFormatter.Clear();
    }
}
