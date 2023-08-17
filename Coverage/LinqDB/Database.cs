using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable AssignNullToNotNullAttribute
namespace CoverageCS.LinqDB;

[TestClass]
public class Database : ATest
{
    [TestMethod]
    public void Executeのネスト1()
    {
        var local = 3;
        this.Execute2(() =>
            local
        );
    }
    private TResult Execute3<TResult>(Expression<Func<TResult>> Lambda){
        var actual0 = this.Optimizer.Execute(Lambda);
        return actual0;
    }
    [TestMethod]
    public void Executeのネスト2()
    {
        var local = 3;
        this.Execute3(() =>
            this.Execute3(
                () => local
            )
        );
        this.Execute2(() =>
            this.Execute2(
                () => local
            )
        );
    }
    private static int Quote(Expression<Func<int>> e) => e.Compile()();
    [TestMethod]
    public void Quoteを生成(){
        var QuoteMethod=typeof(Database).GetMethod(nameof(Quote),BindingFlags.Static|BindingFlags.NonPublic);
        Debug.Assert(QuoteMethod!=null,nameof(QuoteMethod)+" != null");
        {
            var Tree = Expression.Lambda<Func<int>>(
                Expression.Call(
                    QuoteMethod,
                    Expression.Quote(
                        Expression.Lambda<Func<int>>(
                            Expression.Constant(3)
                        )
                    )
                )
            );
            var M = Tree.Compile();
            M();
        }
        {
            var Quote = Expression.Quote(
                Expression.Lambda<Func<int>>(
                    Expression.Constant(3)
                )
            );
            var Tree = Expression.Lambda<Func<int>>(
                Expression.Call(
                    QuoteMethod,
                    Quote
                )
            );
            var M = Tree.Compile();
            M();
        }
        {
            this.Execute2(() => Lambda(() => 3) + Quote(() => 3));
            Expression<Func<int>> e = () => Quote(() => 3) + Quote(() => 3);
            e.Compile();
        }
        {
            //".Lambda LラムダR<Func`1[Int32]>() {",
            //"    3",
            //"}"
            this.Execute2(
                () => 3
            );
            var Lambda = Expression.Lambda<Func<int>>(
                Expression.Constant(3)
            );
            Lambda.Compile();
        }
    }
}