using System;
using System.Diagnostics;
using LinqDB.Helpers;
using Utf8Json;

using Generic = System.Collections.Generic;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json;
internal sealed class FormatterResolver:IJsonFormatterResolver{
    private readonly Generic.Dictionary<Type,IJsonFormatter> DictionaryTypeFormatter=new();















    public IJsonFormatter<T> GetFormatter<T>(){
        var type=typeof(T);
        if(this.DictionaryTypeFormatter.TryGetValue(type,out var value))
            return(IJsonFormatter<T>)value;

        if(type.IsDisplay()){
            
            
            return Return(Formatters.Others.DisplayClass<T>.Instance);
        }
        
        if(typeof(Delegate).IsAssignableFrom(type)){
            return(IJsonFormatter<T>)typeof(Formatters.Others.Delegate<>).MakeGenericType(type).GetValue("Instance");
        }else if(typeof(Expressions.LambdaExpression).IsAssignableFrom(type)){
            return(IJsonFormatter<T>)typeof(Formatters.ExpressionT<>).MakeGenericType(type).GetValue("Instance");
        }else if(type.IsGenericType) {
            IJsonFormatter<T>? Formatter=null;
            if(type.IsAnonymous()){

                return null!;
            }
            var Interfaces=type.GetInterfaces();
            foreach(var Interface in Interfaces)
                if((Formatter=RegisterInterface(Interface,typeof(Sets.IGrouping<,>),typeof(Formatters.Sets.IGrouping<,>))) is not null){

                    return Formatter;
                }
            foreach(var Interface in Interfaces)
                if((Formatter=RegisterInterface(Interface,typeof(System.Linq.IGrouping<,>),typeof(Formatters.Enumerables.IGrouping<,>))) is not null){

                    return Formatter;
                }
            foreach(var Interface in Interfaces)
                if((Formatter=RegisterInterface(Interface,typeof(Sets.IEnumerable<>),typeof(Formatters.Sets.IEnumerable<>))) is not null){

                    return Formatter;
                }
            foreach(var Interface in Interfaces)
                if((Formatter=RegisterInterface(Interface,typeof(Generic.IEnumerable<>),typeof(Formatters.Enumerables.IEnumerable<>))) is not null){

                    return Formatter;
                }
            var type0=type;
            do{
                if((Formatter=RegisterType(type0,typeof(Enumerables.GroupingList<, >)))is not null)break;
                if((Formatter=RegisterType(type0,typeof(Sets.GroupingSet        <, >)))is not null)break;
                if((Formatter=RegisterType(type0,typeof(Sets.SetGroupingList    <, >)))is not null)break;
                if((Formatter=RegisterType(type0,typeof(Sets.SetGroupingSet     <, >)))is not null)break;
                if((Formatter=RegisterType(type0,typeof(Sets.Set                <,,>)))is not null)break;
                if((Formatter=RegisterType(type0,typeof(Sets.Set                <, >)))is not null)break;
                if((Formatter=RegisterType(type0,typeof(Sets.Set                <  >)))is not null)break;
                do{
                    if(type0.BaseType is null)break;
                    type0=type0.BaseType!;
                }while(!type0.IsGenericType);
            } while(typeof(object)!=type0);


            
            return Formatter!;
        }
        return null!;
        IJsonFormatter<T> Return(object Formatter0){
            var result=(IJsonFormatter<T>)Formatter0;
            this.DictionaryTypeFormatter.Add(typeof(T),result);
            return result;
        }
        IJsonFormatter<T>?RegisterInterface(Type type0,Type 検索したいキーGenericInterfaceDefinition,Type FormatterGenericInterfaceDefinition){
            Debug.Assert(検索したいキーGenericInterfaceDefinition.IsInterface||FormatterGenericInterfaceDefinition.IsInterface);
            if(type0.IsGenericType&&type0.GetGenericTypeDefinition()==検索したいキーGenericInterfaceDefinition)
                return RegisterGeneric(type0,FormatterGenericInterfaceDefinition);
            foreach(var Interface in type.GetInterfaces())
                if(Interface.IsGenericType&&Interface.GetGenericTypeDefinition()==検索したいキーGenericInterfaceDefinition)
                    return RegisterGeneric(Interface,FormatterGenericInterfaceDefinition);
            return null;
            IJsonFormatter<T>RegisterGeneric(Type type0,Type FormatterGenericTypeDefinition){
                var GenericArguments=type0.GetGenericArguments();
                var FormatterGenericType=FormatterGenericTypeDefinition.MakeGenericType(GenericArguments);
                var Instance=(IJsonFormatter<T>)FormatterGenericType.GetValue("Instance");
                this.DictionaryTypeFormatter.Add(typeof(T),Instance);
                return Instance;
            }
        }
        IJsonFormatter<T>? RegisterType(Type type0,Type 検索したいキーGenericTypeDefinition){
            Debug.Assert(type.IsGenericType);
            Debug.Assert(!検索したいキーGenericTypeDefinition.IsInterface);
            if(type0.GetGenericTypeDefinition()!=検索したいキーGenericTypeDefinition)return null;
            var Formatter_T=(IJsonFormatter<T>)type0.GetValue("InstanceUtf8Json");
            this.DictionaryTypeFormatter.Add(typeof(T),Formatter_T);
            return Formatter_T;
        }
    }
    public void Clear()=>this.DictionaryTypeFormatter.Clear();
}
/*
            var FormatterGenericType=FormatterGenericInterfaceDefinition.MakeGenericType(type0.GetGenericArguments());
            var Formatter=Activator.CreateInstance(FormatterGenericType)!;
            var Formatter_T=(IJsonFormatter<T>)Formatter;
            this.DictionaryTypeFormatter.Add(typeof(T),Formatter_T);
            return Formatter_T;
*/