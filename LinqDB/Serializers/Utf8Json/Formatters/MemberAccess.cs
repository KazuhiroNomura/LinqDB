
using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Reflection;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.MemberExpression;
public class MemberAccess:IJsonFormatter<T> {
    public static readonly MemberAccess Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,O Resolver){
        Member.Write(ref writer,value.Member,Resolver);
        writer.WriteValueSeparator();
        Expression.WriteNullable(ref writer,value.Expression,Resolver);
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
        var member =Member.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var expression = Expression.ReadNullable(ref reader,Resolver);
        return Expressions.Expression.MakeMemberAccess(expression,member);
    }
    public T Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}

