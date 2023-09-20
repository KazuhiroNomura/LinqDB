using System.Linq.Expressions;
using LinqDB.Optimizers;
using LinqDB.Sets;
namespace Sets;

[Serializable]
public abstract class Test_ATest
{
    protected static Set<T> Set変数<T>() => new();
    [NonSerialized]
    private readonly Optimizer Optimizer = new();
    public TResult AssertExecute<TResult>(Expression<Func<TResult>> acutal) =>
        this.Optimizer.Execute(acutal);
    //public void AssertExecute<TResult>(FSharpExpr<TResult> acutal) =>
    //    this.Optimizer.Execute(
    //        Expression.Lambda<Func<TResult>>(
    //            LeafExpressionConverter.QuotationToExpression(acutal)
    //        )
    //    );
    //public TResult Execute<TResult>(Expression<Func<TResult>> Lambda) =>
    //    this.Optimizer.Execute(
    //        Lambda
    //    );
    //public TResult Execute<TResult>(FSharpExpr<TResult> Lambda) =>
    //    this.Optimizer.Execute(
    //        Expression.Lambda<Func<TResult>>(
    //            LeafExpressionConverter.QuotationToExpression(Lambda)
    //        )
    //    );
    //protected void Equal<TResult>(Expression<Func<TResult>> Lambda)=>
    //    this.AssertExecute(Lambda,);
}