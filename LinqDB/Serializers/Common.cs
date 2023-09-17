using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using Microsoft.CSharp.RuntimeBinder;
namespace LinqDB.Serializers;
internal static class Extension{
    private static BindingFlags Flags=BindingFlags.Instance|BindingFlags.Static|BindingFlags.Public|BindingFlags.NonPublic;
    public static MemberInfo[] Get(this ConcurrentDictionary<Type,MemberInfo[]>d,Type type){
        if(!d.TryGetValue(type,out var array)){
            array=d.GetOrAdd(type,key=>{
                var value=type.GetMembers(Flags).ToArray();
                Array.Sort(value,(a,b)=>string.CompareOrdinal(a.Name,b.Name));
                return value;
            });
        }
        return array;
    }
    public static ConstructorInfo[] Get(this ConcurrentDictionary<Type,ConstructorInfo[]>d,Type type){
        if(!d.TryGetValue(type,out var array)){
            array=d.GetOrAdd(type,key=>{
                var value=type.GetConstructors(BindingFlags.Instance|BindingFlags.Public).ToArray();
                Array.Sort(value,(a,b)=>string.CompareOrdinal(a.ToString(),b.ToString()));
                return value;
            });
        }
        return array;
    }
    public static EventInfo[] Get(this ConcurrentDictionary<Type,EventInfo[]>d,Type type){
        if(!d.TryGetValue(type,out var array)){
            array=d.GetOrAdd(type,key=>{
                var value=type.GetEvents(Flags).ToArray();
                Array.Sort(value,(a,b)=>string.CompareOrdinal(a.Name,b.Name));
                return value;
            });
        }
        return array;
    }
    public static FieldInfo[] Get(this ConcurrentDictionary<Type,FieldInfo[]>d,Type type){
        if(!d.TryGetValue(type,out var array)){
            array=d.GetOrAdd(type,key=>{
                var value=type.GetFields(Flags).ToArray();
                Array.Sort(value,(a,b)=>string.CompareOrdinal(a.Name,b.Name));
                return value;
            });
        }
        return array;
    }
    public static PropertyInfo[] Get(this ConcurrentDictionary<Type,PropertyInfo[]>d,Type type){
        if(!d.TryGetValue(type,out var array)){
            array=d.GetOrAdd(type,key=>{
                var value=type.GetProperties(Flags).ToArray();
                Array.Sort(value,(a,b)=>string.CompareOrdinal(a.Name,b.Name));
                return value;
            });
        }
        return array;
    }
    public static MethodInfo[] Get(this ConcurrentDictionary<Type,MethodInfo[]>d,Type type){
        if(!d.TryGetValue(type,out var array)){
            array=d.GetOrAdd(type,key=>{
                //NonPublicは<>cのinternalメソッドが匿名デリゲートの本体になることがあるため
                var value=type.GetMethods(Flags|BindingFlags.NonPublic).ToArray();
                Array.Sort(value,(a,b)=>string.CompareOrdinal(a.ToString(),b.ToString()));
                return value;
            });
        }
        return array;
    }
}
internal static class Common {
    public static readonly CSharpArgumentInfo CSharpArgumentInfo1 = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
    public static readonly CSharpArgumentInfo[]CSharpArgumentInfos1={CSharpArgumentInfo1};
    public static readonly CSharpArgumentInfo[]CSharpArgumentInfos2={CSharpArgumentInfo1,CSharpArgumentInfo1};
    public static readonly CSharpArgumentInfo[]CSharpArgumentInfos3={CSharpArgumentInfo1,CSharpArgumentInfo1,CSharpArgumentInfo1};
    public static readonly CSharpArgumentInfo[]CSharpArgumentInfos4={CSharpArgumentInfo1,CSharpArgumentInfo1,CSharpArgumentInfo1,CSharpArgumentInfo1};
}
internal enum BinderType:byte{
    BinaryOperationBinder,
    ConvertBinder,
    CreateInstanceBinder,
    DeleteIndexBinder,
    DeleteMemberBinder,
    GetIndexBinder,
    GetMemberBinder,
    InvokeBinder,
    InvokeMemberBinder,
    SetIndexBinder,
    SetMemberBinder,
    UnaryOperationBinder,
}
