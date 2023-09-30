using System;
using Collections=System.Collections;
using System.Diagnostics;
using System.Globalization;
using Linq=System.Linq;
using System.Runtime.CompilerServices;
using LinqDB.Sets.Exceptions;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;
using LinqDB.Helpers;
using System.Runtime.Serialization;
using System.Text;
using LinqDB.CRC;
//using System.Linq;

namespace LinqDB.Sets;
using Generic=Collections.Generic;
internal sealed class SystemLinq_GroupingDebugView<TKey, TElement>
{
    private readonly GroupingSet<TKey,TElement>_grouping;

    private TElement[]? _cachedValues;

    public TKey Key => this._grouping.Key;

    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public TElement[] Values => this._cachedValues??=this._grouping.ToArray();

    public SystemLinq_GroupingDebugView(GroupingSet<TKey, TElement> grouping)
    {
        this._grouping = grouping;
    }
}

/// <summary>
/// Set&lt;T>.GroupByの結果の実体
/// </summary>
/// <typeparam name="TElement">値</typeparam>
/// <typeparam name="TKey">キー</typeparam>
[DebuggerDisplay("Key = {Key}")]
[DebuggerTypeProxy(typeof(SystemLinq_GroupingDebugView<, >))]
[Serializable,MessagePack.MessagePackObject]
public sealed class GroupingSet<TKey,TElement>:ImmutableSet<TElement>,IGroupingCollection<TKey,TElement>,ICollection<TElement>,IEquatable<IGrouping<TKey,TElement>>,IEquatable<Linq.IGrouping<TKey,TElement>>{
    static GroupingSet()=>MemoryPack.MemoryPackFormatterProvider.Register(Serializers.MemoryPack.Formatters.Sets.GroupingSet<TKey,TElement>.Instance);
    public TKey Key{get;}
    /// <summary>
    /// MessagePackが必要とする
    /// </summary>
    public GroupingSet()=>this.Key=default!;
    /// <summary>
    /// コンストラクタ。キーは必須
    /// </summary>
    /// <param name="Key">このキーに関連するタプルの集合</param>
    public GroupingSet(TKey Key)=>this.Key=Key;
    /// <summary>
    /// コンストラクタ。キーは必須
    /// </summary>
    /// <param name="Key">このキーに関連するタプルの集合</param>
    /// <param name="Value">1つのタプル</param>
    public GroupingSet(TKey Key,TElement Value){
        this.Key=Key;
        this.InternalAdd(Value);
        this._LongCount=1;
    }
    public void Add(TElement item) {
        if(this.InternalAdd(item)) this._LongCount++;
    }
    public void Clear() => this.InternalClear();
    public bool Contains(TElement item) => this.InternalContains(item);
    public void CopyTo(TElement[] array,int arrayIndex) {
        foreach(var a in this) {
            array[arrayIndex++]=a;
        }
    }

    public bool Remove(TElement item) => this.InternalRemove(item);
    public int Count => (int)this._LongCount;
    public bool IsReadOnly => false;
    public override int GetHashCode()=>this.Key!.GetHashCode();
    private bool PrivateEquals(IGrouping<TKey,TElement> other)=>Generic.EqualityComparer<TKey>.Default.Equals(this.Key,other.Key)&&this.SetEquals(other);
    public bool Equals(IGrouping<TKey,TElement>? other) {
        if(ReferenceEquals(null,other)) return false;
        if(ReferenceEquals(this,other)) return true;
        return this.PrivateEquals(other);
    }
    public bool Equals(Linq.IGrouping<TKey,TElement>? other) {
        if(ReferenceEquals(null,other)) return false;
        if(ReferenceEquals(this,other)) return true;
        var value=new GroupingSet<TKey,TElement>(other.Key);
        foreach(var a in other) value.Add(a);
        return this.PrivateEquals(value);
    }
    //Linq.IGrouping<TKey,TElement>
    public override bool Equals(object? obj){
        switch(obj){
            case IGrouping<TKey,TElement>other:return this.Equals(other);
            case Linq.IGrouping<TKey,TElement>other:return this.Equals(other);
            default:return false;
        }
    }
}
public class GroupingSet2<T>:ImmutableSet<T>,ICollection<T>{
    /// <summary>
    /// MessagePackが必要とする
    /// </summary>
    [MemoryPack.MemoryPackConstructor]
    public GroupingSet2(){
    }
    /// <summary>
    /// <see cref="GroupingSet2{T}" /> クラスの新しいインスタンスを初期化します。このセット型には等値比較子が使用されます。
    /// </summary>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="Generic.IEqualityComparer{T}" /> の実装。</param>
    public GroupingSet2(Generic.IEqualityComparer<T> Comparer):base(Comparer) {
    }
    /// <summary>
    ///   <see cref="GroupingSet2{T}" /> クラスの新しいインスタンスを初期化します。このセット型には既定の等値比較子が使用されます。指定されたコレクションからコピーされた要素が格納されますます。</summary>
    /// <param name="source">新しいセットの要素のコピー元となるコレクション。</param>
    public GroupingSet2(Generic.IEnumerable<T> source):base(source,Generic.EqualityComparer<T>.Default) {
    }
    /// <summary>
    ///   <see cref="GroupingSet2{T}" /> クラスの新しいインスタンスを初期化します。指定されたコレクションからコピーされた要素が格納されます。</summary>
    /// <param name="source">新しいセットの要素のコピー元となるコレクション。</param>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="Generic.IEqualityComparer{T}" /> の実装。</param>
    public GroupingSet2(Generic.IEnumerable<T> source,Generic.IEqualityComparer<T> Comparer) : base(source,Comparer) {
    }
    /// <summary>
    ///   <see cref="GroupingSet2{T}" /> クラスの新しいインスタンスを初期化します。指定されたコレクションからコピーされた要素が格納されます。</summary>
    /// <param name="source">新しいセットの要素のコピー元となる配列。</param>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="Generic.IEqualityComparer{T}" /> の実装。</param>
    public GroupingSet2(T[] source,Generic.IEqualityComparer<T> Comparer) :base(source,Comparer) {
    }
    /// <summary>
    ///   <see cref="GroupingSet2{T}" /> クラスの新しいインスタンスを初期化します。指定されたコレクションからコピーされた要素が格納されます。</summary>
    /// <param name="source">新しいセットの要素のコピー元となる配列。</param>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="Generic.IEqualityComparer{T}" /> の実装。</param>
    [MethodImpl(MethodImplOptions.NoInlining|MethodImplOptions.NoOptimization)]
    public GroupingSet2(ImmutableSet<T> source,Generic.IEqualityComparer<T> Comparer) :base(source,Comparer) {
    }
    public void Add(T item){
        if(this.InternalAdd(item)) this._LongCount++;
    }
    public bool Contains(T Item)=>this.InternalContains(Item);
    //    if(Item is null) return false;
    //    var HashCode = (long)(uint)Item.GetHashCode();
    //    if(this.InternalAdd前半(out var 下限,out var 上限,out var TreeNode,HashCode)) {
    //        var Comparer = this.Comparer;
    //        LinkedNodeT LinkedNode = TreeNode;
    //        while(true) {
    //            var LinkedNodeItem = LinkedNode._LinkedNodeItem;
    //            if(LinkedNodeItem is null)return false;
    //            if(Comparer.Equals(LinkedNodeItem.Item,Item))return true;
    //            LinkedNode=LinkedNodeItem;
    //        }
    //    }
    //    return false;
    //}
    public void CopyTo(T[] array,int arrayIndex){
        throw new NotImplementedException();
    }
    /// <summary>
    /// 全要素削除します。
    /// </summary>
    public  void Clear() {
        var TreeRoot = this.TreeRoot;
        TreeRoot.L=TreeRoot.R=null;
        TreeRoot._LinkedNodeItem=null;
        this._LongCount=0;
    }
    /// <summary>
    /// 要素の追加処理。
    /// </summary>
    /// <param name="Item">値</param>
    /// <returns>追加に失敗すれば例外送出。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddOrThrow(T Item) {
        if(this.InternalAdd(Item)) {
            this._LongCount++;
        } else {
            throw new ArgumentException(Item!.ToString());
        }
    }
    /// <summary>
    /// 要素の追加処理。
    /// </summary>
    /// <param name="Item">値</param>
    /// <returns>追加に成功すればtrue、失敗すればfalse。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsAdded(T Item) {
        if(this.InternalAdd(Item)) {
            this._LongCount++;
            return true;
        } else {
            return false;
        }
    }
    /// <summary>
    /// 要素集合の追加処理。
    /// </summary>
    /// <param name="source">追加したい集合</param>
    /// <returns>追加に成功すればtrue、失敗すればfalse。</returns>
    public void AddRange(Generic.IEnumerable<T>source) {
        var Count = this._LongCount;
        foreach(var Item in source) {
            if(this.InternalAdd(Item)) {
                Count++;
            }
        }
        this._LongCount+=Count;
    }
    //public Boolean AddRange(ImmutableSet<T> source) {
    //    foreach(var Item in source) {
    //        if(this.InternalContains(Item)) return false;
    //    }
    //    foreach(var Item in source) {
    //        this.InternalAdd(Item);
    //    }
    //    this._Count+=source.Count;
    //    return true;
    //}
    /// <summary>
    /// 複数要素の追加処理。
    /// </summary>
    /// <param name="source">ImmutableSet値</param>
    /// <returns>追加に失敗すれば例外送出。</returns>
    public void AddRangeOrThrow(ImmutableSet<T> source) {
        foreach(var Item in source) {
            if(this.InternalContains(Item)) throw new ArgumentException(Item!.ToString());
        }
        foreach(var Item in source) {
            this.InternalAdd(Item);
        }
        this._LongCount+=source._LongCount;
    }
    /// <summary>
    /// 要素を削除する。
    /// </summary>
    /// <param name="Item">削除したい値</param>
    /// <returns>要素がなければfalse、あればtrue</returns>
    public bool Remove(T Item){
        if(this.InternalRemove(Item)) {
            this._LongCount--;
            return true;
        }
        return false;
    }
    public bool IsReadOnly=>true;

    public int Count => checked((int)this.LongCount);


    /// <summary>
    /// 要素を削除する。失敗は無視する。
    /// </summary>
    /// <param name="Item">削除したい値</param>
    public void VoidRemove(T Item) {
        if(this.InternalRemove(Item))this._LongCount--;
    }
    /// <summary>
    /// 要素を削除する。ConcurrentRemoveで並列化できる失敗は無視する。
    /// </summary>
    /// <param name="Item">削除したい値</param>
    public void ConcurrentVoidRemove(T Item)=>this.InternalRemove(Item);
    /// <summary>
    /// 自身をUpdateで書き換える。
    /// </summary>
    /// <param name="setSelector">置換する場合の式</param>
    /// <param name="predicate">置換対象条件</param>
    /// <returns></returns>
    public (long Remove行数, long Add行数) UpdateWith(Func<T,T> setSelector,Func<T,bool> predicate) {
        var TreeNode = this.TreeRoot;
        var Remove数 = 0L;
        LinkedNodeItemT? RemoveLinkedNodeItem = null;
    LinkedNodeItem走査:
        LinkedNodeT 前LinkedNodeItem = TreeNode;
        var LinkedNodeItem = TreeNode._LinkedNodeItem;
        while(LinkedNodeItem is not null) {
            if(predicate(LinkedNodeItem.Item)) {
                var 後LinkedNodeItem = LinkedNodeItem._LinkedNodeItem;
                前LinkedNodeItem._LinkedNodeItem=後LinkedNodeItem;
                LinkedNodeItem._LinkedNodeItem=RemoveLinkedNodeItem;
                RemoveLinkedNodeItem=LinkedNodeItem;
                Remove数++;
                LinkedNodeItem=後LinkedNodeItem;
            } else {
                前LinkedNodeItem=LinkedNodeItem;
                LinkedNodeItem=LinkedNodeItem._LinkedNodeItem;
            }
        }
        if(TreeNode.L is not null) {
            TreeNode=TreeNode.L;
            goto LinkedNodeItem走査;
        }
    右に移動:
        if(TreeNode.R is not null) {
            TreeNode=TreeNode.R;
            goto LinkedNodeItem走査;
        }
        //上に移動
        // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
        while(TreeNode.P is not null) {
            var P = TreeNode.P;
            if(P.L==TreeNode) {
                if(TreeNode._LinkedNodeItem is null&&TreeNode.L is null&&TreeNode.R is null)
                    P.L=null;
                TreeNode=P;
                goto 右に移動;
            }
            if(TreeNode._LinkedNodeItem is null&&TreeNode.L is null&&TreeNode.R is null)
                P.R=null;
            TreeNode=P;
        }
        var Add数 = 0L;
        for(var Node = RemoveLinkedNodeItem;Node is not null;Node=Node._LinkedNodeItem)
            if(this.InternalAdd(setSelector(Node.Item)))
                Add数++;
        this._LongCount-=Remove数-Add数;
        return (Remove数, Add数);
    }
    /// <summary>
    /// 自身をUpdateで書き換える。
    /// </summary>
    /// <param name="setSelector">置換する場合の式</param>
    /// <returns></returns>
    public (long Remove行数, long Add行数) UpdateWith(Func<T,T> setSelector) {
        var TreeNode = this.TreeRoot;
        var Remove数 = 0L;
        LinkedNodeItemT? RemoveLinkedNodeItem = null;
    LinkedNodeItem走査:
        LinkedNodeT 前LinkedNodeItem = TreeNode;
        var LinkedNodeItem = TreeNode._LinkedNodeItem;
        while(LinkedNodeItem is not null) {
            //predicateの違いだけ
            var 後LinkedNodeItem = LinkedNodeItem._LinkedNodeItem;
            前LinkedNodeItem._LinkedNodeItem=後LinkedNodeItem;
            LinkedNodeItem._LinkedNodeItem=RemoveLinkedNodeItem;
            RemoveLinkedNodeItem=LinkedNodeItem;
            Remove数++;
            LinkedNodeItem=後LinkedNodeItem;
        }
        if(TreeNode.L is not null) {
            TreeNode=TreeNode.L;
            goto LinkedNodeItem走査;
        }
    右に移動:
        if(TreeNode.R is not null) {
            TreeNode=TreeNode.R;
            goto LinkedNodeItem走査;
        }
        //上に移動
        // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
        while(TreeNode.P is not null) {
            var P = TreeNode.P;
            if(P.L==TreeNode) {
                if(TreeNode._LinkedNodeItem is null&&TreeNode.L is null&&TreeNode.R is null)
                    P.L=null;
                TreeNode=P;
                goto 右に移動;
            }
            if(TreeNode._LinkedNodeItem is null&&TreeNode.L is null&&TreeNode.R is null)
                P.R=null;
            TreeNode=P;
        }
        var Add数 = 0L;
        for(var Node = RemoveLinkedNodeItem;Node is not null;Node=Node._LinkedNodeItem)
            if(this.InternalAdd(setSelector(Node.Item)))
                Add数++;
        this._LongCount-=Remove数-Add数;
        return (Remove数, Add数);
    }
    private static long PrivateDeleteWith(Func<T,bool> predicate,ref TreeNodeT? ref_TreeNode,int スレッド先頭,int スレッド末尾) {
        Debug.Assert(スレッド先頭<スレッド末尾);
        var TreeNode = ref_TreeNode;
        if(TreeNode is null) {
            return 0;
        }
        long Remove数;
        var スレッド範囲 = スレッド末尾-スレッド先頭;
        if(スレッド範囲==1) { 
            Remove数=PrivateDeleteWith(predicate,ref TreeNode.L)+PrivateDeleteWith(predicate,ref TreeNode.R);
        }else {
            var Remove数0 = 0L;
            var Remove数1 = 0L;
            var TreeNode2 = TreeNode;
            var スレッド中間番号 = スレッド先頭+スレッド範囲/2;
            Parallel.Invoke(
                () => Remove数0=PrivateDeleteWith(predicate,ref TreeNode2.L,スレッド先頭,スレッド中間番号),
                () => Remove数1=PrivateDeleteWith(predicate,ref TreeNode2.R,スレッド中間番号,スレッド末尾)
            );
            Remove数=Remove数0+Remove数1;
        }
        //ノードのリーフを走査する。
        LinkedNodeT 前TreeNode = TreeNode;
        for(var LinkedNodeItem = TreeNode._LinkedNodeItem;LinkedNodeItem is not null;LinkedNodeItem=LinkedNodeItem._LinkedNodeItem)
            if(predicate(LinkedNodeItem.Item)) {
                前TreeNode._LinkedNodeItem=LinkedNodeItem._LinkedNodeItem;
                Remove数++;
            } else
                前TreeNode=LinkedNodeItem;
        if(TreeNode.L is null && TreeNode.R is null && TreeNode._LinkedNodeItem is null)
            ref_TreeNode=null;
        return Remove数;
    }
    private static long PrivateDeleteWith(Func<T,bool> predicate,ref TreeNodeT? ref_TreeNode) {
        var TreeNode = ref_TreeNode;
        if(TreeNode is null)
            return 0;
        var Remove数 = 0L;
    LinkedNodeItem走査:
        LinkedNodeT 前TreeNode = TreeNode;
        for(var LinkedNodeItem = TreeNode._LinkedNodeItem;LinkedNodeItem is not null;
            LinkedNodeItem=LinkedNodeItem._LinkedNodeItem) {
            if(predicate(LinkedNodeItem.Item)) {
                前TreeNode._LinkedNodeItem=LinkedNodeItem._LinkedNodeItem;
                Remove数++;
            } else {
                前TreeNode=LinkedNodeItem;
            }
        }
        if(TreeNode.L is not null) {
            TreeNode=TreeNode.L;
            goto LinkedNodeItem走査;
        }
    右に移動:
        if(TreeNode.R is not null) {
            TreeNode=TreeNode.R;
            goto LinkedNodeItem走査;
        }
        // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
        while(true) {
            var P = TreeNode.P;
            if(P is null)
                break;
            if(P.L==TreeNode) {
                //右上に移動
                if(TreeNode.L is null&&TreeNode.R is null&&TreeNode._LinkedNodeItem is null)
                    P.L=null;
                TreeNode=P;
                goto 右に移動;
            }
            if(TreeNode.L is null&&TreeNode.R is null&&TreeNode._LinkedNodeItem is null)
                P.R=null;
            TreeNode=P;
        }
        if(TreeNode.L is null&&TreeNode.R is null&&TreeNode._LinkedNodeItem is null)
            ref_TreeNode=null;
        return Remove数;
    }
    /// <summary>
    /// 自身から削除する。
    /// </summary>
    /// <param name="predicate">削除条件</param>
    /// <returns></returns>
    public long DeleteWith(Func<T,bool> predicate) {
        var TreeRoot = this.TreeRoot;
        var Remove数 = PrivateDeleteWith(predicate,ref TreeRoot,0,Environment.ProcessorCount);
        this._LongCount-=Remove数;
        return Remove数;
    }
    /// <summary>現在の <see cref="Set{T}" /> オブジェクトを、そのオブジェクトと指定されたコレクションの両方に存在する要素だけが格納されるように変更します。</summary>
    /// <param name="other">現在の <see cref="Set{T}" /> オブジェクトと比較するコレクション。</param>
    public long IntersectWith(IEnumerable<T> other) {
        var r = new Set<T>();
        long Count = 0;
        foreach(var a in other)
            if(ExtensionSet.Contains(this,a)&&r.InternalAdd(a))
                Count++;
        this.変数Enumerator=r.変数Enumerator;
        return this._LongCount=Count;
    }
    /// <summary>
    /// 現在の <see cref="Set{T}" /> オブジェクトを、そのオブジェクトと指定されたコレクションに存在するすべての要素が格納されるように変更します。</summary>
    /// <param name="other">現在の <see cref="Set{T}" /> オブジェクトと比較するコレクション。</param>
    public long UnionWith(IEnumerable<T> other) {
        long Count = 0;
        foreach(var a in other)
            if(this.InternalAdd(a))
                Count++;
        this._LongCount+=Count;
        return Count;
    }
    /// <summary>
    /// 和集合。重複があったら例外。
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="OneTupleException"></exception>
    public long DUnionWith(IEnumerable<T> other) {
        long Count = 0;
        foreach(var a in other) {
            if(!this.InternalAdd(a))
                throw new OneTupleException(MethodBase.GetCurrentMethod()!.Name);
            Count++;
        }
        this._LongCount+=Count;
        return Count;
    }
    /// <summary>現在の <see cref="Set{T}" /> オブジェクトから、指定されたコレクションに含まれる要素をすべて削除します。</summary>
    /// <param name="other">
    ///   <see cref="Set{T}" /> オブジェクトから削除する項目のコレクション。</param>
    public long ExceptWith(IEnumerable<T> other) {
        long Count = 0;
        foreach(var a in other)
            if(this.InternalRemove(a))
                Count++;
        this._LongCount-=Count;
        return Count;
    }
    /// <summary>
    /// 完全差集合。
    /// </summary>
    /// <param name="other"></param>
    public void SymmetricExceptWith(IEnumerable<T> other) {
        var second=(Set<T>)other;
        var Result = new Set<T>();
        var Count = 0L;
        foreach(var a in this){
            if(second.InternalContains(a))continue;
            var r = Result.InternalAdd(a);
            Debug.Assert(r);
            Count++;
        }
        foreach(var a in second){
            if(this.InternalContains(a))continue;
            var r = Result.InternalAdd(a);
            Debug.Assert(r);
            Count++;
        }
        Result._LongCount=Count;
        this.変数Enumerator=Result.変数Enumerator;
        this._LongCount=Result._LongCount;
    }

}
