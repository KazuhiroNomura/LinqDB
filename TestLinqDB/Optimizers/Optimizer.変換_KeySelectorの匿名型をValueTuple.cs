﻿using System.Linq.Expressions;
using LinqDB.Sets;
using Expression = System.Linq.Expressions.Expression;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
namespace TestLinqDB.Optimizers;
public class 変換_KeySelectorの匿名型をValueTuple:共通{
    [Fact]public void Block(){
        this.ExpressionAssertEqual(
            Expression.Lambda<Action>(
                Expression.Block(
                )
            )
        );
        this.ExpressionAssertEqual(
            Expression.Lambda<Action>(
                Expression.Block(
                    Expression.Default(typeof(void))
                )
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    Expression.Default(typeof(int))
                )
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    Expression.Constant(0)
                )
            )
        );
        var p=Expression.Parameter(typeof(int));
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[]{p},
                    Expression.Assign(p,Expression.Constant(0))
                )
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[]{p},
                    Expression.Assign(p,Expression.Constant(0)),
                    p
                )
            )
        );
    }
    static T F<T>(T t)=>t;
    static LambdaExpression L<T,TResult>(Expression<Func<T,TResult>> i)=>i;
    static Func<T,TResult> Anonymous<T,TResult>(Func<T,TResult> i)=>i;
    [Fact]public void Call(){
        var s=new Set<int>();
        this.Expression実行AssertEqual(() => s.ToString());
        this.Expression実行AssertEqual(() => s.Join(s,o => new { key = o },i => new { key = i },(o,i) => o+i));
        this.Expression実行AssertEqual(() => s.Join(s,o => new { key0 = o,key1 = o },i => new { key0 = i,key1 = i },(o,i) => o+i));
        this.Expression実行AssertEqual(() => s.Join(s,o => new { key = o },i => F(new { key = i }),(o,i) => o+i));
        this.Expression実行AssertEqual(() => s.Join(s,o => F(new { key = o }),i => new { key = i },(o,i) => o+i));
        this.Expression実行AssertEqual(() => s.Join(s,o => F(new { key = o }),i => F(new { key = i }),(o,i) => o+i));
        this.Expression実行AssertEqual(() => s.Join(s,o => new { key = o },Anonymous((int i) => new { key = i }),(o,i) => o+i));
        this.Expression実行AssertEqual(() => s.Join(s,Anonymous((int o) => new { key = o }),i => new { key = i },(o,i) => o+i));
        this.Expression実行AssertEqual(() => s.Join(s,Anonymous((int o) => new { key = o }),Anonymous((int i) => new { key = i }),(o,i) => o+i));
        this.Expression実行AssertEqual(() => s.Join(s,o => o,i => i,(o,i) => o+i));
        this.Expression実行AssertEqual(()=>s.Union(s));
    }
}
