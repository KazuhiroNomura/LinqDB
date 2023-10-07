using System.Diagnostics;
using System.Reflection;
using S = LinqDB.Sets;
using E = System.Collections.Generic;
using Q = System.Linq;
using LinqDB.Sets;
//using Microsoft.SqlServer.Dac.Deployment;
//using System.Diagnostics;
//using System.Runtime.Serialization;

//using static Microsoft.FSharp.Core.ByRefKinds;

namespace TestLinqDB.Serializers.Formatters;
public class Primitive : 共通{
#pragma warning disable CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。
    private void 共通<T>(T t){
        this.MemoryMessageJson_Assert(t);
        this.MemoryMessageJson_Assert((object)t);
        this.MemoryMessageJson_Assert(new{a=t});
        this.MemoryMessageJson_Assert(new{a=(object)t});
        Trace.WriteLine(typeof(T).Name);
    }
#pragma warning restore CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。
    [Fact]public void @sbyte()=>this.共通((sbyte)1);
    [Fact]public void @byte()=>this.共通((byte)1);
    [Fact]public void @short()=>this.共通((short)1);
    [Fact]public void @ushort()=>this.共通((ushort)1);
    [Fact]public void @int()=>this.共通((int)1);
    [Fact]public void @uint()=>this.共通((uint)1);
    [Fact]public void @long()=>this.共通((long)1);
    [Fact]public void @ulong()=>this.共通((ulong)1);
    [Fact]public void @float()=>this.共通((float)1);
    [Fact]public void @double()=>this.共通((double)1);
    [Fact]public void @bool()=>this.共通(true);
    [Fact]public void @char()=>this.共通('a');
    [Fact]public void @decimal()=>this.共通((decimal)1);
    [Fact]public void String()=>this.共通("string");
}
