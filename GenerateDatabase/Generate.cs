using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using LinqDB.Databases.Dom;
using LinqDB.Helpers;
using System.Diagnostics;
using System.Linq;
using LinqDB.Databases;
using VM;
public static class Generate {
    private const string ホスト名= @"COFFEELAKE\MSSQLSERVER2022";
    private const string Windowsログイン = @"Integrated Security=SSPI;";
    private const string SQLServerログイン = @"User ID=sa;Password=SQLSERVER711409;";
    private const string SQLServer接続文字列 = @$"Data Source={ホスト名};Initial Catalog=master;Integrated Security=false;{SQLServerログイン}";

    private const string MySQL接続文字列=@$"Server=localhost;Database=sakila;Uid=root;Pwd=password;";
    private static void LoadSQLServer(string データベース名,VM.Container Container){
        var information_schema=new LinqDB.Product.SQLServer.information_schema(false);
        using var SqlConnection=new SqlConnection(SQLServer接続文字列);
        DbConnection Connection=SqlConnection;
        Connection.Open();
        using var Command = Connection.CreateCommand();
        //var SCHEMA = new SqlParameter("@SCHEMA",Data.SqlDbType.NVarChar,-1);
        //var NAME = new SqlParameter("@NAME",Data.SqlDbType.NVarChar,-1);
        var CATALOG =CreateParameter("@CATALOG");
        CATALOG.Value=データベース名;
        var SCHEMA =CreateParameter("@SCHEMA");
        var NAME =CreateParameter("@NAME");
        DbParameter CreateParameter(string ParameterName) {
            var p =Command.CreateParameter();
            p.ParameterName=ParameterName;
            p.DbType=System.Data.DbType.String;
            return p;
        }
        var Parameters=Command.Parameters;
        Container.Clear();
        Container.Name=データベース名;
        Connection.ChangeDatabase(データベース名);
        var Schemas=Container.Schemas;
        Parameters.Add(CATALOG);
        Command.CommandText=information_schema.SQL_Schema;
        using(var Reader=Command.ExecuteReader()) {
            while(Reader.Read()){
                var SchemaName=Reader.GetString(0);
                Container.CreateSchema(SchemaName);
            }
        }
        Parameters.Add(SCHEMA);
        Command.CommandText=information_schema.SQL_Types;
        foreach(var Schema in Schemas) {
            SCHEMA.Value=Schema.Name;
            using var Reader = Command.ExecuteReader();
            while(Reader.Read()) {
                var 新Name = Reader.GetString(0);
                var 旧Name = Reader.GetString(1);
                var IsNullable= Reader.GetBoolean(2);
                Schema.CreateScalarType(新Name,旧Name);
            }
        }
        Command.CommandText=information_schema.SQL_Table;
        foreach(var Schema in Schemas) {
            SCHEMA.Value=Schema.Name;
            using var Reader = Command.ExecuteReader();
            while(Reader.Read()) {
                var Name = Reader.GetString(0);
                Schema.CreateTable(Name);
            }
        }
        Command.CommandText=information_schema.SQL_View;
        foreach(var Schema in Schemas) {
            SCHEMA.Value=Schema.Name;
            using var Reader = Command.ExecuteReader();
            while(Reader.Read()) {
                var Name = Reader.GetString(0);
                var SQL = Reader.GetString(1);
                Schema.CreateView(Name,SQL);
            }
        }
        Command.CommandText=information_schema.SQL_ScalarFunction;
        foreach(var Schema in Schemas)
        {
            SCHEMA.Value=Schema.Name;
            using var Reader = Command.ExecuteReader();
            while(Reader.Read())
            {
                var Name = Reader.GetString(0);
                var Type = CommonLibrary.SQLのTypeからTypeに変換(Reader.GetString(1));
                var SQL = Reader.GetString(2);
                Schema.CreateScalarFunction(Name,Type,SQL);
            }
        }
        Command.CommandText=information_schema.SQL_TableFunction;
        foreach(var Schema in Schemas)
        {
            SCHEMA.Value=Schema.Name;
            using var Reader = Command.ExecuteReader();
            while(Reader.Read())
            {
                var Name = Reader.GetString(0);
                var SQL = Reader.GetString(1);
                Schema.CreateTableFunction(Name,SQL);
            }
        }
        var count=0;
        Command.CommandText=information_schema.SQL_Procedure;
        foreach(var Schema in Schemas)
        {
            SCHEMA.Value=Schema.Name;
            using var Reader = Command.ExecuteReader();
            while(Reader.Read())
            {
                var Name = Reader.GetString(0);
                var SQL = Reader.GetString(1);
                Schema.CreateProcedure(Name,typeof(int),SQL);
                count++;
                //break;
            }
        }
        Command.CommandText=information_schema.SQL_Synonym;
        foreach(ISchema Schema in Schemas) {
            SCHEMA.Value=Schema.Name;
            using var Reader = Command.ExecuteReader();
            while(Reader.Read()) {
                var synonym = Reader.GetString(0);
                var base_object_name = Reader.GetString(1);
                Schema.CreateSynonym(synonym,base_object_name);
            }
        }
        Command.CommandText=information_schema.Sequences;
        foreach(var Schema in Schemas) {
            SCHEMA.Value=Schema.Name;
            using var Reader = Command.ExecuteReader();
            while(Reader.Read()) {
                var Name = Reader.GetString(0);
                var start_value= Reader.GetValue(1);
                var increment= Reader.GetValue(2);
                var current_value= Reader.GetValue(3);
                Schema.CreateSequence(Name,start_value,increment,current_value);
            }
        }
        Parameters.Add(NAME);
        Command.CommandText=information_schema.SQL_Function_Parameter;
        foreach(var Schema in Schemas) {
            SCHEMA.Value=Schema.Name;
            foreach(var ScalarFunction in Schema.ScalarFunctions){
                NAME.Value=ScalarFunction.Name;
                var ScalarFunction_Parameters = ScalarFunction.Parameters;
                using var Reader = Command.ExecuteReader();
                while(Reader.Read()) {
                    var name = Reader.GetString(0);
                    var Type = CommonLibrary.SQLのTypeからTypeに変換(Reader.GetString(1));
                    var has_default_value = Reader.GetBoolean(2);
                    var default_value = Reader.GetValue(3);
                    ScalarFunction_Parameters.Add(new Parameter(name,Type,has_default_value,default_value));
                }
            }
            foreach(var TableFunction in Schema.TableFunctions){
                NAME.Value=TableFunction.Name;
                var TableFunction_Parameters = TableFunction.Parameters;
                using var Reader = Command.ExecuteReader();
                //TableFunction.Type=共通(TableFunction.Parameters);
                while(Reader.Read()) {
                    var name = Reader.GetString(0);
                    var Type = CommonLibrary.SQLのTypeからTypeに変換(Reader.GetString(1));
                    var has_default_value = Reader.GetBoolean(2);
                    var default_value = Reader.GetValue(3);
                    TableFunction_Parameters.Add(new Parameter(name,Type,has_default_value,default_value));
                }
            }
        }
        Command.CommandText=information_schema.SQL_Table_Column;
        foreach(var Schema in Schemas) {
            SCHEMA.Value=Schema.Name;
            foreach(var Table in Schema.Tables) {
                NAME.Value=Table.Name;
                using var Reader=Command.ExecuteReader();
                while(Reader.Read()) {
                    //var a=Reader.GetString(1);
                    //var b=CommonLibrary.SQLのTypeからTypeに変換(Reader.GetString(0));

                    //var c=Reader.GetString(2)=="YES";
                    //var d=Reader.GetInt32(3)!=0);
                    var TypeのSchemaName=Reader.GetString(0);
                    var TypeのSchema=Schemas.Find(p=>p.Name==TypeのSchemaName);
                    Debug.Assert(TypeのSchema is not null);
                    var TypeName=Reader.GetString(1);
                    var ScalarType=TypeのSchema.ScalarTypes.Find(p=>p.新Name==TypeName);
                    if(ScalarType is not null)
                        TypeName=ScalarType.旧Name;
                    Table.CreateColumn(
                        Reader.GetString(2),
                        CommonLibrary.SQLのTypeからTypeに変換(TypeName),
                        Reader.GetBoolean(3),
                        Reader.GetInt32(4)>0
                    );
                }
            }
        }
        Command.CommandText=information_schema.SQL_View_FunctionTable_Column;
        foreach(var Schema in Schemas) {
            SCHEMA.Value=Schema.Name;
            foreach(var a in Schema.Views) {
                NAME.Value=a.Name;
                using var Reader=Command.ExecuteReader();
                while(Reader.Read())
                    a.CreateColumn(
                        Reader.GetString(0),
                        CommonLibrary.SQLのTypeからTypeに変換(Reader.GetString(1)),
                        Reader.GetBoolean(2)
                    );
            }
            foreach(var a in Schema.TableFunctions) {
                NAME.Value=a.Name;
                using var Reader=Command.ExecuteReader();
                while(Reader.Read())
                    a.CreateColumn(
                        Reader.GetString(0),
                        CommonLibrary.SQLのTypeからTypeに変換(Reader.GetString(1)),
                        Reader.GetBoolean(2)
                    );
            }
        }
        Command.CommandText=information_schema.SQL_Trigger;
        foreach(var Schema in Schemas) {
            SCHEMA.Value=Schema.Name;
            foreach(var Table in Schema.Tables) {
                NAME.Value=Table.Name;
                using var Reader=Command.ExecuteReader();
                while(Reader.Read()) {
                    var TRIGGER_NAME=Reader.GetString(0);
                    var EVENT_MANIPULATION=Reader.GetString(4);
                    var ACTION_STATEMENT=Reader.GetString(5);
                }
            }
        }
        Command.Parameters.Clear();
        Command.CommandText=information_schema.SQL_ForeignKey;
        using(var Reader=Command.ExecuteReader()) {
            while(Reader.Read()) {
                var ForeignKeyName=Reader.GetString(0);
                var ParentSchemaName=Reader.GetString(1);
                var ParentTableName=Reader.GetString(2);
                var ChildSchemaName=Reader.GetString(3);
                var ChildTableName=Reader.GetString(4);
                var ParentSchema=Schemas.Single(p=>p.Name==ParentSchemaName);
                var ParentTable=ParentSchema.Tables.Single(p=>p.Name==ParentTableName);
                var ChildSchema=Schemas.Single(p=>p.Name==ChildSchemaName);
                var ChildTable=ChildSchema.Tables.Single(p=>p.Name==ChildTableName);
                Container.CreateRelation(ForeignKeyName,ParentTable,ChildTable);
            }
        }
        Command.CommandText=information_schema.SQL_ForeignKey_Column;
        using(var Reader=Command.ExecuteReader()) {
            while(Reader.Read()) {
                var ForeignKeyName=Reader.GetString(0);
                var ChildSchemaName=Reader.GetString(1);
                var ChildTableName=Reader.GetString(2);
                var ChildColumnName=Reader.GetString(3);
                var ChildColumn=Schemas.Single(p=>p.Name==ChildSchemaName).Tables.Single(p=>p.Name==ChildTableName).Columns.Single(p=>p.Name==ChildColumnName);
                //var ParentSchemaName=Reader.GetString(4);
                //var ParentTableName=Reader.GetString(5);
                Container.Relations.Single(p=>p.Name==ForeignKeyName).AddColumn(ChildColumn);
            }
        }
        Connection.Close();
    }
    private static void Main() {
        const string SQLServerログイン = @"User ID=sa;Password=SQLSERVER711409;";
        var a=new DateTime(2001,1,1);
        var b=new DateTime(2001,1,2);
        var r=Comparer<DateTime>.Default.Compare(a,b);

        {
            const string ホスト名= @"COFFEELAKE\MSSQLSERVER2022";
            const string SQLServer接続文字列 = @$"Data Source={ホスト名};Initial Catalog=master;Integrated Security=false;{SQLServerログイン}";
            using var SqlConnection=new SqlConnection(SQLServer接続文字列);
            DbConnection Connection=SqlConnection;
            Connection.Open();
        }
        //using var OleDbConnection = new OleDbConnection("Provider=SQLOLEDB;"+接続文字列);
        var AssemblyGenerator=new AssemblyGenerator();
        foreach(var Database in Databases){
            var Container=new VM.Container();
            LoadSQLServer(Database,Container);
            //if(a++==0x
            AssemblyGenerator.Save(Container,Environment.CurrentDirectory);
        }
    }
    private static readonly string[]Databases={
        //"TableFunction",
        //"Sequence",
        //"実験",
        //"Pubs",
        //"Northwind",
        "WideWorldImportersDW",
        "WideWorldImporters",
        "AdventureWorksDW2008R2",
        "AdventureWorksDW2012",
        "AdventureWorksDW2014",
        "AdventureWorksDW2016",
        "AdventureWorksDW2016_EXT",
        "AdventureWorksDW2017",
        "AdventureWorksDW2019",
        "AdventureWorksLT2008R2",
        "AdventureWorksLT2012",
        "AdventureWorksLT2014",
        "AdventureWorksLT2016",
        "AdventureWorksLT2016_EXT",
        "AdventureWorksLT2017",
        "AdventureWorksLT2019",
        "AdventureWorksLT2022",
        //"msdb",
    };
}