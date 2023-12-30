using System.Diagnostics;
using System.Globalization;
using System.Text;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException
// ReSharper disable All
namespace LinqDB.Optimizers.ReturnExpressionTraverser.Profiling;
using static Common;

public class 計測する右辺値:計測{
    //internal static class Reflection {
    //    public static readonly MethodInfo Start = typeof(計測する左辺値).GetMethod(nameof(Start),Instance_NonPublic_Public)!;
    //    public static readonly MethodInfo Stop = typeof(計測する左辺値).GetMethod(nameof(Stop),Instance_NonPublic_Public)!;
    //    public static readonly MethodInfo StopReturn = typeof(計測する左辺値).GetMethod(nameof(StopReturn),Instance_NonPublic_Public)!;
    //}
    public 計測する右辺値(int 制御番号,string Name,string? Value):base(制御番号,Name,Value){
    }
    public 計測する右辺値(int 制御番号):base(制御番号){
    }
    private readonly Stopwatch Stopwatch=new();
    private protected override 計測 Start(){
        this.Stopwatch.Start();
        this.呼出回数++;
        return this;
    }
    private protected override void Stop()=>this.Stopwatch.Stop();
    private protected override T StopReturn<T>(T item){
        this.Stopwatch.Stop();
        return item;
    }
    internal override long 部分100ns{
        get{
            Debug.Assert(!this.Stopwatch.IsRunning);
            return this.Stopwatch.ElapsedTicks;
        }
    }
    //internal override long 割合計算(long 全体100ns){
    //    var 子の部分100ns合計=0L;
    //    var 部分100ns=this.部分100ns;
    //    foreach(var 子 in this.List子演算)
    //        子の部分100ns合計+=子.割合計算(全体100ns);
    //    var 節100ns=部分100ns-子の部分100ns合計;
    //    var sb=new StringBuilder();
    //    sb.Append('│');
    //    共通100ns(部分100ns);
    //    共通100ns(節100ns);
    //    共通割合(部分100ns,全体100ns);
    //    共通割合(節100ns,全体100ns);
    //    共通割合(節100ns,部分100ns);
    //    var 呼出回数=this.呼出回数;
    //    if(呼出回数>=10000000000)
    //        sb.Append("MAX       ");
    //    else
    //        sb.Append($"{呼出回数,10}");
    //    sb.Append('│');
    //    this.数値表=sb.ToString();
    //    void 共通100ns(long Value){
    //        sb.Append($"{Value/10000,6}");
    //        sb.Append('│');
    //    }
    //    void 共通割合(double 分子100ns,double 分母100ns){
    //        if(分母100ns==0){
    //            sb.Append("    ");
    //        } else{
    //            sb.Append(((double)分子100ns/分母100ns).ToString("0.00",CultureInfo.InvariantCulture));
    //        }
    //        sb.Append('│');
    //    }
    //    return 部分100ns;
    //}
}
