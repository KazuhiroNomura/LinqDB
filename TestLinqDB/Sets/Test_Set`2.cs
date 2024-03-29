﻿using System.Diagnostics;
using LinqDB.Sets;
using System.Text;
using System.Globalization;
//using MemoryPack;
using テスト;
using LinqDB.Helpers;
namespace TestLinqDB.Sets;
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
    [MemoryPack.MemoryPackIgnore,MessagePack.IgnoreMember]
    public string? Name{get=>this._Name;set=>this._Name=value;}
    public override bool Equals(object? obj)=>obj is SerializeEntity other&&this.Name==other.Name;
    public override int GetHashCode()=>this.Name is not null?this.Name.GetHashCode():0;
}
[MemoryPack.MemoryPackable,MessagePack.MessagePackObject,Serializable]
public partial class SerializeContainer {
    [MessagePack.Key(0)]
    public string? Name;
    [MemoryPack.MemoryPackIgnore,MessagePack.IgnoreMember]
    public SerializeSchema SerializeSchema=>this._SerializeSchema;
    [MessagePack.Key(1),MemoryPack.MemoryPackInclude,NonSerialized]
    private SerializeSchema _SerializeSchema=new();
    public override bool Equals(object? obj)=>obj is SerializeContainer other&&this.Name==other.Name&&this._SerializeSchema.Equals(other._SerializeSchema);
    public override int GetHashCode()=>this.Name is not null?this.Name.GetHashCode():0;
    //public SerializeContainer(SerializeSchema _SerializeSchema){
    //    this._SerializeSchema=_SerializeSchema;
    //}
    //public SerializeSchema SerializeSchema=>this._SerializeSchema;
}
[MemoryPack.MemoryPackable,MessagePack.MessagePackObject,Serializable]
public partial class SerializeSchema {
    [MessagePack.Key(0)]
    public string? Name;//=> nameof(シリアライズSchema);
    //public Set<シリアライズEntity> シリアライズSet1{get;}=new();
    //[MessagePack.IgnoreMember]
    [MemoryPack.MemoryPackIgnore, MessagePack.IgnoreMember]
    public Set<SerializeEntity> SerializeEntitySet1=>this._SerializeEntitySet1;
    [MessagePack.Key(1),MemoryPack.MemoryPackInclude]
    private Set<SerializeEntity> _SerializeEntitySet1;
    public SerializeSchema(){
        this._SerializeEntitySet1=new();
    }
    public override bool Equals(object? obj)=>obj is SerializeSchema other&&this.SerializeEntitySet1.SetEquals(other.SerializeEntitySet1)&&this.Name==other.Name;
    public override int GetHashCode()=>this.SerializeEntitySet1.GetHashCode();
}
[MemoryPack.MemoryPackable,MessagePack.MessagePackObject,Serializable]
public partial class SerializeSchema1 {
    public Set<SerializeEntity> SerializeEntitySet1=new();
    public override bool Equals(object? obj)=>obj is SerializeSchema1 other&&this.SerializeEntitySet1.SetEquals(other.SerializeEntitySet1);
    public override int GetHashCode()=>this.SerializeEntitySet1.GetHashCode();
}
[Serializable,MessagePack.MessagePackObject(true),MemoryPack.MemoryPackable]
public partial struct メンバー:IEquatable<メンバー> {
    public int value;

    public メンバー(int value) => this.value=value;
    public override bool Equals(object? obj) => obj is メンバー other&&this.value==other.value;
    public override int GetHashCode() => this.value/4;
    public bool Equals(メンバー other) => this.value.Equals(other.value);
    public override string ToString() => this.value.ToString(CultureInfo.CurrentCulture);
}
[Serializable,MessagePack.MessagePackObject(true),MemoryPack.MemoryPackable]
public partial struct Key:IEquatable<Key> {
    public メンバー メンバー11;

    public Key(メンバー メンバー11) => this.メンバー11=メンバー11;
    public override bool Equals(object? obj) => obj is Key other&&this.メンバー11.value==other.メンバー11.value;
    public override int GetHashCode() => this.メンバー11.GetHashCode();
    public bool Equals(Key other) => this.メンバー11.Equals(other.メンバー11);
    public override string ToString() => this.メンバー11.ToString();
}
[Serializable,MessagePack.MessagePackObject(true),MemoryPack.MemoryPackable]
public partial class Value:Entity<Key,Container>{
  //  [MemoryPack.MemoryPackIgnore]
    public メンバー Member => this.Key.メンバー11;

    public Value():base(new Key(new(0))){
    }
    [MemoryPack.MemoryPackConstructor]
    public Value(メンバー Member ) : base(new Key(Member )) {
    }

    public bool Equals(Value other) => this.Member.Equals(other.Member);
    public override bool Equals(object? obj) => obj is Value other&&this.Equals(other);
    public override int GetHashCode()=>this.Key.GetHashCode();
    public static bool operator==(Value? left,Value? right)=>Equals(left,right);
    public static bool operator!=(Value? left,Value? right)=>!Equals(left,right);
    protected override void ToStringBuilder(StringBuilder sb) => sb.Append(this.Member.ToString());
}
[Serializable,MessagePack.MessagePackObject(true),MemoryPack.MemoryPackable]
public partial class シリアライズ対象:IEquatable<シリアライズ対象> {
    public readonly int a;
    public int b;

    public シリアライズ対象(int a,int b){
        this.a=a;
        this.b=b;
    }
    public override bool Equals(object? obj) => obj is シリアライズ対象 other&& this.Equals(other);
    public override int GetHashCode() => this.a.GetHashCode();
    public bool Equals(シリアライズ対象? other) =>other is not null&&(this.a==other.a&&this.b==other.b);
    public override string ToString() => this.a.ToString();
}
public abstract class ATest_Set2:共通 {
    public ATest_Set2(テストオプション テストオプション){}
    private const int 要素数 = 100;
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
//#pragma warning disable IDE0044 // 読み取り専用修飾子を追加します
    [MessagePack.MessagePackObject]
    public class シリアライズMessagePack{
        [MessagePack.Key(0)]private int PrivateKey=0;
        [MessagePack.Key(1)]public int PublicKey { get; set; }
        [MessagePack.Key(2)]private string PrivateKeyS{get;set;}
        [MessagePack.Key(3)]public string PublicKeyS{get;set;}="";
        [MessagePack.Key(4)]
        private string? _A="";
        [MessagePack.IgnoreMember]
        public string? A{
            get=>this._A;
            set=>this._A=value;
        }
    }
#pragma warning restore CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
//#pragma warning restore IDE0044 // 読み取り専用修飾子を追加します
    [Fact]public void シリアライズMessagePack0(){
        var expected=new シリアライズMessagePack(){ PublicKeyS= "AAA"};
        var bytes = global::MessagePack.MessagePackSerializer.Serialize(expected);
        var output = global::MessagePack.MessagePackSerializer.Deserialize<シリアライズMessagePack>(bytes);
        //var bytes = global::MessagePack.MessagePackSerializer.Serialize(expected, global::MessagePack.Resolvers.StandardResolverAllowPrivate.Options);
        //var output = global::MessagePack.MessagePackSerializer.Deserialize<シリアライズMessagePack>(bytes, global::MessagePack.Resolvers.StandardResolverAllowPrivate.Options);
    }
    [Fact]public void Serialize継承しない1(){
        var expected=new Serialize継承しない(){A = "AAA"};
        this.ObjectシリアライズAssertEqual(expected);
    }
    [Fact]public void SerializeEntity0(){
        var expected=new SerializeEntity{Name = "ABCDE",A = "AAA"};
        this.ObjectシリアライズAssertEqual(expected);
    }
    public class Formatter2<T>{

    }
    public class Set2<T>:Formatter2<Set<T>>{

    }
    public class Set3<T>:MemoryPack.MemoryPackFormatter<Set<T>>  {
#pragma warning disable CA1823 // 使用されていないプライベート フィールドを使用しません
        public static readonly Set3<T> Instance = new();//リフレクションで使われる
#pragma warning restore CA1823 // 使用されていないプライベート フィールドを使用しません
        public override void Serialize<TBufferWriter>(ref MemoryPack.MemoryPackWriter<TBufferWriter> writer,scoped ref Set<T>? value){}
        public override void Deserialize(ref MemoryPack.MemoryPackReader reader,scoped ref Set<T>? value) {}
    }
    [Fact]public void Serialize00(){
        var expected = new Set<int> { 0,1,2,3 };
        {
            //var 下位0=Set3<int>.Instance;
            //global::MemoryPack.MemoryPackFormatter<Set<int>> 上位0=下位0;
            //global::MemoryPack.MemoryPackFormatterProvider.Register(LinqDB.Serializers.MemoryPack.Formatters.Set1<int>.Instance);
            var s = this.MemoryPack;
            dynamic a = new NonPublicAccessor(s);
            //var bytes = s.Serialize((object)expected);
            //global::MemoryPack.MemoryPackFormatterProvider.Register(LinqDB.Serializers.MemoryPack.Formatters.Set1<int>.Instance);
            //global::MemoryPack.MemoryPackFormatterProvider.RegisterCollection<TestSet<int>,int>();
            //var output = s.Deserialize<object>(bytes);
            SerializeDeserializeAreEqual<object>(this.MemoryPack,expected);

        }
        {
            var b = this.Utf8Json.Serialize<object>(expected);
            var json = Encoding.UTF8.GetString(b);
            var o = this.Utf8Json.Deserialize<object>(b);
        }
        {
            var b = this.MessagePack.Serialize<object>(expected);
            dynamic a = new NonPublicAccessor(this.MessagePack);
            var json = global::MessagePack.MessagePackSerializer.ConvertToJson(b,a.Options);
            var o = this.MessagePack.Deserialize<object>(b);
        }
        {
            //global::MemoryPack.MemoryPackFormatterProvider.Register(new global::MemoryPack.Formatters.CollectionFormatter<int>());
            //global::MemoryPack.MemoryPackFormatterProvider.Register(new global::MemoryPack.Formatters.InterfaceCollectionFormatter<int>());
            SerializeDeserializeAreEqual<object>(this.MemoryPack,expected);
        }
        this.ExpressionシリアライズAssertEqual(expected,output=>{
            var i=expected.LongCount;
            var o=output.LongCount;
        });
        //this.MemoryMessageJson_Assert((ImmutableSet<int>)expected,output=>{
        //    var i=expected.LongCount;
        //    var o=output.LongCount;
        //});
        //this.MemoryMessageJson_Assert((ImmutableSet)expected,output=>{
        //    var i=expected.LongCount;
        //    var o=output.LongCount;
        //});
    }
    [Fact]public void Serialize01(){
        var expected=new Set<Key,Value>{new(new(0)),new(new(1))};
        SerializeDeserializeAreEqual<object>(this.MemoryPack,expected);
    }
    [Fact]public void Serialize02(){
        var expected=new Set<Key,Value,Container>(null!){new(new(0)),new(new(1))};
        SerializeDeserializeAreEqual<object>(this.MemoryPack,expected);
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
        this.ExpressionシリアライズAssertEqual(expected,output=>{
            var i=expected.Count;
            var o=output.Count;
        });
        //this.MemoryMessageJson_Assert((TestImmutableSet<int>)expected,output=>{
        //    var i=expected.Count;
        //    var o=output.Count;
        //});
        //this.MemoryMessageJson_Assert((TestImmutableSet)expected,output=>{
        //    var i=expected.Count;
        //    var o=output.Count;
        //});
    }
    [Fact]public void Serialize20(){
        //var input=new Set<SerializeEntity>{new(){Name = "Name0",A = "A0"}};
        //this.AssertEqual全パターン(input);
        var expected=new Set<SerializeEntity>{new(){Name = "Name0",A = "A0"}};
        this.ObjectシリアライズAssertEqual<IEnumerable>(expected);
        //var bytes=global::Utf8Json.JsonSerializer.Serialize(expected);
        //var actual = global::Utf8Json.JsonSerializer.Deserialize<Set<SerializeEntity>>(bytes);
        //Assert.Equal(expected,actual);
        this.ObjectシリアライズAssertEqual(expected);
        //this.MemoryMessageJson_Assert<ImmutableSet<SerializeEntity>>(expected);
        this.ObjectシリアライズAssertEqual<LinqDB.Sets.IEnumerable<SerializeEntity>>(expected);
        //this.MemoryMessageJson_Assert<LinqDB.Sets.ICollection<SerializeEntity>>(expected);
        this.ObjectシリアライズAssertEqual<object>(expected);
    }
    [Fact]public void Serialize21(){
        var expected=new TestSet<SerializeEntity>{new(){Name = "A"}};
        var bytes=global::Utf8Json.JsonSerializer.Serialize(expected);
        var actual = global::Utf8Json.JsonSerializer.Deserialize<TestSet<SerializeEntity>>(bytes);
        Assert.Equal(expected,actual);
    }
    [Fact]public void Serialize22(){
        var expected=new Set<SerializeEntity>{new(){Name = "A"}};
        var bytes=global::Utf8Json.JsonSerializer.Serialize(expected);
        var actual = global::Utf8Json.JsonSerializer.Deserialize<Set<SerializeEntity>>(bytes);
        Assert.Equal(expected,actual);
        this.ObjectシリアライズAssertEqual(expected);
    }
    [Fact]public void Serialize3(){
        var expected=new SerializeSchema{Name = "Na"};
        var set=expected.SerializeEntitySet1;
        set.Add(new(){Name = "C"});
        this.ObjectシリアライズAssertEqual(expected);
    }
    [Fact]public void Serialize31(){
        var expected=new SerializeEntity{Name = "Na"};
        this.ObjectシリアライズAssertEqual(expected);
    }
    [Fact]
    public void Serialize32(){
        var expected=new Set<SerializeEntity>{new(){Name="Na"}};
        this.ObjectシリアライズAssertEqual(expected);
    }
    [Fact]public void Serialize33() {
        var expected = new SerializeSchema{SerializeEntitySet1 = {new(){Name = "A"},new(){Name = "B"}}};
        var set=expected.SerializeEntitySet1;
        set.Add(new(){Name = "C"});
        this.ObjectシリアライズAssertEqual(expected);
    }
    [Fact]public void Serialize330(){
        var expected = new {SerializeEntitySet1 =new Set<SerializeEntity> {new SerializeEntity{Name = "A"},new SerializeEntity{Name = "B"}}};
        var set=expected.SerializeEntitySet1;
        set.Add(new SerializeEntity{Name = "C"});
        this.ObjectシリアライズAssertEqual(expected);
    }
    [Fact]public void Serialize331(){
        var expected = new SerializeSchema1{SerializeEntitySet1 = {new(){Name = "A"},new(){Name = "B"}}};
        var set=expected.SerializeEntitySet1;
        set.Add(new SerializeEntity{Name = "C"});
        this.ObjectシリアライズAssertEqual(expected);
    }
    [Fact]public void Serialize34() {
        var expected = new Set<SerializeSchema>{new(){SerializeEntitySet1 = {new(){Name = "A"},new(){Name = "B"}}}};
        this.ObjectシリアライズAssertEqual(expected);
    }
    [Fact]public void Serialize4() {
        var expected = new SerializeContainer{SerializeSchema = { SerializeEntitySet1 = {new(){Name = "A"},new(){Name = "B"}}}};
        var set=expected.SerializeSchema.SerializeEntitySet1;
        set.Add(new(){Name = "C"});
        this.ObjectシリアライズAssertEqual(expected);
    }
    [Fact]public void Serialize5() {
        var expected = new シリアライズ対象(1,2);
        this.ObjectシリアライズAssertEqual(expected);
    }
    [Fact]public void Serialize6() {
        var expected = new Set<シリアライズ対象>();
        for(var a=0;a<10;a++){
            expected.Add(new(a,a));
        }
        this.ObjectシリアライズAssertEqual(expected);
    }
    [Fact]public void Serialize7() {
        var expected = new Set<SerializeEntity>();
        for(var a=0;a<10;a++){
            expected.Add(new SerializeEntity{Name=a.ToString()});
        }
        this.ObjectシリアライズAssertEqual(expected);
    }

    [Fact]public void Serialize8(){
        var Container=new LinqDB.Databases.Container();
        var expected =Container.information_schema.tables.ToArray();
        //var expected = new Set<LinqDB.Databases.Tables.Table,LinqDB.Databases.PrimaryKeys.Reflection>();
        //for(var a=0;a<10;a++){
        //    expected.Add(new Table(default(PropertyInfo)!,new LinqDB.Databases.Tables.Schema(default(PropertyInfo)!)));
        //}
        this.ObjectシリアライズAssertEqual(expected);
    }
    [Fact]public void Serialize9() {
        var expected = new Set<SerializeEntity>();
        for(var a=0;a<10;a++){
            expected.Add(new SerializeEntity{Name=a.ToString()});
        }
        this.ObjectシリアライズAssertEqual(expected);
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
[MemoryPack.MemoryPackableAttribute]
partial class Set1<T>{
    public int a;
}