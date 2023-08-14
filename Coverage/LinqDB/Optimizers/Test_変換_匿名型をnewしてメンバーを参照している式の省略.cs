using System.Drawing;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class Test_変換_Anonymousをnewしてメンバーを参照している式の省略: ATest
{
    [TestMethod]
    public void MemberAccess()
    {
        //if(Member0_Expression==null) return Member0;
        this.Execute引数パターン(a => ArrN<int>(a).Select(p => new Point(p, p)).Select(p => _StaticString));
        //if(Member1_Expression_Type_Name.IsAnonymous()) {
        //    if(New1!=null) {
        this.Execute引数パターン標準ラムダループ((a, b) => ArrN<int>(a).Join(ArrN<int>(b), o => o, i => i, (o, i) => new { o, i }).Select(oi => new { oi, oi.o, oi.i }));
        //    }
        var anonimous = new { a = 3, b = 4 };
        this.Execute引数パターン標準ラムダループ((a, b) => ArrN<int>(a).Join(ArrN<int>(b), o => o, i => i, (o, i) => new { o, i }).Select(oi => anonimous.a));
        //}
        this.Execute引数パターン(a => ArrN<int>(a).Select(p => new Point(p, p)).Select(p => p.X));
    }
}