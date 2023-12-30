using System;
using System.Globalization;
using System.Text;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException
// ReSharper disable All
namespace LinqDB.Optimizers.ReturnExpressionTraverser.Profiling;
using static Common;
public class 計測しない:計測{
    //internal static class Reflection {
    //    public static readonly MethodInfo Start = typeof(計測しない).GetMethod(nameof(Start),Instance_NonPublic_Public)!;
    //    public static readonly MethodInfo Stop = typeof(計測しない).GetMethod(nameof(Stop),Instance_NonPublic_Public)!;
    //    public static readonly MethodInfo StopReturn = typeof(計測しない).GetMethod(nameof(StopReturn),Instance_NonPublic_Public)!;
    //}
    public 計測しない(int 制御番号,string Name,string? Value):base(制御番号,Name,Value){
    }
    private protected 計測しない(計測 制御計測,string Name):base(制御計測,Name){
    }
    private protected 計測しない(string Name):base(Name){
    }
    internal sealed override long 部分100ns{
        get{
            long r=0;
            foreach(var 子 in this.List子演算)
                r+=子.部分100ns;
            return r;
        }
    }
    //internal override long 割合計算(long 全体100ns) {
    //    var 子の部分100ns合計 = this.部分100ns;
    //    var 部分100ns = this.部分100ns;
    //    foreach(var 子 in this.List子演算)
    //        子.割合計算(全体100ns);
    //    var 節100ns = 部分100ns;
    //    var sb = new StringBuilder();
    //    sb.Append('│');
    //    共通100ns(部分100ns);
    //    共通100ns(節100ns);
    //    共通割合(部分100ns,全体100ns);
    //    共通割合(節100ns,全体100ns);
    //    共通割合(節100ns,部分100ns);
    //    var 呼出回数 = 0L;//this.呼出回数;
    //    if(呼出回数>=10000000000)
    //        sb.Append("MAX       ");
    //    else
    //        sb.Append($"{呼出回数,10}");
    //    sb.Append('│');
    //    this.数値表=sb.ToString();
    //    return 子の部分100ns合計;
    //    void 共通100ns(long Value) {
    //        sb.Append($"{Value/10000,6}");
    //        sb.Append('│');
    //    }
    //    void 共通割合(double 分子100ns,double 分母100ns) {
    //        if(分母100ns==0) {
    //            sb.Append("    ");
    //        } else {
    //            sb.Append(((double)分子100ns/分母100ns).ToString("0.00",CultureInfo.InvariantCulture));
    //        }
    //        sb.Append('│');
    //    }
    //}
}
