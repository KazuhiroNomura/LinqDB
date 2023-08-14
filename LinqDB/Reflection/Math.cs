using System.Reflection;
namespace LinqDB.Reflection;

internal static class Math{
    public static readonly MethodInfo Log10=typeof(System.Math).GetMethod(nameof(Log10))!;
    public static readonly MethodInfo Pow=typeof(System.Math).GetMethod(nameof(Pow))!;
    public static readonly MethodInfo Sqrt=typeof(System.Math).GetMethod(nameof(Sqrt))!;
}