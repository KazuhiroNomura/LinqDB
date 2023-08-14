using System;
namespace LinqDB.Databases;

/// <summary>
/// リレーションシップが満たされなくなった時の例外
/// </summary>
[Serializable]
public sealed class RelationshipException:Exception {
    /// <summary>
    /// エラーメッセージを指定したコンストラクタ
    /// </summary>
    /// <param name="message">エラーメッセージ</param>
    public RelationshipException(string message)
        : base(message) {
    }
}