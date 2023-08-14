using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinqDB.Sets;
namespace CoverageCS.作りたい仕様確認;

[TestClass]
public class Test_Treeを直接EnumeratorするのとバッファにEnumerator {
    private const int データ数 = 20000;
    private readonly Set<long> データ = new();
    [TestInitialize]
    public void MyTestInitialize() {
        var データ = this.データ;
        for(var a = 0;a<データ数;a++) {
            データ.VoidAdd(a);
        }
    }
    [TestMethod]
    public void Enumerator速度(){
        for(var a = this.データ.GetEnumerator();a.MoveNext();) {
            for(var b = this.データ.GetEnumerator();b.MoveNext();) {
            }
        }
    }
    [TestMethod]
    public void Enumerator1速度() {
        for(var a = this.データ.GetEnumerator1();a.MoveNext();) {
            for(var b = this.データ.GetEnumerator1();b.MoveNext();) {
            }
        }
    }
    [TestMethod]
    public void バッファにEnumerator速度() {
        for(var a = this.データ.GetWrite1Read1Enumerator();a.MoveNext();) {
            for(var b = this.データ.GetWrite1Read1Enumerator();b.MoveNext();) {
            }
        }
    }
}