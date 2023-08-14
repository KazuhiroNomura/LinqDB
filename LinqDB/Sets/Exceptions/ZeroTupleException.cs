using System;
namespace LinqDB.Sets.Exceptions;

/// <summary>
/// タプルが重複した時の例外
/// </summary>
[Serializable]
public sealed class ZeroTupleException:Exception {
    /// <summary>
    /// エラーメッセージを指定したコンストラクタ
    /// </summary>
    /// <param name="message">エラーメッセージ</param>
    public ZeroTupleException(string message)
        : base(message+Environment.NewLine+"タプルの集合は一意であるべき") {
    }
}