using System.Reflection;
using LinqDB.Sets;
namespace LinqDB.Reflection;

public static class ImmutableSet {
    public static readonly MethodInfo GetEnumerator = typeof(ImmutableSet<>).GetMethod(nameof(ImmutableSet<int>.GetEnumerator))!;
    public static readonly MethodInfo get_Count = typeof(Sets.ImmutableSet).GetProperty(nameof(Sets.ImmutableSet.LongCount))!.GetMethod!;
    public static class Enumerator{
        public static readonly MethodInfo MoveNext = typeof(ImmutableSet<>.Enumerator).GetMethod(nameof(ImmutableSet<int>.Enumerator.MoveNext))!;
        public static readonly FieldInfo InternalCurrent = typeof(ImmutableSet<>.Enumerator).GetField("InternalCurrent",BindingFlags.NonPublic|BindingFlags.Instance)!;
    }
}