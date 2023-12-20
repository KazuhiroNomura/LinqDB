using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;

using JetBrains.dotMemoryUnit.Client.Interface;
using LinqDB.Databases;
using LinqDB.Helpers;
using LinqDB.Optimizers;
using LinqDB.Sets;
//using Sets=LinqDB.Sets;

using TestLinqDB.Serializers.Formatters.Expressions;
using TestLinqDB.Sets;
using Xunit;
//using System.Reflection;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable All
namespace TestLinqDB.Optimizers.ReturnExpressionTraverser;
public class 変換_インラインループ : 共通{
    protected override テストオプション テストオプション{get;}=テストオプション.最適化;
    public 変換_インラインループ(){
        this.Optimizer.IsInline=true;
    }
    private void Cover(Expression Expression){
        this.Optimizer.Lambda最適化(
            Expression.Lambda(Expression)
        );
    }
    private void Cover(LambdaExpression Expression){
        this.Optimizer.Lambda最適化(Expression);
    }
    [Fact]public void AddAssign(){
        var s = new int[]{1,2,3,4,5,6,7};
        this.Cover(() => s.Average(o => o+1));
        //var p = Expression.Parameter(typeof(decimal));
        //this.Cover(
        //    Expression.Block(
        //        new[]{p},
        //        Expression.AddAssign(
        //            p,
        //            Expression.Constant(0m)
        //        )
        //    )
        //);
    }
    [Fact]public void Block_PreIncrementAssign_AddAssign(){
        var s = new int[]{1,2,3,4,5,6,7};
        this.Cover(() => s.Average(o => o+1));
    }
    [Fact]public void Call(){
        var s = new int[]{1,2,3,4,5,6,7}.ToSet();
        this.Cover(() => s.Avedev(o => o+1));
    }
    [Fact]public void Field(){
        var s = new int[]{1,2,3,4,5,6,7}.ToSet();
        this.Cover(() => s.Avedev(o => o+1));
    }
    [Fact]public void LambdaExpressionを展開1(){
        var e = new int[]{1,2,3,4,5,6,7};
        //if(Lambda is LambdaExpression Lambda1) {
        this.Cover(()=>e.Where((p)=>p==0));
        //}
        this.Cover(()=>e.Where(Anonymous((int p)=>p==0)));
    }
    [Fact]public void LambdaExpressionを展開2(){
        var e = new int[]{1,2,3,4,5,6,7};
        //if(Lambda is LambdaExpression Lambda2) {
        this.Cover(()=>e.Where((p,index)=>p==index));
        //}
        this.Cover(()=>e.Where(Anonymous((int p,int index)=>p==index)));
    }
    [Fact]public void ループ起点(){
        //if(Expression1_Type.IsArray){
        this.Cover(
            Expression.Call(
                LinqDB.Reflection.ExtensionEnumerable.Distinct0.MakeGenericMethod(typeof(int)),
                Expression.Constant(new int[]{1,2,3,4,5,6,7})
            )
        );
        //} else{
        this.Cover(
            Expression.Call(
                LinqDB.Reflection.ExtensionSet.DefaultIfEmpty.MakeGenericMethod(typeof(int)),
                Expression.Constant(new int[]{1,2,3,4,5,6,7}.ToSet())
            )
        );
        //}
    }
    [Fact]
    public void 重複なし作業Type(){
        var s = new int[]{
            1,2,3,4,5,6,7
        };
        //if((Type=Expression_Type.GetInterface(CommonLibrary.Generic_IEnumerable1_FullName))is null){
        //    if((Type=Expression_Type.GetInterface(CommonLibrary.Generic_IEnumerable1_FullName)) is null){
        //        if(typeof(Sets.IEnumerable<>)==Expression_Type.GetGenericTypeDefinition()){
        //this.Cover(() => (LinqDB.Sets.IEnumerable<int>)s.ToSet().Except(s.ToSet()));
        //        } else{
        //            Debug.Assert(typeof(Generic.IEnumerable<>)==Expression_Type.GetGenericTypeDefinition());
        //        }
        //    }
        this.Cover(() => s.Except(s));
        //}
        this.Cover(() => s.ToSet().Except(s.ToSet()));
        //}
    }
    [Fact]public void ループ展開0(){
        //if(Expression is MethodCallExpression MethodCall)
        this.Cover(//0
            Expression.Call(
                LinqDB.Reflection.ExtensionSet.DefaultIfEmpty.MakeGenericMethod(typeof(int)),
                Expression.Call(
                    LinqDB.Reflection.ExtensionSet.DefaultIfEmpty.MakeGenericMethod(typeof(int)),
                    Expression.Constant(new int[]{1,2,3,4,5,6,7}.ToSet())
                )
            )
        );
        //else
        //0
    }
    [Fact]
    public void ループ展開1(){
        var e=new int[]{1,2,3,4,5};
        var s=new Set<int>{1,2,3,4,5};
        
        //switch(Name) {
        //    case nameof(Linq.Enumerable.Cast          ): {
        this.Cover(()=>s.Cast<object>());
        //    }
        //    case nameof(Linq.Enumerable.DefaultIfEmpty): {
        this.Cover(()=>s.DefaultIfEmpty());
        //    }
        //    case nameof(Linq.Enumerable.Distinct      ): {
        //        if(MethodCall0_Arguments.Count==1) {
        this.Cover(()=>e.Distinct());
        //        } else {
        this.Cover(()=>e.Distinct(EqualityComparer<int>.Default));
        //        }
        //    }
        //    case nameof(Linq.Enumerable.Except        ): {
        //        if(MethodCall0.Method.DeclaringType==typeof(Sets.ExtensionSet)&&ループ展開可能なSetのCall(second) is null) {
        this.Cover(()=>s.Except(s));
        //        } else {
        //            if(Reflection.ExtensionEnumerable.Except_comparer==GenericMethodDefinition){
        this.Cover(()=>e.Except(e,EqualityComparer<int>.Default));
        //            } else{
        this.Cover(()=>e.Except(e));
        //            }
        //        }
        //    }
        //    case nameof(Linq.Enumerable.GroupBy       ): {
        //        if(GenericMethodDefinition.DeclaringType==typeof(Sets.ExtensionSet))
        this.Cover(()=>s.Select(p=>p+2).GroupBy(p=>p+1));
        this.Cover(()=>s.GroupBy(p=>p+1));
        //        else
        this.Cover(()=>e.GroupBy(p=>p+1));
        //        if(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector==GenericMethodDefinition||Reflection.ExtensionSet.GroupBy_keySelector_elementSelector==GenericMethodDefinition) {
        this.Cover(()=>e.GroupBy(p=>p+1,p=>p+2));//0
        this.Cover(()=>s.GroupBy(p=>p+1,p=>p+2));
        //        } else if(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector_comparer==GenericMethodDefinition) {
        this.Cover(()=>e.GroupBy(p=>p+1,p=>p+2,EqualityComparer<int>.Default));//1
        //        }
        //        if(comparer is not null) {
        //0
        //        } else {
        //1
        //        }
        //    }
        //    case nameof(Linq.Enumerable.Intersect     ): {
        //        if(MethodCall0_Arguments.Count==2) {
        //            if(ループ展開可能なSetのCall(first) is not null) {
        
        this.Cover(()=>s.Select(p=>p+1).Intersect(s.Select(p=>p+2)));
        //            } else {
        this.Cover(()=>s.Intersect(s));
        //            }
        //        } else {
        this.Cover(()=>s.Intersect(s,EqualityComparer<int>.Default));
        //        }
        //    }
        //    case nameof(Linq.Enumerable.OfType        ): {
        //        return this.ループ展開(
        //            argument => {
        //                if(変換元Type.IsNullable()&&変換先Type.IsAssignableFrom(変換元Type.GetGenericArguments()[0])) {
        this.Cover(()=>new int?[]{1,2}.ToSet().OfType<object>());
        //                ) {
        //                } else{
        this.Cover(()=>s.OfType<object>());
        //                }
        //            }
        //        );
        //    }
        //    case nameof(Linq.Enumerable.Range         ): {
        this.Cover(()=>Enumerable.Range(1,2));
        //    }
        //    case nameof(Linq.Enumerable.Repeat        ): {
        this.Cover(()=>Enumerable.Repeat(1,2));
        //    }
        //    case nameof(Linq.Enumerable.Reverse       ): {
        this.Cover(()=>new[]{1,2,3}.Reverse());
        //    }
        //    case nameof(Linq.Enumerable.Select        ): {
        //        if(Reflection.ExtensionEnumerable.Select_indexSelector==GenericMethodDefinition) {
        this.Cover(()=>e.Select((p,index)=>p+index));
        //        } else {
        this.Cover(()=>s.Select(p=>p+1));
        //        }
        //    }
        //    case nameof(Linq.Enumerable.SelectMany    ): {
        //        if(Reflection.ExtensionEnumerable.SelectMany_indexSelector==GenericMethodDefinition) {
        //this.Cover(()=>e.SelectMany((p,index)=>s));
        this.Cover(()=>e.SelectMany((p,index)=>e.Select(q=>new{p,index,q})));
        //        } else if(Reflection.ExtensionEnumerable.SelectMany_indexCollectionSelector_resultSelector==GenericMethodDefinition) {
        //this.Cover(()=>e.Select(p=>(double)p).SelectMany((p,index)=>s.Select(q=>new{p,index}),(a,b)=>b));
        this.Cover(()=>e.SelectMany((p,index)=>s.Select(q=>new{p,index}),(a,b)=>new{a,b}));
        //        } else {
        this.Cover(()=>s.SelectMany(p=>s));
        this.Cover(()=>e.SelectMany(p=>e));
        //        }
        //    }
        //    case nameof(Linq.Enumerable.Take          ): {
        this.Cover(()=>e.Take(3));
        var x=new[]{1,2,3,4,5,6,7,8}.Take(new Range(1,3)).ToArray();
        //new Range(1,3).End.IsFromEnd
        this.Cover(()=>e.Take(new Range(1,2)));
        //    }
        //    case nameof(Linq.Enumerable.TakeWhile     ): {
        //        if(Reflection.ExtensionEnumerable.TakeWhile==GenericMethodDefinition) {
        this.Cover(()=>e.TakeWhile(p=>p%2==0));
        //        } else {
        this.Cover(()=>e.TakeWhile((p,index)=>p==index));
        //        }
        //    }
        //    case nameof(Linq.Enumerable.Union         ): {
        //        if(MethodCall0_Arguments.Count==2) {
        this.Cover(()=>s.Union(s));
        //        } else {
        this.Cover(()=>e.Union(e,EqualityComparer<int>.Default));
        //        }
        //    }
        //    case nameof(Sets.ExtensionSet.DUnion           ): {
        this.Cover(()=>s.DUnion(s));
        //    }            
        //    case nameof(Linq.Enumerable.Where         ): {
        //        if(Reflection.ExtensionEnumerable.Where_index==GenericMethodDefinition) {
        this.Cover(()=>e.Where((p,index)=>p==index));
        //        } else {
        this.Cover(()=>s.Where(p=>p%2==0));
        //        }
        //    }
        //}
        this.Cover(()=>s.SelectMany(q=>s.Where((p,index)=>p+index==q)));
    }
    [Fact]
    public void 具象SetType戻り値ありCountあり(){
        var s = new int[]{
            1,2,3,4,5,6,7
        };
        this.Cover(() => s.ToSet().Select(p=>p+1).Geomean(p=>p+1));
        //if(typeof(Sets.IEnumerable<>)==Type.GetGenericTypeDefinition()){
        this.Cover(() => s.ToSet().Select(o => o+1));
        //}else if((Interface=Type.GetInterface(CommonLibrary.Sets_IEnumerable1_FullName)) is not null){
        //    Type=typeof(Sets.Set<>);
        //}else if(typeof(Generic.IEnumerable<>)==Type.GetGenericTypeDefinition()){
        this.Cover(() => s.Union(s));
        //}else if((Interface=Type.GetInterface(CommonLibrary.Generic_IEnumerable1_FullName)) is not null){
        //    Type=typeof(List<>);
        //} else{
        //    throw new NotSupportedException(Type.FullName);
        //}
        //var ReturnType=Type.MakeGenericType(Interface.GetGenericArguments());
        //string MethodName;
        //if(戻り値あり) {
        //    if(Countあり) {
        //        MethodName=nameof(Sets.Set<int>.IsAdded);
        //    } else {
        //        MethodName=nameof(Sets.Set<int>.InternalIsAdded);
        //    }
        //} else {
        //    if(Countあり) {
        //        MethodName=nameof(Sets.Set<int>.Add);
        //    } else {
        //        MethodName=nameof(Sets.Set<int>.InternalAdd);
        //    }
        //}
        //if(!Type.IsSealed) {
        //    if(typeof(IEnumerable<>)==Type.GetGenericTypeDefinition()) {
        //    } else {
        //        } else {
        this.Cover(() => s.AsEnumerable().Select(o => o+1));
        //        }
        //    }
        //}
    }
    [Fact]public void 具象SetType戻り値ありCountなし(){
        var s = new int[]{
            1,2,3,4,5,6,7
        };
        this.Cover(() => s.ToSet().Select(p=>(int?)p+1).Harmean(p=>p+1f));
    }
    [Fact]public void 具象SetType戻り値なしCountあり(){
        this.Cover(()=>new int[]{1,2,3,4,5,6,7}.Select(p=>p+1).Count());
    }
    [Fact]public void 重複除去されているか(){
        //global::TestLinqDB.Keys.;
        var o=new Tables.Table();
        var s=new Set<Keys.Key,Tables.Table,Container>(null!);
        //if(nameof(Sets.ExtensionSet.GroupBy)==Name)
        this.Cover(()=>s.GroupBy(p=>0m).Geomean(p=>p.Count+1));
        //if(nameof(Sets.ExtensionSet.Except)==Name)
        this.Cover(()=>s.Except(s).Geomean(p=>4d));
        //if(nameof(Sets.ExtensionSet.Intersect)==Name)
        this.Cover(()=>s.Intersect(s).Geomean(p=>3d));
        //if(nameof(Sets.ExtensionSet.Select)==Name)
        //    if(MethodCall.Arguments[1] is LambdaExpression selector)
        this.Cover(()=>s.Select(p=>p.Key).Geomean(p=>3d));
        this.Cover(()=>s.Select(p=>o).Geomean(p=>3d));
        //if(nameof(Sets.ExtensionSet.SelectMany)==Name)
        this.Cover(()=>s.SelectMany(p=>s).Geomean(p=>3d));
        //if(nameof(Sets.ExtensionSet.Union)==Name)
        this.Cover(()=>s.Union(s).Geomean(p=>3d));
        //if(nameof(Sets.ExtensionSet.Where)==Name)
        this.Cover(()=>s.Where(p=>p==o).Geomean(p=>3d));
    }
}
