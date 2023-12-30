using System;
using System.Globalization;
using System.Text;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException
// ReSharper disable All
namespace LinqDB.Optimizers.ReturnExpressionTraverser.Profiling;
using static Common;
[Serializable]
public class 仮想ノード : 計測
{
    public 仮想ノード(string Name) : base(Name) {this.数値表=空表;  }
    public 仮想ノード(計測 計測, string Name) : base(計測, Name) {this.数値表=空表;  }
    public 仮想ノード(int 制御番号, string Name, string Value) : base(制御番号, Name, Value)
    {this.数値表=空表; 
    }
    // override 
    internal override long 部分100ns{
        get{
            var r=0L;
            foreach(var 子 in this.List子演算)
                r+=子.部分100ns;
            return r;
        }
    }
    //internal override long 割合計算(long 全体100ns){
    //    var 子の部分100ns合計=0L;
    //    var 部分100ns=子の部分100ns合計;
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
    //    var 呼出回数=0L;//this.呼出回数;
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
    //    return 子の部分100ns合計;
    //}
}
