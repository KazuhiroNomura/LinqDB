using System;
using System.Linq.Expressions;
using LinqDB.Helpers;
using LinqDB.Reflection;
namespace LinqDB.Optimizers.ReturnExpressionTraverser;
using static CommonLibrary;
using static Common;
/// <summary>
/// Join(,o=>new{o1=o+1,o2=o+2},i=>new{i1=i+1,i2=i+2},(o,i)=>new{o,i})
/// ↓
/// Join(,o=>(o1:o+1,o2:o+2),i=>(i1:i+1,i2:i+2),(o,i)=>(o:o,i:i))
/// Block{
///     default;
///     default;
///     a+b
/// }
/// ↓
/// Block{
///     a+b
/// }
/// </summary>
internal sealed class 変換_KeySelectorの匿名型をValueTuple:ReturnExpressionTraverser_Quoteを処理しない {
    public 変換_KeySelectorの匿名型をValueTuple(作業配列 作業配列) : base(作業配列) {
    }
    public Expression 実行(Expression e)=> this.Traverse(e);
    protected override Expression Block(BlockExpression Block0) {
        var Block0_Expressions=Block0.Expressions;
        var Block0_Expressions_Count = Block0_Expressions.Count;
        if(Block0_Expressions_Count==0) return Default_void;
        if(Block0.Variables.Count==0&&Block0_Expressions_Count==1) return this.Traverse(Block0_Expressions[0]);
        var Block1_Expressions = new Expression[Block0_Expressions_Count];
        for(var a = 0;a<Block0_Expressions_Count;a++)Block1_Expressions[a]=this.Traverse(Block0_Expressions[a]);
        return Expression.Block(
            Block0.Variables,
            Block1_Expressions
        );
    }
    protected override Expression Call(MethodCallExpression MethodCall0){
        var MethodCall0_Method=MethodCall0.Method;
        if(!MethodCall0_Method.IsStatic)return base.Call(MethodCall0);
        var MethodCall0_Arguments=MethodCall0.Arguments;
        var MethodCall1_Arguments_Length=MethodCall0_Arguments.Count;
        var MethodCall1_Arguments=new Expression[MethodCall1_Arguments_Length];
        for(var a=0;a<MethodCall1_Arguments_Length;a++)
            MethodCall1_Arguments[a]=this.Traverse(MethodCall0_Arguments[a]);
        var MethodCall0_GenericMethodDefinition=GetGenericMethodDefinition(MethodCall0_Method);
        var typeArguments=MethodCall0_Method.GetGenericArguments();
        if(
            ExtensionSet.Join==MethodCall0_GenericMethodDefinition||
            ExtensionSet.GroupJoin==MethodCall0_GenericMethodDefinition||
            ExtensionEnumerable.Join==MethodCall0_GenericMethodDefinition||
            ExtensionEnumerable.Join_comparer==MethodCall0_GenericMethodDefinition||
            ExtensionEnumerable.GroupJoin==MethodCall0_GenericMethodDefinition||
            ExtensionEnumerable.GroupJoin_comparer==MethodCall0_GenericMethodDefinition
        ){
            //Join,GroupJoinのKeyが匿名型の場合ValueTupleにしてヒープ消費を減らす
            var keyType=typeArguments[2];
            if(keyType.IsAnonymous()){
                //匿名型をキャストすることはないと考えられるので。
                if(MethodCall1_Arguments[2] is LambdaExpression outerKeySelector1&&
                   MethodCall1_Arguments[3] is LambdaExpression innerKeySelector1){
                    var outerKeySelector1_Body=outerKeySelector1.Body;
                    var innerKeySelector1_Body=innerKeySelector1.Body;
                    if(outerKeySelector1_Body.NodeType==ExpressionType.New&&
                       innerKeySelector1_Body.NodeType==ExpressionType.New){
                        var 作業配列=this._作業配列;
                        var keyType_GetGenericArguments=keyType.GetGenericArguments();
                        Expression outerKeySelector2_Body,innerKeySelector2_Body;
                        Type 新keyType;
                        if(keyType_GetGenericArguments.Length==1){
                            outerKeySelector2_Body=((NewExpression)outerKeySelector1_Body).Arguments[0];
                            innerKeySelector2_Body=((NewExpression)innerKeySelector1_Body).Arguments[0];
                            新keyType=keyType_GetGenericArguments[0];
                        } else{
                            outerKeySelector2_Body=ValueTupleでNewする(作業配列,
                                ((NewExpression)outerKeySelector1_Body).Arguments);
                            innerKeySelector2_Body=ValueTupleでNewする(作業配列,
                                ((NewExpression)innerKeySelector1_Body).Arguments);
                            新keyType=innerKeySelector2_Body.Type;
                        }
                        typeArguments[2]=新keyType;
                        MethodCall1_Arguments[2]=Expression.Lambda(
                            作業配列.MakeGenericType(
                                typeof(Func<,>),
                                typeArguments[0],
                                新keyType
                            ),
                            outerKeySelector2_Body,
                            outerKeySelector1.Parameters
                        );
                        MethodCall1_Arguments[3]=Expression.Lambda(
                            作業配列.MakeGenericType(
                                typeof(Func<,>),
                                typeArguments[1],
                                新keyType
                            ),
                            innerKeySelector2_Body,
                            innerKeySelector1.Parameters
                        );
                        return Expression.Call(
                            MethodCall0_GenericMethodDefinition.MakeGenericMethod(typeArguments),
                            MethodCall1_Arguments
                        );
                    }
                }
            }
        }
        return Expression.Call(
            MethodCall0_Method,
            MethodCall1_Arguments
        );
    }
}
