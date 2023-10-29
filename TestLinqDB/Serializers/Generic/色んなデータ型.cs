using System.Reflection;
namespace TestLinqDB.Serializers.Generic;

public abstract class 色んなデータ型: 共通{
    protected 色んなデータ型(テストオプション テストオプション):base(テストオプション){}
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
    [Fact]
    public void Anonymous000(){
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
        this.AssertEqual(value);
    }
    [Fact]
    public void ClassDisplay(){
        this.AssertEqual(ClassDisplay取得());
    }
    [Fact]
    public void Anonymous001(){
        this.AssertEqual(new{a="1"});
    }
    [Fact]
    public void Anonymous002(){
        this.AssertEqual(new{a=(object)1});
    }
    [Fact]
    public void Anonymous003(){
        this.AssertEqual(new{a=(object)new{aa=11}});
    }
    [Fact]
    public void Anonymous004(){
        this.AssertEqual(new{a=(object)new{aa=1}});
    }
    [Fact]
    public void Anonymous022(){
        this.AssertEqual(new{x=new{a=111}});
    }
    [Fact]
    public void Anonymous023(){
        this.AssertEqual(
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
        this.AssertEqual(
            Tuple.Create(1)
        );
    }
    [Fact]
    public void TupleAnonymous0(){
        this.AssertEqual(
            Tuple.Create(
                new{a=111,}
            )
        );
    }
    [Fact]
    public void TupleAnonymous1(){
        this.AssertEqual(
            Tuple.Create<object>(
                new{a=111,}
            )
        );
    }
    [Fact]public void TupleAnonymous2(){
        this.AssertEqual(
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
    [Fact]public void TupleAnonymous3(){
        this.AssertEqual(
            Tuple.Create(
                new{
                    a=111,
                }
            )
        );
    }
    [Fact]public void TupleAnonymous4(){
        this.AssertEqual(
            Tuple.Create(
                new{
                    e="eee"
                }
            )
        );
    }
    [Fact]
    public void ValueTupleAnonymous0(){
        this.AssertEqual(
            ValueTuple.Create(
                new{a=111,}
            )
        );
    }
    [Fact]
    public void ValueTupleAnonymous1(){
        this.AssertEqual(
            ValueTuple.Create<object>(
                new{a=111,}
            )
        );
    }
    [Fact]public void ValueTupleAnonymous2(){
        this.AssertEqual(
            ValueTuple.Create(
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
    [Fact]public void ValueTupleAnonymous3(){
        this.AssertEqual(
            ValueTuple.Create(
                new{
                    a=111,
                }
            )
        );
    }
    [Fact]public void ValueTupleAnonymous4(){
        this.AssertEqual(
            ValueTuple.Create(
                new{
                    e="eee"
                }
            )
        );
    }
    [Fact]
    public void Anonymous040(){
        this.AssertEqual(new{a=new{aa=1},b=new{aa=1}});
    }
    [Fact]
    public void Anonymous041(){
        this.AssertEqual(new{a=new{aa=1},b=(object)new{aa=1}});
    }
    [Fact]
    public void Anonymous05(){
        this.AssertEqual(
            Tuple.Create(
                new{a=1111}
            )
        );
    }
    [Fact]
    public void Anonymous06(){
        this.AssertEqual(
            new{a=(object)new{a=1111}}
        );
    }
    [Fact]
    public void Anonymous07(){
        this.AssertEqual(
            new[]{new{a=1111},(object)new{a=2222}}
        );
    }
    [Fact]
    public void Anonymous080(){
        this.AssertEqual(
            new[]{new{a=1111}}
        );
    }
    [Fact]
    public void Anonymous081(){
        this.AssertEqual(
            new[]{new{a=1111},new{a=2222}}
        );
    }
    [Fact]
    public void Anonymous09(){
        this.AssertEqual(
            new[]{new{a=1111},new{a=2222}}.ToList()
        );
    }
    [Fact]
    public void Anonymous10(){
        this.AssertEqual(
            (object)new{a=1111}
        );
    }
    [Fact]
    public void Anonymous11(){
        this.AssertEqual(new{a=(object)new{aa=1}});
    }
    [Fact]public void Tuple1(){
        this.AssertEqual(Tuple.Create(1));
    }
    [Fact]public void Tuple2(){
        this.AssertEqual(Tuple.Create(1,2));
    }
    [Fact]public void ValueTuple1(){
        this.AssertEqual(Tuple.Create(1));
        this.AssertEqual(ValueTuple.Create(1));
        this.AssertEqual(new{t=new{t=3}});
        this.AssertEqual(new{t=Tuple.Create(1)});
        this.AssertEqual(new{t=ValueTuple.Create(1)});
        this.AssertEqual(ValueTuple.Create(1));
    }
    [Fact]public void ValueTuple2(){
        this.AssertEqual(ValueTuple.Create(1,2));
    }
    [Fact]public void ValueTuple3(){
        this.AssertEqual<object>(ValueTuple.Create(1,2));
    }
    [Fact]public void @int(){
        this.AssertEqual(1);
    }
    private static int フィールド;
    private static Type ThisType=>typeof(色んなデータ型);
    [Fact]
    public void Field(){
        var f=ThisType.GetField(nameof(フィールド),BindingFlags.Static|BindingFlags.NonPublic);
        this.AssertEqual(f);
    }
    [Fact]
    public void Member(){
        var f=ThisType.GetField(nameof(フィールド),BindingFlags.Static|BindingFlags.NonPublic);
        this.AssertEqual(f);
    }
    [Fact]
    public void Method(){
        var f=ThisType.GetMethod("Method");
        this.AssertEqual(f);
    }
    private static int プロパティ{get;set;}
    [Fact]
    public void Property(){
        var f=ThisType.GetProperty(nameof(プロパティ),BindingFlags.Static|BindingFlags.NonPublic);
        this.AssertEqual(f);
    }
    [Fact]public void ArrayInt32(){
        this.AssertEqual(new[]{1,2,3});
    }
    [Fact]public void ArrayDouble(){
        this.AssertEqual(new double[]{1,2,3});
    }
    [Fact]public void ArrayString(){
        this.AssertEqual(new double[]{1,2,3});
    }
    [Fact]public void ArrayAnonymous(){
        this.AssertEqual(new[]{new{a=1},new{a=2}});
    }
    [Fact]public void ArrayTuple(){
        this.AssertEqual(new[]{Tuple.Create(1),Tuple.Create(2)});
    }
    [Fact]public void ArrayValueTuple(){
        this.AssertEqual(new[]{ValueTuple.Create(1),ValueTuple.Create(2)});
    }
}
