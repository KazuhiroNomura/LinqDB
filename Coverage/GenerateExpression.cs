using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using static System.Diagnostics.Contracts.Contract;
// ReSharper disable PossibleNullReferenceException

namespace CoverageCS;

public class GenerateExpression
{
    private static readonly Type[] Types = {
        typeof(byte),typeof(int),typeof(Func<int>)
    };
    private static readonly Type[] ValueTypes= Types.Where(p => p.IsValueType).ToArray();
    private static readonly Type[] PrimitiveTypes = Types.Where(p => p.IsPrimitive).ToArray();
    private static readonly Type[] ClassTypes = Types.Where(p => p.IsClass).ToArray();
    private static readonly Type[] EnumTypes = Types.Where(p => p.IsEnum).ToArray();
    private static readonly Random r = new(1);
    private readonly int 最大の深さ;
    public GenerateExpression(int 最大の深さ)
    {
        this.最大の深さ = 最大の深さ;
    }
    //private static readonly Type[] ランダムなValueTypes = {
    //    typeof(Int32)
    //};
    private static Expression NodeTypeとTypeに対応する末尾Expression(Type Type) {
        if(Type==typeof(byte))
            return Expression.Constant((byte)r.Next(256));
        if(Type==typeof(int))
            return Expression.Constant(r.Next(int.MaxValue));
        return Expression.Default(Type);
    }
    private T 選択<T>(T[] ここから選択) => ここから選択[r.Next(ここから選択.Length)];
    private static ExpressionType[] Nodes = {
        ExpressionType.Constant,
        ExpressionType.Default,
        ExpressionType.Parameter
    };
    private const BindingFlags B = BindingFlags.Static | BindingFlags.NonPublic;
    private static MethodInfo M<T>(Expression<Func<T>> e) => ((MethodCallExpression)e.Body).Method;
    private static T PrivateCall0<T>() => default!;
    private static T PrivateCall1<T, A>(A a) => default!;
    private static T PrivateCall2<T, A, B>(A a, B b) => default!;
    private readonly Dictionary<Type, MethodInfo[]> DictionaryTypeMethods = new()
    {
        {
            typeof(bool),
            new[] {
                typeof(GenerateExpression).GetMethod(nameof(PrivateCall0),B)!.MakeGenericMethod(typeof(bool)),
                typeof(GenerateExpression).GetMethod(nameof(PrivateCall1),B)!.MakeGenericMethod(typeof(bool),typeof(bool)),
                typeof(GenerateExpression).GetMethod(nameof(PrivateCall2),B)!.MakeGenericMethod(typeof(bool),typeof(bool),typeof(bool))
            }
        }
    };
    //private readonly Dictionary<Type, ConstructorInfo[]> DictionaryTypeConstructorInfos = new Dictionary<Type, ConstructorInfo[]> {
    //    {
    //        typeof(Boolean),
    //        new ConstructorInfo[] {
    //            null
    //        }
    //    }
    //};
    private class MemberClass<T>
    {
        public T Member = default!;
    }
    //private Dictionary<Type, MemberInfo[]> DictionaryTypeMemberExpression = new Dictionary<Type, MemberInfo[]> {
    //    {
    //        typeof(Boolean),
    //        new[] {
    //            typeof(MemberClass<Boolean>).GetField(nameof(MemberClass<Boolean>.Member),B)
    //        }
    //    }
    //};
    //private Dictionary<Type, ExpressionType[]> DictionaryTypeExpressionTypes = new Dictionary<Type, ExpressionType[]>{
    //    {
    //        typeof(Boolean),
    //        new[]{
    //            ExpressionType.Add                  ,
    //            ExpressionType.AddAssign            ,
    //            ExpressionType.AddAssignChecked     ,
    //            ExpressionType.AndAlso              ,
    //            ExpressionType.And                  ,
    //            ExpressionType.AndAssign            ,
    //            ExpressionType.AndAlso              ,
    //            ExpressionType.Assign               ,
    //            ExpressionType.Equal                ,
    //            ExpressionType.ExclusiveOr          ,
    //            ExpressionType.ExclusiveOrAssign    ,
    //            ExpressionType.GreaterThan          ,
    //            ExpressionType.GreaterThanOrEqual   ,
    //            ExpressionType.LessThan             ,
    //            ExpressionType.LessThanOrEqual      ,
    //            ExpressionType.NotEqual             ,
    //            ExpressionType.Or                   ,
    //            ExpressionType.OrAssign             ,
    //            ExpressionType.OrElse               ,
    //            ExpressionType.Conditional          ,
    //            ExpressionType.Invoke               ,
    //            ExpressionType.Loop                 ,
    //            ExpressionType.MemberAccess         ,
    //            ExpressionType.MemberInit           ,
    //            ExpressionType.Call                 ,
    //            ExpressionType.New                  ,
    //            ExpressionType.RuntimeVariables     ,
    //            ExpressionType.Switch               ,
    //            ExpressionType.Try                  ,
    //            ExpressionType.TypeEqual            ,
    //            ExpressionType.TypeIs               ,
    //            ExpressionType.Convert              ,
    //            ExpressionType.ConvertChecked       ,
    //            ExpressionType.IsFalse              ,
    //            ExpressionType.IsTrue               ,
    //            ExpressionType.Not
    //        }
    //    }

    //};
    private readonly ExpressionType[] Leafs = {
        ExpressionType.Add                  ,
        ExpressionType.AddAssign            ,
        ExpressionType.AddAssignChecked     ,
        ExpressionType.AddChecked           ,
        ExpressionType.And                  ,
        ExpressionType.AndAssign            ,
        ExpressionType.AndAlso              ,
        ExpressionType.ArrayIndex           ,
        ExpressionType.Assign               ,
        ExpressionType.Coalesce             ,
        ExpressionType.Divide               ,
        ExpressionType.DivideAssign         ,
        ExpressionType.Equal                ,
        ExpressionType.ExclusiveOr          ,
        ExpressionType.ExclusiveOrAssign    ,
        ExpressionType.GreaterThan          ,
        ExpressionType.GreaterThanOrEqual   ,
        ExpressionType.LeftShift            ,
        ExpressionType.LeftShiftAssign      ,
        ExpressionType.LessThan             ,
        ExpressionType.LessThanOrEqual      ,
        ExpressionType.Modulo               ,
        ExpressionType.ModuloAssign         ,
        ExpressionType.Multiply             ,
        ExpressionType.MultiplyAssign       ,
        ExpressionType.MultiplyAssignChecked,
        ExpressionType.MultiplyChecked      ,
        ExpressionType.NotEqual             ,
        ExpressionType.Or                   ,
        ExpressionType.OrAssign             ,
        ExpressionType.OrElse               ,
        ExpressionType.Power                ,
        ExpressionType.PowerAssign          ,
        ExpressionType.RightShift           ,
        ExpressionType.RightShiftAssign     ,
        ExpressionType.Subtract             ,
        ExpressionType.SubtractAssign       ,
        ExpressionType.SubtractAssignChecked,
        ExpressionType.SubtractChecked      ,
        ExpressionType.Block                ,
        ExpressionType.Conditional          ,
        ExpressionType.Default              ,
        ExpressionType.Dynamic              ,
        ExpressionType.Goto                 ,
        ExpressionType.Index                ,
        ExpressionType.Invoke               ,
        ExpressionType.Label                ,
        ExpressionType.Lambda               ,
        ExpressionType.ListInit             ,
        ExpressionType.Loop                 ,
        ExpressionType.MemberAccess         ,
        ExpressionType.MemberInit           ,
        ExpressionType.Call                 ,
        ExpressionType.NewArrayBounds       ,
        ExpressionType.NewArrayInit         ,
        ExpressionType.New                  ,
        ExpressionType.RuntimeVariables     ,
        ExpressionType.Switch               ,
        ExpressionType.Try                  ,
        ExpressionType.TypeEqual            ,
        ExpressionType.TypeIs               ,
        ExpressionType.ArrayLength          ,
        ExpressionType.Convert              ,
        ExpressionType.ConvertChecked       ,
        ExpressionType.Decrement            ,
        ExpressionType.Increment            ,
        ExpressionType.IsFalse              ,
        ExpressionType.IsTrue               ,
        ExpressionType.Negate               ,
        ExpressionType.NegateChecked        ,
        ExpressionType.Not                  ,
        ExpressionType.OnesComplement       ,
        ExpressionType.PostDecrementAssign  ,
        ExpressionType.PostIncrementAssign  ,
        ExpressionType.PreDecrementAssign   ,
        ExpressionType.PreIncrementAssign   ,
        ExpressionType.Quote                ,
        ExpressionType.Throw                ,
        ExpressionType.TypeAs               ,
        ExpressionType.UnaryPlus            ,
        ExpressionType.Unbox
    };

    private Expression Booleanに対応するExpression(int 深さ)
    {
        var ResultType = typeof(bool);
    再試行:
        var NodeType = this.選択(this.Leafs);
        switch (NodeType)
        {
            case ExpressionType.And:
            case ExpressionType.AndAlso:
            case ExpressionType.Equal:
            case ExpressionType.ExclusiveOr:
            //                case ExpressionType.GreaterThan:
            //              case ExpressionType.GreaterThanOrEqual:
            case ExpressionType.NotEqual:
            case ExpressionType.Or:
            case ExpressionType.OrElse:
            {
                return Expression.MakeBinary(NodeType, this.Typeに対応するExpression(深さ, ResultType), this.Typeに対応するExpression(深さ, ResultType));
            }
            case ExpressionType.Assign:
            case ExpressionType.AndAssign:
            case ExpressionType.ExclusiveOrAssign:
            case ExpressionType.OrAssign:
            {
                return Expression.MakeBinary(NodeType, Typeに対応するAssignの左辺Expression(深さ, ResultType), this.Typeに対応するExpression(深さ, ResultType));
            }
            case ExpressionType.MemberAccess:
            {
                var Type2 = typeof(MemberClass<>).MakeGenericType(ResultType);
                var expression = this.Typeに対応するExpression(深さ, Type2);
                var field = Type2.GetField(nameof(MemberClass<object>.Member));
                return Expression.Field(
                    expression,
                    field
                );
            }
            case ExpressionType.Convert:
            case ExpressionType.ConvertChecked:
            {
                var Operand = this.Typeに対応するExpression(深さ, Types[r.Next(Types.Length)]);
                return Expression.MakeUnary(
                    NodeType,
                    Operand,
                    ResultType
                );
            }
            case ExpressionType.TypeIs:
            {
                var Operand = this.Typeに対応するExpression(深さ, Types[r.Next(Types.Length)]);
                return Expression.TypeIs(
                    Operand,
                    Types[r.Next(Types.Length)]
                );
            }
            //case ExpressionType.TypeIs:{
            //    var operand=Typeに対応するExpression(深さ,Type);
            //    var type=typeof(Boolean);
            //    return Expression.MakeUnary(NodeType, operand, type);
            //}
            case ExpressionType.Not:
            {
                var operand = this.Typeに対応するExpression(深さ, ResultType);
                return Expression.Not(operand);
            }
            case ExpressionType.Call:
            {
                var Methods = this.DictionaryTypeMethods[typeof(bool)];
                var Method = this.選択(Methods);
                if (Method.IsStatic)
                {
                    var Parmaeters = Method.GetParameters();
                    var Arguments = new Expression[Parmaeters.Length];
                    for (var a = 0; a < Parmaeters.Length; a++)
                    {
                        Arguments[a] = this.Typeに対応するExpression(深さ, Parmaeters[a].ParameterType);
                    }
                    return Expression.Call(Method, Arguments);
                }
                else
                {
                    var Object = this.Typeに対応するExpression(深さ, Method.DeclaringType);
                    var Parameters = Method.GetParameters();
                    var Arguments = new Expression[Parameters.Length];
                    for (var a = 0; a < Parameters.Length; a++)
                    {
                        Arguments[a] = this.Typeに対応するExpression(深さ, Parameters[a].ParameterType);
                    }
                    return Expression.Call(Object, Method, Arguments);
                }
            }
            case ExpressionType.New:
            {
                var Constructors = ResultType.GetConstructors();
                if (Constructors.Length == 0)
                {
                    return Expression.New(ResultType);
                }
                else
                {
                    var Constructor = this.選択(Constructors);
                    var Parameters = Constructor.GetParameters();
                    var Arguments = new Expression[Parameters.Length];
                    for (var a = 0; a < Parameters.Length; a++)
                    {
                        Arguments[a] = this.Typeに対応するExpression(深さ, Parameters[a].ParameterType);
                    }
                    return Expression.New(Constructor, Arguments);
                }
            }
            case ExpressionType.RuntimeVariables:
            case ExpressionType.Switch:
            case ExpressionType.Try:
            case ExpressionType.TypeEqual:
            case ExpressionType.Conditional:
            {
                var test = this.Typeに対応するExpression(深さ, typeof(bool));
                var ifTrue = this.Typeに対応するExpression(深さ, typeof(bool));
                var ifFalse = this.Typeに対応するExpression(深さ, typeof(bool));
                return Expression.Condition(
                    test,
                    ifTrue,
                    ifFalse
                );
            }
            case ExpressionType.Dynamic:
            case ExpressionType.Goto:
            {
                var Label = Expression.Label(ResultType);
                return Expression.Block(
                    Expression.Goto(Label, this.Typeに対応するExpression(深さ, ResultType)),
                    Expression.Label(Label, this.Typeに対応するExpression(深さ, ResultType))
                );
            }
            case ExpressionType.Index:
            {
                return Expression.ArrayAccess(
                    this.Typeに対応するExpression(深さ, ResultType.MakeArrayType()),
                    this.Typeに対応するExpression(深さ, typeof(int))
                );
            }
            case ExpressionType.Invoke:
            {
                var Delegate = this.Typeに対応するExpression(深さ, typeof(Func<>).MakeGenericType(ResultType));
                return Expression.Invoke(
                    Delegate
                );
            }
            //case ExpressionType.Lambda: {
            //    return Expression.Lambda(
            //        Typeに対応するExpression(深さ,Type)
            //    );
            //}
            case ExpressionType.Label:
            case ExpressionType.Loop:
            default:
                goto 再試行;
        }
    }
    private Expression Decimalに対応するExpression(int 深さ)
    {
        var ResultType = typeof(decimal);
    再試行:
        var NodeType = this.選択(this.Leafs);
        switch (NodeType)
        {
            case ExpressionType.Equal:
            case ExpressionType.GreaterThan:
            case ExpressionType.GreaterThanOrEqual:
            case ExpressionType.LessThan:
            case ExpressionType.LessThanOrEqual:
            case ExpressionType.NotEqual:
            {
                return Expression.MakeBinary(NodeType, this.Typeに対応するExpression(深さ, ResultType), this.Typeに対応するExpression(深さ, ResultType));
            }
            case ExpressionType.Assign:
            case ExpressionType.AddAssign:
            case ExpressionType.SubtractAssign:
            case ExpressionType.MultiplyAssign:
            case ExpressionType.DivideAssign:
            case ExpressionType.ModuloAssign:
            case ExpressionType.PostDecrementAssign:
            case ExpressionType.PostIncrementAssign:
            case ExpressionType.PreDecrementAssign:
            case ExpressionType.PreIncrementAssign:
            case ExpressionType.PowerAssign:
            {
                return Expression.MakeBinary(NodeType, Typeに対応するAssignの左辺Expression(深さ, ResultType), this.Typeに対応するExpression(深さ, ResultType));
            }
            case ExpressionType.MemberAccess:
            {
                var Type2 = typeof(MemberClass<decimal>);
                var expression = this.Typeに対応するExpression(深さ, Type2);
                var field = Type2.GetField(nameof(MemberClass<object>.Member));
                return Expression.Field(
                    expression,
                    field
                );
            }
            case ExpressionType.Convert:
            case ExpressionType.ConvertChecked:
            case ExpressionType.TypeIs:
                goto 再試行;
            //case ExpressionType.TypeIs:{
            //    var operand=Typeに対応するExpression(深さ,Type);
            //    var type=typeof(Decimal);
            //    return Expression.MakeUnary(NodeType, operand, type);
            //}
            //case ExpressionType.Not: {
            //    var operand = Typeに対応するExpression(深さ, Type);
            //    return Expression.Not(operand);
            //}
            case ExpressionType.Call:
            {
                var Methods = this.DictionaryTypeMethods[typeof(decimal)];
                var Method = this.選択(Methods);
                if (Method.IsStatic)
                {
                    var Parmaeters = Method.GetParameters();
                    var Arguments = new Expression[Parmaeters.Length];
                    for (var a = 0; a < Parmaeters.Length; a++)
                    {
                        Arguments[a] = this.Typeに対応するExpression(深さ, Parmaeters[a].ParameterType);
                    }
                    return Expression.Call(Method, Arguments);
                }
                else
                {
                    var Object = this.Typeに対応するExpression(深さ, Method.DeclaringType);
                    var Parameters = Method.GetParameters();
                    var Arguments = new Expression[Parameters.Length];
                    for (var a = 0; a < Parameters.Length; a++)
                    {
                        Arguments[a] = this.Typeに対応するExpression(深さ, Parameters[a].ParameterType);
                    }
                    return Expression.Call(Object, Method, Arguments);
                }
            }
            case ExpressionType.New:
            {
                var Constructors = ResultType.GetConstructors();
                if (Constructors.Length == 0)
                {
                    return Expression.New(ResultType);
                }
                else
                {
                    var Constructor = this.選択(Constructors);
                    var Parameters = Constructor.GetParameters();
                    var Arguments = new Expression[Parameters.Length];
                    for (var a = 0; a < Parameters.Length; a++)
                    {
                        Arguments[a] = this.Typeに対応するExpression(深さ, Parameters[a].ParameterType);
                    }
                    return Expression.New(Constructor, Arguments);
                }
            }
            //                case ExpressionType.RuntimeVariables:
            case ExpressionType.Switch:
            case ExpressionType.Try:
            case ExpressionType.TypeEqual:
            case ExpressionType.Conditional:
            {
                var test = this.Typeに対応するExpression(深さ, typeof(decimal));
                var ifTrue = this.Typeに対応するExpression(深さ, ResultType);
                var ifFalse = this.Typeに対応するExpression(深さ, ResultType);
                return Expression.Condition(
                    test,
                    ifTrue,
                    ifFalse
                );
            }
            case ExpressionType.Dynamic:
            case ExpressionType.Goto:
            {
                var Label = Expression.Label(ResultType);
                return Expression.Block(
                    Expression.Goto(Label, this.Typeに対応するExpression(深さ, ResultType)),
                    Expression.Label(Label, this.Typeに対応するExpression(深さ, ResultType))
                );
            }
            case ExpressionType.Index:
            {
                return Expression.ArrayAccess(
                    this.Typeに対応するExpression(深さ, ResultType.MakeArrayType()),
                    this.Typeに対応するExpression(深さ, typeof(int))
                );
            }
            case ExpressionType.Invoke:
            {
                var Delegate = this.Typeに対応するExpression(深さ, typeof(Func<>).MakeGenericType(ResultType));
                return Expression.Invoke(
                    Delegate
                );
            }
            //case ExpressionType.Lambda: {
            //    return Expression.Lambda(
            //        Typeに対応するExpression(深さ,Type)
            //    );
            //}
            //case ExpressionType.Label:
            //case ExpressionType.Loop:
            default:
                goto 再試行;
        }
    }
    private Expression FuncDecimalに対応するExpression(int 深さ)
    {
        var ResultType = typeof(Func<decimal>);
    再試行:
        var NodeType = this.選択(this.Leafs);
        switch (NodeType)
        {
            case ExpressionType.Assign:
            case ExpressionType.AddAssign:
            case ExpressionType.SubtractAssign:
            {
                return Expression.MakeBinary(NodeType, Typeに対応するAssignの左辺Expression(深さ, ResultType), this.Typeに対応するExpression(深さ, ResultType));
            }
            case ExpressionType.MemberAccess:
            {
                var Type2 = typeof(MemberClass<>).MakeGenericType(ResultType);
                var expression = this.Typeに対応するExpression(深さ, Type2);
                var field = Type2.GetField(nameof(MemberClass<object>.Member));
                return Expression.Field(
                    expression,
                    field
                );
            }
            //case ExpressionType.TypeIs:{
            //    var operand=Typeに対応するExpression(深さ,Type);
            //    var type=typeof(Decimal);
            //    return Expression.MakeUnary(NodeType, operand, type);
            //}
            case ExpressionType.Call:
            {
                var Methods = this.DictionaryTypeMethods[ResultType];
                var Method = this.選択(Methods);
                if (Method.IsStatic)
                {
                    var Parmaeters = Method.GetParameters();
                    var Arguments = new Expression[Parmaeters.Length];
                    for (var a = 0; a < Parmaeters.Length; a++)
                    {
                        Arguments[a] = this.Typeに対応するExpression(深さ, Parmaeters[a].ParameterType);
                    }
                    return Expression.Call(Method, Arguments);
                }
                else
                {
                    var Object = this.Typeに対応するExpression(深さ, Method.DeclaringType);
                    var Parameters = Method.GetParameters();
                    var Arguments = new Expression[Parameters.Length];
                    for (var a = 0; a < Parameters.Length; a++)
                    {
                        Arguments[a] = this.Typeに対応するExpression(深さ, Parameters[a].ParameterType);
                    }
                    return Expression.Call(Object, Method, Arguments);
                }
            }
            case ExpressionType.New:
            {
                var Constructor = ResultType.GetConstructors()[0];
                return Expression.New(
                    Constructor,
                    Expression.Constant(
                        typeof(GenerateExpression).GetMethod(
                            nameof(PrivateCall0),
                            BindingFlags.NonPublic | BindingFlags.Static
                        )!.MakeGenericMethod(ResultType.GetGenericArguments()[0])
                    )
                );
            }
            //                case ExpressionType.RuntimeVariables:
            case ExpressionType.Switch:
            case ExpressionType.Try:
            case ExpressionType.TypeEqual:
            case ExpressionType.Conditional:
            {
                var test = this.Typeに対応するExpression(深さ, typeof(bool));
                var ifTrue = this.Typeに対応するExpression(深さ, ResultType);
                var ifFalse = this.Typeに対応するExpression(深さ, ResultType);
                return Expression.Condition(
                    test,
                    ifTrue,
                    ifFalse
                );
            }
            case ExpressionType.Dynamic:
            case ExpressionType.Goto:
            {
                var Label = Expression.Label(ResultType);
                return Expression.Block(
                    Expression.Goto(Label, this.Typeに対応するExpression(深さ, ResultType)),
                    Expression.Label(Label, this.Typeに対応するExpression(深さ, ResultType))
                );
            }
            case ExpressionType.Index:
            {
                return Expression.ArrayAccess(
                    this.Typeに対応するExpression(深さ, ResultType.MakeArrayType()),
                    this.Typeに対応するExpression(深さ, typeof(int))
                );
            }
            case ExpressionType.Invoke:
            {
                var Delegate = this.Typeに対応するExpression(深さ, typeof(Func<>).MakeGenericType(ResultType));
                return Expression.Invoke(
                    Delegate
                );
            }
            default:
                goto 再試行;
        }
    }
    private class 左辺<T>
    {
        public T value;
    }

    private static Expression Typeに対応するAssignの左辺Expression(int 深さ, Type Type)
    {
        return Expression.Field(
            Expression.New(typeof(左辺<>).MakeGenericType(Type)),
            nameof(左辺<int>.value)
        );
    }

    internal Expression Typeに対応するExpression(int 深さ, Type Type)
    {
        if (深さ == this.最大の深さ) { return NodeTypeとTypeに対応する末尾Expression(Type); }
        深さ++;
        if (Type == typeof(bool))
        {
            return this.Booleanに対応するExpression(深さ);
        }
        if (Type == typeof(decimal))
        {
            return this.Decimalに対応するExpression(深さ);
        }
        if (Type == typeof(Func<decimal>))
        {
            return this.FuncDecimalに対応するExpression(深さ);
        }
        {
            return Expression.Default(Type);
        }
    }
}