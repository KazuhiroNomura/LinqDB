using System.Diagnostics.CodeAnalysis;
using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Optimizers.変換_跨ぎ変数の先行評価;

[TestClass]
public class Test_取得_ラムダ先行評価式の変数参照: ATest
{
    [TestMethod]
    [SuppressMessage("ReSharper", "ConvertToConstant.Local")]
    public void Traverse()
    {
        var A = 1;
        var B = 1;
        //if(e==null) return null;
        //if(this.Lラムダか&&this.変数_ExpressionEqualityComparer.Equals(e,this.旧Expression)) return this.新Parameter;
        //switch(e.NodeType){
        //    case ExpressionType.Block:{
        //        var Block=(BlockExpression)e;
        //        return this.共通LambdaBlock(Block,Block.Variables);
        //    }
        //    case ExpressionType.Lambda:{
        //    }
        //    default:return base.Traverse(e);
        //}
        this.実行結果が一致するか確認(() => A.Let(a => B.Let(b => a + 1)));
    }
}