using Collections=System.Collections;
using System.Diagnostics;
using LinqDB.Sets;
namespace LinqDB.Enumerables;
using Generic=Collections.Generic;
/// <summary>単方向リストのコンテナ</summary>
/// <typeparam name="T">リスト内の要素の型。</typeparam>
[DebuggerTypeProxy(typeof(SetDebugView<>))]
public class List<T>:Generic.List<T>{
    private static readonly Serializers.MemoryPack.Formatters.Enumerables.List<T> InstanceMemoryPack=Serializers.MemoryPack.Formatters.Enumerables.List<T>.Instance;
    private static readonly Serializers.MessagePack.Formatters.Enumerables.List<T> InstanceMessagePack=Serializers.MessagePack.Formatters.Enumerables.List<T>.Instance;
    private static readonly Serializers.Utf8Json.Formatters.Enumerables.List<T> InstanceUtf8Json=Serializers.Utf8Json.Formatters.Enumerables.List<T>.Instance;
    /// <summary>
    /// カウントアップしない。成否なし。
    /// </summary>
    /// <param name="Item"></param>
    /// <returns></returns>
    internal void InternalAdd(T Item)=>this.Add(Item);
}
