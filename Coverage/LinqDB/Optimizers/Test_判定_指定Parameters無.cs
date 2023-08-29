using LinqDB.Sets;
using static LinqDB.Sets.ExtensionSet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class Test_判定_指定Parameters無: ATest
{
    private static int 最適化されるField = 0;
    private static int 最適化されるMethod() => 1;
    [TestMethod]
    public void MakeAssign()
    {
        this.実行結果が一致するか確認(() => Lambda(a => Lambda(c => c == a)));
    }
    [TestMethod]
    public void Parameter0(){
        this.実行結果が一致するか確認(p=> 
            Inline(() => p + 1)
        );
    }
    [TestMethod]public void Parameter1(){
        //ループ変数が必要ないのに作られる
        this.実行結果が一致するか確認(c => 
            Lambda(a => 
                Inline(() => c == a)
            )
        );
    }
    [TestMethod]public void Parameter2(){
        //ループ変数が必要ないのに作られる
        this.実行結果が一致するか確認(c => 
            Lambda(a => 
                Lambda(b => 
                    Inline(() => c == a)
                )
            )
        );
    }
    [TestMethod]
    public void MemberAccess()
    {
        //if(Member.Member.GetCustomAttribute(typeof(NoOptimizeAttribute))!=null) {
        this.実行結果が一致するか確認(() => Inline(() => 最適化されるField.NoLoopUnrolling()));
        //}
        this.実行結果が一致するか確認(() => Inline(() => 最適化されるField));
    }
    [TestMethod]
    public void Call()
    {
        //if(MethodCall.Method.GetCustomAttribute(typeof(NoOptimizeAttribute))!=null) {
        this.実行結果が一致するか確認(() => Inline(() => 最適化されるMethod().NoLoopUnrolling()));
        //}
        // ReSharper disable once ConvertClosureToMethodGroup
        this.実行結果が一致するか確認(() => Inline(() => 最適化されるMethod()));
    }
}