using System;
using System.Diagnostics;
using LinqDB.Helpers;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack;
internal sealed class FormatterResolver:IFormatterResolver {
    public static readonly FormatterResolver Instance = new();
    private readonly System.Collections.Generic.Dictionary<System.Type,IMessagePackFormatter> DictionaryTypeFormatter = new();
    public IMessagePackFormatter<T> GetFormatter<T>() {
        var type=typeof(T);
        if(this.DictionaryTypeFormatter.TryGetValue(type,out var Formatter))return(IMessagePackFormatter<T>)Formatter;
        if(type.IsDisplay())return Return(Formatters.Others.DisplayClass<T>.Instance);

        if(type.IsArray) {
            
        }else if(type.IsGenericType) {
            if(type.IsAnonymous()) {
                
            } else if(typeof(Expressions.LambdaExpression).IsAssignableFrom(type)) {
                var FormatterType = typeof(Formatters.ExpressionT<>).MakeGenericType(type);
                var Instance=FormatterType.GetField("Instance")!;
                var FormatterT=(IMessagePackFormatter<T>)Instance.GetValue(null)!;
                return Return(FormatterT);
            }else if(type.IsInterface){
                IMessagePackFormatter<T>?Formatter_T;
                if((Formatter_T=RegisterInterface(typeof(Sets.IGrouping       <,>),typeof(Formatters.Sets.GroupingSet    <,>)))is not null)return Formatter_T;
                if((Formatter_T=RegisterInterface(typeof(System.Linq.IGrouping<,>),typeof(Formatters.Sets.GroupingAscList<,>)))is not null)return Formatter_T;
                if((Formatter_T=RegisterInterface(typeof(Sets.IEnumerable     < >),typeof(Formatters.Sets.IEnumerable    < >)))is not null)return Formatter_T;
                if((Formatter_T=RegisterInterface(typeof(Sets.IEnumerable        ),typeof(Formatters.Sets.IEnumerable       )))is not null)return Formatter_T;
            }else{
                IMessagePackFormatter<T>?Formatter_T;
                var type0=type;
                do{
                    if((Formatter_T=RegisterType(type0,typeof(Sets.GroupingSet       <, >),typeof(Formatters.Sets.GroupingSet       <, >)))is not null)return Formatter_T;
                    if((Formatter_T=RegisterType(type0,typeof(Sets.SetGroupingSet    <, >),typeof(Formatters.Sets.SetGroupingSet    <, >)))is not null)return Formatter_T;
                    if((Formatter_T=RegisterType(type0,typeof(Sets.Set               <,,>),typeof(Formatters.Sets.Set               <,,>)))is not null)return Formatter_T;
                    if((Formatter_T=RegisterType(type0,typeof(Sets.Set               <, >),typeof(Formatters.Sets.Set               <, >)))is not null)return Formatter_T;
                    if((Formatter_T=RegisterType(type0,typeof(Sets.Set               <  >),typeof(Formatters.Sets.Set               <  >)))is not null)return Formatter_T;
                    do{
                        if(type0.BaseType is null) return default!;
                        type0=type0.BaseType!;
                    }while(!type0.IsGenericType);
                } while(typeof(object)!=type0);
            }
        }
        return default!;
        IMessagePackFormatter<T> Return(object Formatter0){
            var result=(IMessagePackFormatter<T>)Formatter0;
            this.DictionaryTypeFormatter.Add(typeof(T),result);
            return result;
        }
        IMessagePackFormatter<T>?RegisterInterface(System.Type 検索したいキーGenericInterfaceDefinition,System.Type FormatterGenericInterfaceDefinition){
            Debug.Assert(検索したいキーGenericInterfaceDefinition.IsInterface||FormatterGenericInterfaceDefinition.IsInterface);
            if(type.IsGenericType&&type.GetGenericTypeDefinition()==検索したいキーGenericInterfaceDefinition){
                return RegisterGeneric(FormatterGenericInterfaceDefinition);
                
            }
            foreach(var Interface in type.GetInterfaces()){
                if(!Interface.IsGenericType)continue;
                if(Interface.GetGenericTypeDefinition()==検索したいキーGenericInterfaceDefinition){
                    return RegisterGeneric(FormatterGenericInterfaceDefinition);
                    
                }
            }
            return null;
        }
        IMessagePackFormatter<T>?RegisterType(Type type,Type 検索したいキーGenericTypeDefinition,Type FormatterTypeDifinition){
            Debug.Assert(type.IsGenericType);
            Debug.Assert(!検索したいキーGenericTypeDefinition.IsInterface);
            if(type.GetGenericTypeDefinition()!=検索したいキーGenericTypeDefinition)return null;
            var FormatterGenericType=FormatterTypeDifinition.MakeGenericType(type.GetGenericArguments());
            var Formatter_T=(IMessagePackFormatter<T>)FormatterGenericType.GetField("Instance")!.GetValue(null)!;
            this.DictionaryTypeFormatter.Add(typeof(T),Formatter_T);
            return Formatter_T;
        }
        IMessagePackFormatter<T>RegisterGeneric(Type FormatterGenericTypeDefinition){
            var GenericArguments=type.GetGenericArguments();
            var FormatterGenericType=FormatterGenericTypeDefinition.MakeGenericType(GenericArguments);
            var Formatter_T=(IMessagePackFormatter<T>)FormatterGenericType.GetValue("Instance")!;
            this.DictionaryTypeFormatter.Add(typeof(T),Formatter_T);
            return Formatter_T;
        }
    }
    public void Clear()=>this.DictionaryTypeFormatter.Clear();
}
