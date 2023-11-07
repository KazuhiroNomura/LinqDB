using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqDB.Optimizers.Comparison;
using LinqDB.Optimizers.VoidExpressionTraverser;

namespace LinqDB.Optimizers.VoidExpressionTraverser;

/// <summary>
/// Expressionを最適化する
/// </summary>

internal sealed class 取得_出力Table:VoidExpressionTraverser {
    private readonly List<Expression> List出力TableExpression;
    private readonly ExpressionEqualityComparer ExpressionEqualityComparer;
    //private bool null可能性があるか;
    public 取得_出力Table(ExpressionEqualityComparer ExpressionEqualityComparer,List<Expression> List出力TableExpression) {
        this.ExpressionEqualityComparer=ExpressionEqualityComparer;
        this.List出力TableExpression=List出力TableExpression;
    }
    private IEnumerable<Expression>? 指定TableExpressions;
    public void 実行(Expression e,IEnumerable<Expression> 指定TableExpressions) {
        this.指定TableExpressions=指定TableExpressions;
        this.List出力TableExpression.Clear();
        //this.null可能性があるか= false;
        this.Traverse(e);
    }

    protected override void Traverse(Expression e) {
        base.Traverse(e);
        if(!this.List出力TableExpression.Contains(e)&&this.指定TableExpressions!.Contains(e,this.ExpressionEqualityComparer))
            this.List出力TableExpression.Add(e);
    }
}
