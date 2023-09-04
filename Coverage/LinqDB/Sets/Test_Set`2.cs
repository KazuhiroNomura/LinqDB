using System.Diagnostics;
using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Globalization;
using CoverageCS.LinqDB.テスト;
using CoverageCS.LinqDB.テスト.Tables.dbo;
//using System.Text.Json;

namespace CoverageCS.LinqDB.Sets;

[TestClass]
public class Test_Set2 {
    private const int 要素数 = 100;
    [Serializable]
    private struct メンバー:IEquatable<メンバー> {
        public int value;

        public メンバー(int value) => this.value=value;
        public override bool Equals(object? obj) => obj is メンバー other&&this.value==other.value;
        public override int GetHashCode() => this.value/4;
        public bool Equals(メンバー other) => this.value.Equals(other.value);
        public override string ToString() => this.value.ToString(CultureInfo.CurrentCulture);
    }
    [Serializable]
    private struct Key:IEquatable<Key> {
        public メンバー メンバー;

        public Key(メンバー メンバー) => this.メンバー=メンバー;
        public override bool Equals(object? obj) => obj is Key other&&this.メンバー.value==other.メンバー.value;
        public override int GetHashCode() => this.メンバー.GetHashCode();
        public bool Equals(Key other) => this.メンバー.Equals(other.メンバー);
        public override string ToString() => this.メンバー.ToString();
    }
    [Serializable]
    private class Value:Entity<Key,Container> {
        private メンバー メンバー => this.PrimaryKey.メンバー;
        public Value(メンバー メンバー) : base(new Key(メンバー)) {
        }

        public bool Equals(Value other) => this.メンバー.Equals(other.メンバー);
        public override bool Equals(object? obj) => obj is Value other&&this.Equals(other);
        protected override void ToStringBuilder(StringBuilder sb) => sb.Append(this.メンバー.ToString());
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
    //public class SetConverter<TValue, TKey, TContainer>:Json.Serialization.JsonConverter<Set<TValue,TKey,TContainer>>
    //    where TValue : Entity<TKey,TContainer>
    //    where TKey : struct, IEquatable<TKey>
    //    where TContainer : Container {
    //    private readonly TContainer Container;
    //    public SetConverter(TContainer Container) {
    //        this.Container=Container;
    //    }
    //    public override Boolean CanConvert(Type typeToConvert) {
    //        if(typeToConvert==typeof(Set<TValue,TKey,TContainer>)) {
    //            return true;
    //        }
    //        return false;
    //    }
    //    public override Set<TValue,TKey,TContainer>Read(ref Utf8JsonReader reader,Type typeToConvert,JsonSerializerOptions options) {
    //        if(typeToConvert==typeof(Set<TValue,TKey,TContainer>)) {
    //        }
    //        if(reader.TokenType==JsonTokenType.StartArray) {
    //            var Result = new Set<TValue,TKey,TContainer>(this.Container);
    //            while(true){
    //                var Element=JsonSerializer.Deserialize<TValue>(ref reader,options);
    //                Result.Add(Element);
    //                if(reader.Read()) {
    //                }else if(reader.TokenType==JsonTokenType.EndArray) break;
    //            }
    //            return Result;
    //        } else {
    //            return default!;
    //            //throw new JsonException();
    //        }
    //    }
    //    public override void Write(Utf8JsonWriter Writer,Set<TValue,TKey,TContainer> value,JsonSerializerOptions options){
    //        Writer!.WriteStartArray("Element");
    //        foreach(var item in value!) {
    //            JsonSerializer.Serialize(Writer,item,options);
    //        }
    //        Writer.WriteEndArray();
    //    }
    //}
    [TestMethod]
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
                var Now=Utf8Json.JsonSerializer.Deserialize<DateTimeOffset>(s);
                var actual= new Set<Entity1,テスト.PrimaryKeys.dbo.Entity1,Container>(Container);
                //s=r.ReadString();
                //actual.Read(r);
            }
            Assert.IsTrue(Entity1.SetEquals(LastSet));
            Trace.WriteLine(LastSet.GetInformation());
        }
    }
    [Serializable]
    public class シリアライズEntity {
        public string Name => nameof(シリアライズEntity);
    }
    [Serializable]
    public class シリアライズSchema {
        public string Name=> nameof(シリアライズSchema);
        public Set<シリアライズEntity> シリアライズSet1 { get; } = new();
    }
    [Serializable]
    public class シリアライズContainer {
        public string Name => nameof(シリアライズContainer);
        public シリアライズSchema シリアライズSchema { get; } = new();
    }
    public class Person {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    [TestMethod]
    public void Serialize() {
        const string ファイル名 = nameof(Serialize)+".txt";
        var Container = new シリアライズContainer();
        //var Container = new Person();
        string s;
        using(var m = new FileStream(ファイル名,FileMode.Create)) {
            using var w = new BinaryWriter(m,Encoding.UTF8);
            //using var w = new StreamWriter(m,Encoding.UTF8);
            Utf8Json.JsonSerializer.Serialize(m,Container);
        }
        using(var m = new FileStream(ファイル名,FileMode.Open)) {
            using var r = new BinaryReader(m,Encoding.UTF8);
            s=r.ReadString();
            var actual = Utf8Json.JsonSerializer.Deserialize<シリアライズContainer>(s);
        }
    }
}