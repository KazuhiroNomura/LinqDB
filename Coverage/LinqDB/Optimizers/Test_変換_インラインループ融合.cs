using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class Test_変換_インラインループ融合: ATest
{
    [TestMethod]
    public void 融合0(){
        var s = ArrN<int>(1);
        this.Execute引数パターン(
            a => new {
                a = s.Where(p => p%2==1).Select(p => p+11),
                b = s.Where(p => p%2==2).Select(p => p+12)
            }
        );
        this.Execute引数パターン(
            a => new {
                a = s.Where(p => p%2==1).Select(p => p+11),
                b = s.Where(p => p%2==2).Select(p => p+12),
                c = s.Where(p => p%2==3).Select(p => p+21),
                d = s.Where(p => p%2==4).Select(p => p+22)
            }
        );
        this.Execute引数パターン(
            a => new {
                a = s.Where(p => p%2==1).Select(p => p+11),
                b = s.Where(p => p%2==1).Select(p => p+12),
                c = s.Where(p => p%2==2).Select(p => p+21),
                d = s.Where(p => p%2==2).Select(p => p+22)
            }
        );
        this.Execute引数パターン(
            a => new {
                a = s.Where(p => p%2==0).Select(p => p+1),
                b = s.Where(p => p%2==0).Select(p => p+2)
            }
        );
    }
}