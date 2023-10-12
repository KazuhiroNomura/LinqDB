using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using Data = System.Data;
using System.Linq;
using System.Reflection;
using System.IO;
using LinqDB.Databases;
using System.Runtime.Loader;
using System.Runtime.InteropServices;
using GUI.VM;
using LinqDB.Databases.Dom;
using LinqDB.Helpers;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using AssemblyName=System.Reflection.AssemblyName;
using MySqlConnector;
namespace GUI;

/// <summary>
/// テーブルスキーマ編集.xaml の相互作用ロジック
/// </summary>
public partial class テーブルスキーマ編集:Window {
	private const string ホスト名= @"COFFEELAKE\MSSQLSERVER2019";
	private const string Windowsログイン = @"Integrated Security=SSPI;";
	private const string SQLServerログイン = @"User ID=sa;Password=SQLSERVER711409;";
	//private const String 接続文字列 = @"Data Source=localhost;Initial Catalog=master;Connect Timeout=60;Persist Security Info=True;User ID=sa;Password=password";
	//private const String 接続文字列 = @"Data Source=localhost;Initial Catalog=master;Connect Timeout=60;Persist Security Info=True;";
	private const string SQLServer接続文字列 = @$"Data Source={ホスト名};Initial Catalog=master;Integrated Security=false;{SQLServerログイン}";

    private const string MySQL接続文字列=@$"Server=localhost;Database=sakila;Uid=root;Pwd=password;";
	//private const string 接続文字列 = @"Data Source=COFFEELAKE\MSSQLSERVER2019;Initial Catalog=master;Integrated Security=True;";
	private readonly VM.Container Container = new();
	private readonly VM.Schema dbo;
	public テーブルスキーマ編集(){
		this.InitializeComponent();
		{
			var Parsers = new TSqlParser[] {
				new TSql80Parser(false),
				new TSql90Parser(false),
				new TSql100Parser(false),
				new TSql110Parser(false),
				new TSql120Parser(false),
				new TSql130Parser(false),
				new TSql140Parser(false),
				new TSql150Parser(false),
				new TSql160Parser(false),
				new TSql80Parser(true),
				new TSql90Parser(true),
				new TSql100Parser(true),
				new TSql110Parser(true),
				new TSql120Parser(true),
				new TSql130Parser(true),
				new TSql140Parser(true),
				new TSql150Parser(true),
				new TSql160Parser(true),
			};
			const string SQL = @"
			CREATE VIEW sys.dm_resource_governor_resource_pool_volumes AS
			SELECT *
			FROM OpenRowSet( DM_RG_POOL_VOLUMES)
			";
			foreach(var Parser in Parsers) {
				var Parsed = Parser.Parse(new StringReader(SQL),out var errors);
				if(errors!.Count>0) {
					Trace.WriteLine(Parser.GetType().Name);
					foreach(var error in errors!) {
						Trace.WriteLine(error.Message);
					}
				}
			}
		}
		//var ScriptGenerator = new Sql150ScriptGenerator(
		//    new SqlScriptGeneratorOptions {
		//        KeywordCasing=KeywordCasing.Lowercase,
		//        IncludeSemicolons=true,
		//        NewLineBeforeFromClause=true,
		//        NewLineBeforeJoinClause=true,
		//        NewLineBeforeWhereClause=true,
		//        NewLineBeforeGroupByClause=true,
		//        NewLineBeforeOrderByClause=true,
		//        NewLineBeforeHavingClause=true,
		//    }
		//);
		//ScriptGenerator.GenerateScript(Parsed,out var SQL2);
		
		var Container = this.Container;
		this.DataContext=Container;
		this.DiagramControl.DataContext=Container;
		//this.DiagramControl0.DataContext=Database;
		Container.Name="D0";
		this.dbo=Container.CreateSchema("dbo");
		//監視するディレクトリを指定
		var DllWatcher = new VM.DllWatcher(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!);
		this.DLL.DataContext=DllWatcher;
		{
			var S = Container.CreateSchema("S0");
			{
				var S0T0 = S.CreateTable("S0T0");
				S0T0.CreateColumn("S0T0K0",typeof(int),false,true);
				S0T0.CreateColumn("S0T0K1",typeof(int),false,true);
				S0T0.CreateColumn("S0T0C2",typeof(int),false,false);
				var S0T1 = S.CreateTable("S0T1");
				S0T1.CreateColumn("S0T1C0",typeof(int),false,true);
				var S0T0F1 = S0T1.CreateColumn("S0T0F1",typeof(int),false,false);
				var S0T0F2 = S0T1.CreateColumn("S0T0F2",typeof(int),false,false);
				var S0T0_S0T1 = Container.CreateRelation("S0T0_S0T1",S0T0,S0T1);
				S0T0_S0T1.AddColumn(S0T0F1);
				S0T0_S0T1.AddColumn(S0T0F2);
				S0T0.Point=new Point(100,10);
				S0T1.Point=new Point(100,100);
			}
		}
		var index = 0;
		foreach(var Thumb in FindVisualChildren<Thumb>(this.DiagramListBox)) {
			this.DragDelta(Thumb,new DragDeltaEventArgs(index,index));
			index+=32;
		}
        //@$"Data Source={ホスト名};Initial Catalog=master;Integrated Security=false;{SQLServerログイン}";
        //{
        //    var ItemsSource=new List<string>();
        //    using var Connection=new SqlConnection("Data Source=localhost;Initial Catalog=master;Integrated Security=false;{SQLServerログイン}");
        //    Connection.Open();
        //    using var Command=Connection.CreateCommand();
        //    Command.CommandText=
        //        "USE master\r\n"+
        //        "SELECT name\r\n"+
        //        "FROM sys.databases\r\n"+
        //        "ORDER BY database_id";
        //    using(var Reader=Command.ExecuteReader()){
        //        while(Reader.Read()){
        //            ItemsSource.Add(Reader.GetString(0));
        //        }
        //    }
        //    this.SQLServer.ItemsSource=ItemsSource;
        //}
        {
            var ItemsSource=new List<string>();
            using var Connection=new SqlConnection(SQLServer接続文字列);
            Connection.Open();
            using var Command=Connection.CreateCommand();
            Command.CommandText=
                "USE master\r\n"+
                "SELECT name\r\n"+
                "FROM sys.databases\r\n"+
                "ORDER BY database_id";
            using(var Reader=Command.ExecuteReader()){
                while(Reader.Read()){
                    ItemsSource.Add(Reader.GetString(0));
                }
            }
            this.SQLServer.ItemsSource=ItemsSource;
        }
        {
            var ItemsSource=new List<string>();
            using DbConnection Connection=new MySqlConnection("Server=localhost;Database=sakila;Uid=root;Pwd=password;");
            Connection.Open();
            using var Command=Connection.CreateCommand();
            Command.CommandText="show databases";
            using(var Reader=Command.ExecuteReader()){
                while(Reader.Read()){
                    ItemsSource.Add(Reader.GetString(0));
                }
            }
            this.MySQL.ItemsSource=ItemsSource;
        }
        //this.MySQL保存("sakila");
        //this.SQLServer保存("AdventureWorks2008R2");
        //this.SQLServer保存("Pubs");
        //this.SQLServer保存("単純");
        //this.SQLServer保存("実験_new");
        //this.SQLServer保存("実験");
        //this.データベース保存("VIEWテスト");
        //this.データベース保存("TPC_C");
        //this.データベース保存("TPC_H");
        //this.データベース保存("TPC_E");
        foreach (var Database in Databases){
            this.SQLServer保存(Database);
        }
        //foreach (var Database in Databases){
        //}
        //this.データベース保存("AdventureWorks2017");
        //this.SQLServer保存("AdventureWorks2019");
        //this.データベース保存("DWConfiguration");
        //this.データベース保存("DWQueue");
        //this.データベース保存("WideWorldImporters");
        //this.データベース保存("WideWorldImportersDW");
		//this.データベース保存("AdventureWorksDW2017");
		//this.データベース保存("AdventureWorksDW2019");
		//this.データベース保存("DWDiagnostics");
        //this.データベース保存("msdb");
	}
    private static readonly string[]Databases={
        "Pubs",
        "Northwind",
        "AdventureWorks2008R2",
        "AdventureWorks2012",
        "AdventureWorks2014",
        "AdventureWorks2016",
        "AdventureWorks2016_EXT",
        "AdventureWorks2017",
        "AdventureWorks2019",
        "WideWorldImporters",
        "AdventureWorksDW2008R2",
        "AdventureWorksDW2012",
        "AdventureWorksDW2014",
        "AdventureWorksDW2016",
        "AdventureWorksDW2016_EXT",
        "AdventureWorksDW2017",
        "AdventureWorksDW2019",
        "WideWorldImportersDW",
        //"msdb",
    };
	private void SQLServer保存(string Name) {
		Trace.WriteLine(Name);
		this.SQLServer選択(Name);
		this.保存_Click(null!,null!);
	}
    private void MySQL保存(string Name) {
        this.MySQL選択(Name);
        this.保存_Click(null!,null!);
    }
	private void CloseSchema_Click(object sender,RoutedEventArgs e) {
		var VMSchema = (VM.Schema)((Button)sender).DataContext;
		VMSchema.Container!.Schemas.Remove(VMSchema);
	}
	private void CloseTable_Click(object sender,RoutedEventArgs e) {
		var VMTable = (VM.Table)((Button)sender).DataContext;
		//VMTable.Schema.Database.DiagramObjects.Remove(VMTable);
		((VM.Container)VMTable.Schema.Container).Remove(VMTable);
		((VM.Schema)VMTable.Schema).Tables.Remove(VMTable);
		//((ObservableCollection<ITable>)VMTable.Schema.Tables).Remove(VMTable);
	}

	private void Schemas_SelectionChanged(object sender,SelectionChangedEventArgs e) {
		var ComboBox = (ComboBox)sender;
		if(ComboBox.DataContext is VM.Table Table) {
			foreach(var AddedItem in e.AddedItems) {
				if(AddedItem is VM.Schema Schema) {
					Schema.Tables.Add(Table);
				}
			}
			foreach(var RemovedItem in e.RemovedItems) {
				if(RemovedItem is VM.Schema Schema) {
					Schema.Tables.Remove(Table);
				}
			}
		}
	}
	private readonly AssemblyGenerator AssemblyGenerator = new();
	private void 保存_Click(object sender,RoutedEventArgs e) {
		var Container = (VM.Container)this.DataContext;
		//var Common = this.Common;
        //this.AssemblyGenerator.Save(Container,Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!);
        this.AssemblyGenerator.Save(Container,Environment.CurrentDirectory);
		//try { 
		//Common.Save(Container,Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!);
		//}catch(Exception ex) {

  //      }
		//Container.Save(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!);
	}
	private static IEnumerable<T> FindVisualChildren<T>(DependencyObject obj) where T : DependencyObject {
		for(var i = 0;i<VisualTreeHelper.GetChildrenCount(obj);i++) {
			var Child0 = VisualTreeHelper.GetChild(obj,i);
			if(Child0 is T Result) {
				yield return Result;
			} else {
				foreach(var Child1 in FindVisualChildren<T>(Child0)) {
					yield return Child1;
				}
			}
		}
	}
	private static T? FindVisualChild<T>(DependencyObject obj) where T : DependencyObject {
		for(var i = 0;i<VisualTreeHelper.GetChildrenCount(obj);i++) {
			var child = VisualTreeHelper.GetChild(obj,i);
			if(child is T Result) {
				return Result;
			} else {
				var childOfChild = FindVisualChild<T>(child);
				if(childOfChild!=null) {
					return childOfChild;
				}
			}
		}
		return null;
	}
	private static T? FindVisualChild<T>(DependencyObject obj,string Name) where T : FrameworkElement {
		for(var i = 0;i<VisualTreeHelper.GetChildrenCount(obj);i++) {
			var child = VisualTreeHelper.GetChild(obj,i);
			if(child is T Result&&Result.Name==Name) {
				return Result;
			} else {
				var childOfChild = FindVisualChild<T>(child,Name);
				if(childOfChild!=null) {
					return childOfChild;
				}
			}
		}
		return null;
	}
	private void DragDelta(object sender,DragDeltaEventArgs e) {
		var Thumb = e.Source as Thumb;
		if(Thumb==null)
			return;
		if(Thumb.DataContext is VM.Table Table) {
			var ListBoxItem = (ListBoxItem)ItemsControl.ContainerFromElement(this.DiagramListBox,Thumb);
			var Top = Canvas.GetTop(ListBoxItem)+e.VerticalChange;
			var Left= Canvas.GetLeft(ListBoxItem)+e.HorizontalChange;
			Table.Top=Top;
			Table.Left=Left;
			var DataGridRowHeader = FindVisualChild<DataGridRowHeader>(ListBoxItem)!;
			var Point = DataGridRowHeader.PointToScreen(new Point(Left-VM.Common.ColumnHeaderWidth/2,Top));
			Table.Point=FindVisualChild<Canvas>(ListBoxItem)!.PointFromScreen(Point);
		}
	}
	//public static Type SQLのTypeからTypeに変換(String DBType) {
	//    switch(DBType) {
	//        case "bit":return typeof(Boolean);
	//        case "tinyint":return typeof(Byte);
	//        case "smallint":return typeof(Int16);
	//        case "int":
	//        case "integer":return typeof(Int32);
	//        case "bigint":return typeof(Int64);
	//        case "real":return typeof(Single);
	//        case "float":return typeof(Double);
	//        case "decimal":
	//        case "numeric":
	//        case "smallmoney":
	//        case "money":return typeof(Decimal);
	//        case "date":
	//        case "datetime":
	//        case "datetime2":
	//        case "smalldatetime":
	//        case "datetimeoffset":
	//        case "timestamp":return typeof(DateTimeOffset);
	//        case "binary":
	//        case "varbinary":return typeof(Byte[]);
	//        case "geography":return typeof(Microsoft.SqlServer.Types.SqlGeography);
	//        case "geometry":return typeof(Microsoft.SqlServer.Types.SqlGeometry);
	//        case "image":
	//        case "sql_variant":return typeof(Object);
	//        case "xml":return typeof(XDocument);
	//        case "uniqueidentifier":return typeof(Guid);
	//        case "time":return typeof(TimeSpan);
	//        case "hierarchyid":return typeof(Microsoft.SqlServer.Types.SqlHierarchyId);
	//        case "char":
	//        case "varchar":
	//        case "nchar":
	//        case "nvarchar":
	//        case "text":
	//        case "ntext":
	//        case "sysname":
	//        default: 
	//            if(DBType[..4]=="char"||DBType[..4]=="text"||DBType[..5]=="nchar"||DBType[..5]=="ntext"||DBType[..7]=="varchar"||DBType[..7]=="sysname"||DBType[..8]=="nvarchar")
	//                return typeof(String);
	//            throw new NotSupportedException(DBType);
	//    }
	//}
	private void SQLServer_SelectionChanged(object sender,SelectionChangedEventArgs e){
		var データベース名 = (string)e.AddedItems[0]!;
		this.SQLServer選択(データベース名);
	}
    private void MySQL_SelectionChanged(object sender,SelectionChangedEventArgs e){
        var データベース名 = (string)e.AddedItems[0]!;
        this.MySQL選択(データベース名);
    }

	private void SQLServer選択(string データベース名){
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
			p.DbType=Data.DbType.String;
			return p;
		}
		var Parameters=Command.Parameters;
		var Container=this.Container;
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
		{
			Command.CommandText=information_schema.SQL_Table;
			foreach(var Schema in Schemas) {
				SCHEMA.Value=Schema.Name;
				using var Reader = Command.ExecuteReader();
				while(Reader.Read()) {
					var TableName = Reader.GetString(0);
					Schema.CreateTable(TableName);
				}
			}
		}
        {
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
            foreach(var Schema in Schemas) {
                SCHEMA.Value=Schema.Name;
                using var Reader = Command.ExecuteReader();
                while(Reader.Read()) {
                    var Name = Reader.GetString(0);
                    var SQL = Reader.GetString(1);
                    Schema.CreateScalarFunction(Name,SQL);
                }
            }
            Command.CommandText=information_schema.SQL_TableFunction;
            foreach(var Schema in Schemas) {
                SCHEMA.Value=Schema.Name;
                using var Reader = Command.ExecuteReader();
                while(Reader.Read()) {
                    var Name = Reader.GetString(0);
                    var SQL = Reader.GetString(1);
                    Schema.CreateTableFunction(Name,SQL);
                }
            }
        }
		{
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
		}
        {
			Command.CommandText=information_schema.SQL_Function_Parameter;
			Parameters.Add(NAME);
			foreach(var Schema in Schemas) {
				SCHEMA.Value=Schema.Name;
				foreach(var ScalarFunction in Schema.ScalarFunctions){
					NAME.Value=ScalarFunction.Name;
					var ScalarFunction_Parameters = ScalarFunction.Parameters;
					using var Reader = Command.ExecuteReader();
					while(Reader.Read()) {
						var name = Reader.GetString(0);
						var is_output = Reader.GetBoolean(1);
						var type = Reader.GetString(2);
						var has_default_value = Reader.GetBoolean(3);
						var default_value = Reader.GetValue(4);
						var Type = CommonLibrary.SQLのTypeからTypeに変換(type);
						if(is_output) {
							ScalarFunction.Type=Type;
						} else {
							ScalarFunction_Parameters.Add(new Parameter(name,Type,has_default_value,default_value));
						}
					}
				}
				foreach(var TableFunction in Schema.TableFunctions){
					NAME.Value=TableFunction.Name;
					var TableFunction_Parameters = TableFunction.Parameters;
					using var Reader = Command.ExecuteReader();
					//TableFunction.Type=共通(TableFunction.Parameters);
					while(Reader.Read()) {
						var name = Reader.GetString(0);
						var is_output = Reader.GetBoolean(1);
						var type = Reader.GetString(2);
						var has_default_value = Reader.GetBoolean(3);
						var default_value = Reader.GetValue(4);
						var Type = CommonLibrary.SQLのTypeからTypeに変換(type);
						Debug.Assert(!is_output);
						TableFunction_Parameters.Add(new Parameter(name,Type,has_default_value,default_value));
					}
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
					Table.CreateColumn(Reader.GetString(1),CommonLibrary.SQLのTypeからTypeに変換(Reader.GetString(0)),Reader.GetString(2)=="YES",Reader.GetInt32(3)!=0);
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
					a.CreateColumn(Reader.GetString(0),CommonLibrary.SQLのTypeからTypeに変換(Reader.GetString(1)),Reader.GetString(2)=="YES");
			}
			foreach(var a in Schema.TableFunctions) {
				NAME.Value=a.Name;
				using var Reader=Command.ExecuteReader();
				while(Reader.Read())
					a.CreateColumn(Reader.GetString(0),CommonLibrary.SQLのTypeからTypeに変換(Reader.GetString(1)),Reader.GetString(2)=="YES");
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
	private void MySQL選択(string データベース名){
		var information_schema=new LinqDB.Product.MySQL.information_schema(false);
		using var SqlConnection=new MySqlConnection(MySQL接続文字列);
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
			p.DbType=Data.DbType.String;
			return p;
		}
		var Parameters=Command.Parameters;
		var Container=this.Container;
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
		{
			Command.CommandText=information_schema.SQL_Table;
			foreach(var Schema in Schemas) {
				SCHEMA.Value=Schema.Name;
				using var Reader = Command.ExecuteReader();
				while(Reader.Read()) {
					var TableName = Reader.GetString(0);
					Schema.CreateTable(TableName);
				}
			}
		}
        {
			Command.CommandText=information_schema.SQL_Function_Parameter;
			Parameters.Add(NAME);
			foreach(var Schema in Schemas) {
				SCHEMA.Value=Schema.Name;
				foreach(var ScalarFunction in Schema.ScalarFunctions){
					NAME.Value=ScalarFunction.Name;
					//ScalarFunction.Type=共通(ScalarFunction.Parameters);
					var ScalarFunction_Parameters = ScalarFunction.Parameters;
					using var Reader = Command.ExecuteReader();
					while(Reader.Read()) {
						var name = Reader.GetString(0);
						var is_output = Reader.GetBoolean(1);
						var type = Reader.GetString(2);
						var has_default_value = Reader.GetBoolean(3);
						var default_value = Reader.GetValue(4);
						var Type = CommonLibrary.SQLのTypeからTypeに変換(type);
						if(is_output) {
							ScalarFunction.Type=Type;
						} else {
							ScalarFunction_Parameters.Add(new Parameter(name,Type,has_default_value,default_value));
						}
					}
				}
				foreach(var TableFunction in Schema.TableFunctions){
					NAME.Value=TableFunction.Name;
					var TableFunction_Parameters = TableFunction.Parameters;
					using var Reader = Command.ExecuteReader();
					//TableFunction.Type=共通(TableFunction.Parameters);
					while(Reader.Read()) {
						var name = Reader.GetString(0);
						var is_output = Reader.GetBoolean(1);
						var type = Reader.GetString(2);
						var has_default_value = Reader.GetBoolean(3);
						var default_value = Reader.GetValue(4);
						var Type = CommonLibrary.SQLのTypeからTypeに変換(type);
						Debug.Assert(!is_output);
						TableFunction_Parameters.Add(new Parameter(name,Type,has_default_value,default_value));
					}
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
					Table.CreateColumn(Reader.GetString(1),CommonLibrary.SQLのTypeからTypeに変換(Reader.GetString(0)),Reader.GetString(2)=="YES",Reader.GetInt32(3)!=0);
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
					a.CreateColumn(Reader.GetString(0),CommonLibrary.SQLのTypeからTypeに変換(Reader.GetString(1)),Reader.GetString(2)=="YES");
			}
			foreach(var a in Schema.TableFunctions) {
				NAME.Value=a.Name;
				using var Reader=Command.ExecuteReader();
				while(Reader.Read())
					a.CreateColumn(Reader.GetString(0),CommonLibrary.SQLのTypeからTypeに変換(Reader.GetString(1)),Reader.GetString(2)=="YES");
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
	class UnlodableAssemblyLoadContext:AssemblyLoadContext {
		public UnlodableAssemblyLoadContext() : base(isCollectible: true) { }
		protected override Assembly Load(AssemblyName assemblyName) => null!;
	}
	private static readonly char[] 分割文字 = { '.' };
	private const string NullableContextAttribute="System.Runtime.CompilerServices.NullableContextAttribute";
	//private static readonly Type NullableContextAttribute_Type = Type.GetType(NullableContextAttribute);
	private WeakReference? WeakReference;
	private void GCLoad(string ファイル名)
	{
		this.Load(ファイル名);
		GC.Collect();
		GC.WaitForPendingFinalizers();
		GC.Collect();
	}
	private void Load(string ファイル名){
		var ExeFolderFileName = System.Diagnostics.Process.GetCurrentProcess().MainModule!.FileName;
		var ExeFolderName = Path.GetDirectoryName(ExeFolderFileName);
		var runtimeAssemblies = Directory.GetFiles(RuntimeEnvironment.GetRuntimeDirectory(),"*.dll");
		var paths = new List<string>(runtimeAssemblies)
		{
			//                (String)ファイル名,
			"LinqDB.dll"
		};
		//var resolver = new PathAssemblyResolver(paths);
		//using var mlc = new MetadataLoadContext(resolver);
		var UnlodableAssemblyLoadContext = new UnlodableAssemblyLoadContext();
		var assembly = UnlodableAssemblyLoadContext.LoadFromAssemblyPath($@"{ExeFolderName}\{ファイル名}");
		this.WeakReference = new WeakReference(UnlodableAssemblyLoadContext,true);
		var AssemblyName = assembly.GetName();
		Console.WriteLine($@"{AssemblyName.Name} has following attributes: ");
		var Container_Type = assembly.GetTypes().Single(p =>p.Name=="Container"&&p.Namespace!.Count(q =>q=='.')==0);
		//var Container = assembly.GetTypes().Single(p => typeof(Container).IsAssignableFrom(p));
		//var Database = (VM.Database)this.DataContext;
		//Database.Load(MarshalDatabase);
		var Namespace = Container_Type.Namespace!;
		var ChildExtensions = assembly.GetType($"{Namespace}.ChildExtensions");
		if(ChildExtensions==null) {
			throw new NotSupportedException($"{Namespace}.ChildExtensionsが存在しなかった。");
		}
		// A
		var Types = assembly.GetTypes();
		//var Container = new VM.Container();
		//this.DataContext=Container;
		var Container = (VM.Container)this.DataContext;
		Container.Clear();
		foreach(var Type in Types) {
			var 分割 = Type.Namespace!.Split(分割文字);
			if(分割.Length>1) {
				switch(分割[1]) {
					case "Schemas":
						Container.CreateSchema(Type.Name);
						break;
					case "PrimaryKeys": {
						var Table = Container[分割[2]][Type.Name];
						var ctor = Type.GetConstructors()[0];
						var Parameters = ctor.GetParameters();
						foreach(var Column in Table.Columns) {
							if(Parameters.Any(p => p.Name==Column.Name)) {
								Column.IsPrimaryKey=true;
							}
						}
						break;
					}
					case "Tables": {
						var GetCustomAttributesData = Type.GetCustomAttributesData();//<MetaAttribute>();
						var CustomAttributesData = GetCustomAttributesData.Single(p => p.AttributeType.FullName==typeof(MetaAttribute).FullName);
						//var CustomAttributesData = GetCustomAttributesData.Single(p =>
						//{
						//    var x=p.AttributeType==typeof(MetaAttribute);
						//    return x;
						//});
						var ConstructorArguments = CustomAttributesData.ConstructorArguments;
						var Left = (double)ConstructorArguments[0].Value!;
						var Top = (double)ConstructorArguments[1].Value!;
						//foreach(var GetCustomAttributeData in GetCustomAttributesData)
						//{
						//    var x = GetCustomAttributeData.ConstructorArguments[0];
						//    var x = GetCustomAttributeData.ConstructorArguments[1];
						//}
						//var MetaAttribute = Type.GetCustomAttributesData();//<MetaAttribute>();
						var Table = Container[分割[2]].CreateTable(Type.Name,Left,Top);
						var ctor = Type.GetConstructors()[0];
						var Properties = Type.GetProperties();
						var Schema = 分割[2];
						foreach(var Parameter in ctor.GetParameters()) {
							var 検索したプロパティ = Properties.SingleOrDefault(
								p => p.Name==Parameter.Name
							)!;
							var Nullableプロパティか = 検索したプロパティ.GetCustomAttributesData().Any(q => q.AttributeType.FullName==NullableContextAttribute);
							//var PrimaryKey = assembly.GetType($"{Namespace}.PrimaryKeys.{Schema}");
							//if(PrimaryKey.GetProperty(Parameter.Name!)
							Table.CreateColumn(
								Parameter.Name!,
								Parameter.ParameterType,
								Parameter.ParameterType.IsNullable(),
								Nullableプロパティか
							);
						}
						break;
					}
					case "ChildExtensions": {
						foreach(var Method in Type.GetMethods(BindingFlags.Static|BindingFlags.Public)) {
							共通<ChildAttribute>(Container,Method.ReturnType.GetGenericArguments()[0],Method.GetParameters()[0].ParameterType,Method);
						}
						break;
					}
					case "ParentExtensions": {
						foreach(var Method in Type.GetMethods(BindingFlags.Static|BindingFlags.Public)) {
							共通<ParentAttribute>(Container,Method.GetParameters()[0].ParameterType,Method.ReturnType,Method);
						}
						break;
					}
					static void 共通<TRelationAttribute>(VM.Container Container,Type 子Type,Type 親Type,MethodInfo Method) where TRelationAttribute : RelationAttribute {
						var GetCustomAttributesData = Method.GetCustomAttributesData();//<TRelationAttribute>()!;
						var CustomAttributesData = GetCustomAttributesData.Single(p => p.AttributeType.FullName==typeof(TRelationAttribute).FullName);
						var Coumuns = CustomAttributesData.ConstructorArguments;
						//var CustomAttributeTypedArgument = (CustomAttributeTypedArgument)Coumuns[0].Value!;
						//var gg=(ReadOnlyCollection<CustomAttributeTypedArgument>)CustomAttributeTypedArgument.Value;
						//var Top = (Double)ConstructorArguments[1].Value!;
						//var RelationAttribute = RelationAttributeData.Single(p => p.AttributeType.FullName==typeof(TRelationAttribute).FullName).AttributeType;
						//var RelationAttribute = Method.GetCustomAttribute<TRelationAttribute>()!;
						var ColumnNames = Coumuns.SelectMany(
							p => ((ReadOnlyCollection<CustomAttributeTypedArgument>)p.Value!).Select(q => (string)q.Value!)
						).ToArray();
						//var ColumnNames = new String[] { "" };
						var 子Table = Container[子Type.Namespace!.Split(分割文字)[2]][子Type.Name];
						var 親Table = Container[親Type.Namespace!.Split(分割文字)[2]][親Type.Name];
						var Relation = Container.CreateRelation(
							Method.Name,
							親Table,
							子Table
						);
						var Relation_Columns = Relation.Columns;
						var 子Table_Columns = 子Table.Columns;
						foreach(var ColumnName in ColumnNames) {
							var Column = 子Table_Columns.SingleOrDefault(p => p.Name==ColumnName);
							if(Column!=null) {
								Relation_Columns.Add(Column);
							}
						}
					}
				}
			}
		}
		UnlodableAssemblyLoadContext.Unload();
	}
	private void DLL_SelectionChanged(object sender,SelectionChangedEventArgs e) {
		this.GCLoad((string)e.AddedItems[0]!);
	}
	private void つまみMouseLeftButtonDown(object sender,System.Windows.Input.MouseButtonEventArgs e) {
		if(((FrameworkElement)sender).DataContext is VM.Table Table) {
			var DiagramObjects =((VM.Container)Table.Schema.Container!).DiagramObjects;
			var TableCount = DiagramObjects.OfType<VM.Table>().Count();
			var oldIndex = DiagramObjects.IndexOf(Table);
			DiagramObjects.Move(oldIndex,TableCount-1);
		}
	}
	private void RemoveTable_Click(object sender,RoutedEventArgs e) {
		var VM_Container = (VM.Container)this.DataContext;
		var VM_Table = (VM.Table)((Button)sender).DataContext;
		var 親Relations = VM_Table.親Relations;
		var DiagramObjects=VM_Container.DiagramObjects;
		DiagramObjects.Remove(VM_Table);
		var Relations=VM_Container.Relations;
		foreach(var 親Relation in 親Relations) {
			DiagramObjects.Remove(親Relation);
			Relations.Remove(親Relation);
		}
		var 子Relations = VM_Table.子Relations;
		foreach(var 子Relation in 子Relations) {
			DiagramObjects.Remove(子Relation);
			Relations.Remove(子Relation);
		}
		親Relations.Clear();
		子Relations.Clear();
		VM_Container.Remove(VM_Table); 
		((VM.Schema)VM_Table.Schema).Tables.Remove(VM_Table);
	}

	private void CreateTable_Click(object sender,RoutedEventArgs e) {
		this.dbo.CreateTable("");
	}
}