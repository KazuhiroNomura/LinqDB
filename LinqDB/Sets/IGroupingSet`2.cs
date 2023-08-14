#define 並列化

// ReSharper disable LoopCanBeConvertedToQuery
// ReSharper disable ArrangeStaticMemberQualifier
namespace LinqDB.Sets {
    //
    // 概要:
    //     共通のキーを持つオブジェクトのコレクションを表します。
    //
    // 型パラメーター:
    //   TKey:
    //     キーの種類、 System.Linq.IGrouping`2です。
    //
    //   TElement:
    //     値の型、 System.Linq.IGrouping`2です。
    public interface IGroupingSet<out TKey,TElement>:IOutputSet<TElement>,System.Linq.IGrouping<TKey,TElement> {
    }
}