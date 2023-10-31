using System;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

using LinqDB.Helpers;
using Utf8Json;
using Utf8Json.Formatters;




namespace LinqDB.Serializers.Utf8Json;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
internal static class Extension{
    public static readonly MethodInfo SerializeMethod = typeof(Extension).GetMethod(nameof(Serialize), BindingFlags.Static|BindingFlags.NonPublic)!;
    private static void Serialize<T>(ref Writer writer,T value,O Resolver){
        var Formatter=Resolver.GetFormatter<T>();
        Formatter?.Serialize(ref writer,value,Resolver);
    }




    public static readonly MethodInfo DeserializeMethod = typeof(Extension).GetMethod(nameof(Deserialize), BindingFlags.Static|BindingFlags.NonPublic)!;
    private static T Deserialize<T>(ref Reader reader,O Resolver){
        var Formatter=Resolver.GetFormatter<T>();
        return Formatter is not null?reader.Read(Formatter,Resolver):default!;
    }




    public static void WriteType(this ref Writer writer,Type value)=>writer.WriteString(value.TypeString());
    public static Type ReadType(this ref Reader reader)=>reader.ReadString().StringType();
    

    public static void WriteNodeType(this ref Writer writer,ExpressionType NodeType)=>writer.WriteString(NodeType.ToString());
    public static void WriteNodeType(this ref Writer writer,Expression Expression)=>writer.WriteNodeType(Expression.NodeType);
    public static ExpressionType ReadNodeType(this ref Reader reader)=>Enum.Parse<ExpressionType>(reader.ReadString());
    public static bool TryWriteNil(this ref Writer writer,object? value){
        if(value is not null)return false;
        writer.WriteNull();
        return true;
    }
    public static bool TryReadNil(this ref Reader reader)=>reader.ReadIsNull();













    private static class StaticReadOnlyCollectionFormatter<T> {
        public static readonly ReadOnlyCollectionFormatter<T> Formatter = new();
    }
    internal static void WriteCollection<T>(this ref Writer writer,ReadOnlyCollection<T>? value,O Resolver) =>
        writer.Write(StaticReadOnlyCollectionFormatter<T>.Formatter,value!,Resolver);
    internal static ReadOnlyCollection<T>ReadCollection<T>(this ref Reader reader,O Resolver) =>
        reader.Read(StaticReadOnlyCollectionFormatter<T>.Formatter,Resolver);
    private static class StaticArrayFormatter<T> {
        public static readonly ArrayFormatter<T> Formatter = new();
    }
    internal static void WriteArray<T>(this ref Writer writer,T[] value,O Resolver)=>
        writer.Write(StaticArrayFormatter<T>.Formatter,value,Resolver);
    internal static T[] ReadArray<T>(this ref Reader reader,O Resolver) {
        return reader.Read(StaticArrayFormatter<T>.Formatter,Resolver)!;
    }
    internal static void Serialize宣言Parameters(this ref Writer writer,ReadOnlyCollection<ParameterExpression> value,O Resolver){
        writer.WriteBeginArray();
        var Serializer=Resolver.Serializer();
        var Serializer_Parameters=Serializer.Parameters;
        var Serializer_ラムダ跨ぎParameters=Serializer.ラムダ跨ぎParameters;
        var Count=value.Count;
        if(Count>0){
            for(var a=0;;a++){
                var Parameter=value[a];
                var index0=Serializer_Parameters.LastIndexOf(Parameter);
                if(index0<0){
                    var index1=Serializer_ラムダ跨ぎParameters.LastIndexOf(Parameter);
                    if(index1<0){
                        writer.WriteBeginObject();
                        writer.WriteString(Parameter.Name);
                        writer.WriteNameSeparator();
                        var Type=Parameter.Type;
                        writer.WriteType(Parameter.IsByRef?Type.MakeByRefType():Type);
                        writer.WriteEndObject();
                    } else{
                        writer.WriteInt32(-1);
                        writer.WriteNameSeparator();
                        writer.WriteInt32(index1);
                    }
                } else{
                    writer.WriteInt32(index0);
                }
                if(a==Count-1) break;
                writer.WriteValueSeparator();
            }
        }
        writer.WriteEndArray();
    }
    internal static List<ParameterExpression> Deserialize宣言Parameters(this ref Reader reader,O Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var Serializer=Resolver.Serializer();
        var Serializer_Parameters=Serializer.Parameters;
        var Serializer_ラムダ跨ぎParameters=Serializer.ラムダ跨ぎParameters;
        var Parameters=new List<ParameterExpression>();
        while(!reader.ReadIsEndArray()) {
            if(reader.ReadIsBeginObject()) {
                var name = reader.ReadString();
                reader.ReadIsNameSeparatorWithVerify();
                var type = reader.ReadType();
                Parameters.Add(Expression.Parameter(type,name));
                reader.ReadIsEndObjectWithVerify();
            } else {
                var index0 = reader.ReadInt32();
                if(index0<0) {
                    reader.ReadIsValueSeparatorWithVerify();
                    var index1 = reader.ReadInt32();
                    Parameters.Add(Serializer_ラムダ跨ぎParameters[index1]);
                } else {
                    Parameters.Add(Serializer_Parameters[index0]);
                }
            }
            if(!reader.ReadIsValueSeparator()) {
                reader.ReadIsEndArrayWithVerify();
                break;
            }
        }
        return Parameters;
    }

    
    public static void Write<T>(this ref Writer writer,IJsonFormatter<T>Formatter,T value,O Resolver)=>
        Formatter.Serialize(ref writer,value,Resolver);
        













    public static void Write<T>(this ref Writer writer,T value,O Resolver)=>Resolver.GetFormatter<T>().Serialize(ref writer,value,Resolver);


    //private static readonly CharFormatter CharFormatter=new();
    private static readonly DecimalFormatter DecimalFormatter=new();
    private static readonly TimeSpanFormatter TimeSpanFormatter=new();
    private static readonly DateTimeFormatter DateTimeFormatter=new();
    private static readonly DateTimeOffsetFormatter DateTimeOffsetFormatter=new();
    public static void WriteChar          (this ref Writer writer,char            value)=>writer.WriteUInt16(value);
    public static void WriteDecimal       (this ref Writer writer,decimal         value,O Resolver)=>DecimalFormatter       .Serialize(ref writer,value,Resolver);
    public static void WriteTimeSpan      (this ref Writer writer,TimeSpan        value,O Resolver)=>TimeSpanFormatter      .Serialize(ref writer,value,Resolver);
    public static void WriteDateTime      (this ref Writer writer,DateTime        value,O Resolver)=>DateTimeFormatter      .Serialize(ref writer,value,Resolver);
    public static void WriteDateTimeOffset(this ref Writer writer,DateTimeOffset  value,O Resolver)=>DateTimeOffsetFormatter.Serialize(ref writer,value,Resolver);
    public static void WriteExpression    (this ref Writer writer,Expression      value,O Resolver)=>Formatters.Expression.Write           (ref writer,value,Resolver);
    //public static void WriteType          (this ref Writer writer,Type            value,O Resolver)=>Formatters.Reflection.Type.Write       (ref writer,value,Resolver);
    public static void WriteConstructor   (this ref Writer writer,ConstructorInfo value,O Resolver)=>Formatters.Reflection.Constructor.Write(ref writer,value,Resolver);
    public static void WriteMethod        (this ref Writer writer,MethodInfo      value,O Resolver)=>Formatters.Reflection.Method.Write     (ref writer,value,Resolver);
    public static void WriteProperty      (this ref Writer writer,PropertyInfo    value,O Resolver)=>Formatters.Reflection.Property.Write   (ref writer,value,Resolver);
    public static void WriteEvent         (this ref Writer writer,EventInfo       value,O Resolver)=>Formatters.Reflection.Event.Write      (ref writer,value,Resolver);
    public static void WriteField         (this ref Writer writer,FieldInfo       value,O Resolver)=>Formatters.Reflection.Field.Write      (ref writer,value,Resolver);
    public static void Write(this ref Writer writer,Type type,object value,O Resolver){
        var Formatter=Resolver.GetFormatterDynamic(type);
        if(Formatter is null){
            return;
        }
        var Serialize=Formatter.GetType().GetMethod("Serialize");
        Debug.Assert(Serialize is not null);
        var Objects3=new object[3];//ここでインスタンス化しないとstaticなFormatterで重複してしまう。
        Objects3[0]=writer;
        Objects3[1]=value;
        Objects3[2]=Resolver;
        Serialize.Invoke(Formatter,Objects3);
        writer=(Writer)Objects3[0];
    }


    
    public static T Read<T>(this ref Reader reader,IJsonFormatter<T> Formatter,O Resolver)=>Formatter.Deserialize(ref reader,Resolver);





















    public static char            ReadChar          (this ref Reader reader)=>(char)reader.ReadUInt16();
    public static decimal         ReadDecimal       (this ref Reader reader,O Resolver)=>DecimalFormatter.Deserialize          (ref reader,Resolver);
    public static TimeSpan        ReadTimeSpan      (this ref Reader reader,O Resolver)=>TimeSpanFormatter.Deserialize         (ref reader,Resolver);
    public static DateTime        ReadDateTime      (this ref Reader reader,O Resolver)=>DateTimeFormatter.Deserialize         (ref reader,Resolver);
    public static DateTimeOffset  ReadDateTimeOffset(this ref Reader reader,O Resolver)=>DateTimeOffsetFormatter.Deserialize   (ref reader,Resolver);
    public static Expression      ReadExpression    (this ref Reader reader,O Resolver)=>Formatters.Expression.Read            (ref reader,Resolver);
    public static Type            ReadType          (this ref Reader reader,O Resolver)=>Formatters.Reflection.Type.Read       (ref reader,Resolver);
    public static ConstructorInfo ReadConstructor   (this ref Reader reader,O Resolver)=>Formatters.Reflection.Constructor.Read(ref reader,Resolver);
    public static MethodInfo      ReadMethod        (this ref Reader reader,O Resolver)=>Formatters.Reflection.Method.Read     (ref reader,Resolver);
    public static PropertyInfo    ReadProperty      (this ref Reader reader,O Resolver)=>Formatters.Reflection.Property.Read   (ref reader,Resolver);
    public static EventInfo       ReadEvent         (this ref Reader reader,O Resolver)=>Formatters.Reflection.Event.Read      (ref reader,Resolver);
    public static FieldInfo       ReadField         (this ref Reader reader,O Resolver)=>Formatters.Reflection.Field.Read      (ref reader,Resolver);
    public static T Read<T>(this ref Reader reader,O Resolver)=>
        Resolver.GetFormatter<T>().Deserialize(ref reader,Resolver);
    public static object Read(this ref Reader reader,Type type,O Resolver){
        var Formatter=Resolver.GetFormatterDynamic(type);
        if(Formatter is null){
            if(type.GetIEnumerableT(out var Interface)) {
                Formatter=Resolver.GetFormatterDynamic(Interface);
                //var GenericArguments_0 = Interface.GetGenericArguments()[0];
                //var FormatterGenericType = typeof(Formatters.Enumerables.IEnumerableOther<,>).MakeGenericType(Interface,GenericArguments_0);
                //return Return((IMessagePackFormatter)FormatterGenericType.GetValue("Instance"));
            }

        }
        Debug.Assert(Formatter is not null);
        var Deserialize=Formatter.GetType().GetMethod("Deserialize");
        Debug.Assert(Deserialize is not null);
        var Objects2=new object[2];//ここでインスタンス化しないとstaticなFormatterで重複してしまう。
        Objects2[0]=reader;
        Objects2[1]=Resolver;
        var value=Deserialize.Invoke(Formatter,Objects2)!;
        reader=(Reader)Objects2[0];
        return value;
    }

    
    
    
    public static readonly MethodInfo WriteValueSeparator = typeof(Writer).GetMethod(nameof(Writer.WriteValueSeparator))!;
    public static readonly MethodInfo WriteString = typeof(Writer).GetMethod(nameof(Writer.WriteString))!;
    public static readonly MethodInfo WriteNameSeparator = typeof(Writer).GetMethod(nameof(Writer.WriteNameSeparator))!;
    public static readonly MethodInfo ReadIsValueSeparatorWithVerify = typeof(Reader).GetMethod(nameof(Reader.ReadIsValueSeparatorWithVerify))!;
    public static readonly MethodInfo ReadString = typeof(Reader).GetMethod(nameof(Reader.ReadString))!;
    public static readonly MethodInfo ReadIsNameSeparatorWithVerify = typeof(Reader).GetMethod(nameof(Reader.ReadIsNameSeparatorWithVerify))!;
    
    


















































    public static Serializer Serializer(this O Resolver)=>
        (Serializer)Resolver.GetFormatter<Serializer>();
        /*
    private static object? PrivateGetFormatterDynamic(this O Resolver,Type type){
        var Formatter=Resolver.GetFormatterDynamic(type);
        if(Formatter is not null) return Formatter;
        if(!type.IsArray&&type.GetCustomAttribute(typeof(SerializableAttribute))==null){
            if(type.IsGenericType){
                var GenericTypeDefinition=type.GetGenericTypeDefinition();
                if(GenericTypeDefinition==typeof(Enumerables.GroupingList<,>)
                   ||GenericTypeDefinition==typeof(Sets.GroupingSet<,>)
                   ||GenericTypeDefinition==typeof(Sets.SetGroupingList<,>)
                   ||GenericTypeDefinition==typeof(Sets.SetGroupingSet<,>)
                   ||GenericTypeDefinition==typeof(Sets.Set<,,>)
                   ||GenericTypeDefinition==typeof(Sets.Set<,>)
                   ||GenericTypeDefinition==typeof(Sets.Set<>))
                    goto 発見;
            }
            Type? type0;
            if((type0=type.GetInterface(CommonLibrary.Generic_ICollection1_FullName))!=null){
                type=type0;
            }else if((type0=type.GetInterface(CommonLibrary.Generic_IEnumerable1_FullName))!=null){
                type=type0;
            }
        }
        発見:
        return Resolver.GetFormatterDynamic(type);
    }*/
}
