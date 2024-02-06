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
    private e.Expression AlterTableAddTableElementStatement(AlterTableAddTableElementStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterTableAlterColumnStatement(AlterTableAlterColumnStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterTableAlterIndexStatement(AlterTableAlterIndexStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterTableChangeTrackingModificationStatement(AlterTableChangeTrackingModificationStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterTableConstraintModificationStatement(AlterTableConstraintModificationStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterTableDropTableElementStatement(AlterTableDropTableElementStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterTableFileTableNamespaceStatement(AlterTableFileTableNamespaceStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterTableRebuildStatement(AlterTableRebuildStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterTableSetStatement(AlterTableSetStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterTableSwitchStatement(AlterTableSwitchStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterTableTriggerModificationStatement(AlterTableTriggerModificationStatement x){
        throw this.単純NotSupportedException(x);
    }
}
