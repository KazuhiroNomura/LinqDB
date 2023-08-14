using System;
[Serializable]
class ReadonlyClass {
    public readonly int field;
    public ReadonlyClass(int field) => this.field=field;
}
[Serializable]
readonly struct ReadonlyStruct {
    public readonly int field;
    public ReadonlyStruct(int field) => this.field=field;
}
[Serializable]
ref struct RefStruct {
    public readonly int field;
    private Span<int> _span; //OK
    public RefStruct(int field) {
        this.field=field;
        this._span=new int[10];
    }
}
namespace LinqDB{
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
}
