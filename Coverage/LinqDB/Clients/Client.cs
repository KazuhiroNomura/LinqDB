using System.Diagnostics;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using CoverageCS.LinqDB.Sets;
using LinqDB;
using LinqDB.Databases;
using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static LinqDB.Helpers.Configulation;
using LinqDB.Helpers;
using LinqDB.Optimizers;
using LinqDB.Remote.Clients;
using LinqDB.Remote.Servers;

// ReSharper disable UnusedVariable
namespace CoverageCS.LinqDB.Clients;

[TestClass]
public class Test_Client {
    private Server<string> S = null!;
    [TestInitialize]
    public void MyTestInitialize() {
        this.S=Server.Create("",1,ListenerSocketポート番号);
        this.S.Open();
    }
    // 各テストを実行した後に、TestCleanup を使用してコードを実行してください
    [TestCleanup]
    public void MyTestCleanup() {
        this.S.Dispose();//RequestResponseSingleReceiveSends.DisposeするのでLogic.DisposeのCancelationTokenSource.Cancel()が失敗する。
        CommonLibrary.トレース("this.WCFBackend.Dispose();");
        GC.Collect();
    }
    private void Open() {
        this.S.Open();
    }
    private void Close() {
        this.S.BeginClose();
        this.S.EndClose();
    }
    private void 戻り値なしメソッド() {
    }
    [TestMethod]
    public void ctor() {
        using var Client = new Client();
        Assert.IsNotNull(Client);
    }
    [TestMethod]
    public void ByteEofSendReceive() {
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        R.ByteEofSendReceive(1);
    }
    [TestMethod]
    public void BytesSendReceive() {
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        R.BytesSendReceive(10);
        R.BytesSendReceive(new byte[] { 1,2,3 });
    }
    [TestMethod]
    public void EmptySendReceive() {
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        R.EmptySendReceive();
    }
    [TestMethod]
    public void Expression0() {
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        R.Expression(() => 1);
    }
    [TestMethod]
    public void Expression1() {
        using var R = new Client<string>(Dns.GetHostName(),ListenerSocketポート番号);
        R.Expression(b => b+b);
    }
    [TestMethod, ExpectedException(typeof(OutOfMemoryException))]
    public void BackendOutOfMemoryException() {
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        R.BackendOutOfMemoryException();
    }
    [TestMethod]
    public void SendTimeoutException_MessagePack() {
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        try {
            R.SendTimeoutException(XmlType.MessagePack);
        } catch(InvalidDataException ex) {
            Assert.IsTrue(ex.InnerException is TimeoutException);
        }
    }
    [TestMethod]
    public void SendTimeoutException_Utf8Json() {
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        try {
            R.SendTimeoutException(XmlType.Utf8Json);
        } catch(InvalidDataException ex) {
            Assert.IsTrue(ex.InnerException is TimeoutException);
        }
    }
    [TestMethod]
    public void SerializeSendReceive_string() {
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        var expected="ABC";
        Assert.AreEqual(expected,R.SerializeSendReceive(expected,XmlType.Utf8Json));
        Assert.AreEqual(expected,R.SerializeSendReceive(expected,XmlType.MessagePack));
    }
    [TestMethod]
    public void SerializeSendReceive_Type() {
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        var expected=typeof(int);
        {
            var actual=R.SerializeSendReceive(expected,XmlType.MessagePack);
            Assert.AreEqual(expected,actual);
        }
        {
            var actual=R.SerializeSendReceive(expected,XmlType.Utf8Json);
            Assert.AreEqual(expected,actual);
        }
    }
    [TestMethod]
    public void SerializeSendReceive_Generic() {
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        var expected=typeof(Func<int>);
        var actual=R.SerializeSendReceive(expected,XmlType.Utf8Json);
        Assert.AreEqual(expected,actual);
    }
    [TestMethod]
    public void SerializeSendReceive_カスタムデリゲート() {
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        var expected=typeof(Func<Func<int>>);
        var actual=R.SerializeSendReceive(expected,XmlType.Utf8Json);
        Assert.AreEqual(expected,actual);
    }
    [TestMethod]
    public new void ToString() {
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        Assert.AreEqual(R.ToString(),new DnsEndPoint(Dns.GetHostName(),ListenerSocketポート番号).ToString());
    }
    [TestMethod]
    public void XmlSendReceive() {
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        Assert.AreEqual(R.XmlSendReceive("ABC"),"ABC");
        Assert.AreEqual(R.XmlSendReceive("D",XmlType.MessagePack),"D");
        Assert.AreEqual(R.XmlSendReceive("F",XmlType.Utf8Json),"F");
    }
    private void メソッド名() {
        Trace.WriteLine(new StackTrace(1).GetFrame(0)!.GetMethod()!.Name);
    }
    private void Byte1を送信(int a) {
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        for(var b = 1;b<=a;b++) {
            R.ByteEofSendReceive((byte)b);
        }
    }
    private void Expression処理時間(int 回数,XmlType XmlType) {
        this.メソッド名();
        using var R = new Client(
            Dns.GetHostName(),
            ListenerSocketポート番号);
        var b = 9;
        for(var a = 0;a<回数;a++) {
            var actual = R.Expression(
                () => b,
                XmlType);
            Assert.AreEqual(
                b,
                actual);
        }
    }
    [TestMethod]
    public void 処理時間1() => this.Expression処理時間(1,XmlType.MessagePack);
    [TestMethod]
    public void 処理時間2() => this.Expression処理時間(2,XmlType.MessagePack);
    [TestMethod]
    public void 処理時間4() => this.Expression処理時間(4,XmlType.MessagePack);
    [TestMethod]
    public void 処理時間7() => this.Expression処理時間(7,XmlType.MessagePack);
    [TestMethod]
    public void 処理時間8() => this.Expression処理時間(10,XmlType.MessagePack);
    [TestMethod]
    public void 処理時間N() {
        this.メソッド名();
        for(var a = 1;a<10;a++) {
            this.Expression処理時間(
                a,
                XmlType.MessagePack);
        }
    }
    [TestMethod]
    public void Expression処理時間10Utf8Json() => this.Expression処理時間(10,XmlType.Utf8Json);
    [TestMethod]
    public void Expression処理時間10MessagePack() => this.Expression処理時間(10,XmlType.MessagePack);
    [TestMethod]
    public void SaveRequest() {
        this.メソッド名();
        using var s = new FileStream(
            "リクエスト.xml",
            FileMode.Create,
            FileAccess.ReadWrite,
            FileShare.ReadWrite);
        var b = 1;
        Client.SaveRequest(
            s,
            (Func<Server,int>)(_ => b));
    }
    [TestMethod]
    public void Utf8Jsonをファイルに書き込む() {
        var b = 1;
        int Func() => b;
        using(var m = new FileStream("Utf8Json.txt",FileMode.Create,FileAccess.ReadWrite,FileShare.ReadWrite)) {
            Utf8Json.JsonSerializer.Serialize(m,(Func<int>)Func);
        }
        using(var m = new FileStream("Utf8Json.txt",FileMode.Open,FileAccess.Read,FileShare.Read)) {
            var ReadObject2 = Utf8Json.JsonSerializer.Deserialize<object>(m);
        }
    }
    [TestMethod,ExpectedException(typeof(NotSupportedException))]
    public void Expression_キャプチャ禁止Exception() {
        this.メソッド名();
        using var R = new Client(
            Dns.GetHostName(),
            ListenerSocketポート番号);
        var b = 9;
        var actual = R.Expression(() => b);
        Assert.AreEqual(
            b,
            actual);
    }
    private void ExpressionをXで書き込む(XmlType XmlType) {
        this.S.リクエストを保存するか=true;
        using(var R = new Client(Dns.GetHostName(),ListenerSocketポート番号)) {
            const int expected = 1;
            var actual = R.Expression(() => expected,XmlType);
            Assert.AreEqual(expected,actual);
        }
        this.S.リクエストを保存するか=false;
    }
    [TestMethod]
    public void ExpressionをUtf8Jsonで書き込む()=>this.ExpressionをXで書き込む(XmlType.Utf8Json);
    [TestMethod]
    public void ExpressionをMessagePackで書き込む()=>this.ExpressionをXで書き込む(XmlType.MessagePack);
    private void ObjectをXで書き込む(XmlType XmlType) {
        this.S.リクエストを保存するか=true;
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        var actual = R.XmlSendReceive(1,XmlType);
        Assert.AreEqual(1,actual);
        this.S.リクエストを保存するか=false;
    }
    [TestMethod]
    public void ObjectをUtf8Jsonで書き込む()=>this.ObjectをXで書き込む(XmlType.Utf8Json);
    [TestMethod]
    public void ObjectをMessagePackで書き込む()=>this.ObjectをXで書き込む(XmlType.MessagePack);
    [TestMethod]
    public void 出力サイズ100() {
        this.メソッド名();
        using var R = new Client(
            Dns.GetHostName(),
            ListenerSocketポート番号);
        for(var a = 0;a<16;a++) {
            CommonLibrary.トレース(a.ToString());
            var a1 = a;
            R.Expression(() => new int[a1]);
        }
    }
    [TestMethod]
    public void SerializeSendReceive_Close_Open() {
        this.メソッド名();
        using(var R = new Client(
                  Dns.GetHostName(),
                  ListenerSocketポート番号)) {
            Assert.AreEqual(
                1,
                R.SerializeSendReceive(1,XmlType.Utf8Json));
        }
        this.Close();
        this.Open();
    }
    [TestMethod]
    public void 特殊0() {
        this.メソッド名();
        using(var R = new Client(
                  Dns.GetHostName(),
                  ListenerSocketポート番号)) {
            Assert.AreEqual(
                1,
                R.SerializeSendReceive(1,XmlType.Utf8Json));
        }
        this.Close();
        this.Open();
        using(var R = new Client(
                  Dns.GetHostName(),
                  ListenerSocketポート番号)) {
            for(var a = 0;a<1;a++) {
                var a1 = a;
                var actual = R.Expression(() => a1);
                Assert.AreEqual(
                    a,
                    actual);
            }
        }
        this.Close();
        this.Open();
        using(var R = new Client(
                  Dns.GetHostName(),
                  ListenerSocketポート番号)) {
            for(var a = 0;a<2;a++) {
                var a1 = a;
                R.Expression(() => new int[a1]);
            }
        }
    }
    [TestMethod]
    public void 特殊1() {
        this.メソッド名();
        using(var R = new Client(
                  Dns.GetHostName(),
                  ListenerSocketポート番号)) {
            for(var a = 1;a<300;a++) {
                var value = (byte)a;
                R.ByteEofSendReceive(value);
            }
        }
        this.Close();
        this.Open();
        using(var R = new Client(
                  Dns.GetHostName(),
                  ListenerSocketポート番号)) { R.ByteEofSendReceive(3); }
    }
    [TestMethod]
    public void 入力スカラ値0() {
        this.メソッド名();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        var b = 0;
        R.Expression(() => b);
    }
    private static void Private入力スカラ値2(Client R,int b) {
        R.Expression(() => b);
    }
    [TestMethod]
    public void 入力スカラ値2() {
        this.メソッド名();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        var b = 0;
        for(var a = 0;a<1;a++) {
            Private入力スカラ値2(R,b);
        }
    }
    [TestMethod]
    public void 入力スカラ値3() {
        this.メソッド名();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        for(var a = 0;a<100;a++) {
            var a1 = a;
            var actual = R.Expression(() => a1);
            Assert.AreEqual(a,actual);
        }
    }
    private static void 入力サイズ(int 回数) {
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        for(var a = 0;a<回数;a++) {
            var expected = new int[a];
            var actual = R.Expression(() => expected);
            Assert.AreEqual(expected.Length,actual.Length);
        }
    }
    [TestMethod]
    public void 入力サイズ010() => 入力サイズ(10);
    [TestMethod]
    public void 入力サイズ100() => 入力サイズ(100);
    [TestMethod]
    public void 色んなサイズのシリアライズを送信() {
        this.メソッド名();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        {
            var expected = new byte[475];
            for(var b = 0;b<expected.Length;b++) {
                expected[b]=(byte)b;
            }
            var actual = R.SerializeSendReceive(expected,XmlType.Utf8Json);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
        {
            var expected = new byte[1];
            for(var b = 0;b<expected.Length;b++) {
                expected[b]=(byte)b;
            }
            var actual = R.SerializeSendReceive(expected,XmlType.Utf8Json);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
    private static readonly Optimizer.ExpressionEqualityComparer ExpressionEqualityComparer=new(new List<ParameterExpression>());
    private static void シリアライズ<T>(T expected)where T:Expression{
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        var actual = R.SerializeSendReceive(expected,XmlType.Utf8Json);
        //var actual = R.Expression(()=>expected);
        //Debug.Assert(expected!=null,nameof(expected)+" != null");
        //Assert.IsTrue(expected.Equals(actual));
        Assert.IsTrue(ExpressionEqualityComparer.Equals(expected,actual));
    }
    [TestMethod]
    public void シリアライズConstant()=>シリアライズ(Expression.Constant("abc"));
    //[TestMethod]
    //public void シリアライズstring()=>シリアライズ("abc");
    private void シリアライズを送信(int a) {
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        var expected = new byte[a];
        for(var b = 0;b<a;b++) {
            expected[b]=(byte)b;
        }
        var actual = R.SerializeSendReceive(expected,XmlType.Utf8Json);
        Assert.IsTrue(expected.SequenceEqual(actual));
    }
    [TestMethod]
    public void シリアライズを送信0000() => this.シリアライズを送信(0);
    [TestMethod]
    public void シリアライズを送信0001() => this.シリアライズを送信(1);
    [TestMethod]
    public void シリアライズを送信0008() => this.シリアライズを送信(8);
    [TestMethod]
    public void シリアライズを送信0016() => this.シリアライズを送信(16);
    [TestMethod]
    public void シリアライズを送信0064() => this.シリアライズを送信(64);
    [TestMethod]
    public void シリアライズを送信0256() => this.シリアライズを送信(256);
    [TestMethod]
    public void シリアライズを送信0512() => this.シリアライズを送信(512);
    [TestMethod]
    //        public void シリアライズを送信0879() => this.シリアライズを送信(880);
    public void シリアライズを送信0879() => this.シリアライズを送信(512+256+64+32+8+4+2+1);
    //public void シリアライズを送信1023() => this.シリアライズを送信(512+256+64+32+8+4+2+1);
    [TestMethod]
    public void シリアライズを送信1024() => this.シリアライズを送信(1024);
    [TestMethod] public void Byte1を送信0() => this.Byte1を送信(0);
    [TestMethod] public void Byte1を送信1() => this.Byte1を送信(1);
    [TestMethod]
    public void Byte1を送信2() => this.Byte1を送信(2);
    [TestMethod]
    public void Byte1を送信3() => this.Byte1を送信(3);
    [TestMethod]
    public void Byte1を送信N() {
        this.メソッド名();
        this.Byte1を送信(100);
    }
    private static void Bytesを1接続でNバイトM回(int N,int M) {
        using var R = new Client(
            Dns.GetHostName(),
            ListenerSocketポート番号);
        for(var a = 0;a<M;a++) {
            R.BytesSendReceive(N);
        }
    }
    [TestMethod] public void Binary0バイト2回() => Bytesを1接続でNバイトM回(0,2);
    [TestMethod] public void Binary1バイト2回() => Bytesを1接続でNバイトM回(1,2);
    [TestMethod] public void Binary2バイト2回() => Bytesを1接続でNバイトM回(2,2);
    [TestMethod] public void Binary0バイト3回() => Bytesを1接続でNバイトM回(0,3);
    [TestMethod] public void Binary1バイト3回() => Bytesを1接続でNバイトM回(1,3);
    [TestMethod] public void Binary2バイト3回() => Bytesを1接続でNバイトM回(2,3);
    private void Bytesを送信(int a) {
        using var R = new Client(
            Dns.GetHostName(),
            ListenerSocketポート番号);
        R.BytesSendReceive(a);
    }
    [TestMethod]
    public void Bytesを送信0000() => this.Bytesを送信(0);
    [TestMethod]
    public void Bytesを送信0001() => this.Bytesを送信(1);
    [TestMethod]
    public void Bytesを送信0002() => this.Bytesを送信(2);
    [TestMethod]
    public void Bytesを送信0003() => this.Bytesを送信(3);
    [TestMethod]
    public void Bytesを送信0010() => this.Bytesを送信(10);
    [TestMethod]
    public void Bytesを送信0100() => this.Bytesを送信(100);
    [TestMethod]
    public void Bytesを送信1000() => this.Bytesを送信(1000);
    [TestMethod]
    public void Bytesを送信2000() => this.Bytesを送信(2000);
    //[TestMethod]
    //public void Byteを可変長で送受信して探索テスト() {
    //    using(var R = new global::Lite.Remotes.Remote(Dns.GetHostName(),Fポート)) {
    //        for(var c = 1;c<65536;c<<=1) {
    //            for(var b = 0;b<=c;b++) {
    //                R.ByteSend0();
    //                for(var a = 0;a<b;a++) {
    //                    R.ByteSend1((Byte)a);
    //                }
    //                R.ByteSend2();
    //                R.ByteReceive0();
    //                for(var a = 0;a<b;a++) {
    //                    Assert.AreEqual((Byte)a,R.ByteReceive1());
    //                }
    //                R.ByteReceive2();
    //            }
    //        }
    //    }
    //}
    [TestMethod]
    public void 一回接続で複数回BinaryObjectを送受信() {
        this.メソッド名();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        for(var a = 0;a<100;a++) {
            var expected = (a/2).ToString();
            var actual = R.SerializeSendReceive(expected,XmlType.Utf8Json);
            Assert.AreEqual(expected,actual);
        }
    }
    [TestMethod]
    public void 一回接続で複数回XmlTextSerializeSendReceiveを送受信() {
        this.メソッド名();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        //var expected = "1234567890";
        var expected = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstu";
        {
            //                    var expected = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstu";
            var actual = R.XmlSendReceive(expected,XmlType.MessagePack);
            Assert.AreEqual(expected,actual);
        }
        {
            var actual = R.XmlSendReceive(expected,XmlType.MessagePack);
            Assert.AreEqual(expected,actual);
        }
    }
    [TestMethod]
    public void 一回接続で複数回XmlBinarySerializeSendReceiveを送受信() {
        this.メソッド名();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        //var expected = "1234567890";
        var expected = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstu";
        {
            //                    var expected = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstu";
            var actual = R.XmlSendReceive(expected);
            Assert.AreEqual(expected,actual);
        }
        {
            var actual = R.XmlSendReceive(expected);
            Assert.AreEqual(expected,actual);
        }
    }
    [TestMethod]
    public void 一回接続で複数回XmlBinarySerializeSendReceiveを送受信100() {
        this.メソッド名();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        var expected = "7222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222";
        for(var a = 0;a<100;a++) {
            var actual = R.XmlSendReceive(expected);
            Assert.AreEqual(expected,actual);
        }
    }
    [TestMethod]
    public void 一回接続で1回Emptyを送受信() {
        this.メソッド名();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        R.EmptySendReceive();
    }
    [TestMethod]
    public void 一回接続で複数回Emptyを送受信() {
        this.メソッド名();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        for(var a = 1;a<100;a++) {
            R.EmptySendReceive();
        }
    }
    [TestMethod]
    public void 一回接続で複数回XmlByteN配列を送受信() {
        this.メソッド名();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        for(var a = 1;a<100;a++) {
            var expected = new byte[a];
            for(var b = 0;b<a;b++) {
                expected[b]=(byte)b;
            }
            var actual = R.XmlSendReceive(expected);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
    [TestMethod]
    public void 一回接続で複数回XmlByte0配列を送受信() {
        this.メソッド名();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        for(var a = 1;a<100;a++) {
            var expected = Array.Empty<byte>();
            var actual = R.XmlSendReceive(expected);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
    [TestMethod]
    public void 複数回接続で一回XmlByte配列を送受信() {
        //テスト メソッド カバレッジCS.カバレッジ.ServersFrontendClient.Test_Remote.複数回接続で一回XmlByte配列を送受信 が例外をスローしました:
        //System.IO.IOException: 転送接続にデータを書き込めません: 既存の接続はリモート ホストに強制的に切断されました。。 ---> 
        //System.Net.Sockets.SocketException: 
        //既存の接続はリモート ホストに強制的に切断されました。
        this.メソッド名();
        for(var a = 1;a<100;a++) {
            using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
            var expected = new byte[a];
            for(var b = 0;b<a;b++) {
                expected[b]=(byte)b;
            }
            var actual = R.XmlSendReceive(expected);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
    [TestMethod]
    public void バッファブロック1024バイトにEOFを含む512バイトを2つ送信() {
        this.メソッド名();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        var 送信0 = new byte[511];
        for(var a = 0;a<511;a++) {
            送信0[a]=1;
        }
        var 送信1 = new byte[511];
        for(var a = 0;a<511;a++) {
            送信1[a]=2;
        }
        Assert.IsTrue(R.XmlSendReceive(送信0).SequenceEqual(送信0));
        Assert.IsTrue(R.XmlSendReceive(送信1).SequenceEqual(送信1));
    }
    [TestMethod]
    public void 単一続単純バイト配列を送る() {
        this.メソッド名();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        for(var a = 1;a<100;a++) {
            var 送信 = new byte[a];
            for(var b = 0;b<a;b++) {
                送信[b]=(byte)(b+1);
            }
            Trace.WriteLine("単一続単純バイト配列を送る:"+a);
            var 受信 = R.XmlSendReceive(送信);
            Assert.IsTrue(送信.SequenceEqual(受信));
        }
    }
    [TestMethod]
    public void 複接続単純バイト配列を送る() {
        //テスト メソッド カバレッジCS.カバレッジ.ServersFrontendClient.Test_Remote.Connect複数接続し送受信を繰り返す が例外をスローしました:
        //System.IO.IOException: 転送接続にデータを書き込めません: 確立された接続がホスト コンピューターのソウトウェアによって中止されました。。 ---> 
        //System.Net.Sockets.SocketException: 
        //確立された接続がホスト コンピューターのソウトウェアによって中止されました。
        this.メソッド名();
        for(var a = 1;a<100;a++) {
            var 送信 = new byte[a];
            for(var b = 0;b<a;b++) {
                送信[b]=(byte)(b+1);
            }
            Trace.Write(a);
            using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
            var 受信 = R.XmlSendReceive(送信);
            Assert.IsTrue(送信.SequenceEqual(受信));
        }
    }
    [TestMethod]
    public void Connect複数接続し送受信を繰り返す() {
        //テスト メソッド カバレッジCS.カバレッジ.ServersFrontendClient.Test_Remote.複接続単純バイト配列を送る が例外をスローしました:
        //System.IO.IOException: 転送接続からデータを読み取れません: 既存の接続はリモート ホストに強制的に切断されました。。 ---> 
        //System.Net.Sockets.SocketException: 
        //既存の接続はリモート ホストに強制的に切断されました。
        this.メソッド名();
        for(var a = 1;a<100;a++) {
            Trace.Write(a);
            using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
            var expected = new byte[a];
            for(var b = 0;b<a;b++) {
                expected[b]=(byte)b;
            }
            var actual = R.XmlSendReceive(expected);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
    [TestMethod]
    public void Connect1接続し送受信を繰り返す() {
        this.メソッド名();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        for(var a = 1;a<100;a++) {
            Trace.Write(a);
            var expected = new byte[a];
            for(var b = 0;b<a;b++) {
                expected[b]=(byte)b;
            }
            var actual = R.XmlSendReceive(expected);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
    [TestMethod]
    public void 同値を送信内部でのエラーでもエラー処理されるか() {
        this.メソッド名();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        for(var a = 1;a<100;a++) {
            var expected = new byte[] { 111 };
            var actual = R.XmlSendReceive(expected);
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
    private static bool TimeoutReturn(int ms) {
        Thread.Sleep(ms);
        return true;
    }
    [TestMethod]
    public void ソケットのタイムアウト() {
        this.メソッド名();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        R.Expression(() => TimeoutReturn(100));
    }
    [TestMethod]
    public void Set1のシリアライズとでシリアライズ() {
        this.メソッド名();
        var expected = new Set<int> { 1,2,3 };
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        var actual = R.Expression(() => expected);
        Assert.IsTrue(expected.Equals(actual));
    }
    [TestMethod]
    public void Set2のシリアライズとでシリアライズ() {
        this.メソッド名();
        var expected = new Set<Tables.Entity,PrimaryKeys.Entity,Container>(default!) {
            new(1,1,1),new(2,2,2),new(3,3,3)
        };
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        var actual = R.Expression(() => expected);
        Assert.IsTrue(expected.Equals(actual));
    }
    [TestMethod]
    public void 初期化を繰り返す() {
        this.メソッド名();
        for(var a = 0;a<10;a++) {
            var S = Server.Create("",1,ListenerSocketポート番号);
            //var Logic = new Logic(this.Server);
            S.Open();
            //Logic.Open();
            //Logic.Dispose();
            S.Dispose();//RequestResponseSingleReceiveSends.DisposeするのでLogic.DisposeのCancelationTokenSource.Cancel()が失敗する。
        }
    }
    [TestMethod]
    public void 初期化を2度() {
        this.メソッド名();
        var S = Server.Create("",1,ListenerSocketポート番号);
        //var Logic = new Logic(this.Server);
        S.Open();
        //Logic.Open();
        //Logic.Dispose();
        S.Dispose();//RequestResponseSingleReceiveSends.DisposeするのでLogic.DisposeのCancelationTokenSource.Cancel()が失敗する。
    }
    [TestMethod]
    public void サーバーで式木を実行する() {
        this.メソッド名();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        // var actual0 = R.Call(() => new { a = 1 });
        var actual1 = R.Expression(() => new { a = 1 });
        var expected = 1;
        var actual = R.Expression(() => expected);
    }

    [TestMethod]
    public void シリアライズを送信Target() {
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        var types = new Type[50];
        for(var a = 0;a<50;a++)
            types[a]=typeof(object);
        //var T=
        //    new Target
        //    <Expression,Object,Object,Object,Object,Object,Object,Object,Object,Object,Object,Object,Object,
        //        Object,Object,Object,Object,Object,Object,Object,Object,Object,Object,Object,Object,Object,
        //        Object,Object,Object,Object,Object,Object,Object,Object,Object,Object,Object,Object,Object,
        //        Object,Object,Object,Object,Object,Object,Object,Object,Object,Object,Object>();
        //var Type=T.GetType();
        var Type = typeof(Target1<
                Expression,object,object,object,object,object,object,object,object,object,
                object,object,object,object,object,object,object,object,object,object
                //Object,Object,Object,Object,Object,Object,Object,Object,Object,Object,
                //Object,Object,Object,Object,Object,Object,Object,Object,Object,Object,
                //              Object,Object,Object,Object,Object,Object,Object,Object,Object,Object
            >
        );
        //var Type=T.GetType();
        //Type = typeof(Target<Expression, Object>);
        var actual = R.SerializeSendReceive(Type.GetFields(BindingFlags.Instance|BindingFlags.Public),XmlType.Utf8Json);
    }
}
[Serializable]
public sealed class Target1<
    T00, T01, T02, T03, T04, T05, T06, T07, T08, T09,
    T10, T11, T12, T13, T14, T15, T16, T17, T18, T19
    //T20, T21, T22, T23, T24, T25, T26, T27, T28, T29,
    //T30, T31, T32, T33, T34, T35, T36, T37, T38, T39,
    //T40, T41, T42, T43, T44, T45, T46, T47, T48, T49
> {
    public T00 v00;
    public T01 v01;
    public T02 v02;
    public T03 v03;
    public T04 v04;
    public T05 v05;
    public T06 v06;
    public T07 v07;
    public T08 v08;
    public T09 v09;
    public T10 v10;
    public T11 v11;
    public T12 v12;
    public T13 v13;
    public T14 v14;
    public T15 v15;
    public T16 v16;
    public T17 v17;
    public T18 v18;
    public T19 v19;
    public object this[int index] {
        // ReSharper disable once ValueParameterNotUsed
        set {
            switch(index) {
                //case 0: v00=(T00)value; break;
                //case 1: v01=(T01)value; break;
                //case 2: v02=(T02)value; break;
                //case 3: v03=(T03)value; break;
                //case 4: v04=(T04)value; break;
                //case 5: v05=(T05)value; break;
                //case 6: v06=(T06)value; break;
                //case 7: v07=(T07)value; break;
                //case 8: v08=(T08)value; break;
                //case 9: v09=(T09)value; break;
                //case 10: v10=(T10)value; break;
                //case 11: v11=(T11)value; break;
                //case 12: v12=(T12)value; break;
                //case 13: v13=(T13)value; break;
                //case 14: v14=(T14)value; break;
                //case 15: v15=(T15)value; break;
                //case 16: v16=(T16)value; break;
                //case 17: v17=(T17)value; break;
                //case 18: v18=(T18)value; break;
                //case 19: v19=(T19)value; break;
                //case 20: v20=(T20)value; break;
                //case 21: v21=(T21)value; break;
                //case 22: v22=(T22)value; break;
                //case 23: v23=(T23)value; break;
                //case 24: v24=(T24)value; break;
                //case 25: v25=(T25)value; break;
                //case 26: v26=(T26)value; break;
                //case 27: v27=(T27)value; break;
                //case 28: v28=(T28)value; break;
                //case 29: v29=(T29)value; break;
                //case 30: v30=(T30)value; break;
                //case 31: v31=(T31)value; break;
                //case 32: v32=(T32)value; break;
                //case 33: v33=(T33)value; break;
                //case 34: v34=(T34)value; break;
                //case 35: v35=(T35)value; break;
                //case 36: v36=(T36)value; break;
                //case 37: v37=(T37)value; break;
                //case 38: v38=(T38)value; break;
                //case 39: v39=(T39)value; break;
                //case 40: v40=(T40)value; break;
                //case 41: v41=(T41)value; break;
                //case 42: v42=(T42)value; break;
                //case 43: v43=(T43)value; break;
                //case 44: v44=(T44)value; break;
                //case 45: v45=(T45)value; break;
                //case 46: v46=(T46)value; break;
                //case 47: v47=(T47)value; break;
                //case 48: v48=(T48)value; break;
                //case 49: v49=(T49)value; break;
                default: throw new IndexOutOfRangeException("Target2["+index+"]は範囲外");
            }
        }
    }
}