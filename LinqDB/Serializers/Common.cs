using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LinqDB.Databases.Schemas;
using Microsoft.CSharp.RuntimeBinder;
namespace LinqDB.Serializers;
using Expressions=System.Linq.Expressions;
internal static class Extension{
    private static BindingFlags Flags=BindingFlags.Instance|BindingFlags.Static|BindingFlags.Public|BindingFlags.NonPublic;
    public static MemberInfo[] Get(this Dictionary<Type,MemberInfo[]>d,Type ReflectedType){
        if(!d.TryGetValue(ReflectedType,out var array)){
            array=ReflectedType.GetMembers(Flags).ToArray();
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
            array=ReflectedType.GetEvents(Flags).ToArray();
            d.Add(ReflectedType,array);
            Array.Sort(array,(a,b)=>string.CompareOrdinal(a.ToString(),b.ToString()));
        }
        return array;
    }
    public static FieldInfo[] Get(this Dictionary<Type,FieldInfo[]>d,Type ReflectedType){
        if(!d.TryGetValue(ReflectedType,out var array)){
            array=ReflectedType.GetFields(Flags).ToArray();
            d.Add(ReflectedType,array);
            Array.Sort(array,(a,b)=>string.CompareOrdinal(a.ToString(),b.ToString()));
        }
        return array;
    }
    public static PropertyInfo[] Get(this Dictionary<Type,PropertyInfo[]>d,Type ReflectedType){
        if(!d.TryGetValue(ReflectedType,out var array)){
            array=ReflectedType.GetProperties(Flags).ToArray();
            d.Add(ReflectedType,array);
            Array.Sort(array,(a,b)=>string.CompareOrdinal(a.ToString(),b.ToString()));
        }
        return array;
    }
    public static MethodInfo[] Get(this Dictionary<Type,MethodInfo[]>d,Type ReflectedType){
        if(!d.TryGetValue(ReflectedType,out var array)){
            //NonPublicは<>cのinternalメソッドが匿名デリゲートの本体になることがあるため
            array=ReflectedType.GetMethods(Flags|BindingFlags.NonPublic).ToArray();
            d.Add(ReflectedType,array);
            Array.Sort(array,(a,b)=>string.CompareOrdinal(a.ToString(),b.ToString()));
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
