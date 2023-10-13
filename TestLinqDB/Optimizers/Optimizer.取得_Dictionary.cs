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
        this.共通コンパイル実行(() => 0);
        this.共通コンパイル実行(() => 0m);
    }
    [Fact]public void Dynamic(){
        var CSharpArgumentInfo1=CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None,null);
        var CSharpArgumentInfoArray1=new[]{CSharpArgumentInfo1};
        var CSharpArgumentInfoArray2=new[]{CSharpArgumentInfo1,CSharpArgumentInfo1};
        var CSharpArgumentInfoArray3=new[]{CSharpArgumentInfo1,CSharpArgumentInfo1,CSharpArgumentInfo1};
        //if(!this.SequenceEqual(a.Arguments,b.Arguments)) return false;
        this.共通コンパイル実行(
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
        this.共通コンパイル実行(() => s.Select(p=>p+1));
    }
    //var st=new Set<TestLinqDB.Sets.Key,Sets.Value>{new(new(0)),new(new(1))};
    //this.共通コンパイル実行(()=>st.SelectMany(o=>st).Where(i=>i.Key.メンバー.value==0));
    //this.共通コンパイル実行(()=>st.SelectMany(o=>st.Where(i=>i.Key.メンバー.value==0&& o.Equals(i))));
    //this.共通コンパイル実行(()=>st.Where(p=>p.Key.Equals(new Sets.Key(new(0)))));
}
