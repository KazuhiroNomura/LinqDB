using System.Linq;
using LinqDB.Sets;
using System.Diagnostics;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
//using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Reflection;
using System.Xml.Linq;
using e = System.Linq.Expressions;
using AssemblyName = Microsoft.SqlServer.TransactSql.ScriptDom.AssemblyName;
using System.Globalization;
using LinqDB.Optimizers.ReturnExpressionTraverser;
using LinqDB.Helpers;
using Expressions = System.Linq.Expressions;
using System.Collections.Generic;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
using static Common;
internal partial class 変換_TSqlFragmentからExpression{
    /// <summary>
    /// ごく普通のfromに書くテーブル名
    /// </summary>
    /// <param name="x"></param>
    /// <returns>(Expressions.Expression Expression(Table=>Table.Field),Expressions.ParameterExpression Parameter(Table=>))</returns>
    private(e.Expression Set,e.ParameterExpression ss)NamedTableReference(NamedTableReference x){
        ref var RefPeek=ref this.RefPeek;
        var Dictionary_DatabaseSchemaTable_ColumnExpression=RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression;
        var Dictionary_TableAlias_ColumnAliases=RefPeek.Dictionary_TableAlias_ColumnAliases;
        var List_アスタリスクColumnAlias=RefPeek.List_アスタリスクColumnAlias;
        var List_アスタリスクColumnExpression=RefPeek.List_アスタリスクColumnExpression;
        //var Dictionary_With名_Set_ColumnAliases=this.Dictionary_With名_Set_ColumnAliases;
        var SchemaObject=x.SchemaObject;
        Debug.Assert(SchemaObject is not null);
        var Table=SchemaObject.BaseIdentifier.Value;
        if(this.Dictionary_With名_Set_ColumnAliases.TryGetValue(Table,out var Set_ColumnAliases)) {
            //var Set_Type=Set.Type;
            var Element=e.Expression.Parameter(IEnumerable1のT(Set_ColumnAliases.Set.Type),Table);
            var x_Alias=x.Alias;
            if(x_Alias is not null) Table=x_Alias.Value;
            var TableDot=Table+'.';
            e.Expression ValueTuple=Element;
            var Item番号=1;
            foreach(var ColumnAlias in Set_ColumnAliases.ColumnAliases){
                var Item=ValueTuple_Item(ref ValueTuple,ref Item番号);
                List_アスタリスクColumnAlias.Add(ColumnAlias);
                List_アスタリスクColumnExpression.Add(Item);
                ////Table.Column
                //DictionaryにKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,TableDot+ColumnAlias,Item);
                ////Column
                //DictionaryにKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,         ColumnAlias,Item);
                DictionaryにKey0とKey1があればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,TableDot+ColumnAlias,ColumnAlias,Item);
            }
            return (Set_ColumnAliases.Set,Element);
        } else {
            var ContainerType=this.ContainerType;
            Debug.Assert(ContainerType!=null);
            const BindingFlags BindingFlags=BindingFlags.Instance|BindingFlags.Public|BindingFlags.DeclaredOnly|BindingFlags.IgnoreCase|BindingFlags.GetProperty;
            PropertyInfo Schema_PropertyInfo;
            PropertyInfo Table_Property;
            Type Table_Type;
            if(SchemaObject.SchemaIdentifier is null){
                foreach(var Schema in ContainerType.GetProperties()){
                    foreach(var TableViewFunction in Schema.PropertyType.GetProperties(BindingFlags)){
                        if(string.Equals(TableViewFunction.Name,Table,StringComparison.OrdinalIgnoreCase)){
                            Table_Property=TableViewFunction;
                            Schema_PropertyInfo=Schema;
                            Table_Type=Table_Property.PropertyType;
                            goto 発見;
                        }
                    }
                }
                throw new NotSupportedException($"{Table}が発見できなかった");
            }else{
                //Schema_PropertyInfo=ContainerType.GetProperty(SchemaObject.SchemaIdentifier.Value,BindingFlags)!;
                var Schema= SchemaObject.SchemaIdentifier.Value;
                var Schema_PropertyInfo0=ContainerType.GetProperties(BindingFlags).Where(p => string.Equals(p.Name,Schema,StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
                if(Schema_PropertyInfo0 is null){
                    throw new NotImplementedException($"{Schema}スキーマは定義されていなかった");
                }
                //Table_Property=Schema_PropertyInfo.PropertyType.GetProperty(Table,BindingFlags)!;
                Schema_PropertyInfo=Schema_PropertyInfo0;
                var Table_Property0=Schema_PropertyInfo.PropertyType.GetProperties(BindingFlags).Where(p=>string.Equals(p.Name,Table,StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
                if(Table_Property0 is null){
                    //Trace.WriteLine(Table);
                    //var Table_Type0=typeof(Set<int>);
                    ////var T=IEnumerable1のT(Table_Type);
                    //var Set0=e.Expression.New(
                    //    Table_Type0.GetConstructor(Type.EmptyTypes)
                    //);
                    //var Element0=e.Expression.Parameter(typeof(int),Table);
                    //Trace.WriteLine(Schema);
                    //return(Set0,Element0);
                    throw new KeyNotFoundException($"{Table}テーブルプロパティが定義されていなかった");
                }
                Table_Property=Table_Property0;
                //if (x.Value=="objects$")
                //{
                //   // x.Value="(SELECT x.*,s.null_on_null_input from sys.all_objects x JOIN sys.system_sql_modules s on x.object_id=s.object_id)";
                //}
                //Debug.Assert(Table_Property is not null,$"{Table}テーブルプロパティが定義されていなかった");
                Table_Type=Table_Property.PropertyType;
            }
発見:
            var T=IEnumerable1のT(Table_Type);
            var ctor_Parameters=T.GetConstructors()[0].GetParameters();
            var ctor_Parameters_Length=ctor_Parameters.Length;
            var ColumnAliases=new string[ctor_Parameters_Length];
            var x_Alias=x.Alias;
            var Database=T.FullName!.Split('.')[0];
            string Database_Schema_Table_Dot,Schema_Table_Dot;
            if(x_Alias is null){
                Table=Table_Property.Name;
                var Schema_Table=Schema_PropertyInfo.Name+'.'+Table;
                Schema_Table_Dot=Schema_Table+'.';
                var Database_Schema_Table=Database+'.'+Schema_Table;
                Database_Schema_Table_Dot=Database_Schema_Table+'.';
                Dictionary_TableAlias_ColumnAliases.Add(         Schema_Table,ColumnAliases);
                Dictionary_TableAlias_ColumnAliases.Add(Database_Schema_Table,ColumnAliases);
            }else{
                Table=x_Alias.Value;
                Debug.Assert(x_Alias.Value==Table);
                Schema_Table_Dot="";
                Database_Schema_Table_Dot="";
            }
            Dictionary_TableAlias_ColumnAliases.Add(Table,ColumnAliases);
            Debug.Assert(ContainerType is not null);
            var Table_Dot=Table+'.';
            var Set=e.Expression.MakeMemberAccess(
                e.Expression.Property(this.Container,Schema_PropertyInfo),
                Table_Property
            );
            var Element=e.Expression.Parameter(T,Table);
            if(x_Alias is null){
                Table=Table_Property.Name;
                var Schema_Table=Schema_PropertyInfo.Name+'.'+Table;
                var Database_Schema_Table=Database+'.'+Schema_Table;
                ////Database.Schema.Table
                //DictionaryにKeyがあればValueにnullを代入    (Dictionary_DatabaseSchemaTable_ColumnExpression,Database_Schema_Table,Element);
                ////Schema.Table
                //DictionaryにKeyがあればValueにnullを代入    (Dictionary_DatabaseSchemaTable_ColumnExpression,         Schema_Table,Element);
                DictionaryにKey0とKey1があればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,Database_Schema_Table,Schema_Table,Element);
            }
            //Table
            DictionaryにKeyがあればValueにnullを代入        (Dictionary_DatabaseSchemaTable_ColumnExpression,                Table,Element);
            for(var a=0;a<ctor_Parameters_Length;a++){
                var ColumnAlias=ctor_Parameters[a].Name;
                var Item=e.Expression.PropertyOrField(Element,ColumnAlias);
                ColumnAliases[a]=ColumnAlias;
                List_アスタリスクColumnAlias.Add(ColumnAlias);
                List_アスタリスクColumnExpression.Add(Item);
                if(x_Alias is null){
                    ////Database.Schema.Table.Column
                    //DictionaryにKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,Database_Schema_Table_Dot+ColumnAlias,Item);
                    ////Schema.Table.Column
                    //DictionaryにKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,         Schema_Table_Dot+ColumnAlias,Item);
                    DictionaryにKey0とKey1があればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,Database_Schema_Table_Dot+ColumnAlias,Schema_Table_Dot+ColumnAlias,Item);
                }
                ////Table.Column
                //DictionaryにKeyがあればValueにnullを代入    (Dictionary_DatabaseSchemaTable_ColumnExpression,                Table_Dot+ColumnAlias,Item);
                ////Column
                //DictionaryにKeyがあればValueにnullを代入    (Dictionary_DatabaseSchemaTable_ColumnExpression,                          ColumnAlias,Item);
                DictionaryにKey0とKey1があればValueにnullを代入    (Dictionary_DatabaseSchemaTable_ColumnExpression,Table_Dot+ColumnAlias,ColumnAlias,Item);
            }
            DictionaryのValueがnullのKeyをRemove            (Dictionary_DatabaseSchemaTable_ColumnExpression);
            //if(x_Alias is null){
            //    Table=Table_Property.Name;
            //    var Schema_Table=Schema_PropertyInfo.Name+'.'+Table;
            //    Schema_Table_Dot=Schema_Table+'.';
            //    var Database_Schema_Table=Database+'.'+Schema_Table;
            //    Database_Schema_Table_Dot=Database_Schema_Table+'.';
            //    Dictionary_TableAlias_ColumnAliases.Remove(         Schema_Table);
            //    Dictionary_TableAlias_ColumnAliases.Remove(Database_Schema_Table);
            //}
            //Dictionary_TableAlias_ColumnAliases.Remove(Table);
            return(Set,Element);
        }
    }
    private e.Expression OdbcQualifiedJoinTableReference(OdbcQualifiedJoinTableReference x){throw this.単純NotSupportedException(x);}
    private(e.Expression Set,e.ParameterExpression Element)TableReferenceWithAliasAndColumns(TableReferenceWithAliasAndColumns x)=>x switch{
        SchemaObjectFunctionTableReference y=>this.SchemaObjectFunctionTableReference(y),
        QueryDerivedTable y=>this.QueryDerivedTable(y),
        InlineDerivedTable y=>this.InlineDerivedTable(y),
        BulkOpenRowset y=>this.BulkOpenRowset(y),
        DataModificationTableReference y=>this.DataModificationTableReference(y),
        ChangeTableChangesTableReference y=>this.ChangeTableChangesTableReference(y),
        ChangeTableVersionTableReference y=>this.ChangeTableVersionTableReference(y),
        VariableMethodCallTableReference y=>this.VariableMethodCallTableReference(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private(e.Expression Set,e.ParameterExpression Element)FullTextTableReference(FullTextTableReference x){
        return (Default_void,e.Expression.Parameter(typeof(int)));
        //throw this.単純NotSupportedException(x);
    }
    private e.Expression SemanticTableReference(SemanticTableReference x){throw this.単純NotSupportedException(x);}
    private e.Expression OpenXmlTableReference(OpenXmlTableReference x){throw this.単純NotSupportedException(x);}
    private e.Expression OpenJsonTableReference(OpenJsonTableReference x){throw this.単純NotSupportedException(x);}
    private e.Expression InternalOpenRowset(InternalOpenRowset x){throw this.単純NotSupportedException(x);}
    private e.Expression OpenQueryTableReference(OpenQueryTableReference x){throw this.単純NotSupportedException(x);}
    private(e.Expression Set,e.ParameterExpression Element)AdHocTableReference(AdHocTableReference x){
        throw this.単純NotSupportedException(x);
    }
    private(e.Expression Set,e.ParameterExpression Element)BuiltInFunctionTableReference(BuiltInFunctionTableReference x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression GlobalFunctionTableReference(GlobalFunctionTableReference x){throw this.単純NotSupportedException(x);}
    private partial (Expressions.Expression Set, Expressions.ParameterExpression Element) PivotedTableReference(PivotedTableReference x) {
        //Alias                       A
        //AggregateFunctionIdentifier SUM
        //PivotColumn                 FOR ss.B
        //TableReference              FROMの後
        //InColumns                   0,1,2,3
        //ValueColumns                ss.C
        //SELECT
        //A._0,
        //A._1,
        //A._2,
        //A._3
        //FROM Keyあり ss PIVOT(
        //    SUM(ss.C)
        //    FOR ss.B
        //    IN(_0,_1,_2,_3)
        //)A
        //SELECT ss.A,ss.D,
        //_0=(SELECT SUM(ss.C)FROM Keyあり ss WHERE ss.B=0),
        //_1=(SELECT SUM(ss.C)FROM Keyあり ss WHERE ss.B=1),
        //_2=(SELECT SUM(ss.C)FROM Keyあり ss WHERE ss.B=2),
        //_3=(SELECT SUM(ss.C)FROM Keyあり ss WHERE ss.B=3)
        //FROM Keyあり ss GROUP BY ss.A,ss.D
        //大事なことは"集約対称列以外すべてでグループ化する"
        //Keyあり.GroupBy(
        //    ss=>ss.A,ss.D
        //    (Key,Group)=>new{
        //        Key.Item1,Key.Item2
        //        _0=Group.Where(ss=>ss.B==0).Sum(ss=>ss.C)
        //        _1=Group.Where(ss=>ss.B==1).Sum(ss=>ss.C)
        //        _2=Group.Where(ss=>ss.B==2).Sum(ss=>ss.C)
        //        _3=Group.Where(ss=>ss.B==3).Sum(ss=>ss.C)
        //    }
        //)
        Debug.Assert(x.ValueColumns.Count==1);
        var StackSubquery単位の情報 = this._StackSubquery単位の情報;
        ref var RefPeek0 = ref StackSubquery単位の情報.RefPeek;
        var RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression = RefPeek0.Dictionary_DatabaseSchemaTable_ColumnExpression;
        var RefPeek0_Dictionary_TableAlias_ColumnAliases = RefPeek0.Dictionary_TableAlias_ColumnAliases;
        var RefPeek0_List_アスタリスクColumnAlias = RefPeek0.List_アスタリスクColumnAlias;
        var RefPeek0_List_アスタリスクColumnExpression = RefPeek0.List_アスタリスクColumnExpression;
        StackSubquery単位の情報.Push();
        ref var RefPeek1 = ref StackSubquery単位の情報.RefPeek;
        var RefPeek1_Dictionary_DatabaseSchemaTable_ColumnExpression = RefPeek1.Dictionary_DatabaseSchemaTable_ColumnExpression;
        var RefPeek1_List_アスタリスクColumnAlias = RefPeek1.List_アスタリスクColumnAlias;
        var RefPeek1_List_アスタリスクColumnExpression = RefPeek1.List_アスタリスクColumnExpression;
        var (Source, ss)=this.TableReference(x.TableReference);
        var x_ValueColumns_0 = x.ValueColumns[0];
        Debug.Assert(IEnumerable1のT(Source.Type)==ss.Type);
        var Element_Type = ss.Type;
        //var x_PivotColumn_MultiPartIdentifier =x_PivotColumn.MultiPartIdentifier;
        var x_PivotColumn = x.PivotColumn;
        var PivotColumnAlias =this.SQL取得(x_PivotColumn);
        //if(x_PivotColumn_MultiPartIdentifier.Count==3) {
        //    PivotColumnAlias=x_PivotColumn_MultiPartIdentifier[2].Value;
        //} else if(x_PivotColumn_MultiPartIdentifier.Count==2) {
        //    PivotColumnAlias=x_PivotColumn_MultiPartIdentifier[1].Value;
        //} else {
        //    PivotColumnAlias=x_PivotColumn_MultiPartIdentifier[0].Value;
        //}
        var x_Alias_Value = x.Alias.Value;
        var keySelector_Expressions_Length = RefPeek1_List_アスタリスクColumnExpression.Count-1;
        var keySelector_Expressions = new Expressions.Expression[keySelector_Expressions_Length];
        var keySelector_ColumnAlias= new string[keySelector_Expressions_Length];
        var ExpressionEqualityComparer = this.ExpressionEqualityComparer;
        var PivotColumnExpression = RefPeek1_Dictionary_DatabaseSchemaTable_ColumnExpression[PivotColumnAlias];
        Debug.Assert(PivotColumnExpression is not null);
        var ValueColumnAlias=this.SQL取得(x_ValueColumns_0);
        var ValueColumnExpression = RefPeek1_Dictionary_DatabaseSchemaTable_ColumnExpression[ValueColumnAlias];
        {
            keySelector_ColumnAlias[0]=PivotColumnAlias;
            keySelector_Expressions[0]=PivotColumnExpression;
            var index = 1;
            for(var a=0;a<RefPeek1_List_アスタリスクColumnExpression.Count;a++) {
                var ColumnExpression = RefPeek1_List_アスタリスクColumnExpression[a];
                if(ExpressionEqualityComparer.Equals(ValueColumnExpression,ColumnExpression)||ExpressionEqualityComparer.Equals(PivotColumnExpression,ColumnExpression)) continue;
                keySelector_ColumnAlias[index]=RefPeek1_List_アスタリスクColumnAlias[a];
                keySelector_Expressions[index]=ColumnExpression;
                index++;
            }
        }
        var 作業配列 = this.作業配列;
        var keySelector_Body = CommonLibrary.ValueTupleでNewする(作業配列,keySelector_Expressions);
        var Key = Expressions.Expression.Parameter(keySelector_Body.Type,"Key");
        var Group = Expressions.Expression.Parameter(作業配列.MakeGenericType(typeof(Sets.IEnumerable<>),Element_Type),"Group");
        var x_InColumns=x.InColumns;
        {
            var Item番号 = 1;
            Expressions.Expression ValueTuple = Key;
            for(var a = 0;a<keySelector_Expressions_Length;a++) {
                var keySelector_Expression=keySelector_Expressions[a];
                var Item = ValueTuple_Item(ref ValueTuple,ref Item番号);
                if(ExpressionEqualityComparer.Equals(PivotColumnExpression,keySelector_Expression)) continue;
                if(ExpressionEqualityComparer.Equals(ValueColumnExpression,keySelector_Expression)) continue;
                var ColumnAlias = keySelector_ColumnAlias[a];
                RefPeek0_List_アスタリスクColumnAlias.Add(ColumnAlias);
                RefPeek0_List_アスタリスクColumnExpression.Add(Item);
            }
        }
        var Key_Item1=Expressions.Expression.Field(Key,nameof(ValueTuple<int>.Item1));
        var WhereMethod=作業配列.MakeGenericMethod(Reflection.ExtensionSet.Where,Element_Type);
        var Where_Parameters = new []{ss};
        RefPeek1.集約関数のParameter=ss;
        var FunctionName = x.AggregateFunctionIdentifier.Identifiers[0].Value.ToUpperInvariant();
        foreach(var x_InColumn in x_InColumns) {
            RefPeek0_List_アスタリスクColumnAlias.Add(x_InColumn.Value);
            var Right=Stringをキャスト(Key_Item1,x_InColumn.Value);
            var Where = Expressions.Expression.Call(
                WhereMethod,
                Group,
                Expressions.Expression.Lambda(
                    Expressions.Expression.Equal(Key_Item1,Right),
                    Where_Parameters
                )
            );
            RefPeek1.集約関数のSource=Where;
            Debug.Assert(RefPeek1.集約関数のParameter==ss);
            var Sum_Lambd_Body = this.AggregateFunction(FunctionName,x_ValueColumns_0);
            RefPeek0_List_アスタリスクColumnExpression.Add(Sum_Lambd_Body);
        }
        var resultSelector_Body = CommonLibrary.ValueTupleでNewする(作業配列,RefPeek0_List_アスタリスクColumnExpression);
        var Result = Expressions.Expression.Call(
            作業配列.MakeGenericMethod(
                Reflection.ExtensionSet.GroupBy_keySelector_resultSelector,
                Element_Type,keySelector_Body.Type,resultSelector_Body.Type
            ),
            Source,
            Expressions.Expression.Lambda(keySelector_Body,作業配列.Parameters設定(ss)),
            Expressions.Expression.Lambda(resultSelector_Body,作業配列.Parameters設定(Key,Group))
        );
        var Result_Parameter = Expressions.Expression.Parameter(resultSelector_Body.Type,x_Alias_Value);
        StackSubquery単位の情報.Pop();
        {
            RefPeek0_Dictionary_TableAlias_ColumnAliases.Clear();
            Expressions.Expression ValueTuple = Result_Parameter;
            RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression.Clear();
            var RefPeek0_List_ColumnAlias_Count = RefPeek0_List_アスタリスクColumnAlias.Count;
            var ColumnAliases = new string[RefPeek0_List_ColumnAlias_Count];
            RefPeek0_Dictionary_TableAlias_ColumnAliases.Add(x_Alias_Value,ColumnAliases);
            var Item番号 = 1;
            for(var a = 0;a<RefPeek0_List_ColumnAlias_Count;a++) {
                var ColumnAlias= RefPeek0_List_アスタリスクColumnAlias[a];
                ColumnAliases[a]=ColumnAlias;
                var Item = ValueTuple_Item(ref ValueTuple,ref Item番号);
                RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression.Add(ColumnAlias,Item);                  //Column
                RefPeek0_Dictionary_DatabaseSchemaTable_ColumnExpression.Add(x_Alias_Value+'.'+ColumnAlias,Item);//Table.Column
                RefPeek0_List_アスタリスクColumnExpression[a]=Item;
            }
        }
        return (Result,Result_Parameter);
    }
    private static Expressions.Expression ValueTuple_Item(ref Expressions.Expression ValueTuple,ref int Item番号) {
        if(Item番号==8) {
            ValueTuple=Expressions.Expression.Field(ValueTuple,"Rest");
            Item番号=1;
        }
        return Expressions.Expression.Field(ValueTuple,$"Item{Item番号++}");
    }
    private e.Expression? AggregateFunction(string FunctionName){
        ref var RefPeek = ref this.RefPeek;
        var 集約関数のParameter = RefPeek.集約関数のParameter!;
        var 集約関数のSource = RefPeek.集約関数のSource!;
        RefPeek.集約関数のParameter=null;
        //Debug.Assert(集約関数のParameter is not null&&集約関数のSource is not null);
        return FunctionName switch{
            "XACT_STATE"=>Constant_0,
            "ERROR_NUMBER"=>e.Expression.Constant(1),
            "ERROR_SEVERITY"=>e.Expression.Constant(2),
            "ERROR_STATE"=>e.Expression.Constant(3),
            "ERROR_PROCEDURE"=>e.Expression.Constant("ERROR_PROCEDURE"),
            "ERROR_LINE"=>e.Expression.Constant(4),
            "ERROR_MESSAGE"=>e.Expression.Constant("ERROR_MESSAGE"),
            _=>throw new KeyNotFoundException($"{FunctionName}の実装がない")
        };
    }
    private e.Expression? AggregateFunction(string FunctionName,ScalarExpression arg0){
        ref var RefPeek = ref this.RefPeek;
        var 集約関数のParameter = RefPeek.集約関数のParameter!;
        var 集約関数のSource = RefPeek.集約関数のSource!;
        RefPeek.集約関数のParameter=null;
        //Debug.Assert(集約関数のParameter is not null&&集約関数のSource is not null);
        switch(FunctionName) {
            case "AVG": {
                var Body = 共通();
                var Body_Type =Int32に変換して(ref Body);
                if     (typeof(int?    )==Body_Type)return 共通Nullable(Body,Reflection.ExtensionSet.AverageNullableInt32_selector  );
                else if(typeof(long?   )==Body_Type)return 共通Nullable(Body,Reflection.ExtensionSet.AverageNullableInt64_selector  );
                else if(typeof(float?  )==Body_Type)return 共通Nullable(Body,Reflection.ExtensionSet.AverageNullableSingle_selector );
                else if(typeof(double? )==Body_Type)return 共通Nullable(Body,Reflection.ExtensionSet.AverageNullableDouble_selector );
                else if(typeof(decimal?)==Body_Type)return 共通Nullable(Body,Reflection.ExtensionSet.AverageNullableDecimal_selector);
                else throw new NotSupportedException(Body_Type.FullName);
                Expressions.Expression 共通Nullable(Expressions.Expression Body,MethodInfo Method) {
                    var 作業配列=this.作業配列;
                    Method=作業配列.MakeGenericMethod(Method,集約関数のParameter.Type);
                    var Lambda = Expressions.Expression.Lambda(Body,作業配列.Parameters設定(集約関数のParameter));
                    return Expressions.Expression.Call(Method,集約関数のSource,Lambda);
                }
            }
            case "SUM": {
                var Body = 共通();
                MethodInfo Method;
                var Body_Type =Int32に変換して(ref Body);
                if     (Body_Type==typeof(int     ))Method=Reflection.ExtensionSet.SumInt32_selector;
                else if(Body_Type==typeof(long    ))Method=Reflection.ExtensionSet.SumInt64_selector;
                else if(Body_Type==typeof(float   ))Method=Reflection.ExtensionSet.SumSingle_selector;
                else if(Body_Type==typeof(double  ))Method=Reflection.ExtensionSet.SumDouble_selector;
                else if(Body_Type==typeof(decimal ))Method=Reflection.ExtensionSet.SumDecimal_selector;
                else if(Body_Type==typeof(int?    ))Method=Reflection.ExtensionSet.SumNullableInt32_selector;
                else if(Body_Type==typeof(long?   ))Method=Reflection.ExtensionSet.SumNullableInt64_selector;
                else if(Body_Type==typeof(float?  ))Method=Reflection.ExtensionSet.SumNullableSingle_selector;
                else if(Body_Type==typeof(double? ))Method=Reflection.ExtensionSet.SumNullableDouble_selector;
                else if(Body_Type==typeof(decimal?))Method=Reflection.ExtensionSet.SumNullableDecimal_selector;
                else throw new NotSupportedException(Body_Type.FullName);
                var 作業配列=this.作業配列;
                return Expressions.Expression.Call(
                    作業配列.MakeGenericMethod(Method,集約関数のParameter.Type),
                    集約関数のSource,
                    Expressions.Expression.Lambda(Body,作業配列.Parameters設定(集約関数のParameter))
                );
            }
            case "COUNT":return Expressions.Expression.Convert(
                Expressions.Expression.Call(
                    this.作業配列.MakeGenericMethod(Reflection.ExtensionSet.Count,IEnumerable1のT(集約関数のSource.Type)),
                    集約関数のSource
                ),
                typeof(int?)
            );
            case "MAX":
            case "MIN":{
                var Body = 共通();
                var Body_Type =Int32に変換して(ref Body);
                if(Body_Type==typeof(int     ))return 共通非Nullable用(Body,FunctionName=="min"?Reflection.ExtensionSet.MinInt32_selector          :Reflection.ExtensionSet.MaxInt32_selector          );
                if(Body_Type==typeof(long    ))return 共通非Nullable用(Body,FunctionName=="min"?Reflection.ExtensionSet.MinInt64_selector          :Reflection.ExtensionSet.MaxInt64_selector          );
                if(Body_Type==typeof(float   ))return 共通非Nullable用(Body,FunctionName=="min"?Reflection.ExtensionSet.MinSingle_selector         :Reflection.ExtensionSet.MaxSingle_selector         );
                if(Body_Type==typeof(double  ))return 共通非Nullable用(Body,FunctionName=="min"?Reflection.ExtensionSet.MinDouble_selector         :Reflection.ExtensionSet.MaxDouble_selector         );
                if(Body_Type==typeof(decimal ))return 共通非Nullable用(Body,FunctionName=="min"?Reflection.ExtensionSet.MinDecimal_selector        :Reflection.ExtensionSet.MaxDecimal_selector        );
                if(Body_Type==typeof(int?    ))return 共通Nullable用  (Body,FunctionName=="min"?Reflection.ExtensionSet.MinNullableInt32_selector  :Reflection.ExtensionSet.MaxNullableInt32_selector  );
                if(Body_Type==typeof(long?   ))return 共通Nullable用  (Body,FunctionName=="min"?Reflection.ExtensionSet.MinNullableInt64_selector  :Reflection.ExtensionSet.MaxNullableInt64_selector  );
                if(Body_Type==typeof(float?  ))return 共通Nullable用  (Body,FunctionName=="min"?Reflection.ExtensionSet.MinNullableSingle_selector :Reflection.ExtensionSet.MaxNullableSingle_selector );
                if(Body_Type==typeof(double? ))return 共通Nullable用  (Body,FunctionName=="min"?Reflection.ExtensionSet.MinNullableDouble_selector :Reflection.ExtensionSet.MaxNullableDouble_selector );
                if(Body_Type==typeof(decimal?))return 共通Nullable用  (Body,FunctionName=="min"?Reflection.ExtensionSet.MinNullableDecimal_selector:Reflection.ExtensionSet.MaxNullableDecimal_selector);
                if(Body_Type==typeof(object  )){
                    var Method=FunctionName=="min"?Reflection.ExtensionSet.MinTSource_selector        :Reflection.ExtensionSet.MaxTSource_selector;
                    var 作業配列=this.作業配列;
                    Method= 作業配列.MakeGenericMethod(Method,集約関数のParameter.Type,typeof(object));
                    var Lambda = Expressions.Expression.Lambda(Body,作業配列.Parameters設定(集約関数のParameter));
                    return Expressions.Expression.Call(Method,集約関数のSource,Lambda);
                }
                else throw new NotSupportedException(Body_Type.FullName);
                Expressions.Expression 共通非Nullable用(Expressions.Expression Body0,MethodInfo Method)=>this.ConvertNullable(共通Nullable用(Body0,Method));
                Expressions.Expression 共通Nullable用(Expressions.Expression Body0,MethodInfo Method){
                    var 作業配列=this.作業配列;
                    Method= 作業配列.MakeGenericMethod(Method,集約関数のParameter.Type);
                    var Lambda = Expressions.Expression.Lambda(Body0,作業配列.Parameters設定(集約関数のParameter));
                    return Expressions.Expression.Call(Method,集約関数のSource,Lambda);
                }
            }
            default:return null;
                //Expressions.Expression.Dynamic(
                //    Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(
                //        CSharpBinderFlags.None,FunctionName,)
                //    Binder.Binder.
                //    変換_メソッド正規化_取得インライン不可能定数.DynamicReflection.)
                //throw new NotSupportedException(FunctionName);
            static Type Int32に変換して(ref Expressions.Expression Body) {
                var Body_Type=Body.Type;
                if     (Body_Type==typeof(byte  ))Body=Expressions.Expression.Convert(Body,typeof(int ));
                else if(Body_Type==typeof(short ))Body=Expressions.Expression.Convert(Body,typeof(int ));
                else if(Body_Type==typeof(byte? ))Body=Expressions.Expression.Convert(Body,typeof(int?));
                else if(Body_Type==typeof(short?))Body=Expressions.Expression.Convert(Body,typeof(int?));
                return Body.Type;
            }
            Expressions.Expression 共通() {
                ref var RefPeek = ref this.RefPeek;
                RefPeek.集約関数のParameter = null;
                RefPeek.集約関数の内部か = true;
                var Body = this.ScalarExpression(arg0);
                RefPeek.集約関数の内部か = false;
                RefPeek.集約関数のParameter = 集約関数のParameter;
                return Body;
            }
        }
    }
    private e.Expression UnpivotedTableReference(UnpivotedTableReference x){throw this.単純NotSupportedException(x);}
    private(e.Expression Set,e.ParameterExpression Element)VariableTableReference(VariableTableReference x){
        ref var RefPeek = ref this.RefPeek;
        var Dictionary_DatabaseSchemaTable_ColumnExpression = RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression;
        var Dictionary_TableAlias_ColumnAliases = RefPeek.Dictionary_TableAlias_ColumnAliases;
        var List_アスタリスクColumnAlias = RefPeek.List_アスタリスクColumnAlias;
        var List_アスタリスクColumnExpression = RefPeek.List_アスタリスクColumnExpression;
        //var x_Variable_Name=x.Variable.Name;
        //var ctor_Parameters = T.GetConstructors()[0].GetParameters();
        //var ctor_Parameters_Length = ctor_Parameters.Length;
        string[]ColumnAliases;
        var Set = PrivateFindVariable(this.List_定義型TableVariable,x.Variable.Name);
        //var T = IEnumerable1のT(Set!.Type);
        //var Element = e.Expression.Parameter(T,x.Variable.Name);
        //return (Set, Element);
        var x_Alias = x.Alias;
        string? Table = null, Table_Dot = null;
        if(x_Alias is not null) {
            Table=x_Alias.Value;
            Table_Dot = Table+'.';
        }
        var x_Variable_Name = x.Variable.Name;
        Debug.Assert(x_Variable_Name[0]=='@');
        if(Set is not null) {
            var T = IEnumerable1のT(Set.Type);
            var Element = e.Expression.Parameter(T,x.Variable.Name);
            var ctor_Parameters = T.GetConstructors()[0].GetParameters();
            var ctor_Parameters_Length = ctor_Parameters.Length;
            ColumnAliases = new string[ctor_Parameters_Length];
            if(Table is not null)Dictionary_TableAlias_ColumnAliases.Add(Table,ColumnAliases);
            for(var a = 0;a<ctor_Parameters_Length;a++) {
                var ColumnAlias = ctor_Parameters[a].Name;
                var Item = e.Expression.PropertyOrField(Element,ColumnAlias);
                ColumnAliases[a]=ColumnAlias;
                List_アスタリスクColumnAlias.Add(ColumnAlias);
                List_アスタリスクColumnExpression.Add(Item);
                ////Table.Column
                //if(Table_Dot is not null)DictionaryにKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,Table_Dot+ColumnAlias,Item);
                ////Column
                //DictionaryにKeyがあればValueにnullを代入                         (Dictionary_DatabaseSchemaTable_ColumnExpression,          ColumnAlias,Item);
                DictionaryにDotKeyとKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,Table_Dot,ColumnAlias,Item);
            }
            DictionaryのValueがnullのKeyをRemove(Dictionary_DatabaseSchemaTable_ColumnExpression);
            return (Set, Element);
        } else {
            foreach(var 匿名型TableVariable in this.List_匿名型TableVariable) {
                if(匿名型TableVariable.Variable.Name!=x_Variable_Name)continue;
                ColumnAliases=匿名型TableVariable.Names;
                //var Element=e.Expression.Parameter(IEnumerable1のT(Set.Type),Name);
                var Table_Type = 匿名型TableVariable.Variable.Type;
                var T = IEnumerable1のT(Table_Type);
                var Element = e.Expression.Parameter(T,x_Variable_Name);
                if(Table is not null)Dictionary_TableAlias_ColumnAliases.Add(Table,ColumnAliases);
                Debug.Assert(x_Variable_Name[0]=='@');
                e.Expression ValueTuple = Element;
                var Item番号 = 1;
                for(var a = 0;a<ColumnAliases.Length;a++) {
                    var ColumnAlias = ColumnAliases[a];
                    var Item = ValueTuple_Item(ref ValueTuple,ref Item番号);
                    List_アスタリスクColumnAlias.Add(ColumnAlias);
                    List_アスタリスクColumnExpression.Add(Item);
                    ////Table.Column
                    //if(Table_Dot is not null)DictionaryにKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,Table_Dot+ColumnAlias,Item);
                    ////Column
                    //DictionaryにKeyがあればValueにnullを代入                         (Dictionary_DatabaseSchemaTable_ColumnExpression,          ColumnAlias,Item);
                    DictionaryにDotKeyとKeyがあればValueにnullを代入(Dictionary_DatabaseSchemaTable_ColumnExpression,Table_Dot,ColumnAlias,Item);
                }
                DictionaryのValueがnullのKeyをRemove(Dictionary_DatabaseSchemaTable_ColumnExpression);
                return(匿名型TableVariable.Variable,Element);
            }
            throw new KeyNotFoundException($"{x_Variable_Name}が見つからなかった。");
        }
    }









}
