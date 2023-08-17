using LinqDB.Helpers;
using System.Linq.Expressions;
using LinqDB.Sets;
using static LinqDB.Sets.ExtensionSet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable ConvertNullableToShortForm
// ReSharper disable ExpressionIsAlwaysNull
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable RedundantTypeArgumentsOfMethod
// ReSharper disable RedundantOverflowCheckingContext
// ReSharper disable RedundantCast
namespace CoverageCS.LinqDB.Optimizers.変換_跨ぎ変数の先行評価;

[TestClass]
public class Test_変換_跨ぎ変数の先行評価: ATest
{
    [TestMethod]
    public void 跨ぎ変数パターン() {
        this.Execute引数パターン(a =>
            new {
                A = SetN<int>(a).Join(SetN<int>(a),o => o+3,i => i+2,(o,i) => o+i),
                B = SetN<int>(a).Join(SetN<int>(a),o => o*3,i => i*2,(o,i) => o*i)
            }
        );
        this.Execute引数パターン(a =>
            new {
                A = SetN<int>(a).Join(SetN<int>(a),o => o+1,i => i+1,(o,i) => o+i),
                B = SetN<int>(a).Join(SetN<int>(a),o => o+1,i => i+1,(o,i) => o*i)
            }
        );
        this.Execute引数パターン(a => SetN<int>(a).Join(SetN<int>(a),o => o+1,i => i+1,(o,i) => o+i));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).GroupJoin(ArrN<int>(b),o => o,i => i,(o,i) => new { o,i }));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).GroupJoin(EnuN<int>(b),o => o,i => i,(o,i) => new { o,i }));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).GroupJoin(SetN<int>(b),o => o,i => i,(o,i) => new { o,i }));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).GroupJoin(ArrN<int>(b),o => o,i => i,(o,i) => new { o = o*o,i },AnonymousComparer.Create<int>((x,y) => x==y,p => p.GetHashCode())));
        this.Execute引数パターン標準ラムダループ((a,b) => EnuN<int>(a).GroupJoin(EnuN<int>(b),o => o,i => i,(o,i) => new { o,i },AnonymousComparer.Create<int>((x,y) => x==y,p => p.GetHashCode())));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).GroupJoin(SetN<int>(b),o => o,i => i,(o,i) => new { o,i },AnonymousComparer.Create<int>((x,y) => x==y,p => p.GetHashCode())));
        this.Execute引数パターン標準ラムダループ((a,b) => new {
            A = ArrN<int>(a).GroupJoin(ArrN<int>(b),o => o,i => i,(o,i) => new { o = o+o,i },AnonymousComparer.Create<int>((x,y) => x==y,p => p.GetHashCode())),
            B = ArrN<int>(a).GroupJoin(ArrN<int>(b),o => o,i => i,(o,i) => new { o = o*o,i },AnonymousComparer.Create<int>((x,y) => x==y,p => p.GetHashCode()))
        });
        this.Execute引数パターン(a =>
            ArrN<int>(a).Select(b =>
                new {
                    x = SetN<int>(a).Lookup(c => c).GetTKeyValue(b).Select(d => d+1),
                    y = SetN<int>(a).Lookup(c => c).GetTKeyValue(b).Select(d => d+2)
                }
            )
        );
        this.Execute引数パターン(a =>
            ArrN<int>(a).Select(b =>
                SetN<int>(a).Lookup(c => c).GetTKeyValue(b).Select(d =>d+1)
            )
        );
        this.Execute引数パターン(a =>
            ArrN<int>(a).Select(b =>
                SetN<int>(a).Lookup(c => c).GetTKeyValue(b).SelectMany(d =>
                    SetN<int>(a)
                )
            )
        );
        this.Execute引数パターン(a =>
            ArrN<int>(a).Select(b =>
                SetN<int>(a).Lookup(c => c).GetTKeyValue(b).SelectMany(d => 
                    SetN<int>(a).Where(f => f==5)
                )
            )
        );
        this.Execute引数パターン(a =>
            ArrN<int>(a).Select(b =>
                SetN<int>(a).Lookup(c => c).GetTKeyValue(b).SelectMany(d => b==6
                    ? SetN<int>(a).Where(f => f==5)
                    : ImmutableSet<int>.EmptySet
                )
            )
        );
        this.Execute引数パターン(a =>
            SetN<int>(a).SelectMany(b =>
                SetN<int>(a).Lookup(c => c).GetTKeyValue(b).SelectMany(d => b==6
                    ? SetN<int>(a).Where(f => f==5)
                    : ImmutableSet<int>.EmptySet
                )
            )
        );
        this.Execute引数パターン(_ =>
            (1).Let(a =>
                (2).Let(b => a)
            )
        );
        this.Execute引数パターン(a =>
            ArrN<int>(a).Select(b =>
                Inline(() =>
                    Inline(() =>b)
                )
            )
        );
        this.Execute引数パターン(a =>
            a.Let(b =>
                SetN<int>(a).Lookup(c => c).GetTKeyValue(b)
            )
        );
        this.Execute引数パターン(_ =>
            (1).Let(a =>
                Inline(() => a*2)
            )
        );
        this.Execute引数パターン(_ =>
            (1).Let(a =>
                (2).Let(b => a*2)
            )
        );
        this.Execute引数パターン(_ =>
            (1).Let(a =>
                Inline(() => a)
            )
        );
        this.Execute引数パターン(a =>
            Inline(() =>
                Inline(() =>a)
            )
        );
        this.Execute引数パターン(_ =>
            (1).Let(a =>
                a*2
            )
        );
        this.Execute引数パターン(a =>
            Inline(() =>
                (2).Let(b => a)
            )
        );
        this.Execute引数パターン(b =>
            SetN<int>(3).Lookup(c => c).GetTKeyValue(b).SelectMany(d => 
                SetN<int>(3)
            )
        );
        this.Execute引数パターン(b =>
            SetN<int>(3).Lookup(c => c).GetTKeyValue(b).SelectMany(d => b==6
                ? SetN<int>(3).Where(f => f==5)
                : ImmutableSet<int>.EmptySet
            )
        );
        this.Execute引数パターン(a =>
            SetN<int>(a).SelectMany(b =>
                SetN<int>(a).Where(c => b==c).SelectMany(d =>
                    SetN<int>(a).Where(e => e==5).Where(f => b==6)
                )
            )
        );
        this.Execute引数パターン(a =>
            Inline(() =>
                Inline(() => a*a)
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            Inline(() =>
                Inline(() => (a+a)>b)
            )
        );
        this.Execute引数パターン(_ =>
            SetN<int>(1).SelectMany(a =>
                SetN<int>(2).Where(b => a>b)
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            SetN<int>(1).SelectMany(c =>
                SetN<int>(2).Where(d => d>2&&c>d)
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            SetN<int>(a).Union(SetN<int>(b).Select(c => c+1))
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            SetN<int>(1).SelectMany(c =>
                SetN<int>(2).Where(d => d>2)
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            SetN<int>(a).SelectMany(c =>
                SetN<int>(b).Where(d => d>2&&c>d)
            )
        );
        this.Execute引数パターン(a =>
            Inline(() =>
                Inline(() => a*a)
            )
        );
        this.Execute引数パターン(a=>
            Inline(() =>
                SetN<int>(a).Select(b => b*b)
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            Inline(() => ArrN<int>(b))
        );
        this.Execute引数パターン(a =>
            Inline(() =>
                Inline(() =>
                    SetN<int>(a).Where(d => d==5)
                )
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            Inline(()=>
                Inline(() => a>b)
            )
        );
        this.Execute引数パターン(a =>
            SetN<int>(a).SelectMany(b =>
                ExtensionSet.Inline(() =>
                    SetN<int>(b)
                )
            )
        );
        var p = Expression.Parameter(typeof(int));
        var Call0 = Expression.Call(
            typeof(global::LinqDB.Sets.Helpers).GetMethod(nameof(global::LinqDB.Sets.Helpers.Let)).MakeGenericMethod(typeof(int),typeof(decimal)),
            Expression.Constant(1),
            Expression.Lambda<Func<int,decimal>>(
                Expression.Constant(1m),
                p
            )
        );
        var Call1 = Expression.Call(
            typeof(global::LinqDB.Sets.Helpers).GetMethod(nameof(global::LinqDB.Sets.Helpers.Let)).MakeGenericMethod(typeof(int),typeof(decimal)),
            Expression.Constant(1),
            Expression.Lambda<Func<int,decimal>>(
                Expression.Add(
                    Expression.Constant(1m),
                    Expression.Constant(1m)
                ),
                p
            )
        );
        this.Execute標準ラムダループ(
            Expression.Lambda<Func<decimal>>(
                Expression.Add(
                    Call1,
                    Call0
                )
            )
        );
        this.Execute引数パターン(a => ArrN<decimal>(a+1).Let(p => p.Average()));
        this.Execute引数パターン(a =>
            new {
                A = SetN<int>(a+a).Join(SetN<int>(a),o => o+1,i => i+1,(o,i) => o+i),
                B = SetN<int>(a*a).Join(SetN<int>(a),o => o+1,i => i+1,(o,i) => o*i)
            }
        );
        this.Execute引数パターン(a =>
            a.Let(c =>
                SetN<int>(a).Where(e => c==e).SelectMany(g =>
                    SetN<int>(a)
                )
            )
        );
        this.Execute引数パターン(a =>
            SetN<int>(a).SelectMany(b =>
                SetN<int>(a).Where(c => true).SelectMany(d =>
                    SetN<int>(a)
                )
            )
        );
        this.Execute引数パターン(a =>
            Inline(() =>
                Inline(() =>
                    SetN<int>(a)
                )
            )
        );
        this.Execute引数パターン(a =>
            a.Let(c =>
                SetN<int>(a).Where(e => c==e).SelectMany(g =>
                    SetN<int>(a+1).Where(i => i==5).Where(i => c==6)
                )
            )
        );
        this.Execute引数パターン(a =>
            a.Let(b =>
                SetN<int>(a).Where(d => d==5).Where(e => true)
            )
        );
        this.Execute引数パターン(a =>
            SetN<int>(a).SelectMany(c =>
                SetN<int>(a).Where(e =>c == e).SelectMany(g =>
                    SetN<int>(a).Where(h => h == 4).Where(i => i == 5).Where(i => c == 6)
                )
            )
        );
        this.Execute引数パターン(a =>
            SetN<int>(a).Where(b => b == 0).Where(b => b == 1).SelectMany(c =>
                SetN<int>(a).Where(e => e == 2 && c == e).Where(f => f == 3 && f == c).SelectMany(g =>
                    SetN<int>(a).Where(h => h == 4).Where(i => i == 5).Where(i => c == 6)
                )
            )
        );
        this.Execute引数パターン(a =>
            SetN<int>(a).Where(b => b==0).SelectMany(c =>
                SetN<int>(a).Where(h => h==4)
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            a.Let(c =>
                b.Let(e => e>2&&c>e)
            )
        );
        this.Execute引数パターン(a =>
            Inline(() =>
                a*a
            )
        );
        this.Execute引数パターン(a =>
            "".Let(b =>
                a*a
            )
        );
        this.Execute引数パターン(a =>
            Inline(() =>
                a
            )
        );
        this.Execute引数パターン(a =>
            "".Let(b =>
                a
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            SetN<int>(a).SelectMany(c =>
                SetN<int>(b).Where(e => e>2&&c>e).Where(f => f>3&&f>c).Where(g => g>c&&c>4)
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b,c) =>
            SetN<int>(a).SelectMany(d =>
                SetN<int>(b).Where(e => e>2&&d>e).Where(e => e>3&&e>d).SelectMany(e =>
                    SetN<int>(d).Where(f => f>4).Where(f => f>5)
                )
            )
        );
        this.Execute引数パターン(a =>
            SetN<int>(a).SelectMany(b =>
                SetN<int>(a).SelectMany(c =>
                    SetN<int>(b)
                )
            )
        );
        this.Execute引数パターン(a =>
            SetN<int>(a).SelectMany(c =>
                SetN<int>(a).Where(e => c == e).SelectMany(g =>
                    SetN<int>(a)
                )
            )
        );
        this.Execute引数パターン(a =>
            SetN<int>(a).SelectMany(c =>
                SetN<int>(a).Where(e => c == e).SelectMany(g =>
                    SetN<int>(a).Where(h => h == 4)
                )
            )
        );
        this.Execute引数パターン(a =>
            SetN<int>(a).SelectMany(c =>
                SetN<int>(a).Where(e => e == 2 && c == e).SelectMany(g =>
                    SetN<int>(a).Where(h => h == 4)
                )
            )
        );
        this.Execute引数パターン(a =>
            SetN<int>(a).Where(b => b == 0).SelectMany(c =>
                SetN<int>(a).Where(e => e == 2 && c == e).SelectMany(g =>
                    SetN<int>(a).Where(h => h == 4)
                )
            )
        );
        this.Execute引数パターン標準ラムダループ((a, b, c) =>
            SetN<int>(a).Where(d => d > 0).Where(d => d > 1).SelectMany(d =>
                SetN<int>(b).Where(e => e > 2 && d > e).Where(e => e > 3 && e > d).SelectMany(e =>
                    SetN<int>(c).Where(f => f > 4).Where(f => f > 5)
                )
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            SetN<int>(a).SelectMany(c =>
                SetN<int>(b).Where(e => e>2&&c*c>e)
            )
        );
        //()=>
        //    SetN<Int32>(a).Where(_Field=>
        //        ((_Field==0)AndAlso(_Field==1))
        //    ).SelectMany(c=>
        //        SetN<Int32>(a).Where(e=>
        //            ((e==2)AndAlso(e==3))
        //        ).KeyDictionary64(e=>
        //            newPair`2(e,e)
        //        ).Equal(
        //            newPair`2(c,c)
        //        ).SelectMany(g=>
        //            SetN<Int32>(a).Where(h=>
        //                ((h==4)AndAlso(h==5))
        //            ).Filter(
        //                (c==6)
        //            )
        //        )
        //    )
        //Filterに入れずに
        //    SetN<Int32>(a).Where(_Field=>
        //        _Field==6 AndAlso
        //        ((_Field==0)AndAlso(_Field==1))
        //    ).SelectMany(c=>
        //        SetN<Int32>(a).Where(e=>
        //            ((e==2)AndAlso(e==3))
        //        ).KeyDictionary64(e=>
        //            newPair`2(e,e)
        //        ).Equal(
        //            newPair`2(c,c)
        //        ).SelectMany(g=>
        //            SetN<Int32>(a).Where(h=>
        //                ((h==4)AndAlso(h==5))
        //            ).Filter(
        //                (c==6)
        //            )
        //        )
        //    )
        //にしたい。これはその後の最適化でWhereKeyに変形できるため。
    }
}