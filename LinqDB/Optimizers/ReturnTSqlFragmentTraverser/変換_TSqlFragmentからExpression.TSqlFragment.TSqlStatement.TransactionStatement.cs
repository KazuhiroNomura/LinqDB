﻿using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression BeginTransactionStatement(BeginTransactionStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CommitTransactionStatement(CommitTransactionStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression RollbackTransactionStatement(RollbackTransactionStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression SaveTransactionStatement(SaveTransactionStatement x){
        throw this.単純NotSupportedException(x);
    }
}
