using System.Globalization;
using System.Reflection;

namespace LinqDB.Reflection;

using static Common;
internal static class DateTime{
    public static readonly MethodInfo FromBinary= M(() => System.DateTime.FromBinary(0));
    public static readonly ConstructorInfo ctor_YearMonthDay = typeof(System.DateTime).GetConstructor(new[] { typeof(int),typeof(int),typeof(int) })!;
    //public static readonly ConstructorInfo ctor_Int64 = typeof(global::System.DateTime).GetConstructor(new[] { typeof(long)})!;
    //public static readonly MethodInfo New_Int64_TimeSpan = M(() =>Private_New_Int64_TimeSpan(0,new global::System.TimeSpan()));
    //private static global::System.DateTime Private_New_Int64_TimeSpan(long Ticks,global::System.TimeSpan TimeSpan)=>new(Ticks,DateTimeKind.Utc);
    public static readonly MethodInfo TryGetValue2 = typeof(System.DateTime).GetMethod(nameof(System.DateTime.TryParse),new[] { typeof(string),typeof(System.DateTime).MakeByRefType() })!;
    //public static readonly MethodInfo Parse1 = typeof(global::System.DateTime).GetMethod(nameof(global::System.DateTime.Parse),BindingFlags.Static|BindingFlags.Public,null,new[] { typeof(String) },null);
#pragma warning disable CA1305 // Specify IFormatProvider
    public static readonly MethodInfo Parse_input = M(() => System.DateTime.Parse(""));
    public static readonly MethodInfo ParseExact_input_formats_formatProvider_styles = M(() => System.DateTime.ParseExact("",new string[3],null,DateTimeStyles.None));
    public static readonly MethodInfo Parse_input_provider = M(() => System.DateTime.Parse("",System.Globalization.CultureInfo.CurrentCulture));
#pragma warning restore CA1305 // Specify IFormatProvider
    public static readonly MethodInfo op_LessThanOrEqual = typeof(System.DateTime).GetMethod(nameof(op_LessThanOrEqual))!;
    /// <summary>
    /// AddYears(Int32)
    /// </summary>
    public static readonly MethodInfo AddYear = M(() => System.DateTime.Now.AddYears(0));
    /// <summary>
    /// AddQuater
    /// </summary>
    public static readonly MethodInfo AddQuater = M(() => PrivateAddQuater(System.DateTime.Now,0));
    private static System.DateTime PrivateAddQuater(System.DateTime a,int Quater)=>a.AddMonths(Quater/4);
    /// <summary>
    /// AddMonths(Int32)
    /// </summary>
    public static readonly MethodInfo AddMonths = M(() => System.DateTime.Now.AddMonths(0));
    /// <summary>
    /// AddYear(Int32)
    /// </summary>
    public static readonly MethodInfo AddWeek= M(() => PrivateAddWeek(System.DateTime.Now,0));
    private static System.DateTime PrivateAddWeek(System.DateTime a,int Week) => a.AddDays(Week*7);
    /// <summary>
    /// AddDays(Double)
    /// </summary>
    public static readonly MethodInfo AddDays = M(() => System.DateTime.Now.AddDays(0));
    /// <summary>
    /// AddHours(Double)
    /// </summary>
    public static readonly MethodInfo AddHours = M(() => System.DateTime.Now.AddHours(0));
    /// <summary>
    /// AddMinutes(Double)
    /// </summary>
    public static readonly MethodInfo AddMinutes = M(() => System.DateTime.Now.AddMinutes(0));
    /// <summary>
    /// AddSeconds(Double)
    /// </summary>
    public static readonly MethodInfo AddSeconds = M(() => System.DateTime.Now.AddSeconds(0));
    /// <summary>
    /// AddMilliseconds(Double)
    /// </summary>
    public static readonly MethodInfo AddMilliseconds = M(() => System.DateTime.Now.AddMilliseconds(0));
    /// <summary>
    /// AddTick(Int64)
    /// </summary>
    public static readonly MethodInfo AddTicks = M(() => System.DateTime.Now.AddTicks(0));
    /// <summary>
    /// year,yy,yyyy
    /// </summary>
    public static readonly PropertyInfo Year = P(() => System.DateTime.Now.Year);
    /// <summary>
    /// quarter,qq,q
    /// </summary>
    public static readonly MethodInfo Quarter = M(() => PrivateQuarter(System.DateTime.Now));
    private static int PrivateQuarter(System.DateTime a) => a.Month/4+1;
    /// <summary>
    /// month,mm,m
    /// </summary>
    public static readonly PropertyInfo Month = P(() => System.DateTime.Now.Month);
    /// <summary>
    /// dayofyear,dy,y
    /// </summary>
    public static readonly PropertyInfo DayOfYear = P(() => System.DateTime.Now.DayOfYear);
    /// <summary>
    /// day,dd,d
    /// </summary>
    public static readonly PropertyInfo Day = P(() => System.DateTime.Now.Day);
    /// <summary>
    /// week,wk,ww
    /// </summary>
    public static readonly MethodInfo Week= M(() => PrivateWeek(System.DateTime.Now));
    private static int PrivateWeek(System.DateTime a) {
        var 元旦の曜日=((int)a.DayOfWeek)%7;
        var 週番号=(元旦の曜日+a.DayOfYear)/7;
        return 週番号;
        //日月火水木金土
        // 1 2 3 4 5 6 7
        //   1 2 3 4 5 6
        //     1 2 3 4 5
        //       1 2 3 4
        //         1 2 3
        //           1 2
        //             1
    }
    public static readonly PropertyInfo Now= P(() => System.DateTime.Now);
    /// <summary>
    /// Add(TimeSpan)
    /// </summary>
    public static readonly MethodInfo Add_TimeSpan = M(() => System.DateTime.Now.Add(new System.TimeSpan(0)));
    /// <summary>
    /// dw
    /// </summary>
    public static readonly PropertyInfo DayOfWeek = P(() => System.DateTime.Now.DayOfWeek);
    public static readonly PropertyInfo Hour = P(() => System.DateTime.Now.Hour);
    public static readonly PropertyInfo Minute = P(() => System.DateTime.Now.Minute);
    public static readonly PropertyInfo Second = P(() => System.DateTime.Now.Second);
    public static readonly PropertyInfo Millisecond = P(() => System.DateTime.Now.Millisecond);
    public static readonly MethodInfo Microsecond = M(() => PrivateMicrosecond(System.DateTime.Now));
    private static int PrivateMicrosecond(System.DateTime a) => (int)(a.Ticks/100000);
    public static readonly MethodInfo Nanosecond = M(() => PrivateNanosecond(System.DateTime.Now));
    private static int PrivateNanosecond(System.DateTime a) => (int)(a.Ticks/100);
    public static readonly PropertyInfo Ticks = P(() => System.DateTime.Now.Ticks);
    public static readonly MethodInfo Subtract_DateTime = M(() => System.DateTime.Now.Subtract(System.DateTime.Now));
    public static readonly MethodInfo Subtract_TimeSpan = M(() => System.DateTime.Now.Subtract(new System.TimeSpan(0)));
    public static readonly MethodInfo GetValueOrDefault = M(() => ((System.DateTime?)System.DateTime.Now).GetValueOrDefault());
    public static readonly PropertyInfo HasValue = P(() => ((System.DateTime?)System.DateTime.Now).HasValue);
    public static readonly MethodInfo ToString_ = ToString<System.DateTime>();
}
