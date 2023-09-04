using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using static LinqDB.Helpers.CommonLibrary;
namespace LinqDB.Remote.Servers;

/// <summary>
/// 1つのListenerSocketからAcceptした複数のSocketをMultiReceiveSendで実行させる
/// </summary>
[DebuggerDisplay("Accept状態 ="+nameof(Accept状態)+"}")]
internal class SingleAcceptReceiveSend:IDisposable {
    private const int Listen数 = 100;
    private Socket ?ListenerSocket;
    internal readonly MultiReceiveSend MultiReceiveSend;
    //public readonly Int32 Index;
    public Async状態 Accept状態 = Async状態.None;
    private readonly SocketAsyncEventArgs AcceptEventArgs = new();
    private readonly ManualResetEvent AcceptManualResetEvent = new(true);
    /// <summary>
    /// 受信処理のタイムアウト。管理しているすべてのSingleReceiveSendに対して適用する。
    /// </summary>
    public int ReadTimeout {
        get => this.MultiReceiveSend.ReadTimeout;
        set => this.MultiReceiveSend.ReadTimeout=value;
    }
    /// <summary>
    /// 送信処理のタイムアウト。管理しているすべてのSingleReceiveSendに対して適用する。
    /// </summary>
    public int WriteTimeout {
        get => this.MultiReceiveSend.WriteTimeout;
        set => this.MultiReceiveSend.WriteTimeout=value;
    }
    //internal Logic Logic=>this.Server.Logic;
    internal X509Certificate? X509Certificate =>this.Server.X509Certificate;
    private readonly Server Server;
    private readonly IPEndPoint IPEndPoint;
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="Server"></param>
    /// <param name="ReceiveSendスレッド数"></param>
    /// <param name="ListenPort"></param>
    public SingleAcceptReceiveSend(Server Server,int ReceiveSendスレッド数,int ListenPort){
        this.Server=Server;
        this.IPEndPoint=new IPEndPoint(System.Net.IPAddress.Any,ListenPort);
        Debug.WriteLine("7");
        this.MultiReceiveSend=new MultiReceiveSend(Server,ReceiveSendスレッド数);
        Debug.WriteLine("8");
        this.AcceptEventArgs.Completed+=this.AcceptCompleted;
    }
    private CancellationToken CancellationToken;
    /// <summary>
    /// ListnerSocketをAcceptする。
    /// AcceptしたらMultiReceiveSend.Startする。
    /// 次のAcceptする。
    /// </summary>
    /// <param name="CancellationToken"></param>
    public void Open(CancellationToken CancellationToken){
        this.CancellationToken=CancellationToken;
        var ListenerSocket = this.ListenerSocket=new Socket(
            System.Net.Sockets.AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp);
        ListenerSocket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.ReuseAddress,true);
        ListenerSocket.Bind(
            this.IPEndPoint
        );
        ListenerSocket.Listen(Listen数);
        var e = this.AcceptEventArgs;
        this.Accept状態=Async状態.AcceptAsync中;
        this.AcceptManualResetEvent.Reset();
        this.Server.CountdownEvent.AddCount();
        while(true){
            if(this.ListenerSocket.AcceptAsync(e)){
                Trace_WriteLine(1,MethodBase.GetCurrentMethod()!.Name+C+AcceptAsync+C+Async);
                return;
            }
            Trace_WriteLine(2,MethodBase.GetCurrentMethod()!.Name+C+AcceptAsync+C+Sync);
            Trace_WriteLine(3,MethodBase.GetCurrentMethod()!.Name+C+e.SocketError);
            if(e.SocketError==SocketError.Success){
                try{
                    Debug.Assert(e.AcceptSocket is not null);
                    this.MultiReceiveSend.Open(e.AcceptSocket!,CancellationToken);
                } catch(OperationCanceledException){
                    goto 終了;
                } finally{
                    e.AcceptSocket=null;
                }
            } else{
                break;
            }
        }
        Trace_WriteLine(4,MethodBase.GetCurrentMethod()!.Name+C+AcceptAsync+C+Sync);
    終了:
        this.Accept状態=Async状態.None;
        this.AcceptManualResetEvent.Set();
        this.Server.CountdownEvent.Signal();
        Trace_WriteLine(4,MethodBase.GetCurrentMethod()!.Name+"終了");
    }
    private const char C=' ';
    private const string AcceptAsync=nameof(AcceptAsync);
    private const string Async = nameof(Async);
    private const string Sync = nameof(Sync);
    private void AcceptCompleted(object? sender,SocketAsyncEventArgs e) {
        Trace_WriteLine(1,MethodBase.GetCurrentMethod()!.Name+C+e.LastOperation);
        while(e.SocketError==SocketError.Success) {
            Trace_WriteLine(2,MethodBase.GetCurrentMethod()!.Name+C+e.SocketError);
            try{
                this.MultiReceiveSend.Open(e.AcceptSocket!,this.CancellationToken);
            } catch(OperationCanceledException){
                Trace_WriteLine(3,MethodBase.GetCurrentMethod()!.Name+C+AcceptAsync+nameof(OperationCanceledException));
                goto 終了;
            } catch(Exception ex) {
                Trace_WriteLine(4,MethodBase.GetCurrentMethod()!.Name+C+AcceptAsync+ex.ToString());
                throw;
            } finally {
                e.AcceptSocket=null;
            }
            try {
                if(this.ListenerSocket!.AcceptAsync(e)){
                    Trace_WriteLine(5,MethodBase.GetCurrentMethod()!.Name+C+AcceptAsync+C+Async);
                    return;
                }
                Trace_WriteLine(6,MethodBase.GetCurrentMethod()!.Name+C+AcceptAsync+C+Sync);
            } catch(ObjectDisposedException ex) {
                Trace_WriteLine(7,MethodBase.GetCurrentMethod()!.Name+C+AcceptAsync+C+ex);
                goto 終了;
            } catch(Exception ex) {
                Trace_WriteLine(8,MethodBase.GetCurrentMethod()!.Name+C+AcceptAsync+ex.ToString());
                throw;
            }
        }
        Trace_WriteLine(9,MethodBase.GetCurrentMethod()!.Name+C+e.SocketError);
    終了:
        this.AcceptManualResetEvent.Set();
        this.Server.CountdownEvent.Signal();
        Trace_WriteLine(10,MethodBase.GetCurrentMethod()!.Name+"終了");
    }
    /// <summary>
    /// スレッド終了を開始させる
    /// </summary>
    public void BeginClose() {
        this.ListenerSocket!.Close();
        this.MultiReceiveSend.BeginClose();
    }
    /// <summary>
    /// BeginCloseによるスレッド終了が完了するまで待つ
    /// </summary>
    public void EndClose(){
        this.MultiReceiveSend.EndClose();
        this.AcceptManualResetEvent.WaitOne();
    }
    /// <summary>
    /// 同期でスレッドを終了させる
    /// </summary>
    public void Close() {
        this.BeginClose();
        this.AcceptManualResetEvent.WaitOne();
    }
    /// <summary>
    /// ファイナライザ
    /// </summary>
    ~SingleAcceptReceiveSend() => this.Dispose(false);
    /// <summary>アンマネージ リソースの解放またはリセットに関連付けられているアプリケーション定義のタスクを実行します。</summary>
    /// <filterpriority>2</filterpriority>
    public void Dispose() {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }
    /// <summary>
    /// Disposeされているか
    /// </summary>
    public bool IsDisposed {get; private set;}
    private void Dispose(bool disposing) {
        if(!this.IsDisposed) {
            this.IsDisposed=true;
            if(disposing) {
                this.Close();
                this.AcceptEventArgs.Dispose();
                this.AcceptManualResetEvent.Dispose();
                Debug.Assert(this.ListenerSocket is not null);
                this.ListenerSocket!.Dispose();
            }
        }
    }
}