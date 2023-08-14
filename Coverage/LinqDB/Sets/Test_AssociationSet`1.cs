using CoverageCS.作りたい仕様確認.Set;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Sets
{
    [TestClass]
    public class AssociationSet1
    {
        [TestMethod]
        public void InternalAdd()
        {
            var e = new SetEntities();
            for (var a = 0; a < 100; a++)
            {
                var 多1_000 = new 作りたい仕様確認.Set.多1(a, a, a);
                var 多2_000 = new 作りたい仕様確認.Set.多2(a, a, a);
                e.多1.Add(多1_000);
                e.多2.Add(多2_000);
                Assert.IsTrue(e.多1多2.Add(new 多1多2(多1_000, 多2_000)));
                Assert.IsFalse(e.多1多2.Add(new 多1多2(多1_000, 多2_000)));
            }
        }
        [TestMethod]
        public void InternalRemove()
        {
            var e = new SetEntities();
            for (var a = 0; a < 100; a++)
            {
                var 多1_000 = new 作りたい仕様確認.Set.多1(a, a, a);
                var 多2_000 = new 作りたい仕様確認.Set.多2(a, a, a);
                e.多1.Add(多1_000);
                e.多2.Add(多2_000);
                Assert.IsTrue(e.多1多2.Add(new 多1多2(多1_000, 多2_000)));
            }
            for (var a = 0; a < 100; a++)
            {
                var 多1_000 = new 作りたい仕様確認.Set.多1(a, a, a);
                var 多2_000 = new 作りたい仕様確認.Set.多2(a, a, a);
                Assert.IsTrue(e.多1多2.Remove(new 多1多2(多1_000, 多2_000)));
                Assert.IsFalse(e.多1多2.Remove(new 多1多2(多1_000, 多2_000)));
            }
        }
    }
}
