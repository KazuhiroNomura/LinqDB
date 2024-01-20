using LinqDB.Optimizers;
using LinqDB.Optimizers.Comparer;
using LinqDB.Optimizers.ReturnExpressionTraverser;
using LinqDB.Optimizers.ReturnExpressionTraverser.Profiling;
using LinqDB.Sets;

using MemoryPack;

using System.Collections;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using IEnumerable = System.Collections.IEnumerable;
namespace TestLinqDB.特殊パターン;
internal class ClassIEnumerableInt32Double : System.Collections.Generic.IEnumerable<int>, System.Collections.Generic.IEnumerable<double>
{
    public IEnumerator<int> GetEnumerator()
    {
        for (var a = 0; a<10; a++) yield return a;
    }
    IEnumerator<double> System.Collections.Generic.IEnumerable<double>.GetEnumerator()
    {
        for (var a = 0; a<10; a++) yield return a;
    }
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
internal class ClassIEnumerableInt32 : System.Collections.Generic.IEnumerable<int>
{
    public IEnumerator<int> GetEnumerator()
    {
        for (var a = 0; a<10; a++) yield return a;
    }
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
[MemoryPack.MemoryPackable,MessagePack.MessagePackObject(true),Serializable]
public partial class Instance{
    public static class Reflection{
        public static readonly MethodInfo InstanceCallAssignRef=typeof(Instance).GetMethod(nameof(Instance.InstanceCallAssignRef),特定パターン.F)!;
        public static readonly MethodInfo StaticCallAssignRef=typeof(Instance).GetMethod(nameof(Instance.StaticCallAssignRef),特定パターン.F)!;
    }

    public int InstanceCallAssignRef(ref int a,int b)=>a=b;
    public static int StaticCallAssignRef(Instance @this, ref int a,int b)=>a=b;
    public override bool Equals(object? obj)=>true;
    public override int GetHashCode()=>0;
}
public class 特定パターン:共通{
    //private protected override テストオプション テストオプション=>テストオプション.ローカル実行|テストオプション.アセンブリ保存;
    internal const BindingFlags F=BindingFlags.NonPublic|BindingFlags.Public|BindingFlags.Instance|BindingFlags.Static;
    [Fact]
    public void ClassIEnumerableInt32シリアライズ(){
        System.Collections.Generic.IEnumerable<int> input=new ClassIEnumerableInt32();
        this.ObjectシリアライズAssertEqual(input);
    }
    [Fact]
    public void ClassIEnumerableInt32Doubleシリアライズ(){
        System.Collections.Generic.IEnumerable<int> input=new ClassIEnumerableInt32Double();
        this.ObjectシリアライズAssertEqual(input);
    }
    [Fact]
    public void IEnumerableInt32シリアライズ(){
        System.Collections.Generic.IEnumerable<int> input=new[]{1,2,3};
        //現状
        //ListをIEnumerableでシリアライズするとIEnumerableの解釈でデシリアライズ
        //Listをobjectでシリアライズするとobjectの内部でListでデシリアライズ
        //UnionBy<Iterator>をIEnumerableでシリアライズするとIEnumerableの解釈でデシリアライズしたい
        //UnionBy<Iterator>をobjectでシリアライズするとIEnumerableの解釈でデシリアライズしたい
        //今後
        //ListをIEnumerableでシリアライズするとIEnumerableの内部でListでデシリアライズ
        //Listをobjectでシリアライズするとobjectの内部でListでデシリアライズ
        //UnionBy<Iterator>をIEnumerableでシリアライズするとIEnumerableの解釈でデシリアライズしたい
        //UnionBy<Iterator>をobjectでシリアライズするとIEnumerableの解釈でデシリアライズしたい
        this.ObjectシリアライズAssertEqual(input);
    }
    [Fact]
    public void UnionBy結果シリアライズ(){
        var a=new[]{3,5,7};
        var b=new[]{4,6,8};
        var input=a.UnionBy(b,x=>x+1);
        this.ObjectシリアライズAssertEqual(input);
    }
    [Fact]
    public void IEnumerableAnonymous(){
        var st=(new[]{3,5,7}).ToSet();
        this.Expression実行AssertEqual(()=>st.Select(o=>new{o}));
    }
    [Fact]
    public void CharArray(){
        var Serializer=this.MemoryPack;
        {
            var expected=new[]{'a','b','c'};
            var bytes=Serializer.Serialize<object>(expected);
            var bytes0=new byte[bytes.Length+1];
            Array.Copy(bytes,bytes0,bytes.Length);
            var output=Serializer.Deserialize<object>(bytes0);
            Assert.Equal(expected,output,new 汎用Comparer());
        }
        {
            var expected='A';
            //var bytes = Serializer.Serialize(expected);
            //var output = Serializer.Deserialize<char>(bytes);
            //Assert.Equal(expected,output!,new 汎用Comparer());
            SerializeDeserializeAreEqual<object>(this.MemoryPack,expected);
        }
        {
            var input=new[]{'a','b','c'};
            var m=new MemoryStream();
            Serializer.Serialize(m,input);
            m.Position=0;
            var output=Serializer.Deserialize<char[]>(m);
            Assert.Equal(input,output,new 汎用Comparer());
        }
        {
            var expected=new[]{'a','b','c'};
            //LinqDB.Serializers.MemoryPack.CharArrayFormatter.Instance.Serialize();
            //var bytes=Serializer.Serialize<object>(expected);
            //var output=Serializer.Deserialize<object>(bytes);
            //Assert.Equal(expected,output!,new 汎用Comparer());
            SerializeDeserializeAreEqual<object>(this.MemoryPack,expected);
        }
    }
    [Fact]
    public void MemoryPackでCharArray(){
        MemoryPackFormatterProvider.Register(new MemoryPack.Formatters.ArrayFormatter<char>());
        var input=new[]{'a','b','c'};
        var bytes=MemoryPackSerializer.Serialize(input);
        var output=MemoryPackSerializer.Deserialize<char[]>(bytes);
        Assert.True(input.SequenceEqual(output));
    }
    [Fact]
    public void Select_Where再帰で匿名型を走査(){
        this.Expression実行AssertEqual(()=>CreateSet().Select(p=>new ValueTuple<int,int,int,int,int,int,int>(p,p,p,p,p,p,p)).Where(
                p=>
                    p.Item1==0&&
                    p.Item2==0&&
                    p.Item3==0&&
                    p.Item4==0&&
                    p.Item5==0
            )
        );
    }
    [Fact]
    public void 具象Type(){
        var s=new int[]{1,2,3,4,5,6,7};
        this.Expression実行AssertEqual(()=>s.AsEnumerable().Union(s));
    }
    [Fact]
    public void Exceptを実行するとsourceがnullになるエラーがあった(){
        var st=(new[]{3,5,7}).ToSet();
        //this.Optimizer.CreateDelegate(() => st.Except(st))();
        this.Expression実行AssertEqual(()=>new{a=st.Except(st),b=st.Except(st)});
    }
    [Fact]
    public void WriteLeftRightMethodLambda(){
        var ParameterDecimmal=Expression.Parameter(typeof(decimal));
        var Constant1=Expression.Constant(1m);
        var ConversionDecimal=Expression.Lambda<Func<decimal,decimal>>(Expression.Add(ParameterDecimmal,ParameterDecimmal),ParameterDecimmal);
        var input1=Expression.AddAssign(ParameterDecimmal,Constant1,typeof(decimal).GetMethod("op_Addition"),ConversionDecimal);
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<object>>(
                Expression.Block(
                    new[]{ParameterDecimmal},
                    Expression.Assign(
                        ParameterDecimmal,
                        Expression.Constant(0m)
                    ),
                    Expression.Convert(
                        input1,
                        typeof(object)
                    )
                )
            )
        );
    }
    [Fact]
    public void 明示暗黙ローカル変数(){
        {
            var p=Expression.Parameter(typeof(decimal),"p");
            this.Optimizer_Lambda最適化(
                Expression.Lambda(
                    Expression.Block(
                        Expression.Add(
                            Expression.Constant(1m),
                            Expression.Constant(1m)
                        ),
                        Expression.Constant(1m)
                    )
                )
            );
            this.Optimizer_Lambda最適化(
                Expression.Lambda(
                    Expression.Block(
                        Expression.Assign(
                            p,
                            Expression.Add(
                                Expression.Constant(1m),
                                Expression.Constant(1m)
                            )
                        ),
                        Expression.Constant(1m)
                    )
                )
            );
        }
        //        } else{
        //        }
        //    }
        //    case ExpressionType.Call: {
        //        if(Reflection.Helpers.NoEarlyEvaluation==GetGenericMethodDefinition(((MethodCallExpression)Expression).Method))return;
        {
            var _1m=1m;
            var _7=this.Optimizer.Lambda最適化(
                ()=>_1m+_1m+_1m.NoEarlyEvaluation()
            );
        }
        {
            var _1m=1m;
            var set=new Set<int>();
            var _8=this.Optimizer.Lambda最適化(
                ()=>new{a=_1m+_1m,b=set.Except(set,EqualityComparer<int>.Default)}
            );
        }
        //    }
        //    case ExpressionType.Constant: {
        //        if(ILで直接埋め込めるか((ConstantExpression)Expression))return;
        {
            var _1=1;
            var _10=this.Optimizer.Lambda最適化(
                ()=>_1+_1+1
            );
        }
        //    }
        //    case ExpressionType.Parameter: {
        //        if(this.ラムダ跨ぎParameters.Contains(Expression))break;
        {
            var p=Expression.Parameter(typeof(int),"p");
            var _1=1;
            var _11=this.Optimizer.Lambda最適化(
                Expression.Lambda(
                    Expression.Block(
                        Expression.Constant(1m),
                        Expression.Constant(1m),
                        Expression.Lambda(p)
                    ),p
                )
            );
            var q=Expression.Parameter(typeof(int),"q");
            var _12=this.Optimizer.Lambda最適化(
                Expression.Lambda(
                    Expression.Block(
                        Expression.Constant(1m),
                        Expression.Constant(1m),
                        p,
                        Expression.Lambda(
                            Expression.Block(
                                p,
                                Expression.Add(
                                    Expression.Add(q,q),
                                    Expression.Add(q,q)
                                )
                            ),
                            q
                        )
                    ),p
                )
            );
        }
        //    }
        //}
        //if(this.判定_左辺Expressionsが含まれる.実行(Expression)) {
        //}
    }
    [Fact]
    public void NewArray(){
        var Array10=Expression.NewArrayBounds(typeof(int),Expression.Constant(10));
        var Array2_210=Expression.NewArrayBounds(typeof(int),Expression.Constant(2),Expression.Constant(3));
    }
    //private void 変換_Stopwatchに埋め込む(Expression Expression){
    //    var List計測=new List計測();
    //    var 変換_Stopwatchに埋め込む=new 変換_Stopwatchに埋め込む(new 作業配列(),List計測,new Dictionary<LabelTarget,計測>());

    //    変換_Stopwatchに埋め込む.実行(Expression);
    //    //Lambda07=Lambda06;
    //    //Trace.WriteLine(this._変換_Stopwatchに埋め込む.データフローチャート);
    //    Trace.WriteLine(変換_Stopwatchに埋め込む.Analize);

    //}
    [Fact]public void Condition1(){
        //└Conditional      │
        //　├Parameter a    │
        //　│├Parameter a  │
        //　│└Parameter b  └┬┐
        //　├Parameter b    ┌┘│
        //　│├Parameter b  │　│
        //　│└Parameter c  └┐│
        //　└Parameter c  　┌┼┘
        //　  ├Parameter a  ││
        //　  └Parameter c  └┼┐
        var a = Expression.Constant(0);
        var b = Expression.Constant(1);
        var c = Expression.Constant(2);
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Condition(
                    Expression.Equal(
                        Expression.And(a,b),
                        Expression.Constant(0)
                    ),
                    Expression.And(b,c),
                    Expression.And(a,c)
                )
            )
        );
    }
    [Fact]public void Condition2(){
        var a = Expression.Parameter(typeof(bool), "a");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,bool>>(
                Expression.Condition(
                    a,
                    a,
                    a
                ),a
            )
        );
    }
    [Fact]public void Condition3(){
        var a = Expression.Parameter(typeof(bool), "a");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,decimal>>(
                Expression.Condition(
                    a,
                    Expression.Constant(1m),
                    Expression.Add(
                        Expression.Constant(1m),
                        Expression.Constant(1m)
                    )
                ),a
            )
        );
    }
    [Fact]public void Condition4(){
        var a = Expression.Parameter(typeof(bool), "a");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool,decimal>>(
                Expression.Condition(
                    a,
                    Expression.Add(
                        Expression.Constant(1m),
                        Expression.Constant(1m)
                    ),
                    Expression.Constant(1m)
                ),a
            )
        );
    }
    [Fact]public void Condition5(){
        var Equal=Expression.Equal(
            Expression.Constant(false),
            Expression.Constant(true)
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool>>(
                Expression.Condition(
                    Equal,
                    Equal,
                    Equal
                )
            )
        );
    }
    [Fact]public void Condition7(){
        this.変換_局所Parameterの先行評価_実行(
            Expression.Condition(
                Expression.Condition(
                    Expression.Constant(false),
                    Expression.Constant(false),
                    Expression.Constant(false)
                ),
                Expression.Condition(
                    Expression.Constant(false),
                    Expression.Constant(false),
                    Expression.Constant(false)
                ),
                Expression.Condition(
                    Expression.Constant(false),
                    Expression.Constant(false),
                    Expression.Constant(false)
                )
            )
        );
    }
    [Fact]public void Condition71(){
        var Equal=Expression.Equal(
            Expression.Constant(1m),
            Expression.Constant(1m)
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.Condition(
                Equal,
                Equal,
                Expression.Condition(
                    Expression.Constant(false),
                    Expression.Constant(false),
                    Expression.Constant(false)
                )
            )
        );
    }
    [Fact]public void Condition72(){
        var Equal=Expression.Equal(
            Expression.Constant(1m),
            Expression.Constant(1m)
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.Condition(
                Equal,
                Expression.Condition(
                    Expression.Constant(false),
                    Expression.Constant(false),
                    Expression.Constant(false)
                ),
                Equal
            )
        );
    }
    [Fact]public void Condition73(){
        //│     0,{IIF(局所0, 局所0, 局所0)}
        //└┬┐ 0,IfTest(局所0)
        //┌┘│ 1,IfTrue 局所0{}
        //└┐│ 1,
        //┌┼┘ 2,IfFalse 局所0{}
        //└┼┐ 2,
        //┌┴┘ 3,end if{}
        //│　　 3,

        //{IIF(局所0, 局所0, 局所0)}
        var Equal=Expression.Equal(
            Expression.Constant(1m),
            Expression.Constant(1m)
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.Condition(
                Equal,
                Equal,
                Equal
            )
        );
    }
    [Fact]public void Condition75(){
        var Equal=Expression.Equal(
            Expression.Constant(1m),
            Expression.Constant(1m)
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.IfThen(
                Expression.Condition(
                    Equal,
                    Expression.Constant(false),
                    Expression.Constant(false)
                ),
                Equal
            )
        );
    }
    [Fact]
    public void Condition8ネスト(){
        var Equal=Expression.Equal(
            Expression.Constant(1m),
            Expression.Constant(1m)
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.Condition(
                Expression.Condition(
                    Equal,
                    Expression.Constant(false),
                    Expression.Constant(false)
                ),
                Expression.Condition(
                    Equal,
                    Expression.Constant(false),
                    Expression.Constant(false)
                ),
                Expression.Condition(
                    Equal,
                    Expression.Constant(false),
                    Expression.Constant(false)
                )
            )
        );
    }
    [Fact]
    public void Condition9前にExpression(){
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Constant(1m),
                Expression.Condition(
                    Expression.Constant(true),
                    Expression.Constant(1m),
                    Expression.Constant(1m)
                )
            )
        );
    }
    [Fact]
    public void Condition10後にExpression(){
        //│     0,{{ ... },IIF(True, 局所0, 1)}
        //└┬┐ 0,IfTest(True)
        //┌┘│ 1,IfTrue 局所0{}
        //└┐│ 1,
        //┌┼┘ 2,IfFalse 1{1}
        //└┼┐ 2,
        //┌┴┘ 3,end if{}
        //│　　 3,
        //.Block() {
        //    .If (
        //        True
        //    ) {
        //        $局所0 = 1M
        //    } .Else {
        //        $局所0 = 1M
        //    };
        //    $局所0
        //}
        this.変換_局所Parameterの先行評価_実行(
            Expression.Block(
                Expression.Condition(
                    Expression.Constant(true),
                    Expression.Constant(1m),
                    Expression.Constant(1m)
                ),
                Expression.Constant(1m)
            )
        );
    }
    [Fact]public void 計測埋め込み(){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Add(
                    Expression.Constant(1),
                    Expression.Constant(1)
                )
            )
        );
    }
    [Fact]public void Assign(){
        var a = Expression.Parameter(typeof(int), "a");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.Assign(
                    a,
                    Expression.Constant(3)
                ),a
            )
        );
    }
    [Fact]public void Switch0(){
        var Equal=Expression.Equal(
            Expression.Constant(0m),
            Expression.Constant(0m)
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool>>(
                Expression.Switch(
                    Expression.Constant(true),
                    Expression.Constant(true),
                    Expression.SwitchCase(
                        Equal,
                        Expression.Constant(true)
                    )
                )
            )
        );
    }
    [Fact]public void Switch1(){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<decimal>>(
                Expression.Switch(
                    Expression.Constant(1m),
                    Expression.Constant(1m),
                    Expression.SwitchCase(
                        Expression.Constant(1m),
                        Expression.Constant(1m)
                    )
                )
            )
        );
    }
    [Fact]public void TryCatch2ネスト0(){
        //try{
        //    try{
        //        1
        //    }catch{
        //        2
        //    }
        //}catch{
        //    try{
        //        3
        //    }catch{
        //        4
        //    }
        //}
        //try{
        //    try{
        //        t0=1
        //    }catch{
        //        t0=2
        //    }
        //    t2=t0
        //}catch{
        //    try{
        //        t1=3
        //    }catch{
        //        t1=4
        //    }
        //    t2=t1
        //}
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.TryCatch(
                    Expression.TryCatch(
                        Expression.Constant(1),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(2)
                        )
                    ),
                    Expression.Catch(
                        typeof(Exception),
                        Expression.TryCatch(
                            Expression.Constant(3),
                            Expression.Catch(
                                typeof(Exception),
                                Expression.Constant(4)
                            )
                        )
                    )
                )
            )
        );
    }
    public static int MethodInt32(int a,int b)=>a+b;
    [Fact]public void TryCatch2ネスト1(){
        var Method3=typeof(特定パターン).GetMethod(nameof(特定パターン.MethodInt32),F|BindingFlags.Public|BindingFlags.Instance)!;
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Call(
                    Method3,
                    Expression.TryCatch(
                        Expression.Constant(1),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(1)
                        )
                    ),
                    Expression.TryCatch(
                        Expression.Constant(1),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(2)
                        )
                    )
                )
            )
        );
    }
    [Fact]public void TryCatchをAddオペランドに(){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Add(
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(0)
                        )
                    ),
                    Expression.TryCatch(
                        Expression.Constant(0),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(0)
                        )
                    )
                )
            )
        );
    }
    private static int CallAssignValue(int a,int b)=>a+b;
    [Fact]public void CallAssignValue0(){
        var p=Expression.Parameter(typeof(int),"p");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[]{p},
                    Expression.Assign(p,Expression.Constant(2)),
                    Expression.Call(
                        typeof(特定パターン).GetMethod(nameof(CallAssignValue),F)!,
                        p,
                        p
                    )
                )
            )
        );
    }
    [Fact]public void InstanceCallAssignRef0(){
        var p=Expression.Parameter(typeof(int),"p");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.Call(
                    Expression.Constant(new Instance()),
                    Instance.Reflection.InstanceCallAssignRef,
                    p,
                    p
                ),p
            )
        );
    }
    [Fact]public void StaticCallAssignRef0(){
        var p=Expression.Parameter(typeof(int),"p");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.Call(
                    Instance.Reflection.StaticCallAssignRef,
                    Expression.Constant(new Instance()),
                    p,
                    p
                ),p
            )
        );
    }
    [Fact]public void TryCatchをInstanceCallAssignRef0(){
        var p=Expression.Parameter(typeof(int),"p");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[]{p},
                    Expression.Call(
                        Expression.Constant(new Instance()),
                        Instance.Reflection.InstanceCallAssignRef,
                        p,
                        Expression.TryCatch(
                            Expression.Constant(2),
                            Expression.Catch(
                                typeof(Exception),
                                Expression.Constant(0)
                            )
                        )
                    )
                )
            )
        );
    }
    [Fact]public void TryCatchをStaticCallAssignRef0(){
        var p=Expression.Parameter(typeof(int),"p");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[]{p},
                    Expression.Call(
                        Instance.Reflection.StaticCallAssignRef,
                        Expression.Constant(new Instance()),
                        p,
                        Expression.TryCatch(
                            Expression.Constant(2),
                            Expression.Catch(
                                typeof(Exception),
                                Expression.Constant(0)
                            )
                        )
                    )
                )
            )
        );
    }
    [Fact]public void TryCatchをCallオペランドに2(){
        var p=Expression.Parameter(typeof(int),"p");
        var t=Expression.Parameter(typeof(int),"t");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[]{p,t},
                    Expression.Call(
                        Expression.Constant(new Instance()),
                        Instance.Reflection.InstanceCallAssignRef,
                        p,
                        Expression.Assign(
                            t,
                            Expression.TryCatch(
                                Expression.Constant(2),
                                Expression.Catch(
                                    typeof(Exception),
                                    Expression.Constant(0)
                                )
                            )
                        )
                    )
                )
            )
        );
    }
    private static readonly ParameterExpression ParameterInt32 = Expression.Parameter(typeof(int),"int");
    [Fact]public void Lambda_TryCatch0Finally0(){
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[]{ParameterInt32},
                    Expression.Assign(ParameterInt32,Expression.Constant(0)),
                    Expression.TryCatchFinally(
                        ParameterInt32,
                        Expression.Default(typeof(void))
                    )
                )
            )
        );
    }
    [Fact]
    public void AddTryTry0(){
        var p=Expression.Parameter(typeof(int),"p");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int,int>>(
                Expression.Add(
                    Expression.TryCatch(
                        Expression.Constant(1),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(2)
                        )
                    ),
                    Expression.TryCatch(
                        Expression.Constant(3),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(4)
                        )
                    )
                ),
                p
            )
        );
    }
    private static int Return(int a)=>a;
    [Fact]
    public void AddのオペランドをTryCatch(){
        //.try
        //{
        //    IL_0000: ldc.i4.1
        //    IL_0001: stloc.0
        //    IL_0002: leave IL_000f
        //} // end .try
        //catch [System.Private.CoreLib]System.Exception
        //{
        //    IL_0007: pop
        //    IL_0008: ldc.i4.2
        //    IL_0009: stloc.0
        //    IL_000a: leave IL_000f
        //} // end handler

        //IL_000f: ldloc.0　　　この2行はIL_0024に挿入するべき
        //IL_0010: call int32 [TestLinqDB]TestLinqDB.特殊パターン.特定パターン::Return(int32)
        //    .try
        //{
        //    IL_0015: ldc.i4.3
        //    IL_0016: stloc.1
        //    IL_0017: leave IL_0024
        //} // end .try
        //catch [System.Private.CoreLib]System.Exception
        //{
        //    IL_001c: pop
        //    IL_001d: ldc.i4.4
        //    IL_001e: stloc.1
        //    IL_001f: leave IL_0024
        //} // end handler

        //IL_0024: ldloc.1
        //IL_0025: call int32 [TestLinqDB]TestLinqDB.特殊パターン.特定パターン::Return(int32)
        //IL_002a: add
        //IL_002b: ret
        var Return=typeof(特定パターン).GetMethod(nameof(特定パターン.Return),F)!;
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Add(
                    Expression.Call(
                        Return,
                        Expression.TryCatch(
                            Expression.Constant(1),
                            Expression.Catch(
                                typeof(Exception),
                                Expression.Constant(2)
                            )
                        )
                    ),
                    Expression.Call(
                        Return,
                        Expression.TryCatch(
                            Expression.Constant(3),
                            Expression.Catch(
                                typeof(Exception),
                                Expression.Constant(4)
                            )
                        )
                    )
                )
            )
        );
    }
    [Fact]
    public void TryCatchのBodyでAddのオペランドにTryCatch(){
        //try{
        //    IL_0000: ldc.i4.1   
        //    IL_0001: stloc.3    
        //    IL_0002: leave      IL_000f
        //}catch{
        //    IL_0007: pop        
        //    IL_0008: ldc.i4.2   
        //    IL_0009: stloc.3    
        //    IL_000a: leave      IL_000f
        //}
        //IL_000f: ldloc.3    
        //IL_0010: call       Int32 Return(Int32)/NET48.Program
        //IL_0015: stloc.1    
        //try{
        //    IL_0016: ldc.i4.3   
        //    IL_0017: stloc.3    
        //    IL_0018: leave      IL_0025
        //}catch{
        //    IL_001d: pop        
        //    IL_001e: ldc.i4.4   
        //    IL_001f: stloc.3    
        //    IL_0020: leave      IL_0025
        //}
        //IL_0025: ldloc.3    
        //IL_0026: stloc.0    
        //IL_0027: ldloc.0    
        //IL_0028: call       Int32 Return(Int32)/NET48.Program
        //IL_002d: stloc.2    
        //IL_002e: ldloc.1    
        //IL_002f: ldloc.2    
        //IL_0030: add        
        //IL_0031: ret        
        var Return=typeof(特定パターン).GetMethod(nameof(特定パターン.Return),F)!;
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.TryCatch(
                    Expression.Add(
                        Expression.Call(
                            Return,
                            Expression.TryCatch(
                                Expression.Constant(1),
                                Expression.Catch(
                                    typeof(Exception),
                                    Expression.Constant(2)
                                )
                            )
                        ),
                        Expression.Call(
                            Return,
                            Expression.TryCatch(
                                Expression.Constant(3),
                                Expression.Catch(
                                    typeof(Exception),
                                    Expression.Constant(4)
                                )
                            )
                        )
                    ),
                    Expression.Catch(
                        typeof(Exception),
                        Expression.Constant(2)
                    )
                )
            )
        );
    }
    private static string Concat(string arg0,string arg1,string arg2)=>arg0+arg1+arg2;
    [Fact]
    public void Tryの変形0(){
        //a+try{
        //    b
        //}catch{
        //    c
        //}
        //t0=a
        //try{
        //    t1=b
        //}catch{
        //    t1=c
        //}
        //t0+t1
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<decimal>>(
                Expression.Add(
                    Expression.Constant(1m),
                    Expression.TryCatch(
                        Expression.Constant(2m),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant(3m)
                        )
                    )
                )
            )
        );
    }
    [Fact]
    public void Tryの変形1(){
        //a+try{
        //    b
        //}catch{
        //    c
        //}+p
        //t0=a
        //try{
        //    t1=b
        //}catch{
        //    t1=c
        //}
        //t2=d
        //t0+t1+t2
        var Concat=typeof(特定パターン).GetMethod(nameof(特定パターン.Concat),F)!;
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<string>>(
                Expression.Call(
                    Concat,
                    Expression.Constant("a"),
                    Expression.TryCatch(
                        Expression.Constant("b"),
                        Expression.Catch(
                            typeof(Exception),
                            Expression.Constant("c")
                        )
                    ),
                    Expression.Constant("d")
                )
            )
        );
    }
    [Fact]
    public void Tryの変形2(){
        //1*try{
        //    2*try{
        //        3
        //    }catch{
        //        4
        //    }*5*try{
        //        6
        //    }catch{
        //        7
        //    }*8
        //}catch{
        //    9*try{
        //        10.
        //    }catch{
        //        11
        //    }*12*try{
        //        13
        //    }catch{
        //        14
        //    }*15
        //}*16
        //t0=1
        //try{
        //    t1=2
        //    try{
        //        t2=3
        //    }catch{
        //        t2=4
        //    }
        //    t3=5
        //    try{
        //        t4=6
        //    }catch{
        //        t4=7
        //    }
        //    t5=8
        //    t6=t0*t1*t2*t3*t4*t5
        //}catch{
        //    t7=i
        //    try{
        //        t8=j
        //    }catch{
        //        t8=k
        //    }
        //    t9=l
        //    try{
        //        t10=m
        //    }catch{
        //        t10=n
        //    }
        //    t11=o
        //    t6=t6*t7*t8*t9*t10
        //}
        //t12=p
        //t0*t6*t12
        var Return=typeof(特定パターン).GetMethod(nameof(特定パターン.Return),F)!;
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.TryCatch(
                    Expression.Add(
                        Expression.Call(
                            Return,
                            Expression.TryCatch(
                                Expression.Constant(1),
                                Expression.Catch(
                                    typeof(Exception),
                                    Expression.Constant(2)
                                )
                            )
                        ),
                        Expression.Call(
                            Return,
                            Expression.TryCatch(
                                Expression.Constant(3),
                                Expression.Catch(
                                    typeof(Exception),
                                    Expression.Constant(4)
                                )
                            )
                        )
                    ),
                    Expression.Catch(
                        typeof(Exception),
                        Expression.Constant(2)
                    )
                )
            )
        );
    }
    [Fact]
    public void TryCatch_Filter2(){
        var Variable=Expression.Parameter(typeof(Exception),"ex");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.TryCatch(
                    Expression.Constant(0),
                    Expression.Catch(
                        Variable,
                        Expression.Constant(0),
                        Expression.Equal(Variable,Expression.Default(Variable.Type))

                    )
                )
            )
        );
    }
    [Fact]
    public void Try(){
        var ex=Expression.Parameter(typeof(Exception),"ex");
        this.変換_局所Parameterの先行評価_実行(
            Expression.TryCatch(
                Expression.Add(
                    Expression.Constant(0m),
                    Expression.Constant(0m)
                ),
                Expression.Catch(
                    ex,
                    Expression.Default(typeof(decimal))
                )
            )
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.TryFinally(
                Expression.Constant(0),
                Expression.Add(
                    Expression.Constant(0m),
                    Expression.Constant(0m)
                )
            )
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.TryCatch(
                Expression.Constant(0m),
                Expression.Catch(
                    ex,
                    Expression.Add(
                        Expression.Constant(0m),
                        Expression.Constant(0m)
                    )
                )
            )
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.TryCatch(
                Expression.Constant(0m),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0m),
                    Expression.Equal(
                        Expression.Constant(0m),
                        Expression.Constant(0m)
                    )
                )
            )
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.TryCatch(
                Expression.Constant(0m),
                Expression.Catch(
                    ex,
                    Expression.Add(
                        Expression.Constant(0m),
                        Expression.Constant(0m)
                    )
                )
            )
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.TryCatch(
                Expression.Constant(0m),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0m),
                    Expression.Equal(
                        Expression.Constant(0m),
                        Expression.Constant(0m)
                    )
                )
            )
        );
        this.変換_局所Parameterの先行評価_実行(
            Expression.TryCatch(
                Expression.Default(typeof(void)),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Default(typeof(void))
                )
            )
        );
    }
    [Fact]public void GotoGotoGoto(){
        var L0 = Expression.Label("L0");
        var L1 = Expression.Label("L1");
        var L2 = Expression.Label("L2");
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<decimal>>(
                Expression.Block(
                    Expression.Goto(L1),
                    Expression.Label(L0),
                    Expression.Constant(1m),
                    Expression.Goto(L2),
                    Expression.Label(L1),
                    Expression.Constant(1m),
                    Expression.Goto(L0),
                    Expression.Label(L2),
                    Expression.Constant(1m)
                )
            )
        );
    }
}
