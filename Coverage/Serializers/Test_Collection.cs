using System;
using System.Collections.Generic;
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
    [TestMethod]public void Array()=>共通object(new[]{1,2,3});
    [TestMethod]public void List()=>共通object(new List<int>{1,2,3});
    [TestMethod]public void Dictionary()=>共通object(new Dictionary<int,int>{{1,1},{2,2}});
}
