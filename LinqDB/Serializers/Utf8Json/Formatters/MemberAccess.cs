using Expressions=System.Linq.Expressions;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.MemberExpression;
public class MemberAccess:IJsonFormatter<T> {
    public static readonly MemberAccess Instance=new();
    private static void PrivateSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        Member.Write(ref writer,value.Member,Resolver);
        writer.WriteValueSeparator();
        Expression.WriteNullable(ref writer,value.Expression,Resolver);
    }
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateSerialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        PrivateSerialize(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T Read(ref Reader reader,IJsonFormatterResolver Resolver){
        var member =Member.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var expression = Expression.ReadNullable(ref reader,Resolver);
        return Expressions.Expression.MakeMemberAccess(expression,member);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver) {
        if(reader.TryReadNil())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}

