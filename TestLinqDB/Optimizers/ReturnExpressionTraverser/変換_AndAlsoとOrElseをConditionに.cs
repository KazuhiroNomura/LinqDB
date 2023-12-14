using System.Linq.Expressions;
using LinqDB.Databases.Tables;
using LinqDB.Helpers;
using LinqDB.Sets;


//using Exception=System.Exception;
using Expression = System.Linq.Expressions.Expression;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
namespace TestLinqDB.Optimizers.ReturnExpressionTraverser.ReturnExpressionTraverser;
public class 変換_AndAlsoとOrElseをConditionに : 共通
{
    private static readonly 特殊パターン.変換_局所Parameterの先行評価.operator_true Op = new(true);
    // private static bool boolを返す関数()=>true;
    [Fact]
    public void AndAlso_OrElse()
    {
        //if(test.Type==typeof(bool)) {
        this.Optimizer.Lambda最適化(() => true&&ReferenceEquals(null, ""));
        //} else {
        var Constant = Expression.Constant(Op);
        this.Optimizer.Lambda最適化(
            Expression.Lambda(
                Expression.AndAlso(
                    Constant,
                    Expression.New(typeof(特殊パターン.変換_局所Parameterの先行評価.operator_true))
                )
            )
        );
        //}
    }
    [Fact]
    public void AndAlso()
    {
        //if(Binary1_Right.NodeType is ExpressionType.Constant or ExpressionType.Parameter) return Expression.And(Binary1_Left,Binary1_Right);
        var Constant = Expression.Constant(Op);
        this.Optimizer.Lambda最適化(
            Expression.Lambda(
                Expression.AndAlso(Constant, Constant)
            )
        );
        var p = Expression.Parameter(typeof(特殊パターン.変換_局所Parameterの先行評価.operator_true));
        this.Optimizer.Lambda最適化(
            Expression.Lambda(
                Expression.AndAlso(Constant, p),
                p
            )
        );
        //if(Binary1_Left.NodeType is ExpressionType.Constant or ExpressionType.Parameter){
        this.Optimizer.Lambda最適化(
            Expression.Lambda(
                Expression.AndAlso(
                    Constant,
                    Expression.New(typeof(特殊パターン.変換_局所Parameterの先行評価.operator_true))
                ),
                p
            )
        );
        this.Optimizer.Lambda最適化(
            Expression.Lambda(
                Expression.AndAlso(
                    p,
                    Expression.New(typeof(特殊パターン.変換_局所Parameterの先行評価.operator_true))
                ),
                p
            )
        );
        //} else{
        this.Optimizer.Lambda最適化(() =>
            Tuple.Create(Op, Op).Let(
                p =>
                    p.Item1&&p.Item2
            )
        );
        //}
    }
    [Fact]
    public void OrElse()
    {
        //if(Binary1_Right.NodeType is ExpressionType.Constant or ExpressionType.Parameter) return Expression.Or(Binary1_Left,Binary1_Right);
        var Constant = Expression.Constant(Op);
        this.Optimizer.Lambda最適化(
            Expression.Lambda(
                Expression.OrElse(Constant, Constant)
            )
        );
        var p = Expression.Parameter(typeof(特殊パターン.変換_局所Parameterの先行評価.operator_true));
        this.Optimizer.Lambda最適化(
            Expression.Lambda(
                Expression.OrElse(Constant, p),
                p
            )
        );
        //if(Binary1_Left.NodeType is ExpressionType.Constant or ExpressionType.Parameter){
        this.Optimizer.Lambda最適化(
            Expression.Lambda(
                Expression.OrElse(
                    Constant,
                    Expression.New(typeof(特殊パターン.変換_局所Parameterの先行評価.operator_true))
                ),
                p
            )
        );
        this.Optimizer.Lambda最適化(
            Expression.Lambda(
                Expression.OrElse(
                    p,
                    Expression.New(typeof(特殊パターン.変換_局所Parameterの先行評価.operator_true))
                ),
                p
            )
        );
        //} else{
        this.Optimizer.Lambda最適化(() =>
            Tuple.Create(Op, Op).Let(
                p =>
                    p.Item1||p.Item2
            )
        );
        //}
    }
}
