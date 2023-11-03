namespace LinqDB;
/// <summary>
/// クライアント、サーバーがどのような形式で送信するか。
/// </summary>
public enum SerializeType:byte {
    /// <summary>
    /// Utf8Json.JsonSerializer
    /// </summary>
    Utf8Json,
    /// <summary>
    /// MessagePack.MessagePackSerializer
    /// </summary>
    MessagePack,
    /// <summary>
    /// MemoryPack.MemoryPackSerializer
    /// </summary>
    MemoryPack
}
