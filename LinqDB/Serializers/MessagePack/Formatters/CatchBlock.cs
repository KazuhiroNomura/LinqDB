using System;

using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.CatchBlock;
public class CatchBlock:IMessagePackFormatter<T> {
    public static readonly CatchBlock Instance=new();
    public void Serialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
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
            var ListParameter=Resolver.Serializer().ListParameter;
            ListParameter.Add(value.Variable);
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
            ListParameter.RemoveAt(ListParameter.Count-1);
        }

    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
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
                var ListParameter=Resolver.Serializer().ListParameter;
                ListParameter.Add(Variable);
                
                var body=Expression.Read(ref reader,Resolver);
                ListParameter.RemoveAt(ListParameter.Count-1);
                value=Expressions.Expression.Catch(Variable,body);
                break;
            }
            case 3:{
                var name=reader.ReadString();
                var Variable=Expressions.Expression.Parameter(test,name);
                var ListParameter=Resolver.Serializer().ListParameter;
                ListParameter.Add(Variable);
                var body=Expression.Read(ref reader,Resolver);
                var filter=Expression.Read(ref reader,Resolver);
                ListParameter.RemoveAt(ListParameter.Count-1);
                value=Expressions.Expression.Catch(Variable,body,filter);
                break;
            }
            default:throw new NotSupportedException($"CatchBlock id{id}は不正");
        }
        
        return value;
    }
    //public void Serialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
    //    writer.WriteArrayHeader(ArrayHeader);
    //    writer.WriteType(value.Test);

    //    if(value.Variable is null){
    //        if(value.Filter is null){
    //            writer.WriteInt32();
    //            writer.WriteNil();//Variable
                
    //            Expression.Write(ref writer,value.Body,Resolver);
                
    //            writer.WriteNil();//Filter
    //        } else{
    //            writer.WriteNil();//Variable
                
    //            Expression.Write(ref writer,value.Body,Resolver);
                
    //            Expression.Write(ref writer,value.Filter,Resolver);
    //        }
    //    } else{
    //        var ListParameter=Resolver.Serializer().ListParameter;
    //        if(value.Filter is null){
    //            writer.Write(value.Variable.Name);
    //            ListParameter.Add(value.Variable);
                
    //            Expression.Write(ref writer,value.Body,Resolver);
    //            ListParameter.RemoveAt(ListParameter.Count-1);

    //            writer.WriteNil();//Filter
    //        } else{
    //            writer.Write(value.Variable.Name);
    //            ListParameter.Add(value.Variable);
                
    //            Expression.Write(ref writer,value.Body,Resolver);
                
    //            Expression.Write(ref writer,value.Filter,Resolver);
    //            ListParameter.RemoveAt(ListParameter.Count-1);
    //        }
    //    }

    //}
    //public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
    //    var count=reader.ReadArrayHeader();
    //    Debug.Assert(count==ArrayHeader);
    //    var test=reader.ReadType();
    //    if(reader.TryReadNil()){
    //        var body= Expression.Read(ref reader,Resolver);
    //        var filter=reader.TryReadNil()?null:Expression.Read(ref reader,Resolver);
    //        return Expressions.Expression.Catch(test,body,@filter);
    //    } else{
    //        var name=reader.ReadString();
    //        var ListParameter=Resolver.Serializer().ListParameter;
    //        ListParameter.Add(Expressions.Expression.Parameter(test,name));
    //        var body= Expression.Read(ref reader,Resolver);
    //        var filter=reader.TryReadNil()?null:Expression.Read(ref reader,Resolver);
    //        ListParameter.RemoveAt(ListParameter.Count-1);
    //        return Expressions.Expression.Catch(Expressions.Expression.Parameter(test,name),body,@filter);
    //    }
    //}
}
