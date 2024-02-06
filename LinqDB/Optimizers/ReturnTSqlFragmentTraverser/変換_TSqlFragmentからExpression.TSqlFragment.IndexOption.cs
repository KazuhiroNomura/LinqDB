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
    private e.Expression CompressionDelayIndexOption(CompressionDelayIndexOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DataCompressionOption(DataCompressionOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression FileStreamOnDropIndexOption(FileStreamOnDropIndexOption x){throw this.単純NotSupportedException(x);}
    private e.Expression IndexExpressionOption(IndexExpressionOption x){throw this.単純NotSupportedException(x);}
    private e.Expression IndexStateOption(IndexStateOption x)=>x switch{
        OnlineIndexOption y=>this.OnlineIndexOption(y),
        IgnoreDupKeyIndexOption y=>this.IgnoreDupKeyIndexOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression MaxDurationOption(MaxDurationOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression MoveToDropIndexOption(MoveToDropIndexOption x){throw this.単純NotSupportedException(x);}
    private e.Expression OrderIndexOption(OrderIndexOption x){throw this.単純NotSupportedException(x);}
    private e.Expression WaitAtLowPriorityOption(WaitAtLowPriorityOption x){
        throw this.単純NotSupportedException(x);
    }
}
