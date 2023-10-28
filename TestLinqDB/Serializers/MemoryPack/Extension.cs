//using System.Linq.Expressions;
using MemoryPack;
using System.Reflection;
//using System.Linq.Expressions;
//using Expressions = System.Linq.Expressions;
//using Binder = Microsoft.CSharp.RuntimeBinder;
//using MemoryPack;
namespace TestLinqDB.Serializers.MemoryPack;
/// <summary>
/// privateインスタンスメンバにアクセスできるdynamicクラス
/// </summary>
public class NonPublicStaticAccessor{
    public Type Type{
        get;
        internal set;
    }
    public object? Value{
        get;
        internal set;
    }
    public NonPublicStaticAccessor(Type Type)=>this.Type=Type;
    private const BindingFlags Flags=BindingFlags.Static|BindingFlags.NonPublic|BindingFlags.Public;
    public NonPublicStaticAccessor(object Value)=>this.Value=Value;
    public NonPublicStaticAccessor Method(string Name,params object?[] Arguments)=>
        new NonPublicStaticAccessor(this.Type.GetMethod(Name,Flags)!.Invoke(null, Arguments)!);
    public NonPublicStaticAccessor Field(string Name)=>
        new NonPublicStaticAccessor(this.Type.GetField(Name,Flags)!.GetValue(this.Value)!);
    public NonPublicStaticAccessor NestedType(string Name)=>
        new NonPublicStaticAccessor(this.Type.GetNestedType(Name,BindingFlags.NonPublic|BindingFlags.Public)!);
    public NonPublicStaticAccessor NestedType(string Name,params Type[] Types){
        var Type=this.Type.GetNestedType(Name,BindingFlags.NonPublic|BindingFlags.Public)!;
        if(Types.Length>0)Type=Type.MakeGenericType(Types);
        return new NonPublicStaticAccessor(Type);
    }
}
public class Extension:共通{
    [Fact]public void StaticReadOnlyCollectionFormatter(){
    }
    [Fact]public void ctor(){
        new LinqDB.Serializers.MemoryPack.Serializer();
    }
    public static class A{
        public static class B{
            public const int C=1;
        }
    }
    [Fact]
    public void Deserialize(){
        共通(new{x=new{a=111}});
        void 共通<T>(T input0){
            var s=this.MemoryPack;
            var bytes=this.MemoryPack.Serialize(input0);
            //dynamic a=new NonPublicAccessor(typeof(MemoryPackFormatterProvider));
            //dynamic a=new NonPublicAccessor(typeof(A));
            //var B=a.B;
            //var C=B.C;
           // var Cache_1=a.Cache<T>;
            var MemoryPackFormatterProvider=new NonPublicStaticAccessor(typeof(MemoryPackFormatterProvider));
            //MemoryPackFormatterProvider.NestedType("Check`1",typeof(T)).Field("registered").Value
            dynamic formatter=MemoryPackFormatterProvider.Field("formatters").Value!;
            //formatter.Clear();
            //var formatter=Cache_1.formatter;
            //dynamic formatter=new NonPublicAccessor(Value);
            //formatter.Clear();
            var output=s.Deserialize<T>(bytes);
        }
    }
    [Fact]
    public void SerializeDeserializeTBytes(){
        var Serializer=new LinqDB.Serializers.MemoryPack.Serializer();
        var expected=1m;
        var bytes=Serializer.Serialize(expected);
        var actual=Serializer.Deserialize<decimal>(bytes);
        Assert.Equal(expected, actual);
    }
}
