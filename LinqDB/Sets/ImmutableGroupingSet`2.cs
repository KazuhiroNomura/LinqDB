using System.Linq;
using System.Text;
namespace LinqDB.Sets;
public interface IOutputGroupingSet<out TKey, out TValue>:IGrouping<TKey,TValue> {
}

/// <summary>
/// Set&lt;T>.GroupByの結果
/// </summary>
/// <typeparam name="TValue">値</typeparam>
/// <typeparam name="TKey">キー</typeparam>
public abstract class ImmutableGroupingSet<TKey,TValue>:ImmutableSet<TValue>, IOutputGroupingSet<TKey,TValue>{
    /// <summary>キーを取得します。</summary>
    /// <returns>キー。</returns>
    public TKey Key { get; }
    /// <summary>
    /// コンストラクタ。キーは必須
    /// </summary>
    /// <param name="Key">このキーに関連するタプルの集合</param>
    protected ImmutableGroupingSet(TKey Key)=>this.Key=Key;
    /// <summary>
    /// コンストラクタ。キーは必須
    /// </summary>
    /// <param name="Key">このキーに関連するタプルの集合</param>
    /// <param name="Value">1つのタプル</param>
    protected ImmutableGroupingSet(TKey Key,TValue Value) : this(Key) {
        this.InternalAdd(Value);
        this._Count=1;
    }
    /// <summary>
    /// 文字列で表現する
    /// </summary>
    public override string ToString() {
        var sb=new StringBuilder("(").Append(this.Key).Append(')');
        foreach(var a in this)
            sb.Append(a).Append(',');
        sb.Length--;
        return sb.ToString();
    }
    /// <summary>
    /// キーをハッシュコードにすることで検索できるようにする。
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => this.Key!.GetHashCode();
}