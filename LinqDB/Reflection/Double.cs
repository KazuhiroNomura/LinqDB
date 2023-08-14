using System.Reflection;
namespace LinqDB.Reflection;

using static Common;
internal static class Double{
    public static readonly MethodInfo Parse_s = M(() => double.Parse(""));
    public static readonly MethodInfo ToString_ = ToString<double>();
}