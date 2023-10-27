using System;
using System.Collections.Concurrent;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LinqDB.Helpers;
using Microsoft.CSharp.RuntimeBinder;
namespace LinqDB.Serializers;
internal static class Extension{
    private const BindingFlags Flags=BindingFlags.Instance|BindingFlags.Static|BindingFlags.Public|BindingFlags.NonPublic;
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
    //public static T GetValue<T>(this Type type,string name){
    //    var field=type.GetField(name,BindingFlags.Static|BindingFlags.NonPublic|BindingFlags.Public)!;
    //    var value=field.GetValue(null)!;
    //    return (T)value;
    //}
    public static object GetValue(this Type type,string name){
        var field=type.GetField(name,BindingFlags.Static|BindingFlags.NonPublic|BindingFlags.Public)!;
        var value=field.GetValue(null)!;
        return value;
    }
    private static string[]Type短縮Names;
    private static Type[]短縮NameTypes;
    static Extension(){
        var ListType短縮Name=new System.Collections.Generic.List<string>();
        var List短縮NameType=new System.Collections.Generic.List<Type>();
        共通(typeof(sbyte          ));
        共通(typeof(byte           ));
        共通(typeof(short          ));
        共通(typeof(ushort         ));
        共通(typeof(int            ));
        共通(typeof(uint           ));
        共通(typeof(long           ));
        共通(typeof(ulong          ));
        共通(typeof(float          ));
        共通(typeof(double         ));
        共通(typeof(bool           ));
        共通(typeof(char           ));
        共通(typeof(decimal        ));
        共通(typeof(TimeSpan       ));
        共通(typeof(DateTime       ));
        共通(typeof(DateTimeOffset ));
        共通(typeof(string         ));
        Type短縮Names=ListType短縮Name.ToArray();
        短縮NameTypes=List短縮NameType.ToArray();
        void 共通(Type type){
            ListType短縮Name.Add(type.Name);
            List短縮NameType.Add(type);
        }
    }
    public static string TypeString(this Type value){
        if(typeof(Type           ).IsAssignableFrom(value))return nameof(Type           );
        if(typeof(ConstructorInfo).IsAssignableFrom(value))return nameof(ConstructorInfo);
        if(typeof(MethodInfo     ).IsAssignableFrom(value))return nameof(MethodInfo     );
        if(typeof(PropertyInfo   ).IsAssignableFrom(value))return nameof(PropertyInfo   );
        if(typeof(EventInfo      ).IsAssignableFrom(value))return nameof(EventInfo      );
        if(typeof(FieldInfo      ).IsAssignableFrom(value))return nameof(FieldInfo      );
        //if(typeof(Expression     ).IsAssignableFrom(value)&&!typeof(LambdaExpression).IsAssignableFrom(value))return nameof(Expression     );
        return 短縮NameTypes.Contains(value)?value.Name:value.AssemblyQualifiedName!;
    }
    public static Type StringType(this string value){
        switch(value){
            //case nameof(Expression     ):return typeof(Expression     );
            case nameof(Type           ):return typeof(Type           );
            case nameof(ConstructorInfo):return typeof(ConstructorInfo);
            case nameof(MethodInfo     ):return typeof(MethodInfo     );
            case nameof(PropertyInfo   ):return typeof(PropertyInfo   );
            case nameof(EventInfo      ):return typeof(EventInfo      );
            case nameof(FieldInfo      ):return typeof(FieldInfo      );
        }
        var index=Array.IndexOf(Type短縮Names,value);
        return index<0?Type.GetType(value)!:短縮NameTypes[index];
    }
}
