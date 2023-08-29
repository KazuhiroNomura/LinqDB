using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class Test_変換_Anonymousをnewしてメンバーを参照している式の省略: ATest
{
    [TestMethod]
    public void MemberAccess()
    {
        //if(Member0_Expression==null) return Member0;
        this.実行結果が一致するか確認(a => ArrN<int>(a).Select(p => new Point(p, p)).Select(p => _StaticString));
        //if(Member1_Expression_Type_Name.IsAnonymous()) {
        //    if(New1!=null) {
        this.実行結果が一致するか確認((a, b) => ArrN<int>(a).Join(ArrN<int>(b), o => o, i => i, (o, i) => new { o, i }).Select(oi => new { oi, oi.o, oi.i }));
        //    }
        var anonimous = new { a = 3, b = 4 };
        this.実行結果が一致するか確認((a, b) => ArrN<int>(a).Join(ArrN<int>(b), o => o, i => i, (o, i) => new { o, i }).Select(oi => anonimous.a));
        //}
        this.実行結果が一致するか確認(a => ArrN<int>(a).Select(p => new Point(p, p)).Select(p => p.X));
    }
}