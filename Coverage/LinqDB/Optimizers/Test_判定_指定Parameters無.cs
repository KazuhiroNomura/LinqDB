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
        this.Execute2(() => Lambda(a => Lambda(c => c == a)));
    }
    [TestMethod]
    public void Parameter0(){
        this.Execute引数パターン(p=> 
            Inline(() => p + 1)
        );
    }
    [TestMethod]public void Parameter1(){
        //ループ変数が必要ないのに作られる
        this.Execute引数パターン(c => 
            Lambda(a => 
                Inline(() => c == a)
            )
        );
    }
    [TestMethod]public void Parameter2(){
        //ループ変数が必要ないのに作られる
        this.Execute引数パターン(c => 
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
        this.Execute2(() => Inline(() => 最適化されるField.NoLoopUnrolling()));
        //}
        this.Execute2(() => Inline(() => 最適化されるField));
    }
    [TestMethod]
    public void Call()
    {
        //if(MethodCall.Method.GetCustomAttribute(typeof(NoOptimizeAttribute))!=null) {
        this.Execute2(() => Inline(() => 最適化されるMethod().NoLoopUnrolling()));
        //}
        // ReSharper disable once ConvertClosureToMethodGroup
        this.Execute2(() => Inline(() => 最適化されるMethod()));
    }
}