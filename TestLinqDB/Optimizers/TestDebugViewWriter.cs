using Expression = System.Linq.Expressions.Expression;
//using Binder=System.Reflection.Binder;
// ReSharper disable AssignNullToNotNullAttribute
namespace TestLinqDB.Optimizers;


public class TestDeubgViewerWriter: 共通
{
    [Fact]
    public void 具象Type(){
        var p = Expression.Parameter(typeof(int), "p");
        var Lambda=Expression.Lambda(
            Expression.Block(
                Expression.Constant(1m),
                Expression.Constant(1m),
                Expression.Assign(p, Expression.Constant(1))
            )
        );
        var b=new StringWriter();
        var d=new DebugViewWriter(b);
        d.Visit(Lambda);
        var s=b.ToString();
        //        }
        //    }
        //}
    }
}
