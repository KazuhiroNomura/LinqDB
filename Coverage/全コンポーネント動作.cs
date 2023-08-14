using LinqDB.Remote.Servers;
using static LinqDB.Helpers.Configulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS;

[TestClass]
public class 全コンポーネント動作
{
    [TestMethod]
    public void Open()
    {
        using var S = Server.Create(1,ListenerSocketポート番号);
        S.Open();
    }
}