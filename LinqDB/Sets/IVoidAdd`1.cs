namespace LinqDB.Sets;

/// <summary>
/// 戻り値のないAdd。ADictioary`3,IInputSet`1,KeyValueCollection`3,SetGrouping`3,AscList`1で利用する。
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IVoidAdd<in T>{
    /// <summary>
    /// 戻り値を無視して要素を追加する。
    /// </summary>
    /// <param name="Item"></param>
    void VoidAdd(T Item);
}