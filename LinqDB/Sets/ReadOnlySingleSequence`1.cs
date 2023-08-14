using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
// ReSharper disable LoopCanBeConvertedToQuery
// ReSharper disable ArrangeStaticMemberQualifier
namespace LinqDB.Sets;

/// <summary>
/// 走査を何度もするための代替コンテナ
/// </summary>
/// <typeparam name="T"></typeparam>
[Serializable]
public sealed class ReadOnlySingleSequence<T> {
    internal const int ArrayLength = 1024;
    /// <summary>
    /// 列挙子。値型なのでメソッド呼び出しがCallなので早い。
    /// </summary>
    public struct Enumerator:IEnumerator<T> {
        public T[] Array;
        public int Index;
        public long Count;
        public void Reset() {
            //値型なのでGetEnumeratorで取得することが初期化でありResetで再利用されることはない前提。
        }
        /// <summary>
        /// 要素が存在するかどうかの判定に使う
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() {
            if(this.Index==this.Array.Length) return false;
            this.InternalCurrent=this.Array[this.Index];
            this.Index++;
            return true;
        }
        /*
        /// <summary>
        /// 要素を追加
        /// </summary>
        /// <param name="Item"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T Item) {
            var Last = new LinkedNodeItem(Item);
            this.LastNode._LinkedNodeItem=Last;
            this.LastNode=Last;
        }
        /// <summary>
        /// リセットしてMoveNextできるようにする。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset() => this.CurrentNode=this.FirstNode;
        */
        /// <inheritdoc />
        public void Dispose() {
        }
        internal T InternalCurrent;
        /// <inheritdoc />
        public T Current {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.InternalCurrent;
        }
        object IEnumerator.Current {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#pragma warning disable CS8603 // Null 参照戻り値である可能性があります。
            get => this.InternalCurrent;
#pragma warning restore CS8603 // Null 参照戻り値である可能性があります。
        }
    }
    /// <summary>
    /// 性能のために値型の列挙子を表す
    /// </summary>
    [NonSerialized]
    private Enumerator 変数Enumerator;
    //private readonly T[] Array;
    public ReadOnlySingleSequence(ImmutableSet<T> source) {
        var Count = source.Count;
        this.変数Enumerator.Count=Count;
        var Array=new T[Count];
        this.変数Enumerator.Array=Array;
        var Index = 0;
        foreach(var a in source) {
            Array[Index++]=a;
        }
    }
    /// <summary>
    /// 値の列挙子
    /// </summary>
    /// <returns>Enumerator</returns>
    public Enumerator GetEnumerator() => this.変数Enumerator;
    /*
    public T[] Array;
    //private readonly T[] Array;
    public ReadOnlySequence(ImmutableSet<T> source) {
        var Array = new T[source.Count];
        this.Array=Array;
        var Index = 0;
        foreach(var a in source) {
            Array[Index++]=a;
        }
    }
    /// <summary>
    /// 値の列挙子
    /// </summary>
    /// <returns>Enumerator</returns>
    public IEnumerator<T> GetEnumerator() => (IEnumerator<T>)this.Array.GetEnumerator();
    */
}