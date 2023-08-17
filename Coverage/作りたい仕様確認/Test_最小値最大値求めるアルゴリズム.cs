using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.作りたい仕様確認;

/// <summary>
///DisposableTest のテスト クラスです。すべての
///DisposableTest 単体テストをここに含めます
///</summary>
[TestClass]
public class Test_最小値最大値求めるアルゴリズム
{
    #region 追加のテスト属性
    private const int データ数 = 100000;
    private static readonly 大小 MinValue = new(int.MinValue);
    private static readonly 大小 MaxValue = new(int.MaxValue);
    private static readonly 大小[] データ軽い = new 大小[データ数];
    private struct 大小 : IComparable<大小>, IEquatable<大小>
    {
        public int v;
        public 大小(int v) => this.v=v;
        public int CompareTo(大小 other)
        {
            if (this.v < other.v)
            {
                return -1;
            }
            return this.v > other.v ? 1 : 0;
        }
        public bool Equals(大小 other) => this.v==other.v;
    }
    [ClassInitialize]
    public static void MyClassInitialize(TestContext testContext)
    {
        var r = new Random(1);
        for (var a = 0; a < データ数; a++)
        {
            データ軽い[a].v = a;
        }
        for (var a = 0; a < データ数; a++)
        {
            var i = r.Next(データ数);
            var j = r.Next(データ数);
            var t = データ軽い[i];
            データ軽い[i] = データ軽い[j];
            データ軽い[j] = t;
        }
    }
    #endregion
    [TestMethod]
    public void 最小値最大値を別々に求める()
    {
        var 最小値 = MaxValue;
        var 最大値 = MinValue;
        for (var a = 0; a < データ数; a++)
        {
            var v = データ軽い[a];
            if (最小値.CompareTo(v) > 0)
            {
                最小値 = v;
            }
        }
        for (var a = 0; a < データ数; a++)
        {
            var v = データ軽い[a];
            if (最大値.CompareTo(v) < 0)
            {
                最大値 = v;
            }
        }
        Assert.AreEqual(new 大小(0), 最小値);
        Assert.AreEqual(new 大小(データ数 - 1), 最大値);
    }
    [TestMethod]
    public void 最小値最大値を同時に求める()
    {
        var 最小値 = MaxValue;
        var 最大値 = MinValue;
        for (var a = 0; a < データ数; a++)
        {
            var v = データ軽い[a];
            if (最小値.CompareTo(v) > 0)
            {
                最小値 = v;
            }
            if (最大値.CompareTo(v) < 0)
            {
                最大値 = v;
            }
        }
        Assert.AreEqual(new 大小(0), 最小値);
        Assert.AreEqual(new 大小(データ数 - 1), 最大値);
    }
    [TestMethod]
    public void 最小値最大値を同時に求める小工夫Test()
    {
        var 最小値 = データ軽い[0];
        var 最大値 = データ軽い[0];
        for (var a = 1; a < データ数; a++)
        {
            var v = データ軽い[a];
            if (最小値.CompareTo(v) > 0)
            {
                最小値 = v;
            }
            else if (最大値.CompareTo(v) < 0)
            {
                最大値 = v;
            }
        }
        Assert.AreEqual(new 大小(0), 最小値);
        Assert.AreEqual(new 大小(データ数 - 1), 最大値);
    }
    [TestMethod]
    public void 最小値最大値を同時に求める大工夫()
    {
        var 最小値 = MaxValue;
        var 最大値 = MinValue;
        for (int a = 0, c = 1; a < データ数;)
        {
            var 偶数値 = データ軽い[a];
            var 奇数値 = データ軽い[c];
            if (偶数値.CompareTo(奇数値) < 0)
            {
                if (最小値.CompareTo(偶数値) > 0)
                {
                    最小値 = 偶数値;
                }
                if (最大値.CompareTo(奇数値) < 0)
                {
                    最大値 = 奇数値;
                }
            }
            else
            {
                if (最小値.CompareTo(奇数値) > 0)
                {
                    最小値 = 奇数値;
                }
                if (最大値.CompareTo(偶数値) < 0)
                {
                    最大値 = 偶数値;
                }
            }
            a += 2;
            c += 2;
        }
        Assert.AreEqual(new 大小(0), 最小値);
        Assert.AreEqual(new 大小(データ数 - 1), 最大値);
    }
}