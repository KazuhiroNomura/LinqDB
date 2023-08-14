using System.Reflection;

namespace LinqDB.Reflection;

using static Common;
internal static class String{
    public static readonly MethodInfo Concat_str0_str1 = M(() => string.Concat("",""));
    public static readonly MethodInfo CompareOrdinal = M(() => string.CompareOrdinal("",""));

}