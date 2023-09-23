using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using LinqDB.Sets;
using Microsoft.Build.Execution;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Serializers.MessagePack.Formatters;
using Exception=System.Exception;
using Expression = System.Linq.Expressions.Expression;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
namespace Optimizers;
public class 変換_メソッド正規化_取得インライン不可能定数:共通{
    [Fact]
    public void Constant(){
        this.共通コンパイル実行(
            Expression.Lambda<Func<int>>(
                Expression.Constant(0)
            )
        );
        this.共通コンパイル実行(
            Expression.Lambda<Func<decimal>>(
                Expression.Constant(0m)
            )
        );
    }
    [Fact]
    public void Quote(){
        this.共通コンパイル実行(()=>new int[3].AsQueryable().Select(p=>p));
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
        this.共通コンパイル実行(
            Expression.Lambda<Func<int,int>>(
                Expression.MakeBinary(NodeType,ParameterInt32,ParameterInt32,false,Method_int,ConversionInt32),
                ParameterInt32
            ),1
        );
        this.共通コンパイル実行(
            Expression.Lambda<Func<int,int>>(
                Expression.MakeBinary(NodeType,ParameterInt32,ParameterInt32,false,Method_int),
                ParameterInt32
            ),1
        );
    }
    [Fact]
    public void AddAssign()=>this.共通BinaryAssign(ExpressionType.AddAssign);
    [Fact]
    public void AddAssignChecked()=>this.共通BinaryAssign(ExpressionType.AddChecked);
    [Fact]
    public void AndAssign()=>this.共通BinaryAssign(ExpressionType.And);
    [Fact]
    public void DivideAssign()=>this.共通BinaryAssign(ExpressionType.Divide);
    [Fact]
    public void ExclusiveOrAssign()=>this.共通BinaryAssign(ExpressionType.ExclusiveOr);
    [Fact]
    public void LeftShiftAssign()=>this.共通BinaryAssign(ExpressionType.LeftShift);
    [Fact]
    public void ModuloAssign()=>this.共通BinaryAssign(ExpressionType.Modulo);
    [Fact]
    public void MultiplyAssign()=>this.共通BinaryAssign(ExpressionType.Multiply);
    [Fact]
    public void MultiplyAssignChecked()=>this.共通BinaryAssign(ExpressionType.MultiplyChecked);
    [Fact]
    public void OrAssign()=>this.共通BinaryAssign(ExpressionType.Or);
    [Fact]
    public void PowerAssign()=>this.共通BinaryAssign(ExpressionType.Power);
    [Fact]
    public void RightShiftAssign()=>this.共通BinaryAssign(ExpressionType.RightShift);
    [Fact]
    public void SubtractAssign()=>this.共通BinaryAssign(ExpressionType.Subtract);
    [Fact]
    public void SubtractAssignChecked()=>this.共通BinaryAssign(ExpressionType.SubtractChecked);
    [Fact]
    public void Try(){
        var ParameterInt32=Expression.Parameter(typeof(int),"int32");
        this.共通コンパイル実行(
            Expression.Lambda<Func<int,int>>(
                Expression.TryCatch(
                    Expression.AddAssign(ParameterInt32,ParameterInt32),
                    Expression.Catch(
                        typeof(Exception),
                        Expression.Constant(0)
                    )
                ),
                ParameterInt32
            ),1
        );
        this.共通コンパイル実行(
            Expression.Lambda<Func<int,int>>(
                Expression.TryCatchFinally(
                    ParameterInt32,
                    Expression.AddAssign(ParameterInt32,ParameterInt32)
                ),ParameterInt32
            ),1
        );
        this.共通コンパイル実行(
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
        this.共通コンパイル実行(
            Expression.Lambda<Func<int,int>>(
                Expression.TryCatch(
                    ParameterInt32,
                    Expression.Catch(
                        ex,
                        Expression.AddAssign(ParameterInt32,ParameterInt32)
                    )
                ),
                ParameterInt32
            ),1
        );
        this.共通コンパイル実行(
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
            ),1
        );
        this.共通コンパイル実行(
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
        this.共通コンパイル実行(
            Expression.Lambda<Func<int,int>>(
                Expression.TryCatch(
                    ParameterInt32,
                    Expression.Catch(
                        typeof(Exception),
                        Expression.AddAssign(ParameterInt32,ParameterInt32)
                    )
                ),
                ParameterInt32
            ),1
        );
        this.共通コンパイル実行(
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
            ),1
        );
        this.共通コンパイル実行(
            Expression.Lambda<Func<int,int>>(
                Expression.TryCatch(
                    ParameterInt32,
                    Expression.Catch(
                        typeof(Exception),
                        ParameterInt32
                    )
                ),
                ParameterInt32
            ),1
        );
        this.共通コンパイル実行(
            Expression.Lambda<Func<int,int>>(
                Expression.TryFault(
                    ParameterInt32,
                    Expression.AddAssign(ParameterInt32,ParameterInt32)
                ),
                ParameterInt32
            ),1
        );
        this.共通コンパイル実行(
            Expression.Lambda<Func<int,int>>(
                Expression.TryFault(
                    ParameterInt32,
                    ParameterInt32
                ),
                ParameterInt32
            ),1
        );
    }
    private void 共通Unary(ExpressionType NodeType){
        var ParameterInt32=Expression.Parameter(typeof(int),"int32");
        this.共通コンパイル実行(
            Expression.Lambda<Func<int,int>>(
                Expression.MakeUnary(NodeType,ParameterInt32,typeof(int)),
                ParameterInt32
            ),1
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
    public void Lambda()=>this.共通コンパイル実行(Expression.Lambda<Action>(Expression.Default(typeof(void))));
    [Fact]
    public void Not()=>this.共通Unary(ExpressionType.Not);
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
        //this.共通コンパイル実行(Expression.Lambda<Func<nuint>>(Expression.Convert(Expression.Constant(1 ,typeof(int  )),typeof(nuint))));
        //this.共通コンパイル実行(Expression.Lambda<Func<nint >>(Expression.Convert(Expression.Constant(1L,typeof(long )),typeof(nint ))));
        //this.共通コンパイル実行(Expression.Lambda<Func<nuint>>(Expression.Convert(Expression.Constant(1L,typeof(long )),typeof(nuint))));
        //this.共通コンパイル実行(Expression.Lambda<Func<int  >>(Expression.Convert(Expression.Constant(1 ,typeof(nint )),typeof(int  ))));
        //this.共通コンパイル実行(Expression.Lambda<Func<long >>(Expression.Convert(Expression.Constant(1 ,typeof(nint )),typeof(long ))));
        //this.共通コンパイル実行(Expression.Lambda<Func<uint >>(Expression.Convert(Expression.Constant(1 ,typeof(nuint)),typeof(uint ))));
        //this.共通コンパイル実行(Expression.Lambda<Func<ulong>>(Expression.Convert(Expression.Constant(1 ,typeof(nuint)),typeof(ulong))));
        //this.共通コンパイル実行(Expression.Lambda<Func<nint >>(Expression.Convert(Expression.Constant(1 ,typeof(int  )),typeof(nint ))));
        //this.共通コンパイル実行(Expression.Lambda<Func<nint >>(Expression.Convert(Expression.Constant(1 ,typeof(int  )),typeof(nint ))));
        //var p=Expression.Parameter(typeof(int));
        //this.共通コンパイル実行(
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
        this.共通コンパイル実行(
            Expression.Lambda<Func<sbyte>>(
                Expression.Convert(
                    Expression.Constant(1000),
                    typeof(sbyte)
                )
            )
        );
    }
    [Fact]public void ConvertChecked(){
        Assert.Throws<OverflowException>(()=>
            this.共通コンパイル実行(
                Expression.Lambda<Func<sbyte>>(
                    Expression.ConvertChecked(
                        Expression.Constant(1000),
                        typeof(sbyte)
                    )
                )
            )
        );
    }
    [Fact]public void Call_Average(){
        var s=new int[10];
        this.共通コンパイル実行(()=>s.Select(p=>(decimal)p).Average());
        this.共通コンパイル実行(()=>s.Select(p=>(double)p).Average());
        this.共通コンパイル実行(()=>s.Select(p=>(decimal?)p).Average());
        this.共通コンパイル実行(()=>s.Select(p=>(double?)p).Average());
    }
    private static IEnumerable<T> そのまま<T>(IEnumerable<T> i)=>i;
    [Fact]public void Call_Any(){
        var s=new int[10];
        this.共通コンパイル実行(()=>s.Select(p=>p+p).Any());
        this.共通コンパイル実行(()=>s.GroupJoin(s,o=>o,i=>i,(o,i)=>new{o,i}).Any());
        this.共通コンパイル実行(()=>そのまま(s).Any());
        this.共通コンパイル実行(()=>s.Except(s).Any());
        this.共通コンパイル実行(()=>s.Any(p=>p==0));
    }
    [Fact]public void Call_Contains(){
        this.共通コンパイル実行(()=>new int[10].Contains(0));
        this.共通コンパイル実行(()=>((IEnumerable<int>)new List<int>()).Contains(0));
        this.共通コンパイル実行(()=>new decimal[10].Contains(0));
        this.共通コンパイル実行(()=>new decimal[10].Select(p=>(object)p).Contains(0m));
    }
    [Fact]public void Call_Delete(){
        this.共通コンパイル実行(()=>new Set<int>().SelectMany(p=>new Set<int>()).Delete(p=>true));
        this.共通コンパイル実行(()=>new Set<int>().SelectMany(p=>new Set<int>()).Delete((Func<int,bool>)(p=>true)));
    }
    [Fact]public void Call_GroupBy(){
        this.共通コンパイル実行(()=>new int[10].GroupBy(p=>new{p},(key,g)=>key.p+g.Count()));
        this.共通コンパイル実行(()=>new int[10].GroupBy(p=>(decimal)p,(key,g)=>key+g.Count(),EqualityComparer<decimal>.Default));
        this.共通コンパイル実行(()=>new Set<int>().GroupBy(p=>new{p},(key,g)=>key.p+g.Count()));
        this.共通コンパイル実行(()=>new int[10].GroupBy(p=>new{p},p=>p+p,(key,g)=>key.p+g.Count()));
        this.共通コンパイル実行(()=>new int[10].GroupBy(p=>(decimal)p,p=>p+p,(key,g)=>key+g.Count(),EqualityComparer<decimal>.Default));
        this.共通コンパイル実行(()=>new Set<int>().GroupBy(p=>new{p},p=>p+p,(key,g)=>key.p+g.Count()));
        this.共通コンパイル実行(()=>new int[10].GroupBy(p=>new{p}));
        this.共通コンパイル実行(()=>new int[10].GroupBy(p=>(decimal)p,EqualityComparer<decimal>.Default));
        this.共通コンパイル実行(()=>new Set<int>().GroupBy(p=>new{p}));
    }
    [Fact]public void Call_GroupJoin(){
        this.共通コンパイル実行(()=>new int[10].GroupJoin(new int[10],o=>o,i=>i,(o,i)=>new{o,i}));
        this.共通コンパイル実行(()=>new int[10].GroupJoin(new int[10],(Func<int,int>)(o=>o),i=>i,(o,i)=>new{o,i}));
        this.共通コンパイル実行(()=>new int[10].GroupJoin(new int[10],o=>o,(Func<int,int>)(i=>i),(o,i)=>new{o,i}));
        this.共通コンパイル実行(()=>new Set<int>().GroupJoin(new Set<int>(),o=>o,i=>i,(o,i)=>new{o,i}));
        this.共通コンパイル実行(()=>new Set<int>().GroupJoin(new Set<int>(),(Func<int,int>)(o=>o),i=>i,(o,i)=>new{o,i}));
        this.共通コンパイル実行(()=>new Set<int>().GroupJoin(new Set<int>(),o=>o,(Func<int,int>)(i=>i),(o,i)=>new{o,i}));
        this.共通コンパイル実行(()=>new int[10].GroupJoin(new int[10],o=>o,i=>i,(Func<int,IEnumerable<int>,int>)((o,i)=>o+i.Count())));
    }
    private static Set<int>CreateSet()=>new();
    [Fact]public void Call_Intersect(){
        this.共通コンパイル実行(()=>CreateSet().Intersect(CreateSet()).Where(p=>p==0));
        this.共通コンパイル実行(()=>CreateSet().SelectMany(o=>CreateSet().SelectMany(i=>CreateSet())).Intersect(CreateSet()));
        this.共通コンパイル実行(()=>CreateSet().SelectMany(o=>CreateSet()).Intersect(CreateSet()));
        this.共通コンパイル実行(()=>CreateSet().Intersect(CreateSet().Where(p=>true)));
        this.共通コンパイル実行(()=>CreateSet().Intersect(CreateSet(),EqualityComparer<int>.Default));
    }
    private static List<int>CreateEnum()=>new();
    [Fact]public void Call_Join(){
        this.共通コンパイル実行(()=>CreateSet().Join(CreateSet(),o=>o,i=>i,(o,i)=>new{o,i}));
        this.共通コンパイル実行(()=>CreateEnum().Join(CreateEnum(),o=>o,i=>i,(o,i)=>new{o,i}));
        this.共通コンパイル実行(()=>CreateSet().Join(CreateSet(),o=>o,i=>i,(Func<int,int,int>)((o,i)=>o+i)));
        this.共通コンパイル実行(()=>CreateSet().Join(CreateSet(),o=>o,(Func<int,int>)(i=>i),(o,i)=>new{o,i}));
        this.共通コンパイル実行(()=>CreateSet().Join(CreateSet(),o=>o,(Func<int,int>)(i=>i),(Func<int,int,int>)((o,i)=>o+i)));
        this.共通コンパイル実行(()=>CreateSet().Join(CreateSet(),(Func<int,int>)(o=>o),i=>i,(o,i)=>new{o,i}));
        this.共通コンパイル実行(()=>CreateSet().Join(CreateSet(),(Func<int,int>)(o=>o),i=>i,(Func<int,int,int>)((o,i)=>o+i)));
        this.共通コンパイル実行(()=>CreateSet().Join(CreateSet(),(Func<int,int>)(o=>o),(Func<int,int>)(i=>i),(o,i)=>new{o,i}));
        this.共通コンパイル実行(()=>CreateSet().Join(CreateSet(),(Func<int,int>)(o=>o),(Func<int,int>)(i=>i),(Func<int,int,int>)((o,i)=>o+i)));
        this.共通コンパイル実行(()=>CreateSet().Join(CreateSet(),(Func<int,int>)(o=>o),(Func<int,int>)(i=>i),(Func<int,int,int>)((o,i)=>o+i),EqualityComparer<int>.Default));
    }
    [Fact]public void Call_OfType(){
        this.共通コンパイル実行(()=>CreateSet().SelectMany(o=>CreateSet().SelectMany(i=>CreateSet())).OfType<string>());
        this.共通コンパイル実行(()=>CreateSet().SelectMany(p=>CreateSet()).OfType<string>());
        this.共通コンパイル実行(()=>new object[]{"ABC",1,3.0}.OfType<string>());
        this.共通コンパイル実行(()=>new string[]{"A","B","C"}.OfType<object>());
    }
    [Fact]public void Call_Select(){
        this.共通コンパイル実行(()=>CreateSet().SelectMany(o=>CreateSet().SelectMany(i=>CreateSet())).OfType<string>());
        this.共通コンパイル実行(()=>CreateSet().SelectMany(p=>CreateSet()).OfType<string>());
        this.共通コンパイル実行(()=>new object[]{"ABC",1,3.0}.OfType<string>());
        this.共通コンパイル実行(()=>new string[]{"A","B","C"}.OfType<object>());
    }
    [Fact]public void Call_Single(){
        this.共通コンパイル実行(()=>new int[1].Single(p=>true));
        this.共通コンパイル実行(()=>new int[1].Single());
    }
    [Fact]public void Call_SingleDefault(){
        this.共通コンパイル実行(()=>new int[1].SingleOrDefault(p=>true));
        this.共通コンパイル実行(()=>new int[1].SingleOrDefault(p=>true,1));
        this.共通コンパイル実行(()=>new int[1].SingleOrDefault());
    }
    [Fact]public void Call_ToArray(){
        this.共通コンパイル実行(()=>new int[1].ToArray().ToArray());
        this.共通コンパイル実行(()=>((IEnumerable<int>)new int[1]).ToArray());
    }
    [Fact]public void Call_Except(){
        this.共通コンパイル実行(()=>CreateSet().SelectMany(o=>CreateSet()).Except(CreateSet().Select(p=>p*p),EqualityComparer<int>.Default));
        this.共通コンパイル実行(()=>CreateSet().SelectMany(o=>CreateSet()).Except(CreateSet(),EqualityComparer<int>.Default));
        this.共通コンパイル実行(()=>CreateSet().SelectMany(o=>CreateSet()).Except(CreateSet()));
    }
    [Fact]public void Call_Union(){
        this.共通コンパイル実行(()=>CreateSet().SelectMany(o=>CreateSet()).Union(CreateSet().Select(p=>p*p),EqualityComparer<int>.Default));
        this.共通コンパイル実行(()=>CreateSet().SelectMany(o=>CreateSet()).Union(CreateSet(),EqualityComparer<int>.Default));
        this.共通コンパイル実行(()=>CreateSet().SelectMany(o=>CreateSet()).Union(CreateSet()));
    }
    static Func<TO,TResult> Anonymous<TO,TResult>(Func<TO,TResult> i)=>i;
    static Func<TO,T1,TResult> Anonymous<TO,T1,TResult>(Func<TO,T1,TResult> i)=>i;
    [Fact]public void Call_SelectMany(){
        //if(MethodCall0_Arguments.Count==2) {
        //    if(SelectMany is not null)
        this.共通コンパイル実行(()=>CreateSet().SelectMany(o=>CreateSet()).SelectMany(i=>CreateSet()));
        //this.共通コンパイル実行(()=>CreateSet().SelectMany(o=>CreateSet()).SelectMany(i=>CreateSet()));
        //    if(MethodCall1_Arguments_1 is LambdaExpression selector&&ループ展開可能メソッドか(selector.Body,out _)) {
        //        if(OuterPredicate is not null) {
        this.共通コンパイル実行(()=>CreateSet().SelectMany(o=>CreateSet().Where(i=>o==0)));
        //        }
        this.共通コンパイル実行(()=>CreateSet().SelectMany(o=>CreateSet().Where(i=>i==0)));
        //    } else {
        this.共通コンパイル実行(()=>CreateSet().SelectMany(o=>CreateSet().ToArray()));
        this.共通コンパイル実行(()=>CreateSet().SelectMany(Anonymous((int o)=>CreateSet())));
        //} else {
        //    if(Reflection.ExtensionEnumerable.SelectMany_collectionSelector_resultSelector==MethodCall0_GenericMethodDefinition) {
        this.共通コンパイル実行(()=>CreateEnum().SelectMany(o=>CreateEnum(),(o,i)=>o+i));
        //    } else if(Reflection.ExtensionEnumerable.SelectMany_indexCollectionSelector_resultSelector==MethodCall0_GenericMethodDefinition) {
        this.共通コンパイル実行(()=>CreateEnum().SelectMany((o,index)=>CreateEnum(),(o,i)=>o+i));
        //    }else{
        this.共通コンパイル実行(()=>CreateSet().SelectMany(o=>CreateSet(),(o,i)=>o+i));
        //    if(MethodCall1_Arguments_1 is LambdaExpression collectionSelector) {
        //        if(MethodCall1_Arguments_2 is LambdaExpression resultSelector) {
        this.共通コンパイル実行(()=>CreateSet().SelectMany(o=>CreateSet(),(o,i)=>o*i));
        //        }else{
        this.共通コンパイル実行(()=>CreateSet().SelectMany(o=>CreateSet(),Anonymous((int o,int i)=>o*i)));
        //    }else{
        //        if(MethodCall1_Arguments_2 is LambdaExpression resultSelector) {
        this.共通コンパイル実行(()=>CreateSet().SelectMany(Anonymous<int,Set<int>>(o=>CreateSet()),(o,i)=>o*i));
        //        }else{
        this.共通コンパイル実行(()=>CreateSet().SelectMany(Anonymous<int,Set<int>>(o=>CreateSet()),Anonymous((int o,int i)=>o*i)));
        //        if(indexSelectorか) {
        this.共通コンパイル実行(()=>CreateEnum().SelectMany((Func<int,int,IEnumerable<int>>)((o,index)=>CreateEnum()),Anonymous((int o,int i)=>o*i)));
        this.共通コンパイル実行(()=>CreateSet().SelectMany((Func<int,IEnumerable<int>>)(o=>CreateSet()),(o,i)=>o+i));
    }
    [Fact]
    public void Call_Where(){
        //if(Reflection.ExtensionEnumerable.Where_index!=MethodCall0_GenericMethodDefinition) {
        //    if(ループ展開可能メソッドか(MethodCall1_Arguments_0,out var MethodCall1_MethodCall)) {
        //        switch(MethodCall1_MethodCall_Method.Name) {
        //            case nameof(ExtensionSet.Except):
        this.共通コンパイル実行(() => CreateSet().Except(CreateSet()).Where(p => true));
        //            case nameof(ExtensionSet.Intersect):
        this.共通コンパイル実行(() => CreateSet().Intersect(CreateSet()).Where(p => true));
        //            case nameof(ExtensionSet.Union):
        this.共通コンパイル実行(() => CreateSet().Union(CreateSet()).Where(p => true));
        //            case nameof(ExtensionSet.Select): {
        //                if(MethodCall1_Arguments_1 is LambdaExpression predicate) {
        //                    if(MethodCall1_MethodCall.Arguments[1] is LambdaExpression selector) {
        this.共通コンパイル実行(()=>CreateSet().Select(p=>new{p}).Where(p=>true));
        //                    }
        this.共通コンパイル実行(()=>CreateSet().Select(Anonymous((int p)=>new{p})).Where(p=>true));
        //                }
        this.共通コンパイル実行(()=>CreateSet().Select(p=>p*p).Where(Anonymous((int p)=>true)));
        //            case nameof(ExtensionSet.SelectMany): {
        //                if(Reflection.ExtensionEnumerable.SelectMany_indexSelector!=MethodCall1_MethodCall.Method.GetGenericMethodDefinition()) {
        this.共通コンパイル実行(()=>CreateSet().SelectMany(p=>CreateSet()).Where(p=>p==4));
        //                }
        this.共通コンパイル実行(()=>CreateEnum().SelectMany((o,index)=>CreateEnum(),(o,i)=>o+i).Where(p=>p==4));
        //            case nameof(ExtensionSet.Where): {
        //                if(Reflection.ExtensionEnumerable.Where_index!=MethodCall1_MethodCall_Method.GetGenericMethodDefinition()) {
        //                    if(MethodCall1_Arguments_1 is LambdaExpression predicate外) {
        //                        if(MethodCall1_MethodCall0_Arguments[1]is LambdaExpression predicate内) {
        this.共通コンパイル実行(()=>CreateSet().Where(p=>p==1).Where(p=>p==2));
        //                        }else{
        this.共通コンパイル実行(()=>CreateSet().Where(Anonymous((int p)=>p==1)).Where(p=>p==2));
        //                        }
        //                    }else{
        //                        if(MethodCall1_MethodCall0_Arguments[1]is LambdaExpression predicate内) {
        this.共通コンパイル実行(()=>CreateSet().Where(p=>p==1).Where(Anonymous((int p)=>p==2)));
        //                        }else{
        this.共通コンパイル実行(()=>CreateSet().Where(Anonymous((int p)=>p==1)).Where(Anonymous((int p)=>p==2)));
        //                        }
    }
}
