using Lite.Databases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.作りたい仕様確認.Set
{
    [TestClass]
    public class Test_Check
    {
        private SetEntities e;
        [TestInitialize]
        public void MyTestInitialize()
        {
            this.e = new SetEntities();
        }
        [TestCleanup]
        public void MyTestCleanup()
        {
            this.e.Dispose();
        }
        [TestMethod]
        public void RelationshipValidate()
        {
            var e = this.e.Transaction();
            e.table1.Add(new table1(1));
            e.table2.Add(new table2(1, 1));
            e.Commit();
        }
        [TestMethod]
        [ExpectedException(typeof(RelationshipException))]
        public void RelationshipValidate_RelationshipException()
        {
            var e = this.e.Transaction();
            e.table1.Add(new table1(1));
            e.table2.Add(new table2(1, 1));
            e.table2.Add(new table2(2, 2));
            e.Commit();
        }
    }
}
