#pragma warning disable CS8618 // Null 非許容フィールドは初期化されていません。null 許容として宣言することを検討してください。
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
namespace LinqDB.Sets;

/// <summary>キーと値のペアのジェネリック コレクションを表します。ハッシュジョインで使う</summary>
/// <typeparam name="TValue">ディクショナリ内の値の型。</typeparam>
/// <typeparam name="TKey">ディクショナリ内のキーの型。</typeparam>
/// <typeparam name="TCollection">TValueのIAddを継承した型</typeparam>
[Serializable]
public abstract class Lookup<TValue, TKey, TCollection>:ImmutableSet<KeyValueCollection<TValue,TKey,TCollection>>where TCollection:ICollection<TValue> {
    /// <summary>
    /// キー比較用EqualityComparer
    /// </summary>
    protected readonly IEqualityComparer<TKey> KeyComparer;
    /// <summary>空で、既定の初期量を備え、キーの型の既定の等値比較子を使用する、<see cref="Lookup{TValue, TKey, TCollection}" /> クラスの新しいインスタンスを初期化します。</summary>
    protected Lookup() : this(EqualityComparer<TKey>.Default) { }
    /// <summary>コンストラクタ</summary>
    /// <param name="KeyComparer">キーの比較時に使用する <see cref="IEqualityComparer{T}" /> 実装。キーの型の既定の <see cref="EqualityComparer{T}" /> を使用する場合は null。</param>
    protected Lookup(IEqualityComparer<TKey> KeyComparer) => this.KeyComparer=KeyComparer;
    /// <summary>
    /// 指定したキーと値をディクショナリに追加する。
    /// KeyがなければTGrouping(Key,Value)
    /// KeyがあればTGroupingを取り出しAdd(Value)
    /// </summary>
    /// <param name="Key">追加する要素のキー。</param>
    /// <param name="Value">追加する要素の値。</param>
    public void AddKeyValue(TKey Key,TValue Value) {
        var HashCode = (long)(uint)Key!.GetHashCode();
        if(this.InternalAdd前半(out var 下限,out var 上限,out var TreeNode,HashCode)) {
            var KeyComparer = this.KeyComparer;
            LinkedNodeT LinkedNode = TreeNode;
            while(true) {
                var LinkedNode_LinkedNodeItem = LinkedNode._LinkedNodeItem;
                if(LinkedNode_LinkedNodeItem is null) {
                    LinkedNode._LinkedNodeItem=new LinkedNodeItemT(this.InternalKeyValue(Key,Value));
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
            new LinkedNodeItemT(this.InternalKeyValue(Key,Value))
        );
        this._Count++;
    }
    /// <summary>指定したキーが格納されているかどうかを判断します。</summary>
    /// <returns>指定したキーが格納されている場合はtrue。それ以外の場合は false。</returns>
    /// <param name="Key">
    ///   <see cref="Lookup{TValue, TKey, TCollection}" /> 内で検索されるキー。</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsKey(TKey Key){
        var TreeNode = this.InternalHashCodeに一致するTreeNodeを取得する((uint)Key!.GetHashCode());
        if(TreeNode is not null) {
            var KeyComparer = this.KeyComparer;
            for(var a = TreeNode._LinkedNodeItem;a is not null;a=a._LinkedNodeItem) { 
                if(KeyComparer.Equals(a.Item.Key,Key)) {
                    return true;
                }
            }
        }
        return false;
    }
    /// <summary>
    /// 指定したキーに関連付けられている値を取得します。
    /// </summary>
    /// <returns>指定したキーを持つ要素が <see cref="Lookup{TValue, TKey, TCollection}" /> に格納されている場合はCollection。それ以外の場合はnull。</returns>
    /// <param name="Key"></param>
    /// <param name="Default"></param>
    /// <returns></returns>
    protected TCollection GetValue(TKey Key,TCollection Default) {
        var TreeNode = this.InternalHashCodeに一致するTreeNodeを取得する((uint)Key!.GetHashCode());
        if(TreeNode is not null){
            var KeyComparer = this.KeyComparer;
            for(var a = TreeNode._LinkedNodeItem;a is not null;a=a._LinkedNodeItem) { 
                if(KeyComparer.Equals(a.Item.Key,Key)){
                    return a.Item.Collection;
                }
            }
        }
        return Default;
    }
    /// <summary>
    /// 指定したキーに関連付けられている値を取得します。
    /// </summary>
    /// <returns>指定したキーを持つ要素が <see cref="Lookup{TValue, TKey, TCollection}" /> に格納されている場合はCollection。それ以外の場合はnull。</returns>
    /// <param name="Key"></param>
    /// <param name="Default"></param>
    /// <returns></returns>
    protected TCollection GetValue(object Key,TCollection Default) {
        var TreeNode = this.InternalHashCodeに一致するTreeNodeを取得する((uint)Key.GetHashCode());
        if(TreeNode is not null) {
            for(var a = TreeNode._LinkedNodeItem;a is not null;a=a._LinkedNodeItem) {
                if(a.Item.Key!.Equals(Key)) {
                    return a.Item.Collection;
                }
            }
        }
        return Default;
    }

    /// <summary>指定したキーに関連付けられている値を取得します。</summary>
    /// <returns>検索できた場合は true。それ以外の場合は false。</returns>
    /// <param name="Key">取得する値のキー。</param>
    /// <param name="Collection">キーが見つかった場合は、指定したキーに関連付けられている値が格納されます。</param>
    /// <exception cref="ArgumentNullException">
    ///   <paramref name="Key" /> が null です。</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(TKey Key,ref TCollection Collection){
        if(Key is null) return false;
        var TreeNode = this.InternalHashCodeに一致するTreeNodeを取得する((uint)Key.GetHashCode());
        if(TreeNode is not null) {
            var KeyComparer = this.KeyComparer;
            for(var a = TreeNode._LinkedNodeItem;a is not null;a=a._LinkedNodeItem) {
                if(KeyComparer.Equals(a.Item.Key,Key)) {
                    Collection=a.Item.Collection;
                    return true;
                }
            }
        }
        return false;
    }
    internal abstract KeyValueCollection<TValue,TKey,TCollection> InternalKeyValue(TKey Key,TValue Value);
}