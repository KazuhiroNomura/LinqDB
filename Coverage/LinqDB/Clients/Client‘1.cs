using System.Net;
using System.Security.Cryptography.X509Certificates;
using LinqDB;
using LinqDB.Databases;
using LinqDB.Remote.Clients;
using static LinqDB.Helpers.Configulation;
using LinqDB.Remote.Servers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Clients;

[TestClass]
public class Test_Client1{
    private sealed class Entities:Container{
    }
    private void 戻り値なしメソッド(){
    }
    [TestMethod]
    public void Client_String_Int32_Int32_Int32() {
        using var E = new Entities();
        using var S =Server.Create(E,1,ListenerSocketポート番号);
        S.Open();
        using var R = new Client<Entities>(
            Dns.GetHostName(),
            ListenerSocketポート番号,
            1000,
            1000
        );
        R.Expression(p => this.戻り値なしメソッド());
    }
    [TestMethod]
    public void Client_String_Int32() {
        using var E = new Entities();
        using var S = Server.Create(E,1,ListenerSocketポート番号);
        S.Open();
        using var R = new Client<Entities>(
            Dns.GetHostName(),
            ListenerSocketポート番号
        );
        R.Expression(p => this.戻り値なしメソッド());
    }
    [TestMethod]
    public void Client() {
        using var E = new Entities();
        using var S = Server.Create(E,1,ListenerSocketポート番号);
        S.Open();
        using var R = new Client<Entities>{
            DnsEndPoint =new DnsEndPoint("localhost",ListenerSocketポート番号)
        };
        R.Expression(p => this.戻り値なしメソッド());
    }
    [TestMethod]
    public void TResultExpression(){
        using var E = new Entities();
        using var S = Server.Create(E,1,ListenerSocketポート番号);
        S.Open();
        using var R=new Client<Entities>(Dns.GetHostName(),ListenerSocketポート番号);
        R.Expression(p=>this.戻り値なしメソッド());
    }
    static int Throw()=>throw new InvalidOperationException();
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TResultExpression_InvalidOperationException() {
        using var E = new Entities();
        using var S = Server.Create(E,1,ListenerSocketポート番号);
        S.Open();
        using var R = new Client<Entities>(Dns.GetHostName(),ListenerSocketポート番号);
        R.Expression(p => Throw());
    }
    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public void SendTimeoutException_InvalidDataException1() {
        using var E = new Entities();
        using var S = Server.Create(E,1,ListenerSocketポート番号);
        S.Open();
        using var R = new Client<Entities>(Dns.GetHostName(),ListenerSocketポート番号);
        R.SendTimeoutException(XmlType.Utf8Json);
    }
    [TestMethod]
    public void ObjectExpression() {
        using var E = new Entities();
        using var S = Server.Create(E,1,ListenerSocketポート番号);
        S.Open();
        using var R = new Client<Entities>(Dns.GetHostName(),ListenerSocketポート番号);
        R.Expression("SELECT 1",XmlType.Utf8Json);
        R.Expression("SELECT 1",XmlType.MessagePack);
    }
    [TestMethod]
    public void VoidExpression(){
        using var E=new Entities();
        using var S = Server.Create(E,1,ListenerSocketポート番号);
        S.Open();
        using var R=new Client<Entities>(Dns.GetHostName(),ListenerSocketポート番号);
        Assert.AreEqual(R.Expression(p=>p.ToString()),E.ToString());
    }
    [TestMethod]
    public void X509Certificxate0(){
        var X509Certificate=new X509Certificate2("localhost.pfx","password");
        using var E = new Entities();
        using var S = new Server<Entities>(E,1,ListenerSocketポート番号){
            X509Certificate =X509Certificate
        };
        S.Open();
        using var R = new Client<Entities>(Dns.GetHostName(),ListenerSocketポート番号) {
            X509Certificate=X509Certificate
        };
        Assert.AreEqual(R.Expression(p => p.ToString()),E.ToString());
    }
    [TestMethod]
    public void X509Certificxate1() {
        using var E = new Entities();
        using var S = Server.Create(E,1,ListenerSocketポート番号);
        S.Open();
        using var R = new Client<Entities>(Dns.GetHostName(),ListenerSocketポート番号) {
            X509Certificate=null
        };
        Assert.AreEqual(R.Expression(p => p.ToString()),E.ToString());
    }
    [TestMethod]
    public void XmlSendReceive_T_XmlType() {
        using var E = new Entities();
        using var S = Server.Create(E,1,ListenerSocketポート番号);
        S.Open();
        using var R = new Client<Entities>(Dns.GetHostName(),ListenerSocketポート番号);
        R.XmlSendReceive("",XmlType.Utf8Json);
    }
    [TestMethod]
    public void XmlSendReceive_T() {
        using var E = new Entities();
        using var S = Server.Create(E,1,ListenerSocketポート番号);
        S.Open();
        using var R = new Client<Entities>(Dns.GetHostName(),ListenerSocketポート番号);
        R.XmlSendReceive("");
    }
    [TestMethod]
    public new void ToString(){
        var expected=Dns.GetHostName();
        using var R = new Client<Entities>(expected,ListenerSocketポート番号);
        Assert.AreEqual(new DnsEndPoint(expected,ListenerSocketポート番号).ToString(),R.ToString());
    }
    [TestMethod]
    public void SerializedSendReceive() {
        using var S = Server.Create(1,ListenerSocketポート番号);
        S.Open();
        using var R = new Client<Entities>(Dns.GetHostName(),ListenerSocketポート番号);
        var expected = new byte[475];
        for(var b = 0;b<expected.Length;b++) {
            expected[b]=(byte)b;
        }
        var actual = R.SerializeSendReceive(expected);
        Assert.IsTrue(expected.SequenceEqual(actual));
    }
    [TestMethod]
    public void SendTimeoutException() {
        using var S = Server.Create(1,ListenerSocketポート番号);
        S.Open();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        try {
            R.SendTimeoutException(XmlType.Utf8Json);
        } catch(InvalidDataException ex) {
            Assert.IsTrue(ex.InnerException is TimeoutException);
        }
    }
    [TestMethod]
    public void Finalize0() {
#pragma warning disable IDE0022 // メソッドに式本体を使用する
        // ReSharper disable once ObjectCreationAsStatement
        new Client(Dns.GetHostName(),ListenerSocketポート番号);
#pragma warning restore IDE0022 // メソッドに式本体を使用する
    }
    //[TestMethod]
    //public void Expression_Func() {
    //    using var S = Server.Create(1,ListenerSocketポート番号);
    //    S.Open();
    //    using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
    //    R.Call(() => "XmlType.Utf8Json",XmlType.Utf8Json);
    //    R.Call(() => "XmlType.MessagePack",XmlType.MessagePack);
    //}
    private static XmlType Throw(XmlType XmlType) {
        throw new InvalidOperationException(XmlType.ToString());
    }
    private  static void Expression_Func_InvalidOperationException(XmlType XmlType) {
        using var S = Server.Create(1,ListenerSocketポート番号);
        S.Open();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        var f = false;
        R.Expression(() => f ? XmlType: Throw(XmlType),XmlType);
    }
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Expression_Func_Utf8Json_InvalidOperationException() {
        Expression_Func_InvalidOperationException(XmlType.Utf8Json);
    }
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Expression_Func_MessagePack_InvalidOperationException() {
        Expression_Func_InvalidOperationException(XmlType.MessagePack);
    }
    [TestMethod]
    public void BytesSendTimeoutReceive() {
        using var S = Server.Create(1,ListenerSocketポート番号);
        S.Open();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        R.BytesSendTimeoutReceive(ClientMemoryStreamBufferSize-200,1);
    }
    [TestMethod]
    [ExpectedException(typeof(IOException))]
    public void BytesSendReceiveTimeout() {
        using var S = Server.Create(1,ListenerSocketポート番号);
        S.Open();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        R.BytesSendReceiveTimeout(10000000,1);
    }
    [TestMethod]
    public void BytesSendReceive_Int32() {
        using var S = Server.Create(1,ListenerSocketポート番号);
        S.Open();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        R.BytesSendReceive(10);
    }
    [TestMethod]
    public void BytesSendReceive_ByteArray() {
        using var S = Server.Create(1,ListenerSocketポート番号);
        S.Open();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        var Bytes=new byte[100];
        for(var a=0;a<Bytes.Length;a++){
            Bytes[a]=(byte)a;
        }
        R.BytesSendReceive(Bytes);
    }
    [TestMethod]
    public void Bytes1SendReceive_Int32() {
        using var S = Server.Create(1,ListenerSocketポート番号);
        S.Open();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        R.Bytes1SendReceive(10);
    }
    [TestMethod]
    public void ByteEofSendReceive() {
        using var S = Server.Create(1,ListenerSocketポート番号);
        S.Open();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        R.ByteEofSendReceive(10);
    }
}