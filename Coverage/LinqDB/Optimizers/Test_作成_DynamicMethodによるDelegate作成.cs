using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq.Expressions;
using System.Reflection;
using LinqDB;
using LinqDB.Sets;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Binder = Microsoft.CSharp.RuntimeBinder.Binder;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class Test_作成_DynamicMethodによるDelegate作成 : ATest
{
    [TestMethod]
    public void Add() => this.Execute2(() => Int32_1 + Int32_2);
    [TestMethod]
    public void AddAssign() => this.演算Binary(ExpressionType.AddAssign);
    [TestMethod]
    public void AddAssignChecked() => this.演算Binary(ExpressionType.AddAssignChecked);
    [TestMethod]
    public void AddChecked() => this.演算Binary(ExpressionType.AddChecked);
    [TestMethod]
    public void And() => this.演算Binary(ExpressionType.And);
    [TestMethod]
    public void AndAlso() => this.Execute2(() => Boolean1 && Boolean2);
    [TestMethod]
    public void AndAssign() => this.演算Binary(ExpressionType.AndAssign);
    [TestMethod]
    public void ArrayIndex() => this.Execute2(() => Array[0]);
    [TestMethod]
    public void ArrayLength() => this.Execute2(() => Array.Length);
    [TestMethod]
    public void Assign() => this.共通化Assign(ExpressionType.Assign);
    [TestMethod]
    public void Bindings()
    {
        //    switch(Binding.BindingType) {
        //        case MemberBindingType.Assignment: {
        //            if(MemberAssignment_Expression.Type.IsValueType){
        //                if(Binding_Member.MemberType==MemberTypes.Field){
        this.Execute2(() => new class_演算子オーバーロード { Int32フィールド = 3 });
        //                } else{
        this.Execute2(() => new class_演算子オーバーロード { Int32プロパティ = 3 });
        //                }
        //            } else{
        //                if(Binding_Member.MemberType==MemberTypes.Field){
        this.Execute2(() => new class_演算子オーバーロード { Stringフィールド = "3" });
        //                } else{
        this.Execute2(() => new class_演算子オーバーロード { Stringプロパティ = "3" });
        //                }
        //            }
        //        }
        //        case MemberBindingType.MemberBinding: {
        //            if(Binding_Member.MemberType==MemberTypes.Field) {
        //                if(Local2Type.IsValueType) {
        this.Execute2(() => new class_演算子オーバーロード
        {
            Struct演算子オーバーロード2 = {
                StructCollectionフィールド ={0}
            }
        });
        //                } else {
        this.Execute2(() => new class_演算子オーバーロード
        {
            class_演算子オーバーロード2フィールド ={
                StructCollectionフィールド = {0}
            }
        });
        //                }
        //            } else {
        this.Execute2(() => new class_演算子オーバーロード
        {
            class演算子オーバーロード2プロパティ ={
                StructCollectionフィールド ={1}
            }
        });
        //            }
        //        }
        //        default: {
        //            if(Binding_Member.MemberType==MemberTypes.Field) {
        //                if(LocalType.IsValueType) {
        this.Execute2(() => new class_演算子オーバーロード { StructCollectionフィールド = { 3 } });
        //                }else{
        this.Execute2(() => new class_演算子オーバーロード { Listフィールド = { 3 } });
        //                }
        //            } else {
        this.Execute2(() => new class_演算子オーバーロード { Listプロパティ = { 3 } });
        //            }
        //            if(MemberListBinding_Initializers.Count==1) {
        //                if(AddMethod.ReturnType!=typeof(void)) {
        this.Execute2(() => new class_演算子オーバーロード { HashSetプロパティ = { 0 } });
        //                }else{
        this.Execute2(() => new class_演算子オーバーロード { Listプロパティ = { 2 } });
        //                }
        //            } else {
        //                foreach(var Initializer in MemberListBinding_Initializers) {
        //                    if(AddMethod.ReturnType!=typeof(void)) {
        this.Execute2(() => new class_演算子オーバーロード { HashSetプロパティ = { 0, 1 } });
        //                    }else{
        this.Execute2(() => new class_演算子オーバーロード { Listプロパティ = { 2, 3 } });
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }
    [TestMethod]public void Block0(){
        //foreach(var Block_Variable in Block.Variables) {
        var p = Expression.Parameter(typeof(bool));
        this.Execute2(
            Expression.Lambda<Func<bool>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(p, Expression.Constant(true)),
                    p
                )
            )
        );
    }
    [TestMethod]public void Block1(){
        //}
        //for(var a=0;a<Block_Expressions_Count_1;a++) {
        this.Execute標準ラムダループ(
            Expression.Lambda<Action>(
                Expression.Block(
                    Expression.Constant(1),
                    Expression.Default(typeof(void))
                )
            )
        );
        //}
    }
    private static int Callされるメソッド() => 0;
    [TestMethod]
    public void Call() => this.Execute2(() => Callされるメソッド());
    [TestMethod]
    public void Coalesce()
    {
        //if(Binary_Left_Type.IsValueType){
        this.Execute2(() => _NullableInt32 ?? 3);
        //}else{
        this.Execute2(() => _String ?? "3");
        //}
    }
    [TestMethod]public void Conditional0(){
        //if(Conditional.Type==typeof(void)) {
        //    if(Conditional.IfTrue.NodeType==ExpressionType.Goto) {
        //        if(Goto.Value==null) {
        {
            var ジャンプ1 = Expression.Label("ジャンプ1");
            var ジャンプ2 = Expression.Label("ジャンプ2");
            var ジャンプ3 = Expression.Label(typeof(int),"ジャンプ3");
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        Expression.Condition(
                            Expression.Constant(false),
                            Expression.Goto(ジャンプ1),
                            Expression.Goto(ジャンプ2)
                        ),
                        Expression.Label(ジャンプ1),
                        Expression.Goto(ジャンプ3, Expression.Constant(1)),
                        Expression.Label(ジャンプ2),
                        Expression.Goto(ジャンプ3, Expression.Constant(2)),
                        Expression.Label(
                            ジャンプ3,
                            Expression.Constant(3)
                        )
                    )
                )
            );
        }
    }
    [TestMethod]public void Conditional1(){
        //        }else{
        {
            var ジャンプ = Expression.Label(typeof(int));
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        Expression.Condition(
                            Expression.Constant(true),
                            Expression.Goto(
                                ジャンプ,
                                Expression.Constant(1)
                            ),
                            Expression.Goto(
                                ジャンプ,
                                Expression.Constant(2)
                            )
                        ),
                        Expression.Label(
                            ジャンプ,
                            Expression.Constant(3)
                        )
                    )
                )
            );
        }
    }
    [TestMethod]public void Conditional2(){
        //    }
        //    if(Conditional.IfFalse.NodeType!=ExpressionType.Default) {
        //    } else {
        {
            var ジャンプ1 = Expression.Label(typeof(int));
            var ジャンプ3 = Expression.Label(typeof(int));
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        Expression.Condition(
                            Expression.Constant(false),
                            Expression.Goto(
                                ジャンプ1,
                                Expression.Constant(1),
                                typeof(int)
                            ),
                            Expression.Default(typeof(int))
                        ),
                        Expression.Label(
                            ジャンプ1,
                            Expression.Constant(2)
                        ),
                        Expression.Goto(
                            ジャンプ3,
                            Expression.Constant(1),
                            typeof(int)
                        ),
                        Expression.Label(
                            ジャンプ3,
                            Expression.Constant(3)
                        )
                    )
                )
            );
        }
    }
    [TestMethod]public void Conditional3(){
        //    }
        //} else {
        this.Execute2(() => _Int32 == 3 ? 4 : 5);
        //}
    }
    [TestMethod]
    public void Constant()
    {
        //if     (Constant.Value==null             )this.I.Ldnull();
        this.Execute2(() => (string?)null);
        //else if(Constant      ==this.RootConstant)this.I.Ldarg_0();
        {
            var C = 1;
            this.Execute2(() => C);
        }
        //else {
        //    if(Constant_Value_Type==typeof(String))this.I.Ldstr((String)Constant_Value);
        this.Execute2(() => "A");
        //    else{
        //        var Constant_Value_Type2=Constant_Value_Type.IsEnum
        //            ?Enum.GetUnderlyingType(Constant_Value_Type)
        this.Execute2(() => EEnum.A);
        //            :Constant_Value_Type;
        //        if(Constant_Value_Type2==typeof(SByte)) this.I.Ldc_I4((SByte)Constant_Value);
        {
            const sbyte C = 1;
            this.Execute2(() => C);
        }
        //        else if(Constant_Value_Type2==typeof(Int16)) this.I.Ldc_I4((Int16)Constant_Value);
        {
            const short C = 1;
            this.Execute2(() => C);
        }
        //        else if(Constant_Value_Type2==typeof(Int32)) this.I.Ldc_I4((Int32)Constant_Value);
        {
            const int C = 1;
            this.Execute2(() => C);
        }
        //        else if(Constant_Value_Type2==typeof(Int64)) this.I.Ldc_I8((Int64)Constant_Value);
        {
            const long C = 1;
            this.Execute2(() => C);
        }
        //        else if(Constant_Value_Type2==typeof(IntPtr))
        this.Execute2(
            Expression.Lambda<Func<IntPtr>>(
                Expression.Constant(IntPtr.Zero)
            )
        );
        //            if(IntPtr.Size==4) this.I.Ldc_I4((Int32)(IntPtr)Constant_Value);
        //            else this.I.Ldc_I8((Int64)(IntPtr)Constant_Value);
        //        else if(Constant_Value_Type2==typeof(Byte)) this.I.Ldc_I4((Byte)Constant_Value);
        {
            const byte C = 1;
            this.Execute2(() => C);
        }
        //        else if(Constant_Value_Type2==typeof(UInt16)) this.I.Ldc_I4((UInt16)Constant_Value);
        {
            const ushort C = 1;
            this.Execute2(() => C);
        }
        //        else if(Constant_Value_Type2==typeof(UInt32)) this.I.Ldc_I4((Int32)(UInt32)Constant_Value);
        {
            const uint C = 1;
            this.Execute2(() => C);
        }
        //        else if(Constant_Value_Type2==typeof(UInt64)) this.I.Ldc_I8((Int64)(UInt64)Constant_Value);
        {
            const ulong C = 1;
            this.Execute2(() => C);
        }
        //        else if(Constant_Value_Type2==typeof(UIntPtr))
        this.Execute2(
            Expression.Lambda<Func<UIntPtr>>(
                Expression.Constant(UIntPtr.Zero)
            )
        );
        //            if(UIntPtr.Size==4) this.I.Ldc_I4((Int32)(UIntPtr)Constant_Value);
        //            else this.I.Ldc_I8((Int64)(UIntPtr)Constant_Value);
        //        else if(Constant_Value_Type2==typeof(Boolean)) this.I.Ldc_I4((Boolean)Constant_Value ? 1 : 0);
        this.Execute2(() => true);
        //        else if(Constant_Value_Type2==typeof(Char)) this.I.Ldc_I4((Char)Constant_Value);
        this.Execute2(() => 'A');
        //        else if(Constant_Value_Type2==typeof(Single)) this.I.Ldc_R4((Single)Constant_Value);
        this.Execute2(() => 1.0f);
        //        else {
        this.Execute2(() => 1.0d);
        //        }
        //        if(Constant_Value_Type!=Constant.Type){
        this.Execute2(() => (object)EEnum.A);
        //        }
        //    }
        //}
    }
    private void Convert<T変更前, T変更後>(T変更前 変更前)
    {
        var p = Expression.Parameter(typeof(T変更前));
        var e = Expression.Lambda<Func<T変更前, T変更後>>(
            Expression.Convert(
                p,
                typeof(T変更後)
            ),
            p
        );
        var actual = this.CreateDelegate(e)(変更前);
        var expected = e.Compile()(変更前);
        Assert.AreEqual(expected, actual);
    }
    private struct SByteとimplicit相互変換
    {
        public static implicit operator sbyte(SByteとimplicit相互変換 v) => v.v;
        public static implicit operator SByteとimplicit相互変換(sbyte v) => new SByteとimplicit相互変換(v);
        private readonly sbyte v;
        public SByteとimplicit相互変換(sbyte v) => this.v=v;
    }
    private struct SByteとexplicit相互変換
    {
        public static explicit operator sbyte(SByteとexplicit相互変換 v) => v.v;
        public static explicit operator SByteとexplicit相互変換(sbyte v) => new SByteとexplicit相互変換(v);
        private readonly sbyte v;
        public SByteとexplicit相互変換(sbyte v) => this.v=v;
    }
    [TestMethod]
    public void Convert()
    {
        //            this.Convert<SByteにキャストできる, SByte>(new SByteにキャストできる());
        //if(Unary_Operand_Type.IsPrimitive) {
        //    if(Unary_Type == typeof(SByte)) {
        this.Convert<int, sbyte>(1);
        //    } else if(Unary_Type == typeof(Int16)) {
        this.Convert<int, short>(1);
        //    } else if(Unary_Type == typeof(Int32)) {
        this.Convert<long, int>(1);
        //    } else if(Unary_Type == typeof(Int64)) {
        this.Convert<int, long>(1);
        //    } else if(Unary_Type == typeof(IntPtr)) {
        this.Convert<int, IntPtr>(1);
        //    } else if(Unary_Type == typeof(Byte)) {
        this.Convert<int, byte>(1);
        //    } else if(Unary_Type == typeof(UInt16)) {
        this.Convert<int, ushort>(1);
        //    } else if(Unary_Type == typeof(Char)) {
        this.Convert<int, char>(1);
        //    } else if(Unary_Type == typeof(UInt32)) {
        this.Convert<int, uint>(1);
        //    } else if(Unary_Type == typeof(UInt64)) {
        this.Convert<int, ulong>(1);
        //    } else if(Unary_Type == typeof(UIntPtr)) {
        this.Convert<uint, UIntPtr>(1);
        //    } else if(Unary_Type == typeof(Single)) {
        //        if(IsUnsigned(Unary_Operand_Type))
        this.Convert<uint, float>(1);
        //        else
        this.Convert<int, float>(1);
        //    } else if(Unary_Type == typeof(Double)) {
        this.Convert<int, double>(1);
        //    }
        //}
    }

    [TestMethod]
    public void Convert_ConvertChecked()
    {
        //if(IsNullable(Unary.Type)) {
        //    if(IsNullable(Unary.Operand.Type)) {
        //        if(this.Convert(Convert前Type, Convert後Type)) {
        this.Convert<SByteとexplicit相互変換?, sbyte?>(new SByteとexplicit相互変換(1));
        this.Convert<SByteとimplicit相互変換?, sbyte?>(new SByteとimplicit相互変換(1));
        //        }
        this.Convert<sbyte?, double?>(1);
        //    } else {
        //        if(this.Convert(Unary.Operand.Type, Unary.Type.GetGenericArguments()[0])&& Unary.Method != null) {
        this.Convert<double, decimal?>(1);
        //        }
        this.Convert<sbyte, double?>(1);
        this.Convert<SByteとexplicit相互変換, SByteとexplicit相互変換?>(new SByteとexplicit相互変換(1));
        //    }
        //}
        //if(IsNullable(Unary.Operand.Type)) {
        //    if(this.Convert(GetValueOrDefault.ReturnType, Unary.Type)&& Unary.Method!=null) {
        this.Convert<double?, decimal>(1);
        //    }
        this.Convert<sbyte?, double>(1);
        //}
        //if(Unary.Method != null) {
        this.Convert<double, decimal>(1);
        //}
        //if(!Unary.Operand.Type.IsValueType) {
        //    if(Unary.Type.IsValueType)
        this.Convert<IEquatable<int>, int>(1);
        this.Convert<object, int>(1);
        //    else
        this.Convert<IEquatable<int>, object>(1);
        this.Convert<object, IEquatable<int>>(1);
        //}
        //if(Unary.Type.IsInterface)
        this.Convert<int, IEquatable<int>>(1);
        this.Convert<object, IEquatable<int>>(1);
    }
    private class Nullable0
    {

    }
    private class Nullable1<T> : Nullable0
    {

    }
    private class Nullable2<T> : Nullable1<T>
    {

    }

    [TestMethod]
    public void 共通ConvertConvertChecked()
    {
        //if(Unary.Method != null) {
        //    if(IsNullable(Unary.Type)) {
        //        if(IsNullable(Unary.Operand.Type)) {
        this.Execute2(() => (decimal?)(double?)1);
        //        } else {
        this.Execute2(() => (decimal)(double?)1);
        //        }
        //    } else {
        //        if(IsNullable(Unary.Operand.Type)) {
        this.Execute2(() => (decimal)(double?)1);
        //        } else {
        this.Execute2(() => (decimal)(double)1);
        //        }
        //    }
        //}
        //if(Unary.Operand.Type.IsValueType) {
        //    if(!Unary.Type.IsValueType) {
        const int ボクシングされる = 1;
        this.Execute2(() => (object)ボクシングされる);
        this.Execute2(() => (IEquatable<int>)ボクシングされる);
        //    }
        const double Convertされる = 1;
        this.Execute2(() => (long)Convertされる);
        //} else {
        //    if(Unary.Type.IsValueType)
        const int アンボクシングされる = 1;
        this.Execute2(() => (int)(object)アンボクシングされる);
        //    else
        this.Execute2(() => (string)(object)"1");
        //}
        //if(IsNullable(Unary.Type)) {
        const int Nullableされる = 1;
        this.Execute2(() => (int?)Nullableされる);
        //} else if(IsNullable(Unary.Operand.Type)) {
        const int Nullableから戻される = 1;
        this.Execute2(() => (int)(int?)Nullableから戻される);
        //}
        const long Int64にキャストされるInt32 = 1;
        this.Execute2(() => Int64にキャストされるInt32);
        //if(Unary.Method!=null) {
        this.Execute2(
            Expression.Lambda<Func<struct_演算子オーバーロード>>(
                Expression.ConvertChecked(
                    Expression.Constant(_Int32),
                    typeof(struct_演算子オーバーロード)
                )
            )
        );
        //}
        //if(Unary.Operand.Type==typeof(Object)) {
        //    if(Unary.Type.IsValueType) this.I.Unbox_Any(Unary.Type);
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.ConvertChecked(
                    Expression.Convert(
                        Expression.Constant(Int32),
                        typeof(object)
                    ),
                    typeof(int)
                )
            )
        );
        //    else this.I.Castclass(Unary.Type);
        this.Execute2(
            Expression.Lambda<Func<string>>(
                Expression.ConvertChecked(
                    Expression.Convert(
                        Expression.Constant("ABC"),
                        typeof(object)
                    ),
                    typeof(string)
                )
            )
        );
        //}
        //if(Unary.Type==typeof(Object)) {
        //    if(Unary.Operand.Type.IsValueType){
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.ConvertChecked(
                    Expression.Convert(
                        Expression.Constant(Int32),
                        typeof(object)
                    ),
                    typeof(int)
                )
            )
        );
        //    }
        this.Execute2(
            Expression.Lambda<Func<string>>(
                Expression.ConvertChecked(
                    Expression.Convert(
                        Expression.Constant(String),
                        typeof(object)
                    ),
                    typeof(string)
                )
            )
        );
        //}
        //if(Unary.Type.IsGenericType) {
        //    if(Unary.Type.GetGenericTypeDefinition()==typeof(Nullable<>)) {
        this.Execute2(
            Expression.Lambda<Func<int?>>(
                Expression.ConvertChecked(
                    Expression.Constant(1),
                    typeof(int?)
                )
            )
        );
        //    }
        this.Execute2(
            Expression.Lambda<Func<Nullable1<int>>>(
                Expression.ConvertChecked(
                    Expression.Constant(new Nullable2<int>()),
                    typeof(Nullable1<int>)
                )
            )
        );
        //} else {
        //    if(Unary.Operand.Type.IsGenericType&&Unary.Operand.Type.GetGenericTypeDefinition()==typeof(Nullable<>)){
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.ConvertChecked(
                    Expression.Constant(1, typeof(int?)),
                    typeof(int)
                )
            )
        );
        //    }
        this.Execute2(
            Expression.Lambda<Func<Nullable0>>(
                Expression.ConvertChecked(
                    Expression.Constant(new Nullable1<int>()),
                    typeof(Nullable0)
                )
            )
        );
        //}
    }
    private static IntPtr op_UInt64からIntPtr(ulong a)
    {
        var Int32 = (int)a;
        return (IntPtr)Int32;
    }
    [TestMethod]
    public void ConvertChecked()
    {
        //if(Unary_Operand_Type.IsPrimitive) {
        //    if(Unary_Type==typeof(SByte)) {
        //        if(IsUnsigned(Unary.Operand.Type)) this._I.Conv_Ovf_I1_Un();
        this.Execute2(() => checked((sbyte)UInt32));
        this.Execute2(() => checked((sbyte)Int32));
        //    } else if(Unary_Type==typeof(Int16)) {
        //        if(IsUnsigned(Unary.Operand.Type)) this._I.Conv_Ovf_I2_Un();
        this.Execute2(() => checked((short)UInt32));
        this.Execute2(() => checked((short)Int32));
        //    } else if(Unary_Type==typeof(Int32)) {
        //        if(IsUnsigned(Unary.Operand.Type)) this._I.Conv_Ovf_I4_Un();
        this.Execute2(() => checked((int)UInt32));
        this.Execute2(() => checked((int)Int64));
        //    } else if(Unary_Type==typeof(Int64)) {
        //        if(IsUnsigned(Unary.Operand.Type)) this.I.Conv_Ovf_I8_Un();
        this.Execute2(() => checked((long)UInt64));
        this.Execute2(() => (long)Int32);
        //    } else if(Unary_Type==typeof(IntPtr)) {
        //        if(IsUnsigned(Unary.Operand.Type)) this._I.Conv_Ovf_I_Un();
        this.Execute2(
            Expression.Lambda<Func<IntPtr>>(
                Expression.ConvertChecked(
                    Expression.Constant(UInt64),
                    typeof(IntPtr),
                    typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(op_UInt64からIntPtr), BindingFlags.Static | BindingFlags.NonPublic)
                )
            )
        );
        //        else this._I.Conv_Ovf_I();
        this.Execute2(
            Expression.Lambda<Func<IntPtr>>(
                Expression.ConvertChecked(
                    Expression.Constant(Int64),
                    typeof(IntPtr)
                )
            )
        );
        //    } else if(Unary_Type==typeof(Byte)) {
        //        if(IsUnsigned(Unary.Operand.Type)) this._I.Conv_Ovf_U1_Un();
        this.Execute2(() => checked((byte)UInt32));
        //        else this._I.Conv_Ovf_U1();
        this.Execute2(() => checked((byte)Int32));
        //    } else if(Unary_Type==typeof(UInt16)||Unary_Type==typeof(Char)) {
        //        if(IsUnsigned(Unary.Operand.Type)) this._I.Conv_Ovf_U2_Un();
        this.Execute2(() => checked((ushort)UInt32));
        this.Execute2(() => checked((char)UInt32));
        //        else this._I.Conv_Ovf_U2();
        this.Execute2(() => checked((ushort)Int32));
        this.Execute2(() => checked((char)Int32));
        //    } else if(Unary_Type==typeof(UInt32)) {
        //        if(IsUnsigned(Unary.Operand.Type)) this._I.Conv_Ovf_U4_Un();
        this.Execute2(() => checked((uint)UInt64));
        this.Execute2(() => checked((uint)Int64));
        //    } else if(Unary_Type==typeof(UInt64)) {
        //        this._I.Conv_Ovf_U8();
        this.Execute2(() => (ulong)UInt32);
        this.Execute2(() => checked((ulong)Int32));
        //    } else if(Unary_Type==typeof(UIntPtr)) {
        this.Execute2(
            Expression.Lambda<Func<UIntPtr>>(
                Expression.ConvertChecked(
                    Expression.Constant(UInt32),
                    typeof(UIntPtr)
                )
            )
        );
        //    } else if(Unary_Type==typeof(Single)) {
        //        if(IsUnsigned(Unary.Operand.Type)) this._I.Conv_R_Un();
        this.Execute2(
            Expression.Lambda<Func<float>>(
                Expression.ConvertChecked(
                    Expression.Constant(UInt32),
                    typeof(float)
                )
            )
        );
        //        else this._I.Conv_R4();
        this.Execute2(
            Expression.Lambda<Func<float>>(
                Expression.ConvertChecked(
                    Expression.Constant(Int32),
                    typeof(float)
                )
            )
        );
        //    } else if(Unary_Type==typeof(Double)) {
        this.Execute2(
            Expression.Lambda<Func<double>>(
                Expression.ConvertChecked(
                    Expression.Constant(Int32),
                    typeof(double)
                )
            )
        );
        //    }
        this.Execute2(
            Expression.Lambda<Func<struct_演算子オーバーロード>>(
                Expression.ConvertChecked(
                    Expression.Constant(_Int32),
                    typeof(struct_演算子オーバーロード)
                )
            )
        );
        //}
        this.Execute2(
            Expression.Lambda<Func<decimal>>(
                Expression.ConvertChecked(
                    Expression.Constant(_Static_class_演算子オーバーロード1),
                    typeof(decimal),
                    typeof(class_演算子オーバーロード).GetMethod(nameof(class_演算子オーバーロード.class_演算子オーバーロードからDecimalにキャスト))
                )
            )
        );
    }
    [TestMethod]
    public void Decrement() => this.Execute2(
        Expression.Lambda<Func<int>>(
            Expression.Decrement(
                Expression.Constant(1)
            )
        )
    );
    [TestMethod]
    public void Default()
    {
        //if(Default_Type==typeof(void)) {
        this.Execute標準ラムダループ(
            Expression.Lambda<Action>(
                Expression.Default(typeof(void))
            )
        );
        //} else if(Default_Type.IsValueType) {
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Default(typeof(int))
            )
        );
        //} else {
        this.Execute2(
            Expression.Lambda<Func<string>>(
                Expression.Default(typeof(string))
            )
        );
        //}
    }
    [TestMethod]
    public void Divide() => this.Execute2(() => Int32_1 / Int32_2);
    private void 演算Binary(ExpressionType NodeType)
    {
        var p = Expression.Parameter(typeof(int));
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Constant(2)
                    ),
                    Expression.MakeBinary(
                        NodeType,
                        p,
                        Expression.Constant(2)
                    )
                )
            )
        );
    }
    [TestMethod]
    public void DivideAssign() => this.演算Binary(ExpressionType.DivideAssign);
    private static MethodInfo M<T>(Expression<Func<T>> e) => ((MethodCallExpression)e.Body).Method;
    [TestMethod]
    public void Lambda内のDynamicGetMemberを外部に引き上げる()
    {
        var data = new[] {
            new Point(1,1),
            new Point(2,2)
        };
        var Select = M(() => data.Select(p1 => p1.X));
        var p = Expression.Parameter(typeof(Point), "p");
        this.Execute2(
            Expression.Lambda<Func<object>>(
                Expression.Call(
                    Select,
                    Expression.Constant(data),
                    Expression.Lambda<Func<Point, int>>(
                        Expression.Dynamic(
                            Binder.Convert(
                                CSharpBinderFlags.ConvertExplicit,
                                typeof(int),
                                typeof(Test_作成_DynamicMethodによるDelegate作成)
                            ),
                            typeof(int),
                            Expression.Dynamic(
                                Binder.GetMember(
                                    CSharpBinderFlags.None,
                                    "X",
                                    typeof(Test_作成_DynamicMethodによるDelegate作成),
                                    new[] {
                                        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                                    }
                                ),
                                typeof(object),
                                p
                            )
                        ),
                        p
                    )
                )
            )
        );
    }
    [TestMethod]
    public void Lambda内のDynamicBinaryを外部に引き上げない()
    {
        IEnumerable<int> data = new[] { 1, 2, 3 };
        var Select = M(() => data.Select(p1 => p1));
        var p = Expression.Parameter(typeof(int), "p");
        this.Execute2(
            Expression.Lambda<Func<object>>(
                Expression.Call(
                    Select,
                    Expression.Constant(data),
                    Expression.Lambda<Func<int, int>>(
                        Expression.Dynamic(
                            Binder.Convert(
                                CSharpBinderFlags.ConvertExplicit,
                                typeof(int),
                                typeof(Test_作成_DynamicMethodによるDelegate作成)
                            ),
                            typeof(int),
                            Expression.Dynamic(
                                Binder.BinaryOperation(
                                    CSharpBinderFlags.None,
                                    ExpressionType.Add,
                                    typeof(Test_作成_DynamicMethodによるDelegate作成),
                                    new[] {
                                        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                                        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                                    }
                                ),
                                typeof(object),
                                Expression.Convert(
                                    p,
                                    typeof(object)
                                ),
                                Expression.Convert(
                                    p,
                                    typeof(object)
                                )
                            )
                        ),
                        p
                    )
                )
            )
        );
    }
    [TestMethod]
    public void Equal() => this.大小論理Binary(ExpressionType.Equal);
    [TestMethod]
    public void ExclusiveOr() => this.Execute2(() => Int32_1 ^ Int32_2);
    [TestMethod]
    public void ExclusiveOrAssign() => this.演算Binary(ExpressionType.ExclusiveOrAssign);
    [TestMethod]
    public void Goto()
    {
        var GotoLabel = Expression.Label();
        this.Execute標準ラムダループ(
            Expression.Lambda<Action>(
                Expression.Loop(
                    Expression.Goto(GotoLabel),
                    GotoLabel
                )
            )
        );
    }
    [TestMethod]
    public void GreaterThan() => this.大小論理Binary(ExpressionType.GreaterThan);
    [TestMethod]
    public void GreaterThanOrEqual() => this.大小論理Binary(ExpressionType.GreaterThanOrEqual);
    [TestMethod]
    public void GreaterThanOrEqualLessThanOrEqual() =>
        //if(this.演算(特定の形式はなし,Signed,Unsigned)) return;
        this.Execute2(() => Decimal_1>=Decimal_2);
    [TestMethod]
    public void 共通IncrementDecrement()
    {
        //if(this.共通UnaryExpression(Unary)) return;
        {
            var p = Expression.Parameter(typeof(decimal));
            this.Execute2(
                Expression.Lambda<Func<decimal>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(
                            p,
                            Expression.Default(typeof(decimal))
                        ),
                        Expression.Increment(
                            p
                        )
                    )
                )
            );
        }
        //if(     Unary_Type==typeof(Int32 ))I.Ldc_I4_1();
        {
            var p = Expression.Parameter(typeof(int));
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(
                            p,
                            Expression.Constant(1)
                        ),
                        Expression.Increment(
                            p
                        )
                    )
                )
            );
        }
        //else if(Unary_Type==typeof(Int64 ))I.Ldc_I8(1L);
        {
            var p = Expression.Parameter(typeof(long));
            this.Execute2(
                Expression.Lambda<Func<long>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(
                            p,
                            Expression.Constant(1L)
                        ),
                        Expression.Increment(
                            p
                        )
                    )
                )
            );
        }
        //else if(Unary_Type==typeof(Char  ))I.Ldc_I4((Int16)1);
        {
            var p = Expression.Parameter(typeof(short));
            this.Execute2(
                Expression.Lambda<Func<short>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(
                            p,
                            Expression.Constant((short)1)
                        ),
                        Expression.Increment(
                            p
                        )
                    )
                )
            );
        }
        //else if(Unary_Type==typeof(Single))I.Ldc_R4(1F);
        {
            var p = Expression.Parameter(typeof(float));
            this.Execute2(
                Expression.Lambda<Func<float>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(
                            p,
                            Expression.Constant(1f)
                        ),
                        Expression.Increment(
                            p
                        )
                    )
                )
            );
        }
        //else if(Unary_Type==typeof(_Double))I.Ldc_R8(1D);
        {
            var p = Expression.Parameter(typeof(double));
            this.Execute2(
                Expression.Lambda<Func<double>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(
                            p,
                            Expression.Constant(1d)
                        ),
                        Expression.Increment(
                            p
                        )
                    )
                )
            );
        }
        //else throw new NotImplementedException(Unary_Type.Name+"は++,--出来ない");
        {
            var Field = typeof(ATest).GetField(nameof(_Static_class_演算子オーバーロード1), BindingFlags.Static | BindingFlags.NonPublic);
            var actual = new class_演算子オーバーロード(1);
            var expected0 = this.Execute2(
                Expression.Lambda<Func<class_演算子オーバーロード>>(
                    Expression.Block(
                        Expression.Assign(
                            Expression.Field(
                                null,
                                Field
                            ),
                            Expression.Constant(actual)
                        ),
                        Expression.PostIncrementAssign(
                            Expression.Field(
                                null,
                                Field
                            )
                        )
                    )
                )
            );
            var expected1 = new class_演算子オーバーロード(1);
            expected1++;
            Assert.AreEqual(expected0, actual);
            Assert.AreEqual(expected1, _Static_class_演算子オーバーロード1);
        }
    }
    [TestMethod]
    public void Increment() => this.Execute2(
        Expression.Lambda<Func<int>>(
            Expression.Increment(
                Expression.Constant(1)
            )
        )
    );
    [TestMethod]
    public void Index()
    {
        var Array1 = new[] { 1, 2, 3 };
        //var Index_Object = Index.Object;
        //this.PointerTraverse(Index_Object);
        //foreach(var Argument in Index.Arguments) {
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.ArrayAccess(
                    Expression.Constant(Array1),
                    Expression.Constant(1)
                )
            )
        );
        //}
        //if(Index.Arguments.Count==1) {
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.ArrayAccess(
                    Expression.Constant(Array1),
                    Expression.Constant(2)
                )
            )
        );
        //} else {
        var Array2 = new[,] { { 1, 11 }, { 2, 22 }, { 3, 33 } };
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.ArrayAccess(
                    Expression.Constant(Array2),
                    Expression.Constant(2),
                    Expression.Constant(0)
                )
            )
        );
        //}
    }
    [TestMethod]
    public void Invoke()
    {
        //for(var a = 0;a<Parameters_Length;a++) {
        //    if(Parameters[a].ParameterType.IsByRef) {
        {
            var a = Expression.Parameter(typeof(int), "a");
            var b = Expression.Parameter(typeof(int).MakeByRefType(), "b");
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { a },
                        Expression.Assign(a, Expression.Constant(1)),
                        Expression.Invoke(
                            Expression.Lambda<outパラメータを引数に取りIntを返す1>(
                                Expression.PreIncrementAssign(b), b),
                            a
                        )
                    )
                )
            );
        }
        //        this.PointerTraverse(Invocation_Arguments[a]);
        //    } else {
        this.Execute2(() => _Delegate(0));
        //    }
        //}
    }
    [TestMethod]
    public void IsFalse()
    {
        //if(this.共通UnaryExpression(Unary)) return;
        this.Execute2(
            Expression.Lambda<Func<bool>>(
                Expression.IsFalse(
                    Expression.Constant(new class_演算子オーバーロード(1, true))
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<bool>>(
                Expression.IsFalse(
                    Expression.Constant(false)
                )
            )
        );
    }
    [TestMethod]
    public void IsTrue() => this.Execute2(
        Expression.Lambda<Func<bool>>(
            Expression.IsTrue(
                Expression.Constant(false)
            )
        )
    );

    [TestMethod]
    public void Subtract() => this.演算Binary(ExpressionType.Subtract);
    [TestMethod]
    public void SubtractAssign() => this.演算Binary(ExpressionType.SubtractAssign);
    [TestMethod]
    public void SubtractChecked() => this.演算Binary(ExpressionType.SubtractChecked);
    [TestMethod]
    public void SubtractAssignChecked() => this.演算Binary(ExpressionType.SubtractAssignChecked);
    [TestMethod]
    public void MemberAccess()
    {
        //if(Member_Member.MemberType==MemberTypes.Field) {
        //    if(Member_Field.IsStatic) {
        this.Execute2(() => _Static_class_演算子オーバーロード1);
        //    }else{
        this.Execute2(() => this._InstanceInt32);
        //    }
        //}else{
        this.Execute2(() => Static_struct_演算子オーバーロード1);
        this.Execute2(() => this.Instance_struct_演算子オーバーロード);
        //}
    }
    [TestMethod]
    public void MemberInit()
    {
        //if(MemberInit_Type.IsValueType) {
        this.Execute2(() => new struct_演算子オーバーロード { Int32フィールド = 1 });
        //}else{
        this.Execute2(() => new class_演算子オーバーロード { Listフィールド = new List<int>() });
        //}
    }
    [TestMethod]
    public void Modulo() => this.演算Binary(ExpressionType.Modulo);
    [TestMethod]
    public void ModuloAssign() => this.演算Binary(ExpressionType.ModuloAssign);
    [TestMethod]
    public void Multiply() => this.演算Binary(ExpressionType.Multiply);
    [TestMethod]
    public void MultiplyAssign() => this.演算Binary(ExpressionType.MultiplyAssign);
    [TestMethod]
    public void MultiplyChecked() => this.演算Binary(ExpressionType.MultiplyChecked);
    [TestMethod]
    public void MultiplyAssignChecked() => this.演算Binary(ExpressionType.MultiplyAssignChecked);
    [TestMethod]
    public void Negate() => this.Execute2(() => -Int32_1);
    [TestMethod]
    public void NegateChecked() => this.Execute2(() => checked(-Int32_1));
    [TestMethod]
    public void New()
    {
        //if(New.Constructor!=null) {
        this.Execute2(() => new class_演算子オーバーロード());
        //}else{
        this.Execute2(() => new struct_演算子オーバーロード());
        //}
    }
    [TestMethod]
    public void NewArrayBound()
    {
        //if(NewArray_Expressions_Count==1) {
        this.Execute2(() => new int[1]);
        //}else{
        this.Execute2(() => new int[1, 1]);
        //}
    }
    [TestMethod]
    public void NewArrayInit()
    {
        //for(var a=0;a<NewArray_Expressions_Count;a++) {
        this.Execute2(() => new[] { 1, 2 });
        //}
        this.Execute2(() => System.Array.Empty<int>());
    }
    [TestMethod]
    public void Not()
    {
        //if(this.共通UnaryExpression(Unary)) return;
        var p0 = new class_演算子オーバーロード();
        this.Execute2(() => !p0);
        //if(Unary.Type==typeof(Boolean)) {
        var p1 = true;
        this.Execute2(() => !p1);
        //} else {
        var p2 = 2;
        this.Execute2(() => ~p2);
        //}
    }
    [TestMethod]
    public void NotEqual()
    {
        //if(this.演算(特定の形式はなし,OpCodes.Ceq)) return;
        this.Execute2(() => Decimal_1 != Decimal_2);
        this.Execute2(() => Int32_1 != Int32_2);
    }
    [TestMethod]
    public void OnesComplement()
    {
        this.Execute2(
            Expression.Lambda<Func<class_演算子オーバーロード>>(
                Expression.OnesComplement(
                    Expression.Constant(new class_演算子オーバーロード())
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.OnesComplement(
                    Expression.Constant(2)
                )
            )
        );
    }
    [TestMethod]
    public void Or() => this.演算Binary(ExpressionType.Or);
    [TestMethod]
    public void OrAssign() => this.演算Binary(ExpressionType.OrAssign);
    [TestMethod]
    public void OrElse() => this.Execute2(() => Boolean1 || Boolean2);
    private delegate int FuncRef(ref int input);
    private static int Lambda(ref int input,FuncRef d) => d(ref input);
    [TestMethod]
    public void Parameter()
    {
        //if(index>=0) {
        //    if(this.RootConstant!=null) index++;
        {
            var b = 0;
            this.Execute2(() => Lambda(a => b));
        }
        //    if(Parameter.IsByRef) {
        {
            var p = Expression.Parameter(typeof(int), "p");
            var p1 = Expression.Parameter(typeof(int).MakeByRefType(), "p1");
            var p2 = Expression.Parameter(typeof(int).MakeByRefType(), "p2");
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Call(
                            typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(Lambda), BindingFlags.Static | BindingFlags.NonPublic),
                            p,
                            Expression.Lambda<FuncRef>(
                                Expression.Call(
                                    typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(Lambda), BindingFlags.Static | BindingFlags.NonPublic),
                                    p1,
                                    Expression.Lambda<FuncRef>(
                                        Expression.Block(
                                            Expression.Assign(
                                                p2,
                                                Expression.Constant(4)
                                            ),
                                            p2
                                        ),
                                        p2
                                    )
                                ),
                                p1
                            )
                        )
                    )
                )
            );
        }
        //    }
        this.Execute2(() => Lambda(a => a));
        //} else { 
        {
            var p = Expression.Parameter(typeof(int), "p");
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(p, Expression.Constant(0)),
                        p
                    )
                )
            );
        }
        //}
    }
    private interface IStruct
    {
        Struct キャスト();
    }
    private readonly struct Struct : IStruct, IEquatable<Struct>
    {
        private readonly int 内部の値;
        public Struct(int 内部の値)
        {
            this.内部の値 = 内部の値;
        }

        public Struct キャスト() => this;

        public bool Equals(Struct other) => this.内部の値==other.内部の値;
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            return obj is Struct @struct && this.Equals(@struct);
        }

        public override int GetHashCode() => this.内部の値;
    }

    [TestMethod]
    public void PointerAssign()
    {
        var Int32_ToString = typeof(int).GetMethod(nameof(Int32.ToString), Type.EmptyTypes);
        var String_ToString = typeof(string).GetMethod(nameof(String.ToString), Type.EmptyTypes);
        //if(Assign_Type.IsValueType) {
        //    switch(NodeType) {
        //        case ExpressionType.Index: {
        //            if(Index_Arguments.Count==1) {
        this.Execute2(
            Expression.Lambda<Func<string>>(
                Expression.Call(
                    Expression.Assign(
                        Expression.ArrayAccess(
                            Expression.Constant(new int[2]),
                            Expression.Constant(1)
                        ),
                        Expression.Constant(3)
                    ),
                    Int32_ToString
                )
            )
        );
        //            } else {
        this.Execute2(
            Expression.Lambda<Func<string>>(
                Expression.Call(
                    Expression.Assign(
                        Expression.ArrayAccess(
                            Expression.Constant(new int[2, 3]),
                            Expression.Constant(1),
                            Expression.Constant(2)
                        ),
                        Expression.Constant(3)
                    ),
                    Int32_ToString
                )
            )
        );
        //            }
        //        }
        //        case ExpressionType.MemberAccess: {
        //            if(Member_Member.MemberType==MemberTypes.Field) {
        //                if(Member_Field.IsStatic)
        this.Execute2(
            Expression.Lambda<Func<string>>(
                Expression.Call(
                    Expression.Assign(
                        Expression.Field(
                            null,
                            typeof(ATest).GetField(nameof(_StaticInt32), BindingFlags.Static | BindingFlags.NonPublic)
                        ),
                        Expression.Constant(1)
                    ),
                    Int32_ToString
                )
            )
        );
        //                else {
        this.Execute2(
            Expression.Lambda<Func<string>>(
                Expression.Call(
                    Expression.Assign(
                        Expression.Field(
                            Expression.Constant(this),
                            nameof(this._InstanceInt32)
                        ),
                        Expression.Constant(1)
                    ),
                    Int32_ToString
                )
            )
        );
        //                }
        //            } else {
        this.Execute2(
            Expression.Lambda<Func<string>>(
                Expression.Call(
                    Expression.Assign(
                        Expression.Property(
                            Expression.Constant(this),
                            nameof(this.InstanceInt32プロパティ)
                        ),
                        Expression.Constant(1)
                    ),
                    Int32_ToString
                )
            )
        );
        //            }
        //        }
        //        case ExpressionType.Parameter: {
        var Int32Parameter = Expression.Parameter(typeof(int));
        //            if(index>=0) {
        //                if(this.RootConstant!=null)
        this.Execute2(
            Expression.Lambda<Func<string>>(
                Expression.Block(
                    new[] { Int32Parameter },
                    Expression.Call(
                        typeof(string).GetMethod(nameof(String.Concat), new[] { typeof(object), typeof(object) }),
                        Expression.Call(
                            Expression.Constant(1m),
                            typeof(decimal).GetMethod(nameof(decimal.ToString), Type.EmptyTypes)
                        ),
                        Expression.Call(
                            Expression.Assign(
                                Int32Parameter,
                                Expression.Constant(1)
                            ),
                            Int32_ToString
                        )
                    )
                )
            )
        );
        {
            var in_p = Expression.Parameter(typeof(int), "in_p");
            var ref_p = Expression.Parameter(typeof(int).MakeByRefType(), "ref_p");
            this.Execute2(
                Expression.Lambda<Func<string>>(
                    Expression.Block(
                        new[] { in_p },
                        Expression.Call(
                            typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(String_Lambda_ref_Int32), BindingFlags.Static | BindingFlags.NonPublic),
                            in_p,
                            Expression.Lambda<String_Delegate_ref_Int32>(
                                Expression.Call(
                                    Expression.Assign(
                                        ref_p,
                                        Expression.Constant(3)
                                    ),
                                    Int32_ToString
                                ),
                                ref_p
                            )
                        )
                    )
                )
            );
        }
        //                } else {
        {
            var in_p1 = Expression.Parameter(typeof(int), "in_p1");
            var in_p2 = Expression.Parameter(typeof(int), "in_p2");
            this.Execute2(
                Expression.Lambda<Func<string>>(
                    Expression.Block(
                        new[] { in_p1 },
                        Expression.Call(
                            typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(String_Lambda_Int32), BindingFlags.Static | BindingFlags.NonPublic),
                            in_p1,
                            Expression.Lambda<String_Delegate_Int32>(
                                Expression.Call(
                                    Expression.Assign(
                                        in_p2,
                                        Expression.Constant(3)
                                    ),
                                    Int32_ToString
                                ),
                                in_p2
                            )
                        )
                    )
                )
            );
        }
        //                }
        //            } else {
        {
            var in_p1 = Expression.Parameter(typeof(int), "in_p1");
            this.Execute2(
                Expression.Lambda<Func<string>>(
                    Expression.Block(
                        new[] { in_p1 },
                        Expression.Call(
                            Expression.Assign(
                                in_p1,
                                Expression.Constant(3)
                            ),
                            Int32_ToString
                        )
                    )
                )
            );
        }
        //            }
        //        }
        //        default:
        //    }
        //} else {
        //    switch(NodeType) {
        //        case ExpressionType.Index: {
        //            if(Index_Arguments.Count==1)
        this.Execute2(
            Expression.Lambda<Func<string>>(
                Expression.Call(
                    Expression.Assign(
                        Expression.ArrayAccess(
                            Expression.Constant(new string[2]),
                            Expression.Constant(1)
                        ),
                        Expression.Constant("ABC")
                    ),
                    String_ToString
                )
            )
        );
        //            else {
        this.Execute2(
            Expression.Lambda<Func<string>>(
                Expression.Call(
                    Expression.Assign(
                        Expression.ArrayAccess(
                            Expression.Constant(new string[2, 3]),
                            Expression.Constant(1),
                            Expression.Constant(2)
                        ),
                        Expression.Constant("ABC")
                    ),
                    String_ToString
                )
            )
        );
        //            }
        //        }
        //        case ExpressionType.MemberAccess: {
        //            if(Member_Member.MemberType==MemberTypes.Field) {
        //                if(Member_Field.IsStatic) {
        this.Execute2(
            Expression.Lambda<Func<string>>(
                Expression.Call(
                    Expression.Assign(
                        Expression.Field(
                            null,
                            typeof(ATest).GetField(nameof(_StaticString), BindingFlags.Static | BindingFlags.NonPublic)
                        ),
                        Expression.Constant("ABC")
                    ),
                    String_ToString
                )
            )
        );
        //                } else {
        this.Execute2(
            Expression.Lambda<Func<string>>(
                Expression.Call(
                    Expression.Assign(
                        Expression.Field(
                            Expression.Constant(this),
                            nameof(this._InstanceString)
                        ),
                        Expression.Constant("ABC")
                    ),
                    String_ToString
                )
            )
        );
        //                }
        //            } else {
        this.Execute2(
            Expression.Lambda<Func<string>>(
                Expression.Call(
                    Expression.Assign(
                        Expression.Property(
                            Expression.Constant(this),
                            nameof(this.InstanceStringプロパティ)
                        ),
                        Expression.Constant("ABC")
                    ),
                    String_ToString
                )
            )
        );
        //            }
        //        }
        //        case ExpressionType.Parameter: {
        var StringParameter = Expression.Parameter(typeof(string));
        //            if(index>=0) {
        //                if(this.RootConstant!=null)
        this.Execute2(
            Expression.Lambda<Func<string>>(
                Expression.Block(
                    new[] { StringParameter },
                    Expression.Call(
                        typeof(string).GetMethod(nameof(String.Concat), new[] { typeof(object), typeof(object) }),
                        Expression.Call(
                            Expression.Constant(1m),
                            typeof(decimal).GetMethod(nameof(decimal.ToString), Type.EmptyTypes)
                        ),
                        Expression.Call(
                            Expression.Assign(
                                StringParameter,
                                Expression.Constant("ABC")
                            ),
                            String_ToString
                        )
                    )
                )
            )
        );
        //                if(Parameter.IsByRef) {
        {
            var in_p = Expression.Parameter(typeof(string), "in_p");
            var ref_p = Expression.Parameter(typeof(string).MakeByRefType(), "ref_p");
            this.Execute2(
                Expression.Lambda<Func<string>>(
                    Expression.Block(
                        new[] { in_p },
                        Expression.Call(
                            typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(String_Lambda_ref_String), BindingFlags.Static | BindingFlags.NonPublic),
                            in_p,
                            Expression.Lambda<String_Delegate_ref_String>(
                                Expression.Call(
                                    Expression.Assign(
                                        ref_p,
                                        Expression.Constant("ABC")
                                    ),
                                    String_ToString
                                ),
                                ref_p
                            )
                        )
                    )
                )
            );
        }
        //                } else {
        {
            var in_p1 = Expression.Parameter(typeof(string), "in_p1");
            var in_p2 = Expression.Parameter(typeof(string), "in_p2");
            this.Execute2(
                Expression.Lambda<Func<string>>(
                    Expression.Block(
                        new[] { in_p1 },
                        Expression.Call(
                            typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(String_Lambda_String), BindingFlags.Static | BindingFlags.NonPublic),
                            in_p1,
                            Expression.Lambda<String_Delegate_String>(
                                Expression.Call(
                                    Expression.Assign(
                                        in_p2,
                                        Expression.Constant("ABC")
                                    ),
                                    String_ToString
                                ),
                                in_p2
                            )
                        )
                    )
                )
            );
        }
        //                }
        //            } else {
        {
            var in_p1 = Expression.Parameter(typeof(string), "in_p1");
            this.Execute2(
                Expression.Lambda<Func<string>>(
                    Expression.Block(
                        new[] { in_p1 },
                        Expression.Call(
                            Expression.Assign(
                                in_p1,
                                Expression.Constant("ABC")
                            ),
                            String_ToString
                        )
                    )
                )
            );
        }
        //            }
        //        }
        //        default:
        //    }
        //}
    }
    private delegate string String_Delegate_ref_Int32(ref int input);
    private static string String_Lambda_ref_Int32(ref int input, String_Delegate_ref_Int32 d) => d(ref input);
    private delegate string String_Delegate_ref_String(ref string input);
    private static string String_Lambda_ref_String(ref string input, String_Delegate_ref_String d) => d(ref input);
    private delegate string String_Delegate_Int32(int input);
    private static string String_Lambda_Int32(int input, String_Delegate_Int32 d) => d(input);
    private delegate string String_Delegate_String(string input);
    private static string String_Lambda_String(string input, String_Delegate_String d) => d(input);
    [TestMethod]
    public void PointerTraverse()
    {
        //case ExpressionType.ArrayIndex: 
        //    if(e_Type.IsValueType) {
        this.Execute2(() => _struct_演算子オーバーロードArray[0].Int32フィールド);
        //    } else {
        this.Execute2(() => _class_演算子オーバーロードArray[0].Stringフィールド);
        //    }
        //}
        //case ExpressionType.MemberAccess:{
        //    if(Member_Member.MemberType==MemberTypes.Field) {
        //        if(Member_Field.IsStatic){
        //            if(Member.Type.IsValueType){
        this.Execute2(() => _Static_struct_演算子オーバーロード1.Int32フィールド);
        //            } else{
        this.Execute2(() => _Static_class_演算子オーバーロード1.Stringフィールド);
        //            }
        //        } else{
        //            if(Member.Type.IsValueType){
        this.Execute2(() => this._Instance_struct_演算子オーバーロード1.Int32フィールド);
        //            } else{
        this.Execute2(() => this._Instance_class_演算子オーバーロード1.Stringフィールド);
        //            }
        //        }
        //    } else {
        this.Execute2(() => Static_struct_演算子オーバーロード1.Int32フィールド);
        this.Execute2(() => this.Instance_struct_演算子オーバーロード.Int32フィールド);
        //    }
        //}
        //case ExpressionType.Parameter:{
        //    if(index>=0) {
        //        if(this._RootConstant!=null) index++;
        var b = 3;
        this.Execute2(() => _Static_struct_演算子オーバーロード1.Let(a => a.Int32プロパティ + b));
        //        if(Parameter.Type.IsValueType) {
        this.Execute2(() => _Static_struct_演算子オーバーロード1.Let(a => a.Int32プロパティ));
        //        } else {
        this.Execute2(() => _Static_class_演算子オーバーロード1.Let(a => a.Stringプロパティ));
        //        }
        //    } else { 
        //        if(Parameter.Type.IsValueType) {
        {
            var p = Expression.Parameter(typeof(struct_演算子オーバーロード));
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(
                            p,
                            Expression.Constant(new struct_演算子オーバーロード())
                        ),
                        Expression.Field(
                            p,
                            nameof(struct_演算子オーバーロード.Int32フィールド)
                        )
                    )
                )
            );
        }
        //        } else {
        {
            var p = Expression.Parameter(typeof(class_演算子オーバーロード));
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(
                            p,
                            Expression.Constant(this._Instance_class_演算子オーバーロード1)
                        ),
                        Expression.Field(
                            p,
                            nameof(class_演算子オーバーロード.Int32フィールド)
                        )
                    )
                )
            );
        }
        //        }
        //    }
        //}
        //case ExpressionType.Unbox:{
        {
            IStruct 入力 = new Struct(10);
            var q = Expression.Parameter(typeof(IStruct));
            var expected = this.CreateDelegate(
                Expression.Lambda<Func<IStruct, Struct>>(
                    Expression.Call(
                        Expression.Unbox(
                            q,
                            typeof(Struct)
                        ),
                        typeof(Struct).GetMethod(nameof(Struct.キャスト))
                    ),
                    q
                )
            )(入力);
            var actual = Expression.Lambda<Func<IStruct, Struct>>(
                Expression.Call(
                    Expression.Unbox(
                        q,
                        typeof(Struct)
                    ),
                    typeof(Struct).GetMethod(nameof(Struct.キャスト))
                ),
                q
            ).Compile()(入力);
            Assert.AreEqual(expected, actual);
        }
        //}
        //default:{
        this.Execute2(() => (_Boolean ? _Static_class_演算子オーバーロード1 : this._Instance_class_演算子オーバーロード1).Int32プロパティ);
        //}
    }
    [TestMethod]
    public void PostDecrementAssign()
    {
        const int a = 3;
        var p = Expression.Parameter(typeof(int));
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(p, Expression.Constant(a)),
                    Expression.PostDecrementAssign(p)
                )
            )
        );
    }
    [TestMethod]
    public void PostIncrementAssign()
    {
        const int a = 3;
        var p = Expression.Parameter(typeof(int));
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(p, Expression.Constant(a)),
                    Expression.PostIncrementAssign(p)
                )
            )
        );
    }
    [TestMethod]
    public void PostIncrementDecrementAssign()
    {
        //if(Unary_Method!=null) this._I.Call(Unary_Method);
        {
            var a = Expression.Parameter(typeof(decimal), "a");
            this.Execute2(
                Expression.Lambda<Func<decimal>>(
                    Expression.Block(
                        new[] { a },
                        Expression.Assign(
                            a,
                            Expression.Constant(1m)
                        ),
                        Expression.PostIncrementAssign(a)
                    )
                )
            );
        }
        //else {
        //    if(Unary_Type==typeof(Int32)||Unary_Type==typeof(UInt32)||Unary_Type==typeof(Int16)||Unary_Type==typeof(UInt16)||Unary_Type==typeof(Char)) I.Ldc_I4_1();
        {
            var a = Expression.Parameter(typeof(int), "a");
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { a },
                        Expression.Assign(
                            a,
                            Expression.Constant(1)
                        ),
                        Expression.PostIncrementAssign(a)
                    )
                )
            );
        }
        {
            var a = Expression.Parameter(typeof(uint), "a");
            this.Execute2(
                Expression.Lambda<Func<uint>>(
                    Expression.Block(
                        new[] { a },
                        Expression.Assign(
                            a,
                            Expression.Constant(1U)
                        ),
                        Expression.PostIncrementAssign(a)
                    )
                )
            );
        }
        {
            var a = Expression.Parameter(typeof(short), "a");
            this.Execute2(
                Expression.Lambda<Func<short>>(
                    Expression.Block(
                        new[] { a },
                        Expression.Assign(
                            a,
                            Expression.Constant((short)1)
                        ),
                        Expression.PostIncrementAssign(a)
                    )
                )
            );
        }
        {
            var a = Expression.Parameter(typeof(ushort), "a");
            this.Execute2(
                Expression.Lambda<Func<ushort>>(
                    Expression.Block(
                        new[] { a },
                        Expression.Assign(
                            a,
                            Expression.Constant((ushort)1)
                        ),
                        Expression.PostIncrementAssign(a)
                    )
                )
            );
        }
        //    else if(Unary_Type==typeof(Int64)) I.Ldc_I8(1L);
        {
            var a = Expression.Parameter(typeof(long), "a");
            this.Execute2(
                Expression.Lambda<Func<long>>(
                    Expression.Block(
                        new[] { a },
                        Expression.Assign(
                            a,
                            Expression.Constant(1L)
                        ),
                        Expression.PostIncrementAssign(a)
                    )
                )
            );
        }
        //    else if(Unary_Type==typeof(Single)) I.Ldc_R4(1F);
        {
            var a = Expression.Parameter(typeof(float), "a");
            this.Execute2(
                Expression.Lambda<Func<float>>(
                    Expression.Block(
                        new[] { a },
                        Expression.Assign(
                            a,
                            Expression.Constant(1f)
                        ),
                        Expression.PostIncrementAssign(a)
                    )
                )
            );
        }
        //    else if(Unary_Type==typeof(Double)) I.Ldc_R8(1D);
        {
            var a = Expression.Parameter(typeof(double), "a");
            this.Execute2(
                Expression.Lambda<Func<double>>(
                    Expression.Block(
                        new[] { a },
                        Expression.Assign(
                            a,
                            Expression.Constant(1d)
                        ),
                        Expression.PostIncrementAssign(a)
                    )
                )
            );
        }
        //    else throw new NotImplementedException(Unary_Type.Name+"は++,--出来ない");
        //}
    }
    [TestMethod]
    public void Power()
    {
        const double a = 3, b = 2;
        var ex = Expression.Parameter(typeof(Exception));
        this.Execute2(
            Expression.Lambda<Func<double>>(
                Expression.Power(
                    Expression.Constant(a),
                    Expression.Constant(b)
                )
            )
        );
    }
    [TestMethod]
    public void PowerAssign()
    {
        const double a = 3, b = 2;
        //if(Parameter!=null) {
        {
            var p = Expression.Parameter(typeof(double));
            this.Execute2(
                Expression.Lambda<Func<double>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(
                            p,
                            Expression.Constant(a)
                        ),
                        Expression.PowerAssign(
                            p,
                            Expression.Constant(b)
                        )
                    )
                )
            );
        }
        //} else {
        //    this.Power(特定の形式はなし);
        //    this.共通Assign(特定の形式はなし.Left);
        //}
        {
            var o = Expression.Parameter(typeof(double[]));
            var expected = this.Execute2(
                Expression.Lambda<Func<double>>(
                    Expression.Block(
                        new[] { o },
                        Expression.Assign(
                            o,
                            Expression.NewArrayBounds(
                                typeof(double),
                                Expression.Constant(1)
                            )
                        ),
                        Expression.Assign(
                            Expression.ArrayAccess(
                                o,
                                Expression.Constant(0)
                            ),
                            Expression.Constant(a)
                        ),
                        Expression.PowerAssign(
                            Expression.ArrayAccess(
                                o,
                                Expression.Constant(0)
                            ),
                            Expression.Constant(b)
                        )
                    )
                )
            );
            Assert.AreEqual(Math.Pow(a, b), expected);
        }
    }
    [TestMethod]
    public void PreDecrementAssign()
    {
        const int a = 3;
        var p = Expression.Parameter(typeof(int));
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(p, Expression.Constant(a)),
                    Expression.PreDecrementAssign(p)
                )
            )
        );
    }
    [TestMethod]
    public void PreIncrementAssign()
    {
        const int a = 3;
        var p = Expression.Parameter(typeof(int));
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(p, Expression.Constant(a)),
                    Expression.PreIncrementAssign(p)
                )
            )
        );
    }
    [TestMethod]
    public void 共通PreIncrementDecrementAssign()
    {
        //if(Parameter!=null) {
        {
            const int a = 3;
            var p = Expression.Parameter(typeof(int));
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(
                            p,
                            Expression.Constant(a)
                        ),
                        Expression.PreIncrementAssign(p)
                    )
                )
            );
        }
        //}else{
        {
            var Field = typeof(ATest).GetField(nameof(_Static_class_演算子オーバーロード1), BindingFlags.Static | BindingFlags.NonPublic);
            var o = new class_演算子オーバーロード(1);
            var expected = this.Execute2(
                Expression.Lambda<Func<class_演算子オーバーロード>>(
                    Expression.Block(
                        Expression.Assign(
                            Expression.Field(
                                null,
                                Field
                            ),
                            Expression.Constant(o)
                        ),
                        Expression.PreIncrementAssign(
                            Expression.Field(
                                null,
                                Field
                            )
                        )
                    )
                )
            );
            var actual = new class_演算子オーバーロード(1);
            actual++;
            Assert.AreEqual(actual, expected);
            Assert.AreEqual(_Static_class_演算子オーバーロード1, actual);
        }
        //}
    }
    private static TResult 先行評価されないLambda<TResult>(Func<int, TResult> e) => e(0);

    [TestMethod]
    public void Lambda()
    {
        //if(this._DictionaryLambdaField.TryGetValue(Lambda,out Field)) {
        this.Execute2(() => 先行評価されないLambda(a => a).NoEarlyEvaluation() + 先行評価されないLambda(b => b).NoEarlyEvaluation());
        //} else {
        //    _Target[Delegate番号]=RootParametereが含まれている
        //        ? D.CreateDelegate(Lambda.Type,_Target)
        this.Execute2(() => Lambda(a => a));
        //        : D.CreateDelegate(Lambda.Type);
        var c = 1;
        this.Execute2(() => Lambda(a => c));
        //}
    }
    [TestMethod]
    public void LeftShift() => this.演算Binary(ExpressionType.LeftShift);
    [TestMethod]
    public void LeftShiftAssign() => this.演算Binary(ExpressionType.LeftShiftAssign);
    private void 大小論理Binary(ExpressionType NodeType)
    {
        var p = Expression.Parameter(typeof(int));
        this.Execute2(
            Expression.Lambda<Func<bool>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Constant(2)
                    ),
                    Expression.MakeBinary(
                        NodeType,
                        p,
                        Expression.Constant(2)
                    )
                )
            )
        );
    }
    [TestMethod]
    public void LessThan() => this.大小論理Binary(ExpressionType.LessThan);
    [TestMethod]
    public void LessThanOrEqual() => this.大小論理Binary(ExpressionType.LessThanOrEqual);

    private struct StructList : IList<int>
    {
        public int this[int index] {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();
        public void Add(int item) { }
        public void Clear() { }
        public bool Contains(int item) => true;
        public void CopyTo(int[] array, int arrayIndex) { }
        public IEnumerator<int> GetEnumerator()
        {
            yield return 0;
        }
        public int IndexOf(int item) => 1;
        public void Insert(int index, int item) { }
        public bool Remove(int item) => true;
        public void RemoveAt(int index) { }
        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return 0;
        }
    }
    [TestMethod]
    public void ListInit()
    {
        //foreach(var Initializer in ListInit.Initializers) {
        //    if(ListInit_Type.IsValueType) {
        this.Execute2(() => new StructList { 1 });
        //    }else{
        this.Execute2(() => new List<int> { 1 });
        //    }
        //    if(AddMethod.ReturnType!=typeof(void)) {
        this.Execute2(() => new HashSet<int> { 3 });
        //    }else{
        this.Execute2(() => new List<int> { 2 });
        //    }
        //}
    }
    [TestMethod]
    public void Loop0(){
        //引数4なら4+3+2+1=10
        //if(Loop.ContinueLabel!=null) {
        var 引数 = Expression.Parameter(typeof(int),"引数");
        var 合計 = Expression.Parameter(typeof(int),"合計");
        var Break = Expression.Label();
        var Continue = Expression.Label();
        var R = this.Execute2(
            Expression.Lambda<Func<int, int>>(
                Expression.Block(
                    new[] { 合計 },
                    Expression.Assign(
                        合計,
                        Expression.Constant(0)
                    ),
                    Expression.Loop(
#if false
                        Expression.IfThen(
                            Expression.Not(
                                Expression.Equal(
                                    引数,
                                    Expression.Constant(0)
                                )
                            ),
                            Expression.Block(
                                Expression.AddAssign(合計,引数),
                                Expression.PostDecrementAssign(引数)
                            )
                        ),
#else
                        Expression.IfThenElse(
                            Expression.Equal(引数,Expression.Constant(0)),
                            Expression.Break(Break),
                            Expression.Block(
                                Expression.AddAssign(合計,引数),
                                Expression.PostDecrementAssign(引数)
                            )
                        ),
#endif
                        Break,
                        Continue
                    ),
                    合計
                ),
                引数
            ),4
        );
        Assert.AreEqual(R, 4 + 3 + 2 + 1);
    }
    [TestMethod]
    public void Loop1(){
        var 引数 = Expression.Parameter(typeof(int));
        var 合計 = Expression.Parameter(typeof(int));
        var Break = Expression.Label(typeof(int));
        var Continue = Expression.Label();
        var R = this.Execute2(
            Expression.Lambda<Func<int, int>>(
                Expression.Block(
                    new[] { 合計 },
                    Expression.Assign(
                        合計,
                        Expression.Constant(0)
                    ),
                    Expression.Loop(
                        Expression.IfThenElse(
                            Expression.Equal(
                                引数,
                                Expression.Constant(0)
                            ),
                            Expression.Break(Break, 合計),
                            Expression.Block(
                                Expression.AddAssign(
                                    合計,
                                    引数
                                ),
                                Expression.PostDecrementAssign(
                                    引数
                                ),
                                Expression.IfThen(
                                    Expression.Equal(
                                        引数,
                                        Expression.Constant(0)
                                    ),
                                    Expression.Continue(Continue)
                                )
                            )
                        ),
                        Break,
                        Continue
                    )
                ),
                引数
            ),4
        );
        Assert.AreEqual(R, 4 + 3 + 2 + 1);
    }
    [TestMethod]
    public void Loop2(){
        //} else if(Loop.BreakLabel!=null) {
        var 引数 = Expression.Parameter(typeof(int));
        var 合計 = Expression.Parameter(typeof(int));
        var Break = Expression.Label();
        var R = this.Execute2(
            Expression.Lambda<Func<int, int>>(
                Expression.Block(
                    new[] { 合計 },
                    Expression.Assign(
                        合計,
                        Expression.Constant(0)
                    ),
                    Expression.Loop(
                        Expression.IfThenElse(
                            Expression.Equal(
                                引数,
                                Expression.Constant(0)
                            ),
                            Expression.Break(Break),
                            Expression.Block(
                                Expression.AddAssign(
                                    合計,
                                    引数
                                ),
                                Expression.PostDecrementAssign(
                                    引数
                                )
                            )
                        ),
                        Break
                    ),
                    合計
                ),
                引数
            ),4
        );
        Assert.AreEqual(R, 4 + 3 + 2 + 1);
    }
    [TestMethod]
    public void Loop3(){
        var 引数 = Expression.Parameter(typeof(int));
        var 合計 = Expression.Parameter(typeof(int));
        var Break = Expression.Label(typeof(int));
        var R = this.Execute2(
            Expression.Lambda<Func<int, int>>(
                Expression.Block(
                    new[] { 合計 },
                    Expression.Assign(
                        合計,
                        Expression.Constant(0)
                    ),
                    Expression.Loop(
                        Expression.IfThenElse(
                            Expression.Equal(
                                引数,
                                Expression.Constant(0)
                            ),
                            Expression.Break(Break, 合計),
                            Expression.Block(
                                Expression.AddAssign(
                                    合計,
                                    引数
                                ),
                                Expression.PostDecrementAssign(
                                    引数
                                )
                            )
                        ),
                        Break
                    )
                ),
                引数
            ),4
        );
        Assert.AreEqual(R, 4 + 3 + 2 + 1);
    }
    [TestMethod]
    public void Loop4LoopBreak_LoopContinue(){
        //} else {
        var 引数 = Expression.Parameter(typeof(int),"引数");
        var 合計 = Expression.Parameter(typeof(int),"合計");
        var Break = Expression.Label("Break");
        var Continue = Expression.Label("Continue");
        var R = this.Execute2(
            Expression.Lambda<Func<int, int>>(
                Expression.Block(
                    new[] { 合計 },
                    Expression.Assign(
                        合計,
                        Expression.Constant(0)
                    ),
                    Expression.Loop(
                        Expression.Block(
                            Expression.IfThenElse(
                                Expression.Equal(
                                    引数,
                                    Expression.Constant(0)
                                ),
                                Expression.Break(Break),
                                Expression.Block(
                                    Expression.AddAssign(
                                        合計,
                                        引数
                                    ),
                                    Expression.PostDecrementAssign(
                                        引数
                                    ),
                                    Expression.IfThen(
                                        Expression.Equal(
                                            引数,
                                            Expression.Constant(0)
                                        ),
                                        Expression.Continue(Continue)
                                    )
                                )
                            )
                        ),
                        Break,
                        Continue
                    ),
                    合計
                ),
                引数
            ),4
        );
        Assert.AreEqual(R, 4 + 3 + 2 + 1);
        //}
    }
    [TestMethod]
    public void Loop5LoopBreak_ContinueLabel(){
        //} else {
        var 引数 = Expression.Parameter(typeof(int),"引数");
        var 合計 = Expression.Parameter(typeof(int),"合計");
        var Break = Expression.Label("Break");
        var Continue = Expression.Label("Continue");
        var R = this.Execute2(
            Expression.Lambda<Func<int, int>>(
                Expression.Block(
                    new[] { 合計 },
                    Expression.Assign(
                        合計,
                        Expression.Constant(0)
                    ),
                    Expression.Loop(
                        Expression.Block(
                            Expression.Label(Continue),
                            Expression.IfThenElse(
                                Expression.Equal(
                                    引数,
                                    Expression.Constant(0)
                                ),
                                Expression.Break(Break),
                                Expression.Block(
                                    Expression.AddAssign(
                                        合計,
                                        引数
                                    ),
                                    Expression.PostDecrementAssign(
                                        引数
                                    ),
                                    Expression.IfThen(
                                        Expression.Equal(
                                            引数,
                                            Expression.Constant(0)
                                        ),
                                        Expression.Continue(Continue)
                                    )
                                )
                            )
                        ),
                        Break
                    ),
                    合計
                ),
                引数
            ),4
        );
        Assert.AreEqual(R, 4 + 3 + 2 + 1);
        //}
    }
    [TestMethod]
    public void Loop6BreakLabel_LoopContinue(){
        //} else {
        var 引数 = Expression.Parameter(typeof(int),"引数");
        var 合計 = Expression.Parameter(typeof(int),"合計");
        var Break = Expression.Label("Break");
        var Continue = Expression.Label("Continue");
        var R = this.Execute2(
            Expression.Lambda<Func<int, int>>(
                Expression.Block(
                    new[] { 合計 },
                    Expression.Assign(
                        合計,
                        Expression.Constant(0)
                    ),
                    Expression.Loop(
                        Expression.Block(
                            Expression.IfThenElse(
                                Expression.Equal(
                                    引数,
                                    Expression.Constant(0)
                                ),
                                Expression.Break(Break),
                                Expression.Block(
                                    Expression.AddAssign(
                                        合計,
                                        引数
                                    ),
                                    Expression.PostDecrementAssign(
                                        引数
                                    ),
                                    Expression.IfThen(
                                        Expression.Equal(
                                            引数,
                                            Expression.Constant(0)
                                        ),
                                        Expression.Continue(Continue)
                                    )
                                )
                            )
                        ),
                        null,
                        Continue
                    ),
                    Expression.Label(Break),
                    合計
                ),
                引数
            ),4
        );
        Assert.AreEqual(R, 4 + 3 + 2 + 1);
        //}
    }
    [TestMethod]
    public void Loop7BreakLabel_ContinueLabel(){
        //} else {
        var 引数 = Expression.Parameter(typeof(int),"引数");
        var 合計 = Expression.Parameter(typeof(int),"合計");
        var Break = Expression.Label("Break");
        var Continue = Expression.Label("Continue");
        var R = this.Execute2(
            Expression.Lambda<Func<int, int>>(
                Expression.Block(
                    new[] { 合計 },
                    Expression.Assign(
                        合計,
                        Expression.Constant(0)
                    ),
                    Expression.Loop(
                        Expression.Block(
                            Expression.Label(Continue),
                            Expression.IfThenElse(
                                Expression.Equal(
                                    引数,
                                    Expression.Constant(0)
                                ),
                                Expression.Break(Break),
                                Expression.Block(
                                    Expression.AddAssign(
                                        合計,
                                        引数
                                    ),
                                    Expression.PostDecrementAssign(
                                        引数
                                    ),
                                    Expression.IfThen(
                                        Expression.Equal(
                                            引数,
                                            Expression.Constant(0)
                                        ),
                                        Expression.Continue(Continue)
                                    )
                                )
                            )
                        )
                    ),
                    Expression.Label(Break),
                    合計
                ),
                引数
            ),4
        );
        Assert.AreEqual(R, 4 + 3 + 2 + 1);
        //}
    }
    //[TestMethod] public void Power()=>this._変数Cache.Execute(()=>Int32_1!=Int32_2);
    [TestMethod]
    public void RightShift() => this.演算Binary(ExpressionType.RightShift);
    [TestMethod]
    public void RightShiftAssign() => this.演算Binary(ExpressionType.RightShiftAssign);
    [TestMethod]public void Switch0(){
        this.Switch<XmlType>(pp =>
            Enumerable.Range(64, 1).Select(a =>
                Expression.SwitchCase(
                    Expression.Assign(pp, Expression.Constant((XmlType)a)),
                    Enumerable.Range(a * 2, 2).Select(b => Expression.Constant((XmlType)b)).ToArray()
                )
            )
        );
    }
    private static readonly int[]Case数Enumerable={ 1, 63, 64 };
    [TestMethod]public void Switch01(){
        foreach (var Case数 in Case数Enumerable){
            this.Switch<XmlType>(pp => Enumerable.Range(1, Case数).Select(a =>
                Expression.SwitchCase(
                    Expression.Assign(pp, Expression.Constant((XmlType)a)),
                    Enumerable.Range(a * 2, 2).Select(b => (Expression)Expression.Constant((XmlType)b)).ToArray()
                )
            ));
        }
    }
    [TestMethod]public void Switch02(){
        var p2 = Expression.Parameter(typeof(int));
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Switch(
                    Expression.Constant(124),
                    Expression.Default(typeof(int)),
                    Expression.SwitchCase(
                        Expression.Constant(-124),
                        Expression.Constant(124)
                    ),
                    Expression.SwitchCase(
                        Expression.Constant(-125),
                        Expression.Constant(125)
                    )
                )
            )
        );
    }
    [TestMethod]public void Switch03(){
        var p2 = Expression.Parameter(typeof(int));
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { p2 },
                    Expression.Switch(
                        Expression.Constant(124),
                        Expression.Assign(
                            p2,
                            Expression.Default(typeof(int))
                        ),
                        Expression.SwitchCase(
                            Expression.Assign(p2, Expression.Constant(-66)),
                            Expression.Increment(
                                Expression.Constant(124)
                            )
                        ),
                        Expression.SwitchCase(
                            Expression.Assign(p2, Expression.Constant(-64)),
                            Expression.Increment(
                                Expression.Constant(124)
                            )
                        )
                    ),
                    p2
                )
            )
        );
    }
    [TestMethod]public void Switch04(){
        foreach (var Case数 in Case数Enumerable){
            this.Switch<XmlType>(pp => Enumerable.Range(1, Case数).Select(a =>
                Expression.SwitchCase(
                    Expression.Assign(pp, Expression.Constant((XmlType)a)),
                    Enumerable.Range(a * 2, 2).Select(b => (Expression)Expression.Constant((XmlType)b)).ToArray()
                )
            ));
        }
    }
    [TestMethod]public void Switch05(){
        foreach (var Case数 in Case数Enumerable){
            this.Switch<XmlType>(pp => Enumerable.Range(1, Case数).Select(a =>
                Expression.SwitchCase(
                    Expression.Assign(pp, Expression.Constant((XmlType)a)),
                    Enumerable.Range(a * 2, 2).Select(b => (Expression)Expression.Constant((XmlType)b)).ToArray()
                )
            ));
        }
    }
    [TestMethod]public void Switch06(){
        this.Switch<int>(
            result変数 => Enumerable.Range(1, 1).Select(a =>
                Expression.SwitchCase(
                    Expression.Assign(
                        result変数,
                        Expression.Constant(a)
                    ),
                    new[] { -128, -1, 0, 127 }.Select(testValue =>
                        (Expression)Expression.Constant(testValue)
                    ).ToArray()
                )
            )
        );
    }
    [TestMethod]public void Switch07(){
        var p = Expression.Parameter(typeof(int));
        var Lambda = Expression.Lambda<Func<int>>(
            Expression.Block(
                new[] { p },
                Expression.Switch(
                    Expression.Constant(124),
                    Expression.Assign(
                        p,
                        Expression.Default(typeof(int))
                    ),
                    Expression.SwitchCase(
                        Expression.Assign(p, Expression.Constant(-66)),
                        Expression.Constant(124)
                    ),
                    Expression.SwitchCase(
                        Expression.Assign(p, Expression.Constant(-64)),
                        Expression.Constant(-1)
                    )
                ),
                p
            )
        );
        this.Execute2(Lambda);
    }
    [TestMethod]public void Switch08(){
        this.Switch<decimal>(pp => 
            Enumerable.Range(1, 2).Select(a =>
                Expression.SwitchCase(
                    Expression.Assign(pp, Expression.Constant((decimal)a)),
                    Enumerable.Range(a * 2, 2).Select(b => (Expression)Expression.Constant((decimal)b)).ToArray()
                )
            )
        );
    }
    [TestMethod]public void Switch09(){
        foreach (var Case数 in Case数Enumerable){
            this.Switch<byte>(pp => 
                Enumerable.Range(1, Case数).Select(a =>
                    Expression.SwitchCase(
                        Expression.Assign(pp, Expression.Constant((byte)a)),
                        Enumerable.Range(a * 2, 2).Select(b => (Expression)Expression.Constant((byte)b)).ToArray()
                    )
                )
            );
        }
    }
    [TestMethod]public void Switch10(){
        foreach (var Case数 in Case数Enumerable){
            this.Switch<ushort>(pp => 
                Enumerable.Range(1, Case数).Select(a =>
                    Expression.SwitchCase(
                        Expression.Assign(pp, Expression.Constant((ushort)a)),
                        Enumerable.Range(a * 2, 2).Select(b => (Expression)Expression.Constant((ushort)b)).ToArray()
                    )
                )
            );
        }
    }
    [TestMethod]public void Switch11(){
        foreach (var Case数 in Case数Enumerable){
            this.Switch<uint>(pp => 
                Enumerable.Range(1, Case数).Select(a =>
                    Expression.SwitchCase(
                        Expression.Assign(pp, Expression.Constant((uint)a)),
                        Enumerable.Range(a * 2, 2).Select(b => (Expression)Expression.Constant((uint)b)).ToArray()
                    )
                )
            );
        }
    }
    [TestMethod]public void Switch12(){
        foreach (var Case数 in Case数Enumerable){
            this.Switch<int>(pp => 
                Enumerable.Range(1, Case数).Select(a =>
                    Expression.SwitchCase(
                        Expression.Assign(pp, Expression.Constant(a)),
                        Enumerable.Range(a * 2, 2).Select(b => (Expression)Expression.Constant(b)).ToArray()
                    )
                )
            );
        }
    }
    [TestMethod]public void Switch13(){
        foreach (var Case数 in Case数Enumerable){
            this.Switch<short>(pp => 
                Enumerable.Range(1, Case数).Select(a =>
                    Expression.SwitchCase(
                        Expression.Assign(pp, Expression.Constant((short)a)),
                        Enumerable.Range(a * 2, 2).Select(b => (Expression)Expression.Constant((short)b)).ToArray()
                    )
                )
            );
        }
    }
    [TestMethod]public void Switch14(){
        foreach (var Case数 in Case数Enumerable){
            this.Switch<int>(pp => 
                Enumerable.Range(1, Case数).Select(a =>
                    Expression.SwitchCase(
                        Expression.Assign(pp, Expression.Constant(a)),
                        Enumerable.Range(a * 2, 2).Select(b => (Expression)Expression.Constant(b)).ToArray()
                    )
                )
            );
        }
    }
    private void Switch<T>(Func<ParameterExpression, IEnumerable<SwitchCase>> func){
        var p = Expression.Parameter(typeof(T));
        var SwitchCases = func(p).ToArray();
        foreach (var SwitchCase in SwitchCases){
            var TestValues = SwitchCase.TestValues;
            foreach (var TestValue in TestValues){
                var Lambda = Expression.Lambda<Func<T>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Switch(
                            TestValue,
                            Expression.Assign(
                                p,
                                Expression.Default(typeof(T))
                            ),
                            null,
                            SwitchCases
                        ),
                        p
                    )
                );
                this.Execute2(Lambda);
            }
        }
    }
    private void PrivateTry(Expression<Action<Exception>>Lambda,Exception input){
        var M = this.CreateDelegate(Lambda);
        M(input);
    }
    [TestMethod,ExpectedException(typeof(OverflowException))]
    public void Try_OverflowException(){
        var input = Expression.Parameter(typeof(Exception));
        var OverflowException = Expression.Parameter(typeof(OverflowException), "OverflowException");
        var ApplicationException = Expression.Parameter(typeof(ApplicationException), "ApplicationException");
        this.PrivateTry(
            Expression.Lambda<Action<Exception>>(
                Expression.TryCatch(
                    Expression.Throw(input),
                    Expression.Catch(OverflowException,Expression.Throw(OverflowException)),
                    Expression.Catch(ApplicationException,Expression.Throw(ApplicationException))
                ),
                input
            ),new OverflowException()
        );
    }
    [TestMethod,ExpectedException(typeof(ApplicationException))]
    public void Try_ApplicationException(){
        var input = Expression.Parameter(typeof(Exception));
        this.PrivateTry(
            Expression.Lambda<Action<Exception>>(
                Expression.TryCatch(
                    Expression.Throw(input),
                    Expression.Catch(
                        typeof(OverflowException),
                        Expression.Rethrow()
                    ),
                    Expression.Catch(
                        typeof(ApplicationException),
                        Expression.Rethrow()
                    )
                ),
                input
            ),new ApplicationException()
        );
    }
    [TestMethod]
    public void Try_Fault0(){
        //fault内部では例外が発生しないようだ
        var input = Expression.Parameter(typeof(Exception),"input");
        this.PrivateTry(
            Expression.Lambda<Action<Exception>>(
                Expression.TryFault(
                    Expression.Constant(2),
                    Expression.Throw(input)
                ),
                input
            ),new ApplicationException()
        );
    }
    [TestMethod,ExpectedException(typeof(ApplicationException))]
    public void Try_Finally1(){
        var input=Expression.Parameter(typeof(Exception),"input");
        this.PrivateTry(
            Expression.Lambda<Action<Exception>>(
                Expression.TryFinally(
                    Expression.Constant(2),
                    Expression.Throw(input)
                ),
                input
            ),new ApplicationException()
        );
    }
    [TestMethod]
    public void Try_Fault1(){
        var M = this.CreateDelegate(
            Expression.Lambda<Func<int>>(
                Expression.TryFault(
                    Expression.Constant(2),
                    Expression.Constant(3)
                )
            )
        );
        var actual=M();
        Assert.AreEqual(2,actual);
    }
    [TestMethod]
    public void Try_Fault2(){
        var 評価検証=new 評価検証(1);
        var input=Expression.Parameter(typeof(評価検証),"評価検証");
        var M = this.CreateDelegate(
            Expression.Lambda<Action<評価検証>>(
                Expression.TryFault(
                    Expression.Throw(Expression.Constant(new ApplicationException())),
                    Expression.Add(input,input)
                ),
                input
            )
        );
        try{
            M(評価検証);
        } catch(ApplicationException){

        }
        Assert.AreEqual(1,評価検証.評価回数);
    }
    [TestMethod]
    public void Try_Finally0(){
        const int 評価検証値=4;
        var 評価検証=new 評価検証(評価検証値);
        var M = this.CreateDelegate(
            Expression.Lambda<Func<int>>(
                Expression.TryFinally(
                    Expression.Constant(5),
                    Expression.Add(
                        Expression.Constant(評価検証),
                        Expression.Constant(評価検証)
                    )
                )
            )
        );
        var actual=M();
        Assert.AreEqual(5,評価検証値,actual);
        Assert.AreEqual(1,評価検証.評価回数);
        Assert.AreEqual(評価検証値+評価検証値,評価検証.値+評価検証.値);
    }
    [TestMethod]
    public void TryFilter()
    {
        var ex = Expression.Parameter(typeof(Exception));
        var Exception = Expression.Parameter(typeof(Exception), "Exception");
        {
            var 初期値 = "初期値";
            var result = Expression.Parameter(typeof(object), "result");
            this.CreateDelegate(
                Expression.Lambda<Func<Exception, object>>(
                    Expression.Block(
                        new[] { result },
                        Expression.Assign(result, Expression.Constant(初期値)),
                        Expression.TryCatch(
                            Expression.Throw(ex, typeof(object)),
                            Expression.Catch(
                                Exception,
                                Expression.Assign(result, Exception),
                                Expression.TypeEqual(Exception, typeof(DivideByZeroException))
                            )
                        ),
                        result
                    ),
                    ex
                )
            );
        }
    }
    [TestMethod]
    public void Try_TryCatchパラメータなし() => this.Execute2(
        Expression.Lambda<Func<int>>(
            Expression.TryCatch(
                Expression.Divide(
                    Expression.Constant(4),
                    Expression.Constant(2)
                ),
                Expression.Catch(
                    typeof(DivideByZeroException),
                    Expression.Constant(2)
                )
            )
        )
    );
    [TestMethod]
    public void Try_TryCatchパラメータあり()
    {
        var ex = Expression.Parameter(typeof(Exception));
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.TryCatch(
                    Expression.Divide(
                        Expression.Constant(4),
                        Expression.Constant(2)
                    ),
                    Expression.Catch(
                        ex,
                        Expression.Constant(2)
                    )
                )
            )
        );
    }
    [TestMethod]
    public void Try_TryCatchFinally()
    {
        var p = Expression.Parameter(typeof(int));
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Constant(1)
                    ),
                    Expression.TryCatchFinally(
                        Expression.PostIncrementAssign(p),
                        Expression.Assign(
                            p,
                            Expression.Constant(2)
                        )
                    ),
                    p
                )
            )
        );
    }
    [TestMethod]
    public void TryCatchFinally0()
    {
        const int Catch値 = 40,Finally値=30;
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.TryCatchFinally(
                    Expression.Throw(
                        Expression.New(typeof(Exception)),
                        typeof(int)
                    ),
                    Expression.Constant(Finally値),
                    Expression.Catch(
                        typeof(Exception),
                        Expression.Constant(Catch値)
                    )
                )
            )
        );
    }
    [TestMethod]
    public void TryCatchFinally1()
    {
        const int Left値 = 2, Catch値 = 40,Finally値=30;
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Multiply(
                    Expression.Constant(Left値),
                    Expression.TryCatchFinally(
                        Expression.Throw(
                            Expression.New(typeof(Exception)),
                            typeof(int)
                        ),
                        Expression.Constant(Finally値),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(Catch値)
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void TryThrow値がBinaryLeftの評価スタックを跨ぐ0()
    {
        const int Catch値 = 4;
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.TryCatch(
                    Expression.Throw(
                        Expression.New(typeof(Exception)),
                        typeof(int)
                    ),
                    Expression.Catch(
                        typeof(Exception),
                        Expression.Constant(Catch値)
                    )
                )
            )
        );
    }
    [TestMethod]
    public void TryThrow値がBinaryLeftの評価スタックを跨ぐ1()
    {
        const int Left値 = 2, Catch値 = 4;
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Multiply(
                    Expression.Constant(Left値),
                    Expression.TryCatch(
                        Expression.Throw(
                            Expression.New(typeof(Exception)),
                            typeof(int)
                        ),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(Catch値)
                        )
                    )
                )
            )
        );
    }
    private static int Call4(int a, int b, int c, int d) => a * b / c * d;
    private static TryExpression TryCatchExpression(int v) => Expression.TryCatch(
        Expression.Throw(
            Expression.New(typeof(Exception)),
            typeof(int)
        ),
        Expression.Catch(
            typeof(Exception),
            Expression.Constant(v)
        )
    );
    private void TryCatch値がCallの評価スタックを跨ぐ(Expression e1,Expression e2,Expression e3,Expression e4) => this.Execute2(
        Expression.Lambda<Func<int>>(
            Expression.Call(
                typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(Call4),BindingFlags.Static|BindingFlags.NonPublic),
                e1,
                e2,
                e3,
                e4
            )
        )
    );
    [TestMethod]
    public void TryCatch値がCallの評価スタックを跨ぐ()
    {
        this.TryCatch値がCallの評価スタックを跨ぐ(
            Expression.Constant(2),
            Expression.Constant(3),
            Expression.Constant(4),
            Expression.Constant(5)
        );
        this.TryCatch値がCallの評価スタックを跨ぐ(
            Expression.Constant(2),
            Expression.Constant(3),
            Expression.Constant(4),
            TryCatchExpression(5)
        );
        this.TryCatch値がCallの評価スタックを跨ぐ(
            Expression.Constant(2),
            Expression.Constant(3),
            TryCatchExpression(4),
            Expression.Constant(5)
        );
        this.TryCatch値がCallの評価スタックを跨ぐ(
            Expression.Constant(2),
            Expression.Constant(3),
            TryCatchExpression(4),
            TryCatchExpression(5)
        );
        this.TryCatch値がCallの評価スタックを跨ぐ(
            Expression.Constant(2),
            TryCatchExpression(3),
            Expression.Constant(4),
            Expression.Constant(5)
        );
        this.TryCatch値がCallの評価スタックを跨ぐ(
            Expression.Constant(2),
            TryCatchExpression(3),
            Expression.Constant(4),
            TryCatchExpression(5)
        );
        this.TryCatch値がCallの評価スタックを跨ぐ(
            Expression.Constant(2),
            TryCatchExpression(3),
            TryCatchExpression(4),
            Expression.Constant(5)
        );
        this.TryCatch値がCallの評価スタックを跨ぐ(
            Expression.Constant(2),
            TryCatchExpression(3),
            TryCatchExpression(4),
            TryCatchExpression(5)
        );
        this.TryCatch値がCallの評価スタックを跨ぐ(
            TryCatchExpression(2),
            Expression.Constant(3),
            Expression.Constant(4),
            Expression.Constant(5)
        );
        this.TryCatch値がCallの評価スタックを跨ぐ(
            TryCatchExpression(2),
            Expression.Constant(3),
            Expression.Constant(4),
            TryCatchExpression(5)
        );
        this.TryCatch値がCallの評価スタックを跨ぐ(
            TryCatchExpression(2),
            Expression.Constant(3),
            TryCatchExpression(4),
            Expression.Constant(5)
        );
        this.TryCatch値がCallの評価スタックを跨ぐ(
            TryCatchExpression(2),
            Expression.Constant(3),
            TryCatchExpression(4),
            TryCatchExpression(5)
        );
        this.TryCatch値がCallの評価スタックを跨ぐ(
            TryCatchExpression(2),
            TryCatchExpression(3),
            Expression.Constant(4),
            Expression.Constant(5)
        );
        this.TryCatch値がCallの評価スタックを跨ぐ(
            TryCatchExpression(2),
            TryCatchExpression(3),
            Expression.Constant(4),
            TryCatchExpression(5)
        );
        this.TryCatch値がCallの評価スタックを跨ぐ(
            TryCatchExpression(2),
            TryCatchExpression(3),
            TryCatchExpression(4),
            Expression.Constant(5)
        );
        this.TryCatch値がCallの評価スタックを跨ぐ(
            TryCatchExpression(2),
            TryCatchExpression(3),
            TryCatchExpression(4),
            TryCatchExpression(5)
        );
    }
    [TestMethod]
    public void TypeAs() => this.Execute2(() => Object_String as string);
    [TestMethod]
    public void TypeEqual()
    {
        var p = Expression.Parameter(typeof(object));
        this.Execute2(
            Expression.Lambda<Func<bool>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Convert(
                            Expression.Constant(1),
                            typeof(object)
                        )
                    ),
                    Expression.TypeEqual(
                        p,
                        typeof(int)
                    )
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<bool>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Convert(
                            Expression.Constant(1),
                            typeof(object)
                        )
                    ),
                    Expression.TypeEqual(
                        p,
                        typeof(short)
                    )
                )
            )
        );
    }
    [TestMethod]
    public void TypeIs() => this.Execute2(() => Object_String is string);
    public delegate int outパラメータを引数に取りIntを返す1(ref int q);
    private static int out_Lambda1(ref int v,outパラメータを引数に取りIntを返す1 a) => a(ref v);
    public delegate int outパラメータを引数に取りIntを返す2(ref int q);
    private static int in_Lambda(int v,Func<int,int> a) => a(v);
    private void 共通化Assign(ExpressionType NodeType)
    {
        //switch(Binary_Left.NodeType) {
        //    case ExpressionType.Parameter: {
        //        if(index>=0) {
        //            if(Parameter.IsByRef) {
        {
            var p = Expression.Parameter(typeof(int));
            var q = Expression.Parameter(typeof(int).MakeByRefType());
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(
                            p,
                            Expression.Constant(0)
                        ),
                        Expression.Call(
                            typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(out_Lambda1), BindingFlags.NonPublic | BindingFlags.Static),
                            p,
                            Expression.Lambda<outパラメータを引数に取りIntを返す1>(
                                Expression.Block(
                                    Expression.Assign(
                                        q,
                                        Expression.Constant(0)
                                    ),
                                    Expression.MakeBinary(
                                        NodeType,
                                        q,
                                        Expression.Constant(2)
                                    )
                                ),
                                q
                            )
                        ),
                        p
                    )
                )
            );
        }
        //            } else {
        {
            var p = Expression.Parameter(typeof(int));
            var q = Expression.Parameter(typeof(int));
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(p, Expression.Constant(0)),
                        Expression.Call(
                            typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(in_Lambda), BindingFlags.NonPublic | BindingFlags.Static),
                            p,
                            Expression.Lambda<Func<int, int>>(
                                Expression.Block(
                                    Expression.Assign(
                                        q,
                                        Expression.Constant(0)
                                    ),
                                    Expression.MakeBinary(
                                        NodeType,
                                        q,
                                        Expression.Constant(2)
                                    )
                                ), q)
                        ),
                        p
                    )
                )
            );
        }
        //            }
        //        } else {
        {
            var p = Expression.Parameter(typeof(int));
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(p, Expression.Constant(0)),
                        Expression.MakeBinary(
                            NodeType,
                            p,
                            Expression.Constant(2)
                        )
                    )
                )
            );
        }
        //        }
        //    }
        //    case ExpressionType.Index: {
        //    }
        //    case ExpressionType.MemberAccess: {
        //        if(Member_Member.MemberType==MemberTypes.Field) {
        //            if(Member_Field.IsStatic) {
        {
            const int expected = 2;
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        Expression.Assign(
                            Expression.Field(
                                null,
                                typeof(Test_作成_DynamicMethodによるDelegate作成).GetField(nameof(_StaticInt32), BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.NonPublic)
                            ),
                            Expression.Constant(0)
                        ),
                        Expression.MakeBinary(
                            NodeType,
                            Expression.Field(
                                null,
                                this.GetType().GetField(nameof(_StaticInt32), BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                            ),
                            Expression.Constant(expected)
                        )
                    )
                )
            );
        }
        //            } else {
        {
            const int expected = 2;
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        Expression.Assign(
                            Expression.Field(
                                Expression.Constant(this),
                                typeof(Test_作成_DynamicMethodによるDelegate作成).GetField(nameof(this._InstanceInt32), BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.NonPublic)
                            ),
                            Expression.Constant(0)
                        ),
                        Expression.MakeBinary(
                            NodeType,
                            Expression.Field(
                                Expression.Constant(
                                    this
                                ),
                                this.GetType().GetField(nameof(this._InstanceInt32), BindingFlags.NonPublic | BindingFlags.Instance)
                            ),
                            Expression.Constant(expected)
                        )
                    )
                )
            );
        }
        //            }
        //        } else {
        //        }
        //    }
        //    default: {
        //        throw new InvalidOperationException();
        //    }
        //}
    }
    [TestMethod]
    public void AssignReturn_Signed_Unsigned() => this.共通化Assign(ExpressionType.AddAssignChecked);
    [TestMethod]
    public void AssignReturn_Signed() => this.共通化Assign(ExpressionType.AddAssign);
    [TestMethod]
    public void UnaryPlus()
    {
        //if(this.共通UnaryExpression(Unary)) return;
        {
            var expected = new class_演算子オーバーロード(1);
            var p = Expression.Parameter(typeof(class_演算子オーバーロード));
            var r = this.Execute2(
                Expression.Lambda<Func<class_演算子オーバーロード>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(
                            p,
                            Expression.Constant(new class_演算子オーバーロード(1))
                        ),
                        Expression.UnaryPlus(p)
                    )
                )
            );
            Assert.AreEqual(expected, r);
            Assert.AreNotSame(expected, r);
        }
        //this._I.Unbox_Any(Unary.Type);
        this.Execute2(() => (int)Object_Int32);
    }
    [TestMethod]
    public void Unbox()
    {
        this.Execute2(() => (int)Object_Int32);
        var p = Expression.Parameter(typeof(object));
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Convert(
                            Expression.Constant(1),
                            typeof(object)
                        )
                    ),
                    Expression.Unbox(
                        p,
                        typeof(int)
                    )
                )
            )
        );
    }
    [TestMethod]
    public void Void演算Assign0()
    {
        var p = Expression.Parameter(typeof(int));
        this.Execute標準ラムダループ(
            Expression.Lambda<Action>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Constant(0)
                    ),
                    Expression.AddAssign(
                        p,
                        Expression.Constant(1)
                    ),
                    Expression.Default(typeof(void))//重要
                )
            )
        );
    }
    [TestMethod]
    public void Void演算Assign1()
    {
        var p = Expression.Parameter(typeof(int));
        this.Execute標準ラムダループ(
            Expression.Lambda<Action>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Constant(0)
                    ),
                    Expression.AddAssignChecked(
                        p,
                        Expression.Constant(1)
                    ),
                    Expression.Default(typeof(void))//重要
                )
            )
        );
    }
    [TestMethod]
    public void 共通VoidIncrementDecrementAssign()
    {
        const int a = 3;
        var p = Expression.Parameter(typeof(int));
        this.Execute標準ラムダループ(
            Expression.Lambda<Action>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Constant(a)
                    ),
                    Expression.PostIncrementAssign(p),
                    Expression.Default(typeof(void))
                )
            )
        );
    }
    [TestMethod]
    public void VoidTraverse()
    {
        var p = Expression.Parameter(typeof(int));
        //switch(e.NodeType) {
        //    case ExpressionType.Assign: {
        //        if(Binary_Right.NodeType==ExpressionType.Try){
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.TryCatch(
                            Expression.Divide(
                                Expression.Constant(4),
                                Expression.Constant(2)
                            ),
                            Expression.Catch(
                                typeof(DivideByZeroException),
                                Expression.Constant(2)
                            )
                        )
                    ),
                    p
                )
            )
        );
        //        } else{
        this.Execute標準ラムダループ(
            Expression.Lambda<Action>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Constant(0)
                    ),
                    Expression.Default(typeof(void))
                )
            )
        );
        //        }
        //    }
        //    case ExpressionType.PreDecrementAssign:{
        //    case ExpressionType.PreIncrementAssign:{
        this.Execute2(() => Enumerable.Range(1, 2));
        //    }
        //    case ExpressionType.Block: {
        this.Execute標準ラムダループ(
            Expression.Lambda<Action>(
                Expression.IfThen(
                    Expression.Constant(true),
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(
                            p,
                            Expression.Constant(3)
                        ),
                        Expression.Default(typeof(void))
                    )
                )
            )
        );
        //    }
        //    default: {
        //        if(e.Type!=typeof(void)) {
        this.Execute標準ラムダループ(
            Expression.Lambda<Action>(
                Expression.Block(
                    new[] { p },
                    Expression.Add(
                        Expression.Constant(1),
                        Expression.Constant(2)
                    ),
                    p
                )
            )
        );
        //        }
        this.Execute標準ラムダループ(
            Expression.Lambda<Action>(
                Expression.Default(typeof(void))
            )
        );
        //    }
        //}
    }
    [DebuggerDisplay("{value.ToString()}")]
    private struct DBBool
    {
        private readonly int value;
        // Private instance constructor. The value parameter must be –1, 0, or 1.
        public DBBool(int value) => this.value=value;
        public static DBBool operator &(DBBool x, DBBool y) => new DBBool(x.value & y.value);
        // Logical OR operator. Returns True if either operand is True, 
        // Null if either operand is Null, otherwise False.
        public static DBBool operator |(DBBool x, DBBool y) => new DBBool(x.value | y.value);
        // Definitely true operator. Returns true if the operand is True, false
        // otherwise.
        public static bool operator true(DBBool x) => x.value >= 0;
        // Definitely false operator. Returns true if the operand is False, false
        // otherwise.
        public static bool operator false(DBBool x) => x.value < 0;
    }
    [TestMethod]
    public void 共通AndAlsoOrElse()
    {
        //if(Method!=null) {
        {
            struct_ショートカット検証 a = false;
            struct_ショートカット検証 b = true;
            var expected = a && b;
            var actual = this.Execute2(
                Expression.Lambda<Func<struct_ショートカット検証>>(
                    Expression.AndAlso(
                        Expression.Constant(a),
                        Expression.Constant(b)
                    )
                )
            );
            Assert.AreEqual(
                expected,
                actual
            );
        }
        //} else {
        {
            this.Execute2(
                Expression.Lambda<Func<bool>>(
                    Expression.AndAlso(
                        Expression.Constant(true),
                        Expression.Constant(true)
                    )
                )
            );
        }
        //}
    }
    [TestMethod]
    public void 格納先設定()
    {
        //switch(e.NodeType) {
        //    case ExpressionType.Index: {
        {
            var 配列 = new[] { 1, 2, 3 };
            this.Execute標準ラムダループ(
                Expression.Lambda<Action>(
                    Expression.Block(
                        Expression.Assign(
                            Expression.ArrayAccess(
                                Expression.Constant(配列),
                                Expression.Constant(1)
                            ),
                            Expression.Constant(-2)
                        ),
                        Expression.Default(typeof(void))
                    )
                )
            );
            Assert.AreEqual(-2, 配列[1]);
        }
        //    }
        //    case ExpressionType.MemberAccess: {
        {
            this._InstanceString = "ABC";
            var Field = typeof(ATest).GetField(nameof(this._InstanceString), BindingFlags.Instance | BindingFlags.NonPublic);
            this.Execute標準ラムダループ(
                Expression.Lambda<Action>(
                    Expression.Block(
                        Expression.Assign(
                            Expression.Field(
                                Expression.Constant(this),
                                Field
                            ),
                            Expression.Constant("DEF")
                        ),
                        Expression.Default(typeof(void))
                    )
                )
            );
            Assert.AreEqual("DEF", this._InstanceString);
        }
        //    }
        //    default: {
        //        if(index>=0&&Parameter.IsByRef) {
        //            if(this._RootConstant!=null) index++;
        {
            var p = Expression.Parameter(typeof(int), "p");
            var q = Expression.Parameter(typeof(int).MakeByRefType(), "q");
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(
                            p,
                            Expression.Constant(0)
                        ),
                        Expression.Call(
                            typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(out_Lambda1), BindingFlags.NonPublic | BindingFlags.Static),
                            p,
                            Expression.Lambda<outパラメータを引数に取りIntを返す1>(
                                Expression.Assign(
                                    q,
                                    Expression.Convert(
                                        Expression.Constant(1m),
                                        typeof(int)
                                    )
                                ),
                                q
                            )
                        ),
                        p
                    )
                )
            );
        }
        {
            var p = Expression.Parameter(typeof(int), "p");
            var q = Expression.Parameter(typeof(int).MakeByRefType(), "q");
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(p, Expression.Constant(0)),
                        Expression.Call(
                            typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(out_Lambda1), BindingFlags.NonPublic | BindingFlags.Static),
                            p,
                            Expression.Lambda<outパラメータを引数に取りIntを返す1>(
                                Expression.Assign(
                                    q,
                                    Expression.Constant(1)
                                ),
                                q
                            )
                        ),
                        p
                    )
                )
            );
        }
        //        }
        {
            var p = Expression.Parameter(typeof(int), "p");
            var q = Expression.Parameter(typeof(int), "q");
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(p, Expression.Constant(1)),
                        Expression.Call(
                            typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(in_Lambda), BindingFlags.NonPublic | BindingFlags.Static),
                            p,
                            Expression.Lambda<Func<int, int>>(
                                Expression.Assign(q, Expression.Constant(1)), q)
                        ),
                        p
                    )
                )
            );
        }
        //    }
        //}
    }
    [TestMethod]
    public void Void格納先に格納()
    {
        //switch(e.NodeType) {
        //    case ExpressionType.Index: {
        {
            var 配列 = new[] { 1, 2, 3 };
            this.Execute標準ラムダループ(
                Expression.Lambda<Action>(
                    Expression.Block(
                        Expression.Assign(
                            Expression.ArrayAccess(
                                Expression.Constant(配列),
                                Expression.Constant(1)
                            ),
                            Expression.Constant(-2)
                        ),
                        Expression.Default(typeof(void))
                    )
                )
            );
            Assert.AreEqual(-2, 配列[1]);
        }
        //    }
        //    case ExpressionType.MemberAccess: {
        //        if(Member_Member.MemberType==MemberTypes.Field) {
        //            if(Member_Field.IsStatic) {
        {
            _StaticString = "ABC";
            var Field = typeof(ATest).GetField(nameof(_StaticString), BindingFlags.Static | BindingFlags.NonPublic);
            this.Execute標準ラムダループ(
                Expression.Lambda<Action>(
                    Expression.Block(
                        Expression.Assign(
                            Expression.Field(
                                null,
                                Field
                            ),
                            Expression.Constant("DEF")
                        ),
                        Expression.Default(typeof(void))
                    )
                )
            );
            Assert.AreEqual("DEF", _StaticString);
        }
        //            } else {
        {
            this._InstanceString = "ABC";
            var Field = typeof(ATest).GetField(nameof(this._InstanceString), BindingFlags.Instance | BindingFlags.NonPublic);
            this.Execute標準ラムダループ(
                Expression.Lambda<Action>(
                    Expression.Block(
                        Expression.Assign(
                            Expression.Field(
                                Expression.Constant(this),
                                Field
                            ),
                            Expression.Constant("DEF")
                        ),
                        Expression.Default(typeof(void))
                    )
                )
            );
            Assert.AreEqual("DEF", this._InstanceString);
        }
        //            }
        //        } else {
        {
            this.InstanceInt32プロパティ = 3;
            var Property = typeof(ATest).GetProperty(nameof(this.InstanceInt32プロパティ), BindingFlags.Instance | BindingFlags.NonPublic);
            this.Execute標準ラムダループ(
                Expression.Lambda<Action>(
                    Expression.Block(
                        Expression.Assign(
                            Expression.MakeMemberAccess(
                                Expression.Constant(this),
                                Property
                            ),
                            Expression.Constant(4)
                        ),
                        Expression.Default(typeof(void))
                    )
                )
            );
            Assert.AreEqual(4, this.InstanceInt32プロパティ);
        }
        {
            Staticプロパティ = 3;
            var Property = typeof(ATest).GetProperty(nameof(Staticプロパティ), BindingFlags.Static | BindingFlags.NonPublic);
            this.Execute標準ラムダループ(
                Expression.Lambda<Action>(
                    Expression.Block(
                        Expression.Assign(
                            Expression.MakeMemberAccess(
                                null,
                                Property
                            ),
                            Expression.Constant(4)
                        ),
                        Expression.Default(typeof(void))
                    )
                )
            );
            Assert.AreEqual(4, Staticプロパティ);
        }
        //        }
        //    }
        //    case ExpressionType.Parameter: {
        //        if(index>=0) {
        //            if(Parameter.IsByRef) {
        {
            var p = Expression.Parameter(typeof(int), "p");
            var q = Expression.Parameter(typeof(int).MakeByRefType(), "q");
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(p, Expression.Constant(0)),
                        Expression.Call(
                            typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(out_Lambda1), BindingFlags.NonPublic | BindingFlags.Static),
                            p,
                            Expression.Lambda<outパラメータを引数に取りIntを返す1>(
                                Expression.Block(
                                    Expression.Assign(q, Expression.Constant(1)),
                                    q
                                ),
                                q
                            )
                        ),
                        p
                    )
                )
            );
        }
        //            } else {
        //                if(this._RootConstant!=null) index++;
        {
            var p = Expression.Parameter(typeof(int), "p");
            var q = Expression.Parameter(typeof(int), "q");
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(p, Expression.Constant(1)),
                        Expression.Call(
                            typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(in_Lambda), BindingFlags.NonPublic | BindingFlags.Static),
                            p,
                            Expression.Lambda<Func<int, int>>(
                                Expression.Block(
                                    Expression.Assign(
                                        q,
                                        Expression.Convert(
                                            Expression.Constant(1m),
                                            typeof(int)
                                        )
                                    ),
                                    q
                                ), q)
                        ),
                        p
                    )
                )
            );
        }
        {
            var p = Expression.Parameter(typeof(int), "p");
            var q = Expression.Parameter(typeof(int), "q");
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(p, Expression.Constant(1)),
                        Expression.Call(
                            typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(in_Lambda), BindingFlags.NonPublic | BindingFlags.Static),
                            p,
                            Expression.Lambda<Func<int, int>>(
                                Expression.Block(
                                    Expression.Assign(
                                        q,
                                        Expression.Constant(1)
                                    ),
                                    q
                                ), q)
                        ),
                        p
                    )
                )
            );
        }
        //            }
        //        } else {
        {
            var p = Expression.Parameter(typeof(int), "p");
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(
                            p,
                            Expression.Constant(1)
                        )
                    )
                )
            );
        }
        //        }
        //    }
        //    default:
        //}
    }
    [TestMethod]
    public void 演算1()
    {
        //if(this.共通BinaryExpression(特定の形式はなし)) return true;
        {
            var a = 1m;
            var b = 2m;
            this.Execute2(() => a + b);
        }
        {
            var a = 1U;
            var b = 2U;
            this.Execute2(() => a + b);
        }
    }
    [TestMethod]
    public void 演算2()
    {
        //if(this.共通BinaryExpression(特定の形式はなし)) return true;
        {
            var a = 1m;
            var b = 2m;
            this.Execute2(() => a + b);
        }
        //if(IsUnsigned(特定の形式はなし.Left.Type)&&IsUnsigned(特定の形式はなし.Right.Type)) {
        {
            var a = 1U;
            var b = 2U;
            this.Execute2(() => checked(a + b));
        }
        //} else {
        {
            var a = 1;
            var b = 2U;
            this.Execute2(() => checked(a + b));
        }
        {
            var a = 1U;
            var b = 2;
            this.Execute2(() => checked(a + b));
        }
        {
            var a = 1;
            var b = 2;
            this.Execute2(() => checked(a + b));
        }
        //}
    }

    private interface ITest
    {
        string Interface();
    }
    private struct TestStruct : ITest
    {
        public string Interface() => "TestStruct Interface";
        public string Method() => "TestStruct Method";
        public override string ToString() => "TestStruct";
    }
    private abstract class ATestClass : ITest
    {
        public string Interface() => "ATestClass Interface";
        public abstract string Abstract();
        public virtual string Virtual() => "ATestClass virtual Virtual";
        public string New() => "ATestClass New";
        public override string ToString() => "ATestClass";
    }
    private class TestClass : ATestClass
    {
        public override string Abstract() => "TestClass override Abstract";
        public override string Virtual() => "TestClass override Virtual";
        public new string New() => "TestClass new New";
        public override string ToString() => "TestClass";
    }
    private sealed class STestClass : TestClass
    {
        public override string Virtual() => "STestClass override Virtual";
        public new string New() => "STestClass new New";
        public override string ToString() => "STestClass";
    }
    private class TestClass2 : TestClass
    {
        public sealed override string Virtual() => "TestClass2 sealed override Virtual";
        public new string New() => "TestClass2 new New";
        public sealed override string ToString() => "TestClass2";
    }

    private static MethodInfo 取得ChildMethod(Type ChildType, MethodInfo BaseMethod)
    {
        var ChildMethods = ChildType.GetMethods();
        foreach (var ChildMethod in ChildMethods)
        {
            if (ChildMethod.IsFinal && ChildMethod.GetBaseDefinition() == BaseMethod)
            {
                return ChildMethod;
            }
        }
        return null;
    }

    [TestMethod]
    public void BaseMethodからChildMethodを取得()
    {
        var ChildMethod = 取得ChildMethod(
            typeof(TestClass2),
            typeof(ATestClass).GetMethod(nameof(ATestClass.Virtual))
        );
        Assert.AreEqual(typeof(TestClass2).GetMethod(nameof(TestClass2.Virtual)), ChildMethod);
    }

    [TestMethod]
    public void ObjectメソッドEnum()
    {
        {
            this.Execute2(() => ((Enum)EEnum.A).ToString());
            this.Execute2(() => ((Enum)EEnum.A).Equals(EEnum.B));
            this.Execute2(() => ((Enum)EEnum.A).GetType());
        }
        {
            Enum A = EEnum.A;
            this.Execute2(() => A.ToString());
            this.Execute2(() => A.Equals(EEnum.B));
            this.Execute2(() => A.GetType());
        }
    }
    [TestMethod]
    public void ObjectメソッドEEnum()
    {
        {
            this.Execute2(() => EEnum.A.ToString());
            this.Execute2(() => EEnum.A.Equals(EEnum.B));
            this.Execute2(() => EEnum.A.GetType());
        }
        {
            var A = EEnum.A;
            this.Execute2(() => A.ToString());
            this.Execute2(() => A.Equals(EEnum.B));
            this.Execute2(() => A.GetType());
        }
    }
    [TestMethod]
    public void ObjectメソッドInt32()
    {
        var A = 1;
        this.Execute2(() => A.ToString());
        this.Execute2(() => A.Equals(2));
        this.Execute2(() => A.GetType());
    }
    private interface I<T>
    {
        T Method();
    }
    private struct S<T> : I<T>
    {
        public T Method() => default!;
    }
    [TestMethod]
    public void T_ToString()
    {
        var A = new S<int>();
        this.Execute2(() => A.Method());
    }

    [TestMethod]
    public void 共通Method呼び出し0()
    {
        var TestStruct = new TestStruct();
        ITest ITest_TestStruct = TestStruct;
        var TestClass = new TestClass();
        ATestClass ATestClass_TestClass = TestClass;
        var STestClass = new STestClass();
        TestClass TestClass_STestClass = STestClass;
        var TestClass2 = new TestClass2();
        TestClass TestClass_TestClass2 = TestClass2;
        Assert.AreEqual("TestStruct Interface", TestStruct.Interface());
        Assert.AreEqual("TestStruct Method", TestStruct.Method());
        Assert.AreEqual("TestStruct", TestStruct.ToString());
        Assert.AreEqual("TestStruct Interface", ITest_TestStruct.Interface());
        Assert.AreEqual("TestStruct", ITest_TestStruct.ToString());
        Assert.AreEqual("ATestClass Interface", TestClass.Interface());
        Assert.AreEqual("TestClass override Abstract", TestClass.Abstract());
        Assert.AreEqual("TestClass override Virtual", TestClass.Virtual());
        Assert.AreEqual("TestClass new New", TestClass.New());
        Assert.AreEqual("TestClass", TestClass.ToString());
        Assert.AreEqual("ATestClass Interface", ATestClass_TestClass.Interface());
        Assert.AreEqual("TestClass override Abstract", ATestClass_TestClass.Abstract());
        Assert.AreEqual("TestClass override Virtual", ATestClass_TestClass.Virtual());
        Assert.AreEqual("ATestClass New", ATestClass_TestClass.New());
        Assert.AreEqual("TestClass", ATestClass_TestClass.ToString());
        Assert.AreEqual("ATestClass Interface", STestClass.Interface());
        Assert.AreEqual("TestClass override Abstract", STestClass.Abstract());
        Assert.AreEqual("STestClass override Virtual", STestClass.Virtual());
        Assert.AreEqual("STestClass new New", STestClass.New());
        Assert.AreEqual("STestClass", STestClass.ToString());
        Assert.AreEqual("ATestClass Interface", TestClass_STestClass.Interface());
        Assert.AreEqual("TestClass override Abstract", TestClass_STestClass.Abstract());
        Assert.AreEqual("STestClass override Virtual", TestClass_STestClass.Virtual());
        Assert.AreEqual("TestClass new New", TestClass_STestClass.New());
        Assert.AreEqual("STestClass", TestClass_STestClass.ToString());
        Assert.AreEqual("ATestClass Interface", TestClass2.Interface());
        Assert.AreEqual("TestClass override Abstract", TestClass2.Abstract());
        Assert.AreEqual("TestClass2 sealed override Virtual", TestClass2.Virtual());
        Assert.AreEqual("TestClass2 new New", TestClass2.New());
        Assert.AreEqual("TestClass2", TestClass2.ToString());
        Assert.AreEqual("TestClass override Abstract", TestClass_TestClass2.Abstract());
        Assert.AreEqual("TestClass2 sealed override Virtual", TestClass_TestClass2.Virtual());
        Assert.AreEqual("TestClass new New", TestClass_TestClass2.New());
        Assert.AreEqual("TestClass2", TestClass_TestClass2.ToString());
        //if(Reflection.Object.GetType_ == Method)
        this.Execute2(() => 1.GetType());
        //} else if(InstanceType.IsSealed)
        this.Execute2(() => STestClass.Interface());
        this.Execute2(() => STestClass.Virtual());
        this.Execute2(() => STestClass.ToString());
        //else if(Method.IsVirtual)
        this.Execute2(() => TestClass2.Virtual());
        //    if(InstanceType.IsValueType)
        this.Execute2(() => TestStruct.Interface());
        this.Execute2(() => TestStruct.Method());
        this.Execute2(() => TestStruct.ToString());
        //    else
        this.Execute2(() => TestClass2.Virtual());
        this.Execute2(() => TestClass.Interface());
        this.Execute2(() => TestClass.Virtual());
        this.Execute2(() => TestClass.ToString());
        //else
        //if(Reflection.Object.GetType_==Method||Method.IsVirtual) {
        //    if(InstanceType.IsValueType) {
        this.Execute2(() => 1.ToString());
        //    }
        this.Execute2(() => new object().ToString());
        //} else {
        this.Execute2(() => "1".IndexOf("1", StringComparison.Ordinal));
        //}
    }
    private static int Call1RefInt32(ref int a) => a;
    private static string Call1RefString(ref string a) => a;
    private static int Call1(int a) => a;
    private static decimal Call2RefDecimal(ref decimal i0, decimal i1) => i0 + i1;
    private delegate decimal Func1RefDecimal(ref decimal i0);
    [TestMethod]
    public void 共通Method呼び出し1()
    {
        //for(var a = 0;a<Expressions_Count;a++) {
        //    if(Expression.NodeType==ExpressionType.Try) {
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Call(
                    typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(Call1), BindingFlags.Static | BindingFlags.NonPublic),
                    TryCatchExpression(5)
                )
            )
        );
        //    }
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Call(
                    typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(Call1), BindingFlags.Static | BindingFlags.NonPublic),
                    Expression.Constant(5)
                )
            )
        );
        //}
        //if(Method_Parameters[a].ParameterType.IsByRef){
        //    switch(Expression.NodeType) {
        //        case ExpressionType.ArrayIndex: {
        {
            var Array = new string[10];
            this.Execute2(() => Call1RefString(ref Array[0]));
        }
        {
            var Array = new int[10];
            this.Execute2(() => Call1RefInt32(ref Array[0]));
        }
        //        }
        //        case ExpressionType.Parameter: {
        //            if(index>=0) {
        //                if(this.RootConstant!=null) index++;
        {
            var p = Expression.Parameter(typeof(decimal), "p");
            var p1 = Expression.Parameter(typeof(decimal).MakeByRefType(), "p1");
            this.Execute2(
                Expression.Lambda<Func<decimal>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(
                            p,
                            Expression.Constant(4m)
                        ),
                        Expression.Invoke(
                            Expression.Lambda<Func1RefDecimal>(
                                Expression.Call(
                                    typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(Call2RefDecimal), BindingFlags.Static | BindingFlags.NonPublic),
                                    p1,
                                    Expression.Constant(5m)
                                ),
                                p1
                            ),
                            p
                        )
                    )
                )
            );
        }
        //                if(Parameter.IsByRef) {
        {
            var p = Expression.Parameter(typeof(int), "p");
            var p1 = Expression.Parameter(typeof(int).MakeByRefType(), "p1");
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(
                            p,
                            Expression.Constant(4)
                        ),
                        Expression.Invoke(
                            Expression.Lambda<FuncRef>(
                                Expression.Call(
                                    typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(Call1RefInt32), BindingFlags.Static | BindingFlags.NonPublic),
                                    p1
                                ),
                                p1
                            ),
                            p
                        )
                    )
                )
            );
        }
        //                } else {
        {
            var p = Expression.Parameter(typeof(int), "p");
            var p1 = Expression.Parameter(typeof(int), "p1");
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(
                            p,
                            Expression.Constant(4)
                        ),
                        Expression.Invoke(
                            Expression.Lambda<Func<int, int>>(
                                Expression.Call(
                                    typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(Call1RefInt32), BindingFlags.Static | BindingFlags.NonPublic),
                                    p1
                                ),
                                p1
                            ),
                            p
                        )
                    )
                )
            );
        }
        //                }
        //            } else {
        {
            var p = Expression.Parameter(typeof(int), "p");
            this.Execute2(
                Expression.Lambda<Func<int>>(
                    Expression.Block(
                        new[] { p },
                        Expression.Assign(
                            p,
                            Expression.Constant(4)
                        ),
                        Expression.Call(
                            typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(Call1RefInt32), BindingFlags.Static | BindingFlags.NonPublic),
                            p
                        )
                    )
                )
            );
        }
        //            }
        //        }
        //        case ExpressionType.Try: {
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Call(
                    typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(Call1RefInt32), BindingFlags.Static | BindingFlags.NonPublic),
                    TryCatchExpression(5)
                )
            )
        );
        //        }
        //        case ExpressionType.MemberAccess: {
        //            if(Member_Member.MemberType==MemberTypes.Property){
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Call(
                    typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(Call1RefInt32), BindingFlags.NonPublic | BindingFlags.Static),
                    Expression.Property(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        Expression.Constant(new Point(1, 2)),
                        typeof(Point).GetProperty(nameof(Point.X))
                    )
                )
            )
        );
        //            } else{
        //                if(Member_Field.IsStatic){
        this.Execute2(() => Call1RefInt32(ref _StaticInt32));
        //                } else{
        this.Execute2(() => Call1RefInt32(ref this._InstanceInt32));
        //                }
        //            }
        //        }
        //        default: {
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Call(
                    typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(Call1RefInt32), BindingFlags.NonPublic | BindingFlags.Static),
                    Expression.Constant(1)
                )
            )
        );
        //        }
        //    }
        //} else if(Expression.NodeType==ExpressionType.Try){
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Call(
                    typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(Call1), BindingFlags.Static | BindingFlags.NonPublic),
                    TryCatchExpression(5)
                )
            )
        );
        //} else {
        //}
    }
    [TestMethod]
    public void 共通Negate()
    {
        var x = 3m;
        //if(this.共通UnaryExpression(Unary)) return;
        this.Execute2(() => -x);
        //this.I.Emit(Signed);
        this.Execute2(() => -Int32_1);
    }
    [TestMethod]
    public void 実行()
    {
        //for(var a = 自由変数開始番号;a<自由変数終了番号;a++) {
        //    if(Field.FieldType.IsValueType) {
        this.Execute2(() => 1.Let(a => a.Let(b => a + b)));
        //    } else {
        this.Execute2(() => "1".Let(a => a.Let(b => a + b)));
        //    }
        //}
    }
    private void Nullable比較<T>(ExpressionType NodeType,T? a,T? b) where T : struct => this.NullableBinary<T,bool>(NodeType,a,b);
    private void NullableBinary<T>(ExpressionType NodeType,T? a,T? b) where T : struct => this.NullableBinary<T,T?>(NodeType,a,b);
    private void NullableBinary<T, TResult>(ExpressionType NodeType, T? a, T? b) where T : struct
    {
        var Type = typeof(T?);
        var p = Expression.Parameter(Type);
        this.Execute2(
            Expression.Lambda<Func<TResult>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Constant(null, Type)
                    ),
                    Expression.MakeBinary(
                        NodeType,
                        p,
                        Expression.Constant(null, Type)
                    )
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<TResult>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Constant(null, Type)
                    ),
                    Expression.MakeBinary(
                        NodeType,
                        p,
                        Expression.Constant(b, Type)
                    )
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<TResult>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Constant(a, Type)
                    ),
                    Expression.MakeBinary(
                        NodeType,
                        p,
                        Expression.Constant(null, Type)
                    )
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<TResult>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Constant(a, Type)
                    ),
                    Expression.MakeBinary(
                        NodeType,
                        p,
                        Expression.Constant(b, Type)
                    )
                )
            )
        );
    }
    private void NullableUnaryBoolean<T>(ExpressionType NodeType, T? a) where T : struct
    {
        var Type = typeof(T?);
        var p = Expression.Parameter(Type);
        this.Execute2(
            Expression.Lambda<Func<bool?>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Constant(null, Type)
                    ),
                    Expression.MakeUnary(
                        NodeType,
                        p,
                        typeof(bool)
                    )
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<bool?>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Constant(a, Type)
                    ),
                    Expression.MakeUnary(
                        NodeType,
                        p,
                        typeof(bool)
                    )
                )
            )
        );
    }
    private void NullableUnary<T>(ExpressionType NodeType, T? a) where T : struct
    {
        var Type = typeof(T?);
        var p = Expression.Parameter(Type);
        this.Execute2(
            Expression.Lambda<Func<T?>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Constant(null, Type)
                    ),
                    Expression.MakeUnary(
                        NodeType,
                        p,
                        Type
                    )
                )
            )
        );
        this.Execute2(
            Expression.Lambda<Func<T?>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Constant(a, Type)
                    ),
                    Expression.MakeUnary(
                        NodeType,
                        p,
                        Type
                    )
                )
            )
        );
    }
    private void NullableUnaryUnbox<T>(object a) where T : struct
    {
        var Type = typeof(T?);
        this.Execute2(
            Expression.Lambda<Func<T?>>(
                Expression.Unbox(
                    Expression.Constant(a, typeof(object)),
                    Type
                )
            )
        );
    }
    [TestMethod]
    [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
    public void NullableBinary()
    {
        int? Int32a = 10, Int32b = 3;
        double? Doublea = 10, Doubleb = 3;
        bool? Booleana = true, Booleanb = false;
        struct_演算子オーバーロード? struct_演算子オーバーロードa = new struct_演算子オーバーロード { Booleanフィールド = true, Int32フィールド = 1, Int32プロパティ = 2, Stringフィールド = "3" };
        this.NullableBinary(ExpressionType.Add,Int32a,Int32b);
        this.NullableBinary(ExpressionType.AddAssign,Int32a,Int32b);
        this.NullableBinary(ExpressionType.AddAssignChecked,Int32a,Int32b);
        this.NullableBinary(ExpressionType.AddChecked,Int32a,Int32b);
        this.NullableBinary(ExpressionType.And, Int32a, Int32b);
        this.NullableBinary(ExpressionType.AndAlso, Booleana, Booleanb);
        this.NullableBinary(ExpressionType.AndAssign, Int32a, Int32b);
        this.NullableBinary(ExpressionType.Divide, Int32a, Int32b);
        this.NullableBinary(ExpressionType.DivideAssign, Int32a, Int32b);
        this.Nullable比較(ExpressionType.Equal, Int32a, Int32b);
        this.NullableBinary(ExpressionType.ExclusiveOr, Int32a, Int32b);
        this.NullableBinary(ExpressionType.ExclusiveOrAssign, Int32a, Int32b);
        this.Nullable比較(ExpressionType.GreaterThan, Int32a, Int32b);
        this.Nullable比較(ExpressionType.GreaterThanOrEqual, Int32a, Int32b);
        this.NullableBinary(ExpressionType.LeftShift, Int32a, Int32b);
        this.NullableBinary(ExpressionType.LeftShiftAssign, Int32a, Int32b);
        this.Nullable比較(ExpressionType.LessThan, Int32a, Int32b);
        this.Nullable比較(ExpressionType.LessThanOrEqual, Int32a, Int32b);
        this.NullableBinary(ExpressionType.Modulo, Int32a, Int32b);
        this.NullableBinary(ExpressionType.ModuloAssign, Int32a, Int32b);
        this.NullableBinary(ExpressionType.Multiply, Int32a, Int32b);
        this.NullableBinary(ExpressionType.MultiplyAssign, Int32a, Int32b);
        this.NullableBinary(ExpressionType.MultiplyAssignChecked, Int32a, Int32b);
        this.NullableBinary(ExpressionType.MultiplyChecked, Int32a, Int32b);
        this.Nullable比較(ExpressionType.NotEqual, Int32a, Int32b);
        this.NullableBinary(ExpressionType.Or, Int32a, Int32b);
        this.NullableBinary(ExpressionType.Or, Booleanb, Booleanb);
        this.NullableBinary(ExpressionType.OrAssign, Int32b, Int32b);
        this.NullableBinary(ExpressionType.OrAssign, Booleana, Booleanb);

        this.NullableBinary(ExpressionType.Power, Doublea, Doubleb);
        this.NullableBinary(ExpressionType.PowerAssign, Doublea, Doubleb);
        this.NullableBinary(ExpressionType.RightShift, Int32a, Int32b);
        this.NullableBinary(ExpressionType.RightShiftAssign, Int32a, Int32b);
        this.NullableBinary(ExpressionType.Subtract, Int32a, Int32b);
        this.NullableBinary(ExpressionType.SubtractAssign, Int32a, Int32b);
        this.NullableBinary(ExpressionType.SubtractAssignChecked, Int32a, Int32b);
        this.NullableBinary(ExpressionType.SubtractChecked, Int32a, Int32b);

        this.NullableUnary(ExpressionType.Decrement, Int32a);
        this.NullableUnary(ExpressionType.Increment, Int32a);
        this.NullableUnaryBoolean(ExpressionType.IsFalse, struct_演算子オーバーロードa);
        this.NullableUnaryBoolean(ExpressionType.IsTrue, struct_演算子オーバーロードa);
        this.NullableUnary(ExpressionType.Negate, Int32a);
        this.NullableUnary(ExpressionType.NegateChecked, Int32a);
        this.NullableUnary(ExpressionType.Not, struct_演算子オーバーロードa);
        this.NullableUnary(ExpressionType.OnesComplement, Int32a);
        this.NullableUnary(ExpressionType.PostDecrementAssign, Int32a);
        this.NullableUnary(ExpressionType.PostIncrementAssign, Int32a);
        this.NullableUnary(ExpressionType.PreDecrementAssign, Int32a);
        this.NullableUnary(ExpressionType.PreIncrementAssign, Int32a);
        this.NullableUnary(ExpressionType.UnaryPlus, struct_演算子オーバーロードa);
        this.NullableUnaryUnbox<struct_演算子オーバーロード>(struct_演算子オーバーロードa);
    }

    private static bool 演算子<TLeft, TRight>(TLeft Left, TRight Right) => true;
    private void 共通GreaterThan_LessThan<TLeft, TRight>(TLeft Left, TRight Right)
    {
        var Method = typeof(Test_作成_DynamicMethodによるDelegate作成).GetMethod(nameof(演算子), BindingFlags.Static | BindingFlags.NonPublic)!.MakeGenericMethod(typeof(TLeft), typeof(TRight));
        //if(IsUnsigned(Binary.Left.Type)&&IsUnsigned(Binary.Right.Type))
        var p = Expression.Parameter(typeof(TLeft));
        this.Execute2(
            Expression.Lambda<Func<bool>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Constant(Left)
                    ),
                    Expression.GreaterThan(
                        p,
                        Expression.Constant(Right),
                        false,
                        Method
                    )
                )
            )
        );
    }
    [TestMethod]
    public void 共通GreaterThan_LessThan()
    {
        //if(IsUnsigned(Binary.Left.Type)&&IsUnsigned(Binary.Right.Type))
        this.共通GreaterThan_LessThan(1U, 2U);
        //else
        this.共通GreaterThan_LessThan(1, 2U);
        this.共通GreaterThan_LessThan(1U, 2);
        this.共通GreaterThan_LessThan(1, 2);
    }
    [TestMethod, ExpectedException(typeof(Exception))]
    public void Throw() => this.Execute標準ラムダループ(
        Expression.Lambda<Action>(
            Expression.Throw(
                Expression.New(
                    typeof(Exception)
                )
            )
        )
    );
    [TestMethod]
    public void Try_TryCatchFilter_Exception() => this.Execute2(
        Expression.Lambda<Func<int>>(
            Expression.TryCatch(
                Expression.Divide(
                    Expression.Constant(4),
                    Expression.Constant(2)
                ),
                Expression.Catch(
                    typeof(DivideByZeroException),
                    Expression.Constant(2),
                    Expression.Constant(true)
                )
            )
        )
    );
    //[TestMethod, ExpectedException(typeof(NotSupportedException))]
    //public void Try_Fault() => this.Execute2(
    //    Expression.Lambda<Func<int>>(
    //        Expression.TryFault(
    //            Expression.Constant(0),
    //            Expression.Constant(1)
    //        )
    //    )
    //);

    public static void 例外テスト()
    {
        try
        {
            throw new Exception("A");
        }
#pragma warning disable CS7095 // フィルター式は定数 'true' です
        catch(Exception) when (true)
#pragma warning restore CS7095 // フィルター式は定数 'true' です
        {
            Console.Write("catch(Exception) when(true)");
        }
        finally
        {
            Console.Write("finally");
        }
    }
    [TestMethod]public void TryAdd()
    {
        //    if(!this.PrivateEquals(a_Handler.Filter,b_Handler.Filter)) return false;
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Add(
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(0),
                            Expression.Constant(true)
                        )
                    ),
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(0),
                            Expression.Constant(false)
                        )
                    )
                )
            )
        );
    }
    [TestMethod]public void TryCatchFilter無変数()
    {
        //    if(!this.PrivateEquals(a_Handler.Filter,b_Handler.Filter)) return false;
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.TryCatch(
                    Expression.Constant(0),
                    Expression.Catch(
                        typeof(Exception),
                        Expression.Constant(0),
                        Expression.Constant(false)
                    )
                )
            )
        );
    }
    [TestMethod]public void TryCatchFilter有変数()
    {
        //    if(a_Handler.Test!=b_Handler.Test) return false;
        //}
        var ex = Expression.Parameter(typeof(Exception));
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.TryCatch(
                    Expression.Throw(
                        Expression.New(
                            typeof(Exception)
                        ),
                        typeof(int)
                    ),
                    Expression.Catch(
                        ex,
                        Expression.Constant(0),
                        Expression.NotEqual(
                            ex,
                            Expression.Default(typeof(Exception))
                        )
                    )
                )
            )
        );
    }
    [TestMethod]public void TryCatchFilter無変数CatchFilter有変数()
    {
        //    if(a_Handler.Test!=b_Handler.Test) return false;
        //}
        var ex = Expression.Parameter(typeof(Exception));
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.TryCatch(
                    Expression.Throw(
                        Expression.New(
                            typeof(Exception)
                        ),
                        typeof(int)
                    ),
                    Expression.Catch(
                        typeof(Exception),
                        Expression.Constant(0),
                        Expression.Constant(true)
                    ),
                    Expression.Catch(
                        ex,
                        Expression.Constant(0),
                        Expression.Equal(
                            ex,
                            Expression.Default(typeof(Exception))
                        )
                    )
                )
            )
        );
    }
    [TestMethod]public void TryCatchFilter有変数CatchFilter無変数()
    {
        //    if(a_Handler.Test!=b_Handler.Test) return false;
        //}
        var ex = Expression.Parameter(typeof(Exception));
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.TryCatch(
                    Expression.Throw(
                        Expression.New(
                            typeof(Exception)
                        ),
                        typeof(int)
                    ),
                    Expression.Catch(
                        ex,
                        Expression.Constant(0),
                        Expression.Equal(
                            ex,
                            Expression.Default(typeof(Exception))
                        )
                    ),
                    Expression.Catch(
                        typeof(Exception),
                        Expression.Constant(0),
                        Expression.Constant(true)
                    )
                )
            )
        );
    }
    [TestMethod]public void TryCatchFilter有変数CatchFilter有変数()
    {
        //    if(a_Handler.Test!=b_Handler.Test) return false;
        //}
        var ex = Expression.Parameter(typeof(Exception));
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.TryCatch(
                    Expression.Throw(
                        Expression.New(
                            typeof(Exception)
                        ),
                        typeof(int)
                    ),
                    Expression.Catch(
                        ex,
                        Expression.Constant(0),
                        Expression.Equal(
                            ex,
                            Expression.Default(typeof(Exception))
                        )
                    ),
                    Expression.Catch(
                        ex,
                        Expression.Constant(0),
                        Expression.NotEqual(
                            ex,
                            Expression.Default(typeof(Exception))
                        )
                    )
                )
            )
        );
    }
}