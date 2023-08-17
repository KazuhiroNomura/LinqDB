//using static BackendClient.ExtendAggregate;
using System.Linq.Expressions;
using CoverageCS.LinqDB.Sets;
using LinqDB.Databases;
using LinqDB.Helpers;
using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static LinqDB.Sets.ExtensionSet;
// ReSharper disable InvertIf
// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable ConvertIfStatementToReturnStatement
// ReSharper disable RedundantCast
// ReSharper disable UseCollectionCountProperty
// ReSharper disable RedundantExplicitArrayCreation
// ReSharper disable UnusedTypeParameter
// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable ReplaceWithSingleCallToAny

namespace CoverageCS.LinqDB.Optimizers;

public class Container2:Container<Container2> {
    protected override void Commit(Stream LogStream) => throw new NotImplementedException();
    public override Container2 Transaction() => throw new NotImplementedException();
}
[TestClass]
public class Test_変換_インラインループ:ATest {
    [TestMethod]
    public void Aggregate_func() {
        this.Execute引数パターン(a => ArrN<int>(a+1).Aggregate((x,y) => x+y));
        this.Execute引数パターン(a => EnuN<int>(a+1).Aggregate((x,y) => x+y));
        this.Execute引数パターン(a => SetN<int>(a+1).Aggregate((x,y) => x+y));
    }
    [TestMethod]
    public void Aggregate_seed_func() {
        this.Execute引数パターン(a => ArrN<int>(a).Aggregate(1,(x,y) => x+y));
        this.Execute引数パターン(a => EnuN<int>(a).Aggregate(1,(x,y) => x+y));
        this.Execute引数パターン(a => SetN<int>(a).Aggregate(1,(x,y) => x+y));
    }
    [TestMethod]
    public void Aggregate_seed_func_resultSelector() {
        this.Execute引数パターン(a => ArrN<int>(a).Aggregate(1,(x,y) => x+y,accumulate => accumulate*2));
        this.Execute引数パターン(a => EnuN<int>(a).Aggregate(1,(x,y) => x+y,accumulate => accumulate*2));
        this.Execute引数パターン(a => SetN<int>(a).Aggregate(1,(x,y) => x+y,accumulate => accumulate*2));
    }
    [TestMethod]
    public void All() {
        this.Execute引数パターン(a => ArrN<int>(a).All(p => p==1));
        this.Execute引数パターン(a => EnuN<int>(a).All(p => p==1));
        this.Execute引数パターン(a => SetN<int>(a).All(p => p==1));
    }
    [TestMethod]
    public void Any() {
        //if(MethodCall0_Arguments_0 is MethodCallExpression MethodCall0_MethodCall) {
        //    if((Setか = Reflection.ExtendSet.Except == MethodCall0_MethodCall_GenericMethodDefinition) ||
        this.Except_Any();
        //    }
        //    if((Setか = Reflection.ExtendSet.Intersect0 == MethodCall0_MethodCall_GenericMethodDefinition) ||
        //        Reflection.ExtendEnumerable.Intersect0 == MethodCall0_MethodCall_GenericMethodDefinition) {
        this.Intersect_Any();
        //    }
        //    if((Setか = Reflection.ExtendSet.Dictionry_Join == MethodCall0_MethodCall_GenericMethodDefinition) ||
        //        Reflection.ExtendEnumerable.Dictionry_Join == MethodCall0_MethodCall_GenericMethodDefinition) {
        this.Join_Dictionary_Any();
        //    }
        //    if(Reflection.ExtendSet.Dictionary_Equal == MethodCall0_MethodCall_GenericMethodDefinition ||
        //        Reflection.ExtendEnumerable.Dictionary_Equal == MethodCall0_MethodCall_GenericMethodDefinition) {
        this.Dictionary_Equal_Any();
        //    }
        //    if(Reflection.ExtendSet.OfType == MethodCall0_MethodCall_GenericMethodDefinition ||
        //        Reflection.ExtendEnumerable.OfType == MethodCall0_MethodCall_GenericMethodDefinition) {
        this.OfType_Any();
        //    }
        //    if(Reflection.ExtendSet.SelectMany_selector == MethodCall0_MethodCall_GenericMethodDefinition ||
        //        Reflection.ExtendEnumerable.SelectMany_selector == MethodCall0_MethodCall_GenericMethodDefinition) {
        this.SelectMany_selector_Any();
        //    }
        //    if(Reflection.ExtendSet.Union == MethodCall0_MethodCall_GenericMethodDefinition ||
        //        Reflection.ExtendEnumerable.Union0 == MethodCall0_MethodCall_GenericMethodDefinition) {
        this.Union_Any();
        //    }
        //    if(Reflection.ExtendSet.Where == MethodCall0_MethodCall_GenericMethodDefinition ||
        //        Reflection.ExtendEnumerable.Where == MethodCall0_MethodCall_GenericMethodDefinition) {
        this.Where_Any();
        //    }
        this.Execute引数パターン(a => ArrN<int>(a).Let(p => p).Any());
        this.Execute引数パターン(a => EnuN<int>(a).Let(p => p).Any());
        this.Execute引数パターン(a => SetN<int>(a).Let(p => p).Any());
        //}
        this.Execute引数パターン(a => ArrN<int>(a).Any());
        this.Execute引数パターン(a => EnuN<int>(a).Any());
        this.Execute引数パターン(a => SetN<int>(a).Any());
        //}
    }
    /// <summary>
    /// カバレッジではない
    /// </summary>
    [TestMethod]
    public void AsEnumerable() {
        this.Execute引数パターン(a => ArrN<int>(a).AsEnumerable());
        this.Execute引数パターン(a => EnuN<int>(a).AsEnumerable());
        this.Execute引数パターン(a => SetN<int>(a).AsEnumerable());
    }
    [TestMethod]
    public void Avedev_selector() => this.Execute引数パターン(a => SetN<int>(a+1).Avedev(p => p+1));
    [TestMethod]
    public void Enumerableメソッドで結果にListを要求するか() {
        //if(MethodCall!=null) {
        //    if(Reflection.ExtendEnumerable.GroupBy_keySelector==MethodCall_GenericMethodDefinition) return true;
        this.Execute引数パターン(a => ArrN<int>(a).GroupBy(p => p));
        this.Execute引数パターン(a => EnuN<int>(a).GroupBy(p => p));
        this.Execute引数パターン(a => SetN<int>(a).GroupBy(p => p));
        //    if(Reflection.ExtendEnumerable.GroupBy_keySelector_comparer==MethodCall_GenericMethodDefinition) return true;
        this.Execute引数パターン(a => ArrN<int>(a).GroupBy(p => p,EqualityComparer<int>.Default));
        this.Execute引数パターン(a => EnuN<int>(a).GroupBy(p => p,EqualityComparer<int>.Default));
        this.Execute引数パターン(a => SetN<int>(a).GroupBy(p => p,EqualityComparer<int>.Default));
        //    if(Reflection.ExtendEnumerable.GroupBy_keySelector_elementSelector==MethodCall_GenericMethodDefinition) return true;
        this.Execute引数パターン(a => ArrN<int>(a).GroupBy(p => p,p => p));
        this.Execute引数パターン(a => EnuN<int>(a).GroupBy(p => p,p => p));
        this.Execute引数パターン(a => SetN<int>(a).GroupBy(p => p,p => p));
        //    if(Reflection.ExtendEnumerable.GroupBy_keySelector_elementSelector_comparer==MethodCall_GenericMethodDefinition) return true;
        this.Execute引数パターン(a => ArrN<int>(a).GroupBy(p => p,p => p,EqualityComparer<int>.Default));
        this.Execute引数パターン(a => EnuN<int>(a).GroupBy(p => p,p => p,EqualityComparer<int>.Default));
        this.Execute引数パターン(a => SetN<int>(a).GroupBy(p => p,p => p,EqualityComparer<int>.Default));
        //    if(Reflection.ExtendEnumerable.GroupBy_keySelector_resultSelector==MethodCall_GenericMethodDefinition) return true;
        this.Execute引数パターン(a => ArrN<int>(a).GroupBy(p => p,(p,q) => new { p,q }));
        this.Execute引数パターン(a => EnuN<int>(a).GroupBy(p => p,(p,q) => new { p,q }));
        this.Execute引数パターン(a => SetN<int>(a).GroupBy(p => p,(p,q) => new { p,q }));
        //    if(Reflection.ExtendEnumerable.GroupBy_keySelector_resultSelector_comparer==MethodCall_GenericMethodDefinition) return true;
        this.Execute引数パターン(a => ArrN<int>(a).GroupBy(p => p,(p,q) => new { p,q },EqualityComparer<int>.Default));
        this.Execute引数パターン(a => EnuN<int>(a).GroupBy(p => p,(p,q) => new { p,q },EqualityComparer<int>.Default));
        this.Execute引数パターン(a => SetN<int>(a).GroupBy(p => p,(p,q) => new { p,q },EqualityComparer<int>.Default));
        //    if(Reflection.ExtendEnumerable.GroupBy_keySelector_elementSelector_resultSelector==MethodCall_GenericMethodDefinition) return true;
        this.Execute引数パターン(a => ArrN<int>(a).GroupBy(p => p,p => p,(p,q) => new { p,q }));
        this.Execute引数パターン(a => EnuN<int>(a).GroupBy(p => p,p => p,(p,q) => new { p,q }));
        this.Execute引数パターン(a => SetN<int>(a).GroupBy(p => p,p => p,(p,q) => new { p,q }));
        //    if(Reflection.ExtendEnumerable.GroupBy_keySelector_elementSelector_resultSelector_comparer==MethodCall_GenericMethodDefinition) return true;
        this.Execute引数パターン(a => ArrN<int>(a).GroupBy(p => p,p => p,(p,q) => new { p,q },EqualityComparer<int>.Default));
        this.Execute引数パターン(a => EnuN<int>(a).GroupBy(p => p,p => p,(p,q) => new { p,q },EqualityComparer<int>.Default));
        this.Execute引数パターン(a => SetN<int>(a).GroupBy(p => p,p => p,(p,q) => new { p,q },EqualityComparer<int>.Default));
        //    if(Reflection.ExtendEnumerable.SelectMany_selector==MethodCall_GenericMethodDefinition) return true;
        this.Execute引数パターン(a => ArrN<int>(a).SelectMany(p => ArrN<int>(a)));
        this.Execute引数パターン(a => EnuN<int>(a).SelectMany(p => ArrN<int>(a)));
        this.Execute引数パターン(a => SetN<int>(a).SelectMany(p => ArrN<int>(a)));
        //    if(Reflection.ExtendEnumerable.SelectMany_indexSelector==MethodCall_GenericMethodDefinition) return true;
        this.Execute引数パターン(a => ArrN<int>(a).SelectMany((p,i) => ArrN<int>(a)));
        this.Execute引数パターン(a => EnuN<int>(a).SelectMany((p,i) => ArrN<int>(a)));
        this.Execute引数パターン(a => SetN<int>(a).SelectMany((p,i) => ArrN<int>(a)));
        //    if(Reflection.ExtendEnumerable.Union0==MethodCall_GenericMethodDefinition) return false;
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Union(ArrN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Union(EnuN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Union(SetN<int>(b)));
        //    if(
        //        Reflection.ExtendEnumerable.Where==MethodCall_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.Except==MethodCall_GenericMethodDefinition
        //    ) {
        this.Execute引数パターン(a => ArrN<int>(a).Where(p => true));
        this.Execute引数パターン(a => EnuN<int>(a).Where(p => true));
        this.Execute引数パターン(a => SetN<int>(a).Where(p => true));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Except(ArrN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Except(EnuN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Except(SetN<int>(b)));
        //    }
        //    if(Reflection.ExtendEnumerable.GroupJoin0==MethodCall_GenericMethodDefinition) {
        //        var resultSelector = MethodCall.Arguments[4]as LambdaExpression;
        //        if(resultSelector==null) return false;
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).GroupJoin(ArrN<int>(b),o => o,i => i,(o,i) => o));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).GroupJoin(EnuN<int>(b),o => o,i => i,(o,i) => o));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).GroupJoin(SetN<int>(b),o => o,i => i,(o,i) => o));
        //    }
        //    if(Reflection.ExtendEnumerable.Intersect0==MethodCall_GenericMethodDefinition){
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Intersect(ArrN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Intersect(EnuN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Intersect(SetN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b,c) => ArrN<int>(a).Union(ArrN<int>(b)).Intersect(ArrN<int>(c)));
        this.Execute引数パターン標準ラムダループ((a,b,c) => EnuN<int>(a).Union(EnuN<int>(b)).Intersect(EnuN<int>(c)));
        this.Execute引数パターン標準ラムダループ((a,b,c) => SetN<int>(a).Union(SetN<int>(b)).Intersect(SetN<int>(c)));
        this.Execute引数パターン標準ラムダループ((a,b,c) => ArrN<int>(a).Intersect(ArrN<int>(b+1).Union(ArrN<int>(c+2))));
        this.Execute引数パターン標準ラムダループ((a,b,c) => ArrN<int>(a).Intersect(ArrN<int>(b).Union(ArrN<int>(c))));
        this.Execute引数パターン標準ラムダループ((a,b,c) => EnuN<int>(a).Intersect(EnuN<int>(b).Union(EnuN<int>(c))));
        this.Execute引数パターン標準ラムダループ((a,b,c) => SetN<int>(a).Intersect(SetN<int>(b).Union(SetN<int>(c))));
        this.Execute引数パターン標準ラムダループ((a,b,c,d) => ArrN<int>(a).Union(ArrN<int>(b)).Intersect(ArrN<int>(c).Union(ArrN<int>(d))));
        this.Execute引数パターン標準ラムダループ((a,b,c,d) => EnuN<int>(a).Union(EnuN<int>(b)).Intersect(EnuN<int>(c).Union(EnuN<int>(d))));
        this.Execute引数パターン標準ラムダループ((a,b,c,d) => SetN<int>(a).Union(SetN<int>(b)).Intersect(SetN<int>(c).Union(SetN<int>(d))));
        //    }
        //    if(Reflection.ExtendEnumerable.Join==MethodCall_GenericMethodDefinition) {
        //        if(resultSelector==null) return false;
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => o+i));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Join(EnuN<int>(b),o => o,i => i,(o,i) => o+i));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => o+i));
        //        if(!this.MethodCallの戻り値が重複除去されているのでListでよいか(MethodCall.Arguments[0])) return false;
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Select(p => p+1).Join(ArrN<int>(b),o => o,i => i,(o,i) => o+i));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Select(p => p+1).Join(EnuN<int>(b),o => o,i => i,(o,i) => o+i));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Select(p => p+1).Join(SetN<int>(b),o => o,i => i,(o,i) => o+i));
        //        if(!this.MethodCallの戻り値が重複除去されているのでListでよいか(MethodCall.Arguments[1])) return false;
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b).Select(p => p+1),o => o,i => i,(o,i) => o+i));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Join(EnuN<int>(b).Select(p => p+1),o => o,i => i,(o,i) => o+i));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b).Select(p => p+1),o => o,i => i,(o,i) => o+i));
        //        if(!this._変数_判定_指定PrimaryKeyが存在する.実行(resultSelector.Body,resultSelector.Parameters[0])) return false;
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => new { i }));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Join(EnuN<int>(b),o => o,i => i,(o,i) => new { i }));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => new { i }));
        //        if(!this._変数_判定_指定PrimaryKeyが存在する.実行(resultSelector.Body,resultSelector.Parameters[1])) return false;
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => new { o }));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Join(EnuN<int>(b),o => o,i => i,(o,i) => new { o }));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => new { o }));
        //        return true;
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => new { o,i }));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Join(EnuN<int>(b),o => o,i => i,(o,i) => new { o,i }));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => new { o,i }));
        //    }
        //    if(
        //        Reflection.ExtendEnumerable.Select_selector==MethodCall_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.Select_indexSelector==MethodCall_GenericMethodDefinition
        //    ) {
        //        if(selector==null) return false;
        this.Execute引数パターン(a => ArrN<int>(a).Select(p => p+1));
        this.Execute引数パターン(a => EnuN<int>(a).Select(p => p+1));
        this.Execute引数パターン(a => SetN<int>(a).Select(p => p+1));
        //    }
        this.Execute引数パターン(a => ArrN<int>(a));
        this.Execute引数パターン(a => EnuN<int>(a));
        this.Execute引数パターン(a => SetN<int>(a));
        //}
        {
            var a = ArrN<int>(10);
            this.Execute標準ラムダループ(() => a);
        }
        //return true;
    }
    private interface I {
    }
    private class A:I {
    }
    private class B:A {
    }
    /// <summary>
    /// publicでないとS?がコンパイルできない
    /// </summary>
    public struct S:I {
        private readonly int v;
        public S(int v) => this.v=v;
    }
    [TestMethod]
    public void Cast() {
        {
            this.Execute標準ラムダループ(() => (I)(I)(I)new S(3));
            this.Execute標準ラムダループ(() => (I)(I)(S)new S(3));
            this.Execute標準ラムダループ(() => (I)(I)(S?)new S(3));
            this.Execute標準ラムダループ(() => (I)(S)(I)new S(3));
            this.Execute標準ラムダループ(() => (I)(S)(S)new S(3));
            this.Execute標準ラムダループ(() => (I)(S)(S?)new S(3));
            this.Execute標準ラムダループ(() => (I)(S?)(I)new S(3));
            this.Execute標準ラムダループ(() => (I)(S?)(S)new S(3));
            this.Execute標準ラムダループ(() => (I)(S?)(S?)new S(3));
            this.Execute標準ラムダループ(() => (S)(I)(I)new S(3));
            this.Execute標準ラムダループ(() => (S)(I)(S)new S(3));
            this.Execute標準ラムダループ(() => (S)(I)(S?)new S(3));
            this.Execute標準ラムダループ(() => (S)(S)(I)new S(3));
            this.Execute標準ラムダループ(() => (S)(S)(S)new S(3));
            this.Execute標準ラムダループ(() => (S)(S)(S?)new S(3));
            this.Execute標準ラムダループ(() => (S)(S?)(I)new S(3));
            this.Execute標準ラムダループ(() => (S)(S?)(S)new S(3));
            this.Execute標準ラムダループ(() => (S)(S?)(S?)new S(3));
            this.Execute標準ラムダループ(() => (S?)(I)(I)new S(3));
            this.Execute標準ラムダループ(() => (S?)(I)(S)new S(3));
            this.Execute標準ラムダループ(() => (S?)(I)(S?)new S(3));
            this.Execute標準ラムダループ(() => (S?)(S)(I)new S(3));
            this.Execute標準ラムダループ(() => (S?)(S)(S)new S(3));
            this.Execute標準ラムダループ(() => (S?)(S)(S?)new S(3));
            this.Execute標準ラムダループ(() => (S?)(S?)(I)new S(3));
            this.Execute標準ラムダループ(() => (S?)(S?)(S)new S(3));
            this.Execute標準ラムダループ(() => (S?)(S?)(S?)new S(3));

            this.Execute標準ラムダループ(() => (object)(object)(object)new S(3));
            this.Execute標準ラムダループ(() => (object)(object)(S)new S(3));
            this.Execute標準ラムダループ(() => (object)(object)(S?)new S(3));
            this.Execute標準ラムダループ(() => (object)(S)(object)new S(3));
            this.Execute標準ラムダループ(() => (object)(S)(S)new S(3));
            this.Execute標準ラムダループ(() => (object)(S)(S?)new S(3));
            this.Execute標準ラムダループ(() => (object)(S?)(object)new S(3));
            this.Execute標準ラムダループ(() => (object)(S?)(S)new S(3));
            this.Execute標準ラムダループ(() => (object)(S?)(S?)new S(3));
            this.Execute標準ラムダループ(() => (S)(object)(object)new S(3));
            this.Execute標準ラムダループ(() => (S)(object)(S)new S(3));
            this.Execute標準ラムダループ(() => (S)(object)(S?)new S(3));
            this.Execute標準ラムダループ(() => (S)(S)(object)new S(3));
            this.Execute標準ラムダループ(() => (S)(S)(S)new S(3));
            this.Execute標準ラムダループ(() => (S)(S)(S?)new S(3));
            this.Execute標準ラムダループ(() => (S)(S?)(object)new S(3));
            this.Execute標準ラムダループ(() => (S)(S?)(S)new S(3));
            this.Execute標準ラムダループ(() => (S)(S?)(S?)new S(3));
            this.Execute標準ラムダループ(() => (S?)(object)(object)new S(3));
            this.Execute標準ラムダループ(() => (S?)(object)(S)new S(3));
            this.Execute標準ラムダループ(() => (S?)(object)(S?)new S(3));
            this.Execute標準ラムダループ(() => (S?)(S)(object)new S(3));
            this.Execute標準ラムダループ(() => (S?)(S)(S)new S(3));
            this.Execute標準ラムダループ(() => (S?)(S)(S?)new S(3));
            this.Execute標準ラムダループ(() => (S?)(S?)(object)new S(3));
            this.Execute標準ラムダループ(() => (S?)(S?)(S)new S(3));
            this.Execute標準ラムダループ(() => (S?)(S?)(S?)new S(3));

            this.Execute標準ラムダループ(() => (object?)(object?)(object?)default(S?));
            this.Execute標準ラムダループ(() => (object?)(object?)(S?)default);
            this.Execute標準ラムダループ(() => (object?)(S?)default);
            this.Execute標準ラムダループ(() => (object?)(S?)(object?)default(S?));
            this.Execute標準ラムダループ(() => (object?)(S?)default);
            this.Execute標準ラムダループ(() => (S?)(object?)default(S?));
            this.Execute標準ラムダループ(() => (S?)(object?)(S?)default);
            var S = new S(3);
            S? NS = S;
            object ONS = NS;
            object OS = S;
            var expected00 = (S?)S;
            var expected01 = (S?)NS;
            var expected02 = (S?)ONS;
            var expected03 = (S?)OS;
            var expected04 = (object)S;
            var expected05 = (object)NS;
            var expected06 = (object)ONS;
            var expected07 = (object)OS;
            var expected08 = (S)S;
            var expected09 = (S)NS;
            var expected10 = (S)ONS;
            var expected11 = (S)OS;
            var actual00 = this.Execute2(() => (S?)S);
            var actual01 = this.Execute2(() => (S?)NS);
            var actual02 = this.Execute2(() => (S?)ONS);
            var actual03 = this.Execute2(() => (S?)OS);
            var actual04 = this.Execute2(() => (object)S);
            var actual05 = this.Execute2(() => (object)NS);
            var actual06 = this.Execute2(() => (object)ONS);
            var actual07 = this.Execute2(() => (object)OS);
            var actual08 = this.Execute2(() => (S)S);
            var actual09 = this.Execute2(() => (S)NS);
            var actual10 = this.Execute2(() => (S)ONS);
            var actual11 = this.Execute2(() => (S)OS);
            Assert.IsTrue(Comparer.Equals(expected00,actual00));
            Assert.IsTrue(Comparer.Equals(expected01,actual01));
            Assert.IsTrue(Comparer.Equals(expected02,actual02));
            Assert.IsTrue(Comparer.Equals(expected03,actual03));
            Assert.IsTrue(Comparer.Equals(expected04,actual04));
            Assert.IsTrue(Comparer.Equals(expected05,actual05));
            Assert.IsTrue(Comparer.Equals(expected06,actual06));
            Assert.IsTrue(Comparer.Equals(expected07,actual07));
            Assert.IsTrue(Comparer.Equals(expected08,actual08));
            Assert.IsTrue(Comparer.Equals(expected09,actual09));
            Assert.IsTrue(Comparer.Equals(expected10,actual10));
            Assert.IsTrue(Comparer.Equals(expected11,actual11));
        }
        {
            object[] array1 = { (int?)1,(int?)null,2 };
            //var expected1 = array1.Cast<Int32>().ToArray();
            var expected2 = array1.Cast<int?>().ToArray();
            //  var actual1 = this.AssertExecute(() => array1.Cast<Int32>());
            var actual2 = this.Execute2(() => array1.Cast<int?>());
            //  Assert.IsTrue(EnumerableSetEqualityComparer.Equals(expected1,actual1));
            Assert.IsTrue(Comparer.Equals(expected2,actual2));
        }
        {
            object[] array1 = { "A",null,"B" };
            var expected1 = array1.Cast<string>().ToArray();
            var actual1 = this.Execute2(() => array1.Cast<string>());
            Assert.IsTrue(Comparer.Equals(expected1,actual1));
        }
        this.Execute引数パターン(a => ArrN<int>(a).Cast<object>());
        this.Execute引数パターン(a => EnuN<int>(a).Cast<object>());
        this.Execute引数パターン(a => SetN<int>(a).Cast<object>());
        var IArrN = new I[] { new A(),new B() };
        this.Execute標準ラムダループ(() => IArrN.Cast<object>());
        this.Execute標準ラムダループ(() => IArrN.Cast<I>());
        this.Execute標準ラムダループ(() => IArrN.Cast<A>());
        var IEnuN = IArrN.AsEnumerable();
        this.Execute標準ラムダループ(() => IEnuN.Cast<object>());
        this.Execute標準ラムダループ(() => IEnuN.Cast<I>());
        this.Execute標準ラムダループ(() => IEnuN.Cast<A>());
        var ISetN = new Set<I>(IArrN);
        this.Execute標準ラムダループ(() => ISetN.Cast<object>());
        this.Execute標準ラムダループ(() => ISetN.Cast<I>());
        this.Execute標準ラムダループ(() => ISetN.Cast<A>());
    }

    [TestMethod] public void Dictionary() => this.Dictionary_Equal();

    [TestMethod]
    public void Dictionary_Equal() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => o+i,EqualityComparer<int>.Default));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Join(EnuN<int>(b),o => o,i => i,(o,i) => o+i,EqualityComparer<int>.Default));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => o+i,EqualityComparer<int>.Default));
        this.Execute引数パターン(a => Lambda(b => ArrN<int>(a).Where((Func<int,bool>)(c => c==a))));
        this.Execute引数パターン(a => Lambda(b => EnuN<int>(a).Where((Func<int,bool>)(c => c==a))));
        this.Execute引数パターン(a => Lambda(b => SetN<int>(a).Where((Func<int,bool>)(c => c==a))));
        this.Execute引数パターン(a => Lambda(b => ArrN<int>(a).Where(c => c==a)));
        this.Execute引数パターン(a => Lambda(b => EnuN<int>(a).Where(c => c==a)));
        this.Execute引数パターン(a => Lambda(b => SetN<int>(a).Where(c => c==a)));
    }
    [TestMethod]
    public void Dictionary_Equal_Any() {
        this.Execute引数パターン(a => Lambda(b => ArrN<int>(a).Where(c => c==a).Any()));
        this.Execute引数パターン(a => Lambda(b => EnuN<int>(a).Where(c => c==a).Any()));
        this.Execute引数パターン(a => Lambda(b => SetN<int>(a).Where(c => c==a).Any()));
    }
    [TestMethod]
    public void Distinct() {
        this.Execute引数パターン(a => ArrN<int>(a).Distinct());
        this.Execute引数パターン(a => EnuN<int>(a).Distinct());
        this.Execute引数パターン(a => ArrN<int>(a).Distinct(AnonymousComparer.Create((int x,int y) => x==y&&x<3,i => i)));
        this.Execute引数パターン(a => EnuN<int>(a).Distinct(AnonymousComparer.Create((int x,int y) => x==y&&x<3,i => i)));
    }
    [TestMethod]
    public void Except() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Except(ArrN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Except(EnuN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Except(SetN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Select(p => p+1).Except(ArrN<int>(b).Select(p => p+2)));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Select(p => p+1).Except(EnuN<int>(b).Select(p => p+2)));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Select(p => p+1).Except(SetN<int>(b).Select(p => p+2)));
    }
    [TestMethod]
    public void Except_Any() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Select(p => p+1).Except(ArrN<int>(b).Select(p => p+2)).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Select(p => p+1).Except(EnuN<int>(b).Select(p => p+2)).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Select(p => p+1).Except(SetN<int>(b).Select(p => p+2)).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Except(ArrN<int>(b)).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Except(EnuN<int>(b)).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Except(SetN<int>(b)).Any());
    }
    [TestMethod]
    public void GroupBy_keySelector() {
        this.Execute引数パターン(a => ArrN<int>(a).GroupBy(p => p+"Key"));
        this.Execute引数パターン(a => EnuN<int>(a).GroupBy(p => p+"Key"));
        this.Execute引数パターン(a => SetN<int>(a).GroupBy(p => p+"Key"));
    }
    [TestMethod]
    public void GroupBy_keySelector_comparer() {
        this.Execute引数パターン(a => ArrN<int>(a).GroupBy(p => p+"Key",EqualityComparer<string>.Default));
        this.Execute引数パターン(a => EnuN<int>(a).GroupBy(p => p+"Key",EqualityComparer<string>.Default));
        this.Execute引数パターン(a => SetN<int>(a).GroupBy(p => p+"Key",EqualityComparer<string>.Default));
    }
    [TestMethod]
    public void GroupBy_keySelector_elementSelector() {
        this.Execute引数パターン(a => ArrN<int>(a).GroupBy(p => p+"Key",p => p+"Element"));
        this.Execute引数パターン(a => EnuN<int>(a).GroupBy(p => p+"Key",p => p+"Element"));
        this.Execute引数パターン(a => SetN<int>(a).GroupBy(p => p+"Key",p => p+"Element"));
    }
    [TestMethod]
    public void GroupBy_keySelector_elementSelector_comparer() {
        this.Execute引数パターン(a => ArrN<int>(a).GroupBy(p => p+"Key",p => p+"Element",EqualityComparer<string>.Default));
        this.Execute引数パターン(a => EnuN<int>(a).GroupBy(p => p+"Key",p => p+"Element",EqualityComparer<string>.Default));
        this.Execute引数パターン(a => SetN<int>(a).GroupBy(p => p+"Key",p => p+"Element",EqualityComparer<string>.Default));
    }
    [TestMethod]
    public void GroupBy_keySelector_resultSelector0(){
        this.Execute引数パターン(a => ArrN<int>(a).GroupBy(p => p,(v,e) =>v));
        this.Execute引数パターン(a => EnuN<int>(a).GroupBy(p => p,(v,e) => v));
        this.Execute引数パターン(a => SetN<int>(a).GroupBy(p => p,(v,e) =>v));
    }
    [TestMethod]
    public void GroupBy_keySelector_resultSelector1() {
        this.Execute引数パターン(a => ArrN<int>(a).GroupBy(p => p+"Key",(v,e) => new { v,e }));
        this.Execute引数パターン(a => EnuN<int>(a).GroupBy(p => p+"Key",(v,e) => new { v,e }));
        this.Execute引数パターン(a => SetN<int>(a).GroupBy(p => p+"Key",(v,e) => new { v,e }));
    }
    [TestMethod]
    public void GroupBy_keySelector_resultSelector_comparer() {
        this.Execute引数パターン(a => ArrN<int>(a).GroupBy(p => p+"Key",(v,e) => new { v,e },EqualityComparer<string>.Default));
        this.Execute引数パターン(a => EnuN<int>(a).GroupBy(p => p+"Key",(v,e) => new { v,e },EqualityComparer<string>.Default));
        this.Execute引数パターン(a => SetN<int>(a).GroupBy(p => p+"Key",(v,e) => new { v,e },EqualityComparer<string>.Default));
    }
    [TestMethod]
    public void GroupBy_keySelector_elementSelector_resultSelector() {
        this.Execute引数パターン(a => ArrN<int>(a).GroupBy(p => p+"Key",p => p+"Element",(v,e) => new { v,e }));
        this.Execute引数パターン(a => EnuN<int>(a).GroupBy(p => p+"Key",p => p+"Element",(v,e) => new { v,e }));
        this.Execute引数パターン(a => SetN<int>(a).GroupBy(p => p+"Key",p => p+"Element",(v,e) => new { v,e }));
    }
    [TestMethod]
    public void GroupBy_keySelector_elementSelector_resultSelector_comparer() {
        this.Execute引数パターン(a => ArrN<int>(a).GroupBy(p => p+"Key",p => p+"Element",(v,e) => new { v,e }));
        this.Execute引数パターン(a => EnuN<int>(a).GroupBy(p => p+"Key",p => p+"Element",(v,e) => new { v,e }));
        this.Execute引数パターン(a => SetN<int>(a).GroupBy(p => p+"Key",p => p+"Element",(v,e) => new { v,e }));
        this.Execute引数パターン(a => ArrN<int>(a).GroupBy(p => p+"Key",p => p+"Element",(v,e) => new { v,e },EqualityComparer<string>.Default));
        this.Execute引数パターン(a => EnuN<int>(a).GroupBy(p => p+"Key",p => p+"Element",(v,e) => new { v,e },EqualityComparer<string>.Default));
        this.Execute引数パターン(a => SetN<int>(a).GroupBy(p => p+"Key",p => p+"Element",(v,e) => new { v,e },EqualityComparer<string>.Default));
    }
    [TestMethod]
    public void GroupJoin_Dictionary() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).GroupJoin(ArrN<int>(b),o => o,i => i,(o,i) => new { o,i },AnonymousComparer.Create<int>((x,y) => x==y,p => p.GetHashCode())));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).GroupJoin(EnuN<int>(b),o => o,i => i,(o,i) => new { o,i },AnonymousComparer.Create<int>((x,y) => x==y,p => p.GetHashCode())));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).GroupJoin(SetN<int>(b),o => o,i => i,(o,i) => new { o,i },AnonymousComparer.Create<int>((x,y) => x==y,p => p.GetHashCode())));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).GroupJoin(ArrN<int>(b),o => o,i => i,(o,i) => new { o,i }));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).GroupJoin(EnuN<int>(b),o => o,i => i,(o,i) => new { o,i }));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).GroupJoin(SetN<int>(b),o => o,i => i,(o,i) => new { o,i }));
    }
    [TestMethod]
    public void Intersect() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Intersect(ArrN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Intersect(EnuN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Intersect(SetN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Select(p => p+1).Intersect(ArrN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Select(p => p+1).Intersect(EnuN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Select(p => p+1).Intersect(SetN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Select(p => p+1).Intersect(ArrN<int>(b).Select(p => p+2)));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Select(p => p+1).Intersect(EnuN<int>(b).Select(p => p+2)));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Select(p => p+1).Intersect(SetN<int>(b).Select(p => p+2)));
    }
    [TestMethod]
    public void Intersect_Any() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Intersect(ArrN<int>(b)).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Intersect(EnuN<int>(b)).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Intersect(SetN<int>(b)).Any());
    }
    [TestMethod]
    public void Join_Dictionary() {
        //if(inner0.NodeType==ExpressionType.Parameter)
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => o+i));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Join(EnuN<int>(b),o => o,i => i,(o,i) => o+i));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => o+i));
        //else
        this.Execute引数パターン標準ラムダループ((a,b) => "".Let(p => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => o+i)));
        this.Execute引数パターン標準ラムダループ((a,b) => "".Let(p => EnuN<int>(a).Join(EnuN<int>(b),o => o,i => i,(o,i) => o+i)));
        this.Execute引数パターン標準ラムダループ((a,b) => "".Let(p => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => o+i)));

        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => o+i,EqualityComparer<int>.Default));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Join(EnuN<int>(b),o => o,i => i,(o,i) => o+i,EqualityComparer<int>.Default));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => o+i,EqualityComparer<int>.Default));
        this.Execute引数パターン(a => new Set<Tables.Entity,PrimaryKeys.Entity,Container>(new Container2()).Join(SetN<Tables.Entity>(a),o => o,i => i,(o,i) => new { o,i }));
    }
    [TestMethod]
    public void Join_Dictionary1() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => o+i,EqualityComparer<int>.Default));
    }
    [TestMethod]
    public void Join_outerKeySelector() {
        //if(inner0.NodeType==ExpressionType.Parameter)
        this.Execute引数パターン(a => SetN<Tables.Entity>(a).Join(new Set<Tables.Entity,PrimaryKeys.Entity,Container>(new Container2()),o => o.PrimaryKey,i => i.PrimaryKey,(o,i) => new { o,i }));
        //} else
        this.Execute引数パターン(a => SetN<Tables.Entity>(a).Join(new Set<Tables.Entity,PrimaryKeys.Entity,Container>(new Container2()),o => o.PrimaryKey,i => i.PrimaryKey,(o,i) => new { o,i,s = new Set<Tables.Entity,PrimaryKeys.Entity,Container>(new Container2()) }));
    }
    [TestMethod]
    public void Join_innerKeySelector() {
        this.Execute標準ラムダループ(() =>
            new Set<Tables.Entity,PrimaryKeys.Entity,Container>(new Container2()).Join(
                new Set<Tables.Entity,PrimaryKeys.Entity,Container>(
                    new Sets.Container2()
                ),
                o => new PrimaryKeys.Entity(o.ID1,o.ID2),
                i => i.PrimaryKey,
                (o,i) => new { o,i }
            )
        );
        //if(outer0.NodeType==ExpressionType.Parameter)
        this.Execute標準ラムダループ(() =>
            new Set<Tables.Entity,PrimaryKeys.Entity,Container>(new Container2()).Join(
                new Set<Tables.Entity,PrimaryKeys.Entity,Container>(
                    new Sets.Container2()
                ),
                o => o.PrimaryKey,
                i => new PrimaryKeys.Entity(i.ID1,i.ID2),
                (o,i) => new { o,i }
            )
        );
        //else if(outer0.NodeType==ExpressionType.Assign) {
        //} else
        this.Execute引数パターン(a =>
            new Set<Tables.Entity,PrimaryKeys.Entity,Container>(new Container2()).Join(
                SetN<Tables.Entity>(a),
                o => o.PrimaryKey,
                i => i.PrimaryKey,
                (o,i) => new { o,i }
            )
        );
    }

    [TestMethod]
    public void Join_Dictionary_Any() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => o+i).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Join(EnuN<int>(b),o => o,i => i,(o,i) => o+i).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => o+i).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => o+i).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Join(EnuN<int>(b),o => o,i => i,(o,i) => o+i).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => o+i).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => o+i,EqualityComparer<int>.Default).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Join(EnuN<int>(b),o => o,i => i,(o,i) => o+i,EqualityComparer<int>.Default).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => o+i,EqualityComparer<int>.Default).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).AsEnumerable().Join(ArrN<int>(b),o => o,i => i,(o,i) => o+i).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).AsEnumerable().Join(EnuN<int>(b),o => o,i => i,(o,i) => o+i).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).AsEnumerable().Join(SetN<int>(b),o => o,i => i,(o,i) => o+i).Any());
    }

    [TestMethod]
    public void OfType() {
        //this.AssertExecute(a => ArrN<Int32>(a).OfType<Object>());
        //this.AssertExecute(a => EnuN<Int32>(a).OfType<Object>());
        //this.AssertExecute(a => SetN<Int32>(a).OfType<Object>());
        //this.AssertExecute(a => ArrN<Int32>(a).OfType<String>());
        //this.AssertExecute(a => EnuN<Int32>(a).OfType<String>());
        //this.AssertExecute(a => SetN<Int32>(a).OfType<String>());
        //this.AssertExecute(a => ArrNullable<Int32>(a).OfType<Object>());
        //this.AssertExecute(a => EnuNullable<Int32>(a).OfType<Object>());
        //this.AssertExecute(a => SetNullable<Int32>(a).OfType<Object>());
        //this.AssertExecute(a => ArrNullable<Int32>(a).OfType<String>());
        //this.AssertExecute(a => EnuNullable<Int32>(a).OfType<String>());
        //this.AssertExecute(a => SetNullable<Int32>(a).OfType<String>());
        //this.AssertExecute(a => ArrNullable<Int32>(a).OfType<Int32>());
        //this.AssertExecute(a => EnuNullable<Int32>(a).OfType<Int32>());
        //this.AssertExecute(a => SetNullable<Int32>(a).OfType<Int32>());
        //this.AssertExecute(a => ArrNullable<Int32>(a).OfType<Int32?>());
        //this.AssertExecute(a => EnuNullable<Int32>(a).OfType<Int32?>());
        //this.AssertExecute(a => SetNullable<Int32>(a).OfType<Int32?>());
        this.Execute引数パターン(a => ArrN<int>(a).OfType<int?>());
        this.Execute引数パターン(a => EnuN<int>(a).OfType<int?>());
        this.Execute引数パターン(a => SetN<int>(a).OfType<int?>());
    }
    [TestMethod]
    public void OfType_Any() {
        this.Execute引数パターン(a => ArrN<int>(a).OfType<string>().Any());
        this.Execute引数パターン(a => EnuN<int>(a).OfType<string>().Any());
        this.Execute引数パターン(a => SetN<int>(a).OfType<string>().Any());
        this.Execute引数パターン(a => ArrN<int>(a).OfType<object>().Any());
        this.Execute引数パターン(a => EnuN<int>(a).OfType<object>().Any());
        this.Execute引数パターン(a => SetN<int>(a).OfType<object>().Any());
    }
    [TestMethod]
    public void Range() {
        this.Execute引数パターン標準ラムダループ((a,b) => ExtensionSet.Range(a,b));
        this.Execute引数パターン標準ラムダループ((a,b) => Enumerable.Range(a,b));
    }
    [TestMethod]
    public void Repeat() => this.Execute引数パターン標準ラムダループ((a,b) => Enumerable.Repeat(a,b));
    [TestMethod]
    public void Reverse() => this.Execute引数パターン標準ラムダループ((a,b) => Enumerable.Repeat(a,b).Reverse());
    private Func<T0,T1,TResult> Func<T0, T1, TResult>(Func<T0,T1,TResult> func) => func;
    [TestMethod]
    public void Select_selector0() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Select(p => ArrN<int>(b).Select(q => q+q)));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Select(p => EnuN<int>(b).Select(q => q+q)));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Select(p => SetN<int>(b).Select(q => q+q)));
    }
    [TestMethod]
    public void Select_selector1() {
        this.Execute引数パターン(a => ArrN<int>(a).Select(Func((int p) => p*2)).Select(Func((int p) => (double)p)));
        this.Execute引数パターン(a => EnuN<int>(a).Select(Func((int p) => p*2)).Select(Func((int p) => (double)p)));
        this.Execute引数パターン(a => SetN<int>(a).Select(Func((int p) => p*2)).Select(Func((int p) => (double)p)));
    }
    [TestMethod]
    public void Select_selector2() {
        this.Execute引数パターン(a => ArrN<int>(a).Select(Func((int p) => p*2)));
        this.Execute引数パターン(a => EnuN<int>(a).Select(Func((int p) => p*2)));
        this.Execute引数パターン(a => SetN<int>(a).Select(Func((int p) => p*2)));
    }
    [TestMethod]
    public void Select_selector3() {
        this.Execute引数パターン標準ラムダループ((a,b) =>
            ArrN<int>(a).Join(
                ArrN<int>(b),
                o => o,
                i => i,
                this.Func((int o,int i) => o+1)
            ).Select(Func((int p) => p*2))
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            EnuN<int>(a).Join(
                EnuN<int>(b),
                o => o,
                i => i,
                this.Func((int o,int i) => o+1)
            ).Select(Func((int p) => p*2))
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            SetN<int>(a).Join(
                SetN<int>(b),
                o => o,
                i => i,
                this.Func((int o,int i) => o+1)
            ).Select(Func((int p) => p*2))
        );
    }
    [TestMethod]
    public void Select_selector4() {
        this.Execute引数パターン(a => ArrN<int>(a).Select(p => p+p));
        this.Execute引数パターン(a => EnuN<int>(a).Select(p => p+p));
        this.Execute引数パターン(a => SetN<int>(a).Select(p => p+p));
    }
    [TestMethod]
    public void Select_indexSelector() => this.Execute引数パターン(a => ArrN<int>(a).Select((p,i) => p+i));
    [TestMethod]
    public void SelectMany_selector() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).SelectMany(i => ArrN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).SelectMany(i => EnuN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).SelectMany(i => ArrN<int>(a)));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).SelectMany((Func<int,int[]>)(i => ArrN<int>(b))));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).SelectMany((Func<int,IEnumerable<int>>)(i => EnuN<int>(b))));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).SelectMany((Func<int,Set<int>>)(i => SetN<int>(b))));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).SelectMany(i => ArrN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).SelectMany(i => EnuN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).SelectMany(i => SetN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).SelectMany((Func<int,int[]>)(i => ArrN<int>(b))));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).SelectMany((Func<int,IEnumerable<int>>)(i => EnuN<int>(b))));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).SelectMany((Func<int,Set<int>>)(i => SetN<int>(b))));
    }
    [TestMethod]
    public void SelectMany_selector_Any() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).SelectMany(i => ArrN<int>(b)).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).SelectMany(i => EnuN<int>(b)).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).SelectMany(i => SetN<int>(b)).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).SelectMany((Func<int,int[]>)(i => ArrN<int>(b))).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).SelectMany((Func<int,IEnumerable<int>>)(i => EnuN<int>(b))).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).SelectMany((Func<int,Set<int>>)(i => SetN<int>(b))).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).SelectMany(i => ArrN<int>(b)).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).SelectMany(i => EnuN<int>(b)).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).SelectMany(i => SetN<int>(b)).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).SelectMany((Func<int,int[]>)(i => ArrN<int>(b))).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).SelectMany((Func<int,IEnumerable<int>>)(i => EnuN<int>(b))).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).SelectMany((Func<int,Set<int>>)(i => SetN<int>(b))).Any());
    }
    [TestMethod]
    public void SelectMany_indexSelector() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).SelectMany((p,i) => ArrN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).SelectMany((p,i) => EnuN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).SelectMany((Func<int,int,int[]>)((p,i) => ArrN<int>(b))));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).SelectMany((Func<int,int,IEnumerable<int>>)((p,i) => EnuN<int>(b))));
    }
    [TestMethod]
    public void SelectMany_collectionSelector_resultSelector0() {
        this.Execute引数パターン標準ラムダループ((a,b) =>
            ArrN<int>(a).SelectMany(
                (Func<int,int[]>)(o => ArrN<int>(b)),
                (Func<int,int,int>)((o,i) => o+i)
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            ArrN<int>(a).SelectMany(
                source => ArrN<int>(b),
                (source,collection) => source+collection
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            ArrN<int>(a).SelectMany(
                source => ArrN<int>(b),
                (Func<int,int,int>)((source,collection) => source+collection)
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            ArrN<int>(a).SelectMany(
                (Func<int,int[]>)(source => ArrN<int>(b)),
                (source,collection) => source+collection
            )
        );
    }
    [TestMethod]
    public void SelectMany_collectionSelector_resultSelector1() {
        this.Execute引数パターン標準ラムダループ((a,b) =>
            EnuN<int>(a).SelectMany(
                source => EnuN<int>(b),
                (source,collection) => source+collection
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            EnuN<int>(a).SelectMany(
                source => EnuN<int>(b),
                (Func<int,int,int>)((source,collection) => source+collection)
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            EnuN<int>(a).SelectMany(
                (Func<int,IEnumerable<int>>)(source => EnuN<int>(b)),
                (source,collection) => source+collection
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            EnuN<int>(a).SelectMany(
                (Func<int,IEnumerable<int>>)(source => EnuN<int>(b)),
                (Func<int,int,int>)((source,collection) => source+collection)
            )
        );
    }
    [TestMethod]
    public void SelectMany_collectionSelector_resultSelector2() {
        this.Execute引数パターン標準ラムダループ((a,b) =>
            SetN<int>(a).SelectMany(
                source => SetN<int>(b),
                (source,collection) => source+collection
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            SetN<int>(a).SelectMany(
                source => SetN<int>(b),
                (Func<int,int,int>)((source,collection) => source+collection)
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            SetN<int>(a).SelectMany(
                (Func<int,Set<int>>)(source => SetN<int>(b)),
                (source,collection) => source+collection
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            SetN<int>(a).SelectMany(
                (Func<int,Set<int>>)(source => SetN<int>(b)),
                (Func<int,int,int>)((source,collection) => source+collection)
            )
        );
    }
    [TestMethod]
    public void SelectMany_indexCollectionSelector_resultSelector0() {
        this.Execute引数パターン標準ラムダループ((a,b) =>
            ArrN<int>(a).SelectMany(
                (o,index) => ArrN<double>(b),
                (o,i) => new { o,i }
            )
        );
    }
    [TestMethod]
    public void SelectMany_indexCollectionSelector_resultSelector1() {
        this.Execute引数パターン標準ラムダループ((a,b) =>
            ArrN<int>(a).SelectMany(
                (Func<int,int,double[]>)((o,index) => ArrN<double>(b)),
                (o,i) => new { o,i }
            )
        );
    }
    [TestMethod]
    public void SelectMany_indexCollectionSelector_resultSelector2() {
        this.Execute引数パターン標準ラムダループ((a,b) =>
            ArrN<int>(a).SelectMany(
                (o,index) => ArrN<double>(b),
                (Func<int,double,object>)((o,i) => new { o,i })
            )
        );
    }
    [TestMethod]
    public void SelectMany_indexCollectionSelector_resultSelector3() {
        this.Execute引数パターン標準ラムダループ((a,b) =>
            ArrN<int>(a).SelectMany(
                (Func<int,int,double[]>)((o,index) => ArrN<double>(b)),
                (Func<int,double,object>)((o,i) => new { o,i })
            )
        );
    }
    [TestMethod]
    public void SelectMany_indexCollectionSelector_resultSelector() {
        this.Execute引数パターン標準ラムダループ((a,b) =>
            EnuN<int>(a).SelectMany(
                (Func<int,int,IEnumerable<double>>)((o,index) => EnuN<double>(b)),
                (o,i) => new { o,i }
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            EnuN<int>(a).SelectMany(
                (o,index) => EnuN<double>(b),
                (Func<int,double,object>)((o,i) => new { o,i })
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            EnuN<int>(a).SelectMany(
                (Func<int,int,IEnumerable<double>>)((o,index) => EnuN<double>(b)),
                (Func<int,double,object>)((o,i) => new { o,i })
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            SetN<int>(a).SelectMany(
                (Func<int,int,Set<double>>)((o,index) => SetN<double>(b)),
                (o,i) => new { o,i }
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            SetN<int>(a).SelectMany(
                (o,index) => SetN<double>(b),
                (Func<int,double,object>)((o,i) => new { o,i })
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            SetN<int>(a).SelectMany(
                (Func<int,int,Set<double>>)((o,index) => SetN<double>(b)),
                (Func<int,double,object>)((o,i) => new { o,i })
            )
        );
    }
    private class IEquatableを継承しないclass {
    }
    [TestMethod]
    public void SequenceEqual() {
        this.Execute引数パターン標準ラムダループ((a,b) => EqualityComparer<decimal>.Default.Let(Default => ArrN<decimal>(a).SequenceEqual(ArrN<decimal>(b),Default)));
        this.Execute引数パターン標準ラムダループ((a,b) => EqualityComparer<decimal>.Default.Let(Default => EnuN<decimal>(a).SequenceEqual(EnuN<decimal>(b),Default)));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<IEquatableを継承しないclass>(0).SequenceEqual(ArrN<IEquatableを継承しないclass>(0)));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<decimal>(a).SequenceEqual(ArrN<decimal>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<decimal>(a).SequenceEqual(EnuN<decimal>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<decimal>(a).SequenceEqual(ArrN<decimal>(b),EqualityComparer<decimal>.Default));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<decimal>(a).SequenceEqual(EnuN<decimal>(b),EqualityComparer<decimal>.Default));
    }
    [TestMethod]
    public void Set_Union() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Union(ArrN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Union(EnuN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Union(SetN<int>(b)));
    }
    [TestMethod]
    public void Single() {
        //if(!this.Setメソッドで結果が重複除去されているか(MethodCall0_Arguments_0))
        this.Execute標準ラムダループ(() => new double[] { 1 }.Single());
        this.Execute標準ラムダループ(() => new Set<double> { 1 }.Single());
    }
    [TestMethod]
    public void SingleOrDefault() {
        this.Execute標準ラムダループ(() => ArrN<double>(0).SingleOrDefault());
        this.Execute標準ラムダループ(() => ArrN<double>(1).SingleOrDefault());
        this.Execute標準ラムダループ(() => EnuN<double>(0).SingleOrDefault());
        this.Execute標準ラムダループ(() => EnuN<double>(1).SingleOrDefault());
        this.Execute標準ラムダループ(() => SetN<double>(0).SingleOrDefault());
        this.Execute標準ラムダループ(() => SetN<double>(1).SingleOrDefault());
    }
    [TestMethod]
    public void Take() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a+b).Take(a));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a+b).Take(a));
    }
    [TestMethod]
    public void TakeWhile() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a+b).TakeWhile(p => p%2==0));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a+b).TakeWhile(p => p%2==0));
    }
    [TestMethod]
    public void TakeWhile_index() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a+b).TakeWhile((p,i) => (p+i)%2==0));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a+b).TakeWhile((p,i) => (p+i)%2==0));
    }
    [TestMethod]
    public void Enumerable_Union0() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Union(ArrN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Union(EnuN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Union(SetN<int>(b)));
    }
    [TestMethod, ExpectedException(typeof(global::LinqDB.Sets.Exceptions.OneTupleException))]
    public void DUnion_Exception() {
        this.Execute標準ラムダループ(() => SetN<int>(10).DUnion(SetN<int>(1)));
    }
    [TestMethod]
    public void DUnion() {
        this.Execute標準ラムダループ(() => new Set<int> { 1,2,3 }.DUnion(new Set<int> { 4,5,6 }));
    }
    [TestMethod]
    public void DefaultIfEmpty() {
        //this.AssertExecute(a => ArrN<Int32>(a).DefaultIfEmpty());
        //this.AssertExecute(a => EnuN<Int32>(a).DefaultIfEmpty());
        //this.AssertExecute(a => SetN<Int32>(a).DefaultIfEmpty());
        this.Execute引数パターン(a => ArrN<int>(a).DefaultIfEmpty().Select(p => p+1));
        this.Execute引数パターン(a => EnuN<int>(a).DefaultIfEmpty().Select(p => p+1));
        this.Execute引数パターン(a => SetN<int>(a).DefaultIfEmpty().Select(p => p+1));
    }
    [TestMethod]
    public void DefaultIfEmpty_default() {
        this.Execute引数パターン(a => ArrN<int>(a).DefaultIfEmpty(1));
        this.Execute引数パターン(a => EnuN<int>(a).DefaultIfEmpty(2));
        this.Execute引数パターン(a => SetN<int>(a).DefaultIfEmpty(3));
        this.Execute引数パターン(a => ArrN<int>(a).DefaultIfEmpty(1).Select(p => p+1));
        this.Execute引数パターン(a => EnuN<int>(a).DefaultIfEmpty(2).Select(p => p+1));
        this.Execute引数パターン(a => SetN<int>(a).DefaultIfEmpty(3).Select(p => p+1));
    }
    [TestMethod]
    public void Delete() {
        this.Execute引数パターン(a => SetN<int>(a).Delete((Func<int,bool>)(p => p==0)));
    }


    [TestMethod]
    public void ループに展開() {
        //if(e.NodeType == ExpressionType.Call) {
        //    if(Reflection.ExtendSet.Cast == MethodCall0_GenericMethodDefinition || Reflection.ExtendEnumerable.Cast == MethodCall0_GenericMethodDefinition)
        this.Cast();
        //    if(Reflection.ExtendEnumerable.Distinct0 == MethodCall0_GenericMethodDefinition)
        this.Distinct();
        //    if((Setか = Reflection.ExtendSet.Except == MethodCall0_GenericMethodDefinition) || Reflection.ExtendEnumerable.Except == MethodCall0_GenericMethodDefinition)
        this.Except();
        //    if(Reflection.ExtendSet.GroupBy_keySelector == MethodCall0_GenericMethodDefinition)
        //    if(Reflection.ExtendEnumerable.GroupBy_keySelector == MethodCall0_GenericMethodDefinition)
        //    if(Reflection.ExtendEnumerable.GroupBy_keySelector_comparer == MethodCall0_GenericMethodDefinition)
        //    if(Reflection.ExtendSet.GroupBy_keySelector_elementSelector == MethodCall0_GenericMethodDefinition)
        //    if(Reflection.ExtendEnumerable.GroupBy_keySelector_elementSelector == MethodCall0_GenericMethodDefinition)
        //    if(Reflection.ExtendEnumerable.GroupBy_keySelector_elementSelector_comparer == MethodCall0_GenericMethodDefinition)
        //    if(Reflection.ExtendEnumerable.GroupBy_keySelector_resultSelector == MethodCall0_GenericMethodDefinition)
        //    if(Reflection.ExtendEnumerable.GroupBy_keySelector_resultSelector_comparer == MethodCall0_GenericMethodDefinition)
        //    if(Reflection.ExtendEnumerable.GroupBy_keySelector_elementSelector_resultSelector == MethodCall0_GenericMethodDefinition)
        //    if(Reflection.ExtendEnumerable.GroupBy_keySelector_elementSelector_resultSelector_comparer == MethodCall0_GenericMethodDefinition)
        this.GroupBy_keySelector();
        this.GroupBy_keySelector_comparer();
        this.GroupBy_keySelector_elementSelector();
        this.GroupBy_keySelector_elementSelector_comparer();
        this.GroupBy_keySelector_resultSelector1();
        this.GroupBy_keySelector_resultSelector_comparer();
        this.GroupBy_keySelector_elementSelector_resultSelector();
        this.GroupBy_keySelector_elementSelector_resultSelector_comparer();
        //    if((Setか = Reflection.ExtendSet.GroupJoin0 == MethodCall0_GenericMethodDefinition) || Reflection.ExtendEnumerable.GroupJoin0 == MethodCall0_GenericMethodDefinition || (Enumerable1か = Reflection.ExtendEnumerable.GroupJoin1 == MethodCall0_GenericMethodDefinition))
        this.GroupJoin_Dictionary();
        //    if((Setか = Reflection.ExtendSet.Intersect0 == MethodCall0_GenericMethodDefinition) || Reflection.ExtendEnumerable.Intersect0 == MethodCall0_GenericMethodDefinition)
        this.Intersect();
        //    if((Setか = Reflection.ExtendSet.Join == MethodCall0_GenericMethodDefinition) || Reflection.ExtendEnumerable.Join == MethodCall0_GenericMethodDefinition || (Enumerable1か = Reflection.ExtendEnumerable.Join == MethodCall0_GenericMethodDefinition))
        this.Join_Dictionary();
        //    if(Reflection.ExtendSet.InternalJoin_outerKeySelector==MethodCall0_GenericMethodDefinition)
        this.Join_outerKeySelector();
        //    if(Reflection.ExtendSet.InternalJoin_inKeySelector==MethodCall0_GenericMethodDefinition)
        this.Join_innerKeySelector();
        //    if(Reflection.ExtendSet.Dictionary_Equal == MethodCall0_GenericMethodDefinition || Reflection.ExtendEnumerable.Dictionary_Equal == MethodCall0_GenericMethodDefinition)
        this.Dictionary_Equal();
        //    if(Reflection.ExtendSet.OfType == MethodCall0_GenericMethodDefinition || Reflection.ExtendEnumerable.OfType == MethodCall0_GenericMethodDefinition)
        this.OfType();
        //    if(Reflection.ExtendEnumerable.Range == MethodCall0_GenericMethodDefinition)
        this.Range();
        //    if(Reflection.ExtendEnumerable.Repeat == MethodCall0_GenericMethodDefinition)
        this.Repeat();
        //    if(Reflection.ExtendSet.Select_selector == MethodCall0_GenericMethodDefinition ||Reflection.ExtendEnumerable.Select_selector == MethodCall0_GenericMethodDefinition)
        this.Select_selector0();
        this.Select_selector1();
        this.Select_selector2();
        this.Select_selector3();
        this.Select_selector4();
        //    if(Reflection.ExtendEnumerable.Select_indexSelector == MethodCall0_GenericMethodDefinition)
        this.Select_indexSelector();
        //    if(Reflection.ExtendSet.SelectMany_selector == MethodCall0_GenericMethodDefinition ||Reflection.ExtendEnumerable.SelectMany_selector == MethodCall0_GenericMethodDefinition)
        this.SelectMany_selector();
        //    if(Reflection.ExtendEnumerable.SelectMany_indexSelector == MethodCall0_GenericMethodDefinition)
        this.SelectMany_indexSelector();
        //    if(Reflection.ExtendSet.SelectMany_collectionSelector_resultSelector == MethodCall0_GenericMethodDefinition || Reflection.ExtendEnumerable.SelectMany_collectionSelector_resultSelector == MethodCall0_GenericMethodDefinition)
        this.SelectMany_collectionSelector_resultSelector0();
        this.SelectMany_collectionSelector_resultSelector1();
        this.SelectMany_collectionSelector_resultSelector2();
        //    if(Reflection.ExtendEnumerable.SelectMany_indexCollectionSelector_resultSelector == MethodCall0_GenericMethodDefinition)
        this.SelectMany_indexCollectionSelector_resultSelector();
        //    if(Reflection.ExtendSet.Union == MethodCall0_GenericMethodDefinition)
        this.Set_Union();
        //    if(Reflection.ExtendEnumerable.Union0 == MethodCall0_GenericMethodDefinition)
        this.Enumerable_Union0();
        //    if(Reflection.ExtendSet.DUnion == MethodCall0_GenericMethodDefinition)
        this.DUnion();
        //    if(Reflection.ExtendSet.Delete == MethodCall0_GenericMethodDefinition)
        this.Delete();
        //    if(Reflection.ExtendSet.Where == MethodCall0_GenericMethodDefinition || Reflection.ExtendEnumerable.Where == MethodCall0_GenericMethodDefinition)
        this.Where();
        //{
        //    var EnumeratorExpression = e_Type.IsArray
        //        ? (Expression)Expression.New(
        this.Execute引数パターン(a => ArrN<int>(a).Select(p => p+1));
        //        )
        //        : Expression.Call(
        //        e_Type.GetMethod(nameof(IEnumerable.GetEnumerator))??e_Type.GetInterface(IEnumerable1FullName).GetMethod(nameof(IEnumerable.GetEnumerator))
        this.Execute引数パターン(a => SetN<int>(a).Select(p => p+1));
        var OrdenalEnumerable = new[] { 1,2 }.OrderBy(p => p);
        this.Execute標準ラムダループ(() => OrdenalEnumerable.Select(p => p+1));
        //}
    }
    [TestMethod]
    public void AverageNullable() {
        this.Execute引数パターン(a => ArrN<decimal?>(a).Average());
        this.Execute引数パターン(a => ArrN<double?>(a).Average());
        this.Execute引数パターン(a => ArrN<float?>(a).Average());
        this.Execute引数パターン(a => ArrN<long?>(a).Average());
        this.Execute引数パターン(a => ArrN<int?>(a).Average());
        this.Execute引数パターン(a => EnuN<decimal?>(a).Average());
        this.Execute引数パターン(a => EnuN<double?>(a).Average());
        this.Execute引数パターン(a => EnuN<float?>(a).Average());
        this.Execute引数パターン(a => EnuN<long?>(a).Average());
        this.Execute引数パターン(a => EnuN<int?>(a).Average());
        this.Execute引数パターン(a => SetN<decimal?>(a).Average());
        this.Execute引数パターン(a => SetN<double?>(a).Average());
        this.Execute引数パターン(a => SetN<float?>(a).Average());
        this.Execute引数パターン(a => SetN<long?>(a).Average());
        this.Execute引数パターン(a => SetN<int?>(a).Average());
    }

    public class SetX<U, T>:Set<T> {

    }
    [TestMethod]
    public void ジェネリックアリティの順序を逆にしたSetが正しく動作すること() {
        var Set1 = new SetX<double,int>();
        this.Execute標準ラムダループ(() => Set1.Join(Set1,o => o,i => i,(o,i) => o+i));
    }
    public class ListX<U, T>:Set<T> {

    }
    [TestMethod]
    public void ジェネリックアリティの順序を逆にしたListが正しく動作すること() {
        var Set1 = new ListX<double,int>();
        this.Execute標準ラムダループ(() => Set1.Join(Set1,o => o,i => i,(o,i) => o+i));
    }
    [TestMethod]
    public void AverageNullable_selector() {
        this.Execute引数パターン(a => ArrN<int>(a).Average(p => (decimal?)p));
        this.Execute引数パターン(a => ArrN<int>(a).Average(p => (double?)p));
        this.Execute引数パターン(a => ArrN<int>(a).Average(p => (float?)p));
        this.Execute引数パターン(a => ArrN<int>(a).Average(p => (long?)p));
        this.Execute引数パターン(a => ArrN<int>(a).Average(p => (int?)p));
        this.Execute引数パターン(a => EnuN<int>(a).Average(p => (decimal?)p));
        this.Execute引数パターン(a => EnuN<int>(a).Average(p => (double?)p));
        this.Execute引数パターン(a => EnuN<int>(a).Average(p => (float?)p));
        this.Execute引数パターン(a => EnuN<int>(a).Average(p => (long?)p));
        this.Execute引数パターン(a => EnuN<int>(a).Average(p => (int?)p));
        this.Execute引数パターン(a => SetN<int>(a).Average(p => (decimal?)p));
        this.Execute引数パターン(a => SetN<int>(a).Average(p => (double?)p));
        this.Execute引数パターン(a => SetN<int>(a).Average(p => (float?)p));
        this.Execute引数パターン(a => SetN<int>(a).Average(p => (long?)p));
        this.Execute引数パターン(a => SetN<int>(a).Average(p => (int?)p));
        //if(Setループ展開可能なCallか(MethodCall0_Arguments_0)){
        //    if(Setメソッドで結果が重複除去されているか(MethodCall0_Arguments_0)){
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<decimal>(a).Intersect(SetN<decimal>(b)).Average(p => (decimal?)p));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<decimal>(a).Intersect(SetN<decimal>(b)).Average(p => (decimal?)p));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<decimal>(a).Intersect(SetN<decimal>(b)).Average(p => (decimal?)p));
        //    } else{
        this.Execute引数パターン(a => ArrN<decimal>(a).Select(p => p+1).Average(p => (decimal?)p));
        this.Execute引数パターン(a => EnuN<decimal>(a).Select(p => p+1).Average(p => (decimal?)p));
        this.Execute引数パターン(a => SetN<decimal>(a).Select(p => p+1).Average(p => (decimal?)p));
        //    }
        //} else { 
        //    if(MethodCall0_Arguments_0.NodeType==ExpressionType.Parameter){
        this.Execute引数パターン(a => ArrN<decimal>(a).Let(q => q.Average(p => (decimal?)p)));
        this.Execute引数パターン(a => EnuN<decimal>(a).Let(q => q.Average(p => (decimal?)p)));
        this.Execute引数パターン(a => SetN<decimal>(a).Let(q => q.Average(p => (decimal?)p)));
        //    }else{
        this.Execute引数パターン(a => ArrN<decimal>(a).Average(p => (decimal?)p));
        this.Execute引数パターン(a => EnuN<decimal>(a).Average(p => (decimal?)p));
        this.Execute引数パターン(a => SetN<decimal>(a).Average(p => (decimal?)p));
        //    }
        //}
    }
    [TestMethod]
    public void Call() {
        ////if(Reflection.ExtendSet1.Inline0==MethodCall0_GenericMethodDefinition) {
        //this.AssertExecute(() => _Int32.Inline(p => p + 1));
        //this.AssertExecute(() => _Int32.Inline((Func<Int32, Int32>)(p => p + 1)));
        ////}
        //if(Reflection.ExtendSet1.Inline1==MethodCall0_GenericMethodDefinition) {
        this.Execute標準ラムダループ(() => Inline(() => 1));
        this.Execute標準ラムダループ(() => Inline((Func<int>)(() => 1)));
        //}
        //if(Reflection.ExtendEnumerable.Aggregate_func==MethodCall0_GenericMethodDefinition) {
        this.Aggregate_func();
        //}
        //if(Reflection.ExtendEnumerable.Aggregate_seed_func==MethodCall0_GenericMethodDefinition) {
        this.Aggregate_seed_func();
        //}
        //if(Reflection.ExtendSet1.Aggregate_seed_func_resultSelector==MethodCall0_GenericMethodDefinition||Reflection.ExtendEnumerable.Aggregate_seed_func_resultSelector==MethodCall0_GenericMethodDefinition) {
        this.Aggregate_seed_func_resultSelector();
        //}
        //if(Reflection.ExtendSet1.All==MethodCall0_GenericMethodDefinition||Reflection.ExtendEnumerable.All==MethodCall0_GenericMethodDefinition) {
        this.All();
        //}
        //if(Reflection.ExtendSet.Any == MethodCall0_GenericMethodDefinition || Reflection.ExtendEnumerable.Any == MethodCall0_GenericMethodDefinition) {
        this.Any();
        //}
        //if(Reflection.ExtendSet.AverageDecimal == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendSet.AverageDouble == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendEnumerable.AverageDecimal == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendEnumerable.AverageDouble == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendEnumerable.AverageSingle == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendEnumerable.AverageInt64 == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendEnumerable.AverageInt32 == MethodCall0_GenericMethodDefinition)
        this.Average();
        //if(Reflection.ExtendSet.AverageDecimal_selector == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendSet.AverageDouble_selector == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendEnumerable.AverageDecimal_selector == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendEnumerable.AverageDouble_selector == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendEnumerable.AverageSingle_selector == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendEnumerable.AverageInt64_selector == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendEnumerable.AverageInt32_selector == MethodCall0_GenericMethodDefinition)
        this.Average_selector();
        //if(Reflection.ExtendSet.AverageDecimalNullable == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendSet.AverageDoubleNullable == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendEnumerable.AverageDecimalNullable == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendEnumerable.AverageDoubleNullable == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendEnumerable.AverageSingleNullable == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendEnumerable.AverageInt64Nullable == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendEnumerable.AverageInt32Nullable == MethodCall0_GenericMethodDefinition)
        this.AverageNullable();
        //if(Reflection.ExtendSet.AverageDecimalNullable_selector == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendSet.AverageDoubleNullable_selector == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendEnumerable.AverageDecimalNullable_selector == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendEnumerable.AverageDoubleNullable_selector == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendEnumerable.AverageSingleNullable_selector == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendEnumerable.AverageInt64Nullable_selector == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendEnumerable.AverageInt32Nullable_selector == MethodCall0_GenericMethodDefinition)
        this.AverageNullable_selector();
        //if(Reflection.ExtendSet.StdevDouble == MethodCall0_GenericMethodDefinition)
        this.StdevDouble();
        //if(Reflection.ExtendSet.StdevDouble_selector == MethodCall0_GenericMethodDefinition)
        this.StdevDouble_selector();
        //if(Reflection.ExtendSet.AvedevDouble_selector == MethodCall0_GenericMethodDefinition)
        this.Avedev_selector();
        //if(Reflection.ExtendSet.AsEnumerable == MethodCall0_GenericMethodDefinition || Reflection.ExtendEnumerable.AsEnumerable == MethodCall0_GenericMethodDefinition)
        //    return Expression.Call(
        //        MethodCall0_Method,
        //        this.Traverse(MethodCall0.Arguments)
        //    );
        //if(Reflection.ExtendSet.LongCount0 == MethodCall0_GenericMethodDefinition || Reflection.ExtendEnumerable.Count0 == MethodCall0_GenericMethodDefinition || Reflection.ExtendEnumerable.LongCount0 == MethodCall0_GenericMethodDefinition)
        this.LongCount0();
        //if(Reflection.ExtendEnumerable.Empty == MethodCall0_GenericMethodDefinition)
        this.Empty();
        //if(Reflection.ExtendSet.HarmeanDecimalNullable_selector == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendSet.HarmeanDecimal_selector == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendSet.HarmeanDoubleNullable_selector == MethodCall0_GenericMethodDefinition)
        //if(Reflection.ExtendSet.HarmeanDouble_selector == MethodCall0_GenericMethodDefinition)
        this.HarmeanNullable_selector();
        //if(Reflection.ExtendSet.GeomeanDoubleNullable_selector == MethodCall0_GenericMethodDefinition) {
        this.GeomeanDoubleNullable_selector();
        //if(Reflection.ExtendSet.GeomeanDouble_selector == MethodCall0_GenericMethodDefinition) {
        this.GeomeanDouble_selector();
        //Contract.Assert(Reflection.ExtendSet.Contains_value != MethodCall0_GenericMethodDefinition);
        //if(
        //    Reflection.ExtendEnumerable.MaxDecimal == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MaxDouble == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MaxSingle == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MaxInt32 == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MaxInt64 == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MaxTSource == MethodCall0_GenericMethodDefinition
        //    Reflection.ExtendEnumerable.MinDecimal == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MinDouble == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MinSingle == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MinInt32 == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MinInt64 == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MinTSource == MethodCall0_GenericMethodDefinition
        //) {
        this.MaxMin();
        //if(
        //    Reflection.ExtendEnumerable.MaxDecimalNullable == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MaxDoubleNullable == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MaxSingleNullable == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MaxInt32Nullable == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MaxInt64Nullable == MethodCall0_GenericMethodDefinition
        //    Reflection.ExtendEnumerable.MinDecimalNullable == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MinDoubleNullable == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MinSingleNullable == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MinInt32Nullable == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MinInt64Nullable == MethodCall0_GenericMethodDefinition
        //) {
        this.MaxMinNullable();
        //if(
        //    Reflection.ExtendSet.Max_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MaxDecimal_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MaxDouble_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MaxSingle_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MaxInt32_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MaxInt64_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MaxTSource_selector == MethodCall0_GenericMethodDefinition
        //    Reflection.ExtendSet.Min_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MinDecimal_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MinDouble_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MinSingle_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MinInt32_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MinInt64_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MinTSource_selector == MethodCall0_GenericMethodDefinition
        //) {
        //    return this.MaxMin_selector(MethodCall0, ExpressionType.GreaterThan);
        //}
        this.MaxMin_selector();
        //if(
        //    Reflection.ExtendSet.MaxNullable_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MaxDecimalNullable_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MaxDoubleNullable_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MaxSingleNullable_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MaxInt32Nullable_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MaxInt64Nullable_selector == MethodCall0_GenericMethodDefinition
        //    Reflection.ExtendSet.MinNullable_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MinDecimalNullable_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MinDoubleNullable_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MinSingleNullable_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MinInt32Nullable_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.MinInt64Nullable_selector == MethodCall0_GenericMethodDefinition
        //) {
        this.MaxMinNullable_selector();
        //if(
        //    Varpか ||
        //    Reflection.ExtendSet.VarDecimal_selector == MethodCall0_GenericMethodDefinition || Reflection.ExtendSet.VarDouble_selector == MethodCall0_GenericMethodDefinition
        //) {
        this.Var_Varp();
        //var SequenceEqual0か = Reflection.ExtendEnumerable.SequenceEqual == MethodCall0_GenericMethodDefinition;
        //if(SequenceEqual0か || Reflection.ExtendEnumerable.SequenceEqual_comparer == MethodCall0_GenericMethodDefinition) {
        this.SequenceEqual();
        //if(Reflection.ExtendSet.Single == MethodCall0_GenericMethodDefinition || Reflection.ExtendEnumerable.Single == MethodCall0_GenericMethodDefinition) {
        this.Single();
        //if(Reflection.ExtendSet.SingleOrDefault == MethodCall0_GenericMethodDefinition || Reflection.ExtendSet.SingleOrDefault_defaultValue == MethodCall0_GenericMethodDefinition || Reflection.ExtendEnumerable.SingleOrDefault == MethodCall0_GenericMethodDefinition || Reflection.ExtendEnumerable.SingleOrDefault_predicate == MethodCall0_GenericMethodDefinition) {
        this.SingleOrDefault();
        //if(
        //    Reflection.ExtendSet.SumDecimalNullable_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.SumNullableInt32_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.SumNullableInt64_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.SumNullableDecimal_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.SumNullableDouble_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.SumNullableSingle_selector == MethodCall0_GenericMethodDefinition
        //) {
        this.SumNullable_selector();
        //if(
        //    Reflection.ExtendSet.SumInt32_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendSet.SumInt64_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendSet.SumDecimal_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendSet.SumDouble_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendSet.SumSingle_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.SumInt32_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.SumInt64_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.SumDecimal_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.SumDouble_selector == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.SumSingle_selector == MethodCall0_GenericMethodDefinition
        //) {
        this.Sum_selector();
        //if(
        //    Reflection.ExtendSet.SumInt32 == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendSet.SumInt64 == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendSet.SumDecimal == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendSet.SumDouble == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendSet.SumSingle == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.SumInt32 == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.SumInt64 == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.SumDecimal == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.SumDouble == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.SumSingle == MethodCall0_GenericMethodDefinition
        //) {
        this.Sum();
        //if(
        //    Reflection.ExtendEnumerable.SumInt32Nullable == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.SumInt64Nullable == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.SumDecimalNullable == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.SumDoubleNullable == MethodCall0_GenericMethodDefinition ||
        //    Reflection.ExtendEnumerable.SumSingleNullable == MethodCall0_GenericMethodDefinition
        //) {
        this.SumNullable();
        //var Set1_DictionarySetか = Reflection.ExtendSet.Dictionary == MethodCall0_GenericMethodDefinition;
        //if(Set1_DictionarySetか || Reflection.ExtendEnumerable.Dictionary == MethodCall0_GenericMethodDefinition) {
        this.Dictionary();
        //if(Reflection.ExtendEnumerable.ToArray == MethodCall0_GenericMethodDefinition) {
        this.ToArray();
        //if(EnumerableSetループ展開可能なGenericMethodDefinitionか(MethodCall0_GenericMethodDefinition)) {
        //    if(MethodCall0_Method.DeclaringType == typeof(SetExtension) || MethodCall0_Method.DeclaringType == typeof(InternalSetExtension)) {
        //    } else {
        //            this.Enumerableメソッドで結果にListを要求するか(MethodCall0)
        //            ? typeof(List2<>)
        //            : typeof(HashSet<>)
        //    }
        //    {
        //    }
        //}
        //return base.Call(MethodCall0);


        //    if(Reflection.ExtendSet1            .AverageDecimal        ==MethodCall0_GenericMethodDefinition)return this.Average_Average        (MethodCall0,Reflection.ExtendSet.Count0);
        this.Execute引数パターン(a => SetN<decimal>(a+1).Average());
        //    if(Reflection.ExtendSet1            .AverageDouble         ==MethodCall0_GenericMethodDefinition)return this.Average_Average        (MethodCall0,Reflection.ExtendSet.Count0);
        this.Execute引数パターン(a => SetN<double>(a+1).Average());
        //    if(Reflection.ExtendSet1            .AverageDecimalSelector==MethodCall0_GenericMethodDefinition)return this.Average_Average_Selector(MethodCall0,Reflection.ExtendSet.Count0);
        this.Execute引数パターン(a => SetN<decimal>(a+1).Average(p => p*2));
        //    if(Reflection.ExtendSet1            .AverageDoubleSelector ==MethodCall0_GenericMethodDefinition)return this.Average_Average_Selector(MethodCall0,Reflection.ExtendSet.Count0);
        this.Execute引数パターン(a => SetN<double>(a+1).Average(p => p*2));
        //    if(Reflection.ExtendEnumerable.AverageDecimal               ==MethodCall0_GenericMethodDefinition)return this.Average_Average        (MethodCall0,Reflection.ExtendEnumerable.Count0);
        this.Execute引数パターン(a => ArrN<decimal>(a+1).Average());
        //    if(Reflection.ExtendEnumerable.AverageDouble                ==MethodCall0_GenericMethodDefinition)return this.Average_Average        (MethodCall0,Reflection.ExtendEnumerable.Count0);
        this.Execute引数パターン(a => ArrN<double>(a+1).Average());
        //    if(Reflection.ExtendEnumerable.AverageSingle                ==MethodCall0_GenericMethodDefinition)return this.Average_Average        (MethodCall0,Reflection.ExtendEnumerable.Count0);
        this.Execute引数パターン(a => ArrN<float>(a+1).Average());
        //    if(Reflection.ExtendEnumerable.AverageInt64                 ==MethodCall0_GenericMethodDefinition)return this.Average_Average        (MethodCall0,Reflection.ExtendEnumerable.Count0);
        this.Execute引数パターン(a => ArrN<long>(a+1).Average());
        //    if(Reflection.ExtendEnumerable.AverageInt32                 ==MethodCall0_GenericMethodDefinition)return this.Average_Average        (MethodCall0,Reflection.ExtendEnumerable.Count0);
        this.Execute引数パターン(a => ArrN<int>(a+1).Average());
        //    if(Reflection.ExtendEnumerable.AverageDecimalSelector       ==MethodCall0_GenericMethodDefinition)return this.Average_Average_Selector(MethodCall0,Reflection.ExtendEnumerable.LongCount0);
        this.Execute引数パターン(a => ArrN<decimal>(a+1).Average(p => p*2));
        //    if(Reflection.ExtendEnumerable.AverageDoubleSelector        ==MethodCall0_GenericMethodDefinition)return this.Average_Average_Selector(MethodCall0,Reflection.ExtendEnumerable.LongCount0);
        this.Execute引数パターン(a => ArrN<double>(a+1).Average(p => p*2));
        //    if(Reflection.ExtendEnumerable.AverageSingleSelector        ==MethodCall0_GenericMethodDefinition)return this.Average_Average_Selector(MethodCall0,Reflection.ExtendEnumerable.LongCount0);
        this.Execute引数パターン(a => ArrN<float>(a+1).Average(p => p*2));
        //    if(Reflection.ExtendEnumerable.AverageInt64Selector         ==MethodCall0_GenericMethodDefinition)return this.Average_Average_Selector(MethodCall0,Reflection.ExtendEnumerable.LongCount0);
        this.Execute引数パターン(a => ArrN<long>(a+1).Average(p => p*2));
        //    if(Reflection.ExtendEnumerable.AverageInt32Selector         ==MethodCall0_GenericMethodDefinition)return this.Average_Average_Selector(MethodCall0,Reflection.ExtendEnumerable.LongCount0);
        this.Execute引数パターン(a => ArrN<int>(a+1).Average(p => p*2));

        //    if(Reflection.ExtendSet         .AverageDecimalNullable ==MethodCall0_GenericMethodDefinition)return this.AverageNullable        (MethodCall0,Reflection.ExtendEnumerable.Count0);
        this.Execute引数パターン(a => SetN<decimal?>(a+1).Average());
        //    if(Reflection.ExtendEnumerable.AverageDecimalNullable        ==MethodCall0_GenericMethodDefinition)return this.AverageNullable        (MethodCall0,Reflection.ExtendEnumerable.Count0);
        this.Execute引数パターン(a => ArrN<decimal?>(a+1).Average());
        //    if(Reflection.ExtendEnumerable.AverageDoubleNullable          ==MethodCall0_GenericMethodDefinition)return this.AverageNullable        (MethodCall0,Reflection.ExtendEnumerable.Count0);
        this.Execute引数パターン(a => ArrN<double?>(a+1).Average());
        //    if(Reflection.ExtendEnumerable.AverageSingleNullable          ==MethodCall0_GenericMethodDefinition)return this.AverageNullable        (MethodCall0,Reflection.ExtendEnumerable.Count0);
        this.Execute引数パターン(a => ArrN<float?>(a+1).Average());
        //    if(Reflection.ExtendEnumerable.AverageInt64Nullable           ==MethodCall0_GenericMethodDefinition)return this.AverageNullable        (MethodCall0,Reflection.ExtendEnumerable.Count0);
        this.Execute引数パターン(a => ArrN<long?>(a+1).Average());
        //    if(Reflection.ExtendEnumerable.AverageInt32Nullable           ==MethodCall0_GenericMethodDefinition)return this.AverageNullable        (MethodCall0,Reflection.ExtendEnumerable.Count0);
        this.Execute引数パターン(a => ArrN<int?>(a+1).Average());
        //    if(Reflection.ExtendSet         .AverageDecimalNullable_selector==MethodCall0_GenericMethodDefinition)return this.AverageNullable_selector(MethodCall0,Reflection.ExtendEnumerable.LongCount0);
        this.Execute引数パターン(a => SetN<decimal>(a+1).Average(p => (decimal?)p));
        //    if(Reflection.ExtendEnumerable.AverageDecimalNullable_selector==MethodCall0_GenericMethodDefinition)return this.AverageNullable_selector(MethodCall0,Reflection.ExtendEnumerable.LongCount0);
        this.Execute引数パターン(a => ArrN<decimal>(a+1).Average(p => (decimal?)p));
        //    if(Reflection.ExtendEnumerable.AverageDoubleNullable_selector ==MethodCall0_GenericMethodDefinition)return this.AverageNullable_selector(MethodCall0,Reflection.ExtendEnumerable.LongCount0);
        this.Execute引数パターン(a => ArrN<double>(a+1).Average(p => (double?)p));
        //    if(Reflection.ExtendEnumerable.AverageSingleNullable_selector ==MethodCall0_GenericMethodDefinition)return this.AverageNullable_selector(MethodCall0,Reflection.ExtendEnumerable.LongCount0);
        this.Execute引数パターン(a => ArrN<float>(a+1).Average(p => (float?)p));
        //    if(Reflection.ExtendEnumerable.AverageInt64Nullable_selector  ==MethodCall0_GenericMethodDefinition)return this.AverageNullable_selector(MethodCall0,Reflection.ExtendEnumerable.LongCount0);
        this.Execute引数パターン(a => ArrN<long>(a+1).Average(p => (long?)p));
        //    if(Reflection.ExtendEnumerable.AverageInt32Nullable_selector  ==MethodCall0_GenericMethodDefinition)return this.AverageNullable_selector(MethodCall0,Reflection.ExtendEnumerable.LongCount0);
        this.Execute引数パターン(a => ArrN<int>(a+1).Average(p => (int?)p));

        //    if(Reflection.ExtendSet1.AsEnumerable==MethodCall0_GenericMethodDefinition||Reflection.ExtendEnumerable.AsEnumerable==MethodCall0_GenericMethodDefinition) {
        this.Execute引数パターン(a => ArrN<int>(a).AsEnumerable());
        this.Execute引数パターン(a => SetN<int>(a).AsEnumerable());
        //    if(Reflection.ExtendSet.Count==MethodCall0_GenericMethodDefinition) {
        this.Execute引数パターン(a => ArrN<int>(a).Count());
        this.Execute引数パターン(a => EnuN<int>(a).Count());
        this.Execute引数パターン(a => SetN<int>(a).Count());
        //    }
        //    if(Reflection.ExtendEnumerable.Empty==MethodCall0_GenericMethodDefinition)return MethodCall0;
        this.Execute標準ラムダループ(() => Enumerable.Empty<int>());
        //    if(Reflection.ExtendSet1.HarmeanDecimalNullable_selector==MethodCall0_GenericMethodDefinition)return this.HarmeanNullableSelector(MethodCall0,Constant_0M,Constant_1M);
        this.Execute引数パターン(a => SetN<decimal>(a).Select(p => p+1).Harmean(p => (decimal?)p*2));
        //    if(Reflection.ExtendSet1.HarmeanDecimalSelector==MethodCall0_GenericMethodDefinition)return this.HarmeanSelector(MethodCall0,Constant_0M,Constant_1M);
        this.Execute引数パターン(a => SetN<decimal>(a+1).Select(p => p+1).Harmean(p => p*2));
        //    if(Reflection.ExtendSet1.HarmeanDoubleNullable_selector==MethodCall0_GenericMethodDefinition)return this.HarmeanNullableSelector(MethodCall0,Constant_0D,Constant_1D);
        this.Execute引数パターン(a => SetN<double>(a).Select(p => p+1).Harmean(p => (double?)p*2));
        //    if(Reflection.ExtendSet1.HarmeanDouble_selector==MethodCall0_GenericMethodDefinition)return this.HarmeanSelector(MethodCall0,Constant_0D,Constant_1D);
        this.Execute引数パターン(a => SetN<double>(a+1).Select(p => p+1).Harmean(p => p*2));
        //    if(Reflection.ExtendSet1.GeomeanDoubleNullable_selector==MethodCall0_GenericMethodDefinition) {
        //        if(Setループ展開可能なCallか(MethodCall0_Arguments_0)) {
        //            if(Setメソッドで結果が重複除去されているか(MethodCall0_Arguments_0)) {
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<double>(a).Intersect(SetN<double>(b)).Geomean(p => (double?)p*2));
        //            }else{
        this.Execute引数パターン(a => SetN<double>(a).Select(p => p+1).Geomean(p => (double?)p*2));
        //            }
        //        }else{
        //            if(MethodCall0_Arguments_0.NodeType==ExpressionType.Parameter) {
        this.Execute引数パターン(a => SetN<double>(a).Let(Set => Set.Geomean(p => (double?)p*2)));
        //            }else{
        this.Execute引数パターン(a => SetN<double>(a).Geomean(p => (double?)p*2));
        //            }
        //        }
        //    }
        //    if(Reflection.ExtendSet1.GeomeanDouble_selector==MethodCall0_GenericMethodDefinition) {
        //        if(Setループ展開可能なCallか(MethodCall0_Arguments_0)) {
        //            if(Setメソッドで結果が重複除去されているか(MethodCall0_Arguments_0)) {
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<double>(a+1).Intersect(SetN<double>(b+1)).Geomean(p => p*2));
        //            }else{
        this.Execute引数パターン(a => SetN<double>(a+1).Select(p => p+1).Geomean(p => p*2));
        //            }
        //        }else{
        //            if(MethodCall0_Arguments_0.NodeType==ExpressionType.Parameter) {
        this.Execute引数パターン(a => SetN<double>(a+1).Let(Set => Set.Geomean(p => p*2)));
        //            }else{
        this.Execute引数パターン(a => SetN<double>(a+1).Geomean(p => p*2));
        //            }
        //        }
        //    }
        //    if(
        //        Reflection.ExtendEnumerable.MaxDecimal                ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MaxDouble                 ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MaxSingle                 ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MaxInt32                  ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MaxInt64                  ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MaxTSource                ==MethodCall0_GenericMethodDefinition
        //    ) {
        this.Execute引数パターン(a => ArrN<decimal>(a+1).Max());
        this.Execute引数パターン(a => ArrN<double>(a+1).Max());
        this.Execute引数パターン(a => ArrN<float>(a+1).Max());
        this.Execute引数パターン(a => ArrN<int>(a+1).Max());
        this.Execute引数パターン(a => ArrN<long>(a+1).Max());
        this.Execute引数パターン(a => new[] { "A" }.Max());
        //    }
        //    if(
        //        Reflection.ExtendEnumerable.MaxDecimalNullable        ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MaxDoubleNullable         ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MaxSingleNullable         ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MaxInt32Nullable          ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MaxInt64Nullable          ==MethodCall0_GenericMethodDefinition
        //    ) {
        this.Execute引数パターン(a => ArrN<decimal?>(a).Max());
        this.Execute引数パターン(a => ArrN<double?>(a).Max());
        this.Execute引数パターン(a => ArrN<float?>(a).Max());
        this.Execute引数パターン(a => ArrN<int?>(a).Max());
        this.Execute引数パターン(a => ArrN<long?>(a).Max());
        //    }
        //    if(
        //        Reflection.ExtendSet1            .MaxSelector                       ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MaxDecimalSelector        ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MaxDoubleSelector         ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MaxSingleSelector         ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MaxInt32Selector          ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MaxInt64Selector          ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MaxTSourceSelector        ==MethodCall0_GenericMethodDefinition
        //    ) {
        this.Execute引数パターン(a => SetN<decimal>(a+1).Max(p => p));
        this.Execute引数パターン(a => ArrN<decimal>(a+1).Max(p => p));
        this.Execute引数パターン(a => ArrN<double>(a+1).Max(p => p));
        this.Execute引数パターン(a => ArrN<float>(a+1).Max(p => p));
        this.Execute引数パターン(a => ArrN<int>(a+1).Max(p => p));
        this.Execute引数パターン(a => ArrN<long>(a+1).Max(p => p));
        this.Execute引数パターン(a => new[] { "A" }.Max(p => p));
        //    }
        //    if(
        //        Reflection.ExtendSet1            .MaxNullableSelector==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MaxDecimalNullableSelector==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MaxDoubleNullableSelector ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MaxSingleNullableSelector ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MaxInt32NullableSelector  ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MaxInt64NullableSelector  ==MethodCall0_GenericMethodDefinition
        //    ) {
        this.Execute引数パターン(a => SetN<decimal>(a).Max(p => (decimal?)p));
        this.Execute引数パターン(a => ArrN<decimal>(a).Max(p => (decimal?)p));
        this.Execute引数パターン(a => ArrN<double>(a).Max(p => (double?)p));
        this.Execute引数パターン(a => ArrN<float>(a).Max(p => (float?)p));
        this.Execute引数パターン(a => ArrN<int>(a).Max(p => (int?)p));
        this.Execute引数パターン(a => ArrN<long>(a).Max(p => (long?)p));
        //    }
        //    if(
        //        Reflection.ExtendEnumerable.MinDecimal                ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MinDouble                 ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MinSingle                 ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MinInt32                  ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MinInt64                  ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MinTSource                ==MethodCall0_GenericMethodDefinition
        //    ) {
        this.Execute引数パターン(a => ArrN<decimal>(a+1).Min());
        this.Execute引数パターン(a => ArrN<double>(a+1).Min());
        this.Execute引数パターン(a => ArrN<float>(a+1).Min());
        this.Execute引数パターン(a => ArrN<int>(a+1).Min());
        this.Execute引数パターン(a => ArrN<long>(a+1).Min());
        this.Execute引数パターン(a => new[] { "A" }.Min());
        //    }
        //    if(
        //        Reflection.ExtendEnumerable.MinDecimalNullable        ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MinDoubleNullable         ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MinSingleNullable         ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MinInt32Nullable          ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MinInt64Nullable          ==MethodCall0_GenericMethodDefinition
        //    ) {
        this.Execute引数パターン(a => ArrN<decimal?>(a).Min());
        this.Execute引数パターン(a => ArrN<double?>(a).Min());
        this.Execute引数パターン(a => ArrN<float?>(a).Min());
        this.Execute引数パターン(a => ArrN<int?>(a).Min());
        this.Execute引数パターン(a => ArrN<long?>(a).Min());
        //    }
        //    if(
        //        Reflection.ExtendSet1            .MinSelector               ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MinDecimalSelector        ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MinDoubleSelector         ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MinSingleSelector         ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MinInt32Selector          ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MinInt64Selector          ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MinTSourceSelector        ==MethodCall0_GenericMethodDefinition
        //    ) {
        this.Execute引数パターン(a => SetN<decimal>(a+1).Min(p => p));
        this.Execute引数パターン(a => ArrN<decimal>(a+1).Min(p => p));
        this.Execute引数パターン(a => ArrN<double>(a+1).Min(p => p));
        this.Execute引数パターン(a => ArrN<float>(a+1).Min(p => p));
        this.Execute引数パターン(a => ArrN<int>(a+1).Min(p => p));
        this.Execute引数パターン(a => ArrN<long>(a+1).Min(p => p));
        this.Execute引数パターン(a => new[] { "A" }.Min(p => p));
        //    }
        //    if(
        //        Reflection.ExtendSet1            .MinNullableSelector       ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MinDecimalNullableSelector==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MinDoubleNullableSelector ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MinSingleNullableSelector ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MinInt32NullableSelector  ==MethodCall0_GenericMethodDefinition||
        //        Reflection.ExtendEnumerable.MinInt64NullableSelector  ==MethodCall0_GenericMethodDefinition
        //    ) {
        this.Execute引数パターン(a => SetN<decimal>(a).Min(p => (decimal?)p));
        this.Execute引数パターン(a => ArrN<decimal>(a).Min(p => (decimal?)p));
        this.Execute引数パターン(a => ArrN<double>(a).Min(p => (double?)p));
        this.Execute引数パターン(a => ArrN<float>(a).Min(p => (float?)p));
        this.Execute引数パターン(a => ArrN<int>(a).Min(p => (int?)p));
        this.Execute引数パターン(a => ArrN<long>(a).Min(p => (long?)p));
        //    }
        //    if(Reflection.ExtendSet.VarpDecimal == MethodCall0_GenericMethodDefinition || Reflection.ExtendSet.VarpDouble == MethodCall0_GenericMethodDefinition) {
        var SetDecimal = new Set<decimal> { 1,2,3 };
        var SetDouble = new Set<double> { 1,2,3 };
        this.Execute引数パターン(a => SetDecimal.Varp(p => p+1));
        this.Execute引数パターン(a => SetDouble.Varp(p => p+1));
        //    }
        //    if(Reflection.ExtendSet.VarDecimal == MethodCall0_GenericMethodDefinition || Reflection.ExtendSet.VarDouble == MethodCall0_GenericMethodDefinition) {
        this.Execute引数パターン(a => SetDecimal.Var(p => p+1));
        this.Execute引数パターン(a => SetDouble.Var(p => p+1));
        //    }
        //    if(SequenceEqual0か||Reflection.ExtendEnumerable.SequenceEqual1==MethodCall0_GenericMethodDefinition){
        //        if(SequenceEqual0か) {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<decimal>(a).SequenceEqual(ArrN<decimal>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<decimal>(a).SequenceEqual(EnuN<decimal>(b)));
        //        }else{
        //            if(comparer_NodeType==ExpressionType.Parameter){
        this.Execute引数パターン標準ラムダループ((a,b) => EqualityComparer<decimal>.Default.Let(p => ArrN<decimal>(a).SequenceEqual(ArrN<decimal>(b),p)));
        //            }else{
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<decimal>(a).SequenceEqual(ArrN<decimal>(b),EqualityComparer<decimal>.Default));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<decimal>(a).SequenceEqual(EnuN<decimal>(b),EqualityComparer<decimal>.Default));
        //            }
        //        }
        //    }
        //if(Reflection.ExtendSet1.Single==MethodCall0_GenericMethodDefinition||Reflection.ExtendEnumerable.Single==MethodCall0_GenericMethodDefinition) {
        this.Execute標準ラムダループ(() => ArrN<decimal>(1).Single());
        this.Execute標準ラムダループ(() => EnuN<decimal>(1).Single());
        this.Execute標準ラムダループ(() => SetN<decimal>(1).Single());
        //}
        //if(Reflection.ExtendSet1.SingleOrDefault==MethodCall0_GenericMethodDefinition||Reflection.ExtendSet1.SingleOrDefault_defaultValue==MethodCall0_GenericMethodDefinition||Reflection.ExtendEnumerable.SingleOrDefault==MethodCall0_GenericMethodDefinition) {
        this.Execute標準ラムダループ(() => ArrN<decimal>(0).SingleOrDefault());
        this.Execute標準ラムダループ(() => ArrN<decimal>(1).SingleOrDefault());
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<decimal>(a).SingleOrDefault(p => p==b));
        this.Execute標準ラムダループ(() => EnuN<decimal>(0).SingleOrDefault());
        this.Execute標準ラムダループ(() => EnuN<decimal>(1).SingleOrDefault());
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<decimal>(a).SingleOrDefault(p => p==b));
        this.Execute標準ラムダループ(() => SetN<decimal>(0).SingleOrDefault());
        this.Execute標準ラムダループ(() => SetN<decimal>(0).SingleOrDefault(1m));
        this.Execute標準ラムダループ(() => SetN<decimal>(1).SingleOrDefault());
        this.Execute標準ラムダループ(() => SetN<decimal>(1).SingleOrDefault(1m));
        //}
        //if(
        //    Reflection.ExtendSet1            .SumInt32Selector          ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendSet1            .SumInt64Selector          ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendSet1            .SumDecimalSelector        ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendSet1            .SumDoubleSelector         ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendSet1            .SumSingleSelector         ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.SumInt32Selector          ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.SumInt64Selector          ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.SumDecimalSelector        ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.SumDoubleSelector         ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.SumSingleSelector         ==MethodCall0_GenericMethodDefinition
        //) {
        this.Execute引数パターン(a => ArrN<decimal>(a).Sum(p => p+1));
        this.Execute引数パターン(a => ArrN<double>(a).Sum(p => p+1));
        this.Execute引数パターン(a => ArrN<float>(a).Sum(p => p+1));
        this.Execute引数パターン(a => ArrN<long>(a).Sum(p => p+1));
        this.Execute引数パターン(a => ArrN<int>(a).Sum(p => p+1));
        this.Execute引数パターン(a => EnuN<decimal>(a).Sum(p => p+1));
        this.Execute引数パターン(a => EnuN<double>(a).Sum(p => p+1));
        this.Execute引数パターン(a => EnuN<float>(a).Sum(p => p+1));
        this.Execute引数パターン(a => EnuN<long>(a).Sum(p => p+1));
        this.Execute引数パターン(a => EnuN<int>(a).Sum(p => p+1));
        this.Execute引数パターン(a => SetN<decimal>(a).Sum(p => p+1));
        this.Execute引数パターン(a => SetN<double>(a).Sum(p => p+1));
        this.Execute引数パターン(a => SetN<float>(a).Sum(p => p+1));
        this.Execute引数パターン(a => SetN<long>(a).Sum(p => p+1));
        this.Execute引数パターン(a => SetN<int>(a).Sum(p => p+1));
        //}
        //if(
        //    Reflection.ExtendEnumerable.SumInt32NullableSelector  ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.SumInt64NullableSelector  ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.SumDecimalNullableSelector==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.SumDoubleNullableSelector ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.SumSingleNullableSelector ==MethodCall0_GenericMethodDefinition
        //) {
        this.Execute引数パターン(a => ArrN<int>(a).Sum(p => (int?)p));
        this.Execute引数パターン(a => ArrN<long>(a).Sum(p => (long?)p));
        this.Execute引数パターン(a => ArrN<decimal>(a).Sum(p => (decimal?)p));
        this.Execute引数パターン(a => ArrN<double>(a).Sum(p => (double?)p));
        this.Execute引数パターン(a => ArrN<float>(a).Sum(p => (float?)p));
        //}
        //if(
        //    Reflection.ExtendSet1            .SumInt32          ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendSet1            .SumInt64          ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendSet1            .SumDecimal        ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendSet1            .SumDouble         ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendSet1            .SumSingle         ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.SumInt32          ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.SumInt64          ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.SumDecimal        ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.SumDouble         ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.SumSingle         ==MethodCall0_GenericMethodDefinition
        //) {
        this.Execute引数パターン(a => SetN<int>(a).Sum());
        this.Execute引数パターン(a => SetN<long>(a).Sum());
        this.Execute引数パターン(a => SetN<decimal>(a).Sum());
        this.Execute引数パターン(a => SetN<double>(a).Sum());
        this.Execute引数パターン(a => SetN<float>(a).Sum());
        this.Execute引数パターン(a => ArrN<int>(a).Sum());
        this.Execute引数パターン(a => ArrN<long>(a).Sum());
        this.Execute引数パターン(a => ArrN<decimal>(a).Sum());
        this.Execute引数パターン(a => ArrN<double>(a).Sum());
        this.Execute引数パターン(a => ArrN<float>(a).Sum());
        //}
        //if(
        //    Reflection.ExtendEnumerable.SumInt32Nullable  ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.SumInt64Nullable  ==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.SumDecimalNullable==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.SumDoubleNullable ==MethodCall0_GenericMethodDefinition
        //) {
        this.Execute引数パターン(a => ArrN<int?>(a).Sum());
        this.Execute引数パターン(a => ArrN<long?>(a).Sum());
        this.Execute引数パターン(a => ArrN<decimal?>(a).Sum());
        this.Execute引数パターン(a => ArrN<double?>(a).Sum());
        this.Execute引数パターン(a => ArrN<float?>(a).Sum());
        //}
        //if(Set1_KeyDictionary64か||Reflection.ExtendEnumerable.KeyDictionary64==MethodCall0_GenericMethodDefinition) {
        //    if(Lambda!=null&&Lambda.Body==Lambda.Parameters[0]) {
        this.Execute引数パターン(a => Lambda(b => SetN<int>(a).Where(p => p==a)));
        this.Execute引数パターン(a => Lambda(b => ArrN<int>(a).Where(p => p==a)));
        //    }else{
        //this.変数Cache.Execute(()=>Lambda(a=>Lambda(b=>Set変数1<Int32>().DictionarySet(p=>p))));
        //this.変数Cache.Execute(()=>Lambda(a=>Lambda(b=>Arr変数<Int32>().DictionaryList(p=>p))));
        this.Execute標準ラムダループ(() => Lambda(a => Lambda(b => SetN<int>(a).Where(p => p+1==a))));
        this.Execute標準ラムダループ(() => Lambda(a => Lambda(b => ArrN<int>(a).Where(p => p+1==a))));
        //    }
        //}
        //if(Reflection.ExtendSet1.SymmetricExcept==MethodCall0_GenericMethodDefinition){
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<decimal>(a).SymmetricExcept(SetN<decimal>(b)));
        //}
        //if(Reflection.ExtendEnumerable.ToArray==MethodCall0_GenericMethodDefinition){
        this.ToArray();
        //}
        //if(
        //    Reflection.ExtendEnumerable.OrderBy0==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.OrderBy1==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.OrderByDescending0==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.OrderByDescending1==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.ThenBy0==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.ThenBy1==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.ThenByDescending0==MethodCall0_GenericMethodDefinition||
        //    Reflection.ExtendEnumerable.ThenByDescending1==MethodCall0_GenericMethodDefinition
        //) {
        this.Execute引数パターン(a => ArrN<int>(a).OrderBy(p => p));
        this.Execute引数パターン(a => ArrN<int>(a).OrderBy(p => p,Comparer<int>.Default));
        this.Execute引数パターン(a => ArrN<int>(a).OrderByDescending(p => p));
        this.Execute引数パターン(a => ArrN<int>(a).OrderByDescending(p => p,Comparer<int>.Default));
        this.Execute引数パターン(a => ArrN<int>(a).OrderBy(p => p).ThenBy(p => p));
        this.Execute引数パターン(a => ArrN<int>(a).OrderBy(p => p).ThenBy(p => p,Comparer<int>.Default));
        this.Execute引数パターン(a => ArrN<int>(a).OrderBy(p => p).ThenByDescending(p => p));
        this.Execute引数パターン(a => ArrN<int>(a).OrderBy(p => p).ThenByDescending(p => p,Comparer<int>.Default));
        //}
        //if(ExtendedSet.ループ展開可能なEnumerableSetに属するGenericMethodDefinitionか(MethodCall0_GenericMethodDefinition)) {
        //    if(MethodCall0_Method.DeclaringType==typeof(Set)) {
        this.Execute引数パターン(a => SetN<int>(a).Select(p => p+2));
        //    } else {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Intersect(ArrN<int>(b)));
        this.Execute引数パターン(a => ArrN<int>(a).Select(p => p+2));
        //    }
        //}
        this.Execute引数パターン(a => 展開しないメソッド(SetN<int>(a)));
    }
    private static T 展開しないメソッド<T>(T a) => a;
    [TestMethod]
    public void Average() {
        this.Execute引数パターン(a => ArrN<decimal>(a+1).Average());
        this.Execute引数パターン(a => ArrN<double>(a+1).Average());
        this.Execute引数パターン(a => ArrN<float>(a+1).Average());
        this.Execute引数パターン(a => ArrN<long>(a+1).Average());
        this.Execute引数パターン(a => ArrN<int>(a+1).Average());
        this.Execute引数パターン(a => EnuN<decimal>(a+1).Average());
        this.Execute引数パターン(a => EnuN<double>(a+1).Average());
        this.Execute引数パターン(a => EnuN<float>(a+1).Average());
        this.Execute引数パターン(a => EnuN<long>(a+1).Average());
        this.Execute引数パターン(a => EnuN<int>(a+1).Average());
        this.Execute引数パターン(a => SetN<decimal>(a+1).Average());
        this.Execute引数パターン(a => SetN<double>(a+1).Average());
        this.Execute引数パターン(a => SetN<float>(a+1).Average());
        this.Execute引数パターン(a => SetN<long>(a+1).Average());
        this.Execute引数パターン(a => SetN<int>(a+1).Average());
        //if(ループ展開可能なSetMethodか(MethodCall0_Arguments_0)) {
        //    if(Set固有かEnamurableか(MethodCall0_Arguments_0)) {
        this.Execute引数パターン(a => ArrN<decimal>(a+1).Where(p => p%2==0).Average());
        this.Execute引数パターン(a => EnuN<decimal>(a+1).Where(p => p%2==0).Average());
        this.Execute引数パターン(a => SetN<decimal>(a+1).Where(p => p%2==0).Average());
        //    }else{
        this.Execute引数パターン(a => ArrN<decimal>(a+1).Select(p => p+1).Average());
        this.Execute引数パターン(a => EnuN<decimal>(a+1).Select(p => p+1).Average());
        this.Execute引数パターン(a => SetN<decimal>(a+1).Select(p => p+1).Average());
        //    }
        //}else{
        //    if(MethodCall0_Arguments_0.NodeType==ExpressionType.Parameter){
        //        if(中間Type!=argument.Type) {
        this.Execute引数パターン(a => ArrN<float>(a+1).Let(p => p.Average()));
        this.Execute引数パターン(a => EnuN<float>(a+1).Let(p => p.Average()));
        this.Execute引数パターン(a => SetN<float>(a+1).Let(p => p.Average()));
        //        }
        this.Execute引数パターン(a => ArrN<decimal>(a+1).Let(p => p.Average()));
        this.Execute引数パターン(a => EnuN<decimal>(a+1).Let(p => p.Average()));
        this.Execute引数パターン(a => SetN<decimal>(a+1).Let(p => p.Average()));
        //    }else{
        this.Execute引数パターン(a => ArrN<decimal>(a+1).Average());
        this.Execute引数パターン(a => EnuN<decimal>(a+1).Average());
        this.Execute引数パターン(a => SetN<decimal>(a+1).Average());
        //    }
        //}
    }
    [TestMethod]
    public void Average_selector() {
        this.Execute引数パターン(a => ArrN<int>(a+1).Average(q => (decimal)q+1));
        this.Execute引数パターン(a => ArrN<int>(a+1).Average(q => (double)q+1));
        this.Execute引数パターン(a => ArrN<int>(a+1).Average(q => (float)q+1));
        this.Execute引数パターン(a => ArrN<int>(a+1).Average(q => (long)q+1));
        this.Execute引数パターン(a => ArrN<int>(a+1).Average(q => (int)q+1));
        this.Execute引数パターン(a => EnuN<int>(a+1).Average(q => (decimal)q+1));
        this.Execute引数パターン(a => EnuN<int>(a+1).Average(q => (double)q+1));
        this.Execute引数パターン(a => EnuN<int>(a+1).Average(q => (float)q+1));
        this.Execute引数パターン(a => EnuN<int>(a+1).Average(q => (long)q+1));
        this.Execute引数パターン(a => EnuN<int>(a+1).Average(q => (int)q+1));
        this.Execute引数パターン(a => SetN<int>(a+1).Average(q => (decimal)q+1));
        this.Execute引数パターン(a => SetN<int>(a+1).Average(q => (double)q+1));
        this.Execute引数パターン(a => SetN<int>(a+1).Average(q => (float)q+1));
        this.Execute引数パターン(a => SetN<int>(a+1).Average(q => (long)q+1));
        this.Execute引数パターン(a => SetN<int>(a+1).Average(q => (int)q+1));
        //if(ループ展開可能なSetMethodか(MethodCall0_Arguments_0)) {
        //    if(Set固有かEnamurableか(MethodCall0_Arguments_0)) {
        this.Execute引数パターン(a => SetN<decimal>(a+1).Where(p => p%2==0).Average(q => q+1));
        //    }else{
        this.Execute引数パターン(a => SetN<decimal>(a+1).Select(p => p+1).Average(q => q+1));
        //    }
        //}else{
        //    if(MethodCall0_Arguments_0.NodeType==ExpressionType.Parameter){
        this.Execute引数パターン(a => SetN<decimal>(a+1).Let(p => p.Average(q => q+1)));
        //    }else{
        this.Execute引数パターン(a => SetN<decimal>(a+1).Average(q => q+1));
        //    }
        //}
    }
    public static decimal? NullDeci(decimal? p) {
        return p*p;
    }
    [TestMethod]
    public void NullableTest() {
        {
#pragma warning disable 219
            decimal? a = 1, b = 2;
#pragma warning restore 219
            //this.変数Cache.Execute(()=>a+b+c);
            //this.変数Cache.Execute(()=>a+b);
            this.Execute標準ラムダループ(() => -a);
        }
        {
            decimal? a = 1, b = 2, c = 3;
            this.Execute標準ラムダループ(() =>
                a.HasValue ?
                    b.HasValue ?
                        c.HasValue ?
                            a.Value
                            :
                            default(decimal?)
                        :
                        default
                    :
                    default
            );
            this.Execute標準ラムダループ(() => a.HasValue&&b.HasValue&&c.HasValue ? a.Value+b.Value+c.Value : default(decimal?));
            this.Execute標準ラムダループ(() => a.HasValue&&b.HasValue ? a.Value+b.Value : default(decimal?));
        }
    }
    public struct Nullable2<T> where T : struct {
        private readonly bool hasValue;
        internal readonly T value;
        public bool HasValue => this.hasValue;

        public T Value {
            get {
                if(!this.hasValue) {
                }
                return this.value;
            }
        }
        public Nullable2(T value) {
            this.value=value;
            this.hasValue=true;
        }
        public T GetValueOrDefault() {
            return this.value;
        }
        public T GetValueOrDefault(T defaultValue) {
            if(!this.hasValue) {
                return defaultValue;
            }
            return this.value;
        }
        public override bool Equals(object? other) {
            if(!this.hasValue) {
                return other==null;
            }
            return other!=null&&this.value.Equals(other);
        }
        public override int GetHashCode() {
            if(!this.hasValue) {
                return 0;
            }
            return this.value.GetHashCode();
        }
        public override string ToString() {
            if(!this.hasValue) {
                return "";
            }
            return this.value.ToString();
        }
        public static implicit operator Nullable2<T>(T value) {
            return new Nullable2<T>(value);
        }
        public static explicit operator T(Nullable2<T> value) {
            return value.Value;
        }
    }
    [TestMethod]
    public void GeomeanDoubleNullable_selector() {
        var Set = new Set<double?>{
            1,
            2,
            3,
            4
        };
        double? d = 2;
        var decimalSet = Set;
        foreach(var d2 in decimalSet) {
            var num3 = d2*d;
            if(num3.HasValue) {
            }
        }
        this.Execute標準ラムダループ(() => Set.Geomean(p => p*2));
        //if(ループ展開可能なSetMethodか(MethodCall0_Arguments_0)) {
        //    if(Set固有かEnamurableか(MethodCall0_Arguments_0)) {
        this.Execute標準ラムダループ(() => Set.Where(p => p!=0).Geomean(p => p*2));
        //    } else {
        this.Execute標準ラムダループ(() => Set.Select(p => p+1).Geomean(p => p*2));
        //    }
        //} else {
        //    if(MethodCall0_Arguments_0.NodeType==ExpressionType.Parameter) {
        this.Execute標準ラムダループ(() => Set.Let(q => q.Geomean(p => p*2)));
        //    } else {
        this.Execute標準ラムダループ(() => Set.Let(q => q).Geomean(p => p*2));
        //    }
    }
    [TestMethod]
    public void GeomeanDouble_selector() {
        var Set = new Set<double>{
            1,
            2,
            3,
            4
        };
        this.Execute標準ラムダループ(() => Set.Geomean(p => p*2));
        //if(ループ展開可能なSetMethodか(MethodCall0_Arguments_0)) {
        //    if(Set固有かEnamurableか(MethodCall0_Arguments_0)) {
        this.Execute標準ラムダループ(() => Set.Where(p => p>3.0).Geomean(p => p*2));
        //    } else {
        this.Execute標準ラムダループ(() => Set.Select(p => p+1).Geomean(p => p*2));
        //    }
        //} else {
        //    if(MethodCall0_Arguments_0.NodeType==ExpressionType.Parameter) {
        this.Execute標準ラムダループ(() => Set.Let(q => q.Geomean(p => p*2)));
        //    } else {
        this.Execute標準ラムダループ(() => Set.Let(q => q).Geomean(p => p*2));
        //    }
    }
    private static double? NullableDoubleからNullableDecimal(decimal? p) {
        return (double?)p;
    }
    [TestMethod]
    public void HarmeanNullable_selector() {
        //   this.AssertExecute(() => DecimalSet.Harmean(p =>(Double?)p * 2));
        this.Execute引数パターン(a => SetN<decimal?>(a).Harmean(p => (double?)p+1));
        this.Execute引数パターン(a => SetN<decimal?>(a).Harmean(p => p+1));
        //if(ループ展開可能なSetMethodか(MethodCall0_Arguments_0)) {
        //    if(Set固有かEnamurableか(MethodCall0_Arguments_0)) {
        this.Execute引数パターン(a => SetN<decimal?>(a).Where(p => p>=0).Harmean(p => p+1));
        //    } else {
        this.Execute引数パターン(a => SetN<decimal?>(a).Select(p => p+1).Harmean(p => p+1));
        //    }
        //} else {
        //    if(MethodCall0_Arguments_0.NodeType==ExpressionType.Parameter) {
        this.Execute引数パターン(a => SetN<decimal?>(a).Let(q => q.Harmean(p => p+1)));
        //    } else {
        this.Execute引数パターン(a => SetN<decimal?>(a).Let(q => q).Harmean(p => p+1));
        //    }
    }
    [TestMethod]
    public void Harmean_selector() {
        this.Execute引数パターン(a => SetN<decimal>(a+1).Harmean(p => (double)p+1));
        this.Execute引数パターン(a => SetN<decimal>(a+1).Harmean(p => p+1));
        //if(ループ展開可能なSetMethodか(MethodCall0_Arguments_0)) {
        //    if(Set固有かEnamurableか(MethodCall0_Arguments_0)) {
        this.Execute引数パターン(a => SetN<decimal>(a+1).Where(p => p>=0).Harmean(p => p+1));
        //    } else {
        this.Execute引数パターン(a => SetN<decimal>(a+1).Select(p => p+1).Harmean(p => p+1));
        //    }
        //} else {
        //    if(MethodCall0_Arguments_0.NodeType==ExpressionType.Parameter) {
        this.Execute引数パターン(a => SetN<decimal>(a+1).Let(q => q.Harmean(p => p+1)));
        //    } else {
        this.Execute引数パターン(a => SetN<decimal>(a+1).Let(q => q).Harmean(p => p+1));
        //    }
    }
    [TestMethod]
    public void LambdaExpressionを展開1() {
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Except(SetN<int>(b).Select(p => p+1)));
    }
    [TestMethod]
    public void LambdaExpressionを展開2() {
        //if(Lambda2!=null) {
        this.Execute引数パターン(a => ArrN<int>(a).Select((p,i) => p+i));
        //}else{
        this.Execute引数パターン(a => ArrN<int>(a).Select((Func<int,int,int>)((p,i) => p+i)));
        //}
    }
    [TestMethod]
    public void ListからArrayにコピー() {
        this.Execute引数パターン(a => ArrN<int>(a).ToArray());
    }
    [TestMethod]
    public void LongCount0() {
        const int N = 8;
        Expression<Func<int,long>> Lambda = b => SetN<int>(b).LongCount();
        var ExpectedDelegate = Lambda.Compile();
        var Optimizer = this.Optimizer;
        //Optimizer.OptimizeLevel=OptimizeLevels.None;
        var ActualDelegate0 = Optimizer.CreateDelegate(Lambda);
        //Optimizer.OptimizeLevel=OptimizeLevels.ループ融合;
        var ActualDelegate1 = Optimizer.CreateDelegate(Lambda);
        for(var a = 0;a<N;a++) {
            var expected = ExpectedDelegate(a);
            var actual0 = ActualDelegate0(a);
            var actual1 = ActualDelegate1(a);
            Console.WriteLine("a="+a.ToString()+":"+expected.ToString()+","+actual0.ToString()+","+actual1);
            Assert.IsTrue(Comparer.Equals(expected,actual0));
            Assert.IsTrue(Comparer.Equals(actual0,actual1));
        }
        this.Execute引数パターン(a => SetN<int>(a).LongCount());
        this.Execute引数パターン(a => SetN<int>(a).Select(p => p+1).LongCount());
        this.Execute引数パターン(a => ArrN<int>(a).LongCount());
        var Set = new Set<int> { 1,2 };
        var Arr = new Set<int> { 1,2 };
        this.Execute標準ラムダループ(() => Set.LongCount());
        this.Execute標準ラムダループ(() => Arr.LongCount());
    }
    [TestMethod]
    public void Empty() {
        this.Execute標準ラムダループ(() => Enumerable.Empty<int>());
    }
    [TestMethod]
    public void MaxMin() {
        //argument=>{
        //    if(NodeType==ExpressionType.LessThan&&ElementType.GetMethod(op_LessThan)==null||NodeType==ExpressionType.GreaterThan&&ElementType.GetMethod(op_GreaterThan)==null){
        this.Execute標準ラムダループ(() => new[] { "A","B" }.Min());
        this.Execute標準ラムダループ(() => new[] { "A","B" }.Max());
        //    } else{
        this.Execute引数パターン(a => ArrN<decimal>(a+1).Min());
        this.Execute引数パターン(a => ArrN<decimal>(a+1).Max());
        this.Execute引数パターン(a => EnuN<decimal>(a+1).Min());
        this.Execute引数パターン(a => EnuN<decimal>(a+1).Max());
        this.Execute引数パターン(a => SetN<decimal>(a+1).Min());
        this.Execute引数パターン(a => SetN<decimal>(a+1).Max());
        //    }
        //}
        this.Execute引数パターン(a => ArrN<decimal>(a+1).Min());
        this.Execute引数パターン(a => ArrN<decimal>(a+1).Max());
        this.Execute引数パターン(a => EnuN<decimal>(a+1).Min());
        this.Execute引数パターン(a => EnuN<decimal>(a+1).Max());
        this.Execute引数パターン(a => SetN<decimal>(a+1).Min());
        this.Execute引数パターン(a => SetN<decimal>(a+1).Max());

        this.Execute引数パターン(a => ArrN<double>(a+1).Min());
        this.Execute引数パターン(a => ArrN<double>(a+1).Max());
        this.Execute引数パターン(a => EnuN<double>(a+1).Min());
        this.Execute引数パターン(a => EnuN<double>(a+1).Max());
        this.Execute引数パターン(a => SetN<double>(a+1).Min());
        this.Execute引数パターン(a => SetN<double>(a+1).Max());

        this.Execute引数パターン(a => ArrN<float>(a+1).Min());
        this.Execute引数パターン(a => ArrN<float>(a+1).Max());
        this.Execute引数パターン(a => EnuN<float>(a+1).Min());
        this.Execute引数パターン(a => EnuN<float>(a+1).Max());
        this.Execute引数パターン(a => SetN<float>(a+1).Min());
        this.Execute引数パターン(a => SetN<float>(a+1).Max());

        this.Execute引数パターン(a => ArrN<long>(a+1).Min());
        this.Execute引数パターン(a => ArrN<long>(a+1).Max());
        this.Execute引数パターン(a => EnuN<long>(a+1).Min());
        this.Execute引数パターン(a => EnuN<long>(a+1).Max());
        this.Execute引数パターン(a => SetN<long>(a+1).Min());
        this.Execute引数パターン(a => SetN<long>(a+1).Max());

        this.Execute引数パターン(a => ArrN<int>(a+1).Min());
        this.Execute引数パターン(a => ArrN<int>(a+1).Max());
        this.Execute引数パターン(a => EnuN<int>(a+1).Min());
        this.Execute引数パターン(a => EnuN<int>(a+1).Max());
        this.Execute引数パターン(a => SetN<int>(a+1).Min());
        this.Execute引数パターン(a => SetN<int>(a+1).Max());

        this.Execute引数パターン(a => ArrN<decimal>(a+1).Min());
        this.Execute引数パターン(a => ArrN<decimal>(a+1).Max());
        this.Execute引数パターン(a => EnuN<decimal>(a+1).Min());
        this.Execute引数パターン(a => EnuN<decimal>(a+1).Max());
        this.Execute引数パターン(a => SetN<decimal>(a+1).Min());
        this.Execute引数パターン(a => SetN<decimal>(a+1).Max());

        this.Execute引数パターン(a => ArrN<decimal?>(a).Min());
        this.Execute引数パターン(a => ArrN<decimal?>(a).Max());
        this.Execute引数パターン(a => EnuN<decimal?>(a).Min());
        this.Execute引数パターン(a => EnuN<decimal?>(a).Max());
        this.Execute引数パターン(a => SetN<decimal?>(a).Min());
        this.Execute引数パターン(a => SetN<decimal?>(a).Max());

        this.Execute引数パターン(a => ArrN<double?>(a).Min());
        this.Execute引数パターン(a => ArrN<double?>(a).Max());
        this.Execute引数パターン(a => EnuN<double?>(a).Min());
        this.Execute引数パターン(a => EnuN<double?>(a).Max());
        this.Execute引数パターン(a => SetN<double?>(a).Min());
        this.Execute引数パターン(a => SetN<double?>(a).Max());

        this.Execute引数パターン(a => ArrN<float?>(a).Min());
        this.Execute引数パターン(a => ArrN<float?>(a).Max());
        this.Execute引数パターン(a => EnuN<float?>(a).Min());
        this.Execute引数パターン(a => EnuN<float?>(a).Max());
        this.Execute引数パターン(a => SetN<float?>(a).Min());
        this.Execute引数パターン(a => SetN<float?>(a).Max());

        this.Execute引数パターン(a => ArrN<long?>(a).Min());
        this.Execute引数パターン(a => ArrN<long?>(a).Max());
        this.Execute引数パターン(a => EnuN<long?>(a).Min());
        this.Execute引数パターン(a => EnuN<long?>(a).Max());
        this.Execute引数パターン(a => SetN<long?>(a).Min());
        this.Execute引数パターン(a => SetN<long?>(a).Max());

        this.Execute引数パターン(a => ArrN<int?>(a).Min());
        this.Execute引数パターン(a => ArrN<int?>(a).Max());
        this.Execute引数パターン(a => EnuN<int?>(a).Min());
        this.Execute引数パターン(a => EnuN<int?>(a).Max());
        this.Execute引数パターン(a => SetN<int?>(a).Min());
        this.Execute引数パターン(a => SetN<int?>(a).Max());
    }
    [TestMethod]
    public void MaxMin_selector() {
        //argument=>{
        //    if(NodeType==ExpressionType.LessThan&&ElementType.GetMethod(op_LessThan)==null||NodeType==ExpressionType.GreaterThan&&ElementType.GetMethod(op_GreaterThan)==null){
        this.Execute標準ラムダループ(() => new[] { "A","B" }.Min(p => p));
        this.Execute標準ラムダループ(() => new[] { "A","B" }.Max(p => p));
        //    } else{
        this.Execute引数パターン(a => SetN<decimal>(a+1).Min(p => p));
        this.Execute引数パターン(a => SetN<decimal>(a+1).Max(p => p));
        //    }
        //}
    }
    [TestMethod]
    public void MaxMinNullable() {
        this.Execute引数パターン(a => ArrN<decimal?>(a).Max());
        this.Execute引数パターン(a => ArrN<decimal?>(a).Min());
    }
    [TestMethod]
    public void MaxMinNullable_selector() {
        this.Execute引数パターン(a => SetN<decimal>(a+1).Max(p => (decimal?)p));
        this.Execute引数パターン(a => SetN<decimal>(a+1).Min(p => (decimal?)p));
    }
    [TestMethod]
    public void Set結果で重複除去されているか(){
        //if(MethodCall!=null) {
        //    if(Reflection.ExtendSet.Except==GenericMethodDefinition)
        this.Execute引数パターン(a => SetN<int>(a).Except(SetN<int>(a)).SingleOrDefault());
        this.Execute引数パターン(a => SetN<int>(a+1).Except(SetN<int>(a)).SingleOrDefault());
        //    if(Reflection.ExtendSet.GroupBy_keySelector==GenericMethodDefinition)
        this.Execute引数パターン(a => SetN<int>(a).GroupBy(p => p.ToString().Length).SingleOrDefault());
        //    if(Reflection.ExtendSet1.GroupBy_keySelector_elementSelector.MethodEquals(MethodCall_Method))
        this.Execute引数パターン(a => SetN<int>(a).GroupBy(p => p.ToString().Length,p => p+1).SingleOrDefault());
        //    if(Reflection.ExtendSet.GroupJoin_Dictionary==GenericMethodDefinition) {
        //        if(resultSelector==null) return false;
        this.Execute標準ラムダループ(() => SetN<int>(1).GroupJoin(SetN<int>(1),o => o,i => i,(Func<int,ImmutableSet<int>,int>)((o,i) => o)).SingleOrDefault());
        this.Execute標準ラムダループ(() => SetN<int>(1).GroupJoin(SetN<int>(1),o => o,i => i,(o,i) => o).SingleOrDefault());
        this.Execute標準ラムダループ(() => SetN<int>(0).GroupJoin(SetN<int>(1),o => o,i => i,(Func<int,ImmutableSet<int>,int>)((o,i) => o)).SingleOrDefault());
        this.Execute標準ラムダループ(() => SetN<int>(0).GroupJoin(SetN<int>(1),o => o,i => i,(o,i) => o).SingleOrDefault());
        //    }
        //    if(Reflection.ExtendSet.Intersect==GenericMethodDefinition) {
        //        if(this.Setメソッドで結果が重複除去されているか(Arguments[0])) return true;
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Intersect(SetN<int>(b)).Where(p => p==0).SingleOrDefault());
        this.Execute引数パターン標準ラムダループ((a,b,c) => SetN<int>(1).Union(SetN<int>(1)).Intersect(SetN<int>(1)).Where(p => p==0).SingleOrDefault());
        //    }
        //    if(Reflection.ExtendSet.Join_Dictionary==GenericMethodDefinition) {
        //        if(resultSelector==null) return false;
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => 0,i => 1,(Func<int,int,int>)((o,i) => o+i)).SingleOrDefault());
        //            return this.変数_判定_指定PrimaryKeyが存在する.実行(resultSelector.Body,resultSelector.Parameters[0])&&
        //                   this.変数_判定_指定PrimaryKeyが存在する.実行(resultSelector.Body,resultSelector.Parameters[1]);
        //        if(!this.変数_判定_指定PrimaryKeyが存在する.実行(resultSelector.Body,resultSelector.Parameters[0])) return false;
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => 0,i => 1,(o,i) => o).SingleOrDefault());
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => 0,i => 1,(o,i) => i).SingleOrDefault());
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => 0,i => 1,(o,i) => o+i).SingleOrDefault());
        //    }            //    if(Reflection.ExtendSet.Select_selector==GenericMethodDefinition) {
        //        if(selector==null) return false;
        this.Execute標準ラムダループ(() => SetN<int>(1).Select((Func<int,int>)(p => p+1)).SingleOrDefault());
        this.Execute標準ラムダループ(() => SetN<int>(1).Select(p => p+1).SingleOrDefault());
        this.Execute標準ラムダループ(() => SetN<int>(0).Select((Func<int,int>)(p => p+1)).SingleOrDefault());
        this.Execute標準ラムダループ(() => SetN<int>(0).Select(p => p+1).SingleOrDefault());
        //    }
        //    if(Reflection.ExtendSet.SelectMany_selector==GenericMethodDefinition) return false;
        this.Execute引数パターン(a => SetN<int>(a).SelectMany(p => SetN<int>(0)).SingleOrDefault());
        //    if(Reflection.ExtendSet.Union==GenericMethodDefinition) return false;
        this.Execute標準ラムダループ(() => SetN<int>(1).Union(SetN<int>(1)).SingleOrDefault());
        //    if(Reflection.ExtendSet.Where==GenericMethodDefinition) {
        this.Execute引数パターン(a => SetN<int>(a).Where(p => p==1).SingleOrDefault());
        //    }
        //}
        this.Execute標準ラムダループ(() => SetN<int>(1).Let(p => p).SingleOrDefault());
        this.Execute標準ラムダループ(() => SetN<int>(0).Let(p => p).SingleOrDefault());
    }
    [TestMethod]
    public void SingleSingleOrDefault() {
        this.Execute引数パターン(a => SetN<int>(a+0).Except(SetN<int>(a)).SingleOrDefault(3));
        this.Execute引数パターン(a => SetN<int>(a+1).Except(SetN<int>(a)).SingleOrDefault(3));
        this.Execute引数パターン(a => SetN<int>(a+0).Except(SetN<int>(a)).SingleOrDefault());
        this.Execute引数パターン(a => SetN<int>(a+1).Except(SetN<int>(a)).SingleOrDefault());
        //argument=>{
        //    if(this.Setメソッドで結果が重複除去されているか(MethodCall_Arguments_0)) {
        this.Execute引数パターン(a => SetN<int>(a+1).Except(SetN<int>(a)).SingleOrDefault());
        //    } else {
        this.Execute標準ラムダループ(() => SetN<int>(2).Select(p => p+1).Intersect(SetN<int>(1).Select(p => p+1)).SingleOrDefault());
        this.Execute標準ラムダループ(() => SetN<int>(1).Select(p => p+1).Intersect(SetN<int>(1).Select(p => p+1)).SingleOrDefault());
        //    }
        //}
        this.Execute標準ラムダループ(() => SetN<decimal>(1).Single());
        this.Execute標準ラムダループ(() => SetN<decimal>(1).Select(p => p+1).Single());
    }
    [TestMethod]
    public void StdevDouble() {
        this.Execute標準ラムダループ(() => new Set<double> { 1,2,3 }.Stdev());
    }
    [TestMethod]
    public void StdevDouble_selector() {
        this.Execute標準ラムダループ(() => new Set<double> { 1,2,3 }.Stdev(p => p+1));
    }
    [TestMethod]
    public void Sum() {
        //if(Set固有かEnamurableか(MethodCall_Arguments_0)){
        this.Execute引数パターン(a => SetN<int>(a).Sum());
        //} else {
        this.Execute引数パターン(a => SetN<int>(a).Select(p => p+1).Sum());
        //}
    }
    [TestMethod]
    public void Sum_selector() {
        this.Execute引数パターン(a => ArrN<int>(a).Sum(p => p+1));
        this.Execute引数パターン(a => EnuN<int>(a).Sum(p => p+1));
        this.Execute引数パターン(a => SetN<int>(a).Sum(p => p+1));
    }
    [TestMethod]
    public void SumNullable() {
        this.Execute引数パターン(a => ArrN<int?>(a).Sum());
        this.Execute引数パターン(a => EnuN<int?>(a).Sum());
        this.Execute引数パターン(a => SetN<int?>(a).Sum());
    }
    [TestMethod]
    public void SumNullable_selector() {
        this.Execute引数パターン(a => ArrN<int>(a).Sum(p => (int?)p));
    }

    [TestMethod]
    public void SymmetricExcept() {
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).SymmetricExcept(SetN<int>(b)));
    }

    private static int Quote(Expression<Func<int,int>> l) => l.Compile()(3);
    [TestMethod]
    public void Quote() {
        this.Execute標準ラムダループ(() => Quote(p => 1));
    }

    [TestMethod]
    public void ToArray() {
        this.Execute引数パターン(a => ArrN<int>(a).AsEnumerable().ToArray());
    }
    [TestMethod]
    public void Union() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Union(ArrN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Union(EnuN<int>(b)));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Union(SetN<int>(b)));
    }
    [TestMethod]
    public void Union_Any() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Union(ArrN<int>(b)).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Union(EnuN<int>(b)).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Union(SetN<int>(b)).Any());
    }
    [TestMethod]
    public void Where() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Where(p => p==b));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Where(p => p==b));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Where(p => p==b));
    }
    [TestMethod]
    public void Where_index() {
        var data = new int[] { 0,1,2,3,4,5,6,7,8,9 };
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Where((p,i) => p<b));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Where((p,i) => p>b));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Where((p,i) => p==b));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Where((p,i) => p<=b));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Where((p,i) => p>=b));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Where((p,i) => p<b));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Where((p,i) => p>b));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Where((p,i) => p==b));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Where((p,i) => p<=b));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Where((p,i) => p>=b));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Where((Func<int,int,bool>)((p,i) => p+i<b)));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Where((Func<int,int,bool>)((p,i) => p+i>b)));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Where((Func<int,int,bool>)((p,i) => p+i==b)));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Where((Func<int,int,bool>)((p,i) => p+i<=b)));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Where((Func<int,int,bool>)((p,i) => p+i>=b)));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Where((Func<int,int,bool>)((p,i) => p+i<b)));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Where((Func<int,int,bool>)((p,i) => p+i>b)));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Where((Func<int,int,bool>)((p,i) => p+i==b)));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Where((Func<int,int,bool>)((p,i) => p+i<=b)));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Where((Func<int,int,bool>)((p,i) => p+i>=b)));
    }
    [TestMethod]
    public void Where_Any() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Where(p => p==b).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).Where(p => p==b).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Where(p => p==b).Any());
    }
    [TestMethod]
    public void Var_Varp() {
        this.Execute引数パターン(a => SetN<decimal>(a+2).Var(p => p+1));
        this.Execute引数パターン(a => SetN<double>(a+2).Var(p => p+1));
        this.Execute引数パターン(a => SetN<decimal>(a+1).Varp(p => p+1));
        this.Execute引数パターン(a => SetN<double>(a+1).Varp(p => p+1));
    }

    [TestMethod]
    public void 共通MaxMinNullable() {
        this.MaxMinNullable();
    }
}