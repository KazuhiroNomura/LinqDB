namespace LinqDB;
/// <summary>
/// クライアント、サーバーがどのような形式で送信するか。
/// </summary>
public enum XmlType:byte {
    /// <summary>
    /// 無効
    /// </summary>
    Head,
    /// <summary>
    /// Byte読み書き
    /// </summary>
    Native= Head,
    /// <summary>
    /// Utf8Json.JsonSerializer
    /// </summary>
    Utf8Json,
    /// <summary>
    /// MessagePack.JsonSerializer
    /// </summary>
    MessagePack,
    Tail= MessagePack
}
