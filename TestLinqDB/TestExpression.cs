using System.Diagnostics;
//using System.Linq.Expressions;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
//using System.Linq.Expressions;
//using Expressions = System.Linq.Expressions;
using RuntimeBinder = Microsoft.CSharp.RuntimeBinder;
//using Binder = Microsoft.CSharp.RuntimeBinder;
//using MessagePack;
namespace TestLinqDB;
using Expressions = System.Linq.Expressions;
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
        this.BindCollectionフィールド1 = null!;
        this.BindCollectionフィールド2 = null!;
    }
}
[Serializable,MemoryPack.MemoryPackable,MessagePack.MessagePackObject(true)]
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
[Serializable,MemoryPack.MemoryPackable,MessagePack.MessagePackObject(true)]
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
[Serializable,MemoryPack.MemoryPackable,MessagePack.MessagePackObject(true)]
public partial class テスト:IEquatable<テスト>{
    public static int StaticMethodInt32()=>0;
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
    public int this[int index]{
        get=>1;
        set{}
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
[Serializable,MemoryPack.MemoryPackable,MessagePack.MessagePackObject(true)]
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
public class Serializer:共通{
    protected override テストオプション テストオプション{get;}=テストオプション.MemoryPack_MessagePack_Utf8Json;
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
    //private static readonly Expressions.Expressions.ParameterExpression int8    = Expressions.Expression.Parameter(typeof(sbyte),"int8");
    //private static readonly Expressions.Expressions.ParameterExpression int16   = Expressions.Expression.Parameter(typeof(short),"int16");
    //private static readonly Expressions.Expressions.ConstantExpression  int32   = Expressions.Expression.Constant(1);
    //private static readonly Expressions.Expressions.ParameterExpression int64   = Expressions.Expression.Parameter(typeof(long),"int64");
    //private static readonly Expressions.Expressions.ParameterExpression uint8   = Expressions.Expression.Parameter(typeof(byte),"uint8");
    //private static readonly Expressions.Expressions.ParameterExpression uint16  = Expressions.Expression.Parameter(typeof(ushort),"uint16");
    //private static readonly Expressions.Expressions.ParameterExpression uint32  = Expressions.Expression.Parameter(typeof(uint),"uint32");
    //private static readonly Expressions.Expressions.ParameterExpression uint64  = Expressions.Expression.Parameter(typeof(ulong),"uint64");
    //private static readonly Expressions.Expressions.ParameterExpression @float  = Expressions.Expression.Parameter(typeof(float),"float");
    //private static readonly Expressions.Expressions.ConstantExpression  @double = Expressions.Expression.Constant(1.0);
    //private static readonly Expressions.Expressions.ConstantExpression @string  = Expressions.Expressions.Expression.Constant("string");
    //private static readonly Expressions.Expressions.ConstantExpression array    = Expressions.Expressions.Expression.Constant(new int[1]);
    //private static readonly Expressions.Expressions.ConstantExpression @bool    = Expressions.Expressions.Expression.Constant(true);
    private static readonly Expressions.ParameterExpression ParameterDecimal = Expressions.Expression.Parameter(typeof(decimal),"decimal");

    private static readonly Expressions.ConstantExpression int8 = Expressions.Expression.Constant((sbyte)1);
    private static readonly Expressions.ConstantExpression int16 = Expressions.Expression.Constant((short)1);
    private static readonly Expressions.ConstantExpression int32 = Expressions.Expression.Constant(1);
    private static readonly Expressions.ConstantExpression int64 = Expressions.Expression.Constant((long)1);
    private static readonly Expressions.ConstantExpression uint8 = Expressions.Expression.Constant((byte)1);
    private static readonly Expressions.ConstantExpression uint16 = Expressions.Expression.Constant((ushort)1);
    private static readonly Expressions.ConstantExpression uint32 = Expressions.Expression.Constant((uint)1);
    private static readonly Expressions.ConstantExpression uint64 = Expressions.Expression.Constant((ulong)1);
    private static readonly Expressions.ConstantExpression @float = Expressions.Expression.Constant((float)1);
    private static readonly Expressions.ConstantExpression Constant1d = Expressions.Expression.Constant(1.0);
    //private static readonly Expressions.Expressions.ConstantExpression @decimal = Expressions.Expressions.Expression.Constant(typeof(decimal),"decimal");
    private static readonly Expressions.ConstantExpression @string = Expressions.Expression.Constant("string");
    private static readonly Expressions.ConstantExpression array = Expressions.Expression.Constant(new int[10]);
    private static readonly Expressions.ConstantExpression @bool = Expressions.Expression.Constant(true);
    //private static readonly Expressions.ParameterExpression[] Parameters={int8,int16,int32,int64,uint8,uint16,uint32,uint64,@float,@double,@decimal,@string};
    //private static readonly Expressions.ParameterExpression @int = Expressions.Expressions.Expression.Parameter(typeof(decimal),"int");
    //private static readonly Expressions.ParameterExpression @int = Expressions.Expressions.Expression.Parameter(typeof(decimal),"int");
    //private static readonly Expressions.ParameterExpression @decimal = Expressions.Expressions.Expression.Parameter(typeof(decimal),"decimal");
    private static int FieldInt32;
    private static readonly Expressions.MemberExpression MemberInt32 =Expressions.Expression.MakeMemberAccess(
        null,
        typeof(Serializer).GetField(nameof(FieldInt32),Reflection.BindingFlags.Static|Reflection.BindingFlags.NonPublic)!
    );
    private static double FieldDouble;
    private static readonly Expressions.MemberExpression MemberDouble =Expressions.Expression.MakeMemberAccess(
        null,
        typeof(Serializer).GetField(nameof(FieldDouble),Reflection.BindingFlags.Static|Reflection.BindingFlags.NonPublic)!
    );
    private static string FieldString;
    private static Expressions.MemberExpression MemberString=Expressions.Expression.MakeMemberAccess(
        null,
        typeof(Serializer).GetField(nameof(FieldString),Reflection.BindingFlags.Static|Reflection.BindingFlags.NonPublic)!
    );
    private static bool FieldBoolean;
    private static Expressions.MemberExpression MemberBoolean =Expressions.Expression.MakeMemberAccess(
        null,
        typeof(Serializer).GetField(nameof(FieldBoolean),Reflection.BindingFlags.Static|Reflection.BindingFlags.NonPublic)!
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
        var ParameterDouble=Expressions.Expression.Parameter(typeof(double));
        var ParameterInt32= Expressions.Expression.Parameter(typeof(int),"int32");
        var ParameterString= Expressions.Expression.Parameter(typeof(string),"string");
        var ConstantString = Expressions.Expression.Constant("string");
        //var ConstantStringNull=Expressions.Expression.Constant(null,typeof(string));
        var ConstantArray = Expressions.Expression.Constant(new int[10]);
        var Constant0= Expressions.Expression.Constant(0);
        var Constant1= Expressions.Expression.Constant(1);
        var ConstantTrue = Expressions.Expression.Constant(true);
        var ConstantBinry= Expressions.Expression.Constant(new 演算子(true));
        var ConversionInt32=Expressions.Expression.Lambda<Func<int,int>>(Expressions.Expression.Add(ParameterInt32,ParameterInt32),ParameterInt32);
        var ConversionDouble=Expressions.Expression.Lambda<Func<double,double>>(Expressions.Expression.Add(ParameterDouble,ParameterDouble),ParameterDouble);
        var ConversionString=Expressions.Expression.Lambda<Func<string,string>>(Expressions.Expression.Call(null,GetMethod(()=>string_string_string("","")),ParameterString,ParameterString),ParameterString);
        var Method_int=GetMethod(()=>int_int_int(1,1));
        var Method_double=GetMethod(()=>double_dobuble_double(1,1));
        //var Method_bool=Method(()=>AndAlso(true,true));
        //var Method_NullableBoolean=Method(()=>bool_bool_bool(true,true));
        共通0(Expressions.Expression.ArrayIndex(ConstantArray,Constant1));
        共通0(Expressions.Expression.Assign(ParameterInt32,Constant1));
        共通0(Expressions.Expression.Coalesce(ConstantString,ConstantString));
        共通0(Expressions.Expression.Add                  (Constant1,Constant1));
        共通0(Expressions.Expression.AddChecked           (Constant1,Constant1));
        共通0(Expressions.Expression.And                  (Constant1,Constant1));
        共通0(Expressions.Expression.AndAlso              (ConstantBinry,ConstantBinry));
        共通0(Expressions.Expression.Divide               (Constant1,Constant1));
        共通0(Expressions.Expression.ExclusiveOr          (Constant1,Constant1));
        共通0(Expressions.Expression.LeftShift            (Constant1,Constant1));
        共通0(Expressions.Expression.Modulo               (Constant1,Constant1));
        共通0(Expressions.Expression.Multiply             (Constant1,Constant1));
        共通0(Expressions.Expression.MultiplyChecked      (Constant1,Constant1));
        共通0(Expressions.Expression.Or                   (Constant1,Constant1));
        共通0(Expressions.Expression.OrElse               (ConstantTrue,ConstantTrue));
        共通0(Expressions.Expression.Power                (Constant1d,Constant1d));
        共通0(Expressions.Expression.RightShift           (Constant1,Constant1));
        共通0(Expressions.Expression.Subtract             (Constant1,Constant1));
        共通0(Expressions.Expression.SubtractChecked      (Constant1,Constant1));
        共通0(Expressions.Expression.Equal                (Constant1,Constant1));
        共通0(Expressions.Expression.GreaterThan          (Constant1,Constant1));
        共通0(Expressions.Expression.GreaterThanOrEqual   (Constant1,Constant1));
        共通0(Expressions.Expression.LessThan             (Constant1,Constant1));
        共通0(Expressions.Expression.LessThanOrEqual      (Constant1,Constant1));
        共通0(Expressions.Expression.NotEqual             (Constant1,Constant1));
        共通0(Expressions.Expression.LessThanOrEqual      (Constant1,Constant1));
        共通0(Expressions.Expression.NotEqual             (Constant1,Constant1));
        共通1(Expressions.Expression.AddAssign            (MemberInt32 ,Constant1));
        共通1(Expressions.Expression.AddAssignChecked     (MemberInt32 ,Constant1));
        共通1(Expressions.Expression.AndAssign            (MemberInt32 ,Constant1));
        共通1(Expressions.Expression.DivideAssign         (MemberInt32 ,Constant1));
        共通1(Expressions.Expression.ExclusiveOrAssign    (MemberInt32 ,Constant1));
        共通1(Expressions.Expression.LeftShiftAssign      (MemberInt32 ,Constant1));
        共通1(Expressions.Expression.ModuloAssign         (MemberInt32 ,Constant1));
        共通1(Expressions.Expression.MultiplyAssign       (MemberInt32 ,Constant1));
        共通1(Expressions.Expression.MultiplyAssignChecked(MemberInt32 ,Constant1));
        共通1(Expressions.Expression.OrAssign             (MemberInt32 ,Constant1));
        共通1(Expressions.Expression.PowerAssign          (MemberDouble,Constant1d));
        共通1(Expressions.Expression.RightShiftAssign     (MemberInt32 ,Constant1));
        共通1(Expressions.Expression.SubtractAssign       (MemberInt32 ,Constant1));
        共通1(Expressions.Expression.SubtractAssignChecked(MemberInt32 ,Constant1));

        共通0(Expressions.Expression.Coalesce             (ConstantString,ConstantString,ConversionString));

        共通0(Expressions.Expression.Add                  (Constant1,Constant1,Method_int));
        共通0(Expressions.Expression.AddChecked           (Constant1,Constant1,Method_int));
        共通0(Expressions.Expression.And                  (Constant1,Constant1,Method_int));
        共通0(Expressions.Expression.AndAlso              (ConstantBinry,ConstantBinry,typeof(演算子).GetMethod("op_BitwiseAnd")));
        共通0(Expressions.Expression.Divide               (Constant1,Constant1,Method_int));
        共通0(Expressions.Expression.ExclusiveOr          (Constant1,Constant1,Method_int));
        共通0(Expressions.Expression.LeftShift            (Constant1,Constant1,Method_int));
        共通0(Expressions.Expression.Modulo               (Constant1,Constant1,Method_int));
        共通0(Expressions.Expression.Multiply             (Constant1,Constant1,Method_int));
        共通0(Expressions.Expression.MultiplyChecked      (Constant1,Constant1,Method_int));
        共通0(Expressions.Expression.Or                   (Constant1,Constant1,Method_int));
        共通0(Expressions.Expression.OrElse               (ConstantBinry,ConstantBinry,typeof(演算子).GetMethod("op_BitwiseAnd")));
        共通0(Expressions.Expression.Power                (Constant1d,Constant1d,GetMethod(()=>Math.Pow(1,1))));
        共通0(Expressions.Expression.RightShift           (Constant1,Constant1,Method_int));
        共通0(Expressions.Expression.Subtract             (Constant1,Constant1,Method_int));
        共通0(Expressions.Expression.SubtractChecked      (Constant1,Constant1,Method_int));
        共通1(Expressions.Expression.AddAssign            (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expressions.Expression.AddAssignChecked     (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expressions.Expression.AndAssign            (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expressions.Expression.DivideAssign         (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expressions.Expression.ExclusiveOrAssign    (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expressions.Expression.LeftShiftAssign      (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expressions.Expression.ModuloAssign         (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expressions.Expression.MultiplyAssign       (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expressions.Expression.MultiplyAssignChecked(MemberInt32 ,Constant1 ,Method_int));
        共通1(Expressions.Expression.OrAssign             (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expressions.Expression.PowerAssign          (MemberDouble,Constant1d,Method_double));
        共通1(Expressions.Expression.PowerAssign          (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expressions.Expression.RightShiftAssign     (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expressions.Expression.SubtractAssign       (MemberInt32 ,Constant1 ,Method_int));
        共通1(Expressions.Expression.SubtractAssignChecked(MemberInt32 ,Constant1 ,Method_int));

        共通1(Expressions.Expression.AddAssign            (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expressions.Expression.AddAssignChecked     (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expressions.Expression.AndAssign            (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expressions.Expression.DivideAssign         (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expressions.Expression.ExclusiveOrAssign    (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expressions.Expression.LeftShiftAssign      (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expressions.Expression.ModuloAssign         (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expressions.Expression.MultiplyAssign       (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expressions.Expression.MultiplyAssignChecked(MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expressions.Expression.OrAssign             (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expressions.Expression.PowerAssign          (MemberDouble,Constant1d,Method_double,ConversionDouble));
        共通1(Expressions.Expression.PowerAssign          (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expressions.Expression.RightShiftAssign     (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expressions.Expression.SubtractAssign       (MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));
        共通1(Expressions.Expression.SubtractAssignChecked(MemberInt32 ,Constant1 ,Method_int,ConversionInt32 ));

        共通0(Expressions.Expression.Equal                (Constant1,Constant1,false,Method_int));
        共通0(Expressions.Expression.GreaterThan          (Constant1,Constant1,false,Method_int));
        共通0(Expressions.Expression.GreaterThanOrEqual   (Constant1,Constant1,false,Method_int));
        共通0(Expressions.Expression.LessThan             (Constant1,Constant1,false,Method_int));
        共通0(Expressions.Expression.LessThanOrEqual      (Constant1,Constant1,false,Method_int));
        共通0(Expressions.Expression.NotEqual             (Constant1,Constant1,false,Method_int));
        void 共通0(Expressions.BinaryExpression Binary){
            this.ExpressionシリアライズAssertEqual(Binary);
            this.Expression実行AssertEqual(
                Expressions.Expression.Lambda<Func<object>>(
                    Expressions.Expression.Block(
                        new[]{ParameterInt32},
                        Expressions.Expression.Assign(
                            ParameterInt32,
                            Expressions.Expression.Constant(0)
                        ),
                        Expressions.Expression.Convert(
                            Binary,
                            typeof(object)
                        )
                    )
                )
            );
        }
        void 共通1(Expressions.BinaryExpression Binary){
            this.ExpressionシリアライズAssertEqual(Binary);
            this.Expression実行AssertEqual(
                Expressions.Expression.Lambda<Func<object>>(
                    Expressions.Expression.Block(
                        Expressions.Expression.Assign(MemberInt32,Constant0),
                        Expressions.Expression.Convert(Binary,typeof(object))
                    )
                )
            );
        }
    }
    [Fact]public void Block0(){
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Block(
                new[] { ParameterDecimal },
                ParameterDecimal
            )
        );
    }
    [Fact]public void Block1(){
        var q= Expressions.Expression.Parameter(typeof(decimal),"q");
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Block(
                new[] { ParameterDecimal,q },
                ParameterDecimal
            )
        );
    }
    [Fact]public void Block2(){
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Block(
                new[]{Expressions.Expression.Parameter(typeof(decimal),"a"),Expressions.Expression.Parameter(typeof(decimal),"b"),ParameterDecimal},
                ParameterDecimal
            )
        );
    }
    [Fact]public void Block4(){
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Block(
                new[] { ParameterDecimal },
                Expressions.Expression.Assign(ParameterDecimal,Expressions.Expression.Constant(0m)),
                Expressions.Expression.TryCatchFinally(
                    Expressions.Expression.Increment(ParameterDecimal),
                    ParameterDecimal
                )
            )
        );
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Block(
                new[] { ParameterDecimal },
                Expressions.Expression.Assign(ParameterDecimal,Expressions.Expression.Constant(0m)),
                Expressions.Expression.TryCatchFinally(
                    Expressions.Expression.Increment(ParameterDecimal),
                    Expressions.Expression.Constant(2)
                )
            )
        );
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Block(
                new[] { ParameterDecimal },
                Expressions.Expression.Assign(ParameterDecimal,Expressions.Expression.Constant(0m)),
                Expressions.Expression.TryCatchFinally(
                    Expressions.Expression.PostIncrementAssign(ParameterDecimal),
                    Expressions.Expression.Constant(2)
                )
            )
        );
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Block(
                new[] { ParameterDecimal },
                Expressions.Expression.TryCatchFinally(
                    ParameterDecimal,
                    ParameterDecimal
                ),
                ParameterDecimal
            )
        );
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Block(
                new[] { ParameterDecimal },
                Expressions.Expression.TryCatchFinally(
                    Expressions.Expression.Constant(2),
                    ParameterDecimal
                )
            )
        );
    }
    [Fact]public void Block10(){
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Block(
                Expressions.Expression.Switch(
                    Expressions.Expression.Constant(123),
                    Expressions.Expression.Constant(0m),
                    Expressions.Expression.SwitchCase(
                        Expressions.Expression.Constant(64m),
                        Expressions.Expression.Constant(124)
                    )
                )
            )
        );
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Block(
                Expressions.Expression.Switch(
                    Expressions.Expression.Constant(123),
                    Expressions.Expression.Constant(0m),
                    Expressions.Expression.SwitchCase(
                        Expressions.Expression.Constant(64m),
                        Expressions.Expression.Constant(124)
                    )
                )
            )
        );
    }
    [Fact]public void Condition(){
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Condition(
                Expressions.Expression.Constant(true),
                Expressions.Expression.Constant(1m),
                Expressions.Expression.Constant(2m)
            )
        );
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.IfThenElse(
                Expressions.Expression.Constant(true),
                Expressions.Expression.Constant(1m),
                Expressions.Expression.Constant(2m)
            )
        );
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.IfThen(
                Expressions.Expression.Constant(true),
                Expressions.Expression.Constant(1m)
            )
        );
    }
    [Fact]public void Constant0(){
        this.ExpressionシリアライズAssertEqual((Expressions.Expression)Expressions.Expression.Constant(null,typeof(string)));
    }
    [Fact]public void Constant1(){
        this.ExpressionシリアライズAssertEqual(Expressions.Expression.Constant(1111m));
        this.ExpressionシリアライズAssertEqual(Expressions.Expression.Constant(1111m,typeof(object)));
    }
    [Fact]public void Constructor(){
        this.ExpressionシリアライズAssertEqual(
            typeof(string).GetConstructor(new[]{typeof(char),typeof(int)}),Assert.NotNull
        );
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.New(
                typeof(string).GetConstructor(new[]{typeof(char),typeof(int)})!,
                Expressions.Expression.Constant('a'),
                Expressions.Expression.Constant(2)
            )
        );
    }
    [Fact]public void DebugInfo(){
        var SymbolDocument0=Expressions.Expression.SymbolDocument("SymbolDocument0.cs");
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.DebugInfo(SymbolDocument0,1,1,3,10)
        );
    }
    [Fact]public void Default(){
        this.ExpressionシリアライズAssertEqual(Expressions.Expression.Default(typeof(int)));
        this.ExpressionシリアライズAssertEqual(Expressions.Expression.Default(typeof(decimal)));
    }
    //[Fact]public void Dynamic(){
    //    var CallSiteBinder=new CallSiteBinder();
    //    this.共通Expressions.Expression(Expressions.Expressions.Expression.Dynamic(new CallSiteBinder(); typeof(int)));
    //    this.共通Expressions.Expression(Expressions.Expressions.Expression.Default(typeof(decimal)));
    //}
    private static readonly RuntimeBinder.CSharpArgumentInfo CSharpArgumentInfo1 = RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None, null);
    private static readonly RuntimeBinder.CSharpArgumentInfo[]CSharpArgumentInfoArray1 = {
        CSharpArgumentInfo1
    };
    private static readonly RuntimeBinder.CSharpArgumentInfo[]CSharpArgumentInfoArray2 = {
        CSharpArgumentInfo1,
        CSharpArgumentInfo1
    };
    private static readonly RuntimeBinder.CSharpArgumentInfo[]CSharpArgumentInfoArray3 = {
        CSharpArgumentInfo1,
        CSharpArgumentInfo1,
        CSharpArgumentInfo1
    };
    private static readonly RuntimeBinder.CSharpArgumentInfo[]CSharpArgumentInfoArray4 = {
        CSharpArgumentInfo1,
        CSharpArgumentInfo1,
        CSharpArgumentInfo1,
        CSharpArgumentInfo1
    };
    [Fact]public void DynamicUnary0(){
        var arg1=1;
        var binder=RuntimeBinder.Binder.UnaryOperation(
            RuntimeBinder.CSharpBinderFlags.None,
            Expressions.ExpressionType.Increment,
            this.GetType(),
            new[]{
                RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None, null),
                //RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.NamedArgument,"a"),
            }
        );
        var Dynamic0=Expressions.Expression.Dynamic(
            binder,
            typeof(object),
            Expressions.Expression.Constant(arg1,typeof(object))
        );
        var CallSite=CallSite<Func<CallSite,object,object>>.Create(binder);
        this.共通Dynamic(CallSite.Target(CallSite,arg1),Dynamic0);
    }
    public dynamic dddd(dynamic x){
        return this[x];
    }
    public int this[int index]{
        get=>0;
        set{}
    }
    //[Fact]public void DynamicCreateInstance(){
    //    var binder=RuntimeBinder.Binder.InvokeConstructor(
    //        RuntimeBinder.CSharpBinderFlags.None,
    //        typeof(Expressions.Expression),
    //        new RuntimeBinder.CSharpArgumentInfo[]{
    //            RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.UseCompileTimeType, null)
    //        }
    //    );
    //    var  CallSite = CallSite<Func<CallSite,System.Type,object>>.Create(binder);
    //    {
    //        var Dynamic0 = Expressions.Expressions.Expression.Dynamic(
    //            binder,
    //            typeof(object),
    //            Expressions.Expressions.Expression.Constant(typeof(テスト))
    //        );
    //        this.共通Dynamic(CallSite.Target(CallSite,typeof(テスト)),Dynamic0);
    //    }
    //    //{
    //    //    var Constant_1L = Expressions.Expressions.Expression.Constant(1L);
    //    //    var binder=RuntimeBinder.Binder.Convert(
    //    //        RuntimeBinder.CSharpBinderFlags.ConvertExplicit,
    //    //        typeof(double),
    //    //        this.GetType()
    //    //    );
    //    //    var Dynamic0=Expressions.Expressions.Expression.Dynamic(
    //    //        binder,
    //    //        typeof(object),
    //    //        Constant_1L
    //    //    );
    //    //    var CallSite=CallSite<Func<CallSite,object,object>>.Create(binder);
    //    //    this.共通Dynamic(CallSite.Target(CallSite,Constant_1L),Dynamic0);
    //    //}
    //}
    [Fact]public void DynamicConvertImpliccit(){
        this.PrivateDynamicConvert<int,long>(1,RuntimeBinder.CSharpBinderFlags.None);
        this.PrivateDynamicConvert<int,double>(1,RuntimeBinder.CSharpBinderFlags.None);
        this.PrivateDynamicConvert<float,double>(1,RuntimeBinder.CSharpBinderFlags.None);
    }
    [Fact]public void DynamicConvertExplicit(){
        this.PrivateDynamicConvert<long,int>(1,RuntimeBinder.CSharpBinderFlags.ConvertExplicit);
        this.PrivateDynamicConvert<double,int>(1,RuntimeBinder.CSharpBinderFlags.ConvertExplicit);
        this.PrivateDynamicConvert<double,float>(1,RuntimeBinder.CSharpBinderFlags.ConvertExplicit);
    }
    private void PrivateDynamicConvert<TInput,TResult>(TInput input,RuntimeBinder.CSharpBinderFlags Flag){
        var Constant = Expressions.Expression.Constant(input);
        var binder=RuntimeBinder.Binder.Convert(
            Flag,
            typeof(TResult),
            this.GetType()
        );
        var Dynamic0=Expressions.Expression.Dynamic(
            binder,
            typeof(TResult),
            Constant
        );
        var CallSite=CallSite<Func<CallSite,object,TResult>>.Create(binder);
        var expected=CallSite.Target(CallSite,input!);
        this.ExpressionシリアライズAssertEqual(Dynamic0);
        var Lambda=Expressions.Expression.Lambda<Func<TResult>>(Dynamic0);
        this.Expression実行AssertEqual(Lambda);
        var M=Lambda.Compile();
        var actual=M();
        Assert.Equal(expected,actual);
    }
    [Fact]public void DynamicInvoke(){
        共通((int a,int b,int c)=>a==b&&a==c,1,2,3);
        共通((int a,int b,int c)=>a==b&&a==c,2,2,2);

        void 共通(object オブジェクト, object a,object b,object c){
            var binder=RuntimeBinder.Binder.Invoke(
                RuntimeBinder.CSharpBinderFlags.None,
                typeof(Serializer),
                CSharpArgumentInfoArray4
            );
            var CallSite=CallSite<Func<CallSite,object,object,object,object,object>>.Create(binder);
            var Dynamic0 = Expressions.Expression.Dynamic(
                binder,
                typeof(object),
                Expressions.Expression.Constant(オブジェクト),
                Expressions.Expression.Constant(a),
                Expressions.Expression.Constant(b),
                Expressions.Expression.Constant(c)
            );
            this.共通Dynamic(CallSite.Target(CallSite,オブジェクト,a,b,c),Dynamic0);
        }
    }
    [Fact]public void ClassDisplay(){
        this.Expression実行AssertEqual(
            Expressions.Expression.Lambda<Func<object>>(
                Expressions.Expression.Constant(ClassDisplay取得())
            )
        );
    }
    static dynamic テストStaticMethod(){
        return テスト.StaticMethodInt32();
    }
    static Expressions.Expression<Action<object, int>> DynamicCall()
    {
        //CallSiteBinderの構築
        var binder = RuntimeBinder.Binder.InvokeMember(
            RuntimeBinder.CSharpBinderFlags.ResultDiscarded,
            nameof(テスト.InstanceMethod), 
            null, 
            typeof(Serializer),
            new[] { 
                RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None, null),
                RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.UseCompileTimeType, null)
            }
        );

        //パラメータの構築
        var a1=new テスト();
        var a2=11;
        var p1 = Expressions.Expression.Parameter(typeof(object), "target");
        var p2 = Expressions.Expression.Parameter(typeof(int), "arg");

        //動的操作の生成
        var Dynamic=Expressions.Expression.MakeDynamic(typeof(Action<CallSite,object,int>),
            binder,
            p1,p2
        );
        var M = Expressions.Expression.Lambda<Action<object,int>>(Dynamic,p1,p2).Compile();
        M(a1,100);
        M(a1,100);
        var CallSite=CallSite<Action<CallSite,object,int>>.Create(binder);
        CallSite.Target(CallSite,a1,a2);
        return Expressions.Expression.Lambda<Action<object,int>>(Dynamic,p1,p2);
    }    
    [Fact]
    public void BinderTest(){
        var arg1=new テスト();
        var arg2=11;
        var Dynamic = DynamicCall().Compile();

        Dynamic(arg1, 100);
        Dynamic(arg1, 200);
        var CSharpArgumentInfos=new[]{
            RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None,null),
            RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.UseCompileTimeType, null)
        };
        var binder=RuntimeBinder.Binder.InvokeMember(
            RuntimeBinder.CSharpBinderFlags.ResultDiscarded,
            nameof(テスト.InstanceMethod),
            null,
            this.GetType(),
            CSharpArgumentInfos
        );
        var CallSite=CallSite<Action<CallSite,object,int>>.Create(binder);
        CallSite.Target(CallSite,arg1,arg2);
    }

    [Fact]public void DynamicInvokeMember(){
        var o=new テスト();
        Action3(o,nameof(テスト.Action),1,2.0,"string");
        Action1(o,nameof(テスト.InstanceMethod),1);
        引数名();
        Func3(o,nameof(テスト.Func),1,2.0,"string");

        void Action1<T0,T1>(T0 arg0,string Name,T1 arg1){
            var CSharpArgumentInfos=new[]{
                RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None,null),
                RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.UseCompileTimeType, null)
            };
            var binder=RuntimeBinder.Binder.InvokeMember(
                RuntimeBinder.CSharpBinderFlags.ResultDiscarded,Name,null,typeof(テスト),CSharpArgumentInfos
            );
            var Dynamic0=Expressions.Expression.Dynamic(
                binder,typeof(object),
                Expressions.Expression.Constant(arg0),Expressions.Expression.Constant(arg1)
            );
            Action1後処理<T0,T1>(Dynamic0,binder,arg0,arg1);
            Action1後処理<object,T1>(Dynamic0,binder,arg0,arg1);
        }
        void Action1後処理<T0,T1>(Expressions.DynamicExpression Dynamic,CallSiteBinder binder,T0 @this,T1 arg1){
            var CallSite=CallSite<Action<CallSite,T0,T1>>.Create(binder);
            CallSite.Target(CallSite,@this,arg1);
            this.ExpressionシリアライズAssertEqual(Dynamic);
            var Lambda = Expressions.Expression.Lambda<Action>(Dynamic);
            this.ExpressionシリアライズAssertEqual((Expressions.Expression)Lambda);
            var M = Lambda.Compile();
            M();
        }
        void Action3<T0,T1,T2,T3>(T0 arg0,string Name,T1 arg1,T2 arg2,T3 arg3){
            var CSharpArgumentInfos=new[]{
                CSharpArgumentInfo1,
                //CSharpArgumentInfo1,CSharpArgumentInfo1,CSharpArgumentInfo1
                RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.NamedArgument,"a"),
                RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.NamedArgument,"b"),
                RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.NamedArgument,"c"),
            };
            var binder=RuntimeBinder.Binder.InvokeMember(
                RuntimeBinder.CSharpBinderFlags.ResultDiscarded,Name,null,typeof(LinqDB.Serializers.MemoryPack.Formatters.Expression),
                CSharpArgumentInfos
            );
            var Dynamic0=Expressions.Expression.Dynamic(
                binder,
                typeof(object),
                Expressions.Expression.Constant(arg0),Expressions.Expression.Constant(arg1),Expressions.Expression.Constant(arg2),Expressions.Expression.Constant(arg3)
            );
            Action3後処理<T0,T1,T2,T3>(Dynamic0,binder,arg0,arg1,arg2,arg3);
            Action3後処理<object,T1,T2,T3>(Dynamic0,binder,arg0,arg1,arg2,arg3);
        }
        void Action3後処理<T0,T1,T2,T3>(Expressions.DynamicExpression Dynamic,CallSiteBinder binder,T0 arg0,T1 arg1,T2 arg2,T3 arg3){
            var CallSite=CallSite<Action<CallSite,T0,T1,T2,T3>>.Create(binder);
            CallSite.Target(CallSite,arg0,arg1,arg2,arg3);
            this.ExpressionシリアライズAssertEqual(Dynamic);
            var Lambda = Expressions.Expression.Lambda<Action>(Dynamic);
            this.ExpressionシリアライズAssertEqual((Expressions.Expression)Lambda);
            var M = Lambda.Compile();
            M();
        }
        void Func3<T0,T1,T2,T3>(T0 arg0,string Name,T1 arg1,T2 arg2,T3 arg3){
            var binder=RuntimeBinder.Binder.InvokeMember(
                RuntimeBinder.CSharpBinderFlags.None,Name,null,
                typeof(Serializer),CSharpArgumentInfoArray4
            );
            var Dynamic0 = Expressions.Expression.Dynamic(
                binder,
                typeof(object),
                Expressions.Expression.Constant(arg0),Expressions.Expression.Constant(arg1),Expressions.Expression.Constant(arg2),Expressions.Expression.Constant(arg3)
            );
            Func3後処理<T0,T1,T2,T3,object>(Dynamic0,binder,arg0,arg1,arg2,arg3);
            Func3後処理<object,T1,T2,T3,object>(Dynamic0,binder,arg0,arg1,arg2,arg3);
        }
        void Func3後処理<T0,T1,T2,T3,TResult>(Expressions.DynamicExpression Dynamic,CallSiteBinder binder,T0 arg0,T1 arg1,T2 arg2,T3 arg3){
            var CallSite=CallSite<Func<CallSite,T0,T1,T2,T3,TResult>>.Create(binder);
            var expected=CallSite.Target(CallSite,arg0,arg1,arg2,arg3)!;
            this.共通Dynamic(expected,Dynamic);
        }
        void 引数名(){
            var arg1=new テスト();
            var arg2=11;
            var arg3=22;
            var arg4="cc";
            var CSharpArgumentInfos=new[]{
                CSharpArgumentInfo1,
                RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.NamedArgument,"a"),
                RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.NamedArgument,"b"),
                RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.NamedArgument,"c"),
            };
            var binder=RuntimeBinder.Binder.InvokeMember(
                RuntimeBinder.CSharpBinderFlags.ResultIndexed,
                "Func",
                null,
                this.GetType(),
                CSharpArgumentInfos
            );
            var Dynamic0=Expressions.Expression.Dynamic(
                binder,
                typeof(object),
                Expressions.Expression.Constant(arg1),Expressions.Expression.Constant(arg2),Expressions.Expression.Constant(arg3),Expressions.Expression.Constant(arg4)
            );
            var CallSite=CallSite<Func<CallSite,object,object,object,object,object>>.Create(binder);
            var expected=CallSite.Target(CallSite,arg1,arg2,arg3,arg4);
            this.共通Dynamic(expected,Dynamic0);
        }
    }
    [Fact]public void DynamicGetMember(){
        //if(a_GetMemberBinder!=null) {
        {
            var arg1=new TestDynamic<int>(1,2);
            var binder=RuntimeBinder.Binder.GetMember(
                RuntimeBinder.CSharpBinderFlags.None,
                nameof(TestDynamic<int>.メンバー1),
                this.GetType(),
                CSharpArgumentInfoArray1
            );
            var Dynamic0=Expressions.Expression.Dynamic(
                binder,
                typeof(object),
                Expressions.Expression.Constant(arg1)
            );
            var CallSite=CallSite<Func<CallSite,object,object>>.Create(binder);
            this.共通Dynamic(CallSite.Target(CallSite,arg1),Dynamic0);
        }
        {
            var arg1=new TestDynamic<int>(1,2);
            var binder=RuntimeBinder.Binder.GetMember(
                RuntimeBinder.CSharpBinderFlags.ResultIndexed,
                nameof(TestDynamic<int>.メンバー1),
                this.GetType(),
                CSharpArgumentInfoArray1
            );
            var Dynamic0=Expressions.Expression.Dynamic(
                binder,
                typeof(object),
                Expressions.Expression.Constant(arg1)
            );
            this.ExpressionシリアライズAssertEqual(Dynamic0);
            var CallSite=CallSite<Func<CallSite,object,object>>.Create(binder);
            this.共通Dynamic(CallSite.Target(CallSite,arg1),Dynamic0);
        }
    }
    [Fact]public void DynamicSetMember(){
        var arg1=new TestDynamic<int>(1,2);
        var arg2=2;
        var binder=RuntimeBinder.Binder.SetMember(
            RuntimeBinder.CSharpBinderFlags.ResultIndexed,
            nameof(TestDynamic<int>.メンバー1),
            this.GetType(),
            CSharpArgumentInfoArray2
        );
        var Dynamic0=Expressions.Expression.Dynamic(
            binder,
            typeof(object),
            Expressions.Expression.Constant(arg1),
            Expressions.Expression.Constant(arg2)
        );
        var CallSite=CallSite<Func<CallSite,object,object,object>>.Create(binder);
        this.共通Dynamic(CallSite.Target(CallSite,arg1,arg2),Dynamic0);
    }
    [Fact]public void DynamicGetIndex0(){
        const int expected = 2;
        var arg1 = new[] {
            1,expected,3
        };
        var arg2=1;
        var binder=RuntimeBinder.Binder.GetIndex(
            RuntimeBinder.CSharpBinderFlags.None,
            this.GetType(),
            CSharpArgumentInfoArray2
        );
        var Dynamic0=Expressions.Expression.Dynamic(
            binder,
            typeof(object),
            Expressions.Expression.Constant(arg1),Expressions.Expression.Constant(arg2)
        );
        var CallSite=CallSite<Func<CallSite,object,object,object>>.Create(binder);
        this.共通Dynamic(CallSite.Target(CallSite,arg1,arg2),Dynamic0);
    }
    [Fact]public void DynamicGetIndex1(){
        const int expected = 10;
        var arg1 = new[,] {
            {1,2,3},
            {4,expected,6},
            { 7,8,9 },
        };
        var arg2=1;
        var arg3=1;
        var binder=RuntimeBinder.Binder.GetIndex(
            RuntimeBinder.CSharpBinderFlags.None,
            this.GetType(),
            CSharpArgumentInfoArray3
        );
        var Dynamic0=Expressions.Expression.Dynamic(
            binder,
            typeof(object),
            Expressions.Expression.Constant(arg1),Expressions.Expression.Constant(arg2),Expressions.Expression.Constant(arg3)
        );
        var CallSite=CallSite<Func<CallSite,object,object,object,object>>.Create(binder);
        this.共通Dynamic(CallSite.Target(CallSite,arg1,arg2,arg3),Dynamic0);
    }
    [Fact]public void DynamicGetIndex2(){
        var arg1=new テスト();
        var arg2=1;
        var binder=RuntimeBinder.Binder.GetIndex(
            RuntimeBinder.CSharpBinderFlags.None,
            this.GetType(),
            new[]{
                RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None, null),
                RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None, null),
            }
        );
        var Dynamic0=Expressions.Expression.Dynamic(
            binder,
            typeof(object),
            Expressions.Expression.Constant(arg1,typeof(object)),Expressions.Expression.Constant(arg2,typeof(object))
        );
        var CallSite=CallSite<Func<CallSite,object,object,object>>.Create(binder);
        this.共通Dynamic(CallSite.Target(CallSite,arg1,arg2),Dynamic0);
    }
    [Fact]public void DynamicSetIndex0(){
        var arg1= new[] {
            1,2,3
        };
        var arg2=1;
        var arg3=1;
        var binder=RuntimeBinder.Binder.SetIndex(
            RuntimeBinder.CSharpBinderFlags.None,
            this.GetType(),
            CSharpArgumentInfoArray3
        );
        var Dynamic0=Expressions.Expression.Dynamic(
            binder,
            typeof(object),
            Expressions.Expression.Constant(arg1),Expressions.Expression.Constant(arg2),Expressions.Expression.Constant(arg3)
        );
        var CallSite=CallSite<Func<CallSite,object,object,object,object>>.Create(binder);
        this.共通Dynamic(CallSite.Target(CallSite,arg1,arg2,arg3),Dynamic0);
    }
    [Fact]public void DynamicSetIndex1(){
        var arg1= new[,] {
            {1,2,3},
            {4,5,6},
            {7,8,9},
        };
        var arg2=1;
        var arg3=1;
        var arg4=1;
        var binder=RuntimeBinder.Binder.SetIndex(
            RuntimeBinder.CSharpBinderFlags.None,
            this.GetType(),
            CSharpArgumentInfoArray4
        );
        var Dynamic0=Expressions.Expression.Dynamic(
            binder,
            typeof(object),
            Expressions.Expression.Constant(arg1),Expressions.Expression.Constant(arg2),Expressions.Expression.Constant(arg3),Expressions.Expression.Constant(arg4)
        );
        var CallSite=CallSite<Func<CallSite,object,object,object,object,object>>.Create(binder);
        this.共通Dynamic(CallSite.Target(CallSite,arg1,arg2,arg3,arg4),Dynamic0);
    }
    [Fact]public void DynamicBinary(){
        var arg1=1;
        var arg2=1;
        var binder=RuntimeBinder.Binder.BinaryOperation(
            RuntimeBinder.CSharpBinderFlags.None,
            Expressions.ExpressionType.Add,
            this.GetType(),
            new[]{
                RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None,null),
                RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None,null)
            }
        );
        var Dynamic0 = Expressions.Expression.Dynamic(
            binder,typeof(object),
            Expressions.Expression.Constant(arg1),Expressions.Expression.Constant(arg2)
        );
        var CallSite=CallSite<Func<CallSite,object,object,object>>.Create(binder);
        this.共通Dynamic(CallSite.Target(CallSite,arg1,arg2),Dynamic0);
    }
    private void 共通Dynamic(object expected,Expressions.DynamicExpression Dynamic0){
        this.ExpressionシリアライズAssertEqual(Dynamic0);
        var Lambda=Expressions.Expression.Lambda<Func<object>>(Dynamic0);
        var M=Lambda.Compile();
        var actual=M();
        Assert.Equal(expected,actual);
        this.Expression実行AssertEqual(Lambda);
    }
    [Fact]public void ElementInit(){
        var Type=typeof(BindCollection);
        var Int32フィールド1=Type.GetField(nameof(BindCollection.Int32フィールド1));
        var Int32フィールド2=Type.GetField(nameof(BindCollection.Int32フィールド2));
        var BindCollectionフィールド1=Type.GetField(nameof(BindCollection.BindCollectionフィールド1));
        var BindCollectionフィールド2=Type.GetField(nameof(BindCollection.BindCollectionフィールド2));
        var Listフィールド1=Type.GetField(nameof(BindCollection.Listフィールド1));
        var Listフィールド2=Type.GetField(nameof(BindCollection.Listフィールド2));
        var Constant_1=Expressions.Expression.Constant(1);
        var Constant_2=Expressions.Expression.Constant(2);
        var ctor=Type.GetConstructor(new[]{typeof(int)});
        var New=Expressions.Expression.New(
            ctor,
            Constant_1
        );
        var Add=typeof(List<int>).GetMethod("Add");
        Expressions.Expression.ElementInit(
            Add,
            Constant_1
        );
        {
            var l=new List<int>();
            this.ExpressionシリアライズAssertEqual(
                Expressions.Expression.MemberInit(
                    New,
                    Expressions.Expression.ListBind(
                        Listフィールド2,
                        Expressions.Expression.ElementInit(
                            Add,
                            Constant_1
                        )
                    )
                )
            );
        }
    }
    [Fact]public void Label(){
        var labelTarget=Expressions.Expression.Label();
        this.ExpressionシリアライズAssertEqual(Expressions.Expression.Label(labelTarget));
        this.ExpressionシリアライズAssertEqual(Expressions.Expression.Label(labelTarget,Expressions.Expression.Constant(1)));
    }
    [Fact]public void LabelTarget(){
        this.ObjectシリアライズAssertEqual(Expressions.Expression.Label());
        this.ObjectシリアライズAssertEqual(Expressions.Expression.Label(typeof(int)));
        this.ObjectシリアライズAssertEqual(Expressions.Expression.Label(typeof(double),"abc"));
    }
    static Expressions.LambdaExpression Lambda<T>(Expressions.Expression<Func<T>> e)=>e;
    [Fact]
    public void Lambda0(){
        this.ExpressionシリアライズAssertEqual(Lambda(()=>1));
    }
    [Fact]public void Lambda1(){
        this.Expression実行AssertEqual(Expressions.Expression.Lambda<Func<decimal>>(Expressions.Expression.Constant(2m)));
    }
    [Fact]public void Lambda3(){
        //const decimal Catch値 = 40, Finally値 = 30;
        //Expressions.Expressions.Expression.TryCatchFinally(
        //    //Expressions.Expressions.Expression.PostIncrementAssign(@decimal),
        //    Expressions.Expressions.Expression.Constant(Finally値),
        //    Expressions.Expressions.Expression.Constant(Finally値),
        //    Expressions.Expressions.Expression.Catch(
        //        typeof(Exception),
        //        Expressions.Expressions.Expression.Constant(Catch値)
        //    )
        //);
        this.Expression実行AssertEqual(
            Expressions.Expression.Lambda<Func<decimal>>(
                Expressions.Expression.Block(
                    new[] { ParameterDecimal },
                    Expressions.Expression.Assign(ParameterDecimal,Expressions.Expression.Constant(0m)),
                    Expressions.Expression.TryCatchFinally(
                        Expressions.Expression.PostIncrementAssign(ParameterDecimal),
                        ParameterDecimal,
                        Expressions.Expression.Catch(
                            typeof(Exception),
                            ParameterDecimal
                        )
                    )
                )
            )
        );
    }
    [Fact]public void Lambda31(){
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Lambda<Func<decimal,decimal>>(
                Expressions.Expression.Block(
                    Expressions.Expression.Assign(ParameterDecimal,Expressions.Expression.Constant(0m)),
                    Expressions.Expression.TryCatchFinally(
                        ParameterDecimal,
                        ParameterDecimal
                    )
                ),
                ParameterDecimal
            )
        );
    }
    [Fact]public void Lambda32(){
        this.Expression実行AssertEqual(
            Expressions.Expression.Lambda<Func<decimal>>(
                Expressions.Expression.Block(
                    new[] { ParameterDecimal },
                    Expressions.Expression.Assign(ParameterDecimal,Expressions.Expression.Constant(0m)),
                    Expressions.Expression.TryCatchFinally(
                        ParameterDecimal,
                        ParameterDecimal
                    ),
                    ParameterDecimal
                )
            )
        );
    }
    [Fact]public void Lambda33(){
        this.Expression実行AssertEqual(
            Expressions.Expression.Lambda<Func<decimal>>(
                Expressions.Expression.Block(
                    new[]{ParameterDecimal},
                    Expressions.Expression.Assign(ParameterDecimal,Expressions.Expression.Constant(0m)),
                    Expressions.Expression.TryCatchFinally(
                        Expressions.Expression.PostIncrementAssign(ParameterDecimal),
                        Expressions.Expression.Assign(
                            ParameterDecimal,
                            Expressions.Expression.Constant(2m)
                        )
                    ),
                    ParameterDecimal
                )
            )
        );
    }
    [Fact]public void Lambda34(){
        this.Expression実行AssertEqual(
            Expressions.Expression.Lambda<Func<decimal>>(
                Expressions.Expression.Block(
                    new[] { ParameterDecimal },
                    Expressions.Expression.Assign(ParameterDecimal,Expressions.Expression.Constant(0m)),
                    Expressions.Expression.TryCatchFinally(
                        Expressions.Expression.PostIncrementAssign(ParameterDecimal),
                        Expressions.Expression.Assign(
                            ParameterDecimal,
                            Expressions.Expression.Constant(2m)
                        )
                    ),
                    ParameterDecimal
                )
            )
        );
    }
    [Fact]public void Lambda35(){
        this.Expression実行AssertEqual(
            Expressions.Expression.Lambda<Func<decimal>>(
                Expressions.Expression.Block(
                    new[] { ParameterDecimal },
                    Expressions.Expression.Assign(ParameterDecimal,Expressions.Expression.Constant(0m)),
                    Expressions.Expression.TryCatchFinally(
                        Expressions.Expression.PostIncrementAssign(ParameterDecimal),
                        Expressions.Expression.Assign(
                            ParameterDecimal,
                            Expressions.Expression.Constant(2m)
                        )
                    ),
                    ParameterDecimal
                )
            )
        );
    }
    [Fact]public void Lambda4(){
        var Exception=Expressions.Expression.Parameter(typeof(Exception));
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Lambda<Func<decimal,decimal>>(
                Expressions.Expression.TryCatchFinally(
                    ParameterDecimal,
                    ParameterDecimal,
                    Expressions.Expression.Catch(Exception,ParameterDecimal,Expressions.Expression.Constant(true))
                ),
                ParameterDecimal
            )
        );
    }
    [Fact]public void Lambda5(){
        var Array2=Expressions.Expression.Parameter(typeof(int[,]));
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Lambda(
                Expressions.Expression.ArrayIndex(
                    Array2,
                    Expressions.Expression.Constant(0),
                    Expressions.Expression.Constant(0)
                ),
                Array2
            )
        );
        var Array1=Expressions.Expression.Parameter(typeof(int[]));
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Lambda(
                Expressions.Expression.ArrayIndex(
                    Array1,
                    Expressions.Expression.Constant(0)
                ),
                Array1
            )
        );
    }
    [Fact]
    public void NewArrayInit(){
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.NewArrayInit(
                typeof(int),
                Expressions.Expression.Constant(0),
                Expressions.Expression.Constant(1)
            )
        );
    }
    [Fact]
    public void NewArrayBounds(){
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.NewArrayBounds(
                typeof(int),
                Expressions.Expression.Constant(0),
                Expressions.Expression.Constant(1)
            )
        );
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
        var Constant_1 = Expressions.Expression.Constant(1);
        var ctor = Type.GetConstructor(new[] {
            typeof(int)
        });
        var New = Expressions.Expression.New(
            ctor,
            Constant_1
        );
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.MemberInit(
                New,
                Expressions.Expression.Bind(
                    Int32フィールド1,
                    Constant_1
                )
            )
        );
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.MemberInit(
                New,
                Expressions.Expression.ListBind(
                    Listフィールド2,
                    Expressions.Expression.ElementInit(
                        typeof(List<int>).GetMethod("Add")!,
                        Constant_1
                    )
                )
            )
        );
    }
    private static readonly Expressions.LabelTarget Label_decimal=Expressions.Expression.Label(typeof(decimal),"Label_decimal");
    private static readonly Expressions.LabelTarget Label_void=Expressions.Expression.Label("Label");
    [Fact]
    public void Loop0(){
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Loop(
                Expressions.Expression.Block(
                    Expressions.Expression.Break(Label_decimal,Expressions.Expression.Constant(1m)),
                    Expressions.Expression.Continue(Label_void)
                ),
                Label_decimal,
                Label_void
            )
        );
    }
    [Fact]public void Loop1(){
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Loop(
                Expressions.Expression.Block(
                    Expressions.Expression.Break(Label_decimal,Expressions.Expression.Constant(1m))
                ),
                Label_decimal
            )
        );
    }
    [Fact]
    public void Negate(){
        this.ExpressionシリアライズAssertEqual(Expressions.Expression.Negate(Expressions.Expression.Constant(1m)));
    }
    [Fact]public void Index0(){
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.MakeIndex(
                Expressions.Expression.New(typeof(List<int>).GetConstructor(Type.EmptyTypes)!),
                typeof(List<int>).GetProperty("Item"),
                new []{Expressions.Expression.Constant(0)}
            )
        );
    }
    [Fact]public void Index1(){
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.ArrayIndex(
                Expressions.Expression.NewArrayBounds(typeof(int),Expressions.Expression.Constant(2)),
                Expressions.Expression.Constant(0)
            )
        );
    }
    [Fact]
    public void Index2(){
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.ArrayIndex(
                Expressions.Expression.NewArrayBounds(typeof(int),Expressions.Expression.Constant(2),Expressions.Expression.Constant(3)),
                Expressions.Expression.Constant(0),
                Expressions.Expression.Constant(0)
            )
        );
    }
    [Fact]
    public void Index3(){
        var Array2=Expressions.Expression.Parameter(typeof(int[,,]));
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Lambda(
                Expressions.Expression.ArrayIndex(
                    Array2,
                    Expressions.Expression.Constant(0),
                    Expressions.Expression.Constant(0),
                    Expressions.Expression.Constant(0)
                ),
                Array2
            )
        );
    }
    [Fact]
    public void Goto(){
        var target=Expressions.Expression.Label(typeof(int),"target");
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Block(
                Expressions.Expression.Label(
                    target,
                    Expressions.Expression.Constant(1)
                ),
                Expressions.Expression.MakeGoto(
                    Expressions.GotoExpressionKind.Return,
                    target,
                    Expressions.Expression.Constant(5),
                    typeof(byte)
                )
            )
        );
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Block(
                Expressions.Expression.Label(
                    target,
                    Expressions.Expression.Constant(1)
                ),
                Expressions.Expression.MakeGoto(
                    Expressions.GotoExpressionKind.Goto,
                    target,
                    Expressions.Expression.Constant(2),
                    typeof(double)
                ),
                Expressions.Expression.MakeGoto(
                    Expressions.GotoExpressionKind.Break,
                    target,
                    Expressions.Expression.Constant(3),
                    typeof(decimal)
                ),
                Expressions.Expression.MakeGoto(
                    Expressions.GotoExpressionKind.Continue,
                    target,
                    Expressions.Expression.Constant(4),
                    typeof(float)
                ),
                Expressions.Expression.MakeGoto(
                    Expressions.GotoExpressionKind.Return,
                    target,
                    Expressions.Expression.Constant(5),
                    typeof(byte)
                )
            )
        );
    }
    [Fact]
    public void ListInit(){
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.ListInit(
                Expressions.Expression.New(typeof(List<int>)),
                Expressions.Expression.ElementInit(typeof(List<int>).GetMethod("Add")!,Expressions.Expression.Constant(1))
            )
        );
    }
    [Fact]public void MemberExpression(){
        var Point=Expressions.Expression.Parameter(typeof(Point));
        this.ExpressionシリアライズAssertEqual(Expressions.Expression.Block(new[]{Point},Expressions.Expression.MakeMemberAccess(Point,typeof(Point).GetProperty("X")!)));
    }
    [Fact]public void MemberBinding(){
        var Type = typeof(BindCollection);
        var Int32フィールド1 = Type.GetField(nameof(BindCollection.Int32フィールド1));
        var Int32フィールド2 = Type.GetField(nameof(BindCollection.Int32フィールド2));
        var BindCollectionフィールド1 = Type.GetField(nameof(BindCollection.BindCollectionフィールド1));
        var BindCollectionフィールド2 = Type.GetField(nameof(BindCollection.BindCollectionフィールド2));
        var Listフィールド1 = Type.GetField(nameof(BindCollection.Listフィールド1));
        var Listフィールド2 = Type.GetField(nameof(BindCollection.Listフィールド2));
        var Constant_1 = Expressions.Expression.Constant(1);
        var Constant_2 = Expressions.Expression.Constant(2);
        var ctor = Type.GetConstructor(new[] {
            typeof(int)
        });
        var New = Expressions.Expression.New(
            ctor,
            Constant_1
        );
        //if(a_Bindings.Count!=b_Bindings.Count) return false;
        this.ObjectシリアライズAssertEqual<Expressions.MemberBinding>(
            Expressions.Expression.Bind(
                Int32フィールド1,
                Constant_1
            )
        );
    }
    private static void StaticMethod(){}
    private static void StaticMethod(int a){}
    private static void StaticMethod(int a,int b){}
    private void InstanceMethod(){}
    private void InstanceMethod(int a){}
    private void InstanceMethod(int a,int b){}
    [Fact]public void MethodCall(){
        var o=new テスト();
        var arg=Expressions.Expression.Constant(1);
        var @this=Expressions.Expression.Constant(o);
        this.ExpressionシリアライズAssertEqual(Expressions.Expression.Call(M(()=>テスト.StaticMethod())));
        this.ExpressionシリアライズAssertEqual(Expressions.Expression.Call(M(()=>テスト.StaticMethod(1)),arg));
        this.ExpressionシリアライズAssertEqual(Expressions.Expression.Call(M(()=>テスト.StaticMethod(1,2)),arg,arg));
        this.ExpressionシリアライズAssertEqual(Expressions.Expression.Call(@this,M(()=>o.InstanceMethod())));
        this.ExpressionシリアライズAssertEqual(Expressions.Expression.Call(@this,M(()=>o.InstanceMethod(1)),arg));
        this.ExpressionシリアライズAssertEqual(Expressions.Expression.Call(@this,M(()=>o.InstanceMethod(1,2)),arg,arg));
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Call(
                M(()=>string.Concat("","")),
                Expressions.Expression.Constant("A"),
                Expressions.Expression.Constant("B")
            )
        );
    }
    //[Fact]
    //public void Null(){
    //    this.共通Expressions.Expression<Expressions.Expressions.Expression?>(null);
    //}
    //[Fact]
    //public void GreaterThan(){
    //    this.共通Expressions.Expression(Expressions.Expressions.Expression.GreaterThan(Expressions.Expressions.Expression.Constant(1m),Expressions.Expressions.Expression.Constant(1m)));
    //}

    //[Fact]
    //public void Assign(){
    //    var @string=Expressions.Expressions.Expression.Parameter(typeof(string));
    //    this.共通Expressions.Expression(Expressions.Expressions.Expression.Block(new[]{@string},Expressions.Expressions.Expression.Assign(@string,@string)));
    //}
    [Fact]
    public void Invoke(){
        var @string=Expressions.Expression.Parameter(typeof(string));
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Invoke(
                Expressions.Expression.Lambda(@string,@string),
                Expressions.Expression.Constant("B")
            )
        );
    }
    [Fact]public void New(){
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.New(
                typeof(ValueTuple<int>).GetConstructors()[0],
                Expressions.Expression.Constant(1)
            )
        );
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.New(
                typeof(ValueTuple<int,int>).GetConstructors()[0],
                Expressions.Expression.Constant(1),
                Expressions.Expression.Constant(2)
            )
        );
    }
    static Reflection.MethodInfo M<T>(Expressions.Expression<Func<T>> e){
        var Method=((Expressions.MethodCallExpression)e.Body).Method;
        return Method;
    }
    [Fact]
    public void Parameter(){
        var p0=Expressions.Expression.Parameter(typeof(int));
        this.Expressionシリアライズ(p0);
    }
    [Fact]
    public void Switch(){
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.Switch(
                Expressions.Expression.Constant(123),
                Expressions.Expression.Constant(0m),
                Expressions.Expression.SwitchCase(
                    Expressions.Expression.Constant(64m),
                    Expressions.Expression.Constant(124)
                )
            )
        );
    }
    [Fact]
    public void Try(){
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.TryCatch(
                Expressions.Expression.Constant(0),
                Expressions.Expression.Catch(
                    typeof(Exception),
                    Expressions.Expression.Constant(0)
                )
            )
        );
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.TryCatchFinally(
                Expressions.Expression.Default(typeof(void)),
                Expressions.Expression.Default(typeof(void))
            )
        );
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.TryFault(
                Expressions.Expression.Default(typeof(void)),
                Expressions.Expression.Default(typeof(void))
            )
        );
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.TryFinally(
                Expressions.Expression.Default(typeof(void)),
                Expressions.Expression.Default(typeof(void))
            )
        );
    }
    [Fact]
    public void TypeEqual(){
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.TypeEqual(
                Expressions.Expression.Constant(1m),
                typeof(decimal)
            )
        );
    }
    [Fact]public void TypeIs(){
        this.ExpressionシリアライズAssertEqual(
            Expressions.Expression.TypeIs(
                Expressions.Expression.Constant(1m),
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
        var ConstantArray = Expressions.Expression.Constant(new int[10]);
        var Constant1= Expressions.Expression.Constant(1);
        var Constant1_1d= Expressions.Expression.Constant(1.1);
        var ConstantTrue= Expressions.Expression.Constant(true);
        var Constant演算子=Expressions.Expression.Constant(new 演算子(true));
        var Constant演算子1=Expressions.Expression.Constant(new 演算子1(true));
        var Parameter演算子=Expressions.Expression.Parameter(typeof(演算子));
        var ParameterInt32=Expressions.Expression.Parameter(typeof(int));
        this.ExpressionシリアライズAssertEqual(Expressions.Expression.ArrayLength(Expressions.Expression.Constant(new int[1])));
        共通1(Expressions.Expression.ArrayLength(ConstantArray));
        共通1(Expressions.Expression.Quote(Expressions.Expression.Lambda(ConstantArray)));
        共通1(Expressions.Expression.Convert(Constant1_1d,typeof(int)));
        共通1(Expressions.Expression.ConvertChecked(Constant1_1d,typeof(int)));
        共通1(Expressions.Expression.Decrement(Constant1_1d));
        共通1(Expressions.Expression.Increment(Constant1_1d));
        共通1(Expressions.Expression.IsFalse(ConstantTrue));
        共通1(Expressions.Expression.IsTrue(ConstantTrue));
        共通1(Expressions.Expression.Negate(Constant1_1d));
        共通1(Expressions.Expression.NegateChecked(Constant1_1d));
        共通1(Expressions.Expression.Not(Constant1));
        共通1(Expressions.Expression.OnesComplement(Constant1));
        共通1(Expressions.Expression.Decrement(Constant1_1d));
        共通1(Expressions.Expression.Increment(Constant1_1d));
        共通0(ParameterInt32,Constant1,Expressions.Expression.PostDecrementAssign(ParameterInt32));
        共通0(ParameterInt32,Constant1,Expressions.Expression.PostIncrementAssign(ParameterInt32));
        共通0(ParameterInt32,Constant1,Expressions.Expression.PreDecrementAssign(ParameterInt32));
        共通0(ParameterInt32,Constant1,Expressions.Expression.PreIncrementAssign(ParameterInt32));
        共通1(Expressions.Expression.UnaryPlus(Constant1_1d));

        共通1(Expressions.Expression.Convert(Constant演算子,typeof(演算子1)));
        共通1(Expressions.Expression.ConvertChecked(Constant演算子,typeof(演算子1)));
        共通1(Expressions.Expression.Convert(Constant演算子1,typeof(演算子)));
        共通1(Expressions.Expression.ConvertChecked(Constant演算子1,typeof(演算子)));
        共通1(Expressions.Expression.Decrement(Constant演算子));
        共通1(Expressions.Expression.Increment(Constant演算子));
        共通1(Expressions.Expression.IsFalse(Constant演算子));
        共通1(Expressions.Expression.IsTrue(Constant演算子));
        共通1(Expressions.Expression.Negate(Constant演算子));
        共通1(Expressions.Expression.NegateChecked(Constant演算子));
        共通1(Expressions.Expression.OnesComplement(Constant演算子,GetMethod(nameof(Unary演算子))));
        共通1(Expressions.Expression.Decrement(Constant演算子));
        共通1(Expressions.Expression.Increment(Constant演算子));
        共通0(Parameter演算子,Constant演算子,Expressions.Expression.PostDecrementAssign(Parameter演算子));
        共通0(Parameter演算子,Constant演算子,Expressions.Expression.PostIncrementAssign(Parameter演算子));
        共通0(Parameter演算子,Constant演算子,Expressions.Expression.PreDecrementAssign(Parameter演算子));
        共通0(Parameter演算子,Constant演算子,Expressions.Expression.PreIncrementAssign(Parameter演算子));
        共通1(Expressions.Expression.UnaryPlus(Constant演算子));


        共通1(Expressions.Expression.Convert(Constant1_1d,typeof(int),GetMethod(()=>UnaryDouble(0))));
        共通1(Expressions.Expression.ConvertChecked(Constant1_1d,typeof(int),GetMethod(()=>UnaryDouble(0))));
        共通1(Expressions.Expression.Decrement(Constant1_1d,GetMethod(()=>UnaryDouble(0))));
        共通1(Expressions.Expression.Increment(Constant1_1d,GetMethod(()=>UnaryDouble(0))));
        共通1(Expressions.Expression.IsFalse(Constant演算子,GetMethod(nameof(IsTrue演算子))));
        共通1(Expressions.Expression.IsTrue(Constant演算子,GetMethod(nameof(IsTrue演算子))));
        共通1(Expressions.Expression.Negate(Constant1_1d,GetMethod(()=>UnaryDouble(0))));
        共通1(Expressions.Expression.NegateChecked(Constant1_1d,GetMethod(()=>UnaryDouble(0))));
        共通1(Expressions.Expression.OnesComplement(Constant演算子,GetMethod(nameof(Unary演算子))));
        共通1(Expressions.Expression.Decrement(Constant演算子,GetMethod(nameof(Unary演算子))));
        共通1(Expressions.Expression.Increment(Constant演算子,GetMethod(nameof(Unary演算子))));
        共通1(Expressions.Expression.UnaryPlus(Constant演算子,GetMethod(nameof(Unary演算子))));
        void 共通0(Expressions.ParameterExpression 代入先,Expressions.ConstantExpression 代入元,Expressions.UnaryExpression a){
            this.Expression実行AssertEqual(
                Expressions.Expression.Lambda<Func<object>>(
                    Expressions.Expression.Block(
                        new[]{代入先},
                        Expressions.Expression.Assign(
                            代入先,
                            代入元
                        ),
                        Expressions.Expression.Convert(
                            a,
                            typeof(object)
                        )
                    )
                )
            );
        }
        void 共通1(Expressions.UnaryExpression Unary){
            this.ExpressionシリアライズAssertEqual(Unary);
            this.Expression実行AssertEqual(
                Expressions.Expression.Lambda<Func<object>>(
                    Expressions.Expression.Convert(
                        Unary,
                        typeof(object)
                    )
                )
            );
        }
    }
}
