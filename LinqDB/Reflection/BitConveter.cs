using System.Reflection;
namespace LinqDB.Reflection;

using static Common;
internal static class BitConverter{
    public static readonly MethodInfo Parse_s =M(() => byte.Parse(""));
    public static readonly MethodInfo ToInt16 =M(() => System.BitConverter.ToInt16(System.Array.Empty<byte>(),0));
    public static readonly MethodInfo ToInt32 =M(() => System.BitConverter.ToInt32(System.Array.Empty<byte>(),0));
    public static readonly MethodInfo ToInt64 =M(() => System.BitConverter.ToInt64(System.Array.Empty<byte>(),0));
    public static readonly MethodInfo ToSingle=M(() => System.BitConverter.ToSingle(System.Array.Empty<byte>(),0));
    public static readonly MethodInfo ToDouble=M(() => System.BitConverter.ToDouble(System.Array.Empty<byte>(),0));
    public static readonly MethodInfo ToString_=M(()=> System.BitConverter.ToString(System.Array.Empty<byte>(),0));
    public static readonly MethodInfo GetBytes_bool = M(() => System.BitConverter.GetBytes(false));
    //public static readonly MethodInfo GetBytes_char = M(() => System.BitConverter.GetBytes('a'));
    public static readonly MethodInfo GetBytes_short = M(() => System.BitConverter.GetBytes((short)0));
    public static readonly MethodInfo GetBytes_int = M(() => System.BitConverter.GetBytes(0));
    public static readonly MethodInfo GetBytes_long = M(() => System.BitConverter.GetBytes(0L));
    public static readonly MethodInfo GetBytes_float = M(() => System.BitConverter.GetBytes(0F));
    public static readonly MethodInfo GetBytes_double = M(() => System.BitConverter.GetBytes(0D));
}