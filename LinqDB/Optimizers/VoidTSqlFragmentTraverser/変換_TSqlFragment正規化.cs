
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace LinqDB.Optimizers.VoidTSqlFragmentTraverser;

/// <summary>
/// TSQLからLINQに変換する。
/// </summary>
internal class 変換_TSqlFragment正規化:VoidTSqlFragmentTraverser{
    public 変換_TSqlFragment正規化(Sql160ScriptGenerator ScriptGenerator):base(ScriptGenerator){
    }
    //private String Database;
    public void 実行(TSqlFragment x){
        this.Dictionary_FROM句WITH.Clear();
        //this.Database=Database;
        //this.SQL=SQL;
        this.TSqlFragment(x);
    }
    //private Dictionary<String,SchemaObjectName> Dictionary_PrimaryExpression_PrimaryExpression = new Dictionary<String,SchemaObjectName>();
    private readonly Dictionary<string,QueryDerivedTable> Dictionary_FROM句WITH=new(StringComparer.OrdinalIgnoreCase);
    protected override void FunctionCall(FunctionCall x){
        if(string.Equals(x.FunctionName.Value,"TypePropertyEx",StringComparison.OrdinalIgnoreCase)) x.FunctionName.Value="TypeProperty";
        if(x.FunctionName.Value=="sysconv") x.FunctionName.Value="convert";
        base.FunctionCall(x);
    }
    //protected override void SqlCommandIdentifier(SqlCommandIdentifier x) => this.Identifier(x);
    //protected override void IdentifierSnippet(IdentifierSnippet x) => this.Identifier(x);
    protected override void Identifier(Identifier x){
        //if(x is null)return;
        //識別子は一貫性のために[]で加工
        x.QuoteType=QuoteType.NotQuoted;
        //EscapedName
        //var Value= x.Value.ToLowerInvariant().Replace(' ','_').Replace('-','_');
        //if(x.Value!=Value){}
        //x.Value=x.Value.ToLowerInvariant().Replace(' ','_').Replace('-','_');
        x.Value=x.Value.Replace(' ','_').Replace('-','_');
    }
    protected override void WithCtesAndXmlNamespaces(WithCtesAndXmlNamespaces x){
        //base.WithCtesAndXmlNamespaces(x);
        //return;
        if(x.ChangeTrackingContext is not null){
            this.ValueExpression(x.ChangeTrackingContext);
        }
        //WITH T(C0) AS (SELECT TOP 1 dbo.LINEITEM.L_ORDERKEY FROM dbo.LINEITEM)
        //SELECT T.C0→dbo.LINEITEM.L_ORDERKEY
        //FROM
        //    T→(SELECT TOP 1 dbo.LINEITEM.L_ORDERKEY FROM dbo.LINEITEM)
        var Dictionary_FROM句WITH=this.Dictionary_FROM句WITH;
        foreach(var CommonTableExpression in x.CommonTableExpressions){
            this.CommonTableExpression(CommonTableExpression);
            var QueryExpression=CommonTableExpression.QueryExpression;
            this.QueryExpression(QueryExpression);
            //Dictionary_TableAlias_ColumnAlias_Expression.Add(共通部分式名,QueryExpression);
            var QueryDerivedTable=new QueryDerivedTable{QueryExpression=QueryExpression,Alias=CommonTableExpression.ExpressionName};
            foreach(var Column in CommonTableExpression.Columns) QueryDerivedTable.Columns.Add(Column);
            //SqlScriptGenerator.GenerateScript(CommonTableExpression.ExpressionName,out var SQL);
            //this.Identifier(CommonTableExpression.ExpressionName);
            //Debug.Assert(CommonTableExpression.ExpressionName.Value.IsLower());
            Dictionary_FROM句WITH.Add(CommonTableExpression.ExpressionName.Value,QueryDerivedTable);
        }
        if(x.XmlNamespaces is not null){
            this.XmlNamespaces(x.XmlNamespaces);
        }
    }
    protected override void SelectStatement(SelectStatement x){
        if(x.WithCtesAndXmlNamespaces is not null) this.WithCtesAndXmlNamespaces(x.WithCtesAndXmlNamespaces);
        this.ComputeClauses(x.ComputeClauses);
        if(x.Into is not null) this.SwitchSchemaObjectName(x.Into);
        if(x.On is not null) this.SwitchIdentifier(x.On);
        this.OptimizerHints(x.OptimizerHints);
        this.QueryExpression(x.QueryExpression);
    }
    private static readonly SelectStarExpression _SelectStarExpression=new();
    private static readonly Identifier sys=new(){Value="sys"};
    //private static readonly Identifier Identifier_a= new(){Value="a"};
    //private static readonly Identifier all_objects = new(){ Value="all_objects"};
    //private static readonly Identifier objectsドル = new() { Value="objects$" };
    //private static readonly Identifier availability_replicas_internal = new() { Value="availability_replicas_internal" };
    //private static readonly Identifier configurations = new() { Value="configurations" };
    //private static readonly Identifier dm_hadr_internal_wsfc_ag_replicas = new() { Value="dm_hadr_internal_wsfc_ag_replicas" };









    //private static readonly SchemaObjectName Identifiers_sys_all_objects = new() {
    //    Identifiers={
    //        sys,
    //        new Identifier{ Value="all_objects"}
    //    }
    //};
    //private static readonly SelectScalarExpression SelectScalarExpression0 = new(){
    //    Expression=new IntegerLiteral {
    //        Value="null"
    //    },
    //    ColumnName=new IdentifierOrValueExpression {
    //        Identifier=new Identifier {
    //            Value="null_on_null_input"
    //        }
    //    }
    //};


    //private static readonly SchemaObjectName Identifiers_sys_configurations = new(){
    //    Identifiers={
    //        sys,
    //        new() { Value="configurations" }
    //    }
    //};

    ////private static readonly MultiPartIdentifier a_replica_metadata_id = new(){Identifiers={ Identifier_a,new Identifier{Value="replica_metadata_id" } } };
    ////private static readonly IdentifierOrValueExpression internal_group_id = new(){ Identifier=new Identifier{Value="internal_group_id" } };
    //////\\private static readonly MultiPartIdentifier a_replica_id = new MultiPartIdentifier{Identifiers={Identifier_a,new Identifier{Value="replica_id" } } };
    //////private static readonly MultiPartIdentifier a_replica_id = new MultiPartIdentifier{Identifiers={Identifier_a,new Identifier{Value="replica_server_name" } } };
    ////private static readonly IdentifierOrValueExpression a_ag_id = new(){ Identifier=new Identifier {Value="ag_replica_name" } };


    //private static readonly SelectScalarExpression SelectScalarExpression_replica_metadata_idからinternal_group_id = new SelectScalarExpression {
    //    Expression=new ColumnReferenceExpression {
    //        MultiPartIdentifier=new MultiPartIdentifier{Identifiers={ Identifier_a,new Identifier{Value="replica_metadata_id" } } }
    //    },
    //    ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="internal_group_id" } }
    //};
    //private static readonly MultiPartIdentifier a_replica_id = new MultiPartIdentifier{Identifiers={ Identifier_a,new Identifier{Value="replica_id" } } };
    ///////private static readonly IdentifierOrValueExpression a_ag_id = new(){ Identifier=new Identifier {Value="ag_id" } };
    //private static readonly SelectScalarExpression SelectScalarExpression_a_replica_idからag_id = new SelectScalarExpression {
    //    Expression=new ColumnReferenceExpression {
    //        MultiPartIdentifier=a_replica_id
    //    },
    //    ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="ag_id" } }
    //};
    //private static readonly SchemaObjectName Identifiers_sys_availability_replicas = new() {
    //    Identifiers={
    //        sys,
    //        new Identifier{Value="availability_replicas" }
    //    }
    //};
    //private static readonly SelectScalarExpression SelectScalarExpression_a_replica_idからag_replica_id = new SelectScalarExpression {
    //    Expression=new ColumnReferenceExpression {
    //        MultiPartIdentifier=a_replica_id
    //    },
    //    ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="ag_replica_id" } }
    //};
    //private static readonly SelectScalarExpression SelectScalarExpression_replica_server_nameからag_replica_name = new(){
    //    Expression=new ColumnReferenceExpression {
    //        MultiPartIdentifier=new MultiPartIdentifier{Identifiers={ Identifier_a,new Identifier{Value="replica_server_name" } } }
    //    },
    //    ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="ag_replica_name" } }
    //};
    //private static readonly NamedTableReference _NamedTableReference = new(){
    //    SchemaObject=Identifiers_sys_availability_replicas,
    //    Alias=Identifier_a
    //};
    private TableReference TableReference置換(TableReference x){
        if(x is NamedTableReference NamedTableReference){
            var SchemaObject=NamedTableReference.SchemaObject;
            Debug.Assert(SchemaObject is not null);
            if(SchemaObject.SchemaIdentifier is not null&&string.Equals(SchemaObject.SchemaIdentifier.Value,"sys",StringComparison.CurrentCultureIgnoreCase)){
                var Value=SchemaObject.BaseIdentifier.Value;
                if(string.Equals("objects$",Value,StringComparison.CurrentCultureIgnoreCase)){
                    return 共通0("objects$",
                        new QuerySpecification{
                            FromClause=new FromClause{
                                TableReferences={
                                    new NamedTableReference{
                                        SchemaObject=new SchemaObjectName{Identifiers={sys,new Identifier{Value="all_objects"}}}
                                        //Alias=NamedTableReference.Alias
                                    }
                                }
                            },
                            SelectElements={
                                _SelectStarExpression,
                                new SelectScalarExpression{Expression=new IntegerLiteral{Value="null"},ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="null_on_null_input"}}},
                                列bit("is_disabled"),
                                列bit("is_not_for_replication"),
                                列bit("is_not_trusted"),
                                列bit("property"),
                                列bit("uses_database_collation"),
                                列bit("is_system_named"),
                                列bit("is_auto_executed"),
                                列bit("is_execution_replicated"),
                                列bit("is_repl_serializable_only"),
                                列bit("skips_repl_constraints"),
                                列bit("is_enabled"),
                                列bit("is_security_policy_schema_bound"),
                                列bit("is_replicated"),
                                列bit("has_replication_filter"),
                                列bit("has_opaque_metadata"),
                                列bit("has_unchecked_assembly_data"),
                                列bit("with_check_option"),
                                列bit("is_auto_dropped"),
                                列bit("is_tracked_by_cdc"),
                                列bit("is_snapshot_view"),
                            }
                        }
                    );
                }
                if(string.Equals("configurations$",Value,StringComparison.CurrentCultureIgnoreCase)){
                    return 共通0("configurations$",
                        new QuerySpecification{
                            SelectElements={
                                new SelectScalarExpression{Expression=new IntegerLiteral{Value="1"},ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="configuration_id"}}},
                                new SelectScalarExpression{Expression=new StringLiteral{Value="name"},ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="name"}}},
                                new SelectScalarExpression{
                                    Expression=new ConvertCall{DataType=new SqlDataTypeReference{Name=new SchemaObjectName{Identifiers={new Identifier{Value="sql_variant"}}}},Parameter=new IntegerLiteral{Value="1"}},
                                    ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="value"}}
                                },
                                new SelectScalarExpression{
                                    Expression=new ConvertCall{DataType=new SqlDataTypeReference{Name=new SchemaObjectName{Identifiers={new Identifier{Value="sql_variant"}}}},Parameter=new IntegerLiteral{Value="1"}},
                                    ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="minimum"}}
                                },
                                new SelectScalarExpression{
                                    Expression=new ConvertCall{DataType=new SqlDataTypeReference{Name=new SchemaObjectName{Identifiers={new Identifier{Value="sql_variant"}}}},Parameter=new IntegerLiteral{Value="1"}},
                                    ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="maximum"}}
                                },
                                new SelectScalarExpression{
                                    Expression=new ConvertCall{DataType=new SqlDataTypeReference{Name=new SchemaObjectName{Identifiers={new Identifier{Value="sql_variant"}}}},Parameter=new IntegerLiteral{Value="1"}},
                                    ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="value_in_use"}}
                                },
                                new SelectScalarExpression{Expression=new StringLiteral{Value="description"},ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="description"}}},
                                new SelectScalarExpression{
                                    Expression=new ConvertCall{DataType=new SqlDataTypeReference{Name=new SchemaObjectName{Identifiers={new Identifier{Value="bit"}}}},Parameter=new IntegerLiteral{Value="1"}},
                                    ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="is_dynamic"}}
                                },
                                new SelectScalarExpression{
                                    Expression=new ConvertCall{DataType=new SqlDataTypeReference{Name=new SchemaObjectName{Identifiers={new Identifier{Value="bit"}}}},Parameter=new IntegerLiteral{Value="1"}},
                                    ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="is_advanced"}}
                                },
                                new SelectScalarExpression{
                                    Expression=new ConvertCall{DataType=new SqlDataTypeReference{Name=new SchemaObjectName{Identifiers={new Identifier{Value="bit"}}}},Parameter=new IntegerLiteral{Value="1"}},
                                    ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="is_not_use"}}
                                }
                            }
                        }
                    );
                }
                if(string.Equals("availability_replicas_internal",Value,StringComparison.CurrentCultureIgnoreCase)){
                    return 共通0("availability_replicas_internal",
                        new QuerySpecification{
                            FromClause=new FromClause{TableReferences={new NamedTableReference{SchemaObject=new SchemaObjectName{Identifiers={sys,new Identifier{Value="availability_replicas"}}}}}},
                            SelectElements={
                                _SelectStarExpression,
                                new SelectScalarExpression{
                                    Expression=new ColumnReferenceExpression{MultiPartIdentifier=new MultiPartIdentifier{Identifiers={new Identifier{Value="replica_metadata_id"}}}},
                                    ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="internal_group_id"}}
                                },
                                new SelectScalarExpression{
                                    Expression=new ColumnReferenceExpression{MultiPartIdentifier=new MultiPartIdentifier{Identifiers={new Identifier{Value="replica_id"}}}},
                                    ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="ag_id"}}
                                }
                            }
                        }
                    );
                }
                if(string.Equals("dm_hadr_internal_wsfc_ag_replicas",Value,StringComparison.CurrentCultureIgnoreCase)){
                    return 共通0("dm_hadr_internal_wsfc_ag_replicas",
                        new QuerySpecification{
                            FromClause=new FromClause{TableReferences={new NamedTableReference{SchemaObject=new SchemaObjectName{Identifiers={sys,new Identifier{Value="availability_replicas"}}}}}},
                            SelectElements={
                                _SelectStarExpression,
                                new SelectScalarExpression{
                                    Expression=new ColumnReferenceExpression{MultiPartIdentifier=new MultiPartIdentifier{Identifiers={new Identifier{Value="replica_metadata_id"}}}},
                                    ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="internal_group_id"}}
                                },
                                new SelectScalarExpression{
                                    Expression=new ColumnReferenceExpression{MultiPartIdentifier=new MultiPartIdentifier{Identifiers={new Identifier{Value="replica_id"}}}},
                                    ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="ag_id"}}
                                },
                                new SelectScalarExpression{
                                    Expression=new ColumnReferenceExpression{MultiPartIdentifier=new MultiPartIdentifier{Identifiers={new Identifier{Value="replica_id"}}}},
                                    ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="ag_replica_id"}}
                                },
                                new SelectScalarExpression{
                                    Expression=new ColumnReferenceExpression{MultiPartIdentifier=new MultiPartIdentifier{Identifiers={new Identifier{Value="replica_server_name"}}}},
                                    ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="ag_replica_name"}}
                                }
                            }
                        }
                    );
                    //return 共通0("dm_hadr_internal_wsfc_ag_replicas",
                    //    new QuerySpecification {
                    //        FromClause=new FromClause {
                    //            TableReferences={
                    //                new NamedTableReference{
                    //                    SchemaObject=new SchemaObjectName{
                    //                        Identifiers={
                    //                            sys,
                    //                            new Identifier{Value="availability_replicas" }
                    //                        }
                    //                    },
                    //                    Alias=Identifier_a
                    //                }
                    //            }
                    //        },
                    //        SelectElements={
                    //            _SelectStarExpression,
                    //            new SelectScalarExpression{
                    //                Expression=new ColumnReferenceExpression {
                    //                    MultiPartIdentifier=new MultiPartIdentifier{Identifiers={ Identifier_a,new Identifier{Value="replica_metadata_id" } } }
                    //                },
                    //                ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="internal_group_id" } }
                    //            },
                    //            new SelectScalarExpression{
                    //                Expression=new ColumnReferenceExpression {
                    //                    MultiPartIdentifier=new MultiPartIdentifier{Identifiers={Identifier_a,new Identifier{Value="replica_id" } } }
                    //                },
                    //                ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="ag_id" } }
                    //            },
                    //            new SelectScalarExpression {
                    //                Expression=new ColumnReferenceExpression {
                    //                    MultiPartIdentifier=new MultiPartIdentifier{Identifiers={Identifier_a,new Identifier{Value="replica_id" } } }
                    //                },
                    //                ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="ag_replica_id" } }
                    //            },
                    //            new SelectScalarExpression{
                    //                Expression=new ColumnReferenceExpression {
                    //                    MultiPartIdentifier=new MultiPartIdentifier{Identifiers={ Identifier_a,new Identifier{Value="replica_server_name" } } }
                    //                },
                    //                ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="ag_replica_name" } }
                    //            }
                    //        }
                    //    }
                    //);
                }
                if(string.Equals("dm_hadr_availability_replica_states",Value,StringComparison.CurrentCultureIgnoreCase)){
                    return 共通0("dm_hadr_availability_replica_states",
                        new QuerySpecification{
                            FromClause=new FromClause{TableReferences={new NamedTableReference{SchemaObject=new SchemaObjectName{Identifiers={sys,new Identifier{Value="availability_replicas"}}}}}},
                            SelectElements={
                                _SelectStarExpression,
                                new SelectScalarExpression{
                                    Expression=new ColumnReferenceExpression{MultiPartIdentifier=new MultiPartIdentifier{Identifiers={new Identifier{Value="replica_id"}}}},
                                    ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="real_replica_id"}}
                                },
                                new SelectScalarExpression{Expression=new IntegerLiteral{Value="1"},ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value="role"}}},
                            }
                        }
                    );
                }
                QueryDerivedTable 共通0(string Name,QuerySpecification QuerySpecification)=>new QueryDerivedTable{QueryExpression=QuerySpecification,Alias=new Identifier{Value=NamedTableReference.Alias is null?Name:NamedTableReference.Alias.Value}};
                //QueryDerivedTable? 共通(string Name,QuerySpecification QuerySpecification) {
                //    if(string.Equals(SchemaObject!.BaseIdentifier.Value,Name,StringComparison.CurrentCultureIgnoreCase)) {
                //        return new QueryDerivedTable {
                //            QueryExpression=QuerySpecification,
                //            Alias=new Identifier{Value=NamedTableReference.Alias is null ? Name : NamedTableReference.Alias.Value }
                //        };
                //        //SchemaObject.BaseIdentifier.Value="all_objects";
                //    }
                //    return null;
                //}
                //        //FromClause? 共通(string 内部のfromテーブル名,string 内部のfromエリアス名)=> new FromClause {
                //        //    TableReferences={
                //        //        new NamedTableReference{
                //        //            SchemaObject=new SchemaObjectName{
                //        //                Identifiers={
                //        //                    sys,
                //        //                    new Identifier{Value=内部のfromテーブル名}
                //        //                }
                //        //            },
                //        //            Alias=内部のfromエリアス名
                //        //        }
                //        //    }
                //        //};
            }
        }
        //if(x is NamedTableReference NamedTableReference&&this.Dictionary_FROM句WITH.TryGetValue(NamedTableReference.SchemaObject.BaseIdentifier.Value,out var Value)) {
        //    //TableReferences.RemoveAt(x);
        //    //TableReferences.Insert(x,Value);
        //    return Value;
        //}
        this.TableReference(x);
        return x;
        SelectScalarExpression 列bit(string Name)=>new(){Expression=new IntegerLiteral{Value="1"},ColumnName=new IdentifierOrValueExpression{Identifier=new Identifier{Value=Name}}};
    }
    protected override void FromClause(FromClause x){
        var TableReferences=x.TableReferences;
        var TableReferences_Count=TableReferences.Count;
        for(var a=0;a<TableReferences_Count;a++) TableReferences[a]=this.TableReference置換(TableReferences[a]);
    }
    protected override void PivotedTableReference(PivotedTableReference x){
        x.TableReference=this.TableReference置換(x.TableReference);
        this.MultiPartIdentifier(x.AggregateFunctionIdentifier);
        this.ColumnReferenceExpressions(x.ValueColumns);
        this.ColumnReferenceExpression(x.PivotColumn);
        this.Identifiers(x.InColumns);
        this.SwitchIdentifier(x.Alias);
    }
    protected override void UnqualifiedJoin(UnqualifiedJoin x){
        x.FirstTableReference=this.TableReference置換(x.FirstTableReference);
        x.SecondTableReference=this.TableReference置換(x.SecondTableReference);
    }
    protected override void QualifiedJoin(QualifiedJoin x){
        x.FirstTableReference=this.TableReference置換(x.FirstTableReference);
        x.SecondTableReference=this.TableReference置換(x.SecondTableReference);
        this.BooleanExpression(x.SearchCondition);
    }
    protected override void JoinParenthesisTableReference(JoinParenthesisTableReference x){
        x.Join=this.TableReference置換(x.Join);
    }
    protected override void OdbcQualifiedJoinTableReference(OdbcQualifiedJoinTableReference x){
        x.TableReference=this.TableReference置換(x.TableReference);
    }
    protected override void InsertSpecification(InsertSpecification x){
        this.ColumnReferenceExpressions(x.Columns);
        this.InsertSource(x.InsertSource);
        if(x.OutputClause is not null) this.OutputClause(x.OutputClause);
        if(x.OutputIntoClause is not null) this.OutputIntoClause(x.OutputIntoClause);
        x.Target=this.TableReference置換(x.Target);
        if(x.TopRowFilter is not null) this.TopRowFilter(x.TopRowFilter);
    }
    protected override void MergeSpecification(MergeSpecification x){
        foreach(var a in x.ActionClauses) this.MergeActionClause(a);
        if(x.OutputClause is not null) this.OutputClause(x.OutputClause);
        if(x.OutputIntoClause is not null) this.OutputIntoClause(x.OutputIntoClause);
        this.SwitchIdentifier(x.TableAlias);
        x.TableReference=this.TableReference置換(x.TableReference);
        x.Target=this.TableReference置換(x.Target);
        if(x.TopRowFilter is not null) this.TopRowFilter(x.TopRowFilter);
    }
    protected override void OutputIntoClause(OutputIntoClause x){
        x.IntoTable=this.TableReference置換(x.IntoTable);
        this.ColumnReferenceExpressions(x.IntoTableColumns);
        this.SelectElements(x.SelectColumns);
    }
}
