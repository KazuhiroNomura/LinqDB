﻿using Collections=System.Collections;
namespace LinqDB.Sets;
using Generic=Collections.Generic;
public sealed class HashSet<T>:Generic.HashSet<T>{
    private static readonly Serializers.MemoryPack.Formatters.Sets.HashSet<T> InstanceMemoryPack=Serializers.MemoryPack.Formatters.Sets.HashSet<T>.Instance;
    private static readonly Serializers.MessagePack.Formatters.Sets.HashSet<T> InstanceMessagePack=Serializers.MessagePack.Formatters.Sets.HashSet<T>.Instance;
    private static readonly Serializers.Utf8Json.Formatters.Sets.HashSet<T> InstanceUtf8Json=Serializers.Utf8Json.Formatters.Sets.HashSet<T>.Instance;
    /// <summary>
    /// カウントする。成否あり。
    /// DUnion,Intersectの2度目は戻り値のあるIsAddedメソッドを使う。本メソッドは戻り値は必要ないが"IsAdded"という名前を検索するのでこれにしている。
    /// </summary>
    /// <param name="Item"></param>
    public bool IsAdded(T Item)=>this.Add(Item);
    /// <summary>
    /// カウントアップしない。成否あり。
    /// </summary>
    /// <param name="Item"></param>
    internal bool InternalIsAdded(T Item)=>this.Add(Item);
    /// <summary>
    /// カウントアップしない。成否なし。
    /// </summary>
    /// <param name="Item"></param>
    /// <returns></returns>
    internal void InternalAdd(T Item)=>this.Add(Item);
}