using System.Linq;
using LinqDB.Sets;
using System.Diagnostics;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Reflection;
using e = System.Linq.Expressions;
using AssemblyName = Microsoft.SqlServer.TransactSql.ScriptDom.AssemblyName;
using System.Globalization;
using LinqDB.Optimizers.ReturnExpressionTraverser;
using LinqDB.Helpers;
using Microsoft.Build.Execution;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
using static Common;
internal partial class 変換_TSqlFragmentからExpression{
    //TSqlFragment
    private e.Expression AddFileSpec(AddFileSpec x){
        throw this.単純NotSupportedException(x);
    }
    //TSqlFragment
    private e.Expression AdHocDataSource(AdHocDataSource x){throw this.単純NotSupportedException(x);}
    //TSqlFragment
    private e.Expression AlterAvailabilityGroupAction(AlterAvailabilityGroupAction x){throw this.単純NotSupportedException(x);}
    //TSqlFragment
    private e.Expression AlterAvailabilityGroupFailoverOption(AlterAvailabilityGroupFailoverOption x){throw this.単純NotSupportedException(x);}
    //TSqlFragment
    private e.Expression AlterDatabaseTermination(AlterDatabaseTermination x){throw this.単純NotSupportedException(x);}
    //TSqlFragment
    private e.Expression AlterFullTextIndexAction(AlterFullTextIndexAction x)=>x switch{
        SimpleAlterFullTextIndexAction y=>this.SimpleAlterFullTextIndexAction(y),
        SetStopListAlterFullTextIndexAction y=>this.SetStopListAlterFullTextIndexAction(y),
        SetSearchPropertyListAlterFullTextIndexAction y=>this.SetSearchPropertyListAlterFullTextIndexAction(y),
        DropAlterFullTextIndexAction y=>this.DropAlterFullTextIndexAction(y),
        AddAlterFullTextIndexAction y=>this.AddAlterFullTextIndexAction(y),
        AlterColumnAlterFullTextIndexAction y=>this.AlterColumnAlterFullTextIndexAction(y),
        _=>throw this.単純NotSupportedException(x)
    };
    //TSqlFragment
    private e.Expression AlterRoleAction(AlterRoleAction x){throw this.単純NotSupportedException(x);}
    //TSqlFragment
    private e.Expression AlterServerConfigurationBufferPoolExtensionOption(AlterServerConfigurationBufferPoolExtensionOption x)=>x switch{
        AlterServerConfigurationBufferPoolExtensionContainerOption y=>this.AlterServerConfigurationBufferPoolExtensionContainerOption(y),
        AlterServerConfigurationBufferPoolExtensionSizeOption y=>this.AlterServerConfigurationBufferPoolExtensionSizeOption(y),
        _=>throw this.単純NotSupportedException(x.GetType())
    };
    private e.Expression AlterServerConfigurationDiagnosticsLogOption(AlterServerConfigurationDiagnosticsLogOption x)=>x switch{
        AlterServerConfigurationDiagnosticsLogMaxSizeOption y=>this.AlterServerConfigurationDiagnosticsLogMaxSizeOption(y),
        _=>throw this.単純NotSupportedException(x.GetType())
    };
    //TSqlFragment
    private e.Expression AlterServerConfigurationExternalAuthenticationOption(AlterServerConfigurationExternalAuthenticationOption x){throw this.単純NotSupportedException(x);}
    //TSqlFragment
    private e.Expression AlterServerConfigurationFailoverClusterPropertyOption(AlterServerConfigurationFailoverClusterPropertyOption x){throw this.単純NotSupportedException(x);}
    //TSqlFragment
    private e.Expression AlterServerConfigurationHadrClusterOption(AlterServerConfigurationHadrClusterOption x){throw this.単純NotSupportedException(x);}
    //TSqlFragment
    private e.Expression AlterServerConfigurationSoftNumaOption(AlterServerConfigurationSoftNumaOption x){throw this.単純NotSupportedException(x);}
    //TSqlFragment
    private e.Expression AlterTableDropTableElement(AlterTableDropTableElement x){throw this.単純NotSupportedException(x);}
    //TSqlFragment
    private e.Expression ApplicationRoleOption(ApplicationRoleOption x){throw this.単純NotSupportedException(x);}
    //TSqlFragment
    private e.Expression AssemblyName(AssemblyName x){throw this.単純NotSupportedException(x);}
    //TSqlFragment
    private e.Expression AssemblyOption(AssemblyOption x){throw this.単純NotSupportedException(x);}
    //TSqlFragment
    private string AtomicBlockOption(AtomicBlockOption x)=>x switch{
        LiteralAtomicBlockOption y=>this.LiteralAtomicBlockOption(y),
        IdentifierAtomicBlockOption y=>this.IdentifierAtomicBlockOption(y),
        OnOffAtomicBlockOption y=>this.OnOffAtomicBlockOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    /// <summary>
    /// LANGUAGE = N'us_english'
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private string LiteralAtomicBlockOption(LiteralAtomicBlockOption x){
        return x.OptionKind switch{
            AtomicBlockOptionKind.IsolationLevel=>"IsolationLevel",
            AtomicBlockOptionKind.Language=>"Language",
            AtomicBlockOptionKind.DateFirst=>"DateFirst",
            AtomicBlockOptionKind.DateFormat=>"DateFormat",
            AtomicBlockOptionKind.DelayedDurability=>"DelayedDurability",
            _=>throw new ArgumentOutOfRangeException(x.OptionKind.ToString())
        };
    }
    /// <summary>
    /// (TRANSACTION ISOLATION LEVEL = snapshot
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private string IdentifierAtomicBlockOption(IdentifierAtomicBlockOption x){
        //AtomicBlockOptionKind.Language;
        //var Value=x.Value;
        //x.OptionKind=AtomicBlockOptionKind.IsolationLevel;
        //throw this.単純NotSupportedException(x);
        return x.OptionKind switch{
            AtomicBlockOptionKind.IsolationLevel=>"IsolationLevel",
            AtomicBlockOptionKind.Language=>"Language",
            AtomicBlockOptionKind.DateFirst=>"DateFirst",
            AtomicBlockOptionKind.DateFormat=>"DateFormat",
            AtomicBlockOptionKind.DelayedDurability=>"DelayedDurability",
            _=>throw new ArgumentOutOfRangeException(x.OptionKind.ToString())
        };
    }
    private string OnOffAtomicBlockOption(OnOffAtomicBlockOption x){
        //x.OptionState=OptionState.Off;
        return x.OptionKind switch{
            AtomicBlockOptionKind.IsolationLevel=>"IsolationLevel",
            AtomicBlockOptionKind.Language=>"Language",
            AtomicBlockOptionKind.DateFirst=>"DateFirst",
            AtomicBlockOptionKind.DateFormat=>"DateFormat",
            AtomicBlockOptionKind.DelayedDurability=>"DelayedDurability",
            _=>throw new ArgumentOutOfRangeException(x.OptionKind.ToString())
        };
    }
    //TSqlFragment
    private e.Expression AuditSpecificationPart(AuditSpecificationPart x){throw this.単純NotSupportedException(x);}
    private e.Expression AuditTarget(AuditTarget x){throw this.単純NotSupportedException(x);}
    //TSqlFragment
    private e.Expression AvailabilityGroupOption(AvailabilityGroupOption x)=>x switch{
        LiteralAvailabilityGroupOption y=>this.LiteralAvailabilityGroupOption(y),
        _=>throw this.単純NotSupportedException(x.GetType())
    };
    //TSqlFragment
    private e.Expression AvailabilityReplica(AvailabilityReplica x){throw this.単純NotSupportedException(x);}
    //TSqlFragment
    private e.Expression AvailabilityReplicaOption(AvailabilityReplicaOption x)=>x switch{
        AvailabilityModeReplicaOption y=>this.AvailabilityModeReplicaOption(y),
        FailoverModeReplicaOption     y=>this.FailoverModeReplicaOption(y),
        LiteralReplicaOption          y=>this.LiteralReplicaOption(y),
        PrimaryRoleReplicaOption      y=>this.PrimaryRoleReplicaOption(y),
        SecondaryRoleReplicaOption    y=>this.SecondaryRoleReplicaOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    //TSqlFragment
    private e.Expression BooleanExpression(BooleanExpression x)=>x switch{
        BooleanBinaryExpression                  y=>this.BooleanBinaryExpression(y),
        BooleanComparisonExpression              y=>this.BooleanComparisonExpression(y),
        BooleanExpressionSnippet                 y=>this.BooleanExpressionSnippet(y),
        BooleanIsNullExpression                  y=>this.BooleanIsNullExpression(y),
        BooleanNotExpression                     y=>this.BooleanNotExpression(y),
        BooleanParenthesisExpression             y=>this.BooleanParenthesisExpression(y),
        BooleanTernaryExpression                 y=>this.BooleanTernaryExpression(y),
        EventDeclarationCompareFunctionParameter y=>this.EventDeclarationCompareFunctionParameter(y),
        ExistsPredicate                          y=>this.ExistsPredicate(y),
        FullTextPredicate                        y=>this.FullTextPredicate(y),
        GraphMatchCompositeExpression            y=>this.GraphMatchCompositeExpression(y),
        GraphMatchExpression                     y=>this.GraphMatchExpression(y),
        GraphMatchLastNodePredicate              y=>this.GraphMatchLastNodePredicate(y),
        GraphMatchNodeExpression                 y=>this.GraphMatchNodeExpression(y),
        GraphMatchRecursivePredicate             y=>this.GraphMatchRecursivePredicate(y),
        GraphMatchPredicate                      y=>this.GraphMatchPredicate(y),
        InPredicate                              y=>this.InPredicate(y),
        LikePredicate                            y=>this.LikePredicate(y),
        SubqueryComparisonPredicate              y=>this.SubqueryComparisonPredicate(y),
        TSEqualCall                              y=>this.TSEqualCall(y),
        UpdateCall                               y=>this.UpdateCall(y),
        _=>throw this.単純NotSupportedException(x)
    };
    //TSqlFragment
    private e.Expression BoundingBoxParameter(BoundingBoxParameter x){throw this.単純NotSupportedException(x);}
    //TSqlFragment
    private e.Expression BrokerPriorityParameter(BrokerPriorityParameter x){throw this.単純NotSupportedException(x);}
    //TSqlFragment
    private e.Expression BulkInsertOption(BulkInsertOption x)=>x switch{
        LiteralBulkInsertOption y=>this.LiteralBulkInsertOption(y),
        OrderBulkInsertOption y=>this.OrderBulkInsertOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    //TSqlFragment
    private e.Expression CallTarget(CallTarget x)=>x switch{
        ExpressionCallTarget y=>this.ExpressionCallTarget(y),
        MultiPartIdentifierCallTarget y=>this.MultiPartIdentifierCallTarget(y),
        UserDefinedTypeCallTarget y=>this.UserDefinedTypeCallTarget(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CertificateOption(CertificateOption x){throw this.単純NotSupportedException(x);}
    //TSqlFragment
    private e.Expression ChangeTrackingOptionDetail(ChangeTrackingOptionDetail x)=>x switch{
        AutoCleanupChangeTrackingOptionDetail y=>this.AutoCleanupChangeTrackingOptionDetail(y),
        ChangeRetentionChangeTrackingOptionDetail y=>this.ChangeRetentionChangeTrackingOptionDetail(y),
        _=>throw this.単純NotSupportedException(x)
    };
    //TSqlFragment
    private e.Expression ColumnEncryptionDefinition(ColumnEncryptionDefinition x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ColumnEncryptionDefinitionParameter(ColumnEncryptionDefinitionParameter x)=>x switch{
        ColumnEncryptionKeyNameParameter y=>this.ColumnEncryptionKeyNameParameter(y),
        ColumnEncryptionTypeParameter y=>this.ColumnEncryptionTypeParameter(y),
        ColumnEncryptionAlgorithmParameter y=>this.ColumnEncryptionAlgorithmParameter(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression ColumnWithSortOrder(ColumnWithSortOrder x){throw this.単純NotSupportedException(x);}
    private e.Expression ContractMessage(ContractMessage x){throw this.単純NotSupportedException(x);}
    private e.Expression CopyOption(CopyOption x){throw this.単純NotSupportedException(x);}
    private e.Expression CopyStatementOptionBase(CopyStatementOptionBase x)=>x switch{
        CopyCredentialOption      y=>this.CopyCredentialOption(y),
        SingleValueTypeCopyOption y=>this.SingleValueTypeCopyOption(y),
        ListTypeCopyOption                                    y=>this.ListTypeCopyOption(y),
        CopyColumnOption     y=>this.CopyColumnOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CryptoMechanism(CryptoMechanism x){throw this.単純NotSupportedException(x);}
    private e.Expression CursorDefinition(CursorDefinition x){throw this.単純NotSupportedException(x);}
    private e.Expression CursorId(CursorId x){throw this.単純NotSupportedException(x);}
    private e.Expression CursorOption(CursorOption x){throw this.単純NotSupportedException(x);}
    private e.Expression DatabaseAuditAction(DatabaseAuditAction x){throw this.単純NotSupportedException(x);}
    private e.Expression DatabaseOption(DatabaseOption x)=>x switch{
        AutomaticTuningDatabaseOption    y=>this.AutomaticTuningDatabaseOption(y),
        CatalogCollationOption           y=>this.CatalogCollationOption(y),
        ChangeTrackingDatabaseOption     y=>this.ChangeTrackingDatabaseOption(y),
        ContainmentDatabaseOption        y=>this.ContainmentDatabaseOption(y),
        CursorDefaultDatabaseOption      y=>this.CursorDefaultDatabaseOption(y),
        DelayedDurabilityDatabaseOption  y=>this.DelayedDurabilityDatabaseOption(y),
        FileStreamDatabaseOption         y=>this.FileStreamDatabaseOption(y),
        HadrDatabaseOption               y=>this.HadrDatabaseOption(y),
        IdentifierDatabaseOption         y=>this.IdentifierDatabaseOption(y),
        LiteralDatabaseOption            y=>this.LiteralDatabaseOption(y),
        MaxSizeDatabaseOption            y=>this.MaxSizeDatabaseOption(y),
        OnOffDatabaseOption              y=>this.OnOffDatabaseOption(y),
        PageVerifyDatabaseOption         y=>this.PageVerifyDatabaseOption(y),
        ParameterizationDatabaseOption   y=>this.ParameterizationDatabaseOption(y),
        PartnerDatabaseOption            y=>this.PartnerDatabaseOption(y),
        QueryStoreDatabaseOption         y=>this.QueryStoreDatabaseOption(y),
        RecoveryDatabaseOption           y=>this.RecoveryDatabaseOption(y),
        RemoteDataArchiveDatabaseOption  y=>this.RemoteDataArchiveDatabaseOption(y),
        TargetRecoveryTimeDatabaseOption y=>this.TargetRecoveryTimeDatabaseOption(y),
        WitnessDatabaseOption            y=>this.WitnessDatabaseOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    //private e.Expression DeclareTableVariableBody(DeclareTableVariableBody x){throw this.単純NotSupportedException(x);}
    //private e.Expression DeclareVaiableElement(DeclareVaiableElement x){throw this.単純NotSupportedException(x);}
    private e.Expression DiskStatementOption(DiskStatementOption x){throw this.単純NotSupportedException(x);}
    private e.Expression DropClusteredConstraintOption(DropClusteredConstraintOption x){throw this.単純NotSupportedException(x);}
    private e.Expression EndpointAffinity(EndpointAffinity x){throw this.単純NotSupportedException(x);}
    private e.Expression EventDeclaration(EventDeclaration x){throw this.単純NotSupportedException(x);}
    private e.Expression EventDeclarationSetParameter(EventDeclarationSetParameter x){throw this.単純NotSupportedException(x);}
    private e.Expression EventNotificationObjectScope(EventNotificationObjectScope x){throw this.単純NotSupportedException(x);}
    private e.Expression EventSessionObjectName(EventSessionObjectName x){throw this.単純NotSupportedException(x);}
    private e.Expression EventTypeGroupContainer(EventTypeGroupContainer x)=>x switch{
        EventTypeContainer y=>this.EventTypeContainer(y),
        EventGroupContainer y=>this.EventGroupContainer(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression EventTypeContainer(EventTypeContainer x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression EventGroupContainer(EventGroupContainer x){throw this.単純NotSupportedException(x);}
    private (PropertyInfo Schema,MethodInfo Method)ExecutableEntity(ExecutableEntity x)=>x switch{
        ExecutableProcedureReference y=>this.ExecutableProcedureReference(y),
        ExecutableStringList y=>this.ExecutableStringList(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression ExpressionCallTarget(ExpressionCallTarget x){throw this.単純NotSupportedException(x);}
    private e.Expression ExternalCreateLoginSource(ExternalCreateLoginSource x){throw this.単純NotSupportedException(x);}
    private e.Expression ExternalFileFormatOption(ExternalFileFormatOption x)=>x switch{
        ExternalFileFormatContainerOption y=>this.ExternalFileFormatContainerOption(y),
        ExternalFileFormatLiteralOption y=>this.ExternalFileFormatLiteralOption(y),
        ExternalFileFormatUseDefaultTypeOption y=>this.ExternalFileFormatUseDefaultTypeOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression ExternalLanguageFileOption(ExternalLanguageFileOption x){throw this.単純NotSupportedException(x);}
    private e.Expression ExternalLibraryFileOption(ExternalLibraryFileOption x){throw this.単純NotSupportedException(x);}
    private e.Expression ExternalResourcePoolAffinitySpecification(ExternalResourcePoolAffinitySpecification x){throw this.単純NotSupportedException(x);}
    private e.Expression ExternalResourcePoolParameter(ExternalResourcePoolParameter x){throw this.単純NotSupportedException(x);}
    private e.Expression ExternalStreamLiteralOrIdentifierOption(ExternalStreamLiteralOrIdentifierOption x){throw this.単純NotSupportedException(x);}
    private e.Expression ExternalStreamOption(ExternalStreamOption x){throw this.単純NotSupportedException(x);}
    private e.Expression FetchType(FetchType x){throw this.単純NotSupportedException(x);}
    private e.Expression FileDeclaration(FileDeclaration x){throw this.単純NotSupportedException(x);}
    private e.Expression FileDeclarationOption(FileDeclarationOption x)=>x switch{
        NameFileDeclarationOption y=>this.NameFileDeclarationOption(y),
        FileNameFileDeclarationOption y=>this.FileNameFileDeclarationOption(y),
        SizeFileDeclarationOption y=>this.SizeFileDeclarationOption(y),
        MaxSizeFileDeclarationOption y=>this.MaxSizeFileDeclarationOption(y),
        FileGrowthFileDeclarationOption y=>this.FileGrowthFileDeclarationOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression NameFileDeclarationOption(NameFileDeclarationOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression FileNameFileDeclarationOption(FileNameFileDeclarationOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression SizeFileDeclarationOption(SizeFileDeclarationOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression MaxSizeFileDeclarationOption(MaxSizeFileDeclarationOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression FileGrowthFileDeclarationOption(FileGrowthFileDeclarationOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression FileGroupOrPartitionScheme(FileGroupOrPartitionScheme x){throw this.単純NotSupportedException(x);}
    private e.Expression ForClause(ForClause x)=>x switch{
        BrowseForClause     y=>this.BrowseForClause(y),
        ReadOnlyForClause   y=>this.ReadOnlyForClause(y),
        XmlForClause        y=>this.XmlForClause(y),
        XmlForClauseOption  y=>this.XmlForClauseOption(y),
        JsonForClause       y=>this.JsonForClause(y),
        JsonForClauseOption y=>this.JsonForClauseOption(y),
        UpdateForClause     y=>this.UpdateForClause(y),
        _=>throw this.単純NotSupportedException(x)
    };
    //private e.Expression ComputeClause(ComputeClause x)=>throw this.単純NotSupportedException(x);
    //private e.Expression FromClause2(FromClause x)=>x switch{
    //    ComputeClause                                         y=>this.ComputeClause(y),
    //    DropIndexClauseBase                                   y=>this.DropIndexClauseBase(y),
    //    ExecuteAsClause                                       y=>this.ExecuteAsClause(y),
    //    SecurityUserClause80                                  y=>this.SecurityUserClause80(y),
    //    SetClause                                             y=>this.SetClause(y),
    //    TableSampleClause                                     y=>this.TableSampleClause(y),
    //    TemporalClause                                        y=>this.TemporalClause(y),
    //    _=>throw this.単純NotSupportedException(x)
    //};
    private e.Expression ComputeClause(ComputeClause x){throw this.単純NotSupportedException(x);}
    private e.Expression DropIndexClauseBase(DropIndexClauseBase x)=>x switch{
        BackwardsCompatibleDropIndexClause y=>this.BackwardsCompatibleDropIndexClause(y),
        DropIndexClause y=>this.DropIndexClause(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression BackwardsCompatibleDropIndexClause(BackwardsCompatibleDropIndexClause x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropIndexClause(DropIndexClause x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ExecuteAsClause(ExecuteAsClause x){throw this.単純NotSupportedException(x);}
    private e.Expression BrowseForClause(BrowseForClause x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ReadOnlyForClause(ReadOnlyForClause x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression XmlForClause(XmlForClause x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression XmlForClauseOption(XmlForClauseOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression HavingClause(HavingClause x)=>this.BooleanExpression(x.SearchCondition);
    private e.Expression MergeActionClause(MergeActionClause x){throw this.単純NotSupportedException(x);}
    private e.Expression OffsetClause(OffsetClause x){throw this.単純NotSupportedException(x);}
    private e.Expression OrderByClause(OrderByClause x){throw this.単純NotSupportedException(x);}
    private e.Expression UpdateForClause(UpdateForClause x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression FullTextCatalogAndFileGroup(FullTextCatalogAndFileGroup x){throw this.単純NotSupportedException(x);}
    private e.Expression FullTextIndexColumn(FullTextIndexColumn x){throw this.単純NotSupportedException(x);}
    private e.Expression FullTextIndexOption(FullTextIndexOption x){throw this.単純NotSupportedException(x);}
    private e.Expression FullTextStopListAction(FullTextStopListAction x){throw this.単純NotSupportedException(x);}
    private e.Expression FunctionOption(FunctionOption x)=>x switch{
        InlineFunctionOption y=>this.InlineFunctionOption(y),
        ExecuteAsFunctionOption y=>this.ExecuteAsFunctionOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression InlineFunctionOption(InlineFunctionOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ExecuteAsFunctionOption(ExecuteAsFunctionOption x){
        throw this.単純NotSupportedException(x);
    }
    //private Type FunctionReturnTypeのType(FunctionReturnType x)=>x switch{
    //    TableValuedFunctionReturnType y=>this.TableValuedFunctionReturnTypeのType(y),
    //    ScalarFunctionReturnType y=>this.ScalarFunctionReturnTypeのType(y),
    //    _=>throw this.単純NotSupportedException(x)
    //};
    private  Type FunctionReturnType(FunctionReturnType x)=>x switch{
        //TableValuedFunctionReturnType y=>this.TableValuedFunctionReturnType(y),
        ScalarFunctionReturnType y=>this.ScalarFunctionReturnType(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression GeneralSetCommand(GeneralSetCommand x){throw this.単純NotSupportedException(x);}
    private e.Expression GraphMatchCompositeExpression(GraphMatchCompositeExpression x){throw this.単純NotSupportedException(x);}
    private e.Expression GraphMatchLastNodePredicate(GraphMatchLastNodePredicate x){throw this.単純NotSupportedException(x);}
    private e.Expression GraphMatchNodeExpression(GraphMatchNodeExpression x){throw this.単純NotSupportedException(x);}
    private e.Expression GraphMatchRecursivePredicate(GraphMatchRecursivePredicate x){throw this.単純NotSupportedException(x);}
    private e.Expression GraphRecursiveMatchQuantifier(GraphRecursiveMatchQuantifier x){throw this.単純NotSupportedException(x);}
    private e.Expression GridParameter(GridParameter x){throw this.単純NotSupportedException(x);}
    private e.Expression HadrDatabaseOption(HadrDatabaseOption x)=>x switch{
        HadrAvailabilityGroupDatabaseOption y=>this.HadrAvailabilityGroupDatabaseOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression HadrAvailabilityGroupDatabaseOption(HadrAvailabilityGroupDatabaseOption x){
        throw this.単純NotSupportedException(x);
    }
    private string Identifier(Identifier x)=>x switch{
        SqlCommandIdentifier y=>this.SqlCommandIdentifier(y),
        IdentifierSnippet y=>this.IdentifierSnippet(y),
        _=>x.Value
    };
    /// <summary>
    /// Identifier:TSqlFragment
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private string SqlCommandIdentifier(SqlCommandIdentifier x)=>x.Value;
    /// <summary>
    /// Identifier:TSqlFragment
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private string IdentifierSnippet(IdentifierSnippet x)=>x.Value;
    private e.Expression IdentifierOrScalarExpression(IdentifierOrScalarExpression x){throw this.単純NotSupportedException(x);}
    private e.Expression IdentifierOrValueExpression(IdentifierOrValueExpression x){throw this.単純NotSupportedException(x);}
    private e.Expression IndexOption(IndexOption x)=>x switch{
        CompressionDelayIndexOption y=>this.CompressionDelayIndexOption(y),
        DataCompressionOption       y=>this.DataCompressionOption(y),
        FileStreamOnDropIndexOption y=>this.FileStreamOnDropIndexOption(y),
        IndexExpressionOption       y=>this.IndexExpressionOption(y),
        IndexStateOption            y=>this.IndexStateOption(y),
        MaxDurationOption           y=>this.MaxDurationOption(y),
        MoveToDropIndexOption       y=>this.MoveToDropIndexOption(y),
        OrderIndexOption            y=>this.OrderIndexOption(y),
        WaitAtLowPriorityOption     y=>this.WaitAtLowPriorityOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression OnlineIndexOption(OnlineIndexOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression IgnoreDupKeyIndexOption(IgnoreDupKeyIndexOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression IndexType(IndexType x){throw this.単純NotSupportedException(x);}
/*
    private e.Expression InsertOption(InsertOption x){throw this.単純NotSupportedException(x);}
*/
    private e.Expression JsonForClause(JsonForClause x){throw this.単純NotSupportedException(x);}
    private e.Expression JsonForClauseOption(JsonForClauseOption x){throw this.単純NotSupportedException(x);}
    private e.Expression JsonKeyValue(JsonKeyValue x){throw this.単純NotSupportedException(x);}
    private e.Expression LedgerOption(LedgerOption x){throw this.単純NotSupportedException(x);}
    private e.Expression LedgerTableOption(LedgerTableOption x){throw this.単純NotSupportedException(x);}
    private e.Expression LedgerViewOption(LedgerViewOption x){throw this.単純NotSupportedException(x);}
    private e.Expression LiteralOpenRowsetCosmosOption(LiteralOpenRowsetCosmosOption x){throw this.単純NotSupportedException(x);}
    private e.Expression LiteralOptionValue(LiteralOptionValue x){throw this.単純NotSupportedException(x);}
    //private e.Expression LiteralPayloadOption(LiteralPayloadOption x){throw this.単純NotSupportedException(x);}
    private e.Expression LocationOption(LocationOption x){throw this.単純NotSupportedException(x);}
    private e.Expression LowPriorityLockWaitOption(LowPriorityLockWaitOption x){throw this.単純NotSupportedException(x);}
    private e.Expression OnlineIndexLowPriorityLockWaitOption(OnlineIndexLowPriorityLockWaitOption x){throw this.単純NotSupportedException(x);}
    private e.Expression OpenRowsetColumnDefinition(OpenRowsetColumnDefinition x){throw this.単純NotSupportedException(x);}
    private e.Expression OpenRowsetCosmos(OpenRowsetCosmos x){throw this.単純NotSupportedException(x);}
    private e.Expression OpenRowsetCosmosOption(OpenRowsetCosmosOption x){throw this.単純NotSupportedException(x);}
    private e.Expression OperatorAuditOption(OperatorAuditOption x){throw this.単純NotSupportedException(x);}
    private object OptimizerHint(OptimizerHint x)=>x switch{
        LiteralOptimizerHint     y=>this.LiteralOptimizerHint(y),
        TableHintsOptimizerHint  y=>this.TableHintsOptimizerHint(y),
        OptimizeForOptimizerHint y=>this.OptimizeForOptimizerHint(y),
        UseHintList y=>this.UseHintList(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression OptionValue(OptionValue x)=>x switch{
        OnOffOptionValue y=>this.OnOffOptionValue(y),
        LiteralOptionValue y=>this.LiteralOptionValue(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression OnOffOptionValue(OnOffOptionValue x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression PartitionSpecifier(PartitionSpecifier x){throw this.単純NotSupportedException(x);}
    private e.Expression PayloadOption(PayloadOption x)=>x switch{
        AuthenticationPayloadOption  y=>this.AuthenticationPayloadOption(y),
        CharacterSetPayloadOption    y=>this.CharacterSetPayloadOption(y),
        EnabledDisabledPayloadOption y=>this.EnabledDisabledPayloadOption(y),
        EncryptionPayloadOption      y=>this.EncryptionPayloadOption(y),
        LiteralPayloadOption         y=>this.LiteralPayloadOption(y),
        LoginTypePayloadOption       y=>this.LoginTypePayloadOption(y),
        RolePayloadOption            y=>this.RolePayloadOption(y),
        SchemaPayloadOption          y=>this.SchemaPayloadOption(y),
        SessionTimeoutPayloadOption  y=>this.SessionTimeoutPayloadOption(y),
        SoapMethod                   y=>this.SoapMethod(y),
        WsdlPayloadOption            y=>this.WsdlPayloadOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression PredictTableReference(PredictTableReference x){throw this.単純NotSupportedException(x);}
    private e.Expression PrincipalOption(PrincipalOption x)=>x switch{
        OnOffPrincipalOption y=>this.OnOffPrincipalOption(y),
        LiteralPrincipalOption y=>this.LiteralPrincipalOption(y),
        IdentifierPrincipalOption y=>this.IdentifierPrincipalOption(y),
        PasswordAlterPrincipalOption y=>this.PasswordAlterPrincipalOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression OnOffPrincipalOption(OnOffPrincipalOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression LiteralPrincipalOption(LiteralPrincipalOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression IdentifierPrincipalOption(IdentifierPrincipalOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression PasswordAlterPrincipalOption(PasswordAlterPrincipalOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ProcedureOption(ProcedureOption x)=>x switch{
        ExecuteAsProcedureOption y=>this.ExecuteAsProcedureOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.ParameterExpression ProcedureParameter(ProcedureParameter x){
        var Type = this.Nullableまたは参照型(DataTypeReferenceからTypeに変換(x.DataType));
        //var Name=x.VariableName.Value;
        var Parameter = e.Expression.Parameter(Type,x.VariableName.Value);
        //this.Lambda_Parameters.Add(Parameter);
        //if(x.Value is null)return Default_void;
        //var Parameter=Expressions.Expression.Parameter(Type,Name);
        //this.Dictionary_Name_Parameter.Add(Name,Parameter);
        this.AddScalarVariable(Parameter);
        return Parameter;
        //if(x.Value is null) return Default_void;
        //var Right = this.ScalarExpression(x.Value);
        ////if(Right==Constant_null)Right=Expressions.Expression.Parameter(Type,Name);
        //return e.Expression.Assign(Parameter,Right);
    }
    private (PropertyInfo Schema,MethodInfo Method)ProcedureReferenceName(ProcedureReferenceName x){
        var (Schema,Method)=this.ProcedureReference(x.ProcedureReference);
        if(x.ProcedureVariable is not null){
            var ProcedureVariable=this.VariableReference(x.ProcedureVariable);
        }
        return (Schema,Method);
        //throw this.単純NotSupportedException(x);
    }
    private e.Expression QueryExpression(QueryExpression x)=>x switch{
        QueryParenthesisExpression y=>this.QueryParenthesisExpression(y),
        QuerySpecification y=>this.QuerySpecification(y),
        BinaryQueryExpression y=>this.BinaryQueryExpression(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression QueryParenthesisExpression(QueryParenthesisExpression x){
        var QueryExpression=this.QueryExpression(x.QueryExpression);
        if(x.ForClause is not null) { 
            var ForClause=this.ForClause(x.ForClause);
        }
        if(x.OffsetClause is not null) { 
            var OffsetClause=this.OffsetClause(x.OffsetClause);
        }
        if(x.OrderByClause is not null) { 
            var OrderByClause=this.OrderByClause(x.OrderByClause);
        }
        return QueryExpression;
    }
    //private partial Expressions.Expression QuerySpecification(QuerySpecification x);
    private e.Expression BinaryQueryExpression(BinaryQueryExpression x){
        var QueryExpression1=this.QueryExpression(x.FirstQueryExpression);
        this.RefPeek.Clear();
        var QueryExpression2=this.QueryExpression(x.SecondQueryExpression);
        var GenericMethodDifinition=x.BinaryQueryExpressionType switch{
            BinaryQueryExpressionType.Except=>Reflection.ExtensionSet.Except,
            BinaryQueryExpressionType.Intersect=>Reflection.ExtensionSet.Intersect,
            BinaryQueryExpressionType.Union=>Reflection.ExtensionSet.Union,
            _=>throw new NotImplementedException()
        };
        //NULLは型が確定できない、計算式によってはSingle,Doubleになるときがある
        //FirstQueryExpression  Object,Int16,String
        //SecondQueryExpression 
        //Expressions.Expression keySelector_Body1= keySelector_Body;
        List<e.Expression> List_Expression1=new(),List_Expression2=new();
        Type 内ElementType1 = IEnumerable1のT(QueryExpression1.Type),内ElementType2=IEnumerable1のT(QueryExpression2.Type);
        e.ParameterExpression 内Element1 = e.Expression.Parameter(内ElementType1,"1"),内Element2=e.Expression.Parameter(内ElementType2,"2");
        e.Expression 内ValueTuple1 =内Element1,内ValueTuple2=内Element2;
        var Item番号 = 1;
        while(true) {
            var (内Item1,内Item2)=ValueTuple_Item(ref 内ValueTuple1,ref 内ValueTuple2,ref Item番号);
            if(内Item1 is null)break;
            Debug.Assert(内Item2 is not null);
            var (外Item1,外Item2)=this.Convertデータ型を合わせるNullableは想定する(内Item1,内Item2);
            Debug.Assert(外Item1.Type==外Item2.Type);
            List_Expression1.Add(外Item1);List_Expression2.Add(外Item2);
        }
        var 作業配列=this.作業配列;
        e.NewExpression New1=CommonLibrary.ValueTupleでNewする(作業配列,List_Expression1),New2=CommonLibrary.ValueTupleでNewする(作業配列,List_Expression2);
        var 外ElementType=New1.Type;
        Debug.Assert(外ElementType==New2.Type);
        var Call1=e.Expression.Call(作業配列.MakeGenericMethod(Reflection.ExtensionSet.Select_selector,内ElementType1,外ElementType),QueryExpression1,e.Expression.Lambda(New1,作業配列.Parameters設定(内Element1)));
        var Call2=e.Expression.Call(作業配列.MakeGenericMethod(Reflection.ExtensionSet.Select_selector,内ElementType2,外ElementType),QueryExpression2,e.Expression.Lambda(New2,作業配列.Parameters設定(内Element2)));
        return e.Expression.Call(作業配列.MakeGenericMethod(GenericMethodDifinition,外ElementType),Call1,Call2);
        static (e.Expression?Item1, e.Expression?Item2)ValueTuple_Item(ref e.Expression ValueTuple1,ref e.Expression ValueTuple2,ref int Item番号) {
            if(Item番号==8) {
                FieldInfo ValueTuple1_Rest= ValueTuple1.Type.GetField("Rest")!, ValueTuple2_Rest = ValueTuple2.Type.GetField("Rest")!;
                if(ValueTuple1_Rest is null)return (null,null);
                (ValueTuple1, ValueTuple2) = (e.Expression.Field(ValueTuple1,ValueTuple1_Rest),e.Expression.Field(ValueTuple2,ValueTuple2_Rest));
                Item番号 = 2;
                FieldInfo ValueTuple1_Item1 = ValueTuple1.Type.GetField("Item1")!, ValueTuple2_Item1 = ValueTuple2.Type.GetField("Item1")!;
                return (e.Expression.Field(ValueTuple1,ValueTuple1_Item1), e.Expression.Field(ValueTuple2,ValueTuple2_Item1));
            }
            var Item= $"Item{Item番号}";
            FieldInfo ValueTuple1_Item = ValueTuple1.Type.GetField(Item)!,ValueTuple2_Item=ValueTuple2.Type.GetField(Item)!;
            Item番号++;
            return ValueTuple1_Item  is null?(null,null):(e.Expression.Field(ValueTuple1,ValueTuple1_Item), e.Expression.Field(ValueTuple2,ValueTuple2_Item));
        }
    }

    private e.Expression QuerySpecification(QuerySpecification x) {
        var 作業配列 = this.作業配列;
        //var StackSubquery単位の情報 = this._StackSubquery単位の情報;
        var x_SelectElements = x.SelectElements;
        ref var RefPeek = ref this.RefPeek;
        //Debug.Assert(RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression.Count==0);
        //var RefPeek0_List_ColumnExpression = RefPeek0.List_ColumnExpression;
        Type Element_Type;
        e.Expression Source;
        e.ParameterExpression ss;
        if(x.FromClause is not null) {
            (Source, ss)=this.FromClause(x.FromClause);
            Element_Type=ss.Type;
        } else {
            Source=e.Expression.Call(null,Reflection.Container.TABLE_DUM);
            Element_Type=IEnumerable1のT(Source.Type);
            ss=e.Expression.Parameter(Element_Type,"ss");
        }
        if(x.WhereClause is not null) {
            var predicate_Body = this.WhereClause(x.WhereClause);
            Source=e.Expression.Call(
                作業配列.MakeGenericMethod(Reflection.ExtensionSet.Where,Element_Type),
                Source,
                e.Expression.Lambda(predicate_Body,作業配列.Parameters設定(ss))
            );
        }
        //Debug.Assert(StackSubquery単位の情報==this._StackSubquery単位の情報);
        var RefPeek_List_ColumnAlias = RefPeek.List_ColumnAlias;
        var RefPeek_List_ColumnExpression = RefPeek.List_ColumnExpression;
        RefPeek_List_ColumnAlias.Clear();
        RefPeek_List_ColumnExpression.Clear();
        //Debug.Assert(RefPeek_List_ColumnAlias.Count==0);
        //Debug.Assert(RefPeek_List_ColumnExpression.Count==0);
        if(x.GroupByClause is not null) {
            //GroupBy(ss=>[keySelector_Body])
            //RefPeek0_List_ColumnAlias.Clear();
            //RefPeek0_List_ColumnAlias.Clear();
            var RefPeek_List_GroupByExpression = RefPeek.List_GroupByExpression;
            RefPeek_List_GroupByExpression.Clear();
            var keySelector_Body = this.GroupByClause(x.GroupByClause);
            var RefPeek_List_GroupByExpression_Count = RefPeek_List_GroupByExpression.Count;
            var Key = e.Expression.Parameter(keySelector_Body.Type,"Key");
            //var Key_Argument = keySelector_Body.Arguments;
            var 変換_旧Expressionを新Expression1=this.変換_旧Expressionを新Expression1;

            //{
            //    Expression ValueTuple0=Key;
            //    //Expression keySelector_Body1= keySelector_Body;
            //    var Item番号0=1;
            //    for(var a=0;a<RefPeek_List_GroupByExpression_Count;a++)
            //        RefPeek_List_GroupByExpression[a]=変換_旧Expressionを新Expression1.実行(
            //            keySelector_Body,
            //            RefPeek_List_GroupByExpression[a],
            //            ValueTuple_Item(ref ValueTuple0,ref Item番号0));
            //    //Source.GroupBy(ss =>,(Key,Group) => Group.Sum(ge =>))
            //    var Element_Type0=Source.Type.GetGenericArguments()[0];
            //}
            var Group = e.Expression.Parameter(
                作業配列.MakeGenericType(typeof(Sets.IEnumerable<>),Element_Type),
                "Group"
            );
            RefPeek.集約関数のParameter=ss;
            RefPeek.集約関数のSource=Group;
            Debug.Assert(RefPeek_List_ColumnAlias.Count==0);
            Debug.Assert(RefPeek_List_ColumnExpression.Count==0);                
            foreach(var SelectElement in x_SelectElements)
                this.SelectElement(SelectElement);
            e.Expression resultSelector_Body= CommonLibrary.ValueTupleでNewする(作業配列,RefPeek_List_ColumnExpression);
            e.Expression ValueTuple = Key;
            //Expression keySelector_Body1= keySelector_Body;
            var Item番号 = 1;
            for(var a = 0;a<RefPeek_List_GroupByExpression_Count;a++)
                resultSelector_Body=変換_旧Expressionを新Expression1.実行(
                    resultSelector_Body,
                    RefPeek_List_GroupByExpression[a],
                    ValueTuple_Item(ref ValueTuple,ref Item番号)
                );
            var GroupBy = e.Expression.Call(
                作業配列.MakeGenericMethod(Reflection.ExtensionSet.GroupBy_keySelector_resultSelector,Element_Type,keySelector_Body.Type,resultSelector_Body.Type),
                Source,
                e.Expression.Lambda(
                    keySelector_Body,
                    作業配列.Parameters設定(ss)
                ),
                e.Expression.Lambda(
                    resultSelector_Body,
                    作業配列.Parameters設定(Key,Group)
                )
            );
            return GroupBy;
        } else {
            var 集約関数が有るか=false;
            var 判定_集約関数があるか = this._判定_集約関数があるか;
            var x_SelectElements_Count=x_SelectElements.Count;
            var a = 0;
            while(true){
                if(a<x_SelectElements_Count){
                    if(判定_集約関数があるか.実行(x_SelectElements[a])){
                        集約関数が有るか=true;
                        break;
                    }
                }else{
                    if(判定_集約関数があるか.実行(x.HavingClause)) {
                        集約関数が有るか=true;
                    }
                    break;
                }
                a++;
            }

//                foreach(var SelectElement in x_SelectElements) {
//                    if(判定_集約関数があるか.実行(SelectElement)) {
//                        集約関数が有るか=true;
//                        goto 集約関数が有る;
//                    }
//                }
//                if(!集約関数が有るか&&判定_集約関数があるか.実行(x.HavingClause)) {
//                    集約関数が有るか=true;
//                }
//集約関数が有る:
            RefPeek.集約関数のParameter=ss;
            if(集約関数が有るか&&RefPeek.集約関数のSource is null) {
                //GROUP BYがない集約関数を含む1行を返すSELECT
                //MethodCallを分解するして最小の式に変換する
                //select f0=sum(f.a),f1=avg(f.b)from F f group by 1
                //TABLE_DEE.Select(_=>new{f0=F.Sum(f=>f.c),f1=F.Sum(f.b))})
                Debug.Assert(Source is not null);
                RefPeek.集約関数のSource=Source;
                Element_Type=typeof(Databases.AttributeEmpty);
                ss=e.Expression.Parameter(
                    Element_Type,
                    "_"
                );
                //TABLE_DEE.Select(ss=>selector_Body)
                //(s=new Set<>();s.Add(selector_Body);s)
                Source=TABLE_DEE;
            }
            if(x.HavingClause is not null) {
                var predicate_Body = this.HavingClause(x.HavingClause);
                Source=e.Expression.Call(
                    作業配列.MakeGenericMethod(Reflection.ExtensionSet.Where,Element_Type),
                    Source,
                    e.Expression.Lambda(
                        predicate_Body,
                        作業配列.Parameters設定(ss)
                    )
                );
            }
            //var List_ColumnExpression = RefPeek0.List_ColumnExpression;
            //List_ColumnExpression.Clear();
            //RefPeek0_List_ColumnAlias.Clear();
            Debug.Assert(RefPeek_List_ColumnAlias.Count==0);
            Debug.Assert(RefPeek_List_ColumnExpression.Count==0);
            foreach(var SelectElement in x_SelectElements)
                this.SelectElement(SelectElement);
            //if(RefPeek_List_ColumnAlias.Count==0){
            //    //select @root_id = log_id from graph--代入としてのselect
            //    return Expression.Block(RefPeek_List_ColumnExpression);
            //} else {
            //    var selector_Body = ValueTupleでNewする(作業配列,RefPeek_List_ColumnExpression,0);
            //    var Result=Expression.Call(
            //        作業配列.MakeGenericMethod(Reflection.ExtensionSet.Select_selector,Element_Type,selector_Body.Type),
            //        Source,
            //        Expression.Lambda(
            //            selector_Body,
            //            作業配列.Parameters設定(ss)
            //        )
            //    );
            //    return Result;
            //}
            var selector_Body =CommonLibrary.ValueTupleでNewする(作業配列,RefPeek_List_ColumnExpression);
            var Result=e.Expression.Call(
                作業配列.MakeGenericMethod(Reflection.ExtensionSet.Select_selector,Element_Type,selector_Body.Type),
                Source,
                e.Expression.Lambda(
                    selector_Body,
                    作業配列.Parameters設定(ss)
                )
            );
            return Result;
        }
    }
    private e.Expression QueryStoreCapturePolicyOption(QueryStoreCapturePolicyOption x){throw this.単純NotSupportedException(x);}
    private e.Expression QueryStoreDataFlushIntervalOption(QueryStoreDataFlushIntervalOption x){throw this.単純NotSupportedException(x);}
    private e.Expression QueryStoreDesiredStateOption(QueryStoreDesiredStateOption x){throw this.単純NotSupportedException(x);}
    private e.Expression QueryStoreIntervalLengthOption(QueryStoreIntervalLengthOption x){throw this.単純NotSupportedException(x);}
    private e.Expression QueryStoreMaxPlansPerQueryOption(QueryStoreMaxPlansPerQueryOption x){throw this.単純NotSupportedException(x);}
    private e.Expression QueryStoreMaxStorageSizeOption(QueryStoreMaxStorageSizeOption x){throw this.単純NotSupportedException(x);}
    private e.Expression QueryStoreOption(QueryStoreOption x)=>x switch{
        QueryStoreDesiredStateOption y=>this.QueryStoreDesiredStateOption(y),
        QueryStoreCapturePolicyOption y=>this.QueryStoreCapturePolicyOption(y),
        QueryStoreSizeCleanupPolicyOption y=>this.QueryStoreSizeCleanupPolicyOption(y),
        QueryStoreDataFlushIntervalOption y=>this.QueryStoreDataFlushIntervalOption(y),
        QueryStoreIntervalLengthOption y=>this.QueryStoreIntervalLengthOption(y),
        QueryStoreMaxStorageSizeOption y=>this.QueryStoreMaxStorageSizeOption(y),
        QueryStoreMaxPlansPerQueryOption y=>this.QueryStoreMaxPlansPerQueryOption(y),
        QueryStoreTimeCleanupPolicyOption y=>this.QueryStoreTimeCleanupPolicyOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression QueryStoreSizeCleanupPolicyOption(QueryStoreSizeCleanupPolicyOption x){throw this.単純NotSupportedException(x);}
    private e.Expression QueryStoreTimeCleanupPolicyOption(QueryStoreTimeCleanupPolicyOption x){throw this.単純NotSupportedException(x);}
    private e.Expression QueueOption(QueueOption x){throw this.単純NotSupportedException(x);}
    private e.Expression RdaTableOption(RdaTableOption x){throw this.単純NotSupportedException(x);}
    private e.Expression RemoteDataArchiveDatabaseSetting(RemoteDataArchiveDatabaseSetting x)=>x switch{
        RemoteDataArchiveDbServerSetting y=>this.RemoteDataArchiveDbServerSetting(y),
        RemoteDataArchiveDbCredentialSetting y=>this.RemoteDataArchiveDbCredentialSetting(y),
        RemoteDataArchiveDbFederatedServiceAccountSetting y=>this.RemoteDataArchiveDbFederatedServiceAccountSetting(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression RemoteDataArchiveDbServerSetting(RemoteDataArchiveDbServerSetting x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression RemoteDataArchiveDbCredentialSetting(RemoteDataArchiveDbCredentialSetting x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression RemoteDataArchiveDbFederatedServiceAccountSetting(RemoteDataArchiveDbFederatedServiceAccountSetting x){throw this.単純NotSupportedException(x);}
    private e.Expression ResourcePoolAffinitySpecification(ResourcePoolAffinitySpecification x){throw this.単純NotSupportedException(x);}
    private e.Expression ResourcePoolParameter(ResourcePoolParameter x){throw this.単純NotSupportedException(x);}
    private e.Expression RetentionDaysAuditTargetOption(RetentionDaysAuditTargetOption x){throw this.単純NotSupportedException(x);}
    private e.Expression RetentionPeriodDefinition(RetentionPeriodDefinition x){throw this.単純NotSupportedException(x);}
    private e.Expression RouteOption(RouteOption x){throw this.単純NotSupportedException(x);}
    private e.Expression RowValue(RowValue x){throw this.単純NotSupportedException(x);}
    //TSqlFragment
    private e.Expression ScalarExpression(ScalarExpression x)=>x switch{
        PrimaryExpression        y=>this.PrimaryExpression       (y),
        ExtractFromExpression    y=>this.ExtractFromExpression   (y),
        OdbcConvertSpecification y=>this.OdbcConvertSpecification(y),
        BinaryExpression         y=>this.BinaryExpression        (y),
        IdentityFunctionCall     y=>this.IdentityFunctionCall    (y),
        UnaryExpression          y=>this.UnaryExpression         (y),
        ScalarExpressionSnippet  y=>this.ScalarExpressionSnippet (y),
        SourceDeclaration        y=>this.SourceDeclaration       (y),
        _=>throw this.単純NotSupportedException(x)
    };
    //TSqlFragment.ScalarExpression
    //TSqlFragment.ScalarExpression
    //TSqlFragment.ScalarExpression
    //TSqlFragment.ScalarExpression

    
    private e.Expression SchemaObjectNameOrValueExpression(SchemaObjectNameOrValueExpression x){throw this.単純NotSupportedException(x);}
    private e.Expression SecurityElement80(SecurityElement80 x)=>x switch{
        CommandSecurityElement80 y=>this.CommandSecurityElement80(y),
        PrivilegeSecurityElement80 y=>this.PrivilegeSecurityElement80(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CommandSecurityElement80(CommandSecurityElement80 x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression PrivilegeSecurityElement80(PrivilegeSecurityElement80 x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression SecurityPolicyOption(SecurityPolicyOption x){throw this.単純NotSupportedException(x);}
    private e.Expression SecurityPredicateAction(SecurityPredicateAction x){throw this.単純NotSupportedException(x);}
    private e.Expression SecurityPrincipal(SecurityPrincipal x){throw this.単純NotSupportedException(x);}
    private e.Expression SecurityTargetObject(SecurityTargetObject x){throw this.単純NotSupportedException(x);}
    private e.Expression SecurityTargetObjectName(SecurityTargetObjectName x){throw this.単純NotSupportedException(x);}
    private e.Expression SecurityUserClause80(SecurityUserClause80 x){throw this.単純NotSupportedException(x);}
    /// <summary>
    /// create function r(@id int)return table as return(....)
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression SelectFunctionReturnType(SelectFunctionReturnType x){
        var SelectStatement=this.SelectStatement(x.SelectStatement);
        return SelectStatement;
    }
    private e.Expression SelectiveXmlIndexPromotedPath(SelectiveXmlIndexPromotedPath x){throw this.単純NotSupportedException(x);}
    private e.Expression SensitivityClassificationOption(SensitivityClassificationOption x){throw this.単純NotSupportedException(x);}
    //private e.Expression SessionTimeoutPayloadOption(SessionTimeoutPayloadOption x){throw this.単純NotSupportedException(x);}
    //private e.Expression SetClause(SetClause x)=>x switch{
    //    FunctionCallSetClause y=>this.FunctionCallSetClause(y),
    //    _=>throw this.単純NotSupportedException(x)
    //};
    private e.Expression SetCommand(SetCommand x)=>x switch{
        GeneralSetCommand y=>this.GeneralSetCommand(y),
        SetFipsFlaggerCommand y=>this.SetFipsFlaggerCommand(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression SetFipsFlaggerCommand(SetFipsFlaggerCommand x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression SqlScriptGenerator(SqlScriptGenerator x){throw this.単純NotSupportedException(x);}
    private e.Expression SqlScriptGeneratorOptions(SqlScriptGeneratorOptions x){throw this.単純NotSupportedException(x);}
    private e.Expression SqlServerlessScriptGenerator(SqlServerlessScriptGenerator x){throw this.単純NotSupportedException(x);}
    private e.Expression StatisticsOption(StatisticsOption x)=>x switch{
        ResampleStatisticsOption y=>this.ResampleStatisticsOption(y),
        OnOffStatisticsOption y=>this.OnOffStatisticsOption(y),
        LiteralStatisticsOption y=>this.LiteralStatisticsOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression StatisticsPartitionRange(StatisticsPartitionRange x){throw this.単純NotSupportedException(x);}
    private e.Expression SystemTimePeriodDefinition(SystemTimePeriodDefinition x){throw this.単純NotSupportedException(x);}
    private e.Expression TableSwitchOption(TableSwitchOption x){throw this.単純NotSupportedException(x);}
    /// <summary>
    /// @Student table(TestID int not null)
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression TableValuedFunctionReturnType(TableValuedFunctionReturnType x){
        return this.DeclareTableVariableBody(x.DeclareTableVariableBody);
    }
    private e.Expression TableXmlCompressionOption(TableXmlCompressionOption x){throw this.単純NotSupportedException(x);}
    private e.Expression TargetDeclaration(TargetDeclaration x){throw this.単純NotSupportedException(x);}
    private e.Expression TemporalClause(TemporalClause x){throw this.単純NotSupportedException(x);}
    private e.Expression TriggerObject(TriggerObject x){throw this.単純NotSupportedException(x);}
    private e.Expression TruncateTargetTableSwitchOption(TruncateTargetTableSwitchOption x){throw this.単純NotSupportedException(x);}
    private e.Expression TSqlStatement(TSqlStatement x){
        this.RefPeek.Clear();
        return x switch{
            AlterAsymmetricKeyStatement                                 y=>this.AlterAsymmetricKeyStatement(y),
            AlterAuthorizationStatement                                 y=>this.AlterAuthorizationStatement(y),
            AlterCreateEndpointStatementBase                            y=>this.AlterCreateEndpointStatementBase(y),
            AlterCreateServiceStatementBase                             y=>this.AlterCreateServiceStatementBase(y),
            AlterCryptographicProviderStatement                         y=>this.AlterCryptographicProviderStatement(y),
            AlterDatabaseScopedConfigurationStatement                   y=>this.AlterDatabaseScopedConfigurationStatement(y),
            AlterDatabaseStatement                                      y=>this.AlterDatabaseStatement(y),
            AlterFederationStatement                                    y=>this.AlterFederationStatement(y),
            AlterFullTextIndexStatement                                 y=>this.AlterFullTextIndexStatement(y),
            AlterFullTextStopListStatement                              y=>this.AlterFullTextStopListStatement(y),
            AlterLoginStatement                                         y=>this.AlterLoginStatement(y),
            AlterPartitionFunctionStatement                             y=>this.AlterPartitionFunctionStatement(y),
            AlterPartitionSchemeStatement                               y=>this.AlterPartitionSchemeStatement(y),
            AlterResourceGovernorStatement                              y=>this.AlterResourceGovernorStatement(y),
            AlterSchemaStatement                                        y=>this.AlterSchemaStatement(y),
            AlterSearchPropertyListStatement                            y=>this.AlterSearchPropertyListStatement(y),
            AlterServerConfigurationSetBufferPoolExtensionStatement     y=>this.AlterServerConfigurationSetBufferPoolExtensionStatement(y),
            AlterServerConfigurationSetDiagnosticsLogStatement          y=>this.AlterServerConfigurationSetDiagnosticsLogStatement(y),
            AlterServerConfigurationSetFailoverClusterPropertyStatement y=>this.AlterServerConfigurationSetFailoverClusterPropertyStatement(y),
            AlterServerConfigurationSetHadrClusterStatement             y=>this.AlterServerConfigurationSetHadrClusterStatement(y),
            AlterServerConfigurationSetSoftNumaStatement                y=>this.AlterServerConfigurationSetSoftNumaStatement(y),
            AlterServerConfigurationStatement                           y=>this.AlterServerConfigurationStatement(y),
            AlterServiceMasterKeyStatement                              y=>this.AlterServiceMasterKeyStatement(y),
            AlterTableStatement                                         y=>this.AlterTableStatement(y),
            AlterXmlSchemaCollectionStatement                           y=>this.AlterXmlSchemaCollectionStatement(y),
            ApplicationRoleStatement                                    y=>this.ApplicationRoleStatement(y),
            AssemblyStatement                                           y=>this.AssemblyStatement(y),
            AuditSpecificationStatement                                 y=>this.AuditSpecificationStatement(y),
            AvailabilityGroupStatement                                  y=>this.AvailabilityGroupStatement(y),
            BackupRestoreMasterKeyStatementBase                         y=>this.BackupRestoreMasterKeyStatementBase(y),
            BackupStatement                                             y=>this.BackupStatement(y),
            BeginConversationTimerStatement                             y=>this.BeginConversationTimerStatement(y),
            BeginDialogStatement                                        y=>this.BeginDialogStatement(y),
            BeginEndBlockStatement                                      y=>this.BeginEndBlockStatement(y),
            BreakStatement                                              y=>this.BreakStatement(y),
            BrokerPriorityStatement                                     y=>this.BrokerPriorityStatement(y),
            BulkInsertBase                                              y=>this.BulkInsertBase(y),
            CertificateStatementBase                                    y=>this.CertificateStatementBase(y),
            CheckpointStatement                                         y=>this.CheckpointStatement(y),
            CloseMasterKeyStatement                                     y=>this.CloseMasterKeyStatement(y),
            CloseSymmetricKeyStatement                                  y=>this.CloseSymmetricKeyStatement(y),
            ColumnEncryptionKeyStatement                                y=>this.ColumnEncryptionKeyStatement(y),
            ContinueStatement                                           y=>this.ContinueStatement(y),
            CreateAggregateStatement                                    y=>this.CreateAggregateStatement(y),
            CreateAsymmetricKeyStatement                                y=>this.CreateAsymmetricKeyStatement(y),
            CreateColumnMasterKeyStatement                              y=>this.CreateColumnMasterKeyStatement(y),
            CreateColumnStoreIndexStatement                             y=>this.CreateColumnStoreIndexStatement(y),
            CreateContractStatement                                     y=>this.CreateContractStatement(y),
            CreateCryptographicProviderStatement                        y=>this.CreateCryptographicProviderStatement(y),
            CreateDatabaseStatement                                     y=>this.CreateDatabaseStatement(y),
            CreateDefaultStatement                                      y=>this.CreateDefaultStatement(y),
            CreateEventNotificationStatement                            y=>this.CreateEventNotificationStatement(y),
            CreateFederationStatement                                   y=>this.CreateFederationStatement(y),
            CreateFullTextIndexStatement                                y=>this.CreateFullTextIndexStatement(y),
            CreateFullTextStopListStatement                             y=>this.CreateFullTextStopListStatement(y),
            CreateLoginStatement                                        y=>this.CreateLoginStatement(y),
            CreatePartitionFunctionStatement                            y=>this.CreatePartitionFunctionStatement(y),
            CreatePartitionSchemeStatement                              y=>this.CreatePartitionSchemeStatement(y),
            CreateRuleStatement                                         y=>this.CreateRuleStatement(y),
            CreateSchemaStatement                                       y=>this.CreateSchemaStatement(y),
            CreateSearchPropertyListStatement                           y=>this.CreateSearchPropertyListStatement(y),
            CreateSpatialIndexStatement                                 y=>this.CreateSpatialIndexStatement(y),
            CreateStatisticsStatement                                   y=>this.CreateStatisticsStatement(y),
            CreateSynonymStatement                                      y=>this.CreateSynonymStatement(y),
            CreateTableStatement                                        y=>this.CreateTableStatement(y),
            CreateTypeStatement                                         y=>this.CreateTypeStatement(y),
            CreateXmlSchemaCollectionStatement                          y=>this.CreateXmlSchemaCollectionStatement(y),
            CredentialStatement                                         y=>this.CredentialStatement(y),
            CursorStatement                                             y=>this.CursorStatement(y),
            DatabaseEncryptionKeyStatement                              y=>this.DatabaseEncryptionKeyStatement(y),
            DbccStatement                                               y=>this.DbccStatement(y),
            DeclareCursorStatement                                      y=>this.DeclareCursorStatement(y),
            DeclareTableVariableStatement                               y=>this.DeclareTableVariableStatement(y),
            DeclareVariableStatement                                    y=>this.DeclareVariableStatement(y),
            DiskStatement                                               y=>this.DiskStatement(y),
            DropChildObjectsStatement                                   y=>this.DropChildObjectsStatement(y),
            DropDatabaseEncryptionKeyStatement                          y=>this.DropDatabaseEncryptionKeyStatement(y),
            DropDatabaseStatement                                       y=>this.DropDatabaseStatement(y),
            DropEventNotificationStatement                              y=>this.DropEventNotificationStatement(y),
            DropFullTextIndexStatement                                  y=>this.DropFullTextIndexStatement(y),
            DropIndexStatement                                          y=>this.DropIndexStatement(y),
            DropMasterKeyStatement                                      y=>this.DropMasterKeyStatement(y),
            DropObjectsStatement                                        y=>this.DropObjectsStatement(y),
            DropQueueStatement                                          y=>this.DropQueueStatement(y),
            DropSchemaStatement                                         y=>this.DropSchemaStatement(y),
            DropTypeStatement                                           y=>this.DropTypeStatement(y),
            DropUnownedObjectStatement                                  y=>this.DropUnownedObjectStatement(y),
            DropXmlSchemaCollectionStatement                            y=>this.DropXmlSchemaCollectionStatement(y),
            EnableDisableTriggerStatement                               y=>this.EnableDisableTriggerStatement(y),
            EndConversationStatement                                    y=>this.EndConversationStatement(y),
            EventSessionStatement                                       y=>this.EventSessionStatement(y),
            ExecuteAsStatement                                          y=>this.ExecuteAsStatement(y),
            ExecuteStatement                                            y=>this.ExecuteStatement(y),
            ExternalDataSourceStatement                                 y=>this.ExternalDataSourceStatement(y),
            ExternalFileFormatStatement                                 y=>this.ExternalFileFormatStatement(y),
            ExternalLanguageStatement                                   y=>this.ExternalLanguageStatement(y),
            ExternalLibraryStatement                                    y=>this.ExternalLibraryStatement(y),
            ExternalResourcePoolStatement                               y=>this.ExternalResourcePoolStatement(y),
            ExternalStreamStatement                                     y=>this.ExternalStreamStatement(y),
            ExternalStreamingJobStatement                               y=>this.ExternalStreamingJobStatement(y),
            ExternalTableStatement                                      y=>this.ExternalTableStatement(y),
            FullTextCatalogStatement                                    y=>this.FullTextCatalogStatement(y),
            GoToStatement                                               y=>this.GoToStatement(y),
            IfStatement                                                 y=>this.IfStatement(y),
            IndexDefinition                                             y=>this.IndexDefinition(y),
            IndexStatement                                              y=>this.IndexStatement(y),
            KillQueryNotificationSubscriptionStatement                  y=>this.KillQueryNotificationSubscriptionStatement(y),
            KillStatement                                               y=>this.KillStatement(y),
            KillStatsJobStatement                                       y=>this.KillStatsJobStatement(y),
            LabelStatement                                              y=>this.LabelStatement(y),
            LineNoStatement                                             y=>this.LineNoStatement(y),
            MasterKeyStatement                                          y=>this.MasterKeyStatement(y),
            MessageTypeStatementBase                                    y=>this.MessageTypeStatementBase(y),
            MoveConversationStatement                                   y=>this.MoveConversationStatement(y),
            OpenMasterKeyStatement                                      y=>this.OpenMasterKeyStatement(y),
            OpenSymmetricKeyStatement                                   y=>this.OpenSymmetricKeyStatement(y),
            PrintStatement                                              y=>this.PrintStatement(y),
            ProcedureStatementBodyBase                                  y=>this.ProcedureStatementBodyBase(y),
            QueueStatement                                              y=>this.QueueStatement(y),
            RaiseErrorLegacyStatement                                   y=>this.RaiseErrorLegacyStatement(y),
            RaiseErrorStatement                                         y=>this.RaiseErrorStatement(y),
            ReadTextStatement                                           y=>this.ReadTextStatement(y),
            ReconfigureStatement                                        y=>this.ReconfigureStatement(y),
            RemoteServiceBindingStatementBase                           y=>this.RemoteServiceBindingStatementBase(y),
            ResourcePoolStatement                                       y=>this.ResourcePoolStatement(y),
            RestoreStatement                                            y=>this.RestoreStatement(y),
            ReturnStatement                                             y=>this.ReturnStatement(y),
            RevertStatement                                             y=>this.RevertStatement(y),
            RoleStatement                                               y=>this.RoleStatement(y),
            RouteStatement                                              y=>this.RouteStatement(y),
            SecurityPolicyStatement                                     y=>this.SecurityPolicyStatement(y),
            SecurityStatement                                           y=>this.SecurityStatement(y),
            SecurityStatementBody80                                     y=>this.SecurityStatementBody80(y),
            SendStatement                                               y=>this.SendStatement(y),
            SequenceStatement                                           y=>this.SequenceStatement(y),
            ServerAuditStatement                                        y=>this.ServerAuditStatement(y),
            //SetClause                                                   y=>this.SetClause(y),
            SetCommandStatement                                         y=>this.SetCommandStatement(y),
            SetErrorLevelStatement                                      y=>this.SetErrorLevelStatement(y),
            SetOnOffStatement                                           y=>this.SetOnOffStatement(y),
            SetRowCountStatement                                        y=>this.SetRowCountStatement(y),
            SetTextSizeStatement                                        y=>this.SetTextSizeStatement(y),
            SetTransactionIsolationLevelStatement                       y=>this.SetTransactionIsolationLevelStatement(y),
            SetUserStatement                                            y=>this.SetUserStatement(y),
            SetVariableStatement                                        y=>this.SetVariableStatement(y),
            ShutdownStatement                                           y=>this.ShutdownStatement(y),
            SignatureStatementBase                                      y=>this.SignatureStatementBase(y),
            StatementWithCtesAndXmlNamespaces                           y=>this.StatementWithCtesAndXmlNamespaces(y),
            SymmetricKeyStatement                                       y=>this.SymmetricKeyStatement(y),
            TSqlStatementSnippet                                        y=>this.TSqlStatementSnippet(y),
            TextModificationStatement                                   y=>this.TextModificationStatement(y),
            ThrowStatement                                              y=>this.ThrowStatement(y),
            TransactionStatement                                        y=>this.TransactionStatement(y),
            TriggerStatementBody                                        y=>this.TriggerStatementBody(y),
            TruncateTableStatement                                      y=>this.TruncateTableStatement(y),
            TryCatchStatement                                           y=>this.TryCatchStatement(y),
            UpdateStatisticsStatement                                   y=>this.UpdateStatisticsStatement(y),
            UseFederationStatement                                      y=>this.UseFederationStatement(y),
            UseStatement                                                y=>this.UseStatement(y),
            UserStatement                                               y=>this.UserStatement(y),
            ViewStatementBody                                           y=>this.ViewStatementBody(y),
            WaitForStatement                                            y=>this.WaitForStatement(y),
            WaitForSupportedStatement                                   y=>this.WaitForSupportedStatement(y),
            WhileStatement                                              y=>this.WhileStatement(y),
            WorkloadGroupStatement                                      y=>this.WorkloadGroupStatement(y),
            _=>throw this.単純NotSupportedException(x)
        };
    }
    private e.Expression UserLoginOption(UserLoginOption x){throw this.単純NotSupportedException(x);}
    private e.Expression ViewDistributionOption(ViewDistributionOption x){throw this.単純NotSupportedException(x);}
    private e.Expression ViewDistributionPolicy(ViewDistributionPolicy x){throw this.単純NotSupportedException(x);}
    private e.Expression ViewForAppendOption(ViewForAppendOption x){throw this.単純NotSupportedException(x);}
    private e.Expression ViewHashDistributionPolicy(ViewHashDistributionPolicy x){throw this.単純NotSupportedException(x);}
    private e.Expression ViewOption(ViewOption x){throw this.単純NotSupportedException(x);}
    private e.Expression ViewRoundRobinDistributionPolicy(ViewRoundRobinDistributionPolicy x){throw this.単純NotSupportedException(x);}
    private e.Expression WindowClause(WindowClause x){throw this.単純NotSupportedException(x);}
    private e.Expression WindowDefinition(WindowDefinition x){throw this.単純NotSupportedException(x);}
    private e.Expression WithCtesAndXmlNamespaces(WithCtesAndXmlNamespaces x){
        var StackSubquery単位の情報=this._StackSubquery単位の情報;
        //StackSubquery単位の情報.Push();
        //WITH graph AS(
        //    SELECT *
        //    FROM v x
        //    WHERE x.log_id=@log_id
        //    UNION ALL
        //    SELECT node.* FROM v node
        //    INNER JOIN graph AS leaf ON(node.id=leaf.parent)
        //)
        //var graph=from x in v select * where
        //while(true){
        //    var t=from node in v join leaf in graph on node.id=leaf.parent select *
        //    graph.Add(t)
        //    if(t.Count==0)break;
        //}
        //
        //    
        if(x.ChangeTrackingContext is not null)this.ValueExpression(x.ChangeTrackingContext);
        //ref var ref__Subquery単位の情報=ref this._StackSubquery単位の情報.RefPeek;
        //var Dictionary_DatabaseSchemaTable_ColumnExpression=ref__Subquery単位の情報.Dictionary_DatabaseSchemaTable_ColumnExpression;
        //var List_ColumnAlias=ref__Subquery単位の情報.List_ColumnAlias;
        //var List_ColumnExpression=ref__Subquery単位の情報.List_ColumnExpression;
        //foreach(var CommonTableExpression in x.CommonTableExpressions){
        //    var(共通部分式名,_)=this.CommonTableExpression(CommonTableExpression);
        //    var List_Select_Count=List_ColumnExpression.Count;
        //    for(var a=0;a<List_Select_Count;a++){
        //        Dictionary_DatabaseSchemaTable_ColumnExpression.Add(
        //            共通部分式名+List_ColumnAlias[a],
        //            List_ColumnExpression[a]
        //        );
        //    }
        //}
        if(x.XmlNamespaces is not null){
            this.XmlNamespaces(x.XmlNamespaces);
        }
        var x_CommonTableExpressions=x.CommonTableExpressions;
        var x_CommonTableExpressions_Count=x_CommonTableExpressions.Count;
        if(x_CommonTableExpressions_Count==0)return Default_void;
        var List_Assign=new List<e.Expression>(x_CommonTableExpressions_Count);
        //ref var RefPeek=ref this.RefPeek;
        for(var a=0;a<x_CommonTableExpressions_Count;a++)
            List_Assign.Add(this.CommonTableExpression(x_CommonTableExpressions[a]));
        //StackSubquery単位の情報.Pop();
        if(List_Assign.Count==1)return List_Assign[0];
        return e.Expression.Block(List_Assign);
    }
    private e.Expression WorkloadClassifierOption(WorkloadClassifierOption x){throw this.単純NotSupportedException(x);}
    private e.Expression WorkloadGroupParameter(WorkloadGroupParameter x)=>x switch{
        WorkloadGroupResourceParameter y=>this.WorkloadGroupResourceParameter(y),
        WorkloadGroupImportanceParameter y=>this.WorkloadGroupImportanceParameter(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression XmlCompressionOption(XmlCompressionOption x){throw this.単純NotSupportedException(x);}
    private (PropertyInfo Schema,MemberInfo Method) MultiPartIdentifier(MultiPartIdentifier x)=>x switch{
        SchemaObjectName y=>this.SchemaObjectName(y),
        _=>throw this.単純NotSupportedException(x)
    };
    /// <summary>
    /// PrimaryExpression:ScalarExpression:TSqlFragment
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression Literal(Literal x)=>x switch{
        IdentifierLiteral y=>this.IdentifierLiteral(y),
        IntegerLiteral    y=>this.IntegerLiteral(y),
        NumericLiteral    y=>this.NumericLiteral(y),
        RealLiteral       y=>this.RealLiteral(y),
        MoneyLiteral      y=>this.MoneyLiteral(y),
        BinaryLiteral     y=>this.BinaryLiteral(y),
        StringLiteral     y=>this.StringLiteral(y),
        NullLiteral       y=>this.NullLiteral(y),
        DefaultLiteral    y=>this.DefaultLiteral(y),
        MaxLiteral        y=>this.MaxLiteral(y),
        OdbcLiteral       y=>this.OdbcLiteral(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression IdentifierLiteral(IdentifierLiteral x){
        throw this.単純NotSupportedException(x);
    }
    //private e.Expression IntegerLiteral(IntegerLiteral x)=>e.Expression.Constant(int.Parse(x.Value));
    //private static e.Expression 共通Literal(Literal x,e.ConstantExpression nullable)=>string.Equals(x.Value,"null",StringComparison.OrdinalIgnoreCase)?nullable_int:e.Expression.Constant(int.Parse(x.Value))
    private static readonly e.ConstantExpression nullable_int=e.Expression.Constant(null,typeof(int?));
    private e.Expression IntegerLiteral(IntegerLiteral x)=>string.Equals(x.Value,"null",StringComparison.OrdinalIgnoreCase)?nullable_int:e.Expression.Constant(int.Parse(x.Value));
    private e.Expression NumericLiteral(NumericLiteral x)=>string.Equals(x.Value,"null",StringComparison.OrdinalIgnoreCase)?nullable_int:e.Expression.Constant(decimal.Parse(x.Value));
    private e.Expression RealLiteral(RealLiteral x)=>string.Equals(x.Value,"null",StringComparison.OrdinalIgnoreCase)?nullable_int:e.Expression.Constant(double.Parse(x.Value));
    private e.Expression MoneyLiteral(MoneyLiteral x)=>string.Equals(x.Value,"null",StringComparison.OrdinalIgnoreCase)?nullable_int:e.Expression.Constant(decimal.Parse(x.Value));
    private e.Expression BinaryLiteral(BinaryLiteral x){
        Debug.Assert("0x"==x.Value[..2]);
        var x_Value=x.Value[2..];
        if(x_Value.Length%2==1)x_Value='0'+x_Value;
        var Bytes = new byte[x_Value.Length/2];
        var index =0;
        for(var a=0;a<x_Value.Length;a+=2){
            Debug.Assert(x_Value.Substring(a,2)==x_Value[a..(a+2)]);
            var Byte=Convert.ToByte(x_Value[a..(a+2)],16);
            Bytes[index++]=Byte;
        }
        return e.Expression.Constant(Bytes);
    }
    private e.Expression StringLiteral(StringLiteral x)=>e.Expression.Constant(x.Value);
    private e.Expression NullLiteral(NullLiteral x)=>Constant_null;
    private e.Expression DefaultLiteral(DefaultLiteral x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression MaxLiteral(MaxLiteral x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression OdbcLiteral(OdbcLiteral x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression WlmTimeLiteral(WlmTimeLiteral x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression VariableReference(VariableReference x)=>
        this.FindParameterVariable(x.Name);
    //this.Block_Variables.Concat(this.Lambda_Parameters).Single(p=>p.Name==x.Name);
    /// <summary>
    /// @@servername,@@rowcountとか
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression GlobalVariableExpression(GlobalVariableExpression x){
        Debug.Assert("@@"==x.Name[..2]);
        var Name=x.Name[2..];
        var Property=typeof(Product.SQLServer.Methods).GetProperties(BindingFlags.Static|BindingFlags.Public|BindingFlags.DeclaredOnly).Where(p => string.Equals(p.Name,Name,StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
        if(Property is not null)
            return e.Expression.Property(null,Property);
        if(Name=="IDENTITY")
            return e.Expression.Default(typeof(int));
        if(Name=="ROWCOUNT")
            return e.Expression.Default(typeof(int));
        throw new NotImplementedException(Name);
    }
    //private static readonly RuntimeBinder.CSharpArgumentInfo CSharpArgumentInfo = RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None,null);
    private struct 一致度:IComparable<一致度> {
        private readonly int 引数数差;
        private readonly int 親クラスとの近さ;
        private readonly int インターフェース;
        private readonly int 暗黙型変換;
        public 一致度(int 引数数差,int 親クラスとの近さ,int インターフェース,int 暗黙型変換) {
            this.引数数差=引数数差;
            this.親クラスとの近さ=親クラスとの近さ;
            this.インターフェース=インターフェース;
            this.暗黙型変換=暗黙型変換;
        }
        public int CompareTo(一致度 other){
            int x;
            x=this.引数数差-other.引数数差; if(x!=0) return x;
            x=this.親クラスとの近さ-other.親クラスとの近さ; if(x!=0) return x;
            x=this.インターフェース-other.インターフェース; if(x!=0) return x;
            x=this.暗黙型変換-other.暗黙型変換; if(x!=0) return x;
            return 0;
        }
    }
    public readonly SortedDictionary<string,string>Functions=new();
    /// <summary>
    /// ScalarSubquery orPrimaryExpression:ScalarExpression:TSqlFragment
    /// (1列を返すSELECT...)呼び出した側が1行だけ使うか複数(IN)使うか判断。EXISTS(SELECT...)はScalarSubqueryではない
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression Subquery(ScalarSubquery x){
        var StackSubquery単位の情報=this._StackSubquery単位の情報;
        StackSubquery単位の情報.Push();
        var MethodCall=(e.MethodCallExpression)this.QueryExpression(x.QueryExpression);
        StackSubquery単位の情報.Pop();
        var MethodCall_Arguments = MethodCall.Arguments;
        var Source=MethodCall_Arguments[0];
        var Lambda= (e.LambdaExpression)MethodCall_Arguments[1];
        var Parameters = Lambda.Parameters;
        var Lambda_Body=nameof(ExtensionSet.Select)==MethodCall.Method.Name?((e.NewExpression)Lambda.Body).Arguments[0]:e.Expression.Field(Parameters[0],nameof(ValueTuple<int>.Item1));
        var 作業配列=this.作業配列;
        var Lambda_Body_Type=Lambda_Body.Type;
        return e.Expression.Call(
            作業配列.MakeGenericMethod(Reflection.ExtensionSet.Select_selector,IEnumerable1のT(Source.Type),Lambda_Body_Type),
            Source,e.Expression.Lambda(Lambda_Body,Parameters)
        );
    }
    private static e.Expression Stringをキャスト(e.Expression Left,string Right){
        var Left_Type=Left.Type;
        if(Left_Type==typeof(string))return Left;
        var Constant=e.Expression.Constant(Right);
        if(typeof(byte)==Left_Type||typeof(ushort)==Left_Type||typeof(uint)==Left_Type||typeof(char)==Left_Type||typeof(sbyte)==Left_Type||typeof(short)==Left_Type||typeof(int)==Left_Type){
            return e.Expression.Call(Reflection.Int32.Parse_s,Constant);
        }else if(typeof(byte?)==Left_Type||typeof(ushort?)==Left_Type||typeof(uint?)==Left_Type||typeof(char?)==Left_Type||typeof(sbyte?)==Left_Type||typeof(short?)==Left_Type||typeof(int?)==Left_Type){
            return e.Expression.Convert(
                e.Expression.Call(Reflection.Int32.Parse_s,Constant),
                typeof(int?)
            );
        }else if(typeof(ulong)==Left_Type||typeof(long)==Left_Type){
            return e.Expression.Call(Reflection.Int64.Parse_s,Constant);
        }else if(typeof(ulong?)==Left_Type||typeof(long?)==Left_Type){
            return e.Expression.Convert(
                e.Expression.Call(Reflection.Int64.Parse_s,Constant),
                typeof(long?)
            );
        }else if(typeof(float)==Left_Type){
            return e.Expression.Call(Reflection.Single.Parse_s,Constant);
        }else if(typeof(double)==Left_Type){
            return e.Expression.Call(Reflection.Double.Parse_s,Constant);
        }else if(typeof(float?)==Left_Type){
            return e.Expression.Convert(
                e.Expression.Call(Reflection.Single.Parse_s,Constant),
                Left_Type
            );
        }else if(typeof(double?)==Left_Type){
            return e.Expression.Convert(
                e.Expression.Call(Reflection.Double.Parse_s,Constant),
                Left_Type
            );
        }
        throw new NotSupportedException($"\"{Right}\"を{Left_Type.FullName}にキャストできない。");
    }
    //,Dictionary<(Type Left,Type Right),Type> Dictionary

    /// <summary>
    /// TSqlFragment
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression StatementList(StatementList x)=>x switch{
        StatementListSnippet y=>this.StatementListSnippet(y),
        _=>this.Statements(x.Statements)
    };
    /// <summary>
    /// StatementListSnippet:TSqlFragment
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression StatementListSnippet(StatementListSnippet x){
        throw this.単純NotSupportedException(x);
    }









    
    
    private e.Expression AlterViewStatement(AlterViewStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateViewStatement(CreateViewStatement x){
        this.Statementの共通初期化();
        var ContainerType=this.ContainerType;
        //var x_Name=x.Name;
        var x_SchemaObjectName=x.SchemaObjectName;
        var Schema=x_SchemaObjectName.SchemaIdentifier is null ? "dbo":x_SchemaObjectName.SchemaIdentifier.Value;
        //var Schema=x_Name.SchemaIdentifier is null ? "dbo" orx_Name.SchemaIdentifier.Value;
        var Schema_FulllName=this.ContainerType.Namespace+".Schemas."+Schema;
        var Schema_Type=ContainerType.Assembly.GetType(Schema_FulllName.Replace("*",@"\*"),true,true);
        Debug.Assert(Schema_Type!=null);
        var x_SchemaObjectName_BaseIdentifier_Value=x_SchemaObjectName.BaseIdentifier.Value;
        //var View = Schema_Type.GetProperty(x_SchemaObjectName_BaseIdentifier_Value,BindingFlags.Public|BindingFlags.Instance);
        var View=Schema_Type.GetProperties(BindingFlags.Public|BindingFlags.Instance).Where(p=>string.Equals(p.Name,x_SchemaObjectName_BaseIdentifier_Value,StringComparison.OrdinalIgnoreCase)).Single();
        //Schema_PropertyInfo=ContainerType.GetProperties(BindingFlags).Where(p => string.Equals(p.Name,Schema,StringComparison.OrdinalIgnoreCase)).Single();

        var SelectStatement =this.SelectStatement(x.SelectStatement);
        //このブロックはSelectによりValueTupleを専用型にラップする
        {

            var 内ElementType=SelectStatement.Type.GetGenericArguments()[0];
            var TypeName=$"{this.ContainerType.Namespace}.Views.{Schema}.{x_SchemaObjectName.BaseIdentifier.Value}";
            var 外ElementType=ContainerType.Assembly.GetType(TypeName.Replace("*",@"\*"),true,true);
            Debug.Assert(外ElementType!=null);
            var p=e.Expression.Parameter(内ElementType,"p");
            var Constructor=外ElementType.GetConstructors()[0];
            var Parameters=Constructor.GetParameters();
            var Parameters_Length=Constructor.GetParameters().Length;
            var NewArguments=new e.Expression[Parameters_Length];
            var 作業配列=this.作業配列;
            e.Expression ValueTuple=p;
            var Item番号=1;
            for(var a=0;a<Parameters_Length;a++){
                var Item=ValueTuple_Item(ref ValueTuple,ref Item番号);
                NewArguments[a]=this.Convertデータ型を合わせるNullableは想定する(Item,Parameters[a].ParameterType);
            }
            var Call=e.Expression.Call(
                作業配列.MakeGenericMethod(Reflection.ExtensionSet.Select_selector,内ElementType,外ElementType),
                SelectStatement,
                e.Expression.Lambda(
                    e.Expression.New(Constructor,NewArguments),
                    作業配列.Parameters設定(p)
                )
            );
            var Block=e.Expression.Block(this.List_ScalarVariable,Call);
            return e.Expression.Lambda(
                作業配列.MakeGenericType(typeof(Func<>),View.PropertyType),
                Block,
                x_SchemaObjectName_BaseIdentifier_Value,
                this.List_Parameter
            );
        }
    }
    private e.Expression CreateOrAlterViewStatement(CreateOrAlterViewStatement x){
        throw this.単純NotSupportedException(x);
    }
    private void Statementの共通初期化() {
        this._StackSubquery単位の情報.Clear();
        this.Dictionary_With名_Set_ColumnAliases.Clear();
        this.List_ScalarVariable.Clear();
        this.List_匿名型TableVariable.Clear();
        this.List_定義型TableVariable.Clear();
        this.List_Parameter.Clear();
    }
    private e.Expression DataModificationStatement(DataModificationStatement x)=>x switch{
        DeleteStatement y=>this.DeleteStatement(y),
        InsertStatement y=>this.InsertStatement(y),
        UpdateStatement y=>this.UpdateStatement(y),
        MergeStatement y=>this.MergeStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression DeleteStatement(DeleteStatement x){
        var a0=this.DeleteSpecification(x.DeleteSpecification);
        if(x.WithCtesAndXmlNamespaces is not null)this.WithCtesAndXmlNamespaces(x.WithCtesAndXmlNamespaces);
        //this.OptimizerHints(x.OptimizerHints);
        return a0;
    }
    /// <summary>
    /// InsertStatement:DataModificationStatement:StatementWithCtesAndXmlNamespaces:TSqlStatement:TSqlFragment
    /// insert into @Student select @gtinyint + 4200000000 as tinyint
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression InsertStatement(InsertStatement x){
        var InsertSpecification=this.InsertSpecification(x.InsertSpecification);
        if(x.WithCtesAndXmlNamespaces is not null)this.WithCtesAndXmlNamespaces(x.WithCtesAndXmlNamespaces);
        return InsertSpecification;
    }
    private e.Expression UpdateStatement(UpdateStatement x){
        //s.UpdateWith(p=>p.diagram_id=@DiagId,p=>p.definition=@definition)
        var UpdateSpecification=this.UpdateSpecification(x.UpdateSpecification);
        return UpdateSpecification;
        //return Expressions.Expression.Call(
        //    UpdateSpecification,
        //    作業配列.MakeGenericMethod(
        //        Reflection.ExtendSet.UpdateWith,
        //        UpdateSpecification.Type
        //    )
        //);
    }
    //private e.Expression SetStatisticsStatement(SetStatisticsStatement xxpression OpenCursorStatem
    //    throw this.単純NotSupportedException(x);
    //}
    //private e.Expression SetOffsetsStatement(SetOffsetsStatement x){
    //    throw this.単純NotSupportedException(x);
    //}
    //private e.Expression SetIdentityInsertStatement(SetIdentityInsertStatement x){
    //    throw this.単純NotSupportedException(x);
    //}
    //private e.Expression SetRowCountStatement(SetRowCountStatement x){
    //    throw this.単純NotSupportedException(x);
    //}
    //private e.Expression SetCommandStatement(SetCommandStatement x){
    //    throw this.単純NotSupportedException(x);
    //}
    //private e.Expression SetTransactionIsolationLevelStatement(SetTransactionIsolationLevelStatement x){
    //    throw this.単純NotSupportedException(x);
    //}
    //private e.Expression SetTextSizeStatement(SetTextSizeStatement x){
    //    throw this.単純NotSupportedException(x);
    private e.Expression ResultSetsExecuteOption(ResultSetsExecuteOption x){
        throw this.単純NotSupportedException(x);
    }
    /// <summary>
    /// TSqlFragment
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression ResultSetDefinition(ResultSetDefinition x)=>x switch{
        InlineResultSetDefinition y=>this.InlineResultSetDefinition(y),
        SchemaObjectResultSetDefinition y=>this.SchemaObjectResultSetDefinition(y),
        _=>throw this.単純NotSupportedException(x)
    };
    /// <summary>
    /// ResultSetDefinition:TSqlFragment
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression InlineResultSetDefinition(InlineResultSetDefinition x){
        throw this.単純NotSupportedException(x);
    }
    /// <summary>
    /// ResultSetDefinition:TSqlFragment
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression SchemaObjectResultSetDefinition(SchemaObjectResultSetDefinition x){
        throw this.単純NotSupportedException(x);
    }
    /// <summary>
    /// TSqlFragment
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression ResultColumnDefinition(ResultColumnDefinition x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ExecuteSpecification(ExecuteSpecification x){
        var (Schema,Method)=this.ExecutableEntity(x.ExecutableEntity);
        if(x.ExecuteContext is not null){
            var ExecuteContext=this.ExecuteContext(x.ExecuteContext);
        }
        if(x.LinkedServer is not null){
            var LinkedServer=this.Identifier(x.LinkedServer);
        }
        if(x.Variable is not null){
            var Variable=this.VariableReference(x.Variable);
        }
        return e.Expression.Call(
            e.Expression.Property(
                this.Container,
                Schema
            ),
            Method
        );
    }
    private e.Expression ExecuteContext(ExecuteContext x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ExecuteParameter(ExecuteParameter x){
        throw this.単純NotSupportedException(x);
    }
    //private e.Expression ProcedureReferenceName(ProcedureReferenceName x){
    //    throw this.単純NotSupportedException(x);
    //}
    //private e.Expression ViewOption(ViewOption x){
    //    throw this.単純NotSupportedException(x);
    //}
    //private e.Expression TriggerObject(TriggerObject x){
    //    throw this.単純NotSupportedException(x);
    //}
    private e.Expression TriggerOption(TriggerOption x)=>x switch{
        ExecuteAsTriggerOption y=>this.ExecuteAsTriggerOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression ExecuteAsTriggerOption(ExecuteAsTriggerOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression TriggerAction(TriggerAction x){
        throw this.単純NotSupportedException(x);
    }
    private (PropertyInfo Schema,MethodInfo Method)ProcedureReference(ProcedureReference x){
        var (Schema,Member)=this.SchemaObjectName(x.Name);
        if(x.Number is not null){
            var Number=this.Literal(x.Number);
        }
        return (Schema,(MethodInfo)Member);
        //throw this.単純NotSupportedException(x);
    }
    private e.Expression MethodSpecifier(MethodSpecifier x){
        throw this.単純NotSupportedException(x);
    }
    /// <summary>
    /// create view autoadmin_backup_configurations as
    /// [with xmlnamespaces(N'http://schemas.datacontract.org/2004/07/Microsoft.SqlServer.SmartAdmin.SmartBackupAgent' as sb)]
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private void XmlNamespaces(XmlNamespaces x){
    }
    private e.Expression XmlNamespacesElement(XmlNamespacesElement x)=>x switch{
        XmlNamespacesDefaultElement y=>this.XmlNamespacesDefaultElement(y),
        XmlNamespacesAliasElement y=>this.XmlNamespacesAliasElement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression XmlNamespacesDefaultElement(XmlNamespacesDefaultElement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression XmlNamespacesAliasElement(XmlNamespacesAliasElement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CommonTableExpression(CommonTableExpression x){
        //WITH ExpressionName AS(
        //    SELECT *
        //    FROM v x
        //    WHERE x.log_id=@log_id
        //    UNION ALL
        //    SELECT node.* FROM v node
        //    INNER JOIN ExpressionName AS leaf ON(node.id=leaf.parent)
        //)
        //var ExpressionName=from x in v select * where
        //while(true){
        //    var t=from node in v join leaf in graph on node.id=leaf.parent select *
        //    ExpressionName.AddRange(t)
        //    if(t.Count==0)break;
        //}
        e.Expression Result;
        var WITH名=this.Identifier(x.ExpressionName);
        //var QueryExpression=this.QueryExpression(x.QueryExpression);
        var StackSubquery単位の情報=this._StackSubquery単位の情報;
        StackSubquery単位の情報.Push();
        ref var RefPeek12=ref this.RefPeek;
        ref var RefPeek1=ref StackSubquery単位の情報.RefPeek;
        Debug.Assert(RefPeek12.Dictionary_DatabaseSchemaTable_ColumnExpression==RefPeek1.Dictionary_DatabaseSchemaTable_ColumnExpression);
        var RefPeek1_List_ColumnAlias=RefPeek1.List_ColumnAlias;
        if(x.QueryExpression is BinaryQueryExpression BinaryQueryExpression){
            var FirstQueryExpression=this.QueryExpression(BinaryQueryExpression.FirstQueryExpression);
            var 作業配列=this.作業配列;
            var SetType=作業配列.MakeGenericType(typeof(Set<>),IEnumerable1のT(FirstQueryExpression.Type));
            var UnionWith=SetType.GetMethod(nameof(Set<int>.UnionWith));
            var SetParameter=e.Expression.Parameter(SetType,WITH名+"Set");
            //var Parameter=e.Expression.Parameter(QueryExpression.Type,WITH名+"Set");
            this.AddTableVariable(SetParameter);
            this.RefPeek.Clear();
            var SecondQueryExpression=this.QueryExpression(BinaryQueryExpression.SecondQueryExpression);
            var LongCount0=e.Expression.Parameter(typeof(long),"LongCount0");
            var LongCount1=e.Expression.Parameter(typeof(long),"LongCount0");
            var LongCount=e.Expression.Call(SetParameter,Reflection.ImmutableSet.get_LongCount);
            var Break=e.Expression.Label();
            Result=e.Expression.Block(
                作業配列.Parameters設定(LongCount0,LongCount1),
                e.Expression.Assign(
                    SetParameter,
                    e.Expression.New(SetType)
                ),
                e.Expression.Call(
                    SetParameter,
                    UnionWith,
                    FirstQueryExpression
                ),
                e.Expression.Loop(
                    e.Expression.Block(
                        e.Expression.Assign(LongCount0,LongCount),
                        e.Expression.Call(
                            SetParameter,
                            UnionWith,
                            SecondQueryExpression
                        ),
                        e.Expression.IfThenElse(
                            e.Expression.Equal(LongCount1,LongCount),
                            e.Expression.Break(Break),
                            e.Expression.Assign(LongCount1,LongCount)
                        )
                    ),
                    Break
                )
            );
        } else{
            Result=this.QueryExpression(x.QueryExpression);
        }
        var RefPeek1_List_ColumnAlias_Count=RefPeek1_List_ColumnAlias.Count;
        var RefPeek1_List_アスタリスクColumnAlias=RefPeek1.List_アスタリスクColumnAlias;
        StackSubquery単位の情報.Pop();
        //ref var RefPeek0=ref this.RefPeek;
        //var RefPeek0_List_アスタリスクColumnAlias=RefPeek0.List_アスタリスクColumnAlias;
        var ElementParameter=e.Expression.Parameter(Result.Type.GetGenericArguments()[0],WITH名);
        string[]ColumnAliases;
        var AliasExpressins=new(string Name,e.Expression Expression)[RefPeek1_List_ColumnAlias_Count];
        if(x.Columns.Count>0){
            ColumnAliases=x.Columns.Select(this.Identifier).ToArray();
        } else{
            ColumnAliases=RefPeek1_List_アスタリスクColumnAlias.ToArray();
        }
        {
            e.Expression ValueTuple=ElementParameter;
            var Item番号=1;
            for(var a=0;a<RefPeek1_List_ColumnAlias_Count;a++){
                var Item=ValueTuple_Item(ref ValueTuple,ref Item番号);
                AliasExpressins[a]=(ColumnAliases[a],Item);
            }
        }
        this.Dictionary_With名_Set_ColumnAliases.Add(WITH名,(Result,ElementParameter,AliasExpressins));
        return Result;
    }
    //private e.Expression Type FunctionReturnType(FunctionReturnType x)=>x switch{
    //    TableValuedFunctionReturnType y=>this.TableValuedFunctionReturnType(y),
    //    ScalarFunctionReturnType y=>this.ScalarFunctionReturnType(y),
    //    SelectFunctionReturnType y=>this.SelectFunctionReturnType(y),
    //    _=>throw this.単純NotSupportedException(x)
    //};
    private Type ScalarFunctionReturnTypeのType(ScalarFunctionReturnType x){
        var DataType=DataTypeReferenceからTypeに変換(x.DataType);
        return DataType;
    }
    private Type ScalarFunctionReturnType(ScalarFunctionReturnType x){
        var DataType=DataTypeReferenceからTypeに変換(x.DataType);
        return DataType;
    }
    private Type DataTypeReference(DataTypeReference x)=>x switch{
        ParameterizedDataTypeReference y=>this.ParameterizedDataTypeReference(y),
        XmlDataTypeReference y=>this.XmlDataTypeReference(y),
        _=>throw this.単純NotSupportedException(x)
    };
    /// <summary>
    /// ContactID int primary key not null
    /// FirstName nvarchar (50) null
    /// LastName nvarchar (50) null
    /// JobTitle nvarchar (50) null
    /// ContactType nvarchar (50) null
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private (Type,string[]Names)TableDefinition(TableDefinition x){
        foreach(var TableConstraint in x.TableConstraints) {
            this.ConstraintDefinition(TableConstraint);
        }
        foreach(var Index in x.Indexes)
            this.IndexDefinition(Index);
        if(x.SystemTimePeriod is not null)this.SystemTimePeriodDefinition(x.SystemTimePeriod);
        var x_ColumnDefinitions=x.ColumnDefinitions;
        var x_ColumnDefinitions_Count= x_ColumnDefinitions.Count;
        var Names=new string[x_ColumnDefinitions_Count];
        for(var a = 0;a<x_ColumnDefinitions_Count;a++)
            Names[a]=x_ColumnDefinitions[a].ColumnIdentifier.Value;
        var ElementType=ValueTupleでNewする(this.作業配列,x_ColumnDefinitions,0);
        return (this.作業配列.MakeGenericType(typeof(Set<>),ElementType),Names);
//            foreach(var ColumnDefinition in x.ColumnDefinitions)
//.              this.ColumnDefinition(ColumnDefinition);
 //       throw this.単純NotSupportedException(x);
        Type ValueTupleでNewする(作業配列 作業配列,IList<ColumnDefinition> Arguments,int Offset) {
            var 残りType数 = Arguments.Count-Offset;
            switch(残りType数) {
                case 1:return 作業配列.MakeGenericType(
                    Reflection.ValueTuple.ValueTuple1,
                    this.ColumnDefinition(Arguments[Offset+0])
                );
                case 2:return 作業配列.MakeGenericType(
                    Reflection.ValueTuple.ValueTuple2,
                    this.ColumnDefinition(Arguments[Offset+0]),
                    this.ColumnDefinition(Arguments[Offset+1])
                );
                case 3:return 作業配列.MakeGenericType(
                    Reflection.ValueTuple.ValueTuple3,
                    this.ColumnDefinition(Arguments[Offset+0]),
                    this.ColumnDefinition(Arguments[Offset+1]),
                    this.ColumnDefinition(Arguments[Offset+2])
                );
                case 4:return 作業配列.MakeGenericType(
                    Reflection.ValueTuple.ValueTuple4,
                    this.ColumnDefinition(Arguments[Offset+0]),
                    this.ColumnDefinition(Arguments[Offset+1]),
                    this.ColumnDefinition(Arguments[Offset+2]),
                    this.ColumnDefinition(Arguments[Offset+3])
                );
                case 5:return 作業配列.MakeGenericType(
                    Reflection.ValueTuple.ValueTuple5,
                    this.ColumnDefinition(Arguments[Offset+0]),
                    this.ColumnDefinition(Arguments[Offset+1]),
                    this.ColumnDefinition(Arguments[Offset+2]),
                    this.ColumnDefinition(Arguments[Offset+3]),
                    this.ColumnDefinition(Arguments[Offset+4])
                );
                case 6:return 作業配列.MakeGenericType(
                    Reflection.ValueTuple.ValueTuple6,
                    this.ColumnDefinition(Arguments[Offset+0]),
                    this.ColumnDefinition(Arguments[Offset+1]),
                    this.ColumnDefinition(Arguments[Offset+2]),
                    this.ColumnDefinition(Arguments[Offset+3]),
                    this.ColumnDefinition(Arguments[Offset+4]),
                    this.ColumnDefinition(Arguments[Offset+5])
                );
                case 7:return 作業配列.MakeGenericType(
                    Reflection.ValueTuple.ValueTuple7,
                    this.ColumnDefinition(Arguments[Offset+0]),
                    this.ColumnDefinition(Arguments[Offset+1]),
                    this.ColumnDefinition(Arguments[Offset+2]),
                    this.ColumnDefinition(Arguments[Offset+3]),
                    this.ColumnDefinition(Arguments[Offset+4]),
                    this.ColumnDefinition(Arguments[Offset+5]),
                    this.ColumnDefinition(Arguments[Offset+6])
                );
                default:{
                    var Type7 = ValueTupleでNewする(作業配列,Arguments,Offset+7);
                    return 作業配列.MakeGenericType(
                        Reflection.ValueTuple.ValueTuple6,
                        this.ColumnDefinition(Arguments[Offset+0]),
                        this.ColumnDefinition(Arguments[Offset+1]),
                        this.ColumnDefinition(Arguments[Offset+2]),
                        this.ColumnDefinition(Arguments[Offset+3]),
                        this.ColumnDefinition(Arguments[Offset+4]),
                        this.ColumnDefinition(Arguments[Offset+6]),
                        Type7
                    );
                }
            }
        }
    }
    /// <summary>
    /// DeclareTableVariableBody orTSqlFragment
    /// returns @Student table(TestID int not null)
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression DeclareTableVariableBody(DeclareTableVariableBody x){
        var AsDefined=x.AsDefined;
        var (Type,Names)=this.TableDefinition(x.Definition);
        var Name= this.Identifier(x.VariableName);
        var Variable=e.Expression.Parameter(Type,Name);
        this.AddTableVariable(Variable,Names);
        return Variable;
        //var TableDefinition=this.TableDefinition(x.Definition);
        //this.変数CreateFunctionStatement.変数名=this.Identifier(x.VariableName);
        //return this.Identifier(x.VariableName);
        //return typeof(void);
    }
    private(e.Expression Set,e.ParameterExpression Element)TableReference(TableReference x)=>x switch{
        TableReferenceWithAlias         y=>this.TableReferenceWithAlias(y),
        JoinTableReference              y=>this.JoinTableReference(y),
        JoinParenthesisTableReference   y=>this.JoinParenthesisTableReference(y),
        //OdbcQualifiedJoinTableReference y=>this.OdbcQualifiedJoinTableReference(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private static void DictionaryにKeyがあればValueにnullを代入(SortedDictionary<string,e.Expression?> Dictionary,string Key,e.Expression Value){
        if(Dictionary.ContainsKey(Key))Dictionary[Key]=null;
        else Dictionary.Add(Key,Value);
    }
    private static void DictionaryにDotKeyとKeyがあればValueにnullを代入(SortedDictionary<string,e.Expression?> Dictionary,string? Dot,string Key,e.Expression Value) {
        if(Dot is not null) DictionaryにKeyがあればValueにnullを代入(Dictionary,Dot+Key,Value);
        DictionaryにKeyがあればValueにnullを代入(Dictionary,Key,Value);
    }
    private static void DictionaryにKey0とKey1があればValueにnullを代入(SortedDictionary<string,e.Expression?> Dictionary,string Key0,string Key1,e.Expression Value) {
        DictionaryにKeyがあればValueにnullを代入(Dictionary,Key0,Value);
        DictionaryにKeyがあればValueにnullを代入(Dictionary,Key1,Value);
    }
    private static void DictionaryのValueがnullのKeyをRemove(IDictionary<string,e.Expression?> RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression){
        foreach(var KV in RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression.ToList())
            if(KV.Value is null)
                RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression.Remove(KV.Key);
    }
    //private void Debug0(){
    //    foreach(var b in this.RefPeek.Dictionary_TableAlias_ColumnAliases){
    //        foreach(var c in b.Value){
    //            Debug.Assert(c is not null);
    //        }
    //    }
    //}
    private(e.Expression Set,e.ParameterExpression Element)OpenRowsetTableReference(OpenRowsetTableReference x){
        throw this.単純NotSupportedException(x);
    }
    private partial(e.Expression Set,e.ParameterExpression Element)PivotedTableReference(PivotedTableReference x);
    private (e.Expression Set,e.ParameterExpression Element)JoinTableReference(JoinTableReference x)=>x switch{
        UnqualifiedJoin y=>this.UnqualifiedJoin(y),
        QualifiedJoin y=>this.QualifiedJoin(y),
        _=>throw this.単純NotSupportedException(x)
    };
    /// <summary>
    /// FROM後にカンマ区切りでテーブルを並べる
    /// SELECT ... FROM A,B
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private(e.Expression Set,e.ParameterExpression Element)UnqualifiedJoin(UnqualifiedJoin x){
        var Dictionary_DatabaseSchemaTable_ColumnExpression=this.RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression;
        var (OuterSet,o)=this.TableReference(x.FirstTableReference);
        var TOuter=IEnumerable1のT(OuterSet.Type);
        var(InnerSet,i)=this.TableReference(x.SecondTableReference);
        var InnerSet_Type=InnerSet.Type;
        var TInner=IEnumerable1のT(InnerSet_Type);
        var 作業配列=this.作業配列;
        var ValueTuple2=作業配列.MakeGenericType(Reflection.ValueTuple.ValueTuple2,TOuter,TInner);
        var New2=作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple2,TOuter,TInner);
        var selector_Body=e.Expression.New(New2,作業配列.Expressions設定(o,i));
        var selector=e.Expression.Lambda(selector_Body,作業配列.Parameters設定(i));
        var SelectMany=作業配列.MakeGenericMethod(Reflection.ExtensionSet.SelectMany_selector,TOuter,ValueTuple2);
        var Select=作業配列.MakeGenericMethod(Reflection.ExtensionSet.Select_selector,TInner,selector_Body.Type);
        var oi=e.Expression.Parameter(selector_Body.Type,$"<UnqualifiedJoin>{this.番号++}");
        var 変換_旧Parameterを新Expression=this.変換_旧Parameterを新Expression1;
        共通(Dictionary_DatabaseSchemaTable_ColumnExpression,変換_旧Parameterを新Expression,oi,o,nameof(ValueTuple<int,int>.Item1));
        共通(Dictionary_DatabaseSchemaTable_ColumnExpression,変換_旧Parameterを新Expression,oi,i,nameof(ValueTuple<int,int>.Item2));
        if(x.UnqualifiedJoinType==UnqualifiedJoinType.OuterApply) {
            InnerSet = e.Expression.Call(
                作業配列.MakeGenericMethod(Reflection.ExtensionSet.DefaultIfEmpty,i.Type),
                InnerSet
            );
        }
        return (
            e.Expression.Call(
                SelectMany,
                OuterSet,
                e.Expression.Lambda(
                    e.Expression.Call(Select,InnerSet,selector),
                    作業配列.Parameters設定(o)
                )
            ),
            oi
        );
        //static void 共通(IDictionary<string,e.Expression?> Dictionary_DatabaseSchemaTable_ColumnExpression0,変換_旧Parameterを新Expression1 変換_旧Parameterを新Expression0,e.ParameterExpression oi0,e.ParameterExpression Parameter0,string Item){
        //    var 物理Expression=e.Expression.Field(oi0,Item);
        //    foreach(var KV in Dictionary_DatabaseSchemaTable_ColumnExpression0.ToList())
        //        Dictionary_DatabaseSchemaTable_ColumnExpression0[KV.Key]=変換_旧Parameterを新Expression0.実行(KV.Value!,Parameter0,物理Expression);
        //}
    }
    /// <summary>
    /// FROM後にJOIN区切りでテーブルを並べる
    /// SELECT *
    /// FROM A JOIN B ON A.F=B.F
    /// ↓
    /// A.Join(B,A=>A.F,B=>B.F.(A,B)=>...)
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private(e.Expression Set,e.ParameterExpression Element)QualifiedJoin(QualifiedJoin x){
        var Dictionary_DatabaseSchemaTable_ColumnExpression=this.RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression;
        //leftはleftが1行以上あるはず。だからSelectManyの外側に位置する
        //O.SelectMany(o=>I.Where(i=>o==i).Select(i=>new{o,i?.F1}))
        //O.SelectMany(o=>I.Where(i=>o==i).SingleOrDefault().Select(i=>new{o,i?.F1}))
        //I.SelectMany(i=>O.Where(o=>i==o).SingleOrDefault().Select(o=>new{i,o?.F1}))
        //O.Join(I,o=>o,i=>i,(o,i)o=>I.Where(i=>o==i).SingleOrDefault().Select(i=>new{o,i?.F1}))
        TableReference FirstTableReference,SecondTableReference;
        var QualifiedJoinType=x.QualifiedJoinType;
        switch(QualifiedJoinType) {
            case QualifiedJoinType.Inner:
                FirstTableReference=x.FirstTableReference;
                SecondTableReference=x.SecondTableReference;
                break;
            case QualifiedJoinType.LeftOuter:
                FirstTableReference=x.FirstTableReference;
                SecondTableReference=x.SecondTableReference;
                break;
            case QualifiedJoinType.RightOuter:
                FirstTableReference=x.SecondTableReference;
                SecondTableReference=x.FirstTableReference;
                break;
            case QualifiedJoinType.FullOuter:
                FirstTableReference = x.FirstTableReference;
                SecondTableReference = x.SecondTableReference;
                break;
            default:
                throw new NotSupportedException(QualifiedJoinType.ToString());
        }
        var(OuterSet,o)=this.TableReference(FirstTableReference);
        var TOuter=IEnumerable1のT(OuterSet.Type);
        var(InnerSet,i)=this.TableReference(SecondTableReference);
        var TInner=IEnumerable1のT(InnerSet.Type);
        var 作業配列=this.作業配列;
        var ValueTuple2=作業配列.MakeGenericType(Reflection.ValueTuple.ValueTuple2,TOuter,TInner);
        var SearchCondition=this.BooleanExpression(x.SearchCondition);
        var(OuterPredicate,InnerPredicate,Listプローブビルド)=this.取得_OuterPredicate_InnerPredicate_プローブビルド.実行(
            SearchCondition,new[]{o},new[]{i}
        );
        e.Expression プローブ,ビルド;
        var 変換_旧Parameterを新Expression=this.変換_旧Parameterを新Expression1;
        if(Listプローブビルド.Count==0){
            if(OuterPredicate is not null){
                OuterSet=e.Expression.Call(
                    作業配列.MakeGenericMethod(Reflection.ExtensionSet.Where,o.Type),
                    OuterSet,
                    e.Expression.Lambda(OuterPredicate,作業配列.Parameters設定(o))
                );
            }
            if(InnerPredicate is not null){
                InnerSet=e.Expression.Call(
                    作業配列.MakeGenericMethod(Reflection.ExtensionSet.Where,i.Type),
                    InnerSet,
                    e.Expression.Lambda(InnerPredicate,作業配列.Parameters設定(i))
                );
            }
            if(QualifiedJoinType==QualifiedJoinType.LeftOuter||QualifiedJoinType==QualifiedJoinType.RightOuter) {
                InnerSet=e.Expression.Call(
                    作業配列.MakeGenericMethod(Reflection.ExtensionSet.DefaultIfEmpty,i.Type),
                    InnerSet
                );
            }
            var New2=作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple2,TOuter,TInner);
            var selector_Body=e.Expression.New(New2,作業配列.Expressions設定(o,i));
            var selector=e.Expression.Lambda(selector_Body,作業配列.Parameters設定(i));
            var SelectMany=作業配列.MakeGenericMethod(Reflection.ExtensionSet.SelectMany_selector,TOuter,ValueTuple2);
            var Select=作業配列.MakeGenericMethod(Reflection.ExtensionSet.Select_selector,TInner,selector_Body.Type);
            var oi=e.Expression.Parameter(selector_Body.Type,$"j1{this.番号++}");
            共通(Dictionary_DatabaseSchemaTable_ColumnExpression,変換_旧Parameterを新Expression,oi,o,nameof(ValueTuple<int,int>.Item1));
            共通(Dictionary_DatabaseSchemaTable_ColumnExpression,変換_旧Parameterを新Expression,oi,i,nameof(ValueTuple<int,int>.Item2));
            return(
                e.Expression.Call(
                    SelectMany,
                    OuterSet,
                    e.Expression.Lambda(
                        e.Expression.Call(Select,InnerSet,selector),
                        作業配列.Parameters設定(o)
                    )
                ),
                oi
            );
        }else{
            if(QualifiedJoinType==QualifiedJoinType.LeftOuter||QualifiedJoinType==QualifiedJoinType.RightOuter) {
                //nullになりえる要素変数はこれの上位ででselectしたときに要素?.列とすべきint 列→int?列になる
                InnerSet=e.Expression.Call(
                    作業配列.MakeGenericMethod(Reflection.ExtensionSet.DefaultIfEmpty,i.Type),
                    InnerSet
                );
            }
            //Joinだが変換_メソッド正規化_取得インラインでSelectManyに変換される
            (プローブ,ビルド)=Listプローブビルド.Count==1?Listプローブビルド[0]:ValueTupleでNewしてプローブとビルドに分解(作業配列,Listプローブビルド,0);
            var Join=作業配列.MakeGenericMethod(Reflection.ExtensionSet.Join,TOuter,TInner,ビルド.Type,ValueTuple2);
            var outerKeySelector=e.Expression.Lambda(プローブ,作業配列.Parameters設定(o));
            var innerKeySelector=e.Expression.Lambda(ビルド,作業配列.Parameters設定(i));
            var New2=作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple2,TOuter,TInner);
            var resultSelector_Body=e.Expression.New(New2,作業配列.Expressions設定(o,i));
            var resultSelector=e.Expression.Lambda(resultSelector_Body,作業配列.Parameters設定(o,i));
            //var oi=e.Expression.Parameter(resultSelector_Body.Type,$"<QualifiedJoin2>{this.番号++}");
            var oi=e.Expression.Parameter(resultSelector_Body.Type,$"j2{this.番号++}");
            共通(Dictionary_DatabaseSchemaTable_ColumnExpression,変換_旧Parameterを新Expression,oi,o,nameof(ValueTuple<int,int>.Item1));
            共通(Dictionary_DatabaseSchemaTable_ColumnExpression,変換_旧Parameterを新Expression,oi,i,nameof(ValueTuple<int,int>.Item2));
            if(QualifiedJoinType==QualifiedJoinType.LeftOuter||QualifiedJoinType==QualifiedJoinType.RightOuter) {
                this.RefPeek.List_TableExpression.Add(e.Expression.Field(oi,"Item2"));
            }

            return(
                e.Expression.Call(Join,OuterSet,InnerSet,outerKeySelector,innerKeySelector,resultSelector),
                oi
            );
        }
        //static void 共通(IDictionary<string,e.Expression?> Dictionary_DatabaseSchemaTable_ColumnExpression0,変換_旧Parameterを新Expression1 変換_旧Parameterを新Expression0,e.ParameterExpression oi0,e.ParameterExpression Parameter0,string Item){
        //    var 物理Expression=e.Expression.Field(oi0,Item);
        //    foreach(var KV in Dictionary_DatabaseSchemaTable_ColumnExpression0.ToList())
        //        KV.Value=変換_旧Parameterを新Expression0.実行(KV.Value,Parameter0,物理Expression);
        //    //foreach(var KV in Dictionary_DatabaseSchemaTable_ColumnExpression0.ToList())
        //    //    Dictionary_DatabaseSchemaTable_ColumnExpression0[KV.Key]=変換_旧Parameterを新Expression0.実行(KV.Value,Parameter0,物理Expression);
        //}
    }
    private static void 共通(SortedDictionary<string,e.Expression?> Dictionary_DatabaseSchemaTable_ColumnExpression0,変換_旧Parameterを新Expression1 変換_旧Parameterを新Expression0,e.ParameterExpression oi0,e.ParameterExpression Parameter0,string Item){
        var 物理Expression=e.Expression.Field(oi0,Item);
        //foreach(var KV in Dictionary_DatabaseSchemaTable_ColumnExpression0.ToList())
        //    KV.Value=変換_旧Parameterを新Expression0.実行(KV.Value,Parameter0,物理Expression);
        foreach(var KV in Dictionary_DatabaseSchemaTable_ColumnExpression0.ToList())
            if(KV.Value is not null)//nullとは複数のエリアスで同一の列名があり、エリアスを省略すると重複するので指定できないことを表す
                Dictionary_DatabaseSchemaTable_ColumnExpression0[KV.Key]=変換_旧Parameterを新Expression0.実行(KV.Value,Parameter0,物理Expression);
    }
    /// <summary>
    /// SELECT * FROM(CUSTOMER JOIN ORDERS ON CUSTOMER.C_CUSTKEY=ORDERS.O_CUSTKEY)
    /// CUSTOMER.Join(ORDERS,CUSTOMER=>CUSTOMER.C_CUSTKEY,ORDERS=>ORDERS.O_CUSTKEY,(CUSTOMER,ORDERS)=>(CUSTOMER,ORDERS))
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private(e.Expression Set,e.ParameterExpression Element)JoinParenthesisTableReference(JoinParenthesisTableReference x)=>this.TableReference(x.Join);
    private e.Expression TableHint(TableHint x)=>x switch{
        IndexTableHint y=>this.IndexTableHint(y),
        LiteralTableHint y=>this.LiteralTableHint(y),
        ForceSeekTableHint y=>this.ForceSeekTableHint(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private static bool Nullable比較(int? a,int? b){
        return a.Equals(b);
    }
    private static readonly MethodInfo IsTrue_get=typeof(SqlBoolean).GetProperty(nameof(SqlBoolean.IsTrue))!.GetMethod!;
    //private static readonly MethodInfo IsFalse_get=typeof(SqlBoolean).GetProperty(nameof(SqlBoolean.IsFalse)).GetMethod;
    private e.Expression VariableValuePair(VariableValuePair x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression WhenClause(WhenClause x)=>x switch{
        SimpleWhenClause y=>this.SimpleWhenClause(y),
        SearchedWhenClause y=>this.SearchedWhenClause(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression SimpleWhenClause(SimpleWhenClause x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression SearchedWhenClause(SearchedWhenClause x){
        var WhenExpression=this.BooleanExpression(x.WhenExpression);
        var ThenExpression=this.ScalarExpression(x.ThenExpression);
        return e.Expression.IfThenElse(WhenExpression,ThenExpression,Default_void);
    }
    private e.Expression SchemaDeclarationItem(SchemaDeclarationItem x)=>x switch{
        SchemaDeclarationItemOpenjson y=>this.SchemaDeclarationItemOpenjson(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression OverClause(OverClause x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DeclareVariableElement(DeclareVariableElement x){
        switch(x){
            case ProcedureParameter y:{
                this.ProcedureParameter(y);
                return Default_void;
            }
            default:{
                var Type=this.Nullableまたは参照型(DataTypeReferenceからTypeに変換(x.DataType));
                var Parameter=e.Expression.Variable(Type,x.VariableName.Value);
                this.AddScalarVariable(Parameter);
                //this.Block_Variables.Add(Parameter);
                if(x.Value is null)return Default_void;
                return e.Expression.Assign(
                    Parameter,
                    this.Convertデータ型を合わせるNullableは想定する(this.ScalarExpression(x.Value),Parameter.Type)
                );
            }
        }
    }
    //private void ProcedureParameter(ProcedureParameter x){
    //    var Type=this.Nullableまたは参照型(DataTypeReferenceからTypeに変換(x.DataType));
    //    var Parameter=e.Expression.Parameter(Type,x.VariableName.Value);
    //    this.AddScalarVariable(Parameter);
    //}
    private e.Expression DataModificationSpecification(DataModificationSpecification x)=>x switch{
        UpdateDeleteSpecificationBase y=>this.UpdateDeleteSpecificationBase(y),
        InsertSpecification y=>this.InsertSpecification(y),
        MergeSpecification y=>this.MergeSpecification(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression UpdateDeleteSpecificationBase(UpdateDeleteSpecificationBase x)=>x switch{
        DeleteSpecification y=>this.DeleteSpecification(y),
        UpdateSpecification y=>this.UpdateSpecification(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression DeleteSpecification(DeleteSpecification x){
        if(x.OutputClause is not null)this.OutputClause(x.OutputClause);
        if(x.FromClause is not null)this.FromClause(x.FromClause);
        var(Set,Element)=this.TableReference(x.Target);
        if(x.OutputIntoClause is not null)this.OutputIntoClause(x.OutputIntoClause);
        if(x.TopRowFilter is not null)this.TopRowFilter(x.TopRowFilter);
        var Parameters=this.作業配列.Parameters設定(Element);
        //this.List_Parameter
        e.LambdaExpression predicate;
        if(x.WhereClause is not null){
            var predicate_Body=this.WhereClause(x.WhereClause);
            predicate=e.Expression.Lambda(predicate_Body,Parameters);
        } else{
            predicate=e.Expression.Lambda(Constant_true,Parameters);
        }
        return e.Expression.Call(
            Set,
            Set.Type.GetMethod(nameof(Set<int>.DeleteWith)),
            predicate
        );
    }
    private e.Expression UpdateSpecification(UpdateSpecification x){
        var(Set,Element)=this.TableReference(x.Target);
        var ctor=Element.Type.GetConstructors()[0];
        var ctor_Parameters=ctor.GetParameters();
        var Arguments_Length=ctor_Parameters.Length;
        var Arguments=new e.Expression[Arguments_Length];
        for(var a=0;a<Arguments_Length;a++)Arguments[a]=e.Expression.PropertyOrField(Element,ctor_Parameters[a].Name);
        foreach(var SetClause in x.SetClauses){
            var(ParameterName,NewValue)=this.SetClause(SetClause);
            var index=Array.FindIndex(ctor_Parameters,p=>p.Name==ParameterName);
            ref var Argument=ref Arguments[index];
            Argument=Convert必要なら(NewValue,Argument.Type);
        }
        var predicate_Body=this.WhereClause(x.WhereClause);
        var Parameters=this.作業配列.Parameters設定(Element);
        var predicate=e.Expression.Lambda(predicate_Body,Parameters);
        var set=e.Expression.Lambda(e.Expression.New(ctor,Arguments),Parameters);
        //Func<T,bool> predicate
        return e.Expression.Call(
            Set,
            Set.Type.GetMethod(nameof(Set<int>.UpdateWith),this.作業配列.Types設定(set.Type,predicate.Type)),
            set,
            predicate
        );
    }
    private (e.ParameterExpression Variable, string[] Names) Insert_VariableTableReference(VariableTableReference x) {
        //ref var RefPeek = ref this.RefPeek;
        //var Dictionary_DatabaseSchemaTable_ColumnExpression = RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression;
        //var Dictionary_TableAlias_ColumnAliases = RefPeek.Dictionary_TableAlias_ColumnAliases;
        //var List_アスタリスクColumnAlias = RefPeek.List_アスタリスクColumnAlias;
        //var List_アスタリスクColumnExpression = RefPeek.List_アスタリスクColumnExpression;
        var x_Variable_Name = x.Variable.Name;
        var Set = this.FindTableVariable(x.Variable.Name);
        //var Element=e.Expression.Parameter(IEnumerable1のT(Set.Type),Name);
        //var Table_Type = Set.Type;
        //var T = IEnumerable1のT(Table_Type);
        //var ctor_Parameters = T.GetConstructors()[0].GetParameters();
        //var ctor_Parameters_Length = ctor_Parameters.Length;
        //var ColumnAliases = new string[ctor_Parameters_Length];
        //var x_Alias = x.Alias;
        //string? Table = null, TableDot = null;
        //if(x_Alias is not null) {
        //    Table=x_Alias.Value;
        //    Dictionary_TableAlias_ColumnAliases.Add(Table,ColumnAliases);
        //    TableDot = Table+'.';
        //}
        var result = PrivateFindVariable(this.List_定義型TableVariable,x_Variable_Name);
        if(result is not null){
            var T = IEnumerable1のT(result.Type);
            return (result,T.GetProperties().Select(p=>p.Name).ToArray());
        }
        foreach(var 匿名型TableVariable in this.List_匿名型TableVariable)
            if(匿名型TableVariable.Variable.Name==x_Variable_Name)
                return 匿名型TableVariable;
        throw new KeyNotFoundException($"{x_Variable_Name}が見つからなかった。");
    }
    private (e.ParameterExpression Variable, string[] Names) Insert_VariableTableReference(NamedTableReference x) {
        //ref var RefPeek = ref this.RefPeek;
        //var Dictionary_DatabaseSchemaTable_ColumnExpression = RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression;
        //var Dictionary_TableAlias_ColumnAliases = RefPeek.Dictionary_TableAlias_ColumnAliases;
        //var List_アスタリスクColumnAlias = RefPeek.List_アスタリスクColumnAlias;
        //var List_アスタリスクColumnExpression = RefPeek.List_アスタリスクColumnExpression;
        var x_Variable_Name = x.Alias.Value;
        var Set = this.FindTableVariable(x_Variable_Name);
        //var Element=e.Expression.Parameter(IEnumerable1のT(Set.Type),Name);
        //var Table_Type = Set.Type;
        //var T = IEnumerable1のT(Table_Type);
        //var ctor_Parameters = T.GetConstructors()[0].GetParameters();
        //var ctor_Parameters_Length = ctor_Parameters.Length;
        //var ColumnAliases = new string[ctor_Parameters_Length];
        //var x_Alias = x.Alias;
        //string? Table = null, TableDot = null;
        //if(x_Alias is not null) {
        //    Table=x_Alias.Value;
        //    Dictionary_TableAlias_ColumnAliases.Add(Table,ColumnAliases);
        //    TableDot = Table+'.';
        //}
        var result = PrivateFindVariable(this.List_定義型TableVariable,x_Variable_Name);
        if(result is not null){
            var T = IEnumerable1のT(result.Type);
            return (result,T.GetProperties().Select(p=>p.Name).ToArray());
        }
        foreach(var 匿名型TableVariable in this.List_匿名型TableVariable)
            if(匿名型TableVariable.Variable.Name==x_Variable_Name)
                return 匿名型TableVariable;
        throw new KeyNotFoundException($"{x_Variable_Name}が見つからなかった。");
    }
    /// <summary>
    /// InsertSpecification:DataModificationSpecification:DataModificationSpecification:TSqlFragment
    /// insert into @Student select @gtinyint + 4200000000 as tinyint
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression InsertSpecification(InsertSpecification x){
        ref var RefPeek=ref this.RefPeek;
        if(x.TopRowFilter is not null)
            this.TopRowFilter(x.TopRowFilter);
        var Columns=x.Columns;
        var Columns_Count= Columns.Count;
        var (TargetSet,TargetElement)=this.TableReference(x.Target);
        var List_ColumnAlias=RefPeek.List_ColumnAlias;
        var List_ColumnAlias0=List_ColumnAlias.ToList();
        var List_ColumnAlias_Count0=List_ColumnAlias0.Count;
        var List_ColumnExpression0=RefPeek.List_ColumnExpression;
        RefPeek.Clear();
        ////(_,Set)=this.NamedTableReference(NamedTableReference);
        //string[] Names;
        //if(Columns_Count==0){
        //    var Properties=Element.Type.GetProperties();
        //    Columns_Count=Properties.Length;
        //    Names=new string[Columns_Count];
        //    for(var a = 0;a<Columns_Count;a++)
        //        Names[a]=Properties[a].Name;
        //} else{
        //    Names=new string[Columns_Count];
        //    for(var a = 0;a<Columns_Count;a++) {
        //        var Identifiers = Columns[a].MultiPartIdentifier.Identifiers;
        //        Names[a]=Identifiers[^1].Value;
        //    }
        //}
        //this.List_匿名型TableVariable[0].
        //ここでthis.RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpressionが追加される
        if(x.InsertSource is not null) { 
            var Source=this.InsertSource(x.InsertSource);
            //insert into Set InsertSource(select ... from ...)
            //Set=InsertSource
            //Debug.Assert(Set.NodeType==e.ExpressionType.Parameter);
            if(x.OutputClause is not null)this.OutputClause(x.OutputClause);
            if(x.OutputIntoClause is not null)this.OutputIntoClause(x.OutputIntoClause);
            //var ValueTuple_Type = IEnumerable1のT(InsertSource.Type);
            var SourceSetType=Source.Type;
            if(SourceSetType.IsArray){

            }
            var SourceElementType=IEnumerable1のT(SourceSetType);
            //Debug.Assert(Element.Type==IEnumerable1のT(InsertSource_Type));
            //var Element = e.Expression.Parameter(ValueTuple_Type,"Element");
            var Dictionary_With名_Set_ColumnAliases=this.Dictionary_With名_Set_ColumnAliases;
            var TargetElementType =IEnumerable1のT(TargetSet.Type);
            var Constructor = TargetElementType.GetConstructors()[0];
            var Parameters = Constructor.GetParameters();
            var NewArguments_Length = Parameters.Length;
            var NewArguments = new e.Expression?[NewArguments_Length];
            var SourceElement=e.Expression.Parameter(SourceElementType,"SourceElement");
            var 作業配列 = this.作業配列;
            e.Expression ValueTuple = SourceElement;
            var Item番号 = 1;
            if(Columns_Count==0){
                var List_ColumnAlias_Count=List_ColumnAlias.Count;
                var List_ColumnExpression=RefPeek.List_ColumnExpression;
                Debug.Assert(List_ColumnAlias_Count==NewArguments_Length);
                Debug.Assert(List_ColumnExpression.Count==NewArguments_Length);
                //for(var a=0;a<NewArguments.Length;a++){
                //    NewArguments[a] = this.Convertデータ型を合わせるNullableは想定する(List_ColumnExpression[a],Parameters[a].ParameterType);
                //}
                for(var a = 0;a<NewArguments_Length;a++){
                    var Item = ValueTuple_Item(ref ValueTuple,ref Item番号);
                    NewArguments[a]=this.Convertデータ型を合わせるNullableは想定する(Item,Parameters[a].ParameterType);
                }
            } else {
                var Names=new string[Columns_Count];
                for(var a = 0;a<Columns_Count;a++) {
                    var Identifiers = Columns[a].MultiPartIdentifier.Identifiers;
                    Names[a]=Identifiers[^1].Value;
                }
                for(var a = 0;a < Columns_Count;a++) {
                    var Item = ValueTuple_Item(ref ValueTuple,ref Item番号);
                    var index = Array.FindIndex(Parameters,p => p.Name==Names[a]);
                    NewArguments[index] = this.Convertデータ型を合わせるNullableは想定する(Item,Parameters[index].ParameterType);
                    //Names[a]=null;
                }
                //列がA,B,CあるテーブルにINSERT INTO(A,B)SELECT A,BしたときにCはデフォルト値を入れたい
                for(var a = 0;a < Columns_Count;a++){
                    if(NewArguments[a] is not null) continue;
                    NewArguments[a]=e.Expression.Default(Parameters[a].ParameterType);
                }
                //for(var a = 0;a < Columns_Count;a++){
                //    if(Names[a] is null) continue;
                //    NewArguments[a]=e.Expression.Default(Parameters[a].ParameterType);
                //    NewArguments[index] = this.Convertデータ型を合わせるNullableは想定する(Item,Parameters[index].ParameterType);
                //    Names[a]=null;
                //}
            }
            var Select_selector=作業配列.MakeGenericMethod(
                Reflection.ExtensionSet.Select_selector,
                SourceElementType,
                TargetElementType
            );
            var selector=e.Expression.Lambda(
                e.Expression.New(Constructor,NewArguments!),
                作業配列.Parameters設定(SourceElement)
            );
            return e.Expression.Call(
                TargetSet,
                TargetSet.Type.GetMethod("AddRange"),
                e.Expression.Call(
                    作業配列.MakeGenericMethod(
                        Reflection.ExtensionSet.Select_selector,
                        SourceElementType,
                        TargetElementType
                    ),
                    Source,
                    e.Expression.Lambda(
                        e.Expression.New(Constructor,NewArguments!),
                        作業配列.Parameters設定(SourceElement)
                    )
                )
            );
        } else {
            throw this.単純NotSupportedException(x);
        }
        //foreach(var Column in x.Columns) {}
        ////var(Set,Element)=this.TableReference(x.Target);
        //switch(x.Target){
        //    case VariableTableReference VariableTableReference:{
        //        var(Set,Names1)=this.Insert_VariableTableReference(VariableTableReference);
        //        string[] Names;
        //        if(Columns_Count==0)Names=Names1;
        //        else{
        //            Names=new string[Columns_Count];
        //            for(var a = 0;a<Columns_Count;a++) {
        //                var Identifiers = Columns[a].MultiPartIdentifier.Identifiers;
        //                Names[a]=Identifiers[Identifiers.Count-1].Value;
        //            }
        //        }
        //        //this.List_匿名型TableVariable[0].
        //        //ここでthis.RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpressionが追加される
        //        if(x.InsertSource is not null) { 
        //            var InsertSource=this.InsertSource(x.InsertSource);
        //            //insert into Set InsertSource(select ... from ...)
        //            //Set=InsertSource
        //            Debug.Assert(Set.NodeType==e.ExpressionType.Parameter);
        //            if(x.OutputClause is not null)this.OutputClause(x.OutputClause);
        //            if(x.OutputIntoClause is not null)this.OutputIntoClause(x.OutputIntoClause);
        //            var ValueTuple_Type = IEnumerable1のT(InsertSource.Type);
        //            var ValueTuple_p = e.Expression.Parameter(ValueTuple_Type,"ValueTuple_p");
        //            var Element_Type =IEnumerable1のT(Set.Type);
        //            var Constructor = Element_Type.GetConstructors()[0];
        //            var Parameters = Constructor.GetParameters();
        //            var NewArguments_Length = Parameters.Length;
        //            var NewArguments = new e.Expression[NewArguments_Length];
        //            var 作業配列 = this.作業配列;
        //            e.Expression ValueTuple = ValueTuple_p;
        //            var Item番号 = 1;
        //            if(Columns_Count==0) {
        //                for(var a = 0;a < NewArguments_Length;a++) {
        //                    var Item = ValueTuple_Item(ref ValueTuple,ref Item番号);
        //                    NewArguments[a] = this.Convertデータ型を合わせるNullableは想定する(Item,Parameters[a].ParameterType);
        //                }
        //            } else {
        //                for(var a = 0;a < Columns_Count;a++) {
        //                    var Item = ValueTuple_Item(ref ValueTuple,ref Item番号);
        //                    var index = Array.FindIndex(Parameters,p => p.Name==Names[a]);
        //                    NewArguments[index] = this.Convertデータ型を合わせるNullableは想定する(Item,Parameters[index].ParameterType);
        //                }
        //            }
        //            return e.Expression.Call(
        //                Set,
        //                Set.Type.GetMethod("AddRange"),
        //                e.Expression.Call(
        //                    作業配列.MakeGenericMethod(InsertSource.Type.IsArray ? Reflection.ExtensionEnumerable.Select_selector:Reflection.ExtensionSet.Select_selector,ValueTuple_Type,Element_Type),
        //                    InsertSource,
        //                    e.Expression.Lambda(
        //                        e.Expression.New(Constructor,NewArguments),
        //                        作業配列.Parameters設定(ValueTuple_p)

        //                    )
        //                )
        //            );
        //        } else {
        //            throw this.単純NotSupportedException(x);
        //        }
        //    }
        //    case NamedTableReference NamedTableReference:{
        //        var (Set,Element)=this.NamedTableReference(NamedTableReference);
        //        //(_,Set)=this.NamedTableReference(NamedTableReference);
        //        string[] Names;
        //        if(Columns_Count==0){
        //            var Properties=Element.Type.GetProperties();
        //            Columns_Count=Properties.Length;
        //            Names=new string[Columns_Count];
        //            for(var a = 0;a<Columns_Count;a++)
        //                Names[a]=Properties[a].Name;
        //        } else{
        //            Names=new string[Columns_Count];
        //            for(var a = 0;a<Columns_Count;a++) {
        //                var Identifiers = Columns[a].MultiPartIdentifier.Identifiers;
        //                Names[a]=Identifiers[^1].Value;
        //            }
        //        }
        //        //this.List_匿名型TableVariable[0].
        //        //ここでthis.RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpressionが追加される
        //        if(x.InsertSource is not null) { 
        //            var InsertSource=this.InsertSource(x.InsertSource);
        //            //insert into Set InsertSource(select ... from ...)
        //            //Set=InsertSource
        //            Debug.Assert(Set.NodeType==e.ExpressionType.Parameter);
        //            if(x.OutputClause is not null)this.OutputClause(x.OutputClause);
        //            if(x.OutputIntoClause is not null)this.OutputIntoClause(x.OutputIntoClause);
        //            var ValueTuple_Type = IEnumerable1のT(InsertSource.Type);
        //            var ValueTuple_p = e.Expression.Parameter(ValueTuple_Type,"ValueTuple_p");
        //            var Element_Type =IEnumerable1のT(Set.Type);
        //            var Constructor = Element_Type.GetConstructors()[0];
        //            var Parameters = Constructor.GetParameters();
        //            var NewArguments_Length = Parameters.Length;
        //            var NewArguments = new e.Expression[NewArguments_Length];
        //            var 作業配列 = this.作業配列;
        //            e.Expression ValueTuple = ValueTuple_p;
        //            var Item番号 = 1;
        //            if(Columns_Count==0) {
        //                for(var a = 0;a < NewArguments_Length;a++) {
        //                    var Item = ValueTuple_Item(ref ValueTuple,ref Item番号);
        //                    NewArguments[a] = this.Convertデータ型を合わせるNullableは想定する(Item,Parameters[a].ParameterType);
        //                }
        //            } else {
        //                for(var a = 0;a < Columns_Count;a++) {
        //                    var Item = ValueTuple_Item(ref ValueTuple,ref Item番号);
        //                    var index = Array.FindIndex(Parameters,p => p.Name==Names[a]);
        //                    NewArguments[index] = this.Convertデータ型を合わせるNullableは想定する(Item,Parameters[index].ParameterType);
        //                }
        //            }
        //            return e.Expression.Call(
        //                Set,
        //                Set.Type.GetMethod("AddRange"),
        //                e.Expression.Call(
        //                    作業配列.MakeGenericMethod(InsertSource.Type.IsArray ? Reflection.ExtensionEnumerable.Select_selector:Reflection.ExtensionSet.Select_selector,ValueTuple_Type,Element_Type),
        //                    InsertSource,
        //                    e.Expression.Lambda(
        //                        e.Expression.New(Constructor,NewArguments),
        //                        作業配列.Parameters設定(ValueTuple_p)

        //                    )
        //                )
        //            );
        //        } else {
        //            throw this.単純NotSupportedException(x);
        //        }
        //    }
        //    default:
        //        throw this.単純NotSupportedException(x);
        //}
    }
    private e.Expression MergeSpecification(MergeSpecification x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression Permission(Permission x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression Privilege80(Privilege80 x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression InsertSource(InsertSource x)=>x switch{
        ValuesInsertSource y=>this.ValuesInsertSource(y),
        SelectInsertSource y=>this.SelectInsertSource(y),
        ExecuteInsertSource y=>this.ExecuteInsertSource(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression LiteralRange(LiteralRange x)=>x switch{
        ProcessAffinityRange y=>this.ProcessAffinityRange(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression SequenceOption(SequenceOption x)=>x switch{
        DataTypeSequenceOption y=>this.DataTypeSequenceOption(y),
        ScalarExpressionSequenceOption y=>this.ScalarExpressionSequenceOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression ColumnMasterKeyParameter(ColumnMasterKeyParameter x)=>x switch{
        ColumnMasterKeyStoreProviderNameParameter y=>this.ColumnMasterKeyStoreProviderNameParameter(y),
        ColumnMasterKeyPathParameter y=>this.ColumnMasterKeyPathParameter(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression ColumnEncryptionKeyValue(ColumnEncryptionKeyValue x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ColumnEncryptionKeyValueParameter(ColumnEncryptionKeyValueParameter x)=>x switch{
        ColumnMasterKeyNameParameter y=>this.ColumnMasterKeyNameParameter(y),
        ColumnEncryptionAlgorithmNameParameter y=>this.ColumnEncryptionAlgorithmNameParameter(y),
        EncryptedValueParameter y=>this.EncryptedValueParameter(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression ExternalTableOption(ExternalTableOption x)=>x switch{
        ExternalTableLiteralOrIdentifierOption y=>this.ExternalTableLiteralOrIdentifierOption(y),
        ExternalTableDistributionOption y=>this.ExternalTableDistributionOption(y),
        ExternalTableRejectTypeOption y=>this.ExternalTableRejectTypeOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression ExternalTableDistributionPolicy(ExternalTableDistributionPolicy x)=>x switch{
        ExternalTableReplicatedDistributionPolicy y=>this.ExternalTableReplicatedDistributionPolicy(y),
        ExternalTableRoundRobinDistributionPolicy y=>this.ExternalTableRoundRobinDistributionPolicy(y),
        ExternalTableShardedDistributionPolicy y=>this.ExternalTableShardedDistributionPolicy(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression ExternalDataSourceOption(ExternalDataSourceOption x)=>x switch{
        ExternalDataSourceLiteralOrIdentifierOption y=>this.ExternalDataSourceLiteralOrIdentifierOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression WhereClause(WhereClause x)=>this.BooleanExpression(x.SearchCondition);
    //private e.Expression FileDeclaration(FileDeclaration x){
    //    throw this.単純NotSupportedException(x);
    //}
    private e.Expression FileGroupDefinition(FileGroupDefinition x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DatabaseConfigurationClearOption(DatabaseConfigurationClearOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DatabaseConfigurationSetOption(DatabaseConfigurationSetOption x)=>x switch{
        OnOffPrimaryConfigurationOption y=>this.OnOffPrimaryConfigurationOption(y),
        MaxDopConfigurationOption y=>this.MaxDopConfigurationOption(y),
        GenericConfigurationOption y=>this.GenericConfigurationOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    //private e.Expression QueryStoreSizeCleanupPolicyOption(QueryStoreSizeCleanupPolicyOption x){
    //    throw this.単純NotSupportedException(x);
    //}
    private e.Expression AutomaticTuningOption(AutomaticTuningOption x)=>x switch{
        AutomaticTuningForceLastGoodPlanOption y=>this.AutomaticTuningForceLastGoodPlanOption(y),
        AutomaticTuningCreateIndexOption y=>this.AutomaticTuningCreateIndexOption(y),
        AutomaticTuningDropIndexOption y=>this.AutomaticTuningDropIndexOption(y),
        AutomaticTuningMaintainIndexOption y=>this.AutomaticTuningMaintainIndexOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    //private (Type Type, string Name) ColumnDefinitionBase(ColumnDefinitionBase x){
    //    switch(x){
    //        case ColumnDefinition y:return this.ColumnDefinition(y);
    //        default:throw this.単純NotSupportedException(x);
    //    }
    //}
    /// <summary>
    /// TABLE型の列をValueTupleで表現する
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private Type ColumnDefinition(ColumnDefinition x){
        return this.DataTypeReference(x.DataType);
        //if(x.Encryption is not null)this.ColumnEncryptionDefinition(x.Encryption);
        //if(x.Collation is not null)this.Identifier(x.Collation);
        //var ColumnIdentifier=this.Identifier(x.ColumnIdentifier);
        //if(x.ComputedColumnExpression is not null)this.ScalarExpression(x.ComputedColumnExpression);
        //foreach(var Constraint in x.Constraints)
        //    this.ConstraintDefinition(Constraint);
        //if(x.DefaultConstraint is not null)this.DefaultConstraintDefinition(x.DefaultConstraint);
        //var GeneratedAlways=x.GeneratedAlways;
        //if(x.IdentityOptions is not null)this.IdentityOptions(x.IdentityOptions);
        //if(x.Index is not null)this.IndexDefinition(x.Index);
        //var IsMasked=x.IsMasked;
        //var IsHidden=x.IsHidden;
        //var IsPersisted=x.IsPersisted;
        //var IsRowGuidCol=x.IsRowGuidCol;
        //if(x.MaskingFunction is not null)this.StringLiteral(x.MaskingFunction);
        //if(x.StorageOptions is not null)this.ColumnStorageOptions(x.StorageOptions);
        //return typeof(int);
    }
    private e.Expression IdentityOptions(IdentityOptions x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ColumnStorageOptions(ColumnStorageOptions x){
        throw this.単純NotSupportedException(x);
    }
    private void ConstraintDefinition(ConstraintDefinition x){
        switch(x){
            case CheckConstraintDefinition y:this.CheckConstraintDefinition(y);break;
            case DefaultConstraintDefinition y:this.DefaultConstraintDefinition(y);break;
            case ForeignKeyConstraintDefinition y:this.ForeignKeyConstraintDefinition(y);break;
            case NullableConstraintDefinition y:this.NullableConstraintDefinition(y);break;
            case GraphConnectionConstraintDefinition y:this.GraphConnectionConstraintDefinition(y);break;
            case UniqueConstraintDefinition y:this.UniqueConstraintDefinition(y);break;
            default:throw this.単純NotSupportedException(x);
        }
    }
    private e.Expression FederationScheme(FederationScheme x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression TableDistributionPolicy(TableDistributionPolicy x)=>x switch{
        TableReplicateDistributionPolicy y=>this.TableReplicateDistributionPolicy(y),
        TableRoundRobinDistributionPolicy y=>this.TableRoundRobinDistributionPolicy(y),
        TableHashDistributionPolicy y=>this.TableHashDistributionPolicy(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression TableIndexType(TableIndexType x)=>x switch{
        TableClusteredIndexType y=>this.TableClusteredIndexType(y),
        TableNonClusteredIndexType y=>this.TableNonClusteredIndexType(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression PartitionSpecifications(PartitionSpecifications x)=>x switch{
        TablePartitionOptionSpecifications y=>this.TablePartitionOptionSpecifications(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CompressionPartitionRange(CompressionPartitionRange x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression GraphConnectionBetweenNodes(GraphConnectionBetweenNodes x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression RestoreOption(RestoreOption x)=>x switch{
        ScalarExpressionRestoreOption y=>this.ScalarExpressionRestoreOption(y),
        MoveRestoreOption y=>this.MoveRestoreOption(y),
        StopRestoreOption y=>this.StopRestoreOption(y),
        FileStreamRestoreOption y=>this.FileStreamRestoreOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression BackupOption(BackupOption x)=>x switch{
        BackupEncryptionOption y=>this.BackupEncryptionOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression DeviceInfo(DeviceInfo x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression MirrorToClause(MirrorToClause x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression BackupRestoreFileInfo(BackupRestoreFileInfo x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ExternalTableColumnDefinition(ExternalTableColumnDefinition x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression InsertBulkColumnDefinition(InsertBulkColumnDefinition x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DbccOption(DbccOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DbccNamedLiteral(DbccNamedLiteral x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression PartitionParameterType(PartitionParameterType x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression RemoteServiceBindingOption(RemoteServiceBindingOption x)=>x switch{
        OnOffRemoteServiceBindingOption y=>this.OnOffRemoteServiceBindingOption(y),
        UserRemoteServiceBindingOption y=>this.UserRemoteServiceBindingOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression EncryptionSource(EncryptionSource x)=>x switch{
        AssemblyEncryptionSource y=>this.AssemblyEncryptionSource(y),
        FileEncryptionSource y=>this.FileEncryptionSource(y),
        ProviderEncryptionSource y=>this.ProviderEncryptionSource(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression EndpointProtocolOption(EndpointProtocolOption x)=>x switch{
        AuthenticationEndpointProtocolOption y=>this.AuthenticationEndpointProtocolOption(y),
        CompressionEndpointProtocolOption y=>this.CompressionEndpointProtocolOption(y),
        ListenerIPEndpointProtocolOption y=>this.ListenerIPEndpointProtocolOption(y),
        LiteralEndpointProtocolOption y=>this.LiteralEndpointProtocolOption(y),
        PortsEndpointProtocolOption y=>this.PortsEndpointProtocolOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression IPv4(IPv4 x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression KeyOption(KeyOption x)=>x switch{
        KeySourceKeyOption y=>this.KeySourceKeyOption(y),
        AlgorithmKeyOption y=>this.AlgorithmKeyOption(y),
        IdentityValueKeyOption y=>this.IdentityValueKeyOption(y),
        ProviderKeyNameKeyOption y=>this.ProviderKeyNameKeyOption(y),
        CreationDispositionKeyOption y=>this.CreationDispositionKeyOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression FullTextCatalogOption(FullTextCatalogOption x)=>x switch{
        OnOffFullTextCatalogOption y=>this.OnOffFullTextCatalogOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression ServiceContract(ServiceContract x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ComputeFunction(ComputeFunction x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression TableSampleClause(TableSampleClause x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ExpressionWithSortOrder(ExpressionWithSortOrder x){
        throw this.単純NotSupportedException(x);
    }
    //private List<(Expressions.Expression Column,Int32 index)> List_ColumnIndex=new List<(Expressions.Expression Column,Int32 index)>();
    private e.Expression GroupByClause(GroupByClause x){
        var GroupingSpecifications=x.GroupingSpecifications;
        var GroupingSpecifications_Count=GroupingSpecifications.Count;
        var GroupingExpressions=new e.Expression[GroupingSpecifications_Count];
        var List_GroupByExpression=this.RefPeek.List_GroupByExpression;
        for(var a=0;a<GroupingSpecifications_Count;a++){
            var GroupingSpecification=this.GroupingSpecification(GroupingSpecifications[a]);
            List_GroupByExpression.Add(GroupingSpecification);
            GroupingExpressions[a]=GroupingSpecification;
        }
        return CommonLibrary.ValueTupleでNewする(this.作業配列,GroupingExpressions);
    }
    private e.Expression GroupingSpecification(GroupingSpecification x)=>x switch{
        ExpressionGroupingSpecification y=>this.ExpressionGroupingSpecification(y),
        CompositeGroupingSpecification y=>this.CompositeGroupingSpecification(y),
        CubeGroupingSpecification y=>this.CubeGroupingSpecification(y),
        RollupGroupingSpecification y=>this.RollupGroupingSpecification(y),
        GrandTotalGroupingSpecification y=>this.GrandTotalGroupingSpecification(y),
        GroupingSetsGroupingSpecification y=>this.GroupingSetsGroupingSpecification(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression ExpressionGroupingSpecification(ExpressionGroupingSpecification x){
        var x_Expression=x.Expression;
        var ScalarExpression=this.ScalarExpression(x_Expression);
        return ScalarExpression;
    }
    private e.Expression CompositeGroupingSpecification(CompositeGroupingSpecification x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CubeGroupingSpecification(CubeGroupingSpecification x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression RollupGroupingSpecification(RollupGroupingSpecification x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression GrandTotalGroupingSpecification(GrandTotalGroupingSpecification x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression GroupingSetsGroupingSpecification(GroupingSetsGroupingSpecification x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression OutputClause(OutputClause x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression OutputIntoClause(OutputIntoClause x){
        //if(x is null)return Default_void;
        throw this.単純NotSupportedException(x);
    }
    /// <summary>
    /// SELECT *
    /// FROM A,B
    /// ↓
    /// A.SelectMany(a=>B.Select(...))
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private (e.Expression Source,e.ParameterExpression Element)FromClause(FromClause x){
        var TableReferences0=x.TableReferences;
        var TableReferences0_Count=TableReferences0.Count;
        var(Source,Element0)=this.TableReference(TableReferences0[0]);
        Debug.Assert(IEnumerable1のT(Source.Type)==Element0.Type);
        var Element1=Element0;
        var TSource=Element0.Type;
        if(TableReferences0_Count>1){
            var 作業配列=this.作業配列;
            var Array論理Parameter物理Expressions=new(e.ParameterExpression 論理,e.Expression 物理)[TableReferences0_Count];
            Array論理Parameter物理Expressions[0].論理=Element0;
            //Select(ss=>)
            //Source.SelectMany(Element0=>CD,(Element0,cd)=>AB);
            for(var a=1;a<TableReferences0_Count;a++){
                var(Collection,ce)=this.TableReference(TableReferences0[a]);
                Array論理Parameter物理Expressions[a].論理=ce;
                var TCollection=ce.Type;
                //var b=作業配列.MakeGenericType(typeof(ImmutableSet<>),TCollection);
                //Debug.Assert(b==Collection.Type);
                var collectionSelectorMethod=作業配列.MakeGenericType(typeof(Func<,>),TSource,作業配列.MakeGenericType(typeof(ImmutableSet<>),TCollection));
                var collectionSelector=e.Expression.Lambda(
                    collectionSelectorMethod,
                    Collection,
                    作業配列.Parameters設定(Element1)
                );
                var ValueTuple2_ctr=作業配列.MakeValueTuple_ctor(Reflection.ValueTuple.ValueTuple2,Element1.Type,TCollection);//todo 匿名型
                var resultSelector=e.Expression.Lambda(
                    e.Expression.New(ValueTuple2_ctr,Element1,ce),
                    作業配列.Parameters設定(Element1,ce)
                );
                var TResult=ValueTuple2_ctr.DeclaringType!;
                Source=e.Expression.Call(
                    作業配列.MakeGenericMethod(
                        Reflection.ExtensionSet.SelectMany_collectionSelector_resultSelector,
                        TSource,TCollection,TResult
                    ),
                    Source,
                    collectionSelector,
                    resultSelector
                );
                if(TableReferences0_Count==3) {

                }
                Debug.Assert(Source.Type.GetGenericArguments()[0]==TResult);
                TSource=TResult;
                Element1=e.Expression.Parameter(TSource,$"<,>{this.番号++}");
            }
            e.Expression Expression0=Element1;
            for(var index=TableReferences0_Count-1;index>=0;index--){
                if(Expression0.Type.IsGenericType&&Expression0.Type.GetGenericTypeDefinition()==typeof(ValueTuple<,>)){
                    Array論理Parameter物理Expressions[index].物理=e.Expression.Field(Expression0,nameof(ValueTuple<int,int>.Item2));
                    Debug.Assert(Expression0.Type.GetGenericArguments().Length==2);
                    Expression0=e.Expression.Field(Expression0,nameof(ValueTuple<int,int>.Item1));
                }else{
                    Debug.Assert(index==0);
                    Array論理Parameter物理Expressions[index].物理=Expression0;
                }
            }
            ref var RefPeek=ref this.RefPeek;
            var Dictionary_DatabaseSchemaTable_ColumnExpression=RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression;
            var 変換_旧Parameterを新Expression=this.変換_旧Parameterを新Expression1;
            for(var a=0;a<TableReferences0_Count;a++){
                ref var 論理Parameter物理Expressions=ref Array論理Parameter物理Expressions[a];
                var 物理Expression=論理Parameter物理Expressions.物理;
                var 論理Parameter=論理Parameter物理Expressions.論理;
                foreach(var KV in Dictionary_DatabaseSchemaTable_ColumnExpression.ToList())
                    Dictionary_DatabaseSchemaTable_ColumnExpression[KV.Key]=変換_旧Parameterを新Expression.実行(KV.Value,論理Parameter,物理Expression);
            }
        }
        return(Source,Element1);
    }
    private void SelectElement(SelectElement x){
        switch(x){
            case SelectScalarExpression y:this.SelectScalarExpression(y);break;
            case SelectStarExpression y:this.SelectStarExpression(y);break;
            case SelectSetVariable y:this.SelectSetVariable(y);break;
            default:throw this.単純NotSupportedException(x);
        }
    }
    private void SelectScalarExpression(SelectScalarExpression x){
        ref var RefPeek=ref this.RefPeek;
        var Result=this.ScalarExpression(x.Expression);
        this.判定指定Table.実行(Result,RefPeek.List_TableExpression);
        var 出力TableExpressions=this.出力TableExpressions;
        if(出力TableExpressions.Count>0) { 
            var Predicate=e.Expression.NotEqual(出力TableExpressions[0],Constant_null);
            var 出力TableExpressions_Count=出力TableExpressions.Count;
            for(var a=1;a<出力TableExpressions_Count;a++)
                Predicate=e.Expression.AndAlso(Predicate,e.Expression.NotEqual(出力TableExpressions[a],Constant_null));
            Result=e.Expression.Condition(Predicate,Result,e.Expression.Default(Result.Type));
        }
        //RefPeek.List_TableExpression
        //ResultにTableExpressionがnullだとnullを返すべき式があるか。あればCoalesce
        //    e.Expression.Coalesce()
        RefPeek.List_ColumnExpression.Add(Result);
        if(x.ColumnName is not null){
            RefPeek.List_ColumnAlias.Add(x.ColumnName.Value);
        }else if(x.Expression is ColumnReferenceExpression ColumnReferenceExpression){
            var Identifiers=ColumnReferenceExpression.MultiPartIdentifier.Identifiers;
            RefPeek.List_ColumnAlias.Add(Identifiers[^1].Value);
        }else{
            //SELECT 列名1,列名2 UNION SELECT 345←列名無し
            RefPeek.List_ColumnAlias.Add(this.番号++.ToString(CultureInfo.CurrentCulture));
        }
    }
    private void SelectStarExpression(SelectStarExpression x){
        ref var RefPeek0=ref this.RefPeek;
        var RefPeek0_List_ColumnAlias=RefPeek0.List_ColumnAlias;
        var RefPeek0_ColumnExpression=RefPeek0.List_ColumnExpression;
        var RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression=RefPeek0.Dictionary_DatabaseSchemaTable_ColumnExpression;
        if(x.Qualifier is not null){
            //Database.Schema.Table.*
            //Schema.Table.*
            //Table.*
            var Key=this.SQL取得(x.Qualifier);
            var ColumnAliases=RefPeek0.Dictionary_TableAlias_ColumnAliases[Key];
            RefPeek0_List_ColumnAlias.AddRange(ColumnAliases);
            foreach(var ColumnAlias in ColumnAliases)
                RefPeek0_ColumnExpression.Add(RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression[Key+'.'+ColumnAlias]);
        }else{
            Debug.Assert("*"==this.SQL取得(x));
            RefPeek0_List_ColumnAlias.AddRange(RefPeek0.List_アスタリスクColumnAlias);
            RefPeek0_ColumnExpression.AddRange(RefPeek0.List_アスタリスクColumnExpression);
        }
    }
    /// <summary>
    /// SELECT @ABC=3*4,x.AssignmentKindは処理していない。
    /// </summary>
    /// <param name="x"></param>
    private void SelectSetVariable(SelectSetVariable x){
        //select @theId=DATABASE_PRINCIPAL_ID();    
        //x.AssignmentKind=AssignmentKind.
        Debug.Assert(x.AssignmentKind==AssignmentKind.Equals);
        var Variable=this.VariableReference(x.Variable);
        var Expression=this.ScalarExpression(x.Expression);
        Expression=this.Convertデータ型を合わせるNullableは想定する(Expression,Variable.Type);
        //メッセージ 141、レベル 15、状態 1、行 2
        //変数に値を代入する SELECT ステートメントを、データ取得操作と組み合わせることはできません。
        this.RefPeek.List_ColumnExpression.Add(e.Expression.Assign(Variable,Expression));
        //this._StackSubquery単位の情報.RefPeek.List_ColumnExpression.Add(
        //    Expressions.Expression.Assign(
        //        this.VariableReference(x.Variable),
        //        this.Nullableにする(
        //            this.ScalarExpression(x.Expression)
        //        )
        //    )
        //);
    }
    private e.Expression TopRowFilter(TopRowFilter x){
        return Default_void;
    }
    private e.Expression SearchPropertyListAction(SearchPropertyListAction x)=>x switch{
        AddSearchPropertyListAction y=>this.AddSearchPropertyListAction(y),
        DropSearchPropertyListAction y=>this.DropSearchPropertyListAction(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CreateLoginSource(CreateLoginSource x)=>x switch{
        AsymmetricKeyCreateLoginSource y=>this.AsymmetricKeyCreateLoginSource(y),
        CertificateCreateLoginSource y=>this.CertificateCreateLoginSource(y),
        ExternalCreateLoginSource                             y=>this.ExternalCreateLoginSource(y),
        PasswordCreateLoginSource y=>this.PasswordCreateLoginSource(y),
        WindowsCreateLoginSource y=>this.WindowsCreateLoginSource(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression DialogOption(DialogOption x)=>x switch{
        ScalarExpressionDialogOption y=>this.ScalarExpressionDialogOption(y),
        OnOffDialogOption y=>this.OnOffDialogOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression TSqlFragmentSnippet(TSqlFragmentSnippet x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression TSqlScript(TSqlScript x){
        var Batches=x.Batches;
        var Batches_Count=Batches.Count;
        if(Batches_Count==1)return this.TSqlBatch(Batches[0]);
        else{
            var ExpressionArray=new e.Expression[Batches_Count];
            for(var a=0;a<Batches_Count;a++)
                ExpressionArray[a]=this.TSqlBatch(Batches[a]);
            return e.Expression.Block(ExpressionArray);
        }
    }
    /// <summary>
    /// SELECT xxxx;UPDATE yyyy;SELECT zzzz;
    /// ";"が1ステートメントに対応する。
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression Statements(IList<TSqlStatement> x){
        var Statements_Count=x.Count;
        if(Statements_Count==1)return this.TSqlStatement(x[0]);
        var ListExpression=new List<e.Expression>();
        for(var a=0;a<Statements_Count;a++){
            var Expression=this.TSqlStatement(x[a]);
            if(Expression!=Default_void)ListExpression.Add(Expression);
            //SELECT @package_name = name,@folderid = folderid FROM dbo.sysssispackages
            //ここでクリアすべき
            //SELECT @foldername = foldername,@folderid = parentfolderid FROM dbo.sysssispackagefolders WHERE folderid = @prevfolderid
        }
        return e.Expression.Block(ListExpression);
    }
    /// <summary>
    /// SELECT xxxx;UPDATE yyyy;SELECT zzzz;
    /// GO
    /// DELETE wwww;
    /// GO
    /// "GO"が1バッチ処理。
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression TSqlBatch(TSqlBatch x){
        var Statements=this.Statements(x.Statements);
        return Statements;
    }
    private e.Expression MergeAction(MergeAction x)=>x switch{
        UpdateMergeAction y=>this.UpdateMergeAction(y),
        DeleteMergeAction y=>this.DeleteMergeAction(y),
        InsertMergeAction y=>this.InsertMergeAction(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression AuditSpecificationDetail(AuditSpecificationDetail x)=>x switch{
        AuditActionSpecification y=>this.AuditActionSpecification(y),
        AuditActionGroupReference y=>this.AuditActionGroupReference(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression AuditOption(AuditOption x)=>x switch{
        QueueDelayAuditOption y=>this.QueueDelayAuditOption(y),
        AuditGuidAuditOption y=>this.AuditGuidAuditOption(y),
        OnFailureAuditOption y=>this.OnFailureAuditOption(y),
        StateAuditOption y=>this.StateAuditOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression AuditTargetOption(AuditTargetOption x)=>x switch{
        MaxSizeAuditTargetOption y=>this.MaxSizeAuditTargetOption(y),
        MaxRolloverFilesAuditTargetOption y=>this.MaxRolloverFilesAuditTargetOption(y),
        LiteralAuditTargetOption y=>this.LiteralAuditTargetOption(y),
        OnOffAuditTargetOption y=>this.OnOffAuditTargetOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    //private e.Expression EventDeclaration(EventDeclaration x){
    //    throw this.単純NotSupportedException(x);
    //}
    //private e.Expression EventDeclarationSetParameter(EventDeclarationSetParameter x){
    //    throw this.単純NotSupportedException(x);
    //}
    //private e.Expression TargetDeclaration(TargetDeclaration x){
    //    throw this.単純NotSupportedException(x);
    //}
    private e.Expression SessionOption(SessionOption x)=>x switch{
        EventRetentionSessionOption y=>this.EventRetentionSessionOption(y),
        MemoryPartitionSessionOption y=>this.MemoryPartitionSessionOption(y),
        LiteralSessionOption y=>this.LiteralSessionOption(y),
        MaxDispatchLatencySessionOption y=>this.MaxDispatchLatencySessionOption(y),
        OnOffSessionOption y=>this.OnOffSessionOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression SpatialIndexOption(SpatialIndexOption x)=>x switch{
        SpatialIndexRegularOption y=>this.SpatialIndexRegularOption(y),
        BoundingBoxSpatialIndexOption y=>this.BoundingBoxSpatialIndexOption(y),
        GridsSpatialIndexOption y=>this.GridsSpatialIndexOption(y),
        CellsPerObjectSpatialIndexOption y=>this.CellsPerObjectSpatialIndexOption(y),
        _=>throw this.単純NotSupportedException(x.GetType())
    };
    private e.Expression WindowFrameClause(WindowFrameClause x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression WindowDelimiter(WindowDelimiter x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression WithinGroupClause(WithinGroupClause x){
        throw this.単純NotSupportedException(x);
    }
}
