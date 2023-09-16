using System.Collections.ObjectModel;



using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
internal static class Extension{
    public static void WriteValue<T>(this ref Writer writer,T value,MessagePackSerializerOptions options)=>options.Resolver.GetFormatter<T>()!.Serialize(ref writer,value,options);
    public static T ReadValue<T>(this ref Reader reader,MessagePackSerializerOptions options)=>options.Resolver.GetFormatter<T>()!.Deserialize(ref reader,options);
    public static void WriteType(this ref Writer writer,System.Type value){
        writer.Write(value.AssemblyQualifiedName);
    }
    public static System.Type ReadType(this ref Reader reader){
        return System.Type.GetType(reader.ReadString())!;
    }
    public static void WriteBoolean(this ref Writer writer,bool value)=>writer.Write(value);
    //public static bool ReadBoolean(this ref Reader reader)=>reader.ReadByte()!=0;
    public static void WriteNodeType(this ref Writer writer,Expressions.ExpressionType NodeType)=>writer.WriteInt8((sbyte)NodeType);
    public static Expressions.ExpressionType ReadNodeType(this ref Reader reader){
    	var value=reader.ReadByte();
        return (Expressions.ExpressionType)value;
    }
    public static bool TryWriteNil(this ref Writer writer,object? value){
        if(value is not null){
        	
        	return false;
        }else{
	        writer.WriteNil();
    	    return true;
        }
    }
    
    private static class StaticReadOnlyCollectionFormatter<T>{
        public static readonly ReadOnlyCollectionFormatter<T> Formatter=new();
    }
    internal static void SerializeReadOnlyCollection<T>(this ref Writer writer,ReadOnlyCollection<T> value,MessagePackSerializerOptions Resolver)=>
        StaticReadOnlyCollectionFormatter<T>.Formatter.Serialize(ref writer,value,Resolver);
    private static class StaticArrayFormatter<T>{
        public static readonly ArrayFormatter<T> Formatter=new();
    }
    internal static T[] ReadArray<T>(this ref Reader reader,MessagePackSerializerOptions Resolver){
        return StaticArrayFormatter<T>.Formatter.Deserialize(ref reader,Resolver)!;


    }
    public static void Serialize宣言Parameters(this ref Writer writer,ReadOnlyCollection<Expressions.ParameterExpression>value,MessagePackSerializerOptions Resolver) {
        writer.WriteArrayHeader(value.Count);
        
        foreach(var Parameter in value){
            writer.Write(Parameter.Name);
            writer.WriteType(Parameter.Type);
        }









    }
    public static Expressions.ParameterExpression[]Deserialize宣言Parameters(this ref Reader reader,MessagePackSerializerOptions Resolver){
        var Count=reader.ReadArrayHeader();
        var Parameters=new Expressions.ParameterExpression[Count];
        for(var a=0;a<Count;a++){
            var name=reader.ReadString();
            var type=reader.ReadType();
            Parameters[a]=Expressions.Expression.Parameter(type,name);
        }
        return Parameters;




    }
    public static object ReadValue(this ref Reader reader,System.Type type,MessagePackSerializerOptions Resolver){
        var Formatter=Resolver.Resolver.GetFormatterDynamic(type);
        return MessagePack.Serializer.DynamicDeserialize(Formatter,ref reader,Resolver);
    }
    public static Serializer Serializer(this MessagePackSerializerOptions Options)=>
        (Serializer)Options.Resolver.GetFormatter<Serializer>()!;
}
