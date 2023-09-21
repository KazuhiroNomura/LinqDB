using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Expressions=System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
using LinqDB.Helpers;

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
    public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos) GetBinder(this BinaryOperationBinder v1){
        dynamic d = new NonPublicAccessor(v1);
        return(d._callingContext,d._argumentInfo);
    }
    public static Type GetBinder(this ConvertBinder v1){
        dynamic d = new NonPublicAccessor(v1);
        return d._callingContext;
    }
    public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos,int Flags) GetBinder(this CreateInstanceBinder v1){
        dynamic d = new NonPublicAccessor(v1);
        return(d._callingContext,d._argumentInfo,0);
    }
    public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos,int Flags) GetBinder(this DeleteIndexBinder v1){
        dynamic d = new NonPublicAccessor(v1);
        return(d._callingContext,d._argumentInfo,0);
    }
    public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos,int Flags) GetBinder(this DeleteMemberBinder v1){
        dynamic d = new NonPublicAccessor(v1);
        return(d._callingContext,d._argumentInfo,0);
    }
    public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos) GetBinder(this GetIndexBinder v1){
        dynamic d = new NonPublicAccessor(v1);
        return(d._callingContext,d._argumentInfo);
    }
    public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos) GetBinder(this GetMemberBinder v1){
        dynamic d = new NonPublicAccessor(v1);
        return(d._callingContext,d._argumentInfo);
    }
    public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos) GetBinder(this InvokeBinder v1){
        dynamic d = new NonPublicAccessor(v1);
        return(d._callingContext,d._argumentInfo);
    }
    public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos,int CSharpBinderFlags) GetBinder(this InvokeMemberBinder v1){
        dynamic d = new NonPublicAccessor(v1);
        var s = d.Flags.ToString();
        var m_argumentInfo = d._argumentInfo;
        var value = s switch {
            "None" => CSharpBinderFlags.None,
            "ResultDiscarded" => CSharpBinderFlags.ResultDiscarded,
            _ => throw new NotSupportedException(s)
        };
        return (d.CallingContext, m_argumentInfo,(int)value);
    }
    public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos) GetBinder(this SetIndexBinder v1){
        dynamic d = new NonPublicAccessor(v1);
        return(d._callingContext,d._argumentInfo);
    }
    public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos) GetBinder(this SetMemberBinder v1){
        dynamic d = new NonPublicAccessor(v1);
        return(d._callingContext,d._argumentInfo);
    }
    public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos) GetBinder(this UnaryOperationBinder v1){
        dynamic d = new NonPublicAccessor(v1);
        return(d._callingContext,d._argumentInfo);
    }
    public static (CSharpArgumentInfoFlags flags, string name) GetFlagsName(this CSharpArgumentInfo v1){
        dynamic d=new NonPublicAccessor(v1);
        return(d.Flags,d.Name);
    }
}
internal static class Common {
    public static readonly CSharpArgumentInfo CSharpArgumentInfo1 = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
    public static readonly CSharpArgumentInfo[]CSharpArgumentInfos1={CSharpArgumentInfo1};
    public static readonly CSharpArgumentInfo[]CSharpArgumentInfos2={CSharpArgumentInfo1,CSharpArgumentInfo1};
    public static readonly CSharpArgumentInfo[]CSharpArgumentInfos3={CSharpArgumentInfo1,CSharpArgumentInfo1,CSharpArgumentInfo1};
    public static readonly CSharpArgumentInfo[]CSharpArgumentInfos4={CSharpArgumentInfo1,CSharpArgumentInfo1,CSharpArgumentInfo1,CSharpArgumentInfo1};
    public static CSharpArgumentInfo[]CreateCSharpArgumentInfo(Expressions.Expression[] Arguments){
        var Length=Arguments.Length;
        var value=new CSharpArgumentInfo[Length];
        for(var a=0;a<Length;a++) value[a]=CSharpArgumentInfo1;
        return value;
    }
    //public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos) GetBinder(BinaryOperationBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    return(d._callingContext,d._argumentInfo);
    //}
    //public static Type GetBinder(ConvertBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    return d._callingContext;
    //}
    //public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos,int Flags) GetBinder(CreateInstanceBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    return(d._callingContext,d._argumentInfo,0);
    //}
    //public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos,int Flags) GetBinder(DeleteIndexBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    return(d._callingContext,d._argumentInfo,0);
    //}
    //public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos,int Flags) GetBinder(DeleteMemberBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    return(d._callingContext,d._argumentInfo,0);
    //}
    //public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos) GetBinder(GetIndexBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    return(d._callingContext,d._argumentInfo);
    //}
    //public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos) GetBinder(GetMemberBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    return(d._callingContext,d._argumentInfo);
    //}
    //public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos) GetBinder(InvokeBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    return(d._callingContext,d._argumentInfo);
    //}
    //public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos,int CSharpBinderFlags) GetBinder(InvokeMemberBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    var s = d.Flags.ToString();
    //    var m_argumentInfo = d._argumentInfo;
    //    var value = s switch {
    //        "None" => CSharpBinderFlags.None,
    //        "ResultDiscarded" => CSharpBinderFlags.ResultDiscarded,
    //        _ => throw new NotSupportedException(s)
    //    };
    //    return (d.CallingContext, m_argumentInfo,(int)value);
    //}
    //public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos) GetBinder(SetIndexBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    return(d._callingContext,d._argumentInfo);
    //}
    //public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos) GetBinder(SetMemberBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    return(d._callingContext,d._argumentInfo);
    //}
    //public static (Type CallingContext, CSharpArgumentInfo[] CSharpArgumentInfos) GetBinder(UnaryOperationBinder v1){
    //    dynamic d = new NonPublicAccessor(v1);
    //    return(d._callingContext,d._argumentInfo);
    //}
    //public static (CSharpArgumentInfoFlags flags, string name) GetFlagsName(CSharpArgumentInfo v1){
    //    dynamic d=new NonPublicAccessor(v1);
    //    return(d.Flags,d.Name);
    //}
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
