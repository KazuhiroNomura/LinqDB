using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using MessagePack.Formatters;
namespace LinqDB.Serializers;
using Expressions = System.Linq.Expressions;
public class Serializer{
    internal readonly List<Expressions.ParameterExpression> ListParameter=new();
    internal readonly Dictionary<Expressions.LabelTarget,int> Dictionary_LabelTarget_int=new();
    internal readonly List<Expressions.LabelTarget> LabelTargets=new();
    internal readonly Dictionary<Type,int> Dictionary_Type_int=new();
    internal readonly List<Type> Types=new();
    internal readonly ConcurrentDictionary<Type,MemberInfo     []>TypeMembers     =new();
    internal readonly ConcurrentDictionary<Type,ConstructorInfo[]>TypeConstructors=new();
    internal readonly ConcurrentDictionary<Type,MethodInfo     []>TypeMethods     =new();
    internal readonly ConcurrentDictionary<Type,FieldInfo      []>TypeFields      =new();
    internal readonly ConcurrentDictionary<Type,PropertyInfo   []>TypeProperties  =new();
    internal readonly ConcurrentDictionary<Type,EventInfo      []>TypeEvents      =new();
    protected void ProtectedClear(){
        this.ListParameter.Clear();
        this.Dictionary_LabelTarget_int.Clear();
        this.LabelTargets.Clear();
        this.Dictionary_Type_int.Clear();
        this.Types.Clear();
        this.TypeMembers.Clear();
        this.TypeConstructors.Clear();
        this.TypeMethods.Clear();
        this.TypeFields.Clear();
        this.TypeProperties.Clear();
        this.TypeEvents.Clear();
    }
}
