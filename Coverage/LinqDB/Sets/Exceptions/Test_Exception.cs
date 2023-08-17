using LinqDB.Sets;
using LinqDB.Sets.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Sets.Exceptions;

[TestClass]
public class Test_Exception{
    [TestMethod,ExpectedException(typeof(ManyTupleException))]
    public void 複数行が原因のManyTupleException(){
        var data=new Set<int>{
            1,2
        };
        data.Single();
    }
    [TestMethod,ExpectedException(typeof(OneTupleException))]
    public void 複数行が原因のUniqueTupleException(){
        var data=new Set<int>{
            1
        };
        Assert.AreEqual(1,data.DUnion(data));
    }
    [TestMethod,ExpectedException(typeof(ZeroTupleException))]
    public void ゼロ行が原因のUniqueTupleException(){
        var data=Array.Empty<int>().ToSet();
        data.Max(p=>p);
    }
}