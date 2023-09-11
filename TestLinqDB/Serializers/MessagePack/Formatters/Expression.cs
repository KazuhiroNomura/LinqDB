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
using System.Linq.Expressions;
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
using System.Reflection;
using System.Runtime.CompilerServices;
using LinqDB.Helpers;
using Expressions=System.Linq.Expressions;
using Binder = Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using static LinqDB.Optimizers.Optimizer;
using MessagePack;
using Utf8Json;

namespace Serializers.MessagePack.Formatters;
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
    private static readonly ParameterExpression int8    = Expressions.Expression.Parameter(typeof(sbyte),"int8");
    private static readonly ParameterExpression int16   = Expressions.Expression.Parameter(typeof(short),"int16");
    private static readonly ParameterExpression int32   = Expressions.Expression.Parameter(typeof(int),"int32");
    private static readonly ParameterExpression int64   = Expressions.Expression.Parameter(typeof(long),"int64");
    private static readonly ParameterExpression uint8   = Expressions.Expression.Parameter(typeof(byte),"uint8");
    private static readonly ParameterExpression uint16  = Expressions.Expression.Parameter(typeof(ushort),"uint16");
    private static readonly ParameterExpression uint32  = Expressions.Expression.Parameter(typeof(uint),"uint32");
    private static readonly ParameterExpression uint64  = Expressions.Expression.Parameter(typeof(ulong),"uint64");
    private static readonly ParameterExpression @float  = Expressions.Expression.Parameter(typeof(float),"float");
    private static readonly ParameterExpression @double = Expressions.Expression.Parameter(typeof(double),"double");
    private static readonly ParameterExpression @decimal= Expressions.Expression.Parameter(typeof(decimal),"decimal");
    private static readonly ParameterExpression @string = Expressions.Expression.Parameter(typeof(string),"string");
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
    public struct TestDynamic<T>
    {
        public readonly T メンバー1;
        public readonly T メンバー2;

        public TestDynamic(T メンバー)
        {
            this.メンバー1 = メンバー;
            this.メンバー2 = メンバー;
        }
    }
    [Fact]public void Dynamic()
    {
        var CSharpArgumentInfo1 = Binder.CSharpArgumentInfo.Create(Binder.CSharpArgumentInfoFlags.None, null);
        var CSharpArgumentInfoArray1 = new[] {
            CSharpArgumentInfo1
        };
        var CSharpArgumentInfoArray2 = new[] {
            CSharpArgumentInfo1,
            CSharpArgumentInfo1
        };
        var CSharpArgumentInfoArray3 = new[] {
            CSharpArgumentInfo1,
            CSharpArgumentInfo1,
            CSharpArgumentInfo1
        };
        //if(!this.SequenceEqual(a.Arguments,b.Arguments)) return false;
        {
            this.共通Expression(
                Expressions.Expression.Dynamic(
                    Binder.Binder.UnaryOperation(
                        Binder.CSharpBinderFlags.None,
                        Expressions.ExpressionType.Increment,
                        typeof(Expression),
                        CSharpArgumentInfoArray1
                    ),
                    typeof(object),
                    Expressions.Expression.Constant(1,typeof(object))
                )
            );
        }
        //if(a_ConvertBinder!=null) {
        //    if(a_ConvertBinder.ReturnType!=b_ConvertBinder.ReturnType)return false;
        {
            var Constant_1L = Expressions.Expression.Constant(1L);
            this.共通Expression(
                Expressions.Expression.Dynamic(
                    Binder.Binder.Convert(
                        Binder.CSharpBinderFlags.ConvertExplicit,
                        typeof(double),
                        typeof(Expression)
                    ),
                    typeof(object),
                    Constant_1L
                )
            );
        }
        //    return a_ConvertBinder.Explicit==b_ConvertBinder.Explicit;
        {
            var Constant_1L = Expressions.Expression.Constant(1L);
            this.共通Expression(
                Expressions.Expression.Dynamic(
                    Binder.Binder.Convert(
                        Binder.CSharpBinderFlags.ConvertExplicit,
                        typeof(double),
                        typeof(Expression)
                    ),
                    typeof(object),
                    Constant_1L
                )
            );
        }
        //if(a_GetMemberBinder!=null) {
        {
            this.共通Expression(
                Expressions.Expression.Dynamic(
                    Binder.Binder.GetMember(
                        Binder.CSharpBinderFlags.None,
                        nameof(TestDynamic<int>.メンバー1),
                        typeof(Expression),
                        CSharpArgumentInfoArray1
                    ),
                    typeof(object),
                    Expressions.Expression.Constant(new TestDynamic<int>(1))
                )
            );
        }
        //    return a_GetMemberBinder.Name.Equals(b_GetMemberBinder.Name,StringComparison.Ordinal);
        {
            this.共通Expression(
                Expressions.Expression.Dynamic(
                    Binder.Binder.GetMember(
                        Binder.CSharpBinderFlags.ResultIndexed,
                        nameof(TestDynamic<int>.メンバー1),
                        typeof(Expression),
                        CSharpArgumentInfoArray1
                    ),
                    typeof(object),
                    Expressions.Expression.Constant(new TestDynamic<int>(1))
                )
            );
        }
        //}
        //if(a_SetMemberBinder!=null) {
        //    return a_SetMemberBinder.Name.Equals(b_SetMemberBinder.Name,StringComparison.Ordinal);
        {
            this.共通Expression(
                Expressions.Expression.Dynamic(
                    Binder.Binder.SetMember(
                        Binder.CSharpBinderFlags.ResultIndexed,
                        nameof(TestDynamic<int>.メンバー1),
                        typeof(Expression),
                        CSharpArgumentInfoArray2
                    ),
                    typeof(object),
                    Expressions.Expression.Constant(new TestDynamic<int>(1)),
                    Expressions.Expression.Constant(2)
                )
            );
        }
        //}
        //if(a_GetIndexBinder!=null) {
        {
            const int expected = 2;
            var Array = new[] {
                1,expected,3
            };
            this.共通Expression(
                Expressions.Expression.Dynamic(
                    Binder.Binder.GetIndex(
                        Binder.CSharpBinderFlags.None,
                        typeof(Expression),
                        CSharpArgumentInfoArray2
                    ),
                    typeof(object),
                    Expressions.Expression.Constant(Array),
                    Expressions.Expression.Constant(1)
                )
            );
        }
        //}
        //if(a_SetIndexBinder!=null) {
        //    return a_SetIndexBinder.CallInfo.Equals(b_SetIndexBinder.CallInfo);
        {
            var Array = new[] {
                1,2,3
            };
            this.共通Expression(
                Expressions.Expression.Dynamic(
                    Binder.Binder.SetIndex(
                        Binder.CSharpBinderFlags.None,
                        typeof(Expression),
                        CSharpArgumentInfoArray3
                    ),
                    typeof(object),
                    Expressions.Expression.Constant(Array),
                    Expressions.Expression.Constant(1),
                    Expressions.Expression.Constant(2)
                )
            );
        }
        //}
        //return true;
        {
            var オペランド = Expressions.Expression.Dynamic(
                Binder.Binder.BinaryOperation(
                    Binder.CSharpBinderFlags.None,
                    Expressions.ExpressionType.Add,
                    typeof(Expression),
                    new[] {
                        Binder.CSharpArgumentInfo.Create(Binder.CSharpArgumentInfoFlags.None,null),
                        Binder.CSharpArgumentInfo.Create(Binder.CSharpArgumentInfoFlags.None,null)
                    }
                ),
                typeof(object),
                Expressions.Expression.Constant(1),
                Expressions.Expression.Constant(1)
            );
            this.共通Expression(オペランド);
        }
    }
    static LambdaExpression Lambda<T>(Expression<Func<T>> e)=>e;
    [Fact]
    public void Lambda0(){
        this.共通Expression(Lambda(()=>1));
    }
    [Fact]public void Lambda1(){
        //Expressions.Expression<Func<>>をExpressions.Expressionで呼び出した場合Expressions.Expressionでデシリアライズするとtypeが復元できない()
        this.共通Expression<LambdaExpression>(Expressions.Expression.Lambda<Func<decimal>>(Expressions.Expression.Constant(2m)));
        this.共通Expression<Expressions.Expression>(Expressions.Expression.Lambda<Func<decimal>>(Expressions.Expression.Constant(2m)));
    }
    [Fact]public void Lambda2(){
        this.共通Expression<LambdaExpression>(Expressions.Expression.Lambda<Func<decimal>>(Expressions.Expression.Constant(2m)));
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
        this.共通Expression<LambdaExpression>(
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
        this.共通Expression<LambdaExpression>(
            Expressions.Expression.Lambda<Func<decimal,decimal>>(
                Expressions.Expression.TryCatchFinally(
                    @decimal,
                    @decimal
                ),
                @decimal
            )
        );
        this.共通Expression<LambdaExpression>(
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
        this.共通Expression<LambdaExpression>(
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
        this.共通Expression<LambdaExpression>(
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
        this.共通Expression<LambdaExpression>(
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
        this.共通Expression<LambdaExpression>(
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
        this.共通Expression<LambdaExpression>(
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
        this.共通Expression<LambdaExpression>(
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
                    GotoExpressionKind.Return,
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
                    GotoExpressionKind.Goto,
                    target,
                    Expressions.Expression.Constant(2),
                    typeof(double)
                ),
                Expressions.Expression.MakeGoto(
                    GotoExpressionKind.Break,
                    target,
                    Expressions.Expression.Constant(3),
                    typeof(decimal)
                ),
                Expressions.Expression.MakeGoto(
                    GotoExpressionKind.Continue,
                    target,
                    Expressions.Expression.Constant(4),
                    typeof(float)
                ),
                Expressions.Expression.MakeGoto(
                    GotoExpressionKind.Return,
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
    static MethodInfo M<T>(Expression<Func<T>> e){
        var Method=((MethodCallExpression)e.Body).Method;
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
