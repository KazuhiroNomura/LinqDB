using System.Reflection;
namespace LinqDB.Reflection;

using static Common;
internal static class Int64{
    public static readonly MethodInfo Parse_s = M(() => long.Parse(""));
    public static readonly MethodInfo ToString_ = ToString<long>();
}