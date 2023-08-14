using System.Reflection;

namespace LinqDB.Reflection;

using static Common;
internal static class TimeSpan{
    public static readonly ConstructorInfo ctor_Int64 = typeof(System.TimeSpan).GetConstructor(new[] { typeof(long) })!;
    public static readonly MethodInfo Subtract_TimeSpan = M(() => new System.TimeSpan(1).Subtract(new System.TimeSpan(0)));
    public static readonly PropertyInfo Days= P(() => new System.TimeSpan(1).Days);
}