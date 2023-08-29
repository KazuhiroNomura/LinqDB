using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Optimizers.変換_WhereからDictionary;

[TestClass]
public class 変換_WhereからDictionary: ATest
{
    [TestMethod]
    public void ctor()
    {
        this.実行結果が一致するか確認(a => Lambda(b => ArrN<int>(a).Where(c => a == 0 && a == c)));
    }
}