using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Reflection.Emit;
using LinqDB.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable RedundantCast
#pragma warning disable 649

// ReSharper disable AssignNullToNotNullAttribute
namespace CoverageCS.LinqDB.Helpers;

[TestClass]
public class Test_ILGenerator拡張メソッド
{
    private static void PrivateBr_S下へ(int テスト移動Offset)
    {
        var D = new DynamicMethod("", typeof(int), Type.EmptyTypes, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        var J = I.DefineLabel();
        I.Emit(OpCodes.Br_S, J);
        var ジャンプ元の位置 = I.ILOffset;
        for (var b = 0; b < テスト移動Offset; b++)
        {
            I.Emit(OpCodes.Nop);
        }
        I.MarkLabel(J);
        var ジャンプ先の位置 = I.ILOffset;
        var 移動Offset = ジャンプ先の位置 - ジャンプ元の位置;
        Assert.AreEqual(テスト移動Offset, 移動Offset);
        I.Emit(OpCodes.Ldc_I4_1);
        I.Emit(OpCodes.Ret);
        var M = (Func<int>)D.CreateDelegate(typeof(Func<int>));
        Assert.AreEqual(1, M());
    }

    [TestMethod] public void Br_S下へ() => PrivateBr_S下へ(127);
    [TestMethod, ExpectedException(typeof(NotSupportedException))]
    public void Br_S下へ_Exception() => PrivateBr_S下へ(128);
    private static void PrivateBr_S上へ(int テスト移動Offset)
    {
        var D = new DynamicMethod("", typeof(int), Type.EmptyTypes, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        var J = I.DefineLabel();
        var J2 = I.DefineLabel();
        I.Emit(OpCodes.Br, J2);
        I.MarkLabel(J);
        var ジャンプ先の位置 = I.ILOffset;
        var Nop数 = -テスト移動Offset - OpCodes.Br.Size - 1;
        for (var b = 0; b < Nop数; b++)
        {
            I.Emit(OpCodes.Nop);
        }
        I.Emit(OpCodes.Br_S, J);
        var ジャンプ元の位置 = I.ILOffset;
        var 移動Offset = ジャンプ先の位置 - ジャンプ元の位置;
        Assert.AreEqual(テスト移動Offset, 移動Offset);
        I.MarkLabel(J2);
        I.Emit(OpCodes.Ldc_I4_1);
        I.Emit(OpCodes.Ret);
        var M = (Func<int>)D.CreateDelegate(typeof(Func<int>));
        Assert.AreEqual(1, M());
    }
    [TestMethod]
    public void Br_S上へ() => PrivateBr_S上へ(-128);
    [TestMethod, ExpectedException(typeof(NotSupportedException))]
    public void Br_S上へ_Exception() => PrivateBr_S上へ(-129);

    //private static void PrivateLdelem<T>(T v) {
    //    var D = new DynamicMethod("",typeof(T),new[] { typeof(T[]) },typeof(ILGenerator拡張メソッド),true) {
    //        InitLocals=false
    //    };
    //    var I = D.GetILGenerator();
    //    I.Ldarg_0();
    //    I.Ldc_I4_1();
    //    I.Ldelem(typeof(T));
    //    I.Emit(OpCodes.Ret);
    //    var data=new T[2];
    //    data[1]=v;
    //    var M = (Func<T[],T>)D.CreateDelegate(typeof(Func<T[],T>));
    //    Assert.AreEqual(v,M(data));
    //}

    //[TestMethod] public void Ldelem_I() => PrivateLdelem((IntPtr)1);
    //[TestMethod] public void Ldelem_I1() => PrivateLdelem((SByte)1);
    //[TestMethod] public void Ldelem_I2() => PrivateLdelem((Int16)1);
    //[TestMethod] public void Ldelem_I4() => PrivateLdelem(1);
    //[TestMethod] public void Ldelem_I8() => PrivateLdelem(1L);
    //[TestMethod] public void Ldelem_U() => PrivateLdelem((UIntPtr)1);
    //[TestMethod] public void Ldelem_U1() => PrivateLdelem((Byte)1);
    //[TestMethod] public void Ldelem_U2() => PrivateLdelem((UInt16)1);
    //[TestMethod] public void Ldelem_U4() => PrivateLdelem(1U);
    //[TestMethod] public void Ldelem_U8() => PrivateLdelem(1UL);
    //[TestMethod] public void Ldelem_R4() => PrivateLdelem(1f);
    //[TestMethod] public void Ldelem_R8() => PrivateLdelem(1d);
    //[TestMethod] public void Ldelem() => PrivateLdelem("ABC");
    private static int DynamicSizeof<T>()
    {
        var D = new DynamicMethod("", typeof(int), Type.EmptyTypes, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Sizeof(typeof(T));
        I.Ret();
        return ((Func<int>)D.CreateDelegate(typeof(Func<int>)))();
    }
    private static Func<TLeft, TRight, TResult> 二項演算子<TLeft, TRight, TResult>(Action<ILGenerator> ILメソッド)
    {
        var D = new DynamicMethod("", typeof(TResult), new[]{
            typeof(TLeft),typeof(TRight)
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        I.Ldarg_1();
        ILメソッド(I);
        I.Ret();
        return (Func<TLeft, TRight, TResult>)D.CreateDelegate(typeof(Func<TLeft, TRight, TResult>));
    }

    private static Func<TInput, TInput, TResult> 二項演算子<TInput, TResult>(Action<ILGenerator> ILメソッド) => 二項演算子<TInput, TInput, TResult>(ILメソッド);
    [TestMethod]
    public void Add()
    {
        var M = 二項演算子<int, int>(I => I.Add());
        for (var a = int.MaxValue - 5; a < int.MaxValue - 1; a++)
        {
            for (var b = int.MaxValue - 5; b < int.MaxValue - 1; b++)
            {
                Assert.AreEqual(a + b, M(a, b));
            }
        }
    }
    [TestMethod]
    public void Add_Ovf()
    {
        var M = 二項演算子<int, int>(I => I.Add_Ovf());
        for (var a = int.MaxValue - 5; a < int.MaxValue - 1; a++)
        {
            for (var b = int.MaxValue - 5; b < int.MaxValue - 1; b++)
            {
                try
                {
                    var expected = checked(a + b);
                    Assert.AreEqual(expected, M(a, b));
                    Assert.Fail("OverflowExceptionが発生するべき");
                }
                catch (OverflowException)
                {
                    try
                    {
                        M(a, b);
                        Assert.Fail("OverflowExceptionが発生するべき");
                    }
                    catch (OverflowException) { }
                }
            }
        }
    }
    [TestMethod]
    public void Add_Ovf_Un()
    {
        var M = 二項演算子<uint, uint>(I => I.Add_Ovf_Un());
        for (var a = uint.MaxValue - 5; a < uint.MaxValue - 1; a++)
        {
            for (var b = uint.MaxValue - 5; b < uint.MaxValue - 1; b++)
            {
                try
                {
                    var expected = checked(a + b);
                    Assert.AreEqual(expected, M(a, b));
                    Assert.Fail("OverflowExceptionが発生するべき");
                }
                catch (OverflowException)
                {
                    try
                    {
                        M(a, b);
                        Assert.Fail("OverflowExceptionが発生するべき");
                    }
                    catch (OverflowException) { }
                }
            }
        }
    }
    [TestMethod]
    public void And()
    {
        var M = 二項演算子<int, int>(I => I.And());
        for (var a = int.MaxValue - 5; a < int.MaxValue - 1; a++)
        {
            for (var b = int.MaxValue - 5; b < int.MaxValue - 1; b++)
            {
                Assert.AreEqual(a & b, M(a, b));
            }
        }
    }

    //Arglist
    private static Func<int, int, int> ブランチ(Action<ILGenerator, Label> ILメソッド, int バイト数)
    {
        var D = new DynamicMethod("", typeof(int), new[]{
            typeof(int),typeof(int)
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        I.Ldarg_1();
        var ジャンプ = I.DefineLabel();
        ILメソッド(I, ジャンプ);
        var ILOffset = I.ILOffset;
        for (var a = 0; a < バイト数 - 2; a++)
            I.Nop();
        I.Ldc_I4_0();
        I.Ret();
        I.MarkLabel(ジャンプ);
        Assert.IsTrue(I.ILOffset - ILOffset == バイト数);
        I.Ldc_I4_1();
        I.Ret();
        return (Func<int, int, int>)D.CreateDelegate(typeof(Func<int, int, int>));
    }

    [TestMethod]
    public void Beq()
    {
        var M = ブランチ((I, ジャンプ) => I.Beq(ジャンプ), 255);
        Assert.AreEqual(1, M(0, 0));
        Assert.AreEqual(0, M(0, 1));
        Assert.AreEqual(0, M(1, 0));
        Assert.AreEqual(1, M(1, 1));
    }

    [TestMethod]
    public void Beq_S()
    {
        var M = ブランチ((I, ジャンプ) => I.Beq_S(ジャンプ), 127);
        Assert.AreEqual(1, M(0, 0));
        Assert.AreEqual(0, M(0, 1));
        Assert.AreEqual(0, M(1, 0));
        Assert.AreEqual(1, M(1, 1));
    }

    [TestMethod, ExpectedException(typeof(NotSupportedException))]
    public void Beq_S_NotSupportedException()
    {
        ブランチ((I, ジャンプ) => I.Beq_S(ジャンプ), 128);
    }

    [TestMethod]
    public void Bge()
    {
        var M = ブランチ((I, ジャンプ) => I.Bge(ジャンプ), 254);
        Assert.AreEqual(1, M(0, 0));
        Assert.AreEqual(0, M(0, 1));
        Assert.AreEqual(1, M(1, 0));
        Assert.AreEqual(1, M(1, 1));
        const int 小 = 0;
        const int 大 = unchecked((int)uint.MaxValue);
        Assert.AreEqual(1, M(小, 小));
        Assert.AreEqual(1, M(小, 大));
        Assert.AreEqual(0, M(大, 小));
        Assert.AreEqual(1, M(大, 大));
    }

    [TestMethod]
    public void Bge_Un()
    {
        var M = ブランチ((I, ジャンプ) => I.Bge_Un(ジャンプ), 254);
        Assert.AreEqual(1, M(0, 0));
        Assert.AreEqual(0, M(0, 1));
        Assert.AreEqual(1, M(1, 0));
        Assert.AreEqual(1, M(1, 1));
        const int 小 = 0;
        const int 大 = unchecked((int)uint.MaxValue);
        Assert.AreEqual(1, M(小, 小));
        Assert.AreEqual(0, M(小, 大));
        Assert.AreEqual(1, M(大, 小));
        Assert.AreEqual(1, M(大, 大));
    }

    [TestMethod]
    public void Bge_Un_S()
    {
        var M = ブランチ((I, ジャンプ) => I.Bge_Un_S(ジャンプ), 127);
        Assert.AreEqual(1, M(0, 0));
        Assert.AreEqual(0, M(0, 1));
        Assert.AreEqual(1, M(1, 0));
        Assert.AreEqual(1, M(1, 1));
        const int 小 = 0;
        const int 大 = unchecked((int)uint.MaxValue);
        Assert.AreEqual(1, M(小, 小));
        Assert.AreEqual(0, M(小, 大));
        Assert.AreEqual(1, M(大, 小));
        Assert.AreEqual(1, M(大, 大));
    }

    [TestMethod, ExpectedException(typeof(NotSupportedException))]
    public void Bge_Un_S_NotSupportedException()
    {
        ブランチ((I, ジャンプ) => I.Bge_Un_S(ジャンプ), 128);
    }

    [TestMethod]
    public void Bgt()
    {
        var M = ブランチ((I, ジャンプ) => I.Bgt(ジャンプ), 254);
        Assert.AreEqual(0, M(0, 0));
        Assert.AreEqual(0, M(0, 1));
        Assert.AreEqual(1, M(1, 0));
        Assert.AreEqual(0, M(1, 1));
        const int 小 = 0;
        const int 大 = unchecked((int)uint.MaxValue);
        Assert.AreEqual(0, M(小, 小));
        Assert.AreEqual(1, M(小, 大));
        Assert.AreEqual(0, M(大, 小));
        Assert.AreEqual(0, M(大, 大));
    }

    [TestMethod]
    public void Bgt_S()
    {
        var M = ブランチ((I, ジャンプ) => I.Bgt(ジャンプ), 127);
        Assert.AreEqual(0, M(0, 0));
        Assert.AreEqual(0, M(0, 1));
        Assert.AreEqual(1, M(1, 0));
        Assert.AreEqual(0, M(1, 1));
        const int 小 = 0;
        const int 大 = unchecked((int)uint.MaxValue);
        Assert.AreEqual(0, M(小, 小));
        Assert.AreEqual(1, M(小, 大));
        Assert.AreEqual(0, M(大, 小));
        Assert.AreEqual(0, M(大, 大));
    }

    [TestMethod, ExpectedException(typeof(NotSupportedException))]
    public void Bgt_S_NotSupportedException()
    {
        ブランチ((I, ジャンプ) => I.Bgt_S(ジャンプ), 128);
    }

    [TestMethod]
    public void Bgt_Un()
    {
        var M = ブランチ((I, ジャンプ) => I.Bgt_Un(ジャンプ), 254);
        Assert.AreEqual(0, M(0, 0));
        Assert.AreEqual(0, M(0, 1));
        Assert.AreEqual(1, M(1, 0));
        Assert.AreEqual(0, M(1, 1));
        const int 小 = 0;
        const int 大 = unchecked((int)uint.MaxValue);
        Assert.AreEqual(0, M(小, 小));
        Assert.AreEqual(0, M(小, 大));
        Assert.AreEqual(1, M(大, 小));
        Assert.AreEqual(0, M(大, 大));
    }

    [TestMethod]
    public void Bgt_Un_S()
    {
        var M = ブランチ((I, ジャンプ) => I.Bgt_Un_S(ジャンプ), 127);
        Assert.AreEqual(0, M(0, 0));
        Assert.AreEqual(0, M(0, 1));
        Assert.AreEqual(1, M(1, 0));
        Assert.AreEqual(0, M(1, 1));
        const int 小 = 0;
        const int 大 = unchecked((int)uint.MaxValue);
        Assert.AreEqual(0, M(小, 小));
        Assert.AreEqual(0, M(小, 大));
        Assert.AreEqual(1, M(大, 小));
        Assert.AreEqual(0, M(大, 大));
    }

    [TestMethod, ExpectedException(typeof(NotSupportedException))]
    public void Bgt_Un_S_NotSupportedException()
    {
        ブランチ((I, ジャンプ) => I.Bgt_Un_S(ジャンプ), 128);
    }

    [TestMethod]
    public void Ble()
    {
        var M = ブランチ((I, ジャンプ) => I.Ble(ジャンプ), 254);
        Assert.AreEqual(1, M(0, 0));
        Assert.AreEqual(1, M(0, 1));
        Assert.AreEqual(0, M(1, 0));
        Assert.AreEqual(1, M(1, 1));
        const int 小 = 0;
        const int 大 = unchecked((int)uint.MaxValue);
        Assert.AreEqual(1, M(小, 小));
        Assert.AreEqual(0, M(小, 大));
        Assert.AreEqual(1, M(大, 小));
        Assert.AreEqual(1, M(大, 大));
    }

    [TestMethod]
    public void Ble_S()
    {
        var M = ブランチ((I, ジャンプ) => I.Ble(ジャンプ), 127);
        Assert.AreEqual(1, M(0, 0));
        Assert.AreEqual(1, M(0, 1));
        Assert.AreEqual(0, M(1, 0));
        Assert.AreEqual(1, M(1, 1));
        const int 小 = 0;
        const int 大 = unchecked((int)uint.MaxValue);
        Assert.AreEqual(1, M(小, 小));
        Assert.AreEqual(0, M(小, 大));
        Assert.AreEqual(1, M(大, 小));
        Assert.AreEqual(1, M(大, 大));
    }

    [TestMethod, ExpectedException(typeof(NotSupportedException))]
    public void Ble_S_NotSupportedException()
    {
        ブランチ((I, ジャンプ) => I.Ble_S(ジャンプ), 128);
    }

    [TestMethod]
    public void Ble_Un()
    {
        var M = ブランチ((I, ジャンプ) => I.Ble_Un(ジャンプ), 254);
        Assert.AreEqual(1, M(0, 0));
        Assert.AreEqual(1, M(0, 1));
        Assert.AreEqual(0, M(1, 0));
        Assert.AreEqual(1, M(1, 1));
        const int 小 = 0;
        const int 大 = unchecked((int)uint.MaxValue);
        Assert.AreEqual(1, M(小, 小));
        Assert.AreEqual(1, M(小, 大));
        Assert.AreEqual(0, M(大, 小));
        Assert.AreEqual(1, M(大, 大));
    }

    [TestMethod]
    public void Ble_Un_S()
    {
        var M = ブランチ((I, ジャンプ) => I.Ble_Un_S(ジャンプ), 127);
        Assert.AreEqual(1, M(0, 0));
        Assert.AreEqual(1, M(0, 1));
        Assert.AreEqual(0, M(1, 0));
        Assert.AreEqual(1, M(1, 1));
        const int 小 = 0;
        const int 大 = unchecked((int)uint.MaxValue);
        Assert.AreEqual(1, M(小, 小));
        Assert.AreEqual(1, M(小, 大));
        Assert.AreEqual(0, M(大, 小));
        Assert.AreEqual(1, M(大, 大));
    }

    [TestMethod, ExpectedException(typeof(NotSupportedException))]
    public void Ble_Un_S_NotSupportedException()
    {
        ブランチ((I, ジャンプ) => I.Ble_Un_S(ジャンプ), 128);
    }

    [TestMethod]
    public void Blt()
    {
        var M = ブランチ((I, ジャンプ) => I.Blt(ジャンプ), 254);
        Assert.AreEqual(0, M(0, 0));
        Assert.AreEqual(1, M(0, 1));
        Assert.AreEqual(0, M(1, 0));
        Assert.AreEqual(0, M(1, 1));
        const int 小 = 0;
        const int 大 = unchecked((int)uint.MaxValue);
        Assert.AreEqual(0, M(小, 小));
        Assert.AreEqual(0, M(小, 大));
        Assert.AreEqual(1, M(大, 小));
        Assert.AreEqual(0, M(大, 大));
    }

    [TestMethod]
    public void Blt_S()
    {
        var M = ブランチ((I, ジャンプ) => I.Blt_S(ジャンプ), 127);
        Assert.AreEqual(0, M(0, 0));
        Assert.AreEqual(1, M(0, 1));
        Assert.AreEqual(0, M(1, 0));
        Assert.AreEqual(0, M(1, 1));
        const int 小 = 0;
        const int 大 = unchecked((int)uint.MaxValue);
        Assert.AreEqual(0, M(小, 小));
        Assert.AreEqual(0, M(小, 大));
        Assert.AreEqual(1, M(大, 小));
        Assert.AreEqual(0, M(大, 大));
    }

    [TestMethod, ExpectedException(typeof(NotSupportedException))]
    public void Blt_S_NotSupportedException()
    {
        ブランチ((I, ジャンプ) => I.Blt_S(ジャンプ), 128);
    }

    [TestMethod]
    public void Blt_Un()
    {
        var M = ブランチ((I, ジャンプ) => I.Blt_Un(ジャンプ), 254);
        Assert.AreEqual(0, M(0, 0));
        Assert.AreEqual(1, M(0, 1));
        Assert.AreEqual(0, M(1, 0));
        Assert.AreEqual(0, M(1, 1));
        const int 小 = 0;
        const int 大 = unchecked((int)uint.MaxValue);
        Assert.AreEqual(0, M(小, 小));
        Assert.AreEqual(1, M(小, 大));
        Assert.AreEqual(0, M(大, 小));
        Assert.AreEqual(0, M(大, 大));
    }

    [TestMethod]
    public void Blt_Un_S()
    {
        var M = ブランチ((I, ジャンプ) => I.Blt_Un_S(ジャンプ), 127);
        Assert.AreEqual(0, M(0, 0));
        Assert.AreEqual(1, M(0, 1));
        Assert.AreEqual(0, M(1, 0));
        Assert.AreEqual(0, M(1, 1));
        const int 小 = 0;
        const int 大 = unchecked((int)uint.MaxValue);
        Assert.AreEqual(0, M(小, 小));
        Assert.AreEqual(1, M(小, 大));
        Assert.AreEqual(0, M(大, 小));
        Assert.AreEqual(0, M(大, 大));
    }

    [TestMethod, ExpectedException(typeof(NotSupportedException))]
    public void Blt_Un_S_NotSupportedException()
    {
        ブランチ((I, ジャンプ) => I.Blt_Un_S(ジャンプ), 128);
    }

    [TestMethod]
    public void Bne_Un()
    {
        var M = ブランチ((I, ジャンプ) => I.Bne_Un(ジャンプ), 255);
        Assert.AreEqual(0, M(0, 0));
        Assert.AreEqual(1, M(0, 1));
        Assert.AreEqual(1, M(1, 0));
        Assert.AreEqual(0, M(1, 1));
    }

    [TestMethod]
    public void Bne_Un_S()
    {
        var M = ブランチ((I, ジャンプ) => I.Bne_Un_S(ジャンプ), 127);
        Assert.AreEqual(0, M(0, 0));
        Assert.AreEqual(1, M(0, 1));
        Assert.AreEqual(1, M(1, 0));
        Assert.AreEqual(0, M(1, 1));
    }

    [TestMethod, ExpectedException(typeof(NotSupportedException))]
    public void Bne_Un_S_NotSupportedException()
    {
        ブランチ((I, ジャンプ) => I.Bne_Un_S(ジャンプ), 128);
    }

    //public void Box_Interface() {
    //    var D = new DynamicMethod("",typeof(IEquatable<Int32>),new[] { typeof(Int32) },typeof(Test_ILGenerator拡張メソッド),true) {
    //        InitLocals=false
    //    };
    //    var I = D.GetILGenerator();
    //    I.Ldarg_0();
    //    I.Box(typeof(Int32));
    //    I.Ret();
    //    var M = (Func<Int32,IEquatable<Int32>>)D.CreateDelegate(typeof(Func<Int32,IEquatable<Int32>>));
    //    var actual = M(1);
    //    Assert.AreEqual(1,actual);
    //}
    [TestMethod]
    public void Box_Object()
    {
        var D = new DynamicMethod("", typeof(object), new[]{
            typeof(int)
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        I.Box(typeof(int));
        I.Ret();
        var M = (Func<int, object>)D.CreateDelegate(typeof(Func<int, object>));
        var actual = M(1);
        Assert.AreEqual(1, actual);
    }

    private static Func<int> Brブランチ(Action<ILGenerator, Label> ILメソッド, int バイト数)
    {
        var D = new DynamicMethod("", typeof(int), Type.EmptyTypes, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        var ジャンプ = I.DefineLabel();
        ILメソッド(I, ジャンプ);
        var ILOffset = I.ILOffset;
        for (var a = 0; a < バイト数; a++)
            I.Nop();
        I.MarkLabel(ジャンプ);
        Assert.IsTrue(I.ILOffset - ILOffset == バイト数);
        I.Ldc_I4_1();
        I.Ret();
        return (Func<int>)D.CreateDelegate(typeof(Func<int>));
    }

    [TestMethod]
    public void Br()
    {
        var M = Brブランチ((I, ジャンプ先) => I.Br(ジャンプ先), 128);
        Assert.AreEqual(1, M());
    }

    [TestMethod]
    public void Br_S()
    {
        var M = Brブランチ((I, ジャンプ先) => I.Br_S(ジャンプ先), 127);
        Assert.AreEqual(1, M());
    }

    [TestMethod, ExpectedException(typeof(NotSupportedException))]
    public void Br_S_NotSupportedException()
    {
        Brブランチ((I, ジャンプ先) => I.Br_S(ジャンプ先), 128);
    }

    [TestMethod]
    public void Break()
    {
        var D = new DynamicMethod("", typeof(int), Type.EmptyTypes, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Break();
        I.Ldc_I4_1();
        I.Ret();
        var M = (Func<int>)D.CreateDelegate(typeof(Func<int>));
        Assert.AreEqual(1, M());
    }

    private static Func<int, int> Brfalse_Brtrue(Action<ILGenerator, Label> ILメソッド, int バイト数)
    {
        var D = new DynamicMethod("", typeof(int), new[]{
            typeof(int)
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        var ジャンプ = I.DefineLabel();
        ILメソッド(I, ジャンプ);
        var ILOffset = I.ILOffset;
        for (var a = 0; a < バイト数 - 2; a++)
            I.Nop();
        I.Ldc_I4_3();
        I.Ret();
        I.MarkLabel(ジャンプ);
        Assert.IsTrue(I.ILOffset - ILOffset == バイト数);
        I.Ldc_I4_4();
        I.Ret();
        return (Func<int, int>)D.CreateDelegate(typeof(Func<int, int>));
    }

    [TestMethod]
    public void Brfalse()
    {
        var M = Brfalse_Brtrue((I, ジャンプ) => I.Brfalse(ジャンプ), 128);
        Assert.AreEqual(4, M(0));
        Assert.AreEqual(3, M(1));
    }

    [TestMethod]
    public void Brfalse_S()
    {
        var M = Brfalse_Brtrue((I, ジャンプ) => I.Brfalse_S(ジャンプ), 127);
        Assert.AreEqual(4, M(0));
        Assert.AreEqual(3, M(1));
    }

    [TestMethod, ExpectedException(typeof(NotSupportedException))]
    public void Brfalse_S_NotSupportedException()
    {
        Brfalse_Brtrue((I, ジャンプ) => I.Brfalse_S(ジャンプ), 128);
    }

    [TestMethod]
    public void Brtrue()
    {
        var M = Brfalse_Brtrue((I, ジャンプ) => I.Brtrue(ジャンプ), 128);
        Assert.AreEqual(3, M(0));
        Assert.AreEqual(4, M(1));
    }

    [TestMethod]
    public void Brtrue_S()
    {
        var M = Brfalse_Brtrue((I, ジャンプ) => I.Brtrue_S(ジャンプ), 127);
        Assert.AreEqual(3, M(0));
        Assert.AreEqual(4, M(1));
    }

    [TestMethod, ExpectedException(typeof(NotSupportedException))]
    public void Brtrue_S_NotSupportedException()
    {
        Brfalse_Brtrue((I, ジャンプ) => I.Brtrue_S(ジャンプ), 128);
    }

    private static void Call(Action<ILGenerator> ILメソッド)
    {
        var D = new DynamicMethod("", typeof(string), new[]{
            typeof(string)
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        ILメソッド(I);
        I.Ret();
        var M = (Func<string, string>)D.CreateDelegate(typeof(Func<string, string>));
        Assert.AreEqual("A", M("A"));
    }

    [TestMethod]
    public void Call()
    {
        Call(I => I.Call(typeof(string).GetMethod(nameof(string.ToString), Type.EmptyTypes)!));
    }

    [TestMethod]
    public void Callvirt()
    {
        Call(I => I.Callvirt(typeof(string).GetMethod(nameof(string.ToString), Type.EmptyTypes)!));
    }

    [TestMethod]
    public void Castclass()
    {
        var D = new DynamicMethod("", typeof(string), new[]{
            typeof(object)
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        I.Castclass(typeof(string));
        I.Ret();
        var M = (Func<object, string>)D.CreateDelegate(typeof(Func<object, string>));
        Assert.AreEqual("A", M("A"));
    }

    private static Func<int, int, int> C比較(Action<ILGenerator> ILメソッド)
    {
        var D = new DynamicMethod("", typeof(int), new[]{
            typeof(int),typeof(int)
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        I.Ldarg_1();
        ILメソッド(I);
        I.Ret();
        return (Func<int, int, int>)D.CreateDelegate(typeof(Func<int, int, int>));
    }

    [TestMethod]
    public void Ceq()
    {
        var M = C比較(I => I.Ceq());
        Assert.AreEqual(1, M(0, 0));
    }

    [TestMethod]
    public void Cgt()
    {
        var M = C比較(I => I.Cgt());
        Assert.AreEqual(0, M(0, 0));
        Assert.AreEqual(0, M(0, 1));
        Assert.AreEqual(1, M(1, 0));
        Assert.AreEqual(0, M(1, 1));
        const int 小 = 0;
        const int 大 = unchecked((int)uint.MaxValue);
        Assert.AreEqual(0, M(小, 小));
        Assert.AreEqual(1, M(小, 大));
        Assert.AreEqual(0, M(大, 小));
        Assert.AreEqual(0, M(大, 大));
    }

    [TestMethod]
    public void Cgt_Un()
    {
        var M = C比較(I => I.Cgt_Un());
        Assert.AreEqual(0, M(0, 0));
        Assert.AreEqual(0, M(0, 1));
        Assert.AreEqual(1, M(1, 0));
        Assert.AreEqual(0, M(1, 1));
        const int 小 = 0;
        const int 大 = unchecked((int)uint.MaxValue);
        Assert.AreEqual(0, M(小, 小));
        Assert.AreEqual(0, M(小, 大));
        Assert.AreEqual(1, M(大, 小));
        Assert.AreEqual(0, M(大, 大));
    }

    private static Func<double, double> CreateCkfinite()
    {
        var D = new DynamicMethod("", typeof(double), new[]{
            typeof(double)
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        I.Ckfinite();
        I.Ret();
        return (Func<double, double>)D.CreateDelegate(typeof(Func<double, double>));
    }

    [TestMethod]
    public void Ckfinite()
    {
        CreateCkfinite()(3);
    }

    [TestMethod, ExpectedException(typeof(OverflowException))]
    public void Ckfinite_OverflowException()
    {
        CreateCkfinite()(double.NaN);
    }

    [TestMethod]
    public void Clt()
    {
        var M = C比較(I => I.Clt());
        Assert.AreEqual(0, M(0, 0));
        Assert.AreEqual(1, M(0, 1));
        Assert.AreEqual(0, M(1, 0));
        Assert.AreEqual(0, M(1, 1));
        const int 小 = 0;
        const int 大 = unchecked((int)uint.MaxValue);
        Assert.AreEqual(0, M(小, 小));
        Assert.AreEqual(0, M(小, 大));
        Assert.AreEqual(1, M(大, 小));
        Assert.AreEqual(0, M(大, 大));
    }

    [TestMethod]
    public void Clt_Un()
    {
        var M = C比較(I => I.Clt_Un());
        Assert.AreEqual(0, M(0, 0));
        Assert.AreEqual(1, M(0, 1));
        Assert.AreEqual(0, M(1, 0));
        Assert.AreEqual(0, M(1, 1));
        const int 小 = 0;
        const int 大 = unchecked((int)uint.MaxValue);
        Assert.AreEqual(0, M(小, 小));
        Assert.AreEqual(1, M(小, 大));
        Assert.AreEqual(0, M(大, 小));
        Assert.AreEqual(0, M(大, 大));
    }

    [TestMethod]
    public void Constrained()
    {
        var D = new DynamicMethod("", typeof(string), new[]{
            typeof(int)
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarga_S(0);
        I.Constrained(typeof(int));
        I.Callvirt(typeof(int).GetMethod(nameof(int.ToString), Type.EmptyTypes)!);
        I.Ret();
        var M = (Func<int, string>)D.CreateDelegate(typeof(Func<int, string>));
        Assert.AreEqual("2", M(2));
    }

    private static Func<TInput, TResult> Conv<TInput, TResult>(Action<ILGenerator> ILメソッド)
    {
        var D = new DynamicMethod("", typeof(TResult), new[]{
            typeof(TInput)
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        ILメソッド(I);
        I.Ret();
        return (Func<TInput, TResult>)D.CreateDelegate(typeof(Func<TInput, TResult>));
    }

    [TestMethod]
    public void Conv_I()
    {
        Assert.AreEqual((IntPtr)1, Conv<int, IntPtr>(I => I.Conv_I())(1));
    }

    [TestMethod]
    public void Conv_I1()
    {
        Assert.AreEqual((sbyte)1, Conv<int, sbyte>(I => I.Conv_I1())(1));
    }

    [TestMethod]
    public void Conv_I2()
    {
        Assert.AreEqual((short)1, Conv<int, short>(I => I.Conv_I2())(1));
    }

    [TestMethod]
    public void Conv_I4()
    {
        Assert.AreEqual((int)1, Conv<long, int>(I => I.Conv_I4())(1));
    }

    [TestMethod]
    public void Conv_I8()
    {
        Assert.AreEqual((long)1, Conv<int, long>(I => I.Conv_I8())(1));
    }

    [TestMethod]
    public void Conv_Ovf_I()
    {
        Assert.AreEqual((IntPtr)1, Conv<int, IntPtr>(I => I.Conv_Ovf_I())(1));
    }

    [TestMethod]
    public void Conv_Ovf_I_Un()
    {
        Assert.AreEqual((IntPtr)1, Conv<UIntPtr, IntPtr>(I => I.Conv_Ovf_I_Un())((UIntPtr)1));
    }

    [TestMethod, ExpectedException(typeof(OverflowException))]
    public void Conv_Ovf_I_Un_OverflowException()
    {
        Conv<UIntPtr, IntPtr>(I => I.Conv_Ovf_I_Un())(UIntPtr.Zero - 1);
    }

    [TestMethod]
    public void Conv_Ovf_I1()
    {
        Assert.AreEqual((sbyte)1, Conv<int, sbyte>(I => I.Conv_Ovf_I1())(1));
    }

    [TestMethod]
    public void Conv_Ovf_I1_Un()
    {
        Assert.AreEqual((sbyte)1, Conv<byte, sbyte>(I => I.Conv_Ovf_I1_Un())(1));
    }

    [TestMethod, ExpectedException(typeof(OverflowException))]
    public void Conv_Ovf_I1_Un_OverflowException()
    {
        Conv<byte, sbyte>(I => I.Conv_Ovf_I1_Un())(unchecked((byte)(-1)));
    }

    [TestMethod]
    public void Conv_Ovf_I2()
    {
        Assert.AreEqual((short)1, Conv<int, short>(I => I.Conv_Ovf_I2())(1));
    }

    [TestMethod]
    public void Conv_Ovf_I2_Un()
    {
        Assert.AreEqual((short)1, Conv<ushort, short>(I => I.Conv_Ovf_I2_Un())(1));
    }

    [TestMethod, ExpectedException(typeof(OverflowException))]
    public void Conv_Ovf_I2_Un_OverflowException()
    {
        Conv<ushort, short>(I => I.Conv_Ovf_I2_Un())(unchecked((ushort)(-1)));
    }

    [TestMethod]
    public void Conv_Ovf_I4()
    {
        Assert.AreEqual((int)1, Conv<long, int>(I => I.Conv_Ovf_I4())(1));
    }

    [TestMethod]
    public void Conv_Ovf_I4_Un()
    {
        Assert.AreEqual((int)1, Conv<uint, int>(I => I.Conv_Ovf_I4_Un())(1));
    }

    [TestMethod, ExpectedException(typeof(OverflowException))]
    public void Conv_Ovf_I4_Un_OverflowException()
    {
        Conv<uint, int>(I => I.Conv_Ovf_I4_Un())(unchecked((uint)(-1)));
    }

    [TestMethod]
    public void Conv_Ovf_I8()
    {
        Assert.AreEqual(1L, Conv<int, long>(I => I.Conv_Ovf_I8())(1));
    }

    [TestMethod]
    public void Conv_Ovf_I8_Un()
    {
        Assert.AreEqual(1L, Conv<ulong, long>(I => I.Conv_Ovf_I8_Un())(1UL));
    }

    [TestMethod, ExpectedException(typeof(OverflowException))]
    public void Conv_Ovf_I8_Un_OverflowException()
    {
        Conv<ulong, long>(I => I.Conv_Ovf_I8_Un())(ulong.MaxValue);
    }

    [TestMethod]
    public void Conv_Ovf_U()
    {
        Assert.AreEqual(Conv<int, UIntPtr>(I => I.Conv_Ovf_U())(1), (UIntPtr)1);
    }

    [TestMethod]
    public void Conv_Ovf_U_Un()
    {
        Assert.AreEqual(Conv<uint, UIntPtr>(I => I.Conv_Ovf_U_Un())(1U), (UIntPtr)1);
    }

    [TestMethod]
    public void Conv_Ovf_U_Un_OverflowException()
    {
        if (UIntPtr.Size == 8)
        {
            Conv<ulong, UIntPtr>(I => I.Conv_Ovf_U_Un())(ulong.MaxValue);
        }
        else
        {
            try
            {
                Assert.AreEqual(Conv<ulong, UIntPtr>(I => I.Conv_Ovf_U_Un())(ulong.MaxValue), unchecked((UIntPtr)(-1)));
            }
            catch (OverflowException) { }
        }
    }

    [TestMethod]
    public void Conv_Ovf_U1()
    {
        Assert.AreEqual(Conv<ulong, byte>(I => I.Conv_Ovf_U())(1UL), (byte)1);
    }

    [TestMethod]
    public void Conv_Ovf_U1_Un()
    {
        Assert.AreEqual(Conv<ulong, byte>(I => I.Conv_Ovf_U_Un())(1UL), (byte)1);
    }

    [TestMethod, ExpectedException(typeof(OverflowException))]
    public void Conv_Ovf_U1_Un_OverflowException()
    {
        Conv<ulong, byte>(I => I.Conv_Ovf_U1_Un())(ulong.MaxValue);
    }

    [TestMethod]
    public void Conv_Ovf_U2()
    {
        Assert.AreEqual(Conv<ulong, ushort>(I => I.Conv_Ovf_U2())(1UL), (ushort)1);
    }

    [TestMethod]
    public void Conv_Ovf_U2_Un()
    {
        Assert.AreEqual(Conv<ulong, ushort>(I => I.Conv_Ovf_U2_Un())(1UL), (ushort)1);
    }

    [TestMethod, ExpectedException(typeof(OverflowException))]
    public void Conv_Ovf_U2_Un_OverflowException()
    {
        Conv<ulong, ushort>(I => I.Conv_Ovf_U2_Un())(ulong.MaxValue);
    }

    [TestMethod]
    public void Conv_Ovf_U4()
    {
        Assert.AreEqual(Conv<ulong, uint>(I => I.Conv_Ovf_U4())(1UL), 1U);
    }

    [TestMethod]
    public void Conv_Ovf_U4_Un()
    {
        Assert.AreEqual(Conv<ulong, uint>(I => I.Conv_Ovf_U4_Un())(1UL), 1U);
    }

    [TestMethod, ExpectedException(typeof(OverflowException))]
    public void Conv_Ovf_U4_Un_OverflowException()
    {
        Conv<ulong, uint>(I => I.Conv_Ovf_U4_Un())(ulong.MaxValue);
    }

    [TestMethod]
    public void Conv_Ovf_U8()
    {
        Assert.AreEqual(Conv<long, ulong>(I => I.Conv_Ovf_U8())(1L), (ulong)1);
    }

    [TestMethod]
    public void Conv_Ovf_U8_Un()
    {
        Assert.AreEqual(Conv<uint, ulong>(I => I.Conv_Ovf_U8_Un())(1U), (ulong)1);
    }

    [TestMethod]
    public void Conv_R_Un()
    {
        Assert.AreEqual(Conv<uint, float>(I => I.Conv_R_Un())(1U), 1F);
    }

    [TestMethod]
    public void Conv_R4()
    {
        Assert.AreEqual(Conv<int, float>(I => I.Conv_R4())(1), 1F);
    }

    [TestMethod]
    public void Conv_R8()
    {
        Assert.AreEqual(Conv<int, double>(I => I.Conv_R8())(1), 1D);
    }

    [TestMethod]
    public void Conv_U()
    {
        Assert.AreEqual(Conv<IntPtr, UIntPtr>(I => I.Conv_U())(IntPtr.Subtract(IntPtr.Zero, 1)), UIntPtr.Subtract(UIntPtr.Zero, 1));
    }

    [TestMethod]
    public void Conv_U1()
    {
        Assert.AreEqual(Conv<sbyte, byte>(I => I.Conv_U1())(-1), byte.MaxValue);
    }

    [TestMethod]
    public void Conv_U2()
    {
        Assert.AreEqual(Conv<short, ushort>(I => I.Conv_U2())(-1), ushort.MaxValue);
    }

    [TestMethod]
    public void Conv_U4()
    {
        Assert.AreEqual(Conv<int, uint>(I => I.Conv_U4())(-1), uint.MaxValue);
    }
    [TestMethod]
    public void Conv_U8()
    {
        Assert.AreEqual(Conv<long, ulong>(I => I.Conv_U8())(-1), ulong.MaxValue);
    }

    private static void Cpblk<T>(T[] コピー元)
    {
        var D = new DynamicMethod("", typeof(void), new[]{
            typeof(T[]),typeof(T[]),typeof(int)
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        I.Ldc_I4_0();
        I.Ldelema(typeof(T));
        I.Ldarg_1();
        I.Ldc_I4_0();
        I.Ldelema(typeof(T));
        I.Ldarg_2();
        I.Cpblk();
        I.Ret();
        var M = (Action<T[], T[], int>)D.CreateDelegate(typeof(Action<T[], T[], int>));
        var 要素数 = コピー元.Length;
        var バイト数 = 要素数 * DynamicSizeof<T>();
        var コピー先 = new T[要素数];
        M(コピー先, コピー元, バイト数);
        Assert.IsTrue(((IStructuralEquatable)コピー先).Equals(コピー元, StructuralComparisons.StructuralEqualityComparer));
    }

    [TestMethod]
    public void Cpblk()
    {
        const int 要素数 = 65536;
        {
            var コピー元 = new byte[要素数];
            for (var a = 0; a < 要素数; a++)
                コピー元[a] = (byte)a;
            Cpblk(コピー元);
        }
        {
            var コピー元 = new short[要素数];
            for (var a = 0; a < 要素数; a++)
                コピー元[a] = (short)a;
            Cpblk(コピー元);
        }
        {
            var コピー元 = new int[要素数];
            for (var a = 0; a < 要素数; a++)
                コピー元[a] = (int)a;
            Cpblk(コピー元);
        }
        {
            var コピー元 = new long[要素数];
            for (var a = 0; a < 要素数; a++)
                コピー元[a] = (long)a;
            Cpblk(コピー元);
        }
    }
    private delegate void void_refTrefT<T>(ref T コピー先, ref T コピー元);
    private static void struct型をCpobj<T>(T コピー元) where T : struct
    {
        var D = new DynamicMethod("", typeof(void), new[]{
            typeof(T).MakeByRefType(),typeof(T).MakeByRefType()
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg(0);
        I.Ldarg(1);
        I.Cpobj(typeof(T));
        I.Ret();
        var M = (void_refTrefT<T>)D.CreateDelegate(typeof(void_refTrefT<T>));
        var コピー先 = Activator.CreateInstance<T>();
        M(ref コピー先, ref コピー元);
        Assert.AreEqual(コピー先, コピー元);
    }
    private delegate void void_TT<in T>(T コピー先, T コピー元);
    private readonly struct SCpobj : IEquatable<SCpobj>
    {
        private readonly int a;
        private readonly string b;
        public SCpobj(int a, string b)
        {
            this.a = a;
            this.b = b;
        }
        public bool Equals(SCpobj other) => this.a == other.a && this.b == other.b;
        public override bool Equals(object? obj) => obj is SCpobj cpobj && this.Equals(cpobj);
        public override int GetHashCode() => this.a;
    }
    [TestMethod]
    public void struct型をCpobj()
    {
        struct型をCpobj(new SCpobj(1, "a"));
    }
    [TestMethod]
    public void Div()
    {
        var M = 二項演算子<int, int>(I => I.Div());
        for (var a = 1; a <= 10; a++)
        {
            for (var b = 1; b <= 10; b++)
            {
                Assert.AreEqual(a / b, M(a, b));
            }
        }
    }

    [TestMethod]
    public void Div_Un()
    {
        var M = 二項演算子<uint, uint>(I => I.Div_Un());
        for (var a = 1U; a <= 10U; a++)
        {
            for (var b = 1U; b <= 10U; b++)
            {
                Assert.AreEqual(a / b, M(a, b));
            }
        }
    }

    [TestMethod]
    public void Dup()
    {
        var D = new DynamicMethod("", typeof(int), new[]{
            typeof(int)
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        I.Dup();
        I.Add();
        I.Ret();
        Assert.AreEqual(((Func<int, int>)D.CreateDelegate(typeof(Func<int, int>)))(1), 2);
    }

    [TestMethod]
    public void Endfinally()
    {
        var D = new DynamicMethod("", typeof(void), Type.EmptyTypes, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.BeginExceptionBlock();
        I.EmitWriteLine("ExceptionBlock");
        I.BeginFinallyBlock();
        I.EmitWriteLine("FinallyBlock");
        I.Endfinally();
        I.EndExceptionBlock();
        I.Ret();
        ((Action)D.CreateDelegate(typeof(Action)))();
    }

    private delegate void DelegateInitblk(ref byte 開始アドレス, byte 初期値, int バイト数);

    [TestMethod]
    public void Initblk()
    {
        const int 要素数 = 100;
        var D = new DynamicMethod("", typeof(void), new[]{
            typeof(byte).MakeByRefType(),typeof(byte),typeof(int)
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        I.Ldarg_1();
        I.Ldarg_2();
        I.Initblk();
        I.Ret();
        var 初期化したいBytes = new byte[要素数];
        ((DelegateInitblk)D.CreateDelegate(typeof(DelegateInitblk)))(ref 初期化したいBytes[0], 2, 要素数);
        for (var a = 0; a < 要素数; a++)
        {
            Assert.AreEqual(初期化したいBytes[a], (byte)2);
        }
    }

    private delegate void void_refT<T>(ref T 初期化したい値);

    private static void InitObj<T>(ref T 初期化したい値)
    {
        var D = new DynamicMethod("", typeof(void), new[]{
            typeof(T).MakeByRefType()
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        I.Initobj(typeof(T));
        I.Ret();
        ((void_refT<T>)D.CreateDelegate(typeof(void_refT<T>)))(ref 初期化したい値);
    }

    [TestMethod]
    public void Initobj()
    {
        sbyte SByte = 1;
        InitObj(ref SByte);
        Assert.AreEqual(SByte, default);
        var Decimal = 1m;
        InitObj(ref Decimal);
        Assert.AreEqual(Decimal, default);
    }

    private static bool Isinst<T0, T1>(T0 入力) where T0 : class
    {
        var D = new DynamicMethod("", typeof(bool), new[]{
            typeof(T0)
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        I.Isinst(typeof(T1));
        I.Ret();
        var M = (Func<T0, bool>)D.CreateDelegate(typeof(Func<T0, bool>));
        return M(入力);
        //Assert.IsTrue(M(入力));
    }

    [TestMethod]
    public void Isinst()
    {
        Assert.IsTrue(Isinst<object, string>(""));
        Assert.IsFalse(Isinst<string, int>(""));
        Assert.IsTrue(Isinst<string, string>(""));
    }

    private static int Jmpで呼び出されるメソッド(int v) => v;

    [TestMethod]
    public void Jmp()
    {
        var D = new DynamicMethod("D0", typeof(int), new[]{
            typeof(int)
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Jmp(typeof(Test_ILGenerator拡張メソッド).GetMethod(nameof(Jmpで呼び出されるメソッド),BindingFlags.Static | BindingFlags.NonPublic)!);
        I.Ret();
        Assert.AreEqual(((Func<int, int>)D.CreateDelegate(typeof(Func<int, int>)))(10), 10);
    }

    private static void Ldarg(ushort index) => Ldarg(I => I.Ldarg(index), index);

    [TestMethod]
    public void Ldarg()
    {
        Ldarg(0);
        Ldarg(1);
        Ldarg(2);
        Ldarg(3);
        Ldarg(4);
        Ldarg(255);
        Ldarg(256);
        Ldarg(16382);
    }
    private static void Ldarg(Action<ILGenerator> ILメソッド, int index)
    {
        var Types = new Type[index + 1];
        for (var a = 0; a <= index; a++)
            Types[a] = typeof(int);
        var D = new DynamicMethod("D0", typeof(int), Types, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        ILメソッド(I);
        I.Ret();
        var parameters = new object[index + 1];
        parameters[index] = 1;
        Assert.AreEqual(D.Invoke(null, parameters), 1);
    }
    [TestMethod] public void Ldarg_0() => Ldarg(I => I.Ldarg_0(), 0);
    [TestMethod] public void Ldarg_1() => Ldarg(I => I.Ldarg_1(), 1);
    [TestMethod] public void Ldarg_2() => Ldarg(I => I.Ldarg_2(), 2);
    [TestMethod] public void Ldarg_3() => Ldarg(I => I.Ldarg_3(), 3);
    [TestMethod] public void Ldarg_S() => Ldarg(I => I.Ldarg_S(4), 4);

    private static void Ldarga(Action<ILGenerator> ILメソッド, int index)
    {
        var Types = new Type[index + 1];
        for (var a = 0; a <= index; a++)
            Types[a] = typeof(int);
        var D = new DynamicMethod("D0", typeof(string), Types, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        ILメソッド(I);
        I.Constrained(typeof(int));
        I.Callvirt(typeof(int).GetMethod(nameof(int.ToString), Type.EmptyTypes)!);
        I.Ret();
        var parameters = new object[index + 1];
        parameters[index] = 1;
        Assert.AreEqual(D.Invoke(null, parameters), "1");
    }

    [TestMethod]
    public void Ldarga()
    {
        Ldarga(I => I.Ldarga(0), 0);
        Ldarga(I => I.Ldarga(255), 255);
        Ldarga(I => I.Ldarga(256), 256);
        Ldarga(I => I.Ldarga(16382), 16382);
    }

    [TestMethod]
    public void Ldarga_S()
    {
        Ldarga(I => I.Ldarga_S(0), 0);
        Ldarga(I => I.Ldarga_S(255), 255);
    }

    private static void Ldc_I4(int expected) => Ldc(I => I.Ldc_I4(expected), expected);

    [TestMethod]
    public void Ldc_I4()
    {
        Ldc_I4(-1);
        Ldc_I4(0);
        Ldc_I4(1);
        Ldc_I4(2);
        Ldc_I4(3);
        Ldc_I4(4);
        Ldc_I4(5);
        Ldc_I4(6);
        Ldc_I4(7);
        Ldc_I4(8);
        Ldc_I4(-128);
        Ldc_I4(127);
        Ldc_I4(-129);
        Ldc_I4(128);
    }

    private static void Ldc<T>(Action<ILGenerator> ILメソッド, T expected)
    {
        var D = new DynamicMethod("", typeof(T), Type.EmptyTypes, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        ILメソッド(I);
        I.Ret();
        Assert.AreEqual(((Func<T>)D.CreateDelegate(typeof(Func<T>)))(), expected);
    }

    [TestMethod] public void Ldc_I4_0() => Ldc(I => I.Ldc_I4_0(), 0);
    [TestMethod] public void Ldc_I4_1() => Ldc(I => I.Ldc_I4_1(), 1);
    [TestMethod] public void Ldc_I4_2() => Ldc(I => I.Ldc_I4_2(), 2);
    [TestMethod] public void Ldc_I4_3() => Ldc(I => I.Ldc_I4_3(), 3);
    [TestMethod] public void Ldc_I4_4() => Ldc(I => I.Ldc_I4_4(), 4);
    [TestMethod] public void Ldc_I4_5() => Ldc(I => I.Ldc_I4_5(), 5);
    [TestMethod] public void Ldc_I4_6() => Ldc(I => I.Ldc_I4_6(), 6);
    [TestMethod] public void Ldc_I4_7() => Ldc(I => I.Ldc_I4_7(), 7);
    [TestMethod] public void Ldc_I4_8() => Ldc(I => I.Ldc_I4_8(), 8);
    [TestMethod] public void Ldc_I4_M1() => Ldc(I => I.Ldc_I4_M1(), -1);
    [TestMethod] public void Ldc_I4_S() => Ldc(I => I.Ldc_I4_S(-128), -128);
    [TestMethod] public void Ldc_I8() => Ldc(I => I.Ldc_I8(1000), 1000L);
    [TestMethod] public void Ldc_R4() => Ldc(I => I.Ldc_R4(1000), 1000f);
    [TestMethod] public void Ldc_R8() => Ldc(I => I.Ldc_R8(1000), 1000d);
    private static void Ldelem<T>(T expected) => Ldelem(I => I.Ldelem(typeof(T)), expected);

    private static void Ldelem<T>(Action<ILGenerator> ILメソッド, T expected)
    {
        var 配列 = new[]{
            expected
        };
        var D = new DynamicMethod("", typeof(T), new[]{
            typeof(T[])
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        I.Ldc_I4_0();
        ILメソッド(I);
        I.Ret();
        Assert.AreEqual(((Func<T[], T>)D.CreateDelegate(typeof(Func<T[], T>)))(配列), expected);
    }

    [TestMethod]
    public void Ldelem()
    {
        Ldelem((IntPtr)1);
        Ldelem((sbyte)1);
        Ldelem((short)1);
        Ldelem(1);
        Ldelem(1L);
        Ldelem(1f);
        Ldelem(1d);
        Ldelem("1");
        Ldelem((byte)1);
        Ldelem((ushort)1);
        Ldelem(1U);
        Ldelem(1m);
    }
    [TestMethod] public void Ldelem_I() => Ldelem(I => I.Ldelem_I(), (IntPtr)1);
    [TestMethod] public void Ldelem_I1() => Ldelem(I => I.Ldelem_I1(), (sbyte)1);
    [TestMethod] public void Ldelem_I2() => Ldelem(I => I.Ldelem_I2(), (short)1);
    [TestMethod] public void Ldelem_I4() => Ldelem(I => I.Ldelem_I4(), 1);
    [TestMethod] public void Ldelem_I8() => Ldelem(I => I.Ldelem_I8(), (long)1);
    [TestMethod] public void Ldelem_R4() => Ldelem(I => I.Ldelem_R4(), 1f);
    [TestMethod] public void Ldelem_R8() => Ldelem(I => I.Ldelem_R8(), 1d);
    [TestMethod] public void Ldelem_Ref() => Ldelem(I => I.Ldelem_Ref(), "ABC");
    [TestMethod] public void Ldelem_U1() => Ldelem(I => I.Ldelem_U1(), (byte)1);
    [TestMethod] public void Ldelem_U2() => Ldelem(I => I.Ldelem_U2(), (ushort)1);
    [TestMethod] public void Ldelem_U4() => Ldelem(I => I.Ldelem_U4(), 1U);
    private static void Lda<T>(Action<ILGenerator> ILメソッド, T 入力)
    {
        var D = new DynamicMethod("", typeof(void), new[] { typeof(T) }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        ILメソッド(I);
        I.Stobj(typeof(T));
        I.Ret();
        ((Action<T>)D.CreateDelegate(typeof(Action<T>)))(入力);
    }

    [TestMethod]
    public void Ldelema()
    {
        const int expected = 3;
        var 配列 = new int[1];
        Lda(I =>
        {
            I.Ldc_I4_0();
            I.Ldelema(typeof(int));
            I.Ldc_I4(expected);
        }, 配列);
        Assert.AreEqual(expected, 配列[0]);
    }

    private static FieldInfo Field取得(string Name){
        var Field=typeof(Test_ILGenerator拡張メソッド).GetField(Name,ConstBindingFlags);
        Debug.Assert(Field!=null,nameof(Field)+" != null");
        return Field;
    }
    private static void Ld<TInput, TResult>(Action<ILGenerator> ILメソッド, TInput 入力, TResult expected)
    {
        var D = new DynamicMethod("", typeof(TResult), new[] { typeof(TInput) }, typeof(Test_ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        ILメソッド(I);
        I.Ret();
        Assert.AreEqual(expected, ((Func<TInput, TResult>)D.CreateDelegate(typeof(Func<TInput, TResult>)))(入力));
    }
    private int Ldfld用Field;
    [TestMethod]
    public void Ldfld()
    {
        const int expected = 3;
        this.Ldfld用Field = expected;
        Ld(I => I.Ldfld(Field取得(nameof(this.Ldfld用Field))), this, expected);
    }
    [TestMethod]
    public void Ldflda()
    {
        const int expected = 3;
        this.Ldfld用Field = 0;
        Lda(I =>
        {
            I.Ldflda(Field取得(nameof(this.Ldfld用Field)));
            I.Ldc_I4(expected);
        }, this);
        Assert.AreEqual(expected, this.Ldfld用Field);
    }
    private void Ldind<T>(Action<ILGenerator> ILメソッド, string Field名, T expected)
    {
        var Field = Field取得(Field名);
        Contract.Assert(Field != null, "Field != null");
        Field.SetValue(this, expected);
        Ld(I =>
        {
            I.Ldflda(Field);
            ILメソッド(I);
        }, this, expected);
    }
    private readonly IntPtr Ldind_I用Field;
    [TestMethod] public void Ldind_I() => this.Ldind(I => I.Ldind_I(), nameof(this.Ldind_I用Field), (IntPtr)3);
    private sbyte Ldind_I1用Field;
    [TestMethod] public void Ldind_I1() => this.Ldind(I => I.Ldind_I1(), nameof(this.Ldind_I1用Field), (sbyte)3);
    private short Ldind_I2用Field;
    [TestMethod] public void Ldind_I2() => this.Ldind(I => I.Ldind_I2(), nameof(this.Ldind_I2用Field), (short)3);
    private int Ldind_I4用Field;
    [TestMethod] public void Ldind_I4() => this.Ldind(I => I.Ldind_I4(), nameof(this.Ldind_I4用Field), (int)3);
    private long Ldind_I8用Field;
    [TestMethod] public void Ldind_I8() => this.Ldind(I => I.Ldind_I8(), nameof(this.Ldind_I8用Field), (long)3);
    private float Ldind_R4用Field;
    [TestMethod] public void Ldind_R4() => this.Ldind(I => I.Ldind_R4(), nameof(this.Ldind_R4用Field), (float)3);
    private double Ldind_R8用Field;
    [TestMethod] public void Ldind_R8() => this.Ldind(I => I.Ldind_R8(), nameof(this.Ldind_R8用Field), (double)3);
    private object Ldind_Ref用Field;
    [TestMethod] public void Ldind_Ref() => this.Ldind(I => I.Ldind_Ref(), nameof(this.Ldind_Ref用Field), "ABC");
    private byte Ldind_U1用Field;
    [TestMethod] public void Ldind_U1() => this.Ldind(I => I.Ldind_U1(), nameof(this.Ldind_U1用Field), (byte)3);
    private ushort Ldind_U2用Field;
    [TestMethod] public void Ldind_U2() => this.Ldind(I => I.Ldind_U2(), nameof(this.Ldind_U2用Field), (ushort)3);
    private uint Ldind_U4用Field;
    [TestMethod] public void Ldind_U4() => this.Ldind(I => I.Ldind_U4(), nameof(this.Ldind_U4用Field), (uint)3);
    [TestMethod]
    public void Ldlen()
    {
        var D = new DynamicMethod("", typeof(int), new[] { typeof(int[]) }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        I.Ldlen();
        I.Ret();
        const int expected = 2;
        Assert.AreEqual(expected, ((Func<int[], int>)D.CreateDelegate(typeof(Func<int[], int>)))(new int[expected]));
    }
    private static void Ldloc(Action<ILGenerator, LocalBuilder> ILメソッド, int index)
    {
        var D = new DynamicMethod("", typeof(int), new[] { typeof(int) }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        var Locals = new LocalBuilder[index + 1];
        for (var a = 0; a <= index; a++)
        {
            Locals[a] = I.DeclareLocal(typeof(int));
        }
        var L = Locals[index];
        I.Stloc(L);
        ILメソッド(I, L);
        I.Ret();
        const int expected = 2;
        Assert.AreEqual(expected, ((Func<int, int>)D.CreateDelegate(typeof(Func<int, int>)))(expected));
    }
    [TestMethod] public void Ldloc() => Ldloc((I, L) => I.Ldloc(L), 0);
    [TestMethod] public void Ldloc_0() => Ldloc((I, _) => I.Ldloc_0(), 0);
    [TestMethod] public void Ldloc_1() => Ldloc((I, _) => I.Ldloc_1(), 1);
    [TestMethod] public void Ldloc_2() => Ldloc((I, _) => I.Ldloc_2(), 2);
    [TestMethod] public void Ldloc_3() => Ldloc((I, _) => I.Ldloc_3(), 3);
    [TestMethod] public void Ldloc_S() => Ldloc((I, L) => I.Ldloc_S(L), 4);
    private static void Ldloca(Action<ILGenerator, LocalBuilder> ILメソッド, int index)
    {
        var D = new DynamicMethod("", typeof(string), new[] { typeof(int) }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        var Locals = new LocalBuilder[index + 1];
        for (var a = 0; a <= index; a++)
        {
            Locals[a] = I.DeclareLocal(typeof(int));
        }
        var L = Locals[index];
        I.Stloc(L);
        ILメソッド(I, L);
        I.Constrained(typeof(int));
        I.Callvirt(typeof(int).GetMethod(nameof(int.ToString), Type.EmptyTypes)!);
        I.Ret();
        const int expected = 2;
        Assert.AreEqual(expected.ToString(), ((Func<int, string>)D.CreateDelegate(typeof(Func<int, string>)))(expected));
    }
    [TestMethod]
    public void Ldloca()
    {
        Ldloca((I, L) => I.Ldloca(L), 255);
        Ldloca((I, L) => I.Ldloca(L), 256);
    }
    [TestMethod] public void Ldloca_S() => Ldloca((I, L) => I.Ldloca_S(L), 255);

    [TestMethod]
    public void Ldnull()
    {
        var D = new DynamicMethod("", typeof(object), Type.EmptyTypes, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldnull();
        I.Ret();
        Assert.IsNull(((Func<object>)D.CreateDelegate(typeof(Func<object>)))());
    }
    private delegate T T_refT<T>(ref T コピー先);
    private static void Ldobj<T>(T v)
    {
        var D = new DynamicMethod("", typeof(T), new[] { typeof(T).MakeByRefType() }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        I.Ldobj(typeof(T));
        I.Ret();
        Assert.AreEqual(v, ((T_refT<T>)D.CreateDelegate(typeof(T_refT<T>)))(ref v));
    }
    [TestMethod]
    public void Ldobj()
    {
        Ldobj((IntPtr)1);
        Ldobj((sbyte)1);
        Ldobj((short)1);
        Ldobj(1);
        Ldobj((long)1);
        Ldobj((float)1);
        Ldobj((double)1);
        Ldobj("ABC");
        Ldobj((byte)1);
        Ldobj((ushort)1);
        Ldobj(1U);
        Ldobj(1m);
    }
    private static void Ldstfld<TResult>(Action<ILGenerator> ILメソッド, TResult expected)
    {
        var D = new DynamicMethod("", typeof(TResult), Type.EmptyTypes, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        ILメソッド(I);
        I.Ret();
        Assert.AreEqual(expected, ((Func<TResult>)D.CreateDelegate(typeof(Func<TResult>)))());
    }
    private const BindingFlags ConstBindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic;
    private static int StaticInt32Field;
    private static string StaticStringField;
    private static decimal StaticDecimalField;
    [TestMethod]
    public void Ldsfld()
    {
        {
            const int expected = 3;
            StaticInt32Field = expected;
            Ldstfld(I => I.Ldsfld(Field取得(nameof(StaticInt32Field))), expected);
        }
        {
            const string expected = "ABC";
            StaticStringField = expected;
            Ldstfld(I => I.Ldsfld(Field取得(nameof(StaticStringField))), expected);
        }
        {
            const decimal expected = 3m;
            StaticDecimalField = expected;
            Ldstfld(I => I.Ldsfld(Field取得(nameof(StaticDecimalField))), expected);
        }
    }
    private static void Ldsflda<T>(T expected, string Field名)
    {
        var D = new DynamicMethod("", typeof(void), new[] { typeof(T) }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var Field = Field取得(Field名);
        Contract.Assert(Field != null, "Field != null");
        Field.SetValue(null, expected);
        var I = D.GetILGenerator();
        I.Ldsflda(Field);
        I.Ldarg_0();
        I.Stobj(typeof(T));
        I.Ret();
        ((Action<T>)D.CreateDelegate(typeof(Action<T>)))(expected);
        Assert.AreEqual(Field.GetValue(null), expected);
    }
    [TestMethod]
    public void Ldsflda()
    {
        {
            const int expected = 2;
            StaticInt32Field = expected;
            Ldsflda(expected, nameof(StaticInt32Field));
        }
        {
            const string expected = "ABC";
            StaticStringField = expected;
            Ldsflda(expected, nameof(StaticStringField));
        }
        {
            const decimal expected = 2;
            StaticDecimalField = expected;
            Ldsflda(expected, nameof(StaticDecimalField));
        }
    }
    [TestMethod]
    public void Ldstr()
    {
        var D = new DynamicMethod("", typeof(string), Type.EmptyTypes, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        const string expected = "ABC";
        I.Ldstr(expected);
        I.Ret();
        Assert.AreEqual(expected, ((Func<string>)D.CreateDelegate(typeof(Func<string>)))());
    }
    private void Ldtoken<T>(Action<ILGenerator> ILメソッド, T metadata)
    {
        var D = new DynamicMethod("", typeof(T), Type.EmptyTypes, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        ILメソッド(I);
        I.Ret();
        Assert.AreEqual(((Func<T>)D.CreateDelegate(typeof(Func<T>)))(), metadata);
    }
    [TestMethod]
    public void Ldtoken()
    {
        var Constructor = typeof(string).GetConstructors()[0];
        this.Ldtoken(I => I.M_Metadata(Constructor), Constructor);
        var Method = typeof(string).GetMethods()[0];
        this.Ldtoken(I => I.M_Metadata(Method), Method);
        var Field = Field取得(nameof(this.Ldfld用Field));
        this.Ldtoken(I => I.M_Metadata(Field), Field);
    }
    [TestMethod]
    public void Localloc()
    {
        var D = new DynamicMethod("", typeof(IntPtr), Type.EmptyTypes, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldc_I4(40);
        I.Localloc();
        I.Ret();
        Assert.IsNotNull(((Func<IntPtr>)D.CreateDelegate(typeof(Func<IntPtr>)))());
    }
    [TestMethod] public void Mkrefany() => this.Refanyval();
    [TestMethod]
    public void Mul()
    {
        var M = 二項演算子<int, int>(I => I.Mul());
        for (var a = int.MaxValue - 5; a < int.MaxValue - 1; a++)
        {
            for (var b = int.MaxValue - 5; b < int.MaxValue - 1; b++)
            {
                Assert.AreEqual(a * b, M(a, b));
            }
        }
    }
    [TestMethod]
    public void Mul_Ovf()
    {
        var M = 二項演算子<int, int>(I => I.Mul_Ovf());
        for (var a = int.MaxValue - 5; a < int.MaxValue - 1; a++)
        {
            for (var b = int.MaxValue - 5; b < int.MaxValue - 1; b++)
            {
                try
                {
                    var expected = checked(a + b);
                    Assert.AreEqual(expected, M(a, b));
                    Assert.Fail("OverflowExceptionが発生するべき");
                }
                catch (OverflowException)
                {
                    try
                    {
                        M(a, b);
                        Assert.Fail("OverflowExceptionが発生するべき");
                    }
                    catch (OverflowException) { }
                }
            }
        }
    }
    [TestMethod]
    public void Mul_Ovf_Un()
    {
        var M = 二項演算子<uint, uint>(I => I.Mul_Ovf_Un());
        for (var a = uint.MaxValue - 5; a < uint.MaxValue - 1; a++)
        {
            for (var b = uint.MaxValue - 5; b < uint.MaxValue - 1; b++)
            {
                try
                {
                    var expected = checked(a + b);
                    Assert.AreEqual(expected, M(a, b));
                    Assert.Fail("OverflowExceptionが発生するべき");
                }
                catch (OverflowException)
                {
                    try
                    {
                        var actual = M(a, b);
                        Assert.Fail("OverflowExceptionが発生するべき");
                    }
                    catch (OverflowException) { }
                }
            }
        }
    }
    private static Func<TInput, TResult> 単項演算子<TInput, TResult>(Action<ILGenerator> ILメソッド)
    {
        var D = new DynamicMethod("", typeof(TResult), new[]{
            typeof(TInput)
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        ILメソッド(I);
        I.Ret();
        return (Func<TInput, TResult>)D.CreateDelegate(typeof(Func<TInput, TResult>));
    }
    [TestMethod]
    public void Neg()
    {
        var M = 単項演算子<int, int>(I => I.Neg());
        for (var a = int.MaxValue - 5; a < int.MaxValue - 1; a++)
        {
            Assert.AreEqual(-a, M(a));
        }
    }
    [TestMethod]
    public void Newarr()
    {
        var M = 単項演算子<int, byte[]>(I => I.Newarr(typeof(byte)));
        Assert.AreEqual(M(10).Length, 10);
    }
    [TestMethod]
    public void Newobj()
    {
        var M = 単項演算子<char[], byte[]>(I => I.Newobj(typeof(string).GetConstructor(new[] { typeof(char[]) })!));
        Assert.AreEqual(M(new[] { 'a', 'b', 'c' }), "abc");
    }
    [TestMethod]
    public void Nop()
    {
        var M = 単項演算子<int, int>(I => I.Nop());
        const int テスト値 = 123;
        Assert.AreEqual(M(テスト値), テスト値);
    }
    [TestMethod]
    public void Not()
    {
        var M = 単項演算子<int, int>(I => I.Not());
        const int テスト値 = 123;
        Assert.AreEqual(M(テスト値), ~テスト値);
    }
    [TestMethod]
    public void Or()
    {
        var M = 二項演算子<int, int>(I => I.Or());
        for (var a = int.MaxValue - 5; a < int.MaxValue - 1; a++)
        {
            for (var b = int.MaxValue - 5; b < int.MaxValue - 1; b++)
            {
                Assert.AreEqual(a | b, M(a, b));
            }
        }
    }
    [TestMethod]
    public void Pop()
    {
        var M = 単項演算子<int, int>(I =>
        {
            I.Dup();
            I.Pop();
        });
        const int テスト値 = 123;
        Assert.AreEqual(M(テスト値), テスト値);
    }
    [TestMethod]
    public void Readonly()
    {
        const int 要素数 = 10;
        var D = new DynamicMethod("", typeof(int), new[]{
            typeof(object[])
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        var result = I.DeclareLocal(typeof(int));
        I.Ldc_I4_0();
        I.Stloc(result);
        var a = I.DeclareLocal(typeof(int));
        I.Ldc_I4_0();
        I.Stloc(a);
        var 開始 = I.DefineLabel();
        I.Br(開始);
        var 先頭 = I.M_DefineLabel_MarkLabel();
        I.Ldarg_0();
        I.Ldloc(a);
        I.Readonly();
        I.Ldelema(typeof(int));
        I.Ldind_I4();
        I.Ldloc(result);
        I.Add();
        I.Stloc(result);
        I.Ldloc(a);
        I.Ldc_I4_1();
        I.Add();
        I.Stloc(a);
        I.MarkLabel(開始);
        I.Ldloc(a);
        I.Ldc_I4(要素数);
        I.Blt(先頭);
        I.Ldloc(result);
        I.Ret();
        var 入力 = new object[要素数];
        for (var b = 0; b < 要素数; b++)
            入力[b] = b;
        var M = (Func<object[], int>)D.CreateDelegate(typeof(Func<object[], int>));
        M(入力);
    }
    [TestMethod]
    public void Refanytype()
    {
        //var D = new DynamicMethod("",typeof(Object),new[] { typeof(String) },typeof(Test_ILGenerator拡張メソッド),true) {
        //    InitLocals=false
        //};
        //var I = D.GetILGenerator();
        //I.Ldarga(0);
        //I.Mkrefany(typeof(String));//TypedReference
        //I.Refanyval(typeof(String));
        //I.Ldobj(typeof(String));
        //I.Ret();
        //Assert.AreEqual(((Func<String,Object>)D.CreateDelegate(typeof(Func<String,Object>)))("abc"),"abc");
        //    var D = new DynamicMethod("",typeof(Type),new[]{
        //        typeof(Object)
        //    },typeof(Test_ILGenerator拡張メソッド),true) {
        //        InitLocals=false
        //    };
        //    var I = D.GetILGenerator();
        //    I.Ldarg_0();
        //    I.Refanytype();
        //    I.Ret();
        //    Assert.AreEqual(((Func<Object,Type>)D.CreateDelegate(typeof(Func<Object,Type>)))(""),typeof(String));
    }
    [TestMethod]
    public void Refanyval()
    {
        var D = new DynamicMethod("", typeof(object), new[] { typeof(string) }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarga(0);
        I.Mkrefany(typeof(string));//TypedReference
        I.Refanyval(typeof(string));
        I.Ldobj(typeof(string));
        I.Ret();
        Assert.AreEqual(((Func<string, object>)D.CreateDelegate(typeof(Func<string, object>)))("abc"), "abc");
    }
    [TestMethod]
    public void Rem()
    {
        var M = 二項演算子<int, int>(I => I.Rem());
        for (var a = int.MaxValue - 5; a < int.MaxValue - 1; a++)
        {
            for (var b = int.MaxValue - 5; b < int.MaxValue - 1; b++)
            {
                Assert.AreEqual(a % b, M(a, b));
            }
        }
    }
    [TestMethod]
    public void Rem_Un()
    {
        var M = 二項演算子<uint, uint>(I => I.Rem_Un());
        for (var a = uint.MaxValue - 5; a < uint.MaxValue - 1; a++)
        {
            for (var b = uint.MaxValue - 5; b < uint.MaxValue - 1; b++)
            {
                Assert.AreEqual(a % b, M(a, b));
            }
        }
    }
    [TestMethod]
    public void Ret()
    {
        var D = new DynamicMethod("", typeof(void), Type.EmptyTypes, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ret();
        ((Action)D.CreateDelegate(typeof(Action)))();
    }
    [TestMethod]
    public void Shl()
    {
        var M = 二項演算子<int, int>(I => I.Shl());
        for (var a = int.MaxValue - 5; a < int.MaxValue - 1; a++)
        {
            for (var b = 0; b < 32; b++)
            {
                Assert.AreEqual(a << b, M(a, b));
            }
        }
    }
    [TestMethod]
    public void Shr()
    {
        var M = 二項演算子<int, int>(I => I.Shr());
        for (var a = int.MaxValue - 5; a < int.MaxValue - 1; a++)
        {
            for (var b = 0; b < 32; b++)
            {
                Assert.AreEqual(a >> b, M(a, b));
            }
        }
    }
    [TestMethod]
    public void Shr_Un()
    {
        var M = 二項演算子<uint, int, uint>(I => I.Shr_Un());
        for (var a = uint.MaxValue - 5; a < uint.MaxValue - 1; a++)
        {
            for (var b = 0; b < 32; b++)
            {
                Assert.AreEqual(a >> b, M(a, b));
            }
        }
    }
    [TestMethod]
    public void Sizeof()
    {
        Assert.AreEqual(DynamicSizeof<int>(), 4);
    }
    private static void ggLdloc(Action<ILGenerator, LocalBuilder> ILメソッド, int index)
    {
        var D = new DynamicMethod("", typeof(int), new[] { typeof(int) }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        var Locals = new LocalBuilder[index + 1];
        for (var a = 0; a <= index; a++)
        {
            Locals[a] = I.DeclareLocal(typeof(int));
        }
        var L = Locals[index];
        I.Stloc(L);
        ILメソッド(I, L);
        I.Ret();
        const int expected = 2;
        Assert.AreEqual(expected, ((Func<int, int>)D.CreateDelegate(typeof(Func<int, int>)))(expected));
    }
    private static void Starg(Action<ILGenerator, ushort> ILメソッド, ushort index)
    {
        var Types = new Type[index + 1];
        for (var a = 0; a <= index; a++)
            Types[a] = typeof(int);
        var D = new DynamicMethod("D0", typeof(int), Types, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldc_I4_1();
        ILメソッド(I, index);
        I.Ldarg(index);
        I.Ret();
        var parameters = new object[index + 1];
        parameters[index] = 1;
        Assert.AreEqual(D.Invoke(null, parameters), 1);
    }

    [TestMethod]
    public void Starg()
    {
        Starg((I, index) => I.Starg(index), 0);
        Starg((I, index) => I.Starg(index), 255);
        Starg((I, index) => I.Starg(index), 256);
        Starg((I, index) => I.Starg(index), 16382);
    }

    [TestMethod]
    public void Starg_S()
    {
        Starg((I, index) => I.Starg_S((byte)index), 0);
        Starg((I, index) => I.Starg_S((byte)index), 255);
    }
























    private static void Stelem<T>(T expected) => Stelem(I => I.Stelem(typeof(T)), expected);

    private static void Stelem<T>(Action<ILGenerator> ILメソッド, T expected)
    {
        var 配列 = new T[1];
        var D = new DynamicMethod("", typeof(void), new[]{
            typeof(T[]),typeof(T)
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        I.Ldc_I4_0();
        I.Ldarg_1();
        ILメソッド(I);
        I.Ret();
        ((Action<T[], T>)D.CreateDelegate(typeof(Action<T[], T>)))(配列, expected);
        Assert.AreEqual(配列[0], expected);
    }

    [TestMethod]
    public void Stelem()
    {
        Stelem((IntPtr)1);
        Stelem((sbyte)1);
        Stelem((short)1);
        Stelem(1);
        Stelem((long)1);
        Stelem((float)1);
        Stelem((double)1);
        Stelem("abc");
        Stelem((decimal)1);
    }
    [TestMethod] public void Stelem_I() => Stelem(I => I.Stelem_I(), (IntPtr)1);
    [TestMethod] public void Stelem_I1() => Stelem(I => I.Stelem_I1(), (sbyte)1);
    [TestMethod] public void Stelem_I2() => Stelem(I => I.Stelem_I2(), (short)1);
    [TestMethod] public void Stelem_I4() => Stelem(I => I.Stelem_I4(), 1);
    [TestMethod] public void Stelem_I8() => Stelem(I => I.Stelem_I8(), (long)1);
    [TestMethod] public void Stelem_R4() => Stelem(I => I.Stelem_R4(), 1f);
    [TestMethod] public void Stelem_R8() => Stelem(I => I.Stelem_R8(), 1d);
    [TestMethod] public void Stelem_Ref() => Stelem(I => I.Stelem_Ref(), "ABC");
    private void St<T>(Action<ILGenerator> ILメソッド, FieldInfo Field, T expected)
    {
        var D = new DynamicMethod("", typeof(void), new[] { typeof(Test_ILGenerator拡張メソッド), typeof(T) }, typeof(Test_ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        I.Ldarg_1();
        ILメソッド(I);
        I.Ret();
        Field.SetValue(this, default(T));
        ((Action<Test_ILGenerator拡張メソッド, T>)D.CreateDelegate(typeof(Action<Test_ILGenerator拡張メソッド, T>)))(this, expected);
        Assert.AreEqual(Field.GetValue(this), expected);
    }
    [TestMethod]
    public void Stfld()
    {
        var Field = Field取得(nameof(this.Ldfld用Field));
        this.St(I => I.Stfld(Field), Field, 10);
    }
    private void Stind<T>(string Field名, T expected)
    {
        var Field = Field取得(Field名);
        this.St(I => I.Stfld(Field), Field, expected);
    }
    [TestMethod] public void Stind_I() => this.Stind(nameof(this.Ldind_I用Field), (IntPtr)3);
    [TestMethod] public void Stind_I1() => this.Stind(nameof(this.Ldind_I1用Field), (sbyte)3);
    [TestMethod] public void Stind_I2() => this.Stind(nameof(this.Ldind_I2用Field), (short)3);
    [TestMethod] public void Stind_I4() => this.Stind(nameof(this.Ldind_I4用Field), 3);
    [TestMethod] public void Stind_I8() => this.Stind(nameof(this.Ldind_I8用Field), (long)3);
    [TestMethod] public void Stind_R4() => this.Stind(nameof(this.Ldind_R4用Field), (float)3);
    [TestMethod] public void Stind_R8() => this.Stind(nameof(this.Ldind_R8用Field), (double)3);
    [TestMethod] public void Stind_Ref() => this.Stind(nameof(this.Ldind_Ref用Field), "abc");
    private static void Stloc(Action<ILGenerator, LocalBuilder> ILメソッド, int index)
    {
        var D = new DynamicMethod("", typeof(int), new[] { typeof(int) }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        var Locals = new LocalBuilder[index + 1];
        for (var a = 0; a <= index; a++)
        {
            Locals[a] = I.DeclareLocal(typeof(int));
        }
        var L = Locals[index];
        ILメソッド(I, L);
        I.Ldloc(L);
        I.Ret();
        const int expected = 2;
        Assert.AreEqual(expected, ((Func<int, int>)D.CreateDelegate(typeof(Func<int, int>)))(expected));
    }
    [TestMethod] public void Stloc() => Stloc((I, L) => I.Stloc(L), 0);
    [TestMethod] public void Stloc_0() => Stloc((I, _) => I.Stloc_0(), 0);
    [TestMethod] public void Stloc_1() => Stloc((I, _) => I.Stloc_1(), 1);
    [TestMethod] public void Stloc_2() => Stloc((I, _) => I.Stloc_2(), 2);
    [TestMethod] public void Stloc_3() => Stloc((I, _) => I.Stloc_3(), 3);
    [TestMethod] public void Stloc_S() => Stloc((I, L) => I.Stloc_S(L), 4);
    private delegate void void_TrefT<T>(T expected, ref T actual);
    private static void Stobj<T>(T expected)
    {
        var D = new DynamicMethod("", typeof(void), new[] { typeof(T), typeof(T).MakeByRefType() }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_1();
        I.Ldarg_0();
        I.Stobj(typeof(T));
        I.Ret();
        var actual = default(T);
        ((void_TrefT<T>)D.CreateDelegate(typeof(void_TrefT<T>)))(expected, ref actual);
        Assert.AreEqual(expected, actual);
    }
    [TestMethod]
    public void Stobj()
    {
        Stobj(8);
        Stobj(7L);
        Stobj(6f);
        Stobj(5d);
        Stobj("abc");
        Stobj(5m);
    }
    private static void Stsfld<T>(Action<ILGenerator> ILメソッド, FieldInfo Field, T expected)
    {
        var D = new DynamicMethod("", typeof(void), new[] { typeof(T) }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        ILメソッド(I);
        I.Ret();
        Field.SetValue(null, default(T));
        ((Action<T>)D.CreateDelegate(typeof(Action<T>)))(expected);
        Assert.AreEqual(Field.GetValue(null), expected);
    }
    [TestMethod]
    public void Stsfld()
    {
        {
            var Field = Field取得(nameof(StaticInt32Field));
            Stsfld(I => I.Stsfld(Field), Field, 3);
        }
        {
            var Field = Field取得(nameof(StaticStringField));
            Stsfld(I => I.Stsfld(Field), Field, "ABC");
        }
        {
            var Field = Field取得(nameof(StaticDecimalField));
            Stsfld(I => I.Stsfld(Field), Field, 3m);
        }
    }
    [TestMethod]
    public void Sub()
    {
        var M = 二項演算子<int, int>(I => I.Sub());
        for (var a = int.MaxValue - 5; a < int.MaxValue - 1; a++)
        {
            for (var b = int.MaxValue - 5; b < int.MaxValue - 1; b++)
            {
                Assert.AreEqual(a - b, M(a, b));
            }
        }
    }
    [TestMethod]
    public void Sub_Ovf()
    {
        var M = 二項演算子<int, int>(I => I.Sub_Ovf());
        const int a = int.MinValue;
        for (var b = 1; b < 5; b++)
        {
            try
            {
                var expected = checked(a - b);
                Assert.AreEqual(expected, M(a, b));
                Assert.Fail("OverflowExceptionが発生するべき");
            }
            catch (OverflowException)
            {
                try
                {
                    M(a, b);
                    Assert.Fail("OverflowExceptionが発生するべき");
                }
                catch (OverflowException) { }
            }
        }
    }
    [TestMethod]
    public void Sub_Ovf_Un()
    {
        var M = 二項演算子<uint, uint>(I => I.Sub_Ovf_Un());
        const uint a = 0;
        for (var b = uint.MaxValue - 5; b < uint.MaxValue - 1; b++)
        {
            try
            {
                var expected = checked(a - b);
                Assert.AreEqual(expected, M(a, b));
                Assert.Fail("OverflowExceptionが発生するべき");
            }
            catch (OverflowException)
            {
                try
                {
                    M(a, b);
                    Assert.Fail("OverflowExceptionが発生するべき");
                }
                catch (OverflowException) { }
            }
        }
    }
    [TestMethod]
    public void Switch()
    {
        var D = new DynamicMethod("", typeof(int), new[] { typeof(int) }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        var EndSwitch = I.DefineLabel();
        const int Case数 = 10;
        var JumpTables = new Label[Case数];
        for (var a = 0; a < Case数; a++)
            JumpTables[a] = I.DefineLabel();
        I.Switch(JumpTables);
        I.Ldc_I4_M1();//default
        I.Br(EndSwitch);
        for (var a = 0; a < Case数 - 1; a++)
        {
            I.MarkLabel(JumpTables[a]);
            I.Ldc_I4(a);
            I.Ldc_I4(10);
            I.Mul();
            I.Br(EndSwitch);
        }
        I.MarkLabel(JumpTables[Case数 - 1]);
        I.Ldc_I4(Case数 - 1);
        I.Ldc_I4(10);
        I.Mul();
        I.MarkLabel(EndSwitch);
        I.Ret();
        var M = (Func<int, int>)D.CreateDelegate(typeof(Func<int, int>));
        for (var a = 0; a < Case数; a++)
        {
            Assert.AreEqual(M(a), a * 10);
        }
        Assert.AreEqual(M(Case数), -1);
    }
    //private static Int32 フィボナッチ数列(Int32 a) {
    //    if(a==0)
    //        return 1;
    //    else
    //        return フィボナッチ数列(a-1)*a;
    //}
    //[TestMethod]
    //public void Tailcall() {
    //    var D = new DynamicMethod("フィボナッチ数列",typeof(Int32),new[] { typeof(Int32) },typeof(Test_ILGenerator拡張メソッド),true) {
    //        InitLocals=false
    //    };
    //    var I = D.GetILGenerator();
    //    I.Ldarg_0();
    //    var 呼び出し継続 = I.DefineLabel();
    //    I.Brtrue(呼び出し継続);
    //    I.Ldc_I4_1();
    //    I.Ret();
    //    I.MarkLabel(呼び出し継続);
    //    I.Ldarg_0();
    //    I.Ldc_I4_1();
    //    I.Sub();
    //    I.Tailcall();
    //    I.Call(D);
    //    I.Ldarg_0();
    //    I.Mul();
    //    I.Ret();
    //    var M = (Func<Int32,Int32>)D.CreateDelegate(typeof(Func<Int32,Int32>));
    //    for(var a = 0;a<10;a++) {
    //        Assert.AreEqual(M(a),フィボナッチ数列(a));
    //    }
    //}
    [TestMethod]
    public void Tailcall()
    {
        var D = new DynamicMethod("", typeof(int), new[] { typeof(int), typeof(int) }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        var 呼び出し継続 = I.DefineLabel();
        I.Brtrue(呼び出し継続);
        I.Ldarg_1();
        I.Ret();
        I.MarkLabel(呼び出し継続);
        I.Ldarg_0();
        I.Ldc_I4_1();
        I.Sub();
        I.Ldarg_1();
        I.Ldc_I4_1();
        I.Add();
        //Tallcall,Call,Retに並ばないとだめ。
        //CallとRetの間にMulなどの演算が入るとだめ
        I.Tailcall();
        I.Call(D);
        I.Ret();
        var M = (Func<int, int, int>)D.CreateDelegate(typeof(Func<int, int, int>));
        for (var a = 0; a < 10; a++)
        {
            var r = M(a, 0);
        }
    }
    [TestMethod, ExpectedException(typeof(Exception))]
    public void Throw()
    {
        var D = new DynamicMethod("", typeof(void), Type.EmptyTypes, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Newobj(typeof(Exception).GetConstructor(Type.EmptyTypes));
        I.Throw();
        I.Ret();
        ((Action)D.CreateDelegate(typeof(Action)))();
    }

//    private delegate int UnalignedDelegate(ref int a);
//    /// <summary>
//    /// todo Unalignedの使い方がまだわからない。
//    /// </summary>
//    [TestMethod]
//    public void Unaligned()
//    {
//        var D = new DynamicMethod("", typeof(int),new []{typeof(int).MakeByRefType()}, typeof(ILGenerator拡張メソッド), true)
//        {
//            InitLocals = false
//        };
//        var I = D.GetILGenerator();
//        #if false
//        var ジャンプ = I.DefineLabel();
//        I.Br(ジャンプ);
//        I.Unaligned(ジャンプ);
//        I.MarkLabel(ジャンプ);
//        I.Ldarg_0();
//        I.Emit(OpCodes.Unaligned);
//#else
//        I.Ldobj(typeof(int));
//        I.Ldarg_0();
//        I.Emit(OpCodes.Unaligned);
//        #endif
//        I.Ldobj(typeof(int));
//        I.Ret();
//        var a=1;
//        Assert.AreEqual(((UnalignedDelegate)D.CreateDelegate(typeof(UnalignedDelegate)))(ref a), 1);
//    }
    private delegate void void_refT_refT_Int32_Int32<T>(ref T コピー先, ref T コピー元, int コピーバイト数, int オフセット);
    //        private delegate void Unaligned_Delegate<T>(ref T コピー先,)
    private static void Unaligned_Cpblk<T>(T[] コピー元, int コピーバイト数)
    {
        var D = new DynamicMethod("", typeof(void), new[]{
            typeof(T).MakeByRefType(),typeof(T).MakeByRefType(),typeof(int),typeof(int)
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        I.Ldarg_3();
        I.Add();
        //            I.Unaligned(1);
        I.Ldarg_1();
        I.Ldarg_3();
        I.Add();
        I.Ldarg_2();
        I.Unaligned(1);
        I.Cpblk();
        I.Ret();
        var M = (void_refT_refT_Int32_Int32<T>)D.CreateDelegate(typeof(void_refT_refT_Int32_Int32<T>));
        var 要素数 = コピー元.Length;
        var コピー先 = new T[要素数];
        for (var a = 0; a < 8; a++)
        {
            M(ref コピー先[0], ref コピー元[0], コピーバイト数 - a, a);
            Assert.IsTrue(((IStructuralEquatable)コピー先).Equals(コピー元, StructuralComparisons.StructuralEqualityComparer));
        }
    }
    [TestMethod]
    public void Unaligned_Pointer()
    {
        const int 要素数 = 65536;
        {
            var コピー元 = new byte[要素数];
            for (var a = 0; a < 要素数; a++)
                コピー元[a] = (byte)a;
            Unaligned_Cpblk(コピー元, 要素数);
        }
        {
            var コピー元 = new short[要素数];
            for (var a = 0; a < 要素数; a++)
                コピー元[a] = (short)a;
            for (var a = 0; a < sizeof(short); a++)
                Unaligned_Cpblk(コピー元, 要素数 * sizeof(short));
        }
        {
            var コピー元 = new int[要素数];
            for (var a = 0; a < 要素数; a++)
                コピー元[a] = (int)a;
            for (var a = 0; a < sizeof(int); a++)
                Unaligned_Cpblk(コピー元, 要素数 * sizeof(int));
        }
        {
            var コピー元 = new long[要素数];
            for (var a = 0; a < 要素数; a++)
                コピー元[a] = (long)a;
            for (var a = 0; a < sizeof(long); a++)
                Unaligned_Cpblk(コピー元, 要素数 * sizeof(long));
        }
    }
    private static void Unbox<TResult>(Action<ILGenerator> ILメソッド, TResult actual)
    {
        var D = new DynamicMethod("", typeof(TResult), new[]{
            typeof(object)
        }, typeof(ILGenerator拡張メソッド), true)
        {
            InitLocals = false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        ILメソッド(I);
        I.Ret();
        var expected = ((Func<object, TResult>)D.CreateDelegate(typeof(Func<object, TResult>)))(actual);
        Assert.AreEqual(expected, actual);
    }
    [TestMethod]
    public void Unbox()
    {
        Unbox(I =>
        {
            I.Unbox(typeof(int));
            I.Ldobj(typeof(int));
        }, 1);
        Unbox(I =>
        {
            I.Unbox(typeof(double));
            I.Ldobj(typeof(double));
        }, 1d);
        Unbox(I =>
        {
            I.Unbox(typeof(decimal));
            I.Ldobj(typeof(decimal));
        }, 1m);
    }
    [TestMethod]
    public void Unbox_Any()
    {
        Unbox(I => I.Unbox_Any(typeof(int)), 1);
        Unbox(I => I.Unbox_Any(typeof(double)), 1d);
        Unbox(I => I.Unbox_Any(typeof(decimal)), 1m);
        Unbox(I => I.Unbox_Any(typeof(string)), "abc");
    }
}