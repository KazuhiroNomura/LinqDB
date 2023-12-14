using System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnExpressionTraverser;
using static Common;
/// <summary>
///a||b||c　はそのままだと
///(a.op_True?a:a|b).op_True?(a.op_True?a:a|b):(a.op_True?a:a|b)|c と指数関数的に増大する
///{(var t1={(var t0=a).op_True?t0:t0|b}).op_True?t1:t1|c}　そこであえて例外的にここで先行評価にする
/// </summary>
internal sealed class 変換_AndAlsoとOrElseをConditionに:ReturnExpressionTraverser_Quoteを処理しない {
    public 変換_AndAlsoとOrElseをConditionに(作業配列 作業配列) : base(作業配列) {
    }
    public Expression 実行(Expression e) {
        return this.Traverse(e);
    }
    #if true
    private Expression AndAlso_OrElse(Expression test,Expression ifTrue,Expression ifFalse) {
        if(test.Type==typeof(bool)) {
            return Expression.Condition(
                test,
                ifTrue,
                ifFalse
            );
        } else {
            return Expression.Condition(
                Expression.Call(
                    test.Type.GetMethod(op_True)!,
                    test
                ),
                ifTrue,
                ifFalse
            );
        }
    }
    private Expression AndAlso_OrElse(ParameterExpression p,Expression test,Expression ifTrue,Expression ifFalse) {
        if(test.Type==typeof(bool)) {
            return Expression.Block(
                this.作業配列.Parameters設定(p),
                Expression.Condition(
                    test,
                    ifTrue,
                    ifFalse
                )                
            );
        } else {
            return Expression.Block(
                this.作業配列.Parameters設定(p),
                Expression.Condition(
                    Expression.Call(
                        test.Type.GetMethod(op_True)!,
                        test
                    ),
                    ifTrue,
                    ifFalse
                )
            );
        }
    }
    /// <summary>
    /// a&amp;&amp;b→operator true(a)?a&amp;b:a
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected override Expression AndAlso(BinaryExpression Binary0) {
        var Binary1_Left = this.Traverse(Binary0.Left);
        var Binary1_Right=this.Traverse(Binary0.Right);
        if(Binary1_Right.NodeType is ExpressionType.Constant or ExpressionType.Parameter) return Expression.And(Binary1_Left,Binary1_Right);
        if(Binary1_Left.NodeType is ExpressionType.Constant or ExpressionType.Parameter){
            return this.AndAlso_OrElse(
                Binary1_Left,
                Expression.And(Binary1_Left,Binary1_Right),
                Binary1_Left
            );
        } else{
            var p=Expression.Parameter(Binary1_Left.Type,"AndAlso");
            return this.AndAlso_OrElse(
                p,
                Expression.Assign(p,Binary1_Left),
                Expression.And(p,Binary1_Right),
                p
            );
        }
    }
    /// <summary>
    /// a||b→operator true(a)?a:a|b
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected override Expression OrElse(BinaryExpression Binary0) {
        var Binary1_Left =this.Traverse(Binary0.Left);
        var Binary1_Right=this.Traverse(Binary0.Right);
        if(Binary1_Right.NodeType is ExpressionType.Constant or ExpressionType.Parameter) return Expression.Or(Binary1_Left,Binary1_Right);
        if(Binary1_Left.NodeType is ExpressionType.Constant or ExpressionType.Parameter){
            return this.AndAlso_OrElse(
                Binary1_Left,
                Binary1_Left,
                Expression.Or(Binary1_Left,Binary1_Right)
            );
        } else{
            var p=Expression.Parameter(Binary1_Left.Type,"AndAlso");
            return this.AndAlso_OrElse(
                p,
                Expression.Assign(p,Binary1_Left),
                p,
                Expression.Or(p,Binary1_Right)
            );
        }
    }
    #endif
}
