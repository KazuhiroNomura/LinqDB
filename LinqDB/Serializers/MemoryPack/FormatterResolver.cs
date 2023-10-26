using System;
using System.Diagnostics;
using LinqDB.Helpers;
using MemoryPack;
using MemoryPack.Formatters;
using Generic = System.Collections.Generic;
using Expressions = System.Linq.Expressions;
using System.Runtime.Serialization;

namespace LinqDB.Serializers.MemoryPack;
internal static class FormatterResolver {
    public static MemoryPackFormatter<T>? GetRegisteredFormatter<T>(){
        var type=typeof(T);
        if(type.IsArray){
            GetRegisteredFormatter(type.GetElementType());
            return null;
        }
        if(type.IsAnonymous())
            return Return(Register(type,typeof(Formatters.Others.Anonymous<>)));
        if(type.IsDisplay())
            return Return(Register(type,typeof(Formatters.Others.DisplayClass<>)));
        if(typeof(Delegate).IsAssignableFrom(type))
            return Return(Register(type,typeof(Formatters.Others.Delegate<>)));
        if(type.IsGenericType) {
            if(typeof(Expressions.LambdaExpression).IsAssignableFrom(type))
                return Return(Register(type,typeof(Formatters.ExpressionT<>)));
            object? Formatter;
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
        return default;
        MemoryPackFormatter<T> Return(object Formatter0){
            
            foreach(var GenericArgument in type.GetGenericArguments())GetRegisteredFormatter(GenericArgument);
            return (MemoryPackFormatter<T>)Formatter0;
        }







    }
    private static object? RegisterInterface(Type type0,Type 検索したいキーGenericInterfaceDefinition,Type FormatterGenericInterfaceDefinition){
        Debug.Assert(検索したいキーGenericInterfaceDefinition.IsInterface);
        if(!type0.IsGenericType||type0.GetGenericTypeDefinition()!=検索したいキーGenericInterfaceDefinition)return null;
        var GenericArguments=type0.GetGenericArguments();
        var FormatterGenericType=FormatterGenericInterfaceDefinition.MakeGenericType(GenericArguments);
        var Instance=FormatterGenericType.GetValue("Instance");
        Serializer.Register.MakeGenericMethod(type0).Invoke(null,new object?[]{Instance});
        return Instance;
    }
    static object? RegisterType(Type type0,Type 検索したいキーGenericTypeDefinition) {
        Debug.Assert(type0.IsGenericType);
        Debug.Assert(!検索したいキーGenericTypeDefinition.IsInterface);
        if(type0.GetGenericTypeDefinition()!=検索したいキーGenericTypeDefinition) return null;
        var Formatter = type0.GetValue("InstanceMemoryPack");
        Serializer.Register.MakeGenericMethod(type0).Invoke(null,new object?[] { Formatter });
        return Formatter;
    }
    public static object? GetRegisteredFormatter(Type type) {
        if(type.IsArray) {
            GetRegisteredFormatter(type.GetElementType());
            return null;
        }
        if(type.IsAnonymous())
            return Return(Register(type,typeof(Formatters.Others.Anonymous<>)));
        if(type.IsDisplay())
            return Return(Register(type,typeof(Formatters.Others.DisplayClass<>)));
        if(typeof(Delegate).IsAssignableFrom(type))
            return Return(Register(type,typeof(Formatters.Others.Delegate<>)));
        if(type.IsGenericType) {
            if(typeof(Expressions.LambdaExpression).IsAssignableFrom(type))
                return Return(Register(type,typeof(Formatters.ExpressionT<>)));
            object? Formatter = null;
            if(type.IsInterface) {
                if((Formatter=RegisterInterface(type,typeof(Sets.IGrouping<,>),typeof(Formatters.Sets.IGrouping<,>))) is not null) return Return(Formatter);
                if((Formatter=RegisterInterface(type,typeof(System.Linq.IGrouping<,>),typeof(Formatters.Enumerables.IGrouping<,>))) is not null) return Return(Formatter);
                if((Formatter=RegisterInterface(type,typeof(Sets.IEnumerable<>),typeof(Formatters.Sets.IEnumerable<>))) is not null) return Return(Formatter);
                if((Formatter=RegisterInterface(type,typeof(Generic.IEnumerable<>),typeof(Formatters.Enumerables.IEnumerable<>))) is not null) return Return(Formatter);
            } else {
                if((Formatter=RegisterType(type,typeof(Enumerables.GroupingList<, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.GroupingSet        <, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.SetGroupingList    <, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.SetGroupingSet     <, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.Set                <,,>))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.Set                <, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.Set                <  >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Enumerables.List        <  >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.HashSet            <  >))) is not null) return Return(Formatter);
                //if(type.GetGenericArguments(typeof(Generic.ICollection<>),out var GenericArguments1)) {
                //    var GenericArguments_0 = GenericArguments1[0];
                //    var FormatterGenericType = typeof(InterfaceEnumerableFormatter<>).MakeGenericType(GenericArguments_0);
                //    return Return(Activator.CreateInstance(FormatterGenericType)!);
                //}
            }
        }
        {
            if(type.GetGenericArguments(typeof(Generic.ICollection<>),out var GenericArguments0)){
                var i=type.GetInterface(CommonLibrary.Generic_IEnumerable1_FullName);
                MemoryPackFormatterProvider.RegisterCollection(typeof(Generic.ICollection<>).MakeGenericType(GenericArguments0));
                return null;
                //var t = type.GetInterface(CommonLibrary.Collections_IEnumerable_FullName);
                //var GenericArguments_0 = GenericArguments0[0];
                //var FormatterGenericType = typeof(GenericCollectionFormatter<,>).MakeGenericType(type,GenericArguments_0);
                //return Return(Activator.CreateInstance(FormatterGenericType)!);
            }
        }
        {
            if(type.GetIEnumerable1(out var GenericArguments)){
                var GenericArguments_0 = GenericArguments[0];
                var FormatterGenericType = typeof(Formatters.Enumerables.IEnumerable<>).MakeGenericType(GenericArguments_0);
                return Return(FormatterGenericType.GetValue(nameof(Formatters.Enumerables.IEnumerable<int>.Instance)));
            }
            if(type.GetGenericArguments(typeof(Generic.IEnumerable<>),out var GenericArguments0)){
                var i=type.GetInterface(CommonLibrary.Generic_IEnumerable1_FullName);
                MemoryPackFormatterProvider.RegisterCollection(typeof(Generic.ICollection<>).MakeGenericType(GenericArguments0));
                return null;
                //var t = type.GetInterface(CommonLibrary.Collections_IEnumerable_FullName);
                //var GenericArguments_0 = GenericArguments0[0];
                //var FormatterGenericType = typeof(GenericCollectionFormatter<,>).MakeGenericType(type,GenericArguments_0);
                //return Return(Activator.CreateInstance(FormatterGenericType)!);
            }
        }
        return default;
        object Return(object Formatter0) {

            foreach(var GenericArgument in type.GetGenericArguments()) GetRegisteredFormatter(GenericArgument);
            return Formatter0;
        }
    }
    private static object Register(Type type,Type FormatterGenericTypeDefinition){
        var GenericArguments=new []{type};
        var FormatterGenericType=FormatterGenericTypeDefinition.MakeGenericType(GenericArguments);
        var Register = Serializer.Register.MakeGenericMethod(GenericArguments);
        var Instance=FormatterGenericType.GetValue("Instance");
        Register.Invoke(null,new object?[]{Instance});
        return Instance;
    }
}
