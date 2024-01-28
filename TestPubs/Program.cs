using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.SqlServer.Types;
using LinqDB.Sets;
using Pubs.Tables.dbo;
using Pubs;
using System.Data.SqlClient;
using System.Net.Mime;
namespace TestWideWorldImporters;

abstract class Program:共通 {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization","CA1305:IFormatProvider を指定します",Justification = "<保留中>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization","CA1303:ローカライズされるパラメーターとしてリテラルを渡さない",Justification = "<保留中>")]
    private static void Create() {
        using var Container = new Container();
        var dbo= Container.dbo;
        var jobs = dbo.jobs;
        var stores = dbo.stores;
        var authors= dbo.authors;
        var publishers= dbo.publishers;
        var pub_info= dbo.pub_info;
        var employee= dbo.employee;
        var titles= dbo.titles;
        var discounts= dbo.discounts;
        var sales= dbo.sales;
        var roysched= dbo.roysched;
        var titleauthor= dbo.titleauthor;
        var s = Stopwatch.StartNew();
        //LV0
        for(short job_id=0;job_id<2;job_id++){
            jobs.AddOrThrow(
                new(job_id,"",byte.MinValue,byte.MaxValue)
            );
        }
        for(var stor_id=0;stor_id<2;stor_id++){
            stores.AddOrThrow(
                new(stor_id.ToString(),"","","","","")
            );
            for(var b=0;b<2;b++){
                var str=b.ToString();
                discounts.AddOrThrow(
                    new(str,str,0,0,0)
                );
            }
        }
        for(var au_id=0;au_id<2;au_id++){
            authors.AddOrThrow(
                new(au_id.ToString(),"","","","","","","",true)
            );
        }
        for(var pub_id=0;pub_id<2;pub_id++){
            publishers.AddOrThrow(
                new(pub_id.ToString(),"","","","")
            );
        }
        {
            var emp_id=0;
            E(
                publishers,
                a =>{
                    E(
                        jobs,
                        b=>employee.AddOrThrow(
                            new(
                                emp_id++.ToString(),
                                "",
                                "",
                                "",
                                b.job_id,
                                0,
                                a.pub_id,
                                DateTime.Now
                            )
                        )
                    );
                    pub_info.AddOrThrow(new(a.pub_id,null,""));
                    for(var b=0;b<2;b++){
                        var title_id=b.ToString();
                        titles.AddOrThrow(new(title_id,"","",a.pub_id,0,0,0,0,"",DateTime.Now));
                        E(
                            authors,
                            c=>{
                                titleauthor.AddOrThrow(new(c.au_id,title_id,0,0));
                            }
                        );
                        roysched.AddOrThrow(new(title_id,0,0,0));
                    }
                }
            );
            var ord_num=0;
            E(
                titles,
                a=>E(
                    stores,
                    b=>sales.AddOrThrow(
                        new(
                            b.stor_id,
                            ord_num++.ToString(),
                            DateTime.Now,
                            0,
                            "",
                            a.title_id
                        )
                    )
                )
            );
        }
        Console.WriteLine($"Create {s.ElapsedMilliseconds,7}ms");
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization","CA1305:IFormatProvider を指定します",Justification = "<保留中>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization","CA1303:ローカライズされるパラメーターとしてリテラルを渡さない",Justification = "<保留中>")]
    private static void Load() {
        using var C = new SqlConnection(SQLServer接続文字列);
        C.Open();
        using var Command = new SqlCommand { Connection=C };
        using var e = new Container();
        var dbo= e.dbo;
        var jobs = dbo.jobs;
        var stores = dbo.stores;
        var authors= dbo.authors;
        var publishers= dbo.publishers;
        var pub_info= dbo.pub_info;
        var employee= dbo.employee;
        var titles= dbo.titles;
        var discounts= dbo.discounts;
        var sales= dbo.sales;
        var roysched= dbo.roysched;
        var titleauthor= dbo.titleauthor;
        var s = Stopwatch.StartNew();
        {
            Command.CommandText=@"
                SELECT
                 [job_id]
                ,[job_desc]
                ,[min_lvl]
                ,[max_lvl]
                FROM [dbo].[jobs];
                SELECT 
                 [pub_id]
                ,[pub_name]
                ,[city]
                ,[state]
                ,[country]
                FROM [dbo].[publishers];
                SELECT 
                 [pub_id]
                ,[logo]
                ,[pr_info]
                FROM [dbo].[pub_info];
                SELECT 
                 [au_id]
                ,[au_lname]
                ,[au_fname]
                ,[phone]
                ,[address]
                ,[city]
                ,[state]
                ,[zip]
                ,[contract]
                FROM [dbo].[authors];
                SELECT 
                 [stor_id]
                ,[stor_name]
                ,[stor_address]
                ,[city]
                ,[state]
                ,[zip]
                FROM [dbo].[stores];
                SELECT 
                 [discounttype]
                ,[stor_id]
                ,[lowqty]
                ,[highqty]
                ,[discount]
                FROM [dbo].[discounts];
                SELECT
                 [title_id]
                ,[title]
                ,[type]
                ,[pub_id]
                ,[price]
                ,[advance]
                ,[royalty]
                ,[ytd_sales]
                ,[notes]
                ,[pubdate]
                FROM [dbo].[titles]
                SELECT 
                 [au_id]
                ,[title_id]
                ,[au_ord]
                ,[royaltyper]
                FROM [dbo].[titleauthor];
                ";
            //static SqlGeography? GetSqlGeography(SqlDataReader r,Int32 i) {
            //    if(r.IsDBNull(i)) {
            //        return null;
            //    } else {
            //        var SqlBytes = r.GetSqlBytes(i);
            //        using var BinaryReader = new System.IO.BinaryReader(SqlBytes.Stream,System.Text.Encoding.UTF8,true);
            //        var SqlGeography = new SqlGeography();
            //        SqlGeography.Read(BinaryReader);
            //        return SqlGeography;
            //    }
            //}
            //static String? GetString(SqlDataReader r,Int32 i) => r.IsDBNull(i) ? default : r.GetString(i);
            //static Int32? GetInt32(SqlDataReader r,Int32 i) => r.IsDBNull(i) ? default(Int32?) : r.GetInt32(i);
            //static Int64? GetInt64(SqlDataReader r,Int32 i) => r.IsDBNull(i) ? default(Int64?) : r.GetInt64(i);
            //static Decimal? GetDecimal(SqlDataReader r,Int32 i) => r.IsDBNull(i) ? default(Decimal?) : r.GetDecimal(i);
            //static DateTime? GetDateTime(SqlDataReader r,Int32 i) => r.IsDBNull(i) ? default(DateTime?) : r.GetDateTime(i);
            ////static DateTime? GetDateTimeOffset(SqlDataReader r,Int32 i) => r.IsDBNull(i) ? default(DateTime?) : r.GetDateTimeOffset(i);
            //static Boolean? GetBoolean(SqlDataReader r,Int32 i) => r.IsDBNull(i) ? default(Boolean?) : r.GetBoolean(i);
            ////static SqlGeography? GetSqlGeography(SqlDataReader r,Int32 i) => r.IsDBNull(i) ? default : (SqlGeography)r.GetValue(i);
            using var Reader = Command.ExecuteReader();
            while(Reader.Read()) {
                jobs.AddOrThrow(
                    new(
                        Reader.GetInt16(0),
                        Reader.GetString(1),
                        Reader.GetByte(2),
                        Reader.GetByte(3)
                    )
                );
            }
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                publishers.AddOrThrow(
                    new(
                        Reader.GetString(0),
                        GetString(Reader,1),
                        GetString(Reader,2),
                        GetString(Reader,3),
                        GetString(Reader,4)
                    )
                );
            }
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                pub_info.AddOrThrow(
                    new(
                        Reader.GetString(0),
                        GetBytes(Reader,1),
                        GetString(Reader,2)
                    )
                );
            }
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                authors.AddOrThrow(
                    new(
                        Reader.GetString(0),
                        Reader.GetString(1),
                        Reader.GetString(2),
                        Reader.GetString(3),
                        GetString(Reader,4),
                        GetString(Reader,5),
                        GetString(Reader,6),
                        GetString(Reader,7),
                        Reader.GetBoolean(8)
                    )
                );
            }
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                stores.AddOrThrow(
                    new(
                        Reader.GetString(0),
                        GetString(Reader,1),
                        GetString(Reader,2),
                        GetString(Reader,3),
                        GetString(Reader,4),
                        GetString(Reader,5)
                    )
                );
            }
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                discounts.AddOrThrow(
                    new(
                        Reader.GetString(0),
                        GetString(Reader,1),
                        GetInt16(Reader,2),
                        GetInt16(Reader,3),
                        Reader.GetDecimal(4)
                    )
                );
            }
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                titles.AddOrThrow(
                    new(
                        Reader.GetString(0),
                        Reader.GetString(1),
                        Reader.GetString(2),
                        GetString(Reader,3),
                        GetDecimal(Reader,4),
                        GetDecimal(Reader,5),
                        GetInt32(Reader,6),
                        GetInt32(Reader,7),
                        GetString(Reader,8),
                        Reader.GetDateTime(9)
                    )
                );
            }
            if(!Reader.NextResult()) throw new Exception();
            while(Reader.Read()) {
                titleauthor.AddOrThrow(
                    new(
                        Reader.GetString(0),
                        Reader.GetString(1),
                        GetByte(Reader,2),
                        GetInt32(Reader,3)
                    )
                );
            }
        }
        e.Clear();
        Console.WriteLine($"Load {s.ElapsedMilliseconds,7}ms");
    }
    private static void Main() {
        Load();
        Create();
#pragma warning disable CA1303 // ローカライズされるパラメーターとしてリテラルを渡さない
        Console.Write("終了");
#pragma warning restore CA1303 // ローカライズされるパラメーターとしてリテラルを渡さない
        Console.ReadKey();
    }
}