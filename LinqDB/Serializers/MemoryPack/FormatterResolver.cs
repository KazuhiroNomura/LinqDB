using System;
using System.Diagnostics;
using LinqDB.Helpers;


using Generic = System.Collections.Generic;
using Expressions = System.Linq.Expressions;
using MemoryPack;

namespace LinqDB.Serializers.MemoryPack;
internal static class FormatterResolver {


    public static MemoryPackFormatter<T>? GetDisplayAnonymousFormatter<T>(){
        if(typeof(T).IsDisplay())return Formatters.Others.DisplayClass<T>.Instance;
        if(typeof(T).IsAnonymous()){
            foreach(var GenericArgument in typeof(T).GetGenericArguments()) GetAnonymousDisplaySetFormatter(GenericArgument);
            return Formatters.Others.Anonymous<T>.Instance;
        }
        return null;
    }
    public static object? GetDisplayAnonymous以外Formatter(Type type) {
        Debug.Assert(!(type.IsDisplay()||type.IsAnonymous()));


        if(type.IsArray)return GetAnonymousDisplaySetFormatter(type.GetElementType());
        if(type.IsGenericType) {
            foreach(var GenericArgument in type.GetGenericArguments())GetAnonymousDisplaySetFormatter(GenericArgument);
            if(typeof(Expressions.LambdaExpression).IsAssignableFrom(type))return RegisterAnonymousDisplay(type,typeof(Formatters.ExpressionT<>));
            if(type.IsInterface){
                object? Formatter;
                if((Formatter=RegisterInterface(type,typeof(Sets.IGrouping       <,>),typeof(Formatters.Sets.IGrouping         <,>)))is not null)return Formatter;
                if((Formatter=RegisterInterface(type,typeof(System.Linq.IGrouping<,>),typeof(Formatters.Enumerables.IGrouping  <,>)))is not null)return Formatter;
                if((Formatter=RegisterInterface(type,typeof(Sets.IEnumerable     < >),typeof(Formatters.Sets.IEnumerable       < >)))is not null)return Formatter;
                if((Formatter=RegisterInterface(type,typeof(Generic.IEnumerable  < >),typeof(Formatters.Enumerables.IEnumerable< >)))is not null)return Formatter;
            }else{
                object? Formatter;
                var type0=type;
                do{
                    if((Formatter=RegisterType(type0,typeof(Enumerables.GroupingList<, >)))is not null)return Formatter;
                    if((Formatter=RegisterType(type0,typeof(Sets.GroupingSet        <, >)))is not null)return Formatter;
                    if((Formatter=RegisterType(type0,typeof(Sets.SetGroupingList    <, >)))is not null)return Formatter;
                    if((Formatter=RegisterType(type0,typeof(Sets.SetGroupingSet     <, >)))is not null)return Formatter;
                    if((Formatter=RegisterType(type0,typeof(Sets.Set                <,,>)))is not null)return Formatter;
                    if((Formatter=RegisterType(type0,typeof(Sets.Set                <, >)))is not null)return Formatter;
                    if((Formatter=RegisterType(type0,typeof(Sets.Set                <  >)))is not null)return Formatter;
                    do{
                        if(type0==typeof(object)) return null;
                        type0=type0.BaseType!;
                    }while(!type0.IsGenericType);
                } while(typeof(object)!=type0);
            }
        }
        return null;
        
        
        
        
        
        
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
                var Register = Serializer.Register.MakeGenericMethod(type0);
                var Instance=FormatterGenericType.GetValue("Instance")!;
                Register.Invoke(null,new object?[]{Instance});
                return Instance;
            }
        }
        static object? RegisterType(Type type0,Type 検索したいキーGenericTypeDefinition){
            Debug.Assert(type0.IsGenericType);
            Debug.Assert(!検索したいキーGenericTypeDefinition.IsInterface);
            if(type0.GetGenericTypeDefinition()!=検索したいキーGenericTypeDefinition)return null;
            var Formatter0=type0.GetValue("InstanceMemoryPack");
            var Register = Serializer.Register.MakeGenericMethod(type0);
            Register.Invoke(null,new object?[]{Formatter0});
            return Formatter0;
        }
    }
    private static object RegisterAnonymousDisplay(Type type,Type FormatterGenericTypeDefinition){
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
    public static object? GetAnonymousDisplaySetFormatter(Type type){

        if(type.IsDisplay())return RegisterAnonymousDisplay(type,typeof(Formatters.Others.DisplayClass<>));
        if(type.IsArray){
            GetAnonymousDisplaySetFormatter(type.GetElementType());
            return null;
        }
        if(type.IsAnonymous()) {
            foreach(var GenericArgument in type.GetGenericArguments()) GetAnonymousDisplaySetFormatter(GenericArgument);
            return RegisterAnonymousDisplay(type,typeof(Formatters.Others.Anonymous<>));
        }
        return GetDisplayAnonymous以外Formatter(type);
    }
}
