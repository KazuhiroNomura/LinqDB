using System.Reflection;
namespace LinqDB.Reflection;
using static Common;
internal static class Guid{
    public static readonly MethodInfo Parse_s = M(() => System.Guid.Parse(""));
    public static readonly MethodInfo TryParse_input_result = typeof(System.Guid).GetMethod("TryParse",new[] {typeof(string),typeof(System.Guid).MakeByRefType() })!;
    //public static readonly ConstructorInfo ctor_abcdefghijk= C(() => System.Guid.pa new System.Guid(0,0,0,0,0,0,0,0,0,0,0));
    //public static readonly ConstructorInfo ctor_input_format = C(() => System.Guid.ParseExact("",""));
    //public void gg(){System.Guid.NewGuid().ToByteArray
    public static readonly MethodInfo ToByteArray =  M(() => new System.Guid(0,1,2,3,4,5,6,7,8,9,10).ToByteArray());//typeof(System.Guid).GetMethod("ToByteArray")!;
    public static readonly MethodInfo ToString_ = ToString<System.Guid>();
}