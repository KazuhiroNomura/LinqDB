using System;
using System.Collections;
using System.Runtime.Serialization;

namespace LinqDB.Sets;

/// <summary>
/// Setの基底クラス
/// </summary>
[Serializable]
public abstract class ImmutableSet:IOutputSet {
    internal const long 初期下限 = 0;
    internal const long 初期上限 = uint.MaxValue+(long)uint.MaxValue;
    /// <summary>
    /// Interlockedで使用するためプロパティではいけない
    /// </summary>
    protected internal long _Count;
    /// <summary>
    /// タプルの濃度(要素数)
    /// </summary>
    [IgnoreDataMember]
    public long Count => this._Count;
    /// <summary>コレクションを反復処理する列挙子を返します。</summary>
    /// <returns>コレクションを反復処理するために使用できる <see cref="IEnumerator" /> オブジェクト。</returns>
    IEnumerator IEnumerable.GetEnumerator()=>this.ProtectedGetEnumerator();
    /// <summary>
    /// Enumeratorを返す。
    /// </summary>
    /// <returns></returns>
    private protected abstract IEnumerator ProtectedGetEnumerator();
}