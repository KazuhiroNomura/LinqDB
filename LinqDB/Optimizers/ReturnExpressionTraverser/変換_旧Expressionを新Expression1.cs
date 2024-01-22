using System.Collections.Generic;
using System.Linq.Expressions;
// ReSharper disable AssignNullToNotNullAttribute
namespace LinqDB.Optimizers.ReturnExpressionTraverser;

internal class 変換_旧Expressionを新Expression1(作業配列 作業配列,IEqualityComparer<Expression> ExpressionEqualityComparer):ReturnExpressionTraverser(作業配列){
    private Expression? 旧Expression, 新Expression;
    public Expression 実行(Expression e,Expression 旧Expression,Expression 新Expression) {
        this.旧Expression=旧Expression;
        this.新Expression=新Expression;
        return this.Traverse(e);
    }
    protected override Expression Traverse(Expression Expression0){
        if(ExpressionEqualityComparer.Equals(Expression0,this.旧Expression!))
            return this.新Expression!;
        return base.Traverse(Expression0);
    }
}
