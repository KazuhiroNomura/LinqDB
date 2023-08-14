using System.Reflection;
namespace LinqDB.Reflection;

using static Common;
internal static class Byte{
    public static readonly MethodInfo Parse_s = M(() => byte.Parse(""));
    public static readonly MethodInfo ToString_ = ToString<byte>();
}