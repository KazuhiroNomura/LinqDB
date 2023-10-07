using System;
using System.Diagnostics;
using LinqDB.Helpers;
using MessagePack;
using MessagePack.Formatters;
using Generic = System.Collections.Generic;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack;
internal sealed class FormatterResolver:IFormatterResolver {
    public static readonly FormatterResolver Instance = new();
    private readonly Generic.Dictionary<Type,IMessagePackFormatter> DictionaryTypeFormatter = new();
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    public IMessagePackFormatter<T> GetFormatter<T>() {
        var type=typeof(T);
        if(this.DictionaryTypeFormatter.TryGetValue(type,out var value))return(IMessagePackFormatter<T>)value;
        if(type.IsDisplay())return Return(Formatters.Others.DisplayClass<T>.Instance);








        if(typeof(Delegate).IsAssignableFrom(type))return(IMessagePackFormatter<T>)typeof(Formatters.Others.Delegate<>).MakeGenericType(type).GetValue("Instance");
        if(typeof(Expressions.LambdaExpression).IsAssignableFrom(type))return(IMessagePackFormatter<T>)typeof(Formatters.ExpressionT<>    ).MakeGenericType(type).GetValue("Instance");
        if(type.IsGenericType) {
            IMessagePackFormatter<T>? Formatter=null;
            if(type.IsAnonymous()){

            } else if(typeof(Delegate).IsAssignableFrom(type)) {
                Formatter=(IMessagePackFormatter<T>)typeof(Formatters.Others.Delegate<>).MakeGenericType(type).GetValue("Instance");
            } else if(typeof(Expressions.LambdaExpression).IsAssignableFrom(type)) {
                Formatter=(IMessagePackFormatter<T>)typeof(Formatters.ExpressionT<>    ).MakeGenericType(type).GetValue("Instance");
            }else if(type.IsInterface){
                if      ((Formatter=RegisterInterface(type,typeof(Sets.IGrouping       <,>),typeof(Formatters.Sets.IGrouping         <,>)))is not null){
                }else if((Formatter=RegisterInterface(type,typeof(System.Linq.IGrouping<,>),typeof(Formatters.Enumerables.IGrouping  <,>)))is not null){
                }else if((Formatter=RegisterInterface(type,typeof(Sets.IEnumerable     < >),typeof(Formatters.Sets.IEnumerable       < >)))is not null){
                }else if((Formatter=RegisterInterface(type,typeof(Generic.IEnumerable  < >),typeof(Formatters.Enumerables.IEnumerable< >)))is not null){
                }
            }else{
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
            }
            
            return Formatter!;
        }
        return null!;
        IMessagePackFormatter<T> Return(object Formatter0){
            var result=(IMessagePackFormatter<T>)Formatter0;
            this.DictionaryTypeFormatter.Add(typeof(T),result);
            return result;
        }
        IMessagePackFormatter<T>? RegisterInterface(Type type0,Type 検索したいキーGenericInterfaceDefinition,Type FormatterGenericInterfaceDefinition) {
            Debug.Assert(検索したいキーGenericInterfaceDefinition.IsInterface||FormatterGenericInterfaceDefinition.IsInterface);
            if(type0.IsGenericType&&type.GetGenericTypeDefinition()==検索したいキーGenericInterfaceDefinition)
                return RegisterGeneric(type0,FormatterGenericInterfaceDefinition);
            foreach(var Interface in type0.GetInterfaces())
                if(Interface.IsGenericType&&Interface.GetGenericTypeDefinition()==検索したいキーGenericInterfaceDefinition)
                    return RegisterGeneric(Interface,FormatterGenericInterfaceDefinition);
            return null;
            IMessagePackFormatter<T> RegisterGeneric(Type type1,Type FormatterGenericTypeDefinition){
                var GenericArguments = type1.GetGenericArguments();
                var FormatterGenericType = FormatterGenericTypeDefinition.MakeGenericType(GenericArguments);
                var Instance=(IMessagePackFormatter<T>)FormatterGenericType.GetValue("Instance");
                this.DictionaryTypeFormatter.Add(typeof(T),Instance);
                return Instance;
            }
        }
        IMessagePackFormatter<T>? RegisterType(Type type0,Type 検索したいキーGenericTypeDefinition) {
            Debug.Assert(type0.IsGenericType);
            Debug.Assert(!検索したいキーGenericTypeDefinition.IsInterface);
            if(type0.GetGenericTypeDefinition()!=検索したいキーGenericTypeDefinition) return null;
            var Formatter_T=(IMessagePackFormatter<T>)type0.GetValue("InstanceMessagePack");
            this.DictionaryTypeFormatter.Add(typeof(T),Formatter_T);
            return Formatter_T;
        }
    }
    public void Clear()=>this.DictionaryTypeFormatter.Clear();
}
