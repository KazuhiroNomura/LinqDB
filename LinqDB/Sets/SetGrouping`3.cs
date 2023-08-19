#pragma warning disable CS8618 // Null 非許容フィールドは初期化されていません。null 許容として宣言することを検討してください。
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
namespace LinqDB.Sets;

/// <summary>
/// GroupByの結果。タプルはIGrouping{TKey,TValue}。
/// </summary>
/// <typeparam name="TKey">結合式のType</typeparam>
/// <typeparam name="TValue">値のType</typeparam>
/// <typeparam name="TGrouping"></typeparam>
[Serializable]
public abstract class SetGrouping<TKey, TValue, TGrouping>:ImmutableSet<TGrouping>where TGrouping : IGrouping<TKey,TValue>, ICollection<TValue> {
    /// <summary>
    /// キー比較用EqualityComparer
    /// </summary>
    protected readonly IEqualityComparer<TKey> KeyComparer;
    protected SetGrouping(SerializationInfo SerializationInfo,StreamingContext StreamingContext) : base(SerializationInfo,StreamingContext) {
    }
    /// <summary>
    /// 既定コンストラクタ
    /// </summary>
    protected SetGrouping(): this(EqualityComparer<TKey>.Default) { }
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="KeyComparer">キーの比較時に使用する <see cref="EqualityComparer{TKey}" /> 実装。キーの型の既定の <see cref="EqualityComparer{TKey}" /> を使用する場合は null。</param>
    protected SetGrouping(IEqualityComparer<TKey> KeyComparer) => this.KeyComparer=KeyComparer;
    /// <summary>
    /// 指定したキーと値をディクショナリに追加する。
    /// KeyがなければTGrouping(Key,Value)
    /// KeyがあればTGroupingを取り出しAdd(Value)
    /// </summary>
    /// <param name="Key">追加する要素のキー。</param>
    /// <param name="Value">追加する要素の値。参照型の場合、null の値を使用できます。</param>
    internal void AddKeyValue(TKey Key,TValue Value) {
        var HashCode = (long)(uint)Key!.GetHashCode();
        if(this.InternalAdd前半(out var 下限,out var 上限,out var TreeNode,HashCode)) {
            var KeyComparer = this.KeyComparer;
            LinkedNode LinkedNode = TreeNode;
            while(true) {
                var LinkedNode_LinkedNodeItem = LinkedNode._LinkedNodeItem;
                if(LinkedNode_LinkedNodeItem is null) {
                    LinkedNode._LinkedNodeItem=new LinkedNodeItem(this.InternalKeyValue(Key,Value));
                    this._Count++;
                    return;
                }
                if(KeyComparer.Equals(LinkedNode_LinkedNodeItem.Item.Key,Key)) {
                    LinkedNode_LinkedNodeItem.Item.Add(Value);
                    return;
                }
                LinkedNode=LinkedNode_LinkedNodeItem;
            }
        }
        InternalAdd後半(
            下限,
            上限,
            TreeNode,
            HashCode,
            new LinkedNodeItem(this.InternalKeyValue(Key,Value))
        );
        this._Count++;
    }
    internal abstract TGrouping InternalKeyValue(TKey Key,TValue Value);
}