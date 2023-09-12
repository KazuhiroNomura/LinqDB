using LinqDB.Optimizers;
using LinqDB.Remote.Clients;
using LinqDB.Remote.Servers;
using LinqDB.Sets;
using LinqDB;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using System.Linq;
//using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LinqDB.Serializers;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using LinqDB.Serializers.MemoryPack.Formatters;
using MemoryPack;
using System.Buffers;
using System.Collections.ObjectModel;
using System.Drawing;
using Reflection=System.Reflection;
using System.Runtime.CompilerServices;
using LinqDB.Helpers;
using Expressions=System.Linq.Expressions;
using Microsoft.CSharp.RuntimeBinder;
//using Binder = Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using static LinqDB.Optimizers.Optimizer;
using MessagePack;
using Utf8Json;
using static Microsoft.FSharp.Core.ByRefKinds;

namespace Serializers.MessagePack.Formatters;
[Serializable,MemoryPackable,MessagePackObject(true)]
public partial class テスト:IEquatable<テスト>{
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
public class Expression:共通 {
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
    private static readonly Expressions.ParameterExpression int8    = Expressions.Expression.Parameter(typeof(sbyte),"int8");
    private static readonly Expressions.ParameterExpression int16   = Expressions.Expression.Parameter(typeof(short),"int16");
    private static readonly Expressions.ParameterExpression int32   = Expressions.Expression.Parameter(typeof(int),"int32");
    private static readonly Expressions.ParameterExpression int64   = Expressions.Expression.Parameter(typeof(long),"int64");
    private static readonly Expressions.ParameterExpression uint8   = Expressions.Expression.Parameter(typeof(byte),"uint8");
    private static readonly Expressions.ParameterExpression uint16  = Expressions.Expression.Parameter(typeof(ushort),"uint16");
    private static readonly Expressions.ParameterExpression uint32  = Expressions.Expression.Parameter(typeof(uint),"uint32");
    private static readonly Expressions.ParameterExpression uint64  = Expressions.Expression.Parameter(typeof(ulong),"uint64");
    private static readonly Expressions.ParameterExpression @float  = Expressions.Expression.Parameter(typeof(float),"float");
    private static readonly Expressions.ParameterExpression @double = Expressions.Expression.Parameter(typeof(double),"double");
    private static readonly Expressions.ParameterExpression @decimal= Expressions.Expression.Parameter(typeof(decimal),"decimal");
    private static readonly Expressions.ParameterExpression @string = Expressions.Expression.Parameter(typeof(string),"string");
    //private static readonly ParameterExpression[] Parameters={int8,int16,int32,int64,uint8,uint16,uint32,uint64,@float,@double,@decimal,@string};
    //private static readonly ParameterExpression @int = Expressions.Expression.Parameter(typeof(decimal),"int");
    //private static readonly ParameterExpression @int = Expressions.Expression.Parameter(typeof(decimal),"int");
    //private static readonly ParameterExpression @decimal = Expressions.Expression.Parameter(typeof(decimal),"decimal");
    [Fact]public void Binary(){
        var Parameters=new[]{int16,int32,int64,uint16,uint32,uint64,@float,@double,@decimal};
        foreach(var p in Parameters){
            this.共通Expression(
                Expressions.Expression.Block(
                    new[]{p},
                    Expressions.Expression.Add(p,p)
                )
            );
        }
    }
    [Fact]public void Block0(){
        this.共通Expression(
            Expressions.Expression.Block(
                new[] { @decimal },
                @decimal
            )
        );
    }
    [Fact]public void Block1(){
        var q= Expressions.Expression.Parameter(typeof(decimal),"q");
        this.共通Expression(
            Expressions.Expression.Block(
                new[] { @decimal,q },
                @decimal
            )
        );
    }
    [Fact]public void Block2(){
        this.共通Expression(
            Expressions.Expression.Block(
                new[]{Expressions.Expression.Parameter(typeof(decimal),"a"),Expressions.Expression.Parameter(typeof(decimal),"b"),@decimal},
                @decimal
            )
        );
    }
    [Fact]public void Block4(){
        this.共通Expression(
            Expressions.Expression.Block(
                new[] { @decimal },
                Expressions.Expression.TryCatchFinally(
                    Expressions.Expression.Increment(@decimal),
                    @decimal
                )
            )
        );
        this.共通Expression(
            Expressions.Expression.Block(
                new[] { @decimal },
                Expressions.Expression.TryCatchFinally(
                    Expressions.Expression.Increment(@decimal),
                    Expressions.Expression.Constant(2)
                )
            )
        );
        this.共通Expression(
            Expressions.Expression.Block(
                new[] { @decimal },
                Expressions.Expression.TryCatchFinally(
                    Expressions.Expression.PostIncrementAssign(@decimal),
                    Expressions.Expression.Constant(2)
                )
            )
        );
        this.共通Expression(
            Expressions.Expression.Block(
                new[] { @decimal },
                Expressions.Expression.TryCatchFinally(
                    @decimal,
                    @decimal
                ),
                @decimal
            )
        );
        this.共通Expression(
            Expressions.Expression.Block(
                new[] { @decimal },
                Expressions.Expression.TryCatchFinally(
                    Expressions.Expression.Constant(2),
                    @decimal
                )
            )
        );
    }
    [Fact]public void Block10(){
        this.共通Expression(
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
        this.共通Expression(
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
        this.共通Expression(
            Expressions.Expression.Condition(
                Expressions.Expression.Constant(true),
                Expressions.Expression.Constant(1m),
                Expressions.Expression.Constant(2m)
            )
        );
        this.共通Expression(
            Expressions.Expression.IfThenElse(
                Expressions.Expression.Constant(true),
                Expressions.Expression.Constant(1m),
                Expressions.Expression.Constant(2m)
            )
        );
        this.共通Expression(
            Expressions.Expression.IfThen(
                Expressions.Expression.Constant(true),
                Expressions.Expression.Constant(1m)
            )
        );
    }
    [Fact]public void Constant0(){
        this.共通Expression((Expressions.Expression)Expressions.Expression.Constant(null,typeof(string)));
    }
    [Fact]public void Constant1(){
        this.共通Expression(Expressions.Expression.Constant(1111m));
        this.共通Expression(Expressions.Expression.Constant(1111m,typeof(object)));
    }
    [Fact]public void Constructor(){
        this.共通Expression(
            Expressions.Expression.New(
                typeof(string).GetConstructor(new System.Type[]{typeof(char),typeof(int)})!,
                Expressions.Expression.Constant('a'),
                Expressions.Expression.Constant(2)
            )
        );
    }
    [Fact]public void Default(){
        this.共通Expression(Expressions.Expression.Default(typeof(int)));
        this.共通Expression(Expressions.Expression.Default(typeof(decimal)));
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
            Expressions.ExpressionType.Increment,
            this.GetType(),
            CSharpArgumentInfoArray1
        );
        var Dynamic0=Expressions.Expression.Dynamic(
            binder,
            typeof(object),
            Expressions.Expression.Constant(arg1,typeof(object))
        );
        var CallSite=CallSite<Func<CallSite,object,object>>.Create(binder);
        this.共通Dynamic(CallSite.Target(CallSite,arg1),Dynamic0);
    }
    [Fact]public void DynamicCreateInstance(){
        var binder=Binder.InvokeConstructor(
            CSharpBinderFlags.None,
            typeof(Expression),
            new CSharpArgumentInfo[]{
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
            }
        );
        var  CallSite = CallSite<Func<CallSite,System.Type,object>>.Create(binder);
        {
            var Dynamic0 = Expressions.Expression.Dynamic(
                binder,
                typeof(object),
                Expressions.Expression.Constant(typeof(テスト))
            );
            this.共通Dynamic(CallSite.Target(CallSite,typeof(テスト)),Dynamic0);
        }
        //{
        //    var Constant_1L = Expressions.Expression.Constant(1L);
        //    var binder=Binder.Convert(
        //        CSharpBinderFlags.ConvertExplicit,
        //        typeof(double),
        //        this.GetType()
        //    );
        //    var Dynamic0=Expressions.Expression.Dynamic(
        //        binder,
        //        typeof(object),
        //        Constant_1L
        //    );
        //    var CallSite=CallSite<Func<CallSite,object,object>>.Create(binder);
        //    this.共通Dynamic(CallSite.Target(CallSite,Constant_1L),Dynamic0);
        //}
    }
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
        var Constant = Expressions.Expression.Constant(input);
        var binder=Binder.Convert(
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
        this.共通Expression(Dynamic0);
        var Lambda=Expressions.Expression.Lambda<Func<TResult>>(Dynamic0);
        this.共通Expression(Lambda);
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
                typeof(Expression),
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
                typeof(Expression),
                CSharpArgumentInfoArray4
            );
            var CallSite=CallSite<Action<CallSite,object,object,object,object>>.Create(binder);
            CallSite.Target(CallSite,o,@int,@double,@string);
            var Dynamic0 = Expressions.Expression.Dynamic(
                binder,
                typeof(object),
                Expressions.Expression.Constant(o),
                Expressions.Expression.Constant(@int),
                Expressions.Expression.Constant(@double),
                Expressions.Expression.Constant(@string)
            );
            this.共通Expression(Dynamic0);
            var Lambda=Expressions.Expression.Lambda<Action>(Dynamic0);
            this.共通Expression(Lambda);
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
                typeof(Expression),
                CSharpArgumentInfoArray4
            );
            var CallSite=CallSite<Func<CallSite,object,object,object,object,object>>.Create(binder);
            var Dynamic0 = Expressions.Expression.Dynamic(
                binder,
                typeof(object),
                Expressions.Expression.Constant(o),
                Expressions.Expression.Constant(@int),
                Expressions.Expression.Constant(@double),
                Expressions.Expression.Constant(@string)
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
            var Dynamic0=Expressions.Expression.Dynamic(
                binder,
                typeof(object),
                Expressions.Expression.Constant(arg1),
                Expressions.Expression.Constant(arg2),
                Expressions.Expression.Constant(arg3),
                Expressions.Expression.Constant(arg4)
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
            var Dynamic0=Expressions.Expression.Dynamic(
                binder,
                typeof(object),
                Expressions.Expression.Constant(arg1)
            );
            this.共通Expression(Dynamic0);
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
            var Dynamic0=Expressions.Expression.Dynamic(
                binder,
                typeof(object),
                Expressions.Expression.Constant(arg1)
            );
            this.共通Expression(Dynamic0);
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
        var Dynamic0=Expressions.Expression.Dynamic(
            binder,
            typeof(object),
            Expressions.Expression.Constant(arg1),
            Expressions.Expression.Constant(arg2)
        );
        this.共通Expression(Dynamic0);
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
        var Dynamic0=Expressions.Expression.Dynamic(
            binder,
            typeof(object),
            Expressions.Expression.Constant(arg1),
            Expressions.Expression.Constant(arg2)
        );
        this.共通Expression(Dynamic0);
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
        var Dynamic0=Expressions.Expression.Dynamic(
            binder,
            typeof(object),
            Expressions.Expression.Constant(arg1),
            Expressions.Expression.Constant(arg2),
            Expressions.Expression.Constant(arg3)
        );
        this.共通Expression(Dynamic0);
        var CallSite=CallSite<Func<CallSite,object,object,object,object>>.Create(binder);
        this.共通Dynamic(CallSite.Target(CallSite,arg1,arg2,arg3),Dynamic0);
    }
    [Fact]public void DynamicBinary(){
        var arg1=1;
        var arg2=1;
        var binder=Binder.BinaryOperation(
            CSharpBinderFlags.None,
            Expressions.ExpressionType.Add,
            this.GetType(),
            new[]{
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null)
            }
        );
        var Dynamic0 = Expressions.Expression.Dynamic(
            binder,
            typeof(object),
            Expressions.Expression.Constant(arg1),
            Expressions.Expression.Constant(arg2)
        );
        var CallSite=CallSite<Func<CallSite,object,object,object>>.Create(binder);
        this.共通Dynamic(CallSite.Target(CallSite,arg1,arg2),Dynamic0);
    }
    private void 共通Dynamic(object expected,Expressions.DynamicExpression Dynamic0){
        this.共通Expression(Dynamic0);
        var Lambda=Expressions.Expression.Lambda<Func<object>>(Dynamic0);
        this.共通Expression(Lambda);
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
            this.共通Expression(
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
    static Expressions.LambdaExpression Lambda<T>(Expressions.Expression<Func<T>> e)=>e;
    [Fact]
    public void Lambda0(){
        this.共通Expression(Lambda(()=>1));
    }
    [Fact]public void Lambda1(){
        //Expressions.Expression<Func<>>をExpressions.Expressionで呼び出した場合Expressions.Expressionでデシリアライズするとtypeが復元できない()
        this.共通Expression<Expressions.LambdaExpression>(Expressions.Expression.Lambda<Func<decimal>>(Expressions.Expression.Constant(2m)));
        this.共通Expression<Expressions.Expression>(Expressions.Expression.Lambda<Func<decimal>>(Expressions.Expression.Constant(2m)));
    }
    [Fact]public void Lambda2(){
        this.共通Expression<Expressions.LambdaExpression>(Expressions.Expression.Lambda<Func<decimal>>(Expressions.Expression.Constant(2m)));
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
        this.共通Expression<Expressions.LambdaExpression>(
            Expressions.Expression.Lambda<Func<decimal>>(
                Expressions.Expression.Block(
                    new[] { @decimal },
                    Expressions.Expression.TryCatchFinally(
                        Expressions.Expression.PostIncrementAssign(@decimal),
                        @decimal,
                        Expressions.Expression.Catch(
                            typeof(Exception),
                            @decimal
                        )
                    )
                )
            )
        );
        this.共通Expression<Expressions.LambdaExpression>(
            Expressions.Expression.Lambda<Func<decimal,decimal>>(
                Expressions.Expression.TryCatchFinally(
                    @decimal,
                    @decimal
                ),
                @decimal
            )
        );
        this.共通Expression<Expressions.LambdaExpression>(
            Expressions.Expression.Lambda<Func<decimal>>(
                Expressions.Expression.Block(
                    new[] { @decimal },
                    Expressions.Expression.TryCatchFinally(
                        @decimal,
                        @decimal
                    ),
                    @decimal
                )
            )
        );
        this.共通Expression<Expressions.LambdaExpression>(
            Expressions.Expression.Lambda<Func<decimal>>(
                Expressions.Expression.Block(
                    new[] { @decimal },
                    Expressions.Expression.TryCatchFinally(
                        Expressions.Expression.PostIncrementAssign(@decimal),
                        Expressions.Expression.Assign(
                            @decimal,
                            Expressions.Expression.Constant(2m)
                        )
                    ),
                    @decimal
                )
            )
        );
        this.共通Expression<Expressions.LambdaExpression>(
            Expressions.Expression.Lambda<Func<decimal>>(
                Expressions.Expression.Block(
                    new[] { @decimal },
                    Expressions.Expression.Assign(
                        @decimal,
                        Expressions.Expression.Constant(1m)
                    ),
                    Expressions.Expression.TryCatchFinally(
                        Expressions.Expression.PostIncrementAssign(@decimal),
                        Expressions.Expression.Assign(
                            @decimal,
                            Expressions.Expression.Constant(2m)
                        )
                    ),
                    @decimal
                )
            )
        );
        this.共通Expression<Expressions.LambdaExpression>(
            Expressions.Expression.Lambda<Func<decimal>>(
                Expressions.Expression.Block(
                    new[] { @decimal },
                    Expressions.Expression.Assign(
                        @decimal,
                        Expressions.Expression.Constant(1m)
                    ),
                    Expressions.Expression.TryCatchFinally(
                        Expressions.Expression.PostIncrementAssign(@decimal),
                        Expressions.Expression.Assign(
                            @decimal,
                            Expressions.Expression.Constant(2m)
                        )
                    ),
                    @decimal
                )
            )
        );
    }
    [Fact]public void Lambda4(){
        var Exception=Expressions.Expression.Parameter(typeof(Exception));
        this.共通Expression<Expressions.LambdaExpression>(
            Expressions.Expression.Lambda<Func<decimal,decimal>>(
                Expressions.Expression.TryCatchFinally(
                    @decimal,
                    @decimal,
                    Expressions.Expression.Catch(Exception,@decimal,Expressions.Expression.Constant(true))
                ),
                @decimal
            )
        );
    }
    [Fact]public void Lambda5(){
        var Array2=Expressions.Expression.Parameter(typeof(int[,]));
        this.共通Expression<Expressions.LambdaExpression>(
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
        this.共通Expression<Expressions.LambdaExpression>(
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
        this.共通Expression(
            Expressions.Expression.NewArrayInit(
                typeof(int),
                Expressions.Expression.Constant(0),
                Expressions.Expression.Constant(1)
            )
        );
    }
    [Fact]
    public void NewArrayBounds(){
        this.共通Expression(
            Expressions.Expression.NewArrayBounds(
                typeof(int),
                Expressions.Expression.Constant(0),
                Expressions.Expression.Constant(1)
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
        var Constant_1 = Expressions.Expression.Constant(1);
        var ctor = Type.GetConstructor(new[] {
            typeof(int)
        });
        var New = Expressions.Expression.New(
            ctor,
            Constant_1
        );
        this.共通Expression(
            Expressions.Expression.MemberInit(
                New,
                Expressions.Expression.Bind(
                    Int32フィールド1,
                    Constant_1
                )
            )
        );
        this.共通Expression(
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
    [Fact]
    public void TypeEqual(){
        this.共通Expression(
            Expressions.Expression.TypeEqual(
                Expressions.Expression.Constant(1m),
                typeof(decimal)
            )
        );
    }
    [Fact]
    public void TypeIs(){
        this.共通Expression(
            Expressions.Expression.TypeIs(
                Expressions.Expression.Constant(1m),
                typeof(decimal)
            )
        );
    }
    private static readonly Expressions.LabelTarget Label_decimal=Expressions.Expression.Label(typeof(decimal),"Label_decimal");
    private static readonly Expressions.LabelTarget Label_void=Expressions.Expression.Label("Label");
    [Fact]
    public void Loop0(){
        this.共通Expression(
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
        this.共通Expression(
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
        this.共通Expression(Expressions.Expression.Negate(Expressions.Expression.Constant(1m)));
    }
    [Fact]public void Index0(){
        var List=Expressions.Expression.Parameter(typeof(List<int>));
        this.共通Expression((Expressions.Expression)
            Expressions.Expression.Block(
                new[] { List },
                Expressions.Expression.MakeIndex(
                    List,
                    typeof(List<int>).GetProperty("Item"),
                    new []{Expressions.Expression.Constant(0)}
                )
            )
        );
    }
    [Fact]public void Index1(){
        var Array1=Expressions.Expression.Parameter(typeof(int[]));
        this.共通Expression(
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
    public void Index2(){
        var Array2=Expressions.Expression.Parameter(typeof(int[,]));
        this.共通Expression(
            Expressions.Expression.Lambda(
                Expressions.Expression.ArrayIndex(
                    Array2,
                    Expressions.Expression.Constant(0),
                    Expressions.Expression.Constant(0)
                ),
                Array2
            )
        );
    }
    [Fact]
    public void Index3(){
        var Array2=Expressions.Expression.Parameter(typeof(int[,,]));
        this.共通Expression(
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
        this.共通Expression(
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
        this.共通Expression(
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
        this.共通Expression(
            Expressions.Expression.ListInit(
                Expressions.Expression.New(typeof(List<int>)),
                Expressions.Expression.ElementInit(typeof(List<int>).GetMethod("Add")!,Expressions.Expression.Constant(1))
            )
        );
    }
    [Fact]
    public void MemberExpression(){
        var Point=Expressions.Expression.Parameter(typeof(Point));
        this.共通Expression(Expressions.Expression.Block(new[]{Point},Expressions.Expression.MakeMemberAccess(Point,typeof(Point).GetProperty("X")!)));
    }
    //[Fact]
    //public void Null(){
    //    this.共通Expression<Expressions.Expression?>(null);
    //}
    [Fact]
    public void GreaterThan(){
        this.共通Expression(Expressions.Expression.GreaterThan(Expressions.Expression.Constant(1m),Expressions.Expression.Constant(1m)));
    }

    [Fact]
    public void Assign(){
        this.共通Expression(Expressions.Expression.Block(new[]{@string},Expressions.Expression.Assign(@string,@string)));
    }
    [Fact]
    public void Invoke(){
        this.共通Expression(
            Expressions.Expression.Invoke(
                Expressions.Expression.Lambda(@string,@string),
                Expressions.Expression.Constant("B")
            )
        );
    }
    [Fact]public void New(){
        this.共通Expression(
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
    public void Call(){
        this.共通Expression(
            Expressions.Expression.Call(
                M(()=>string.Concat("","")),
                Expressions.Expression.Constant("A"),
                Expressions.Expression.Constant("B")
            )
        );
    }
}
