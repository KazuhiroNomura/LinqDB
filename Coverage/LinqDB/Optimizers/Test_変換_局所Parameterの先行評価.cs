using System.Drawing;
using System.Linq.Expressions;
using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable ImplicitlyCapturedClosure
// ReSharper disable PossibleUnintendedReferenceComparison
// ReSharper disable ArrangeRedundantParentheses
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable ConditionalTernaryEqualBranch
// ReSharper disable ConvertToConstant.Local
// ReSharper disable StaticMemberInGenericType
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class Test_変換_局所Parameterの先行評価:ATest {
    [TestMethod]
    public void Conditionalラムダ局所1() {
        //(i,a)=>i?a*a:a*a
        //↑は変換されない
        var X=Expression.Label();
        var i = Expression.Parameter(typeof(bool),"i");
        var a = Expression.Parameter(typeof(decimal),"a");
        this.Execute2(
            Expression.Lambda<Func<bool,decimal,decimal>>(
                Expression.Condition(
                    i,
                    Expression.Multiply(a,a),
                    Expression.Multiply(Expression.Multiply(a,a),Expression.Multiply(a,a))
                ),
                i,a
            ),true,1m
        );
    }
    [TestMethod]
    public void Conditionalラムダ局所2() {
        //(i,a)=>i?(a*a)+(a*a):(a*a)-(a*a)
        //(i,a)=>i?(t=a*a)+t:(t=a*a)-t
        var X=Expression.Label();
        var i = Expression.Parameter(typeof(bool),"i");
        var a = Expression.Parameter(typeof(decimal),"a");
        this.Execute2(
            Expression.Lambda<Func<bool,decimal,decimal>>(
                Expression.Condition(
                    i,
                    Expression.Add(Expression.Multiply(a,a),Expression.Multiply(a,a)),
                    Expression.Subtract(Expression.Multiply(a,a),Expression.Multiply(a,a))
                ),
                i,a
            ),true,1m
        );
    }
    [TestMethod]
    public void Conditionalラムダ局所3() {
        //(i,a)=>(a*a)==0?(a*a)+(a*a):(a*a)-(a*a)
        //(i,a)=>(t=a*a)==0?t+t:t-t
        var a = Expression.Parameter(typeof(decimal),"a");
        this.Execute2(
            Expression.Lambda<Func<decimal,decimal>>(
                Expression.Condition(
                    Expression.Equal(Expression.Multiply(a,a),Expression.Constant(1m)),
                    Expression.Add(Expression.Multiply(a,a),Expression.Multiply(a,a)),
                    Expression.Subtract(Expression.Multiply(a,a),Expression.Multiply(a,a))
                ),
                a
            ),1m
        );
    }
    [TestMethod]
    public void Conditional4ラムダ局所() {
        Assert.AreEqual(
            4,
            //v.Let(a=>
            //    (a+a)+(a+a)==a
            //    ?(a+a)-(a+a)
            //    :(a+a)+(a+a)
            //↓
            //v.Let(a=>
            //    (t1=(t0=a+a)+t0)==a 4
            //    ?t0-t0              0
            //    :t0+t0              4
            this.Execute標準ラムダループ(
                v =>
                    v.Let(
                        a =>
                            (a+a)+(a+a)==a
                                ? (a+a)-(a+a)
                                : (a+a)+(a+a)
                    ),
                1
            )
        );
    }
    [TestMethod]
    public void Blockの複数式の局所先行評価() {
        var Point = new Point(1,2);
        var Point1 = Expression.Parameter(typeof(Point),"Point1");
        var Point2 = Expression.Parameter(typeof(Point),"Point2");
        var a = Expression.Parameter(typeof(int),"a");
        var Cラムダ局所1 = Expression.Parameter(typeof(int),"Cラムダ局所1");
        var Cラムダ局所0 = Expression.Parameter(typeof(Point),"Cラムダ局所0");
        this.Execute2(
            Expression.Lambda<Func<Point>>(
                Expression.Block(
                    new[] { Point1,Point2,a,Cラムダ局所1,Cラムダ局所0 },
                    Expression.Assign(
                        Point1,
                        Expression.Constant(new Point(0,1))
                    ),
                    Expression.Assign(
                        Point2,
                        Point1
                    ),
                    Expression.Assign(
                        a,
                        Expression.Constant(0)
                    ),
                    Expression.Assign(
                        Cラムダ局所1,
                        Expression.PropertyOrField(
                            Expression.Constant(Point),
                            nameof(Point.X)
                        )
                    ),
                    Expression.Assign(
                        Cラムダ局所0,
                        Expression.Constant(Point)
                    )
                )
            )
        );
    }
    //$Cループ跨ぎ0 = ._3;
    //$Cループ跨ぎ1 = .New DateTimeOffset(
    //    1996,
    //    12,
    //    31);
    //$Cループ跨ぎ2 = .New DateTimeOffset(
    //    1995,
    //    1,
    //    1);
    //$Cループ跨ぎ3 = ._2;
    //$Cループ跨ぎ4 = ._1;
    //$Cラムダ局所1 = (.e).NATION;
    //$Cラムダ局所0 = .e;
    [TestMethod]
    public void 等価式() {
        this.Execute2(() => (new[] { "A" } as IEnumerable<object>).Contains("A"));
        this.Execute引数パターン(a => ArrN<object>(a).Contains(2));
        this.Execute引数パターン(a => ArrN<int>(a).Contains(2));
        //if(a_Type.IsPrimitive) {
        this.Execute引数パターン(a => ArrN<int>(a).Where(p => p==1).Where(p => p==2));
        //} else if(IEquatableType.IsAssignableFrom(a_Type)) {
        this.Execute引数パターン(a => ArrN<decimal>(a).Where(p => p==1).Where(p => p==2));
        //} else {
        //    if(op_Equality!=null) {
        {
            var a = new Point(1,2);
            var b = new Point(2,3);
            this.Execute2(() =>
                new Point[]{
                    new(1,1),
                    new(1,2),
                    new(1,3),
                    new(2,1),
                    new(2,2),
                    new(2,3),
                    new(3,1),
                    new(3,2),
                    new(3,3)
                }.Where(p => p==a).Where(p => p==b));
        }
        //    } else {
        {
            var b = new object();
            var c = new object();
            this.Execute引数パターン(a => ArrN<object>(a).Where(p => p==b).Where(p => p==c));
        }
        //    }
        //}
    }
    [TestMethod]
    public void フロー考慮0() {
        //a=>{
        //  goto X
        //X:
        //  return a*a
        //}
        var X=Expression.Label();
        var a = Expression.Parameter(typeof(decimal),"変数");
        this.Execute2(
            Expression.Lambda<Func<decimal,decimal>>(
                Expression.Block(
                    Expression.Goto(X),
                    Expression.Label(X),
                    Expression.Multiply(Expression.Multiply(a,a),Expression.Multiply(a,a))
                ),
                a
            ),1m
        );
    }
    [TestMethod]
    public void フロー考慮1() {
        //(i,a)=>{
        //  if(i)goto X
        //  return a*a
        //X:
        //  return (a*a)*(a*a)
        //}
        //(i,a)=>{
        //  if(i)goto X
        //  return a*a
        //X:
        //  return (t=a*a)*t
        //}
        var X=Expression.Label();
        var i = Expression.Parameter(typeof(bool),"i");
        var a = Expression.Parameter(typeof(decimal),"a");
        this.Execute2(
            Expression.Lambda<Func<bool,decimal,decimal>>(
                Expression.Block(
                    Expression.IfThen(
                        i,
                        Expression.Goto(X)
                    ),
                    Expression.Multiply(a,a),
                    Expression.Label(X),
                    Expression.Multiply(Expression.Multiply(a,a),Expression.Multiply(a,a))
                ),
                i,a
            ),true,1m
        );
    }
    [TestMethod]
    public void フロー考慮3() {
        //a=>{
        //  if(a*a==0)goto X
        //  return (a*a)+(a*a)
        //X:
        //  return (a*a)-(a*a)
        //↑は↓と同等
        //a=>a*a==0?a*a:a*a
        //↑は↓に変換される
        //a=>{
        //  if(a*a==0)goto X
        //  return (a*a)+(a*a)
        //X:
        //  return (a*a)-(a*a)
        var X=Expression.Label();
        var a = Expression.Parameter(typeof(decimal),"a");
        this.Execute2(
            Expression.Lambda<Func<decimal,decimal>>(
                Expression.Block(
                    Expression.IfThen(
                        Expression.Equal(Expression.Multiply(a,a),Expression.Constant(1m)),
                        Expression.Goto(X)
                    ),
                    Expression.Add(Expression.Multiply(a,a),Expression.Multiply(a,a)),
                    Expression.Label(X),
                    Expression.Subtract(Expression.Multiply(a,a),Expression.Multiply(a,a))
                ),
                a
            ),1m
        );
    }
    [TestMethod]
    public void フロー考慮4() {
        //a=>{
        //  if((a+a)+(a+a)==a)goto X
        //  return (a+a)-(a+a)
        //X:
        //  return (a+a)+(a+a)
        //}
        //↑は↓と同等
        //a=>(a+a)+(a+a)==a?(a+a)+(a+a):(a+a)-(a+a)
        //↑は↓に変換される
        //a=>{
        //  if((t1=(t0=a+a)+t0)==a)goto X
        //  return t1
        //X:
        //  return t0-t0
        //}
        var X=Expression.Label();
        var a = Expression.Parameter(typeof(decimal),"a");
        this.Execute2(
            Expression.Lambda<Func<decimal,decimal>>(
                Expression.Block(
                    Expression.IfThen(
                        Expression.Equal(
                            Expression.Add(Expression.Multiply(a,a),Expression.Multiply(a,a)),
                            a
                        ),
                        Expression.Goto(X)
                    ),
                    Expression.Add(Expression.Multiply(a,a),Expression.Multiply(a,a)),
                    Expression.Label(X),
                    Expression.Subtract(Expression.Multiply(a,a),Expression.Multiply(a,a))
                ),
                a
            ),1m
        );
    }
    [TestMethod]
    public void 並び替え00() {
        var Break0 = Expression.Label(typeof(decimal),"Break0");
        var Break1 = Expression.Label(typeof(decimal),"Break1");
        var カウンタ0 = Expression.Parameter(typeof(decimal),"カウンタ0");
        var カウンタ1 = Expression.Parameter(typeof(decimal),"カウンタ1");
        this.Execute2(
            Expression.Lambda<Func<decimal>>(
                Expression.Block(
                    new[]{
                        カウンタ0,カウンタ1
                    },
                    Expression.Assign(
                        カウンタ0,
                        Expression.Constant(1m)
                    ),
                    Expression.Constant(1m),
                    Expression.Loop(
                        Expression.IfThen(
                            Expression.Equal(
                                カウンタ0,
                                Expression.Constant(1m)
                            ),
                            Expression.Goto(Break0,Expression.Constant(10m))
                        ),
                        Break0
                    ),
                    Expression.Assign(
                        カウンタ1,
                        Expression.Constant(2m)
                    ),
                    Expression.Loop(
                        Expression.IfThen(
                            Expression.Equal(
                                カウンタ1,
                                Expression.Constant(2m)
                            ),
                            Expression.Goto(Break1,Expression.Constant(20m))
                        ),
                        Break1
                    )
                )
            )
        );
    }
    [TestMethod]
    public void 並び替え01() {
        var a = Expression.Parameter(typeof(int),"a");
        var b = Expression.Parameter(typeof(int),"b");
        var Parameter配列 = Expression.Parameter(typeof(int[]),"配列");
        //左辺式の参照は一時変数に代入できないが内部式は代入できる。例えば
        //Array[a*2+a*2] = a
        //↓
        //b=a*2
        //Array[b+b]=a
        Assert.AreEqual(
            1,
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { a,b,Parameter配列 },
                        Expression.Assign(
                            a,
                            Expression.Constant(1)
                        ),
                        Expression.Assign(
                            Parameter配列,
                            Expression.NewArrayBounds(
                                typeof(int),
                                Expression.Constant(5)
                            )
                        ),
                        Expression.Assign(
                            Expression.ArrayAccess(
                                Parameter配列,
                                Expression.Add(
                                    Expression.Multiply(
                                        a,
                                        Expression.Constant(2)
                                    ),
                                    Expression.Multiply(
                                        a,
                                        Expression.Constant(2)
                                    )
                                )
                            ),
                            a
                        ),
                        Expression.ArrayIndex(
                            Parameter配列,
                            Expression.Constant(4)
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void 並び替え02() {
        var a = Expression.Parameter(typeof(int),"a");
        var b = Expression.Parameter(typeof(int),"b");
        //a=1,b=2
        //a=a+b 1+2=3
        //b=a+b 3+2=5

        //$a = 1;
        //$b = 2;
        //$a = ($局所0 = $a + $b); 3
        //$b = $局所0
        //↑はだめ。aが書き込まれているので
        //$a = 1;
        //$b = 2;
        //$a = $a + $b;
        //$b = $a + $b
        //であるべき
        Assert.AreEqual(
            5,
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { a,b },
                        Expression.Assign(
                            a,
                            Expression.Constant(1)
                        ),
                        Expression.Assign(
                            b,
                            Expression.Constant(2)
                        ),
                        Expression.Assign(
                            a,
                            Expression.Add(
                                a,
                                b
                            )
                        ),
                        Expression.Assign(
                            b,
                            Expression.Add(
                                a,
                                b
                            )
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void 並び替え03() {
        var a = Expression.Parameter(typeof(int),"a");
        var b = Expression.Parameter(typeof(int),"b");
        Assert.AreEqual(
            4,
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { a,b },
                        Expression.Assign(
                            a,
                            Expression.Constant(1)
                        ),
                        Expression.Assign(
                            a,
                            Expression.Add(
                                a,
                                a
                            )
                        ),
                        Expression.Assign(
                            a,
                            Expression.Add(
                                a,
                                a
                            )
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void 並び替え04() {
        var a = Expression.Parameter(typeof(int),"a");
        var b = Expression.Parameter(typeof(int),"b");
        Assert.AreEqual(
            5,
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { a,b },
                        Expression.Assign(
                            a,
                            Expression.Constant(1)
                        ),
                        Expression.Assign(
                            b,
                            Expression.Add(
                                a,
                                Expression.Constant(1)
                            )
                        ),
                        Expression.Assign(
                            a,
                            Expression.Add(
                                a,
                                b
                            )
                        ),
                        Expression.Assign(
                            b,
                            Expression.Add(
                                a,
                                b
                            )
                        ),
                        b
                    )
                )
            )
        );
    }
    [TestMethod]
    public void 並び替え05() {
        var a = Expression.Parameter(typeof(int),"a");
        var b = Expression.Parameter(typeof(int),"b");
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { a,b },
                    Expression.AddAssign(
                        a,
                        b
                    ),
                    Expression.Assign(
                        b,
                        Expression.Constant(2)
                    ),
                    Expression.Assign(
                        a,
                        Expression.Constant(1)
                    )
                )
            )
        );
    }
    [TestMethod]
    public void 並び替え06() {
        var Parameter配列 = Expression.Parameter(typeof(int[]),"配列");
        var 配列 = new int[3];
        this.Execute標準ラムダループ(
            Expression.Lambda<Action>(
                Expression.Block(
                    new[] { Parameter配列 },
                    Expression.Assign(
                        Parameter配列,
                        Expression.Constant(配列)
                    ),
                    Expression.Assign(
                        Expression.ArrayAccess(
                            Parameter配列,
                            Expression.Constant(1)
                        ),
                        Expression.Constant(2)
                    ),
                    Expression.Default(typeof(void))
                )
            )
        );
        Assert.AreEqual(2,配列[1]);
    }
    [TestMethod]
    public void 並び替え07() {
        var a = Expression.Parameter(typeof(int),"a");
        var b = Expression.Parameter(typeof(int),"b");
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { a,b },
                    Expression.Add(
                        Expression.Add(
                            Expression.Constant(1),
                            Expression.Constant(2)
                        ),
                        Expression.Add(
                            Expression.Constant(3),
                            Expression.Constant(4)
                        )
                    ),
                    Expression.Assign(
                        b,
                        Expression.Add(
                            Expression.Constant(3),
                            Expression.Constant(4)
                        )
                    ),
                    Expression.Assign(
                        a,
                        Expression.Add(
                            Expression.Constant(1),
                            Expression.Constant(2)
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void 並び替え08() {
        var a = Expression.Parameter(typeof(int),"a");
        var b = Expression.Parameter(typeof(int),"b");
        var c = Expression.Parameter(typeof(int),"c");
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { a,b,c },
                    Expression.Assign(
                        b,
                        Expression.Constant(10)
                    ),
                    Expression.Assign(
                        a,
                        b
                    ),
                    Expression.Assign(
                        b,
                        c
                    ),
                    Expression.Assign(
                        c,
                        a
                    ),
                    a
                )
            )
        );
    }
    [TestMethod]
    public void 並び替え09() {
        var a = Expression.Parameter(typeof(int),"a");
        var b = Expression.Parameter(typeof(int),"b");
        Assert.AreEqual(
            3,
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { a,b },
                        Expression.Assign(
                            a,
                            Expression.Constant(1)
                        ),
                        Expression.Assign(
                            b,
                            Expression.Add(
                                a,
                                Expression.Constant(1)
                            )
                        ),
                        Expression.Assign(
                            a,
                            Expression.Add(
                                a,
                                b
                            )
                        ),
                        a
                    )
                )
            )
        );
    }
    [TestMethod]
    public void 並び替え10() {
        //a=1
        //b=1
        //a=a+(b+1) 3
        //b=a+(b+1) 5
        //b

        //a=1
        //b=1
        //a=a+(t=b+1)
        //b=a+t
        var a = Expression.Parameter(typeof(int),"a");
        var b = Expression.Parameter(typeof(int),"b");
        var Constant1 = Expression.Constant(1);
        var b_plus_1 = Expression.Add(
            b,
            Constant1
        );
        Assert.AreEqual(
            5,
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { a,b },
                        Expression.Assign(a,Constant1),
                        Expression.Assign(b,Constant1),
                        Expression.Assign(//a=a+b+1 a:3
                            a,
                            Expression.Add(a,b_plus_1)
                        ),
                        Expression.Assign(//b=a+b+1 b:5
                            b,
                            Expression.Add(a,b_plus_1)
                        ),
                        b
                    )
                )
            )
        );
    }
    [TestMethod]
    public void 並び替え11() {
        //a=1
        //b=1
        //c=1
        //a=a+(b+1) 3
        //b=a+(b+1) 5
        //c=a+(b+1) 5
        //c

        //a=1
        //b=1
        //a=a+(t=b+1)
        //b=a+t
        var a = Expression.Parameter(typeof(int),"a");
        var b = Expression.Parameter(typeof(int),"b");
        var c = Expression.Parameter(typeof(int),"c");
        var Constant1 = Expression.Constant(1);
        var b_plus_1 = Expression.Add(b,Constant1);
        Assert.AreEqual(
            9,
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { a,b,c },
                        Expression.Assign(a,Constant1),//a=1
                        Expression.Assign(b,Constant1),//b=1
                        Expression.Assign(             //a=a+b+1 a:3
                            a,
                            Expression.Add(
                                a,
                                b_plus_1
                            )
                        ),
                        Expression.Assign(             //b=a+b+1  b:5
                            b,
                            Expression.Add(
                                a,
                                b_plus_1
                            )
                        ),
                        Expression.Assign(             //c=a+b+1  c:9
                            c,
                            Expression.Add(
                                a,
                                b_plus_1
                            )
                        ),
                        c
                    )
                )
            )
        );
    }
    [TestMethod]
    public void 並び替え12() {
        //a=1
        //b=1
        //a=a+(b+1) 3
        //b=a+(b+1) 5
        //a=a+(b+1) 9
        //b+1       6

        //a=1
        //b=1
        //a=a+(t0=b+1)
        //b=a+t0
        //a=a+(t1=b+1)
        //t1
        var a = Expression.Parameter(typeof(int),"a");
        var b = Expression.Parameter(typeof(int),"b");
        var Constant1 = Expression.Constant(1);
        var e0 = Expression.Add(
            b,
            Constant1
        );
        Assert.AreEqual(
            6,
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { a,b},
                        Expression.Assign(
                            a,
                            Constant1
                        ),
                        Expression.Assign(
                            b,
                            Constant1
                        ),
                        Expression.Assign(
                            a,//3
                            Expression.Add(
                                a,//1
                                e0//2
                            )
                        ),
                        Expression.Assign(
                            b,//5
                            Expression.Add(
                                a,//3
                                e0//2
                            )
                        ),
                        Expression.Assign(
                            a,//9
                            Expression.Add(
                                a,//3
                                e0//6
                            )
                        ),
                        e0
                    )
                )
            )
        );
    }
    [TestMethod]
    public void 並び替え13() {
        //a=1
        //b=1
        //a=a+(b+1) 3
        //b=a+(b+1) 5
        //c=a+(b+1) 9
        //d=a+(b+1) 9
        //d

        //a=1
        //b=1
        //a=a+(t0=b+1)
        //b=a+t0
        //c=a+(t1=b+1)
        //d=a+t1
        //d
        var a = Expression.Parameter(typeof(int),"a");
        var b = Expression.Parameter(typeof(int),"b");
        var c = Expression.Parameter(typeof(int),"c");
        var d = Expression.Parameter(typeof(int),"d");
        var Constant1 = Expression.Constant(1);
        var e0 = Expression.Add(
            b,
            Constant1
        );
        Assert.AreEqual(
            9,
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { a,b,c,d },
                        Expression.Assign(
                            a,
                            Constant1
                        ),
                        Expression.Assign(
                            b,
                            Constant1
                        ),
                        Expression.Assign(
                            a,
                            Expression.Add(
                                a,
                                e0
                            )
                        ),
                        Expression.Assign(
                            b,
                            Expression.Add(
                                a,
                                e0
                            )
                        ),
                        Expression.Assign(
                            c,
                            Expression.Add(
                                a,
                                e0
                            )
                        ),
                        Expression.Assign(
                            d,
                            Expression.Add(
                                a,
                                e0
                            )
                        ),
                        d
                    )
                )
            )
        );
    }
    [TestMethod]
    public void 並び替え14() {
        //a=1
        //b=1
        //a=a+1 2
        //b=a+1 3
        //b
        var a = Expression.Parameter(typeof(int),"a");
        var b = Expression.Parameter(typeof(int),"b");
        var Constant1 = Expression.Constant(1);
        var b_plus_1 = Expression.Add(
            b,
            Constant1
        );
        Assert.AreEqual(
            5,
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { a,b },
                        Expression.Assign(a,Constant1),
                        Expression.Assign(b,Constant1),
                        Expression.Assign(//a=a+b+1 a:3
                            a,
                            Expression.Add(a,b_plus_1)
                        ),
                        Expression.Assign(//b=a+b+1 b:5
                            b,
                            Expression.Add(a,b_plus_1)
                        ),
                        b
                    )
                )
            )
        );
    }
    [TestMethod]
    public void 並び替え15() {
        var Break0 = Expression.Label(typeof(int),"Break0");
        var Break1 = Expression.Label(typeof(int),"Break1");
        var Continue0 = Expression.Label("Continue0");
        var Continue1 = Expression.Label("Continue1");
        var カウンタ0 = Expression.Parameter(typeof(int),"カウンタ0");
        var カウンタ1 = Expression.Parameter(typeof(int),"カウンタ1");
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { カウンタ0,カウンタ1 },
                    Expression.Assign(
                        カウンタ0,
                        Expression.Constant(0)
                    ),
                    Expression.Loop(
                        Expression.IfThenElse(
                            Expression.LessThan(
                                Expression.PostIncrementAssign(カウンタ0),
                                //MessageExpression(
                                //    "カウンタ0",
                                //    Expression.PostIncrementAssign(カウンタ0)
                                //),
                                Expression.Constant(10)
                            ),
                            Expression.Goto(Continue0),
                            Expression.Goto(Break0,Expression.Constant(1))
                        ),
                        Break0,
                        Continue0
                    ),
                    Expression.Assign(
                        カウンタ1,
                        Expression.Constant(0)
                    ),
                    Expression.Loop(
                        Expression.IfThenElse(
                            Expression.LessThan(
                                Expression.PostIncrementAssign(カウンタ1),
                                Expression.Constant(10)
                            ),
                            Expression.Goto(Continue1),
                            Expression.Goto(Break1,Expression.Constant(1))
                        ),
                        Break1,
                        Continue1
                    )
                )
            )
        );
    }
    [TestMethod]
    public void 並び替え16() {
        var Break0 = Expression.Label(typeof(int),"Break0");
        var Break1 = Expression.Label(typeof(int),"Break1");
        var Continue0 = Expression.Label("Continue0");
        var Continue1 = Expression.Label("Continue1");
        var カウンタ0 = Expression.Parameter(typeof(int),"カウンタ0");
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { カウンタ0 },
                    Expression.Assign(
                        カウンタ0,
                        Expression.Constant(0)
                    ),
                    Expression.Loop(
                        Expression.IfThenElse(
                            Expression.LessThanOrEqual(
                                Expression.PreIncrementAssign(カウンタ0),
                                Expression.Constant(10)
                            ),
                            Expression.Goto(Continue0),
                            Expression.Goto(Break0,Expression.Constant(1))
                        ),
                        Break0,
                        Continue0
                    ),
                    Expression.Assign(
                        カウンタ0,
                        Expression.Constant(0)
                    ),
                    Expression.Loop(
                        Expression.IfThenElse(
                            Expression.LessThanOrEqual(
                                Expression.PreIncrementAssign(カウンタ0),
                                Expression.Constant(10)
                            ),
                            Expression.Goto(Continue1),
                            Expression.Goto(Break1,Expression.Constant(1))
                        ),
                        Break1,
                        Continue1
                    )
                )
            )
        );
    }
    [TestMethod]
    public void 並び替え17() {
        var Break0 = Expression.Label(typeof(int),"Break0");
        var Break1 = Expression.Label(typeof(int),"Break1");
        var Continue0 = Expression.Label("Continue0");
        var Continue1 = Expression.Label("Continue1");
        var カウンタ0 = Expression.Parameter(typeof(int),"カウンタ0");
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { カウンタ0 },
                    Expression.Assign(
                        カウンタ0,
                        Expression.Constant(0)
                    ),
                    Expression.Loop(
                        Expression.IfThenElse(
                            Expression.GreaterThan(
                                Expression.PostDecrementAssign(カウンタ0),
                                Expression.Constant(10)
                            ),
                            Expression.Goto(Continue0),
                            Expression.Goto(Break0,Expression.Constant(1))
                        ),
                        Break0,
                        Continue0
                    ),
                    Expression.Assign(
                        カウンタ0,
                        Expression.Constant(0)
                    ),
                    Expression.Loop(
                        Expression.IfThenElse(
                            Expression.GreaterThan(
                                Expression.PostDecrementAssign(カウンタ0),
                                Expression.Constant(10)
                            ),
                            Expression.Goto(Continue1),
                            Expression.Goto(Break1,Expression.Constant(1))
                        ),
                        Break1,
                        Continue1
                    )
                )
            )
        );
    }
    [TestMethod]
    public void 並び替え18() {
        var Break0 = Expression.Label(typeof(int),"Break0");
        var Break1 = Expression.Label(typeof(int),"Break1");
        var Continue0 = Expression.Label("Continue0");
        var Continue1 = Expression.Label("Continue1");
        var カウンタ0 = Expression.Parameter(typeof(int),"カウンタ0");
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { カウンタ0 },
                    Expression.Assign(
                        カウンタ0,
                        Expression.Constant(0)
                    ),
                    Expression.Loop(
                        Expression.IfThenElse(
                            Expression.GreaterThanOrEqual(
                                Expression.PreDecrementAssign(カウンタ0),
                                Expression.Constant(-10)
                            ),
                            Expression.Goto(Continue0),
                            Expression.Goto(Break0,Expression.Constant(1))
                        ),
                        Break0,
                        Continue0
                    ),
                    Expression.Assign(
                        カウンタ0,
                        Expression.Constant(0)
                    ),
                    Expression.Loop(
                        Expression.IfThenElse(
                            Expression.GreaterThanOrEqual(
                                Expression.PreDecrementAssign(カウンタ0),
                                Expression.Constant(-10)
                            ),
                            Expression.Goto(Continue1),
                            Expression.Goto(Break1,Expression.Constant(1))
                        ),
                        Break1,
                        Continue1
                    )
                )
            )
        );
    }
    [TestMethod]
    public void 並び替え19() {
        var a = Expression.Parameter(typeof(decimal),"a");
        var b = Expression.Parameter(typeof(decimal),"b");
        //パラ_a=1m
        //パラ_b=パラ_a*パラ_a
        this.Execute2(
            Expression.Lambda<Func<decimal>>(
                Expression.Block(
                    new[] { a,b },
                    Expression.Assign(
                        a,
                        Expression.Constant(2m)
                    ),
                    Expression.Assign(
                        b,
                        Expression.Multiply(
                            a,
                            a
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void 並び替え20() {
        var カウンタ0 = Expression.Parameter(typeof(int),"カウンタ0");
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { カウンタ0 },
                    Expression.PostIncrementAssign(カウンタ0),
                    Expression.Assign(
                        カウンタ0,
                        Expression.Constant(1)
                    )
                )
            )
        );
    }
    [TestMethod]
    public void 並び替え21() {
        var Input0 = Expression.Parameter(typeof(bool),"Input0");
        var Input1 = Expression.Parameter(typeof(int),"Input1");
        var Break = Expression.Label(typeof(int),"Break");
        var Continue = Expression.Label("Continue");
        this.Execute2(
            Expression.Lambda<Func<bool,int,int>>(
                Expression.Loop(
                    Expression.IfThenElse(
                        Input0,
                        Expression.Goto(Continue),
                        Expression.Goto(Break,Expression.Constant(1))
                    ),
                    Break,
                    Continue
                ),Input0,Input1
            ),false,1
        );
    }
}
