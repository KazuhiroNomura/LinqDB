using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.ParameterExpression;
public class Parameter:IMessagePackFormatter<T> {
    public static readonly Parameter Instance=new();
    internal static void Write(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        var index=Resolver.Serializer().ListParameter.LastIndexOf(value);
        if(index>=0){
            writer.WriteArrayHeader(2);
            writer.WriteNodeType(Expressions.ExpressionType.Parameter);
            writer.Write(index);
        }else{
            writer.WriteArrayHeader(3);
            writer.WriteNodeType(Expressions.ExpressionType.Parameter);
            writer.WriteType(value.Type);
            writer.Write(value.Name);
        }
    }
    public void Serialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(2);
        writer.WriteType(value.Type);

        writer.Write(value.Name);
        
    }
    internal static T Read(ref Reader reader,MessagePackSerializerOptions Resolver,int ArrayHeader){
        var ListParameter=Resolver.Serializer().ListParameter;
        if(ArrayHeader==2){
            var index=reader.ReadInt32();
            return ListParameter[index];
        }
        Debug.Assert(ArrayHeader==3);
        var type=reader.ReadType();
        
        var name=reader.ReadString();
        var Parameter=Expressions.Expression.Parameter(type,name);
        ListParameter.Add(Parameter);
        return Parameter;
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var ArrayHeader=reader.ReadArrayHeader();
        var type=reader.ReadType();
        
        var name=reader.ReadString();
        return Expressions.Expression.Parameter(type,name);
    }
}
