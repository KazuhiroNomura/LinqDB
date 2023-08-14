using System;
namespace LinqDB.Sets.Exceptions;

/// <summary>
/// タプルが重複した時の例外
/// </summary>
[Serializable]
public sealed class OneTupleException:Exception {
    /// <summary>
    /// エラーメッセージを指定したコンストラクタ
    /// </summary>
    /// <param name="message">エラーメッセージ</param>
    public OneTupleException(string message)
        : base(message+Environment.NewLine+"タプルの集合は一意であるべき") {
    }
}