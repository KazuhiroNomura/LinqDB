using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
namespace LinqDB.Optimizers.ReturnExpressionTraverser.Profiling;
public sealed class List計測:List<計測>{
    internal static class Reflection{
        public static readonly MethodInfo Start= typeof(Stopwatch).GetMethod(nameof(Stopwatch.Start))!;
        public static readonly MethodInfo Stop = typeof(Stopwatch).GetMethod(nameof(Stopwatch.Stop))!;
    }
    private readonly StringBuilder sb=new();
    private readonly List<string>ListString=new();
    internal readonly Stopwatch Stopwatch=new();
    public long ElapsedMilliseconds=>this.Stopwatch.ElapsedMilliseconds;
    public string Analize{
        get{
            if(this.Count==0) return string.Empty;
            var ListString=this.ListString;
            ListString.Clear();
            var sb=this.sb;
            this[0].Consoleに出力(ListString,sb,this.Stopwatch.ElapsedMilliseconds);
            sb.Clear();
            foreach(var a in ListString) sb.AppendLine(a);
            return sb.ToString();
        }
    }
}
//20311228 1166
