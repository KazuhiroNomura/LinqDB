using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;


using Utf8Json;
using Utf8Json.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json;
using Writer = JsonWriter;
using Reader = JsonReader;
internal static class Extension{
    public static void WriteValue<T>(this ref Writer writer,T value,IJsonFormatterResolver Resolver)=>Resolver.GetFormatter<T>().Serialize(ref writer,value,Resolver);
    public static T ReadValue<T>(this ref Reader reader,IJsonFormatterResolver Resolver)=>Resolver.GetFormatter<T>().Deserialize(ref reader,Resolver);
    public static void WriteType(this ref Writer writer,Type value){
        writer.WriteString(value.AssemblyQualifiedName);
    }
    public static Type ReadType(this ref Reader reader){
        return Type.GetType(reader.ReadString())!;
    }
    

    public static void WriteNodeType(this ref Writer writer,Expressions.ExpressionType NodeType)=>writer.WriteString(NodeType.ToString());
    public static void WriteNodeType(this ref Writer writer,Expressions.Expression Expression)=>writer.WriteString(Expression.NodeType.ToString());
    public static Expressions.ExpressionType ReadNodeType(this ref Reader reader){
    	var value=reader.ReadString();
        return Enum.Parse<Expressions.ExpressionType>(value);
    }
    public static bool WriteIsNull(this ref Writer writer,object? value){
        if(value is not null){
        	
        	return false;
        }else{
	        writer.WriteNull();
    	    return true;
        }
    }

    private static class StaticReadOnlyCollectionFormatter<T> {
        public static readonly ReadOnlyCollectionFormatter<T> Formatter = new();
    }
    internal static void WriteCollection<T>(this ref Writer writer,ReadOnlyCollection<T>? value,IJsonFormatterResolver Resolver) =>
        StaticReadOnlyCollectionFormatter<T>.Formatter.Serialize(ref writer,value!,Resolver);
    private static class StaticArrayFormatter<T> {
        public static readonly ArrayFormatter<T> Formatter = new();
    }
    internal static T[] ReadArray<T>(this ref Reader reader,IJsonFormatterResolver Resolver) {
        return StaticArrayFormatter<T>.Formatter.Deserialize(ref reader,Resolver)!;
    }
    //internal static void SerializeReadOnlyCollection<T>(this ref Writer writer,ReadOnlyCollection<T>? value,IJsonFormatterResolver Resolver){
    //    var off=writer.CurrentOffset;
    //    writer.WriteBeginArray();
    //    var Count=value!.Count;
    //    if(Count>0){
    //        var Formatter=Resolver.GetFormatter<T>();
    //        Formatter.Serialize(ref writer,value[0],Resolver);
    //        for(var index=1;index<Count;index++){
    //            writer.WriteValueSeparator();
    //            Formatter.Serialize(ref writer,value[index],Resolver);
    //        }
    //    }
    //    writer.WriteEndArray();
    //}
    //internal static T[] ReadArray<T>(this ref Reader reader,IJsonFormatterResolver Resolver){
    //    var off=reader.GetCurrentOffsetUnsafe();
    //    reader.ReadIsBeginArrayWithVerify();
    //    var value=new List<T>();
    //    var Formatter=Resolver.GetFormatter<T>();
    //    if(!reader.ReadIsEndArray()){
    //        value.Add(Formatter.Deserialize(ref reader,Resolver));
    //        while(!reader.ReadIsEndArray()){
    //            reader.ReadIsValueSeparatorWithVerify();
    //            value.Add(Formatter.Deserialize(ref reader,Resolver));
    //        }
    //    }
    //    //reader.ReadIsEndArrayWithVerify();
    //    return value.ToArray();


    //}
    internal static void Serialize宣言Parameters(this ref Writer writer,ReadOnlyCollection<Expressions.ParameterExpression> value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        var Count=value.Count;
        if(Count>0){
            for(var a=0;;a++){
                var Parameter=value[a];
                writer.WriteBeginObject();
                writer.WriteString(Parameter.Name);
                writer.WriteNameSeparator();
                writer.WriteType(Parameter.Type);
                writer.WriteEndObject();
                if(a==Count-1) break;
                writer.WriteValueSeparator();
            }
        }
        writer.WriteEndArray();
    }
    internal static List<Expressions.ParameterExpression> Deserialize宣言Parameters(this ref Reader reader,IJsonFormatterResolver Resolver){
        var List=new List<Expressions.ParameterExpression>();
        reader.ReadIsBeginArrayWithVerify();
        while(reader.ReadIsBeginObject()){
            var name=reader.ReadString();
            reader.ReadIsNameSeparatorWithVerify();
            var type=reader.ReadType();
            List.Add(Expressions.Expression.Parameter(type,name));
            reader.ReadIsEndObjectWithVerify();
            if(!reader.ReadIsValueSeparator()) break;
        }
        reader.ReadIsEndArrayWithVerify();
        return List;
    }
    public static object ReadValue(this ref Reader reader,Type type,IJsonFormatterResolver Resolver){
        var Formatter=Resolver.GetFormatterDynamic(type);
        var Deserialize=Formatter.GetType().GetMethod("Deserialize");
        Debug.Assert(Deserialize is not null);
        var Objects2=new object[2];//ここでインスタンス化しないとstaticなFormatterで重複してしまう。
        Objects2[0]=reader;
        Objects2[1]=Resolver;
        var value=Deserialize.Invoke(Formatter,Objects2)!;
        reader=(Reader)Objects2[0];
        return value;
    }
    public static Serializer Serializer(this IJsonFormatterResolver Resolver)=>
        (Serializer)Resolver.GetFormatter<Serializer>();
}
