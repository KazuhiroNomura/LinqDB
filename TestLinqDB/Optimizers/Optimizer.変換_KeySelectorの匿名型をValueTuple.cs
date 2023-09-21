using System.Linq.Expressions;
using LinqDB.Sets;
using Microsoft.Build.Execution;
using Serializers.MessagePack.Formatters;
using Expression = System.Linq.Expressions.Expression;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
namespace Optimizers;
public class 変換_KeySelectorの匿名型をValueTuple:共通{
    [Fact]public void Block(){
        this.共通コンパイル実行(
            Expression.Lambda<Action>(
                Expression.Block(
                )
            )
        );
        this.共通コンパイル実行(
            Expression.Lambda<Action>(
                Expression.Block(
                    Expression.Default(typeof(void))
                )
            )
        );
        this.共通コンパイル実行(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    Expression.Default(typeof(int))
                )
            )
        );
        this.共通コンパイル実行(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    Expression.Constant(0)
                )
            )
        );
        var p=Expression.Parameter(typeof(int));
        this.共通コンパイル実行(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[]{p},
                    Expression.Assign(p,Expression.Constant(0))
                )
            )
        );
        this.共通コンパイル実行(
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
    //static T Anonymous<T>(T t)=>F(new{key=t});
    static LambdaExpression L<T,TResult>(Expression<Func<T,TResult>> i)=>i;
    static Func<T,TResult> Anonymous<T,TResult>(Func<T,TResult> i)=>i;
    [Fact]public void Call(){
        var s=new Set<int>();
        this.共通コンパイル実行(() => s.ToString());
        this.共通コンパイル実行(() => s.Join(s,o => new { key = o },i => new { key = i },(o,i) => o+i));
        this.共通コンパイル実行(() => s.Join(s,o => new { key0 = o,key1 = o },i => new { key0 = i,key1 = i },(o,i) => o+i));
        this.共通コンパイル実行(() => s.Join(s,o => new { key = o },i => F(new { key = i }),(o,i) => o+i));
        this.共通コンパイル実行(() => s.Join(s,o => F(new { key = o }),i => new { key = i },(o,i) => o+i));
        this.共通コンパイル実行(() => s.Join(s,o => F(new { key = o }),i => F(new { key = i }),(o,i) => o+i));
        this.共通コンパイル実行(() => s.Join(s,o => new { key = o },Anonymous((int i) => new { key = i }),(o,i) => o+i));
        this.共通コンパイル実行(() => s.Join(s,Anonymous((int o) => new { key = o }),i => new { key = i },(o,i) => o+i));
        this.共通コンパイル実行(() => s.Join(s,Anonymous((int o) => new { key = o }),Anonymous((int i) => new { key = i }),(o,i) => o+i));
        this.共通コンパイル実行(() => s.Join(s,o => o,i => i,(o,i) => o+i));
        this.共通コンパイル実行(()=>s.Union(s));
    }
}
