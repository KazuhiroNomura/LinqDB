﻿//using Microsoft.SqlServer.Dac.Deployment;
//using System.Diagnostics;
//using System.Runtime.Serialization;

//using static Microsoft.FSharp.Core.ByRefKinds;

namespace TestLinqDB.Serializers.Formatters.Others;
public class Primitive:共通{
    [Fact]public void Action3(){
        this.MemoryMessageJson_Assert(new{a=default(Action<int,int,int>)});
        this.MemoryMessageJson_Assert(new{a=(Action<int,int,int>)((int a,int b,int c)=>{})});
    }
    [Fact]public void Action2(){
        this.MemoryMessageJson_Assert(new{a=default(Action<int,int>)});
        this.MemoryMessageJson_Assert(new{a=(Action<int,int>)((int a,int b)=>{})});
    }
    [Fact]public void Action1(){
        this.MemoryMessageJson_Assert(new{a=default(Action<int>)});
        this.MemoryMessageJson_Assert(new{a=(Action<int>)((int a)=>{})});
    }
    [Fact]public void Action0(){
        this.MemoryMessageJson_Assert(new{a=default(Action)});
        this.MemoryMessageJson_Assert(new{a=(Action)(()=>{})});
    }
    [Fact]public void Func3(){
        this.MemoryMessageJson_Assert(new{a=default(Func<int,int,int>)});
        this.MemoryMessageJson_Assert(new{a=(Func<int,int,int>)((int a,int b)=>a+b)});
    }
    [Fact]public void Func2(){
        this.MemoryMessageJson_Assert(new{a=default(Func<int,int>)});
        this.MemoryMessageJson_Assert(new{a=(Func<int,int>)((int a)=>a)});
    }
    [Fact]public void Func1(){
        this.MemoryMessageJson_Assert(new{a=default(Func<int>)});
        this.MemoryMessageJson_Assert(new{a=(Func<int>)(() => 0)});
    }
}
