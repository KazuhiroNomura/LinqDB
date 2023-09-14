using System.Diagnostics;
//using System.Linq.Expressions;
using LinqDB.Serializers.MemoryPack.Formatters;
using MemoryPack;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
//using System.Linq.Expressions;
//using Expressions = System.Linq.Expressions;
using Microsoft.CSharp.RuntimeBinder;
//using Binder = Microsoft.CSharp.RuntimeBinder;
using MessagePack;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using Xunit.Sdk;
namespace Serializers.MessagePack.Formatters;
using System.Linq.Expressions;
[Serializable,MemoryPackable,MessagePackObject(true)]
public partial struct 演算子{
    public bool HasValue;
    public bool Value;
    public 演算子(bool Value){
        this.Value=Value;
        this.HasValue=true;
    }
    public 演算子(){
        this.Value=default!;
        this.HasValue=false;
    }
    public static bool operator true(演算子 x)=>x.HasValue;
    public static bool operator false(演算子 x)=>!x.HasValue;
    public static 演算子 operator &(演算子 x,演算子 y)=>x.HasValue&&y.HasValue?new 演算子(x.Value&y.Value):new 演算子();
    public static 演算子 operator |(演算子 x,演算子 y)=>x.HasValue&&y.HasValue?new 演算子(x.Value|y.Value):new 演算子();
    public static 演算子 operator ~(演算子 x)=>new(!x.Value);
    public static 演算子 operator ++(演算子 x)=>new(x.Value);
    public static 演算子 operator --(演算子 x)=>new(x.Value);
    public static 演算子 operator -(演算子 x)=>new(x.Value);
    public static 演算子 operator +(演算子 x)=>new(x.Value);
    public static explicit  operator 演算子1(演算子 x)=>new(x.Value);
    public static explicit  operator 演算子(演算子1 x)=>new(x.Value);
}
[Serializable,MemoryPackable,MessagePackObject(true)]
public partial struct 演算子1{
    public bool HasValue;
    public bool Value;
    public 演算子1(bool Value){
        this.Value=Value;
        this.HasValue=true;
    }
    public 演算子1(){
        this.Value=default!;
        this.HasValue=false;
    }
    public static bool operator true(演算子1 x)=>x.HasValue;
    public static bool operator false(演算子1 x)=>!x.HasValue;
    public static 演算子1 operator &(演算子1 x,演算子1 y)=>x.HasValue&&y.HasValue?new 演算子1(x.Value&y.Value):new 演算子1();
    public static 演算子1 operator |(演算子1 x,演算子1 y)=>x.HasValue&&y.HasValue?new 演算子1(x.Value|y.Value):new 演算子1();
    public static 演算子1 operator ~(演算子1 x)=>new(!x.Value);
    public static 演算子1 operator ++(演算子1 x)=>new(x.Value);
    public static 演算子1 operator --(演算子1 x)=>new(x.Value);
    public static 演算子1 operator -(演算子1 x)=>new(x.Value);
    public static 演算子1 operator +(演算子1 x)=>new(x.Value);
}
//public partial struct AlsoElse{
//    public bool HasValue;
//    public bool Value;
//    public AlsoElse(bool Value){
//        this.Value=Value;
//        this.HasValue=true;
//    }
//    public AlsoElse(){
//        this.Value=default!;
//        this.HasValue=false;
//    }
//    public static bool operator true(Binary x)=>x.HasValue;
//    public static bool operator false(Binary x)=>!x.HasValue;
//    public static Binary operator &(Binary x,Binary y)=>x.HasValue&&y.HasValue?new Binary(x.Value&y.Value):new Binary();
//    public static Binary operator |(Binary x,Binary y)=>x.HasValue&&y.HasValue?new Binary(x.Value|y.Value):new Binary();
//}
[Serializable,MemoryPackable,MessagePackObject(true)]
public partial class テスト:IEquatable<テスト>{
    public static void StaticMethod(){}
    public static void StaticMethod(int a){}
    public static void StaticMethod(int a,int b){}
    public void InstanceMethod(){}
    public void InstanceMethod(int a){}
    public void InstanceMethod(int a,int b){}
    //[MemoryPackIgnore,IgnoreMember]
    //public Func<int,int,int,bool> Delegate=(a,b,c)=>a==b&&b==c;
    public void Action(int a,double b,string c){Trace.WriteLine("Action");}
    public bool Func(int a,double b,string c){
        Trace.WriteLine("Func");
        return a.Equals(b)&&b.Equals(c);
    }
    void xx(){
       // this.Delegate(1,2,3);
        //this.Action(a:0,1,b:3);
    }
    public static int static_Func(int a,int b)=>a+b;
    public bool Equals(テスト? other){
        if(ReferenceEquals(null,other)){
            return false;
        }
        if(ReferenceEquals(this,other)){
            return true;
        }
        return true;
    }
    public override bool Equals(object? obj){
        if(ReferenceEquals(null,obj)){
            return false;
        }
        if(ReferenceEquals(this,obj)){
            return true;
        }
        if(obj.GetType()!=this.GetType()){
            return false;
        }
        return this.Equals((テスト)obj);
    }
    public override int GetHashCode(){
        return 0;
    }
    public static bool operator==(テスト? left,テスト? right){
        return Equals(left,right);
    }
    public static bool operator!=(テスト? left,テスト? right){
        return!Equals(left,right);
    }
}
[MemoryPackable,MessagePackObject(true)]
public partial struct TestDynamic<T>
{
    public T メンバー1;
    public T メンバー2;

    public TestDynamic(T メンバー1,T メンバー2)
    {
        this.メンバー1 = メンバー1;
        this.メンバー2 = メンバー2;
    }
}
public class TestExpression:共通 {
    //private readonly ExpressionEqualityComparer ExpressionEqualityComparer=new();
    //protected readonly IJsonFormatterResolver JsonFormatterResolver;
    //protected readonly MessagePackSerializerOptions MessagePackSerializerOptions;
    //private readonly SerializerConfiguration SerializerConfiguration=new();
    //public Expression(){
    //    var SerializerConfiguration=this.SerializerConfiguration;
    //    this.JsonFormatterResolver=Utf8Json.Resolvers.CompositeResolver.Create(
    //        new IJsonFormatter[]{
    //        },
    //        new IJsonFormatterResolver[]{
    //            SerializerConfiguration.JsonFormatterResolver
    //        }

    //        //new IJsonFormatterResolver[]{
    //        //    Utf8Json.Resolvers.BuiltinResolver.Instance,//よく使う型
    //        //    Utf8Json.Resolvers.DynamicGenericResolver.Instance,//主にジェネリックコレクション
    //        //    Utf8Json.Resolvers.EnumResolver.Default,
    //        //    AnonymousExpressionJsonFormatterResolver,
    //        //}
    //    );
    //    this.MessagePackSerializerOptions=MessagePackSerializerOptions.Standard.WithResolver(
    //        global::MessagePack.Resolvers.CompositeResolver.Create(
    //            new global::MessagePack.Formatters.IMessagePackFormatter[]{
    //            },
    //            new IFormatterResolver[]{
    //                SerializerConfiguration.MessagePackSerializerOptions.Resolver
    //            }
    //        )
    //    );
    //}
    //private static readonly Expressions.ParameterExpression int8    = Expressions.Expression.Parameter(typeof(sbyte),"int8");
    //private static readonly Expressions.ParameterExpression int16   = Expressions.Expression.Parameter(typeof(short),"int16");
    //private static readonly Expressions.ConstantExpression  int32   = Expressions.Expression.Constant(1);
    //private static readonly Expressions.ParameterExpression int64   = Expressions.Expression.Parameter(typeof(long),"int64");
    //private static readonly Expressions.ParameterExpression uint8   = Expressions.Expression.Parameter(typeof(byte),"uint8");
    //private static readonly Expressions.ParameterExpression uint16  = Expressions.Expression.Parameter(typeof(ushort),"uint16");
    //private static readonly Expressions.ParameterExpression uint32  = Expressions.Expression.Parameter(typeof(uint),"uint32");
    //private static readonly Expressions.ParameterExpression uint64  = Expressions.Expression.Parameter(typeof(ulong),"uint64");
    //private static readonly Expressions.ParameterExpression @float  = Expressions.Expression.Parameter(typeof(float),"float");
    //private static readonly Expressions.ConstantExpression  @double = Expressions.Expression.Constant(1.0);
    //private static readonly Expressions.ConstantExpression @string  = Expressions.Expression.Constant("string");
    //private static readonly Expressions.ConstantExpression array    = Expressions.Expression.Constant(new int[1]);
    //private static readonly Expressions.ConstantExpression @bool    = Expressions.Expression.Constant(true);
    private static readonly ParameterExpression ParameterDecimal = Expression.Parameter(typeof(decimal),"decimal");

    private static readonly ConstantExpression int8 = Expression.Constant((sbyte)1);
    private static readonly ConstantExpression int16 = Expression.Constant((short)1);
    private static readonly ConstantExpression int32 = Expression.Constant(1);
    private static readonly ConstantExpression int64 = Expression.Constant((long)1);
    private static readonly ConstantExpression uint8 = Expression.Constant((byte)1);
    private static readonly ConstantExpression uint16 = Expression.Constant((ushort)1);
    private static readonly ConstantExpression uint32 = Expression.Constant((uint)1);
    private static readonly ConstantExpression uint64 = Expression.Constant((ulong)1);
    private static readonly ConstantExpression @float = Expression.Constant((float)1);
    private static readonly ConstantExpression Constant1d = Expression.Constant(1.0);
    //private static readonly Expressions.ConstantExpression @decimal = Expressions.Expression.Constant(typeof(decimal),"decimal");
    private static readonly ConstantExpression @string = Expression.Constant("string");
    private static readonly ConstantExpression array = Expression.Constant(new int[10]);
    private static readonly ConstantExpression @bool = Expression.Constant(true);
    //private static readonly ParameterExpression[] Parameters={int8,int16,int32,int64,uint8,uint16,uint32,uint64,@float,@double,@decimal,@string};
    //private static readonly ParameterExpression @int = Expressions.Expression.Parameter(typeof(decimal),"int");
    //private static readonly ParameterExpression @int = Expressions.Expression.Parameter(typeof(decimal),"int");
    //private static readonly ParameterExpression @decimal = Expressions.Expression.Parameter(typeof(decimal),"decimal");
    private static int FieldInt32;
    private static MemberExpression MemberInt32 =Expression.MakeMemberAccess(
        null,
        typeof(TestExpression).GetField(nameof(FieldInt32),Reflection.BindingFlags.Static|Reflection.BindingFlags.NonPublic)!
    );
    private static double FieldDouble;
    private static MemberExpression MemberDouble =Expression.MakeMemberAccess(
        null,
        typeof(TestExpression).GetField(nameof(FieldDouble),Reflection.BindingFlags.Static|Reflection.BindingFlags.NonPublic)!
    );
    private static string FieldString;
    private static MemberExpression MemberString=Expression.MakeMemberAccess(
        null,
        typeof(TestExpression).GetField(nameof(FieldString),Reflection.BindingFlags.Static|Reflection.BindingFlags.NonPublic)!
    );
    private static bool FieldBoolean;
    private static MemberExpression MemberBoolean =Expression.MakeMemberAccess(
        null,
        typeof(TestExpression).GetField(nameof(FieldBoolean),Reflection.BindingFlags.Static|Reflection.BindingFlags.NonPublic)!
    );
    
    private static string string_string_string(string? a,string b)=>a??b;
    private static int int_int_int(int a,int b){
        var r=1;
        for(var x=0;x<b;x++){
            r*=a;
        }
        return r;
    }
    ///// <summary>
    ///// a&&b a.HasValue=true?a.Value&b.Value:a
    ///// a||b a.HasValue=false?b:a.Value&b.Value
    ///// </summary>
    ///// <param name="a"></param>
    ///// <param name="b"></param>
    ///// <returns></returns>
    //public static AlsoElse AlsoElseAnd(AlsoElse a,AlsoElse b){
    //    if(a.HasValue) return new AlsoElse(a.Value&b.Value);
    //    return a;
    //}
    //private static AlsoElse AlsoElseOr(AlsoElse a,AlsoElse b){
    //    if(a.HasValue) return b;
    //    return new AlsoElse(a.Value|b.Value);
    //}
    private static double double_dobuble_double(double a,double b)=>Math.Pow(a,b);
    [Fact]
    public void Binary(){
        var ParameterDouble=Expression.Parameter(typeof(double));
        var ParameterInt32= Expression.Parameter(typeof(int),"int32");
        var ParameterString= Expression.Parameter(typeof(string),"string");
        var ConstantString = Expression.Constant("string");
        //var ConstantStringNull=Expression.Constant(null,typeof(string));
        var ConstantArray = Expression.Constant(new int[10]);
        var Constant0= Expression.Constant(0);
        var Constant1= Expression.Constant(1);
        var ConstantTrue = Expression.Constant(true);
        var ConstantBinry= Expression.Constant(new 演算子(true));
        var ConversionInt32=Expression.Lambda<Func<int,int>>(Expression.Add(ParameterInt32,ParameterInt32),ParameterInt32);
        var ConversionDouble=Expression.Lambda<Func<double,double>>(Expression.Add(ParameterDouble,ParameterDouble),ParameterDouble);
        var call=Expression.Call(
            Method(()=>string_string_string("","")),
            ParameterString,
            ParameterString
        );
        var ConversionString=Expression.Lambda<Func<string,string>>(Expression.Call(null,Method(()=>string_string_string("","")),ParameterString,ParameterString),ParameterString);
        var Method_int=Method(()=>int_int_int(1,1));
        var Method_double=Method(()=>double_dobuble_double(1,1));
        //var Method_bool=Method(()=>AndAlso(true,true));
        //var Method_NullableBoolean=Method(()=>bool_bool_bool(true,true));
        共通0(Expression.ArrayIndex           (ConstantArray,Constant1));
        共通0(Expression.Assign               (ParameterInt32,Constant1));
        共通0(Expression.Coalesce             (ConstantString,ConstantString));
        共通0(Expression.Add                  (Constant1,Constant1));
        共通0(Expression.AddChecked           (Constant1,Constant1));
        共通0(Expression.And                  (Constant1,Constant1));
        共通0(Expression.AndAlso              (ConstantBinry,ConstantBinry));
        共通0(Expression.Divide               (Constant1,Constant1));
        共通0(Expression.ExclusiveOr          (Constant1,Constant1));
        共通0(Expression.LeftShift            (Constant1,Constant1));
        共通0(Expression.Modulo               (Constant1,Constant1));
        共通0(Expression.Multiply             (Constant1,Constant1));
        共通0(Expression.MultiplyChecked      (Constant1,Constant1));
        共通0(Expression.Or                   (Constant1,Constant1));
        共通0(Expression.OrElse               (ConstantTrue,ConstantTrue));
        共通0(Expression.Power                (Constant1d,Constant1d));
        共通0(Expression.RightShift           (Constant1,Constant1));
        共通0(Expression.Subtract             (Constant1,Constant1));
        共通0(Expression.SubtractChecked      (Constant1,Constant1));
        共通0(Expression.Equal                (Constant1,Constant1));
        共通0(Expression.GreaterThan          (Constant1,Constant1));
        共通0(Expression.GreaterThanOrEqual   (Constant1,Constant1));
        共通0(Expression.LessThan             (Constant1,Constant1));
        共通0(Expression.LessThanOrEqual      (Constant1,Constant1));
        共通0(Expression.NotEqual             (Constant1,Constant1));
        共通0(Expression.LessThanOrEqual      (Constant1,Constant1));
        共通0(Expression.NotEqual             (Constant1,Constant1));
        共通1(Expression.AddAssign            (MemberInt32 ,Constant1));
        共通1(Expression.AddAssignChecked     (MemberInt32 ,Constant1));
        共通1(Expression.AndAssign            (MemberInt32 ,Constant1));
        共通1(Expression.DivideAssign         (MemberInt32 ,Constant1));
        共通1(Expression.ExclusiveOrAssign    (MemberInt32 ,Constant1));
        共通1(Expression.LeftShiftAssign      (MemberInt32 ,Constant1));
        共通1(Expression.ModuloAssign         (MemberInt32 ,Constant1));
        共通1(Expression.MultiplyAssign       (MemberInt32 ,Constant1));
        共通1(Expression.MultiplyAssignChecked(MemberInt32 ,Constant1));
        共通1(Expression.OrAssign             (MemberInt32 ,Constant1));
        共通1(Expression.PowerAssign          (MemberDouble,Constant1d));
        共通1(Expression.RightShiftAssign     (MemberInt32 ,Constant1));
        共通1(Expression.SubtractAssign       (MemberInt32 ,Constant1));
        共通1(Expression.SubtractAssignChecked(MemberInt32 ,Constant1));

        共通0(Expression.Coalesce             (ConstantString,ConstantString,ConversionString));

        共通0(Expression.Add                  (Constant1,Constant1,Method_int));
        共通0(Expression.AddChecked           (Constant1,Constant1,Method_int));
        共通0(Expression.And                  (Constant1,Constant1,Method_int));
        共通0(Expression.AndAlso              (ConstantBinry,ConstantBinry,typeof(演算子).GetMethod("op_BitwiseAnd")));
        共通0(Expression.Divide               (Constant1,Constant1,Method_int));
        共通0(Expression.ExclusiveOr          (Constant1,Constant1,Method_int));
        共通0(Expression.LeftShift            (Constant1,Constant1,Method_int));
        共通0(Expression.Modulo               (Constant1,Constant1,Method_int));
        共通0(Expression.Multiply             (Constant1,Constant1,Method_int));
        共通0(Expression.MultiplyChecked      (Constant1,Constant1,Method_int));
        共通0(Expression.Or                   (Constant1,Constant1,Method_int));
        共通0(Expression.OrElse               (ConstantBinry,ConstantBinry,typeof(演算子).GetMethod("op_BitwiseAnd")));
        共通0(Expression.Power                (Constant1d,Constant1d,Method(()=>Math.Pow(1,1))));
        共通0(Expression.RightShift           (Constant1,Constant1,Method_int));
        共通0(Expression.Subtract             (Constant1,Constant1,Method_int));
        共通0(Expression.SubtractChecked      (Constant1,Constant1,Method_int));
        共通1(Expression.AddAssign            (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expression.AddAssignChecked     (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expression.AndAssign            (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expression.DivideAssign         (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expression.ExclusiveOrAssign    (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expression.LeftShiftAssign      (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expression.ModuloAssign         (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expression.MultiplyAssign       (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expression.MultiplyAssignChecked(MemberInt32 ,Constant1 ,Method_int));
        共通1(Expression.OrAssign             (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expression.PowerAssign          (MemberDouble,Constant1d,Method_double));
        共通1(Expression.PowerAssign          (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expression.RightShiftAssign     (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expression.SubtractAssign       (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expression.SubtractAssignChecked(MemberInt32 ,Constant1 ,Method_int));

        共通1(Expression.AddAssign            (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expression.AddAssignChecked     (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expression.AndAssign            (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expression.DivideAssign         (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expression.ExclusiveOrAssign    (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expression.LeftShiftAssign      (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expression.ModuloAssign         (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expression.MultiplyAssign       (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expression.MultiplyAssignChecked(MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expression.OrAssign             (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expression.PowerAssign          (MemberDouble,Constant1d,Method_double,ConversionDouble));
        共通1(Expression.PowerAssign          (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expression.RightShiftAssign     (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expression.SubtractAssign       (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expression.SubtractAssignChecked(MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));

        共通0(Expression.Equal                (Constant1,Constant1,false,Method_int));
        共通0(Expression.GreaterThan          (Constant1,Constant1,false,Method_int));
        共通0(Expression.GreaterThanOrEqual   (Constant1,Constant1,false,Method_int));
        共通0(Expression.LessThan             (Constant1,Constant1,false,Method_int));
        共通0(Expression.LessThanOrEqual      (Constant1,Constant1,false,Method_int));
        共通0(Expression.NotEqual             (Constant1,Constant1,false,Method_int));
        void 共通0(BinaryExpression Binary){
            this.シリアライズMemoryMessageJsonコンパイル(
                Expression.Lambda<Func<object>>(
                    Expression.Block(
                        new[]{ParameterInt32},
                        Expression.Assign(
                            ParameterInt32,
                            Expression.Constant(0)
                        ),
                        Expression.Convert(
                            Binary,
                            typeof(object)
                        )
                    )
                )
            );
        }
        void 共通1(BinaryExpression Binary){
            this.シリアライズMemoryMessageJsonコンパイル(
                Expression.Lambda<Func<object>>(
                    Expression.Block(
                        Expression.Assign(MemberInt32,Constant0),
                        Expression.Convert(Binary,typeof(object))
                    )
                )
            );
        }
    }
    private static Reflection.MethodInfo Method<T>(Expression<Func<T>>e)=>((MethodCallExpression)e.Body).Method;
    //private static Reflection.MethodInfo Method<T,TResult>(Func<T,TResult>e)=>e.Method;
    //private static Reflection.MethodInfo Method<T>(Func<T>e)=>e.Method;
    private static Reflection.MethodInfo Method(string Name)=>typeof(TestExpression).GetMethod(Name,Reflection.BindingFlags.Static|Reflection.BindingFlags.NonPublic)!;
    [Fact]public void Block0(){
        this.シリアライズMemoryMessageJson(
            Expression.Block(
                new[] { ParameterDecimal },
                ParameterDecimal
            )
        );
    }
    [Fact]public void Block1(){
        var q= Expression.Parameter(typeof(decimal),"q");
        this.シリアライズMemoryMessageJson(
            Expression.Block(
                new[] { ParameterDecimal,q },
                ParameterDecimal
            )
        );
    }
    [Fact]public void Block2(){
        this.シリアライズMemoryMessageJson(
            Expression.Block(
                new[]{Expression.Parameter(typeof(decimal),"a"),Expression.Parameter(typeof(decimal),"b"),ParameterDecimal},
                ParameterDecimal
            )
        );
    }
    [Fact]public void Block4(){
        this.シリアライズMemoryMessageJson(
            Expression.Block(
                new[] { ParameterDecimal },
                Expression.TryCatchFinally(
                    Expression.Increment(ParameterDecimal),
                    ParameterDecimal
                )
            )
        );
        this.シリアライズMemoryMessageJson(
            Expression.Block(
                new[] { ParameterDecimal },
                Expression.TryCatchFinally(
                    Expression.Increment(ParameterDecimal),
                    Expression.Constant(2)
                )
            )
        );
        this.シリアライズMemoryMessageJson(
            Expression.Block(
                new[] { ParameterDecimal },
                Expression.TryCatchFinally(
                    Expression.PostIncrementAssign(ParameterDecimal),
                    Expression.Constant(2)
                )
            )
        );
        this.シリアライズMemoryMessageJson(
            Expression.Block(
                new[] { ParameterDecimal },
                Expression.TryCatchFinally(
                    ParameterDecimal,
                    ParameterDecimal
                ),
                ParameterDecimal
            )
        );
        this.シリアライズMemoryMessageJson(
            Expression.Block(
                new[] { ParameterDecimal },
                Expression.TryCatchFinally(
                    Expression.Constant(2),
                    ParameterDecimal
                )
            )
        );
    }
    [Fact]public void Block10(){
        this.シリアライズMemoryMessageJson(
            Expression.Block(
                Expression.Switch(
                    Expression.Constant(123),
                    Expression.Constant(0m),
                    Expression.SwitchCase(
                        Expression.Constant(64m),
                        Expression.Constant(124)
                    )
                )
            )
        );
        this.シリアライズMemoryMessageJson(
            Expression.Block(
                Expression.Switch(
                    Expression.Constant(123),
                    Expression.Constant(0m),
                    Expression.SwitchCase(
                        Expression.Constant(64m),
                        Expression.Constant(124)
                    )
                )
            )
        );
    }
    [Fact]public void Condition(){
        this.シリアライズMemoryMessageJson(
            Expression.Condition(
                Expression.Constant(true),
                Expression.Constant(1m),
                Expression.Constant(2m)
            )
        );
        this.シリアライズMemoryMessageJson(
            Expression.IfThenElse(
                Expression.Constant(true),
                Expression.Constant(1m),
                Expression.Constant(2m)
            )
        );
        this.シリアライズMemoryMessageJson(
            Expression.IfThen(
                Expression.Constant(true),
                Expression.Constant(1m)
            )
        );
    }
    [Fact]public void Constant0(){
        this.シリアライズMemoryMessageJson((Expression)Expression.Constant(null,typeof(string)));
    }
    [Fact]public void Constant1(){
        this.シリアライズMemoryMessageJson(Expression.Constant(1111m));
        this.シリアライズMemoryMessageJson(Expression.Constant(1111m,typeof(object)));
    }
    [Fact]public void Constructor(){
        this.シリアライズMemoryMessageJson(
            Expression.New(
                typeof(string).GetConstructor(new System.Type[]{typeof(char),typeof(int)})!,
                Expression.Constant('a'),
                Expression.Constant(2)
            )
        );
    }
    [Fact]public void Default(){
        this.シリアライズMemoryMessageJson(Expression.Default(typeof(int)));
        this.シリアライズMemoryMessageJson(Expression.Default(typeof(decimal)));
    }
    //[Fact]public void Dynamic(){
    //    var CallSiteBinder=new CallSiteBinder();
    //    this.共通Expression(Expressions.Expression.Dynamic(new CallSiteBinder(); typeof(int)));
    //    this.共通Expression(Expressions.Expression.Default(typeof(decimal)));
    //}
    private static CSharpArgumentInfo CSharpArgumentInfo1 = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
    private static CSharpArgumentInfo[]CSharpArgumentInfoArray1 = {
        CSharpArgumentInfo1
    };
    private static CSharpArgumentInfo[]CSharpArgumentInfoArray2 = {
        CSharpArgumentInfo1,
        CSharpArgumentInfo1
    };
    private static CSharpArgumentInfo[]CSharpArgumentInfoArray3 = {
        CSharpArgumentInfo1,
        CSharpArgumentInfo1,
        CSharpArgumentInfo1
    };
    private static CSharpArgumentInfo[]CSharpArgumentInfoArray4 = {
        CSharpArgumentInfo1,
        CSharpArgumentInfo1,
        CSharpArgumentInfo1,
        CSharpArgumentInfo1
    };
    [Fact]public void DynamicUnary(){
        var arg1=1;
        var binder=Binder.UnaryOperation(
            CSharpBinderFlags.None,
            ExpressionType.Increment,
            this.GetType(),
            CSharpArgumentInfoArray1
        );
        var Dynamic0=Expression.Dynamic(
            binder,
            typeof(object),
            Expression.Constant(arg1,typeof(object))
        );
        var CallSite=CallSite<Func<CallSite,object,object>>.Create(binder);
        this.共通Dynamic(CallSite.Target(CallSite,arg1),Dynamic0);
    }
    //[Fact]public void DynamicCreateInstance(){
    //    var binder=Binder.InvokeConstructor(
    //        CSharpBinderFlags.None,
    //        typeof(Expression),
    //        new CSharpArgumentInfo[]{
    //            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
    //        }
    //    );
    //    var  CallSite = CallSite<Func<CallSite,System.Type,object>>.Create(binder);
    //    {
    //        var Dynamic0 = Expressions.Expression.Dynamic(
    //            binder,
    //            typeof(object),
    //            Expressions.Expression.Constant(typeof(テスト))
    //        );
    //        this.共通Dynamic(CallSite.Target(CallSite,typeof(テスト)),Dynamic0);
    //    }
    //    //{
    //    //    var Constant_1L = Expressions.Expression.Constant(1L);
    //    //    var binder=Binder.Convert(
    //    //        CSharpBinderFlags.ConvertExplicit,
    //    //        typeof(double),
    //    //        this.GetType()
    //    //    );
    //    //    var Dynamic0=Expressions.Expression.Dynamic(
    //    //        binder,
    //    //        typeof(object),
    //    //        Constant_1L
    //    //    );
    //    //    var CallSite=CallSite<Func<CallSite,object,object>>.Create(binder);
    //    //    this.共通Dynamic(CallSite.Target(CallSite,Constant_1L),Dynamic0);
    //    //}
    //}
    [Fact]public void DynamicConvertImpliccit(){
        this.PrivateDynamicConvert<int,long>(1,CSharpBinderFlags.None);
        this.PrivateDynamicConvert<int,double>(1,CSharpBinderFlags.None);
        this.PrivateDynamicConvert<float,double>(1,CSharpBinderFlags.None);
    }
    [Fact]public void DynamicConvertExplicit(){
        this.PrivateDynamicConvert<long,int>(1,CSharpBinderFlags.ConvertExplicit);
        this.PrivateDynamicConvert<double,int>(1,CSharpBinderFlags.ConvertExplicit);
        this.PrivateDynamicConvert<double,float>(1,CSharpBinderFlags.ConvertExplicit);
    }
    private void PrivateDynamicConvert<TInput,TResult>(TInput input,CSharpBinderFlags Flag){
        var Constant = Expression.Constant(input);
        var binder=Binder.Convert(
            Flag,
            typeof(TResult),
            this.GetType()
        );
        var Dynamic0=Expression.Dynamic(
            binder,
            typeof(TResult),
            Constant
        );
        var CallSite=CallSite<Func<CallSite,object,TResult>>.Create(binder);
        var expected=CallSite.Target(CallSite,input!);
        this.シリアライズMemoryMessageJson(Dynamic0);
        var Lambda=Expression.Lambda<Func<TResult>>(Dynamic0);
        this.シリアライズMemoryMessageJson(Lambda);
        var M=Lambda.Compile();
        var actual=M();
        Assert.Equal(expected,actual);
    }
    [Fact]public void DynamicInvoke(){
        共通((int a,int b,int c)=>a==b&&a==c,1,2,3);
        共通((int a,int b,int c)=>a==b&&a==c,2,2,2);

        void 共通(object オブジェクト, object a,object b,object c){
            var binder=Binder.Invoke(
                CSharpBinderFlags.None,
                typeof(TestExpression),
                CSharpArgumentInfoArray4
            );
            var CallSite=CallSite<Func<CallSite,object,object,object,object,object>>.Create(binder);
            var Dynamic0 = Expression.Dynamic(
                binder,
                typeof(object),
                Expression.Constant(オブジェクト),
                Expression.Constant(a),
                Expression.Constant(b),
                Expression.Constant(c)
            );
            this.共通Dynamic(CallSite.Target(CallSite,オブジェクト,a,b,c),Dynamic0);
        }
    }
    [Fact]public void Display(){
        var o=new テスト();
        this.シリアライズデシリアライズ3パターン(o);
    }

    [Fact]public void DynamicInvokeMember(){
        引数名();
        Action("Action",1,2.0,"string");
        Func("Func",1,2.0,"string");

        void Action(string メンバー名,object @int,object @double,object @string){
            var o=new テスト();
            var binder=Binder.InvokeMember(
                CSharpBinderFlags.ResultDiscarded,//Actionの時に指定
                メンバー名,
                //Type.EmptyTypes,
                null,//new []{typeof(object),typeof(object),typeof(object)},
                //new []{引数0.GetType(),引数1.GetType(),引数2.GetType()},
                typeof(TestExpression),
                CSharpArgumentInfoArray4
            );
            var CallSite=CallSite<Action<CallSite,object,object,object,object>>.Create(binder);
            CallSite.Target(CallSite,o,@int,@double,@string);
            var Dynamic0 = Expression.Dynamic(
                binder,
                typeof(object),
                Expression.Constant(o),
                Expression.Constant(@int),
                Expression.Constant(@double),
                Expression.Constant(@string)
            );
            this.シリアライズMemoryMessageJson(Dynamic0);
            var Lambda=Expression.Lambda<Action>(Dynamic0);
            this.シリアライズMemoryMessageJson(Lambda);
            var M=Lambda.Compile();
            M();
        }
        void Func(string メンバー名,object @int,object @double,object @string){
            var o=new テスト();
            var binder=Binder.InvokeMember(
                CSharpBinderFlags.None,
                メンバー名,
                //Type.EmptyTypes,
                null,//new []{typeof(object),typeof(object),typeof(object)},
                //new []{引数0.GetType(),引数1.GetType(),引数2.GetType()},
                typeof(TestExpression),
                CSharpArgumentInfoArray4
            );
            var CallSite=CallSite<Func<CallSite,object,object,object,object,object>>.Create(binder);
            var Dynamic0 = Expression.Dynamic(
                binder,
                typeof(object),
                Expression.Constant(o),
                Expression.Constant(@int),
                Expression.Constant(@double),
                Expression.Constant(@string)
            );
            this.共通Dynamic(CallSite.Target(CallSite,o,@int,@double,@string),Dynamic0);
        }
        void 引数名(){
            var arg1=new テスト();
            var arg2=11;
            var arg3=22;
            var arg4="cc";
            var binder=Binder.InvokeMember(
                CSharpBinderFlags.ResultIndexed,
                "Func",
                null,
                this.GetType(),
                new[]{
                    //CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null),
                    CSharpArgumentInfo1,CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.NamedArgument,"a"),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null),
                    //CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.NamedArgument,"b"),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null),
                    //CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.NamedArgument,"c"),
                }
            );
            var Dynamic0=Expression.Dynamic(
                binder,
                typeof(object),
                Expression.Constant(arg1),
                Expression.Constant(arg2),
                Expression.Constant(arg3),
                Expression.Constant(arg4)
            );
            var CallSite=CallSite<Func<CallSite,object,object,object,object,object>>.Create(binder);
            this.共通Dynamic(CallSite.Target(CallSite,arg1,arg2,arg3,arg4),Dynamic0);
        }
    }
    [Fact]public void DynamicGetMember(){
        //if(a_GetMemberBinder!=null) {
        {
            var arg1=new TestDynamic<int>(1,2);
            var binder=Binder.GetMember(
                CSharpBinderFlags.None,
                nameof(TestDynamic<int>.メンバー1),
                this.GetType(),
                CSharpArgumentInfoArray1
            );
            var Dynamic0=Expression.Dynamic(
                binder,
                typeof(object),
                Expression.Constant(arg1)
            );
            this.シリアライズMemoryMessageJson(Dynamic0);
            var CallSite=CallSite<Func<CallSite,object,object>>.Create(binder);
            this.共通Dynamic(CallSite.Target(CallSite,arg1),Dynamic0);
        }
        //    return a_GetMemberBinder.Name.Equals(b_GetMemberBinder.Name,StringComparison.Ordinal);
        {
            var arg1=new TestDynamic<int>(1,2);
            var binder=Binder.GetMember(
                CSharpBinderFlags.ResultIndexed,
                nameof(TestDynamic<int>.メンバー1),
                this.GetType(),
                CSharpArgumentInfoArray1
            );
            var Dynamic0=Expression.Dynamic(
                binder,
                typeof(object),
                Expression.Constant(arg1)
            );
            this.シリアライズMemoryMessageJson(Dynamic0);
            var CallSite=CallSite<Func<CallSite,object,object>>.Create(binder);
            this.共通Dynamic(CallSite.Target(CallSite,arg1),Dynamic0);
        }
    }
    [Fact]public void DynamicSetMember(){
        var arg1=new TestDynamic<int>(1,2);
        var arg2=2;
        var binder=Binder.SetMember(
            CSharpBinderFlags.ResultIndexed,
            nameof(TestDynamic<int>.メンバー1),
            this.GetType(),
            CSharpArgumentInfoArray2
        );
        var Dynamic0=Expression.Dynamic(
            binder,
            typeof(object),
            Expression.Constant(arg1),
            Expression.Constant(arg2)
        );
        this.シリアライズMemoryMessageJson(Dynamic0);
        var CallSite=CallSite<Func<CallSite,object,object,object>>.Create(binder);
        this.共通Dynamic(CallSite.Target(CallSite,arg1,arg2),Dynamic0);
    }
    [Fact]public void DynamicGetIndex(){
        const int expected = 2;
        var arg1 = new[] {
            1,expected,3
        };
        var arg2=1;
        var binder=Binder.GetIndex(
            CSharpBinderFlags.None,
            this.GetType(),
            CSharpArgumentInfoArray2
        );
        var Dynamic0=Expression.Dynamic(
            binder,
            typeof(object),
            Expression.Constant(arg1),
            Expression.Constant(arg2)
        );
        this.シリアライズMemoryMessageJson(Dynamic0);
        var CallSite=CallSite<Func<CallSite,object,object,object>>.Create(binder);
        this.共通Dynamic(CallSite.Target(CallSite,arg1,arg2),Dynamic0);
    }
    [Fact]public void DynamicSetIndex(){
        var arg1= new[] {
            1,2,3
        };
        var arg2=1;
        var arg3=1;
        var binder=Binder.SetIndex(
            CSharpBinderFlags.None,
            this.GetType(),
            CSharpArgumentInfoArray3
        );
        var Dynamic0=Expression.Dynamic(
            binder,
            typeof(object),
            Expression.Constant(arg1),
            Expression.Constant(arg2),
            Expression.Constant(arg3)
        );
        this.シリアライズMemoryMessageJson(Dynamic0);
        var CallSite=CallSite<Func<CallSite,object,object,object,object>>.Create(binder);
        this.共通Dynamic(CallSite.Target(CallSite,arg1,arg2,arg3),Dynamic0);
    }
    [Fact]public void DynamicBinary(){
        var arg1=1;
        var arg2=1;
        var binder=Binder.BinaryOperation(
            CSharpBinderFlags.None,
            ExpressionType.Add,
            this.GetType(),
            new[]{
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null)
            }
        );
        var Dynamic0 = Expression.Dynamic(
            binder,
            typeof(object),
            Expression.Constant(arg1),
            Expression.Constant(arg2)
        );
        var CallSite=CallSite<Func<CallSite,object,object,object>>.Create(binder);
        this.共通Dynamic(CallSite.Target(CallSite,arg1,arg2),Dynamic0);
    }
    private void 共通Dynamic(object expected,DynamicExpression Dynamic0){
        this.シリアライズMemoryMessageJson(Dynamic0);
        var Lambda=Expression.Lambda<Func<object>>(Dynamic0);
        this.シリアライズMemoryMessageJson(Lambda);
        var M=Lambda.Compile();
        var actual=M();
        Assert.Equal(expected,actual);
    }
    [Fact]public void ElementInit(){
        var Type=typeof(BindCollection);
        var Int32フィールド1=Type.GetField(nameof(BindCollection.Int32フィールド1));
        var Int32フィールド2=Type.GetField(nameof(BindCollection.Int32フィールド2));
        var BindCollectionフィールド1=Type.GetField(nameof(BindCollection.BindCollectionフィールド1));
        var BindCollectionフィールド2=Type.GetField(nameof(BindCollection.BindCollectionフィールド2));
        var Listフィールド1=Type.GetField(nameof(BindCollection.Listフィールド1));
        var Listフィールド2=Type.GetField(nameof(BindCollection.Listフィールド2));
        var Constant_1=Expression.Constant(1);
        var Constant_2=Expression.Constant(2);
        var ctor=Type.GetConstructor(new[]{typeof(int)});
        var New=Expression.New(
            ctor,
            Constant_1
        );
        var Add=typeof(List<int>).GetMethod("Add");
        Expression.ElementInit(
            Add,
            Constant_1
        );
        {
            var l=new List<int>();
            this.シリアライズMemoryMessageJson(
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
        }
    }
    [Fact]public void Label(){
        var labelTarget=Expression.Label();
        this.シリアライズMemoryMessageJson(Expression.Label(labelTarget));
        this.シリアライズMemoryMessageJson(Expression.Label(labelTarget,Expression.Constant(1)));
    }
    [Fact]public void LabelTarget(){
        共通LabelTarget(Expression.Label());
        共通LabelTarget(Expression.Label(typeof(int)));
        共通LabelTarget(Expression.Label(typeof(double),"abc"));
        void 共通LabelTarget(LabelTarget input){
            Debug.Assert(input!=null,nameof(input)+" != null");
            this.シリアライズMemoryMessageJson<object>(input,output=>Assert.Equal(input,(LabelTarget)output,this.ExpressionEqualityComparer));
            this.シリアライズMemoryMessageJson(input,output=>Assert.Equal(input,output,this.ExpressionEqualityComparer));
        }
    }
    static LambdaExpression Lambda<T>(Expression<Func<T>> e)=>e;
    [Fact]
    public void Lambda0(){
        this.シリアライズMemoryMessageJson(Lambda(()=>1));
    }
    [Fact]public void Lambda1(){
        this.シリアライズMemoryMessageJson(Expression.Lambda<Func<decimal>>(Expression.Constant(2m)));
    }
    [Fact]public void Lambda3(){
        //const decimal Catch値 = 40, Finally値 = 30;
        //Expressions.Expression.TryCatchFinally(
        //    //Expressions.Expression.PostIncrementAssign(@decimal),
        //    Expressions.Expression.Constant(Finally値),
        //    Expressions.Expression.Constant(Finally値),
        //    Expressions.Expression.Catch(
        //        typeof(Exception),
        //        Expressions.Expression.Constant(Catch値)
        //    )
        //);
        this.シリアライズMemoryMessageJsonコンパイル(
            Expression.Lambda<Func<decimal>>(
                Expression.Block(
                    new[] { ParameterDecimal },
                    Expression.TryCatchFinally(
                        Expression.PostIncrementAssign(ParameterDecimal),
                        ParameterDecimal,
                        Expression.Catch(
                            typeof(Exception),
                            ParameterDecimal
                        )
                    )
                )
            )
        );
        this.シリアライズMemoryMessageJsonコンパイル(
            Expression.Lambda<Func<decimal,decimal>>(
                Expression.TryCatchFinally(
                    ParameterDecimal,
                    ParameterDecimal
                ),
                ParameterDecimal
            ),1
        );
        this.シリアライズMemoryMessageJsonコンパイル(
            Expression.Lambda<Func<decimal>>(
                Expression.Block(
                    new[] { ParameterDecimal },
                    Expression.TryCatchFinally(
                        ParameterDecimal,
                        ParameterDecimal
                    ),
                    ParameterDecimal
                )
            )
        );
        this.シリアライズMemoryMessageJsonコンパイル(
            Expression.Lambda<Func<decimal>>(
                Expression.Block(
                    new[] { ParameterDecimal },
                    Expression.TryCatchFinally(
                        Expression.PostIncrementAssign(ParameterDecimal),
                        Expression.Assign(
                            ParameterDecimal,
                            Expression.Constant(2m)
                        )
                    ),
                    ParameterDecimal
                )
            )
        );
        this.シリアライズMemoryMessageJsonコンパイル(
            Expression.Lambda<Func<decimal>>(
                Expression.Block(
                    new[] { ParameterDecimal },
                    Expression.Assign(
                        ParameterDecimal,
                        Expression.Constant(1m)
                    ),
                    Expression.TryCatchFinally(
                        Expression.PostIncrementAssign(ParameterDecimal),
                        Expression.Assign(
                            ParameterDecimal,
                            Expression.Constant(2m)
                        )
                    ),
                    ParameterDecimal
                )
            )
        );
        this.シリアライズMemoryMessageJsonコンパイル(
            Expression.Lambda<Func<decimal>>(
                Expression.Block(
                    new[] { ParameterDecimal },
                    Expression.Assign(
                        ParameterDecimal,
                        Expression.Constant(1m)
                    ),
                    Expression.TryCatchFinally(
                        Expression.PostIncrementAssign(ParameterDecimal),
                        Expression.Assign(
                            ParameterDecimal,
                            Expression.Constant(2m)
                        )
                    ),
                    ParameterDecimal
                )
            )
        );
    }
    [Fact]public void Lambda4(){
        var Exception=Expression.Parameter(typeof(Exception));
        this.シリアライズMemoryMessageJsonコンパイル(
            Expression.Lambda<Func<decimal,decimal>>(
                Expression.TryCatchFinally(
                    ParameterDecimal,
                    ParameterDecimal,
                    Expression.Catch(Exception,ParameterDecimal,Expression.Constant(true))
                ),
                ParameterDecimal
            ),1
        );
    }
    [Fact]public void Lambda5(){
        var Array2=Expression.Parameter(typeof(int[,]));
        this.シリアライズ(
            Expression.Lambda(
                Expression.ArrayIndex(
                    Array2,
                    Expression.Constant(0),
                    Expression.Constant(0)
                ),
                Array2
            )
        );
        var Array1=Expression.Parameter(typeof(int[]));
        this.シリアライズ(
            Expression.Lambda(
                Expression.ArrayIndex(
                    Array1,
                    Expression.Constant(0)
                ),
                Array1
            )
        );
    }
    [Fact]
    public void NewArrayInit(){
        this.シリアライズMemoryMessageJson(
            Expression.NewArrayInit(
                typeof(int),
                Expression.Constant(0),
                Expression.Constant(1)
            )
        );
    }
    [Fact]
    public void NewArrayBounds(){
        this.シリアライズMemoryMessageJson(
            Expression.NewArrayBounds(
                typeof(int),
                Expression.Constant(0),
                Expression.Constant(1)
            )
        );
    }
    class BindCollection
    {
        public int Int32フィールド1;
        public int Int32フィールド2;
        public BindCollection BindCollectionフィールド1;
        public BindCollection BindCollectionフィールド2;
        public readonly List<int> Listフィールド1 = new();
        public readonly List<int> Listフィールド2 = new();

        public BindCollection(int v)
        {
            this.Int32フィールド1 = 0;
            this.Int32フィールド2 = 0;
            this.BindCollectionフィールド1 = null;
            this.BindCollectionフィールド2 = null;
        }
    }
    [Fact]
    public void MemberInit(){
        var Type = typeof(BindCollection);
        var Int32フィールド1 = Type.GetField(nameof(BindCollection.Int32フィールド1));
        Assert.NotNull(Int32フィールド1);
        var Listフィールド1 = Type.GetField(nameof(BindCollection.Listフィールド1));
        Assert.NotNull(Listフィールド1);
        var Listフィールド2 = Type.GetField(nameof(BindCollection.Listフィールド2))!;
        Assert.NotNull(Listフィールド2);
        var Constant_1 = Expression.Constant(1);
        var ctor = Type.GetConstructor(new[] {
            typeof(int)
        });
        var New = Expression.New(
            ctor,
            Constant_1
        );
        this.シリアライズMemoryMessageJson(
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド1,
                    Constant_1
                )
            )
        );
        this.シリアライズMemoryMessageJson(
            Expression.MemberInit(
                New,
                Expression.ListBind(
                    Listフィールド2,
                    Expression.ElementInit(
                        typeof(List<int>).GetMethod("Add")!,
                        Constant_1
                    )
                )
            )
        );
    }
    private static readonly LabelTarget Label_decimal=Expression.Label(typeof(decimal),"Label_decimal");
    private static readonly LabelTarget Label_void=Expression.Label("Label");
    [Fact]
    public void Loop0(){
        this.シリアライズMemoryMessageJson(
            Expression.Loop(
                Expression.Block(
                    Expression.Break(Label_decimal,Expression.Constant(1m)),
                    Expression.Continue(Label_void)
                ),
                Label_decimal,
                Label_void
            )
        );
    }
    [Fact]public void Loop1(){
        this.シリアライズMemoryMessageJson(
            Expression.Loop(
                Expression.Block(
                    Expression.Break(Label_decimal,Expression.Constant(1m))
                ),
                Label_decimal
            )
        );
    }
    [Fact]
    public void Negate(){
        this.シリアライズMemoryMessageJson(Expression.Negate(Expression.Constant(1m)));
    }
    [Fact]public void Index0(){
        var List=Expression.Parameter(typeof(List<int>));
        this.シリアライズMemoryMessageJson((Expression)
            Expression.Block(
                new[] { List },
                Expression.MakeIndex(
                    List,
                    typeof(List<int>).GetProperty("Item"),
                    new []{Expression.Constant(0)}
                )
            )
        );
    }
    [Fact]public void Index1(){
        var Array1=Expression.Parameter(typeof(int[]));
        this.シリアライズMemoryMessageJson(
            Expression.Lambda(
                Expression.ArrayIndex(
                    Array1,
                    Expression.Constant(0)
                ),
                Array1
            )
        );
    }
    [Fact]
    public void Index2(){
        var Array2=Expression.Parameter(typeof(int[,]));
        this.シリアライズMemoryMessageJson(
            Expression.Lambda(
                Expression.ArrayIndex(
                    Array2,
                    Expression.Constant(0),
                    Expression.Constant(0)
                ),
                Array2
            )
        );
    }
    [Fact]
    public void Index3(){
        var Array2=Expression.Parameter(typeof(int[,,]));
        this.シリアライズMemoryMessageJson(
            Expression.Lambda(
                Expression.ArrayIndex(
                    Array2,
                    Expression.Constant(0),
                    Expression.Constant(0),
                    Expression.Constant(0)
                ),
                Array2
            )
        );
    }
    [Fact]
    public void Goto(){
        var target=Expression.Label(typeof(int),"target");
        this.シリアライズMemoryMessageJson(
            Expression.Block(
                Expression.Label(
                    target,
                    Expression.Constant(1)
                ),
                Expression.MakeGoto(
                    GotoExpressionKind.Return,
                    target,
                    Expression.Constant(5),
                    typeof(byte)
                )
            )
        );
        this.シリアライズMemoryMessageJson(
            Expression.Block(
                Expression.Label(
                    target,
                    Expression.Constant(1)
                ),
                Expression.MakeGoto(
                    GotoExpressionKind.Goto,
                    target,
                    Expression.Constant(2),
                    typeof(double)
                ),
                Expression.MakeGoto(
                    GotoExpressionKind.Break,
                    target,
                    Expression.Constant(3),
                    typeof(decimal)
                ),
                Expression.MakeGoto(
                    GotoExpressionKind.Continue,
                    target,
                    Expression.Constant(4),
                    typeof(float)
                ),
                Expression.MakeGoto(
                    GotoExpressionKind.Return,
                    target,
                    Expression.Constant(5),
                    typeof(byte)
                )
            )
        );
    }
    [Fact]
    public void ListInit(){
        this.シリアライズMemoryMessageJson(
            Expression.ListInit(
                Expression.New(typeof(List<int>)),
                Expression.ElementInit(typeof(List<int>).GetMethod("Add")!,Expression.Constant(1))
            )
        );
    }
    [Fact]public void MemberExpression(){
        var Point=Expression.Parameter(typeof(Point));
        this.シリアライズMemoryMessageJson(Expression.Block(new[]{Point},Expression.MakeMemberAccess(Point,typeof(Point).GetProperty("X")!)));
    }
    [Fact]public void MemberBinding(){
        var Type = typeof(BindCollection);
        var Int32フィールド1 = Type.GetField(nameof(BindCollection.Int32フィールド1));
        var Int32フィールド2 = Type.GetField(nameof(BindCollection.Int32フィールド2));
        var BindCollectionフィールド1 = Type.GetField(nameof(BindCollection.BindCollectionフィールド1));
        var BindCollectionフィールド2 = Type.GetField(nameof(BindCollection.BindCollectionフィールド2));
        var Listフィールド1 = Type.GetField(nameof(BindCollection.Listフィールド1));
        var Listフィールド2 = Type.GetField(nameof(BindCollection.Listフィールド2));
        var Constant_1 = Expression.Constant(1);
        var Constant_2 = Expression.Constant(2);
        var ctor = Type.GetConstructor(new[] {
            typeof(int)
        });
        var New = Expression.New(
            ctor,
            Constant_1
        );
        //if(a_Bindings.Count!=b_Bindings.Count) return false;
        this.シリアライズMemoryMessageJson(
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド1,
                    Constant_1
                ),
                Expression.Bind(
                    Int32フィールド2,
                    Constant_1
                )
            )
        );
    }
    private static Reflection.MethodInfo M(Expression<Action> f)=>
        ((MethodCallExpression)f.Body).Method;
    private static void StaticMethod(){}
    private static void StaticMethod(int a){}
    private static void StaticMethod(int a,int b){}
    private void InstanceMethod(){}
    private void InstanceMethod(int a){}
    private void InstanceMethod(int a,int b){}
    [Fact]public void MethodCall(){
        var o=new テスト();
        var arg=Expression.Constant(1);
        var @this=Expression.Constant(o);
        this.シリアライズMemoryMessageJson(Expression.Call(M(()=>テスト.StaticMethod())));
        this.シリアライズMemoryMessageJson(Expression.Call(M(()=>テスト.StaticMethod(1)),arg));
        this.シリアライズMemoryMessageJson(Expression.Call(M(()=>テスト.StaticMethod(1,2)),arg,arg));
        this.シリアライズMemoryMessageJson(Expression.Call(@this,M(()=>o.InstanceMethod())));
        this.シリアライズMemoryMessageJson(Expression.Call(@this,M(()=>o.InstanceMethod(1)),arg));
        this.シリアライズMemoryMessageJson(Expression.Call(@this,M(()=>o.InstanceMethod(1,2)),arg,arg));
        this.シリアライズMemoryMessageJson(
            Expression.Call(
                M(()=>string.Concat("","")),
                Expression.Constant("A"),
                Expression.Constant("B")
            )
        );
    }
    //[Fact]
    //public void Null(){
    //    this.共通Expression<Expressions.Expression?>(null);
    //}
    //[Fact]
    //public void GreaterThan(){
    //    this.共通Expression(Expressions.Expression.GreaterThan(Expressions.Expression.Constant(1m),Expressions.Expression.Constant(1m)));
    //}

    //[Fact]
    //public void Assign(){
    //    var @string=Expressions.Expression.Parameter(typeof(string));
    //    this.共通Expression(Expressions.Expression.Block(new[]{@string},Expressions.Expression.Assign(@string,@string)));
    //}
    [Fact]
    public void Invoke(){
        var @string=Expression.Parameter(typeof(string));
        this.シリアライズMemoryMessageJson(
            Expression.Invoke(
                Expression.Lambda(@string,@string),
                Expression.Constant("B")
            )
        );
    }
    [Fact]public void New(){
        this.シリアライズMemoryMessageJson(
            Expression.New(
                typeof(ValueTuple<int,int>).GetConstructors()[0],
                Expression.Constant(1),
                Expression.Constant(2)
            )
        );
    }
    static Reflection.MethodInfo M<T>(Expression<Func<T>> e){
        var Method=((MethodCallExpression)e.Body).Method;
        return Method;
    }
    [Fact]
    public void Parameter(){
        var p0=Expression.Parameter(typeof(int));
        this.シリアライズMemoryMessageJson(Expression.Lambda<Func<int,int>>(p0,p0));
        var p1=Expression.Parameter(typeof(int),"a");
        this.シリアライズMemoryMessageJson(Expression.Lambda<Func<int,int>>(p1,p1));
    }
    [Fact]
    public void Switch(){
        this.シリアライズMemoryMessageJson(
            Expression.Switch(
                Expression.Constant(123),
                Expression.Constant(0m),
                Expression.SwitchCase(
                    Expression.Constant(64m),
                    Expression.Constant(124)
                )
            )
        );
    }
    [Fact]
    public void Try(){
        this.シリアライズMemoryMessageJson(
            Expression.TryCatch(
                Expression.Constant(0),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0)
                )
            )
        );
        this.シリアライズMemoryMessageJson(
            Expression.TryCatchFinally(
                Expression.Default(typeof(void)),
                Expression.Default(typeof(void))
            )
        );
        this.シリアライズMemoryMessageJson(
            Expression.TryFault(
                Expression.Default(typeof(void)),
                Expression.Default(typeof(void))
            )
        );
        this.シリアライズMemoryMessageJson(
            Expression.TryFinally(
                Expression.Default(typeof(void)),
                Expression.Default(typeof(void))
            )
        );
    }
    [Fact]
    public void TypeEqual(){
        this.シリアライズMemoryMessageJson(
            Expression.TypeEqual(
                Expression.Constant(1m),
                typeof(decimal)
            )
        );
    }
    [Fact]public void TypeIs(){
        this.シリアライズMemoryMessageJson(
            Expression.TypeIs(
                Expression.Constant(1m),
                typeof(decimal)
            )
        );
    }
    private static 演算子 Unary演算子(演算子 a)=>~a;
    private static bool IsTrue演算子(演算子 a)=>a.HasValue;
    static int UnaryDouble(double a){
        return (int)a;
    }
    [Fact]public void Unary(){
        var ConstantArray = Expression.Constant(new int[10]);
        var Constant1= Expression.Constant(1);
        var Constant1_1d= Expression.Constant(1.1);
        var ConstantTrue= Expression.Constant(true);
        var Constant演算子=Expression.Constant(new 演算子(true));
        var Constant演算子1=Expression.Constant(new 演算子1(true));
        var Parameter演算子=Expression.Parameter(typeof(演算子));
        var ParameterInt32=Expression.Parameter(typeof(int));
        this.シリアライズMemoryMessageJson(Expression.ArrayLength(Expression.Constant(new int[1])));
        共通1(Expression.ArrayLength(ConstantArray));
        共通1(Expression.Quote(Expression.Lambda(ConstantArray)));
        共通1(Expression.Convert(Constant1_1d,typeof(int)));
        共通1(Expression.ConvertChecked(Constant1_1d,typeof(int)));
        共通1(Expression.Decrement(Constant1_1d));
        共通1(Expression.Increment(Constant1_1d));
        共通1(Expression.IsFalse(ConstantTrue));
        共通1(Expression.IsTrue(ConstantTrue));
        共通1(Expression.Negate(Constant1_1d));
        共通1(Expression.NegateChecked(Constant1_1d));
        共通1(Expression.OnesComplement(Constant1));
        共通1(Expression.Decrement(Constant1_1d));
        共通1(Expression.Increment(Constant1_1d));
        共通0(ParameterInt32,Constant1,Expression.PostDecrementAssign(ParameterInt32));
        共通0(ParameterInt32,Constant1,Expression.PostIncrementAssign(ParameterInt32));
        共通0(ParameterInt32,Constant1,Expression.PreDecrementAssign(ParameterInt32));
        共通0(ParameterInt32,Constant1,Expression.PreIncrementAssign(ParameterInt32));
        共通1(Expression.UnaryPlus(Constant1_1d));

        共通1(Expression.Convert(Constant演算子,typeof(演算子1)));
        共通1(Expression.ConvertChecked(Constant演算子,typeof(演算子1)));
        共通1(Expression.Convert(Constant演算子1,typeof(演算子)));
        共通1(Expression.ConvertChecked(Constant演算子1,typeof(演算子)));
        共通1(Expression.Decrement(Constant演算子));
        共通1(Expression.Increment(Constant演算子));
        共通1(Expression.IsFalse(Constant演算子));
        共通1(Expression.IsTrue(Constant演算子));
        共通1(Expression.Negate(Constant演算子));
        共通1(Expression.NegateChecked(Constant演算子));
        共通1(Expression.OnesComplement(Constant演算子,Method(nameof(Unary演算子))));
        共通1(Expression.Decrement(Constant演算子));
        共通1(Expression.Increment(Constant演算子));
        共通0(Parameter演算子,Constant演算子,Expression.PostDecrementAssign(Parameter演算子));
        共通0(Parameter演算子,Constant演算子,Expression.PostIncrementAssign(Parameter演算子));
        共通0(Parameter演算子,Constant演算子,Expression.PreDecrementAssign(Parameter演算子));
        共通0(Parameter演算子,Constant演算子,Expression.PreIncrementAssign(Parameter演算子));
        共通1(Expression.UnaryPlus(Constant演算子));


        共通1(Expression.Convert(Constant1_1d,typeof(int),Method(()=>UnaryDouble(0))));
        共通1(Expression.ConvertChecked(Constant1_1d,typeof(int),Method(()=>UnaryDouble(0))));
        共通1(Expression.Decrement(Constant1_1d,Method(()=>UnaryDouble(0))));
        共通1(Expression.Increment(Constant1_1d,Method(()=>UnaryDouble(0))));
        共通1(Expression.IsFalse(Constant演算子,Method(nameof(IsTrue演算子))));
        共通1(Expression.IsTrue(Constant演算子,Method(nameof(IsTrue演算子))));
        共通1(Expression.Negate(Constant1_1d,Method(()=>UnaryDouble(0))));
        共通1(Expression.NegateChecked(Constant1_1d,Method(()=>UnaryDouble(0))));
        共通1(Expression.OnesComplement(Constant演算子,Method(nameof(Unary演算子))));
        共通1(Expression.Decrement(Constant演算子,Method(nameof(Unary演算子))));
        共通1(Expression.Increment(Constant演算子,Method(nameof(Unary演算子))));
        共通1(Expression.UnaryPlus(Constant演算子,Method(nameof(Unary演算子))));
        void 共通0(ParameterExpression 代入先,ConstantExpression 代入元,UnaryExpression a){
            this.シリアライズMemoryMessageJsonコンパイル(
                Expression.Lambda<Func<object>>(
                    Expression.Block(
                        new[]{代入先},
                        Expression.Assign(
                            代入先,
                            代入元
                        ),
                        Expression.Convert(
                            a,
                            typeof(object)
                        )
                    )
                )
            );
        }
        void 共通1(UnaryExpression Unary){
            this.シリアライズMemoryMessageJsonコンパイル(
                Expression.Lambda<Func<object>>(
                    Expression.Convert(
                        Unary,
                        typeof(object)
                    )
                )
            );
        }
    }
    [Fact]public void DebugInfo(){
        var SymbolDocument0=Expression.SymbolDocument("SymbolDocument0.cs");
        this.シリアライズMemoryMessageJson(
            Expression.DebugInfo(SymbolDocument0,1,1,3,10)
        );
    }
}
