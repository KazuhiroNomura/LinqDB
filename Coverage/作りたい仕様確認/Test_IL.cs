//using System.Linq;
using System.Linq.Expressions;
using CoverageCS.LinqDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.作りたい仕様確認;

[TestClass]
public class Test_IL : ATest
{
    //todo op_Explicitが呼び出される
    [TestMethod]
    public void Conv_I() => this.Conv<IntPtr,int>();
    [TestMethod]
    public void Conv_I1() => this.Conv<sbyte,long>();
    [TestMethod]
    public void Conv_I2() => this.Conv<short,long>();
    [TestMethod]
    public void Conv_I4() => this.Conv<int,long>();
    [TestMethod]
    public void Conv_I8() => this.Conv<long,ulong>();
    //todo op_Explicitが呼び出される
    [TestMethod]
    public void Conv_Ovf_I() => this.Conv_Ovf<IntPtr,int>();
    //[TestMethod]
    //[ExpectedException(typeof(OverflowException))]
    //public void Conv_Ovf_I_OverflowException() {
    //    var MaxValue= Int64.MaxValue;
    //    var r = this._変数Cache.AssertExecute(() => checked((IntPtr)MaxValue));
    //}
    //[TestMethod]
    //[ExpectedException(typeof(OverflowException))]
    //public void Conv_Ovf_I_Un_OverflowException() {
    //    var MaxValue = UInt64.MaxValue;
    //    var r = this._変数Cache.AssertExecute(() => checked((IntPtr)MaxValue));
    //}
    [TestMethod]
    public void Conv_Ovf_I1() => this.Conv_Ovf<sbyte,int>();
    [TestMethod]
    [ExpectedException(typeof(OverflowException))]
    public void Conv_Ovf_I1_OverflowException() => this.Conv_Ovf_MaxValue<sbyte,short>();
    [TestMethod]
    public void Conv_Ovf_I1_Un() => this.Conv_Ovf<sbyte,uint>();
    [TestMethod]
    [ExpectedException(typeof(OverflowException))]
    public void Conv_Ovf_I1_Un_OverflowException() => this.Conv_Ovf_MaxValue<sbyte,byte>();
    [TestMethod]
    public void Conv_Ovf_I2() => this.Conv_Ovf<short,sbyte>();
    [TestMethod]
    [ExpectedException(typeof(OverflowException))]
    public void Conv_Ovf_I2_OverflowException() => this.Conv_Ovf_MaxValue<short,int>();
    [TestMethod]
    public void Conv_Ovf_I2_Un() => this.Conv_Ovf<short,byte>();
    [TestMethod]
    [ExpectedException(typeof(OverflowException))]
    public void Conv_Ovf_I2_Un_OverflowException() => this.Conv_Ovf_MaxValue<short,ushort>();
    [TestMethod]
    public void Conv_Ovf_I4() => this.Conv_Ovf<int,short>();
    [TestMethod]
    [ExpectedException(typeof(OverflowException))]
    public void Conv_Ovf_I4_OverflowException() => this.Conv_Ovf_MaxValue<int,long>();
    [TestMethod]
    public void Conv_Ovf_I4_Un()
    {
        uint d = 0;
        this.実行結果が一致するか確認(() => checked((int)d));
    }
    [TestMethod]
    [ExpectedException(typeof(OverflowException))]
    public void Conv_Ovf_I4_Un_OverflowException() => this.Conv_Ovf_MaxValue<int,uint>();
    [TestMethod]
    public void Conv_Ovf_I8() => this.Conv_Ovf<long,int>();
    [TestMethod]
    public void Conv_Ovf_I8_Un() => this.Conv_Ovf<long,ulong>();
    [TestMethod]
    [ExpectedException(typeof(OverflowException))]
    public void Conv_Ovf_I8_Un_OverflowException() => this.Conv_Ovf_MaxValue<long,ulong>();
    //todo op_Explicitが呼び出される
    [TestMethod]
    public void Conv_Ovf_U() => this.Conv_Ovf_MinValue<UIntPtr,uint>();
    [TestMethod]
    public void Conv_Ovf_U_Un() => this.Conv_Ovf<UIntPtr,uint>();
    //[TestMethod]
    //[ExpectedException(typeof(OverflowException))]
    //public void Conv_Ovf_U_Un_OverflowException() {
    //    var MaxValue = UInt64.MaxValue;
    //    var r = this._変数Cache.AssertExecute(() => checked((UIntPtr)MaxValue));
    //}
    [TestMethod]
    public void Conv_Ovf_U1() => this.Conv_Ovf<byte,sbyte>();
    [TestMethod]
    [ExpectedException(typeof(OverflowException))]
    public void Conv_Ovf_U1_OverflowException() => this.Conv_Ovf_MaxValue<byte,short>();
    [TestMethod]
    public void Conv_Ovf_U1_Un() => this.Conv_Ovf<byte,uint>();
    [TestMethod]
    [ExpectedException(typeof(OverflowException))]
    public void Conv_Ovf_U1_Un_OverflowException() => this.Conv_Ovf_MaxValue<byte,uint>();
    [TestMethod]
    public void Conv_Ovf_U2() => this.Conv_Ovf<ushort,uint>();
    [TestMethod]
    [ExpectedException(typeof(OverflowException))]
    public void Conv_Ovf_U2_OverflowException() => this.Conv_Ovf_MinValue<ushort,int>();
    [TestMethod]
    public void Conv_Ovf_U2_Un() => this.Conv_Ovf<ushort,uint>();
    [TestMethod]
    [ExpectedException(typeof(OverflowException))]
    public void Conv_Ovf_U2_Un_OverflowException() => this.Conv_Ovf_MaxValue<ushort,uint>();
    [TestMethod]
    public void Conv_Ovf_U4() => this.Conv_Ovf<uint,long>();
    [TestMethod]
    [ExpectedException(typeof(OverflowException))]
    public void Conv_Ovf_U4_OverflowException() => this.Conv_Ovf_MinValue<uint,long>();
    [TestMethod]
    public void Conv_Ovf_U4_Un() => this.Conv_Ovf<uint,ushort>();
    [TestMethod]
    [ExpectedException(typeof(OverflowException))]
    public void Conv_Ovf_U4_Un_OverflowException() => this.Conv_Ovf_MaxValue<uint,ulong>();
    [TestMethod]
    public void Conv_Ovf_U8() => this.Conv_Ovf<ulong,long>();
    [TestMethod]
    [ExpectedException(typeof(OverflowException))]
    public void Conv_Ovf_U8_OverflowException() => this.Conv_Ovf_MinValue<ulong,long>();
    [TestMethod]
    public void Conv_Ovf_U8_Un() => this.Conv_Ovf<ulong,uint>();
    [TestMethod]
    public void Conv_R_Un() => this.Conv<float,uint>();
    [TestMethod]
    public void Conv_R4() => this.Conv<float,int>();
    [TestMethod]
    public void Conv_R8() => this.Conv<double,int>();
    [TestMethod]
    public void Conv_U() => this.Conv<UIntPtr,uint>();
    [TestMethod]
    public void Conv_U1() => this.Conv<byte,ulong>();
    [TestMethod]
    public void Conv_U2() => this.Conv<ushort,ulong>();
    [TestMethod]
    public void Conv_U4() => this.Conv<uint,ulong>();
    [TestMethod]
    public void Conv_U8() => this.Conv<ulong,uint>();
    [TestMethod]
    public void Div()
    {
        var i = 0;
        this.実行結果が一致するか確認(() => i / 2);
    }
    [TestMethod]
    public void Div_Un()
    {
        uint i = 0;
        this.実行結果が一致するか確認(() => i / 2u);
    }
    private struct STRUCT
    {
        public int value;
    }
    [TestMethod]
    public void Initobj() => this.実行結果が一致するか確認(() => new STRUCT().value);
    [TestMethod]
    public void Ldarg() => this.実行結果が一致するか確認(() =>
        ((Func<int,int>)(p0 => p0))(1)
    );
    [TestMethod]
    public void Ldarga() => this.実行結果が一致するか確認(() =>
        ((Func<STRUCT,int>)(p0 => p0.value))(new STRUCT { value=1 })
    );
    [TestMethod]
    public void Ldc_I4() => this.実行結果が一致するか確認(() => 1);
    [TestMethod]
    public void Ldc_I8() => this.実行結果が一致するか確認(() => 1L);
    [TestMethod]
    public void Ldc_R4() => this.実行結果が一致するか確認(() => 1F);
    [TestMethod]
    public void Ldc_R8() => this.実行結果が一致するか確認(() => 1D);
    [TestMethod]
    public void Ldelem()
    {
        var d = new[] { 1 };
        this.実行結果が一致するか確認(() => d[0]);
    }
    [TestMethod]
    public void Ldelema()
    {
        var d = new[] { new STRUCT { value = 1 } };
        this.実行結果が一致するか確認(() => d[0].value);
    }
    [TestMethod]
    public void Ldfld() => this.実行結果が一致するか確認(() => new STRUCT { value=1 }.value);
    [TestMethod]
    public void Ldflda() => this.実行結果が一致するか確認(() =>
        ((Func<STRUCT,string>)(p0 => p0.value.ToString()))(new STRUCT { value=1 })
    );
    [TestMethod]
    public void Ldftn() => this.実行結果が一致するか確認(() =>
        ((Func<int,int>)(p0 => p0))(1)
    );
    [TestMethod]
    public void Ldlen()
    {
        var i = new[] { 1 };
        this.実行結果が一致するか確認(() => i.Length);
    }
    [TestMethod]
    public void Ldnull() => this.実行結果が一致するか確認(() => (object?)null);
    private static readonly int static_field = 1;
    [TestMethod]
    public void Ldsfld() => this.実行結果が一致するか確認(() => static_field);
    [TestMethod]
    public void Ldsflda() => this.実行結果が一致するか確認(() => static_field.ToString());
    [TestMethod]
    public void Ldstr() => this.実行結果が一致するか確認(() => "1");
    [TestMethod]
    public void Mul()
    {
        var i = 1;
        this.実行結果が一致するか確認(() => i * 2);
    }
    [TestMethod]
    public void Mul_Ovf()
    {
        var i = 1;
        this.実行結果が一致するか確認(() => checked(i * 2));
    }
    [TestMethod]
    [ExpectedException(typeof(OverflowException))]
    public void Mul_Ovf_OverflowException()
    {
        var i = int.MaxValue;
        this.実行結果が一致するか確認(() => checked(i * int.MaxValue));
    }
    [TestMethod]
    public void Mul_Ovf_Un()
    {
        var i = 1U;
        this.実行結果が一致するか確認(() => checked(i * 2U));
    }
    [TestMethod]
    [ExpectedException(typeof(OverflowException))]
    public void Mul_Ovf_Un_OverflowException()
    {
        var i = uint.MaxValue;
        this.実行結果が一致するか確認(() => checked(i * uint.MaxValue));
    }
    [TestMethod]
    public void Neg()
    {
        var i = 1;
        this.実行結果が一致するか確認(() => -i);
    }
    [TestMethod]
    public void Newarr() => this.実行結果が一致するか確認(() => new int[3]);
    [TestMethod]
    public void Newobj() => this.実行結果が一致するか確認(() => new string('a',2));
    [TestMethod]
    public void Not()
    {
        var i = true;
        this.実行結果が一致するか確認(() => !i);
    }
    [TestMethod]
    public void Or()
    {
        var a = 4;
        this.実行結果が一致するか確認(() => a | 1);
    }
    [TestMethod]
    public void Rem()
    {
        var a = 2;
        this.実行結果が一致するか確認(() => 5 % a);
    }
    [TestMethod]
    public void Rem_Un()
    {
        var a = 2U;
        this.実行結果が一致するか確認(() => 5 % a);
    }
    [TestMethod]
    public void Ret() => this.実行結果が一致するか確認(() => 1);
    [TestMethod]
    public void Shl()
    {
        var a = 1;
        this.実行結果が一致するか確認(() => a << 1);
    }
    [TestMethod]
    public void Shr()
    {
        var a = 4;
        this.実行結果が一致するか確認(() => a >> 1);
    }
    [TestMethod]
    public void Shr_Un()
    {
        var a = 4U;
        this.実行結果が一致するか確認(() => a >> 1);
    }
    [TestMethod]
    public void Sub()
    {
        var a = 4;
        this.実行結果が一致するか確認(() => a - 1);
    }
    [TestMethod]
    public void Sub_Ovf()
    {
        var a = 4;
        this.実行結果が一致するか確認(() => checked(a - 1));
    }
    [TestMethod]
    public void Sub_Ovf_Un()
    {
        var a = 4U;
        this.実行結果が一致するか確認(() => checked(a - 1U));
    }
    [TestMethod]
    public void Unbox_Any()
    {
        object a = 4;
        this.実行結果が一致するか確認(() => (int)a);
    }
    [TestMethod]
    public void Xor()
    {
        var a = 5;
        this.実行結果が一致するか確認(() => a ^ 15);
    }
    private void Conv<TResult, TInput>()
    {
        var MaxValue = typeof(TInput).GetField("MaxValue");
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<TResult>>(
                Expression.Convert(
                    Expression.Constant(MaxValue.GetValue(null)),
                    typeof(TResult)
                )
            )
        );
    }
    private void Conv_Ovf<TResult, TInput>() => this.実行結果が一致するか確認(
        Expression.Lambda<Func<TResult>>(
            Expression.ConvertChecked(
                Expression.Constant(default(TInput)),
                typeof(TResult)
            )
        )
    );
    private void Conv_Ovf_MinValue<TResult, TInput>()
    {
        var MinValue = typeof(TInput).GetField("MinValue");
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<TResult>>(
                Expression.ConvertChecked(
                    Expression.Constant(MinValue.GetValue(null)),
                    typeof(TResult)
                )
            )
        );
    }
    //private void Conv_Ovf_MinValue<TResult, TInput>(TInput MinValue) {
    //    変数Cache.AssertExecute(
    //        Expression.Lambda<Func<TResult>>(
    //            Expression.ConvertChecked(
    //                Expression.Constant(MinValue),
    //                typeof(TResult)
    //            )
    //        )
    //    );
    //}
    private void Conv_Ovf_MaxValue<TResult, TInput>()
    {
        var MaxValue = typeof(TInput).GetField("MaxValue")!;
        this.実行結果が一致するか確認(
            Expression.Lambda<Func<TResult>>(
                Expression.ConvertChecked(
                    Expression.Constant(MaxValue.GetValue(null)),
                    typeof(TResult)
                )
            )
        );
    }
    //private void Conv_Ovf_MaxValue<TResult, TInput>(TInput MaxValue) {
    //    変数Cache.AssertExecute(
    //        Expression.Lambda<Func<TResult>>(
    //            Expression.ConvertChecked(
    //                Expression.Constant(MaxValue),
    //                typeof(TResult)
    //            )
    //        )
    //    );
    //}
    //private static void Conv_Ovf<TResult,TInput>() {
    //    var MaxValue=typeof(TInput).GetField("MaxValue");
    //    var MinValue=typeof(TInput).GetField("MinValue");
    //    this._変数Cache.AssertExecute(
    //        Expression.Lambda<Func<TResult>>(
    //            Expression.ConvertChecked(
    //                Expression.Constant(MinValue.GetValue(null)),
    //                typeof(TResult)
    //            )
    //        )
    //    );
    //}
}