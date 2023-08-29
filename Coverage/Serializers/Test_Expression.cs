using System.Drawing;
using System.Linq.Expressions;
using System.Reflection;
using LinqDB.Optimizers;
using LinqDB.Serializers;
//using LinqDB.Serializers.Formatters;
//using LinqDB.Serializers.MessagePack;
//using MessagePack.Resolvers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utf8Json;

using Assert=Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
// ReSharper disable PossibleNullReferenceException
//具体的なAnonymousTypeをそのままSerialize,DeserializeするときはAnonymousExpressionResolverを通過しない。AnonymousTypeを返す。
//具体的なAnonymousTypeをObjectでSerialize,DeserializeするときはAnonymousExpressionResolverを通過しない。Dictionaryを返す。
namespace CoverageCS.Serializers;
[TestClass]
public class Test_Expression:ATest_シリアライズ{
    private static readonly SerializerConfiguration SerializerConfiguration=new();
    private static readonly Optimizer.ExpressionEqualityComparer ExpressionEqualityComparer=new(new List<ParameterExpression>());
    //private static void 共通object(object input){
    //    Private共通object(input,output=>Assert.IsTrue(Comparer.Equals(output,input)));
    //    Private共通object<object>(input,output=>Assert.IsTrue(Comparer.Equals(output,input)));
    //}
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
    private static readonly ParameterExpression @decimal = Expression.Parameter(typeof(decimal),"p");
    [TestMethod]public void Block0(){
        共通Expression(
            Expression.Block(
                new[] { @decimal },
                @decimal
            )
        );
    }
    [TestMethod]public void Block1(){
        var q= Expression.Parameter(typeof(decimal),"q");
        共通Expression(
            Expression.Block(
                new[] { @decimal,q },
                @decimal
            )
        );
    }
    [TestMethod]public void Block2(){
        共通Expression(
            Expression.Block(
                new[]{Expression.Parameter(typeof(decimal),"a"),Expression.Parameter(typeof(decimal),"b"),@decimal},
                @decimal
            )
        );
    }
    [TestMethod]public void Block4(){
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
    }
    [TestMethod]public void Block10(){
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
    public void Binary(){
        共通Expression(
            Expression.Add(
                Expression.Constant(1),
                Expression.Constant(2)
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
    [TestMethod]public void Lambda1(){
        //Expression<Func<>>をExpressionで呼び出した場合Expressionでデシリアライズするとtypeが復元できない()
        共通Expression<LambdaExpression>(Expression.Lambda<Func<decimal>>(Expression.Constant(2m)));
        共通Expression<Expression>(Expression.Lambda<Func<decimal>>(Expression.Constant(2m)));
    }
    [TestMethod]public void Lambda2(){
        共通Expression<LambdaExpression>(Expression.Lambda<Func<decimal>>(Expression.Constant(2m)));
    }
    [TestMethod]public void Lambda3(){
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
        共通Expression<LambdaExpression>(
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

        共通Expression<LambdaExpression>(
            Expression.Lambda<Func<decimal,decimal>>(
                Expression.TryCatchFinally(
                    @decimal,
                    @decimal
                ),
                @decimal
            )
        );
        共通Expression<LambdaExpression>(
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
        共通Expression<LambdaExpression>(
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
        共通Expression<LambdaExpression>(
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
        共通Expression<LambdaExpression>(
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
        共通Expression<LambdaExpression>(
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
        共通Expression<LambdaExpression>(
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
        共通Expression<LambdaExpression>(
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
    public void ArrayIndex0(){
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
    }
    [TestMethod]
    public void ArrayIndex1(){
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
