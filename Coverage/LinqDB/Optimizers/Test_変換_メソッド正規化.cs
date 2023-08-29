using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable RedundantCast

namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class Test_変換_メソッド正規化:ATest {
    [TestMethod]
    public void Travase() {
        //if(e.NodeType==ExpressionType.AndAlso) {
        //    if(Left葉Outerに移動する) {
        //        if(Right葉Outerに移動する) {
        this.実行結果が一致するか確認((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => new { o,i }).Where(p => p.o==3&&p.o==2));
        //        } else {
        this.実行結果が一致するか確認((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => new { o,i }).Where(p => p.o==3&&p.i==2));
        //        }
        //    } else {
        //        if(Right葉Outerに移動する) {
        this.実行結果が一致するか確認((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => new { o,i }).Where(p => p.o==p.i&&p.o==1));
        //        } else {
        this.実行結果が一致するか確認((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => new { o,i }).Where(p => p.o==p.i&&p.i==p.o));
        //        }
        //    }
        //} else { 
        //    if(this._変数_判定_葉に移動したいpredicate.実行(e,this._DictionaryOuter,this._DictionaryInner)) {
        var c = 1;
        this.実行結果が一致するか確認((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => new { o,i }).Where(p => p.o==c||c==p.o));
        //    } else if(this._変数_判定_葉に移動したいpredicate.実行(e,this._DictionaryInner,this._DictionaryOuter)) {
        this.実行結果が一致するか確認((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => new { o,i }).Where(p => p.i==c||c==p.i));
        //    } else {
        this.実行結果が一致するか確認((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => new { o,i }).Where(p => p.i==p.o||p.o==p.i));
        //    }
        //}
    }
    [TestMethod]
    public void JoinWhere再帰で匿名型を走査() {
        var data = new Set<int> { 1,2,3,4 };
        this.実行結果が一致するか確認((a,b) => 
            ArrN<int>(a).Join(
                ArrN<int>(b),
                o0 => o0,
                i0 => i0,
                (o1,i1) => new { o1,i1 }
            ).Where(
                p => p.o1==3&&p.o1==2
            )
        );
    }
    [TestMethod]
    public void GroupJoin_Whereを葉に移動() {
        var A=new Set<int>{1,2,3,4};
        var B = A;
        var C = A;
        this.実行結果が一致するか確認(() =>
                //A.GroupJoin(
                //    B,o=>o,i=>i,
                //    (o,i)=>new{oo_oi=new{oo=o+o,oi=o+i.Count()},oi_ii=new{oi=o+i.Count(),ii=i.Count()+i.Count()}}
                //).GroupJoin(
                //    C,
                //    oo_oi_oi_ii=>oo_oi_oi_ii.oo_oi.oo,
                //    i=>i,
                //    (o,i)=>new{o,i=i.Count()}
                //).Where(p=>p.o.oo_oi.oo==1&&p.o.oo_oi.oi==2&&p.o.oi_ii.oi==3&&p.o.oi_ii.ii==4&&p.i==5)
                A.Where(
                    o => o+o==1
                ).GroupJoin(
                    B,o => o,i => i,
                    (o,i) => new { oo_oi = new { oo = o+o,oi = o+i.Count() },oi_ii = new { oi = o+i.Count(),ii = i.Count()+i.Count() } }
                ).Where(
                    o => o.oo_oi.oi==2&&o.oi_ii.oi==3&&o.oi_ii.ii==4
                ).GroupJoin(
                    C,oo_oi_oi_ii => oo_oi_oi_ii.oo_oi.oo,i => i,
                    (o,i) => new { o,i = i.Count() }
                ).Where(p => p.i==5)
            //  ArrN<Int32>(a).               GroupJoin(ArrN<Int32>(b),o=>o,i=>i,(o,i)=>new{o=new{oo=o+o,oi=o+i.Count()},i=new{io=i.Count()+o,ii=i.Count()+i.Count()}}).Where(p=>p.o.oo==3&&p.o.oi==4&&p.i.io==5&&p.i.ii==6).Where(p=>p.o.o.oo==3&&p.o.oi==4&&p.i.ii==6).GroupJoin(ArrN<Int32>(b),o=>o.o.oo,i=>i,(o,i)=>new{o,i=i.Count()}).Where(p=>p.i==5)
            //  ArrN<Int32>(a).Where(o=>o==3).GroupJoin(ArrN<Int32>(b),o=>o,i=>i,(o,i)=>new{o=new{oo=o+o,oi=o+i.Count()},i=new{io=i.Count()+o,ii=i.Count()+i.Count()}}).Where(p=>p.o.oo==3&&p.o.oi==4&&p.i.io==5&&p.i.ii==6).Where(p=>p.o.o.oo==3&&p.o.o.oi==4&&p.o.i.ii==6).GroupJoin(ArrN<Int32>(b),o=>o.o.oo,i=>i,(o,i)=>new{o,i=i.Count()}).Where(p=>p.i==5)
        );
        this.実行結果が一致するか確認((a,b)=>
                ArrN<int>(a).GroupJoin(ArrN<int>(b),o=>o,i=>i,(o,i)=>new{o=new{oo=o+o,oi=o+i.Count()},i=new{io=i.Count()+o,ii=i.Count()+i.Count()}}).Where(p=>p.o.Equals(new{oo=p.o.oo*p.o.oo,oi=p.o.oo-p.o.oo}))
            //  ArrN<Int32>(a).GroupJoin(ArrN<Int32>(b),o=>o,i=>i,(o,i)=>new{o=new{oo=o+o,oi=o+i.Count()},i=new{io=i.Count()+o,ii=i.Count()+i.Count()}}).Where(p=>p.o.Equals(new{oo=p.o.oo*p.o.oo,oi=p.o.oo-p.o.oo}))
        );
        this.実行結果が一致するか確認((a,b)=>
                ArrN<int>(a).               GroupJoin(ArrN<int>(b),o=>o,i=>i,(o,i)=>new{o=new{oo=o+o,oi=o+i.Count()},i=new{io=i.Count()+o,ii=i.Count()+i.Count()}}).Where(p=>p.o.oo==3)
            //  ArrN<Int32>(a).Where(o=>o==3).GroupJoin(ArrN<Int32>(b),o=>o,i=>i,(o,i)=>new{o=new{oo=o+o,oi=o+i.Count()},i=new{io=i.Count()+o,ii=i.Count()+i.Count()}})
        );
        this.実行結果が一致するか確認((a,b)=>
                ArrN<int>(a).               GroupJoin(ArrN<int>(b),o=>o,i=>i,(o,i)=>new{o=o+1,i=i.Count()+2}).Where(p=>p.o==3)
            //  ArrN<Int32>(a).Where(o=>o==3).GroupJoin(ArrN<Int32>(b),o=>o,i=>i,(o,i)=>new{o=o+1,i=i.Count()+2})
        );
        this.実行結果が一致するか確認((a,b)=>
                ArrN<int>(a).               GroupJoin(ArrN<int>(b),o=>o,i=>i,(o,i)=>new{o=o+1,i=i.Count()+2}).Where(p=>p.o==3&&p.i==2)
            //  ArrN<Int32>(a).Where(o=>o==3).GroupJoin(ArrN<Int32>(b),o=>o,i=>i,(o,i)=>new{o=o+1,i=i.Count()+2}).Where(p=>        p.i==2)
        );
    }
    [TestMethod]
    public void Select_Whereを葉に移動() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Select(
                s => new { a = s+s,b = s*s }
            ).Where(
                p => p.a==1&&p.b==2
            )
        );
    }
    [TestMethod]
    public void Join_Join_Whereを葉に移動(){
        var data=new Set<int>{1,2,3,4};
        this.実行結果が一致するか確認((a,b)=>
            ArrN<int>(a).Join(
                ArrN<int>(b),
                o=> o,
                i => i,
                (int o,int i)=>new{ o,i }
            ).Join(
                ArrN<int>(b),
                o0=> o0.
                    o,i0=> i0,
                (o1,i1)=>new{o2= o1,i2=i1 }
            ).Where(p=> p.o2.o==1)
        );
    }
    [TestMethod]
    public void Join_匿名型_Whereを葉に移動0() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Join(
                ArrN<int>(b),
                o => o,
                i => i,
                (o0,i0) => new {i0,o0 }
            ).Where(
                p => p.i0.Equals(
                    p.i0
                )
            )
        );
    }
    [TestMethod]
    public void Join_匿名型_Whereを葉に移動1() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Join(
                ArrN<int>(b),
                o => o,
                i => i,
                (o0,i0) => new { o1 = new { o2= o0},i1 = new { i1= i0} }
            ).Where(
                p => p.o1.o2.Equals(
                    p.o1.o2
                )
            )
        );
    }
    [TestMethod]
    public void Join_匿名型_Whereを葉に移動2() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Join(
                ArrN<int>(b),
                o => o,
                i => i,
                (o0,i0) => new { o1 = new { o0o0 = o0+o0,o0i0 = o0+i0 },i1 = new { i0o0 = i0+o0,i0i0 = i0+i0 } }
            ).Where(
                p => p.o1.o0o0.Equals(
                    p.o1.o0o0
                )
            )
        );
    }
    [TestMethod]
    public void Join_匿名型_Whereを葉に移動3() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Join(
                ArrN<int>(b),
                o => o,
                i => i,
                (o0,i0) => new { o1 = new { o0o0 = o0+o0,o0i0 = o0+i0 },i1 = new { i0o0 = i0+o0,i0i0 = i0+i0 } }
            ).Where(
                p => p.o1.Equals(
                    new { oo2 = p.o1.o0o0*p.o1.o0o0,oi2 = p.o1.o0o0-p.o1.o0o0 }
                )
            )
        );
    }
    [TestMethod]
    public void SelectMany_SelectMany0() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                p => ArrN<int>(b)
            ).SelectMany(
                q => ArrN<int>(a+b)
            )
        );
    }
    [TestMethod]
    public void SelectMany_SelectMany1() {
        this.実行結果が一致するか確認((a,b) =>
            SetN<int>(a).SelectMany(
                o1=>SetN<int>(b)
            ).SelectMany(
                o0 => SetN<int>(a+b)
            )
        );
    }
    [TestMethod]
    public void SelectMany_SelectMany2() {
        this.実行結果が一致するか確認((a,b) =>
            SetN<int>(a).SelectMany(
                o1 => SetN<int>(b).SelectMany(
                    o0 => SetN<int>(a+b).Select(p => new { a,b })
                )
            )
        );
    }
    [TestMethod]
    public void SelectMany_SelectMany3() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                o0 => ArrN<int>(b),
                (o1,i1) => o1+i1
            ).SelectMany(
                p => ArrN<int>(a+b),
                (o2,i2) => o2+i2
            ).Select(
                p => p*p
            )
        );
    }
    [TestMethod]
    public void Select_selector_Select_selector() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Select(
                (p1) => p1+p1
            ).Select<int,double>(
                p0=> p0*p0
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Select(
                (p1) => p1+p1
            ).Select(
                (Func<int,double>)(p0 => p0*p0)
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Select(
                (Func<int,int>)((p1) => p1+p1)
            ).Select<int,double>(
                p0=> p0*p0
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Select(
                (Func<int,int>)((p1) => p1+p1)
            ).Select(
                (Func<int,double>)(p0 => p0*p0)
            )
        );
    }
    [TestMethod]
    public void Select_selector_Select_indexSelector() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Select(
                (p1) => p1+p1
            ).Select<int,double>(
                (p0,index0) => p0*index0
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Select(
                (p1) => p1+p1
            ).Select(
                (Func<int,int,double>)((p0,index0) => (double)p0*index0)
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Select(
                (Func<int,int>)((p1) => p1+p1)
            ).Select<int,double>(
                (p0,index0) => (double)p0*index0
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Select(
                (Func<int,int>)((p1) => p1+p1)
            ).Select(
                (Func<int,int,double>)((p0,index0) => (double)p0*index0)
            )
        );
    }
    [TestMethod]
    public void Select_indexSelector_Select_selector() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Select(
                (p0,index0) => p0+index0
            ).Select<int,double>(
                p1 => p1*p1
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Select(
                (p0,index0) => p0+index0
            ).Select(
                (Func<int,double>)(p1 => p1*p1)
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Select(
                (Func<int,int,int>)((p0,index0) => p0+index0)
            ).Select<int,double>(
                p1 => p1*p1
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Select(
                (Func<int,int,int>)((p0,index0) => p0+index0)
            ).Select(
                (Func<int,double>)(p1 => p1*p1)
            )
        );
    }
    [TestMethod]
    public void Select_indexSelector_Select_indexSelector() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Select(
                (p0,index0) => p0+index0
            ).Select<int,double>(
                (p1,index1) => p1*index1
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Select(
                (p0,index0) => p0+index0
            ).Select<int,double>(
                (Func<int,int,double>)((p1,index1) => p1*index1)
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Select(
                (Func<int,int,int>)((p0,index0) => p0+index0)
            ).Select<int,double>(
                (p1,index1) => p1*index1
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Select(
                (Func<int,int,int>)((p0,index0) => p0+index0)
            ).Select<int,double>(
                (Func<int,int,double>)((p1,index1) => p1*index1)
            )
        );
    }
    [TestMethod]
    public void SelectMany_indexColleciotnSelector_resultSelector_Select_indexSelector() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                (o0,index0) => ArrN<int>(b+index0),
                (o1,i1) => o1+i1
            ).Select(
                (p,index) => p*index
            )
        );
    }
    [TestMethod]
    public void SelectMany_colleciotnSelector_resultSelector_Select_indexSelector() {
        //this.AssertExecute((a,b) =>
        //    ArrN<Int32>(a).SelectMany(
        //        (o0) => ArrN<Int32>(b).Select(
        //            i1 => o0
        //        )
        //    )
        //);
        //this.AssertExecute((a,b) =>
        //    ArrN<Int32>(a).SelectMany(
        //        (o0) => ArrN<Int32>(b).Select(
        //            i1 => o0
        //        ).Select(
        //            (p,index) => p
        //        )
        //    )
        //);
        //this.AssertExecute((a,b) =>
        //    ArrN<Int32>(a).SelectMany(
        //        (o0) => ArrN<Int32>(b).Select(
        //            i1 => o0+i1
        //        ).Select(
        //            (p,index) => p*index
        //        )
        //    )
        //);
        //this.AssertExecute((a,b)=>
        //    ArrN<Int32>(a).SelectMany(
        //        (o0) => ArrN<Int32>(b),
        //        (o1,i1) => o1+i1
        //    ).Select(
        //        (p,index) => p*index
        //    )
        //);
        var s = ArrN<int>(2).SelectMany(
            (_) => ArrN<int>(2),
            (o1,i1) => o1+i1
        ).Select(
            (_,index) => index
        ).ToArray();
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                (o0) => ArrN<int>(b+b),
                (o1,i1) => o1+i1
            ).Select(
                (p,index) => index
            )
        );
    }
    [TestMethod]
    public void SelectMany_indexSelector_SelectMany_indexSelector0() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                (o0,index0) => ArrN<int>(b+index0)
            ).Select(
                (p,index) => p*index
            )
        );
    }
    [TestMethod]
    public void SelectMany_indexSelector_SelectMany_indexSelector1() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                (o0,index0) => ArrN<int>(b+index0),
                (o1,i1) => o1+i1
            ).SelectMany(
                (o2,index2) => ArrN<int>(a+b+index2),
                (o3,i3) => o3+i3
            ).Select(
                (p,index) => p*index
            )
        );
    }
    [TestMethod]
    public void SelectMany_indexSelector_SelectMany_indexSelector2() {
        this.実行結果が一致するか確認((a,b) =>
            SetN<int>(a).SelectMany(
                (o1,index1) => SetN<int>(b).SelectMany(
                    (o0,index0) => SetN<int>(a+b).Select((p,index) => new { a,b })
                )
            )
        );
    }
    [TestMethod]
    public void Join_Join0() {
        this.実行結果が一致するか確認((a,b) =>
            SetN<int>(a).Join(
                SetN<int>(b).Join(
                    SetN<int>(a+b),
                    o0 => o0,
                    i0 => i0,
                    (o1,i1) => o1+i1
                ),
                o2 => o2,
                i3 => i3,
                (o4,i4) => new { o4,i4 }
            )
        );
    }
    [TestMethod]
    public void Join_Join1() {
        this.実行結果が一致するか確認((a,b) =>
            SetN<int>(a).Join(
                SetN<int>(b).Join(
                    SetN<int>(a+b),
                    o => new { Key = new { Key = o },Key1 = new { Key = o } },
                    i => new { Key = new { Key = i },Key1 = new { Key = i } },
                    (o,i) => o+i
                ),
                o => new { Key = new { Key = o },Key1 = new { Key = o } },
                i => new { Key = new { Key = i },Key1 = new { Key = i } },
                (o,i) => new { o,i }
            )
        );
    }
    private static Func<T,TKey> keySelector<T, TKey>(Func<T,TKey> s) => s;
    private static Func<T0,T1,TResult> resultSelector<T0, T1, TResult>(Func<T0,T1,TResult> s) => s;
    private static Func<T,bool> predicate<T>(Func<T,bool> s) => s;
    [TestMethod]
    public void Join_resultSelector_Whereを葉に移動0() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Join(
                ArrN<int>(b),
                o => o,
                i => i,
                (o0,i0) => new { o1 = new { o0o0 = o0+o0,o0i0 = o0+i0 },i1 = new { i0o0 = i0+o0,i0i0 = i0+i0 } }
            ).Where(
                p => p.i1.Equals(p.o1)
            )
        );
    }
    [TestMethod]
    public void Join_resultSelector_Whereを葉に移動1() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Join(
                ArrN<int>(b),
                o => o,
                i => i,
                (o0,i0) => new { o1 = new { o0o0 = o0+o0,o0i0 = o0+i0 },i1 = new { i0o0 = i0+o0,i0i0 = i0+i0 } }
            ).Where(
                p => p.o1.Equals(new { oo = p.o1.o0o0+p.o1.o0i0,oi = p.i1.i0o0+p.i1.i0i0 })
            )
        );
    }
    [TestMethod]
    public void Join_resultSelector_Whereを葉に移動2() {
        this.実行結果が一致するか確認((a,b)=>
            ArrN<int>(a).Join(
                ArrN<int>(b),
                o=>o,
                i=>i,
                (o0,i0)=>new{o1=new{o0o0=o0+o0,oi=o0+i0},i1=new{i0o0=i0+o0,i0i0=i0+i0}}
            ).Where(p=>p.o1.o0o0==3)
        );
    }
    [TestMethod]
    public void Join_resultSelector_Whereを葉に移動3() {
        this.実行結果が一致するか確認((a,b)=>
            ArrN<int>(a).Join(
                ArrN<int>(b),
                o=>o,
                i=>i,
                (o,i)=>new{o=new{oo=o+o,oi=o+i},i=new{io=i+o,ii=i+i}}
            ).Where(p=>p.o.oi==3)
        );
    }
    [TestMethod]
    public void Join_resultSelector_Whereを葉に移動4() {
        this.実行結果が一致するか確認((a,b)=>
            ArrN<int>(a).Join(
                ArrN<int>(b),
                o=>o,
                i=>i,
                (o,i)=>new{o=new{oo=o+o,oi=o+i},i=new{io=i+o,ii=i+i}}
            ).Where(p=>p.i.io==3)
        );
    }
    [TestMethod]
    public void Join_resultSelector_Whereを葉に移動5() {
        this.実行結果が一致するか確認((a,b)=>
            ArrN<int>(a).Join(
                ArrN<int>(b),
                o=>o,
                i=>i,
                (o,i)=>new{o=new{oo=o+o,oi=o+i},i=new{io=i+o,ii=i+i}}
            ).Where(p=>p.i.ii==3)
        );
    }
    [TestMethod]
    public void Join_resultSelector_Whereを葉に移動6() {
        this.実行結果が一致するか確認((a,b)=>
            ArrN<int>(a).Join(
                ArrN<int>(b),
                o=>o,
                i=>i,
                (o,i)=>new{o=o+1,i=i+2}
            ).Where(p=>p.o==3)
        );
    }
    [TestMethod]
    public void Join_resultSelector_Whereを葉に移動7() {
        this.実行結果が一致するか確認((a,b)=>
            ArrN<int>(a).Join(ArrN<int>(b),o=>o,i=>i,(o,i)=>new{o=o+1,i=i+2}).Where(p=>p.i==2)
        );
    }
    [TestMethod]
    public void Join_resultSelector_Whereを葉に移動8() {
        this.実行結果が一致するか確認((a,b)=>
            ArrN<int>(a).Join(ArrN<int>(b),o=>o,i=>i,(o,i)=>new{o=o+1,i=i+2}).Where(p=>p.o==3&&p.i==2)
        );
    }
    [TestMethod]
    public void Join_resultSelector_Whereを葉に移動9() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Join(
                ArrN<int>(b),
                o => o,
                i => i,
                (o,i) => new { o,i}
            ).Where(p => p.o==3)
        );
    }
    [TestMethod]
    public void Join_resultSelector_Whereを葉に移動10() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Join(
                ArrN<int>(b),
                o => new { },
                i => new { },
                (o,i) => new { o,i }
            ).Where(p => p.o==3)
        );
    }
    [TestMethod]
    public void SelectMany_selector_Whereを葉に移動0() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                o => ArrN<int>(b).Where(
                    i=>o==i&&o==0&&i==1&&i==o&&2==o&&3==i
                )
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                o => ArrN<int>(b).Where(
                    (Func<int,bool>)(i => o==i&&o==0&&i==1&&i==o&&2==o&&3==i)
                )
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                (Func<int,IEnumerable<int>>)(o => ArrN<int>(b).Where(
                    i => o==i&&o==0&&i==1&&i==o&&2==o&&3==i
                ))
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                (Func<int,IEnumerable<int>>)(o => ArrN<int>(b).Where(
                    (Func<int,bool>)(i => o==i&&o==0&&i==1&&i==o&&2==o&&3==i)
                ))
            )
        );
    }
    [TestMethod]
    public void SelectMany_selector_Whereを葉に移動1() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                o => ArrN<int>(b),
                (o,i) =>new ValueTuple<int,int>(o,i)
            ).Where(
                i => i.Item1==0&&2==i.Item1&&i.Item2==1&&3==i.Item2&&i.Item1==i.Item2&&i.Item2==i.Item1
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                o => ArrN<int>(b),
                (o,i) => new { o,i }
            ).Where(
                i => i.o==0&&2==i.o&&i.i==1&&3==i.i&&i.o==i.i&&i.i==i.o
            )
        );
    }
    [TestMethod]
    public void SelectMany_selector_Whereを葉に移動2() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                o => ArrN<int>(b),
                (o,i) => new { o,i }
            ).Where(
                i => i.o==i.i&&i.o==0&&i.i==1&&i.i==i.o&&2==i.o&&3==i.i
            )
        );
    }
    [TestMethod]
    public void SelectMany_selector_Whereを葉に移動3() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                o => ArrN<int>(b)
            ).Where(
                i => i==0&&1==i
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                o => ArrN<int>(b)
            ).Where(
                (Func<int,bool>)(i => i==0&&1==i)
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                (Func<int,int[]>)(o => ArrN<int>(b))
            ).Where(
                i => i==0&&1==i
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                (Func<int,int[]>)(o => ArrN<int>(b))
            ).Where(
                (Func<int,bool>)(i => i==0&&1==i)
            )
        );
    }
    [TestMethod]
    public void SelectMany_selector_Whereを葉に移動4() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                o => ArrN<int>(b).Where(
                    i => i==0&&1==o
                ).Select(
                    i=>i*i
                )
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                o => ArrN<int>(b).Where(
                    (Func<int,bool>)(i => i==0&&1==o)
                ).Select(
                    i => i*i
                )
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                (Func<int,IEnumerable<int>>)(o => ArrN<int>(b).Where(
                    i => i==0&&1==o
                ).Select(
                    i => i*i
                ))
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                (Func<int,IEnumerable<int>>)(o => ArrN<int>(b).Where(
                    (Func<int,bool>)(i => i==0&&1==o)
                ).Select(
                    i => i*i
                ))
            )
        );
    }
    [TestMethod]
    public void SelectMany_selector_Whereを葉に移動5() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                o => ArrN<int>(b).Where(
                    i => i==0&&o==1&&o==i
                )
            ).Select(
                io=>io+io
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                o => ArrN<int>(b).Where(
                    (Func<int,bool>)(i => i==0&&o==1&&o==i)
                )
            ).Select(
                io => io+io
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                (Func<int,IEnumerable<int>>)(o => ArrN<int>(b).Where(
                    i => i==0&&o==1&&o==i
                ))
            ).Select(
                io => io+io
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                (Func<int,IEnumerable<int>>)(o => ArrN<int>(b).Where(
                    (Func<int,bool>)(i => i==0&&o==1&&o==i)
                ))
            ).Select(
                io => io+io
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                o => ArrN<int>(b).Where(
                    i => i==0&&o==1&&o==i
                )
            ).Select(
                (Func<int,int>)(io => io+io)
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                o => ArrN<int>(b).Where(
                    (Func<int,bool>)(i => i==0&&o==1&&o==i)
                )
            ).Select(
                (Func<int,int>)(io => io+io)
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                (Func<int,IEnumerable<int>>)(o => ArrN<int>(b).Where(
                    i => i==0&&o==1&&o==i
                ))
            ).Select(
                (Func<int,int>)(io => io+io)
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                (Func<int,IEnumerable<int>>)(o => ArrN<int>(b).Where(
                    (Func<int,bool>)(i => i==0&&o==1&&o==i)
                ))
            ).Select(
                (Func<int,int>)(io => io+io)
            )
        );
    }
    [TestMethod]
    public void SelectMany_resultSelector_Whereを葉に移動0() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                o0=>ArrN<int>(b),
                (o1,i1) => new { 
                    o1 = new { 
                        o0o0 = o1+o1,
                        o0i0 = o1+i1 
                    },
                    i1 = new { 
                        i0o0 = i1+o1,
                        i0i0 = i1+i1 
                    } 
                }
            ).Where(
                p => p.o1.Equals(new { 
                    oo = p.o1.o0o0+p.o1.o0i0,
                    oi = p.i1.i0o0+p.i1.i0i0 
                })
            )
        );
    }
    [TestMethod]
    public void SelectMany_resultSelector_Whereを葉に移動1() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                o0 => ArrN<int>(b).Where(
                    i0 => o0==i0&&o0==0&&i0==1&&i0==o0&&2==o0&&3==i0
                ),
                (o,i) => new { o,i }
            ).Select(
                oi => new { oi.o,oi.i }
            )
        );
    }
    [TestMethod]
    public void SelectMany_resultSelector_Whereを葉に移動2() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                a=>ArrN<int>(b),
                (o,i)=>new { o,i }
            ).Where(
                p => p.o==p.i&&p.o==0&&p.i==1&&p.i==p.o&&2==p.o&&3==p.i
            ).Select(
                oi => new { oi.o,oi.i }
            )
        );
    }
    [TestMethod]
    public void SelectMany_resultSelector_Whereを葉に移動3() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                o0=>ArrN<int>(b),
                (o1,i1) => new { 
                    o1 = new { 
                        o1o1 = o1+o1,
                        o1i1 = o1+i1 
                    },
                    i1 = new { 
                        i1o1 = i1+o1,
                        i1i1 = i1+i1 
                    } 
                }
            ).Where(
                p => p.o1.o1i1==p.i1.i1i1&&p.o1.o1o1==0&&p.i1.i1i1==1&&2==p.o1.o1o1&&3==p.i1.i1i1
            )
        );
    }
    [TestMethod]
    public void SelectMany_resultSelector_Whereを葉に移動4() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                o0=>ArrN<int>(b),
                (o1,i1) => new {
                    o1 = new {
                        o1o1 = o1+o1,
                        o1i1 = o1+i1
                    },
                    i1 = new {
                        i1o1 = i1+o1,
                        i1i1 = i1+i1
                    }
                }
            ).Where(
                p => p.o1.Equals(new {
                    o1 = new {
                        o1o1=0,
                        o1i1=1
                    },
                    i1 = new {
                        i1o1=2,
                        i1i1=3
                    }
                })
            )
        );
    }
    [TestMethod]
    public void SelectMany_resultSelector_Whereを葉に移動5() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                o0 => ArrN<int>(b),
                (o1,i1) => new { o1,i1 }
            ).Where(
                p => p.o1==p.i1
            )
        );
    }
    [TestMethod]
    public void SelectMany_Select() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                p => ArrN<int>(b)
            ).Select(
                q => q*q
            )
        );
    }
    [TestMethod]
    public void SelectMany_SelectMany_SelectMany() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                p => ArrN<int>(b)
            ).SelectMany(
                q => ArrN<int>(a+b)
            ).SelectMany(
                r => ArrN<int>(a*b)
            )
        );
    }
    [TestMethod]
    public void Intersect_SelectMany0() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                p => ArrN<int>(b)
            ).Intersect(
                ArrN<int>(b)
            )
        );
    }
    [TestMethod]
    public void Intersect_SelectMany1() {
        this.実行結果が一致するか確認((a,b) =>
            SetN<int>(a).SelectMany(
                p => SetN<int>(b)
            ).Intersect(
                ArrN<int>(b)
            )
        );
    }
    [TestMethod]
    public void Intersect_SelectMany2() {
        this.実行結果が一致するか確認((a,b) =>
            SetN<int>(a).SelectMany(
                p => SetN<int>(b).Select(q => p*10+q*1)
            ).Intersect(
                SetN<int>(b).Select(q => q*10)
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            SetN<int>(a).SelectMany(
                p => SetN<int>(b).Select(q=>p*10+q*1)
            ).Intersect(
                ArrN<int>(b).Select(q =>q*10)
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                p => ArrN<int>(b).Select(q => p*10+q*1)
            ).Intersect(
                ArrN<int>(b).Select(q => q*10)
            )
        );
    }
    [TestMethod]
    public void SelectMany_SelectMany_Select() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).SelectMany(
                p => ArrN<int>(b)
            ).SelectMany(
                q => ArrN<int>(a+b)
            ).Select(
                r => r*r
            )
        );
    }
    [TestMethod]
    public void Intersect_Whereを葉に移動(){
        this.実行結果が一致するか確認((a,b)=>
                ArrN<int>(a).Intersect(ArrN<int>(b)).Where(p=>p+1==p*p)
            //  ArrN<Int32>(a).Where(p=>p+1==p*p).Intersect(ArrN<Int32>(b).Where(p=>p+1==p*p))
        );
        this.実行結果が一致するか確認((a,b)=>
                ArrN<int>(a).Intersect(ArrN<int>(b).Where(i=>i==2)).Where(p=>p+1==p*p)
            //  ArrN<Int32>(a).Where(p=>p+1==p*p).Intersect(ArrN<Int32>(b).Where(i=>i==2).Where(p=>p+1==p*p))
            //  ArrN<Int32>(a).Where(p=>p+1==p*p).Intersect(ArrN<Int32>(b).Where(i=>i==2&&i+1==i*i))
        );
        this.実行結果が一致するか確認((a,b)=>
                ArrN<int>(a).Where(o=>o==1).Intersect(ArrN<int>(b)).Where(p=>p+1==p*p)
            //  ArrN<Int32>(a).Where(o=>o==1).Where(p=>p+1==p*p).Intersect(ArrN<Int32>(b).Where(p=>p+1==p*p))
            //  ArrN<Int32>(a).Where(o=>o==1&&o+1==o*o).Intersect(ArrN<Int32>(b).Where(p=>p+1==p*p))
        );
        this.実行結果が一致するか確認((a,b)=>
                ArrN<int>(a).Where(o=>o==1).Intersect(ArrN<int>(b).Where(i=>i==2)).Where(p=>p+1==p*p)
            //  ArrN<Int32>(a).Where(o=>o==1).Where(p=>p+1==p*p).Intersect(ArrN<Int32>(b).Where(i=>i==2).Where(p=>p+1==p*p))
            //  ArrN<Int32>(a).Where(o=>o==1&&o+1==o*o).Intersect(ArrN<Int32>(b).Where(i=>i==2&&i+1==i*i))
        );
    }
    [TestMethod]
    public void Where_Where() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Where(
                (p1) => p1==1
            ).Where(
                (p0) => p0==0
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Where(
                (p1) => p1==1
            ).Where(
                (Func<int,bool>)((p0) => p0==0)
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Where(
                (Func<int,bool>)((p1) => p1==1)
            ).Where(
                (p0) => p0==0
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Where(
                (Func<int,bool>)((p1) => p1==1)
            ).Where(
                (Func<int,bool>)((p0) => p0==0)
            )
        );
    }
    [TestMethod]
    public void Where_Where_index() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Where(
                (p1) => p1==1
            ).Where(
                (p0,i0) => p0==i0
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Where(
                (p1) => p1==1
            ).Where(
                (Func<int,int,bool>)((p0,i0) => p0==i0)
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Where(
                (Func<int,bool>)((p1) => p1==1)
            ).Where(
                (p0,i0) => p0==i0
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Where(
                (Func<int,bool>)((p1) => p1==1)
            ).Where(
                (Func<int,int,bool>)((p0,i0) => p0==i0)
            )
        );
    }
    [TestMethod]
    public void Where_index_Where() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Where(
                (p1,i1) => p1==i1
            ).Where(
                (p0) => p0==0
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Where(
                (p1,i1) => p1==i1
            ).Where(
                (Func<int,bool>)((p0) => p0==0)
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Where(
                (Func<int,int,bool>)((p1,i1) => p1==i1)
            ).Where(
                (p0) => p0==0
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Where(
                (Func<int,int,bool>)((p1,i1) => p1==i1)
            ).Where(
                (Func<int,bool>)((p0) => p0==0)
            )
        );
    }
    [TestMethod]
    public void Where_index_Where_index() {
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Where(
                (p1,i1) => p1==i1
            ).Where(
                (p0,i0) => p0==i0
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Where(
                (p1,i1) => p1==i1
            ).Where(
                (Func<int,int,bool>)((p0,i0) => p0==i0)
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Where(
                (Func<int,int,bool>)((p1,i1) => p1==i1)
            ).Where(
                (p0,i0) => p0==i0
            )
        );
        this.実行結果が一致するか確認((a,b) =>
            ArrN<int>(a).Where(
                (Func<int,int,bool>)((p1,i1) => p1==i1)
            ).Where(
                (Func<int,int,bool>)((p0,i0) => p0==i0)
            )
        );
    }
}