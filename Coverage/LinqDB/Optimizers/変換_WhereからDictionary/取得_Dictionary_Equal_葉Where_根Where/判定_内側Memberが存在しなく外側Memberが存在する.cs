using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Optimizers.変換_WhereからDictionary.取得_Dictionary_Equal_葉Where_根Where;

[TestClass]
public class 判定_内側Memberが存在しなく外側Memberが存在する: ATest
{
    [TestMethod]
    public void Parameter()
    {
        //if(this.ListParameter.Contains(Parameter)){
        this.実行結果が一致するか確認(a => ArrN<int>(a).Where(c => c * 2 == c + 1));
        //} else{
        this.実行結果が一致するか確認(a => Lambda(b => ArrN<int>(a).Where(c => a == 0 && a == c)));
        //}
    }
    private static int Int32Lambda(int v, Func<int, int> func)
    {
        return func(v);
    }
    [TestMethod]
    public void Lambda()
    {
        this.実行結果が一致するか確認(a => ArrN<int>(a).Where(b => b == Int32Lambda(b, c => c + c)));
    }
}