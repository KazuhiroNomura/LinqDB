using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    private object LiteralOptimizerHint(LiteralOptimizerHint x){
        switch(x.HintKind){
            case OptimizerHintKind.Unspecified:
                break;
            case OptimizerHintKind.HashGroup:
                break;
            case OptimizerHintKind.OrderGroup:
                break;
            case OptimizerHintKind.MergeJoin:
                break;
            case OptimizerHintKind.HashJoin:
                break;
            case OptimizerHintKind.LoopJoin:
                break;
            case OptimizerHintKind.ConcatUnion:
                break;
            case OptimizerHintKind.HashUnion:
                break;
            case OptimizerHintKind.MergeUnion:
                break;
            case OptimizerHintKind.KeepUnion:
                break;
            case OptimizerHintKind.ForceOrder:
                break;
            case OptimizerHintKind.RobustPlan:
                break;
            case OptimizerHintKind.KeepPlan:
                break;
            case OptimizerHintKind.KeepFixedPlan:
                break;
            case OptimizerHintKind.ExpandViews:
                break;
            case OptimizerHintKind.AlterColumnPlan:
                break;
            case OptimizerHintKind.ShrinkDBPlan:
                break;
            case OptimizerHintKind.BypassOptimizerQueue:
                break;
            case OptimizerHintKind.UsePlan:
                break;
            case OptimizerHintKind.ParameterizationSimple:
                break;
            case OptimizerHintKind.ParameterizationForced:
                break;
            case OptimizerHintKind.OptimizeCorrelatedUnionAll:
                break;
            case OptimizerHintKind.Recompile:
                break;
            case OptimizerHintKind.Fast:
                break;
            case OptimizerHintKind.CheckConstraintsPlan:
                break;
            case OptimizerHintKind.MaxRecursion:{
                var y=x.Value.LiteralType;
                var maxrecursion数=int.Parse(x.Value.Value);
                break;
            }
            case OptimizerHintKind.MaxDop:
                break;
            case OptimizerHintKind.QueryTraceOn:
                break;
            case OptimizerHintKind.CardinalityTunerLimit:
                break;
            case OptimizerHintKind.TableHints:
                break;
            case OptimizerHintKind.OptimizeFor:
                break;
            case OptimizerHintKind.IgnoreNonClusteredColumnStoreIndex:
                break;
            case OptimizerHintKind.MaxGrantPercent:
                break;
            case OptimizerHintKind.MinGrantPercent:
                break;
            case OptimizerHintKind.NoPerformanceSpool:
                break;
            case OptimizerHintKind.Label:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return new object();
    }
    private e.Expression TableHintsOptimizerHint(TableHintsOptimizerHint x){
        switch(x.HintKind){
            case OptimizerHintKind.Unspecified:
                break;
            case OptimizerHintKind.HashGroup:
                break;
            case OptimizerHintKind.OrderGroup:
                break;
            case OptimizerHintKind.MergeJoin:
                break;
            case OptimizerHintKind.HashJoin:
                break;
            case OptimizerHintKind.LoopJoin:
                break;
            case OptimizerHintKind.ConcatUnion:
                break;
            case OptimizerHintKind.HashUnion:
                break;
            case OptimizerHintKind.MergeUnion:
                break;
            case OptimizerHintKind.KeepUnion:
                break;
            case OptimizerHintKind.ForceOrder:
                break;
            case OptimizerHintKind.RobustPlan:
                break;
            case OptimizerHintKind.KeepPlan:
                break;
            case OptimizerHintKind.KeepFixedPlan:
                break;
            case OptimizerHintKind.ExpandViews:
                break;
            case OptimizerHintKind.AlterColumnPlan:
                break;
            case OptimizerHintKind.ShrinkDBPlan:
                break;
            case OptimizerHintKind.BypassOptimizerQueue:
                break;
            case OptimizerHintKind.UsePlan:
                break;
            case OptimizerHintKind.ParameterizationSimple:
                break;
            case OptimizerHintKind.ParameterizationForced:
                break;
            case OptimizerHintKind.OptimizeCorrelatedUnionAll:
                break;
            case OptimizerHintKind.Recompile:
                break;
            case OptimizerHintKind.Fast:
                break;
            case OptimizerHintKind.CheckConstraintsPlan:
                break;
            case OptimizerHintKind.MaxRecursion:
                break;
            case OptimizerHintKind.MaxDop:
                break;
            case OptimizerHintKind.QueryTraceOn:
                break;
            case OptimizerHintKind.CardinalityTunerLimit:
                break;
            case OptimizerHintKind.TableHints:
                break;
            case OptimizerHintKind.OptimizeFor:
                break;
            case OptimizerHintKind.IgnoreNonClusteredColumnStoreIndex:
                break;
            case OptimizerHintKind.MaxGrantPercent:
                break;
            case OptimizerHintKind.MinGrantPercent:
                break;
            case OptimizerHintKind.NoPerformanceSpool:
                break;
            case OptimizerHintKind.Label:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        throw this.単純NotSupportedException(x);
    }
    private e.Expression OptimizeForOptimizerHint(OptimizeForOptimizerHint x){
        switch(x.HintKind){
            case OptimizerHintKind.Unspecified:
                break;
            case OptimizerHintKind.HashGroup:
                break;
            case OptimizerHintKind.OrderGroup:
                break;
            case OptimizerHintKind.MergeJoin:
                break;
            case OptimizerHintKind.HashJoin:
                break;
            case OptimizerHintKind.LoopJoin:
                break;
            case OptimizerHintKind.ConcatUnion:
                break;
            case OptimizerHintKind.HashUnion:
                break;
            case OptimizerHintKind.MergeUnion:
                break;
            case OptimizerHintKind.KeepUnion:
                break;
            case OptimizerHintKind.ForceOrder:
                break;
            case OptimizerHintKind.RobustPlan:
                break;
            case OptimizerHintKind.KeepPlan:
                break;
            case OptimizerHintKind.KeepFixedPlan:
                break;
            case OptimizerHintKind.ExpandViews:
                break;
            case OptimizerHintKind.AlterColumnPlan:
                break;
            case OptimizerHintKind.ShrinkDBPlan:
                break;
            case OptimizerHintKind.BypassOptimizerQueue:
                break;
            case OptimizerHintKind.UsePlan:
                break;
            case OptimizerHintKind.ParameterizationSimple:
                break;
            case OptimizerHintKind.ParameterizationForced:
                break;
            case OptimizerHintKind.OptimizeCorrelatedUnionAll:
                break;
            case OptimizerHintKind.Recompile:
                break;
            case OptimizerHintKind.Fast:
                break;
            case OptimizerHintKind.CheckConstraintsPlan:
                break;
            case OptimizerHintKind.MaxRecursion:
                break;
            case OptimizerHintKind.MaxDop:
                break;
            case OptimizerHintKind.QueryTraceOn:
                break;
            case OptimizerHintKind.CardinalityTunerLimit:
                break;
            case OptimizerHintKind.TableHints:
                break;
            case OptimizerHintKind.OptimizeFor:
                break;
            case OptimizerHintKind.IgnoreNonClusteredColumnStoreIndex:
                break;
            case OptimizerHintKind.MaxGrantPercent:
                break;
            case OptimizerHintKind.MinGrantPercent:
                break;
            case OptimizerHintKind.NoPerformanceSpool:
                break;
            case OptimizerHintKind.Label:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        throw this.単純NotSupportedException(x);
    }
    private e.Expression UseHintList(UseHintList x){
        throw this.単純NotSupportedException(x);
    }
}
