using System.Collections.Generic;
using System.Reflection;
using LinqDB.Sets;
namespace LinqDB.Reflection;

using static Common;
internal static class Helpers {
    public static readonly MethodInfo NoLoopUnrolling = M(()=>T.NoLoopUnrolling());
    public static readonly MethodInfo NoEarlyEvaluation = M(() => T.NoEarlyEvaluation());
    //public static readonly MethodInfo EqualityComparer_Equals= M(() => Sets.Helpers.Equals(1,2));
    public static readonly MethodInfo EqualityComparer_Equals= M(() =>EqualityComparer<int>.Default.Equals(1,2));
    public static readonly MethodInfo TryGetValue = typeof(Set<,>).GetMethod("TryGetValue")!;
    public static readonly PropertyInfo PrimaryKey = typeof(IKey<>).GetProperty(nameof(IKey<int>.Key))!;
}