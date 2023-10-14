using System.Linq.Expressions;
using LinqDB.Sets;
using Microsoft.CSharp.RuntimeBinder;
//using System.Reflection;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable All
namespace TestLinqDB.Optimizers;
public class 変換_跨ぎParameterの不要置換復元:共通{
    private delegate int FuncRef(ref int input);
    private static int Lambda0(ref int input, FuncRef d)
    {
        return d(ref input);
    }
    private delegate int Int32_Delegate_ref_Int32(ref int input);
    private static int Int32_Lambda_ref_Int32(ref int input, Int32_Delegate_ref_Int32 d) => d(ref input);
    [Fact]
    public void Ref1(){
        var f=System.Reflection.BindingFlags.Static|System.Reflection.BindingFlags.NonPublic;
        var in_p=Expression.Parameter(typeof(int),"p");
        var ref_p=Expression.Parameter(typeof(int).MakeByRefType(),"ref_p");
        var Lambda0=typeof(変換_跨ぎParameterの不要置換復元).GetMethod(nameof(変換_跨ぎParameterの不要置換復元.Lambda0),f)!;
        var Int32_Lambda_ref_Int32=typeof(変換_跨ぎParameterの不要置換復元).GetMethod(nameof(変換_跨ぎParameterの不要置換復元.Int32_Lambda_ref_Int32),f)!;
        this.共通コンパイル実行(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[]{in_p},
                    Expression.Assign(
                        in_p,
                        Expression.Constant(0)
                    ),
                    Expression.Call(
                        Int32_Lambda_ref_Int32,
                        in_p,
                        Expression.Lambda<Int32_Delegate_ref_Int32>(
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
        var f=System.Reflection.BindingFlags.Static|System.Reflection.BindingFlags.NonPublic;
        var ref_p = Expression.Parameter(typeof(int).MakeByRefType(), "ref_p");
        var Lambda0=typeof(変換_跨ぎParameterの不要置換復元).GetMethod(nameof(変換_跨ぎParameterの不要置換復元.Lambda0),f)!;
        var Int32_Lambda_ref_Int32=typeof(変換_跨ぎParameterの不要置換復元).GetMethod(nameof(変換_跨ぎParameterの不要置換復元.Int32_Lambda_ref_Int32),f)!;
        var p = Expression.Parameter(typeof(int), "p");
       // var p1 = Expression.Parameter(typeof(int).MakeByRefType(), "p1");
       // var p2 = Expression.Parameter(typeof(int).MakeByRefType(), "p2");
        this.共通コンパイル実行(
            Expression.Lambda<Action>(
                Expression.Block(
                    new[]{p},
                    Expression.Call(
                        Lambda0,
                        p,
                        Expression.Lambda<FuncRef>(
                            Expression.Call(
                                Lambda0,
                                ref_p,
                                Expression.Lambda<FuncRef>(
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
        //this.共通コンパイル実行(
        //    Expression.Lambda<Func<int>>(
        //        Expression.Block(
        //            new[] { p },
        //            Expression.Call(
        //                Lambda0,
        //                p,
        //                Expression.Lambda<FuncRef>(
        //                    Expression.Call(
        //                        Lambda0,
        //                        p1,
        //                        Expression.Lambda<FuncRef>(
        //                            Expression.Assign(
        //                                p2,
        //                                Expression.Constant(4)
        //                            ),
        //                            p2
        //                        )
        //                    ),
        //                    p1
        //                )
        //            )
        //        )
        //    )
        //);
    }
    [Fact]public void Lambda(){
        var s = new Set<int>();
        this.共通コンパイル実行(() => s.Select(p=>p+1));
    }
    //var st=new Set<TestLinqDB.Sets.Key,Sets.Value>{new(new(0)),new(new(1))};
    //this.共通コンパイル実行(()=>st.SelectMany(o=>st).Where(i=>i.Key.メンバー.value==0));
    //this.共通コンパイル実行(()=>st.SelectMany(o=>st.Where(i=>i.Key.メンバー.value==0&& o.Equals(i))));
    //this.共通コンパイル実行(()=>st.Where(p=>p.Key.Equals(new Sets.Key(new(0)))));
}
