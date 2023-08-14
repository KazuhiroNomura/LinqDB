using System.Reflection;
using RegularExpressions=System.Text.RegularExpressions;
//Regex
namespace LinqDB.Reflection;

internal static class Regex {
    public static readonly MethodInfo IsMatch = typeof(RegularExpressions.Regex).GetMethod(
        nameof(RegularExpressions.Regex.IsMatch),
        LinqDB.Helpers.CommonLibrary.Types_String
    )!;
    public static readonly MethodInfo LikeからRegex = typeof(Regex).GetMethod(nameof(PrivateLikeからRegex),BindingFlags.NonPublic|BindingFlags.Static)!;
    private static RegularExpressions.Regex PrivateLikeからRegex(string s) {
        s=s.Replace('_','.').Replace("%",".*");
        return new RegularExpressions.Regex(s,RegularExpressions.RegexOptions.Compiled);
    }
}