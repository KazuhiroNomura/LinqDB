﻿using System.Collections.Generic;
using System.Linq.Expressions;
// ReSharper disable AssignNullToNotNullAttribute
namespace LinqDB.Optimizers.ReturnExpressionTraverser;

internal class 変換_旧Expressionを新Expression1:ReturnExpressionTraverser {
    private protected readonly IEqualityComparer<Expression> ExpressionEqualityComparer;
    public 変換_旧Expressionを新Expression1(作業配列 作業配列,IEqualityComparer<Expression>ExpressionEqualityComparer) : base(作業配列) => this.ExpressionEqualityComparer=ExpressionEqualityComparer;
    private Expression? 旧Expression, 新Expression;
    public Expression 実行(Expression e,Expression 旧Expression,Expression 新Expression) {
        this.旧Expression=旧Expression;
        this.新Expression=新Expression;
        return this.Traverse(e);
    }
    protected override Expression Traverse(Expression Expression0) => this.ExpressionEqualityComparer.Equals(Expression0,this.旧Expression!)
        ? this.新Expression!
        : base.Traverse(Expression0);
}
