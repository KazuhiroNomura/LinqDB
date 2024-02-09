using System.Diagnostics;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
//using System.Collections.Generic;
using System.Xml.Linq;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    private(e.Expression Set,e.ParameterExpression Element)SchemaObjectFunctionTableReference(SchemaObjectFunctionTableReference x){
        //SELECT 
        //     jc.[JobCandidateID] 
        //    ,jc.[BusinessEntityID] 
        //    ,[Resume].ref.value(N'declare default element namespace "http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/Resume"; 
        //       (/Resume/Name/Name.Prefix)[1]','nvarchar(30)')AS [Name.Prefix] 
        //    ,[Resume].ref.value(N'declare default element namespace "http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/Resume";
        //       (/Resume/Name/Name.First)[1]','nvarchar(30)')AS [Name.First] 
        //FROM [HumanResources].[JobCandidate] jc 
        //CROSS APPLY 
        //    jc.[Resume].nodes(N'declare default element namespace "http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/Resume";/Resume')
        //AS[Resume](ref)
        //SchemaObject.BaseIdentifier.Value(x.Parameters[0])as Alias(x.Columns[0])
        //sys.fn_helpdatatypemapとか
        var SchemaObject=x.SchemaObject;
        string Key;
        if(SchemaObject.DatabaseIdentifier is null){
            if(SchemaObject.SchemaIdentifier is null){
                Debug.Assert(this.RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression is not null);
                throw new NotSupportedException(Properties.Resources.スキーマとテーブルが発見できなかった);
            }else{
                Key=SchemaObject.SchemaIdentifier.Value;
            }
        }else{
            if(SchemaObject.SchemaIdentifier is null){
                throw new NotSupportedException(Properties.Resources.スキーマとテーブルが発見できなかった);
            }else{
                Key=SchemaObject.DatabaseIdentifier.Value+'.'+SchemaObject.SchemaIdentifier.Value;
            }
        }
        //var Schema = this.List_Schema.SingleOrDefault(p => p.Member.Name==Key);
        var Schema = this.FindSchema(Key);
        //var Schema=this.ContainerType.GetProperty(Key);
        if(Schema is not null) {
            Debug.Assert(Schema.Expression!=null&&(e.ParameterExpression)Schema.Expression==this.Container);
            var Method =FindFunction(Schema.Type,SchemaObject.BaseIdentifier.Value);
            //var Method=Schema.Type.GetMethod(SchemaObject.BaseIdentifier.Value,BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly);
            if(Method is null) {
                Trace.WriteLine("function2:"+SchemaObject.BaseIdentifier.Value);
                //動的な型を使ってごまかす
                return (TABLE_DEE,e.Expression.Parameter(typeof(Databases.AttributeEmpty),x.Alias?.Value));
            }
            Debug.Assert(x.Columns.Count==0);
            var Parameters=x.Parameters;
            var Arguments_Length=Parameters.Count;
            var Method_GetParameters=Method.GetParameters();
            Debug.Assert(Method_GetParameters.Length==Arguments_Length);
            //var x_Alias_Value=x.Alias.Value;
            var Arguments=new e.Expression[Arguments_Length];
            for(var a = 0;a<Arguments_Length;a++) { 
                var Parameter0=Parameters[a];
                var Method_GetParameter=Method_GetParameters[a];
                if(Parameter0 is DefaultLiteral) {
                    if(Method_GetParameter.HasDefaultValue) {
                        Arguments[a]=e.Expression.Constant(Method_GetParameter.DefaultValue,Method_GetParameter.ParameterType);
                    } else { 
                        Arguments[a]=e.Expression.Default(Method_GetParameter.ParameterType);
                    }
                } else { 
                    Arguments[a]=this.Convertデータ型を合わせるNullableは想定する(this.ScalarExpression(Parameter0),Method_GetParameter.ParameterType);
                }
            }
            var Select_Expression=e.Expression.Call(Schema,Method,Arguments);
            var Parameter=e.Expression.Parameter(IEnumerable1のT(Select_Expression.Type),x.Alias?.Value);
            {
                var RefPeek=this.RefPeek;
                var Dictionary_TableAlias_ColumnAliases=RefPeek.Dictionary_TableAlias_ColumnAliases;
                var List_アスタリスクColumnAlias=RefPeek.List_アスタリスクColumnAlias;
                var List_アスタリスクColumnExpression=RefPeek.List_アスタリスクColumnExpression;
                var Dictionary_DatabaseSchemaTable_ColumnExpression=RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression;
                //string Table;
                var ContainerType=this.ContainerType;
                //const BindingFlags BindingFlags=BindingFlags.Instance|BindingFlags.Public|BindingFlags.FlattenHierarchy|BindingFlags.IgnoreCase|BindingFlags.GetProperty|BindingFlags.GetField;
                //PropertyInfo Schema_PropertyInfo;
                //PropertyInfo Table_Property;
                var Table_Type=Method.ReturnType;
                var T=IEnumerable1のT(Table_Type);
                var ctor_Parameters=T.GetConstructors()[0].GetParameters();
                var ctor_Parameters_Length=ctor_Parameters.Length;
                var ColumnAliases=new string[ctor_Parameters_Length];
                var x_Alias = x.Alias;
                string? Table = null, TableDot = null;
                if(x_Alias is not null) {
                    Table=x_Alias.Value;
                    TableDot = Table+'.';
                }
                //Table
                if(Table is not null)DictionaryにKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,Table,Parameter);
                for(var a=0;a<ctor_Parameters_Length;a++){
                    var ColumnAlias=ctor_Parameters[a].Name;
                    var Item=e.Expression.PropertyOrField(Parameter,ColumnAlias);
                    ColumnAliases[a]=ColumnAlias;
                    List_アスタリスクColumnAlias.Add(ColumnAlias);
                    List_アスタリスクColumnExpression.Add(Item);
                    ////Table.Column
                    //if(TableDot is not null)DictionaryにKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,TableDot+ColumnAlias,Item);
                    ////Column
                    //DictionaryにKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,ColumnAlias,Item);
                    DictionaryにDotKeyとKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,TableDot,ColumnAlias,Item);
                }
                DictionaryのValueがnullのKeyをRemove(Dictionary_DatabaseSchemaTable_ColumnExpression);
            }
            return(Select_Expression,Parameter);
        }else if(SchemaObject.BaseIdentifier.Value.Equals("nodes",StringComparison.OrdinalIgnoreCase)){ 
            ref var RefPeek=ref this.RefPeek;
            var RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression=RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression;
            var RefPeek_Dictionary_TableAlias_ColumnAliases=RefPeek.Dictionary_TableAlias_ColumnAliases;
            Debug.Assert(x.Parameters.Count==1);
            var x_Columns=x.Columns;
            Debug.Assert(x_Columns.Count==1);
            var Select_Expression=RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression[Key];
            Debug.Assert(Select_Expression is not null);
            var x_Columns_Count=x_Columns.Count;
            var x_Alias_Value=x.Alias.Value;
            var ColumnAliases=new string[x_Columns_Count];
            RefPeek_Dictionary_TableAlias_ColumnAliases.Add(x_Alias_Value,ColumnAliases);
            var ListアスタリスクColumnAliase=RefPeek.List_アスタリスクColumnAlias;
            var x_Alias_Value_Dot=x_Alias_Value+'.';
            for(var a=0;a<x_Columns_Count;a++){
                var ColumnAlias=x_Columns[a].Value;
                RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression.Add(x_Alias_Value_Dot+ColumnAlias,Select_Expression);
                //複数テーブルで同一列名があったときに無効にする
                if(RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression.ContainsKey(ColumnAlias)){
                    RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression[ColumnAlias]=null!;
                }else{
                    RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression.Add(ColumnAlias,Select_Expression);
                }
                ColumnAliases[a]=ColumnAlias;
                ListアスタリスクColumnAliase.Add(ColumnAlias);
            }
            DictionaryのValueがnullのKeyをRemove(RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression);
            Debug.Assert(Select_Expression.Type==typeof(XDocument));
            Select_Expression=e.Expression.Call(
                Product.SQLServer.Reflection.nodes,
                Select_Expression,
                this.ScalarExpression(x.Parameters[0])
            );
            var Parameter=e.Expression.Parameter(typeof(XElement),x.Alias.Value);
            return(Select_Expression,Parameter);
        }else{
            ref var RefPeek=ref this.RefPeek;
            var RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression=RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression;
            var RefPeek_Dictionary_TableAlias_ColumnAliases=RefPeek.Dictionary_TableAlias_ColumnAliases;
            Debug.Assert(x.Parameters.Count==1);
            var x_Columns=x.Columns;
            Debug.Assert(x_Columns.Count==1);
            var Select_Expression=RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression[Key];
            Debug.Assert(Select_Expression is not null);
            var x_Columns_Count=x_Columns.Count;
            var x_Alias_Value=x.Alias.Value;
            var ColumnAliases=new string[x_Columns_Count];
            RefPeek_Dictionary_TableAlias_ColumnAliases.Add(x_Alias_Value,ColumnAliases);

            //RefPeek_Dictionary_TableAlias_ColumnAliases.Add(x_Alias_Value+".*",ColumnAliases);
            //AddStartColumnAliases(RefPeek_Dictionary_TableAlias_ColumnAliases,ColumnAliases);
            var ListアスタリスクColumnAliase=RefPeek.List_アスタリスクColumnAlias;
            var x_Alias_Value_Dot=x_Alias_Value+'.';
            for(var a=0;a<x_Columns_Count;a++){
                //foreach(var Column in x_Columns){
                var ColumnAlias=x_Alias_Value_Dot+x_Columns[a].Value;
                RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression.Add(x_Alias_Value_Dot+ColumnAlias,Select_Expression);
                //複数テーブルで同一列名があったときに無効にする
                if(RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression.ContainsKey(ColumnAlias)){
                    RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression[ColumnAlias]=null!;
                }else{
                    RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression.Add(ColumnAlias,Select_Expression);
                }
                ColumnAliases[a]=ColumnAlias;
                ListアスタリスクColumnAliase.Add(ColumnAlias);
            }
            DictionaryのValueがnullのKeyをRemove(RefPeek_Dictionary_DatabaseSchemaTable_ColumnExpression);
            Type T;
            if(SchemaObject.BaseIdentifier.Value.Equals("nodes",StringComparison.OrdinalIgnoreCase)){
                Debug.Assert(Select_Expression.Type==typeof(XDocument));
                Select_Expression=e.Expression.Call(
                    Product.SQLServer.Reflection.nodes,
                    Select_Expression,
                    this.ScalarExpression(x.Parameters[0])
                );
                T=typeof(XElement);
            }else{
                T=IEnumerable1のT(Select_Expression.Type);
            }
            var Parameter=e.Expression.Parameter(T,x.Alias.Value);
            return(Select_Expression,Parameter);
        }
    }
    /// <summary>
    /// これを呼び出したら呼び出し元はRefPeekを参照して利用可能な列を取得できる
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private(e.Expression Set,e.ParameterExpression Element)QueryDerivedTable(QueryDerivedTable x){
        var StackSubquery単位の情報=this._StackSubquery単位の情報;
        StackSubquery単位の情報.Push();
        ref var RefPeek1=ref StackSubquery単位の情報.RefPeek;
        var RefPeek1_List_ColumnAlias=RefPeek1.List_ColumnAlias;
        var Result=this.QueryExpression(x.QueryExpression);
        var RefPeek1_List_ColumnAlias_Count=RefPeek1_List_ColumnAlias.Count;
        var x_Alias_Value=x.Alias.Value;
        Debug.Assert(x.Columns.Count==0,"0以外未発見");
        var ColumnAliases=new string[RefPeek1_List_ColumnAlias_Count];
        var TableAlias_Dot=x_Alias_Value+'.';
        StackSubquery単位の情報.Pop();
        ref var RefPeek0=ref StackSubquery単位の情報.RefPeek;
        var RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression=RefPeek0.Dictionary_DatabaseSchemaTable_ColumnExpression;
        var RefPeek0_Dictionary_TableAlias_ColumnAliases=RefPeek0.Dictionary_TableAlias_ColumnAliases;
        var RefPeek0_List_アスタリスクColumnAlias=RefPeek0.List_アスタリスクColumnAlias;
        var RefPeek0_List_アスタリスクExpression=RefPeek0.List_アスタリスクColumnExpression;
        RefPeek0_Dictionary_TableAlias_ColumnAliases.Add(x_Alias_Value!,ColumnAliases);
        var Result_Parameter=e.Expression.Parameter(Result.Type.GetGenericArguments()[0],x_Alias_Value);
        e.Expression ValueTuple=Result_Parameter;
        var Item番号=1;
        for(var a=0;a<RefPeek1_List_ColumnAlias_Count;a++){
            var ColumnAlias=RefPeek1_List_ColumnAlias[a];
            var Item=ValueTuple_Item(ref ValueTuple,ref Item番号);
            DictionaryにKeyがあればValueにnullを代入(RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression,ColumnAlias,Item);
            RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression.Add(TableAlias_Dot+ColumnAlias,Item);
            ColumnAliases[a]=ColumnAlias;
            RefPeek0_List_アスタリスクColumnAlias.Add(ColumnAlias);
            RefPeek0_List_アスタリスクExpression.Add(Item);
        }
        DictionaryのValueがnullのKeyをRemove(RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression);
        return(Result,Result_Parameter);
    }
    private(e.Expression Set,e.ParameterExpression Element)InlineDerivedTable(InlineDerivedTable x){
        throw this.単純NotSupportedException(x);
    }
    private(e.Expression Set,e.ParameterExpression Element)BulkOpenRowset(BulkOpenRowset x){
        throw this.単純NotSupportedException(x);
    }
    private(e.Expression Set,e.ParameterExpression Element)DataModificationTableReference(DataModificationTableReference x){
        throw this.単純NotSupportedException(x);
    }
    private(e.Expression Set,e.ParameterExpression Element)ChangeTableChangesTableReference(ChangeTableChangesTableReference x){
        throw this.単純NotSupportedException(x);
    }
    private(e.Expression Set,e.ParameterExpression Element)ChangeTableVersionTableReference(ChangeTableVersionTableReference x){
        throw this.単純NotSupportedException(x);
    }
    private(e.Expression Set,e.ParameterExpression Element)VariableMethodCallTableReference(VariableMethodCallTableReference x){
        throw this.単純NotSupportedException(x);
    }
}
