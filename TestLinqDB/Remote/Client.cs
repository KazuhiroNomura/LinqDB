using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using LinqDB;
using LinqDB.Remote.Servers;
namespace TestLinqDB.Remote;
public class Client : 共通{
    protected override テストオプション テストオプション=>テストオプション.MemoryPack_MessagePack_Utf8Json|テストオプション.リモート実行;
    private static string CreateRequest(string server,string path){
        return $"GET {path} HTTP/1.1\r\n"+
               $"Host: {server}\r\n"+
               $"Connection: Close\r\n\r\n";
    }
    [Fact]public void httpGoogle(){
        const string server="www.google.com";
        using var R = new LinqDB.Remote.Clients.Client(server,80);
        R.HttpRequest(CreateRequest(server,"/"));
    }
    //[Fact]public void httpsGoogle(){
    //    const string server="www.google.com";
    //    var X509Certificate=new X509Certificate2(@"証明書\Google.crt");
    //    using var R = new LinqDB.Remote.Clients.Client(server,443);
    //    R.X509Certificate=X509Certificate;
    //    R.HttpRequest(CreateRequest(server,"/"));
    //}
    [Fact]public void 阿部寛(){
        const string server="abehiroshi.la.coocan.jp";
        const string path="/tv/tv.htm";
        using var R = new LinqDB.Remote.Clients.Client(server,80);
        R.HttpRequest(CreateRequest(server,path));
    }
    [Fact]public void Ssl(){
        const int receiveTimeout = 1000;
        //var port = Interlocked.Increment(ref ポート番号);
        const int port=443;
        using var Server = new Server(1,port);
        Server.ReadTimeout=receiveTimeout;
        var X509Certificate=new X509Certificate2(@"証明書\certificate.pfx", "password");
        Server.X509Certificate=X509Certificate;
        Server.Open();
        using var R = new LinqDB.Remote.Clients.Client(Dns.GetHostName(),port);
        R.X509Certificate=X509Certificate;
        //R.SerializeSendReceive(1,SerializeType.MemoryPack);
        R.BytesSendReceive(10);
        Server.Close();
        var target=Expression.Label(typeof(int),"target");
        var input=Expression.MakeGoto(
            GotoExpressionKind.Return,
            target,
            Expression.Constant(5),
            typeof(byte)
        );
        this.ExpressionシリアライズAssertEqual(input);
    }
    [Fact]public void SerializeSendReceive(){
        const int receiveTimeout = 1000;
        var port = Interlocked.Increment(ref ポート番号);
        using var Server = new Server(1,port);
        Server.ReadTimeout=receiveTimeout;
        Server.Open();
        using var R = new LinqDB.Remote.Clients.Client(Dns.GetHostName(),port);
        R.SerializeSendReceive(1,SerializeType.MemoryPack);
        Server.Close();
        var target=Expression.Label(typeof(int),"target");
        var input=Expression.MakeGoto(
            GotoExpressionKind.Return,
            target,
            Expression.Constant(5),
            typeof(byte)
        );
        this.ExpressionシリアライズAssertEqual(input);
    }
    [Fact]public void BytesSendReceive(){
        const int receiveTimeout = 1000;
        var port = Interlocked.Increment(ref ポート番号);
        using var Server = new Server(1,port);
        Server.ReadTimeout=receiveTimeout;
        Server.Open();
        using var R = new LinqDB.Remote.Clients.Client(Dns.GetHostName(),port);
        R.BytesSendReceive(1);
        Server.Close();
        var target=Expression.Label(typeof(int),"target");
        var input=Expression.MakeGoto(
            GotoExpressionKind.Return,
            target,
            Expression.Constant(5),
            typeof(byte)
        );
        this.ExpressionシリアライズAssertEqual(input);
    }
    [Fact]public void EmptySendReceive(){
        const int receiveTimeout = 1000;
        var port = Interlocked.Increment(ref ポート番号);
        using var Server = new Server(1,port);
        Server.ReadTimeout=receiveTimeout;
        Server.Open();
        using var R = new LinqDB.Remote.Clients.Client(Dns.GetHostName(),port);
        R.EmptySendReceive();
        Server.Close();
        var target=Expression.Label(typeof(int),"target");
        var input=Expression.MakeGoto(
            GotoExpressionKind.Return,
            target,
            Expression.Constant(5),
            typeof(byte)
        );
        this.ExpressionシリアライズAssertEqual(input);
    }
}
