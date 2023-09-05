using LinqDB.Optimizers;
using LinqDB.Remote.Clients;
using LinqDB.Remote.Servers;
using LinqDB.Sets;
using LinqDB;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LinqDB.Serializers;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using LinqDB.Serializers.MemoryPack.Formatters;
using MemoryPack;
using System.Buffers;
using System.Collections.ObjectModel;
using System.Reflection;
using LinqDB.Helpers;
using MessagePack;
using Expressions=System.Linq.Expressions;
using Newtonsoft.Json.Linq;

namespace Serializers.MessagePack.Formatters;

[Serializable,MessagePackObject(true),MemoryPackable]
public partial record シリアライズ対象(
    int @int,
    string @string
);
public class 自然なシリアライズ:共通{
    [Fact]public void Test0(){
        var value=new シリアライズ対象(1,"s");
        //var FormatterType=typeof(Anonymous<>).MakeGenericType(value.GetType());
        //dynamic formatter=Activator.CreateInstance(FormatterType)!;
        //MemoryPackFormatterProvider.Register(formatter);
        this.共通object2(value);
    }
}
