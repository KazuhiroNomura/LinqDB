using LinqDB.Optimizers;
using LinqDB.Optimizers.Comparer;
using LinqDB.Optimizers.ReturnExpressionTraverser;
using LinqDB.Sets;

using MemoryPack;

using System.Collections;
using System.Diagnostics;
using System.Linq.Expressions;

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
public class 特定パターン:共通{
    protected override テストオプション テストオプション{get;}=テストオプション.MemoryPack_MessagePack_Utf8Json|テストオプション.ローカル実行;
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
    private void 変換_Stopwatchに埋め込む(Expression Expression){
        var List計測=new List計測();
        var 変換_Stopwatchに埋め込む=new 変換_Stopwatchに埋め込む(new 作業配列(),List計測,new Dictionary<LabelTarget,A計測>());

        変換_Stopwatchに埋め込む.実行(Expression);
        //Lambda07=Lambda06;
        //Trace.WriteLine(this._変換_Stopwatchに埋め込む.データフローチャート);
        Trace.WriteLine(変換_Stopwatchに埋め込む.Analize);

    }
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
        var a = Expression.Parameter(typeof(bool), "a");
        var b = Expression.Parameter(typeof(bool), "b");
        var c = Expression.Parameter(typeof(bool), "c");
        //var pp=Expression.And(p,p);
        this.変換_Stopwatchに埋め込む(
            Expression.Condition(
                Expression.And(a,b),
                Expression.And(b,c),
                Expression.And(a,c)
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
}
