using Expressions=System.Linq.Expressions;
using Utf8Json;
using System.Diagnostics;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.DynamicExpression;
using static Common;
public class Dynamic:IJsonFormatter<T> {
    public static readonly Dynamic Instance=new();
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
     //   if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        //switch(value.Binder){
        //    case BinaryOperationBinder v:{
        //        writer.WriteBeginArray();
        //        value.Binder.
        //        writer.WriteEndArray();
        //        break;
        //    }
        //    case ConvertBinder v:
        //        break;
        //}
        writer.WriteType(value.Type);
        writer.WriteValueSeparator();
        SerializeReadOnlyCollection(ref writer,value.Arguments,Resolver);
        //this.Serialize(ref writer,value.Type,Resolver);
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        //if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var arguments=DeserializeArray<Expressions.Expression>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return null!;
        //return Expressions.Expression.Dynamic(type);
    }
}
