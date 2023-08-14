using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using LinqDB.Sets;
namespace LinqDB.Reflection;

internal static class Common{
    public static MethodInfo M<T>(Expression<Func<T>> e){
        var Method=((MethodCallExpression)e.Body).Method;
        return Method.IsGenericMethod ? Method.GetGenericMethodDefinition() : Method;
    }
    public static MethodInfo ToString<T>() {
        var Method = typeof(T).GetMethod(nameof(object.ToString),Type.EmptyTypes);
        if(Method is null) throw new KeyNotFoundException(nameof(object.ToString));
        return Method;
    }
    public static PropertyInfo P<T>(Expression<Func<T>> e)=>(PropertyInfo)((MemberExpression)e.Body).Member;
    public static ConstructorInfo C<T>(Expression<Func<T>> e)=>((NewExpression)e.Body).Constructor!;
    public const Set<int> T=null!;
}