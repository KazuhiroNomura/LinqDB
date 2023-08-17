using System.Collections.Concurrent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.作りたい仕様確認;

[TestClass]
public class プーリングとGCとどちらがスループットが良いかバッファ数1000000バイト数100 : Abstract_プーリングとGCとどちらがスループットが良いか
{
    protected override int バッファ数 => 1000000;
    protected override int バイト数 => 100;
}
[TestClass]
public class プーリングとGCとどちらがスループットが良いかバッファ数100000バイト数1000 : Abstract_プーリングとGCとどちらがスループットが良いか
{
    protected override int バッファ数 => 100000;
    protected override int バイト数 => 1000;
}
[TestClass]
public class プーリングとGCとどちらがスループットが良いかバッファ数10000バイト数10000 : Abstract_プーリングとGCとどちらがスループットが良いか
{
    protected override int バッファ数 => 10000;
    protected override int バイト数 => 10000;
}
public abstract class Abstract_プーリングとGCとどちらがスループットが良いか
{
    protected abstract int バッファ数 { get; }
    protected abstract int バイト数 { get; }
    private BlockingCollection<byte[]> BlockingCollection;
    private ConcurrentQueue<byte[]> ConcurrentQueue;
    [TestInitialize]
    public void TestInitialize()
    {
        var BlockingCollection = this.BlockingCollection = new BlockingCollection<byte[]>(this.バッファ数);
        var ConcurrentQueue = this.ConcurrentQueue = new ConcurrentQueue<byte[]>();
        for (var a = 0; a < this.バッファ数; a++)
        {
            BlockingCollection.Add(new byte[this.バイト数]);
            ConcurrentQueue.Enqueue(new byte[this.バイト数]);
        }
    }
    [TestMethod]
    public void GC確保()
    {
        for (var a = 0; a < this.バッファ数; a++)
        {
            // ReSharper disable once UnusedVariable
            var b = new byte[this.バイト数];
        }
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }
    [TestMethod]
    public void BlockingCollection確保()
    {
        var B = this.BlockingCollection;
        for (var a = 0; a < this.バッファ数; a++)
        {
            var b = B.Take();
            B.Add(b);
        }
    }
    [TestMethod]
    public void ConcurrentQueue確保()
    {
        var B = this.ConcurrentQueue;
        for (var a = 0; a < this.バッファ数; a++)
        {
            B.TryDequeue(out var result);
            B.Enqueue(result);
        }
    }
}