using LinqDB.Optimizers;

using System.Globalization;
using System.Reflection;

namespace LinqDB.Reflection;

using static Common;
internal static class DateTimeOffset{
    public static readonly ConstructorInfo ctor_YearMonthDay = typeof(System.DateTimeOffset).GetConstructor(new[] { typeof(int),typeof(int),typeof(int) })!;
    //public static readonly ConstructorInfo ctor_Int64 = typeof(global::System.DateTimeOffset).GetConstructor(new[] { typeof(long)})!;
    public static readonly MethodInfo New_Int64_TimeSpan = M(() =>Private_New_Int64_TimeSpan(0,new System.TimeSpan()));
    private static System.DateTimeOffset Private_New_Int64_TimeSpan(long Ticks,System.TimeSpan TimeSpan)=>new(Ticks,TimeSpan);
    public static readonly MethodInfo TryGetValue2 = typeof(System.DateTimeOffset).GetMethod(nameof(System.DateTimeOffset.TryParse),new[] { typeof(string),typeof(System.DateTimeOffset).MakeByRefType() })!;
    //public static readonly MethodInfo Parse1 = typeof(global::System.DateTimeOffset).GetMethod(nameof(global::System.DateTimeOffset.Parse),BindingFlags.Static|BindingFlags.Public,null,new[] { typeof(String) },null);
#pragma warning disable CA1305 // Specify IFormatProvider
    public static readonly MethodInfo Parse_s = M(() => System.DateTimeOffset.Parse(""));
    public static readonly MethodInfo ParseExact_input_formats_formatProvider_styles = M(() => System.DateTimeOffset.ParseExact("",new string[3],null,DateTimeStyles.None));
    public static readonly MethodInfo Parse_input_provider = M(() => System.DateTimeOffset.Parse("",System.Globalization.CultureInfo.CurrentCulture));
#pragma warning restore CA1305 // Specify IFormatProvider
    public static readonly MethodInfo op_LessThanOrEqual = typeof(System.DateTimeOffset).GetMethod(nameof(op_LessThanOrEqual))!;
    /// <summary>
    /// AddYears(Int32)
    /// </summary>
    public static readonly MethodInfo AddYear = M(() => System.DateTimeOffset.Now.AddYears(0));
    /// <summary>
    /// AddQuater
    /// </summary>
    public static readonly MethodInfo AddQuater = M(() => PrivateAddQuater(System.DateTimeOffset.Now,0));
    private static System.DateTimeOffset PrivateAddQuater(System.DateTimeOffset a,int Quater)=>a.AddMonths(Quater/4);
    /// <summary>
    /// AddMonths(Int32)
    /// </summary>
    public static readonly MethodInfo AddMonths = M(() => System.DateTimeOffset.Now.AddMonths(0));
    /// <summary>
    /// AddYear(Int32)
    /// </summary>
    public static readonly MethodInfo AddWeek= M(() => PrivateAddWeek(System.DateTimeOffset.Now,0));
    private static System.DateTimeOffset PrivateAddWeek(System.DateTimeOffset a,int Week) => a.AddDays(Week*7);
    /// <summary>
    /// AddDays(Double)
    /// </summary>
    public static readonly MethodInfo AddDays = M(() => System.DateTimeOffset.Now.AddDays(0));
    /// <summary>
    /// AddHours(Double)
    /// </summary>
    public static readonly MethodInfo AddHours = M(() => System.DateTimeOffset.Now.AddHours(0));
    /// <summary>
    /// AddMinutes(Double)
    /// </summary>
    public static readonly MethodInfo AddMinutes = M(() => System.DateTimeOffset.Now.AddMinutes(0));
    /// <summary>
    /// AddSeconds(Double)
    /// </summary>
    public static readonly MethodInfo AddSeconds = M(() => System.DateTimeOffset.Now.AddSeconds(0));
    /// <summary>
    /// AddMilliseconds(Double)
    /// </summary>
    public static readonly MethodInfo AddMilliseconds = M(() => System.DateTimeOffset.Now.AddMilliseconds(0));
    /// <summary>
    /// AddTick(Int64)
    /// </summary>
    public static readonly MethodInfo AddTicks = M(() => System.DateTimeOffset.Now.AddTicks(0));
    /// <summary>
    /// year,yy,yyyy
    /// </summary>
    public static readonly PropertyInfo Year = P(() => System.DateTimeOffset.Now.Year);
    /// <summary>
    /// quarter,qq,q
    /// </summary>
    public static readonly MethodInfo Quarter = M(() => PrivateQuarter(System.DateTimeOffset.Now));
    private static int PrivateQuarter(System.DateTimeOffset a) => a.Month/4+1;
    /// <summary>
    /// month,mm,m
    /// </summary>
    public static readonly PropertyInfo Month = P(() => System.DateTimeOffset.Now.Month);
    /// <summary>
    /// dayofyear,dy,y
    /// </summary>
    public static readonly PropertyInfo DayOfYear = P(() => System.DateTimeOffset.Now.DayOfYear);
    /// <summary>
    /// day,dd,d
    /// </summary>
    public static readonly PropertyInfo Day = P(() => System.DateTimeOffset.Now.Day);
    /// <summary>
    /// week,wk,ww
    /// </summary>
    public static readonly MethodInfo Week= M(() => PrivateWeek(System.DateTimeOffset.Now));
    private static int PrivateWeek(System.DateTimeOffset a) {
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
    public static readonly PropertyInfo Now= P(() => System.DateTimeOffset.Now);
    /// <summary>
    /// Add(TimeSpan)
    /// </summary>
    public static readonly MethodInfo Add_TimeSpan = M(() => System.DateTimeOffset.Now.Add(new System.TimeSpan(0)));
    /// <summary>
    /// dw
    /// </summary>
    public static readonly PropertyInfo DayOfWeek = P(() => System.DateTimeOffset.Now.DayOfWeek);
    public static readonly PropertyInfo Hour = P(() => System.DateTimeOffset.Now.Hour);
    public static readonly PropertyInfo Minute = P(() => System.DateTimeOffset.Now.Minute);
    public static readonly PropertyInfo Second = P(() => System.DateTimeOffset.Now.Second);
    public static readonly PropertyInfo Millisecond = P(() => System.DateTimeOffset.Now.Millisecond);
    public static readonly MethodInfo Microsecond = M(() => PrivateMicrosecond(System.DateTimeOffset.Now));
    private static int PrivateMicrosecond(System.DateTimeOffset a) => (int)(a.Ticks/100000);
    public static readonly MethodInfo Nanosecond = M(() => PrivateNanosecond(System.DateTimeOffset.Now));
    private static int PrivateNanosecond(System.DateTimeOffset a) => (int)(a.Ticks/100);
    public static readonly PropertyInfo Ticks = P(() => System.DateTimeOffset.Now.Ticks);
    public static readonly MethodInfo Subtract_DateTime = M(() => System.DateTimeOffset.Now.Subtract(System.DateTimeOffset.Now));
    public static readonly MethodInfo Subtract_TimeSpan = M(() => System.DateTimeOffset.Now.Subtract(new System.TimeSpan(0)));
    public static readonly MethodInfo GetValueOrDefault = M(() => ((System.DateTimeOffset?)System.DateTimeOffset.Now).GetValueOrDefault());
    public static readonly PropertyInfo HasValue = P(() => ((System.DateTimeOffset?)System.DateTimeOffset.Now).HasValue);
    public static readonly MethodInfo ToString_ = ToString<System.DateTimeOffset>();
}
