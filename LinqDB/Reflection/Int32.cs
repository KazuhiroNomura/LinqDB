using System.Reflection;
namespace LinqDB.Reflection;

using static Common;
internal static class Int32{
    public static readonly MethodInfo Parse_s = M(() => int.Parse(""));
    public static readonly MethodInfo ToString_ = ToString<int>();
}