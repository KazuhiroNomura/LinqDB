using MessagePack;
using MessagePack.Formatters;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.SwitchCase;
public class SwitchCase:IMessagePackFormatter<T> {
    public static readonly SwitchCase Instance=new();
    public void Serialize(ref Writer writer,T value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(2);
        writer.WriteCollection(value.TestValues,Resolver);
        
        Expression.Write(ref writer,value.Body,Resolver);

    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        var testValues=reader.ReadArray<Expressions.Expression>(Resolver);
        
        var body= Expression.Read(ref reader,Resolver);
        
        return Expressions.Expression.SwitchCase(body,testValues);
    }
}
