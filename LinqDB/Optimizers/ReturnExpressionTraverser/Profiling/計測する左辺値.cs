using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException
// ReSharper disable All
namespace LinqDB.Optimizers.ReturnExpressionTraverser.Profiling;
using static Common;

public class 計測する左辺値:計測する右辺値{
    internal new static class Reflection {
        public static readonly MethodInfo Start = typeof(計測する左辺値).GetMethod(nameof(計測する左辺値.Start),Instance_NonPublic_Public)!;
        public static readonly MethodInfo Stop = typeof(計測する左辺値).GetMethod(nameof(計測する左辺値.Stop),Instance_NonPublic_Public)!;
        public static readonly MethodInfo StopReturnRef = typeof(計測する左辺値).GetMethod(nameof(計測する左辺値.StopReturnRef),Instance_NonPublic_Public)!;
    }
    public 計測する左辺値(int 制御番号,string Name,string? Value,object Object) : base(制御番号,Name,Value) {
    }
    //public 左辺値計測する(int 制御番号,string Name,string? Value) : base(制御番号,Name,Value) {
    //}
    //public 左辺値計測する(int 制御番号) : base(制御番号) {
    //}
    /// <summary>
    /// 全体木に対する木の100ns割合
    /// </summary>
    [NonSerialized]
    private double 全体木に対する木の100ns割合;
    /// <summary>
    /// 全体木に対する節の100ns割合
    /// </summary>
    [NonSerialized]
    private double 全体木に対する節の100ns割合;
    /// <summary>
    /// 木に対する節の100ns割合
    /// </summary>
    [NonSerialized]
    private double 木に対する節の100ns割合;
    [NonSerialized]
    private readonly Stopwatch Stopwatch = new();
    internal override long 部分100ns => this.Stopwatch.ElapsedTicks;
    /// <summary>
    /// 計測開始
    /// </summary>
    private new 計測する左辺値 Start() {
        this.Stopwatch.Start();
        this.呼出回数++;
        return this;
    }

    private ref T StopReturnRef<T>(ref T item) {
        this.Stopwatch.Stop();
        return ref item;
    }
    internal override long 割合計算(long 全体100ns) {
        var 子の部分100ns合計=0L;
        var 部分100ns = this.部分100ns;
        foreach(var 子 in this.List子演算) {
            子の部分100ns合計+=子.割合計算(全体100ns);
        }
        var 節100ns=部分100ns-子の部分100ns合計;
        this.全体木に対する木の100ns割合=全体100ns==0 ? 0 : (double)this.部分100ns/全体100ns;
        this.全体木に対する節の100ns割合=全体100ns==0 ? 0.0 : (double)節100ns/全体100ns;
        this.木に対する節の100ns割合=部分100ns==0 ? 0.0 : (double)節100ns/部分100ns;
        var sb = new StringBuilder();
        sb.Append('│');
        void 共通100ns(long Value) {
            sb.Append($"{Value,6}");
            sb.Append('│');
        }
        void 共通割合(double 割合) {
            sb.Append(割合.ToString("0.0000",CultureInfo.InvariantCulture));
            sb.Append('│');
        }
        共通100ns(部分100ns);
        共通100ns(this.排他100ns);
        共通割合(this.全体木に対する木の100ns割合);
        共通割合(this.全体木に対する節の100ns割合);
        共通割合(this.木に対する節の100ns割合);
        var 呼出回数 = this.呼出回数;
        sb.Append(呼出回数>=10000000000 ? "MAX       " : $"{呼出回数,10}");
        sb.Append('│');
        this.数値表=sb.ToString();
        return 部分100ns;
    }
    //internal override void 割合計算() {
    //    this.全体木に対する木の100ns割合=0.0;
    //    this.全体木に対する節の100ns割合=0.0;
    //    this.木に対する節の100ns割合=0.0;
    //    this.割合計算(this.100ns);
    //}
    private long 排他100ns {
        get {
            var ElapsedTicks = this.Stopwatch.ElapsedTicks;
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach(var 子 in this.List子演算)
                ElapsedTicks-=子.部分100ns;
            return ElapsedTicks;
        }
    }
}
