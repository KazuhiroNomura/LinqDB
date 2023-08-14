using System.Collections.Generic;
namespace LinqDB.Sets;

/// <summary>
/// ExtensionSet.GroupJoin,ExtensionSet.Joinなどのハッシュ結合で使うDictionaryコレクション
/// </summary>
/// <typeparam name="TValue">値のType</typeparam>
/// <typeparam name="TKey">結合式のType</typeparam>
public sealed class LookupSet<TValue, TKey>:Lookup<TValue,TKey,Set<TValue>>{
    /// <summary>
    /// 既定コンストラクタ
    /// </summary>
    public LookupSet(){
    }
    /// <summary>
    /// 比較方法を指定したコンストラクタ
    /// </summary>
    /// <param name="KeyComparer">比較方法</param>
    public LookupSet(IEqualityComparer<TKey> KeyComparer):base(KeyComparer){
    }
    private static readonly Set<TValue> EmptyCollection = new();
    /// <summary>指定したキーに関連付けられている値を取得します。</summary>
    /// <returns>指定したキーに対応するCollection。それ以外の場合はEmptyなCollection。</returns>
    /// <param name="Key"></param>
    public ImmutableSet<TValue> GetTKeyValue(TKey Key) => this.GetValue(Key,EmptyCollection);
    /// <summary>指定したキーに関連付けられている値を取得します。</summary>
    /// <returns>指定したキーに対応するCollection。それ以外の場合はEmptyなCollection。</returns>
    /// <param name="Key"></param>
    public ImmutableSet<TValue> GetObjectValue(object Key) => this.GetValue(Key,EmptyCollection);
    internal override KeyValueCollection<TValue,TKey,Set<TValue>> InternalKeyValue(TKey Key,TValue Value) =>
        new(Key,new Set<TValue> { Value });
}