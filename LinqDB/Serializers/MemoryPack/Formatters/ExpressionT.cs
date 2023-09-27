
using MemoryPack;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;


using Reader = MemoryPackReader;
public class ExpressionT<T>:MemoryPackFormatter<T>where T:Expressions.LambdaExpression {
    public static readonly ExpressionT<T> Instance=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;

        writer.WriteType(value!.Type);

        writer.Serialize宣言Parameters(value.Parameters);
        var Parameters= writer.Serializer().Parameters;
        var Parameters_Count=Parameters.Count;
        var value_Parameters=value.Parameters;
        Parameters.AddRange(value_Parameters);
        
        Expression.Write(ref writer,value.Body);
        
        writer.WriteBoolean(value.TailCall);
        
        Parameters.RemoveRange(Parameters_Count,value_Parameters.Count);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;

        var type = reader.ReadType();

        var parameters= reader.Deserialize宣言Parameters();

        var Parameters= reader.Serializer().Parameters;
        var Parameters_Count=Parameters.Count;
        
        Parameters.AddRange(parameters);
        var body = Expression.Read(ref reader);
        
        var tailCall = reader.ReadBoolean();
        
        Parameters.RemoveRange(Parameters_Count,parameters.Length);
        value=(T)Expressions.Expression.Lambda(
            type,
            body,
            tailCall,
            parameters
        );
    }
}
