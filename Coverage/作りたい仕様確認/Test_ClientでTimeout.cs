using System.Net;
using LinqDB.Remote.Servers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static LinqDB.Helpers.Configulation;
namespace CoverageCS.作りたい仕様確認;

[TestClass]
public class Test_ClientでTimeout {
    //[TestMethod]
    //public void タイムアウトしない() {
    //    using(var M = new global::Lite.Servers.MultiAcceptReceiveSend(1,ListenerSocketポート番号))
    //    using(var L = new Logic(M)) 
    //    {
    //        M.Open();
    //        L.Open();
    //        using(var R = new global::Lite.Remotes.Remote(Dns.GetHostName(),ListenerSocketポート番号,Timeout.Infinite,Timeout.Infinite)) {
    //            R.Call(
    //                () => Console.WriteLine(@"Sendでタイムアウトしてこれは表示されないはず"));
    //        }
    //    }
    //}
    private const int タイムアウトバイト数 = 1024*1024;
    [TestMethod]
    [ExpectedException(typeof(IOException))]
    public void Readタイムアウトした() {
        using var S = Server.Create("",1,ListenerSocketポート番号);
        S.Open();
        using var R = new global::LinqDB.Remote.Clients.Client(Dns.GetHostName(),ListenerSocketポート番号,Timeout.Infinite,1);
        R.Expression(() => new byte[タイムアウトバイト数]);
    }
    [TestMethod]
    [ExpectedException(typeof(IOException))]
    public void Writeタイムアウトした() {
        using var S = Server.Create("",1,ListenerSocketポート番号);
        S.Open();
        S.WriteTimeout=1;
        using var R = new global::LinqDB.Remote.Clients.Client(Dns.GetHostName(),ListenerSocketポート番号,1,Timeout.Infinite);
        R.BytesSendReceive(タイムアウトバイト数);
    }
}