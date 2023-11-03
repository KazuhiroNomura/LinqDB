namespace LinqDB.Helpers;

/// <summary>
/// BackendとFrontendでの共通ルーチンと定数。
/// </summary>
public static class Configulation{
    /// <summary>
    /// Remote.ConnectSocketのReceiveバイト数
    /// </summary>
    public const int ClientReceiveBufferSize = 102;
    /// <summary>
    /// Remote.ConnectSocketのReceiveバイト数
    /// </summary>
    public const int ClientSendBufferSize = 102;
    ///// <summary>
    ///// Remote.MemoryStreamのバイト数。足りないと「メモリ ストリームは展開不可能です。」
    ///// </summary>
    //public const int ClientMemoryStreamBufferSize = 1<<16;
    /// <summary>
    /// Server.AcceptSocketのReceiveバイト数
    /// </summary>
    public const int ServerReceiveBytes = 102;
    /// <summary>
    /// Server.AcceptSocketのReceiveバイト数
    /// </summary>
    public const int ServerSendBytes = 102;
    /// <summary>
    /// サーバーのMemoryStreamのバイト数。足りないと「リモート先で例外が発生した。」
    /// </summary>
    public const int MemoryStreamBufferSize = 1<<20;
    /// <summary>
    /// WCFBackendのSocketポート番号
    /// </summary>
    public const int ListenerSocketポート番号 = 8888;
    /// <summary>
    /// スレッド、ソケットの既定のタイムアウト。
    /// </summary>
    public const int 既定のタイムアウト = -1;//1000*1000;
}