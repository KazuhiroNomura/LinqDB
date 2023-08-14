namespace LinqDB;

/// <summary>
/// クライアントから送信するヘッダー
/// </summary>
internal enum Response:byte {
    None,
    /// <summary>
    /// 0バイト送信する。
    /// </summary>
    Bytes0 = (byte)'A',
    /// <summary>
    /// Byteを送信する
    /// </summary>
    Byte=(byte)'B',
    /// <summary>
    /// 複数Byteを送信する。
    /// </summary>
    BytesN=(byte)'F',
    /// <summary>
    /// Objectを送信する。
    /// </summary>
    Object=(byte)'G',
    /// <summary>
    /// Throwして欲しいExceptionをシリアライズして送信
    /// </summary>
    ThrowException=(byte)'J'
}