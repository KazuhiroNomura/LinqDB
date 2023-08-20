using System.Drawing;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using LinqDB.Databases;
using LinqDB.Optimizers;
using LinqDB.Sets;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoverageCS.LinqDB.Sets;
using Binder = Microsoft.CSharp.RuntimeBinder.Binder;
// ReSharper disable ConvertNullableToShortForm
// ReSharper disable ExpressionIsAlwaysNull
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable RedundantTypeArgumentsOfMethod
// ReSharper disable RedundantOverflowCheckingContext
// ReSharper disable RedundantCast
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class Test_変換_単一メソッド : ATest
{
    private static Set<int> Get(int i)
    {
        return new Set<int> { 1, 2 };
    }
    private static bool static_field = false;
    private static bool predicate(int o)
    {
        return o == 3;
    }
    private static PrimaryKeys.Entity KeySelector(Tables.Entity oi)
    {
        return oi.PrimaryKey;
    }
    private static PrimaryKeys.Entity Property => new(0, 0);
    private static int resultSelector(int o,Tables.Entity i)
    {
        return o + (int)i.PrimaryKey.ID1;
    }
    private static int resultSelector(Tables.Entity o, int i)
    {
        return (int)o.PrimaryKey.ID1 + i;
    }
    private static int resultSelector(int o, int i)
    {
        return o;
    }
    private static int selector(int oi)
    {
        return oi;
    }
    private struct IEquatableでなくop_Equality
    {
        public static bool operator ==(IEquatableでなくop_Equality a, IEquatableでなくop_Equality b)
        {
            return true;
        }
        public static bool operator !=(IEquatableでなくop_Equality a, IEquatableでなくop_Equality b)
        {
            return false;
        }
        public override bool Equals(object obj)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return 0;
        }
    }
    private struct IEquatableでなくop_Equalityでない
    {
    }
    [TestMethod]public void Test0(){
        this.Execute引数パターン(a => SetN<int>(a).SelectMany(b => SetN<int>(a).Where(c => c==1&&b==0)));
        this.Execute引数パターン(a => SetN<int>(a).Update(p => true, p => p));
        //SelectMany内部のメソッドチェーンをAny()が出来る最低限に変形
        this.Execute引数パターン(a => SetN<int>(a).SelectMany(Get).Any());
        this.Execute引数パターン(a => SetN<int>(a).Any());
        this.Execute引数パターン(a => SetN<int>(a).Select(p => p).LongCount());
        this.Execute引数パターン(a => SetN<int>(a).SelectMany(p => SetN<int>(a)).Any());
        this.Execute引数パターン(a => SetN<int>(a).Union(SetN<int>(a)).Any());
        this.Execute引数パターン(a => SetN<int>(a).Select(p => SetN<int>(a).SelectMany(q => SetN<int>(a)).Any()));
        this.Execute引数パターン(a => SetN<int>(a).SelectMany(p => SetN<int>(a).Select(r => r)).Any());
        this.Execute引数パターン(a => SetN<int>(a).SelectMany(p => SetN<int>(a).SelectMany(r => SetN<int>(a).SelectMany(q => SetN<int>(a)))).Any());
        //SelectMany
        this.Execute引数パターン(a => SetN<int>(a).SelectMany(p => SetN<int>(a).Where(r => r == 3)));
        this.Execute引数パターン(a => SetN<int>(a).SelectMany(p => SetN<int>(a).Where(r => p == 4)));
        this.Execute引数パターン(a => SetN<int>(a).SelectMany(p => SetN<int>(a).Where(r => r == 3 && p == 4)));
        this.Execute引数パターン(a => SetN<int>(a).SelectMany(p => SetN<int>(a).Where(r => 4 == p && 3 == r)));
        this.Execute引数パターン(a => SetN<int>(a).SelectMany(p => SetN<int>(a).Where(r => 4 == p && 3 == p)));
        this.Execute引数パターン(a => SetN<int>(a).SelectMany(p => SetN<int>(a).Where(r => static_field)));
    }
    [TestMethod]public void Test1(){
        var IEquatableでなくop_Equality変数 = new Set<IEquatableでなくop_Equality>();
        var IEquatableでなくop_Equalityでない変数 = new Set<IEquatableでなくop_Equalityでない>();
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
        //todo ImmutableSet<>のGetEnumerator()でthisがnullになっている
        this.Execute引数パターン(a => SetN<int>(a).SelectMany(p => SetN<int>(a).Where(predicate)));
        this.Execute引数パターン(a => SetN<int>(a).SelectMany(p => SetN<int>(a).Select(r => r)));
        //MethodCallExpression
        this.Execute引数パターン(a => SetN<int>(a).Contains(1));
        this.Execute2(() => IEquatableでなくop_Equality変数.Contains(new IEquatableでなくop_Equality()));
        this.Execute2(() => IEquatableでなくop_Equalityでない変数.Contains(new IEquatableでなくop_Equalityでない()));
        this.Execute引数パターン(a => SetN<int>(a).Delete(p => p == 1));
        this.Execute引数パターン(a => SetN<int>(a).GroupBy(p => p));
        this.Execute引数パターン(a => SetN<int>(a).SelectMany(p => SetN<int>(a), (p, collection) => new { p, collection }));
        this.Execute引数パターン(a => SetN<int>(a).SelectMany(p => SetN<int>(a), resultSelector));
        //Join11.Select
        this.Execute引数パターン(a => SetN<int>(a).Join(SetN<int>(a), o => o, i => i, (o, i) => o + i).Select(p => p + 1));
        this.Execute引数パターン(a => SetN<int>(a).Join(SetN<int>(a), o => o, i => i, (o, i) => o + i).Select(selector));
        this.Execute引数パターン(a => SetN<int>(a).Join(SetN<int>(a), o => o, i => i, resultSelector).Select(selector));
        //Select.Select
        this.Execute引数パターン(a => SetN<int>(a).Select(p => p + 1).Select(q => q + 1));
        this.Execute引数パターン(a => SetN<int>(a).Select(p => p + 1).Select(selector));
        this.Execute引数パターン(a => SetN<int>(a).Select(selector).Select(q => q + 1));
        this.Execute引数パターン(a => SetN<int>(a).Select(p => new { a = p, b = p }).Select(q => new { q.a, q.b, c = q.a, d = q.b }));
        //Join11.Where
        this.Execute引数パターン(a => SetN<int>(a).Join(SetN<int>(a), o => o, i => i, (o, i) => new { o, i }).Where(p => p.o == 3));
        this.Execute引数パターン(a => SetN<int>(a).Join(SetN<int>(a), o => o, i => i, (o, i) => new { o, i }).Where(p => p.i == 3));
        this.Execute引数パターン(a => SetN<int>(a).Join(SetN<int>(a), o => o, i => i, (o, i) => new { o, i }).Where(p => p.i + p.o == 3));
        this.Execute引数パターン(a => SetN<int>(a).Join(SetN<int>(a), o => o, i => i, resultSelector).Where(p => p == 3));
        //Select.Where
        this.Execute引数パターン(a => SetN<int>(a).Select(p => p + 1).Where(p => p == 3));
        this.Execute引数パターン(a => SetN<int>(a).Select(selector).Where(p => p == 3));
        //Where.Where
        this.Execute引数パターン(a => SetN<int>(a).Where(p => p == 3).Where(q => q == 3));
        this.Execute引数パターン(a => SetN<int>(a).Where(p => p == 3).Where(predicate));
        this.Execute引数パターン(a => SetN<int>(a).Where(predicate).Where(q => q == 3));
        this.Execute引数パターン(a => SetN<int>(a).Where(predicate).Where(predicate));
        //SelectWhere再帰で匿名型を走査
        this.Execute引数パターン(a => SetN<int>(a).Select(p => new string((char)p, 1)).Where(p => p == ""));
        this.Execute引数パターン(a => SetN<int>(a).Select(p => new { p }).Where(p => p.p == 3));
        //JoinWhere再帰で匿名型を走査
        this.Execute引数パターン(a => SetN<int>(a).Join(SetN<int>(a), o => o, i => i, (o, i) => new string((char)o, 1)).Where(p => p == ""));
        //ArrayIndex
        this.Execute引数パターン(a => (new int[3])[0]);
        //GreaterThan
        var v = 3;
        this.Execute引数パターン(a => v > 3);
        //GreaterThanOrEqual
        this.Execute引数パターン(a => v >= 3);
    }
    private void 共通BinaryAssign1パターン<T>(T a, T b, ExpressionType NodeType)
    {
        var p = Expression.Parameter(typeof(T));
        this.Execute2(
            Expression.Lambda<Func<T>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Constant(a, typeof(T))
                    ),
                    Expression.MakeBinary(
                        NodeType,
                        p,
                        Expression.Constant(b, typeof(T))
                    )
                )
            )
        );
    }
    private void 共通BinaryAssignNパターン<T>(T a, T b, ExpressionType NodeType) where T : struct
    {
        this.共通BinaryAssign1パターン<T?>(default, default, NodeType);
        this.共通BinaryAssign1パターン<T?>(default, a, NodeType);
        this.共通BinaryAssign1パターン<T?>(default, b, NodeType);
        this.共通BinaryAssign1パターン<T?>(a, default, NodeType);
        this.共通BinaryAssign1パターン<T?>(a, a, NodeType);
        this.共通BinaryAssign1パターン<T?>(a, b, NodeType);
        this.共通BinaryAssign1パターン<T?>(b, default, NodeType);
        this.共通BinaryAssign1パターン<T?>(b, a, NodeType);
        this.共通BinaryAssign1パターン<T?>(b, b, NodeType);
        this.共通BinaryAssign1パターン(a, a, NodeType);
        this.共通BinaryAssign1パターン(a, b, NodeType);
        this.共通BinaryAssign1パターン(b, a, NodeType);
        this.共通BinaryAssign1パターン(b, b, NodeType);
    }
    private void 共通四則演算Assign(ExpressionType NodeType)
    {
        this.共通BinaryAssignNパターン(4, 3, NodeType);
        this.共通BinaryAssignNパターン(4U, 3U, NodeType);
        this.共通BinaryAssignNパターン(4L, 3L, NodeType);
        this.共通BinaryAssignNパターン(4UL, 3UL, NodeType);
        this.共通BinaryAssignNパターン(4F, 3F, NodeType);
        this.共通BinaryAssignNパターン(4D, 3D, NodeType);
        this.共通BinaryAssignNパターン(4M, 3M, NodeType);
    }
    private void 共通BinaryNパターン<T>(T a, T b, ExpressionType NodeType) where T : struct
    {
        this.共通Binary1パターン<T?>(default, default, NodeType);
        this.共通Binary1パターン<T?>(default, a, NodeType);
        this.共通Binary1パターン<T?>(default, b, NodeType);
        this.共通Binary1パターン<T?>(a, default, NodeType);
        this.共通Binary1パターン<T?>(a, a, NodeType);
        this.共通Binary1パターン<T?>(a, b, NodeType);
        this.共通Binary1パターン<T?>(b, default, NodeType);
        this.共通Binary1パターン<T?>(b, a, NodeType);
        this.共通Binary1パターン<T?>(b, b, NodeType);
        this.共通Binary1パターン(a, a, NodeType);
        this.共通Binary1パターン(a, b, NodeType);
        this.共通Binary1パターン(b, a, NodeType);
        this.共通Binary1パターン(b, b, NodeType);
    }
    private void 共通四則演算(ExpressionType NodeType)
    {
        this.共通BinaryNパターン(3, 2, NodeType);
        this.共通BinaryNパターン(3U, 2U, NodeType);
        this.共通BinaryNパターン(3L, 2L, NodeType);
        this.共通BinaryNパターン(3UL, 2UL, NodeType);
        this.共通BinaryNパターン(3F, 2F, NodeType);
        this.共通BinaryNパターン(3D, 2D, NodeType);
        this.共通BinaryNパターン(3M, 2M, NodeType);
    }
    private void 共通BinaryAssignCheckedNパターン<T>(T a, T b, ExpressionType NodeType) where T : struct
    {
        this.共通BinaryAssign1パターン<T?>(default, default, NodeType);
        this.共通BinaryAssign1パターン<T?>(default, a, NodeType);
        this.共通BinaryAssign1パターン<T?>(default, b, NodeType);
        this.共通BinaryAssign1パターン<T?>(a, default, NodeType);
        this.共通BinaryAssign1パターン<T?>(a, a, NodeType);
        this.共通BinaryAssign1パターン<T?>(a, b, NodeType);
        this.共通BinaryAssign1パターン<T?>(b, default, NodeType);
        this.共通BinaryAssign1パターン<T?>(b, b, NodeType);
        this.共通BinaryAssign1パターン(a, a, NodeType);
        this.共通BinaryAssign1パターン(a, b, NodeType);
        this.共通BinaryAssign1パターン(b, b, NodeType);
    }
    private void 共通四則演算AssignChecked(ExpressionType NodeType)
    {
        this.共通BinaryAssignCheckedNパターン(4, 3, NodeType);
        this.共通BinaryAssignCheckedNパターン(4U, 3U, NodeType);
        this.共通BinaryAssignCheckedNパターン(4L, 3L, NodeType);
        this.共通BinaryAssignCheckedNパターン(4UL, 3UL, NodeType);
        this.共通BinaryAssignCheckedNパターン(4F, 3F, NodeType);
        this.共通BinaryAssignCheckedNパターン(4D, 3D, NodeType);
        this.共通BinaryAssignCheckedNパターン(4M, 3M, NodeType);
    }
    private void 共通BinaryCheckedNパターン<T>(T a, T b, ExpressionType NodeType) where T : struct
    {
        this.共通Binary1パターン<T?>(default, default, NodeType);
        this.共通Binary1パターン<T?>(default, a, NodeType);
        this.共通Binary1パターン<T?>(default, b, NodeType);
        this.共通Binary1パターン<T?>(a, default, NodeType);
        this.共通Binary1パターン<T?>(a, a, NodeType);
        this.共通Binary1パターン<T?>(a, b, NodeType);
        this.共通Binary1パターン<T?>(b, default, NodeType);
        this.共通Binary1パターン<T?>(b, b, NodeType);
        this.共通Binary1パターン(a, a, NodeType);
        this.共通Binary1パターン(a, b, NodeType);
    }
    private void 共通四則演算Checked(ExpressionType NodeType)
    {
        this.共通BinaryCheckedNパターン(3, 2, NodeType);
        this.共通BinaryCheckedNパターン(3U, 2U, NodeType);
        this.共通BinaryCheckedNパターン(3L, 2L, NodeType);
        this.共通BinaryCheckedNパターン(3UL, 2UL, NodeType);
        this.共通BinaryCheckedNパターン(3F, 2F, NodeType);
        this.共通BinaryCheckedNパターン(3D, 2D, NodeType);
        this.共通BinaryCheckedNパターン(3M, 2M, NodeType);
    }
    private void 共通Binary<T, TResult>(T a, T b, ExpressionType NodeType){
        var ap=Expression.Parameter(typeof(T));
        var bp=Expression.Parameter(typeof(T));
        this.Execute2(
            Expression.Lambda<Func<T,T,TResult>>(
                Expression.MakeBinary(
                    NodeType,
                    ap,
                    bp
                ),
                ap,bp
            ),a,b
        );
        this.Execute2(
            Expression.Lambda<Func<TResult>>(
                Expression.MakeBinary(
                    NodeType,
                    Expression.Constant(a, typeof(T)),
                    Expression.Constant(b, typeof(T))
                )
            )
        );
    }
    private void 共通Binary1パターン<T>(T a, T b, ExpressionType NodeType)
    {
        this.共通Binary<T, T>(a, b, NodeType);
    }
    private void 共通AndOrXorNパターン(ExpressionType NodeType)
    {
        this.共通Binary1パターン(false, false, NodeType);
        this.共通Binary1パターン(false, true, NodeType);
        this.共通Binary1パターン(true, false, NodeType);
        this.共通Binary1パターン(true, true, NodeType);
        this.共通Binary1パターン<bool?>(null, null, NodeType);
        this.共通Binary1パターン<bool?>(null, false, NodeType);
        this.共通Binary1パターン<bool?>(null, true, NodeType);
        this.共通Binary1パターン<bool?>(false, null, NodeType);
        this.共通Binary1パターン<bool?>(false, false, NodeType);
        this.共通Binary1パターン<bool?>(false, true, NodeType);
        this.共通Binary1パターン<bool?>(true, null, NodeType);
        this.共通Binary1パターン<bool?>(true, false, NodeType);
        this.共通Binary1パターン<bool?>(true, true, NodeType);
        this.共通Binary1パターン(2, 2, NodeType);
        this.共通Binary1パターン(2, 3, NodeType);
        this.共通Binary1パターン(3, 2, NodeType);
        this.共通Binary1パターン(3, 3, NodeType);
        this.共通Binary1パターン<int?>(null, null, NodeType);
        this.共通Binary1パターン<int?>(null, 2, NodeType);
        this.共通Binary1パターン<int?>(null, 3, NodeType);
        this.共通Binary1パターン<int?>(2, null, NodeType);
        this.共通Binary1パターン<int?>(2, 2, NodeType);
        this.共通Binary1パターン<int?>(2, 3, NodeType);
        this.共通Binary1パターン<int?>(3, null, NodeType);
        this.共通Binary1パターン<int?>(3, 2, NodeType);
        this.共通Binary1パターン<int?>(3, 3, NodeType);
        this.共通Binary1パターン<struct_ショートカット検証>(false, false, NodeType);
        this.共通Binary1パターン<struct_ショートカット検証>(false, true, NodeType);
        this.共通Binary1パターン<struct_ショートカット検証>(true, false, NodeType);
        this.共通Binary1パターン<struct_ショートカット検証>(true, true, NodeType);
        this.共通Binary1パターン<struct_ショートカット検証?>(null, null, NodeType);
        this.共通Binary1パターン<struct_ショートカット検証?>(null, false, NodeType);
        this.共通Binary1パターン<struct_ショートカット検証?>(null, true, NodeType);
        this.共通Binary1パターン<struct_ショートカット検証?>(false, null, NodeType);
        this.共通Binary1パターン<struct_ショートカット検証?>(false, false, NodeType);
        this.共通Binary1パターン<struct_ショートカット検証?>(false, true, NodeType);
        this.共通Binary1パターン<struct_ショートカット検証?>(true, null, NodeType);
        this.共通Binary1パターン<struct_ショートカット検証?>(true, false, NodeType);
        this.共通Binary1パターン<struct_ショートカット検証?>(true, true, NodeType);
    }
    private void 共通AndOrXorAssignNパターン(ExpressionType NodeType)
    {
        this.共通BinaryAssign1パターン(false, false, NodeType);
        this.共通BinaryAssign1パターン(false, true, NodeType);
        this.共通BinaryAssign1パターン(true, false, NodeType);
        this.共通BinaryAssign1パターン(true, true, NodeType);
        this.共通BinaryAssign1パターン<bool?>(null, null, NodeType);
        this.共通BinaryAssign1パターン<bool?>(null, false, NodeType);
        this.共通BinaryAssign1パターン<bool?>(null, true, NodeType);
        this.共通BinaryAssign1パターン<bool?>(false, null, NodeType);
        this.共通BinaryAssign1パターン<bool?>(false, false, NodeType);
        this.共通BinaryAssign1パターン<bool?>(false, true, NodeType);
        this.共通BinaryAssign1パターン<bool?>(true, null, NodeType);
        this.共通BinaryAssign1パターン<bool?>(true, false, NodeType);
        this.共通BinaryAssign1パターン<bool?>(true, true, NodeType);
        this.共通BinaryAssign1パターン(2, 2, NodeType);
        this.共通BinaryAssign1パターン(2, 3, NodeType);
        this.共通BinaryAssign1パターン(3, 2, NodeType);
        this.共通BinaryAssign1パターン(3, 3, NodeType);
        this.共通BinaryAssign1パターン<int?>(null, null, NodeType);
        this.共通BinaryAssign1パターン<int?>(null, 2, NodeType);
        this.共通BinaryAssign1パターン<int?>(null, 3, NodeType);
        this.共通BinaryAssign1パターン<int?>(2, null, NodeType);
        this.共通BinaryAssign1パターン<int?>(2, 2, NodeType);
        this.共通BinaryAssign1パターン<int?>(2, 3, NodeType);
        this.共通BinaryAssign1パターン<int?>(3, null, NodeType);
        this.共通BinaryAssign1パターン<int?>(3, 2, NodeType);
        this.共通BinaryAssign1パターン<int?>(3, 3, NodeType);
        this.共通BinaryAssign1パターン<struct_ショートカット検証>(false, false, NodeType);
        this.共通BinaryAssign1パターン<struct_ショートカット検証>(false, true, NodeType);
        this.共通BinaryAssign1パターン<struct_ショートカット検証>(true, false, NodeType);
        this.共通BinaryAssign1パターン<struct_ショートカット検証>(true, true, NodeType);
        this.共通BinaryAssign1パターン<struct_ショートカット検証?>(null, null, NodeType);
        this.共通BinaryAssign1パターン<struct_ショートカット検証?>(null, false, NodeType);
        this.共通BinaryAssign1パターン<struct_ショートカット検証?>(null, true, NodeType);
        this.共通BinaryAssign1パターン<struct_ショートカット検証?>(false, null, NodeType);
        this.共通BinaryAssign1パターン<struct_ショートカット検証?>(false, false, NodeType);
        this.共通BinaryAssign1パターン<struct_ショートカット検証?>(false, true, NodeType);
        this.共通BinaryAssign1パターン<struct_ショートカット検証?>(true, null, NodeType);
        this.共通BinaryAssign1パターン<struct_ショートカット検証?>(true, false, NodeType);
        this.共通BinaryAssign1パターン<struct_ショートカット検証?>(true, true, NodeType);
    }

    private void 共通AndOr短絡評価<T>(T a, T b, ExpressionType NodeType)
    {
        var Lambda = Expression.Lambda<Func<T>>(
            Expression.MakeBinary(
                NodeType,
                Expression.Constant(a, typeof(T)),
                Expression.Constant(b, typeof(T))
            )
        );
        var expected = Lambda.Compile()();
        var actual = this.Execute2(Lambda);
        Assert.AreEqual(expected, actual);
    }
    private void 共通AndOr短絡評価(ExpressionType NodeType)
    {
        {
            bool f = false, t = true;
            this.共通AndOr短絡評価(f, f, NodeType);
            this.共通AndOr短絡評価(f, t, NodeType);
            this.共通AndOr短絡評価(t, f, NodeType);
            this.共通AndOr短絡評価(t, t, NodeType);
        }
        {
            struct_ショートカット検証 f = false;
            struct_ショートカット検証 t = true;
            this.共通AndOr短絡評価(f, f, NodeType);
            this.共通AndOr短絡評価(f, t, NodeType);
            this.共通AndOr短絡評価(t, f, NodeType);
            this.共通AndOr短絡評価(t, t, NodeType);
        }
        {
            bool? f = false, t = true, n = null;
            this.共通AndOr短絡評価(f, f, NodeType);
            this.共通AndOr短絡評価(f, t, NodeType);
            this.共通AndOr短絡評価(t, f, NodeType);
            this.共通AndOr短絡評価(t, t, NodeType);
            this.共通AndOr短絡評価(f, n, NodeType);
            this.共通AndOr短絡評価(t, n, NodeType);
            this.共通AndOr短絡評価(n, f, NodeType);
            this.共通AndOr短絡評価(n, t, NodeType);
            this.共通AndOr短絡評価(n, n, NodeType);
        }
        {
            struct_ショートカット検証? n = null;
            struct_ショートカット検証? f = new struct_ショートカット検証(1, false);
            struct_ショートカット検証? t = new struct_ショートカット検証(3, true);
            this.共通AndOr短絡評価(n, n, NodeType);
            this.共通AndOr短絡評価(n, f, NodeType);
            this.共通AndOr短絡評価(n, t, NodeType);
            this.共通AndOr短絡評価(f, n, NodeType);
            this.共通AndOr短絡評価(f, f, NodeType);
            this.共通AndOr短絡評価(f, t, NodeType);
            this.共通AndOr短絡評価(t, n, NodeType);
            this.共通AndOr短絡評価(t, f, NodeType);
            this.共通AndOr短絡評価(t, t, NodeType);
        }
    }
    private void 共通大小等価不等価<T>(T a, T b, ExpressionType NodeType) where T : struct
    {
        this.共通Binary<T?, bool>(a, a, NodeType);
        this.共通Binary<T?, bool>(a, b, NodeType);
        this.共通Binary<T?, bool>(b, a, NodeType);
        this.共通Binary<T?, bool>(b, b, NodeType);
        this.共通Binary<T?, bool>(null, null, NodeType);
        this.共通Binary<T?, bool>(null, a, NodeType);
        this.共通Binary<T?, bool>(null, b, NodeType);
        this.共通Binary<T?, bool>(a, null, NodeType);
        this.共通Binary<T?, bool>(b, null, NodeType);
        this.共通Binary<T, bool>(a, a, NodeType);
        this.共通Binary<T, bool>(a, b, NodeType);
        this.共通Binary<T, bool>(b, a, NodeType);
        this.共通Binary<T, bool>(b, b, NodeType);
    }
    private void 共通大小等価不等価(ExpressionType NodeType)
    {
        this.共通大小等価不等価(1, 1, NodeType);
        this.共通大小等価不等価(1, 2, NodeType);
        this.共通大小等価不等価(1U, 2U, NodeType);
        this.共通大小等価不等価(1L, 2L, NodeType);
        this.共通大小等価不等価(1UL, 2UL, NodeType);
        this.共通大小等価不等価(1F, 2F, NodeType);
        this.共通大小等価不等価(1D, 2D, NodeType);
        this.共通大小等価不等価(1M, 2M, NodeType);
    }
    private void 共通UnaryNullable対応2<T>(T a, ExpressionType NodeType)
    {
        this.Execute2(
            Expression.Lambda<Func<T>>(
                Expression.MakeUnary(
                    NodeType,
                    Expression.Constant(a, typeof(T)),
                    typeof(T)
                )
            )
        );
    }
    private void 共通UnaryNullable対応<T>(T a, ExpressionType NodeType) where T : struct
    {
        this.共通UnaryNullable対応2(a, NodeType);
        this.共通UnaryNullable対応2<T?>(a, NodeType);
    }
    private void 共通Unary(ExpressionType NodeType)
    {
        this.共通UnaryNullable対応(1, NodeType);
        this.共通UnaryNullable対応(1U, NodeType);
        this.共通UnaryNullable対応(1L, NodeType);
        this.共通UnaryNullable対応(1UL, NodeType);
        this.共通UnaryNullable対応(1F, NodeType);
        this.共通UnaryNullable対応(1D, NodeType);
        this.共通UnaryNullable対応(1M, NodeType);
    }
    private void 共通PostPreIncrementDecrementAssign<T>(ExpressionType NodeType)
    {
        var p = Expression.Parameter(typeof(T));
        this.Execute2(
            Expression.Lambda<Func<T>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(p, Expression.Constant(0, typeof(T))),
                    Expression.MakeUnary(
                        NodeType,
                        p,
                        typeof(T)
                    )
                )
            )
        );
    }
    private void 共通PostPreIncrementDecrementAssign(ExpressionType NodeType)
    {
        this.共通PostPreIncrementDecrementAssign<int?>(NodeType);
        this.共通PostPreIncrementDecrementAssign<int>(NodeType);
    }
    [TestMethod]
    public void Add() => this.共通四則演算(ExpressionType.Add);
    [TestMethod]
    public void AddAssign() => this.共通四則演算Assign(ExpressionType.AddAssign);
    [TestMethod]
    public void AddAssignChecked() => this.共通四則演算AssignChecked(ExpressionType.AddAssignChecked);
    [TestMethod]
    public void AddChecked() => this.共通四則演算Checked(ExpressionType.AddChecked);
    [TestMethod]
    public void And() => this.共通AndOrXorNパターン(ExpressionType.And);
    [TestMethod]
    public void AndAlso() => this.共通AndOr短絡評価(ExpressionType.AndAlso);
    [TestMethod]
    public void AndAssign() => this.共通AndOrXorAssignNパターン(ExpressionType.AndAssign);
    [TestMethod]
    public void Block()
    {
        //for(var a=0;a<Block0_Expressions_Count;a++){
        //    if(Block1_Expression.NodeType==ExpressionType.Assign){
        //        if(Dictionary代入先Expression.ContainsKey(Block1_Assign.Left)){
        var p = Expression.Parameter(typeof(int), "p");
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(p, Expression.Constant(1)),
                    Expression.Assign(p, Expression.Constant(2))
                )
            )
        );
        //        }
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(p, Expression.Constant(1))
                )
            )
        );
    }
    [TestMethod]
    public void MakeAssign()
    {
        //if(Binary0_Left==Binary1_Left&&Binary0_Right==Binary1_Right&&Binary0_Conversion==Binary1_Conversion)
        this._InstanceInt32 = 0;
        const int expected = 2;
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Assign(
                    Expression.Field(
                        Expression.Constant(
                            this
                        ),
                        // ReSharper disable once AssignNullToNotNullAttribute
                        this.GetType().GetField(nameof(this._InstanceInt32), BindingFlags.NonPublic | BindingFlags.Instance)
                    ),
                    Expression.Constant(expected)
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { i0, i1 },
                    Expression.Assign(i1, Expression.Constant(4)),
                    Expression.Assign(i0, i1)
                )
            )
        );
    }
    private delegate int FuncRef(ref int input);
    private static int Lambda(ref int input, FuncRef d)
    {
        return d(ref input);
    }
    [TestMethod]
    public void DictionaryParameterParameter_Add()
    {
        var p = Expression.Parameter(typeof(int), "p");
        var p1 = Expression.Parameter(typeof(int).MakeByRefType(), "p1");
        var p2 = Expression.Parameter(typeof(int).MakeByRefType(), "p2");
        //()=>{
        //    Int32 p
        //    return Lambda(
        //        p,
        //        (ref Int32 p1)=>{
        //            Lambda(
        //                p1,
        //                (ref Int32 p2)=>{
        //                    return p2=4;
        //                }
        //            }
        //        )
        //    );
        //}
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { p },
                    Expression.Call(
                        typeof(Test_変換_単一メソッド).GetMethod(nameof(Lambda), BindingFlags.Static | BindingFlags.NonPublic),
                        p,
                        Expression.Lambda<FuncRef>(
                            Expression.Call(
                                typeof(Test_変換_単一メソッド).GetMethod(nameof(Lambda), BindingFlags.Static | BindingFlags.NonPublic),
                                p1,
                                Expression.Lambda<FuncRef>(
                                    Expression.Assign(
                                        p2,
                                        Expression.Constant(4)
                                    ),
                                    p2
                                )
                            ),
                            p1
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void DictionaryParameterParameter_Remove()
    {
        this.DictionaryParameterParameter_Add();
    }
    //private object DynamicBinary(object a, object b, ExpressionType NodeType){
    //    var binder=Binder.BinaryOperation(
    //        CSharpBinderFlags.None,
    //        NodeType,
    //        typeof(Test_変換_単一メソッド),
    //        new[]{
    //            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null),
    //            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null)
    //        }
    //    );
    //    var CallSite=CallSite<Func<CallSite,object,object,object>>.Create(binder);
    //    var expected=CallSite.Target(CallSite,a,b);
    //    var left = Expression.Constant(a);
    //    var right = Expression.Constant(b);
    //    //left = Expression.Constant(a);
    //    //right = Expression.Constant(b);
    //    var Dynamic0 = Expression.Dynamic(
    //        binder,
    //        typeof(object),
    //        left,
    //        right
    //    );
    //    var actual=this.Execute(Expression.Lambda<Func<object>>(Dynamic0));
    //    Assert.AreEqual(expected,actual);
    //    return expected;
    //}
    private object PrivateDynamicBinary<Ta,Tb>(Ta a, Tb b, ExpressionType NodeType){
        var binder=Binder.BinaryOperation(
            CSharpBinderFlags.None,
            NodeType,
            typeof(Test_変換_単一メソッド),
            new[]{
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null)
            }
        );
        var CallSite=CallSite<Func<CallSite,Ta,Tb,object>>.Create(binder);
        var expected=CallSite.Target(CallSite,a,b);
        var left = Expression.Constant(a,typeof(Ta));
        var right = Expression.Constant(b,typeof(Tb));
        var Dynamic0 = Expression.Dynamic(
            binder,
            typeof(object),
            left,
            right
        );
        var actual=this.Execute2(Expression.Lambda<Func<object>>(Dynamic0));
        Assert.AreEqual(expected,actual);
        return expected;
    }
    private void DynamicBinary<Ta,Tb>(Ta a,Tb b,ExpressionType NodeType){
        var actual0=this.PrivateDynamicBinary(a,b,NodeType);
        var actual1=this.PrivateDynamicBinary<object,object>(a,b,NodeType);
        Assert.AreEqual(actual0,actual1);
    }

    private object PrivateDynamicUnary<T>(T a, ExpressionType NodeType){
        var binder=Binder.UnaryOperation(
            CSharpBinderFlags.None,
            NodeType,
            typeof(Test_変換_単一メソッド),
            new[]{
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null)
            }
        );
        Expression operand = Expression.Constant(a,typeof(T));
        var CallSite=CallSite<Func<CallSite,object,object>>.Create(binder);
        var expected=CallSite.Target(CallSite,a);
        var Dynamic0 = Expression.Dynamic(
            binder,
            typeof(object),
            operand
        );
        var actual=this.Execute2(Expression.Lambda<Func<object>>(Dynamic0));
        Assert.AreEqual(expected,actual);
        return actual;
    }
    private void DynamicUnary<T>(T a,ExpressionType NodeType){
        var actual0=this.PrivateDynamicUnary(a,NodeType);
        var actual1=this.PrivateDynamicUnary<object>(a,NodeType);
        Assert.AreEqual(actual0,actual1);
    }
    [TestMethod]
    public void DynamicConvert(){
        this.DynamicConvert<int,double>(1,1.0);
    }
    private void DynamicConvert<TInput, TResult>(TInput input,TResult expected){
        var binder=Binder.Convert(
            CSharpBinderFlags.ConvertExplicit,
            typeof(TResult),
            typeof(Test_変換_単一メソッド)
        );
        var CallSite=CallSite<Func<CallSite,object,TResult>>.Create(binder);
        var actual0=CallSite.Target(CallSite,input);
        Assert.AreEqual(expected,actual0);
        var Dynamic0 = Expression.Dynamic(
            binder,
            typeof(TResult),
            Expression.Constant(input, typeof(object))
        );
        var actual1=this.Execute2(Expression.Lambda<Func<TResult>>(Dynamic0));
        Assert.AreEqual(expected,actual1);
    }
    [TestMethod]
    public void DynamicGetIndex(){
        var Array=new int[3];
        Array[1]=3;
        this.DynamicGetIndex(Array, 1);
    }
    private object PrivateDynamicGetIndex<TArray,TIndex>(TArray array,TIndex index){
        var GetIndex=Binder.GetIndex(
            CSharpBinderFlags.None,
            typeof(Test_変換_単一メソッド),
            new[]{
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null)
            }
        );
        var CallSite=CallSite<Func<CallSite,object,object,object>>.Create(GetIndex);
        var expected=CallSite.Target(CallSite,array,index);
        var Dynamic0 = Expression.Dynamic(
            GetIndex,
            typeof(object),
            Expression.Constant(array,typeof(TArray)),
            Expression.Constant(index,typeof(TIndex))
        );
        var actual=this.Execute2(Expression.Lambda<Func<object>>(Dynamic0));
        Assert.AreEqual(expected,actual);
        return expected;
    }
    private void DynamicGetIndex<TArray,TIndex>(TArray array,TIndex index){
        var actual0=this.PrivateDynamicGetIndex<TArray,TIndex>(array,index);
        var actual1=this.PrivateDynamicGetIndex<TArray,object>(array,index);
        var actual2=this.PrivateDynamicGetIndex<object,TIndex>(array,index);
        var actual3=this.PrivateDynamicGetIndex<object,object>(array,index);
        Assert.AreEqual(actual0,actual1);
        Assert.AreEqual(actual0,actual2);
        Assert.AreEqual(actual0,actual3);
    }
    [TestMethod]
    public void DynamicSetIndex(){
        var Array=new int[3];
        this.DynamicSetIndex(Array, 1,2);
    }
    private object PrivateDynamicSetIndex<TArray,TIndex,TValue>(TArray array,TIndex index, TValue value){
        var SetIndex=Binder.SetIndex(
            CSharpBinderFlags.None,
            typeof(Test_変換_単一メソッド),
            new[]{
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null)
            }
        );
        var CallSite=CallSite<Func<CallSite,object,object,object,object>>.Create(
            SetIndex
        );
        var expected=CallSite.Target(CallSite,array,index,value);
        var Dynamic0 = Expression.Dynamic(
            SetIndex,
            typeof(object),
            Expression.Constant(array,typeof(TArray)),
            Expression.Constant(index,typeof(TIndex)),
            Expression.Constant(value,typeof(TValue))
        );
        var actual=this.Execute2(Expression.Lambda<Func<object>>(Dynamic0));
        Assert.AreEqual(expected,actual);
        Assert.AreEqual(expected,value);
        return expected;
    }

    private void DynamicSetIndex<TArray,TIndex,TValue>(TArray array,TIndex index,TValue value){
        var actual0=this.PrivateDynamicSetIndex<TArray,TIndex,TValue>(array,index,value);
        var actual1=this.PrivateDynamicSetIndex<TArray,TIndex,object>(array,index,value);
        var actual2=this.PrivateDynamicSetIndex<TArray,object,TValue>(array,index,value);
        var actual3=this.PrivateDynamicSetIndex<TArray,object,object>(array,index,value);
        var actual4=this.PrivateDynamicSetIndex<object,TIndex,TValue>(array,index,value);
        var actual5=this.PrivateDynamicSetIndex<object,TIndex,object>(array,index,value);
        var actual6=this.PrivateDynamicSetIndex<object,object,TValue>(array,index,value);
        var actual7=this.PrivateDynamicSetIndex<object,object,object>(array,index,value);
        Assert.AreEqual(actual0,actual1);
        Assert.AreEqual(actual0,actual2);
        Assert.AreEqual(actual0,actual3);
        Assert.AreEqual(actual0,actual4);
        Assert.AreEqual(actual0,actual5);
        Assert.AreEqual(actual0,actual6);
        Assert.AreEqual(actual0,actual7);
    }

    [TestMethod]
    public void DynamicGetMember(){
        var anonymous=new{a=1D,b=2M,c="A"};
        Assert.AreEqual(anonymous.a, this.DynamicGetMember(anonymous, nameof(anonymous.a)));
        Assert.AreEqual(anonymous.b, this.DynamicGetMember(anonymous, nameof(anonymous.b)));
        Assert.AreEqual(anonymous.c, this.DynamicGetMember(anonymous, nameof(anonymous.c)));
    }
    private object DynamicGetMember(object オブジェクト, string メンバー名){
        var CallSite=CallSite<Func<CallSite,object,object>>.Create(
            Binder.GetMember(
                CSharpBinderFlags.None,メンバー名,
                typeof(Test_変換_単一メソッド),
                new []{CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null)}
            )
        );
        var expected=CallSite.Target(CallSite,オブジェクト);
        var Dynamic0 = Expression.Dynamic(
            Binder.GetMember(
                CSharpBinderFlags.None,
                メンバー名,
                typeof(Test_変換_単一メソッド),
                new[] {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                }
            ),
            typeof(object),
            Expression.Constant(オブジェクト)
        );
        var actual=this.Execute2(Expression.Lambda<Func<object>>(Dynamic0));
        Assert.AreEqual(expected,actual);
        return expected;
    }
    [TestMethod]
    public void DynamicSetMember(){
        this.DynamicSetMember(new class_演算子オーバーロード(), nameof(class_演算子オーバーロード.Stringフィールド),"初期","設定");
    }
    private void DynamicSetMember(class_演算子オーバーロード オブジェクト, string メンバー名, object 初期値, object 設定値){
        var Field=オブジェクト.GetType().GetField(メンバー名)!;
        Field.SetValue(オブジェクト,初期値);
        Assert.AreEqual(初期値,Field.GetValue(オブジェクト));
        var Infos=new[]{
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null)
        };
        var binder=Binder.SetMember(
            CSharpBinderFlags.None,
            メンバー名,
            typeof(Test_変換_単一メソッド),
            Infos
        );
        var CallSite=CallSite<Func<CallSite,object,object,object>>.Create(binder);
        Assert.AreEqual(設定値,CallSite.Target(CallSite,オブジェクト,設定値));
        Assert.AreEqual(設定値,Field.GetValue(オブジェクト));
        var Dynamic0 = Expression.Dynamic(
            binder,
            typeof(object),
            Expression.Constant(オブジェクト,typeof(object)),
            Expression.Constant(設定値)
        );
        var Lambda = Expression.Lambda<Action>(Dynamic0);
        //Field.SetValue(オブジェクト,初期値);
        this.Execute標準ラムダループ(
            Lambda,
            ()=>Field.SetValue(オブジェクト,初期値),
            ()=>Assert.AreEqual(設定値,Field.GetValue(オブジェクト))
        );
        //Assert.AreEqual(設定値,Field.GetValue(オブジェクト));
    }
    private object DynamicInvoke(object オブジェクト, object a,object b,object c){
        var binder=Binder.Invoke(
            CSharpBinderFlags.None,
            typeof(Test_変換_単一メソッド),
            new[]{
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null)
            }
        );
        var CallSite=CallSite<Func<CallSite,object,object,object,object,object>>.Create(
            binder
        );
        var expected=CallSite.Target(CallSite,オブジェクト,a,b,c);
        var Dynamic0 = Expression.Dynamic(
            binder,
            typeof(object),
            Expression.Constant(オブジェクト),
            Expression.Constant(a),
            Expression.Constant(b),
            Expression.Constant(c)
        );
        var actual=this.Execute2(Expression.Lambda<Func<object>>(Dynamic0));
        Assert.AreEqual(expected,actual);
        return expected;
    }
    private void DynamicInvokeMember_Action(object オブジェクト, string メンバー名,object @int,object @double,object @string){
        var binder=Binder.InvokeMember(
            CSharpBinderFlags.ResultDiscarded,//Actionの時に指定
            メンバー名,
            //Type.EmptyTypes,
            null,//new []{typeof(object),typeof(object),typeof(object)},
            //new []{引数0.GetType(),引数1.GetType(),引数2.GetType()},
            typeof(Test_変換_単一メソッド),
            new[]{
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null)
            }
        );
        var CallSite=CallSite<Action<CallSite,object,object,object,object>>.Create(binder);
        CallSite.Target(CallSite,オブジェクト,@int,@double,@string);
        var Dynamic0 = Expression.Dynamic(
            binder,
            typeof(object),
            Expression.Constant(オブジェクト),
            Expression.Constant(@int),
            Expression.Constant(@double),
            Expression.Constant(@string)
        );
        this.Execute標準ラムダループ(Expression.Lambda<Action>(Dynamic0));
    }
    private object DynamicInvokeMember_Func(object オブジェクト, string メンバー名,object @int,object @double,object @string){
        var binder=Binder.InvokeMember(
            CSharpBinderFlags.None,
            メンバー名,
            //Type.EmptyTypes,
            null,//new []{typeof(object),typeof(object),typeof(object)},
            //new []{引数0.GetType(),引数1.GetType(),引数2.GetType()},
            typeof(Test_変換_単一メソッド),
            new[]{
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null)
            }
        );
        var CallSite=CallSite<Func<CallSite,object,object,object,object,object>>.Create(
            binder
        );
        var expected=CallSite.Target(CallSite,オブジェクト,@int,@double,@string);
        var Dynamic0 = Expression.Dynamic(
            binder,
            typeof(object),
            Expression.Constant(オブジェクト),
            Expression.Constant(@int),
            Expression.Constant(@double),
            Expression.Constant(@string)
        );
        var actual=this.Execute2(Expression.Lambda<Func<object>>(Dynamic0));
        Assert.AreEqual(expected,actual);
        return expected;
    }

    [TestMethod, ExpectedException(typeof(RuntimeBinderException))]
    public void DynamicException()
    {
        var 引数 = new
        {
            c = "C"
        };
        var Dynamic0 = Expression.Dynamic(
            Binder.GetMember(
                CSharpBinderFlags.None,
                "c",
                typeof(Optimizer),//この匿名型はここでは定義していないので例外
                new[] {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null)
                }
            ),
            typeof(object),
            Expression.Constant(引数)
        );
        var Lambda = Expression.Lambda<Func<object>>(
            Dynamic0,
            false
        );
        var Func = Lambda.Compile();
        Assert.AreEqual("C", Func());
    }

    class テスト{
        public Func<int,int,int,bool> Delegate=(a,b,c)=>a==b&&b==c;
        public void Action(int a,double b,string c){}
        public bool Func(int a,double b,string c)=>a.Equals(b)&&b.Equals(c);
        public static int static_Func(int a,int b)=>a+b;
    }
    [TestMethod]
    public void Dynamic()
    {
        var anonymous = new {
            a = 1D,
            b = 2M,
            c = "A"
        };
        Assert.AreEqual(anonymous.c, this.DynamicGetMember(anonymous, nameof(anonymous.c)));
        foreach(var a in new[]{
                     ExpressionType.Add                  ,
                     ExpressionType.AddAssign            ,
                     ExpressionType.And                  ,
                     ExpressionType.AndAssign            ,
                     ExpressionType.Divide               ,
                     ExpressionType.DivideAssign         ,
                     ExpressionType.Equal                ,
                     ExpressionType.ExclusiveOr          ,
                     ExpressionType.ExclusiveOrAssign    ,
                     ExpressionType.GreaterThan          ,
                     ExpressionType.GreaterThanOrEqual   ,
                     ExpressionType.LeftShift            ,
                     ExpressionType.LeftShiftAssign      ,
                     ExpressionType.LessThan             ,
                     ExpressionType.LessThanOrEqual      ,
                     ExpressionType.Modulo               ,
                     ExpressionType.ModuloAssign         ,
                     ExpressionType.Multiply             ,
                     ExpressionType.MultiplyAssign       ,
                     ExpressionType.NotEqual             ,
                     ExpressionType.Or                   ,
                     ExpressionType.OrAssign             ,
                     ExpressionType.RightShift           ,
                     ExpressionType.RightShiftAssign     ,
                     ExpressionType.Subtract             ,
                     ExpressionType.SubtractAssign
                 }) {
            this.DynamicBinary(1,2,a);
        }
        //        }
        //    }
        //    if(UnaryOperationBinder!=null) {
        foreach(var a in new[]{
                     ExpressionType.Negate        ,
                     ExpressionType.Not           ,
                     ExpressionType.OnesComplement,
                     ExpressionType.Decrement     ,
                     ExpressionType.Increment     ,
                     ExpressionType.UnaryPlus
                 }) {
            this.DynamicUnary(new class_演算子オーバーロード(1,true),a);
            this.DynamicUnary(new struct_演算子オーバーロード(1,true),a);
        }
        foreach (var a in new[]{
                     ExpressionType.IsTrue        ,
                     ExpressionType.IsFalse
                 })
        {
            this.DynamicIsFalse_IsTrue(new class_演算子オーバーロード(1, true), a);
            this.DynamicIsFalse_IsTrue(new struct_演算子オーバーロード(1, true), a);
        }
        //    }
        //    if(ConvertBinder!=null) {
        //        if(Result.Type!=ConvertBinder_ReturnType){
        this.DynamicConvert<decimal, double>(2M,2d);
        this.DynamicConvert<double, decimal>(2D,2m);
        
        this.DynamicConvert<object, double>(2D,2d);
        this.DynamicConvert<object, decimal>(2d,2m);
        //    }
        //    if(GetIndexBinder!=null) return this.Dynamic(Dynamic0,DynamicReflection.GetIndex_Field,DynamicReflection.ObjectObjectObjectTarget);
        this.DynamicGetIndex(new double[] { 1, 2, 3 }, 2);
        this.DynamicGetIndex(new long[] { 1, 2, 3 }, 2);
        this.DynamicGetIndex(new decimal[] { 1, 2, 3 }, 2);
        //    if(SetIndexBinder!=null) return this.Dynamic(Dynamic0,DynamicReflection.ExtendSetIndex_Field,DynamicReflection.ObjectObjectObjectObjectTarget);
        this.DynamicSetIndex(new List<int> { 1, 2, 3 }, 1, 11);
        this.DynamicSetIndex(new int[3], 1, 11);
        //    if(GetMemberBinder!=null) {
        //        if(Dynamic1_Argument_0.Type.IsValueType) {
        Assert.AreEqual(1, this.DynamicGetMember(new struct_演算子オーバーロード { Int32フィールド = 1 }, nameof(struct_演算子オーバーロード.Int32フィールド)));
        Assert.AreEqual(2, this.DynamicGetMember(new struct_演算子オーバーロード { Int32プロパティ = 2 }, nameof(struct_演算子オーバーロード.Int32プロパティ)));
        Assert.AreEqual("A", this.DynamicGetMember(new struct_演算子オーバーロード { Stringフィールド = "A" }, nameof(struct_演算子オーバーロード.Stringフィールド)));
        //        }
        Assert.AreEqual("A", this.DynamicGetMember(new { a = 1D, b = 2M, c = "A" }, "c"));
        Assert.AreEqual(1D, this.DynamicGetMember(new { a = 1D, b = 2M, c = 'A' }, "a"));
        Assert.AreEqual(2M, this.DynamicGetMember(new { a = 1D, b = 2M, c = 'A' }, "b"));
        //    }
        //    if(SetMemberBinder!=null) {
        //        if(Dynamic1_Argument_0.Type.IsValueType) {
        //Assert.AreEqual(4, this.DynamicSetMember(new struct_演算子オーバーロード(), nameof(struct_演算子オーバーロード.Int32フィールド), 4));
        //        }
        this.DynamicSetMember(new class_演算子オーバーロード(), nameof(class_演算子オーバーロード.Int32フィールド), 0,4);
        //        if(Dynamic1_Argument_1.Type.IsValueType) {
        this.DynamicSetMember(new class_演算子オーバーロード(), nameof(class_演算子オーバーロード.Int32フィールド), 0,4);
        //        }
        this.DynamicSetMember(new class_演算子オーバーロード(), nameof(class_演算子オーバーロード.Stringフィールド),"初期", "設定");
        this.DynamicInvoke((int a,int b,int c)=>a==b&&a==c,1,2,3);
        this.DynamicInvokeMember_Func(new テスト(),"Func",1,2.0,"string");
        this.DynamicInvokeMember_Func(new テスト(),"Func",1,2.0,"string");
        this.DynamicInvokeMember_Action(new テスト(),"Action",1,2.0,"string");
        //    }
        //    throw new InvalidOperationException("Dynamicで実行されないはず");
        //}
    }
    private void DynamicIsFalse_IsTrue(object a, ExpressionType NodeType)
    {
        Expression operand = Expression.Constant(a);
        if (operand.Type.IsValueType)
        {
            operand = Expression.Convert(operand, typeof(object));
        }
        var Dynamic0 = Expression.Dynamic(
            Binder.UnaryOperation(
                CSharpBinderFlags.None,
                NodeType,
                typeof(Test_変換_単一メソッド),
                new[] {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                }
            ),
            typeof(bool),
            operand
        );
        this.Execute2(Expression.Lambda<Func<bool>>(Dynamic0));
    }
    [TestMethod]
    public void Convert()
    {
        this.Execute引数パターン(a => (int)a);
    }
    [TestMethod]
    public void ConvertChecked()
    {
        this.Execute引数パターン(a => checked((int)a));
        this.Execute引数パターン(a => checked((double)checked((int)a)));
    }

    [TestMethod]
    public void 祖先のSet1を取得()
    {
        //while(Type!=null) {
        //    var GenericTypeDefinition = Type;
        //    if(GenericTypeDefinition.IsGenericType)
        //        GenericTypeDefinition=Type.GetGenericTypeDefinition();
        //    if(GenericTypeDefinition==typeof(Set<>))
        this.Execute引数パターン(a => SetN<int>(a).Contains(0));
        //    Type=Type.BaseType;
        //}
        this.Execute引数パターン(a => EnuN<int>(a).Contains(0));
    }

    [TestMethod]
    public void Call()
    {
        //if(Reflection.ExtendSet.Any==MethodCall0_GenericMethodDefinition||Reflection.ExtendEnumerable.Any==MethodCall0_GenericMethodDefinition) {
        this.Execute引数パターン(a => ArrN<int>(a).Any());
        this.Execute引数パターン(a => EnuN<int>(a).Any());
        this.Execute引数パターン(a => SetN<int>(a).Any());
        //} else if(Reflection.ExtendEnumerable.Any_predicate==MethodCall0_GenericMethodDefinition) {
        this.Execute引数パターン(a => ArrN<int>(a).Any(p => p == 0));
        //}else if((Setか=(Reflection.ExtendSet1.Contains_value==MethodCall0_GenericMethodDefinition))||Reflection.ExtendEnumerable.Contains_value==MethodCall0_GenericMethodDefinition) {
        //    if(MethodCall1_Arguments_0_Type.IsArray) {
        this.Execute引数パターン(a => ArrN<int>(a).Contains(0));
        //    }else{
        //        GenericArguments=Set1?.GetGenericArguments()??TypeからIEnumerable1を取得(MethodCall1_Arguments_0_Type).GetGenericArguments();
        this.Execute引数パターン(a => SetN<int>(a).Contains(0));
        this.Execute引数パターン(a => EnuN<int>(a).Contains(0));
        //    }
        //    if(Setか){
        this.Execute引数パターン(a => SetN<int>(a).Contains(0));
        //    }else{
        this.Execute引数パターン(a => EnuN<int>(a).Contains(0));
        //    }
        //if(p_Type.IsPrimitive) {
        this.Execute引数パターン(a => new Set<double>().Contains(0));
        //} else {
        //    if(IEquatableType.IsAssignableFrom(p_Type)) {
        this.Execute2(() => new Set<struct_演算子オーバーロード>().Contains(new struct_演算子オーバーロード(0, true)));
        //    } else {
        //        if(q_Type.IsValueType) {
        this.Execute2(() => new Set<Point>().Contains(new Point(0, 3)));
        //        }
        this.Execute2(() => new Set<ValueType>().Contains(3));
        //    }
        //}
        //} else if(Reflection.ExtendSet1.Delete==MethodCall0_GenericMethodDefinition) {
        //    if(MethodCall1_predicate!=null) {
        this.Execute引数パターン(a => SetN<int>(a).Delete(p => p == 1));
        //    }
        this.Execute引数パターン(a => SetN<int>(a).Delete((Func<int, bool>)(p => p == 1)));
        //} else if(Reflection.ExtendSet1.Join==MethodCall0_GenericMethodDefinition) {
        //    if(outer.Type.GetGenericArguments().Length==1){
        //        if(inner.Type.GetGenericArguments().Length==1){
        this.Execute2(() => new Set<Tables.Entity>().Join(new Set<Tables.Entity>(), o => o.PrimaryKey, i => i.PrimaryKey, (o, i) => new { o, i }));
        //        }else{
        //            if(innerKeySelecter?.Body is MemberExpression innerKeySelecter_Body) {
        //                if(innerKeySelecter_Body_Expression==innerKeySelecter.Parameters[0]&&innerKeySelecter_Body.Member.MetadataToken==innerKeySelecter_Body_Expression.Type.GetProperty(nameof(IKeyEquatable<Int32>.PrimaryKey)).MetadataToken) {
        this.Execute2(() => new Set<Tables.Entity>().Join(new Set<Tables.Entity, PrimaryKeys.Entity, Container>(new Container2()), o => o.PrimaryKey, i => i.PrimaryKey, (o, i) => new { o, i }));
        //                }
        this.Execute2(() => new Set<Tables.Entity>().Join(new Set<Tables.Entity, PrimaryKeys.Entity, Container>(new Container2()), o => o.PrimaryKey.ID1, i => i.PrimaryKey.ID1, (o, i) => new { o, i }));
        var c = new Tables.Entity(1);
        this.Execute2(() => new Set<Tables.Entity>().Join(new Set<Tables.Entity, PrimaryKeys.Entity, Container>(new Container2()), o => o.PrimaryKey.ID1, i => c.PrimaryKey.ID1, (o, i) => new { o, i }));
        //            }
        this.Execute2(() => new Set<Tables.Entity>().Join(new Set<Tables.Entity, PrimaryKeys.Entity, Container>(new Container2()), o => o, i => i, (o, i) => new { o, i }));
        this.Execute2(() => new Set<Tables.Entity>().Join(new Set<Tables.Entity, PrimaryKeys.Entity, Container>(new Container2()), o => o, (Func<Tables.Entity,Tables.Entity>)(i => i), (o, i) => new { o, i }));
        //        }
        //    } else{
        //        if(outerKeySelecter?.Body is MemberExpression outerKeySelecter_Body) {
        //            if(outerKeySelecter_Body_Expression == outerKeySelecter.Parameters[0] && outerKeySelecter_Body.Member.MetadataToken == outerKeySelecter_Body_Expression.Type.GetProperty(nameof(IKeyEquatable<Int32>.PrimaryKey)).MetadataToken) {
        this.Execute2(() => new Set<Tables.Entity, PrimaryKeys.Entity, Container>(new Container2()).Join(new Set<Tables.Entity>(), o => o.PrimaryKey, i => i.PrimaryKey, (o, i) => new { o, i }));
        this.Execute2(() => new Set<Tables.Entity, PrimaryKeys.Entity, Container>(new Container2()).Join(new Set<Tables.Entity, PrimaryKeys.Entity, Container>(new Container2()), o => o.PrimaryKey, i => i.PrimaryKey, (o, i) => new { o, i }));
        //            }
        this.Execute2(() => new Set<Tables.Entity, PrimaryKeys.Entity, Container>(new Container2()).Join(new Set<Tables.Entity, PrimaryKeys.Entity, Container>(new Container2()), o => o.PrimaryKey.ID1, i => i.PrimaryKey.ID1, (o, i) => new { o, i }));
        this.Execute2(() => new Set<Tables.Entity, PrimaryKeys.Entity, Container>(new Container2()).Join(new Set<Tables.Entity, PrimaryKeys.Entity, Container>(new Container2()), o => c.PrimaryKey.ID1, i => i.PrimaryKey.ID1, (o, i) => new { o, i }));
        //        }
        this.Execute2(() => new Set<Tables.Entity, PrimaryKeys.Entity, Container>(new Container2()).Join(new Set<Tables.Entity, PrimaryKeys.Entity, Container>(new Container2()), o => o, i => i, (o, i) => new { o, i }));
        this.Execute2(() => new Set<Tables.Entity, PrimaryKeys.Entity, Container>(new Container2()).Join(new Set<Tables.Entity, PrimaryKeys.Entity, Container>(new Container2()), (Func<Tables.Entity, Tables.Entity>)(o => o), i => i, (o, i) => new { o, i }));
        //    }
        //}else if(Reflection.ExtendEnumerable.OfType==MethodCall0_GenericMethodDefinition){
        //    if(MethodCall1_Arguments_0_ElementType==MethodCall0_Method.ReturnType.GetGenericArguments()[0]){
        // ReSharper disable once RedundantEnumerableCastCall
        this.Execute引数パターン(a => ArrN<int>(a).OfType<int>().OfType<object>());
        //    }else{
        this.Execute引数パターン(a => ArrN<int>(a).OfType<object>());
        //    }
        //}else if(Reflection.ExtendSet1.OfType==MethodCall0_GenericMethodDefinition){
        //    if(MethodCall1_Arguments_0.Type==MethodCall0_Method.ReturnType){
        this.Execute引数パターン(a => SetN<int>(a).OfType<int>());
        //    }else{
        this.Execute引数パターン(a => SetN<int>(a).OfType<object>());
        //    }
        //} else if(Reflection.ExtendSet1.Select_selector==MethodCall0_GenericMethodDefinition||Reflection.ExtendEnumerable.Select_selector==MethodCall0_GenericMethodDefinition) {
        //  if(MethodCall1_selector!=null&&MethodCall1_selector.Parameters[0]==MethodCall1_selector.Body)return MethodCall1_Arguments[0];
        this.Execute引数パターン(a => SetN<int>(a).Select(p => p));
        this.Execute引数パターン(a => ArrN<int>(a).Select(p => p));
        this.Execute引数パターン(a => EnuN<int>(a).Select(p => p));
        this.Execute引数パターン(a => SetN<int>(a).Select(p => p + 1));
        this.Execute引数パターン(a => ArrN<int>(a).Select(p => p + 1));
        this.Execute引数パターン(a => EnuN<int>(a).Select(p => p + 1));
        this.Execute引数パターン(a => SetN<int>(a).Select((Func<int, int>)(p => p)));
        this.Execute引数パターン(a => ArrN<int>(a).Select((Func<int, int>)(p => p)));
        this.Execute引数パターン(a => EnuN<int>(a).Select((Func<int, int>)(p => p)));
        //} else if(Reflection.ExtendEnumerable.Single_predicate==MethodCall0_GenericMethodDefinition) {
        this.Execute2(() => ArrN<int>(1).Single(p => p == 0));
        //} else if(Reflection.ExtendEnumerable.SingleOrDefault_predicate==MethodCall0_GenericMethodDefinition) {
        this.Execute引数パターン(a => ArrN<int>(a).SingleOrDefault(p => p == 0));
        //} else if(Reflection.ExtendEnumerable.ToArray==MethodCall0_GenericMethodDefinition) {
        //    if(MethodCall1_Arguments_0.Type.IsArray) return MethodCall1_Arguments_0;
        this.Execute引数パターン(a => ArrN<int>(a).ToArray());
        this.Execute引数パターン(a => ArrN<int>(a).AsEnumerable().ToArray());
        //} else if(Reflection.ExtendSet1.Update==MethodCall0_GenericMethodDefinition) {
        this.Execute引数パターン(a => SetN<int>(a).Update(p => p == 0, p => p));
        this.Execute引数パターン(a => SetN<int>(a).Update(p => p == 0, (Func<int, int>)(p => p)));
        this.Execute引数パターン(a => SetN<int>(a).Update((Func<int, bool>)(p => p == 0), p => p));
        this.Execute引数パターン(a => SetN<int>(a).Update((Func<int, bool>)(p => p == 0), (Func<int, int>)(p => p)));
        //}else if((Setか=Reflection.ExtendSet1.SelectMany_collectionSelector_resultSelector==MethodCall0_GenericMethodDefinition)||Reflection.ExtendEnumerable.SelectMany_collectionSelector_resultSelector==MethodCall0_GenericMethodDefinition) {
        //    if(collectionSelector!=null&&resultSelector!=null) {
        //        if(Setか){
        this.Execute引数パターン標準ラムダループ((a, b) => SetN<int>(a).SelectMany(p => SetN<int>(b), (p, q) => new { p, q }));
        this.Execute引数パターン標準ラムダループ((a, b) => SetN<int>(a).SelectMany((Func<int, Set<int>>)(p => SetN<int>(b)), (p, q) => new { p, q }));
        this.Execute引数パターン標準ラムダループ((a, b) => SetN<int>(a).SelectMany(p => SetN<int>(b), (Func<int, int, int>)((p, q) => p + q)));
        this.Execute引数パターン標準ラムダループ((a, b) => SetN<int>(a).SelectMany((Func<int, Set<int>>)(p => SetN<int>(b)), (Func<int, int, int>)((p, q) => p + q)));
        //        }else{
        this.Execute引数パターン標準ラムダループ((a, b) => ArrN<int>(a).SelectMany(p => ArrN<int>(b), (p, q) => p + q));
        this.Execute引数パターン標準ラムダループ((a, b) => ArrN<int>(a).SelectMany((Func<int, IEnumerable<int>>)(p => ArrN<int>(a)), (p, q) => p + q));
        this.Execute引数パターン標準ラムダループ((a, b) => ArrN<int>(a).SelectMany(p => ArrN<int>(b), (Func<int, int, int>)((p, q) => p + q)));
        this.Execute引数パターン標準ラムダループ((a, b) => ArrN<int>(a).SelectMany((Func<int, IEnumerable<int>>)(p => ArrN<int>(b)), (Func<int, int, int>)((p, q) => p + q)));
        //        }
        //    }
        this.Execute引数パターン標準ラムダループ((a, b) => ArrN<int>(a).SelectMany(p => ArrN<int>(b), (p, q) => p + q));
        //}else if(Reflection.ExtendEnumerable.SelectMany_indexCollectionSelector_resultSelector==MethodCall0_GenericMethodDefinition) {
        //    if(indexCollectionSelector!=null&&resultSelector!=null) {
        this.Execute引数パターン標準ラムダループ((a, b) => ArrN<int>(a).SelectMany((p, i) => ArrN<int>(b), (p, q) => new { p, q }));
        //    }
        this.Execute引数パターン標準ラムダループ((a, b) => ArrN<int>(a).SelectMany((p, i) => ArrN<int>(b), (Func<int, int, int>)((p, q) => p + q)));
        this.Execute引数パターン標準ラムダループ((a, b) => ArrN<int>(a).SelectMany((Func<int, int, IEnumerable<int>>)((p, i) => ArrN<int>(b)), (p, q) => new { p, q }));
        this.Execute引数パターン標準ラムダループ((a, b) => ArrN<int>(a).SelectMany((Func<int, int, IEnumerable<int>>)((p, i) => ArrN<int>(b)), (Func<int, int, int>)((p, q) => p + q)));
        //}
        //if(!MethodCall0_Method.IsStatic) {
        //    foreach(var ChildMethod in MethodCall1_Object_Type.GetMethods(BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.Public))
        //        if((ChildMethod.IsFinal||MethodCall1_Object_Type.IsSealed)&&ChildMethod.GetBaseDefinition()==MethodCall0_Method) {
        this.Execute2(() => new STestClass().Virtual());
        this.Execute2(() => new TestClass2().Virtual());
        //        }
        this.Execute2(() => new TestClass().Virtual());
        //}
        this.Execute2(() => int.Parse("30"));
    }
    private interface ITest
    {
        string Interface();
    }
    private struct TestStruct : ITest
    {
        public string Interface() => "TestStruct Interface";
        public string Method() => "TestStruct Method";
        public override string ToString() => "TestStruct";
    }
    private abstract class ATestClass : ITest
    {
        public string Interface() => "ATestClass Interface";
        public abstract string Abstract();
        public virtual string Virtual() => "ATestClass virtual Virtual";
        public string New() => "ATestClass New";
        public override string ToString() => "ATestClass";
    }
    private class TestClass : ATestClass
    {
        public override string Abstract() => "TestClass override Abstract";
        public override string Virtual() => "TestClass override Virtual";
        public new string New() => "TestClass new New";
        public override string ToString() => "TestClass";
    }
    private sealed class STestClass : TestClass
    {
        public override string Virtual() => "STestClass override Virtual";
        public new string New() => "STestClass new New";
        public override string ToString() => "STestClass";
    }
    private class TestClass2 : TestClass
    {
        public sealed override string Virtual() => "TestClass2 sealed override Virtual";
        public new string New() => "TestClass2 new New";
        public sealed override string ToString() => "TestClass2";
    }
    [TestMethod]
    public void Decrement() => this.共通Unary(ExpressionType.Decrement);
    [TestMethod]
    public void Divide() => this.共通四則演算(ExpressionType.Divide);
    [TestMethod]
    public void DivideAssign() => this.共通四則演算Assign(ExpressionType.DivideAssign);
    [TestMethod]
    public void Equal() => this.共通大小等価不等価(ExpressionType.Equal);
    [TestMethod]
    public void ExclusiveOr() => this.共通AndOrXorNパターン(ExpressionType.ExclusiveOr);

    private static struct_ショートカット検証? ExclusiveOr実装(struct_ショートカット検証?a,struct_ショートカット検証?b){
        return a^b;
    }
    [TestMethod]
    public void ExclusiveOrAssign() => this.共通AndOrXorAssignNパターン(ExpressionType.ExclusiveOrAssign);
    [TestMethod]
    public void GreaterThan() => this.共通大小等価不等価(ExpressionType.GreaterThan);
    [TestMethod]
    public void GreaterThanOrEqual() => this.共通大小等価不等価(ExpressionType.GreaterThanOrEqual);
    [TestMethod]
    public void Increment() => this.共通Unary(ExpressionType.Increment);
    [TestMethod]
    public void IsFalse()
    {
        this.Execute2(
            Expression.Lambda<Func<bool>>(
                Expression.IsFalse(
                    Expression.Default(typeof(struct_ショートカット検証))
                )
            )
        );
    }
    [TestMethod]
    public void IsTrue()
    {
        this.Execute2(
            Expression.Lambda<Func<bool>>(
                Expression.IsTrue(
                    Expression.Default(typeof(struct_ショートカット検証))
                )
            )
        );
    }

    [TestMethod]
    public void Join()
    {
        var S = new Set<int> { 1, 2, 3 };
        this.Execute2(() => S.Join(S, o => o, i => i, (o, i) => new { o, i }));
        this.Execute2(() => S.Join(S, o => o, i => i, (o, i) => new { o, i }));
        this.Execute2(() => S.Join(S, o => o, i => i, resultSelector));
    }
    [TestMethod]
    public void JoinOuterKey()
    {
        var EntitySet = new Set<Tables.Entity,PrimaryKeys.Entity, Container>(new Container2()){
            new(1),
            new(2),
            new(3)
        };
        var Set = new Set<int> { 1, 2, 3 };
        this.Execute2(() => EntitySet.Join(Set, entity_set => entity_set.PrimaryKey, set => new PrimaryKeys.Entity(set, set), (entity_set, set) => new { entity_set, set }));
        this.Execute2(() => EntitySet.Join(Set, entity_set => entity_set.PrimaryKey, set => new PrimaryKeys.Entity(set, set), (entity_set, set) => new { entity_set, set }));
        this.Execute2(() => EntitySet.Join(Set, entity_set => entity_set.PrimaryKey, set => new PrimaryKeys.Entity(set, set), (Func<Tables.Entity, int, object>)((entity_set, set) => new { entity_set, set })));
    }
    [TestMethod]
    public void InternalJoin_outerKeySelector()
    {
        var Set = new Set<int> { 1, 2, 3 };
        var EntitySet = new Set<Tables.Entity,PrimaryKeys.Entity, Container>(new Container2()){
            new(1),
            new(2),
            new(3)
        };
        this.Execute2(() => Set.Join(EntitySet, set => new PrimaryKeys.Entity(set, set), entity_set => entity_set.PrimaryKey, (set, entity_set) => new { set, i = entity_set }));
        this.Execute2(() => Set.Join(EntitySet, set => new PrimaryKeys.Entity(set, set), entity_set => entity_set.PrimaryKey, (set, entity_set) => new { set, i = entity_set }));
        this.Execute2(() => Set.Join(EntitySet, set => new PrimaryKeys.Entity(set, set), entity_set => entity_set.PrimaryKey, (Func<int, Tables.Entity, object>)((set, entity_set) => new { set, entity_set })));
    }

    [TestMethod]
    public void Lambda()
    {
        this.Execute2(
            Expression.Lambda<Func<bool>>(
                Expression.Constant(true)
            )
        );
    }
    private void 共通Shift<TLeft, TRight>(TLeft a, TRight b, ExpressionType NodeType)
    {
        this.Execute2(
            Expression.Lambda<Func<TLeft>>(
                Expression.MakeBinary(
                    NodeType,
                    Expression.Constant(a, typeof(TLeft)),
                    Expression.Constant(b, typeof(TRight))
                )
            )
        );
    }
    private void 共通Shift(ExpressionType NodeType)
    {
        this.共通Shift(1, 2, NodeType);
        this.共通Shift<uint, int>(1, 2, NodeType);
        this.共通Shift<long, int>(1, 2, NodeType);
        this.共通Shift<ulong, int>(1, 2, NodeType);
    }
    private void 共通ShiftAssgin<TLeft, TRight>(TLeft a, TRight b, ExpressionType NodeType)
    {
        var p = Expression.Parameter(typeof(TLeft));
        this.Execute2(
            Expression.Lambda<Func<TLeft>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Constant(a, typeof(TLeft))
                    ),
                    Expression.MakeBinary(
                        NodeType,
                        p,
                        Expression.Constant(b, typeof(TRight))
                    )
                )
            )
        );
    }
    private void 共通ShiftAssign(ExpressionType NodeType)
    {
        this.共通ShiftAssgin(1, 2, NodeType);
        this.共通ShiftAssgin<uint, int>(1, 2, NodeType);
        this.共通ShiftAssgin<long, int>(1, 2, NodeType);
        this.共通ShiftAssgin<ulong, int>(1, 2, NodeType);
    }
    [TestMethod]
    public void LeftShift() => this.共通Shift(ExpressionType.LeftShift);
    [TestMethod]
    public void LeftShiftAssign() => this.共通ShiftAssign(ExpressionType.LeftShiftAssign);
    [TestMethod]
    public void LessThan() => this.共通大小等価不等価(ExpressionType.LessThan);
    [TestMethod]
    public void LessThanOrEqual() => this.共通大小等価不等価(ExpressionType.LessThanOrEqual);
    [TestMethod]
    public void Modulo() => this.共通四則演算(ExpressionType.Modulo);
    [TestMethod]
    public void ModuloAssign() => this.共通四則演算Assign(ExpressionType.ModuloAssign);
    [TestMethod]
    public void Multipty() => this.共通四則演算(ExpressionType.Multiply);
    [TestMethod]
    public void MultiplyAssign() => this.共通四則演算Assign(ExpressionType.MultiplyAssign);
    [TestMethod]
    public void MultiplyCheckedAssign() => this.共通四則演算AssignChecked(ExpressionType.MultiplyAssignChecked);
    [TestMethod]
    public void MultiplyChecked() => this.共通四則演算Checked(ExpressionType.MultiplyChecked);
    private void 共通Negate(ExpressionType NodeType)
    {
        this.共通UnaryNullable対応(1, NodeType);
        this.共通UnaryNullable対応(1L, NodeType);
        this.共通UnaryNullable対応(1F, NodeType);
        this.共通UnaryNullable対応(1D, NodeType);
        this.共通UnaryNullable対応(1M, NodeType);
        this.共通UnaryNullable対応(new struct_ショートカット検証(), NodeType);
    }
    [TestMethod]
    public void Negate() => this.共通Negate(ExpressionType.Negate);
    [TestMethod]
    public void NegateChecked() => this.共通Negate(ExpressionType.NegateChecked);
    [TestMethod]
    public void Not()
    {
        this.共通UnaryNullable対応(1, ExpressionType.Not);
        this.共通UnaryNullable対応(1U, ExpressionType.Not);
        this.共通UnaryNullable対応(1L, ExpressionType.Not);
        this.共通UnaryNullable対応(1UL, ExpressionType.Not);
        this.共通UnaryNullable対応(new struct_ショートカット検証(), ExpressionType.Not);
    }
    [TestMethod]
    public void NotEqual() => this.共通大小等価不等価(ExpressionType.NotEqual);
    [TestMethod]
    public void OnesComplement()
    {
        this.共通UnaryNullable対応(1, ExpressionType.OnesComplement);
        this.共通UnaryNullable対応(1U, ExpressionType.OnesComplement);
        this.共通UnaryNullable対応(1L, ExpressionType.OnesComplement);
        this.共通UnaryNullable対応(1UL, ExpressionType.OnesComplement);
        this.共通UnaryNullable対応(new struct_ショートカット検証(), ExpressionType.OnesComplement);
    }
    [TestMethod]
    public void Or() => this.共通AndOrXorNパターン(ExpressionType.Or);
    [TestMethod]
    public void OrAssign() => this.共通AndOrXorAssignNパターン(ExpressionType.OrAssign);
    [TestMethod]
    public void OrElse() => this.共通AndOr短絡評価(ExpressionType.OrElse);
    [TestMethod]
    public void Parameter()
    {
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { i0 },
                    Expression.Assign(i0, Expression.Constant(4)),
                    i0
                )
            )
        );
    }
    [TestMethod] public void PostDecrementAssign() => this.共通PostPreIncrementDecrementAssign(ExpressionType.PostDecrementAssign);
    [TestMethod] public void PostIncrementAssign() => this.共通PostPreIncrementDecrementAssign(ExpressionType.PostIncrementAssign);
    [TestMethod]
    public void Power() => this.共通Binary1パターン<double>(2, 3, ExpressionType.Power);
    [TestMethod]
    public void PowerAssign() => this.共通BinaryAssignNパターン<double>(2, 3, ExpressionType.PowerAssign);
    [TestMethod] public void PreDecrementAssign() => this.共通PostPreIncrementDecrementAssign(ExpressionType.PreDecrementAssign);
    [TestMethod] public void PreIncrementAssign() => this.共通PostPreIncrementDecrementAssign(ExpressionType.PreIncrementAssign);
    [TestMethod]
    public void RightShift() => this.共通Shift(ExpressionType.RightShift);
    [TestMethod]
    public void RightShiftAssign() => this.共通ShiftAssign(ExpressionType.RightShiftAssign);
    [TestMethod]
    public void Subtract() => this.共通四則演算(ExpressionType.Subtract);
    [TestMethod]
    public void SubtractAssign() => this.共通四則演算Assign(ExpressionType.SubtractAssign);
    [TestMethod]
    public void SubtractAssignChecked() => this.共通四則演算AssignChecked(ExpressionType.SubtractAssignChecked);
    [TestMethod]
    public void SubtractChecked() => this.共通四則演算Checked(ExpressionType.SubtractChecked);
    [TestMethod]
    public void Try()
    {
        var 引数 = Expression.Parameter(typeof(int?));
        var 式0 = Expression.AddAssign(
            引数,
            Expression.Constant(1, typeof(int?))
        );
        var ex = Expression.Parameter(typeof(Exception), "Exception");
        //if (Try0_Body != Try1_Body)
        {
            var M = this.CreateDelegate(
                Expression.Lambda<Func<int?, int?>>(
                    Expression.TryCatch(
                        式0,
                        Expression.Catch(
                            ex,
                            Expression.Constant(3, typeof(int?))
                        )
                    ),
                    引数
                )
            );
            Assert.AreEqual(M(1), 2);
        }
        //if (Try0_Finally != Try1_Finally)
        {
            //Catchパラメーターあり
            var M = this.CreateDelegate(
                Expression.Lambda<Func<int?, int?>>(
                    Expression.TryFinally(
                        引数,
                        式0
                    ),
                    引数
                )
            );
            Assert.AreEqual(M(1), 1);
        }
        //for (var a = 0; a < Try0_Handlers_Count; a++){
        //    if (Try0_Handler_Variable != null){
        //        if (Try0_Handler.Body != Try1_Handler_Body || Try0_Handler.Filter != Try1_Handler_Filter){
        {
            var M = this.CreateDelegate(
                Expression.Lambda<Func<int?, int?>>(
                    Expression.TryCatch(
                        式0,
                        Expression.Catch(
                            ex,
                            Expression.Constant(3, typeof(int?))
                        )
                    ),
                    引数
                )
            );
            Assert.AreEqual(M(1), 2);
        }
        try
        {
            this.CreateDelegate(
                Expression.Lambda<Func<int?, int?>>(
                    Expression.TryCatch(
                        Expression.Throw(Expression.Constant(new DivideByZeroException()), typeof(int?)),
                        Expression.Catch(
                            ex,
                            式0,
                            Expression.TypeEqual(ex, typeof(DivideByZeroException))
                        )
                    ),
                    引数
                )
            );
        }
        catch (NotSupportedException) { }
        //        }else
        //    }else{
        //        if (Try0_Handler.Body != Try1_Handler_Body||Try0_Handler.Filter!=Try1_Handler_Filter) {
        {
            var M = this.CreateDelegate(
                Expression.Lambda<Func<int?, int?>>(
                    Expression.TryCatch(
                        式0,
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(3, typeof(int?))
                        )
                    ),
                    引数
                )
            );
            Assert.AreEqual(M(1), 2);
        }
        try
        {
            this.CreateDelegate(
                Expression.Lambda<Func<int?, int?>>(
                    Expression.TryCatch(
                        式0,
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(3, typeof(int?)),
                            Expression.Constant(true)
                        )
                    ),
                    引数
                )
            );
        }
        catch (NotSupportedException) { }
        //        }else
        //    }
        //}
    }
    [TestMethod]
    public void UnaryPlus()
    {
        this.共通UnaryNullable対応(new struct_ショートカット検証(1, true), ExpressionType.UnaryPlus);
    }
    [TestMethod]
    public void Unbox()
    {
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Unbox(
                    Expression.Constant(1, typeof(object)),
                    typeof(int)
                )
            )
        );
    }
    [TestMethod]
    public void 共通And() => this.共通AndOrXorNパターン(ExpressionType.And);
    [TestMethod]
    public void 共通BinaryAssignNullable対応() => this.共通四則演算Assign(ExpressionType.AddAssign);
    [TestMethod]
    public void 共通BinaryNullable() => this.共通四則演算Assign(ExpressionType.AddAssign);
    [TestMethod]
    public void 共通BinaryNullable対応() => this.共通四則演算(ExpressionType.Add);
    private void 共通ConvertConvertChecked2<Tキャスト先>(object v)
    {
        this.Execute2(
            Expression.Lambda<Func<Tキャスト先>>(
                Expression.ConvertChecked(
                    Expression.Constant(v),
                    typeof(Tキャスト先)
                )
            )
        );
    }
    private delegate int Func2(int a);
    [TestMethod]
    public void 共通ConvertConvertChecked()
    {
        //if(Unary_Type==Unary_Operand_Type&&Unary_Operand.NodeType!=ExpressionType.Lambda) return this.Traverse(Unary_Operand);
        this.Execute2(() => ((Func2)(Func2)(a => a))(3));
        //if(Unary0.Method==null)
        this.共通ConvertConvertChecked2<double>(2);
        //if(
        //    Unary0_Type==typeof(IntPtr)&&(Unary0_Operand_Type==typeof(Int32)||
        //    Unary0_Operand_Type==typeof(Int64))||
        //    Unary0_Type==typeof(UIntPtr)&&(Unary0_Operand_Type==typeof(UInt32)||
        //    Unary0_Operand_Type==typeof(UInt64))||
        //    (Unary0_Type==typeof(Int32)||Unary0_Type==typeof(Int64))&&Unary0_Operand_Type==typeof(IntPtr)||
        //    (Unary0_Type==typeof(UInt32)||Unary0_Type==typeof(UInt64))&&Unary0_Operand_Type==typeof(UIntPtr)
        //) {
        this.共通ConvertConvertChecked2<IntPtr>((int)2);
        this.共通ConvertConvertChecked2<IntPtr>((long)2);
        this.共通ConvertConvertChecked2<UIntPtr>((uint)2);
        this.共通ConvertConvertChecked2<UIntPtr>((ulong)2);
        this.共通ConvertConvertChecked2<int>((IntPtr)2);
        this.共通ConvertConvertChecked2<long>((IntPtr)2);
        this.共通ConvertConvertChecked2<uint>((UIntPtr)2);
        this.共通ConvertConvertChecked2<ulong>((UIntPtr)2);
        //} else {
        //    if(Unary0_Operand==Unary1_Operand)
        this.共通ConvertConvertChecked2<long>((decimal)2);
        var b = 3;
        this.Execute引数パターン(a => (decimal)(IntPtr)b);
        //}
    }
    [TestMethod]
    public void 共通Or() => this.共通AndOrXorNパターン(ExpressionType.Or);
    [TestMethod]
    public void 共通Post() => this.共通PostPreIncrementDecrementAssign(ExpressionType.PostDecrementAssign);
    [TestMethod]
    public void 共通Pre() => this.共通PostPreIncrementDecrementAssign(ExpressionType.PreDecrementAssign);
    [TestMethod]
    public void 共通UnaryNullable() => this.共通PostPreIncrementDecrementAssign(ExpressionType.PreDecrementAssign);
    [TestMethod]
    public void Dgecrement() => this.共通Unary(ExpressionType.Decrement);
    [TestMethod]
    public void 共通大小比較Nullable対応() => this.共通大小等価不等価(ExpressionType.GreaterThan);
}