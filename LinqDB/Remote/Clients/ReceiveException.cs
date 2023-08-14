using System;
using System.Globalization;
using LinqDB.Properties;

namespace LinqDB.Remote.Clients;

/// <summary>
/// 具体的な受信失敗例外。
/// </summary>
[Serializable]
public class ReceiveException:Exception {
    /// <summary>
    /// 既定コンストラクタ。
    /// </summary>
    /// <param name="受信済みバイト数"></param>
    /// <param name="残りバイト数"></param>
    /// <param name="innerException"></param>
    public ReceiveException(int 受信済みバイト数,int 残りバイト数,Exception innerException):base(
        string.Format(
            CultureInfo.CurrentCulture,
            Resources._バイト受信した_バイト受信できなかった!,
            受信済みバイト数.ToString(CultureInfo.CurrentCulture),
            残りバイト数.ToString(CultureInfo.CurrentCulture)
        ),
        innerException
    ) {
    }
}