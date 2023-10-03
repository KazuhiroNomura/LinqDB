using System;
using System.Diagnostics;
using LinqDB.Helpers;
using LinqDB.Sets;

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
                if(RegisterInterface(type,typeof(IGrouping  <,>),typeof(Formatters.Sets.IGrouping  <,>)))return;
                if(RegisterInterface(type,typeof(IEnumerable< >),typeof(Formatters.Sets.IEnumerable< >)))return;
                
                
                
            }else{
                
                var type0=type;
                do{
                    if(RegisterType(type0,typeof(GroupingSet        <, >),typeof(Formatters.Sets.GroupingSet   <, >)))break;
                    if(RegisterType(type0,typeof(SetGroupingSet     <, >),typeof(Formatters.Sets.SetGroupingSet<, >)))break;
                    if(RegisterType(type0,typeof(Set                <,,>),typeof(Formatters.Sets.Set           <,,>)))break;
                    if(RegisterType(type0,typeof(Set                <, >),typeof(Formatters.Sets.Set           <, >)))break;
                    if(RegisterType(type0,typeof(Set                <  >),typeof(Formatters.Sets.Set           <  >)))break;
                    do{
                        if(type0.BaseType is null) return;
                        type0=type0.BaseType!;
                    }while(!type0.IsGenericType);
                } while(typeof(object)!=type0);
            }
        }
        
        
        
        
        
        
        static bool RegisterInterface(Type type,Type 検索したいキーGenericInterfaceDefinition,Type FormatterGenericInterfaceDefinition){
            Debug.Assert(検索したいキーGenericInterfaceDefinition.IsInterface||FormatterGenericInterfaceDefinition.IsInterface);
            if(type.IsGenericType&&type.GetGenericTypeDefinition()==FormatterGenericInterfaceDefinition){
                RegisterGeneric(type,FormatterGenericInterfaceDefinition);
                return true;
            }
            foreach(var Interface in type.GetInterfaces()){
                if(!Interface.IsGenericType)continue;
                if(Interface.GetGenericTypeDefinition()==FormatterGenericInterfaceDefinition){
                    RegisterGeneric(Interface,FormatterGenericInterfaceDefinition);
                    return true;
                }
            }
            return false;
        }
        static bool RegisterType(Type type,Type 検索したいキーGenericTypeDefinition,Type FormatterGenericTypeDefinition){
            Debug.Assert(type.IsGenericType);
            Debug.Assert(!検索したいキーGenericTypeDefinition.IsInterface);
            if(type.GetGenericTypeDefinition()!=検索したいキーGenericTypeDefinition)return false;
            RegisterGeneric(type,FormatterGenericTypeDefinition);
            return true;


        }
        static void RegisterGeneric(Type type,Type FormatterGenericTypeDefinition){
            var GenericArguments=type.GetGenericArguments();
            var FormatterGenericType=FormatterGenericTypeDefinition.MakeGenericType(GenericArguments);
            var Register = Serializer.Register.MakeGenericMethod(type);
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
//            foreach(var GenericArgument in type.GetGenericArguments()) GetFormatter(GenericArgument);
