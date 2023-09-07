using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinqDB.Serializers;
internal static class Common {
    public static MemberInfo[] Get(this Dictionary<Type,MemberInfo[]>d,Type ReflectedType){
        if(!d.TryGetValue(ReflectedType,out var array)){
            array=ReflectedType.GetMembers(BindingFlags.Instance|BindingFlags.Static|BindingFlags.Public).ToArray();
            d.Add(ReflectedType,array);
            Array.Sort(array,(a,b)=>string.CompareOrdinal(a.ToString(),b.ToString()));
        }
        return array;
    }
    public static ConstructorInfo[] Get(this Dictionary<Type,ConstructorInfo[]>d,Type ReflectedType){
        if(!d.TryGetValue(ReflectedType,out var array)){
            array=ReflectedType.GetConstructors(BindingFlags.Instance|BindingFlags.Public).ToArray();
            d.Add(ReflectedType,array);
            Array.Sort(array,(a,b)=>string.CompareOrdinal(a.ToString(),b.ToString()));
        }
        return array;
    }
    public static EventInfo[] Get(this Dictionary<Type,EventInfo[]>d,Type ReflectedType){
        if(!d.TryGetValue(ReflectedType,out var array)){
            array=ReflectedType.GetEvents(BindingFlags.Instance|BindingFlags.Static|BindingFlags.Public).ToArray();
            d.Add(ReflectedType,array);
            Array.Sort(array,(a,b)=>string.CompareOrdinal(a.ToString(),b.ToString()));
        }
        return array;
    }
    public static FieldInfo[] Get(this Dictionary<Type,FieldInfo[]>d,Type ReflectedType){
        if(!d.TryGetValue(ReflectedType,out var array)){
            array=ReflectedType.GetFields(BindingFlags.Instance|BindingFlags.Static|BindingFlags.Public).ToArray();
            d.Add(ReflectedType,array);
            Array.Sort(array,(a,b)=>string.CompareOrdinal(a.ToString(),b.ToString()));
        }
        return array;
    }
    public static PropertyInfo[] Get(this Dictionary<Type,PropertyInfo[]>d,Type ReflectedType){
        if(!d.TryGetValue(ReflectedType,out var array)){
            array=ReflectedType.GetProperties(BindingFlags.Instance|BindingFlags.Static|BindingFlags.Public).ToArray();
            d.Add(ReflectedType,array);
            Array.Sort(array,(a,b)=>string.CompareOrdinal(a.ToString(),b.ToString()));
        }
        return array;
    }
    public static MethodInfo[] Get(this Dictionary<Type,MethodInfo[]>d,Type ReflectedType){
        if(!d.TryGetValue(ReflectedType,out var array)){
            array=ReflectedType.GetMethods(BindingFlags.Instance|BindingFlags.Static|BindingFlags.Public).ToArray();
            d.Add(ReflectedType,array);
            Array.Sort(array,(a,b)=>string.CompareOrdinal(a.ToString(),b.ToString()));
        }
        return array;
    }
}

