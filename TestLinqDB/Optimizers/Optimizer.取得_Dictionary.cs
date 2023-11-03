using System.Linq.Expressions;
using LinqDB.Sets;
using Microsoft.CSharp.RuntimeBinder;
//using System.Reflection;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable All
namespace TestLinqDB.Optimizers;
public class 取得_Dictionary:共通{
    [Fact]public void Constant(){
        //if(ILで直接埋め込めるか(Constant.Type))return;
        this.Expression実行AssertEqual(() => 0);
        this.Expression実行AssertEqual(() => 0m);
    }
    private delegate int FuncRef(ref int input);
    private static int Lambda0(ref int input, FuncRef d)
    {
        return d(ref input);
    }
    private delegate int Int32_Delegate_ref_Int32(ref int input);
    private static int Int32_Lambda_ref_Int32(ref int input, Int32_Delegate_ref_Int32 d) => d(ref input);
    [Fact]public void Dynamic(){
        var CSharpArgumentInfo1=CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null);
        var CSharpArgumentInfoArray1=new[]{CSharpArgumentInfo1};
        var CSharpArgumentInfoArray2=new[]{CSharpArgumentInfo1,CSharpArgumentInfo1};
        var CSharpArgumentInfoArray3=new[]{CSharpArgumentInfo1,CSharpArgumentInfo1,CSharpArgumentInfo1};
        //if(!this.SequenceEqual(a.Arguments,b.Arguments)) return false;
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<object>>(
                Expression.Dynamic(
                    Binder.UnaryOperation(
                        CSharpBinderFlags.None,
                        ExpressionType.Increment,
                        typeof(取得_Dictionary),
                        CSharpArgumentInfoArray1
                    ),
                    typeof(object),
                    Expression.Constant(1,typeof(object))
                )
            )
        );
    }
    [Fact]public void Lambda(){
        var s = new Set<int>();
        //if(!this.Quote内か&&this.Lambdas.Contains(Lambda,this.ExpressionEqualityComparer))
        //else
        //this.共通MemoryMessageJson_Expression_ExpressionAssertEqual(() => s.Let(s=>s.Let(p=>new{s})));
        //this.共通MemoryMessageJson_Expression_ExpressionAssertEqual(() => (1).Let(a=>a.Let((Func<int,int>)(p=>a))));
        //this.共通MemoryMessageJson_Expression_ExpressionAssertEqual(() => s.Let(s=>s.Select(p=>ValueTuple.Create(s))));
        this.Expression実行AssertEqual(() => s.Let(s=>s.Select((Func<int,ValueTuple<Set<int>>>)(p=>ValueTuple.Create(s)))));
        this.Expression実行AssertEqual(() => s.Select((Func<int,int>)(p=>p+1)));
        this.Expression実行AssertEqual(() => s.Let(s=>s.Select((Func<int,int>)(p=>p+1))));
    }
    [Fact]public void Block(){
        var p=Expression.Parameter(typeof(int));
        var s = new Set<int>();
        //var 判定_内部LambdaにParameterが存在するか=this._判定_内部LambdaにParameterが存在するか;
        //var Dictionaryラムダ跨ぎParameter=this.Dictionaryラムダ跨ぎParameter;
        //foreach(var Variable in Block.Variables)
        //    if(判定_内部LambdaにParameterが存在するか.実行(Block,Variable))
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[]{p},
                    Expression.Lambda<Func<int>>(p),
                    p
                )
            )
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[]{p},
                    Expression.Lambda<Func<int>>(Expression.Constant(0)),
                    p
                )
            )
        );
    }
    //var st=new Set<TestLinqDB.Sets.Key,Sets.Value>{new(new(0)),new(new(1))};
    //this.共通MemoryMessageJson_Expression_ExpressionAssertEqual(()=>st.SelectMany(o=>st).Where(i=>i.Key.メンバー.value==0));
    //this.共通MemoryMessageJson_Expression_ExpressionAssertEqual(()=>st.SelectMany(o=>st.Where(i=>i.Key.メンバー.value==0&& o.Equals(i))));
    //this.共通MemoryMessageJson_Expression_ExpressionAssertEqual(()=>st.Where(p=>p.Key.Equals(new Sets.Key(new(0)))));
}
