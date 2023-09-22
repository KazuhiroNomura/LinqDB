using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LinqDB.Helpers;
using LinqDB.Sets;
using LinqDB.Sets.Exceptions;
namespace LinqDB.Optimizers;

partial class Optimizer {
    private class 変換_インラインループ:ReturnExpressionTraverser {
        protected static bool Compare_Defaultを使うべきか(ExpressionType NodeType,Type Type) =>
            NodeType==ExpressionType.LessThan&&Type.GetMethod(op_LessThan) is null||NodeType==ExpressionType.GreaterThan&&Type.GetMethod(op_GreaterThan) is null;
        protected static MethodCallExpression Call(Expression instance,string MethodName) => Expression.Call(
            instance,
            instance.Type.GetMethod(MethodName,Instance_NonPublic_Public)
        );
        protected static MemberExpression Field(Expression instance,string FieldName) => Expression.Field(
            instance,
            instance.Type.GetField(FieldName,Instance_NonPublic_Public)
        );
        protected static readonly ConstructorInfo InvalidOperationException_ctor = typeof(InvalidOperationException).GetConstructor(CommonLibrary.Types_String)!;
        protected static UnaryExpression Throw_ZeroTuple(MethodInfo Method) => Expression.Throw(
            Expression.New(
                InvalidOperationException_ctor,
                Expression.Constant($"{Method.DeclaringType}.{Method}:{CommonLibrary.シーケンスに要素が含まれていません_NoElements}")
            ),
            Method.ReturnType
        );
        protected static readonly UnaryExpression Throw_ZeroTuple_Stdev = Expression.Throw(
            Expression.New(
                InvalidOperationException_ctor,
                Expression.Constant("Stdev(selector):シーケンスに要素が含まれていません")
            )
        );
        protected static readonly UnaryExpression Throw_ZeroTuple_Var = Expression.Throw(
            Expression.New(
                InvalidOperationException_ctor,
                Expression.Constant("Var(selector):シーケンスに要素が含まれていません")
            )
        );
        protected static readonly UnaryExpression Throw_ZeroTuple_Varp = Expression.Throw(
            Expression.New(
                InvalidOperationException_ctor,
                Expression.Constant("Varp(selector):シーケンスに要素が含まれていません")
            )
        );
        protected static UnaryExpression Throwシーケンスに要素が含まれていません(作業配列 作業配列,Type Type,MethodBase Method) => Expression.Throw(
            Expression.New(
                InvalidOperationException_ctor,
                作業配列.Expressions設定(
                    Expression.Constant(Method.ToString())
                )
            ),
            Type
        );
        protected static readonly NewExpression New_ZeroTupleException = Expression.New(
            InvalidOperationException_ctor,
            Expression.Constant(nameof(ExtensionSet.Single))
        );
        protected static(ParameterExpression Parameter,Type Parameter_Type,BinaryExpression Assign) 具象Type(Expression Expression0,string Name,bool 重複除去希望) {
            var Type = Expression0.Type;
            Debug.Assert(Type.IsGenericType);
            if(!Type.IsSealed) {
                Type IOutputSet;
                if(typeof(IOutputSet<>)==Type.GetGenericTypeDefinition()) {
                    Type=typeof(Set<>).MakeGenericType(Type.GetGenericArguments());
                } else if((IOutputSet=Type.GetInterface(CommonLibrary.IOutputSet1_FullName)!) is not null) {
                    if(typeof(IGroupingSet<,>)==Type.GetGenericTypeDefinition()) {
                        Type=typeof(SetGroupingSet<,>).MakeGenericType(Type.GetGenericArguments());
                    } else if(typeof(IGrouping<,>)==Type.GetGenericTypeDefinition()) {
                        Type=typeof(SetGroupingAscList<,>).MakeGenericType(Type.GetGenericArguments());
                    } else {
                        var IGroupingSet = Type.GetInterface(CommonLibrary.IGroupingSet2_FullName);
                        if(IGroupingSet is not null) {
                            Type=typeof(SetGroupingSet<,>).MakeGenericType(IGroupingSet.GetGenericArguments());
                        } else{
                            var IGrouping = Type.GetInterface(CommonLibrary.IGrouping2_FullName);
                            Type=IGrouping is not null
                                ?typeof(SetGroupingAscList<,>).MakeGenericType(IGrouping.GetGenericArguments())
                                :typeof(Set<>).MakeGenericType(IOutputSet.GetGenericArguments());
                        }
                    }
                } else {
                    Debug.Assert(
                        typeof(IEnumerable<>)==Type.GetGenericTypeDefinition()||
                        Type.GetInterface(CommonLibrary.IEnumerable1_FullName) is not null
                    );
                    var IEnumerable = typeof(IEnumerable<>)==Type.GetGenericTypeDefinition()
                        ? Type
                        :Type.GetInterface(CommonLibrary.IEnumerable1_FullName)!;
                    if(重複除去希望||Expression0 is MethodCallExpression MethodCall&&Enumerableメソッドで結果にSetを要求するか(MethodCall)) {
                        Type=typeof(HashSet_VoidAdd<>);
                    } else {
                        Type=typeof(AscList<>);
                    }
                    Type=Type.MakeGenericType(IEnumerable.GetGenericArguments());
                }
            }
            var Parameter = Expression.Parameter(Type,Name);
            return (
                Parameter,
                Type,
                Expression.Assign(
                    Parameter,
                    Expression.New(
                        Type.GetConstructor(Type.EmptyTypes)!
                    )
                )
            );
        }
        private sealed class 判定_指定PrimaryKeyが存在する:VoidExpressionTraverser_Quoteを処理しない {
            private readonly HashSet<string> HashSetProperty_Name = new();
            private ParameterExpression? EntityParameter;
            private bool PrimaryKeyを参照したか;
            private bool Parameterを参照したか;
            private bool 固有を許可する;
            private PropertyInfo? ParameterKey;
            public bool 実行(Expression e,ParameterExpression EntityParameter) {
                this.固有を許可する=true;
                this.PrimaryKeyを参照したか=false;
                this.Parameterを参照したか=false;
                this.EntityParameter=EntityParameter;
                var HashSetProperty_Name = this.HashSetProperty_Name;
                HashSetProperty_Name.Clear();
                var EntityParameter_Type = EntityParameter.Type;
                var PrimaryKey = EntityParameter_Type.GetProperty(nameof(IPrimaryKey<int>.PrimaryKey));
                bool Anonymousか;
                // ReSharper disable once PossibleNullReferenceException
                if(PrimaryKey is not null) {
                    Anonymousか=false;
                    this.ParameterKey=PrimaryKey;
                    foreach(var Property in PrimaryKey.PropertyType.GetProperties()) {
                        HashSetProperty_Name.Add(Property.Name);
                    }
                } else {
                    Anonymousか=EntityParameter_Type.IsAnonymous();
                    this.ParameterKey=null;
                    if(Anonymousか) {
                        foreach(var Property in EntityParameter_Type.GetProperties()) {
                            HashSetProperty_Name.Add(Property.Name);
                        }
                    }
                }
                //PrimaryKeyが出現したら                  true
                //Anonymous&&HashSetフィールド名.LongCountGenericMethodDefinition==0 true
                //その他のType&&Parameterを参照したか     true
                this.Traverse(e);
                return this.固有を許可する&&(this.Parameterを参照したか||this.PrimaryKeyを参照したか||Anonymousか&&HashSetProperty_Name.Count==0);
            }
            protected override void Traverse(Expression e) {
                switch(e.NodeType) {
                    case ExpressionType.Parameter: {
                        if(e==this.EntityParameter) {
                            this.Parameterを参照したか=true;
                        }
                        break;
                    }
                    case ExpressionType.New: {
                        var New = (NewExpression)e;
                        if(e.Type.IsAnonymous()) {
                            foreach(var Argument in New.Arguments) {
                                this.Traverse(Argument);
                            }
                        } else {
                            this.固有を許可する=false;
                        }
                        break;
                    }
                    case ExpressionType.MemberAccess: {
                        var MemberExpression = (MemberExpression)e;
                        if(MemberExpression.Expression==this.EntityParameter) {
                            if(this.ParameterKey is not null&&MemberExpression.Member.MetadataToken==this.ParameterKey.MetadataToken) {
                                this.PrimaryKeyを参照したか=true;
                            } else {
                                this.HashSetProperty_Name.Remove(MemberExpression.Member.Name);
                            }
                        }
                        break;
                    }
                    default: {
                        this.固有を許可する=false;
                        break;
                    }
                }
            }
        }
        private readonly 判定_指定PrimaryKeyが存在する _判定_指定PrimaryKeyが存在する;
        private readonly 変換_旧Parameterを新Expression1 変換_旧Parameterを新Expression1;
        private readonly 変換_旧Parameterを新Expression2 変換_旧Parameterを新Expression2;
        protected readonly ExpressionEqualityComparer ExpressionEqualityComparer;
        public 変換_インラインループ(作業配列 作業配列,ExpressionEqualityComparer ExpressionEqualityComparer,変換_旧Parameterを新Expression1 変換_旧Parameterを新Expression1,変換_旧Parameterを新Expression2 変換_旧Parameterを新Expression2) : base(作業配列) {
            this.ExpressionEqualityComparer=ExpressionEqualityComparer;
            this._判定_指定PrimaryKeyが存在する=new 判定_指定PrimaryKeyが存在する();
            this.変換_旧Parameterを新Expression1=変換_旧Parameterを新Expression1;
            this.変換_旧Parameterを新Expression2=変換_旧Parameterを新Expression2;
        }
        protected int 番号;
        public virtual Expression 実行(Expression Lambda) {
            this.番号=0;
            return this.Traverse(Lambda);
        }
        protected static BinaryExpression DecrementAssign(ParameterExpression Parameter) => Expression.Assign(
            Parameter,
            Expression.Decrement(Parameter)
        );
        protected static BinaryExpression IncrementAssign(Expression Expression) => Expression.Assign(
            Expression,
            Expression.Increment(Expression)
        );
        protected static BinaryExpression AddAssign(Expression Left,Expression Right) => Expression.Assign(
            Left,
            Expression.Add(
                Left,
                Right
            )
        );
        protected static BlockExpression Block_PreIncrementAssign_AddAssign(ParameterExpression Count,Expression Left,Expression Right) => Expression.Block(
            IncrementAssign(Count),
            AddAssign(
                Left,
                Right
            )
        );
        [SuppressMessage("ReSharper","TailRecursiveCall")]
        protected bool 重複除去されているか(Expression Expression)=>Expression is MethodCallExpression MethodCall&&this.重複除去されているか(MethodCall);
        protected bool 重複除去されているか(MethodCallExpression MethodCall) {
            var GenericMethodDefinition = GetGenericMethodDefinition(MethodCall.Method);
            if(Reflection.ExtensionSet.GroupBy_keySelector==GenericMethodDefinition)
                return true;
            if(Reflection.ExtensionSet.GroupBy_keySelector_elementSelector==GenericMethodDefinition)
                return true;
            if(Reflection.ExtensionSet.Except==GenericMethodDefinition)
                return this.重複除去されているか(MethodCall.Arguments[0]);
            if(Reflection.ExtensionSet.Intersect==GenericMethodDefinition)
                return this.重複除去されているか(MethodCall.Arguments[0])||this.重複除去されているか(MethodCall.Arguments[1]);
            if(Reflection.ExtensionSet.Select_selector==GenericMethodDefinition)
                return MethodCall.Arguments[1] is LambdaExpression selector&&this._判定_指定PrimaryKeyが存在する.実行(selector.Body,selector.Parameters[0]);
            if(Reflection.ExtensionSet.SelectMany_selector==GenericMethodDefinition)
                return false;
            if(Reflection.ExtensionSet.Union==GenericMethodDefinition)
                return false;
            if(Reflection.ExtensionSet.Where==GenericMethodDefinition)
                return this.重複除去されているか(MethodCall.Arguments[0]);
            return true;
        }
        protected bool Set結果かつ重複が残っているか(Expression e) => 
            e.Type.GetInterface(CommonLibrary.IOutputSet1_FullName) is not null&&!this.重複除去されているか(e);
        private static bool Enumerableメソッドで結果にSetを要求するか(Expression Expression) {
            if(Expression is MethodCallExpression MethodCall&&typeof(Enumerable)==MethodCall.Method.DeclaringType) {
                var Name = MethodCall.Method.Name;
                if(nameof(Enumerable.SelectMany)==Name)
                    return false;
                if(nameof(Enumerable.Except)==Name||nameof(Enumerable.Intersect)==Name||nameof(Enumerable.Union)==Name)
                    return true;
                if(nameof(Enumerable.Where)==Name)
                    return Enumerableメソッドで結果にSetを要求するか(MethodCall.Arguments[0]);
            }
            return false;
        }
        protected Expression LambdaExpressionを展開1(Expression Lambda,Expression argument1) => Optimizer.LambdaExpressionを展開1(
            Lambda,
            argument1,
            this.変換_旧Parameterを新Expression1
        );
        protected Expression LambdaExpressionを展開2(Expression Lambda,Expression argument1,Expression argument2) {
            if(Lambda is LambdaExpression Lambda2) {
                var Parameters = Lambda2.Parameters;
                return this.変換_旧Parameterを新Expression2.実行(
                    Lambda2.Body,
                    Parameters[0],
                    argument1,
                    Parameters[1],
                    argument2
                );
            }
            return Expression.Invoke(
                Lambda,
                argument1,
                argument2
            );
        }
        private static readonly UnaryExpression Throw_OneTupleException_DUnion = Expression.Throw(
            Expression.New(
                typeof(OneTupleException).GetConstructor(CommonLibrary.Types_String)!,
                Expression.Constant("DUnion")
            )
        );
        [SuppressMessage("ReSharper","UnusedMember.Local")]
        private sealed class SZArrayEnumerator<T> {
            private readonly T[] Array;
            private int Index;
            private readonly int EndIndex;
            public T Current => this.Array[this.Index];
            public SZArrayEnumerator(T[] Array) {
                this.Array=Array;
                this.Index=-1;
                this.EndIndex=Array.Length;
            }
            public bool MoveNext() => this.Index<this.EndIndex&&++this.Index<this.EndIndex;
        }
        protected Expression ループ起点(Expression Expression0,ループの内部処理 ループの内部処理) {
            var 作業配列 = this._作業配列;
            var Expression1 = base.Traverse(Expression0);
            if(Expression0.NodeType==ExpressionType.Assign&&Expression1.NodeType==ExpressionType.Block) {
                return Expression.Assign(
                    ((BinaryExpression)Expression0).Left,
                    Expression1
                );
            }
            var Expression1_Type = Expression1.Type;
            var EnumeratorExpression = Expression1_Type.IsArray
                ? (Expression)Expression.New(
                    作業配列.GetConstructor(
                        作業配列.MakeGenericType(
                            typeof(SZArrayEnumerator<>),
                            IEnumerable1のT(Expression1_Type)
                        ),
                        Expression1_Type
                    ),
                    Expression1
                )
                : Expression.Call(
                    Expression1,
                    Expression1_Type.GetMethod(nameof(IEnumerable.GetEnumerator))??Expression1_Type.GetInterface(CommonLibrary.IEnumerable1_FullName)!.GetMethod(nameof(IEnumerable.GetEnumerator))!
                );
            var GetEnumerator_ReturnType = EnumeratorExpression.Type;
            var 変数名 = $"Setﾟ{this.番号++}ﾟ";
            var Enumerator変数 = Expression.Parameter(
                GetEnumerator_ReturnType,
                $"{変数名}Enumerator"
            );
            var Break = Expression.Label(変数名);
            var Expression本体 = ループの内部処理(
                Expression.Property(
                    Enumerator変数,
                    nameof(IEnumerator.Current)
                )
            );
            return Expression.Block(
                作業配列.Parameters設定(Enumerator変数),
                作業配列.Expressions設定(
                    Expression.Assign(
                        Enumerator変数,
                        EnumeratorExpression
                    ),
                    Expression.Loop(
                        Expression.Block(
                            Expression.IfThenElse(
                                Expression.Call(
                                    Enumerator変数,
                                    GetEnumerator_ReturnType.GetMethod(
                                        nameof(IEnumerator.MoveNext),
                                        Type.EmptyTypes
                                    )??Reflection.IEnumerator.MoveNext
                                ),
                                Default_void,
                                Expression.Break(Break)
                            ),
                            Expression本体
                        ),
                        Break
                    )
                )
            );
        }
        /// <summary>
        /// Except,Distinctで使う
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        private static Type 重複なし作業Type(Expression Expression) {
            Debug.Assert(Expression.Type.IsGenericType);
            var Type = Expression.Type;
            var IEnumerable1 = Type.GetInterface(CommonLibrary.IEnumerable1_FullName);
            if(IEnumerable1 is not null){
                Type=IEnumerable1;
            }else if(Type.IsGenericType&&typeof(IEnumerable<>)==Type.GetGenericTypeDefinition()) {
            }else{
                var IOutputSet1=Type.GetInterface(CommonLibrary.IOutputSet1_FullName);
                if(IOutputSet1 is not null){
                    Type=IOutputSet1;
                } else{
                    throw new NotSupportedException(Type.FullName);
                }
            }
            return typeof(Set<>).MakeGenericType(Type.GetGenericArguments());
        }
        protected delegate Expression ループの内部処理(Expression argument);
        protected Expression ループ展開(Expression Expression,ループの内部処理 ループの内部処理) =>
            Expression is MethodCallExpression MethodCall
                ? this.ループ展開(MethodCall,ループの内部処理)
                : this.ループ起点(Expression,ループの内部処理);
        protected override Expression Quote(UnaryExpression Unary0) => Unary0;
        protected Expression ループ展開(MethodCallExpression MethodCall0,ループの内部処理 ループの内部処理) {
            var Method = MethodCall0.Method;
            var GenericMethodDefinition = GetGenericMethodDefinition(Method);
            var Name = Method.Name;
            var 変数名 = $"{Name}ﾟ{this.番号++}ﾟ";
            var 作業配列 = this._作業配列;
            Debug.Assert(
                nameof(Enumerable.Join)!=Name&&
                nameof(Enumerable.GroupJoin)!=Name&&
                Reflection.ExtensionSet.Update!=GenericMethodDefinition
            );
            var MethodCall0_Arguments = MethodCall0.Arguments;
            switch(Name) {
                case nameof(Enumerable.Cast): {
                    Debug.Assert(Reflection.ExtensionSet.Cast==GenericMethodDefinition||Reflection.ExtensionEnumerable.Cast==GenericMethodDefinition);
                    return this.ループ展開(
                        MethodCall0_Arguments[0],
                        argument => ループの内部処理(
                            Expression.Convert(
                                argument,
                                Method.GetGenericArguments()[0]
                            )
                        )
                    );
                }
                case nameof(Enumerable.DefaultIfEmpty): {
                    Debug.Assert(
                        MethodCall0.Arguments.Count==1
                        &&(
                            Reflection.ExtensionSet.DefaultIfEmpty==GenericMethodDefinition
                            ||
                            Reflection.ExtensionEnumerable.DefaultIfEmpty==GenericMethodDefinition
                        )
                        ||
                        MethodCall0.Arguments.Count==2
                        &&(
                            Reflection.ExtensionSet.DefaultIfEmpty_defaultValue==GenericMethodDefinition
                            ||
                            Reflection.ExtensionEnumerable.DefaultIfEmpty_defaultValue==GenericMethodDefinition
                        )
                    );
                    var 存在したか = Expression.Parameter(
                        typeof(bool),
                        $"{変数名}存在したか"
                    );
                    var Expression1 = this.ループ展開(
                        MethodCall0_Arguments[0],
                        argument => Expression.Block(
                            ループの内部処理(argument),
                            Expression.Assign(存在したか,Constant_true)
                        )
                    );
                    var Block_Expression=作業配列.Expressions設定(
                        Expression.Assign(
                            存在したか,
                            Constant_false
                        ),
                        Expression1,
                        Expression.IfThenElse(
                            存在したか,
                            Default_void,
                            ループの内部処理(
                                MethodCall0.Arguments.Count==1
                                    ?Expression.Default(IEnumerable1のT(MethodCall0.Type))
                                    :this.Traverse(MethodCall0.Arguments[1])
                            )
                        )
                    );
                    return Expression.Block(
                        作業配列.Parameters設定(存在したか),
                        Block_Expression
                    );
                }
                case nameof(Enumerable.Distinct): {
                    var Item_Type = 重複なし作業Type(MethodCall0);
                    var Item = Expression.Parameter(
                        Item_Type,
                        $"{変数名}作業"
                    );
                    Expression Right;
                    if(MethodCall0_Arguments.Count==1) {
                        Debug.Assert(Reflection.ExtensionEnumerable.Distinct0==GenericMethodDefinition);
                        Right=Expression.New(Item_Type);
                    } else {
                        Debug.Assert(Reflection.ExtensionEnumerable.Distinct1==GenericMethodDefinition);
                        var Constructor = 作業配列.GetConstructor(
                            Item_Type,
                            作業配列.MakeGenericType(
                                typeof(IEqualityComparer<>),
                                IEnumerable1のT(MethodCall0.Type)
                            )
                        );
                        Right=Expression.New(
                            Constructor,
                            this.Traverse(MethodCall0_Arguments[1])
                        );
                    }
                    var Expression1 = this.ループ展開(
                        MethodCall0_Arguments[0],
                        argument => Expression.IfThenElse(
                            Expression.Call(
                                Item,
                                Item_Type.GetMethod(nameof(Set<int>.IsAdded),Instance_NonPublic_Public)!,
                                argument
                            ),
                            ループの内部処理(argument),
                            Default_void
                        )
                    );
                    return Expression.Block(
                        作業配列.Parameters設定(Item),
                        作業配列.Expressions設定(
                            Expression.Assign(
                                Item,
                                Right
                            ),
                            Expression1
                        )
                    );
                }
                case nameof(Enumerable.Except): {
                    Debug.Assert(
                        Reflection.ExtensionSet.Except==GenericMethodDefinition||
                        Reflection.ExtensionEnumerable.Except==GenericMethodDefinition||
                        Reflection.ExtensionEnumerable.Except_comparer==GenericMethodDefinition
                    );
                    //Except_comparerの対応が不明
                    var second = MethodCall0_Arguments[1];
                    Type 作業_Type;
                    ParameterExpression 作業;
                    Expression Expression0;
                    if(MethodCall0.Method.DeclaringType==typeof(ExtensionSet)&&ループ展開可能なSetのCall(second) is null) {
                        作業_Type=MethodCall0.Type;
                        作業=Expression.Parameter(
                            作業_Type,
                            $"{変数名}{nameof(作業)}"
                        );
                        Expression0=Expression.Assign(
                            作業,
                            this.Traverse(second)
                        );
                    } else {
                        作業_Type=重複なし作業Type(MethodCall0);
                        作業=Expression.Parameter(
                            作業_Type,
                            $"{変数名}{nameof(作業)}"
                        );
                        NewExpression New;
                        if(Reflection.ExtensionEnumerable.Except_comparer==GenericMethodDefinition){
                            var IEqualityComparer=typeof(IEqualityComparer<>).MakeGenericType(Method.GetGenericArguments());
                            var ctor=作業配列.GetConstructor(作業_Type,IEqualityComparer);
                            New=Expression.New(
                                ctor,
                                this.Traverse(MethodCall0_Arguments[2])
                            );
                        } else{
                            New=Expression.New(作業_Type);
                        }
                        Expression0=Expression.Block(
                            Expression.Assign(
                                作業,
                                New
                            ),
                            this.ループ展開(
                                second,
                                argument1 => Expression.Call(
                                    作業,
                                    作業_Type.GetMethod(nameof(ImmutableSet<int>.InternalAdd),Instance_NonPublic_Public),
                                    argument1
                                )
                            )
                        );
                    }
                    var Expression1 = this.ループ展開(
                        MethodCall0_Arguments[0],
                        argument => Expression.IfThenElse(
                            Expression.Call(
                                作業,
                                作業_Type.GetMethod(nameof(ImmutableSet<int>.InternalContains),Instance_NonPublic_Public),
                                argument
                            ),
                            Default_void,
                            ループの内部処理(argument)
                        )
                    );
                    return Expression.Block(
                        作業配列.Parameters設定(作業),
                        作業配列.Expressions設定(
                            Expression0,
                            Expression1
                        )
                    );
                }
                case nameof(Enumerable.GroupBy): {
                    var SetGroupingSet_GenericTypeDefinition = GenericMethodDefinition.DeclaringType==typeof(ExtensionSet)
                        ? typeof(SetGroupingSet<,>)
                        : typeof(SetGroupingAscList<,>);
                    Expression? elementSelector;
                    Expression? resultSelector;
                    Expression? comparer;
                    if(Reflection.ExtensionEnumerable.GroupBy_keySelector==GenericMethodDefinition||Reflection.ExtensionSet.GroupBy_keySelector==GenericMethodDefinition) {
                        elementSelector=null;
                        resultSelector=null;
                        comparer=null;
                    } else if(Reflection.ExtensionEnumerable.GroupBy_keySelector_comparer==GenericMethodDefinition) {
                        elementSelector=null;
                        resultSelector=null;
                        comparer=this.Traverse(MethodCall0_Arguments[2]);
                    } else if(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector==GenericMethodDefinition||Reflection.ExtensionSet.GroupBy_keySelector_elementSelector==GenericMethodDefinition) {
                        elementSelector=this.Traverse(MethodCall0_Arguments[2]);
                        resultSelector=null;
                        comparer=null;
                    } else if(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector_comparer==GenericMethodDefinition) {
                        elementSelector=this.Traverse(MethodCall0_Arguments[2]);
                        resultSelector=null;
                        comparer=this.Traverse(MethodCall0_Arguments[3]);
                    } else if(Reflection.ExtensionEnumerable.GroupBy_keySelector_resultSelector==GenericMethodDefinition||Reflection.ExtensionSet.GroupBy_keySelector_resultSelector==GenericMethodDefinition) {
                        elementSelector=null;
                        resultSelector=this.Traverse(MethodCall0_Arguments[2]);
                        comparer=null;
                    } else if(Reflection.ExtensionEnumerable.GroupBy_keySelector_resultSelector_comparer==GenericMethodDefinition) {
                        elementSelector=null;
                        resultSelector=this.Traverse(MethodCall0_Arguments[2]);
                        comparer=this.Traverse(MethodCall0_Arguments[3]);
                    } else if(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector_resultSelector==GenericMethodDefinition||Reflection.ExtensionSet.GroupBy_keySelector_elementSelector_resultSelector==GenericMethodDefinition) {
                        elementSelector=this.Traverse(MethodCall0_Arguments[2]);
                        resultSelector=this.Traverse(MethodCall0_Arguments[3]);
                        comparer=null;
                    } else {
                        Debug.Assert(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector_resultSelector_comparer==GenericMethodDefinition);
                        elementSelector=this.Traverse(MethodCall0_Arguments[2]);
                        resultSelector=this.Traverse(MethodCall0_Arguments[3]);
                        comparer=this.Traverse(MethodCall0_Arguments[4]);
                    }
                    var MethodCall0_Arguments_0 = MethodCall0_Arguments[0];
                    var MethodCall0_Arguments_1 = MethodCall0_Arguments[1];
                    var KeyType = MethodCall0_Arguments_1.Type.GetGenericArguments()[1];
                    var Result_Type = 作業配列.MakeGenericType(
                        SetGroupingSet_GenericTypeDefinition,
                        KeyType,
                        elementSelector is not null ? elementSelector.Type.GetGenericArguments()[1] :
                            IEnumerable1のT(MethodCall0_Arguments_0.Type)
                    );
                    var Result = Expression.Parameter(
                        Result_Type,
                        $"{変数名}Result"
                    );
                    NewExpression SetGroupingSetNew;
                    if(comparer is not null) {
                        var Constructor = Result_Type.GetConstructor(
                            作業配列.Types設定(
                                作業配列.MakeGenericType(
                                    typeof(EqualityComparer<>),
                                    KeyType
                                )
                            )
                        );
                        SetGroupingSetNew=Expression.New(
                            Constructor,
                            作業配列.Expressions設定(this.Traverse(comparer))
                        );
                    } else {
                        SetGroupingSetNew=Expression.New(
                            Result_Type.GetConstructor(Type.EmptyTypes)!
                        );
                    }
                    var Expression1ループ = this.ループ展開(
                        MethodCall0_Arguments_0,
                        argument => {
                            if(argument.NodeType==ExpressionType.Parameter) {
                                return Expression.Call(
                                    Result,
                                    Result_Type.GetMethod(nameof(SetGroupingSet<int,int>.AddKeyValue)),
                                    this.LambdaExpressionを展開1(
                                        this.Traverse(MethodCall0_Arguments_1),
                                        argument
                                    ),
                                    elementSelector is not null
                                        ? this.LambdaExpressionを展開1(
                                            this.Traverse(elementSelector),
                                            argument
                                        )
                                        : argument
                                );
                            } else {
                                //次の2つのクエリは同じ結果を生成します。
                                //source.GroupBy(x => x.Id).Select(g => new { Id = g.Key, Count = g.Count() });
                                //source.GroupBy(x => x.Id, (key, g) => new { Id = key, Count = g.Count() });
                                var p = Expression.Parameter(
                                    argument.Type,
                                    $"{変数名}p"
                                );
                                var Key = this.LambdaExpressionを展開1(
                                    this.Traverse(MethodCall0_Arguments_1),
                                    p
                                );
                                //var Element = elementSelector is not null
                                //    ? this.LambdaExpressionを展開1(
                                //        this.Traverse(elementSelector),
                                //        p
                                //    )
                                //    : p;
                                //var Result = resultSelector is not null
                                //    ? this.LambdaExpressionを展開2(
                                //        this.Traverse(resultSelector),
                                //        Key,
                                //        Element
                                //    )
                                //    : Element;
                                var 作業配列1 = 作業配列;
                                return Expression.Block(
                                    作業配列1.Parameters設定(p),
                                    作業配列1.Expressions設定(
                                        Expression.Assign(
                                            p,
                                            argument
                                        ),
                                        Expression.Call(
                                            Result,
                                            Result_Type.GetMethod(nameof(SetGroupingSet<int,int>.AddKeyValue),Instance_NonPublic_Public),
                                            Key,
                                            elementSelector is not null
                                                ? this.LambdaExpressionを展開1(
                                                    this.Traverse(elementSelector),
                                                    p
                                                )
                                                : p
                                        )
                                    )
                                );
                            }
                        }
                    );
                    if(resultSelector is null) {
                        var Expression2ループ = this.ループ展開(
                            Result,
                            ループの内部処理
                        );
                        return Expression.Block(
                            作業配列.Parameters設定(Result),
                            作業配列.Expressions設定(
                                Expression.Assign(
                                    Result,
                                    SetGroupingSetNew
                                ),
                                Expression1ループ,
                                Expression2ループ
                            )
                        );
                    } else {
                        this.ループ展開(
                            Result,
                            argument => {
                                var u = this.LambdaExpressionを展開2(
                                    this.Traverse(resultSelector!),
                                    Expression.Property(
                                        argument,
                                        nameof(ImmutableGroupingSet<int,int>.Key)
                                    ),
                                    argument
                                );
                                var r = ループの内部処理(
                                    u
                                );
                                return r;
                            }
                        );
                        var Expression2ループ = this.ループ展開(
                            Result,
                            argument => ループの内部処理(
                                this.LambdaExpressionを展開2(
                                    this.Traverse(resultSelector!),
                                    Expression.Property(
                                        argument,
                                        nameof(ImmutableGroupingSet<int,int>.Key)
                                    ),
                                    argument
                                )
                            )
                        );
                        return Expression.Block(
                            作業配列.Parameters設定(Result),
                            作業配列.Expressions設定(
                                Expression.Assign(
                                    Result,
                                    SetGroupingSetNew
                                ),
                                Expression1ループ,
                                Expression2ループ
                            )
                        );
                    }
                }
                case nameof(Enumerable.Intersect): {
                    var first = MethodCall0_Arguments[0];
                    //Type 作業_Type;
                    //ParameterExpression 作業;
                    //Expression Expression0,Expression1;
                    if(MethodCall0_Arguments.Count==2) {
                        Debug.Assert(
                            Reflection.ExtensionSet.Intersect==GenericMethodDefinition||
                            Reflection.ExtensionEnumerable.Intersect==GenericMethodDefinition
                        );
                        if(ループ展開可能なSetのCall(first) is not null) {
                            var 作業_Type=MethodCall0.Type;
                            var 作業=Expression.Parameter(
                                作業_Type,
                                $"{変数名}作業"
                            );
                            var Expression0=Expression.Assign(
                                作業,
                                this.Traverse(first)
                            );
                            var Expression2 = this.ループ展開(
                                MethodCall0_Arguments[1],
                                argument => Expression.IfThenElse(
                                    Expression.Call(
                                        作業,
                                        作業_Type.GetMethod(nameof(ImmutableSet<int>.InternalContains),Instance_NonPublic_Public)!,
                                        argument
                                    ),
                                    ループの内部処理(argument),
                                    Default_void
                                )
                            );
                            return Expression.Block(
                                作業配列.Parameters設定(作業),
                                作業配列.Expressions設定(
                                    Expression0,
                                    Expression2
                                )
                            );
                        } else {
                            var 作業_Type=作業配列.MakeGenericType(
                                typeof(Set<>),
                                IEnumerable1のT(MethodCall0.Type)
                            );
                            var 作業=Expression.Parameter(
                                作業_Type,
                                $"{変数名}作業"
                            );
                            var Expression0=Expression.Assign(
                                作業,
                                Expression.New(作業_Type)
                            );
                            var Expression1 = this.ループ展開(
                                first,
                                argument => Expression.Call(
                                    作業,
                                    作業_Type.GetMethod(nameof(Set<int>.InternalAdd),Instance_NonPublic_Public)!,
                                    argument
                                )
                            );
                            var Expression2 = this.ループ展開(
                                MethodCall0_Arguments[1],
                                argument => Expression.IfThenElse(
                                    Expression.Call(
                                        作業,
                                        作業_Type.GetMethod(nameof(ImmutableSet<int>.InternalContains),Instance_NonPublic_Public)!,
                                        argument
                                    ),
                                    ループの内部処理(argument),
                                    Default_void
                                )
                            );
                            return Expression.Block(
                                作業配列.Parameters設定(作業),
                                作業配列.Expressions設定(
                                    Expression0,
                                    Expression1,
                                    Expression2
                                )
                            );
                        }
                    } else {
                        Debug.Assert(
                            MethodCall0_Arguments.Count==3&&
                            Reflection.ExtensionEnumerable.Intersect_comparer==GenericMethodDefinition
                        );
                        var T = IEnumerable1のT(MethodCall0.Type);
                        var 作業_Type=作業配列.MakeGenericType(
                            typeof(HashSet<>),
                            T
                        );
                        var 作業=Expression.Parameter(
                            作業_Type,
                            $"{変数名}作業"
                        );
                        var Constructor = 作業配列.GetConstructor(
                            作業_Type,
                            作業配列.MakeGenericType(
                                typeof(IEqualityComparer<>),
                                T
                            )
                        );
                        var Expression0=Expression.Assign(
                            作業,
                            Expression.New(
                                Constructor,
                                this.Traverse(MethodCall0_Arguments[2])
                            )
                        );
                        var Expression1 = this.ループ展開(
                            first,
                            argument => Expression.Call(
                                作業,
                                作業_Type.GetMethod(nameof(HashSet<int>.Add))!,
                                argument
                            )
                        );
                        var Expression2 = this.ループ展開(
                            MethodCall0_Arguments[1],
                            argument => Expression.IfThenElse(
                                Expression.Call(
                                    作業,
                                    作業_Type.GetMethod(nameof(HashSet<int>.Contains)),
                                    argument
                                ),
                                ループの内部処理(argument),
                                Default_void
                            )
                        );
                        return Expression.Block(
                            作業配列.Parameters設定(作業),
                            作業配列.Expressions設定(
                                Expression0,
                                Expression1,
                                Expression2
                            )
                        );
                    }
                }
                case nameof(Enumerable.OfType): {
                    Debug.Assert(Reflection.ExtensionSet.OfType==GenericMethodDefinition||Reflection.ExtensionEnumerable.OfType==GenericMethodDefinition);
                    var MethodCall0_Arguments_0 = MethodCall0_Arguments[0];
                    return this.ループ展開(
                        MethodCall0_Arguments_0,
                        argument => {
                            var 変換元Type = argument.Type;
                            var 変換先Type = Method.GetGenericArguments()[0];
                            Debug.Assert(変換元Type==IEnumerable1のT(MethodCall0_Arguments_0.Type));
                            var 変換元TypeがNullableか = false;
                            if(
                                (変換元TypeがNullableか=変換元Type.IsNullable())&&変換元Type.GetGenericArguments()[0].IsAssignableFrom(変換先Type)||
                                変換元Type.IsAssignableFrom(変換先Type)
                            ) {
                                return Expression.IfThenElse(
                                    Expression.TypeIs(
                                        argument,
                                        変換先Type
                                    ),
                                    ループの内部処理(
                                        Expression.Convert(
                                            argument,
                                            変換先Type
                                        )
                                    ),
                                    Default_void
                                );
                            } else if(
                                変換元TypeがNullableか&&
                                変換先Type.IsAssignableFrom(変換元Type.GetGenericArguments()[0])
                            ) {
                                return Expression.IfThenElse(
                                    Expression.Property(
                                        argument,
                                        nameof(Nullable<int>.HasValue)
                                    ),
                                    ループの内部処理(
                                        Expression.Convert(
                                            argument,
                                            変換先Type
                                        )
                                    ),
                                    Default_void
                                );
                            } else if(変換先Type.IsAssignableFrom(変換元Type)) {
                                return ループの内部処理(
                                    Expression.Convert(
                                        argument,
                                        変換先Type
                                    )
                                );
                            } else {
                                return Default_void;
                            }
                        }
                    );
                }
                case nameof(Enumerable.Range): {
                    Debug.Assert(Reflection.ExtensionSet.Range==GenericMethodDefinition||Reflection.ExtensionEnumerable.Range==GenericMethodDefinition);
                    var start = Expression.Parameter(
                        typeof(int),
                        $"{変数名}start"
                    );
                    var Count = Expression.Parameter(
                        typeof(int),
                        $"{変数名}Count"
                    );
                    var ループ先頭 = Expression.Label($"{変数名}ループ先頭");
                    var ループ開始 = Expression.Label($"{変数名}ループ開始");
                    var Expression0 = Expression.Assign(
                        start,
                        this.Traverse(MethodCall0_Arguments[0])
                    );
                    var Expression1 = Expression.Assign(
                        Count,
                        this.Traverse(MethodCall0_Arguments[1])
                    );
                    var Expression5 = ループの内部処理(start);
                    return Expression.Block(
                        作業配列.Parameters設定(
                            start,
                            Count
                        ),
                        作業配列.Expressions設定(
                            Expression0,
                            Expression1,
                            Expression.Goto(ループ開始),
                            Expression.Label(ループ先頭),
                            DecrementAssign(Count),
                            Expression5,
                            IncrementAssign(start),
                            Expression.Label(ループ開始),
                            Expression.IfThenElse(
                                Expression.LessThan(
                                    Constant_0,
                                    Count
                                ),
                                Expression.Goto(ループ先頭),
                                Default_void
                            )
                        )
                    );
                }
                case nameof(Enumerable.Repeat): {
                    Debug.Assert(Reflection.ExtensionEnumerable.Repeat==GenericMethodDefinition);
                    var Element = Expression.Parameter(
                        typeof(int),
                        $"{変数名}Element"
                    );
                    var Count = Expression.Parameter(
                        typeof(int),
                        $"{変数名}Count"
                    );
                    var ループ先頭 = Expression.Label($"{変数名}ループ先頭");
                    var ループ開始 = Expression.Label($"{変数名}ループ開始");
                    var Expression0 = Expression.Assign(
                        Element,
                        this.Traverse(MethodCall0_Arguments[0])
                    );
                    var Expression1 = Expression.Assign(
                        Count,
                        this.Traverse(MethodCall0_Arguments[1])
                    );
                    var Expression4 = ループの内部処理(Element);
                    return Expression.Block(
                        作業配列.Parameters設定(
                            Element,
                            Count
                        ),
                        作業配列.Expressions設定(
                            Expression0,
                            Expression1,
                            Expression.Goto(ループ開始),
                            Expression.Label(ループ先頭),
                            Expression.PreDecrementAssign(Count),
                            Expression4,
                            Expression.Label(ループ開始),
                            Expression.IfThenElse(
                                Expression.LessThan(
                                    Constant_0,
                                    Count
                                ),
                                Expression.Goto(ループ先頭),
                                Default_void
                            )
                        )
                    );
                }
                case nameof(Enumerable.Reverse): {
                    Debug.Assert(Reflection.ExtensionEnumerable.Reverse==GenericMethodDefinition);
                    var Arguments_0 = MethodCall0_Arguments[0];
                    var DescList_Type = typeof(DescList<>).MakeGenericType(
                        IEnumerable1(Arguments_0.Type).GetGenericArguments()
                    );
                    var DescList = Expression.Parameter(
                        DescList_Type,
                        $"{変数名}DescList"
                    );
                    var Expression1 = this.ループ展開(
                        Arguments_0,
                        argument => Expression.Call(
                            DescList,
                            DescList_Type.GetMethod(nameof(ICollection<int>.Add)),
                            argument
                        )
                    );
                    var Expression2 = this.ループ展開(
                        DescList,
                        ループの内部処理
                    );
                    return Expression.Block(
                        作業配列.Parameters設定(DescList),
                        作業配列.Expressions設定(
                            Expression.Assign(
                                DescList,
                                Expression.New(DescList_Type)
                            ),
                            Expression1,
                            Expression2
                        )
                    );
                }
                case nameof(Enumerable.Select): {
                    if(Reflection.ExtensionEnumerable.Select_indexSelector==GenericMethodDefinition) {
                        var index = Expression.Parameter(
                            typeof(int),
                            $"{変数名}index"
                        );
                        var Expression1 = this.ループ展開(
                            MethodCall0_Arguments[0],
                            argument => Expression.Block(
                                ループの内部処理(
                                    this.LambdaExpressionを展開2(
                                        this.Traverse(MethodCall0_Arguments[1]),
                                        argument,
                                        index
                                    )
                                ),
                                IncrementAssign(index)
                            )
                        );
                        return Expression.Block(
                            作業配列.Parameters設定(index),
                            作業配列.Expressions設定(
                                Expression.Assign(
                                    index,
                                    Constant_0
                                ),
                                Expression1
                            )
                        );
                    } else {
                        Debug.Assert(
                            Reflection.ExtensionSet.Select_selector==GenericMethodDefinition||
                            Reflection.ExtensionEnumerable.Select_selector==GenericMethodDefinition
                        );
                        return this.ループ展開(
                            MethodCall0_Arguments[0],
                            argument => ループの内部処理(
                                this.LambdaExpressionを展開1(
                                    this.Traverse(MethodCall0_Arguments[1]),
                                    argument
                                )
                            )
                        );
                    }
                }
                case nameof(Enumerable.SelectMany): {
                    if(Reflection.ExtensionEnumerable.SelectMany_indexSelector==GenericMethodDefinition) {
                        var index = Expression.Parameter(
                            typeof(int),
                            $"{変数名}index"
                        );
                        var Expression1 = this.ループ展開(
                            MethodCall0_Arguments[0],
                            argument0 => Expression.Block(
                                this.ループ展開(
                                    this.LambdaExpressionを展開2(
                                        this.Traverse(MethodCall0_Arguments[1]),
                                        argument0,
                                        index
                                    ),
                                    ループの内部処理
                                ),
                                IncrementAssign(index)
                            )
                        );
                        return Expression.Block(
                            作業配列.Parameters設定(index),
                            作業配列.Expressions設定(
                                Expression.Assign(
                                    index,
                                    Constant_0
                                ),
                                Expression1
                            )
                        );
                    } else if(Reflection.ExtensionEnumerable.SelectMany_indexCollectionSelector_resultSelector==GenericMethodDefinition) {
                        var index = Expression.Parameter(
                            typeof(int),
                            $"{変数名}index"
                        );
                        var Expression1 = this.ループ展開(
                            MethodCall0_Arguments[0],
                            argument0 => Expression.Block(
                                this.ループ展開(
                                    this.LambdaExpressionを展開2(
                                        this.Traverse(MethodCall0_Arguments[1]),
                                        argument0,
                                        index
                                    ),
                                    argument1 => ループの内部処理(
                                        this.LambdaExpressionを展開2(
                                            this.Traverse(MethodCall0_Arguments[2]),
                                            argument0,
                                            argument1
                                        )
                                    )
                                ),
                                IncrementAssign(index)
                            )
                        );
                        return Expression.Block(
                            作業配列.Parameters設定(index),
                            作業配列.Expressions設定(
                                Expression.Assign(
                                    index,
                                    Constant_0
                                ),
                                Expression1
                            )
                        );
                    } else if(
                        Reflection.ExtensionSet.SelectMany_selector==GenericMethodDefinition||
                        Reflection.ExtensionEnumerable.SelectMany_selector==GenericMethodDefinition
                    ) {
                        return this.ループ展開(
                            MethodCall0_Arguments[0],
                            argument => this.ループ展開(
                                this.LambdaExpressionを展開1(
                                    this.Traverse(MethodCall0_Arguments[1]),
                                    argument
                                ),
                                ループの内部処理
                            )
                        );
                    } else {
                        Debug.Assert(
                            Reflection.ExtensionSet.SelectMany_collectionSelector_resultSelector==GenericMethodDefinition||
                            Reflection.ExtensionEnumerable.SelectMany_collectionSelector_resultSelector==GenericMethodDefinition
                        );
                        return this.ループ展開(
                            MethodCall0_Arguments[0],
                            source => this.ループ展開(
                                this.LambdaExpressionを展開1(
                                    this.Traverse(MethodCall0_Arguments[1]),
                                    source
                                ),
                                collection => ループの内部処理(
                                    this.LambdaExpressionを展開2(
                                        this.Traverse(MethodCall0_Arguments[2]),
                                        source,
                                        collection
                                    )
                                )
                            )
                        );
                    }
                }
                case nameof(Enumerable.Take): {
                    Debug.Assert(Reflection.ExtensionEnumerable.Take==GenericMethodDefinition);
                    var Count = Expression.Parameter(
                        typeof(int),
                        $"{変数名}Count"
                    );
                    var Label = Expression.Label();
                    var Expression1ループ = this.ループ展開(
                        MethodCall0_Arguments[0],
                        argument => Expression.IfThenElse(
                            Expression.LessThanOrEqual(
                                Constant_0,
                                DecrementAssign(Count)
                            ),
                            ループの内部処理(argument),
                            Expression.Break(Label)
                        )
                    );
                    var Assign = Expression.Assign(
                        Count,
                        this.Traverse(MethodCall0_Arguments[1])
                    );
                    return Expression.Block(
                        作業配列.Parameters設定(Count),
                        作業配列.Expressions設定(
                            Assign,
                            Expression1ループ,
                            Expression.Label(Label)
                        )
                    );
                }
                case nameof(Enumerable.TakeWhile): {
                    Debug.Assert(
                        MethodCall0_Arguments.Count==2
                        &&(
                            Reflection.ExtensionEnumerable.TakeWhile==GenericMethodDefinition
                            ||
                            Reflection.ExtensionEnumerable.TakeWhile_index==GenericMethodDefinition
                        )
                    );
                    if(Reflection.ExtensionEnumerable.TakeWhile==GenericMethodDefinition) {
                        var Label = Expression.Label();
                        var Expression1ループ = this.ループ展開(
                            MethodCall0_Arguments[0],
                            argument => Expression.IfThenElse(
                                this.LambdaExpressionを展開1(
                                    this.Traverse(MethodCall0_Arguments[1]),
                                    argument
                                ),
                                ループの内部処理(argument),
                                Expression.Break(Label)
                            )
                        );
                        return Expression.Block(
                            Expression1ループ,
                            Expression.Label(Label)
                        );
                    } else {
                        Debug.Assert(Reflection.ExtensionEnumerable.TakeWhile_index==GenericMethodDefinition);
                        var index = Expression.Parameter(
                            typeof(int),
                            $"{変数名}index"
                        );
                        var Label = Expression.Label();
                        var Expression1ループ = this.ループ展開(
                            MethodCall0_Arguments[0],
                            argument => Expression.IfThenElse(
                                this.LambdaExpressionを展開2(
                                    this.Traverse(MethodCall0_Arguments[1]),
                                    argument,
                                    index
                                ),
                                Expression.Block(
                                    ループの内部処理(argument),
                                    IncrementAssign(index)
                                ),
                                Expression.Break(Label)
                            )
                        );
                        return Expression.Block(
                            作業配列.Parameters設定(index),
                            作業配列.Expressions設定(
                                Expression.Assign(
                                    index,
                                    Constant_0
                                ),
                                Expression1ループ,
                                Expression.Label(Label)
                            )
                        );
                    }
                }
                case nameof(Enumerable.Union): {
                    Debug.Assert(
                        MethodCall0_Arguments.Count==2
                        &&(
                            Reflection.ExtensionSet.Union==GenericMethodDefinition
                            ||
                            Reflection.ExtensionEnumerable.Union==GenericMethodDefinition
                        )
                        ||
                        MethodCall0_Arguments.Count==3
                        &&
                        Reflection.ExtensionEnumerable.Union_comparer==GenericMethodDefinition
                    );
                    var T = IEnumerable1のT(MethodCall0.Type);
                    var Item_Type = 作業配列.MakeGenericType(
                        typeof(HashSet<>),
                        T
                    );
                    var Item = Expression.Parameter(
                        Item_Type,
                        $"{変数名}作業"
                    );
                    Expression Right;
                    if(MethodCall0_Arguments.Count==2) {
                        Debug.Assert(
                            Reflection.ExtensionSet.Union==GenericMethodDefinition||
                            Reflection.ExtensionEnumerable.Union==GenericMethodDefinition
                        );
                        Right=Expression.New(Item_Type);
                    } else {
                        Debug.Assert(MethodCall0_Arguments.Count==3);
                        var Constructor = 作業配列.GetConstructor(
                            Item_Type,
                            作業配列.MakeGenericType(
                                typeof(IEqualityComparer<>),
                                T
                            )
                        );
                        Right=Expression.New(
                            Constructor,
                            this.Traverse(MethodCall0_Arguments[2])
                        );
                    }
                    var Expression1 = Expression.Assign(
                        Item,
                        Right
                    );
                    ループの内部処理 Delegate = argument => Expression.Call(
                        Item,
                        Item_Type.GetMethod(nameof(HashSet<int>.Add)),
                        argument
                    );
                    var Expression2 = this.ループ展開(
                        MethodCall0_Arguments[0],
                        Delegate
                    );
                    var Expression3 = this.ループ展開(
                        MethodCall0_Arguments[1],
                        Delegate
                    );
                    var Expression4 = this.ループ展開(
                        Item,
                        ループの内部処理
                    );
                    return Expression.Block(
                        作業配列.Parameters設定(Item),
                        作業配列.Expressions設定(
                            Expression1,
                            Expression2,
                            Expression3,
                            Expression4
                        )
                    );
                }
                case nameof(ExtensionSet.DUnion): {
                    Debug.Assert(Reflection.ExtensionSet.DUnion==GenericMethodDefinition);
                    return Expression.Block(
                        this.ループ展開(
                            MethodCall0_Arguments[0],
                            ループの内部処理
                        ),
                        this.ループ展開(
                            MethodCall0_Arguments[1],
                            argument => Expression.IfThenElse(
                                ループの内部処理(argument),
                                Default_void,
                                Throw_OneTupleException_DUnion
                            )
                        )
                    );
                }
                case nameof(ExtensionSet.Delete): {
                    Debug.Assert(Reflection.ExtensionSet.Delete==GenericMethodDefinition);
                    return this.ループ展開(
                        MethodCall0_Arguments[0],
                        argument => {
                            Debug.Assert(MethodCall0_Arguments[1].NodeType!=ExpressionType.Lambda);
                            var p = Expression.Parameter(
                                argument.Type,
                                $"{変数名}p"
                            );
                            Debug.Assert(typeof(Delegate).IsAssignableFrom(MethodCall0_Arguments[1].Type));
                            var Expression1 = Expression.IfThenElse(
                                Expression.Invoke(//Lambda展開出来た場合はWhereに置き換えられている
                                    this.Traverse(MethodCall0_Arguments[1]),
                                    p
                                ),
                                Default_void,
                                ループの内部処理(p)
                            );
                            return Expression.Block(
                                作業配列.Parameters設定(p),
                                作業配列.Expressions設定(
                                    Expression.Assign(
                                        p,
                                        argument
                                    ),
                                    Expression1
                                )
                            );
                        }
                    );
                }
                //case nameof(ExtensionSet.Having):
                case nameof(Enumerable.Where): {
                    if(Reflection.ExtensionEnumerable.Where_index==GenericMethodDefinition) {
                        var index = Expression.Parameter(
                            typeof(int),
                            $"{変数名}index"
                        );
                        var Expression1 = this.ループ展開(
                            MethodCall0_Arguments[0],
                            argument => {
                                if(argument.NodeType==ExpressionType.Parameter) {
                                    return Expression.Block(
                                        Expression.IfThenElse(
                                            this.LambdaExpressionを展開2(
                                                this.Traverse(MethodCall0_Arguments[1]),
                                                argument,
                                                index
                                            ),
                                            ループの内部処理(argument),
                                            Default_void
                                        ),
                                        IncrementAssign(index)
                                    );
                                } else {
                                    var p = Expression.Parameter(
                                        argument.Type,
                                        $"{変数名}argument"
                                    );
                                    var IfThenElse = Expression.IfThenElse(
                                        this.LambdaExpressionを展開2(
                                            this.Traverse(MethodCall0_Arguments[1]),
                                            p,
                                            index
                                        ),
                                        ループの内部処理(p),
                                        Default_void
                                    );
                                    return Expression.Block(
                                        作業配列.Parameters設定(p),
                                        作業配列.Expressions設定(
                                            Expression.Assign(
                                                p,
                                                argument
                                            ),
                                            IfThenElse,
                                            IncrementAssign(index)
                                        )
                                    );
                                }
                            }
                        );
                        return Expression.Block(
                            作業配列.Parameters設定(index),
                            作業配列.Expressions設定(
                                Expression.Assign(
                                    index,
                                    Constant_0
                                ),
                                Expression1
                            )
                        );
                    } else {
                        Debug.Assert(
                            //Reflection.ExtensionSet.Having==GenericMethodDefinition||
                            Reflection.ExtensionSet.Where==GenericMethodDefinition||
                            Reflection.ExtensionEnumerable.Where==GenericMethodDefinition
                        );
                        return this.ループ展開(
                            MethodCall0_Arguments[0],
                            argument => Expression.IfThenElse(
                                this.LambdaExpressionを展開1(
                                    this.Traverse(MethodCall0_Arguments[1]),
                                    argument
                                ),
                                ループの内部処理(argument),
                                Default_void
                            )
                        );
                    }
                }
                default: {
                    if(typeof(Enumerable)==Method.DeclaringType||typeof(ExtensionSet)==Method.DeclaringType) {
                        Debug.Assert(nameof(Enumerable.Join)!=Name&&nameof(Enumerable.GroupJoin)!=Name);
                    }
                    break;
                }
            }
            return this.ループ起点(MethodCall0,ループの内部処理);
        }
    }
}