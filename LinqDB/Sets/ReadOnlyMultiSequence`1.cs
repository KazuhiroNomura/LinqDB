#define 並列化
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
// ReSharper disable LoopCanBeConvertedToQuery
// ReSharper disable ArrangeStaticMemberQualifier
namespace LinqDB.Sets;

/// <summary>
/// 関係集合。
/// </summary>
/// <typeparam name="T"></typeparam>
[Serializable]
public sealed class ReadOnlyMultiSequence<T>{
    public const int ArrayLength = 1024;
    public class ArrayNode {
        public T[] Array;
        public ArrayNode? Next;
        public ArrayNode(T[] Array) {
            this.Array=Array;
        }
    }
    /// <summary>
    /// 列挙子。値型なのでメソッド呼び出しがCallなので早い。
    /// </summary>
    public struct Enumerator:IEnumerator<T> {
        public ArrayNode ArrayNode;
        public T[] Array;
        public int ArrayIndex,Index,EndIndex;
        public long ArrayCount;
        public void Reset() {
            //値型なのでGetEnumeratorで取得することが初期化でありResetで再利用されることはない前提。
        }
        /// <summary>
        /// 要素が存在するかどうかの判定に使う
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() {
            if(this.ArrayIndex<this.ArrayCount) {
                if(this.Index==ArrayLength) {
                    this.ArrayNode=this.ArrayNode.Next!;
                    if(this.ArrayNode is null) return false;
                    this.Array=this.ArrayNode.Array;
                    this.Index=0;
                    this.ArrayIndex++;
                }
            }else if(this.Index==this.EndIndex) return false;
            this.InternalCurrent=this.Array[this.Index];
            this.Index++;
            return true;
        }
        public void Dispose() {
        }
        internal T InternalCurrent;
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
    public ReadOnlyMultiSequence(ImmutableSet<T>source) {
        var Count=source.LongCount;
        var ArrayIndex = 0;
        var ArrayCount = Count/ArrayLength;
        var Enumerator=source.GetEnumerator();
        var Array=new T[ArrayLength];
        var ArrayNode = new ArrayNode(Array);
        var TailArrayNode = ArrayNode;
        this.変数Enumerator.ArrayNode=ArrayNode;
        this.変数Enumerator.Array=Array;
        this.変数Enumerator.ArrayCount=ArrayCount;
        var EndIndex=(int)(Count%ArrayLength);
        var Index = 0;
        while(Enumerator.MoveNext()) {
            Array[Index++]=Enumerator.InternalCurrent;
            if(Index==ArrayLength) {
                ArrayIndex++;
                if(ArrayIndex==ArrayCount) break;
                Array=new T[ArrayLength];
                ArrayNode=new ArrayNode(Array);
                TailArrayNode.Next=ArrayNode;
                TailArrayNode=ArrayNode;
                Index=0;
            }
        }
        if(EndIndex==0) {
            this.変数Enumerator.EndIndex=ArrayLength;
        } else {
            this.変数Enumerator.EndIndex=EndIndex;
            Array=new T[ArrayLength];
            ArrayNode=new ArrayNode(Array);
            TailArrayNode.Next=ArrayNode;
            Index=0;
            while(Enumerator.MoveNext()) {
                Array[Index++]=Enumerator.InternalCurrent;
            }
        }
    }
    /// <summary>
    /// 値の列挙子
    /// </summary>
    /// <returns>Enumerator</returns>
    public Enumerator GetEnumerator() => this.変数Enumerator;
}