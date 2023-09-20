using System;
using System.Collections;

namespace LinqDB.Sets;

/// <summary>
/// Setの基底クラス
/// </summary>
[Serializable,MessagePack.MessagePackObject]
public abstract partial class ImmutableSet:IOutputSet {
    internal const long 初期下限 = 0;
    internal const long 初期上限 = uint.MaxValue+(long)uint.MaxValue;
    /// <summary>
    /// Interlockedで使用するためプロパティではいけない
    /// </summary>
    [MessagePack.Key(0)]
    protected internal long _Count;
    /// <summary>
    /// タプルの濃度(要素数)
    /// </summary>
    [MessagePack.IgnoreMember]
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