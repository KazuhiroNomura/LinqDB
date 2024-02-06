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
    private e.Expression TableOption(TableOption x)=>x switch{
        LockEscalationTableOption y=>this.LockEscalationTableOption(y),
        FileStreamOnTableOption y=>this.FileStreamOnTableOption(y),
        FileTableDirectoryTableOption y=>this.FileTableDirectoryTableOption(y),
        FileTableCollateFileNameTableOption y=>this.FileTableCollateFileNameTableOption(y),
        FileTableConstraintNameTableOption y=>this.FileTableConstraintNameTableOption(y),
        MemoryOptimizedTableOption y=>this.MemoryOptimizedTableOption(y),
        DurabilityTableOption y=>this.DurabilityTableOption(y),
        RemoteDataArchiveTableOption y=>this.RemoteDataArchiveTableOption(y),
        RemoteDataArchiveAlterTableOption y=>this.RemoteDataArchiveAlterTableOption(y),
        SystemVersioningTableOption y=>this.SystemVersioningTableOption(y),
        TableDataCompressionOption y=>this.TableDataCompressionOption(y),
        TableDistributionOption y=>this.TableDistributionOption(y),
        TableIndexOption y=>this.TableIndexOption(y),
        TablePartitionOption y=>this.TablePartitionOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression LockEscalationTableOption(LockEscalationTableOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression FileStreamOnTableOption(FileStreamOnTableOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression FileTableDirectoryTableOption(FileTableDirectoryTableOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression FileTableCollateFileNameTableOption(FileTableCollateFileNameTableOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression FileTableConstraintNameTableOption(FileTableConstraintNameTableOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression MemoryOptimizedTableOption(MemoryOptimizedTableOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DurabilityTableOption(DurabilityTableOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression RemoteDataArchiveTableOption(RemoteDataArchiveTableOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression RemoteDataArchiveAlterTableOption(RemoteDataArchiveAlterTableOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression SystemVersioningTableOption(SystemVersioningTableOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression TableDataCompressionOption(TableDataCompressionOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression TableDistributionOption(TableDistributionOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression TableIndexOption(TableIndexOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression TablePartitionOption(TablePartitionOption x){
        throw this.単純NotSupportedException(x);
    }
}
