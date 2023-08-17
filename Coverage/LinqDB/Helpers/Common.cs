using LinqDB.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable AssignNullToNotNullAttribute
namespace CoverageCS.LinqDB.Helpers;

[TestClass]
public class Test_CommonLibrary {
    [TestMethod]
    public void Sizeof()
    {
        Assert.AreEqual(IntPtr.Size,CommonLibrary.Sizeof(typeof(string)));
        Assert.AreEqual(8,CommonLibrary.Sizeof(typeof(long)));
    }
    [TestMethod]
    public void スレッド実行()
    {
        const int 回数 = 10000;
        CommonLibrary.スレッド実行("スレッド1", true, () =>
        {
            for (var a = 0; a < 回数; a++)
                Console.WriteLine(@"スレッド1," + a);
        });
        CommonLibrary.スレッド実行("スレッド2", true, () =>
        {
            for (var a = 0; a < 回数; a++)
                Console.WriteLine(@"スレッド2," + a);
        });
    }
}