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
    /// 全体木に対する木のms割合
    /// </summary>
    [NonSerialized]
    private double 全体木に対する木のms割合;
    /// <summary>
    /// 全体木に対する節のms割合
    /// </summary>
    [NonSerialized]
    private double 全体木に対する節のms割合;
    /// <summary>
    /// 木に対する節のms割合
    /// </summary>
    [NonSerialized]
    private double 木に対する節のms割合;
    [NonSerialized]
    private readonly Stopwatch Stopwatch = new();
    private long 呼出回数;
    internal override long 部分ms => this.Stopwatch.ElapsedMilliseconds;
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
    internal override void 割合計算(long 全体ms) {
        var 木ms = this.部分ms;
        var 節ms = 木ms;
        foreach(var 子 in this.List子演算) {
            子.割合計算(全体ms);
            節ms-=子.部分ms;
        }
        this.全体木に対する木のms割合=全体ms==0 ? 0 : (double)this.部分ms/全体ms;
        this.全体木に対する節のms割合=全体ms==0 ? 0.0 : (double)節ms/全体ms;
        this.木に対する節のms割合=木ms==0 ? 0.0 : (double)節ms/木ms;
        var sb = new StringBuilder();
        sb.Append('│');
        void 共通ms(long Value) {
            sb.Append($"{Value,6}");
            sb.Append('│');
        }
        void 共通割合(double 割合) {
            sb.Append(割合.ToString("0.0000",CultureInfo.InvariantCulture));
            sb.Append('│');
        }
        共通ms(木ms);
        共通ms(this.排他ms);
        共通割合(this.全体木に対する木のms割合);
        共通割合(this.全体木に対する節のms割合);
        共通割合(this.木に対する節のms割合);
        var 呼出回数 = this.呼出回数;
        sb.Append(呼出回数>=10000000000 ? "MAX       " : $"{呼出回数,10}");
        sb.Append('│');
        this.数値表=sb.ToString();
    }
    //internal override void 割合計算() {
    //    this.全体木に対する木のms割合=0.0;
    //    this.全体木に対する節のms割合=0.0;
    //    this.木に対する節のms割合=0.0;
    //    this.割合計算(this.ms);
    //}
    private long 排他ms {
        get {
            var ElapsedMilliseconds = this.Stopwatch.ElapsedMilliseconds;
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach(var 子 in this.List子演算)
                ElapsedMilliseconds-=子.部分ms;
            return ElapsedMilliseconds;
        }
    }
}
