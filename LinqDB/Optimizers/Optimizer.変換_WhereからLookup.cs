using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq.Expressions;
using LinqDB.Sets;
using ExtensionEnumerable = LinqDB.Reflection.ExtensionEnumerable;
using ExtensionSet = LinqDB.Reflection.ExtensionSet;
// ReSharper disable AssignNullToNotNullAttribute
namespace LinqDB.Optimizers;
using static Common;
partial class Optimizer {
    private sealed class 変換_WhereからLookup:ReturnExpressionTraverser_Quoteを処理しない {
        private readonly 取得_OuterPredicate_InnerPredicate_プローブビルド 取得_OuterPredicate_InnerPredicate_プローブビルド;
        private readonly 判定_指定Parameters無 判定_指定Parameters無;
        public 変換_WhereからLookup(作業配列 作業配列,取得_OuterPredicate_InnerPredicate_プローブビルド 取得_OuterPredicate_InnerPredicate_プローブビルド,判定_指定Parameters無 判定_指定Parameters無) : base(作業配列) {
            this.取得_OuterPredicate_InnerPredicate_プローブビルド=取得_OuterPredicate_InnerPredicate_プローブビルド;
            this.判定_指定Parameters無=判定_指定Parameters無;
        }
        private ReadOnlyCollection<ParameterExpression>? 外側Parameters;
        public Expression 実行(Expression e) {
            Debug.Assert(e.NodeType==ExpressionType.Lambda);
            this.外側Parameters=null;
            return this.Lambda((LambdaExpression)e);
        }
        protected override Expression Lambda(LambdaExpression Lambda0) {
            var 外側Parameters = this.外側Parameters;
            this.外側Parameters=Lambda0.Parameters;
            var Lambda1=base.Lambda(Lambda0);
            this.外側Parameters=外側Parameters;
            return Lambda1;
        }
        protected override Expression Call(MethodCallExpression MethodCall0) {
            var MethodCall0_Method = MethodCall0.Method;
            if(ループ展開可能メソッドか(MethodCall0)) {
                if(nameof(Sets.ExtensionSet.Where)==MethodCall0_Method.Name) {
                    var MethodCall0_Arguments = MethodCall0.Arguments;
                    var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments[0]);
                    var MethodCall1_Arguments_1 = this.Traverse(MethodCall0_Arguments[1]);
                    if(this.判定_指定Parameters無.実行(MethodCall1_Arguments_0,this.外側Parameters!)&&MethodCall1_Arguments_1 is LambdaExpression predicate) {
                        //A.SelectMany(a=>
                        //    B.Where(b=>b==a)
                        //    (a,b)=>a,b
                        //↓
                        //A.SelectMany(a=>
                        //    B.Dictionary(b=>b).Equal(a)
                        //    (a,b)=>a,b
                        var predicate_Parameters = predicate.Parameters;
                        //↓でDictionary().Equal()と連続しないようにする。
                        //REVENUE_S.Let(revenue_s=>
                        //    revenue_s.Where(revenue=>
                        //        (revenue_s.Single().total_revenue==revenue.total_revenue)
                        //    )
                        //)
                        //A.Where(a=>
                        //    B.Select(b=>
                        //        C.Where(c=>c+1==a&&c+2==b)
                        //↓
                        //A.Where(a=>
                        //    Dictionary=C.Dictionary(c=>c+2)
                        //    B.Select(b=>
                        //        Dictionary.Equal(b).Where(c=>c+1==a)
                        //Debug.Assert(this.外側Parameters is not null);
                        var (OuterPredicate, InnerPredicate, Listプローブビルド)=this.取得_OuterPredicate_InnerPredicate_プローブビルド.実行(
                            predicate.Body,
                            this.外側Parameters!,
                            predicate_Parameters[0]
                        );
                        if(OuterPredicate is not null) {
                            MethodCall1_Arguments_0=Expression.Call(
                                MethodCall0_Method,
                                MethodCall1_Arguments_0,
                                Expression.Lambda(
                                    predicate.Type,
                                    OuterPredicate,
                                    predicate_Parameters
                                )
                            );
                        }
                        var Listプローブビルド_Count = Listプローブビルド.Count;
                        if(Listプローブビルド_Count>0) {
                            if(Listプローブビルド_Count==1) {
                                var (プローブ, ビルド)=Listプローブビルド[0];
                                var MethodCall1_Arguments_0_Type =MethodCall1_Arguments_0.Type;
                                var Set1 = MethodCall1_Arguments_0_Type;
                                while(true) {
                                    if(Set1 is null) {
                                        MethodCall1_Arguments_0=LookupExpression(プローブ,ビルド);
                                        break;
                                    }
                                    var GenericTypeDefinition = Set1;
                                    if(GenericTypeDefinition.IsGenericType) {
                                        GenericTypeDefinition=Set1.GetGenericTypeDefinition();
                                    }
                                    if(GenericTypeDefinition==typeof(Set<,>)) {
                                        var GetSet = Set1.GetMethod("GetSet");
                                        Debug.Assert(GetSet is not null);
                                        if(
                                            ビルド is MemberExpression Member&&
                                            Member.Member.DeclaringType!.IsGenericType&&
                                            Member.Member.DeclaringType.GetGenericTypeDefinition()==typeof(Entity<,>)&&
                                            Member.Member.Name==nameof(IKey<int>.Key)
                                            //Member.Member==Member.Expression!.Type.GetProperty(nameof(IKey<int>.Key))&&
                                            //Set1.GetGenericArguments()[0].GetProperty(nameof(IKey<int>.Key))==Member.Member
                                        ) {
                                            MethodCall1_Arguments_0=Expression.Call(
                                                MethodCall1_Arguments_0,
                                                GetSet,
                                                プローブ
                                            );
                                        } else {
                                            MethodCall1_Arguments_0=LookupExpression(プローブ,ビルド);
                                        }
                                        break;
                                    }
                                    Set1=Set1.BaseType;
                                }
                            } else {
                                var (プローブ, ビルド)=ValueTupleでNewする(this._作業配列,Listプローブビルド,0);
                                MethodCall1_Arguments_0=LookupExpression(プローブ,ビルド);
                            }
                            Expression LookupExpression(Expression プローブ,Expression ビルド) {
                                var Lookup = typeof(Sets.ExtensionSet)==MethodCall0_Method.DeclaringType
                                    ? ExtensionSet.Lookup
                                    : ExtensionEnumerable.Where==MethodCall0_Method.GetGenericMethodDefinition()
                                        ? ExtensionEnumerable.Lookup
                                        : ExtensionEnumerable.Lookup_index;
                                var 作業配列=this._作業配列;
                                var Instance = Expression.Call(
                                    作業配列.MakeGenericMethod(
                                        Lookup,
                                        MethodCall0_Method.GetGenericArguments()[0],
                                        ビルド.Type
                                    ),
                                    MethodCall1_Arguments_0,
                                    Expression.Lambda(
                                        ビルド,
                                        predicate_Parameters
                                    )
                                );
                                var GetValue = 作業配列.GetMethod(Instance.Type,nameof(SetGroupingList<int,int>.GetTKeyValue),プローブ.Type);
                                // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                                Debug.Assert(GetValue is not null);
                                    プローブ=Convert必要なら(
                                        プローブ,
                                        GetValue.GetParameters()[0].ParameterType
                                    );
                                //if(GetValue is null) {
                                //    GetValue=Instance.Type.GetMethod(nameof(LookupList<int,int>.GetObjectValue));
                                //    プローブ=Expression.Convert(
                                //        プローブ,
                                //        typeof(object)
                                //    );
                                //} else {
                                //    プローブ=Convert必要なら(
                                //        プローブ,
                                //        GetValue.GetParameters()[0].ParameterType
                                //    );
                                //}
                                return Expression.Call(
                                    Instance,
                                    GetValue,
                                    プローブ
                                );
                            }
                        }
                        if(InnerPredicate is not null){
                            if(this.判定_指定Parameters無.実行(InnerPredicate,predicate_Parameters)) {
                                var ReturnType = MethodCall0_Method.ReturnType;
                                Expression ifFalse;
                                if(typeof(Sets.ExtensionSet)==MethodCall0.Method.DeclaringType) {
                                    ifFalse=Expression.Field(
                                        null,
                                        typeof(ImmutableSet<>).MakeGenericType(ReturnType.GetGenericArguments()).GetField(nameof(ImmutableSet<int>.EmptySet))
                                    );
                                } else {
                                    ifFalse=Expression.Convert(
                                        Expression.NewArrayBounds(
                                            ReturnType.GetGenericArguments()[0],
                                            Constant_0
                                        ),
                                        ReturnType
                                    );
                                }
                                return Expression.Condition(
                                    InnerPredicate,
                                    Convert必要なら(
                                        MethodCall1_Arguments_0,
                                        ReturnType
                                    ),
                                    ifFalse
                                );
                            }
                            MethodCall1_Arguments_0=Expression.Call(
                                MethodCall0_Method,
                                MethodCall1_Arguments_0,
                                Expression.Lambda(
                                    predicate.Type,
                                    InnerPredicate,
                                    predicate_Parameters
                                )
                            );
                        }
                        return MethodCall1_Arguments_0;
                    }
                    return Expression.Call(
                        MethodCall0_Method,
                        MethodCall1_Arguments_0,
                        MethodCall1_Arguments_1
                    );
                }
            }
            return base.Call(MethodCall0);
        }
    }
}