using System.Reflection;
namespace LinqDB.Reflection;

using static Common;
public static class Object{
    public static readonly MethodInfo Equals_=M(()=> new object().Equals(default(Object)));
    public static readonly MethodInfo GetType_=M(()=> new object().GetType());
    public static readonly MethodInfo ToString_=M(()=> new object().ToString());
}