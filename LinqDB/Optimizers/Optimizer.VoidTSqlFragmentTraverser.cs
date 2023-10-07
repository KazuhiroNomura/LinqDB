using System;
using System.Diagnostics;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.Collections.Generic;
using AssemblyName = Microsoft.SqlServer.TransactSql.ScriptDom.AssemblyName;
//using Expression = System.Linq.Expressions.Expression;
//using BinaryExpression = System.Linq.Expressions.BinaryExpression;
//using MemberExpression = System.Linq.Expressions.MemberExpression;
//using MethodCallExpression = System.Linq.Expressions.MethodCallExpression;
//using Expressions.ExpressionType = System.Linq.Expressions.Expressions.ExpressionType;
//using ParameterExpression = System.Linq.Expressions.ParameterExpression;
//using Expressions.LambdaExpression = System.Linq.Expressions.Expressions.LambdaExpression;
namespace LinqDB.Optimizers;

/// <summary>
/// Expressionを最適化する
/// </summary>
public sealed partial class Optimizer {
    /// <summary>
    /// TSQLからLINQに変換する。
    /// </summary>
    public abstract class VoidTSqlFragmentTraverser {
        protected readonly Sql160ScriptGenerator ScriptGenerator;
        public string SQL取得(TSqlFragment TSqlFragment) {
            this.ScriptGenerator.GenerateScript(TSqlFragment,out var SQL);
            return SQL;
        }
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        protected VoidTSqlFragmentTraverser(Sql160ScriptGenerator ScriptGenerator) {
            this.ScriptGenerator=ScriptGenerator;
        }
        //protected virtual void TableReferences(IList<TableReference> x) {
        //    foreach(var a in x) this.TableReference(a);
        //}
        private void ValueExpressions(IList<ValueExpression> x) {
            foreach(var a in x)this.ValueExpression(a);
        }
        private void ExecuteParameters(IList<ExecuteParameter> x) {
            foreach(var a in x)this.ExecuteParameter(a);
        }
        private void AlterServerConfigurationBufferPoolExtensionOptions(IList<AlterServerConfigurationBufferPoolExtensionOption> x) {
            foreach(var a in x)this.AlterServerConfigurationBufferPoolExtensionOption(a);
        }
        private void EventSessionObjectNames(IList<EventSessionObjectName> x) {
            foreach(var a in x)this.EventSessionObjectName(a);
        }
        private void EventDeclarations(IList<EventDeclaration> x) {
            foreach(var a in x)this.EventDeclaration(a);
        }
        private void SessionOptions(IList<SessionOption> x) {
            foreach(var a in x)this.SessionOption(a);
        }
        private void BrokerPriorityParameters(IList<BrokerPriorityParameter> x) {
            foreach(var a in x)this.BrokerPriorityParameter(a);
        }
        private void ExternalResourcePoolParameters(IList<ExternalResourcePoolParameter> x) {
            foreach(var a in x)this.ExternalResourcePoolParameter(a);
        }
        private void FullTextCatalogOptions(IList<FullTextCatalogOption> x) {
            foreach(var a in x)this.FullTextCatalogOption(a);
        }
        private void KeyOptions(IList<KeyOption> x) {
            foreach(var a in x)this.KeyOption(a);
        }
        private void CryptoMechanisms(IList<CryptoMechanism> x) {
            foreach(var a in x)this.CryptoMechanism(a);
        }
        private void RemoteServiceBindingOptions(IList<RemoteServiceBindingOption> x) {
            foreach(var a in x)this.RemoteServiceBindingOption(a);
        }
        private void FullTextIndexColumns(IList<FullTextIndexColumn> x) {
            foreach(var a in x)this.FullTextIndexColumn(a);
        }
        private void ExternalFileFormatOptions(IList<ExternalFileFormatOption> x) {
            foreach(var a in x)this.ExternalFileFormatOption(a);
        }
        private void ColumnEncryptionKeyValues(IList<ColumnEncryptionKeyValue> x) {
            foreach(var a in x)this.ColumnEncryptionKeyValue(a);
        }
        private void SchemaObjectNames(IList<SchemaObjectName> x) {
            foreach(var a in x)this.SchemaObjectName(a);
        }
        private void Permissions(IList<Permission> x) {
            foreach(var a in x)this.Permission(a);
        }
        private void IdentifierOrValueExpressions(IList<IdentifierOrValueExpression> x) {
            foreach(var a in x)this.IdentifierOrValueExpression(a);
        }
        private void BulkInsertOptions(IList<BulkInsertOption> x) {
            foreach(var a in x)this.BulkInsertOption(a);
        }
        private void MirrorToClauses(IList<MirrorToClause> x) {
            foreach(var a in x)this.MirrorToClause(a);
        }
        private void BackupOptions(IList<BackupOption> x) {
            foreach(var a in x)this.BackupOption(a);
        }
        private void DeviceInfos(IList<DeviceInfo> x) {
            foreach(var a in x)this.DeviceInfo(a);
        }
        private void ConstraintDefinitions(IList<ConstraintDefinition> x) {
            foreach(var a in x)this.ConstraintDefinition(a);
        }
        private void CreateDatabaseStatements(IList<DatabaseOption> x) {
            foreach(var a in x)this.DatabaseOption(a);
        }
        private void FileDeclarations(IList<FileDeclaration> x) {
            foreach(var a in x)this.FileDeclaration(a);
        }
        protected void AuditSpecificationParts(IList<AuditSpecificationPart> x) {
            foreach(var a in x)this.AuditSpecificationPart(a);
        }
        protected void CompressionPartitionRanges(IList<CompressionPartitionRange> x) {
            foreach(var a in x)this.CompressionPartitionRange(a);
        }
        protected void SecurityPrincipals(IList<SecurityPrincipal> x) {
            foreach(var a in x)this.SecurityPrincipal(a);
        }
        protected void EndpointProtocolOptions(IList<EndpointProtocolOption> x) {
            foreach(var a in x)this.EndpointProtocolOption(a);
        }
        protected void PayloadOptions(IList<PayloadOption> x) {
            foreach(var a in x)this.PayloadOption(a);
        }
        protected void ResourcePoolParameters(IList<ResourcePoolParameter> x) {
            foreach(var a in x)this.ResourcePoolParameter(a);
        }
        protected void SchemaDeclarationItems(IList<SchemaDeclarationItem> x) {
            foreach(var a in x)this.SchemaDeclarationItem(a);
        }
        protected void SecurityPolicyOptions(IList<SecurityPolicyOption> x) {
            foreach(var a in x)this.SecurityPolicyOption(a);
        }
        protected void SecurityPredicateActions(IList<SecurityPredicateAction> x) {
            foreach(var a in x)this.SecurityPredicateAction(a);
        }
        protected void StatisticsOptions(IList<StatisticsOption> x) {
            foreach(var a in x)this.StatisticsOption(a);
        }
        protected void TargetDeclarations(IList<TargetDeclaration> x) {
            foreach(var a in x)this.TargetDeclaration(a);
        }
        protected void WorkloadGroupParameters(IList<WorkloadGroupParameter> x) {
            foreach(var a in x)this.WorkloadGroupParameter(a);
        }
        protected void ProcedureOptions(IList<ProcedureOption> x) {
            foreach(var a in x)this.ProcedureOption(a);
        }
        protected void TriggerActions(IList<TriggerAction> x) {
            foreach(var a in x)this.TriggerAction(a);
        }
        protected void ProcedureParameters(IList<ProcedureParameter> x) {
            foreach(var a in x)this.ProcedureParameter(a);
        }
        protected void FunctionOptions(IList<FunctionOption> x) {
            foreach(var a in x)this.FunctionOption(a);
        }
        protected void SequenceOptions(IList<SequenceOption> x) {
            foreach(var a in x)this.SequenceOption(a);
        }
        protected void ExternalDataSourceOptions(IList<ExternalDataSourceOption> x) {
            foreach(var a in x)this.ExternalDataSourceOption(a);
        }
        protected void AssemblyOptions(IList<AssemblyOption> x) {
            foreach(var a in x)this.AssemblyOption(a);
        }
        protected void TriggerNames(IList<SchemaObjectName> x) {
            foreach(var a in x)this.SchemaObjectName(a);
        }

        protected void TableOptions(IList<TableOption> x) {
            foreach(var a in x)this.TableOption(a);
        }
        protected void RouteOptions(IList<RouteOption> x) {
            foreach(var a in x)this.RouteOption(a);
        }
        protected void QueueOptions(IList<QueueOption> x) {
            foreach(var a in x)this.QueueOption(a);
        }
        protected void PrincipalOptions(IList<PrincipalOption> x) {
            foreach(var a in x)this.PrincipalOption(a);
        }
        protected void Identifiers(IList<Identifier> x) {
            foreach(var a in x)this.SwitchIdentifier(a);
        }
        protected void Statements(IList<TSqlStatement> x) {
            foreach(var a in x)this.TSqlStatement(a);
        }
        protected void LowPriorityLockWaitOptions(IList<LowPriorityLockWaitOption> x) {
            foreach(var a in x)this.LowPriorityLockWaitOption(a);
        }
        protected void ScalarExpressions(IList<ScalarExpression>x) {
            foreach(var a in x)this.ScalarExpression(a);
        }
        protected void PoolAffinityRanges(IList<LiteralRange> x) {
            foreach(var a in x)this.LiteralRange(a);
        }
        protected void ColumnReferenceExpressions(IList<ColumnReferenceExpression> x) {
            foreach(var a in x)this.ColumnReferenceExpression(a);
        }
        protected void SelectElements(IList<SelectElement> x) {
            foreach(var a in x)this.SelectElement(a);
        }
        protected void GroupingSpecifications(IList<GroupingSpecification> x) {
            foreach(var a in x)this.GroupingSpecification(a);
        }
        protected void ColumnWithSortOrders(IList<ColumnWithSortOrder> x) {
            foreach(var a in x)this.ColumnWithSortOrder(a);
        }
        protected void IndexOptions(IList<IndexOption> x) {
            foreach(var a in x)this.IndexOption(a);
        }
        protected void PromotedPaths(IList<SelectiveXmlIndexPromotedPath> x) {
            foreach(var a in x)this.SelectiveXmlIndexPromotedPath(a);
        }

        protected void RowValues(IList<RowValue> x) {
            foreach(var a in x)this.RowValue(a);
        }
        protected void Literals(IList<Literal> x) {
            foreach(var a in x)this.Literal(a);

        }
        protected void TableHints(IList<TableHint> x) {
            foreach(var a in x)this.TableHint(a);
        }
        protected void ViewOptions(IList<ViewOption> x) {
            foreach(var a in x)this.ViewOption(a);
        }
        protected void SetClauses(IList<SetClause> x) {
            foreach(var a in x)this.SetClause(a);
        }
        protected void OptimizerHints(IList<OptimizerHint> x) {
            foreach(var a in x)this.OptimizerHint(a);
        }
        protected void ComputeClauses(IList<ComputeClause>x) {
            foreach(var a in x)this.ComputeClause(a);
        }
        /// <summary>
        /// 起点
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TSqlFragment(TSqlFragment x) {
            switch(x) {
                case MultiPartIdentifier                                   y:this.SwitchMultiPartIdentifier                             (y);break;
                case Identifier                                            y:this.SwitchIdentifier                                      (y);break;
                case ScalarExpression                                      y:this.ScalarExpression                                      (y);break;
                case IdentifierOrValueExpression                           y:this.IdentifierOrValueExpression                           (y);break;
                case StatementList                                         y:this.StatementList                                         (y);break;
                case TSqlStatement                                         y:this.TSqlStatement                                         (y);break;
                case ExecuteOption                                         y:this.ExecuteOption                                         (y);break;
                case ResultSetDefinition                                   y:this.ResultSetDefinition                                   (y);break;
                case ResultColumnDefinition                                y:this.ResultColumnDefinition                                (y);break;
                case ExecuteSpecification                                  y:this.ExecuteSpecification                                  (y);break;
                case ExecuteContext                                        y:this.ExecuteContext                                        (y);break;
                case ExecuteParameter                                      y:this.ExecuteParameter                                      (y);break;
                case ExecutableEntity                                      y:this.ExecutableEntity                                      (y);break;
                case ProcedureReferenceName                                y:this.ProcedureReferenceName                                (y);break;
                case AdHocDataSource                                       y:this.AdHocDataSource                                       (y);break;
                case ViewOption                                            y:this.ViewOption                                            (y);break;
                case TriggerObject                                         y:this.TriggerObject                                         (y);break;
                case TriggerOption                                         y:this.TriggerOption                                         (y);break;
                case TriggerAction                                         y:this.TriggerAction                                         (y);break;
                case ProcedureReference                                    y:this.ProcedureReference                                    (y);break;
                case MethodSpecifier                                       y:this.MethodSpecifier                                       (y);break;
                case ProcedureOption                                       y:this.ProcedureOption                                       (y);break;
                case FunctionOption                                        y:this.FunctionOption                                        (y);break;
                case XmlNamespaces                                         y:this.XmlNamespaces                                         (y);break;
                case XmlNamespacesElement                                  y:this.XmlNamespacesElement                                  (y);break;
                case CommonTableExpression                                 y:this.CommonTableExpression                                 (y);break;
                case WithCtesAndXmlNamespaces                              y:this.WithCtesAndXmlNamespaces                              (y);break;
                case FunctionReturnType                                    y:this.FunctionReturnType                                    (y);break;
                case DataTypeReference                                     y:this.DataTypeReference                                     (y);break;
                case TableDefinition                                       y:this.TableDefinition                                       (y);break;
                case DeclareTableVariableBody                              y:this.DeclareTableVariableBody                              (y);break;
                case TableReference                                        y:this.TableReference                                        (y);break;
                case TableHint                                             y:this.TableHint                                             (y);break;
                case BooleanExpression                                     y:this.BooleanExpression                                     (y);break;
                case ForClause                                             y:this.ForClause                                             (y);break;
                case OptimizerHint                                         y:this.OptimizerHint                                         (y);break;
                case VariableValuePair                                     y:this.VariableValuePair                                     (y);break;
                case WhenClause                                            y:this.WhenClause                                            (y);break;
                case SchemaDeclarationItem                                 y:this.SchemaDeclarationItem                                 (y);break;
                case CallTarget                                            y:this.CallTarget                                            (y);break;
                case OverClause                                            y:this.OverClause                                            (y);break;
                case AtomicBlockOption                                     y:this.AtomicBlockOption                                     (y);break;
                case ColumnWithSortOrder                                   y:this.ColumnWithSortOrder                                   (y);break;
                case DeclareVariableElement                                y:this.DeclareVariableElement                                (y);break;
                case DataModificationSpecification                         y:this.DataModificationSpecification                         (y);break;
                case Permission                                            y:this.Permission                                            (y);break;
                case SecurityTargetObject                                  y:this.SecurityTargetObject                                  (y);break;
                case SecurityTargetObjectName                              y:this.SecurityTargetObjectName                              (y);break;
                case SecurityPrincipal                                     y:this.SecurityPrincipal                                     (y);break;
                case SecurityElement80                                     y:this.SecurityElement80                                     (y);break;
                case Privilege80                                           y:this.Privilege80                                           (y);break;
                case SecurityUserClause80                                  y:this.SecurityUserClause80                                  (y);break;
                case SetClause                                             y:this.SetClause                                             (y);break;
                case InsertSource                                          y:this.InsertSource                                          (y);break;
                case RowValue                                              y:this.RowValue                                              (y);break;
                case LiteralRange                                          y:this.LiteralRange                                          (y);break;
                case OptionValue                                           y:this.OptionValue                                           (y);break;
                case IdentifierOrScalarExpression                          y:this.IdentifierOrScalarExpression                          (y);break;
                case SchemaObjectNameOrValueExpression                     y:this.SchemaObjectNameOrValueExpression                     (y);break;
                case SequenceOption                                        y:this.SequenceOption                                        (y);break;
                case SecurityPredicateAction                               y:this.SecurityPredicateAction                               (y);break;
                case SecurityPolicyOption                                  y:this.SecurityPolicyOption                                  (y);break;
                case ColumnMasterKeyParameter                              y:this.ColumnMasterKeyParameter                              (y);break;
                case ColumnEncryptionKeyValue                              y:this.ColumnEncryptionKeyValue                              (y);break;
                case ColumnEncryptionKeyValueParameter                     y:this.ColumnEncryptionKeyValueParameter                     (y);break;
                case ExternalTableOption                                   y:this.ExternalTableOption                                   (y);break;
                case ExternalTableDistributionPolicy                       y:this.ExternalTableDistributionPolicy                       (y);break;
                case ExternalDataSourceOption                              y:this.ExternalDataSourceOption                              (y);break;
                case ExternalFileFormatOption                              y:this.ExternalFileFormatOption                              (y);break;
                case AssemblyOption                                        y:this.AssemblyOption                                        (y);break;
                case AddFileSpec                                           y:this.AddFileSpec                                           (y);break;
                case AssemblyName                                          y:this.AssemblyName                                          (y);break;
                case TableOption                                           y:this.TableOption                                           (y);break;
                case DatabaseOption                                        y:this.DatabaseOption                                        (y);break;
                case RemoteDataArchiveDatabaseSetting                      y:this.RemoteDataArchiveDatabaseSetting                      (y);break;
                case RetentionPeriodDefinition                             y:this.RetentionPeriodDefinition                             (y);break;
                case TableSwitchOption                                     y:this.TableSwitchOption                                     (y);break;
                case DropClusteredConstraintOption                         y:this.DropClusteredConstraintOption                         (y);break;
                case AlterTableDropTableElement                            y:this.AlterTableDropTableElement                            (y);break;
                case ExecuteAsClause                                       y:this.ExecuteAsClause                                       (y);break;
                case QueueOption                                           y:this.QueueOption                                           (y);break;
                case RouteOption                                           y:this.RouteOption                                           (y);break;
                case SystemTimePeriodDefinition                            y:this.SystemTimePeriodDefinition                            (y);break;
                case IndexType                                             y:this.IndexType                                             (y);break;
                case PartitionSpecifier                                    y:this.PartitionSpecifier                                    (y);break;
                case FileGroupOrPartitionScheme                            y:this.FileGroupOrPartitionScheme                            (y);break;
                case IndexOption                                           y:this.IndexOption                                           (y);break;
                case OnlineIndexLowPriorityLockWaitOption                  y:this.OnlineIndexLowPriorityLockWaitOption                  (y);break;
                case LowPriorityLockWaitOption                             y:this.LowPriorityLockWaitOption                             (y);break;
                case FullTextIndexColumn                                   y:this.FullTextIndexColumn                                   (y);break;
                case FullTextIndexOption                                   y:this.FullTextIndexOption                                   (y);break;
                case FullTextCatalogAndFileGroup                           y:this.FullTextCatalogAndFileGroup                           (y);break;
                case EventTypeGroupContainer                               y:this.EventTypeGroupContainer                               (y);break;
                case EventNotificationObjectScope                          y:this.EventNotificationObjectScope                          (y);break;
                case ApplicationRoleOption                                 y:this.ApplicationRoleOption                                 (y);break;
                case AlterRoleAction                                       y:this.AlterRoleAction                                       (y);break;
                case UserLoginOption                                       y:this.UserLoginOption                                       (y);break;
                case StatisticsOption                                      y:this.StatisticsOption                                      (y);break;
                case StatisticsPartitionRange                              y:this.StatisticsPartitionRange                              (y);break;
                case CursorDefinition                                      y:this.CursorDefinition                                      (y);break;
                case CursorOption                                          y:this.CursorOption                                          (y);break;
                case CursorId                                              y:this.CursorId                                              (y);break;
                case CryptoMechanism                                       y:this.CryptoMechanism                                       (y);break;
                case FetchType                                             y:this.FetchType                                             (y);break;
                case WhereClause                                           y:this.WhereClause                                           (y);break;
                case DropIndexClauseBase                                   y:this.DropIndexClauseBase                                   (y);break;
                case SetCommand                                            y:this.SetCommand                                            (y);break;
                case FileDeclaration                                       y:this.FileDeclaration                                       (y);break;
                case FileDeclarationOption                                 y:this.FileDeclarationOption                                 (y);break;
                case FileGroupDefinition                                   y:this.FileGroupDefinition                                   (y);break;
                case DatabaseConfigurationClearOption                      y:this.DatabaseConfigurationClearOption                      (y);break;
                case DatabaseConfigurationSetOption                        y:this.DatabaseConfigurationSetOption                        (y);break;
                case AlterDatabaseTermination                              y:this.AlterDatabaseTermination                              (y);break;
                case ChangeTrackingOptionDetail                            y:this.ChangeTrackingOptionDetail                            (y);break;
                case QueryStoreOption                                      y:this.QueryStoreOption                                      (y);break;
                case AutomaticTuningOption                                 y:this.AutomaticTuningOption                                 (y);break;
                case ColumnDefinitionBase                                  y:this.ColumnDefinitionBase                                  (y);break;
                case ColumnEncryptionDefinition                            y:this.ColumnEncryptionDefinition                            (y);break;
                case ColumnEncryptionDefinitionParameter                   y:this.ColumnEncryptionDefinitionParameter                   (y);break;
                case IdentityOptions                                       y:this.IdentityOptions                                       (y);break;
                case ColumnStorageOptions                                  y:this.ColumnStorageOptions                                  (y);break;
                case ConstraintDefinition                                  y:this.ConstraintDefinition                                  (y);break;
                case FederationScheme                                      y:this.FederationScheme                                      (y);break;
                case TableDistributionPolicy                               y:this.TableDistributionPolicy                               (y);break;
                case TableIndexType                                        y:this.TableIndexType                                        (y);break;
                case PartitionSpecifications                               y:this.PartitionSpecifications                               (y);break;
                case CompressionPartitionRange                             y:this.CompressionPartitionRange                             (y);break;
                case GraphConnectionBetweenNodes                           y:this.GraphConnectionBetweenNodes                           (y);break;
                case RestoreOption                                         y:this.RestoreOption                                         (y);break;
                case BackupOption                                          y:this.BackupOption                                          (y);break;
                case DeviceInfo                                            y:this.DeviceInfo                                            (y);break;
                case MirrorToClause                                        y:this.MirrorToClause                                        (y);break;
                case BackupRestoreFileInfo                                 y:this.BackupRestoreFileInfo                                 (y);break;
                case BulkInsertOption                                      y:this.BulkInsertOption                                      (y);break;
                case ExternalTableColumnDefinition                         y:this.ExternalTableColumnDefinition                         (y);break;
                case InsertBulkColumnDefinition                            y:this.InsertBulkColumnDefinition                            (y);break;
                case DbccOption                                            y:this.DbccOption                                            (y);break;
                case DbccNamedLiteral                                      y:this.DbccNamedLiteral                                      (y);break;
                case PartitionParameterType                                y:this.PartitionParameterType                                (y);break;
                case RemoteServiceBindingOption                            y:this.RemoteServiceBindingOption                            (y);break;
                case EncryptionSource                                      y:this.EncryptionSource                                      (y);break;
                case CertificateOption                                     y:this.CertificateOption                                     (y);break;
                case ContractMessage                                       y:this.ContractMessage                                       (y);break;
                case EndpointAffinity                                      y:this.EndpointAffinity                                      (y);break;
                case EndpointProtocolOption                                y:this.EndpointProtocolOption                                (y);break;
                case IPv4                                                  y:this.IPv4                                                  (y);break;
                case PayloadOption                                         y:this.PayloadOption                                         (y);break;
                case KeyOption                                             y:this.KeyOption                                             (y);break;
                case FullTextCatalogOption                                 y:this.FullTextCatalogOption                                 (y);break;
                case ServiceContract                                       y:this.ServiceContract                                       (y);break;
                case ComputeClause                                         y:this.ComputeClause                                         (y);break;
                case ComputeFunction                                       y:this.ComputeFunction                                       (y);break;
                case TableSampleClause                                     y:this.TableSampleClause                                     (y);break;
                case ExpressionWithSortOrder                               y:this.ExpressionWithSortOrder                               (y);break;
                case GroupByClause                                         y:this.GroupByClause                                         (y);break;
                case GroupingSpecification                                 y:this.GroupingSpecification                                 (y);break;
                case OutputClause                                          y:this.OutputClause                                          (y);break;
                case OutputIntoClause                                      y:this.OutputIntoClause                                      (y);break;
                case HavingClause                                          y:this.HavingClause                                          (y);break;
                case OrderByClause                                         y:this.OrderByClause                                         (y);break;
                case QueryExpression                                       y:this.QueryExpression                                       (y);break;
                case FromClause                                            y:this.FromClause                                            (y);break;
                case SelectElement                                         y:this.SelectElement                                         (y);break;
                case TopRowFilter                                          y:this.TopRowFilter                                          (y);break;
                case OffsetClause                                          y:this.OffsetClause                                          (y);break;
                case AlterFullTextIndexAction                              y:this.AlterFullTextIndexAction                              (y);break;
                case SearchPropertyListAction                              y:this.SearchPropertyListAction                              (y);break;
                case CreateLoginSource                                     y:this.CreateLoginSource                                     (y);break;
                case PrincipalOption                                       y:this.PrincipalOption                                       (y);break;
                case DialogOption                                          y:this.DialogOption                                          (y);break;
                case TSqlFragmentSnippet                                   y:this.TSqlFragmentSnippet                                   (y);break;
                case TSqlScript                                            y:this.TSqlScript                                            (y);break;
                case TSqlBatch                                             y:this.TSqlBatch                                             (y);break;
                case MergeActionClause                                     y:this.MergeActionClause                                     (y);break;
                case MergeAction                                           y:this.MergeAction                                           (y);break;
                case AuditSpecificationPart                                y:this.AuditSpecificationPart                                (y);break;
                case AuditSpecificationDetail                              y:this.AuditSpecificationDetail                              (y);break;
                case DatabaseAuditAction                                   y:this.DatabaseAuditAction                                   (y);break;
                case AuditTarget                                           y:this.AuditTarget                                           (y);break;
                case AuditOption                                           y:this.AuditOption                                           (y);break;
                case AuditTargetOption                                     y:this.AuditTargetOption                                     (y);break;
                case ResourcePoolParameter                                 y:this.ResourcePoolParameter                                 (y);break;
                case ResourcePoolAffinitySpecification                     y:this.ResourcePoolAffinitySpecification                     (y);break;
                case ExternalResourcePoolParameter                         y:this.ExternalResourcePoolParameter                         (y);break;
                case ExternalResourcePoolAffinitySpecification             y:this.ExternalResourcePoolAffinitySpecification             (y);break;
                case WorkloadGroupParameter                                y:this.WorkloadGroupParameter                                (y);break;
                case BrokerPriorityParameter                               y:this.BrokerPriorityParameter                               (y);break;
                case FullTextStopListAction                                y:this.FullTextStopListAction                                (y);break;
                case EventSessionObjectName                                y:this.EventSessionObjectName                                (y);break;
                case EventDeclaration                                      y:this.EventDeclaration                                      (y);break;
                case EventDeclarationSetParameter                          y:this.EventDeclarationSetParameter                          (y);break;
                case TargetDeclaration                                     y:this.TargetDeclaration                                     (y);break;
                case SessionOption                                         y:this.SessionOption                                         (y);break;
                case SpatialIndexOption                                    y:this.SpatialIndexOption                                    (y);break;
                case BoundingBoxParameter                                  y:this.BoundingBoxParameter                                  (y);break;
                case GridParameter                                         y:this.GridParameter                                         (y);break;
                case AlterServerConfigurationBufferPoolExtensionOption     y:this.AlterServerConfigurationBufferPoolExtensionOption     (y);break;
                case AlterServerConfigurationDiagnosticsLogOption          y:this.AlterServerConfigurationDiagnosticsLogOption          (y);break;
                case AlterServerConfigurationFailoverClusterPropertyOption y:this.AlterServerConfigurationFailoverClusterPropertyOption (y);break;
                case AlterServerConfigurationHadrClusterOption             y:this.AlterServerConfigurationHadrClusterOption             (y);break;
                case AlterServerConfigurationSoftNumaOption                y:this.AlterServerConfigurationSoftNumaOption                (y);break;
                case AvailabilityReplica                                   y:this.AvailabilityReplica                                   (y);break;
                case AvailabilityReplicaOption                             y:this.AvailabilityReplicaOption                             (y);break;
                case AvailabilityGroupOption                               y:this.AvailabilityGroupOption                               (y);break;
                case AlterAvailabilityGroupAction                          y:this.AlterAvailabilityGroupAction                          (y);break;
                case AlterAvailabilityGroupFailoverOption                  y:this.AlterAvailabilityGroupFailoverOption                  (y);break;
                case DiskStatementOption                                   y:this.DiskStatementOption                                   (y);break;
                case WindowFrameClause                                     y:this.WindowFrameClause                                     (y);break;
                case WindowDelimiter                                       y:this.WindowDelimiter                                       (y);break;
                case WithinGroupClause                                     y:this.WithinGroupClause                                     (y);break;
                case SelectiveXmlIndexPromotedPath                         y:this.SelectiveXmlIndexPromotedPath                         (y);break;
                case TemporalClause                                        y:this.TemporalClause                                        (y);break;
            }
        }
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SwitchMultiPartIdentifier(MultiPartIdentifier x) {
            switch(x) {
                case SchemaObjectName y:this.SwitchSchemaObjectName(y);break;
                default: this.MultiPartIdentifier(x);break;
            }
        }
        /// <summary>
        /// MultiPartIdentifier:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SwitchSchemaObjectName(SchemaObjectName x) {
            switch(x) {
                case ChildObjectName y:this.ChildObjectName(y);break;
                case SchemaObjectNameSnippet y:this.SchemaObjectNameSnippet(y);break;
                default:this.SchemaObjectName(x);break;
            }
        }
        /// <summary>
        /// SchemaObjectName:MultiPartIdentifier:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ChildObjectName(ChildObjectName x) {
            this.Identifiers(x.Identifiers);
        }
        /// <summary>
        /// SchemaObjectName:MultiPartIdentifier:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SchemaObjectNameSnippet(SchemaObjectNameSnippet x) {
            this.SwitchIdentifier(x.BaseIdentifier);
            this.SwitchIdentifier(x.DatabaseIdentifier);
            this.SwitchIdentifier(x.SchemaIdentifier);
            this.SwitchIdentifier(x.ServerIdentifier);
            this.Identifiers(x.Identifiers);
        }
        protected virtual void SchemaObjectName(SchemaObjectName x) {
            this.Identifiers(x.Identifiers);
        }
        protected virtual void MultiPartIdentifier(MultiPartIdentifier x) {
            this.Identifiers(x.Identifiers);
        }
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SwitchIdentifier(Identifier x) {
            switch(x) {
                case SqlCommandIdentifier y: this.SqlCommandIdentifier(y);break;
                case IdentifierSnippet y: this.IdentifierSnippet(y);break;
                default: this.Identifier(x);break;
            }
        }
        /// <summary>
        /// Identifier
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SqlCommandIdentifier(SqlCommandIdentifier x) {
        }
        /// <summary>
        /// Identifier
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void IdentifierSnippet(IdentifierSnippet x) {
        }
        protected virtual void Identifier(Identifier x) {
            Debug.Assert(x is not null);
        }
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ScalarExpression(ScalarExpression x) {
            switch(x) {
                case PrimaryExpression y: this.PrimaryExpression(y);break;
                case ExtractFromExpression y: this.ExtractFromExpression(y);break;
                case OdbcConvertSpecification y: this.OdbcConvertSpecification(y);break;
                case BinaryExpression y: this.BinaryExpression(y);break;
                case IdentityFunctionCall y: this.IdentityFunctionCall(y);break;
                case UnaryExpression y: this.UnaryExpression(y);break;
                case ScalarExpressionSnippet y: this.ScalarExpressionSnippet(y);break;
                case SourceDeclaration y: this.SourceDeclaration(y);break;
            }
        }

        /// <summary>
        /// ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void PrimaryExpression(PrimaryExpression x) {
            switch(x) {
                case ValueExpression y: this.ValueExpression(y);break;
                case UserDefinedTypePropertyAccess y: this.UserDefinedTypePropertyAccess(y);break;
                case CaseExpression y: this.CaseExpression(y);break;
                case NullIfExpression y: this.NullIfExpression(y);break;
                case CoalesceExpression y: this.CoalesceExpression(y);break;
                case IIfCall y: this.IIfCall(y);break;
                case ConvertCall y: this.ConvertCall(y);break;
                case TryConvertCall y: this.TryConvertCall(y);break;
                case ParseCall y: this.ParseCall(y);break;
                case TryParseCall y: this.TryParseCall(y);break;
                case CastCall y: this.CastCall(y);break;
                case TryCastCall y: this.TryCastCall(y);break;
                case AtTimeZoneCall y: this.AtTimeZoneCall(y);break;
                case FunctionCall y: this.FunctionCall(y);break;
                case LeftFunctionCall y: this.LeftFunctionCall(y);break;
                case RightFunctionCall y: this.RightFunctionCall(y);break;
                case PartitionFunctionCall y: this.PartitionFunctionCall(y);break;
                case ParameterlessCall y: this.ParameterlessCall(y);break;
                case ScalarSubquery y: this.ScalarSubquery(y);break;
                case OdbcFunctionCall y: this.OdbcFunctionCall(y);break;
                case ParenthesisExpression y: this.ParenthesisExpression(y);break;
                case ColumnReferenceExpression y: this.ColumnReferenceExpression(y);break;
                case NextValueForExpression y: this.NextValueForExpression(y);break;
            }
        }
        /// <summary>
        /// PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ValueExpression(ValueExpression x) {
            switch(x) {
                case Literal y: this.Literal(y);break;
                case VariableReference y: this.VariableReference(y);break;
                case GlobalVariableExpression y: this.GlobalVariableExpression(y);break;
            }
        }
        /// <summary>
        /// ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void Literal(Literal x) {
            switch(x) {
                case IdentifierLiteral y: this.IdentifierLiteral(y);break;
                case IntegerLiteral y: this.IntegerLiteral(y);break;
                case NumericLiteral y: this.NumericLiteral(y);break;
                case RealLiteral y: this.RealLiteral(y);break;
                case MoneyLiteral y: this.MoneyLiteral(y);break;
                case BinaryLiteral y: this.BinaryLiteral(y);break;
                case StringLiteral y: this.StringLiteral(y);break;
                case NullLiteral y: this.NullLiteral(y);break;
                case DefaultLiteral y: this.DefaultLiteral(y);break;
                case MaxLiteral y: this.MaxLiteral(y);break;
                case OdbcLiteral y: this.OdbcLiteral(y);break;
            }
        }
        /// <summary>
        /// Literal:ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void IdentifierLiteral(IdentifierLiteral x) {
            if(x.Collation is not null)this.SwitchIdentifier(x.Collation);

        }
        /// <summary>
        /// Literal:ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void IntegerLiteral(IntegerLiteral x) {
            if(x.Collation is not null)this.SwitchIdentifier(x.Collation);
        }
        /// <summary>
        /// Literal:ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void NumericLiteral(NumericLiteral x) {
            if(x.Collation is not null) this.SwitchIdentifier(x.Collation);
        }
        /// <summary>
        /// Literal:ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RealLiteral(RealLiteral x) {
            if(x.Collation is not null) this.SwitchIdentifier(x.Collation);
        }
        /// <summary>
        /// Literal:ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MoneyLiteral(MoneyLiteral x) {
            if(x.Collation is not null) this.SwitchIdentifier(x.Collation);
        }
        /// <summary>
        /// Literal:ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BinaryLiteral(BinaryLiteral x) {
            if(x.Collation is not null) this.SwitchIdentifier(x.Collation);
        }
        /// <summary>
        /// Literal:ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void StringLiteral(StringLiteral x) {
            if(x.Collation is not null) this.SwitchIdentifier(x.Collation);
        }
        /// <summary>
        /// Literal:ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void NullLiteral(NullLiteral x) {
            if(x.Collation is not null) this.SwitchIdentifier(x.Collation);
        }
        /// <summary>
        /// Literal:ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DefaultLiteral(DefaultLiteral x) {
            if(x.Collation is not null) this.SwitchIdentifier(x.Collation);
        }
        /// <summary>
        /// Literal:ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MaxLiteral(MaxLiteral x) {
            if(x.Collation is not null)this.SwitchIdentifier(x.Collation);
        }
        /// <summary>
        /// Literal:ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OdbcLiteral(OdbcLiteral x) {
            if(x.Collation is not null)this.SwitchIdentifier(x.Collation);
        }
        /// <summary>
        /// VariableReference:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void VariableReference(VariableReference x) {
            if(x.Collation is not null)this.SwitchIdentifier(x.Collation);
        }
        /// <summary>
        /// VariableReference:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void GlobalVariableExpression(GlobalVariableExpression x) {
            if(x.Collation is not null)this.SwitchIdentifier(x.Collation);
        }
        /// <summary>
        /// PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void UserDefinedTypePropertyAccess(UserDefinedTypePropertyAccess x) {
            if(x.Collation is not null)this.SwitchIdentifier(x.Collation);
            this.CallTarget(x.CallTarget);
        }
        /// <summary>
        /// PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CaseExpression(CaseExpression x) {
            switch(x) {
                case SimpleCaseExpression y: this.SimpleCaseExpression(y);break;
                case SearchedCaseExpression y: this.SearchedCaseExpression(y);break;
            }
        }
        /// <summary>
        ///CaseExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SimpleCaseExpression(SimpleCaseExpression x) {
            if(x.Collation is not null)this.SwitchIdentifier(x.Collation);
            this.ScalarExpression(x.ElseExpression);
            this.ScalarExpression(x.InputExpression);
        }
        /// <summary>
        ///CaseExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SearchedCaseExpression(SearchedCaseExpression x) {
            if(x.Collation is not null)this.SwitchIdentifier(x.Collation);
            this.ScalarExpression(x.ElseExpression);
            foreach(var WhenClause in x.WhenClauses)this.WhenClause(WhenClause);
        }
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void NullIfExpression(NullIfExpression x) {
            if(x.Collation is not null)this.SwitchIdentifier(x.Collation);
        }
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CoalesceExpression(CoalesceExpression x) {
            if(x.Collation is not null)this.SwitchIdentifier(x.Collation);
            this.ScalarExpressions(x.Expressions);
        }
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void IIfCall(IIfCall x) {
            if(x.Collation is not null)this.SwitchIdentifier(x.Collation);
            this.BooleanExpression(x.Predicate);
            this.TSqlFragment(x.ThenExpression);
            this.TSqlFragment(x.ElseExpression);
        }
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ConvertCall(ConvertCall x) {
            if(x.Collation is not null)this.SwitchIdentifier(x.Collation);
            this.DataTypeReference(x.DataType);
            this.TSqlFragment(x.Parameter);
            this.TSqlFragment(x.Style);
        }
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TryConvertCall(TryConvertCall x) {
            if(x.Collation is not null)this.SwitchIdentifier(x.Collation);
            this.DataTypeReference(x.DataType);
            this.TSqlFragment(x.Parameter);
            this.TSqlFragment(x.Style);
        }
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ParseCall(ParseCall x) {
            if(x.Collation is not null)this.SwitchIdentifier(x.Collation);
            this.TSqlFragment(x.Culture);
            this.DataTypeReference(x.DataType);
            this.TSqlFragment(x.StringValue);
        }
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TryParseCall(TryParseCall x) {
            if(x.Collation is not null)this.SwitchIdentifier(x.Collation);
            this.TSqlFragment(x.Culture);
            this.DataTypeReference(x.DataType);
            this.TSqlFragment(x.StringValue);
        }
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CastCall(CastCall x) {
            if(x.Collation is not null)this.SwitchIdentifier(x.Collation);
            this.DataTypeReference(x.DataType);
            this.TSqlFragment(x.Parameter);
        }
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TryCastCall(TryCastCall x) {
            if(x.Collation is not null)this.SwitchIdentifier(x.Collation);
            this.DataTypeReference(x.DataType);
            this.TSqlFragment(x.Parameter);
        }
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AtTimeZoneCall(AtTimeZoneCall x) {
            if(x.Collation is not null)this.SwitchIdentifier(x.Collation);
            this.TSqlFragment(x.DateValue);
            this.TSqlFragment(x.TimeZone);
        }
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FunctionCall(FunctionCall x) {
            this.Identifier(x.FunctionName);
            if(x.Collation is not null)this.SwitchIdentifier(x.Collation);
            this.CallTarget(x.CallTarget);
            if(x.OverClause is not null)this.OverClause(x.OverClause);
            if(x.WithinGroupClause is not null)this.WithinGroupClause(x.WithinGroupClause);
            this.ScalarExpressions(x.Parameters);
        }
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LeftFunctionCall(LeftFunctionCall x) {
            if(x.Collation is not null) this.SwitchIdentifier(x.Collation);
            this.ScalarExpressions(x.Parameters);
        }
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RightFunctionCall(RightFunctionCall x) {
            if(x.Collation is not null) this.SwitchIdentifier(x.Collation);
            this.ScalarExpressions(x.Parameters);
        }
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void PartitionFunctionCall(PartitionFunctionCall x) {
            if(x.Collation is not null) this.SwitchIdentifier(x.Collation);
            this.ScalarExpressions(x.Parameters);
        }
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ParameterlessCall(ParameterlessCall x) {
            if(x.Collation is not null) this.SwitchIdentifier(x.Collation);
        }
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ScalarSubquery(ScalarSubquery x) {
            if(x.Collation is not null) this.SwitchIdentifier(x.Collation);
            this.QueryExpression(x.QueryExpression);
        }
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OdbcFunctionCall(OdbcFunctionCall x) {
            this.SwitchIdentifier(x.Name);
            this.ScalarExpressions(x.Parameters);
        }
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ParenthesisExpression(ParenthesisExpression x) {
            if(x.Collation is not null) this.SwitchIdentifier(x.Collation);
            this.ScalarExpression(x.Expression);
        }
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ColumnReferenceExpression(ColumnReferenceExpression x) {
            if(x.Collation is not null) this.SwitchIdentifier(x.Collation);
            if(x.MultiPartIdentifier is not null)this.SwitchMultiPartIdentifier(x.MultiPartIdentifier);
        }
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void NextValueForExpression(NextValueForExpression x) {
            if(x.Collation is not null) this.SwitchIdentifier(x.Collation);
            this.OverClause(x.OverClause);
            this.SwitchSchemaObjectName(x.SequenceName);
        }
        /// <summary>
        ///ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExtractFromExpression(ExtractFromExpression x) {
            this.ScalarExpression(x.Expression);
        }
        /// <summary>
        ///ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OdbcConvertSpecification(OdbcConvertSpecification x) {
            this.SwitchIdentifier(x.Identifier);
        }
        /// <summary>
        ///ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BinaryExpression(BinaryExpression x) {
            this.ScalarExpression(x.FirstExpression);
            this.ScalarExpression(x.SecondExpression);
        }
        /// <summary>
        ///ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void IdentityFunctionCall(IdentityFunctionCall x) {
            this.ScalarExpression(x.Increment);
            this.ScalarExpression(x.Seed);
            this.DataTypeReference(x.DataType);
        }
        /// <summary>
        ///ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void UnaryExpression(UnaryExpression x) {
            this.ScalarExpression(x.Expression);
        }
        /// <summary>
        ///ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ScalarExpressionSnippet(ScalarExpressionSnippet x) {
        }
        /// <summary>
        ///ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SourceDeclaration(SourceDeclaration x) {
            this.EventSessionObjectName(x.Value);
        }
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void IdentifierOrValueExpression(IdentifierOrValueExpression x) {
            this.SwitchIdentifier(x.Identifier);
            this.ValueExpression(x.ValueExpression);
        }
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void StatementList(StatementList x) {
            switch(x) {
                case StatementListSnippet y:this.StatementListSnippet(y);break;
                default:
                    foreach(var a in x.Statements)
                        this.TSqlStatement(a);
                    break;
            }
        }
        /// <summary>
        /// StatementListSnippet:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void StatementListSnippet(StatementListSnippet x) {
            this.Statements(x.Statements);
        }
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TSqlStatement(TSqlStatement x) {
            switch(x) {
                case ExecuteStatement y: this.ExecuteStatement(y);break;
                case ViewStatementBody y: this.ViewStatementBody(y);break;
                case TriggerStatementBody y:this.TriggerStatementBody(y);break;
                case ProcedureStatementBodyBase y:this.ProcedureStatementBodyBase(y);break;
                case DeclareTableVariableStatement y:this.DeclareTableVariableStatement(y);break;
                case StatementWithCtesAndXmlNamespaces y:this.StatementWithCtesAndXmlNamespaces(y);break;
                case BeginEndBlockStatement y:this.BeginEndBlockStatement(y);break;
                case TransactionStatement y: this.TransactionStatement(y);break;
                case BreakStatement y:this.BreakStatement(y);break;
                case ContinueStatement y:this.ContinueStatement(y);break;
                case CreateDefaultStatement y:this.CreateDefaultStatement(y);break;
                case CreateRuleStatement y:this.CreateRuleStatement(y);break;
                case DeclareVariableStatement y:this.DeclareVariableStatement(y);break;
                case GoToStatement y:this.GoToStatement(y);break;
                case IfStatement y:this.IfStatement(y);break;
                case LabelStatement y:this.LabelStatement(y);break;
                case WhileStatement y:this.WhileStatement(y);break;
                case CreateSchemaStatement y:this.CreateSchemaStatement(y);break;
                case WaitForStatement y:this.WaitForStatement(y);break;
                case ReadTextStatement y:this.ReadTextStatement(y);break;
                case TextModificationStatement y:this.TextModificationStatement(y);break;
                case LineNoStatement y:this.LineNoStatement(y);break;
                case SecurityStatement y:this.SecurityStatement(y);break;
                case AlterAuthorizationStatement y:this.AlterAuthorizationStatement(y);break;
                case SecurityStatementBody80 y:this.SecurityStatementBody80(y);break;
                case PrintStatement y:this.PrintStatement(y);break;
                case SequenceStatement y:this.SequenceStatement(y);break;
                case DropObjectsStatement y:this.DropObjectsStatement(y);break;
                case SecurityPolicyStatement y:this.SecurityPolicyStatement(y);break;
                case CreateColumnMasterKeyStatement y:this.CreateColumnMasterKeyStatement(y);break;
                case DropUnownedObjectStatement y:this.DropUnownedObjectStatement(y);break;
                case ColumnEncryptionKeyStatement y:this.ColumnEncryptionKeyStatement(y);break;
                case ExternalTableStatement y:this.ExternalTableStatement(y);break;
                case ExternalDataSourceStatement y:this.ExternalDataSourceStatement(y);break;
                case ExternalFileFormatStatement y:this.ExternalFileFormatStatement(y);break;
                case AssemblyStatement y:this.AssemblyStatement(y);break;
                case CreateXmlSchemaCollectionStatement y:this.CreateXmlSchemaCollectionStatement(y);break;
                case AlterXmlSchemaCollectionStatement y:this.AlterXmlSchemaCollectionStatement(y);break;
                case DropXmlSchemaCollectionStatement y:this.DropXmlSchemaCollectionStatement(y);break;
                case AlterTableStatement y:this.AlterTableStatement(y);break;
                case EnableDisableTriggerStatement y:this.EnableDisableTriggerStatement(y);break;
                case TryCatchStatement y:this.TryCatchStatement(y);break;
                case CreateTypeStatement y:this.CreateTypeStatement(y);break;
                case CreateSynonymStatement y:this.CreateSynonymStatement(y);break;
                case RouteStatement y:this.RouteStatement(y);break;
                case QueueStatement y:this.QueueStatement(y);break;
                case IndexDefinition y:this.IndexDefinition(y);break;
                case IndexStatement y:this.IndexStatement(y);break;
                case CreateFullTextIndexStatement y:this.CreateFullTextIndexStatement(y);break;
                case CreateEventNotificationStatement y:this.CreateEventNotificationStatement(y);break;
                case MasterKeyStatement y:this.MasterKeyStatement(y);break;
                case ApplicationRoleStatement y:this.ApplicationRoleStatement(y);break;
                case RoleStatement y:this.RoleStatement(y);break;
                case UserStatement y:this.UserStatement(y);break;
                case CreateStatisticsStatement y:this.CreateStatisticsStatement(y);break;
                case UpdateStatisticsStatement y:this.UpdateStatisticsStatement(y);break;
                case ReturnStatement y:this.ReturnStatement(y);break;
                case DeclareCursorStatement y:this.DeclareCursorStatement(y);break;
                case SetVariableStatement y:this.SetVariableStatement(y);break;
                case CursorStatement y:this.CursorStatement(y);break;
                case OpenSymmetricKeyStatement y:this.OpenSymmetricKeyStatement(y);break;
                case CloseSymmetricKeyStatement y:this.CloseSymmetricKeyStatement(y);break;
                case OpenMasterKeyStatement y:this.OpenMasterKeyStatement(y);break;
                case CloseMasterKeyStatement y:this.CloseMasterKeyStatement(y);break;
                case DropDatabaseStatement y:this.DropDatabaseStatement(y);break;
                case DropChildObjectsStatement y:this.DropChildObjectsStatement(y);break;
                case DropIndexStatement y:this.DropIndexStatement(y);break;
                case DropSchemaStatement y:this.DropSchemaStatement(y);break;
                case RaiseErrorLegacyStatement y:this.RaiseErrorLegacyStatement(y);break;
                case RaiseErrorStatement y:this.RaiseErrorStatement(y);break;
                case ThrowStatement y:this.ThrowStatement(y);break;
                case UseStatement y:this.UseStatement(y);break;
                case KillStatement y:this.KillStatement(y);break;
                case KillQueryNotificationSubscriptionStatement y:this.KillQueryNotificationSubscriptionStatement(y);break;
                case KillStatsJobStatement y:this.KillStatsJobStatement(y);break;
                case CheckpointStatement y:this.CheckpointStatement(y);break;
                case ReconfigureStatement y:this.ReconfigureStatement(y);break;
                case ShutdownStatement y:this.ShutdownStatement(y);break;
                case SetUserStatement y:this.SetUserStatement(y);break;
                case TruncateTableStatement y:this.TruncateTableStatement(y);break;
                case SetOnOffStatement y:this.SetOnOffStatement(y);break;
                case SetRowCountStatement y:this.SetRowCountStatement(y);break;
                case SetCommandStatement y:this.SetCommandStatement(y);break;
                case SetTransactionIsolationLevelStatement y:this.SetTransactionIsolationLevelStatement(y);break;
                case SetTextSizeStatement y:this.SetTextSizeStatement(y);break;
                case SetErrorLevelStatement y:this.SetErrorLevelStatement(y);break;
                case CreateDatabaseStatement y:this.CreateDatabaseStatement(y);break;
                case AlterDatabaseStatement y:this.AlterDatabaseStatement(y);break;
                case AlterDatabaseScopedConfigurationStatement y:this.AlterDatabaseScopedConfigurationStatement(y);break;
                case CreateTableStatement y:this.CreateTableStatement(y);break;
                case BackupStatement y:this.BackupStatement(y);break;
                case RestoreStatement y:this.RestoreStatement(y);break;
                case BulkInsertBase y:this.BulkInsertBase(y);break;
                case DbccStatement y:this.DbccStatement(y);break;
                case CreateAsymmetricKeyStatement y:this.CreateAsymmetricKeyStatement(y);break;
                case CreatePartitionFunctionStatement y:this.CreatePartitionFunctionStatement(y);break;
                case CreatePartitionSchemeStatement y:this.CreatePartitionSchemeStatement(y);break;
                case RemoteServiceBindingStatementBase y:this.RemoteServiceBindingStatementBase(y);break;
                case CertificateStatementBase y:this.CertificateStatementBase(y);break;
                case CreateContractStatement y:this.CreateContractStatement(y);break;
                case CredentialStatement y:this.CredentialStatement(y);break;
                case MessageTypeStatementBase y:this.MessageTypeStatementBase(y);break;
                case CreateAggregateStatement y:this.CreateAggregateStatement(y);break;
                case AlterCreateEndpointStatementBase y:this.AlterCreateEndpointStatementBase(y);break;
                case SymmetricKeyStatement y:this.SymmetricKeyStatement(y);break;
                case FullTextCatalogStatement y:this.FullTextCatalogStatement(y);break;
                case AlterCreateServiceStatementBase y:this.AlterCreateServiceStatementBase(y);break;
                case DropFullTextIndexStatement y:this.DropFullTextIndexStatement(y);break;
                case DropTypeStatement y:this.DropTypeStatement(y);break;
                case DropMasterKeyStatement y:this.DropMasterKeyStatement(y);break;
                case AlterPartitionFunctionStatement y:this.AlterPartitionFunctionStatement(y);break;
                case AlterPartitionSchemeStatement y:this.AlterPartitionSchemeStatement(y);break;
                case AlterFullTextIndexStatement y:this.AlterFullTextIndexStatement(y);break;
                case CreateSearchPropertyListStatement y:this.CreateSearchPropertyListStatement(y);break;
                case AlterSearchPropertyListStatement y:this.AlterSearchPropertyListStatement(y);break;
                case CreateLoginStatement y:this.CreateLoginStatement(y);break;
                case AlterLoginStatement y:this.AlterLoginStatement(y);break;
                case RevertStatement y:this.RevertStatement(y);break;
                case DropQueueStatement y:this.DropQueueStatement(y);break;
                case SignatureStatementBase y:this.SignatureStatementBase(y);break;
                case DropEventNotificationStatement y:this.DropEventNotificationStatement(y);break;
                case ExecuteAsStatement y:this.ExecuteAsStatement(y);break;
                case EndConversationStatement y:this.EndConversationStatement(y);break;
                case MoveConversationStatement y:this.MoveConversationStatement(y);break;
                case WaitForSupportedStatement y:this.WaitForSupportedStatement(y);break;
                case SendStatement y:this.SendStatement(y);break;
                case AlterSchemaStatement y:this.AlterSchemaStatement(y);break;
                case AlterAsymmetricKeyStatement y:this.AlterAsymmetricKeyStatement(y);break;
                case AlterServiceMasterKeyStatement y:this.AlterServiceMasterKeyStatement(y);break;
                case BeginConversationTimerStatement y:this.BeginConversationTimerStatement(y);break;
                case BeginDialogStatement y:this.BeginDialogStatement(y);break;
                case BackupRestoreMasterKeyStatementBase y:this.BackupRestoreMasterKeyStatementBase(y);break;
                case TSqlStatementSnippet y:this.TSqlStatementSnippet(y);break;
                case AuditSpecificationStatement y:this.AuditSpecificationStatement(y);break;
                case ServerAuditStatement y:this.ServerAuditStatement(y);break;
                case DatabaseEncryptionKeyStatement y:this.DatabaseEncryptionKeyStatement(y);break;
                case DropDatabaseEncryptionKeyStatement y:this.DropDatabaseEncryptionKeyStatement(y);break;
                case ResourcePoolStatement y:this.ResourcePoolStatement(y);break;
                case ExternalResourcePoolStatement y:this.ExternalResourcePoolStatement(y);break;
                case WorkloadGroupStatement y:this.WorkloadGroupStatement(y);break;
                case BrokerPriorityStatement y:this.BrokerPriorityStatement(y);break;
                case CreateFullTextStopListStatement y:this.CreateFullTextStopListStatement(y);break;
                case AlterFullTextStopListStatement y:this.AlterFullTextStopListStatement(y);break;
                case CreateCryptographicProviderStatement y:this.CreateCryptographicProviderStatement(y);break;
                case AlterCryptographicProviderStatement y:this.AlterCryptographicProviderStatement(y);break;
                case EventSessionStatement y:this.EventSessionStatement(y);break;
                case AlterResourceGovernorStatement y:this.AlterResourceGovernorStatement(y);break;
                case CreateSpatialIndexStatement y:this.CreateSpatialIndexStatement(y);break;
                case AlterServerConfigurationStatement y:this.AlterServerConfigurationStatement(y);break;
                case AlterServerConfigurationSetBufferPoolExtensionStatement y:this.AlterServerConfigurationSetBufferPoolExtensionStatement(y);break;
                case AlterServerConfigurationSetDiagnosticsLogStatement y:this.AlterServerConfigurationSetDiagnosticsLogStatement(y);break;
                case AlterServerConfigurationSetFailoverClusterPropertyStatement y:this.AlterServerConfigurationSetFailoverClusterPropertyStatement(y);break;
                case AlterServerConfigurationSetHadrClusterStatement y:this.AlterServerConfigurationSetHadrClusterStatement(y);break;
                case AlterServerConfigurationSetSoftNumaStatement y:this.AlterServerConfigurationSetSoftNumaStatement(y);break;
                case AvailabilityGroupStatement y:this.AvailabilityGroupStatement(y);break;
                case CreateFederationStatement y:this.CreateFederationStatement(y);break;
                case AlterFederationStatement y:this.AlterFederationStatement(y);break;
                case UseFederationStatement y:this.UseFederationStatement(y);break;
                case DiskStatement y:this.DiskStatement(y);break;
                case CreateColumnStoreIndexStatement y:this.CreateColumnStoreIndexStatement(y);break;
            }
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExecuteStatement(ExecuteStatement x) {
            this.ExecuteSpecification(x.ExecuteSpecification);
            foreach(var Option in x.Options) {
                this.ExecuteOption(Option);
            }
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ViewStatementBody(ViewStatementBody x) {
            switch(x) {
                case AlterViewStatement y:this.AlterViewStatement(y);break;
                case CreateViewStatement y:this.CreateViewStatement(y);break;
                case CreateOrAlterViewStatement y:this.CreateOrAlterViewStatement(y);break;
            }
        }
        /// <summary>
        ///ViewStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterViewStatement(AlterViewStatement x) {
            this.Identifiers(x.Columns);
            this.SwitchSchemaObjectName(x.SchemaObjectName);
            this.SwitchSelectStatement(x.SelectStatement);
            this.ViewOptions(x.ViewOptions);
        }
        /// <summary>
        ///ViewStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateViewStatement(CreateViewStatement x) {
            this.Identifiers(x.Columns);
            this.SwitchSchemaObjectName(x.SchemaObjectName);
            this.SwitchSelectStatement(x.SelectStatement);
            this.ViewOptions(x.ViewOptions);
        }
        /// <summary>
        ///ViewStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateOrAlterViewStatement(CreateOrAlterViewStatement x) {
            this.Identifiers(x.Columns);
            this.SwitchSchemaObjectName(x.SchemaObjectName);
            this.SwitchSelectStatement(x.SelectStatement);
            this.ViewOptions(x.ViewOptions);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TriggerStatementBody(TriggerStatementBody x) {
            switch(x) {
                case AlterTriggerStatement y:this.AlterTriggerStatement(y);break;
                case CreateTriggerStatement y:this.CreateTriggerStatement(y);break;
                case CreateOrAlterTriggerStatement y:this.CreateOrAlterTriggerStatement(y);break;
            }
        }
        /// <summary>
        ///TriggerStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterTriggerStatement(AlterTriggerStatement x) {
            this.SchemaObjectName(x.Name);
            this.StatementList(x.StatementList);
            this.MethodSpecifier(x.MethodSpecifier);
            this.TriggerObject(x.TriggerObject);
            this.TriggerActions(x.TriggerActions);
        }
        /// <summary>
        ///TriggerStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateTriggerStatement(CreateTriggerStatement x) {
            this.SchemaObjectName(x.Name);
            this.StatementList(x.StatementList);
            this.MethodSpecifier(x.MethodSpecifier);
            this.TriggerObject(x.TriggerObject);
            this.TriggerActions(x.TriggerActions);
        }
        /// <summary>
        ///TriggerStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateOrAlterTriggerStatement(CreateOrAlterTriggerStatement x) {
            this.SchemaObjectName(x.Name);
            this.StatementList(x.StatementList);
            this.MethodSpecifier(x.MethodSpecifier);
            this.TriggerObject(x.TriggerObject);
            this.TriggerActions(x.TriggerActions);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ProcedureStatementBodyBase(ProcedureStatementBodyBase x) {
            switch(x) {
                case ProcedureStatementBody y:this.ProcedureStatementBody(y);break;
                case FunctionStatementBody y:this.FunctionStatementBody(y);break;
            }
        }
        /// <summary>
        ///ProcedureStatementBodyBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ProcedureStatementBody(ProcedureStatementBody x) {
            switch(x) {
                case AlterProcedureStatement y:this.AlterProcedureStatement(y);break;
                case CreateProcedureStatement y:this.CreateProcedureStatement(y);break;
                case CreateOrAlterProcedureStatement y:this.CreateOrAlterProcedureStatement(y);break;
            }
        }
        /// <summary>
        ///ProcedureStatementBody:TriggerStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterProcedureStatement(AlterProcedureStatement x) {
            this.StatementList(x.StatementList);
            this.ProcedureReference(x.ProcedureReference);
            this.MethodSpecifier(x.MethodSpecifier);
            this.ProcedureParameters(x.Parameters);
            this.ProcedureOptions(x.Options);
        }
        /// <summary>
        ///ProcedureStatementBody:TriggerStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateProcedureStatement(CreateProcedureStatement x) {
            this.StatementList(x.StatementList);
            this.ProcedureReference(x.ProcedureReference);
            this.MethodSpecifier(x.MethodSpecifier);
            this.ProcedureParameters(x.Parameters);
            this.ProcedureOptions(x.Options);
        }
        /// <summary>
        ///ProcedureStatementBody:TriggerStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateOrAlterProcedureStatement(CreateOrAlterProcedureStatement x) {
            this.StatementList(x.StatementList);
            this.ProcedureReference(x.ProcedureReference);
            this.MethodSpecifier(x.MethodSpecifier);
            this.ProcedureParameters(x.Parameters);
            this.ProcedureOptions(x.Options);
        }
        ///ProcedureStatementBodyBase:TriggerStatementBody:TSqlStatement:TSqlFragment
        protected virtual void FunctionStatementBody(FunctionStatementBody x) {
            switch(x) {
                case AlterFunctionStatement y:this.AlterFunctionStatement(y);break;
                case CreateFunctionStatement y:this.CreateFunctionStatement(y);break;
                case CreateOrAlterFunctionStatement y:this.CreateOrAlterFunctionStatement(y);break;
            }
        }
        /// <summary>
        ///ProcedureStatementBody:ProcedureStatementBodyBase:TriggerStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterFunctionStatement(AlterFunctionStatement x) { 
            this.MethodSpecifier(x.MethodSpecifier);
            this.SchemaObjectName(x.Name);
            this.FunctionReturnType(x.ReturnType);
            this.ProcedureParameters(x.Parameters);
            this.FunctionOptions(x.Options);
            this.OrderBulkInsertOption(x.OrderHint);
        }
        /// <summary>
        ///ProcedureStatementBody:ProcedureStatementBodyBase:TriggerStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateFunctionStatement(CreateFunctionStatement x) { 
            if(x.MethodSpecifier is not null)this.MethodSpecifier(x.MethodSpecifier);
            this.SchemaObjectName(x.Name);
            this.FunctionReturnType(x.ReturnType);
            this.ProcedureParameters(x.Parameters);
            this.FunctionOptions(x.Options);
            if(x.OrderHint is not null)this.OrderBulkInsertOption(x.OrderHint);
            if(x.StatementList is not null)this.StatementList(x.StatementList);
        }
        /// <summary>
        ///ProcedureStatementBody:ProcedureStatementBodyBase:TriggerStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateOrAlterFunctionStatement(CreateOrAlterFunctionStatement x) { 
            this.MethodSpecifier(x.MethodSpecifier);
            this.SchemaObjectName(x.Name);
            this.FunctionReturnType(x.ReturnType);
            this.ProcedureParameters(x.Parameters);
            this.FunctionOptions(x.Options);
            this.OrderBulkInsertOption(x.OrderHint);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DeclareTableVariableStatement(DeclareTableVariableStatement x) {
            this.DeclareTableVariableBody(x.Body);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void StatementWithCtesAndXmlNamespaces(StatementWithCtesAndXmlNamespaces x) {
            switch(x) {
                case SelectStatement y:this.SwitchSelectStatement(y);break;
                case DataModificationStatement y:this.DataModificationStatement(y);break;
            }
        }
        /// <summary>
        ///StatementWithCtesAndXmlNamespaces:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SwitchSelectStatement(SelectStatement x) {
            switch(x) {
                case SelectStatementSnippet y:this.SelectStatementSnippet(y);break;
                default: this.SelectStatement(x);break;
            }
        }
        /// <summary>
        ///SelectStatement:StatementWithCtesAndXmlNamespaces:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SelectStatementSnippet(SelectStatementSnippet x) {
            if(x.WithCtesAndXmlNamespaces is not null)this.WithCtesAndXmlNamespaces(x.WithCtesAndXmlNamespaces);
            this.ComputeClauses(x.ComputeClauses);
            if(x.Into is not null)this.SwitchSchemaObjectName(x.Into);
            if(x.On is not null)this.SwitchIdentifier(x.On);
            this.OptimizerHints(x.OptimizerHints);
            this.QueryExpression(x.QueryExpression);
        }
        protected virtual void SelectStatement(SelectStatement x) {
            if(x.WithCtesAndXmlNamespaces is not null)this.WithCtesAndXmlNamespaces(x.WithCtesAndXmlNamespaces);
            this.ComputeClauses(x.ComputeClauses);
            if(x.Into is not null)this.SwitchSchemaObjectName(x.Into);
            if(x.On is not null)this.SwitchIdentifier(x.On);
            this.OptimizerHints(x.OptimizerHints);
            this.QueryExpression(x.QueryExpression);
        }
        /// <summary>
        ///StatementWithCtesAndXmlNamespaces:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DataModificationStatement(DataModificationStatement x) {
            switch(x) {
                case DeleteStatement y:this.DeleteStatement(y);break;
                case InsertStatement y:this.InsertStatement(y);break;
                case UpdateStatement y:this.UpdateStatement(y);break;
                case MergeStatement y:this.MergeStatement(y);break;
            }
        }
        /// <summary>
        ///DeleteStatement:StatementWithCtesAndXmlNamespaces:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DeleteStatement(DeleteStatement x) {
            this.DeleteSpecification(x.DeleteSpecification);
            if(x.WithCtesAndXmlNamespaces is not null)this.WithCtesAndXmlNamespaces(x.WithCtesAndXmlNamespaces);
            this.OptimizerHints(x.OptimizerHints);
        }
        /// <summary>
        ///DeleteStatement:StatementWithCtesAndXmlNamespaces:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void InsertStatement(InsertStatement x) {
            this.InsertSpecification(x.InsertSpecification);
            if(x.WithCtesAndXmlNamespaces is not null)this.WithCtesAndXmlNamespaces(x.WithCtesAndXmlNamespaces);
            this.OptimizerHints(x.OptimizerHints);
        }
        /// <summary>
        ///DeleteStatement:StatementWithCtesAndXmlNamespaces:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void UpdateStatement(UpdateStatement x) {
            this.UpdateSpecification(x.UpdateSpecification);
            if(x.WithCtesAndXmlNamespaces is not null)this.WithCtesAndXmlNamespaces(x.WithCtesAndXmlNamespaces);
            this.OptimizerHints(x.OptimizerHints);
        }
        /// <summary>
        ///DeleteStatement:StatementWithCtesAndXmlNamespaces:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MergeStatement(MergeStatement x) {
            this.MergeSpecification(x.MergeSpecification);
            if(x.WithCtesAndXmlNamespaces is not null)this.WithCtesAndXmlNamespaces(x.WithCtesAndXmlNamespaces);
            this.OptimizerHints(x.OptimizerHints);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BeginEndBlockStatement(BeginEndBlockStatement x) {
            switch(x) {
                case BeginEndAtomicBlockStatement y:this.BeginEndAtomicBlockStatement(y);break;
                default:
                    this.StatementList(x.StatementList);break;
            }
        }
        /// <summary>
        ///BeginEndBlockStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BeginEndAtomicBlockStatement(BeginEndAtomicBlockStatement x) {
            this.StatementList(x.StatementList);
            foreach(var Option in x.Options)
                this.AtomicBlockOption(Option);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TransactionStatement(TransactionStatement x) {
            switch(x) {
                case BeginTransactionStatement y: this.BeginTransactionStatement(y);break;
                case CommitTransactionStatement y: this.CommitTransactionStatement(y);break;
                case RollbackTransactionStatement y: this.RollbackTransactionStatement(y);break;
                case SaveTransactionStatement y: this.SaveTransactionStatement(y);break;
            }
        }
        /// <summary>
        ///TransactionStatement:BeginEndBlockStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BeginTransactionStatement(BeginTransactionStatement x) {
            this.ValueExpression(x.MarkDescription);
            this.IdentifierOrValueExpression(x.Name);
        }
        /// <summary>
        ///TransactionStatement:BeginEndBlockStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CommitTransactionStatement(CommitTransactionStatement x) {
            this.IdentifierOrValueExpression(x.Name);
        }
        /// <summary>
        ///TransactionStatement:BeginEndBlockStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RollbackTransactionStatement(RollbackTransactionStatement x) {
            this.IdentifierOrValueExpression(x.Name);
        }
        /// <summary>
        ///TransactionStatement:BeginEndBlockStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SaveTransactionStatement(SaveTransactionStatement x) {
            this.IdentifierOrValueExpression(x.Name);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BreakStatement(BreakStatement x) {
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ContinueStatement(ContinueStatement x) {
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateDefaultStatement(CreateDefaultStatement x) {
            this.SchemaObjectName(x.Name);
            this.ScalarExpression(x.Expression);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateRuleStatement(CreateRuleStatement x) {
            this.SchemaObjectName(x.Name);
            this.BooleanExpression(x.Expression);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DeclareVariableStatement(DeclareVariableStatement x) {
            foreach(var Declaration in x.Declarations)
                this.DeclareVariableElement(Declaration);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void GoToStatement(GoToStatement x) {
            this.Identifier(x.LabelName);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void IfStatement(IfStatement x) {
            this.TSqlStatement(x.ElseStatement);
            this.BooleanExpression(x.Predicate);
            this.TSqlStatement(x.ThenStatement);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LabelStatement(LabelStatement x) {
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void WhileStatement(WhileStatement x) {
            this.TSqlStatement(x.Statement);
            this.BooleanExpression(x.Predicate);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateSchemaStatement(CreateSchemaStatement x) {
            this.Identifier(x.Name);
            this.Identifier(x.Owner);
            this.StatementList(x.StatementList);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void WaitForStatement(WaitForStatement x) {
            this.ValueExpression(x.Parameter);
            this.WaitForSupportedStatement(x.Statement);
            this.ScalarExpression(x.Timeout);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ReadTextStatement(ReadTextStatement x) {
            this.ColumnReferenceExpression(x.Column);
            this.ValueExpression(x.Offset);
            this.ValueExpression(x.Size);
            this.ValueExpression(x.TextPointer);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TextModificationStatement(TextModificationStatement x) {
            switch(x) {
                case UpdateTextStatement y:this.UpdateTextStatement(y);break;
                case WriteTextStatement y:this.WriteTextStatement(y);break;
            }
        }
        /// <summary>
        ///TextModificationStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void UpdateTextStatement(UpdateTextStatement x) {
            this.ValueExpression(x.SourceParameter);
            this.ScalarExpression(x.DeleteLength);
            this.ScalarExpression(x.InsertOffset);
            this.ColumnReferenceExpression(x.SourceColumn);
            this.ColumnReferenceExpression(x.Column);
            this.ValueExpression(x.TextId);
            this.Literal(x.Timestamp);
        }
        /// <summary>
        ///TextModificationStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void WriteTextStatement(WriteTextStatement x) {
            this.ValueExpression(x.SourceParameter);
            this.ColumnReferenceExpression(x.Column);
            this.ValueExpression(x.TextId);
            this.Literal(x.Timestamp);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LineNoStatement(LineNoStatement x) {
            this.IntegerLiteral(x.LineNo);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SecurityStatement(SecurityStatement x) {
            switch(x) {
                case GrantStatement y:this.GrantStatement(y);break;
                case DenyStatement y:this.DenyStatement(y);break;
                case RevokeStatement y:this.RevokeStatement(y);break;
            }
        }
        /// <summary>
        ///SecurityStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void GrantStatement(GrantStatement x) {
            this.Identifier(x.AsClause);
            this.SecurityTargetObject(x.SecurityTargetObject);
            this.Permissions(x.Permissions);
            this.SecurityPrincipals(x.Principals);
        }
        /// <summary>
        ///SecurityStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DenyStatement(DenyStatement x) {
            this.Identifier(x.AsClause);
            this.SecurityTargetObject(x.SecurityTargetObject);
            this.Permissions(x.Permissions);
            this.SecurityPrincipals(x.Principals);
        }
        /// <summary>
        ///SecurityStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RevokeStatement(RevokeStatement x) {
            this.Identifier(x.AsClause);
            this.SecurityTargetObject(x.SecurityTargetObject);
            this.Permissions(x.Permissions);
            this.SecurityPrincipals(x.Principals);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterAuthorizationStatement(AlterAuthorizationStatement x) {
            this.Identifier(x.PrincipalName);
            this.SecurityTargetObject(x.SecurityTargetObject);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SecurityStatementBody80(SecurityStatementBody80 x) {
            switch(x) {
                case GrantStatement80 y:this.GrantStatement80(y);break;
                case DenyStatement80 y:this.DenyStatement80(y);break;
                case RevokeStatement80 y:this.RevokeStatement80(y);break;
            }
        }
        /// <summary>
        ///SecurityStatementBody80:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void GrantStatement80(GrantStatement80 x) {
            this.Identifier(x.AsClause);
            this.SecurityElement80(x.SecurityElement80);
            this.SecurityUserClause80(x.SecurityUserClause80);
        }
        /// <summary>
        ///SecurityStatementBody80:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DenyStatement80(DenyStatement80 x) {
            this.SecurityElement80(x.SecurityElement80);
            this.SecurityUserClause80(x.SecurityUserClause80);
        }
        /// <summary>
        ///SecurityStatementBody80:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RevokeStatement80(RevokeStatement80 x) {
            this.Identifier(x.AsClause);
            this.SecurityElement80(x.SecurityElement80);
            this.SecurityUserClause80(x.SecurityUserClause80);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void PrintStatement(PrintStatement x) {
            this.ScalarExpression(x.Expression);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SequenceStatement(SequenceStatement x) {
            switch(x) {
                case CreateSequenceStatement y:this.CreateSequenceStatement(y);break;
                case AlterSequenceStatement y:this.AlterSequenceStatement(y);break;
            }
        }
        /// <summary>
        ///SequenceStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateSequenceStatement(CreateSequenceStatement x) {
            this.SchemaObjectName(x.Name);
            this.SequenceOptions(x.SequenceOptions);
        }
        /// <summary>
        ///SequenceStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterSequenceStatement(AlterSequenceStatement x) {
            this.SchemaObjectName(x.Name);
            this.SequenceOptions(x.SequenceOptions);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropObjectsStatement(DropObjectsStatement x) {
            switch(x) {
                case DropSequenceStatement y:this.DropSequenceStatement(y);break;
                case DropSecurityPolicyStatement y:this.DropSecurityPolicyStatement(y);break;
                case DropExternalTableStatement y:this.DropExternalTableStatement(y);break;
                case DropTableStatement y:this.DropTableStatement(y);break;
                case DropProcedureStatement y:this.DropProcedureStatement(y);break;
                case DropFunctionStatement y:this.DropFunctionStatement(y);break;
                case DropViewStatement y:this.DropViewStatement(y);break;
                case DropDefaultStatement y:this.DropDefaultStatement(y);break;
                case DropRuleStatement y:this.DropRuleStatement(y);break;
                case DropTriggerStatement y:this.DropTriggerStatement(y);break;
                case DropSynonymStatement y:this.DropSynonymStatement(y);break;
                case DropAggregateStatement y:this.DropAggregateStatement(y);break;
                case DropAssemblyStatement y:this.DropAssemblyStatement(y);break;
            }
        }
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropSequenceStatement(DropSequenceStatement x) {
            this.SchemaObjectNames(x.Objects);
        }
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropSecurityPolicyStatement(DropSecurityPolicyStatement x) {
            this.SchemaObjectNames(x.Objects);
        }
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropExternalTableStatement(DropExternalTableStatement x) {
            this.SchemaObjectNames(x.Objects);
        }
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropTableStatement(DropTableStatement x) {
            this.SchemaObjectNames(x.Objects);
        }
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropProcedureStatement(DropProcedureStatement x) {
            this.SchemaObjectNames(x.Objects);
        }
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropFunctionStatement(DropFunctionStatement x) {
            this.SchemaObjectNames(x.Objects);
        }
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropViewStatement(DropViewStatement x) {
            this.SchemaObjectNames(x.Objects);
        }
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropDefaultStatement(DropDefaultStatement x) {
            this.SchemaObjectNames(x.Objects);
        }
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropRuleStatement(DropRuleStatement x) {
            this.SchemaObjectNames(x.Objects);
        }
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropTriggerStatement(DropTriggerStatement x) {
            this.SchemaObjectNames(x.Objects);
        }
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropSynonymStatement(DropSynonymStatement x) {
            this.SchemaObjectNames(x.Objects);
        }
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropAggregateStatement(DropAggregateStatement x) {
            this.SchemaObjectNames(x.Objects);
        }
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropAssemblyStatement(DropAssemblyStatement x) {
            this.SchemaObjectNames(x.Objects);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SecurityPolicyStatement(SecurityPolicyStatement x) {
            switch(x) {
                case CreateSecurityPolicyStatement y:this.CreateSecurityPolicyStatement(y);break;
                case AlterSecurityPolicyStatement y:this.AlterSecurityPolicyStatement(y);break;
            }
        }
        /// <summary>
        ///SecurityPolicyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateSecurityPolicyStatement(CreateSecurityPolicyStatement x) {
            this.SchemaObjectName(x.Name);
            this.SecurityPolicyOptions(x.SecurityPolicyOptions);
            this.SecurityPredicateActions(x.SecurityPredicateActions);
        }
        /// <summary>
        ///SecurityPolicyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterSecurityPolicyStatement(AlterSecurityPolicyStatement x) {
            this.SchemaObjectName(x.Name);
            this.SecurityPolicyOptions(x.SecurityPolicyOptions);
            this.SecurityPredicateActions(x.SecurityPredicateActions);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateColumnMasterKeyStatement(CreateColumnMasterKeyStatement x) {
            this.Identifier(x.Name);
            foreach(var Parameter in x.Parameters)
                this.ColumnMasterKeyParameter(Parameter);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropUnownedObjectStatement(DropUnownedObjectStatement x) {
            switch(x) {
                case DropColumnMasterKeyStatement y:this.DropColumnMasterKeyStatement(y);break;
                case DropColumnEncryptionKeyStatement y:this.DropColumnEncryptionKeyStatement(y);break;
                case DropExternalDataSourceStatement y:this.DropExternalDataSourceStatement(y);break;
                case DropExternalFileFormatStatement y:this.DropExternalFileFormatStatement(y);break;
                case DropServerRoleStatement y:this.DropServerRoleStatement(y);break;
                case DropPartitionFunctionStatement y:this.DropPartitionFunctionStatement(y);break;
                case DropPartitionSchemeStatement y:this.DropPartitionSchemeStatement(y);break;
                case DropApplicationRoleStatement y:this.DropApplicationRoleStatement(y);break;
                case DropFullTextCatalogStatement y:this.DropFullTextCatalogStatement(y);break;
                case DropLoginStatement y:this.DropLoginStatement(y);break;
                case DropRoleStatement y:this.DropRoleStatement(y);break;
                case DropUserStatement y:this.DropUserStatement(y);break;
                case DropSymmetricKeyStatement y:this.DropSymmetricKeyStatement(y);break;
                case DropAsymmetricKeyStatement y:this.DropAsymmetricKeyStatement(y);break;
                case DropCertificateStatement y:this.DropCertificateStatement(y);break;
                case DropCredentialStatement y:this.DropCredentialStatement(y);break;
                case DropSearchPropertyListStatement y:this.DropSearchPropertyListStatement(y);break;
                case DropContractStatement y:this.DropContractStatement(y);break;
                case DropEndpointStatement y:this.DropEndpointStatement(y);break;
                case DropMessageTypeStatement y:this.DropMessageTypeStatement(y);break;
                case DropRemoteServiceBindingStatement y:this.DropRemoteServiceBindingStatement(y);break;
                case DropRouteStatement y:this.DropRouteStatement(y);break;
                case DropServiceStatement y:this.DropServiceStatement(y);break;
                case DropDatabaseAuditSpecificationStatement y:this.DropDatabaseAuditSpecificationStatement(y);break;
                case DropServerAuditSpecificationStatement y:this.DropServerAuditSpecificationStatement(y);break;
                case DropServerAuditStatement y:this.DropServerAuditStatement(y);break;
                case DropResourcePoolStatement y:this.DropResourcePoolStatement(y);break;
                case DropExternalResourcePoolStatement y:this.DropExternalResourcePoolStatement(y);break;
                case DropWorkloadGroupStatement y:this.DropWorkloadGroupStatement(y);break;
                case DropBrokerPriorityStatement y:this.DropBrokerPriorityStatement(y);break;
                case DropFullTextStopListStatement y:this.DropFullTextStopListStatement(y);break;
                case DropCryptographicProviderStatement y:this.DropCryptographicProviderStatement(y);break;
                case DropEventSessionStatement y:this.DropEventSessionStatement(y);break;
                case DropAvailabilityGroupStatement y:this.DropAvailabilityGroupStatement(y);break;
                case DropFederationStatement y:this.DropFederationStatement(y);break;
            }
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropColumnMasterKeyStatement(DropColumnMasterKeyStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropColumnEncryptionKeyStatement(DropColumnEncryptionKeyStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropExternalDataSourceStatement(DropExternalDataSourceStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropExternalFileFormatStatement(DropExternalFileFormatStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropServerRoleStatement(DropServerRoleStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropPartitionFunctionStatement(DropPartitionFunctionStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropPartitionSchemeStatement(DropPartitionSchemeStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropApplicationRoleStatement(DropApplicationRoleStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropFullTextCatalogStatement(DropFullTextCatalogStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropLoginStatement(DropLoginStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropRoleStatement(DropRoleStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropUserStatement(DropUserStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropSymmetricKeyStatement(DropSymmetricKeyStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropAsymmetricKeyStatement(DropAsymmetricKeyStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropCertificateStatement(DropCertificateStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropCredentialStatement(DropCredentialStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropSearchPropertyListStatement(DropSearchPropertyListStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropContractStatement(DropContractStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropEndpointStatement(DropEndpointStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropMessageTypeStatement(DropMessageTypeStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropRemoteServiceBindingStatement(DropRemoteServiceBindingStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropRouteStatement(DropRouteStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropServiceStatement(DropServiceStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropDatabaseAuditSpecificationStatement(DropDatabaseAuditSpecificationStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropServerAuditSpecificationStatement(DropServerAuditSpecificationStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropServerAuditStatement(DropServerAuditStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropResourcePoolStatement(DropResourcePoolStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropExternalResourcePoolStatement(DropExternalResourcePoolStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropWorkloadGroupStatement(DropWorkloadGroupStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropBrokerPriorityStatement(DropBrokerPriorityStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropFullTextStopListStatement(DropFullTextStopListStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropCryptographicProviderStatement(DropCryptographicProviderStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropEventSessionStatement(DropEventSessionStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropAvailabilityGroupStatement(DropAvailabilityGroupStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropFederationStatement(DropFederationStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ColumnEncryptionKeyStatement(ColumnEncryptionKeyStatement x) {
            switch(x) {
                case CreateColumnEncryptionKeyStatement y:this.CreateColumnEncryptionKeyStatement(y);break;
                case AlterColumnEncryptionKeyStatement y:this.AlterColumnEncryptionKeyStatement(y);break;
            }
        }
        /// <summary>
        ///ColumnEncryptionKeyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateColumnEncryptionKeyStatement(CreateColumnEncryptionKeyStatement x) {
            this.Identifier(x.Name);
            this.ColumnEncryptionKeyValues(x.ColumnEncryptionKeyValues);
        }
        /// <summary>
        ///ColumnEncryptionKeyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterColumnEncryptionKeyStatement(AlterColumnEncryptionKeyStatement x) {
            this.Identifier(x.Name);
            this.ColumnEncryptionKeyValues(x.ColumnEncryptionKeyValues);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExternalTableStatement(ExternalTableStatement x) {
            switch(x) {
                case CreateExternalTableStatement y:this.CreateExternalTableStatement(y);break;
            }
        }
        /// <summary>
        ///ExternalTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateExternalTableStatement(CreateExternalTableStatement x) {
            this.SchemaObjectName(x.SchemaObjectName);
            this.SelectStatement(x.SelectStatement);
            this.Identifier(x.DataSource);
            foreach(var a in x.ExternalTableOptions)this.ExternalTableOption(a);
            foreach(var a in x.ColumnDefinitions)this.ExternalTableColumnDefinition(a);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExternalDataSourceStatement(ExternalDataSourceStatement x) {
            switch(x) {
                case CreateExternalDataSourceStatement y:this.CreateExternalDataSourceStatement(y);break;
                case AlterExternalDataSourceStatement y:this.AlterExternalDataSourceStatement(y);break;
            }
        }
        /// <summary>
        ///ExternalDataSourceStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateExternalDataSourceStatement(CreateExternalDataSourceStatement x) {
            this.Identifier(x.Name);
            this.Literal(x.Location);
            this.ExternalDataSourceOptions(x.ExternalDataSourceOptions);
        }
        /// <summary>
        ///ExternalDataSourceStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterExternalDataSourceStatement(AlterExternalDataSourceStatement x) {
            this.Identifier(x.Name);
            this.ExternalDataSourceOptions(x.ExternalDataSourceOptions);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExternalFileFormatStatement(ExternalFileFormatStatement x) {
            switch(x) {
                case CreateExternalFileFormatStatement y:this.CreateExternalFileFormatStatement(y);break;
            }
        }
        /// <summary>
        ///ExternalFileFormatStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateExternalFileFormatStatement(CreateExternalFileFormatStatement x) {
            this.Identifier(x.Name);
            this.ExternalFileFormatOptions(x.ExternalFileFormatOptions);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AssemblyStatement(AssemblyStatement x) {
            switch(x) {
                case CreateAssemblyStatement y:this.CreateAssemblyStatement(y);break;
                case AlterAssemblyStatement y:this.AlterAssemblyStatement(y);break;
            }
        }
        /// <summary>
        ///AssemblyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateAssemblyStatement(CreateAssemblyStatement x) {
            this.Identifier(x.Name);
            this.Identifier(x.Owner);
            this.AssemblyOptions(x.Options);
        }
        /// <summary>
        ///AssemblyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterAssemblyStatement(AlterAssemblyStatement x) {
            this.Identifier(x.Name);
            foreach(var a in x.AddFiles)
                this.AddFileSpec(a);
            this.Literals(x.DropFiles);
            this.ScalarExpressions(x.Parameters);
            this.AssemblyOptions(x.Options);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateXmlSchemaCollectionStatement(CreateXmlSchemaCollectionStatement x) {
            this.SchemaObjectName(x.Name);
            this.ScalarExpression(x.Expression);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterXmlSchemaCollectionStatement(AlterXmlSchemaCollectionStatement x) {
            this.SchemaObjectName(x.Name);
            this.ScalarExpression(x.Expression);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropXmlSchemaCollectionStatement(DropXmlSchemaCollectionStatement x) {
            this.SchemaObjectName(x.Name);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterTableStatement(AlterTableStatement x) {
            switch(x) {
                case AlterTableRebuildStatement y:this.AlterTableRebuildStatement(y);break;
                case AlterTableChangeTrackingModificationStatement y:this.AlterTableChangeTrackingModificationStatement(y);break;
                case AlterTableFileTableNamespaceStatement y:this.AlterTableFileTableNamespaceStatement(y);break;
                case AlterTableSetStatement y:this.AlterTableSetStatement(y);break;
                case AlterTableAddTableElementStatement y:this.AlterTableAddTableElementStatement(y);break;
                case AlterTableConstraintModificationStatement y:this.AlterTableConstraintModificationStatement(y);break;
                case AlterTableSwitchStatement y:this.AlterTableSwitchStatement(y);break;
                case AlterTableDropTableElementStatement y:this.AlterTableDropTableElementStatement(y);break;
                case AlterTableTriggerModificationStatement y:this.AlterTableTriggerModificationStatement(y);break;
                case AlterTableAlterIndexStatement y:this.AlterTableAlterIndexStatement(y);break;
                case AlterTableAlterColumnStatement y:this.AlterTableAlterColumnStatement(y);break;
            }
        }
        /// <summary>
        ///AlterTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterTableRebuildStatement(AlterTableRebuildStatement x) {
            this.SchemaObjectName(x.SchemaObjectName);
            this.IndexOptions(x.IndexOptions);
        }
        /// <summary>
        ///AlterTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterTableChangeTrackingModificationStatement(AlterTableChangeTrackingModificationStatement x) {
            this.SchemaObjectName(x.SchemaObjectName);
        }
        /// <summary>
        ///AlterTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterTableFileTableNamespaceStatement(AlterTableFileTableNamespaceStatement x) {
            this.SchemaObjectName(x.SchemaObjectName);
        }
        /// <summary>
        ///AlterTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterTableSetStatement(AlterTableSetStatement x) {
            this.SchemaObjectName(x.SchemaObjectName);
            this.TableOptions(x.Options);
        }
        /// <summary>
        ///AlterTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterTableAddTableElementStatement(AlterTableAddTableElementStatement x) {
            this.SchemaObjectName(x.SchemaObjectName);
            this.TableDefinition(x.Definition);
        }
        /// <summary>
        ///AlterTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterTableConstraintModificationStatement(AlterTableConstraintModificationStatement x) {
            this.SchemaObjectName(x.SchemaObjectName);
            this.Identifiers(x.ConstraintNames);
        }
        /// <summary>
        ///AlterTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterTableSwitchStatement(AlterTableSwitchStatement x) {
            this.SchemaObjectName(x.SchemaObjectName);
            this.SchemaObjectName(x.TargetTable);
            this.ScalarExpression(x.SourcePartitionNumber);
            this.ScalarExpression(x.TargetPartitionNumber);
            foreach(var a in x.Options)
                this.TableSwitchOption(a);
        }
        /// <summary>
        ///AlterTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterTableDropTableElementStatement(AlterTableDropTableElementStatement x) {
            this.SchemaObjectName(x.SchemaObjectName);
            foreach(var a in x.AlterTableDropTableElements)
                this.AlterTableDropTableElement(a);
        }
        /// <summary>
        ///AlterTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterTableTriggerModificationStatement(AlterTableTriggerModificationStatement x) {
            this.SchemaObjectName(x.SchemaObjectName);
            this.Identifiers(x.TriggerNames);
        }
        /// <summary>
        ///AlterTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterTableAlterIndexStatement(AlterTableAlterIndexStatement x) {
            this.SchemaObjectName(x.SchemaObjectName);
            this.IndexOptions(x.IndexOptions);
            this.Identifier(x.IndexIdentifier);
        }
        /// <summary>
        ///AlterTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterTableAlterColumnStatement(AlterTableAlterColumnStatement x) {
            this.DataTypeReference(x.DataType);
            this.IndexOptions(x.Options);
            this.Identifier(x.Collation);
            this.Identifier(x.ColumnIdentifier);
            this.ColumnEncryptionDefinition(x.Encryption);
            this.StringLiteral(x.MaskingFunction);
            this.ColumnStorageOptions(x.StorageOptions);
            this.SchemaObjectName(x.SchemaObjectName);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void EnableDisableTriggerStatement(EnableDisableTriggerStatement x) {
            this.TriggerObject(x.TriggerObject);
            this.TriggerNames(x.TriggerNames);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TryCatchStatement(TryCatchStatement x) {
            this.StatementList(x.CatchStatements);
            this.StatementList(x.TryStatements);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateTypeStatement(CreateTypeStatement x) {
            switch(x) {
                case CreateTypeUdtStatement y:this.CreateTypeUdtStatement(y);break;
                case CreateTypeUddtStatement y:this.CreateTypeUddtStatement(y);break;
                case CreateTypeTableStatement y:this.CreateTypeTableStatement(y);break;
            }
        }
        /// <summary>
        ///CreateTypeStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateTypeUdtStatement(CreateTypeUdtStatement x) {
            this.SchemaObjectName(x.Name);
            this.AssemblyName(x.AssemblyName);
        }
        /// <summary>
        ///CreateTypeStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateTypeUddtStatement(CreateTypeUddtStatement x) {
            this.SchemaObjectName(x.Name);
            this.DataTypeReference(x.DataType);
            if(x.NullableConstraint is not null) this.NullableConstraintDefinition(x.NullableConstraint);
        }
        /// <summary>
        ///CreateTypeStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateTypeTableStatement(CreateTypeTableStatement x) {
            this.SchemaObjectName(x.Name);
            this.TableDefinition(x.Definition);
            this.TableOptions(x.Options);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateSynonymStatement(CreateSynonymStatement x) {
            this.SchemaObjectName(x.Name);
            this.SchemaObjectName(x.ForName);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RouteStatement(RouteStatement x) {
            switch(x) {
                case CreateRouteStatement y:this.CreateRouteStatement(y);break;
                case AlterRouteStatement y:this.AlterRouteStatement(y);break;
            }
        }
        /// <summary>
        ///RouteStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateRouteStatement(CreateRouteStatement x) {
            this.Identifier(x.Name);
            this.Identifier(x.Owner);
            this.RouteOptions(x.RouteOptions);
        }
        /// <summary>
        ///RouteStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterRouteStatement(AlterRouteStatement x) {
            this.Identifier(x.Name);
            this.RouteOptions(x.RouteOptions);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void QueueStatement(QueueStatement x) {
            switch(x) {
                case CreateQueueStatement y:this.CreateQueueStatement(y);break;
                case AlterQueueStatement y:this.AlterQueueStatement(y);break;
            }
        }
        /// <summary>
        ///QueueStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateQueueStatement(CreateQueueStatement x) {
            this.IdentifierOrValueExpression(x.OnFileGroup);
            this.SchemaObjectName(x.Name);
            this.QueueOptions(x.QueueOptions);
        }
        /// <summary>
        ///QueueStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterQueueStatement(AlterQueueStatement x) {
            this.SchemaObjectName(x.Name);
            this.QueueOptions(x.QueueOptions);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void IndexDefinition(IndexDefinition x) {
            this.IdentifierOrValueExpression(x.FileStreamOn);
            this.BooleanExpression(x.FilterPredicate);
            this.Identifier(x.Name);
            this.FileGroupOrPartitionScheme(x.OnFileGroupOrPartitionScheme);
            this.IndexOptions(x.IndexOptions);
            this.ColumnWithSortOrders(x.Columns);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void IndexStatement(IndexStatement x) {
            switch(x) {
                case AlterIndexStatement y:this.AlterIndexStatement(y);break;
                case CreateXmlIndexStatement y:this.CreateXmlIndexStatement(y);break;
                case CreateSelectiveXmlIndexStatement y:this.CreateSelectiveXmlIndexStatement(y);break;
                case CreateIndexStatement y:this.CreateIndexStatement(y);break;
            }
        }
        /// <summary>
        ///IndexStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterIndexStatement(AlterIndexStatement x) {
            this.Identifier(x.Name);
            this.PartitionSpecifier(x.Partition);
            this.XmlNamespaces(x.XmlNamespaces);
            this.IndexOptions(x.IndexOptions);
            this.PromotedPaths(x.PromotedPaths);
        }
        /// <summary>
        ///IndexStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateXmlIndexStatement(CreateXmlIndexStatement x) {
            this.Identifier(x.Name);
            this.Identifier(x.XmlColumn);
            this.Identifier(x.SecondaryXmlIndexName);
            this.SchemaObjectName(x.OnName);
            this.IndexOptions(x.IndexOptions);
        }
        /// <summary>
        ///IndexStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateSelectiveXmlIndexStatement(CreateSelectiveXmlIndexStatement x) {
            this.Identifier(x.Name);
            this.Identifier(x.PathName);
            this.Identifier(x.UsingXmlIndexName);
            this.Identifier(x.XmlColumn);
            this.SchemaObjectName(x.OnName);
            this.XmlNamespaces(x.XmlNamespaces);
            this.PromotedPaths(x.PromotedPaths);
            this.IndexOptions(x.IndexOptions);
        }
        /// <summary>
        ///IndexStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateIndexStatement(CreateIndexStatement x) {
            this.BooleanExpression(x.FilterPredicate);
            this.IdentifierOrValueExpression(x.FileStreamOn);
            this.FileGroupOrPartitionScheme(x.OnFileGroupOrPartitionScheme);
            this.ColumnWithSortOrders(x.Columns);
            this.ColumnReferenceExpressions(x.IncludeColumns);
            this.IndexOptions(x.IndexOptions);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateFullTextIndexStatement(CreateFullTextIndexStatement x) {
            this.SchemaObjectName(x.OnName);
            this.FullTextCatalogAndFileGroup(x.CatalogAndFileGroup);
            this.FullTextIndexColumns(x.FullTextIndexColumns);
            foreach(var ScriptTokenStream in x.ScriptTokenStream){}
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateEventNotificationStatement(CreateEventNotificationStatement x){
            this.Identifier(x.Name);
            this.Literal(x.BrokerInstanceSpecifier);
            this.Literal(x.BrokerService);
            this.EventNotificationObjectScope(x.Scope);
            foreach(var a in x.EventTypeGroups)
                this.EventTypeGroupContainer(a);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MasterKeyStatement(MasterKeyStatement x) {
            switch(x) {
                case CreateMasterKeyStatement y:this.CreateMasterKeyStatement(y);break;
                case AlterMasterKeyStatement y:this.AlterMasterKeyStatement(y);break;
            }
        }
        /// <summary>
        ///MasterKeyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateMasterKeyStatement(CreateMasterKeyStatement x) {
            this.Literal(x.Password);
        }
        /// <summary>
        ///MasterKeyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterMasterKeyStatement(AlterMasterKeyStatement x) {
            this.Literal(x.Password);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ApplicationRoleStatement(ApplicationRoleStatement x) {
            this.Identifier(x.Name);
            foreach(var a in x.ApplicationRoleOptions)
                this.ApplicationRoleOption(a);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RoleStatement(RoleStatement x) {
            switch(x) {
                case CreateRoleStatement y:this.CreateRoleStatement(y);break;
                case AlterRoleStatement y:this.AlterRoleStatement(y);break;
            }
        }
        /// <summary>
        ///RoleStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateRoleStatement(CreateRoleStatement x) {
            switch(x) {
                case CreateServerRoleStatement y:this.CreateServerRoleStatement(y);break;
            }
        }
        /// <summary>
        ///CreateRoleStatement:RoleStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateServerRoleStatement(CreateServerRoleStatement x) {
            this.Identifier(x.Name);
            this.Identifier(x.Owner);
        }
        /// <summary>
        ///RoleStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterRoleStatement(AlterRoleStatement x) {
            switch(x) {
                case AlterServerRoleStatement y:this.AlterServerRoleStatement(y);break;
            }
        }
        /// <summary>
        ///AlterRoleStatement:RoleStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterServerRoleStatement(AlterServerRoleStatement x) {
            this.Identifier(x.Name);
            this.AlterRoleAction(x.Action);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void UserStatement(UserStatement x) {
            switch(x) {
                case CreateUserStatement y:this.CreateUserStatement(y);break;
                case AlterUserStatement y:this.AlterUserStatement(y);break;
            }
        }
        /// <summary>
        ///UserStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateUserStatement(CreateUserStatement x) {
            this.UserLoginOption(x.UserLoginOption);
            this.Identifier(x.Name);
            this.PrincipalOptions(x.UserOptions);
        }
        /// <summary>
        ///UserStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterUserStatement(AlterUserStatement x) {
            this.Identifier(x.Name);
            this.PrincipalOptions(x.UserOptions);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateStatisticsStatement(CreateStatisticsStatement x) {
            this.Identifier(x.Name);
            this.SchemaObjectName(x.OnName);
            this.BooleanExpression(x.FilterPredicate);
            this.StatisticsOptions(x.StatisticsOptions);
            this.ColumnReferenceExpressions(x.Columns);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void UpdateStatisticsStatement(UpdateStatisticsStatement x) {
            this.SchemaObjectName(x.SchemaObjectName);
            this.StatisticsOptions(x.StatisticsOptions);
            this.Identifiers(x.SubElements);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ReturnStatement(ReturnStatement x) {
            this.ScalarExpression(x.Expression);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DeclareCursorStatement(DeclareCursorStatement x) {
            this.CursorDefinition(x.CursorDefinition);
            this.Identifier(x.Name);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SetVariableStatement(SetVariableStatement x) {
            if (x.Identifier is not null) this.Identifier(x.Identifier);
            if(x.CursorDefinition is not null)this.CursorDefinition(x.CursorDefinition);
            this.ScalarExpression(x.Expression);
            this.VariableReference(x.Variable);
            this.ScalarExpressions(x.Parameters);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CursorStatement(CursorStatement x) {
            switch(x) {
                case OpenCursorStatement y:this.OpenCursorStatement(y);break;
                case CloseCursorStatement y:this.CloseCursorStatement(y);break;
                case DeallocateCursorStatement y:this.DeallocateCursorStatement(y);break;
                case FetchCursorStatement y:this.FetchCursorStatement(y);break;
            }
        }
        /// <summary>
        ///CursorStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OpenCursorStatement(OpenCursorStatement x) {
            this.CursorId(x.Cursor);
        }
        /// <summary>
        ///CursorStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CloseCursorStatement(CloseCursorStatement x) {
            this.CursorId(x.Cursor);
        }
        /// <summary>
        ///CursorStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DeallocateCursorStatement(DeallocateCursorStatement x) {
            this.CursorId(x.Cursor);
        }
        /// <summary>
        ///CursorStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FetchCursorStatement(FetchCursorStatement x) {
            foreach(var IntoVariable in x.IntoVariables)
                this.VariableReference(IntoVariable);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OpenSymmetricKeyStatement(OpenSymmetricKeyStatement x) {
            this.Identifier(x.Name);
            this.CryptoMechanism(x.DecryptionMechanism);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CloseSymmetricKeyStatement(CloseSymmetricKeyStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OpenMasterKeyStatement(OpenMasterKeyStatement x) {
            this.Literal(x.Password);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CloseMasterKeyStatement(CloseMasterKeyStatement x) {
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropDatabaseStatement(DropDatabaseStatement x) {
            this.Identifiers(x.Databases);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropChildObjectsStatement(DropChildObjectsStatement x) {
            switch(x) {
                case DropStatisticsStatement y:this.DropStatisticsStatement(y);break;
            }
        }
        /// <summary>
        ///DropChildObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropStatisticsStatement(DropStatisticsStatement x) {
            foreach(var a in x.Objects)
                this.ChildObjectName(a);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropIndexStatement(DropIndexStatement x) {
            foreach(var a in x.DropIndexClauses)
                this.DropIndexClauseBase(a);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropSchemaStatement(DropSchemaStatement x) {
            this.SchemaObjectName(x.Schema);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RaiseErrorLegacyStatement(RaiseErrorLegacyStatement x) {
            this.ScalarExpression(x.FirstParameter);
            this.ValueExpression(x.SecondParameter);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RaiseErrorStatement(RaiseErrorStatement x) {
            this.ScalarExpression(x.FirstParameter);
            this.ScalarExpression(x.SecondParameter);
            this.ScalarExpression(x.ThirdParameter);
            this.ScalarExpressions(x.OptionalParameters);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ThrowStatement(ThrowStatement x) {
            this.ValueExpression(x.ErrorNumber);
            this.ValueExpression(x.Message);
            this.ValueExpression(x.State);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void UseStatement(UseStatement x) {
            this.Identifier(x.DatabaseName);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void KillStatement(KillStatement x) {
            this.ScalarExpression(x.Parameter);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void KillQueryNotificationSubscriptionStatement(KillQueryNotificationSubscriptionStatement x) {
            this.Literal(x.SubscriptionId);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void KillStatsJobStatement(KillStatsJobStatement x) {
            this.ScalarExpression(x.JobId);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CheckpointStatement(CheckpointStatement x) {
            this.Literal(x.Duration);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ReconfigureStatement(ReconfigureStatement x) {
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ShutdownStatement(ShutdownStatement x) {
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SetUserStatement(SetUserStatement x) {
            this.ValueExpression(x.UserName);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TruncateTableStatement(TruncateTableStatement x) {
            this.SchemaObjectName(x.TableName);
            this.CompressionPartitionRanges(x.PartitionRanges);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SetOnOffStatement(SetOnOffStatement x) {
            switch(x) {
                case PredicateSetStatement y:this.PredicateSetStatement(y);break;
                case SetStatisticsStatement y:this.SetStatisticsStatement(y);break;
                case SetOffsetsStatement y:this.SetOffsetsStatement(y);break;
                case SetIdentityInsertStatement y:this.SetIdentityInsertStatement(y);break;
            }
        }
        /// <summary>
        ///SetOnOffStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void PredicateSetStatement(PredicateSetStatement x) {
            throw new NotSupportedException();
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SetStatisticsStatement(SetStatisticsStatement x) {
            throw new NotSupportedException();
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SetOffsetsStatement(SetOffsetsStatement x) {
            throw new NotSupportedException();
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SetIdentityInsertStatement(SetIdentityInsertStatement x) {
            this.SchemaObjectName(x.Table);
            throw new NotSupportedException();
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SetRowCountStatement(SetRowCountStatement x) {
            this.ValueExpression(x.NumberRows);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SetCommandStatement(SetCommandStatement x) {
            foreach(var a in x.Commands)
                this.SetCommand(a);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SetTransactionIsolationLevelStatement(SetTransactionIsolationLevelStatement x) {
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SetTextSizeStatement(SetTextSizeStatement x) {
            this.ScalarExpression(x.TextSize);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SetErrorLevelStatement(SetErrorLevelStatement x) {
            this.ScalarExpression(x.Level);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateDatabaseStatement(CreateDatabaseStatement x) {
            this.Identifier(x.Collation);
            this.ContainmentDatabaseOption(x.Containment);
            this.MultiPartIdentifier(x.CopyOf);
            this.Identifier(x.DatabaseName);
            this.Identifier(x.DatabaseSnapshot);
            this.CreateDatabaseStatements(x.Options);
            this.FileDeclarations(x.LogOn);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterDatabaseStatement(AlterDatabaseStatement x) {
            switch(x) {
                case AlterDatabaseCollateStatement y:this.AlterDatabaseCollateStatement(y);break;
                case AlterDatabaseRebuildLogStatement y:this.AlterDatabaseRebuildLogStatement(y);break;
                case AlterDatabaseAddFileStatement y:this.AlterDatabaseAddFileStatement(y);break;
                case AlterDatabaseAddFileGroupStatement y:this.AlterDatabaseAddFileGroupStatement(y);break;
                case AlterDatabaseRemoveFileGroupStatement y:this.AlterDatabaseRemoveFileGroupStatement(y);break;
                case AlterDatabaseRemoveFileStatement y:this.AlterDatabaseRemoveFileStatement(y);break;
                case AlterDatabaseModifyNameStatement y:this.AlterDatabaseModifyNameStatement(y);break;
                case AlterDatabaseModifyFileStatement y:this.AlterDatabaseModifyFileStatement(y);break;
                case AlterDatabaseModifyFileGroupStatement y:this.AlterDatabaseModifyFileGroupStatement(y);break;
                case AlterDatabaseSetStatement y:this.AlterDatabaseSetStatement(y);break;
            }
        }
        /// <summary>
        ///AlterDatabaseStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterDatabaseCollateStatement(AlterDatabaseCollateStatement x) {
            this.Identifier(x.DatabaseName);
            this.Identifier(x.Collation);
        }
        /// <summary>
        ///AlterDatabaseStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterDatabaseRebuildLogStatement(AlterDatabaseRebuildLogStatement x) {
            this.Identifier(x.DatabaseName);
            this.FileDeclaration(x.FileDeclaration);
        }
        /// <summary>
        ///AlterDatabaseStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterDatabaseAddFileStatement(AlterDatabaseAddFileStatement x) {
            this.Identifier(x.FileGroup);
            this.FileDeclarations(x.FileDeclarations);
        }
        /// <summary>
        ///AlterDatabaseStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterDatabaseAddFileGroupStatement(AlterDatabaseAddFileGroupStatement x) {
            this.Identifier(x.FileGroup);
            this.Identifier(x.DatabaseName);
        }
        /// <summary>
        ///AlterDatabaseStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterDatabaseRemoveFileGroupStatement(AlterDatabaseRemoveFileGroupStatement x) {
            this.Identifier(x.FileGroup);
            this.Identifier(x.DatabaseName);
        }
        /// <summary>
        ///AlterDatabaseStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterDatabaseRemoveFileStatement(AlterDatabaseRemoveFileStatement x) {
            this.Identifier(x.File);
            this.Identifier(x.DatabaseName);
        }
        /// <summary>
        ///AlterDatabaseStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterDatabaseModifyNameStatement(AlterDatabaseModifyNameStatement x) {
            this.Identifier(x.NewDatabaseName);
            this.Identifier(x.DatabaseName);
        }
        /// <summary>
        ///AlterDatabaseStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterDatabaseModifyFileStatement(AlterDatabaseModifyFileStatement x) {
            this.FileDeclaration(x.FileDeclaration);
            this.Identifier(x.DatabaseName);
        }
        /// <summary>
        ///AlterDatabaseStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterDatabaseModifyFileGroupStatement(AlterDatabaseModifyFileGroupStatement x) {
            this.Identifier(x.FileGroup);
            this.AlterDatabaseTermination(x.Termination);
            this.Identifier(x.NewFileGroupName);
        }
        /// <summary>
        ///AlterDatabaseStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterDatabaseSetStatement(AlterDatabaseSetStatement x) {
            this.Identifier(x.DatabaseName);
            this.AlterDatabaseTermination(x.Termination);
            this.CreateDatabaseStatements(x.Options);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterDatabaseScopedConfigurationStatement(AlterDatabaseScopedConfigurationStatement x) {
            switch(x) {
                case AlterDatabaseScopedConfigurationSetStatement y:this.AlterDatabaseScopedConfigurationSetStatement(y);break;
                case AlterDatabaseScopedConfigurationClearStatement y:this.AlterDatabaseScopedConfigurationClearStatement(y);break;
            }
        }
        /// <summary>
        ///AlterDatabaseScopedConfigurationStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterDatabaseScopedConfigurationSetStatement(AlterDatabaseScopedConfigurationSetStatement x) {
            this.DatabaseConfigurationSetOption(x.Option);
        }
        /// <summary>
        ///AlterDatabaseScopedConfigurationStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterDatabaseScopedConfigurationClearStatement(AlterDatabaseScopedConfigurationClearStatement x) {
            this.DatabaseConfigurationClearOption(x.Option);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateTableStatement(CreateTableStatement x) {
            this.TableDefinition(x.Definition);
            this.FederationScheme(x.FederationScheme);
            this.IdentifierOrValueExpression(x.FileStreamOn);
            this.FileGroupOrPartitionScheme(x.OnFileGroupOrPartitionScheme);
            this.SchemaObjectName(x.SchemaObjectName);
            this.SelectStatement(x.SelectStatement);
            this.IdentifierOrValueExpression(x.TextImageOn);
            this.Identifiers(x.CtasColumns);
            this.TableOptions(x.Options);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BackupStatement(BackupStatement x) {
            switch(x) {
                case BackupDatabaseStatement y:this.BackupDatabaseStatement(y);break;
                case BackupTransactionLogStatement y:this.BackupTransactionLogStatement(y);break;
            }
        }
        /// <summary>
        ///BackupStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BackupDatabaseStatement(BackupDatabaseStatement x) {
            this.IdentifierOrValueExpression(x.DatabaseName);
            this.BackupOptions(x.Options);
            this.DeviceInfos(x.Devices);
            this.MirrorToClauses(x.MirrorToClauses);
        }
        /// <summary>
        ///BackupStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BackupTransactionLogStatement(BackupTransactionLogStatement x) {
            this.IdentifierOrValueExpression(x.DatabaseName);
            this.BackupOptions(x.Options);
            this.DeviceInfos(x.Devices);
            this.MirrorToClauses(x.MirrorToClauses);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RestoreStatement(RestoreStatement x) {
            this.IdentifierOrValueExpression(x.DatabaseName);
            foreach(var a in x.Options)
                this.RestoreOption(a);
            this.DeviceInfos(x.Devices);
            foreach(var a in x.Files)
                this.BackupRestoreFileInfo(a);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BulkInsertBase(BulkInsertBase x) {
            switch(x) {
                case BulkInsertStatement y:this.BulkInsertStatement(y);break;
                case InsertBulkStatement y:this.InsertBulkStatement(y);break;
            }
        }
        /// <summary>
        ///BulkInsertBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BulkInsertStatement(BulkInsertStatement x) {
            this.IdentifierOrValueExpression(x.From);
            this.SchemaObjectName(x.To);
            this.BulkInsertOptions(x.Options);
        }
        /// <summary>
        ///BulkInsertBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void InsertBulkStatement(InsertBulkStatement x) {
            this.SchemaObjectName(x.To);
            this.BulkInsertOptions(x.Options);
            foreach(var a in x.ColumnDefinitions)
                this.InsertBulkColumnDefinition(a);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DbccStatement(DbccStatement x){
            foreach(var a in x.Options)
                this.DbccOption(a);
            foreach(var a in x.Literals)
                this.DbccNamedLiteral(a);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateAsymmetricKeyStatement(CreateAsymmetricKeyStatement x) {
            this.EncryptionSource(x.KeySource);
            this.Identifier(x.Name);
            this.Identifier(x.Owner);
            this.Literal(x.Password);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreatePartitionFunctionStatement(CreatePartitionFunctionStatement x) {
            this.Identifier(x.Name);
            this.ScalarExpressions(x.BoundaryValues);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreatePartitionSchemeStatement(CreatePartitionSchemeStatement x) {
            this.Identifier(x.Name);
            this.Identifier(x.PartitionFunction);
            this.IdentifierOrValueExpressions(x.FileGroups);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RemoteServiceBindingStatementBase(RemoteServiceBindingStatementBase x) {
            switch(x) {
                case CreateRemoteServiceBindingStatement y:this.CreateRemoteServiceBindingStatement(y);break;
                case AlterRemoteServiceBindingStatement y:this.AlterRemoteServiceBindingStatement(y);break;
            }
        }
        /// <summary>
        ///RemoteServiceBindingStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateRemoteServiceBindingStatement(CreateRemoteServiceBindingStatement x) {
            this.Identifier(x.Owner);
            this.Identifier(x.Name);
            this.Literal(x.Service);
            this.RemoteServiceBindingOptions(x.Options);
        }
        /// <summary>
        ///RemoteServiceBindingStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterRemoteServiceBindingStatement(AlterRemoteServiceBindingStatement x) {
            this.Identifier(x.Name);
            this.RemoteServiceBindingOptions(x.Options);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CertificateStatementBase(CertificateStatementBase x) {
            switch(x) {
                case CreateCertificateStatement y:this.CreateCertificateStatement(y);break;
                case AlterCertificateStatement y:this.AlterCertificateStatement(y);break;
                case BackupCertificateStatement y:this.BackupCertificateStatement(y);break;
            }
        }
        /// <summary>
        ///CertificateStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateCertificateStatement(CreateCertificateStatement x) {
            this.Identifier(x.Owner);
            this.Identifier(x.Name);
            this.EncryptionSource(x.CertificateSource);
            this.Literal(x.EncryptionPassword);
            this.Literal(x.DecryptionPassword);
            foreach(var CertificateOption in x.CertificateOptions)
                this.CertificateOption(CertificateOption);
        }
        /// <summary>
        ///CertificateStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterCertificateStatement(AlterCertificateStatement x) {
            this.Literal(x.AttestedBy);
            this.Identifier(x.Name);
            this.Literal(x.EncryptionPassword);
            this.Literal(x.DecryptionPassword);
            this.Literal(x.PrivateKeyPath);
        }
        /// <summary>
        ///CertificateStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BackupCertificateStatement(BackupCertificateStatement x) {
            this.Literal(x.File);
            this.Identifier(x.Name);
            this.Literal(x.EncryptionPassword);
            this.Literal(x.DecryptionPassword);
            this.Literal(x.PrivateKeyPath);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateContractStatement(CreateContractStatement x) {
            foreach(var a in x.Messages)
                this.ContractMessage(a);
            this.Identifier(x.Name);
            this.Identifier(x.Owner);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CredentialStatement(CredentialStatement x) {
            switch(x) {
                case CreateCredentialStatement y:this.CreateCredentialStatement(y);break;
                case AlterCredentialStatement y:this.AlterCredentialStatement(y);break;
            }
        }
        /// <summary>
        ///CredentialStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateCredentialStatement(CreateCredentialStatement x) {
            this.Identifier(x.CryptographicProviderName);
            this.Identifier(x.Name);
            this.Literal(x.Identity);
        }
        /// <summary>
        ///CredentialStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterCredentialStatement(AlterCredentialStatement x) {
            this.Identifier(x.Name);
            this.Literal(x.Identity);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MessageTypeStatementBase(MessageTypeStatementBase x) {
            switch(x) {
                case CreateMessageTypeStatement y:this.CreateMessageTypeStatement(y);break;
                case AlterMessageTypeStatement y:this.AlterMessageTypeStatement(y);break;
            }
        }
        /// <summary>
        ///MessageTypeStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateMessageTypeStatement(CreateMessageTypeStatement x) {
            this.Identifier(x.Owner);
            this.Identifier(x.Name);
            this.SchemaObjectName(x.XmlSchemaCollectionName);
        }
        /// <summary>
        ///MessageTypeStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterMessageTypeStatement(AlterMessageTypeStatement x) {
            this.Identifier(x.Name);
            this.SchemaObjectName(x.XmlSchemaCollectionName);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateAggregateStatement(CreateAggregateStatement x) {
            this.AssemblyName(x.AssemblyName);
            this.SchemaObjectName(x.Name);
            this.DataTypeReference(x.ReturnType);
            this.ProcedureParameters(x.Parameters);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterCreateEndpointStatementBase(AlterCreateEndpointStatementBase x) {
            switch(x) {
                case CreateEndpointStatement y:this.CreateEndpointStatement(y);break;
                case AlterEndpointStatement y:this.AlterEndpointStatement(y);break;
            }
        }
        /// <summary>
        ///AlterCreateEndpointStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateEndpointStatement(CreateEndpointStatement x) {
            this.Identifier(x.Name);
            this.EndpointAffinity(x.Affinity);
            this.PayloadOptions(x.PayloadOptions);
            this.EndpointProtocolOptions(x.ProtocolOptions);
        }
        /// <summary>
        ///AlterCreateEndpointStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterEndpointStatement(AlterEndpointStatement x) {
            this.Identifier(x.Name);
            this.EndpointAffinity(x.Affinity);
            this.PayloadOptions(x.PayloadOptions);
            this.EndpointProtocolOptions(x.ProtocolOptions);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SymmetricKeyStatement(SymmetricKeyStatement x) {
            switch(x) {
                case CreateSymmetricKeyStatement y:this.CreateSymmetricKeyStatement(y);break;
                case AlterSymmetricKeyStatement y:this.AlterSymmetricKeyStatement(y);break;
            }
        }
        /// <summary>
        ///SymmetricKeyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateSymmetricKeyStatement(CreateSymmetricKeyStatement x) {
            this.Identifier(x.Owner);
            this.Identifier(x.Provider);
            this.Identifier(x.Name);
            this.CryptoMechanisms(x.EncryptingMechanisms);
            this.KeyOptions(x.KeyOptions);
        }
        /// <summary>
        ///SymmetricKeyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterSymmetricKeyStatement(AlterSymmetricKeyStatement x) {
            this.Identifier(x.Name);
            this.CryptoMechanisms(x.EncryptingMechanisms);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FullTextCatalogStatement(FullTextCatalogStatement x) {
            switch(x) {
                case CreateFullTextCatalogStatement y:this.CreateFullTextCatalogStatement(y);break;
                case AlterFullTextCatalogStatement y:this.AlterFullTextCatalogStatement(y);break;
            }
        }
        /// <summary>
        ///FullTextCatalogStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateFullTextCatalogStatement(CreateFullTextCatalogStatement x) {
            this.Identifier(x.Owner);
            this.Identifier(x.FileGroup);
            this.Identifier(x.Name);
            this.Literal(x.Path);
            this.FullTextCatalogOptions(x.Options);
        }
        /// <summary>
        ///FullTextCatalogStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterFullTextCatalogStatement(AlterFullTextCatalogStatement x) {
            this.Identifier(x.Name);
            this.FullTextCatalogOptions(x.Options);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterCreateServiceStatementBase(AlterCreateServiceStatementBase x) {
            switch(x) {
                case CreateServiceStatement y:this.CreateServiceStatement(y);break;
                case AlterServiceStatement y:this.AlterServiceStatement(y);break;
            }
        }
        /// <summary>
        ///AlterCreateServiceStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateServiceStatement(CreateServiceStatement x) {
            this.Identifier(x.Owner);
            this.Identifier(x.Name);
        }
        /// <summary>
        ///AlterCreateServiceStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterServiceStatement(AlterServiceStatement x) {
            this.Identifier(x.Name);
            this.SchemaObjectName(x.QueueName);
            foreach(var ServiceContract in x.ServiceContracts)
                this.ServiceContract(ServiceContract);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropFullTextIndexStatement(DropFullTextIndexStatement x) {
            this.SchemaObjectName(x.TableName);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropTypeStatement(DropTypeStatement x) {
            this.SchemaObjectName(x.Name);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropMasterKeyStatement(DropMasterKeyStatement x) {
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterPartitionFunctionStatement(AlterPartitionFunctionStatement x) {
            this.ScalarExpression(x.Boundary);
            this.Identifier(x.Name);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterPartitionSchemeStatement(AlterPartitionSchemeStatement x) {
            this.IdentifierOrValueExpression(x.FileGroup);
            this.Identifier(x.Name);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterFullTextIndexStatement(AlterFullTextIndexStatement x) {
            this.AlterFullTextIndexAction(x.Action);
            this.SchemaObjectName(x.OnName);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateSearchPropertyListStatement(CreateSearchPropertyListStatement x) {
            this.Identifier(x.Name);
            this.Identifier(x.Owner);
            this.MultiPartIdentifier(x.SourceSearchPropertyList);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterSearchPropertyListStatement(AlterSearchPropertyListStatement x) {
            this.SearchPropertyListAction(x.Action);
            this.Identifier(x.Name);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateLoginStatement(CreateLoginStatement x) {
            this.Identifier(x.Name);
            this.CreateLoginSource(x.Source);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterLoginStatement(AlterLoginStatement x) {
            switch(x) {
                case AlterLoginOptionsStatement y:this.AlterLoginOptionsStatement(y);break;
                case AlterLoginEnableDisableStatement y:this.AlterLoginEnableDisableStatement(y);break;
                case AlterLoginAddDropCredentialStatement y:this.AlterLoginAddDropCredentialStatement(y);break;
            }
        }
        /// <summary>
        ///AlterLoginStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterLoginOptionsStatement(AlterLoginOptionsStatement x) {
            this.Identifier(x.Name);
            this.PrincipalOptions(x.Options);
        }
        /// <summary>
        ///AlterLoginStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterLoginEnableDisableStatement(AlterLoginEnableDisableStatement x) {
            this.Identifier(x.Name);
        }
        /// <summary>
        ///AlterLoginStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterLoginAddDropCredentialStatement(AlterLoginAddDropCredentialStatement x) {
            this.Identifier(x.CredentialName);
            this.Identifier(x.Name);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RevertStatement(RevertStatement x) {
            this.ScalarExpression(x.Cookie);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropQueueStatement(DropQueueStatement x) {
            this.SchemaObjectName(x.Name);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SignatureStatementBase(SignatureStatementBase x) {
            switch(x) {
                case AddSignatureStatement y:this.AddSignatureStatement(y);break;
                case DropSignatureStatement y:this.DropSignatureStatement(y);break;
            }
        }
        /// <summary>
        ///SignatureStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AddSignatureStatement(AddSignatureStatement x){
            this.SchemaObjectName(x.Element);
            this.CryptoMechanisms(x.Cryptos);
        }
        /// <summary>
        ///SignatureStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropSignatureStatement(DropSignatureStatement x) {
            this.SchemaObjectName(x.Element);
            this.CryptoMechanisms(x.Cryptos);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropEventNotificationStatement(DropEventNotificationStatement x) {
            this.EventNotificationObjectScope(x.Scope);
            this.Identifiers(x.Notifications);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExecuteAsStatement(ExecuteAsStatement x) {
            this.ExecuteContext(x.ExecuteContext);
            this.VariableReference(x.Cookie);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void EndConversationStatement(EndConversationStatement x) {
            this.ScalarExpression(x.Conversation);
            this.ValueExpression(x.ErrorCode);
            this.ValueExpression(x.ErrorDescription);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MoveConversationStatement(MoveConversationStatement x) {
            this.ScalarExpression(x.Group);
            this.ScalarExpression(x.Conversation);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void WaitForSupportedStatement(WaitForSupportedStatement x) {
            switch(x) {
                case GetConversationGroupStatement y:this.GetConversationGroupStatement(y);break;
                case ReceiveStatement y:this.ReceiveStatement(y);break;
            }
        }
        /// <summary>
        ///WaitForSupportedStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void GetConversationGroupStatement(GetConversationGroupStatement x) {
            this.SchemaObjectName(x.Queue);
            this.VariableReference(x.GroupId);
        }
        /// <summary>
        ///WaitForSupportedStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ReceiveStatement(ReceiveStatement x) {
            this.VariableTableReference(x.Into);
            this.SchemaObjectName(x.Queue);
            this.ScalarExpression(x.Top);
            this.SelectElements(x.SelectElements);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SendStatement(SendStatement x) {
            this.ScalarExpression(x.MessageBody);
            this.IdentifierOrValueExpression(x.MessageTypeName);
            this.ScalarExpressions(x.ConversationHandles);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterSchemaStatement(AlterSchemaStatement x) {
            this.Identifier(x.Name);
            this.SchemaObjectName(x.ObjectName);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterAsymmetricKeyStatement(AlterAsymmetricKeyStatement x) {
            this.Identifier(x.Name);
            this.Literal(x.AttestedBy);
            this.Literal(x.EncryptionPassword);
            this.Literal(x.DecryptionPassword);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterServiceMasterKeyStatement(AlterServiceMasterKeyStatement x) {
            this.Literal(x.Password);
            this.Literal(x.Account);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BeginConversationTimerStatement(BeginConversationTimerStatement x) {
            this.ScalarExpression(x.Handle);
            this.ScalarExpression(x.Timeout);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BeginDialogStatement(BeginDialogStatement x) {
            foreach(var Option in x.Options)
                this.DialogOption(Option);
            this.IdentifierOrValueExpression(x.ContractName);
            this.VariableReference(x.Handle);
            this.IdentifierOrValueExpression(x.InitiatorServiceName);
            this.ValueExpression(x.InstanceSpec);
            this.ValueExpression(x.TargetServiceName);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BackupRestoreMasterKeyStatementBase(BackupRestoreMasterKeyStatementBase x) {
            switch(x) {
                case BackupServiceMasterKeyStatement y:this.BackupServiceMasterKeyStatement(y);break;
                case RestoreServiceMasterKeyStatement y:this.RestoreServiceMasterKeyStatement(y);break;
                case BackupMasterKeyStatement y:this.BackupMasterKeyStatement(y);break;
                case RestoreMasterKeyStatement y:this.RestoreMasterKeyStatement(y);break;
            }
        }
        /// <summary>
        ///BackupRestoreMasterKeyStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BackupServiceMasterKeyStatement(BackupServiceMasterKeyStatement x){
            this.Literal(x.File);
            this.Literal(x.Password);
        }
        /// <summary>
        ///BackupRestoreMasterKeyStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RestoreServiceMasterKeyStatement(RestoreServiceMasterKeyStatement x) {
            this.Literal(x.File);
            this.Literal(x.Password);
        }
        /// <summary>
        ///BackupRestoreMasterKeyStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BackupMasterKeyStatement(BackupMasterKeyStatement x) {
            this.Literal(x.File);
            this.Literal(x.Password);
        }
        /// <summary>
        ///BackupRestoreMasterKeyStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RestoreMasterKeyStatement(RestoreMasterKeyStatement x) {
            this.Literal(x.EncryptionPassword);
            this.Literal(x.File);
            this.Literal(x.Password);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TSqlStatementSnippet(TSqlStatementSnippet x) {
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AuditSpecificationStatement(AuditSpecificationStatement x) {
            switch(x) {
                case CreateDatabaseAuditSpecificationStatement y:this.CreateDatabaseAuditSpecificationStatement(y);break;
                case AlterDatabaseAuditSpecificationStatement y:this.AlterDatabaseAuditSpecificationStatement(y);break;
                case CreateServerAuditSpecificationStatement y:this.CreateServerAuditSpecificationStatement(y);break;
                case AlterServerAuditSpecificationStatement y:this.AlterServerAuditSpecificationStatement(y);break;
            }
        }
        /// <summary>
        ///AuditSpecificationStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateDatabaseAuditSpecificationStatement(CreateDatabaseAuditSpecificationStatement x) {
            this.Identifier(x.AuditName);
            this.Identifier(x.SpecificationName);
            this.AuditSpecificationParts(x.Parts);
        }
        /// <summary>
        ///AuditSpecificationStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterDatabaseAuditSpecificationStatement(AlterDatabaseAuditSpecificationStatement x) {
            this.Identifier(x.AuditName);
            this.Identifier(x.SpecificationName);
            this.AuditSpecificationParts(x.Parts);
        }
        /// <summary>
        ///AuditSpecificationStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateServerAuditSpecificationStatement(CreateServerAuditSpecificationStatement x) {
            this.Identifier(x.AuditName);
            this.Identifier(x.SpecificationName);
            this.AuditSpecificationParts(x.Parts);
        }
        /// <summary>
        ///AuditSpecificationStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterServerAuditSpecificationStatement(AlterServerAuditSpecificationStatement x) {
            this.Identifier(x.AuditName);
            this.Identifier(x.SpecificationName);
            this.AuditSpecificationParts(x.Parts);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ServerAuditStatement(ServerAuditStatement x) {
            switch(x) {
                case CreateServerAuditStatement y:this.CreateServerAuditStatement(y);break;
                case AlterServerAuditStatement y:this.AlterServerAuditStatement(y);break;
            }
        }
        /// <summary>
        ///ServerAuditStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateServerAuditStatement(CreateServerAuditStatement x) {
            this.Identifier(x.AuditName);
            this.AuditTarget(x.AuditTarget);
            this.BooleanExpression(x.PredicateExpression);
            foreach(var Option in x.Options)
                this.AuditOption(Option);
        }
        /// <summary>
        ///ServerAuditStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterServerAuditStatement(AlterServerAuditStatement x) {
            this.Identifier(x.NewName);
            this.Identifier(x.AuditName);
            this.AuditTarget(x.AuditTarget);
            this.BooleanExpression(x.PredicateExpression);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DatabaseEncryptionKeyStatement(DatabaseEncryptionKeyStatement x) {
            switch(x) {
                case CreateDatabaseEncryptionKeyStatement y:this.CreateDatabaseEncryptionKeyStatement(y);break;
                case AlterDatabaseEncryptionKeyStatement y:this.AlterDatabaseEncryptionKeyStatement(y);break;
            }
        }
        /// <summary>
        ///DatabaseEncryptionKeyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateDatabaseEncryptionKeyStatement(CreateDatabaseEncryptionKeyStatement x) {
            this.CryptoMechanism(x.Encryptor);
        }
        /// <summary>
        ///DatabaseEncryptionKeyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterDatabaseEncryptionKeyStatement(AlterDatabaseEncryptionKeyStatement x) {
            this.CryptoMechanism(x.Encryptor);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropDatabaseEncryptionKeyStatement(DropDatabaseEncryptionKeyStatement x) {

        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ResourcePoolStatement(ResourcePoolStatement x) {
            switch(x) {
                case CreateResourcePoolStatement y:this.CreateResourcePoolStatement(y);break;
                case AlterResourcePoolStatement y:this.AlterResourcePoolStatement(y);break;
            }
        }
        /// <summary>
        ///ResourcePoolStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateResourcePoolStatement(CreateResourcePoolStatement x) {
            this.Identifier(x.Name);
            this.ResourcePoolParameters(x.ResourcePoolParameters);
        }
        /// <summary>
        ///ResourcePoolStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterResourcePoolStatement(AlterResourcePoolStatement x) {
            this.Identifier(x.Name);
            this.ResourcePoolParameters(x.ResourcePoolParameters);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExternalResourcePoolStatement(ExternalResourcePoolStatement x) {
            switch(x) {
                case CreateExternalResourcePoolStatement y:this.CreateExternalResourcePoolStatement(y);break;
                case AlterExternalResourcePoolStatement y:this.AlterExternalResourcePoolStatement(y);break;
            }
        }
        /// <summary>
        ///ExternalResourcePoolStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateExternalResourcePoolStatement(CreateExternalResourcePoolStatement x) {
            this.Identifier(x.Name);
            this.ExternalResourcePoolParameters(x.ExternalResourcePoolParameters);
        }
        /// <summary>
        ///ExternalResourcePoolStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterExternalResourcePoolStatement(AlterExternalResourcePoolStatement x) {
            this.Identifier(x.Name);
            this.ExternalResourcePoolParameters(x.ExternalResourcePoolParameters);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void WorkloadGroupStatement(WorkloadGroupStatement x) {
            switch(x) {
                case CreateWorkloadGroupStatement y:this.CreateWorkloadGroupStatement(y);break;
                case AlterWorkloadGroupStatement y:this.AlterWorkloadGroupStatement(y);break;
            }
        }
        /// <summary>
        ///WorkloadGroupStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateWorkloadGroupStatement(CreateWorkloadGroupStatement x) {
            this.Identifier(x.Name);
            this.Identifier(x.ExternalPoolName);
            this.WorkloadGroupParameters(x.WorkloadGroupParameters);
        }
        /// <summary>
        ///WorkloadGroupStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterWorkloadGroupStatement(AlterWorkloadGroupStatement x) {
            this.Identifier(x.Name);
            this.Identifier(x.ExternalPoolName);
            this.Identifier(x.PoolName);
            this.WorkloadGroupParameters(x.WorkloadGroupParameters);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BrokerPriorityStatement(BrokerPriorityStatement x) {
            switch(x) {
                case CreateBrokerPriorityStatement y:this.CreateBrokerPriorityStatement(y);break;
                case AlterBrokerPriorityStatement y:this.AlterBrokerPriorityStatement(y);break;
            }
        }
        /// <summary>
        ///BrokerPriorityStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateBrokerPriorityStatement(CreateBrokerPriorityStatement x) {
            this.Identifier(x.Name);
            this.BrokerPriorityParameters(x.BrokerPriorityParameters);
        }
        /// <summary>
        ///BrokerPriorityStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterBrokerPriorityStatement(AlterBrokerPriorityStatement x) {
            this.Identifier(x.Name);
            this.BrokerPriorityParameters(x.BrokerPriorityParameters);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateFullTextStopListStatement(CreateFullTextStopListStatement x) {
            this.Identifier(x.DatabaseName);
            this.Identifier(x.Name);
            this.Identifier(x.Owner);
            this.Identifier(x.SourceStopListName);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterFullTextStopListStatement(AlterFullTextStopListStatement x) {
            this.FullTextStopListAction(x.Action);
            this.Identifier(x.Name);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateCryptographicProviderStatement(CreateCryptographicProviderStatement x) {
            this.Literal(x.File);
            this.Identifier(x.Name);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterCryptographicProviderStatement(AlterCryptographicProviderStatement x) {
            this.Literal(x.File);
            this.Identifier(x.Name);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void EventSessionStatement(EventSessionStatement x) {
            switch(x) {
                case CreateEventSessionStatement y:this.CreateEventSessionStatement(y);break;
                case AlterEventSessionStatement y:this.AlterEventSessionStatement(y);break;
            }
        }
        /// <summary>
        ///EventSessionStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateEventSessionStatement(CreateEventSessionStatement x) {
            this.Identifier(x.Name);
            this.SessionOptions(x.SessionOptions);
            this.TargetDeclarations(x.TargetDeclarations);
            this.EventDeclarations(x.EventDeclarations);
        }
        /// <summary>
        ///EventSessionStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterEventSessionStatement(AlterEventSessionStatement x) {
            this.Identifier(x.Name);
            this.EventSessionObjectNames(x.DropEventDeclarations);
            this.EventSessionObjectNames(x.DropTargetDeclarations);
            this.SessionOptions(x.SessionOptions);
            this.EventDeclarations(x.EventDeclarations);
            this.TargetDeclarations(x.TargetDeclarations);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterResourceGovernorStatement(AlterResourceGovernorStatement x) {
            this.SchemaObjectName(x.ClassifierFunction);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateSpatialIndexStatement(CreateSpatialIndexStatement x) {
            this.Identifier(x.Name);
            this.SchemaObjectName(x.Object);
            this.IdentifierOrValueExpression(x.OnFileGroup);
            foreach(var SpatialIndexOption in x.SpatialIndexOptions)
                this.SpatialIndexOption(SpatialIndexOption);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterServerConfigurationStatement(AlterServerConfigurationStatement x) {
            foreach(var ProcessAffinityRange in x.ProcessAffinityRanges)
                this.ProcessAffinityRange(ProcessAffinityRange);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterServerConfigurationSetBufferPoolExtensionStatement(AlterServerConfigurationSetBufferPoolExtensionStatement x) {
            this.AlterServerConfigurationBufferPoolExtensionOptions(x.Options);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterServerConfigurationSetDiagnosticsLogStatement(AlterServerConfigurationSetDiagnosticsLogStatement x) {
            foreach(var a in x.Options)
                this.AlterServerConfigurationDiagnosticsLogOption(a);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterServerConfigurationSetFailoverClusterPropertyStatement(AlterServerConfigurationSetFailoverClusterPropertyStatement x) {
            foreach(var a in x.Options)
                this.AlterServerConfigurationFailoverClusterPropertyOption(a);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterServerConfigurationSetHadrClusterStatement(AlterServerConfigurationSetHadrClusterStatement x) {
            foreach(var a in x.Options)
                this.AlterServerConfigurationHadrClusterOption(a);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterServerConfigurationSetSoftNumaStatement(AlterServerConfigurationSetSoftNumaStatement x) {
            foreach(var a in x.Options)
                this.AlterServerConfigurationSoftNumaOption(a);
        }

        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AvailabilityGroupStatement(AvailabilityGroupStatement x) {
            switch(x) {
                case CreateAvailabilityGroupStatement y:this.CreateAvailabilityGroupStatement(y);break;
                case AlterAvailabilityGroupStatement y:this.AlterAvailabilityGroupStatement(y);break;
            }
        }
        /// <summary>
        ///AvailabilityGroupStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateAvailabilityGroupStatement(CreateAvailabilityGroupStatement x) {
            this.Identifier(x.Name);
            foreach(var a in x.Options)
                this.AvailabilityGroupOption(a);
            foreach(var a in x.Replicas)
                this.AvailabilityReplica(a);
            this.Identifiers(x.Databases);
        }
        /// <summary>
        ///AvailabilityGroupStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterAvailabilityGroupStatement(AlterAvailabilityGroupStatement x) {
            this.AlterAvailabilityGroupAction(x.Action);
            this.Identifier(x.Name);
            this.Identifiers(x.Databases);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateFederationStatement(CreateFederationStatement x) {
            this.Identifier(x.DistributionName);
            this.Identifier(x.Name);
            this.DataTypeReference(x.DataType);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterFederationStatement(AlterFederationStatement x) {
            this.ScalarExpression(x.Boundary);
            this.Identifier(x.DistributionName);
            this.Identifier(x.Name);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void UseFederationStatement(UseFederationStatement x) {
            this.ScalarExpression(x.Value);
            this.Identifier(x.DistributionName);
            this.Identifier(x.FederationName);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DiskStatement(DiskStatement x) {
            foreach(var a in x.Options)
                this.DiskStatementOption(a);
        }
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateColumnStoreIndexStatement(CreateColumnStoreIndexStatement x) {
            this.BooleanExpression(x.FilterPredicate);
            this.ColumnReferenceExpressions(x.Columns);
            this.IndexOptions(x.IndexOptions);
            this.IndexOptions(x.IndexOptions);
            this.Identifier(x.Name);
            this.FileGroupOrPartitionScheme(x.OnFileGroupOrPartitionScheme);
            this.SchemaObjectName(x.OnName);
            this.ColumnReferenceExpressions(x.OrderedColumns);
        }
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExecuteOption(ExecuteOption x) {
            switch(x) {
                case ResultSetsExecuteOption y:this.ResultSetsExecuteOption(y);break;
            }
        }
        /// <summary>
        /// ExecuteOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ResultSetsExecuteOption(ResultSetsExecuteOption x) {
            foreach(var a in x.Definitions)
                this.ResultSetDefinition(a);
        }
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ResultSetDefinition(ResultSetDefinition x) {
            switch(x) {
                case InlineResultSetDefinition y:this.InlineResultSetDefinition(y);break;
                case SchemaObjectResultSetDefinition y:this.SchemaObjectResultSetDefinition(y);break;
            }
        }
        /// <summary>
        /// ResultSetDefinition:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void InlineResultSetDefinition(InlineResultSetDefinition x) {
            foreach(var a in x.ResultColumnDefinitions)
                this.ResultColumnDefinition(a);
        }
        /// <summary>
        /// ResultSetDefinition:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SchemaObjectResultSetDefinition(SchemaObjectResultSetDefinition x) {
            this.SchemaObjectName(x.Name);
        }
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ResultColumnDefinition(ResultColumnDefinition x) {
            if(x.Nullable is not null) this.NullableConstraintDefinition(x.Nullable);
            this.ColumnDefinitionBase(x.ColumnDefinition);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExecuteSpecification(ExecuteSpecification x) {
            this.ExecutableEntity(x.ExecutableEntity);
            this.ExecuteContext(x.ExecuteContext);
            this.Identifier(x.LinkedServer);
            this.VariableReference(x.Variable);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExecuteContext(ExecuteContext x) {
            this.ScalarExpression(x.Principal);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExecuteParameter(ExecuteParameter x) {
            this.ScalarExpression(x.ParameterValue);
            this.VariableReference(x.Variable);
            this.ScalarExpression(x.ParameterValue);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExecutableEntity(ExecutableEntity x) {
            switch(x) {
                case ExecutableProcedureReference y:this.ExecutableProcedureReference(y);break;
                case ExecutableStringList y:this.ExecutableStringList(y);break;
            }
        }
        /// <summary>
        ///ExecutableEntity:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExecutableProcedureReference(ExecutableProcedureReference x) {
            if(x.AdHocDataSource is not null)this.AdHocDataSource(x.AdHocDataSource);
            this.ProcedureReferenceName(x.ProcedureReference);
            this.ExecuteParameters(x.Parameters);
        }
        /// <summary>
        ///ExecutableEntity:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExecutableStringList(ExecutableStringList x) {
            this.ValueExpressions(x.Strings);
            this.ExecuteParameters(x.Parameters);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ProcedureReferenceName(ProcedureReferenceName x) {
            this.ProcedureReference(x.ProcedureReference);
            if(x.ProcedureVariable is not null)this.VariableReference(x.ProcedureVariable);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AdHocDataSource(AdHocDataSource x) {
            this.StringLiteral(x.InitString);
            this.StringLiteral(x.ProviderName);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ViewOption(ViewOption x) {
            //x.OptionKind=
            //create view Person.vStateProvinceCountryRegion
            //    with schemabinding
            //    as
            //schemabindingならこのビューを構成するオブジェクトに変更があったらsp_refreshviewが実行される
            //this.SQL取得(x)	"schemabinding"	string
            //switch(x.OptionKind) {
            //    case ViewOptionKind.ViewMetadata:break;
            //        case ViewOptionKind.Encryption:break;//ENCRYPTION
            //        case 
            //オプション設定なので基本虫
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TriggerObject(TriggerObject x) {
            this.SchemaObjectName(x.Name);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TriggerOption(TriggerOption x) {
            switch(x) {
                case ExecuteAsTriggerOption y:this.ExecuteAsTriggerOption(y);break;
            }
        }
        /// <summary>
        ///TriggerOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExecuteAsTriggerOption(ExecuteAsTriggerOption x) {
            this.ExecuteAsClause(x.ExecuteAsClause);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TriggerAction(TriggerAction x) {
            this.EventTypeGroupContainer(x.EventTypeGroup);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ProcedureReference(ProcedureReference x) { 
            this.SchemaObjectName(x.Name);
            this.Literal(x.Number);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MethodSpecifier(MethodSpecifier x) { 
            this.Identifier(x.AssemblyName);
            this.Identifier(x.ClassName);
            this.Identifier(x.MethodName);
            //if(x.AssemblyName is not null)this.Identifier(x.AssemblyName);
            //if(x.ClassName is not null)this.Identifier(x.ClassName);
            //if(x.MethodName is not null)this.Identifier(x.MethodName);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ProcedureOption(ProcedureOption x) {
            switch(x) {
                case ExecuteAsProcedureOption y:this.ExecuteAsProcedureOption(y);break;
            }
        }
        /// <summary>
        ///ProcedureOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExecuteAsProcedureOption(ExecuteAsProcedureOption x) {
            this.ExecuteAsClause(x.ExecuteAs);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FunctionOption(FunctionOption x) {
            switch(x) {
                case InlineFunctionOption y:this.InlineFunctionOption(y);break;
                case ExecuteAsFunctionOption y:this.ExecuteAsFunctionOption(y);break;
            }
        }
        /// <summary>
        ///FunctionOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void InlineFunctionOption(InlineFunctionOption x) {
            throw new NotSupportedException();
        }
        /// <summary>
        ///FunctionOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExecuteAsFunctionOption(ExecuteAsFunctionOption x) {
            this.ExecuteAsClause(x.ExecuteAs);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void XmlNamespaces(XmlNamespaces x) {
            foreach(var a in x.XmlNamespacesElements)this.XmlNamespacesElement(a);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void XmlNamespacesElement(XmlNamespacesElement x) {
            switch(x) {
                case XmlNamespacesDefaultElement y:this.XmlNamespacesDefaultElement(y);break;
                case XmlNamespacesAliasElement y:this.XmlNamespacesAliasElement(y);break;
            }
        }
        /// <summary>
        ///XmlNamespacesElement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void XmlNamespacesDefaultElement(XmlNamespacesDefaultElement x) {
            throw new NotSupportedException();
        }
        /// <summary>
        ///XmlNamespacesElement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void XmlNamespacesAliasElement(XmlNamespacesAliasElement x){
            this.Identifier(x.Identifier);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CommonTableExpression(CommonTableExpression x) {
            this.SwitchIdentifier(x.ExpressionName);
            this.Identifiers(x.Columns);
            this.QueryExpression(x.QueryExpression);
        }
        /// <summary>
        /// WITH共通式
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void WithCtesAndXmlNamespaces(WithCtesAndXmlNamespaces x) {
            this.ValueExpression(x.ChangeTrackingContext);
            foreach(var a in x.CommonTableExpressions)this.CommonTableExpression(a);
            this.XmlNamespaces(x.XmlNamespaces);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FunctionReturnType(FunctionReturnType x) {
            switch(x) {
                case TableValuedFunctionReturnType y:this.TableValuedFunctionReturnType(y);break;
                case ScalarFunctionReturnType y:this.ScalarFunctionReturnType(y);break;
                case SelectFunctionReturnType y:this.SelectFunctionReturnType(y);break;
            }
        }
        /// <summary>
        ///FunctionReturnType:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableValuedFunctionReturnType(TableValuedFunctionReturnType x) {
            this.DeclareTableVariableBody(x.DeclareTableVariableBody);
        }
        /// <summary>
        ///FunctionReturnType:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ScalarFunctionReturnType(ScalarFunctionReturnType x) {
            this.DataTypeReference(x.DataType);
        }
        /// <summary>
        ///FunctionReturnType:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SelectFunctionReturnType(SelectFunctionReturnType x){
            this.SelectStatement(x.SelectStatement);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DataTypeReference(DataTypeReference x) {
            switch(x) {
                case ParameterizedDataTypeReference y:this.ParameterizedDataTypeReference(y);break;
                case XmlDataTypeReference y:this.XmlDataTypeReference(y);break;
            }
        }
        /// <summary>
        ///DataTypeReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ParameterizedDataTypeReference(ParameterizedDataTypeReference x) {
            switch(x) {
                case SqlDataTypeReference y:this.SqlDataTypeReference(y);break;
                case UserDataTypeReference y:this.UserDataTypeReference(y);break;
            }
        }
        /// <summary>
        ///ParameterizedDataTypeReference:DataTypeReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SqlDataTypeReference(SqlDataTypeReference x) {
            this.SwitchSchemaObjectName(x.Name);
            this.Literals(x.Parameters);
        }
        /// <summary>
        ///ParameterizedDataTypeReference:DataTypeReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void UserDataTypeReference(UserDataTypeReference x) {
            this.SwitchSchemaObjectName(x.Name);
            this.Literals(x.Parameters);
        }
        /// <summary>
        ///DataTypeReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void XmlDataTypeReference(XmlDataTypeReference x) {
            this.SwitchSchemaObjectName(x.Name);
            this.SwitchSchemaObjectName(x.XmlSchemaCollection);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableDefinition(TableDefinition x) {
            if(x.SystemTimePeriod is not null)this.SystemTimePeriodDefinition(x.SystemTimePeriod);
            foreach(var a in x.ColumnDefinitions)this.ColumnDefinition(a);
            this.ConstraintDefinitions(x.TableConstraints);
            foreach(var a in x.Indexes)this.IndexDefinition(a);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DeclareTableVariableBody(DeclareTableVariableBody x) {
            this.TableDefinition(x.Definition);
            this.SwitchIdentifier(x.VariableName);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableReference(TableReference x) {
            switch(x) {
                case TableReferenceWithAlias y: this.TableReferenceWithAlias(y);break;
                case JoinTableReference y: this.JoinTableReference(y);break;
                case JoinParenthesisTableReference y: this.JoinParenthesisTableReference(y);break;
                case OdbcQualifiedJoinTableReference y: this.OdbcQualifiedJoinTableReference(y);break;
            }
        }
        /// <summary>
        ///TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableReferenceWithAlias(TableReferenceWithAlias x) {
            switch(x) {
                case NamedTableReference y: this.NamedTableReference(y);break;
                case TableReferenceWithAliasAndColumns y: this.TableReferenceWithAliasAndColumns(y);break;
                case FullTextTableReference y: this.FullTextTableReference(y);break;
                case SemanticTableReference y: this.SemanticTableReference(y);break;
                case OpenXmlTableReference y: this.OpenXmlTableReference(y);break;
                case OpenJsonTableReference y: this.OpenJsonTableReference(y);break;
                case InternalOpenRowset y: this.InternalOpenRowset(y);break;
                case OpenQueryTableReference y: this.OpenQueryTableReference(y);break;
                case AdHocTableReference y: this.AdHocTableReference(y);break;
                case BuiltInFunctionTableReference y: this.BuiltInFunctionTableReference(y);break;
                case GlobalFunctionTableReference y: this.GlobalFunctionTableReference(y);break;
                case PivotedTableReference y: this.PivotedTableReference(y);break;
                case UnpivotedTableReference y: this.UnpivotedTableReference(y);break;
                case VariableTableReference y: this.VariableTableReference(y);break;
            }
        }
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void NamedTableReference(NamedTableReference x) {
            this.SwitchSchemaObjectName(x.SchemaObject);
            if(x.Alias is not null)this.Identifier(x.Alias);
            this.TableHints(x.TableHints);
            if(x.TableSampleClause is not null)this.TableSampleClause(x.TableSampleClause);
            if(x.TemporalClause is not null)this.TemporalClause(x.TemporalClause);
            var ForPath=x.ForPath;
            //this.SwitchSchemaObjectName(x.SchemaObject);
            //if(x.Alias is not null)this.Identifier(x.Alias);
            //this.TableHints(x.TableHints);
            //if(x.TableSampleClause is not null)this.TableSampleClause(x.TableSampleClause);
            //if(x.TemporalClause is not null)this.TemporalClause(x.TemporalClause);
        }
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableReferenceWithAliasAndColumns(TableReferenceWithAliasAndColumns x) {
            switch(x) {
                case SchemaObjectFunctionTableReference y: this.SchemaObjectFunctionTableReference(y);break;
                case QueryDerivedTable y: this.QueryDerivedTable(y);break;
                case InlineDerivedTable y: this.InlineDerivedTable(y);break;
                case BulkOpenRowset y: this.BulkOpenRowset(y);break;
                case DataModificationTableReference y: this.DataModificationTableReference(y);break;
                case ChangeTableChangesTableReference y: this.ChangeTableChangesTableReference(y);break;
                case ChangeTableVersionTableReference y: this.ChangeTableVersionTableReference(y);break;
                case VariableMethodCallTableReference y: this.VariableMethodCallTableReference(y);break;
            }
        }
        /// <summary>
        ///TableReferenceWithAliasAndColumns:TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SchemaObjectFunctionTableReference(SchemaObjectFunctionTableReference x) {
            if(x.Alias is not null)this.SwitchIdentifier(x.Alias);
            this.SchemaObjectName(x.SchemaObject);
            this.Identifiers(x.Columns);
            this.ScalarExpressions(x.Parameters);
        }
        /// <summary>
        ///TableReferenceWithAliasAndColumns:TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void QueryDerivedTable(QueryDerivedTable x) {
            if(x.Alias is not null)this.SwitchIdentifier(x.Alias);
            this.Identifiers(x.Columns);
            this.QueryExpression(x.QueryExpression);
        }
        /// <summary>
        ///TableReferenceWithAliasAndColumns:TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void InlineDerivedTable(InlineDerivedTable x) {
            if(x.Alias is not null)this.SwitchIdentifier(x.Alias);
            this.Identifiers(x.Columns);
            this.RowValues(x.RowValues);
        }
        /// <summary>
        ///TableReferenceWithAliasAndColumns:TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BulkOpenRowset(BulkOpenRowset x) {
            if(x.Alias is not null)this.SwitchIdentifier(x.Alias);
            this.Identifiers(x.Columns);
            foreach(var DataFile in x.DataFiles){
                this.StringLiteral(DataFile);
            }
            this.BulkInsertOptions(x.Options);
        }
        /// <summary>
        ///TableReferenceWithAliasAndColumns:TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DataModificationTableReference(DataModificationTableReference x) {
            if(x.Alias is not null)this.SwitchIdentifier(x.Alias);
            this.Identifiers(x.Columns);
            this.DataModificationSpecification(x.DataModificationSpecification);
        }
        /// <summary>
        ///TableReferenceWithAliasAndColumns:TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ChangeTableChangesTableReference(ChangeTableChangesTableReference x) {
            if(x.Alias is not null)this.SwitchIdentifier(x.Alias);
            this.Identifiers(x.Columns);
            this.ValueExpression(x.SinceVersion);
            this.SwitchSchemaObjectName(x.Target);
        }
        /// <summary>
        ///TableReferenceWithAliasAndColumns:TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ChangeTableVersionTableReference(ChangeTableVersionTableReference x) {
            if(x.Alias is not null)this.SwitchIdentifier(x.Alias);
            this.Identifiers(x.Columns);
            this.Identifiers(x.PrimaryKeyColumns);
            this.ScalarExpressions(x.PrimaryKeyValues);
        }
        /// <summary>
        ///TableReferenceWithAliasAndColumns:TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void VariableMethodCallTableReference(VariableMethodCallTableReference x) {
            if(x.Alias is not null)this.SwitchIdentifier(x.Alias);
            this.Identifiers(x.Columns);
            this.SwitchIdentifier(x.MethodName);
            this.ScalarExpressions(x.Parameters);
            this.VariableReference(x.Variable);
        }
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FullTextTableReference(FullTextTableReference x) {
            if(x.Alias is not null)this.SwitchIdentifier(x.Alias);
            this.ColumnReferenceExpressions(x.Columns);
            this.ValueExpression(x.Language);
            this.StringLiteral(x.PropertyName);
            this.ValueExpression(x.SearchCondition);
            this.SwitchSchemaObjectName(x.TableName);
            this.ValueExpression(x.TopN);
        }
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SemanticTableReference(SemanticTableReference x) {
            if(x.Alias is not null)this.SwitchIdentifier(x.Alias);
            this.ColumnReferenceExpressions(x.Columns);
            this.ColumnReferenceExpression(x.MatchedColumn);
            this.ScalarExpression(x.MatchedKey);
            this.ScalarExpression(x.SourceKey);
            this.SwitchSchemaObjectName(x.TableName);
        }
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OpenXmlTableReference(OpenXmlTableReference x) {
            if(x.Alias is not null)this.SwitchIdentifier(x.Alias);
            this.ValueExpression(x.RowPattern);
            this.SchemaDeclarationItems(x.SchemaDeclarationItems);
            this.SwitchSchemaObjectName(x.TableName);
            this.VariableReference(x.Variable);
        }
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OpenJsonTableReference(OpenJsonTableReference x) {
            if(x.Alias is not null)this.SwitchIdentifier(x.Alias);
            this.ScalarExpression(x.RowPattern);
            foreach(var a in x.SchemaDeclarationItems)this.SchemaDeclarationItemOpenjson(a);
            this.ScalarExpression(x.Variable);
        }
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OpenRowsetTableReference(OpenRowsetTableReference x) {
            if(x.Alias is not null)this.SwitchIdentifier(x.Alias);
            this.StringLiteral(x.DataSource);
            this.SwitchSchemaObjectName(x.Object);
            this.StringLiteral(x.Password);
            this.StringLiteral(x.ProviderName);
            this.StringLiteral(x.ProviderString);
            this.StringLiteral(x.Query);
            this.StringLiteral(x.UserId);
        }
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void InternalOpenRowset(InternalOpenRowset x) {
            if(x.Alias is not null)this.SwitchIdentifier(x.Alias);
            this.SwitchIdentifier(x.Identifier);
            this.ScalarExpressions(x.VarArgs);
        }
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OpenQueryTableReference(OpenQueryTableReference x) {
            if(x.Alias is not null)this.SwitchIdentifier(x.Alias);
            this.SwitchIdentifier(x.LinkedServer);
            this.StringLiteral(x.Query);
        }
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AdHocTableReference(AdHocTableReference x) {
            if(x.Alias is not null)this.SwitchIdentifier(x.Alias);
            this.AdHocDataSource(x.DataSource);
            this.SchemaObjectNameOrValueExpression(x.Object);
        }
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BuiltInFunctionTableReference(BuiltInFunctionTableReference x) {
            if(x.Alias is not null)this.SwitchIdentifier(x.Alias);
            this.ScalarExpressions(x.Parameters);
        }
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void GlobalFunctionTableReference(GlobalFunctionTableReference x) {
            if(x.Alias is not null)this.SwitchIdentifier(x.Alias);
            this.SwitchIdentifier(x.Name);
            this.ScalarExpressions(x.Parameters);
        }
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void PivotedTableReference(PivotedTableReference x) {
            this.TableReference(x.TableReference);
            this.MultiPartIdentifier(x.AggregateFunctionIdentifier);
            this.ColumnReferenceExpressions(x.ValueColumns);
            this.ColumnReferenceExpression(x.PivotColumn);
            this.Identifiers(x.InColumns);
            if(x.Alias is not null)this.SwitchIdentifier(x.Alias);
        }
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void UnpivotedTableReference(UnpivotedTableReference x) {
            if(x.Alias is not null)this.SwitchIdentifier(x.Alias);
            this.ColumnReferenceExpressions(x.InColumns);
            this.SwitchIdentifier(x.PivotColumn);
            this.SwitchIdentifier(x.ValueColumn);
        }
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void VariableTableReference(VariableTableReference x) {
            if(x.Alias is not null)this.SwitchIdentifier(x.Alias);
            this.VariableReference(x.Variable);
        }
        /// <summary>
        ///TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void JoinTableReference(JoinTableReference x) {
            switch(x) {
                case UnqualifiedJoin y: this.UnqualifiedJoin(y);break;
                case QualifiedJoin y: this.QualifiedJoin(y);break;
            }
        }
        /// <summary>
        ///JoinTableReference:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void UnqualifiedJoin(UnqualifiedJoin x) {
            this.TableReference(x.FirstTableReference);
            this.TableReference(x.SecondTableReference);
        }
        /// <summary>
        ///JoinTableReference:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void QualifiedJoin(QualifiedJoin x) {
            this.TableReference(x.FirstTableReference);
            this.TableReference(x.SecondTableReference);
            this.BooleanExpression(x.SearchCondition);
        }
        /// <summary>
        ///TableReference:TSqlFragment
        /// SELECT * FROM (CUSTOMER JOIN ORDERS ON CUSTOMER.C_CUSTKEY=ORDERS.O_CUSTKEY)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void JoinParenthesisTableReference(JoinParenthesisTableReference x) {
            this.TableReference(x.Join);
        }
        /// <summary>
        ///TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OdbcQualifiedJoinTableReference(OdbcQualifiedJoinTableReference x) {
            this.TableReference(x.TableReference);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableHint(TableHint x) {
            switch(x) {
                case IndexTableHint y: this.TableHint(y);break;
                case LiteralTableHint y: this.TableHint(y);break;
                case ForceSeekTableHint y: this.TableHint(y);break;
                default:break;
            }
        }
        /// <summary>
        ///TableHint:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableHint(IndexTableHint x) {
            this.IdentifierOrValueExpressions(x.IndexValues);
        }
        /// <summary>
        ///TableHint:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableHint(LiteralTableHint x) {
            this.Literal(x.Value);
        }
        /// <summary>
        ///TableHint:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableHint(ForceSeekTableHint x) {
            this.ColumnReferenceExpressions(x.ColumnValues);
            this.IdentifierOrValueExpression(x.IndexValue);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BooleanExpression(BooleanExpression x) {
            switch(x) {
                case SubqueryComparisonPredicate y:this.SubqueryComparisonPredicate(y);break;
                case ExistsPredicate y:this.ExistsPredicate(y);break;
                case LikePredicate y:this.LikePredicate(y);break;
                case InPredicate y:this.InPredicate(y);break;
                case FullTextPredicate y:this.FullTextPredicate(y);break;
                case UpdateCall y:this.UpdateCall(y);break;
                case TSEqualCall y:this.TSEqualCall(y);break;
                case BooleanNotExpression y:this.BooleanNotExpression(y);break;
                case BooleanParenthesisExpression y:this.BooleanParenthesisExpression(y);break;
                case BooleanComparisonExpression y:this.BooleanComparisonExpression(y);break;
                case BooleanBinaryExpression y:this.BooleanBinaryExpression(y);break;
                case BooleanIsNullExpression y:this.BooleanIsNullExpression(y);break;
                case GraphMatchPredicate y:this.GraphMatchPredicate(y);break;
                case GraphMatchExpression y:this.GraphMatchExpression(y);break;
                case BooleanTernaryExpression y:this.BooleanTernaryExpression(y);break;
                case BooleanExpressionSnippet y:this.BooleanExpressionSnippet(y);break;
                case EventDeclarationCompareFunctionParameter y:this.EventDeclarationCompareFunctionParameter(y);break;
            }
        }
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SubqueryComparisonPredicate(SubqueryComparisonPredicate x) {
            this.ScalarExpression(x.Expression);
            this.ScalarSubquery(x.Subquery);
        }
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExistsPredicate(ExistsPredicate x) {
            this.ScalarSubquery(x.Subquery);
        }
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LikePredicate(LikePredicate x) {
            this.ScalarExpression(x.EscapeExpression);
            this.ScalarExpression(x.FirstExpression);
            this.ScalarExpression(x.SecondExpression);
        }
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void InPredicate(InPredicate x) {
            this.ScalarExpression(x.Expression);
            if(x.Subquery is not null)this.ScalarSubquery(x.Subquery);
            this.ScalarExpressions(x.Values);
        }
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FullTextPredicate(FullTextPredicate x) {
            this.ColumnReferenceExpressions(x.Columns);
            this.StringLiteral(x.PropertyName);
            this.ValueExpression(x.Value);
        }
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void UpdateCall(UpdateCall x) {
            this.SwitchIdentifier(x.Identifier);
        }
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TSEqualCall(TSEqualCall x) {
            this.ScalarExpression(x.FirstExpression);
            this.ScalarExpression(x.SecondExpression);
        }
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BooleanNotExpression(BooleanNotExpression x) {
            this.BooleanExpression(x.Expression);
        }
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BooleanParenthesisExpression(BooleanParenthesisExpression x) {
            this.BooleanExpression(x.Expression);
        }
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BooleanComparisonExpression(BooleanComparisonExpression x) {
            this.ScalarExpression(x.FirstExpression);
            this.ScalarExpression(x.SecondExpression);
        }
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BooleanBinaryExpression(BooleanBinaryExpression x) {
            this.BooleanExpression(x.FirstExpression);
            this.BooleanExpression(x.SecondExpression);
        }
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BooleanIsNullExpression(BooleanIsNullExpression x) {
            this.ScalarExpression(x.Expression);
        }
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void GraphMatchPredicate(GraphMatchPredicate x) {
            this.BooleanExpression(x.Expression);
        }
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void GraphMatchExpression(GraphMatchExpression x) {
            this.SwitchIdentifier(x.Edge);
            this.SwitchIdentifier(x.LeftNode);
            this.SwitchIdentifier(x.RightNode);
        }
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BooleanTernaryExpression(BooleanTernaryExpression x) {
            this.ScalarExpression(x.FirstExpression);
            this.ScalarExpression(x.SecondExpression);
            this.ScalarExpression(x.ThirdExpression);
        }
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BooleanExpressionSnippet(BooleanExpressionSnippet x) {
        }
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void EventDeclarationCompareFunctionParameter(EventDeclarationCompareFunctionParameter x) {
            this.ScalarExpression(x.EventValue);
            this.EventSessionObjectName(x.Name);
            this.SourceDeclaration(x.SourceDeclaration);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ForClause(ForClause x) {
            switch(x) {
                case BrowseForClause y:this.BrowseForClause(y);break;
                case ReadOnlyForClause y:this.ReadOnlyForClause(y);break;
                case XmlForClause y:this.XmlForClause(y);break;
                case XmlForClauseOption y:this.XmlForClauseOption(y);break;
                case JsonForClause y:this.JsonForClause(y);break;
                case JsonForClauseOption y:this.JsonForClauseOption(y);break;
                case UpdateForClause y:this.UpdateForClause(y);break;
            }
        }
        /// <summary>
        ///ForClause:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BrowseForClause(BrowseForClause x) {
        }
        /// <summary>
        ///ForClause:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ReadOnlyForClause(ReadOnlyForClause x) {
        }
        /// <summary>
        ///ForClause:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void XmlForClause(XmlForClause x) {
            foreach(var a in x.Options)this.XmlForClauseOption(a);
        }
        /// <summary>
        ///ForClause:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void XmlForClauseOption(XmlForClauseOption x) {
            this.Literal(x.Value);
        }
        /// <summary>
        ///ForClause:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void JsonForClause(JsonForClause x) {
            foreach(var a in x.Options)this.JsonForClauseOption(a);
        }
        /// <summary>
        ///ForClause:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void JsonForClauseOption(JsonForClauseOption x) {
            this.Literal(x.Value);
        }
        /// <summary>
        ///ForClause:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void UpdateForClause(UpdateForClause x) {
            this.ColumnReferenceExpressions(x.Columns);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OptimizerHint(OptimizerHint x) {
            switch(x) {
                case LiteralOptimizerHint y:this.LiteralOptimizerHint(y);break;
                case TableHintsOptimizerHint y:this.TableHintsOptimizerHint(y);break;
                case OptimizeForOptimizerHint y:this.OptimizeForOptimizerHint(y);break;
                case UseHintList y:this.UseHintList(y);break;
                default:break;
            }
        }
        /// <summary>
        ///OptimizerHint:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LiteralOptimizerHint(LiteralOptimizerHint x) {
            this.Literal(x.Value);
        }
        /// <summary>
        ///OptimizerHint:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableHintsOptimizerHint(TableHintsOptimizerHint x) {
            this.SwitchSchemaObjectName(x.ObjectName);
            this.TableHints(x.TableHints);
        }
        /// <summary>
        ///OptimizerHint:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OptimizeForOptimizerHint(OptimizeForOptimizerHint x) {
            foreach(var a in x.Pairs)this.VariableValuePair(a);
        }
        /// <summary>
        ///OptimizerHint:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void UseHintList(UseHintList x) {
            foreach(var a in x.Hints)this.StringLiteral(a);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void VariableValuePair(VariableValuePair x) {
            this.ScalarExpression(x.Value);
            this.VariableReference(x.Variable);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void WhenClause(WhenClause x) {
            switch(x) {
                case SimpleWhenClause y:this.SimpleWhenClause(y);break;
                case SearchedWhenClause y:this.SearchedWhenClause(y);break;
            }
        }
        /// <summary>
        ///WhenClause:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SimpleWhenClause(SimpleWhenClause x) {
            this.ScalarExpression(x.WhenExpression);
            this.ScalarExpression(x.ThenExpression);
        }
        /// <summary>
        ///WhenClause:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SearchedWhenClause(SearchedWhenClause x) {
            this.BooleanExpression(x.WhenExpression);
            this.ScalarExpression(x.ThenExpression);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SchemaDeclarationItem(SchemaDeclarationItem x) {
            switch(x) {
                case SchemaDeclarationItemOpenjson y:this.SchemaDeclarationItemOpenjson(y);break;
                default: {
                    this.ColumnDefinitionBase(x.ColumnDefinition);
                    this.ValueExpression(x.Mapping);
                    break;
                }
            }
        }
        /// <summary>
        ///SchemaDeclarationItem:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SchemaDeclarationItemOpenjson(SchemaDeclarationItemOpenjson x) {
            this.ColumnDefinitionBase(x.ColumnDefinition);
            this.ValueExpression(x.Mapping);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CallTarget(CallTarget x) {
            switch(x) {
                case ExpressionCallTarget y:this.ExpressionCallTarget(y);break;
                case MultiPartIdentifierCallTarget y:this.MultiPartIdentifierCallTarget(y);break;
                case UserDefinedTypeCallTarget y:this.UserDefinedTypeCallTarget(y);break;
            }
        }
        /// <summary>
        ///CallTarget:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExpressionCallTarget(ExpressionCallTarget x) {
            this.ScalarExpression(x.Expression);
        }
        /// <summary>
        ///CallTarget:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MultiPartIdentifierCallTarget(MultiPartIdentifierCallTarget x) {
            this.SwitchMultiPartIdentifier(x.MultiPartIdentifier);
        }
        /// <summary>
        ///CallTarget:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void UserDefinedTypeCallTarget(UserDefinedTypeCallTarget x) {
            this.SwitchSchemaObjectName(x.SchemaObjectName);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OverClause(OverClause x) {
            this.OrderByClause(x.OrderByClause);
            this.ScalarExpressions(x.Partitions);
            if(x.WindowFrameClause is not null)this.WindowFrameClause(x.WindowFrameClause);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AtomicBlockOption(AtomicBlockOption x) {
            switch(x) {
                case LiteralAtomicBlockOption y: this.LiteralAtomicBlockOption(y);break;
                case IdentifierAtomicBlockOption y: this.IdentifierAtomicBlockOption(y);break;
                case OnOffAtomicBlockOption y: this.OnOffAtomicBlockOption(y);break;
            }
        }
        /// <summary>
        ///AtomicBlockOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LiteralAtomicBlockOption(LiteralAtomicBlockOption x) {
            this.Literal(x.Value);
        }
        /// <summary>
        ///AtomicBlockOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void IdentifierAtomicBlockOption(IdentifierAtomicBlockOption x) {
            this.SwitchIdentifier(x.Value);
        }
        /// <summary>
        ///AtomicBlockOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OnOffAtomicBlockOption(OnOffAtomicBlockOption x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ColumnWithSortOrder(ColumnWithSortOrder x) {
            this.ColumnReferenceExpression(x.Column);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DeclareVariableElement(DeclareVariableElement x) {
            switch(x) {
                case ProcedureParameter y:this.ProcedureParameter(y);break;
                default: {
                    this.DataTypeReference(x.DataType);
                    if(x.Nullable is not null) this.NullableConstraintDefinition(x.Nullable);
                    this.ScalarExpression(x.Value);
                    this.SwitchIdentifier(x.VariableName);
                    break;
                }
            }
        }
        /// <summary>
        ///DeclareVariableElement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ProcedureParameter(ProcedureParameter x) {
            if(x.Nullable is not null)this.NullableConstraintDefinition(x.Nullable);
            if(x.Value is not null)this.ScalarExpression(x.Value);
            this.SwitchIdentifier(x.VariableName);
            this.DataTypeReference(x.DataType);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DataModificationSpecification(DataModificationSpecification x) {
            switch(x) {
                case UpdateDeleteSpecificationBase y:this.UpdateDeleteSpecificationBase(y);break;
                case InsertSpecification y:this.InsertSpecification(y);break;
                case MergeSpecification y:this.MergeSpecification(y);break;
            }
        }
        /// <summary>
        ///DataModificationSpecification:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void UpdateDeleteSpecificationBase(UpdateDeleteSpecificationBase x) {
            switch(x) {
                case DeleteSpecification y:this.DeleteSpecification(y);break;
                case UpdateSpecification y:this.UpdateSpecification(y);break;
            }
        }
        /// <summary>
        ///UpdateDeleteSpecificationBase:DataModificationSpecification:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DeleteSpecification(DeleteSpecification x){
            this.FromClause(x.FromClause);
        }
        /// <summary>
        ///UpdateDeleteSpecificationBase:DataModificationSpecification:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void UpdateSpecification(UpdateSpecification x) {
            this.SetClauses(x.SetClauses);
            this.OutputClause(x.OutputClause);
            this.FromClause(x.FromClause);
            if(x.OutputIntoClause is not null)this.OutputIntoClause(x.OutputIntoClause);
            this.TableReference(x.Target);
            if(x.TopRowFilter is not null)this.TopRowFilter(x.TopRowFilter);
            this.WhereClause(x.WhereClause);
        }
        /// <summary>
        ///DataModificationSpecification:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void InsertSpecification(InsertSpecification x) {
            this.ColumnReferenceExpressions(x.Columns);
            this.InsertSource(x.InsertSource);
            if(x.OutputClause is not null)this.OutputClause(x.OutputClause);
            if(x.OutputIntoClause is not null)this.OutputIntoClause(x.OutputIntoClause);
            this.TableReference(x.Target);
            if(x.TopRowFilter is not null)this.TopRowFilter(x.TopRowFilter);
        }
        /// <summary>
        ///DataModificationSpecification:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MergeSpecification(MergeSpecification x) {
            foreach(var a in x.ActionClauses)this.MergeActionClause(a);
            if(x.OutputClause is not null)this.OutputClause(x.OutputClause);
            if(x.OutputIntoClause is not null)this.OutputIntoClause(x.OutputIntoClause);
            this.SwitchIdentifier(x.TableAlias);
            this.TableReference(x.TableReference);
            this.TableReference(x.Target);
            if(x.TopRowFilter is not null)this.TopRowFilter(x.TopRowFilter);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void Permission(Permission x) {
            this.Identifiers(x.Columns);
            this.Identifiers(x.Identifiers);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SecurityTargetObject(SecurityTargetObject x) {
            this.Identifiers(x.Columns);
            this.SecurityTargetObjectName(x.ObjectName);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SecurityTargetObjectName(SecurityTargetObjectName x) {
            this.SwitchMultiPartIdentifier(x.MultiPartIdentifier);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SecurityPrincipal(SecurityPrincipal x) {
            this.SwitchIdentifier(x.Identifier);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SecurityElement80(SecurityElement80 x) {
            switch(x) {
                case CommandSecurityElement80 y:this.CommandSecurityElement80(y);break;
                case PrivilegeSecurityElement80 y:this.PrivilegeSecurityElement80(y);break;
            }
        }
        /// <summary>
        ///SecurityElement80:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CommandSecurityElement80(CommandSecurityElement80 x) {
        }
        /// <summary>
        ///SecurityElement80:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void PrivilegeSecurityElement80(PrivilegeSecurityElement80 x) {
            this.Identifiers(x.Columns);
            foreach(var a in x.Privileges)this.Privilege80(a);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void Privilege80(Privilege80 x) {
            this.Identifiers(x.Columns);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SecurityUserClause80(SecurityUserClause80 x) {
            this.Identifiers(x.Users);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SetClause(SetClause x) {
            switch(x) {
                case AssignmentSetClause y:this.AssignmentSetClause(y);break;
                case FunctionCallSetClause y:this.FunctionCallSetClause(y);break;
            }
        }
        /// <summary>
        ///SetClause:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AssignmentSetClause(AssignmentSetClause x) {
            this.ColumnReferenceExpression(x.Column);
            this.ScalarExpression(x.NewValue);
            this.VariableReference(x.Variable);
        }
        /// <summary>
        ///SetClause:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FunctionCallSetClause(FunctionCallSetClause x) {
            this.FunctionCall(x.MutatorFunction);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void InsertSource(InsertSource x) {
            switch(x) {
                case ValuesInsertSource y:this.ValuesInsertSource(y);break;
                case SelectInsertSource y:this.SelectInsertSource(y);break;
                case ExecuteInsertSource y:this.ExecuteInsertSource(y);break;
            }
        }
        /// <summary>
        ///InsertSource:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ValuesInsertSource(ValuesInsertSource x) {
            this.RowValues(x.RowValues);
        }
        /// <summary>
        ///InsertSource:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SelectInsertSource(SelectInsertSource x) {
            this.QueryExpression(x.Select);
        }
        /// <summary>
        ///InsertSource:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExecuteInsertSource(ExecuteInsertSource x) {
            this.ExecuteSpecification(x.Execute);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RowValue(RowValue x) {
            this.ScalarExpressions(x.ColumnValues);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LiteralRange(LiteralRange x) {
            switch(x) {
                case ProcessAffinityRange y:this.ProcessAffinityRange(y);break;
                default: {
                    this.Literal(x.From);
                    this.Literal(x.To);
                    break;
                }
            }
        }
        /// <summary>
        ///LiteralRange:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ProcessAffinityRange(ProcessAffinityRange x) {
            this.Literal(x.From);
            this.Literal(x.To);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OptionValue(OptionValue x) {
            switch(x) {
                case OnOffOptionValue y:this.OnOffOptionValue(y);break;
                case LiteralOptionValue y:this.LiteralOptionValue(y);break;
            }
        }
        /// <summary>
        ///OptionValue:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OnOffOptionValue(OnOffOptionValue x) {
        }
        /// <summary>
        ///OptionValue:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LiteralOptionValue(LiteralOptionValue x) {
            this.Literal(x.Value);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void IdentifierOrScalarExpression(IdentifierOrScalarExpression x) {
            this.SwitchIdentifier(x.Identifier);
            this.ScalarExpression(x.ScalarExpression);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SchemaObjectNameOrValueExpression(SchemaObjectNameOrValueExpression x) {
            this.SwitchSchemaObjectName(x.SchemaObjectName);
            this.ValueExpression(x.ValueExpression);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SequenceOption(SequenceOption x) {
            switch(x) {
                case DataTypeSequenceOption y:this.DataTypeSequenceOption(y);break;
                case ScalarExpressionSequenceOption y:this.ScalarExpressionSequenceOption(y);break;
            }
        }
        /// <summary>
        ///SequenceOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DataTypeSequenceOption(DataTypeSequenceOption x) {
        }
        /// <summary>
        ///SequenceOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ScalarExpressionSequenceOption(ScalarExpressionSequenceOption x) {
            this.ScalarExpression(x.OptionValue);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SecurityPredicateAction(SecurityPredicateAction x) {
            this.FunctionCall(x.FunctionCall);
            this.SwitchSchemaObjectName(x.TargetObjectName);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SecurityPolicyOption(SecurityPolicyOption x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ColumnMasterKeyParameter(ColumnMasterKeyParameter x) {
            switch(x) {
                case ColumnMasterKeyStoreProviderNameParameter y:this.ColumnMasterKeyStoreProviderNameParameter(y);break;
                case ColumnMasterKeyPathParameter y:this.ColumnMasterKeyPathParameter(y);break;
            }
        }
        /// <summary>
        ///ColumnMasterKeyParameter:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ColumnMasterKeyStoreProviderNameParameter(ColumnMasterKeyStoreProviderNameParameter x) {
            this.StringLiteral(x.Name);
        }
        /// <summary>
        ///ColumnMasterKeyParameter:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ColumnMasterKeyPathParameter(ColumnMasterKeyPathParameter x) {
            this.StringLiteral(x.Path);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ColumnEncryptionKeyValue(ColumnEncryptionKeyValue x) {
            foreach(var a in x.Parameters)this.ColumnEncryptionKeyValueParameter(a);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ColumnEncryptionKeyValueParameter(ColumnEncryptionKeyValueParameter x) {
            switch(x) {
                case ColumnMasterKeyNameParameter y:this.ColumnMasterKeyNameParameter(y);break;
                case ColumnEncryptionAlgorithmNameParameter y:this.ColumnEncryptionAlgorithmNameParameter(y);break;
                case EncryptedValueParameter y:this.EncryptedValueParameter(y);break;
            }
        }
        /// <summary>
        ///ColumnEncryptionKeyValueParameter:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ColumnMasterKeyNameParameter(ColumnMasterKeyNameParameter x) {
            this.SwitchIdentifier(x.Name);
        }
        /// <summary>
        ///ColumnEncryptionKeyValueParameter:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ColumnEncryptionAlgorithmNameParameter(ColumnEncryptionAlgorithmNameParameter x) {
            this.StringLiteral(x.Algorithm);
        }
        /// <summary>
        ///ColumnEncryptionKeyValueParameter:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void EncryptedValueParameter(EncryptedValueParameter x) {
            this.BinaryLiteral(x.Value);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExternalTableOption(ExternalTableOption x) {
            switch(x) {
                case ExternalTableLiteralOrIdentifierOption y:this.ExternalTableLiteralOrIdentifierOption(y);break;
                case ExternalTableDistributionOption y:this.ExternalTableDistributionOption(y);break;
                case ExternalTableRejectTypeOption y:this.ExternalTableRejectTypeOption(y);break;
            }
        }
        /// <summary>
        ///ExternalTableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExternalTableLiteralOrIdentifierOption(ExternalTableLiteralOrIdentifierOption x) {
            this.IdentifierOrValueExpression(x.Value);
        }
        /// <summary>
        ///ExternalTableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExternalTableDistributionOption(ExternalTableDistributionOption x) {
            this.ExternalTableDistributionPolicy(x.Value);
        }
        /// <summary>
        ///ExternalTableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExternalTableRejectTypeOption(ExternalTableRejectTypeOption x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExternalTableDistributionPolicy(ExternalTableDistributionPolicy x) {
            switch(x) {
                case ExternalTableReplicatedDistributionPolicy y:this.ExternalTableReplicatedDistributionPolicy(y);break;
                case ExternalTableRoundRobinDistributionPolicy y:this.ExternalTableRoundRobinDistributionPolicy(y);break;
                case ExternalTableShardedDistributionPolicy y:this.ExternalTableShardedDistributionPolicy(y);break;
            }
        }
        /// <summary>
        ///ExternalTableDistributionPolicy:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExternalTableReplicatedDistributionPolicy(ExternalTableReplicatedDistributionPolicy x) {
        }
        /// <summary>
        ///ExternalTableDistributionPolicy:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExternalTableRoundRobinDistributionPolicy(ExternalTableRoundRobinDistributionPolicy x) {
        }
        /// <summary>
        ///ExternalTableDistributionPolicy:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExternalTableShardedDistributionPolicy(ExternalTableShardedDistributionPolicy x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExternalDataSourceOption(ExternalDataSourceOption x) {
            switch(x) {
                case ExternalDataSourceLiteralOrIdentifierOption y:this.ExternalDataSourceLiteralOrIdentifierOption(y);break;
            }
        }
        /// <summary>
        ///ExternalDataSourceOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExternalDataSourceLiteralOrIdentifierOption(ExternalDataSourceLiteralOrIdentifierOption x) {
            this.IdentifierOrValueExpression(x.Value);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExternalFileFormatOption(ExternalFileFormatOption x) {
            switch(x) {
                case ExternalFileFormatLiteralOption y:this.ExternalFileFormatLiteralOption(y);break;
                case ExternalFileFormatUseDefaultTypeOption y:this.ExternalFileFormatUseDefaultTypeOption(y);break;
                case ExternalFileFormatContainerOption y:this.ExternalFileFormatContainerOption(y);break;
            }
        }
        /// <summary>
        ///ExternalFileFormatOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExternalFileFormatLiteralOption(ExternalFileFormatLiteralOption x) {
            this.Literal(x.Value);
        }
        /// <summary>
        ///ExternalFileFormatOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExternalFileFormatUseDefaultTypeOption(ExternalFileFormatUseDefaultTypeOption x) {
        }
        /// <summary>
        ///ExternalFileFormatOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExternalFileFormatContainerOption(ExternalFileFormatContainerOption x) {
            this.ExternalFileFormatOptions(x.Suboptions);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AssemblyOption(AssemblyOption x) {
            switch(x) {
                case OnOffAssemblyOption y:this.OnOffAssemblyOption(y);break;
                case PermissionSetAssemblyOption y:this.PermissionSetAssemblyOption(y);break;
                default:break;
            }
        }
        /// <summary>
        ///AssemblyOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OnOffAssemblyOption(OnOffAssemblyOption x) {
        }
        /// <summary>
        ///AssemblyOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void PermissionSetAssemblyOption(PermissionSetAssemblyOption x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AddFileSpec(AddFileSpec x) {
            this.ScalarExpression(x.File);
            this.Literal(x.FileName);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AssemblyName(AssemblyName x) {
            this.SwitchIdentifier(x.ClassName);
            this.SwitchIdentifier(x.Name);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableOption(TableOption x) {
            switch(x) {
                case LockEscalationTableOption y:this.LockEscalationTableOption(y);break;
                case FileStreamOnTableOption y:this.FileStreamOnTableOption(y);break;
                case FileTableDirectoryTableOption y:this.FileTableDirectoryTableOption(y);break;
                case FileTableCollateFileNameTableOption y:this.FileTableCollateFileNameTableOption(y);break;
                case FileTableConstraintNameTableOption y:this.FileTableConstraintNameTableOption(y);break;
                case MemoryOptimizedTableOption y:this.MemoryOptimizedTableOption(y);break;
                case DurabilityTableOption y:this.DurabilityTableOption(y);break;
                case RemoteDataArchiveTableOption y:this.RemoteDataArchiveTableOption(y);break;
                case RemoteDataArchiveAlterTableOption y:this.RemoteDataArchiveAlterTableOption(y);break;
                case SystemVersioningTableOption y:this.SystemVersioningTableOption(y);break;
                case TableDataCompressionOption y:this.TableDataCompressionOption(y);break;
                case TableDistributionOption y:this.TableDistributionOption(y);break;
                case TableIndexOption y:this.TableIndexOption(y);break;
                case TablePartitionOption y:this.TablePartitionOption(y);break;
            }
        }
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LockEscalationTableOption(LockEscalationTableOption x) {
        }
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FileStreamOnTableOption(FileStreamOnTableOption x) {
            this.IdentifierOrValueExpression(x.Value);
        }
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FileTableDirectoryTableOption(FileTableDirectoryTableOption x) {
            this.Literal(x.Value);
        }
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FileTableCollateFileNameTableOption(FileTableCollateFileNameTableOption x) {
            this.SwitchIdentifier(x.Value);
        }
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FileTableConstraintNameTableOption(FileTableConstraintNameTableOption x) {
            this.SwitchIdentifier(x.Value);
        }
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MemoryOptimizedTableOption(MemoryOptimizedTableOption x) {
        }
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DurabilityTableOption(DurabilityTableOption x) {
        }
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RemoteDataArchiveTableOption(RemoteDataArchiveTableOption x) {
        }
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RemoteDataArchiveAlterTableOption(RemoteDataArchiveAlterTableOption x) {
            this.FunctionCall(x.FilterPredicate);
        }
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SystemVersioningTableOption(SystemVersioningTableOption x) {
            this.RetentionPeriodDefinition(x.RetentionPeriod);
            this.SwitchSchemaObjectName(x.HistoryTable);
        }
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableDataCompressionOption(TableDataCompressionOption x) {
            this.DataCompressionOption(x.DataCompressionOption);
        }
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableDistributionOption(TableDistributionOption x) {
            this.TableDistributionPolicy(x.Value);
        }
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableIndexOption(TableIndexOption x) {
            this.TableIndexType(x.Value);
        }
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TablePartitionOption(TablePartitionOption x) {
            this.SwitchIdentifier(x.PartitionColumn);
            this.TablePartitionOptionSpecifications(x.PartitionOptionSpecs);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DatabaseOption(DatabaseOption x) {
            switch(x) {
                case RemoteDataArchiveDatabaseOption y:this.RemoteDataArchiveDatabaseOption(y);break;
                case OnOffDatabaseOption y:this.OnOffDatabaseOption(y);break;
                case ContainmentDatabaseOption y:this.ContainmentDatabaseOption(y);break;
                case HadrDatabaseOption y:this.HadrDatabaseOption(y);break;
                case DelayedDurabilityDatabaseOption y:this.DelayedDurabilityDatabaseOption(y);break;
                case CursorDefaultDatabaseOption y:this.CursorDefaultDatabaseOption(y);break;
                case RecoveryDatabaseOption y:this.RecoveryDatabaseOption(y);break;
                case TargetRecoveryTimeDatabaseOption y:this.TargetRecoveryTimeDatabaseOption(y);break;
                case PageVerifyDatabaseOption y:this.PageVerifyDatabaseOption(y);break;
                case PartnerDatabaseOption y:this.PartnerDatabaseOption(y);break;
                case WitnessDatabaseOption y:this.WitnessDatabaseOption(y);break;
                case ParameterizationDatabaseOption y:this.ParameterizationDatabaseOption(y);break;
                case LiteralDatabaseOption y:this.LiteralDatabaseOption(y);break;
                case IdentifierDatabaseOption y:this.IdentifierDatabaseOption(y);break;
                case ChangeTrackingDatabaseOption y:this.ChangeTrackingDatabaseOption(y);break;
                case QueryStoreDatabaseOption y:this.QueryStoreDatabaseOption(y);break;
                case AutomaticTuningDatabaseOption y:this.AutomaticTuningDatabaseOption(y);break;
                case FileStreamDatabaseOption y:this.FileStreamDatabaseOption(y);break;
                case CatalogCollationOption y:this.CatalogCollationOption(y);break;
                case MaxSizeDatabaseOption y:this.MaxSizeDatabaseOption(y);break;
                default:break;
            }
        }
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RemoteDataArchiveDatabaseOption(RemoteDataArchiveDatabaseOption x) {
            foreach(var a in x.Settings)this.RemoteDataArchiveDatabaseSetting(a);
        }
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OnOffDatabaseOption(OnOffDatabaseOption x) {
            switch(x) {
                case AutoCreateStatisticsDatabaseOption y:this.AutoCreateStatisticsDatabaseOption(y);break;
                default:break;
            }
        }
        /// <summary>
        ///OnOffDatabaseOption:DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AutoCreateStatisticsDatabaseOption(AutoCreateStatisticsDatabaseOption x) {
        }
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ContainmentDatabaseOption(ContainmentDatabaseOption x) {
        }
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void HadrDatabaseOption(HadrDatabaseOption x) {
            switch(x) {
                case HadrAvailabilityGroupDatabaseOption y:this.HadrAvailabilityGroupDatabaseOption(y);break;
                default:break;
            }
        }
        /// <summary>
        ///HadrDatabaseOption:DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void HadrAvailabilityGroupDatabaseOption(HadrAvailabilityGroupDatabaseOption x) {
            this.SwitchIdentifier(x.GroupName);
        }
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DelayedDurabilityDatabaseOption(DelayedDurabilityDatabaseOption x) {
        }
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CursorDefaultDatabaseOption(CursorDefaultDatabaseOption x) {
        }
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RecoveryDatabaseOption(RecoveryDatabaseOption x) {
        }
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TargetRecoveryTimeDatabaseOption(TargetRecoveryTimeDatabaseOption x) {
            this.Literal(x.RecoveryTime);
        }
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void PageVerifyDatabaseOption(PageVerifyDatabaseOption x) {
        }
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void PartnerDatabaseOption(PartnerDatabaseOption x) {
            this.Literal(x.PartnerServer);
        }
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void WitnessDatabaseOption(WitnessDatabaseOption x) {
            this.Literal(x.WitnessServer);
        }
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ParameterizationDatabaseOption(ParameterizationDatabaseOption x) {
        }
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LiteralDatabaseOption(LiteralDatabaseOption x) {
            this.Literal(x.Value);
        }
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void IdentifierDatabaseOption(IdentifierDatabaseOption x) {
            this.SwitchIdentifier(x.Value);
        }
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ChangeTrackingDatabaseOption(ChangeTrackingDatabaseOption x) {
            foreach(var a in x.Details)this.ChangeTrackingOptionDetail(a);
        }
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void QueryStoreDatabaseOption(QueryStoreDatabaseOption x) {
            foreach(var a in x.Options)this.QueryStoreOption(a);
        }
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AutomaticTuningDatabaseOption(AutomaticTuningDatabaseOption x) {
            throw new NotSupportedException();
        }
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FileStreamDatabaseOption(FileStreamDatabaseOption x) {
            this.Literal(x.DirectoryName);
        }
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CatalogCollationOption(CatalogCollationOption x) {
            throw new NotSupportedException();
        }
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MaxSizeDatabaseOption(MaxSizeDatabaseOption x) {
            this.Literal(x.MaxSize);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RemoteDataArchiveDatabaseSetting(RemoteDataArchiveDatabaseSetting x) {
            switch(x) {
                case RemoteDataArchiveDbServerSetting y:this.RemoteDataArchiveDbServerSetting(y);break;
                case RemoteDataArchiveDbCredentialSetting y:this.RemoteDataArchiveDbCredentialSetting(y);break;
                case RemoteDataArchiveDbFederatedServiceAccountSetting y:this.RemoteDataArchiveDbFederatedServiceAccountSetting(y);break;
            }
        }
        /// <summary>
        ///RemoteDataArchiveDatabaseSetting:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RemoteDataArchiveDbServerSetting(RemoteDataArchiveDbServerSetting x) {
            this.StringLiteral(x.Server);
        }
        /// <summary>
        ///RemoteDataArchiveDatabaseSetting:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RemoteDataArchiveDbCredentialSetting(RemoteDataArchiveDbCredentialSetting x) {
            this.SwitchIdentifier(x.Credential);
        }
        /// <summary>
        ///RemoteDataArchiveDatabaseSetting:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RemoteDataArchiveDbFederatedServiceAccountSetting(RemoteDataArchiveDbFederatedServiceAccountSetting x) {
            throw new NotSupportedException();
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RetentionPeriodDefinition(RetentionPeriodDefinition x) {
            this.IntegerLiteral(x.Duration);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableSwitchOption(TableSwitchOption x) {
            switch(x) {
                case LowPriorityLockWaitTableSwitchOption y:this.LowPriorityLockWaitTableSwitchOption(y);break;
            }
        }
        /// <summary>
        ///TableSwitchOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LowPriorityLockWaitTableSwitchOption(LowPriorityLockWaitTableSwitchOption x) {
            this.LowPriorityLockWaitOptions(x.Options);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropClusteredConstraintOption(DropClusteredConstraintOption x) {
            switch(x) {
                case DropClusteredConstraintStateOption y:this.DropClusteredConstraintStateOption(y);break;
                case DropClusteredConstraintValueOption y:this.DropClusteredConstraintValueOption(y);break;
                case DropClusteredConstraintMoveOption y:this.DropClusteredConstraintMoveOption(y);break;
                case DropClusteredConstraintWaitAtLowPriorityLockOption y:this.DropClusteredConstraintWaitAtLowPriorityLockOption(y);break;
            }
        }
        /// <summary>
        ///DropClusteredConstraintOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropClusteredConstraintStateOption(DropClusteredConstraintStateOption x) {
        }
        /// <summary>
        ///DropClusteredConstraintOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropClusteredConstraintValueOption(DropClusteredConstraintValueOption x) {
            this.Literal(x.OptionValue);
        }
        /// <summary>
        ///DropClusteredConstraintOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropClusteredConstraintMoveOption(DropClusteredConstraintMoveOption x) {
            this.FileGroupOrPartitionScheme(x.OptionValue);
        }
        /// <summary>
        ///DropClusteredConstraintOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropClusteredConstraintWaitAtLowPriorityLockOption(DropClusteredConstraintWaitAtLowPriorityLockOption x) {
            this.LowPriorityLockWaitOptions(x.Options);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterTableDropTableElement(AlterTableDropTableElement x) {
            this.SwitchIdentifier(x.Name);
            foreach(var a in x.DropClusteredConstraintOptions)this.DropClusteredConstraintOption(a);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExecuteAsClause(ExecuteAsClause x) {
            this.Literal(x.Literal);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void QueueOption(QueueOption x) {
            //x.OptionKind
            switch(x) {
                case QueueStateOption y:this.QueueStateOption(y);break;
                case QueueProcedureOption y:this.QueueProcedureOption(y);break;
                case QueueValueOption y:this.QueueValueOption(y);break;
                case QueueExecuteAsOption y:this.QueueExecuteAsOption(y);break;
            }
        }
        /// <summary>
        ///QueueOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void QueueStateOption(QueueStateOption x) {
        }
        /// <summary>
        ///QueueOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void QueueProcedureOption(QueueProcedureOption x) {
            this.SwitchSchemaObjectName(x.OptionValue);
        }
        /// <summary>
        ///QueueOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void QueueValueOption(QueueValueOption x) {
            this.ValueExpression(x.OptionValue);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void QueueExecuteAsOption(QueueExecuteAsOption x) {
            this.ExecuteAsClause(x.OptionValue);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RouteOption(RouteOption x) {
            this.Literal(x.Literal);
        }
        /// <summary>
        ///TSqlFragment CREATE FUNCTIONの戻り値のテーブル型定義  @Student table(TestID int not null)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SystemTimePeriodDefinition(SystemTimePeriodDefinition x) {
            this.SwitchIdentifier(x.StartTimeColumn);
            this.SwitchIdentifier(x.EndTimeColumn);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void IndexType(IndexType x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void PartitionSpecifier(PartitionSpecifier x) {
            this.ScalarExpression(x.Number);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FileGroupOrPartitionScheme(FileGroupOrPartitionScheme x) {
            this.IdentifierOrValueExpression(x.Name);
            this.Identifiers(x.PartitionSchemeColumns);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void IndexOption(IndexOption x) {
            switch(x) {
                case IndexStateOption y:this.IndexStateOption(y);break;
                case IndexExpressionOption y:this.IndexExpressionOption(y);break;
                case MaxDurationOption y:this.MaxDurationOption(y);break;
                case WaitAtLowPriorityOption y:this.WaitAtLowPriorityOption(y);break;
                case OrderIndexOption y:this.OrderIndexOption(y);break;
                case MoveToDropIndexOption y:this.MoveToDropIndexOption(y);break;
                case FileStreamOnDropIndexOption y:this.FileStreamOnDropIndexOption(y);break;
                case DataCompressionOption y:this.DataCompressionOption(y);break;
                case CompressionDelayIndexOption y:this.CompressionDelayIndexOption(y);break;
            }
        }
        /// <summary>
        ///IndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void IndexStateOption(IndexStateOption x) {
            switch(x) {
                case OnlineIndexOption y:this.OnlineIndexOption(y);break;
                case IgnoreDupKeyIndexOption y:this.IgnoreDupKeyIndexOption(y);break;
                default:break;
            }
        }
        /// <summary>
        ///IndexStateOption:IndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OnlineIndexOption(OnlineIndexOption x) {
            this.OnlineIndexLowPriorityLockWaitOption(x.LowPriorityLockWaitOption);
        }
        /// <summary>
        ///IndexStateOption:IndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void IgnoreDupKeyIndexOption(IgnoreDupKeyIndexOption x) {
        }
        /// <summary>
        ///IndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void IndexExpressionOption(IndexExpressionOption x) {
            this.ScalarExpression(x.Expression);
        }
        /// <summary>
        ///IndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MaxDurationOption(MaxDurationOption x) {
            this.Literal(x.MaxDuration);
        }
        /// <summary>
        ///IndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void WaitAtLowPriorityOption(WaitAtLowPriorityOption x) {
            this.LowPriorityLockWaitOptions(x.Options);
        }
        /// <summary>
        ///IndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OrderIndexOption(OrderIndexOption x) {
            this.ColumnReferenceExpressions(x.Columns);
        }
        /// <summary>
        ///IndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MoveToDropIndexOption(MoveToDropIndexOption x) {
            this.FileGroupOrPartitionScheme(x.MoveTo);
        }
        /// <summary>
        ///IndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FileStreamOnDropIndexOption(FileStreamOnDropIndexOption x) {
            this.IdentifierOrValueExpression(x.FileStreamOn);
        }
        /// <summary>
        ///IndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DataCompressionOption(DataCompressionOption x) {
            this.CompressionPartitionRanges(x.PartitionRanges);
        }
        /// <summary>
        ///IndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CompressionDelayIndexOption(CompressionDelayIndexOption x) {
            this.ScalarExpression(x.Expression);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OnlineIndexLowPriorityLockWaitOption(OnlineIndexLowPriorityLockWaitOption x) {
            this.LowPriorityLockWaitOptions(x.Options);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LowPriorityLockWaitOption(LowPriorityLockWaitOption x) {
            switch(x) {
                case LowPriorityLockWaitMaxDurationOption y:this.LowPriorityLockWaitMaxDurationOption(y);break;
                case LowPriorityLockWaitAbortAfterWaitOption y:this.LowPriorityLockWaitAbortAfterWaitOption(y);break;
            }
        }
        /// <summary>
        ///LowPriorityLockWaitOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LowPriorityLockWaitMaxDurationOption(LowPriorityLockWaitMaxDurationOption x) {
            this.Literal(x.MaxDuration);
        }
        /// <summary>
        ///LowPriorityLockWaitOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LowPriorityLockWaitAbortAfterWaitOption(LowPriorityLockWaitAbortAfterWaitOption x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FullTextIndexColumn(FullTextIndexColumn x) {
            this.SwitchIdentifier(x.Name);
            this.SwitchIdentifier(x.TypeColumn);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FullTextIndexOption(FullTextIndexOption x) {
            switch(x) {
                case ChangeTrackingFullTextIndexOption y:this.ChangeTrackingFullTextIndexOption(y);break;
                case StopListFullTextIndexOption y:this.StopListFullTextIndexOption(y);break;
                case SearchPropertyListFullTextIndexOption y:this.SearchPropertyListFullTextIndexOption(y);break;
            }
        }
        /// <summary>
        ///FullTextIndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ChangeTrackingFullTextIndexOption(ChangeTrackingFullTextIndexOption x) {
        }
        /// <summary>
        ///FullTextIndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void StopListFullTextIndexOption(StopListFullTextIndexOption x) {
            this.SwitchIdentifier(x.StopListName);
        }
        /// <summary>
        ///FullTextIndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SearchPropertyListFullTextIndexOption(SearchPropertyListFullTextIndexOption x) {
            this.SwitchIdentifier(x.PropertyListName);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FullTextCatalogAndFileGroup(FullTextCatalogAndFileGroup x) {
            this.SwitchIdentifier(x.CatalogName);
            this.SwitchIdentifier(x.FileGroupName);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void EventTypeGroupContainer(EventTypeGroupContainer x) {
            switch(x) {
                case EventTypeContainer y:this.EventTypeContainer(y);break;
                case EventGroupContainer y:this.EventGroupContainer(y);break;
            }
        }
        /// <summary>
        ///EventTypeGroupContainer:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void EventTypeContainer(EventTypeContainer x) {
        }
        /// <summary>
        ///EventTypeGroupContainer:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void EventGroupContainer(EventGroupContainer x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void EventNotificationObjectScope(EventNotificationObjectScope x) {
            this.SwitchSchemaObjectName(x.QueueName);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ApplicationRoleOption(ApplicationRoleOption x) {
            this.IdentifierOrValueExpression(x.Value);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterRoleAction(AlterRoleAction x) {
            switch(x) {
                case RenameAlterRoleAction y:this.RenameAlterRoleAction(y);break;
                case AddMemberAlterRoleAction y:this.AddMemberAlterRoleAction(y);break;
                case DropMemberAlterRoleAction y:this.DropMemberAlterRoleAction(y);break;
            }
        }
        /// <summary>
        ///AlterRoleAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RenameAlterRoleAction(RenameAlterRoleAction x) {
            this.SwitchIdentifier(x.NewName);
        }
        /// <summary>
        ///AlterRoleAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AddMemberAlterRoleAction(AddMemberAlterRoleAction x) {
            this.SwitchIdentifier(x.Member);
        }
        /// <summary>
        ///AlterRoleAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropMemberAlterRoleAction(DropMemberAlterRoleAction x) {
            this.SwitchIdentifier(x.Member);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void UserLoginOption(UserLoginOption x) {
            this.Identifier(x.Identifier);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void StatisticsOption(StatisticsOption x) {
            switch(x) {
                case ResampleStatisticsOption y:this.ResampleStatisticsOption(y);break;
                case OnOffStatisticsOption y:this.OnOffStatisticsOption(y);break;
                case LiteralStatisticsOption y:this.LiteralStatisticsOption(y);break;
            }
        }
        /// <summary>
        ///StatisticsOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ResampleStatisticsOption(ResampleStatisticsOption x) {
            foreach(var a in x.Partitions)this.StatisticsPartitionRange(a);
        }
        /// <summary>
        ///StatisticsOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OnOffStatisticsOption(OnOffStatisticsOption x) {
        }
        /// <summary>
        ///StatisticsOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LiteralStatisticsOption(LiteralStatisticsOption x) {
            this.Literal(x.Literal);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void StatisticsPartitionRange(StatisticsPartitionRange x) {
            this.IntegerLiteral(x.From);
            this.IntegerLiteral(x.To);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CursorDefinition(CursorDefinition x) {
            foreach(var a in x.Options)this.CursorOption(a);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CursorOption(CursorOption x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CursorId(CursorId x) {
            this.IdentifierOrValueExpression(x.Name);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CryptoMechanism(CryptoMechanism x) {
            this.SwitchIdentifier(x.Identifier);
            this.Literal(x.PasswordOrSignature);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FetchType(FetchType x) {
            this.ScalarExpression(x.RowOffset);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void WhereClause(WhereClause x) {
            if(x.Cursor is not null)this.CursorId(x.Cursor);
            this.BooleanExpression(x.SearchCondition);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropIndexClauseBase(DropIndexClauseBase x) {
            switch(x) {
                case BackwardsCompatibleDropIndexClause y:this.BackwardsCompatibleDropIndexClause(y);break;
                case DropIndexClause y:this.DropIndexClause(y);break;
            }
        }
        /// <summary>
        ///DropIndexClauseBase:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BackwardsCompatibleDropIndexClause(BackwardsCompatibleDropIndexClause x) {
            this.ChildObjectName(x.Index);
        }
        /// <summary>
        ///DropIndexClauseBase:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropIndexClause(DropIndexClause x) {
            this.SwitchIdentifier(x.Index);
            this.IndexOptions(x.Options);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SetCommand(SetCommand x) {
            switch(x) {
                case GeneralSetCommand y:this.GeneralSetCommand(y);break;
                case SetFipsFlaggerCommand y:this.SetFipsFlaggerCommand(y);break;
            }
        }
        /// <summary>
        ///SetCommand:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void GeneralSetCommand(GeneralSetCommand x) {
            this.ScalarExpression(x.Parameter);
        }
        /// <summary>
        ///SetCommand:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SetFipsFlaggerCommand(SetFipsFlaggerCommand x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FileDeclaration(FileDeclaration x) {
            foreach(var a in x.Options)this.FileDeclarationOption(a);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FileDeclarationOption(FileDeclarationOption x) {
            switch(x) {
                case NameFileDeclarationOption y:this.NameFileDeclarationOption(y);break;
                case FileNameFileDeclarationOption y:this.FileNameFileDeclarationOption(y);break;
                case SizeFileDeclarationOption y:this.SizeFileDeclarationOption(y);break;
                case MaxSizeFileDeclarationOption y:this.MaxSizeFileDeclarationOption(y);break;
                case FileGrowthFileDeclarationOption y:this.FileGrowthFileDeclarationOption(y);break;
                default:break;
            }
        }
        /// <summary>
        ///FileDeclarationOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void NameFileDeclarationOption(NameFileDeclarationOption x) {
            this.IdentifierOrValueExpression(x.LogicalFileName);
        }
        /// <summary>
        ///FileDeclarationOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FileNameFileDeclarationOption(FileNameFileDeclarationOption x) {
            this.Literal(x.OSFileName);
        }
        /// <summary>
        ///FileDeclarationOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SizeFileDeclarationOption(SizeFileDeclarationOption x) {
            this.Literal(x.Size);
        }
        /// <summary>
        ///FileDeclarationOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MaxSizeFileDeclarationOption(MaxSizeFileDeclarationOption x) {
            this.Literal(x.MaxSize);
        }
        /// <summary>
        ///FileDeclarationOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FileGrowthFileDeclarationOption(FileGrowthFileDeclarationOption x) {
            this.Literal(x.GrowthIncrement);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FileGroupDefinition(FileGroupDefinition x) {
            this.FileDeclarations(x.FileDeclarations);
            this.SwitchIdentifier(x.Name);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DatabaseConfigurationClearOption(DatabaseConfigurationClearOption x) {
            this.BinaryLiteral(x.PlanHandle);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DatabaseConfigurationSetOption(DatabaseConfigurationSetOption x) {
            switch(x) {
                case OnOffPrimaryConfigurationOption y:this.OnOffPrimaryConfigurationOption(y);break;
                case MaxDopConfigurationOption y:this.MaxDopConfigurationOption(y);break;
                case GenericConfigurationOption y:this.GenericConfigurationOption(y);break;
                default: {
                    this.SwitchIdentifier(x.GenericOptionKind);
                    break;
                }
            }
        }
        /// <summary>
        ///DatabaseConfigurationSetOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OnOffPrimaryConfigurationOption(OnOffPrimaryConfigurationOption x) {
            this.SwitchIdentifier(x.GenericOptionKind);

        }
        /// <summary>
        ///DatabaseConfigurationSetOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MaxDopConfigurationOption(MaxDopConfigurationOption x) {
            this.SwitchIdentifier(x.GenericOptionKind);
            this.Literal(x.Value);
        }
        /// <summary>
        ///DatabaseConfigurationSetOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void GenericConfigurationOption(GenericConfigurationOption x) {
            this.SwitchIdentifier(x.GenericOptionKind);
            this.IdentifierOrScalarExpression(x.GenericOptionState);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterDatabaseTermination(AlterDatabaseTermination x) {
            this.Literal(x.RollbackAfter);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ChangeTrackingOptionDetail(ChangeTrackingOptionDetail x) {
            switch(x) {
                case AutoCleanupChangeTrackingOptionDetail y:this.AutoCleanupChangeTrackingOptionDetail(y);break;
                case ChangeRetentionChangeTrackingOptionDetail y:this.ChangeRetentionChangeTrackingOptionDetail(y);break;
            }
        }
        /// <summary>
        ///ChangeTrackingOptionDetail:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AutoCleanupChangeTrackingOptionDetail(AutoCleanupChangeTrackingOptionDetail x) {
        }
        /// <summary>
        ///ChangeTrackingOptionDetail:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ChangeRetentionChangeTrackingOptionDetail(ChangeRetentionChangeTrackingOptionDetail x) {
            this.Literal(x.RetentionPeriod);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void QueryStoreOption(QueryStoreOption x) {
            switch(x) {
                case QueryStoreDesiredStateOption y:this.QueryStoreDesiredStateOption(y);break;
                case QueryStoreCapturePolicyOption y:this.QueryStoreCapturePolicyOption(y);break;
                case QueryStoreSizeCleanupPolicyOption y:this.QueryStoreSizeCleanupPolicyOption(y);break;
                case QueryStoreDataFlushIntervalOption y:this.QueryStoreDataFlushIntervalOption(y);break;
                case QueryStoreIntervalLengthOption y:this.QueryStoreIntervalLengthOption(y);break;
                case QueryStoreMaxStorageSizeOption y:this.QueryStoreMaxStorageSizeOption(y);break;
                case QueryStoreMaxPlansPerQueryOption y:this.QueryStoreMaxPlansPerQueryOption(y);break;
                case QueryStoreTimeCleanupPolicyOption y:this.QueryStoreTimeCleanupPolicyOption(y);break;
            }
        }
        /// <summary>
        ///QueryStoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void QueryStoreDesiredStateOption(QueryStoreDesiredStateOption x) {
        }
        /// <summary>
        ///QueryStoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void QueryStoreCapturePolicyOption(QueryStoreCapturePolicyOption x) {
        }
        /// <summary>
        ///QueryStoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void QueryStoreSizeCleanupPolicyOption(QueryStoreSizeCleanupPolicyOption x) {
        }
        /// <summary>
        ///QueryStoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void QueryStoreDataFlushIntervalOption(QueryStoreDataFlushIntervalOption x) {
            this.Literal(x.FlushInterval);
        }
        /// <summary>
        ///QueryStoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void QueryStoreIntervalLengthOption(QueryStoreIntervalLengthOption x) {
            this.Literal(x.StatsIntervalLength);
        }
        /// <summary>
        ///QueryStoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void QueryStoreMaxStorageSizeOption(QueryStoreMaxStorageSizeOption x) {
            this.Literal(x.MaxQdsSize);
        }
        /// <summary>
        ///QueryStoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void QueryStoreMaxPlansPerQueryOption(QueryStoreMaxPlansPerQueryOption x) {
            this.Literal(x.MaxPlansPerQuery);
        }
        /// <summary>
        ///QueryStoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void QueryStoreTimeCleanupPolicyOption(QueryStoreTimeCleanupPolicyOption x) {
            this.Literal(x.StaleQueryThreshold);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AutomaticTuningOption(AutomaticTuningOption x) {
            switch(x) {
                case AutomaticTuningForceLastGoodPlanOption y:this.AutomaticTuningForceLastGoodPlanOption(y);break;
                case AutomaticTuningCreateIndexOption y:this.AutomaticTuningCreateIndexOption(y);break;
                case AutomaticTuningDropIndexOption y:this.AutomaticTuningDropIndexOption(y);break;
                case AutomaticTuningMaintainIndexOption y:this.AutomaticTuningMaintainIndexOption(y);break;
                default:break;
            }
        }
        /// <summary>
        ///AutomaticTuningOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AutomaticTuningForceLastGoodPlanOption(AutomaticTuningForceLastGoodPlanOption x) {
            throw new NotSupportedException();
        }
        /// <summary>
        ///AutomaticTuningOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AutomaticTuningCreateIndexOption(AutomaticTuningCreateIndexOption x)
        {
            throw new NotSupportedException();
        }
        /// <summary>
        ///AutomaticTuningOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AutomaticTuningDropIndexOption(AutomaticTuningDropIndexOption x) {
            throw new NotSupportedException();
        }
        /// <summary>
        ///AutomaticTuningOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AutomaticTuningMaintainIndexOption(AutomaticTuningMaintainIndexOption x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ColumnDefinitionBase(ColumnDefinitionBase x) {
            switch(x) {
                case ColumnDefinition y:this.ColumnDefinition(y);break;
                default: {
                    if(x.Collation is not null)this.SwitchIdentifier(x.Collation);
                    this.SwitchIdentifier(x.ColumnIdentifier);
                    this.DataTypeReference(x.DataType);
                    break;
                }
            }
        }
        /// <summary>
        ///ColumnDefinitionBase:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ColumnDefinition(ColumnDefinition x) {
            if(x.Collation is not null)this.SwitchIdentifier(x.Collation);
            this.SwitchIdentifier(x.ColumnIdentifier);
            this.DataTypeReference(x.DataType);
            if(x.ComputedColumnExpression is not null)this.ScalarExpression(x.ComputedColumnExpression);
            if(x.DefaultConstraint is not null)this.DefaultConstraintDefinition(x.DefaultConstraint);
            if(x.IdentityOptions is not null)this.IdentityOptions(x.IdentityOptions);
            this.ConstraintDefinitions(x.Constraints);
            if(x.StorageOptions is not null)this.ColumnStorageOptions(x.StorageOptions);
            if(x.Index is not null)this.IndexDefinition(x.Index);
            if(x.Encryption is not null)this.ColumnEncryptionDefinition(x.Encryption);
            if(x.MaskingFunction is not null)this.StringLiteral(x.MaskingFunction);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ColumnEncryptionDefinition(ColumnEncryptionDefinition x) {
            foreach(var a in x.Parameters)this.ColumnEncryptionDefinitionParameter(a);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ColumnEncryptionDefinitionParameter(ColumnEncryptionDefinitionParameter x) {
            switch(x) {
                case ColumnEncryptionKeyNameParameter y:this.ColumnEncryptionKeyNameParameter(y);break;
                case ColumnEncryptionTypeParameter y:this.ColumnEncryptionTypeParameter(y);break;
                case ColumnEncryptionAlgorithmParameter y:this.ColumnEncryptionAlgorithmParameter(y);break;
            }
        }
        /// <summary>
        ///ColumnEncryptionDefinitionParameter:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ColumnEncryptionKeyNameParameter(ColumnEncryptionKeyNameParameter x) {
            this.SwitchIdentifier(x.Name);
        }
        /// <summary>
        ///ColumnEncryptionDefinitionParameter:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ColumnEncryptionTypeParameter(ColumnEncryptionTypeParameter x) {
            throw new NotSupportedException();
        }
        /// <summary>
        ///ColumnEncryptionDefinitionParameter:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ColumnEncryptionAlgorithmParameter(ColumnEncryptionAlgorithmParameter x) {
            this.StringLiteral(x.EncryptionAlgorithm);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void IdentityOptions(IdentityOptions x) {
            this.ScalarExpression(x.IdentityIncrement);
            this.ScalarExpression(x.IdentitySeed);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ColumnStorageOptions(ColumnStorageOptions x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ConstraintDefinition(ConstraintDefinition x) {
            switch(x) {
                case CheckConstraintDefinition y:this.CheckConstraintDefinition(y);break;
                case DefaultConstraintDefinition y:this.DefaultConstraintDefinition(y);break;
                case ForeignKeyConstraintDefinition y:this.ForeignKeyConstraintDefinition(y);break;
                case NullableConstraintDefinition y:this.NullableConstraintDefinition(y);break;
                case GraphConnectionConstraintDefinition y:this.GraphConnectionConstraintDefinition(y);break;
                case UniqueConstraintDefinition y:this.UniqueConstraintDefinition(y);break;
            }
        }
        /// <summary>
        ///ConstraintDefinition:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CheckConstraintDefinition(CheckConstraintDefinition x) {
            this.SwitchIdentifier(x.ConstraintIdentifier);
            this.BooleanExpression(x.CheckCondition);
        }
        /// <summary>
        ///ConstraintDefinition:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DefaultConstraintDefinition(DefaultConstraintDefinition x) {
            this.SwitchIdentifier(x.ConstraintIdentifier);
            this.SwitchIdentifier(x.Column);
            this.ScalarExpression(x.Expression);
        }
        /// <summary>
        ///ConstraintDefinition:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ForeignKeyConstraintDefinition(ForeignKeyConstraintDefinition x) {
            this.SwitchIdentifier(x.ConstraintIdentifier);
            this.Identifiers(x.Columns);
            this.Identifiers(x.ReferencedTableColumns);
            this.SwitchSchemaObjectName(x.ReferenceTableName);
        }
        /// <summary>
        ///ConstraintDefinition:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void NullableConstraintDefinition(NullableConstraintDefinition x) {
            if(x.ConstraintIdentifier is not null)this.SwitchIdentifier(x.ConstraintIdentifier);
            var Nullable=x.Nullable;
        }
        /// <summary>
        ///ConstraintDefinition:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void GraphConnectionConstraintDefinition(GraphConnectionConstraintDefinition x) {
            this.SwitchIdentifier(x.ConstraintIdentifier);
            foreach(var a in x.FromNodeToNodeList)this.GraphConnectionBetweenNodes(a);
        }
        /// <summary>
        ///ConstraintDefinition:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void UniqueConstraintDefinition(UniqueConstraintDefinition x) {
            if(x.ConstraintIdentifier is not null)this.SwitchIdentifier(x.ConstraintIdentifier);
            this.ColumnWithSortOrders(x.Columns);
            if(x.FileStreamOn is not null)this.IdentifierOrValueExpression(x.FileStreamOn);
            this.IndexOptions(x.IndexOptions);
            if(x.IndexType is not null)this.IndexType(x.IndexType);
            if(x.OnFileGroupOrPartitionScheme is not null)this.FileGroupOrPartitionScheme(x.OnFileGroupOrPartitionScheme);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FederationScheme(FederationScheme x) {
            this.SwitchIdentifier(x.ColumnName);
            this.SwitchIdentifier(x.DistributionName);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableDistributionPolicy(TableDistributionPolicy x) {
            switch(x) {
                case TableReplicateDistributionPolicy y:this.TableReplicateDistributionPolicy(y);break;
                case TableRoundRobinDistributionPolicy y:this.TableRoundRobinDistributionPolicy(y);break;
                case TableHashDistributionPolicy y:this.TableHashDistributionPolicy(y);break;
            }
        }
        /// <summary>
        ///TableDistributionPolicy:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableReplicateDistributionPolicy(TableReplicateDistributionPolicy x) {
            throw new NotSupportedException();
        }
        /// <summary>
        ///TableDistributionPolicy:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableRoundRobinDistributionPolicy(TableRoundRobinDistributionPolicy x){
            throw new NotSupportedException();
        }
        /// <summary>
        ///TableDistributionPolicy:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableHashDistributionPolicy(TableHashDistributionPolicy x) {
            this.SwitchIdentifier(x.DistributionColumn);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableIndexType(TableIndexType x) {
            switch(x) {
                case TableClusteredIndexType y:this.TableClusteredIndexType(y);break;
                case TableNonClusteredIndexType y:this.TableNonClusteredIndexType(y);break;
            }
        }
        /// <summary>
        ///TableIndexType:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableClusteredIndexType(TableClusteredIndexType x) {
            this.ColumnWithSortOrders(x.Columns);
        }
        /// <summary>
        ///TableIndexType:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableNonClusteredIndexType(TableNonClusteredIndexType x){
            throw new NotSupportedException();
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void PartitionSpecifications(PartitionSpecifications x) {
            switch(x) {
                case TablePartitionOptionSpecifications y:this.TablePartitionOptionSpecifications(y);break;
            }
        }
        /// <summary>
        ///PartitionSpecifications:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TablePartitionOptionSpecifications(TablePartitionOptionSpecifications x){
            this.ScalarExpressions(x.BoundaryValues);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CompressionPartitionRange(CompressionPartitionRange x) {
            this.ScalarExpression(x.From);
            this.ScalarExpression(x.To);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void GraphConnectionBetweenNodes(GraphConnectionBetweenNodes x) {
            this.SchemaObjectName(x.FromNode);
            this.SchemaObjectName(x.ToNode);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RestoreOption(RestoreOption x) {
            switch(x) {
                case ScalarExpressionRestoreOption y:this.ScalarExpressionRestoreOption(y);break;
                case MoveRestoreOption y:this.MoveRestoreOption(y);break;
                case StopRestoreOption y:this.StopRestoreOption(y);break;
                case FileStreamRestoreOption y:this.FileStreamRestoreOption(y);break;
            }
        }
        /// <summary>
        ///RestoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ScalarExpressionRestoreOption(ScalarExpressionRestoreOption x) {
            this.ScalarExpression(x.Value);
        }
        /// <summary>
        ///RestoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MoveRestoreOption(MoveRestoreOption x) {
            this.ValueExpression(x.LogicalFileName);
            this.ValueExpression(x.OSFileName);
        }
        /// <summary>
        ///RestoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void StopRestoreOption(StopRestoreOption x){
            this.ValueExpression(x.After);
            this.ValueExpression(x.Mark);
        }
        /// <summary>
        ///RestoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FileStreamRestoreOption(FileStreamRestoreOption x) {
            this.FileStreamDatabaseOption(x.FileStreamOption);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BackupOption(BackupOption x) {
            switch(x) {
                case BackupEncryptionOption y:this.BackupEncryptionOption(y);break;
            }
        }
        /// <summary>
        ///BackupOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BackupEncryptionOption(BackupEncryptionOption x) {
            this.CryptoMechanism(x.Encryptor);
            this.ScalarExpression(x.Value);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DeviceInfo(DeviceInfo x) {
            this.IdentifierOrValueExpression(x.LogicalDevice);
            this.ValueExpression(x.PhysicalDevice);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MirrorToClause(MirrorToClause x) {
            this.DeviceInfos(x.Devices);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BackupRestoreFileInfo(BackupRestoreFileInfo x) {
            this.ValueExpressions(x.Items);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BulkInsertOption(BulkInsertOption x) {
            switch(x) {
                case LiteralBulkInsertOption y:this.LiteralBulkInsertOption(y);break;
                case OrderBulkInsertOption y:this.OrderBulkInsertOption(y);break;
            }
        }
        /// <summary>
        ///BulkInsertOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LiteralBulkInsertOption(LiteralBulkInsertOption x) {
            this.Literal(x.Value);
        }
        /// <summary>
        ///BulkInsertOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OrderBulkInsertOption(OrderBulkInsertOption x) {
            this.ColumnWithSortOrders(x.Columns);
        }
        /// <summary>
        ///BulkInsertOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExternalTableColumnDefinition(ExternalTableColumnDefinition x) {
            this.ColumnDefinitionBase(x.ColumnDefinition);
            if(x.NullableConstraint is not null) this.NullableConstraintDefinition(x.NullableConstraint);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void InsertBulkColumnDefinition(InsertBulkColumnDefinition x) {
            this.ColumnDefinitionBase(x.Column);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DbccOption(DbccOption x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DbccNamedLiteral(DbccNamedLiteral x) {
            this.ScalarExpression(x.Value);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void PartitionParameterType(PartitionParameterType x) {
            if(x.Collation is not null)this.SwitchIdentifier(x.Collation);
            this.DataTypeReference(x.DataType);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RemoteServiceBindingOption(RemoteServiceBindingOption x) {
            switch(x) {
                case OnOffRemoteServiceBindingOption y:this.OnOffRemoteServiceBindingOption(y);break;
                case UserRemoteServiceBindingOption y:this.UserRemoteServiceBindingOption(y);break;
            }
        }
        /// <summary>
        ///RemoteServiceBindingOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OnOffRemoteServiceBindingOption(OnOffRemoteServiceBindingOption x) {
        }
        /// <summary>
        ///RemoteServiceBindingOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void UserRemoteServiceBindingOption(UserRemoteServiceBindingOption x) {
            this.SwitchIdentifier(x.User);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void EncryptionSource(EncryptionSource x) {
            switch(x) {
                case AssemblyEncryptionSource y:this.AssemblyEncryptionSource(y);break;
                case FileEncryptionSource y:this.FileEncryptionSource(y);break;
                case ProviderEncryptionSource y:this.ProviderEncryptionSource(y);break;
            }
        }
        /// <summary>
        ///EncryptionSource:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AssemblyEncryptionSource(AssemblyEncryptionSource x) {
            this.SwitchIdentifier(x.Assembly);
        }
        /// <summary>
        ///EncryptionSource:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FileEncryptionSource(FileEncryptionSource x) {
            this.Literal(x.File);
        }
        /// <summary>
        ///EncryptionSource:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ProviderEncryptionSource(ProviderEncryptionSource x) {
            this.KeyOptions(x.KeyOptions);
            this.SwitchIdentifier(x.Name);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CertificateOption(CertificateOption x) {
            this.Literal(x.Value);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ContractMessage(ContractMessage x) {
            this.SwitchIdentifier(x.Name);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void EndpointAffinity(EndpointAffinity x) {
            this.Literal(x.Value);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void EndpointProtocolOption(EndpointProtocolOption x) {
            switch(x) {
                case LiteralEndpointProtocolOption y:this.LiteralEndpointProtocolOption(y);break;
                case AuthenticationEndpointProtocolOption y:this.AuthenticationEndpointProtocolOption(y);break;
                case PortsEndpointProtocolOption y:this.PortsEndpointProtocolOption(y);break;
                case CompressionEndpointProtocolOption y:this.CompressionEndpointProtocolOption(y);break;
                case ListenerIPEndpointProtocolOption y:this.ListenerIPEndpointProtocolOption(y);break;
            }
        }
        /// <summary>
        ///EndpointProtocolOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LiteralEndpointProtocolOption(LiteralEndpointProtocolOption x) {
            this.Literal(x.Value);
        }
        /// <summary>
        ///EndpointProtocolOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AuthenticationEndpointProtocolOption(AuthenticationEndpointProtocolOption x) {
        }
        /// <summary>
        ///EndpointProtocolOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void PortsEndpointProtocolOption(PortsEndpointProtocolOption x) {
        }
        /// <summary>
        ///EndpointProtocolOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CompressionEndpointProtocolOption(CompressionEndpointProtocolOption x) {
        }
        /// <summary>
        ///EndpointProtocolOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ListenerIPEndpointProtocolOption(ListenerIPEndpointProtocolOption x) {
            this.Literal(x.IPv6);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void IPv4(IPv4 x) {
            this.Literal(x.OctetOne);
            this.Literal(x.OctetTwo);
            this.Literal(x.OctetThree);
            this.Literal(x.OctetFour);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void PayloadOption(PayloadOption x) {
            switch(x) {
                case SoapMethod y:this.SoapMethod(y);break;
                case EnabledDisabledPayloadOption y:this.EnabledDisabledPayloadOption(y);break;
                case WsdlPayloadOption y:this.WsdlPayloadOption(y);break;
                case LoginTypePayloadOption y:this.LoginTypePayloadOption(y);break;
                case LiteralPayloadOption y:this.LiteralPayloadOption(y);break;
                case SessionTimeoutPayloadOption y:this.SessionTimeoutPayloadOption(y);break;
                case SchemaPayloadOption y:this.SchemaPayloadOption(y);break;
                case CharacterSetPayloadOption y:this.CharacterSetPayloadOption(y);break;
                case RolePayloadOption y:this.RolePayloadOption(y);break;
                case AuthenticationPayloadOption y:this.AuthenticationPayloadOption(y);break;
                case EncryptionPayloadOption y:this.EncryptionPayloadOption(y);break;
            }
        }
        /// <summary>
        ///PayloadOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SoapMethod(SoapMethod x) {
            this.Literal(x.Alias);
            this.Literal(x.Name);
            this.Literal(x.Namespace);
        }
        /// <summary>
        ///PayloadOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void EnabledDisabledPayloadOption(EnabledDisabledPayloadOption x) {
        }
        /// <summary>
        ///PayloadOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void WsdlPayloadOption(WsdlPayloadOption x) {
            this.Literal(x.Value);
        }
        /// <summary>
        ///PayloadOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LoginTypePayloadOption(LoginTypePayloadOption x) {
        }
        /// <summary>
        ///PayloadOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LiteralPayloadOption(LiteralPayloadOption x) {
            this.Literal(x.Value);
        }
        /// <summary>
        ///PayloadOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SessionTimeoutPayloadOption(SessionTimeoutPayloadOption x) {
            this.Literal(x.Timeout);
        }
        /// <summary>
        ///PayloadOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SchemaPayloadOption(SchemaPayloadOption x) {
        }
        /// <summary>
        ///PayloadOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CharacterSetPayloadOption(CharacterSetPayloadOption x) {
        }
        /// <summary>
        ///PayloadOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RolePayloadOption(RolePayloadOption x) {
        }
        /// <summary>
        ///PayloadOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AuthenticationPayloadOption(AuthenticationPayloadOption x) {
            this.SwitchIdentifier(x.Certificate);
        }
        /// <summary>
        ///PayloadOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void EncryptionPayloadOption(EncryptionPayloadOption x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void KeyOption(KeyOption x) {
            switch(x) {
                case KeySourceKeyOption y:this.KeySourceKeyOption(y);break;
                case AlgorithmKeyOption y:this.AlgorithmKeyOption(y);break;
                case IdentityValueKeyOption y:this.IdentityValueKeyOption(y);break;
                case ProviderKeyNameKeyOption y:this.ProviderKeyNameKeyOption(y);break;
                case CreationDispositionKeyOption y:this.CreationDispositionKeyOption(y);break;
            }
        }
        /// <summary>
        ///KeyOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void KeySourceKeyOption(KeySourceKeyOption x) {
            this.Literal(x.PassPhrase);
        }
        /// <summary>
        ///KeyOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlgorithmKeyOption(AlgorithmKeyOption x) {
        }
        /// <summary>
        ///KeyOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void IdentityValueKeyOption(IdentityValueKeyOption x) {
            this.Literal(x.IdentityPhrase);
        }
        /// <summary>
        ///KeyOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ProviderKeyNameKeyOption(ProviderKeyNameKeyOption x) {
            this.Literal(x.KeyName);
        }
        /// <summary>
        ///KeyOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreationDispositionKeyOption(CreationDispositionKeyOption x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FullTextCatalogOption(FullTextCatalogOption x) {
            switch(x) {
                case OnOffFullTextCatalogOption y:this.OnOffFullTextCatalogOption(y);break;
            }
        }
        /// <summary>
        ///FullTextCatalogOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OnOffFullTextCatalogOption(OnOffFullTextCatalogOption x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ServiceContract(ServiceContract x) {
            this.SwitchIdentifier(x.Name);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ComputeClause(ComputeClause x) {
            this.ScalarExpressions(x.ByExpressions);
            foreach(var a in x.ComputeFunctions)this.ComputeFunction(a);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ComputeFunction(ComputeFunction x) {
            this.ScalarExpression(x.Expression);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TableSampleClause(TableSampleClause x) {
            if(x.RepeatSeed is not null)this.ScalarExpression(x.RepeatSeed);
            this.ScalarExpression(x.SampleNumber);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExpressionWithSortOrder(ExpressionWithSortOrder x) {
            this.ScalarExpression(x.Expression);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void GroupByClause(GroupByClause x) {
            this.GroupingSpecifications(x.GroupingSpecifications);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void GroupingSpecification(GroupingSpecification x) {
            switch(x) {
                case ExpressionGroupingSpecification   y:this.ExpressionGroupingSpecification  (y);break;
                case CompositeGroupingSpecification    y:this.CompositeGroupingSpecification   (y);break;
                case CubeGroupingSpecification         y:this.CubeGroupingSpecification        (y);break;
                case RollupGroupingSpecification       y:this.RollupGroupingSpecification      (y);break;
                case GrandTotalGroupingSpecification   y:this.GrandTotalGroupingSpecification  (y);break;
                case GroupingSetsGroupingSpecification y:this.GroupingSetsGroupingSpecification(y);break;
            }
        }
        /// <summary>
        ///GroupingSpecification:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExpressionGroupingSpecification(ExpressionGroupingSpecification x) {
            this.ScalarExpression(x.Expression);
        }
        /// <summary>
        ///GroupingSpecification:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CompositeGroupingSpecification(CompositeGroupingSpecification x) {
            this.GroupingSpecifications(x.Items);
        }
        /// <summary>
        ///GroupingSpecification:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CubeGroupingSpecification(CubeGroupingSpecification x) {
            this.GroupingSpecifications(x.Arguments);
        }
        /// <summary>
        ///GroupingSpecification:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void RollupGroupingSpecification(RollupGroupingSpecification x) {
            this.GroupingSpecifications(x.Arguments);
        }
        /// <summary>
        ///GroupingSpecification:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void GrandTotalGroupingSpecification(GrandTotalGroupingSpecification x) {
        }
        /// <summary>
        ///GroupingSpecification:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void GroupingSetsGroupingSpecification(GroupingSetsGroupingSpecification x) {
            foreach(var a in x.Sets)this.GroupingSpecification(a);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OutputClause(OutputClause x) {
            this.SelectElements(x.SelectColumns);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OutputIntoClause(OutputIntoClause x) {
            this.TableReference(x.IntoTable);
            this.ColumnReferenceExpressions(x.IntoTableColumns);
            this.SelectElements(x.SelectColumns);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void HavingClause(HavingClause x) {
            if(x.SearchCondition is not null)this.BooleanExpression(x.SearchCondition);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OrderByClause(OrderByClause x) {
            foreach(var a in x.OrderByElements)this.ExpressionWithSortOrder(a);
        }
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void QueryExpression(QueryExpression x) {
            switch(x) {
                case QueryParenthesisExpression y:this.QueryParenthesisExpression(y);break;
                case QuerySpecification         y:this.QuerySpecification        (y);break;
                case BinaryQueryExpression      y:this.BinaryQueryExpression     (y);break;
            }
        }
        /// <summary>
        ///QueryExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void QueryParenthesisExpression(QueryParenthesisExpression x) {
            if(x.ForClause is not null)this.ForClause(x.ForClause);
            if(x.OrderByClause is not null)this.OrderByClause(x.OrderByClause);
            if(x.OffsetClause is not null)this.OffsetClause(x.OffsetClause);
            this.QueryExpression(x.QueryExpression);
        }
        /// <summary>
        ///QueryExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void QuerySpecification(QuerySpecification x) {
            this.ForClause(x.ForClause);
            if(x.FromClause is not null)this.FromClause(x.FromClause);
            if(x.GroupByClause is not null)this.GroupByClause(x.GroupByClause);
            if(x.HavingClause is not null)this.HavingClause(x.HavingClause);
            if(x.OrderByClause is not null)this.OrderByClause(x.OrderByClause);
            if(x.TopRowFilter is not null)if(x.TopRowFilter is not null)this.TopRowFilter(x.TopRowFilter);
            if(x.WhereClause is not null)this.WhereClause(x.WhereClause);
            if(x.OffsetClause is not null)this.OffsetClause(x.OffsetClause);
            this.SelectElements(x.SelectElements);
        }
        /// <summary>
        ///QueryExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BinaryQueryExpression(BinaryQueryExpression x) {
            this.QueryExpression(x.FirstQueryExpression);
            this.QueryExpression(x.SecondQueryExpression);
            if(x.ForClause is not null)this.ForClause(x.ForClause);
            if(x.OffsetClause is not null)this.OffsetClause(x.OffsetClause);
            if(x.OrderByClause is not null)this.OrderByClause(x.OrderByClause);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FromClause(FromClause x) {
            foreach(var TableReference in x.TableReferences) this.TableReference(TableReference);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SelectElement(SelectElement x) {
            switch(x) {
                case SelectScalarExpression y:this.SelectScalarExpression(y);break;
                case SelectStarExpression   y:this.SelectStarExpression  (y);break;
                case SelectSetVariable      y:this.SelectSetVariable     (y);break;
            }
        }
        /// <summary>
        ///SelectElement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SelectScalarExpression(SelectScalarExpression x) {
            if(x.ColumnName is not null)this.IdentifierOrValueExpression(x.ColumnName);
            this.ScalarExpression(x.Expression);
        }
        /// <summary>
        ///SelectElement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SelectStarExpression(SelectStarExpression x) {
            if(x.Qualifier is not null)this.SwitchMultiPartIdentifier(x.Qualifier);
        }
        /// <summary>
        ///SelectElement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SelectSetVariable(SelectSetVariable x) {
            this.VariableReference(x.Variable);
            this.ScalarExpression(x.Expression);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TopRowFilter(TopRowFilter x) {
            this.ScalarExpression(x.Expression);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OffsetClause(OffsetClause x) {
            this.ScalarExpression(x.FetchExpression);
            this.ScalarExpression(x.OffsetExpression);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterFullTextIndexAction(AlterFullTextIndexAction x) {
            switch(x) {
                case SimpleAlterFullTextIndexAction                y:this.SimpleAlterFullTextIndexAction               (y);break;
                case SetStopListAlterFullTextIndexAction           y:this.SetStopListAlterFullTextIndexAction          (y);break;
                case SetSearchPropertyListAlterFullTextIndexAction y:this.SetSearchPropertyListAlterFullTextIndexAction(y);break;
                case DropAlterFullTextIndexAction                  y:this.DropAlterFullTextIndexAction                 (y);break;
                case AddAlterFullTextIndexAction                   y:this.AddAlterFullTextIndexAction                  (y);break;
                case AlterColumnAlterFullTextIndexAction           y:this.AlterColumnAlterFullTextIndexAction          (y);break;
            }
        }
        /// <summary>
        ///AlterFullTextIndexAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SimpleAlterFullTextIndexAction(SimpleAlterFullTextIndexAction x) {
        }
        /// <summary>
        ///AlterFullTextIndexAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SetStopListAlterFullTextIndexAction(SetStopListAlterFullTextIndexAction x) {
            this.StopListFullTextIndexOption(x.StopListOption);
        }
        /// <summary>
        ///AlterFullTextIndexAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SetSearchPropertyListAlterFullTextIndexAction(SetSearchPropertyListAlterFullTextIndexAction x) {
            this.SearchPropertyListFullTextIndexOption(x.SearchPropertyListOption);
        }
        /// <summary>
        ///AlterFullTextIndexAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropAlterFullTextIndexAction(DropAlterFullTextIndexAction x) {
            this.Identifiers(x.Columns);
        }
        /// <summary>
        ///AlterFullTextIndexAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AddAlterFullTextIndexAction(AddAlterFullTextIndexAction x) {
            this.FullTextIndexColumns(x.Columns);
        }
        /// <summary>
        ///AlterFullTextIndexAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterColumnAlterFullTextIndexAction(AlterColumnAlterFullTextIndexAction x) {
            this.FullTextIndexColumn(x.Column);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SearchPropertyListAction(SearchPropertyListAction x) {
            switch(x) {
                case AddSearchPropertyListAction  y:this.AddSearchPropertyListAction (y);break;
                case DropSearchPropertyListAction y:this.DropSearchPropertyListAction(y);break;
            }
        }
        /// <summary>
        ///SearchPropertyListAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AddSearchPropertyListAction(AddSearchPropertyListAction x) {
            this.StringLiteral(x.PropertyName);
            this.IntegerLiteral(x.Id);
            this.StringLiteral(x.Guid);
        }
        /// <summary>
        ///SearchPropertyListAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DropSearchPropertyListAction(DropSearchPropertyListAction x) {
            this.StringLiteral(x.PropertyName);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CreateLoginSource(CreateLoginSource x) {
            switch(x) {
                case PasswordCreateLoginSource      y:this.PasswordCreateLoginSource     (y);break;
                case WindowsCreateLoginSource       y:this.WindowsCreateLoginSource      (y);break;
                case CertificateCreateLoginSource   y:this.CertificateCreateLoginSource  (y);break;
                case AsymmetricKeyCreateLoginSource y:this.AsymmetricKeyCreateLoginSource(y);break;
            }
        }
        /// <summary>
        ///CreateLoginSource:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void PasswordCreateLoginSource(PasswordCreateLoginSource x) {
            this.Literal(x.Password);
            this.PrincipalOptions(x.Options);
        }
        /// <summary>
        ///CreateLoginSource:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void WindowsCreateLoginSource(WindowsCreateLoginSource x) {
            this.PrincipalOptions(x.Options);
        }
        /// <summary>
        ///CreateLoginSource:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CertificateCreateLoginSource(CertificateCreateLoginSource x) {
            this.SwitchIdentifier(x.Certificate);
            this.SwitchIdentifier(x.Credential);
        }
        /// <summary>
        ///CreateLoginSource:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AsymmetricKeyCreateLoginSource(AsymmetricKeyCreateLoginSource x) {
            this.SwitchIdentifier(x.Credential);
            this.SwitchIdentifier(x.Key);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void PrincipalOption(PrincipalOption x) {
            switch(x) {
                case OnOffPrincipalOption         y:this.OnOffPrincipalOption        (y);break;
                case LiteralPrincipalOption       y:this.LiteralPrincipalOption      (y);break;
                case IdentifierPrincipalOption    y:this.IdentifierPrincipalOption   (y);break;
                case PasswordAlterPrincipalOption y:this.PasswordAlterPrincipalOption(y);break;
            }
        }
        /// <summary>
        ///PrincipalOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OnOffPrincipalOption(OnOffPrincipalOption x) {
        }
        /// <summary>
        ///PrincipalOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LiteralPrincipalOption(LiteralPrincipalOption x) {
            this.Literal(x.Value);
        }
        /// <summary>
        ///PrincipalOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void IdentifierPrincipalOption(IdentifierPrincipalOption x) {
            this.SwitchIdentifier(x.Identifier);
        }
        /// <summary>
        ///PrincipalOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void PasswordAlterPrincipalOption(PasswordAlterPrincipalOption x) {
            this.Literal(x.OldPassword);
            this.Literal(x.Password);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DialogOption(DialogOption x) {
            switch(x) {
                case ScalarExpressionDialogOption y:this.ScalarExpressionDialogOption(y);break;
                case OnOffDialogOption            y:this.OnOffDialogOption           (y);break;
            }
        }
        /// <summary>
        ///DialogOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ScalarExpressionDialogOption(ScalarExpressionDialogOption x) {
            this.ScalarExpression(x.Value);
        }
        /// <summary>
        ///DialogOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OnOffDialogOption(OnOffDialogOption x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TSqlFragmentSnippet(TSqlFragmentSnippet x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TSqlScript(TSqlScript x) {
            foreach(var Batch in x.Batches) {
                this.TSqlBatch(Batch);
            }
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TSqlBatch(TSqlBatch x) {
            this.Statements(x.Statements);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MergeActionClause(MergeActionClause x) {
            this.MergeAction(x.Action);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MergeAction(MergeAction x) {
            switch(x) {
                case UpdateMergeAction y:this.UpdateMergeAction(y);break;
                case DeleteMergeAction y:this.DeleteMergeAction(y);break;
                case InsertMergeAction y:this.InsertMergeAction(y);break;
            }
        }
        /// <summary>
        ///MergeAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void UpdateMergeAction(UpdateMergeAction x) {
            this.SetClauses(x.SetClauses);
        }
        /// <summary>
        ///MergeAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DeleteMergeAction(DeleteMergeAction x) {
        }
        /// <summary>
        ///MergeAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void InsertMergeAction(InsertMergeAction x) {
            this.ColumnReferenceExpressions(x.Columns);
            this.ValuesInsertSource(x.Source);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AuditSpecificationPart(AuditSpecificationPart x) {
            this.AuditSpecificationDetail(x.Details);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AuditSpecificationDetail(AuditSpecificationDetail x) {
            switch(x) {
                case AuditActionSpecification y:this.AuditActionSpecification(y);break;
                case AuditActionGroupReference y:this.AuditActionGroupReference(y);break;
            }
        }
        /// <summary>
        ///AuditSpecificationDetail:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AuditActionSpecification(AuditActionSpecification x) {
            foreach(var a in x.Actions)this.DatabaseAuditAction(a);
            this.SecurityPrincipals(x.Principals);
        }
        /// <summary>
        ///AuditSpecificationDetail:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AuditActionGroupReference(AuditActionGroupReference x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DatabaseAuditAction(DatabaseAuditAction x) {
        }
        protected virtual void AuditTarget(AuditTarget x) {
            foreach(var a in x.TargetOptions)this.AuditTargetOption(a);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AuditOption(AuditOption x) {
            switch(x) {
                case QueueDelayAuditOption y:this.QueueDelayAuditOption(y);break;
                case AuditGuidAuditOption y:this.AuditGuidAuditOption(y);break;
                case OnFailureAuditOption y:this.OnFailureAuditOption(y);break;
                case StateAuditOption     y:this.StateAuditOption    (y);break;
            }
        }
        /// <summary>
        ///AuditOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void QueueDelayAuditOption(QueueDelayAuditOption x) {
            this.Literal(x.Delay);
        }
        /// <summary>
        ///AuditOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AuditGuidAuditOption(AuditGuidAuditOption x) {
            this.Literal(x.Guid);
        }
        /// <summary>
        ///AuditOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OnFailureAuditOption(OnFailureAuditOption x) {
        }
        /// <summary>
        ///AuditOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void StateAuditOption(StateAuditOption x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AuditTargetOption(AuditTargetOption x) {
            switch(x) {
                case MaxSizeAuditTargetOption          y:this.MaxSizeAuditTargetOption         (y);break;
                case MaxRolloverFilesAuditTargetOption y:this.MaxRolloverFilesAuditTargetOption(y);break;
                case LiteralAuditTargetOption          y:this.LiteralAuditTargetOption         (y);break;
                case OnOffAuditTargetOption            y:this.OnOffAuditTargetOption           (y);break;
            }
        }
        /// <summary>
        ///AuditTargetOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MaxSizeAuditTargetOption(MaxSizeAuditTargetOption x) {
            this.Literal(x.Size);
        }
        /// <summary>
        ///AuditTargetOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MaxRolloverFilesAuditTargetOption(MaxRolloverFilesAuditTargetOption x) {
            this.Literal(x.Value);
        }
        /// <summary>
        ///AuditTargetOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LiteralAuditTargetOption(LiteralAuditTargetOption x) {
            this.Literal(x.Value);
        }
        /// <summary>
        ///AuditTargetOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OnOffAuditTargetOption(OnOffAuditTargetOption x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ResourcePoolParameter(ResourcePoolParameter x) {
            this.ResourcePoolAffinitySpecification(x.AffinitySpecification);
            this.Literal(x.ParameterValue);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ResourcePoolAffinitySpecification(ResourcePoolAffinitySpecification x) {
            this.Literal(x.ParameterValue);
            this.PoolAffinityRanges(x.PoolAffinityRanges);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExternalResourcePoolParameter(ExternalResourcePoolParameter x) {
            this.ExternalResourcePoolAffinitySpecification(x.AffinitySpecification);
            this.Literal(x.ParameterValue);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void ExternalResourcePoolAffinitySpecification(ExternalResourcePoolAffinitySpecification x) {
            this.Literal(x.ParameterValue);
            this.PoolAffinityRanges(x.PoolAffinityRanges);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void WorkloadGroupParameter(WorkloadGroupParameter x) {
            switch(x) {
                case WorkloadGroupResourceParameter   y:this.WorkloadGroupResourceParameter  (y);break;
                case WorkloadGroupImportanceParameter y:this.WorkloadGroupImportanceParameter(y);break;
            }
        }
        /// <summary>
        ///WorkloadGroupParameter:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void WorkloadGroupResourceParameter(WorkloadGroupResourceParameter x) {
            this.Literal(x.ParameterValue);
        }
        /// <summary>
        ///WorkloadGroupParameter:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void WorkloadGroupImportanceParameter(WorkloadGroupImportanceParameter x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BrokerPriorityParameter(BrokerPriorityParameter x) {
            this.IdentifierOrValueExpression(x.ParameterValue);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FullTextStopListAction(FullTextStopListAction x) {
            this.IdentifierOrValueExpression(x.LanguageTerm);
            this.Literal(x.StopWord);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void EventSessionObjectName(EventSessionObjectName x) {
            this.SwitchMultiPartIdentifier(x.MultiPartIdentifier);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void EventDeclaration(EventDeclaration x) {
            this.EventSessionObjectName(x.ObjectName);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void EventDeclarationSetParameter(EventDeclarationSetParameter x) {
            this.SwitchIdentifier(x.EventField);
            this.ScalarExpression(x.EventValue);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TargetDeclaration(TargetDeclaration x) {
            this.EventSessionObjectName(x.ObjectName);
            foreach(var a in x.TargetDeclarationParameters)this.EventDeclarationSetParameter(a);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SessionOption(SessionOption x) {
            switch(x) {
                case EventRetentionSessionOption     y:this.EventRetentionSessionOption    (y);break;
                case MemoryPartitionSessionOption    y:this.MemoryPartitionSessionOption   (y);break;
                case LiteralSessionOption            y:this.LiteralSessionOption           (y);break;
                case MaxDispatchLatencySessionOption y:this.MaxDispatchLatencySessionOption(y);break;
                case OnOffSessionOption              y:this.OnOffSessionOption             (y);break;
            }
        }
        /// <summary>
        ///SessionOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void EventRetentionSessionOption(EventRetentionSessionOption x) {
        }
        /// <summary>
        ///SessionOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MemoryPartitionSessionOption(MemoryPartitionSessionOption x) {
        }
        /// <summary>
        ///SessionOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LiteralSessionOption(LiteralSessionOption x) {
        }
        /// <summary>
        ///SessionOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void MaxDispatchLatencySessionOption(MaxDispatchLatencySessionOption x) {
        }
        /// <summary>
        ///SessionOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void OnOffSessionOption(OnOffSessionOption x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SpatialIndexOption(SpatialIndexOption x) {
            switch(x) {
                case SpatialIndexRegularOption        y:this.SpatialIndexRegularOption       (y);break;
                case BoundingBoxSpatialIndexOption    y:this.BoundingBoxSpatialIndexOption   (y);break;
                case GridsSpatialIndexOption          y:this.GridsSpatialIndexOption         (y);break;
                case CellsPerObjectSpatialIndexOption y:this.CellsPerObjectSpatialIndexOption(y);break;
            }
        }
        /// <summary>
        ///SpatialIndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SpatialIndexRegularOption(SpatialIndexRegularOption x) {
            this.IndexOption(x.Option);
        }
        /// <summary>
        ///SpatialIndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BoundingBoxSpatialIndexOption(BoundingBoxSpatialIndexOption x) {
            foreach(var a in x.BoundingBoxParameters)this.BoundingBoxParameter(a);
        }
        /// <summary>
        ///SpatialIndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void GridsSpatialIndexOption(GridsSpatialIndexOption x) {
            foreach(var a in x.GridParameters)this.GridParameter(a);
        }
        /// <summary>
        ///SpatialIndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void CellsPerObjectSpatialIndexOption(CellsPerObjectSpatialIndexOption x) {
            this.Literal(x.Value);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void BoundingBoxParameter(BoundingBoxParameter x) {
            this.ScalarExpression(x.Value);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void GridParameter(GridParameter x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterServerConfigurationBufferPoolExtensionOption(AlterServerConfigurationBufferPoolExtensionOption x) {
            switch(x) {
                case AlterServerConfigurationBufferPoolExtensionContainerOption y:this.AlterServerConfigurationBufferPoolExtensionContainerOption(y);break;
                case AlterServerConfigurationBufferPoolExtensionSizeOption y:this.AlterServerConfigurationBufferPoolExtensionSizeOption(y);break;
            }
        }
        /// <summary>
        ///AlterServerConfigurationBufferPoolExtensionOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterServerConfigurationBufferPoolExtensionContainerOption(AlterServerConfigurationBufferPoolExtensionContainerOption x) {
            this.OptionValue(x.OptionValue);
            this.AlterServerConfigurationBufferPoolExtensionOptions(x.Suboptions);
        }
        /// <summary>
        ///AlterServerConfigurationBufferPoolExtensionOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterServerConfigurationBufferPoolExtensionSizeOption(AlterServerConfigurationBufferPoolExtensionSizeOption x) {
            this.OptionValue(x.OptionValue);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterServerConfigurationDiagnosticsLogOption(AlterServerConfigurationDiagnosticsLogOption x) {
            switch(x) {
                case AlterServerConfigurationDiagnosticsLogMaxSizeOption y:this.AlterServerConfigurationDiagnosticsLogMaxSizeOption(y);break;
            }
        }
        /// <summary>
        ///AlterServerConfigurationDiagnosticsLogOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterServerConfigurationDiagnosticsLogMaxSizeOption(AlterServerConfigurationDiagnosticsLogMaxSizeOption x) {
            this.OptionValue(x.OptionValue);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterServerConfigurationFailoverClusterPropertyOption(AlterServerConfigurationFailoverClusterPropertyOption x) {
            this.OptionValue(x.OptionValue);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterServerConfigurationHadrClusterOption(AlterServerConfigurationHadrClusterOption x) {
            this.OptionValue(x.OptionValue);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterServerConfigurationSoftNumaOption(AlterServerConfigurationSoftNumaOption x) {
            this.OptionValue(x.OptionValue);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AvailabilityReplica(AvailabilityReplica x) {
            foreach(var a in x.Options)this.AvailabilityReplicaOption(a);
            this.StringLiteral(x.ServerName);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AvailabilityReplicaOption(AvailabilityReplicaOption x) {
            switch(x) {
                case LiteralReplicaOption          y:this.LiteralReplicaOption         (y);break;
                case AvailabilityModeReplicaOption y:this.AvailabilityModeReplicaOption(y);break;
                case FailoverModeReplicaOption     y:this.FailoverModeReplicaOption    (y);break;
                case PrimaryRoleReplicaOption      y:this.PrimaryRoleReplicaOption     (y);break;
                case SecondaryRoleReplicaOption    y:this.SecondaryRoleReplicaOption   (y);break;
            }
        }
        /// <summary>
        ///AvailabilityReplicaOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LiteralReplicaOption(LiteralReplicaOption x) {
        }
        /// <summary>
        ///AvailabilityReplicaOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AvailabilityModeReplicaOption(AvailabilityModeReplicaOption x) {
            //x.Value=AvailabilityModeOptionKind.AsynchronousCommit
        }
        /// <summary>
        ///AvailabilityReplicaOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void FailoverModeReplicaOption(FailoverModeReplicaOption x) {
        }
        /// <summary>
        ///AvailabilityReplicaOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void PrimaryRoleReplicaOption(PrimaryRoleReplicaOption x) {
        }
        /// <summary>
        ///AvailabilityReplicaOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SecondaryRoleReplicaOption(SecondaryRoleReplicaOption x) {
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AvailabilityGroupOption(AvailabilityGroupOption x) {
            switch(x) {
                case LiteralAvailabilityGroupOption y:this.LiteralAvailabilityGroupOption(y);break;
            }
        }
        /// <summary>
        ///AvailabilityGroupOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void LiteralAvailabilityGroupOption(LiteralAvailabilityGroupOption x) {
            this.Literal(x.Value);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterAvailabilityGroupAction(AlterAvailabilityGroupAction x) {
            switch(x) {
                case AlterAvailabilityGroupFailoverAction y:this.AlterAvailabilityGroupFailoverAction(y);break;
            }
        }
        /// <summary>
        ///AlterAvailabilityGroupAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterAvailabilityGroupFailoverAction(AlterAvailabilityGroupFailoverAction x) {
            foreach(var a in x.Options)this.AlterAvailabilityGroupFailoverOption(a);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void AlterAvailabilityGroupFailoverOption(AlterAvailabilityGroupFailoverOption x) {
            this.Literal(x.Value);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void DiskStatementOption(DiskStatementOption x) {
            this.IdentifierOrValueExpression(x.Value);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void WindowFrameClause(WindowFrameClause x) {
            this.WindowDelimiter(x.Bottom);
            this.WindowDelimiter(x.Top);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void WindowDelimiter(WindowDelimiter x) {
            this.ScalarExpression(x.OffsetValue);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void WithinGroupClause(WithinGroupClause x) {
            this.OrderByClause(x.OrderByClause);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void SelectiveXmlIndexPromotedPath(SelectiveXmlIndexPromotedPath x) {
            this.IntegerLiteral(x.MaxLength);
            this.SwitchIdentifier(x.Name);
            this.TSqlFragment(x.Path);
            this.DataTypeReference(x.SQLDataType);
            this.Literal(x.XQueryDataType);
        }
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual void TemporalClause(TemporalClause x) {
            this.TSqlFragment(x.StartTime);
            this.TSqlFragment(x.EndTime);
        }
    }
}
//10034
