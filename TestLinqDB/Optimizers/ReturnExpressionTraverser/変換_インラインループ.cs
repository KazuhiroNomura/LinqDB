using System.Linq.Expressions;
using LinqDB.Sets;
//using System.Reflection;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable All
namespace TestLinqDB.Optimizers.ReturnExpressionTraverser;
public class 変換_インラインループ : 共通{
    protected override テストオプション テストオプション{get;}=テストオプション.最適化;
    public 変換_インラインループ(){
        this.Optimizer.IsInline=true;
    }
    private void Cover(Expression Expression){
        this.Optimizer.Lambda最適化(
            Expression.Lambda(Expression)
        );
    }
    [Fact]public void AddAssign(){
        var s = new int[]{1,2,3,4,5,6,7};
        this.Cover(() => s.Average(o => o+1));
        //var p = Expression.Parameter(typeof(decimal));
        //this.Cover(
        //    Expression.Block(
        //        new[]{p},
        //        Expression.AddAssign(
        //            p,
        //            Expression.Constant(0m)
        //        )
        //    )
        //);
    }
    [Fact]public void Block_PreIncrementAssign_AddAssign(){
        var s = new int[]{1,2,3,4,5,6,7};
        this.Cover(() => s.Average(o => o+1));
    }
    [Fact]public void Call(){
        var s = new int[]{1,2,3,4,5,6,7}.ToSet();
        this.Cover(() => s.Avedev(o => o+1));
    }
    [Fact]public void Field(){
        var s = new int[]{1,2,3,4,5,6,7}.ToSet();
        this.Cover(() => s.Avedev(o => o+1));
    }
    [Fact]public void ループ起点(){
        var s = new int[]{1,2,3,4,5,6,7};
        var p=Expression.Parameter(typeof(int[]));
        var q=Expression.Parameter(typeof(int));
        var source=Expression.Assign(p,Expression.Constant(s));
        this.Cover(
            Expression.Block(
                new[]{p},
                Expression.Call(
                    LinqDB.Reflection.ExtensionEnumerable.Distinct0.MakeGenericMethod(typeof(int)),
                    Expression.Assign(
                        p,
                        Expression.Constant(s)
                    )
                )
            )
        );
    }
    [Fact]
    public void 具象Type(){
        var s = new int[]{
            1,2,3,4,5,6,7
        };
        //if(!Type.IsSealed) {
        //    if(typeof(IEnumerable<>)==Type.GetGenericTypeDefinition()) {
        this.Expression実行AssertEqual(() => s.ToSet().Select(o => o+1));
        //    } else {
        this.Expression実行AssertEqual(() => s.AsEnumerable().Union(s));
        //        } else {
        this.Expression実行AssertEqual(() => s.AsEnumerable().Select(o => o+1));
        //        }
        //    }
        //}
    }
}
