using Expressions = System.Linq.Expressions;
using Utf8Json;
using System.Diagnostics;
using LinqDB.Serializers.Utf8Json.Formatters.Reflection;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.IndexExpression;
public class Index:IJsonFormatter<T> {
    public static readonly Index Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,O Resolver){
        Expression.Write(ref writer,value.Object,Resolver);
        writer.WriteValueSeparator();
        Property.WriteNullable(ref writer,value.Indexer,Resolver);
        writer.WriteValueSeparator();
        writer.WriteCollection(value.Arguments,Resolver);
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteBeginArray();
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateWrite(ref writer,value,Resolver);
        writer.WriteEndArray(); 
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        PrivateWrite(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T Read(ref Reader reader,O Resolver){
        var instance= Expression.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var indexer= Property.ReadNullable(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
        return Expressions.Expression.MakeIndex(instance,indexer,arguments);
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
