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
    internal sealed override long 部分ms{
        get{
            long r=0;
            foreach(var 子 in this.List子演算){
                r+=子.部分ms;
            }
            return r;
        }
    }
    internal override void 割合計算(long 全体ms){
        foreach(var 子 in this.List子演算)
            子.割合計算(全体ms);
        this.数値表=空表;
    }
}
