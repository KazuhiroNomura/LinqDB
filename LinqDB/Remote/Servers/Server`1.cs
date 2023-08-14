//#define クエリは別プロセスで実行
//#define クエリは別スレッドで実行
#define クエリは同スレッドで実行
namespace LinqDB.Remote.Servers;

public class Server<T>:Server where T:class{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="ServerT"></param>
    /// <param name="ReceiveSendスレッド数"></param>
    /// <param name="ListenPorts"></param>
    public Server(T ServerT,int ReceiveSendスレッド数,params int[] ListenPorts):base(ServerT,new Proxy<T>(ServerT),ReceiveSendスレッド数,ListenPorts) {
    }
}