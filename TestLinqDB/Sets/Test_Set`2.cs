﻿using System.Collections;
using System.Diagnostics;
using LinqDB.Sets;
using System.Text;
using System.Globalization;
using System.Runtime.InteropServices.ObjectiveC;
using System.Runtime.Serialization;
//using MemoryPack;
using Serializers.MessagePack.Formatters;
using テスト;
using テスト.Tables.dbo;
using LinqDB.Helpers;
using MessagePack;

namespace Sets;
[Serializable,MessagePack.MessagePackObject,MemoryPack.MemoryPackable]
public partial class Serialize継承しない{
    [MessagePack.Key(0)]
    private string? _A;
    [MessagePack.IgnoreMember]
    public string? A {
        get => this._A;
        set => this._A=value;
    }
    public override bool Equals(object? obj) => obj is Serialize継承しない other&&this.A is not null&&this.A==other.A;
    public override int GetHashCode() => this.A is not null ? this.A.GetHashCode() : 0;
}
[MessagePack.MessagePackObject]
public class SerializeEntityBase{
    [MessagePack.Key(0)]
    private string _A;
    [MessagePack.IgnoreMember]
    public string A{
        get=>this._A;
        set=>this._A=value;
    }
}
[Serializable,MessagePack.MessagePackObject,MemoryPack.MemoryPackable,MemoryPack.GenerateTypeScript]
[DebuggerDisplay("{Name}",Name = "{Name}")]
public partial class SerializeEntity:SerializeEntityBase{
    [MessagePack.Key(1)]public string? _Name;
    [MessagePack.IgnoreMember]
    public string? Name{get=>this._Name;set=>this._Name=value;}
    public override bool Equals(object? obj)=>obj is SerializeEntity other&&this.Name==other.Name;
    public override int GetHashCode()=>this.Name is not null?this.Name.GetHashCode():0;
}
public class Person {
    public string Name { get; set; }
    public int Age { get; set; }
}
[Serializable,MessagePack.MessagePackObject,MemoryPack.MemoryPackable,MemoryPack.GenerateTypeScript]
public partial class SerializeContainer {
    [MessagePack.Key(0)]
    public string? Name;
    [MessagePack.Key(1),MemoryPack.MemoryPackAllowSerialize]        
    private readonly SerializeSchema _SerializeSchema=new();
    [MessagePack.IgnoreMember,MemoryPack.MemoryPackIgnore]
    public SerializeSchema SerializeSchema=>this._SerializeSchema;
    public override bool Equals(object? obj)=>obj is SerializeEntity other&&this.Name==other.Name;
    public override int GetHashCode()=>this.Name is not null?this.Name.GetHashCode():0;
    //public SerializeContainer(SerializeSchema _SerializeSchema){
    //    this._SerializeSchema=_SerializeSchema;
    //}
    //public SerializeSchema SerializeSchema=>this._SerializeSchema;
}
[Serializable,MessagePack.MessagePackObject,MemoryPack.MemoryPackable,MemoryPack.GenerateTypeScript]
public partial class SerializeSchema {
    public string? Name;//=> nameof(シリアライズSchema);
    //public Set<シリアライズEntity> シリアライズSet1{get;}=new();
    //[MessagePack.IgnoreMember,MemoryPack.MemoryPackIgnore]
    public Set<SerializeEntity> SerializeEntitySet1=>this._SerializeEntitySet1;
    [MessagePack.Key(0),MemoryPack.MemoryPackAllowSerialize]        
    private readonly Set<SerializeEntity> _SerializeEntitySet1= new();
    public override bool Equals(object? obj)=>obj is SerializeSchema other&&this.SerializeEntitySet1.SetEquals(other.SerializeEntitySet1)&&this.Name==other.Name;
    public override int GetHashCode()=>this.SerializeEntitySet1.GetHashCode();
}
[Serializable,MessagePack.MessagePackObject,MemoryPack.MemoryPackable]
public partial struct メンバー:IEquatable<メンバー> {
    public int value;

    public メンバー(int value) => this.value=value;
    public override bool Equals(object? obj) => obj is メンバー other&&this.value==other.value;
    public override int GetHashCode() => this.value/4;
    public bool Equals(メンバー other) => this.value.Equals(other.value);
    public override string ToString() => this.value.ToString(CultureInfo.CurrentCulture);
}
[Serializable,MessagePack.MessagePackObject,MemoryPack.MemoryPackable]
public partial struct Key:IEquatable<Key> {
    public メンバー メンバー;

    public Key(メンバー メンバー) => this.メンバー=メンバー;
    public override bool Equals(object? obj) => obj is Key other&&this.メンバー.value==other.メンバー.value;
    public override int GetHashCode() => this.メンバー.GetHashCode();
    public bool Equals(Key other) => this.メンバー.Equals(other.メンバー);
    public override string ToString() => this.メンバー.ToString();
}
[Serializable,MessagePack.MessagePackObject,MemoryPack.MemoryPackable]
public partial class Value:Entity<Key,Container>,IWriteRead<Value>{
    [MemoryPack.MemoryPackIgnore]
    public メンバー Member => this.PrimaryKey.メンバー;
    [MemoryPack.MemoryPackConstructor]
    public Value():base(new Key(new(0))){
    }
    //[MemoryPack.MemoryPackConstructor]
    public Value(メンバー x) : base(new Key(x)) {
    }

    public bool Equals(Value other) => this.Member.Equals(other.Member);
    public override bool Equals(object? obj) => obj is Value other&&this.Equals(other);
    protected override void ToStringBuilder(StringBuilder sb) => sb.Append(this.Member.ToString());
    public void BinaryWrite(BinaryWriter Writer){
        throw new NotImplementedException();
    }
    public void BinaryRead(BinaryReader Reader,Func<Value> Create){
        throw new NotImplementedException();
    }
}

public class Test_Set2:共通 {
    private const int 要素数 = 100;
    [Fact]
    public void Difference1() {
        const string ファイル名 = "Difference.txt";
        using var Container = new Container();
        var List削除HashCode = new List<uint>();
        var Entity1 = Container.dbo.Entity1;
        var Entity1_Copy = new Set<Entity1, テスト.PrimaryKeys.dbo.Entity1, Container>(Container);
        Set<Entity1,テスト.PrimaryKeys.dbo.Entity1,Container> LastSet = new(Container);
        //var actual = new Set<Value,Key,Container>(Container);
        //var options = new JsonSerializerOptions {
        //    WriteIndented=true,
        //    IgnoreNullValues=true,
        //    //IgnoreReadOnlyProperties=true
        //};
        //options.Converters.Add(new SetConverter<Entity1,テスト.PrimaryKeys.dbo.Entity1,Container>(Container));
        string s;
        using(var m = new FileStream(ファイル名,FileMode.Create)) {
            using var w = new BinaryWriter(m,Encoding.UTF8);
            //using var w = new StreamWriter(m,Encoding.UTF8);
            {
                w.Write(DateTimeOffset.Now.Ticks);
                Entity1.Clear();
                for(var b = 0;b<要素数;b++) {
                    Entity1.Add(new Entity1(b));
                }
                Entity1.BinaryWrite(w);
            }
            //for(var a = 0;a<100;a++) {
            //    Entity1_Copy.Clear();
            //    for(var b = 0;b<要素数;b++) {
            //        Entity1_Copy.VoidAdd(new Entity1(a+b));
            //    }
            //    Entity1.WriteDifference(m,Entity1_Copy);
            //    LastSet=Entity1_Copy;
            //    var t = Entity1;
            //    Entity1=Entity1_Copy;
            //    Entity1_Copy=t;
            //    w.Flush();
            //}
        }
        using(var m = new FileStream(ファイル名,FileMode.Open)) {
            using var r = new BinaryReader(m,Encoding.UTF8);
            while(true) {
                s=r.ReadString();
                var Now=global::Utf8Json.JsonSerializer.Deserialize<DateTimeOffset>(s);
                var actual= new Set<Entity1,テスト.PrimaryKeys.dbo.Entity1,Container>(Container);
                //s=r.ReadString();
                //actual.Read(r);
            }
            Assert.True(Entity1.SetEquals(LastSet));
            Trace.WriteLine(LastSet.GetInformation());
        }
    }
    [MessagePack.MessagePackObject]
    public class シリアライズMessagePack{
        [MessagePack.Key(0)]private int privateKey;
        [MessagePack.Key(1)]public int PublicKey { get; set; }
        [MessagePack.Key(2)]private string privateKeyS { get; set; }
        [MessagePack.Key(3)]public string PublicKeyS { get; set; }
        [MessagePack.Key(4)]
        private string? _A;
        [MessagePack.IgnoreMember]
        public string? A{
            get=>this._A;
            set=>this._A=value;
        }
        //public override bool Equals(object? obj)=>obj is シリアライズMessagePack other&&this.A is not null&&this.A==other.A;
        //public override int GetHashCode()=>this.A is not null?this.A.GetHashCode():0;
    }
    [Fact]public void シリアライズMessagePack0(){
        var expected=new シリアライズMessagePack(){ PublicKeyS= "AAA"};
        var bytes = global::MessagePack.MessagePackSerializer.Serialize(expected);
        var output = global::MessagePack.MessagePackSerializer.Deserialize<シリアライズMessagePack>(bytes);
        //var bytes = global::MessagePack.MessagePackSerializer.Serialize(expected, global::MessagePack.Resolvers.StandardResolverAllowPrivate.Options);
        //var output = global::MessagePack.MessagePackSerializer.Deserialize<シリアライズMessagePack>(bytes, global::MessagePack.Resolvers.StandardResolverAllowPrivate.Options);
    }
    [Fact]public void Serialize継承しない1(){
        var expected=new Serialize継承しない(){A = "AAA"};
        this.MemoryMessageJson_Assert(expected,output=>Assert.Equal(expected,output));
    }
    [Fact]public void SerializeEntity0(){
        var expected=new SerializeEntity{Name = "ABCDE",A = "AAA"};
        this.MemoryMessageJson_Assert(expected,output=>Assert.Equal(expected,output));
    }
    public class Formatter2<T>{

    }
    public class Set2<T>:Formatter2<Set<T>>{

    }
    public class Set3<T>:global::MemoryPack.MemoryPackFormatter<Set<T>>  {
#pragma warning disable CA1823 // 使用されていないプライベート フィールドを使用しません
        public static readonly Set3<T> Instance = new();//リフレクションで使われる
#pragma warning restore CA1823 // 使用されていないプライベート フィールドを使用しません
        public override void Serialize<TBufferWriter>(ref global::MemoryPack.MemoryPackWriter<TBufferWriter> writer,scoped ref Set<T>? value){}
        public override void Deserialize(ref global::MemoryPack.MemoryPackReader reader,scoped ref Set<T>? value) {}
    }
    [Fact]public void Serialize00(){
        var expected=new Set<int>{0,1,2,3};
        {
            //var 下位0=Set3<int>.Instance;
            //global::MemoryPack.MemoryPackFormatter<Set<int>> 上位0=下位0;
            //global::MemoryPack.MemoryPackFormatterProvider.Register(LinqDB.Serializers.MemoryPack.Formatters.Set1<int>.Instance);
            var s = this.MemoryPack;
            var bytes = s.Serialize((object)expected);
            dynamic a = new NonPublicAccessor(s);
            //global::MemoryPack.MemoryPackFormatterProvider.Register(LinqDB.Serializers.MemoryPack.Formatters.Set1<int>.Instance);
            //global::MemoryPack.MemoryPackFormatterProvider.RegisterCollection<TestSet<int>,int>();
            var output = s.Deserialize<object>(bytes);

        }
        //{
        //    Set2<int> 下位0=default;
        //    Formatter2<Set<int>> 上位0=下位0;
        //    var 下位1=LinqDB.Serializers.MemoryPack.Formatters.Set1<int>.Instance;
        //    global::MemoryPack.Formatters.MemoryFormatter<Set<int>> 上位1 = 下位1;
        //    var Instance = LinqDB.Serializers.MemoryPack.Formatters.Set0<Set<int>,int>.Instance;
        //    var type = Instance.GetType();
        //    var BaseType = type.BaseType;
        //    var bb = BaseType.IsAssignableFrom(type);
        //    var bc = type.IsAssignableFrom(BaseType);
        //    //x=LinqDB.Serializers.MemoryPack.Formatters.Set0<Set<int>,int>.Instance;
        //    var s = this.MessagePack;
        //    var bytes = s.Serialize(expected);
        //    dynamic a = new NonPublicAccessor(s);
        //    global::MemoryPack.MemoryPackFormatterProvider.Register(LinqDB.Serializers.MemoryPack.Formatters.Set1<int>.Instance);
        //    //global::MemoryPack.MemoryPackFormatterProvider.RegisterCollection<TestSet<int>,int>();
        //    var json = global::MessagePack.MessagePackSerializer.ConvertToJson(bytes,a.Options);
        //    var output = s.Deserialize<TestSet<int>>(bytes);
        //}
        {
            var b=this.Utf8Json.Serialize<object>(expected);
            var json = Encoding.UTF8.GetString(b);
            var o=this.Utf8Json.Deserialize<object>(b);
        }
        {
            var b=this.MessagePack.Serialize<object>(expected);
            dynamic a = new NonPublicAccessor(this.MessagePack);
            var json = MessagePackSerializer.ConvertToJson(b,a.Options);
            var o=this.MessagePack.Deserialize<object>(b);
        }
        {
            //global::MemoryPack.MemoryPackFormatterProvider.Register(new global::MemoryPack.Formatters.CollectionFormatter<int>());
            //global::MemoryPack.MemoryPackFormatterProvider.Register(new global::MemoryPack.Formatters.InterfaceCollectionFormatter<int>());
            var b=this.MemoryPack.Serialize<object>(expected);
            var o=this.MemoryPack.Deserialize<object>(b);
        }
        this.MemoryMessageJson_Assert(expected,output=>{
            var i=expected.Count;
            var o=output.Count;
        });
        this.MemoryMessageJson_Assert((ImmutableSet<int>)expected,output=>{
            var i=expected.Count;
            var o=output.Count;
        });
        this.MemoryMessageJson_Assert((ImmutableSet)expected,output=>{
            var i=expected.Count;
            var o=output.Count;
        });
    }
    [Fact]public void Serialize01(){
        var expected=new Set<Value,Key>{new(new(0)),new(new(1))};
        {
            var s=this.MemoryPack;
            var bytes=s.Serialize((object)expected);
            var output=s.Deserialize<object>(bytes);
        }
    }
    [Fact]public void Serialize02(){
        var expected=new Set<Value,Key,Container>(null!){new(new(0)),new(new(1))};
        {
            var s=this.MemoryPack;
            var bytes=s.Serialize((object)expected);
            var output=s.Deserialize<object>(bytes);
        }
    }
    [Fact]public void Serialize1(){
        var expected=new TestSet<int>{0,1,2,3};
        {
            var s = this.MessagePack;
            var bytes = s.Serialize(expected);
            dynamic a = new NonPublicAccessor(s);
            //global::MemoryPack.MemoryPackFormatterProvider.Register(LinqDB.Serializers.MemoryPack.Formatters.Set1<int>.Instance);
            //global::MemoryPack.MemoryPackFormatterProvider.<TestImmutableSet<int>,int>();
            //global::MemoryPack.MemoryPackFormatterProvider.RegisterCollection<TestSet<int>,int>();
            var json = global::MessagePack.MessagePackSerializer.ConvertToJson(bytes,a.Options);
            var output = s.Deserialize<TestSet<int>>(bytes);
        }
        this.MemoryMessageJson_Assert(expected,output=>{
            var i=expected.Count;
            var o=output.Count;
        });
        this.MemoryMessageJson_Assert((TestImmutableSet<int>)expected,output=>{
            var i=expected.Count;
            var o=output.Count;
        });
        this.MemoryMessageJson_Assert((TestImmutableSet)expected,output=>{
            var i=expected.Count;
            var o=output.Count;
        });
    }
    [Fact]public void Serialize20(){
        var expected=new Set<SerializeEntity>{new(){Name = "Name0",A = "A0"}};
        //var bytes=global::Utf8Json.JsonSerializer.Serialize(expected);
        //var actual = global::Utf8Json.JsonSerializer.Deserialize<Set<SerializeEntity>>(bytes);
        //Assert.Equal(expected,actual);
        this.MemoryMessageJson_Assert(expected,actual=>Assert.Equal(expected,actual));
        this.MemoryMessageJson_Assert<ImmutableSet<SerializeEntity>>(expected,actual=>Assert.Equal(expected,actual));
        this.MemoryMessageJson_Assert<IEnumerable<SerializeEntity>>(expected,actual=>Assert.Equal(expected,actual));
        this.MemoryMessageJson_Assert<ICollection<SerializeEntity>>(expected,actual=>Assert.Equal(expected,actual));
        this.MemoryMessageJson_Assert<IEnumerable>(expected,actual=>Assert.Equal(expected,actual));
        this.MemoryMessageJson_Assert<object>(expected,actual=>Assert.Equal(expected,actual));
    }
    [Fact]public void Serialize21(){
        var expected=new TestSet<SerializeEntity>{new SerializeEntity{Name = "A"}};
        var bytes=global::Utf8Json.JsonSerializer.Serialize(expected);
        var actual = global::Utf8Json.JsonSerializer.Deserialize<Set<SerializeEntity>>(bytes);
        Assert.Equal(expected,actual);
    }
    [Fact]public void Serialize3(){
        var expected=new SerializeSchema();
        var set=expected.SerializeEntitySet1;
        set.Add(new(){Name = "C"});
        var bytes=global::Utf8Json.JsonSerializer.Serialize(expected);
        var actual = global::Utf8Json.JsonSerializer.Deserialize<SerializeSchema>(bytes);
        this.MemoryMessageJson_Assert(expected,output=>Assert.Equal(expected,output));
        Assert.Equal(expected,actual);
    }
    [Fact]public void Serialize4() {
        var expected = new SerializeContainer{SerializeSchema = { SerializeEntitySet1 = {new(){Name = "A"},new(){Name = "B"}}}};
        var set=expected.SerializeSchema.SerializeEntitySet1;
        set.Add(new(){Name = "C"});
        var bytes=global::Utf8Json.JsonSerializer.Serialize(expected);
        var actual = global::Utf8Json.JsonSerializer.Deserialize<SerializeContainer>(bytes);
        Assert.Equal(expected,actual);
    }
    [Serializable]
    public struct JsonSet<T> {
        public readonly DateTimeOffset DateTimeOffset;
        public readonly string Name;
        public readonly T Set;
        public JsonSet(DateTimeOffset DateTimeOffset,string Name,T Set) {
            this.DateTimeOffset=DateTimeOffset;
            this.Name=Name;
            this.Set=Set;
        }
    }
    private static JsonSet<T> Create<T>(DateTimeOffset DateTimeOffset,string Name,T Set) => new(DateTimeOffset,Name,Set);
}
