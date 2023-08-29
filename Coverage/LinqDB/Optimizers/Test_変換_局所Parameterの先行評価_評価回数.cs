using System.Linq.Expressions;
using System.Reflection;

using LinqDB.Sets;

using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class Test_変換_局所Parameterの先行評価_評価回数:ATest {
    private static int StaticInt32_Property => ++_StaticInt32;
    //private static Int32 NoOptimize_StaticInt32_Property => ++_StaticInt32;
    private static int StaticInt32_Method() => ++_StaticInt32;
    //private static Int32 NoOptimize_StaticInt32_Method() => ++_StaticInt32;
    private static int StaticInt32_Field = 5;
    //private static Int32 NoOptimize_StaticInt32_Field = 6;
    private static readonly Set<int> data4 = new[] { 1,2,3,4 }.ToSet();
    [TestMethod]
    public void StaticInt32_Propertyの大域最適化0() {
        var Call = 式木Bodyを取り出す(() =>
            data4.Select(p =>
                StaticInt32_Property
            )
        );
        this.実行結果が一致するか確認(
            Expression.Lambda<Action>(
                Expression.Block(
                    Call,
                    Expression.Default(typeof(void))
                )
            ),
            ()=>_StaticInt32=0,
            ()=>Assert.AreEqual(1,_StaticInt32)
        );
    }
    [TestMethod]
    public void StaticInt32_Propertyの大域最適化1() {
        var Call = 式木Bodyを取り出す(() =>
            data4.Select(p =>
                StaticInt32_Property.NoLoopUnrolling()
            )
        );
        this.実行結果が一致するか確認(
            Expression.Lambda<Action>(
                Expression.Block(
                    Call,
                    Expression.Default(typeof(void))
                )
            ),
            ()=>_StaticInt32=0,
            ()=>Assert.AreEqual(data4.Count,_StaticInt32)
        );
    }
    [TestMethod]
    public void StaticInt32_Methodの大域最適化0() {
        var Call = 式木Bodyを取り出す(() =>
            data4.Select(p =>
                StaticInt32_Method()
            )
        );
        this.実行結果が一致するか確認(
            Expression.Lambda<Action>(
                Expression.Block(
                    Call,
                    Expression.Default(typeof(void))
                )
            ),
            ()=>_StaticInt32=0,
            ()=>Assert.AreEqual(1,_StaticInt32)
        );
    }
    [TestMethod]
    public void StaticInt32_Methodの大域最適化1() {
        var Call = 式木Bodyを取り出す(() =>
            data4.Select(p =>
                StaticInt32_Method().NoLoopUnrolling()
            )
        );
        this.実行結果が一致するか確認(
            Expression.Lambda<Action>(
                Expression.Block(
                    Call,
                    Expression.Default(typeof(void))
                )
            ),
            ()=>_StaticInt32=0,
            ()=>Assert.AreEqual(data4.Count,_StaticInt32)
        );
    }
    private int InstanceInt32_Method() => ++this._InstanceInt32;
    private int InstanceInt32_Property => ++this._InstanceInt32;
    [TestMethod]
    public void InstanceInt32_Propertyの大域最適化0() {
        var Call = 式木Bodyを取り出す(() =>
            data4.Select(p =>
                this.InstanceInt32_Property
            )
        );
        this.実行結果が一致するか確認(
            Expression.Lambda<Action>(
                Expression.Block(
                    Call,
                    Expression.Default(typeof(void))
                )
            ),
            ()=>this._InstanceInt32=0,
            ()=>Assert.AreEqual(1,this._InstanceInt32)
        );
    }
    [TestMethod]
    public void InstanceInt32_Propertyの大域最適化1() {
        var Call = 式木Bodyを取り出す(() =>
            data4.Select(p =>
                this.InstanceInt32_Property.NoLoopUnrolling()
            )
        );
        this.実行結果が一致するか確認(
            Expression.Lambda<Action>(
                Expression.Block(
                    Call,
                    Expression.Default(typeof(void))
                )
            ),
            ()=>this._InstanceInt32=0,
            ()=>Assert.AreEqual(data4.Count,this._InstanceInt32)
        );
    }
    private static Expression 式木Bodyを取り出す<T>(Expression<Func<T>> e) => e.Body;
    [TestMethod]
    public void InstanceInt32_Methodの大域最適化0() {
        var Call = 式木Bodyを取り出す(() =>
            data4.Select(p =>
                this.InstanceInt32_Method().NoLoopUnrolling()
            )
        );
        this.実行結果が一致するか確認(
            Expression.Lambda<Action>(
                Expression.Block(
                    Call,
                    Expression.Default(typeof(void))
                )
            ),
            ()=>this._InstanceInt32=0,
            ()=>Assert.AreEqual(data4.Count,this._InstanceInt32)
        );
    }
    [TestMethod]
    public void InstanceInt32_Methodの大域最適化1() {
        //data4.Select(p=>data4.Select(p =>this.InstanceInt32_Method())

        //t=this.InstanceInt32_Method()
        //data4.Select(p=>t)
        var Call = 式木Bodyを取り出す(() =>
            data4.Select(p =>
                this.InstanceInt32_Method()
            )
        );
        this.実行結果が一致するか確認(
            Expression.Lambda<Action>(
                Expression.Block(
                    Call,
                    Expression.Default(typeof(void))
                )
            ),
            ()=>this._InstanceInt32=0,
            ()=>Assert.AreEqual(1,this._InstanceInt32)
        );
    }
    [TestMethod]
    public void StaticInt32_Propertyのローカル最適化0() {
        //var v00=static_Property(++static)
        //v00+v00
        var Property = Expression.Property(
            null,
            typeof(Test_変換_局所Parameterの先行評価_評価回数).GetProperty(nameof(StaticInt32_Property),BindingFlags.NonPublic|BindingFlags.Static|BindingFlags.FlattenHierarchy)!
        );
        this.実行結果が一致するか確認(
            Expression.Lambda<Action>(
                Expression.Block(
                    Expression.Add(
                        Property,
                        Property
                    ),
                    Expression.Default(typeof(void))
                )
            ),
            ()=>_StaticInt32=0,
            ()=>Assert.AreEqual(1,_StaticInt32)
        );
    }
    [TestMethod]
    public void StaticInt32_Propertyのローカル最適化1() {
        //(++static)+(++static)
        var Property = Expression.Call(
            typeof(global::LinqDB.Sets.Helpers).GetMethod(nameof(global::LinqDB.Sets.Helpers.NoEarlyEvaluation))!.MakeGenericMethod(typeof(int)),
            Expression.Property(
                null,
                typeof(Test_変換_局所Parameterの先行評価_評価回数).GetProperty(nameof(StaticInt32_Property),BindingFlags.NonPublic|BindingFlags.Static|BindingFlags.FlattenHierarchy)!
            )
        );
        this.実行結果が一致するか確認(
            Expression.Lambda<Action>(
                Expression.Block(
                    Expression.Add(
                        Property,
                        Property
                    ),
                    Expression.Default(typeof(void))
                )
            ),
            ()=>_StaticInt32=0,
            ()=>Assert.AreEqual(2,_StaticInt32)
        );
    }
    [TestMethod]
    public void StaticInt32_Methodのローカル最適化0() {
        var Call = Expression.Call(typeof(Test_変換_局所Parameterの先行評価_評価回数).GetMethod(nameof(StaticInt32_Method),BindingFlags.NonPublic|BindingFlags.Static|BindingFlags.FlattenHierarchy)!);
        this.実行結果が一致するか確認(
            Expression.Lambda<Action>(
                Expression.Block(
                    Expression.Add(
                        Call,
                        Call
                    ),
                    Expression.Default(typeof(void))
                )
            ),
            ()=>_StaticInt32=0,
            ()=>Assert.AreEqual(1,_StaticInt32)
        );
    }
    [TestMethod]
    public void StaticInt32_Methodのローカル最適化1() {
        var Call = Expression.Call(
            typeof(global::LinqDB.Sets.Helpers).GetMethod(nameof(global::LinqDB.Sets.Helpers.NoEarlyEvaluation))!.MakeGenericMethod(typeof(int)),
            Expression.Call(typeof(Test_変換_局所Parameterの先行評価_評価回数).GetMethod(nameof(StaticInt32_Method),BindingFlags.NonPublic|BindingFlags.Static|BindingFlags.FlattenHierarchy)!)
        );
        this.実行結果が一致するか確認(
            Expression.Lambda<Action>(
                Expression.Block(
                    Expression.Add(
                        Call,
                        Call
                    ),
                    Expression.Default(typeof(void))
                )
            ),
            ()=>_StaticInt32=0,
            ()=>Assert.AreEqual(2,_StaticInt32)
        );
    }
    [TestMethod]
    public void InstanceInt32_Propertyのローカル最適化0() {
        var Property = Expression.Property(
            Expression.Constant(this),
            typeof(Test_変換_局所Parameterの先行評価_評価回数).GetProperty(
                nameof(this.InstanceInt32_Property),
                BindingFlags.NonPublic|BindingFlags.Instance
            )!
        );
        this.実行結果が一致するか確認(
            Expression.Lambda<Action>(
                Expression.Block(
                    Expression.Add(
                        Property,
                        Property
                    ),
                    Expression.Default(typeof(void))
                )
            ),
            ()=>this._InstanceInt32=0,
            ()=>Assert.AreEqual(1,this._InstanceInt32)
        );
    }
    [TestMethod]
    public void InstanceInt32_Propertyのローカル最適化1() {
        var MakeMemberAccess = Expression.Call(
            typeof(global::LinqDB.Sets.Helpers).GetMethod(nameof(global::LinqDB.Sets.Helpers.NoEarlyEvaluation))!.MakeGenericMethod(typeof(int)),
            Expression.MakeMemberAccess(
                Expression.Constant(this),
                typeof(Test_変換_局所Parameterの先行評価_評価回数).GetProperty(
                    nameof(this.InstanceInt32_Property),
                    BindingFlags.NonPublic|BindingFlags.Instance
                )!
            )
        );
        this.実行結果が一致するか確認(
            Expression.Lambda<Action>(
                Expression.Block(
                    Expression.Add(
                        MakeMemberAccess,
                        MakeMemberAccess
                    ),
                    Expression.Default(typeof(void))
                )
            ),
            ()=>this._InstanceInt32=0,
            ()=>Assert.AreEqual(2,this._InstanceInt32)
        );
    }
    [TestMethod]
    public void InstanceInt32_Methodのローカル最適化0() {
        //this.InstanceInt32_Method()+this.InstanceInt32_Method()
        //(t=this.InstanceInt32_Method())+t
        var Call = Expression.Call(
            Expression.Constant(this),
            typeof(Test_変換_局所Parameterの先行評価_評価回数).GetMethod(
                nameof(this.InstanceInt32_Method),
                BindingFlags.NonPublic|BindingFlags.Instance
            )!
        );
        this.実行結果が一致するか確認(
            Expression.Lambda<Action>(
                Expression.Block(
                    Expression.Add(
                        Call,
                        Call
                    ),
                    Expression.Default(typeof(void))
                )
            ),
            ()=>this._InstanceInt32=0,
            ()=>Assert.AreEqual(1,this._InstanceInt32)
        );
    }
    [TestMethod]
    public void InstanceInt32_Methodのローカル最適化1() {
        var Call = Expression.Call(
            typeof(global::LinqDB.Sets.Helpers).GetMethod(nameof(global::LinqDB.Sets.Helpers.NoEarlyEvaluation)).MakeGenericMethod(typeof(int)),
            Expression.Call(
                Expression.Constant(this),
                typeof(Test_変換_局所Parameterの先行評価_評価回数).GetMethod(
                    nameof(InstanceInt32_Method),
                    BindingFlags.NonPublic|BindingFlags.Instance
                )
            )
        );
        this.実行結果が一致するか確認(
            Expression.Lambda<Action>(
                Expression.Block(
                    Expression.Add(
                        Call,
                        Call
                    ),
                    Expression.Default(typeof(void))
                )
            ),
            ()=>this._InstanceInt32=0,
            ()=>Assert.AreEqual(2,this._InstanceInt32)
        );
    }
}
