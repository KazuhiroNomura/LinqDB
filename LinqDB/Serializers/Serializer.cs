using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LinqDB.Databases.Schemas;
namespace LinqDB.Serializers;
using Expressions=System.Linq.Expressions;
public class Serializer{
    internal readonly List<Expressions.ParameterExpression> ListParameter=new();
    internal readonly Dictionary<Expressions.LabelTarget,int> Dictionary_LabelTarget_int=new();
    internal readonly List<Expressions.LabelTarget> LabelTargets=new();
    internal readonly Dictionary<Type,int> Dictionary_Type_int=new();
    internal readonly List<Type> Types=new();
    internal readonly Dictionary<Type,MemberInfo[]> TypeMembers=new();
    internal readonly Dictionary<Type,ConstructorInfo[]> TypeConstructors=new();
    internal readonly Dictionary<Type,MethodInfo[]> TypeMethods=new();
    internal readonly Dictionary<Type,FieldInfo[]> TypeFields=new();
    internal readonly Dictionary<Type,PropertyInfo[]> TypeProperties=new();
    internal readonly Dictionary<Type,EventInfo[]> TypeEvents=new();
    internal int GetIndex(Expressions.LabelTarget value){
        if(this.Dictionary_LabelTarget_int.TryGetValue(value,out var index)){
        } else{
            var Dictionary_LabelTarget_int=this.Dictionary_LabelTarget_int;
            index=Dictionary_LabelTarget_int.Count;
            this.LabelTargets.Add(value);
            Dictionary_LabelTarget_int.Add(value,index);
        }
        return index;
    }
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
