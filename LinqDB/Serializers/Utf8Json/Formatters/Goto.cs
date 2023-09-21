using Utf8Json;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using O=IJsonFormatterResolver;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.GotoExpression;
public class Goto:IJsonFormatter<T> {
    public static readonly Goto Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,O Resolver){
        writer.WriteInt32((int)value.Kind);
        writer.WriteValueSeparator();
        LabelTarget.Write(ref writer,value.Target,Resolver);
        writer.WriteValueSeparator();
        if(!writer.TryWriteNil(value.Value))Expression.Write(ref writer,value.Value,Resolver);
        writer.WriteValueSeparator();
        writer.WriteType(value.Type);
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteBeginArray();
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateWrite(ref writer, value,Resolver);
        writer.WriteEndArray(); 
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        PrivateWrite(ref writer, value,Resolver);
        writer.WriteEndArray();
    }
    internal static T Read(ref Reader reader,O Resolver){
        var kind=(Expressions.GotoExpressionKind)reader.ReadInt32();
        reader.ReadIsValueSeparatorWithVerify();
        var target= LabelTarget.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var value=reader.TryReadNil()?null:Expression.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var type=reader.ReadType();
        return Expressions.Expression.MakeGoto(kind,target,value,type);
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
