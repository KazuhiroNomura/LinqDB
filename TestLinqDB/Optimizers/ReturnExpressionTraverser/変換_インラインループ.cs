using System.Linq.Expressions;
using LinqDB.Databases;
using LinqDB.Sets;
//using Sets=LinqDB.Sets;
//using System.Reflection;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable All
namespace TestLinqDB.Optimizers.ReturnExpressionTraverser;
public class 変換_インラインループ : 共通{
    protected override テストオプション テストオプション{get;}=テストオプション.式木の最適化を試行|テストオプション.インライン| テストオプション.ローカル実行|テストオプション.プロファイラ;
    public 変換_インラインループ(){
        this.Optimizer.IsInline=true;
    }
    private void Expression実行AssertEqual(Expression Expression){
        this.Optimizer.Lambda最適化(
            Expression.Lambda(Expression)
        );
    }
    private void Expression実行AssertEqual(LambdaExpression Expression){
        this.Optimizer.Lambda最適化(Expression);
    }
    public class SZArrayEnumerator:共通{
        protected override テストオプション テストオプション{get;}=テストオプション.式木の最適化を試行|テストオプション.インライン;
        [Fact]public void MoveNext(){
            var s = new int[]{1,2,3,4,5,6,7};
            this.Expression実行AssertEqual(() => s.Select(o => o+1));
        }
    }
    [Fact]public void AddAssign(){
        var s = new int[]{1,2,3,4,5,6,7};
        this.Expression実行AssertEqual(() => s.Average(o => o+1));
    }
    [Fact]public void Block_PreIncrementAssign_AddAssign(){
        var s = new int[]{1,2,3,4,5,6,7};
        this.Expression実行AssertEqual(() => s.Average(o => o+1));
    }
    [Fact]public void Call(){
        var s = new int[]{1,2,3,4,5,6,7}.ToSet();
        this.Expression実行AssertEqual(() => s.Avedev(o => o+1));
    }
    [Fact]public void Field(){
        var s = new int[]{1,2,3,4,5,6,7}.ToSet();
        this.Expression実行AssertEqual(() => s.Avedev(o => o+1));
    }
    [Fact]public void LambdaExpressionを展開1(){
        var e = new int[]{1,2,3,4,5,6,7};
        //if(Lambda is LambdaExpression Lambda1) {
        this.Expression実行AssertEqual(()=>e.Where((p)=>p==0));
        //}
        this.Expression実行AssertEqual(()=>e.Where(Anonymous((int p)=>p==0)));
    }
    [Fact]public void LambdaExpressionを展開2(){
        var e = new int[]{1,2,3,4,5,6,7};
        //if(Lambda is LambdaExpression Lambda2) {
        this.Expression実行AssertEqual(()=>e.Where((p,index)=>p==index));
        //}
        this.Expression実行AssertEqual(()=>e.Where(Anonymous((int p,int index)=>p==index)));
    }
    [Fact]public void ループ起点(){
        //if(Expression1_Type.IsArray){
        this.Expression実行AssertEqual(
            Expression.Call(
                LinqDB.Reflection.ExtensionEnumerable.Distinct0.MakeGenericMethod(typeof(int)),
                Expression.Constant(new int[]{1,2,3,4,5,6,7})
            )
        );
        //} else{
        this.Expression実行AssertEqual(
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
        //this.Expression実行AssertEqual(() => (LinqDB.Sets.IEnumerable<int>)s.ToSet().Except(s.ToSet()));
        //        } else{
        //            Debug.Assert(typeof(Generic.IEnumerable<>)==Expression_Type.GetGenericTypeDefinition());
        //        }
        //    }
        this.Expression実行AssertEqual(() => s.Except(s));
        //}
        this.Expression実行AssertEqual(() => s.ToSet().Except(s.ToSet()));
        //}
    }
    [Fact]public void ループ展開0(){
        //if(Expression is MethodCallExpression MethodCall)
        this.Expression実行AssertEqual(//0
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
        var ints=new Set<int>{1,2,3,4,5};
        var doubles=new Set<double>{1,2,3,4,5};
        var objects=new Set<object>{1,2.0,"3"};
        //var x0=ints.Cast<double>().ToArray();
        //var xx=objects.Cast<object>().ToArray();
        //switch(Name) {
        //    case nameof(Linq.Enumerable.Cast          ): {
        this.Expression実行AssertEqual(()=>ints.Cast<double>());
        this.Expression実行AssertEqual(()=>objects.Cast<int>());
        this.Expression実行AssertEqual(()=>objects.Cast<double>());
        //    }
        //    case nameof(Linq.Enumerable.DefaultIfEmpty): {
        this.Expression実行AssertEqual(()=>ints.DefaultIfEmpty());
        //    }
        //    case nameof(Linq.Enumerable.Distinct      ): {
        //        if(MethodCall0_Arguments.Count==1) {
        this.Expression実行AssertEqual(()=>e.Distinct());
        //        } else {
        this.Expression実行AssertEqual(()=>e.Distinct(EqualityComparer<int>.Default));
        //        }
        //    }
        //    case nameof(Linq.Enumerable.Except        ): {
        //        if(MethodCall0.Method.DeclaringType==typeof(Sets.ExtensionSet)&&ループ展開可能なSetのCall(second) is null) {
        this.Expression実行AssertEqual(()=>ints.Except(ints));
        //        } else {
        //            if(Reflection.ExtensionEnumerable.Except_comparer==GenericMethodDefinition){
        this.Expression実行AssertEqual(()=>e.Except(e,EqualityComparer<int>.Default));
        //            } else{
        this.Expression実行AssertEqual(()=>e.Except(e));
        //            }
        //        }
        //    }
        //    case nameof(Linq.Enumerable.GroupBy       ): {
        //        if(GenericMethodDefinition.DeclaringType==typeof(Sets.ExtensionSet))
        this.Expression実行AssertEqual(()=>ints.Select(p=>p+2).GroupBy(p=>p+1));
        this.Expression実行AssertEqual(()=>ints.GroupBy(p=>p+1));
        //        else
        this.Expression実行AssertEqual(()=>e.GroupBy(p=>p+1));
        //        if(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector==GenericMethodDefinition||Reflection.ExtensionSet.GroupBy_keySelector_elementSelector==GenericMethodDefinition) {
        this.Expression実行AssertEqual(()=>e.GroupBy(p=>p+1,p=>p+2));//0
        this.Expression実行AssertEqual(()=>ints.GroupBy(p=>p+1,p=>p+2));
        //        } else if(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector_comparer==GenericMethodDefinition) {
        this.Expression実行AssertEqual(()=>e.GroupBy(p=>p+1,p=>p+2,EqualityComparer<int>.Default));//1
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
        
        this.Expression実行AssertEqual(()=>ints.Select(p=>p+1).Intersect(ints.Select(p=>p+2)));
        //            } else {
        this.Expression実行AssertEqual(()=>ints.Intersect(ints));
        //            }
        //        } else {
        this.Expression実行AssertEqual(()=>ints.Intersect(ints,EqualityComparer<int>.Default));
        //        }
        //    }
        //    case nameof(Linq.Enumerable.OfType        ): {
        //        return this.ループ展開(
        //            argument => {
        //                if(変換元Type.IsNullable()&&変換先Type.IsAssignableFrom(変換元Type.GetGenericArguments()[0])) {
        this.Expression実行AssertEqual(()=>new int?[]{1,2}.ToSet().OfType<object>());
        //                ) {
        //                } else{
        this.Expression実行AssertEqual(()=>ints.OfType<object>());
        //                }
        //            }
        //        );
        //    }
        //    case nameof(Linq.Enumerable.Range         ): {
        this.Expression実行AssertEqual(()=>Enumerable.Range(1,2));
        //    }
        //    case nameof(Linq.Enumerable.Repeat        ): {
        this.Expression実行AssertEqual(()=>Enumerable.Repeat(1,2));
        //    }
        //    case nameof(Linq.Enumerable.Reverse       ): {
        this.Expression実行AssertEqual(()=>new[]{1,2,3}.Reverse());
        //    }
        //    case nameof(Linq.Enumerable.Select        ): {
        //        if(Reflection.ExtensionEnumerable.Select_indexSelector==GenericMethodDefinition) {
        this.Expression実行AssertEqual(()=>e.Select((p,index)=>p+index));
        //        } else {
        this.Expression実行AssertEqual(()=>ints.Select(p=>p+1));
        //        }
        //    }
        //    case nameof(Linq.Enumerable.SelectMany    ): {
        //        if(Reflection.ExtensionEnumerable.SelectMany_indexSelector==GenericMethodDefinition) {
        //this.Expression実行AssertEqual(()=>e.SelectMany((p,index)=>s));
        this.Expression実行AssertEqual(()=>e.SelectMany((p,index)=>e.Select(q=>new{p,index,q})));
        //        } else if(Reflection.ExtensionEnumerable.SelectMany_indexCollectionSelector_resultSelector==GenericMethodDefinition) {
        //this.Expression実行AssertEqual(()=>e.Select(p=>(double)p).SelectMany((p,index)=>s.Select(q=>new{p,index}),(a,b)=>b));
        this.Expression実行AssertEqual(()=>e.SelectMany((p,index)=>ints.Select(q=>new{p,index}),(a,b)=>new{a,b}));
        //        } else {
        this.Expression実行AssertEqual(()=>ints.SelectMany(p=>ints));
        this.Expression実行AssertEqual(()=>e.SelectMany(p=>e));
        //        }
        //    }
        //    case nameof(Linq.Enumerable.Take          ): {
        this.Expression実行AssertEqual(()=>e.Take(3));
        var x=new[]{1,2,3,4,5,6,7,8}.Take(new Range(1,3)).ToArray();
        //new Range(1,3).End.IsFromEnd
        this.Expression実行AssertEqual(()=>e.Take(new Range(1,2)));
        //    }
        //    case nameof(Linq.Enumerable.TakeWhile     ): {
        //        if(Reflection.ExtensionEnumerable.TakeWhile==GenericMethodDefinition) {
        this.Expression実行AssertEqual(()=>e.TakeWhile(p=>p%2==0));
        //        } else {
        this.Expression実行AssertEqual(()=>e.TakeWhile((p,index)=>p==index));
        //        }
        //    }
        //    case nameof(Linq.Enumerable.Union         ): {
        //        if(MethodCall0_Arguments.Count==2) {
        this.Expression実行AssertEqual(()=>ints.Union(ints));
        //        } else {
        this.Expression実行AssertEqual(()=>e.Union(e,EqualityComparer<int>.Default));
        //        }
        //    }
        //    case nameof(Sets.ExtensionSet.DUnion           ): {
        this.Expression実行AssertEqual(()=>ints.DUnion(ints));
        //    }            
        //    case nameof(Linq.Enumerable.Where         ): {
        //        if(Reflection.ExtensionEnumerable.Where_index==GenericMethodDefinition) {
        this.Expression実行AssertEqual(()=>e.Where((p,index)=>p==index));
        //        } else {
        this.Expression実行AssertEqual(()=>ints.Where(p=>p%2==0));
        //        }
        //    }
        //}
        this.Expression実行AssertEqual(()=>ints.SelectMany(q=>ints.Where((p,index)=>p+index==q)));
    }
    [Fact]
    public void 具象SetType戻り値ありCountあり(){
        var s = new int[]{
            1,2,3,4,5,6,7
        };
        this.Expression実行AssertEqual(() => s.ToSet().Select(p=>p+1).Geomean(p=>p+1));
        //if(typeof(Sets.IEnumerable<>)==Type.GetGenericTypeDefinition()){
        this.Expression実行AssertEqual(() => s.ToSet().Select(o => o+1));
        //}else if((Interface=Type.GetInterface(CommonLibrary.Sets_IEnumerable1_FullName)) is not null){
        //    Type=typeof(Sets.Set<>);
        //}else if(typeof(Generic.IEnumerable<>)==Type.GetGenericTypeDefinition()){
        this.Expression実行AssertEqual(() => s.Union(s));
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
        this.Expression実行AssertEqual(() => s.AsEnumerable().Select(o => o+1));
        //        }
        //    }
        //}
    }
    [Fact]public void 具象SetType戻り値ありCountなし(){
        var s = new int[]{
            1,2,3,4,5,6,7
        };
        this.Expression実行AssertEqual(() => s.ToSet().Select(p=>(int?)p+1).Harmean(p=>p+1f));
    }
    [Fact]public void 具象SetType戻り値なしCountあり(){
        this.Expression実行AssertEqual(()=>new int[]{1,2,3,4,5,6,7}.Select(p=>p+1).Count());
    }
    [Fact]public void 重複除去されているか(){
        var o=new Tables.Table();
        var s=new Set<Keys.Key,Tables.Table,Container>(null!);
        //if(nameof(Sets.ExtensionSet.GroupBy)==Name)
        this.Expression実行AssertEqual(()=>s.GroupBy(p=>0m).Geomean(p=>p.Count+1));
        //if(nameof(Sets.ExtensionSet.Except)==Name)
        this.Expression実行AssertEqual(()=>s.Except(s).Geomean(p=>4d));
        //if(nameof(Sets.ExtensionSet.Intersect)==Name)
        this.Expression実行AssertEqual(()=>s.Intersect(s).Geomean(p=>3d));
        //if(nameof(Sets.ExtensionSet.Select)==Name)
        //    if(MethodCall.Arguments[1] is LambdaExpression selector)
        this.Expression実行AssertEqual(()=>s.Select(p=>p.Key).Geomean(p=>3d));
        this.Expression実行AssertEqual(()=>s.Select(Anonymous((Tables.Table p)=>o)).Geomean(p=>3d));
        //if(nameof(Sets.ExtensionSet.SelectMany)==Name)
        this.Expression実行AssertEqual(()=>s.SelectMany(p=>s).Geomean(p=>3d));
        //if(nameof(Sets.ExtensionSet.Union)==Name)
        this.Expression実行AssertEqual(()=>s.Union(s).Geomean(p=>3d));
        //if(nameof(Sets.ExtensionSet.Where)==Name)
        this.Expression実行AssertEqual(()=>s.Where(p=>p==o).Geomean(p=>3d));
    }
    [Fact]public void Throwシーケンスに要素が含まれていません(){
        var s = new int[]{
            1,2,3,4,5,6,7
        };
        this.Expression実行AssertEqual(()=>s.Single());
    }
    private static int Quote引数<T>(Expression<Func<T>> x)=>1;
    [Fact]public void Quote(){
        var s = new int[]{
            1,2,3,4,5,6,7
        };
        this.Expression実行AssertEqual(()=>Quote引数(()=>s));
    }
    public class 判定_指定PrimaryKeyが存在する:共通{
        private void Expression実行AssertEqual(Expression Expression){
            this.Optimizer.Lambda最適化(
                Expression.Lambda(Expression)
            );
        }
        private void Expression実行AssertEqual(LambdaExpression Expression){
            this.Optimizer.Lambda最適化(Expression);
        }
        [Fact]public void Traverse(){
            var o=new Tables.Table();
            var s=new Set<Keys.Key,Tables.Table,Container>(null!);
            //switch(e.NodeType) {
            //    case ExpressionType.Parameter:
            //        if(e==this.EntityParameter)
            this.Expression実行AssertEqual(()=>s.Select(p=>new{p}).Geomean(p=>3d));//0
            this.Expression実行AssertEqual(()=>s.Select(p=>new ValueTuple<Tables.Table>(p)).Geomean(p=>3d));//0
            //        break;
            //    case ExpressionType.New: {
            //        var New = (NewExpression)e;
            //        if(e.Type.IsAnonymous())
            //            foreach(var Argument in New.Arguments)
            //0
            //        else
            this.Expression実行AssertEqual(()=>s.Select(p=>new string(p.ToString()[0],3)).Geomean(p=>3d));//0
            //        break;
            //    }
            //    case ExpressionType.MemberAccess:
            //        if(MemberExpression.Expression==this.EntityParameter)
            //            if(this.ParameterKey is not null)
            //                if(MemberExpression.Member.MetadataToken==this.ParameterKey.MetadataToken)
            this.Expression実行AssertEqual(()=>s.Select(p=>p.Key).Geomean(p=>3d));
            //                else
            this.Expression実行AssertEqual(()=>s.Select(p=>p.field1).Geomean(p=>3d));
            //            else
            this.Expression実行AssertEqual(()=>new[]{new{a=1}}.ToSet().Select(p=>p.a).Geomean(p=>p+3d));            //        break;
            //    default:
            this.Expression実行AssertEqual(()=>s.Select(p=>p.ToString()).Geomean(p=>3d));
            //}
        }

    }
}
