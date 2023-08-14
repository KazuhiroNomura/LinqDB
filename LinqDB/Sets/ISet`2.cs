namespace LinqDB.Sets;

/// <summary>
/// EntitySet,AssociationSetでの共通処理。抽象クラスではないのはAssociationSetはstructだから。
/// </summary>
/// <typeparam name="TContainer"></typeparam>
public interface ISet<out TContainer>{
    /// <summary>
    /// 検索で他のセットを参照するために使う。
    /// </summary>
    TContainer Container{
        get;
    }
}