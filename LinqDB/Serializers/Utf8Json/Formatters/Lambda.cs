
using Utf8Json;

using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using O=IJsonFormatterResolver;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.LambdaExpression;
public class Lambda:IJsonFormatter<T> {
    public static readonly Lambda Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,O Resolver) {
        writer.WriteType(value.Type);
        writer.WriteValueSeparator();
        writer.Serialize宣言Parameters(value.Parameters,Resolver);
        var Parameters=Resolver.Serializer().Parameters;
        var Parameters_Count=Parameters.Count;
        var value_Parameters=value.Parameters;
        Parameters.AddRange(value_Parameters);
        writer.WriteValueSeparator();
        Expression.Write(ref writer,value.Body,Resolver);
        writer.WriteValueSeparator();
        writer.WriteBoolean(value.TailCall);
        Parameters.RemoveRange(Parameters_Count,value_Parameters.Count);
        writer.WriteEndArray();
    }
    internal static void Write(ref Writer writer,T value,O Resolver) {
        writer.WriteBeginArray();
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateWrite(ref writer,value,Resolver);
    }
    internal static void WriteNullable(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        PrivateWrite(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,O Resolver)=>WriteNullable(ref writer,value,Resolver);
    internal static T Read(ref Reader reader,O Resolver){
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var parameters=reader.Deserialize宣言Parameters(Resolver);
        var Parameters=Resolver.Serializer().Parameters;
        var Parameters_Count=Parameters.Count;
        Parameters.AddRange(parameters);
        reader.ReadIsValueSeparatorWithVerify();
        var body=Expression.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var tailCall=reader.ReadBoolean();
        Parameters.RemoveRange(Parameters_Count,parameters.Count);
        return Expressions.Expression.Lambda(
            type,
            body,
            tailCall,
            parameters
        );
    }
    internal static T? ReadNullable(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null;
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
    public T Deserialize(ref Reader reader,O Resolver)=>ReadNullable(ref reader,Resolver)!;
}
