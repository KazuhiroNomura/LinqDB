using Expressions=System.Linq.Expressions;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.MemberExpression;
public class MemberAccess:IJsonFormatter<T> {
    public static readonly MemberAccess Instance=new();
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        Member.Instance.Serialize(ref writer,value.Member,Resolver);
        //this.Serialize(ref writer,value.Member,Resolver);
        writer.WriteValueSeparator();
        Expression.Instance.Serialize(ref writer,value.Expression,Resolver);
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var member =Member.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var expression = Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.MakeMemberAccess(expression,member);
    }
}

