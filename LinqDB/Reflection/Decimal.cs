using System.Reflection;
namespace LinqDB.Reflection;

using static Common;
internal static class Decimal{
    //mal(int lo,int mid,int hi,bool isNegative,byte scale);
    public static readonly ConstructorInfo ctor_lo_mid_hi_isNegative_scale= C(() => new decimal(0,1,2,true,0));
    public static readonly MethodInfo Parse_s = M(() => decimal.Parse(""));
    public static readonly MethodInfo ToString_ = ToString<decimal>();
}