using System.Drawing;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Binder = Microsoft.CSharp.RuntimeBinder.Binder;
// ReSharper disable AssignNullToNotNullAttribute
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class 沢山の組み合わせ : ATest
{
    public class 結果 : IEquatable<結果>
    {
        // ReSharper disable once MemberHidesStaticFromOuterClass
        public int Int32;
        public bool Boolean;
        // ReSharper disable once MemberHidesStaticFromOuterClass
        public string String;
        public 結果 _結果;
        public bool Equals(結果 other)
        {
            foreach (var field in typeof(結果).GetFields())
            {
                var a = field.GetValue(this)!;
                var b = field.GetValue(other)!;
                if (!a.Equals(b)) return false;
            }
            return true;
        }
        public override bool Equals(object obj)
        {
            return this.Equals((結果)obj);
        }
        public override int GetHashCode() => 0;
    }
    //private static MemberAssignment Bind(String フィールド名,Expression Expression){
    //    if(Expression.Type.IsValueType){
    //        Expression=Expression.Convert(
    //            Expression,
    //            typeof(Object)
    //        );
    //    }
    //    return Expression.Bind(
    //        typeof(結果).GetField(フィールド名),
    //        Expression
    //    );
    //}
    private static MemberAssignment BindBinary(ExpressionType NodeType, Expression オペランド1, Expression オペランド2)
    {
        Expression MakeBinary = Expression.MakeBinary(
            NodeType,
            オペランド1,
            オペランド2
        );
        if (MakeBinary.Type.IsValueType)
        {
            MakeBinary = Expression.Convert(
                MakeBinary,
                typeof(object)
            );
        }
        return Expression.Bind(
            typeof(結果).GetField(NodeType.ToString()),
            MakeBinary
        );
    }
    private static MemberAssignment BindUnary(ExpressionType NodeType, Expression オペランド1)
    {
        var MakeUnary = Expression.MakeUnary(
            NodeType,
            オペランド1,
            null
        );
        if (MakeUnary.Type.IsValueType)
        {
            MakeUnary = Expression.Convert(
                MakeUnary,
                typeof(object)
            );
        }
        return Expression.Bind(
            typeof(結果).GetField(NodeType.ToString()),
            MakeUnary
        );
    }
    private static T Call<T>(T o) => o;
    private static MemberAssignment Bind(string name, Expression Expression)
    {
        return Expression.Bind(
            typeof(結果).GetField(name),
            Expression.Convert(
                Expression,
                typeof(object)
            )
        );
    }
    //[TestMethod]
    //public void 結果にBind(){
    //    var e=Expression.MemberInit(
    //        Expression.New(
    //            typeof(結果)
    //        ),
    //        Bind(nameof(結果.Int32),this.Typeに対応したExpression(typeof(Int32))),
    //        Bind(nameof(結果.Boolean),this.Booleanに対応したExpression()),
    //        Bind(nameof(結果.String),this.Stringに対応したExpression()),
    //        Bind(nameof(結果._結果),this.結果に対応したExpression())
    //    );
    //    var x=this._変数Cache.AssertExecute(
    //        Expression.Lambda<Func<結果>>(e)
    //    );
    //}
    private int Arrayの添字;
    private void Array共通(Expression Expression)
    {
        try
        {
            this.ListExpression.Add(
                Expression.Assign(
                    Expression.ArrayAccess(
                        結果Array,
                        Expression.Constant(this.Arrayの添字++)
                    ),
                    Expression
                )
            );
        }
        catch (InvalidOperationException)
        {

        }
    }
    private void ArrayBinary(Type Type, ExpressionType NodeType, Expression オペランド1, Expression オペランド2)
    {
        try
        {
            var Binary = Expression.MakeBinary(
                NodeType,
                オペランド1,
                オペランド2
            );
            if (Type == Binary.Type)
            {
                this.ListExpression.Add(
                    Expression.Assign(
                        Expression.ArrayAccess(
                            結果Array,
                            Expression.Constant(this.Arrayの添字++)
                        ),
                        Binary
                    )
                );
            }
        }
        catch (InvalidOperationException)
        {

        }
    }
    private void ArrayUnary(Type Type, ExpressionType NodeType, Expression オペランド1)
    {
        try
        {
            var Unary = Expression.MakeUnary(
                NodeType,
                オペランド1,
                null
            );
            if (Type == Unary.Type)
            {
                this.ListExpression.Add(
                    Expression.Assign(
                        Expression.ArrayAccess(
                            結果Array,
                            Expression.Constant(this.Arrayの添字++)
                        ),
                        Unary
                    )
                );
            }
        }
        catch (InvalidOperationException)
        {

        }
    }
    private static ParameterExpression 結果Array;
    //private static readonly ParameterExpression ParameterArray = Expression.Parameter(typeof(Int32[]), "Array");
    //private static readonly ParameterExpression ParameterInt32 = Expression.Parameter(typeof(Int32), "Int32");
    private static ParameterExpression Parameter;
    //private static readonly ParameterExpression ParameterList = Expression.Parameter(typeof(List<Int32>), "List");
    //private static readonly ParameterExpression ParameterNullableInt32 = Expression.Parameter(typeof(Int32?), "NullableInt32");
    private static readonly ParameterExpression ParameterBoolean = Expression.Parameter(typeof(bool), "Boolean");
    private static readonly ParameterExpression ParameterDouble = Expression.Parameter(typeof(double), "Double");
    //private static ConstantExpression Constant=Expression.Constant(0);
    // private static ConstantExpression Constant1i=Expression.Constant(0);
    // private static ConstantExpression Constant=Expression.Constant(1111);
    private static readonly ConstantExpression Constant1d = Expression.Constant(1d);
    private static readonly ConstantExpression Constant_false = Expression.Constant(false);
    private static readonly ConstantExpression Constant_true = Expression.Constant(true);
    private static readonly ConstantExpression ConstantObject = Expression.Constant("ABC", typeof(object));
    private static LabelTarget GotoTarget=null!;
    private static LabelTarget LoopBreak = null!;
    private struct GenericStruct<T>
    {
        public T Field;
    }
    private readonly List<Expression> ListExpression = new();
    private Expression ArrayにBoolean代入()
    {
        var ListExpression = this.ListExpression;
        {
            const bool 値 = true;
            //var x=true+false;
            var Type = typeof(bool);
            this.Arrayの添字 = 0;
            結果Array = Expression.Parameter(Type.MakeArrayType(), "結果Array");
            GotoTarget = Expression.Label(Type);
            LoopBreak = Expression.Label(Type);
            var Parameter = Expression.Parameter(Type);
            var Constant = Expression.Constant(値);
            ListExpression.Clear();
            ListExpression.Add(Expression.Assign(Parameter, Expression.Default(Type)));
            this.ArrayBinary(Type,ExpressionType.Add,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.AddAssign,Parameter,Constant);
            this.ArrayBinary(Type,ExpressionType.AddAssignChecked,Parameter,Constant);
            this.ArrayBinary(Type,ExpressionType.AddChecked,Constant,Constant);
            this.ArrayBinary(Type, ExpressionType.And, Constant, Constant);
            this.ArrayBinary(Type, ExpressionType.AndAlso, Constant, Constant);
            this.ArrayBinary(Type, ExpressionType.AndAssign, Parameter, Constant);
            this.ArrayBinary(
                Type,
                ExpressionType.ArrayIndex,
                Expression.NewArrayBounds(
                    Type,
                    Expression.Constant(1)
                ),
                Expression.Constant(0)
            );
            this.ArrayBinary(Type, ExpressionType.Assign, Parameter, Constant);
            this.Array共通(
                Expression.Block(Constant)
            );
            this.Array共通(
                Expression.Call(
                    typeof(沢山の組み合わせ).GetMethod(nameof(Call), BindingFlags.Static | BindingFlags.NonPublic)!.MakeGenericMethod(typeof(bool)),
                    Constant
                )
            );
            this.ArrayBinary(Type, ExpressionType.Coalesce, Constant, Constant);
            this.Array共通(
                Expression.Condition(
                    Expression.Constant(true),
                    Constant,
                    Constant
                )
            );
            this.Array共通(
                Expression.Convert(
                    Constant,
                    Type
                )
            );
            this.Array共通(
                Expression.ConvertChecked(
                    Constant,
                    Type
                )
            );
            this.Array共通(
                Expression.Block(
                    Expression.DebugInfo(
                        Expression.SymbolDocument("ファイル名"),
                        1,
                        1,
                        2,
                        1
                    ),
                    Constant
                )
            );
            this.ArrayUnary(Type, ExpressionType.Decrement, Constant);
            this.Array共通(
                Expression.Default(Type)
            );
            this.ArrayBinary(Type, ExpressionType.Divide, Constant, Constant);
            this.ArrayBinary(Type, ExpressionType.DivideAssign, Parameter, Constant);
            this.ArrayBinary(Type, ExpressionType.Equal, Constant, Constant);
            this.ArrayBinary(Type, ExpressionType.ExclusiveOr, Constant, Constant);
            this.ArrayBinary(Type, ExpressionType.ExclusiveOrAssign, Parameter, Constant);
            this.Array共通(
                Expression.Block(
                    Expression.Goto(
                        GotoTarget,
                        Constant
                    ),
                    Expression.Label(
                        GotoTarget,
                        Constant
                    )
                )
            );
            this.ArrayBinary(Type, ExpressionType.GreaterThan, Constant, Constant);
            this.ArrayBinary(Type, ExpressionType.GreaterThanOrEqual, Constant, Constant);
            this.ArrayUnary(Type, ExpressionType.Increment, Constant);
            this.Array共通(
                Expression.ArrayAccess(
                    Expression.NewArrayInit(
                        Type,
                        Expression.Constant(true)
                    ),
                    Expression.Constant(0)
                )
            );
            this.Array共通(
                Expression.Invoke(
                    Expression.Constant(
                        (Func<bool>)(() => true)
                    )
                )
            );
            this.Array共通(
                Expression.Label(
                    Expression.Label(Type),
                    Constant
                )
            );
            this.ArrayBinary(Type,ExpressionType.LeftShift,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.LeftShiftAssign,Parameter,Constant);
            this.ArrayBinary(Type,ExpressionType.LessThan,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.LessThanOrEqual,Constant,Constant);
            this.Array共通(
                Expression.Loop(
                    Expression.Break(LoopBreak,Constant),
                    LoopBreak
                )
            );
            this.Array共通(
                Expression.Field(
                    Expression.New(typeof(GenericStruct<>).MakeGenericType(Type)),
                    nameof(GenericStruct<int>.Field)
                )
            );
            this.ArrayBinary(Type,ExpressionType.Modulo,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.ModuloAssign,Parameter,Constant);
            this.ArrayBinary(Type,ExpressionType.Multiply,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.MultiplyAssign,Parameter,Constant);
            this.ArrayBinary(Type,ExpressionType.MultiplyAssignChecked,Parameter,Constant);
            this.ArrayBinary(Type,ExpressionType.MultiplyChecked,Constant,Constant);
            this.ArrayUnary(Type,ExpressionType.Negate,Constant);
            this.ArrayUnary(Type,ExpressionType.NegateChecked,Constant);
            this.ArrayUnary(Type,ExpressionType.Not,Constant);
            this.ArrayBinary(Type,ExpressionType.NotEqual,Constant,Constant);
            this.ArrayUnary(Type,ExpressionType.OnesComplement,Constant);
            this.ArrayBinary(Type,ExpressionType.Or,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.OrAssign,Parameter,Constant);
            this.ArrayBinary(Type,ExpressionType.OrElse,Constant,Constant);
            this.Array共通(
                Constant
            );
            this.ArrayUnary(Type,ExpressionType.PostDecrementAssign,Parameter);
            this.ArrayUnary(Type,ExpressionType.PostIncrementAssign,Parameter);
            this.ArrayBinary(Type,ExpressionType.Power,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.PowerAssign,Parameter,Constant);
            this.ArrayUnary(Type,ExpressionType.PreDecrementAssign,Parameter);
            this.ArrayUnary(Type,ExpressionType.PreIncrementAssign,Parameter);
            this.ArrayBinary(Type,ExpressionType.RightShift,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.RightShiftAssign,Parameter,Constant);
            this.ArrayBinary(Type,ExpressionType.Subtract,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.SubtractAssign,Parameter,Constant);
            this.ArrayBinary(Type,ExpressionType.SubtractAssignChecked,Parameter,Constant);
            this.ArrayBinary(Type,ExpressionType.SubtractChecked,Constant,Constant);
            this.Array共通(
                Expression.Switch(
                    Expression.Constant(1),
                    Constant,
                    Expression.SwitchCase(
                        Constant,
                        Expression.Constant(11)
                    ),
                    Expression.SwitchCase(
                        Constant,
                        Expression.Constant(21),
                        Expression.Constant(22)
                    ),
                    Expression.SwitchCase(
                        Constant,
                        Expression.Constant(31),
                        Expression.Constant(32),
                        Expression.Constant(33)
                    )
                )
            );
            this.Array共通(
                Expression.TryCatch(
                    Expression.Condition(
                        Constant,
                        Expression.Throw(
                            Expression.New(typeof(Exception)),
                            Constant.Type
                        ),
                        Constant
                    ),
                    Expression.Catch(
                        typeof(Exception),
                        Constant
                    )
                )
            );
            this.Array共通(
                Expression.TryCatch(
                    Expression.Condition(
                        Constant,
                        Expression.Throw(
                            Expression.New(typeof(Exception)),
                            Constant.Type
                        ),
                        Constant
                    ),
                    Expression.Catch(
                        typeof(Exception),
                        Constant
                    )
                )
            );
            this.Array共通(
                Expression.Unbox(
                    Expression.Convert(
                        Constant,
                        typeof(object)
                    ),
                    Type
                )
            );
            ListExpression.Insert(
                0,
                Expression.Assign(
                    結果Array,
                    Expression.Constant(new bool[ListExpression.Count])
                )
            );
            ListExpression.Add(結果Array);
            return Expression.Block(
                new[] { 結果Array, Parameter },
                ListExpression
            );
        }
    }
    private Expression ArrayにInt32代入()
    {
        var ListExpression = this.ListExpression;
        {
            const int 値 = 1;
            var Type = 値.GetType();
            this.Arrayの添字 = 0;
            結果Array = Expression.Parameter(Type.MakeArrayType(), "結果Array");
            GotoTarget = Expression.Label(Type);
            LoopBreak = Expression.Label(Type);
            var Parameter = Expression.Parameter(Type);
            var Constant = Expression.Constant(値);
            ListExpression.Clear();
            ListExpression.Add(Expression.Assign(Parameter, Expression.Default(Type)));
            this.ArrayBinary(Type, ExpressionType.Add, Constant, Constant);
            this.ArrayBinary(Type, ExpressionType.AddAssign, Parameter, Constant);
            this.ArrayBinary(Type, ExpressionType.AddAssignChecked, Parameter, Constant);
            this.ArrayBinary(Type, ExpressionType.AddChecked, Constant, Constant);
            this.ArrayBinary(Type, ExpressionType.And, Constant, Constant);
            this.ArrayBinary(Type, ExpressionType.AndAlso, Constant, Constant);
            this.ArrayBinary(Type, ExpressionType.AndAssign, Parameter, Constant);
            this.ArrayBinary(
                Type,
                ExpressionType.ArrayIndex,
                Expression.NewArrayBounds(
                    Type,
                    Expression.Constant(1)
                ),
                Expression.Constant(0)
            );
            this.ArrayUnary(Type, ExpressionType.ArrayLength, 結果Array);
            this.ArrayBinary(Type, ExpressionType.Assign, Parameter, Constant);
            this.Array共通(
                Expression.Block(Constant)
            );
            this.Array共通(
                Expression.Call(
                    typeof(沢山の組み合わせ).GetMethod(nameof(Call), BindingFlags.Static | BindingFlags.NonPublic)!.MakeGenericMethod(Type),
                    Constant
                )
            );
            this.ArrayBinary(Type, ExpressionType.Coalesce, Constant, Constant);
            this.Array共通(
                Expression.Condition(
                    Expression.Constant(true),
                    Constant,
                    Constant
                )
            );
            this.Array共通(
                Expression.Convert(
                    Expression.Constant(1d),
                    Type
                )
            );
            this.Array共通(
                Expression.ConvertChecked(
                    Expression.Constant(1d),
                    Type
                )
            );
            this.Array共通(
                Expression.Block(
                    Expression.DebugInfo(
                        Expression.SymbolDocument("ファイル名"),
                        1,
                        1,
                        2,
                        1
                    ),
                    Constant
                )
            );
            this.ArrayUnary(Type, ExpressionType.Decrement, Constant);
            this.Array共通(
                Expression.Default(Type)
            );
            this.ArrayBinary(Type, ExpressionType.Divide, Constant, Constant);
            this.ArrayBinary(Type, ExpressionType.DivideAssign, Parameter, Constant);
            this.ArrayBinary(Type, ExpressionType.Equal, Constant, Constant);
            this.ArrayBinary(Type, ExpressionType.ExclusiveOr, Constant, Constant);
            this.ArrayBinary(Type, ExpressionType.ExclusiveOrAssign, Parameter, Constant);
            this.Array共通(
                Expression.Block(
                    Expression.Goto(
                        GotoTarget,
                        Constant
                    ),
                    Expression.Label(
                        GotoTarget,
                        Constant
                    )
                )
            );
            this.ArrayBinary(Type, ExpressionType.GreaterThan, Constant, Constant);
            this.ArrayBinary(Type, ExpressionType.GreaterThanOrEqual, Constant, Constant);
            this.ArrayUnary(Type, ExpressionType.Increment, Constant);
            this.Array共通(
                Expression.ArrayAccess(
                    Expression.NewArrayInit(
                        typeof(int),
                        Expression.Constant(1),
                        Expression.Constant(2),
                        Expression.Constant(3)
                    ),
                    Expression.Constant(1)
                )
            );
            this.Array共通(
                Expression.Invoke(
                    Expression.Constant(
                        (Func<int>)(() => 1)
                    )
                )
            );
            this.Array共通(
                Expression.Label(
                    Expression.Label(typeof(int)),
                    Constant
                )
            );
            this.ArrayBinary(Type,ExpressionType.LeftShift,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.LeftShiftAssign,Parameter,Constant);
            this.ArrayBinary(Type,ExpressionType.LessThan,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.LessThanOrEqual,Constant,Constant);
            this.Array共通(
                Expression.Loop(
                    Expression.Break(LoopBreak,Constant),
                    LoopBreak
                )
            );
            this.Array共通(
                Expression.Property(
                    Expression.New(typeof(Point)),
                    "X"
                )
            );
            this.ArrayBinary(Type,ExpressionType.Modulo,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.ModuloAssign,Parameter,Constant);
            this.ArrayBinary(Type,ExpressionType.Multiply,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.MultiplyAssign,Parameter,Constant);
            this.ArrayBinary(Type,ExpressionType.MultiplyAssignChecked,Parameter,Constant);
            this.ArrayBinary(Type,ExpressionType.MultiplyChecked,Constant,Constant);
            this.ArrayUnary(Type,ExpressionType.Negate,Constant);
            this.ArrayUnary(Type,ExpressionType.NegateChecked,Constant);
            this.ArrayUnary(Type,ExpressionType.Not,Constant);
            this.ArrayBinary(Type,ExpressionType.NotEqual,Constant,Constant);
            this.ArrayUnary(Type,ExpressionType.OnesComplement,Constant);
            this.ArrayBinary(Type,ExpressionType.Or,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.OrAssign,Parameter,Constant);
            this.ArrayBinary(Type,ExpressionType.OrElse,Constant,Constant);
            this.Array共通(
                Constant
            );
            this.ArrayUnary(Type,ExpressionType.PostDecrementAssign,Parameter);
            this.ArrayUnary(Type,ExpressionType.PostIncrementAssign,Parameter);
            this.ArrayBinary(Type,ExpressionType.Power,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.PowerAssign,Parameter,Constant);
            this.ArrayUnary(Type,ExpressionType.PreDecrementAssign,Parameter);
            this.ArrayUnary(Type,ExpressionType.PreIncrementAssign,Parameter);
            this.ArrayUnary(Type,
                ExpressionType.Quote,
                Expression.Lambda(
                    typeof(Func<>).MakeGenericType(Type),
                    Constant
                )
            );
            this.ArrayBinary(Type,ExpressionType.RightShift,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.RightShiftAssign,Parameter,Constant);
            this.ArrayBinary(Type,ExpressionType.Subtract,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.SubtractAssign,Parameter,Constant);
            this.ArrayBinary(Type,ExpressionType.SubtractAssignChecked,Parameter,Constant);
            this.ArrayBinary(Type,ExpressionType.SubtractChecked,Constant,Constant);
            this.Array共通(
                Expression.Switch(
                    Constant,
                    Expression.Constant(-1),
                    Expression.SwitchCase(
                        Expression.Constant(10),
                        Expression.Constant(11)
                    ),
                    Expression.SwitchCase(
                        Expression.Constant(20),
                        Expression.Constant(21),
                        Expression.Constant(22)
                    ),
                    Expression.SwitchCase(
                        Expression.Constant(30),
                        Expression.Constant(31),
                        Expression.Constant(32),
                        Expression.Constant(33)
                    )
                )
            );
            this.Array共通(
                Expression.TryCatch(
                    Expression.Condition(
                        Expression.Constant(true),
                        Expression.Throw(
                            Expression.New(typeof(Exception)),
                            Constant.Type
                        ),
                        Constant
                    ),
                    Expression.Catch(
                        typeof(Exception),
                        Constant
                    )
                )
            );
            this.Array共通(
                Expression.TryCatch(
                    Expression.Condition(
                        Expression.Constant(true),
                        Expression.Throw(
                            Expression.New(typeof(Exception)),
                            Constant.Type
                        ),
                        Constant
                    ),
                    Expression.Catch(
                        typeof(Exception),
                        Constant
                    )
                )
            );
            this.Array共通(
                Expression.UnaryPlus(
                    Constant
                )
            );
            this.Array共通(
                Expression.Unbox(
                    Expression.Convert(
                        Constant,
                        typeof(object)
                    ),
                    Type
                )
            );
            ListExpression.Insert(
                0,
                Expression.Assign(
                    結果Array,
                    Expression.Constant(new int[ListExpression.Count])
                )
            );
            ListExpression.Add(結果Array);
            return Expression.Block(
                new[] { 結果Array, Parameter },
                ListExpression
            );
        }
    }
    private Expression ArrayにString代入()
    {
        var ListExpression = this.ListExpression;
        {
            const string 値 = "1";
            var Type = 値.GetType();
            this.Arrayの添字 = 0;
            結果Array=Expression.Parameter(Type.MakeArrayType(),"結果Array");
            GotoTarget=Expression.Label(Type);
            LoopBreak=Expression.Label(Type);
            var Parameter = Expression.Parameter(Type);
            var Constant = Expression.Constant(値);
            ListExpression.Clear();
            ListExpression.Add(Expression.Assign(Parameter,Expression.Default(Type)));
            this.ArrayBinary(Type,ExpressionType.Add,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.AddAssign,Parameter,Constant);
            this.ArrayBinary(Type,ExpressionType.AddAssignChecked,Parameter,Constant);
            this.ArrayBinary(Type,ExpressionType.AddChecked,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.And,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.AndAlso,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.AndAssign,Parameter,Constant);
            this.ArrayBinary(
                Type,
                ExpressionType.ArrayIndex,
                Expression.NewArrayBounds(
                    Type,
                    Expression.Constant(1)
                ),
                Expression.Constant(0)
            );
            this.ArrayUnary(Type,ExpressionType.ArrayLength,結果Array);
            this.ArrayBinary(Type,ExpressionType.Assign,Parameter,Constant);
            this.Array共通(
                Expression.Block(Constant)
            );
            this.Array共通(
                Expression.Call(
                    typeof(沢山の組み合わせ).GetMethod(nameof(Call),BindingFlags.Static|BindingFlags.NonPublic)!.MakeGenericMethod(Type),
                    Constant
                )
            );
            this.ArrayBinary(Type,ExpressionType.Coalesce,Constant,Constant);
            this.Array共通(
                Expression.Condition(
                    Expression.Constant(true),
                    Constant,
                    Constant
                )
            );
            this.Array共通(
                Expression.Convert(
                    Expression.Convert(
                        Expression.Constant("ABC"),
                        typeof(object)
                    ),
                    Type
                )
            );
            this.Array共通(
                Expression.ConvertChecked(
                    Expression.ConvertChecked(
                        Expression.Constant("ABC"),
                        typeof(object)
                    ),
                    Type
                )
            );
            this.Array共通(
                Expression.Block(
                    Expression.DebugInfo(
                        Expression.SymbolDocument("ファイル名"),
                        1,
                        1,
                        2,
                        1
                    ),
                    Constant
                )
            );
            this.ArrayUnary(Type,ExpressionType.Decrement,Constant);
            this.Array共通(
                Expression.Default(Type)
            );
            this.ArrayBinary(Type,ExpressionType.Divide,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.DivideAssign,Parameter,Constant);
            this.ArrayBinary(Type,ExpressionType.Equal,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.ExclusiveOr,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.ExclusiveOrAssign,Parameter,Constant);
            this.Array共通(
                Expression.Block(
                    Expression.Goto(
                        GotoTarget,
                        Constant
                    ),
                    Expression.Label(
                        GotoTarget,
                        Constant
                    )
                )
            );
            this.ArrayBinary(Type,ExpressionType.GreaterThan,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.GreaterThanOrEqual,Constant,Constant);
            this.ArrayUnary(Type,ExpressionType.Increment,Constant);
            this.Array共通(
                Expression.ArrayAccess(
                    Expression.NewArrayInit(
                        Type,
                        Constant
                    ),
                    Expression.Constant(0)
                )
            );
            this.Array共通(
                Expression.Invoke(
                    Expression.Constant(
                        (Func<string>)(() => "A")
                    )
                )
            );
            this.Array共通(
                Expression.Label(
                    Expression.Label(Type),
                    Constant
                )
            );
            this.ArrayBinary(Type,ExpressionType.LeftShift,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.LeftShiftAssign,Parameter,Constant);
            this.ArrayBinary(Type,ExpressionType.LessThan,Constant,Constant);
            this.ArrayBinary(Type,ExpressionType.LessThanOrEqual,Constant,Constant);
            this.Array共通(
                Expression.Loop(
                    Expression.Break(LoopBreak, Constant),
                    LoopBreak
                )
            );
            this.Array共通(
                Expression.Field(
                    Expression.New(typeof(GenericStruct<>).MakeGenericType(Type)),
                    nameof(GenericStruct<int>.Field)
                )
            );
            this.ArrayBinary(Type, ExpressionType.Modulo, Constant, Constant);
            this.ArrayBinary(Type, ExpressionType.ModuloAssign, Parameter, Constant);
            this.ArrayBinary(Type, ExpressionType.Multiply, Constant, Constant);
            this.ArrayBinary(Type, ExpressionType.MultiplyAssign, Parameter, Constant);
            this.ArrayBinary(Type, ExpressionType.MultiplyAssignChecked, Parameter, Constant);
            this.ArrayBinary(Type, ExpressionType.MultiplyChecked, Constant, Constant);
            this.ArrayUnary(Type, ExpressionType.Negate, Constant);
            this.ArrayUnary(Type, ExpressionType.NegateChecked, Constant);
            this.ArrayUnary(Type, ExpressionType.Not, Constant);
            this.ArrayBinary(Type, ExpressionType.NotEqual, Constant, Constant);
            this.ArrayUnary(Type, ExpressionType.OnesComplement, Constant);
            this.ArrayBinary(Type, ExpressionType.Or, Constant, Constant);
            this.ArrayBinary(Type, ExpressionType.OrAssign, Parameter, Constant);
            this.ArrayBinary(Type, ExpressionType.OrElse, Constant, Constant);
            this.Array共通(
                Constant
            );
            this.ArrayUnary(Type, ExpressionType.PostDecrementAssign, Parameter);
            this.ArrayUnary(Type, ExpressionType.PostIncrementAssign, Parameter);
            this.ArrayBinary(Type, ExpressionType.Power, Constant, Constant);
            this.ArrayBinary(Type, ExpressionType.PowerAssign, Parameter, Constant);
            this.ArrayUnary(Type, ExpressionType.PreDecrementAssign, Parameter);
            this.ArrayUnary(Type, ExpressionType.PreIncrementAssign, Parameter);
            this.ArrayUnary(Type,
                ExpressionType.Quote,
                Expression.Lambda(
                    typeof(Func<>).MakeGenericType(Type),
                    Constant
                )
            );
            this.ArrayBinary(Type, ExpressionType.RightShift, Constant, Constant);
            this.ArrayBinary(Type, ExpressionType.RightShiftAssign, Parameter, Constant);
            this.ArrayBinary(Type, ExpressionType.Subtract, Constant, Constant);
            this.ArrayBinary(Type, ExpressionType.SubtractAssign, Parameter, Constant);
            this.ArrayBinary(Type, ExpressionType.SubtractAssignChecked, Parameter, Constant);
            this.ArrayBinary(Type, ExpressionType.SubtractChecked, Constant, Constant);
            this.Array共通(
                Expression.Switch(
                    Expression.Constant(1),
                    Expression.Constant("A"),
                    Expression.SwitchCase(
                        Expression.Constant("B"),
                        Expression.Constant(11)
                    ),
                    Expression.SwitchCase(
                        Expression.Constant("C"),
                        Expression.Constant(21),
                        Expression.Constant(22)
                    ),
                    Expression.SwitchCase(
                        Expression.Constant("D"),
                        Expression.Constant(31),
                        Expression.Constant(32),
                        Expression.Constant(33)
                    )
                )
            );
            this.Array共通(
                Expression.TryCatch(
                    Expression.Condition(
                        Expression.Constant(true),
                        Expression.Throw(
                            Expression.New(typeof(Exception)),
                            Constant.Type
                        ),
                        Constant
                    ),
                    Expression.Catch(
                        typeof(Exception),
                        Constant
                    )
                )
            );
            this.Array共通(
                Expression.TryCatch(
                    Expression.Condition(
                        Expression.Constant(true),
                        Expression.Throw(
                            Expression.New(typeof(Exception)),
                            Constant.Type
                        ),
                        Constant
                    ),
                    Expression.Catch(
                        typeof(Exception),
                        Constant
                    )
                )
            );
            ListExpression.Insert(
                0,
                Expression.Assign(
                    結果Array,
                    Expression.NewArrayBounds(Type, Expression.Constant(ListExpression.Count))
                )
            );
            ListExpression.Add(結果Array);
            return Expression.Block(
                new[] { 結果Array, Parameter },
                ListExpression
            );
        }
    }
    [TestMethod]
    public void Arrayに代入()
    {
        this.ArrayにString代入();
        this.ArrayにBoolean代入();
        this.ArrayにInt32代入();
    }
    private void Parameter共通(Expression Expression)
    {
        var 計算結果 = Expression.Parameter(Expression.Type, "計算結果");
        var ReturnExpression = Expression.Assign(
            計算結果,
            Expression
        );
        var Lambda = Expression.Lambda(
            Expression.NodeType == ExpressionType.Parameter ? typeof(Func<,>).MakeGenericType(Expression.Type, Expression.Type) : typeof(Func<>).MakeGenericType(Expression.Type),
            Expression.Block(
                new[] { 計算結果 },
                ReturnExpression
            )
        );
        this.CreateDelegate(Lambda);
    }
    private void ParameterBinary(ExpressionType NodeType, Expression オペランド1, Expression オペランド2)
    {
        try
        {
            var Binary = Expression.MakeBinary(
                NodeType,
                オペランド1,
                オペランド2
            );
            var Type = Binary.Type;
            var 計算結果 = Expression.Parameter(Type, "計算結果");
            var ReturnExpression = Expression.Assign(
                計算結果,
                Binary
            );
            var Lambda = Expression.Lambda(
                typeof(Func<>).MakeGenericType(Type),
                Expression.Block(
                    new[] { 計算結果 },
                    ReturnExpression
                )
            );
            var Delegate = this.CreateDelegate(Lambda);
            var result = Delegate.DynamicInvoke();
        }
        catch (InvalidOperationException) { }
    }
    private void ParameterBinaryAssign(ExpressionType NodeType, ParameterExpression オペランド1, Expression オペランド2)
    {
        try
        {
            var ReturnExpression = Expression.Assign(
                オペランド1,
                Expression.MakeBinary(
                    NodeType,
                    オペランド1,
                    オペランド2
                )
            );
            var Lambda = Expression.Lambda(
                typeof(Func<,>).MakeGenericType(オペランド1.Type, ReturnExpression.Type),
                ReturnExpression,
                オペランド1
            );
            var Delegate = this.CreateDelegate(Lambda);
            var result = Delegate.DynamicInvoke(Activator.CreateInstance(オペランド1.Type));
        }
        catch (InvalidOperationException)
        {

        }
    }
    private void ParameterUnary(ExpressionType NodeType, Expression オペランド1)
    {
        try
        {
            this.Parameter共通(
                Expression.MakeUnary(
                    NodeType,
                    オペランド1,
                    null
                )
            );
        }
        catch (InvalidOperationException)
        {

        }
    }
    private void ParameterUnaryAssign(ExpressionType NodeType, ParameterExpression オペランド1)
    {
        try
        {
            var ReturnExpression = Expression.Assign(
                オペランド1,
                Expression.MakeUnary(
                    NodeType,
                    オペランド1,
                    null
                )
            );
            var Lambda = Expression.Lambda(
                typeof(Func<,>).MakeGenericType(オペランド1.Type, ReturnExpression.Type),
                ReturnExpression,
                オペランド1
            );
            var Delegate = this.CreateDelegate(Lambda);
            var result = Delegate.DynamicInvoke(Activator.CreateInstance(オペランド1.Type));
        }
        catch (InvalidOperationException)
        {

        }
    }
    [TestMethod]
    public void Parameterに代入()
    {
        /*
         * 必要なデータ型、例えばInt32あ欲しいならDictionary<Int32,Expression>
         */
        //var Array=Expression.Parameter(typeof(Int32[]),"Array");
        //var Int32=Expression.Parameter(typeof(Int32),"Int32");
        //var List=Expression.Parameter(typeof(List<Int32>),"List");
        //var NullableInt32=Expression.Parameter(typeof(Int32?),"NullableInt32");
        //var Boolean=Expression.Parameter(typeof(Boolean),"Boolean");
        var Constant0 = Expression.Constant(0);
        //var Constant1=Expression.Constant(0);
        //var Constant1111=Expression.Constant(1111);
        //var Constant1d=Expression.Constant(1d);
        //var Double=Expression.Parameter(typeof(Double),"Double");
        //var Constant_false=Expression.Constant(false);
        //var Constant_true=Expression.Constant(true);
        //var GotoTarget=Expression.Label(typeof(Int32));
        //var LoopBreak=Expression.Label(typeof(Int32));
        foreach (var 値 in new object[] { true, 1, (double)1, (decimal)1 })
        {
            var Type = 値.GetType();
            GotoTarget = Expression.Label(Type);
            LoopBreak = Expression.Label(Type);
            Parameter = Expression.Parameter(Type, "引数");
            var Constant = Expression.Constant(値);
            this.ParameterBinary(ExpressionType.Add, Constant, Constant);
            this.ParameterBinaryAssign(ExpressionType.AddAssign, Parameter, Constant);
            this.ParameterBinaryAssign(ExpressionType.AddAssignChecked, Parameter, Constant);
            this.ParameterBinaryAssign(ExpressionType.AddChecked, Parameter, Constant);
            this.ParameterBinary(ExpressionType.And, Constant, Constant);
            this.ParameterBinary(ExpressionType.AndAlso, Constant, Constant);
            this.ParameterBinaryAssign(ExpressionType.AndAssign, Parameter, Constant);
            var ConstantArray = Expression.Constant(Activator.CreateInstance(Type.MakeArrayType(), 2));
            this.ParameterBinary(ExpressionType.ArrayIndex, ConstantArray, Constant0);
            this.ParameterUnary(ExpressionType.ArrayLength, ConstantArray);
            {
                var ReturnExpression = Expression.Assign(
                    Parameter,
                    Constant
                );
                var Lambda = Expression.Lambda(
                    typeof(Func<,>).MakeGenericType(Type, ReturnExpression.Type),
                    ReturnExpression,
                    Parameter
                );
                var Delegate = this.CreateDelegate(Lambda);
                var result = Delegate.DynamicInvoke(Activator.CreateInstance(Type));
            }
            this.Parameter共通(Expression.Block(Constant));
            this.Parameter共通(
                Expression.Call(
                    typeof(沢山の組み合わせ).GetMethod(nameof(Call), BindingFlags.Static | BindingFlags.NonPublic)!.MakeGenericMethod(Type),
                    Constant
                )
            );
            this.ParameterBinary(ExpressionType.Coalesce, ConstantObject, ConstantArray);
            this.Parameter共通(
                Expression.Condition(
                    Constant_false,
                    Constant_false,
                    Constant_true
                )
            );
            this.Parameter共通(
                Expression.Convert(
                    Constant,
                    typeof(double)
                )
            );
            this.Parameter共通(
                Expression.ConvertChecked(
                    Constant,
                    typeof(double)
                )
            );
            this.Parameter共通(
                Expression.Block(
                    Expression.DebugInfo(
                        Expression.SymbolDocument("ファイル名"),
                        1,
                        1,
                        2,
                        1
                    ),
                    Constant
                )
            );
            this.ParameterUnary(ExpressionType.Decrement, Constant);
            this.Parameter共通(
                Expression.Default(typeof(object))
            );
            this.ParameterBinary(ExpressionType.Divide, Constant, Constant);
            this.ParameterBinaryAssign(ExpressionType.DivideAssign, Parameter, Constant);
            this.Parameter共通(
                Expression.Dynamic(
                    Binder.GetIndex(
                        CSharpBinderFlags.None,
                        typeof(沢山の組み合わせ),
                        new[] {
                            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null)
                        }
                    ),
                    typeof(object),
                    ConstantArray,
                    Constant0
                )
            );
            this.ParameterBinary(ExpressionType.Equal, Constant, Constant);
            this.ParameterBinary(ExpressionType.ExclusiveOr, Constant, Constant);
            this.ParameterBinaryAssign(ExpressionType.ExclusiveOrAssign, Parameter, Constant);
            this.Parameter共通(
                Expression.Block(
                    Expression.Goto(
                        GotoTarget,
                        Constant
                    ),
                    Expression.Label(
                        GotoTarget,
                        Constant
                    )
                )
            );
            this.ParameterBinary(ExpressionType.GreaterThan, Constant, Constant);
            this.ParameterBinary(ExpressionType.GreaterThanOrEqual, Constant, Constant);
            this.ParameterUnary(ExpressionType.Increment, Constant);
            this.Parameter共通(
                Expression.ArrayAccess(
                    ConstantArray,
                    Constant0
                )
            );
            this.Parameter共通(
                Expression.Invoke(
                    Expression.Constant(
                        (Func<object>)(() => 1)
                    )
                )
            );
            this.Parameter共通(
                Expression.IsFalse(
                    Expression.Constant(
                        new class_演算子オーバーロード(1, true)
                    )
                )
            );
            this.Parameter共通(
                Expression.IsTrue(
                    Expression.Constant(
                        new class_演算子オーバーロード(1, true)
                    )
                )
            );
            this.Parameter共通(
                Expression.Label(
                    Expression.Label(Type),
                    Constant
                )
            );
            this.Parameter共通(
                Expression.Lambda(
                    typeof(Func<>).MakeGenericType(Type),
                    Constant
                )
            );
            this.ParameterBinary(ExpressionType.LeftShift, Constant, Constant);
            this.ParameterBinaryAssign(ExpressionType.LeftShiftAssign, Parameter, Constant);
            this.ParameterBinary(ExpressionType.LessThan, Constant, Constant);
            this.ParameterBinary(ExpressionType.LessThanOrEqual, Constant, Constant);
            this.Parameter共通(
                Expression.ListInit(
                    Expression.New(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        typeof(List<>).MakeGenericType(Type).GetConstructor(Type.EmptyTypes)
                    ),
                    Expression.ElementInit(
                        typeof(List<>).MakeGenericType(Type).GetMethod("Add"),
                        Constant
                    )
                )
            );
            this.Parameter共通(
                Expression.Loop(
                    Expression.Break(LoopBreak, Constant),
                    LoopBreak
                )
            );
            this.Parameter共通(
                Expression.Property(
                    Expression.New(typeof(Point)),
                    "X"
                )
            );
            var GenericStruct = typeof(GenericStruct<>).MakeGenericType(Type);
            this.Parameter共通(
                Expression.MemberInit(
                    Expression.New(GenericStruct),
                    Expression.Bind(
                        GenericStruct.GetField(nameof(GenericStruct<object>.Field)),
                        Constant
                    )
                )
            );
            this.ParameterBinary(ExpressionType.Modulo, Constant, Constant);
            this.ParameterBinaryAssign(ExpressionType.ModuloAssign, Parameter, Constant);
            this.ParameterBinary(ExpressionType.Multiply, Constant, Constant);
            this.ParameterBinaryAssign(ExpressionType.MultiplyAssign, Parameter, Constant);
            this.ParameterBinaryAssign(ExpressionType.MultiplyAssignChecked, Parameter, Constant);
            this.ParameterBinaryAssign(ExpressionType.MultiplyChecked, Parameter, Constant);
            this.ParameterUnary(ExpressionType.Negate, Constant);
            this.ParameterUnary(ExpressionType.NegateChecked, Constant);
            this.Parameter共通(
                Expression.New(GenericStruct)
            );
            this.Parameter共通(
                Expression.NewArrayBounds(
                    Type,
                    this.Typeに対応したExpression(typeof(int))
                )
            );
            this.Parameter共通(
                Expression.NewArrayInit(
                    Type,
                    Constant,
                    Constant
                )
            );
            this.ParameterUnary(ExpressionType.Not, Constant);
            this.ParameterBinary(ExpressionType.NotEqual, Constant, Constant);
            this.ParameterUnary(ExpressionType.OnesComplement, Constant);
            this.ParameterBinary(ExpressionType.Or, Constant, Constant);
            this.ParameterBinaryAssign(ExpressionType.OrAssign, Parameter, Constant);
            this.ParameterBinary(ExpressionType.OrElse, Constant_false, Constant_false);
            this.Parameter共通(
                Constant
            );
            this.ParameterUnaryAssign(ExpressionType.PostDecrementAssign, Parameter);
            this.ParameterUnaryAssign(ExpressionType.PostIncrementAssign, Parameter);
            this.ParameterBinary(ExpressionType.Power, Constant1d, Constant1d);
            this.ParameterBinaryAssign(ExpressionType.PowerAssign, ParameterDouble, Constant1d);
            this.ParameterUnaryAssign(ExpressionType.PreDecrementAssign, Parameter);
            this.ParameterUnaryAssign(ExpressionType.PreIncrementAssign, Parameter);
            this.ParameterUnary(
                ExpressionType.Quote,
                Expression.Lambda(
                    typeof(Func<>).MakeGenericType(Type),
                    Constant
                )
            );
            this.ParameterBinary(ExpressionType.RightShift, Constant, Constant);
            this.ParameterBinaryAssign(ExpressionType.RightShiftAssign, Parameter, Constant);
            this.ParameterBinary(ExpressionType.Subtract, Constant, Constant);
            this.ParameterBinaryAssign(ExpressionType.SubtractAssign, Parameter, Constant);
            this.ParameterBinaryAssign(ExpressionType.SubtractAssignChecked, Parameter, Constant);
            this.ParameterBinary(ExpressionType.SubtractChecked, Constant, Constant);
            try
            {
                this.Parameter共通(
                    Expression.Switch(
                        Constant,
                        Expression.Constant(-1),
                        Expression.SwitchCase(
                            Expression.Constant(10),
                            Expression.Constant(11)
                        ),
                        Expression.SwitchCase(
                            Expression.Constant(20),
                            Expression.Constant(21),
                            Expression.Constant(22)
                        ),
                        Expression.SwitchCase(
                            Expression.Constant(30),
                            Expression.Constant(31),
                            Expression.Constant(32),
                            Expression.Constant(33)
                        )
                    )
                );
            }
            catch (InvalidOperationException)
            {

            }
            this.Parameter共通(
                Expression.Block(
                    new[] { ParameterBoolean },
                    Expression.TryCatch(
                        Expression.Condition(
                            ParameterBoolean,
                            Expression.Throw(
                                Expression.New(typeof(Exception)),
                                typeof(bool)
                            ),
                            ParameterBoolean
                        ),
                        Expression.Catch(
                            typeof(Exception),
                            ParameterBoolean
                        )
                    )
                )
            );
            this.Parameter共通(
                Expression.Block(
                    new[] { ParameterBoolean },
                    Expression.TryCatch(
                        Expression.Condition(
                            ParameterBoolean,
                            Expression.Throw(
                                Expression.New(typeof(Exception)),
                                typeof(bool)
                            ),
                            ParameterBoolean
                        ),
                        Expression.Catch(
                            typeof(Exception),
                            ParameterBoolean
                        )
                    )
                )
            );
            this.Parameter共通(
                Expression.TypeAs(
                    Expression.Constant("ABC"),
                    typeof(object)
                )
            );
            this.Parameter共通(
                Expression.TypeEqual(
                    Expression.Constant("ABC"),
                    typeof(object)
                )
            );
            this.Parameter共通(
                Expression.TypeIs(
                    Expression.Constant("ABC"),
                    typeof(object)
                )
            );
            try
            {
                this.Parameter共通(
                    Expression.UnaryPlus(
                        Constant
                    )
                );
            }
            catch (InvalidOperationException)
            {

            }
            this.Parameter共通(
                Expression.Unbox(
                    Expression.Convert(
                        Constant,
                        typeof(object)
                    ),
                    Type
                )
            );
        }
    }
    private readonly Random r = new(1);
    //        private Expression Arrayに対応したExpression(Type Type){
    //            //Int
    //            var NodeType=(ExpressionType)r.Next((Int32)ExpressionType.IsFalse+1);
    //            switch(NodeType){
    //                case ExpressionType.ArrayIndex           :
    //                    return Expression.ArrayIndex(
    //                        this.Arrayに対応したExpression(Type.MakeArrayType()),
    //                        this.Typeに対応したExpression(typeof(Int32))
    //                    );
    //                case ExpressionType.Assign               :{
    //                    var Parameter=Expression.Parameter(typeof(Int32[]));
    //                    return Expression.Block(
    //                        new[] {Parameter},
    //                        Expression.MakeBinary(
    //                            NodeType,
    //                            Parameter,
    //                            this.Arrayに対応したExpression(Type)
    //                        )
    //                    );
    //                }
    //                case ExpressionType.Block                :
    //                    return Expression.Block(
    //                        this.Arrayに対応したExpression(Type)
    //                    );
    //                case ExpressionType.Conditional          :
    //                    return Expression.Condition(
    //                        Expression.Constant(true),
    //                        this.Arrayに対応したExpression(Type),
    //                        this.Arrayに対応したExpression(Type)
    //                    );
    //                case ExpressionType.Constant             :
    //                    return Expression.Constant(new Int32[1]);
    //                case ExpressionType.Default              :
    //                    return Expression.Default(typeof(Int32[]));
    //                case ExpressionType.Index                :
    //                    return Expression.ArrayIndex(
    //                        Expression.NewArrayInit(
    //                            typeof(Int32[]),
    //                            Expression.NewArrayBounds(
    //                                typeof(Int32),
    //                                Expression.Constant(1)
    //                            )
    //                        ),
    //                        Expression.Constant(0)
    //                    );
    //                case ExpressionType.Invoke               :
    //                    return Expression.Invoke(
    //                        Expression.Lambda<Func<Int32[]>>(
    //                            Expression.Constant(new Int32[1])
    //                        )
    //                    );
    //                case ExpressionType.Call                 :
    //                    return Expression.Call(
    //                        typeof(沢山の組み合わせ).GetMethod(nameof(GenericCall),BindingFlags.Static|BindingFlags.NonPublic).MakeGenericMethod(typeof(Int32[])),
    //                        Expression.Constant(new Int32[1])
    //                    );
    //                case ExpressionType.Parameter:{
    //                    var Parameter=Expression.Parameter(typeof(Int32),"Parameter");
    //                    return Expression.Block(
    //                        new[] {Parameter},
    //                        Expression.Assign(Parameter,Expression.Constant(new Int32[1])),
    //                        Parameter
    //                    );
    //                }
    //                case ExpressionType.NewArrayBounds       :
    //                    return Expression.NewArrayBounds(
    //                        typeof(Int32),
    //                        this.Typeに対応したExpression(typeof(Int32))
    //                    );
    //                case ExpressionType.NewArrayInit         :
    //                    return Expression.NewArrayInit(
    //                        typeof(Int32),
    //                        this.Typeに対応したExpression(typeof(Int32))
    //                    );
    //                case ExpressionType.Convert              :
    //                case ExpressionType.ConvertChecked       :
    //                    return Expression.Convert(
    //                        Expression.Convert(
    //                            Expression.Constant(new Int32[1]),
    //                            typeof(Object)
    //                        ),
    //                        typeof(Int32[])
    //                    );
    //                case ExpressionType.Label:{
    //                    var Break=Expression.Label(typeof(Int32[]));
    //                    return Expression.Block(
    //                        Expression.Break(
    //                            Break,
    //                            Expression.Constant(new Int32[1])
    //                        ),
    //                        Expression.Label(Break)
    //                    );
    //                }
    //                case ExpressionType.Loop:{
    //                    var Break=Expression.Label(typeof(Int32[]));
    //                    return Expression.Loop(
    //                        Expression.Break(
    //                            Break,
    //                            Expression.Constant(new Int32[1])
    //                        ),
    //                        Break
    //                    );
    //                }
    //                case ExpressionType.Switch: { 
    //                    return Expression.Switch(
    //                        this.Typeに対応したExpression(typeof(Int32)),
    //                        this.Arrayに対応したExpression(Type),
    //                        Expression.SwitchCase(
    //                            this.Arrayに対応したExpression(Type),
    //                            this.Arrayに対応したExpression(Type)
    //                        )
    //                    );
    //                }
    //                case ExpressionType.Add                  :
    //                case ExpressionType.AddAssign            :
    //                case ExpressionType.AddAssignChecked     :
    //                case ExpressionType.AddChecked           :
    //                case ExpressionType.And                  :
    //                case ExpressionType.AndAssign            :
    //                case ExpressionType.AndAlso              :
    //                case ExpressionType.Coalesce             :
    //                case ExpressionType.Divide               :
    //                case ExpressionType.DivideAssign         :
    //                case ExpressionType.Equal                :
    //                case ExpressionType.ExclusiveOr          :
    //                case ExpressionType.ExclusiveOrAssign    :
    //                case ExpressionType.GreaterThan          :
    //                case ExpressionType.GreaterThanOrEqual   :
    //                case ExpressionType.LeftShift            :
    //                case ExpressionType.LeftShiftAssign      :
    //                case ExpressionType.LessThan             :
    //                case ExpressionType.LessThanOrEqual      :
    //                case ExpressionType.Modulo               :
    //                case ExpressionType.ModuloAssign         :
    //                case ExpressionType.Multiply             :
    //                case ExpressionType.MultiplyAssign       :
    //                case ExpressionType.MultiplyAssignChecked:
    //                case ExpressionType.MultiplyChecked      :
    //                case ExpressionType.NotEqual             :
    //                case ExpressionType.Or                   :
    //                case ExpressionType.OrAssign             :
    //                case ExpressionType.OrElse               : 
    //                case ExpressionType.Power                :
    //                case ExpressionType.PowerAssign          :
    //                case ExpressionType.RightShift           :
    //                case ExpressionType.RightShiftAssign     :
    //                case ExpressionType.Subtract             :
    //                case ExpressionType.SubtractAssign       :
    //                case ExpressionType.SubtractAssignChecked:
    //                case ExpressionType.SubtractChecked      :
    //                case ExpressionType.DebugInfo            :
    //                case ExpressionType.Dynamic              :
    //                case ExpressionType.Goto                 :
    //                case ExpressionType.Lambda               :
    //                case ExpressionType.ListInit             :
    //                case ExpressionType.MemberAccess         :
    //                case ExpressionType.MemberInit           :
    //                case ExpressionType.New                  :
    //                case ExpressionType.RuntimeVariables     :
    //                case ExpressionType.Try                  :
    //                case ExpressionType.TypeEqual            :
    //                case ExpressionType.TypeIs               : 
    //                case ExpressionType.ArrayLength          :
    //                case ExpressionType.Decrement            :
    //                case ExpressionType.Increment            :
    ////                case ExpressionType.IsFalse             );
    //                case ExpressionType.IsTrue               :
    //                case ExpressionType.Negate               :
    //                case ExpressionType.NegateChecked        :
    //                case ExpressionType.Not                  :
    //                case ExpressionType.OnesComplement       :
    //                case ExpressionType.PostDecrementAssign  :
    //                case ExpressionType.PostIncrementAssign  :
    //                case ExpressionType.PreDecrementAssign   :
    //                case ExpressionType.PreIncrementAssign   :
    //                case ExpressionType.Quote                :
    //                case ExpressionType.Throw                :
    //                case ExpressionType.TypeAs               :
    //                case ExpressionType.UnaryPlus            :
    //                case ExpressionType.Unbox                :
    //                default:
    //                    return Expression.Constant(new Int32[10]);
    //            }
    //        }
    private static T GenericCall<T>(T input) => input;
    //        private Int32 Int32に対応したExpressionCount=0;
    //        private Expression Typeに対応したExpression(typeof(Int32)){
    //            //Int
    //            var NodeType=(ExpressionType)(Int32に対応したExpressionCount++);
    //            switch(NodeType){
    //                case ExpressionType.Add:
    //                case ExpressionType.AddChecked           :
    //                case ExpressionType.And                  :
    //                case ExpressionType.Divide               :
    //                case ExpressionType.ExclusiveOr          :
    //                case ExpressionType.LeftShift            :
    //                case ExpressionType.Modulo               :
    //                case ExpressionType.Multiply             :
    //                case ExpressionType.MultiplyChecked      :
    //                case ExpressionType.Or                   :
    //                case ExpressionType.RightShift           :
    //                case ExpressionType.Subtract             :
    //                case ExpressionType.SubtractChecked      :
    //                    return Expression.MakeBinary(
    //                        NodeType,
    //                        this.Typeに対応したExpression(typeof(Int32)),
    //                        this.Typeに対応したExpression(typeof(Int32))
    //                    );

    //                case ExpressionType.AddAssign            :
    //                case ExpressionType.AddAssignChecked     :
    //                case ExpressionType.AndAssign            :
    //                case ExpressionType.Assign               :
    //                case ExpressionType.DivideAssign         :
    //                case ExpressionType.LeftShiftAssign      :
    //                case ExpressionType.ModuloAssign         :
    //                case ExpressionType.MultiplyAssign       :
    //                case ExpressionType.MultiplyAssignChecked:
    //                case ExpressionType.ExclusiveOrAssign    :
    //                case ExpressionType.RightShiftAssign     :
    //                case ExpressionType.SubtractAssign       :
    //                case ExpressionType.SubtractAssignChecked:
    //                case ExpressionType.OrAssign:{
    //                    var Parameter=Expression.Parameter(typeof(Int32));
    //                    return Expression.Block(
    //                        new[] {Parameter},
    //                        Expression.Assign(Parameter,Expression.Constant(0)),
    //                        Expression.MakeBinary(
    //                            NodeType,
    //                            Parameter,
    //                            this.Typeに対応したExpression(typeof(Int32))
    //                        )
    //                    );
    //                }
    //                case ExpressionType.ArrayIndex:{
    //                    return Expression.ArrayIndex(
    //                        this.Arrayに対応したExpression(typeof(Int32)),
    //                        Expression.Constant(0)
    //                    );
    //                }
    //                case ExpressionType.Block                :
    //                    return Expression.Block(
    //                        this.Typeに対応したExpression(typeof(Int32))
    //                    );
    //                case ExpressionType.Call                 :
    //                    return Expression.Call(
    //                        typeof(沢山の組み合わせ).GetMethod(nameof(GenericCall),BindingFlags.Static|BindingFlags.NonPublic).MakeGenericMethod(typeof(Int32)),
    //                        Expression.Constant(1)
    //                    );
    //                case ExpressionType.Coalesce             :
    //                    return Expression.Coalesce(
    //                        Expression.Convert(
    //                            this.Typeに対応したExpression(typeof(Int32)),
    //                            typeof(Int32?)
    //                        ),
    //                        this.Typeに対応したExpression(typeof(Int32))
    //                    );
    //                case ExpressionType.Conditional          :
    //                    return Expression.Condition(
    //                        this.Booleanに対応したExpression(),
    //                        this.Typeに対応したExpression(typeof(Int32)),
    //                        Typeに対応したExpression(typeof(Int32))
    //                    );
    //                case ExpressionType.Default              :
    //                    return Expression.Default(typeof(Int32));
    //                case ExpressionType.Index                :
    //                    return Expression.ArrayIndex(
    //                        Expression.NewArrayBounds(
    //                            typeof(Int32),
    //                            this.Typeに対応したExpression(typeof(Int32)),
    //                            this.Typeに対応したExpression(typeof(Int32))
    //                        ),
    //                        this.Typeに対応したExpression(typeof(Int32)),
    //                        this.Typeに対応したExpression(typeof(Int32))
    //                    );
    //                case ExpressionType.Invoke               :
    //                    return Expression.Invoke(
    //                        Expression.Lambda<Func<Int32>>(
    //                            Expression.Constant(2)
    //                        )
    //                    );
    //                case ExpressionType.MemberAccess         :
    //                    return Expression.Property(
    //                        Expression.New(
    //                            typeof(Point)
    //                        ),
    //                        nameof(Point.X)
    //                    );
    //                case ExpressionType.New                  :
    //                    return Expression.New(
    //                        typeof(Int32)
    //                    );
    //                case ExpressionType.Parameter:{
    //                    var Parameter=Expression.Parameter(typeof(Int32),"Parameter");
    //                    return Expression.Block(
    //                        new[] {Parameter},
    //                        Expression.Assign(Parameter,Expression.Constant(0)),
    //                        Parameter
    //                    );
    //                }
    //                case ExpressionType.Switch: { 
    //                    return Expression.Switch(
    //                        this.Typeに対応したExpression(typeof(Int32)),
    //                        this.Typeに対応したExpression(typeof(Int32)),
    //                        Expression.SwitchCase(
    //                            this.Typeに対応したExpression(typeof(Int32)),
    //                            this.Typeに対応したExpression(typeof(Int32))
    //                        )
    //                    );
    //                }
    //                case ExpressionType.ArrayLength          :
    //                    return Expression.ArrayLength(
    //                        this.Arrayに対応したExpression(typeof(Int32))
    //                    );
    //                case ExpressionType.Convert              :
    //                case ExpressionType.ConvertChecked       :
    //                    return Expression.MakeUnary(
    //                        NodeType,
    //                        this.Typeに対応したExpression(typeof(Int32)),
    //                        typeof(Int32)
    //                    );
    //                case ExpressionType.Decrement            :
    //                case ExpressionType.Increment            :
    //                    return Expression.MakeUnary(
    //                        NodeType,
    //                        this.Typeに対応したExpression(typeof(Int32)),
    //                        null
    //                    );
    //                case ExpressionType.Label:{
    //                    var Break=Expression.Label(typeof(Int32));
    //                    return Expression.Loop(
    //                        Expression.Break(
    //                            Break,
    //                            Expression.Constant(1)
    //                        ),
    //                        Break
    //                    );
    //                }
    //                case ExpressionType.Loop:{
    //                    var Break=Expression.Label(typeof(Int32));
    //                    return Expression.Loop(
    //                        Expression.Break(
    //                            Break,
    //                            Expression.Constant(1)
    //                        ),
    //                        Break
    //                    );
    //                }
    //                case ExpressionType.Negate               :
    //                case ExpressionType.NegateChecked        :
    //                case ExpressionType.OnesComplement       :
    //                case ExpressionType.UnaryPlus            :
    //                    return Expression.MakeUnary(
    //                        NodeType,
    //                        this.Typeに対応したExpression(typeof(Int32)),
    //                        null
    //                    );
    //                case ExpressionType.PostDecrementAssign  :
    //                case ExpressionType.PostIncrementAssign  :
    //                case ExpressionType.PreDecrementAssign   :
    //                case ExpressionType.PreIncrementAssign   :{
    //                    var Parameter=Expression.Parameter(typeof(Int32));
    //                    return Expression.Block(
    //                        new[] {Parameter},
    //                        Expression.Assign(Parameter,Expression.Constant(0)),
    //                        Expression.MakeUnary(
    //                            NodeType,
    //                            Parameter,
    //                            null
    //                        )
    //                    );
    //                }
    //                case ExpressionType.AndAlso              :
    //                case ExpressionType.Equal                :
    //                case ExpressionType.GreaterThan          :
    //                case ExpressionType.GreaterThanOrEqual   :
    //                case ExpressionType.LessThan             :
    //                case ExpressionType.LessThanOrEqual      :
    //                case ExpressionType.NotEqual             :
    //                case ExpressionType.OrElse               : 
    //                case ExpressionType.Power                :
    //                case ExpressionType.PowerAssign          :
    //                case ExpressionType.Constant             :
    //                case ExpressionType.DebugInfo            :
    //                case ExpressionType.Dynamic              :
    //                case ExpressionType.Goto                 :
    //                case ExpressionType.Lambda               :
    //                case ExpressionType.ListInit             :
    //                case ExpressionType.MemberInit           :
    //                case ExpressionType.NewArrayBounds       :
    //                case ExpressionType.NewArrayInit         :
    //                case ExpressionType.RuntimeVariables     :
    //                case ExpressionType.Try                  :
    //                case ExpressionType.TypeEqual            :
    //                case ExpressionType.TypeIs               : 
    ////                case ExpressionType.IsFalse             );
    //                case ExpressionType.IsTrue               :
    //                case ExpressionType.Not                  :
    //                case ExpressionType.Quote                :
    //                case ExpressionType.Throw                :
    //                case ExpressionType.TypeAs               :
    //                case ExpressionType.Unbox                :
    //                default:
    //                    return Expression.Constant(r.Next(Int32.MaxValue));

    //            }
    //        }
    //private Expression Booleanに対応したExpression(){
    //    //Int
    //    var NodeType=(ExpressionType)r.Next((Int32)ExpressionType.IsFalse+1);
    //    switch(NodeType){
    //        case ExpressionType.And                  :
    //        case ExpressionType.Or                   :
    //        case ExpressionType.AndAlso              :
    //        case ExpressionType.OrElse               : 
    //            return Expression.MakeBinary(
    //                NodeType,
    //                this.Booleanに対応したExpression(),
    //                this.Booleanに対応したExpression()
    //            );
    //        case ExpressionType.Equal                :
    //        case ExpressionType.NotEqual             :
    //        case ExpressionType.GreaterThan          :
    //        case ExpressionType.GreaterThanOrEqual   :
    //        case ExpressionType.LessThan             :
    //        case ExpressionType.LessThanOrEqual      :
    //            return Expression.MakeBinary(
    //                NodeType,
    //                this.Typeに対応したExpression(typeof(Int32)),
    //                this.Typeに対応したExpression(typeof(Int32))
    //            );
    //        case ExpressionType.AndAssign            :
    //        case ExpressionType.Assign               :
    //        case ExpressionType.OrAssign:{
    //            var Parameter=Expression.Parameter(typeof(Boolean));
    //            return Expression.Block(
    //                new[] {Parameter},
    //                Expression.Assign(Parameter,Expression.Constant(true)),
    //                Expression.MakeBinary(
    //                    NodeType,
    //                    Parameter,
    //                    this.Booleanに対応したExpression()
    //                )
    //            );
    //        }
    //        case ExpressionType.ArrayIndex:{
    //            //return Expression.ArrayIndex(
    //            //    this.Arrayに対応したExpression(typeof(Boolean)),
    //            //    Expression.Constant(0)
    //            //);
    //            return null;
    //        }
    //        case ExpressionType.Block                :
    //            return Expression.Block(
    //                this.Booleanに対応したExpression()
    //            );
    //        case ExpressionType.Call                 :
    //            return Expression.Call(
    //                typeof(沢山の組み合わせ).GetMethod(nameof(GenericCall),BindingFlags.Static|BindingFlags.NonPublic).MakeGenericMethod(typeof(Boolean)),
    //                Expression.Constant(true)
    //            );
    //        case ExpressionType.Conditional          :
    //        case ExpressionType.Constant             :
    //        case ExpressionType.Default              :
    //        case ExpressionType.Index                :
    //            return Expression.ArrayIndex(
    //                Expression.NewArrayBounds(
    //                    typeof(Boolean),
    //                    this.Booleanに対応したExpression(),
    //                    this.Booleanに対応したExpression()
    //                ),
    //                this.Typeに対応したExpression(typeof(Int32)),
    //                this.Typeに対応したExpression(typeof(Int32))
    //            );
    //        case ExpressionType.Invoke               :
    //            return Expression.Invoke(
    //                Expression.Lambda<Func<Boolean>>(
    //                    Expression.Constant(true)
    //                )
    //            );
    //        case ExpressionType.IsFalse:
    //        case ExpressionType.IsTrue               :
    //            return Expression.MakeUnary(
    //                NodeType,
    //                Expression.Constant(new class_演算子オーバーロード(1,true)),
    //                null
    //            );
    //        case ExpressionType.Label                :{
    //            var Break=Expression.Label(typeof(Boolean));
    //            return Expression.Loop(
    //                Expression.Break(
    //                    Break,
    //                    Expression.Constant(true)
    //                ),
    //                Break
    //            );
    //        }
    //        case ExpressionType.Loop                 :{
    //            var Break=Expression.Label(typeof(Boolean));
    //            return Expression.Loop(
    //                Expression.Break(
    //                    Break,
    //                    Expression.Constant(true)
    //                ),
    //                Break
    //            );
    //        }
    //        case ExpressionType.MemberAccess         :
    //            return Expression.Property(
    //                null,
    //                typeof(ATest).GetProperty(nameof(Booleanを返すプロパティ),BindingFlags.NonPublic|BindingFlags.Static)
    //            );
    //        case ExpressionType.Parameter:{
    //            var Parameter=Expression.Parameter(typeof(Boolean),"Parameter");
    //            return Expression.Block(
    //                new[] {Parameter},
    //                Expression.Assign(Parameter,Expression.Constant(true)),
    //                Parameter
    //            );
    //        }
    //        case ExpressionType.Switch: { 
    //            return Expression.Switch(
    //                this.Typeに対応したExpression(typeof(Int32)),
    //                this.Typeに対応したExpression(typeof(Int32)),
    //                Expression.SwitchCase(
    //                    this.Booleanに対応したExpression(),
    //                    this.Booleanに対応したExpression()
    //                )
    //            );
    //        }
    //        case ExpressionType.TypeEqual            :
    //        case ExpressionType.TypeIs               : 
    //            //return Expression.MakeUnary(
    //            //    NodeType,
    //            //    Expression.Constant(new class_演算子オーバーロード(1,true)),
    //            //    null
    //            //);
    //        case ExpressionType.Convert              :
    //        case ExpressionType.ConvertChecked       :
    //        case ExpressionType.Not                  :

    //        case ExpressionType.Add                  :
    //        case ExpressionType.AddAssign            :
    //        case ExpressionType.AddAssignChecked     :
    //        case ExpressionType.AddChecked           :
    //        case ExpressionType.Coalesce             :
    //        case ExpressionType.Divide               :
    //        case ExpressionType.DivideAssign         :
    //        case ExpressionType.ExclusiveOr          :
    //        case ExpressionType.ExclusiveOrAssign    :
    //        case ExpressionType.LeftShift            :
    //        case ExpressionType.LeftShiftAssign      :
    //        case ExpressionType.Modulo               :
    //        case ExpressionType.ModuloAssign         :
    //        case ExpressionType.Multiply             :
    //        case ExpressionType.MultiplyAssign       :
    //        case ExpressionType.MultiplyAssignChecked:
    //        case ExpressionType.MultiplyChecked      :
    //        case ExpressionType.Power                :
    //        case ExpressionType.PowerAssign          :
    //        case ExpressionType.RightShift           :
    //        case ExpressionType.RightShiftAssign     :
    //        case ExpressionType.Subtract             :
    //        case ExpressionType.SubtractAssign       :
    //        case ExpressionType.SubtractAssignChecked:
    //        case ExpressionType.SubtractChecked      :
    //        case ExpressionType.DebugInfo            :
    //        case ExpressionType.Dynamic              :
    //        case ExpressionType.Goto                 :
    //        case ExpressionType.Lambda               :
    //        case ExpressionType.ListInit             :
    //        case ExpressionType.MemberInit           :
    //        case ExpressionType.NewArrayBounds       :
    //        case ExpressionType.NewArrayInit         :
    //        case ExpressionType.New                  :
    //        case ExpressionType.RuntimeVariables     :
    //        case ExpressionType.Try                  :
    //        case ExpressionType.ArrayLength          :
    //        case ExpressionType.Decrement            :
    //        case ExpressionType.Increment            :
    //        case ExpressionType.Negate               :
    //        case ExpressionType.NegateChecked        :
    //        case ExpressionType.OnesComplement       :
    //        case ExpressionType.PostDecrementAssign  :
    //        case ExpressionType.PostIncrementAssign  :
    //        case ExpressionType.PreDecrementAssign   :
    //        case ExpressionType.PreIncrementAssign   :
    //        case ExpressionType.Quote                :
    //        case ExpressionType.Throw                :
    //        case ExpressionType.TypeAs               :
    //        case ExpressionType.UnaryPlus            :
    //        case ExpressionType.Unbox                :
    //        default:
    //            return Expression.Constant(true);
    //    }
    //}
    //        private Expression Stringに対応したExpression(){
    //            var Type=typeof(String);
    //            var NodeType=(ExpressionType)r.Next((Int32)ExpressionType.IsFalse+1);
    //            switch(NodeType){
    //                case ExpressionType.Add:
    //                case ExpressionType.AddChecked           :
    //                case ExpressionType.And                  :
    //                case ExpressionType.Divide               :
    //                case ExpressionType.ExclusiveOr          :
    //                case ExpressionType.LeftShift            :
    //                case ExpressionType.Modulo               :
    //                case ExpressionType.Multiply             :
    //                case ExpressionType.MultiplyChecked      :
    //                case ExpressionType.Or                   :
    //                case ExpressionType.RightShift           :
    //                case ExpressionType.Subtract             :
    //                case ExpressionType.SubtractChecked      :
    //                case ExpressionType.AddAssign            :
    //                case ExpressionType.AddAssignChecked     :
    //                case ExpressionType.AndAssign            :
    //                case ExpressionType.Assign               :
    //                case ExpressionType.DivideAssign         :
    //                case ExpressionType.LeftShiftAssign      :
    //                case ExpressionType.ModuloAssign         :
    //                case ExpressionType.MultiplyAssign       :
    //                case ExpressionType.MultiplyAssignChecked:
    //                case ExpressionType.ExclusiveOrAssign    :
    //                case ExpressionType.RightShiftAssign     :
    //                case ExpressionType.SubtractAssign       :
    //                case ExpressionType.SubtractAssignChecked:
    //                case ExpressionType.OrAssign:
    //                case ExpressionType.ArrayIndex:
    //                case ExpressionType.Block                :
    //                    return Expression.Block(
    //                        this.Stringに対応したExpression()
    //                    );
    //                case ExpressionType.Call                 :
    //                    return Expression.Call(
    //                        typeof(沢山の組み合わせ).GetMethod(nameof(GenericCall),BindingFlags.Static|BindingFlags.NonPublic).MakeGenericMethod(Type),
    //                        Expression.Constant(r.Next().ToString())
    //                    );
    //                case ExpressionType.Coalesce             :
    //                    return Expression.Coalesce(
    //                        this.Stringに対応したExpression(),
    //                        this.Stringに対応したExpression()
    //                    );
    //                case ExpressionType.Conditional          :
    //                    return Expression.Condition(
    //                        this.Booleanに対応したExpression(),
    //                        this.Stringに対応したExpression(),
    //                        this.Stringに対応したExpression()
    //                    );
    //                case ExpressionType.Default              :
    //                    return Expression.Default(Type);
    //                case ExpressionType.Index                :
    //                    return Expression.ArrayIndex(
    //                        Expression.NewArrayBounds(
    //                            Type,
    //                            this.Typeに対応したExpression(typeof(Int32)),
    //                            this.Typeに対応したExpression(typeof(Int32))
    //                        ),
    //                        this.Typeに対応したExpression(typeof(Int32)),
    //                        this.Typeに対応したExpression(typeof(Int32))
    //                    );
    //                case ExpressionType.Invoke               :
    //                    return Expression.Invoke(
    //                        Expression.Lambda<Func<Int32>>(
    //                            Expression.Constant(2)
    //                        )
    //                    );
    //                case ExpressionType.MemberAccess         :
    //                    return Expression.Property(
    //                        Expression.New(
    //                            typeof(Point)
    //                        ),
    //                        nameof(Point.X)
    //                    );
    //                case ExpressionType.New                  :
    //                    return Expression.New(
    //                        typeof(Int32)
    //                    );
    //                case ExpressionType.Parameter:{
    //                    var Parameter=Expression.Parameter(Type,"Parameter");
    //                    return Expression.Block(
    //                        new[] {Parameter},
    //                        Expression.Assign(Parameter,Expression.Constant(r.Next().ToString())),
    //                        Parameter
    //                    );
    //                }
    //                case ExpressionType.Switch: { 
    //                    return Expression.Switch(
    //                        this.Typeに対応したExpression(typeof(Int32)),
    //                        this.Typeに対応したExpression(typeof(Int32)),
    //                        Expression.SwitchCase(
    //                            this.Typeに対応したExpression(typeof(Int32)),
    //                            this.Typeに対応したExpression(typeof(Int32))
    //                        )
    //                    );
    //                }
    //                case ExpressionType.ArrayLength          :
    //                    return Expression.ArrayLength(
    //                        this.Arrayに対応したExpression(typeof(Int32))
    //                    );
    //                case ExpressionType.Convert              :
    //                case ExpressionType.ConvertChecked       :
    //                    return Expression.MakeUnary(
    //                        NodeType,
    //                        this.Typeに対応したExpression(typeof(Int32)),
    //                        typeof(Int32)
    //                    );
    //                case ExpressionType.Decrement            :
    //                case ExpressionType.Increment            :
    //                    return Expression.MakeUnary(
    //                        NodeType,
    //                        this.Typeに対応したExpression(typeof(Int32)),
    //                        null
    //                    );
    //                case ExpressionType.Label:{
    //                    var Break=Expression.Label(typeof(Int32));
    //                    return Expression.Loop(
    //                        Expression.Break(
    //                            Break,
    //                            Expression.Constant(1)
    //                        ),
    //                        Break
    //                    );
    //                }
    //                case ExpressionType.Loop:{
    //                    var Break=Expression.Label(typeof(Int32));
    //                    return Expression.Loop(
    //                        Expression.Break(
    //                            Break,
    //                            Expression.Constant(1)
    //                        ),
    //                        Break
    //                    );
    //                }
    //                case ExpressionType.Negate               :
    //                case ExpressionType.NegateChecked        :
    //                case ExpressionType.OnesComplement       :
    //                case ExpressionType.UnaryPlus            :
    //                    return Expression.MakeUnary(
    //                        NodeType,
    //                        this.Typeに対応したExpression(typeof(Int32)),
    //                        null
    //                    );
    //                case ExpressionType.PostDecrementAssign  :
    //                case ExpressionType.PostIncrementAssign  :
    //                case ExpressionType.PreDecrementAssign   :
    //                case ExpressionType.PreIncrementAssign   :{
    //                    var Parameter=Expression.Parameter(typeof(Int32));
    //                    return Expression.Block(
    //                        new[] {Parameter},
    //                        Expression.Assign(Parameter,Expression.Constant(0)),
    //                        Expression.MakeUnary(
    //                            NodeType,
    //                            Parameter,
    //                            null
    //                        )
    //                    );
    //                }
    //                case ExpressionType.AndAlso              :
    //                case ExpressionType.Equal                :
    //                case ExpressionType.GreaterThan          :
    //                case ExpressionType.GreaterThanOrEqual   :
    //                case ExpressionType.LessThan             :
    //                case ExpressionType.LessThanOrEqual      :
    //                case ExpressionType.NotEqual             :
    //                case ExpressionType.OrElse               : 
    //                case ExpressionType.Power                :
    //                case ExpressionType.PowerAssign          :
    //                case ExpressionType.Constant             :
    //                case ExpressionType.DebugInfo            :
    //                case ExpressionType.Dynamic              :
    //                case ExpressionType.Goto                 :
    //                case ExpressionType.Lambda               :
    //                case ExpressionType.ListInit             :
    //                case ExpressionType.MemberInit           :
    //                case ExpressionType.NewArrayBounds       :
    //                case ExpressionType.NewArrayInit         :
    //                case ExpressionType.RuntimeVariables     :
    //                case ExpressionType.Try                  :
    //                case ExpressionType.TypeEqual            :
    //                case ExpressionType.TypeIs               : 
    ////                case ExpressionType.IsFalse             );
    //                case ExpressionType.IsTrue               :
    //                case ExpressionType.Not                  :
    //                case ExpressionType.Quote                :
    //                case ExpressionType.Throw                :
    //                case ExpressionType.TypeAs               :
    //                case ExpressionType.Unbox                :
    //                default:
    //                    return Expression.Constant(r.Next(Int32.MaxValue).ToString());
    //            }
    //        }
    private void BooleanのオペランドTypeに対応したExpression(out Expression オペランド1, out Expression オペランド2)
    {
        switch (this.r.Next(3))
        {
            case 0:
            {
                var Type = typeof(int);
                オペランド1 = this.Typeに対応したExpression(Type);
                オペランド2 = this.Typeに対応したExpression(Type);
                break;
            }
            case 1:
            {
                var Type = typeof(double);
                オペランド1 = this.Typeに対応したExpression(Type);
                オペランド2 = this.Typeに対応したExpression(Type);
                break;
            }
            case 2:
            {
                var Type = typeof(decimal);
                オペランド1 = this.Typeに対応したExpression(Type);
                オペランド2 = this.Typeに対応したExpression(Type);
                break;
            }
            default:
                throw new NotSupportedException();
        }
    }
    private int Typeに対応したExpressionCount;
    private Expression Typeに対応したExpression(Type Type)
    {
        //                    if(Type==typeof(Int32)||Type==typeof(Double)||Type==typeof(Decimal)||Type==typeof(Decimal)||Type==typeof(String)) break;
    再試行:
        var NodeType = (ExpressionType)this.Typeに対応したExpressionCount++;
        // ReSharper disable once SwitchStatementMissingSomeCases
        switch (NodeType)
        {
            case ExpressionType.Add:
            case ExpressionType.AddChecked:
            case ExpressionType.Divide:
            case ExpressionType.Multiply:
            case ExpressionType.MultiplyChecked:
            case ExpressionType.Subtract:
            case ExpressionType.SubtractChecked:
                if (Type == typeof(int) || Type == typeof(double) || Type == typeof(decimal))
                {
                    return Expression.MakeBinary(
                        NodeType,
                        this.Typeに対応したExpression(Type),
                        this.Typeに対応したExpression(Type)
                    );
                }
                break;
            case ExpressionType.And:
            case ExpressionType.Or:
            case ExpressionType.ExclusiveOr:
                if (Type == typeof(bool) || Type == typeof(int) || Type == typeof(double))
                {
                    return Expression.MakeBinary(
                        NodeType,
                        this.Typeに対応したExpression(Type),
                        this.Typeに対応したExpression(Type)
                    );
                }
                break;
            case ExpressionType.Modulo:
            case ExpressionType.LeftShift:
            case ExpressionType.RightShift:
                if (Type == typeof(int))
                {
                    return Expression.MakeBinary(
                        NodeType,
                        this.Typeに対応したExpression(Type),
                        this.Typeに対応したExpression(Type)
                    );
                }
                break;
            case ExpressionType.Power:
                if (Type == typeof(double))
                {
                    return Expression.MakeBinary(
                        NodeType,
                        this.Typeに対応したExpression(Type),
                        this.Typeに対応したExpression(Type)
                    );
                }
                break;
            case ExpressionType.AddAssign:
            case ExpressionType.AddAssignChecked:
            case ExpressionType.DivideAssign:
            case ExpressionType.LeftShiftAssign:
            case ExpressionType.ModuloAssign:
            case ExpressionType.MultiplyAssign:
            case ExpressionType.MultiplyAssignChecked:
            case ExpressionType.ExclusiveOrAssign:
            case ExpressionType.RightShiftAssign:
            case ExpressionType.SubtractAssign:
            case ExpressionType.SubtractAssignChecked:
            {
                if (Type == typeof(int) || Type == typeof(double) || Type == typeof(decimal))
                {
                    var Parameter = Expression.Parameter(Type);
                    return Expression.Block(
                        new[] { Parameter },
                        Expression.Assign(
                            Parameter,
                            Expression.Default(Type)
                        ),
                        Expression.MakeBinary(
                            NodeType,
                            Parameter,
                            this.Typeに対応したExpression(Type)
                        )
                    );
                }
                break;
            }
            case ExpressionType.PowerAssign:
            {
                if (Type == typeof(double))
                {
                    var Parameter = Expression.Parameter(Type);
                    return Expression.Block(
                        new[] { Parameter },
                        Expression.Assign(
                            Parameter,
                            Expression.Default(Type)
                        ),
                        Expression.MakeBinary(
                            NodeType,
                            Parameter,
                            this.Typeに対応したExpression(Type)
                        )
                    );
                }
                break;
            }
            case ExpressionType.AndAssign:
            case ExpressionType.OrAssign:
            {
                if (Type == typeof(int) || Type == typeof(bool))
                {
                    var Parameter = Expression.Parameter(Type);
                    return Expression.Block(
                        new[] { Parameter },
                        Expression.Assign(Parameter, Expression.Constant(0)),
                        Expression.MakeBinary(
                            NodeType,
                            Parameter,
                            this.Typeに対応したExpression(Type)
                        )
                    );
                }
                break;
            }
            case ExpressionType.ArrayIndex:
            {
                return Expression.ArrayIndex(
                    this.Typeに対応したExpression(Type.MakeArrayType()),
                    Expression.Constant(0)
                );
            }
            case ExpressionType.Assign:
            case ExpressionType.Block:
                return Expression.Block(
                    this.Typeに対応したExpression(Type)
                );
            case ExpressionType.Call:
                return Expression.Call(
                    typeof(沢山の組み合わせ).GetMethod(nameof(GenericCall), BindingFlags.Static | BindingFlags.NonPublic)!.MakeGenericMethod(Type),
                    Expression.Default(Type)
                );
            case ExpressionType.Coalesce:
                if (Type == typeof(bool) || Type == typeof(int) || Type == typeof(double) || Type == typeof(decimal))
                {
                    return Expression.Coalesce(
                        Expression.Convert(
                            this.Typeに対応したExpression(Type),
                            typeof(Nullable<>).MakeGenericType(Type)
                        ),
                        this.Typeに対応したExpression(Type)
                    );
                }
                if (Type == typeof(string))
                {
                    return Expression.Coalesce(
                        this.Typeに対応したExpression(Type),
                        this.Typeに対応したExpression(Type)
                    );
                }
                break;
            case ExpressionType.Conditional:
                return Expression.Condition(
                    this.Typeに対応したExpression(typeof(bool)),
                    this.Typeに対応したExpression(Type),
                    this.Typeに対応したExpression(Type)
                );
            case ExpressionType.Default:
                return Expression.Default(Type);
            case ExpressionType.Index:
                return Expression.ArrayIndex(
                    Expression.NewArrayBounds(
                        Type,
                        this.Typeに対応したExpression(typeof(int)),
                        this.Typeに対応したExpression(typeof(int))
                    ),
                    Expression.Constant(0)
                );
            case ExpressionType.Invoke:
                return Expression.Invoke(
                    Expression.Lambda(
                        typeof(Func<>).MakeGenericType(Type),
                        Expression.Default(Type)
                    )
                );
            case ExpressionType.MemberAccess:
                return Expression.Field(
                    Expression.New(
                        typeof(GenericStruct<>).MakeGenericType(Type)
                    ),
                    nameof(GenericStruct<int>.Field)
                );
            case ExpressionType.New:
                return Expression.New(
                    Type
                );
            case ExpressionType.Parameter:
            {
                var Parameter = Expression.Parameter(Type, "Parameter");
                return Expression.Block(
                    new[] { Parameter },
                    Expression.Assign(
                        Parameter,
                        Expression.Default(Type)
                    ),
                    Parameter
                );
            }
            case ExpressionType.Switch:
            {
                return Expression.Switch(
                    this.Typeに対応したExpression(typeof(int)),
                    this.Typeに対応したExpression(Type),
                    Expression.SwitchCase(
                        this.Typeに対応したExpression(Type),
                        this.Typeに対応したExpression(Type)
                    )
                );
            }
            case ExpressionType.ArrayLength:
                if (Type == typeof(int))
                {
                    return Expression.ArrayLength(
                        this.Typeに対応したExpression(Type.MakeArrayType())
                    );
                }
                break;
            case ExpressionType.Convert:
            case ExpressionType.ConvertChecked:
                return Expression.MakeUnary(
                    NodeType,
                    this.Typeに対応したExpression(typeof(int)),
                    Type
                );
            case ExpressionType.Decrement:
            case ExpressionType.Increment:
                if (Type == typeof(int) || Type == typeof(double) || Type == typeof(decimal))
                {
                    return Expression.MakeUnary(
                        NodeType,
                        this.Typeに対応したExpression(Type),
                        null
                    );
                }
                break;
            case ExpressionType.Label:
            {
                var Break = Expression.Label(Type);
                return Expression.Loop(
                    Expression.Break(
                        Break,
                        Expression.Default(Type)
                    ),
                    Break
                );
            }
            case ExpressionType.Loop:
            {
                var Break = Expression.Label(Type);
                return Expression.Loop(
                    Expression.Break(
                        Break,
                        Expression.Default(Type)
                    ),
                    Break
                );
            }
            case ExpressionType.Negate:
            case ExpressionType.NegateChecked:
            case ExpressionType.OnesComplement:
                if (Type == typeof(int) || Type == typeof(double) || Type == typeof(decimal))
                {
                    return Expression.MakeUnary(
                        NodeType,
                        this.Typeに対応したExpression(Type),
                        null
                    );
                }
                break;
            case ExpressionType.UnaryPlus:
                if (Type == typeof(struct_演算子オーバーロード))
                {
                    return Expression.MakeUnary(
                        NodeType,
                        this.Typeに対応したExpression(Type),
                        null
                    );
                }
                break;
            case ExpressionType.PostDecrementAssign:
            case ExpressionType.PostIncrementAssign:
            case ExpressionType.PreDecrementAssign:
            case ExpressionType.PreIncrementAssign:
            {
                var Parameter = Expression.Parameter(Type);
                return Expression.Block(
                    new[] { Parameter },
                    Expression.Assign(
                        Parameter,
                        Expression.Default(Type)
                    ),
                    Expression.MakeUnary(
                        NodeType,
                        Parameter,
                        null
                    )
                );
            }
            case ExpressionType.AndAlso:
            case ExpressionType.OrElse:
            {
                if (Type == typeof(bool) || Type == typeof(struct_ショートカット検証))
                {
                    return Expression.MakeBinary(
                        NodeType,
                        this.Typeに対応したExpression(Type),
                        this.Typeに対応したExpression(Type)
                    );
                }
                break;
            }
            case ExpressionType.Equal:
            case ExpressionType.NotEqual:
                if (Type == typeof(bool) || Type == typeof(結果))
                {
                    return Expression.MakeBinary(
                        NodeType,
                        this.Typeに対応したExpression(Type),
                        this.Typeに対応したExpression(Type)
                    );
                }
                break;
            case ExpressionType.GreaterThan:
            case ExpressionType.GreaterThanOrEqual:
            case ExpressionType.LessThan:
            case ExpressionType.LessThanOrEqual:
                if (Type == typeof(bool))
                {
                    this.BooleanのオペランドTypeに対応したExpression(out var オペランド1, out var オペランド2);
                    return Expression.MakeBinary(
                        NodeType,
                        オペランド1,
                        オペランド2
                    );
                }
                break;
            case ExpressionType.MemberInit:
                if (Type == typeof(結果))
                {
                    return Expression.MemberInit(
                        Expression.New(
                            typeof(結果)
                        ),
                        Bind(nameof(結果.Int32), this.Typeに対応したExpression(typeof(int))),
                        Bind(nameof(結果.Boolean), this.Typeに対応したExpression(typeof(bool))),
                        Bind(nameof(結果.String), this.Typeに対応したExpression(typeof(string))),
                        Bind(nameof(結果._結果), this.Typeに対応したExpression(typeof(結果)))
                    );
                }
                break;
            case ExpressionType.DebugInfo:
            case ExpressionType.Dynamic:
            case ExpressionType.Goto:
            case ExpressionType.Lambda:
            case ExpressionType.ListInit:
            case ExpressionType.NewArrayBounds:
            case ExpressionType.NewArrayInit:
            case ExpressionType.RuntimeVariables:
            case ExpressionType.Try:
            case ExpressionType.TypeEqual:
            case ExpressionType.TypeIs:
            //                case ExpressionType.IsFalse             );
            case ExpressionType.IsTrue:
            case ExpressionType.Not:
            case ExpressionType.Quote:
            case ExpressionType.Throw:
            case ExpressionType.TypeAs:
            case ExpressionType.Unbox:
            case ExpressionType.Constant:
                break;
            default:
                this.Typeに対応したExpressionCount = 0;
                goto 再試行;
        }
        return Expression.Constant(Activator.CreateInstance(Type));
    }
    [TestMethod]
    public void Typeに対応したExpressionを作る()
    {
        var Types = new[] { typeof(bool), typeof(int), typeof(double), typeof(decimal) };
        for (var a = 0; a < 1000; a++)
        {
            var Type = Types[a % Types.Length];
            var Body = this.Typeに対応したExpression(Type);
            var result = this.CreateDelegate(
                Expression.Lambda(
                    typeof(Func<>).MakeGenericType(Type),
                    Body
                )
            );
        }
    }
    [TestMethod]
    public void シグネチャ()
    {
        var Types = new[] { typeof(int) };
        for (var a = 0; a < 1000; a++)
        {
            var Type = Types[this.r.Next(Types.Length)];
            // ReSharper disable once InvertIf
            if (Type == typeof(int))
            {
                var Body = this.Typeに対応したExpression(typeof(int));
                var result = this.CreateDelegate(
                    Expression.Lambda<Func<int>>(
                        Body
                    )
                );
            }
        }
    }
    [TestMethod]
    public void サンキーダイアグラムに実行計画を表示()
    {
        var outer = new int[10];
        var inner = new int[20];
        this.実行結果が一致するか確認(() => from o in outer join i in inner on o equals i select new { o, i });
    }
}
//public interface IEnumerable2<out T>:IEnumerable<T>{
//}
//public interface IQueryable2<out T>:IQueryable<T>{
//}
//public class Enumerable2<T>:IEnumerable2<T>{
//    public IEnumerator<T> GetEnumerator(){
//        yield return default(T);
//    }
//    IEnumerator IEnumerable.GetEnumerator(){
//        yield return default(T);
//    }
//}
//public class Queryable2<T>:IQueryable2<T>{
//    public IEnumerator<T> GetEnumerator(){
//        yield return default(T);
//    }
//    IEnumerator IEnumerable.GetEnumerator(){
//        yield return default(T);
//    }
//    public Expression Expression{get;}
//    public Type ElementType{get;}
//    public IQueryProvider Provider{get;}
//}
//sealed class SelectEnumerator<T, TResult> : IEnumerator<TResult>{
//    private readonly IEnumerable<T> source;
//    private readonly Func<T, TResult> selector;
//    private readonly IEnumerator<T> enumerator;
//    public TResult Current =>this.selector(this.enumerator.Current);
//    object IEnumerator.Current=>this.selector(this.enumerator.Current);
//    public SelectEnumerator(IEnumerable<T> source,Func<T, TResult> selector)
//    {
//        this.source=source;
//        this.selector=selector;
//        this.enumerator=source.GetEnumerator();
//    }
//    public Boolean MoveNext()=>this.enumerator.MoveNext();
//    void IEnumerator.Reset()
//    {
//        throw new NotSupportedException();
//    }
//    public void Dispose(){
//        throw new NotImplementedException();
//    }
//}
//sealed class SelectEnumerable<T, TResult> : IEnumerable2<TResult>{
//    private readonly IEnumerable<T> source;
//    private readonly Func<T, TResult> selector;
//    private readonly IEnumerator<TResult> enumerator;
//    public SelectEnumerable(IEnumerable<T> source,Func<T, TResult> selector)
//    {
//        this.source=source;
//        this.selector=selector;
//        this.enumerator=new SelectEnumerator<T,TResult>(this.source,this.selector);
//    }
//    public Boolean MoveNext()=>this.enumerator.MoveNext();
//    public void Dispose(){
//        throw new NotImplementedException();
//    }
//    public IEnumerator<TResult> GetEnumerator() =>this.enumerator;
//    IEnumerator IEnumerable.GetEnumerator()=>this.enumerator;
//}
//public static class ExtendEnumerable2{
//    public static IEnumerator<T> S<T>(){
//        yield return default(T);
//    }
//    public static IEnumerable<T> U<T>(){
//        yield return default(T);
//    }
//    //public static IEnumerable2<T> V<T>(){
//    //    yield return default(T);
//    //}
//    public static void 呼び出しテスト(){
//        var source=new Enumerable2<Int32>();
//        var x=source.Select(p=>p*2).Select(p=>p*4).AsEnumerable().Select(p=>p*5);
//    }
//    public static IEnumerable2<TResult> Select<T, TResult>(this IEnumerable2<T> source,Func<T,TResult> func) {
//        var r = new SelectEnumerable<T,TResult>(source,func);
//        return r;
//    }
//}
//public static class ExtendQueryable2{
//    public static void 呼び出しテスト(){
//        var source=new Queryable2<Int32>();
//        var x=source.Select(p=>p*2).Select(p=>p*4).AsEnumerable().Select(p=>p*5);
//    }
//    public static IEnumerable2<TResult> Select<T, TResult>(this IQueryable2<T> source,Func<T,TResult> func) {
//        var r = new SelectEnumerable<T,TResult>(source,func);
//        return r;
//    }
//}