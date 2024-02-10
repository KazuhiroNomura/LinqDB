using System.Linq;
using System.Diagnostics;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using System.Collections.Generic;
using System.Reflection;
using LinqDB.Databases.Attributes;
using e = System.Linq.Expressions;
using LinqDB.Helpers;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
using static Common;
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression PrimaryExpression(PrimaryExpression x)=>x switch{
        AtTimeZoneCall                y=>this.AtTimeZoneCall(y),
        CaseExpression                y=>this.CaseExpression(y),
        CastCall                      y=>this.CastCall(y),
        CoalesceExpression            y=>this.CoalesceExpression(y),
        ColumnReferenceExpression     y=>this.ColumnReferenceExpression(y),
        ConvertCall                   y=>this.ConvertCall(y),
        FunctionCall                  y=>this.FunctionCall(y),
        IIfCall                       y=>this.IIfCall(y),
        LeftFunctionCall              y=>this.LeftFunctionCall(y),
        NextValueForExpression        y=>this.NextValueForExpression(y),
        NullIfExpression              y=>this.NullIfExpression(y),
        OdbcFunctionCall              y=>this.OdbcFunctionCall(y),
        ParameterlessCall             y=>this.ParameterlessCall(y),
        ParenthesisExpression         y=>this.ParenthesisExpression(y),
        ParseCall                     y=>this.ParseCall(y),
        PartitionFunctionCall         y=>this.PartitionFunctionCall(y),
        RightFunctionCall             y=>this.RightFunctionCall(y),
        ScalarSubquery                y=>this.ScalarSubquery(y),
        TryCastCall                   y=>this.TryCastCall(y),
        TryConvertCall                y=>this.TryConvertCall(y),
        TryParseCall                  y=>this.TryParseCall(y),
        UserDefinedTypePropertyAccess y=>this.UserDefinedTypePropertyAccess(y),
        ValueExpression               y=>this.ValueExpression(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression AtTimeZoneCall(AtTimeZoneCall x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CaseExpression(CaseExpression x)=>x switch{
        SimpleCaseExpression y=>this.SimpleCaseExpression(y),
        SearchedCaseExpression y=>this.SearchedCaseExpression(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression CastCall(CastCall x){
        var Parameter=this.ScalarExpression(x.Parameter);
        var 変換先Type=DataTypeReferenceからTypeに変換(x.DataType);
        return this.Convertデータ型を合わせるNullableは想定する(Parameter,変換先Type);
    }
    private e.Expression CoalesceExpression(CoalesceExpression x){
        //coalesce(a,b,c,d)
        //引数を順番に評価し、NULL と評価されない最初の式の現在の値
        //a??b??c??d
        //var Result=this.ScalarExpression(x.Collation);
        var x_Expressions=x.Expressions;
        var Result=this.ScalarExpression(x_Expressions[0]);
        for(var a=1;a<x_Expressions.Count;a++)
            Result=e.Expression.Coalesce(Result,this.ScalarExpression(x_Expressions[a]));
        return Result;
    }
    private e.Expression ColumnReferenceExpression(ColumnReferenceExpression x){
        switch(x.ColumnType){
            case ColumnType.Regular:{
                var 識別子=this.SQL取得(x);
                var Result=this._StackSubquery単位の情報[識別子];
                Debug.Assert(Result is not null);
                return Result;
            }
            default:{
                throw this.単純NotSupportedException(x);
            }
        }
    }
    private e.Expression ConvertCall(ConvertCall x){
        //convert(datetime,'2020-3-4 1:2:3'),101)
        //var Convert=Constantを最適化(this.ScalarExpression(x.Parameter),DBTypeからTypeに変換(x.DataType));
        var 変換元=this.ScalarExpression(x.Parameter);
        var 変換先_Type=DataTypeReferenceからTypeに変換(x.DataType);
        return this.Convertデータ型を合わせるNullableは想定する(変換元,変換先_Type);
    }
    private e.Expression FunctionCall(FunctionCall x){
        var FunctionName = x.FunctionName.Value.ToUpperInvariant();
        var x_Parameters = x.Parameters;
        switch(FunctionName) {
            //構成関数	現在の構成についての情報を返します。
            //変換関数	データ型のキャストと変換をサポートします。
            //カーソル関数	カーソルについての情報を返します。
            //日付と時刻のデータ型および関数	日付時刻型の入力値に対して操作を実行し、文字列値、数値、または日付時刻値を返します。
            case "DATEADD":{//(datepart,number,date)
                var datepart = (ColumnReferenceExpression)x_Parameters[0];
                Debug.Assert(datepart.ColumnType == ColumnType.Regular);
                Debug.Assert(datepart.MultiPartIdentifier.Identifiers.Count == 1);
                var number = this.ScalarExpression(x_Parameters[1]);
                //dateadd(,,ここは日付型であってoffsetはいらない)
                var date = this.Convertデータ型を合わせるNullableは想定する(this.ScalarExpression(x_Parameters[2]),typeof(DateTime));
                return datepart.MultiPartIdentifier.Identifiers[0].Value.ToUpperInvariant()switch{
                    "YYYY"or"YY"or"YEAR"     =>dateadd_int(Reflection.DateTime.AddYear),
                    "QQ"  or"Q" or"QUARTER"  =>dateadd_int(Reflection.DateTime.AddQuater),
                    "MM"  or"M" or"MONTH"    =>dateadd_int(Reflection.DateTime.AddMonths),
                    "DY"  or"Y" or"DAYOFYEAR"=>throw new NotSupportedException(datepart.MultiPartIdentifier.Identifiers[0].Value),
                    "DD"  or"D" or"DAY"      =>throw new NotSupportedException(datepart.MultiPartIdentifier.Identifiers[0].Value),
                    "DW"  or"W" or"WEEKDAY"  =>dateadd_double(Reflection.DateTime.AddDays),
                    "WK"  or"WW"or"WEEK"     =>dateadd_double(Reflection.DateTime.AddWeek),
                    "HH"  or"HOUR"           =>dateadd_double(Reflection.DateTime.AddHours),
                    "MI"  or"N" or"MINUTE"   =>dateadd_double(Reflection.DateTime.AddMinutes),
                    "SS"  or"S" or"SECOND"   =>dateadd_double(Reflection.DateTime.AddSeconds),
                    "MS"  or"MILLISECOND"    =>dateadd_double(Reflection.DateTime.AddMilliseconds),
                    "MCS" or"MICROSECOND"    =>dateadd_long(Reflection.DateTime.AddTicks,Constant_100000),
                    "NS"  or"NANOSECOND"     =>dateadd_long(Reflection.DateTime.AddTicks,Constant_100),
                    _=>throw new NotSupportedException(datepart.MultiPartIdentifier.Identifiers[0].Value),
                };
                e.MethodCallExpression dateadd_int(MethodInfo Method) => dateadd(Method,typeof(int));
                e.MethodCallExpression dateadd_double(MethodInfo Method) => dateadd(Method,typeof(double));
                e.MethodCallExpression dateadd(MethodInfo Method,Type Type) {
                    Debug.Assert(Method.GetParameters()[0].ParameterType == Type);
                    return e.Expression.Call(date,Method,this.Convertデータ型を合わせるNullableは想定する(number,Type));
                }
                e.BinaryExpression dateadd_long(MethodInfo Method,e.Expression Constant) => e.Expression.Divide(dateadd(Method,typeof(int)),Constant);
            }
            case "DATEDIFF":{
                //datediff(mm,startdate,enddate)。enddate-startdate。小さな数値に切り捨てられる
                Debug.Assert(x_Parameters.Count == 3);
                var y = (ColumnReferenceExpression)x_Parameters[0];
                var Method=y.MultiPartIdentifier.Identifiers[0].Value.ToUpperInvariant()switch{
                    "YEAR" or"YY"   or"YYYY"     =>Product.SQLServer.Reflection.DATEPART.year,
                    "QQ"   or"Q"    or"QUARTER"  =>Product.SQLServer.Reflection.DATEPART.quarter,
                    "MM"   or"M"    or"MONTH"    =>Product.SQLServer.Reflection.DATEPART.month,
                    "DY"   or"Y"    or"DAYOFYEAR"=>Product.SQLServer.Reflection.DATEPART.dayofyear,
                    "DD"   or"D"    or"DAY"      =>Product.SQLServer.Reflection.DATEPART.day,
                    "WK"   or"WW"   or"WEEK"     =>Product.SQLServer.Reflection.DATEPART.week,//その年の何週目か
                    "DW"   or"W"    or"WEEKDAY"  =>Product.SQLServer.Reflection.DATEPART.weekday,
                    "HH"   or"HOUR"              =>Product.SQLServer.Reflection.DATEPART.hour,
                    "MI"   or"N"    or"MINUTE"   =>Product.SQLServer.Reflection.DATEPART.minute,
                    "SS"   or"S"    or"SECOND"   =>Product.SQLServer.Reflection.DATEPART.second,
                    "MS"   or"MILLISECOND"       =>Product.SQLServer.Reflection.DATEPART.millisecond,
                    "MCS"  or"MICROSECOND"       =>Product.SQLServer.Reflection.DATEPART.microsecond,
                    "NS"   or"NANOSECOND"        =>Product.SQLServer.Reflection.DATEPART.nanosecond,
                    _=>throw new NotSupportedException(y.MultiPartIdentifier.Identifiers[0].Value)
                };
                // 2021/11/11-2021/10/11→11-10
                return this.NULLを返す2(
                    this.Convertデータ型を合わせるNullableは想定する(this.ScalarExpression(x_Parameters[2]),typeof(DateTime?)),
                    this.Convertデータ型を合わせるNullableは想定する(this.ScalarExpression(x_Parameters[1]),typeof(DateTime?)),
                   (arg0,arg1) => e.Expression.Subtract(
                        e.Expression.Call(Method,arg0),
                        e.Expression.Call(Method,arg1)
                    )
                );
            }
            case "DATEPART":{
                Debug.Assert(x_Parameters.Count == 2);
                return this.NULLを返す1(
                    this.Convertデータ型を合わせるNullableは想定する(this.ScalarExpression(x_Parameters[1]),typeof(DateTime?)),
                    arg0=>{
                        var y = (ColumnReferenceExpression)x_Parameters[0];
                        var Method=y.MultiPartIdentifier.Identifiers[0].Value.ToUpperInvariant()switch{
                            "YEAR" or"YY"   or"YYYY"     =>Product.SQLServer.Reflection.DATEPART.year,
                            "QQ"   or"Q"    or"QUARTER"  =>Product.SQLServer.Reflection.DATEPART.quarter,
                            "MM"   or"M"    or"MONTH"    =>Product.SQLServer.Reflection.DATEPART.month,
                            "DY"   or"Y"    or"DAYOFYEAR"=>Product.SQLServer.Reflection.DATEPART.dayofyear,
                            "DD"   or"D"    or"DAY"      =>Product.SQLServer.Reflection.DATEPART.day,
                            "WK"   or"WW"   or"WEEK"     =>Product.SQLServer.Reflection.DATEPART.week,//その年の何週目か
                            "DW"   or"W"    or"WEEKDAY"  =>Product.SQLServer.Reflection.DATEPART.weekday,
                            "HH"   or"HOUR"              =>Product.SQLServer.Reflection.DATEPART.hour,
                            "MI"   or"N"    or"MINUTE"   =>Product.SQLServer.Reflection.DATEPART.minute,
                            "SS"   or"S"    or"SECOND"   =>Product.SQLServer.Reflection.DATEPART.second,
                            "MS"   or"MILLISECOND"       =>Product.SQLServer.Reflection.DATEPART.millisecond,
                            "MCS"  or"MICROSECOND"       =>Product.SQLServer.Reflection.DATEPART.microsecond,
                            "NS"   or"NANOSECOND"        =>Product.SQLServer.Reflection.DATEPART.nanosecond,
                            "ISOWK"or"ISOWW"or"ISO_WEEK" =>Product.SQLServer.Reflection.DATEPART.iso_week,
                            "TZ"   or"TZOFFSET"          =>Product.SQLServer.Reflection.DATEPART.tzoffset,
                            _=>throw new NotSupportedException(y.MultiPartIdentifier.Identifiers[0].Value)
                        };
                        return e.Expression.Call(Method,arg0);
                    });
                //return e.Expression.Call(interval(x_Parameters[0]),this.Convertデータ型を合わせるNullableは想定する(this.ScalarExpression(x_Parameters[1]),typeof(DateTimeOffset?)));
            }
            case "DATENAME":{
                Debug.Assert(x_Parameters.Count == 2);
                var y = (ColumnReferenceExpression)x_Parameters[0];
                var Method=y.MultiPartIdentifier.Identifiers[0].Value.ToUpperInvariant()switch{
                    "YEAR"or"YY"or"YYYY"     =>Product.SQLServer.Reflection.DATENAME.year,
                    "QQ"  or"Q" or"QUARTER"  =>Product.SQLServer.Reflection.DATENAME.quarter,
                    "MM"  or"M" or"MONTH"    =>Product.SQLServer.Reflection.DATENAME.month,
                    "DY"  or"Y" or"DAYOFYEAR"=>Product.SQLServer.Reflection.DATENAME.dayofyear,
                    "DD"  or"D" or"DAY"      =>Product.SQLServer.Reflection.DATENAME.day,
                    "WK"  or"WW"or"WEEK"     =>Product.SQLServer.Reflection.DATENAME.week,//その年の何週目か
                    "DW"  or"W" or"WEEKDAY"  =>Product.SQLServer.Reflection.DATENAME.weekday,
                    "HH"  or"HOUR"           =>Product.SQLServer.Reflection.DATENAME.hour,
                    "MI"  or"N" or"MINUTE"   =>Product.SQLServer.Reflection.DATENAME.minute,
                    "SS"  or"S" or"SECOND"   =>Product.SQLServer.Reflection.DATENAME.second,
                    "MS"  or"MILLISECOND"    =>Product.SQLServer.Reflection.DATENAME.millisecond,
                    "MCS" or"MICROSECOND"    =>Product.SQLServer.Reflection.DATENAME.microsecond,
                    "NS"  or"NANOSECOND"     =>Product.SQLServer.Reflection.DATENAME.nanosecond,
                    _=>throw new NotSupportedException(y.MultiPartIdentifier.Identifiers[0].Value)
                };
                //return e.Expression.Call(Method,this.Convertデータ型を合わせるNullableは想定する(this.ScalarExpression(x_Parameters[1]),typeof(DateTimeOffset?)));
                return this.NULLを返す1(
                    this.Convertデータ型を合わせるNullableは想定する(this.ScalarExpression(x_Parameters[1]),typeof(DateTime?)),
                    arg0=>e.Expression.Call(Method,arg0)
                );
            }
            //CAST および CONVERT(Transact - SQL)
            //case "cast":{
            //    return e.Expression.Call(Product.SQLServer.Reflection.cast,
            //        this.ScalarExpression(x_Parameters[0]),
            //        this.ScalarExpression(x_Parameters[1])
            //    );
            //}
            //case "convert":{
            //    return e.Expression.Call(Product.SQLServer.Reflection.cast,
            //        e.Expression.Constant(CommonLibrary.SQLのTypeからTypeに変換(this.SQL取得(x_Parameters[0]))),
            //        this.ScalarExpression(x_Parameters[1])
            //    );
            //}
            case "ERROR_NUMBER":{
                return Constant_0;
            }
            case "GROUPING":{
                Debug.Assert(x_Parameters.Count==1);
                //SELECT FirstName,PersonType,NameStyle,Title,grouping(NameStyle)
                //    FROM Person.Person a
                //GROUP BY ROLLUP(FirstName,PersonType,NameStyle,Title)
                //A,B,C,D
                //0,0,0,0 GROUPING(D)=0
                //0,0,0,  GROUPING(D)=1
                //0,0, ,  GROUPING(D)=1
                //0, , ,  GROUPING(D)=1
                return Constant_0;
                //return e.Expression.Call(
                //    this.ScalarExpression(x_Parameters[0]),
                //    this.ScalarExpression(x_Parameters[0]);
                //    Product.SQLServer.Reflection.Rank
                //);
            }
            //システム関数
            case "ISNULL":{
                var 変換元 = this.ScalarExpression(x_Parameters[0]);
                var 変換先 = this.ScalarExpression(x_Parameters[1]);
                this.判定指定Table.実行(変換元,this.RefPeek.List_TableExpression);
                e.Expression? Predicate0 = null;
                var 出力TableExpressions = this.出力TableExpressions;
                if(出力TableExpressions.Count > 0) {
                    var Predicate = e.Expression.NotEqual(出力TableExpressions[0],Constant_null);
                    var 出力TableExpressions_Count = 出力TableExpressions.Count;
                    for(var a = 1;a < 出力TableExpressions_Count;a++)
                        Predicate = e.Expression.AndAlso(Predicate,e.Expression.NotEqual(出力TableExpressions[a],Constant_null));
                    //変換元=e.Expression.Condition(Predicate,変換元,変換先);
                }

                //var (L1,R1)=this.Convertデータ型を合わせるNullableは想定する(L0,R0);
                //foreach(var a in 出力TableExpressions)
                //    変換元=e.Expression.Condition(e.Expression.NotEqual(a,Constant_null),変換元,Constant_null);
                //byte?a;int?b
                //if(a.HasValue)return a else b
                //int?a;byte?b
                //if(a.HasValue)return a else new Nullable<int>(b.GetValueOrDefault())
                //new Nullable<int>()
                var 変換元_Type = 変換元.Type;
                var 変換先_Type = 変換先.Type;
                if(変換元_Type == 変換先_Type) return 変換元;
                var 変換元_Type_IsNullable = 変換元_Type.IsNullable();
                var 変換先_Type_IsNullable = 変換先_Type.IsNullable();
                if(!変換元_Type.IsValueType) {
                    var Predicate1 = e.Expression.NotEqual(変換元,Constant_null);
                    Predicate0=Predicate0 is not null?e.Expression.AndAlso(Predicate0,Predicate1):Predicate1;
                    if(!変換先_Type.IsValueType) {
                        //object=(object)??(string)
                        (変換元, 変換先) = this.Convertデータ型を合わせるNullableは想定しない(変換元,変換先);
                        return e.Expression.Condition(Predicate0,変換元,変換先);
                    } else if(変換先_Type_IsNullable) {
                        //object=(object)??(int?)
                        var Element変換先 = GetValueOrDefault(変換先);
                        (変換元, Element変換先) = this.Convertデータ型を合わせるNullableは想定しない(変換元,Element変換先);
                        //変換元0=this.Convertデータ型を合わせるNullableは想定しない(Element変換元,Element変換先.Type);
                        return e.Expression.Condition(Predicate0,変換元,e.Expression.Condition(e.Expression.Property(変換先,"HasValue"),Element変換先,e.Expression.Default(変換先.Type)));
                    } else {
                        //object=(object)??(int)
                        return e.Expression.Condition(Predicate0,変換元,e.Expression.Convert(変換先,変換元_Type));
                    }
                } else if(変換元_Type_IsNullable) {
                    var Predicate1 = e.Expression.Property(変換元,"HasValue");
                    if(Predicate0 is not null) Predicate0 = e.Expression.AndAlso(Predicate0,Predicate1);
                    else Predicate0 = Predicate1;
                    if(!変換先_Type.IsValueType) {
                        //object=(int?)??(object)
                        var Element変換元 = GetValueOrDefault(変換元);
                        (Element変換元, 変換先) = this.Convertデータ型を合わせるNullableは想定しない(Element変換元,変換先);
                        return e.Expression.Condition(Predicate0,Element変換元,変換先);
                    } else if(変換先_Type_IsNullable) {
                        //double?=(int?)??(double?)
                        var Element変換元 = GetValueOrDefault(変換元);
                        var Element変換先 = GetValueOrDefault(変換先);
                        (Element変換元, Element変換先) = this.Convertデータ型を合わせるNullableは想定しない(Element変換元,Element変換先);
                        return e.Expression.Condition(Predicate0,Element変換元,Element変換先);
                    } else {
                        //double?=(int?)??(double)
                        var Element変換元 = GetValueOrDefault(変換元);
                        (Element変換元, 変換先) = this.Convertデータ型を合わせるNullableは想定しない(Element変換元,変換先);
                        //     var Element変換元0=this.Convertデータ型を合わせるNullableは想定しない(Element変換元,変換先0.Type);
                        return e.Expression.Condition(Predicate0,Element変換元,変換先);
                    }
                } else{
                    //decimal=(decimal)??(int)
                    return 変換元;
                }
                //throw new NotSupportedException($"ISNULL({変換元_Type.FullName},{変換先_Type.FullName})は出来ない");
                //return e.Expression.Coalesce(変換元,変換先);
            }
            //case "rank":{
            //    var x_Parameters=x_Parameters;
            //    Debug.Assert(x_Parameters.Count==0);
            //    return Expressions.Expression.Call(
            //        this.ScalarExpression(x_Parameters[0]),
            //        Product.SQLServer.Reflection.Rank
            //    );
            //}
            //rank
            //e.Expression InstanceMethod1(MethodInfo Method){
            //    var Parameters=Method.GetParameters();
            //    return this.NULLUnary(
            //        this.ScalarExpression(x_Parameters[0]),
            //       (arg1)=>e.Expression.Call(arg1,Method)
            //    );
            //}
            //OPEN XML
            case "VALUE":{
                Debug.Assert(x_Parameters.Count == 2);
                //'nvarchar'のようにリテラルなのでシングルクォートなので削除するべき
                var DBType = this.SQL取得(x_Parameters[1])[1..^1].ToUpperInvariant();
                //Typeに合うようにExpressionを変形する()
                var value=DBType switch{
                    "BIT"             =>Product.SQLServer.Reflection.value_Boolean,
                    "TINYINT"         =>Product.SQLServer.Reflection.value_SByte,
                    "SMALLINT"        =>Product.SQLServer.Reflection.value_Int16,
                    "INTEGER"or"INT"  =>Product.SQLServer.Reflection.value_Int32,
                    "BIGINT"          =>Product.SQLServer.Reflection.value_Int64,
                    "REAL"            =>Product.SQLServer.Reflection.value_Single,
                    "FLOAT"           =>Product.SQLServer.Reflection.value_Double,
                    "MONEY"or"DECIMAL"=>Product.SQLServer.Reflection.value_Decimal,
                    "DATETIME"        =>Product.SQLServer.Reflection.value_DateTime,
                    "XML"             =>Product.SQLServer.Reflection.value_XElement,
                    "UNIQUEIDENTIFIER"=>Product.SQLServer.Reflection.value_Guid,
                    _=>DBType.Length>=4
                        ?DBType[..4]is"CHAR"or"TEXT"
                            ?Product.SQLServer.Reflection.value_String
                            :DBType.Length>=5
                                ?DBType[..5]is"NCHAR"or"NTEXT"
                                    ?Product.SQLServer.Reflection.value_String
                                    :DBType.Length>=7
                                        ?DBType[..7]is"DECIMAL"
                                            ?Product.SQLServer.Reflection.value_Decimal
                                            :DBType[..7]is"VARCHAR"or"SYSNAME"
                                                ?Product.SQLServer.Reflection.value_String
                                                :DBType.Length>=8
                                                    ?DBType[..8]is"NVARCHAR"
                                                        ?Product.SQLServer.Reflection.value_String
                                                        :throw new NotSupportedException(DBType)
                                                    :throw new NotSupportedException(DBType)
                                        :throw new NotSupportedException(DBType)
                                :throw new NotSupportedException(DBType)
                        :throw new NotSupportedException(DBType)
                };
                //switch(DBType) {
                //    case "bit":value = Product.SQLServer.Reflection.value_Boolean;break;
                //    case "tinyint":value = Product.SQLServer.Reflection.value_SByte;break;
                //    case "smallint":value = Product.SQLServer.Reflection.value_Int16;break;
                //    case "integer":
                //    case "int":value = Product.SQLServer.Reflection.value_Int32;break;
                //    case "bigint":value = Product.SQLServer.Reflection.value_Int64;break;
                //    //case //"hierarchyid":
                //    case "real":value = Product.SQLServer.Reflection.value_Single;break;
                //    case "float":value = Product.SQLServer.Reflection.value_Double;break;
                //    case "money":
                //    case "decimal":value = Product.SQLServer.Reflection.value_Decimal;break;
                //    //"numeric" :value=SQLServer.Reflection.value_Decimal;break;
                //    //"smallmoney" :value=SQLServer.Reflection.value_smallmoney;break;
                //    //"money" :value=SQLServer.Reflection.value_money;break;
                //    //"char":
                //    //"varchar":
                //    //"nchar":
                //    //"nvarchar":
                //    //"text":
                //    //"ntext":
                //    //"sysname":
                //    //    typeof(String);break;
                //    //"date" :value=SQLServer.Reflection.value_DateTime
                //    case "datetime":value = Product.SQLServer.Reflection.value_DateTime;break;
                //    //"datetime2" :value=SQLServer.Reflection.value_DateTime;break;
                //    //"smalldatetime" :value=SQLServer.Reflection.value_DateTime;break;
                //    //"datetimeoffset" :value=SQLServer.Reflection.value_DateTime;break;
                //    //"binary" :value=SQLServer.Reflection.value_Bytes;break;
                //    //"varbinary" :value=SQLServer.Reflection.value_money;break;
                //    //"geography" :value=SQLServer.Reflection.value_money;break;
                //    //"geometry" :value=SQLServer.Reflection.value_money;break;
                //    //"image" :value=SQLServer.Reflection.value_money;break;
                //    //"sql_variant" :value=SQLServer.Reflection.value_money;break;
                //    case "xml":value = Product.SQLServer.Reflection.value_XElement;break;
                //    case "uniqueidentifier":value = Product.SQLServer.Reflection.value_Guid;break;
                //    default:
                //    if(DBType.Length >= 4)
                //        if(DBType[..4] == "char" || DBType[..4] == "text")
                //            value = Product.SQLServer.Reflection.value_String;
                //        else if(DBType.Length >= 5)
                //            if(DBType[..5] == "nchar" || DBType[..5] == "ntext")
                //                value = Product.SQLServer.Reflection.value_String;
                //            else if(DBType.Length >= 7)
                //                if(DBType[..7] == "decimal")
                //                    value = Product.SQLServer.Reflection.value_Decimal;
                //                else if(DBType[..7] == "varchar" || DBType[..7] == "sysname")
                //                    value = Product.SQLServer.Reflection.value_String;
                //                else if(DBType.Length >= 8)
                //                    if(DBType[..8] == "nvarchar")
                //                        value = Product.SQLServer.Reflection.value_String;
                //                    else throw new NotSupportedException(DBType);
                //                else throw new NotSupportedException(DBType);
                //            else throw new NotSupportedException(DBType);
                //        else throw new NotSupportedException(DBType);
                //    else throw new NotSupportedException(DBType);
                //    break;
                //    //"time" :value=SQLServer.Reflection.value_money;break;
                //    //"hierarchyid" :value=SQLServer.Reflection.value_money;break;
                //}
                //    _=>
                //        if(DBType[..8]=="nvarchar"||dbtype[..7]=="varchar"||dbtype[..5]=="nchar"||dbtype[..4]=="char"||dbtype[..4]=="text"||dbtype[..5]=="ntext"||dbtype[..7]=="sysname"){
                //            typeof(String),
                //        }
                //        throw new NotSupportedException(DBType),
                //}
                var f0 = this.CallTarget(x.CallTarget);
                var f1 = this.ScalarExpression(x_Parameters[0]);
                return e.Expression.Call(
                    value,
                    this.CallTarget(x.CallTarget),
                    this.ScalarExpression(x_Parameters[0])
                );
            }
            //引数をメソッド情報から適切にキャスト
            default:{
                var 名前一致Methods = typeof(Product.SQLServer.Methods).GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly).Where(p => string.Equals(p.Name,FunctionName,StringComparison.OrdinalIgnoreCase)).ToList();
                if(名前一致Methods.Count==0) break;
                //1,名前が一致して1つだけならそれ
                //特殊化すべき関数
                //convert(intなど特殊な予約語
                var x_Parameters_Count = x_Parameters.Count;
                var ScalarExpressions = new e.Expression[x_Parameters_Count];
                for(var a = 0;a < x_Parameters_Count;a++) {
                    if("convert" == FunctionName) {
                        if(a == 0) {
                            ScalarExpressions[a] = e.Expression.Constant(CommonLibrary.SQLのTypeからTypeに変換(this.SQL取得(x_Parameters[0])));
                            continue;
                        }
                    }
                    ScalarExpressions[a] = this.ScalarExpression(x_Parameters[a]);
                }
                //メソッドが1つだけの場合
                e.MethodCallExpression MethodCall=null!;
                if(共通default(名前一致Methods,ref MethodCall))return MethodCall;
                //省略なくぴったり引数の数が一致しているもの
                if(共通default(名前一致Methods.Where(Method =>ScalarExpressions.Length==Method.GetParameters().Length),ref MethodCall))return MethodCall;
                //f(int a,int b=3,params int[]c)
                //1,引数の数が一致         f(1 , 2,3 )
                //2,引数の型が一致         f(1d,2d,3d)
                //ぴったり一致する型(int,double),(int,int,double=1.0)に対して(int,int)は右で解決
                if(
                    共通default(
                        名前一致Methods.Where(
                            Method => {
                                var Method_Parameters = Method.GetParameters();
                                var Method_Parameters_Length = Method_Parameters.Length;
                                var ScalarExpressions_Length = ScalarExpressions.Length;
                                for(var a="convert" == FunctionName?1:0;a< ScalarExpressions_Length;a++) {
                                    var Method_Parameter = Method_Parameters[a];
                                    var ScalarExpression_Type = ScalarExpressions[a].Type;
                                    if(Method_Parameter.ParameterType!=ScalarExpression_Type) return false;
                                }
                                return true;
                            }
                        ),ref MethodCall
                    )
                )return MethodCall;
                //3,ジェネリックな型。推論は難しいので今は思いつかない
                //4,親クラス多段に派生している場合、近い型ほど優先 インターフェースも同様だがインターフェースと親クラスの両方があった場合解決できない
                //近い型は難しいので祖先であるかどうかだけ見る方法にしている
                if(
                    共通default(
                        名前一致Methods.Where(
                            Method =>{
                                var Method_Parameters = Method.GetParameters();
                                var Method_Parameters_Length = Method_Parameters.Length;
                                var ScalarExpressions_Length = ScalarExpressions.Length;
                                for(var a="convert" == FunctionName?1:0;a< ScalarExpressions_Length;a++) {
                                    var Method_Parameter = Method_Parameters[a];
                                    var ScalarExpression_Type = ScalarExpressions[a].Type;
                                    if(!Method_Parameter.ParameterType.IsAssignableFrom(ScalarExpression_Type)) return false;
                                }
                                return true;
                            }
                        ),ref MethodCall
                    )
                )return MethodCall;
                //暗黙的に変換できる型
                if(
                    共通default(
                        名前一致Methods.Where(
                            Method =>{
                                var Method_Parameters = Method.GetParameters();
                                var Method_Parameters_Length = Method_Parameters.Length;
                                var ScalarExpressions_Length = ScalarExpressions.Length;
                                for(var a="convert" == FunctionName?1:0;a< ScalarExpressions_Length;a++) {
                                    var Method_Parameter = Method_Parameters[a];
                                    var ScalarExpression_Type = ScalarExpressions[a].Type;
                                    if(!Method_Parameter.ParameterType.IsAssignableFrom(ScalarExpression_Type)){
                                        if(this.作業配列.GetMethod(Method_Parameter.ParameterType,"op_Implicit",ScalarExpression_Type)is not null){
                                        }else if(this.作業配列.GetMethod(ScalarExpression_Type,"op_Implicit",Method_Parameter.ParameterType)is not null){
                                        }else return false;
                                    }
                                }
                                return true;
                            }
                        ),ref MethodCall
                    )
                )return MethodCall;
                //その型が実装しているインターフェイス
                //ユーザー定義の型変換がある場合
                //object 
                //4,引数の代入可能型が一致
                //var 引数の数が同じMethods = NameMethods;
                //2,引数の数が完全一致、オプション引数、可変長引数を考慮してもあり得るメソッド群
                var 名前一致_既定値_可変長引数BaseMethods = 名前一致Methods.Where(
                    Method => {
                        if(Method.Name=="concat"){

                        }
                        var Method_Parameters = Method.GetParameters();
                        var Method_Parameters_Length = Method_Parameters.Length;
                        var ScalarExpressions_Length = ScalarExpressions.Length;
                        //パラメータ数が一致したら一次審査は通る
                        //(int,int=3)に(int)
                        //if(Method_Parameters_Length==0&&ScalarExpressions_Length>0) return false;
                        if(ScalarExpressions_Length<Method_Parameters_Length){
                            //(int=0,params[]int)または
                            //(int=0,int=0)メソッドを
                            //()呼び出し
                            for(var a = ScalarExpressions_Length;a<Method_Parameters_Length-1;a++) {
                                var Method_Parameter = Method_Parameters[a];
                                if(!Method_Parameter.IsDefined(typeof(System.Runtime.InteropServices.OptionalAttribute))) return false;
                            }
                            {
                                var Method_Parameter = Method_Parameters[Method_Parameters_Length-1];
                                if(!(Method_Parameter.IsDefined(typeof(ParamArrayAttribute))&&Method_Parameter.IsDefined(typeof(System.Runtime.InteropServices.OptionalAttribute)))) return false;
                            }
                        } else{
                            //(params[]int)メソッドを
                            //(1,2)呼び出し
                            var Method_Parameter = Method_Parameters[Method_Parameters_Length-1];
                            if(!Method_Parameter.IsDefined(typeof(ParamArrayAttribute))) return false;
                        }
                        return true;
                        //if(ScalarExpressions_Length==Method_Parameters_Length-1) {
                        //    //(params int[])Method_Parameters_Length==3
                        //    //()は一致する
                        //    var Method_Parameter = Method_Parameters[ScalarExpressions_Length];
                        //    Debug.Assert(Attribute.IsDefined(Method_Parameter,typeof(ParamArrayAttribute))==Method_Parameter.IsDefined(typeof(ParamArrayAttribute)));
                        //    Debug.Assert(Attribute.IsDefined(Method_Parameter,typeof(System.Runtime.InteropServices.OptionalAttribute))==Method_Parameter.IsDefined(typeof(System.Runtime.InteropServices.OptionalAttribute)));
                        //    return Attribute.IsDefined(Method_Parameter,typeof(ParamArrayAttribute))||Attribute.IsDefined(Method_Parameter,typeof(System.Runtime.InteropServices.OptionalAttribute));
                        //} else {
                        //    //(params int[])Method_Parameters_Length==3
                        //    //(1,2,3)は一致する
                        //    for(var a = Method_Parameters_Length;a<ScalarExpressions_Length;a++) {
                        //        var Method_Parameter = Method_Parameters[a];
                        //        if(!Method_Parameter.IsDefined(typeof(ParamArrayAttribute))) return false;
                        //        if(!Method_Parameter.IsDefined(typeof(System.Runtime.InteropServices.OptionalAttribute))) return false;
                        //    }
                        //    return true;
                        //}
                    }
                ).ToList();
                if(FunctionName=="concat"){

                }
                if(
                    共通default(
                        名前一致_既定値_可変長引数BaseMethods.Where(
                            Method => {
                                var Method_Parameters = Method.GetParameters();
                                var Method_Parameters_Length = Method_Parameters.Length;
                                var ScalarExpressions_Length = ScalarExpressions.Length;
                                for(var a="CONVERT" == FunctionName?1:0;a< ScalarExpressions_Length;a++) {
                                    var Method_Parameter = Method_Parameters[a];
                                    var ScalarExpression_Type = ScalarExpressions[a].Type;
                                    if(Method_Parameter.ParameterType!=ScalarExpression_Type) return false;
                                }
                                return true;
                            }
                        ),ref MethodCall
                    )
                )return MethodCall;

                if(
                    共通default(
                        名前一致Methods.Where(
                            Method => {
                                var Method_Parameters = Method.GetParameters();
                                var Method_Parameters_Length = Method_Parameters.Length;
                                var ScalarExpressions_Length = ScalarExpressions.Length;
                                //パラメータ数が一致したら一次審査は通る
                                //(int,int=3)に(int)
                                if(Method_Parameters_Length==0&&ScalarExpressions_Length>0) return false;
                                for(var a="CONVERT" == FunctionName?1:0;a< ScalarExpressions_Length;a++) {
                                    var Method_Parameter = Method_Parameters[a];
                                    var ScalarExpression_Type = this.Convertデータ型を合わせるNullableは想定する(ScalarExpressions[a],Method_Parameter.ParameterType).Type;
                                    if(Method_Parameter.ParameterType!=ScalarExpression_Type) return false;
                                }
                                return true;
                            }
                        ),ref MethodCall
                    )
                ) return MethodCall;
                break;
                bool 共通default(System.Collections.Generic.IEnumerable<MethodInfo> Methods,ref e.MethodCallExpression MethodCall) {
                    var List_Method=Methods.ToList();
                    if(List_Method.Count!=1) return false;
                    var Method= List_Method[0];
                    var Method_Parameters = Method.GetParameters();
                    var Arguments_Length = Method_Parameters.Length;
                    switch(Arguments_Length) {
                        case 0:MethodCall = e.Expression.Call(Method); return true;
                        case 1:MethodCall = e.Expression.Call(Method,@params(0)); return true;
                        case 2:MethodCall = e.Expression.Call(Method,@params(0),@params(1)); return true;
                        case 3:MethodCall = e.Expression.Call(Method,@params(0),@params(1),@params(2)); return true;
                        case 4:MethodCall = e.Expression.Call(Method,@params(0),@params(1),@params(2),@params(3)); return true;
                        default:{
                            var Arguments = new e.Expression[Arguments_Length];
                            for(var a = 0;a < Arguments_Length;a++) Arguments[a] = @params(a);
                            MethodCall = e.Expression.Call(Method,Arguments);
                            return true;
                        }
                    }
                    e.Expression @params(int index) {
                        if(index < ScalarExpressions.Length) {
                            var Method_Parameter = Method_Parameters[index];
                            //メソッド定義の引数より実際の引数が多かった場合、可変長引数だったらそのように処理する
                            if(Method_Parameter.IsDefined(typeof(Product.SQLServer.TypeAttribute))) {
                                return ScalarExpressions[index];
                            } else if(Method_Parameter.IsDefined(typeof(ParamArrayAttribute))) {
                                //func(params int[]c)
                                var params_Length = x_Parameters.Count - 1;
                                var param = new e.Expression[params_Length];
                                var ElementType = Method_Parameter.ParameterType.GetElementType();
                                for(var a = 0;a < params_Length;a++)
                                    param[a] = this.Convertデータ型を合わせるNullableは想定する(ScalarExpressions[index + a],ElementType);
                                return e.Expression.NewArrayInit(ElementType,param);
                            } else return this.Convertデータ型を合わせるNullableは想定する(ScalarExpressions[index],Method_Parameter.ParameterType);
                        } else if(index < Method_Parameters.Length) {
                            //メソッド定義の引数より実際の引数が少なかった場合、オプション引数だったらそのように処理する
                            //func(int a,int b=3,params int[]c)
                            var Method_Parameter = Method_Parameters[index];
                            if(Method_Parameter.IsDefined(typeof(System.Runtime.InteropServices.OptionalAttribute)))
                                return e.Expression.Constant(Method_Parameter.DefaultValue,Method_Parameters[index].ParameterType);
                            if(Method_Parameter.IsDefined(typeof(ParamArrayAttribute))) {
                                var params_Length = x_Parameters.Count - 1;
                                var param = new e.Expression[params_Length];
                                var ElementType = Method_Parameter.ParameterType.GetElementType();
                                for(var a = 0;a < params_Length;a++)
                                    param[a] = this.Convertデータ型を合わせるNullableは想定する(ScalarExpressions[index + a],ElementType);
                                return e.Expression.NewArrayInit(ElementType,param);
                            }
                            return this.Convertデータ型を合わせるNullableは想定する(ScalarExpressions[index],Method_Parameter.ParameterType);
                        } else {
                            //func(int a,int b=3,params int[]c)
                            var Method_Parameter = Method_Parameters[Method_Parameters.Length-1];
                            if(Attribute.IsDefined(Method_Parameter,typeof(ParamArrayAttribute))) {
                                var params_Length = x_Parameters.Count - 1;
                                var param = new e.Expression[params_Length];
                                var ElementType = Method_Parameter.ParameterType.GetElementType();
                                for(var a = 0;a < params_Length;a++)
                                    param[a] = this.Convertデータ型を合わせるNullableは想定する(ScalarExpressions[index + a],ElementType);
                                return e.Expression.NewArrayInit(ElementType,param);
                            } else throw new NotSupportedException("引数が少なかった。仮引数にはデフォルトパラメーターがなかった。parms可変長引数0を指定したわけでもない。");
                        }
                    }
                }
            }
        }
        if(x.CallTarget is not null) {
            var Schema = this.CallTarget(x.CallTarget);
            //e.Expression Instance;
            if(Schema.Type.IsNullable()){
                //Instance=e.Expression.Call(
                //    Schema,
                //    Schema.Type.GetMethod(nameof(Nullable<int>.GetValueOrDefault))!
                //);
                //var Method=Instance.Type.GetMethod(FunctionName,BindingFlags.Instance|BindingFlags.Public|BindingFlags.DeclaredOnly|BindingFlags.IgnoreCase);
                //Debug.Assert(Method!=null);
                //var Parameters=Method.GetParameters();
                //var arguments_Length=x_Parameters.Count;
                //var arguments=new e.Expression[arguments_Length];
                //for(var a=0;a<arguments_Length;a++) arguments[a]=this.Convertデータ型を合わせるNullableは想定する(this.ScalarExpression(x_Parameters[a]),Parameters[a].ParameterType);
                var Schema_Type=Schema.Type;
                e.Expression ifTrue=共通CallTarget(
                    e.Expression.Call(
                        Schema,
                        Schema_Type.GetMethod(nameof(Nullable<int>.GetValueOrDefault),Type.EmptyTypes)!
                    )
                );
                var test=e.Expression.Call(
                    Schema,
                    Schema_Type.GetProperty(nameof(Nullable<int>.HasValue))!.GetMethod
                );
                if(ifTrue.Type.IsNullable()){
                    //ifTrue=e.Expression.Convert(
                    //    ifTrue,
                    //    Schema_Type
                    //);
                }else if(ifTrue.Type.IsValueType){
                    ifTrue=e.Expression.Convert(
                        ifTrue,
                        Schema_Type
                    );
                }
                return e.Expression.Condition(
                    test,
                    ifTrue,
                    e.Expression.Default(ifTrue.Type)
                );
            } else{
                //Instance=Schema;
                //var Method=Instance.Type.GetMethod(FunctionName,BindingFlags.Instance|BindingFlags.Public|BindingFlags.DeclaredOnly|BindingFlags.IgnoreCase);
                //Debug.Assert(Method!=null);
                //var Parameters=Method.GetParameters();
                //var arguments_Length=x_Parameters.Count;
                //var arguments=new e.Expression[arguments_Length];
                //for(var a=0;a<arguments_Length;a++) arguments[a]=this.Convertデータ型を合わせるNullableは想定する(this.ScalarExpression(x_Parameters[a]),Parameters[a].ParameterType);
                return 共通CallTarget(Schema);
            }
            e.MethodCallExpression 共通CallTarget(e.Expression Instance){
                var Method=Instance.Type.GetMethod(FunctionName,BindingFlags.Instance|BindingFlags.Public|BindingFlags.DeclaredOnly|BindingFlags.IgnoreCase)!;
                var Parameters=Method.GetParameters();
                var arguments_Length=x_Parameters.Count;
                var arguments=new e.Expression[arguments_Length];
                for(var a=0;a<arguments_Length;a++) arguments[a]=this.Convertデータ型を合わせるNullableは想定する(this.ScalarExpression(x_Parameters[a]),Parameters[a].ParameterType);
                return e.Expression.Call(Instance,Method,arguments);
            }
        }
        var AggregateFunction=x_Parameters.Count switch{
            0=>this.AggregateFunction(FunctionName),
            1=>this.AggregateFunction(FunctionName,x_Parameters[0]),
            _=>null
        };
        if(AggregateFunction is not null)return AggregateFunction;
        throw new NotSupportedException($"{FunctionName}関数が定義されていなかった");
        //Barから派生したクラスのインスタンスで引数なしで動的メソッドを呼び出します。DynamicObject
        //dynamic dynamicObject = new DerivedFromDynamicObject();
        //var callSiteBinder = Binder.InvokeMember(CSharpBinderFlags.None,"Bar",Enumerable.Empty<Type>(),typeof(Program),
        //    new[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null) });
        //var callSite = CallSite<Action<CallSite,object>>.Create(callSiteBinder);
        //callSite.Target(callSite,dynamicObject);

        //{ 
        //    var Functions=this.Functions;
        //    var x_Parameters_Count= x_Parameters.Count;
        //    //var Arguments_Length = x_Parameters.Count;
        //    var Types=new Type[x_Parameters_Count];
        //    var Arguments = new e.Expression[x_Parameters_Count+2];
        //    Arguments[0]=Argument;
        //    var CSharpArgumentInfos = new RuntimeBinder.CSharpArgumentInfo[x_Parameters_Count+1];
        //    for(var a=0;a<x_Parameters_Count;a++){
        //        var Argument=this.ScalarExpression(x_Parameters[a]);
        //        Arguments[a+1]=Argument;
        //        Types[a]=Argument.Type;
        //        CSharpArgumentInfos[a]=CSharpArgumentInfo;
        //    }
        //    CSharpArgumentInfos[x_Parameters_Count]=CSharpArgumentInfo;
        //    var sb=new StringBuilder();
        //    foreach(var Type in Types) {
        //        sb.Append(Type.Name+',');
        //    }
        //    if(!Functions.ContainsKey(FunctionName))Functions.Add(FunctionName,sb.ToString());
        //    var InvokeMember = RuntimeBinder.Binder.InvokeMember(RuntimeBinder.CSharpBinderFlags.None,FunctionName,null,typeof(変換_TSqlFragmentからExpression),CSharpArgumentInfos);
        //    return e.Expression.Dynamic(InvokeMember,typeof(object),Arguments);
        //}
    }
    private e.Expression IIfCall(IIfCall x){throw this.単純NotSupportedException(x);}
    private e.Expression LeftFunctionCall(LeftFunctionCall x)=>this.LeftRightFunctionCall(x.Parameters,Product.SQLServer.Reflection.left);
    private e.Expression RightFunctionCall(RightFunctionCall x)=>this.LeftRightFunctionCall(x.Parameters,Product.SQLServer.Reflection.right);
    private e.Expression LeftRightFunctionCall(IList<ScalarExpression>Parameters,MethodInfo Method)=>e.Expression.Call(
        Method,
        this.ConvertNullable(this.ScalarExpression(Parameters[0])),
        this.ConvertNullable(this.ScalarExpression(Parameters[1]))
    );
    private static string Schema取得(SchemaObjectName x){
        if(x.SchemaIdentifier is null) 
            return "dbo";
        else 
            return x.SchemaIdentifier.Value;
    }
    private e.Expression NextValueForExpression(NextValueForExpression x){
        var ContainerType=this.ContainerType;
        var x_SchemaObjectName=x.SequenceName;
        var Schema=x_SchemaObjectName.Name取得();
        //var Schema=x_Name.SchemaIdentifier is null ? "dbo" orx_Name.SchemaIdentifier.Value;
        var Schema_FulllName=this.ContainerType.Namespace+".Schemas."+Schema;
        var Schema_Type=ContainerType.Assembly.GetType(Schema_FulllName.Replace("*",@"\*"),true,true);
        Debug.Assert(Schema_Type!=null);
        var x_SchemaObjectName_BaseIdentifier_Value=x_SchemaObjectName.BaseIdentifier.Value;
        //var View = Schema_Type.GetProperty(x_SchemaObjectName_BaseIdentifier_Value,BindingFlags.Public|BindingFlags.Instance);
        var Sequence=Schema_Type.GetProperties(BindingFlags.Public|BindingFlags.Instance).Single(p => string.Equals(p.Name,x_SchemaObjectName_BaseIdentifier_Value,StringComparison.OrdinalIgnoreCase));
        var NextValue=Sequence.PropertyType.GetMethod(nameof(Sequence<int>.NextValue));
        return e.Expression.Call(
            e.Expression.Property(
                e.Expression.Property(
                    this.Container,
                    Schema
                ),
                Sequence
            ),
            NextValue
        );
    }
    /// <summary>
    /// TSqlFragment.ScalarExpression.PrimaryExpression
    /// nullif nullだった場合どの値を返すか
    /// nullif(aatm.task_agent_data.value('(/*/retentionPeriod)[1]','int'),0)
    /// a.HasValue?a.GetValueOrDefault():default
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    /// <example>nullif(NULL,1)</example>
    private e.Expression NullIfExpression(NullIfExpression x){
        var FirstExpression=this.ScalarExpression(x.FirstExpression);
        e.Expression test;
        if(FirstExpression.Type.IsNullable()){
            test=e.Expression.Property(FirstExpression,nameof(Nullable<int>.HasValue));
            FirstExpression=e.Expression.Call(FirstExpression,FirstExpression.Type.GetMethod(nameof(Nullable<int>.GetValueOrDefault),Type.EmptyTypes));
        }else{
            test=e.Expression.Equal(FirstExpression,Constant_null);
        }
        return e.Expression.Condition(
            test,
            FirstExpression,
            this.ScalarExpression(x.SecondExpression)
        );
    }
    private e.Expression OdbcFunctionCall(OdbcFunctionCall x){throw this.単純NotSupportedException(x);}
    /// <summary>
    /// curent_userなど引数のない関数。()がないからプロパティみたいなもん
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression ParameterlessCall(ParameterlessCall x)=>x.ParameterlessCallType switch{
        ParameterlessCallType.CurrentTimestamp=>Constant_0,
        ParameterlessCallType.CurrentUser=>e.Expression.Constant("dbox"),
        ParameterlessCallType.User=>Constant_0,
        ParameterlessCallType.SessionUser=>Constant_0,
        ParameterlessCallType.SystemUser=>Constant_0,
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression ParenthesisExpression(ParenthesisExpression x){
        var y=this.ScalarExpression(x.Expression);
        return y;
    }
    private e.Expression ParseCall(ParseCall x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression TryCastCall(TryCastCall x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression TryParseCall(TryParseCall x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression PartitionFunctionCall(PartitionFunctionCall x){throw this.単純NotSupportedException(x);}
    private e.Expression ScalarSubquery(ScalarSubquery x){
        var Subquery=this.Subquery(x);
        var Subquery_Type=Subquery.Type;
        var GenericArguments=Subquery_Type.GetGenericArguments();
        var T=GenericArguments[0];
        if(T.IsValueType&&!T.IsNullable()) {
            GenericArguments[0]=typeof(Nullable<>).MakeGenericType(GenericArguments);
            Subquery=e.Expression.Call(
                Reflection.ExtensionSet.Cast.MakeGenericMethod(GenericArguments),
                Subquery
            );
        }
        return e.Expression.Call(
            Reflection.ExtensionSet.SingleOrDefault.MakeGenericMethod(GenericArguments),
            Subquery
        );
    }
    private e.Expression TryConvertCall(TryConvertCall x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression UserDefinedTypePropertyAccess(UserDefinedTypePropertyAccess x){throw this.単純NotSupportedException(x);}
    /// <summary>
    /// PrimaryExpression:ScalarExpression:TSqlFragment
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private e.Expression ValueExpression(ValueExpression x)=>x switch{
        Literal y=>this.Literal(y),
        VariableReference y=>this.VariableReference(y),
        GlobalVariableExpression y=>this.GlobalVariableExpression(y),
        _=>throw this.単純NotSupportedException(x)
    };

    
    
    
    
    private e.Expression ExtractFromExpression(ExtractFromExpression x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression OdbcConvertSpecification(OdbcConvertSpecification x){throw this.単純NotSupportedException(x);}
    private e.Expression BinaryExpression(BinaryExpression x){
        //const Int64 DayTicks=937840050000;
        var Left=this.ScalarExpression(x.FirstExpression);
        var Right=this.ScalarExpression(x.SecondExpression);
        if(Right.Type.IsArray) {

        }
        return x.BinaryExpressionType switch{
            BinaryExpressionType.Add       =>共通(e.ExpressionType.Add),
            BinaryExpressionType.Subtract  =>共通(e.ExpressionType.Subtract),
            BinaryExpressionType.Multiply  =>共通(e.ExpressionType.Multiply),
            BinaryExpressionType.Divide    =>共通(e.ExpressionType.Divide),
            BinaryExpressionType.Modulo    =>共通(e.ExpressionType.Modulo),
            BinaryExpressionType.BitwiseAnd=>共通(e.ExpressionType.And),
            BinaryExpressionType.BitwiseOr =>共通(e.ExpressionType.Or),
            BinaryExpressionType.BitwiseXor=>共通(e.ExpressionType.ExclusiveOr),
            _=>throw this.単純NotSupportedException(x)
        };
        //    BinaryExpressionType.Add=>Expressions.Expression.Add(Left,Right),
        //    BinaryExpressionType.Subtract=>Expressions.Expression.Subtract(Left,Right),
        //    BinaryExpressionType.Multiply=>Expressions.Expression.Multiply(Left,Right),
        //    BinaryExpressionType.Divide=>Expressions.Expression.Divide(Left,Right),
        //    BinaryExpressionType.Modulo=>Expressions.Expression.Modulo(Left,Right),
        //    BinaryExpressionType.BitwiseAnd=>Expressions.Expression.Add(Left,Right),
        //    BinaryExpressionType.BitwiseOr=>Expressions.Expression.Or(Left,Right),
        //    BinaryExpressionType.BitwiseXor=>Expressions.Expression.ExclusiveOr(Left,Right),
        //    _=>throw this.単純NotSupportedException(x)
        //};
        e.Expression 共通(e.ExpressionType NodeType)=>this.NULLを返す2(
            Left,Right,
           (Left0,Right0)=>{
               var(Left1,Right1)=this.Convertデータ型を合わせるNullableは想定しない(Left0,Right0);
               if(NodeType==e.ExpressionType.Add&&Left1.Type==typeof(string))
                   return e.Expression.Add(Left1,Right1,Reflection.String.Concat_str0_str1);
               if(Left1.Type==typeof(byte[])) return e.Expression.MakeBinary(NodeType,Left1,Right1,false,Product.SQLServer.Reflection.Internal.Add);
               return e.Expression.MakeBinary(NodeType,Left1,Right1);
           }
        );
    }
    private e.Expression IdentityFunctionCall(IdentityFunctionCall x){throw this.単純NotSupportedException(x);}
    //TSqlFragment.ScalarExpression
    private e.Expression UnaryExpression(UnaryExpression x)=>x.UnaryExpressionType switch{
        UnaryExpressionType.Positive=>this.ScalarExpression(x.Expression),
        UnaryExpressionType.Negative=>e.Expression.Negate(this.ScalarExpression(x.Expression)),
        UnaryExpressionType.BitwiseNot=>e.Expression.OnesComplement(this.ScalarExpression(x.Expression)),
        _=>throw new ArgumentOutOfRangeException()
    };
    //TSqlFragment.ScalarExpression
    private e.Expression ScalarExpressionSnippet(ScalarExpressionSnippet x){throw this.単純NotSupportedException(x);}
    //TSqlFragment.ScalarExpression
    private e.Expression SourceDeclaration(SourceDeclaration x){throw this.単純NotSupportedException(x);}
}
