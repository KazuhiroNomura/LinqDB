using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.LambdaExpression;
using static Extension;
public class Lambda:IMessagePackFormatter<T> {
    public static readonly Lambda Instance=new();
    /// <summary>
    /// 4要素
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="Resolver"></param>
    private static void PrivateSerialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        var ListParameter=Resolver.Serializer().ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Parameters=value!.Parameters;
        ListParameter.AddRange(Parameters);
        writer.WriteType(value.Type);
        writer.Serialize宣言Parameters(value.Parameters,Resolver);
        Expression.Instance.Serialize(ref writer,value.Body,Resolver);
        writer.WriteBoolean(value.TailCall);
        ListParameter.RemoveRange(ListParameter_Count,Parameters.Count);
    }
    internal static void InternalSerialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(5);
        writer.WriteNodeType(Expressions.ExpressionType.Lambda);
        PrivateSerialize(ref writer,value,Resolver);
    }
    internal static void InternalSerializeConversion(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        if(value is null)writer.WriteNil();
        else Instance.Serialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(4);
        PrivateSerialize(ref writer,value,Resolver);
    }
    /// <summary>
    /// 4要素。Expressionから呼ばれる。
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="Resolver"></param>
    /// <returns></returns>
    internal static T InternalDeserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var ListParameter=Resolver.Serializer().ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var type=reader.ReadType();
        var parameters = reader.Deserialize宣言Parameters(Resolver);
        ListParameter.AddRange(parameters);
        var body =Expression.Instance.Deserialize(ref reader,Resolver);
        var tailCall=reader.ReadBoolean();
        ListParameter.RemoveRange(ListParameter_Count,parameters.Length);
        return Expressions.Expression.Lambda(
            type,
            body,
            tailCall,
            parameters
        );
    }
    internal static T? InternalDeserializeConversion(ref Reader reader,MessagePackSerializerOptions Resolver){
        return reader.TryReadNil()?null:Instance.Deserialize(ref reader,Resolver);
    }
    /// <summary>
    /// 自由に呼ばれる。
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="Resolver"></param>
    /// <returns></returns>
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==4);
        return InternalDeserialize(ref reader,Resolver);
    }
}
