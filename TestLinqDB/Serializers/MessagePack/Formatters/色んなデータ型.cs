using System.Reflection;
namespace Serializers.MessagePack.Formatters;

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
    [Fact]public void ClassDisplay(){
        this.シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス(ClassDisplay取得());
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
