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
    private e.Expression AlterDatabaseCollateStatement(AlterDatabaseCollateStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterDatabaseRebuildLogStatement(AlterDatabaseRebuildLogStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterDatabaseAddFileStatement(AlterDatabaseAddFileStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterDatabaseAddFileGroupStatement(AlterDatabaseAddFileGroupStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterDatabaseRemoveFileGroupStatement(AlterDatabaseRemoveFileGroupStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterDatabaseRemoveFileStatement(AlterDatabaseRemoveFileStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterDatabaseModifyNameStatement(AlterDatabaseModifyNameStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterDatabaseModifyFileStatement(AlterDatabaseModifyFileStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterDatabaseModifyFileGroupStatement(AlterDatabaseModifyFileGroupStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterDatabaseSetStatement(AlterDatabaseSetStatement x){
        throw this.単純NotSupportedException(x);
    }
}
