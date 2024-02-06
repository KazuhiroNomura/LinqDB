using System.Linq;
using LinqDB.Sets;
using System.Diagnostics;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Reflection;
using System.Xml.Linq;
using e = System.Linq.Expressions;
using AssemblyName = Microsoft.SqlServer.TransactSql.ScriptDom.AssemblyName;
using System.Globalization;
using LinqDB.Optimizers.ReturnExpressionTraverser;
using LinqDB.Helpers;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
using static Common;
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression CheckConstraintDefinition(CheckConstraintDefinition x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DefaultConstraintDefinition(DefaultConstraintDefinition x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ForeignKeyConstraintDefinition(ForeignKeyConstraintDefinition x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression NullableConstraintDefinition(NullableConstraintDefinition x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression GraphConnectionConstraintDefinition(GraphConnectionConstraintDefinition x){
        throw this.単純NotSupportedException(x);
    }
    /// <summary>
    /// primary keyとか
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private void UniqueConstraintDefinition(UniqueConstraintDefinition x){
        foreach(var IndexOption in x.IndexOptions)
            this.IndexOption(IndexOption);
        var IndexType=x.IndexType;
        var IsEnforced = x.IsEnforced;
        var Clustered = x.Clustered;
        var IsPrimaryKey = x.IsPrimaryKey;
        foreach(var Column in x.Columns)
            this.ColumnWithSortOrder(Column);
    }
}
