using System;
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
    internal override long 部分ms { get; }
    internal override void 割合計算(long 全体ms){
        foreach(var 子 in this.List子演算)
            子.割合計算(全体ms);
        this.数値表=空表;
    }
}
