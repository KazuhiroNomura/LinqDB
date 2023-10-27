//using System.Linq.Expressions;
using System.Buffers;
using MemoryPack;
//using System.Linq.Expressions;
//using Expressions = System.Linq.Expressions;
//using Binder = Microsoft.CSharp.RuntimeBinder;
//using MessagePack;
namespace TestLinqDB.Serializers.MemoryPack;
public class MemoryPackable : 共通
{
    class Class0 : IMemoryPackable<Class0>
    {
        public readonly int a;
        public readonly string b;
        public Class0(int a, string b)
        {
            this.a=a;
            this.b=b;
        }
        public static void RegisterFormatter()
        {
            if (!MemoryPackFormatterProvider.IsRegistered<Class0>())
            {
                MemoryPackFormatterProvider.Register(new global::MemoryPack.Formatters.MemoryPackableFormatter<Class0>());
            }
            if (!MemoryPackFormatterProvider.IsRegistered<Class0[]>())
            {
                MemoryPackFormatterProvider.Register(new global::MemoryPack.Formatters.ArrayFormatter<Class0>());
            }
        }
        public static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref Class0? value) where TBufferWriter : IBufferWriter<byte>
        {
            writer.DangerousWriteUnmanaged(value!.a, value.b);
        }
        public static void Deserialize(ref MemoryPackReader reader, scoped ref Class0? value)
        {
            reader.DangerousReadUnmanaged(out int a, out string b);
            value=new(a, b);
        }
    }
    [Fact]
    public void Serialize()
    {
        var expected = new Class0(1, "2");
        var bytes = MemoryPackSerializer.Serialize(expected);
        var actual = MemoryPackSerializer.Deserialize<Class0>(bytes);
    }
}
