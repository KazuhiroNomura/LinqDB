using System;
using System.Collections;

namespace LinqDB.Sets;

/// <summary>
/// Setの基底クラス
/// </summary>
[Serializable,MessagePack.MessagePackObject]
public abstract partial class ImmutableSet:IEnumerable {
    internal const long 初期下限 = 0;
    internal const long 初期上限 = uint.MaxValue+(long)uint.MaxValue;
    /// <summary>
    /// Interlockedで使用するためプロパティではいけない
    /// </summary>
    [MessagePack.Key(0)]
    protected internal long _LongCount;

    public long LongCount =>this._LongCount;
    ///// <summary>
    ///// タプルの濃度(要素数)
    ///// </summary>
    //[MessagePack.IgnoreMember]
    //public long LongCount => this._LongCount;

    /// <summary>コレクションを反復処理する列挙子を返します。</summary>
    /// <returns>コレクションを反復処理するために使用できる <see cref="IEnumerator" /> オブジェクト。</returns>
    IEnumerator System.Collections.IEnumerable.GetEnumerator() => this.ProtectedGetEnumerator();
    /// <summary>
    /// Enumeratorを返す。
    /// </summary>
    /// <returns></returns>
    private protected abstract IEnumerator ProtectedGetEnumerator();
}