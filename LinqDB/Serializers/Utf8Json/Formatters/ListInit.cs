using Utf8Json;

using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using O=IJsonFormatterResolver;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.ListInitExpression;
public class ListInit:IJsonFormatter<T> {
    public static readonly ListInit Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,O Resolver){
        New.WriteNew(ref writer,value.NewExpression,Resolver);
        writer.WriteValueSeparator();
        writer.WriteCollection(value.Initializers,Resolver);
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
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
        var @new=New.ReadNew(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Initializers=reader.ReadArray<Expressions.ElementInit>(Resolver);
        return Expressions.Expression.ListInit(@new,Initializers);
    }
    public T Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
