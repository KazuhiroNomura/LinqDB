using System.Reflection;

namespace TestLinqDB.Serializers;

public abstract class 色んなデータ型 : 共通
{
    protected 色んなデータ型(テストオプション テストオプション) { }
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
    public void Anonymous000()
    {
        var value = new
        {
            a = 1,
            b = 2.0,
            c = 3m,
            d = 4f,
            e = "e"
        };
        //var FormatterType=typeof(Anonymous<>).MakeGenericType(value.GetType());
        //dynamic formatter=Activator.CreateInstance(FormatterType)!;
        //MemoryPackFormatterProvider.Register(formatter);
        this.ObjectシリアライズAssertEqual(value);
    }
    [Fact]
    public void ClassDisplay()
    {
        this.ObjectシリアライズAssertEqual(ClassDisplay取得());
    }
    [Fact]
    public void Anonymous001()
    {
        this.ObjectシリアライズAssertEqual(new { a = "1" });
    }
    [Fact]
    public void Anonymous002()
    {
        this.ObjectシリアライズAssertEqual(new { a = (object)1 });
    }
    [Fact]
    public void Anonymous003()
    {
        this.ObjectシリアライズAssertEqual(new { a = (object)new { aa = 11 } });
    }
    [Fact]
    public void Anonymous004()
    {
        this.ObjectシリアライズAssertEqual(new { a = (object)new { aa = 1 } });
    }
    [Fact]
    public void Anonymous022()
    {
        this.ObjectシリアライズAssertEqual(new { x = new { a = 111 } });
    }
    [Fact]
    public void Anonymous023()
    {
        this.ObjectシリアライズAssertEqual(
            new
            {
                x = new
                {
                    a = 111,
                    b = 222.0,
                    c = 333m,
                    d = 444f,
                    e = "eee"
                }
            }
        );
    }
    [Fact]
    public void Anonymous030()
    {
        this.ObjectシリアライズAssertEqual(
            Tuple.Create(1)
        );
    }
    [Fact]
    public void TupleAnonymous0()
    {
        this.ObjectシリアライズAssertEqual(
            Tuple.Create(
                new { a = 111, }
            )
        );
    }
    [Fact]
    public void TupleAnonymous1()
    {
        this.ObjectシリアライズAssertEqual(
            Tuple.Create<object>(
                new { a = 111, }
            )
        );
    }
    [Fact]
    public void TupleAnonymous2()
    {
        this.ObjectシリアライズAssertEqual(
            Tuple.Create(
                new
                {
                    a = 111,
                    b = 222.0,
                    c = 333m,
                    d = 444f,
                    e = "eee"
                }
            )
        );
    }
    [Fact]
    public void TupleAnonymous3()
    {
        this.ObjectシリアライズAssertEqual(
            Tuple.Create(
                new
                {
                    a = 111,
                }
            )
        );
    }
    [Fact]
    public void TupleAnonymous4()
    {
        this.ObjectシリアライズAssertEqual(
            Tuple.Create(
                new
                {
                    e = "eee"
                }
            )
        );
    }
    [Fact]
    public void ValueTupleAnonymous0()
    {
        this.ObjectシリアライズAssertEqual(
            ValueTuple.Create(
                new { a = 111, }
            )
        );
    }
    [Fact]
    public void ValueTupleAnonymous1()
    {
        this.ObjectシリアライズAssertEqual(
            ValueTuple.Create<object>(
                new { a = 111, }
            )
        );
    }
    [Fact]
    public void ValueTupleAnonymous2()
    {
        this.ObjectシリアライズAssertEqual(
            ValueTuple.Create(
                new
                {
                    a = 111,
                    b = 222.0,
                    c = 333m,
                    d = 444f,
                    e = "eee"
                }
            )
        );
    }
    [Fact]
    public void ValueTupleAnonymous3()
    {
        this.ObjectシリアライズAssertEqual(
            ValueTuple.Create(
                new
                {
                    a = 111,
                }
            )
        );
    }
    [Fact]
    public void ValueTupleAnonymous4()
    {
        this.ObjectシリアライズAssertEqual(
            ValueTuple.Create(
                new
                {
                    e = "eee"
                }
            )
        );
    }
    [Fact]
    public void Anonymous040()
    {
        this.ObjectシリアライズAssertEqual(new { a = new { aa = 1 }, b = new { aa = 1 } });
    }
    [Fact]
    public void Anonymous041()
    {
        this.ObjectシリアライズAssertEqual(new { a = new { aa = 1 }, b = (object)new { aa = 1 } });
    }
    [Fact]
    public void Anonymous05()
    {
        this.ObjectシリアライズAssertEqual(
            Tuple.Create(
                new { a = 1111 }
            )
        );
    }
    [Fact]
    public void Anonymous06()
    {
        this.ObjectシリアライズAssertEqual(
            new { a = (object)new { a = 1111 } }
        );
    }
    [Fact]
    public void Anonymous07()
    {
        this.ObjectシリアライズAssertEqual(
            new[] { new { a = 1111 }, (object)new { a = 2222 } }
        );
    }
    [Fact]
    public void Anonymous080()
    {
        this.ObjectシリアライズAssertEqual(
            new[] { new { a = 1111 } }
        );
    }
    [Fact]
    public void Anonymous081()
    {
        this.ObjectシリアライズAssertEqual(
            new[] { new { a = 1111 }, new { a = 2222 } }
        );
    }
    [Fact]
    public void Anonymous09()
    {
        this.ObjectシリアライズAssertEqual(
            new[] { new { a = 1111 }, new { a = 2222 } }.ToList()
        );
    }
    [Fact]
    public void Anonymous10()
    {
        this.ObjectシリアライズAssertEqual(
            (object)new { a = 1111 }
        );
    }
    [Fact]
    public void Anonymous11()
    {
        this.ObjectシリアライズAssertEqual(new { a = (object)new { aa = 1 } });
    }
    [Fact]
    public void Tuple1()
    {
        this.ObjectシリアライズAssertEqual(Tuple.Create(1));
    }
    [Fact]
    public void Tuple2()
    {
        this.ObjectシリアライズAssertEqual(Tuple.Create(1, 2));
    }
    [Fact]
    public void ValueTuple1()
    {
        this.ObjectシリアライズAssertEqual(Tuple.Create(1));
        this.ObjectシリアライズAssertEqual(ValueTuple.Create(1));
        this.ObjectシリアライズAssertEqual(new { t = new { t = 3 } });
        this.ObjectシリアライズAssertEqual(new { t = Tuple.Create(1) });
        this.ObjectシリアライズAssertEqual(new { t = ValueTuple.Create(1) });
        this.ObjectシリアライズAssertEqual(ValueTuple.Create(1));
    }
    [Fact]
    public void ValueTuple2()
    {
        this.ObjectシリアライズAssertEqual(ValueTuple.Create(1, 2));
    }
    [Fact]
    public void ValueTuple3()
    {
        this.ObjectシリアライズAssertEqual<object>(ValueTuple.Create(1, 2));
    }
    [Fact]
    public void @int()
    {
        this.ObjectシリアライズAssertEqual(1);
    }
    private static int フィールド;
    private static Type ThisType => typeof(色んなデータ型);
    [Fact]
    public void Field()
    {
        var f = ThisType.GetField(nameof(フィールド), BindingFlags.Static|BindingFlags.NonPublic);
        this.ObjectシリアライズAssertEqual(f);
    }
    [Fact]
    public void Member()
    {
        var f = ThisType.GetField(nameof(フィールド), BindingFlags.Static|BindingFlags.NonPublic);
        this.ObjectシリアライズAssertEqual(f);
    }
    [Fact]
    public void Method()
    {
        var f = ThisType.GetMethod("Method");
        this.ObjectシリアライズAssertEqual(f);
    }
    private static int プロパティ { get; set; }
    [Fact]
    public void Property()
    {
        var f = ThisType.GetProperty(nameof(プロパティ), BindingFlags.Static|BindingFlags.NonPublic);
        this.ObjectシリアライズAssertEqual(f);
    }
    [Fact]
    public void ArrayInt32()
    {
        this.ObjectシリアライズAssertEqual(new[] { 1, 2, 3 });
    }
    [Fact]
    public void ArrayDouble()
    {
        this.ObjectシリアライズAssertEqual(new double[] { 1, 2, 3 });
    }
    [Fact]
    public void ArrayString()
    {
        this.ObjectシリアライズAssertEqual(new double[] { 1, 2, 3 });
    }
    [Fact]
    public void ArrayAnonymous()
    {
        this.ObjectシリアライズAssertEqual(new[] { new { a = 1 }, new { a = 2 } });
    }
    [Fact]
    public void ArrayTuple()
    {
        this.ObjectシリアライズAssertEqual(new[] { Tuple.Create(1), Tuple.Create(2) });
    }
    [Fact]
    public void ArrayValueTuple()
    {
        this.ObjectシリアライズAssertEqual(new[] { ValueTuple.Create(1), ValueTuple.Create(2) });
    }
}
