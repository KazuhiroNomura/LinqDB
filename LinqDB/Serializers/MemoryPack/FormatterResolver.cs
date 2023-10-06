using System;
using System.Diagnostics;
using System.Reflection;
using LinqDB.Helpers;
using MemoryPack;

using Generic = System.Collections.Generic;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack;
internal static class FormatterResolver {
    public static MemoryPackFormatter<T>? GetRegisteredFormatter<T>(){
        if(typeof(T).IsDisplay()){
            foreach(var Field in typeof(T).GetFields()) GetRegisteredFormatter(Field.FieldType);
            var Instance=Formatters.Others.DisplayClass<T>.Instance;
            MemoryPackFormatterProvider.Register(Instance);
            return Instance;
        }
        if(typeof(T).IsAnonymous()){
            foreach(var GenericArgument in typeof(T).GetGenericArguments()) GetRegisteredFormatter(GenericArgument);
            var Instance=Formatters.Others.Anonymous<T>.Instance;
            MemoryPackFormatterProvider.Register(Instance);
            return Instance;
        }
        return null;
    }
    public static object? GetRegisteredFormatter(Type type) {
        
        
        if(type.IsArray){
            GetRegisteredFormatter(type.GetElementType());
            return null;
        }
        if(type.IsDisplay()){
            var value=Register(type,typeof(Formatters.Others.DisplayClass<>));
            foreach(var Field in type.GetFields(BindingFlags.Instance|BindingFlags.Public)) GetRegisteredFormatter(Field.FieldType);
            return value;
        }
        object? Formatter=null;
        if(typeof(Delegate).IsAssignableFrom(type)) {
            Formatter=Register(type,typeof(Formatters.Others.Delegate<>));
        }else if(typeof(Expressions.LambdaExpression).IsAssignableFrom(type)){
            Formatter=Register(type,typeof(Formatters.ExpressionT<>));
        }else if(type.IsGenericType) {
            if(type.IsAnonymous()){
                Formatter=Register(type,typeof(Formatters.Others.Anonymous<>));
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
                        if(type0==typeof(object))break;
                        type0=type0.BaseType!;
                    }while(!type0.IsGenericType);
                } while(typeof(object)!=type0);
            }
            foreach(var GenericArgument in type.GetGenericArguments())GetRegisteredFormatter(GenericArgument);
        }
        return Formatter;

        
        
        
        
        static object? RegisterInterface(Type type,Type 検索したいキーGenericInterfaceDefinition,Type FormatterGenericInterfaceDefinition){
            Debug.Assert(検索したいキーGenericInterfaceDefinition.IsInterface||FormatterGenericInterfaceDefinition.IsInterface);
            if(type.IsGenericType&&type.GetGenericTypeDefinition()==検索したいキーGenericInterfaceDefinition)
                return RegisterGeneric(type,FormatterGenericInterfaceDefinition);
            foreach(var Interface in type.GetInterfaces())
                if(Interface.IsGenericType&&Interface.GetGenericTypeDefinition()==検索したいキーGenericInterfaceDefinition)
                    return RegisterGeneric(Interface,FormatterGenericInterfaceDefinition);
            return null;
            static object RegisterGeneric(Type type0,Type FormatterGenericTypeDefinition){
                var GenericArguments=type0.GetGenericArguments();
                var FormatterGenericType=FormatterGenericTypeDefinition.MakeGenericType(GenericArguments);
                var Instance=FormatterGenericType.GetValue("Instance")!;
                Serializer.Register.MakeGenericMethod(type0).Invoke(null,new object?[]{Instance});
                return Instance;
            }
        }
        static object? RegisterType(Type type0,Type 検索したいキーGenericTypeDefinition){
            Debug.Assert(type0.IsGenericType);
            Debug.Assert(!検索したいキーGenericTypeDefinition.IsInterface);
            if(type0.GetGenericTypeDefinition()!=検索したいキーGenericTypeDefinition)return null;
            var Formatter_T=type0.GetValue("InstanceMemoryPack");
            Serializer.Register.MakeGenericMethod(type0).Invoke(null,new object?[]{Formatter_T});
            return Formatter_T;
        }
    }
    private static object Register(Type type,Type FormatterGenericTypeDefinition){
        //MemoryPackFormatterProvider.Register<Set<int>>(default(MemoryPackFormatter<Set<int>>)!);
        //MemoryPackFormatterProvider.Register<Set<int>>(Formatters.Sets.Set<int>.Instance);
        //MemoryPackFormatterProvider.Register<Formatters.Sets.Set<Set<int>>>(Formatters.Sets.Set<Set<int>>.Instance);
        var GenericArguments=new []{type};
        var FormatterGenericType=FormatterGenericTypeDefinition.MakeGenericType(GenericArguments);
        var Register = Serializer.Register.MakeGenericMethod(GenericArguments);
        var Instance=FormatterGenericType.GetValue("Instance");
        //var Register = Serializer.Register.MakeGenericMethod(Instance.FieldType);
        Register.Invoke(null,new object?[]{Instance});
        return Instance;
    }
    //public static object? GetAnonymousDisplaySetFormatter(Type type){

    //    if(type.IsDisplay())return Register(type,typeof(Formatters.Others.DisplayClass<>));
    //    if(type.IsArray){
    //        GetAnonymousDisplaySetFormatter(type.GetElementType());
    //        return null;
    //    }
    //    if(type.IsAnonymous()) {
    //        foreach(var GenericArgument in type.GetGenericArguments()) GetAnonymousDisplaySetFormatter(GenericArgument);
    //        return Register(type,typeof(Formatters.Others.Anonymous<>));
    //    }
    //    return GetRegisteredFormatter(type);
    //}
}
