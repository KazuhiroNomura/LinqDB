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
    /// Utf8Json.JsonSerializer
    /// </summary>
    Utf8Json=Head,
    /// <summary>
    /// MessagePack.MessagePackSerializer
    /// </summary>
    MessagePack,
    /// <summary>
    /// MemoryPack.MemoryPackSerializer
    /// </summary>
    MemoryPack,
    Tail= MessagePack
}
