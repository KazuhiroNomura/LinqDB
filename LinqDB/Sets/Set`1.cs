using LinqDB.Sets.Exceptions;

using System;
//using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Generic=System.Collections.Generic;
namespace LinqDB.Sets;

/// <summary>
/// 関係集合。
/// </summary>
/// <typeparam name="T"></typeparam>
[Serializable,MessagePack.MessagePackObject]
public class Set<T>:ImmutableSet<T>,ICollection<T>{
    static Set()=>MemoryPack.MemoryPackFormatterProvider.Register(Serializers.MemoryPack.Formatters.Sets.Set<T>.Instance);
    //public class Formatter:MemoryPack.MemoryPackFormatter<Set<T>> {
    //    public static readonly Formatter Instance = new();
    //    public override void Serialize<TBufferWriter>(ref MemoryPack.MemoryPackWriter<TBufferWriter> writer,scoped ref Set<T>? value) {
    //        var Count = value!.LongCount;
    //        var Formatter = writer.GetFormatter<T>();
    //        writer.WriteVarInt(Count);
    //        foreach(var item in value) {
    //            var item0 = item;
    //            Formatter.Serialize(ref writer,ref item0);
    //        }
    //    }
    //    public override void Deserialize(ref MemoryPack.MemoryPackReader reader,scoped ref Set<T>? value) {
    //        var set = new Set<T>();
    //        var Count = reader.ReadVarIntInt64();
    //        var Formatter = reader.GetFormatter<T>();
    //        T? item = default;
    //        for(long a = 0;a<Count;a++) {
    //            Formatter.Deserialize(ref reader,ref item);
    //            set.Add(item);
    //        }
    //        value=set;
    //    }
    //}
    /// <summary>
    ///   <see cref="Set{T}" /> クラスの新しいインスタンスを初期化します。このセット型には既定の等値比較子が使用されます。
    /// </summary>
    [MemoryPack.MemoryPackConstructor]
    public Set(){
    }
    /// <summary>
    /// <see cref="Set{T}" /> クラスの新しいインスタンスを初期化します。このセット型には等値比較子が使用されます。
    /// </summary>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="Generic.IEqualityComparer{T}" /> の実装。</param>
    public Set(Generic.IEqualityComparer<T> Comparer):base(Comparer) {
    }
    /// <summary>
    ///   <see cref="Set{T}" /> クラスの新しいインスタンスを初期化します。このセット型には既定の等値比較子が使用されます。指定されたコレクションからコピーされた要素が格納されますます。</summary>
    /// <param name="source">新しいセットの要素のコピー元となるコレクション。</param>
    public Set(Generic.IEnumerable<T> source):base(source,Generic.EqualityComparer<T>.Default) {
    }
    /// <summary>
    ///   <see cref="Set{T}" /> クラスの新しいインスタンスを初期化します。指定されたコレクションからコピーされた要素が格納されます。</summary>
    /// <param name="source">新しいセットの要素のコピー元となるコレクション。</param>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="Generic.IEqualityComparer{T}" /> の実装。</param>
    public Set(Generic.IEnumerable<T> source,Generic.IEqualityComparer<T> Comparer) : base(source,Comparer) {
    }
    /// <summary>
    ///   <see cref="Set{T}" /> クラスの新しいインスタンスを初期化します。指定されたコレクションからコピーされた要素が格納されます。</summary>
    /// <param name="source">新しいセットの要素のコピー元となる配列。</param>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="Generic.IEqualityComparer{T}" /> の実装。</param>
    public Set(T[] source,Generic.IEqualityComparer<T> Comparer) :base(source,Comparer) {
    }
    /// <summary>
    ///   <see cref="Set{T}" /> クラスの新しいインスタンスを初期化します。指定されたコレクションからコピーされた要素が格納されます。</summary>
    /// <param name="source">新しいセットの要素のコピー元となる配列。</param>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="Generic.IEqualityComparer{T}" /> の実装。</param>
    [MethodImpl(MethodImplOptions.NoInlining|MethodImplOptions.NoOptimization)]
    public Set(ImmutableSet<T> source,Generic.IEqualityComparer<T> Comparer) :base(source,Comparer) {
    }
    //public sealed class JsonFormatter:Utf8Json.IJsonFormatter<Set<T>>{
    //    public static readonly JsonFormatter Instance=new();
    //    public void Serialize(ref Utf8Json.JsonWriter writer,Set<T> value,Utf8Json.IJsonFormatterResolver formatterResolver){
    //        var Formatter=formatterResolver.GetFormatter<T>();
    //        writer.WriteBeginArray();
    //        var 二度目以降の出力か=false;
    //        var TreeNode = value.TreeRoot;
    //    LinkedNodeItem走査:
    //        for(var LinkedNodeItem = TreeNode._LinkedNodeItem;LinkedNodeItem is not null;LinkedNodeItem=LinkedNodeItem._LinkedNodeItem) {
    //            if(二度目以降の出力か)writer.WriteValueSeparator();
    //            二度目以降の出力か=true;
    //            Formatter.Serialize(ref writer,LinkedNodeItem.Item,formatterResolver);
    //        }
    //        if(TreeNode.L is not null) {
    //            TreeNode=TreeNode.L;
    //            goto LinkedNodeItem走査;
    //        }
    //        右に移動:
    //        if(TreeNode.R is not null) {
    //            TreeNode=TreeNode.R;
    //            goto LinkedNodeItem走査;
    //        }
    //        //上に移動
    //        while(!(TreeNode.P is null)) {
    //            var 旧TreeNode_P = TreeNode.P;
    //            if(旧TreeNode_P.L==TreeNode) {
    //                TreeNode=旧TreeNode_P;
    //                goto 右に移動;
    //            }
    //            TreeNode=旧TreeNode_P;
    //        }
    //        writer.WriteEndArray();
    //    }
    //    public Set<T> Deserialize(ref Utf8Json.JsonReader reader,Utf8Json.IJsonFormatterResolver formatterResolver){
    //        var result=new Set<T>();
    //        var Formatter=formatterResolver.GetFormatter<T>();
    //        reader.ReadIsBeginArrayWithVerify();
    //        while(true){
    //            var value=Formatter.Deserialize(ref reader,formatterResolver);
    //            result.IsAdded(value);
    //            var s=reader.ReadIsValueSeparator();
    //            if(!s) break;
    //        }
    //        return result;
    //    }
    //}
    /// <summary>
    /// 代入元からコピーする。代入元はDisposeする。
    /// </summary>
    /// <param name="source"></param>
    public virtual void Assign(ImmutableSet<T> source) {
        this.変数Enumerator.TreeNode=new TreeNodeT(null);
        this.PrivateProtectedImport(source);
    }
    //private void Difference(TreeNode 旧TreeNode,TreeNode 新TreeNode,Int64 下限 ,Int64 上限,List<T> 削除Item,List<T> 追加Item) {
    //    TreeNode? 旧TreeNode0 = 旧TreeNode;
    //    TreeNode? 新TreeNode0 = 新TreeNode;
    //    var 旧TreeRoot =旧TreeNode0.P;
    //    LinkedNodeItem走査:
    //    var 旧TreeNode_LinkedNodeItem=旧TreeNode0._LinkedNodeItem;
    //    var 新TreeNode_LinkedNodeItem=新TreeNode0._LinkedNodeItem;
    //    for(var 旧LinkedNodeItem = 旧TreeNode_LinkedNodeItem;旧LinkedNodeItem is not null;旧LinkedNodeItem=旧LinkedNodeItem._LinkedNodeItem) {
    //        var 新LinkedNodeItem = 新TreeNode_LinkedNodeItem;
    //        while(true){
    //            if(新LinkedNodeItem is null) {
    //                削除Item.Add(旧LinkedNodeItem.Item);
    //                break;
    //            }
    //            新LinkedNodeItem=新LinkedNodeItem._LinkedNodeItem;
    //        }
    //    }
    //    for(var 新LinkedNodeItem = 新TreeNode_LinkedNodeItem;新LinkedNodeItem is not null;新LinkedNodeItem=新LinkedNodeItem._LinkedNodeItem) {
    //        var 旧LinkedNodeItem = 旧TreeNode_LinkedNodeItem;
    //        while(true) {
    //            if(旧LinkedNodeItem is null) {
    //                追加Item.Add(新LinkedNodeItem.Item);
    //                break;
    //            }
    //            旧LinkedNodeItem=旧LinkedNodeItem._LinkedNodeItem;
    //        }
    //    }
    //    var CurrentHashCode = (上限+下限)>>1;
    //    if(旧TreeNode0.L is not null) {
    //        if(新TreeNode0.L is null) {
    //            削除HashCode.Add((UInt32)CurrentHashCode);
    //        } else {
    //            下限=CurrentHashCode-1L;
    //            旧TreeNode0=旧TreeNode0.L;
    //            新TreeNode0=新TreeNode0.L;
    //            goto LinkedNodeItem走査;
    //        }
    //    }
    //    右に移動:
    //    if(旧TreeNode0.R is not null) {
    //        Debug.Assert(新TreeNode0 is not null);
    //        if(新TreeNode0!.R is null) {
    //            削除HashCode.Add((UInt32)CurrentHashCode);
    //        } else {
    //            上限=CurrentHashCode+1L;
    //            旧TreeNode0=旧TreeNode0.R;
    //            新TreeNode0=新TreeNode0.R;
    //            goto LinkedNodeItem走査;
    //        }
    //    }
    //    while(旧TreeNode0.P!=旧TreeRoot) {
    //        var 旧TreeNode_P=旧TreeNode0.P;
    //        Debug.Assert(旧TreeNode_P is not null);
    //        if(旧TreeNode_P!.L==旧TreeNode0) {
    //            旧TreeNode0=旧TreeNode_P;
    //            Debug.Assert(新TreeNode0 is not null);
    //            新TreeNode0=新TreeNode0!.P;
    //            goto 右に移動;
    //        }
    //        旧TreeNode0=旧TreeNode_P;
    //        Debug.Assert(新TreeNode0 is not null);
    //        新TreeNode0=新TreeNode0!.P;
    //    }
    //}
    private static void WriteObject(string フォルダ名,object value) {
        var フィールドに対応するファイル名 = フォルダ名+@"\"+DateTimeOffset.Now.ToString("yyyyMMddHHmmssff",CultureInfo.CurrentCulture)+".tlg";
        using var FileStream = new FileStream(フィールドに対応するファイル名,FileMode.Create,FileAccess.Write,FileShare.Read);
        Utf8Json.JsonSerializer.Serialize(FileStream,value);
        //var Writer = XmlDictionaryWriter.CreateTextWriter(FileStream,Encoding.UTF8,false);
        //ExpressionSurrogateSelector.serializer.WriteObject(Writer,value);
        //Writer.Flush();
    }
    ///// <summary>
    ///// Setの差分を書き込む。
    ///// </summary>
    ///// <param name="Writer"></param>
    ///// <param name="削除HashCode"></param>
    ///// <param name="second"></param>
    //public void WriteDifference(XmlDictionaryWriter Writer,List<UInt32> 削除HashCode,ImmutableSet<T> second) {
    //    削除HashCode.Clear();
    //    var 削除Item = new List<T>();
    //    var 追加Item = new List<T>();
    //    this.Difference(this.TreeRoot,second.TreeRoot,初期下限,初期上限,削除HashCode,削除Item,追加Item);
    //    ExpressionSurrogateSelector.serializer.WriteObject(Writer,削除HashCode);
    //    ExpressionSurrogateSelector.serializer.WriteObject(Writer,削除Item);
    //    ExpressionSurrogateSelector.serializer.WriteObject(Writer,追加Item);
    //}
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
    public virtual void Clear() {
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
