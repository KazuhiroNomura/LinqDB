using System.Diagnostics;
using System.Linq.Expressions;
using System.Net;
using LinqDB;
using LinqDB.Remote.Clients;
using LinqDB.Remote.Servers;
using LinqDB.Serializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utf8Json;
using static LinqDB.Helpers.Configulation;
// ReSharper disable LocalizableElement

// ReSharper disable UnusedVariable
namespace CoverageCS;

[TestClass]
public class Test_特定パターン
{
    [TestMethod]
    public void 特定パターン0()
    {
        Expression e=Expression.Constant(3);
        SerializerConfiguration SerializerConfiguration=new();

        //SerializerConfiguration.Clear();
        var JsonStream = new FileStream("Json.json",FileMode.Create,FileAccess.Write,FileShare.ReadWrite);
        JsonSerializer.Serialize(JsonStream,e,SerializerConfiguration.JsonFormatterResolver);
        JsonStream.Close();
    }
    [TestMethod]public void ServerClient匿名型0(){
        const int 回数 = 10;
        const int ReceiveTimeout = 1000;
        using var S = new Server<string>("",1,ListenerSocketポート番号) { ReadTimeout=ReceiveTimeout };
        S.Open();
        Console.WriteLine("Backend.Open();");
        using(var C = new Client<object>(Dns.GetHostName(),ListenerSocketポート番号)) {
            for(var a = 0;a<回数;a++) {
                var Json= C.SerializeSendReceive(new{a=1,b=2},XmlType.Utf8Json);
                var MessagePack= C.SerializeSendReceive(new{a=1,b=2},XmlType.MessagePack);
            }
        }
        S.Close();
    }
    [TestMethod]public void ServerClient匿名型1(){
        const int 回数 = 10;
        const int ReceiveTimeout = 1000;
        using var S = new Server<string>("",1,ListenerSocketポート番号) { ReadTimeout=ReceiveTimeout };
        S.Open();
        Console.WriteLine("Backend.Open();");
        using(var C = new Client<object>(Dns.GetHostName(),ListenerSocketポート番号)) {
            for(var a = 0;a<回数;a++) {
                var Json = C.Expression(() => new{a=1,b=2},XmlType.Utf8Json);
                var MessagePack = C.Expression(() => new{a=1,b=2},XmlType.MessagePack);
            }
        }
        S.Close();
    }
    [TestMethod]public void ServerClientキャプチャ1(){
        const int 回数 = 10;
        const int ReceiveTimeout = 1000;
        using var S = new Server<string>("",1,ListenerSocketポート番号) { ReadTimeout=ReceiveTimeout };
        S.Open();
        Console.WriteLine("Backend.Open();");
        using(var C = new Client<object>(Dns.GetHostName(),ListenerSocketポート番号)) {
            var Target = "Target";
            for(var a = 0;a<回数;a++) {
                var Utf8Json = C.Expression(() => Target,XmlType.Utf8Json);
                var MessagePack = C.Expression(() => Target,XmlType.MessagePack);
            }
        }
        S.Close();
    }
    [TestMethod]public void Json(){
        const int 回数 = 10;
        const int ReceiveTimeout = 1000;
        const string serverdata="serverdata";
        using var S = new Server<string>(serverdata,1,ListenerSocketポート番号) { ReadTimeout=ReceiveTimeout };
        S.Open();
        Console.WriteLine("Backend.Open();");
        using(var C = new Client<object>(Dns.GetHostName(),ListenerSocketポート番号)) {
            const string 定数= "定数";
            var キャプチャ= "キャプチャ";
            for(var a = 0;a<回数;a++) {
                Trace.WriteLine($"{a}回目");
                var r0= C.SerializeSendReceive(定数,XmlType.Utf8Json);
                Assert.AreEqual(定数,r0);
                var r1= C.Expression(() => 定数,XmlType.Utf8Json);
                Assert.AreEqual(定数,r1);
                var r2= C.Expression(() => キャプチャ,XmlType.Utf8Json);
                Assert.AreEqual(キャプチャ,r2);
                var r3= C.Expression(p => p+キャプチャ,XmlType.Utf8Json);
                Assert.AreEqual(serverdata+キャプチャ,r3);
            }
        }
        S.Close();
    }
    [TestMethod]public void MessagePack(){
        const int 回数 = 10;
        const int ReceiveTimeout = 1000;
        const string serverdata="serverdata";
        using var S = new Server<string>(serverdata,1,ListenerSocketポート番号) { ReadTimeout=ReceiveTimeout };
        S.Open();
        Console.WriteLine("Backend.Open();");
        using(var C = new Client<object>(Dns.GetHostName(),ListenerSocketポート番号)) {
            const string 定数= "定数";
            var キャプチャ= "キャプチャ";
            for(var a = 0;a<回数;a++) {
                Trace.WriteLine($"{a}回目");
                //var r0= C.SerializeSendReceive(定数,XmlType.MessagePack);
                //Assert.AreEqual(定数,r0);
                //var r1= C.Expression(() => 定数,XmlType.MessagePack);
                //Assert.AreEqual(定数,r1);
                var r2= C.Expression(() => キャプチャ,XmlType.MessagePack);
                Assert.AreEqual(キャプチャ,r2);
                var r3= C.Expression(p => p+キャプチャ,XmlType.MessagePack);
                Assert.AreEqual(serverdata+キャプチャ,r3);
            }
        }
        S.Close();
    }
}