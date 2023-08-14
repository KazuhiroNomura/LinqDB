using System.Text;
namespace LinqDB.Sets;

/// <summary>
/// IEnumerable&lt;T>.GroupByの結果の実体
/// </summary>
/// <typeparam name="TValue">値</typeparam>
/// <typeparam name="TKey">キー</typeparam>
public sealed class GroupingAscList<TKey,TValue>:AscList<TValue>, System.Linq.IGrouping<TKey,TValue>{
    /// <summary>キーを取得します。</summary>
    /// <returns>キー。</returns>
    public TKey Key{get;}
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="Key"></param>
    public GroupingAscList(TKey Key)=>this.Key=Key;
    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="Key">このキーに関連するタプルの集合</param>
    /// <param name="Value">1つのタプル</param>
    public GroupingAscList(TKey Key,TValue Value) : this(Key) {
        this.Key=Key;
        this.VoidAdd(Value);
    }
    /// <summary>
    /// 文字列で表現する
    /// </summary>
    public override string ToString() {
        var sb = new StringBuilder("(").Append(this.Key).Append(')');
        foreach(var a in this)
            sb.Append(a).Append(',');
        sb.Length--;
        return sb.ToString();
    }
    /// <summary>
    /// IEnumerable&lt;T>.GroupByの結果を順序も考慮したHashCode
    /// </summary>
    /// <returns>HashCode</returns>
    public override int GetHashCode()=>this.Key!.GetHashCode();
}