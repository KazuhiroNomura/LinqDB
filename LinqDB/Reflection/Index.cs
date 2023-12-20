using System.Reflection;

namespace LinqDB.Reflection;

using static Common;
internal static class Index{
    public static readonly PropertyInfo Value= P(() => new System.Index().Value);

}