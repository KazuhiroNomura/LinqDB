//#define 匿名型にキーを入れる
using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using Lokad.ILPack;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
//public class DisplayClassJsonFormatter{
//    protected static readonly MethodInfo WriteBeginArray=typeof(JsonWriter).GetMethod("WriteBeginArray")!;
//    protected static readonly MethodInfo WriteValueSeparator=typeof(JsonWriter).GetMethod("WriteValueSeparator")!;
//    protected static readonly MethodInfo WriteValWriteEndArrayueSeparator=typeof(JsonWriter).GetMethod("WriteEndArray")!;
//    protected static readonly MethodInfo ReadIsBeginArrayWithVerify=typeof(JsonReader).GetMethod("ReadIsBeginArrayWithVerify")!;
//    protected static readonly MethodInfo ReadIsValueSeparatorWithVerify=typeof(JsonReader).GetMethod("ReadIsValueSeparatorWithVerify")!;
//    protected static readonly MethodInfo ReadIsEndArrayWithVerify=typeof(JsonReader).GetMethod("ReadIsEndArrayWithVerify")!;
//    protected static void WriteType(ref JsonWriter writer,Type value)=>writer.WriteString(value.AssemblyQualifiedName);
//    protected static Type ReadType(ref JsonReader reader)=>Type.GetType(reader.ReadString())!;
//    protected static void WriteType(ref MessagePackWriter writer,Type value)=>writer.Write(value.AssemblyQualifiedName);
//    protected static Type ReadType(ref MessagePackReader reader)=>Type.GetType(reader.ReadString())!;
//    protected static void Serialize<T>(ref JsonWriter writer,T value,IJsonFormatterResolver Resolver)=>Resolver.GetFormatter<T>().Serialize(ref writer,value,Resolver);
//    protected static readonly MethodInfo MethodSerialize=typeof(CommonJsonFormatter).GetMethod(nameof(Serialize),BindingFlags.Static|BindingFlags.NonPublic)!;
//    protected static T Deserialize<T>(ref JsonReader reader,IJsonFormatterResolver Resolver)=>Resolver.GetFormatter<T>().Deserialize(ref reader,Resolver);
//    protected static readonly MethodInfo MethodDeserialize=typeof(CommonJsonFormatter).GetMethod(nameof(Deserialize),BindingFlags.Static|BindingFlags.NonPublic)!;
//}
public class DisplayClassMessagePackFormatter<T>:Anonymous,IMessagePackFormatter<T>{
    //public static readonly CatchBlock Instance=new();
    private delegate void delegate_Serialize(ref Writer writer,T value,MessagePackSerializerOptions options);
    private readonly delegate_Serialize DelegateSerialize;
    private delegate T delegate_Deserialize(ref Reader reader,MessagePackSerializerOptions options);
    private readonly delegate_Deserialize DelegateDeserialize;
    public DisplayClassMessagePackFormatter() {
        var Types1 = new System.Type[1];
        var Types2 = new System.Type[2];
        var Types3 = new System.Type[3];
        Types3[0]=typeof(Writer).MakeByRefType();
        Types2[0]=typeof(Reader).MakeByRefType();
        Types3[1]=typeof(T);
        Types2[1]=Types3[2]=typeof(MessagePackSerializerOptions);
        var ctor=typeof(T).GetConstructors()[0];
        var Fields = typeof(T).GetFields(BindingFlags.Public|BindingFlags.Instance);
        var Fields_Length = Fields.Length;
        Array.Sort(Fields,(a,b)=>string.CompareOrdinal(a.Name,b.Name));
        {
            var Serialize=new DynamicMethod("Serialize",typeof(void), Types3, typeof(Utf8Json.Serializer),true){ InitLocals=false};
            var Deserialize=new DynamicMethod("Deserialize",typeof(T), Types2, typeof(Utf8Json.Serializer),true){ InitLocals=false};
            //var (D0,D1,ctor,Properties)=((DynamicMethod D0,DynamicMethod D1,ConstructorInfo ctor,PropertyInfo[] Properties))(D0:D2,D1:D3,ctor:Ctor,Properties:Properties1);
            var I0=Serialize.GetILGenerator();
            var I1=Deserialize.GetILGenerator();
            共通(I0,I1);
            this.DelegateSerialize=(delegate_Serialize)Serialize.CreateDelegate(typeof(delegate_Serialize));
            this.DelegateDeserialize=(delegate_Deserialize)Deserialize.CreateDelegate(typeof(delegate_Deserialize));
        }
        {
            var AssemblyName = new AssemblyName { Name="Name" };
            var DynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(AssemblyName,AssemblyBuilderAccess.RunAndCollect);
            var ModuleBuilder = DynamicAssembly.DefineDynamicModule("動的");
            var Disp_TypeBuilder = ModuleBuilder.DefineType("Disp",TypeAttributes.Public);
            var Serialize = Disp_TypeBuilder.DefineMethod($"Serialize",MethodAttributes.Static|MethodAttributes.Public,typeof(void),Types3);
            Serialize.DefineParameter(1,ParameterAttributes.None,"writer");
            Serialize.DefineParameter(2,ParameterAttributes.None,"value");
            Serialize.DefineParameter(3,ParameterAttributes.None,"options");
            var Deserialize = Disp_TypeBuilder.DefineMethod($"Deserialize",MethodAttributes.Static|MethodAttributes.Public,typeof(T),Types2);
            Deserialize.DefineParameter(1,ParameterAttributes.None,"writer");
            Deserialize.DefineParameter(2,ParameterAttributes.None,"options");
            Deserialize.InitLocals=false;
            共通(Serialize.GetILGenerator(),Deserialize.GetILGenerator());
            Disp_TypeBuilder.CreateType();
            var Folder = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);
            new AssemblyGenerator().GenerateAssembly(DynamicAssembly,@$"{Folder}\MessagePackSerializer.dll");
        }
        void 共通(ILGenerator I0,ILGenerator I1){
            I0.Emit(OpCodes.Ldarg_0);//writer
            I0.Emit(OpCodes.Ldc_I4,Fields_Length);
            I0.Emit(OpCodes.Call,WriteArrayHeader);
            I1.Emit(OpCodes.Ldarg_0);//reader
            I1.Emit(OpCodes.Call,ReadArrayHeader);
            I1.Emit(OpCodes.Pop);
            I1.Emit(OpCodes.Newobj,ctor);
            I1.DeclareLocal(typeof(T));
            I1.Emit(OpCodes.Stloc_0);//変数=new T()
            var index=0;
            while(true){
                var Field=Fields[index];
                Types1[0]=Field.FieldType;
                I0.Emit(OpCodes.Ldarg_0);//writer
                I0.Emit(OpCodes.Ldarg_1);//value
                I0.Emit(OpCodes.Ldfld,Field);//value.property
                I0.Emit(OpCodes.Ldarg_2);//options
                I0.Emit(OpCodes.Call,MethodSerialize.MakeGenericMethod(Types1));
                I1.Emit(OpCodes.Ldloc_0);//変数.
                I1.Emit(OpCodes.Ldarg_0);//reader
                I1.Emit(OpCodes.Ldarg_1);//options
                I1.Emit(OpCodes.Call,MethodDeserialize.MakeGenericMethod(Types1));
                I1.Emit(OpCodes.Stfld,Field);//変数.field=Deserialize(ref reader,options)
                index++;
                if(index==Fields_Length) break;
            }
            I0.Emit(OpCodes.Ret);
            I1.Emit(OpCodes.Ldloc_0);
            I1.Emit(OpCodes.Ret);
        }
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions options){
        if(writer.TryWriteNil(value)) return;
        this.DelegateSerialize(ref writer, value,options);
//        var Parameters = typeof(T).GetConstructors()[0].GetParameters();
//        var Parameters_Length = Parameters.Length;
//#if 匿名型にキーを入れる
//        writer.WriteMapHeader(Parameters_Length);
//        for(var a = 0;a<Parameters_Length;a++) {
//            var Parameter = Parameters[a];
//            var Key = Parameter.Name;
//            writer.Write(Key);
//            SerializerConfiguration.DynamicSerialize(options.Resolver.GetFormatterDynamic(Parameter.ParameterType),ref writer,typeof(T).GetProperty(Key)!.GetMethod.Invoke(value,Array.Empty<object>()),options);
//        }
//#else
//        writer.WriteArrayHeader(Parameters_Length);
//        for(var a = 0;a<Parameters_Length;a++) {
//            var Parameter = Parameters[a];
//            var Key = Parameter.Name;
//            SerializerConfiguration.DynamicSerialize(options.Resolver.GetFormatterDynamic(Parameter.ParameterType),ref writer,typeof(T).GetProperty(Key)!.GetMethod.Invoke(value,Array.Empty<object>()),options);
//        }
//#endif
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions options){
        if(reader.TryReadNil()) return default!;
        return this.DelegateDeserialize(ref reader,options);
//        var ctor = typeof(T).GetConstructors()[0];
//        var Parameters = ctor.GetParameters();
//#if 匿名型にキーを入れる
//        var Length=reader.ReadMapHeader();
//        var args=new object[Length];
//        for(var a = 0;a<Length;a++) {
//            var Key = reader.ReadString();
//            Debug.Assert(Parameters[a].Name==Key);
//            args[a]=SerializerConfiguration.DynamicDeserialize(options.Resolver.GetFormatterDynamic(Parameters[a].ParameterType),ref reader,options);
//        }
//#else
//        var Length=reader.ReadArrayHeader();
//        var args=new object[Length];
//        for(var a = 0;a<Length;a++) {
//            //var Key = reader.ReadString();
//            //Debug.Assert(Parameters[a].Name==Key);
//            args[a]=SerializerConfiguration.DynamicDeserialize(options.Resolver.GetFormatterDynamic(Parameters[a].ParameterType),ref reader,options);
//        }
//#endif
//        Debug.Assert(Length==Parameters.Length);
//        return (T)ctor.Invoke(args);
    }
}
