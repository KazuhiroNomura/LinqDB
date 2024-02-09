using System.Diagnostics;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using e = System.Linq.Expressions;
using LinqDB.Helpers;
using LinqDB.Sets;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal partial class 変換_TSqlFragmentからExpression{
    private e.Expression ValuesInsertSource(ValuesInsertSource x){
        var 作業配列= this.作業配列;
        var RowValues=x.RowValues;
        var RowValues_Count = RowValues.Count;
        var ColumnValues_Count= RowValues[0].ColumnValues.Count;
        var Arguments0=new e.Expression[ColumnValues_Count];
        var ValueTuples=new e.Expression[RowValues_Count];
        var ColumnValues0 = RowValues[0].ColumnValues;
        Debug.Assert(ColumnValues_Count == ColumnValues0.Count);
        //for(var b = 0;b < ColumnValues_Count;b++)
        //    Arguments0[b] = this.ScalarExpression(ColumnValues0[b]);
        //var ValueTuple0= CommonLibrary.ValueTupleでNewする(作業配列,Arguments0);
        //ValueTuples[0] = ValueTuple0;
        for(var a=0;a < RowValues_Count;a++) { 
            var ColumnValues=RowValues[a].ColumnValues;
            Debug.Assert(ColumnValues_Count== ColumnValues.Count);
            for(var b=0;b<ColumnValues_Count;b++)
                Arguments0[b]=this.ScalarExpression(ColumnValues[b]);
            ValueTuples[a]= CommonLibrary.ValueTupleでNewする(作業配列,Arguments0);
        }
        //return e.Expression.NewArrayInit(ValueTuple0.Type,ValueTuples);
        var ValueTuple_Type=ValueTuples[0].Type;
        var SetType=作業配列.MakeGenericType(typeof(Set<>),ValueTuple_Type);
        var IEnumerableT=作業配列.MakeGenericType(typeof(System.Collections.Generic.IEnumerable<>),ValueTuple_Type);
        var ctor=SetType.GetConstructor(作業配列.Types設定(IEnumerableT));
        return e.Expression.New(
            ctor,
            e.Expression.NewArrayInit(
                ValueTuple_Type,
                ValueTuples
            )
        );
        //return e.Expression.NewArrayInit(ValueTuple0.Type,ValueTuples);
    }
    private e.Expression SelectInsertSource(SelectInsertSource x){
        return this.QueryExpression(x.Select);
    }
    private e.Expression ExecuteInsertSource(ExecuteInsertSource x){
        throw this.単純NotSupportedException(x);
    }
}
