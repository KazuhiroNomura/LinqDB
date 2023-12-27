using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.CompilerServices;
using LinqDB.Helpers;
using LinqDB.Optimizers;
using Microsoft.CSharp.RuntimeBinder;
using System.Linq.Expressions;
using System.Reflection;
using TestLinqDB.Optimizers.Comparer;
using Binder=Microsoft.CSharp.RuntimeBinder.Binder;
using ExpressionEqualityComparer=LinqDB.Optimizers.Comparer.ExpressionEqualityComparer;
using System.Diagnostics;

namespace TestLinqDB.Optimizers.ReturnExpressionTraverser;
[SuppressMessage("ReSharper","InconsistentNaming")]
public class ReturnExpressionTraverser:共通{
    //private readonly dynamic  Traverser= new NonPublicAccessor(typeof(LinqDB.Optimizers.ReturnExpressionTraverser.ReturnExpressionTraverser).GetConstructor(System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance, new[] { typeof(作業配列) })!.Invoke(new object[] { new 作業配列() }));
    //private readonly dynamic o =new LinqDB.Optimizers.ReturnExpressionTraverser.変換_局所Parameterの先行評価();
    //private static readonly dynamic o = new NonPublicAccessor(typeof(LinqDB.Optimizers.ReturnExpressionTraverser.変換_局所Parameterの先行評価).GetConstructor(BindingFlags.NonPublic|BindingFlags.Instance, new[] { typeof(作業配列) })!.Invoke(new object[] { new 作業配列() }));
    private readonly dynamic 変換_旧Parameterを新Expression1=new NonPublicAccessor(
        typeof(LinqDB.Optimizers.ReturnExpressionTraverser.変換_旧Parameterを新Expression1).GetConstructors()[0]!.Invoke(new[]{new 作業配列()}));
    //private readonly dynamic 変換_局所Parameterの先行評価 = new NonPublicAccessor(
    //    typeof(LinqDB.Optimizers.ReturnExpressionTraverser.変換_局所Parameterの先行評価).GetConstructor(Type.EmptyTypes)!.Invoke(Array.Empty<object>()));
    ///// <summary>
    ///// 変換_局所Parameterの先行評価.実行
    ///// </summary>
    ///// <param name="Body"></param>
    //private void 変換_局所Parameterの先行評価_実行(Expression Body){
    //    var Lambda = Expression.Lambda(Body);
    //    this.変換_局所Parameterの先行評価.実行(Lambda);
    //    //var Lambda = Expression.Lambda(Expression0);
    //    //this.Lambda最適化(Lambda );
    //}
    static ReturnExpressionTraverser(){
    }
    //private static readonly LinqDB.Optimizers.ReturnExpressionTraverser o=new LinqDB.Optimizers.ReturnExpressionTraverser.変換_局所Parameterの先行評価(new 作業配列());
    private static readonly CSharpArgumentInfo CSharpArgumentInfo1=CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null);
    private static readonly CSharpArgumentInfo[] CSharpArgumentInfoArray1=new[]{CSharpArgumentInfo1};
    private static readonly CSharpArgumentInfo[] CSharpArgumentInfoArray2=new[]{CSharpArgumentInfo1,CSharpArgumentInfo1};
    private static readonly CSharpArgumentInfo[] CSharpArgumentInfoArray3={CSharpArgumentInfo1,CSharpArgumentInfo1,CSharpArgumentInfo1};
    private static readonly CSharpArgumentInfo[] CSharpArgumentInfoArray4={CSharpArgumentInfo1,CSharpArgumentInfo1,CSharpArgumentInfo1,CSharpArgumentInfo1};
    private static readonly ParameterExpression p_int=Expression.Parameter(typeof(int));
    private static readonly ParameterExpression p_double=Expression.Parameter(typeof(double));
    private static readonly ParameterExpression p_decimal=Expression.Parameter(typeof(decimal));
    private static readonly Expression p_List=Expression.Parameter(typeof(List<int>));
    private static readonly Expression @int=Expression.Constant(1);
    private static readonly Expression @double=Expression.Constant(1d);
    private static readonly Expression @bool=Expression.Constant(true);
    private static readonly Expression @array=Expression.Constant(new int[3]);
    private static readonly Expression @string=Expression.Constant("abc");
    private static readonly Type type=typeof(BindCollection);
    private static readonly FieldInfo Int32フィールド1=type.GetField(nameof(BindCollection.Int32フィールド1))!;
    private static readonly ConstructorInfo ctor=type.GetConstructor(new[]{typeof(int)})!;
    private static readonly NewExpression _New=Expression.New(ctor,@int);
    private static readonly MethodInfo ToString=typeof(string).GetMethod("ToString",Array.Empty<Type>())!;
    //private static readonly dynamic o=new NonPublicAccessor(new LinqDB.Optimizers.ReturnExpressionTraverser.ReturnExpressionTraverser(new 作業配列()));
    [Fact]
    public void TraverseNullable(){
        this.変換_局所Parameterの先行評価.TraverseNullable(null);
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Default(typeof(void)));
    }
    [Fact]
    public void Traverse(){
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Add(@int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.AddAssign(p_int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.AddAssignChecked(p_int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.AddChecked(@int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.And(@int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.AndAssign(p_int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.AndAlso(@bool,@bool));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.ArrayIndex(@array,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Assign(p_int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Coalesce(@string,@string));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Divide(@int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.DivideAssign(p_int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Equal(@int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.ExclusiveOr(@int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.ExclusiveOrAssign(p_int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.GreaterThan(@int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.GreaterThanOrEqual(p_int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.LeftShift(@int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.LeftShiftAssign(p_int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.LessThan(@int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.LessThanOrEqual(@int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Modulo(@int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.ModuloAssign(p_int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Multiply(@int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.MultiplyAssign(p_int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.MultiplyAssignChecked(p_int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.MultiplyChecked(@int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.NotEqual(@int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Or(@int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.OrAssign(p_int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.OrElse(@bool,@bool));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Power(@double,@double));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.PowerAssign(p_double,@double));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.RightShift(@int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.RightShiftAssign(p_int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Subtract(@int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.SubtractAssign(p_int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.SubtractAssignChecked(p_int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.SubtractChecked(@int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Block(@int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Condition(@bool,@int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Constant(0));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.DebugInfo(Expression.SymbolDocument("abc"),1,1,1,1));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Default(typeof(void)));
        this.変換_局所Parameterの先行評価.TraverseNullable(
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
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Goto(Expression.Label()));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.MakeIndex(p_List,typeof(List<int>).GetProperty("Item"),new[]{@int}));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Invoke(Expression.Lambda(p_int,p_int),@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Label(Expression.Label()));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Lambda<Action>(@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.ListInit(Expression.New(typeof(List<int>)),Expression.ElementInit(typeof(List<int>).GetMethod("Add")!,Expression.Constant(1))));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Loop(Expression.Default(typeof(void))));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.MakeMemberAccess(Expression.Constant(new Point(0,0)),typeof(Point).GetProperty(nameof(Point.X))!));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.MemberInit(_New,Expression.Bind(Int32フィールド1,@int)));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Call(@string,ToString));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.NewArrayBounds(typeof(int),@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.NewArrayInit(typeof(int),@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.New(typeof(List<int>).GetConstructor(Type.EmptyTypes)!));
        this.変換_局所Parameterの先行評価.TraverseNullable(p_int);
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.RuntimeVariables(p_int,p_double));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Switch(@int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.TryFinally(@int,@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.TypeEqual(@int,typeof(int)));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.TypeIs(@int,typeof(int)));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.ArrayLength(@array));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Convert(@int,typeof(int)));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.ConvertChecked(@int,typeof(int)));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Decrement(@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Increment(@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.IsFalse(@bool));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.IsTrue(@bool));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Negate(@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.NegateChecked(@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Not(@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.OnesComplement(@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.PostDecrementAssign(p_int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.PostIncrementAssign(p_int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.PreDecrementAssign(p_int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.PreIncrementAssign(p_int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Quote(Expression.Lambda<Action>(@int)));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Throw(Expression.Constant(new NotImplementedException())));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.TypeAs(@int,typeof(object)));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.UnaryPlus(@int));
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Unbox(Expression.Constant(1,typeof(object)),typeof(int)));
    }
    [Fact]
    public void TraverseExpressions(){
        Expression.Parameter(typeof(int));
        Expression.Parameter(typeof(double));
        var @int=Expression.Constant(1);
        this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Add(@int,@int));
    }
    [Fact]
    public void AndAlso(){
        //if(Binary0_Left==Binary1_Left)
        //    if(Binary0_Right==Binary1_Right)
        //        if(Binary0_Conversion==Binary1_Conversion)
        var Equal=Expression.Equal(
            Expression.Constant(0m),
            Expression.Constant(0m)
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.AndAlso(
                Equal,
                Equal
            )
        );
    }
    [Fact]
    public void Bindings(){
        var Type=typeof(BindCollection);
        var Int32フィールド1=Type.GetField(nameof(BindCollection.Int32フィールド1))!;
        var Int32フィールド2=Type.GetField(nameof(BindCollection.Int32フィールド2))!;
        var BindCollectionフィールド2=Type.GetField(nameof(BindCollection.BindCollectionフィールド2))!;
        var Listフィールド2=Type.GetField(nameof(BindCollection.Listフィールド2))!;
        var Constant_1=Expression.Constant(1);
        var ctor=Type.GetConstructor(new[]{typeof(int)})!;
        var New=Expression.New(
            ctor,
            Constant_1
        );
        var p=Expression.Parameter(typeof(int));
        var pp=Expression.Add(p,p);
        pp=Expression.Add(pp,pp);
        var Add=typeof(List<int>).GetMethod("Add")!;
        //for(var a=0; a < Bindings0_Count; a++) {
        //    switch (Binding0.BindingType) {
        //        case MemberBindingType.Assignment: {
        //            if(Binding0_Expression==Binding1_Expression) {
        this.変換_局所Parameterの先行評価_実行(
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド2,
                    Constant_1
                )
            )
        );
        //            } else {
        this.変換_局所Parameterの先行評価_実行(
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド2,
                    pp
                )
            )
        );
        //            }
        //        }
        //        case MemberBindingType.MemberBinding: {
        //            if(ReferenceEquals(Binding0_Bindings,Binding1_Bindings)) {
        this.変換_局所Parameterの先行評価_実行(
            Expression.MemberInit(
                New,
                Expression.MemberBind(
                    BindCollectionフィールド2,
                    Expression.Bind(
                        Int32フィールド1,
                        Constant_1
                    )
                )
            )
        );
        //            } else {
        this.変換_局所Parameterの先行評価_実行(
            Expression.MemberInit(
                New,
                Expression.MemberBind(
                    BindCollectionフィールド2,
                    Expression.Bind(
                        Int32フィールド1,
                        pp
                    )
                )
            )
        );
        //            }
        //        }
        //        default: {
        //            for(var b=0; b < MemberListBinding0_Initializers_Count; b++) {
        //                if(ReferenceEquals(MemberListBinding0_Initializer_Arguments,MemberListBinding1_Initializer_Arguments)) {
        this.変換_局所Parameterの先行評価_実行(
            Expression.MemberInit(
                New,
                Expression.ListBind(
                    Listフィールド2,
                    Expression.ElementInit(
                        Add,
                        Constant_1
                    )
                )
            )
        );
        //                } else {
        this.変換_局所Parameterの先行評価_実行(
            Expression.MemberInit(
                New,
                Expression.ListBind(
                    Listフィールド2,
                    Expression.ElementInit(
                        Add,
                        pp
                    )
                )
            )
        );
        //                }
        //            }
        //        }
        //    }
        //}
        ////for(var a=0; a < Bindings0_Count; a++) {
        ////    switch (Binding0.BindingType) {
        ////        case MemberBindingType.Assignment: {
        ////            if(Binding0_Expression==Binding1_Expression) {
        //this.Expression実行AssertEqual(() => new class_演算子オーバーロード { Int32フィールド=3 });
        ////            } else {
        //var a = 3;
        //this.Expression実行AssertEqual(() => new class_演算子オーバーロード { Int32フィールド=a*a });
        ////            }
        ////        }
        ////        case MemberBindingType.MemberBinding: {
        ////            if(ReferenceEquals(Binding0_Bindings,Binding1_Bindings)) {
        //this.Expression実行AssertEqual(() => new class_演算子オーバーロード { StructCollectionフィールド={ 1 } });
        ////            } else {
        //this.Expression実行AssertEqual(() => new class_演算子オーバーロード { StructCollectionフィールド={ a*a } });
        ////            }
        ////        }
        ////        default: {
        ////            for(var b=0; b < MemberListBinding0_Initializers_Count; b++) {
        ////                if(ReferenceEquals(MemberListBinding0_Initializer_Arguments,MemberListBinding1_Initializer_Arguments)) {
        //this.Expression実行AssertEqual(() => new class_演算子オーバーロード { 自己参照=new() { StructCollectionフィールド={ 1 } } });
        ////                } else {
        //this.Expression実行AssertEqual(() => new class_演算子オーバーロード { 自己参照=new() { StructCollectionフィールド={ a*a } } });
        ////                }
        ////            }
        ////            if(変化したか1) {
        //this.Expression実行AssertEqual(() => new class_演算子オーバーロード { StructCollectionフィールド={ a*a } });
        ////            } else
        //this.Expression実行AssertEqual(() => new class_演算子オーバーロード { StructCollectionフィールド={ 1 } });
        ////        }
        ////        default:throw new NotSupportedException($"{Binding0.BindingType}はサポートされていない");
        ////    }
        ////}
    }
    [Fact]
    public void Add()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Add(@int,@int));
    [Fact]
    public void AddAssign()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.AddAssign(p_int,@int));
    [Fact]
    public void AddAssignChecked()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.AddAssignChecked(p_int,@int));
    [Fact]
    public void AddChecked()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.AddChecked(@int,@int));
    [Fact]
    public void And()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.And(@int,@int));
    [Fact]
    public void AndAssign()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.AndAssign(p_int,@int));
    [Fact]
    public void ArrayIndex()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.ArrayIndex(@array,@int));
    [Fact]
    public void Assign()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Assign(p_int,@int));
    [Fact]
    public void Coalesce()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Coalesce(@string,@string));
    [Fact]
    public void Divide()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Divide(@int,@int));
    [Fact]
    public void DivideAssign()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.DivideAssign(p_int,@int));
    [Fact]
    public void Equal()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Equal(@int,@int));
    [Fact]
    public void ExclusiveOr()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.ExclusiveOr(@int,@int));
    [Fact]
    public void ExclusiveOrAssign()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.ExclusiveOrAssign(p_int,@int));
    [Fact]
    public void GreaterThan()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.GreaterThan(@int,@int));
    [Fact]
    public void GreaterThanOrEqual()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.GreaterThanOrEqual(p_int,@int));
    [Fact]
    public void LeftShift()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.LeftShift(@int,@int));
    [Fact]
    public void LeftShiftAssign()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.LeftShiftAssign(p_int,@int));
    [Fact]
    public void LessThan()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.LessThan(@int,@int));
    [Fact]
    public void LessThanOrEqual()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.LessThanOrEqual(@int,@int));
    [Fact]
    public void Modulo()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Modulo(@int,@int));
    [Fact]
    public void ModuloAssign()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.ModuloAssign(p_int,@int));
    [Fact]
    public void Multiply()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Multiply(@int,@int));
    [Fact]
    public void MultiplyAssign()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.MultiplyAssign(p_int,@int));
    [Fact]
    public void MultiplyAssignChecked()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.MultiplyAssignChecked(p_int,@int));
    [Fact]
    public void MultiplyChecked()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.MultiplyChecked(@int,@int));
    [Fact]
    public void NotEqual()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.NotEqual(@int,@int));
    [Fact]
    public void Or()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Or(@int,@int));
    [Fact]
    public void OrAssign()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.OrAssign(p_int,@int));
    [Fact]
    public void OrElse(){
        //if(Binary0_Left==Binary1_Left)
        //    if(Binary0_Right==Binary1_Right)
        //        if(Binary0_Conversion==Binary1_Conversion)
        var Equal=Expression.Equal(
            Expression.Constant(0m),
            Expression.Constant(0m)
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.OrElse(
                Equal,
                Equal
            )
        );
    }
    [Fact]
    public void Power()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Power(@double,@double));
    [Fact]
    public void PowerAssign()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.PowerAssign(p_double,@double));
    [Fact]
    public void RightShift()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.RightShift(@int,@int));
    [Fact]
    public void RightShiftAssign()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.RightShiftAssign(p_int,@int));
    [Fact]
    public void Subtract()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Subtract(@int,@int));
    [Fact]
    public void SubtractAssign()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.SubtractAssign(p_int,@int));
    [Fact]
    public void SubtractAssignChecked()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.SubtractAssignChecked(p_int,@int));
    [Fact]
    public void SubtractChecked()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.SubtractChecked(@int,@int));
    [Fact]
    public void Block(){
        //if(Block0_Expressions_Count>=6||Block0.Variables.Count>=1||Block0_Expressions[Block0_Expressions_Count-1].Type!=Block0.Type){
        //    if(ReferenceEquals(Block0_Expressions,Block1_Expressions))
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Constant(0m),
                Expression.Constant(1m),
                Expression.Constant(2m),
                Expression.Constant(3m),
                Expression.Constant(4m),
                Expression.Constant(5m)
            )
        );
        //    else 
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Constant(0m),
                Expression.Constant(0m),
                Expression.Constant(0m),
                Expression.Constant(0m),
                Expression.Constant(0m),
                Expression.Constant(0m)
            )
        );
        //}
        //if(Block0_Expressions_Count<=1)
        //    if(b) 
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Add(
                    Expression.Constant(0m),
                    Expression.Constant(0m)
                )
            )
        );
        //    else
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Constant(0m)
            )
        );
        //if(Block0_Expressions_Count<=2)
        //    if(b) 
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Constant(0m),
                Expression.Constant(0m)
            )
        );
        //    else
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Constant(0m),
                Expression.Constant(1m)
            )
        );
        //if(Block0_Expressions_Count<=3)
        //    if(b) 
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Constant(0m),
                Expression.Constant(0m),
                Expression.Constant(0m)
            )
        );
        //    else
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Constant(0m),
                Expression.Constant(1m),
                Expression.Constant(2m)
            )
        );
        //if(Block0_Expressions_Count<=4)
        //    if(b) 
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Constant(0m),
                Expression.Constant(0m),
                Expression.Constant(0m),
                Expression.Constant(0m)
            )
        );
        //    else
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Constant(0m),
                Expression.Constant(1m),
                Expression.Constant(2m),
                Expression.Constant(3m)
            )
        );
        //if(b)
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Constant(0m),
                Expression.Constant(0m),
                Expression.Constant(0m),
                Expression.Constant(0m),
                Expression.Constant(0m)
            )
        );
        //else
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Constant(0m),
                Expression.Constant(1m),
                Expression.Constant(2m),
                Expression.Constant(3m),
                Expression.Constant(4m)
            )
        );
    }
    [Fact]
    public void Conditional(){
        var Equal=Expression.Equal(
            Expression.Constant(0m),
            Expression.Constant(0m)
        );
        //if(Conditional0_Test==Conditional1_Test)
        //    if(Conditional0_IfTrue==Conditional1_IfTrue)
        //        if(Conditional0_IfFalse==Conditional1_IfFalse)
        this.変換_局所Parameterの先行評価_実行(
            Expression.Condition(
                Expression.Constant(true),
                Expression.Constant(true),
                Expression.Constant(true)
            )
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.Condition(
                Expression.Constant(true),
                Expression.Constant(true),
                Equal
            )
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.Condition(
                Expression.Constant(true),
                Equal,
                Expression.Constant(true)
            )
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.Condition(
                Equal,
                Expression.Constant(true),
                Expression.Constant(true)
            )
        );
    }
    [Fact]
    public void Constant()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Constant(0));
    [Fact]
    public void DebugInfo()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.DebugInfo(Expression.SymbolDocument("abc"),1,1,1,1));
    [Fact]
    public void Default()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Default(typeof(void)));
    [Fact]
    public void Dynamic000(){
        this.変換_局所Parameterの先行評価.TraverseNullable(
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
    }
    [Fact]
    public void Goto()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Goto(Expression.Label()));
    [Fact]
    public void MakeIndex()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.MakeIndex(p_List,typeof(List<int>).GetProperty("Item"),new[]{@int}));
    [Fact]
    public void Index(){
        //if(Index1_Object==Index0_Object)
        //    if(ReferenceEquals(Index0_Arguments,Index1_Arguments)) 
        var array=Expression.Parameter(typeof(decimal[]),"array");
        this.変換_局所Parameterの先行評価_実行(
            Expression.ArrayAccess(
                array,
                Expression.Constant(0)
            )
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.ArrayAccess(
                array,
                Expression.Convert(
                    Expression.Add(
                        Expression.Constant(0m),
                        Expression.Constant(0m)
                    ),
                    typeof(int)
                )
            )
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.ArrayAccess(
                Expression.ArrayAccess(
                    Expression.Constant(new decimal[][]{new decimal[1]}),
                    Expression.Convert(
                        Expression.Add(
                            Expression.Constant(0m),
                            Expression.Constant(0m)
                        ),
                        typeof(int)
                    )
                ),
                Expression.Constant(0)
            )
        );
    }
    [Fact]
    public void Invoke(){
        this.変換_局所Parameterの先行評価_実行(
            Expression.Invoke(
                Expression.Lambda(
                    Expression.Constant(0m),
                    p_decimal
                ),
                Expression.Constant(0m)
            )
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.Invoke(
                Expression.Lambda(
                    Expression.Add(
                        Expression.Constant(1m),
                        Expression.Constant(1m)
                    ),
                    p_decimal
                ),
                Expression.Add(
                    Expression.Constant(2m),
                    Expression.Constant(2m)
                )
            )
        );
    }
    [Fact]
    public void Label(){
        //if(Label0_DefaultValue==Label1_DefaultValue) return Label0;
        var Label=Expression.Label(typeof(string));
        this.変換_局所Parameterの先行評価_実行(Expression.Label(Label,Expression.Constant("ABC")));
        var Call=Expression.Call(typeof(ReturnExpressionTraverser).GetMethod(nameof(static_string),BindingFlags.Static|BindingFlags.NonPublic)!);
        this.変換_局所Parameterの先行評価_実行(
            Expression.Label(
                Label,
                Expression.Call(
                    typeof(string).GetMethod("Concat",new[]{typeof(string),typeof(string)})!,
                    Call,
                    Call
                )
            )
        );
    }
    [Fact]
    public void Lambda(){
        //if(Lambda0_Body==Lambda1_Body)return Lambda0;
        var p0=Expression.Parameter(typeof(int));
        var p1=Expression.Parameter(typeof(int));
        共通(p0,p0);
        共通(p0,p1);
        void 共通(ParameterExpression Expression0,Expression Expression1){
            var Lambda=Expression.Lambda(Expression0);
            this.変換_旧Parameterを新Expression1.実行(Lambda,Expression0,Expression1);
        }
    }
    //[Fact]public void ListInit()=>o.TraverseNullable(Expression.ListInit(Expression.New(typeof(List<int>)), Expression.ElementInit(typeof(List<int>).GetMethod("Add")!, Expression.Constant(1))));
    //[Fact]public void Loop()=>o.TraverseNullable(Expression.Loop(Expression.Default(typeof(void))));
    [Fact]
    public void MakeMemberAccess()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.MakeMemberAccess(Expression.Constant(new Point(0,0)),typeof(Point).GetProperty(nameof(Point.X))!));
    //[Fact]public void MemberInit()=>o.TraverseNullable(Expression.MemberInit(_New, Expression.Bind(Int32フィールド1, @int)));
    static decimal static6(decimal p1,decimal p2,decimal p3,decimal p4,decimal p5,decimal p6)=>0;
    static decimal static5(decimal p1,decimal p2,decimal p3,decimal p4,decimal p5)=>0;
    static decimal static4(decimal p1,decimal p2,decimal p3,decimal p4)=>0;
    static decimal static3(decimal p1,decimal p2,decimal p3)=>0;
    static decimal static2(decimal p1,decimal p2)=>0;
    static decimal static0()=>0;
    static decimal static1(decimal p1)=>0;
    [SuppressMessage("ReSharper","ConvertClosureToMethodGroup")]
    [Fact]
    public void Call(){
        var d=0m;
        //if(MethodCall0_Object is null) {
        //    if(MethodCall0_Arguments_Count>=6){
        //        if(ReferenceEquals(MethodCall0_Argumentsm,MethodCall1_Arguments)) 
        this.Optimizer_Lambda最適化(()=>static6(1m,2m,3m,4m,5m,6m));
        //        else 
        this.Optimizer_Lambda最適化(()=>static6(1m,1m,1m,1m,1m,1m));
        //    }
        //    if(MethodCall0_Arguments_Count==0)return MethodCall0;
        this.Optimizer_Lambda最適化(()=>static0());
        //    if(MethodCall0_Arguments_Count==1)
        //        if(b)
        this.Optimizer_Lambda最適化(()=>static1(1m));
        //        else 
        this.Optimizer_Lambda最適化(()=>static1(d*d));
        //    if(MethodCall0_Arguments_Count==2)
        //        if(b)
        this.Optimizer_Lambda最適化(()=>static2(1m,2m));
        //        else 
        this.Optimizer_Lambda最適化(()=>static2(1m,1m));
        //    if(MethodCall0_Arguments_Count==3)
        //        if(b)
        this.Optimizer_Lambda最適化(()=>static3(1m,2m,3m));
        //        else 
        this.Optimizer_Lambda最適化(()=>static3(1m,1m,1m));
        //    if(MethodCall0_Arguments_Count==4)
        //        if(b)
        this.Optimizer_Lambda最適化(()=>static4(1m,2m,3m,4m));
        //        else 
        this.Optimizer_Lambda最適化(()=>static4(1m,1m,1m,1m));
        //    if(b) 
        this.Optimizer_Lambda最適化(()=>static5(1m,2m,3m,4m,5m));
        //    else
        this.Optimizer_Lambda最適化(()=>static5(1m,1m,1m,1m,1m));
        //} else {
        //    if(MethodCall0_Arguments_Count>=4){
        //        if(b)
        //            if(ReferenceEquals(MethodCall0_Argumentsm,MethodCall1_Arguments))
        var f4=(decimal p1m,decimal p2m,decimal p3m,decimal p4)=>0;
        this.Optimizer_Lambda最適化(()=>f4.Invoke(1m,2m,3m,4m));
        //                return MethodCall0;
        this.Optimizer_Lambda最適化(()=>f4.Invoke(1m,1m,1m,1m));
        this.Optimizer_Lambda最適化(()=>new[]{f4}[(int)(d*d)].Invoke(1m,1m,1m,1m));
        //    }
        //    if(MethodCall0_Arguments_Count==0)
        //        if(b)
        var f0=()=>0;
        this.Optimizer_Lambda最適化(()=>f0.Invoke());
        //        else 
        this.Optimizer_Lambda最適化(()=>new[]{f0}[(int)(d*d)].Invoke());
        //    if(MethodCall0_Arguments_Count==1)
        //        if(b)
        var f1=(decimal p1)=>0;
        this.Optimizer_Lambda最適化(()=>f1.Invoke(1m));
        //        else 
        this.Optimizer_Lambda最適化(()=>f1.Invoke(d*d));
        //    if(MethodCall0_Arguments_Count==2)
        //        if(b)
        var f2=(decimal p1m,decimal p2)=>0;
        this.Optimizer_Lambda最適化(()=>f2.Invoke(1m,2m));
        //        else 
        this.Optimizer_Lambda最適化(()=>f2.Invoke(1m,1m));
        //    if(b) 
        var f3=(decimal p1m,decimal p2m,decimal p3)=>0;
        this.Optimizer_Lambda最適化(()=>f3.Invoke(1m,2m,3m));
        //    else
        this.Optimizer_Lambda最適化(()=>f3.Invoke(1m,1m,1m));
        //}
    }
    [Fact]
    public void NewArrayBounds()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.NewArrayBounds(typeof(int),@int));
    //[Fact]public void NewArrayInit()=>o.TraverseNullable(Expression.NewArrayInit(typeof(int), @int));
    //[Fact]public void New()=>o.TraverseNullable(Expression.New(typeof(List<int>).GetConstructor(Type.EmptyTypes)!));
    //[Fact]public void ;()=>o.TraverseNullable(p_int);
    [Fact]
    public void RuntimeVariables()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.RuntimeVariables(p_int,p_double));
    //[Fact]public void Switch()=>o.TraverseNullable(Expression.Switch(@int, @int));
    [Fact]
    public void TryFinally()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.TryFinally(@int,@int));
    //[Fact]public void TypeEqual()=>o.TraverseNullable(Expression.TypeEqual(@int, typeof(int)));
    //[Fact]public void TypeIs()=>o.TraverseNullable(Expression.TypeIs(@int, typeof(int)));
    [Fact]
    public void ArrayLength()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.ArrayLength(@array));
    [Fact]
    public void Convert()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Convert(@int,typeof(int)));
    [Fact]
    public void ConvertChecked()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.ConvertChecked(@int,typeof(int)));
    [Fact]
    public void Decrement()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Decrement(@int));
    [Fact]
    public void Increment()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Increment(@int));
    [Fact]
    public void IsFalse()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.IsFalse(@bool));
    [Fact]
    public void IsTrue()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.IsTrue(@bool));
    [Fact]
    public void Negate()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Negate(@int));
    [Fact]
    public void NegateChecked()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.NegateChecked(@int));
    [Fact]
    public void Not()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Not(@int));
    [Fact]
    public void OnesComplement()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.OnesComplement(@int));
    [Fact]
    public void PostDecrementAssign()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.PostDecrementAssign(p_int));
    [Fact]
    public void PostIncrementAssign()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.PostIncrementAssign(p_int));
    [Fact]
    public void PreDecrementAssign()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.PreDecrementAssign(p_int));
    [Fact]
    public void PreIncrementAssign()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.PreIncrementAssign(p_int));
    [Fact]
    public void Quote()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Quote(Expression.Lambda<Action>(@int)));
    [Fact]
    public void Throw()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Throw(Expression.Constant(new NotImplementedException())));
    //[Fact]public void TypeAs()=>o.TraverseNullable(Expression.TypeAs(@int, typeof(object)));
    [Fact]
    public void UnaryPlus()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.UnaryPlus(@int));
    [Fact]
    public void Unbox()=>this.変換_局所Parameterの先行評価.TraverseNullable(Expression.Unbox(Expression.Constant(1,typeof(object)),typeof(int)));
    //private static readonly CSharpArgumentInfo CSharpArgumentInfo1 = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
    //private static readonly CSharpArgumentInfo[] CSharpArgumentInfoArray1 = {
    //    CSharpArgumentInfo1
    //};
    //private static readonly CSharpArgumentInfo[] CSharpArgumentInfoArray2 = {
    //    CSharpArgumentInfo1,
    //    CSharpArgumentInfo1
    //};
    private static bool static3<T>(T a,T b,T c)=>true;
    [Fact]
    public void Dynamic(){
        //if(Dynamic0_Arguments_Count >= 5){
        {
            var binder=Binder.Invoke(
                CSharpBinderFlags.None,
                typeof(Serializer),
                CSharpArgumentInfoArray4
            );
            {
                this.変換_局所Parameterの先行評価_実行(
                    Expression.Dynamic(
                        binder,
                        typeof(object),
                        Expression.Constant((decimal a,decimal b,decimal c)=>true),
                        Expression.Constant(1m),
                        Expression.Constant(1m),
                        Expression.Constant(1m)
                    )
                );
            }
            {
                this.変換_局所Parameterの先行評価_実行(
                    Expression.Dynamic(
                        binder,
                        typeof(object),
                        Expression.Constant((int a,int b,int c)=>true),
                        Expression.Constant(1),
                        Expression.Constant(1),
                        Expression.Constant(1)
                    )
                );
            }
        }
        //}
        //if(Dynamic0_Arguments_Count<=1){
        //    if(b) return Dynamic0;

        {
            var binder=Binder.Invoke(
                CSharpBinderFlags.None,
                typeof(Serializer),
                CSharpArgumentInfoArray1
            );
            {
                this.変換_局所Parameterの先行評価_実行(
                    Expression.Dynamic(
                        binder,
                        typeof(object),
                        Expression.Constant(()=>true)
                    )
                );
            }
            {
                this.変換_局所Parameterの先行評価_実行(
                    Expression.Dynamic(
                        binder,
                        typeof(object),
                        Expression.ArrayAccess(
                            Expression.Constant(new[]{()=>true}),
                            Expression.Convert(
                                Expression.Add(
                                    Expression.Constant(0m),
                                    Expression.Constant(0m)
                                ),
                                typeof(int)
                            )
                        )
                    )
                );
            }
        }
        //}
        //if(Dynamic0_Arguments_Count<=2){
        //    if(b) return Dynamic0;
        {
            var binder=Binder.Invoke(
                CSharpBinderFlags.None,
                typeof(Serializer),
                CSharpArgumentInfoArray2
            );
            {
                this.変換_局所Parameterの先行評価_実行(
                    Expression.Dynamic(
                        binder,
                        typeof(object),
                        Expression.Constant((decimal a)=>true),
                        Expression.Constant(1m)
                    )
                );
            }
            {
                this.変換_局所Parameterの先行評価_実行(
                    Expression.Dynamic(
                        binder,
                        typeof(object),
                        Expression.Constant((decimal a)=>true),
                        Expression.Add(
                            Expression.Constant(1m),
                            Expression.Constant(1m)
                        )
                    )
                );
            }
        }
        //}
        //if(b) return Dynamic0;
        {
            var binder=Binder.Invoke(
                CSharpBinderFlags.None,
                typeof(Serializer),
                CSharpArgumentInfoArray3
            );
            this.変換_局所Parameterの先行評価_実行(
                Expression.Dynamic(
                    binder,
                    typeof(object),
                    Expression.Constant((int a,int b)=>true),
                    Expression.Constant(1),
                    Expression.Constant(1)
                )
            );
            this.変換_局所Parameterの先行評価_実行(
                Expression.Dynamic(
                    binder,
                    typeof(object),
                    Expression.Constant((decimal a,decimal b)=>true),
                    Expression.Constant(1m),
                    Expression.Constant(1m)
                )
            );
        }
    }
    [Fact]
    public void ListInit(){
        //for(var a=0; a < ListInit1_Initializers_Count; a++) {
        //    if(ReferenceEquals(ListInit0_Initialize_Arguments,ListInit1_Initialize_Arguments)) {
        this.Expression実行AssertEqual(()=>new List<int>{1,2,3});
        //    } else {
        var a=0;
        this.Expression実行AssertEqual(()=>new List<int>{a,a});
        //    }
        //}
    }
    [Fact]
    public void Loop(){
        //this.MemoryMessageJson_Assert(new{a=default(LoopExpression)});
        var Label_decimal=Expression.Label(typeof(decimal),"Label_decimal");
        var Label_void=Expression.Label("Label");
        this.Expression実行AssertEqual(
            Expression.Lambda<Action>(
                Expression.Loop(
                    Expression.Block(
                        Expression.Add(Expression.Constant(1m),Expression.Constant(1m)),
                        Expression.Break(Label_decimal,Expression.Constant(1m)),
                        Expression.Continue(Label_void)
                    ),
                    Label_decimal,
                    Label_void
                )
            )
        );
    }
    [Fact]
    public void MakeAssign(){
        var ParameterDecimmal=Expression.Parameter(typeof(decimal));
        var Constant1=Expression.Constant(1m);
        var ConversionDecimal=Expression.Lambda<Func<decimal,decimal>>(Expression.Add(ParameterDecimmal,ParameterDecimmal),ParameterDecimmal);
        //if(Binary0_Left==Binary1_Left)
        //    if(Binary0_Right==Binary1_Right)
        //        if(Binary0_Conversion==Binary1_Conversion)
        var Add=Expression.Add(
            ParameterDecimmal,
            ParameterDecimmal
        );
        Add=Expression.Add(Add,Add);
        this.変換_局所Parameterの先行評価_実行(
            Expression.AddAssign(
                ParameterDecimmal,
                Expression.Constant(0m),
                null,
                null
            )
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.AddAssign(
                ParameterDecimmal,
                Constant1,
                typeof(decimal).GetMethod("op_Addition"),
                Expression.Lambda(
                    Add,
                    ParameterDecimmal
                )
            )
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.AddAssign(
                ParameterDecimmal,
                Add,
                typeof(decimal).GetMethod("op_Addition"),
                Expression.Lambda(
                    ParameterDecimmal,
                    ParameterDecimmal
                )
            )
        );
        var array=Expression.Parameter(typeof(decimal[]));
        this.変換_局所Parameterの先行評価_実行(
            Expression.AddAssign(
                Expression.ArrayAccess(
                    array,
                    Expression.Convert(
                        Expression.Add(
                            Expression.Constant(0m),
                            Expression.Constant(0m)
                        ),
                        typeof(int)
                    )
                ),
                Expression.Constant(0m),
                typeof(decimal).GetMethod("op_Addition"),
                Expression.Lambda(
                    ParameterDecimmal,
                    ParameterDecimmal
                )
            )
        );
        void 共通(Expression Expression0){
            var Lambda=Expression.Lambda(Expression0);
            this.変換_旧Parameterを新Expression1.実行(Lambda,ParameterDecimmal,ParameterDecimmal);
        }
    }
    [Fact]
    public void MakeBinary(){
    }
    [Fact]
    public void MakeUnary(){
    }
    [Fact]
    public void MamberAccess(){
        //if(Member0_Expression==Member1_Expression)
        this.Expression実行AssertEqual(()=>new Point(0,1).X);
        var a=3;
        this.Expression実行AssertEqual(()=>new Point(a,a).X);
    }
    [Fact]
    public void MemberInit(){
        //if(MemberInit0_NewExpression==MemberInit1_NewExpression && ReferenceEquals(MemberInit0_Bindings,MemberInit1_Bindings))
        //this.共通MemoryMessageJson_Expression_ExpressionAssertEqual(()=>new class_演算子オーバーロード{Int32フィールド=3,Stringフィールド = 3});
        var a=3;
        this.Expression実行AssertEqual(()=>new class_演算子オーバーロード{Int32フィールド=a,Stringフィールド=a.ToString()});
    }
    [Fact]
    public void New(){
        //if(New0.Constructor is null)
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.New(typeof(int))
            )
        );
        //if(ReferenceEquals(New1_Arguments,New0_Arguments))
        this.Expression実行AssertEqual(()=>new decimal(1));
        //    ?Expression.New(New0.Constructor,New1_Arguments)
        var a=1;
        this.Expression実行AssertEqual(()=>new decimal(a,a,a,true,1));
        //    :Expression.New(New0.Constructor,New1_Arguments,New0.Members);
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<Types.Point>>(
                Expression.New(
                    typeof(Types.Point).GetConstructors()[0],
                    new[]{Expression.Constant(1),Expression.Constant(2)},
                    typeof(Types.Point).GetProperty(nameof(Types.Point.X))!,typeof(Types.Point).GetProperty(nameof(Types.Point.Y))!)
            )
        );
    }
    [Fact]
    public void NewArrayBound(){
        //if(ReferenceEquals(NewArray1_Expressions,NewArray0_Expressions))

        this.Expression実行AssertEqual(()=>new int[2,3]);
        var a=3;
        this.Expression実行AssertEqual(()=>new int[a,a]);
    }
    [Fact]
    public void NewArrayInit(){
        //if(ReferenceEquals(NewArray1_Expressions,NewArray0_Expressions))
        this.Expression実行AssertEqual(()=>new int[]{1});
        var a=3;
        this.Expression実行AssertEqual(()=>new int[]{a,a});
    }
    [Fact]
    public void Switch(){
        var Equal=Expression.Equal(
            Expression.Constant(0m),
            Expression.Constant(0m)
        );
        //for(var a=0; a < Switch0_Cases_Count; a++) {
        //    if(Switch0_Case_Body !=Switch1_Case_Body) {
        this.変換_局所Parameterの先行評価_実行(
            Expression.Switch(
                Expression.Constant(true),
                Expression.Constant(true),
                Expression.SwitchCase(
                    Equal,
                    Expression.Constant(true)
                )
            )
        );
        //    } else
        this.変換_局所Parameterの先行評価_実行(
            Expression.Switch(
                Expression.Constant(true),
                Expression.Constant(true),
                Expression.SwitchCase(
                    Expression.Constant(true),
                    Expression.Constant(true)
                )
            )
        );
        //    } else
        this.変換_局所Parameterの先行評価_実行(
            Expression.Switch(
                Expression.Constant(true),
                Equal,
                Expression.SwitchCase(
                    Expression.Constant(true),
                    Expression.Constant(true)
                )
            )
        );
    }
    [Fact]
    public void Try(){
        var ex=Expression.Parameter(typeof(Exception),"ex");
        //if(Try0_Body !=Try1_Body)
        this.変換_局所Parameterの先行評価_実行(
            Expression.TryCatch(
                Expression.Add(
                    Expression.Constant(0m),
                    Expression.Constant(0m)
                ),
                Expression.Catch(
                    ex,
                    Expression.Default(typeof(decimal))
                )
            )
        );
        //if(Try0_Finally!=Try1_Finally)
        this.変換_局所Parameterの先行評価_実行(
            Expression.TryFinally(
                Expression.Constant(0),
                Expression.Add(
                    Expression.Constant(0m),
                    Expression.Constant(0m)
                )
            )
        );
        //for(var a=0; a < Try0_Handlers_Count; a++) {
        //    if(Try0_Handler.Body!=Try1_Handler_Body||Try0_Handler.Filter!=Try1_Handler_Filter) {
        this.変換_局所Parameterの先行評価_実行(
            Expression.TryCatch(
                Expression.Constant(0m),
                Expression.Catch(
                    ex,
                    Expression.Add(
                        Expression.Constant(0m),
                        Expression.Constant(0m)
                    )
                )
            )
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.TryCatch(
                Expression.Constant(0m),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0m),
                    Expression.Equal(
                        Expression.Constant(0m),
                        Expression.Constant(0m)
                    )
                )
            )
        );
        //        if(Try0_Handler.Variable is not null) {
        this.変換_局所Parameterの先行評価_実行(
            Expression.TryCatch(
                Expression.Constant(0m),
                Expression.Catch(
                    ex,
                    Expression.Add(
                        Expression.Constant(0m),
                        Expression.Constant(0m)
                    )
                )
            )
        );
        //        } else {
        this.変換_局所Parameterの先行評価_実行(
            Expression.TryCatch(
                Expression.Constant(0m),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0m),
                    Expression.Equal(
                        Expression.Constant(0m),
                        Expression.Constant(0m)
                    )
                )
            )
        );
        //        }
        //    } else {
        this.変換_局所Parameterの先行評価_実行(
            Expression.TryCatch(
                Expression.Default(typeof(void)),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Default(typeof(void))
                )
            )
        );
        //    }
        //}
        //if(変化したか) {
        //} else {
        //}
    }
    private static string static_string()=>"";
    [Fact]
    public void TypeAs(){
        //if(Unary0_Operand==Unary1_Operand)
        this.変換_局所Parameterの先行評価_実行(
            Expression.TypeAs(
                Expression.Constant("string"),
                typeof(string)
            )
        );
        var Call=Expression.Call(typeof(ReturnExpressionTraverser).GetMethod(nameof(static_string),BindingFlags.Static|BindingFlags.NonPublic)!);
        this.変換_局所Parameterの先行評価_実行(
            Expression.TypeAs(
                Expression.Call(
                    typeof(string).GetMethod("Concat",new[]{typeof(string),typeof(string)})!,
                    Call,
                    Call
                ),
                typeof(string)
            )
        );
    }
    [Fact]
    public void TypeEqual(){
        //if(TypeBinary0_Expression==TypeBinary1_Expression)
        this.変換_局所Parameterの先行評価_実行(
            Expression.TypeEqual(
                Expression.Constant("string"),
                typeof(string)
            )
        );
        var Call=Expression.Call(typeof(ReturnExpressionTraverser).GetMethod(nameof(static_string),BindingFlags.Static|BindingFlags.NonPublic)!);
        this.変換_局所Parameterの先行評価_実行(
            Expression.TypeEqual(
                Expression.Call(
                    typeof(string).GetMethod("Concat",new[]{typeof(string),typeof(string)})!,
                    Call,
                    Call
                ),
                typeof(string)
            )
        );
    }
    [Fact]
    public void TypeIs(){
        //if(TypeBinary0_Expression==TypeBinary1_Expression)
        this.変換_局所Parameterの先行評価_実行(
            Expression.TypeIs(
                Expression.Constant(0m),
                typeof(int)
            )
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.TypeIs(
                Expression.Equal(
                    Expression.Constant(0m),
                    Expression.Constant(0m)
                ),
                typeof(int)
            )
        );
    }
}
