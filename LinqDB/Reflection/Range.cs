using System.Reflection;

namespace LinqDB.Reflection;

using static Common;
internal static class Range{
    public static readonly PropertyInfo Start= P(() => new System.Range().Start);
    public static readonly PropertyInfo End= P(() => new System.Range().End);

}