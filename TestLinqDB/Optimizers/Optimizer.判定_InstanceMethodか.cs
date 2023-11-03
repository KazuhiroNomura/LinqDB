﻿using System.Linq.Expressions;
using LinqDB.Sets;
//using System.Reflection;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable All
namespace TestLinqDB.Optimizers;
public class 判定_InstanceMethodか:共通{
    private static int F(Expression<Func<int>> f){
        var m=f.Compile();
        return m();
    }
    [Fact]public void Quote(){
        //this.MemoryMessageJson_Expression_ExpressionAssertEqual(
        //    Expression.Lambda<Func<int>>(
        //        Expression.Call(
        //            typeof(判定_InstanceMethodか).GetMethod(nameof(F),BindingFlags.NonPublic|BindingFlags.Static)!,
        //            Expression.Quote(
        //                Expression.Lambda<Func<int>>(
        //                    Expression.Constant(3)
        //                )
        //            )
        //        )
        //    )
        //);
        //this.MemoryMessageJson_Expression_ExpressionAssertEqual(()=>F(()=>3));
        this.Expression実行AssertEqual(()=>F(()=>3));
    }
    [Fact]public void Block(){
        var p=Expression.Parameter(typeof(int));
        var s = new Set<int>();
        //var 判定_内部LambdaにParameterが存在するか=this._判定_内部LambdaにParameterが存在するか;
        //var Dictionaryラムダ跨ぎParameter=this.Dictionaryラムダ跨ぎParameter;
        //foreach(var Variable in Block.Variables)
        //    if(判定_内部LambdaにParameterが存在するか.実行(Block,Variable))
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[]{p},
                    Expression.Lambda<Func<int>>(p),
                    p
                )
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[]{p},
                    Expression.Lambda<Func<int>>(Expression.Constant(0)),
                    p
                )
            )
        );
    }
    //var st=new Set<TestLinqDB.Sets.Key,Sets.Value>{new(new(0)),new(new(1))};
    //this.共通MemoryMessageJson_Expression_ExpressionAssertEqual(()=>st.SelectMany(o=>st).Where(i=>i.Key.メンバー.value==0));
    //this.共通MemoryMessageJson_Expression_ExpressionAssertEqual(()=>st.SelectMany(o=>st.Where(i=>i.Key.メンバー.value==0&& o.Equals(i))));
    //this.共通MemoryMessageJson_Expression_ExpressionAssertEqual(()=>st.Where(p=>p.Key.Equals(new Sets.Key(new(0)))));
}
