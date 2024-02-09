using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression AutomaticTuningDatabaseOption(AutomaticTuningDatabaseOption x){        throw this.単純NotSupportedException(x);    }
    private e.Expression CatalogCollationOption(CatalogCollationOption x){        throw this.単純NotSupportedException(x);    }
    private e.Expression ChangeTrackingDatabaseOption(ChangeTrackingDatabaseOption x){        throw this.単純NotSupportedException(x);    }
    private e.Expression ContainmentDatabaseOption(ContainmentDatabaseOption x){throw this.単純NotSupportedException(x);}
    private e.Expression CursorDefaultDatabaseOption(CursorDefaultDatabaseOption x){        throw this.単純NotSupportedException(x);    }
    private e.Expression DelayedDurabilityDatabaseOption(DelayedDurabilityDatabaseOption x){        throw this.単純NotSupportedException(x);    }
    private e.Expression FileStreamDatabaseOption(FileStreamDatabaseOption x){        throw this.単純NotSupportedException(x);    }

    private e.Expression IdentifierDatabaseOption(IdentifierDatabaseOption x){        throw this.単純NotSupportedException(x);    }
    private e.Expression LiteralDatabaseOption(LiteralDatabaseOption x){        throw this.単純NotSupportedException(x);    }
    private e.Expression MaxSizeDatabaseOption(MaxSizeDatabaseOption x){        throw this.単純NotSupportedException(x);    }
    private e.Expression OnOffDatabaseOption(OnOffDatabaseOption x)=>x switch{
        AutoCreateStatisticsDatabaseOption y=>this.AutoCreateStatisticsDatabaseOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression PageVerifyDatabaseOption(PageVerifyDatabaseOption x){        throw this.単純NotSupportedException(x);    }
    private e.Expression ParameterizationDatabaseOption(ParameterizationDatabaseOption x){        throw this.単純NotSupportedException(x);    }
    private e.Expression PartnerDatabaseOption(PartnerDatabaseOption x){        throw this.単純NotSupportedException(x);    }
    private e.Expression QueryStoreDatabaseOption(QueryStoreDatabaseOption x){        throw this.単純NotSupportedException(x);    }
    private e.Expression RecoveryDatabaseOption(RecoveryDatabaseOption x){        throw this.単純NotSupportedException(x);    }
    private e.Expression RemoteDataArchiveDatabaseOption(RemoteDataArchiveDatabaseOption x){throw this.単純NotSupportedException(x);}
    private e.Expression TargetRecoveryTimeDatabaseOption(TargetRecoveryTimeDatabaseOption x){        throw this.単純NotSupportedException(x);    }
    private e.Expression WitnessDatabaseOption(WitnessDatabaseOption x){        throw this.単純NotSupportedException(x);    }
}
