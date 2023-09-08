using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using LinqDB.Databases;
using System.IO;
using System.CodeDom.Compiler;
using LinqDB.Serializers.Utf8Json;
namespace LinqDB.Sets;

/// <summary>
/// Containerで使用する完結したSetコレクション。
/// </summary>
/// <typeparam name="TValue"></typeparam>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TContainer"></typeparam>
[Serializable]
public sealed class Set<TValue, TKey, TContainer>:Set<TValue,TKey>,ISet<TContainer>, IWriteRead<TValue>
    where TValue : Entity<TKey,TContainer>, IWriteRead<TValue>
    where TKey : struct, IEquatable<TKey>
    where TContainer : Container{
    /// <summary>
    /// このEntitySet&lt;<typeparamref name="TValue"/>,&lt;<typeparamref name="TKey"/>,&lt;<typeparamref name="TContainer"/>>の属する<typeparamref name="TContainer"/>。
    /// </summary>
    public TContainer Container {
        get;
        private set;
    }
    /// <summary>
    /// 既定コンストラクタ
    /// </summary>
    /// <param name="Container"></param>
    public Set(TContainer Container) => this.Container=Container;
    /// <summary>
    ///   <see cref="Set{TValue,TKey,TContainer}" /> クラスの新しいインスタンスを初期化します。このセット型には既定の等値比較子が使用されます。指定されたコレクションからコピーされた要素が格納され、コピー対象の要素数を格納できるだけの十分な容量が確保されます。</summary>
    /// <param name="Container"></param>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="IEqualityComparer{TValue}" /> の実装。</param>
    public Set(TContainer Container,IEqualityComparer<TValue> Comparer) :base(Comparer){
        this.Container=Container;
    }
    /// <summary>
    ///   <see cref="Set{TValue,TKey,TContainer}" /> クラスの新しいインスタンスを初期化します。このセット型には既定の等値比較子が使用されます。指定されたコレクションからコピーされた要素が格納されますます。</summary>
    /// <param name="Container"></param>
    /// <param name="source">新しいセットの要素のコピー元となるコレクション。</param>
    public Set(TContainer Container,IEnumerable<TValue> source) : this(Container) {
        this.PrivateProtectedImport(source);
    }
    /// <summary>
    ///   <see cref="Set{TValue,TKey,TContainer}" /> クラスの新しいインスタンスを初期化します。指定されたコレクションからコピーされた要素が格納されます。</summary>
    /// <param name="Container"></param>
    /// <param name="source">新しいセットの要素のコピー元となるコレクション。</param>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="IEqualityComparer{TValue}" /> の実装。</param>
    public Set(TContainer Container,IEnumerable<TValue> source,IEqualityComparer<TValue> Comparer) : this(Container,Comparer) {
        this.PrivateProtectedImport(source);
    }
    /// <summary>
    ///   <see cref="Set{TValue,TKey,TContainer}" /> クラスの新しいインスタンスを初期化します。指定されたコレクションからコピーされた要素が格納されます。</summary>
    /// <param name="Container"></param>
    /// <param name="source">新しいセットの要素のコピー元となる配列。</param>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="IEqualityComparer{TValue}" /> の実装。</param>
    public Set(TContainer Container,TValue[] source,IEqualityComparer<TValue> Comparer) : this(Container,Comparer) {
        this.PrivateProtectedImport(source);
    }
    /// <summary>
    ///   <see cref="Set{TValue,TKey,TContainer}" /> クラスの新しいインスタンスを初期化します。指定されたコレクションからコピーされた要素が格納されます。</summary>
    /// <param name="Container"></param>
    /// <param name="source">新しいセットの要素のコピー元となる配列。</param>
    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="IEqualityComparer{TValue}" /> の実装。</param>
    public Set(TContainer Container,ImmutableSet<TValue> source,IEqualityComparer<TValue> Comparer) : this(Container,Comparer) {
        this.PrivateProtectedImport(source);
    }
    /// <summary>
    /// <see cref="ImmutableSet{TValue}" />からのコンストラクタ。Containerに属させる。
    /// </summary>
    public Set(TContainer Container,ImmutableSet<TValue> source) : this(Container) => this.PrivateProtectedImport(source);
    /// <summary>
    /// 配列からのコンストラクタ。Containerに属させる。
    /// </summary>
    public Set(TContainer Container,params TValue[] source) : base(EqualityComparer<TValue>.Default) {
        this.Container=Container;
        this.PrivateProtectedImport(source);
    }
    /// <summary>
    /// 別のContainerに属してコピーする。
    /// </summary>
    /// <param name="Container"></param>
    /// <returns></returns>
    public Set<TValue,TKey,TContainer> Copy(TContainer Container) => new(Container,this);
    private Set(SerializationInfo SerializationInfo,StreamingContext StreamingContext) : base(SerializationInfo,StreamingContext) => this.Container=null!;
    /// <summary>
    /// デシリアライズした後や一括追加した後のリレーションシップ作成。
    /// </summary>
    public void UpdateRelationship() {
        var Container = this.Container;
        foreach(var Item in this) {
            Item.AddRelationship(Container);
        }
    }
    internal override void AddRelationship(TValue Item) => Item.AddRelationship(this.Container);
    internal override void RemoveRelationship(TValue Item) => Item.RemoveRelationship();
    /// <summary>
    /// Setの差分を書き込む。
    /// </summary>
    /// <param name="Writer"></param>
    /// <param name="second"></param>
    public void WriteDifference(Stream Writer,Set<TValue,TKey,TContainer> second) {
        var List削除Key = new List<TKey>();
        var List追加Value = new List<TValue>();
        foreach(var v1 in this) {
            if(second.Contains(v1)) continue;
            List削除Key.Add(v1.PrimaryKey);
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
    /// <summary>
    /// Setの差分を読み込む。
    /// </summary>
    /// <param name="Reader"></param>
    public void ReadDifference(Stream Reader) {
        //var List削除Key=(List<TKey>)ExpressionSurrogateSelector.serializer.ReadObject(Reader);
        //var List追加Value=(List<TValue>)ExpressionSurrogateSelector.serializer.ReadObject(Reader);
        var List削除Key =  Serializer.Instance.Deserialize<List<TKey>>(Reader);
        var List追加Value =Serializer.Instance.Deserialize<List<TValue>>(Reader);
        var Count = this.Count;
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
        this._Count=Count+List追加Value.Count;
    }
    public void BinaryWrite(BinaryWriter Writer) {
        Writer.Write(this.Count);
        foreach(var a in this) {
            a.BinaryWrite(Writer);
        }
    }
    public void BinaryRead(BinaryReader Reader,Func<TValue>Create) {
        for(var a= Reader.ReadInt64();a>0;a--) {
            var Value = Create();
            if(!this.InternalAdd(Value)) {
                throw new Exception();
            }
        }
    }
    public void TextWrite(IndentedTextWriter Writer) => throw new NotImplementedException();
    public void TextRead(StreamReader Reader,int Indent) => throw new NotImplementedException();
}