using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CoverageCS.LinqDB;
using LinqDB.Databases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute
namespace CoverageCS.作りたい仕様確認;

[TestClass]
public class Test_式を自動生成して最適化 : ATest
{
    private readonly Random r=new(1);
    //オペランドの型
    //ラムダ式の深さ1-4
    //ラムダ式の広がり1-4
    [ClassInitialize]
    public static void MyClassInitialize(TestContext testContext)
    {
    }
    private readonly List<Type> 大小比較できるTypes=new()
    {
        typeof(sbyte),typeof(short),typeof(int),typeof(long),
        typeof(byte),typeof(ushort),typeof(uint),typeof(ulong),
        typeof(char),typeof(float),typeof(double),
        typeof(DateTimeOffset),typeof(TimeSpan),typeof(decimal)
    };
    private readonly Type[] 利用可能な基礎Types={
        typeof(sbyte),typeof(short),typeof(int),typeof(long),
        typeof(byte),typeof(ushort),typeof(uint),typeof(ulong),
        typeof(bool),typeof(char),typeof(float),typeof(double),
        typeof(DateTimeOffset),typeof(TimeSpan),typeof(decimal),typeof(string)
    };
    private readonly Type[] FuncTypes={
        typeof(Func<>),typeof(Func<,>),typeof(Func<,,>),typeof(Func<,,,>)
    };
    private T 選択<T>(IList<T> List)=> List.Count==0
        ? default
        : List[this.r.Next(List.Count)];
    private readonly Dictionary<Type, ConstantExpression> ReturnTypeに対応するConstants=new();
    private readonly Dictionary<Type, List<MethodInfo>> ReturnTypeに対応するMethods=new();
    private readonly Dictionary<Type, List<MethodInfo>> ReturnTypeに対応するget_Items=new();
    private readonly Dictionary<Type, List<FieldInfo>> ReturnTypeに対応するFields=new();
    private T ReturnTypeに対応する要素<T>(Dictionary<Type, List<T>> Dictionary, Type Type) where T : class
    {
        if (Dictionary.TryGetValue(Type, out var List))
        {
            return this.選択(List);
        }
        return null;
    }
    private readonly Dictionary<Type, List<PropertyInfo>> DictionaryReturnTypeに対応するProperties=new();
    private readonly Dictionary<Type, List<ExpressionType>> DictionaryReturnTypeに対応するExpressionTypes=new();
    private List<ExpressionType> ReturnTypeに対応するExpressionTypes(Type Type)
    {
        if (Type.IsArray)
        {
            if (this.r.Next(2)==0 || Type.GetArrayRank() > 1)
            {
                return new List<ExpressionType> { ExpressionType.NewArrayBounds };
            }
            return new List<ExpressionType> { ExpressionType.NewArrayInit };
        }
        if (typeof(Delegate).IsAssignableFrom(Type) && !Type.IsAbstract)
        {
            return new List<ExpressionType> { ExpressionType.Lambda };
        }
        if (this.DictionaryReturnTypeに対応するExpressionTypes.TryGetValue(Type, out var ExpressionTypes))
        {
            return ExpressionTypes;
        }
        if (Type.IsByRef)
        {
            return new List<ExpressionType> { ExpressionType.MemberAccess, ExpressionType.Parameter };
        }
        return new List<ExpressionType>{
            ExpressionType.Default
        };
    }
    //private Type ReturnTypeに対応するFuncのExpressionType(Type Type) {
    //    var FuncType=選択(FuncTypes);
    //    var GenericArguments=FuncType.GetGenericArguments();
    //    var GenericArguments_Length_1=GenericArguments.Length-1;
    //    GenericArguments[GenericArguments_Length_1]=Type;
    //    for(var a=0;a<GenericArguments_Length_1;a++) {
    //        GenericArguments[a]=選択(利用可能な基礎Types);
    //    }
    //    return FuncType.MakeGenericType(GenericArguments);
    //}
    public override void MyTestInitialize()
    {
        this.Parameter番号=0;
        var ReturnTypeに対応するExpressionTypes=this.DictionaryReturnTypeに対応するExpressionTypes;
        foreach (var 利用可能な基礎Type in this.利用可能な基礎Types)
        {
            ReturnTypeに対応するExpressionTypes.Add(利用可能な基礎Type, new List<ExpressionType>());
        }
        this.ReturnTypeに対応するConstants.Add(typeof(sbyte), Expression.Constant(Activator.CreateInstance(typeof(sbyte))));
        this.ReturnTypeに対応するConstants.Add(typeof(short), Expression.Constant((short)2));
        this.ReturnTypeに対応するConstants.Add(typeof(int), Expression.Constant(3));
        this.ReturnTypeに対応するConstants.Add(typeof(long), Expression.Constant(4L));
        this.ReturnTypeに対応するConstants.Add(typeof(byte), Expression.Constant((byte)5));
        this.ReturnTypeに対応するConstants.Add(typeof(ushort), Expression.Constant((ushort)6));
        this.ReturnTypeに対応するConstants.Add(typeof(uint), Expression.Constant(7U));
        this.ReturnTypeに対応するConstants.Add(typeof(ulong), Expression.Constant(8UL));
        this.ReturnTypeに対応するConstants.Add(typeof(bool), Expression.Constant(true));
        this.ReturnTypeに対応するConstants.Add(typeof(char), Expression.Constant('a'));
        this.ReturnTypeに対応するConstants.Add(typeof(float), Expression.Constant(11f));
        this.ReturnTypeに対応するConstants.Add(typeof(double), Expression.Constant(12d));
        this.ReturnTypeに対応するConstants.Add(typeof(DateTimeOffset), Expression.Constant(new DateTimeOffset(new DateTime(1913, 1, 2, 3, 4, 5))));
        this.ReturnTypeに対応するConstants.Add(typeof(TimeSpan), Expression.Constant(new TimeSpan(14, 15, 16)));
        this.ReturnTypeに対応するConstants.Add(typeof(decimal), Expression.Constant(15m));
        this.ReturnTypeに対応するConstants.Add(typeof(string), Expression.Constant("16"));
        var 共通ExpressionTypes=new[] {
            ExpressionType.ArrayIndex,    //array[3]
            //ExpressionType.Convert,       //(Int32)x
            //ExpressionType.ConvertChecked,//checked((Int32)x)
            ExpressionType.Conditional,   //x?y:z
            ExpressionType.Constant,      //"_Field"
            ExpressionType.Default,       //default(Int32)
            //ExpressionType.Index,         //array[3,4]
            ExpressionType.Invoke,        //lambda.Invoke
            ExpressionType.MemberAccess,  //_Field.b
            ExpressionType.MemberInit,    //new Point{x=3,y=4}
            ExpressionType.Call,          //Method(3,4)
            ExpressionType.Parameter     //p
        };
        var ビット演算ExpressionTypes=new[] {
            ExpressionType.And,
            ExpressionType.Or,
            ExpressionType.OnesComplement
        };
        //var 加減乗除算ExpressionTypes=new[] {
        //    ExpressionType.Divide,
        //    ExpressionType.Modulo,
        //    ExpressionType.Multiply,
        //    ExpressionType.MultiplyChecked,
        //    ExpressionType.Decrement,
        //    ExpressionType.Increment,
        //    ExpressionType.Negate,
        //    ExpressionType.NegateChecked,
        //    ExpressionType.Add,
        //    ExpressionType.AddChecked,
        //    ExpressionType.Subtract,
        //    ExpressionType.SubtractChecked,
        //    ExpressionType.UnaryPlus
        //};
        var Int32UInt32Int64UInt64ExpressionTypes=new[] {
            ExpressionType.RightShift,
            ExpressionType.LeftShift
        };
        var 論理演算ExpressionTypes=new[] {
            ExpressionType.AndAlso,
            ExpressionType.OrElse,
            ExpressionType.Equal,         //x==y
            ExpressionType.NotEqual,      //x!=y
            ExpressionType.GreaterThan,
            ExpressionType.GreaterThanOrEqual,
            ExpressionType.LessThan,
            ExpressionType.LessThanOrEqual
        };
        //            var dd=~((Char)1);
        var 加減算大小比較できる値型ExpressionTypes=new[] {
            ExpressionType.Unbox
        };
        {
            ReturnTypeに対応するExpressionTypes[typeof(sbyte)].AddRange(共通ExpressionTypes);
            ReturnTypeに対応するExpressionTypes[typeof(short)].AddRange(共通ExpressionTypes);
            ReturnTypeに対応するExpressionTypes[typeof(int)].AddRange(共通ExpressionTypes);
            ReturnTypeに対応するExpressionTypes[typeof(long)].AddRange(共通ExpressionTypes);
            ReturnTypeに対応するExpressionTypes[typeof(ushort)].AddRange(共通ExpressionTypes);
            ReturnTypeに対応するExpressionTypes[typeof(uint)].AddRange(共通ExpressionTypes);
            ReturnTypeに対応するExpressionTypes[typeof(ulong)].AddRange(共通ExpressionTypes);
            ReturnTypeに対応するExpressionTypes[typeof(bool)].AddRange(共通ExpressionTypes);
            ReturnTypeに対応するExpressionTypes[typeof(char)].AddRange(共通ExpressionTypes);
            ReturnTypeに対応するExpressionTypes[typeof(double)].AddRange(共通ExpressionTypes);
            ReturnTypeに対応するExpressionTypes[typeof(decimal)].AddRange(共通ExpressionTypes);
            ReturnTypeに対応するExpressionTypes[typeof(DateTimeOffset)].AddRange(共通ExpressionTypes);
            ReturnTypeに対応するExpressionTypes[typeof(TimeSpan)].AddRange(共通ExpressionTypes);
            ReturnTypeに対応するExpressionTypes[typeof(string)].AddRange(共通ExpressionTypes);
        }
        {
            foreach (var 大小比較できるType in this.大小比較できるTypes)
            {
                ReturnTypeに対応するExpressionTypes[大小比較できるType].AddRange(加減算大小比較できる値型ExpressionTypes);
            }
        }
        {
            ReturnTypeに対応するExpressionTypes[typeof(int)].AddRange(ビット演算ExpressionTypes);
            ReturnTypeに対応するExpressionTypes[typeof(long)].AddRange(ビット演算ExpressionTypes);
            ReturnTypeに対応するExpressionTypes[typeof(uint)].AddRange(ビット演算ExpressionTypes);
            ReturnTypeに対応するExpressionTypes[typeof(ulong)].AddRange(ビット演算ExpressionTypes);
        }
        {
            var ExpressionTypes=共通ExpressionTypes.Concat(加減算大小比較できる値型ExpressionTypes).ToArray();
            ReturnTypeに対応するExpressionTypes[typeof(DateTimeOffset)].AddRange(ExpressionTypes);
            ReturnTypeに対応するExpressionTypes[typeof(TimeSpan)].AddRange(ExpressionTypes);
        }
        {
            ReturnTypeに対応するExpressionTypes[typeof(string)].AddRange(共通ExpressionTypes);
        }
        {
            ReturnTypeに対応するExpressionTypes[typeof(int)].AddRange(Int32UInt32Int64UInt64ExpressionTypes);
            ReturnTypeに対応するExpressionTypes[typeof(uint)].AddRange(Int32UInt32Int64UInt64ExpressionTypes);
            ReturnTypeに対応するExpressionTypes[typeof(long)].AddRange(Int32UInt32Int64UInt64ExpressionTypes);
            ReturnTypeに対応するExpressionTypes[typeof(ulong)].AddRange(Int32UInt32Int64UInt64ExpressionTypes);
        }
        {
            ReturnTypeに対応するExpressionTypes[typeof(bool)].AddRange(論理演算ExpressionTypes);
        }
        var ReturnTypeに対応するMethods=this.ReturnTypeに対応するMethods;
        //var Types=typeof(String).Assembly.ExportedTypes.Where(p=>!p.IsGenericTypeDefinition&&利用可能な基礎Types.Contains(p)).ToArray();
        //foreach(var Method in 
        //    Types.SelectMany(p=>
        //        p.GetMethods().Where(q=>
        //            !q.IsGenericMethodDefinition&&q.GetParameters().All(r=>
        //                利用可能な基礎Types.Contains(r.ParameterType)
        this.大小比較できるTypes.Clear();
        //case ExpressionType.AndAlso: return this.Boolean2項演算子(引数,"op_LogicalAnd");
        //case ExpressionType.OrElse:return this.Boolean2項演算子(引数,"op_LogicalOr");
        //case ExpressionType.Equal:return this.Boolean2項演算子(引数,"op_Equality");
        //case ExpressionType.NotEqual: return this.Boolean2項演算子(引数,"op_Inequality");
        //case ExpressionType.GreaterThan: return this.Boolean2項演算子(引数,"op_GreaterThan");
        //case ExpressionType.GreaterThanOrEqual: return this.Boolean2項演算子(引数,"op_GreaterThanOrEqual");
        //case ExpressionType.LessThan: return this.Boolean2項演算子(引数,"op_LessThan");
        //case ExpressionType.LessThanOrEqual: return this.Boolean2項演算子(引数,"op_LessThanOrEqual");
        //            var Types=typeof(String).Assembly.ExportedTypes.Where(p=>!p.IsGenericTypeDefinition);
        var Types=typeof(Container).Assembly.DefinedTypes.Concat(typeof(string).Assembly.DefinedTypes).Where(p=> !p.IsGenericTypeDefinition).ToArray();
        this.大小比較できるTypes.AddRange(Types.Where(p=> p.GetMethod("op_GreaterThan") !=null && p.GetMethod("op_GreaterThanOrEqual") !=null && p.GetMethod("op_LessThan") !=null && p.GetMethod("op_LessThanOrEqual") !=null));
        foreach (var Type in Types)
        {
            foreach (var Method in
                     Type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).Where(q=>
                         !q.IsGenericMethodDefinition && q.GetParameters().All(r=> !r.ParameterType.IsByRef)
                     )
                    )
            {
                var ReturnType=Method.ReturnType;
                if (Method.Name=="get_Item")
                {
                    if (this.ReturnTypeに対応するget_Items.TryGetValue(ReturnType, out var get_Items))
                    {
                        get_Items.Add(Method);
                    }
                    else
                    {
                        this.ReturnTypeに対応するget_Items.Add(ReturnType, new List<MethodInfo> { Method });
                    }
                }
                else
                {
                    if (ReturnTypeに対応するMethods.TryGetValue(ReturnType, out var Methods))
                    {
                        Methods.Add(Method);
                    }
                    else
                    {
                        ReturnTypeに対応するMethods.Add(ReturnType, new List<MethodInfo> { Method });
                    }
                }
            }
            foreach (var Field in Type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var FieldType=Field.FieldType;
                if (this.ReturnTypeに対応するFields.TryGetValue(FieldType, out var Fields))
                {
                    Fields.Add(Field);
                }
                else
                {
                    this.ReturnTypeに対応するFields.Add(FieldType, new List<FieldInfo> { Field });
                }
            }
            foreach (var Property in Type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var PropertyType=Property.PropertyType;
                if (this.DictionaryReturnTypeに対応するProperties.TryGetValue(PropertyType, out var Properties))
                {
                    Properties.Add(Property);
                }
                else
                {
                    this.DictionaryReturnTypeに対応するProperties.Add(PropertyType, new List<PropertyInfo> { Property });
                }
            }
        }
    }
    private int Parameter番号 {
        get;
        set;
    }
    private UnaryExpression _1項演算子(S引数 引数, string メソッド名)
    {
        var Parameters=引数.Parameters;
        var unaryType=引数.NodeType;
        var オペランドType=引数.オペランドType;
        Type ParameterType_0;
        var op=オペランドType.GetMethod(メソッド名);
        if (op !=null)
        {
            var op_GetParameters=op.GetParameters();
            ParameterType_0=op_GetParameters[0].ParameterType;
        }
        else
        {
            ParameterType_0=オペランドType;
        }
        return Expression.MakeUnary(
            unaryType,
            this.Expression作る(
                Parameters,
                ParameterType_0
            ),
            引数.オペランドType,
            op
        );
    }
    private BinaryExpression Boolean2項演算子(S引数 引数)
    {
        var Parameters=引数.Parameters;
        var binaryType=引数.NodeType;
        var 左右Type=this.選択(this.大小比較できるTypes);
        return Expression.MakeBinary(
            binaryType,
            this.Expression作る(
                Parameters,
                左右Type
            ),
            this.Expression作る(
                Parameters,
                左右Type
            )
        );
    }
    private BinaryExpression _2項演算子(S引数 引数, string メソッド名)
    {
        var Parameters=引数.Parameters;
        var binaryType=引数.NodeType;
        var 左右戻Type=引数.オペランドType;
        Type ParameterType_0, ParameterType_1;
        var op=左右戻Type.GetMethod(メソッド名);
        if (op !=null)
        {
            var op_GetParameters=op.GetParameters();
            ParameterType_0=op_GetParameters[0].ParameterType;
            ParameterType_1=op_GetParameters[1].ParameterType;
        }
        else
        {
            ParameterType_0=左右戻Type;
            ParameterType_1=左右戻Type;
        }
        return Expression.MakeBinary(
            binaryType,
            this.Expression作る(
                Parameters,
                ParameterType_0
            ),
            this.Expression作る(
                Parameters,
                ParameterType_1
            )
        );
    }
    private BinaryExpression _2項演算子(S引数 引数, string メソッド名, Type 右Type)
    {
        var Parameters=引数.Parameters;
        var binaryType=引数.NodeType;
        var 左戻Type=引数.オペランドType;
        Type ParameterType_0;
        var op=左戻Type.GetMethod(メソッド名);
        if (op !=null){
            var op_GetParameters=op.GetParameters();
            ParameterType_0=op_GetParameters[0].ParameterType;
        }else{
            ParameterType_0=左戻Type;
        }
        return Expression.MakeBinary(
            binaryType,
            this.Expression作る(
                Parameters,
                ParameterType_0
            ),
            this.Expression作る(
                Parameters,
                右Type
            )
        );
    }
    private int ノード数;
    //private T ランダムの選択<T>(params T[] array)=>array[r.Next(array.Length)];
    private struct S引数
    {
        public readonly ParameterExpression[] Parameters;
        public readonly ExpressionType NodeType;
        public readonly Type オペランドType;
        public S引数(ParameterExpression[] Parameters, ExpressionType NodeType, Type オペランドType)
        {
            this.Parameters=Parameters;
            this.NodeType=NodeType;
            this.オペランドType=オペランドType;
        }
    }
    private Expression Expression作る(ParameterExpression[] Parameters, Type オペランドType)
    {
        List<ExpressionType> ExpressionTypes;
        if (オペランドType.IsByRef)
        {
            ExpressionTypes=new List<ExpressionType> { ExpressionType.MemberAccess };
            var ElementType=オペランドType.GetElementType();
            if (Parameters.Select(p=> p.Type).Contains(ElementType))
            {
                ExpressionTypes.Add(ExpressionType.Parameter);
            }
        }
        else
        {
            ExpressionTypes=this.ReturnTypeに対応するExpressionTypes(オペランドType);
        }
        return this.Expression作る(Parameters, オペランドType, ExpressionTypes);
    }
    private Expression Expression作る(ParameterExpression[] Parameters, Type オペランドType, List<ExpressionType> ExpressionTypes)
    {
        if (this.ノード数++ > 100) return Expression.Default(オペランドType);
        //var ExpressionTypes=new _List<ExpressionType> {
        //    ExpressionType.MemberAccess,
        //    ExpressionType.ArrayIndex
        //};
        //} else { 
        //    ExpressionTypes=ReturnTypeに対応するExpressionTypes(オペランドType);
        //            var ExpressionTypes=ReturnTypeに対応するExpressionTypes(オペランドType);
    再試行:
        //ExpressionType NodeType;
        //if(オペランドType.IsByRef) {
        //    ExpressionTypes=new _List<ExpressionType> {ExpressionType.para }
        //    var ElementType=オペランドType.GetElementType();
        //    if(Parameters.Select(p=>p.Type).Contains(ElementType)) {
        //        ExpressionTypes.Add(ExpressionType.Parameter);
        //    }
        //} else { 
        //}
        var NodeType=this.選択(ExpressionTypes);
        var 引数=new S引数(Parameters, NodeType, オペランドType);
        // ReSharper disable once SwitchStatementMissingSomeCases
        switch (NodeType)
        {
            case ExpressionType.AndAlso:
            case ExpressionType.OrElse:
                return Expression.MakeBinary(
                    NodeType,
                    this.Expression作る(
                        Parameters,
                        typeof(bool)
                    ),
                    this.Expression作る(
                        Parameters,
                        typeof(bool)
                    )
                );
            case ExpressionType.Equal: return this.Boolean2項演算子(引数);
            case ExpressionType.NotEqual: return this.Boolean2項演算子(引数);
            case ExpressionType.GreaterThan: return this.Boolean2項演算子(引数);
            case ExpressionType.GreaterThanOrEqual: return this.Boolean2項演算子(引数);
            case ExpressionType.LessThan: return this.Boolean2項演算子(引数);
            case ExpressionType.LessThanOrEqual: return this.Boolean2項演算子(引数);

            case ExpressionType.Add:
            case ExpressionType.AddChecked: return this._2項演算子(引数, "op_Addition");
            case ExpressionType.Subtract:
            case ExpressionType.SubtractChecked: return this._2項演算子(引数, "op_Subtraction");
            case ExpressionType.Multiply:
            case ExpressionType.MultiplyChecked: return this._2項演算子(引数, "op_Multiply");
            case ExpressionType.Divide: return this._2項演算子(引数, "op_Division");
            case ExpressionType.And: return this._2項演算子(引数, "op_BitwiseAnd");
            case ExpressionType.Or: return this._2項演算子(引数, "op_BitwiseOr");
            case ExpressionType.ExclusiveOr: return this._2項演算子(引数, "op_ExclusiveOr");
            case ExpressionType.Coalesce:
            case ExpressionType.LeftShift: return this._2項演算子(引数, "op_LeftShift", typeof(int));
            case ExpressionType.Modulo: return this._2項演算子(引数, "op_Modulus", typeof(int));
            case ExpressionType.Power:
            {
                Assert.AreEqual(typeof(double), オペランドType);
                var arguments=new Expression[2];
                arguments[0]=this.Expression作る(Parameters, typeof(double));
                arguments[1]=this.Expression作る(Parameters, typeof(double));
                return Expression.Call(
                    typeof(Math).GetMethod("Pow"),
                    arguments
                );
            }
            case ExpressionType.RightShift: return this._2項演算子(引数, "op_RightShift", typeof(int));
            case ExpressionType.ArrayLength:
                return Expression.ArrayLength(
                    this.Expression作る(
                        Parameters,
                        this.選択(this.利用可能な基礎Types)
                    )
                );
            //case ExpressionType.Convert:
            //case ExpressionType.ConvertChecked: {
            //    var op=オペランドType.GetMethod("op_Implicit");
            //    if(op==null) {
            //        op=オペランドType.GetMethod("op_Explicit");
            //    }
            //    Type ParameterType_0,ParameterType_1;
            //    if(op!=null) {
            //        var op_GetParameters=op.GetParameters();
            //        ParameterType_0=op_GetParameters[0].ParameterType;
            //        ParameterType_1=op_GetParameters[1].ParameterType;
            //    } else {
            //        ParameterType_0=オペランドType;
            //        ParameterType_1=オペランドType;
            //    }
            //    return Expression.MakeUnary(
            //        NodeType,
            //        this.Expression作る(
            //            Parameters,
            //            ParameterTy pe_0
            //        ),
            //        引数.オペランドType,
            //        op
            //    );
            //}
            case ExpressionType.Decrement: return this._1項演算子(引数, "op_Increment");
            case ExpressionType.Increment: return this._1項演算子(引数, "op_Decrement");
            case ExpressionType.IsFalse: return Expression.Not(this.Expression作る(Parameters, オペランドType));
            case ExpressionType.IsTrue: return this.Expression作る(Parameters, オペランドType);
            case ExpressionType.Negate:
            case ExpressionType.NegateChecked: return this._1項演算子(引数, "op_UnaryNegation");
            case ExpressionType.Not: return Expression.Not(this.Expression作る(Parameters, オペランドType));
            case ExpressionType.OnesComplement: return Expression.OnesComplement(this.Expression作る(Parameters, オペランドType));
            case ExpressionType.Quote:
            case ExpressionType.TypeAs: return Expression.TypeAs(this.Expression作る(Parameters, オペランドType), オペランドType);
            case ExpressionType.UnaryPlus: return Expression.UnaryPlus(this.Expression作る(Parameters, オペランドType));
            case ExpressionType.Unbox:
                return Expression.Unbox(
                    Expression.Convert(
                        this.Expression作る(
                            Parameters,
                            オペランドType
                        ),
                        typeof(object)
                    ),
                    オペランドType
                );
            case ExpressionType.Conditional:
            {
                var test=this.Expression作る(Parameters, typeof(bool));
                var ifTrue=this.Expression作る(Parameters, オペランドType);
                var ifFalse=this.Expression作る(Parameters, オペランドType);
                return Expression.Condition(
                    test,
                    ifTrue,
                    ifFalse
                );
            }
            case ExpressionType.Constant: return this.ReturnTypeに対応するConstants[オペランドType];
            case ExpressionType.Default:
            {
                if (オペランドType.IsByRef) goto 再試行;
                return Expression.Default(オペランドType);
            }
            case ExpressionType.ArrayIndex:
            {
                //一次元配列
                var rank=this.r.Next(1, 10);
                var indexer=new Expression[rank];
                for (var a=0; a < rank; a++)
                {
                    indexer[a]=this.Expression作る(Parameters, typeof(int));
                }
                return Expression.ArrayAccess(
                    this.Expression作る(
                        Parameters,
                        オペランドType.MakeArrayType(rank)
                    ),
                    indexer
                );
            }
            case ExpressionType.Index:
            {
                //IndexExpression.NodeType==ExpressionType.Index
                //IndexExpressionの中にPropertyInfoがあった。VBインデクス付きプロパティ？
                //var get_Item=ReturnTypeに対応する要素(ReturnTypeに対応するget_Items,オペランドType);
                //var DeclaringType=get_Item.DeclaringType;
                //var instance=this.Expression作る(Parameters,DeclaringType);
                //var get_Item_GetParameters=get_Item.GetParameters();
                //var arguments=new Expression[get_Item_GetParameters.Length];
                //for(var _Field=0;_Field<arguments.Length;_Field++) {
                //    var ParameterType=get_Item_GetParameters[_Field].ParameterType;
                //    arguments[_Field]=this.Expression作る(Parameters,ParameterType);
                //}
                //return Expression.Property(
                //    instance,
                //    get_Item,
                //    arguments
                //);
                throw new NotSupportedException("ExpressionType.Index");
            }
            case ExpressionType.Invoke:
            {
                //var Types0=オペランドType.GetGenericArguments();
                //var Types1=Types0.Take(Types0.Length-1).ToArray();
                //var arguments=new Expression[Types1.Length];
                //for(var _Field=0;_Field<Types1.Length;_Field++) {
                //    arguments[_Field]=this.Expression作る(Parameters,Types1[_Field]);
                //}
                var FuncType=this.選択(this.FuncTypes);
                //オペランドにLambdaを要求する
                var GenericArguments=FuncType.GetGenericArguments();
                var GenericArguments_Length_1=GenericArguments.Length - 1;
                GenericArguments[GenericArguments_Length_1]=オペランドType;
                var arguments=new Expression[GenericArguments_Length_1];
                for(var a=0;a < GenericArguments_Length_1;a++){
                    GenericArguments[a]=this.選択(this.利用可能な基礎Types);
                    arguments[a]=this.Expression作る(Parameters, GenericArguments[a]);
                }
                return Expression.Invoke(
                    this.Expression作る(
                        Parameters,
                        FuncType.MakeGenericType(GenericArguments)
                    ),
                    arguments
                );
            }
            case ExpressionType.Lambda:
            {
                var Invoke=オペランドType.GetMethod("Invoke")!;
                var Invoke_Parameters=Invoke.GetParameters();
                var Invoke_Parameters_Length=Invoke_Parameters.Length;
                var Parameters1=new ParameterExpression[Invoke_Parameters_Length];
                for (var a=0; a < Invoke_Parameters_Length; a++)
                {
                    Parameters1[a]=Expression.Parameter(Invoke_Parameters[a].ParameterType, this.Parameter番号++.ToString(CultureInfo.CurrentCulture));
                }
                var body=this.Expression作る(Parameters1.Concat(Parameters).ToArray(), Invoke.ReturnType);
                return Expression.Lambda(
                    オペランドType,
                    body,
                    Parameters1
                );
            }
            case ExpressionType.ListInit:
            {
                NewExpression NewExpression;
                var initializers=new Expression[this.r.Next(10)];
                var Constructor=this.選択(オペランドType.GetConstructors());
                if (Constructor !=null)
                {
                    var Constructor_GetParameters=Constructor.GetParameters();
                    var Constructor_GetParameters_Length=Constructor_GetParameters.Length;
                    var arguments=new Expression[Constructor_GetParameters_Length];
                    for (var a=0; a < Constructor_GetParameters_Length; a++)
                    {
                        var Constructor_GetParameter=Constructor_GetParameters[a];
                        var ParameterType=Constructor_GetParameter.ParameterType;
                        arguments[a]=this.Expression作る(Parameters, ParameterType);
                    }
                    NewExpression=Expression.New(Constructor, arguments);
                }
                else
                {
                    NewExpression=Expression.New(オペランドType);
                }
                var initializers_Length=initializers.Length;
                var ElementType=オペランドType.GetElementType();
                for (var a=0; a < initializers_Length; a++)
                {
                    initializers[a]=this.Expression作る(Parameters, ElementType);
                }
                return Expression.ListInit(
                    NewExpression,
                    initializers
                );
            }
            case ExpressionType.MemberAccess:
            {
                if (オペランドType.IsByRef)
                {
                    var Member=this.ReturnTypeに対応する要素(this.ReturnTypeに対応するFields, オペランドType.GetElementType());
                    return Expression.MakeMemberAccess(
                        // ReSharper disable once AssignNullToNotNullAttribute
                        Member.IsStatic ?
                            null :
                            this.Expression作る(
                                Parameters,
                                Member.DeclaringType
                            ),
                        Member
                    );
                }
                else
                {
                    var Member=this.ReturnTypeに対応する要素(this.DictionaryReturnTypeに対応するProperties, オペランドType);
                    if (Member==null) goto 再試行;
                    if (Member.GetGetMethod()==null) goto 再試行;
                    return Expression.MakeMemberAccess(
                        this.Expression作る(
                            Parameters,
                            Member.DeclaringType
                        ),
                        Member
                    );
                }
            }
            case ExpressionType.MemberInit:
            case ExpressionType.Call:
            {
                var Method=this.ReturnTypeに対応する要素(this.ReturnTypeに対応するMethods, オペランドType);
                Expression instance;
                if (Method.IsStatic)
                {
                    instance=null;
                }
                else
                {
                    var DeclaringType=Method.DeclaringType;
                    instance=this.Expression作る(Parameters, DeclaringType);
                }
                var Method_GetParameters=Method.GetParameters();
                var arguments=new Expression[Method_GetParameters.Length];
                for (var a=0; a < arguments.Length; a++)
                {
                    var ParameterType=Method_GetParameters[a].ParameterType;
                    arguments[a]=this.Expression作る(Parameters, ParameterType);
                }
                if (this.ノード数==14)
                {
                }
                return Expression.Call(
                    instance,
                    Method,
                    arguments
                );
            }
            case ExpressionType.NewArrayBounds:
            {
                var 次元=オペランドType.GetArrayRank();
                var bounds=new Expression[次元];
                var ElementType=オペランドType.GetElementType();
                for (var a=0; a < 次元; a++)
                {
                    bounds[a]=this.Expression作る(Parameters, typeof(int));
                }
                return Expression.NewArrayBounds(
                    ElementType,
                    bounds
                );
            }
            case ExpressionType.NewArrayInit:
            {
                //一次元
                if (オペランドType.GetArrayRank() > 1) goto 再試行;
                var 要素数=this.r.Next(10);
                var initializers=new Expression[要素数];
                var ElementType=オペランドType.GetElementType();
                for (var a=0; a < 要素数; a++)
                {
                    initializers[a]=this.Expression作る(Parameters, ElementType);
                }
                return Expression.NewArrayInit(
                    ElementType,
                    initializers
                );
            }
            case ExpressionType.New:
            {
                var constructors=オペランドType.GetConstructors();
                if (constructors.Length > 0)
                {
                    var constructor=constructors[this.r.Next(constructors.Length)];
                    var Parameters2=constructor.GetParameters();
                    var Parameters2_Length=Parameters2.Length;
                    var arguments=new Expression[Parameters2_Length];
                    for (var a=0; a < Parameters2_Length; a++)
                    {
                        var ParameterType=Parameters2[a].ParameterType;
                        arguments[a]=this.Expression作る(Parameters, ParameterType);
                    }
                    return Expression.New(constructor, arguments);
                }
                return Expression.New(オペランドType);
            }
            case ExpressionType.Parameter:
            {
                if (オペランドType.IsByRef)
                {
                    オペランドType=オペランドType.GetElementType();
                    foreach (var Parameter in Parameters)
                    {
                        if (Parameter.Type==オペランドType) return Parameter;
                    }
                }
                else
                {
                    foreach (var Parameter in Parameters)
                    {
                        if (Parameter.Type==オペランドType) return Parameter;
                    }
                }
                goto 再試行;
            }
            case ExpressionType.TypeEqual: throw new NotImplementedException();
            case ExpressionType.TypeIs: throw new NotImplementedException();
            default: throw new NotImplementedException();
        }
    }
    //private List<MemberBinding> Binding作成(ParameterExpression[] Parameters,Type Type) {
    //    var Members=Type.GetMembers().ToList();
    //    var 削除数=r.Next(Members.Count);
    //    for(var a=0;a<削除数;a++) {
    //        Members.RemoveAt(r.Next(Members.Count));
    //    }
    //    var bindings=new List<MemberBinding>();
    //    foreach(var Member in Members) {
    //        var MemberType=Member.MemberType;
    //        if(MemberType==MemberTypes.Field||MemberType==MemberTypes.Property) {
    //            Type Type2;
    //            if(MemberType==MemberTypes.Field) {
    //                var Field=Member as FieldInfo;
    //                Contract.Assert(Field!=null,"Field !=null");
    //                Type2=Field.FieldType;
    //            } else {
    //                var Property=Member as PropertyInfo;
    //                Contract.Assert(Property!=null,"Property !=null");
    //                Type2=Property.PropertyType;
    //            }
    //            if(Type2.IsArray) {
    //                var ElementType=Type2.GetElementType();
    //                var AddMethods=ElementType.GetMethods().Where(p=> p.Name=="AddMethod").ToArray();
    //                var AddMethod=AddMethods[r.Next(AddMethods.Length)];
    //                var initializers_Length=r.Next(10);
    //                var initializers=new ElementInit[initializers_Length];
    //                var ParameterTypes=AddMethod.GetParameters().Select(p=> p.ParameterType).ToArray();
    //                var ParameterTypes_Length=ParameterTypes.Length;
    //                var arguments2=new Expression[ParameterTypes_Length];
    //                for(var b=0;b<initializers_Length;b++) {
    //                    for(var c=0;c<ParameterTypes_Length;c++) {
    //                        var ParameterType=ParameterTypes[c];
    //                        arguments2[c]=this.Expression作る(Parameters,ParameterType);
    //                    }
    //                    initializers[b]=Expression.ElementInit(AddMethod,arguments2);
    //                }
    //                bindings.Add(Expression.ListBind(Member,initializers));
    //            } else if(r.Next(2)==0) {
    //                bindings.Add(Expression.Bind(Member,this.Expression作る(Parameters,Type2)));
    //            } else {
    //                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
    //                Expression.MemberBind(Member,this.Binding作成(Parameters,Type2));
    //            }
    //        }
    //    }
    //    return bindings;
    //}
    public int this[int a, int b]=> 3;
    //[TestMethod]
    //public void Lambdaネスト自動生成() {
    //    var dx=new Int32[3,4];
    //    var e0=Expression.ArrayAccess(
    //        Expression.Constant(new Int32[3]),
    //        Expression.Constant(dx[0,0])
    //    );
    //    Assert.AreEqual(e0.NodeType,ExpressionType.Index);
    //    var e1=Expression.ArrayAccess(
    //        Expression.Constant(new Int32[3,3]),
    //        Expression.Constant(0),
    //        Expression.Constant(1)
    //    );
    //    Assert.AreEqual(e1.NodeType,ExpressionType.Index);
    //    var e2=Expression.ArrayIndex(
    //        Expression.Constant(new Int32[3]),
    //        Expression.Constant(0)
    //    );
    //    Assert.AreEqual(e2.NodeType,ExpressionType.ArrayIndex);
    //    var e3=Expression.ArrayIndex(
    //        Expression.Constant(new Int32[3,3]),
    //        Expression.Constant(0),
    //        Expression.Constant(1)
    //    );
    //    Assert.AreEqual(e3.NodeType,ExpressionType.Call);
    //    //var e4=Expression.Or(
    //    //    Expression.Constant('_Field'),
    //    //    Expression.Constant('b')
    //    //);
    //    var e5=Expression.Or(
    //        Expression.Constant(1),
    //        Expression.Constant(2)
    //    );
    //    var e6=Expression.Call(
    //        Expression.Default(typeof(MemberFilter)),
    //        typeof(MemberFilter).GetMethod("EndInvoke"),
    //        Expression.Default(typeof(IAsyncResult))
    //    );

    //    Expression<Func<Int32,Int32>>fun0=x=>this[2,2];
    //    Assert.AreEqual(fun0.Body.NodeType,ExpressionType.Call);
    //    var 一次元配列=new Int32[3];
    //    Expression<Func<Int32,Int32>>fun1=x=>一次元配列[1];
    //    Assert.AreEqual(fun1.Body.NodeType,ExpressionType.ArrayIndex);
    //    var 多次元配列=new Int32[3,4];
    //    Expression<Func<Int32,Int32>>fun2=x=>多次元配列[2,3];
    //    Assert.AreEqual(fun2.Body.NodeType,ExpressionType.Call);
    //    var D=new Dictionary<Int32,Int32>();
    //    Expression<Func<Int32,Int32>>fun3=x=>D[2];
    //    Assert.AreEqual(fun3.Body.NodeType,ExpressionType.Call);
    //    Expression<Func<Int32,Int32>>fun4=x=>this[1,2];
    //    Assert.AreEqual(fun4.Body.NodeType,ExpressionType.Call);
    //    //Expression<Func<Int32,Int32[,]>>fun5=x=>new Int32[,] { {1,2 },{2,3 } };
    //    Expression<Func<Boolean>>f6=()=>default(MemberFilter).EndInvoke(null);
    //    var c0='a';
    //    var c1='b';
    //    Expression<Func<Int32,Int32>>fun7=x=>c0|c1;
    //    Int32 i32a=3,i32b=4;
    //    Expression<Func<Int32,Int64>>fun8=x=>((Int64)3)|((Int64)3);
    //    Int64 i64a=3,i64b=4;
    //    Expression<Func<Int32,Int64>>fun9=x=>i64a|i64b;
    //    Int16 i16a=3,i16b=4;
    //    Expression<Func<Int32,Int32>>fun10=x=>i16a>>i16b;
    //    Expression<Func<Int32,Int64>>fun11=x=>i64a>>i32b;
    //    Expression<Func<Int32,Int64>>fun12=x=>i64a>>i16b;
    //    var u16b=(UInt16)3;
    //    Expression<Func<Int32,Int64>>fun13=x=>i64a>>u16b;
    //    //Expression<Func<Int32,Char>>fun14=x=>~c0;
    //    Int32 エラーが発生する最も短いseed;
    //    var 短い長さ=Int32.MaxValue;
    //    Expression 短いExpression;
    //    Exception 短いException;
    //    for(var a=6;a<=10;a++) {
    //        this.Parameter番号=0;
    //        this.ノード数=0;
    //        this.r=new Random(a);
    //        var body=this.Expression作る(
    //            new ParameterExpression[0],
    //            選択(利用可能な基礎Types)
    //        );
    //        var Lambda=Expression.Lambda(
    //            body
    //        );
    //        //共通.Equal(expression);
    //        try {
    //            Trace.WriteLine(Optimizer.LambdaExpressionのDebugView(Lambda));
    //            var Delegate=Lambda.Compile();
    //            this._変数Cache.Execute(Lambda);
    //            //Delegate.DynamicInvoke();
    //        } catch(StackOverflowException) {
    //        } catch(NotSupportedException) {
    //        } catch(FreeVariableOverflowException) {
    //        //} catch(Exception e) {
    //        //    var s=Lambda.ToString();
    //        //    if(短い長さ>s.Length) {
    //        //        短い長さ=s.Length;
    //        //        エラーが発生する最も短いseed=_Field;
    //        //        短いException=e;
    //        //        短いExpression=Lambda;
    //        //    }
    //        }
    //    }
    //}
}