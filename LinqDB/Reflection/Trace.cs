using System.Reflection;

namespace LinqDB.Reflection;

using static Common;
internal static class Trace
{
    public static readonly MethodInfo WriteLine = M(() => System.Diagnostics.Trace.WriteLine(""));

}