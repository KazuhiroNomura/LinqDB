//using CoverageCS.作りたい仕様確認.Set;

//namespace CoverageCS.LinqDB.Sets
//{
    
//    public class AssociationSet1
//    {
//        [Fact]
//        public void InternalAdd()
//        {
//            var e = new SetEntities();
//            for (var a = 0; a < 100; a++)
//            {
//                var 多1_000 = new 作りたい仕様確認.Set.多1(a, a, a);
//                var 多2_000 = new 作りたい仕様確認.Set.多2(a, a, a);
//                e.多1.Add(多1_000);
//                e.多2.Add(多2_000);
//                Assert.IsTrue(e.多1多2.Add(new 多1多2(多1_000, 多2_000)));
//                Assert.False(e.多1多2.Add(new 多1多2(多1_000, 多2_000)));
//            }
//        }
//        [Fact]
//        public void InternalRemove()
//        {
//            var e = new SetEntities();
//            for (var a = 0; a < 100; a++)
//            {
//                var 多1_000 = new 作りたい仕様確認.Set.多1(a, a, a);
//                var 多2_000 = new 作りたい仕様確認.Set.多2(a, a, a);
//                e.多1.Add(多1_000);
//                e.多2.Add(多2_000);
//                Assert.IsTrue(e.多1多2.Add(new 多1多2(多1_000, 多2_000)));
//            }
//            for (var a = 0; a < 100; a++)
//            {
//                var 多1_000 = new 作りたい仕様確認.Set.多1(a, a, a);
//                var 多2_000 = new 作りたい仕様確認.Set.多2(a, a, a);
//                Assert.IsTrue(e.多1多2.Remove(new 多1多2(多1_000, 多2_000)));
//                Assert.False(e.多1多2.Remove(new 多1多2(多1_000, 多2_000)));
//            }
//        }
//    }
//}
