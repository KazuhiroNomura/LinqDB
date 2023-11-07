using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
// ReSharper disable LocalizableElement
namespace LinqDB.Optimizers;
using static Common;

/// <summary>
/// Expressionを最適化する
/// </summary>
public sealed partial class Optimizer{
    [Serializable]
    public class 計測しない:A計測{
        public 計測しない(ParameterExpression Parameter,string Name,string? Value,int 番号):base(Parameter,Name,Value,番号) {
        }
        public 計測しない(string Name,string? Value,int 番号) : base(Name,Value,番号) {
        }
        public 計測しない(string Name) : base(Name) {
        }
        internal override long ms {
            get {
                long r = 0;
                foreach(var 子 in this.List計測) {
                    r+=子.ms;
                }
                return r;
            }
        }

        internal override void 割合計算(long 全体木のms) {
            foreach(var 子 in this.List計測) {
                子.割合計算(全体木のms);
            }
            var sb = new StringBuilder();
            sb.Append('│');
            sb.Append($"{this.ms,6}");
            sb.Append("│      │");
            sb.Append((全体木のms==0 ? 0 : (double)this.ms/全体木のms).ToString("0.0000",CultureInfo.InvariantCulture));
            sb.Append("│      │      │          │");
            this.ヘッダ=sb.ToString();
        }
        internal override void 割合計算() => this.割合計算(this.ms);
        protected override A計測 Start() => this;
        protected override void Stop(){}
        protected override T StopReturn<T>(T item) => item;
    }
    [Serializable]
    public class 計測する:A計測 {
        public 計測する(ParameterExpression Parameter,string Name,string? Value,int 番号) : base(Parameter,Name,Value,番号) {
        }
        public 計測する(string Name,string? Value,int 番号):base(Name,Value,番号) {
        }
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
        internal override long ms => this.Stopwatch.ElapsedMilliseconds;

        /// <summary>
        /// 計測開始
        /// </summary>
        protected override A計測 Start() {
            this.Stopwatch.Start();
            this.呼出回数++;
            return this;
        }

        /// <summary>
        /// 計測終了
        /// </summary>
        protected override void Stop() => this.Stopwatch.Stop();
        protected override T StopReturn<T>(T item) {
            this.Stopwatch.Stop();
            return item;
        }
        internal override void 割合計算(long 全体木のms) {
            var 木ms = this.ms;
            var 節ms = 木ms;
            foreach(var 子 in this.List計測) {
                子.割合計算(全体木のms);
                節ms-=子.ms;
            }
            this.全体木に対する木のms割合=全体木のms==0 ? 0 : (double)this.ms/全体木のms;
            this.全体木に対する節のms割合=全体木のms==0 ? 0.0 : (double)節ms/全体木のms;
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
            this.ヘッダ=sb.ToString();
        }
        internal override void 割合計算() {
            this.全体木に対する木のms割合=0.0;
            this.全体木に対する節のms割合=0.0;
            this.木に対する節のms割合=0.0;
            this.割合計算(this.ms);
        }
        private long 排他ms {
            get {
                var ElapsedMilliseconds = this.Stopwatch.ElapsedMilliseconds;
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach(var 子 in this.List計測)
                    ElapsedMilliseconds-=子.ms;
                return ElapsedMilliseconds;
            }
        }
    }
    /// <summary>
    /// 罫線だけを出力する
    /// </summary>
    [Serializable]
    [DebuggerDisplay("{this.XElement.ToString()}")]
    public abstract class A計測 {
        internal static class Reflection {
            public static readonly MethodInfo Start = typeof(A計測).GetMethod(nameof(A計測.Start),Instance_NonPublic_Public)!;
            public static readonly MethodInfo Stop = typeof(A計測).GetMethod(nameof(A計測.Stop),Instance_NonPublic_Public)!;
            public static readonly MethodInfo StopReturn = typeof(A計測).GetMethod(nameof(A計測.StopReturn),Instance_NonPublic_Public)!;
        }
        [NonSerialized]
        private XElement XElement=null!;
        public int 番号;
        internal string Name{
            set=>this.XElement=new XElement(value);
        }
        internal string? Value {
            set {
                this.XElement.SetAttributeValue("Value",value);
            }
        }
        [NonSerialized]
        internal ParameterExpression? Parameter;
        protected A計測(ParameterExpression Parameter,string Name,string? Value,int 番号) {
            this.Parameter=Parameter;
            this.Name=Name;
            this.Value=Value;
            this.番号=番号;
        }
        protected A計測(string Name,string? Value,int 番号) {
            this.Name=Name;
            this.Value=Value;
            this.番号=番号;
        }
        protected A計測(string Name) {
            this.Name=Name;
        }
        /// <summary>
        /// 子要素のプロファイル│。
        /// </summary>
        protected readonly List<A計測> List計測=new();
        public void Clear()=>this.List計測.Clear();
        protected string? ヘッダ;
        private static void Write(string s) {
            Console.Write(s);
            Trace.Write(s);
            //Debug.Write(s);
        }
        private static void WriteLine(string s) {
            Console.WriteLine(s);
            Trace.WriteLine(s);
            //Debug.WriteLine(s);
        }
        private static void WriteLine() {
            Console.WriteLine();
            Trace.WriteLine("");
            //Debug.WriteLine("");
        }
        /// <summary>
        /// プロファイル結果をConsoleに出力
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization","CA1303:Do not pass literals as localized parameters",Justification = "<保留中>")]
        public void Consoleに出力(){
            WriteLine("┌───┬───┬───────────┬─────┐");
            WriteLine("│包含ms│排他ms│割合                  │ 呼出回数 │");
            WriteLine("│      │      ├───┬───┬───┤          │");
            WriteLine("│      │      │部分木│  節  │  節  │          │");
            WriteLine("│      │      │───│───│───│          │");
            WriteLine("│      │      │全体木│全体木│部分木│          │");
            WriteLine("├───┼───┼───┼───┼───┼─────┤");
            this.割合計算();
            this.展開("");
            WriteLine("└───┴───┴───┴───┴───┴─────┘");
        }
        private void 展開(string インデント){
            var XElement=this.XElement;
            Write(this.ヘッダ??"");
            Write(インデント);
            //Debug.Assert(XElement is not null);
            Write(XElement.Name.LocalName);
            var Value = XElement.Attribute("Value");
            if(Value is not null){
                Write(" "+Value.Value);
            }
            WriteLine();
            var List計測 = this.List計測;
            var List計測_Count_1 = List計測.Count-1;
            if(List計測_Count_1 >=0){
                var インデント_Length_1=インデント.Length-1;
                if(インデント_Length_1>=0){
                    var 罫線=インデント[インデント_Length_1];
                    // ReSharper disable once ConvertIfStatementToSwitchStatement
                    if(罫線=='├')
                        インデント=インデント[..インデント_Length_1]+'│';
                    else if(罫線=='└')
                        インデント=インデント[..インデント_Length_1]+'　';
                }
                var インデント2 = インデント+'├';
                for(var a = 0;a<List計測_Count_1 ;a++)
                    List計測[a].展開(インデント2);
                List計測[List計測_Count_1 ].展開(インデント+'└');
            }
        }
        /// <summary>
        /// Stopwatchのミリ秒を返す。
        /// </summary>
        internal abstract long ms{
            get;
        }
        /// <summary>
        /// 計測を子として追加する。
        /// </summary>
        /// <param name="末子"></param>
        public void Add末子(A計測 末子){
            this.List計測.Add(末子);
            //Debug.Assert(this.XElement is not null);
            this.XElement.Add(末子.XElement);
        }
        /// <summary>
        /// 計測開始
        /// </summary>
        protected abstract A計測 Start();
        /// <summary>
        /// 計測終了
        /// </summary>
        protected abstract void Stop();
        internal abstract void 割合計算(long 全体木のms);
        internal abstract void 割合計算();
        /// <summary>
        /// 戻り値のある計測終了
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        protected abstract T StopReturn<T>(T item);
        /// <summary>
        /// XDocumentを作成
        /// </summary>
        /// <returns></returns>
        public XDocument CreateXDocument() => new(this.XElement);
    }
}