using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using LinqDB.Helpers;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;


namespace LinqDB.Serializers.MessagePack;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
internal static class Extension{
    public static readonly MethodInfo SerializeMethod = typeof(Extension).GetMethod(nameof(Serialize), BindingFlags.Static|BindingFlags.NonPublic)!;
    private static void Serialize<T>(ref Writer writer,T value,O Resolver){
        var Formatter=Resolver.Resolver.GetFormatter<T>();
        Formatter?.Serialize(ref writer,value,Resolver);
    }
    
    
    
    
    public static readonly MethodInfo DeserializeMethod = typeof(Extension).GetMethod(nameof(Deserialize), BindingFlags.Static|BindingFlags.NonPublic)!;
    private static T Deserialize<T>(ref Reader reader,O Resolver){
        var Formatter=Resolver.Resolver.GetFormatter<T>();
        return Formatter is not null?reader.Read(Formatter,Resolver):default!;
    }
    public static readonly MethodInfo WriteArrayHeader = typeof(Writer).GetMethod(nameof(Writer.WriteArrayHeader), new[] { typeof(int) })!;
    public static readonly MethodInfo ReadArrayHeader = typeof(Reader).GetMethod(nameof(Reader.ReadArrayHeader))!;


    public static void WriteType(this ref Writer writer,Type value)=>writer.Write(value.TypeString());
    public static Type ReadType(this ref Reader reader)=> reader.ReadString().StringType();
    public static void WriteBoolean(this ref Writer writer,bool value)=>writer.Write(value);

    public static void WriteNodeType(this ref Writer writer,Expressions.ExpressionType NodeType)=>writer.WriteInt8((sbyte)NodeType);
    public static void WriteNodeType(this ref Writer writer,Expressions.Expression Expression)=>writer.WriteInt8((sbyte)Expression.NodeType);
    public static Expressions.ExpressionType ReadNodeType(this ref Reader reader)=>(Expressions.ExpressionType)reader.ReadByte();
    public static bool TryWriteNil(this ref Writer writer,object? value){
        if(value is not null)return false;
        writer.WriteNil();
	    return true;
    }











    public static void Write<T>(this IMessagePackFormatter<T>Formatter,ref Writer writer,T value,O Resolver){
        Formatter.Serialize(ref writer,value,Resolver);
    }
    private static class StaticReadOnlyCollectionFormatter<T>{
        public static readonly ReadOnlyCollectionFormatter<T> Formatter=new();
    }
    internal static void WriteCollection<T>(this ref Writer writer,ReadOnlyCollection<T> value,O Resolver)=>
        writer.Write(StaticReadOnlyCollectionFormatter<T>.Formatter,value,Resolver);
    private static class StaticArrayFormatter<T>{
        public static readonly ArrayFormatter<T> Formatter=new();
    }
    internal static void WriteArray<T>(this ref Writer writer,T[] value,O Resolver)=>
        writer.Write(StaticArrayFormatter<T>.Formatter,value,Resolver);
    internal static T[] ReadArray<T>(this ref Reader reader,O Resolver){

        return reader.Read(StaticArrayFormatter<T>.Formatter,Resolver)!;

    }
    internal static void Serialize宣言Parameters(this ref Writer writer,ReadOnlyCollection<Expressions.ParameterExpression>value,O Resolver){
        var Serializer=Resolver.Serializer();
        var Serializer_Parameters=Serializer.Parameters;
        var Serializer_ラムダ跨ぎParameters=Serializer.ラムダ跨ぎParameters;
        var Count=1;
        foreach(var Parameter in value){
            var index0=Serializer_Parameters.LastIndexOf(Parameter);
            Count++;
            if(index0<0){
                var index1=Serializer_ラムダ跨ぎParameters.LastIndexOf(Parameter);
                Count++;
                if(index1<0){
                    Count+=2;
                }
            }
        }
        writer.WriteArrayHeader(Count);
        writer.WriteInt32(value.Count);
        foreach(var Parameter in value){
            var index0=Serializer_Parameters.LastIndexOf(Parameter);
            writer.WriteInt32(index0);
            if(index0<0){
                var index1=Serializer_ラムダ跨ぎParameters.LastIndexOf(Parameter);
                writer.WriteInt32(index1);
                if(index1<0){
                    writer.Write(Parameter.Name);
                    var Type=Parameter.Type;
                    writer.WriteType(Parameter.IsByRef?Type.MakeByRefType():Type);
                }
            }
        }
    }
    
    internal static Expressions.ParameterExpression[]Deserialize宣言Parameters(this ref Reader reader,O Resolver){
        var ArrayHeader=reader.ReadArrayHeader()-1;
        var Serializer=Resolver.Serializer();
        var Serializer_Parameters=Serializer.Parameters;
        var Serializer_ラムダ跨ぎParameters=Serializer.ラムダ跨ぎParameters;
        var Parameters_Length=reader.ReadInt32();
        var Parameters=new Expressions.ParameterExpression[Parameters_Length];
        var a=0;
        while(ArrayHeader>0){
            var index0=reader.ReadInt32();
            if(index0<0){
                ArrayHeader--;
                var index1=reader.ReadInt32();
                if(index1<0){
                    ArrayHeader-=2;
                    var name=reader.ReadString();
                    var type=reader.ReadType();
                    Parameters[a]=Expressions.Expression.Parameter(type,name);
                    
                } else{
                    Parameters[a]=Serializer_ラムダ跨ぎParameters[index1];
                }
            }else{
                Parameters[a]=Serializer_Parameters[index0];
            }
            ArrayHeader--;
            a++;
        }
        return Parameters;
    }
    private delegate void SerializeDelegate(object Formatter,ref Writer writer,object value,O options);
    private static readonly Type[] SerializeTypes={typeof(object),typeof(Writer).MakeByRefType(),typeof(object),typeof(O) };
    public static void Write<T>(this ref Writer writer,IMessagePackFormatter<T>Formatter,T value,O Resolver)=>
        Formatter.Serialize(ref writer,value,Resolver);
    public static void Write(this ref Writer writer,object Formatter,object value,O Resolver){
        // Invokeではref引数を呼べないため。
        var Formatter_Serialize=Formatter.GetType().GetMethod("Serialize")!;
        var D=new DynamicMethod("",typeof(void),SerializeTypes){InitLocals=false};
        var I=D.GetILGenerator();
        I.Ldarg_0();//formatter
        I.Ldarg_1();//writer
        I.Ldarg_2();//value
        I.Unbox_Any(Formatter_Serialize.GetParameters()[1].ParameterType);
        I.Ldarg_3();//options
        I.Callvirt(Formatter_Serialize);
        I.Ret();
        ((SerializeDelegate)D.CreateDelegate(typeof(SerializeDelegate)))(Formatter,ref writer,value,Resolver);
    }
    public static void Write<T>(this ref Writer writer,T value,O Resolver)=>
        Resolver.GetFormatter<T>().Serialize(ref writer,value,Resolver);






    public static void Write(this ref Writer writer,Type type,object? value,O Resolver) {
        Debug.Assert(type==value!.GetType());
        var Formatter=Resolver.GetFormatterDynamic(type)!;
        var Formatter_Serialize=Formatter.GetType().GetMethod("Serialize")!;
        //Formatter_Serialize.CreateDelegate<Func<int>>();
        //CreateDelegate(Formatte)
        var D=new DynamicMethod("",typeof(void),SerializeTypes){InitLocals=false};
        var I=D.GetILGenerator();
        I.Ldarg_0();//formatter
        I.Ldarg_1();//writer
        I.Ldarg_2();//value
        I.Unbox_Any(Formatter_Serialize.GetParameters()[1].ParameterType);
        I.Ldarg_3();//options
        I.Callvirt(Formatter_Serialize);
        I.Ret();
        ((SerializeDelegate)D.CreateDelegate(typeof(SerializeDelegate)))(Formatter,ref writer,value,Resolver);
    }
    public static T Read<T>(this ref Reader reader,IMessagePackFormatter<T> Formatter,O Resolver)=>Formatter.Deserialize(ref reader,Resolver);




    public static T Read<T>(this ref Reader reader,O Resolver)=>
        Resolver.GetFormatter<T>().Deserialize(ref reader, Resolver);








    public static object Read(this ref Reader reader,Type type,O Resolver){
        var Formatter=Resolver.GetFormatterDynamic(type)!;
        var Method=Formatter.GetType().GetMethod("Deserialize")!;
        var D=new DynamicMethod("",typeof(object),DeserializeTypes){InitLocals=false};
        var I=D.GetILGenerator();
        I.Ldarg_0();
        I.Ldarg_1();
        I.Ldarg_2();
        I.Callvirt(Method);
        I.Box(Method.ReturnType);
        I.Ret();
        var Del=(DelegateRead)D.CreateDelegate(typeof(DelegateRead));
        var Result=Del(Formatter,ref reader,Resolver);
        return Result;
    }
    private delegate object DelegateRead(object Formatter,ref Reader reader,O options);
    private static readonly Type[] DeserializeTypes={typeof(object),typeof(Reader).MakeByRefType(),typeof(O)};
    public static IMessagePackFormatter<T> GetFormatter<T>(this O Resolver)=>
        Resolver.Resolver.GetFormatter<T>()!;
    public static object? GetFormatterDynamic(this O Resolver,Type type){
        if(!type.IsArray&&type.GetCustomAttribute(typeof(MessagePackObjectAttribute))==null){
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
            //<UnionByIterator>はシリアライズできないクラスなのでインターフェースに変換する
            //Type? type0;
            //if((type0=type.GetInterface(CommonLibrary.Generic_ICollection1_FullName))!=null){
            //    type=type0;
            //} else if((type0=type.GetInterface(CommonLibrary.Generic_IEnumerable1_FullName))!=null){
            //    type=type0;
            //}
        }
        発見: ;
        return Resolver.Resolver.GetFormatterDynamic(type);
    }
    public static Serializer Serializer(this O Resolver)=>
        (Serializer)Resolver.GetFormatter<Serializer>();
}
