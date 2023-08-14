using System.Diagnostics.CodeAnalysis;
using static LinqDB.Helpers.Configulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.作りたい仕様確認;

[TestClass]
public class Test_Server
{
    [TestMethod]
    public void 何度もインスタンス化を繰り返す()
    {
        for (var a = 0; a < 100; a++)
        {
            var M = new global::LinqDB.Remote.Servers.Server(1,1,ListenerSocketポート番号);
            M.Dispose();
        }
    }
    [TestMethod]
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public void 何度も実行を繰り返す()
    {
        using var M = new global::LinqDB.Remote.Servers.Server(1,1,ListenerSocketポート番号);
        for(var a = 0;a<4;a++) {
            M.Open();
            M.BeginClose();
            M.EndClose();
        }
    }
}