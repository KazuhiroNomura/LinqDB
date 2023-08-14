using System.Reflection;
namespace LinqDB.Reflection;

using static Common;
internal static class Int16{
    public static readonly MethodInfo Parse_s = M(() => short.Parse(""));
    public static readonly MethodInfo ToString_ = ToString<short>();
}