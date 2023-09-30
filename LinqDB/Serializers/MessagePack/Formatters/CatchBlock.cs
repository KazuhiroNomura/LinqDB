using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.CatchBlock;
public class CatchBlock:IMessagePackFormatter<T> {
    public static readonly CatchBlock Instance=new();
    public void Serialize(ref Writer writer,T value,O Resolver){
        if(writer.TryWriteNil(value)) return;

        if(value.Variable is null){
            if(value.Filter is null){
                writer.WriteArrayHeader(3);
                writer.WriteInt8(0);
                
                writer.WriteType(value.Test);
                
                Expression.Write(ref writer,value.Body,Resolver);
            } else{
                writer.WriteArrayHeader(4);
                writer.WriteInt8(1);
                
                writer.WriteType(value.Test);
                
                Expression.Write(ref writer,value.Body,Resolver);
                
                Expression.Write(ref writer,value.Filter,Resolver);
            }
        } else{
            var Parameters=Resolver.Serializer().Parameters;
            Parameters.Add(value.Variable);
            if(value.Filter is null){
                writer.WriteArrayHeader(4);
                writer.WriteInt8(2);
                
                writer.WriteType(value.Test);
                
                writer.Write(value.Variable.Name);
                
                Expression.Write(ref writer,value.Body,Resolver);
            } else{
                writer.WriteArrayHeader(5);
                writer.WriteInt8(3);
                
                writer.WriteType(value.Test);
                
                writer.Write(value.Variable.Name);
                
                Expression.Write(ref writer,value.Body,Resolver);
                
                Expression.Write(ref writer,value.Filter,Resolver);
            }
            Parameters.RemoveAt(Parameters.Count-1);
        }

    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        T value;
        
        var id=reader.ReadByte();
        
        var test=reader.ReadType();
        
        switch(id){
            case 0:{
                var body=Expression.Read(ref reader,Resolver);
                value=Expressions.Expression.Catch(test,body);
                break;
            }
            case 1:{
                var body=Expression.Read(ref reader,Resolver);
                
                var filter=Expression.Read(ref reader,Resolver);
                value=Expressions.Expression.Catch(test,body,filter);
                break;
            }
            case 2:{
                var name=reader.ReadString();
                var Variable=Expressions.Expression.Parameter(test,name);
                var Parameters=Resolver.Serializer().Parameters;
                Parameters.Add(Variable);
                
                var body=Expression.Read(ref reader,Resolver);
                Parameters.RemoveAt(Parameters.Count-1);
                value=Expressions.Expression.Catch(Variable,body);
                break;
            }
            default:{
                Debug.Assert(id==3);
                var name=reader.ReadString();
                var Variable=Expressions.Expression.Parameter(test,name);
                var Parameters=Resolver.Serializer().Parameters;
                Parameters.Add(Variable);
                
                var body=Expression.Read(ref reader,Resolver);
                
                var filter=Expression.Read(ref reader,Resolver);
                Parameters.RemoveAt(Parameters.Count-1);
                value=Expressions.Expression.Catch(Variable,body,filter);
                break;
            }
        }
        
        return value;
    }
}
