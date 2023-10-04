using System;
using System.Diagnostics;
using LinqDB.Helpers;
using Utf8Json;

using Generic = System.Collections.Generic;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json;
internal sealed class FormatterResolver:IJsonFormatterResolver{
    private readonly System.Collections.Generic.Dictionary<System.Type,IJsonFormatter> DictionaryTypeFormatter=new();

    public IJsonFormatter<T> GetFormatter<T>(){
        var type=typeof(T);
        if(this.DictionaryTypeFormatter.TryGetValue(type,out var Formatter))return(IJsonFormatter<T>)Formatter;
        if(type.IsDisplay())return Return(Formatters.Others.DisplayClass<T>.Instance);

        if(type.IsArray) {
            
        }else if(type.IsGenericType) {
            if(type.IsAnonymous()) {

            } else if(typeof(Expressions.LambdaExpression).IsAssignableFrom(type)) {
                var FormatterType = typeof(Formatters.ExpressionT<>).MakeGenericType(type);
                var Instance=FormatterType.GetField("Instance")!;
                var FormatterT=(IJsonFormatter<T>)Instance.GetValue(null)!;
                return Return(FormatterT);
            }else if(type.IsInterface){
                IJsonFormatter<T>?Formatter_T;
                if((Formatter_T=RegisterInterface(type,typeof(Sets.IGrouping       <,>),typeof(Formatters.Sets.IGrouping         <,>)))is not null)return Formatter_T;
                if((Formatter_T=RegisterInterface(type,typeof(System.Linq.IGrouping<,>),typeof(Formatters.Enumerables.IGrouping  <,>)))is not null)return Formatter_T;
                if((Formatter_T=RegisterInterface(type,typeof(Sets.IEnumerable     < >),typeof(Formatters.Sets.IEnumerable       < >)))is not null)return Formatter_T;
                if((Formatter_T=RegisterInterface(type,typeof(Generic.IEnumerable  < >),typeof(Formatters.Enumerables.IEnumerable< >)))is not null)return Formatter_T;
            }else{
                IJsonFormatter<T>? Formatter_T;
                var type0=type;
                do{
                    if((Formatter_T=RegisterType(type0,typeof(Enumerables.GroupingList<, >)))is not null)return Formatter_T;
                    if((Formatter_T=RegisterType(type0,typeof(Sets.GroupingSet        <, >)))is not null)return Formatter_T;
                    if((Formatter_T=RegisterType(type0,typeof(Sets.SetGroupingList    <, >)))is not null)return Formatter_T;
                    if((Formatter_T=RegisterType(type0,typeof(Sets.SetGroupingSet     <, >)))is not null)return Formatter_T;
                    if((Formatter_T=RegisterType(type0,typeof(Sets.Set                <,,>)))is not null)return Formatter_T;
                    if((Formatter_T=RegisterType(type0,typeof(Sets.Set                <, >)))is not null)return Formatter_T;
                    if((Formatter_T=RegisterType(type0,typeof(Sets.Set                <  >)))is not null)return Formatter_T;
                    do{
                        if(type0.BaseType is null) return default!;
                        type0=type0.BaseType!;
                    }while(!type0.IsGenericType);
                } while(typeof(object)!=type0);
            }            
            
            
        }
        return default!;
        IJsonFormatter<T> Return(object Formatter0){
            var result=(IJsonFormatter<T>)Formatter0;
            this.DictionaryTypeFormatter.Add(typeof(T),result);
            return result;
        }
        IJsonFormatter<T>?RegisterInterface(Type type0,System.Type 検索したいキーGenericInterfaceDefinition,System.Type FormatterGenericInterfaceDefinition){
            Debug.Assert(検索したいキーGenericInterfaceDefinition.IsInterface||FormatterGenericInterfaceDefinition.IsInterface);
            if(type0.IsGenericType&&type0.GetGenericTypeDefinition()==検索したいキーGenericInterfaceDefinition){
                return RegisterGeneric(type0,FormatterGenericInterfaceDefinition);

            }
            foreach(var Interface in type.GetInterfaces()){
                if(!Interface.IsGenericType)continue;
                if(Interface.GetGenericTypeDefinition()==検索したいキーGenericInterfaceDefinition){
                    return RegisterGeneric(Interface,FormatterGenericInterfaceDefinition);
                    
                }
            }
            return null;
        }
        IJsonFormatter<T>? RegisterType(Type type0,Type 検索したいキーGenericTypeDefinition){
            Debug.Assert(type.IsGenericType);
            Debug.Assert(!検索したいキーGenericTypeDefinition.IsInterface);
            if(type0.GetGenericTypeDefinition()!=検索したいキーGenericTypeDefinition)return null;
            var Formatter0=type0.GetValue("InstanceUtf8Json");
            var Formatter_T=(IJsonFormatter<T>)Formatter0;
            this.DictionaryTypeFormatter.Add(typeof(T),Formatter_T);
            return Formatter_T;
        }
        IJsonFormatter<T>RegisterGeneric(Type type0,Type FormatterGenericTypeDefinition){
            var GenericArguments=type0.GetGenericArguments();
            var FormatterGenericType=FormatterGenericTypeDefinition.MakeGenericType(GenericArguments);
            var Formatter_T=(IJsonFormatter<T>)FormatterGenericType.GetValue("Instance")!;
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