using System.Reflection;
namespace LinqDB.Reflection;

using static Common;
internal static class Boolean{
    public static readonly MethodInfo Parse_s = M(() => bool.Parse(""));
    public static readonly MethodInfo ToString_ = ToString<bool>();
}