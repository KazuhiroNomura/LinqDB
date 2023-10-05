using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
namespace LinqDB.Sets;

/// <summary>
/// TKeyキーを含むTElementのSetコレクション。
/// </summary>
/// <typeparam name="TElement"></typeparam>
/// <typeparam name="TKey"></typeparam>
public class Set<TKey,TElement>:Set<TElement>
    where TElement:IKey<TKey>
    where TKey : struct, IEquatable<TKey>{
#pragma warning disable CA1823 // 使用されていないプライベート フィールドを使用しません
    //internal new static readonly Serializers.MemoryPack.Formatters.Sets.Set<TKey,TElement> InstanceMemoryPack=new();
    //internal new static readonly Serializers.MessagePack.Formatters.Sets.Set<TKey,TElement>InstanceMessagePack=new();
    //internal new static readonly Serializers.Utf8Json.Formatters.Sets.Set<TKey,TElement>   InstanceUtf8Json=new();
    internal new static readonly Serializers.MemoryPack.Formatters.Sets.Set<TKey,TElement> InstanceMemoryPack=Serializers.MemoryPack.Formatters.Sets.Set<TKey,TElement>.Instance;
    internal new static readonly Serializers.MessagePack.Formatters.Sets.Set<TKey,TElement>InstanceMessagePack=Serializers.MessagePack.Formatters.Sets.Set<TKey,TElement>.Instance;
    internal new static readonly Serializers.Utf8Json.Formatters.Sets.Set<TKey,TElement>   InstanceUtf8Json=Serializers.Utf8Json.Formatters.Sets.Set<TKey,TElement>.Instance;
#pragma warning restore CA1823 // 使用されていないプライベート フィールドを使用しません
    //static Set()=> MemoryPack.MemoryPackFormatterProvider.Register(Serializers.MemoryPack.Formatters.Sets.Set<TKey,TElement>.Instance);
    public Set() { }
    /// <summary>
    ///   <see cref="Set{TKey,TElement}" /> クラスの新しいインスタンスを初期化します。このセット型には既定の等値比較子が使用されます。指定されたコレクションからコピーされた要素が格納され、コピー対象の要素数を格納できるだけの十分な容量が確保されます。</summary>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="IEqualityComparer{TElement}" /> の実装。</param>
    public Set(IEqualityComparer<TElement> Comparer) : base(Comparer) {
    }
    /// <summary>
    ///   <see cref="Set{TKey,TElement}" /> クラスの新しいインスタンスを初期化します。このセット型には既定の等値比較子が使用されます。指定されたコレクションからコピーされた要素が格納されますます。</summary>
    /// <param name="source">新しいセットの要素のコピー元となるコレクション。</param>
    public Set(System.Collections.Generic.IEnumerable<TElement> source) : base(source) {
    }
    /// <summary>
    ///   <see cref="Set{TKey,TElement}" /> クラスの新しいインスタンスを初期化します。指定されたコレクションからコピーされた要素が格納されます。</summary>
    /// <param name="source">新しいセットの要素のコピー元となるコレクション。</param>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="IEqualityComparer{TElement}" /> の実装。</param>
    public Set(System.Collections.Generic.IEnumerable<TElement> source,IEqualityComparer<TElement> Comparer) : base(source,Comparer) {
    }
    /// <summary>
    ///   <see cref="Set{TKey,TElement}" /> クラスの新しいインスタンスを初期化します。指定されたコレクションからコピーされた要素が格納されます。</summary>
    /// <param name="source">新しいセットの要素のコピー元となる配列。</param>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="IEqualityComparer{TElement}" /> の実装。</param>
    public Set(TElement[] source,IEqualityComparer<TElement> Comparer) : base(source,Comparer) {
    }
    /// <summary>
    ///   <see cref="Set{TKey,TElement}" /> クラスの新しいインスタンスを初期化します。指定されたコレクションからコピーされた要素が格納されます。</summary>
    /// <param name="source">新しいセットの要素のコピー元となる配列。</param>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="IEqualityComparer{TElement}" /> の実装。</param>
    public Set(ImmutableSet<TElement> source,IEqualityComparer<TElement> Comparer) : base(source,Comparer) {
    }
    /// <summary>
    /// キーから値を取得する。
    /// </summary>
    /// <param name="key"></param>
    /// <exception cref="KeyNotFoundException"></exception>
    [IgnoreDataMember]
    public TElement this[TKey key] {
        get {
            TElement value = default!;
            if(this.TryGetValue(key,ref value)) {
                return value;
            }
            throw new KeyNotFoundException();
        }
    }
    /// <summary>
    /// 集合からキーに一致する値を削除する。
    /// </summary>
    /// <param name="Key"></param>
    /// <returns></returns>
    public bool RemoveKey(TKey Key) {
        var TreeNode = this.InternalHashCodeに一致するTreeNodeを取得する((uint)Key.GetHashCode());
        if(TreeNode is null)
            return false;
        LinkedNodeT LinkedNode = TreeNode;
        for(var a = TreeNode._LinkedNodeItem;a is not null;a=a._LinkedNodeItem) {
            if(a.Item.Key.Equals(Key)) {
                this.RemoveRelationship(a.Item);
                LinkedNode._LinkedNodeItem=a._LinkedNodeItem;
                while(TreeNode.P is not null&&TreeNode.L is null&&TreeNode.R is null&&TreeNode._LinkedNodeItem is null) {
                    var P = TreeNode.P;
                    Debug.Assert(P.L==TreeNode||P.R==TreeNode);
                    if(P.L==TreeNode) {
                        P.L=null;
                    } else {
                        P.R=null;
                    }
                    TreeNode=P;
                }
                this._LongCount--;
                return true;
            }
            LinkedNode=a;
        }
        return false;
    }
    /// <summary>
    /// キーから値を取得する。
    /// </summary>
    /// <param name="Key"></param>
    /// <param name="Value"></param>
    /// <returns>値が取得出来たか</returns>
    public bool TryGetValue(TKey Key,ref TElement Value) {
        var TreeNode = this.InternalHashCodeに一致するTreeNodeを取得する((uint)Key.GetHashCode());
        if(TreeNode is not null) {
            for(var a = TreeNode._LinkedNodeItem;a is not null;a=a._LinkedNodeItem) {
                if(a.Item.Key.Equals(Key)) {
                    Value=a.Item;
                    return true;
                }
            }
        }
        return false;
    }
    /// <summary>
    /// キーから集合を取得する。
    /// </summary>
    /// <param name="Key"></param>
    /// <returns>値が取得出来たか</returns>
    public IEnumerable<TElement> GetSet(TKey Key) {
        var TreeNode = this.InternalHashCodeに一致するTreeNodeを取得する((uint)Key.GetHashCode());
        if(TreeNode is not null) {
            for(var a = TreeNode._LinkedNodeItem;a is not null;a=a._LinkedNodeItem) {
                if(a.Item.Key.Equals(Key)) {
                    var Result = new Set<TElement> {
                        a.Item
                    };
                    return Result;
                }
            }
        }
        return EmptySet;
    }
    /// <summary>
    /// キーに一致する値を削除する。
    /// </summary>
    /// <param name="Key"></param>
    /// <returns></returns>
    public bool ContainsKey(TKey Key) {
        var TreeNode = this.InternalHashCodeに一致するTreeNodeを取得する((uint)Key.GetHashCode());
        if(TreeNode is not null) {
            for(var a = TreeNode._LinkedNodeItem;a is not null;a=a._LinkedNodeItem) {
                if(a.Item.Key.Equals(Key)) {
                    return true;
                }
            }
        }
        return false;
    }
    internal override bool InternalAdd(TElement Item) {
        var HashCode = (long)(uint)Item.GetHashCode();
        if(this.InternalAdd前半(out var 下限,out var 上限,out var TreeNode,HashCode)) {
            var Comparer = EqualityComparer<TKey>.Default;
            LinkedNodeT LinkedNode = TreeNode;
            while(true) {
                var LinkedNodeItem = LinkedNode._LinkedNodeItem;
                if(LinkedNodeItem is null) {
                    this.AddRelationship(Item);
                    LinkedNode._LinkedNodeItem=new LinkedNodeItemT(Item);
                    return true;
                }
                if(Comparer.Equals(LinkedNodeItem.Item.Key,Item.Key)) {
                    return false;
                    //throw new RelationshipException("キー"+Item.PrimaryKey.ToString()+"が重複していたので失敗した。");
                }
                LinkedNode=LinkedNodeItem;
            }
        }
        this.AddRelationship(Item);
        InternalAdd後半(下限,上限,TreeNode,HashCode,new LinkedNodeItemT(Item));
        return true;
    }
}