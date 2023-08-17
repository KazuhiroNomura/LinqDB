using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinqDB.Remote.Clients;
using static LinqDB.Helpers.Configulation;
namespace CoverageCS.LinqDB.Servers;

[TestClass]
public class Server
{
    //L:Logic
    //M:MUltiAcceptReceiveSend
    //R:Remote
    //S:SendReceive
    private static void SLCReadTimeout(int M数, int L数, int R数) => MLR(M数, -1, -1, L数, R数, -1, 1);
    private static void SLCWriteTimeout(int M数, int L数, int R数) => MLR(M数, -1, -1, L数, R数, 1, -1);
    private static void MReadTimeoutLR(int M数, int L数, int R数) => MLR(M数, -1, 1, L数, R数, -1, -1);
    private static void MWriteTimeoutLR(int M数, int L数, int R数) => MLR(M数, 1, -1, L数, R数, -1, -1);
    private static void SLC(int M数, int L数, int R数) => MLR(M数, -1, -1, L数, R数, -1, -1);
    private static bool TimeoutReturn(int ms) {
        Thread.Sleep(ms);
        return true;
    }
    private static void MLR(int M数, int M_WriteTimeout, int M_ReadTimeout, int L数, int R数, int R_WriteTimeout, int R_ReadTimeout){
        for (var a = 0; a < M数; a++){
            using var S = new global::LinqDB.Remote.Servers.Server(1, 1,ListenerSocketポート番号);
            S.ReadTimeout = M_ReadTimeout;
            S.WriteTimeout = M_WriteTimeout;
            for (var b = 0; b < M数; b++){
                S.Open();
                for (var c = 0; c < L数; c++){
                    for (var d = 0; d < L数; d++){
                        for (var e = 0; e < R数; e++){
                            using var R = new Client {
                                DnsEndPoint = new DnsEndPoint("localhost", ListenerSocketポート番号),
                                ReadTimeout = R_ReadTimeout,
                                WriteTimeout = R_WriteTimeout
                            };
                            for (var f = 0; f < R数; f++){
                                R.Expression(() => TimeoutReturn(1000));
                            }
                        }
                    }
                }
                S.Close();
            }
        }
    }
    [TestMethod]
    public void ReceiveAsync(){
        const int 試行回数 = 1;
        //    try{
        //        switch(e.SocketError) {
        //            case SocketError.Success: {
        //                if(e_BytesTransferred==0) {
        //                }
        //                if(e_Offset_BytesTransferred==メモリブロックバイト数) {
        //                } else {
        //                    while(true) {
        //                        if(e_Buffer[index]==ConstantEscape.EOF) {
        //                        }
        //                        if(index<=e_Offset) {
        //                        }
        //                    }
        //                }
        //                if(this.Receive()) {//(0)
        //                }
        //            }
        //            default: {
        //                if(this.ReceiveとSendのCancellationToken.WaitHandle.WaitOne(0)) {
        //                }
        using (var S = new global::LinqDB.Remote.Servers.Server(1, 1,ListenerSocketポート番号)){
            S.Open();
            for (var a = 0; a <試行回数; a++){
                var C = new Client {
                    DnsEndPoint = new DnsEndPoint("localhost", ListenerSocketポート番号)
                };
                C.EmptySendReceive();//これがないと接続前にWCFFrontend.Dispose()してしまう。
            }
        }
        //            }
        //        }
        //    } catch(OperationCanceledException) {
        //    } catch(ObjectDisposedException) {//(0)
        //    } catch(Exception ex){
        //    }
        //Acceptする:
        //    try {
        //    } catch(ObjectDisposedException) {
        using (var S = new global::LinqDB.Remote.Servers.Server(1, 1,ListenerSocketポート番号)){
            S.Open();
            for (var a = 0; a <試行回数; a++){
                var C = new Client {
                    DnsEndPoint = new DnsEndPoint("localhost", ListenerSocketポート番号)
                };
                C.EmptySendReceive();//これがないと接続前にWCFFrontend.Dispose()してしまう。
            }
            S.Dispose();
        }
        //    }
    }
    [TestMethod] public void S1L1C1() => SLC(1, 1, 1);
    [TestMethod] public void S1L1C2() => SLC(1, 1, 2);
    [TestMethod] public void S1L1C3() => SLC(1, 1, 3);
    [TestMethod] public void S1L1C4() => SLC(1, 1, 4);
    [TestMethod] public void S1L2C1() => SLC(1, 2, 1);
    [TestMethod] public void S1L3C1() => SLC(1, 3, 1);
    [TestMethod] public void S1L4C1() => SLC(1, 4, 1);
    [TestMethod] public void S2L1C1() => SLC(2, 1, 1);
    [TestMethod] public void S3L1C1() => SLC(3, 1, 1);
    [TestMethod] public void S4L1C1() => SLC(4, 1, 1);
    [TestMethod] public void S2L2C2() => SLC(2, 2, 2);
    [TestMethod] public void S3L3C3() => SLC(3, 3, 3);
    [TestMethod] public void S4L4C4() => SLC(4, 4, 4);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S1L1C1ReadTimeout() => SLCReadTimeout(1, 1, 1);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S1L1C2ReadTimeout() => SLCReadTimeout(1, 1, 2);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S1L1C3ReadTimeout() => SLCReadTimeout(1, 1, 3);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S1L2C1ReadTimeout() => SLCReadTimeout(1, 2, 1);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S1L2C2ReadTimeout() => SLCReadTimeout(1, 2, 2);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S1L2C3ReadTimeout() => SLCReadTimeout(1, 2, 3);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S1L3C1ReadTimeout() => SLCReadTimeout(1, 3, 1);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S1L3C2ReadTimeout() => SLCReadTimeout(1, 3, 2);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S1L3C3ReadTimeout() => SLCReadTimeout(1, 3, 3);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S2L1C1ReadTimeout() => SLCReadTimeout(2, 1, 1);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S2L1C2ReadTimeout() => SLCReadTimeout(2, 1, 2);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S2L1C3ReadTimeout() => SLCReadTimeout(2, 1, 3);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S2L2C1ReadTimeout() => SLCReadTimeout(2, 2, 1);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S2L2C2ReadTimeout() => SLCReadTimeout(2, 2, 2);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S2L2C3ReadTimeout() => SLCReadTimeout(2, 2, 3);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S2L3C1ReadTimeout() => SLCReadTimeout(2, 3, 1);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S2L3C2ReadTimeout() => SLCReadTimeout(2, 3, 2);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S2L3C3ReadTimeout() => SLCReadTimeout(2, 3, 3);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S3L1C1ReadTimeout() => SLCReadTimeout(3, 1, 1);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S3L1C2ReadTimeout() => SLCReadTimeout(3, 1, 2);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S3L1C3ReadTimeout() => SLCReadTimeout(3, 1, 3);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S3L2C1ReadTimeout() => SLCReadTimeout(3, 2, 1);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S3L2C2ReadTimeout() => SLCReadTimeout(3, 2, 2);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S3L2C3ReadTimeout() => SLCReadTimeout(3, 2, 3);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S3L3C1ReadTimeout() => SLCReadTimeout(3, 3, 1);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S3L3C2ReadTimeout() => SLCReadTimeout(3, 3, 2);
    [TestMethod, ExpectedException(typeof(ReceiveException))] public void S3L3C3ReadTimeout() => SLCReadTimeout(3, 3, 3);

    [TestMethod] public void S3L3C3WriteTimeout() => SLCWriteTimeout(3, 3, 3);
    [TestMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability","CA2000:Dispose objects before losing scope",Justification = "<保留中>")]
    public void ランダムに起動する()
    {
        var r = new Random(1);
        /*
        using(var S = new global::Lite.Servers.Server(1,ListenerSocketポート番号,8000))
        using(var L = new Logic(S)) {
            L.Open();
            S.Open();
            var C = new global::Lite.Clients.Client {
                Endpoint=new DnsEndPoint("localhost",ListenerSocketポート番号,8000)
            };
            C.EmptySendReceive();//これがないと接続前にWCFFrontend.Dispose()してしまう。
        }
*/
        const int 試行回数 = 1000;
        var S = new global::LinqDB.Remote.Servers.Server(1, 1,ListenerSocketポート番号);
        //var L = new Logic(S);
        var C = new Client();
        for (var a = 0; a < 試行回数; a++)
        {
            var v = r.Next(7);
            Debug.WriteLine(v);
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (v)
            {
                case 0:
                    if (!S.IsDisposed)
                    {
                        // L.Dispose();
                        S.Dispose();
                        S = new global::LinqDB.Remote.Servers.Server(1, 1,ListenerSocketポート番号);
                        //L = new Logic(S);
                    }
                    break;
                case 1:
                    if (!C.IsDisposed)
                    {
                        C.Dispose();
                        C = new Client();
                    }
                    break;
                case 2:
                    if (S.IsRunning)
                    {
                        try
                        {
                            C.DnsEndPoint = new DnsEndPoint("localhost", ListenerSocketポート番号);
                        }
                        catch (SocketException)
                        {

                        }
                    }
                    break;
                case 3:
                    if (S.IsRunning){
                        if(C.DnsEndPoint==null) continue;
                        try
                        {
                            C.EmptySendReceive();
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }
                    break;
                case 4:
                    if (S.IsRunning)
                    {
                        if(C.DnsEndPoint==null) continue;
                        C.BytesSendReceive(10);
                    }
                    break;
                case 5:
                    if (!S.IsRunning && !S.IsDisposed)
                    {
                        S.Open();
                    }
                    break;
                case 6:
                    if (S.IsRunning && !S.IsDisposed)
                    {
                        S.Close();
                    }
                    break;
            }
        }
    }

    [TestMethod]
    public void AcceptAsync(){
        //try {
        //    switch(e.SocketError) {
        //        case SocketError.Success: {
        //            try {
        //                if(this.Receive()) {//(1)
        //                    try {
        //                        try {
        //                            if(!this.Send()) {
        //                            }
        using var S = new global::LinqDB.Remote.Servers.Server(1, 1,ListenerSocketポート番号);
        //L.Open();
        S.Open();
        using var C = new Client {
            DnsEndPoint = new DnsEndPoint("localhost", ListenerSocketポート番号)
        };
        C.EmptySendReceive();//これがないと接続前にWCFFrontend.Dispose()してしまう。
    }
    [TestMethod]
    public void SendAsync()
    {
        using (var S = new global::LinqDB.Remote.Servers.Server(1, 1,ListenerSocketポート番号))
            //using (var L = new Logic(S))
        {
            //L.Open();
            S.Open();
            using (var C = new Client())
            {
                C.DnsEndPoint = new DnsEndPoint("localhost", ListenerSocketポート番号);
                C.Expression(() => Enumerable.Range(0, 10000).ToArray());
            }
        }
        using (var S = new global::LinqDB.Remote.Servers.Server(1, 1,ListenerSocketポート番号))
            //using (var L = new Logic(S))
        {
            //L.Open();
            S.Open();
            using (var C = new Client())
            {
                C.DnsEndPoint = new DnsEndPoint("localhost", ListenerSocketポート番号);
                C.Expression(() => Enumerable.Range(0, 10000).ToArray());
            }
            //L.Dispose();
            S.Dispose();
        }
    }
    [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
    public void Receive_大きすぎException(){
        using var S = new global::LinqDB.Remote.Servers.Server(1, 1,ListenerSocketポート番号);
        //L.Open();
        S.Open();
        using var C = new Client();
        C.DnsEndPoint = new DnsEndPoint("localhost", ListenerSocketポート番号);
        C.BytesSendReceive(ClientMemoryStreamBufferSize);
    }
}