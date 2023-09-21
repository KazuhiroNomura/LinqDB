using Expressions = System.Linq.Expressions;

using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader = MemoryPackReader;

public class ExpressionT<T>:MemoryPackFormatter<T>where T:Expressions.LambdaExpression {
    public static readonly ExpressionT<T> Instance=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        var ListParameter= writer.Serializer().ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Parameters=value!.Parameters;
        ListParameter.AddRange(Parameters);
        writer.WriteType(value.Type);
        writer.Serialize宣言Parameters(value.Parameters);
        Expression.Write(ref writer,value.Body);
        writer.WriteBoolean(value.TailCall);
        
        ListParameter.RemoveRange(ListParameter_Count,Parameters.Count);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        var ListParameter= reader.Serializer().ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var type = reader.ReadType();
        var parameters= reader.Deserialize宣言Parameters();
        ListParameter.AddRange(parameters!);
        var body = Expression.Read(ref reader);
        var tailCall = reader.ReadBoolean();
        ListParameter.RemoveRange(ListParameter_Count,parameters!.Length);
        value=(T)Expressions.Expression.Lambda(
            type,
            body,
            tailCall,
            parameters!
        );
    }
}
