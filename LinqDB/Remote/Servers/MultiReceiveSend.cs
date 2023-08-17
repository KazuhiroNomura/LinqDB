using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using static LinqDB.Helpers.Configulation;
namespace LinqDB.Remote.Servers;

/// <summary>
/// 複数のSingleReceiveSendを管理する
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Design","CA1001:Types that own disposable fields should be disposable",Justification = "<保留中>")]
internal class MultiReceiveSend {
    internal readonly SingleReceiveSend[] SingleReceiveSends;
    internal readonly BlockingCollection<int> 未使用のSingleReceiveSends;
    internal readonly Server Server;
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="Server"></param>
    /// <param name="ReceiveSendスレッド数"></param>
    public MultiReceiveSend(Server Server,int ReceiveSendスレッド数){
        this.Server=Server;
        var SingleReceiveSends=this.SingleReceiveSends=new SingleReceiveSend[ReceiveSendスレッド数];
        var 未使用のSingleReceiveSends=this.未使用のSingleReceiveSends = new BlockingCollection<int>(ReceiveSendスレッド数);
        for(var a = 0;a<ReceiveSendスレッド数;a++) {
            SingleReceiveSends[a]=new SingleReceiveSend(this,a);
            未使用のSingleReceiveSends.Add(a);
        }
    }

    //internal Logic Logic=>this.Server.Logic;
    internal X509Certificate? X509Certificate =>this.Server.X509Certificate;
    private int _ReadTimeout= 既定のタイムアウト;
    /// <summary>
    /// 受信処理のタイムアウト。管理しているすべてのSingleReceiveSendに対して適用する。
    /// </summary>
    public int ReadTimeout{
        get=>this._ReadTimeout;
        set{
            this._ReadTimeout=value;
            foreach(var a in this.SingleReceiveSends) {
                a.ReadTimeout=value;
            }
        }
    }
    private int _WriteTimeout = 既定のタイムアウト;
    /// <summary>
    /// 送信処理のタイムアウト。管理しているすべてのSingleReceiveSendに対して適用する。
    /// </summary>
    public int WriteTimeout {
        get => this._WriteTimeout;
        set {
            this._WriteTimeout=value;
            foreach(var a in this.SingleReceiveSends) {
                a.WriteTimeout=value;
            }
        }
    }

    private int _ReceiveBufferSize = 1024*1024;
    /// <summary>
    /// 受信バッファサイズ
    /// </summary>
    public int ReceiveBufferSize{
        get=>this._ReceiveBufferSize;
        set{
            this._ReceiveBufferSize=value;
            foreach(var a in this.SingleReceiveSends)
                a.ReceiveBufferSize=value;
        }
    }
    private int _SendBufferSize=1024*1024;
    /// <summary>
    /// 送信バッファサイズ
    /// </summary>
    public int SendBufferSize {
        get=>this._SendBufferSize;
        set{
            this._SendBufferSize=value;
            foreach(var a in this.SingleReceiveSends){
                a.SendBufferSize=value;
            }
        }
    }
    internal CancellationTokenSource? CancellationTokenSource;
    /// <summary>
    /// 開始する。SingleAcceptReceivedSendのAcceptから呼ばれる。
    /// </summary>
    /// <param name="Socket"></param>
    /// <param name="CancellationToken"></param>
    internal void Open(Socket Socket,CancellationToken CancellationToken){
        Socket.ReceiveTimeout=-1;//
        //this.RecieveTimeout;
        Socket.SendTimeout=-1;//            this.SendTimeout;
        //Socket.ReceiveBufferSize=this._ReceiveBufferSize;
        //Socket.SendBufferSize=this._SendBufferSize;
        var Index = this.未使用のSingleReceiveSends.Take(CancellationToken);
        try {
            this.SingleReceiveSends[Index].受信(Socket,CancellationToken);
        } catch(OperationCanceledException) {
            this.未使用のSingleReceiveSends.Add(Index,CancellationToken);
            throw;
        }
    }
    /// <summary>
    /// 全ての複数ReceiveAsync,SendAsyncの終了を開始させる。
    /// </summary>
    public void BeginClose(){
        if(this.CancellationTokenSource is not null){
            this.CancellationTokenSource.Cancel();
            foreach(var SingleReceiveSend in this.SingleReceiveSends){
                SingleReceiveSend.BeginClose();
            }
        }
    }
    /// <summary>
    /// BeginCloseによるスレッド終了が完了するまで待つ
    /// </summary>
    public void EndClose() {
        if(this.CancellationTokenSource is not null){
            this.CancellationTokenSource=null;
            foreach(var SingleReceiveSend in this.SingleReceiveSends){
                SingleReceiveSend.EndClose();
            }
        }
    }
}