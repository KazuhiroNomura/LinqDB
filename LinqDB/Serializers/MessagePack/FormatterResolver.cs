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
    public static readonly FormatterResolver Instance = new();
    private readonly Generic.Dictionary<Type,IMessagePackFormatter> DictionaryTypeFormatter = new();
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    public IMessagePackFormatter<T> GetFormatter<T>() {
        var type=typeof(T);
        if(type.GetCustomAttribute(typeof(MessagePackObjectAttribute))!=null)return null!;
        if(this.DictionaryTypeFormatter.TryGetValue(type,out var value))return(IMessagePackFormatter<T>)value;

        if(type.IsDisplay())
            return Return(Formatters.Others.DisplayClass<T>.Instance);



        IMessagePackFormatter<T>? Formatter=null;
        if(typeof(Delegate).IsAssignableFrom(type)){
            Formatter=(IMessagePackFormatter<T>)typeof(Formatters.Others.Delegate<>).MakeGenericType(type).GetValue("Instance");
        }else if(typeof(Expressions.LambdaExpression).IsAssignableFrom(type)){
            Formatter=(IMessagePackFormatter<T>)typeof(Formatters.ExpressionT<>    ).MakeGenericType(type).GetValue("Instance");
        }else if(type.IsGenericType) {
            if(type.IsAnonymous()){
                
            }else if(type.IsInterface){
                if((Formatter=RegisterInterface(type,typeof(Sets.IGrouping       <,>),typeof(Formatters.Sets.IGrouping         <,>)))is not null)goto 発見;
                if((Formatter=RegisterInterface(type,typeof(System.Linq.IGrouping<,>),typeof(Formatters.Enumerables.IGrouping  <,>)))is not null)goto 発見;
                if((Formatter=RegisterInterface(type,typeof(Sets.IEnumerable     < >),typeof(Formatters.Sets.IEnumerable       < >)))is not null)goto 発見;
                if((Formatter=RegisterInterface(type,typeof(Generic.IEnumerable  < >),typeof(Formatters.Enumerables.IEnumerable< >)))is not null)goto 発見;
            }else{
                var type0=type;
                if((Formatter=RegisterType(type0,typeof(Enumerables.GroupingList<, >)))is not null)goto 発見;
                if((Formatter=RegisterType(type0,typeof(Sets.GroupingSet        <, >)))is not null)goto 発見;
                if((Formatter=RegisterType(type0,typeof(Sets.SetGroupingList    <, >)))is not null)goto 発見;
                if((Formatter=RegisterType(type0,typeof(Sets.SetGroupingSet     <, >)))is not null)goto 発見;
                if((Formatter=RegisterType(type0,typeof(Sets.Set                <,,>)))is not null)goto 発見;
                if((Formatter=RegisterType(type0,typeof(Sets.Set                <, >)))is not null)goto 発見;
                if((Formatter=RegisterType(type0,typeof(Sets.Set                <  >)))is not null)goto 発見;
                Type? Interface;
                if((Interface=type.GetInterface(CommonLibrary.Generic_ICollection1_FullName))!=null){
                    var GenericArguments=Interface.GetGenericArguments();
                    var GenericArguments_0=GenericArguments[0];
                    var FormatterGenericType=typeof(GenericCollectionFormatter<,>).MakeGenericType(GenericArguments_0,type);
                    Formatter=(IMessagePackFormatter<T>)Activator.CreateInstance(FormatterGenericType)!;


                    return Formatter;
                }
                if((Interface=type.GetInterface(CommonLibrary.Generic_IEnumerable1_FullName))!=null){
                    var GenericArguments=Interface.GetGenericArguments();
                    var GenericArguments_0=GenericArguments[0];
                    //var FormatterGenericType=typeof(InterfaceEnumerableFormatter<>).MakeGenericType(GenericArguments_0);
                    InterfaceEnumerableFormatter<int> a0=default!;
                    CollectionFormatterBase<int,int[],Generic.IEnumerable<int>>a1=a0;
                    CollectionFormatterBase<int,int[],Generic.IEnumerator<int>,Generic.IEnumerable<int>>a2=a1;
                    IMessagePackFormatter<Generic.IEnumerable<int>?> a3=a2;
                    var FormatterGenericType=typeof(InterfaceEnumerableFormatter<>).MakeGenericType(GenericArguments_0);
                    Formatter=(IMessagePackFormatter<T>)Activator.CreateInstance(FormatterGenericType)!;

                        
                    return Formatter;
                }

            }
    発見: ;

        }
        return Formatter!;
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
            var Instance=(IMessagePackFormatter<T>)FormatterGenericType.GetValue("Instance");
            this.DictionaryTypeFormatter.Add(typeof(T),Instance);
            return Instance;
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
