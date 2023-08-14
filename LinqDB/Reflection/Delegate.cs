using System;
using System.Reflection;
namespace LinqDB.Reflection;

using static Common;
internal static class Delegate{
    public static readonly MethodInfo CreateDelegate=M(()=> System.Delegate.CreateDelegate(typeof(Type),default!));
    public static readonly MethodInfo CreateDelegateTarget=M(()=> System.Delegate.CreateDelegate(typeof(Type),default,default(MethodInfo)!));
}