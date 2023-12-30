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
    private long 呼出回数;
    internal override long 部分ms=>this.Stopwatch.ElapsedMilliseconds;
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
    internal override void 割合計算(long 全体ms){
        var 部分ms=this.部分ms;
        var 節ms=部分ms;
        foreach(var 子 in this.List子演算){
            子.割合計算(全体ms);
            節ms-=子.部分ms;
        }
        var sb=new StringBuilder();
        sb.Append('│');
        共通ms(部分ms);
        共通ms(節ms);
        共通割合(部分ms,全体ms);
        共通割合(節ms,全体ms);
        共通割合(節ms,部分ms);
        var 呼出回数=this.呼出回数;
        if(呼出回数>=10000000000)
            sb.Append("MAX       ");
        else
            sb.Append($"{呼出回数,10}");
        sb.Append('│');
        this.数値表=sb.ToString();
        void 共通ms(long Value){
            sb.Append($"{Value,6}");
            sb.Append('│');
        }
        void 共通割合(double 分子ms,double 分母ms){
            if(分母ms==0){
                sb.Append("    ");
            } else{
                sb.Append(((double)分子ms/分母ms).ToString("0.00",CultureInfo.InvariantCulture));
            }
            sb.Append('│');
        }
    }
}
