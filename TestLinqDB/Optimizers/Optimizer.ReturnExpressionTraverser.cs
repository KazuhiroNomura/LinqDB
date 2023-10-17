﻿
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
//using System.Reflection;
using LinqDB.Helpers;
using LinqDB.Optimizers;
using LinqDB.Sets;
using MessagePack.Formatters;
using Microsoft.CSharp.RuntimeBinder;
using static LinqDB.Optimizers.Optimizer;
//using Binder=System.Reflection.Binder;
using Binder=Microsoft.CSharp.RuntimeBinder.Binder;
using Expression = System.Linq.Expressions.Expression;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
namespace TestLinqDB.Optimizers;
public class ReturnExpressionTraverser:共通{
    private class ggg{
        public ggg(Optimizer.作業配列 xxx){

        }
    }
    //private static readonly Type ReturnExpressionTraverser=typeof(LinqDB.Optimizers.Optimizer).GetNestedType("ReturnExpressionTraverser",System.Reflection.BindingFlags.NonPublic)!;
    //private static readonly ConstructorInfo ReturnExpressionTraverser_ctor=ReturnExpressionTraverser!.GetConstructor(BindingFlags.NonPublic|BindingFlags.Instance,new[]{typeof(Optimizer.作業配列)})!;

    private static readonly dynamic o=new NonPublicAccessor(typeof(LinqDB.Optimizers.Optimizer).GetNestedType("ReturnExpressionTraverser",System.Reflection.BindingFlags.NonPublic)!.GetConstructor(System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance,new[]{typeof(作業配列)})!.Invoke(new object[]{new 作業配列()}));
    //private static dynamic o{
    //    get{
    //        var ReturnExpressionTraverser=typeof(LinqDB.Optimizers.Optimizer).GetNestedType("ReturnExpressionTraverser",System.Reflection.BindingFlags.NonPublic);
    //        var 作業配列=new Optimizer.作業配列();
    //        var ctor=ReturnExpressionTraverser!.GetConstructor(BindingFlags.NonPublic|BindingFlags.Instance,new[]{typeof(Optimizer.作業配列)});
    //        var x=ctor!.Invoke(new object[]{作業配列});
    //        return new NonPublicAccessor(x);
    //    }
    //}
    [Fact]public void TraverseNullable(){
        o.TraverseNullable(null);
        o.TraverseNullable(Expression.Default(typeof(void)));
    }
    [Fact]public void Traverse(){
        var CSharpArgumentInfo1=CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null);
        var CSharpArgumentInfoArray1=new[]{CSharpArgumentInfo1};
        var CSharpArgumentInfoArray2=new[]{CSharpArgumentInfo1,CSharpArgumentInfo1};
        var p_int=Expression.Parameter(typeof(int));
        var p_double=Expression.Parameter(typeof(double));
        var p_List=Expression.Parameter(typeof(List<int>));
        var @int=Expression.Constant(1);
        var @double=Expression.Constant(1d);
        var @bool=Expression.Constant(true);
        var @array=Expression.Constant(new int[3]);
        var @string=Expression.Constant("abc");
        o.TraverseNullable(Expression.Add                  (@int,@int));
        o.TraverseNullable(Expression.AddAssign            (p_int,@int));
        o.TraverseNullable(Expression.AddAssignChecked     (p_int,@int));
        o.TraverseNullable(Expression.AddChecked           (@int,@int));
        o.TraverseNullable(Expression.And                  (@int,@int));
        o.TraverseNullable(Expression.AndAssign            (p_int,@int));
        o.TraverseNullable(Expression.AndAlso              (@bool,@bool));
        o.TraverseNullable(Expression.ArrayIndex           (@array,@int));
        o.TraverseNullable(Expression.Assign               (p_int,@int));
        o.TraverseNullable(Expression.Coalesce             (@string,@string));
        o.TraverseNullable(Expression.Divide               (@int,@int));
        o.TraverseNullable(Expression.DivideAssign         (p_int,@int));
        o.TraverseNullable(Expression.Equal                (@int,@int));
        o.TraverseNullable(Expression.ExclusiveOr          (@int,@int));
        o.TraverseNullable(Expression.ExclusiveOrAssign    (p_int,@int));
        o.TraverseNullable(Expression.GreaterThan          (@int,@int));
        o.TraverseNullable(Expression.GreaterThanOrEqual   (p_int,@int));
        o.TraverseNullable(Expression.LeftShift            (@int,@int));
        o.TraverseNullable(Expression.LeftShiftAssign      (p_int,@int));
        o.TraverseNullable(Expression.LessThan             (@int,@int));
        o.TraverseNullable(Expression.LessThanOrEqual      (@int,@int));
        o.TraverseNullable(Expression.Modulo               (@int,@int));
        o.TraverseNullable(Expression.ModuloAssign         (p_int,@int));
        o.TraverseNullable(Expression.Multiply             (@int,@int));
        o.TraverseNullable(Expression.MultiplyAssign       (p_int,@int));
        o.TraverseNullable(Expression.MultiplyAssignChecked(p_int,@int));
        o.TraverseNullable(Expression.MultiplyChecked      (@int,@int));
        o.TraverseNullable(Expression.NotEqual             (@int,@int));
        o.TraverseNullable(Expression.Or                   (@int,@int));
        o.TraverseNullable(Expression.OrAssign             (p_int,@int));
        o.TraverseNullable(Expression.OrElse               (@bool,@bool));
        o.TraverseNullable(Expression.Power                (@double,@double));
        o.TraverseNullable(Expression.PowerAssign          (p_double,@double));
        o.TraverseNullable(Expression.RightShift           (@int,@int));
        o.TraverseNullable(Expression.RightShiftAssign     (p_int,@int));
        o.TraverseNullable(Expression.Subtract             (@int,@int));
        o.TraverseNullable(Expression.SubtractAssign       (p_int,@int));
        o.TraverseNullable(Expression.SubtractAssignChecked(p_int,@int));
        o.TraverseNullable(Expression.SubtractChecked      (@int,@int));
        o.TraverseNullable(Expression.Block                (@int,@int));
        o.TraverseNullable(Expression.Condition            (@bool,@int,@int));
        o.TraverseNullable(Expression.Constant             (0));
        o.TraverseNullable(Expression.DebugInfo            (Expression.SymbolDocument("abc"),1,1,1,1));
        o.TraverseNullable(Expression.Default              (typeof(void)));
        o.TraverseNullable(
            Expression.Dynamic(
                Binder.SetMember(
                    CSharpBinderFlags.ResultIndexed,
                    nameof(TestDynamic<int>.メンバー1),
                    typeof(ExpressionEqualityComparer),
                    CSharpArgumentInfoArray2
                ),
                typeof(object),
                Expression.Constant(new TestDynamic<int>(1,2)),
                Expression.Constant(2)
            )
        );
        o.TraverseNullable(Expression.Goto                 (Expression.Label()));
        o.TraverseNullable(Expression.MakeIndex            (p_List,typeof(List<int>).GetProperty("Item"),new[]{@int}));
        o.TraverseNullable(Expression.Invoke(Expression.Lambda(p_int, p_int),@int));
        o.TraverseNullable(Expression.Label                (Expression.Label()));
        o.TraverseNullable(Expression.Lambda       <Action>(@int));
        var type=typeof(BindCollection);
        var Int32フィールド1=type.GetField(nameof(BindCollection.Int32フィールド1))!;
        var ctor=type.GetConstructor(new[]{typeof(int)})!;
        var New=Expression.New(ctor,@int);
        o.TraverseNullable(Expression.ListInit(Expression.New(typeof(List<int>)),Expression.ElementInit(typeof(List<int>).GetMethod("Add")!,Expression.Constant(1))));
        o.TraverseNullable(Expression.Loop                 (Expression.Default(typeof(void))));
        o.TraverseNullable(Expression.MakeMemberAccess     (Expression.Constant(new Point(0,0)),typeof(Point).GetProperty(nameof(Point.X))!));
        o.TraverseNullable(Expression.MemberInit           (New,Expression.Bind(Int32フィールド1,@int)));
        var ToString=typeof(string).GetMethod("ToString",Array.Empty<System.Type>())!;
        o.TraverseNullable(Expression.Call(@string,ToString));
        o.TraverseNullable(Expression.NewArrayBounds       (typeof(int),@int));
        o.TraverseNullable(Expression.NewArrayInit         (typeof(int),@int));
        o.TraverseNullable(Expression.New                  (typeof(List<int>).GetConstructor(Type.EmptyTypes)!));
        o.TraverseNullable(p_int);
        o.TraverseNullable(Expression.RuntimeVariables     (p_int,p_double));
        o.TraverseNullable(Expression.Switch               (@int,@int));
        o.TraverseNullable(Expression.TryFinally           (@int,@int));
        o.TraverseNullable(Expression.TypeEqual            (@int,typeof(int)));
        o.TraverseNullable(Expression.TypeIs               (@int,typeof(int)));
        o.TraverseNullable(Expression.ArrayLength          (@array));
        o.TraverseNullable(Expression.Convert              (@int,typeof(int)));
        o.TraverseNullable(Expression.ConvertChecked       (@int,typeof(int)));
        o.TraverseNullable(Expression.Decrement            (@int));
        o.TraverseNullable(Expression.Increment            (@int));
        o.TraverseNullable(Expression.IsFalse              (@bool));
        o.TraverseNullable(Expression.IsTrue               (@bool));
        o.TraverseNullable(Expression.Negate               (@int));
        o.TraverseNullable(Expression.NegateChecked        (@int));
        o.TraverseNullable(Expression.Not                  (@int));
        o.TraverseNullable(Expression.OnesComplement       (@int));
        o.TraverseNullable(Expression.PostDecrementAssign  (p_int));
        o.TraverseNullable(Expression.PostIncrementAssign  (p_int));
        o.TraverseNullable(Expression.PreDecrementAssign   (p_int));
        o.TraverseNullable(Expression.PreIncrementAssign   (p_int));
        o.TraverseNullable(Expression.Quote                (Expression.Lambda<Action>(@int)));
        o.TraverseNullable(Expression.Throw                (Expression.Constant(new NotImplementedException())));
        o.TraverseNullable(Expression.TypeAs               (@int,typeof(object)));
        o.TraverseNullable(Expression.UnaryPlus            (@int));
        o.TraverseNullable(Expression.Unbox                (Expression.Constant(1,typeof(object)),typeof(int)));
    }
    [Fact]public void TraverseExpressions(){
        var p_int=Expression.Parameter(typeof(int));
        var p_double=Expression.Parameter(typeof(double));
        var @int=Expression.Constant(1);
        var @double=Expression.Constant(1d);
        var @bool=Expression.Constant(true);
        var @array=Expression.Constant(new int[3]);
        var @string=Expression.Constant("abc");
        o.TraverseNullable(Expression.Add(@int,@int));
    }
    [Fact]public void Bindings(){
        //for(var a=0; a < Bindings0_Count; a++) {
        //    switch (Binding0.BindingType) {
        //        case MemberBindingType.Assignment: {
        //            if(Binding0_Expression==Binding1_Expression) {
        //            } else {
        //            }
        //        }
        //        case MemberBindingType.MemberBinding: {
        //            if(ReferenceEquals(Binding0_Bindings,Binding1_Bindings)) {
        //            } else {
        //            }
        //        }
        //        case MemberBindingType.ListBinding: {
        //            for(var b=0; b < MemberListBinding0_Initializers_Count; b++) {
        //                if(ReferenceEquals(MemberListBinding0_Initializer_Arguments,MemberListBinding1_Initializer_Arguments)) {
        //                } else {
        //                }
        //            }
        //            if(変化したか1) {
        //            } else
        //        }
        //        default:throw new NotSupportedException($"{Binding0.BindingType}はサポートされていない");
        //    }
        //}
    }
    [Fact]public void Block(){
    }
    [Fact]public void Call(){
    }
    [Fact]public void Conditional(){
    }
    [Fact]public void Dynamic(){
    }
    [Fact]public void ExpressionEqual(){
    }
    [Fact]public void Goto(){
    }
    [Fact]public void Index(){
    }
    [Fact]public void Invoke(){
    }
    [Fact]public void Label(){
    }
    [Fact]public void Lambda(){
    }
    [Fact]public void ListInit(){
        //for(var a=0; a < ListInit1_Initializers_Count; a++) {
        //    if(ReferenceEquals(ListInit0_Initialize_Arguments,ListInit1_Initialize_Arguments)) {
        this.共通コンパイル実行(()=>new List<int>{1,2,3});
        //    } else {
        var a=0;
        this.共通コンパイル実行(()=>new List<int>{a,a});
        //    }
        //}
    }
    [Fact]public void Loop(){
        //this.MemoryMessageJson_Assert(new{a=default(LoopExpression)});
        var Label_decimal = Expression.Label(typeof(decimal), "Label_decimal");
        var Label_void = Expression.Label("Label");
        this.共通コンパイル実行(
            Expression.Lambda<Action>(
                Expression.Loop(
                    Expression.Block(
                        Expression.Add(Expression.Constant(1m), Expression.Constant(1m)),
                        Expression.Break(Label_decimal, Expression.Constant(1m)),
                        Expression.Continue(Label_void)
                    ),
                    Label_decimal,
                    Label_void
                )
            )
        );
    }
    [Fact]public void MakeAssign(){
    }
    [Fact]public void MakeBinary(){
    }
    [Fact]public void MakeUnary(){
    }
    [Fact]public void MamberAccess(){
        //if(Member0_Expression==Member1_Expression)
        this.共通コンパイル実行(()=>new Point(0,1).X);
        var a=3;
        this.共通コンパイル実行(()=>new Point(a,a).X);
    }
    [Fact]public void MemberInit(){
        //if(MemberInit0_NewExpression==MemberInit1_NewExpression && ReferenceEquals(MemberInit0_Bindings,MemberInit1_Bindings))
        //this.共通コンパイル実行(()=>new class_演算子オーバーロード{Int32フィールド=3,Stringフィールド = 3});
        var a=3;
        this.共通コンパイル実行(()=>new class_演算子オーバーロード{Int32フィールド=a,Stringフィールド = a.ToString()});
    }
    [Fact]public void New(){
        //if(New0.Constructor is null)
        this.共通コンパイル実行(
            Expression.Lambda<Func<int>>(
                Expression.New(typeof(int))
            )
        );
        //if(ReferenceEquals(New1_Arguments,New0_Arguments))
        this.共通コンパイル実行(()=>new decimal(1));
        //    ?Expression.New(New0.Constructor,New1_Arguments)
        var a=1;
        this.共通コンパイル実行(()=>new decimal(a,a,a,true,1));
        //    :Expression.New(New0.Constructor,New1_Arguments,New0.Members);
        //this.共通コンパイル実行(()=>new Point(1,2));
        //this.共通コンパイル実行(
        //    Expression.Lambda<Func<Point>>(
        //        Expression.New(
        //            typeof(Point).GetConstructors()[0],
        //            new[]{Expression.Constant(1),Expression.Constant(2)},
        //            new MemberInfo[]{typeof(Point).GetProperty(nameof(Point.Y))!,typeof(Point).GetProperty(nameof(Point.X))!}
        //        )
        //    )
        //);
        this.共通コンパイル実行(
            Expression.Lambda<Func<Point>>(
                Expression.New(
                    typeof(Point).GetConstructors()[0],
                    new[]{Expression.Constant(1),Expression.Constant(2)},
                    new MemberInfo[]{typeof(Point).GetProperty(nameof(Point.X))!,typeof(Point).GetProperty(nameof(Point.Y))!}
                )
            )
        );
    }
    [Fact]public void NewArrayBound(){
        //if(ReferenceEquals(NewArray1_Expressions,NewArray0_Expressions))
        this.共通コンパイル実行(()=>new int[2,3]);
        var a=3;
        this.共通コンパイル実行(()=>new int[a,a]);
    }
    [Fact]public void NewArrayInit(){
        //if(ReferenceEquals(NewArray1_Expressions,NewArray0_Expressions))
        this.共通コンパイル実行(()=>new int[]{1});
        var a=3;
        this.共通コンパイル実行(()=>new int[]{a,a});
    }
    [Fact]public void Switch(){
        var p=Expression.Parameter(typeof(int));
        //for(var a=0; a < Switch0_Cases_Count; a++) {
        //    for(var b=0; b < Switch0_Case_TestValues_Count; b++) {
        //        if(Switch0_Case_TestValue !=Switch1_Case_TestValue)
        this.共通コンパイル実行(
            Expression.Lambda<Func<int,int>>(
                Expression.Switch(
                    Expression.Add(Expression.Add(p,p),Expression.Add(p,p)),
                    Expression.Constant(-1),
                    Expression.SwitchCase(
                        Expression.Constant(10),
                        Expression.Add(Expression.Add(p,p),Expression.Add(p,p)),
                        Expression.Add(p,p),p
                    )
                ),
                p
            ),
            0
        );
        //    }
        //    if(Switch0_Case_Body !=Switch1_Case_Body || 変化したか1) {
        //    } else
        //}
        //return 変化したか?
        this.共通コンパイル実行(
            Expression.Lambda<Func<int,int>>(
                Expression.Switch(
                    p,
                    Expression.Constant(-1),
                    Expression.SwitchCase(
                        Expression.Constant(0),
                        Expression.Constant(1)
                    )
                ),
                p
            ),
            0
        );
    }
    [Fact]public void Try(){
    }
    [Fact]public void TypeAs(){
    }
    [Fact]public void TypeEqual(){
    }
    [Fact]public void TypeIs(){
    }
}