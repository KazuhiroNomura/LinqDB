using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.ParameterExpression;
public class Parameter:IMessagePackFormatter<T> {
    public static readonly Parameter Instance=new();
    internal static void Write(ref Writer writer,T value,O Resolver){
        var Serializer=Resolver.Serializer();
        var index0=Serializer.Parameters.LastIndexOf(value);
        if(index0<0){
            var index1=Serializer.ラムダ跨ぎParameters.LastIndexOf(value);
            if(index1<0){
                writer.WriteArrayHeader(4);
                writer.WriteNodeType(Expressions.ExpressionType.Parameter);
                writer.WriteInt32(-2);
                
                writer.Write(value.Name);
                
                writer.WriteType(value.Type);
                
                writer.WriteBoolean(value.IsByRef);
                Serializer.ラムダ跨ぎParameters.Add(value);
            }else{
                writer.WriteArrayHeader(3);
                writer.WriteNodeType(Expressions.ExpressionType.Parameter);
                writer.WriteInt32(-1);
                writer.Write(index1);
            }
        }else{
            writer.WriteArrayHeader(2);
            writer.WriteNodeType(Expressions.ExpressionType.Parameter);
            writer.Write(index0);
        }
    }
    public void Serialize(ref Writer writer,T value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        var Serializer=Resolver.Serializer();
        var index0=Serializer.Parameters.LastIndexOf(value);
        if(index0<0){
            var index1=Serializer.ラムダ跨ぎParameters.LastIndexOf(value);
            if(index1<0){
                writer.WriteArrayHeader(3);
                writer.WriteInt32(-2);
                writer.Write(value.Name);
                writer.WriteType(value.Type);
                writer.WriteBoolean(value.IsByRef);
                Serializer.ラムダ跨ぎParameters.Add(value);
            }else{
                writer.WriteArrayHeader(2);
                writer.WriteInt32(-1);
                writer.Write(index1);
            }
        }else{
            writer.WriteArrayHeader(1);
            writer.Write(index0);
        }
        
    }
    internal static T Read(ref Reader reader,O Resolver,int ArrayHeader){
        var index0=reader.ReadInt32();
        var Serializer=Resolver.Serializer();
        if(ArrayHeader!=2){
            
            
            if(ArrayHeader!=3){
                Debug.Assert(index0==-2);
                var name=reader.ReadString();
                var type=reader.ReadType();
                var IsByRef=reader.ReadBoolean();
                var Parameter=Expressions.Expression.Parameter(IsByRef?type.MakeByRefType():type,name);
                Serializer.ラムダ跨ぎParameters.Add(Parameter);
                return Parameter;
            }else{
                Debug.Assert(index0==-1);
                var index1=reader.ReadInt32();
                return Serializer.ラムダ跨ぎParameters[index1];
            }
        }else{
            
            return Serializer.Parameters[index0];
        }
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var ArrayHeader=reader.ReadArrayHeader();
        var index0=reader.ReadInt32();
        var Serializer=Resolver.Serializer();
        if(ArrayHeader!=1){
            
            
            if(ArrayHeader!=2){
                Debug.Assert(index0==-2);
                var name=reader.ReadString();

                var type=reader.ReadType();
                var IsByRef=reader.ReadBoolean();
                var Parameter=Expressions.Expression.Parameter(IsByRef?type.MakeByRefType():type,name);
                Serializer.ラムダ跨ぎParameters.Add(Parameter);
                return Parameter;
            }else{
                Debug.Assert(index0==-1);
                var index1=reader.ReadInt32();
                return Serializer.ラムダ跨ぎParameters[index1];
            }
        }else{
            
            return Serializer.Parameters[index0];
        }
        


    }
}
