using Utf8Json;

using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using O=IJsonFormatterResolver;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.MemberListBinding;
using Reflection;
public class MemberListBinding:IJsonFormatter<T> {
    public static readonly MemberListBinding Instance=new();
    internal static void Write(ref Writer writer,T value,O Resolver){


        Member.Write(ref writer,value.Member,Resolver);
        writer.WriteValueSeparator();
        writer.WriteCollection(value.Initializers,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,O Resolver) {
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        Write(ref writer,value!,Resolver);
        writer.WriteEndArray();
    }
    internal static T Read(ref Reader reader,O Resolver){
        var member= Member.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        return Expressions.Expression.ListBind(member,reader.ReadArray<Expressions.ElementInit>(Resolver));
    }
    public T Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
