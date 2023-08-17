using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class Test_取得_DelgateType_Quote: ATest
{
    [TestMethod]
    public void Call()
    {
        //if(ExtendedSet.ループ展開可能なEnumerableSetに属するGenericMethodDefinitionか(GetGenericMethodDefinition(MethodCall.Method))) {
        //    foreach(var MethodCall_Argument in MethodCall.Arguments) {
        this.Execute引数パターン(a => ArrN<int>(a).Select(p => p + 1));
        //    }
        //} else {
        this.Execute2(() => Lambda(p => 1));
        //}
    }
    private static int Lambda(Func<int, int> l) => 1;
    [TestMethod]
    public void Lambda()
    {
        //if(this._LambdaとしてAddするか) {
        this.Execute2(() => Lambda(p => 1));
        //} else {
        this.Execute引数パターン(a => ArrN<int>(a).Select(p => p + 1));
        //}
    }
    private static int Quote(Expression<Func<int, int>> l) => l.Compile()(3);
    [TestMethod]
    public void Quote()
    {
        this.Execute2(() => Quote(p => 1));
    }
}