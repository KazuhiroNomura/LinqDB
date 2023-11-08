using System.Linq.Expressions;
using System.Reflection;

using LinqDB.Sets;
//using System.Reflection;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable All
namespace TestLinqDB.Optimizers;
public class 変換_跨ぎParameterの不要置換復元:共通{
    private delegate void delegate_refint(ref int input);
    private static void refint(ref int input,Action d)=>
        d();
    [Fact]public void Assign(){
        var f= BindingFlags.Static|BindingFlags.NonPublic;
        var ref_p = Expression.Parameter(typeof(int).MakeByRefType(), "ref_p");
        var int_refint_intFuncRef=typeof(変換_跨ぎParameterの不要置換復元).GetMethod(nameof(変換_跨ぎParameterの不要置換復元.int_refint),f)!;
        var int_refint=typeof(変換_跨ぎParameterの不要置換復元).GetMethod(nameof(変換_跨ぎParameterの不要置換復元.int_refint),f)!;
        var input = Expression.Parameter(typeof(int), "p");
        //()=>{var p;int_Lambda0_refint_intFuncRef(
        this.ExpressionAssertEqual(
            Expression.Lambda<Action>(
                Expression.Block(
                    new[]{input},
                    Expression.Call(
                        int_refint,
                        input,
                        Expression.Lambda<delegate_int_refint>(
                            Expression.Assign(
                                ref_p,
                                Expression.Constant(4)
                            ),
                            ref_p
                        )
                    )
                )
            )
        );
    }
    private delegate int delegate_int_refint(ref int input);
    private static int int_refint(ref int input, delegate_int_refint d) => d(ref input);
    [Fact]
    public void Ref1(){
        var f= BindingFlags.Static|BindingFlags.NonPublic;
        var in_p=Expression.Parameter(typeof(int),"p");
        var ref_p=Expression.Parameter(typeof(int).MakeByRefType(),"ref_p");
        var int_Lambda0_refint_intFuncRef=typeof(変換_跨ぎParameterの不要置換復元).GetMethod(nameof(変換_跨ぎParameterの不要置換復元.int_refint),f)!;
        var int_Lambda_refint=typeof(変換_跨ぎParameterの不要置換復元).GetMethod(nameof(変換_跨ぎParameterの不要置換復元.int_refint),f)!;
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[]{in_p},
                    Expression.Assign(
                        in_p,
                        Expression.Constant(0)
                    ),
                    Expression.Call(
                        int_Lambda_refint,
                        in_p,
                        Expression.Lambda<delegate_int_refint>(
                            Expression.Add(
                                in_p,
                                in_p
                            ),
                            ref_p
                        )
                    )
                )
            )
        );
    }
    [Fact]public void Ref2(){
        var f= BindingFlags.Static|BindingFlags.NonPublic;
        var ref_p = Expression.Parameter(typeof(int).MakeByRefType(), "ref_p");
        var Lambda0=typeof(変換_跨ぎParameterの不要置換復元).GetMethod(nameof(int_refint),f)!;
        var Int32_Lambda_ref_Int32=typeof(変換_跨ぎParameterの不要置換復元).GetMethod(nameof(変換_跨ぎParameterの不要置換復元.int_refint),f)!;
        var p = Expression.Parameter(typeof(int), "p");
        this.ExpressionAssertEqual(
            Expression.Lambda<Action>(
                Expression.Block(
                    new[]{p},
                    Expression.Call(
                        Lambda0,
                        p,
                        Expression.Lambda<delegate_int_refint>(
                            Expression.Call(
                                Lambda0,
                                ref_p,
                                Expression.Lambda<delegate_int_refint>(
                                    Expression.Assign(
                                        ref_p,
                                        Expression.Constant(4)
                                    ),
                                    ref_p
                                )
                            ),
                            ref_p
                        )
                    )
                )
            )
        );
    }
    [Fact]public void Invoke(){
        this.Expression実行AssertEqual(() =>((Func<int,int>)((int a)=>a+a))(2));
    }
    [Fact]public void Lambda(){
        var s = new Set<int>();
        this.Expression実行AssertEqual(() => s.Select(p=>p+1));
    }
    [Fact]public void Traverse(){
        var f= BindingFlags.Static|BindingFlags.NonPublic;
        var ref_p = Expression.Parameter(typeof(int).MakeByRefType(), "ref_p");
        var Lambda0=typeof(変換_跨ぎParameterの不要置換復元).GetMethod(nameof(int_refint),f)!;
        var Int32_Lambda_ref_Int32=typeof(変換_跨ぎParameterの不要置換復元).GetMethod(nameof(変換_跨ぎParameterの不要置換復元.int_refint),f)!;
        var p = Expression.Parameter(typeof(int), "p");
        this.ExpressionAssertEqual(
            Expression.Lambda<Action>(
                Expression.Block(
                    new[]{p},
                    Expression.Call(
                        Lambda0,
                        p,
                        Expression.Lambda<delegate_int_refint>(
                            Expression.Call(
                                Lambda0,
                                ref_p,
                                Expression.Lambda<delegate_int_refint>(
                                    Expression.Assign(
                                        ref_p,
                                        Expression.Constant(4)
                                    ),
                                    ref_p
                                )
                            ),
                            ref_p
                        )
                    )
                )
            )
        );
    }
    //var st=new Set<TestLinqDB.Sets.Key,Sets.Value>{new(new(0)),new(new(1))};
    //this.共通MemoryMessageJson_Expression_ExpressionAssertEqual(()=>st.SelectMany(o=>st).Where(i=>i.Key.メンバー.value==0));
    //this.共通MemoryMessageJson_Expression_ExpressionAssertEqual(()=>st.SelectMany(o=>st.Where(i=>i.Key.メンバー.value==0&& o.Equals(i))));
    //this.共通MemoryMessageJson_Expression_ExpressionAssertEqual(()=>st.Where(p=>p.Key.Equals(new Sets.Key(new(0)))));
}
