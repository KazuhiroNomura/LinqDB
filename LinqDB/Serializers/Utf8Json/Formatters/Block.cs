
using Utf8Json;

using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using O=IJsonFormatterResolver;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.BlockExpression;
public class Block:IJsonFormatter<T> {
    public static readonly Block Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,O Resolver){
        var Variables=value.Variables;
        writer.WriteType(value.Type);
        writer.WriteValueSeparator();
        writer.Serialize宣言Parameters(Variables,Resolver);
        var Parameters=Resolver.Serializer().Parameters;
        var Parameters_Count=Parameters.Count;
        Parameters.AddRange(Variables);
        writer.WriteValueSeparator();
        writer.WriteCollection(value.Expressions,Resolver);
        Parameters.RemoveRange(Parameters_Count,Variables.Count);
    }
    public static void Write(ref Writer writer,T value,O Resolver) {
        writer.WriteBeginArray();
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateWrite(ref writer,value,Resolver);
        writer.WriteEndArray(); 
    }
    public void Serialize(ref Writer writer,T? value,O Resolver) {
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        PrivateWrite(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T Read(ref Reader reader,O Resolver){
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var variables=reader.Deserialize宣言Parameters(Resolver);
        var Parameters=Resolver.Serializer().Parameters;
        var Parameters_Count=Parameters.Count;
        Parameters.AddRange(variables);
        reader.ReadIsValueSeparatorWithVerify();
        var expressions=reader.ReadArray<Expressions.Expression>(Resolver);
        Parameters.RemoveRange(Parameters_Count,variables.Count);
        return Expressions.Expression.Block(type,variables,expressions);
    }
    public T Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
