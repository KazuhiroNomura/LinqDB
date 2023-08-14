using System.Reflection;
namespace LinqDB.Reflection;

internal static class IEnumerator{
    public static readonly MethodInfo MoveNext=typeof(System.Collections.IEnumerator).GetMethod(nameof(System.Collections.IEnumerator.MoveNext))!;
}