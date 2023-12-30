using System.Collections.Generic;
using System.Text;
namespace LinqDB.Optimizers.ReturnExpressionTraverser.Profiling;
public sealed class List計測:List<計測>{
    private readonly StringBuilder sb=new();
    private readonly List<string>ListString=new();
    public string Analize{
        get{
            if(this.Count==0) return string.Empty;
            var ListString=this.ListString;
            ListString.Clear();
            var sb=this.sb;
            this[0].Consoleに出力(ListString,sb);
            sb.Clear();
            foreach(var a in ListString) sb.AppendLine(a);
            return sb.ToString();
        }
    }
}
//20311228 1166
