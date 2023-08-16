using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CoverageCS.LinqDB;
using LinqDB.Helpers;
using LinqDB.Optimizers;
using LinqDB.Remote.Clients;
using LinqDB.Serializers;
using LinqDB.Serializers.Formatters;
//using LinqDB.Serializers.Formatters;
//using LinqDB.Serializers.MessagePack;
using MessagePack;
//using MessagePack.Resolvers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utf8Json;
using static System.Diagnostics.Contracts.Contract;
using Assert=Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Json=Newtonsoft.Json;
// ReSharper disable PossibleNullReferenceException
//具体的なAnonymousTypeをそのままSerialize,DeserializeするときはAnonymousExpressionResolverを通過しない。AnonymousTypeを返す。
//具体的なAnonymousTypeをObjectでSerialize,DeserializeするときはAnonymousExpressionResolverを通過しない。Dictionaryを返す。
namespace CoverageCS.Serializers;
[TestClass]
public class Test_Collection:ATest_シリアライズ{
    [TestMethod]public void Array()=>共通object(new[]{1});
    [TestMethod]public void List()=>共通object(new List<int>{1});
    [TestMethod]public void LinkedList()=>共通object(new LinkedList<int>(new[]{1}));
    [TestMethod]public void Queue()=>共通object(new Queue<int>(new[]{1}));
    [TestMethod]public void Stack()=>共通object(new Stack<int>(new[]{1}));
    [TestMethod]public void HashSet()=>共通object(new HashSet<int>(new[]{1}));
    [TestMethod]public void ReadOnlyCollection()=>共通object(new ReadOnlyCollection<int>(new[]{1}));
    [TestMethod]public void IList()=>共通object((IList<int>)new[]{1});
    [TestMethod]public void ICollection()=>共通object((ICollection<int>)new[]{1});
    [TestMethod]public void IEnumerable()=>共通object((IEnumerable<int>)new[]{1});
    [TestMethod]public void Dictionary()=>共通object(new Dictionary<int,int>{{1,1}});
    [TestMethod]public void IDictionary()=>共通object((IDictionary<int,int>)new Dictionary<int,int>{{1,1}});
    [TestMethod]public void SortedDictionary()=>共通object(new SortedDictionary<int,int>{{1,1}});
    [TestMethod]public void SortedList()=>共通object(new SortedList<int,int>{{1,1}});
    [TestMethod]public void ILookup()=>共通object((ILookup<int,int>)System.Array.Empty<int>().ToLookup(p=>p*p));
    //[TestMethod]public void IDictionary()=>共通object((IDictionary<int,int>)new Dictionary<int,int>{{1,1}});
    //[TestMethod]public void SortedDictionary()=>共通object(new SortedDictionary<int,int>{{1,1}});
    //[TestMethod]public void SortedList()=>共通object(new SortedList<int,int>{{1,1}});
    //[TestMethod]public void ILookup()=>共通object((ILookup<int,int>)new[]{1}.GroupBy);
    [TestMethod]public void IGrouping()=>共通object(new[]{1}.GroupBy(p=>p));
    [TestMethod]public void ObservableCollection()=>共通object(new ObservableCollection<int>(new[]{1}));
    [TestMethod]public void ReadOnlyObservableCollection()=>共通object(new ReadOnlyObservableCollection<int>(new ObservableCollection<int>(new[]{1})));
    [TestMethod]public void IReadOnlyList()=>共通object((IReadOnlyList<int>)new []{1,2});
    [TestMethod]public void IReadOnlyCollection()=>共通object((IReadOnlyCollection<int>)new[]{1});
    [TestMethod]public void ISet()=>共通object((ISet<int>)new HashSet<int>(new[]{1}));
    [TestMethod]public void ConcurrentBag()=>共通object(new ConcurrentBag<int>(new[]{1}));
    [TestMethod]public void ConcurrentQueue()=>共通object(new ConcurrentQueue<int>(new[]{1}));
    [TestMethod]public void ConcurrentStack()=>共通object(new ConcurrentStack<int>(new[]{1}));
    [TestMethod]public void ReadOnlyDictionary()=>共通object(new ReadOnlyDictionary<int,int>(new Dictionary<int,int>{{1,1}}));
    [TestMethod]public void IReadOnlyDictionary()=>共通object((IReadOnlyDictionary<int,int>)new ReadOnlyDictionary<int,int>(new Dictionary<int,int>{{1,1}}));
    [TestMethod]public void ConcurrentDictionary()=>共通object(new ConcurrentDictionary<int,int>(new[]{new KeyValuePair<int,int>(1,1)}));
    [TestMethod]public void Lazy()=>共通object(new Lazy<int,int>(1));
    [TestMethod]public void Task()=>共通object(new Task<int>(()=>1));
}
