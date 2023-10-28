#pragma warning disable CS8981 // 型名には、小文字の ASCII 文字のみが含まれています。このような名前は、プログラミング言語用に予約されている可能性があります。
#define テキスト
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using LinqDB.CRC;
using LinqDB.Databases;
using LinqDB.Sets;
using Serializers=LinqDB.Serializers;
using System.Diagnostics;
using テスト.Schemas;
// ReSharper disable ConvertIfStatementToReturnStatement
// ReSharper disable InconsistentNaming
[MemoryPack.MemoryPackable,MessagePack.MessagePackObject,Serializable]
public partial struct Struct比較対象にEquals {
    private readonly int a;
    public Struct比較対象にEquals(int a) => this.a=a;
    public static bool operator ==(Struct比較対象にEquals a,Struct比較対象にEquals b) => a.a==b.a;
    public static bool operator !=(Struct比較対象にEquals a,Struct比較対象にEquals b) => a.a!=b.a;
    public override bool Equals(object? obj)=>obj is Struct比較対象にEquals other&&this.a==other.a;
    public override int GetHashCode() => this.a;
}
[MemoryPack.MemoryPackable,MessagePack.MessagePackObject,Serializable]
public partial struct Struct比較対象にIEquatableEquals:IEquatable<Struct比較対象にIEquatableEquals> {
    public readonly int a;
    public Struct比較対象にIEquatableEquals(int a) => this.a=a;
    public static bool operator ==(Struct比較対象にIEquatableEquals a,Struct比較対象にIEquatableEquals b) => a.a==b.a;
    public static bool operator !=(Struct比較対象にIEquatableEquals a,Struct比較対象にIEquatableEquals b) => a.a!=b.a;
    public override bool Equals(object? obj)=>obj is Struct比較対象にIEquatableEquals other&&this.a==other.a;
    public bool Equals(Struct比較対象にIEquatableEquals other) => this==other;
    public override int GetHashCode() => this.a;
}
namespace テスト {
    [Serializable]
    public class Container:Container<Container> {
        public dbo dbo { get; private set; }
        public override Container Transaction() {
            var Container = new Container(this);
            this.Copy(Container);
            return Container;
        }
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
        public Container(){
        }
        public Container(Container? Parent):base(Parent){
        }
#pragma warning restore CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
        //public Container(Stream Reader) : base(Reader) { }
        //public Container(Stream Writer) : base(Writer) { }
        protected override void Read(Stream LogStream){
            try{
                this.dbo=this.Serializer.Deserialize<dbo>(LogStream);
                //this.dbo.Deserialize(LogStream);
            } catch(AggregateException ex)when(ex.InnerExceptions[0] is NotSupportedException){

            }catch(Utf8Json.JsonParsingException){}
            //new Pack
        }
        protected override void Init(){
            this.dbo=new(this);
        }
        //protected override void Init() {
        //    this.dbo=new Schemas.dbo(this);
        //}
        protected override void Write(Stream Writer) {
            Debug.Assert(Writer!=null);
            //this.dbo.Serialize(Writer);
        }
        protected override void Copy(Container To) {
            To.dbo.Assign(this.dbo);
        }
        protected override void UpdateRelationship() {
            this.dbo.UpdateRelationship();
        }
        protected override void Commit(Stream LogStream){
            this.Serializer.Serialize(LogStream,this.dbo);
        }
    }
    namespace Schemas {
        [MemoryPack.MemoryPackable,MessagePack.MessagePackObject,Serializable]
        public partial class dbo1:Schema{
            private int _a;
            public int a=>this._a;
            //[MemoryPack.MemoryPackIgnore,MessagePack.IgnoreMember,System.Runtime.Serialization.IgnoreDataMember]
            public readonly int b=0;
            [MemoryPack.MemoryPackIgnore,MessagePack.IgnoreMember,System.Runtime.Serialization.IgnoreDataMember]
            private readonly Container Container11;
            [MemoryPack.MemoryPackConstructor]
            public dbo1(int a,int b) {
                this._a=a;
                this.b=b;
            }
            public dbo1(int a) {
                this._a=a;
            }
        }
        [MemoryPack.MemoryPackable,MessagePack.MessagePackObject,Serializable]
        public partial class dbo:Schema {
            public Set<PrimaryKeys.dbo.Entity1,Tables.dbo.Entity1,Container> Entity1;
            public Set<PrimaryKeys.dbo.Entity1,Tables.dbo.Entity1,Container> Entity2;
            public Struct比較対象にEquals struct比較対象にEquals;
            public Struct比較対象にIEquatableEquals struct比較対象にIEquatableEquals;
//            public object Objectstruct比較対象にEquals;// = new Struct比較対象にEquals();
//            public object Objectstruct比較対象にIEquatableEquals = new Struct比較対象にIEquatableEquals();
            [MemoryPack.MemoryPackIgnore,MessagePack.IgnoreMember,System.Runtime.Serialization.IgnoreDataMember]
            private readonly Container Container;
            [MemoryPack.MemoryPackConstructor]
            internal dbo(Set<PrimaryKeys.dbo.Entity1,Tables.dbo.Entity1,Container> Entity1
                ,Set<PrimaryKeys.dbo.Entity1,Tables.dbo.Entity1,Container> Entity2
                ,Struct比較対象にEquals struct比較対象にEquals
                ,Struct比較対象にIEquatableEquals struct比較対象にIEquatableEquals
                 //,object Objectstruct比較対象にEquals
                 //,object Objectstruct比較対象にIEquatableEquals
                //,Container Container
                ){
                this.Entity1=Entity1;
                this.Entity2=Entity2;
                this.struct比較対象にEquals=struct比較対象にEquals;
                this.struct比較対象にIEquatableEquals=struct比較対象にIEquatableEquals;
                //this.Objectstruct比較対象にEquals=Objectstruct比較対象にEquals;
                //this.Objectstruct比較対象にIEquatableEquals=Objectstruct比較対象にIEquatableEquals;
                //this.Container=Container;
            }
            public dbo(Container Container) {
                this.Container=Container;
                this.Entity1=new Set<PrimaryKeys.dbo.Entity1,Tables.dbo.Entity1,Container>(this.Container);
                this.Entity2=new Set<PrimaryKeys.dbo.Entity1,Tables.dbo.Entity1,Container>(this.Container);
            }
            internal void Serialize(Stream Writer){
                var Serializer=this.Container.Serializer;
                Serializer.Serialize(Writer,this.Entity1);
                Serializer.Serialize(Writer,this.Entity2);
                Serializer.Serialize(Writer,this.struct比較対象にEquals);
                Serializer.Serialize(Writer,this.struct比較対象にIEquatableEquals);
                //Serializer.Serialize(Writer,this.Objectstruct比較対象にEquals);
                //Serializer.Serialize(Writer,this.Objectstruct比較対象にIEquatableEquals);
            }
            internal void Deserialize(Stream Reader){
                var Serializer=this.Container.Serializer;
                var _Entity1=Serializer.Deserialize<Set<PrimaryKeys.dbo.Entity1,Tables.dbo.Entity1,Container>>(Reader);
                var _Entity2=Serializer.Deserialize<Set<PrimaryKeys.dbo.Entity1,Tables.dbo.Entity1,Container>>(Reader);
                var struct比較対象にEquals=Serializer.Deserialize<Struct比較対象にEquals>(Reader);
                var struct比較対象にIEquatableEquals=Serializer.Deserialize<Struct比較対象にIEquatableEquals>(Reader);
                var Objectstruct比較対象にEquals=Serializer.Deserialize<object>(Reader);
                var Objectstruct比較対象にIEquatableEquals=Serializer.Deserialize<object>(Reader);
                this.Entity1=_Entity1;
                this.Entity2=_Entity2;
                this.struct比較対象にEquals=struct比較対象にEquals;
                this.struct比較対象にIEquatableEquals=struct比較対象にIEquatableEquals;
                //this.Objectstruct比較対象にEquals=Objectstruct比較対象にEquals;
                //this.Objectstruct比較対象にIEquatableEquals=Objectstruct比較対象にIEquatableEquals;
            }

            internal void RelationValidate() {
            }

            internal void Assign(dbo dbo) {
                this.Entity1.Assign(dbo.Entity1);
                this.Entity2.Assign(dbo.Entity2);
                this.struct比較対象にEquals=dbo.struct比較対象にEquals;
                this.struct比較対象にIEquatableEquals=dbo.struct比較対象にIEquatableEquals;
                //this.Objectstruct比較対象にEquals=dbo.Objectstruct比較対象にEquals;
//                this.Objectstruct比較対象にIEquatableEquals=dbo.Objectstruct比較対象にIEquatableEquals;
            }
            internal void UpdateRelationship() {
            }
        }
    }
    namespace PrimaryKeys {
        namespace dbo {
            [Serializable,MessagePack.MessagePackObject(true)]
            public struct Entity1:IEquatable<Entity1> {
                public decimal ID1 {
                    get;
                }
                public decimal ID2 {
                    get;
                }
                public Entity1(decimal ID1,decimal ID2) {
                    this.ID1=ID1;
                    this.ID2=ID2;
                }
                public bool Equals(Entity1 other) {
                    // ReSharper disable once PossibleNullReferenceException
                    if(!this.ID1.Equals(other.ID1))
                        return false;
                    // ReSharper disable once ConvertIfStatementToReturnStatement
                    if(!this.ID2.Equals(other.ID2))
                        return false;
                    return true;
                }
                public override bool Equals(object? obj)=>obj is Entity1 other&&this.Equals((Entity1)other);
                public override int GetHashCode() {
                    var CRC = new CRC32();
                    CRC.Input(this.ID1);
                    CRC.Input(this.ID2);
                    return CRC.GetHashCode();
                }
                public static bool operator ==(Entity1 x,Entity1 y) => x.Equals(y);
                public static bool operator !=(Entity1 x,Entity1 y) => !x.Equals(y);
                public int CompareTo(Entity1 other) => this.GetHashCode()-other.GetHashCode();
                public override string ToString()
                    => "ID1="+this.ID1.ToString(CultureInfo.CurrentCulture)+",ID2="+this.ID2.ToString(CultureInfo.CurrentCulture);
                internal void ToStringBuilder(StringBuilder sb) {
                    sb.Append("ID1="+this.ID1);
                    sb.Append("ID2="+this.ID2);
                }
            }
        }
    }
    namespace Tables {
        namespace dbo {
            [MemoryPack.MemoryPackable,MessagePack.MessagePackObject(true),Serializable]
            public sealed partial class Entity1:Entity<PrimaryKeys.dbo.Entity1,Container>, IEquatable<Entity1>{
                public decimal ID1 => this.ProtectKey.ID1;
                public int C_ID { get; private set; }
                public string? C_DATA { get; private set; }
                public Entity1(int C_ID,string? C_DATA = default) : base(new PrimaryKeys.dbo.Entity1(C_ID,C_ID)) {
                    this.C_ID=C_ID;
                    this.C_DATA=C_DATA;
                }
                protected override void ToStringBuilder(StringBuilder sb) {
                    this.Key.ToStringBuilder(sb);
                    sb.Append(",C_ID=");
                    sb.Append(this.C_ID);
                    sb.Append(",C_DATA=");
                    sb.Append(this.C_DATA);
                }
                private bool PrivateEquals(Entity1 other) {
                    if(!this.C_ID.Equals(other.C_ID)) return false;
                    if(this.C_DATA!=other.C_DATA) return false;
                    return true;
                }
                public bool Equals(Entity1? other) {
                    if(other is null)return false;
                    return this.PrivateEquals(other);
                }
                public override bool Equals(object? obj) {
                    if(obj is Entity1 other)
                        return this.PrivateEquals(other);
                    return false;
                }
                public override int GetHashCode()=>HashCode.Combine(this.Key,this.C_ID);
                public static bool operator ==(Entity1? a,Entity1? b)=>a is not null?a.Equals(b):ReferenceEquals(a,b);
                public static bool operator !=(Entity1? a,Entity1? b)=>!(a==b);
            }
        }
    }
}

public class Transaction {
    [Fact]public void TestTransaction() {
        using var e = new テスト.Container();
        var e0 = e.Transaction();
        e0.dbo.Entity1.Assign(
            new Set<テスト.PrimaryKeys.dbo.Entity1,テスト.Tables.dbo.Entity1,テスト.Container>(e0){
                new(1,"1")
            }
        );
    }
    private static Set<テスト.PrimaryKeys.dbo.Entity1,テスト.Tables.dbo.Entity1,テスト.Container> 共通(テスト.Container e,int タプル濃度) {
        var result = new Set<テスト.PrimaryKeys.dbo.Entity1,テスト.Tables.dbo.Entity1,テスト.Container>(e);
        for(var a = 0;a<タプル濃度;a++) {
            var value = new テスト.Tables.dbo.Entity1(a,a.ToString());
            result.IsAdded(value);
        }
        return result;
    }
    [Fact]public void 連続書き込み0(){
        共通連続書き込み(new Serializers.MemoryPack.Serializer());
        共通連続書き込み(new Serializers.MessagePack.Serializer());
        共通連続書き込み(new Serializers.Utf8Json.Serializer());
        static void 共通連続書き込み(Serializers.Serializer Serializer){
            const int 回数 = 100;
            var expected=new Set<テスト.PrimaryKeys.dbo.Entity1,テスト.Tables.dbo.Entity1,テスト.Container>(null!);
            for(var b = 0;b<回数;b++) {
                expected.IsAdded(new テスト.Tables.dbo.Entity1(b));
            }
            {
                var bytes=Serializer.Serialize(expected);
                var actual=Serializer.Deserialize<Set<テスト.PrimaryKeys.dbo.Entity1,テスト.Tables.dbo.Entity1,テスト.Container>>(bytes);
                Assert.Equal(expected,actual);
            }
            {
                var stream = new MemoryStream();
                Serializer.Serialize(stream,expected);
                stream.Position=0;
                var actual=Serializer.Deserialize<Set<テスト.PrimaryKeys.dbo.Entity1,テスト.Tables.dbo.Entity1,テスト.Container>>(stream);
                Assert.Equal(expected,actual);
            }
        }
    }
    [Fact]public void 連続書き込み1(){
        共通連続書き込み(new Serializers.MemoryPack.Serializer());
        共通連続書き込み(new Serializers.MessagePack.Serializer());
        static void 共通連続書き込み(Serializers.Serializer Serializer){
            const int 回数 = 100;
            var expected0=new Set<テスト.PrimaryKeys.dbo.Entity1,テスト.Tables.dbo.Entity1,テスト.Container>(null!);
            var expected2=new Set<テスト.PrimaryKeys.dbo.Entity1,テスト.Tables.dbo.Entity1,テスト.Container>(null!);
            for(var b = 0;b<回数;b++) {
                expected0.IsAdded(new テスト.Tables.dbo.Entity1(b));
                expected2.IsAdded(new テスト.Tables.dbo.Entity1(b+10));
            }
            var stream = new MemoryStream();
            const string expected1="abc";
            Serializer.Serialize(stream,expected0);
            var length0=stream.Length;
            Serializer.Serialize(stream,expected1);
            var length1=stream.Length;
            Serializer.Serialize(stream,expected0);
            var length2=stream.Length;
            stream.Position=0;
            var actual0=Serializer.Deserialize<Set<テスト.PrimaryKeys.dbo.Entity1,テスト.Tables.dbo.Entity1,テスト.Container>>(stream);
            var actual1=Serializer.Deserialize<string>(stream);
            var actual2=Serializer.Deserialize<Set<テスト.PrimaryKeys.dbo.Entity1,テスト.Tables.dbo.Entity1,テスト.Container>>(stream);
            Assert.Equal(expected0,actual0);
        }
    }
    [Fact]public void Transactionログ1(){
        const int 回数 = 100;
        Container.ClearLog();
        var 新規Container0 = new テスト.Container();
        var 新規Container1 = 新規Container0.Transaction();
        for(var b = 0;b<回数;b++)
            新規Container1.dbo.Entity1.IsAdded(new テスト.Tables.dbo.Entity1(b));
        Assert.Equal(0,新規Container0.dbo.Entity1.LongCount);
        var expected=新規Container1.dbo.Entity1.LongCount;
        Assert.NotEqual(0,expected);
        新規Container1.Commit();
        //Writerがないので書き込めない。ログを読み込むことはできても書き込まない設定はどうすれば
        Assert.Equal(expected,新規Container0.dbo.Entity1.LongCount);
        Assert.Equal(expected,新規Container1.dbo.Entity1.LongCount);
        新規Container0.Commit();
        //w0.Close();
        //s0.Close();
        //var s1= new FileStream(ファイル名,FileMode.Open,FileAccess.ReadWrite);
        //var r1 = new BinaryReader(s1,Encoding.UTF8);
        //var w1 = new BinaryWriter(s1,Encoding.UTF8);
        var 復元Container = new テスト.Container();
        Assert.True(復元Container.dbo.Entity1.SetEquals(新規Container0.dbo.Entity1));
        Assert.Equal(復元Container.dbo.Entity1.GetInformation(),新規Container0.dbo.Entity1.GetInformation());
        var 復元Container0 = 復元Container.Transaction();
        Assert.True(復元Container0.dbo.Entity1.SetEquals(新規Container1.dbo.Entity1));
        Assert.True(復元Container.dbo.Entity1.SetEquals(新規Container0.dbo.Entity1));
        Assert.Equal(復元Container0.dbo.Entity1.GetInformation(),新規Container1.dbo.Entity1.GetInformation());
        Assert.Equal(復元Container.dbo.Entity1.GetInformation(),新規Container0.dbo.Entity1.GetInformation());
        復元Container0.Commit();
        Assert.True(復元Container0.dbo.Entity1.SetEquals(新規Container1.dbo.Entity1));
        Assert.True(復元Container.dbo.Entity1.SetEquals(新規Container0.dbo.Entity1));
        Assert.Equal(復元Container0.dbo.Entity1.GetInformation(),新規Container1.dbo.Entity1.GetInformation());
        Assert.Equal(復元Container.dbo.Entity1.GetInformation(),新規Container0.dbo.Entity1.GetInformation());
        復元Container.Commit();
        新規Container0.Dispose();
        復元Container.Dispose();
    }
    [Fact]
    public void Transactionログ2() {
        const string ファイル名 = "Transaction.xml";
        {
            Container.ClearLog();
            var actual0 = new テスト.Container();
            var actual1 = actual0.Transaction();
            actual1.dbo.Entity1.Assign(共通(actual1,1));
            actual1.dbo.Entity2.Assign(共通(actual1,2));
            Assert.Equal(0,actual0.dbo.Entity1.LongCount());
            Assert.Equal(0,actual0.dbo.Entity2.LongCount());
            Assert.Equal(1,actual1.dbo.Entity1.LongCount());
            Assert.Equal(2,actual1.dbo.Entity2.LongCount());
            actual1.Commit();
            Assert.Equal(1,actual0.dbo.Entity1.LongCount());
            Assert.Equal(2,actual0.dbo.Entity2.LongCount());
            Assert.Equal(1,actual1.dbo.Entity1.LongCount());
            Assert.Equal(2,actual1.dbo.Entity2.LongCount());
            actual0.Commit();
            var expected = new テスト.Container();
            Assert.True(expected.dbo.Entity1.SetEquals(actual0.dbo.Entity1));
            Assert.True(expected.dbo.Entity2.SetEquals(actual0.dbo.Entity2));
            var expected0 = expected.Transaction();
            Assert.True(expected0.dbo.Entity1.SetEquals(actual1.dbo.Entity1));
            Assert.True(expected0.dbo.Entity2.SetEquals(actual1.dbo.Entity2));
            Assert.True(expected.dbo.Entity1.SetEquals(actual0.dbo.Entity1));
            Assert.True(expected.dbo.Entity2.SetEquals(actual0.dbo.Entity2));
            expected0.Commit();
            Assert.True(expected.dbo.Entity1.SetEquals(actual0.dbo.Entity1));
            Assert.True(expected.dbo.Entity2.SetEquals(actual0.dbo.Entity2));
            Assert.True(expected0.dbo.Entity1.SetEquals(actual0.dbo.Entity1));
            Assert.True(expected0.dbo.Entity2.SetEquals(actual0.dbo.Entity2));
            expected.Commit();
            //w1.Close();
            //r1.Close();
            //s1.Close();
            actual0.Dispose();
            expected.Dispose();
        }
    }

    private static Expression<Func<int[],System.Collections.Generic.IEnumerable<int>>> シリアライズデータ=>
        入力=>
            from a in 入力
            join b in 入力 on a equals b
            select a+b;
    [Fact]
    public void MessagePack動作の確認() {
        const string タグ1 = nameof(タグ1);
        const string タグ2 = nameof(タグ2);
        const string 値 = nameof(値);
        //Utf8Json.JsonSerializer.Serialize();
        //MessagePack.MessagePackSerializer.Serialize()
        //var d = "ABC";
        //var d = typeof(String);
        using(var s=new FileStream("MessagePack動作の確認.json",FileMode.Create,FileAccess.ReadWrite)){
            try{
                var I匿名型=new{a=3,b=4};
                MessagePack.MessagePackSerializer.Serialize(s,this.GetType().FullName,
                    MessagePack.MessagePackSerializerOptions.Standard);
                MessagePack.MessagePackSerializer.Serialize(s,this);
                MessagePack.MessagePackSerializer.Serialize(s,I匿名型.GetType().FullName);
                MessagePack.MessagePackSerializer.Serialize(s,I匿名型);
                MessagePack.MessagePackSerializer.Serialize(s,シリアライズデータ.GetType().FullName);
                MessagePack.MessagePackSerializer.Serialize(s,シリアライズデータ);
                s.Position=0;
                var 匿名型Type=MessagePack.MessagePackSerializer.Deserialize<string>(s);
                var O匿名型=MessagePack.MessagePackSerializer.Deserialize(Type.GetType(匿名型Type)!,s);
                var シリアライズデータType=MessagePack.MessagePackSerializer.Deserialize<string>(s);
                var Oシリアライズデータ=MessagePack.MessagePackSerializer.Deserialize(Type.GetType(シリアライズデータType)!,s);
            } catch{
            }
        }
    }
    [Fact]
    public void TransactionCommit() {
        Container.ClearLog();
        using var e = new テスト.Container();
        var e0 = e.Transaction();
        var expected1 = 共通(e0,1);
        var expected2 = 共通(e0,1);
        {
            e0.dbo.Entity1.Assign(expected1);
            e0.dbo.Entity2.Assign(expected2);
            Assert.True(expected1.SetEquals(e0.dbo.Entity1));
            Assert.True(expected2.SetEquals(e0.dbo.Entity2));
            Assert.False(expected1.SetEquals(e.dbo.Entity1));
            Assert.False(expected2.SetEquals(e.dbo.Entity2));
        }
        e0.Commit();
        Assert.True(expected1.SetEquals(e.dbo.Entity1));
        Assert.True(expected2.SetEquals(e.dbo.Entity2));
    }
}

