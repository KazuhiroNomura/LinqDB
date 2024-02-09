using System.Reflection;

namespace LinqDB.Product.SQLServer;

public static class Throw
{
    /// <summary>
    /// TSQLでのRAISE ERRORで発生させる例外。
    /// </summary>
    [method:System.Diagnostics.CodeAnalysis.SuppressMessage("スタイル", "IDE0060:未使用のパラメーターを削除します", Justification = "<保留中>")]
    public sealed class ThrowException(int error_number,string message,int state):System.Exception(message){
        private readonly int error_number=error_number;
        private readonly int state=state;
    }
    public static readonly ConstructorInfo ctor = typeof(ThrowException).GetConstructor(
        new[] {
            typeof(int),
            typeof(string),
            typeof(int)
        }
    )!;
}