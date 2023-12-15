using LinqDB.Sets;
//using System.Reflection;
//using MemoryPack;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable All
namespace TestLinqDB.Optimizers.ReturnExpressionTraverser;
public class 変換_インラインループ : 共通
{
    [Fact]
    public void 具象Type()
    {
        var s = new int[]{
            1,2,3,4,5,6,7
        };
        //if(!Type.IsSealed) {
        //    if(typeof(IEnumerable<>)==Type.GetGenericTypeDefinition()) {
        this.Expression実行AssertEqual(() => s.ToSet().Select(o => o+1));
        //    } else {
        this.Expression実行AssertEqual(() => s.AsEnumerable().Union(s));
        //        } else {
        this.Expression実行AssertEqual(() => s.AsEnumerable().Select(o => o+1));
        //        }
        //    }
        //}
    }
}
