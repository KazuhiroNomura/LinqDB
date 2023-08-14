using System.Collections.Generic;
using System.Reflection.Emit;
namespace LinqDB.Databases.Dom;
public interface IRelation:IName {
    /// <summary>
    /// テーブルからは親が分かればいいのでこれが重要
    /// </summary>
    ITable? 親ITable{ get; }
    //internal Point 親Point;
    /// <summary>
    /// ダイアグラムでの子
    /// </summary>
    ITable? 子ITable{ get; }
    IEnumerable<IColumn> Columns { get; }
    public bool IsNullable {
        get {
            foreach(var Column in this.Columns) {
                if(Column.IsNullable) {
                    //if(Column.NullableAttribute||Column.Type.IsNullable()) {
                    return true;
                }
            }
            return false;
        }
    }
    public class Information {
        /// <summary>
        /// orders customer.orders
        /// </summary>
        internal FieldBuilder? 親Table_子Many_FieldBuilder;
        /// <summary>
        /// Set{customer}orders.customer
        /// </summary>
        internal FieldBuilder? 子Table_親One_FieldBuilder;
    }
    public Information I { get; }
}
