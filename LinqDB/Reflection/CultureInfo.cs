using System.Reflection;
namespace LinqDB.Reflection;

using static Common;
internal static class CultureInfo {
    public static readonly PropertyInfo CurrentCulture = P(() => System.Globalization.CultureInfo.CurrentCulture);
}