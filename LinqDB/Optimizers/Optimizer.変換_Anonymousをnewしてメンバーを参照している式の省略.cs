using System.Diagnostics;
using System.Linq.Expressions;
using LinqDB.Helpers;
using System.Reflection;
using Object=LinqDB.Reflection.Object;
namespace LinqDB.Optimizers;

partial class Optimizer {
    /// <summary>
    /// あるパラメータに依存する式を集めたい。現在はAndAlso,OrElseだけだが将来的にはもっと増やしたい。
    /// Lambdaに名前を付ける。この名前はのちに最適化に使う。
    /// </summary>
    private sealed class 変換_Anonymousをnewしてメンバーを参照している式の省略:ReturnExpressionTraverser_Quoteを処理しない {
        public 変換_Anonymousをnewしてメンバーを参照している式の省略(作業配列 作業配列) : base(作業配列) { }
        public Expression 実行(Expression e)=> this.Traverse(e);
        protected override Expression MemberAccess(MemberExpression Member0) {
            var Member0_Expression=Member0.Expression;
            if(Member0_Expression is null)return Member0;
            if(Member0_Expression.Type.IsAnonymous()){
                var Member1_Expression=this.Traverse(Member0_Expression);
                if(Member1_Expression is NewExpression New1) {
                    //new{o,i},new{o,i}.o,new{o,i}.i
                    //new{o,i},         o,         i}
                    var Member0_Member_Name = Member0.Member.Name;
                    var Parameters = New1.Constructor!.GetParameters();
                    var Parameters_Length = Parameters.Length;
                    for(var Index = 0;Index<Parameters_Length;Index++)
                        if(Parameters[Index].Name==Member0_Member_Name)return New1.Arguments[Index];
                } else
                    return Expression.MakeMemberAccess(Member1_Expression,Member0.Member);
            }else if(Member0_Expression.Type.IsValueTuple()){
                var Member1_Expression=this.Traverse(Member0_Expression);
                if(Member1_Expression is NewExpression New1) {
                    //new ValueTuple(o,i),new ValueTuple(o,i).o,new ValueTuple(o,i).i
                    //new ValueTuple(o,i),                    o,                    i
                    var Member0_Member_Name = Member0.Member.Name;
                    var Parameters = New1.Constructor!.GetParameters();
                    var Parameters_Length = Parameters.Length;
                    for(var Index = 0;Index<Parameters_Length;Index++)
                        if(Parameters[Index].Name==Member0_Member_Name.ToLower())return New1.Arguments[Index];
                } else
                    return Expression.MakeMemberAccess(Member1_Expression,Member0.Member);
            }
            return base.MemberAccess(Member0);
        }
        protected override Expression Call(MethodCallExpression MethodCall0){
            var MethodCall0_Method=MethodCall0.Method!;
            if(!MethodCall0_Method.IsStatic){
                var MethodCall0_Object= MethodCall0.Object!;
                var MethodCall0_Object_Type= MethodCall0_Object.Type;
                var IsAnonymous = MethodCall0_Object_Type.IsAnonymous();
                var IsValueTuple = MethodCall0_Object_Type.IsValueTuple();
                if(IsAnonymous||IsValueTuple) {
                    if(IsAnonymous) {
                        if(Object.Equals_==MethodCall0_Method&&MethodCall0_Object_Type==MethodCall0.Arguments[0].Type)
                            return this.共通(MethodCall0_Object,MethodCall0_Method,MethodCall0.Arguments[0]);
                    } else {
                        Debug.Assert(IsValueTuple);
                        if(MethodCall0_Object_Type.GetInterface(CommonLibrary.IEquatable_FullName) is not null)
                            return this.共通(MethodCall0_Object,MethodCall0_Method,MethodCall0.Arguments[0]);
                    }
                }
            }
            return base.Call(MethodCall0);
        }
        private Expression 共通(Expression MethodCall0_Object,MethodInfo MethodCall0_Method,Expression MethodCall0_Arguments_0) {
            var MethodCall1_Object = this.Traverse(MethodCall0_Object);
            var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments_0);
            if(MethodCall1_Object is NewExpression LNew&&MethodCall1_Arguments_0 is NewExpression RNew) {
                var 作業配列 = this._作業配列;
                var LNew_Arguments = LNew.Arguments;
                var RNew_Arguments = RNew.Arguments;
                var LNew_Arguments_0 = LNew_Arguments[0];
                var RNew_Arguments_0 = RNew_Arguments[0];
                var LNew_Arguments_0_Type = LNew_Arguments_0.Type;
                Expression Result = Expression.Call(LNew_Arguments_0,作業配列.GetMethod(LNew_Arguments_0_Type,nameof(Equals),LNew_Arguments_0_Type),RNew_Arguments_0);
                var LNew_Arguments_Count = LNew_Arguments.Count;
                for(var a = 1;a<LNew_Arguments_Count;a++) {
                    var LNew_Arguments_a = LNew_Arguments[a];
                    var LNew_Arguments_a_Type = LNew_Arguments_a.Type;
                    Result=Expression.AndAlso(
                        Result,
                        Expression.Call(
                            LNew_Arguments_a,
                            作業配列.GetMethod(LNew_Arguments_a_Type,nameof(Equals),LNew_Arguments_a_Type),
                            RNew_Arguments[a]
                        )
                    );
                }
                return Result;
            }
            return Expression.Call(MethodCall1_Object,MethodCall0_Method,this._作業配列.Expressions設定(MethodCall1_Arguments_0));
        }
    }
}