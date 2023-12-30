using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException
// ReSharper disable All
namespace LinqDB.Optimizers.ReturnExpressionTraverser.Profiling;
using static Common;

public class 計測Label:計測{
    internal new static class Reflection {
        public static readonly MethodInfo Count = typeof(計測Label).GetMethod(nameof(Count),Instance_NonPublic_Public)!;
    }
    public 計測Label(int 制御番号,string Name,string? Value):base(制御番号,Name,Value){
    }
    public 計測Label(int 制御番号):base(制御番号){
    }
    private readonly Stopwatch Stopwatch=new();
    internal override long 部分100ns=>0;
    private void Count(){
        this.呼出回数++;
    }
    internal override long 割合計算(long 全体100ns){
        this.数値表=$"│      │      │    │    │    │{this.呼出回数,10}│";
        var 子の部分100ns合計 = 0L;
        foreach(var 子 in this.List子演算)
            子の部分100ns合計+=子.割合計算(全体100ns);
        return 子の部分100ns合計;
    }
}
