using System.Reflection;
namespace TestLinqDB.Serializers.Formatters;

public class 色んなデータ型 : 共通
{
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
        this.MemoryMessageJson_TObject(value);
    }
    [Fact]
    public void ClassDisplay()
    {
        this.MemoryMessageJson_TObject(ClassDisplay取得());
    }
    [Fact]
    public void Anonymous002()
    {
        this.MemoryMessageJson_TObject(new { a = (object)1 });
    }
    [Fact]
    public void Anonymous003()
    {
        this.MemoryMessageJson_TObject(new { a = (object)new { aa = 11 } });
    }
    [Fact]
    public void Anonymous004()
    {
        this.MemoryMessageJson_TObject(new { a = (object)new { aa = 1 } });
    }
    [Fact]
    public void Anonymous022()
    {
        this.MemoryMessageJson_TObject(new { x = new { a = 111 } });
    }
    [Fact]
    public void Anonymous023()
    {
        this.MemoryMessageJson_TObject(
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
        this.MemoryMessageJson_TObject(
            Tuple.Create(1)
        );
    }
    [Fact]
    public void Anonymous031()
    {
        this.MemoryMessageJson_TObject(
            Tuple.Create(
                new
                {
                    a = 111,
                }
            )
        );
    }
    [Fact]
    public void Anonymous032()
    {
        this.MemoryMessageJson_TObject(
            Tuple.Create<object>(
                new
                {
                    a = 111,
                }
            )
        );
    }
    [Fact]
    public void Anonymous033()
    {
        this.MemoryMessageJson_TObject(
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
    public void Anonymous040()
    {
        this.MemoryMessageJson_TObject(new
        {
            a = new { aa = 1 },
            b = new { aa = 1 }
        });
    }
    [Fact]
    public void Anonymous041()
    {
        this.MemoryMessageJson_TObject(new
        {
            a = new { aa = 1 },
            b = (object)new { aa = 1 }
        });
    }
    [Fact]
    public void Anonymous05()
    {
        this.MemoryMessageJson_TObject(
            Tuple.Create(
                new
                {
                    a = 1111
                }
            )
        );
    }
    [Fact]
    public void Anonymous06()
    {
        this.MemoryMessageJson_TObject(
            new
            {
                a = (object)new
                {
                    a = 1111
                }
            }
        );
    }
    [Fact]
    public void Anonymous07()
    {
        this.MemoryMessageJson_TObject(
            new[]{
                new{
                    a=1111
                },
                (object)new{
                    a=2222
                }
            }
        );
    }
    [Fact]public void Anonymous080()
    {
        this.MemoryMessageJson_TObject(
            new[]{
                new{
                    a=1111
                }
            }
        );
    }
    [Fact]public void Anonymous081()
    {
        this.MemoryMessageJson_TObject(
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
    public void Anonymous09()
    {
        this.MemoryMessageJson_TObject(
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
    public void Anonymous10()
    {
        this.MemoryMessageJson_TObject(
            (object)new
            {
                a = 1111
            }
        );
    }
    [Fact]
    public void Anonymous11()
    {
        this.MemoryMessageJson_TObject(new
        {
            a = (object)new { aa = 1 }
        });
    }
    private static int フィールド;
    [Fact]
    public void Field()
    {
        var f = typeof(色んなデータ型).GetField(nameof(フィールド), BindingFlags.Static|BindingFlags.NonPublic);
        this.MemoryMessageJson_TObject(f);
    }
    [Fact]
    public void Member()
    {
        MemberInfo f = typeof(色んなデータ型).GetField(nameof(フィールド), BindingFlags.Static|BindingFlags.NonPublic);
        this.MemoryMessageJson_TObject(f);
    }
    [Fact]
    public void Method()
    {
        var f = typeof(色んなデータ型).GetMethod("Method");
        this.MemoryMessageJson_TObject(f);
    }
    private static int プロパティ { get; set; }
    [Fact]
    public void Property()
    {
        var f = typeof(色んなデータ型).GetProperty(nameof(プロパティ), BindingFlags.Static|BindingFlags.NonPublic);
        this.MemoryMessageJson_TObject(f);
    }
}
