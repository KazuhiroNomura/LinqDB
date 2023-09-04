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
using Expressions=System.Linq.Expressions;
using Newtonsoft.Json.Linq;

namespace Serializers.MessagePack.Formatters;

public class Anonymous:共通 {
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
        this.共通object2(value);
    }
    [Fact]
    public void Anonymous002(){
        this.共通object2(new{a=(object)1});
    }
    [Fact]
    public void Anonymous003(){
        this.共通object2(new{a=(object)new{aa=11}});
    }
    [Fact]
    public void Anonymous004(){
        this.共通object2(new{a=(object)new{aa=1}});
    }
    [Fact]
    public void Anonymous022(){
        this.共通object2(new{x=new{a=111}});
    }
    [Fact]
    public void Anonymous023(){
        this.共通object2(
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
        this.共通object2(
            Tuple.Create(1)
        );
    }
    [Fact]
    public void Anonymous031(){
        this.共通object2(
            Tuple.Create(
                new{
                    a=111,
                }
            )
        );
    }
    [Fact]
    public void Anonymous032(){
        this.共通object2(
            Tuple.Create<object>(
                new{
                    a=111,
                }
            )
        );
    }
    [Fact]
    public void Anonymous033(){
        this.共通object2(
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
        this.共通object2(new{
            a=new{aa=1},
            b=new{aa=1}
        });
    }
    [Fact]
    public void Anonymous041(){
        this.共通object2(new{
            a=new{aa=1},
            b=(object)new{aa=1}
        });
    }
    [Fact]
    public void Anonymous05(){
        this.共通object2(
            Tuple.Create(
                new{
                    a=1111
                }
            )
        );
    }
    [Fact]
    public void Anonymous06(){
        this.共通object2(
            new{
                a=(object)new{
                    a=1111
                }
            }
        );
    }
    [Fact]
    public void Anonymous07(){
        this.共通object2(
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
        this.共通object2(
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
        this.共通object2(
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
        this.共通object2<object>(
            (object)new{
                a=1111
            }
        );
    }
    [Fact]public void Anonymous11(){
        this.共通object2(new{
            a=(object)new{aa=1}
        });
    }
}
