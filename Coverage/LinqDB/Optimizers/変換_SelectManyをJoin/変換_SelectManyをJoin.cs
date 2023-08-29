using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable InvokeAsExtensionMethod
namespace CoverageCS.LinqDB.Optimizers.変換_SelectManyをJoin;

[TestClass]
public class 変換_SelectManyをJoin:ATest {
    [TestMethod]
    public void Traverse1() {
        var S0 = new Set<int>();
        var S1 = new Set<int>();
        //if(
        //    ループ展開可能メソッドか(GetGenericMethodDefinition(外MethodCall0_Method))&&
        //    nameof(ExtendSet.Where)==外MethodCall0_Method.Name&&
        //    外MethodCall1_Arguments[1] is LambdaExpression Where_predicate&&
        //    外MethodCall1_Arguments[0] is MethodCallExpression 左MethodCall&&
        //    ループ展開可能メソッドか(GetGenericMethodDefinition(左MethodCall.Method))&&
        //    nameof(ExtendSet.SelectMany)==左MethodCall.Method.Name&&
        //    左MethodCall.Arguments[1] is LambdaExpression SelectMany_resultSelector&&
        //    SelectMany_resultSelector.Body is MethodCallExpression 内MethodCall&&
        //    ループ展開可能メソッドか(GetGenericMethodDefinition(内MethodCall.Method))&&
        //    nameof(ExtendSet.Select)==内MethodCall.Method.Name&&
        //    内MethodCall.Arguments[1] is LambdaExpression Select_selector&&
        //    Select_selector.Body is NewExpression New&&
        //    New.Arguments.Count==2
        //) {
        //    if(Select_selector.Body.Type.IsAnonymous()) {
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.Select(
                    s1 => new { s0,s1 }
                )
            ).Where(
                lr => lr.s0==lr.s1
            )
        );
        //    } else {
        //    }
        //    if(Listビルド_Count>0) {
        //        if(Listビルド_Count==1) {
        //        } else {
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.Select(
                    s1 => new { s0,s1 }
                )
            ).Where(
                lr => lr.s0==lr.s1&&lr.s0+1==lr.s1+1
            )
        );
        //        }
        //        if(葉Where!=null) {
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.Select(
                    s1 => new { s0,s1 }
                )
            ).Where(
                lr => lr.s0==lr.s1&&lr.s0<3
            )
        );
        //        }
        //        if(根Where!=null) {
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.Select(
                    s1 => new { s0,s1 }
                )
            ).Where(
                lr => lr.s0==lr.s1&&lr.s0<lr.s1
            )
        );
        //        }
        //    } else if(葉Where!=null) {
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.Select(
                    s1 => new { s0,s1 }
                )
            ).Where(
                lr => lr.s0==0
            )
        );
        //    } else {
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.Select(
                    s1 => new { s0,s1 }
                )
            ).Where(
                lr => true
            )
        );
        //    }
        //} else {
        //}
    }
    [TestMethod]
    public void Traverse2() {
        var S0 = new Set<int>();
        var S1 = new Set<int>();
        var S2 = new Set<int>();
        //if(
        //    ループ展開可能メソッドか(GetGenericMethodDefinition(外MethodCall0_Method))&&
        //    nameof(ExtendSet.Where)==外MethodCall0_Method.Name&&
        //    外MethodCall1_Arguments[1] is LambdaExpression Where_predicate&&
        //    外MethodCall1_Arguments[0] is MethodCallExpression 左MethodCall&&
        //    ループ展開可能メソッドか(GetGenericMethodDefinition(左MethodCall.Method))&&
        //    nameof(ExtendSet.SelectMany)==左MethodCall.Method.Name&&
        //    左MethodCall.Arguments[1] is LambdaExpression SelectMany_resultSelector&&
        //    SelectMany_resultSelector.Body is MethodCallExpression 内MethodCall&&
        //    ループ展開可能メソッドか(GetGenericMethodDefinition(内MethodCall.Method))&&
        //    nameof(ExtendSet.Select)==内MethodCall.Method.Name&&
        //    内MethodCall.Arguments[1] is LambdaExpression Select_selector&&
        //    Select_selector.Body is NewExpression New&&
        //    New.Arguments.Count==2
        //) {
        //    if(Select_selector.Body.Type.IsAnonymous()) {
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.SelectMany(
                    s1 => S2.Select(
                        s2 => new { s1,s2 }
                    )
                ).Select(s12 =>
                    new { s0,s12 }
                )
            ).Where(
                lr => lr.s0==lr.s12.s1
            )
        );
        //    } else {
        //    }
        //    if(Listビルド_Count>0) {
        //        if(Listビルド_Count==1) {
        //        } else {
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.SelectMany(
                    s1 => S2.Select(
                        s2 => new { s1,s2 }
                    )
                ).Select(s12 =>
                    new { s0,s12 }
                )
            ).Where(
                lr => lr.s0==lr.s12.s1&&lr.s0+1==lr.s12.s1+1
            )
        );
        //        }
        //        if(葉Where!=null) {
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.SelectMany(
                    s1 => S2.Select(
                        s2 => new { s1,s2 }
                    )
                ).Select(s12 =>
                    new { s0,s12 }
                )
            ).Where(
                lr => lr.s0==lr.s12.s1&&lr.s0<3
            )
        );
        //        }
        //        if(根Where!=null) {
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.SelectMany(
                    s1 => S2.Select(
                        s2 => new { s1,s2 }
                    )
                ).Select(s12 =>
                    new { s0,s12 }
                )
            ).Where(
                lr => lr.s0==lr.s12.s1&&lr.s0<lr.s12.s1
            )
        );
        //        }
        //    } else if(葉Where!=null) {
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.SelectMany(
                    s1 => S2.Select(
                        s2 => new { s1,s2 }
                    )
                ).Select(s12 =>
                    new { s0,s12 }
                )
            ).Where(
                lr => lr.s0==0
            )
        );
        //    } else {
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.SelectMany(
                    s1 => S2.Select(
                        s2 => new { s1,s2 }
                    )
                ).Select(s12 =>
                    new { s0,s12 }
                )
            ).Where(
                lr => true
            )
        );
        //    }
        //} else {
        //}
    }
    [TestMethod]
    public void SelectMany_SelectManyをJoin_Join() {
        var S0 = new Set<int>();
        var S1 = new Set<int>();
        var S2 = new Set<int>();
        this.実行結果が一致するか確認(
            () => ExtensionSet.Where(
                ExtensionSet.SelectMany(
                    ExtensionSet.SelectMany(
                        S0,
                        s0 => ExtensionSet.Select(
                            S1,
                            s1 => new { s0,s1 }
                        )
                    ),
                    s01 => ExtensionSet.Select(
                        S2,
                        s2 => new { s01,s2 }
                    )
                ),
                s012 =>
                    s012.s01.s1==s012.s01.s0&&s012.s2==s012.s01.s0&&s012.s2==s012.s01.s1
            )
        );
    }
    [TestMethod]
    public void Select_SelectMany_SelectMany_Where_WhereをJoin_Where_Where() {
        var S0 = new Set<int>();
        var S1 = new Set<int>();
        var S2 = new Set<int>();
        this.実行結果が一致するか確認(
            () => ExtensionSet.Where(
                ExtensionSet.Where(
                    ExtensionSet.SelectMany(
                        ExtensionSet.SelectMany(
                            S0,
                            s0 => ExtensionSet.Select(
                                S1,
                                s1 => new { s0,s1 }
                            )
                        ),
                        s01 => ExtensionSet.Select(
                            S2,
                            s2 => new { s01,s2 }
                        )
                    ),
                    s012 =>
                        s012.s01.s0==s012.s01.s1&&s012.s01.s0==s012.s2&&s012.s01.s1==s012.s2&&
                        s012.s01.s0>0&&s012.s01.s1>1&&s012.s2>2&&
                        s012.s01.s0!=s012.s01.s1&&s012.s01.s0!=s012.s2&&s012.s01.s1!=s012.s2
                ),
                s012 =>
                    s012.s01.s1==s012.s01.s0&&s012.s2==s012.s01.s0&&s012.s2==s012.s01.s1&&
                    0<s012.s01.s0&&1<s012.s01.s1&&2<s012.s2&&
                    s012.s01.s1!=s012.s01.s0&&s012.s2!=s012.s01.s0&&s012.s2!=s012.s01.s1
            )
        );
    }
    [TestMethod]
    public void JoinをOuterInnerOtherのWhereにする() {
        var S0 = new Set<int>();
        var S1 = new Set<int>();
        var S2 = new Set<int>();
        var S3 = new Set<int>();
        this.実行結果が一致するか確認(
            () => ExtensionSet.Where(
                ExtensionSet.Join(
                    ExtensionSet.Join(
                        S0,
                        S1,
                        s0 => s0,
                        s1 => s1,
                        (s0,s1) => new { s0,s1 }
                    ),
                    S2,
                    s01 => s01.s1,
                    s2 => s2,
                    (s01,s2) => new { s01,s2 }
                ),
                s012 =>
                    s012.s01.s1==1&&
                    s012.s2==2&&
                    s012.s01.s1==s012.s2
            )
        );
    }
    [TestMethod]
    public void Select_Where_SelectMany0() {
        var S0 = new Set<int>();
        var S1 = new Set<int>();
        var S2 = new Set<int>();
        this.実行結果が一致するか確認(
            () => ExtensionSet.SelectMany(
                S0,
                s0 => ExtensionSet.Where(
                    ExtensionSet.Select(
                        S1,
                        s1 => new { s0,s1 }
                    ),
                    s01 =>
                        s01.s0==s01.s0&&s01.s0==s01.s1&&s01.s1==s01.s1
                )
            )
        );
    }
    [TestMethod]
    public void Select_Where_SelectMany1() {
        var S0 = new Set<int>();
        var S1 = new Set<int>();
        this.実行結果が一致するか確認(
            () => ExtensionSet.SelectMany(
                S0,
                s0 => ExtensionSet.Where(
                    ExtensionSet.Select(
                        S1,
                        s1 => new { s0,s1 }
                    ),
                    s01 =>
                        s01.s0==s01.s0&&s01.s0==s01.s1&&s01.s1==s01.s1&&
                        s01.s0<=s01.s0&&s01.s0<=s01.s1&&s01.s1<=s01.s1&&
                        s01.s0!=s01.s0&&s01.s0!=s01.s1&&s01.s1!=s01.s1
                )
            )
        );
    }
    [TestMethod]
    public void SelectMany2をSelectMany3にする() {
        var S0 = new Set<int>();
        var S1 = new Set<int>();
        var x=this.実行結果が一致するか確認(
            () => ExtensionSet.SelectMany(
                S0,
                s0 => ExtensionSet.Select(
                    S1,
                    s1 => new { s0,s1 }
                )
            )
        );
    }
    [TestMethod]
    public void Select_Where_SelectMany_SelectMany0() {
        var S0 = new Set<int>();
        var S1 = new Set<int>();
        var S2 = new Set<int>();
        this.実行結果が一致するか確認(
            () => ExtensionSet.SelectMany(
                S0,
                s0 => ExtensionSet.SelectMany(
                    S1,
                    s1 => ExtensionSet.Where(
                        ExtensionSet.Select(
                            S2,
                            s2 => new { s01 = new { s0,s1 },s2 }
                        ),
                        s012 =>
                            s012.s01.s0==s012.s01.s1&&s012.s01.s0==s012.s2&&s012.s01.s1==s012.s2
                    )
                )
            )
        );
    }
    [TestMethod]
    public void Select_Where_SelectMany_SelectMany1() {
        var S0 = new Set<int>();
        var S1 = new Set<int>();
        var S2 = new Set<int>();
        this.実行結果が一致するか確認(
            () => ExtensionSet.SelectMany(
                S0,
                s0 => ExtensionSet.SelectMany(
                    S1,
                    s1 => ExtensionSet.Where(
                        ExtensionSet.Select(
                            S2,
                            s2 => new { s01 = new { s0,s1 },s2 }
                        ),
                        s012 =>
                            s012.s01.s0==s012.s01.s1&&s012.s01.s0==s012.s2&&s012.s01.s1==s012.s2&&
                            s012.s01.s0>0&&s012.s01.s1>1&&s012.s2>2&&
                            s012.s01.s0!=s012.s01.s1&&s012.s01.s0!=s012.s2&&s012.s01.s1!=s012.s2
                    )
                )
            )
        );
    }
    [TestMethod]
    public void Join3() {
        var S0 = new Set<int>();
        var S1 = new Set<int>();
        var S2 = new Set<int>();
        var S3 = new Set<int>();
        //S0.SelectMany(
        //    s0 =>S1.Select(
        //        s1 =>{s0,s1}
        //    ).Where(s012=>)
        //).SelectMany(
        //    s01=>S2.Where(
        //        s2 =>
        //    ).Select(
        //        s2=>{s01,s2}
        //    ).Where(s012=>)
        //)
        this.実行結果が一致するか確認(
            () => ExtensionSet.SelectMany(
                ExtensionSet.SelectMany(
                    S0,
                    s0 => ExtensionSet.Where(
                        ExtensionSet.Select(
                            S1,
                            s1 => new { s0,s1 }
                        ),
                        s01 =>
                            s01.s0==s01.s1&&
                            s01.s0>0&&s01.s1>1&&
                            s01.s0!=s01.s1
                    )
                ),
                s01 => ExtensionSet.Where(
                    ExtensionSet.Select(
                        ExtensionSet.Where(
                            S2,
                            s2 =>
                                s01.s0==s2&&s01.s1==s2&&
                                s2>2&&
                                s01.s0!=s2&&s01.s1!=s2
                        ),
                        s2 => new { s01,s2 }
                    ),
                    s012 =>
                        s012.s01.s0==s012.s2&&
                        s012.s01.s0!=s012.s2
                )
            )
        );
        //S0.SelectMany(
        //    s0 =>S1.Select(
        //        s1 =>{s0,s1}
        //    ).Where(s012=>)
        //).SelectMany(
        //    s01=>S2.Select(
        //        s2=>{s01,s2}
        //    ).Where(s012=>)
        //)
        this.実行結果が一致するか確認(
            () => ExtensionSet.SelectMany(
                ExtensionSet.SelectMany(
                    S0,
                    s0 => ExtensionSet.Where(
                        ExtensionSet.Select(
                            S1,
                            s1 => new { s0,s1 }
                        ),
                        s01 =>
                            s01.s0==s01.s1&&
                            s01.s0>0&&s01.s1>1&&
                            s01.s0!=s01.s1
                    )
                ),
                s01 => ExtensionSet.Where(
                    ExtensionSet.Select(
                        S2,
                        s2 => new { s01,s2 }
                    ),
                    s012 =>
                        s012.s01.s0==s012.s2&&s012.s01.s1==s012.s2&&
                        s012.s2>2&&
                        s012.s01.s0!=s012.s2&&s012.s01.s1!=s012.s2
                )
            )
        );
        //S0.SelectMany(
        //    s0 =>S1.Select(
        //        s1 =>{s0,s1}
        //    )
        //).SelectMany(
        //    s01=>S2.Select(
        //        s2=>{s01,s2}
        //    ).Where(s012=>)
        //)
        this.実行結果が一致するか確認(
            () => ExtensionSet.SelectMany(
                ExtensionSet.SelectMany(
                    S0,
                    s0 => ExtensionSet.Select(
                        S1,
                        s1 => new { s0,s1 }
                    )
                ),
                s01 => ExtensionSet.Where(
                    ExtensionSet.Select(
                        S2,
                        s2 => new { s01,s2 }
                    ),
                    s012 =>
                        s012.s01.s0==s012.s01.s1&&s012.s01.s0==s012.s2&&s012.s01.s1==s012.s2&&
                        s012.s01.s0>0&&s012.s01.s1>1&&s012.s2>2&&
                        s012.s01.s0!=s012.s01.s1&&s012.s01.s0!=s012.s2&&s012.s01.s1!=s012.s2
                )
            )
        );
        //S0.SelectMany(
        //    s0 =>S1.Select(
        //        s1 =>{s0,s1}
        //    )
        //).SelectMany(
        //    s01=>S2.Select(
        //        s2=>{s01,s2}
        //    )
        //).Where(s012=>)
        this.実行結果が一致するか確認(
            () => ExtensionSet.Where(
                ExtensionSet.SelectMany(
                    ExtensionSet.SelectMany(
                        S0,
                        s0 => ExtensionSet.Select(
                            S1,
                            s1 => new { s0,s1 }
                        )
                    ),
                    s01 => ExtensionSet.Select(
                        S2,
                        s2 => new { s01,s2}
                    )
                ),
                s012 =>
                    s012.s01.s0==s012.s01.s1&&s012.s01.s0==s012.s2&&s012.s01.s1==s012.s2&&
                    s012.s01.s0>0&&s012.s01.s1>1&&s012.s2>2&&
                    s012.s01.s0!=s012.s01.s1&&s012.s01.s0!=s012.s2&&s012.s01.s1!=s012.s2
            )
        );
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.Select(s1 => new { s0,s1 }).Where(
                    s01 =>
                        s01.s0==s01.s1&&s01.s1==s01.s0&&
                        s01.s0==0&&0==s01.s0&&
                        s01.s1==1&&1==s01.s1&&
                        s01.s0!=s01.s1&&s01.s1!=s01.s1
                )
            )
        );
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.Select(s1 => new { s0,s1 })
            ).Where(
                s01 =>
                    s01.s0==s01.s1&&s01.s1==s01.s0&&
                    s01.s0==0&&0==s01.s0&&
                    s01.s1==1&&1==s01.s1&&
                    s01.s0!=s01.s1&&s01.s1!=s01.s1
            )
        );
        this.実行結果が一致するか確認(
            () => ExtensionSet.Where(
                ExtensionSet.SelectMany(
                    ExtensionSet.SelectMany(
                        S0,
                        s0 => ExtensionSet.Select(
                            S1,
                            s1 => new { s0_0 = s0+0,s0_1 = s0+1,s1_0 = s1+0,s1_1 = s1+1 }
                        )
                    ),
                    s01 => ExtensionSet.Select(
                        S2,
                        s2 => new { s01,s2_0 = s2+0,s2_1 = s2+1 }
                    )
                ),
                s012 =>
                    s012.s01.s0_0==s012.s01.s1_0&&
                    s012.s01.s1_0>0&&s012.s2_0>1&&
                    s012.s01.s0_0!=s012.s01.s1_0
            )
        );
        this.実行結果が一致するか確認(
            () => ExtensionSet.Where(
                ExtensionSet.SelectMany(
                    ExtensionSet.SelectMany(
                        S0,
                        s0 => ExtensionSet.Select(
                            S1,
                            s1 => new { s0_0=s0+0,s0_1=s0+1,s1_0=s1+0,s1_1=s1+1 }
                        )
                    ),
                    s01 => ExtensionSet.Select(
                        S2,
                        s2 => new { s01,s2_0=s2+0,s2_1=s2+1 }
                    )
                ),
                s012 => 
                    s012.s01.s0_0==s012.s01.s1_0&&s012.s01.s0_0==s012.s2_0&&
                    s012.s01.s1_0>0&&s012.s2_0>1&&
                    s012.s01.s0_0!=s012.s01.s1_0&&s012.s01.s0_0!=s012.s2_0
            )
        );
        this.実行結果が一致するか確認(
            () => ExtensionSet.Where(
                ExtensionSet.SelectMany(
                    ExtensionSet.SelectMany(
                        S0,
                        s0 => ExtensionSet.Select(
                            S1,
                            s1 => new { s0,s1 }
                        )
                    ),
                    s01 => ExtensionSet.Select(
                        S2,
                        s2 => new { s01,s2 }
                    )
                ),
                s012 => s012.s01.s0==s012.s01.s1&&s012.s01.s0==s012.s2&&s012.s01.s1==s012.s2
            )
        );
        this.実行結果が一致するか確認(
            () => ExtensionSet.Where(
                ExtensionSet.SelectMany(
                    ExtensionSet.Where(
                        ExtensionSet.SelectMany(
                            S0,
                            s0 => ExtensionSet.Select(
                                S1,s1 => new { s0,s1 }
                            )
                        ),
                        s01 => s01.s0==s01.s1
                    ),
                    s01 => ExtensionSet.Select(
                        S2,
                        s2 => new { s01,s2 }
                    )
                ),
                s012 => s012.s01.s0==s012.s2
            )
        );
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.Select(s1 => new { s0,s1 }).Where(
                    w => 
                        w.s0==w.s1&&w.s1==w.s0&&
                        w.s0==0&&0==w.s0&&
                        w.s1==1&&1==w.s1&&
                        w.s0!=w.s1&&w.s1!=w.s1
                )
            )
        );
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.Select(s1 => new { s0,s1 }).Where(w => w.s0==w.s1)
            )
        );
        //this.AssertExecute(
        //    () => ExtendSet.Where(
        //        ExtendSet.SelectMany(
        //            ExtendSet.Where(
        //                ExtendSet.SelectMany(
        //                    S0,
        //                    s0 => S1.Select(s1 => new { s0,s1 })
        //                ),
        //                s01 => s01.s0==s01.s1&&s01.s1==s01.s0
        //            ),
        //            s01 => S2.Select(s2 => new { s01,s2 })
        //        ),
        //        s012 =>
        //            s012.s01.s1==1&&
        //            s012.s2==2//&&
        //            //s012.s01.s0==s012.s01.s1&&
        //            //s012.s01.s1==s012.s01.s0&&
        //            //s012.s01.s0==s012.s2&&
        //            //s012.s2==s012.s01.s0&&
        //            //s012.s01.s1==s012.s2&&
        //            //s012.s2==s012.s01.s1
        //    )
        //);
        this.実行結果が一致するか確認(
            () => ExtensionSet.Where(
                ExtensionSet.Join(
                    ExtensionSet.Join(
                        S0,
                        S1,
                        s0 => s0,
                        s1 => s1,
                        (s0,s1) => new { s0,s1 }
                    ),
                    S2,
                    s01 => s01.s1,
                    s2 => s2,
                    (s01,s2) => new { s01,s2 }
                ),
                s012 =>
                    s012.s01.s0==0&&
                    s012.s01.s1==1&&
                    s012.s2==2&&
                    s012.s01.s0==s012.s01.s1&&
                    s012.s01.s0==s012.s2&&
                    s012.s01.s1==s012.s2&&
                    0==s012.s01.s0&&
                    1==s012.s01.s1&&
                    2==s012.s2&&
                    s012.s01.s1==s012.s01.s0&&
                    s012.s2==s012.s01.s0&&
                    s012.s2==s012.s01.s1
            )
        );
        //this.AssertExecute(
        //    () => ExtendSet.Join(
        //        ExtendSet.Join(
        //            S0,
        //            S1,
        //            s0 => s0,
        //            s1 => s1,
        //            (s0,s1) => new { s0,s1 }
        //        ),
        //        ExtendSet.Join(
        //            S2,
        //            S3,
        //            s2 => s2,
        //            s3 => s3,
        //            (s2,s3) => new { s2,s3 }
        //        ),
        //        s01 => s01.s1,
        //        s23 => s23.s2,
        //        (s01,s23) => new { s01,s23 }
        //    )
        //);
        //this.AssertExecute(
        //    () => ExtendSet.Where(
        //        ExtendSet.SelectMany(
        //            S0,
        //            s0 => S1.Select(s1 => new { s0,s1 })
        //        ),
        //        s01 => s01.s0==s01.s1&&s01.s1==s01.s0
        //    )
        //);
        this.実行結果が一致するか確認(
            () => ExtensionSet.Where(
                ExtensionSet.SelectMany(
                    S0,
                    s0=>ExtensionSet.SelectMany(
                        S1.Select(s1 => new { s0,s1 }),
                        s01 => S2.Select(s2 => new { s01,s2 })
                    )
                ),
                s012 => s012.s01.s0==s012.s01.s1&&s012.s01.s0==s012.s2&&s012.s01.s1==s012.s2
            )
        );
        this.実行結果が一致するか確認(
            () => ExtensionSet.Where(
                ExtensionSet.Join(
                    S0,
                    ExtensionSet.Join(
                        S1,
                        S2,
                        s1 => s1,
                        s2 => s2,
                        (s1,s2) => new { s1,s2 }
                    ),
                    s0 => s0,
                    s12 => s12.s1,
                    (s0,s12) => new { s0,s12 }
                ),
                s012 =>
                    s012.s0==0&&
                    s012.s12.s1==1&&
                    s012.s12.s2==2&&
                    s012.s0==s012.s0&&
                    s012.s0==s012.s12.s1&&
                    s012.s12.s1==s012.s12.s2&&
                    0==s012.s0&&
                    1==s012.s12.s1&&
                    2==s012.s12.s2&&
                    s012.s0==s012.s0&&
                    s012.s12.s1==s012.s0&&
                    s012.s12.s2==s012.s12.s1
            )
        );
        //this.AssertExecute(
        //    () => S0.Select(
        //        s0 => new { s0 }
        //    ).Where(
        //        w => w.s0==0
        //    )
        //);
        this.実行結果が一致するか確認(
            () => S0.Join(
                S1,
                s0 => s0,
                s1 => s1,
                (s0,s1) => new { s0,s1 }
            ).Where(
                s01 =>
                    s01.s0==0&&
                    s01.s1==1&&
                    s01.s0==s01.s1&&
                    0==s01.s0&&
                    1==s01.s1&&
                    s01.s1==s01.s0
            ).Select(
                s01 => new { s01.s0,s01.s1 }
            )
        );
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1,
                (s0,s1) => new { s0,s1 }
            ).SelectMany(
                s01 => S2,(s01,s2) => new { s01,s2 }).Where(s012 => s012.s01.s0==s012.s2
            ).Select(
                s012 => new { s012.s01.s0,s012.s01.s1,s012.s2 }
            )
        );
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.Select(s1 => new { s0,s1 })
            ).SelectMany(
                s01 => S2.Select(s2 => new { s01,s2 })
            ).Where(s012 => s012.s01.s0==s012.s2
            ).Select(
                s012 => new { s012.s01.s0,s012.s01.s1,s012.s2 }
            )
        );
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.Where(w1 => w1==s0).Select(s1 => new { s0,s1 })
            )
        );
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.SelectMany(
                    s1=>S2.Where(w2 => w2==s1).Select(s2 => new { s0,s1,s2 })
                )
            )
        );
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.Select(s1 => new { s0,s1 })
            ).Where(
                w => w.s0==w.s1
            )
        );
        this.実行結果が一致するか確認(
            ()=>S0.Join(
                S1,
                s0 => s0,
                s1 => s1,
                (s0,s1) => new { s0,s1 }
            )
        );
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1,
                (s0,s1) => new { s0,s1 }
            ).Select(
                s01 => new { s01.s0,s01.s1 }
            ).Where(
                w => w.s0==w.s1
            )
        );
        //() => S0.SelectMany(
        //    s0 => S1.Select(s1 => new { s0,s1 })
        //).Where(
        //    w => w.s0==w.s1
        //).Select(
        //    s01 => new { s01.s0,s01.s1 }
        //)
        //S0.Join(
        //    S1,
        //    s0=>s0,
        //    s1=>s1,
        //    (s0,s1)=>new{s0,s1}
        //)
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.Select(s1 => new { s0,s1 }).Where(w => w.s0==w.s1)
            ).Select(
                s01 => new { s01.s0,s01.s1 }
            )
        );
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1,
                (s0,s1) => new { s0,s1 }
            ).Where(
                w => w.s0==w.s1
            ).Select(
                s01 => new { s01.s0,s01.s1}
            )
        );
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1,
                (s0,s1) => new { s0,s1 }
            ).Select(
                s01 => new { s01.s0,s01.s1 }
            ).Where(
                w => w.s0==w.s1
            )
        );
        //S0.Join(
        //    S1,
        //    s0=>s0,
        //    s1=>s1,
        //    (s0,s1)=>new{s0,s1}
        //).Join(
        //    S2,
        //    s01=>s01.s1,
        //    s2=>s2
        //    (s01,s2)=>new{s01,s2}
        //)
        //this.AssertExecute(
        //    () => (from s0 in S0
        //           join s1 in S1 on s0 equals s1
        //           join s2 in S2 on s1 equals s2
        //           select new { s0,s1,s2 })
        //);
        this.実行結果が一致するか確認(
            () => from s0 in S0
                from s1 in S1
                from s2 in S2
                where s0==s2
                select new { s0,s1,s2 }
        );
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.Where(
                    s1_w => s0==s1_w
                ).Select(
                    s1_s => new { s0,s1 = s1_s }
                )
            )
        );
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.Where(
                    s1_w => s0==s1_w
                ).Select(
                    s1_s => new { s0,s1 = s1_s }
                )
            ).SelectMany(
                s01 => S2.Where(
                    s2_w => s2_w==s01.s1
                ).Select(
                    s2_s => new { s01,s2 = s2_s }
                )
            )
        );
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.Select(
                    s1 => new { s0,s1 }
                )
            ).Where(
                lr => lr.s0==lr.s1
            ).SelectMany(
                s01 => S2.Select(
                    s2 => new { s01,s2 }
                )
            ).Where(
                lr => lr.s01.s1==lr.s2
            )
        );
        /*
        this.AssertExecute(
            () => S0.SelectMany(
                s0 => S1.Select(
                    s1 => new { s0,s1 }
                ).Where(
                    lr => lr.s0==lr.s1
                )
            ).SelectMany(
                s01 => S2.Select(
                    s2 => new { s01,s2 }
                ).Where(
                    lr => lr.s01.s1==lr.s2
                )
            )
        );
        */
        //S0.Join(
        //    S1,Join(
        //        S2
        //        s1=>s1,
        //        s2=>s2,
        //        (s1,s2)=>new{s1,s2}
        //    ),
        //    s0=>s0,
        //    s12=>s12.s1,
        //    (s0,s12)=>new{s0,s12}
        //)
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.SelectMany(
                    s1 => S2.Select(
                        s2 => new { s1,s2 }
                    )
                ).Where(
                    lr => lr.s1==lr.s2
                ).Select(s12 =>
                    new { s0,s12 }
                )
            ).Where(
                lr => lr.s0==lr.s12.s1
            )
        );
        this.実行結果が一致するか確認(
            () => S0.SelectMany(
                s0 => S1.SelectMany(
                    s1 => S2.Select(
                        s2 => new { s1,s2 }
                    )
                ).Select(s12 =>
                    new { s0,s12 }
                )
            ).Where(
                lr => lr.s0==lr.s12.s1&&lr.s12.s1==lr.s12.s2
            )
        );
    }
}