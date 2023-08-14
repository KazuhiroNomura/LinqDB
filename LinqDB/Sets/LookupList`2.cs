using System.Collections.Generic;
namespace LinqDB.Sets;

/// <summary>
/// Enumerable.GroupJoin,Enumerable.Joinなどのハッシュ結合で使うDictionaryコレクション
/// </summary>
/// <typeparam name="TValue">値のType</typeparam>
/// <typeparam name="TKey">結合式のType</typeparam>
public sealed class LookupList<TValue, TKey>:Lookup<TValue,TKey,AscList<TValue>>{
    /// <summary>
    /// 既定コンストラクタ
    /// </summary>
    public LookupList() {
    }
    /// <summary>
    /// 比較方法を指定したコンストラクタ
    /// </summary>
    /// <param name="KeyComparer">比較方法</param>
    public LookupList(IEqualityComparer<TKey> KeyComparer):base(KeyComparer) {
    }
    private static readonly AscList<TValue> EmptyCollection =new();
    /// <summary>指定したキーに関連付けられている値を取得します。</summary>
    /// <returns>指定したキーに対応するCollection。それ以外の場合はEmptyなCollection。</returns>
    /// <param name="Key"></param>
    public IEnumerable<TValue> GetTKeyValue(TKey Key) => this.GetValue(Key,EmptyCollection);
    /// <summary>指定したキーに関連付けられている値を取得します。</summary>
    /// <returns>指定したキーに対応するCollection。それ以外の場合はEmptyなCollection。</returns>
    /// <param name="Key"></param>
    public IEnumerable<TValue> GetObjectValue(object Key) => this.GetValue(Key,EmptyCollection);
    internal override KeyValueCollection<TValue,TKey,AscList<TValue>> InternalKeyValue(TKey Key,TValue Value) {
        var AscList = new AscList<TValue>();
        AscList.VoidAdd(Value);
        return new KeyValueCollection<TValue,TKey,AscList<TValue>>(Key,AscList);
    }
}