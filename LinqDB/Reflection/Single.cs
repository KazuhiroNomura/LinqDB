using System.Reflection;
namespace LinqDB.Reflection;

using static Common;
internal static class Single{
    public static readonly MethodInfo Parse_s = M(() => float.Parse(""));
    public static readonly MethodInfo ToString_ = ToString<float>();
}