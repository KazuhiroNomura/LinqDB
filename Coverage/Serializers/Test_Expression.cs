using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CoverageCS.LinqDB;
using LinqDB.Helpers;
using LinqDB.Optimizers;
using LinqDB.Remote.Clients;
using LinqDB.Serializers;
using LinqDB.Serializers.Formatters;
//using LinqDB.Serializers.Formatters;
//using LinqDB.Serializers.MessagePack;
using MessagePack;
//using MessagePack.Resolvers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utf8Json;
using static System.Diagnostics.Contracts.Contract;
using Assert=Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Json=Newtonsoft.Json;
// ReSharper disable PossibleNullReferenceException
//具体的なAnonymousTypeをそのままSerialize,DeserializeするときはAnonymousExpressionResolverを通過しない。AnonymousTypeを返す。
//具体的なAnonymousTypeをObjectでSerialize,DeserializeするときはAnonymousExpressionResolverを通過しない。Dictionaryを返す。
namespace CoverageCS.Serializers;
[TestClass]
public class Test_Expression:ATest{
    //private static readonly IJsonFormatterResolver JsonFormatterResolver=global::LinqDB.Serializers.Utf8Json.Resolver.Instance;
    //private static readonly IJsonFormatterResolver JsonFormatterResolver=Utf8Json.Resolvers.CompositeResolver.Create(
    //    new[]{
    //        ExpressionFormatter.Instance,
    //    },
    //    new[]{
    //        Utf8Json.Resolvers.StandardResolver.Default,
    //        global::LinqDB.Serializers.Utf8Json.Resolver.Instance
    //    });
    private static readonly SerializerSet SerializerSet=new();
    private static readonly Optimizer.ExpressionEqualityComparer ExpressionEqualityComparer=new(new List<ParameterExpression>());
    [TestMethod]
    public void Anonymous000(){
        共通object(new{
            a=1,
            b=2.0,
            c=3m,
            d=4f,
            e="e"
        });
    }
    private static void 共通object(object input){
        Private共通object(input,output=>Assert.IsTrue(Comparer.Equals(output,input)));
        Private共通object<object>(input,output=>Assert.IsTrue(Comparer.Equals(output,input)));
    }
    [TestMethod]
    public void Anonymous001(){
        共通object(new{
            a=1
        });
    }
    [TestMethod]
    public void Anonymous002(){
        共通object((object)new{
            a=(object)1
        });
    }
    [TestMethod]
    public void Anonymous003(){
        共通object((object)new{
            a=(object)new{aa=11}
        });
    }
    [TestMethod]
    public void Anonymous004(){
        共通object(new{
            a=(object)new{aa=1}
        });
    }
    [TestMethod]
    public void Anonymous010(){
        共通Expression(Expression.Constant(11));
    }
    [TestMethod]
    public void Anonymous011(){
        共通Expression(Expression.Constant(new{a=11}));
    }
    [TestMethod]
    public void Anonymous012(){
        共通Expression(Expression.Constant(new{a=11,b=2.2,c=33m,d=44f,e="ee"}));
    }
    [TestMethod]
    public void Anonymous021(){
        共通object(
            new{
                x=1
            }
        );
    }
    [TestMethod]
    public void Anonymous022(){
        共通object(
            new{
                x=new{
                    a=111
                }
            }
        );
    }
    [TestMethod]
    public void Anonymous023(){
        共通object(
            new{
                x=new{
                    a=111,
                    b=222.0,
                    c=333m,
                    d=444f,
                    e="eee"
                }
            }
        );
    }
    [TestMethod]
    public void Anonymous030(){
        共通object(
            Tuple.Create(1)
        );
    }
    [TestMethod]
    public void Anonymous031(){
        共通object(
            Tuple.Create(
                new{
                    a=111,
                    b=222.0,
                    c=333m,
                    d=444f,
                    e="eee"
                }
            )
        );
    }
    [TestMethod]
    public void Anonymous040(){
        共通object(new{
            a=new{aa=1},
            b=new{aa=1}
        });
    }
    [TestMethod]
    public void Anonymous041(){
        共通object(new{
            a=new{aa=1},
            b=(object)new{aa=1}
        });
    }
    [TestMethod]
    public void Anonymous05(){
        共通object(
            Tuple.Create(
                new{
                    a=1111
                }
            )
        );
    }
    [TestMethod]
    public void Anonymous06(){
        共通object(
            new{
                a=(object)new{
                    a=1111
                }
            }
        );
    }
    [TestMethod]
    public void Anonymous07(){
        共通object(
            new[]{
                (object)new{
                    a=1111
                },
                (object)new{
                    a=2222
                }
            }
        );
    }
    [TestMethod]
    public void Anonymous08(){
        共通object(
            new[]{
                new{
                    a=1111
                },
                new{
                    a=2222
                }
            }
        );
    }
    [TestMethod]
    public void Anonymous09(){
        共通object(
            new[]{
                new{
                    a=1111
                },
                new{
                    a=2222
                }
            }.ToList()
        );
    }
    [TestMethod]
    public void Anonymous10(){
        共通object<object>(
            (object)new{
                a=1111
            }
        );
    }
    [TestMethod]
    public void Anonymous11(){
        共通object(new{
            a=(object)new{aa=1}
        });
    }
    [MessagePackObject(true)]
    public class 独自Class:IEquatable<独自Class>{
        public int a=3;
        public string b="b";
        public bool Equals(独自Class? other){
            if(ReferenceEquals(null,other)) return false;
            if(ReferenceEquals(this,other)) return true;
            return this.a==other.a&&this.b==other.b;
        }
        public override bool Equals(object? obj){
            if(ReferenceEquals(null,obj)) return false;
            if(ReferenceEquals(this,obj)) return true;
            if(obj.GetType()!=this.GetType()) return false;
            return this.Equals((独自Class)obj);
        }
        public override int GetHashCode(){
            return HashCode.Combine(this.a,this.b);
        }
        public static bool operator==(独自Class? left,独自Class? right){
            return Equals(left,right);
        }
        public static bool operator!=(独自Class? left,独自Class? right){
            return!Equals(left,right);
        }
    }
    [TestMethod]
    public void 独自Class1(){
        var t=Tuple.Create(new 独自Class());
        共通object(t);
    }
    [TestMethod]
    public void 独自Class2(){
        var t=new 独自Class();
        共通object(t);
    }
    private static readonly ParameterExpression @decimal = Expression.Parameter(typeof(decimal),"p");
    [TestMethod]
    public void Block(){
        共通Expression(
            Expression.Block(
                new[] { @decimal },
                Expression.TryCatchFinally(
                    Expression.Increment(@decimal),
                    @decimal
                )
            )
        );
        共通Expression(
            Expression.Block(
                new[] { @decimal },
                Expression.TryCatchFinally(
                    Expression.Increment(@decimal),
                    Expression.Constant(2)
                )
            )
        );
        共通Expression(
            Expression.Block(
                new[] { @decimal },
                Expression.TryCatchFinally(
                    Expression.PostIncrementAssign(@decimal),
                    Expression.Constant(2)
                )
            )
        );
        共通Expression(
            Expression.Block(
                new[] { @decimal },
                Expression.TryCatchFinally(
                    @decimal,
                    @decimal
                ),
                @decimal
            )
        );
        共通Expression(
            Expression.Block(
                new[] { @decimal },
                Expression.TryCatchFinally(
                    Expression.Constant(2),
                    @decimal
                )
            )
        );
        共通Expression(
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
        共通Expression(
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
    [TestMethod]
    public void Constant(){
        共通Expression(
            Expression.Constant(null,typeof(string))
        );
        共通Expression(Expression.Constant(1111m));
        共通Expression(Expression.Constant(1111m));
    }
    static LambdaExpression Lambda<T>(Expression<Func<T>> e)=>e;
    [TestMethod]
    public void Lambda0(){
        共通Expression(Lambda(()=>1));

    }
    [TestMethod]
    public void Lambda1(){
        //Expression<Func<>>をExpressionで呼び出した場合Expressionでデシリアライズするとtypeが復元できない()
        共通Expression<LambdaExpression>(Expression.Lambda<Func<decimal>>(Expression.Constant(2m)));
        共通Expression<Expression>(Expression.Lambda<Func<decimal>>(Expression.Constant(2m)));
        共通Expression(Expression.Lambda<Func<decimal>>(Expression.Constant(2m)));
        //const decimal Catch値 = 40, Finally値 = 30;
        //Expression.TryCatchFinally(
        //    //Expression.PostIncrementAssign(@decimal),
        //    Expression.Constant(Finally値),
        //    Expression.Constant(Finally値),
        //    Expression.Catch(
        //        typeof(Exception),
        //        Expression.Constant(Catch値)
        //    )
        //);
        共通Expression(
            Expression.Lambda<Func<decimal>>(
                Expression.Block(
                    new[] { @decimal },
                    Expression.TryCatchFinally(
                        Expression.PostIncrementAssign(@decimal),
                        @decimal,
                        Expression.Catch(
                            typeof(Exception),
                            @decimal
                        )
                    )
                )
            )
        );

        共通Expression(
            Expression.Lambda<Func<decimal,decimal>>(
                Expression.TryCatchFinally(
                    @decimal,
                    @decimal
                ),
                @decimal
            )
        );
        共通Expression(
            Expression.Lambda<Func<decimal>>(
                Expression.Block(
                    new[] { @decimal },
                    Expression.TryCatchFinally(
                        @decimal,
                        @decimal
                    ),
                    @decimal
                )
            )
        );
        共通Expression(
            Expression.Lambda<Func<decimal>>(
                Expression.Block(
                    new[] { @decimal },
                    Expression.TryCatchFinally(
                        Expression.PostIncrementAssign(@decimal),
                        Expression.Assign(
                            @decimal,
                            Expression.Constant(2m)
                        )
                    ),
                    @decimal
                )
            )
        );
        共通Expression(
            Expression.Lambda<Func<decimal>>(
                Expression.Block(
                    new[] { @decimal },
                    Expression.Assign(
                        @decimal,
                        Expression.Constant(1m)
                    ),
                    Expression.TryCatchFinally(
                        Expression.PostIncrementAssign(@decimal),
                        Expression.Assign(
                            @decimal,
                            Expression.Constant(2m)
                        )
                    ),
                    @decimal
                )
            )
        );
        共通Expression(
            Expression.Lambda<Func<decimal>>(
                Expression.Block(
                    new[] { @decimal },
                    Expression.Assign(
                        @decimal,
                        Expression.Constant(1m)
                    ),
                    Expression.TryCatchFinally(
                        Expression.PostIncrementAssign(@decimal),
                        Expression.Assign(
                            @decimal,
                            Expression.Constant(2m)
                        )
                    ),
                    @decimal
                )
            )
        );
        var Exception=Expression.Parameter(typeof(Exception));
        共通Expression(
            Expression.Lambda<Func<decimal,decimal>>(
                Expression.TryCatchFinally(
                    @decimal,
                    @decimal,
                    Expression.Catch(Exception,@decimal,Expression.Constant(true))
                ),
                @decimal
            )
        );
        var Array2=Expression.Parameter(typeof(int[,]));
        共通Expression(
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
        共通Expression(
            Expression.Lambda(
                Expression.ArrayIndex(
                    Array1,
                    Expression.Constant(0)
                ),
                Array1
            )
        );
    }
    [TestMethod]
    public void NewArrayInit(){
        共通Expression(
            Expression.NewArrayInit(
                typeof(int),
                Expression.Constant(0),
                Expression.Constant(1)
            )
        );
    }
    [TestMethod]
    public void NewArrayBounds(){
        共通Expression(
            Expression.NewArrayBounds(
                typeof(int),
                Expression.Constant(0),
                Expression.Constant(1)
            )
        );
    }
    [TestMethod]
    public void Default(){
        共通Expression(Expression.Default(typeof(int)));
        共通Expression(Expression.Default(typeof(decimal)));
    }
    [TestMethod]
    public void MemberInit(){
        var Type = typeof(BindCollection);
        var Int32フィールド1 = Type.GetField(nameof(BindCollection.Int32フィールド1));
        Assert.IsNotNull(Int32フィールド1);
        var Listフィールド1 = Type.GetField(nameof(BindCollection.Listフィールド1));
        Assert.IsNotNull(Listフィールド1);
        var Listフィールド2 = Type.GetField(nameof(BindCollection.Listフィールド2))!;
        Assert.IsNotNull(Listフィールド2);
        var Constant_1 = Expression.Constant(1);
        var ctor = Type.GetConstructor(new[] {
            typeof(int)
        });
        var New = Expression.New(
            ctor,
            Constant_1
        );
        共通Expression(
            Expression.MemberInit(
                New,
                Expression.Bind(
                    Int32フィールド1,
                    Constant_1
                )
            )
        );
        共通Expression(
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
    [TestMethod]
    public void TypeEqual(){
        共通Expression(
            Expression.TypeEqual(
                Expression.Constant(1m),
                typeof(decimal)
            )
        );
    }
    [TestMethod]
    public void TypeIs(){
        共通Expression(
            Expression.TypeIs(
                Expression.Constant(1m),
                typeof(decimal)
            )
        );
    }
    private static readonly LabelTarget Label_decimal=Expression.Label(typeof(decimal),"Label_decimal");
    private static readonly LabelTarget Label_void=Expression.Label("Label");
    [TestMethod]
    public void Loop(){
        共通Expression(
            Expression.Loop(
                Expression.Block(
                    Expression.Break(Label_decimal,Expression.Constant(1m)),
                    Expression.Continue(Label_void)
                ),
                Label_decimal,
                Label_void
            )
        );
        共通Expression(
            Expression.Loop(
                Expression.Block(
                    Expression.Break(Label_decimal,Expression.Constant(1m))
                ),
                Label_decimal
            )
        );
    }
    [TestMethod]
    public void Negate(){
        共通Expression(Expression.Negate(Expression.Constant(1m)));
    }
    [TestMethod]
    public void ArrayIndex(){
        var List=Expression.Parameter(typeof(List<int>));
        共通Expression(
            Expression.Block(
                new[] { List },
                Expression.MakeIndex(
                    List,
                    typeof(List<int>).GetProperty("Item"),
                    new []{Expression.Constant(0)}
                )
            )
        );
        var Array1=Expression.Parameter(typeof(int[]));
        共通Expression(
            Expression.Lambda(
                Expression.ArrayIndex(
                    Array1,
                    Expression.Constant(0)
                ),
                Array1
            )
        );
        var Array2=Expression.Parameter(typeof(int[,]));
        共通Expression(
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
    [TestMethod]
    public void Condition(){
        共通Expression(Expression.Add(Expression.Constant(1m),Expression.Constant(1m)));
        共通Expression(
            Expression.Condition(
                Expression.Constant(true),
                Expression.Constant(1m),
                Expression.Constant(2m)
            )
        );
    }
    [TestMethod]
    public void Goto(){
        var target=Expression.Label(typeof(int),"target");
        共通Expression(
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
        共通Expression(
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
    [TestMethod]
    public void ListInit(){
        共通Expression(
            Expression.ListInit(
                Expression.New(typeof(List<int>)),
                Expression.ElementInit(typeof(List<int>).GetMethod("Add")!,Expression.Constant(1))
            )
        );
    }
    [TestMethod]
    public void MemberExpression(){
        var Point=Expression.Parameter(typeof(Point));
        共通Expression(Expression.Block(new[]{Point},Expression.MakeMemberAccess(Point,typeof(Point).GetProperty("X")!)));
    }
    [TestMethod]
    public void Null(){
        共通Expression<Expression?>(null);
    }
    [TestMethod]
    public void GreaterThan(){
        共通Expression(Expression.GreaterThan(Expression.Constant(1m),Expression.Constant(1m)));
    }
    private static readonly ParameterExpression @string=Expression.Parameter(typeof(string),"p");

    [TestMethod]
    public void Assign(){
        共通Expression(Expression.Block(new[]{@string},Expression.Assign(@string,@string)));
    }
    [TestMethod]
    public void Invoke(){
        共通Expression(
            Expression.Invoke(
                Expression.Lambda(@string,@string),
                Expression.Constant("B")
            )
        );
    }
    [TestMethod]
    public void New(){
        共通Expression(
            Expression.New(
                typeof(ValueTuple<int,int>).GetConstructors()[0],
                Expression.Constant(1),
                Expression.Constant(2)
            )
        );
    }
    static MethodInfo M<T>(Expression<Func<T>> e){
        var Method=((MethodCallExpression)e.Body).Method;
        return Method;
    }
    [TestMethod]
    public void Call(){
        共通Expression(
            Expression.Call(
                M(()=>string.Concat("","")),
                Expression.Constant("A"),
                Expression.Constant("B")
            )
        );
    }
    [TestMethod]
    public void ValueTuple(){
        共通object((a:11,b:"bb",c:33m));
    }
    [TestMethod]
    public void Type_string(){
        共通object(typeof(string));
    }
    [TestMethod]
    public void Type_Func(){
        共通object(typeof(Func<int>));
    }
    [TestMethod]
    public void Type_カスタムデリゲート(){
        共通object(typeof(Client.サーバーで実行する式木<Func<int>>));
    }
    [TestMethod]
    public void Type(){
        共通object(typeof(string));
    }
    [TestMethod]
    public void MethodInfo(){
        共通object(typeof(string).GetMethods());
    }
    [TestMethod]
    public void MemberInfo(){
        共通object(typeof(string).GetMembers(BindingFlags.Instance|BindingFlags.Static|BindingFlags.Public|BindingFlags.NonPublic));
    }
    const string Messagepackファイル名="Messagepack.bin";
    const string Jsonファイル名="Json.txt";
    const string 整形済みJsonファイル名="整形済みJson.txt";
    private static void Private共通object<T>(T input,Action<T> AssertAction){
        SerializerSet.Clear();
        //var jsonString = MessagePackSerializer.ConvertToJson(MessagePackSerializer.Serialize(input, SerializerSet.MessagePackSerializerOptions));
        {
            SerializerSet.Clear();
            var JsonStream = new FileStream(Jsonファイル名,FileMode.Create,FileAccess.Write,FileShare.ReadWrite);
            JsonSerializer.Serialize(JsonStream,input,SerializerSet.JsonFormatterResolver);
            JsonStream.Close();
            var Json=File.ReadAllText(Jsonファイル名);
            File.WriteAllText(整形済みJsonファイル名,format_json(Json));
        }
        {
            SerializerSet.Clear();
            var json0=File.ReadAllText(Jsonファイル名);
            var json1=File.ReadAllText(整形済みJsonファイル名);
            //var o0=JsonSerializer.Deserialize<object>(json0,SerializerSet.JsonFormatterResolver);
            //var O0=(T)o0;
            var T0=JsonSerializer.Deserialize<T>(json0,SerializerSet.JsonFormatterResolver);
            SerializerSet.Clear();
            //var o1=JsonSerializer.Deserialize<object>(json1,SerializerSet.JsonFormatterResolver);
            //var O1=(T)o1;
            //var T1=JsonSerializer.Deserialize<T>(json1,SerializerSet.JsonFormatterResolver);
            AssertAction(T0);
            //AssertAction(T1);
        }
        {
            SerializerSet.Clear();
            var MessagepackStream = new FileStream(Messagepackファイル名,FileMode.Create,FileAccess.Write,FileShare.ReadWrite);
            MessagePackSerializer.Serialize(MessagepackStream,input,SerializerSet.MessagePackSerializerOptions);
            MessagepackStream.Close();
        }
        {
            SerializerSet.Clear();
            var MessagepackStream = new FileStream(Messagepackファイル名,FileMode.Open,FileAccess.ReadWrite,FileShare.ReadWrite);
            var output = MessagePackSerializer.Deserialize<T>(MessagepackStream,SerializerSet.MessagePackSerializerOptions);
            MessagepackStream.Close();
            AssertAction(output);
        }
    }
    private static void 共通object<T>(T[] input){
        Private共通object(input,output=>Assert.IsTrue(output.SequenceEqual(input)));
    }
    private static void 共通object<T>(T input){
        Private共通object<object>(input,output=>Assert.IsTrue(Comparer.Equals(output,input)));
        Private共通object(input,output=>Assert.IsTrue(Comparer.Equals(output,input)));
    }
    private static void 共通Expression<T>(T input)where T:Expression?{
        Private共通object<Expression>(input,output=>Assert.IsTrue(ExpressionEqualityComparer.Equals(output,input)));
    }
    static string format_json(string json){
        dynamic parsedJson = Json.JsonConvert.DeserializeObject(json)!;
        return Json.JsonConvert.SerializeObject(parsedJson, Json.Formatting.Indented);
    }
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
