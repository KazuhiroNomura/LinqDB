using System.Diagnostics;
using System.Reflection;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using LinqDB.Serializers.Utf8Json.Formatters;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.NewExpression;
using C=MessagePackCustomSerializer;
using static Common;
public class New:IMessagePackFormatter<Expressions.NewExpression>{
    public static readonly New Instance=new();
    public void Serialize(ref MessagePackWriter writer,Expressions.NewExpression? value,MessagePackSerializerOptions Resolver){
        Debug.Assert(value!=null,nameof(value)+" != null");
        if(value.Constructor is null)writer.WriteNil();
        else Constructor.Instance.Serialize(ref writer,value.Constructor!,Resolver);
        SerializeReadOnlyCollection(ref writer,value.Arguments,Resolver);
    }
    public Expressions.NewExpression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var constructor=reader.TryReadNil()?null:Constructor.Instance.Deserialize(ref reader,Resolver);
        var arguments=DeserializeArray<Expressions.Expression>(ref reader,Resolver);
        return Expressions.Expression.New(
            constructor,
            arguments
        );
    }
}
