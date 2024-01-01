using System;
using System.Diagnostics;
using LinqDB.Helpers;
using MemoryPack;
using Generic = System.Collections.Generic;
using Expressions = System.Linq.Expressions;
using LinqDB.Enumerables;
namespace LinqDB.Serializers.MemoryPack;
internal static class FormatterResolver {
    //private static void Serialize<TBufferWriter, T>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value) where TBufferWriter :IBufferWriter<byte>{
    //    var Formatter=FormatterResolver.GetRegisteredFormatter<T>()??writer.GetFormatter<T>();
    //public static MemoryPackFormatter<T>? GetRegisteredFormatter<TBufferWriter,T>(ref MemoryPackWriter<TBufferWriter> writer)where TBufferWriter :IBufferWriter<byte>{
    //}
    public static MemoryPackFormatter<T>? GetFormatterDynamic<T>(){
        var type=typeof(T);
        
        
        if(type.IsArray){
            GetFormatterDynamic(type.GetElementType());
            return null;
        }
        if(type.IsAnonymous())
            return Return(Register(type,typeof(Formatters.Others.Anonymous<>)));
        Debug.Assert(!type.IsDisplay(),"<>__Displayが具体的な型で宣言されていることは想定していない");
        //if(type.IsDisplay())
        //    return Return(Register(type,typeof(Formatters.Others.DisplayClass<>)));
        if(typeof(Delegate).IsAssignableFrom(type))
            return Return(Register(type,typeof(Formatters.Others.Delegate<>)));
        if(type.IsGenericType) {
            foreach(var GenericArgument in type.GetGenericArguments())GetFormatterDynamic(GenericArgument);
            if(typeof(Expressions.LambdaExpression).IsAssignableFrom(type))
                return Return(Register(type,typeof(Formatters.ExpressionT<>)));
            object? Formatter;
            if(type.IsInterface){
                if((Formatter=RegisterInterface(type,typeof(Sets.ILookup         <,>),typeof(Formatters.Sets.ILookup           <,>)))is not null)return Return(Formatter);
                if((Formatter=RegisterInterface(type,typeof(Sets.IGrouping       <,>),typeof(Formatters.Sets.IGrouping         <,>)))is not null)return Return(Formatter);
                if((Formatter=RegisterInterface(type,typeof(System.Linq.IGrouping<,>),typeof(Formatters.Enumerables.IGrouping  <,>)))is not null)return Return(Formatter);
                if((Formatter=RegisterInterface(type,typeof(Sets.IEnumerable     < >),typeof(Formatters.Sets.IEnumerable       < >)))is not null)return Return(Formatter);
                //if((Formatter=RegisterInterface(type,typeof(Generic.IEnumerable  < >),typeof(Formatters.Enumerables.IEnumerable< >)))is not null)return Return(Formatter);
                Formatter=RegisterInterface(type,typeof(Generic.IEnumerable  < >),typeof(Formatters.Enumerables.IEnumerable< >));
                return Return(Formatter);
            }else{
                if((Formatter=RegisterType(type,typeof(Grouping<, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.Grouping        <, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Lookup    <, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.SetGroupingSet     <, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.Set                <,,>))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.Set                <, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.Set                <  >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(List        <  >))) is not null) return Return(Formatter);
                //if((Formatter=RegisterType(type,typeof(Sets.HashSet            <  >))) is not null) return Return(Formatter);
                Formatter=RegisterType(type,typeof(Sets.HashSet<>));
                return Return(Formatter);
            }
        }
        return default;
        MemoryPackFormatter<T>Return(object Formatter0)=>RegisterFormatter<MemoryPackFormatter<T>>(type,Formatter0);







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
    
    
    
    public static object? GetFormatterDynamic(Type type) {
        if(type.IsArray) {
            GetFormatterDynamic(type.GetElementType());
            return null;
        }
        if(type.IsAnonymous())
            return Return(Register(type,typeof(Formatters.Others.Anonymous<>)));
        if(type.IsDisplay())//classしか想定してない。structはローカル関数のキャプチャ。ジェネリックstructはローカル関数の親関数がジェネリック関数だった場合
            return Return(Register(type,typeof(Formatters.Others.DisplayClass<>)));
        if(typeof(Delegate).IsAssignableFrom(type))
            return Return(Register(type,typeof(Formatters.Others.Delegate<>)));
        if(type.IsGenericType) {
            foreach(var GenericArgument in type.GetGenericArguments())GetFormatterDynamic(GenericArgument);
            if(typeof(Expressions.LambdaExpression).IsAssignableFrom(type))
                return Return(Register(type,typeof(Formatters.ExpressionT<>)));
            object? Formatter = null;
            if(type.IsInterface) {
                if((Formatter=RegisterInterface(type,typeof(Sets.ILookup<,>),typeof(Formatters.Sets.ILookup<,>))) is not null) return Return(Formatter);
                if((Formatter=RegisterInterface(type,typeof(Sets.IGrouping<,>),typeof(Formatters.Sets.IGrouping<,>))) is not null) return Return(Formatter);
                if((Formatter=RegisterInterface(type,typeof(System.Linq.ILookup<,>),typeof(Formatters.Enumerables.ILookup<,>))) is not null) return Return(Formatter);
                if((Formatter=RegisterInterface(type,typeof(System.Linq.IGrouping<,>),typeof(Formatters.Enumerables.IGrouping<,>))) is not null) return Return(Formatter);
                if((Formatter=RegisterInterface(type,typeof(Sets.IEnumerable<>),typeof(Formatters.Sets.IEnumerable<>))) is not null) return Return(Formatter);
                //if((Formatter=RegisterInterface(type,typeof(Generic.IEnumerable<>),typeof(Formatters.Enumerables.IEnumerable<>))) is not null) return Return(Formatter);
                Formatter=RegisterInterface(type,typeof(Generic.IEnumerable<>),typeof(Formatters.Enumerables.IEnumerable<>));
                Debug.Assert(Formatter is not null,"UnionByでエラーになるかも？");
                return Return(Formatter);
            } else {
                if((Formatter=RegisterType(type,typeof(Grouping<, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.Grouping        <, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Lookup    <, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.SetGroupingSet     <, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.Set                <,,>))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.Set                <, >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.Set                <  >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(List        <  >))) is not null) return Return(Formatter);
                if((Formatter=RegisterType(type,typeof(Sets.HashSet            <  >))) is not null) return Return(Formatter);
                //Formatter=RegisterType(type,typeof(Sets.HashSet<>));
                //return Return(Formatter);
            }
        }
        {
            if(type.GetIEnumerableT(out var Interface)){
                var GenericArguments_0 = Interface.GetGenericArguments()[0];
                var FormatterGenericType = typeof(Formatters.Enumerables.IEnumerableOther<>).MakeGenericType(GenericArguments_0);
                return Return(FormatterGenericType.GetValue("Instance"));
            }
        }
        return default;
        object Return(object Formatter0)=>RegisterFormatter<object>(type,Formatter0);
    }
    private static object Register(Type type,Type FormatterGenericTypeDefinition){
        var GenericArguments=new []{type};
        var FormatterGenericType=FormatterGenericTypeDefinition.MakeGenericType(GenericArguments);
        var Register = Serializer.Register.MakeGenericMethod(GenericArguments);
        var Instance=FormatterGenericType.GetValue("Instance");
        Register.Invoke(null,new[]{Instance});
        return Instance;
    }
    private static T RegisterFormatter<T>(Type type,object Formatter0){
            
        //foreach(var GenericArgument in type.GetGenericArguments())GetRegisteredFormatter(GenericArgument);
        return (T)Formatter0;
    }
}
