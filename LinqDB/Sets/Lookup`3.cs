#pragma warning disable CS8618 // Null 非許容フィールドは初期化されていません。null 許容として宣言することを検討してください。
using System;
using System.Runtime.CompilerServices;
namespace LinqDB.Sets;
using Generic=System.Collections.Generic;

/// <summary>キーと値のペアのジェネリック コレクションを表します。ハッシュジョインで使う</summary>
/// <typeparam name="TValue">ディクショナリ内の値の型。</typeparam>
/// <typeparam name="TKey">ディクショナリ内のキーの型。</typeparam>
/// <typeparam name="TCollection">TValueのIAddを継承した型</typeparam>
public abstract class Lookup<TValue, TKey, TCollection>:ImmutableSet<KeyValueCollection<TValue,TKey,TCollection>>where TCollection:class,Generic.ICollection<TValue>,new() {
    /// <summary>
    /// キー比較用EqualityComparer
    /// </summary>
    [NonSerialized]
    protected readonly Generic.IEqualityComparer<TKey> KeyComparer;
    /// <summary>空で、既定の初期量を備え、キーの型の既定の等値比較子を使用する、<see cref="Lookup{TValue, TKey, TCollection}" /> クラスの新しいインスタンスを初期化します。</summary>
    protected Lookup() : this(Generic.EqualityComparer<TKey>.Default){}
    /// <summary>コンストラクタ</summary>
    /// <param name="KeyComparer">キーの比較時に使用する <see cref="Generic.IEqualityComparer{T}" /> 実装。キーの型の既定の <see cref="Generic.EqualityComparer{T}" /> を使用する場合は null。</param>
    protected Lookup(Generic.IEqualityComparer<TKey> KeyComparer) => this.KeyComparer=KeyComparer;
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
                    this._LongCount++;
                    return;
                }
                if(KeyComparer.Equals(LinkedNode_LinkedNodeItem.Item.Key,Key)) {
                    LinkedNode_LinkedNodeItem.Item.Add(Value);
                    return;
                }
                LinkedNode=LinkedNode_LinkedNodeItem;
            }
        }
        InternalAdd後半(下限,上限,TreeNode,HashCode,new LinkedNodeItemT(this.InternalKeyValue(Key,Value)));
        this._LongCount++;
    }
    private TCollection?GetCollection(TKey Key){
        var TreeNode = this.InternalHashCodeに一致するTreeNodeを取得する((uint)Key!.GetHashCode());
        if(TreeNode is not null) {
            var KeyComparer = this.KeyComparer;
            for(var a = TreeNode._LinkedNodeItem;a is not null;a=a._LinkedNodeItem){
                if(KeyComparer.Equals(a.Item.Key,Key)) return a.Item.Collection;
            }
        }
        return null;
    }
    /// <summary>指定したキーが格納されているかどうかを判断します。</summary>
    /// <returns>指定したキーが格納されている場合はtrue。それ以外の場合は false。</returns>
    /// <param name="Key">
    ///   <see cref="Lookup{TValue, TKey, TCollection}" /> 内で検索されるキー。</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsKey(TKey Key)=>this.GetCollection(Key) is not null;
    //bool System.Linq.ILookup<TKey,TValue>.Contains(TKey key)=>this.ContainsKey(key);
    //Generic.IEnumerator<System.Linq.IGrouping<TKey,TValue>> Generic.IEnumerable<System.Linq.IGrouping<TKey,TValue>>.GetEnumerator() {
    //    foreach(var a in this) yield return a;
    //}

    //Generic.IEnumerator<IGrouping<TKey,TValue>> Generic.IEnumerable<IGrouping<TKey,TValue>>.GetEnumerator() {
    //    foreach(var a in this) yield return a;
    //}
    //int System.Linq.ILookup<TKey,TValue>.Count=>checked((int)this._LongCount);

    //Generic.IEnumerable<TValue> System.Linq.ILookup<TKey,TValue>.this[TKey key]=>this.GetIndex(key);
    /// <summary>
    /// 指定したキーに関連付けられている値を取得します。
    /// </summary>
    /// <returns>指定したキーを持つ要素が <see cref="Lookup{TValue, TKey, TCollection}" /> に格納されている場合はCollection。それ以外の場合はnull。</returns>
    /// <param name="Key"></param>
    /// <param name="Default"></param>
    /// <returns></returns>
    private TCollection GetValue(TKey Key,TCollection Default){
        var Collection=this.GetCollection(Key);
        return Collection??Default;
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
            var KeyComparer = this.KeyComparer;
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
        var Item=this.GetCollection(Key);
        if(Item is not null){
            Collection=Item;
            return true;
        }
        return false;
    }
    internal abstract KeyValueCollection<TValue,TKey,TCollection> InternalKeyValue(TKey Key,TValue Value);

    //int ILookup<TKey,TValue>.Count => throw new NotImplementedException();

    protected TCollection GetIndex(TKey key){
        TCollection value=default!;
        if(this.TryGetValue(key,ref value)) return value;
        throw new NotImplementedException();
    }
    public TCollection this[TKey key]=>this.GetIndex(key);
    private static readonly TCollection EmptyCollection =new();



    /// <summary>指定したキーに関連付けられている値を取得します。</summary>
    /// <returns>指定したキーに対応するCollection。それ以外の場合はEmptyなCollection。</returns>
    /// <param name="Key"></param>
    public TCollection GetTKeyValue(TKey Key) => this.GetValue(Key,EmptyCollection);
    /// <summary>指定したキーに関連付けられている値を取得します。</summary>
    /// <returns>指定したキーに対応するCollection。それ以外の場合はEmptyなCollection。</returns>
    /// <param name="Key"></param>
    public TCollection GetObjectValue(object Key) => this.GetValue(Key,EmptyCollection);

}
