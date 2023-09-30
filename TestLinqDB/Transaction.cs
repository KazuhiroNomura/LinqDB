﻿#define テキスト
using System.CodeDom.Compiler;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml;
using LinqDB.CRC;
using LinqDB.Databases;
using LinqDB.Sets;
using Serializers=LinqDB.Serializers;
//using MessagePack=LinqDB.Serializers.MessagePack;
using Serializer=LinqDB.Serializers.Utf8Json.Serializer;
// ReSharper disable ConvertIfStatementToReturnStatement
[Serializable]
public struct Struct比較対象にEquals {
    private readonly int a;
    public Struct比較対象にEquals(int a) => this.a=a;
    public static bool operator ==(Struct比較対象にEquals a,Struct比較対象にEquals b) => a.a==b.a;
    public static bool operator !=(Struct比較対象にEquals a,Struct比較対象にEquals b) => a.a!=b.a;
    public override bool Equals(object obj) {
        if(!(obj is Struct比較対象にEquals)) return false;
        var o = (Struct比較対象にEquals)obj;
        return this.a==o.a;
    }
    public override int GetHashCode() => this.a;
}
[Serializable]
public struct Struct比較対象にIEquatableEquals:IEquatable<Struct比較対象にIEquatableEquals> {
    public readonly int a;
    public Struct比較対象にIEquatableEquals(int a) => this.a=a;
    public static bool operator ==(Struct比較対象にIEquatableEquals a,Struct比較対象にIEquatableEquals b) => a.a==b.a;
    public static bool operator !=(Struct比較対象にIEquatableEquals a,Struct比較対象にIEquatableEquals b) => a.a!=b.a;
    public override bool Equals(object obj) {
        var o = (Struct比較対象にIEquatableEquals)obj;
        return this==o;
    }
    public bool Equals(Struct比較対象にIEquatableEquals other) => this==other;
    public override int GetHashCode() => this.a;
}
namespace テスト {
    [Serializable]
    public class Container:Container<Container> {
        public Schemas.dbo dbo { get; private set; }
        public override Container Transaction() {
            var Container = new Container(this);
            this.Copy(Container);
            return Container;
        }
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
        public Container()=>this.Init();
        public Container(Container? Parent) : base(Parent)=>this.Init();
        public Container(Stream logStream) : base(logStream)=>this.Init();
#pragma warning restore CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
        //public Container(Stream Reader) : base(Reader) { }
        //public Container(Stream Writer) : base(Writer) { }
        private void Init() {
            this.dbo=new Schemas.dbo(this);
        }
        protected override void Read(Stream Reader) {
            this.dbo.Read(Reader);
        }
        protected override void Write(Stream Writer) {
            Contract.Requires(Writer!=null);
            this.dbo.Write(Writer);
        }
        protected override void Copy(Container To) {
            To.dbo.Assign(this.dbo);
        }
        protected override void UpdateRelationship() {
            this.dbo.UpdateRelationship();
        }
        protected override void Commit(Stream LogStream) {
            this.dbo.Write(LogStream);
        }
    }
    namespace Schemas {
        [Serializable]
        public class dbo:Schema {
            //public Set<Int32> Int32 { get; private set; }
            private Set<Tables.dbo.Entity1,PrimaryKeys.dbo.Entity1,Container> _Entity1;
            public Set<Tables.dbo.Entity1,PrimaryKeys.dbo.Entity1,Container> Entity1 =>this._Entity1;
            private Set<Tables.dbo.Entity1,PrimaryKeys.dbo.Entity1,Container> _Entity2;
            public Set<Tables.dbo.Entity1,PrimaryKeys.dbo.Entity1,Container> Entity2 =>this._Entity2;
            public Struct比較対象にEquals struct比較対象にEquals;
            public Struct比較対象にIEquatableEquals struct比較対象にIEquatableEquals;
            public object Objectstruct比較対象にEquals = new Struct比較対象にEquals();
            public object Objectstruct比較対象にIEquatableEquals = new Struct比較対象にIEquatableEquals();
            private readonly Container Container;
            internal dbo(Container Container) {
                this.Container=Container;
                this._Entity1=new Set<Tables.dbo.Entity1,PrimaryKeys.dbo.Entity1,Container>(this.Container);
                this._Entity2=new Set<Tables.dbo.Entity1,PrimaryKeys.dbo.Entity1,Container>(this.Container);
            }
            internal void Read(Stream Reader){
                var Serializer=this.Container.Serializer;
                this._Entity1=Serializer.Deserialize<Set<Tables.dbo.Entity1,PrimaryKeys.dbo.Entity1,Container>>(Reader);
                this._Entity2=Serializer.Deserialize<Set<Tables.dbo.Entity1,PrimaryKeys.dbo.Entity1,Container>>(Reader);
                this.struct比較対象にEquals=Serializer.Deserialize<Struct比較対象にEquals>(Reader);
                this.struct比較対象にIEquatableEquals=Serializer.Deserialize<Struct比較対象にIEquatableEquals>(Reader);
                this.Objectstruct比較対象にEquals=Serializer.Deserialize<object>(Reader);
                this.Objectstruct比較対象にIEquatableEquals=Serializer.Deserialize<object>(Reader);
            }
            internal void Write(Stream Writer){
                var Serializer=this.Container.Serializer;
                Serializer.Serialize(Writer,this.Entity1);
                Serializer.Serialize(Writer,this.Entity2);
                Serializer.Serialize(Writer,this.struct比較対象にEquals);
                Serializer.Serialize(Writer,this.struct比較対象にIEquatableEquals);
                Serializer.Serialize(Writer,this.Objectstruct比較対象にEquals);
                Serializer.Serialize(Writer,this.Objectstruct比較対象にIEquatableEquals);
            }

            internal void RelationValidate() {
            }

            internal void Assign(dbo dbo) {
                this._Entity1.Assign(dbo.Entity1);
                this._Entity2.Assign(dbo.Entity2);
                this.struct比較対象にEquals=dbo.struct比較対象にEquals;
                this.struct比較対象にIEquatableEquals=dbo.struct比較対象にIEquatableEquals;
                this.Objectstruct比較対象にEquals=dbo.Objectstruct比較対象にEquals;
                this.Objectstruct比較対象にIEquatableEquals=dbo.Objectstruct比較対象にIEquatableEquals;
            }
            internal void UpdateRelationship() {
            }
        }
    }
    namespace PrimaryKeys {
        namespace dbo {
            [Serializable]
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
                public override bool Equals(object other) {
                    Contract.Assert(other!=null,"other != null");
                    return this.Equals((Entity1)other);
                }
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
            [Serializable]
            public sealed class Entity1:Entity<PrimaryKeys.dbo.Entity1,Container>, IEquatable<Entity1>, IWriteRead<Entity1> {
                [JsonIgnore]
                public decimal ID1 => this.ProtectedPrimaryKey.ID1;
                public int C_ID { get; private set; }
                public string? C_DATA { get; private set; }
                public Entity1(int C_ID,string? C_DATA = default) : base(new PrimaryKeys.dbo.Entity1(C_ID,C_ID)) {
                    this.C_ID=C_ID;
                    this.C_DATA=C_DATA;
                }
                protected override void ToStringBuilder(StringBuilder sb) {
                    this.PrimaryKey.ToStringBuilder(sb);
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

                public void BinaryWrite(BinaryWriter Writer) {
                    Writer.Write(this.C_ID);
                    if(this.C_DATA!=null) {
                        Writer.Write(true);
                        Writer.Write(this.C_DATA);
                    } else {
                        Writer.Write(false);
                    }
                }
                public void BinaryRead(BinaryReader Reader,Func<Entity1> Create) {
                }
                public void TextWrite(IndentedTextWriter Writer) {
                    Writer.Write(this.C_ID);
                    if(this.C_DATA!=null) {
                        Writer.Write(true);
                        Writer.Write(this.C_DATA);
                    } else {
                        Writer.Write(false);
                    }
                }
                public void TextRead(StreamReader Reader,int Indent) {
                    var Line=Reader.ReadLine();
                    this.C_ID=int.Parse(Line);
                    if(bool.Parse(Reader.ReadLine())) {
                        this.C_DATA=Reader.ReadLine().Substring(Indent);
                    }
                }

                public static bool operator ==(Entity1 a,Entity1 b)=>a.Equals(b);
                public static bool operator !=(Entity1 a,Entity1 b)=>!a.Equals(b);
            }
        }
    }
}

public class Transaction {
    [Fact]
    public void TestTransaction() {
        using var e = new テスト.Container();
        var e0 = e.Transaction();
        e0.dbo.Entity1.Assign(
            new Set<テスト.Tables.dbo.Entity1,テスト.PrimaryKeys.dbo.Entity1,テスト.Container>(e0){
                new(1,"1")
            }
        );
    }
    private static Set<テスト.Tables.dbo.Entity1,テスト.PrimaryKeys.dbo.Entity1,テスト.Container> 共通(テスト.Container e,int タプル濃度) {
        var result = new Set<テスト.Tables.dbo.Entity1,テスト.PrimaryKeys.dbo.Entity1,テスト.Container>(e);
        for(var a = 0;a<タプル濃度;a++) {
            var value = new テスト.Tables.dbo.Entity1(a,a.ToString());
            result.IsAdded(value);
        }
        return result;
    }
    private static readonly OnXmlDictionaryReaderClose OnXmlDictionaryReaderClose = _ => { };
    [Fact]
    public void Transactionログ1(){
        const string ファイル名 = "Transaction.xml";
        const int 回数 = 100;
        var rnd = new Random(1);
        {
            var s0 = new FileStream(ファイル名,FileMode.Create,FileAccess.Write);
            //var w0 = new BinaryWriter(s0,Encoding.UTF8);
            var 新規Container = new テスト.Container(s0);
            var 新規Container0 = 新規Container.Transaction();
            for(var b = 0;b<回数;b++) {
                新規Container0.dbo.Entity1.IsAdded(new テスト.Tables.dbo.Entity1(b));
            }
            Assert.Equal(0,新規Container.dbo.Entity1.LongCount);
            var expected=新規Container0.dbo.Entity1.LongCount;
            Assert.NotEqual(0,expected);
            新規Container0.Commit();
            //Writerがないので書き込めない。ログを読み込むことはできても書き込まない設定はどうすれば
            Assert.Equal(expected,新規Container.dbo.Entity1.LongCount);
            Assert.Equal(expected,新規Container0.dbo.Entity1.LongCount);
            新規Container.Commit();
            //w0.Close();
            s0.Close();
            var s1= new FileStream(ファイル名,FileMode.Open,FileAccess.ReadWrite);
            //var r1 = new BinaryReader(s1,Encoding.UTF8);
            //var w1 = new BinaryWriter(s1,Encoding.UTF8);
            var 復元Container = new テスト.Container(s1);
            Assert.True(復元Container.dbo.Entity1.SetEquals(新規Container.dbo.Entity1));
            Assert.Equal(復元Container.dbo.Entity1.GetInformation(),新規Container.dbo.Entity1.GetInformation());
            var 復元Container0 = 復元Container.Transaction();
            Assert.True(復元Container0.dbo.Entity1.SetEquals(新規Container0.dbo.Entity1));
            Assert.True(復元Container.dbo.Entity1.SetEquals(新規Container.dbo.Entity1));
            Assert.Equal(復元Container0.dbo.Entity1.GetInformation(),新規Container0.dbo.Entity1.GetInformation());
            Assert.Equal(復元Container.dbo.Entity1.GetInformation(),新規Container.dbo.Entity1.GetInformation());
            復元Container0.Commit();
            Assert.True(復元Container0.dbo.Entity1.SetEquals(新規Container0.dbo.Entity1));
            Assert.True(復元Container.dbo.Entity1.SetEquals(新規Container.dbo.Entity1));
            Assert.Equal(復元Container0.dbo.Entity1.GetInformation(),新規Container0.dbo.Entity1.GetInformation());
            Assert.Equal(復元Container.dbo.Entity1.GetInformation(),新規Container.dbo.Entity1.GetInformation());
            復元Container.Commit();
            //w1.Close();
            //r1.Close();
            s1.Close();
            新規Container.Dispose();
            復元Container.Dispose();
        }
    }
    [Fact]
    public void Transactionログ2() {
        const string ファイル名 = "Transaction.xml";
        Func<Stream,XmlDictionaryReader>[] Readers ={
            s=>XmlDictionaryReader.CreateTextReader(
                s,
                Encoding.UTF8,
                XmlDictionaryReaderQuotas.Max,
                OnXmlDictionaryReaderClose
            ),
            s=>XmlDictionaryReader.CreateMtomReader(
                s,
                Encoding.UTF8,
                XmlDictionaryReaderQuotas.Max
            ),
            s=>XmlDictionaryReader.CreateBinaryReader(
                s,
                XmlDictionaryReaderQuotas.Max
            )
        };
        Func<Stream,XmlDictionaryWriter>[] Writers ={
            s=>XmlDictionaryWriter.CreateTextWriter(
                s,
                Encoding.UTF8,
                false
            ),
            s=>XmlDictionaryWriter.CreateMtomWriter(
                s,
                Encoding.UTF8,
                1024,
                ""
            ),
            XmlDictionaryWriter.CreateBinaryWriter
        };
        for(var a = 0;a<Writers.Length;a++) {
            var Reader = Readers[a];
            var Writer = Writers[a];
            var s0 = new FileStream(ファイル名,FileMode.Create,FileAccess.ReadWrite);
            //var w0 = new BinaryWriter(s0,Encoding.UTF8);
            var actual = new テスト.Container(s0);
            var actual0 = actual.Transaction();
            actual0.dbo.Entity1.Assign(共通(actual0,1));
            actual0.dbo.Entity2.Assign(共通(actual0,2));
            Assert.Equal(0,actual.dbo.Entity1.LongCount());
            Assert.Equal(0,actual.dbo.Entity2.LongCount());
            Assert.Equal(1,actual0.dbo.Entity1.LongCount());
            Assert.Equal(2,actual0.dbo.Entity2.LongCount());
            actual0.Commit();
            Assert.Equal(1,actual.dbo.Entity1.LongCount());
            Assert.Equal(2,actual.dbo.Entity2.LongCount());
            Assert.Equal(1,actual0.dbo.Entity1.LongCount());
            Assert.Equal(2,actual0.dbo.Entity2.LongCount());
            actual.Commit();
            //actual.dbo.Entity1.Assign(共通(actual,dbo.Entity1_Count1));
            //actual.dbo.Entity2.Assign(共通(actual,dbo.Entity2_Count1));
            //Assert.Equal(dbo.Entity1_Count1,actual.dbo.Entity1.LongCount());
            //Assert.Equal(dbo.Entity2_Count1,actual.dbo.Entity2.LongCount());
            //w0.Close();
            s0.Close();
            var s1 = new FileStream(ファイル名,FileMode.Open,FileAccess.ReadWrite);
            //var r1 = new BinaryReader(s1,Encoding.UTF8);
            //var w1 = new BinaryWriter(s1,Encoding.UTF8);
            var expected = new テスト.Container(s1);
            Assert.True(expected.dbo.Entity1.SetEquals(actual.dbo.Entity1));
            Assert.True(expected.dbo.Entity2.SetEquals(actual.dbo.Entity2));
            var expected0 = expected.Transaction();
            Assert.True(expected0.dbo.Entity1.SetEquals(actual0.dbo.Entity1));
            Assert.True(expected0.dbo.Entity2.SetEquals(actual0.dbo.Entity2));
            Assert.True(expected.dbo.Entity1.SetEquals(actual.dbo.Entity1));
            Assert.True(expected.dbo.Entity2.SetEquals(actual.dbo.Entity2));
            expected0.Commit();
            Assert.True(expected.dbo.Entity1.SetEquals(actual.dbo.Entity1));
            Assert.True(expected.dbo.Entity2.SetEquals(actual.dbo.Entity2));
            Assert.True(expected0.dbo.Entity1.SetEquals(actual.dbo.Entity1));
            Assert.True(expected0.dbo.Entity2.SetEquals(actual.dbo.Entity2));
            expected.Commit();
            //w1.Close();
            //r1.Close();
            s1.Close();
            actual.Dispose();
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
                var O匿名型=MessagePack.MessagePackSerializer.Deserialize(Type.GetType(匿名型Type),s);
                var シリアライズデータType=MessagePack.MessagePackSerializer.Deserialize<string>(s);
                var Oシリアライズデータ=MessagePack.MessagePackSerializer.Deserialize(Type.GetType(シリアライズデータType),s);
            } catch{
            }
        }
    }
    [Fact]
    public void TransactionCommit() {
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

