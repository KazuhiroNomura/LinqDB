﻿
namespace LinqDB.Remote.Servers;

public class Server<T>:Server{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="ServerT"></param>
    /// <param name="ReceiveSendスレッド数"></param>
    /// <param name="ListenPorts"></param>
    public Server(T ServerT,int ReceiveSendスレッド数,params int[] ListenPorts):base(ServerT,ReceiveSendスレッド数,ListenPorts) {
    }
}