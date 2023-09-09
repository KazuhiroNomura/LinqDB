using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
public class Anonymous{
    private static void Serialize<T>(ref Writer writer,T value,MessagePackSerializerOptions options)=>options.Resolver.GetFormatter<T>()!.Serialize(ref writer,value,options);
    protected static readonly MethodInfo MethodSerialize=typeof(Anonymous).GetMethod(nameof(Serialize),BindingFlags.Static|BindingFlags.NonPublic)!;
    private static T Deserialize<T>(ref Reader reader,MessagePackSerializerOptions options)=>options.Resolver.GetFormatter<T>()!.Deserialize(ref reader,options);
    protected static readonly MethodInfo MethodDeserialize=typeof(Anonymous).GetMethod(nameof(Deserialize),BindingFlags.Static|BindingFlags.NonPublic)!;
    protected static readonly MethodInfo WriteArrayHeader=typeof(Writer).GetMethod(nameof(Writer.WriteArrayHeader),new []{typeof(int)})!;
    protected static readonly MethodInfo ReadArrayHeader=typeof(Reader).GetMethod(nameof(Reader.ReadArrayHeader))!;
}
public class Anonymous<T>:Anonymous,IMessagePackFormatter<T>{
    private delegate void delegate_Serialize(ref Writer writer,T value,MessagePackSerializerOptions options);
    private readonly delegate_Serialize DelegateSerialize;
    private delegate T delegate_Deserialize(ref Reader reader,MessagePackSerializerOptions options);
    private readonly delegate_Deserialize DelegateDeserialize;
    public Anonymous() {
        var Types1 = new System.Type[1];
        var DeserializeTypes = new System.Type[2];
        var SerializeTypes = new System.Type[3];
        SerializeTypes[0]=typeof(Writer).MakeByRefType();
        DeserializeTypes[0]=typeof(Reader).MakeByRefType();
        SerializeTypes[1]=typeof(T);
        DeserializeTypes[1]=SerializeTypes[2]=typeof(MessagePackSerializerOptions);
        var ctor=typeof(T).GetConstructors()[0];
        var Parameters=ctor.GetParameters();
        var Properties = typeof(T).GetProperties(BindingFlags.Public|BindingFlags.Instance);
        var Properties_Length = Properties.Length;
        Debug.Assert(Parameters.Length==Properties_Length);
        Properties=Parameters.Select(Parameter=>Properties.Single(Property=>Property.Name==Parameter.Name)).ToArray();
        {
            var D0=new DynamicMethod("Serialize",typeof(void),SerializeTypes,typeof(Anonymous<T>),true){InitLocals=false};
            var D1=new DynamicMethod("Deserialize",typeof(T),DeserializeTypes,typeof(Anonymous<T>),true){InitLocals=false};
            //var (D0,D1,ctor,Properties)=((DynamicMethod D0,DynamicMethod D1,ConstructorInfo ctor,PropertyInfo[] Properties))(D0:D2,D1:D3,ctor:Ctor,Properties:Properties1);
            var I0=D0.GetILGenerator();
            var I1=D1.GetILGenerator();
            D0.InitLocals=false;
            D1.InitLocals=false;
            共通(I0,I1);
            this.DelegateSerialize=(delegate_Serialize)D0.CreateDelegate(typeof(delegate_Serialize));
            this.DelegateDeserialize=(delegate_Deserialize)D1.CreateDelegate(typeof(delegate_Deserialize));
        }
        //{
        //    var AssemblyName = new AssemblyName { Name="Name" };
        //    var DynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(AssemblyName,AssemblyBuilderAccess.RunAndCollect);
        //    var ModuleBuilder = DynamicAssembly.DefineDynamicModule("動的");
        //    var Disp_TypeBuilder = ModuleBuilder.DefineType("Disp",TypeAttributes.Public);
        //    var Serialize = Disp_TypeBuilder.DefineMethod($"Serialize",MethodAttributes.Static|MethodAttributes.Public,typeof(void),SerializeTypes);
        //    Serialize.DefineParameter(1,ParameterAttributes.None,"writer");
        //    Serialize.DefineParameter(2,ParameterAttributes.None,"value");
        //    Serialize.DefineParameter(3,ParameterAttributes.None,"options");
        //    Serialize.InitLocals=false;
        //    var Deserialize = Disp_TypeBuilder.DefineMethod($"Deserialize",MethodAttributes.Static|MethodAttributes.Public,typeof(T),DeserializeTypes);
        //    Deserialize.DefineParameter(1,ParameterAttributes.None,"writer");
        //    Deserialize.DefineParameter(2,ParameterAttributes.None,"options");
        //    Deserialize.InitLocals=false;
        //    共通(Serialize.GetILGenerator(),Deserialize.GetILGenerator());
        //    Disp_TypeBuilder.CreateType();
        //    var Folder = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);
        //    new AssemblyGenerator().GenerateAssembly(DynamicAssembly,@$"{Folder}\MessagePackSerializer.dll");
        //}
        void 共通(ILGenerator I0,ILGenerator I1){
            I0.Emit(OpCodes.Ldarg_0);//writer
            I0.Emit(OpCodes.Ldc_I4,Properties_Length);
            I0.Emit(OpCodes.Call,WriteArrayHeader);
            I1.Emit(OpCodes.Ldarg_0);//reader
            I1.Emit(OpCodes.Call,ReadArrayHeader);
            I1.Emit(OpCodes.Pop);
            var index=0;
            while(true){
                var Property=Properties[index];
                Types1[0]=Property.PropertyType;
                I0.Emit(OpCodes.Ldarg_0);//writer
                I0.Emit(OpCodes.Ldarg_1);//value
                Debug.Assert(Property.GetMethod!=null&&!Property.GetMethod.IsVirtual);
                I0.Emit(OpCodes.Call,Property.GetMethod);//value.property
                I0.Emit(OpCodes.Ldarg_2);//options
                I0.Emit(OpCodes.Call,MethodSerialize.MakeGenericMethod(Types1));
                I1.Emit(OpCodes.Ldarg_0);//reader
                I1.Emit(OpCodes.Ldarg_1);//options
                I1.Emit(OpCodes.Call,MethodDeserialize.MakeGenericMethod(Types1));
                index++;
                if(index==Properties_Length) break;
            }
            I0.Emit(OpCodes.Ret);
            I1.Emit(OpCodes.Newobj,ctor);
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
//        var Length = reader.ReadArrayHeader();
//        var args = new object[Length];
//        for(var a = 0;a<Length;a++) {
//            //var Key = reader.ReadString();
//            //Debug.Assert(Parameters[a].Name==Key);
//            //options.Resolver.GetFormatterDynamic(Parameters[a].ParameterType).
//            args[a]=SerializerConfiguration.DynamicDeserialize(options.Resolver.GetFormatterDynamic(Parameters[a].ParameterType),ref reader,options);
//        }
//#endif
//        Debug.Assert(Length==Parameters.Length);
//        return (T)ctor.Invoke(args);
    }
}
