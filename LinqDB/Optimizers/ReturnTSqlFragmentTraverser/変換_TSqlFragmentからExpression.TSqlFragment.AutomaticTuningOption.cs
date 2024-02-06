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
    private e.Expression AutomaticTuningForceLastGoodPlanOption(AutomaticTuningForceLastGoodPlanOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AutomaticTuningCreateIndexOption(AutomaticTuningCreateIndexOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AutomaticTuningDropIndexOption(AutomaticTuningDropIndexOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AutomaticTuningMaintainIndexOption(AutomaticTuningMaintainIndexOption x){
        throw this.単純NotSupportedException(x);
    }
}
