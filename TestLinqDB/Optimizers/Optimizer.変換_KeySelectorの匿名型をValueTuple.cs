using LinqDB.Sets;
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
    [Fact]public void Call(){
        {
            var s=new Set<int>();
            //s.Select(i=>i);
            this.Optimizer.IsInline=true;
            //this.共通コンパイル実行(()=>s.Select(i=>i));

            this.共通コンパイル実行(()=>s.Join(s,o=>o,i=>i,(o,i)=>o+i));
            //this.共通コンパイル実行(()=>s.Join(s,o=>o,i=>i,(o,i)=>new{o,i}));
        }
    }
}
