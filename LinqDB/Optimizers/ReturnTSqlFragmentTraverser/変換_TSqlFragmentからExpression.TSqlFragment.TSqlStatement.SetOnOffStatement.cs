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
    /// <summary>
    /// SET NOCOUNTなどのオプション設定
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression PredicateSetStatement(PredicateSetStatement x)=>Default_void;
    private e.Expression SetStatisticsStatement(SetStatisticsStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression SetOffsetsStatement(SetOffsetsStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression SetIdentityInsertStatement(SetIdentityInsertStatement x){throw this.単純NotSupportedException(x);}
}
