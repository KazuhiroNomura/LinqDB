using System.Reflection;

namespace LinqDB.Reflection;

public static class Exception {
    /// <summary>
    /// TSQLでのRAISE ERRORで発生させる例外。
    /// </summary>
    public sealed class RaiseErrorException:global::System.Exception {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("スタイル","IDE0060:未使用のパラメーターを削除します",Justification = "<保留中>")]
        public RaiseErrorException(string msg_id,int severity,int state,params object[]arguents) : base(msg_id) {
        }
    }
    public static readonly ConstructorInfo RaiseErrorException_ctor = typeof(RaiseErrorException).GetConstructor(
        new[] {
            typeof(string),
            typeof(int),
            typeof(int),
            typeof(object[]),
        }
    )!;
    public static readonly ConstructorInfo RelationshipException_ctor = typeof(Databases.RelationshipException).GetConstructor(
        new[] {typeof(string)}
    )!;
}