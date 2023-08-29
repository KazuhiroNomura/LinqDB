using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable ConvertClosureToMethodGroup

// ReSharper disable ConvertNullableToShortForm
// ReSharper disable ExpressionIsAlwaysNull
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable RedundantTypeArgumentsOfMethod
// ReSharper disable RedundantOverflowCheckingContext
// ReSharper disable RedundantCast
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class Test_DynamicAssembply:ATest {
    //private sealed class Lambda2 {
    //    private Int32 a;

    //    private Int32 b;

    //    private Int32 o0;

    //    private Int32 i1;

    //    private Int32 p;

    //    private Int32 index;

    //    internal IEnumerable<Int32> _6(Int32 o0) {
    //        //Error decoding local variables: Signature type sequence must have at least one element.
    //        return ATest.ArrN<Int32>(b).Select((Int32 i1) => base.Lambda2.o0+i1).Select((Int32 p,Int32 index) => p*index);
    //    }

    //    internal Int32 _7(Int32 i1) {
    //        return o0+i1;
    //    }

    //    internal Int32 _8(Int32 p,Int32 index) {
    //        return p*index;
    //    }
    //}


    private int selector(int a) => a;
    public IEnumerable<int>Test() {
        return new int[3].Select(this.selector);
    }
    [TestMethod]
    public void Select_indexSelector() {
        this.実行結果が一致するか確認(a => SetN<int>(a).Select((p,index) => p+index));
    }
    [TestMethod]
    public void Select_selector(){
        this.実行結果が一致するか確認(a => SetN<int>(a).Select(b => b+b));
    }
    [TestMethod]
    public void SelectMany_selector() {
        this.実行結果が一致するか確認(a => SetN<int>(a).SelectMany(b => SetN<int>(b)));
    }
    [TestMethod]
    public void SelectMany_resultSelector() {
        this.実行結果が一致するか確認(a => SetN<int>(a).SelectMany(b => SetN<int>(b),(o,i)=>o));
    }
    [TestMethod]
    public void SelectMany_resultSelector_Select_selector() {
        this.実行結果が一致するか確認(a => SetN<int>(a).SelectMany(b => SetN<int>(b),(o,i) => o).Select((p,index)=>p));
    }
}