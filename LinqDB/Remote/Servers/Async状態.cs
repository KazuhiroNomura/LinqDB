#pragma warning disable 1591
namespace LinqDB.Remote.Servers;

/// <summary>
/// 非同期状態を表す
/// </summary>
public enum Async状態 {
    None,
    ReceiveStart中,
    ReceiveAsync中,
    ReceiveComplete中,
    ReceiveTake中,
    SendStart中,
    SendComplete中,
    SendAsync中,
    SendAdd中,
    Accept中,
    AcceptAsync中,
    AcceptComplate中,
    DisconnectAsync中,
    DisconnectComplate中
}