using System.Diagnostics;
//using Microsoft.SqlServer.Dac.Deployment;
//using System.Diagnostics;
//using System.Runtime.Serialization;

//using static Microsoft.FSharp.Core.ByRefKinds;

namespace TestLinqDB.Serializers.Generic.Formatters;
public abstract class Primitive<TSerializer> : 共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected Primitive():base(new AssertDefinition(new TSerializer())){}
    [Fact]public void @sbyte()=>this.MemoryMessageJson_T_Assert全パターン((sbyte)1);
    [Fact]public void @byte()=>this.MemoryMessageJson_T_Assert全パターン((byte)1);
    [Fact]public void @short()=>this.MemoryMessageJson_T_Assert全パターン((short)1);
    [Fact]public void @ushort()=>this.MemoryMessageJson_T_Assert全パターン((ushort)1);
    [Fact]public void @int()=>this.MemoryMessageJson_T_Assert全パターン((int)1);
    [Fact]public void @uint()=>this.MemoryMessageJson_T_Assert全パターン((uint)1);
    [Fact]public void @long()=>this.MemoryMessageJson_T_Assert全パターン((long)1);
    [Fact]public void @ulong()=>this.MemoryMessageJson_T_Assert全パターン((ulong)1);
    [Fact]public void @float()=>this.MemoryMessageJson_T_Assert全パターン((float)1);
    [Fact]public void @double()=>this.MemoryMessageJson_T_Assert全パターン((double)1);
    [Fact]public void @bool()=>this.MemoryMessageJson_T_Assert全パターン(true);
    [Fact]public void @char()=>this.MemoryMessageJson_T_Assert全パターン('a');
    [Fact]public void @decimal()=>this.MemoryMessageJson_T_Assert全パターン((decimal)1);
    [Fact]public void String()=>this.MemoryMessageJson_T_Assert全パターン("string");
}
