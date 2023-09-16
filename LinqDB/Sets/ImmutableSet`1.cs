#define 並列化
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;
using LinqDB.Helpers;
using System.IO;
using Utf8Json;
// ReSharper disable LoopCanBeConvertedToQuery
// ReSharper disable ArrangeStaticMemberQualifier
namespace LinqDB.Sets;

/// <summary>
/// 関係集合。
/// </summary>
/// <typeparam name="T"></typeparam>
[Serializable]
[DebuggerDisplay("Count = {"+nameof(Count)+"}")]
[DebuggerTypeProxy(typeof(SetDebugView<>))]
public abstract class ImmutableSet<T>:ImmutableSet, IOutputSet<T>, IEquatable<ImmutableSet<T>>,ISerializable{
    /// <summary>
    /// 要素のルート
    /// </summary>
    internal TreeNode TreeRoot => this.変数Enumerator.TreeNode;
    [IgnoreDataMember]
    public (long TreeNode数, long LinkedNode数) 衝突数 => this.変数Enumerator.TreeNode.衝突数;
    /// <summary>
    /// タプルを追加した時に参照関係を作る。
    /// </summary>
    /// <param name="Item"></param>
    internal virtual void AddRelationship(T Item) { }
    /// <summary>
    /// タプルを削除した時に参照関係を作る。
    /// </summary>
    /// <param name="Item"></param>
    internal virtual void RemoveRelationship(T Item) { }
    /// <summary>
    /// HashCodeによるTreeNode探索
    /// </summary>
    /// <param name="HashCode"></param>
    /// <returns></returns>
    internal TreeNode? InternalHashCodeに一致するTreeNodeを取得する(long HashCode) {
        Debug.Assert(this.TreeRoot is not null);
        TreeNode? TreeNode = this.TreeRoot;
        long 下限 = 初期下限, 上限 = 初期上限;
        do {
            var CurrentHashCode = (下限+上限)>>1;
            Debug.Assert(TreeNode is not null);
            if(HashCode<CurrentHashCode) {
                上限=CurrentHashCode-1L;
                TreeNode=TreeNode!.L;
            } else if(HashCode>CurrentHashCode) {
                下限=CurrentHashCode+1L;
                TreeNode=TreeNode!.R;
            } else {
                return TreeNode;
            }
        } while(TreeNode is not null);
        return null;
    }
    private readonly Random Random = new(1);
    private List<T> List = new();
    [IgnoreDataMember]
    public T Sampling {
        get{
            if(this.Count==0) throw new NotSupportedException();
            var 試行回数=0;
            while(true) {
                var LinkedNodeItem = this.GetSampling(this.TreeRoot,31);
                試行回数++;
                if(LinkedNodeItem is null) continue;
                return LinkedNodeItem.Item;
            }
        }
    }
    private LinkedNodeItem? GetSampling(TreeNode TreeNode,int Level) {
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
        LinkedNodeItem? 共通() {
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
    private void PrivateSampling() {
        var List = this.List;
        List.Clear();
        do {
            var Level = 0;
            var Random = this.Random;
            TreeNode? TreeNode = this.TreeRoot;
            while(TreeNode is not null) {
                for(var a = TreeNode.LinkedNodeItem;a is not null;a=a.LinkedNodeItem)
                    List.Add(a.Item);
                TreeNode=Random.Next(2)==0
                    ?TreeNode.L??TreeNode.R
                    :TreeNode.R??TreeNode.L;
                Level++;
            }
        } while(List.Count==0);
    }
    //public T Sampling{
    //    get {
    //        this.PrivateSampling();
    //        var List = this.List;
    //        if(List.Count==0) return default!;
    //        this.PrivateSampling();
    //        var List_Count =List.Count;
    //        var Random = this.Random;
    //        var index = Random.Next(List_Count);
    //        return List[index];
    //    }
    //}
    [IgnoreDataMember]
    public T SamplingNullable=> this.Count==0 ? default! : this.Sampling;
    /// <summary>
    /// Add時の前半処理。目的のノードを探索する。存在しなければ作る。
    /// </summary>
    /// <param name="下限"></param>
    /// <param name="上限"></param>
    /// <param name="out_TreeNode"></param>
    /// <param name="HashCode"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool InternalAdd前半(out long 下限,out long 上限,out TreeNode out_TreeNode,long HashCode) {
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
                    out_TreeNode=TreeNode.L=new TreeNode(TreeNode);
                    return false;
                }
                TreeNode=TreeNode.L;
            } else if(HashCode>CurrentHashCode) {
                下限1=CurrentHashCode+1;
                if(TreeNode!.R is null) {
                    下限=下限1;
                    上限=上限1;
                    out_TreeNode=TreeNode.R=new TreeNode(TreeNode);
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
    /// <param name="LinkedNodeItem"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void InternalAdd後半(long 下限,long 上限,TreeNode TreeNode,long HashCode,LinkedNodeItem LinkedNodeItem) {
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
                TreeNode=TreeNode.L=new TreeNode(TreeNode);
            } else if(HashCode>CurrentHashCode) {
                下限=CurrentHashCode+1;
                Debug.Assert(TreeNode.R is null);
                TreeNode=TreeNode.R=new TreeNode(TreeNode);
            } else {
                Debug.Assert(TreeNode._LinkedNodeItem is null);
                TreeNode._LinkedNodeItem=LinkedNodeItem;
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
        if(Item is null) {
            Debug.Assert(this._Count==0);
            this.変数Enumerator.TreeNode._LinkedNodeItem=new LinkedNodeItem(default!);
        } else {
            var HashCode = (long)(uint)Item.GetHashCode();
            if(this.InternalAdd前半(out var 下限,out var 上限,out var TreeNode,HashCode)) {
                var Comparer = this.Comparer;
                LinkedNode LinkedNode = TreeNode;
                while(true) {
                    var LinkedNodeItem = LinkedNode._LinkedNodeItem;
                    if(LinkedNodeItem is null) {
                        this.AddRelationship(Item);
                        LinkedNode._LinkedNodeItem=new LinkedNodeItem(Item);
                        return true;
                    }
                    if(Comparer.Equals(LinkedNodeItem.Item,Item)) {
                        return false;
                    }
                    LinkedNode=LinkedNodeItem;
                }
            }
            this.AddRelationship(Item);
            InternalAdd後半(下限,上限,TreeNode,HashCode,new LinkedNodeItem(Item));
        }
        return true;
    }
    /// <summary>既定の等値比較子を使用して、指定した要素が集合に含まれているかどうかを判断します。</summary>
    /// <returns>指定した値を持つ要素がソース 集合に含まれている場合は true。それ以外は false。</returns>
    /// <param name="Value">集合内で検索する値。</param>
    internal bool InternalContains(T Value) {
        Debug.Assert(Value is not null);
        var TreeNode = this.InternalHashCodeに一致するTreeNodeを取得する((uint)Value.GetHashCode());
        if(TreeNode is not null) {
            var Comparer = this.Comparer;
            for(var a = TreeNode._LinkedNodeItem;a is not null;a=a._LinkedNodeItem) {
                if(Comparer.Equals(a.Item,Value)) {
                    return true;
                }
            }
        }
        return false;
    }
    /// <summary>
    /// データを格納するコンテナの葉
    /// </summary>
    [Serializable]
    public class LinkedNode {
        /// <summary>
        /// 同一HashCodeの値を管理する
        /// </summary>
        internal LinkedNodeItem? _LinkedNodeItem;
        /// <summary>
        /// 同一HashCodeの値を管理する
        /// </summary>
        public LinkedNodeItem? LinkedNodeItem => this._LinkedNodeItem;
        /// <summary>
        /// スーパーコンストラクタ
        /// </summary>
        /// <param name="LinkedNodeItem" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal LinkedNode(LinkedNodeItem? LinkedNodeItem) => this._LinkedNodeItem=LinkedNodeItem;
    }
    /// <summary>
    /// データを格納するコンテナの葉
    /// </summary>
    [Serializable, DebuggerDisplay("{Item.ToString()}")]
    public sealed class LinkedNodeItem:LinkedNode {
        /// <summary>
        /// 格納している値
        /// </summary>
        public readonly T Item;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal LinkedNodeItem(T Item,LinkedNodeItem? LinkedNodeItem) : base(LinkedNodeItem) => this.Item=Item;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal LinkedNodeItem(T Item) : base(null) => this.Item=Item;
    }
    /// <summary>
    /// データを格納するコンテナの先頭葉
    /// </summary>
    [Serializable/*, DebuggerDisplay("{this.ToString()}")*/]
    public sealed class TreeNode:LinkedNode {
        internal TreeNode? L, R, P;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal TreeNode(TreeNode? P) : base(null) => this.P=P;
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
        private (long TreeNode数, long LinkedNode数) Private衝突数(TreeNode? TreeNode) {
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
    public struct Enumerator:IEnumerator<T> {
        //走査順はthis,L,R
        internal LinkedNodeItem? LinkedNodeItem;
        internal TreeNode TreeNode;
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
    /// バッファ経由でバッファへの書き込みは別スレッドで行う。列挙子。値型なのでメソッド呼び出しがCallなので早い。
    /// </summary>
    public class Write1Read1Enumerator:IEnumerator<T> {
        internal LinkedNodeItem? LinkedNodeItem;
        internal TreeNode TreeNode;
        private bool 終了;
        private readonly BlockingCollection<T> BlockingCollection = new(10);
        //private readonly CancellationToken CancellationToken=new CancellationToken();
        public Write1Read1Enumerator(Enumerator Enumerator) {
            this.TreeNode=Enumerator.TreeNode;
            this.LinkedNodeItem=Enumerator.LinkedNodeItem;
            this.終了=false;
            this.InternalCurrent=default!;
            //バッファに書き込む
            ThreadPool.QueueUserWorkItem(_ => {
                var BlockingCollection=this.BlockingCollection;
                //var CancellationToken = this.CancellationToken;
                var TreeNode=this.TreeNode;
                var LinkedNodeItem=this.LinkedNodeItem;
            LinkedNodeItem走査:
                if(LinkedNodeItem is not null) {
                    BlockingCollection.Add(LinkedNodeItem.Item);
                    LinkedNodeItem=LinkedNodeItem.LinkedNodeItem;
                    goto LinkedNodeItem走査;
                }
                if(TreeNode.L is not null) {
                    TreeNode=TreeNode.L;
                    LinkedNodeItem=TreeNode.LinkedNodeItem;
                    goto LinkedNodeItem走査;
                }
            右に移動:
                if(TreeNode.R is not null) {
                    TreeNode=TreeNode.R;
                    LinkedNodeItem=TreeNode.LinkedNodeItem;
                    goto LinkedNodeItem走査;
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
                this.終了=true;
            },this);
        }
        public void Reset() {
            var BlockingCollection=this.BlockingCollection;
            while(BlockingCollection.TryTake(out var _)) { }
        }
        /// <summary>
        /// 要素が存在するかどうかの判定に使う
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() {
            try {
                while(true) {
                    if(this.BlockingCollection.TryTake(out this.InternalCurrent!)) {
                        return true;
                    } else if(this.終了) {
                        return false;
                    }
                }
            } catch(OperationCanceledException) {
                return false;
            }
        }
#pragma warning disable CA1816 // Dispose メソッドは、SuppressFinalize を呼び出す必要があります
        public void Dispose() {
#pragma warning restore CA1816 // Dispose メソッドは、SuppressFinalize を呼び出す必要があります
            this.BlockingCollection.Dispose();
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
    public struct Enumerator1{
        internal LinkedNodeItem? LinkedNodeItem;
        internal TreeNode TreeNode;
        public Enumerator1(Enumerator Enumerator) {
            this.TreeNode=Enumerator.TreeNode;
            this.LinkedNodeItem=Enumerator.LinkedNodeItem;
            this.InternalCurrent=default!;
        }
        public void Reset() => this.LinkedNodeItem=this.TreeNode._LinkedNodeItem;
        /// <summary>
        /// 要素が存在するかどうかの判定に使う
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() {
            ////Enumeratorの速度2593ms
            ////Enumerator1の速度2807ms
            //LinkedNodeItem走査:
            //if(this.LinkedNodeItem is not null) {
            //    var LinkedNodeItem = this.LinkedNodeItem;
            //    this.InternalCurrent=LinkedNodeItem.Item;
            //    this.LinkedNodeItem=LinkedNodeItem.LinkedNodeItem;
            //    return true;
            //}
            //if(this.TreeNode.L is not null) {
            //    var TreeNode_L=this.TreeNode.L;
            //    this.TreeNode=TreeNode_L;
            //    this.LinkedNodeItem=TreeNode_L.LinkedNodeItem;
            //    goto LinkedNodeItem走査;
            //}
            //右に移動:
            //if(this.TreeNode.R is not null) {
            //    var TreeNode_R=this.TreeNode.R;
            //    this.TreeNode=TreeNode_R;
            //    this.LinkedNodeItem=TreeNode_R.LinkedNodeItem;
            //    goto LinkedNodeItem走査;
            //}
            ////上に移動
            //var TreeNode = this.TreeNode;
            //while(TreeNode.P is not null) {
            //    var P = TreeNode.P;
            //    if(P.L==TreeNode) {
            //        //右上に移動
            //        this.TreeNode=P;
            //        goto 右に移動;
            //    }
            //    TreeNode=P;
            //}
            //this.TreeNode=TreeNode;

            ////Enumeratorの速度2614ms
            ////Enumerator1の速度2729ms
            //var LinkedNodeItem = this.LinkedNodeItem;
            //LinkedNodeItem走査:
            //if(LinkedNodeItem is not null) {
            //    this.InternalCurrent=LinkedNodeItem.Item;
            //    this.LinkedNodeItem=LinkedNodeItem.LinkedNodeItem;
            //    return true;
            //}
            //if(this.TreeNode.L is not null) {
            //    var TreeNode_L = this.TreeNode.L;
            //    this.TreeNode=TreeNode_L;
            //    LinkedNodeItem=TreeNode_L.LinkedNodeItem;
            //    goto LinkedNodeItem走査;
            //}
            //右に移動:
            //if(this.TreeNode.R is not null) {
            //    var TreeNode_R = this.TreeNode.R;
            //    this.TreeNode=TreeNode_R;
            //    LinkedNodeItem=TreeNode_R.LinkedNodeItem;
            //    goto LinkedNodeItem走査;
            //}
            ////上に移動
            //var TreeNode = this.TreeNode;
            //while(TreeNode.P is not null) {
            //    var P = TreeNode.P;
            //    if(P.L==TreeNode) {
            //        //右上に移動
            //        this.TreeNode=P;
            //        goto 右に移動;
            //    }
            //    TreeNode=P;
            //}
            //this.TreeNode=TreeNode;

            var LinkedNodeItem = this.LinkedNodeItem;
        LinkedNodeItem走査:
            if(LinkedNodeItem is not null) {
                this.InternalCurrent=LinkedNodeItem.Item;
                this.LinkedNodeItem=LinkedNodeItem.LinkedNodeItem;
                return true;
            }
            var TreeNode = this.TreeNode;
            if(TreeNode.L is not null) {
                TreeNode=TreeNode.L;
                this.TreeNode=TreeNode;
                LinkedNodeItem=TreeNode.LinkedNodeItem;
                goto LinkedNodeItem走査;
            }
        右に移動:
            if(TreeNode.R is not null) {
                TreeNode=TreeNode.R;
                this.TreeNode=TreeNode;
                LinkedNodeItem=TreeNode.LinkedNodeItem;
                goto LinkedNodeItem走査;
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
            this.TreeNode=TreeNode;
            return false;
        }
        internal T InternalCurrent;
        public T Current {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.InternalCurrent;
        }
    }
    /// <summary>
    /// 部分木により分割された範囲が指定された列挙子。値型なのでメソッド呼び出しがCallなので早い。
    /// </summary>
    public struct RangeEnumerator:IEnumerator<T> {
        //走査順はthis,L,R
        private LinkedNodeItem? LinkedNodeItem;
        private TreeNode RootNode;
        internal TreeNode TreeNode;
        internal TreeNode 番兵Node;
        public void ctor(TreeNode 番兵Node,TreeNode TreeNode) {
            this.番兵Node=番兵Node;
            this.TreeNode=TreeNode;
        }
        /// <summary>
        /// Ⅹ(ダミー)───────────────────────────┐
        /// 　　　　　　　　　　　　　　　　┌───────────────０(Root)
        /// 　　　　　　　　┌───────０───────┐
        /// 　　　　┌───０───┐　　　　　　　┌───４───┐
        /// 　　┌─０─┐　　　┌─２─┐　　　┌─４─┐　　　┌─６─┐
        /// 　┌０┐　┌１┐　┌２┐　┌３┐　┌４┐　┌５┐　┌６┐　┌７┐この段からスレッドスタート
        /// 例えば１が開始ノードだとしたらそこより上に行くときは右上なら出力する。左上なら終了。
        /// スレッド数は２の倍数でないとうまくスケールしない。
        /// </summary>
        /// <param name="スレッド番号"></param>
        /// <param name="スレッド数"></param>
        /// <param name="TreeNode"></param>
        public void Init(int スレッド番号,int スレッド数,TreeNode TreeNode) {
            var 左スレッド番号 = 0;
            var スレッド範囲 = スレッド数;
            while(true) {
                var 中スレッド番号 = (左スレッド番号+スレッド範囲)/2;
                if(スレッド番号<中スレッド番号) {
                    TreeNode=TreeNode!.L!;
                    スレッド範囲<<=1;
                } else if(中スレッド番号<スレッド番号) {
                    TreeNode=TreeNode!.R!;
                    スレッド範囲<<=1;
                    左スレッド番号+=スレッド範囲;
                } else {
                    this.TreeNode=this.RootNode=TreeNode;
                    break;
                }
            }
        }
        public void Reset() {
            this.LinkedNodeItem=this.TreeNode._LinkedNodeItem;
        }
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
            while(this.TreeNode.P!=this.番兵Node) {
                var P = this.TreeNode.P!;
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
        internal T InternalCurrent;
        public T Current {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.InternalCurrent;
        }
        object IEnumerator.Current {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.InternalCurrent!;
        }
    }
    /// <summary>
    /// 性能のために値型の列挙子を表す
    /// </summary>
    [NonSerialized]
    protected internal Enumerator 変数Enumerator;
    /// <summary>
    /// 値の列挙子
    /// </summary>
    /// <returns>Enumerator</returns>
    public Enumerator GetEnumerator() {
        this.変数Enumerator.Reset();
        return this.変数Enumerator;
    }
    public Write1Read1Enumerator GetWrite1Read1Enumerator() {
        this.変数Enumerator.Reset();
        return new Write1Read1Enumerator(this.変数Enumerator);
    }
    public Enumerator1 GetEnumerator1() {
        this.変数Enumerator.Reset();
        return new Enumerator1(this.変数Enumerator);
    }
    IEnumerator<T> IEnumerable<T>.GetEnumerator() =>this.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    /// <summary>
    /// EnumeratorをASetに返す。
    /// </summary>
    /// <returns></returns>
    private protected override IEnumerator ProtectedGetEnumerator() => this.GetEnumerator();
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
    internal IEqualityComparer<T> Comparer {
        get;
        set;
    }
    /// <summary>
    ///   <see cref="ImmutableSet{T}" /> クラスの新しいインスタンスを初期化します。初期化後のインスタンスの内容は空です。このセット型には、指定された等値比較子が使用されます。</summary>
    protected ImmutableSet():this(EqualityComparer<T>.Default){
    }
    /// <summary>
    ///   <see cref="ImmutableSet{T}" /> クラスの新しいインスタンスを初期化します。初期化後のインスタンスの内容は空です。このセット型には、指定された等値比較子が使用されます。</summary>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="IEqualityComparer{T}" /> の実装。</param>
    protected ImmutableSet(IEqualityComparer<T> Comparer) {
        this.変数Enumerator.TreeNode=new TreeNode(null);
        this.Comparer=Comparer;
    }
    /// <summary>
    ///   <see cref="ImmutableSet{T}" /> クラスの新しいインスタンスを初期化します。このセット型には既定の等値比較子が使用されます。指定されたコレクションからコピーされた要素が格納され、コピー対象の要素数を格納できるだけの十分な容量が確保されます。
    /// </summary>
    /// <param name="source">新しいセットの要素のコピー元となるコレクション。</param>
    protected ImmutableSet(IEnumerable<T> source):this(source,EqualityComparer<T>.Default) {}
    /// <summary>
    ///   <see cref="ImmutableSet{T}" /> クラスの新しいインスタンスを初期化します。このセット型には既定の等値比較子が使用されます。指定されたコレクションからコピーされた要素が格納され、コピー対象の要素数を格納できるだけの十分な容量が確保されます。</summary>
    /// <param name="source">新しいセットの要素のコピー元となるコレクション。</param>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="IEqualityComparer{T}" /> の実装。</param>
    protected ImmutableSet(IEnumerable<T> source,IEqualityComparer<T> Comparer):this(Comparer) {
        this.PrivateProtectedImport(source);
    }
    /// <summary>
    ///   <see cref="ImmutableSet{T}" /> クラスの新しいインスタンスを初期化します。このセット型には既定の等値比較子が使用されます。指定されたコレクションからコピーされた要素が格納され、コピー対象の要素数を格納できるだけの十分な容量が確保されます。</summary>
    /// <param name="source">新しいセットの要素のコピー元となる配列。</param>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="IEqualityComparer{T}" /> の実装。</param>
    protected ImmutableSet(T[] source,IEqualityComparer<T> Comparer):this(Comparer){
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
        var 元TreeRoot = 元TreeNode!.P;
    LinkedNodeItem走査:
        LinkedNode 先LinkedNode = 先TreeNode;
        for(var 元LinkedNodeItem = 元TreeNode._LinkedNodeItem;元LinkedNodeItem is not null;元LinkedNodeItem=元LinkedNodeItem._LinkedNodeItem) {
            var 元LinkedNodeItem_Item = 元LinkedNodeItem.Item;
            var 先LinkedNodeItem_LinkedNodeItem = new LinkedNodeItem(元LinkedNodeItem_Item);
            //Debug.Assert(先LinkedNode is not null);
            先LinkedNode._LinkedNodeItem=先LinkedNodeItem_LinkedNodeItem;
            先LinkedNode=先LinkedNodeItem_LinkedNodeItem;
        }
        if(元TreeNode.L is not null) {
            元TreeNode=元TreeNode.L;
            //Debug.Assert(先TreeNode != null);
            先TreeNode=先TreeNode.L=new TreeNode(先TreeNode);
            goto LinkedNodeItem走査;
        }
    右に移動:
        if(元TreeNode.R is not null) {
            元TreeNode=元TreeNode.R;
            //Debug.Assert(先TreeNode != null);
            先TreeNode=先TreeNode!.R=new TreeNode(先TreeNode);
            goto LinkedNodeItem走査;
        }
        //上に移動
        // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
        while(元TreeNode.P!=元TreeRoot) {
            var 元TreeNode_P = 元TreeNode.P!;
            if(元TreeNode_P.L==元TreeNode) {
                元TreeNode=元TreeNode_P;
                Debug.Assert(先TreeNode is not null&&先TreeNode.P is not null);
                先TreeNode=先TreeNode!.P!;
                goto 右に移動;
            }
            元TreeNode=元TreeNode_P;
            Debug.Assert(先TreeNode is not null&&先TreeNode.P is not null);
            先TreeNode=先TreeNode!.P!;
        }
        this._Count=source._Count;
    }
    private protected void PrivateProtectedImport(T[] source) {
        for(var a = 0;a<source.Length;a++) {
            this.PrivateImport(source[a]);
        }
        this._Count=source.LongLength;
    }
    private protected void PrivateProtectedImport(IEnumerable<T> source) {
        var Count = 0L;
        foreach(var Item in source) {
            this.PrivateImport(Item);
            Count++;
        }
        this._Count=Count;
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
                AddTreeNode(ref TreeNode!,ref TreeNode!.L);
            } else if(HashCode>CurrentHashCode) {
                下限=CurrentHashCode+1;
                AddTreeNode(ref TreeNode!,ref TreeNode!.R);
            } else {
                var Comparer = this.Comparer;
                LinkedNode LinkedNode = TreeNode!;
                while(true) {
                    var LinkedNode_LinkedNodeItem = LinkedNode._LinkedNodeItem;
                    if(LinkedNode_LinkedNodeItem is null) {
                        LinkedNode._LinkedNodeItem=new LinkedNodeItem(Item);
                        return;
                    }
                    Debug.Assert(!Comparer.Equals(LinkedNode_LinkedNodeItem.Item,Item));
                    LinkedNode=LinkedNode_LinkedNodeItem!;
                }
            }
        }
    }
    private static void AddTreeNode(ref TreeNode TreeNode,ref TreeNode? TreeNode_LR){
        TreeNode_LR??=new TreeNode(TreeNode);
        TreeNode=TreeNode_LR;
    }
    ///// <summary>
    ///// sourceを走査して全て並列で追加する。
    ///// </summary>
    ///// <param name="Array">新しいセットの要素のコピー元となる配列。</param>
    //private protected void ProtectedImport並列(T[] Array) {
    //    if(Array.Length==0) {
    //        return;
    //    }
    //    var DebugCount = 0L;
    //    Parallel.ForEach(
    //        Partitioner.Create(0,Array.Length),
    //        (StartEnd) => {
    //            var Array0 = Array;
    //            var TreeRoot = this.TreeRoot;
    //            Interlocked.Add(ref DebugCount,StartEnd.Item2-StartEnd.Item1);
    //            for(var i = StartEnd.Item1;i<StartEnd.Item2;i++) {
    //                var Item = Array0[i]!;
    //                var HashCode = (Int64)(UInt32)Item.GetHashCode();
    //                var TreeNode = TreeRoot;
    //                Int64 下限 = 初期下限, 上限 = 初期上限;
    //                //目的のノードを探索する。存在しなければ作る。
    //                while(true) {
    //                    var CurrentHashCode = (下限+上限)>>1;
    //                    Debug.Assert(TreeNode is not null);
    //                    if(HashCode<CurrentHashCode) {
    //                        上限=CurrentHashCode-1;
    //                        ConcurrentAddTreeNode(ref TreeNode!,ref TreeNode!.L);
    //                    } else if(HashCode>CurrentHashCode) {
    //                        下限=CurrentHashCode+1;
    //                        ConcurrentAddTreeNode(ref TreeNode!,ref TreeNode!.R);
    //                    } else {
    //                        var Comparer = this.Comparer;
    //                        lock(TreeNode!) {
    //                            LinkedNode LinkedNode = TreeNode!;
    //                            while(true) {
    //                                var LinkedNode_LinkedNodeItem = LinkedNode._LinkedNodeItem;
    //                                if(LinkedNode_LinkedNodeItem is null) {
    //                                    LinkedNode._LinkedNodeItem=new LinkedNodeItem(Item);
    //                                    goto 終了;
    //                                }
    //                                Debug.Assert(!Comparer.Equals(LinkedNode_LinkedNodeItem.Item,Item));
    //                                LinkedNode=LinkedNode_LinkedNodeItem!;
    //                            }
    //                        }
    //                        //var 試行回数 = 0;
    //                        //var 追加LinkedNodeItem = new LinkedNodeItem(Item);
    //                        //while(true) {
    //                        //    var 既存LinkedNodeItem = Interlocked.CompareExchange(
    //                        //        ref LinkedNode._LinkedNodeItem,
    //                        //        追加LinkedNodeItem,
    //                        //        null
    //                        //    );
    //                        //    if(既存LinkedNodeItem is null) {
    //                        //        this.ConcurrentAddRelationship(Item);
    //                        //        goto 終了;
    //                        //    }
    //                        //    Debug.Assert(!Comparer.Equals(既存LinkedNodeItem.Item,Item));
    //                        //    LinkedNode=既存LinkedNodeItem;
    //                        //    試行回数++;
    //                        //}
    //                    }
    //                }
    //                終了:
    //                ;
    //            }
    //        }
    //    );
    //    Debug.Assert(Array.LongLength==DebugCount);
    //    /*
    //    var ProcessorCount = 1;//Environment.ProcessorCount;
    //    var 分割範囲 = Array.Length/ProcessorCount;
    //    var actions = new Action[ProcessorCount];
    //    var L = 0L;
    //    var R = 0L;
    //    var DebugCount = 0L;
    //    for(var スレッド番号 = 0;スレッド番号<ProcessorCount;スレッド番号++) {
    //        R+=分割範囲;
    //        var R0 = スレッド番号<ProcessorCount-1 ? R : Array.Length;
    //        var L0 = L;
    //        actions[スレッド番号]=() => {
    //            var Array1 = Array;
    //            var L1 = L0;
    //            var R1 = R0;
    //            var TreeRoot = this.TreeRoot;
    //            Interlocked.Add(ref DebugCount,R1-L1);
    //            for(var a = L1;a<R1;a++) {
    //                var Item = Array1[a];
    //                var HashCode = (Int64)(UInt32)Item.GetHashCode();
    //                var TreeNode = TreeRoot;
    //                Int64 下限 = 初期下限, 上限 = 初期上限;
    //                //目的のノードを探索する。存在しなければ作る。
    //                while(true) {
    //                    var CurrentHashCode = (下限+上限)>>1;
    //                    if(HashCode<CurrentHashCode) {
    //                        上限=CurrentHashCode-1;
    //                        this.共通AddTreeNode(ref TreeNode,ref TreeNode.L);
    //                    } else if(HashCode>CurrentHashCode) {
    //                        下限=CurrentHashCode+1;
    //                        this.共通AddTreeNode(ref TreeNode,ref TreeNode.R);
    //                    } else {
    //                        var Comparer = this.Comparer;
    //                        LinkedNode LinkedNode = TreeNode;
    //                        //lock(TreeNode) {
    //                        //    while(true) {
    //                        //        var LinkedNode_LinkedNodeItem = LinkedNode._LinkedNodeItem;
    //                        //        if(LinkedNode_LinkedNodeItem is null) {
    //                        //            LinkedNode._LinkedNodeItem=new LinkedNodeItem(Item);
    //                        //            goto 終了;
    //                        //        }
    //                        //        Debug.Assert(!Comparer.Equals(LinkedNode_LinkedNodeItem.Item,Item));
    //                        //        LinkedNode=LinkedNode_LinkedNodeItem;
    //                        //    }
    //                        //}
    //                        var 試行回数 = 0;
    //                        var 追加LinkedNodeItem = new LinkedNodeItem(Item);
    //                        while(true) {
    //                            var 既存LinkedNodeItem = Interlocked.CompareExchange(
    //                                ref LinkedNode._LinkedNodeItem,
    //                                追加LinkedNodeItem,
    //                                null
    //                            );
    //                            if(既存LinkedNodeItem is null) {
    //                                goto 終了;
    //                            }
    //                            if(!Comparer.Equals(既存LinkedNodeItem.Item,Item)){
    //                            }
    //                            Debug.Assert(!Comparer.Equals(既存LinkedNodeItem.Item,Item));
    //                            LinkedNode=既存LinkedNodeItem;
    //                            試行回数++;
    //                        }
    //                    }
    //                }
    //                終了:
    //                ;
    //            }
    //        };
    //        L=R;
    //    }
    //    Parallel.Invoke(actions);
    //    this._Count=Array.LongLength;
    //    Debug.Assert(Array.LongLength==DebugCount);
    //    */
    //}
    ///// <summary>
    ///// sourceを走査して全て並列で追加する。
    ///// </summary>
    ///// <param name="source">新しいセットの要素のコピー元となるIEnumerable<typeparamref name="T"/>。</param>
    ///// <param name="Comparer">セット内の値を比較する際に使用する <see cref="IEqualityComparer{T}" /> の実装。</param>
    //private void PrivateImport並列(IEnumerable<T> source,IEqualityComparer<T> Comparer) {
    //    this.Comparer=Comparer;
    //    var Count = 0L;
    //    Parallel.ForEach(source,Item=>{
    //        var HashCode = (Int64)(UInt32)Item!.GetHashCode();
    //        var TreeNode = this.TreeRoot;
    //        Int64 下限 = 初期下限, 上限 = 初期上限;
    //        //目的のノードを探索する。存在しなければ作る。
    //        while(true) {
    //            var CurrentHashCode = (下限+上限)>>1;
    //            Debug.Assert(TreeNode is not null);
    //            if(HashCode<CurrentHashCode) {
    //                上限=CurrentHashCode-1;
    //                ConcurrentAddTreeNode(ref TreeNode!,ref TreeNode!.L);
    //            } else if(HashCode>CurrentHashCode) {
    //                下限=CurrentHashCode+1;
    //                ConcurrentAddTreeNode(ref TreeNode!,ref TreeNode!.R);
    //            } else {
    //                var Comparer1 = this.Comparer;
    //                LinkedNode LinkedNode = TreeNode!;
    //                //lock(TreeNode) {
    //                //    while(true) {
    //                //        var LinkedNode_LinkedNodeItem = LinkedNode._LinkedNodeItem;
    //                //        if(LinkedNode_LinkedNodeItem is null) {
    //                //            LinkedNode._LinkedNodeItem=new LinkedNodeItem(Item);
    //                //            return;
    //                //        }
    //                //        Debug.Assert(!Comparer1.Equals(LinkedNode_LinkedNodeItem.Item,Item));
    //                //        LinkedNode=LinkedNode_LinkedNodeItem;
    //                //    }
    //                //}
    //                var 追加LinkedNodeItem = new LinkedNodeItem(Item);
    //                while(true) {
    //                    var 既存LinkedNodeItem = Interlocked.CompareExchange(
    //                        ref LinkedNode._LinkedNodeItem,
    //                        追加LinkedNodeItem,
    //                        null
    //                    );
    //                    if(既存LinkedNodeItem is null) {
    //                        Interlocked.Increment(ref Count);
    //                        return;
    //                    }
    //                    if(Comparer1.Equals(既存LinkedNodeItem.Item,Item)) {
    //                        return;
    //                    }
    //                    LinkedNode=既存LinkedNodeItem;
    //                }
    //            }
    //        }
    //    });
    //    this._Count=Count;
    //}
    ///// <summary>
    /////     <see cref="ImmutableSet{T}" />からのコンストラクタ。
    ///// </summary>
    ///// <param name="source"></param>
    //[MethodImpl(MethodImplOptions.NoInlining|MethodImplOptions.NoOptimization)]
    //protected ImmutableSet(ImmutableSet<T> source) {
    //    var 旧TreeNode = this.変数Enumerator.TreeNode;
    //    var 新TreeNode = source.変数Enumerator.TreeNode;
    //    this.Comparer=source.Comparer;
    //}
    /// <summary>シリアル化したデータを使用して、<see cref="Set{T}" />[]クラスの新しいインスタンスを初期化します。</summary>
    /// <param name="SerializationInfo">
    ///   <see cref="ImmutableSet{T}" /> オブジェクトをシリアル化するために必要な情報を格納している <see cref="SerializationInfo" /> オブジェクト。</param>
    /// <param name="StreamingContext">
    ///   <see cref="ImmutableSet{T}" /> オブジェクトに関連付けられているシリアル化ストリームの転送元および転送先を格納する <see cref="StreamingContext" /> 構造体。</param>
    /// 
        protected ImmutableSet(SerializationInfo SerializationInfo,StreamingContext StreamingContext) {
        this.Comparer=(IEqualityComparer<T>)SerializationInfo.GetValue(
            nameof(this.Comparer),
            typeof(IEqualityComparer<T>)
        )!;
        var RootNode=this.変数Enumerator.TreeNode=(TreeNode)SerializationInfo.GetValue(
            nameof(this.TreeRoot),
            typeof(TreeNode)
        )!;
        static long 検証とカウント(TreeNode? TreeNode,long 下限,long 上限){
            if(TreeNode is null) return 0;
            var Count = 0L;
            LinkedNode LinkedNode = TreeNode;
            while(true) {
                var LinkedNode_LinkedNodeItem = LinkedNode._LinkedNodeItem;
                if(LinkedNode_LinkedNodeItem is null) {
                    break;
                }
                LinkedNode=LinkedNode_LinkedNodeItem!;
                Count++;
            }
            var 中間=(下限+上限)>>1;
            return 検証とカウント(TreeNode.L,下限,中間)+検証とカウント(TreeNode.R,中間,上限)+Count;
        }
        var _Count=SerializationInfo.GetInt64(nameof(this.Count));
        this._Count=検証とカウント(RootNode,初期下限,初期上限);
        Debug.Assert(this._Count==_Count);
        this.Assert();
    }
    ///// <summary>
    ///// データをJsonで書き込む。
    ///// </summary>
    ///// <param name="Writer"></param>
    //public void Write(BinaryWriter Writer) {
    //    Writer.Write(Json.JsonSerializer.Serialize(this.Count));
    //    foreach(var a in this) {
    //        Writer.Write(Json.JsonSerializer.Serialize(a));
    //    }
    //}
    ///// <summary>
    ///// データをJsonで書き込む。
    ///// </summary>
    ///// <param name="Reader"></param>
    //public void Read(BinaryReader Reader) {
    //    var ElementCount=Json.JsonResolver.Serializer().Deserialize<Int64>(Reader.ReadString());
    //    Int64 Count = 0;
    //    for(Int64 a = 0;a<ElementCount;a++) {
    //        if(this.InternalAdd(Json.JsonResolver.Serializer().Deserialize<T>(Reader.ReadString()))){
    //            Count++;
    //        }
    //    }
    //    Debug.Assert(ElementCount==Count);
    //}
    //public class struct_Json {
    //    public readonly Int64 Count;
    //}
    ///// <summary>
    ///// Json文字列
    ///// </summary>
    //public String Json{
    //    get {
    //        var Count = this.Count;
    //        Writer.WriteByte((Byte)(Count>>0));
    //        Writer.WriteByte((Byte)(Count>>8));
    //        Writer.WriteByte((Byte)(Count>>16));
    //        Writer.WriteByte((Byte)(Count>>24));
    //        Writer.WriteByte((Byte)(Count>>32));
    //        Writer.WriteByte((Byte)(Count>>40));
    //        Writer.WriteByte((Byte)(Count>>48));
    //        Writer.WriteByte((Byte)(Count>>56));
    //        foreach(var a in this) {
    //            Writer.Write(Utf8Json.JsonSerializer.Serialize(a));
    //        }
    //    }
    //}
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
                    JsonSerializer.Deserialize<T>(Reader)
                );
                Count++;
            }
#pragma warning disable CA1031 // 一般的な例外の種類はキャッチしません
        } catch(Exception) {
#pragma warning restore CA1031 // 一般的な例外の種類はキャッチしません
            this._Count=Count;
        }
    }
    /// <summary>
    /// シリアライザから呼び出されるコンストラクタ。内部データをシリアライズしやすいように変換する。
    /// </summary>
    /// <param name="SerializationInfo">データ書き込み先</param>
    /// <param name="StreamingContext"></param>
    public void GetObjectData(SerializationInfo SerializationInfo,StreamingContext StreamingContext) {
        SerializationInfo.AddValue(
            nameof(this.Comparer),
            this.Comparer,
            typeof(IEqualityComparer<T>)
        );
        SerializationInfo.AddValue(
            nameof(this.TreeRoot),
            this.TreeRoot,
            typeof(TreeNode)
        );
        SerializationInfo.AddValue(
            nameof(this.Count),
            this.Count
        );
    }
    /// <summary>
    /// 要素を削除する。this.Count--はしない。
    /// </summary>
    /// <param name="Item"></param>
    /// <returns>削除に成功すればtrue、失敗すればfalse。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal virtual bool InternalRemove(T Item) {
        Debug.Assert(Item is not null);
        var TreeNode = this.InternalHashCodeに一致するTreeNodeを取得する((uint)Item!.GetHashCode());
        if(TreeNode is not null){
            var Comparer=this.Comparer;
            LinkedNode LinkedNode =TreeNode;
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
    private static (long TreeNode数, long LinkedNode数) PrivateGetMemoryBytes(TreeNode? TreeNode) {
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
        Debug.Assert(Count==this._Count);
    }
    /// <summary>コレクションの集合としてのHashCode</summary>
    /// <returns>コレクションの集合としてのHashCode</returns>
    public override int GetHashCode() {
        var CRC = new CRC.CRC32();
        var TreeNode = this.TreeRoot;
    LinkedNodeItem走査:
        var HashCode = 0;
        for(var LinkedNodeItem = TreeNode._LinkedNodeItem;LinkedNodeItem is not null;LinkedNodeItem=LinkedNodeItem._LinkedNodeItem) {
            Debug.Assert(LinkedNodeItem.Item is not null);
            HashCode^=LinkedNodeItem.Item!.GetHashCode();
        }
        CRC.Input(HashCode);
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
        return CRC.GetHashCode();
    }
    /// <summary>
    ///   <see cref="ImmutableSet{T}" /> オブジェクトが、指定されたコレクションのサブセットであるかどうかを判断します。</summary>
    /// <returns>
    ///   <see cref="ImmutableSet{T}" /> オブジェクトが <paramref name="other" /> のサブセットである場合は true。それ以外の場合は false。</returns>
    /// <param name="other">現在の <see cref="ImmutableSet{T}" /> オブジェクトと比較するコレクション。</param>
    public bool IsSubsetOf(ImmutableSet<T> other) {
        foreach(var a in this) {
            if(!ExtensionSet.Contains(other,a)) {
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
        if(this.Count==other.Count) {
            return false;
        }
        foreach(var a in this) {
            if(!ExtensionSet.Contains(other,a)) {
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
            if(!ExtensionSet.Contains(this,a)) {
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
        if(this.Count==other.Count) {
            return false;
        }
        foreach(var a in other) {
            if(!ExtensionSet.Contains(this,a)) {
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
            if(ExtensionSet.Contains(this,a)) {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 集合としての等価。
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool SetEquals(ImmutableSet<T> other) {
        if(ReferenceEquals(this,other)) {
            return true;
        }
        if(this.Count!=other.Count) {
            return false;
        }
        if(this.GetHashCode()!=other.GetHashCode()) {
            return false;
        }
        foreach(var a in this) {
            if(!ExtensionSet.Contains(other,a)) {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    ///   <see cref="ImmutableSet{T}" /> オブジェクトと指定されたコレクションに同じ要素が存在するかどうかを判断します。</summary>
    /// <returns>
    ///   <see cref="ImmutableSet{T}" /> オブジェクトが <paramref name="other" /> と等しい場合は true。それ以外の場合は false。</returns>
    /// <param name="other">現在の <see cref="ImmutableSet{T}" /> オブジェクトと比較するコレクション。</param>
    public bool Equals(ImmutableSet<T>? other) => this.SetEquals(other);
    public override bool Equals(object? obj) => obj is ImmutableSet<T> other&&this.Equals(other);
    /// <summary>
    /// 配列に変換する。
    /// </summary>
    /// <returns></returns>
    public T[] ToArray() {
        var array = new T[this.Count];
        var ArrayIndex = 0L;
        foreach(var a in this) {
            array[ArrayIndex++]=a;
        }
        return array;
    }
    /// <summary>
    /// 濃度0の集合。
    /// </summary>
    public static readonly ImmutableSet<T> EmptySet = new Set<T>();
    /// <summary>
    /// <see cref="ImmutableSet{T}" />の文字列表現
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $"{nameof(this.Count)}:{this.Count}";
    /// <summary>
    /// ILで<see cref="ImmutableSet{T}" />,<see cref="IEnumerable{T}" />を２回走査するときの２回目の走査に使う、F(irst)I(n)L(ast)O(ut)。
    /// </summary>
    internal struct FILO:IEnumerator<T> {
        private LinkedNodeItem? LinkedListNode;
        /// <summary>
        /// 要素数
        /// </summary>
        internal long Count;
        public T Current{get;private set;}
#pragma warning disable CS8603 // Null 参照戻り値である可能性があります。
        object IEnumerator.Current => this.Current;
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
            this.LinkedListNode=new LinkedNodeItem(Item,this.LinkedListNode);
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
        bool IEnumerator.MoveNext() => this.MoveNext();
        public void Reset() {
        }
    }
    /// <summary>
    /// ILで<see cref="ImmutableSet{T}" />,<see cref="IEnumerable{T}" />を２回走査するときの２回目の走査に使う、F(irst)I(n)F(irst)O(ut)。
    /// </summary>
    internal struct FIFO:IEnumerator<T>{
        private LinkedNode FirstNode;
        private LinkedNode LastNode;
        private LinkedNode CurrentNode;
        public T Current {
            get; private set;
        }
#pragma warning disable CS8603 // Null 参照戻り値である可能性があります。
        object IEnumerator.Current => this.Current;
#pragma warning restore CS8603 // Null 参照戻り値である可能性があります。
        /// <summary>
        /// structは引数なしコンストラクタの代わり
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Constructor() => this.LastNode=this.FirstNode=new LinkedNode(null);
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
        bool IEnumerator.MoveNext() => this.MoveNext();
        public bool SequenceEqual(FIFO other) {
            var EqualityComparer=EqualityComparer<T>.Default;
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