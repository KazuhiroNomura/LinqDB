using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Linq = System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LinqDB.Helpers;
using LinqDB.Sets.Exceptions;
using Collections = System.Collections;
using LinqDB.Enumerables;
using LinqDB.Optimizers.VoidExpressionTraverser;
//using LinqDB.Sets;
namespace LinqDB.Optimizers.ReturnExpressionTraverser;
using Generic = Collections.Generic;
using static Common;
internal class 変換_インラインループ:ReturnExpressionTraverser {
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
    //protected static readonly NewExpression New_ZeroTupleException = Expression.New(
    //    InvalidOperationException_ctor,
    //    Expression.Constant(nameof(Sets.ExtensionSet.Single))
    //);
    private static(ParameterExpression Parameter,MethodInfo IsAdded,BinaryExpression Assign) Private具象SetType(Expression Expression0,string ParameterName,Type Type,string MethodName){
        //Debug.Assert(Expression0.Type.IsInterface);
        var ReturnType=Type.MakeGenericType(Expression0.Type.GetGenericArguments());
        var Parameter = Expression.Parameter(ReturnType,ParameterName);
        return (
            Parameter,
            ReturnType.GetMethod(MethodName,Instance_NonPublic_Public)!,
            Expression.Assign(
                Parameter,
                Expression.New(
                    ReturnType.GetConstructor(Type.EmptyTypes)!
                )
            )
        );
    }
    protected static(ParameterExpression Parameter,MethodInfo IsAdded,BinaryExpression Assign) 具象SetType戻り値ありCountあり(Expression Expression0,string ParameterName){
        //Debug.Assert(Expression0.Type.IsInterface);
        return Private具象SetType(Expression0,ParameterName,typeof(Sets.Set<>),nameof(Sets.Set<int>.IsAdded));
    }
    protected static(ParameterExpression Parameter,MethodInfo IsAdded,BinaryExpression Assign) 具象SetType戻り値ありCountなし(Expression Expression0,string ParameterName){
        //Debug.Assert(Expression0.Type.IsInterface);
        return Private具象SetType(Expression0,ParameterName,typeof(Sets.Set<>),nameof(Sets.Set<int>.InternalIsAdded));
    }
    protected static(ParameterExpression Parameter,MethodInfo Add,BinaryExpression Assign) 具象SetType戻り値なしCountあり(Expression Expression0,string ParameterName){
        var Type = Expression0.Type;
        if(typeof(Sets.IEnumerable<>)==Type.GetGenericTypeDefinition()){
            Type=typeof(Sets.Set<>);
        }else{
            Debug.Assert(typeof(Generic.IEnumerable<>)==Type.GetGenericTypeDefinition());
            Type=typeof(List<>);
        }
        return Private具象SetType(Expression0,ParameterName,Type,nameof(Sets.Set<int>.Add));
    }
    //protected static(ParameterExpression Parameter,MethodInfo IsAdded,BinaryExpression Assign) 具象Type(Expression Expression0,string Name,bool 戻り値あり,bool Countあり){
    //    var Type = Expression0.Type;
    //    Debug.Assert(Type.IsInterface);
    //    Type? Interface;
    //    if(typeof(Sets.IEnumerable<>)==Type.GetGenericTypeDefinition()){
    //        Interface=Type;
    //        Type=typeof(Sets.Set<>);
    //    }else{
    //        Debug.Assert(typeof(Generic.IEnumerable<>)==Type.GetGenericTypeDefinition());
    //        Interface=Type;
    //        Type=typeof(List<>);
    //    }
    //    var ReturnType=Type.MakeGenericType(Interface.GetGenericArguments());
    //    string MethodName;
    //    if(戻り値あり) {
    //        if(Countあり) {
    //            MethodName=nameof(Sets.Set<int>.IsAdded);
    //        } else {
    //            MethodName=nameof(Sets.Set<int>.InternalIsAdded);
    //        }
    //    } else {
    //        if(Countあり) {
    //            MethodName=nameof(Sets.Set<int>.Add);
    //        } else {
    //            MethodName=nameof(Sets.Set<int>.InternalAdd);
    //        }
    //    }
    //    var Parameter = Expression.Parameter(ReturnType,Name);
    //    return (
    //        Parameter,
    //        ReturnType.GetMethod(MethodName,Instance_NonPublic_Public)!,
    //        Expression.Assign(
    //            Parameter,
    //            Expression.New(
    //                ReturnType.GetConstructor(Type.EmptyTypes)!
    //            )
    //        )
    //    );
    //}
    private sealed class 判定_指定PrimaryKeyが存在する:VoidExpressionTraverser_Quoteを処理しない {
        private readonly Generic.HashSet<string> Properties = new();
        private ParameterExpression? EntityParameter;
        private bool PrimaryKeyを参照したか;
        private bool Parameterを参照したか;
        private bool ユニークな結果になる;
        private PropertyInfo? ParameterKey;
        public bool 実行(Expression e,ParameterExpression EntityParameter){
            const string IKey="LinqDB.Sets.IKey`1";
            this.ユニークな結果になる=true;
            this.PrimaryKeyを参照したか=false;
            this.Parameterを参照したか=false;
            this.EntityParameter=EntityParameter;
            var Properties = this.Properties;
            Properties.Clear();
            bool Anonymousか;
            var EntityParameter_Type = EntityParameter.Type;
            var Interface=EntityParameter_Type.GetInterface(IKey);
            if(Interface is null){
                Anonymousか=EntityParameter_Type.IsAnonymous();
                this.ParameterKey=null;
                if(Anonymousか)
                    foreach(var Property in EntityParameter_Type.GetProperties())
                        Properties.Add(Property.Name);
            } else{
                Anonymousか=false;
                //var PrimaryKey = Interface.GetProperty(nameof(Sets.IKey<int>.Key));
                var PrimaryKey = EntityParameter_Type.GetProperty(nameof(Sets.IKey<int>.Key));
                Debug.Assert(PrimaryKey is not null);
                this.ParameterKey=PrimaryKey;
                foreach(var Property in PrimaryKey.PropertyType.GetProperties())
                    Properties.Add(Property.Name);
            }
            this.Traverse(e);
            if(this.ユニークな結果になる){
                if(this.Parameterを参照したか) return true;
                if(this.PrimaryKeyを参照したか) return true;
                if(Anonymousか)
                    return Properties.Count==0;
                return false;
            }
            return false;
        }
        protected override void Traverse(Expression Expression) {
            switch(Expression.NodeType) {
                case ExpressionType.Parameter:
                    if(Expression==this.EntityParameter)
                        this.Parameterを参照したか=true;
                    break;
                case ExpressionType.New: {
                    var New = (NewExpression)Expression;
                    if(Expression.Type.IsAnonymousValueTuple())
                        foreach(var Argument in New.Arguments)
                            this.Traverse(Argument);
                    else
                        this.ユニークな結果になる=false;
                    break;
                }
                case ExpressionType.MemberAccess: 
                    var MemberExpression = (MemberExpression)Expression;
                    if(MemberExpression.Expression==this.EntityParameter) 
                        if(this.ParameterKey is not null)
                            if(MemberExpression.Member.MetadataToken==this.ParameterKey.MetadataToken)
                                this.PrimaryKeyを参照したか=true;
                            else
                                this.Properties.Remove(MemberExpression.Member.Name);
                        else
                            this.Properties.Remove(MemberExpression.Member.Name);
                    break;
                default:
                    this.ユニークな結果になる=false;
                    break;
            }
        }
    }
    private readonly 判定_指定PrimaryKeyが存在する _判定_指定PrimaryKeyが存在する;
    private protected readonly 変換_旧Parameterを新Expression1 変換_旧Parameterを新Expression1;
    private readonly 変換_旧Parameterを新Expression2 変換_旧Parameterを新Expression2;
    public 変換_インラインループ(作業配列 作業配列,変換_旧Parameterを新Expression1 変換_旧Parameterを新Expression1,変換_旧Parameterを新Expression2 変換_旧Parameterを新Expression2) : base(作業配列) {
        this._判定_指定PrimaryKeyが存在する=new 判定_指定PrimaryKeyが存在する();
        this.変換_旧Parameterを新Expression1=変換_旧Parameterを新Expression1;
        this.変換_旧Parameterを新Expression2=変換_旧Parameterを新Expression2;
    }
    protected int 番号;
    public virtual Expression 実行(Expression Lambda) {
        this.番号=0;
        return this.Traverse(Lambda);
    }
    private static BinaryExpression DecrementAssign(ParameterExpression Parameter) => Expression.Assign(
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
    protected bool 重複除去されているか(Expression Expression){
        //Debug.Assert(ループ展開可能メソッドか(Expression,out var _));
        if(Expression is MethodCallExpression MethodCall)
            return this.重複除去されているか(MethodCall);
        else return true;
    }
    protected bool 重複除去されているか(MethodCallExpression MethodCall) {
        var Name=MethodCall.Method.Name;
        if(nameof(Sets.ExtensionSet.GroupBy)==Name)
            return true;
        if(nameof(Sets.ExtensionSet.Except)==Name)
            return this.重複除去されているか(MethodCall.Arguments[0]);
        if(nameof(Sets.ExtensionSet.Intersect)==Name)
            return this.重複除去されているか(MethodCall.Arguments[0])||this.重複除去されているか(MethodCall.Arguments[1]);
        if(nameof(Sets.ExtensionSet.Select)==Name){
            if(MethodCall.Arguments[1] is LambdaExpression selector)
                return this._判定_指定PrimaryKeyが存在する.実行(selector.Body,selector.Parameters[0]);
            return false;
        }
        if(nameof(Sets.ExtensionSet.SelectMany)==Name)
            return false;
        if(nameof(Sets.ExtensionSet.Union)==Name)
            return false;
        if(nameof(Sets.ExtensionSet.Where)==Name)
            return this.重複除去されているか(MethodCall.Arguments[0]);
        //Debug.Assert(nameof(Sets.ExtensionSet.Where)==Name);
        return true;
        //return this.重複除去されているか(MethodCall.Arguments[0]);
    }
    //protected bool Set結果かつ重複が残っているか(Expression e){
    //    Debug.Assert(e.Type.IsInterface);
    //    if(e.Type.get.GetInterface(CommonLibrary.Sets_IEnumerable1_FullName) is not null&&!this.重複除去されているか(e);
    //}
    //private static bool Enumerableメソッドで結果にSetを要求するか(Expression Expression) {
    //    if(Expression is MethodCallExpression MethodCall&&typeof(Linq.Enumerable)==MethodCall.Method.DeclaringType) {
    //        var Name = MethodCall.Method.Name;
    //        if(nameof(Linq.Enumerable.SelectMany)==Name)
    //            return false;
    //        if(nameof(Linq.Enumerable.Except)==Name||nameof(Linq.Enumerable.Intersect)==Name||nameof(Linq.Enumerable.Union)==Name)
    //            return true;
    //        if(nameof(Linq.Enumerable.Where)==Name)
    //            return Enumerableメソッドで結果にSetを要求するか(MethodCall.Arguments[0]);
    //    }
    //    return false;
    //}
    private static Expression LambdaExpressionを展開1(Expression Lambda,Expression argument,変換_旧Parameterを新Expression1 変換_旧Parameterを新Expression){
        Debug.Assert(typeof(Delegate).IsAssignableFrom(Lambda.Type));
        if(Lambda is LambdaExpression Lambda1){
            return 変換_旧Parameterを新Expression.実行(
                Lambda1.Body,
                Lambda1.Parameters[0],
                argument
            );
        }
        return Expression.Invoke(Lambda,argument);
    }
    protected Expression LambdaExpressionを展開1(Expression Lambda,Expression argument1) => LambdaExpressionを展開1(
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
        return Expression.Invoke(Lambda,argument1,argument2);
    }
    private static readonly UnaryExpression Throw_OneTupleException_DUnion = Expression.Throw(
        Expression.New(
            typeof(OneTupleException).GetConstructor(CommonLibrary.Types_String)!,
            Expression.Constant("DUnion")
        )
    );
    [SuppressMessage("ReSharper","UnusedMember.Local")]
    private sealed class SZArrayEnumerator<T>{
        private readonly T[] Array;
        private int Index;
        private readonly int EndIndex;
        public T Current{get;private set;}=default!;
        public SZArrayEnumerator(T[] Array) {
            this.Array=Array;
            this.Index=-1;
            this.EndIndex=Array.Length;
        }
        public bool MoveNext(){
            this.Index++;
            if(this.Index>=this.EndIndex) return false;
            this.Current=this.Array[this.Index];
            return true;
        }
    }
    protected Expression ループ起点(Expression Expression0,ループの内部処理 ループの内部処理) {
        var 作業配列 = this.作業配列;
        var Expression1 = base.Traverse(Expression0);
        //if(Expression0.NodeType==ExpressionType.Assign)//代入式を更新したら
        //    if(Expression1.NodeType==ExpressionType.Block)//複雑な展開された
        //        return Expression.Assign(((BinaryExpression)Expression0).Left,Expression1);
        var Expression1_Type = Expression1.Type;
        Expression EnumeratorExpression;
        if(Expression1_Type.IsArray){
            EnumeratorExpression=Expression.New(
                作業配列.GetConstructor(
                    作業配列.MakeGenericType(
                        typeof(SZArrayEnumerator<>),
                        IEnumerable1のT(Expression1_Type)
                    ),
                    Expression1_Type
                ),
                Expression1
            );
        } else{
            var GetEnumerator=Expression1_Type.GetMethod(nameof(Sets.IEnumerable.GetEnumerator));
            // ReSharper disable once ConvertIfStatementToNullCoalescingExpression
            if(GetEnumerator is null)GetEnumerator=Expression1_Type.GetInterface(CommonLibrary.Generic_IEnumerable1_FullName)!.GetMethod(nameof(Sets.IEnumerable.GetEnumerator));
            EnumeratorExpression=Expression.Call(Expression1,GetEnumerator);
        }
        var GetEnumerator_ReturnType = EnumeratorExpression.Type;
        var 変数名 = $"Setﾟ{this.番号++}ﾟ";
        var Enumerator変数 = Expression.Parameter(GetEnumerator_ReturnType,$"{変数名}Enumerator");
        var Break = Expression.Label(変数名);
        var Expression本体 = ループの内部処理(
            Expression.Property(
                Enumerator変数,
                nameof(Collections.IEnumerator.Current)
            )
        );
        return Expression.Block(
            作業配列.Parameters設定(Enumerator変数),
            作業配列.Expressions設定(
                Expression.Assign(Enumerator変数,EnumeratorExpression),
                Expression.Loop(
                    Expression.Block(
                        Expression.IfThenElse(
                            Expression.Call(
                                Enumerator変数,
                                GetEnumerator_ReturnType.GetMethod(
                                    nameof(Collections.IEnumerator.MoveNext),
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
        Debug.Assert(Expression.Type.IsInterface);
        //var Expression_Type= Expression.Type;
        //Type? Type;
        //if(Expression_Type.IsGenericType){
        //    if(typeof(Generic.IEnumerable<>)==Expression_Type.GetGenericTypeDefinition()){
        //    } else if(typeof(Sets.IEnumerable<>)==Expression_Type.GetGenericTypeDefinition()){
        //    }
        //}
        //Type[] GenericArguments;
        //if(typeof(Sets.IEnumerable<>)==Expression_Type.GetGenericTypeDefinition()){
        //    GenericArguments=Expression_Type.GetGenericArguments();
        //} else{
        //    Debug.Assert(typeof(Generic.IEnumerable<>)==Expression_Type.GetGenericTypeDefinition());
        //}
        //if((Type=Expression_Type.GetInterface(CommonLibrary.Sets_IEnumerable1_FullName))is null){
        //    if((Type=Expression_Type.GetInterface(CommonLibrary.Generic_IEnumerable1_FullName)) is null){
        //        if(typeof(Sets.IEnumerable<>)==Expression_Type.GetGenericTypeDefinition()){
        //        } else{
        //            Debug.Assert(typeof(Generic.IEnumerable<>)==Expression_Type.GetGenericTypeDefinition());
        //        }
        //    }
        //    Debug.Assert(Type is not null,$"{Type.FullName}はSets.IEnumerable<>を継承しているはず");
        //}
        //var Generic_IEnumerable1 = Type.GetInterface(CommonLibrary.Generic_IEnumerable1_FullName);
        //if(Generic_IEnumerable1 is not null){
        //    Type=Generic_IEnumerable1;
        //}else if(Type.IsGenericType&&(typeof(Generic.IEnumerable<>)==Type.GetGenericTypeDefinition()||typeof(IEnumerable<>)==Type.GetGenericTypeDefinition())) {
        //}else{
        //    var Sets_IEnumerable1=Type.GetInterface(CommonLibrary.Sets_IEnumerable1_FullName);
        //    Debug.Assert(Sets_IEnumerable1 is not null,$"{Type.FullName}はSets.IEnumerable<>を継承しているはず");
        //    Type=Sets_IEnumerable1;
        //}
        return typeof(Sets.Set<>).MakeGenericType(Expression.Type.GetGenericArguments());
    }
    protected delegate Expression ループの内部処理(Expression argument);
    protected Expression ループ展開(Expression Expression,ループの内部処理 ループの内部処理){
        if(Expression is MethodCallExpression MethodCall)
            return this.ループ展開(MethodCall,ループの内部処理);
        else
            return this.ループ起点(Expression,ループの内部処理);
    }
    private static readonly ConstructorInfo InvalidCastException_ctor=typeof(InvalidCastException).GetConstructor(CommonLibrary.Types_String)!;
    protected Expression ループ展開(MethodCallExpression MethodCall0,ループの内部処理 ループの内部処理) {
        var Method = MethodCall0.Method;
        var GenericMethodDefinition = GetGenericMethodDefinition(Method);
        var Name = Method.Name;
        var 変数名 = $"{Name}ﾟ{this.番号++}ﾟ";
        var 作業配列 = this.作業配列;
        Debug.Assert(
            nameof(Linq.Enumerable.Join)!=Name&&
            nameof(Linq.Enumerable.GroupJoin)!=Name
        );
        var MethodCall0_Arguments = MethodCall0.Arguments;
        switch(Name) {
            case nameof(Linq.Enumerable.Cast          ): {
                Debug.Assert(Reflection.ExtensionSet.Cast==GenericMethodDefinition||Reflection.ExtensionEnumerable.Cast==GenericMethodDefinition);
                return this.ループ展開(
                    MethodCall0_Arguments[0],
                    argument=>{
                        var 変換先Type=Method.GetGenericArguments()[0];
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
                            Expression.Throw(
                                Expression.New(
                                    InvalidCastException_ctor,
                                    Expression.Constant($"Unable to cast object of type '{argument.Type.FullName}' to type '{変換先Type.FullName}'.")
                                ),
                                argument.Type
                            )
                        );
                    }
                );
            }
            case nameof(Linq.Enumerable.DefaultIfEmpty): {
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
            case nameof(Linq.Enumerable.Distinct      ): {
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
                            typeof(Generic.IEqualityComparer<>),
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
                            Item_Type.GetMethod(nameof(Sets.Set<int>.IsAdded))!,
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
            case nameof(Linq.Enumerable.Except        ): {
                Debug.Assert(
                    Reflection.ExtensionSet.Except==GenericMethodDefinition||
                    Reflection.ExtensionEnumerable.Except==GenericMethodDefinition||
                    Reflection.ExtensionEnumerable.Except_comparer==GenericMethodDefinition
                );
                //Except_comparerの対応が不明
                var second = MethodCall0_Arguments[1];
                var 作業_Type=重複なし作業Type(MethodCall0);
                var 作業=Expression.Parameter(
                    作業_Type,
                    $"{変数名}作業"
                );
                Expression Expression0;
                if(MethodCall0.Method.DeclaringType==typeof(Sets.ExtensionSet)&&ループ展開可能なSetのCall(second) is null) {
                    Expression0=Expression.Assign(
                        作業,
                        Expression.Convert(
                            this.Traverse(second),
                            作業_Type
                        )
                    );
                } else {
                    NewExpression New;
                    if(Reflection.ExtensionEnumerable.Except_comparer==GenericMethodDefinition){
                        var IEqualityComparer=typeof(Generic.IEqualityComparer<>).MakeGenericType(Method.GetGenericArguments());
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
                                作業_Type.GetMethod(nameof(Sets.ImmutableSet<int>.InternalIsAdded),Instance_NonPublic_Public),
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
                            作業_Type.GetMethod(nameof(Sets.ImmutableSet<int>.InternalContains),Instance_NonPublic_Public),
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
            case nameof(Linq.Enumerable.GroupBy       ): {
                Type SetGroupingSet_GenericTypeDefinition;
                if(GenericMethodDefinition.DeclaringType==typeof(Sets.ExtensionSet))
                    SetGroupingSet_GenericTypeDefinition=typeof(Sets.SetGroupingSet<,>);
                else
                    SetGroupingSet_GenericTypeDefinition=typeof(Lookup<,>);
                Expression elementSelector;
                //Expression? resultSelector;
                Expression? comparer;
                //if(Reflection.ExtensionEnumerable.GroupBy_keySelector==GenericMethodDefinition||Reflection.ExtensionSet.GroupBy_keySelector==GenericMethodDefinition) {
                //    elementSelector=null;
                //    resultSelector=null;
                //    comparer=null;
                //} else if(Reflection.ExtensionEnumerable.GroupBy_keySelector_comparer==GenericMethodDefinition) {
                //    elementSelector=null;
                //    resultSelector=null;
                //    comparer=this.Traverse(MethodCall0_Arguments[2]);
                //} else 
                if(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector==GenericMethodDefinition||Reflection.ExtensionSet.GroupBy_keySelector_elementSelector==GenericMethodDefinition) {
                    elementSelector=this.Traverse(MethodCall0_Arguments[2]);
                    //resultSelector=null;
                    comparer=null;
                }else{
                    Debug.Assert(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector_comparer==GenericMethodDefinition);
                    elementSelector=this.Traverse(MethodCall0_Arguments[2]);
                    //resultSelector=null;
                    comparer=this.Traverse(MethodCall0_Arguments[3]);
                //} else if(Reflection.ExtensionEnumerable.GroupBy_keySelector_resultSelector==GenericMethodDefinition||Reflection.ExtensionSet.GroupBy_keySelector_resultSelector==GenericMethodDefinition) {
                //    elementSelector=null;
                //    resultSelector=this.Traverse(MethodCall0_Arguments[2]);
                //    comparer=null;
                //} else if(Reflection.ExtensionEnumerable.GroupBy_keySelector_resultSelector_comparer==GenericMethodDefinition) {
                //    elementSelector=null;
                //    resultSelector=this.Traverse(MethodCall0_Arguments[2]);
                //    comparer=this.Traverse(MethodCall0_Arguments[3]);
                //} else if(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector_resultSelector==GenericMethodDefinition||Reflection.ExtensionSet.GroupBy_keySelector_elementSelector_resultSelector==GenericMethodDefinition) {
                //    elementSelector=this.Traverse(MethodCall0_Arguments[2]);
                //    resultSelector=this.Traverse(MethodCall0_Arguments[3]);
                //    comparer=null;
                //} else {
                //    Debug.Assert(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector_resultSelector_comparer==GenericMethodDefinition);
                //    elementSelector=this.Traverse(MethodCall0_Arguments[2]);
                //    resultSelector=this.Traverse(MethodCall0_Arguments[3]);
                //    comparer=this.Traverse(MethodCall0_Arguments[4]);
                }
                var MethodCall0_Arguments_0 = MethodCall0_Arguments[0];
                var MethodCall0_Arguments_1 = MethodCall0_Arguments[1];
                var KeyType = MethodCall0_Arguments_1.Type.GetGenericArguments()[1];
                var Result_Type = 作業配列.MakeGenericType(
                    SetGroupingSet_GenericTypeDefinition,
                    KeyType,
                    elementSelector.Type.GetGenericArguments()[1]
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
                                typeof(Generic.EqualityComparer<>),
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
                //次の2つのクエリは同じ結果を生成します。
                //source.GroupBy(x => x.Id).Select(g => new { Id = g.Key, Count = g.Count() });
                //source.GroupBy(x => x.Id, (key, g) => new { Id = key, Count = g.Count() });
                var Expression1ループ = this.ループ展開(
                    MethodCall0_Arguments_0,
                    argument =>Expression.Call(
                        Result,
                        Result_Type.GetMethod(nameof(Sets.SetGroupingSet<int,int>.AddKeyValue)),
                        this.LambdaExpressionを展開1(
                            this.Traverse(MethodCall0_Arguments_1),
                            argument
                        ),
                        this.LambdaExpressionを展開1(
                            this.Traverse(elementSelector),
                            argument
                        )
                    )
                );
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
            }
            case nameof(Linq.Enumerable.Intersect     ): {
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
                        var 作業=Expression.Parameter(作業_Type,$"{変数名}作業");
                        var Expression0=Expression.Assign(作業,this.Traverse(first));
                        var Expression2 = this.ループ展開(
                            MethodCall0_Arguments[1],
                            argument => Expression.IfThenElse(
                                Expression.Call(
                                    Reflection.ExtensionSet.Contains_value.MakeGenericMethod(作業_Type.GetGenericArguments()),
                                    作業,
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
                            typeof(Sets.Set<>),
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
                                作業_Type.GetMethod(nameof(Sets.Set<int>.InternalIsAdded),Instance_NonPublic_Public)!,
                                argument
                            )
                        );
                        var Expression2 = this.ループ展開(
                            MethodCall0_Arguments[1],
                            argument => Expression.IfThenElse(
                                Expression.Call(
                                    作業,
                                    作業_Type.GetMethod(nameof(Sets.ImmutableSet<int>.InternalContains),Instance_NonPublic_Public)!,
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
                    var 作業_Type=作業配列.MakeGenericType(typeof(Generic.HashSet<>),T);
                    var 作業=Expression.Parameter(作業_Type,$"{変数名}作業");
                    var Constructor = 作業配列.GetConstructor(
                        作業_Type,
                        作業配列.MakeGenericType(
                            typeof(Generic.IEqualityComparer<>),
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
                            作業_Type.GetMethod(nameof(Generic.HashSet<int>.Add))!,
                            argument
                        )
                    );
                    var Expression2 = this.ループ展開(
                        MethodCall0_Arguments[1],
                        argument => Expression.IfThenElse(
                            Expression.Call(
                                作業,
                                作業_Type.GetMethod(nameof(Generic.HashSet<int>.Contains)),
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
            case nameof(Linq.Enumerable.OfType        ): {
                Debug.Assert(Reflection.ExtensionSet.OfType==GenericMethodDefinition||Reflection.ExtensionEnumerable.OfType==GenericMethodDefinition);
                var MethodCall0_Arguments_0 = MethodCall0_Arguments[0];
                return this.ループ展開(
                    MethodCall0_Arguments_0,
                    argument => {
                        var 変換元Type = argument.Type;
                        var 変換先Type = Method.GetGenericArguments()[0];
                        Debug.Assert(変換元Type==IEnumerable1のT(MethodCall0_Arguments_0.Type));
                        int? a=3;
                        var b=(double?)a;
                        if(変換元Type.IsNullable()){
                            if(変換元Type.IsNullable()){

                            }
                        }
                        if(変換元Type.IsAssignableFrom(変換先Type)){
                            //class 変換先:変換元
                            //(変換元)変換先←これは暗黙でできるが(参照型)値型の場合ボクシングの為にConvertが必要
                            if(変換先Type.IsValueType)
                                return ループの内部処理(
                                    Expression.Convert(
                                        argument,
                                        変換先Type
                                    )
                                );
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
                        }else if(変換先Type.IsAssignableFrom(変換元Type)){
                            //class 変換先:変換元
                            //(変換先)変換元
                            return ループの内部処理(
                                Expression.Convert(
                                    argument,
                                    変換先Type
                                )
                            );
                        } else
                            //キャストなし
                            return Default_void;
                        ////(object)int
                        //if(変換先Type.IsValueType){
                        //    if(変換元Type.IsValueType){
                        //        //(struct1)struct0
                        //        return ループの内部処理(
                        //            Expression.Convert(
                        //                argument,
                        //                変換先Type
                        //            )
                        //        );
                        //    } else{
                        //        //(int)object
                        //        return Expression.IfThenElse(
                        //            Expression.TypeIs(
                        //                argument,
                        //                変換先Type
                        //            ),
                        //            ループの内部処理(
                        //                Expression.Convert(
                        //                    argument,
                        //                    変換先Type
                        //                )
                        //            ),
                        //            Default_void
                        //        );
                        //    }
                        //} else{
                        //    if(変換元Type.IsAssignableTo(変換先Type)){
                        //        //(object2)object1
                        //        return Expression.IfThenElse(
                        //            Expression.TypeIs(
                        //                argument,
                        //                変換先Type
                        //            ),
                        //            ループの内部処理(
                        //                Expression.Convert(
                        //                    argument,
                        //                    変換先Type
                        //                )
                        //            ),
                        //            Default_void
                        //        );
                        //    } else{
                        //        return ループの内部処理(
                        //            Expression.Convert(
                        //                argument,
                        //                変換先Type
                        //            )
                        //        );
                        //    }
                        //}
                        //if(変換先Type.IsValueType){
                        //} else{
                        //    var t=Expression.Parameter(変換先Type,"OfType");
                        //    return Expression.Block(
                        //        作業配列.Parameters設定(t),
                        //        Expression.Assign(
                        //            t,
                        //            Expression.TypeAs(
                        //                argument,
                        //                変換先Type
                        //            )
                        //        ),
                        //        Expression.IfThenElse(
                        //            Expression.NotEqual(
                        //                t,
                        //                Constant_null
                        //            ),
                        //            ループの内部処理(t),
                        //            Default_void
                        //        )
                        //    );
                        //}
                        //if(
                        //    変換元Type.IsNullable()&&変換元Type.GetGenericArguments()[0].IsAssignableFrom(変換先Type)||
                        //    変換元Type.IsAssignableFrom(変換先Type)
                        //) {
                        //    //int? is object
                        //    var t=Expression.Parameter(変換先Type,"OfType");
                        //    return Expression.Block(
                        //        作業配列.Parameters設定(t),
                        //        Expression.Assign(
                        //            t,
                        //            Expression.TypeAs(
                        //                argument,
                        //                変換先Type
                        //            )
                        //        ),
                        //        Expression.IfThenElse(
                        //            Expression.Equal(
                        //                t,
                        //                Constant_null
                        //            ),
                        //            ループの内部処理(t),
                        //            Default_void
                        //        )
                        //    );
                        //} else{

                        //}
                        //if(変換元Type.IsNullable()&&変換先Type.IsAssignableFrom(変換元Type.GetGenericArguments()[0])) {
                        //    //int? is object
                        //    return Expression.IfThenElse(
                        //        Expression.Property(
                        //            argument,
                        //            nameof(Nullable<int>.HasValue)
                        //        ),
                        //        ループの内部処理(
                        //            Expression.Convert(
                        //                argument,
                        //                変換先Type
                        //            )
                        //        ),
                        //        Default_void
                        //    );
                        //}else{
                        //    Debug.Assert(変換先Type.IsAssignableFrom(変換元Type));
                        //    return ループの内部処理(
                        //        Expression.Convert(
                        //            argument,
                        //            変換先Type
                        //        )
                        //    );
                        ////} else {
                        ////    return Default_void;
                        //}
                    }
                );
            }
            case nameof(Linq.Enumerable.Range         ): {
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
            case nameof(Linq.Enumerable.Repeat        ): {
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
            case nameof(Linq.Enumerable.Reverse       ): {
                Debug.Assert(Reflection.ExtensionEnumerable.Reverse==GenericMethodDefinition);
                var Arguments_0 = MethodCall0_Arguments[0];
                var DescList_Type=typeof(DescList<>).MakeGenericType(Method.GetGenericArguments());
                var DescList = Expression.Parameter(
                    DescList_Type,
                    $"{変数名}DescList"
                );
                var Expression1 = this.ループ展開(
                    Arguments_0,
                    argument => Expression.Call(
                        DescList,
                        DescList_Type.GetMethod(nameof(Generic.ICollection<int>.Add)),
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
            case nameof(Linq.Enumerable.Select        ): {
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
            case nameof(Linq.Enumerable.SelectMany    ): {
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
                //} else if(Reflection.ExtensionEnumerable.SelectMany_indexCollectionSelector_resultSelector==GenericMethodDefinition) {
                //    var index = Expression.Parameter(
                //        typeof(int),
                //        $"{変数名}index"
                //    );
                //    var Expression1 = this.ループ展開(
                //        MethodCall0_Arguments[0],
                //        argument0 => Expression.Block(
                //            this.ループ展開(
                //                this.LambdaExpressionを展開2(
                //                    this.Traverse(MethodCall0_Arguments[1]),
                //                    argument0,
                //                    index
                //                ),
                //                argument1 => ループの内部処理(
                //                    this.LambdaExpressionを展開2(
                //                        this.Traverse(MethodCall0_Arguments[2]),
                //                        argument0,
                //                        argument1
                //                    )
                //                )
                //            ),
                //            IncrementAssign(index)
                //        )
                //    );
                //    return Expression.Block(
                //        作業配列.Parameters設定(index),
                //        作業配列.Expressions設定(
                //            Expression.Assign(
                //                index,
                //                Constant_0
                //            ),
                //            Expression1
                //        )
                //    );
                }else{
                    Debug.Assert(
                        Reflection.ExtensionSet.SelectMany_selector==GenericMethodDefinition||
                        Reflection.ExtensionEnumerable.SelectMany_selector==GenericMethodDefinition
                    );
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
                //} else {
                //    Debug.Assert(
                //        Reflection.ExtensionSet.SelectMany_collectionSelector_resultSelector==GenericMethodDefinition||
                //        Reflection.ExtensionEnumerable.SelectMany_collectionSelector_resultSelector==GenericMethodDefinition
                //    );
                //    return this.ループ展開(
                //        MethodCall0_Arguments[0],
                //        source => this.ループ展開(
                //            this.LambdaExpressionを展開1(
                //                this.Traverse(MethodCall0_Arguments[1]),
                //                source
                //            ),
                //            collection => ループの内部処理(
                //                this.LambdaExpressionを展開2(
                //                    this.Traverse(MethodCall0_Arguments[2]),
                //                    source,
                //                    collection
                //                )
                //            )
                //        )
                //    );
                }
            }
            case nameof(Linq.Enumerable.Take          ): {
                var Label=Expression.Label();
                var Expression3=Expression.Label(Label);
                if(Reflection.ExtensionEnumerable.Take_count==GenericMethodDefinition){
                    var Count=Expression.Parameter(
                        typeof(int),
                        $"{変数名}Count"
                    );
                    var Expression1=this.ループ展開(
                        MethodCall0_Arguments[0],
                        argument=>Expression.IfThenElse(
                            Expression.LessThanOrEqual(
                                Constant_0,
                                DecrementAssign(Count)
                            ),
                            ループの内部処理(argument),
                            Expression.Break(Label)
                        )
                    );
                    var Expression0=Expression.Assign(
                        Count,
                        this.Traverse(MethodCall0_Arguments[1])
                    );
                    return Expression.Block(
                        作業配列.Parameters設定(Count),
                        作業配列.Expressions設定(
                            Expression0,
                            Expression1,
                            Expression3
                        )
                    );
                } else{
                    Debug.Assert(Reflection.ExtensionEnumerable.Take_range==GenericMethodDefinition);
                    var Count=Expression.Parameter(
                        typeof(int),
                        $"{変数名}Count"
                    );
                    var Range=Expression.Parameter(
                        typeof(Range),
                        $"{変数名}Range"
                    );
                    var Expression0=Expression.Assign(Count,Constant_0);
                    var Expression2=this.ループ展開(
                        MethodCall0_Arguments[0],
                        argument=>Expression.IfThenElse(
                            Expression.AndAlso(
                                Expression.LessThanOrEqual(
                                    Expression.Property(
                                        Expression.Property(Range,Reflection.Range.Start),
                                        Reflection.Index.Value
                                    ),
                                    Count
                                ),
                                Expression.LessThan(
                                    Count,
                                    Expression.Property(
                                        Expression.Property(Range,Reflection.Range.End),
                                        Reflection.Index.Value
                                    )
                                )
                            ),
                            ループの内部処理(argument),
                            Expression.Break(Label)
                        )
                    );
                    var Expression1=Expression.Assign(
                        Range,
                        this.Traverse(MethodCall0_Arguments[1])
                    );
                    return Expression.Block(
                        作業配列.Parameters設定(Count,Range),
                        作業配列.Expressions設定(
                            Expression0,
                            Expression1,
                            Expression2,
                            Expression3
                        )
                    );
                }
            }
            case nameof(Linq.Enumerable.TakeWhile     ): {
                Debug.Assert(
                    MethodCall0_Arguments.Count==2
                    &&(
                        Reflection.ExtensionEnumerable.TakeWhile==GenericMethodDefinition
                        ||
                        Reflection.ExtensionEnumerable.TakeWhile_index==GenericMethodDefinition
                    )
                );
                var Label = Expression.Label();
                var Break=Expression.Break(Label);
                var Expression2=Expression.Label(Label);
                if(Reflection.ExtensionEnumerable.TakeWhile==GenericMethodDefinition) {
                    var Expression1ループ = this.ループ展開(
                        MethodCall0_Arguments[0],
                        argument => Expression.IfThenElse(
                            this.LambdaExpressionを展開1(
                                this.Traverse(MethodCall0_Arguments[1]),
                                argument
                            ),
                            ループの内部処理(argument),
                            Break
                        )
                    );
                    return Expression.Block(
                        Expression1ループ,
                        Expression2
                    );
                } else {
                    Debug.Assert(Reflection.ExtensionEnumerable.TakeWhile_index==GenericMethodDefinition);
                    var index = Expression.Parameter(
                        typeof(int),
                        $"{変数名}index"
                    );
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
                            Break
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
                            Expression2
                        )
                    );
                }
            }
            case nameof(Linq.Enumerable.Union         ): {
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
                //Setsを使わずGenericを使った理由はIEqualityComparerを引数に取るものがそれだから
                var Item_Type = 作業配列.MakeGenericType(typeof(Generic.HashSet<>),T);
                var Item = Expression.Parameter(Item_Type,$"{変数名}作業");
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
                        作業配列.MakeGenericType(typeof(Generic.IEqualityComparer<>),T)
                    );
                    Right=Expression.New(
                        Constructor,
                        this.Traverse(MethodCall0_Arguments[2])
                    );
                }
                var Expression1 = Expression.Assign(Item,Right);
                ループの内部処理 Delegate = argument => Expression.Call(
                    Item,
                    Item_Type.GetMethod(nameof(Sets.HashSet<int>.Add)),
                    argument
                );
                var Expression2 = this.ループ展開(MethodCall0_Arguments[0],Delegate);
                var Expression3 = this.ループ展開(MethodCall0_Arguments[1],Delegate);
                var Expression4 = this.ループ展開(Item,ループの内部処理);
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
            case nameof(Sets.ExtensionSet.DUnion           ): {
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
            case nameof(Linq.Enumerable.Where         ): {
                if(Reflection.ExtensionEnumerable.Where_index==GenericMethodDefinition) {
                    var index = Expression.Parameter(typeof(int),$"{変数名}index");
                    var Expression1 = this.ループ展開(
                        MethodCall0_Arguments[0],
                        argument =>Expression.Block(
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
                        )
                    );
                    return Expression.Block(
                        作業配列.Parameters設定(index),
                        作業配列.Expressions設定(
                            Expression.Assign(index,Constant_0),
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
            //default: {
            //    if(typeof(Linq.Enumerable)==Method.DeclaringType||typeof(Sets.ExtensionSet)==Method.DeclaringType) {
            //        Debug.Assert(nameof(Linq.Enumerable.Join)!=Name&&nameof(Linq.Enumerable.GroupJoin)!=Name);
            //    }
            //    break;
            //}
        }
        return this.ループ起点(MethodCall0,ループの内部処理);
    }
}
