using System.Diagnostics.CodeAnalysis;
using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static LinqDB.Sets.ExtensionSet;
namespace CoverageCS.LinqDB.Optimizers.変換_跨ぎ変数の先行評価;

[TestClass]
public class Test_取得_ループ先行評価式の変数参照 : ATest
{
    [TestMethod]
    [SuppressMessage("ReSharper", "ConvertToConstant.Local")]
    public void Traverse()
    {
        var A = 1;
        var B = 1;
        //if(e==null) return null;
        //if(this.Lループか&&this.変数_ExpressionEqualityComparer.Equals(e,this.旧Expression)) return this.新Parameter;
        this.実行結果が一致するか確認(() => Inline(() => B.Let(b => A + 1)));
        //switch(e.NodeType){
        //    case ExpressionType.Block:{
        //    }
        //    case ExpressionType.Lambda:{
        //    }
        //    default:return base.Traverse(e);
        //}
        this.実行結果が一致するか確認(() => Inline(() => Inline(() => A + 1)));
    }
}