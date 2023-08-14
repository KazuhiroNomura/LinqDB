//using System.IO;
using System.Collections.Generic;
using System.Reflection;

// ReSharper disable AssignNullToNotNullAttribute
namespace LinqDB.Product.SQLServer;
internal static class Reflection {
    private static MethodInfo i(string Name){
        var Method=typeof(Methods.DATEPART).GetMethod(Name);
        if(Method is null)throw new KeyNotFoundException(Name);
        return Method;
    }

    internal static class Internal{
        public static readonly MethodInfo Add=i(nameof(Methods.Internal.Add));
    }
    //public static readonly MethodInfo database_principal_id0 = M(() => Methods.database_principal_id());
    //public static readonly MethodInfo database_principal_id1 = M(() => Methods.database_principal_id(""));
    //public static readonly MethodInfo is_member = A(nameof(Methods.is_member));
    //public static readonly MethodInfo user_name0 = M(() => Methods.user_name());
    //public static readonly MethodInfo user_name1 = M(() => Methods.user_name(0));
    //public static readonly MethodInfo object_id = A(nameof(Methods.object_id));
    //public static readonly MethodInfo formatmessage = A(nameof(Methods.formatmessage));

    //public static readonly MethodInfo getdate = A(nameof(Methods.getdate));
    internal static class DATENAME{
        public static readonly MethodInfo year       =i(nameof(Methods.DATENAME.year));
        public static readonly MethodInfo month      =i(nameof(Methods.DATENAME.month));
        public static readonly MethodInfo day        =i(nameof(Methods.DATENAME.day));
        public static readonly MethodInfo dayofyear  =i(nameof(Methods.DATENAME.dayofyear));
        public static readonly MethodInfo quarter    =i(nameof(Methods.DATENAME.quarter));
        public static readonly MethodInfo week       =i(nameof(Methods.DATENAME.week));
        public static readonly MethodInfo weekday    =i(nameof(Methods.DATENAME.weekday));
        public static readonly MethodInfo hour       =i(nameof(Methods.DATENAME.hour));
        public static readonly MethodInfo minute     =i(nameof(Methods.DATENAME.minute));
        public static readonly MethodInfo second     =i(nameof(Methods.DATENAME.second));
        public static readonly MethodInfo millisecond=i(nameof(Methods.DATENAME.millisecond));
        public static readonly MethodInfo microsecond=i(nameof(Methods.DATENAME.microsecond));
        public static readonly MethodInfo nanosecond =i(nameof(Methods.DATENAME.nanosecond));
    }
    internal static class DATEPART{
        public static readonly MethodInfo year       =i(nameof(Methods.DATEPART.year));
        public static readonly MethodInfo month      =i(nameof(Methods.DATEPART.month));
        public static readonly MethodInfo day        =i(nameof(Methods.DATEPART.day));
        public static readonly MethodInfo dayofyear  =i(nameof(Methods.DATEPART.dayofyear));
        public static readonly MethodInfo quarter    =i(nameof(Methods.DATEPART.quarter));
        public static readonly MethodInfo week       =i(nameof(Methods.DATEPART.week));
        public static readonly MethodInfo weekday    =i(nameof(Methods.DATEPART.weekday));
        public static readonly MethodInfo hour       =i(nameof(Methods.DATEPART.hour));
        public static readonly MethodInfo minute     =i(nameof(Methods.DATEPART.minute));
        public static readonly MethodInfo second     =i(nameof(Methods.DATEPART.second));
        public static readonly MethodInfo millisecond=i(nameof(Methods.DATEPART.millisecond));
        public static readonly MethodInfo microsecond=i(nameof(Methods.DATEPART.microsecond));
        public static readonly MethodInfo nanosecond =i(nameof(Methods.DATEPART.nanosecond));
        public static readonly MethodInfo tzoffset   =i(nameof(Methods.DATEPART.tzoffset));
        public static readonly MethodInfo iso_week   =i(nameof(Methods.DATEPART.iso_week));
    }
    //public static readonly MethodInfo ltrim = A(nameof(Methods.ltrim));
    //public static readonly MethodInfo rtrim = A(nameof(Methods.rtrim));
    public static readonly MethodInfo left       =m(nameof(Methods.left));
    public static readonly MethodInfo right      =m(nameof(Methods.right));
    //public static readonly MethodInfo len= A(nameof(Methods.len));
    //public static readonly MethodInfo replace = A(nameof(Methods.replace));
    //public static readonly MethodInfo replicate = A(nameof(Methods.replicate));
    //public static readonly MethodInfo substring = A(nameof(Methods.substring));
    //public static readonly MethodInfo upper= A(nameof(Methods.upper));
    //public static readonly MethodInfo lower= A(nameof(Methods.lower));
    //public static readonly MethodInfo datalength_bytes= A(nameof(Methods.datalength_bytes));
    //public static readonly MethodInfo datalength_string= A(nameof(Methods.datalength_string));
    //public static readonly MethodInfo datalength_object= A(nameof(Methods.datalength_object));
    //構成関数	現在の構成についての情報を返します。
    //変換関数	データ型のキャストと変換をサポートします。
    public static readonly MethodInfo cast = m(nameof(Methods.cast));
    public static readonly MethodInfo convert = m(nameof(Methods.convert));
    //カーソル関数	カーソルについての情報を返します。
    //日付と時刻のデータ型および関数	日付時刻型の入力値に対して操作を実行し、文字列値、数値、または日付時刻値を返します。
    //JSON 関数	JSON データを検証、クエリ、または変更します。
    //論理関数	論理演算を実行します。
    //数学関数	パラメーターとして関数に渡された入力値に基づいて計算を実行し、数値を返します。
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
    //public static readonly MethodInfo round= A(nameof(Methods.round));
    //    SIGN  
    //    SIN  
    //    SQRT  
    //    SQUARE  
    //    TAN
    //メタデータ関数	データベースおよびデータベース オブジェクトについての情報を返します。
    //セキュリティ関数	ユーザーとロールについての情報を返します。
    //    CERTENCODED (Transact-SQL)
    //    PWDCOMPARE (Transact-SQL)
    //    CERTPRIVATEKEY (Transact-SQL)
    //    PWDENCRYPT (Transact-SQL)
    //    CURRENT_USER (Transact-SQL)
    //    SCHEMA_ID (Transact-SQL)
    //    DATABASE_PRINCIPAL_ID (Transact-SQL)
    //    SCHEMA_NAME (Transact-SQL)
    //    sys.fn_builtin_permissions (Transact-SQL)
    //    SESSION_USER (Transact-SQL)
    //    sys.fn_get_audit_file (Transact-sql SQL)
    //    SUSER_ID (Transact-SQL)
    //public static readonly MethodInfo suser_id         = m(nameof(Methods.suser_id));
    //    sys.fn_my_permissions (Transact-SQL)
    //    SUSER_SID (Transact-SQL)
    //    HAS_PERMS_BY_NAME (Transact-SQL)
    //    SUSER_SNAME (Transact-SQL)
    //    IS_MEMBER (Transact-SQL)
    //    SYSTEM_USER (Transact-SQL)
    //    IS_ROLEMEMBER (Transact-SQL)
    //    SUSER_NAME (Transact-SQL)
    //    IS_SRVROLEMEMBER (Transact-SQL)
    //    USER_ID (Transact-SQL)
    //    LOGINPROPERTY (Transact-SQL)
    //    USER_NAME (Transact-SQL)
    //    ORIGINAL_LOGIN (Transact-SQL)
    //    PERMISSIONS (Transact-SQL)
    //文字列関数	文字列型 (char または varchar) の入力値に対して操作を実行し、文字列値または数値を返します。
    //システム関数	メソッドのインスタンスで操作を実行し、値、オブジェクト、および設定に関する情報を返SQL Server。
    //システム統計関数	システムについての統計情報を返します。
    //テキストとイメージ関数	テキスト入力値、イメージ入力値、または列に対して操作を実行し、値についての情報を返します。
    //バイナリー

    private static MethodInfo m(string name)=>typeof(Methods).GetMethod(name);
    public static readonly MethodInfo nodes           = m(nameof(Methods.nodes));
    public static readonly MethodInfo value_Boolean   = m(nameof(Methods.value_Boolean));
    public static readonly MethodInfo value_SByte     = m(nameof(Methods.value_SByte));
    public static readonly MethodInfo value_Int16     = m(nameof(Methods.value_Int16));
    public static readonly MethodInfo value_Int32     = m(nameof(Methods.value_Int32));
    public static readonly MethodInfo value_Int64     = m(nameof(Methods.value_Int64));
    public static readonly MethodInfo value_Single    = m(nameof(Methods.value_Single));
    public static readonly MethodInfo value_Double    = m(nameof(Methods.value_Double));
    public static readonly MethodInfo value_Decimal   = m(nameof(Methods.value_Decimal));
    public static readonly MethodInfo value_DateTime  = m(nameof(Methods.value_DateTimeOffset));
    public static readonly MethodInfo value_Guid      = m(nameof(Methods.value_Guid));
    public static readonly MethodInfo value_String    = m(nameof(Methods.value_String));
    public static readonly MethodInfo value_XElement  = m(nameof(Methods.value_XElement));
    public static readonly MethodInfo compress_String = m(nameof(Methods.compress_string));
    public static readonly MethodInfo compress_Bytes  = m(nameof(Methods.compress));
    public static readonly MethodInfo decompress_Bytes= m(nameof(Methods.decompress));
    public static readonly MethodInfo serverproperty  = m(nameof(Methods.serverproperty));
    public static readonly MethodInfo CASTBytesString = m(nameof(Methods.CASTBytesString));
    public static readonly MethodInfo CASTStringBytes = m(nameof(Methods.CASTStringBytes));
}
