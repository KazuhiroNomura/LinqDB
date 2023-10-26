using System;
using System.Diagnostics;
using System.Reflection;
using LinqDB.Helpers;
using MessagePack;
using MessagePack.Formatters;
using Generic = System.Collections.Generic;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack;
internal sealed class FormatterResolver:IFormatterResolver {
    private readonly Generic.Dictionary<Type,IMessagePackFormatter> DictionaryTypeFormatter = new();
    public IMessagePackFormatter<T> GetFormatter<T>() {
        var type=typeof(T);
        if(type.GetCustomAttribute(typeof(MessagePackObjectAttribute))!=null)return null!;
        if(this.DictionaryTypeFormatter.TryGetValue(type,out var value))return(IMessagePackFormatter<T>)value;
        if(type.IsArray)
            return default!;
            
            
        if(type.IsAnonymous())
            return default!;
        if(type.IsDisplay())
            return Return(Formatters.Others.DisplayClass<T>.Instance);
        if(typeof(Delegate).IsAssignableFrom(type))
            return Return((IMessagePackFormatter<T>)typeof(Formatters.Others.Delegate<>).MakeGenericType(type).GetValue("Instance"));
        if(type.IsGenericType) {
            if(typeof(Expressions.LambdaExpression).IsAssignableFrom(type))
                return Return((IMessagePackFormatter<T>)typeof(Formatters.ExpressionT<>    ).MakeGenericType(type).GetValue("Instance"));
            IMessagePackFormatter<T>? Formatter=null;
            if(type.IsInterface){
                if((Formatter=RegisterInterface(type,typeof(Sets.IGrouping       <,>),typeof(Formatters.Sets.IGrouping         <,>)))is not null)return Return(Formatter);
                if((Formatter=RegisterInterface(type,typeof(System.Linq.IGrouping<,>),typeof(Formatters.Enumerables.IGrouping  <,>)))is not null)return Return(Formatter);
                if((Formatter=RegisterInterface(type,typeof(Sets.IEnumerable     < >),typeof(Formatters.Sets.IEnumerable       < >)))is not null)return Return(Formatter);
                if((Formatter=RegisterInterface(type,typeof(Generic.IEnumerable  < >),typeof(Formatters.Enumerables.IEnumerable< >)))is not null)return Return(Formatter);
            }else{
                if((Formatter=RegisterType(type,typeof(Enumerables.GroupingList<, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.GroupingSet        <, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.SetGroupingList    <, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.SetGroupingSet     <, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.Set                <,,>))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.Set                <, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.Set                <  >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Enumerables.List        <  >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.HashSet            <  >))) is not null) return Return(Formatter);
            }
        }
        {
            if(type.GetIEnumerable1(out var GenericArguments)){
                var GenericArguments_0 = GenericArguments[0];
                var FormatterGenericType = typeof(Formatters.Enumerables.IEnumerable<>).MakeGenericType(GenericArguments_0);
                return Return(FormatterGenericType.GetValue(nameof(Formatters.Enumerables.IEnumerable<int>.Instance)));
            }
        }
        return default!;
        IMessagePackFormatter<T> Return(object Formatter0){
            var result=(IMessagePackFormatter<T>)Formatter0;
            this.DictionaryTypeFormatter.Add(typeof(T),result);
            return result;
        }
        IMessagePackFormatter<T>? RegisterInterface(Type type0,Type 検索したいキーGenericInterfaceDefinition,Type FormatterGenericInterfaceDefinition) {
            Debug.Assert(検索したいキーGenericInterfaceDefinition.IsInterface);
            if(!type0.IsGenericType||type0.GetGenericTypeDefinition()!=検索したいキーGenericInterfaceDefinition)return null;
            var GenericArguments = type0.GetGenericArguments();
            var FormatterGenericType = FormatterGenericInterfaceDefinition.MakeGenericType(GenericArguments);
            return (IMessagePackFormatter<T>)FormatterGenericType.GetValue("Instance");
        }
        IMessagePackFormatter<T>? RegisterType(Type type0,Type 検索したいキーGenericTypeDefinition) {
            Debug.Assert(type0.IsGenericType);
            Debug.Assert(!検索したいキーGenericTypeDefinition.IsInterface);
            if(type0.GetGenericTypeDefinition()!=検索したいキーGenericTypeDefinition) return null;
            return (IMessagePackFormatter<T>)type0.GetValue("InstanceMessagePack");
        }
    }
    public void Clear()=>this.DictionaryTypeFormatter.Clear();
}
