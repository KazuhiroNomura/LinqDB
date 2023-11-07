using System.Linq.Expressions;
// ReSharper disable AssignNullToNotNullAttribute
namespace LinqDB.Optimizers.ReturnExpressionTraverser;

internal sealed class 変換_旧Parameterを新Expression2:ReturnExpressionTraverser {
    private ParameterExpression? 旧Parameter1, 旧Parameter2;
    private Expression? 新Expression1, 新Expression2;
    public 変換_旧Parameterを新Expression2(作業配列 作業配列) : base(作業配列){}
    public Expression 実行(Expression e,ParameterExpression 旧Parameter1,Expression 新Expression1,ParameterExpression 旧Parameter2,Expression 新Expression2) {
        this.旧Parameter1=旧Parameter1;
        this.新Expression1=新Expression1;
        //Debug.Assert(旧Parameter1.Type==新Expression1.Type);
        this.旧Parameter2=旧Parameter2;
        this.新Expression2=新Expression2;
        //Debug.Assert(旧Parameter2.Type==新Expression2.Type);
        return this.Traverse(e);
    }
    protected override Expression Traverse(Expression Expression0) => Expression0==this.旧Parameter1!
        ? this.新Expression1!
        : Expression0==this.旧Parameter2!
            ? this.新Expression2!
            : base.Traverse(Expression0);
}
