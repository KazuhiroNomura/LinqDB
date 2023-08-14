namespace LinqDB;

/// <summary>
/// クライアントから送信するヘッダー
/// </summary>
internal enum Request:byte {
    None,
    /// <summary>
    /// 何も意味はない
    /// </summary>
    None_None =(byte)'X',
    /// <summary>
    /// 0バイト送信してNoneと0バイト受信したい。
    /// </summary>
    Bytes0_Bytes0=(byte)'A',
    /// <summary>
    /// Byteを送信してByteと1バイト受信したい。
    /// </summary>
    Byte_Byte=(byte)'B',
    /// <summary>
    /// CommunicateClient_ReceiveでOutOfMemoryExceptionを発生させる
    /// </summary>
    CommunicateClient_ReceiveでOutOfMemoryException=(byte)'C',
    /// <summary>
    /// WCFBackend_ReceiveでOutOfMemoryExceptionを発生させる
    /// </summary>
    リモート先でOutOfMemoryException=(byte)'D',
    /// <summary>
    /// Timeoutを送信するのでサーバーではTimeoutExceptionして欲しい。
    /// </summary>
    TimeoutException_ThrowException=(byte)'E',
    /// <summary>
    /// 複数Byteを送信してBytesNと複数Byteを受信したい。
    /// </summary>
    BytesN_BytesN = (byte)'F',
    /// <summary>
    /// オブジェクトを送信してObjectとオブジェクトを受信したい。
    /// </summary>
    Object_Object = (byte)'G',
    /// <summary>
    /// デリゲートを送信するのでサーバーではそれをInvokeして結果を受信したい。
    /// </summary>
    Delegate_Invoke=(byte)'H',
    /// <summary>
    /// 式木を送信するのでサーバーではそれをコンパイルしてInvokeして結果を受信したい。
    /// </summary>
    Expression_Invoke = (byte)'I',
    /// <summary>
    /// C#ソースを送信するのでサーバーではそれをコンパイルしてInvokeして結果を受信したい。
    /// </summary>
    CSharp_Invoke = (byte)'#',
    /// <summary>
    /// Throwして欲しいExceptionを送信するのでthrowを受信したい。
    /// </summary>
    Exception_ThrowException=(byte)'J'
}