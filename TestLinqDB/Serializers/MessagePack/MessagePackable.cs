﻿using MessagePack;
namespace TestLinqDB.Serializers.MessagePack;
public class MessagePackable : 共通
{
    [MessagePackObject(true)]
    class Class0
    {
        public int a;
        public string b;
        public Class0(int a, string b)
        {
            this.a=a;
            this.b=b;
        }
    }
    [Fact]
    public void Serialize0()
    {
        //var expected=new Class0();
        var expected = new Class0(1, "2");
        var bytes = this.MessagePack.Serialize<object>(expected);
        var actual = this.MessagePack.Deserialize<object>(bytes);
    }
    //[Fact]public void Serialize1(){
    //    //var expected=new Class0();
    //    var expected=new Class0(1,"2");
    //    var o=MessagePackSerializerOptions.Standard.WithResolver(StandardResolver.Instance);
    //    var bytes=MessagePackSerializer.Serialize(expected,o);
    //    var actual=MessagePackSerializer.Deserialize<Class0>(bytes);
    //}
}
