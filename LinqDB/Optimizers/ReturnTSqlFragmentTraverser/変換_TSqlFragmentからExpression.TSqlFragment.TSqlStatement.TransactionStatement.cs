using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression BeginTransactionStatement(BeginTransactionStatement x){
        //x.Distributed//分散トランザクション
        string Name;
        if(x.Name is not null){
            Name=x.Name.Value;
            var a=x.Name.ValueExpression;
            var b=x.Name.Identifier;
        } else
            Name="_";
        var Transaction=e.Expression.Call(
            this.Container,
            this.ContainerType.GetMethod("Transaction")!
        );
        var Variable=e.Expression.Parameter(Transaction.Type,Name);
        this.AddScalarVariable(Variable);
        return e.Expression.Assign(Variable,Transaction);
    }
    private e.Expression CommitTransactionStatement(CommitTransactionStatement x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression RollbackTransactionStatement(RollbackTransactionStatement x){
        string Name;
        if(x.Name is not null){
            Name=x.Name.Value;
            var a=x.Name.ValueExpression;
            var b=x.Name.Identifier;
        } else
            Name="_";
        var Variable=this.FindScalarVariable(Name);
        var Transaction=e.Expression.Call(
            Variable,
            Variable.Type.GetMethod("Commit")!
        );
        return Transaction;
    }
    private e.Expression SaveTransactionStatement(SaveTransactionStatement x){
        throw this.単純NotSupportedException(x);
    }
}
