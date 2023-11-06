#define 並列化
using System;
using System.Text;
using Collections=System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using LinqDB.Helpers;
using System.IO;
using LinqDB.CRC;
//using System.Collections;
//using MemoryPack;
//using MessagePack.Resolvers;
//using Utf8Json;
// ReSharper disable LoopCanBeConvertedToQuery
// ReSharper disable ArrangeStaticMemberQualifier
namespace LinqDB.Sets;
using Generic=Collections.Generic;

/// <summary>
/// 関係集合。
/// </summary>
/// <typeparam name="T"></typeparam>
//[Serializable,MessagePack.MessagePackObject,MemoryPack.MemoryPackable]
//[DebuggerDisplay("Count = {"+nameof(Count)+"}")]
[DebuggerTypeProxy(typeof(SetDebugView<>))]
public abstract class ImmutableSet<T>:ImmutableSet, IEnumerable<T>,IEquatable<IEnumerable<T>>{
    /// <summary>
    /// 要素のルート
    /// </summary>
    internal TreeNodeT TreeRoot => this.変数Enumerator.TreeNode;
    public (long TreeNode数, long LinkedNode数) 衝突数 => this.変数Enumerator.TreeNode.衝突数;
    /// <summary>
    /// タプルを追加した時に参照関係を作る。
    /// </summary>
    /// <param name="Item"></param>
    internal virtual void AddRelationship(T Item){}
    /// <summary>
    /// タプルを削除した時に参照関係を作る。
    /// </summary>
    /// <param name="Item"></param>
    internal virtual void RemoveRelationship(T Item){}
    /// <summary>
    /// HashCodeによるTreeNode探索
    /// </summary>
    /// <param name="HashCode"></param>
    /// <returns></returns>
    internal TreeNodeT? InternalHashCodeに一致するTreeNodeを取得する(long HashCode) {
        Debug.Assert(this.TreeRoot is not null);
        var TreeNode = this.TreeRoot;
        long 下限 = 初期下限, 上限 = 初期上限;
        do {
            var CurrentHashCode = (下限+上限)>>1;
            Debug.Assert(TreeNode is not null);
            if(HashCode<CurrentHashCode) {
                上限=CurrentHashCode-1L;
                TreeNode=TreeNode.L;
            } else if(HashCode>CurrentHashCode) {
                下限=CurrentHashCode+1L;
                TreeNode=TreeNode.R;
            } else {
                return TreeNode;
            }
        } while(TreeNode is not null);
        return null;
    }
    private readonly Random Random = new(1);
    //private List<T> List = new();
    public T Sampling {
        get{
            if(this._LongCount==0) throw new NotSupportedException();
            var 試行回数=0;
            while(true) {
                var LinkedNodeItem = this.GetSampling(this.TreeRoot,31);
                試行回数++;
                if(LinkedNodeItem is null) continue;
                return LinkedNodeItem.Item;
            }
        }
    }
    private LinkedNodeItemT? GetSampling(TreeNodeT TreeNode,int Level) {
        if(Level<0)return 共通();
        var 範囲 = 1L<<Level;
        var 値 = (long)(this.Random.NextDouble()*((double)範囲+1+範囲));
        //var 値 = this.Random.Next(3);
        if(値<範囲) {
            if(TreeNode.L is not null) {
                return this.GetSampling(TreeNode.L,Level-1);
            } else if(TreeNode.R is not null) {
                return this.GetSampling(TreeNode.R,Level-1);
            }
        } else if(値<範囲+範囲) {
            if(TreeNode.R is not null) {
                return this.GetSampling(TreeNode.R,Level-1);
            } else if(TreeNode.L is not null) {
                return this.GetSampling(TreeNode.L,Level-1);
            }
        } else {
            Debug.Assert(値==範囲+範囲);
        }
        return 共通();
        LinkedNodeItemT? 共通() {
            var Count = 0;
            for(var LinkedNodeItem = TreeNode.LinkedNodeItem;LinkedNodeItem is not null;LinkedNodeItem=LinkedNodeItem.LinkedNodeItem) {
                Count++;
            }
            var Index = this.Random.Next(Count);
            for(var LinkedNodeItem = TreeNode.LinkedNodeItem;LinkedNodeItem is not null;LinkedNodeItem=LinkedNodeItem.LinkedNodeItem) {
                if(Index==0) {
                    return LinkedNodeItem;
                }
                Index--;
            }
            return null;
        }
    }
    public T SamplingNullable=> this._LongCount==0 ? default! : this.Sampling;
    /// <summary>
    /// Add時の前半処理。目的のノードを探索する。存在しなければ作る。
    /// </summary>
    /// <param name="下限"></param>
    /// <param name="上限"></param>
    /// <param name="out_TreeNode"></param>
    /// <param name="HashCode"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool InternalAdd前半(out long 下限,out long 上限,out TreeNodeT out_TreeNode,long HashCode) {
        Debug.Assert(this.TreeRoot is not null);
        var TreeNode = this.TreeRoot;
        long 下限1 = 初期下限, 上限1 = 初期上限;
        while(true) {
            var CurrentHashCode = (上限1+下限1)>>1;
            if(HashCode<CurrentHashCode) {
                上限1=CurrentHashCode-1;
                if(TreeNode!.L is null) {
                    下限=下限1;
                    上限=上限1;
                    out_TreeNode=TreeNode.L=new TreeNodeT(TreeNode);
                    return false;
                }
                TreeNode=TreeNode.L;
            } else if(HashCode>CurrentHashCode) {
                下限1=CurrentHashCode+1;
                if(TreeNode!.R is null) {
                    下限=下限1;
                    上限=上限1;
                    out_TreeNode=TreeNode.R=new TreeNodeT(TreeNode);
                    return false;
                }
                TreeNode=TreeNode.R;
            } else {
                下限=0;
                上限=0;
                out_TreeNode=TreeNode!;
                return true;
            }
        }
    }
    /// <summary>
    /// Add時の後半処理。TreeNodeをItemが格納できるところまで追加し続ける。
    /// </summary>
    /// <param name="下限"></param>
    /// <param name="上限"></param>
    /// <param name="TreeNode"></param>
    /// <param name="HashCode"></param>
    /// <param name="LinkedNodeItemT"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void InternalAdd後半(long 下限,long 上限,TreeNodeT TreeNode,long HashCode,LinkedNodeItemT LinkedNodeItemT) {
        while(true) {
            var CurrentHashCode = (下限+上限)>>1;
            /*
            if(HashCode==CurrentHashCode) {
                Debug.Assert(TreeNode._LinkedNodeItem is null);
                TreeNode._LinkedNodeItem=LinkedNodeItem;
                break;
            }
            TreeNode=new TreeNode(TreeNode);
            if(HashCode<CurrentHashCode) {
                上限=CurrentHashCode-1;
                Debug.Assert(TreeNode.L is null);
                TreeNode.L=TreeNode;
            } else {
                Debug.Assert(HashCode>CurrentHashCode);
                下限=CurrentHashCode+1;
                Debug.Assert(TreeNode.R is null);
                TreeNode.R=TreeNode;
            }
            */
            if(HashCode<CurrentHashCode) {
                上限=CurrentHashCode-1;
                Debug.Assert(TreeNode.L is null);
                TreeNode=TreeNode.L=new TreeNodeT(TreeNode);
            } else if(HashCode>CurrentHashCode) {
                下限=CurrentHashCode+1;
                Debug.Assert(TreeNode.R is null);
                TreeNode=TreeNode.R=new TreeNodeT(TreeNode);
            } else {
                Debug.Assert(TreeNode._LinkedNodeItem is null);
                TreeNode._LinkedNodeItem=LinkedNodeItemT;
                break;
            }
        }
    }
    /// <summary>
    /// 要素を追加する。this.Count++はしない。
    /// </summary>
    /// <param name="Item"></param>
    /// <returns>追加に成功すればtrue、失敗すればfalse。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal virtual bool InternalAdd(T Item) {
        var HashCode = typeof(T).IsNullable()?(uint)Item!.GetHashCode():Item is not null?(long)(uint)Item.GetHashCode():0;
        if(this.InternalAdd前半(out var 下限,out var 上限,out var TreeNode,HashCode)) {
            var Comparer = this.Comparer;
            LinkedNodeT LinkedNode = TreeNode;
            while(true) {
                var LinkedNodeItem = LinkedNode._LinkedNodeItem;
                if(LinkedNodeItem is null) {
                    this.AddRelationship(Item);
                    LinkedNode._LinkedNodeItem=new LinkedNodeItemT(Item);
                    return true;
                }
                if(Comparer.Equals(LinkedNodeItem.Item,Item)) {
                    return false;
                }
                LinkedNode=LinkedNodeItem;
            }
        }
        this.AddRelationship(Item);
        InternalAdd後半(下限,上限,TreeNode,HashCode,new LinkedNodeItemT(Item));
        return true;
    }
    /// <summary>既定の等値比較子を使用して、指定した要素が集合に含まれているかどうかを判断します。</summary>
    /// <returns>指定した値を持つ要素がソース 集合に含まれている場合は true。それ以外は false。</returns>
    /// <param name="item">集合内で検索する値。</param>
    internal bool InternalContains(T item) {
        Debug.Assert(item is not null);
        var TreeNode = this.InternalHashCodeに一致するTreeNodeを取得する((uint)item.GetHashCode());
        if(TreeNode is not null) {
            var Comparer = this.Comparer;
            for(var a = TreeNode._LinkedNodeItem;a is not null;a=a._LinkedNodeItem) {
                if(Comparer.Equals(a.Item,item)) {
                    return true;
                }
            }
        }
        return false;
    }
    //internal bool InternalContains(T Item){
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
    /// <summary>
    /// 全要素削除します。
    /// </summary>
    internal void InternalClear() {
        var TreeRoot = this.TreeRoot;
        TreeRoot.L=TreeRoot.R=null;
        TreeRoot._LinkedNodeItem=null;
        this._LongCount=0;
    }
    /// <summary>
    /// データを格納するコンテナの葉
    /// </summary>
    //[Serializable]
    public class LinkedNodeT {
        /// <summary>
        /// 同一HashCodeの値を管理する
        /// </summary>
        internal LinkedNodeItemT? _LinkedNodeItem;
        /// <summary>
        /// 同一HashCodeの値を管理する
        /// </summary>
        public LinkedNodeItemT? LinkedNodeItem => this._LinkedNodeItem;
        /// <summary>
        /// スーパーコンストラクタ
        /// </summary>
        /// <param name="LinkedNodeItem" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal LinkedNodeT(LinkedNodeItemT? LinkedNodeItem) => this._LinkedNodeItem=LinkedNodeItem;
    }
    /// <summary>
    /// データを格納するコンテナの葉
    /// </summary>
    //[Serializable, DebuggerDisplay("{Item.ToString()}")]
    public sealed class LinkedNodeItemT:LinkedNodeT {
        /// <summary>
        /// 格納している値
        /// </summary>
        public readonly T Item;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal LinkedNodeItemT(T Item,LinkedNodeItemT? LinkedNodeItem) : base(LinkedNodeItem) => this.Item=Item;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal LinkedNodeItemT(T Item) : base(null) => this.Item=Item;
    }
    /// <summary>
    /// データを格納するコンテナの先頭葉
    /// </summary>
    [Serializable/*, DebuggerDisplay("{this.ToString()}")*/]
    public sealed class TreeNodeT:LinkedNodeT {
        internal TreeNodeT? L, R, P;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal TreeNodeT(TreeNodeT? P) : base(null) => this.P=P;
        /// <summary>
        /// 次のノードを返す。
        /// </summary>
        /// <summary>
        /// 要素数。
        /// </summary>
        public long Count {
            get {
                var Count = 0L;
                for(var a = this._LinkedNodeItem;a is not null;a=a._LinkedNodeItem) {
                    Count++;
                }
                if(this.L is not null) {
                    Count+=this.L.Count;
                }
                if(this.R is not null) {
                    Count+=this.R.Count;
                }
                return Count;
            }
        }
        /// <summary>
        /// 部分木の衝突数。
        /// </summary>
        public (long TreeNode数, long LinkedNode数) 衝突数 => this.Private衝突数(this);
        private (long TreeNode数, long LinkedNode数) Private衝突数(TreeNodeT? TreeNode) {
            if(TreeNode is null)
                return (0, 0);
            var LinkedNode数 = 0L;
            var TreeNode数 = 1L;
            var TreeNode_LinkedNodeItem = TreeNode._LinkedNodeItem;
            for(var n = TreeNode_LinkedNodeItem;n is not null;n=n._LinkedNodeItem) {
                LinkedNode数++;
            }
            var 左 = this.Private衝突数(TreeNode.L);
            var 右 = this.Private衝突数(TreeNode.R);
            return (TreeNode数+左.TreeNode数+右.TreeNode数, LinkedNode数+左.LinkedNode数+右.LinkedNode数);
        }
        /// <summary>
        /// TreeNode数/衝突数
        /// </summary>
        public double 衝突率 {
            get {
                var (TreeNode数, LinkedNode数)=this.Private衝突数(this);
                if(TreeNode数==0)
                    return 0;
                return (double)LinkedNode数/TreeNode数;
            }
        }
        /// <summary>
        /// 文字列で表現する。
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            var sb = new StringBuilder();
            var TreeNode = this;
            for(var LinkedNodeItem = TreeNode._LinkedNodeItem;LinkedNodeItem is not null;LinkedNodeItem=LinkedNodeItem._LinkedNodeItem) {
                Debug.Assert(LinkedNodeItem.Item is not null);
                sb.Append(LinkedNodeItem.Item!.ToString());
                sb.Append(',');
            }
            sb.Length--;
            return sb.ToString();
        }
    }
    /// <summary>
    /// 列挙子。値型なのでメソッド呼び出しがCallなので早い。
    /// </summary>
    public struct Enumerator:Generic.IEnumerator<T> {
        //走査順はthis,L,R
        private LinkedNodeItemT? LinkedNodeItem;
        internal TreeNodeT TreeNode;
        public void Reset() => this.LinkedNodeItem=this.TreeNode._LinkedNodeItem;
        /// <summary>
        /// 要素が存在するかどうかの判定に使う
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() {
        LinkedNodeItem走査:
            if(this.LinkedNodeItem is not null) {
                this.InternalCurrent=this.LinkedNodeItem.Item;
                this.LinkedNodeItem=this.LinkedNodeItem.LinkedNodeItem;
                return true;
            }
            if(this.TreeNode.L is not null) {
                this.TreeNode=this.TreeNode.L;
                this.LinkedNodeItem=this.TreeNode.LinkedNodeItem;
                goto LinkedNodeItem走査;
            }
        右に移動:
            if(this.TreeNode.R is not null) {
                this.TreeNode=this.TreeNode.R;
                this.LinkedNodeItem=this.TreeNode.LinkedNodeItem;
                goto LinkedNodeItem走査;
            }
            //上に移動
            while(this.TreeNode.P is not null) {
                var P = this.TreeNode.P;
                if(P.L==this.TreeNode) {
                    //右上に移動
                    this.TreeNode=P;
                    goto 右に移動;
                }
                this.TreeNode=P;
            }
            return false;
        }
        public void Dispose() {
        }
        [NonSerialized]
        internal T InternalCurrent;
        public T Current {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.InternalCurrent;
        }
        object Collections.IEnumerator.Current {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#pragma warning disable CS8603 // Null 参照戻り値である可能性があります。
            get => this.InternalCurrent;
#pragma warning restore CS8603 // Null 参照戻り値である可能性があります。
        }
    }
    /// <summary>
    /// 性能のために値型の列挙子を表す
    /// </summary>
    protected internal Enumerator 変数Enumerator;
    /// <summary>
    /// 値の列挙子
    /// </summary>
    /// <returns>Enumerator</returns>
    public Enumerator GetEnumerator() {
        this.変数Enumerator.Reset();
        return this.変数Enumerator;
    }
    Generic.IEnumerator<T> Generic.IEnumerable<T>.GetEnumerator()=>this.GetEnumerator();
    /// <summary>
    /// EnumeratorをASetに返す。
    /// </summary>
    /// <returns></returns>
    private protected override Collections.IEnumerator ProtectedGetEnumerator() => this.GetEnumerator();
    ///// <summary>
    ///// ┌０┐
    ///// 　┌─０─┐
    ///// ┌０┐　┌１┐
    ///// 　　　┌───０───┐
    ///// 　┌─０─┐　　　┌─２─┐
    ///// ┌０┐　┌１┐　┌２┐　┌３┐
    ///// 例えば１が開始ノードだとしたらそこより上に行くときは右上なら出力する。左上なら終了。
    ///// スレッド数は２の倍数でないとうまくスケールしない。
    ///// </summary>
    ///// <param name="RangeEnumerator"></param>
    ///// <param name="スレッド番号"></param>
    ///// <param name="スレッド数"></param>
    //public void Init(ref RangeEnumerator RangeEnumerator,Int32 スレッド番号,Int32 スレッド数) {
    //    var TreeNode = this.TreeRoot;
    //    var 左スレッド番号 = 0;
    //    var スレッド範囲 = スレッド数;
    //    while(true) {
    //        var 中スレッド番号 = (左スレッド番号+スレッド範囲)/2;
    //        if(スレッド番号<中スレッド番号) {
    //            while(true) {
    //                if(TreeNode.L is not null) {
    //                    if(TreeNode.R is not null) {
    //                        TreeNode=TreeNode.L;
    //                    }
    //                }
    //            }
    //            スレッド範囲<<=1;
    //        } else if(中スレッド番号<スレッド番号) {
    //            TreeNode=TreeNode.R;
    //            スレッド範囲<<=1;
    //            左スレッド番号+=スレッド範囲;
    //        } else {
    //            RangeEnumerator.TreeNode=TreeNode;
    //            break;
    //        }
    //    }
    //}
    //public RangeEnumerator<T>[] GetEnumerators() {
    //    var ProcessorCount = Environment.ProcessorCount;
    //    ProcessorCount=2;
    //    var Enumerators = new RangeEnumerator<T>[ProcessorCount];
    //    var TreeRoot = this.TreeRoot;
    //    for(var a = 0;a<ProcessorCount;a++) {
    //        var RangeEnumerator = new RangeEnumerator<T>();
    //        RangeEnumerator.Init(a,ProcessorCount,TreeRoot);
    //        Enumerators[a]=RangeEnumerator;
    //    }
    //    return Enumerators;
    //}
    /// <summary>
    /// タプル同士の比較方法。
    /// </summary>
    private protected Generic.IEqualityComparer<T> Comparer{
        get=>Generic.EqualityComparer<T>.Default;
        //set{}
    }
    //[NonSerialized]
    //private protected readonly System.Collections.Generic.IEqualityComparer<T> Comparer;
    //[field:NonSerialized]
    //internal System.Collections.Generic.IEqualityComparer<T> Comparer {
    //    get;
    //    set;
    //}

    /// <summary>
    ///   <see cref="ImmutableSet{T}" /> クラスの新しいインスタンスを初期化します。初期化後のインスタンスの内容は空です。このセット型には、指定された等値比較子が使用されます。</summary>
    protected ImmutableSet():this(Generic.EqualityComparer<T>.Default){
    }
    /// <summary>
    ///   <see cref="ImmutableSet{T}" /> クラスの新しいインスタンスを初期化します。初期化後のインスタンスの内容は空です。このセット型には、指定された等値比較子が使用されます。</summary>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="Generic.IEqualityComparer{T}" /> の実装。</param>
    protected ImmutableSet(Generic.IEqualityComparer<T> Comparer) {
        this.変数Enumerator.TreeNode=new TreeNodeT(null);
        //this.Comparer=Comparer;
    }
    /// <summary>
    ///   <see cref="ImmutableSet{T}" /> クラスの新しいインスタンスを初期化します。このセット型には既定の等値比較子が使用されます。指定されたコレクションからコピーされた要素が格納され、コピー対象の要素数を格納できるだけの十分な容量が確保されます。
    /// </summary>
    /// <param name="source">新しいセットの要素のコピー元となるコレクション。</param>
    protected ImmutableSet(Generic.IEnumerable<T> source):this(source,Generic.EqualityComparer<T>.Default) {}
    /// <summary>
    ///   <see cref="ImmutableSet{T}" /> クラスの新しいインスタンスを初期化します。このセット型には既定の等値比較子が使用されます。指定されたコレクションからコピーされた要素が格納され、コピー対象の要素数を格納できるだけの十分な容量が確保されます。</summary>
    /// <param name="source">新しいセットの要素のコピー元となるコレクション。</param>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="Generic.IEqualityComparer{T}" /> の実装。</param>
    protected ImmutableSet(Generic.IEnumerable<T> source,Generic.IEqualityComparer<T> Comparer):this(Comparer) {
        this.PrivateProtectedImport(source);
    }
    /// <summary>
    ///   <see cref="ImmutableSet{T}" /> クラスの新しいインスタンスを初期化します。このセット型には既定の等値比較子が使用されます。指定されたコレクションからコピーされた要素が格納され、コピー対象の要素数を格納できるだけの十分な容量が確保されます。</summary>
    /// <param name="source">新しいセットの要素のコピー元となる配列。</param>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="Generic.IEqualityComparer{T}" /> の実装。</param>
    protected ImmutableSet(T[] source,Generic.IEqualityComparer<T> Comparer):this(Comparer){
        this.PrivateProtectedImport(source);
    }
    ///// <summary>
    ///// 並列にツリーをコピーする。
    ///// </summary>
    ///// <param name="source"></param>
    //private void PrivateImport並列(ImmutableSet<T> source) {
    //    PrivateImport並列(source.TreeRoot,this.変数Enumerator.TreeNode,0,Environment.ProcessorCount);
    //    this._Count=source.Count;
    //    void PrivateImport並列(TreeNode 旧TreeNode,TreeNode 新TreeNode,Int32 スレッド先頭,Int32 スレッド末尾) {
    //        Debug.Assert(スレッド先頭<スレッド末尾);
    //        var スレッド範囲 = スレッド末尾-スレッド先頭;
    //        if(スレッド範囲==1) {
    //            this.PrivateImport直列(旧TreeNode,新TreeNode);
    //        } else {
    //            //ノードのリーフを走査する。
    //            LinkedNodeItem走査:
    //            LinkedNode 新LinkedNodeItem = 新TreeNode;
    //            for(var 旧LinkedNodeItem = 旧TreeNode._LinkedNodeItem;旧LinkedNodeItem is not null;旧LinkedNodeItem=旧LinkedNodeItem._LinkedNodeItem) {
    //                var 旧LinkedNodeItem_Item = 旧LinkedNodeItem.Item;
    //                var 新LinkedNodeItem_LinkedNodeItem = new LinkedNodeItem(旧LinkedNodeItem_Item);
    //                新LinkedNodeItem._LinkedNodeItem=新LinkedNodeItem_LinkedNodeItem;
    //                新LinkedNodeItem=新LinkedNodeItem_LinkedNodeItem;
    //            }
    //            var 旧TreeNode_L = 旧TreeNode.L;
    //            var 旧TreeNode_R = 旧TreeNode.R;
    //            if(旧TreeNode_L is null) {
    //                if(旧TreeNode_R is null) {
    //                } else {
    //                    var 新TreeNode_R = new TreeNode(新TreeNode);
    //                    旧TreeNode=旧TreeNode_R;
    //                    新TreeNode.R=新TreeNode_R;
    //                    新TreeNode=新TreeNode_R;
    //                    goto LinkedNodeItem走査;
    //                }
    //            } else if(旧TreeNode_R is null) {
    //                var 新TreeNode_L = new TreeNode(新TreeNode);
    //                旧TreeNode=旧TreeNode_L;
    //                新TreeNode.L=新TreeNode_L;
    //                新TreeNode=新TreeNode_L;
    //                goto LinkedNodeItem走査;
    //            } else {
    //                Parallel.Invoke(
    //                    () => {
    //                        var 新TreeNode_L = new TreeNode(新TreeNode);
    //                        PrivateImport並列(旧TreeNode_L,新TreeNode_L,スレッド先頭,スレッド先頭+スレッド範囲/2);
    //                        新TreeNode.L=新TreeNode_L;
    //                    },
    //                    () => {
    //                        var 新TreeNode_R = new TreeNode(新TreeNode);
    //                        PrivateImport並列(旧TreeNode_R,新TreeNode_R,スレッド先頭+スレッド範囲/2,スレッド末尾);
    //                        新TreeNode.R=新TreeNode_R;
    //                    }
    //                );
    //            }
    //        }
    //    }
    //}
    private protected void PrivateProtectedImport(ImmutableSet<T> source) {
        var 元TreeNode = source.変数Enumerator.TreeNode;
        var 先TreeNode = this.変数Enumerator.TreeNode;
        var 元TreeRoot = 元TreeNode.P;
    LinkedNodeItem走査:
        LinkedNodeT 先LinkedNode = 先TreeNode;
        for(var 元LinkedNodeItem = 元TreeNode._LinkedNodeItem;元LinkedNodeItem is not null;元LinkedNodeItem=元LinkedNodeItem._LinkedNodeItem) {
            var 元LinkedNodeItem_Item = 元LinkedNodeItem.Item;
            var 先LinkedNodeItem_LinkedNodeItem = new LinkedNodeItemT(元LinkedNodeItem_Item);
            //Debug.Assert(先LinkedNode is not null);
            先LinkedNode._LinkedNodeItem=先LinkedNodeItem_LinkedNodeItem;
            先LinkedNode=先LinkedNodeItem_LinkedNodeItem;
        }
        if(元TreeNode.L is not null) {
            元TreeNode=元TreeNode.L;
            //Debug.Assert(先TreeNode != null);
            先TreeNode=先TreeNode.L=new TreeNodeT(先TreeNode);
            goto LinkedNodeItem走査;
        }
    右に移動:
        if(元TreeNode.R is not null) {
            元TreeNode=元TreeNode.R;
            //Debug.Assert(先TreeNode != null);
            先TreeNode=先TreeNode.R=new TreeNodeT(先TreeNode);
            goto LinkedNodeItem走査;
        }
        //上に移動
        // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
        while(元TreeNode.P!=元TreeRoot) {
            var 元TreeNode_P = 元TreeNode.P!;
            if(元TreeNode_P.L==元TreeNode) {
                元TreeNode=元TreeNode_P;
                Debug.Assert(先TreeNode is not null&&先TreeNode.P is not null);
                先TreeNode=先TreeNode.P!;
                goto 右に移動;
            }
            元TreeNode=元TreeNode_P;
            Debug.Assert(先TreeNode is not null&&先TreeNode.P is not null);
            先TreeNode=先TreeNode.P!;
        }
        this._LongCount=source._LongCount;
    }
    private protected void PrivateProtectedImport(T[] source) {
        for(var a = 0;a<source.Length;a++) {
            this.PrivateImport(source[a]);
        }
        this._LongCount=source.LongLength;
    }
    private protected void PrivateProtectedImport(Generic.IEnumerable<T> source) {
        var Count = 0L;
        foreach(var Item in source) {
            this.PrivateImport(Item);
            Count++;
        }
        this._LongCount=Count;
    }
    private void PrivateImport(T Item) {
        var HashCode = (long)(uint)Item!.GetHashCode();
        var TreeNode = this.TreeRoot;
        long 下限 = 初期下限, 上限 = 初期上限;
        //目的のノードを探索する。存在しなければ作る。
        while(true) {
            var CurrentHashCode = (下限+上限)>>1;
            Debug.Assert(TreeNode is not null);
            if(HashCode<CurrentHashCode) {
                上限=CurrentHashCode-1;
                AddTreeNode(ref TreeNode,ref TreeNode.L);
            } else if(HashCode>CurrentHashCode) {
                下限=CurrentHashCode+1;
                AddTreeNode(ref TreeNode,ref TreeNode.R);
            } else {
                var Comparer = this.Comparer;
                LinkedNodeT LinkedNode = TreeNode;
                while(true) {
                    var LinkedNode_LinkedNodeItem = LinkedNode._LinkedNodeItem;
                    if(LinkedNode_LinkedNodeItem is null) {
                        LinkedNode._LinkedNodeItem=new LinkedNodeItemT(Item);
                        return;
                    }
                    Debug.Assert(!Comparer.Equals(LinkedNode_LinkedNodeItem.Item,Item));
                    LinkedNode=LinkedNode_LinkedNodeItem;
                }
            }
        }
    }
    private static void AddTreeNode(ref TreeNodeT TreeNode,ref TreeNodeT? TreeNode_LR){
        TreeNode_LR??=new TreeNodeT(TreeNode);
        TreeNode=TreeNode_LR;
    }
    /// 
    /// <summary>
    /// データのみを読み込む。
    /// </summary>
    /// <param name="Reader"></param>
    public void Read(Stream Reader) {
        var TreeRoot = this.TreeRoot;
        TreeRoot.L=TreeRoot.R=null;
        TreeRoot._LinkedNodeItem=null;
        //var DebugCount = 0L;
        var Count = 0L;
        //var Array数 = 0;
        try {
            while(true) {
                this.InternalAdd(
                    Utf8Json.JsonSerializer.Deserialize<T>(Reader)
                );
                Count++;
            }
#pragma warning disable CA1031 // 一般的な例外の種類はキャッチしません
        } catch(Exception) {
#pragma warning restore CA1031 // 一般的な例外の種類はキャッチしません
            this._LongCount=Count;
        }
    }
    /// <summary>
    /// 要素を削除する。this.Count--はしない。
    /// </summary>
    /// <param name="Item"></param>
    /// <returns>削除に成功すればtrue、失敗すればfalse。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal virtual bool InternalRemove(T Item) {
        Debug.Assert(Item is not null);
        var TreeNode = this.InternalHashCodeに一致するTreeNodeを取得する((uint)Item.GetHashCode());
        if(TreeNode is not null){
            var Comparer=this.Comparer;
            LinkedNodeT LinkedNode =TreeNode;
            for(var a=TreeNode._LinkedNodeItem;a is not null;a=a._LinkedNodeItem){
                if(Comparer.Equals(a.Item,Item)){
                    this.RemoveRelationship(Item);
                    LinkedNode._LinkedNodeItem=a._LinkedNodeItem;
                    while(TreeNode._LinkedNodeItem is null&&TreeNode.P is not null&&TreeNode.L is null&&TreeNode.R is null){
                        var TreeNode_上=TreeNode.P;
                        Debug.Assert(TreeNode_上.L==TreeNode^TreeNode_上.R==TreeNode);
                        if(TreeNode_上.L==TreeNode){
                            TreeNode_上.L=null;
                        } else{
                            TreeNode_上.R=null;
                        }
                        TreeNode=TreeNode_上;
                    }
                    return true;
                }
                LinkedNode=a;
            }
        }
        return false;
    }
    /// <summary>
    /// ノード、リスト情報の取得
    /// </summary>
    /// <returns></returns>
    public (long Leaf数, int Node数, int 空Node数, int 有Node数) GetInformation() {
        var TreeNode = this.TreeRoot;
        var Leaf数 = 0L;
        var 空Node数 = 0;
        var 有Node数 = 0;
        while(true) {
            if(TreeNode._LinkedNodeItem is null) {
                空Node数++;
            } else {
                有Node数++;
                for(var a = TreeNode.LinkedNodeItem;a is not null;a=a._LinkedNodeItem) {
                    Leaf数++;
                }
            }
            if(TreeNode.L is not null) {
                TreeNode=TreeNode.L;
                continue;
            }
        右に移動:
            if(TreeNode.R is not null) {
                TreeNode=TreeNode.R;
                continue;
            }
            //上に移動
            while(TreeNode.P is not null) {
                var P = TreeNode.P;
                if(P.L==TreeNode) {
                    //右上に移動
                    TreeNode=P;
                    goto 右に移動;
                }
                TreeNode=P;
            }
            break;
        }
        Debug.Assert(空Node数+有Node数>=1);
        return (Leaf数, 空Node数+有Node数, 空Node数, 有Node数);
    }
    private static (long TreeNode数, long LinkedNode数) PrivateGetMemoryBytes(TreeNodeT? TreeNode) {
        if(TreeNode is null) {
            return (0, 0);
        }
        var (左TreeNode数, 左LinkedNode数)=PrivateGetMemoryBytes(TreeNode.L);
        var (右TreeNode数, 右LinkedNode数)=PrivateGetMemoryBytes(TreeNode.R);
        var LinkedNode数 = 0L;
        for(var LinkedNodeItem = TreeNode._LinkedNodeItem;LinkedNodeItem is not null;LinkedNodeItem=LinkedNodeItem._LinkedNodeItem) {
            LinkedNode数++;
        }
        return (左TreeNode数+右TreeNode数+1, 左LinkedNode数+右LinkedNode数+LinkedNode数);
    }
    /// <summary>
    /// このASetコレクションが管理している容量
    /// </summary>
    /// <returns></returns>
    public long GetMemoryBytes() {
        var (TreeNode数, LinkedNode数)=PrivateGetMemoryBytes(this.TreeRoot);
        return IntPtr.Size*3*TreeNode数+(IntPtr.Size+CommonLibrary.Sizeof(typeof(T)))*LinkedNode数;
    }
    /// <summary>
    /// 左,右のNodeとListがすべて存在しないNodeは存在してはいけない。
    /// </summary>
    [Conditional("DEBUG")]
    public void Assert() {
        Debug.Assert(this.TreeRoot is not null);
        const int 最大Level = 34;
        var TreeNode = this.TreeRoot;
        long 下限 = 初期下限, 上限 = 初期上限;
        var Count = 0L;
        var 存在してはいけないNode数 = 0;
        var Level = 0;
        while(true) {
            var CurrentHashCode = (下限+上限)>>1;
            if(TreeNode!.L is not null) {
                TreeNode=TreeNode.L;
                上限=CurrentHashCode-1;
                Level++;
                Debug.Assert(Level<最大Level);
                continue;
            }
        再検索:
            for(var LinkedNodeItem = TreeNode._LinkedNodeItem;LinkedNodeItem is not null;LinkedNodeItem=LinkedNodeItem._LinkedNodeItem) {
                Debug.Assert((uint)LinkedNodeItem.Item!.GetHashCode()==CurrentHashCode);
                Count++;
            }
            if(TreeNode.R is not null) {
                TreeNode=TreeNode.R;
                下限=CurrentHashCode+1;
                Level++;
                Debug.Assert(Level<最大Level);
                continue;
            }
            if(CurrentHashCode!=(下限+上限)>>1&&TreeNode.L is null&&TreeNode.R is null&&TreeNode._LinkedNodeItem is null) {
                存在してはいけないNode数++;
            }
            //左も右も行けなかったら上へ
            // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
            while(TreeNode.P is not null) {
                Level--;
                var TreeNode_上 = TreeNode.P;
                if(TreeNode_上.L==TreeNode) {
                    //右上へ
                    上限+=上限-下限+2;
                    TreeNode=TreeNode_上;
                    CurrentHashCode=(下限+上限)>>1;
                    goto 再検索;
                }
                //左上へ
                下限-=上限-下限+2;
                TreeNode=TreeNode_上;
                Debug.Assert(下限<=上限);
            }
            break;
        }
        Debug.Assert(Level==0);
        Debug.Assert(存在してはいけないNode数==0,"存在してはいけないNode数が"+存在してはいけないNode数);
        Debug.Assert(Count==this._LongCount);
    }
    /// <summary>
    ///   <see cref="ImmutableSet{T}" /> オブジェクトが、指定されたコレクションのサブセットであるかどうかを判断します。</summary>
    /// <returns>
    ///   <see cref="ImmutableSet{T}" /> オブジェクトが <paramref name="other" /> のサブセットである場合は true。それ以外の場合は false。</returns>
    /// <param name="other">現在の <see cref="ImmutableSet{T}" /> オブジェクトと比較するコレクション。</param>
    public bool IsSubsetOf(ImmutableSet<T> other) {
        foreach(var a in this) {
            if(!other.Contains(a)) {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    ///   <see cref="ImmutableSet{T}" /> オブジェクトが、指定されたコレクションの真のサブセット (真部分集合) であるかどうかを判断します。</summary>
    /// <returns>
    ///   <see cref="ImmutableSet{T}" /> オブジェクトが <paramref name="other" /> の真のサブセットである場合は true。それ以外の場合は false。</returns>
    /// <param name="other">現在の <see cref="ImmutableSet{T}" /> オブジェクトと比較するコレクション。</param>
    public bool IsProperSubsetOf(ImmutableSet<T> other) {
        if(this._LongCount==other._LongCount) {
            return false;
        }
        foreach(var a in this) {
            if(!other.Contains(a)) {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    ///   <see cref="ImmutableSet{T}" /> オブジェクトが、指定されたコレクションのスーパーセットであるかどうかを判断します。</summary>
    /// <returns>
    ///   <see cref="ImmutableSet{T}" /> オブジェクトが <paramref name="other" /> のスーパーセットである場合は true。それ以外の場合は false。</returns>
    /// <param name="other">現在の <see cref="ImmutableSet{T}" /> オブジェクトと比較するコレクション。</param>
    public bool IsSupersetOf(ImmutableSet<T> other) {
        foreach(var a in other) {
            if(!this.Contains(a)) {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    ///   <see cref="ImmutableSet{T}" /> オブジェクトが、指定されたコレクションの真のスーパーセット (真上位集合) であるかどうかを判断します。</summary>
    /// <returns>
    ///   <see cref="ImmutableSet{T}" /> オブジェクトが <paramref name="other" /> の真のスーパーセットである場合は true。それ以外の場合は false。</returns>
    /// <param name="other">現在の <see cref="ImmutableSet{T}" /> オブジェクトと比較するコレクション。</param>
    public bool IsProperSupersetOf(ImmutableSet<T> other) {
        if(this._LongCount==other._LongCount) {
            return false;
        }
        foreach(var a in other) {
            if(!this.Contains(a)) {
                return false;
            }
        }
        return true;
    }
    /// <summary>現在の <see cref="ImmutableSet{T}" /> オブジェクトと指定されたコレクションとが共通の要素を共有しているかどうかを判断します。</summary>
    /// <returns>
    ///   <see cref="ImmutableSet{T}" /> オブジェクトと <paramref name="other" /> との間に共通する要素が 1 つでも存在する場合は true。それ以外の場合は false。</returns>
    /// <param name="other">現在の <see cref="ImmutableSet{T}" /> オブジェクトと比較するコレクション。</param>
    public bool Overlaps(ImmutableSet<T> other) {
        foreach(var a in other) {
            if(this.Contains(a)) {
                return true;
            }
        }
        return false;
    }
    private protected bool PrivateProtectedSetEquals(IEnumerable<T> other){
        if(this.LongCount!=other.LongCount)return false;
        if(this.GetHashCode()!=other.GetHashCode())return false;
        foreach(var a in this)
            if(!other.Contains(a))return false;
        foreach(var a in other)
            if(!this.Contains(a))return false;
        return true;
    }
    /// <summary>
    /// 集合としての等価。
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool SetEquals(IEnumerable<T> other)=>ReferenceEquals(this,other)||this.PrivateProtectedSetEquals(other);
    public bool Equals(IEnumerable<T>? other)=>other is not null&&this.SetEquals(other);
    public override bool Equals(object? obj)=>this.Equals(obj as IEnumerable<T>);
    /// <summary>コレクションの集合としてのHashCode</summary>
    /// <returns>コレクションの集合としてのHashCode</returns>
    public override int GetHashCode(){
        var CRC = new HashCode();
        var TreeNode = this.TreeRoot;
LinkedNodeItem走査:
        var HashCode = 0;
        for(var LinkedNodeItem = TreeNode._LinkedNodeItem;LinkedNodeItem is not null;LinkedNodeItem=LinkedNodeItem._LinkedNodeItem) {
            Debug.Assert(LinkedNodeItem.Item is not null);
            CRC.Add(LinkedNodeItem.Item);
        }
        CRC.Add(HashCode);
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
        while(!(TreeNode.P is null)) {
            var 旧TreeNode_P = TreeNode.P;
            if(旧TreeNode_P.L==TreeNode) {
                TreeNode=旧TreeNode_P;
                goto 右に移動;
            }
            TreeNode=旧TreeNode_P;
        }
        return CRC.ToHashCode();
    }
    /// <summary>
    /// 配列に変換する。
    /// </summary>
    /// <returns></returns>
    public T[] ToArray() {
        var array = new T[this._LongCount];
        var ArrayIndex = 0L;
        foreach(var a in this) {
            array[ArrayIndex++]=a;
        }
        return array;
    }
    /// <summary>
    /// 濃度0の集合。
    /// </summary>
    public static readonly IEnumerable<T> EmptySet = new Set<T>();
    /// <summary>
    /// <see cref="ImmutableSet{T}" />の文字列表現
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $"{nameof(this.LongCount)}:{this.LongCount}";



    /// <summary>
    /// ILで<see cref="ImmutableSet{T}" />,<see cref="Generic.IEnumerable{T}" />を２回走査するときの２回目の走査に使う、F(irst)I(n)L(ast)O(ut)。
    /// </summary>
    internal struct FILO:Generic.IEnumerator<T> {
        private LinkedNodeItemT? LinkedListNode;
        /// <summary>
        /// 要素数
        /// </summary>
        internal long Count;
        public T Current{get;private set;}
#pragma warning disable CS8603 // Null 参照戻り値である可能性があります。
        object Collections.IEnumerator.Current => this.Current;
#pragma warning restore CS8603 // Null 参照戻り値である可能性があります。
        /// <summary>
        /// structは引数なしコンストラクタの代わり
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Constructor() {
            this.LinkedListNode=null;
            this.Count=0;
        }
        /// <summary>
        /// 要素を追加
        /// </summary>
        /// <param name="Item"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T Item) {
            this.LinkedListNode=new LinkedNodeItemT(Item,this.LinkedListNode);
            this.Count++;
        }
        /// <summary>
        /// IEnumerator.MoveNextと同じ
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal bool MoveNext() {
            var LinkedListNode = this.LinkedListNode;
            if(LinkedListNode is null)
                return false;
            this.Current=LinkedListNode.Item;
            this.LinkedListNode=LinkedListNode._LinkedNodeItem;
            return true;
        }
        public void Dispose() {
        }
        bool Collections.IEnumerator.MoveNext() => this.MoveNext();
        public void Reset() {
        }
    }
    /// <summary>
    /// ILで<see cref="ImmutableSet{T}" />,<see cref="Generic.IEnumerable{T}" />を２回走査するときの２回目の走査に使う、F(irst)I(n)F(irst)O(ut)。
    /// </summary>
    internal struct FIFO:Generic.IEnumerator<T>{
        private LinkedNodeT FirstNode;
        private LinkedNodeT LastNode;
        private LinkedNodeT CurrentNode;
        public T Current {
            get; private set;
        }
#pragma warning disable CS8603 // Null 参照戻り値である可能性があります。
        object Collections.IEnumerator.Current => this.Current;
#pragma warning restore CS8603 // Null 参照戻り値である可能性があります。
        /// <summary>
        /// structは引数なしコンストラクタの代わり
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Constructor() => this.LastNode=this.FirstNode=new LinkedNodeT(null);
        /// <summary>
        /// 要素を追加
        /// </summary>
        /// <param name="Item"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T Item) {
            var Last = new LinkedNodeItemT(Item);
            this.LastNode._LinkedNodeItem=Last;
            this.LastNode=Last;
        }
        /// <summary>
        /// リセットしてMoveNextできるようにする。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset() => this.CurrentNode=this.FirstNode;
        /// <summary>
        /// IEnumerator.MoveNextと同じ
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal bool MoveNext() {
            var LinkedListNode = this.CurrentNode._LinkedNodeItem;
            if(LinkedListNode is null)
                return false;
            this.CurrentNode=LinkedListNode;
            this.Current=LinkedListNode.Item;
            return true;
        }
        public void Dispose() {
        }
        bool Collections.IEnumerator.MoveNext() => this.MoveNext();
        public bool SequenceEqual(FIFO other) {
            var EqualityComparer=Generic.EqualityComparer<T>.Default;
            var a =this.FirstNode._LinkedNodeItem;
            var b = other.FirstNode._LinkedNodeItem;
            while(a is not null&&b is not null) {
                if(!EqualityComparer.Equals(a.Item,b.Item)) {
                    return false;
                }
                a=a._LinkedNodeItem;
                b=b._LinkedNodeItem;
            }
            return a is null&&b is null;
        }
    }
}