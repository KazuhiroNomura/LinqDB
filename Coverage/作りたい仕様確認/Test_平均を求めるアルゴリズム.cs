using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.作りたい仕様確認;

/// <summary>
///DisposableTest のテスト クラスです。すべての
///DisposableTest 単体テストをここに含めます
///</summary>
[TestClass]
public class Test_平均を求めるアルゴリズム
{
    private const int データ数 = 1000000;
    private double[] データ = new double[データ数];
    //クラスの最初のテストを実行する前にコードを実行するには、ClassInitialize を使用
    [TestInitialize]
    public void MyTestInitialize()
    {
        var r = new Random(1);
        for (var a = 0; a < データ数; a++)
            this.データ[a] = a;
        for (var a = 0; a < データ数; a++)
        {
            var i = r.Next(データ数);
            var j = r.Next(データ数);
            var t = this.データ[i];
            this.データ[i] = this.データ[j];
            this.データ[j] = t;
        }
    }
    [TestMethod]
    public void Avedev平均偏差Test()
    {
        //解答：算術平均値は (2+3+4+7+9) / 5=5 
        //平均偏差は (|2−5|+|3−5|+|4−5|+|7−5|+|9−5|)/5=(3+2+1+2+4) / 5=2.4
        double 合計 = 0;
        var データ = new double[] { 2, 3, 4, 7, 9 };
        foreach (var T in データ)
            合計 += T;
        var 算術平均 = 合計 / データ数;
        double 差 = 0;
        foreach (var T in データ)
            差 += T - 算術平均;
        Debug.WriteLine("平均偏差 " + 差 / データ数);
    }
    [TestMethod]
    public void ArithmeticAverage相加平均Test()
    {
        double 合計 = 0;
        foreach (var T in this.データ)
            合計 += T;
        var 算術平均 = 合計 / データ数;
        Assert.AreEqual(this.データ.Average(), 算術平均);
    }
    [TestMethod]
    public void Geomean相乗平均Test()
    {
        this.データ = new double[] { 2, 8 };
        var 掛け算合計 = this.データ[0];
        for (var a = 1; a < this.データ.Length; a++)
            掛け算合計 *= this.データ[a];
        var expected = Math.Sqrt(掛け算合計);
        var log合計 = Math.Log(this.データ[0], 2);
        for (var a = 1; a < this.データ.Length; a++)
            log合計 += Math.Log(this.データ[a], 2);
        var actural = log合計 / this.データ.Length * (log合計 / this.データ.Length);
        Assert.AreEqual(expected, actural);
    }
}