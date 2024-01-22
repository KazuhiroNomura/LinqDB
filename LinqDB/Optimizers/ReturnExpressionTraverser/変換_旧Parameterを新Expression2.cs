using System.Diagnostics;
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
        Debug.Assert(旧Parameter1.Type==新Expression1.Type);
        this.旧Parameter2=旧Parameter2;
        this.新Expression2=新Expression2;
        //旧Parameter2.Type==typeof(IEnumerable<int>)
        //新Expression2.Type==typeof(IGrouping<int,int>)
        //の時があるので型は一致しない。
        return this.Traverse(e);
    }
    protected override Expression Traverse(Expression Expression0){
        if(Expression0==this.旧Parameter1!) return this.新Expression1!;
        if(Expression0==this.旧Parameter2!) return this.新Expression2!;
        return base.Traverse(Expression0);
    }
}
