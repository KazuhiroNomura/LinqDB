#pragma warning disable 1591
using System.Collections.Generic;
using System.Reflection;
using LinqDB.Sets;
namespace LinqDB.Reflection;

using static Common;
/// <summary>
/// 拡張メソッドのリフレクション
/// </summary>
public static class ExtensionSet2 {
#if DEBUG
    static ExtensionSet2() {
        var HashSet = new HashSet<int>();
        foreach(var Field in typeof(ExtensionSet2).GetFields(BindingFlags.Public|BindingFlags.Static))
            System.Diagnostics.Debug.Assert(HashSet.Add(Field.MetadataToken));
    }
#endif
    private const Set<int> _Int32 = null!;
    public static readonly MethodInfo SymmetricExcept = M(() => _Int32.SymmetricExcept(_Int32));
}