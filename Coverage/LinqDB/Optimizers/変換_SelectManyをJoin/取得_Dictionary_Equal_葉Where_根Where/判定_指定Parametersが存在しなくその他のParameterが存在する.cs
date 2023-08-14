using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Optimizers.変換_SelectManyをJoin.取得_Dictionary_Equal_葉Where_根Where;

[TestClass]
public class 判定_指定Parametersが存在しなくその他のParameterが存在する : ATest
{
    [TestMethod]
    public void Parameter()
    {
        //if(this.ListParameter.Contains(Parameter)){
        this.Execute引数パターン(a => ArrN<int>(a).Where(c => c * 2 == c + 1));
        //} else{
        this.Execute引数パターン(a => Lambda(b => ArrN<int>(a).Where(c => a == 0 && a == c)));
        //}
    }
    private static int Int32Lambda(int v, Func<int, int> func)
    {
        return func(v);
    }
    [TestMethod]
    public void Lambda()
    {
        this.Execute引数パターン(a => ArrN<int>(a).Where(b => b == Int32Lambda(b, c => c + c)));
    }
}