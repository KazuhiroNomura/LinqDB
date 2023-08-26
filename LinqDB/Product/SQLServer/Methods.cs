using System;
//using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using IO = System.IO;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using System.Xml.XPath;
using LinqDB.Sets;

// ReSharper disable AssignNullToNotNullAttribute
namespace LinqDB.Product.SQLServer;
//[AttributeUsage(AttributeTargets.Parameter)]
[AttributeUsage(AttributeTargets.Parameter,Inherited = false)]
public sealed class TypeAttribute:Attribute {

}
public static class Methods {
    //ODBCスカラー
    public static string? odbcscale(byte?Param1,byte?Param2){
        return "type";
    }
    //Aggregate
    //分析
    //照合順序
    public static string? collationproperty(string?collation_name ,string? property){
        return "type";
    }
    //構成
    //    @@DATEFIRST
    //    @@DBTS
    //    @@LANGID
    //    @@LANGUAGE
    //    @@LOCK_TIMEOUT
    //    @@MAX_CONNECTIONS
    //    @@MAX_PRECISION
    //    @@NESTLEVEL
    //    @@OPTIONS
    //    @@REMSERVER
    //    @@SERVERNAME
    /// <summary>
    /// 公開しているサーバ名
    /// </summary>
    public static string servername=>$@"computer名\{servicename}";
    //    @@SERVICENAME
    /// <summary>
    /// 公開しているサーバ名
    /// </summary>
    public static string servicename=>@"MSSQLSERVER2019";
    //    @@SPID
    //    @@TEXTSIZE
    //    @@VERSION
    //データ型
    //    DATALENGTH (Transact-SQL)
    public static long? datalength(byte[]? expression)=>expression?.LongLength;
    public static long? datalength(string? expression)=>expression?.Length;
    public static long? datalength(object? expression)=>expression switch {
        byte[] x=>datalength(x),
        string x=>datalength(x),
        _=>throw new NotImplementedException()
    };
    //    IDENT_SEED (Transact-SQL)
    //    IDENT_CURRENT (Transact-SQL)
    //    IDENTITY (関数) (Transact-SQL)
    //    IDENT_INCR (Transact-SQL)
    //    SQL_VARIANT_PROPERTY (Transact-SQL)
    //日付と時刻のデータ型および関数	日付時刻型の入力値に対して操作を実行し、文字列値、数値、または日付時刻値を返します。
    public static DateTime? getdate()=>DateTime.Now;
    internal static class Internal{
        public static byte[] Add(byte[] a,byte[] b){
            var result=new byte[a.Length+b.Length];
            Array.Copy(a,result,a.Length);
            Array.Copy(b,0,result,a.Length,b.Length);
            return result;
        }
    }
    internal static class DATENAME {
        public static string year(DateTime date)=>date.Year.ToString();
        public static string month(DateTime date)=>date.Month.ToString();
        public static string day(DateTime date)=>date.Day.ToString();
        public static string dayofyear(DateTime date)=>(date.DayOfYear).ToString();
        public static string quarter(DateTime date)=>((date.Month+3)/4).ToString();
        /// <summary>
        /// 1/1(日)～1/7(土)は1,1/8(日)～1/14(土)は2
        /// 1/1(月)～1/6(土)は1,1/7(日)～1/13(土)は2
        /// 1/1(火)～1/5(土)は1,1/6(日)～1/12(土)は2
        /// 1/1(水)～1/4(土)は1,1/5(日)～1/11(土)は2
        /// 1/1(木)～1/3(土)は1,1/4(日)～1/10(土)は2
        /// 1/1(金)～1/2(土)は1,1/3(日)～1/09(土)は2
        /// 1/1(土)～1/1(土)は1,1/2(日)～1/08(土)は2
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string week(DateTime date)=>((date.DayOfYear+(int)date.DayOfWeek-1)/7+1).ToString();
        public static string weekday(DateTime date)=>
            Thread.CurrentThread.CurrentUICulture.DateTimeFormat.GetDayName(date.DayOfWeek);
        public static string hour(DateTime date)=>date.Hour.ToString();
        public static string minute(DateTime date)=>date.Minute.ToString();
        public static string second(DateTime date)=>date.Second.ToString();
        public static string millisecond(DateTime date)=>date.Millisecond.ToString();
        public static string microsecond(DateTime date)=>(date.Ticks/100000).ToString();
        public static string nanosecond(DateTime date)=>(date.Ticks/100).ToString();
        //public static int? year(DateTimeOffset? date)=>date?.Year;
        //public static int? month(DateTimeOffset? date)=>date?.Month;
        //public static int? day(DateTimeOffset? date)=>date?.Day;
        //public static int? dayofyear(DateTimeOffset? date)=>date?.DayOfYear;
        //public static int? quarter(DateTimeOffset? date)=> date is null?null:(date.Value.Month+3)/4;
        ///// <summary>
        ///// 1/1(日)～1/7(土)は1,1/8(日)～1/14(土)は2
        ///// 1/1(月)～1/6(土)は1,1/7(日)～1/13(土)は2
        ///// 1/1(火)～1/5(土)は1,1/6(日)～1/12(土)は2
        ///// 1/1(水)～1/4(土)は1,1/5(日)～1/11(土)は2
        ///// 1/1(木)～1/3(土)は1,1/4(日)～1/10(土)は2
        ///// 1/1(金)～1/2(土)は1,1/3(日)～1/09(土)は2
        ///// 1/1(土)～1/1(土)は1,1/2(日)～1/08(土)は2
        ///// </summary>
        ///// <param name="date"></param>
        ///// <returns></returns>
        //public static int? week(DateTimeOffset? date)=>
        //    date is null?null:(date.Value.DayOfYear+(int)date.Value.DayOfWeek-1)/7+1;
        //    //(date.DayOfYear+date.DayOfWeek-1)/7+1;
        //public static int? weekday(DateTimeOffset? date)=> date is null?null:(int)date.Value.DayOfWeek;
        //public static int? hour(DateTimeOffset? date)=>date?.Hour;
        //public static int? minute(DateTimeOffset? date)=>date?.Minute;
        //public static int? second(DateTimeOffset? date)=>date?.Second;
        //public static int? millisecond(DateTimeOffset? date)=>date?.Millisecond;
        //public static long? microsecond(DateTimeOffset? date)=>date?.Ticks/100000;
        //public static long? nanosecond(DateTimeOffset? date)=>date?.Ticks/100;
    }
    internal static class DATEPART {
        public static int year(DateTime date)=>date.Year;
        public static int month(DateTime date)=>date.Month;
        public static int day(DateTime date)=>date.Day;
        public static int dayofyear(DateTime date)=>date.DayOfYear;
        public static int quarter(DateTime date)=>(date.Month+3)/4;
        /// <summary>
        /// 1/1(日)～1/7(土)は1,1/8(日)～1/14(土)は2
        /// 1/1(月)～1/6(土)は1,1/7(日)～1/13(土)は2
        /// 1/1(火)～1/5(土)は1,1/6(日)～1/12(土)は2
        /// 1/1(水)～1/4(土)は1,1/5(日)～1/11(土)は2
        /// 1/1(木)～1/3(土)は1,1/4(日)～1/10(土)は2
        /// 1/1(金)～1/2(土)は1,1/3(日)～1/09(土)は2
        /// 1/1(土)～1/1(土)は1,1/2(日)～1/08(土)は2
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int week(DateTime date)=>(date.DayOfYear+(int)date.DayOfWeek-1)/7+1;
        //(date.DayOfYear+date.DayOfWeek-1)/7+1;
        public static int weekday(DateTime date)=> (int)date.DayOfWeek;
        public static int hour(DateTime date)=>date.Hour;
        public static int minute(DateTime date)=>date.Minute;
        public static int second(DateTime date)=>date.Second;
        public static int millisecond(DateTime date)=>date.Millisecond;
        public static long microsecond(DateTime date)=>date.Ticks/100000;
        public static long nanosecond(DateTime date)=>date.Ticks/100;
        public static long tzoffset(DateTime date)=>date.Ticks;
        public static long iso_week(DateTime date)=> System.Globalization.ISOWeek.GetWeekOfYear(date);
        //public static int? year(DateTime? date)=>date?.Year;
        //public static int? month(DateTime? date)=>date?.Month;
        //public static int? day(DateTime? date)=>date?.Day;
        //public static int? dayofyear(DateTime? date)=>date?.DayOfYear;
        //public static int? quarter(DateTime? date)=> date is null?null:(date.Value.Month+3)/4;
        ///// <summary>
        ///// 1/1(日)～1/7(土)は1,1/8(日)～1/14(土)は2
        ///// 1/1(月)～1/6(土)は1,1/7(日)～1/13(土)は2
        ///// 1/1(火)～1/5(土)は1,1/6(日)～1/12(土)は2
        ///// 1/1(水)～1/4(土)は1,1/5(日)～1/11(土)は2
        ///// 1/1(木)～1/3(土)は1,1/4(日)～1/10(土)は2
        ///// 1/1(金)～1/2(土)は1,1/3(日)～1/09(土)は2
        ///// 1/1(土)～1/1(土)は1,1/2(日)～1/08(土)は2
        ///// </summary>
        ///// <param name="date"></param>
        ///// <returns></returns>
        //public static int? week(DateTime? date)=>
        //    date is null?null:(date.Value.DayOfYear+(int)date.Value.DayOfWeek-1)/7+1;
        //    //(date.DayOfYear+date.DayOfWeek-1)/7+1;
        //public static int? weekday(DateTime? date)=> date is null?null:(int)date.Value.DayOfWeek;
        //public static int? hour(DateTime? date)=>date?.Hour;
        //public static int? minute(DateTime? date)=>date?.Minute;
        //public static int? second(DateTime? date)=>date?.Second;
        //public static int? millisecond(DateTime? date)=>date?.Millisecond;
        //public static long? microsecond(DateTime? date)=>date?.Ticks/100000;
        //public static long? nanosecond(DateTime? date)=>date?.Ticks/100;
    }
    //高精度のシステム日付/時刻関数
    //    sysdatetime
    //sysDateTime
    //    sysutcdatetime
    //低精度のシステム日付/時刻関数
    //    current_timestamp
    //    getdate
    //    getutcdate
    //日付と時刻の要素を返す関数
    //    datename
    //    datepart
    //datepart(mm,...)のmmはinterval識別子で特殊なのでswitchで処理した
    //    day
    public static int? day(DateTime? date)=>date?.Day;
    //    month
    public static int? month(DateTime? date)=>date?.Month;
    //    year
    public static int? year(DateTime? date)=>date?.Year;
    //要素から日付と時刻の値を返す関数
    //    datefromparts
    //    datetime2fromparts
    //    datetimefromparts
    //    DateTimefromparts
    //    smalldatetimefromparts
    //    timefromparts
    //日付と時刻の差の値を返す関数
    //    datediff
    //    datediff_big
    //日付と時刻の値を変更する関数
    //    dateadd
    //eomonth
    //    switchoffset
    //toDateTime
    //    セッションの形式の関数を設定または返す関数
    //    @@DATEFIRST
    //    @@LANGUAGE
    //    SET DATEFIRST
    //    SET DATEFORMAT
    //    SET LANGUAGE
    //    sp_helplanguage
    //日付と時刻の値を検証する関数
    //    isdate
    //日付と時刻に関連したトピック
    //    AT TIME ZONE (Transact-SQL)
    //CAST および CONVERT (Transact-SQL)
    public static object? cast([Type]Type data_type, byte[]? expression, int? style=0)=>
        convert(data_type,expression,style);
    public static object? convert([Type] Type data_type,object? expression,int? style = 0) {
        if(expression is null)return expression;
        var expression_type=expression.GetType();
        if(expression_type == typeof(byte          ))return convert(data_type,(byte          )expression,style);
        if(expression_type == typeof(short         ))return convert(data_type,(short         )expression,style);
        if(expression_type == typeof(int           ))return convert(data_type,(int           )expression,style);
        if(expression_type == typeof(long          ))return convert(data_type,(long          )expression,style);
        if(expression_type == typeof(float         ))return convert(data_type,(float         )expression,style);
        if(expression_type == typeof(double        ))return convert(data_type,(double        )expression,style);
        if(expression_type == typeof(decimal       ))return convert(data_type,(decimal       )expression,style);
        if(expression_type == typeof(bool          ))return convert(data_type,(bool          )expression,style);
        if(expression_type == typeof(DateTime      ))return convert(data_type,(DateTime      )expression,style);
        if(expression_type == typeof(DateTimeOffset))return convert(data_type,(DateTimeOffset)expression,style);
        if(expression_type == typeof(Guid          ))return convert(data_type,(Guid          )expression,style);
        if(expression_type == typeof(XDocument     ))return convert(data_type,(XDocument     )expression,style);
        if(expression_type == typeof(object        ))return expression;
        return null;
    }
    private static object? convert([Type] Type data_type,byte[] expression, int? style){
        if(data_type == typeof(byte))return expression[0];
        if(data_type == typeof(short))return BitConverter.ToInt16(expression);
        if(data_type == typeof(int))return BitConverter.ToInt32(expression);
        if(data_type == typeof(long))return BitConverter.ToInt64(expression);
        if(data_type == typeof(float))return BitConverter.ToInt64(expression);
        if(data_type == typeof(double))return BitConverter.ToInt64(expression);
        if(data_type == typeof(decimal)) {
            var lo = BitConverter.ToInt32(expression);
            var mid = BitConverter.ToInt32(expression,4);
            var hi = BitConverter.ToInt32(expression,8);
            var isNegative = BitConverter.ToBoolean(expression,12);
            var scale = expression[13];
            var d = new Decimal(lo,mid,hi,isNegative,scale);
            return d;
        }
        if(data_type == typeof(bool))return BitConverter.ToBoolean(expression);
        if(data_type == typeof(DateTime      ))return DateTime.FromBinary(BitConverter.ToInt64(expression));
        if(data_type == typeof(DateTimeOffset))return DateTimeOffset.FromUnixTimeMilliseconds(BitConverter.ToInt64(expression));
        if(data_type == typeof(Guid))return new Guid(expression);
        if(data_type == typeof(XDocument)) {
            var MemoryStream = new IO.MemoryStream(expression);
            var Result = XDocument.Load(MemoryStream);
            return Result;
        }
        if(data_type == typeof(object))return expression;
        return null;
    }
    private static object? convert([Type] Type data_type,long expression,int? style) {
        if(data_type == typeof(byte          ))return (byte   )expression;
        if(data_type == typeof(short         ))return (short  )expression;
        if(data_type == typeof(int           ))return (int    )expression;
        if(data_type == typeof(long          ))return          expression;
        if(data_type == typeof(float         ))return (float  )expression;
        if(data_type == typeof(double        ))return (double )expression;
        if(data_type == typeof(decimal       ))return (decimal)expression;
        if(data_type == typeof(bool          ))return expression!=0;
        if(data_type == typeof(DateTime      ))return DateTime.FromBinary(expression);
        if(data_type == typeof(DateTimeOffset))return DateTimeOffset.FromUnixTimeMilliseconds(expression);
        if(data_type == typeof(Guid          ))throw new NotSupportedException();
        if(data_type == typeof(XDocument     ))throw new NotSupportedException();
        if(data_type == typeof(object        ))return expression;
        return null;
    }
    private const byte byte0 = 0, byte1 = 1;
    private const short short0 = 0, short1 = 1;
    private static object? convert([Type] Type data_type,bool expression,int? style = 0) {
        if(data_type == typeof(byte          ))return expression?byte1:byte0;
        if(data_type == typeof(short         ))return expression?short1:short0;
        if(data_type == typeof(int           ))return expression?1:0;
        if(data_type == typeof(long          ))return expression?1L:0L;
        if(data_type == typeof(float         ))return expression?1F:0F;
        if(data_type == typeof(double        ))return expression?1D:0D;
        if(data_type == typeof(decimal       ))return expression?1M:0M;
        if(data_type == typeof(bool          ))return expression;
        if(data_type == typeof(DateTime      ))throw new NotSupportedException();
        if(data_type == typeof(DateTimeOffset))throw new NotSupportedException();
        if(data_type == typeof(Guid          ))throw new NotSupportedException();
        if(data_type == typeof(XDocument     ))throw new NotSupportedException();
        if(data_type == typeof(object        ))return expression;
        return null;
    }
    //FORMAT
    //    ODBC スカラー関数 (Transact-SQL)
    //国際化に対応した Transact-SQL ステートメントの記述
    //構成関数	現在の構成についての情報を返します。
    //変換関数	データ型のキャストと変換をサポートします。
    //カーソル関数	カーソルについての情報を返します。
    //JSON JSON データを検証、クエリ、または変更します。
    //数学 パラメーターとして関数に渡された入力値に基づいて計算を実行し、数値を返します。
    //    ABS
    //    ACOS
    //    ASIN
    //    ATAN
    //    ATN2
    //    CEILING
    //    COS
    //    COT
    //    DEGREES
    //    EXP
    //    FLOOR
    //    LOG
    //    LOG10
    //    PI
    //    POWER
    //    RADIANS  
    //    RAND  
    //    ROUND  
    public static double? round(double? numeric_expression,int? length,int? function=0) {
        if(numeric_expression is null||length is null)return null;
        return function is null or 0?Math.Round(numeric_expression.Value):Math.Ceiling(numeric_expression.Value);
    }
    //    SIGN  
    //    SIN  
    //    SQRT  
    //    SQUARE  
    //    TAN
    //論理	論理演算を実行します。
    //メタデータ関数	データベースおよびデータベース オブジェクトについての情報を返します。
    //    @@PROCID
    //    APP_NAME
    //    APPLOCK_MODE
    //    APPLOCK_TEST
    //    ASSEMBLYPROPERTY
    //    COL_LENGTH
    //    COL_NAME
    public static string? col_name(int?table_id,int? column_id) {
        if(table_id is null||column_id is null) return null;
        return null;
    }
    //    COLUMNPROPERTY
    public static int? columnproperty(int?id,string? column,string?property) {
        if(id is null||column is null|property is null) return null;
        return 0;
    }
    public static int? columnpropertyex(int?id,string? column,string?property) {
        if(id is null||column is null|property is null) return null;
        return 0;
    }
    //    DATABASE_PRINCIPAL_ID
    //    DATABASEPROPERTYEX
    public static string? databasepropertyex(string? database,string?property) {
        if(database is null||property is null) return null;
        return "dbo";
    }
    //    DB_ID
    //    DB_NAME
    /// <summary>
    /// データベース名。例えばmaster,msdb
    /// </summary>
    /// <param name="database_id"></param>
    /// <returns></returns>
    public static string? db_name(int? database_id=null) {
        return "msdb";
    }
    //    FILE_ID
    //    FILE_IDEX
    //    FILE_NAME
    //    FILEGROUP_ID
    //    FILEGROUP_NAME
    //    FILEGROUPPROPERTY
    //    FILEPROPERTY
    //    FULLTEXTCATALOGPROPERTY
    //    FULLTEXTSERVICEPROPERTY
    //    INDEX_COL  
    //    INDEXKEY_PROPERTY  
    //    INDEXPROPERTY  
    //    NEXT VALUE FOR  
    //    OBJECT_DEFINITION  
    public static int? object_definition(int?object_id,int?Param2=null,int?Param3=null) {
        return 1;
    }
    //    OBJECT_ID  
    public static int? object_id(string object_name) {
        return Type.GetType(object_name)?.MetadataToken;
    }
    //    OBJECT_NAME  
    //    OBJECT_SCHEMA_NAME  
    //    OBJECTPROPERTY  
    //    OBJECTPROPERTYEX  
    //    ORIGINAL_DB_NAME  
    //    PARSENAME  
    //    SCHEMA_ID  
    //    SCHEMA_NAME  
    /// <summary>
    /// スキーマ名を返す。例えばdbo
    /// </summary>
    /// <param name="schema_id"></param>
    /// <returns></returns>
    public static string? schema_name(int? schema_id=null) {
        if(schema_id is null||schema_id.Value<=0) return null;
        return "dbo";
    }
    //    SCOPE_IDENTITY  
    //    SERVERPROPERTY  
    //    STATS_DATE  
    //    TYPE_ID  
    //    TYPE_NAME  
    public static string? type_name(int? type_id=null)=>type_id switch {
        0=>"void type",
        1=>"table",
        _=>null
    };
    //    TYPEPROPERTY  
    public static string? typeproperty(string?type,string?property){
        return type;
    }
    //    VERSION  

    //順位付け 順位付け関数により、パーティションの各行の順位値が返されます。 使用する関数によっては、いくつかの行で、他の行と同じ値を受け取る場合があります。 順位付け関数は非決定的です。

    //レプリケーション

    //セキュリティ関数	ユーザーとロールについての情報を返します。
    //    CERTENCODED (Transact-SQL)
    //    CERTPRIVATEKEY (Transact-SQL)
    //    CURRENT_USER (Transact-SQL)
    //    DATABASE_PRINCIPAL_ID (Transact-SQL)
    public static int? database_principal_id() {
        return null;
    }
    public static int? database_principal_id(string principal_name) {
        return null;
    }
    //    HAS_PERMS_BY_NAME (Transact-SQL)
    //    IS_MEMBER (Transact-SQL)
    public static int? is_member(string principal_name) {
        return null;
    }
    //    IS_ROLEMEMBER (Transact-SQL)
    /// <summary>
    /// 
    /// </summary>
    /// <param name="role">確認するデータベース ロールの名前を指定します。 role は sysnameです。</param>
    /// <param name="database_principal">確認するデータベース ユーザー、データベース ロール、またはアプリケーション ロールの名前です。 database_principal は sysname, 、既定値は NULL です。 値を指定しない場合、結果は現在の実行コンテキストに基づきます。 パラメーターに "NULL" という語が含まれていると、NULL が返されます。</param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static int? is_rolemember(string? role,string? database_principal=null) {
        if(role=="dbo")return 1;
        return null;
    }
    //    IS_SRVROLEMEMBER (Transact-SQL)
    public static int? is_srvrolemember(string? role,string? login=null) {
        //[sysadmin]
        //serveradmin
        //dbcreator
        //setupadmin
        //bulkadmin
        //securityadmin
        //diskadmin
        //public
        //processadmin
        return 1;
    }
    //    LOGINPROPERTY (Transact-SQL)
    //    ORIGINAL_LOGIN (Transact-SQL)
    public static string? original_login(){
        return "original_login";
    }
    //    PERMISSIONS (Transact-SQL)
    //    PWDCOMPARE (Transact-SQL)
    //    PWDENCRYPT (Transact-SQL)
    //    SCHEMA_ID (Transact-SQL)
    //    SCHEMA_NAME (Transact-SQL)
    //    SESSION_USER (Transact-SQL)
    //    SUSER_ID (Transact-SQL)
    public static int? suser_sid()=>0;
    public static int? suser_sid(string? login)=>0;
    public static int? suser_sid(string? login,int? Param2)=>0;
    //    SUSER_NAME (Transact-SQL)
    //    SUSER_SID (Transact-SQL)
    //    SUSER_SNAME (Transact-SQL)
    /// <summary>
    /// セキュリティ ID 番号 (SID) に関連付けられているログイン名を返します。
    /// </summary>
    /// <returns>nvarchar(128)</returns>
    public static string? suser_sname() {
        return null;
    }
    /// <summary>
    /// セキュリティ ID 番号 (SID) に関連付けられているログイン名を返します。
    /// </summary>
    /// <param name="server_user_id">varbinary(85)</param>
    /// <returns>nvarchar(128)</returns>
    public static string? suser_sname(byte[]?server_user_id) {
        return null;
    }
    //    sys.fn_builtin_permissions (Transact-SQL)
    //    sys.fn_get_audit_file (Transact-sql SQL)
    //    sys.fn_my_permissions (Transact-SQL)
    //    SYSTEM_USER (Transact-SQL)
    //    USER_ID (Transact-SQL)
    //    USER_NAME (Transact-SQL)
    public static string? user_name() {
        return null;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">データベース ユーザーに関連付けられている識別番号を指定します。 id は int です。かっこが必要です。</param>
    /// <returns>nvarchar(128)</returns>
    public static string? user_name(int? id) {
        return null;
    }
    //文字列関数	文字列型 (char または varchar) の入力値に対して操作を実行し、文字列値または数値を返します。
    //    ASCII
    //    CHAR
    //    CHARINDEX
    public static int charindex(string? expressionToFind ,string? expressionToSearch,long? start_location=1){
        if(expressionToFind is null||expressionToSearch is null||start_location.GetValueOrDefault()<=0)return 0;
        var index=expressionToSearch.IndexOf(expressionToFind, StringComparison.Ordinal);
        if(index<0)return 0;
        index++;
        if(index>=start_location.GetValueOrDefault())return index;
        return 0;
    }
    //    CONCAT
    public static string? concat(string? string_value1,string? string_value2){
        return string_value1+string_value2;
    }
    public static string? concat(string? string_value1,string? string_value2,string? string_value3){
        return string_value1+string_value2+string_value3;
    }
    public static string? concat(string? string_value1,string? string_value2,string? string_value3,string? string_value4){
        return string_value1+string_value2+string_value3;
    }
    //    CONCAT_WS
    //    DIFFERENCE
    //    FORMAT
    //    LEFT
    public static string? left(string? character_expression,int? integer_expression){
        if(character_expression is null||integer_expression is null)return null;
        return character_expression[..integer_expression.GetValueOrDefault()];
    }
    //    LEN
    public static int? len(string? character_expression)=>character_expression?.Length;
    //    LOWER
    public static string? lower(string? character_expression)=>character_expression?.ToLower(CultureInfo.InvariantCulture);
    //    LTRIM
    public static string? ltrim(string? character_expression)=>character_expression?.TrimStart();
    //    NCHAR
    //    PATINDEX
    //    QUOTENAME
    //    REPLACE
    public static string? replace(string? string_expression,string? string_pattern,string? string_replacement) {
        if(string_expression is null||string_pattern is null||string_replacement is null)return null;
        return string_expression.Replace(string_pattern,string_replacement);
    }
    //    REPLICATE
    public static string? replicate(string? string_expression,int? integer_expression){
        if(string_expression is null||integer_expression is null)return null;
        var sb=new StringBuilder();
        for(var a=0;a<integer_expression;a++)
            sb.Append(string_expression);
        return sb.ToString();
    }
    //    REVERSE
    //    RIGHT
    public static string? right(string? character_expression,int? integer_expression){
        if(character_expression is null||integer_expression is null)return null;
        Debug.Assert(character_expression.Substring(character_expression.Length,integer_expression.GetValueOrDefault())==character_expression[^integer_expression.GetValueOrDefault()..]);
        return character_expression[^integer_expression.GetValueOrDefault()..];
        //return character_expression.Substring(character_expression.Length-integer_expression.GetValueOrDefault());
    }
    //    RTRIM
    public static string? rtrim(string? character_expression)=>character_expression?.TrimEnd();
    //    SOUNDEX
    //    SPACE
    public static string? space(int? expression)=>expression is null?null:new string(' ',expression.Value);
    //    STR
    //    STRING_AGG
    //    STRING_ESCAPE
    //    STRING_SPLIT
    //    STUFF
    //    SUBSTRING
    public static string? substring(string? expression,int? start,int? length) {
        if(expression is null||start is null||length is null)return null;
        //return expression[(start.GetValueOrDefault()-1)..(length.GetValueOrDefault()+1)];
        var i0=start.GetValueOrDefault()-1;
        var i1=i0+length.GetValueOrDefault();
        Debug.Assert(expression[i0..i1]==expression.Substring(i0,length.GetValueOrDefault()));
        return expression[i0..i1];
        //return expression.Substring(start.GetValueOrDefault()-1,length.GetValueOrDefault());
    }
    //    TRANSLATE
    //    TRIM
    //    UNICODE
    //    UPPER
    public static string? upper(string? character_expression)=>character_expression?.ToUpperInvariant();
    //システム関数	メソッドのインスタンスで操作を実行し、値、オブジェクト、および設定に関する情報を返SQL Server。
    //    @@ERROR
    //    @@IDENTITY
    //    @@PACK_RECEIVED
    //    @@ROWCOUNT
    //    @@TRANCOUNT
    //    $PARTITION
    //    BINARY_CHECKSUM
    //    CHECKSUM
    //    COMPRESS
    //    CONNECTIONPROPERTY
    //    CONTEXT_INFO
    //    CURRENT_REQUEST_ID
    //    CURRENT_TRANSACTION_ID
    //    DECOMPRESS
    //    ERROR_LINE
    //    ERROR_MESSAGE
    //    ERROR_NUMBER
    //    ERROR_PROCEDURE
    //    ERROR_SEVERITY
    //    ERROR_STATE
    //    FORMATMESSAGE
    public static string? formatmessage(int? msg_number,params string?[]? param_value)=>msg_number switch {
        null=>null,
        0=>"エラーメッセージ0",
        _=>throw new NotSupportedException($"エラーコード{msg_number.Value}に対応するメッセージがない。")
    };
    //    GET_FILESTREAM_TRANSACTION_CONTEXT
    //    GETANSINULL
    //    HOST_ID
    //    HOST_NAME
    //    ISNULL
    //    ISNUMERIC
    //    MIN_ACTIVE_ROWVERSION
    //    NEWID
    //    NEWSEQUENTIALID
    //    ROWCOUNT_BIG
    //    SESSION_CONTEXT
    /// <summary>
    /// SESSION_CONTEXT(N'user_id')
    /// </summary>
    /// <param name="key">nvarchar</param>
    /// <returns></returns>
    public static int? session_context(string? key) {
        return 4;
    }
    //    SESSION_ID
    //    XACT_STATE
    //システム統計関数	システムについての統計情報を返します。
    //テキストとイメージ関数	テキスト入力値、イメージ入力値、または列に対して操作を実行し、値についての情報を返します。
    //バイナリー
    public static ImmutableSet<XElement> nodes(XDocument XDocument,string XPath) => new Set<XElement>(XDocument.XPathSelectElements(XPath));
    public static IEnumerable<XElement> query(XNode XNode,string? XPath) => XNode.XPathSelectElements(XPath);
    public static bool? value_Boolean(XDocument XDocument,string XPath)
    {
        var Element = XDocument.XPathSelectElement(XPath);
        if(Element is null) return null;
        return bool.TryParse(Element.Value,out var result) ? result : null;
    }
    public static sbyte? value_SByte(XDocument XDocument,string XPath)
    {
        var Element = XDocument.XPathSelectElement(XPath);
        if(Element is null) return null;
        return sbyte.TryParse(Element.Value,out var result) ? result : null;
    }
    public static short? value_Int16(XDocument XDocument,string XPath){
        var Element = XDocument.XPathSelectElement(XPath);
        if(Element is null) return null;
        return short.TryParse(Element.Value,out var result) ? result : null;
    }
    public static int? value_Int32(XDocument XDocument,string XPath){
        var Element = XDocument.XPathSelectElement(XPath);
        if(Element is null) return null;
        return int.TryParse(Element.Value,out var result) ? result : null;
    }
    public static long? value_Int64(XDocument XDocument,string XPath){
        var Element = XDocument.XPathSelectElement(XPath);
        if(Element is null) return null;
        return long.TryParse(Element.Value,out var result) ? result : null;
    }
    public static float? value_Single(XDocument XDocument,string XPath){
        var Element = XDocument.XPathSelectElement(XPath);
        if(Element is null) return null;
        return float.TryParse(Element.Value,out var result) ? result : null;
    }
    public static double? value_Double(XDocument XDocument,string XPath){
        var Element = XDocument.XPathSelectElement(XPath);
        if(Element is null) return null;
        return double.TryParse(Element.Value,out var result) ? result : null;
    }
    public static decimal? value_Decimal(XDocument XDocument,string XPath) {
        var Element = XDocument.XPathSelectElement(XPath);
        if(Element is null) return null;
        return decimal.TryParse(Element.Value,out var result) ? result : null;
    }
    public static DateTime? value_DateTime(XDocument XDocument,string XPath)
    {
        var Element = XDocument.XPathSelectElement(XPath);
        if(Element is null) return null;
        return DateTime.TryParse(Element.Value,out var result) ? result : null;
    }
    public static DateTimeOffset? value_DateTimeOffset(XDocument XDocument,string XPath)
    {
        var Element = XDocument.XPathSelectElement(XPath);
        if(Element is null) return null;
        return DateTimeOffset.TryParse(Element.Value,out var result) ? result : null;
    }
    public static Guid? value_Guid(XDocument XDocument,string XPath)
    {
        var Element = XDocument.XPathSelectElement(XPath);
        if(Element is null) return null;
        return Guid.TryParse(Element.Value,out var result) ? result : null;
    }
    public static string? value_String(XDocument XDocument,string XPath) => XDocument.XPathSelectElement(XPath)?.Value;
    public static XElement? value_XElement(XDocument XDocument,string XPath) => XDocument.XPathSelectElement(XPath);
    /// <summary>
    /// GZIPアルゴリズムを使用して、入力式を圧縮します。
    /// </summary>
    /// <param name="source"></param>
    /// <returns>圧縮結果のByte[]</returns>
    public static byte[] compress_string(string source) {
        var Input = Encoding.Unicode.GetBytes(source);
        using var MemoryStream = new IO.MemoryStream();
        using var GZipStream = new GZipStream(MemoryStream,CompressionMode.Compress);
        GZipStream.Write(Input,0,Input.Length);
        GZipStream.Flush();
        MemoryStream.Flush();
        return MemoryStream.GetBuffer();
    }
    /// <summary>
    /// GZIPアルゴリズムを使用して、入力式を圧縮します。
    /// </summary>
    /// <param name="source"></param>
    /// <returns>圧縮結果のByte[]</returns>
    public static byte[] compress(byte[] source) {
        using var MemoryStream = new IO.MemoryStream();
        using var GZipStream = new GZipStream(MemoryStream,CompressionMode.Compress);
        GZipStream.Write(source,0,source.Length);
        GZipStream.Flush();
        MemoryStream.Flush();
        return MemoryStream.GetBuffer();
    }
    /// <summary>
    /// GZIPアルゴリズムを使用して、入力式の値の圧縮を解除します
    /// </summary>
    /// <param name="source"></param>
    /// <returns>圧縮解除結果のByte[]</returns>
    public static byte[] decompress(byte[] source) {
        using var InputStream = new IO.MemoryStream(source);
        using var GZipStream = new GZipStream(InputStream,CompressionMode.Decompress);
        var Output = new List<byte>();
        while(true) {
            var result = GZipStream.ReadByte();
            if(result<0) break;
            Output.Add((byte)result);
        }
        return Output.ToArray();
    }
    /// <summary>
    /// https://docs.microsoft.com/ja-jp/sql/t-sql/functions/serverproperty-transact-sql?view=sql-server-ver15
    /// </summary>
    /// <param name="propertyname"></param>
    /// <returns></returns>
    public static string serverproperty(string propertyname)
    {
        return propertyname;
    }
    public static string CASTBytesString(byte[] source) => Encoding.Unicode.GetString(source);
    public static byte[] CASTStringBytes(string source) => Encoding.Unicode.GetBytes(source);
}
