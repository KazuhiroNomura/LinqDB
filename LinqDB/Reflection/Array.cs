using System.Reflection;

namespace LinqDB.Reflection;

using static Common;
internal static class Array{
    public static readonly PropertyInfo Length = P(() => System.Array.Empty<int>().Length);

}