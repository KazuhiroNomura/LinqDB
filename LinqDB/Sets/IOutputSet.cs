using System.Collections;
namespace LinqDB.Sets;

public interface IOutputSet:IEnumerable {
    /// <summary>
    /// タプルの濃度(要素数)
    /// </summary>
    long Count { get; }
}