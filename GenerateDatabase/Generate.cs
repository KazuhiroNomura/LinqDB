using System;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Data.SqlClient;
namespace ImportDatabase {
    class テスト{
        public String? nf = "ABC";
        public String vf = "ABC";
        public String? np => this.nf;
        public String vp =>this.vf;
        public String? nm() => this.nf;
        public String vm() => this.vf;
    }
    public static class Generate {
        private readonly struct S {
            private readonly Int32 v;
            public S(Int32 v) {
                this.v=v;
            }
        }
        private class C {
            private readonly Int32 v;
            public C(Int32 v) {
                this.v=v;
            }
        }
        private static void Main() {
            //const String Sql接続文字列 = @"Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=master;Connect Timeout=60;Persist Security Info=True;User ID=sa;Password=password";
            const String 接続文字列 = @"Data Source=localhost;Initial Catalog=master;Connect Timeout=60;Persist Security Info=True;User ID=sa;Password=password";
            using var SqlConnection = new SqlConnection(接続文字列);
            //using var OleDbConnection = new OleDbConnection("Provider=SQLOLEDB;"+接続文字列);
            var i = new LinqDB.Databases.SQLServer();
            DbConnection DbConnection= SqlConnection;
            using var p = new LinqDB.Databases.ImportDatabase();
            //using var o = new LinqDB.Optimizers.Optimizer();
            void 出力(String DatabaseName) {
                p.Read1(DatabaseName,DbConnection,i);
                p.OutputAssembly1(DatabaseName);
            }
            //void 出力(String DatabaseName) {
            //    p.Read(DatabaseName,DbConnection,i);
            //    p.OutputAssembly(DatabaseName);
            //}
            出力("sqlr");
            出力("TPC-H");
            出力("TPC-C");
            出力("TPC-E");
            出力("WideWorldImporters");
            出力("WideWorldImportersDW");
            出力("AdventureWorks2017");
            出力("AdventureWorksDW2017");
        }
    }
}
