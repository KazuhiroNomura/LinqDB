using System.Reflection;
using System.Collections.Generic;
using LinqDB.Sets;
using System.Linq;
using Extension = System.Linq.Enumerable;

namespace LinqDB.Reflection;

using static Common;
/// <summary>
/// ループ展開するEnumerable拡張クラスのstaticメソッド
/// </summary>
public static class ExtensionEnumerable2 {
#if DEBUG
    static ExtensionEnumerable2() {
        var HashSet = new HashSet<int>();
        foreach(var Field in typeof(ExtensionEnumerable2).GetFields(BindingFlags.Public|BindingFlags.Static))
            System.Diagnostics.Debug.Assert(HashSet.Add(Field.MetadataToken));
    }
#endif
    private const IEnumerable<int> _Int32 = null!;
    //Enumerable独自非インラインメソッド
    public static readonly MethodInfo Concat = M(() => _Int32.Concat(default!));
    public static readonly MethodInfo Contains_value_comparer = M(() => _Int32.Contains(0,null));
    public static readonly MethodInfo Count = M(() => _Int32.Count());
    public static readonly MethodInfo Count_predicate = M(() => _Int32.Count(null!));
    public static readonly MethodInfo ElementAt = M(() => _Int32.ElementAt(0));
    public static readonly MethodInfo ElementAtOrDefault = M(() => _Int32.ElementAtOrDefault(0));
    public static readonly MethodInfo Empty = M(() => Extension.Empty<int>());
    public static readonly MethodInfo First = M(() => _Int32.First());
    public static readonly MethodInfo First_predicate = M(() => _Int32.First(null!));
    public static readonly MethodInfo FirstOrDefault0 = M(() => _Int32.FirstOrDefault());
    public static readonly MethodInfo FirstOrDefault1 = M(() => _Int32.FirstOrDefault(null!));
    public static readonly MethodInfo Last = M(() => _Int32.Last());
    public static readonly MethodInfo Last_predicate = M(() => _Int32.Last(null!));
    public static readonly MethodInfo LastOrDefault = M(() => _Int32.LastOrDefault());
    public static readonly MethodInfo LastOrDefault_predicate = M(() => _Int32.LastOrDefault(null!));
    public static readonly MethodInfo LongCount_predicate = M(() => _Int32.LongCount(null!));
    public static readonly MethodInfo OrderBy = M(() => _Int32.OrderBy(p => p));
    public static readonly MethodInfo OrderBy_comparer = M(() => _Int32.OrderBy(p => p,null));
    public static readonly MethodInfo OrderByDescending = M(() => _Int32.OrderByDescending(p => p));
    public static readonly MethodInfo OrderByDescending_comparer = M(() => _Int32.OrderByDescending(p => p,null));
    public static readonly MethodInfo ThenBy = M(() => _Int32.OrderBy(p => 0).ThenBy(p => p));
    public static readonly MethodInfo ThenBy_comparer = M(() => _Int32.OrderBy(p => 0).ThenBy(p => 0,null));
    public static readonly MethodInfo ThenByDescending = M(() => _Int32.OrderBy(p => 0).ThenByDescending(p => p));
    public static readonly MethodInfo ThenByDescending_comparer = M(() => _Int32.OrderBy(p => 0).ThenByDescending(p => 0,null));
    public static readonly MethodInfo ToDictionary_keySelector = M(() => _Int32.ToDictionary(p => p));
    public static readonly MethodInfo ToDictionary_keySelector_elementSelector = M(() => _Int32.ToDictionary(p => p,p => p));
    public static readonly MethodInfo ToDictionary_keySelector_comparer = M(() => _Int32.ToDictionary(p => p,default));
    public static readonly MethodInfo ToDictionary_keySelector_elementSelector_comparer = M(() => _Int32.ToDictionary(p => p,p => p,default));
    public static readonly MethodInfo ToList = M(() => _Int32.ToList());
    public static readonly MethodInfo ToLookup_keySelector = M(() => _Int32.ToLookup(p => p));
    public static readonly MethodInfo ToLookup_keySelector_elementSelector = M(() => _Int32.ToLookup(p => p,p => p));
    public static readonly MethodInfo ToLookup_comparer = M(() => _Int32.ToLookup(p => p,default));
    public static readonly MethodInfo ToLookup_keySelector_elementSelector_comparer = M(() => _Int32.ToLookup(p => p,p => p,default));
    public static readonly MethodInfo Zip = M(() => _Int32.Zip(_Int32,(p,q) => 0));
    public static readonly MethodInfo SequenceEqual = M(() => _Int32.SequenceEqual(default!));
    public static readonly MethodInfo SequenceEqual_comparer = M(() => _Int32.SequenceEqual(default!,null));
    public static readonly MethodInfo Skip = M(() => _Int32.Skip(0));
    public static readonly MethodInfo SkipWhile = M(() => _Int32.SkipWhile(p => true));
    public static readonly MethodInfo SkipWhile_index = M(() => _Int32.SkipWhile((p,i) => true));
    public static readonly MethodInfo Lookup_comparer = M(() => _Int32.Lookup(p => p,null!));
    public static readonly MethodInfo Lookup_index_comparer = M(() => _Int32.Lookup((p,index) => p+index,null!));
}