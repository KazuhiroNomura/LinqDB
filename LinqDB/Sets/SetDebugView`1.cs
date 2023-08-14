using System.Diagnostics;
// ReSharper disable LoopCanBeConvertedToQuery
// ReSharper disable ArrangeStaticMemberQualifier

namespace LinqDB.Sets;

internal class SetDebugView<T> {
    private readonly ImmutableSet<T> Set;
    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public T[] Items => this.Set.ToArray();
    public SetDebugView(ImmutableSet<T> Set) => this.Set=Set;
}