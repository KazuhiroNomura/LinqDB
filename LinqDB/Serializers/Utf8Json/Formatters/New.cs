using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using static Common;
public class New:IJsonFormatter<Expressions.NewExpression>{
    public static readonly New Instance=new();
    public void Serialize(ref Writer writer,Expressions.NewExpression? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        Constructor.Instance.Serialize(ref writer,value.Constructor!,Resolver);
        writer.WriteValueSeparator();
        SerializeReadOnlyCollection(ref writer,value.Arguments,Resolver);
        writer.WriteEndArray();
        //var Arguments=value.Arguments;
        //var Arguments_Count=Arguments.Count;
        //writer.WriteBeginArray();
        //for(var a=0;a<Arguments_Count;a++)
        //    _Expression.Serialize(ref writer,Arguments[a],Resolver);
        //writer.WriteEndArray();
    }
    public Expressions.NewExpression Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var constructor= Constructor.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var arguments=DeserializeArray<Expressions.Expression>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.New(
            constructor,
            arguments
        );
    }
}
