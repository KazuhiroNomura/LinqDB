using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq.Expressions;
using System.Reflection;
using LinqDB.Sets;
using static LinqDB.Sets.ExtensionSet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable ImplicitlyCapturedClosure
// ReSharper disable PossibleUnintendedReferenceComparison
// ReSharper disable ArrangeRedundantParentheses
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable ConditionalTernaryEqualBranch
// ReSharper disable ConvertToConstant.Local
// ReSharper disable StaticMemberInGenericType
namespace CoverageCS.LinqDB;

[TestClass]
public class Optimizerによる式変換:ATest{
    [TestMethod]
    public void Conditional0(){
        {
            var x=(a:true,b:new 評価検証(10));
            this.実行結果が一致するか確認(
                Expression.Lambda<Func<評価検証>>(
                    Expression.Condition(
                        Expression.Field(
                            Expression.Constant(x),
                            nameof(ValueTuple<int,int>.Item1)
                        ),
                        Expression.Field(
                            Expression.Constant(x),
                            nameof(ValueTuple<int,int>.Item2)
                        ),
                        Expression.Field(
                            Expression.Constant(x),
                            nameof(ValueTuple<int,int>.Item2)
                        )
                    )
                )
            );
        }
        {
            var a=true;
            var b=new 評価検証(10);
            this.実行結果が一致するか確認(()=>
                a?b:b
            );
        }
    }
    [TestMethod]
    public void Conditional1(){
        var a=true;
        var b=true;
        var c=new 評価検証(10);
        this.実行結果が一致するか確認(()=>
            a?b?c
            :c
            :b?c+c
            :c+c
        );
        this.実行結果が一致するか確認(()=>
            a?b?c
            :c
            :b?c
            :c
        );
        this.実行結果が一致するか確認(()=>
            a?b?c
            :c
            :b?c
            :c+c
        );
        this.実行結果が一致するか確認(()=>
            a?b?c
            :c
            :b?c+c
            :c
        );
        this.実行結果が一致するか確認(()=>
            a?b?c
            :c+c
            :b?c
            :c
        );
        this.実行結果が一致するか確認(()=>
            a?b?c
            :c+c
            :b?c
            :c+c
        );
        this.実行結果が一致するか確認(()=>
            a?b?c
            :c+c
            :b?c+c
            :c
        );
        this.実行結果が一致するか確認(()=>
            a?b?c
            :c+c
            :b?c+c
            :c+c
        );
        this.実行結果が一致するか確認(()=>
            a?b?c+c
            :c
            :b?c
            :c
        );
        this.実行結果が一致するか確認(()=>
            a?b?c+c
            :c
            :b?c
            :c+c
        );
        this.実行結果が一致するか確認(()=>
            a?b?c+c
            :c
            :b?c+c
            :c
        );
        this.実行結果が一致するか確認(()=>
            a?b?c+c
            :c
            :b?c+c
            :c+c
        );
        this.実行結果が一致するか確認(()=>
            a?b?c+c
            :c+c
            :b?c
            :c
        );
        this.実行結果が一致するか確認(()=>
            a?b?c+c
            :c+c
            :b?c
            :c+c
        );
        this.実行結果が一致するか確認(()=>
            a?b?c+c
            :c+c
            :b?c+c
            :c
        );
        this.実行結果が一致するか確認(()=>
            a?b?c+c
            :c+c
            :b?c+c
            :c+c
        );
    }
    [TestMethod]
    public void Conditional2(){
        var a=true;
        var b=true;
        var c=new 評価検証(10);
        this.実行結果が一致するか確認(()=>
            a
                ?b
                    ?c
                    :c
                :b
                    ?c
                    :c
        );
        this.実行結果が一致するか確認(()=>
            a
                ?b
                    ?c
                    :c
                :b&b
                    ?c
                    :c
        );
        this.実行結果が一致するか確認(()=>
            a
                ?b&b
                    ?c
                    :c
                :b
                    ?c
                    :c
        );
        this.実行結果が一致するか確認(()=>
            a
                ?b&b
                    ?c
                    :c
                :b&b
                    ?c
                    :c
        );
        this.実行結果が一致するか確認(()=>
            b&b
                ?b
                    ?c
                    :c
                :b
                    ?c
                    :c
        );
        this.実行結果が一致するか確認(()=>
            b&b
                ?b
                    ?c
                    :c
                :b&b
                    ?c
                    :c
        );
        this.実行結果が一致するか確認(()=>
            b&b
                ?b&b
                    ?c
                    :c
                :b
                    ?c
                    :c
        );
        this.実行結果が一致するか確認(()=>
            b&b
                ?b&b
                    ?c
                    :c
                :b&b
                    ?c
                    :c
        );
    }
    [TestMethod]
    public void Conditional3(){
        var a=true;
        this.実行結果が一致するか確認(()=>
            a
                ?a
                :a
        );
        this.実行結果が一致するか確認(()=>
            a
                ?a
                :a&a
        );
        this.実行結果が一致するか確認(()=>
            a
                ?a&a
                :a
        );
        this.実行結果が一致するか確認(()=>
            a
                ?a&a
                :a&a
        );
        this.実行結果が一致するか確認(()=>
            a&a
                ?a
                :a
        );
        this.実行結果が一致するか確認(()=>
            a&a
                ?a
                :a&a
        );
        this.実行結果が一致するか確認(()=>
            a&a
                ?a&a
                :a
        );
        this.実行結果が一致するか確認(()=>
            a&a
                ?a&a
                :a&a
        );
    }
    [TestMethod]public void Conditional4ラムダ局所0(){
        {
            var a=3;
            //v.Let(a=>(a+a)+(a+a)==(a+a)+(a+a)
            //↓
            //v.Let(a=>t1==(t1=(t0=a+a)+t0)
            this.実行結果が一致するか確認(
                ()=>(a+a)+(a+a)==(a+a)+(a+a)
            );
        }
    }
    [TestMethod]public void Conditional4ラムダ局所1(){
        {
            //v.Let(a=>
            //    (a+a)+(a+a)==a
            //    ?(a+a)+(a+a)
            //    :(a+a)+(a+a)+a
            //↓
            //v.Let(a=>
            //    (t1=(t0=a+a)+t0)==a
            //    ?t1
            //    :t1+a←ここを評価
            this.共通Assert(
                3,
                v=>
                    v.Let(
                        a=>
                            (a+a)+(a+a)==a
                                ?(a+a)+(a+a)
                                :(a+a)+(a+a)+a//ここを評価するので4
                    )
            );
        }
    }
    [TestMethod]public void Conditional4ラムダ局21(){
        {
            //v.Let(a=>
            //    (a+a)+(a+a)==a
            //    ?(a+a)+(a+a)+a
            //    :(a+a)+(a+a)
            //↓
            //v.Let(a=>
            //    (t1=(t0=a+a)+t0)==a
            //    ?t1+a
            //    :t1←ここを評価
            this.共通Assert(
                2,
                v=>
                    v.Let(
                        a=>
                            (a+a)+(a+a)==a
                                ?(a+a)+(a+a)+a
                                :(a+a)+(a+a)
                    )
            );
        }
    }
    [TestMethod]
    public void Blockネスト(){
        var a=new 評価検証(10);
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<評価検証>>(
                Expression.Block(
                    Expression.Add(
                        Expression.Constant(a),
                        Expression.Block(
                            Expression.Constant(a)
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void Loopネスト0(){
        var a=new 評価検証(10);
        var Break0=Expression.Label(typeof(評価検証));
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<評価検証>>(
                Expression.Loop(
                    Expression.Break(
                        Break0,
                        Expression.Add(
                            Expression.Constant(a),
                            Expression.Constant(a)
                        )
                    ),
                    Break0
                )
            )
        );
    }
    [TestMethod]
    public void Loopネスト1(){
        var a=new 評価検証(10);
        var Break0=Expression.Label(typeof(評価検証));
        var Break1=Expression.Label(typeof(評価検証));
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<評価検証>>(
                Expression.Loop(
                    Expression.Break(
                        Break0,
                        Expression.Add(
                            Expression.Constant(a),
                            Expression.Loop(
                                Expression.Break(
                                    Break1,
                                    Expression.Add(
                                        Expression.Constant(a),
                                        Expression.Constant(a)
                                    )
                                ),
                                Break1
                            )
                        )
                    ),
                    Break0
                )
            )
        );
    }
    [TestMethod]
    public void ConditionalTrue1度False2度Expressionが出現(){
        {
            var x=(a:true,b:new 評価検証(10));
            this.実行結果が一致するか確認(
                Expression.Lambda<Func<評価検証>>(
                    Expression.Condition(
                        Expression.Field(
                            Expression.Constant(x),
                            "Item1"
                        ),
                        Expression.Field(
                            Expression.Constant(x),
                            "Item2"
                        ),
                        Expression.Add(
                            Expression.Field(
                                Expression.Constant(x),
                                "Item2"
                            ),
                            Expression.Field(
                                Expression.Constant(x),
                                "Item2"
                            )
                        )
                    )
                )
            );
        }
        {
            var a=true;
            var b=new 評価検証(10);
            this.実行結果が一致するか確認(()=>
                a?b:b+b
            );
        }
    }
    [TestMethod]
    public void ConditionalTrue2度False1度Expressionが出現(){
        {
            var x=(a:true,b:new 評価検証(10));
            this.実行結果が一致するか確認(
                Expression.Lambda<Func<評価検証>>(
                    Expression.Condition(
                        Expression.Field(
                            Expression.Constant(x),
                            "Item1"
                        ),
                        Expression.Add(
                            Expression.Field(
                                Expression.Constant(x),
                                "Item2"
                            ),
                            Expression.Field(
                                Expression.Constant(x),
                                "Item2"
                            )
                        ),
                        Expression.Field(
                            Expression.Constant(x),
                            "Item2"
                        )
                    )
                )
            );
        }
        {
            var a=true;
            var b=new 評価検証(10);
            this.実行結果が一致するか確認(()=>
                a?b+b:b
            );
        }
    }
    [TestMethod]
    public void ConditionalTrue2度False2度Expressionが出現(){
        {
            var x=(a:true,b:new 評価検証(10));
            this.実行結果が一致するか確認(
                Expression.Lambda<Func<評価検証>>(
                    Expression.Condition(
                        Expression.Field(
                            Expression.Constant(x),
                            "Item1"
                        ),
                        Expression.Add(
                            Expression.Field(
                                Expression.Constant(x),
                                "Item2"
                            ),
                            Expression.Field(
                                Expression.Constant(x),
                                "Item2"
                            )
                        ),
                        Expression.Add(
                            Expression.Field(
                                Expression.Constant(x),
                                "Item2"
                            ),
                            Expression.Field(
                                Expression.Constant(x),
                                "Item2"
                            )
                        )
                    )
                )
            );
        }
        {
            var a=true;
            var b=new 評価検証(10);
            this.実行結果が一致するか確認(()=>
                a?b+b:b+b
            );
        }
    }
    [TestMethod]
    public void 共通部分式(){
        var t=1m;
        this.実行結果が一致するか確認(()=>(t+t)+(t+t));
    }
    [TestMethod]
    public void Blockの複数式の局所先行評価(){
        var Point=new Point(1,2);
        var Point1=Expression.Parameter(typeof(Point),"Point1");
        var Point2=Expression.Parameter(typeof(Point),"Point2");
        var a=Expression.Parameter(typeof(int),"a");
        var Cラムダ局所1=Expression.Parameter(typeof(int),"Cラムダ局所1");
        var Cラムダ局所0=Expression.Parameter(typeof(Point),"Cラムダ局所0");
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<Point>>(
                Expression.Block(
                    new[]{
                        Point1,Point2,a,Cラムダ局所1,Cラムダ局所0
                    },
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
    public void 等価式(){
        this.実行結果が一致するか確認(()=>(new[]{
            "A"
        } as IEnumerable<object>).Contains("A"));
        this.実行結果が一致するか確認(a=>ArrN<object>(a).Contains(2));
        this.実行結果が一致するか確認(a=>ArrN<int>(a).Contains(2));
        //if(a_Type.IsPrimitive) {
        this.実行結果が一致するか確認(a=>ArrN<int>(a).Where(p=>p==1).Where(p=>p==2));
        //} else if(IEquatableType.IsAssignableFrom(a_Type)) {
        this.実行結果が一致するか確認(a=>ArrN<decimal>(a).Where(p=>p==1).Where(p=>p==2));
        //} else {
        //    if(op_Equality!=null) {
        {
            var a=new Point(1,2);
            var b=new Point(2,3);
            this.実行結果が一致するか確認(()=>
                new Point[]{
                    new(1,1),new(1,2),new(1,3),new(2,1),new(2,2),new(2,3),new(3,1),new(3,2),new(3,3)
                }.Where(p=>p==a).Where(p=>p==b));
        }
        //    } else {
        {
            var b=new object();
            var c=new object();
            this.実行結果が一致するか確認(a=>ArrN<object>(a).Where(p=>p==b).Where(p=>p==c));
        }
        //    }
        //}
    }
    [TestMethod]
    public void ラムダ局所00(){
        this.共通Assert(
            1,
            a=>
                a+a
        );
    }
    [TestMethod]
    public void ラムダ局所01(){
        //ClientTest.BackendClient.評価検証 $Cラムダ局所0,
        //ClientTest.BackendClient.評価検証 $Cラムダ局所1) {
        //$Cラムダ局所1 = .Call .a
        //$Cラムダ局所0 = $Cラムダ局所1 + $Cラムダ局所1;
        //$Cラムダ局所0 + $Cラムダ局所0
        this.共通Assert(
            2,
            v=>
                (v+v)
                +
                (v+v)
        );
    }

    [TestMethod]
    public void ループ跨ぎ0(){
        this.共通Assert(1,v=>
            Inline(()=>
                Inline(()=>
                    v+v
                )
            )
        );
    }
    [TestMethod]
    public void ループ跨ぎ1インラインラムダごと共通部分式(){
        this.共通Assert(1,v=>
            Inline(()=>
                v
            )
            +
            Inline(()=>
                v
            )
        );
    }
    [TestMethod]
    public void ループ跨ぎ2(){
        this.共通Assert(1,v=>
            Inline(()=>
                Inline(()=>
                    v
                )
                +
                Inline(()=>
                    v
                )
            )
        );
    }
    [TestMethod]
    public void ループ跨ぎ3(){
        this.共通Assert(2,v=>
            Inline(()=>
                (v+v)
                +
                (v+v)
            )
        );
    }
    [TestMethod]
    public void ループ跨ぎ4(){
        this.共通Assert(2,v=>
            Inline(()=>
                Inline(()=>
                    v+v
                )
                +
                Inline(()=>
                    v+v
                )
            )
        );
    }
    [TestMethod]
    public void ループ跨ぎ5(){
        this.共通Assert(1,v=>
            Inline(()=>
                Inline(()=>
                    Inline(()=>
                        v
                    )
                )
                +
                Inline(()=>
                    Inline(()=>
                        v
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ループ跨ぎ6(){
        this.共通Assert(2,v=>
            Inline(()=>
                Inline(()=>
                    v
                    +
                    v
                )
                +
                Inline(()=>
                    v
                    +
                    v
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ00(){
        //$Cループ跨ぎ0=.v
        //$Cラムダ跨ぎ1a0=$Cループ跨ぎ0
        //$Cラムダ跨ぎ1a0+$Cループ跨ぎ0+Let(
        //    $Cループ跨ぎ0,
        //    $Pラムダ引数3b1 =>
        //        $Cラムダ跨ぎ1a0+$Pラムダ引数3b1
        //)
        this.共通Assert(3,v=>
            Inline(()=>
                Inline(()=>
                    v
                    +
                    v
                )
                +
                v.Let(b1=>
                    v
                    +
                    b1
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ01(){
        //$Cループ跨ぎ0=.v
        //.Block() {
        //    $Cラムダ跨ぎ1a0=$Cループ跨ぎ0
        //    Let(
        //        $Cループ跨ぎ0,
        //        $Pラムダ引数2b0 =>
        //            $Cラムダ跨ぎ1a0+ $Pラムダ引数2b0
        //    )+ $Cラムダ跨ぎ1a0+$Cループ跨ぎ0
        //}
        this.共通Assert(3,v=>
            Inline(()=>
                v.Let(b0=>
                    v
                    +
                    b0
                )
                +
                Inline(()=>
                    v
                    +
                    v
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ020(){
        //.v=v
        //v.Let(b0=>
        //    .v+b0
        this.共通Assert(1,v=>
            Inline(()=>
                v.Let(b0=>
                    v+b0
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ021(){
        //$Cループ跨ぎ0=.v
        //$Cラムダ跨ぎ1a0=$Cループ跨ぎ0
        //(
        //    $Cラムダ局所0=Let(
        //        $Cループ跨ぎ0,
        //        $Pラムダ引数2b0 =>
        //            $Cラムダ跨ぎ1a0+ $Pラムダ引数2b0
        //    )+$Cラムダ局所0
        //)
        this.共通Assert(2,v=>
            Inline(()=>
                v.Let(b0=>
                    v
                    +
                    b0
                )
                +
                v.Let(b1=>
                    v
                    +
                    b1
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ03(){
        //$Cラムダ跨ぎ0=($Cラムダ局所0=.v)Let(
        //    $Cラムダ局所0,
        //    $Pラムダ引数1a0=>
        //        $Cラムダ跨ぎ1=$Pラムダ引数1a0;
        //        $Cラムダ跨ぎ1+($Cラムダ局所1=$Cラムダ跨ぎ0)+Let(
        //            $Cラムダ局所1,
        //            $Pラムダ引数3b1 =>
        //                $Cラムダ跨ぎ1+ $Pラムダ引数3b1
        //        )
        //)
        this.共通Assert(3,v=>
            v.Let(a0=>
                Inline(()=>
                    v
                    +
                    a0
                )
                +
                v.Let(b1=>
                    v
                    +
                    b1
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ040(){
        //Let(
        //    $Pラムダ引数0v,
        //    $Pラムダ引数1a0=>
        //        ($Cラムダ局所0) {
        //            {
        //                $Cラムダ跨ぎ1= $Pラムダ引数1a0;
        //                Let(
        //                    $Cラムダ局所0= $Cラムダ跨ぎ0,
        //                    $Pラムダ引数2b0=>$Cラムダ跨ぎ1+$Pラムダ引数2b0
        //                )
        //            }
        //}
        this.共通Assert(1,v=>
            v.Let(a0=>
                v.Let(b0=>
                    a0
                    +
                    b0
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ041(){
        //.v=v
        //v.Let(a0=>
        //    .a0=a0
        //    .v.Let(b0=>
        //        .a0+b0
        //    )+a0+.v
        this.共通Assert(3,v=>
            v.Let(a0=>
                v.Let(b0=>
                    a0+b0
                )+Inline(()=>
                    a0+v
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ042(){
        //.v=v
        //v.Let(a0=>
        //    .a0=a0
        //    .v.Let(b0=>
        //        .a0+b0
        //    )+Inline(()=>
        //        .a0+.v
        //    )
        this.共通Assert(3,v=>
            v.Let(a0=>
                v.Let(b0=>
                    a0+b0
                )+Inline(()=>
                    a0+v
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ050(){
        this.共通Assert(0,v=>
            v.Let(a0=>
                v.Let(b0=>
                    a0
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ051(){
        //$Cラムダ跨ぎ0=($Cラムダ局所0=.v)
        //Let(
        //    $Cラムダ局所0,
        //    $Pラムダ引数1a0=>
        //        $Cラムダ跨ぎ1a0=$Pラムダ引数1a0;
        //        (
        //            $Cラムダ局所1=Let(
        //                $Cラムダ跨ぎ0,
        //                $Pラムダ引数2b0 =>$Cラムダ跨ぎ1a0+ $Pラムダ引数2b0
        //            )
        //        )+$Cラムダ局所1
        //)
        this.共通Assert(2,v=>
            v.Let(a0=>
                v.Let(b0=>
                    a0
                    +
                    b0
                )
                +
                v.Let(b1=>
                    a0
                    +
                    b1
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ06(){
        //$Cループ跨ぎ0=.v
        //{
        //    $Cラムダ跨ぎ1a0=$Cループ跨ぎ0
        //    Let(
        //        $Cループ跨ぎ0,
        //        $Pラムダ引数2b0 =>$Cラムダ跨ぎ1a0+$Pラムダ引数2b0
        //    )
        //}+$Cループ跨ぎ0+$Cループ跨ぎ0
        this.共通Assert(3,v=>
            Inline(()=>
                v.Let(b0=>
                    v
                    +
                    b0
                )
            )
            +
            Inline(()=>
                Inline(()=>
                    v
                    +
                    v
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ10(){
        //(
        //    Cラムダ局所0=Inline(
        //        (Cラムダ跨ぎ0=v),
        //        a=>{
        //            Cラムダ跨ぎ1=a
        //            Inline(
        //                Cラムダ跨ぎ0,
        //                b=>Cラムダ跨ぎ1+b
        //            )
        //        )
        //    )
        //)+Cラムダ局所0
        this.共通Assert(2,v=>
            Inline(()=>
                Inline(()=>
                    v
                    +
                    v
                )
            )
            +
            Inline(()=>
                Inline(()=>
                    v
                    +
                    v
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ11(){
        //$Cループ跨ぎ0=.v
        //$Cループ跨ぎ0+$Cループ跨ぎ0+{
        //    $Cラムダ跨ぎ3a1=$Cループ跨ぎ0
        //    Let(
        //        $Cループ跨ぎ0,
        //        $Pラムダ引数4b0=>$Cラムダ跨ぎ3a1 + $Pラムダ引数4b0
        //    )
        //}
        this.共通Assert(3,v=>
            Inline(()=>
                Inline(()=>
                    v
                    +
                    v
                )
            )
            +
            Inline(()=>
                v.Let(b0=>
                    v
                    +
                    b0
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ13(){
        //v=>
        //    Block(CoverageCS.Lite.評価検証 $Cラムダ局所0) {
        //        Block(
        //            CoverageCS.Lite.評価検証 $Cラムダ跨ぎ0,
        //            CoverageCS.Lite.評価検証 $Cラムダ跨ぎ1,
        //            CoverageCS.Lite.評価検証 $Cラムダ跨ぎ2) {
        //            $Cラムダ跨ぎ0 = v;
        //            (
        //                $Cラムダ局所0 = .Call Lite.Sets.ExtendSetInline(
        //                    v,
        //                    a=>{
        //                        $Cラムダ跨ぎ1 = a
        //                        Let(
        //                            $Cラムダ跨ぎ0,
        //                            b=>$Cラムダ跨ぎ1 + $Pラムダ引数2b0
        //                    }
        //                ) 
        //            )+ $Cラムダ局所0
        //        }
        //    }
        //}



        this.共通Assert(2,v=>
            Inline(()=>
                v.Let(b0=>
                    v
                    +
                    b0
                )
            )
            +
            Inline(()=>
                v.Let(b0=>
                    v
                    +
                    b0
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ15(){
        //$Cループ跨ぎ0=($Cラムダ局所0=Constant).v
        //$Cラムダ跨ぎ1=$Cラムダ局所0.v
        //$Cループ跨ぎ0+$Cループ跨ぎ0+Let(
        //    $Cループ跨ぎ0,
        //    $Pラムダ引数3a1 =>
        //        $Pラムダ引数3a1+$Cラムダ跨ぎ1
        //)
        this.共通Assert(3,v=>
            Inline(()=>
                Inline(()=>
                    v
                    +
                    v
                )
            )
            +
            v.Let(a1=>
                Inline(()=>
                    a1
                    +
                    v
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ19(){
        //v=>
        //    .v=v
        //    ($1=v.Let(a0=>
        //        $0=a0+.v
        //        Inline(()=>
        //            $0
        //        )
        //    )
        //    +
        //    $1
        this.共通Assert(2,v=>
            v.Let(a0=>
                Inline(()=>
                    a0+v
                )
            )
            +
            v.Let(a1=>
                Inline(()=>
                    a1+v
                )
            )
        );
    }
    private static decimal DecimalLambda(Func<decimal> e)=>e();
    [TestMethod]
    public void ラムダループ跨ぎ20(){
        var p=Expression.Parameter(typeof(decimal),"p");
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<decimal>>(
                Expression.Block(
                    new[]{
                        p
                    },
                    Expression.Assign(
                        p,
                        Expression.Constant(2m)
                    ),
                    Expression.AddAssign(
                        p,
                        Expression.Invoke(
                            Expression.Lambda<Func<decimal>>(
                                Expression.Add(
                                    Expression.Constant(1m),
                                    Expression.Constant(1m)
                                )
                            )
                        )
                    )
                )
            )
        );
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<decimal>>(
                Expression.Invoke(
                    Expression.Lambda<Func<decimal>>(
                        Expression.Add(
                            Expression.Constant(1m),
                            Expression.Constant(1m)
                        )
                    )
                )
            )
        );
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<decimal>>(
                Expression.Call(
                    typeof(Optimizerによる式変換).GetMethod(nameof(DecimalLambda),BindingFlags.Static|BindingFlags.NonPublic),
                    Expression.Lambda<Func<decimal>>(
                        Expression.Add(
                            Expression.Constant(1m),
                            Expression.Constant(1m)
                        )
                    )
                )
            )
        );
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<decimal>>(
                Expression.Block(
                    new[]{
                        p
                    },
                    Expression.Assign(
                        p,
                        Expression.Constant(2m)
                    ),
                    Expression.AddAssign(
                        p,
                        Expression.Call(
                            typeof(Optimizerによる式変換).GetMethod(nameof(DecimalLambda),BindingFlags.Static|BindingFlags.NonPublic),
                            Expression.Lambda<Func<decimal>>(
                                Expression.Add(
                                    Expression.Constant(1m),
                                    Expression.Constant(1m)
                                )
                            )
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数00(){
        this.共通Assert(0,v=>
            v.Let(a0=>
                v.Let(b0=>
                    a0
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数01(){
        this.共通Assert(1,v=>
            v.Let(a0=>
                a0
            )
            +
            v.Let(a1=>
                a1
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数02(){
        this.共通Assert(1,v=>
            v.Let(a0=>
                v.Let(b0=>
                    a0+a0
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数03(){
        this.共通Assert(1,v=>
            v.Let(a0=>
                v.Let(b0=>
                    a0
                )
                +
                v.Let(b1=>
                    a0
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数04(){
        this.共通Assert(2,v=>
            v.Let(a0=>
                v.Let(b0=>
                    a0+a0
                )
                +
                v.Let(b1=>
                    a0+a0
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数05(){
        this.共通Assert(1,v=>
            v.Let(a0=>
                v.Let(b0=>
                    v.Let(c0=>
                        a0
                    )
                )
                +
                v.Let(b1=>
                    v.Let(c2=>
                        a0
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数06(){
        this.共通Assert(2,v=>
            v.Let(a0=>
                v.Let(b0=>
                    a0+b0
                )
                +
                v.Let(b1=>
                    a0+b1
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数07(){
        //v=>{
        //    .v=v
        //    (t2=v.Let(a0 =>
        //        .a0=a0
        //        .v.Let(b0 =>
        //            b0
        //1           +
        //            .a0
        //        ))
        //    ))
        //2   +
        //    t2
        //}
        this.共通Assert(2,v=>
            v.Let(a0=>
                v.Let(b0=>
                    a0+b0
                )
            )
            +
            v.Let(a1=>
                v.Let(b0=>
                    a1+b0
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数08(){
        //v=>{
        //    .v=v
        //    (t2=v.Let(a0 =>
        //        .a0=a0
        //        (t1=.v.Let(b0 =>
        //            .a0+b0
        //        +t1
        //    ))+t2
        this.共通Assert(3,v=>
            v.Let(a0=>
                v.Let(b0=>
                    a0+b0
                )+v.Let(b1=>
                    a0+b1
                )
            )+v.Let(a1=>
                v.Let(b0=>
                    a1+b0
                )+v.Let(b1=>
                    a1+b1
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数09(){
        //v=>
        //    .v=v
        //    (t2=v.Let(a0=>
        //        .a0=a0
        //        (t1=.v.Let(b0=>
        //            (t0=.v.Let(c0=>
        //                c0+.a0
        //            ))+t
        //        ))+t1
        //    ))+t2
        this.共通Assert(4,v=>
            v.Let(a0=>
                v.Let(b0=>
                    v.Let(c0=>
                        c0+a0
                    )+v.Let(c1=>
                        c1+a0
                    )
                )+v.Let(b1=>
                    v.Let(c2=>
                        c2+a0
                    )+v.Let(c3=>
                        c3+a0
                    )
                )
            )+v.Let(a1=>
                v.Let(b2=>
                    v.Let(c4=>
                        c4+a1
                    )+v.Let(c5=>
                        c5+a1
                    )
                )+v.Let(b3=>
                    v.Let(c6=>
                        c6+a1
                    )+v.Let(c7=>
                        c7+a1
                    )
                )
            )
        );
    }
    [TestMethod]
    public void InlineInline(){
        //v=>
        //    ループ跨$0=v
        //    ループ跨$0
        this.共通Assert(0,v=>
            Inline(()=>
                Inline(()=>
                    v
                )
            )
        );
    }
    [TestMethod]
    public void LetInline(){
        //v =>
        //    .t=Inline(()=>
        //       b
        //    )
        //    v.Let(a =>
        //        .t
        //    )
        this.共通Assert(0,v=>
            v.Let(a=>
                Inline(()=>
                    a
                )
            )
        );
    }
    [TestMethod]
    public void InlineLet(){
        //v =>
        //    ループ跨$0=v.Let(b=>
        //        b
        //    )
        //    ループ跨$0
        this.共通Assert(0,v=>
            Inline(()=>
                v.Let(b=>
                    b
                )
            )
        );
    }
    [TestMethod]
    public void LetLet(){
        //v =>
        //    ラムダ跨$0=v.Let(b=>
        //        b
        //    )
        //    v.Let(a=>
        //        ラムダ跨$0
        //    )
        this.共通Assert(0,v=>
            v.Let(a=>
                v.Let(b=>
                    b
                )
            )
        );
    }
    [TestMethod]
    public void InlineInlineループ跨(){
        //v=>
        //    .v=v
        //    Inline(()=>
        //        .Inline(()=>
        //            a
        //        )
        //    )
        this.共通Assert(0,v=>
            Inline(()=>
                Inline(()=>
                    v
                )
            )
        );
    }
    [TestMethod]
    public void LetInlineループ跨(){
        //v =>
        //    .v=v
        //    v.Let(a=>
        //        .Inline(()=>
        //            a
        //        )
        //    )
        this.共通Assert(0,v=>
            v.Let(a=>
                Inline(()=>
                    a
                )
            )
        );
    }
    [TestMethod]
    public void InlineLetラムダ跨(){
        //v =>
        //    Inline(()=>
        //        .a=a
        //        v.Let(b=>
        //            .a
        //        )
        //    )
        this.共通Assert(0,v=>
            Inline(()=>
                v.Let(b=>
                    v
                )
            )
        );
    }
    [TestMethod]
    public void Letラムダ跨(){
        //v =>
        //    .v=v
        //    v.Let(a=>
        //        .v
        //    )
        this.共通Assert(0,v=>
            v.Let(a=>v)
        );
    }
    [TestMethod]
    public void LetLetラムダ跨(){
        //v =>
        //    .v=v
        //    v.Let(a=>
        //        .a=a
        //        .v.Let(b=>
        //            .a
        //        )
        //    )
        this.共通Assert(0,v=>
            v.Let(a=>
                v.Let(b=>
                    a
                )
            )
        );
    }
    [TestMethod]
    public void InlineInlineループ跨評価回数(){
        //v=>
        //    ($ローカル$0=$v+$v)+$ローカル$0
        this.共通Assert(2,v=>
            Inline(()=>
                Inline(()=>
                    v+v
                )
                +
                Inline(()=>
                    v+v
                )
            )
        );
    }
    [TestMethod]public void Letラムダ0(){
        var v=Expression.Parameter(typeof(int),"v");
        var a=Expression.Parameter(typeof(int),"a");
        var Inline=typeof(ExtensionSet).GetMethods().Last(p=>p.Name=="Inline");
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<int,int>>(
                Expression.Call(
                    typeof(global::LinqDB.Sets.Helpers).GetMethod("Let")!.MakeGenericMethod(typeof(int),typeof(int)),
                    v,
                    Expression.Lambda<Func<int,int>>(
                        Expression.Call(
                            Inline.MakeGenericMethod(typeof(int)),
                            Expression.Lambda<Func<int>>(
                                Expression.Add(v,a)
                            )
                        ),
                        a
                    )
                ),
                v
            ),0
        );
    }
    [TestMethod]public void Letラムダ1(){
        var v=Expression.Parameter(typeof(評価検証),"v");
        var a=Expression.Parameter(typeof(評価検証),"a");
        var Inline=typeof(ExtensionSet).GetMethods().Last(p=>p.Name=="Inline");
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<評価検証,評価検証>>(
                Expression.Call(
                    typeof(global::LinqDB.Sets.Helpers).GetMethod("Let")!.MakeGenericMethod(typeof(評価検証),typeof(評価検証)),
                    v,
                    Expression.Lambda<Func<評価検証,評価検証>>(
                        Expression.Call(
                            Inline.MakeGenericMethod(typeof(評価検証)),
                            Expression.Lambda<Func<評価検証>>(
                                Expression.Add(v,a)
                            )
                        ),
                        a
                    )
                ),
                v
            ),new 評価検証(0)
        );
    }
    [TestMethod]public void Letラムダ30(){
        this.実行結果が一致するか確認(v=>
            v.Let(a=>
                v.Let(b=>
                    a+b
                )
            ),0
        );
    }
    [TestMethod]public void Letラムダ31(){
        this.実行結果が一致するか確認(v=>
            v.Let(a=>
                v.Inline(b=>
                    a+b
                )
            ),0
        );
    }
    [TestMethod]public void Letラムダ32(){
        var v=1;
        this.実行結果が一致するか確認(()=>
            v.Let(a=>
                Inline(()=>
                    v+a
                )
            )
        );
    }
    [TestMethod]public void Letラムダ33(){
        this.実行結果が一致するか確認(v=>
            v.Let(a=>
                Inline(()=>
                    v+a
                )
            ),0
        );
    }
    [TestMethod]public void Letラムダ4(){
        this.実行結果が一致するか確認(v=>
            v.Let(a=>
                Inline(()=>
                    v+a
                )
            ),new 評価検証(0)
        );
    }
    [TestMethod]public void LetInlineループ跨評価回数00(){
        //this.共通Assert(2,v=>
        //    v.Let(a=>
        //        v.Let(b=>
        //            v+a
        //        )
        //    )
        //);
        var v=new[]{1,2,3};
        this.実行結果が一致するか確認(a=>
            v.Let(b=>
                b.Select(c=>
                    new{b,a}
                )
            ),
            v
        );
    }
    [TestMethod]public void LetInlineループ跨評価回数01(){
        //Lambda(評価検証 v)
        //    └Block
        //    ├Assign
        //    │├Parameter ラムダv.0
        //    │└Parameter v
        //    └Call CoverageCS.LinqDB.評価検証 Let[評価検証,評価検証](CoverageCS.LinqDB.評価検証, System.Func`2[CoverageCS.LinqDB.評価検証,CoverageCS.LinqDB.評価検証])
        //    ├Parameter v
        //    └Lambda(評価検証 a)
        //    └Block(評価検証 ループ.1)
        //    └Block
        //    ├Assign
        //    │├Parameter ループ.1
        //    │└Add
        //    │　├Parameter ラムダv.0
        //    │　└Parameter a
        //    └Parameter ループ.1
        this.共通Assert(1,v=>
            v.Let(a=>
                Inline(()=>
                    v+a
                )
            )
        );
    }
    [TestMethod]public void LetInlineループ跨評価回数1(){
        //v =>
        //    .v=v
        //    v.Let(a=>
        //        a+.v
        //    )
        this.共通Assert(1,v=>
            v.Let(a=>
                Inline(()=>
                    Inline(()=>
                        v+a
                    )
                )
            )
        );
    }
    [TestMethod]public void LetInlineループ跨評価回数2(){
        //v =>
        //    .v=v
        //    v.Let(a=>
        //        ($ローカル$0=$a+$ラムダ跨$0$v)+$ローカル$0
        //    )
        this.共通Assert(2,v=>
            v.Let(a=>
                Inline(()=>
                    a+v
                )
                +
                Inline(()=>
                    a+v
                )
            )
        );
    }
    [TestMethod]
    public void InlineLetラムダ跨評価回数(){
        //v =>
        //    .v=v
        //    ($ローカル$0=v.Let(a=>
        //        .v+a
        //    )+$ローカル$0
        this.共通Assert(2,v=>
            Inline(()=>
                v.Let(b=>
                    v+b
                )
                +
                v.Let(b=>
                    v+b
                )
            )
        );
    }
    [TestMethod]
    public void LetLetラムダ跨評価回数(){
        //v =>
        //    .v=v
        //    v.Let(a=>
        //        .a=a
        //        .v.Let(b=>
        //            .a+.v
        //        )
        //    )
        this.共通Assert(1,v=>
            v.Let(a=>
                v.Let(b=>
                    a+v
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ評価回数02(){
        //v =>
        //    .v=v
        //    v.Let(a0 =>
        //1       a0+.v
        this.共通Assert(1,v=>
            v.Let(a0=>
                Inline(()=>
                    a0+v
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ評価回数03(){
        //v =>
        //    .v=v
        //    v.Let(a0 =>
        //2      (t=a0+.v)+t
        this.共通Assert(2,v=>
            v.Let(a0=>
                Inline(()=>
                    a0+v
                )+Inline(()=>
                    a0+v
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ評価回数04(){
        //v =>
        //    .v=v
        //    v.Let(a0 =>
        //        .a0=a0
        //1       a0+.v
        //2       +
        //        .v.Let(b0 =>
        //3          .a0+b0
        this.共通Assert(3,v=>
            v.Let(a0=>
                Inline(()=>
                    a0+v
                )+v.Let(b0=>
                    a0+b0
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ評価回数05(){
        //v =>
        //    .v=v
        //    v.Let(a0 =>
        //        .a0=a0
        //        .v.Let(b0 =>
        //1          .a0+b0
        //2       +
        //3       a0+.v
        this.共通Assert(3,v=>
            v.Let(a0=>
                v.Let(b0=>
                    a0+b0
                )+Inline(()=>
                    a0+v
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ評価回数06(){
        //$ループ.1 = ($局所1 = $v + $v);
        //$ラムダv.2 = $v;
        //    $ラムダ.3 = $局所1;
        //    $ループ.0 = $ループ.1 + .Call LinqDB.Sets.Helpers.Let(
        //    $v,
        //    .Lambda #Lambda2<System.Func`2[CoverageCS.LinqDB.評価検証,CoverageCS.LinqDB.評価検証]>);
        //$ループ.0
        this.共通Assert(2,v=>
            Inline(()=>
                Inline(()=>
                    v+v
                )+v.Let(b0=>
                    v+b0
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ評価回数09(){
        //$ループ.1 = $v + $v;
        //    $ラムダv.2 = $v;
        //    $ラムダ.3 = ($局所0 = $ラムダv.2) + $局所0;
        //    $ループ.0 = $ループ.1 + .Call LinqDB.Sets.Helpers.Let(
        //    $v,
        //    .Lambda #Lambda2<System.Func`2[CoverageCS.LinqDB.評価検証,CoverageCS.LinqDB.評価検証]>);
        //$ループ.0
        this.共通Assert(4,v=>
            v.Let(a0=>
                v.Let(b0=>
                    v.Let(c0=>
                        c0+a0
                    )+
                    Inline(()=>
                        a0+b0
                    )
                )
                +
                v.Let(b1=>
                    v.Let(c2=>
                        c2+a0
                    )
                    +
                    Inline(()=>
                        a0+b1
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ評価回数100(){
        this.共通Assert(11,v=>
            v.Let(a0=>
                v.Let(b0=>
                    v.Let(c0=>
                        c0+a0
                    )
                    +
                    Inline(()=>
                        b0+a0
                    )
                )
                +
                v.Let(b1=>
                    v.Let(c2=>
                        c2+a0
                    )
                    +
                    Inline(()=>
                        b1+a0
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ評価回数101(){
        //v =>
        //    .v=v
        //     (t1=v.Let(a0 =>
        //         .a0=a0
        //         (t0=.v.Let(b0 =>
        //             .v.Let(c0 =>
        //                 c0+.a0
        //             )+Inline(() =>
        //                 c1+.a0
        //             )
        //         ))+t0
        //     ))+t1
        this.共通Assert(11,v=>
            v.Let(a0=>
                v.Let(b0=>
                    v.Let(c0=>
                        c0+a0
                    )
                    +
                    Inline(()=>
                        b0+a0
                    )
                )
                +
                v.Let(b1=>
                    v.Let(c2=>
                        c2+a0
                    )
                    +
                    Inline(()=>
                        b1+a0
                    )
                )
            )
            +
            v.Let(a1=>
                v.Let(b2=>
                    v.Let(c4=>
                        c4+a1
                    )
                    +
                    Inline(()=>
                        b2+a1
                    )
                )
                +
                v.Let(b3=>
                    Inline(()=>
                        b3+a1
                    )
                    +
                    v.Let(c7=>
                        c7+a1
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数11(){
        //$Cラムダ跨ぎ0=.v;
        //($Cラムダ局所0=Let(
        //    $Cラムダ跨ぎ0,
        //	$Pラムダ引数1a0=>
        //        $Cラムダ跨ぎ1a0=$Pラムダ引数1a0;
        //        ($Cラムダ局所1=Let(
        //            $Cラムダ跨ぎ0,
        //            $Pラムダ引数2b0=>
        //                $Pラムダ引数2b0+$Cラムダ跨ぎ1a0+$Pラムダ引数2b0+$Cラムダ跨ぎ1a0
        //        ))+$Cラムダ局所1
        //))+$Cラムダ局所0
        this.共通Assert(4,v=>
            v.Let(a0=>
                v.Let(b0=>
                    Inline(()=>
                        b0
                        +
                        a0
                    )
                    +
                    Inline(()=>
                        b0
                        +
                        a0
                    )
                )
                +
                v.Let(b1=>
                    Inline(()=>
                        b1
                        +
                        a0
                    )
                    +
                    Inline(()=>
                        b1
                        +
                        a0
                    )
                )
            )
            +
            v.Let(a1=>
                v.Let(b2=>
                    Inline(()=>
                        b2
                        +
                        a1
                    )
                    +
                    Inline(()=>
                        b2
                        +
                        a1
                    )
                )
                +
                v.Let(b3=>
                    Inline(()=>
                        b3
                        +
                        a1
                    )
                    +
                    Inline(()=>
                        b3
                        +
                        a1
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ0(){
        this.実行結果が一致するか確認(v=>
            v.Let(a=>
                v.Let(b=>
                    Inline(()=>
                        b==a
                    )
                )
            )
        );
        this.実行結果が一致するか確認(a=>ArrN<int>(a).Where(p=>p==1));
        this.実行結果が一致するか確認(a=>ArrN<int>(a).Select(b=>a.Let(c=>a.Let(d=>c+b))));
        this.実行結果が一致するか確認(a=>a.Let(aa=>a.Let(aaa=>ArrN<int>(a).Where(aaaa=>aaaa==aa))));
    }
    [TestMethod]
    public void ラムダ跨ぎ1(){
        var data=new[]{
            1,2,3
        }.ToSet();
        var value=100m;
        var v=1m;
        this.実行結果が一致するか確認(()=>
            v.Let(x=>
                data.GroupBy(LINEITEM=>LINEITEM).Where(p=>p.Sum()>300m)
            )
        );
        this.実行結果が一致するか確認(()=>
            v.Let(x=>
                value
            )
        );
        this.実行結果が一致するか確認(()=>
            v.Let(x=>
                data.Select(p=>data.Sum(q=>q)>300m)
            )
        );
        this.実行結果が一致するか確認(()=>
            v.Let(x=>
                data.Sum(q=>q)>300m
            )
        );
        var g=data.GroupBy(LINEITEM=>LINEITEM);
        this.実行結果が一致するか確認(()=>
            v.Let(x=>
                g.Where(p=>p.Sum()>300m)
            )
        );
        this.実行結果が一致するか確認(()=>
            v.Let(x=>
                g.Where(p=>p.Sum()>300m)
            )
        );
        this.実行結果が一致するか確認(()=>
            from LINEITEM in data
            group LINEITEM by LINEITEM
            into p
            where p.Sum(q=>q)>300m
            select p.Key
        );
    }
}
public sealed class 参照評価回数{
    public int 評価回数;
    public 参照評価回数(int 評価回数){
        this.評価回数=評価回数;
    }
}
[DebuggerDisplay("{this.値}")]
public sealed class 評価検証:IEquatable<評価検証>{
    private 参照評価回数 参照評価回数;
    public int 評価回数=>this.参照評価回数.評価回数;
    public int 値;
    private 評価検証(参照評価回数 参照評価回数){
        this.参照評価回数=参照評価回数;
    }
    public 評価検証(){
        this.参照評価回数=new 参照評価回数(0);
    }
    public 評価検証(int 値){
        this.参照評価回数=new 参照評価回数(0);
        this.値=値;
    }
    [Pure]
    public static 評価検証 operator+(評価検証 a,評価検証 b){
        var 参照評価回数=a.参照評価回数;
        参照評価回数.評価回数++;
        b.参照評価回数=参照評価回数;
        var r=new 評価検証(参照評価回数);
        r.値=a.値+b.値;
        return r;
    }
    public bool Equals(評価検証 other){
        return other!=null&&this.参照評価回数.評価回数==other.参照評価回数.評価回数;
    }

    public override bool Equals(object obj)=>this.Equals((評価検証)obj);
    [SuppressMessage("ReSharper","NonReadonlyMemberInGetHashCode")]
    public override int GetHashCode()=>this.参照評価回数.評価回数;
}
public class OptimizerGeneric<T>:ATest where T:new(){
    private int Parameter番号;
    private int 自由変数数;
    private readonly List<ParameterExpression> ListParameter=new();
    private readonly Random r=new(1);
    private static MethodInfo M<U>(Expression<Func<U>> e)=>((MethodCallExpression)e.Body).Method.GetGenericMethodDefinition();
    //private static readonly MethodInfo InlineMethod0 = M(() => Inline(() => 1));
    private static readonly MethodInfo InlineMethod=M(()=>1.Inline(a=>a));
    private static readonly MethodInfo LetMethod=M(()=>1.Let(a=>a));
    private ParameterExpression Parameter(string Name)=>Expression.Parameter(typeof(T),Name);
    private static Expression 評価(Expression オペランド){
        var 評価=オペランド.Type.GetMethod("評価");
        if(評価==null) return オペランド;
        return Expression.Call(
            オペランド,
            評価
        );
    }
    private static Expression Constant評価=>Expression.Constant(new T());
    private Expression オペランド(){
        //v
        //Parameter
        //Add
        //Lambda,Inline
        //    Lambda
        //    Inline
        var value=
            this.ListParameter.Count==0?this.r.Next(3):
            this.ListParameter.Count<2?this.r.Next(4):
            3;
        switch(value){
            case 0:
                this.自由変数数++;
                return Constant評価;
            case 1:
                return Expression.Add(
                    this.オペランド(),
                    this.オペランド()
                );
            case 2:{
                this.自由変数数++;
                var p=this.Parameter(this.Parameter番号++.ToString());
                var オペランド0=this.オペランド();
                this.ListParameter.Add(p);
                var オペランド1=this.オペランド();
                this.ListParameter.RemoveAt(this.ListParameter.Count-1);
                return Expression.Call(
                    LetMethod.MakeGenericMethod(typeof(T),typeof(T)),
                    オペランド0,
                    Expression.Lambda(
                        オペランド1,
                        p
                    )
                );
            }
            case 3:
                return this.ListParameter[this.r.Next(this.ListParameter.Count)];
            default:
                throw new NotSupportedException();
        }
    }
    protected virtual void Assert(Expression e){
        this.実行結果が一致するか確認(Expression.Lambda<Func<T>>(e));
    }
    //[TestMethod]
    //public void 組み合わせ()
    //{
    //    for (var a = 0; a < 100; a++)
    //    {
    //        this.自由変数数 = 0;
    //        this.ListParameter.Clear();
    //        this.Parameter番号 = 0;
    //        Console.WriteLine(nameof(組み合わせ) + ":" + a);
    //        this.Assert(this.オペランド());
    //    }
    //}
    [TestMethod]
    public void 組み合わせ0(){
        var a1=new 評価検証(1);
        var a2=new 評価検証(2);
        var a3=new 評価検証(3);
        var a4=new 評価検証(4);
        var a5=new 評価検証(5);
        var a6=new 評価検証(6);
        this.実行結果が一致するか確認(
            ()=>(
                (
                    a1+(
                        Inline(
                            ()=>(
                                a3.Let(i1=>i1)
                                +
                                a4
                            )
                        )
                        +
                        Inline(()=>a5).Let(
                            i2=>(
                                i2.Let(i4=>i4)
                                +
                                Inline(()=>i2)
                            )
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ局所0(){
        var a=new T();
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                Expression.Add(
                    Expression.Constant(a),
                    Expression.Constant(a)
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ局所1(){
        var a=new T();
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                Expression.Add(
                    Expression.Add(
                        Expression.Constant(a),
                        Expression.Constant(a)
                    ),
                    Expression.Add(
                        Expression.Constant(a),
                        Expression.Constant(a)
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ局所2(){
        var v=1;
        this.実行結果が一致するか確認(()=>
            v.Let(a=>a)
        );
    }
    [TestMethod]
    public void ラムダ局所3() {
        var v = 1;
        this.実行結果が一致するか確認(() =>
            v.Let(a =>
                a*2+a*2==0
                    ? a*2+a*2
                    : a*2+a*2+2)
        );
    }
    private static Expression InlineExpression(ParameterExpression p,Expression e0,Expression e1){
        return Expression.Call(
            InlineMethod.MakeGenericMethod(e0.Type,e1.Type),
            e0,
            Expression.Lambda(
                e1,
                p
            )
        );
    }
    private static Expression LetExpression(ParameterExpression p,Expression e0,Expression e1){
        return Expression.Call(
            LetMethod.MakeGenericMethod(e0.Type,e1.Type),
            e0,
            Expression.Lambda(
                e1,
                p
            )
        );
    }
    [TestMethod]
    public void ループ跨ぎ0(){
        var p=this.Parameter("p");
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T,T>>(
                InlineExpression(p,v評価,v評価),
                p
            ),
            new T()
        );
    }
    [TestMethod]
    public void ループ跨ぎ1インラインラムダごと共通部分式(){
        var a=this.Parameter("a");
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T,T>>(
                Expression.Add(
                    InlineExpression(a,Constant評価,a),
                    InlineExpression(a,Constant評価,a)
                ),
                a
            ),
            new T()
        );
    }
    [TestMethod]
    public void ループ跨ぎ2(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var v評価=Constant評価;
        var b評価=評価(b);
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T,T>>(
                InlineExpression(
                    a,
                    v評価,
                    Expression.Add(
                        InlineExpression(b,v評価,b評価),
                        InlineExpression(b,v評価,b評価)
                    )
                ),
                a
            ),
            new T()
        );
    }
    [TestMethod]
    public void ループ跨ぎ3(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var c=this.Parameter("c");
        var v評価=Constant評価;
        var b評価=評価(b);
        var c評価=評価(c);
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T,T>>(
                InlineExpression(
                    a,
                    v評価,
                    Expression.Add(
                        Expression.Add(
                            InlineExpression(b,v評価,b評価),
                            InlineExpression(b,v評価,b評価)
                        ),
                        Expression.Add(
                            InlineExpression(c,v評価,c評価),
                            InlineExpression(c,v評価,c評価)
                        )
                    )
                ),
                a
            ),
            new T()
        );
    }
    [TestMethod]
    public void ループ跨ぎ4(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var c=this.Parameter("c");
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T,T>>(
                InlineExpression(
                    b,
                    v評価,
                    Expression.Add(
                        Expression.Add(
                            InlineExpression(b,a,a),
                            InlineExpression(b,a,b)
                        ),
                        Expression.Add(
                            InlineExpression(c,b,a),
                            InlineExpression(c,b,b)
                        )
                    )
                ),
                a
            ),
            new T()
        );
    }
    [TestMethod]
    public void ループ跨ぎ6(){
        var d0=new int[]{
            1,2,3
        };
        var d1=new int[]{
            3,4,5
        };
        this.実行結果が一致するか確認(
            ()=>
                d0.Join(d1,o=>o,i=>i,(o,i)=>new{
                    o,i
                })
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ0(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var a評価=評価(a);
        var b評価=評価(b);
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                InlineExpression(
                    a,
                    v評価,
                    Expression.Add(
                        InlineExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                a評価
                            )
                        ),
                        LetExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ループ跨ぎ1(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var a評価=評価(a);
        var b評価=評価(b);
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                Expression.Block(
                    new[]{
                        b
                    },
                    Expression.Assign(b,Constant評価),
                    InlineExpression(
                        a,
                        v評価,
                        Expression.Add(
                            a評価,
                            b評価
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ引数跨ぎ1(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T,T>>(
                Expression.Call(
                    LetMethod.MakeGenericMethod(v評価.Type,a.Type),
                    v評価,
                    Expression.Lambda(
                        a,
                        b
                    )
                ),
                a
            ),
            default!
        );
    }
    [TestMethod]
    public void Block変数跨ぎ1(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                Expression.Block(
                    new[]{
                        a
                    },
                    Expression.Assign(a,Expression.Default(typeof(T))),
                    Expression.Call(
                        LetMethod.MakeGenericMethod(v評価.Type,a.Type),
                        v評価,
                        Expression.Lambda(
                            a,
                            b
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ01(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var a評価=評価(a);
        var b評価=評価(b);
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                InlineExpression(
                    a,
                    v評価,
                    Expression.Add(
                        LetExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        ),
                        InlineExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ02(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var a評価=評価(a);
        var b評価=評価(b);
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                InlineExpression(
                    a,
                    v評価,
                    Expression.Add(
                        LetExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        ),
                        LetExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ03(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var a評価=評価(a);
        var b評価=評価(b);
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                LetExpression(
                    a,
                    v評価,
                    Expression.Add(
                        InlineExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        ),
                        LetExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ04(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var a評価=評価(a);
        var b評価=評価(b);
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                LetExpression(
                    a,
                    v評価,
                    Expression.Add(
                        LetExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        ),
                        InlineExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ05(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var a評価=評価(a);
        var b評価=評価(b);
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                LetExpression(
                    a,
                    v評価,
                    Expression.Add(
                        LetExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        ),
                        LetExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ10(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var a評価=評価(a);
        var b評価=評価(b);
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                InlineExpression(
                    a,
                    v評価,
                    Expression.Add(
                        InlineExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        ),
                        InlineExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ11(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var a評価=評価(a);
        var b評価=評価(b);
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                Expression.Add(
                    InlineExpression(
                        a,
                        v評価,
                        InlineExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        )
                    ),
                    InlineExpression(
                        a,
                        v評価,
                        LetExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ12(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var a評価=評価(a);
        var b評価=評価(b);
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                Expression.Add(
                    InlineExpression(
                        a,
                        v評価,
                        LetExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        )
                    ),
                    InlineExpression(
                        a,
                        v評価,
                        InlineExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ13(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var a評価=評価(a);
        var b評価=評価(b);
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                Expression.Add(
                    InlineExpression(
                        a,
                        v評価,
                        LetExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        )
                    ),
                    InlineExpression(
                        a,
                        v評価,
                        LetExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ15(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var a評価=評価(a);
        var b評価=評価(b);
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                Expression.Add(
                    InlineExpression(
                        a,
                        v評価,
                        InlineExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        )
                    ),
                    LetExpression(
                        a,
                        v評価,
                        InlineExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダループ跨ぎ19(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var a評価=評価(a);
        var b評価=評価(b);
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                Expression.Add(
                    LetExpression(
                        a,
                        v評価,
                        InlineExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        )
                    ),
                    LetExpression(
                        a,
                        v評価,
                        InlineExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数0(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var a評価=評価(a);
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                LetExpression(
                    a,
                    v評価,
                    InlineExpression(
                        b,
                        v評価,
                        a評価
                    )
                )
            )
        );
        //this.共通Assert(10,() =>
        //    L(a0 =>
        //        L(b0 =>
        //            a0
        //        )
        //    )
        //);
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数1(){
        var a=this.Parameter("a");
        var a評価=評価(a);
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                Expression.Add(
                    LetExpression(
                        a,
                        v評価,
                        a評価
                    ),
                    LetExpression(
                        a,
                        v評価,
                        a評価
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数2(){
        var a=this.Parameter("a");
        var a評価=評価(a);
        var b=this.Parameter("b");
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                LetExpression(
                    a,
                    v評価,
                    LetExpression(
                        b,
                        v評価,
                        Expression.Add(
                            a評価,
                            a評価
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数3(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var a評価=評価(a);
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                LetExpression(
                    a,
                    v評価,
                    Expression.Add(
                        LetExpression(
                            b,
                            v評価,
                            a評価
                        ),
                        LetExpression(
                            b,
                            v評価,
                            a評価
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数4(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var a評価=評価(a);
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                LetExpression(
                    a,
                    v評価,
                    Expression.Add(
                        LetExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                a評価
                            )
                        ),
                        LetExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                a評価
                            )
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数5(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var c=this.Parameter("c");
        var a評価=評価(a);
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                LetExpression(
                    a,
                    v評価,
                    Expression.Add(
                        LetExpression(
                            b,
                            v評価,
                            LetExpression(
                                c,
                                v評価,
                                a評価
                            )
                        ),
                        LetExpression(
                            b,
                            v評価,
                            LetExpression(
                                c,
                                v評価,
                                a評価
                            )
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数6(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var a評価=評価(a);
        var b評価=評価(b);
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                LetExpression(
                    a,
                    v評価,
                    Expression.Add(
                        LetExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        ),
                        LetExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数7(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var a評価=評価(a);
        var b評価=評価(b);
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                Expression.Add(
                    LetExpression(
                        a,
                        v評価,
                        LetExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        )
                    ),
                    LetExpression(
                        a,
                        v評価,
                        LetExpression(
                            b,
                            v評価,
                            Expression.Add(
                                a評価,
                                b評価
                            )
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数8(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var a評価=評価(a);
        var b評価=評価(b);
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                Expression.Add(
                    LetExpression(
                        a,
                        v評価,
                        Expression.Add(
                            LetExpression(
                                b,
                                v評価,
                                Expression.Add(
                                    a評価,
                                    b評価
                                )
                            ),
                            LetExpression(
                                b,
                                v評価,
                                Expression.Add(
                                    a評価,
                                    b評価
                                )
                            )
                        )
                    ),
                    LetExpression(
                        a,
                        v評価,
                        Expression.Add(
                            LetExpression(
                                b,
                                v評価,
                                Expression.Add(
                                    a評価,
                                    b評価
                                )
                            ),
                            LetExpression(
                                b,
                                v評価,
                                Expression.Add(
                                    a評価,
                                    b評価
                                )
                            )
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数9(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var c=this.Parameter("c");
        var a評価=評価(a);
        var c評価=評価(c);
        var v評価=Constant評価;
        var Lambda_c=LetExpression(
            c,
            v評価,
            Expression.Add(
                a評価,
                c評価
            )
        );
        var Lambda_b=LetExpression(
            b,
            v評価,
            Expression.Add(
                Lambda_c,
                Lambda_c
            )
        );
        var Lambda_a=LetExpression(
            a,
            v評価,
            Expression.Add(
                Lambda_b,
                Lambda_b
            )
        );
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                Expression.Add(
                    Lambda_a,
                    Lambda_a
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数10(){
        this.実行結果が一致するか確認(()=>
            7.Let(a0=>
                7.Let(b0=>
                    Inline(()=>
                        b0
                        +
                        a0
                    )
                    +
                    Inline(()=>
                        b0
                        +
                        a0
                    )
                )
                +
                1.Let(b1=>
                    Inline(()=>
                        b1
                        +
                        a0
                    )
                    +
                    Inline(()=>
                        b1
                        +
                        a0
                    )
                )
            )
            +
            1.Let(a1=>
                2.Let(b2=>
                    Inline(()=>
                        b2
                        +
                        a1
                    )
                    +
                    Inline(()=>
                        b2
                        +
                        a1
                    )
                )
                +
                3.Let(b3=>
                    Inline(()=>
                        b3
                        +
                        a1
                    )
                    +
                    Inline(()=>
                        b3
                        +
                        a1
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ評価回数11(){
        this.実行結果が一致するか確認(()=>
            0.Let(a0=>
                1.Let(b1=>
                    2.Let(c2=>
                        c2
                        +
                        a0
                    )
                    +
                    Inline(()=>
                        b1
                        +
                        a0
                    )
                )
                +
                3.Let(b1=>
                    4.Let(c2=>
                        c2
                        +
                        a0
                    )
                    +
                    Inline(()=>
                        b1
                        +
                        a0
                    )
                )
            )
            +
            5.Let(a1=>
                6.Let(b2=>
                    7.Let(c4=>
                        c4
                        +
                        a1
                    )
                    +
                    Inline(()=>
                        b2
                        +
                        a1
                    )
                )
                +
                8.Let(b3=>
                    Inline(()=>
                        b3
                        +
                        a1
                    )
                    +
                    9.Let(c7=>
                        c7
                        +
                        a1
                    )
                )
            )
        );
    }
    //[TestMethod]
    //public void ラムダ跨ぎ評価回数12() {
    //    this.AssertExecute(() =>
    //        0.Let(a0 =>
    //            1.Let(b1 =>
    //                2.Let(c2 =>
    //                    c2
    //                    +
    //                    a0
    //                )
    //                +
    //                Inline(() =>
    //                    c1
    //                    +
    //                    a0
    //                )
    //            )
    //            +
    //            1.Let(b1 =>
    //                2.Let(c2 =>
    //                    c2
    //                    +
    //                    a0
    //                )
    //                +
    //                Inline(() =>
    //                    c1
    //                    +
    //                    a0
    //                )
    //            )
    //        )
    //        +
    //        1.Let(a1 =>
    //            2.Let(b2 =>
    //                3.Let(c4 =>
    //                    c4
    //                    +
    //                    a1
    //                )
    //                +
    //                Inline(c2 =>
    //                    c2
    //                    +
    //                    a1
    //                )
    //            )
    //            +
    //            3.Let(b3 =>
    //                b3Inline(c3 =>
    //                    c3
    //                    +
    //                    a1
    //                )
    //                +
    //                7.Let(c7 =>
    //                    c7
    //                    +
    //                    a1
    //                )
    //            )
    //        )
    //    );
    //}
    [TestMethod]
    public void ラムダ跨ぎ00(){
        var array=ArrN<int>(1);
        this.実行結果が一致するか確認(a=>
            array.GroupBy(LINEITEM=>LINEITEM).Select(p=>p.Max())
        );
        this.実行結果が一致するか確認(a=>
            array.GroupBy(LINEITEM=>LINEITEM).Select(p=>p.Sum())
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ01(){
        var array=ArrN<int>(1);
        this.実行結果が一致するか確認(a =>
            array.GroupBy(LINEITEM => LINEITEM).Select(p => p.Sum()>300m)
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ02(){
        var array=ArrN<int>(1);
        this.実行結果が一致するか確認(a =>
            array.GroupBy(LINEITEM => LINEITEM).Where(p => p.Sum()>300m)
        );
        this.実行結果が一致するか確認(a =>
            a.Let(aa =>
                array.GroupBy(LINEITEM => LINEITEM).Where(p => p.Sum()>300m)
            )
        );
        this.実行結果が一致するか確認(a =>
            a.Let(aa =>
                array.GroupBy(LINEITEM => LINEITEM).Where(p => p.Sum()>300m)
            )
        );
    }

    [TestMethod]
    public void ラムダ跨ぎ1(){
        //var data=new[]{1,2,3}.ToSet();
        //var v=100m;
        this.実行結果が一致するか確認(v =>
            v.Let(a =>
                v.Let(b =>
                    Inline(() =>
                        b==a
                    )
                )
            )
        );
        this.実行結果が一致するか確認(a => ArrN<int>(a).Where(p => p==1));
        this.実行結果が一致するか確認(a => ArrN<int>(a).Select(b => a.Let(c => a.Let(d => c+b))));
        this.実行結果が一致するか確認(a => a.Let(aa => a.Let(aaa => ArrN<int>(a).Where(aaaa => aaaa==aa))));
        //.Lambda ラムダ1<System.Func`1[System.Decimal]>() {
        //    .Block(System.Decimal $Cラムダ跨ぎ0) {
        //        $Cラムダ跨ぎ0 = .Constant<ClientTest.BackendClient.AAATest_Optimizer+<>c__DisplayClass3_0>(ClientTest.BackendClient.AAATest_Optimizer+<>c__DisplayClass3_0).b;
        //        .Call ClientTest.BackendClient.ATest.v.Let(.Lambda ラムダ0<System.Func`1[System.Decimal]>)
        //    }
        //}
        //.Lambda ラムダ0<System.Func`1[System.Decimal]>() {
        //    $Cラムダ跨ぎ0
        //}
        this.実行結果が一致するか確認(a =>
            a.Let(p =>
                a
            )
        );
        this.実行結果が一致するか確認(a =>
            a.Let(b =>
                ArrN<int>(a).Select(p => ArrN<int>(b).Sum(q => q)>300m)
            )
        );
        this.実行結果が一致するか確認(a =>
            a.Let(aa =>
                ArrN<int>(a).Sum(q => q)>300m
            )
        );
        this.実行結果が一致するか確認(a=>
            a.Let(aa=>
                ArrN<int>(a).GroupBy(LINEITEM=>LINEITEM).Where(p=>p.Sum()>300m)
            )
        );
        this.実行結果が一致するか確認(a=>
            a.Let(aa=>
                ArrN<int>(a).GroupBy(LINEITEM=>LINEITEM).Where(p=>p.Sum()>300m)
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ2(){
        var a=this.Parameter("a");
        var b=this.Parameter("b");
        var a評価=評価(a);
        var b評価=評価(b);
        var v評価=Constant評価;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                Expression.Block(
                    new[]{
                        b
                    },
                    Expression.Assign(b,v評価),
                    LetExpression(
                        a,
                        v評価,
                        Expression.Add(
                            a評価,
                            b評価
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void ラムダ跨ぎ3() {
        var array = ArrN<int>(1);
        var g=array.GroupBy(LINEITEM=>LINEITEM);
        this.実行結果が一致するか確認(a =>
            g.Select(p => p.Count())
        );
    }
    private int 組み合わせ1Assert(MethodInfo A_Method0,MethodInfo A_Method3,MethodInfo B_Method1,MethodInfo B_Method2,MethodInfo B_Method4,MethodInfo B_Method5){
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                Expression.Add(
                    組み合わせ1Assert共通メソッド(A_Method0,B_Method1,B_Method2),
                    組み合わせ1Assert共通メソッド(A_Method3,B_Method4,B_Method5)
                )
            )
        );
        return 0;
    }
    private static MethodCallExpression 組み合わせ1Assert共通メソッド(MethodInfo A_Method1,MethodInfo B_Method0,MethodInfo B_Method1){
        return Expression.Call(
            A_Method1.MakeGenericMethod(Arguments),
            A,
            Expression.Lambda(
                Expression.Add(
                    組み合わせ1Assert共通メソッド(B_Method0),
                    組み合わせ1Assert共通メソッド(B_Method1)
                ),
                a
            )
        );
    }
    private static MethodCallExpression 組み合わせ1Assert共通メソッド(MethodInfo B_Method0){
        return Expression.Call(
            B_Method0.MakeGenericMethod(Arguments),
            B,
            Expression.Lambda(
                a,
                b
            )
        );
    }
    [TestMethod]
    public void 組み合わせ1(){
        var Methods=new[]{
            LetMethod,InlineMethod
        };
        var x=(
            from A_Method0 in Methods
            from A_Method1 in Methods
            from B_Method0 in Methods
            from B_Method1 in Methods
            from B_Method2 in Methods
            from B_Method3 in Methods
            select this.組み合わせ1Assert(A_Method0,A_Method1,B_Method0,B_Method1,B_Method2,B_Method3)
        ).ToArray();
    }
    private static readonly ParameterExpression a=Expression.Parameter(typeof(T),"a");
    private static readonly ParameterExpression b=Expression.Parameter(typeof(T),"b");
    private static readonly ParameterExpression c=Expression.Parameter(typeof(T),"c");
    private static readonly Expression A=評価(Expression.Constant(new T()));
    private static readonly Expression B=評価(Expression.Constant(new T()));
    private static readonly Expression C=評価(Expression.Constant(new T()));
    private static readonly Type[] Arguments={
        typeof(T),typeof(T)
    };
    private static MethodCallExpression 組み合わせ2の共通メソッド(MethodInfo A_Method0,MethodInfo B_Method1,MethodInfo B_Method4,MethodInfo C_Method2,MethodInfo C_Method3,MethodInfo C_Method5,MethodInfo C_Method6){
        return Expression.Call(
            A_Method0.MakeGenericMethod(Arguments),
            A,
            Expression.Lambda(
                Expression.Add(
                    組み合わせ2の共通メソッド(B_Method1,C_Method2,C_Method3),
                    組み合わせ2の共通メソッド(B_Method4,C_Method5,C_Method6)
                ),
                a
            )
        );
    }
    private static MethodCallExpression 組み合わせ2の共通メソッド(MethodInfo B_Method1,MethodInfo C_Method0,MethodInfo C_Method1){
        return Expression.Call(
            B_Method1.MakeGenericMethod(Arguments),
            B,
            Expression.Lambda(
                Expression.Add(
                    組み合わせ2の共通メソッド(C_Method0),
                    組み合わせ2の共通メソッド(C_Method1)
                ),
                b
            )
        );
    }
    private static MethodCallExpression 組み合わせ2の共通メソッド(MethodInfo C_Method0){
        return Expression.Call(
            C_Method0.MakeGenericMethod(Arguments),
            C,
            Expression.Lambda(
                a,
                c
            )
        );
    }
    private void 組み合わせ2Assert(MethodInfo A_Method00,MethodInfo A_Method07,MethodInfo B_Method01,MethodInfo B_Method02,MethodInfo B_Method08,MethodInfo B_Method09,MethodInfo C_Method03,MethodInfo C_Method04,MethodInfo C_Method05,MethodInfo C_Method06,MethodInfo C_Method10,MethodInfo C_Method11,MethodInfo C_Method12,MethodInfo C_Method13){
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<T>>(
                Expression.Add(
                    組み合わせ2の共通メソッド(A_Method00,B_Method01,B_Method02,C_Method03,C_Method04,C_Method05,C_Method06),
                    組み合わせ2の共通メソッド(A_Method07,B_Method08,B_Method09,C_Method10,C_Method11,C_Method12,C_Method13)
                )
            )
        );
    }
    [TestMethod]
    public void 組み合わせ2(){
        var Methods=new[]{
            LetMethod,InlineMethod
        };
        var 回数=0;
        foreach(var A_Method0 in Methods)
        foreach(var A_Method1 in Methods)
        foreach(var B_Method0 in Methods)
        foreach(var B_Method1 in Methods)
        foreach(var B_Method2 in Methods)
        foreach(var B_Method3 in Methods)
        foreach(var C_Method0 in Methods)
        foreach(var C_Method1 in Methods)
        foreach(var C_Method2 in Methods)
        foreach(var C_Method3 in Methods)
        foreach(var C_Method4 in Methods)
        foreach(var C_Method5 in Methods)
        foreach(var C_Method6 in Methods)
        foreach(var C_Method7 in Methods){
            //this.組み合わせ2Assert(
            //    A_Method0,A_Method1,
            //    B_Method0,B_Method1,B_Method2,B_Method3,
            //    C_Method0,C_Method1,C_Method2,C_Method3,C_Method4,C_Method5,C_Method6,C_Method7
            //);
            this.実行結果が一致するか確認(
                Expression.Lambda<Func<T>>(
                    Expression.Add(
                        組み合わせ2の共通メソッド(
                            A_Method0,
                            B_Method0,
                            B_Method1,
                            C_Method0,
                            C_Method1,
                            C_Method2,
                            C_Method3
                        ),
                        組み合わせ2の共通メソッド(
                            A_Method1,
                            B_Method2,
                            B_Method3,
                            C_Method4,
                            C_Method5,
                            C_Method6,
                            C_Method7
                        )
                    )
                )
            );
            GC.Collect();
            GC.WaitForPendingFinalizers();
            回数++;
            if(回数==10) return;
        }
        //var x=(
        //    from A_Method0 in Methods
        //    from A_Method1 in Methods
        //    from B_Method0 in Methods
        //    from B_Method1 in Methods
        //    from B_Method2 in Methods
        //    from B_Method3 in Methods
        //    from C_Method0 in Methods
        //    from C_Method1 in Methods
        //    from C_Method2 in Methods
        //    from C_Method3 in Methods
        //    from C_Method4 in Methods
        //    from C_Method5 in Methods
        //    from C_Method6 in Methods
        //    from C_Method7 in Methods
        //    select this.組み合わせ2Assert(
        //        A_Method0,A_Method1,
        //        B_Method0,B_Method1,B_Method2,B_Method3,
        //        C_Method0,C_Method1,C_Method2,C_Method3,C_Method4,C_Method5,C_Method6,C_Method7
        //    )
        //).ToArray();
    }
}
[TestClass]
public sealed class OptimizerGenericInt32:OptimizerGeneric<int>{
}
[TestClass]
public sealed class OptimizerGeneric評価検証:OptimizerGeneric<評価検証>{
    protected override void Assert(Expression e){
        this.実行結果が一致するか確認(Expression.Lambda<Func<評価検証>>(e));
    }
}