using System.Collections.Generic;
//using System.Linq;
using System.Linq.Expressions;
using LinqDB.Optimizers.Comparison;
// ReSharper disable LoopCanBeConvertedToQuery
// ReSharper disable InheritdocConsiderUsage
namespace LinqDB.Optimizers;

public class ブローブビルドExpressionEqualityComparer:IEqualityComparer<(Expression ビルド,Expression プローブ)> {
    private readonly ExpressionEqualityComparer ExpressionEqualityComparer;
    public ブローブビルドExpressionEqualityComparer(ExpressionEqualityComparer ExpressionEqualityComparer) => this.ExpressionEqualityComparer=ExpressionEqualityComparer;
    public bool Equals((Expression ビルド,Expression プローブ) x,(Expression ビルド,Expression プローブ) y) {
        var ExpressionEqualityComparer=this.ExpressionEqualityComparer;
        if(!ExpressionEqualityComparer.Equals(x.プローブ,y.プローブ)) return false;
        if(!ExpressionEqualityComparer.Equals(x.ビルド,y.ビルド)) return false;
        return true;
    }
    public int GetHashCode((Expression ビルド, Expression プローブ) obj) =>0;
}
