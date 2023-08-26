using System.Linq.Expressions;
using System.Net;
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
    [TestMethod]public void ServerClient匿名型(){
        const int 回数 = 10;
        const int ReceiveTimeout = 1000;
        using var S = new Server<string>("",1,ListenerSocketポート番号) { ReadTimeout=ReceiveTimeout };
        S.Open();
        Console.WriteLine("Backend.Open();");
        using(var C = new Client<object>(Dns.GetHostName(),ListenerSocketポート番号)) {
            for(var a = 0;a<回数;a++) {
                var TargetありParameterなし2 = C.Expression(() => new{a=1,b=2});
            }
        }
        S.Close();
    }
    [TestMethod]public void ServerClientキャプチャ(){
        const int 回数 = 10;
        const int ReceiveTimeout = 1000;
        using var S = new Server<string>("",1,ListenerSocketポート番号) { ReadTimeout=ReceiveTimeout };
        S.Open();
        Console.WriteLine("Backend.Open();");
        using(var C = new Client<object>(Dns.GetHostName(),ListenerSocketポート番号)) {
            var Target = "Target";
            for(var a = 0;a<回数;a++) {
                var TargetありParameterなし0 = C.Expression(() => Target);
            }
        }
        S.Close();
    }
    [TestMethod]public void 特定パターン2(){
        const int 回数 = 10;
        const int ReceiveTimeout = 1000;
        using var S = new Server<string>("",1,ListenerSocketポート番号) { ReadTimeout=ReceiveTimeout };
        S.Open();
        Console.WriteLine("Backend.Open();");
        using(var C = new Client<object>(Dns.GetHostName(),ListenerSocketポート番号)) {
            var Target = "Target";
            for(var a = 0;a<回数;a++) {
                var TargetなしParameterあり = C.Expression(p => p+"TargetなしParameterあり");
                //try {
                //} catch(IOException) {
                //    //
                //}
                //try {
                //    var TargetありParameterあり = C.Expression(p => p+Target+"TargetありParameterあり");
                //} catch(IOException) {
                //    //
                //}
                //// var xx= R.Expression(() => "TargetなしParameterなし",() => "TargetなしParameterなし");
                //try {
                //    C.BytesSendTimeoutReceive(1,ReceiveTimeout*2);
                //    //
                //} catch(IOException) {
                //    //
                //}
                //try {
                //    var TargetなしParameterなし = C.Expression(() => "TargetなしParameterなし");
                //} catch(IOException) {
                //    //
                //}
            }
        }
        S.Close();
    }
}