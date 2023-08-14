using System;
using LinqDB.Properties;
namespace LinqDB.Databases;

/// <summary>
/// Check制約時の例外
/// </summary>
[Serializable]
public sealed class ValidateException:Exception {
    /// <summary>
    /// 既定コンストラクタ
    /// </summary>
    public ValidateException()
        : base(Resources.制約エラー) {
    }
}