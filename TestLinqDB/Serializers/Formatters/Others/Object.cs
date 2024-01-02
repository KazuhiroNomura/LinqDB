using System.Drawing;
using System.Reflection;
using LinqDB.Sets;
namespace TestLinqDB.Serializers.Formatters.Others;
public class Object : 共通
{
    //protected override テストオプション テストオプション=>テストオプション.MemoryPack|テストオプション.MessagePack;
    //private static T serialize<T>(T input){
    //    var bytes=global::Utf8Json.JsonSerializer.Serialize(input);
    //    var output=global::Utf8Json.JsonSerializer.Deserialize<T>(bytes);
    //    return output;
    //}
    //[Fact]
    //public void Utf8JsonSerializerでシリアライズできない(){
    //    serialize(new{a=new DateTime(2,3,4)});
    //    serialize(new{a=new TimeSpan(2,3,4,5)});//System.InvalidOperationException: 'invalid datetime format. value:2.03:04:05'
    //    static void serialize<T>(T input){
    //        var bytes=global::Utf8Json.JsonSerializer.Serialize(input);
    //        var output=global::Utf8Json.JsonSerializer.Deserialize<T>(bytes);
    //    }
    //}
    [Fact]
    public void Write(){
        //switch (value){
        //    case sbyte                  v:writer.WriteSByte         (v         );break;
        this.ObjectシリアライズAssertEqual((sbyte)1);
        //    case byte                   v:writer.WriteByte          (v         );break;
        this.ObjectシリアライズAssertEqual((byte)1);
        //    case short                  v:writer.WriteInt16         (v         );break;
        this.ObjectシリアライズAssertEqual((short)1);
        //    case ushort                 v:writer.WriteUInt16        (v         );break;
        this.ObjectシリアライズAssertEqual((ushort)1);
        //    case int                    v:writer.WriteInt32         (v         );break;
        this.ObjectシリアライズAssertEqual(1);
        //    case uint                   v:writer.WriteUInt32        (v         );break;
        this.ObjectシリアライズAssertEqual((uint)1);
        //    case long                   v:writer.WriteInt64         (v         );break;
        this.ObjectシリアライズAssertEqual((long)1);
        //    case ulong                  v:writer.WriteUInt64        (v         );break;
        this.ObjectシリアライズAssertEqual((ulong)1);
        //    case float                  v:writer.WriteSingle        (v         );break;
        this.ObjectシリアライズAssertEqual((float)1);
        //    case double                 v:writer.WriteDouble        (v         );break;
        this.ObjectシリアライズAssertEqual((double)1);
        //    case bool                   v:writer.WriteBoolean       (v         );break;
        this.ObjectシリアライズAssertEqual(true);
        //    case char                   v:writer.WriteChar          (v         );break;
        this.ObjectシリアライズAssertEqual('a');
        //    case decimal                v:writer.WriteDecimal       (v,Resolver);break;
        this.ObjectシリアライズAssertEqual((decimal)1);
        //    case TimeSpan               v:writer.WriteTimeSpan      (v,Resolver);break;
        this.ObjectシリアライズAssertEqual(new{a=new TimeSpan(2,3,4,5)});
        this.ObjectシリアライズAssertEqual(new TimeSpan(1,1,1,1));
        //    case DateTime               v:writer.WriteDateTime      (v,Resolver);break;
        this.ObjectシリアライズAssertEqual(new DateTime(2001,1,1));
        //    case DateTimeOffset         v:writer.WriteDateTimeOffset(v,Resolver);break;
        this.ObjectシリアライズAssertEqual(new DateTimeOffset(2000, 8, 1, 13, 30, 0, new TimeSpan(9, 0, 0)));
        //    case string                 v:writer.WriteString        (v         );break;
        this.ObjectシリアライズAssertEqual("string");
        //    case Expressions.Expression v:writer.WriteExpression    (v,Resolver);break;
        this.ObjectシリアライズAssertEqual(System.Linq.Expressions.Expression.Constant(1));
        //    case Expressions.SymbolDocumentInfo v:writer.WriteSymbolDocumentInfo(v,Resolver);break;
        this.ObjectシリアライズAssertEqual(System.Linq.Expressions.Expression.SymbolDocument("",Guid.NewGuid(),Guid.NewGuid(),Guid.NewGuid()));
        //    case Type                   v:writer.WriteType          (v         );break;
        this.ObjectシリアライズAssertEqual(typeof(int));
        //    case ConstructorInfo        v:writer.WriteConstructor   (v,Resolver);break;
        this.ObjectシリアライズAssertEqual(typeof(Point).GetConstructors()[0]);
        //    case MethodInfo             v:writer.WriteMethod        (v,Resolver);break;
        this.ObjectシリアライズAssertEqual(typeof(string).GetMethods()[0]);
        //    case PropertyInfo           v:writer.WriteProperty      (v,Resolver);break;
        this.ObjectシリアライズAssertEqual(typeof(string).GetProperties()[0]);
        //    case EventInfo              v:writer.WriteEvent         (v,Resolver);break;
        this.ObjectシリアライズAssertEqual(typeof(Exception).GetEvents(BindingFlags.Instance|BindingFlags.NonPublic)[0]);
        //    case FieldInfo              v:writer.WriteField         (v,Resolver);break;
        this.ObjectシリアライズAssertEqual(typeof(string).GetFields()[0]);
        //    default:{
        //        if(type.IsArray){
        this.ObjectシリアライズAssertEqual(new[]{"A","B"});
        //        }else if(type.GetIEnumerableT(out var Interface)){
        this.ObjectシリアライズAssertEqual( (System.Collections.Generic.IEnumerable<int>) new List<int>());
        //        } else{
        //            if(Formatter is not null){
        {
            //Display変数
            var s = new Set<int>();
            this.ObjectシリアライズAssertEqual(() => s.Select(p => p+1));
        }
        //            } else{
        this.ObjectシリアライズAssertEqual( (object) new 演算子1());
        //            }
        //        }
        //        break;
        //    }
        //}
    }
    [Fact]
    public void Action2()
    {
        this.ObjectシリアライズAssertEqual((int a, int b) => { });
    }
    [Fact]
    public void Action1()
    {
        this.ObjectシリアライズAssertEqual((int a) => { });
    }
    [Fact]
    public void Action0()
    {
        this.ObjectシリアライズAssertEqual(() => { });
    }
    [Fact]
    public void Func3()
    {
        this.ObjectシリアライズAssertEqual((int a, int b) => a+b);
    }
    [Fact]
    public void Func2()
    {
        this.ObjectシリアライズAssertEqual((int a) => a);
    }
    [Fact]
    public void Func1()
    {
        this.ObjectシリアライズAssertEqual(() => 0);
    }
}
