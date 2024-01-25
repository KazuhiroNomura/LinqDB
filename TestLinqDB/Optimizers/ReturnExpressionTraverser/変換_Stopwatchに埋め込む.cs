using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using MemoryPack;
using MessagePack;
using TestLinqDB.Optimizers.Comparer;
using TestLinqDB.仕様;

using Expression = System.Linq.Expressions.Expression;
// ReSharper disable All
namespace TestLinqDB.Optimizers.ReturnExpressionTraverser;
[MemoryPackable,MessagePackObject(true)]
public partial class 時間計測{
    public bool BooleanMethod(int x){
        //Thread.Sleep(100);
        return true;
    }
    public int Int32Method(int x)=>x;
    public override int GetHashCode()=>0;
    public override bool Equals(object? obj)=>true;
}
public class 変換_Stopwatchに埋め込む : 共通{
    //protected override テストオプション テストオプション=>テストオプション.ローカル実行|テストオプション.アセンブリ保存|テストオプション.プロファイラ;
    //protected override テストオプション テストオプション=>テストオプション.ローカル実行|テストオプション.アセンブリ保存;
    private static readonly MethodInfo BooleanMethod=typeof(時間計測).GetMethod(nameof(時間計測.BooleanMethod))!;
    private static readonly MethodInfo Int32Method=typeof(時間計測).GetMethod(nameof(時間計測.Int32Method))!;

    [Fact]public void Condition0(){
        var p=Expression.Constant(new 時間計測());
        var Loop=Expression.Condition(
            Expression.Call(p,BooleanMethod,Expression.Constant(0)),
            Expression.Call(p,BooleanMethod,Expression.Constant(1)),
            Expression.Call(p,BooleanMethod,Expression.Constant(2))
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool>>(
                Loop
            )
        );
    }
    [Fact]public void Condition3(){
        var i=Expression.Parameter(typeof(int));
        var p=Expression.Constant(new 時間計測());
        var Break=Expression.Label(typeof(時間計測));
        _=typeof(時間計測).GetMethod(nameof(時間計測.BooleanMethod))!;
        var Loop=Expression.Loop(
            Expression.Block(
                Expression.IfThen(
                    Expression.LessThan(
                        Expression.PostDecrementAssign(i),
                        Expression.Constant(0)
                    ),
                    Expression.Break(Break,p)
                //),
                //Expression.Condition(
                //    Expression.Call(p,Method,Expression.Constant(0)),
                //    Expression.Call(p,Method,Expression.Constant(1)),
                //    Expression.Call(p,Method,Expression.Constant(2))
                )
            ),
            Break
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<時間計測>>(
                Expression.Block(
                    new[]{i},
                    Expression.Assign(i,Expression.Constant(1)),
                    Loop
                )
            )
        );
    }
    [Fact]public void Condition40(){
        var p=Expression.Constant(new 時間計測());
        var Break=Expression.Label(typeof(時間計測));
        var Loop=Expression.Loop(
            Expression.Block(
                Expression.Break(Break,p),
                Expression.Condition(
                    Expression.Call(p,BooleanMethod,Expression.Constant(0)),
                    Expression.Call(p,BooleanMethod,Expression.Constant(1)),
                    Expression.Call(p,BooleanMethod,Expression.Constant(2))
                )
            ),
            Break
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<時間計測>>(Loop));
    }
    [Fact]public void Condition41(){
        var p=Expression.Constant(new 時間計測());
        var Break=Expression.Label(typeof(時間計測));
        _=typeof(時間計測).GetMethod(nameof(時間計測.BooleanMethod))!;
        var Loop=Expression.Loop(
            Expression.Block(
                Expression.Break(Break,p)
            ),
            Break
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<時間計測>>(Loop));
    }
    [Fact]public void Condition42(){
        var p=Expression.Constant(new 時間計測());
        var Break=Expression.Label(typeof(時間計測));
        _=typeof(時間計測).GetMethod(nameof(時間計測.BooleanMethod))!;
        var Loop=Expression.Loop(
            Expression.Block(
                Expression.Break(Break,p),
                p
            ),
            Break
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<時間計測>>(Loop));
    }
    [Fact]public void Condition5(){
        var i=Expression.Parameter(typeof(int));
        var c=Expression.Constant(new 時間計測());
        var Break=Expression.Label(typeof(int));
        var e=Expression.Block(
            new[]{i},
            Expression.Assign(i,Expression.Constant(ループ回数)),
            Expression.Loop(
                Expression.Block(
                    Expression.IfThen(
                        Expression.LessThan(
                            Expression.PostDecrementAssign(i),
                            Expression.Constant(0)
                        ),
                        Expression.Break(
                            Break,
                            Expression.Call(c,Int32Method,Expression.Constant(0))
                        )
                    ),
                    Expression.Condition(
                        Expression.Call(c,BooleanMethod,Expression.Constant(0)),
                        Expression.Call(c,BooleanMethod,Expression.Constant(1)),
                        Expression.Call(c,BooleanMethod,Expression.Constant(2))
                    )
                ),
                Break
            )
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<int>>(e));
    }
    [Fact]public void Goto0(){
        //ok
        var p=Expression.Constant(1);
        var Continue=Expression.Label();
        var Break=Expression.Label(typeof(int));
        var Loop=Expression.Block(
            Expression.Label(Continue),
            Expression.Break(Break,p),
            p,
            Expression.Goto(Continue),
            Expression.Label(Break,p)
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<int>>(Loop));
    }
    [Fact]public void Goto1(){
        //ok
        var p=Expression.Constant(1);
        Expression.Label();
        var Break=Expression.Label(typeof(int));
        var Loop=Expression.Block(
            Expression.Break(Break,p),
            Expression.Label(Break,p)
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<int>>(Loop));
    }
    [Fact]public void ContinueBreakを組み合わせてLoop0(){
        //ok
        var p=Expression.Parameter(typeof(int),"p");
        var Continue=Expression.Label();
        var Break=Expression.Label(typeof(int));
        var Loop=Expression.Block(
            Expression.Label(Continue),
            Expression.Break(Break,p),
            Expression.Goto(Continue),
            Expression.Label(Break,Expression.Default(Break.Type))
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<int,int>>(Loop,p));
    }
    [Fact]public void ContinueBreakを組み合わせてLoop01(){
        //ok
        var p=Expression.Parameter(typeof(int),"p");
        var Continue=Expression.Label("Continue");
        var Break=Expression.Label(typeof(int),"Break");
        var Loop=Expression.Block(
            Expression.Label(Continue),
            Expression.Break(Break,p),
            Expression.Goto(Continue),
            Expression.Label(Break,p)
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<int,int>>(Loop,p));
    }
    private const int ループ回数=1000000;
    [Fact]public void ContinueBreakを組み合わせてLoop1(){
        var i=Expression.Parameter(typeof(int),"i");
        var p=Expression.Parameter(typeof(int),"p");
        var Continue=Expression.Label("Continue");
        var Break=Expression.Label(typeof(int),"Break");
        var Loop=Expression.Block(
            new[]{i},
            Expression.Assign(i,Expression.Constant(ループ回数)),
            Expression.Label(Continue),
            Expression.IfThen(
                Expression.LessThan(
                    Expression.PostDecrementAssign(i),
                    Expression.Constant(0)
                ),
                Expression.Break(Break,p)
            ),
            Expression.Continue(Continue),
            Expression.Label(Break,Expression.Default(Break.Type))
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<int,int>>(Loop,p));
    }
    [Fact]public void ContinueBreakを組み合わせてLoop10(){
        var i=Expression.Parameter(typeof(int));
        var p=Expression.Parameter(typeof(int),"p");
        var Continue=Expression.Label("Continue");
        var Break=Expression.Label(typeof(int),"Break");
        var Loop=Expression.Block(
            new[]{i},
            Expression.Assign(i,Expression.Constant(ループ回数)),
            Expression.Label(Continue),
            Expression.IfThenElse(
                Expression.LessThan(
                    Expression.PostDecrementAssign(i),
                    Expression.Constant(0)
                ),
                Expression.Break(Break,p),
                i
            ),
            Expression.Continue(Continue),
            Expression.Label(Break,Expression.Default(Break.Type))
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<int,int>>(Loop,p));
    }
    [Fact]public void Conditional6(){
        var i=Expression.Parameter(typeof(int));
        var p=Expression.Parameter(typeof(int),"p");
        var Loop=Expression.Block(
            new[]{i},
            Expression.Assign(i,Expression.Constant(ループ回数)),
            Expression.IfThenElse(
                Expression.LessThan(
                    Expression.PostDecrementAssign(i),
                    Expression.Constant(0)
                ),
                p,
                p
            ),
            i
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<int,int>>(Loop,p));
    }
    [Fact]public void ContinueBreakを組み合わせてLoop2(){
        var p=Expression.Parameter(typeof(int),"p");
        var Continue=Expression.Label();
        var Break=Expression.Label();
        var Loop=Expression.Block(
            Expression.Label(Continue),
            Expression.Block(
                Expression.Break(Break),
                p
            ),
            Expression.Goto(Continue),
            Expression.Label(Break),
            p
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<int,int>>(Loop,p));
    }
    [Fact]public void Loop0(){
        //ok
        var p=Expression.Parameter(typeof(int),"p");
        var Break=Expression.Label(typeof(int));
        var Loop=Expression.Loop(
            Expression.Block(
                Expression.Break(Break,p)
            ),
            Break
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<int,int>>(Loop,p));
    }
    [Fact]public void Loop1(){
        //error
        var p=Expression.Parameter(typeof(int),"p");
        var Break=Expression.Label(typeof(int),"Break");
        var Loop=Expression.Loop(
            Expression.Block(
                Expression.Break(Break,p)
                ,p
            ),
            Break
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<int,int>>(Loop,p));
    }
    [Fact]public void TryCatch(){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.TryCatch(
                    Expression.Constant(1),
                    Expression.Catch(
                        typeof(Exception),
                        Expression.Constant(0)
                    )
                )
            )
        );
    }
    [Fact]public void TryCatchFinally(){
        var p = Expression.Parameter(typeof(int), "int32");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int, int>>(
                Expression.TryCatchFinally(
                    p,
                    Expression.AddAssign(p, p)
                ), p
            )
        );
    }
    [Fact]public void TryCatchFilter(){
        var InvalidCastException=Expression.Parameter(typeof(InvalidCastException),"ex0");
        var NotImplementedException=Expression.Parameter(typeof(NotImplementedException),"ex1");
        var NotSupportedException=Expression.Parameter(typeof(NotSupportedException),"ex2");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.TryCatch(
                    Expression.Constant(1),
                    Expression.Catch(
                        InvalidCastException,
                        Expression.Constant(2),
                        Expression.Equal(InvalidCastException,Expression.Default(InvalidCastException.Type))
                    ),
                    Expression.Catch(
                        NotImplementedException,
                        Expression.Constant(3),
                        Expression.Equal(NotImplementedException,Expression.Default(NotImplementedException.Type))
                    ),
                    Expression.Catch(
                        NotSupportedException,
                        Expression.Constant(4),
                        Expression.Equal(NotSupportedException,Expression.Default(NotSupportedException.Type))
                    )
                )
            )
        );
    }
    public static void gg(){
        try{
            gg();
        } catch(Exception ex)when(ex.Message==""){
        } catch(Exception ex){
        }
    }
    [Fact]
    public void TryCatchFinallyFilter0(){
        var InvalidCastException=Expression.Parameter(typeof(InvalidCastException),"ex0");
        var NotImplementedException=Expression.Parameter(typeof(NotImplementedException),"ex1");
        var NotSupportedException=Expression.Parameter(typeof(NotSupportedException),"ex2");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.TryCatchFinally(
                    Expression.Add(
                        Expression.Constant(-11),
                        Expression.Constant(1)
                    ),
                    Expression.Constant(5),
                    Expression.Catch(
                        InvalidCastException,
                        Expression.Constant(2),
                        Expression.Equal(InvalidCastException,Expression.Default(InvalidCastException.Type))
                    ),
                    Expression.Catch(
                        NotImplementedException,
                        Expression.Constant(3),
                        Expression.Equal(NotImplementedException,Expression.Default(NotImplementedException.Type))
                    ),
                    Expression.Catch(
                        NotImplementedException,
                        Expression.Constant(3)
                    ),
                    Expression.Catch(
                        NotSupportedException,
                        Expression.Constant(4),
                        Expression.Equal(NotSupportedException,Expression.Default(NotSupportedException.Type))
                    )
                )
            )
        );
    }
    [Fact]
    public void TryCatchFinallyFilter1(){
        //var InvalidCastException=Expression.Parameter(typeof(InvalidCastException),"ex0");
        var NotImplementedException=Expression.Parameter(typeof(NotImplementedException),"ex1");
        //var NotSupportedException=Expression.Parameter(typeof(NotSupportedException),"ex2");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.TryCatchFinally(
                    Expression.Add(
                        Expression.Constant(-11),
                        Expression.Constant(1)
                    ),
                    Expression.Constant(5),
                    Expression.Catch(
                        typeof(InvalidCastException),
                        Expression.Constant(2),
                        Expression.Constant(true)
                    ),
                    Expression.Catch(
                        NotImplementedException,
                        Expression.Constant(3),
                        Expression.Equal(NotImplementedException,Expression.Default(NotImplementedException.Type))
                    ),
                    Expression.Catch(
                        NotImplementedException,
                        Expression.Constant(3)
                    ),
                    Expression.Catch(
                        typeof(NotSupportedException),
                        Expression.Constant(4),
                        Expression.Constant(false)
                    )
                )
            )
        );
    }
    [Fact]public void Switch_Case_Default(){
        var p = Expression.Parameter(typeof(int), "p");
        var pp=Expression.And(p,p);
        var SwitchCases=new List<SwitchCase>();
        var TestValues=new List<Expression>();
        var r=new Random(1);
        var case定数=0;
        for(var a=0;a<10;a++){
            for(var b=r.Next(1,10);b>=0;b--) TestValues.Add(Expression.Constant(case定数++));
            var SwitchCase=Expression.SwitchCase(pp,TestValues);
            SwitchCases.Add(SwitchCase);
            TestValues.Clear();
        }
        var e=Expression.Switch(pp,pp,SwitchCases.ToArray());
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                e,
                p
            )
        );
    }
    [Fact]public void TypeAs(){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<string>>(
                Expression.TypeAs(
                    Expression.Constant("string"),
                    typeof(string)
                )
            )
        );
    }
    [Fact]public void TypeIs(){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool>>(
                Expression.TypeIs(
                    Expression.Constant(0m),
                    typeof(int)
                )
            )
        );
    }
    [Fact]public void Constant(){
        //if(Constant0.Value is null){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<object?>>(
                Expression.Constant(null)
            )
        );
        //} else {
        //    if(Constant_Type==typeof(string)){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<object>>(
                Expression.Constant("abc")
            )
        );
        //    }else if(Constant_Type==typeof(decimal)){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<decimal>>(
                Expression.Constant(1m)
            )
        );
        //    }else {
        //        if(Constant_Type.IsEnum){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<TypeCode>>(
                Expression.Constant(TypeCode.Boolean)
            )
        );
        //        }
        //        if(Constant_Type==typeof(sbyte)){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<sbyte>>(
                Expression.Constant((sbyte)1)
            )
        );
        //        }else if(Constant_Type==typeof(short)){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<short>>(
                Expression.Constant((short)1)
            )
        );
        //        }else if(Constant_Type==typeof(int)){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Constant(1)
            )
        );
        //        }else if(Constant_Type==typeof(long)){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<long>>(
                Expression.Constant((long)1)
            )
        );
        //        }else if(Constant_Type==typeof(nint)){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<nint>>(
                Expression.Constant((nint)1)
            )
        );
        //        }else if(Constant_Type==typeof(byte)){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<byte>>(
                Expression.Constant((byte)1)
            )
        );
        //        }else if(Constant_Type==typeof(ushort)){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<ushort>>(
                Expression.Constant((ushort)1)
            )
        );
        //        }else if(Constant_Type==typeof(uint)){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<uint>>(
                Expression.Constant(1u)
            )
        );
        //        }else if(Constant_Type==typeof(ulong)){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<ulong>>(
                Expression.Constant((ulong)1)
            )
        );
        //        }else if(Constant_Type==typeof(nuint)){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<nuint>>(
                Expression.Constant((nuint)1)
            )
        );
        //        }else if(Constant_Type==typeof(bool)){
        //            if((bool)Constant0.Value)
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool>>(
                Expression.Constant(true)
            )
        );
        //            else
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool>>(
                Expression.Constant(false)
            )
        );
        //        }else if(Constant_Type==typeof(char)){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<char>>(
                Expression.Constant('a')
            )
        );
        //        }else if(Constant_Type==typeof(float)){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<float>>(
                Expression.Constant(1f)
            )
        );
        //        }else if(Constant_Type==typeof(double)){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<double>>(
                Expression.Constant(1d)
            )
        );
        //        } else{
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<object>>(
                Expression.Constant("object",typeof(object))
            )
        );
        //        }
        //    }
        //}
    }
    [Fact]public void Bindings(){
        //for(var a = 0;a < Bindings0_Count;a++) {
        //    switch(Binding0.BindingType) {
        //        case MemberBindingType.Assignment:{
        this.Expression実行AssertEqual(()=>new class_演算子オーバーロード{Int32フィールド=3});
        //        }
        //        case MemberBindingType.MemberBinding: {
        this.Expression実行AssertEqual(() => new class_演算子オーバーロード {class_演算子オーバーロード2= {a = 1}});
        //        }
        //        default: {
        //            for(var b = 0;b < MemberListBinding0_Initializers_Count;b++) {
        this.Expression実行AssertEqual(()=>new class_演算子オーバーロード{StructCollectionフィールド = {1,2}});
        //            }
        //        }
        //    }
        //}
    }
    private delegate int delegate_int_refint(ref int input);
    private static int int_refint(ref int input, delegate_int_refint d) => d(ref input);
    private const BindingFlags f = BindingFlags.Static|BindingFlags.NonPublic;
    private static int int_int_int(int a,int b){
        var r=1;
        for(var x=0;x<b;x++){
            r*=a;
        }
        return r;
    }
    private static int FieldInt32;
    private static readonly MemberExpression MemberInt32 =Expression.MakeMemberAccess(
        null,
        typeof(変換_Stopwatchに埋め込む).GetField(nameof(FieldInt32),BindingFlags.Static|BindingFlags.NonPublic)!
    );
    [Fact]public void Assign(){
        //switch(Binary0_Left_NodeType){
        //    case ExpressionType.Parameter: {
        //    }
        //    case ExpressionType.MemberAccess:{
        //    }
        //    default:{
        //    }
        //}
        var f = BindingFlags.Static|BindingFlags.NonPublic;
        var ref_p = Expression.Parameter(typeof(int).MakeByRefType(), "ref_p");
        var int_refint_intFuncRef = typeof(変換_Stopwatchに埋め込む).GetMethod(nameof(変換_Stopwatchに埋め込む.int_refint), f)!;
        var int_refint = typeof(変換_Stopwatchに埋め込む).GetMethod(nameof(変換_Stopwatchに埋め込む.int_refint), f)!;
        var p = Expression.Parameter(typeof(int), "p");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.Assign(
                    p,
                    Expression.Constant(4)
                ),
                p
            )
        );
        var ParameterInt32= Expression.Parameter(typeof(int),"int32");
        var Constant0= Expression.Constant(0);
        var Constant1= Expression.Constant(1);
        var ConversionInt32=Expression.Lambda<Func<int,int>>(Expression.Add(ParameterInt32,ParameterInt32),ParameterInt32);
        var Method_int=GetMethod(()=>int_int_int(1,1));
        共通1(Expression.AddAssign            (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        void 共通1(BinaryExpression Binary){
            this.Expression実行AssertEqual(
                Expression.Lambda<Func<object>>(
                    Expression.Block(
                        Expression.Assign(MemberInt32,Constant0),
                        Expression.Convert(Binary,typeof(object))
                    )
                )
            );
        }
        var a=Expression.Parameter(typeof(int),"a");
        const int expected=10,index=1;
        var array=new int[30];
        array[index]=expected;
        this.Expression実行AssertEqual(
            Expression.Lambda<Action>(
                Expression.Block(
                    Expression.Lambda<Func<int>>(
                        Expression.Assign(
                            Expression.ArrayAccess(
                                Expression.Constant(array),
                                Expression.Convert(
                                    Expression.Constant((decimal)index),
                                    typeof(int)
                                )
                            ),
                            Expression.Constant(10)
                        )
                    )
                )
            )
        );
    }
}
[MemoryPackable,MessagePackObject(true)]
public partial struct S{
    public int a=3;
    public S(){}
}
