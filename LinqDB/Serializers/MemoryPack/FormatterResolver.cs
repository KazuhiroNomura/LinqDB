using System;
using System.Diagnostics;
using LinqDB.Helpers;


using Generic = System.Collections.Generic;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack;
internal static class FormatterResolver {


    public static void GetFormatter(Type type) {


        if(type.IsDisplay()){
            RegisterAnonymousDisplay(type,typeof(Formatters.Others.DisplayClass<>));
        }else if(type.IsArray){
            GetFormatter(type.GetElementType());
        }else if(type.IsGenericType) {
            if(type.IsAnonymous()) {
                RegisterAnonymousDisplay(type,typeof(Formatters.Others.Anonymous<>));
            } else if(typeof(Expressions.LambdaExpression).IsAssignableFrom(type)) {
                RegisterAnonymousDisplay(type,typeof(Formatters.ExpressionT<>));
                
                
                
            }else if(type.IsInterface){

                if(RegisterInterface(type,typeof(Sets.IGrouping       <,>),typeof(Formatters.Sets.IGrouping         <,>)))goto 型パラメーターを登録;
                if(RegisterInterface(type,typeof(System.Linq.IGrouping<,>),typeof(Formatters.Enumerables.IGrouping  <,>)))goto 型パラメーターを登録;
                if(RegisterInterface(type,typeof(Sets.IEnumerable     < >),typeof(Formatters.Sets.IEnumerable       < >)))goto 型パラメーターを登録;
                if(RegisterInterface(type,typeof(Generic.IEnumerable  < >),typeof(Formatters.Enumerables.IEnumerable< >)))goto 型パラメーターを登録;
            }else{
                
                var type0=type;
                do{
                    if(RegisterType(type0,typeof(Enumerables.GroupingList<, >)))break;
                    if(RegisterType(type0,typeof(Sets.GroupingSet        <, >)))break;
                    if(RegisterType(type0,typeof(Sets.SetGroupingList    <, >)))break;
                    if(RegisterType(type0,typeof(Sets.SetGroupingSet     <, >)))break;
                    if(RegisterType(type0,typeof(Sets.Set                <,,>)))break;
                    if(RegisterType(type0,typeof(Sets.Set                <, >)))break;
                    if(RegisterType(type0,typeof(Sets.Set                <  >)))break;
                    do{
                        if(type0==typeof(object)) goto 型パラメーターを登録;
                        type0=type0.BaseType!;
                    }while(!type0.IsGenericType);
                } while(typeof(object)!=type0);
            }
            型パラメーターを登録:
            foreach(var GenericArgument in type.GetGenericArguments()) GetFormatter(GenericArgument);
        }
        
        
        
        
        
        
        static bool RegisterInterface(Type type,Type 検索したいキーGenericInterfaceDefinition,Type FormatterGenericInterfaceDefinition){
            Debug.Assert(検索したいキーGenericInterfaceDefinition.IsInterface||FormatterGenericInterfaceDefinition.IsInterface);
            if(type.IsGenericType&&type.GetGenericTypeDefinition()==検索したいキーGenericInterfaceDefinition){
                RegisterGeneric(type,FormatterGenericInterfaceDefinition);
                return true;
            }
            foreach(var Interface in type.GetInterfaces()){
                if(!Interface.IsGenericType)continue;
                if(Interface.GetGenericTypeDefinition()==検索したいキーGenericInterfaceDefinition){
                    RegisterGeneric(Interface,FormatterGenericInterfaceDefinition);
                    return true;
                }
            }
            return false;
        }
        static bool RegisterType(Type type0,Type 検索したいキーGenericTypeDefinition){
            Debug.Assert(type0.IsGenericType);
            Debug.Assert(!検索したいキーGenericTypeDefinition.IsInterface);
            if(type0.GetGenericTypeDefinition()!=検索したいキーGenericTypeDefinition)return false;
            var cctor=type0.GetConstructor(System.Reflection.BindingFlags.Static|System.Reflection.BindingFlags.NonPublic,Type.EmptyTypes)!;
            cctor.Invoke(null,Array.Empty<object>());
            //var Formatter0=type0.GetValue("InstanceMemoryPack");
            //var Register = Serializer.Register.MakeGenericMethod(type0);
            //Register.Invoke(null,new object?[]{Formatter0});
            return true;
        }
        static void RegisterGeneric(Type type0,Type FormatterGenericTypeDefinition){
            var GenericArguments=type0.GetGenericArguments();
            var FormatterGenericType=FormatterGenericTypeDefinition.MakeGenericType(GenericArguments);
            var Register = Serializer.Register.MakeGenericMethod(type0);
            var Instance=FormatterGenericType.GetValue("Instance")!;
            Register.Invoke(null,new object?[]{Instance});
        }
        static void RegisterAnonymousDisplay(Type type,Type FormatterGenericTypeDefinition){
            //MemoryPackFormatterProvider.Register<Set<int>>(default(MemoryPackFormatter<Set<int>>)!);
            //MemoryPackFormatterProvider.Register<Set<int>>(Formatters.Sets.Set<int>.Instance);
            //MemoryPackFormatterProvider.Register<Formatters.Sets.Set<Set<int>>>(Formatters.Sets.Set<Set<int>>.Instance);
            var GenericArguments=new []{type};
            var FormatterGenericType=FormatterGenericTypeDefinition.MakeGenericType(GenericArguments);
            var Register = Serializer.Register.MakeGenericMethod(GenericArguments);
            var Instance=FormatterGenericType.GetField("Instance")!;
            var Value=Instance.GetValue(null);
            //var Register = Serializer.Register.MakeGenericMethod(Instance.FieldType);
            Register.Invoke(null,new object?[]{Value});
        }
    }
}
