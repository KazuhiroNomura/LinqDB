using System.Linq.Expressions;
using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Optimizers.変換_跨ぎ変数の先行評価;

[TestClass]
public class Test_取得_ループラムダ先行評価式 : ATest
{
    [TestMethod]
    public void Traverse()
    {
        //if(e==null) return;
        this.実行結果が一致するか確認((a, b) => ArrN<int>(a));
        //switch(e.NodeType) {
        //    case ExpressionType.Constant:{
        //        if(e.Type.Is定数をILで直接埋め込めるTypeか()) return;
        this.実行結果が一致するか確認((a, b) => ArrN<int>(a).Select(c => ArrN<int>(b).Select(d => 1)));
        this.実行結果が一致するか確認((a, b) => ArrN<int>(a).Select(c => ArrN<int>(b).Select(d => 1m)));
        //    }
        //    case ExpressionType.Lambda:
        this.実行結果が一致するか確認((a, b, c) => ArrN<int>(a).Select(d => ArrN<int>(b).Select(e => ArrN<int>(c).Select(f => e))));
        //    case ExpressionType.Block:{
        this.実行結果が一致するか確認((a, b) =>
            ArrN<int>(a).Let(c =>
                ArrN<int>(b).Let(d => c)
            )
        );
        this.実行結果が一致するか確認((a, b) => ArrN<int>(a).Let(c => ArrN<int>(b).Let(d => c)));
        //    }
        //}
        //if(this.変数_判定_指定Parametersが存在しない.実行(e,this._Parameters)) {
        this.実行結果が一致するか確認((a, b, c) => ArrN<int>(a).Select(d => ArrN<int>(b).Select(e => ArrN<int>(c).Select(f => e))));
        //}
        this.実行結果が一致するか確認((a, b, c) => ArrN<int>(a).Select(d => ArrN<int>(b).Select(e => ArrN<int>(c).Select(f => e))));
    }
    [TestMethod]
    public void 発見したうまくいかないパターン0(){
        var a=Expression.Parameter(typeof(int),"a");
        var p=Expression.Parameter(typeof(int),"p");
        var Select_selector=global::LinqDB.Reflection.ExtensionSet.Select_selector.MakeGenericMethod(typeof(int),typeof(int));
        var Lambda = Expression.Lambda<Func<ImmutableSet<int>>>(
            Expression.Block(
                new[]{a },
                Expression.Assign(a,Expression.Constant(2)),
                Expression.Call(
                    Select_selector,
                    Expression.Constant(new Set<int>{1,2}),
                    Expression.Lambda<Func<int,int>>(
                        Expression.Add(a,a),
                        p
                    )
                )
            )
        );
        this.CreateDelegate(Lambda);
    }
}