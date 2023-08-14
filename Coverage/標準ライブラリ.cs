using LinqDB.Sets;
using System;
using System.Buffers;
using System.Linq.Expressions;
using System.Text;
using LinqDB.Optimizers;
using LinqDB.Serializers;
using MessagePack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utf8Json;
using Json=Newtonsoft.Json;
namespace CoverageCS;

[TestClass]
public class 標準ライブラリ
{
    public static ImmutableSet<LinqDB.Sets.Tables.Entity> Start(global::LinqDB.Optimizers.Target<ValueTuple<decimal>> _) => default!;
    [TestMethod]
    public void Testシグネチャデリゲート(){
        var Target = new Target<ValueTuple<decimal>>(new ValueTuple<decimal>(3));
        var Start = typeof(標準ライブラリ).GetMethod("Start")!;
        var Func = typeof(Func<>).MakeGenericType(
            Start.ReturnType
        );
        Delegate.CreateDelegate(
            Func,
            Target,
            Start
        );
    }
    [TestMethod]
    public void Testリフレクション標準(){
        const int expected=-1;
        var Tuple = new ClassTuple<int,int,int,int,int,int,int,
            ValueTuple<int,int,int,int,int,int,int,ValueTuple<int>>
        >(11,12,13,14,15,16,17,(21,22,23,24,25,26,27,28));
        var Rest0=Tuple.GetType().GetField("Rest")!.GetValue(Tuple)!;
        var Rest1=Rest0.GetType().GetField("Rest")!.GetValue(Rest0);
        Rest1.GetType().GetField("Item1")!.SetValue(Rest1,expected);
        Rest0.GetType().GetField("Rest")!.SetValue(Rest0,Rest1);
        Tuple.GetType().GetField("Rest")!.SetValue(Tuple,Rest0);
        dynamic a=Tuple;
        Assert.AreEqual(expected,a.Rest.Rest.Item1);
    }
    [TestMethod]
    public void Testリフレクション特殊0(){
        var Tuple = new ClassTuple<int,int,int,int,int,int,int>(11,12,13,14,15,16,17);
        var ref_Tuple=__makeref(Tuple);
        Tuple.GetType().GetField("Item7")!.SetValueDirect(ref_Tuple,1);
    }
    [TestMethod]
    public void Testリフレクション値型(){
        const int expected=1;
        var Tuple=new ValueTuple<int,int,int,int,int,int,int,int>();
        var ref_Tuple=__makeref(Tuple);
        Tuple.GetType().GetField("Rest")!.SetValueDirect(ref_Tuple,expected);
        Assert.AreEqual(expected,Tuple.Rest);
    }
    [TestMethod]
    public void Utf8JsonからMessagePack(){
        //{
        //  "int": 1,
        //  "double": 2.2,
        //  "array": [
        //    "ABC",
        //    3.3
        //  ]
        //}
        const string expected0="int",expected1="double",expected2="array";
        const int value0=1;
        const double value1=2.2;
        const string value2="ABC";
        const double value3=3.3;
        var SerializerSet=new SerializerSet();
        Expression<Func<int>> e=()=>1;
        var Buffer=new byte[1000];
        var w=new JsonWriter(Buffer);
        w.WriteBeginObject();
        w.WritePropertyName(expected0);
        w.WriteInt32(value0);
        w.WriteValueSeparator();
        w.WritePropertyName(expected1);
        w.WriteDouble(value1);
        w.WriteValueSeparator();
        w.WritePropertyName(expected2);
        {
            w.WriteBeginArray();
            w.WriteString(value2);
            w.WriteValueSeparator();
            w.WriteDouble(value3);
            w.WriteEndArray();
        }
        w.WriteEndObject();
        var json0=format_json(Encoding.UTF8.GetString(Buffer,0,w.CurrentOffset));
        var pack0=MessagePack.MessagePackSerializer.ConvertFromJson(json0,SerializerSet.MessagePackSerializerOptions);
        var r=new MessagePackReader(pack0);
        if(r.TryReadMapHeader(out var count)){
            var Property0=r.ReadString();
            Assert.AreEqual(expected0,Property0);
            var Value0=r.ReadInt32();
            Assert.AreEqual(value0,Value0);
            var Property1=r.ReadString();
            Assert.AreEqual(expected1,Property1);
            var Value1=r.ReadDouble();
            Assert.AreEqual(value1,Value1);
            var Property2=r.ReadString();
            Assert.AreEqual(expected2,Property2);
            if(r.TryReadArrayHeader(out var ArrayCount)){
                var Value2=r.ReadString();
                Assert.AreEqual(value2,Value2);
                var Value3=r.ReadDouble();
                Assert.AreEqual(value3,Value3);
            }

        }
    }
    [TestMethod]
    public void MessagePackからUtf8Json(){
        //{
        //  "int": 1,
        //  "double": 2.2,
        //  "array": [
        //    "ABC",
        //    3.3
        //  ]
        //}
        const string expected_int="int",expected_double="double",expected_array="array";
        const int expected1=1;
        const double expected2_2=2.2;
        const string expected_ABC="ABC";
        const double expected3_3=3.3;
        var SerializerSet=new SerializerSet();
        Expression<Func<int>> e=()=>1;
        var Buffer=new ArrayBufferWriter<byte>(1000);
        var w=new MessagePackWriter(Buffer);
        w.WriteMapHeader(3);
        w.Write(expected_int);
        w.WriteInt32(expected1);
        w.Write(expected_double);
        w.Write(expected2_2);
        w.Write(expected_array);
        w.WriteArrayHeader(2);
        w.Write(expected_ABC);
        w.Write(expected3_3);
        var pack0=MessagePack.MessagePackSerializer.ConvertToJson(Buffer.GetMemory(),SerializerSet.MessagePackSerializerOptions);
        var Bytes=Encoding.UTF8.GetBytes(pack0);
        //var Buffer=new byte[1000];
        //var w=new JsonWriter(Buffer);
        var r=new JsonReader(Bytes);
        r.ReadIsBeginObjectWithVerify();
        var @int=r.ReadString();
        Assert.AreEqual(expected_int,@int);
        r.ReadIsNameSeparatorWithVerify();
        var Value0=r.ReadInt32();
        Assert.AreEqual(expected1,Value0);
        r.ReadIsValueSeparatorWithVerify();
        var @double=r.ReadString();
        Assert.AreEqual(expected_double,@double);
        r.ReadIsNameSeparatorWithVerify();
        var Value1=r.ReadDouble();
        Assert.AreEqual(expected2_2,Value1);
        r.ReadIsValueSeparatorWithVerify();
        var array=r.ReadString();
        r.ReadIsNameSeparatorWithVerify();
        Assert.AreEqual(expected_array,array);
        {
            r.ReadIsBeginArray();
            var ABC=r.ReadString();
            Assert.AreEqual(expected_ABC,ABC);
            r.ReadIsValueSeparatorWithVerify();
            var Value3=r.ReadDouble();
            Assert.AreEqual(expected3_3,Value3);
            r.ReadIsEndArrayWithVerify();
        }
        r.ReadIsEndObjectWithVerify();
    }
    static string format_json(string json){
        dynamic parsedJson=Json.JsonConvert.DeserializeObject(json)!;
        return Json.JsonConvert.SerializeObject(parsedJson,Json.Formatting.Indented);
    }
}
