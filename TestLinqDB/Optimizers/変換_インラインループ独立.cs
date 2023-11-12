﻿using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

using LinqDB.Helpers;
using LinqDB.Sets;

using Xunit;
//using System.Reflection;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable All
namespace TestLinqDB.Optimizers;
public class 変換_インラインループ独立:共通{
    [Fact]public void Inline(){
        var s=new int[]{1,2,3,4,5,6,7};
        //if(Reflection.ExtensionSet.Inline2==GenericMethodDefinition) {
        this.Expression実行AssertEqual(()=>"".Inline(p=>"a"));
        //}
        //return MethodCall0.Arguments[0] is LambdaExpression MethodCall0_Lambda
        //    ?this.Traverse(MethodCall0_Lambda.Body)
        this.Expression実行AssertEqual(()=>ExtensionSet.Inline(()=>"a"));
        //                :base.Call(MethodCall0);
        this.Expression実行AssertEqual(()=>ExtensionSet.Inline((Func<string>)(()=>"a")));
    }
    [Fact]public void Aggregate(){
        var s=new int[]{1,2,3,4,5,6,7};
        //if(MethodCall0_Arguments.Count==2) {
        this.Expression実行AssertEqual(() => s.ToSet().Aggregate((a,b) => a+b));
        this.Expression実行AssertEqual(() => s.Aggregate((a,b) => a+b));
        //} else if(MethodCall0_Arguments.Count==3){
        this.Expression実行AssertEqual(() => s.ToSet().Aggregate(1,(a,b) => a+b));
        this.Expression実行AssertEqual(() => s.Aggregate(1,(a,b) => a+b));
        //}else{
        this.Expression実行AssertEqual(() => s.ToSet().Aggregate(1,(a,b) => a+b,p => p*p));
        this.Expression実行AssertEqual(() => s.Aggregate(1,(a,b) => a+b,p => p*p));
        //}
    }
    [Fact]public void All(){
        var s=new int[]{1,2,3,4,5,6,7};
        this.Expression実行AssertEqual(() => s.ToSet().All(p => true));
        this.Expression実行AssertEqual(() => s.All(p => true));
    }
    [Fact]public void Any(){
        var s=new int[]{1,2,3,4,5,6,7};
        //if(ループ展開可能メソッドか(MethodCall0_Arguments_0,out var MethodCall0_MethodCall)) {
        //    switch(MethodCall0_MethodCall.Method.Name) {
        //        case nameof(Enumerable.Except): {
        //            if(MethodCall0_MethodCall.Method.DeclaringType==typeof(ExtensionSet)&&ループ展開可能なSetのCall(second) is null) {
        this.Expression実行AssertEqual(() => s.ToSet().Except(new[] { 1,2 }.ToSet()).Any());
        //            } else {
        this.Expression実行AssertEqual(() => s.ToSet().Except(s.ToSet().Select(p => p+1)).Any());
        this.Expression実行AssertEqual(() => s.Except(s.ToSet()).Any());
        //            }
        //        }
        //        case nameof(Enumerable.Intersect): {
        //            if(MethodCall0_MethodCall_Arguments.Count==2) {
        //                if(ループ展開可能なSetのCall(first) is not null) {
        this.Expression実行AssertEqual(() => s.ToSet().Select(p => p+1).Intersect(new[] { 1,2 }.ToSet().Select(p => p+2)).Any());
        //                } else {
        this.Expression実行AssertEqual(() => s.ToSet().Select(p => p+1).Intersect(new[] { 1,2 }.ToSet()).Any());
        //                }
        //            } else {
        this.Expression実行AssertEqual(() => s.Select(p => p+1).Intersect(new[] { 1,2 },EqualityComparer<int>.Default).Any());
        //            }
        //        }
        //        case nameof(Enumerable.OfType): {
        //            if(変換元Type.IsAssignableTo(変換先Type)) return Constant_true;
        this.Expression実行AssertEqual(() => new int[] { 1,2 }.ToSet().OfType<object>().Any());

        this.Expression実行AssertEqual(() => new int?[] { 1,2 }.ToSet().OfType<int>().Any());
        this.Expression実行AssertEqual(() => new int?[] { 1,2 }.ToSet().OfType<object>().Any());
        //        }
        //        case nameof(Enumerable.SelectMany): {
        this.Expression実行AssertEqual(() => new int?[] { 1,2 }.SelectMany((index,p) => new double[] { 1,2 },(a,b) => a+b).Any());
        //        }
        //        case nameof(Enumerable.Union): {
        this.Expression実行AssertEqual(() => new int[] { 1,2 }.Union(new int[] { 1,2,3 }).Any());
        //        }
        //        case nameof(Enumerable.Where): {
        this.Expression実行AssertEqual(() => new int[] { 1,2 }.Where(p => p%2==0).Any());
        //        }
        //    }
        this.Expression実行AssertEqual(() => new int[] { 1,2 }.Select(p => p+1).Any());
        //}
        this.Expression実行AssertEqual(() => new int[] { 1,2 }.Any());
    }
    [Fact]public void Average(){
        var e=new int[]{1,2,3,4,5,6,7};
        var s=e.ToSet();

        //if     (Reflection.ExtensionSet       .AverageInt32_selector         ==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => s.Average(p => 1));
        //else if(Reflection.ExtensionSet       .AverageInt64_selector         ==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => s.Average(p => 1L));
        //else if(Reflection.ExtensionSet       .AverageSingle_selector        ==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => s.Average(p => 1f));
        //else if(Reflection.ExtensionSet       .AverageDouble_selector        ==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => s.Average(p => 1d));
        //else if(Reflection.ExtensionSet       .AverageNullableInt32_selector ==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => s.Average(p => (int?)1));
        //else if(Reflection.ExtensionSet       .AverageNullableInt64_selector ==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => s.Average(p => (long?)1L));
        //else if(Reflection.ExtensionSet       .AverageNullableSingle_selector==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => s.Average(p => (float?)1f));
        //else if(Reflection.ExtensionSet       .AverageNullableDouble_selector==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => s.Average(p => (double?)1d));
        //else if(Reflection.ExtensionSet       .AverageInt32                  ==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => s.Average());
        //else if(Reflection.ExtensionSet       .AverageInt64                  ==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => s.Select(p=>(long)p).Average());
        //else if(Reflection.ExtensionSet       .AverageSingle                 ==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => s.Select(p=>(float)p).Average());
        //else if(Reflection.ExtensionSet       .AverageDouble                 ==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => s.Select(p=>(double)p).Average());
        //else if(Reflection.ExtensionSet       .AverageNullableInt32          ==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => s.Select(p=>(int?)p).Average());
        //else if(Reflection.ExtensionSet       .AverageNullableInt64          ==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => s.Select(p=>(long?)p).Average());
        //else if(Reflection.ExtensionSet       .AverageNullableSingle         ==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => s.Select(p=>(float?)p).Average());
        //else if(Reflection.ExtensionSet       .AverageNullableDouble         ==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => s.Select(p=>(double?)p).Average());
        //else if(Reflection.ExtensionEnumerable.AverageInt32_selector         ==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => e.Average(p => p));
        //else if(Reflection.ExtensionEnumerable.AverageInt64_selector         ==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => e.Average(p => 1L));
        //else if(Reflection.ExtensionEnumerable.AverageSingle_selector        ==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => e.Average(p => 1f));
        //else if(Reflection.ExtensionEnumerable.AverageDouble_selector        ==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => e.Average(p => 1d));
        //else if(Reflection.ExtensionEnumerable.AverageNullableInt32_selector ==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => e.Average(p => (int?)1));
        //else if(Reflection.ExtensionEnumerable.AverageNullableInt64_selector ==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => e.Average(p => (long?)1));
        //else if(Reflection.ExtensionEnumerable.AverageNullableSingle_selector==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => e.Average(p => (float?)1));
        //else if(Reflection.ExtensionEnumerable.AverageNullableDouble_selector==GenericMethodDefinition) Sum_Type=typeof(double);
        this.Expression実行AssertEqual(() => e.Average(p => (double?)1));
        //else{
        this.Expression実行AssertEqual(() => s.Average(p => 1m));
        //}
        //ListExpression.Add(
        //    this.ループ展開(
        //        argument => {
        //            if(MethodCall0_Arguments.Count==1)return 共通(MethodCall0_Type,argument,Sum,Average_Int64Count);
        this.Expression実行AssertEqual(() => s.Average());
        this.Expression実行AssertEqual(() => s.Average(p => 1));
        //            static Expression 共通(Type MethodCall0_Type,Expression 共通argument,ParameterExpression Sum,ParameterExpression Average_Int64Count){
        //                if(MethodCall0_Type.IsNullable()) {
        this.Expression実行AssertEqual(() => s.Average(p => (int?)1));
        //                } else {
        this.Expression実行AssertEqual(() => s.Average(p => 1));       
        //                }
        //            }
        //        }
        //    )
        //);
        //if(MethodCall0_Type.IsNullable())行数0の処理=Expression.Default(MethodCall0_Type);
        this.Expression実行AssertEqual(() => s.Average(p => (int?)1));
        //else 行数0の処理=Throw_ZeroTuple(MethodCall0.Method);
        this.Expression実行AssertEqual(() => s.Average(p => 1));
    }
    [Fact]public void Avedev(){
        var s=new int[]{1,2,3,4,5,6,7}.ToSet();
        this.Expression実行AssertEqual(() => s.Avedev(p => p));
    }
    [Fact]public void AsEnumerable(){
        var s=new int[]{1,2,3,4,5,6,7}.ToSet();
        this.Expression実行AssertEqual(() => s.AsEnumerable());
    }
    [Fact]public void ToLookup(){
        var e=new int[]{1,2,3,4,5,6,7};
        var s=e.ToSet();
        //var Expression1ループ = this.ループ展開(
        //    argument => {
        //        if(argument.NodeType==ExpressionType.Parameter) {
        //        } else {
        //        }
        //    }
        //);
        //if(MethodCall0_Arguments.Count==3) {
        //} else {
        //    New=Expression.New(Dictionary_Type);
        //}
        //ExtensionEnumerable.ToLookup(e,p=>p/2);
        //e.ToLookup(p=>p/2);
        this.Expression実行AssertEqual(() => e.ToLookup(p=>p/2));
        this.Expression実行AssertEqual(() => s.ToLookup(p=>p/2));
    }
    [Fact]public void Count(){
        var e=new int[]{1,2,3,4,5,6,7};
        var s0=e.ToSet();
        var s1=e.ToSet();
        //if(ループ展開可能メソッドか(MethodCall0_Arguments_0,out _)){
        //    if(this.重複除去されているか(MethodCall0_Arguments_0)){
        this.Expression実行AssertEqual(() => s0.Intersect(s1).Count());
        //    } else{
        this.Expression実行AssertEqual(() => e.Select(p=>p+1).Count());
        //    }
        //}
        this.Expression実行AssertEqual(() => e.ToSet().Count());
    }
    [Fact]public void Harmean(){
        var s0=new Set<decimal>{1,2,3};
        var s1=s0;
        var n0=new Set<decimal?>{1,2,3};
        var n1=n0;
        //if(Reflection.ExtensionSet.HarmeanNullableDecimal_selector==GenericMethodDefinition||Reflection.ExtensionSet.HarmeanDecimal_selector==GenericMethodDefinition) {
        this.Expression実行AssertEqual(() => new Set<decimal?> { 1,2,3 }.Harmean(p => p));
        this.Expression実行AssertEqual(() => new Set<decimal> { 1,2,3 }.Harmean(p => p));
        //} else {
        this.Expression実行AssertEqual(() => new Set<double?> { 1,2,3 }.Harmean(p => p));
        this.Expression実行AssertEqual(() => new Set<double> { 1,2,3 }.Harmean(p => p));
        //}
        //if(MethodCall0.Type.IsNullable()) {
        //    if(MethodCall is not null) {
        //        if(this.重複除去されているか(MethodCall)) {
        this.Expression実行AssertEqual(() => n0.GroupBy(p=>p).Harmean(p => p.Key));
        //        } else {
        this.Expression実行AssertEqual(() => n0.Select(p => p+1).Harmean(p => p));
        //        }
        //    } else {
        this.Expression実行AssertEqual(() => n0.Harmean(p => p));
        //    }
        //} else {
        //    if(MethodCall is not null&&this.重複除去されているか(MethodCall)) {
        this.Expression実行AssertEqual(() => s0.GroupBy(p=>p).Harmean(p => p.Key));
        //    } else {
        this.Expression実行AssertEqual(() => s0.Select(p => p+1).Harmean(p => p));
        //    }
        //}
    }
    [Fact]public void Geomean(){
        var s0=new Set<double>{1,2,3};
        var s1=s0;
        var n0=new Set<double?>{1,2,3};
        var n1 = n0;
        //if(Reflection.ExtensionSet.GeomeanDouble_selector==GenericMethodDefinition) {
        //    if(this.重複除去されているか(MethodCall0_Arguments_0)){
        this.Expression実行AssertEqual(() => s0.GroupBy(p => p).Geomean(p => p.Key));
        //    } else{
        this.Expression実行AssertEqual(() => s0.Geomean(p => p));
        //    }
        //} else {
        //    if(MethodCall0_Arguments_0.NodeType==ExpressionType.Parameter) {
        this.Expression実行AssertEqual(() => n0.Let(n => n.Geomean(p => p)));
        //    } else {
        //        if(MethodCall is not null) {
        //            if(this.重複除去されているか(MethodCall)) {
        //this.Expression実行AssertEqual(() => n0.GroupBy(p => p).Geomean(p => p.Key));
        //            } else {
        this.Expression実行AssertEqual(() => n0.Select(p=>p+1).Geomean(p => p));
        //            }
        //        } else {
        this.Expression実行AssertEqual(() => n0.Geomean(p => p));
        //        }
        //    }
        //}
    }
    [Fact]public void Call() {
        var s = new int[]{1,2,3,4,5,6,7};
        //if(ループ展開可能メソッドか(GenericMethodDefinition)) {
        //    switch(Method.Name) {
        //        case nameof(ExtensionSet.Inline):return this.Inline(MethodCall0);
        this.Inline();
        //        case nameof(Enumerable.Aggregate):return this.Aggregate(MethodCall0);
        this.Aggregate();
        //        case nameof(Enumerable.All):return this.All(MethodCall0);
        this.All();
        //        case nameof(Enumerable.Any):return this.Any(MethodCall0);
        this.Any();
        //        case nameof(Enumerable.Average): return this.Average(MethodCall0);
        this.Average();
        //        case nameof(ExtensionSet.Avedev):return this.Avedev(MethodCall0);
        this.Avedev();
        //        case nameof(Enumerable.AsEnumerable): return this.AsEnumerable(MethodCall0);
        this.AsEnumerable();
        //        case nameof(ExtensionSet.Lookup): {
        this.ToLookup();
        //            var Expression1ループ = this.ループ展開(
        //                argument => {
        //                    if(argument.NodeType==ExpressionType.Parameter) {
        //                    } else {
        //                    }
        //                }
        //            );
        //            if(MethodCall0_Arguments.Count==3) {
        //            } else {
        //            }
        //        }
        //        case nameof(Enumerable.LongCount):
        //        case nameof(Enumerable.Count): {
        //            if(ループ展開可能メソッドか(MethodCall0_Arguments_0,out _)){
        //                if(this.重複除去されているか(MethodCall0_Arguments_0)){
        //                } else{
        //                }
        //            } else {
        //            }
        //        }
        //        case nameof(ExtensionSet.Harmean): {
        //            Expression Constant_0, Constant_1;
        //            if(Reflection.ExtensionSet.HarmeanNullableDecimal_selector==GenericMethodDefinition||Reflection.ExtensionSet.HarmeanDecimal_selector==GenericMethodDefinition) {
        //            } else {
        //            }
        //            if(MethodCall0.Type.IsNullable()) {
        //                if(MethodCall is not null) {
        //                    if(this.重複除去されているか(MethodCall)) {
        //                    } else {
        //                    }
        //                } else {
        //                }
        //            } else {
        //                if(MethodCall is not null&&this.重複除去されているか(MethodCall)) {
        //                } else {
        //                }
        //            }
        //        }
        //        case nameof(ExtensionSet.Geomean): {
        //            if(Reflection.ExtensionSet.GeomeanDouble_selector==GenericMethodDefinition) {
        //                if(this.重複除去されているか(MethodCall0_Arguments_0)){
        //                } else{
        //                }
        //            } else {
        //                if(MethodCall0_Arguments_0.NodeType==ExpressionType.Parameter) {
        //                } else {
        //                    if(MethodCall is not null) {
        //                        if(this.重複除去されているか(MethodCall)) {
        //                        } else {
        //                        }
        //                    } else {
        //                    }
        //                }
        //            }
        //        }
        //        case nameof(Enumerable.Max)or nameof(Enumerable.Min): {
        //            var NodeType = nameof(ExtensionSet.Max)==Name
        //                ? ExpressionType.LessThan
        //                : ExpressionType.GreaterThan;
        //            if(MethodCall0_Type.IsNullable()) {
        //            } else {
        //            }
        //            if(Compare_Defaultを使うべきか(NodeType,ElementType)) {
        //            } else {
        //            }
        //            ListExpression.Add(
        //                this.ループ展開(
        //                    argument => {
        //                        var Element = MethodCall0_Arguments.Count==1
        //                            ? argument
        //                            : this.LambdaExpressionを展開1(
        //                                this.Traverse(MethodCall0_Arguments[1]),
        //                                argument
        //                            );
        //                        if(GetValueOrDefault is not null) {
        //                            if(Element.NodeType!=ExpressionType.Parameter) {
        //                            } else {
        //                            }
        //                        } else {
        //                        }
        //                        if(Parameter_Comparer_Default is not null) {
        //                        } else {
        //                        }
        //                    }
        //                )
        //            );
        //        }
        //        case nameof(ExtensionSet.Stdev): {
        //            if(MethodCall0_Arguments.Count==1) {
        //            } else {
        //            }
        //        }
        //        case nameof(ExtensionSet.Var)or nameof(ExtensionSet.Varp): {
        //        }
        //        case nameof(Enumerable.SequenceEqual): {
        //        }
        //        case nameof(Enumerable.Single)or nameof(Enumerable.SingleOrDefault): {
        //            var Expressions1 = this.ループ展開(
        //                argument => {
        //                    if(!this.重複除去されているか(MethodCall0_Arguments_0)) {
        //                        if(IEquatableType.IsAssignableFrom(Item_Type)) {
        //                        } else {
        //                        }
        //                    }
        //                }
        //            );
        //            //Expression 要素なしifTrue;
        //            var 要素なしifTrue=nameof(ExtensionSet.SingleOrDefault)==Name
        //                ?MethodCall0_Arguments.Count==1
        //                    ?Expression.Default(ElementType)
        //                    :this.Traverse(MethodCall0_Arguments[1])
        //                :Expression.Throw(
        //                    New_ZeroTupleException,
        //                    ElementType
        //                );
        //        }
        //        case nameof(Enumerable.Sum): {
        //            if(Nullableか) {
        //                if(MethodCall0_Arguments.Count==1) {
        //                } else {
        //                }
        //            } else {
        //                if(MethodCall0_Arguments.Count==1) {
        //                    if(this.重複除去されているか(MethodCall0_Arguments_0)){
        //                    } else{
        //                    }
        //                } else {
        //                }
        //            }
        //        }
        //        case nameof(Enumerable.ToArray): {
        //            if(Method.DeclaringType!=typeof(Enumerable)) {
        //            }
        //        }
        //    }
        //    Debug.Assert(MethodCall0.Type!=typeof(void));
        //    {
        //    }
    }
}
