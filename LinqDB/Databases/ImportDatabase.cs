using System;
using System.Diagnostics;
using System.Xml;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Linq;
using LinqDB.Sets;
using System.IO;
using System.Text;
using LinqDB.Helpers;
using System.Runtime.CompilerServices;
using Data=System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows;
using System.Collections.ObjectModel;
using System.CodeDom.Compiler;

namespace LinqDB.Databases {
    public class ImportDatabase:IDisposable {
        public static Int32? Add(Int32 a,Int32 b) => a+b;
        public static Int32? Add(Int32 a,Int32? b) => a+b;
        public static Int32? Add(Int32? a,Int32 b) => a+b;
        public static Int32? Add(Int32? a,Int32? b) => a+b;
        public static Boolean 比較(Int32 a,Int32 b) => a.Equals(b);
        public static Boolean 比較(Int32 a,Int32? b) => a.Equals(b);
        public static Boolean 比較(Int32? a,Int32 b) => a.Equals(b);
        public static Boolean 比較(Int32?a,Int32? b) => a.Equals(b);
        /// <summary>アンマネージ リソースの解放またはリセットに関連付けられているアプリケーション定義のタスクを実行します。</summary>
        /// <filterpriority>2</filterpriority>
        /// <summary>オブジェクトが、ガベージ コレクションによって収集される前に、リソースの解放とその他のクリーンアップ操作の実行を試みることができるようにします。</summary>
        ~ImportDatabase() => this.Dispose(false);
        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 破棄されているか
        /// </summary>
        private Boolean IsDisposed {
            get; set;
        }
        /// <summary>
        /// ファイナライザでDispose(false)する。
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(Boolean disposing) {
            if(this.IsDisposed) {
                this.IsDisposed=true;
                if(disposing) {
                    //this.Optimizer.Dispose();
                }
            }
        }
        //public String? AProperty { get;private set;}
        //public String? AField;
        //public (Int32?a,String?b) AReturn() {
        //    return (1,"b");
        //}
        //public void AInput(Int32? a,String? b) {
        //}
        protected static void Write(TextWriter Writer,String argument0) {
            Writer.Write(argument0);
            Writer.WriteLine();
        }
        protected static void Write(TextWriter Writer,String argument0,String argument1) {
            Writer.Write(argument0);
            Writer.Write(argument1);
            Writer.WriteLine();
        }
        protected static void Write(TextWriter Writer,String argument0,String argument1,String argument2) {
            Writer.Write(argument0);
            Writer.Write(argument1);
            Writer.Write(argument2);
            Writer.WriteLine();
        }
        protected static void Write(TextWriter Writer,String argument0,String argument1,String argument2,String argument3) {
            Writer.Write(argument0);
            Writer.Write(argument1);
            Writer.Write(argument2);
            Writer.Write(argument3);
            Writer.WriteLine();
        }
        protected static void Write(TextWriter Writer,String argument0,String argument1,String argument2,String argument3,String argument4) {
            Writer.Write(argument0);
            Writer.Write(argument1);
            Writer.Write(argument2);
            Writer.Write(argument3);
            Writer.Write(argument4);
            Writer.WriteLine();
        }
        protected static void Write(TextWriter Writer,String argument0,String argument1,String argument2,String argument3,String argument4,String argument5) {
            Writer.Write(argument0);
            Writer.Write(argument1);
            Writer.Write(argument2);
            Writer.Write(argument3);
            Writer.Write(argument4);
            Writer.Write(argument5);
            Writer.WriteLine();
        }
        protected static void Write(TextWriter Writer,String argument0,String argument1,String argument2,String argument3,String argument4,String argument5,String argument6) {
            Writer.Write(argument0);
            Writer.Write(argument1);
            Writer.Write(argument2);
            Writer.Write(argument3);
            Writer.Write(argument4);
            Writer.Write(argument5);
            Writer.Write(argument6);
            Writer.WriteLine();
        }
        protected static void Write(TextWriter Writer,String argument0,String argument1,String argument2,String argument3,String argument4,String argument5,String argument6,String argument7) {
            Writer.Write(argument0);
            Writer.Write(argument1);
            Writer.Write(argument2);
            Writer.Write(argument3);
            Writer.Write(argument4);
            Writer.Write(argument5);
            Writer.Write(argument6);
            Writer.Write(argument7);
            Writer.WriteLine();
        }
        protected static void Write(TextWriter Writer,String argument0,String argument1,String argument2,String argument3,String argument4,String argument5,String argument6,String argument7,String argument8) {
            Writer.Write(argument0);
            Writer.Write(argument1);
            Writer.Write(argument2);
            Writer.Write(argument3);
            Writer.Write(argument4);
            Writer.Write(argument5);
            Writer.Write(argument6);
            Writer.Write(argument7);
            Writer.Write(argument8);
            Writer.WriteLine();
        }
        protected static void Write(TextWriter Writer,String argument0,String argument1,String argument2,String argument3,String argument4,String argument5,String argument6,String argument7,String argument8,String argument9) {
            Writer.Write(argument0);
            Writer.Write(argument1);
            Writer.Write(argument2);
            Writer.Write(argument3);
            Writer.Write(argument4);
            Writer.Write(argument5);
            Writer.Write(argument6);
            Writer.Write(argument7);
            Writer.Write(argument8);
            Writer.Write(argument9);
            Writer.WriteLine();
        }
        protected static void Write(TextWriter Writer,String argument0,String argument1,String argument2,String argument3,String argument4,String argument5,String argument6,String argument7,String argument8,String argument9,String argument10) {
            Writer.Write(argument0);
            Writer.Write(argument1);
            Writer.Write(argument2);
            Writer.Write(argument3);
            Writer.Write(argument4);
            Writer.Write(argument5);
            Writer.Write(argument6);
            Writer.Write(argument7);
            Writer.Write(argument8);
            Writer.Write(argument9);
            Writer.Write(argument10);
            Writer.WriteLine();
        }
        protected static void Write(TextWriter Writer,String argument0,String argument1,String argument2,String argument3,String argument4,String argument5,String argument6,String argument7,String argument8,String argument9,String argument10,String argument11) {
            Writer.Write(argument0);
            Writer.Write(argument1);
            Writer.Write(argument2);
            Writer.Write(argument3);
            Writer.Write(argument4);
            Writer.Write(argument5);
            Writer.Write(argument6);
            Writer.Write(argument7);
            Writer.Write(argument8);
            Writer.Write(argument9);
            Writer.Write(argument10);
            Writer.Write(argument11);
            Writer.WriteLine();
        }
        protected static void Write(TextWriter Writer,String argument0,String argument1,String argument2,String argument3,String argument4,String argument5,String argument6,String argument7,String argument8,String argument9,String argument10,String argument11,String argument12) {
            Writer.Write(argument0);
            Writer.Write(argument1);
            Writer.Write(argument2);
            Writer.Write(argument3);
            Writer.Write(argument4);
            Writer.Write(argument5);
            Writer.Write(argument6);
            Writer.Write(argument7);
            Writer.Write(argument8);
            Writer.Write(argument9);
            Writer.Write(argument10);
            Writer.Write(argument11);
            Writer.Write(argument12);
            Writer.WriteLine();
        }
        protected static void Write(TextWriter Writer,String argument0,String argument1,String argument2,String argument3,String argument4,String argument5,String argument6,String argument7,String argument8,String argument9,String argument10,String argument11,String argument12,String argument13,String argument14) {
            Writer.Write(argument0);
            Writer.Write(argument1);
            Writer.Write(argument2);
            Writer.Write(argument3);
            Writer.Write(argument4);
            Writer.Write(argument5);
            Writer.Write(argument6);
            Writer.Write(argument7);
            Writer.Write(argument8);
            Writer.Write(argument9);
            Writer.Write(argument10);
            Writer.Write(argument11);
            Writer.Write(argument12);
            Writer.Write(argument13);
            Writer.Write(argument14);
            Writer.WriteLine();
        }
        private readonly StringBuilder sb = new StringBuilder();
        //DbConnection
        //    SqlConnection
        //    OleDbConnection
        //    OdbcConnection
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Security","CA2100:Review SQL queries for security vulnerabilities",Justification = "<保留中>")]
        public void Read(String DatabaseName,DbConnection Connection,information_schema information_schema) {
            var SortedDictionaryCatalog = this.SortedDictionaryCotalog;
            SortedDictionaryCatalog.Clear();
            var Catalogs = this.Catalogs;
            Catalogs.Clear();
            Connection.Open();
            var Command = Connection.CreateCommand();
            //カタログ
            var CATALOG = Command.CreateParameter();
            CATALOG.ParameterName="@CATALOG";
            CATALOG.DbType=Data.DbType.String;
            Command.Parameters.Add(CATALOG);
            Command.CommandText=
                "USE master\r\n"+
                "SELECT name\r\n"+
                "FROM sys.databases\r\n"+
                "WHERE name=@CATALOG\r\n"+
                "ORDER BY database_id";
            CATALOG.Value=DatabaseName;
            using(var Reader = Command.ExecuteReader()) {
                while(Reader.Read()) {
                    var CatalogName = Reader.GetString(0);
                    SortedDictionaryCatalog.Add(
                        CatalogName,
                        new Catalog(Escape(CatalogName))
                    );
                    Catalogs.Add(new Catalog1(Escape(CatalogName),CatalogName));
                }
            }
            Command.Parameters.Remove(CATALOG);
            var TABLE_SCHEMA = new SqlParameter("@TABLE_SCHEMA",Data.SqlDbType.NVarChar,-1);
            var TABLE_NAME = new SqlParameter("@TABLE_NAME",Data.SqlDbType.NVarChar,-1);
            foreach(var Catalog in SortedDictionaryCatalog) {
                var CatalogKey = Catalog.Key;
                Connection.ChangeDatabase(CatalogKey);
                var SortedDictionarySchema = Catalog.Value.SortedDictionarySchema;
                var SortedDictionary_SelfChild = Catalog.Value.SortedDictionary_SelfChild;
                var SortedDictionary_SelfParent = Catalog.Value.SortedDictionary_SelfParent;
                var Catalog1 = Catalogs.Single(p => p.Name==Catalog.Value.EscapedName);
                var Schemas =Catalog1.Schemas;
                var SQLSchema = information_schema.SQLSchema;
                Command.CommandText=SQLSchema;
                using(var Reader = Command.ExecuteReader()) {
                    while(Reader.Read()) {
                        var SchemaName = Reader.GetString(0);
                        SortedDictionarySchema.Add(
                            SchemaName,
                            new Schema(
                                Escape(SchemaName)
                            )
                        );
                        Schemas.Add(new Schema1(SchemaName));
                    }
                }
                Command.Parameters.Add(TABLE_SCHEMA);
                var SQLTable = information_schema.SQLTable;
                Command.CommandText=SQLTable;
                foreach(var Schema in SortedDictionarySchema) {
                    var Schema1 = Schemas.Single(p => p.Name==Schema.Key);
                    var Tables=Schema1.Tables;
                    TABLE_SCHEMA.Value=Schema.Key;
                    var SortedDictionaryTable = Schema.Value.SortedDictionaryTable;
                    using var Reader = Command.ExecuteReader();
                    while(Reader.Read()) {
                        var TableName = Reader.GetString(0);
                        SortedDictionaryTable.Add(
                            TableName,
                            new Table(
                                Schema.Value,
                                TableName
                            )
                        );
                        Tables.Add(new Table1(Schema1,TableName));
                    }
                }
                var SQLView = information_schema.SQLView;
                Command.CommandText=SQLView;
                foreach(var Schema in SortedDictionarySchema) {
                    var Schema1 = Schemas.Single(p => p.Name==Schema.Key);
                    var Views=Schema1.Views;
                    TABLE_SCHEMA.Value=Schema.Key;
                    var SortedDictionaryView = Schema.Value.SortedDictionaryView;
                    using var Reader = Command.ExecuteReader();
                    while(Reader.Read()) {
                        var TableName = Reader.GetString(0);
                        SortedDictionaryView.Add(
                            TableName,
                            new View(
                                Schema.Value,
                                TableName,
                                Reader.GetString(1)
                            )
                        );
                        Views.Add(new View1(Schema1,TableName,Reader.GetString(1)));
                    }
                }
                var SQLProcedure =information_schema.SQLProcedure;
                Command.CommandText=SQLProcedure;
                foreach(var Schema in SortedDictionarySchema) {
                    TABLE_SCHEMA.Value=Schema.Key;
                    var SortedDictionaryProcedure = Schema.Value.SortedDictionaryProcedure;
                    using var Reader = Command.ExecuteReader();
                    while(Reader.Read()) {
                        SortedDictionaryProcedure.Add(
                            Reader.GetString(0),
                            new Procedure(
                                Reader.GetString(1)
                            )
                        );
                    }
                }
                var SQLTableColumn = information_schema.SQLTableColumn;
                Command.CommandText=SQLTableColumn;
                Command.Parameters.Add(TABLE_NAME);
                foreach(var Schema in SortedDictionarySchema) {
                    var Schema1 = Schemas.Single(p => p.Name==Schema.Key);
                    TABLE_SCHEMA.Value=Schema.Key;
                    foreach(var Table in Schema.Value.SortedDictionaryTable) {
                        var Tables = Schema1.Tables;
                        var Table1=Tables.Single(p => p.Name==Table.Key);
                        TABLE_NAME.Value=Table.Key;
                        var ListColumn = Table.Value.ListColumn;
                        var ListColumn1=Table1.ListColumn;
                        using var Reader = Command.ExecuteReader();
                        while(Reader.Read()) {
                            var Type = DBTypeからTypeに変換(Reader.GetString(0));
                            var Types1=this.Types1;
                            Types1[0]=Type;
                            ListColumn.Add(
                                new Column(
                                    Type,
                                    Escape(Reader.GetString(1)),
                                    Reader.GetString(2)=="YES"
                                )
                            );
                            ListColumn1.Add(
                                new Column1(
                                    Type,
                                    Escape(Reader.GetString(1)),
                                    Reader.GetString(2)=="YES"
                                )
                            );
                        }
                    }
                }
                var SQLTrigger = information_schema.SQLTrigger;
                Command.CommandText=SQLTrigger;
                foreach(var Schema in SortedDictionarySchema) {
                    TABLE_SCHEMA.Value=Schema.Key;
                    foreach(var Table in Schema.Value.SortedDictionaryTable) {
                        TABLE_NAME.Value=Table.Key;
                        using var Reader = Command.ExecuteReader();
                        var Value = Table.Value;
                        var SortedDictionaryBeforeTrigger = Value.SortedDictionaryBeforeTrigger;
                        var SortedDictionaryAfterTrigger = Value.SortedDictionaryAfterTrigger;
                        while(Reader.Read()) {
                            var TRIGGER_NAME = Reader.GetString(0);
                            var SortedDictionaryTrigger=Reader.GetString(3)=="BEFORE"?SortedDictionaryBeforeTrigger:SortedDictionaryAfterTrigger;
                            if(!SortedDictionaryTrigger.TryGetValue(TRIGGER_NAME,out var Trigger)) {
                                SortedDictionaryTrigger.Add(TRIGGER_NAME,Trigger=new Trigger());
                            }
                            var EVENT_MANIPULATION = Reader.GetString(4);
                            var ACTION_STATEMENT = Reader.GetString(5);
                            switch(EVENT_MANIPULATION) {
                                case "INSERT":
                                    Trigger.InsertSQL=ACTION_STATEMENT;break;
                                case "UPDATE":
                                    Trigger.UpdateSQL=ACTION_STATEMENT; break;
                                case "DELETE":
                                    Trigger.DeleteSQL=ACTION_STATEMENT; break;
                                default: throw new NotSupportedException(EVENT_MANIPULATION+"はトリガー対象になるテーブル操作ではない");
                            }
                        }
                    }
                }
                var SQLViewColumn = information_schema.SQLViewColumn;
                Command.CommandText=SQLViewColumn;
                foreach(var Schema in SortedDictionarySchema) {
                    var Schema1 = Schemas.Single(p => p.Name==Schema.Key);
                    TABLE_SCHEMA.Value=Schema.Key;
                    foreach(var View in Schema.Value.SortedDictionaryView) {
                        var View1 = Schema1.Views.Single(p => p.Name==View.Key);
                        var ListColumn1 = View1.ListColumn;
                        TABLE_NAME.Value=View.Key;
                        var ListColumn = View.Value.ListColumn;
                        using var Reader = Command.ExecuteReader();
                        while(Reader.Read()) {
                            var Type = DBTypeからTypeに変換(Reader.GetString(0));
                            var Types1 = this.Types1;
                            Types1[0]=Type;
                            ListColumn.Add(
                                new Column(
                                    Type,
                                    Escape(Reader.GetString(1)),
                                    Reader.GetString(2)=="YES"
                                )
                            );
                            ListColumn1.Add(
                                new Column1(
                                    Type,
                                    Escape(Reader.GetString(1)),
                                    Reader.GetString(2)=="YES"
                                )
                            );
                        }
                    }
                }
                var SQLPrimaryKey= information_schema.SQLPrimaryKey;
                Command.CommandText=SQLPrimaryKey;
                foreach(var Schema in SortedDictionarySchema) {
                    var Schema1 = Schemas.Single(p => p.Name==Schema.Key);
                    TABLE_SCHEMA.Value=Schema.Key;
                    foreach(var Table in Schema.Value.SortedDictionaryTable) {
                        var Table1 = Schema1.Tables.Single(p => p.Name==Table.Key);
                        var ListColumn1 = Table1.ListColumn;
                        var ListPrimaryKeyColumn1 = Table1.ListPrimaryKeyColumn;
                        TABLE_NAME.Value=Table.Key;
                        var ListColumn = Table.Value.ListColumn;
                        var ListPrimaryKeyColumn = Table.Value.ListPrimaryKeyColumn;
                        using var Reader = Command.ExecuteReader();
                        while(Reader.Read()) {
                            var 識別子 = Escape(Reader.GetString(0));
                            ListPrimaryKeyColumn.Add(ListColumn.Single(p => p.Name==識別子));
                            ListPrimaryKeyColumn1.Add(ListColumn1.Single(p => p.Name==識別子));
                        }
                    }
                }
                var SQLForeignKey= information_schema.SQLForeignKey;
                Command.Parameters.Clear();
                Command.CommandText=SQLForeignKey;
                using(var Reader = Command.ExecuteReader()) {
                    while(Reader.Read()) {
                        var ParentSchemaName = Reader.GetString(0);
                        var ParentTableName = Reader.GetString(1);
                        var ChildSchemaName = Reader.GetString(2);
                        var ChildTableName = Reader.GetString(3);
                        var ForeignKeyName = Reader.GetString(4);
                        var ParentSchema = SortedDictionarySchema[ParentSchemaName];
                        var ParentTable = ParentSchema.SortedDictionaryTable[ParentTableName];
                        var ChildSchema = SortedDictionarySchema[ChildSchemaName];
                        var ChildTable = ChildSchema.SortedDictionaryTable[ChildTableName];
                        {
                            if(!SortedDictionary_SelfParent.TryGetValue((Schema:ChildSchemaName, Table:ChildTableName),out var SortedDictionary)) {
                                SortedDictionary=new SortedDictionary<String,ForeignKey>();
                                SortedDictionary_SelfParent.Add((ChildSchemaName, ChildTableName),SortedDictionary);
                            }
                            SortedDictionary.Add(
                                ForeignKeyName,
                                new ForeignKey(
                                    Escape(ForeignKeyName),
                                    ParentSchema,
                                    ParentTable
                                )
                            );
                            var 子_Table = Schemas.Single(p => p.Name==ChildSchemaName).Tables.Single(p => p.Name==ChildTableName);
                            var 親_Table = Schemas.Single(p => p.Name==ParentSchemaName).Tables.Single(p => p.Name==ParentTableName);
                            var 子_Table_親ForeignKey = new ForeignKey1(
                                ForeignKeyName,
                                親_Table
                            );
                            var 親_Table_子ForeignKey=new ForeignKey1(
                                ForeignKeyName,
                                子_Table
                            );
                            子_Table_親ForeignKey.対になるForeignKey1=親_Table_子ForeignKey;
                            親_Table_子ForeignKey.対になるForeignKey1=子_Table_親ForeignKey;
                            子_Table.親ForeignKeys.Add(子_Table_親ForeignKey);
                            親_Table.子ForeignKeys.Add(親_Table_子ForeignKey);
                        }
                        {
                            if(!SortedDictionary_SelfChild.TryGetValue((ParentSchemaName, ParentTableName),out var SortedDictionary)) {
                                SortedDictionary=new SortedDictionary<String,ForeignKey>();
                                SortedDictionary_SelfChild.Add((ParentSchemaName, ParentTableName),SortedDictionary);
                            }
                            SortedDictionary.Add(
                                ForeignKeyName,
                                new ForeignKey(
                                    Escape(ForeignKeyName),
                                    ChildSchema,
                                    ChildTable
                                )
                            );
                        }
                    }
                }
                var SQLForeignKeyColumn = information_schema.SQLForeignKeyColumn;
                Command.CommandText=SQLForeignKeyColumn;
                using(var Reader = Command.ExecuteReader()) {
                    while(Reader.Read()) {
                        var ForeignKeyName = Reader.GetString(0);
                        var ChildSchemaName = Reader.GetString(1);
                        var ChildTableName = Reader.GetString(2);
                        var ChildForeignKeyColumnName = Escape(Reader.GetString(3));
                        var ChildForeignKeyColumn = SortedDictionarySchema[ChildSchemaName].SortedDictionaryTable[ChildTableName].ListColumn.Single(
                            p => p.Name==ChildForeignKeyColumnName
                        );
                        var SchemaTable=SortedDictionary_SelfParent[(ChildSchemaName, ChildTableName)][ForeignKeyName];
                        if(ChildForeignKeyColumn.IsNullableClass||ChildForeignKeyColumn.Type.IsNullable()) {
                            SchemaTable.IsNullable=true;
                        }
                        SchemaTable.ListForeignKeyColumn.Add(ChildForeignKeyColumn);
                        var 親スキーマ正式識別子称 = Reader.GetString(4);
                        var 親テーブル正式識別子称 = Reader.GetString(5);
                        SortedDictionary_SelfChild[(親スキーマ正式識別子称, 親テーブル正式識別子称)][ForeignKeyName].ListForeignKeyColumn.Add(ChildForeignKeyColumn);
                        var ChildForeignKeyColumn1 = Schemas.Single(p => p.Name==ChildSchemaName).Tables.Single(p => p.Name==ChildTableName).ListColumn.Single(p => p.Name==ChildForeignKeyColumnName);
                        Schemas.Single(p => p.Name==親スキーマ正式識別子称).Tables.Single(p=>p.Name==親テーブル正式識別子称).子ForeignKeys.Single(p=>p.Name==ForeignKeyName).ListForeignKeyColumn.Add(ChildForeignKeyColumn1);

                    }
                }
            }
            Connection.Close();
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Security","CA2100:Review SQL queries for security vulnerabilities",Justification = "<保留中>")]
        public void Read1(String DatabaseName,DbConnection Connection,information_schema information_schema) {
            var Catalogs = this.Catalogs;
            Catalogs.Clear();
            Connection.Open();
            var Command = Connection.CreateCommand();
            //カタログ
            var CATALOG = Command.CreateParameter();
            CATALOG.ParameterName="@CATALOG";
            CATALOG.DbType=Data.DbType.String;
            Command.Parameters.Add(CATALOG);
            Command.CommandText=
                "USE master\r\n"+
                "SELECT name\r\n"+
                "FROM sys.databases\r\n"+
                "WHERE name=@CATALOG\r\n"+
                "ORDER BY database_id";
            CATALOG.Value=DatabaseName;
            using(var Reader = Command.ExecuteReader()) {
                while(Reader.Read()) {
                    var CatalogName = Reader.GetString(0);
                    Catalogs.Add(new Catalog1(Escape(CatalogName),CatalogName));
                }
            }
            Command.Parameters.Remove(CATALOG);
            var TABLE_SCHEMA = new SqlParameter("@TABLE_SCHEMA",Data.SqlDbType.NVarChar,-1);
            var TABLE_NAME = new SqlParameter("@TABLE_NAME",Data.SqlDbType.NVarChar,-1);
            foreach(var Catalog in Catalogs) {
                Connection.ChangeDatabase(Catalog.DatabaseName);
                var Schemas = Catalog.Schemas;
                var SQLSchema = information_schema.SQLSchema;
                Command.CommandText=SQLSchema;
                using(var Reader = Command.ExecuteReader()) {
                    while(Reader.Read()) {
                        var SchemaName = Reader.GetString(0);
                        Schemas.Add(new Schema1(SchemaName));
                    }
                }
                Command.Parameters.Add(TABLE_SCHEMA);
                var SQLTable = information_schema.SQLTable;
                Command.CommandText=SQLTable;
                foreach(var Schema in Schemas) {
                    var Tables = Schema.Tables;
                    TABLE_SCHEMA.Value=Schema.Name;
                    using var Reader = Command.ExecuteReader();
                    while(Reader.Read()) {
                        var TableName = Reader.GetString(0);
                        Tables.Add(new Table1(Schema,TableName));
                    }
                }
                var SQLView = information_schema.SQLView;
                Command.CommandText=SQLView;
                foreach(var Schema in Schemas) {
                    var Views = Schema.Views;
                    TABLE_SCHEMA.Value=Schema.Name;
                    using var Reader = Command.ExecuteReader();
                    while(Reader.Read()) {
                        var ViewName = Reader.GetString(0);
                        Views.Add(new View1(Schema,ViewName,Reader.GetString(1)));
                    }
                }
                var SQLProcedure = information_schema.SQLProcedure;
                Command.CommandText=SQLProcedure;
                foreach(var Schema in Schemas) {
                    TABLE_SCHEMA.Value=Schema.Name;
                    using var Reader = Command.ExecuteReader();
                    while(Reader.Read()) {
                        //Reader.GetString(1)
                    }
                }
                var SQLTableColumn = information_schema.SQLTableColumn;
                Command.CommandText=SQLTableColumn;
                Command.Parameters.Add(TABLE_NAME);
                foreach(var Schema in Schemas) {
                    TABLE_SCHEMA.Value=Schema.Name;
                    foreach(var Table in Schema.Tables) {
                        TABLE_NAME.Value=Table.Name;
                        var ListColumn = Table.ListColumn;
                        using var Reader = Command.ExecuteReader();
                        while(Reader.Read()) {
                            var Type = DBTypeからTypeに変換(Reader.GetString(0));
                            ListColumn.Add(
                                new Column1(
                                    Type,
                                    Escape(Reader.GetString(1)),
                                    Reader.GetString(2)=="YES"
                                )
                            );
                        }
                    }
                }
                var SQLTrigger = information_schema.SQLTrigger;
                Command.CommandText=SQLTrigger;
                foreach(var Schema in Schemas) {
                    TABLE_SCHEMA.Value=Schema.Name;
                    foreach(var Table in Schema.Tables) {
                        TABLE_NAME.Value=Table.Name;
                        using var Reader = Command.ExecuteReader();
                        while(Reader.Read()) {
                            var TRIGGER_NAME = Reader.GetString(0);
                            var EVENT_MANIPULATION = Reader.GetString(4);
                            var ACTION_STATEMENT = Reader.GetString(5);
                        }
                    }
                }
                var SQLViewColumn = information_schema.SQLViewColumn;
                Command.CommandText=SQLViewColumn;
                foreach(var Schema in Schemas) {
                    TABLE_SCHEMA.Value=Schema.Name;
                    foreach(var View in Schema.Views) {
                        TABLE_NAME.Value=View.Name;
                        var ListColumn = View.ListColumn;
                        using var Reader = Command.ExecuteReader();
                        while(Reader.Read()) {
                            var Type = DBTypeからTypeに変換(Reader.GetString(0));
                            var Types1 = this.Types1;
                            Types1[0]=Type;
                            ListColumn.Add(
                                new Column1(
                                    Type,
                                    Escape(Reader.GetString(1)),
                                    Reader.GetString(2)=="YES"
                                )
                            );
                        }
                    }
                }
                var SQLPrimaryKey = information_schema.SQLPrimaryKey;
                Command.CommandText=SQLPrimaryKey;
                foreach(var Schema in Schemas) {
                    TABLE_SCHEMA.Value=Schema.Name;
                    foreach(var Table in Schema.Tables) {
                        TABLE_NAME.Value=Table.Name;
                        var ListColumn = Table.ListColumn;
                        var ListPrimaryKeyColumn = Table.ListPrimaryKeyColumn;
                        using var Reader = Command.ExecuteReader();
                        while(Reader.Read()) {
                            var 識別子 = Escape(Reader.GetString(0));
                            ListPrimaryKeyColumn.Add(ListColumn.Single(p => p.Name==識別子));
                        }
                    }
                }
                foreach(var Schema in Schemas) {
                    foreach(var Table in Schema.Tables) {
                        var ListPrimaryKeyColumn = Table.ListPrimaryKeyColumn;
                        if(ListPrimaryKeyColumn.Count==0) {
                            foreach(var Column in Table.ListColumn) {
                                ListPrimaryKeyColumn.Add(Column);
                            }
                        }
                    }
                }
                var SQLForeignKey = information_schema.SQLForeignKey;
                Command.Parameters.Clear();
                Command.CommandText=SQLForeignKey;
                using(var Reader = Command.ExecuteReader()) {
                    while(Reader.Read()) {
                        var ParentSchemaName = Reader.GetString(0);
                        var ParentTableName = Reader.GetString(1);
                        var ChildSchemaName = Reader.GetString(2);
                        var ChildTableName = Reader.GetString(3);
                        var ForeignKeyName = Reader.GetString(4);
                        var ParentSchema = Schemas.Single(p=>p.Name==ParentSchemaName);
                        var ParentTable = ParentSchema.Tables.Single(p=>p.Name==ParentTableName);
                        var ChildSchema = Schemas.Single(p => p.Name==ChildSchemaName);
                        var ChildTable = ChildSchema.Tables.Single(p => p.Name==ChildTableName);
                        {
                            //if(!SortedDictionary_SelfParent.TryGetValue((Schema: ChildSchemaName, Table: ChildTableName),out var SortedDictionary)) {
                            //    SortedDictionary=new SortedDictionary<String,ForeignKey>();
                            //    SortedDictionary_SelfParent.Add((ChildSchemaName, ChildTableName),SortedDictionary);
                            //}
                            //SortedDictionary.Add(
                            //    ForeignKeyName,
                            //    new ForeignKey(
                            //        Escape(ForeignKeyName),
                            //        ParentSchema,
                            //        ParentTable
                            //    )
                            //);
                            var 子_Table = Schemas.Single(p => p.Name==ChildSchemaName).Tables.Single(p => p.Name==ChildTableName);
                            var 親_Table = Schemas.Single(p => p.Name==ParentSchemaName).Tables.Single(p => p.Name==ParentTableName);
                            var 子_Table_親ForeignKey = new ForeignKey1(
                                ForeignKeyName,
                                親_Table
                            );
                            var 親_Table_子ForeignKey = new ForeignKey1(
                                ForeignKeyName,
                                子_Table
                            );
                            子_Table_親ForeignKey.対になるForeignKey1=親_Table_子ForeignKey;
                            親_Table_子ForeignKey.対になるForeignKey1=子_Table_親ForeignKey;
                            子_Table.親ForeignKeys.Add(子_Table_親ForeignKey);
                            親_Table.子ForeignKeys.Add(親_Table_子ForeignKey);
                        }
                    }
                }
                var SQLForeignKeyColumn = information_schema.SQLForeignKeyColumn;
                Command.CommandText=SQLForeignKeyColumn;
                using(var Reader = Command.ExecuteReader()) {
                    while(Reader.Read()) {
                        var ForeignKeyName = Reader.GetString(0);
                        var ChildSchemaName = Reader.GetString(1);
                        var ChildTableName = Reader.GetString(2);
                        var ChildForeignKeyColumnName = Escape(Reader.GetString(3));
                        var ChildForeignKeyColumn = Schemas.Single(p => p.Name==ChildSchemaName).Tables.Single(p => p.Name==ChildTableName).ListColumn.Single(p => p.Name==ChildForeignKeyColumnName);
                        var 親スキーマ正式識別子称 = Reader.GetString(4);
                        var 親テーブル正式識別子称 = Reader.GetString(5);
                        var ParentSchema = Schemas.Single(p => p.Name==親スキーマ正式識別子称);
                        var ParentTable = ParentSchema.Tables.Single(p => p.Name==親テーブル正式識別子称);
                        var ParentTable_子ForeignKey = ParentTable.子ForeignKeys.Single(p => p.Name==ForeignKeyName);
                        ParentTable_子ForeignKey.ListForeignKeyColumn.Add(ChildForeignKeyColumn);

                    }
                }
            }
            Connection.Close();
        }
        private static Type DBTypeからTypeに変換(String DBType) => Optimizers.Optimizer.DBTypeからTypeに変換(DBType);
        private static String Escape(String s) {
            switch(s) {
                case "event":
                case "operator": s='@'+s; break;
            }
            return s.Replace(' ','_').Replace('-','_');
        }
        [DebuggerDisplay("{this."+nameof(Name)+"}")]
        private abstract class 共通要素 {
            /// <summary>
            /// エスケープ前
            /// </summary>
            private readonly String Name;
            /// <summary>
            /// エスケープ後
            /// </summary>
            public String EscapedName => Escape(this.Name);
            protected 共通要素(String Name) {
                this.Name=Name;
            }
            public TypeBuilder? TypeBuilder;
        }
        [DebuggerDisplay("{this."+nameof(Name)+"}")]
        private class Column1:NameDependencyObject {
            private static readonly DependencyProperty TypeProperty = DependencyProperty.Register(nameof(Type),typeof(Type),typeof(Column1));
            public Type Type {
                get => (Type)this.GetValue(TypeProperty);
                set => this.SetValue(TypeProperty,value);
            }
            private static readonly DependencyProperty IsNullableClassProperty = DependencyProperty.Register(nameof(IsNullableClass),typeof(Boolean),typeof(Column1));
            public Boolean IsNullableClass {
                get => (Boolean)this.GetValue(IsNullableClassProperty);
                set => this.SetValue(IsNullableClassProperty,value);
            }
            public Column1(Type Type,String Name,Boolean IsNullable):base(Name) {
                if(IsNullable&&Type.IsValueType) {
                    Type=typeof(Nullable<>).MakeGenericType(Type);
                    IsNullable=false;
                }
                this.Type=Type;
                this.IsNullableClass=IsNullable;
            }
            public FieldBuilder? FieldBuilder;
        }
        [DebuggerDisplay("{this."+nameof(Name)+"}")]
        private class Column {
            public readonly Type Type;
            public String TypeFullName => "global::"+this.Type.FullName;
            public readonly String Name;
            public readonly Boolean IsNullableClass;
            public Column(Type Type,String Name,Boolean IsNullable) {
                if(IsNullable&&Type.IsValueType) {
                    Type=typeof(Nullable<>).MakeGenericType(Type);
                    IsNullable=false;
                }
                this.Type=Type;
                this.Name=Name;
                this.IsNullableClass=IsNullable;
            }
            public FieldBuilder? FieldBuilder;
        }
        [DebuggerDisplay("{this."+nameof(Name)+"}")]
        private class Table1:NameDependencyObject {
            public readonly Schema1 Schema;
            public readonly ForeignKey1 PrimaryKey;
            public readonly ObservableCollection<ForeignKey1> 親ForeignKeys = new ObservableCollection<ForeignKey1>();
            public readonly ObservableCollection<ForeignKey1> 子ForeignKeys = new ObservableCollection<ForeignKey1>();
            /// <summary>
            /// ソース生成で使う。
            /// </summary>
            //public readonly String Type識別子;
            public readonly ObservableCollection<Column1> ListColumn = new ObservableCollection<Column1>();
            public ObservableCollection<Column1> ListPrimaryKeyColumn => this.PrimaryKey.ListForeignKeyColumn;
            public readonly SortedDictionary<String,Trigger> SortedDictionaryBeforeTrigger = new SortedDictionary<String,Trigger>();
            public readonly SortedDictionary<String,Trigger> SortedDictionaryAfterTrigger = new SortedDictionary<String,Trigger>();
            public Table1(Schema1 Schema,String Name):base(Name) {
                this.Schema=Schema;
                this.PrimaryKey = new ForeignKey1("PK",this);
            }
            // ReSharper disable once RedundantDefaultMemberInitializer
             public TypeBuilder? Key_TypeBuilder;
            public MethodBuilder? Key_Equals;
            public ConstructorBuilder? Key_ctor;
            public TypeBuilder? TypeBuilder;
            public ILGenerator? ctor_I;
            public FieldBuilder? FieldBuilder;
            public MethodBuilder? AddRelationship;
            public MethodBuilder? RemoveRelationship;
            public MethodBuilder? InvalidateClearRelationship;
        }
        [DebuggerDisplay("{this."+nameof(Name)+"}")]
        private class Table {
            /// <summary>
            /// エスケープ前
            /// </summary>
            public readonly String Name;
            /// <summary>
            /// エスケープ後
            /// </summary>
            public String EscapedName => Escape(this.Name);
            public readonly Schema Schema;
            /// <summary>
            /// ソース生成で使う。
            /// </summary>
            //public readonly String Type識別子;
            public readonly List<Column> ListColumn = new List<Column>();
            public readonly List<Column> ListPrimaryKeyColumn = new List<Column>();
            //public readonly HashSet<String> HashSetSelfChild = new HashSet<String>();
            public readonly SortedDictionary<String,Trigger> SortedDictionaryBeforeTrigger=new SortedDictionary<String, Trigger>();
            public readonly SortedDictionary<String,Trigger> SortedDictionaryAfterTrigger = new SortedDictionary<String,Trigger>();
            public String SchemaTableName => this.Schema.EscapedName+'.'+this.EscapedName;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="Schema"></param>
            /// <param name="Name">アセンブリの時の正式な識別子</param>
            public Table(Schema Schema,String Name) {
                this.Schema=Schema;
                this.Name=Name;
            }
            // ReSharper disable once RedundantDefaultMemberInitializer
            public TypeBuilder? Key_TypeBuilder;
            public MethodBuilder? Key_Equals;
            public ConstructorBuilder? Key_ctor;
            public TypeBuilder? TypeBuilder;
            public ILGenerator? ctor_I;
            public FieldBuilder? FieldBuilder;
            public MethodBuilder? AddRelationship;
            public MethodBuilder? RemoveRelationship;
            public MethodBuilder? InvalidateClearRelationship;
            public override String ToString() => this.EscapedName;
        }
        [DebuggerDisplay("{this."+nameof(Name)+"}")]
        private class View1:NameDependencyObject {
            private readonly Schema1? Schema;
            public readonly List<Column1> ListColumn = new List<Column1>();
            //public String SchemaTableName => this.Schema.EscapedName+'.'+this.EscapedName;
            public readonly String SQL;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="Schema"></param>
            /// <param name="Name">アセンブリの時の正式な識別子</param>
            /// <param name="SQL">VIEWの定義本体</param>
            public View1(Schema1 Schema,String Name,String SQL):base(Name) {
                this.Schema=Schema;
                this.SQL=SQL;
            }
            public TypeBuilder? TypeBuilder;
        }
        [DebuggerDisplay("{this."+nameof(Name)+"}")]
        private class View{
            /// <summary>
            /// エスケープ前
            /// </summary>
            private readonly String Name;
            /// <summary>
            /// エスケープ後
            /// </summary>
            public String EscapedName => Escape(this.Name);

            private readonly Schema? Schema;
            public readonly List<Column> ListColumn = new List<Column>();
            //public String SchemaTableName => this.Schema.EscapedName+'.'+this.EscapedName;
            public readonly String SQL;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="Schema"></param>
            /// <param name="Name">アセンブリの時の正式な識別子</param>
            /// <param name="SQL">VIEWの定義本体</param>
            public View(Schema Schema,String Name,String SQL) {
                this.Schema=Schema;
                this.Name=Name;
                this.SQL=SQL;
            }
            public TypeBuilder? TypeBuilder;
            //public ILGenerator ctor_I = null!;
            //public PropertyBuilder Property = null!;
            //public Type Type=null!;
            public override String ToString() => this.EscapedName;
        }
        [DebuggerDisplay("{this."+nameof(Name)+"}")]
        private class ForeignKey1:NameDependencyObject {
            public readonly Table1 Table;
            public readonly ObservableCollection<Column1> ListForeignKeyColumn=new ObservableCollection<Column1>();
            public ForeignKey1(String Name,Table1 Table):base(Name) {
                this.Table=Table;
            }
            public ForeignKey1? 対になるForeignKey1;
            public FieldBuilder? FieldBuilder;
            public LocalBuilder? 親LocalBuilder;
            public Boolean IsNullable {
                get {
                    foreach(var KeyColumn in this.ListForeignKeyColumn) {
                        if(KeyColumn.IsNullableClass||KeyColumn.Type.IsNullable()) {
                            return true;
                        }
                    }
                    return false;
                }
            }
        }
        [DebuggerDisplay("{this."+nameof(Name)+"}")]
        private class 親子:DependencyObject {
            private static readonly DependencyProperty NameProperty = DependencyProperty.Register(nameof(Name),typeof(String),typeof(親子));
            private String Name {
                get => (String)this.GetValue(NameProperty);
                set => this.SetValue(NameProperty,value);
            }
            public readonly ForeignKey1 親, 子;
            public 親子(String Name,ForeignKey1 親,ForeignKey1 子) {
                this.Name=Name;
                this.親=親;
                this.子=子;
            }
        }
        [DebuggerDisplay("{this."+nameof(Name)+"}")]
        private class ForeignKey {
            /// <summary>
            /// エスケープ前
            /// </summary>
            public readonly String Name;
            /// <summary>
            /// エスケープ後
            /// </summary>
            public String EscapedName => Escape(this.Name);
            public readonly Schema Schema;
            public readonly Table Table;
            public readonly List<Column> ListForeignKeyColumn;
            public String SchemaTableName => this.Schema.EscapedName+'.'+this.Table.EscapedName;
            public ForeignKey(String Name,Schema Schema,Table Table) {
                this.Name=Name;
                this.Schema=Schema;
                this.Table=Table;
                this.ListForeignKeyColumn=new List<Column>();
            }
            public FieldBuilder? FieldBuilder;
            public Boolean IsNullable;
            public override String ToString() => this.Schema+"."+this.Table+":"+this.Name;
        }
        [DebuggerDisplay("{this."+nameof(SQL)+"}")]
        private class Procedure1 {
            private readonly String SQL;
            public Procedure1(String SQL) {
                this.SQL=SQL;
            }
        }
        [DebuggerDisplay("{this."+nameof(SQL)+"}")]
        private class Procedure {
            private readonly String SQL;
            public Procedure(String SQL) {
                this.SQL=SQL;
            }
        }
        [DebuggerDisplay("{this.InsertSQL??\"\"+this.UpdateSQL??\"\"+this.DeleteSQL??\"\"}")]
        private class Trigger {
            public String? InsertSQL;
            public String? UpdateSQL;
            public String? DeleteSQL;
        }
        [DebuggerDisplay("{this."+nameof(Name)+"}")]
        private class Schema1:NameDependencyObject {
            public readonly ObservableCollection<Table1> Tables = new ObservableCollection<Table1>();
            public readonly ObservableCollection<View1> Views = new ObservableCollection<View1>();
            public readonly ObservableCollection<Procedure1> Procedures = new ObservableCollection<Procedure1>();
            public Schema1(String NormalizedName):base(NormalizedName) {
            }
            public FieldBuilder? FieldBuilder;
            public LocalBuilder? LocalBuilder;
        }
        [DebuggerDisplay("{this."+nameof(Name)+"}")]
        private class Schema {
            //AssemblyBuilderで使う
            public readonly String Name;
            public String EscapedName => Escape(this.Name);
            public readonly SortedDictionary<String,Table> SortedDictionaryTable = new SortedDictionary<String,Table>();
            public readonly SortedDictionary<String,View> SortedDictionaryView = new SortedDictionary<String,View>();
            public readonly SortedDictionary<String,Procedure> SortedDictionaryProcedure = new SortedDictionary<String,Procedure>();
            public Schema(String NormalizedName) {
                this.Name=NormalizedName;
            }
            public FieldBuilder? FieldBuilder;
            public LocalBuilder? LocalBuilder;
            public override String ToString() => this.EscapedName;
        }
        [DebuggerDisplay("{this."+nameof(Name)+"}")]
        private class NameDependencyObject:DependencyObject {
            private static readonly DependencyProperty NameProperty = DependencyProperty.Register(nameof(Name),typeof(String),typeof(NameDependencyObject));
            public String Name {
                get => (String)this.GetValue(NameProperty);
                set => this.SetValue(NameProperty,value);
            }
            public String EscapedName => Escape(this.Name);
            public NameDependencyObject(String Name) {
                this.Name=Name;
            }
        }
        [DebuggerDisplay("{this."+nameof(Name)+"}")]
        private class Catalog1:NameDependencyObject {
            private static readonly DependencyProperty DatabaseNameProperty = DependencyProperty.Register(nameof(DatabaseName),typeof(String),typeof(Catalog1));
            public String DatabaseName {
                get => (String)this.GetValue(DatabaseNameProperty);
                set => this.SetValue(DatabaseNameProperty,value);
            }
            public readonly ObservableCollection<Schema1> Schemas = new ObservableCollection<Schema1>();
            public readonly ObservableCollection<親子> 親子Collection = new ObservableCollection<親子>();
            public Catalog1(String Name,String DatabaseName):base(Name) {
                this.DatabaseName=DatabaseName;
            }
            public Type? Type;
        }
        [DebuggerDisplay("{this."+nameof(Name)+"}")]
        private class Catalog {
            //AssemblyBuilderで使う
            private readonly String Name;
            public String EscapedName => Escape(this.Name);
            public readonly SortedDictionary<String,Schema> SortedDictionarySchema;
            public readonly SortedDictionary<(String Schema, String Table),SortedDictionary<String,ForeignKey>> SortedDictionary_SelfChild;
            public readonly SortedDictionary<(String Schema, String Table),SortedDictionary<String,ForeignKey>> SortedDictionary_SelfParent;
            public Catalog(String Name) {
                this.Name=Name;
                this.SortedDictionarySchema=new SortedDictionary<String,Schema>();
                this.SortedDictionary_SelfChild=new SortedDictionary<(String Schema, String Table),SortedDictionary<String,ForeignKey>>();
                this.SortedDictionary_SelfParent=new SortedDictionary<(String Schema, String Table),SortedDictionary<String,ForeignKey>>();
            }
            public Type? Type;
        }
        private readonly SortedDictionary<
            String,
            Catalog
        > SortedDictionaryCotalog = new SortedDictionary<
            String,
            Catalog
        >();
        private readonly ObservableCollection<Catalog1> Catalogs = new ObservableCollection<Catalog1>();
        private static readonly MethodInfo InputT = typeof(CRC.CRC32).GetMethods().Single(p => p.Name=="Input"&&p.IsGenericMethod);
        private static readonly Type[] Types_Object = { typeof(Object) };
        //private static readonly Type[] Types_BinaryReader = { typeof(BinaryReader) };
        private static readonly Type[] Types_Stream = { typeof(Stream) };
        private static readonly Type[] Types_Stream_Stream = { typeof(Stream),typeof(Stream) };
        private static readonly Type[] Types_StringBuilder = { typeof(StringBuilder) };
        private static readonly MethodInfo Object_Equals = typeof(Object).GetMethod(nameof(Object.Equals),Types_Object)!;
        private static readonly MethodInfo Object_GetHashCode = typeof(Object).GetMethod(nameof(Object.GetHashCode))!;
        private static readonly MethodInfo Object_ToString = typeof(Object).GetMethod(nameof(Object.ToString))!;
        private static readonly MethodInfo StringBuilder_Append_String = typeof(StringBuilder).GetMethod(nameof(StringBuilder.Append),CommonLibrary.Types_String)!;
        private static readonly MethodInfo StringBuilder_AppendLine_String = typeof(StringBuilder).GetMethod(nameof(StringBuilder.AppendLine),CommonLibrary.Types_String)!;
        private static readonly MethodInfo StringBuilder_Append_Object = typeof(StringBuilder).GetMethod(nameof(StringBuilder.Append),Types_Object)!;
        private static readonly MethodInfo StringBuilder_AppendLine = typeof(StringBuilder).GetMethod(nameof(StringBuilder.AppendLine),Type.EmptyTypes)!;
        private static readonly MethodInfo Entity_InvalidateClearRelationship = typeof(Entity).GetMethod(nameof(Entity.InvalidateClearRelationship),BindingFlags.NonPublic|BindingFlags.Instance)!;
        private static (ConstructorBuilder ConstructorBuilder, ILGenerator I) コンストラクタ開始(TypeBuilder TypeBuilder,MethodAttributes MethodAttributes,Type[] Types) {
            var ctor = TypeBuilder.DefineConstructor(
                MethodAttributes,
                CallingConventions.HasThis,
                Types
            );
            ctor.InitLocals=false;
            var I = ctor.GetILGenerator();
            return (ctor, I);
        }
        private static (ConstructorBuilder ConstructorBuilder, ILGenerator I) コンストラクタ開始引数名(TypeBuilder TypeBuilder,MethodAttributes MethodAttributes,Type[] Types,String 引数名) {
            var (ctor, I)=コンストラクタ開始(TypeBuilder,MethodAttributes,Types);
            ctor.DefineParameter(1,ParameterAttributes.None,引数名);
            return (ctor, I);
        }
        private static (MethodBuilder MethodBuilder, ILGenerator I) メソッド開始(TypeBuilder TypeBuilder,String メソッド名,MethodAttributes MethodAttributes,Type ReturnType) {
            var MethodBuilder = TypeBuilder.DefineMethod(
                メソッド名,
                MethodAttributes,
                ReturnType,
                Type.EmptyTypes
            );
            MethodBuilder.InitLocals=false;
            return (MethodBuilder, MethodBuilder.GetILGenerator());
        }
        private static (MethodBuilder MethodBuilder, ILGenerator I) メソッド開始引数名(TypeBuilder TypeBuilder,String メソッド名,MethodAttributes MethodAttributes,Type ReturnType,Type[] Types,String 引数名) {
            var MethodBuilder = TypeBuilder.DefineMethod(
                メソッド名,
                MethodAttributes,
                ReturnType,
                Types
            );
            MethodBuilder.InitLocals=false;
            MethodBuilder.DefineParameter(1,ParameterAttributes.None,引数名);
            return (MethodBuilder, MethodBuilder.GetILGenerator());
        }
        private static MethodBuilder AddRelationship開始(TypeBuilder Type,String メソッド名,Type[] Types1) {
            var AddRelationship = Type.DefineMethod(
                メソッド名,
                MethodAttributes.Family|MethodAttributes.HideBySig|MethodAttributes.Virtual,
                typeof(void),
                Types1
            );
            AddRelationship.InitLocals=false;
            AddRelationship.DefineParameter(1,ParameterAttributes.None,"Container");
            Type.DefineMethodOverride(
                AddRelationship,
                TypeBuilder.GetMethod(
                    typeof(Entity<>).MakeGenericType(Types1),
                    typeof(Entity<>).GetMethod(メソッド名,BindingFlags.NonPublic|BindingFlags.Instance)
                )
            );
            return AddRelationship;
        }
        private static MethodBuilder RemoveRelationship開始(TypeBuilder TypeBulder,String メソッド名,Type[] Types1) {
            var RemoveRelation = TypeBulder.DefineMethod(
                メソッド名,
                MethodAttributes.Family|MethodAttributes.HideBySig|MethodAttributes.Virtual,
                typeof(void),
                Type.EmptyTypes
            );
            RemoveRelation.InitLocals=false;
            TypeBulder.DefineMethodOverride(
                RemoveRelation,
                TypeBuilder.GetMethod(
                    typeof(Entity<>).MakeGenericType(Types1),
                    typeof(Entity<>).GetMethod(メソッド名,BindingFlags.NonPublic|BindingFlags.Instance)
                )
            );
            return RemoveRelation;
        }
        private static void 共通override_IEquatable_Equalsメソッド終了(ILGenerator I,Label IEquatable_Equalsでfalseの時) {
            I.Ldc_I4_1();
            I.Ret();
            I.MarkLabel(IEquatable_Equalsでfalseの時);
            I.Ldc_I4_0();
            I.Ret();
        }
        private static ILGenerator 共通struct_op_Equality_Inequality(TypeBuilder TypeBulder,MethodBuilder Equals,String メソッド名,Type[] Types) {
            var opEquality = TypeBulder.DefineMethod(
                メソッド名,
                MethodAttributes.Public|MethodAttributes.Static|MethodAttributes.SpecialName|MethodAttributes.HideBySig,
                typeof(Boolean),
                Types
            );
            opEquality.InitLocals=false;
            opEquality.DefineParameter(1,ParameterAttributes.None,"a");
            opEquality.DefineParameter(2,ParameterAttributes.None,"b");
            var I = opEquality.GetILGenerator();
            I.Ldarga_S(0);
            I.Ldarg_1();
            I.Call(Equals);
            return I;
        }
        private static void 共通override_struct_Object_Equals終了(TypeBuilder TypeBuilder,MethodBuilder IEquatable_Equals,OpCode キャストOpCode) {
            var Type_Equals = TypeBuilder.DefineMethod(
                nameof(Object.Equals),
                MethodAttributes.Public|MethodAttributes.Virtual|MethodAttributes.HideBySig,
                typeof(Boolean),
                Types_Object
            );
            TypeBuilder.DefineMethodOverride(
                Type_Equals,
                Object_Equals
            );
            Type_Equals.InitLocals=false;
            Type_Equals.DefineParameter(1,ParameterAttributes.None,"other");
            var I = Type_Equals.GetILGenerator();
            I.Ldarg_1();
            I.Emit(キャストOpCode,TypeBuilder);
            var 変数 = I.M_DeclareLocal_Stloc_Ldloc(TypeBuilder);
            var nullの時 = I.DefineLabel();
            I.Brfalse_S(nullの時);
            I.Ldarg_0();
            I.Ldloc(変数);
            I.Callvirt(IEquatable_Equals);
            var 終了 = I.DefineLabel();
            I.Br_S(終了);
            I.MarkLabel(nullの時);
            I.Ldc_I4_0();
            I.MarkLabel(終了);
            I.Ret();
        }
        private static void 共通override_Object_Equals終了(TypeBuilder TypeBuilder,MethodBuilder IEquatable_Equals,OpCode キャストOpCode) {
            var Type_Equals = TypeBuilder.DefineMethod(
                nameof(Object.Equals),
                MethodAttributes.Public|MethodAttributes.Virtual|MethodAttributes.HideBySig,
                typeof(Boolean),
                Types_Object
            );
            TypeBuilder.DefineMethodOverride(
                Type_Equals,
                Object_Equals
            );
            Type_Equals.InitLocals=false;
            Type_Equals.DefineParameter(1,ParameterAttributes.None,"other");
            var I=Type_Equals.GetILGenerator();
            I.Ldarg_1();
            I.Emit(キャストOpCode,TypeBuilder);
            var 変数=I.M_DeclareLocal_Stloc_Ldloc(TypeBuilder);
            var nullの時 =I.DefineLabel();
            I.Brfalse_S(nullの時);
            I.Ldarg_0();
            I.Ldloc(変数);
            I.Callvirt(IEquatable_Equals);
            var 終了 = I.DefineLabel();
            I.Br_S(終了);
            I.MarkLabel(nullの時);
            I.Ldc_I4_0();
            I.MarkLabel(終了);
            I.Ret();
        }
        private void 共通Equals(Boolean IsNullableClass,FieldBuilder Field,ILGenerator I,Label Equalsでfalseの時) {
            //Left==Right→計算
            //Left==null→false
            //null==Right→false
            //null==null→true
            I.Ldarg_0();
            var FieldType = Field.FieldType;
            var Types1 = this.Types1;
            //Debug.Assert(Reflection.Object.Equals_==Equals.GetBaseDefinition()&&Equals.DeclaringType.IsSubclassOf(typeof(Object)));
            if(FieldType.IsValueType) {
                //LinqDB.Helpers.CommonLibrary.IsNullable()
                if(FieldType.IsNullable()) {
                    var GetValueOrDefault = FieldType.GetMethod(nameof(Nullable<Int32>.GetValueOrDefault),Type.EmptyTypes)!;
                    var get_HasValue = FieldType.GetProperty(nameof(Nullable<Int32>.HasValue))!.GetMethod;
                    //Stringにはoverride bool Equals(string)が存在してしまっている
                    I.Ldfld(Field);
                    var Left =I.M_DeclareLocal_Stloc(FieldType);
                    I.Ldloca(Left);
                    I.Call(GetValueOrDefault);
                    var GetValueOrDefault_ReturnType=GetValueOrDefault.ReturnType;
                    var 変数 = I.DeclareLocal(GetValueOrDefault_ReturnType);
                    I.Stloc(変数);
                    I.Ldloca(変数);
                    I.Ldarg_1();
                    I.Ldfld(Field);
                    var Right=I.M_DeclareLocal_Stloc(FieldType);
                    I.Ldloca(Right);
                    I.Call(GetValueOrDefault);
                    Types1[0]=GetValueOrDefault_ReturnType;
                    I.Call(GetValueOrDefault_ReturnType.GetMethod(nameof(Object.Equals),Types1));
                    var 一致した = I.DefineLabel();
                    I.Brtrue_S(一致した);
                    I.Br(Equalsでfalseの時);
                    I.MarkLabel(一致した);
                    I.Ldloca(Left);
                    I.Call(get_HasValue);
                    I.Ldloca(Right);
                    I.Call(get_HasValue);
                    I.And();
                    I.Brfalse(Equalsでfalseの時);
                } else {
                    Types1[0]=FieldType;
                    //Stringにはoverride bool Equals(string)が存在してしまっている
                    var Equals = FieldType.GetMethod(nameof(Object.Equals),Types1);
                    I.Ldflda(Field);
                    I.Ldarg_1();
                    I.Ldfld(Field);
                    if(Reflection.Object.Equals_==Equals) {
                        I.Box(FieldType);
                        I.Constrained(FieldType);
                        I.Callvirt(Equals);
                    } else {
                        I.Call(Equals);
                    }
                    I.Brfalse(Equalsでfalseの時);
                }
            } else {
                Types1[0]=FieldType;
                //Stringにはoverride bool Equals(string)が存在してしまっている
                var Equals = FieldType.GetMethod(nameof(Object.Equals),Types1);
                I.Ldfld(Field);
                if(IsNullableClass) {
                    var L = I.M_DeclareLocal_Stloc_Ldloc(FieldType);
                    I.Ldarg_1();
                    I.Ldfld(Field);
                    var R= I.M_DeclareLocal_Stloc_Ldloc(FieldType);
                    //if(L==R)goto 次の処理
                    var 次の処理 = I.DefineLabel();
                    I.Beq(次の処理);
                    I.Ldloc(L);
                    I.Ldloc(R);
                    //if(L.Equals(R))goto 次の処理
                    I.Callvirt(Equals);
                    I.Brfalse(Equalsでfalseの時);
                    I.MarkLabel(次の処理);
                } else {
                    I.Ldarg_1();
                    I.Ldfld(Field);
                    I.Callvirt(Equals);
                    I.Brfalse(Equalsでfalseの時);
                }
            }
        }
        private LocalBuilder 共通AddRelationship0(
            FieldInfo Entity2_InternalPrimaryKey,
            ILGenerator AddRelationship_I,
            ILGenerator RemoveRelationship_I,
            Table1 子Table,
            Table1 親Table,
            ForeignKey1 親ForeignKey,
            MethodInfo Set2_TryGetValue,
            MethodInfo Set1_VoidRemove
        ) {
            var 子ForeignKey = 親ForeignKey.対になるForeignKey1!;
            var 子Table_Key_TypeBuilder = 子Table.Key_TypeBuilder!;
            Label 親タプルにNULLを代入して正常進行;
            var 正常進行 = AddRelationship_I.DefineLabel();
            var 親ForeignKey_Table = 親ForeignKey.Table;
            var 親LocalBuilder = AddRelationship_I.DeclareLocal(親Table.TypeBuilder!);
            var 自己参照か = 親ForeignKey_Table==子ForeignKey.Table;
            if(親ForeignKey.IsNullable) {
                親タプルにNULLを代入して正常進行=AddRelationship_I.DefineLabel();
                //外部キーを構成する属性がNULLだった場合親は存在しなくてもよいので飛ばす。
                foreach(var 親ForeignKeyColumn in 親ForeignKey.ListForeignKeyColumn) {
                    var 親ForeignKeyColumn_FieldBuilder = 親ForeignKeyColumn.FieldBuilder!;
                    var 親ForeignKeyColumn_FieldBuilder_FieldType = 親ForeignKeyColumn_FieldBuilder.FieldType;
                    if(親ForeignKeyColumn.IsNullableClass) {
                        AddRelationship_I.Ldarg_0();
                        AddRelationship_I.Ldfld(親ForeignKeyColumn_FieldBuilder);
                        AddRelationship_I.Brfalse(親タプルにNULLを代入して正常進行);
                    } else if(親ForeignKeyColumn_FieldBuilder_FieldType.IsNullable()) {
                        AddRelationship_I.Ldarg_0();
                        AddRelationship_I.Ldflda(親ForeignKeyColumn_FieldBuilder);
                        AddRelationship_I.Call(親ForeignKeyColumn_FieldBuilder_FieldType.GetProperty(nameof(Nullable<Int32>.HasValue))!.GetMethod);
                        AddRelationship_I.Brfalse(親タプルにNULLを代入して正常進行);
                    }
                }
            } else {
                親タプルにNULLを代入して正常進行=default;
                foreach(var ForeignKeyColumn in 親ForeignKey.ListForeignKeyColumn) {
                    Debug.Assert(!ForeignKeyColumn.IsNullableClass);
                    Debug.Assert(!ForeignKeyColumn.FieldBuilder!.FieldType.IsNullable());
                }
            }
            if(自己参照か) {
                AddRelationship_I.Ldarg_0();
                AddRelationship_I.Ldflda(Entity2_InternalPrimaryKey);
                foreach(var 親ForeignKeyColumn in 親ForeignKey.ListForeignKeyColumn) {
                    var 親ForeignKeyColumn_FieldBuilder = 親ForeignKeyColumn.FieldBuilder!;
                    var 親ForeignKeyColumn_FieldBuilder_FieldType = 親ForeignKeyColumn_FieldBuilder.FieldType;
                    AddRelationship_I.Ldarg_0();
                    if(親ForeignKeyColumn_FieldBuilder_FieldType.IsNullable()) {
                        AddRelationship_I.Ldflda(親ForeignKeyColumn_FieldBuilder);
                        AddRelationship_I.Call(親ForeignKeyColumn_FieldBuilder_FieldType.GetMethod(nameof(Nullable<Int32>.GetValueOrDefault),Type.EmptyTypes));
                    } else {
                        AddRelationship_I.Ldfld(親ForeignKeyColumn_FieldBuilder);
                    }
                }
                AddRelationship_I.Newobj(親ForeignKey_Table.Key_ctor!);
                AddRelationship_I.Call(子Table.Key_Equals!);
                var Equalsでfalseの時 = AddRelationship_I.DefineLabel();
                AddRelationship_I.Brfalse(Equalsでfalseの時);
                AddRelationship_I.Ldarg_0();
                AddRelationship_I.Stloc(親LocalBuilder);
                AddRelationship_I.Br(正常進行);
                AddRelationship_I.MarkLabel(Equalsでfalseの時);
            }
            AddRelationship_I.Ldarg_1();//Container_TypeBuilder
            AddRelationship_I.Ldfld(親Table.Schema.FieldBuilder!);//dbo
            AddRelationship_I.Ldfld(親Table.FieldBuilder!);//customer
            foreach(var ForeignKeyColumn in 親ForeignKey.ListForeignKeyColumn) {
                AddRelationship_I.Ldarg_0();
                var ForeignKeyColumn外部キー列_FieldBuilder = ForeignKeyColumn.FieldBuilder!;
                if(ForeignKeyColumn外部キー列_FieldBuilder.DeclaringType==子Table_Key_TypeBuilder) {
                    AddRelationship_I.Ldflda(Entity2_InternalPrimaryKey);
                }
                var 外部キー列_FieldBuilder_FieldType = ForeignKeyColumn外部キー列_FieldBuilder.FieldType;
                if(外部キー列_FieldBuilder_FieldType.IsNullable()) {
                    AddRelationship_I.Ldflda(ForeignKeyColumn外部キー列_FieldBuilder);
                    AddRelationship_I.Call(外部キー列_FieldBuilder_FieldType.GetMethod(nameof(Nullable<Int32>.GetValueOrDefault),Type.EmptyTypes));
                } else {
                    AddRelationship_I.Ldfld(ForeignKeyColumn外部キー列_FieldBuilder);
                }
            }
            AddRelationship_I.Newobj(親Table.Key_ctor!);
            var sb = this.sb;
            sb.Clear();
            sb.Append($"[{子Table.Name}].[");
            foreach(var ForeignKeyColumn in 親ForeignKey.ListForeignKeyColumn) {
                sb.Append($"{ForeignKeyColumn.Name},");
            }
            sb.Length--;
            sb.Append($"]に対応するタプルが[{親Table.Name}]に存在しなかった。");
            AddRelationship_I.Ldloca(親LocalBuilder);
            AddRelationship_I.Call(Set2_TryGetValue);
            AddRelationship_I.Brtrue_S(正常進行);
            AddRelationship_I.Ldstr(sb.ToString());
            AddRelationship_I.Newobj(Reflection.Exception.RelationshipException_ctor);
            AddRelationship_I.Throw();
            if(親ForeignKey.IsNullable) {
                AddRelationship_I.MarkLabel(親タプルにNULLを代入して正常進行);
                AddRelationship_I.Ldnull();
                AddRelationship_I.Stloc(親LocalBuilder);
            }
            AddRelationship_I.MarkLabel(正常進行);
            //AddRelationship_I.Ldarg_0();
            //AddRelationship_I.Ldloc(親LocalBuilder);
            //AddRelationship_I.Stfld(子ForeignKey.FieldBuilder!);
            //子.Count>0だと例外を発生させる
            //this→子→thisを削除
            if(親ForeignKey.IsNullable) {
                RemoveRelationship_I.Ldarg_0();
                RemoveRelationship_I.Ldfld(子ForeignKey.FieldBuilder!);
                var スキップ = RemoveRelationship_I.DefineLabel();
                RemoveRelationship_I.Brfalse(スキップ);
                親タプルの子Setから自身を削除();
                RemoveRelationship_I.MarkLabel(スキップ);
            } else {
                親タプルの子Setから自身を削除();
            }
            return 親LocalBuilder;
            void 親タプルの子Setから自身を削除() {
                //RemoveRelationship_I.Ldarg_0();
                //RemoveRelationship_I.Ldfld(子ForeignKey.FieldBuilder!);
                //RemoveRelationship_I.Ldfld(親ForeignKey.FieldBuilder!);
                //RemoveRelationship_I.Ldarg_0();
                //RemoveRelationship_I.Call(Set1_VoidRemove);
            }
        }
        private void 共通AddRelationship0(
            ILGenerator AddRelationship_I,
            ForeignKey1 子ForeignKey,
            LocalBuilder 親タプル
        ) {
            AddRelationship_I.Ldarg_0();
            AddRelationship_I.Ldloc(親タプル);
            AddRelationship_I.Stfld(子ForeignKey.FieldBuilder!);
        }
        private LocalBuilder 共通AddRelationship1(
            FieldInfo Entity2_InternalPrimaryKey,
            ILGenerator AddRelationship_I,
            ILGenerator RemoveRelationship_I,
            //Table1 自Table,
            Table1 子Table,
            Table1 親Table,
            ForeignKey1 親ForeignKey,
            MethodInfo Set2_TryGetValue,
            MethodInfo Set1_VoidRemove
        ) {
            var 子ForeignKey = 親ForeignKey.対になるForeignKey1!;
            var 子Table_Key_TypeBuilder = 子Table.Key_TypeBuilder!;
            //var Types2 = this.Types2;
            Label 親タプルにNULLを代入して正常進行;
            var 正常進行 = AddRelationship_I.DefineLabel();
            var 親ForeignKey_Table = 親ForeignKey.Table;
            var 親タプル = AddRelationship_I.DeclareLocal(親Table.TypeBuilder!);
            var 自己参照か = 親ForeignKey_Table==子ForeignKey.Table;
            if(親ForeignKey.IsNullable) {
                親タプルにNULLを代入して正常進行=AddRelationship_I.DefineLabel();
                //外部キーを構成する属性がNULLだった場合親は存在しなくてもよいので飛ばす。
                foreach(var 親ForeignKeyColumn in 親ForeignKey.ListForeignKeyColumn) {
                    var 親ForeignKeyColumn_FieldBuilder = 親ForeignKeyColumn.FieldBuilder!;
                    var 親ForeignKeyColumn_FieldBuilder_FieldType = 親ForeignKeyColumn_FieldBuilder.FieldType;
                    if(親ForeignKeyColumn.IsNullableClass) {
                        AddRelationship_I.Ldarg_0();
                        AddRelationship_I.Ldfld(親ForeignKeyColumn_FieldBuilder);
                        AddRelationship_I.Brfalse(親タプルにNULLを代入して正常進行);
                    } else if(親ForeignKeyColumn_FieldBuilder_FieldType.IsNullable()) {
                        AddRelationship_I.Ldarg_0();
                        AddRelationship_I.Ldflda(親ForeignKeyColumn_FieldBuilder);
                        AddRelationship_I.Call(親ForeignKeyColumn_FieldBuilder_FieldType.GetProperty(nameof(Nullable<Int32>.HasValue))!.GetMethod);
                        AddRelationship_I.Brfalse(親タプルにNULLを代入して正常進行);
                    }
                }
            } else {
                親タプルにNULLを代入して正常進行=default;
                foreach(var ForeignKeyColumn in 親ForeignKey.ListForeignKeyColumn) {
                    Debug.Assert(!ForeignKeyColumn.IsNullableClass);
                    Debug.Assert(!ForeignKeyColumn.FieldBuilder!.FieldType.IsNullable());
                }
            }
            if(自己参照か) {
                AddRelationship_I.Ldarg_0();
                AddRelationship_I.Ldflda(Entity2_InternalPrimaryKey);
                foreach(var 親ForeignKeyColumn in 親ForeignKey.ListForeignKeyColumn) {
                    var 親ForeignKeyColumn_FieldBuilder = 親ForeignKeyColumn.FieldBuilder!;
                    var 親ForeignKeyColumn_FieldBuilder_FieldType = 親ForeignKeyColumn_FieldBuilder.FieldType;
                    AddRelationship_I.Ldarg_0();
                    if(親ForeignKeyColumn_FieldBuilder_FieldType.IsNullable()) {
                        AddRelationship_I.Ldflda(親ForeignKeyColumn_FieldBuilder);
                        AddRelationship_I.Call(親ForeignKeyColumn_FieldBuilder_FieldType.GetMethod(nameof(Nullable<Int32>.GetValueOrDefault),Type.EmptyTypes));
                    } else {
                        AddRelationship_I.Ldfld(親ForeignKeyColumn_FieldBuilder);
                    }
                }
                AddRelationship_I.Newobj(親ForeignKey_Table.Key_ctor!);
                AddRelationship_I.Call(子Table.Key_Equals!);
                var Equalsでfalseの時 = AddRelationship_I.DefineLabel();
                AddRelationship_I.Brfalse(Equalsでfalseの時);
                AddRelationship_I.Ldarg_0();
                AddRelationship_I.Stloc(親タプル);
                AddRelationship_I.Br(正常進行);
                AddRelationship_I.MarkLabel(Equalsでfalseの時);
            }
            AddRelationship_I.Ldarg_1();//Container_TypeBuilder
            AddRelationship_I.Ldfld(親Table.Schema.FieldBuilder!);//dbo
            AddRelationship_I.Ldfld(親Table.FieldBuilder!);//customer
            foreach(var ForeignKeyColumn in 親ForeignKey.ListForeignKeyColumn) {
                AddRelationship_I.Ldarg_0();
                var ForeignKeyColumn外部キー列_FieldBuilder = ForeignKeyColumn.FieldBuilder!;
                if(ForeignKeyColumn外部キー列_FieldBuilder.DeclaringType==子Table_Key_TypeBuilder) {
                    AddRelationship_I.Ldflda(Entity2_InternalPrimaryKey);
                }
                var 外部キー列_FieldBuilder_FieldType = ForeignKeyColumn外部キー列_FieldBuilder.FieldType;
                if(外部キー列_FieldBuilder_FieldType.IsNullable()) {
                    AddRelationship_I.Ldflda(ForeignKeyColumn外部キー列_FieldBuilder);
                    AddRelationship_I.Call(外部キー列_FieldBuilder_FieldType.GetMethod(nameof(Nullable<Int32>.GetValueOrDefault),Type.EmptyTypes));
                } else {
                    AddRelationship_I.Ldfld(ForeignKeyColumn外部キー列_FieldBuilder);
                }
            }
            AddRelationship_I.Newobj(親ForeignKey_Table.Key_ctor!);
            var sb = this.sb;
            sb.Clear();
            sb.Append($"[{子Table.Name}].[");
            foreach(var ForeignKeyColumn in 親ForeignKey.ListForeignKeyColumn) {
                sb.Append($"{ForeignKeyColumn.Name},");
            }
            sb.Length--;
            sb.Append($"]に対応するタプルが[{親Table.Name}]に存在しなかった。");
            AddRelationship_I.Ldloca(親タプル);
            AddRelationship_I.Call(Set2_TryGetValue);
            AddRelationship_I.Brtrue_S(正常進行);
            AddRelationship_I.Ldstr(sb.ToString());
            AddRelationship_I.Newobj(Reflection.Exception.RelationshipException_ctor);
            AddRelationship_I.Throw();
            if(親タプルにNULLを代入して正常進行!=null) {
                AddRelationship_I.MarkLabel(親タプルにNULLを代入して正常進行);
                AddRelationship_I.Ldnull();
                AddRelationship_I.Stloc(親タプル);
            }
            AddRelationship_I.MarkLabel(正常進行);
            AddRelationship_I.Ldarg_0();
            AddRelationship_I.Ldloc(親タプル);
            AddRelationship_I.Stfld(子ForeignKey.FieldBuilder!);
            //子.Count>0だと例外を発生させる
            //this→子→thisを削除
            if(親ForeignKey.IsNullable) {
                RemoveRelationship_I.Ldarg_0();
                RemoveRelationship_I.Ldfld(子ForeignKey.FieldBuilder!);
                var スキップ = RemoveRelationship_I.DefineLabel();
                RemoveRelationship_I.Brfalse(スキップ);
                親タプルの子Setから自身を削除();
                RemoveRelationship_I.MarkLabel(スキップ);
            } else {
                親タプルの子Setから自身を削除();
            }
            return 親タプル;
            void 親タプルの子Setから自身を削除() {
                RemoveRelationship_I.Ldarg_0();
                RemoveRelationship_I.Ldfld(子ForeignKey.FieldBuilder!);
                RemoveRelationship_I.Ldfld(親ForeignKey.FieldBuilder!);
                RemoveRelationship_I.Ldarg_0();
                RemoveRelationship_I.Call(Set1_VoidRemove);
            }
        }
        private LocalBuilder 共通AddRelationship0(
            FieldInfo Entity2_InternalPrimaryKey,
            ILGenerator AddRelationship_I,
            ILGenerator RemoveRelationship_I,
            Table 自Table,
            FieldBuilder 親Schema_FieldBuilder,
            Table 親Table,
            ForeignKey 親ForeignKey,
            ForeignKey 子ForeignKey,
            MethodInfo Set2_TryGetValue,
            MethodInfo Set1_VoidRemove
        ) {
            var 自Table_Key_TypeBuilder = 自Table.Key_TypeBuilder!;
            //var Types2 = this.Types2;
            var 親タプルにNULLを代入して正常進行 = AddRelationship_I.DefineLabel();
            var 正常進行 = AddRelationship_I.DefineLabel();
            var 親ForeignKey_Table =親ForeignKey.Table;
            var 親タプル= AddRelationship_I.DeclareLocal(親Table.TypeBuilder!);
            var 自己参照か = 親ForeignKey_Table==子ForeignKey.Table;
            if(親ForeignKey.IsNullable) {
                //外部キーを構成する属性がNULLだった場合親は存在しなくてもよいので飛ばす。
                foreach(var ForeignKeyColumn in 親ForeignKey.ListForeignKeyColumn) {
                    var ForeignKeyColumn_FieldBuilder = ForeignKeyColumn.FieldBuilder!;
                    var ForeignKeyColumn_FieldBuilder_FieldType = ForeignKeyColumn_FieldBuilder.FieldType;
                    if(ForeignKeyColumn.IsNullableClass) {
                        AddRelationship_I.Ldarg_0();
                        AddRelationship_I.Ldfld(ForeignKeyColumn_FieldBuilder);
                        AddRelationship_I.Brfalse(親タプルにNULLを代入して正常進行);
                    } else if(ForeignKeyColumn_FieldBuilder_FieldType.IsNullable()) {
                        AddRelationship_I.Ldarg_0();
                        AddRelationship_I.Ldflda(ForeignKeyColumn_FieldBuilder);
                        AddRelationship_I.Call(ForeignKeyColumn_FieldBuilder_FieldType.GetProperty(nameof(Nullable<Int32>.HasValue))!.GetMethod);
                        AddRelationship_I.Brfalse(親タプルにNULLを代入して正常進行);
                    }
                }
            } else {
                foreach(var ForeignKeyColumn in 親ForeignKey.ListForeignKeyColumn) {
                    Debug.Assert(!ForeignKeyColumn.IsNullableClass);
                    Debug.Assert(!ForeignKeyColumn.FieldBuilder!.FieldType.IsNullable());
                }
            }
            if(自己参照か) {
                AddRelationship_I.Ldarg_0();
                AddRelationship_I.Ldflda(Entity2_InternalPrimaryKey);
                foreach(var ForeignKeyColumn in 親ForeignKey.ListForeignKeyColumn) {
                    var ForeignKeyColumn_FieldBuilder = ForeignKeyColumn.FieldBuilder!;
                    var ForeignKeyColumn_FieldBuilder_FieldType = ForeignKeyColumn_FieldBuilder.FieldType;
                    AddRelationship_I.Ldarg_0();
                    if(ForeignKeyColumn_FieldBuilder_FieldType.IsNullable()) {
                        AddRelationship_I.Ldflda(ForeignKeyColumn_FieldBuilder);
                        AddRelationship_I.Call(ForeignKeyColumn_FieldBuilder_FieldType.GetMethod(nameof(Nullable<Int32>.GetValueOrDefault),Type.EmptyTypes));
                    } else {
                        AddRelationship_I.Ldfld(ForeignKeyColumn_FieldBuilder);
                    }
                }
                AddRelationship_I.Newobj(親ForeignKey_Table.Key_ctor!);
                AddRelationship_I.Call(自Table.Key_Equals!);
                var Equalsでfalseの時 = AddRelationship_I.DefineLabel();
                AddRelationship_I.Brfalse(Equalsでfalseの時);
                AddRelationship_I.Ldarg_0();
                AddRelationship_I.Stloc(親タプル);
                AddRelationship_I.Br(正常進行);
                AddRelationship_I.MarkLabel(Equalsでfalseの時);
            }
            AddRelationship_I.Ldarg_1();//Container_TypeBuilder
            AddRelationship_I.Ldfld(親Schema_FieldBuilder);//dbo
            AddRelationship_I.Ldfld(親Table.FieldBuilder!);//customer
            foreach(var ForeignKeyColumn in 親ForeignKey.ListForeignKeyColumn) {
                AddRelationship_I.Ldarg_0();
                var ForeignKeyColumn外部キー列_FieldBuilder = ForeignKeyColumn.FieldBuilder!;
                if(ForeignKeyColumn外部キー列_FieldBuilder.DeclaringType==自Table_Key_TypeBuilder) {
                    AddRelationship_I.Ldflda(Entity2_InternalPrimaryKey);
                }
                var 外部キー列_FieldBuilder_FieldType = ForeignKeyColumn外部キー列_FieldBuilder.FieldType;
                if(外部キー列_FieldBuilder_FieldType.IsNullable()) {
                    AddRelationship_I.Ldflda(ForeignKeyColumn外部キー列_FieldBuilder);
                    AddRelationship_I.Call(外部キー列_FieldBuilder_FieldType.GetMethod(nameof(Nullable<Int32>.GetValueOrDefault),Type.EmptyTypes));
                } else {
                    AddRelationship_I.Ldfld(ForeignKeyColumn外部キー列_FieldBuilder);
                }
            }
            AddRelationship_I.Newobj(親ForeignKey_Table.Key_ctor!);
            var sb = this.sb;
            sb.Clear();
            sb.Append($"[{自Table.Name}].[");
            foreach(var ForeignKeyColumn in 親ForeignKey.ListForeignKeyColumn) {
                sb.Append($"{ForeignKeyColumn.Name},");
            }
            sb.Length--;
            sb.Append($"]に対応するタプルが[{親Table.Name}]に存在しなかった。");
            AddRelationship_I.Ldloca(親タプル);
            AddRelationship_I.Call(Set2_TryGetValue);
            AddRelationship_I.Brtrue_S(正常進行);
            AddRelationship_I.Ldstr(sb.ToString());
            AddRelationship_I.Newobj(Reflection.Exception.RelationshipException_ctor);
            AddRelationship_I.Throw();
            AddRelationship_I.MarkLabel(親タプルにNULLを代入して正常進行);
            AddRelationship_I.Ldnull();
            AddRelationship_I.Stloc(親タプル);
            AddRelationship_I.MarkLabel(正常進行);

            AddRelationship_I.Ldarg_0();
            AddRelationship_I.Ldloc(親タプル);
            AddRelationship_I.Stfld(親ForeignKey.FieldBuilder!);

            //子.Count>0だと例外を発生させる
            //this→子→thisを削除
            if(親ForeignKey.IsNullable) {
                RemoveRelationship_I.Ldarg_0();
                RemoveRelationship_I.Ldfld(親ForeignKey.FieldBuilder!);
                var スキップ=RemoveRelationship_I.DefineLabel();
                RemoveRelationship_I.Brfalse(スキップ);
                親タプルの子Setから自身を削除();
                RemoveRelationship_I.MarkLabel(スキップ);
            } else {
                親タプルの子Setから自身を削除();
            }
            return 親タプル;
            void 親タプルの子Setから自身を削除() {
                RemoveRelationship_I.Ldarg_0();
                RemoveRelationship_I.Ldfld(親ForeignKey.FieldBuilder!);
                RemoveRelationship_I.Ldfld(子ForeignKey.FieldBuilder!);
                RemoveRelationship_I.Ldarg_0();
                RemoveRelationship_I.Call(Set1_VoidRemove);
            }
        }
        private const String op_Equality = nameof(op_Equality);
        private const String op_Inequality = nameof(op_Inequality);
        private static readonly ConstructorInfo DifinitionContainer_ctor0 = typeof(Container<>).GetConstructor(BindingFlags.Instance|BindingFlags.NonPublic,null,Type.EmptyTypes,null)!;
        private static readonly ConstructorInfo DifinitionContainer_ctor1 = typeof(Container<>).GetConstructor(BindingFlags.Instance|BindingFlags.NonPublic,null,new[] { typeof(Container<>).GetGenericArguments()[0] },null)!;
        private static readonly ConstructorInfo DifinitionContainer_ctor2 = typeof(Container<>).GetConstructor(BindingFlags.Instance|BindingFlags.NonPublic,null,Types_Stream_Stream,null)!;
        private static readonly MethodInfo DifinitionContainer_Transaction = typeof(Container<>).GetMethod("Transaction",BindingFlags.Instance|BindingFlags.Public)!;
        private static readonly MethodInfo DifinitionContainer_Copy = typeof(Container<>).GetMethod("Copy",BindingFlags.Instance|BindingFlags.NonPublic)!;
        //private static readonly MethodInfo DifinitionImmutableSet_Read = typeof(ImmutableSet<>).GetMethod(nameof(ImmutableSet<Int32>.Read))!;
        //private static readonly MethodInfo DifinitionImmutableSet_Write = typeof(ImmutableSet<>).GetMethod(nameof(ImmutableSet<Int32>.Write))!;
        private class Entity1:Entity<Int32,Container>, IWriteRead<Entity1> {
            public Entity1(Int32 PrimaryKey) : base(PrimaryKey) { }
            public void BinaryRead(BinaryReader Reader,Func<Entity1> Create) => throw new NotImplementedException();
            public void BinaryWrite(BinaryWriter Writer) => throw new NotImplementedException();
            public void TextRead(StreamReader Reader,Int32 Indent) => throw new NotImplementedException();
            public void TextWrite(IndentedTextWriter Writer) => throw new NotImplementedException();
        }
        private static readonly MethodInfo DifinitionSet3_BinaryRead = typeof(Set<,,>).GetMethod(nameof(Set<Entity1,Int32,Container>.BinaryRead))!;
        private static readonly MethodInfo DifinitionSet3_BinaryWrite = typeof(Set<,,>).GetMethod(nameof(Set<Entity1,Int32,Container>.BinaryWrite))!;
        private static readonly MethodInfo DifinitionSet_Assign = typeof(Set<>).GetMethod(nameof(Set<Int32>.Assign))!;
        private static readonly MethodInfo DifinitionSet_Clear = typeof(Set<>).GetMethod(nameof(Set<Int32>.Clear))!;
        private static readonly MethodInfo Container_Init = typeof(Container).GetMethod("Init",BindingFlags.Instance|BindingFlags.NonPublic)!;
        private static readonly MethodInfo Container_Read = typeof(Container).GetMethod("Read",BindingFlags.Instance|BindingFlags.NonPublic)!;
        private static readonly MethodInfo Container_Write = typeof(Container).GetMethod("Write",BindingFlags.Instance|BindingFlags.NonPublic)!;
        private static readonly MethodInfo Container_UpdateRelationship = typeof(Container).GetMethod("UpdateRelationship",BindingFlags.Instance|BindingFlags.NonPublic)!;
        private static readonly MethodInfo Container_RelationValidate = typeof(Container).GetMethod(nameof(Container.RelationValidate))!;
        private static readonly MethodInfo Container_Clear = typeof(Container).GetMethod(nameof(Container.Clear))!;
        private static readonly FieldInfo DifinitionEntity2_ProtectedPrimaryKey = typeof(Entity<,>).GetField("ProtectedPrimaryKey",BindingFlags.Instance|BindingFlags.NonPublic)!;
        private static readonly MethodInfo DifinitionSet2_ContainsKey = typeof(Set<,>).GetMethod("ContainsKey")!;
        private static readonly MethodInfo DifinitionSet2_TryGetValue = typeof(Set<,>).GetMethod("TryGetValue")!;
        private static readonly MethodInfo DifinitionSet1_getVoidAdd = typeof(Set<>).GetMethod(nameof(Set<Int32>.VoidAdd))!;
        private static readonly MethodInfo DifinitionSet1_getVoidRemove = typeof(Set<>).GetMethod(nameof(Set<Int32>.VoidRemove))!;
        private static readonly MethodInfo CRC32_GetHashCode = typeof(CRC.CRC32).GetMethod(nameof(CRC.CRC32.GetHashCode))!;
        private static readonly MethodInfo DifinitionIEquatable_Equals = typeof(IEquatable<>).GetMethod(nameof(IEquatable<Int32>.Equals))!;
        private static readonly ConstructorInfo DifinitionSet1_ctor = typeof(Set<>).GetConstructor(Type.EmptyTypes)!;
        private static readonly ConstructorInfo DifinitionSet3_ctor = typeof(Set<,,>).GetConstructor(new[] { typeof(Set<,,>).GetGenericArguments()[2] })!;
        private static readonly ConstructorInfo DifinitionEntity2_ctor = typeof(Entity<,>).GetConstructors(BindingFlags.NonPublic|BindingFlags.Instance)[0];
        private static readonly CustomAttributeBuilder Extension_CustomAttributeBuilder = new CustomAttributeBuilder(typeof(ExtensionAttribute).GetConstructor(Type.EmptyTypes),Array.Empty<Object>());
        private static readonly ConstructorInfo StringBuilder_ctor = typeof(StringBuilder).GetConstructor(Type.EmptyTypes)!;
        private static readonly Type[] Types_InputHashCode = { typeof(CRC.CRC32).MakeByRefType() };
        private static readonly CustomAttributeBuilder Nullable_CustomAttributeBuilder = new CustomAttributeBuilder(Type.GetType("System.Runtime.CompilerServices.NullableAttribute")!.GetConstructor(new[] { typeof(Byte) }),new Object[] { (Byte)2 });
        private static readonly CustomAttributeBuilder NullableContext_CustomAttributeBuilder = new CustomAttributeBuilder(Type.GetType("System.Runtime.CompilerServices.NullableContextAttribute")!.GetConstructor(new[] { typeof(Byte) }),new Object[] { (Byte)2 });
        private static readonly CustomAttributeBuilder NonSerialized_CustomAttributeBuilder = new CustomAttributeBuilder(typeof(NonSerializedAttribute).GetConstructor(Type.EmptyTypes),Array.Empty<Object>());
        private readonly Type[] Types1 = new Type[1];
        private readonly Type[] Types2 = new Type[2];
        private readonly Type[] Types3 = new Type[3];
        private readonly Type[] Types_Catalog = new Type[1];
        private readonly Object[] Objects1 = new Object[1];
        public void OutputAssembly1(String アセンブリ名) {
            var Types1 = this.Types1;
            var Types2 = this.Types2;
            var Objects1 = this.Objects1;
            var Types3 = this.Types3;
            var Types_Catalog = this.Types_Catalog;
            Debug.Assert(DifinitionContainer_ctor0!=null);
            Debug.Assert(DifinitionContainer_ctor1!=null);
            Debug.Assert(DifinitionContainer_ctor2!=null);
            var (DynamicAssembly, ModuleBuilder)=CommonLibrary.DefineAssemblyModule(アセンブリ名);
            foreach(var Catalog in this.Catalogs) {
                var Catalog_EscapedName = Catalog.EscapedName;
                var Catalog_EscapedName_PrimaryKeys_Dot = Catalog_EscapedName+".PrimaryKeys.";
                var Catalog_EscapedName_Tables_Dot = Catalog_EscapedName+".Tables.";
                var Catalog_EscapedName_Dot = Catalog_EscapedName+'.';
                var Catalog_EscapedName_Views_Dot = Catalog_EscapedName+".Views.";
                var ParentExtensions = ModuleBuilder.DefineType(Catalog_EscapedName+".ParentExtensions",TypeAttributes.Public|TypeAttributes.Sealed|TypeAttributes.Abstract);
                ParentExtensions.SetCustomAttribute(Extension_CustomAttributeBuilder);
                var ChildExtensions = ModuleBuilder.DefineType(Catalog_EscapedName+".ChildExtensions",TypeAttributes.Public|TypeAttributes.Sealed|TypeAttributes.Abstract);
                ChildExtensions.SetCustomAttribute(Extension_CustomAttributeBuilder);
                var Container_TypeBuilder = ModuleBuilder.DefineType(Catalog_EscapedName+".Container",TypeAttributes.Public|TypeAttributes.Serializable);
                Types_Catalog[0]=Container_TypeBuilder;
                var ContainerBaseType = typeof(Container<>).MakeGenericType(Types_Catalog);
                Container_TypeBuilder.SetParent(ContainerBaseType);
                var (_, Container_ctor0_I)=コンストラクタ開始(Container_TypeBuilder,MethodAttributes.Public,Type.EmptyTypes);
                Container_ctor0_I.Ldarg_0();
                Container_ctor0_I.Call(TypeBuilder.GetConstructor(ContainerBaseType,DifinitionContainer_ctor0));
                var (Catalog_ctor1, Container_ctor1_I)=コンストラクタ開始(Container_TypeBuilder,MethodAttributes.Public,Types_Catalog);
                Catalog_ctor1.DefineParameter(1,ParameterAttributes.None,Catalog_EscapedName);
                Container_ctor1_I.Ldarg_0();
                Container_ctor1_I.Ldarg_1();
                Container_ctor1_I.Call(TypeBuilder.GetConstructor(ContainerBaseType,DifinitionContainer_ctor1));
                var (Container_ctor2, Container_ctor2_I)=コンストラクタ開始(Container_TypeBuilder,MethodAttributes.Public,Types_Stream_Stream);
                Container_ctor2.DefineParameter(1,ParameterAttributes.None,"Reader");
                Container_ctor2.DefineParameter(2,ParameterAttributes.None,"Writer");
                Container_ctor2_I.Ldarg_0();
                Container_ctor2_I.Ldarg_1();
                Container_ctor2_I.Ldarg_2();
                Container_ctor2_I.Call(TypeBuilder.GetConstructor(ContainerBaseType,DifinitionContainer_ctor2));
                var (Container_Init, Container_Init_I)=メソッド開始(
                    Container_TypeBuilder,
                    "Init",
                    MethodAttributes.Family|MethodAttributes.HideBySig|MethodAttributes.Virtual,
                    typeof(void));
                Container_TypeBuilder.DefineMethodOverride(Container_Init,ImportDatabase.Container_Init);
                var (Container_Read, Container_Read_I)=メソッド開始引数名(
                    Container_TypeBuilder,
                    "Read",
                    MethodAttributes.Family|MethodAttributes.HideBySig|MethodAttributes.Virtual,
                    typeof(void),
                    Types_Stream,
                    "Reader"
                );
                Container_TypeBuilder.DefineMethodOverride(Container_Read,ImportDatabase.Container_Read);
                var (Container_Write, Container_Write_I)=メソッド開始引数名(
                    Container_TypeBuilder,
                    "Write",
                    MethodAttributes.Family|MethodAttributes.HideBySig|MethodAttributes.Virtual,
                    typeof(void),
                    Types_Stream,
                    "Writer"
                );
                Container_TypeBuilder.DefineMethodOverride(Container_Write,ImportDatabase.Container_Write);
                var (Container_UpdateRelationship, Container_UpdateRelationship_I)=メソッド開始(
                    Container_TypeBuilder,
                    "UpdateRelationship",
                    MethodAttributes.Family|MethodAttributes.HideBySig|MethodAttributes.Virtual,
                    typeof(void));
                Container_TypeBuilder.DefineMethodOverride(Container_UpdateRelationship,ImportDatabase.Container_UpdateRelationship);
                var (Container_RelationValidate, Container_RelationValidate_I)=メソッド開始(
                    Container_TypeBuilder,
                    nameof(Container.RelationValidate),
                    MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.Virtual,
                    typeof(void));
                Container_TypeBuilder.DefineMethodOverride(Container_RelationValidate,ImportDatabase.Container_RelationValidate);
                var (Container_Transaction, Container_Transaction_I)=メソッド開始(
                    Container_TypeBuilder,
                    "Transaction",
                    MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.Virtual,
                    Container_TypeBuilder);
                Container_TypeBuilder.DefineMethodOverride(Container_Transaction,TypeBuilder.GetMethod(ContainerBaseType,DifinitionContainer_Transaction));
                var (Container_Copy, Container_Copy_I)=メソッド開始引数名(
                    Container_TypeBuilder,
                    "Copy",
                    MethodAttributes.Family|MethodAttributes.HideBySig|MethodAttributes.Virtual,
                    typeof(void),
                    Types_Catalog,
                    "Container_TypeBuilder"
                );
                Container_TypeBuilder.DefineMethodOverride(
                    Container_Copy,
                    TypeBuilder.GetMethod(
                        typeof(Container<>).MakeGenericType(Types_Catalog),
                        DifinitionContainer_Copy
                    )
                );
                var (Container_Clear, Container_Clear_I)=メソッド開始(
                    Container_TypeBuilder,
                    nameof(Container.Clear),
                    MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.Virtual,
                    typeof(void));
                Container_TypeBuilder.DefineMethodOverride(Container_Clear,ImportDatabase.Container_Clear);


                Types1[0]=Container_TypeBuilder;
                var IEquatable_Container = typeof(IEquatable<>).MakeGenericType(Types1);
                Container_TypeBuilder.AddInterfaceImplementation(IEquatable_Container);
                var (Container_Equals, Container_Equals_I)=メソッド開始引数名(
                    Container_TypeBuilder,
                    nameof(IEquatable<Int32>.Equals),
                    MethodAttributes.Public|MethodAttributes.Final|MethodAttributes.NewSlot|MethodAttributes.HideBySig|MethodAttributes.Virtual,
                    typeof(Boolean),
                    Types1,
                    "other"
                );
                Container_TypeBuilder.DefineMethodOverride(
                    Container_Equals,
                    TypeBuilder.GetMethod(
                        IEquatable_Container,
                        DifinitionIEquatable_Equals
                    )
                );
                var Container_Equalsでfalseの時 = Container_Equals_I.DefineLabel();



                var Schemas = Catalog.Schemas;
                foreach(var Schema in Schemas) {
                    var Schema_EscapeName = Schema.EscapedName;
                    var Catalog_Tables_Schema_Dot = Catalog_EscapedName_Tables_Dot+Schema_EscapeName+'.';
                    var Catalog_PrimaryKeys_Schema識別子_Dot = Catalog_EscapedName_PrimaryKeys_Dot+Schema_EscapeName+'.';
                    var Schema_TypeBuilder = ModuleBuilder.DefineType(
                        Catalog_EscapedName_Dot+"Schemas."+Schema_EscapeName,
                        TypeAttributes.Public|TypeAttributes.Serializable,
                        typeof(Sets.Schema)
                    );
                    Types1[0]=Schema_TypeBuilder;
                    var IEquatable_Schema = typeof(IEquatable<>).MakeGenericType(Types1);
                    Schema_TypeBuilder.AddInterfaceImplementation(IEquatable_Schema);
                    var (Schema_Equals, Schema_Equals_I)=メソッド開始引数名(
                        Schema_TypeBuilder,
                        nameof(IEquatable<Int32>.Equals),
                        MethodAttributes.Public|MethodAttributes.Final|MethodAttributes.NewSlot|MethodAttributes.HideBySig|MethodAttributes.Virtual,
                        typeof(Boolean),
                        Types1,
                        "other"
                    );
                    Schema_TypeBuilder.DefineMethodOverride(
                        Schema_Equals,
                        TypeBuilder.GetMethod(
                            IEquatable_Schema,
                            DifinitionIEquatable_Equals
                        )
                    );
                    var Schema_Equalsでfalseの時 = Schema_Equals_I.DefineLabel();
                    var (Schema_ctor, Schema_ctor_I)=コンストラクタ開始引数名(Schema_TypeBuilder,MethodAttributes.Assembly,Types_Catalog,Catalog_EscapedName);
                    static (FieldBuilder FieldBuilder, MethodBuilder GetMethodBuilder) Field実装Property実装GetMethod実装(TypeBuilder TypeBuilder,Type Type,String Name) {
                        var FieldBuilder = TypeBuilder.DefineField(
                            $"<{Name}>",
                            Type,
                            FieldAttributes.Assembly|FieldAttributes.InitOnly
                        );
                        var Property = TypeBuilder.DefineProperty(
                            Name,
                            PropertyAttributes.None,
                            CallingConventions.HasThis,
                            Type,
                            Type.EmptyTypes
                        );
                        var GetMethodBuilder = TypeBuilder.DefineMethod(
                            Name,
                            MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.SpecialName,
                            Type,
                            Type.EmptyTypes
                        );
                        Property.SetGetMethod(GetMethodBuilder);
                        var I = GetMethodBuilder.GetILGenerator();
                        I.Ldarg_0();
                        I.Ldfld(FieldBuilder);
                        I.Ret();
                        return (FieldBuilder, GetMethodBuilder);
                    }
                    var (Schema_FieldBuilder, _)=Field実装Property実装GetMethod実装(Container_TypeBuilder,Schema_TypeBuilder,Schema_EscapeName);
                    Container_Equals_I.Ldarg_0();
                    Container_Equals_I.Ldfld(Schema_FieldBuilder);
                    Container_Equals_I.Ldarg_1();
                    Container_Equals_I.Ldfld(Schema_FieldBuilder);
                    Container_Equals_I.Call(Schema_Equals);
                    Container_Equals_I.Brfalse(Container_Equalsでfalseの時);
                    Schema.FieldBuilder=Schema_FieldBuilder;
                    Container_RelationValidate_I.Ldarg_0();
                    Container_RelationValidate_I.Ldfld(Schema_FieldBuilder);
                    Schema.LocalBuilder=Container_RelationValidate_I.M_DeclareLocal_Stloc(Schema_TypeBuilder);
                    var (Schema_Read, Schema_Read_I)=メソッド開始引数名(Schema_TypeBuilder,"Read",MethodAttributes.Assembly,typeof(void),Types_Stream,"Reader");
                    var (Schema_Write, Schema_Write_I)=メソッド開始引数名(Schema_TypeBuilder,"Write",MethodAttributes.Assembly,typeof(void),Types_Stream,"Writer");
                    //var (Schema_UpdateRelationship, Schema_UpdateRelationship_I)=メソッド開始(Schema_TypeBuilder,"RelationValidate",MethodAttributes.Assembly,typeof(void),Type.EmptyTypes);
                    Types1[0]=Schema_TypeBuilder;
                    var (Schema_Assign, Schema_Assign_I)=メソッド開始引数名(Schema_TypeBuilder,"Assign",MethodAttributes.Assembly,typeof(void),Types1,"source");
                    var (Schema_Clear, Schema_Clear_I)=メソッド開始(Schema_TypeBuilder,"Clear",MethodAttributes.Assembly,typeof(void));
                    var Schema_Tables = Schema.Tables;
                    var Schema_ToString = Schema_TypeBuilder.DefineMethod(
                        nameof(Object.ToString),
                        MethodAttributes.Public|MethodAttributes.Virtual,
                        typeof(String),
                        Type.EmptyTypes
                    );
                    Schema_ToString.InitLocals=false;
                    Schema_TypeBuilder.DefineMethodOverride(
                        Schema_ToString,
                        Object_ToString
                    );
                    var Schema_ToString_I = Schema_ToString.GetILGenerator();
                    Schema_ToString_I.Newobj(StringBuilder_ctor);
                    var Schema_ToString_sb = Schema_ToString_I.M_DeclareLocal_Stloc(typeof(StringBuilder));
                    foreach(var Table in Schema_Tables) {
                        var Table_EscapedName = Table.EscapedName;
                        var Key_TypeBuilder = ModuleBuilder.DefineType(
                            Catalog_PrimaryKeys_Schema識別子_Dot+Table_EscapedName,
                            TypeAttributes.Public|TypeAttributes.Serializable,
                            typeof(ValueType)
                        )!;
                        Key_TypeBuilder.SetCustomAttribute(typeof(IsReadOnlyAttribute).GetConstructor(Array.Empty<Type>()),Array.Empty<Byte>());
                        Types1[0]=Key_TypeBuilder;
                        var IEquatable_Key = typeof(IEquatable<>).MakeGenericType(Types1);
                        Key_TypeBuilder.AddInterfaceImplementation(IEquatable_Key);
                        var (Key_Equals, Key_Equals_I)=メソッド開始引数名(
                            Key_TypeBuilder,
                            nameof(IEquatable<Int32>.Equals),
                            MethodAttributes.Public|MethodAttributes.Final|MethodAttributes.NewSlot|MethodAttributes.HideBySig|MethodAttributes.Virtual,
                            typeof(Boolean),
                            Types1,
                            "other"
                        );
                        Key_TypeBuilder.DefineMethodOverride(
                            Key_Equals,
                            TypeBuilder.GetMethod(
                                IEquatable_Key,
                                DifinitionIEquatable_Equals
                            )
                        );
                        Types2[0]=Key_TypeBuilder;
                        Types2[1]=Container_TypeBuilder;
                        var Entity2 = typeof(Entity<,>).MakeGenericType(Types2);
                        var Entity2_InternalPrimaryKey = TypeBuilder.GetField(
                            Entity2,
                            DifinitionEntity2_ProtectedPrimaryKey
                        );
                        var Table_TypeBuilder = Table.TypeBuilder=ModuleBuilder.DefineType(
                            Catalog_Tables_Schema_Dot+Table_EscapedName,
                            TypeAttributes.Public|TypeAttributes.Serializable,
                            Entity2
                        )!;
                        Table.Key_Equals=Key_Equals;
                        Table.Key_TypeBuilder=Key_TypeBuilder;
                        var ListColumn = Table.ListColumn;
                        var ListPrimaryKeyColumn = Table.ListPrimaryKeyColumn;
                        var ListPrimaryKeyColumn_Count = ListPrimaryKeyColumn.Count;
                        var Key_ctor_parameterTypes = new Type[ListPrimaryKeyColumn_Count];
                        for(var a = 0;a<ListPrimaryKeyColumn_Count;a++) {
                            Key_ctor_parameterTypes[a]=ListPrimaryKeyColumn[a].Type;
                        }
                        var Key_ctor = Key_TypeBuilder.DefineConstructor(
                            MethodAttributes.Public,
                            CallingConventions.HasThis,
                            Key_ctor_parameterTypes
                        );
                        Key_ctor.InitLocals=false;
                        Table.Key_ctor=Key_ctor;
                        var Key_ctor_I = Key_ctor.GetILGenerator();
                        var ListColumn_Count = ListColumn.Count;
                        var Table_ctor_parameterTypes = new Type[ListColumn_Count];
                        for(var a = 0;a<ListColumn_Count;a++) {
                            Table_ctor_parameterTypes[a]=ListColumn[a].Type;
                        }
                        var Table_ctor = Table_TypeBuilder.DefineConstructor(
                            MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.SpecialName|MethodAttributes.RTSpecialName,
                            CallingConventions.HasThis,
                            Table_ctor_parameterTypes
                        );
                        Table_ctor.InitLocals=false;
                        var Table_ctor_I = Table_ctor.GetILGenerator();
                        Table.ctor_I=Table_ctor_I;
                        var (_, Key_InputHashCode_I)=メソッド開始引数名(
                            Key_TypeBuilder,
                            "InputHashCode",
                            MethodAttributes.Assembly|MethodAttributes.HideBySig,
                            typeof(void),
                            Types_InputHashCode,
                            "CRC"
                        );
                        var (Key_GetHashCode, Key_GetHashCode_I)=メソッド開始(
                            Key_TypeBuilder,
                            nameof(Object.GetHashCode),
                            MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.Virtual,
                            typeof(Int32));
                        Key_TypeBuilder.DefineMethodOverride(Key_GetHashCode,Object_GetHashCode);
                        var (Key_ToStringBuilder, Key_ToStringBuilder_I)=メソッド開始引数名(
                            Key_TypeBuilder,
                            "ToStringBuilder",
                            MethodAttributes.Assembly|MethodAttributes.HideBySig,
                            typeof(void),
                            Types_StringBuilder,
                            "sb"
                        );
                        var (_, Table_ToStringBuilder_I)=メソッド開始引数名(
                            Table_TypeBuilder,
                            "ToStringBuilder",
                            MethodAttributes.Family|MethodAttributes.Virtual,
                            typeof(void),
                            Types_StringBuilder,
                            "sb"
                        );
                        Table_ToStringBuilder_I.Ldarg_0();
                        Table_ToStringBuilder_I.Ldflda(Entity2_InternalPrimaryKey);
                        Table_ToStringBuilder_I.Ldarg_1();
                        Table_ToStringBuilder_I.Call(Key_ToStringBuilder);
                        Table.AddRelationship=AddRelationship開始(Table_TypeBuilder,nameof(Entity<Int32,Container>.AddRelationship),Types_Catalog);
                        Table.RemoveRelationship=RemoveRelationship開始(Table_TypeBuilder,nameof(Entity<Int32,Container>.RemoveRelationship),Types_Catalog);
                        {
                            var InvalidateClearRelationship = Table_TypeBuilder.DefineMethod(
                                nameof(Entity.InvalidateClearRelationship),
                                MethodAttributes.FamORAssem|MethodAttributes.HideBySig|MethodAttributes.Virtual,
                                typeof(void),
                                Type.EmptyTypes
                            );
                            InvalidateClearRelationship.InitLocals=false;
                            Table_TypeBuilder.DefineMethodOverride(
                                InvalidateClearRelationship,
                                Entity_InvalidateClearRelationship
                            );
                            Table.InvalidateClearRelationship=InvalidateClearRelationship;
                        }
                        {
                            //var InvalidateRemoveRelationship = Table.DefineMethod(
                            //    nameof(Entity.InvalidateRemoveRelationship),
                            //    MethodAttributes.FamORAssem|MethodAttributes.HideBySig|MethodAttributes.Virtual,
                            //    typeof(void),
                            //    Type.EmptyTypes
                            //);
                            //InvalidateRemoveRelationship.InitLocals=false;
                            //Table.DefineMethodOverride(
                            //    InvalidateRemoveRelationship,
                            //    Entity_InvalidateRemoveRelationship
                            //);
                            //テーブル.Value.InvalidateRemoveRelationship=InvalidateRemoveRelationship;
                        }
                        Types1[0]=Table_TypeBuilder;
                        var IEquatable_Table = typeof(IEquatable<>).MakeGenericType(Types1);
                        Table_TypeBuilder.AddInterfaceImplementation(IEquatable_Table);
                        //var GenericASet = typeof(ImmutableSet<>).MakeGenericType(Types1);
                        Types3[0]=Table_TypeBuilder;
                        Types3[1]=Key_TypeBuilder;
                        Types3[2]=Container_TypeBuilder;
                        var GenericSet3 = typeof(Set<,,>).MakeGenericType(Types3);
                        var (Table_Equals, Table_Equals_I)=メソッド開始引数名(
                            Table_TypeBuilder,
                            nameof(IEquatable<Int32>.Equals),
                            MethodAttributes.Public|MethodAttributes.Final|MethodAttributes.NewSlot|MethodAttributes.HideBySig|MethodAttributes.Virtual,
                            typeof(Boolean),
                            Types1,
                            "other"
                        );
                        Table_TypeBuilder.DefineMethodOverride(
                            Table_Equals,
                            TypeBuilder.GetMethod(
                                IEquatable_Table,
                                DifinitionIEquatable_Equals
                            )
                        );
                        var KeyTable_Equalsでfalseの時 = Key_Equals_I.DefineLabel();
                        LocalBuilder KeyTable_GetHashCode_CRC;
                        if(ListPrimaryKeyColumn_Count==1) {
                            KeyTable_GetHashCode_CRC=default!;
                        } else {
                            KeyTable_GetHashCode_CRC = Key_GetHashCode_I.DeclareLocal(typeof(CRC.CRC32));
                            Key_GetHashCode_I.Ldloca(KeyTable_GetHashCode_CRC);
                            Key_GetHashCode_I.Initobj(typeof(CRC.CRC32));
                        }
                        var Table_Equalsでfalseの時 = Table_Equals_I.DefineLabel();
                        for(var a = 0;a<ListPrimaryKeyColumn_Count;) {
                            var PrimaryKeyColumn = ListPrimaryKeyColumn[a];
                            a++;
                            var Type = PrimaryKeyColumn.Type;
                            var EscapedName = PrimaryKeyColumn.EscapedName;
                            var (FieldBuilder, PrimaryKey_GetMethod)=Field実装Property実装GetMethod実装(Key_TypeBuilder,Type,EscapedName);
                            PrimaryKeyColumn.FieldBuilder=FieldBuilder;
                            Key_ctor.DefineParameter(
                                a,
                                ParameterAttributes.None,
                                EscapedName
                            );
                            Key_ctor_I.Ldarg_0();
                            Key_ctor_I.Ldarg((UInt16)a);
                            Key_ctor_I.Stfld(FieldBuilder);
                            {
                                Key_Equals_I.Ldarg_0();
                                if(Type.IsValueType) {
                                    Key_Equals_I.Ldflda(FieldBuilder);
                                } else {
                                    Key_Equals_I.Ldfld(FieldBuilder);
                                }
                                Key_Equals_I.Ldarg_1();
                                Key_Equals_I.Ldfld(FieldBuilder);
                                Types1[0]=Type;
                                var Equals = Type.GetMethod(nameof(Object.Equals),Types1)!;
                                Debug.Assert(Equals.DeclaringType==typeof(Object)||Equals.DeclaringType!.IsSubclassOf(typeof(Object)));
                                if(Type.IsValueType&&Reflection.Object.Equals_==Equals.GetBaseDefinition()) {
                                    Key_Equals_I.Box(Type);
                                    Key_Equals_I.Constrained(Type);
                                    Key_Equals_I.Callvirt(Equals);
                                } else {
                                    Key_Equals_I.Call(Equals);
                                }
                                Key_Equals_I.Brfalse(KeyTable_Equalsでfalseの時);
                            }
                            {
                                var Property = Table_TypeBuilder.DefineProperty(
                                    EscapedName,
                                    PropertyAttributes.None,
                                    CallingConventions.HasThis,
                                    Type,
                                    Type.EmptyTypes
                                );
                                var GetMethodBuilder = Table_TypeBuilder.DefineMethod(
                                    EscapedName,
                                    MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.SpecialName,
                                    Type,
                                    Type.EmptyTypes
                                );
                                Property.SetGetMethod(GetMethodBuilder);
                                var I = GetMethodBuilder.GetILGenerator();
                                I.Ldarg_0();
                                I.Ldflda(Entity2_InternalPrimaryKey);
                                I.Ldfld(FieldBuilder);
                                I.Ret();
                            }
                            Key_InputHashCode_I.Ldarg_1();
                            Key_InputHashCode_I.Ldarg_0();
                            Key_InputHashCode_I.Ldfld(FieldBuilder);
                            Types1[0]=Type;
                            Key_InputHashCode_I.Call(InputT.MakeGenericMethod(Types1));
                            if(ListPrimaryKeyColumn_Count==1) {
                                Key_GetHashCode_I.Ldarg_0();
                                if(Type==typeof(Int32)) {
                                    Key_GetHashCode_I.Ldfld(FieldBuilder);
                                } else {
                                    if(FieldBuilder.FieldType.IsValueType) {
                                        Key_GetHashCode_I.Ldflda(FieldBuilder);
                                    } else {
                                        Key_GetHashCode_I.Ldfld(FieldBuilder);
                                    }
                                    //Key_GetHashCode_I.Constrained(Type);
                                    //Key_GetHashCode_I.Callvirt(Type.GetMethod(nameof(Object.GetHashCode)));
                                    Key_GetHashCode_I.Call(Type.GetMethod(nameof(Object.GetHashCode),Type.EmptyTypes));
                                }
                            } else {
                                Debug.Assert(ListPrimaryKeyColumn_Count>1);
                                Key_GetHashCode_I.Ldloca(KeyTable_GetHashCode_CRC!);
                                Key_GetHashCode_I.Ldarg_0();
                                Key_GetHashCode_I.Ldfld(FieldBuilder);
                                Key_GetHashCode_I.Call(InputT.MakeGenericMethod(Types1));
                            }
                            Key_ToStringBuilder_I.Ldarg_1();
                            Key_ToStringBuilder_I.Ldstr(EscapedName+"=");
                            Key_ToStringBuilder_I.Call(StringBuilder_Append_String);
                            Key_ToStringBuilder_I.Ldarg_0();
                            Key_ToStringBuilder_I.Ldfld(FieldBuilder);
                            if(Type==typeof(String)) {
                                Key_ToStringBuilder_I.Call(StringBuilder_AppendLine_String);
                            } else {
                                if(Type.IsValueType) {
                                    Key_ToStringBuilder_I.Box(Type);
                                }
                                Key_ToStringBuilder_I.Call(StringBuilder_Append_Object);
                                Key_ToStringBuilder_I.Call(StringBuilder_AppendLine);
                            }
                            Key_ToStringBuilder_I.Pop();
                        }
                        Key_ctor_I.Ret();
                        if(ListPrimaryKeyColumn_Count==0) {
                            Key_GetHashCode_I.Ldc_I4_1();
                        } else if(ListPrimaryKeyColumn_Count>1) {
                            Key_GetHashCode_I.Ldloca(KeyTable_GetHashCode_CRC!);
                            //Key_GetHashCode_I.Constrained(typeof(CRC.CRC32));
                            //Key_GetHashCode_I.Callvirt(CRC32_GetHashCode);
                            Key_GetHashCode_I.Call(CRC32_GetHashCode);
                        }
                        Key_GetHashCode_I.Ret();
                        Key_InputHashCode_I.Ret();
                        Key_ToStringBuilder_I.Ret();
                        {
                            var (ToString, I)=メソッド開始(
                                Key_TypeBuilder,
                                nameof(Object.ToString),
                                MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.Virtual,
                                typeof(String));
                            Key_TypeBuilder.DefineMethodOverride(ToString,Object_ToString);
                            I.Newobj(StringBuilder_ctor);
                            var sb = I.M_DeclareLocal_Stloc(typeof(StringBuilder));
                            I.Ldarg_0();
                            I.Ldloc(sb);
                            I.Call(Key_ToStringBuilder);
                            I.Ldloc(sb);
                            I.Callvirt(Object_ToString);
                            I.Ret();
                        }
                        共通override_IEquatable_Equalsメソッド終了(
                            Key_Equals_I,
                            KeyTable_Equalsでfalseの時
                        );
                        Table_ctor_I.Ldarg_0();
                        for(var a = 0;a<ListColumn_Count;) {
                            var Column = ListColumn[a];
                            a++;
                            if(ListPrimaryKeyColumn.Contains(Column)) {
                                Table_ctor_I.Ldarg((UInt16)a);
                            }
                            var Parameter = Table_ctor.DefineParameter(
                                a,
                                ParameterAttributes.None,
                                Column.EscapedName
                            );
                            if(Column.IsNullableClass) {
                                Parameter.SetCustomAttribute(
                                    Nullable_CustomAttributeBuilder
                                );
                            }
                        }
                        Table_ctor_I.Newobj(Key_ctor);
                        Table_ctor_I.Call(
                            TypeBuilder.GetConstructor(
                                Entity2,
                                DifinitionEntity2_ctor
                            )
                        );
                        Table_Equals_I.Ldarg_0();
                        Table_Equals_I.Ldflda(Entity2_InternalPrimaryKey);
                        Table_Equals_I.Ldarg_1();
                        Table_Equals_I.Ldfld(Entity2_InternalPrimaryKey);
                        Table_Equals_I.Call(Key_Equals);
                        Table_Equals_I.Brfalse(Table_Equalsでfalseの時);
                        for(var a = 0;a<ListColumn_Count;) {
                            var Column = ListColumn[a];
                            a++;
                            if(ListPrimaryKeyColumn.Contains(Column)) continue;
                            var Type = Column.Type;
                            var EscapedName = Column.EscapedName;
                            var (FieldBuilder, GetMethodBuilder)=Field実装Property実装GetMethod実装(Table_TypeBuilder,Type,EscapedName);
                            Column.FieldBuilder=FieldBuilder;
                            if(Column.IsNullableClass) {
                                FieldBuilder.SetCustomAttribute(Nullable_CustomAttributeBuilder);
                                GetMethodBuilder.SetCustomAttribute(NullableContext_CustomAttributeBuilder);
                            }
                            Table_ctor_I.Ldarg_0();
                            Table_ctor_I.Ldarg((UInt16)a);
                            Table_ctor_I.Stfld(FieldBuilder);
                            Table_ToStringBuilder_I.Ldarg_1();
                            Table_ToStringBuilder_I.Ldstr(EscapedName+"=");
                            Table_ToStringBuilder_I.Call(StringBuilder_Append_String);
                            Table_ToStringBuilder_I.Ldarg_0();
                            Table_ToStringBuilder_I.Ldfld(FieldBuilder);
                            if(Type==typeof(String)) {
                                Table_ToStringBuilder_I.Call(StringBuilder_AppendLine_String);
                            } else {
                                if(Type.IsValueType) {
                                    Table_ToStringBuilder_I.Box(Type);
                                }
                                Table_ToStringBuilder_I.Call(StringBuilder_Append_Object);
                                Table_ToStringBuilder_I.Call(StringBuilder_AppendLine);
                            }
                            Table_ToStringBuilder_I.Pop();
                            this.共通Equals(Column.IsNullableClass,FieldBuilder,Table_Equals_I,Table_Equalsでfalseの時);
                        }
                        Table_ToStringBuilder_I.Ret();
                        共通override_IEquatable_Equalsメソッド終了(
                            Table_Equals_I,
                            Table_Equalsでfalseの時
                        );
                        Types2[0]=Types2[1]=Key_TypeBuilder;
                        共通op_Equality_Inequality(Key_TypeBuilder,Key_Equals,Types2);
                        //共通override_Object_Equals終了(Key_TypeBuilder,Key_Equals,OpCodes.Unbox_Any);
                        {
                            var Type_Equals = Key_TypeBuilder.DefineMethod(
                                nameof(Object.Equals),
                                MethodAttributes.Public|MethodAttributes.Virtual|MethodAttributes.HideBySig,
                                typeof(Boolean),
                                Types_Object
                            );
                            Key_TypeBuilder.DefineMethodOverride(
                                Type_Equals,
                                Object_Equals
                            );
                            Type_Equals.InitLocals=false;
                            Type_Equals.DefineParameter(1,ParameterAttributes.None,"other");
                            var I = Type_Equals.GetILGenerator();
                            I.Ldarg_1();
                            I.Isinst(Key_TypeBuilder);
                            var falseの時 = I.DefineLabel();
                            I.Brfalse_S(falseの時);
                            I.Ldarg_1();
                            I.Unbox_Any(Key_TypeBuilder);
                            var 変数 = I.M_DeclareLocal_Stloc_Ldloc(Key_TypeBuilder);
                            I.Ldarg_0();
                            I.Ldloc(変数);
                            I.Callvirt(Key_Equals);
                            var 終了 = I.DefineLabel();
                            I.Br_S(終了);
                            I.MarkLabel(falseの時);
                            I.Ldc_I4_0();
                            I.MarkLabel(終了);
                            I.Ret();
                        }
                        共通override_Object_Equals終了(Table_TypeBuilder,Table_Equals,OpCodes.Isinst);
                        Types3[0]=Table_TypeBuilder;
                        Types3[1]=Key_TypeBuilder;
                        Types3[2]=Container_TypeBuilder;
                        var Table_FieldType = typeof(Set<,,>).MakeGenericType(Types3);
                        var (Table_FieldBuilder, _)=Field実装Property実装GetMethod実装(Schema_TypeBuilder,Table_FieldType,Table_TypeBuilder.Name);
                        Table.FieldBuilder=Table_FieldBuilder;
                        Schema_ctor_I.Ldarg_0();
                        Schema_ctor_I.Ldarg_1();
                        Schema_ctor_I.Newobj(
                            TypeBuilder.GetConstructor(
                                Table_FieldType,
                                DifinitionSet3_ctor
                            )
                        );
                        Schema_ctor_I.Stfld(Table_FieldBuilder);
                        Schema_Read_I.Ldarg_0();
                        Schema_Read_I.Ldfld(Table_FieldBuilder);
                        Schema_Read_I.Ldarg_1();
                        Schema_Read_I.Call(
                            TypeBuilder.GetMethod(
                                GenericSet3,
                                DifinitionSet3_BinaryRead
                            )
                        );
                        Schema_Write_I.Ldarg_0();
                        Schema_Write_I.Ldfld(Table_FieldBuilder);
                        Schema_Write_I.Ldarg_1();
                        Schema_Write_I.Call(
                            TypeBuilder.GetMethod(
                                GenericSet3,
                                DifinitionSet3_BinaryWrite
                            )
                        );
                        Schema_Equals_I.Ldarg_0();
                        Schema_Equals_I.Ldfld(Table_FieldBuilder);
                        Schema_Equals_I.Ldarg_1();
                        Schema_Equals_I.Ldfld(Table_FieldBuilder);
                        Schema_Equals_I.Callvirt(Object_Equals);//本当はネイティブImmutable<>.Equalsを呼び出したい
                        Schema_Equals_I.Brfalse(Schema_Equalsでfalseの時);

                        Schema_Assign_I.Ldarg_0();
                        Schema_Assign_I.Ldfld(Table_FieldBuilder);
                        Schema_Assign_I.Ldarg_1();
                        Schema_Assign_I.Ldfld(Table_FieldBuilder);
                        Types1[0]=Table_TypeBuilder;
                        var Set1 = typeof(Set<>).MakeGenericType(Types1);
                        Schema_Assign_I.Call(
                            TypeBuilder.GetMethod(
                                Set1,
                                DifinitionSet_Assign
                            )
                        );
                        Schema_Clear_I.Ldarg_0();
                        Schema_Clear_I.Ldfld(Table_FieldBuilder);
                        Schema_Clear_I.Callvirt(
                            TypeBuilder.GetMethod(
                                Set1,
                                DifinitionSet_Clear
                            )
                        );
                        Key_TypeBuilder.CreateType();
                        Schema_ToString_I.Ldloc(Schema_ToString_sb);
                        Schema_ToString_I.Ldstr(Table_EscapedName+":");
                        Schema_ToString_I.Call(StringBuilder_Append_String);
                        Schema_ToString_I.Ldarg_0();
                        Schema_ToString_I.Ldfld(Table_FieldBuilder);
                        Schema_ToString_I.Callvirt(Object_ToString);
                        Schema_ToString_I.Call(StringBuilder_AppendLine_String);
                        Schema_ToString_I.Pop();
                    }
                    var Catalog_Views_Schema_Dot = Catalog_EscapedName_Views_Dot+Schema_EscapeName+'.';
                    foreach(var View in Schema.Views) {
                        var View_EscapeName = View.EscapedName;
                        var View_TypeBuilder = ModuleBuilder.DefineType(
                            Catalog_Views_Schema_Dot+View_EscapeName,
                            TypeAttributes.Public|TypeAttributes.Serializable
                        )!;
                        View.TypeBuilder=View_TypeBuilder;
                        var ListColumn = View.ListColumn;
                        var ListColumn_Count = ListColumn.Count;
                        var View_ctor_parameterTypes = new Type[ListColumn_Count];
                        for(var a = 0;a<ListColumn_Count;a++) {
                            View_ctor_parameterTypes[a]=ListColumn[a].Type;
                        }
                        var View_ctor = View_TypeBuilder.DefineConstructor(
                            MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.SpecialName|MethodAttributes.RTSpecialName,
                            CallingConventions.HasThis,
                            View_ctor_parameterTypes
                        );
                        View_ctor.InitLocals=false;
                        var View_ctor_I = View_ctor.GetILGenerator();
                        var (_, View_ToStringBuilder_I)=メソッド開始引数名(
                            View_TypeBuilder,
                            "ToStringBuilder",
                            MethodAttributes.Family|MethodAttributes.Virtual,
                            typeof(void),
                            Types_StringBuilder,
                            "sb"
                        );
                        Types1[0]=View_TypeBuilder;
                        var IEquatable_View = typeof(IEquatable<>).MakeGenericType(Types1);
                        View_TypeBuilder.AddInterfaceImplementation(IEquatable_View);
                        var (View_Equals, View_Equals_I)=メソッド開始引数名(
                            View_TypeBuilder,
                            nameof(IEquatable<Int32>.Equals),
                            MethodAttributes.Public|MethodAttributes.Final|MethodAttributes.NewSlot|MethodAttributes.HideBySig|MethodAttributes.Virtual,
                            typeof(Boolean),
                            Types1,
                            "other"
                        );
                        View_TypeBuilder.DefineMethodOverride(
                            View_Equals,
                            TypeBuilder.GetMethod(
                                IEquatable_View,
                                DifinitionIEquatable_Equals
                            )
                        );
                        var View_Equalsでfalseの時 = View_Equals_I.DefineLabel();
                        var FieldIndex = 1;
                        foreach(var Column in ListColumn) {
                            var Parameter = View_ctor.DefineParameter(
                                FieldIndex++,
                                ParameterAttributes.None,
                                Column.EscapedName
                            );
                            if(Column.IsNullableClass) {
                                Parameter.SetCustomAttribute(
                                    Nullable_CustomAttributeBuilder
                                );
                            }
                        }
                        FieldIndex=1;
                        foreach(var Column in ListColumn) {
                            var Type = Column.Type;
                            var EscapedName = Column.EscapedName;
                            var (FieldBuilder, GetMethodBuilder)=Field実装Property実装GetMethod実装(View_TypeBuilder,Type,EscapedName);
                            Column.FieldBuilder=FieldBuilder;
                            if(Column.IsNullableClass) {
                                FieldBuilder.SetCustomAttribute(Nullable_CustomAttributeBuilder);
                                GetMethodBuilder.SetCustomAttribute(NullableContext_CustomAttributeBuilder);
                            }
                            View_ctor_I.Ldarg_0();
                            View_ctor_I.Ldarg((UInt16)FieldIndex);
                            View_ctor_I.Stfld(FieldBuilder);
                            View_ToStringBuilder_I.Ldarg_1();
                            View_ToStringBuilder_I.Ldstr(EscapedName+"=");
                            View_ToStringBuilder_I.Call(StringBuilder_Append_String);
                            View_ToStringBuilder_I.Ldarg_0();
                            View_ToStringBuilder_I.Ldfld(FieldBuilder);
                            if(Type==typeof(String)) {
                                View_ToStringBuilder_I.Call(StringBuilder_AppendLine_String);
                            } else {
                                if(Type.IsValueType) {
                                    View_ToStringBuilder_I.Box(Type);
                                }
                                View_ToStringBuilder_I.Call(StringBuilder_Append_Object);
                                View_ToStringBuilder_I.Call(StringBuilder_AppendLine);
                            }
                            View_ToStringBuilder_I.Pop();
                            this.共通Equals(Column.IsNullableClass,FieldBuilder,View_Equals_I,View_Equalsでfalseの時);
                            FieldIndex++;
                        }
                        View_ctor_I.Ret();
                        View_ToStringBuilder_I.Ret();
                        共通override_IEquatable_Equalsメソッド終了(
                            View_Equals_I,
                            View_Equalsでfalseの時
                        );
                        Types2[0]=Types2[1]=View_TypeBuilder;
                        共通op_Equality_Inequality(View_TypeBuilder,View_Equals,Types2);
                        共通override_Object_Equals終了(View_TypeBuilder,View_Equals,OpCodes.Isinst);
                        {
                            //schema
                            //    private Func<ImmutableSet<View>>#View;
                            //    public ImmutableSet<View>View=>#View;
                            Types1[0]=View_TypeBuilder;
                            var ImmutableSetType = typeof(ImmutableSet<>).MakeGenericType(Types1);
                            //var (View_FieldBuilder, GetMethod)=Field実装Property実装GetMethod実装(Schema_TypeBuilder,View_TypeBuilder,View_EscapeName);
                            ////View.FieldBuilder=PrimaryKey_GetMethod;
                            var DelegateType = typeof(Func<>).MakeGenericType(ImmutableSetType);
                            var View_FieldBuilder = Schema_TypeBuilder.DefineField(
                                View_EscapeName,
                                DelegateType,
                                FieldAttributes.Private
                            );
                            var Property = Schema_TypeBuilder.DefineProperty(
                                View_EscapeName,
                                PropertyAttributes.None,
                                CallingConventions.HasThis,
                                ImmutableSetType,
                                Type.EmptyTypes
                            );
                            var GetMethod = Schema_TypeBuilder.DefineMethod(
                                View_EscapeName,
                                MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.SpecialName,
                                ImmutableSetType,
                                Type.EmptyTypes
                            );
                            Property.SetGetMethod(GetMethod);
                            var I = GetMethod.GetILGenerator();
                            I.Ldarg_0();
                            I.Ldfld(View_FieldBuilder);
                            I.Callvirt(
                                TypeBuilder.GetMethod(
                                    DelegateType,
                                    typeof(Func<>).GetMethod(nameof(Func<Int32>.Invoke),Type.EmptyTypes)
                                )
                            );
                            I.Ret();
                            Schema_ToString_I.Ldloc(Schema_ToString_sb);
                            Schema_ToString_I.Ldstr(View_EscapeName+":");
                            Schema_ToString_I.Call(StringBuilder_Append_String);
                            Schema_ToString_I.Ldarg_0();
                            Schema_ToString_I.Ldfld(View_FieldBuilder);
                            Schema_ToString_I.Callvirt(Object_ToString);
                            Schema_ToString_I.Call(StringBuilder_AppendLine_String);
                            Schema_ToString_I.Pop();
                        }
                        View.TypeBuilder.CreateType();
                    }
                    共通override_IEquatable_Equalsメソッド終了(Schema_Equals_I,Schema_Equalsでfalseの時);
                    共通override_Object_Equals終了(Schema_TypeBuilder,Schema_Equals,OpCodes.Isinst);
                    Schema_ToString_I.Ldloc(Schema_ToString_sb);
                    Schema_ToString_I.Callvirt(Object_ToString);
                    Schema_ToString_I.Ret();
                    static void 共通op_Equality_Inequality(TypeBuilder TypeBuilder,MethodBuilder Equals,Type[] Types2) {
                        共通struct_op_Equality_Inequality(TypeBuilder,Equals,op_Equality,Types2).Ret();
                        var I = 共通struct_op_Equality_Inequality(TypeBuilder,Equals,op_Inequality,Types2);
                        I.Ldc_I4_0();
                        I.Ceq();
                        I.Ret();
                    }
                    Schema_ctor_I.Ret();
                    Schema_Read_I.Ret();
                    Schema_Write_I.Ret();
                    //Schema_UpdateRelationship_I.Ret();
                    Schema_Assign_I.Ret();

                    Container_Init_I.Ldarg_0();
                    Container_Init_I.Dup();
                    Container_Init_I.Newobj(Schema_ctor);
                    Container_Init_I.Stfld(Schema_FieldBuilder);

                    Container_Read_I.Ldarg_0();
                    Container_Read_I.Ldfld(Schema_FieldBuilder);
                    Container_Read_I.Ldarg_1();
                    Container_Read_I.Call(Schema_Read);

                    Container_Write_I.Ldarg_0();
                    Container_Write_I.Ldfld(Schema_FieldBuilder);
                    Container_Write_I.Ldarg_1();
                    Container_Write_I.Call(Schema_Write);

                    //Container_UpdateRelationship_I.Ldarg_0();
                    //Container_UpdateRelationship_I.Ldfld(Schema_FieldBuilder);
                    //Container_UpdateRelationship_I.Call(Schema_UpdateRelationship);

                    Container_Copy_I.Ldarg_1();
                    Container_Copy_I.Ldfld(Schema_FieldBuilder);
                    Container_Copy_I.Ldarg_0();
                    Container_Copy_I.Ldfld(Schema_FieldBuilder);
                    Container_Copy_I.Call(Schema_Assign);

                    Schema_Clear_I.Ret();

                    Container_Clear_I.Ldarg_0();
                    Container_Clear_I.Ldfld(Schema_FieldBuilder);
                    Container_Clear_I.Call(Schema_Clear);

                    var t = Schema_TypeBuilder.CreateType();
                }
                共通override_IEquatable_Equalsメソッド終了(Container_Equals_I,Container_Equalsでfalseの時);
                共通override_Object_Equals終了(Container_TypeBuilder,Container_Equals,OpCodes.Isinst);
                Container_Init_I.Ret();
                Container_Read_I.Ret();
                Container_Write_I.Ret();
                Container_UpdateRelationship_I.Ret();
                Container_Clear_I.Ret();
                //var Tables = Catalog.Schemas.SelectMany(p => p.Tables).Where(
                //    p =>
                //        p.Name.StartsWith("Table_4")
                //).ToArray();
                var Tables = Catalog.Schemas.SelectMany(p => p.Tables).ToArray();
                //Tables = Catalog.Schemas.SelectMany(p => p.Tables).Where(
                //    p =>
                //        p.Name.StartsWith("Table_5")||
                //        p.Name.StartsWith("Table_6")
                //).ToArray();
                foreach(var Table0 in Tables) {
                    //Set,Oneのフィールドを定義
                    foreach(var Table1 in Tables) {
                        foreach(var Table1_親ForeignKey in Table1.親ForeignKeys) {
                            if(Table0!=Table1_親ForeignKey.Table) continue;
                            //親のOneを保持するフィールド
                            var 親FieldBuilder = Table1.TypeBuilder!.DefineField(
                                "親"+Table1_親ForeignKey.Name,
                                Table0.TypeBuilder,
                                FieldAttributes.Assembly
                            );
                            親FieldBuilder.SetCustomAttribute(NonSerialized_CustomAttributeBuilder);
                            if(Table1_親ForeignKey.IsNullable) {
                                親FieldBuilder.SetCustomAttribute(Nullable_CustomAttributeBuilder);
                            }
                            Debug.Assert(Table1_親ForeignKey.FieldBuilder==null);
                            Table1_親ForeignKey.FieldBuilder=親FieldBuilder;
                            //子のSetを保持するフィールド
                            var Table0_子ForeignKey = Table1_親ForeignKey.対になるForeignKey1!;
                            Types1[0]=Table1.TypeBuilder!;
                            var 子FieldBuilder = Table0.TypeBuilder!.DefineField(
                                "子"+Table0_子ForeignKey.Name,
                                typeof(Set<>).MakeGenericType(Types1),
                                FieldAttributes.Assembly
                            );
                            子FieldBuilder.SetCustomAttribute(NonSerialized_CustomAttributeBuilder);
                            Debug.Assert(Table0_子ForeignKey.FieldBuilder==null);
                            Table0_子ForeignKey.FieldBuilder=子FieldBuilder;
                        }
                    }
                }
                foreach(var 自Table in Tables) {
                    var AddRelationship_I = 自Table.AddRelationship!.GetILGenerator();
                    var RemoveRelationship_I = 自Table.RemoveRelationship!.GetILGenerator();
                    var InvalidateClearRelationship_I = 自Table.InvalidateClearRelationship!.GetILGenerator();
                    var 自Table_ctor_I = 自Table.ctor_I!;
                    //1回目
                    //AddRelationshipで親が存在するか
                    //RemoveRelationshipで子が存在するか
                    foreach(var 子Table in Tables) {
                        foreach(var 子Table_親ForeignKey in 子Table.親ForeignKeys) {
                            var 親Table_子ForeignKey = 子Table_親ForeignKey.対になるForeignKey1!;
                            if(自Table!=子Table_親ForeignKey.Table) continue;
                            //var 自己参照 = 親Table_子ForeignKey.Table==子Table_親ForeignKey.Table;
                            //if(自己参照) continue;
                            Debug.Assert(子Table_親ForeignKey.FieldBuilder!=null);
                            //子のSetを保持するフィールド
                            //var 自Table_TypeBuilder = 自Table.TypeBuilder!;
                            //var 自Table_ctor_I = 自Table.ctor_I!;
                            Debug.Assert(親Table_子ForeignKey.Table==子Table);
                            Types1[0]=子Table.TypeBuilder!;
                            var SetType = typeof(Set<>).MakeGenericType(Types1);
                            Debug.Assert(親Table_子ForeignKey.FieldBuilder!=null);
                            var 親Table_子ForeignKey_FieldBuilder = 親Table_子ForeignKey.FieldBuilder!;
                            自Table_ctor_I.Ldarg_0();
                            自Table_ctor_I.Newobj(
                                TypeBuilder.GetConstructor(
                                    SetType,
                                    DifinitionSet1_ctor
                                )
                            );
                            自Table_ctor_I.Stfld(親Table_子ForeignKey_FieldBuilder);
                            {
                                Types1[0]=子Table.TypeBuilder!;
                                var ImmutableSet1 = typeof(ImmutableSet<>).MakeGenericType(Types1);
                                Types1[0]=自Table.TypeBuilder!;
                                var ChildExtensions_Method = ChildExtensions.DefineMethod(
                                    子Table_親ForeignKey.EscapedName,
                                    MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.Static,
                                    ImmutableSet1,
                                    Types1
                                );
                                ChildExtensions_Method.InitLocals=false;
                                ChildExtensions_Method.SetCustomAttribute(Extension_CustomAttributeBuilder);
                                ChildExtensions_Method.DefineParameter(1,ParameterAttributes.None,自Table.EscapedName);
                                var ChildExtensions_Method_I = ChildExtensions_Method.GetILGenerator();
                                ChildExtensions_Method_I.Ldarg_0();
                                ChildExtensions_Method_I.Ldfld(親Table_子ForeignKey_FieldBuilder);
                                ChildExtensions_Method_I.Ret();
                            }
                            子Setに要素があれば例外を発生させる(RemoveRelationship_I);
                            if(親Table_子ForeignKey.Table==子Table_親ForeignKey.Table) {
                                //自信と同じSetに属するタプルなら無視する。他のタプルの参照があったら例外を発生させる
                                子Setに要素があれば例外を発生させる(InvalidateClearRelationship_I);
                            }
                            void 子Setに要素があれば例外を発生させる(ILGenerator I) {
                                I.Ldarg_0();
                                I.Ldfld(親Table_子ForeignKey_FieldBuilder);
                                I.Call(Reflection.ImmutableSet.get_Count);
                                var Brfalse_S = I.DefineLabel();
                                I.Brfalse_S(Brfalse_S);
                                I.Ldstr('\"'+親Table_子ForeignKey_FieldBuilder.Name+"\"の要素が存在したので削除できなかった。");
                                I.Newobj(Reflection.Exception.RelationshipException_ctor);
                                I.Throw();
                                I.MarkLabel(Brfalse_S);
                            }
                        }
                    }
                    var 自Table_Key_TypeBuilder=自Table.Key_TypeBuilder!;
                    Types2[0]=自Table_Key_TypeBuilder;
                    Types2[1]=Container_TypeBuilder;
                    var 自ProtectedPrimaryKey = TypeBuilder.GetField(
                        typeof(Entity<,>).MakeGenericType(Types2),
                        DifinitionEntity2_ProtectedPrimaryKey
                    );
                    var 自Table_FieldBuilder = 自Table.FieldBuilder!;
                    var 自Table_TypeBuilder = 自Table.TypeBuilder!;
                    Types1[0]=自Table_TypeBuilder;
                    var Set1 = typeof(Set<>).MakeGenericType(Types1);
                    var VoidAdd = TypeBuilder.GetMethod(
                        Set1,
                        DifinitionSet1_getVoidAdd
                    );
                    var VoidRemove = TypeBuilder.GetMethod(
                        Set1,
                        DifinitionSet1_getVoidRemove
                    );
                    var GetEnumerator = TypeBuilder.GetMethod(
                        typeof(ImmutableSet<>).MakeGenericType(Types1),
                        Reflection.ImmutableSet.GetEnumerator
                    );
                    var Enumerator_LocalType = typeof(ImmutableSet<>.Enumerator).MakeGenericType(Types1);
                    var Enumerator = Container_RelationValidate_I.DeclareLocal(Enumerator_LocalType);
                    Types2[0]=自Table_TypeBuilder;
                    Types2[1]=自Table_Key_TypeBuilder;
                    var Set2 = typeof(Set<,>).MakeGenericType(Types2);
                    var Set3 = 自Table_FieldBuilder.FieldType;
                    var 自Table_Schema = 自Table.Schema;
                    var 自Table_Schema_LocalBuilder = 自Table_Schema.LocalBuilder!;
                    foreach(var 親Table in Tables) {
                        var 親Table_Key_TypeBuilder = 親Table.Key_TypeBuilder!;
                        Types2[0]=親Table_Key_TypeBuilder;
                        Types2[1]=Container_TypeBuilder;
                        var ProtectedPrimaryKey = TypeBuilder.GetField(
                            typeof(Entity<,>).MakeGenericType(Types2),
                            DifinitionEntity2_ProtectedPrimaryKey
                        );
                        var 親Table_TypeBuilder = 親Table.TypeBuilder!;
                        Types2[0]=自Table.TypeBuilder!;
                        Types2[1]=自Table.Key_TypeBuilder!;
                        Types2[0]=親Table_TypeBuilder;
                        Types2[1]=親Table_Key_TypeBuilder;
                        var ContainsKey = TypeBuilder.GetMethod(
                            typeof(Set<,>).MakeGenericType(Types2),
                            DifinitionSet2_ContainsKey
                        );
                        Types2[0]=親Table_TypeBuilder;
                        Types2[1]=親Table_Key_TypeBuilder;
                        var Set2_TryGetValue = TypeBuilder.GetMethod(
                            typeof(Set<,>).MakeGenericType(Types2),
                            DifinitionSet2_TryGetValue
                        );
                        //var 親Table_Schema_LocalBuilder=親Table.Schema.LocalBuilder!;
                        //var 親Table_FieldBuilder=親Table.FieldBuilder!;
                        foreach(var 親Table_子ForeignKey in 親Table.子ForeignKeys) {
                            if(自Table!=親Table_子ForeignKey.Table) continue;
                            Debug.Assert(親Table_子ForeignKey.FieldBuilder!=null);
                            //子のSetを保持するフィールド
                            var 子Table_親ForeignKey = 親Table_子ForeignKey.対になるForeignKey1!;
                            var 子Table_親ForeignKey_FieldBuilder = 子Table_親ForeignKey.FieldBuilder!;
                            if(!親Table_子ForeignKey.IsNullable) {
                                Container_RelationValidate_I.Ldloc(自Table_Schema_LocalBuilder);
                                Container_RelationValidate_I.Ldfld(自Table_FieldBuilder);
                                Container_RelationValidate_I.Call(GetEnumerator);
                                Container_RelationValidate_I.Stloc(Enumerator);
                                Container_RelationValidate_I.BeginExceptionBlock();
                                var ループ開始 = Container_RelationValidate_I.DefineLabel();
                                Container_RelationValidate_I.Br(ループ開始);
                                var ループ先頭 = Container_RelationValidate_I.M_DefineLabel_MarkLabel();
                                Container_RelationValidate_I.Ldloca(Enumerator);
                                Container_RelationValidate_I.Ldfld(
                                    TypeBuilder.GetField(
                                        Enumerator_LocalType,
                                        Reflection.ImmutableSet.Enumerator.InternalCurrent
                                    )
                                );
                                var Enumerator_Current = Container_RelationValidate_I.M_DeclareLocal_Stloc(自Table_TypeBuilder!);

                                //Container_RelationValidate_I.Ldloc(Enumerator_Current);
                                //Container_RelationValidate_I.Ldfld(子Table_親ForeignKey.FieldBuilder!);
                                //var v = Container_RelationValidate_I.M_DeclareLocal_Stloc(親Table_TypeBuilder);
                                //Container_RelationValidate_I.Ldloc(v);
                                //Container_RelationValidate_I.Ldfld(ProtectedPrimaryKey);
                                //var w = Container_RelationValidate_I.M_DeclareLocal_Stloc(親Table_Key_TypeBuilder);

                                //Container_RelationValidate_I.Ldloc(親Table.Schema.LocalBuilder!);
                                //Container_RelationValidate_I.Ldfld(親Table.FieldBuilder!);
                                //Container_RelationValidate_I.Ldloc(Enumerator_Current);
                                //Container_RelationValidate_I.Ldfld(子Table_親ForeignKey.FieldBuilder!);
                                //Container_RelationValidate_I.Ldfld(ProtectedPrimaryKey);
                                ////Container_RelationValidate_I.Ldloc(w);
                                //Container_RelationValidate_I.Call(ContainsKey);
                                Container_RelationValidate_I.Ldloc(Enumerator_Current);
                                Container_RelationValidate_I.Ldfld(子Table_親ForeignKey_FieldBuilder);
                                //Container_RelationValidate_I.Ldnull();
                                Container_RelationValidate_I.Brfalse(ループ開始);
                                Container_RelationValidate_I.Ldstr($"{自Table_Schema.Name}.{自Table.Name}.{親Table_子ForeignKey.Name}に対応する{自Table.Name}がなかった。");
                                Container_RelationValidate_I.Newobj(Reflection.Exception.RelationshipException_ctor);
                                Container_RelationValidate_I.Throw();
                                Container_RelationValidate_I.MarkLabel(ループ開始);
                                Container_RelationValidate_I.Ldloca(Enumerator);
                                Container_RelationValidate_I.Call(TypeBuilder.GetMethod(Enumerator_LocalType,Reflection.ImmutableSet.Enumerator.MoveNext));
                                Container_RelationValidate_I.Brtrue(ループ先頭);
                                Container_RelationValidate_I.BeginFinallyBlock();
                                Container_RelationValidate_I.Ldloca(Enumerator);
                                Container_RelationValidate_I.Constrained(Enumerator_LocalType);
                                Container_RelationValidate_I.Callvirt(TypeBuilder.GetMethod(Enumerator_LocalType,typeof(ImmutableSet<>.Enumerator).GetMethod("Dispose")));
                                Container_RelationValidate_I.EndExceptionBlock();
                            }
                            Types1[0]=自Table.TypeBuilder!;
                            var ParentExtensions_Method = ParentExtensions.DefineMethod(
                                親Table_子ForeignKey.EscapedName,
                                MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.Static,
                                親Table_TypeBuilder,
                                Types1
                            );
                            ParentExtensions_Method.InitLocals=false;
                            ParentExtensions_Method.SetCustomAttribute(Extension_CustomAttributeBuilder);
                            ParentExtensions_Method.DefineParameter(1,ParameterAttributes.None,自Table.EscapedName);
                            var ParentExtensions_Method_I = ParentExtensions_Method.GetILGenerator();
                            ParentExtensions_Method_I.Ldarg_0();
                            ParentExtensions_Method_I.Ldfld(子Table_親ForeignKey_FieldBuilder);
                            ParentExtensions_Method_I.Ret();
                            {
                                var 子ForeignKey = 親Table_子ForeignKey.対になるForeignKey1!;
                                Label 親タプルにNULLを代入して正常進行;
                                var 正常進行 = AddRelationship_I.DefineLabel();
                                var 親ForeignKey_Table = 親Table_子ForeignKey.Table;
                                var 親LocalBuilder = AddRelationship_I.DeclareLocal(親Table.TypeBuilder!);
                                var 自己参照か = 親ForeignKey_Table==子ForeignKey.Table;
                                var 親Table_子ForeignKey_IsNullable=親Table_子ForeignKey.IsNullable;
                                if(親Table_子ForeignKey_IsNullable) {
                                    親タプルにNULLを代入して正常進行=AddRelationship_I.DefineLabel();
                                    //外部キーを構成する属性がNULLだった場合親は存在しなくてもよいので飛ばす。
                                    foreach(var 親Table_子ForeignKey_ForeignKeyColumn in 親Table_子ForeignKey.ListForeignKeyColumn) {
                                        var 親ForeignKeyColumn_FieldBuilder = 親Table_子ForeignKey_ForeignKeyColumn.FieldBuilder!;
                                        if(親Table_子ForeignKey_ForeignKeyColumn.IsNullableClass) {
                                            AddRelationship_I.Ldarg_0();
                                            AddRelationship_I.Ldfld(親ForeignKeyColumn_FieldBuilder);
                                            AddRelationship_I.Brfalse(親タプルにNULLを代入して正常進行);
                                        } else if(親ForeignKeyColumn_FieldBuilder.FieldType.IsNullable()) {
                                            AddRelationship_I.Ldarg_0();
                                            AddRelationship_I.Ldflda(親ForeignKeyColumn_FieldBuilder);
                                            AddRelationship_I.Call(親ForeignKeyColumn_FieldBuilder.FieldType.GetProperty(nameof(Nullable<Int32>.HasValue))!.GetMethod);
                                            AddRelationship_I.Brfalse(親タプルにNULLを代入して正常進行);
                                        }
                                    }
                                } else {
                                    親タプルにNULLを代入して正常進行=default;
                                    foreach(var 親Table_子ForeignKey_ForeignKeyColumn in 親Table_子ForeignKey.ListForeignKeyColumn) {
                                        Debug.Assert(!親Table_子ForeignKey_ForeignKeyColumn.IsNullableClass);
                                        Debug.Assert(!親Table_子ForeignKey_ForeignKeyColumn.FieldBuilder!.FieldType.IsNullable());
                                    }
                                }
                                if(自己参照か) {
                                    AddRelationship_I.Ldarg_0();
                                    AddRelationship_I.Ldflda(自ProtectedPrimaryKey);
                                    foreach(var 親Table_子ForeignKey_ForeignKeyColumn in 親Table_子ForeignKey.ListForeignKeyColumn) {
                                        var 親ForeignKeyColumn_FieldBuilder = 親Table_子ForeignKey_ForeignKeyColumn.FieldBuilder!;
                                        var 親ForeignKeyColumn_FieldBuilder_FieldType = 親ForeignKeyColumn_FieldBuilder.FieldType;
                                        AddRelationship_I.Ldarg_0();
                                        if(親ForeignKeyColumn_FieldBuilder_FieldType.IsNullable()) {
                                            AddRelationship_I.Ldflda(親ForeignKeyColumn_FieldBuilder);
                                            AddRelationship_I.Call(親ForeignKeyColumn_FieldBuilder_FieldType.GetMethod(nameof(Nullable<Int32>.GetValueOrDefault),Type.EmptyTypes));
                                        } else {
                                            AddRelationship_I.Ldfld(親ForeignKeyColumn_FieldBuilder);
                                        }
                                    }
                                    AddRelationship_I.Newobj(親ForeignKey_Table.Key_ctor!);
                                    AddRelationship_I.Call(自Table.Key_Equals!);
                                    var Equalsでfalseの時 = AddRelationship_I.DefineLabel();
                                    AddRelationship_I.Brfalse(Equalsでfalseの時);
                                    AddRelationship_I.Ldarg_0();
                                    AddRelationship_I.Stloc(親LocalBuilder);
                                    AddRelationship_I.Br(正常進行);
                                    AddRelationship_I.MarkLabel(Equalsでfalseの時);
                                }
                                AddRelationship_I.Ldarg_1();//Container_TypeBuilder
                                AddRelationship_I.Ldfld(親Table.Schema.FieldBuilder!);//dbo
                                AddRelationship_I.Ldfld(親Table.FieldBuilder!);//customer
                                foreach(var 親Table_子ForeignKey_ForeignKeyColumn in 親Table_子ForeignKey.ListForeignKeyColumn) {
                                    AddRelationship_I.Ldarg_0();
                                    var 親Table_子ForeignKey_親ForeignKeyColumn_FieldBuilder = 親Table_子ForeignKey_ForeignKeyColumn.FieldBuilder!;
                                    if(親Table_子ForeignKey_親ForeignKeyColumn_FieldBuilder.DeclaringType==自Table_Key_TypeBuilder) {
                                        AddRelationship_I.Ldflda(自ProtectedPrimaryKey);
                                    }
                                    var 親Table_子ForeignKey_親ForeignKeyColumn_FieldBuilder_FieldType = 親Table_子ForeignKey_親ForeignKeyColumn_FieldBuilder.FieldType;
                                    if(親Table_子ForeignKey_親ForeignKeyColumn_FieldBuilder_FieldType.IsNullable()) {
                                        AddRelationship_I.Ldflda(親Table_子ForeignKey_親ForeignKeyColumn_FieldBuilder);
                                        AddRelationship_I.Call(親Table_子ForeignKey_親ForeignKeyColumn_FieldBuilder_FieldType.GetMethod(nameof(Nullable<Int32>.GetValueOrDefault),Type.EmptyTypes));
                                    } else {
                                        AddRelationship_I.Ldfld(親Table_子ForeignKey_親ForeignKeyColumn_FieldBuilder);
                                    }
                                }
                                AddRelationship_I.Newobj(親Table.Key_ctor!);
                                var sb = this.sb;
                                sb.Clear();
                                sb.Append($"[{自Table.Name}].[");
                                foreach(var 親Table_子ForeignKey_ForeignKeyColumn in 親Table_子ForeignKey.ListForeignKeyColumn) {
                                    sb.Append($"{親Table_子ForeignKey_ForeignKeyColumn.Name},");
                                }
                                sb.Length--;
                                sb.Append($"]に対応するタプルが[{親Table.Name}]に存在しなかった。");
                                AddRelationship_I.Ldloca(親LocalBuilder);
                                AddRelationship_I.Call(Set2_TryGetValue);
                                AddRelationship_I.Brtrue_S(正常進行);
                                AddRelationship_I.Ldstr(sb.ToString());
                                AddRelationship_I.Newobj(Reflection.Exception.RelationshipException_ctor);
                                AddRelationship_I.Throw();
                                if(親Table_子ForeignKey_IsNullable) {
                                    AddRelationship_I.MarkLabel(親タプルにNULLを代入して正常進行);
                                    AddRelationship_I.Ldnull();
                                    AddRelationship_I.Stloc(親LocalBuilder);
                                }
                                AddRelationship_I.MarkLabel(正常進行);
                                親Table_子ForeignKey.親LocalBuilder=親LocalBuilder;
                            }
                        }
                    }
                    foreach(var 親Table in Tables) {
                        foreach(var 親Table_子ForeignKey in 親Table.子ForeignKeys) {
                            if(自Table!=親Table_子ForeignKey.Table) continue;
                            var 親LocalBuilder = 親Table_子ForeignKey.親LocalBuilder!;
                            {
                                var 親Table_子ForeignKey_FieldBuilder = 親Table_子ForeignKey.FieldBuilder!;
                                if(親Table_子ForeignKey.IsNullable) {
                                    AddRelationship_I.Ldloc(親LocalBuilder);
                                    var スキップ = AddRelationship_I.DefineLabel();
                                    AddRelationship_I.Brfalse_S(スキップ);
                                    共通AddRelationship1();
                                    AddRelationship_I.MarkLabel(スキップ);
                                } else {
                                    共通AddRelationship1();
                                }
                                void 共通AddRelationship1() {
                                    AddRelationship_I.Ldloc(親LocalBuilder);
                                    AddRelationship_I.Ldfld(親Table_子ForeignKey_FieldBuilder);
                                    AddRelationship_I.Ldarg_0();
                                    AddRelationship_I.Call(VoidAdd);
                                }
                            }
                            AddRelationship_I.Ldarg_0();
                            AddRelationship_I.Ldloc(親LocalBuilder);
                            var 自Table_親ForeignKey_FieldBuilder= 親Table_子ForeignKey.対になるForeignKey1!.FieldBuilder!;
                            AddRelationship_I.Stfld(自Table_親ForeignKey_FieldBuilder);
                            //子.Count>0だと例外を発生させる
                            //this→子→thisを削除
                            if(親Table_子ForeignKey.IsNullable) {
                                RemoveRelationship_I.Ldarg_0();
                                RemoveRelationship_I.Ldfld(自Table_親ForeignKey_FieldBuilder);
                                var スキップ = RemoveRelationship_I.DefineLabel();
                                RemoveRelationship_I.Brfalse(スキップ);
                                親タプルの子Setから自身を削除();
                                RemoveRelationship_I.MarkLabel(スキップ);
                            } else {
                                親タプルの子Setから自身を削除();
                            }
                            void 親タプルの子Setから自身を削除() {
                                RemoveRelationship_I.Ldarg_0();
                                RemoveRelationship_I.Ldfld(自Table_親ForeignKey_FieldBuilder);
                                RemoveRelationship_I.Ldfld(親Table_子ForeignKey.FieldBuilder!);
                                RemoveRelationship_I.Ldarg_0();
                                RemoveRelationship_I.Call(VoidRemove);
                            }
                        }
                    }
                    AddRelationship_I.Ret();
                    RemoveRelationship_I.Ret();
                    自Table.ctor_I!.Ret();
                    InvalidateClearRelationship_I.Ret();
                }
                Container_RelationValidate_I.Ret();
                Container_Transaction_I.Ldarg_0();
                Container_Transaction_I.Newobj(Catalog_ctor1);
                var Result = Container_Transaction_I.M_DeclareLocal_Stloc(Container_TypeBuilder);
                Container_Transaction_I.Ldarg_0();
                Container_Transaction_I.Ldloc(Result);
                Container_Transaction_I.Call(Container_Copy);
                Container_Transaction_I.Ldloc(Result);
                Container_Transaction_I.Ret();
                Container_Copy_I.Ret();
                Container_ctor0_I.Ret();
                Container_ctor1_I.Ret();
                Container_ctor2_I.Ret();
                ParentExtensions.CreateType();
                ChildExtensions.CreateType();
                Catalog.Type=Container_TypeBuilder.CreateType();
            }
            foreach(var Catalog in this.Catalogs) {
                var Catalog_EscapedName = Catalog.EscapedName;
                var Catalog_EscapedName_Views_Dot = Catalog_EscapedName+".Views.";
                var Catalog_EscapedName_Dot = Catalog_EscapedName+'.';
                foreach(var Schema in Catalog.Schemas) {
                    foreach(var Table in Schema.Tables) {
                //        foreach(var Table in Schema.Tables.Where(
                //    p =>
                //        p.Name.StartsWith("Table_1")||
                //        p.Name.StartsWith("Table_2")||
                //        p.Name.StartsWith("Table_3")
                //)) {
                        Table.TypeBuilder!.CreateType();
                    }
                }
                //var Container_Type=Container_TypeBuilder.Type!;
                //foreach(var Schema in Container_TypeBuilder.SortedDictionarySchema.Values) {
                //    var Schema_EscapeName = Schema.EscapedName;
                //    var Schema_Type = Container_Type.GetField(Schema_EscapeName).FieldType;// Schema.CreateType();
                //    var Catalog_Tables_Schema_Dot = Catalog_EscapedName_Views_Dot+Schema_EscapeName+'.';
                //    var Catalog_Tables_Dot = Catalog_EscapedName_Dot+"Tables.";
                //    var ImplSchema_TypeBuilder = ModuleBuilder.DefineType(
                //        Catalog_EscapedName_Dot+"Schemas."+Schema_EscapeName+"#{
                //        TypeAttributes.NotPublic|TypeAttributes.Serializable
                //    );
                //    ImplSchema_TypeBuilder.SetParent(Schema_Type);
                //    var (ImplSchema_ctor, ImplSchema_ctor_I)=コンストラクタ開始引数名(ImplSchema_TypeBuilder,MethodAttributes.Assembly,Types_Container,Catalog_EscapedName);
                //    ImplSchema_ctor_I.Ldarg_0();
                //    ImplSchema_ctor_I.Ldarg_1();
                //    ImplSchema_ctor_I.Call(Schema_Type.GetConstructor(BindingFlags.NonPublic|BindingFlags.Instance,null,Types_Container,null));
                //    ImplSchema_ctor_I.Ret();
                //    //foreach(var Table in Schema.SortedDictionaryTable.Values) {
                //    //    Console.WriteLine(Table.EscapedName);
                //    //    Table.KeyTypeBuilder.CreateType();
                //    //    Table.TypeBuilder.CreateType();
                //    //}
                //    ImplContainer_Init_I.Ldarg_0();
                //    ImplContainer_Init_I.Dup();
                //    ImplContainer_Init_I.Newobj(ImplSchema_ctor);
                //    ImplContainer_Init_I.Stfld(Container_Type.GetField(Schema_EscapeName));



















                //    var Catalog_Views_Schema_Dot = Catalog_EscapedName_Views_Dot+Schema_EscapeName+'.';
                //    var Catalog_Views_Dot = Catalog_EscapedName_Dot+"Views.";
                //    //var Schema_Type=Schema.TypeBuilder.CreateType();
                //    foreach(var View in Schema.SortedDictionaryView.Values) {
                //        var View_EscapedName = View.EscapedName;
                //        var View_Type = View.TypeBuilder!.CreateType();
                //        var ViewImpl_TypeBuilder = ModuleBuilder.DefineType(
                //            Catalog_Views_Schema_Dot+View_EscapedName+"#{
                //            TypeAttributes.NotPublic|TypeAttributes.Serializable
                //        );
                //        ViewImpl_TypeBuilder.SetParent(View_Type);
                //        var ListColumn = View.ListColumn;
                //        var ListColumn_Count = ListColumn.Count;
                //        var View_ctor_parameterTypes = new Type[ListColumn_Count];
                //        for(var a = 0;a<ListColumn_Count;a++) {
                //            View_ctor_parameterTypes[a]=ListColumn[a].Type;
                //        }
                //        var ViewImpl_ctor = ViewImpl_TypeBuilder.DefineConstructor(
                //            MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.SpecialName,
                //            CallingConventions.HasThis,
                //            View_ctor_parameterTypes
                //        );
                //        var ViewImpl_ctor_I = ViewImpl_ctor.GetILGenerator();
                //        ViewImpl_ctor_I.Ldarg(0);
                //        for(var a = 0;a<ListColumn_Count;) {
                //            var Column = ListColumn[a];
                //            a++;
                //            ViewImpl_ctor_I.Ldarg((UInt16)a);
                //            var Parameter = ViewImpl_ctor.DefineParameter(
                //                a,
                //                ParameterAttributes.None,
                //                Column.Name
                //            );
                //            if(Column.IsNullableClass) {
                //                Parameter.SetCustomAttribute(
                //                    Nullable_CustomAttributeBuilder
                //                );
                //            }
                //        }
                //        ViewImpl_ctor_I.Call(View_Type.GetConstructor(View_ctor_parameterTypes));
                //        ViewImpl_ctor_I.Ret();
                //        Types1[0]=View_Type;
                //        {
                //            var GetMethod = ImplSchema_TypeBuilder.DefineMethod(
                //                View_EscapedName,
                //                MethodAttributes.Public|MethodAttributes.Virtual,
                //                typeof(ImmutableSet<>).MakeGenericType(Types1),
                //                Type.EmptyTypes
                //            );
                //            ImplSchema_TypeBuilder.DefineMethodOverride(
                //                GetMethod,
                //                Schema_Type.GetMethod(View_EscapedName,BindingFlags.Public|BindingFlags.Instance)
                //            );
                //            var I = GetMethod.GetILGenerator();
                //            Types1[0]=View_Type;
                //            try {
                //                if(View.SQL.Contains($"CREATE VIEW sys.dm_db_fts_index_physical_stats AS")) {

                //                }
                //                Optimizer.CreateImplView(Container_Parameter,I,View.SQL);
                //            } catch(Data.SyntaxErrorException) {
                //                I.Ldnull();
                //                I.Ret();
                //            } catch(NotSupportedException) {
                //                I.Ldnull();
                //                I.Ret();
                //            } catch(NullReferenceException) {
                //                I.Ldnull();
                //                I.Ret();
                //            }
                //            ViewImpl_TypeBuilder.CreateType();
                //        }
                //        //var View_PropertyType = typeof(ImmutableSet<>).MakeGenericType(Types1);
                //        //var View_Property = Schema.DefineProperty(
                //        //    View_TypeBuilder.Name,
                //        //    PropertyAttributes.None,
                //        //    View_PropertyType,
                //        //    Types1
                //        //);
                //        //var Get = Schema.DefineMethod(
                //        //    View_TypeBuilder.Name,
                //        //    MethodAttributes.FamANDAssem,//|MethodAttributes.Abstract,
                //        //    View_PropertyType,
                //        //    Types1
                //        //);
                //        //View_Property.SetGetMethod(Get);
                //        //var I = Get.GetILGenerator();
                //        ////I.Newobj(
                //        ////    TypeBuilder.GetConstructor(
                //        ////        typeof(Set<>).MakeGenericType(Types1),
                //        ////        Set1_ctor
                //        ////    )
                //        ////);
                //        ////var o = new Optimizers.Optimizer();
                //        ////var TypeGenerator.DefineMethod(View_EscapeName,MethodAttributes.Public,View,Type.EmptyTypes,null);
                //        ////o.CreateView(TypeGenerator,View_EscapeName,View,View2.Value.SQL);
                //        //I.Ldnull();
                //        //I.Ret();
                //    }












                //    ImplSchema_TypeBuilder.CreateType();
                //}
                //ImplContainer_Init_I.Ret();
                //ImplContainer.CreateType();
            }
#if NETFRAMEWORK
            DynamicAssembly.Save(アセンブリ名+".dll");
#endif
        }
        private readonly HashSet<String> HashSetSchemaName = new HashSet<String>();
        public void OutputCS() {
            var パス = Path.GetDirectoryName(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(Environment.CurrentDirectory)
                )
            );
            foreach(var Catalog in this.SortedDictionaryCotalog) {
                var CatalogName = Escape(Catalog.Key);
                using(var Writer = new StreamWriter(パス+'/'+CatalogName+".cs")) {
                    Write                           (Writer,$"#pragma warning disable CS0109 // メンバーは継承されたメンバーを非表示にしません。new キーワードは不要です");
                    var SortedDictionarySchema = Catalog.Value.SortedDictionarySchema;
                    var SortedDictionary_SeldChild = Catalog.Value.SortedDictionary_SelfChild;
                    var SortedDictionary_SelfParent = Catalog.Value.SortedDictionary_SelfParent;
                    Write                           (Writer,$"namespace {CatalogName}");
                    Write                           (Writer,$"    public static class ChildExtensions{{");
                    foreach(var Schema in SortedDictionarySchema) {
                        var SchemaName = Escape(Schema.Key);
                        var SchemaNameDot = SchemaName+'.';
                        foreach(var Table in Schema.Value.SortedDictionaryTable) {
                            if(SortedDictionary_SeldChild.TryGetValue((Schema.Key, Table.Key),out var SortedDictionary_SelfChild_ForeignKey0)) {
                                var TableName = Escape(Table.Key);
                                var SchemaTableName = SchemaNameDot+TableName;
                                foreach(var ForeignKey in SortedDictionary_SelfChild_ForeignKey0.Values) {
                                    var ForeignKeyName = ForeignKey.EscapedName;
                                    Write           (Writer,$"        public static global::LinqDB.Sets.ASet<Tables.{ForeignKey.Schema.EscapedName}.{ForeignKey.Table.EscapedName}>{ForeignKeyName}(this Tables.{SchemaTableName} {TableName})=>{TableName}.{ForeignKeyName};");
                                }
                            }
                        }
                    }
                    Write                           (Writer,$"    }}");
                    Write                           (Writer,$"    public static class ParentExtensions{{");
                    foreach(var Schema in SortedDictionarySchema) {
                        var SchemaName = Escape(Schema.Key);
                        var SchemaNameDot = SchemaName+'.';
                        foreach(var Table in Schema.Value.SortedDictionaryTable) {
                            if(SortedDictionary_SelfParent.TryGetValue((Schema.Key, Table.Key),out var SortedDictionary_SelfParent_ForeignKey)) {
                                var TableName = Escape(Table.Key);
                                var SchemaTableName = SchemaNameDot+TableName;
                                foreach(var ForeignKey in SortedDictionary_SelfParent_ForeignKey.Values) {
                                    var ForeignTableName = ForeignKey.Table.EscapedName;
                                    var ForeignSchemaTableName = ForeignKey.Schema.EscapedName+"."+ForeignTableName;
                                    var ForeignKeyName = ForeignKey.EscapedName;
                                    Write           (Writer,$"        public static Tables.{ForeignSchemaTableName} {ForeignKeyName}(this Tables.{SchemaTableName} {TableName})=>{TableName}.{ForeignKeyName};");
                                }
                            }
                        }
                    }
                    Write                           (Writer,$"    }}");
                    Write                           (Writer,$"    namespace PrimaryKeys{{");
                    foreach(var Schema in SortedDictionarySchema) {
                        var SchemaName = Escape(Schema.Key);
                        //var SchemaNameDot = SchemaName+'.';
                        Write                        (Writer,$"        namespace {{{SchemaName}{{");
                        foreach(var Table in Schema.Value.SortedDictionaryTable) {
                            var ListPrimaryKeyColumn = Table.Value.ListPrimaryKeyColumn;
                            var TableName = Escape(Table.Key);
                            Write                   (Writer,$"            [global::System.Serializable]");
                            Write                   (Writer,$"            public struct {TableName}:global::System.IEquatable<{TableName}>{{");
                            //foreach(var Column in Table.Value.ListColumn) {
                            //    var ColumnName = Column.Name;
                            //    if(ColumnName==TableName) {
                            //        Column.Name=ColumnName+this.番号++;
                            //    }
                            //}
                            if(ListPrimaryKeyColumn.Count==0) {
                                var ListColumn = Table.Value.ListColumn;
                                ListPrimaryKeyColumn.AddRange(ListColumn);
                            }
                            foreach(var PrimaryKeyColumn in ListPrimaryKeyColumn) {
                                Write               (Writer,$"                public {PrimaryKeyColumn.TypeFullName} {PrimaryKeyColumn.Name};");
                            }
                            Writer.Write            ($"                public ");
                            Writer.Write            (TableName);
                            Writer.Write            ('(');
                            var ListPrimaryKeyColumn_Count = ListPrimaryKeyColumn.Count;
                            var a = 0;
                            while(true) {
                                var PrimaryKeyColumn=ListPrimaryKeyColumn[a];
                                Writer.Write        (PrimaryKeyColumn.TypeFullName);
                                Writer.Write        (' ');
                                Writer.Write        (PrimaryKeyColumn.Name);
                                a++;
                                if(a==ListPrimaryKeyColumn_Count) {
                                    break;
                                }
                                Writer.Write        (',');
                            }
                            Writer.Write            ($"){{");
                            foreach(var PrimaryKeyColumn in ListPrimaryKeyColumn) {
                                var Name = PrimaryKeyColumn.Name;
                                Write               (Writer,$"                    this.{Name}={Name};");
                            }
                            Write                   (Writer,$"                }}");
                            Write                   (Writer,$"                internal void ToStringBuilder(global::System.Text.StringBuilder sb){{");
                            foreach(var PrimaryKeyColumn in ListPrimaryKeyColumn) {
                                var Name = PrimaryKeyColumn.Name;
                                Write               (Writer,$"                    sb.Append(nameof(this.{Name}),\"=\"+this.{Name});");
                            }
                            Write                   (Writer,$"                }}");
                            Write                   (Writer,$"                public override global::System.String ToString(){{");
                            Write                   (Writer,$"                    var sb=new global::System.Text.StringBuilder();");
                            Write                   (Writer,$"                    this.ToStringBuilder(sb);");
                            Write                   (Writer,$"                    return sb.ToString();");
                            Write                   (Writer,$"                }}");
                            Write                   (Writer,$"                internal void InputHashCode(ref global::LinqDB.CRC.CRC32 CRC){{");
                            foreach(var PrimaryKeyColumn in ListPrimaryKeyColumn) {
                                Write               (Writer,$"                    CRC.Input(this.{PrimaryKeyColumn.Name});");
                            }               
                            Write                   (Writer,$"                }}");
                            if(ListPrimaryKeyColumn.Count==1) {
                                var Column0 = ListPrimaryKeyColumn.Single();
                                if(typeof(Byte)==Column0.Type||typeof(Int16)==Column0.Type||typeof(Int32)==Column0.Type) {
                                    Write           (Writer,$"                public override global::System.Int32 GetHashCode()=>this.{Column0.Name};");
                                } else {    
                                    Write           (Writer,$"                public override global::System.Int32 GetHashCode()=>this.{Column0.Name}.GetHashCode();");
                                }           
                            } else {        
                                Write               (Writer,$"                public override global::System.Int32 GetHashCode(){{");
                                Write               (Writer,$"                    var CRC=new global::LinqDB.CRC.CRC32();");
                                foreach(var Column0 in ListPrimaryKeyColumn) {
                                    Write           (Writer,$"                    CRC.Input(this.{Column0.Name});");
                                }           
                                Write               (Writer,$"                    return CRC.GetHashCode();");
                                Write               (Writer,$"                }}");
                            }               
                            Write                   (Writer,$"                public global::System.Boolean Equals({TableName} other){{");
                            foreach(var Column0 in ListPrimaryKeyColumn) {
                                var Column0_Name=Column0.Name;
                                Write               (Writer,$"                    if(!this.{Column0_Name}.Equals(other.{Column0_Name}))return false;");
                            }               
                            Write                   (Writer,$"                    return true;");
                            Write                   (Writer,$"                }}");
                            Write                   (Writer,$"                public override global::System.Boolean Equals(global::System.Object obj)=>this.Equals(({TableName})obj);");
                            Write                   (Writer,$"                public static global::System.Boolean operator==({TableName} x,{TableName} y)=> x.Equals(y);");
                            Write                   (Writer,$"                public static global::System.Boolean operator!=({TableName} x,{TableName} y)=>!x.Equals(y);");
                            Write                   (Writer,$"            }}");
                        }
                        Write                       (Writer,$"        }}");
                    }
                    Write                           (Writer,$"    }}//end PrimaryKeys");
                    Write                           (Writer,$"    namespace BaseTables{{");
                    foreach(var Schema in SortedDictionarySchema) {
                        var SchemaName = Escape(Schema.Key);
                        var SchemaNameDot = SchemaName+'.';
                        Write                       (Writer,$"        namespace {SchemaName}{{");
                        foreach(var Table in Schema.Value.SortedDictionaryTable) {
                            var TableName = Escape(Table.Key);
                            Write                   (Writer,$"            [global::System.Serializable]");
                            Write                   (Writer,$"            public abstract class {TableName}:global::LinqDB.Sets.AEntity<global::{CatalogName}.PrimaryKeys.{SchemaNameDot}{TableName},Container>,global::System.IEquatable<{TableName}>{{");
                            var ListPrimaryKeyColumn = Table.Value.ListPrimaryKeyColumn;
                            var ListColumn = Table.Value.ListColumn;
                            foreach(var Column in ListColumn) {
                                if(ListPrimaryKeyColumn.Any(p => p.Name==Column.Name)) {
                                    Write           (Writer,$"                public {Column.TypeFullName} {Column.Name}=>this.PrimaryKey.{Column.Name};");
                                } else {
                                    Write           (Writer,$"                public readonly {Column.TypeFullName} {Column.Name};");
                                }
                            }
                            Writer.Write            ($"                public ");
                            Writer.Write            (TableName);
                            Writer.Write            ('(');
                            var ListColumn_Count = ListColumn.Count;
                            var a = 0;
                            while(true) {
                                var Column = ListColumn[a];
                                Writer.Write        (Column.Type);
                                Writer.Write        (' ');
                                Writer.Write        (Column.Name);
                                a++;
                                if(a==ListColumn_Count) {
                                    break;
                                }
                                Writer.Write        (',');
                            }                       
                            Writer.Write            ($"):base(new PrimaryKeys.");
                            Writer.Write            (SchemaNameDot);
                            Writer.Write            (TableName);
                            Writer.Write            ($"($");
                            var ListPrimaryKeyColumn_Count = ListPrimaryKeyColumn.Count;
                            a = 0;
                            while(true) {
                                var PrimaryKeyColumn = ListPrimaryKeyColumn[a];
                                Writer.Write(PrimaryKeyColumn.Name);
                                a++;
                                if(a==ListPrimaryKeyColumn_Count) {
                                    break;
                                }
                                Writer.Write        (',');
                            }
                            Writer.WriteLine        ($")){{");
                            foreach(var Column in ListColumn) {
                                var ColumnName = Column.Name;
                                if(ListPrimaryKeyColumn.All(p => p.Name != ColumnName)) {
                                    Write           (Writer,$"                    this.{ColumnName}={ColumnName};");
                                }
                            }
                            Write                   (Writer,$"                }}");
                            Write                   (Writer,$"                public global::System.Boolean Equals({TableName} other){{");
                            Write                   (Writer,$"                    if(other==null)return false;");
                            Write                   (Writer,$"                    if(!this.PrimaryKey.Equals(other.PrimaryKey))return false;");
                            foreach(var Column in ListColumn) {
                                if(ListPrimaryKeyColumn.All(p => p.Name != Column.Name)) {
                                    Write           (Writer,$"                    if(!this.{Column.Name}.Equals(other.{Column.Name}))return false;");
                                }
                            }
                            Write                   (Writer,$"                    return true;");
                            Write                   (Writer,$"                }}");
                            Write                   (Writer,$"                public override global::System.Boolean Equals(global::System.Object other)=>this.Equals(other as {TableName});");
                            Write                   (Writer,$"                public static global::System.Boolean operator==({TableName} x,{TableName} y)=> x.Equals(y);");
                            Write                   (Writer,$"                public static global::System.Boolean operator!=({TableName} x,{TableName} y)=>!x.Equals(y);");
                            Write                   (Writer,$"                protected override void ToStringBuilder(global::System.Text.StringBuilder sb){{");
                            Write                   (Writer,$"                    this.PrimaryKey.ToStringBuilder(sb);");
                            foreach(var Column in ListColumn) {
                                var ColumnName = Column.Name;
                                Write               (Writer,$"                    sb.Append(\"+nameof(this.{ColumnName})+\"=\"+this.{ColumnName});");
                            }
                            Write                   (Writer,$"                }}");
                            Write                   (Writer,$"            }}//end class");
                        }
                        Write                       (Writer,$"        }}");
                    }
                    Write                           (Writer,$"    }}//BaseTables");
                    Write                           (Writer,$"    namespace Tables{{");
                    foreach(var Schema in SortedDictionarySchema) {
                        var SchemaName = Escape(Schema.Key);
                        var SchemaNameDot = SchemaName+'.';
                        Write                       (Writer,$"        namespace {{{SchemaName}{{");
                        foreach(var Table in Schema.Value.SortedDictionaryTable) {
                            var TableName = Escape(Table.Key);
                            Write                   (Writer,$"            [global::System.Serializable]");
                            Write                   (Writer,$"            public sealed class {TableName}:global::{CatalogName}.BaseTables.{SchemaNameDot}{TableName}{{");
                            //var ListPrimaryKeyColumn = Table.Value.ListPrimaryKeyColumn;
                            var ListColumn = Table.Value.ListColumn;
                            var ListColumn_Count = ListColumn.Count;
                            Writer.Write            (       $"                public {TableName}(");
                            {
                                var a = 0;
                                while(true) {
                                    var Column=ListColumn[a];
                                    Writer.Write    (Column.Type);
                                    Writer.Write    (' ');
                                    Writer.Write    (Column.Name);
                                    a++;
                                    if(a==ListColumn_Count) {
                                        break;
                                    }
                                    Writer.Write    (',');
                                }
                                Writer.Write        ($"):base($");
                                a = 0;
                                while(true) {
                                    var Column = ListColumn[a];
                                    Writer.Write    (Column.Name);
                                    a++;
                                    if(a==ListColumn_Count) {
                                        break;
                                    }
                                    Writer.Write    (',');
                                }
                            }
                            Writer.WriteLine        ($"){{}}");
                            SortedDictionary<String,ForeignKey> SortedDictionary_SelfParent_ForeignKey;
                            void AddRelationship(String Relationshipメソッド名,String Addメソッド名) {
                                Write               (Writer,$"                protected override void {Relationshipメソッド名}(Container Container){{");
                                foreach(var SelfParent_ForeignKey in SortedDictionary_SelfParent_ForeignKey) {
                                    var ParentTable = SelfParent_ForeignKey.Value.Table;
                                    Writer.Write    ($"                    (this.{SelfParent_ForeignKey.Value.EscapedName}=Container.{ParentTable.SchemaTableName}.GetReference(new PrimaryKeys.{ParentTable.Schema.EscapedName}.{ParentTable.EscapedName}(");
                                    var ForeignKeyName = SelfParent_ForeignKey.Value.EscapedName;
                                    if(SortedDictionary_SeldChild.TryGetValue((Schema.Key, Table.Key),out var SortedDictionary_SelfChild_Foreign)&&SortedDictionary_SelfChild_Foreign.TryGetValue(SelfParent_ForeignKey.Key,out var SelfChildForeign)) {
                                        ForeignKeyName=SelfChildForeign.EscapedName;
                                    }
                                    var ListForeignKeyColumn = SelfParent_ForeignKey.Value.ListForeignKeyColumn;
                                    var ListForeignKeyColumn_Count = ListForeignKeyColumn.Count;
                                    var a = 0;
                                    while(true) {
                                        var ForeignKeyColumn = ListForeignKeyColumn[a];
                                        Writer.Write($"this.");
                                        Writer.Write(ForeignKeyColumn.Name);
                                        a++;
                                        if(a==ListForeignKeyColumn_Count) {
                                            break;
                                        }
                                        Writer.Write(',');
                                    }
                                    Write           (Writer,$"))).{ForeignKeyName}.{Addメソッド名}(this);");
                                }
                                Write               (Writer,$"                }}");
                            }
                            void RemoveRelationship(String Relationshipメソッド名,String Removeメソッド名) {
                                Write               (Writer,$"                protected override void {Relationshipメソッド名}(){{");
                                foreach(var SelfParent_ForeignKey in SortedDictionary_SelfParent_ForeignKey) {
                                    var SelfName = SelfParent_ForeignKey.Value.EscapedName;
                                    if(SortedDictionary_SeldChild.TryGetValue((Schema.Key, Table.Key),out var SortedDictionary_SelfChild_ForeignKey)&&SortedDictionary_SelfChild_ForeignKey.TryGetValue(SelfParent_ForeignKey.Key,out var SelfChildForeign)) {
                                        SelfName=SelfChildForeign.EscapedName;
                                    }
                                    Write           (Writer,$"                    this.{SelfParent_ForeignKey.Value.EscapedName}.{SelfName}.{Removeメソッド名}(this);");
                                }
                                Write               (Writer,$"                }}");
                            }
                            if(SortedDictionary_SelfParent.TryGetValue((Schema.Key, Table.Key),out SortedDictionary_SelfParent_ForeignKey!)) {
                                AddRelationship("AddRelationship","VoidAdd");
                                RemoveRelationship("RemoveRelationship","Remove");
                            } else {
                                Write               (Writer,$"                protected override void AddRelationship(Container Container){{}}");
                                Write               (Writer,$"                protected override void RemoveRelationship(){{}}");
                            }
                            if(SortedDictionary_SeldChild.TryGetValue((Schema.Key, Table.Key),out var SortedDictionary_SelfChild_ForeignKey1)) {
                                foreach(var ForeignKey in SortedDictionary_SelfChild_ForeignKey1.Values) {
                                    Write           (Writer,$"                //1→*");
                                    Write           (Writer,$"                [global::System.NonSerialized]");
                                    var ForeignSchemaTableName = ForeignKey.Schema.EscapedName+"."+ForeignKey.Table.EscapedName;
                                    Write           (Writer,$"                internal readonly global::LinqDB.Sets.Set<{ForeignSchemaTableName}>{ForeignKey.EscapedName}=new global::LinqDB.Sets.Set<{ForeignSchemaTableName}>();");
                                }
                            }
                            if(SortedDictionary_SelfParent.TryGetValue((Schema.Key, Table.Key),out var SortedDictionary_SelfParent_ForeignKey1)) {
                                foreach(var ForeignKey in SortedDictionary_SelfParent_ForeignKey1.Values) {
                                    Write           (Writer,$"                //*→1");
                                    Write           (Writer,$"                [global::System.NonSerialized]");
                                    Write           (Writer,$"                internal {ForeignKey.Schema.EscapedName}.{ForeignKey.Table.EscapedName} {ForeignKey.EscapedName}=default;");
                                }
                            }
                            Write                   (Writer,$"            }}");
                        }
                        Write                       (Writer,$"        }}");
                    }
                    Write                           (Writer,$"    }}//end Tables");
                    Write                           (Writer,$"    namespace Schemas{{");
                    foreach(var Schema in SortedDictionarySchema) {
                        var SchemaName = Escape(Schema.Key);
                        Write                       (Writer,$"        [global::System.Serializable]");
                        Write                       (Writer,$"        public sealed class {SchemaName}{{");
                        var SortedDictionaryTable = Schema.Value.SortedDictionaryTable;
                        foreach(var Table in SortedDictionaryTable) {
                            var TableName = Escape(Table.Key);
                            Write                   (Writer,$"            public global::LinqDB.Sets.Set<Tables.{SchemaName}.{TableName},PrimaryKeys.{SchemaName}.{TableName},Container>{TableName}{{get;}}");
                        }
                        Write                       (Writer,$"            public {SchemaName}(Container Container){{");
                        foreach(var Table in SortedDictionaryTable) {
                            var TableName = Escape(Table.Key);
                            Write                   (Writer,$"                this.{TableName}=new global::LinqDB.Sets.Set<Tables.{SchemaName}.{TableName},PrimaryKeys.{SchemaName}.{TableName},Container>(Container);");
                        }
                        Write                       (Writer,$"            }}");
                        var Keys = SortedDictionaryTable.Keys;
                        Write                       (Writer,$"            internal void Read(global::System.Xml.XmlDictionaryReader Reader){{");
                        foreach(var Key in Keys) {
                            Write                   (Writer,$"                this.{Escape(Key)}.Read(Reader);");
                        }
                        Write                       (Writer,$"            }}");
                        Write                       (Writer,$"            internal void Write(global::System.Xml.XmlDictionaryWriter Writer){{");
                        foreach(var Key in Keys) {
                            Write                   (Writer,$"                this.{Escape(Key)}.Write           (Writer);");
                        }
                        Write                       (Writer,$"            }}");
                        Write                       (Writer,$"            internal void Assign({SchemaName} To){{");
                        foreach(var Table in SortedDictionaryTable) {
                            var TableName = Table.Value.EscapedName;
                            Write                   (Writer,$"                To.{TableName}.Assign(this.{TableName});");
                        }
                        Write                       (Writer,$"            }}");
                        Write                       (Writer,$"            internal void Commit(global::System.Xml.XmlDictionaryWriter Writer){{");
                        foreach(var Key in Keys) {
                            Write                   (Writer,$"                this.{Escape(Key)}.Write           (Writer);");
                        }
                        Write                       (Writer,$"            }}");
                        Write                       (Writer,$"            internal void UpdateRelationship(){{");
                        foreach(var Key in Keys) {
                            Write                   (Writer,$"                this.{Escape(Key)}.UpdateRelationship();");
                        }
                        Write                       (Writer,$"            }}");
                        Write                       (Writer,$"        }}");
                    }
                    Write                           (Writer,$"    }}//end Schemas");
                    Write                           (Writer,$"    [global::System.Serializable]");
                    Write                           (Writer,$"    public sealed class Container:global::LinqDB.Databases.Container<Container>{{");
                    var SortedDictionarySchema_Keys = SortedDictionarySchema.Keys;
                    foreach(var Key in SortedDictionarySchema_Keys) {
                        var Name = Escape(Key);
                        Write                       (Writer,$"        public Schemas.{Name} {Name}{{get;private set;}}");
                    }
                    Write                           (Writer,$"        public Container():base(default)=>this.Init();");
                    Write                           (Writer,$"        public Container(Container Parent):base(Parent)=>this.Init();");
                    Write                           (Writer,$"        public Container(global::System.Xml.XmlDictionaryReader Reader,global::System.Xml.XmlDictionaryWriter Writer):base(Reader,Writer){{}}");
                    Write                           (Writer,$"        protected override void Init(){{");
                    foreach(var Key in SortedDictionarySchema_Keys) {
                        var Name = Escape(Key);
                        Write                       (Writer,$"            this.{Name}=new Schemas.{Name}(this);");
                    }
                    Write                           (Writer,$"        }}");
                    Write                           (Writer,$"        public override Container Transaction(){{");
                    Write                           (Writer,$"            var Container=new Container(this);");
                    Write                           (Writer,$"            this.Copy(Container);");
                    Write                           (Writer,$"            return Container;");
                    Write                           (Writer,$"        }}");
                    Write                           (Writer,$"        protected override void Read(global::System.Xml.XmlDictionaryReader Reader){{");
                    foreach(var Key in SortedDictionarySchema_Keys) {
                        Write                       (Writer,$"            this.{Escape(Key)}.Read(Reader);");
                    }
                    Write                           (Writer,$"        }}");
                    Write                           (Writer,$"        protected override void Write(global::System.Xml.XmlDictionaryWriter Writer){{");
                    foreach(var Key in SortedDictionarySchema_Keys) {
                        Write                       (Writer,$"            this.{Escape(Key)}.Write           (Writer);");
                    }
                    Write                           (Writer,$"        }}");
                    Write                           (Writer,$"        protected override void Copy(Container To){{");
                    foreach(var Key in SortedDictionarySchema_Keys) {
                        var Name = Escape(Key);
                        Write                       (Writer,$"            To.{Name}.Assign(this.{Name});");
                    }
                    Write                           (Writer,$"        }}");
                    Write                           (Writer,$"        protected override void Commit(global::System.Xml.XmlDictionaryWriter Writer){{");
                    foreach(var Key in SortedDictionarySchema_Keys) {
                        Write                       (Writer,$"            this.{Escape(Key)}.Write           (Writer);");
                    }
                    Write                           (Writer,$"        }}");
                    Write                           (Writer,$"        protected override void UpdateRelationship(){{");
                    foreach(var Key in SortedDictionarySchema_Keys) {
                        Write                       (Writer,$"            this.{Escape(Key)}.UpdateRelationship();");
                    }
                    Write                           (Writer,$"        }}");
                    Write                           (Writer,$"        public override void RelationValidate(){{");
                    var HashSetSchemaName = this.HashSetSchemaName;
                    HashSetSchemaName.Clear();
                    foreach(var Schema in SortedDictionarySchema) {
                        var SchemaName = Escape(Schema.Key);
                        if(HashSetSchemaName.Add(SchemaName)) {
                            Write                   (Writer,$"            var {SchemaName}=this.{SchemaName};");
                        }
                    }
                    Write                           (Writer,$"            //多対１");
                    foreach(var Schema in SortedDictionarySchema) {
                        var SchemaName = Escape(Schema.Key);
                        foreach(var Table in Schema.Value.SortedDictionaryTable) {
                            if(SortedDictionary_SelfParent.TryGetValue((Schema.Key, Table.Key),out var SortedDictionaryForeign)) {
                                var SchemaTableName = SchemaName+'.'+Escape(Table.Key);
                                Write               (Writer,$"            foreach(var a in {SchemaTableName}){{");
                                foreach(var Foreign in SortedDictionaryForeign.Values) {
                                    var Foreign_SchemaTableName = Foreign.SchemaTableName;
                                    Write           (Writer,$"                if(!{Foreign_SchemaTableName}.ContainsKey(a.{Foreign.EscapedName}.PrimaryKey))throw new global::LinqDB.Databases.RelationshipException(\"{SchemaTableName}\"に対応する{Foreign.EscapedName}.PrimaryKeyがなかった。\");");
                                }
                                Write               (Writer,$"            }}");
                            }
                        }
                    }
                    Write                           (Writer,$"        }}");
                    Write                           (Writer,$"    }}");
                    Write                           (Writer,$"}}");
                }
            }
        }
    }
}
