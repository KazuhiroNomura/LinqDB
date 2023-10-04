using System;
using System.Collections.Generic;
using LinqDB.Databases;
using System.IO;
namespace LinqDB.Sets;

/// <summary>
/// Containerで使用する完結したSetコレクション。
/// </summary>
/// <typeparam name="TElement"></typeparam>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TContainer"></typeparam>
public sealed class Set<TKey,TElement, TContainer>:Set<TKey,TElement>,IContainer<TContainer>
    where TElement : Entity<TKey,TContainer>
    where TKey : struct, IEquatable<TKey>
    where TContainer : Container{
#pragma warning disable CA1823 // 使用されていないプライベート フィールドを使用しません
    private new static readonly Serializers.MessagePack.Formatters.Sets.Set<TKey,TElement,TContainer> InstanceMessagePack=new();
    private new static readonly Serializers.Utf8Json.Formatters.Sets.Set<TKey,TElement,TContainer> InstanceUtf8Json=new();
#pragma warning restore CA1823 // 使用されていないプライベート フィールドを使用しません
    static Set()=> MemoryPack.MemoryPackFormatterProvider.Register(Serializers.MemoryPack.Formatters.Sets.Set<TKey,TElement,TContainer>.Instance);
    /// <summary>
    /// このEntitySet&lt;<typeparamref name="TElement"/>,&lt;<typeparamref name="TKey"/>,&lt;<typeparamref name="TContainer"/>>の属する<typeparamref name="TContainer"/>。
    /// </summary>
    public TContainer Container {
        get;
        private set;
    }
    public Set():this(null!){}
    /// <summary>
    /// 既定コンストラクタ
    /// </summary>
    /// <param name="Container"></param>
    public Set(TContainer Container) => this.Container=Container;
    /// <summary>
    ///   <see cref="Set{TValue,TKey,TContainer}" /> クラスの新しいインスタンスを初期化します。このセット型には既定の等値比較子が使用されます。指定されたコレクションからコピーされた要素が格納され、コピー対象の要素数を格納できるだけの十分な容量が確保されます。</summary>
    /// <param name="Container"></param>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="IEqualityComparer{TValue}" /> の実装。</param>
    public Set(TContainer Container,IEqualityComparer<TElement> Comparer) :base(Comparer){
        this.Container=Container;
    }
    /// <summary>
    ///   <see cref="Set{TValue,TKey,TContainer}" /> クラスの新しいインスタンスを初期化します。このセット型には既定の等値比較子が使用されます。指定されたコレクションからコピーされた要素が格納されますます。</summary>
    /// <param name="Container"></param>
    /// <param name="source">新しいセットの要素のコピー元となるコレクション。</param>
    public Set(TContainer Container,System.Collections.Generic.IEnumerable<TElement> source) : this(Container) {
        this.PrivateProtectedImport(source);
    }
    /// <summary>
    ///   <see cref="Set{TValue,TKey,TContainer}" /> クラスの新しいインスタンスを初期化します。指定されたコレクションからコピーされた要素が格納されます。</summary>
    /// <param name="Container"></param>
    /// <param name="source">新しいセットの要素のコピー元となるコレクション。</param>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="IEqualityComparer{TValue}" /> の実装。</param>
    public Set(TContainer Container,System.Collections.Generic.IEnumerable<TElement> source,IEqualityComparer<TElement> Comparer) : this(Container,Comparer) {
        this.PrivateProtectedImport(source);
    }
    /// <summary>
    ///   <see cref="Set{TValue,TKey,TContainer}" /> クラスの新しいインスタンスを初期化します。指定されたコレクションからコピーされた要素が格納されます。</summary>
    /// <param name="Container"></param>
    /// <param name="source">新しいセットの要素のコピー元となる配列。</param>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="IEqualityComparer{TValue}" /> の実装。</param>
    public Set(TContainer Container,TElement[] source,IEqualityComparer<TElement> Comparer) : this(Container,Comparer) {
        this.PrivateProtectedImport(source);
    }
    /// <summary>
    ///   <see cref="Set{TValue,TKey,TContainer}" /> クラスの新しいインスタンスを初期化します。指定されたコレクションからコピーされた要素が格納されます。</summary>
    /// <param name="Container"></param>
    /// <param name="source">新しいセットの要素のコピー元となる配列。</param>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="IEqualityComparer{TValue}" /> の実装。</param>
    public Set(TContainer Container,ImmutableSet<TElement> source,IEqualityComparer<TElement> Comparer) : this(Container,Comparer) {
        this.PrivateProtectedImport(source);
    }
    /// <summary>
    /// <see cref="ImmutableSet{TValue}" />からのコンストラクタ。Containerに属させる。
    /// </summary>
    public Set(TContainer Container,ImmutableSet<TElement> source) : this(Container) => this.PrivateProtectedImport(source);
    /// <summary>
    /// 配列からのコンストラクタ。Containerに属させる。
    /// </summary>
    public Set(TContainer Container,params TElement[] source) : base(EqualityComparer<TElement>.Default) {
        this.Container=Container;
        this.PrivateProtectedImport(source);
    }
    /// <summary>
    /// 別のContainerに属してコピーする。
    /// </summary>
    /// <param name="Container"></param>
    /// <returns></returns>
    public Set<TKey,TElement,TContainer> Copy(TContainer Container) => new(Container,this);
    /// <summary>
    /// デシリアライズした後や一括追加した後のリレーションシップ作成。
    /// </summary>
    public void UpdateRelationship() {
        var Container = this.Container;
        foreach(var Item in this) {
            Item.AddRelationship(Container);
        }
    }
    internal override void AddRelationship(TElement Item) => Item.AddRelationship(this.Container);
    internal override void RemoveRelationship(TElement Item) => Item.RemoveRelationship();
    /// <summary>
    /// Setの差分を書き込む。
    /// </summary>
    /// <param name="Writer"></param>
    /// <param name="second"></param>
    public void WriteDifference(Stream Writer,Set<TKey,TElement,TContainer> second) {
        var List削除Key = new List<TKey>();
        var List追加Value = new List<TElement>();
        foreach(var v1 in this) {
            if(second.Contains(v1)) continue;
            List削除Key.Add(v1.Key);
        }
        foreach(var v2 in second) {
            if(this.Contains(v2)) continue;
            List追加Value.Add(v2);
        }
        {
            var buffer = Utf8Json.JsonSerializer.Serialize(List削除Key);
            Writer.Write(buffer,0,buffer.Length);
        }
        {
            var buffer = Utf8Json.JsonSerializer.Serialize(List追加Value);
            Writer.Write(buffer,0,buffer.Length);
        }
        //ExpressionSurrogateSelector.serializer.WriteObject(Writer,List削除Key);
        //ExpressionSurrogateSelector.serializer.WriteObject(Writer,List追加Value);
    }
    //private readonly Utf8Json.
    /// <summary>
    /// Setの差分を読み込む。
    /// </summary>
    /// <param name="Reader"></param>
    public void ReadDifference(Stream Reader) {
        //var List削除Key=(List<TKey>)ExpressionSurrogateSelector.serializer.ReadObject(Reader);
        //var List追加Value=(List<TValue>)ExpressionSurrogateSelector.serializer.ReadObject(Reader);
        var List削除Key =  Utf8Json.JsonSerializer.Deserialize<List<TKey>>(Reader);
        var List追加Value =Utf8Json.JsonSerializer.Deserialize<List<TElement>>(Reader);
        var Count = this.LongCount;
        foreach(var 削除Key in List削除Key) {
            if(!this.RemoveKey(削除Key)) {
                throw new InvalidDataException("トランザクションログに存在しないキーを削除するという不整合が発生した。");
            }
        }
        Count-=List削除Key.Count;
        foreach(var 追加Value in List追加Value) {
            if(!this.InternalAdd(追加Value)) {
                throw new InvalidDataException("トランザクションログに追加に重複という不整合が発生した。");
            }
        }
        this._LongCount=Count+List追加Value.Count;
    }
}