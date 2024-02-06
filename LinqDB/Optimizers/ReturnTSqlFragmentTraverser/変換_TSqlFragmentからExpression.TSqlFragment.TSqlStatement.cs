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
using LinqDB.Reflection;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
using static Common;
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression AlterAsymmetricKeyStatement(AlterAsymmetricKeyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterAuthorizationStatement(AlterAuthorizationStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterCreateEndpointStatementBase(AlterCreateEndpointStatementBase x)=>x switch{
        CreateEndpointStatement y=>this.CreateEndpointStatement(y),
        AlterEndpointStatement y=>this.AlterEndpointStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression AlterCreateServiceStatementBase(AlterCreateServiceStatementBase x)=>x switch{
        CreateServiceStatement y=>this.CreateServiceStatement(y),
        AlterServiceStatement y=>this.AlterServiceStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression AlterCryptographicProviderStatement(AlterCryptographicProviderStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterDatabaseScopedConfigurationStatement(AlterDatabaseScopedConfigurationStatement x)=>x switch{
        AlterDatabaseScopedConfigurationSetStatement y=>this.AlterDatabaseScopedConfigurationSetStatement(y),
        AlterDatabaseScopedConfigurationClearStatement y=>this.AlterDatabaseScopedConfigurationClearStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression AlterDatabaseStatement(AlterDatabaseStatement x)=>x switch{
        AlterDatabaseCollateStatement y=>this.AlterDatabaseCollateStatement(y),
        AlterDatabaseRebuildLogStatement y=>this.AlterDatabaseRebuildLogStatement(y),
        AlterDatabaseAddFileStatement y=>this.AlterDatabaseAddFileStatement(y),
        AlterDatabaseAddFileGroupStatement y=>this.AlterDatabaseAddFileGroupStatement(y),
        AlterDatabaseRemoveFileGroupStatement y=>this.AlterDatabaseRemoveFileGroupStatement(y),
        AlterDatabaseRemoveFileStatement y=>this.AlterDatabaseRemoveFileStatement(y),
        AlterDatabaseModifyNameStatement y=>this.AlterDatabaseModifyNameStatement(y),
        AlterDatabaseModifyFileStatement y=>this.AlterDatabaseModifyFileStatement(y),
        AlterDatabaseModifyFileGroupStatement y=>this.AlterDatabaseModifyFileGroupStatement(y),
        AlterDatabaseSetStatement y=>this.AlterDatabaseSetStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression AlterFederationStatement(AlterFederationStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterFullTextIndexStatement(AlterFullTextIndexStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterFullTextStopListStatement(AlterFullTextStopListStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterLoginStatement(AlterLoginStatement x)=>x switch{
        AlterLoginOptionsStatement y=>this.AlterLoginOptionsStatement(y),
        AlterLoginEnableDisableStatement y=>this.AlterLoginEnableDisableStatement(y),
        AlterLoginAddDropCredentialStatement y=>this.AlterLoginAddDropCredentialStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression AlterPartitionFunctionStatement(AlterPartitionFunctionStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterPartitionSchemeStatement(AlterPartitionSchemeStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterResourceGovernorStatement(AlterResourceGovernorStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterSchemaStatement(AlterSchemaStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterSearchPropertyListStatement(AlterSearchPropertyListStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterServerConfigurationSetBufferPoolExtensionStatement(AlterServerConfigurationSetBufferPoolExtensionStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterServerConfigurationSetDiagnosticsLogStatement(AlterServerConfigurationSetDiagnosticsLogStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterServerConfigurationSetFailoverClusterPropertyStatement(AlterServerConfigurationSetFailoverClusterPropertyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterServerConfigurationSetHadrClusterStatement(AlterServerConfigurationSetHadrClusterStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterServerConfigurationSetSoftNumaStatement(AlterServerConfigurationSetSoftNumaStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterServerConfigurationStatement(AlterServerConfigurationStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterServiceMasterKeyStatement(AlterServiceMasterKeyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterTableStatement(AlterTableStatement x)=>x switch{
        AlterTableAddTableElementStatement            y=>this.AlterTableAddTableElementStatement(y),
        AlterTableAlterColumnStatement                y=>this.AlterTableAlterColumnStatement(y),
        AlterTableAlterIndexStatement                 y=>this.AlterTableAlterIndexStatement(y),
        AlterTableChangeTrackingModificationStatement y=>this.AlterTableChangeTrackingModificationStatement(y),
        AlterTableConstraintModificationStatement     y=>this.AlterTableConstraintModificationStatement(y),
        AlterTableDropTableElementStatement           y=>this.AlterTableDropTableElementStatement(y),
        AlterTableFileTableNamespaceStatement         y=>this.AlterTableFileTableNamespaceStatement(y),
        AlterTableRebuildStatement                    y=>this.AlterTableRebuildStatement(y),
        AlterTableSetStatement                        y=>this.AlterTableSetStatement(y),
        AlterTableSwitchStatement                     y=>this.AlterTableSwitchStatement(y),
        AlterTableTriggerModificationStatement        y=>this.AlterTableTriggerModificationStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression AlterXmlSchemaCollectionStatement(AlterXmlSchemaCollectionStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ApplicationRoleStatement(ApplicationRoleStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AssemblyStatement(AssemblyStatement x)=>x switch{
        CreateAssemblyStatement y=>this.CreateAssemblyStatement(y),
        AlterAssemblyStatement y=>this.AlterAssemblyStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CreateAssemblyStatement(CreateAssemblyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterAssemblyStatement(AlterAssemblyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AuditSpecificationStatement(AuditSpecificationStatement x)=>x switch{
        CreateDatabaseAuditSpecificationStatement y=>this.CreateDatabaseAuditSpecificationStatement(y),
        AlterDatabaseAuditSpecificationStatement y=>this.AlterDatabaseAuditSpecificationStatement(y),
        CreateServerAuditSpecificationStatement y=>this.CreateServerAuditSpecificationStatement(y),
        AlterServerAuditSpecificationStatement y=>this.AlterServerAuditSpecificationStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CreateDatabaseAuditSpecificationStatement(CreateDatabaseAuditSpecificationStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterDatabaseAuditSpecificationStatement(AlterDatabaseAuditSpecificationStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateServerAuditSpecificationStatement(CreateServerAuditSpecificationStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterServerAuditSpecificationStatement(AlterServerAuditSpecificationStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AvailabilityGroupStatement(AvailabilityGroupStatement x)=>x switch{
        CreateAvailabilityGroupStatement y=>this.CreateAvailabilityGroupStatement(y),
        AlterAvailabilityGroupStatement y=>this.AlterAvailabilityGroupStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CreateAvailabilityGroupStatement(CreateAvailabilityGroupStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterAvailabilityGroupStatement(AlterAvailabilityGroupStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression BackupRestoreMasterKeyStatementBase(BackupRestoreMasterKeyStatementBase x)=>x switch{
        BackupServiceMasterKeyStatement  y=>this.BackupServiceMasterKeyStatement(y),
        BackupMasterKeyStatement         y=>this.BackupMasterKeyStatement(y),
        RestoreServiceMasterKeyStatement y=>this.RestoreServiceMasterKeyStatement(y),
        RestoreMasterKeyStatement        y=>this.RestoreMasterKeyStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression BackupStatement(BackupStatement x)=>x switch{
        BackupDatabaseStatement y=>this.BackupDatabaseStatement(y),
        BackupTransactionLogStatement y=>this.BackupTransactionLogStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression BeginConversationTimerStatement(BeginConversationTimerStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression BeginDialogStatement(BeginDialogStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression BeginEndBlockStatement(BeginEndBlockStatement x)=>x switch{
        BeginEndAtomicBlockStatement y=>this.BeginEndAtomicBlockStatement(y),
        _=>this.StatementList(x.StatementList)
    };
    /// <summary>
    /// begin atmic～end アトミックブロック
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression BeginEndAtomicBlockStatement(BeginEndAtomicBlockStatement x){
        foreach(var Option in x.Options){
            this.AtomicBlockOption(Option);
        }
        return this.StatementList(x.StatementList);
    }
    private e.Expression BreakStatement(BreakStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression BrokerPriorityStatement(BrokerPriorityStatement x)=>x switch{
        CreateBrokerPriorityStatement y=>this.CreateBrokerPriorityStatement(y),
        AlterBrokerPriorityStatement y=>this.AlterBrokerPriorityStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CreateBrokerPriorityStatement(CreateBrokerPriorityStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterBrokerPriorityStatement(AlterBrokerPriorityStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression BulkInsertBase(BulkInsertBase x)=>x switch{
        BulkInsertStatement y=>this.BulkInsertStatement(y),
        InsertBulkStatement y=>this.InsertBulkStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CertificateStatementBase(CertificateStatementBase x)=>x switch{
        CreateCertificateStatement y=>this.CreateCertificateStatement(y),
        AlterCertificateStatement y=>this.AlterCertificateStatement(y),
        BackupCertificateStatement y=>this.BackupCertificateStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CreateCertificateStatement(CreateCertificateStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterCertificateStatement(AlterCertificateStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CheckpointStatement(CheckpointStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CloseMasterKeyStatement(CloseMasterKeyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CloseSymmetricKeyStatement(CloseSymmetricKeyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ColumnEncryptionKeyStatement(ColumnEncryptionKeyStatement x)=>x switch{
        CreateColumnEncryptionKeyStatement y=>this.CreateColumnEncryptionKeyStatement(y),
        AlterColumnEncryptionKeyStatement y=>this.AlterColumnEncryptionKeyStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CreateColumnEncryptionKeyStatement(CreateColumnEncryptionKeyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterColumnEncryptionKeyStatement(AlterColumnEncryptionKeyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ContinueStatement(ContinueStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateAggregateStatement(CreateAggregateStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateAsymmetricKeyStatement(CreateAsymmetricKeyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateColumnMasterKeyStatement(CreateColumnMasterKeyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateColumnStoreIndexStatement(CreateColumnStoreIndexStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateContractStatement(CreateContractStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateCryptographicProviderStatement(CreateCryptographicProviderStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateDatabaseStatement(CreateDatabaseStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateDefaultStatement(CreateDefaultStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateEventNotificationStatement(CreateEventNotificationStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateFederationStatement(CreateFederationStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateFullTextIndexStatement(CreateFullTextIndexStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateFullTextStopListStatement(CreateFullTextStopListStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateLoginStatement(CreateLoginStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreatePartitionFunctionStatement(CreatePartitionFunctionStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreatePartitionSchemeStatement(CreatePartitionSchemeStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateRuleStatement(CreateRuleStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateSchemaStatement(CreateSchemaStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateSearchPropertyListStatement(CreateSearchPropertyListStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateSpatialIndexStatement(CreateSpatialIndexStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateStatisticsStatement(CreateStatisticsStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateSynonymStatement(CreateSynonymStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateTableStatement(CreateTableStatement x){
        //DDLがあった場合
        System.Diagnostics.Trace.WriteLine("CREATE TABLE");
        return Default_void;
    }
    private e.Expression CreateTypeStatement(CreateTypeStatement x)=>x switch{
        CreateTypeUdtStatement y=>this.CreateTypeUdtStatement(y),
        CreateTypeUddtStatement y=>this.CreateTypeUddtStatement(y),
        CreateTypeTableStatement y=>this.CreateTypeTableStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CreateTypeUdtStatement(CreateTypeUdtStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateTypeUddtStatement(CreateTypeUddtStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateTypeTableStatement(CreateTypeTableStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateXmlSchemaCollectionStatement(CreateXmlSchemaCollectionStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CredentialStatement(CredentialStatement x)=>x switch{
        CreateCredentialStatement y=>this.CreateCredentialStatement(y),
        AlterCredentialStatement y=>this.AlterCredentialStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CreateCredentialStatement(CreateCredentialStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterCredentialStatement(AlterCredentialStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CursorStatement(CursorStatement x)=>x switch{
        OpenCursorStatement y=>this.OpenCursorStatement(y),
        CloseCursorStatement y=>this.CloseCursorStatement(y),
        DeallocateCursorStatement y=>this.DeallocateCursorStatement(y),
        FetchCursorStatement y=>this.FetchCursorStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression OpenCursorStatement(OpenCursorStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CloseCursorStatement(CloseCursorStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DeallocateCursorStatement(DeallocateCursorStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression FetchCursorStatement(FetchCursorStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DatabaseEncryptionKeyStatement(DatabaseEncryptionKeyStatement x)=>x switch{
        CreateDatabaseEncryptionKeyStatement y=>this.CreateDatabaseEncryptionKeyStatement(y),
        AlterDatabaseEncryptionKeyStatement y=>this.AlterDatabaseEncryptionKeyStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CreateDatabaseEncryptionKeyStatement(CreateDatabaseEncryptionKeyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterDatabaseEncryptionKeyStatement(AlterDatabaseEncryptionKeyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DbccStatement(DbccStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DeclareCursorStatement(DeclareCursorStatement x){
        throw this.単純NotSupportedException(x);
    }
    /// <summary>
    /// テーブル変数宣言
    /// declare @ids table(maj int primary key,nam sysname)
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression DeclareTableVariableStatement(DeclareTableVariableStatement x){
        return this.DeclareTableVariableBody(x.Body);
    }
    private e.Expression DeclareVariableStatement(DeclareVariableStatement x){
        var Declarations=x.Declarations;
        var Declarations_Count=Declarations.Count;
        if(Declarations_Count==1){
            var Result=this.DeclareVariableElement(Declarations[0]);
            return Result;
        }else{
            var Block_Expressions=new List<e.Expression>();
            for(var a=0;a<Declarations_Count;a++){
                var DeclareVariableElement=this.DeclareVariableElement(Declarations[a]);
                if(DeclareVariableElement!=Default_void){
                    Block_Expressions.Add(this.DeclareVariableElement(Declarations[a]));
                }
            }
            if(Block_Expressions.Count==0)return Default_void;
            return e.Expression.Block(Block_Expressions);
        }
    }
    private e.Expression DiskStatement(DiskStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropChildObjectsStatement(DropChildObjectsStatement x)=>x switch{
        DropStatisticsStatement y=>this.DropStatisticsStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression DropStatisticsStatement(DropStatisticsStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropDatabaseEncryptionKeyStatement(DropDatabaseEncryptionKeyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropDatabaseStatement(DropDatabaseStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropEventNotificationStatement(DropEventNotificationStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropFullTextIndexStatement(DropFullTextIndexStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropIndexStatement(DropIndexStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropObjectsStatement(DropObjectsStatement x)=>x switch{
        DropSequenceStatement y=>this.DropSequenceStatement(y),
        DropSecurityPolicyStatement y=>this.DropSecurityPolicyStatement(y),
        DropExternalTableStatement y=>this.DropExternalTableStatement(y),
        DropTableStatement y=>this.DropTableStatement(y),
        DropProcedureStatement y=>this.DropProcedureStatement(y),
        DropFunctionStatement y=>this.DropFunctionStatement(y),
        DropViewStatement y=>this.DropViewStatement(y),
        DropDefaultStatement y=>this.DropDefaultStatement(y),
        DropRuleStatement y=>this.DropRuleStatement(y),
        DropTriggerStatement y=>this.DropTriggerStatement(y),
        DropSynonymStatement y=>this.DropSynonymStatement(y),
        DropAggregateStatement y=>this.DropAggregateStatement(y),
        DropAssemblyStatement y=>this.DropAssemblyStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression DropSequenceStatement(DropSequenceStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropSecurityPolicyStatement(DropSecurityPolicyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropExternalTableStatement(DropExternalTableStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropTableStatement(DropTableStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropProcedureStatement(DropProcedureStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropFunctionStatement(DropFunctionStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropViewStatement(DropViewStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropDefaultStatement(DropDefaultStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropRuleStatement(DropRuleStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropTriggerStatement(DropTriggerStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropSynonymStatement(DropSynonymStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropAggregateStatement(DropAggregateStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropAssemblyStatement(DropAssemblyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropQueueStatement(DropQueueStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropSchemaStatement(DropSchemaStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropTypeStatement(DropTypeStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropUnownedObjectStatement(DropUnownedObjectStatement x)=>x switch{
        DropApplicationRoleStatement            y=>this.DropApplicationRoleStatement(y),
        DropAsymmetricKeyStatement              y=>this.DropAsymmetricKeyStatement(y),
        DropAvailabilityGroupStatement          y=>this.DropAvailabilityGroupStatement(y),
        DropBrokerPriorityStatement             y=>this.DropBrokerPriorityStatement(y),
        DropCertificateStatement                y=>this.DropCertificateStatement(y),
        DropColumnEncryptionKeyStatement        y=>this.DropColumnEncryptionKeyStatement(y),
        DropColumnMasterKeyStatement            y=>this.DropColumnMasterKeyStatement(y),
        DropContractStatement                   y=>this.DropContractStatement(y),
        DropCredentialStatement                 y=>this.DropCredentialStatement(y),
        DropCryptographicProviderStatement      y=>this.DropCryptographicProviderStatement(y),
        DropDatabaseAuditSpecificationStatement y=>this.DropDatabaseAuditSpecificationStatement(y),
        DropEndpointStatement                   y=>this.DropEndpointStatement(y),
        DropEventSessionStatement               y=>this.DropEventSessionStatement(y),
        DropExternalDataSourceStatement         y=>this.DropExternalDataSourceStatement(y),
        DropExternalFileFormatStatement         y=>this.DropExternalFileFormatStatement(y),
        DropExternalResourcePoolStatement       y=>this.DropExternalResourcePoolStatement(y),
        DropFederationStatement                 y=>this.DropFederationStatement(y),
        DropFullTextCatalogStatement            y=>this.DropFullTextCatalogStatement(y),
        DropFullTextStopListStatement           y=>this.DropFullTextStopListStatement(y),
        DropLoginStatement                      y=>this.DropLoginStatement(y),
        DropMessageTypeStatement                y=>this.DropMessageTypeStatement(y),
        DropPartitionFunctionStatement          y=>this.DropPartitionFunctionStatement(y),
        DropPartitionSchemeStatement            y=>this.DropPartitionSchemeStatement(y),
        DropRemoteServiceBindingStatement       y=>this.DropRemoteServiceBindingStatement(y),
        DropResourcePoolStatement               y=>this.DropResourcePoolStatement(y),
        DropRoleStatement                       y=>this.DropRoleStatement(y),
        DropRouteStatement                      y=>this.DropRouteStatement(y),
        DropSearchPropertyListStatement         y=>this.DropSearchPropertyListStatement(y),
        DropServerAuditSpecificationStatement   y=>this.DropServerAuditSpecificationStatement(y),
        DropServerAuditStatement                y=>this.DropServerAuditStatement(y),
        DropServerRoleStatement                 y=>this.DropServerRoleStatement(y),
        DropServiceStatement                    y=>this.DropServiceStatement(y),
        DropSymmetricKeyStatement               y=>this.DropSymmetricKeyStatement(y),
        DropUserStatement                       y=>this.DropUserStatement(y),
        DropWorkloadGroupStatement              y=>this.DropWorkloadGroupStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression DropApplicationRoleStatement(DropApplicationRoleStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropAsymmetricKeyStatement(DropAsymmetricKeyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropAvailabilityGroupStatement(DropAvailabilityGroupStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropBrokerPriorityStatement(DropBrokerPriorityStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropCertificateStatement(DropCertificateStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropColumnEncryptionKeyStatement(DropColumnEncryptionKeyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropColumnMasterKeyStatement(DropColumnMasterKeyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropContractStatement(DropContractStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropCredentialStatement(DropCredentialStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropCryptographicProviderStatement(DropCryptographicProviderStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropDatabaseAuditSpecificationStatement(DropDatabaseAuditSpecificationStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropEndpointStatement(DropEndpointStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropEventSessionStatement(DropEventSessionStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropExternalDataSourceStatement(DropExternalDataSourceStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropExternalFileFormatStatement(DropExternalFileFormatStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropExternalResourcePoolStatement(DropExternalResourcePoolStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropFederationStatement(DropFederationStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropFullTextCatalogStatement(DropFullTextCatalogStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropFullTextStopListStatement(DropFullTextStopListStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropLoginStatement(DropLoginStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropMasterKeyStatement(DropMasterKeyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropMessageTypeStatement(DropMessageTypeStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropPartitionFunctionStatement(DropPartitionFunctionStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropPartitionSchemeStatement(DropPartitionSchemeStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropRemoteServiceBindingStatement(DropRemoteServiceBindingStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropResourcePoolStatement(DropResourcePoolStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropRoleStatement(DropRoleStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropRouteStatement(DropRouteStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropSearchPropertyListStatement(DropSearchPropertyListStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropServerAuditSpecificationStatement(DropServerAuditSpecificationStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropServerAuditStatement(DropServerAuditStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropServerRoleStatement(DropServerRoleStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropServiceStatement(DropServiceStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropSymmetricKeyStatement(DropSymmetricKeyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropUserStatement(DropUserStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropWorkloadGroupStatement(DropWorkloadGroupStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DropXmlSchemaCollectionStatement(DropXmlSchemaCollectionStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression EnableDisableTriggerStatement(EnableDisableTriggerStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression EndConversationStatement(EndConversationStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression EventSessionStatement(EventSessionStatement x)=>x switch{
        CreateEventSessionStatement y=>this.CreateEventSessionStatement(y),
        AlterEventSessionStatement y=>this.AlterEventSessionStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CreateEventSessionStatement(CreateEventSessionStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterEventSessionStatement(AlterEventSessionStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ExecuteAsStatement(ExecuteAsStatement x){
        //-- SQL Server Syntax  
        //Functions(except inline table-valued functions),Stored Procedures,and DML Triggers  
        //{ EXEC | EXECUTE }AS{ CALLER | SELF | OWNER | 'user_name' }  

        //DDL Triggers with Database Scope  
        //{ EXEC | EXECUTE }AS{ CALLER | SELF | 'user_name' }  

        //DDL Triggers with Server Scope and logon triggers  
        //{ EXEC | EXECUTE }AS{ CALLER | SELF | 'login_name' }  

        //Queues  
        //{ EXEC | EXECUTE }AS{ SELF | OWNER | 'user_name' }
        return Default_void;
    }
    private e.Expression ExecuteStatement(ExecuteStatement x){
        var ExecuteSpecification=this.ExecuteSpecification(x.ExecuteSpecification);
        var l=new List<e.Expression>();
        foreach(var Option in x.Options) 
            l.Add(this.ExecuteOption(Option));
        return ExecuteSpecification;
    }
    private e.Expression ExternalDataSourceStatement(ExternalDataSourceStatement x)=>x switch{
        CreateExternalDataSourceStatement y=>this.CreateExternalDataSourceStatement(y),
        AlterExternalDataSourceStatement y=>this.AlterExternalDataSourceStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression ExternalFileFormatStatement(ExternalFileFormatStatement x)=>x switch{
        CreateExternalFileFormatStatement y=>this.CreateExternalFileFormatStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression ExternalLanguageStatement(ExternalLanguageStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression ExternalLibraryStatement(ExternalLibraryStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression ExternalResourcePoolStatement(ExternalResourcePoolStatement x)=>x switch{
        AlterExternalResourcePoolStatement y=>this.AlterExternalResourcePoolStatement(y),
        CreateExternalResourcePoolStatement y=>this.CreateExternalResourcePoolStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression ExternalStreamStatement(ExternalStreamStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression ExternalStreamingJobStatement(ExternalStreamingJobStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression ExternalTableStatement(ExternalTableStatement x)=>x switch{
        CreateExternalTableStatement y=>this.CreateExternalTableStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression FullTextCatalogStatement(FullTextCatalogStatement x)=>x switch{
        CreateFullTextCatalogStatement y=>this.CreateFullTextCatalogStatement(y),
        AlterFullTextCatalogStatement y=>this.AlterFullTextCatalogStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression GoToStatement(GoToStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression IfStatement(IfStatement x){
        var test=this.BooleanExpression(x.Predicate);
        var ifTrue=this.TSqlStatement(x.ThenStatement);
        e.Expression ifFalse;
        if(x.ElseStatement is null)
            ifFalse=Default_void;
        else
            ifFalse=this.TSqlStatement(x.ElseStatement);
        return e.Expression.IfThenElse(
            test,
            ifTrue,
            ifFalse
        );
    }
    private e.Expression IndexDefinition(IndexDefinition x){
        foreach(var Column in x.Columns)
            this.ColumnWithSortOrder(Column);
        foreach(var IncludeColumn in x.IncludeColumns)
            this.ColumnReferenceExpression(IncludeColumn);
        this.IdentifierOrValueExpression(x.FileStreamOn);
        this.BooleanExpression(x.FilterPredicate);
        foreach(var IndexOption in x.IndexOptions)
            this.IndexOption(IndexOption);
        this.IndexType(x.IndexType);
        this.Identifier(x.Name);
        this.FileGroupOrPartitionScheme(x.OnFileGroupOrPartitionScheme);
        var Unique=x.Unique;
        throw this.単純NotSupportedException(x);
    }
    private e.Expression IndexStatement(IndexStatement x)=>x switch{
        AlterIndexStatement              y=>this.AlterIndexStatement(y),
        CreateIndexStatement             y=>this.CreateIndexStatement(y),
        CreateSelectiveXmlIndexStatement y=>this.CreateSelectiveXmlIndexStatement(y),
        CreateXmlIndexStatement          y=>this.CreateXmlIndexStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression KillQueryNotificationSubscriptionStatement(KillQueryNotificationSubscriptionStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression KillStatement(KillStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression KillStatsJobStatement(KillStatsJobStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression LabelStatement(LabelStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression LineNoStatement(LineNoStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression MasterKeyStatement(MasterKeyStatement x)=>x switch{
        CreateMasterKeyStatement y=>this.CreateMasterKeyStatement(y),
        AlterMasterKeyStatement y=>this.AlterMasterKeyStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression MessageTypeStatementBase(MessageTypeStatementBase x)=>x switch{
        CreateMessageTypeStatement y=>this.CreateMessageTypeStatement(y),
        AlterMessageTypeStatement y=>this.AlterMessageTypeStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CreateMessageTypeStatement(CreateMessageTypeStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterMessageTypeStatement(AlterMessageTypeStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression MergeStatement(MergeStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression MoveConversationStatement(MoveConversationStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression OpenMasterKeyStatement(OpenMasterKeyStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression OpenSymmetricKeyStatement(OpenSymmetricKeyStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression PrintStatement(PrintStatement x){
        var Expression=this.ScalarExpression(x.Expression);
        return e.Expression.Call(
            Reflection.Trace.WriteLine,
            Expression
        );
    }
    private e.Expression ProcedureStatementBodyBase(ProcedureStatementBodyBase x)=>x switch{
        ProcedureStatementBody y=>this.ProcedureStatementBody(y),
        FunctionStatementBody y=>this.FunctionStatementBody(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression ProcedureStatementBody(ProcedureStatementBody x)=>x switch{
        AlterProcedureStatement y=>this.AlterProcedureStatement(y),
        CreateProcedureStatement y=>this.CreateProcedureStatement(y),
        CreateOrAlterProcedureStatement y=>this.CreateOrAlterProcedureStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression AlterProcedureStatement(AlterProcedureStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.LabelTarget? ReturnLabel;
    //private e.ParameterExpression? ReturnParameter;
    private e.Expression CreateProcedureStatement(CreateProcedureStatement x){
        this.Statementの共通初期化();
        var ContainerType=this.ContainerType;
        var Name=x.ProcedureReference.Name;
        var Schema=Name.SchemaIdentifier is null ? "dbo":Name.SchemaIdentifier.Value;
        var Schema_FulllName=this.ContainerType.Namespace+".Schemas."+Schema;
        var Schema_Type=ContainerType.Assembly.GetType(Schema_FulllName.Replace("*",@"\*"),true,true);
        var Name_BaseIdentifier_Value=Name.BaseIdentifier.Value;

        var Method = FindFunction(Schema_Type,Name_BaseIdentifier_Value)!;
        //var Method =Schema_Type.GetMethod(Name_BaseIdentifier_Value,BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly);
        //var ReturnType=Method.ReturnType;
        var x_Parameters=x.Parameters;
        var x_Parameters_Count=x_Parameters.Count;
        var Types=new Type[x_Parameters_Count+1];
        for(var a = 0;a<x_Parameters_Count;a++) {
            //IsVarying 条件構文。不要かも
            var x_Parameter = x_Parameters[a];
            var Type = this.Nullableまたは参照型(DataTypeReferenceからTypeに変換(x_Parameter.DataType));
            Types[a]=Type;
            this.List_Parameter.Add(e.Expression.Parameter(Type,x_Parameter.VariableName.Value));
        }
        Types[x_Parameters_Count]=typeof(int);
        //e.Expression Body;
        //Debug(x.StatementList is not null) ;
        this.ReturnLabel=e.Expression.Label(typeof(int),"procedure");
        //this.ReturnLabel=null;
        if(x.MethodSpecifier is not null) this.MethodSpecifier(x.MethodSpecifier);
        e.Expression Body;
        try{
            Body=this.StatementList(x.StatementList);
        } catch(KeyNotFoundException){
            Body=Default_void;
        }
        if(this.Variables.Any())
            Body=e.Expression.Block(
                this.Variables,
                this.作業配列.Expressions設定(Body,Constant_0)
            );
        else
            Body=e.Expression.Block(Body,Constant_0);
        return e.Expression.Lambda(Reflection.Func.Get(x_Parameters_Count).MakeGenericType(Types),Body,Name_BaseIdentifier_Value,this.List_Parameter);
    }
    private e.Expression CreateOrAlterProcedureStatement(CreateOrAlterProcedureStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression FunctionStatementBody(FunctionStatementBody x)=>x switch{
        AlterFunctionStatement y=>this.AlterFunctionStatement(y),
        CreateFunctionStatement y=>this.CreateFunctionStatement(y),
        CreateOrAlterFunctionStatement y=>this.CreateOrAlterFunctionStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression AlterFunctionStatement(AlterFunctionStatement x){
        throw this.単純NotSupportedException(x);
    }
    private struct 情報CreateFunctionStatement {
        public string? 変数名;
        public e.ParameterExpression? ReturnVariable;
        //public e.LabelTarget? ReturnLabel;
    }
    //private 情報CreateFunctionStatement 変数CreateFunctionStatement;
    private e.Expression CreateFunctionStatement(CreateFunctionStatement x){
        this.Statementの共通初期化();
        var ContainerType=this.ContainerType;
        var Name=x.Name;
        var Schema=Name.SchemaIdentifier is null ? "dbo":Name.SchemaIdentifier.Value;
        var Schema_FulllName=this.ContainerType.Namespace+".Schemas."+Schema;
        var Schema_Type=ContainerType.Assembly.GetType(Schema_FulllName.Replace("*",@"\*"),true,true);
        var Name_BaseIdentifier_Value=Name.BaseIdentifier.Value;

        var Method = FindFunction(Schema_Type,Name_BaseIdentifier_Value)!;
        //var Method =Schema_Type.GetMethod(Name_BaseIdentifier_Value,BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly);
        var ReturnType=Method.ReturnType;
        var x_Parameters=x.Parameters;
        var x_Parameters_Count=x_Parameters.Count;
        var Types=new Type[x_Parameters_Count+1];
        for(var a = 0;a<x_Parameters_Count;a++) {
            //IsVarying 条件構文。不要かも
            var x_Parameter = x_Parameters[a];
            var Type = this.Nullableまたは参照型(DataTypeReferenceからTypeに変換(x_Parameter.DataType));
            Types[a]=Type;
            this.List_Parameter.Add(e.Expression.Parameter(Type,x_Parameter.VariableName.Value));
        }
        Types[x_Parameters_Count]=ReturnType;
        e.Expression Body;
        if(x.StatementList is not null) {
            //create function dbo.ufnGetContactInformation
            //(@ContactID int)
            //returns 
            //    @retContactInformation table (
            //        ContactID   int           primary key not null,
            //        FirstName   nvarchar (50) null,
            //        LastName    nvarchar (50) null,
            //        JobTitle    nvarchar (50) null,
            //        ContactType nvarchar (50) null)
            //as
            //begin
            //    return;
            //end

            //var ReturnType=this.FunctionReturnType(x.ReturnType);
            if(x.ReturnType is TableValuedFunctionReturnType y){
                var Variable_Name=this.Identifier(y.DeclareTableVariableBody.VariableName);
                //                        ScalarFunctionReturnType:
                //                      (TableValuedFunctionReturnType)x.ReturnType).Name;
                //.Name:this.TableValuedFunctionReturnType((TableValuedFunctionReturnType)x.ReturnType);
                //var 変数CreateFunctionStatement = this.変数CreateFunctionStatement;
                var ReturnLabel = this.ReturnLabel=e.Expression.Label("function");
                var 作業配列 = this.作業配列;
                var Variable_Type =作業配列.MakeGenericType(typeof(Set<>),ReturnType.GetGenericArguments()[0]);
                var Variable=e.Expression.Parameter(Variable_Type,Variable_Name);
                if(x.MethodSpecifier is not null) this.MethodSpecifier(x.MethodSpecifier);
                //var Types = new Type[x_Parameters_Count+1];
                this.AddTableVariable(Variable);//todo 名前がないからこれでいいのか？
                Body= this.StatementList(x.StatementList);
                Body=e.Expression.Block(
                    this.Variables,
                    作業配列.Expressions設定(
                        e.Expression.Assign(
                            Variable,
                            e.Expression.New(Variable_Type.GetConstructor(Type.EmptyTypes))
                        ),
                        Body,
                        e.Expression.Label(ReturnLabel),Variable
                    )
                );
                //if(ReturnType.IsGenericType&&ReturnType.GetGenericTypeDefinition()==typeof(Set<>)) { 
                //    Body=e.Expression.Block(
                //        this.Variables,
                //        作業配列.Expressions設定(
                //            e.Expression.Assign(
                //                Variable,
                //                e.Expression.New(Variable_Type.GetConstructor(Type.EmptyTypes))
                //            ),
                //            Body,
                //            e.Expression.Label(ReturnLabel),Variable
                //        )
                //    );
                //} else {
                //    Body=e.Expression.Block(
                //        this.Variables,
                //        作業配列.Expressions設定(Body,e.Expression.Label(ReturnLabel),Variable)
                //    );
                //}
                //return e.Expression.Lambda(Reflection.Func.Get(x_Parameters_Count).MakeGenericType(Types),StatementList,Name_BaseIdentifier_Value,List_Parameter);
            } else{
                Debug.Assert(
                    ReturnType.IsValueType&&ReturnType.GetGenericArguments()[0]==this.DataTypeReference(((ScalarFunctionReturnType)x.ReturnType).DataType)||
                    ReturnType==this.DataTypeReference(((ScalarFunctionReturnType)x.ReturnType).DataType)
                );
                var ReturnLabel = this.ReturnLabel=e.Expression.Label(ReturnType,"function");
                if(x.MethodSpecifier is not null) this.MethodSpecifier(x.MethodSpecifier);
                var StatementList=this.StatementList(x.StatementList);
                var Block=e.Expression.Block(
                    StatementList,
                    e.Expression.Label(ReturnLabel,e.Expression.Default(ReturnType))
                );
                Body=Block;
                //Body=this.Convertデータ型を合わせるNullableは想定する(Block,ReturnType);
                if(this.Variables.Any()) Body=e.Expression.Block(this.Variables,Body);
                //return e.Expression.Lambda(Reflection.Func.Get(x_Parameters_Count).MakeGenericType(Types),StatementList,Name_BaseIdentifier_Value,List_Parameter);
            }
        } else {
            //create function Application.DetermineCustomerAccess
            //(@CityID int)
            //returns table 
            //with schemabinding
            //as
            //return 
            //    (select 1 as AccessResult
            //     where  IS_ROLEMEMBER(N'db_owner') <> 0
            //            or IS_ROLEMEMBER((select sp.SalesTerritory
            //                              from   Application.Cities as c
            //                                     inner join
            //                                     Application.StateProvinces as sp
            //                                     on c.StateProvinceID = sp.StateProvinceID
            //                              where  c.CityID = @CityID) + N' Sales') <> 0
            //            or (ORIGINAL_LOGIN() = N'Website'
            //                and exists (select 1
            //                            from   Application.Cities as c
            //                                   inner join
            //                                   Application.StateProvinces as sp
            //                                   on c.StateProvinceID = sp.StateProvinceID
            //                            where  c.CityID = @CityID
            //                                   and sp.SalesTerritory = SESSION_CONTEXT(N'SalesTerritory'))))
            //Body=this.SelectFunctionReturnType((SelectFunctionReturnType)x.ReturnType);
            //Body=this.FunctionReturnType(x.ReturnType);
            Body= this.StatementList(x.StatementList);
            Body=this.ConvertNullable(Body);
            if(this.Variables.Any())Body=e.Expression.Block(this.Variables,Body);
            var ValueTuple_Type=IEnumerable1のT(Body.Type);
            var ValueTuple_p=e.Expression.Parameter(ValueTuple_Type,"ValueTuple_p");
            var Element_Type=IEnumerable1のT(ReturnType);
            var Constructor=Element_Type.GetConstructors()[0];
            var Parameters=Constructor.GetParameters();
            var Parameters_Length=Constructor.GetParameters().Length;
            var NewArguments=new e.Expression[Parameters_Length];
            var 作業配列=this.作業配列;
            e.Expression ValueTuple=ValueTuple_p;
            var Item番号=1;
            for(var a=0;a<Parameters_Length;a++)
                NewArguments[a]=this.Convertデータ型を合わせるNullableは想定する(ValueTuple_Item(ref ValueTuple,ref Item番号),Parameters[a].ParameterType);
            Body=e.Expression.Call(
                作業配列.MakeGenericMethod(Reflection.ExtensionSet.Select_selector,ValueTuple_Type,Element_Type),
                Body,
                e.Expression.Lambda(e.Expression.New(Constructor,NewArguments),作業配列.Parameters設定(ValueTuple_p))
            );
        }
        return e.Expression.Lambda(
            Reflection.Func.Get(x_Parameters_Count).MakeGenericType(Types),
            Body,
            Name_BaseIdentifier_Value,
            this.List_Parameter
        );
    }
    private e.Expression CreateOrAlterFunctionStatement(CreateOrAlterFunctionStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression QueueStatement(QueueStatement x)=>x switch{
        CreateQueueStatement y=>this.CreateQueueStatement(y),
        AlterQueueStatement y=>this.AlterQueueStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CreateQueueStatement(CreateQueueStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterQueueStatement(AlterQueueStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression RaiseErrorLegacyStatement(RaiseErrorLegacyStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression RaiseErrorStatement(RaiseErrorStatement x){
        var x_OptionalParameters=x.OptionalParameters;
        var x_OptionalParameters_Count=x_OptionalParameters.Count;
        var Arguments=new e.Expression[x_OptionalParameters_Count];
        for(var a=0;a<x_OptionalParameters_Count;a++){
            Arguments[a]=this.ScalarExpression(x_OptionalParameters[a]);
        }
        this.ScalarExpression(x.FirstParameter);
        this.ScalarExpression(x.SecondParameter);
        this.ScalarExpression(x.ThirdParameter);
        return e.Expression.Throw(
            e.Expression.New(
                Reflection.Exception.RaiseErrorException_ctor,
                this.ScalarExpression(x.FirstParameter),
                this.ScalarExpression(x.SecondParameter),
                this.ScalarExpression(x.ThirdParameter),
                e.Expression.NewArrayInit(
                    typeof(object),
                    Arguments
                )
            )
        );
    }
    private e.Expression ReadTextStatement(ReadTextStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression ReconfigureStatement(ReconfigureStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression RemoteServiceBindingStatementBase(RemoteServiceBindingStatementBase x)=>x switch{
        CreateRemoteServiceBindingStatement y=>this.CreateRemoteServiceBindingStatement(y),
        AlterRemoteServiceBindingStatement y=>this.AlterRemoteServiceBindingStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CreateRemoteServiceBindingStatement(CreateRemoteServiceBindingStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterRemoteServiceBindingStatement(AlterRemoteServiceBindingStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ResourcePoolStatement(ResourcePoolStatement x)=>x switch{
        CreateResourcePoolStatement y=>this.CreateResourcePoolStatement(y),
        AlterResourcePoolStatement y=>this.AlterResourcePoolStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CreateResourcePoolStatement(CreateResourcePoolStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterResourcePoolStatement(AlterResourcePoolStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression RestoreStatement(RestoreStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression ReturnStatement(ReturnStatement x){
        //if(this.ReturnLabel is null){
        //    //return this.ScalarExpression(x.Expression);
        //    var Goto=e.Expression.Goto(this.ReturnLabel);
        //    if(x.Expression is null)return expression;
        //    var ScalarExpression=this.ScalarExpression(x.Expression);
        //    ScalarExpression=e.Expression.Assign(this.変数CreateFunctionStatement.ReturnVariable,ScalarExpression);
        //    if(this.変数CreateFunctionStatement.ReturnVariable is not null)
        //        ScalarExpression=e.Expression.Assign(this.変数CreateFunctionStatement.ReturnVariable,ScalarExpression);
        //    return e.Expression.Block(ScalarExpression,Goto);
        //}
        if(x.Expression is null){
            return e.Expression.Return(this.ReturnLabel,Constant_0);
        }else{
            var ScalarExpression=this.ScalarExpression(x.Expression);
            var ReturnLabel=this.ReturnLabel;
            Debug.Assert(ReturnLabel is not null);
            ScalarExpression=this.Convertデータ型を合わせるNullableは想定する(ScalarExpression,ReturnLabel.Type);
            var Return=e.Expression.Return(ReturnLabel,ScalarExpression);
            return Return;
            //ScalarExpression=e.Expression.Assign(this.変数CreateFunctionStatement.ReturnVariable,ScalarExpression);
            //if(this.変数CreateFunctionStatement.ReturnVariable is not null) ScalarExpression=e.Expression.Assign(this.変数CreateFunctionStatement.ReturnVariable,ScalarExpression);
            //return e.Expression.Block(ScalarExpression,Goto);
        }
    }
    private e.Expression RevertStatement(RevertStatement x){
        //execute as callerの権限を戻す
        return Default_void;
    }
    private e.Expression RoleStatement(RoleStatement x)=>x switch{
        CreateRoleStatement y=>this.CreateRoleStatement(y),
        AlterRoleStatement y=>this.AlterRoleStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CreateRoleStatement(CreateRoleStatement x)=>x switch{
        CreateServerRoleStatement y=>this.CreateServerRoleStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CreateServerRoleStatement(CreateServerRoleStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterRoleStatement(AlterRoleStatement x)=>x switch{
        AlterServerRoleStatement y=>this.AlterServerRoleStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression AlterServerRoleStatement(AlterServerRoleStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression RouteStatement(RouteStatement x)=>x switch{
        CreateRouteStatement y=>this.CreateRouteStatement(y),
        AlterRouteStatement y=>this.AlterRouteStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CreateRouteStatement(CreateRouteStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterRouteStatement(AlterRouteStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression SecurityPolicyStatement(SecurityPolicyStatement x)=>x switch{
        CreateSecurityPolicyStatement y=>this.CreateSecurityPolicyStatement(y),
        AlterSecurityPolicyStatement y=>this.AlterSecurityPolicyStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression SecurityStatement(SecurityStatement x)=>x switch{
        GrantStatement y=>this.GrantStatement(y),
        DenyStatement y=>this.DenyStatement(y),
        RevokeStatement y=>this.RevokeStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression GrantStatement(GrantStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression DenyStatement(DenyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression RevokeStatement(RevokeStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression SecurityStatementBody80(SecurityStatementBody80 x)=>x switch{
        GrantStatement80 y=>this.GrantStatement80(y),
        DenyStatement80 y=>this.DenyStatement80(y),
        RevokeStatement80 y=>this.RevokeStatement80(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression SendStatement(SendStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression SequenceStatement(SequenceStatement x)=>x switch{
        CreateSequenceStatement y=>this.CreateSequenceStatement(y),
        AlterSequenceStatement y=>this.AlterSequenceStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression ServerAuditStatement(ServerAuditStatement x)=>x switch{
        CreateServerAuditStatement y=>this.CreateServerAuditStatement(y),
        AlterServerAuditStatement y=>this.AlterServerAuditStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression SetCommandStatement(SetCommandStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression SetErrorLevelStatement(SetErrorLevelStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression SetOnOffStatement(SetOnOffStatement x)=>x switch{
        PredicateSetStatement y=>this.PredicateSetStatement(y),
        SetStatisticsStatement y=>this.SetStatisticsStatement(y),
        SetOffsetsStatement y=>this.SetOffsetsStatement(y),
        SetIdentityInsertStatement y=>this.SetIdentityInsertStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression SetRowCountStatement(SetRowCountStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression SetTextSizeStatement(SetTextSizeStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression SetTransactionIsolationLevelStatement(SetTransactionIsolationLevelStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression SetUserStatement(SetUserStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression SetVariableStatement(SetVariableStatement x){
        Debug.Assert(!x.FunctionCallExists);
        Debug.Assert(x.CursorDefinition is null);
        Debug.Assert(x.Identifier is null);
        var NodeType=x.AssignmentKind switch{
            AssignmentKind.AddEquals=>e.ExpressionType.AddAssign,
            AssignmentKind.BitwiseAndEquals=>e.ExpressionType.AndAssign,
            AssignmentKind.BitwiseOrEquals=>e.ExpressionType.OrAssign,
            AssignmentKind.BitwiseXorEquals=>e.ExpressionType.ExclusiveOrAssign,
            AssignmentKind.DivideEquals=>e.ExpressionType.DivideAssign,
            AssignmentKind.Equals=>e.ExpressionType.Assign,
            AssignmentKind.ModEquals=>e.ExpressionType.ModuloAssign,
            AssignmentKind.MultiplyEquals=>e.ExpressionType.MultiplyAssign,
            AssignmentKind.SubtractEquals=>e.ExpressionType.SubtractAssign,
            _=>throw new NotSupportedException(x.AssignmentKind.ToString())
        };
        var Left=this.VariableReference(x.Variable);
        var Right=this.ScalarExpression(x.Expression);
        if(Right.Type.IsArray) {
        }else if(Right.Type==typeof(string)) {
        } else { 
            var IEnumerable1 = Right.Type.GetInterface(CommonLibrary.Generic_IEnumerable1_FullName);
            if(IEnumerable1 is not null) {
                //var SingleOrDefault=e.Expression.Call(Reflection.ExtensionSet.SingleOrDefault.MakeGenericMethod(IEnumerable1.GetGenericArguments()),Right);
                //return e.Expression.Call(作業配列.MakeGenericMethod(Reflection.ExtensionSet.SingleOrDefault,Lambda_Body_Type),MethodCall);

                //Debug.Assert(typeof(ITuple).IsAssignableFrom(SingleOrDefault.Type));
                //Right=e.Expression.Field(SingleOrDefault,"Item1");
                Right=e.Expression.Call(Reflection.ExtensionSet.SingleOrDefault.MakeGenericMethod(IEnumerable1.GetGenericArguments()),Right);
            }
        }
        Right=this.Convertデータ型を合わせるNullableは想定する(Right,Left.Type);
        return e.Expression.MakeBinary(NodeType,Left,Right);
    }
    private e.Expression ShutdownStatement(ShutdownStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression StatementWithCtesAndXmlNamespaces(StatementWithCtesAndXmlNamespaces x)=>x switch{
        SelectStatement y=>this.SelectStatement(y),
        DataModificationStatement y=>this.DataModificationStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression SelectStatement(SelectStatement x){
        switch(x){
            case SelectStatementSnippet y:return this.SelectStatementSnippet(y);
            default:{
                //this.Statementの共通初期化();
                var List_Expression=new List<e.Expression>();
                if(x.WithCtesAndXmlNamespaces is not null) { 
                    var WithCtesAndXmlNamespaces=this.WithCtesAndXmlNamespaces(x.WithCtesAndXmlNamespaces);
                    List_Expression.Add(WithCtesAndXmlNamespaces);
                }
                var Variables=this.Variables.ToArray();//サブクエリで書き換えられる可能性
                //compute by句
                foreach(var ComputeClause in x.ComputeClauses){
                    this.ComputeClause(ComputeClause);
                }
                if(x.Into is not null)this.SchemaObjectName(x.Into);
                if(x.On is not null)this.Identifier(x.On);
                foreach(var OptimizerHint in x.OptimizerHints){
                    this.OptimizerHint(OptimizerHint);
                }
                var QueryExpression=this.QueryExpression(x.QueryExpression);
                if(List_Expression.Count==0)return QueryExpression;
                List_Expression.Add(QueryExpression);
                return e.Expression.Block(Variables,List_Expression);
            }
        }
    }
    private e.Expression SelectStatementSnippet(SelectStatementSnippet x){
        //x.Script
        return this.QueryExpression(x.QueryExpression);
    }
    private e.Expression SensitivityClassificationStatement(SensitivityClassificationStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression SymmetricKeyStatement(SymmetricKeyStatement x)=>x switch{
        CreateSymmetricKeyStatement y=>this.CreateSymmetricKeyStatement(y),
        AlterSymmetricKeyStatement y=>this.AlterSymmetricKeyStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CreateSymmetricKeyStatement(CreateSymmetricKeyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterSymmetricKeyStatement(AlterSymmetricKeyStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression TextModificationStatement(TextModificationStatement x)=>x switch{
        UpdateTextStatement y=>this.UpdateTextStatement(y),
        WriteTextStatement y=>this.WriteTextStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression UpdateTextStatement(UpdateTextStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression WriteTextStatement(WriteTextStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ThrowStatement(ThrowStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression TransactionStatement(TransactionStatement x)=>x switch{
        BeginTransactionStatement y=>this.BeginTransactionStatement(y),
        CommitTransactionStatement y=>this.CommitTransactionStatement(y),
        RollbackTransactionStatement y=>this.RollbackTransactionStatement(y),
        SaveTransactionStatement y=>this.SaveTransactionStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression TriggerStatementBody(TriggerStatementBody x)=>x switch{
        AlterTriggerStatement y=>this.AlterTriggerStatement(y),
        CreateTriggerStatement y=>this.CreateTriggerStatement(y),
        CreateOrAlterTriggerStatement y=>this.CreateOrAlterTriggerStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression AlterTriggerStatement(AlterTriggerStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateTriggerStatement(CreateTriggerStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CreateOrAlterTriggerStatement(CreateOrAlterTriggerStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression TruncateTableStatement(TruncateTableStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression TryCatchStatement(TryCatchStatement x){
        var Statements=this.StatementList(x.TryStatements);
        var CatchStatements=this.StatementList(x.CatchStatements);
        if(Statements.Type!=typeof(void)) Statements=e.Expression.Block(typeof(void),Statements);
        return e.Expression.TryCatch(
            Statements,
            this.作業配列.CatchBlocks設定(
                e.Expression.Catch(
                    typeof(System.Exception),
                    CatchStatements
                )
            )
        );
    }
    private e.Expression TSqlStatementSnippet(TSqlStatementSnippet x){throw this.単純NotSupportedException(x);}
    private e.Expression UpdateStatisticsStatement(UpdateStatisticsStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression UseFederationStatement(UseFederationStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression UseStatement(UseStatement x)=>Default_void;
    private e.Expression UserStatement(UserStatement x)=>x switch{
        CreateUserStatement y=>this.CreateUserStatement(y),
        AlterUserStatement y=>this.AlterUserStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CreateUserStatement(CreateUserStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterUserStatement(AlterUserStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ViewStatementBody(ViewStatementBody x)=>x switch{
        AlterViewStatement y=>this.AlterViewStatement(y),
        CreateViewStatement y=> this.CreateViewStatement(y),
        CreateOrAlterViewStatement y=>this.CreateOrAlterViewStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression WaitForStatement(WaitForStatement x){throw this.単純NotSupportedException(x);}
    private e.Expression WaitForSupportedStatement(WaitForSupportedStatement x)=>x switch{
        GetConversationGroupStatement y=>this.GetConversationGroupStatement(y),
        ReceiveStatement y=>this.ReceiveStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression SignatureStatementBase(SignatureStatementBase x)=>x switch{
        AddSignatureStatement y=>this.AddSignatureStatement(y),
        DropSignatureStatement y=>this.DropSignatureStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression WhileStatement(WhileStatement x){
        var Break=e.Expression.Label();
        var Stack_WHILEのBreak先=this.Stack_WHILEのBreak先;
        Stack_WHILEのBreak先.Push(Break);
        var Predicate=this.BooleanExpression(x.Predicate);
        var Body=this.TSqlStatement(x.Statement);
        Stack_WHILEのBreak先.Pop();
        return e.Expression.Loop(
            e.Expression.Block(
                e.Expression.IfThenElse(
                    Predicate,
                    e.Expression.Break(Break),
                    Default_void
                ),
                Body
            ),
            Break
        );
    }
    private e.Expression WorkloadGroupStatement(WorkloadGroupStatement x)=>x switch{
        CreateWorkloadGroupStatement y=>this.CreateWorkloadGroupStatement(y),
        AlterWorkloadGroupStatement y=>this.AlterWorkloadGroupStatement(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CreateWorkloadGroupStatement(CreateWorkloadGroupStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression AlterWorkloadGroupStatement(AlterWorkloadGroupStatement x){
        throw this.単純NotSupportedException(x);
    }
}
