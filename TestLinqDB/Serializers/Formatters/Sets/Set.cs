using System.Reflection;
using S = LinqDB.Sets;
using E = System.Collections.Generic;
using Q = System.Linq;
using LinqDB.Sets;
//using Microsoft.SqlServer.Dac.Deployment;
//using System.Diagnostics;
//using System.Runtime.Serialization;

//using static Microsoft.FSharp.Core.ByRefKinds;

namespace TestLinqDB.Serializers.Formatters.Sets;
public class Set1:共通{
    [Fact]
    public void WriteNullable(){
        this.MemoryMessageJson_Assert(new{a=new LinqDB.Sets.Set<Tables.Table>{new(1)}});
        this.MemoryMessageJson_Assert(new{a=default(LinqDB.Sets.Set<Tables.Table>)});
    }
    [Fact]
    public void ReadNullable(){
        //if(typeof(Sets.Set<T>)==type){
        this.MemoryMessageJson_Assert(new{a=new LinqDB.Sets.Set<Tables.Table>{new(1)}});
        //}else{
        //this.MemoryMessageJson_Assert(new { a = new LinqDB.Sets.GroupingSet<int,int>(1)});
    }
}
//[Serializable,MessagePack.MessagePackObject(true)]
//public class Set2<TValue, TKey>:Set<TValue>
//    where TValue:IPrimaryKey<TKey>
//    where TKey : struct, IEquatable<TKey>{
//    public Set2() { }
//    /// <summary>
//    ///   <see cref="Set2{TValue,TKey}" /> クラスの新しいインスタンスを初期化します。このセット型には既定の等値比較子が使用されます。指定されたコレクションからコピーされた要素が格納され、コピー対象の要素数を格納できるだけの十分な容量が確保されます。</summary>
//    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="IEqualityComparer{TValue}" /> の実装。</param>
//    public Set2(IEqualityComparer<TValue> Comparer) : base(Comparer) {
//    }
//    /// <summary>
//    ///   <see cref="Set2{TValue,TKey}" /> クラスの新しいインスタンスを初期化します。このセット型には既定の等値比較子が使用されます。指定されたコレクションからコピーされた要素が格納されますます。</summary>
//    /// <param name="source">新しいセットの要素のコピー元となるコレクション。</param>
//    public Set2(System.Collections.Generic.IEnumerable<TValue> source) : base(source) {
//    }
//    /// <summary>
//    ///   <see cref="Set2{TValue,TKey}" /> クラスの新しいインスタンスを初期化します。指定されたコレクションからコピーされた要素が格納されます。</summary>
//    /// <param name="source">新しいセットの要素のコピー元となるコレクション。</param>
//    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="IEqualityComparer{TValue}" /> の実装。</param>
//    public Set2(System.Collections.Generic.IEnumerable<TValue> source,IEqualityComparer<TValue> Comparer) : base(source,Comparer) {
//    }
//    /// <summary>
//    ///   <see cref="Set2{TValue,TKey}" /> クラスの新しいインスタンスを初期化します。指定されたコレクションからコピーされた要素が格納されます。</summary>
//    /// <param name="source">新しいセットの要素のコピー元となる配列。</param>
//    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="IEqualityComparer{TValue}" /> の実装。</param>
//    public Set2(TValue[] source,IEqualityComparer<TValue> Comparer) : base(source,Comparer) {
//    }
//    /// <summary>
//    ///   <see cref="Set2{TValue,TKey}" /> クラスの新しいインスタンスを初期化します。指定されたコレクションからコピーされた要素が格納されます。</summary>
//    /// <param name="source">新しいセットの要素のコピー元となる配列。</param>
//    /// <param name="Comparer">セット内の値を比較する際に使用する <see cref="IEqualityComparer{TValue}" /> の実装。</param>
//    public Set2(ImmutableSet<TValue> source,IEqualityComparer<TValue> Comparer) : base(source,Comparer) {
//    }
//    /// <summary>
//    /// キーから値を取得する。
//    /// </summary>
//    /// <param name="key"></param>
//    /// <exception cref="KeyNotFoundException"></exception>
//    [IgnoreDataMember]
//    public TValue this[TKey key] {
//        get {
//            TValue value = default!;
//            if(this.TryGetValue(key,ref value)) {
//                return value;
//            }
//            throw new KeyNotFoundException();
//        }
//    }
//    /// <summary>
//    /// 集合からキーに一致する値を削除する。
//    /// </summary>
//    /// <param name="Key"></param>
//    /// <returns></returns>
//    public bool RemoveKey(TKey Key) {
//        return false;
//    }
//    /// <summary>
//    /// キーから値を取得する。
//    /// </summary>
//    /// <param name="Key"></param>
//    /// <param name="Value"></param>
//    /// <returns>値が取得出来たか</returns>
//    public bool TryGetValue(TKey Key,ref TValue Value) {
//        return false;
//    }
//    /// <summary>
//    /// キーから集合を取得する。
//    /// </summary>
//    /// <param name="Key"></param>
//    /// <returns>値が取得出来たか</returns>
//    public System.Collections.Generic.IEnumerable<TValue> GetSet(TKey Key) {
//        return EmptySet;
//    }
//    /// <summary>
//    /// キーに一致する値を削除する。
//    /// </summary>
//    /// <param name="Key"></param>
//    /// <returns></returns>
//    public bool ContainsKey(TKey Key) {
//        return false;
//    }
//}
public class Set2:共通 {
    [Fact]
    public void WriteNullable(){
        //var input = new { a = new Set<Keys.Key,Tables.Table> { new(1) } };
        //this.Utf8Json_Assert(input,actual => Assert.Equal(input,actual,this.Comparer));
        this.MemoryMessageJson_Assert(new { a = new LinqDB.Sets.Set<Keys.Key,Tables.Table> { new(1) } });
        this.MemoryMessageJson_Assert(new { a = default(LinqDB.Sets.Set<Keys.Key,Tables.Table>) });
    }
    [Fact]
    public void ReadNullable() {
        //if(typeof(Sets.Set<T>)==type){
        this.MemoryMessageJson_Assert(new { a = new LinqDB.Sets.Set<int> { 1 } });
        //}else{
        //this.MemoryMessageJson_Assert(new { a = new LinqDB.Sets.GroupingSet<int,int>(1)});
    }
}
public class Set3:共通 {
    [Fact]
    public void WriteNullable(){
        var C=new LinqDB.Databases.Container();
        this.MemoryMessageJson_Assert(new { a = new Tables.Table(1)});
        //var input = new { a = new Set<Keys.Key,Tables.Table> { new(1) } };
        //this.Utf8Json_Assert(input,actual => Assert.Equal(input,actual,this.Comparer));
        this.MemoryMessageJson_Assert(new LinqDB.Sets.Set<Keys.Key,Tables.Table,LinqDB.Databases.Container>(C) { new(1) } );
        this.MemoryMessageJson_Assert(new { a = new LinqDB.Sets.Set<Keys.Key,Tables.Table,LinqDB.Databases.Container>(C) { new(1) } });
        this.MemoryMessageJson_Assert(new { a = default(LinqDB.Sets.Set<Keys.Key,Tables.Table,LinqDB.Databases.Container>) });
    }
    [Fact]
    public void ReadNullable() {
        //if(typeof(Sets.Set<T>)==type){
        this.MemoryMessageJson_Assert(new { a = new LinqDB.Sets.Set<int> { 1 } });
        //}else{
        //this.MemoryMessageJson_Assert(new { a = new LinqDB.Sets.GroupingSet<int,int>(1)});
    }
}
public class SetGroupingSet:共通 {
    [Fact]public void WriteNullable0(){
        var s=new LinqDB.Sets.Set<Tables.Table>{new(1),new(2)};
        var input=s.GroupBy(p=>p);
        this.MemoryMessageJson_Assert(input, actual => Assert.Equal(input, actual, this.Comparer));
    }
    [Fact]public void WriteNullable1(){
        var s=new S.Set<Tables.Table>{new(1),new(2)};

        var input=s.GroupBy(p=>new{p});
        this.MemoryMessageJson_Assert(input, actual => Assert.Equal(input, actual, this.Comparer));
    }
    [Fact]public void WriteNullable20(){
        //var C=new LinqDB.Databases.Container();
        var s=new S.Set<Tables.Table>{new(1),new(2)};
        var GroupBy=s.GroupBy(p=>new{p});
        var input=(S.IEnumerable)GroupBy;
        this.MemoryMessageJson_Assert(input, actual => Assert.Equal(input, actual, this.Comparer));
    }
    [Fact]public void WriteNullable21(){
        var s=new S.Set<Tables.Table>{new(1),new(2)};
        var GroupBy=s.GroupBy(p=>new{p});
        var input=new{a=GroupBy,b=(S.IEnumerable)GroupBy,c=(object)GroupBy};
        this.MemoryMessageJson_Assert(input, actual => Assert.Equal(input, actual, this.Comparer));
    }
    [Fact]public void WriteNullable30() {
        var s=new S.Set<int>{1,2,3,4,5,6};
        var GroupBy=s.GroupBy(p=>p/2);
        var input=(E.IEnumerable<Q.IGrouping<int,int>>)GroupBy;
        this.MemoryMessageJson_Assert(input, actual => Assert.Equal(input, actual, this.Comparer));
    }
    [Fact]public void WriteNullable31() {
        var s=new S.Set<int>{1,2,3,4,5,6};
        var GroupBy=s.GroupBy(p=>p/2);
        var input=(S.IEnumerable<S.IGrouping<int,int>>)GroupBy;
        this.MemoryMessageJson_Assert(input, actual => Assert.Equal(input, actual, this.Comparer));
    }
    [Fact]public void WriteNullable32() {
        var s=new S.Set<int>{1,2,3,4,5,6};
        var GroupBy=s.GroupBy(p=>p/2);
        var input=GroupBy;
        this.MemoryMessageJson_Assert(input, actual => Assert.Equal(input, actual, this.Comparer));
    }
    [Fact]public void WriteNullable33() {
        var s=new S.Set<int>{1,2,3,4,5,6};
        var GroupBy=s.GroupBy(p=>p/2);
        var input=new{a=GroupBy};
        this.MemoryMessageJson_Assert(input, actual => Assert.Equal(input, actual, this.Comparer));
    }
    [Fact]public void WriteNullable34() {
        var s=new S.Set<int>{1,2,3,4,5,6};
        var GroupBy=s.GroupBy(p=>p/2);
        var input=new{b=(E.IEnumerable<Q.IGrouping<int,int>>)GroupBy};
        this.MemoryMessageJson_Assert(input, actual => Assert.Equal(input, actual, this.Comparer));
    }
    [Fact]public void WriteNullable35() {
        var s=new S.Set<int>{1,2,3,4,5,6};
        var GroupBy=s.GroupBy(p=>p/2);
        var input=new{c=(S.IEnumerable<S.IGrouping<int,int>>)GroupBy};
        this.MemoryMessageJson_Assert(input, actual => Assert.Equal(input, actual, this.Comparer));
    }
    [Fact]public void WriteNullable36() {
        var s=new S.Set<int>{1,2,3,4,5,6};
        var GroupBy=s.GroupBy(p=>p/2);
        var input=new{a=GroupBy,b=(E.IEnumerable<Q.IGrouping<int,int>>)GroupBy,c=(S.IEnumerable<S.IGrouping<int,int>>)GroupBy};
        this.MemoryMessageJson_Assert(input, actual => Assert.Equal(input, actual, this.Comparer));
    }
}
