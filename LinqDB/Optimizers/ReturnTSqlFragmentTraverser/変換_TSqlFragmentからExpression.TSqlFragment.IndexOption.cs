using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
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
