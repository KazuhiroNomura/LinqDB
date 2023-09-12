﻿using LinqDB.Optimizers;
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
using Expressions=System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using MessagePack;

namespace Serializers.MessagePack.Formatters;

[Serializable,MemoryPackable,MessagePackObject(true)]
public partial class Eventテスト:IEquatable<Eventテスト>{
    public event Func<int,int> Event0;
    public bool Equals(Eventテスト? other){
        return this.Event0.Target.Equals(other.Event0.Target)&&this.Event0.Method==other.Event0.Method;
    }
    public override bool Equals(object? obj){
        if(ReferenceEquals(null,obj)){
            return false;
        }
        if(ReferenceEquals(this,obj)){
            return true;
        }
        if(obj.GetType()!=this.GetType()){
            return false;
        }
        return this.Equals((Eventテスト)obj);
    }
    public override int GetHashCode(){
        return 0;
    }
    public static bool operator==(Eventテスト? left,Eventテスト? right){
        return Equals(left,right);
    }
    public static bool operator!=(Eventテスト? left,Eventテスト? right){
        return!Equals(left,right);
    }
}
public class 色んなデータ型:共通 {
    //[Fact]public void Anonymous0(){
    //    MemoryPackFormatterProvider.Register(new Formatter2());
    //    object data=new Data2{
    //        a=11,
    //        b=22,
    //        c=33,
    //        d=44,
    //        e=55,
    //    };
    //    this.Formatters.Clear();
    //    var bytes=MemoryPackSerializer.Serialize(data);
    //    this.Formatters.Clear();
    //    object actual=default!;
    //    MemoryPackSerializer.Deserialize(bytes,ref actual);
    //}
    [Fact]public void Anonymous000(){
        var value=new{
            a=1,
            b=2.0,
            c=3m,
            d=4f,
            e="e"
        };
        //var FormatterType=typeof(Anonymous<>).MakeGenericType(value.GetType());
        //dynamic formatter=Activator.CreateInstance(FormatterType)!;
        //MemoryPackFormatterProvider.Register(formatter);
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(value);
    }
    static LambdaExpression Lambda<T>(Expression<Func<T>> e)=>e;
    [Fact]public void ClassDisplay(){
        var a=1;
        var body=Lambda(()=>a).Body;
        var member=(Expressions.MemberExpression)body;
        var constant=(Expressions.ConstantExpression)member.Expression;
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(constant.Value);
    }
    [Fact]
    public void Anonymous002(){
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(new{a=(object)1});
    }
    [Fact]
    public void Anonymous003(){
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(new{a=(object)new{aa=11}});
    }
    [Fact]
    public void Anonymous004(){
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(new{a=(object)new{aa=1}});
    }
    [Fact]
    public void Anonymous022(){
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(new{x=new{a=111}});
    }
    [Fact]
    public void Anonymous023(){
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(
            new{
                x=new{
                    a=111,
                    b=222.0,
                    c=333m,
                    d=444f,
                    e="eee"
                }
            }
        );
    }
    [Fact]
    public void Anonymous030(){
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(
            Tuple.Create(1)
        );
    }
    [Fact]
    public void Anonymous031(){
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(
            Tuple.Create(
                new{
                    a=111,
                }
            )
        );
    }
    [Fact]
    public void Anonymous032(){
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(
            Tuple.Create<object>(
                new{
                    a=111,
                }
            )
        );
    }
    [Fact]
    public void Anonymous033(){
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(
            Tuple.Create(
                new{
                    a=111,
                    b=222.0,
                    c=333m,
                    d=444f,
                    e="eee"
                }
            )
        );
    }
    [Fact]
    public void Anonymous040(){
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(new{
            a=new{aa=1},
            b=new{aa=1}
        });
    }
    [Fact]
    public void Anonymous041(){
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(new{
            a=new{aa=1},
            b=(object)new{aa=1}
        });
    }
    [Fact]
    public void Anonymous05(){
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(
            Tuple.Create(
                new{
                    a=1111
                }
            )
        );
    }
    [Fact]
    public void Anonymous06(){
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(
            new{
                a=(object)new{
                    a=1111
                }
            }
        );
    }
    [Fact]
    public void Anonymous07(){
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(
            new[]{
                (object)new{
                    a=1111
                },
                (object)new{
                    a=2222
                }
            }
        );
    }
    [Fact]
    public void Anonymous08(){
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(
            new[]{
                new{
                    a=1111
                },
                new{
                    a=2222
                }
            }
        );
    }
    [Fact]
    public void Anonymous09(){
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(
            new[]{
                new{
                    a=1111
                },
                new{
                    a=2222
                }
            }.ToList()
        );
    }
    [Fact]
    public void Anonymous10(){
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス<object>(
            (object)new{
                a=1111
            }
        );
    }
    [Fact]public void Anonymous11(){
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(new{
            a=(object)new{aa=1}
        });
    }
    [Fact]public void Event(){
        const int expected=12345;
        var Eventテスト=new Eventテスト();
        Eventテスト.Event0+=x=>expected;
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(Eventテスト);
    }
    private static int フィールド;
    [Fact]public void Field(){
        var f=typeof(色んなデータ型).GetField(nameof(フィールド),BindingFlags.Static|BindingFlags.NonPublic);
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(f);
    }
    [Fact]public void Member(){
        MemberInfo f=typeof(色んなデータ型).GetField(nameof(フィールド),BindingFlags.Static|BindingFlags.NonPublic);
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(f);
    }
    [Fact]public void Method(){
        var f=typeof(色んなデータ型).GetMethod("Method");
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(f);
    }
    private static int プロパティ{get;set;}
    [Fact]public void Property(){
        var f=typeof(色んなデータ型).GetProperty(nameof(プロパティ),BindingFlags.Static|BindingFlags.NonPublic);
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(f);
    }
}
