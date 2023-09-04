#define アセンブリに保存
//#define タイム出力
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using AssemblyName = Microsoft.SqlServer.TransactSql.ScriptDom.AssemblyName;
namespace LinqDB.Optimizers;

/// <summary>
/// Expressionを最適化する
/// </summary>
public sealed partial class Optimizer{
    /// <summary>
    /// TSQLからLINQに変換する。
    /// </summary>
    public class ReturnTSqlFragmentTraverser{
        /// <summary>
        ///リフレクションで使う作業領域確保によるGCを防ぐ。
        /// </summary>
        protected readonly 作業配列 作業配列;
        protected readonly SqlScriptGenerator ScriptGenerator;
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="作業配列"></param>
        public ReturnTSqlFragmentTraverser(作業配列 作業配列,SqlScriptGenerator ScriptGenerator) {
            this.作業配列=作業配列;
            this.ScriptGenerator=ScriptGenerator;
        }
        private static T Throw<T>(T x)where T: TSqlFragment => throw new NotSupportedException(x.ToString());
        /// <summary>
        /// 起点
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TSqlFragment XmlNamespaces(TSqlFragment x) => x switch{
            MultiPartIdentifier                                   y => this.MultiPartIdentifier(y),
            Identifier                                            y => this.Identifier(y),
            ScalarExpression                                      y => this.ScalarExpression(y),
            IdentifierOrValueExpression                           y => this.IdentifierOrValueExpression(y),
            StatementList                                         y => this.StatementList(y),
            TSqlStatement                                         y => this.TSqlStatement(y),
            ExecuteOption                                         y => this.ExecuteOption(y),
            ResultSetDefinition                                   y => this.ResultSetDefinition(y),
            ResultColumnDefinition                                y => this.ResultColumnDefinition(y),
            ExecuteSpecification                                  y => this.ExecuteSpecification(y),
            ExecuteContext                                        y => this.ExecuteContext(y),
            ExecuteParameter                                      y => this.ExecuteParameter(y),
            ExecutableEntity                                      y => this.ExecutableEntity(y),
            ProcedureReferenceName                                y => this.ProcedureReferenceName(y),
            AdHocDataSource                                       y => this.AdHocDataSource(y),
            ViewOption                                            y => this.ViewOption(y),
            TriggerObject                                         y => this.TriggerObject(y),
            TriggerOption                                         y => this.TriggerOption(y),
            TriggerAction                                         y => this.TriggerAction(y),
            ProcedureReference                                    y => this.ProcedureReference(y),
            MethodSpecifier                                       y => this.MethodSpecifier(y),
            ProcedureOption                                       y => this.ProcedureOption(y),
            FunctionOption                                        y => this.FunctionOption(y),
            XmlNamespaces                                         y => this.XmlNamespaces(y),
            XmlNamespacesElement                                  y => this.XmlNamespacesElement(y),
            CommonTableExpression                                 y => this.CommonTableExpression(y),
            WithCtesAndXmlNamespaces                              y => this.WithCtesAndXmlNamespaces(y),
            FunctionReturnType                                    y => this.FunctionReturnType(y),
            DataTypeReference                                     y => this.DataTypeReference(y),
            TableDefinition                                       y => this.TableDefinition(y),
            DeclareTableVariableBody                              y => this.DeclareTableVariableBody(y),
            TableReference                                        y => this.TableReference(y),
            TableHint                                             y => this.TableHint(y),
            BooleanExpression                                     y => this.BooleanExpression(y),
            ForClause                                             y => this.ForClause(y),
            OptimizerHint                                         y => this.OptimizerHint(y),
            VariableValuePair                                     y => this.VariableValuePair(y),
            WhenClause                                            y => this.WhenClause(y),
            SchemaDeclarationItem                                 y => this.SchemaDeclarationItem(y),
            CallTarget                                            y => this.CallTarget(y),
            OverClause                                            y => this.OverClause(y),
            AtomicBlockOption                                     y => this.AtomicBlockOption(y),
            ColumnWithSortOrder                                   y => this.ColumnWithSortOrder(y),
            DeclareVariableElement                                y => this.DeclareVariableElement(y),
            DataModificationSpecification                         y => this.DataModificationSpecification(y),
            Permission                                            y => this.Permission(y),
            SecurityTargetObject                                  y => this.SecurityTargetObject(y),
            SecurityTargetObjectName                              y => this.SecurityTargetObjectName(y),
            SecurityPrincipal                                     y => this.SecurityPrincipal(y),
            SecurityElement80                                     y => this.SecurityElement80(y),
            Privilege80                                           y => this.Privilege80(y),
            SecurityUserClause80                                  y => this.SecurityUserClause80(y),
            SetClause                                             y => this.SetClause(y),
            InsertSource                                          y => this.InsertSource(y),
            RowValue                                              y => this.RowValue(y),
            LiteralRange                                          y => this.LiteralRange(y),
            OptionValue                                           y => this.OptionValue(y),
            IdentifierOrScalarExpression                          y => this.Traverse(y),
            SchemaObjectNameOrValueExpression                     y => this.Traverse(y),
            SequenceOption                                        y => this.Traverse(y),
            SecurityPredicateAction                               y => this.Traverse(y),
            SecurityPolicyOption                                  y => this.Traverse(y),
            ColumnMasterKeyParameter                              y => this.Traverse(y),
            ColumnEncryptionKeyValue                              y => this.Traverse(y),
            ColumnEncryptionKeyValueParameter                     y => this.Traverse(y),
            ExternalTableOption                                   y => this.Traverse(y),
            ExternalTableDistributionPolicy                       y => this.Traverse(y),
            ExternalDataSourceOption                              y => this.Traverse(y),
            ExternalFileFormatOption                              y => this.Traverse(y),
            AssemblyOption                                        y => this.Traverse(y),
            AddFileSpec                                           y => this.Traverse(y),
            AssemblyName                                          y => this.Traverse(y),
            TableOption                                           y => this.Traverse(y),
            DatabaseOption                                        y => this.Traverse(y),
            RemoteDataArchiveDatabaseSetting                      y => this.Traverse(y),
            RetentionPeriodDefinition                             y => this.Traverse(y),
            TableSwitchOption                                     y => this.Traverse(y),
            DropClusteredConstraintOption                         y => this.Traverse(y),
            AlterTableDropTableElement                            y => this.Traverse(y),
            ExecuteAsClause                                       y => this.Traverse(y),
            QueueOption                                           y => this.Traverse(y),
            RouteOption                                           y => this.Traverse(y),
            SystemTimePeriodDefinition                            y => this.Traverse(y),
            IndexType                                             y => this.Traverse(y),
            PartitionSpecifier                                    y => this.Traverse(y),
            FileGroupOrPartitionScheme                            y => this.Traverse(y),
            IndexOption                                           y => this.Traverse(y),
            OnlineIndexLowPriorityLockWaitOption                  y => this.Traverse(y),
            LowPriorityLockWaitOption                             y => this.Traverse(y),
            FullTextIndexColumn                                   y => this.Traverse(y),
            FullTextIndexOption                                   y => this.Traverse(y),
            FullTextCatalogAndFileGroup                           y => this.Traverse(y),
            EventTypeGroupContainer                               y => this.Traverse(y),
            EventNotificationObjectScope                          y => this.Traverse(y),
            ApplicationRoleOption                                 y => this.Traverse(y),
            AlterRoleAction                                       y => this.Traverse(y),
            UserLoginOption                                       y => this.Traverse(y),
            StatisticsOption                                      y => this.Traverse(y),
            StatisticsPartitionRange                              y => this.Traverse(y),
            CursorDefinition                                      y => this.Traverse(y),
            CursorOption                                          y => this.XmlNamespaces(y),
            CursorId                                              y => this.Traverse(y),
            CryptoMechanism                                       y => this.Traverse(y),
            FetchType                                             y => this.Traverse(y),
            WhereClause                                           y => this.Traverse(y),
            DropIndexClauseBase                                   y => this.Traverse(y),
            SetCommand                                            y => this.Traverse(y),
            FileDeclaration                                       y => this.Traverse(y),
            FileDeclarationOption                                 y => this.Traverse(y),
            FileGroupDefinition                                   y => this.Traverse(y),
            DatabaseConfigurationClearOption                      y => this.Traverse(y),
            DatabaseConfigurationSetOption                        y => this.Traverse(y),
            AlterDatabaseTermination                              y => this.Traverse(y),
            ChangeTrackingOptionDetail                            y => this.Traverse(y),
            QueryStoreOption                                      y => this.Traverse(y),
            AutomaticTuningOption                                 y => this.Traverse(y),
            ColumnDefinitionBase                                  y => this.Traverse(y),
            ColumnEncryptionDefinition                            y => this.Traverse(y),
            ColumnEncryptionDefinitionParameter                   y => this.Traverse(y),
            IdentityOptions                                       y => this.Traverse(y),
            ColumnStorageOptions                                  y => this.Traverse(y),
            ConstraintDefinition                                  y => this.Traverse(y),
            FederationScheme                                      y => this.Traverse(y),
            TableDistributionPolicy                               y => this.Traverse(y),
            TableIndexType                                        y => this.Traverse(y),
            PartitionSpecifications                               y => this.Traverse(y),
            CompressionPartitionRange                             y => this.Traverse(y),
            GraphConnectionBetweenNodes                           y => this.Traverse(y),
            RestoreOption                                         y => this.Traverse(y),
            BackupOption                                          y => this.Traverse(y),
            DeviceInfo                                            y => this.Traverse(y),
            MirrorToClause                                        y => this.Traverse(y),
            BackupRestoreFileInfo                                 y => this.Traverse(y),
            BulkInsertOption                                      y => this.Traverse(y),
            ExternalTableColumnDefinition                         y => this.Traverse(y),
            InsertBulkColumnDefinition                            y => this.Traverse(y),
            DbccOption                                            y => this.Traverse(y),
            DbccNamedLiteral                                      y => this.Traverse(y),
            PartitionParameterType                                y => this.Traverse(y),
            RemoteServiceBindingOption                            y => this.Traverse(y),
            EncryptionSource                                      y => this.Traverse(y),
            CertificateOption                                     y => this.Traverse(y),
            ContractMessage                                       y => this.Traverse(y),
            EndpointAffinity                                      y => this.Traverse(y),
            EndpointProtocolOption                                y => this.Traverse(y),
            IPv4                                                  y => this.Traverse(y),
            PayloadOption                                         y => this.Traverse(y),
            KeyOption                                             y => this.Traverse(y),
            FullTextCatalogOption                                 y => this.Traverse(y),
            ServiceContract                                       y => this.Traverse(y),
            ComputeClause                                         y => this.Traverse(y),
            ComputeFunction                                       y => this.Traverse(y),
            TableSampleClause                                     y => this.Traverse(y),
            ExpressionWithSortOrder                               y => this.Traverse(y),
            GroupByClause                                         y => this.Traverse(y),
            GroupingSpecification                                 y => this.Traverse(y),
            OutputClause                                          y => this.Traverse(y),
            OutputIntoClause                                      y => this.Traverse(y),
            HavingClause                                          y => this.Traverse(y),
            OrderByClause                                         y => this.Traverse(y),
            QueryExpression                                       y => this.QueryExpression(y),
            FromClause                                            y => this.Traverse(y),
            SelectElement                                         y => this.Traverse(y),
            TopRowFilter                                          y => this.Traverse(y),
            OffsetClause                                          y => this.Traverse(y),
            AlterFullTextIndexAction                              y => this.Traverse(y),
            SearchPropertyListAction                              y => this.Traverse(y),
            CreateLoginSource                                     y => this.Traverse(y),
            PrincipalOption                                       y => this.Traverse(y),
            DialogOption                                          y => this.Traverse(y),
            TSqlFragmentSnippet                                   y => this.Traverse(y),
            TSqlScript                                            y => this.Traverse(y),
            TSqlBatch                                             y => this.Traverse(y),
            MergeActionClause                                     y => this.Traverse(y),
            MergeAction                                           y => this.Traverse(y),
            AuditSpecificationPart                                y => this.Traverse(y),
            AuditSpecificationDetail                              y => this.Traverse(y),
            DatabaseAuditAction                                   y => this.Traverse(y),
            AuditTarget                                           y => this.XmlNamespaces(y),
            AuditOption                                           y => this.Traverse(y),
            AuditTargetOption                                     y => this.Traverse(y),
            ResourcePoolParameter                                 y => this.Traverse(y),
            ResourcePoolAffinitySpecification                     y => this.Traverse(y),
            ExternalResourcePoolParameter                         y => this.Traverse(y),
            ExternalResourcePoolAffinitySpecification             y => this.Traverse(y),
            WorkloadGroupParameter                                y => this.Traverse(y),
            BrokerPriorityParameter                               y => this.Traverse(y),
            FullTextStopListAction                                y => this.Traverse(y),
            EventSessionObjectName                                y => this.Traverse(y),
            EventDeclaration                                      y => this.Traverse(y),
            EventDeclarationSetParameter                          y => this.Traverse(y),
            TargetDeclaration                                     y => this.Traverse(y),
            SessionOption                                         y => this.Traverse(y),
            SpatialIndexOption                                    y => this.Traverse(y),
            BoundingBoxParameter                                  y => this.Traverse(y),
            GridParameter                                         y => this.Traverse(y),
            AlterServerConfigurationBufferPoolExtensionOption     y => this.Traverse(y),
            AlterServerConfigurationDiagnosticsLogOption          y => this.Traverse(y),
            AlterServerConfigurationFailoverClusterPropertyOption y => this.Traverse(y),
            AlterServerConfigurationHadrClusterOption             y => this.Traverse(y),
            AlterServerConfigurationSoftNumaOption                y => this.Traverse(y),
            AvailabilityReplica                                   y => this.Traverse(y),
            AvailabilityReplicaOption                             y => this.Traverse(y),
            AvailabilityGroupOption                               y => this.Traverse(y),
            AlterAvailabilityGroupAction                          y => this.Traverse(y),
            AlterAvailabilityGroupFailoverOption                  y => this.Traverse(y),
            DiskStatementOption                                   y => this.Traverse(y),
            WindowFrameClause                                     y => this.Traverse(y),
            WindowDelimiter                                       y => this.Traverse(y),
            WithinGroupClause                                     y => this.Traverse(y),
            SelectiveXmlIndexPromotedPath                         y => this.Traverse(y),
            TemporalClause                                        y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MultiPartIdentifier MultiPartIdentifier(MultiPartIdentifier x)=>x switch {
            SchemaObjectName y => this.SchemaObjectName(y),
            _ =>Throw(x)
        };
        /// <summary>
        /// MultiPartIdentifier:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SchemaObjectName SchemaObjectName(SchemaObjectName x)=>x switch {
            ChildObjectName y => this.SchemaObjectName(y),
            SchemaObjectNameSnippet y => this.SchemaObjectName(y),
            _ =>Throw(x)
        };
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual Identifier Identifier(Identifier x) =>x switch {
            SqlCommandIdentifier y => this.Identifier(y),
            IdentifierSnippet y => this.Identifier(y),
            _ =>Throw(x)
        };
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ScalarExpression ScalarExpression(ScalarExpression x)=>x switch {
            PrimaryExpression y => this.ScalarExpression(y),
            ExtractFromExpression y => this.ScalarExpression(y),
            OdbcConvertSpecification y => this.ScalarExpression(y),
            BinaryExpression y =>this.ScalarExpression(y),
            IdentityFunctionCall y => this.ScalarExpression(y),
            UnaryExpression y => this.ScalarExpression(y),
            ScalarExpressionSnippet y => this.ScalarExpression(y),
            SourceDeclaration y => this.ScalarExpression(y),
            _ =>Throw(x)
        };

        /// <summary>
        /// ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ScalarExpression ScalarExpression(PrimaryExpression x) => x switch{
            ValueExpression y => this.ScalarExpression(y),
            UserDefinedTypePropertyAccess y => this.ScalarExpression(y),
            CaseExpression y => this.ScalarExpression(y),
            NullIfExpression y => this.ScalarExpression(y),
            CoalesceExpression y => this.ScalarExpression(y),
            IIfCall y => this.ScalarExpression(y),
            ConvertCall y => this.ScalarExpression(y),
            TryConvertCall y => this.ScalarExpression(y),
            ParseCall y => this.ScalarExpression(y),
            TryParseCall y => this.ScalarExpression(y),
            CastCall y => this.ScalarExpression(y),
            TryCastCall y => this.ScalarExpression(y),
            AtTimeZoneCall y => this.ScalarExpression(y),
            FunctionCall y => this.ScalarExpression(y),
            LeftFunctionCall y => this.ScalarExpression(y),
            RightFunctionCall y => this.ScalarExpression(y),
            PartitionFunctionCall y => this.ScalarExpression(y),
            ParameterlessCall y => this.ScalarExpression(y),
            ScalarSubquery y => this.ScalarExpression(y),
            OdbcFunctionCall y => this.ScalarExpression(y),
            ParenthesisExpression y => this.ScalarExpression(y),
            ColumnReferenceExpression y => this.ScalarExpression(y),
            NextValueForExpression y => this.ScalarExpression(y),
            _ => Throw(x)
        };
        /// <summary>
        /// PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ValueExpression ScalarExpression(ValueExpression x)=>x switch{
            Literal y=>this.ScalarExpression(y),
            VariableReference y => this.ScalarExpression(y),
            GlobalVariableExpression y => this.ScalarExpression(y),
            _ => Throw(x)
        };
        /// <summary>
        /// ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual Literal ScalarExpression(Literal x) => x switch {
            IdentifierLiteral y => this.ScalarExpression(y),
            IntegerLiteral y => this.ScalarExpression(y),
            NumericLiteral y => this.ScalarExpression(y),
            RealLiteral y => this.ScalarExpression(y),
            MoneyLiteral y => this.ScalarExpression(y),
            BinaryLiteral y => this.ScalarExpression(y),
            StringLiteral y => this.ScalarExpression(y),
            NullLiteral y => this.ScalarExpression(y),
            DefaultLiteral y => this.ScalarExpression(y),
            MaxLiteral y => this.ScalarExpression(y),
            OdbcLiteral y => this.ScalarExpression(y),
            _ => Throw(x)
        };
        /// <summary>
        /// Literal:ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual IdentifierLiteral ScalarExpression(IdentifierLiteral x)=>x;
        /// <summary>
        /// Literal:ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual IntegerLiteral ScalarExpression(IntegerLiteral x)=>x;
        /// <summary>
        /// Literal:ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual NumericLiteral ScalarExpression(NumericLiteral x)=>x;
        /// <summary>
        /// Literal:ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RealLiteral ScalarExpression(RealLiteral x)=>x;
        /// <summary>
        /// Literal:ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MoneyLiteral ScalarExpression(MoneyLiteral x)=>x;
        /// <summary>
        /// Literal:ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BinaryLiteral ScalarExpression(BinaryLiteral x)=>x;
        /// <summary>
        /// Literal:ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual StringLiteral ScalarExpression(StringLiteral x)=>x;
        /// <summary>
        /// Literal:ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual NullLiteral ScalarExpression(NullLiteral x)=>x;
        /// <summary>
        /// Literal:ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DefaultLiteral ScalarExpression(DefaultLiteral x)=>x;
        /// <summary>
        /// Literal:ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MaxLiteral ScalarExpression(MaxLiteral x)=>x;
        /// <summary>
        /// Literal:ValueExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OdbcLiteral ScalarExpression(OdbcLiteral x)=>x;
        /// <summary>
        /// VariableReference:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual VariableReference ScalarExpression(VariableReference x)=>x;
        /// <summary>
        /// VariableReference:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual GlobalVariableExpression ScalarExpression(GlobalVariableExpression x)=>x;
        /// <summary>
        /// PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual UserDefinedTypePropertyAccess ScalarExpression(UserDefinedTypePropertyAccess x)=>x;
        /// <summary>
        /// PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CaseExpression ScalarExpression(CaseExpression x) => x switch{
            SimpleCaseExpression y => this.ScalarExpression(y),
            SearchedCaseExpression y => this.ScalarExpression(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///CaseExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SimpleCaseExpression ScalarExpression(SimpleCaseExpression x)=>x;
        /// <summary>
        ///CaseExpression:PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SearchedCaseExpression ScalarExpression(SearchedCaseExpression x)=>x;
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual NullIfExpression ScalarExpression(NullIfExpression x)=>x;
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CoalesceExpression ScalarExpression(CoalesceExpression x)=>x;
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual IIfCall ScalarExpression(IIfCall x)=>x;
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ConvertCall ScalarExpression(ConvertCall x)=>x;
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TryConvertCall ScalarExpression(TryConvertCall x)=>x;
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ParseCall ScalarExpression(ParseCall x)=>x;
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TryParseCall ScalarExpression(TryParseCall x)=>x;
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CastCall ScalarExpression(CastCall x)=>x;
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TryCastCall ScalarExpression(TryCastCall x)=>x;
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AtTimeZoneCall ScalarExpression(AtTimeZoneCall x)=>x;
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FunctionCall ScalarExpression(FunctionCall x)=>x;
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LeftFunctionCall ScalarExpression(LeftFunctionCall x)=>x;
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RightFunctionCall ScalarExpression(RightFunctionCall x)=>x;
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual PartitionFunctionCall ScalarExpression(PartitionFunctionCall x)=>x;
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ParameterlessCall ScalarExpression(ParameterlessCall x)=>x;
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ScalarSubquery ScalarExpression(ScalarSubquery x)=>x;
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OdbcFunctionCall ScalarExpression(OdbcFunctionCall x)=>x;
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ParenthesisExpression ScalarExpression(ParenthesisExpression x) => x;
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ColumnReferenceExpression ScalarExpression(ColumnReferenceExpression x)=>x;
        /// <summary>
        ///PrimaryExpression:ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual NextValueForExpression ScalarExpression(NextValueForExpression x)=>x;
        /// <summary>
        ///ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ScalarExpression ScalarExpression(ExtractFromExpression x)=>x;
        /// <summary>
        ///ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ScalarExpression ScalarExpression(OdbcConvertSpecification x)=>x;
        /// <summary>
        ///ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ScalarExpression ScalarExpression(BinaryExpression x)=>x;
        /// <summary>
        ///ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ScalarExpression ScalarExpression(IdentityFunctionCall x)=>x;
        /// <summary>
        ///ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ScalarExpression ScalarExpression(UnaryExpression x)=>x;
        /// <summary>
        ///ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ScalarExpression ScalarExpression(ScalarExpressionSnippet x)=>x;
        /// <summary>
        ///ScalarExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ScalarExpression ScalarExpression(SourceDeclaration x)=>x;
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual IdentifierOrValueExpression IdentifierOrValueExpression(IdentifierOrValueExpression x)=>x;
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual StatementList StatementList(StatementList x) => x switch{
            StatementListSnippet y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        /// StatementListSnippet:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual StatementListSnippet Traverse(StatementListSnippet x)=>x;
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TSqlStatement TSqlStatement(TSqlStatement x) => x switch{
            ExecuteStatement                                            y => this.Statement(y),
            ViewStatementBody                                           y => this.Statement(y),
            TriggerStatementBody                                        y => this.Statement(y),
            ProcedureStatementBodyBase                                  y => this.Statement(y),
            DeclareTableVariableStatement                               y => this.Statement(y),
            StatementWithCtesAndXmlNamespaces                           y => this.Statement(y),
            BeginEndBlockStatement                                      y => this.Statement(y),
            TransactionStatement                                        y => this.Statement(y),
            BreakStatement                                              y => this.Statement(y),
            ContinueStatement                                           y => this.Statement(y),
            CreateDefaultStatement                                      y => this.Statement(y),
            CreateRuleStatement                                         y => this.Statement(y),
            DeclareVariableStatement                                    y => this.Statement(y),
            GoToStatement                                               y => this.Statement(y),
            IfStatement                                                 y => this.Statement(y),
            LabelStatement                                              y => this.Statement(y),
            WhileStatement                                              y => this.Statement(y),
            CreateSchemaStatement                                       y => this.Statement(y),
            WaitForStatement                                            y => this.Statement(y),
            ReadTextStatement                                           y => this.Statement(y),
            TextModificationStatement                                   y => this.Statement(y),
            LineNoStatement                                             y => this.Statement(y),
            SecurityStatement                                           y => this.Statement(y),
            AlterAuthorizationStatement                                 y => this.Statement(y),
            SecurityStatementBody80                                     y => this.Statement(y),
            PrintStatement                                              y => this.Statement(y),
            SequenceStatement                                           y => this.Statement(y),
            DropObjectsStatement                                        y => this.Statement(y),
            SecurityPolicyStatement                                     y => this.Statement(y),
            CreateColumnMasterKeyStatement                              y => this.Statement(y),
            DropUnownedObjectStatement                                  y => this.Statement(y),
            ColumnEncryptionKeyStatement                                y => this.Statement(y),
            ExternalTableStatement                                      y => this.Statement(y),
            ExternalDataSourceStatement                                 y => this.Statement(y),
            ExternalFileFormatStatement                                 y => this.Statement(y),
            AssemblyStatement                                           y => this.Statement(y),
            CreateXmlSchemaCollectionStatement                          y => this.Statement(y),
            AlterXmlSchemaCollectionStatement                           y => this.Statement(y),
            DropXmlSchemaCollectionStatement                            y => this.Statement(y),
            AlterTableStatement                                         y => this.Statement(y),
            EnableDisableTriggerStatement                               y => this.Statement(y),
            TryCatchStatement                                           y => this.Statement(y),
            CreateTypeStatement                                         y => this.Statement(y),
            CreateSynonymStatement                                      y => this.Statement(y),
            RouteStatement                                              y => this.Statement(y),
            QueueStatement                                              y => this.Statement(y),
            IndexDefinition                                             y => this.Statement(y),
            IndexStatement                                              y => this.Statement(y),
            CreateFullTextIndexStatement                                y => this.Statement(y),
            CreateEventNotificationStatement                            y => this.Statement(y),
            MasterKeyStatement                                          y => this.Statement(y),
            ApplicationRoleStatement                                    y => this.Statement(y),
            RoleStatement                                               y => this.Statement(y),
            UserStatement                                               y => this.Statement(y),
            CreateStatisticsStatement                                   y => this.Statement(y),
            UpdateStatisticsStatement                                   y => this.Statement(y),
            ReturnStatement                                             y => this.Statement(y),
            DeclareCursorStatement                                      y => this.Statement(y),
            SetVariableStatement                                        y => this.Statement(y),
            CursorStatement                                             y => this.Statement(y),
            OpenSymmetricKeyStatement                                   y => this.Statement(y),
            CloseSymmetricKeyStatement                                  y => this.Statement(y),
            OpenMasterKeyStatement                                      y => this.Statement(y),
            CloseMasterKeyStatement                                     y => this.Statement(y),
            DropDatabaseStatement                                       y => this.Statement(y),
            DropChildObjectsStatement                                   y => this.Statement(y),
            DropIndexStatement                                          y => this.Statement(y),
            DropSchemaStatement                                         y => this.Statement(y),
            RaiseErrorLegacyStatement                                   y => this.Statement(y),
            RaiseErrorStatement                                         y => this.Statement(y),
            ThrowStatement                                              y => this.Statement(y),
            UseStatement                                                y => this.Statement(y),
            KillStatement                                               y => this.Statement(y),
            KillQueryNotificationSubscriptionStatement                  y => this.Statement(y),
            KillStatsJobStatement                                       y => this.Statement(y),
            CheckpointStatement                                         y => this.Statement(y),
            ReconfigureStatement                                        y => this.Statement(y),
            ShutdownStatement                                           y => this.Statement(y),
            SetUserStatement                                            y => this.Statement(y),
            TruncateTableStatement                                      y => this.Statement(y),
            SetOnOffStatement                                           y => this.Statement(y),
            SetRowCountStatement                                        y => this.Statement(y),
            SetCommandStatement                                         y => this.Statement(y),
            SetTransactionIsolationLevelStatement                       y => this.Statement(y),
            SetTextSizeStatement                                        y => this.Statement(y),
            SetErrorLevelStatement                                      y => this.Statement(y),
            CreateDatabaseStatement                                     y => this.Statement(y),
            AlterDatabaseStatement                                      y => this.Statement(y),
            AlterDatabaseScopedConfigurationStatement                   y => this.Statement(y),
            CreateTableStatement                                        y => this.Statement(y),
            BackupStatement                                             y => this.Statement(y),
            RestoreStatement                                            y => this.Statement(y),
            BulkInsertBase                                              y => this.Statement(y),
            DbccStatement                                               y => this.Statement(y),
            CreateAsymmetricKeyStatement                                y => this.Statement(y),
            CreatePartitionFunctionStatement                            y => this.Statement(y),
            CreatePartitionSchemeStatement                              y => this.Statement(y),
            RemoteServiceBindingStatementBase                           y => this.Statement(y),
            CertificateStatementBase                                    y => this.Statement(y),
            CreateContractStatement                                     y => this.Statement(y),
            CredentialStatement                                         y => this.Statement(y),
            MessageTypeStatementBase                                    y => this.Statement(y),
            CreateAggregateStatement                                    y => this.Statement(y),
            AlterCreateEndpointStatementBase                            y => this.Statement(y),
            SymmetricKeyStatement                                       y => this.Statement(y),
            FullTextCatalogStatement                                    y => this.Statement(y),
            AlterCreateServiceStatementBase                             y => this.Statement(y),
            DropFullTextIndexStatement                                  y => this.Statement(y),
            DropTypeStatement                                           y => this.DropTypeStatement(y),
            DropMasterKeyStatement                                      y => this.DropMasterKeyStatement(y),
            AlterPartitionFunctionStatement                             y => this.AlterPartitionFunctionStatement(y),
            AlterPartitionSchemeStatement                               y => this.AlterPartitionSchemeStatement(y),
            AlterFullTextIndexStatement                                 y => this.AlterFullTextIndexStatement(y),
            CreateSearchPropertyListStatement                           y => this.CreateSearchPropertyListStatement(y),
            AlterSearchPropertyListStatement                            y => this.AlterSearchPropertyListStatement(y),
            CreateLoginStatement                                        y => this.CreateLoginStatement(y),
            AlterLoginStatement                                         y => this.Statement(y),
            RevertStatement                                             y => this.RevertStatement(y),
            DropQueueStatement                                          y => this.DropQueueStatement(y),
            SignatureStatementBase                                      y => this.SignatureStatementBase(y),
            DropEventNotificationStatement                              y => this.Statement(y),
            ExecuteAsStatement                                          y => this.Statement(y),
            EndConversationStatement                                    y => this.Statement(y),
            MoveConversationStatement                                   y => this.Statement(y),
            WaitForSupportedStatement                                   y => this.Statement(y),
            SendStatement                                               y => this.Statement(y),
            AlterSchemaStatement                                        y => this.Statement(y),
            AlterAsymmetricKeyStatement                                 y => this.Statement(y),
            AlterServiceMasterKeyStatement                              y => this.Statement(y),
            BeginConversationTimerStatement                             y => this.Statement(y),
            BeginDialogStatement                                        y => this.Statement(y),
            BackupRestoreMasterKeyStatementBase                         y => this.Statement(y),
            TSqlStatementSnippet                                        y => this.Statement(y),
            AuditSpecificationStatement                                 y => this.Statement(y),
            ServerAuditStatement                                        y => this.Statement(y),
            DatabaseEncryptionKeyStatement                              y => this.Statement(y),
            DropDatabaseEncryptionKeyStatement                          y => this.Statement(y),
            ResourcePoolStatement                                       y => this.Statement(y),
            ExternalResourcePoolStatement                               y => this.Statement(y),
            WorkloadGroupStatement                                      y => this.Statement(y),
            BrokerPriorityStatement                                     y => this.Statement(y),
            CreateFullTextStopListStatement                             y => this.Statement(y),
            AlterFullTextStopListStatement                              y => this.Statement(y),
            CreateCryptographicProviderStatement                        y => this.Statement(y),
            AlterCryptographicProviderStatement                         y => this.Statement(y),
            EventSessionStatement                                       y => this.Statement(y),
            AlterResourceGovernorStatement                              y => this.Statement(y),
            CreateSpatialIndexStatement                                 y => this.Statement(y),
            AlterServerConfigurationStatement                           y => this.Statement(y),
            AlterServerConfigurationSetBufferPoolExtensionStatement     y => this.Statement(y),
            AlterServerConfigurationSetDiagnosticsLogStatement          y => this.Statement(y),
            AlterServerConfigurationSetFailoverClusterPropertyStatement y => this.Statement(y),
            AlterServerConfigurationSetHadrClusterStatement             y => this.Statement(y),
            AlterServerConfigurationSetSoftNumaStatement                y => this.Statement(y),
            AvailabilityGroupStatement                                  y => this.Statement(y),
            CreateFederationStatement                                   y => this.Statement(y),
            AlterFederationStatement                                    y => this.Statement(y),
            UseFederationStatement                                      y => this.Statement(y),
            DiskStatement                                               y => this.Statement(y),
            CreateColumnStoreIndexStatement                             y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExecuteStatement Statement(ExecuteStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ViewStatementBody Statement(ViewStatementBody x) => x switch{
            AlterViewStatement         y => this.Statement(y),
            CreateViewStatement        y => this.Statement(y),
            CreateOrAlterViewStatement y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ViewStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterViewStatement Statement(AlterViewStatement x) => x;
        /// <summary>
        ///ViewStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateViewStatement Statement(CreateViewStatement x) => x;
        /// <summary>
        ///ViewStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateOrAlterViewStatement Statement(CreateOrAlterViewStatement x) => x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TriggerStatementBody Statement(TriggerStatementBody x) => x switch{
            AlterTriggerStatement         y => this.Statement(y),
            CreateTriggerStatement        y => this.Statement(y),
            CreateOrAlterTriggerStatement y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///TriggerStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterTriggerStatement Statement(AlterTriggerStatement x) => x;
        /// <summary>
        ///TriggerStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateTriggerStatement Statement(CreateTriggerStatement x) => x;
        /// <summary>
        ///TriggerStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateOrAlterTriggerStatement Statement(CreateOrAlterTriggerStatement x) => x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ProcedureStatementBodyBase Statement(ProcedureStatementBodyBase x) => x switch{
            ProcedureStatementBody y => this.Statement(y),
            FunctionStatementBody  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ProcedureStatementBodyBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ProcedureStatementBody Statement(ProcedureStatementBody x) => x switch{
            AlterProcedureStatement         y => this.Statement(y),
            CreateProcedureStatement        y => this.Statement(y),
            CreateOrAlterProcedureStatement y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ProcedureStatementBody:TriggerStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterProcedureStatement Statement(AlterProcedureStatement x) => x;
        /// <summary>
        ///ProcedureStatementBody:TriggerStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateProcedureStatement Statement(CreateProcedureStatement x) => x;
        /// <summary>
        ///ProcedureStatementBody:TriggerStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateOrAlterProcedureStatement Statement(CreateOrAlterProcedureStatement x) => x;
        ///ProcedureStatementBodyBase:TriggerStatementBody:TSqlStatement:TSqlFragment
        protected virtual FunctionStatementBody Statement(FunctionStatementBody x) => x switch{
            AlterFunctionStatement         y => this.Statement(y),
            CreateFunctionStatement        y => this.Statement(y),
            CreateOrAlterFunctionStatement y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ProcedureStatementBody:ProcedureStatementBodyBase:TriggerStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterFunctionStatement Statement(AlterFunctionStatement x) => x;
        /// <summary>
        ///ProcedureStatementBody:ProcedureStatementBodyBase:TriggerStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateFunctionStatement Statement(CreateFunctionStatement x) => x;
        /// <summary>
        ///ProcedureStatementBody:ProcedureStatementBodyBase:TriggerStatementBody:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateOrAlterFunctionStatement Statement(CreateOrAlterFunctionStatement x) => x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DeclareTableVariableStatement Statement(DeclareTableVariableStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual StatementWithCtesAndXmlNamespaces Statement(StatementWithCtesAndXmlNamespaces x) => x switch{
            SelectStatement           y => this.Statement(y),
            DataModificationStatement y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///StatementWithCtesAndXmlNamespaces:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SelectStatement Statement(SelectStatement x) => x switch{
            SelectStatementSnippet y=>this.Statement(y),
            _ => Throw(x)
        };
        /// <summary>
        ///SelectStatement:StatementWithCtesAndXmlNamespaces:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SelectStatementSnippet Statement(SelectStatementSnippet x)=>x;
        /// <summary>
        ///StatementWithCtesAndXmlNamespaces:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DataModificationStatement Statement(DataModificationStatement x) => x switch{
            DeleteStatement y => this.Statement(y),
            InsertStatement y => this.Statement(y),
            UpdateStatement y => this.Statement(y),
            MergeStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///DeleteStatement:StatementWithCtesAndXmlNamespaces:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DeleteStatement Statement(DeleteStatement x)=>x;
        /// <summary>
        ///DeleteStatement:StatementWithCtesAndXmlNamespaces:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual InsertStatement Statement(InsertStatement x)=>x;
        /// <summary>
        ///DeleteStatement:StatementWithCtesAndXmlNamespaces:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual UpdateStatement Statement(UpdateStatement x)=>x;
        /// <summary>
        ///DeleteStatement:StatementWithCtesAndXmlNamespaces:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MergeStatement Statement(MergeStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BeginEndBlockStatement Statement(BeginEndBlockStatement x) => x switch{
            BeginEndAtomicBlockStatement y=>this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///BeginEndBlockStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BeginEndAtomicBlockStatement Statement(BeginEndAtomicBlockStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TransactionStatement Statement(TransactionStatement x) => x switch{
            BeginTransactionStatement    y => this.Statement(y),
            CommitTransactionStatement   y => this.Statement(y),
            RollbackTransactionStatement y => this.Statement(y),
            SaveTransactionStatement     y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///TransactionStatement:BeginEndBlockStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BeginTransactionStatement Statement(BeginTransactionStatement x)=>x;
        /// <summary>
        ///TransactionStatement:BeginEndBlockStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CommitTransactionStatement Statement(CommitTransactionStatement x)=>x;
        /// <summary>
        ///TransactionStatement:BeginEndBlockStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RollbackTransactionStatement Statement(RollbackTransactionStatement x)=>x;
        /// <summary>
        ///TransactionStatement:BeginEndBlockStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SaveTransactionStatement Statement(SaveTransactionStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BreakStatement Statement(BreakStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ContinueStatement Statement(ContinueStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateDefaultStatement Statement(CreateDefaultStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateRuleStatement Statement(CreateRuleStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DeclareVariableStatement Statement(DeclareVariableStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual GoToStatement Statement(GoToStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual IfStatement Statement(IfStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LabelStatement Statement(LabelStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual WhileStatement Statement(WhileStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateSchemaStatement Statement(CreateSchemaStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual WaitForStatement Statement(WaitForStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ReadTextStatement Statement(ReadTextStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TextModificationStatement Statement(TextModificationStatement x) => x switch{
            UpdateTextStatement y => this.Statement(y),
            WriteTextStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///TextModificationStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual UpdateTextStatement Statement(UpdateTextStatement x)=>x;
        /// <summary>
        ///TextModificationStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual WriteTextStatement Statement(WriteTextStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LineNoStatement Statement(LineNoStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SecurityStatement Statement(SecurityStatement x) => x switch{
            GrantStatement  y => this.Statement(y),
            DenyStatement   y => this.Statement(y),
            RevokeStatement y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///SecurityStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual GrantStatement Statement(GrantStatement x)=>x;
        /// <summary>
        ///SecurityStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DenyStatement Statement(DenyStatement x) => x;
        /// <summary>
        ///SecurityStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RevokeStatement Statement(RevokeStatement x) => x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterAuthorizationStatement Statement(AlterAuthorizationStatement x) => x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SecurityStatementBody80 Statement(SecurityStatementBody80 x) => x switch{
            GrantStatement80  y => this.Statement(y),
            DenyStatement80   y => this.Statement(y),
            RevokeStatement80 y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///SecurityStatementBody80:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual GrantStatement80 Statement(GrantStatement80 x)=>x;
        /// <summary>
        ///SecurityStatementBody80:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DenyStatement80 Statement(DenyStatement80 x)=>x;
        /// <summary>
        ///SecurityStatementBody80:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RevokeStatement80 Statement(RevokeStatement80 x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual PrintStatement Statement(PrintStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SequenceStatement Statement(SequenceStatement x) => x switch{
            CreateSequenceStatement y => this.Statement(y),
            AlterSequenceStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///SequenceStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateSequenceStatement Statement(CreateSequenceStatement x)=>x;
        /// <summary>
        ///SequenceStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterSequenceStatement Statement(AlterSequenceStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropObjectsStatement Statement(DropObjectsStatement x) => x switch{
            DropSequenceStatement       y => this.Statement(y),
            DropSecurityPolicyStatement y => this.Statement(y),
            DropExternalTableStatement  y => this.Statement(y),
            DropTableStatement          y => this.Statement(y),
            DropProcedureStatement      y => this.Statement(y),
            DropFunctionStatement       y => this.Statement(y),
            DropViewStatement           y => this.Statement(y),
            DropDefaultStatement        y => this.Statement(y),
            DropRuleStatement           y => this.Statement(y),
            DropTriggerStatement        y => this.Statement(y),
            DropSynonymStatement        y => this.Statement(y),
            DropAggregateStatement      y => this.Statement(y),
            DropAssemblyStatement       y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropSequenceStatement Statement(DropSequenceStatement x)=>x;
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropSecurityPolicyStatement Statement(DropSecurityPolicyStatement x)=>x;
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropExternalTableStatement Statement(DropExternalTableStatement x)=>x;
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropTableStatement Statement(DropTableStatement x)=>x;
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropProcedureStatement Statement(DropProcedureStatement x)=>x;
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropFunctionStatement Statement(DropFunctionStatement x)=>x;
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropViewStatement Statement(DropViewStatement x)=>x;
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropDefaultStatement Statement(DropDefaultStatement x)=>x;
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropRuleStatement Statement(DropRuleStatement x)=>x;
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropTriggerStatement Statement(DropTriggerStatement x)=>x;
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropSynonymStatement Statement(DropSynonymStatement x)=>x;
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropAggregateStatement Statement(DropAggregateStatement x)=>x;
        /// <summary>
        ///DropObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropAssemblyStatement Statement(DropAssemblyStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SecurityPolicyStatement Statement(SecurityPolicyStatement x) => x switch{
            CreateSecurityPolicyStatement y => this.Statement(y),
            AlterSecurityPolicyStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///SecurityPolicyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateSecurityPolicyStatement Statement(CreateSecurityPolicyStatement x)=>x;
        /// <summary>
        ///SecurityPolicyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterSecurityPolicyStatement Statement(AlterSecurityPolicyStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateColumnMasterKeyStatement Statement(CreateColumnMasterKeyStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropUnownedObjectStatement Statement(DropUnownedObjectStatement x) => x switch{
            DropColumnMasterKeyStatement            y => this.Statement(y),
            DropColumnEncryptionKeyStatement        y => this.Statement(y),
            DropExternalDataSourceStatement         y => this.Statement(y),
            DropExternalFileFormatStatement         y => this.Statement(y),
            DropServerRoleStatement                 y => this.Statement(y),
            DropPartitionFunctionStatement          y => this.Statement(y),
            DropPartitionSchemeStatement            y => this.Statement(y),
            DropApplicationRoleStatement            y => this.Statement(y),
            DropFullTextCatalogStatement            y => this.Statement(y),
            DropLoginStatement                      y => this.Statement(y),
            DropRoleStatement                       y => this.Statement(y),
            DropUserStatement                       y => this.Statement(y),
            DropSymmetricKeyStatement               y => this.Statement(y),
            DropAsymmetricKeyStatement              y => this.Statement(y),
            DropCertificateStatement                y => this.Statement(y),
            DropCredentialStatement                 y => this.Statement(y),
            DropSearchPropertyListStatement         y => this.Statement(y),
            DropContractStatement                   y => this.Statement(y),
            DropEndpointStatement                   y => this.Statement(y),
            DropMessageTypeStatement                y => this.Statement(y),
            DropRemoteServiceBindingStatement       y => this.Statement(y),
            DropRouteStatement                      y => this.Statement(y),
            DropServiceStatement                    y => this.Statement(y),
            DropDatabaseAuditSpecificationStatement y => this.Statement(y),
            DropServerAuditSpecificationStatement   y => this.Statement(y),
            DropServerAuditStatement                y => this.Statement(y),
            DropResourcePoolStatement               y => this.Statement(y),
            DropExternalResourcePoolStatement       y => this.Statement(y),
            DropWorkloadGroupStatement              y => this.Statement(y),
            DropBrokerPriorityStatement             y => this.Statement(y),
            DropFullTextStopListStatement           y => this.Statement(y),
            DropCryptographicProviderStatement      y => this.Statement(y),
            DropEventSessionStatement               y => this.Statement(y),
            DropAvailabilityGroupStatement          y => this.Statement(y),
            DropFederationStatement                 y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropColumnMasterKeyStatement Statement(DropColumnMasterKeyStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropColumnEncryptionKeyStatement Statement(DropColumnEncryptionKeyStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropExternalDataSourceStatement Statement(DropExternalDataSourceStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropExternalFileFormatStatement Statement(DropExternalFileFormatStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropServerRoleStatement Statement(DropServerRoleStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropPartitionFunctionStatement Statement(DropPartitionFunctionStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropPartitionSchemeStatement Statement(DropPartitionSchemeStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropApplicationRoleStatement Statement(DropApplicationRoleStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropFullTextCatalogStatement Statement(DropFullTextCatalogStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropLoginStatement Statement(DropLoginStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropRoleStatement Statement(DropRoleStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropUserStatement Statement(DropUserStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropSymmetricKeyStatement Statement(DropSymmetricKeyStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropAsymmetricKeyStatement Statement(DropAsymmetricKeyStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropCertificateStatement Statement(DropCertificateStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropCredentialStatement Statement(DropCredentialStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropSearchPropertyListStatement Statement(DropSearchPropertyListStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropContractStatement Statement(DropContractStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropEndpointStatement Statement(DropEndpointStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropMessageTypeStatement Statement(DropMessageTypeStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropRemoteServiceBindingStatement Statement(DropRemoteServiceBindingStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropRouteStatement Statement(DropRouteStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropServiceStatement Statement(DropServiceStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropDatabaseAuditSpecificationStatement Statement(DropDatabaseAuditSpecificationStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropServerAuditSpecificationStatement Statement(DropServerAuditSpecificationStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropServerAuditStatement Statement(DropServerAuditStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropResourcePoolStatement Statement(DropResourcePoolStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropExternalResourcePoolStatement Statement(DropExternalResourcePoolStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropWorkloadGroupStatement Statement(DropWorkloadGroupStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropBrokerPriorityStatement Statement(DropBrokerPriorityStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropFullTextStopListStatement Statement(DropFullTextStopListStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropCryptographicProviderStatement Statement(DropCryptographicProviderStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropEventSessionStatement Statement(DropEventSessionStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropAvailabilityGroupStatement Statement(DropAvailabilityGroupStatement x)=>x;
        /// <summary>
        ///DropUnownedObjectStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropFederationStatement Statement(DropFederationStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ColumnEncryptionKeyStatement Statement(ColumnEncryptionKeyStatement x) => x switch{
            CreateColumnEncryptionKeyStatement y => this.Statement(y),
            AlterColumnEncryptionKeyStatement y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ColumnEncryptionKeyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateColumnEncryptionKeyStatement Statement(CreateColumnEncryptionKeyStatement x)=>x;
        /// <summary>
        ///ColumnEncryptionKeyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterColumnEncryptionKeyStatement Statement(AlterColumnEncryptionKeyStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExternalTableStatement Statement(ExternalTableStatement x) => x switch{
            CreateExternalTableStatement y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ExternalTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateExternalTableStatement Statement(CreateExternalTableStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExternalDataSourceStatement Statement(ExternalDataSourceStatement x) => x switch{
            CreateExternalDataSourceStatement y => this.Statement(y),
            AlterExternalDataSourceStatement y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ExternalDataSourceStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateExternalDataSourceStatement Statement(CreateExternalDataSourceStatement x)=>x;
        /// <summary>
        ///ExternalDataSourceStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterExternalDataSourceStatement Statement(AlterExternalDataSourceStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExternalFileFormatStatement Statement(ExternalFileFormatStatement x) => x switch{
            CreateExternalFileFormatStatement y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ExternalFileFormatStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateExternalFileFormatStatement Statement(CreateExternalFileFormatStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AssemblyStatement Statement(AssemblyStatement x) => x switch{
            CreateAssemblyStatement y => this.Statement(y),
            AlterAssemblyStatement y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AssemblyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateAssemblyStatement Statement(CreateAssemblyStatement x)=>x;
        /// <summary>
        ///AssemblyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterAssemblyStatement Statement(AlterAssemblyStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateXmlSchemaCollectionStatement Statement(CreateXmlSchemaCollectionStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterXmlSchemaCollectionStatement Statement(AlterXmlSchemaCollectionStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropXmlSchemaCollectionStatement Statement(DropXmlSchemaCollectionStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterTableStatement Statement(AlterTableStatement x) => x switch{
            AlterTableRebuildStatement                    y => this.Statement(y),
            AlterTableChangeTrackingModificationStatement y => this.Statement(y),
            AlterTableFileTableNamespaceStatement         y => this.Statement(y),
            AlterTableSetStatement                        y => this.Statement(y),
            AlterTableAddTableElementStatement            y => this.Statement(y),
            AlterTableConstraintModificationStatement     y => this.Statement(y),
            AlterTableSwitchStatement                     y => this.Statement(y),
            AlterTableDropTableElementStatement           y => this.Statement(y),
            AlterTableTriggerModificationStatement        y => this.Statement(y),
            AlterTableAlterIndexStatement                 y => this.Statement(y),
            AlterTableAlterColumnStatement                y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AlterTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterTableRebuildStatement Statement(AlterTableRebuildStatement x)=>x;
        /// <summary>
        ///AlterTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterTableChangeTrackingModificationStatement Statement(AlterTableChangeTrackingModificationStatement x)=>x;
        /// <summary>
        ///AlterTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterTableFileTableNamespaceStatement Statement(AlterTableFileTableNamespaceStatement x)=>x;
        /// <summary>
        ///AlterTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterTableSetStatement Statement(AlterTableSetStatement x)=>x;
        /// <summary>
        ///AlterTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterTableAddTableElementStatement Statement(AlterTableAddTableElementStatement x)=>x;
        /// <summary>
        ///AlterTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterTableConstraintModificationStatement Statement(AlterTableConstraintModificationStatement x)=>x;
        /// <summary>
        ///AlterTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterTableSwitchStatement Statement(AlterTableSwitchStatement x)=>x;
        /// <summary>
        ///AlterTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterTableDropTableElementStatement Statement(AlterTableDropTableElementStatement x)=>x;
        /// <summary>
        ///AlterTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterTableTriggerModificationStatement Statement(AlterTableTriggerModificationStatement x)=>x;
        /// <summary>
        ///AlterTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterTableAlterIndexStatement Statement(AlterTableAlterIndexStatement x)=>x;
        /// <summary>
        ///AlterTableStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterTableAlterColumnStatement Statement(AlterTableAlterColumnStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual EnableDisableTriggerStatement Statement(EnableDisableTriggerStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TryCatchStatement Statement(TryCatchStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateTypeStatement Statement(CreateTypeStatement x) => x switch{
            CreateTypeUdtStatement   y => this.Statement(y),
            CreateTypeUddtStatement  y => this.Statement(y),
            CreateTypeTableStatement y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///CreateTypeStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateTypeUdtStatement Statement(CreateTypeUdtStatement x)=>x;
        /// <summary>
        ///CreateTypeStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateTypeUddtStatement Statement(CreateTypeUddtStatement x)=>x;
        /// <summary>
        ///CreateTypeStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateTypeTableStatement Statement(CreateTypeTableStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateSynonymStatement Statement(CreateSynonymStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RouteStatement Statement(RouteStatement x) => x switch{
            CreateRouteStatement y => this.Statement(y),
            AlterRouteStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///RouteStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateRouteStatement Statement(CreateRouteStatement x)=>x;
        /// <summary>
        ///RouteStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterRouteStatement Statement(AlterRouteStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual QueueStatement Statement(QueueStatement x) => x switch{
            CreateQueueStatement y => this.Statement(y),
            AlterQueueStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///QueueStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateQueueStatement Statement(CreateQueueStatement x)=>x;
        /// <summary>
        ///QueueStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterQueueStatement Statement(AlterQueueStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual IndexDefinition Statement(IndexDefinition x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual IndexStatement Statement(IndexStatement x) => x switch{
            AlterIndexStatement              y => this.Statement(y),
            CreateXmlIndexStatement          y => this.Statement(y),
            CreateSelectiveXmlIndexStatement y => this.Statement(y),
            CreateIndexStatement             y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///IndexStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterIndexStatement Statement(AlterIndexStatement x)=>x;
        /// <summary>
        ///IndexStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateXmlIndexStatement Statement(CreateXmlIndexStatement x)=>x;
        /// <summary>
        ///IndexStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateSelectiveXmlIndexStatement Statement(CreateSelectiveXmlIndexStatement x)=>x;
        /// <summary>
        ///IndexStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateIndexStatement Statement(CreateIndexStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateFullTextIndexStatement Statement(CreateFullTextIndexStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateEventNotificationStatement Statement(CreateEventNotificationStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MasterKeyStatement Statement(MasterKeyStatement x) => x switch{
            CreateMasterKeyStatement y => this.Statement(y),
            AlterMasterKeyStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///MasterKeyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateMasterKeyStatement Statement(CreateMasterKeyStatement x)=>x;
        /// <summary>
        ///MasterKeyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterMasterKeyStatement Statement(AlterMasterKeyStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ApplicationRoleStatement Statement(ApplicationRoleStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RoleStatement Statement(RoleStatement x) => x switch{
            CreateRoleStatement y => this.Statement(y),
            AlterRoleStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///RoleStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateRoleStatement Statement(CreateRoleStatement x) => x switch{
            CreateServerRoleStatement y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///CreateRoleStatement:RoleStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateServerRoleStatement Statement(CreateServerRoleStatement x)=>x;
        /// <summary>
        ///RoleStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterRoleStatement Statement(AlterRoleStatement x) => x switch{
            AlterServerRoleStatement y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AlterRoleStatement:RoleStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterServerRoleStatement Statement(AlterServerRoleStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual UserStatement Statement(UserStatement x) => x switch{
            CreateUserStatement y => this.Statement(y),
            AlterUserStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///UserStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateUserStatement Statement(CreateUserStatement x)=>x;
        /// <summary>
        ///UserStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterUserStatement Statement(AlterUserStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateStatisticsStatement Statement(CreateStatisticsStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual UpdateStatisticsStatement Statement(UpdateStatisticsStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ReturnStatement Statement(ReturnStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DeclareCursorStatement Statement(DeclareCursorStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SetVariableStatement Statement(SetVariableStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CursorStatement Statement(CursorStatement x) => x switch{
            OpenCursorStatement       y => this.Statement(y),
            CloseCursorStatement      y => this.Statement(y),
            DeallocateCursorStatement y => this.Statement(y),
            FetchCursorStatement      y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///CursorStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OpenCursorStatement Statement(OpenCursorStatement x)=>x;
        /// <summary>
        ///CursorStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CloseCursorStatement Statement(CloseCursorStatement x)=>x;
        /// <summary>
        ///CursorStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DeallocateCursorStatement Statement(DeallocateCursorStatement x)=>x;
        /// <summary>
        ///CursorStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FetchCursorStatement Statement(FetchCursorStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OpenSymmetricKeyStatement Statement(OpenSymmetricKeyStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CloseSymmetricKeyStatement Statement(CloseSymmetricKeyStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OpenMasterKeyStatement Statement(OpenMasterKeyStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CloseMasterKeyStatement Statement(CloseMasterKeyStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropDatabaseStatement Statement(DropDatabaseStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropChildObjectsStatement Statement(DropChildObjectsStatement x) => x switch{
            DropStatisticsStatement y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///DropChildObjectsStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropStatisticsStatement Statement(DropStatisticsStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropIndexStatement Statement(DropIndexStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropSchemaStatement Statement(DropSchemaStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RaiseErrorLegacyStatement Statement(RaiseErrorLegacyStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RaiseErrorStatement Statement(RaiseErrorStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ThrowStatement Statement(ThrowStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual UseStatement Statement(UseStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual KillStatement Statement(KillStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual KillQueryNotificationSubscriptionStatement Statement(KillQueryNotificationSubscriptionStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual KillStatsJobStatement Statement(KillStatsJobStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CheckpointStatement Statement(CheckpointStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ReconfigureStatement Statement(ReconfigureStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ShutdownStatement Statement(ShutdownStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SetUserStatement Statement(SetUserStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TruncateTableStatement Statement(TruncateTableStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SetOnOffStatement Statement(SetOnOffStatement x) => x switch{
            PredicateSetStatement      y => this.Statement(y),
            SetStatisticsStatement     y => this.Statement(y),
            SetOffsetsStatement        y => this.Statement(y),
            SetIdentityInsertStatement y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///SetOnOffStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual PredicateSetStatement Statement(PredicateSetStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SetStatisticsStatement Statement(SetStatisticsStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SetOffsetsStatement Statement(SetOffsetsStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SetIdentityInsertStatement Statement(SetIdentityInsertStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SetRowCountStatement Statement(SetRowCountStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SetCommandStatement Statement(SetCommandStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SetTransactionIsolationLevelStatement Statement(SetTransactionIsolationLevelStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SetTextSizeStatement Statement(SetTextSizeStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SetErrorLevelStatement Statement(SetErrorLevelStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateDatabaseStatement Statement(CreateDatabaseStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterDatabaseStatement Statement(AlterDatabaseStatement x) => x switch{
            AlterDatabaseCollateStatement         y => this.Statement(y),
            AlterDatabaseRebuildLogStatement      y => this.Statement(y),
            AlterDatabaseAddFileStatement         y => this.Statement(y),
            AlterDatabaseAddFileGroupStatement    y => this.Statement(y),
            AlterDatabaseRemoveFileGroupStatement y => this.Statement(y),
            AlterDatabaseRemoveFileStatement      y => this.Statement(y),
            AlterDatabaseModifyNameStatement      y => this.Statement(y),
            AlterDatabaseModifyFileStatement      y => this.Statement(y),
            AlterDatabaseModifyFileGroupStatement y => this.Statement(y),
            AlterDatabaseSetStatement             y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AlterDatabaseStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterDatabaseCollateStatement Statement(AlterDatabaseCollateStatement x)=>x;
        /// <summary>
        ///AlterDatabaseStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterDatabaseRebuildLogStatement Statement(AlterDatabaseRebuildLogStatement x)=>x;
        /// <summary>
        ///AlterDatabaseStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterDatabaseAddFileStatement Statement(AlterDatabaseAddFileStatement x)=>x;
        /// <summary>
        ///AlterDatabaseStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterDatabaseAddFileGroupStatement Statement(AlterDatabaseAddFileGroupStatement x)=>x;
        /// <summary>
        ///AlterDatabaseStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterDatabaseRemoveFileGroupStatement Statement(AlterDatabaseRemoveFileGroupStatement x)=>x;
        /// <summary>
        ///AlterDatabaseStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterDatabaseRemoveFileStatement Statement(AlterDatabaseRemoveFileStatement x)=>x;
        /// <summary>
        ///AlterDatabaseStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterDatabaseModifyNameStatement Statement(AlterDatabaseModifyNameStatement x)=>x;
        /// <summary>
        ///AlterDatabaseStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterDatabaseModifyFileStatement Statement(AlterDatabaseModifyFileStatement x)=>x;
        /// <summary>
        ///AlterDatabaseStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterDatabaseModifyFileGroupStatement Statement(AlterDatabaseModifyFileGroupStatement x)=>x;
        /// <summary>
        ///AlterDatabaseStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterDatabaseSetStatement Statement(AlterDatabaseSetStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterDatabaseScopedConfigurationStatement Statement(AlterDatabaseScopedConfigurationStatement x) => x switch{
            AlterDatabaseScopedConfigurationSetStatement   y => this.Statement(y),
            AlterDatabaseScopedConfigurationClearStatement y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AlterDatabaseScopedConfigurationStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterDatabaseScopedConfigurationSetStatement Statement(AlterDatabaseScopedConfigurationSetStatement x)=>x;
        /// <summary>
        ///AlterDatabaseScopedConfigurationStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterDatabaseScopedConfigurationClearStatement Statement(AlterDatabaseScopedConfigurationClearStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateTableStatement Statement(CreateTableStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BackupStatement Statement(BackupStatement x) => x switch{
            BackupDatabaseStatement y => this.Statement(y),
            BackupTransactionLogStatement y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///BackupStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BackupDatabaseStatement Statement(BackupDatabaseStatement x)=>x;
        /// <summary>
        ///BackupStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BackupTransactionLogStatement Statement(BackupTransactionLogStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RestoreStatement Statement(RestoreStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BulkInsertBase Statement(BulkInsertBase x) => x switch{
            BulkInsertStatement y => this.Statement(y),
            InsertBulkStatement y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///BulkInsertBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BulkInsertStatement Statement(BulkInsertStatement x)=>x;
        /// <summary>
        ///BulkInsertBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual InsertBulkStatement Statement(InsertBulkStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DbccStatement Statement(DbccStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateAsymmetricKeyStatement Statement(CreateAsymmetricKeyStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreatePartitionFunctionStatement Statement(CreatePartitionFunctionStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreatePartitionSchemeStatement Statement(CreatePartitionSchemeStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RemoteServiceBindingStatementBase Statement(RemoteServiceBindingStatementBase x) => x switch{
            CreateRemoteServiceBindingStatement y => this.Statement(y),
            AlterRemoteServiceBindingStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///RemoteServiceBindingStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateRemoteServiceBindingStatement Statement(CreateRemoteServiceBindingStatement x)=>x;
        /// <summary>
        ///RemoteServiceBindingStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterRemoteServiceBindingStatement Statement(AlterRemoteServiceBindingStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CertificateStatementBase Statement(CertificateStatementBase x) => x switch{
            CreateCertificateStatement y => this.Statement(y),
            AlterCertificateStatement  y => this.Statement(y),
            BackupCertificateStatement y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///CertificateStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateCertificateStatement Statement(CreateCertificateStatement x)=>x;
        /// <summary>
        ///CertificateStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterCertificateStatement Statement(AlterCertificateStatement x)=>x;
        /// <summary>
        ///CertificateStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BackupCertificateStatement Statement(BackupCertificateStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateContractStatement Statement(CreateContractStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CredentialStatement Statement(CredentialStatement x) => x switch{
            CreateCredentialStatement y => this.Statement(y),
            AlterCredentialStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///CredentialStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateCredentialStatement Statement(CreateCredentialStatement x)=>x;
        /// <summary>
        ///CredentialStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterCredentialStatement Statement(AlterCredentialStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MessageTypeStatementBase Statement(MessageTypeStatementBase x) => x switch{
            CreateMessageTypeStatement y => this.Statement(y),
            AlterMessageTypeStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///MessageTypeStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateMessageTypeStatement Statement(CreateMessageTypeStatement x)=>x;
        /// <summary>
        ///MessageTypeStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterMessageTypeStatement Statement(AlterMessageTypeStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateAggregateStatement Statement(CreateAggregateStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterCreateEndpointStatementBase Statement(AlterCreateEndpointStatementBase x) => x switch{
            CreateEndpointStatement y => this.Statement(y),
            AlterEndpointStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AlterCreateEndpointStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateEndpointStatement Statement(CreateEndpointStatement x)=>x;
        /// <summary>
        ///AlterCreateEndpointStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterEndpointStatement Statement(AlterEndpointStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SymmetricKeyStatement Statement(SymmetricKeyStatement x) => x switch{
            CreateSymmetricKeyStatement y => this.Statement(y),
            AlterSymmetricKeyStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///SymmetricKeyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateSymmetricKeyStatement Statement(CreateSymmetricKeyStatement x)=>x;
        /// <summary>
        ///SymmetricKeyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterSymmetricKeyStatement Statement(AlterSymmetricKeyStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FullTextCatalogStatement Statement(FullTextCatalogStatement x) => x switch{
            CreateFullTextCatalogStatement y => this.Statement(y),
            AlterFullTextCatalogStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///FullTextCatalogStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateFullTextCatalogStatement Statement(CreateFullTextCatalogStatement x)=>x;
        /// <summary>
        ///FullTextCatalogStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterFullTextCatalogStatement Statement(AlterFullTextCatalogStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterCreateServiceStatementBase Statement(AlterCreateServiceStatementBase x) => x switch{
            CreateServiceStatement y => this.Statement(y),
            AlterServiceStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AlterCreateServiceStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateServiceStatement Statement(CreateServiceStatement x)=>x;
        /// <summary>
        ///AlterCreateServiceStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterServiceStatement Statement(AlterServiceStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropFullTextIndexStatement Statement(DropFullTextIndexStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropTypeStatement DropTypeStatement(DropTypeStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropMasterKeyStatement DropMasterKeyStatement(DropMasterKeyStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterPartitionFunctionStatement AlterPartitionFunctionStatement(AlterPartitionFunctionStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterPartitionSchemeStatement AlterPartitionSchemeStatement(AlterPartitionSchemeStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterFullTextIndexStatement AlterFullTextIndexStatement(AlterFullTextIndexStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateSearchPropertyListStatement CreateSearchPropertyListStatement(CreateSearchPropertyListStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterSearchPropertyListStatement AlterSearchPropertyListStatement(AlterSearchPropertyListStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateLoginStatement CreateLoginStatement(CreateLoginStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterLoginStatement Statement(AlterLoginStatement x) => x switch{
            AlterLoginOptionsStatement           y => this.AlterLoginOptionsStatement(y),
            AlterLoginEnableDisableStatement     y => this.AlterLoginEnableDisableStatement(y),
            AlterLoginAddDropCredentialStatement y => this.AlterLoginAddDropCredentialStatement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AlterLoginStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterLoginOptionsStatement AlterLoginOptionsStatement(AlterLoginOptionsStatement x)=>x;
        /// <summary>
        ///AlterLoginStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterLoginEnableDisableStatement AlterLoginEnableDisableStatement(AlterLoginEnableDisableStatement x)=>x;
        /// <summary>
        ///AlterLoginStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterLoginAddDropCredentialStatement AlterLoginAddDropCredentialStatement(AlterLoginAddDropCredentialStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RevertStatement RevertStatement(RevertStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropQueueStatement DropQueueStatement(DropQueueStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SignatureStatementBase SignatureStatementBase(SignatureStatementBase x) => x switch{
            AddSignatureStatement  y => this.AddSignatureStatement(y),
            DropSignatureStatement y => this.DropSignatureStatement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///SignatureStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AddSignatureStatement AddSignatureStatement(AddSignatureStatement x)=>x;
        /// <summary>
        ///SignatureStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropSignatureStatement DropSignatureStatement(DropSignatureStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropEventNotificationStatement Statement(DropEventNotificationStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExecuteAsStatement Statement(ExecuteAsStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual EndConversationStatement Statement(EndConversationStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MoveConversationStatement Statement(MoveConversationStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual WaitForSupportedStatement Statement(WaitForSupportedStatement x) => x switch{
            GetConversationGroupStatement y => this.Statement(y),
            ReceiveStatement              y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///WaitForSupportedStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual GetConversationGroupStatement Statement(GetConversationGroupStatement x)=>x;
        /// <summary>
        ///WaitForSupportedStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ReceiveStatement Statement(ReceiveStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SendStatement Statement(SendStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterSchemaStatement Statement(AlterSchemaStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterAsymmetricKeyStatement Statement(AlterAsymmetricKeyStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterServiceMasterKeyStatement Statement(AlterServiceMasterKeyStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BeginConversationTimerStatement Statement(BeginConversationTimerStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BeginDialogStatement Statement(BeginDialogStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BackupRestoreMasterKeyStatementBase Statement(BackupRestoreMasterKeyStatementBase x) => x switch{
            BackupServiceMasterKeyStatement  y => this.Statement(y),
            RestoreServiceMasterKeyStatement y => this.Statement(y),
            BackupMasterKeyStatement         y => this.Statement(y),
            RestoreMasterKeyStatement        y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///BackupRestoreMasterKeyStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BackupServiceMasterKeyStatement Statement(BackupServiceMasterKeyStatement x)=>x;
        /// <summary>
        ///BackupRestoreMasterKeyStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RestoreServiceMasterKeyStatement Statement(RestoreServiceMasterKeyStatement x)=>x;
        /// <summary>
        ///BackupRestoreMasterKeyStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BackupMasterKeyStatement Statement(BackupMasterKeyStatement x)=>x;
        /// <summary>
        ///BackupRestoreMasterKeyStatementBase:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RestoreMasterKeyStatement Statement(RestoreMasterKeyStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TSqlStatementSnippet Statement(TSqlStatementSnippet x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AuditSpecificationStatement Statement(AuditSpecificationStatement x) => x switch{
            CreateDatabaseAuditSpecificationStatement y => this.Statement(y),
            AlterDatabaseAuditSpecificationStatement  y => this.Statement(y),
            CreateServerAuditSpecificationStatement   y => this.Statement(y),
            AlterServerAuditSpecificationStatement    y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AuditSpecificationStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateDatabaseAuditSpecificationStatement Statement(CreateDatabaseAuditSpecificationStatement x)=>x;
        /// <summary>
        ///AuditSpecificationStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterDatabaseAuditSpecificationStatement Statement(AlterDatabaseAuditSpecificationStatement x)=>x;
        /// <summary>
        ///AuditSpecificationStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateServerAuditSpecificationStatement Statement(CreateServerAuditSpecificationStatement x)=>x;
        /// <summary>
        ///AuditSpecificationStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterServerAuditSpecificationStatement Statement(AlterServerAuditSpecificationStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ServerAuditStatement Statement(ServerAuditStatement x) => x switch{
            CreateServerAuditStatement y => this.Statement(y),
            AlterServerAuditStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ServerAuditStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateServerAuditStatement Statement(CreateServerAuditStatement x)=>x;
        /// <summary>
        ///ServerAuditStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterServerAuditStatement Statement(AlterServerAuditStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DatabaseEncryptionKeyStatement Statement(DatabaseEncryptionKeyStatement x) => x switch{
            CreateDatabaseEncryptionKeyStatement y => this.Statement(y),
            AlterDatabaseEncryptionKeyStatement y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///DatabaseEncryptionKeyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateDatabaseEncryptionKeyStatement Statement(CreateDatabaseEncryptionKeyStatement x)=>x;
        /// <summary>
        ///DatabaseEncryptionKeyStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterDatabaseEncryptionKeyStatement Statement(AlterDatabaseEncryptionKeyStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropDatabaseEncryptionKeyStatement Statement(DropDatabaseEncryptionKeyStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ResourcePoolStatement Statement(ResourcePoolStatement x) => x switch{
            CreateResourcePoolStatement y => this.Statement(y),
            AlterResourcePoolStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ResourcePoolStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateResourcePoolStatement Statement(CreateResourcePoolStatement x)=>x;
        /// <summary>
        ///ResourcePoolStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterResourcePoolStatement Statement(AlterResourcePoolStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExternalResourcePoolStatement Statement(ExternalResourcePoolStatement x) => x switch{
            CreateExternalResourcePoolStatement y => this.Statement(y),
            AlterExternalResourcePoolStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ExternalResourcePoolStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateExternalResourcePoolStatement Statement(CreateExternalResourcePoolStatement x)=>x;
        /// <summary>
        ///ExternalResourcePoolStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterExternalResourcePoolStatement Statement(AlterExternalResourcePoolStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual WorkloadGroupStatement Statement(WorkloadGroupStatement x) => x switch{
            CreateWorkloadGroupStatement y => this.Statement(y),
            AlterWorkloadGroupStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///WorkloadGroupStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateWorkloadGroupStatement Statement(CreateWorkloadGroupStatement x)=>x;
        /// <summary>
        ///WorkloadGroupStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterWorkloadGroupStatement Statement(AlterWorkloadGroupStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BrokerPriorityStatement Statement(BrokerPriorityStatement x) => x switch{
            CreateBrokerPriorityStatement y => this.Statement(y),
            AlterBrokerPriorityStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///BrokerPriorityStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateBrokerPriorityStatement Statement(CreateBrokerPriorityStatement x)=>x;
        /// <summary>
        ///BrokerPriorityStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterBrokerPriorityStatement Statement(AlterBrokerPriorityStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateFullTextStopListStatement Statement(CreateFullTextStopListStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterFullTextStopListStatement Statement(AlterFullTextStopListStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateCryptographicProviderStatement Statement(CreateCryptographicProviderStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterCryptographicProviderStatement Statement(AlterCryptographicProviderStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual EventSessionStatement Statement(EventSessionStatement x) => x switch{
            CreateEventSessionStatement y => this.Statement(y),
            AlterEventSessionStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///EventSessionStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateEventSessionStatement Statement(CreateEventSessionStatement x)=>x;
        /// <summary>
        ///EventSessionStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterEventSessionStatement Statement(AlterEventSessionStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterResourceGovernorStatement Statement(AlterResourceGovernorStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateSpatialIndexStatement Statement(CreateSpatialIndexStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterServerConfigurationStatement Statement(AlterServerConfigurationStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterServerConfigurationSetBufferPoolExtensionStatement Statement(AlterServerConfigurationSetBufferPoolExtensionStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterServerConfigurationSetDiagnosticsLogStatement Statement(AlterServerConfigurationSetDiagnosticsLogStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterServerConfigurationSetFailoverClusterPropertyStatement Statement(AlterServerConfigurationSetFailoverClusterPropertyStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterServerConfigurationSetHadrClusterStatement Statement(AlterServerConfigurationSetHadrClusterStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterServerConfigurationSetSoftNumaStatement Statement(AlterServerConfigurationSetSoftNumaStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AvailabilityGroupStatement Statement(AvailabilityGroupStatement x) => x switch{
            CreateAvailabilityGroupStatement y => this.Statement(y),
            AlterAvailabilityGroupStatement  y => this.Statement(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AvailabilityGroupStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateAvailabilityGroupStatement Statement(CreateAvailabilityGroupStatement x)=>x;
        /// <summary>
        ///AvailabilityGroupStatement:TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterAvailabilityGroupStatement Statement(AlterAvailabilityGroupStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateFederationStatement Statement(CreateFederationStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterFederationStatement Statement(AlterFederationStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual UseFederationStatement Statement(UseFederationStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DiskStatement Statement(DiskStatement x)=>x;
        /// <summary>
        ///TSqlStatement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateColumnStoreIndexStatement Statement(CreateColumnStoreIndexStatement x)=>x;
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExecuteOption ExecuteOption(ExecuteOption x) => x switch{
            ResultSetsExecuteOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        /// ExecuteOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ResultSetsExecuteOption Traverse(ResultSetsExecuteOption x)=>x;
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ResultSetDefinition ResultSetDefinition(ResultSetDefinition x) => x switch{
            InlineResultSetDefinition       y => this.Traverse(y),
            SchemaObjectResultSetDefinition y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        /// ResultSetDefinition:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual InlineResultSetDefinition Traverse(InlineResultSetDefinition x)=>x;
        /// <summary>
        /// ResultSetDefinition:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SchemaObjectResultSetDefinition Traverse(SchemaObjectResultSetDefinition x)=>x;
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ResultColumnDefinition ResultColumnDefinition(ResultColumnDefinition x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExecuteSpecification ExecuteSpecification(ExecuteSpecification x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExecuteContext ExecuteContext(ExecuteContext x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExecuteParameter ExecuteParameter(ExecuteParameter x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExecutableEntity ExecutableEntity(ExecutableEntity x) => x switch{
            ExecutableProcedureReference y => this.Traverse(y),
            ExecutableStringList         y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ExecutableEntity:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExecutableProcedureReference Traverse(ExecutableProcedureReference x)=>x;
        /// <summary>
        ///ExecutableEntity:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExecutableStringList Traverse(ExecutableStringList x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ProcedureReferenceName ProcedureReferenceName(ProcedureReferenceName x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AdHocDataSource AdHocDataSource(AdHocDataSource x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ViewOption ViewOption(ViewOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TriggerObject TriggerObject(TriggerObject x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TriggerOption TriggerOption(TriggerOption x) => x switch{
            ExecuteAsTriggerOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///TriggerOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExecuteAsTriggerOption Traverse(ExecuteAsTriggerOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TriggerAction TriggerAction(TriggerAction x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ProcedureReference ProcedureReference(ProcedureReference x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MethodSpecifier MethodSpecifier(MethodSpecifier x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ProcedureOption ProcedureOption(ProcedureOption x) => x switch{
            ExecuteAsProcedureOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ProcedureOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExecuteAsProcedureOption Traverse(ExecuteAsProcedureOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FunctionOption FunctionOption(FunctionOption x) => x switch{
            InlineFunctionOption    y => this.Traverse(y),
            ExecuteAsFunctionOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///FunctionOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual InlineFunctionOption Traverse(InlineFunctionOption x)=>x;
        /// <summary>
        ///FunctionOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExecuteAsFunctionOption Traverse(ExecuteAsFunctionOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual XmlNamespacesElement XmlNamespacesElement(XmlNamespacesElement x) => x switch{
            XmlNamespacesDefaultElement y => this.Traverse(y),
            XmlNamespacesAliasElement   y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///XmlNamespacesElement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual XmlNamespacesDefaultElement Traverse(XmlNamespacesDefaultElement x)=>x;
        /// <summary>
        ///XmlNamespacesElement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual XmlNamespacesAliasElement Traverse(XmlNamespacesAliasElement x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CommonTableExpression CommonTableExpression(CommonTableExpression x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TSqlFragment WithCtesAndXmlNamespaces(WithCtesAndXmlNamespaces x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FunctionReturnType FunctionReturnType(FunctionReturnType x) => x switch{
            TableValuedFunctionReturnType y => this.Traverse(y),
            ScalarFunctionReturnType      y => this.Traverse(y),
            SelectFunctionReturnType      y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///FunctionReturnType:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TableValuedFunctionReturnType Traverse(TableValuedFunctionReturnType x)=>x;
        /// <summary>
        ///FunctionReturnType:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ScalarFunctionReturnType Traverse(ScalarFunctionReturnType x)=>x;
        /// <summary>
        ///FunctionReturnType:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SelectFunctionReturnType Traverse(SelectFunctionReturnType x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DataTypeReference DataTypeReference(DataTypeReference x) => x switch{
            ParameterizedDataTypeReference y => this.Traverse(y),
            XmlDataTypeReference           y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///DataTypeReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ParameterizedDataTypeReference Traverse(ParameterizedDataTypeReference x) => x switch{
            SqlDataTypeReference  y => this.Traverse(y),
            UserDataTypeReference y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ParameterizedDataTypeReference:DataTypeReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SqlDataTypeReference Traverse(SqlDataTypeReference x)=>x;
        /// <summary>
        ///ParameterizedDataTypeReference:DataTypeReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual UserDataTypeReference Traverse(UserDataTypeReference x)=>x;
        /// <summary>
        ///DataTypeReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual XmlDataTypeReference Traverse(XmlDataTypeReference x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TableDefinition TableDefinition(TableDefinition x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DeclareTableVariableBody DeclareTableVariableBody(DeclareTableVariableBody x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TableReference TableReference(TableReference x) => x switch{
            TableReferenceWithAlias         y => this.TableReference(y),
            JoinTableReference              y => this.TableReference(y),
            JoinParenthesisTableReference   y => this.TableReference(y),
            OdbcQualifiedJoinTableReference y => this.TableReference(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TableReferenceWithAlias TableReference(TableReferenceWithAlias x) => x switch{
            NamedTableReference               y => this.TableReference(y),
            TableReferenceWithAliasAndColumns y => this.TableReference(y),
            FullTextTableReference            y => this.TableReference(y),
            SemanticTableReference            y => this.TableReference(y),
            OpenXmlTableReference             y => this.TableReference(y),
            OpenJsonTableReference            y => this.TableReference(y),
            //OpenRowsetTableReference          y => this.TableReference(y),
            InternalOpenRowset                y => this.TableReference(y),
            OpenQueryTableReference           y => this.TableReference(y),
            AdHocTableReference               y => this.TableReference(y),
            BuiltInFunctionTableReference     y => this.TableReference(y),
            GlobalFunctionTableReference      y => this.TableReference(y),
            PivotedTableReference             y => this.TableReference(y),
            UnpivotedTableReference           y => this.TableReference(y),
            VariableTableReference            y => this.TableReference(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TableReferenceWithAlias TableReference(NamedTableReference x)=>x;
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TableReferenceWithAliasAndColumns TableReference(TableReferenceWithAliasAndColumns x) => x switch{
            OpenRowsetTableReference           y => this.TableReference(y),
            SchemaObjectFunctionTableReference y => this.TableReference(y),
            QueryDerivedTable                  y => this.TableReference(y),
            InlineDerivedTable                 y => this.TableReference(y),
            BulkOpenRowset                     y => this.TableReference(y),
            DataModificationTableReference     y => this.TableReference(y),
            ChangeTableChangesTableReference   y => this.TableReference(y),
            ChangeTableVersionTableReference   y => this.TableReference(y),
            VariableMethodCallTableReference   y => this.TableReference(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///TableReferenceWithAliasAndColumns:TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SchemaObjectFunctionTableReference TableReference(SchemaObjectFunctionTableReference x)=>x;
        /// <summary>
        ///TableReferenceWithAliasAndColumns:TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual QueryDerivedTable TableReference(QueryDerivedTable x)=>x;
        /// <summary>
        ///TableReferenceWithAliasAndColumns:TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual InlineDerivedTable TableReference(InlineDerivedTable x)=>x;
        /// <summary>
        ///TableReferenceWithAliasAndColumns:TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BulkOpenRowset TableReference(BulkOpenRowset x)=>x;
        /// <summary>
        ///TableReferenceWithAliasAndColumns:TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DataModificationTableReference TableReference(DataModificationTableReference x)=>x;
        /// <summary>
        ///TableReferenceWithAliasAndColumns:TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ChangeTableChangesTableReference TableReference(ChangeTableChangesTableReference x)=>x;
        /// <summary>
        ///TableReferenceWithAliasAndColumns:TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ChangeTableVersionTableReference TableReference(ChangeTableVersionTableReference x)=>x;
        /// <summary>
        ///TableReferenceWithAliasAndColumns:TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual VariableMethodCallTableReference TableReference(VariableMethodCallTableReference x)=>x;
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FullTextTableReference TableReference(FullTextTableReference x)=>x;
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SemanticTableReference TableReference(SemanticTableReference x)=>x;
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OpenXmlTableReference TableReference(OpenXmlTableReference x)=>x;
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OpenJsonTableReference TableReference(OpenJsonTableReference x)=>x;
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OpenRowsetTableReference TableReference(OpenRowsetTableReference x)=>x;
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual InternalOpenRowset TableReference(InternalOpenRowset x)=>x;
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OpenQueryTableReference TableReference(OpenQueryTableReference x)=>x;
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AdHocTableReference TableReference(AdHocTableReference x)=>x;
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BuiltInFunctionTableReference TableReference(BuiltInFunctionTableReference x)=>x;
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual GlobalFunctionTableReference TableReference(GlobalFunctionTableReference x)=>x;
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual PivotedTableReference TableReference(PivotedTableReference x)=>x;
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual UnpivotedTableReference TableReference(UnpivotedTableReference x)=>x;
        /// <summary>
        ///TableReferenceWithAlias:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual VariableTableReference TableReference(VariableTableReference x)=>x;
        /// <summary>
        ///TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual JoinTableReference TableReference(JoinTableReference x) => x switch{
            UnqualifiedJoin y => this.TableReference(y),
            QualifiedJoin   y => this.TableReference(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///JoinTableReference:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual UnqualifiedJoin TableReference(UnqualifiedJoin x)=>x;
        /// <summary>
        ///JoinTableReference:TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual QualifiedJoin TableReference(QualifiedJoin x) => x;
        /// <summary>
        ///TableReference:TSqlFragment
        /// SELECT * FROM (CUSTOMER JOIN ORDERS ON CUSTOMER.C_CUSTKEY=ORDERS.O_CUSTKEY)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual JoinParenthesisTableReference TableReference(JoinParenthesisTableReference x) => x;
        /// <summary>
        ///TableReference:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OdbcQualifiedJoinTableReference TableReference(OdbcQualifiedJoinTableReference x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TableHint TableHint(TableHint x) => x switch{
            IndexTableHint     y => this.TableHint(y),
            LiteralTableHint   y => this.TableHint(y),
            ForceSeekTableHint y => this.TableHint(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///TableHint:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual IndexTableHint TableHint(IndexTableHint x)=>x;
        /// <summary>
        ///TableHint:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LiteralTableHint TableHint(LiteralTableHint x)=>x;
        /// <summary>
        ///TableHint:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ForceSeekTableHint TableHint(ForceSeekTableHint x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BooleanExpression BooleanExpression(BooleanExpression x) => x switch{
            SubqueryComparisonPredicate              y => this.Traverse(y),
            ExistsPredicate                          y => this.Traverse(y),
            LikePredicate                            y => this.Traverse(y),
            InPredicate                              y => this.Traverse(y),
            FullTextPredicate                        y => this.Traverse(y),
            UpdateCall                               y => this.Traverse(y),
            TSEqualCall                              y => this.Traverse(y),
            BooleanNotExpression                     y => this.Traverse(y),
            BooleanParenthesisExpression             y => this.Traverse(y),
            BooleanComparisonExpression              y => this.Traverse(y),
            BooleanBinaryExpression                  y => this.Traverse(y),
            BooleanIsNullExpression                  y => this.Traverse(y),
            GraphMatchPredicate                      y => this.Traverse(y),
            GraphMatchExpression                     y => this.Traverse(y),
            BooleanTernaryExpression                 y => this.Traverse(y),
            BooleanExpressionSnippet                 y => this.Traverse(y),
            EventDeclarationCompareFunctionParameter y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SubqueryComparisonPredicate Traverse(SubqueryComparisonPredicate x)=>x;
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExistsPredicate Traverse(ExistsPredicate x)=>x;
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LikePredicate Traverse(LikePredicate x)=>x;
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual InPredicate Traverse(InPredicate x)=>x;
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FullTextPredicate Traverse(FullTextPredicate x)=>x;
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual UpdateCall Traverse(UpdateCall x)=>x;
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TSEqualCall Traverse(TSEqualCall x)=>x;
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BooleanNotExpression Traverse(BooleanNotExpression x)=>x;
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BooleanParenthesisExpression Traverse(BooleanParenthesisExpression x)=>x;
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BooleanComparisonExpression Traverse(BooleanComparisonExpression x)=>x;
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BooleanBinaryExpression Traverse(BooleanBinaryExpression x)=>x;
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BooleanIsNullExpression Traverse(BooleanIsNullExpression x)=>x;
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual GraphMatchPredicate Traverse(GraphMatchPredicate x)=>x;
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual GraphMatchExpression Traverse(GraphMatchExpression x)=>x;
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BooleanTernaryExpression Traverse(BooleanTernaryExpression x)=>x;
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BooleanExpressionSnippet Traverse(BooleanExpressionSnippet x)=>x;
        /// <summary>
        ///BooleanExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual EventDeclarationCompareFunctionParameter Traverse(EventDeclarationCompareFunctionParameter x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ForClause ForClause(ForClause x) => x switch{
            BrowseForClause     y => this.Traverse(y),
            ReadOnlyForClause   y => this.Traverse(y),
            XmlForClause        y => this.Traverse(y),
            XmlForClauseOption  y => this.Traverse(y),
            JsonForClause       y => this.Traverse(y),
            JsonForClauseOption y => this.Traverse(y),
            UpdateForClause     y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ForClause:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BrowseForClause Traverse(BrowseForClause x)=>x;
        /// <summary>
        ///ForClause:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ReadOnlyForClause Traverse(ReadOnlyForClause x)=>x;
        /// <summary>
        ///ForClause:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual XmlForClause Traverse(XmlForClause x)=>x;
        /// <summary>
        ///ForClause:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual XmlForClauseOption Traverse(XmlForClauseOption x)=>x;
        /// <summary>
        ///ForClause:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual JsonForClause Traverse(JsonForClause x)=>x;
        /// <summary>
        ///ForClause:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual JsonForClauseOption Traverse(JsonForClauseOption x)=>x;
        /// <summary>
        ///ForClause:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual UpdateForClause Traverse(UpdateForClause x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OptimizerHint OptimizerHint(OptimizerHint x) => x switch{
            LiteralOptimizerHint     y => this.Traverse(y),
            TableHintsOptimizerHint  y => this.Traverse(y),
            OptimizeForOptimizerHint y => this.Traverse(y),
            UseHintList y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///OptimizerHint:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LiteralOptimizerHint Traverse(LiteralOptimizerHint x)=>x;
        /// <summary>
        ///OptimizerHint:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TableHintsOptimizerHint Traverse(TableHintsOptimizerHint x)=>x;
        /// <summary>
        ///OptimizerHint:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OptimizeForOptimizerHint Traverse(OptimizeForOptimizerHint x)=>x;
        /// <summary>
        ///OptimizerHint:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual UseHintList Traverse(UseHintList x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual VariableValuePair VariableValuePair(VariableValuePair x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual WhenClause WhenClause(WhenClause x) => x switch{
            SimpleWhenClause   y => this.Traverse(y),
            SearchedWhenClause y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///WhenClause:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SimpleWhenClause Traverse(SimpleWhenClause x)=>x;
        /// <summary>
        ///WhenClause:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SearchedWhenClause Traverse(SearchedWhenClause x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SchemaDeclarationItem SchemaDeclarationItem(SchemaDeclarationItem x) => x switch{
            SchemaDeclarationItemOpenjson y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///SchemaDeclarationItem:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SchemaDeclarationItemOpenjson Traverse(SchemaDeclarationItemOpenjson x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CallTarget CallTarget(CallTarget x) => x switch{
            ExpressionCallTarget          y => this.Traverse(y),
            MultiPartIdentifierCallTarget y => this.Traverse(y),
            UserDefinedTypeCallTarget     y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///CallTarget:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExpressionCallTarget Traverse(ExpressionCallTarget x)=>x;
        /// <summary>
        ///CallTarget:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MultiPartIdentifierCallTarget Traverse(MultiPartIdentifierCallTarget x)=>x;
        /// <summary>
        ///CallTarget:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual UserDefinedTypeCallTarget Traverse(UserDefinedTypeCallTarget x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OverClause OverClause(OverClause x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AtomicBlockOption AtomicBlockOption(AtomicBlockOption x) => x switch{
            LiteralAtomicBlockOption    y => this.Traverse(y),
            IdentifierAtomicBlockOption y => this.Traverse(y),
            OnOffAtomicBlockOption      y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AtomicBlockOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LiteralAtomicBlockOption Traverse(LiteralAtomicBlockOption x)=>x;
        /// <summary>
        ///AtomicBlockOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual IdentifierAtomicBlockOption Traverse(IdentifierAtomicBlockOption x)=>x;
        /// <summary>
        ///AtomicBlockOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OnOffAtomicBlockOption Traverse(OnOffAtomicBlockOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ColumnWithSortOrder ColumnWithSortOrder(ColumnWithSortOrder x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DeclareVariableElement DeclareVariableElement(DeclareVariableElement x) => x switch{
            ProcedureParameter y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///DeclareVariableElement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ProcedureParameter Traverse(ProcedureParameter x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DataModificationSpecification DataModificationSpecification(DataModificationSpecification x) => x switch{
            UpdateDeleteSpecificationBase y => this.Traverse(y),
            InsertSpecification y => this.Traverse(y),
            MergeSpecification  y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///DataModificationSpecification:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual UpdateDeleteSpecificationBase Traverse(UpdateDeleteSpecificationBase x) => x switch{
            DeleteSpecification y => this.Traverse(y),
            UpdateSpecification y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///UpdateDeleteSpecificationBase:DataModificationSpecification:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DeleteSpecification Traverse(DeleteSpecification x)=>x;
        /// <summary>
        ///UpdateDeleteSpecificationBase:DataModificationSpecification:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual UpdateSpecification Traverse(UpdateSpecification x)=>x;
        /// <summary>
        ///DataModificationSpecification:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual InsertSpecification Traverse(InsertSpecification x)=>x;
        /// <summary>
        ///DataModificationSpecification:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MergeSpecification Traverse(MergeSpecification x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual Permission Permission(Permission x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SecurityTargetObject SecurityTargetObject(SecurityTargetObject x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SecurityTargetObjectName SecurityTargetObjectName(SecurityTargetObjectName x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SecurityPrincipal SecurityPrincipal(SecurityPrincipal x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SecurityElement80 SecurityElement80(SecurityElement80 x) => x switch{
            CommandSecurityElement80   y => this.Traverse(y),
            PrivilegeSecurityElement80 y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///SecurityElement80:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CommandSecurityElement80 Traverse(CommandSecurityElement80 x)=>x;
        /// <summary>
        ///SecurityElement80:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual PrivilegeSecurityElement80 Traverse(PrivilegeSecurityElement80 x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual Privilege80 Privilege80(Privilege80 x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SecurityUserClause80 SecurityUserClause80(SecurityUserClause80 x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SetClause SetClause(SetClause x) => x switch{
            AssignmentSetClause   y => this.Traverse(y),
            FunctionCallSetClause y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///SetClause:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AssignmentSetClause Traverse(AssignmentSetClause x)=>x;
        /// <summary>
        ///SetClause:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FunctionCallSetClause Traverse(FunctionCallSetClause x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual InsertSource InsertSource(InsertSource x) => x switch{
            ValuesInsertSource  y => this.Traverse(y),
            SelectInsertSource  y => this.Traverse(y),
            ExecuteInsertSource y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///InsertSource:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ValuesInsertSource Traverse(ValuesInsertSource x)=>x;
        /// <summary>
        ///InsertSource:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SelectInsertSource Traverse(SelectInsertSource x)=>x;
        /// <summary>
        ///InsertSource:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExecuteInsertSource Traverse(ExecuteInsertSource x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RowValue RowValue(RowValue x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LiteralRange LiteralRange(LiteralRange x) => x switch{
            ProcessAffinityRange y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///LiteralRange:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ProcessAffinityRange Traverse(ProcessAffinityRange x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OptionValue OptionValue(OptionValue x) => x switch{
            OnOffOptionValue   y => this.Traverse(y),
            LiteralOptionValue y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///OptionValue:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OnOffOptionValue Traverse(OnOffOptionValue x)=>x;
        /// <summary>
        ///OptionValue:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LiteralOptionValue Traverse(LiteralOptionValue x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual IdentifierOrScalarExpression Traverse(IdentifierOrScalarExpression x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SchemaObjectNameOrValueExpression Traverse(SchemaObjectNameOrValueExpression x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SequenceOption Traverse(SequenceOption x) => x switch{
            DataTypeSequenceOption         y => this.Traverse(y),
            ScalarExpressionSequenceOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///SequenceOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DataTypeSequenceOption Traverse(DataTypeSequenceOption x)=>x;
        /// <summary>
        ///SequenceOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ScalarExpressionSequenceOption Traverse(ScalarExpressionSequenceOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SecurityPredicateAction Traverse(SecurityPredicateAction x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SecurityPolicyOption Traverse(SecurityPolicyOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ColumnMasterKeyParameter Traverse(ColumnMasterKeyParameter x) => x switch{
            ColumnMasterKeyStoreProviderNameParameter y => this.Traverse(y),
            ColumnMasterKeyPathParameter              y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ColumnMasterKeyParameter:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ColumnMasterKeyStoreProviderNameParameter Traverse(ColumnMasterKeyStoreProviderNameParameter x)=>x;
        /// <summary>
        ///ColumnMasterKeyParameter:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ColumnMasterKeyPathParameter Traverse(ColumnMasterKeyPathParameter x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ColumnEncryptionKeyValue Traverse(ColumnEncryptionKeyValue x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ColumnEncryptionKeyValueParameter Traverse(ColumnEncryptionKeyValueParameter x) => x switch{
            ColumnMasterKeyNameParameter           y => this.Traverse(y),
            ColumnEncryptionAlgorithmNameParameter y => this.Traverse(y),
            EncryptedValueParameter                y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ColumnEncryptionKeyValueParameter:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ColumnMasterKeyNameParameter Traverse(ColumnMasterKeyNameParameter x)=>x;
        /// <summary>
        ///ColumnEncryptionKeyValueParameter:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ColumnEncryptionAlgorithmNameParameter Traverse(ColumnEncryptionAlgorithmNameParameter x)=>x;
        /// <summary>
        ///ColumnEncryptionKeyValueParameter:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual EncryptedValueParameter Traverse(EncryptedValueParameter x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExternalTableOption Traverse(ExternalTableOption x) => x switch{
            ExternalTableLiteralOrIdentifierOption y => this.Traverse(y),
            ExternalTableDistributionOption        y => this.Traverse(y),
            ExternalTableRejectTypeOption          y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ExternalTableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExternalTableLiteralOrIdentifierOption Traverse(ExternalTableLiteralOrIdentifierOption x)=>x;
        /// <summary>
        ///ExternalTableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExternalTableDistributionOption Traverse(ExternalTableDistributionOption x)=>x;
        /// <summary>
        ///ExternalTableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExternalTableRejectTypeOption Traverse(ExternalTableRejectTypeOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExternalTableDistributionPolicy Traverse(ExternalTableDistributionPolicy x) => x switch{
            ExternalTableReplicatedDistributionPolicy y => this.Traverse(y),
            ExternalTableRoundRobinDistributionPolicy y => this.Traverse(y),
            ExternalTableShardedDistributionPolicy    y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ExternalTableDistributionPolicy:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExternalTableReplicatedDistributionPolicy Traverse(ExternalTableReplicatedDistributionPolicy x)=>x;
        /// <summary>
        ///ExternalTableDistributionPolicy:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExternalTableRoundRobinDistributionPolicy Traverse(ExternalTableRoundRobinDistributionPolicy x)=>x;
        /// <summary>
        ///ExternalTableDistributionPolicy:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExternalTableShardedDistributionPolicy Traverse(ExternalTableShardedDistributionPolicy x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExternalDataSourceOption Traverse(ExternalDataSourceOption x) => x switch{
            ExternalDataSourceLiteralOrIdentifierOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ExternalDataSourceOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExternalDataSourceLiteralOrIdentifierOption Traverse(ExternalDataSourceLiteralOrIdentifierOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExternalFileFormatOption Traverse(ExternalFileFormatOption x) => x switch{
            ExternalFileFormatLiteralOption        y => this.Traverse(y),
            ExternalFileFormatUseDefaultTypeOption y => this.Traverse(y),
            ExternalFileFormatContainerOption      y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ExternalFileFormatOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExternalFileFormatLiteralOption Traverse(ExternalFileFormatLiteralOption x)=>x;
        /// <summary>
        ///ExternalFileFormatOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExternalFileFormatUseDefaultTypeOption Traverse(ExternalFileFormatUseDefaultTypeOption x)=>x;
        /// <summary>
        ///ExternalFileFormatOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExternalFileFormatContainerOption Traverse(ExternalFileFormatContainerOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AssemblyOption Traverse(AssemblyOption x) => x switch{
            OnOffAssemblyOption         y => this.Traverse(y),
            PermissionSetAssemblyOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AssemblyOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OnOffAssemblyOption Traverse(OnOffAssemblyOption x)=>x;
        /// <summary>
        ///AssemblyOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual PermissionSetAssemblyOption Traverse(PermissionSetAssemblyOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AddFileSpec Traverse(AddFileSpec x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AssemblyName Traverse(AssemblyName x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TableOption Traverse(TableOption x) => x switch{
            LockEscalationTableOption           y => this.Traverse(y),
            FileStreamOnTableOption             y => this.Traverse(y),
            FileTableDirectoryTableOption       y => this.Traverse(y),
            FileTableCollateFileNameTableOption y => this.Traverse(y),
            FileTableConstraintNameTableOption  y => this.Traverse(y),
            MemoryOptimizedTableOption          y => this.Traverse(y),
            DurabilityTableOption               y => this.Traverse(y),
            RemoteDataArchiveTableOption        y => this.Traverse(y),
            RemoteDataArchiveAlterTableOption   y => this.Traverse(y),
            SystemVersioningTableOption         y => this.Traverse(y),
            TableDataCompressionOption          y => this.Traverse(y),
            TableDistributionOption             y => this.Traverse(y),
            TableIndexOption                    y => this.Traverse(y),
            TablePartitionOption                y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LockEscalationTableOption Traverse(LockEscalationTableOption x)=>x;
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FileStreamOnTableOption Traverse(FileStreamOnTableOption x)=>x;
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FileTableDirectoryTableOption Traverse(FileTableDirectoryTableOption x)=>x;
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FileTableCollateFileNameTableOption Traverse(FileTableCollateFileNameTableOption x)=>x;
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FileTableConstraintNameTableOption Traverse(FileTableConstraintNameTableOption x)=>x;
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MemoryOptimizedTableOption Traverse(MemoryOptimizedTableOption x)=>x;
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DurabilityTableOption Traverse(DurabilityTableOption x)=>x;
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RemoteDataArchiveTableOption Traverse(RemoteDataArchiveTableOption x)=>x;
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RemoteDataArchiveAlterTableOption Traverse(RemoteDataArchiveAlterTableOption x)=>x;
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SystemVersioningTableOption Traverse(SystemVersioningTableOption x)=>x;
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TableDataCompressionOption Traverse(TableDataCompressionOption x)=>x;
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TableDistributionOption Traverse(TableDistributionOption x)=>x;
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TableIndexOption Traverse(TableIndexOption x)=>x;
        /// <summary>
        ///TableOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TablePartitionOption Traverse(TablePartitionOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DatabaseOption Traverse(DatabaseOption x) => x switch{
            RemoteDataArchiveDatabaseOption  y => this.Traverse(y),
            OnOffDatabaseOption              y => this.Traverse(y),
            ContainmentDatabaseOption        y => this.Traverse(y),
            HadrDatabaseOption               y => this.Traverse(y),
            DelayedDurabilityDatabaseOption  y => this.Traverse(y),
            CursorDefaultDatabaseOption      y=> this.Traverse(y),
            RecoveryDatabaseOption           y => this.Traverse(y),
            TargetRecoveryTimeDatabaseOption y => this.Traverse(y),
            PageVerifyDatabaseOption         y => this.Traverse(y),
            PartnerDatabaseOption            y => this.Traverse(y),
            WitnessDatabaseOption            y => this.Traverse(y),
            ParameterizationDatabaseOption   y => this.Traverse(y),
            LiteralDatabaseOption            y => this.Traverse(y),
            IdentifierDatabaseOption         y => this.Traverse(y),
            ChangeTrackingDatabaseOption     y => this.Traverse(y),
            QueryStoreDatabaseOption         y => this.Traverse(y),
            AutomaticTuningDatabaseOption    y => this.Traverse(y),
            FileStreamDatabaseOption         y => this.Traverse(y),
            CatalogCollationOption           y => this.Traverse(y),
            MaxSizeDatabaseOption            y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RemoteDataArchiveDatabaseOption Traverse(RemoteDataArchiveDatabaseOption x)=>x;
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OnOffDatabaseOption Traverse(OnOffDatabaseOption x) => x switch{
            AutoCreateStatisticsDatabaseOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///OnOffDatabaseOption:DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AutoCreateStatisticsDatabaseOption Traverse(AutoCreateStatisticsDatabaseOption x)=>x;
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ContainmentDatabaseOption Traverse(ContainmentDatabaseOption x)=>x;
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual HadrDatabaseOption Traverse(HadrDatabaseOption x) => x switch{
            HadrAvailabilityGroupDatabaseOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///HadrDatabaseOption:DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DelayedDurabilityDatabaseOption Traverse(DelayedDurabilityDatabaseOption x)=>x;
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CursorDefaultDatabaseOption Traverse(CursorDefaultDatabaseOption x)=>x;
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RecoveryDatabaseOption Traverse(RecoveryDatabaseOption x)=>x;
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TargetRecoveryTimeDatabaseOption Traverse(TargetRecoveryTimeDatabaseOption x)=>x;
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual PageVerifyDatabaseOption Traverse(PageVerifyDatabaseOption x)=>x;
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual PartnerDatabaseOption Traverse(PartnerDatabaseOption x)=>x;
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual WitnessDatabaseOption Traverse(WitnessDatabaseOption x)=>x;
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ParameterizationDatabaseOption Traverse(ParameterizationDatabaseOption x)=>x;
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LiteralDatabaseOption Traverse(LiteralDatabaseOption x)=>x;
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual IdentifierDatabaseOption Traverse(IdentifierDatabaseOption x)=>x;
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ChangeTrackingDatabaseOption Traverse(ChangeTrackingDatabaseOption x)=>x;
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual QueryStoreDatabaseOption Traverse(QueryStoreDatabaseOption x)=>x;
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AutomaticTuningDatabaseOption Traverse(AutomaticTuningDatabaseOption x)=>x;
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FileStreamDatabaseOption Traverse(FileStreamDatabaseOption x)=>x;
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CatalogCollationOption Traverse(CatalogCollationOption x)=>x;
        /// <summary>
        ///DatabaseOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MaxSizeDatabaseOption Traverse(MaxSizeDatabaseOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RemoteDataArchiveDatabaseSetting Traverse(RemoteDataArchiveDatabaseSetting x) => x switch{
            RemoteDataArchiveDbServerSetting                  y => this.Traverse(y),
            RemoteDataArchiveDbCredentialSetting              y => this.Traverse(y),
            RemoteDataArchiveDbFederatedServiceAccountSetting y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///RemoteDataArchiveDatabaseSetting:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RemoteDataArchiveDbServerSetting Traverse(RemoteDataArchiveDbServerSetting x)=>x;
        /// <summary>
        ///RemoteDataArchiveDatabaseSetting:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RemoteDataArchiveDbCredentialSetting Traverse(RemoteDataArchiveDbCredentialSetting x)=>x;
        /// <summary>
        ///RemoteDataArchiveDatabaseSetting:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RemoteDataArchiveDbFederatedServiceAccountSetting Traverse(RemoteDataArchiveDbFederatedServiceAccountSetting x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RetentionPeriodDefinition Traverse(RetentionPeriodDefinition x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TableSwitchOption Traverse(TableSwitchOption x) => x switch{
            LowPriorityLockWaitTableSwitchOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///TableSwitchOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LowPriorityLockWaitTableSwitchOption Traverse(LowPriorityLockWaitTableSwitchOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropClusteredConstraintOption Traverse(DropClusteredConstraintOption x) => x switch{
            DropClusteredConstraintStateOption                 y => this.Traverse(y),
            DropClusteredConstraintValueOption                 y => this.Traverse(y),
            DropClusteredConstraintMoveOption                  y => this.Traverse(y),
            DropClusteredConstraintWaitAtLowPriorityLockOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///DropClusteredConstraintOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropClusteredConstraintStateOption Traverse(DropClusteredConstraintStateOption x)=>x;
        /// <summary>
        ///DropClusteredConstraintOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropClusteredConstraintValueOption Traverse(DropClusteredConstraintValueOption x)=>x;
        /// <summary>
        ///DropClusteredConstraintOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropClusteredConstraintMoveOption Traverse(DropClusteredConstraintMoveOption x)=>x;
        /// <summary>
        ///DropClusteredConstraintOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropClusteredConstraintWaitAtLowPriorityLockOption Traverse(DropClusteredConstraintWaitAtLowPriorityLockOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterTableDropTableElement Traverse(AlterTableDropTableElement x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExecuteAsClause Traverse(ExecuteAsClause x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual QueueOption Traverse(QueueOption x) => x switch{
            QueueStateOption     y => this.Traverse(y),
            QueueProcedureOption y => this.Traverse(y),
            QueueValueOption     y => this.Traverse(y),
            QueueExecuteAsOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///QueueOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual QueueStateOption Traverse(QueueStateOption x)=>x;
        /// <summary>
        ///QueueOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual QueueProcedureOption Traverse(QueueProcedureOption x)=>x;
        /// <summary>
        ///QueueOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual QueueValueOption Traverse(QueueValueOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual QueueExecuteAsOption Traverse(QueueExecuteAsOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RouteOption Traverse(RouteOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SystemTimePeriodDefinition Traverse(SystemTimePeriodDefinition x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual IndexType Traverse(IndexType x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual PartitionSpecifier Traverse(PartitionSpecifier x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FileGroupOrPartitionScheme Traverse(FileGroupOrPartitionScheme x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual IndexOption Traverse(IndexOption x) => x switch{
            IndexStateOption            y => this.Traverse(y),
            IndexExpressionOption       y => this.Traverse(y),
            MaxDurationOption           y => this.Traverse(y),
            WaitAtLowPriorityOption     y => this.Traverse(y),
            OrderIndexOption            y => this.Traverse(y),
            MoveToDropIndexOption       y => this.Traverse(y),
            FileStreamOnDropIndexOption y => this.Traverse(y),
            DataCompressionOption       y => this.Traverse(y),
            CompressionDelayIndexOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///IndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual IndexStateOption Traverse(IndexStateOption x) => x switch{
            OnlineIndexOption       y => this.Traverse(y),
            IgnoreDupKeyIndexOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///IndexStateOption:IndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OnlineIndexOption Traverse(OnlineIndexOption x)=>x;
        /// <summary>
        ///IndexStateOption:IndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual IgnoreDupKeyIndexOption Traverse(IgnoreDupKeyIndexOption x)=>x;
        /// <summary>
        ///IndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual IndexExpressionOption Traverse(IndexExpressionOption x)=>x;
        /// <summary>
        ///IndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MaxDurationOption Traverse(MaxDurationOption x)=>x;
        /// <summary>
        ///IndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual WaitAtLowPriorityOption Traverse(WaitAtLowPriorityOption x)=>x;
        /// <summary>
        ///IndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OrderIndexOption Traverse(OrderIndexOption x)=>x;
        /// <summary>
        ///IndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MoveToDropIndexOption Traverse(MoveToDropIndexOption x)=>x;
        /// <summary>
        ///IndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FileStreamOnDropIndexOption Traverse(FileStreamOnDropIndexOption x)=>x;
        /// <summary>
        ///IndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DataCompressionOption Traverse(DataCompressionOption x)=>x;
        /// <summary>
        ///IndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CompressionDelayIndexOption Traverse(CompressionDelayIndexOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OnlineIndexLowPriorityLockWaitOption Traverse(OnlineIndexLowPriorityLockWaitOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LowPriorityLockWaitOption Traverse(LowPriorityLockWaitOption x) => x switch{
            LowPriorityLockWaitMaxDurationOption    y => this.Traverse(y),
            LowPriorityLockWaitAbortAfterWaitOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///LowPriorityLockWaitOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LowPriorityLockWaitMaxDurationOption Traverse(LowPriorityLockWaitMaxDurationOption x)=>x;
        /// <summary>
        ///LowPriorityLockWaitOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LowPriorityLockWaitAbortAfterWaitOption Traverse(LowPriorityLockWaitAbortAfterWaitOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FullTextIndexColumn Traverse(FullTextIndexColumn x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FullTextIndexOption Traverse(FullTextIndexOption x) => x switch{
            ChangeTrackingFullTextIndexOption     y => this.Traverse(y),
            StopListFullTextIndexOption           y => this.Traverse(y),
            SearchPropertyListFullTextIndexOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///FullTextIndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ChangeTrackingFullTextIndexOption Traverse(ChangeTrackingFullTextIndexOption x)=>x;
        /// <summary>
        ///FullTextIndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual StopListFullTextIndexOption Traverse(StopListFullTextIndexOption x)=>x;
        /// <summary>
        ///FullTextIndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SearchPropertyListFullTextIndexOption Traverse(SearchPropertyListFullTextIndexOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FullTextCatalogAndFileGroup Traverse(FullTextCatalogAndFileGroup x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual EventTypeGroupContainer Traverse(EventTypeGroupContainer x) => x switch{
            EventTypeContainer  y => this.Traverse(y),
            EventGroupContainer y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///EventTypeGroupContainer:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual EventTypeContainer Traverse(EventTypeContainer x)=>x;
        /// <summary>
        ///EventTypeGroupContainer:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual EventGroupContainer Traverse(EventGroupContainer x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual EventNotificationObjectScope Traverse(EventNotificationObjectScope x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ApplicationRoleOption Traverse(ApplicationRoleOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterRoleAction Traverse(AlterRoleAction x) => x switch{
            RenameAlterRoleAction     y => this.Traverse(y),
            AddMemberAlterRoleAction  y => this.Traverse(y),
            DropMemberAlterRoleAction y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AlterRoleAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RenameAlterRoleAction Traverse(RenameAlterRoleAction x)=>x;
        /// <summary>
        ///AlterRoleAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AddMemberAlterRoleAction Traverse(AddMemberAlterRoleAction x)=>x;
        /// <summary>
        ///AlterRoleAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropMemberAlterRoleAction Traverse(DropMemberAlterRoleAction x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual UserLoginOption Traverse(UserLoginOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual StatisticsOption Traverse(StatisticsOption x) => x switch{
            ResampleStatisticsOption y => this.Traverse(y),
            OnOffStatisticsOption    y => this.Traverse(y),
            LiteralStatisticsOption  y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///StatisticsOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ResampleStatisticsOption Traverse(ResampleStatisticsOption x)=>x;
        /// <summary>
        ///StatisticsOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OnOffStatisticsOption Traverse(OnOffStatisticsOption x)=>x;
        /// <summary>
        ///StatisticsOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LiteralStatisticsOption Traverse(LiteralStatisticsOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual StatisticsPartitionRange Traverse(StatisticsPartitionRange x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CursorDefinition Traverse(CursorDefinition x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CursorId Traverse(CursorId x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CryptoMechanism Traverse(CryptoMechanism x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FetchType Traverse(FetchType x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual WhereClause Traverse(WhereClause x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropIndexClauseBase Traverse(DropIndexClauseBase x) => x switch{
            BackwardsCompatibleDropIndexClause y => this.Traverse(y),
            DropIndexClause                    y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///DropIndexClauseBase:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BackwardsCompatibleDropIndexClause Traverse(BackwardsCompatibleDropIndexClause x)=>x;
        /// <summary>
        ///DropIndexClauseBase:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropIndexClause Traverse(DropIndexClause x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SetCommand Traverse(SetCommand x) => x switch{
            GeneralSetCommand     y => this.Traverse(y),
            SetFipsFlaggerCommand y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///SetCommand:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual GeneralSetCommand Traverse(GeneralSetCommand x)=>x;
        /// <summary>
        ///SetCommand:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SetFipsFlaggerCommand Traverse(SetFipsFlaggerCommand x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FileDeclaration Traverse(FileDeclaration x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FileDeclarationOption Traverse(FileDeclarationOption x) => x switch{
            NameFileDeclarationOption       y => this.Traverse(y),
            FileNameFileDeclarationOption   y => this.Traverse(y),
            SizeFileDeclarationOption       y => this.Traverse(y),
            MaxSizeFileDeclarationOption    y => this.Traverse(y),
            FileGrowthFileDeclarationOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///FileDeclarationOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual NameFileDeclarationOption Traverse(NameFileDeclarationOption x)=>x;
        /// <summary>
        ///FileDeclarationOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FileNameFileDeclarationOption Traverse(FileNameFileDeclarationOption x)=>x;
        /// <summary>
        ///FileDeclarationOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SizeFileDeclarationOption Traverse(SizeFileDeclarationOption x)=>x;
        /// <summary>
        ///FileDeclarationOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MaxSizeFileDeclarationOption Traverse(MaxSizeFileDeclarationOption x)=>x;
        /// <summary>
        ///FileDeclarationOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FileGrowthFileDeclarationOption Traverse(FileGrowthFileDeclarationOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FileGroupDefinition Traverse(FileGroupDefinition x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DatabaseConfigurationClearOption Traverse(DatabaseConfigurationClearOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DatabaseConfigurationSetOption Traverse(DatabaseConfigurationSetOption x) => x switch{
            OnOffPrimaryConfigurationOption y => this.Traverse(y),
            MaxDopConfigurationOption       y => this.Traverse(y),
            GenericConfigurationOption      y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///DatabaseConfigurationSetOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OnOffPrimaryConfigurationOption Traverse(OnOffPrimaryConfigurationOption x)=>x;
        /// <summary>
        ///DatabaseConfigurationSetOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MaxDopConfigurationOption Traverse(MaxDopConfigurationOption x)=>x;
        /// <summary>
        ///DatabaseConfigurationSetOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual GenericConfigurationOption Traverse(GenericConfigurationOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterDatabaseTermination Traverse(AlterDatabaseTermination x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ChangeTrackingOptionDetail Traverse(ChangeTrackingOptionDetail x) => x switch{
            AutoCleanupChangeTrackingOptionDetail     y => this.Traverse(y),
            ChangeRetentionChangeTrackingOptionDetail y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ChangeTrackingOptionDetail:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AutoCleanupChangeTrackingOptionDetail Traverse(AutoCleanupChangeTrackingOptionDetail x)=>x;
        /// <summary>
        ///ChangeTrackingOptionDetail:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ChangeRetentionChangeTrackingOptionDetail Traverse(ChangeRetentionChangeTrackingOptionDetail x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual QueryStoreOption Traverse(QueryStoreOption x) => x switch{
            QueryStoreDesiredStateOption      y => this.Traverse(y),
            QueryStoreCapturePolicyOption     y => this.Traverse(y),
            QueryStoreSizeCleanupPolicyOption y => this.Traverse(y),
            QueryStoreDataFlushIntervalOption y => this.Traverse(y),
            QueryStoreIntervalLengthOption    y => this.Traverse(y),
            QueryStoreMaxStorageSizeOption    y => this.Traverse(y),
            QueryStoreMaxPlansPerQueryOption  y => this.Traverse(y),
            QueryStoreTimeCleanupPolicyOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///QueryStoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual QueryStoreDesiredStateOption Traverse(QueryStoreDesiredStateOption x)=>x;
        /// <summary>
        ///QueryStoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual QueryStoreCapturePolicyOption Traverse(QueryStoreCapturePolicyOption x)=>x;
        /// <summary>
        ///QueryStoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual QueryStoreSizeCleanupPolicyOption Traverse(QueryStoreSizeCleanupPolicyOption x)=>x;
        /// <summary>
        ///QueryStoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual QueryStoreDataFlushIntervalOption Traverse(QueryStoreDataFlushIntervalOption x)=>x;
        /// <summary>
        ///QueryStoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual QueryStoreIntervalLengthOption Traverse(QueryStoreIntervalLengthOption x)=>x;
        /// <summary>
        ///QueryStoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual QueryStoreMaxStorageSizeOption Traverse(QueryStoreMaxStorageSizeOption x)=>x;
        /// <summary>
        ///QueryStoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual QueryStoreMaxPlansPerQueryOption Traverse(QueryStoreMaxPlansPerQueryOption x)=>x;
        /// <summary>
        ///QueryStoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual QueryStoreTimeCleanupPolicyOption Traverse(QueryStoreTimeCleanupPolicyOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AutomaticTuningOption Traverse(AutomaticTuningOption x) => x switch{
            AutomaticTuningForceLastGoodPlanOption y => this.Traverse(y),
            AutomaticTuningCreateIndexOption       y => this.Traverse(y),
            AutomaticTuningDropIndexOption         y => this.Traverse(y),
            AutomaticTuningMaintainIndexOption     y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AutomaticTuningOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AutomaticTuningForceLastGoodPlanOption Traverse(AutomaticTuningForceLastGoodPlanOption x)=>x;
        /// <summary>
        ///AutomaticTuningOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AutomaticTuningCreateIndexOption Traverse(AutomaticTuningCreateIndexOption x)=>x;
        /// <summary>
        ///AutomaticTuningOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AutomaticTuningDropIndexOption Traverse(AutomaticTuningDropIndexOption x)=>x;
        /// <summary>
        ///AutomaticTuningOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AutomaticTuningMaintainIndexOption Traverse(AutomaticTuningMaintainIndexOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ColumnDefinitionBase Traverse(ColumnDefinitionBase x) => x switch{
            ColumnDefinition y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ColumnDefinitionBase:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ColumnDefinition Traverse(ColumnDefinition x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ColumnEncryptionDefinition Traverse(ColumnEncryptionDefinition x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ColumnEncryptionDefinitionParameter Traverse(ColumnEncryptionDefinitionParameter x) => x switch{
            ColumnEncryptionKeyNameParameter   y => this.Traverse(y),
            ColumnEncryptionTypeParameter      y => this.Traverse(y),
            ColumnEncryptionAlgorithmParameter y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ColumnEncryptionDefinitionParameter:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ColumnEncryptionKeyNameParameter Traverse(ColumnEncryptionKeyNameParameter x)=>x;
        /// <summary>
        ///ColumnEncryptionDefinitionParameter:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ColumnEncryptionTypeParameter Traverse(ColumnEncryptionTypeParameter x)=>x;
        /// <summary>
        ///ColumnEncryptionDefinitionParameter:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ColumnEncryptionAlgorithmParameter Traverse(ColumnEncryptionAlgorithmParameter x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual IdentityOptions Traverse(IdentityOptions x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ColumnStorageOptions Traverse(ColumnStorageOptions x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ConstraintDefinition Traverse(ConstraintDefinition x) => x switch{
            CheckConstraintDefinition           y => this.Traverse(y),
            DefaultConstraintDefinition         y => this.Traverse(y),
            ForeignKeyConstraintDefinition      y => this.Traverse(y),
            NullableConstraintDefinition        y => this.Traverse(y),
            GraphConnectionConstraintDefinition y => this.Traverse(y),
            UniqueConstraintDefinition          y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///ConstraintDefinition:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CheckConstraintDefinition Traverse(CheckConstraintDefinition x)=>x;
        /// <summary>
        ///ConstraintDefinition:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DefaultConstraintDefinition Traverse(DefaultConstraintDefinition x)=>x;
        /// <summary>
        ///ConstraintDefinition:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ForeignKeyConstraintDefinition Traverse(ForeignKeyConstraintDefinition x)=>x;
        /// <summary>
        ///ConstraintDefinition:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual NullableConstraintDefinition Traverse(NullableConstraintDefinition x)=>x;
        /// <summary>
        ///ConstraintDefinition:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual GraphConnectionConstraintDefinition Traverse(GraphConnectionConstraintDefinition x)=>x;
        /// <summary>
        ///ConstraintDefinition:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual UniqueConstraintDefinition Traverse(UniqueConstraintDefinition x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FederationScheme Traverse(FederationScheme x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TableDistributionPolicy Traverse(TableDistributionPolicy x) => x switch{
            TableReplicateDistributionPolicy  y => this.Traverse(y),
            TableRoundRobinDistributionPolicy y => this.Traverse(y),
            TableHashDistributionPolicy       y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///TableDistributionPolicy:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TableReplicateDistributionPolicy Traverse(TableReplicateDistributionPolicy x)=>x;
        /// <summary>
        ///TableDistributionPolicy:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TableRoundRobinDistributionPolicy Traverse(TableRoundRobinDistributionPolicy x)=>x;
        /// <summary>
        ///TableDistributionPolicy:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TableHashDistributionPolicy Traverse(TableHashDistributionPolicy x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TableIndexType Traverse(TableIndexType x) => x switch{
            TableClusteredIndexType    y => this.Traverse(y),
            TableNonClusteredIndexType y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///TableIndexType:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TableClusteredIndexType Traverse(TableClusteredIndexType x)=>x;
        /// <summary>
        ///TableIndexType:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TableNonClusteredIndexType Traverse(TableNonClusteredIndexType x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual PartitionSpecifications Traverse(PartitionSpecifications x) => x switch{
            TablePartitionOptionSpecifications y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///PartitionSpecifications:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TablePartitionOptionSpecifications Traverse(TablePartitionOptionSpecifications x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CompressionPartitionRange Traverse(CompressionPartitionRange x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual GraphConnectionBetweenNodes Traverse(GraphConnectionBetweenNodes x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RestoreOption Traverse(RestoreOption x) => x switch{
            ScalarExpressionRestoreOption y => this.Traverse(y),
            MoveRestoreOption             y => this.Traverse(y),
            StopRestoreOption             y => this.Traverse(y),
            FileStreamRestoreOption       y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///RestoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ScalarExpressionRestoreOption Traverse(ScalarExpressionRestoreOption x)=>x;
        /// <summary>
        ///RestoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MoveRestoreOption Traverse(MoveRestoreOption x)=>x;
        /// <summary>
        ///RestoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual StopRestoreOption Traverse(StopRestoreOption x)=>x;
        /// <summary>
        ///RestoreOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FileStreamRestoreOption Traverse(FileStreamRestoreOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BackupOption Traverse(BackupOption x) => x switch{
            BackupEncryptionOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///BackupOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BackupEncryptionOption Traverse(BackupEncryptionOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DeviceInfo Traverse(DeviceInfo x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MirrorToClause Traverse(MirrorToClause x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BackupRestoreFileInfo Traverse(BackupRestoreFileInfo x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BulkInsertOption Traverse(BulkInsertOption x) => x switch{
            LiteralBulkInsertOption y => this.Traverse(y),
            OrderBulkInsertOption   y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///BulkInsertOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LiteralBulkInsertOption Traverse(LiteralBulkInsertOption x)=>x;
        /// <summary>
        ///BulkInsertOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OrderBulkInsertOption Traverse(OrderBulkInsertOption x)=>x;
        /// <summary>
        ///BulkInsertOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExternalTableColumnDefinition Traverse(ExternalTableColumnDefinition x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual InsertBulkColumnDefinition Traverse(InsertBulkColumnDefinition x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DbccOption Traverse(DbccOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DbccNamedLiteral Traverse(DbccNamedLiteral x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual PartitionParameterType Traverse(PartitionParameterType x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RemoteServiceBindingOption Traverse(RemoteServiceBindingOption x) => x switch{
            OnOffRemoteServiceBindingOption y => this.Traverse(y),
            UserRemoteServiceBindingOption  y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///RemoteServiceBindingOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OnOffRemoteServiceBindingOption Traverse(OnOffRemoteServiceBindingOption x)=>x;
        /// <summary>
        ///RemoteServiceBindingOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual UserRemoteServiceBindingOption Traverse(UserRemoteServiceBindingOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual EncryptionSource Traverse(EncryptionSource x) => x switch{
            AssemblyEncryptionSource y => this.Traverse(y),
            FileEncryptionSource     y => this.Traverse(y),
            ProviderEncryptionSource y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///EncryptionSource:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AssemblyEncryptionSource Traverse(AssemblyEncryptionSource x)=>x;
        /// <summary>
        ///EncryptionSource:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FileEncryptionSource Traverse(FileEncryptionSource x)=>x;
        /// <summary>
        ///EncryptionSource:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ProviderEncryptionSource Traverse(ProviderEncryptionSource x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CertificateOption Traverse(CertificateOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ContractMessage Traverse(ContractMessage x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual EndpointAffinity Traverse(EndpointAffinity x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual EndpointProtocolOption Traverse(EndpointProtocolOption x) => x switch{
            LiteralEndpointProtocolOption        y => this.Traverse(y),
            AuthenticationEndpointProtocolOption y => this.Traverse(y),
            PortsEndpointProtocolOption          y => this.Traverse(y),
            CompressionEndpointProtocolOption    y => this.Traverse(y),
            ListenerIPEndpointProtocolOption     y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///EndpointProtocolOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LiteralEndpointProtocolOption Traverse(LiteralEndpointProtocolOption x)=>x;
        /// <summary>
        ///EndpointProtocolOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AuthenticationEndpointProtocolOption Traverse(AuthenticationEndpointProtocolOption x)=>x;
        /// <summary>
        ///EndpointProtocolOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual PortsEndpointProtocolOption Traverse(PortsEndpointProtocolOption x)=>x;
        /// <summary>
        ///EndpointProtocolOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CompressionEndpointProtocolOption Traverse(CompressionEndpointProtocolOption x)=>x;
        /// <summary>
        ///EndpointProtocolOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ListenerIPEndpointProtocolOption Traverse(ListenerIPEndpointProtocolOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual IPv4 Traverse(IPv4 x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual PayloadOption Traverse(PayloadOption x) => x switch{
            SoapMethod                   y => this.Traverse(y),
            EnabledDisabledPayloadOption y => this.Traverse(y),
            WsdlPayloadOption            y => this.Traverse(y),
            LoginTypePayloadOption       y => this.Traverse(y),
            LiteralPayloadOption         y => this.Traverse(y),
            SessionTimeoutPayloadOption  y => this.Traverse(y),
            SchemaPayloadOption          y => this.Traverse(y),
            CharacterSetPayloadOption    y => this.Traverse(y),
            RolePayloadOption            y => this.Traverse(y),
            AuthenticationPayloadOption  y => this.Traverse(y),
            EncryptionPayloadOption      y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///PayloadOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SoapMethod Traverse(SoapMethod x)=>x;
        /// <summary>
        ///PayloadOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual EnabledDisabledPayloadOption Traverse(EnabledDisabledPayloadOption x)=>x;
        /// <summary>
        ///PayloadOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual WsdlPayloadOption Traverse(WsdlPayloadOption x)=>x;
        /// <summary>
        ///PayloadOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LoginTypePayloadOption Traverse(LoginTypePayloadOption x)=>x;
        /// <summary>
        ///PayloadOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LiteralPayloadOption Traverse(LiteralPayloadOption x)=>x;
        /// <summary>
        ///PayloadOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SessionTimeoutPayloadOption Traverse(SessionTimeoutPayloadOption x)=>x;
        /// <summary>
        ///PayloadOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SchemaPayloadOption Traverse(SchemaPayloadOption x)=>x;
        /// <summary>
        ///PayloadOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CharacterSetPayloadOption Traverse(CharacterSetPayloadOption x)=>x;
        /// <summary>
        ///PayloadOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RolePayloadOption Traverse(RolePayloadOption x)=>x;
        /// <summary>
        ///PayloadOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AuthenticationPayloadOption Traverse(AuthenticationPayloadOption x)=>x;
        /// <summary>
        ///PayloadOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual EncryptionPayloadOption Traverse(EncryptionPayloadOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual KeyOption Traverse(KeyOption x) => x switch{
            KeySourceKeyOption           y => this.Traverse(y),
            AlgorithmKeyOption           y => this.Traverse(y),
            IdentityValueKeyOption       y => this.Traverse(y),
            ProviderKeyNameKeyOption     y => this.Traverse(y),
            CreationDispositionKeyOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///KeyOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual KeySourceKeyOption Traverse(KeySourceKeyOption x)=>x;
        /// <summary>
        ///KeyOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlgorithmKeyOption Traverse(AlgorithmKeyOption x)=>x;
        /// <summary>
        ///KeyOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual IdentityValueKeyOption Traverse(IdentityValueKeyOption x)=>x;
        /// <summary>
        ///KeyOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ProviderKeyNameKeyOption Traverse(ProviderKeyNameKeyOption x)=>x;
        /// <summary>
        ///KeyOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreationDispositionKeyOption Traverse(CreationDispositionKeyOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FullTextCatalogOption Traverse(FullTextCatalogOption x) => x switch{
            OnOffFullTextCatalogOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///FullTextCatalogOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OnOffFullTextCatalogOption Traverse(OnOffFullTextCatalogOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ServiceContract Traverse(ServiceContract x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ComputeClause Traverse(ComputeClause x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ComputeFunction Traverse(ComputeFunction x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TableSampleClause Traverse(TableSampleClause x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExpressionWithSortOrder Traverse(ExpressionWithSortOrder x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual GroupByClause Traverse(GroupByClause x) => x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual GroupingSpecification Traverse(GroupingSpecification x) => x switch{
            ExpressionGroupingSpecification   y => this.Traverse(y),
            CompositeGroupingSpecification    y => this.Traverse(y),
            CubeGroupingSpecification         y => this.Traverse(y),
            RollupGroupingSpecification       y => this.Traverse(y),
            GrandTotalGroupingSpecification   y => this.Traverse(y),
            GroupingSetsGroupingSpecification y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///GroupingSpecification:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExpressionGroupingSpecification Traverse(ExpressionGroupingSpecification x) => x;
        /// <summary>
        ///GroupingSpecification:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CompositeGroupingSpecification Traverse(CompositeGroupingSpecification x)=>x;
        /// <summary>
        ///GroupingSpecification:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CubeGroupingSpecification Traverse(CubeGroupingSpecification x)=>x;
        /// <summary>
        ///GroupingSpecification:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual RollupGroupingSpecification Traverse(RollupGroupingSpecification x)=>x;
        /// <summary>
        ///GroupingSpecification:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual GrandTotalGroupingSpecification Traverse(GrandTotalGroupingSpecification x)=>x;
        /// <summary>
        ///GroupingSpecification:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual GroupingSetsGroupingSpecification Traverse(GroupingSetsGroupingSpecification x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OutputClause Traverse(OutputClause x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OutputIntoClause Traverse(OutputIntoClause x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual HavingClause Traverse(HavingClause x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OrderByClause Traverse(OrderByClause x)=>x;
        /// <summary>
        /// TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual QueryExpression QueryExpression(QueryExpression x) => x switch{
            QueryParenthesisExpression y => this.Traverse(y),
            QuerySpecification         y => this.Traverse(y),
            BinaryQueryExpression      y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///QueryExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual QueryParenthesisExpression Traverse(QueryParenthesisExpression x)=>x;
        /// <summary>
        ///QueryExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual QuerySpecification Traverse(QuerySpecification x)=>x;
        /// <summary>
        ///QueryExpression:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BinaryQueryExpression Traverse(BinaryQueryExpression x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FromClause Traverse(FromClause x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SelectElement Traverse(SelectElement x) => x switch{
            SelectScalarExpression y => this.Traverse(y),
            SelectStarExpression y => this.Traverse(y),
            SelectSetVariable    y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///SelectElement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SelectScalarExpression Traverse(SelectScalarExpression x)=>x;
        /// <summary>
        ///SelectElement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SelectStarExpression Traverse(SelectStarExpression x)=>x;
        /// <summary>
        ///SelectElement:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SelectSetVariable Traverse(SelectSetVariable x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TopRowFilter Traverse(TopRowFilter x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OffsetClause Traverse(OffsetClause x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterFullTextIndexAction Traverse(AlterFullTextIndexAction x) => x switch{
            SimpleAlterFullTextIndexAction                y => this.Traverse(y),
            SetStopListAlterFullTextIndexAction           y => this.Traverse(y),
            SetSearchPropertyListAlterFullTextIndexAction y => this.Traverse(y),
            DropAlterFullTextIndexAction                  y => this.Traverse(y),
            AddAlterFullTextIndexAction                   y => this.Traverse(y),
            AlterColumnAlterFullTextIndexAction           y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AlterFullTextIndexAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SimpleAlterFullTextIndexAction Traverse(SimpleAlterFullTextIndexAction x)=>x;
        /// <summary>
        ///AlterFullTextIndexAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SetStopListAlterFullTextIndexAction Traverse(SetStopListAlterFullTextIndexAction x)=>x;
        /// <summary>
        ///AlterFullTextIndexAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SetSearchPropertyListAlterFullTextIndexAction Traverse(SetSearchPropertyListAlterFullTextIndexAction x)=>x;
        /// <summary>
        ///AlterFullTextIndexAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropAlterFullTextIndexAction Traverse(DropAlterFullTextIndexAction x)=>x;
        /// <summary>
        ///AlterFullTextIndexAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AddAlterFullTextIndexAction Traverse(AddAlterFullTextIndexAction x)=>x;
        /// <summary>
        ///AlterFullTextIndexAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterColumnAlterFullTextIndexAction Traverse(AlterColumnAlterFullTextIndexAction x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SearchPropertyListAction Traverse(SearchPropertyListAction x) => x switch{
            AddSearchPropertyListAction  y => this.Traverse(y),
            DropSearchPropertyListAction y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///SearchPropertyListAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AddSearchPropertyListAction Traverse(AddSearchPropertyListAction x)=>x;
        /// <summary>
        ///SearchPropertyListAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DropSearchPropertyListAction Traverse(DropSearchPropertyListAction x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CreateLoginSource Traverse(CreateLoginSource x) => x switch{
            PasswordCreateLoginSource      y => this.Traverse(y),
            WindowsCreateLoginSource       y => this.Traverse(y),
            CertificateCreateLoginSource   y => this.Traverse(y),
            AsymmetricKeyCreateLoginSource y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///CreateLoginSource:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual PasswordCreateLoginSource Traverse(PasswordCreateLoginSource x)=>x;
        /// <summary>
        ///CreateLoginSource:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual WindowsCreateLoginSource Traverse(WindowsCreateLoginSource x)=>x;
        /// <summary>
        ///CreateLoginSource:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CertificateCreateLoginSource Traverse(CertificateCreateLoginSource x)=>x;
        /// <summary>
        ///CreateLoginSource:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AsymmetricKeyCreateLoginSource Traverse(AsymmetricKeyCreateLoginSource x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual PrincipalOption Traverse(PrincipalOption x) => x switch{
            OnOffPrincipalOption         y => this.Traverse(y),
            LiteralPrincipalOption       y => this.Traverse(y),
            IdentifierPrincipalOption    y => this.Traverse(y),
            PasswordAlterPrincipalOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///PrincipalOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OnOffPrincipalOption Traverse(OnOffPrincipalOption x)=>x;
        /// <summary>
        ///PrincipalOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LiteralPrincipalOption Traverse(LiteralPrincipalOption x)=>x;
        /// <summary>
        ///PrincipalOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual IdentifierPrincipalOption Traverse(IdentifierPrincipalOption x)=>x;
        /// <summary>
        ///PrincipalOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual PasswordAlterPrincipalOption Traverse(PasswordAlterPrincipalOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DialogOption Traverse(DialogOption x) => x switch{
            ScalarExpressionDialogOption y => this.Traverse(y),
            OnOffDialogOption            y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///DialogOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ScalarExpressionDialogOption Traverse(ScalarExpressionDialogOption x)=>x;
        /// <summary>
        ///DialogOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OnOffDialogOption Traverse(OnOffDialogOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TSqlFragmentSnippet Traverse(TSqlFragmentSnippet x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TSqlScript Traverse(TSqlScript x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TSqlBatch Traverse(TSqlBatch x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MergeActionClause Traverse(MergeActionClause x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MergeAction Traverse(MergeAction x) => x switch{
            UpdateMergeAction y => this.Traverse(y),
            DeleteMergeAction y => this.Traverse(y),
            InsertMergeAction y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///MergeAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual UpdateMergeAction Traverse(UpdateMergeAction x)=>x;
        /// <summary>
        ///MergeAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DeleteMergeAction Traverse(DeleteMergeAction x)=>x;
        /// <summary>
        ///MergeAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual InsertMergeAction Traverse(InsertMergeAction x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AuditSpecificationPart Traverse(AuditSpecificationPart x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AuditSpecificationDetail Traverse(AuditSpecificationDetail x) => x switch{
            AuditActionSpecification  y => this.Traverse(y),
            AuditActionGroupReference y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AuditSpecificationDetail:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AuditActionSpecification Traverse(AuditActionSpecification x)=>x;
        /// <summary>
        ///AuditSpecificationDetail:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AuditActionGroupReference Traverse(AuditActionGroupReference x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DatabaseAuditAction Traverse(DatabaseAuditAction x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AuditOption Traverse(AuditOption x) => x switch{
            QueueDelayAuditOption y => this.Traverse(y),
            AuditGuidAuditOption  y => this.Traverse(y),
            OnFailureAuditOption  y => this.Traverse(y),
            StateAuditOption      y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AuditOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual QueueDelayAuditOption Traverse(QueueDelayAuditOption x)=>x;
        /// <summary>
        ///AuditOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AuditGuidAuditOption Traverse(AuditGuidAuditOption x)=>x;
        /// <summary>
        ///AuditOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OnFailureAuditOption Traverse(OnFailureAuditOption x)=>x;
        /// <summary>
        ///AuditOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual StateAuditOption Traverse(StateAuditOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AuditTargetOption Traverse(AuditTargetOption x) => x switch{
            MaxSizeAuditTargetOption          y => this.Traverse(y),
            MaxRolloverFilesAuditTargetOption y => this.Traverse(y),
            LiteralAuditTargetOption          y => this.Traverse(y),
            OnOffAuditTargetOption            y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AuditTargetOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MaxSizeAuditTargetOption Traverse(MaxSizeAuditTargetOption x)=>x;
        /// <summary>
        ///AuditTargetOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MaxRolloverFilesAuditTargetOption Traverse(MaxRolloverFilesAuditTargetOption x)=>x;
        /// <summary>
        ///AuditTargetOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LiteralAuditTargetOption Traverse(LiteralAuditTargetOption x)=>x;
        /// <summary>
        ///AuditTargetOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OnOffAuditTargetOption Traverse(OnOffAuditTargetOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ResourcePoolParameter Traverse(ResourcePoolParameter x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ResourcePoolAffinitySpecification Traverse(ResourcePoolAffinitySpecification x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExternalResourcePoolParameter Traverse(ExternalResourcePoolParameter x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual ExternalResourcePoolAffinitySpecification Traverse(ExternalResourcePoolAffinitySpecification x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual WorkloadGroupParameter Traverse(WorkloadGroupParameter x) => x switch{
            WorkloadGroupResourceParameter   y => this.Traverse(y),
            WorkloadGroupImportanceParameter y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///WorkloadGroupParameter:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual WorkloadGroupResourceParameter Traverse(WorkloadGroupResourceParameter x)=>x;
        /// <summary>
        ///WorkloadGroupParameter:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual WorkloadGroupImportanceParameter Traverse(WorkloadGroupImportanceParameter x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BrokerPriorityParameter Traverse(BrokerPriorityParameter x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FullTextStopListAction Traverse(FullTextStopListAction x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual EventSessionObjectName Traverse(EventSessionObjectName x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual EventDeclaration Traverse(EventDeclaration x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual EventDeclarationSetParameter Traverse(EventDeclarationSetParameter x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TargetDeclaration Traverse(TargetDeclaration x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SessionOption Traverse(SessionOption x) => x switch{
            EventRetentionSessionOption     y => this.Traverse(y),
            MemoryPartitionSessionOption    y => this.Traverse(y),
            LiteralSessionOption            y => this.Traverse(y),
            MaxDispatchLatencySessionOption y => this.Traverse(y),
            OnOffSessionOption              y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///SessionOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual EventRetentionSessionOption Traverse(EventRetentionSessionOption x)=>x;
        /// <summary>
        ///SessionOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MemoryPartitionSessionOption Traverse(MemoryPartitionSessionOption x)=>x;
        /// <summary>
        ///SessionOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LiteralSessionOption Traverse(LiteralSessionOption x)=>x;
        /// <summary>
        ///SessionOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual MaxDispatchLatencySessionOption Traverse(MaxDispatchLatencySessionOption x)=>x;
        /// <summary>
        ///SessionOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual OnOffSessionOption Traverse(OnOffSessionOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SpatialIndexOption Traverse(SpatialIndexOption x) => x switch{
            SpatialIndexRegularOption        y => this.Traverse(y),
            BoundingBoxSpatialIndexOption    y => this.Traverse(y),
            GridsSpatialIndexOption          y => this.Traverse(y),
            CellsPerObjectSpatialIndexOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///SpatialIndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SpatialIndexRegularOption Traverse(SpatialIndexRegularOption x)=>x;
        /// <summary>
        ///SpatialIndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BoundingBoxSpatialIndexOption Traverse(BoundingBoxSpatialIndexOption x)=>x;
        /// <summary>
        ///SpatialIndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual GridsSpatialIndexOption Traverse(GridsSpatialIndexOption x)=>x;
        /// <summary>
        ///SpatialIndexOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual CellsPerObjectSpatialIndexOption Traverse(CellsPerObjectSpatialIndexOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual BoundingBoxParameter Traverse(BoundingBoxParameter x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual GridParameter Traverse(GridParameter x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterServerConfigurationBufferPoolExtensionOption Traverse(AlterServerConfigurationBufferPoolExtensionOption x) => x switch{
            AlterServerConfigurationBufferPoolExtensionContainerOption y => this.Traverse(y),
            AlterServerConfigurationBufferPoolExtensionSizeOption      y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AlterServerConfigurationBufferPoolExtensionOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterServerConfigurationBufferPoolExtensionContainerOption Traverse(AlterServerConfigurationBufferPoolExtensionContainerOption x)=>x;
        /// <summary>
        ///AlterServerConfigurationBufferPoolExtensionOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterServerConfigurationBufferPoolExtensionSizeOption Traverse(AlterServerConfigurationBufferPoolExtensionSizeOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterServerConfigurationDiagnosticsLogOption Traverse(AlterServerConfigurationDiagnosticsLogOption x) => x switch{
            AlterServerConfigurationDiagnosticsLogMaxSizeOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AlterServerConfigurationDiagnosticsLogOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterServerConfigurationDiagnosticsLogMaxSizeOption Traverse(AlterServerConfigurationDiagnosticsLogMaxSizeOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterServerConfigurationFailoverClusterPropertyOption Traverse(AlterServerConfigurationFailoverClusterPropertyOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterServerConfigurationHadrClusterOption Traverse(AlterServerConfigurationHadrClusterOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterServerConfigurationSoftNumaOption Traverse(AlterServerConfigurationSoftNumaOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AvailabilityReplica Traverse(AvailabilityReplica x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AvailabilityReplicaOption Traverse(AvailabilityReplicaOption x) => x switch{
            LiteralReplicaOption          y => this.Traverse(y),
            AvailabilityModeReplicaOption y => this.Traverse(y),
            FailoverModeReplicaOption     y => this.Traverse(y),
            PrimaryRoleReplicaOption      y => this.Traverse(y),
            SecondaryRoleReplicaOption    y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AvailabilityReplicaOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LiteralReplicaOption Traverse(LiteralReplicaOption x)=>x;
        /// <summary>
        ///AvailabilityReplicaOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AvailabilityModeReplicaOption Traverse(AvailabilityModeReplicaOption x)=>x;
        /// <summary>
        ///AvailabilityReplicaOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual FailoverModeReplicaOption Traverse(FailoverModeReplicaOption x)=>x;
        /// <summary>
        ///AvailabilityReplicaOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual PrimaryRoleReplicaOption Traverse(PrimaryRoleReplicaOption x)=>x;
        /// <summary>
        ///AvailabilityReplicaOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SecondaryRoleReplicaOption Traverse(SecondaryRoleReplicaOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AvailabilityGroupOption Traverse(AvailabilityGroupOption x) => x switch{
            LiteralAvailabilityGroupOption y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AvailabilityGroupOption:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual LiteralAvailabilityGroupOption Traverse(LiteralAvailabilityGroupOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterAvailabilityGroupAction Traverse(AlterAvailabilityGroupAction x) => x switch{
            AlterAvailabilityGroupFailoverAction y => this.Traverse(y),
            _ =>Throw(x)
        };
        /// <summary>
        ///AlterAvailabilityGroupAction:TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterAvailabilityGroupFailoverAction Traverse(AlterAvailabilityGroupFailoverAction x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual AlterAvailabilityGroupFailoverOption Traverse(AlterAvailabilityGroupFailoverOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual DiskStatementOption Traverse(DiskStatementOption x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual WindowFrameClause Traverse(WindowFrameClause x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual WindowDelimiter Traverse(WindowDelimiter x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual WithinGroupClause Traverse(WithinGroupClause x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual SelectiveXmlIndexPromotedPath Traverse(SelectiveXmlIndexPromotedPath x)=>x;
        /// <summary>
        ///TSqlFragment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual TemporalClause Traverse(TemporalClause x)=>x;
    }
}