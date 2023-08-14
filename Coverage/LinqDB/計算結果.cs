using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB;

[TestClass]
public class 計算結果 : ATest
{
    [TestMethod]
    public void Add() => this.AddSubtract(ExpressionType.Add, op_Addition);
    private void Equal<T0, T1, TResult>(T0 a, T1 b, Expression<Func<T0, T1, TResult>> Lambda)
    {
        var actual1Stopwatch = Stopwatch.StartNew();
        var actualDelegate = this.CreateDelegate(Lambda);
        var actual1 = actualDelegate(a, b);
        var actual1秒 = actual1Stopwatch.ElapsedMilliseconds;
        var expectedStopwatch = Stopwatch.StartNew();
        var expectedDelegate = Lambda.Compile();
        var expected = expectedDelegate(a, b);
        var expected秒 = expectedStopwatch.ElapsedMilliseconds;
        var s = new StackFrame(1);
        Trace.WriteLine(s.GetMethod()!.Name + "," + actual1秒 + "ミリ秒," + expected秒 + "ミリ秒," + ((double)expected秒 / actual1秒) + "倍");
        Assert.AreEqual(expected, actual1);
    }
    private void MakeBinary<T>(T a, T b, ExpressionType NodeType, string 演算子名)
    {
        var Parameter_a = Expression.Parameter(typeof(T));
        var Parameter_b = Expression.Parameter(typeof(T));
        this.Equal(
            a,
            b,
            Expression.Lambda<Func<T, T, T>>(
                Expression.MakeBinary(
                    NodeType,
                    Parameter_a,
                    Parameter_b
                ), Parameter_a, Parameter_b)
        );
        this.Equal(
            a,
            b,
            Expression.Lambda<Func<T, T, T>>(
                Expression.MakeBinary(
                    NodeType,
                    Parameter_a,
                    Parameter_b,
                    false,
                    typeof(T).GetMethod(演算子名)
                ), Parameter_a, Parameter_b)
        );
    }
    private void AddSubtract(ExpressionType NodeType, string op)
    {
        this.MakeBinary(0, 0, NodeType, op); this.MakeBinary(0, 1, NodeType, op); this.MakeBinary(1, 0, NodeType, op); this.MakeBinary(1, 1, NodeType, op); this.MakeBinary(1, int.MaxValue, NodeType, op); this.MakeBinary(int.MaxValue, 1, NodeType, op); this.MakeBinary(int.MaxValue, int.MaxValue, NodeType, op);
        this.MakeBinary(0U, 0U, NodeType, op); this.MakeBinary(0U, 1U, NodeType, op); this.MakeBinary(1U, 0U, NodeType, op); this.MakeBinary(1U, 1U, NodeType, op); this.MakeBinary(1U, uint.MaxValue, NodeType, op); this.MakeBinary(uint.MaxValue, 1U, NodeType, op); this.MakeBinary(uint.MaxValue, uint.MaxValue, NodeType, op);
        this.MakeBinary(0L, 0L, NodeType, op); this.MakeBinary(0L, 1L, NodeType, op); this.MakeBinary(1L, 0L, NodeType, op); this.MakeBinary(1L, 1L, NodeType, op); this.MakeBinary(1L, long.MaxValue, NodeType, op); this.MakeBinary(long.MaxValue, 1L, NodeType, op); this.MakeBinary(long.MaxValue, long.MaxValue, NodeType, op);
        this.MakeBinary(0UL, 0UL, NodeType, op); this.MakeBinary(0UL, 1UL, NodeType, op); this.MakeBinary(1UL, 0UL, NodeType, op); this.MakeBinary(1UL, 1UL, NodeType, op); this.MakeBinary(1UL, ulong.MaxValue, NodeType, op); this.MakeBinary(ulong.MaxValue, 1UL, NodeType, op); this.MakeBinary(ulong.MaxValue, ulong.MaxValue, NodeType, op);
        this.MakeBinary(0F, 0F, NodeType, op); this.MakeBinary(0F, 1F, NodeType, op); this.MakeBinary(1F, 0F, NodeType, op); this.MakeBinary(1F, 1F, NodeType, op); this.MakeBinary(1F, float.MaxValue, NodeType, op); this.MakeBinary(float.MaxValue, 1F, NodeType, op); this.MakeBinary(float.MaxValue, float.MaxValue, NodeType, op);
        this.MakeBinary(0D, 0D, NodeType, op); this.MakeBinary(0D, 1D, NodeType, op); this.MakeBinary(1D, 0D, NodeType, op); this.MakeBinary(1D, 1D, NodeType, op); this.MakeBinary(1D, double.MaxValue, NodeType, op); this.MakeBinary(double.MaxValue, 1D, NodeType, op); this.MakeBinary(double.MaxValue, double.MaxValue, NodeType, op);
        this.MakeBinary(0M, 0M, NodeType, op); this.MakeBinary(0M, 1M, NodeType, op); this.MakeBinary(1M, 0M, NodeType, op); this.MakeBinary(1M, 1M, NodeType, op);
    }
    private void AddSubtractChecked(ExpressionType NodeType, string op)
    {
        this.MakeBinary(0, 0, NodeType, op); this.MakeBinary(0, 1, NodeType, op); this.MakeBinary(1, 0, NodeType, op); this.MakeBinary(1, 1, NodeType, op);
        this.MakeBinary(0U, 0U, NodeType, op); this.MakeBinary(0U, 1U, NodeType, op); this.MakeBinary(1U, 0U, NodeType, op); this.MakeBinary(1U, 1U, NodeType, op);
        this.MakeBinary(0L, 0L, NodeType, op); this.MakeBinary(0L, 1L, NodeType, op); this.MakeBinary(1L, 0L, NodeType, op); this.MakeBinary(1L, 1L, NodeType, op);
        this.MakeBinary(0UL, 0UL, NodeType, op); this.MakeBinary(0UL, 1UL, NodeType, op); this.MakeBinary(1UL, 0UL, NodeType, op); this.MakeBinary(1UL, 1UL, NodeType, op);
        this.MakeBinary(0F, 0F, NodeType, op); this.MakeBinary(0F, 1F, NodeType, op); this.MakeBinary(1F, 0F, NodeType, op); this.MakeBinary(1F, 1F, NodeType, op);
        this.MakeBinary(0D, 0D, NodeType, op); this.MakeBinary(0D, 1D, NodeType, op); this.MakeBinary(1D, 0D, NodeType, op); this.MakeBinary(1D, 1D, NodeType, op);
        this.MakeBinary(0M, 0M, NodeType, op); this.MakeBinary(0M, 1M, NodeType, op); this.MakeBinary(1M, 0M, NodeType, op); this.MakeBinary(1M, 1M, NodeType, op);
    }
    private const string op_Addition = nameof(op_Addition);
    [TestMethod] public void AddAssign() => this.AddSubtract(ExpressionType.AddAssign, op_Addition);
    [TestMethod] public void AddAssignChecked() => this.AddSubtractChecked(ExpressionType.AddAssignChecked, op_Addition);
    [TestMethod] public void AddChecked() => this.AddSubtractChecked(ExpressionType.AddChecked, op_Addition);
    private void AndOr(ExpressionType NodeType, string op)
    {
        this.MakeBinary(false, false, NodeType, op); this.MakeBinary(false, true, NodeType, op); this.MakeBinary(true, false, NodeType, op); this.MakeBinary(true, true, NodeType, op);
        this.MakeBinary(0, 0, NodeType, op); this.MakeBinary(0, 1, NodeType, op); this.MakeBinary(1, 0, NodeType, op); this.MakeBinary(1, 1, NodeType, op); this.MakeBinary(1, int.MaxValue - 1, NodeType, op); this.MakeBinary(int.MaxValue - 1, 1, NodeType, op);
        this.MakeBinary(0U, 0U, NodeType, op); this.MakeBinary(0U, 1U, NodeType, op); this.MakeBinary(1U, 0U, NodeType, op); this.MakeBinary(1U, 1U, NodeType, op); this.MakeBinary(1U, uint.MaxValue - 1, NodeType, op); this.MakeBinary(uint.MaxValue - 1, 1U, NodeType, op);
        this.MakeBinary(0L, 0L, NodeType, op); this.MakeBinary(0L, 1L, NodeType, op); this.MakeBinary(1L, 0L, NodeType, op); this.MakeBinary(1L, 1L, NodeType, op); this.MakeBinary(1L, long.MaxValue - 1, NodeType, op); this.MakeBinary(long.MaxValue - 1, 1L, NodeType, op);
        this.MakeBinary(0UL, 0UL, NodeType, op); this.MakeBinary(0UL, 1UL, NodeType, op); this.MakeBinary(1UL, 0UL, NodeType, op); this.MakeBinary(1UL, 1UL, NodeType, op); this.MakeBinary(1UL, ulong.MaxValue - 1, NodeType, op); this.MakeBinary(ulong.MaxValue - 1, 1UL, NodeType, op);
    }
    private const string op_BitwiseAnd = nameof(op_BitwiseAnd);
    [TestMethod] public void And() => this.AndOr(ExpressionType.And, op_BitwiseAnd);
    [TestMethod] public void AndAssign() => this.AndOr(ExpressionType.AndAssign, op_BitwiseAnd);
    private void ArrayIndex<T>(T[] array)
    {
        var Parameter_a = Expression.Parameter(typeof(T[]));
        var Parameter_b = Expression.Parameter(typeof(int));
        var Lambda = Expression.Lambda<Func<T[], int, T>>(
            Expression.ArrayIndex(
                Parameter_a,
                Parameter_b
            ), Parameter_a, Parameter_b);
        this.Equal(
            array,
            0,
            Lambda
        );
        this.Equal(
            array,
            array.Length - 1,
            Lambda
        );
    }
    [TestMethod]
    public void ArrayIndex()
    {
        this.ArrayIndex(new sbyte[] { 1, 2, 3 }); this.ArrayIndex(new byte[] { 1, 2, 3 }); this.ArrayIndex(new[] { true, false, true });
        this.ArrayIndex(new short[] { 1, 2, 3 }); this.ArrayIndex(new ushort[] { 1, 2, 3 }); this.ArrayIndex(new[] { '1', '2', '3' });
        this.ArrayIndex(new[] { 1, 2, 3 }); this.ArrayIndex(new uint[] { 1, 2, 3 });
        this.ArrayIndex(new long[] { 1, 2, 3 }); this.ArrayIndex(new ulong[] { 1, 2, 3 });
        this.ArrayIndex(new double[] { 1, 2, 3 }); this.ArrayIndex(new float[] { 1, 2, 3 });
        this.ArrayIndex(new decimal[] { 1, 2, 3 }); this.ArrayIndex(new[] { "1", "2", "3" });
    }
    private void ArrayLength<T>(T[] array)
    {
        var Parameter_a = Expression.Parameter(typeof(T[]));
        this.Equal(
            array,
            Expression.Lambda<Func<T[], int>>(
                Expression.ArrayLength(
                    Parameter_a
                ), Parameter_a)
        );
    }
    [TestMethod]
    public void ArrayLength()
    {
        this.ArrayLength(new sbyte[] { 1, 2, 3 }); this.ArrayLength(new byte[] { 1, 2, 3 }); this.ArrayLength(new[] { true, false, true });
        this.ArrayLength(new short[] { 1, 2, 3 }); this.ArrayLength(new ushort[] { 1, 2, 3 }); this.ArrayLength(new[] { '1', '2', '3' });
        this.ArrayLength(new[] { 1, 2, 3 }); this.ArrayLength(new uint[] { 1, 2, 3 });
        this.ArrayLength(new long[] { 1, 2, 3 }); this.ArrayLength(new ulong[] { 1, 2, 3 });
        this.ArrayLength(new double[] { 1, 2, 3 }); this.ArrayLength(new float[] { 1, 2, 3 });
        this.ArrayLength(new decimal[] { 1, 2, 3 }); this.ArrayLength(new[] { "1", "2", "3" });
    }
    private void Equal<T0, TResult>(T0[] a, int index, Expression<Func<T0[], int, TResult>> Lambda)
    {
        var actual1Stopwatch = Stopwatch.StartNew();
        var actualDelegate = this.CreateDelegate(Lambda);
        var actual1 = actualDelegate(a, index);
        var actual1秒 = actual1Stopwatch.ElapsedMilliseconds;
        var expectedStopwatch = Stopwatch.StartNew();
        var expectedDelegate = Lambda.Compile();
        var expected = expectedDelegate(a, index);
        var expected秒 = expectedStopwatch.ElapsedMilliseconds;
        var s = new StackFrame(1);
        Trace.WriteLine(s.GetMethod()!.Name + "," + actual1秒 + "ミリ秒," + expected秒 + "ミリ秒," + ((double)expected秒 / actual1秒) + "倍");
        Assert.AreEqual(expected, actual1);
    }
    private void Assign<T>(T a, T b)
    {
        var Parameter_a = Expression.Parameter(typeof(T));
        var Parameter_b = Expression.Parameter(typeof(T));
        this.Equal(
            a,
            b,
            Expression.Lambda<Func<T, T, T>>(
                Expression.Assign(
                    Parameter_a,
                    Parameter_b
                ), Parameter_a, Parameter_b)
        );
    }
    [TestMethod]
    public void Assign()
    {
        this.Assign<sbyte>(1, 2); this.Assign<byte>(1, 2); this.Assign(false, true);
        this.Assign<short>(1, 2); this.Assign<ushort>(1, 2); this.Assign('a', 'b');
        this.Assign(1, 2); this.Assign<uint>(1, 2); this.Assign<float>(1, 2);
        this.Assign<long>(1, 2); this.Assign<ulong>(1, 2); this.Assign<double>(1, 2);
        this.Assign<decimal>(1, 2); this.Assign("1", "2");
    }
    private void Block<T>(T a)
    {
        var Parameter_a = Expression.Parameter(typeof(T));
        this.Equal(
            a,
            Expression.Lambda<Func<T, T>>(
                Expression.Block(
                    Parameter_a
                ), Parameter_a)
        );
    }
    [TestMethod]
    public void Block()
    {
        this.Block(1); this.Block("1"); this.Block(1m);
    }
    private static int Callメソッド(int a) => a;
    [TestMethod]
    public void Call()
    {
        var Parameter_a = Expression.Parameter(typeof(int));
        var Lambda = Expression.Lambda<Func<int, int>>(
            Expression.Call(
                typeof(計算結果).GetMethod(nameof(Callメソッド), BindingFlags.NonPublic | BindingFlags.Static),
                Parameter_a
            ), Parameter_a);
        this.Equal(1, Lambda); this.Equal(2, Lambda);
    }
    [TestMethod]
    public void Coalesce()
    {
        var Parameter_a = Expression.Parameter(typeof(int?));
        var Parameter_b = Expression.Parameter(typeof(int));
        var Lambda = Expression.Lambda<Func<int?, int, int>>(
            Expression.Coalesce(
                Parameter_a,
                Parameter_b
            ), Parameter_a, Parameter_b);
        this.Equal(new int?(), 2, Lambda); this.Equal(1, 2, Lambda);
    }
    [TestMethod]
    public void Conditional()
    {
        var Parameter_a = Expression.Parameter(typeof(bool));
        var Lambda = Expression.Lambda<Func<bool, int>>(
            Expression.Condition(
                Parameter_a,
                Expression.Constant(1),
                Expression.Constant(2)
            ), Parameter_a);
        this.Equal(false, Lambda); this.Equal(true, Lambda);
    }
    private void Constant<T>(T input)
    {
        var Lambda = Expression.Lambda<Func<T>>(
            Expression.Constant(input)
        );
        this.Execute2(Lambda);
    }
    [TestMethod]
    public void Constant()
    {
        this.Constant(1); this.Constant("1"); this.Constant(1m); this.Constant(new object());
    }
    private void Convert<T>(object a)
    {
        var Lambda = Expression.Lambda<Func<T>>(
            Expression.Convert(
                Expression.Constant(a),
                typeof(T)
            )
        );
        this.Execute2(Lambda);
    }
    [TestMethod]
    public void Convert()
    {
        this.Convert<int>(1m); this.Convert<decimal>(1); this.Convert<int>(1f);
    }
}