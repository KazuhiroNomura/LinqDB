using System.Linq.Expressions;
using LinqDB.Databases.Tables;
using LinqDB.Helpers;
using LinqDB.Sets;
//using Exception=System.Exception;
using Expression = System.Linq.Expressions.Expression;
using ValueTuple=System.ValueTuple;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
namespace TestLinqDB.Optimizers;
public class 変換_メソッド正規化_取得インライン不可能定数:共通{
    [Fact]
    public void Constant(){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Constant(0)
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<decimal>>(
                Expression.Constant(0m)
            )
        );
    }
    private static int F(Expression<Func<int>> f){
        var m=f.Compile();
        return m();
    }
    [Fact]public void Quote(){
        this.Expression実行AssertEqual(()=>F(()=>3));
    }
    private static int int_int_int(int a,int b){
        var r=1;
        for(var x=0;x<b;x++){
            r*=a;
        }
        return r;
    }
    private void 共通BinaryAssign(ExpressionType NodeType){
        var ParameterInt32=Expression.Parameter(typeof(int),"int32");
        var ConversionInt32=
            Expression.Lambda<Func<int,int>>(Expression.Add(ParameterInt32,ParameterInt32),ParameterInt32);
        var Method_int=GetMethod(()=>int_int_int(1,1));
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.MakeBinary(NodeType,ParameterInt32,ParameterInt32,false,Method_int,ConversionInt32),
                ParameterInt32
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.MakeBinary(NodeType,ParameterInt32,ParameterInt32,false,Method_int),
                ParameterInt32
            )
        );
    }
    [Fact]
    public void AddAssign()=>this.共通BinaryAssign(ExpressionType.AddAssign);
    [Fact]
    public void AddAssignChecked()=>this.共通BinaryAssign(ExpressionType.AddAssignChecked);
    [Fact]
    public void AndAssign()=>this.共通BinaryAssign(ExpressionType.AndAssign);
    [Fact]
    public void DivideAssign()=>this.共通BinaryAssign(ExpressionType.DivideAssign);
    [Fact]
    public void ExclusiveOrAssign()=>this.共通BinaryAssign(ExpressionType.ExclusiveOrAssign);
    [Fact]
    public void LeftShiftAssign()=>this.共通BinaryAssign(ExpressionType.LeftShiftAssign);
    [Fact]
    public void ModuloAssign()=>this.共通BinaryAssign(ExpressionType.ModuloAssign);
    [Fact]
    public void MultiplyAssign()=>this.共通BinaryAssign(ExpressionType.MultiplyAssign);
    [Fact]
    public void MultiplyAssignChecked()=>this.共通BinaryAssign(ExpressionType.MultiplyAssignChecked);
    [Fact]
    public void OrAssign()=>this.共通BinaryAssign(ExpressionType.OrAssign);
    [Fact]
    public void PowerAssign()=>this.共通BinaryAssign(ExpressionType.PowerAssign);
    [Fact]
    public void RightShiftAssign()=>this.共通BinaryAssign(ExpressionType.RightShiftAssign);
    [Fact]
    public void SubtractAssign()=>this.共通BinaryAssign(ExpressionType.SubtractAssign);
    [Fact]
    public void SubtractAssignChecked()=>this.共通BinaryAssign(ExpressionType.SubtractAssignChecked);
    [Fact]
    public void Try(){
        var ParameterInt32=Expression.Parameter(typeof(int),"int32");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.TryCatch(
                    Expression.AddAssign(ParameterInt32,ParameterInt32),
                    Expression.Catch(
                        typeof(Exception),
                        Expression.Constant(0)
                    )
                ),
                ParameterInt32
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.TryCatchFinally(
                    ParameterInt32,
                    Expression.AddAssign(ParameterInt32,ParameterInt32)
                ),ParameterInt32
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.TryCatch(
                    Expression.Constant(0),
                    Expression.Catch(
                        typeof(Exception),
                        Expression.Constant(0)
                    )
                )
            )
        );
        var ex=Expression.Parameter(typeof(Exception),"ex");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.TryCatch(
                    ParameterInt32,
                    Expression.Catch(
                        ex,
                        Expression.AddAssign(ParameterInt32,ParameterInt32)
                    )
                ),
                ParameterInt32
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.TryCatch(
                    ParameterInt32,
                    Expression.Catch(
                        ex,
                        ParameterInt32,
                        Expression.Equal(
                            Expression.AddAssign(ParameterInt32,ParameterInt32),
                            ParameterInt32
                        )
                    )
                ),
                ParameterInt32
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.TryCatch(
                    Expression.Constant(0),
                    Expression.Catch(
                        ex,
                        Expression.Constant(0)
                    )
                )
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.TryCatch(
                    ParameterInt32,
                    Expression.Catch(
                        typeof(Exception),
                        Expression.AddAssign(ParameterInt32,ParameterInt32)
                    )
                ),
                ParameterInt32
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.TryCatch(
                    ParameterInt32,
                    Expression.Catch(
                        typeof(Exception),
                        ParameterInt32,
                        Expression.Equal(
                            Expression.AddAssign(ParameterInt32,ParameterInt32),
                            ParameterInt32
                        )
                    )
                ),
                ParameterInt32
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.TryCatch(
                    ParameterInt32,
                    Expression.Catch(
                        typeof(Exception),
                        ParameterInt32
                    )
                ),
                ParameterInt32
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.TryFault(
                    ParameterInt32,
                    Expression.AddAssign(ParameterInt32,ParameterInt32)
                ),
                ParameterInt32
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.TryFault(
                    ParameterInt32,
                    ParameterInt32
                ),
                ParameterInt32
            )
        );
    }
    private void 共通Unary(ExpressionType NodeType){
        var ParameterInt32=Expression.Parameter(typeof(int),"int32");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.MakeUnary(NodeType,ParameterInt32,typeof(int)),
                ParameterInt32
            )
        );
    }
    [Fact]
    public void PostDecrementAssign()=>this.共通Unary(ExpressionType.PostDecrementAssign);
    [Fact]
    public void PostIncrementAssign()=>this.共通Unary(ExpressionType.PostIncrementAssign);
    [Fact]
    public void PreDecrementAssign()=>this.共通Unary(ExpressionType.PreDecrementAssign);
    [Fact]
    public void PreIncrementAssign()=>this.共通Unary(ExpressionType.PreIncrementAssign);
    [Fact]
    public void Lambda()=>this.ExpressionAssertEqual(Expression.Lambda<Action>(Expression.Default(typeof(void))));
    [Fact]
    public void Not(){
        //this.共通Unary(ExpressionType.Not);
        //if(Unary1_Operand.NodeType==ExpressionType.Not)return ((UnaryExpression)Unary1_Operand).Operand;
        //if(Unary0_Operand==Unary1_Operand)return Unary0;
        var ParameterInt32=Expression.Parameter(typeof(int),"int32");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.Not(Expression.Not(ParameterInt32)),
                ParameterInt32
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.Not(Expression.Not(Expression.Not(ParameterInt32))),
                ParameterInt32
            )
        );
    }
    [Fact]
    public void MakeAssign()=>this.共通BinaryAssign(ExpressionType.AddAssign);
    [Fact]
    public void 共通ConvertConvertChecked(){
        var @sbyte  =(sbyte  ) 1;
        var @short  =(short  ) 2;
        var @int    =(int    ) 3;
        var @long   =(long   ) 4;
        var @nint   =(nint   ) 5;
        var @byte   =(byte   ) 6;
        var @ushort =(ushort ) 7;
        var @uint   =(uint   ) 8;
        var @ulong  =(ulong  ) 9;
        var @nuint  =(nuint  )10;
        var @float  =(float  )11;
        var @double =(double )12;
        var @decimal=(decimal)13;
        var @char   =(char   )14;
        C(@sbyte,typeof(sbyte));
        C(@sbyte,typeof(short));
        C(@sbyte,typeof(int));
        C(@sbyte,typeof(long));
        //C(@sbyte,typeof(nint));
        C(@sbyte,typeof(byte));
        C(@sbyte,typeof(ushort));
        C(@sbyte,typeof(uint));
        C(@sbyte,typeof(ulong));
        //C(@sbyte,typeof(nuint));
        C(@sbyte,typeof(float));
        C(@sbyte,typeof(double));
        C(@sbyte,typeof(decimal));
        C(@sbyte,typeof(char));
        C(@short,typeof(sbyte));
        C(@short,typeof(short));
        C(@short,typeof(int));
        C(@short,typeof(long));
        //C(@short,typeof(nint));
        C(@short,typeof(byte));
        C(@short,typeof(ushort));
        C(@short,typeof(uint));
        C(@short,typeof(ulong));
        //C(@short,typeof(nuint));
        C(@short,typeof(float));
        C(@short,typeof(double));
        C(@short,typeof(decimal));
        C(@short,typeof(char));
        C(@int,typeof(sbyte));
        C(@int,typeof(short));
        C(@int,typeof(int));
        C(@int,typeof(long));
        C(@int,typeof(nint));
        C(@int,typeof(byte));
        C(@int,typeof(ushort));
        C(@int,typeof(uint));
        C(@int,typeof(ulong));
        //C(@int,typeof(nuint));
        C(@int,typeof(float));
        C(@int,typeof(double));
        C(@int,typeof(decimal));
        C(@int,typeof(char));
        C(@long,typeof(sbyte));
        C(@long,typeof(short));
        C(@long,typeof(int));
        C(@long,typeof(long));
        C(@long,typeof(nint));
        C(@long,typeof(byte));
        C(@long,typeof(ushort));
        C(@long,typeof(uint));
        C(@long,typeof(ulong));
        //C(@long,typeof(nuint));
        C(@long,typeof(float));
        C(@long,typeof(double));
        C(@long,typeof(decimal));
        C(@long,typeof(char));
        //C(@nint,typeof(sbyte));
        //C(@nint,typeof(short));
        C(@nint,typeof(int));
        C(@nint,typeof(long));
        C(@nint,typeof(nint));
        //C(@nint,typeof(byte));
        //C(@nint,typeof(ushort));
        //C(@nint,typeof(uint));
        //C(@nint,typeof(ulong));
        //C(@nint,typeof(nuint));
        //C(@nint,typeof(float));
        //C(@nint,typeof(double));
        //C(@nint,typeof(decimal));
        //C(@nint,typeof(char));
        C(@byte,typeof(sbyte));
        C(@byte,typeof(short));
        C(@byte,typeof(int));
        C(@byte,typeof(long));
        //C(@byte,typeof(nint));
        C(@byte,typeof(byte));
        C(@byte,typeof(ushort));
        C(@byte,typeof(uint));
        C(@byte,typeof(ulong));
        //C(@byte,typeof(nuint));
        C(@byte,typeof(float));
        C(@byte,typeof(double));
        C(@byte,typeof(decimal));
        C(@byte,typeof(char));
        C(@ushort,typeof(sbyte));
        C(@ushort,typeof(short));
        C(@ushort,typeof(int));
        C(@ushort,typeof(long));
        //C(@ushort,typeof(nint));
        C(@ushort,typeof(byte));
        C(@ushort,typeof(ushort));
        C(@ushort,typeof(uint));
        C(@ushort,typeof(ulong));
        //C(@ushort,typeof(nuint));
        C(@ushort,typeof(float));
        C(@ushort,typeof(double));
        C(@ushort,typeof(decimal));
        C(@ushort,typeof(char));
        C(@uint,typeof(sbyte));
        C(@uint,typeof(short));
        C(@uint,typeof(int));
        C(@uint,typeof(long));
        //C(@uint,typeof(nint));
        C(@uint,typeof(byte));
        C(@uint,typeof(ushort));
        C(@uint,typeof(uint));
        C(@uint,typeof(ulong));
        C(@uint,typeof(nuint));
        C(@uint,typeof(float));
        C(@uint,typeof(double));
        C(@uint,typeof(decimal));
        C(@uint,typeof(char));
        C(@ulong,typeof(sbyte));
        C(@ulong,typeof(short));
        C(@ulong,typeof(int));
        C(@ulong,typeof(long));
        //C(@ulong,typeof(nint));
        C(@ulong,typeof(byte));
        C(@ulong,typeof(ushort));
        C(@ulong,typeof(uint));
        C(@ulong,typeof(ulong));
        C(@ulong,typeof(nuint));
        C(@ulong,typeof(float));
        C(@ulong,typeof(double));
        C(@ulong,typeof(decimal));
        C(@ulong,typeof(char));
        //C(@nuint,typeof(sbyte));
        //C(@nuint,typeof(short));
        //C(@nuint,typeof(int));
        //C(@nuint,typeof(long));
        //C(@nuint,typeof(nint));
        //C(@nuint,typeof(byte));
        //C(@nuint,typeof(ushort));
        C(@nuint,typeof(uint));
        C(@nuint,typeof(ulong));
        C(@nuint,typeof(nuint));
        //C(@nuint,typeof(float));
        //C(@nuint,typeof(double));
        //C(@nuint,typeof(decimal));
        //C(@nuint,typeof(char));
        C(@float,typeof(sbyte));
        C(@float,typeof(short));
        C(@float,typeof(int));
        C(@float,typeof(long));
        //C(@float,typeof(nint));
        C(@float,typeof(byte));
        C(@float,typeof(ushort));
        C(@float,typeof(uint));
        C(@float,typeof(ulong));
        //C(@float,typeof(nuint));
        C(@float,typeof(float));
        C(@float,typeof(double));
        C(@float,typeof(decimal));
        C(@float,typeof(char));
        C(@double,typeof(sbyte));
        C(@double,typeof(short));
        C(@double,typeof(int));
        C(@double,typeof(long));
        //C(@double,typeof(nint));
        C(@double,typeof(byte));
        C(@double,typeof(ushort));
        C(@double,typeof(uint));
        C(@double,typeof(ulong));
        //C(@double,typeof(nuint));
        C(@double,typeof(float));
        C(@double,typeof(double));
        C(@double,typeof(decimal));
        C(@double,typeof(char));
        C(@decimal,typeof(sbyte));
        C(@decimal,typeof(short));
        C(@decimal,typeof(int));
        C(@decimal,typeof(long));
        //C(@decimal,typeof(nint));
        C(@decimal,typeof(byte));
        C(@decimal,typeof(ushort));
        C(@decimal,typeof(uint));
        C(@decimal,typeof(ulong));
        //C(@decimal,typeof(nuint));
        C(@decimal,typeof(float));
        C(@decimal,typeof(double));
        C(@decimal,typeof(decimal));
        C(@decimal,typeof(char));
        C(@char,typeof(sbyte));
        C(@char,typeof(short));
        C(@char,typeof(int));
        C(@char,typeof(long));
        //C(@char,typeof(nint));
        C(@char,typeof(byte));
        C(@char,typeof(ushort));
        C(@char,typeof(uint));
        C(@char,typeof(ulong));
        //C(@char,typeof(nuint));
        C(@char,typeof(float));
        C(@char,typeof(double));
        C(@char,typeof(decimal));
        C(@char,typeof(char));

        //var s=new object[]{@sbyte,@short,@int,@long,@nint,@byte,@ushort,@uint,@ulong,@nuint,@float,@double,@decimal,@char};
        //foreach(var a in s){
        //    foreach(var b in s){
        //        try{
        //            Expression.Convert(Expression.Constant(a),b.GetType());
        //            Debug.Print($"C(@{a.GetType().Name},typeof({b.GetType().Name}));");
        //        } catch(InvalidOperationException){
        //            Debug.Print($"//C(@{a.GetType().Name},typeof({b.GetType().Name}));");
        //        }
        //    }
        //}
        ////共通(Expression.Lambda(Expression.Convert(Expression.Constant(a),b.GetType())));
        //this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(Expression.Lambda<Func<nuint>>(Expression.Convert(Expression.Constant(1 ,typeof(int  )),typeof(nuint))));
        //this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(Expression.Lambda<Func<nint >>(Expression.Convert(Expression.Constant(1L,typeof(long )),typeof(nint ))));
        //this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(Expression.Lambda<Func<nuint>>(Expression.Convert(Expression.Constant(1L,typeof(long )),typeof(nuint))));
        //this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(Expression.Lambda<Func<int  >>(Expression.Convert(Expression.Constant(1 ,typeof(nint )),typeof(int  ))));
        //this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(Expression.Lambda<Func<long >>(Expression.Convert(Expression.Constant(1 ,typeof(nint )),typeof(long ))));
        //this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(Expression.Lambda<Func<uint >>(Expression.Convert(Expression.Constant(1 ,typeof(nuint)),typeof(uint ))));
        //this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(Expression.Lambda<Func<ulong>>(Expression.Convert(Expression.Constant(1 ,typeof(nuint)),typeof(ulong))));
        //this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(Expression.Lambda<Func<nint >>(Expression.Convert(Expression.Constant(1 ,typeof(int  )),typeof(nint ))));
        //this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(Expression.Lambda<Func<nint >>(Expression.Convert(Expression.Constant(1 ,typeof(int  )),typeof(nint ))));
        //var p=Expression.Parameter(typeof(int));
        //this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(
        //    Expression.Lambda<Func<int,int>>(
        //        Expression.Convert(
        //            Expression.AddAssign(p,p),
        //            typeof(int)),
        //        p
        //    ),1
        //);
        //C(@sbyte,typeof(sbyte));
        void C<T>(T input,Type type)=>共通(Expression.Lambda(Expression.Convert(Expression.Constant(input),type)));
        void 共通(LambdaExpression input){
            var Optimizer=this.Optimizer;
            var 標準=input.Compile();
            var expected0=標準.DynamicInvoke();
            Optimizer.IsInline=true;
            var expected2=Optimizer.CreateDelegate(input).DynamicInvoke();
            Assert.Equal(expected0,expected2);
        }
    }
    [Fact]public void Convert(){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<sbyte>>(
                Expression.Convert(
                    Expression.Constant(1000),
                    typeof(sbyte)
                )
            )
        );
    }
    [Fact]public void ConvertChecked(){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<sbyte>>(
                Expression.ConvertChecked(
                    Expression.Constant(100),
                    typeof(sbyte)
                )
            )
        );
    }
    [Fact]public void Call_Average(){
        var s=new int[10];
        //if(Reflection.ExtensionEnumerable.AverageDecimal==MethodCall0_GenericMethodDefinition)
        this.Expression実行AssertEqual(()=>s.Select(p=>(decimal)p).Average());
        //if(Reflection.ExtensionEnumerable.AverageDouble==MethodCall0_GenericMethodDefinition)
        this.Expression実行AssertEqual(()=>s.Select(p=>(double)p).Average());
        //if(Reflection.ExtensionEnumerable.AverageNullableDecimal==MethodCall0_GenericMethodDefinition)
        this.Expression実行AssertEqual(()=>s.Select(p=>(decimal?)p).Average());
        //if(Reflection.ExtensionEnumerable.AverageNullableDouble==MethodCall0_GenericMethodDefinition)
        this.Expression実行AssertEqual(()=>s.Select(p=>(double?)p).Average());
        this.Expression実行AssertEqual(()=>new Set<int>{1}.Select(p=>(decimal)p).Average());
    }
    private static System.Collections.Generic.IEnumerable<T> そのまま<T>(System.Collections.Generic.IEnumerable<T> i)=>i;
    [Fact]public void Call_Any(){
        var s=new int[10];
        this.Expression実行AssertEqual(()=>s.Select(p=>p+p).Any());
        this.Expression実行AssertEqual(()=>s.GroupJoin(s,o=>o,i=>i,(o,i)=>new{o,i}).Any());
        this.Expression実行AssertEqual(()=>そのまま(s).Any());
        this.Expression実行AssertEqual(()=>s.Except(s).Any());
        this.Expression実行AssertEqual(()=>s.Any(p=>p==0));
    }
    [Fact]public void Call_Contains(){
        this.Expression実行AssertEqual(()=>new int[10].Contains(0));
        this.Expression実行AssertEqual(()=>((System.Collections.Generic.IEnumerable<int>)new List<int>()).Contains(0));
        this.Expression実行AssertEqual(()=>new decimal[10].Contains(0));
        this.Expression実行AssertEqual(()=>new decimal[10].Select(p=>(object)p).Contains(0m));
    }
    [Fact]public void Call_GroupBy0(){
        this.Expression実行AssertEqual(()=>new int[10].GroupBy(p=>new{p},(key,g)=>key.p+g.Count()));
        this.Expression実行AssertEqual(()=>new int[10].GroupBy(p=>(decimal)p,(key,g)=>key+g.Count(),EqualityComparer<decimal>.Default));
    }
    [Fact]public void Call_GroupBy1(){
        this.Expression実行AssertEqual(()=>new Set<int>().GroupBy(p=>new{p},(key,g)=>key.p+g.Count()));
        this.Expression実行AssertEqual(()=>new int[10].GroupBy(p=>new{p},p=>p+p,(key,g)=>key.p+g.Count()));
        this.Expression実行AssertEqual(()=>new int[10].GroupBy(p=>(decimal)p,p=>p+p,(key,g)=>key+g.Count(),EqualityComparer<decimal>.Default));
        this.Expression実行AssertEqual(()=>new Set<int>().GroupBy(p=>new{p},p=>p+p,(key,g)=>key.p+g.Count()));
    }
    [Fact]public void Call_GroupBy2(){
        this.Expression実行AssertEqual(()=>new int[10].GroupBy(p=>new{p}));
        this.Expression実行AssertEqual(()=>new int[10].GroupBy(p=>(decimal)p,EqualityComparer<decimal>.Default));
        this.Expression実行AssertEqual(()=>new Set<int>().GroupBy(p=>new{p}));
        this.Expression実行AssertEqual(()=>new Set<int>().GroupBy(p=>new{p},p=>p*p));
    }
    [Fact]public void Call_GroupBy_GroupBy_keySelector_resultSelector(){
        //if(MethodCall1_Arguments.Count==3) {
        this.Expression実行AssertEqual(()=>new int[10].GroupBy(p=>new{p},(key,g)=>key.p+g.Count()));
        //} else {
        this.Expression実行AssertEqual(()=>new int[10].GroupBy(p=>(decimal)p,(key,g)=>key+g.Count(),EqualityComparer<decimal>.Default));
        //}
        //if(MethodCall1_Arguments_2 is LambdaExpression resultSelector) {
        this.Expression実行AssertEqual(()=>new int[10].GroupBy(p=>new{p},(key,g)=>key.p+g.Count()));
        //} else {
        this.Expression実行AssertEqual(()=>new int[10].GroupBy(p=>p,Anonymous((int key,System.Collections.Generic.IEnumerable<int>g)=>key+g.Count())));
        //}
    }
    [Fact]public void Call_GroupBy_GroupBy_keySelector_elementSelector_resultSelector(){
        //if(MethodCall1_Arguments.Count==4) {
        this.Expression実行AssertEqual(()=>new int[10].GroupBy(p=>new{p},p=>p+p,(key,g)=>key.p+g.Count()));
        //} else {
        this.Expression実行AssertEqual(()=>new int[10].GroupBy(p=>(decimal)p,p=>p+p,(key,g)=>key+g.Count(),EqualityComparer<decimal>.Default));
        //}
        //if(MethodCall1_Arguments_2 is LambdaExpression resultSelector) {
        this.Expression実行AssertEqual(()=>new int[10].GroupBy(p=>new{p},p=>p+p,(key,g)=>key.p+g.Count()));
        //} else {
        this.Expression実行AssertEqual(()=>new int[10].GroupBy(p=>p,p=>p+p,Anonymous((int key,System.Collections.Generic.IEnumerable<int>g)=>key+g.Count())));
        //}
    }
    [Fact]public void Call_GroupBy_GroupBy_keySelector(){
        //if(MethodCall1_Arguments.Count==2) {
        this.Expression実行AssertEqual(()=>new int[10].GroupBy(p=>new{p}));
        //} else {
        this.Expression実行AssertEqual(()=>new int[10].GroupBy(p=>(decimal)p));
        //}
    }
    [Fact]public void Call_GroupJoin(){
        this.Expression実行AssertEqual(()=>new int[10].GroupJoin(new int[10],o=>o,i=>i,(o,i)=>new{o,i},EqualityComparer<int>.Default));
        this.Expression実行AssertEqual(()=>new int[10].GroupJoin(new int[10],o=>o,i=>i,(o,i)=>new{o,i}));
        this.Expression実行AssertEqual(()=>new int[10].GroupJoin(new int[10],(Func<int,int>)(o=>o),i=>i,(o,i)=>new{o,i}));
        this.Expression実行AssertEqual(()=>new int[10].GroupJoin(new int[10],o=>o,(Func<int,int>)(i=>i),(o,i)=>new{o,i}));
        this.Expression実行AssertEqual(()=>new Set<int>().GroupJoin(new Set<int>(),o=>o,i=>i,(o,i)=>new{o,i}));
        this.Expression実行AssertEqual(()=>new Set<int>().GroupJoin(new Set<int>(),(Func<int,int>)(o=>o),i=>i,(o,i)=>new{o,i}));
        this.Expression実行AssertEqual(()=>new Set<int>().GroupJoin(new Set<int>(),o=>o,(Func<int,int>)(i=>i),(o,i)=>new{o,i}));
        this.Expression実行AssertEqual(()=>new int[10].GroupJoin(new int[10],o=>o,i=>i,(Func<int,System.Collections.Generic.IEnumerable<int>,int>)((o,i)=>o+i.Count())));
    }
    private static Set<int>CreateSet()=>new();
    [Fact]public void Call_Intersect(){
        this.Expression実行AssertEqual(()=>CreateSet().Intersect(CreateSet()).Where(p=>p==0));
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet().SelectMany(i=>CreateSet())).Intersect(CreateSet()));
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet()).Intersect(CreateSet()));
        this.Expression実行AssertEqual(()=>CreateSet().Intersect(CreateSet().Where(p=>true)));
        this.Expression実行AssertEqual(()=>CreateSet().Intersect(CreateSet(),EqualityComparer<int>.Default));
    }
    private static List<int>CreateEnum()=>new();
    [Fact]public void Call_Join(){
        this.Expression実行AssertEqual(()=>CreateSet().Join(CreateSet(),o=>o,i=>i,(o,i)=>new{o,i}));
        this.Expression実行AssertEqual(()=>CreateEnum().Join(CreateEnum(),o=>o,i=>i,(o,i)=>new{o,i}));
        this.Expression実行AssertEqual(()=>CreateSet().Join(CreateSet(),o=>o,i=>i,(Func<int,int,int>)((o,i)=>o+i)));
        this.Expression実行AssertEqual(()=>CreateSet().Join(CreateSet(),o=>o,(Func<int,int>)(i=>i),(o,i)=>new{o,i}));
        this.Expression実行AssertEqual(()=>CreateSet().Join(CreateSet(),o=>o,(Func<int,int>)(i=>i),(Func<int,int,int>)((o,i)=>o+i)));
        this.Expression実行AssertEqual(()=>CreateSet().Join(CreateSet(),(Func<int,int>)(o=>o),i=>i,(o,i)=>new{o,i}));
        this.Expression実行AssertEqual(()=>CreateSet().Join(CreateSet(),(Func<int,int>)(o=>o),i=>i,(Func<int,int,int>)((o,i)=>o+i)));
        this.Expression実行AssertEqual(()=>CreateSet().Join(CreateSet(),(Func<int,int>)(o=>o),(Func<int,int>)(i=>i),(o,i)=>new{o,i}));
        this.Expression実行AssertEqual(()=>CreateSet().Join(CreateSet(),(Func<int,int>)(o=>o),(Func<int,int>)(i=>i),(Func<int,int,int>)((o,i)=>o+i)));
        this.Expression実行AssertEqual(()=>CreateSet().Join(CreateSet(),(Func<int,int>)(o=>o),(Func<int,int>)(i=>i),(Func<int,int,int>)((o,i)=>o+i),EqualityComparer<int>.Default));
    }
    [Fact]public void Call_OfType(){
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet()).OfType<string>());
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet().SelectMany(i=>CreateSet())).OfType<string>());
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(p=>CreateSet()).OfType<string>());
        this.Expression実行AssertEqual(()=>new object[]{"ABC",1,3.0}.OfType<string>());
        this.Expression実行AssertEqual(()=>new string[]{"A","B","C"}.OfType<object>());
    }
    [Fact]public void Call_Select(){
        //this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(()=>CreateSet().SelectMany(o=>CreateSet()).Select(p=>p+p));
        //this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(()=>CreateSet().SelectMany((o,index)=>CreateSet().SelectMany(i=>CreateSet())).Select(p=>p+p));
        //this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(()=>CreateSet().SelectMany(o=>CreateSet().SelectMany(i=>CreateSet())).Select(p=>p+p));
        //this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(()=>CreateSet().SelectMany((o,index)=>CreateSet()).Select(p=>p+p));
        //if(Reflection.ExtensionEnumerable.Select_indexSelector!=MethodCall0_GenericMethodDefinition) {
        //    if(ループ展開可能メソッドか(MethodCall1_Arguments_0,out var MethodCall1_MethodCall)) {
        //        switch(MethodCall1_MethodCall_Method.Name) {
        //            case nameof(Enumerable.SelectMany): {
        //                if(Reflection.ExtensionEnumerable.SelectMany_indexSelector!=MethodCall1_MethodCall.Method.GetGenericMethodDefinition()){
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet()).Select(p=>p+p));
        //                }
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany((o,index)=>CreateSet().SelectMany(i=>CreateSet())).Select(p=>p+p));
        //            }
        //            case nameof(Enumerable.Select): {
        //                if(MethodCall1_MethodCall_Arguments_1 is LambdaExpression selector1) {
        //                    if(MethodCall1_Arguments_1 is LambdaExpression selector0) {
        this.Expression実行AssertEqual(()=>CreateSet().Select(p=>p+p).Select(p=>p+p));
        //                                } else {
        this.Expression実行AssertEqual(()=>CreateSet().Select(p=>p+p).Select(Anonymous((int p)=>p+p)));
        //                    }
        //                } else {
        //                    if(MethodCall1_Arguments_1 is LambdaExpression selector0) {
        this.Expression実行AssertEqual(()=>CreateSet().Select(Anonymous((int p)=>p-p)).Select(p=>p+p));
        //                                } else {
        this.Expression実行AssertEqual(()=>CreateSet().Select(Anonymous((int p)=>p-p)).Select(Anonymous((int p)=>p+p)));
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    if(MethodCall1_Arguments_1 is LambdaExpression MethodCall1_selector&&MethodCall1_selector.Parameters[0]==MethodCall1_selector.Body) {
        this.Expression実行AssertEqual(()=>CreateSet().Select(p=>p));
        this.Expression実行AssertEqual(()=>CreateSet().Select(Anonymous((int p)=>p)));
        this.Expression実行AssertEqual(()=>CreateSet().Select(p=>p+p));
        //}
        this.Expression実行AssertEqual(()=>CreateSet().Select((p,index)=>p+index));
    }
    [Fact]public void Call_Single(){
        this.Expression実行AssertEqual(()=>new int[1].Single(p=>true));
        this.Expression実行AssertEqual(()=>new int[1].Single());
    }
    [Fact]public void Call_SingleDefault(){
        this.Expression実行AssertEqual(()=>new int[1].SingleOrDefault(p=>true));
        this.Expression実行AssertEqual(()=>new int[1].SingleOrDefault(p=>true,1));
        this.Expression実行AssertEqual(()=>new int[1].SingleOrDefault());
    }
    [Fact]public void Call_ToArray(){
        this.Expression実行AssertEqual(()=>new int[1].ToArray().ToArray());
        this.Expression実行AssertEqual(()=>((System.Collections.Generic.IEnumerable<int>)new int[1]).ToArray());
    }
    [Fact]public void Call_Except0(){
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet()).Except(CreateSet().Select(p=>p*p),EqualityComparer<int>.Default));
    }
    [Fact]public void Call_Except1(){
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet()).Except(CreateSet(),EqualityComparer<int>.Default));
    }
    [Fact]public void Call_Except2(){
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet()).Except(CreateSet()));
    }
    static Set<int> CreateSet0()=>CreateSet();
    static Set<int> CreateSet1()=>CreateSet();
    [Fact]public void Call_Union(){
        //CommonLibrary.
        //CommonLibrary.IsImplement()
        var x=typeof(Set<int>).IsInheritInterface(typeof(LinqDB.Sets.IEnumerable<>));
            //.GetInterfaces().GetInterface(typeof(LinqDB.Sets.IEnumerable<int>).FullName);

        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet0()).Union(CreateSet1()));
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet()).Union(CreateEnum()));
        this.Expression実行AssertEqual(()=>CreateEnum().SelectMany(o=>CreateEnum()).Union(CreateSet()));
        this.Expression実行AssertEqual(()=>CreateEnum().SelectMany(o=>CreateSet()).Union(CreateEnum()));
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateEnum()).Union(CreateSet()));
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet()).Union(CreateSet(),EqualityComparer<int>.Default));
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet()).Union(CreateSet().Select(p=>p*p),EqualityComparer<int>.Default));
    }
    [Fact]public void Call_UnionBy(){
        var a=new[]{3,5,7};
        var b=new[]{4,6,8};
        this.Expression実行AssertEqual(()=>CreateSet().UnionBy(CreateSet(),o=>o+1));
        this.Expression実行AssertEqual(()=>CreateSet().UnionBy(CreateSet(),o=>o+1,EqualityComparer<int>.Default));
    }
    [Fact]public void Call_UnionBy_Anonymous(){
        var a=new[]{3,5,7};
        var b=new[]{4,6,8};
        this.Expression実行AssertEqual(()=>new{a=CreateSet().UnionBy(CreateSet(),o=>o+1)});
        this.Expression実行AssertEqual(()=>new{a=CreateSet().UnionBy(CreateSet(),o=>o+1,EqualityComparer<int>.Default) });
    }
    static Func<TO,TResult> Anonymous<TO,TResult>(Func<TO,TResult> i)=>i;
    static Func<TO,T1,TResult> Anonymous<TO,T1,TResult>(Func<TO,T1,TResult> i)=>i;
    [Fact]public void Call_SelectMany(){
        //if(MethodCall0_Arguments.Count==2) {
        //    if(SelectMany is not null)
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet()).SelectMany(i=>CreateSet()));
        //this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(()=>CreateSet().SelectMany(o=>CreateSet()).SelectMany(i=>CreateSet()));
        //    if(MethodCall1_Arguments_1 is LambdaExpression selector&&ループ展開可能メソッドか(selector.Body,out _)) {
        //        if(OuterPredicate is not null) {
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet().Where(i=>o==0)));
        //        }
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet().Where(i=>i==0)));
        //    } else {
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet().ToArray()));
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(Anonymous((int o)=>CreateSet())));
        //} else {
        //    if(Reflection.ExtensionEnumerable.SelectMany_collectionSelector_resultSelector==MethodCall0_GenericMethodDefinition) {
        this.Expression実行AssertEqual(()=>CreateEnum().SelectMany(o=>CreateEnum(),(o,i)=>o+i));
        //    } else if(Reflection.ExtensionEnumerable.SelectMany_indexCollectionSelector_resultSelector==MethodCall0_GenericMethodDefinition) {
        this.Expression実行AssertEqual(()=>CreateEnum().SelectMany((o,index)=>CreateEnum(),(o,i)=>o+i));
        //    }else{
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet(),(o,i)=>o+i));
        //    if(MethodCall1_Arguments_1 is LambdaExpression collectionSelector) {
        //        if(MethodCall1_Arguments_2 is LambdaExpression resultSelector) {
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet(),(o,i)=>o*i));
        //        }else{
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet(),Anonymous((int o,int i)=>o*i)));
        //    }else{
        //        if(MethodCall1_Arguments_2 is LambdaExpression resultSelector) {
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(Anonymous<int,Set<int>>(o=>CreateSet()),(o,i)=>o*i));
        //        }else{
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(Anonymous<int,Set<int>>(o=>CreateSet()),Anonymous((int o,int i)=>o*i)));
        //        if(indexSelectorか) {
        this.Expression実行AssertEqual(()=>CreateEnum().SelectMany((Func<int,int,System.Collections.Generic.IEnumerable<int>>)((o,index)=>CreateEnum()),Anonymous((int o,int i)=>o*i)));
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany((Func<int,System.Collections.Generic.IEnumerable<int>>)(o=>CreateSet()),(o,i)=>o+i));
    }
    [Fact]public void Call_SelectMany_共通(){
        //if(ループ展開可能メソッドか(InputBody,out var MethodCall)) {
        //    switch(MethodCall_Method.Name) {
        //        case nameof(Enumerable.Where): {
        //            if(MethodCall.Arguments[1] is LambdaExpression predicate) {
        //                if(OuterPredicate is not null) {
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet().Where(i=>o==0)));
        //                }
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet().Where(i=>i==0)));
        //                if(OtherPredicate is not null) {
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet().Where(i=>i==0&&o==3)));
        //                }
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet().Where(i=>o==0)));
        //            }
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet().Where(Anonymous((int i)=>i==0))));
        //        }
        //        default: {
        //            for(var a = 1;a<MethodCall.Arguments.Count;a++) {
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet().Select(i=>o==0)));
        //            }
        //            if(ループ展開可能メソッドか(MethodCall.Arguments[0],out var MethodCall2)){
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet().Where(i=>o==9).Select(i=>o==0)));
        //            }else{
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet().OfType<object>()));
        //            }
        //        }
        //    }
        //}
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet().Select(p=>p+1)));
    }
    [Fact]public void Call_Where(){
        //if(Reflection.ExtensionEnumerable.Where_index=MethodCall0_GenericMethodDefinition)break;
        //if(ループ展開可能メソッドか(MethodCall1_Arguments_0,out var MethodCall1_MethodCall)) {
        //    switch(MethodCall1_MethodCall_Method.Name) {
        //        case nameof(ExtensionSet.Except):
        this.Expression実行AssertEqual(() => CreateSet().Except(CreateSet()).Where(p => true));
        //        case nameof(ExtensionSet.Intersect):
        this.Expression実行AssertEqual(() => CreateSet().Intersect(CreateSet()).Where(p => true));
        //        case nameof(ExtensionSet.Union):
        this.Expression実行AssertEqual(() => CreateSet().Union(CreateSet()).Where(p => true));
        //        case nameof(ExtensionSet.Select): {
        //            if(MethodCall1_Arguments_1 is LambdaExpression predicate) {
        //                if(MethodCall1_MethodCall.Arguments[1] is LambdaExpression selector) {
        this.Expression実行AssertEqual(()=>CreateSet().Select(p=>new{p}).Where(p=>true));
        //                }
        this.Expression実行AssertEqual(()=>CreateSet().Select(Anonymous((int p)=>new{p})).Where(p=>true));
        //            }
        this.Expression実行AssertEqual(()=>CreateSet().Select(p=>p*p).Where(Anonymous((int p)=>true)));
        //        case nameof(ExtensionSet.SelectMany): {
        //            if(Reflection.ExtensionEnumerable.SelectMany_indexSelector!=MethodCall1_MethodCall.Method.GetGenericMethodDefinition()) {
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(p=>CreateSet()).Where(p=>p==4));
        //            }
        this.Expression実行AssertEqual(()=>CreateEnum().SelectMany((o,index)=>CreateEnum(),(o,i)=>o+i).Where(p=>p==4));
        //        case nameof(ExtensionSet.Where): {
        //            if(Reflection.ExtensionEnumerable.Where_index==MethodCall1_MethodCall_Method.GetGenericMethodDefinition())break;
        this.Expression実行AssertEqual(()=>CreateSet().Where((int p,int index)=>p==1).Where(p=>p==1));
        //            if(MethodCall1_Arguments_1 is LambdaExpression predicate外) {
        //                if(MethodCall1_MethodCall0_Arguments[1]is LambdaExpression predicate内) {
        this.Expression実行AssertEqual(()=>CreateSet().Where(p=>p==1).Where(p=>p==2));
        //                }else{
        this.Expression実行AssertEqual(()=>CreateSet().Where(Anonymous((int p)=>p==1)).Where(p=>p==2));
        //                }
        //            }else{
        //                if(MethodCall1_MethodCall0_Arguments[1]is LambdaExpression predicate内) {
        this.Expression実行AssertEqual(()=>CreateSet().Where(p=>p==1).Where(Anonymous((int p)=>p==2)));
        //                }else{
        this.Expression実行AssertEqual(()=>CreateSet().Where(Anonymous((int p)=>p==1)).Where(Anonymous((int p)=>p==2)));
        //                }
        //            }
        //        }
    }
    class Base{
        public virtual string M()=>"A";
    }
    class Derived0:Base{
        public override string M()=>"A";
    }
    sealed class IsSealed:Base{
        public override string M()=>"A";
    }
    class IsFinal:Base{
        public sealed override string M()=>"A";
    }
    sealed class IsSealedIsFinal:Base{
        public sealed override string M()=>"A";
    }
    [Fact]
    public void x(){
        this.Expression実行AssertEqual(()=>new ValueTuple<int,int>(1,2).ToString());
    }
    [Fact]
    public void Call(){
        //if(MethodCall0_Method.IsStatic){
        //}else{
        //    if(IsAnonymous||IsValueTuple) {
        //        if(IsAnonymous) {
        //            if(Reflection.Object.Equals_==MethodCall0_Method.GetBaseDefinition())
        this.Expression実行AssertEqual(()=>new{a="a",b="b"}.Equals(new{a="a",b="b"}));
        this.Expression実行AssertEqual(()=>new{a="a",b="b"}.ToString());
        //        } else {
        //            Debug.Assert(IsValueTuple);
        //            if(MethodCall1_Object_Type.GetInterface(CommonLibrary.IEquatable_FullName) is not null)
        this.Expression実行AssertEqual(()=>new ValueTuple<int,int>(1,2).Equals(new ValueTuple<int,int>(1,2)));
        this.Expression実行AssertEqual(()=>new ValueTuple<int,int>(1,2).ToString());
        //        }
        //    }
        //    if(MethodCall1_Object_Type.IsAnonymousValueTuple()) {
        //        if(Reflection.Object.Equals_==MethodCall0_Method.GetBaseDefinition()){
        //            if(MethodCall1_Object is NewExpression LNew&&MethodCall1_Arguments_0 is NewExpression RNew) {
        //                for(var a = 1;a<LNew_Arguments_Count;a++) {
        // ReSharper disable once EqualExpressionComparison
        this.Expression実行AssertEqual(() => new { a = "a",b = "b" }.Equals(new { a = "a",b = "b" }));
        this.Expression実行AssertEqual(() => new ValueTuple<int,int>(1,2).Equals(new ValueTuple<int,int>(1,2)));
        //            }
        this.Expression実行AssertEqual(() => new ValueTuple<int,int>(1,2).Equals(F(new ValueTuple<int,int>(1,2))));
        ////        }
        //    }
        //}
        //    foreach(var ChildMethod in MethodCall1_Object_Type.GetMethods(BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.Public)) {
        //        if((ChildMethod.IsFinal||MethodCall1_Object_Type.IsSealed)&&ChildMethod.GetBaseDefinition()==MethodCall0_Method) {
        this.Expression実行AssertEqual(()=>new Derived0().M());
        this.Expression実行AssertEqual(()=>new IsSealed().M());
        this.Expression実行AssertEqual(()=>new IsFinal().M());
        this.Expression実行AssertEqual(()=>new IsSealedIsFinal().M());
    }
    //[Fact]public void Call_Anonymous最適化(){
    //    //if(Member0_Expression is null)return Member0;
    //    this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(()=>CreateSet().Select(p=>StaticProperty));
    //    //if(Member0_Expression.Type.IsAnonymous()){
    //    //    if(Member1_Expression is NewExpression New1) {
    //    //        for(var Index = 0;Index<Parameters_Length;Index++)
    //    //            if(Parameters[Index].Name==Member0_Member_Name)return New1.Arguments[Index];
    //    this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(()=>CreateSet().Select(p=>new{a=p,b=p}.a));
    //    this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(()=>CreateSet().Select(p=>new{a=p,b=p}.b));
    //    //    } else
    //    this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(()=>CreateSet().Select(p=>F(new{p}).p));
    //    //}else if(Member0_Expression.Type.IsValueTuple()){
    //    //    if(Member1_Expression is NewExpression New1) {
    //    //        for(var Index = 0;Index<Parameters_Length;Index++)
    //    this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(()=>CreateSet().Select(p=>new ValueTuple<int,int>(p,p).Item1));
    //    this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(()=>CreateSet().Select(p=>new ValueTuple<int,int>(p,p).Item2));
    //    //    } else
    //    this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(()=>CreateSet().Select(p=>F(new ValueTuple<int,int>(p,p)).Item2));
    //    //}
    //    this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(()=>CreateSet().Select(p=>this.InstanceProperty));
    //}
    [Fact]public void 共通AnonymousValueTuple(){
        //if(MethodCall1_Object is NewExpression LNew&&MethodCall1_Arguments_0 is NewExpression RNew) {
        //    for(var a = 1;a<LNew_Arguments_Count;a++) {
        this.Expression実行AssertEqual(()=>new{a="a",b="b"}.Equals(new{a="a",b="b"}));
        this.Expression実行AssertEqual(()=>new ValueTuple<int,int>(1,2).Equals(new ValueTuple<int,int>(1,2)));
        //    }
        //}
        this.Expression実行AssertEqual(()=>new ValueTuple<int,int>(1,2).Equals(F(new ValueTuple<int,int>(1,2))));
    }
    [Fact]public void 条件が合えば内部SelectManyのselector_Bodyに外部メソッドを入れる0(){
        //if(ループ展開可能メソッドか(MethodCall1_Arguments_0,out var MethodCall1_MethodCall)) {
        //    switch(MethodCall1_MethodCall.Method.Name) {
        //        case nameof(ExtensionSet.SelectMany): {
        //            if(Reflection.ExtensionEnumerable.SelectMany_indexSelector!=MethodCall1_MethodCall.Method.GetGenericMethodDefinition()) {
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(p=>CreateSet()).OfType<string>());
        //            }
        this.Expression実行AssertEqual(()=>CreateEnum().SelectMany((p,index)=>CreateEnum()).OfType<object>());
        //        }
        //    }
        //}
        this.Expression実行AssertEqual(()=>CreateEnum().OfType<object>());
    }
    [Fact]public void 条件が合えば内部SelectManyのselector_Bodyに外部メソッドを入れる1(){
        //if(ループ展開可能メソッドか(MethodCall1_Arguments_0,out var MethodCall1_MethodCall)) {
        //    switch(MethodCall1_MethodCall.Method.Name) {
        //        case nameof(ExtensionSet.SelectMany): {
        //            if(Reflection.ExtensionEnumerable.SelectMany_indexSelector!=MethodCall1_MethodCall.Method.GetGenericMethodDefinition()) {
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(p=>CreateSet()).Except(CreateSet()));
        //            }
        this.Expression実行AssertEqual(()=>CreateEnum().SelectMany((p,index)=>CreateEnum()).Except(CreateEnum()));
        //        }
        //    }
        //}
        this.Expression実行AssertEqual(()=>CreateEnum().Except(CreateEnum()));
    }
    [Fact]public void 条件が合えば内部SelectManyのselector_Bodyに外部メソッドを入れる2(){
        //if(ループ展開可能メソッドか(MethodCall1_Arguments_0,out var MethodCall1_MethodCall)) {
        //    switch(MethodCall1_MethodCall.Method.Name) {
        //        case nameof(ExtensionSet.SelectMany): {
        //            if(Reflection.ExtensionEnumerable.SelectMany_indexSelector!=MethodCall1_MethodCall.Method.GetGenericMethodDefinition()) {
        this.Expression実行AssertEqual(()=>CreateEnum().SelectMany(p=>CreateEnum()).Except(CreateEnum(),EqualityComparer<int>.Default));
        //            }
        this.Expression実行AssertEqual(()=>CreateEnum().SelectMany((p,index)=>CreateEnum()).Except(CreateEnum(),EqualityComparer<int>.Default));
        //        }
        //    }
        //}
        this.Expression実行AssertEqual(()=>CreateEnum().Except(CreateEnum(),EqualityComparer<int>.Default));
    }
    [Fact]public void 内部SelectManyのselector_Bodyに外部メソッドを入れる0(){
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(p=>CreateSet()).OfType<string>());
        this.Expression実行AssertEqual(()=>CreateEnum().SelectMany((p,index)=>CreateEnum()).OfType<object>());
    }
    [Fact]public void 内部SelectManyのselector_Bodyに外部メソッドを入れる1(){
        //if(MethodCall1_MethodCall_Arguments[1] is LambdaExpression selector0) {
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(p=>CreateSet()).Except(CreateSet()));
        //} else {
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(Anonymous((int p)=>CreateSet())).Except(CreateSet()));
        //}
    }
    [Fact]public void 内部SelectManyのselector_Bodyに外部メソッドを入れる2(){
        //if(MethodCall1_MethodCall_Arguments[1] is LambdaExpression selector0) {
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(p=>CreateSet()).Except(CreateSet(),EqualityComparer<int>.Default));
        //} else {
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(Anonymous((int p)=>CreateSet())).Except(CreateSet(),EqualityComparer<int>.Default));
        //}
    }
    //[Fact]public void 内部SelectManyのselector_Bodyに外部メソッドを入れる(){
    //    //if(MethodCall1_MethodCall_Arguments[1] is LambdaExpression selector0) {
    //    this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(()=>CreateSet().SelectMany(p=>CreateSet()).OfType<string>());
    //    //} else {
    //    this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(()=>CreateSet().SelectMany(Anonymous((int p)=>CreateSet())).OfType<string>());
    //    //}
    //    //if(typeof(ExtensionSet)==MethodCall1_MethodCall_GenericMethodDefinition.DeclaringType) {
    //    //    while(true) {
    //    //        if(GenericTypeDefinition.IsGenericType)GenericTypeDefinition=Set1.GetGenericTypeDefinition();
    //    this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(()=>CreateSet().SelectMany(Anonymous((int p)=>CreateSet())).OfType<string>());
    //    //        if(GenericTypeDefinition==typeof(ImmutableSet<>)) break;
    //    this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(()=>((ImmutableSet<int>)CreateSet()).SelectMany(Anonymous((int p)=>CreateSet())).OfType<string>());
    //    //        if(Set1.BaseType is null) {
    //    //            if(MethodCall1_MethodCall_GenericMethodDefinition==Reflection.ExtensionSet.SelectMany_selector)
    //    //        }
    //    //    }
    //    //}
    //    //if(ループ展開可能メソッドか(MethodCall1_Arguments_0,out var MethodCall1_MethodCall)) {
    //    //    switch(MethodCall1_MethodCall.Method.Name) {
    //    //        case nameof(ExtensionSet.SelectMany): {
    //    //            if(Reflection.ExtensionEnumerable.SelectMany_indexSelector!=MethodCall1_MethodCall.Method.GetGenericMethodDefinition()) {
    //    this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(()=>CreateSet().SelectMany(p=>CreateSet()).OfType<string>());
    //    //            }
    //    this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(()=>CreateEnum().SelectMany((p,index)=>CreateEnum()).OfType<object>());
    //    //        }
    //    //    }
    //    //}
    //    this.MemoryMessageJson_TExpressionObject_ExpressionAssertEqual(()=>CreateEnum().OfType<object>());
    //}

    [Fact]public void 共通後処理内部SelectManyのselectorBodyに外部メソッドを入れる(){
        var Tables = new Set<LinqDB.Databases.PrimaryKeys.Reflection,Table>();
        this.Expression実行AssertEqual(()=>Tables.SelectMany(o=>Tables).Except(Tables));
        this.Expression実行AssertEqual(()=>CreateEnum().SelectMany((p,index)=>CreateEnum().Select(q=>new{p,q,index})).Select(p=>p.p+p.q+p.index));
        //if(typeof(ExtensionSet)==MethodCall1_MethodCall_GenericMethodDefinition.DeclaringType) {
        //    while(true) {
        //        if(GenericTypeDefinition.IsGenericType)GenericTypeDefinition=Set1.GetGenericTypeDefinition();
        this.Expression実行AssertEqual(()=>CreateEnum().SelectMany(Anonymous((int p)=>CreateEnum())).OfType<string>());
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(Anonymous((int p)=>CreateSet())).OfType<string>());
        //        if(GenericTypeDefinition==typeof(ImmutableSet<>)) break;
        this.Expression実行AssertEqual(()=>((ImmutableSet<int>)CreateSet()).SelectMany(Anonymous((int p)=>CreateSet())).OfType<string>());
        //        if(Set1.BaseType is null) {
        //            if(MethodCall1_MethodCall_GenericMethodDefinition==Reflection.ExtensionSet.SelectMany_selector)
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet()).Except(CreateEnum()));

        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet()).Except(CreateSet().Select(p=>p*p),EqualityComparer<int>.Default));
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet().Except(CreateSet().Select(p=>p*p),EqualityComparer<int>.Default)));
        //        }
        //    }
        //}
    }
    [Fact]public void Select_Where再帰で匿名型を走査(){
        //if(匿名 is NewExpression NewExpression) {
        //    if(IsAnonymous||IsValueTuple) {
        //        if(IsAnonymous) {
        //            for(var a = 0;a<NewExpression_Arguments_Count;a++) {
        this.Expression実行AssertEqual(() => CreateSet().Select(p => new { p }).Where(p => p.p==0));
        //            }
        //        } else {
        //            foreach(var NewExpression_Argument in NewExpression.Arguments) {
        //                switch(Index) {
        //                    case 0: 対象=this.Select_Where再帰で匿名型を走査(NewExpression_Argument,Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Item1)),対象); Index=1;break;
        this.Expression実行AssertEqual(() => CreateSet().Select(p => new ValueTuple<int>(p)).Where(p => p.Item1==0));
        //                    case 1: 対象=this.Select_Where再帰で匿名型を走査(NewExpression_Argument,Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Item2)),対象); Index=2;break;
        this.Expression実行AssertEqual(() => CreateSet().Select(p => new ValueTuple<int,int>(p,p)).Where(p => p.Item2==0));
        //                    case 2: 対象=this.Select_Where再帰で匿名型を走査(NewExpression_Argument,Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Item3)),対象); Index=3;break;
        this.Expression実行AssertEqual(() => CreateSet().Select(p => new ValueTuple<int,int,int>(p,p,p)).Where(p => p.Item3==0));
        //                    case 3: 対象=this.Select_Where再帰で匿名型を走査(NewExpression_Argument,Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Item4)),対象); Index=4;break;
        this.Expression実行AssertEqual(() => CreateSet().Select(p => new ValueTuple<int,int,int,int>(p,p,p,p)).Where(p => p.Item4==0));
        //                    case 4: 対象=this.Select_Where再帰で匿名型を走査(NewExpression_Argument,Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Item5)),対象); Index=5;break;
        this.Expression実行AssertEqual(() => CreateSet().Select(p => new ValueTuple<int,int,int,int,int>(p,p,p,p,p)).Where(p => p.Item5==0));
        //                    case 5: 対象=this.Select_Where再帰で匿名型を走査(NewExpression_Argument,Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Item6)),対象); Index=6;break;
        this.Expression実行AssertEqual(() => CreateSet().Select(p => new ValueTuple<int,int,int,int,int,int>(p,p,p,p,p,p)).Where(p => p.Item6==0));
        //                    case 6: 対象=this.Select_Where再帰で匿名型を走査(NewExpression_Argument,Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Item7)),対象); Index=7;break;
        this.Expression実行AssertEqual(() => CreateSet().Select(p => new ValueTuple<int,int,int,int,int,int,int>(p,p,p,p,p,p,p)).Where(p => p.Item7==0));
        //                    default: Instance=Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Rest)); goto case 0;
        this.Expression実行AssertEqual(()=>CreateSet().Select(p=>new ValueTuple<int,int,int,int,int,int,int,ValueTuple<int,int,int,int,int,int,int,ValueTuple<int>>>(p,p,p,p,p,p,p,new ValueTuple<int,int,int,int,int,int,int,ValueTuple<int>>(p,p,p,p,p,p,p,new ValueTuple<int>(p)))).Where(
            p=>
                p.Item1==0&&
                p.Item2==0&&
                p.Item3==0&&
                p.Item4==0&&
                p.Item5==0&&
                p.Item6==0&&
                p.Item7==0&&
                p.Rest.Item1==0&&
                p.Rest.Item2==0&&
                p.Rest.Item3==0&&
                p.Rest.Item4==0&&
                p.Rest.Item5==0&&
                p.Rest.Item6==0&&
                p.Rest.Item7==0&&
                p.Rest.Rest.Item1==0
            )
        );
        //                }
        //            }
        //        }
        //    }
        //}
        this.Expression実行AssertEqual(() => CreateSet().Select(p =>new{a=new ValueTuple<int,int>(p+1,p+2)}).Where(p => p.a.Item1==0));
        this.Expression実行AssertEqual(
            () => CreateSet().Select(
                p =>new{
                    a=new ValueTuple<ValueTuple<int,int>,ValueTuple<int,int>>(
                        new ValueTuple<int,int>(p+1,p+2),new ValueTuple<int,int>(p+1,p+2)
                    )
                }
            ).Where(p => p.a.Item1.Item2==0)
        );
    }
    [Fact]public void Call_取得_Parameter_OuterPredicate_InnerPredicate(){
        //if(e.NodeType==ExpressionType.AndAlso) {
        //    if(Left葉Outerに移動する) {
        //        if(Right葉Outerに移動する) {
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet().Where(i=>o==3&&4==o)));
        //        } else {
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet().Where(i=>o==3&&i==3)));
        //        }
        //    } else if(Right葉Outerに移動する) {
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet().Where(i=>i==3&&o==3)));
        //    } else {
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet().Where(i=>i==o&&o==i)));
        //    }
        //} else if(this._判定_Parameter_葉に移動したいPredicate.実行(e,this.Outer!)) {
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet().Where(i=>o==0)));
        //} else {
        this.Expression実行AssertEqual(()=>CreateSet().SelectMany(o=>CreateSet().Where(i=>i==0)));
        //}
    }
    private static int StaticProperty=>1;
    private int InstanceProperty=>1;
    static T F<T>(T t)=>t;
    [Fact]public void 特定エラー(){
        this.Expression実行AssertEqual(()=>
            new ValueTuple<int,int,int,int,int,int,int,ValueTuple<int,int,int,int,int,int,int>>(1,2,3,4,5,6,7,new ValueTuple<int,int,int,int,int,int,int>(81,82,83,84,85,86,87)).Let(
                p=>
                    p.Item4+
                    p.Item5+
                    p.Item6+
                    p.Item7+
                    p.Rest.Item1+
                    p.Rest.Item2+
                    p.Rest.Item3+
                    p.Rest.Item4+
                    p.Rest.Item5+
                    p.Rest.Item6+
                    p.Rest.Item7
            )
        );
        this.Expression実行AssertEqual(()=>
            new ValueTuple<int,int,int,int,int,int,int>(1,2,3,4,5,6,7).Let(
                p=>
                    p.Item1+
                    p.Item2+
                    p.Item3+
                    p.Item4+
                    p.Item5+
                    p.Item6+
                    p.Item7
            )
        );
        this.Expression実行AssertEqual(()=>
            new ValueTuple<int,int,int,int,int,int,int,ValueTuple<int,int,int,int,int,int,int>>(1,2,3,4,5,6,7,new ValueTuple<int,int,int,int,int,int,int>(81,82,83,84,85,86,87)).Let(
                p=>
                    p.Item1+
                    p.Item2+
                    p.Item3+
                    p.Item4+
                    p.Item5+
                    p.Item6+
                    p.Item7+
                    p.Rest.Item1+
                    p.Rest.Item2+
                    p.Rest.Item3+
                    p.Rest.Item4+
                    p.Rest.Item5+
                    p.Rest.Item6+
                    p.Rest.Item7
            )
        );
        this.Expression実行AssertEqual(()=>
            new ValueTuple<int,int,int,int,int,int,int,ValueTuple<int,int,int,int,int,int,int>>(1,2,3,4,5,6,7,new ValueTuple<int,int,int,int,int,int,int>(81,82,83,84,85,86,87)).Let(
                p=>
                    p.Item1==0&&
                    //p.Item2==0&&
                    //p.Item3==0&&
                    p.Item4==0&&
                    p.Item5==0&&
                    p.Item6==0&&
                    p.Item7==0&&
                    p.Rest.Item1==0&&
                    p.Rest.Item2==0&&
                    p.Rest.Item3==0&&
                    p.Rest.Item4==0&&
                    p.Rest.Item5==0&&
                    p.Rest.Item6==0&&
                    p.Rest.Item7==0
            )
        );
        this.Expression実行AssertEqual(()=>
            new ValueTuple<int,int,int,int,int,int,int,ValueTuple<int,int,int,int,int,int,int>>(1,2,3,4,5,6,7,new ValueTuple<int,int,int,int,int,int,int>(81,82,83,84,85,86,87)).Let(
                p=>
                    p.Item1==0&&
                    p.Item2==0&&
                    p.Item3==0&&
                    p.Item4==0&&
                    p.Item5==0&&
                    p.Item6==0&&
                    p.Item7==0&&
                    p.Rest.Item1==0&&
                    p.Rest.Item2==0&&
                    p.Rest.Item3==0&&
                    p.Rest.Item4==0&&
                    p.Rest.Item5==0&&
                    p.Rest.Item6==0&&
                    p.Rest.Item7==0
            )
        );
        this.Expression実行AssertEqual(()=>
            new ValueTuple<int,int,int,int,int,int,int,ValueTuple<int>>(1,2,3,4,5,6,7,new ValueTuple<int>(81)).Let(
                p=>
                    p.Item1==0&&
                    p.Item2==0&&
                    p.Item3==0&&
                    p.Item4==0&&
                    p.Item5==0&&
                    p.Item6==0&&
                    p.Item7==0&&
                    p.Rest.Item1==0
            )
        );
        this.Expression実行AssertEqual(()=>
            new ValueTuple<int,int,int,int,int,int,int,ValueTuple<int,int,int,int,int,int,int,ValueTuple<int>>>(1,2,3,4,5,6,7,new ValueTuple<int,int,int,int,int,int,int,ValueTuple<int>>(81,82,83,84,85,86,87,new ValueTuple<int>(881))).Let(
                p=>
                    p.Item1==0&&
                    p.Item2==0&&
                    p.Item3==0&&
                    p.Item4==0&&
                    p.Item5==0&&
                    p.Item6==0&&
                    p.Item7==0&&
                    p.Rest.Item1==0&&
                    p.Rest.Item2==0&&
                    p.Rest.Item3==0&&
                    p.Rest.Item4==0&&
                    p.Rest.Item5==0&&
                    p.Rest.Item6==0&&
                    p.Rest.Item7==0&&
                    p.Rest.Rest.Item1==0
            )
        );
        this.Expression実行AssertEqual(()=>CreateSet().Select(p=>new ValueTuple<int,int,int,int,int,int,int,ValueTuple<int,int,int,int,int,int,int,ValueTuple<int>>>(p,p,p,p,p,p,p,new ValueTuple<int,int,int,int,int,int,int,ValueTuple<int>>(p,p,p,p,p,p,p,new ValueTuple<int>(p)))).Where(
            p=>
                p.Item1==0&&
                p.Item2==0&&
                p.Item3==0&&
                p.Item4==0&&
                p.Item5==0&&
                p.Item6==0&&
                p.Item7==0&&
                p.Rest.Item1==0&&
                p.Rest.Item2==0&&
                p.Rest.Item3==0&&
                p.Rest.Item4==0&&
                p.Rest.Item5==0&&
                p.Rest.Item6==0&&
                p.Rest.Item7==0&&
                p.Rest.Rest.Item1==0
            )
        );
    }
}
