using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using LinqDB.Helpers;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack;
public class Serializer:Serializers.Serializer,IMessagePackFormatter<Serializer>{//IMessagePackFormatter<Serializer>を継承する理由はFormatterでResolverを経由でSerializer情報を取得するため
    public static readonly Dictionary<System.Type,object> TypeFormatter=new();

    private readonly MessagePackSerializerOptions Options;
    public Serializer(){
        var formatters=new IMessagePackFormatter[]{
            this,
            Formatters.Others.Object.Instance,

            Formatters.Binary.Instance,
            Formatters.Block.Instance,
            Formatters.CatchBlock.Instance,
            Formatters.Conditional.Instance,
            Formatters.Constant.Instance,
            Formatters.Default.Instance,
            Formatters.DebugInfo.Instance,
            Formatters.Dynamic.Instance,
            Formatters.ElementInit.Instance,
            Formatters.Expression.Instance,
            Formatters.Goto.Instance,
            Formatters.Index.Instance,
            Formatters.Invocation.Instance,
            Formatters.Label.Instance,
            Formatters.LabelTarget.Instance,
            Formatters.Lambda.Instance,
            Formatters.ListInit.Instance,
            Formatters.Loop.Instance,
            Formatters.MemberAccess.Instance,
            Formatters.MemberBinding.Instance,
            Formatters.MemberInit.Instance,
            Formatters.MethodCall.Instance,
            Formatters.New.Instance,
            Formatters.NewArray.Instance,
            Formatters.Parameter.Instance,
            Formatters.Switch.Instance,
            Formatters.SwitchCase.Instance,
            Formatters.SymbolDocumentInfo.Instance,
            Formatters.Try.Instance,
            Formatters.TypeBinary.Instance,
            Formatters.Unary.Instance,

            Formatters.Reflection.Type.Instance,
            Formatters.Reflection.Member.Instance,
            Formatters.Reflection.Constructor.Instance,
            Formatters.Reflection.Method.Instance,
            Formatters.Reflection.Property.Instance,
            Formatters.Reflection.Event.Instance,
            Formatters.Reflection.Field.Instance,
            Formatters.Others.Delegate.Instance,

            Formatters.CSharpArgumentInfo.Instance,
            
            Formatters.Enumerables.IEnumerable.Instance,
            Formatters.Sets.IEnumerable.Instance,
            
        };
        var resolvers=new IFormatterResolver[]{
            global::MessagePack.Resolvers.StandardResolverAllowPrivate.Instance,
            global::MessagePack.Resolvers.BuiltinResolver.Instance,
            //global::MessagePack.Resolvers.DynamicGenericResolver.Instance,//GenericEnumerableFormatter
            //global::MessagePack.Resolvers.DynamicObjectResolverAllowPrivate.Instance,//MessagePackObjectAttribute
            global::MessagePack.Resolvers.StandardResolverAllowPrivate.Instance,
            FormatterResolver.Instance
        };
        this.Options=MessagePackSerializerOptions.Standard.WithResolver(
            global::MessagePack.Resolvers.CompositeResolver.Create(
                formatters,
                resolvers
            )
        );
    }
    private void Clear(){
        this.ProtectedClear();
        
        
    }
    public override byte[] Serialize<T>(T value){
        this.Clear();
        return MessagePackSerializer.Serialize<object>(value,this.Options);
    }
    public override void Serialize<T>(Stream stream,T value){
        this.Clear();
        MessagePackSerializer.Serialize<object>(stream,value,this.Options);
    }
    public override T Deserialize<T>(byte[] bytes){
        this.Clear();
        return (T)MessagePackSerializer.Deserialize<object>(bytes,this.Options);
    }
    public override T Deserialize<T>(Stream stream){
        this.Clear();
        return (T)MessagePackSerializer.Deserialize<object>(stream,this.Options);
    }

    private delegate void SerializeDelegate(object Formatter,ref MessagePackWriter writer,object value,
        MessagePackSerializerOptions options);
    private static readonly System.Type[] SerializeTypes={
        typeof(object),typeof(MessagePackWriter).MakeByRefType(),typeof(object),typeof(MessagePackSerializerOptions)
    };
    /// <summary>
    /// Invokeではref引数を呼べないため。
    /// </summary>
    /// <param name="Formatter"></param>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public static void DynamicSerialize(object Formatter,ref MessagePackWriter writer,object value,
        MessagePackSerializerOptions options){
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
        ((SerializeDelegate)D.CreateDelegate(typeof(SerializeDelegate)))(Formatter,ref writer,value,options);
    }
    private delegate object DeserializeDelegate(object Formatter,ref MessagePackReader reader,
        MessagePackSerializerOptions options);
    private static readonly System.Type[] DeserializeTypes={
        typeof(object),typeof(MessagePackReader).MakeByRefType(),typeof(MessagePackSerializerOptions)
    };
    public static object DynamicDeserialize(object Formatter,ref MessagePackReader reader,
        MessagePackSerializerOptions options){
        var Method=Formatter.GetType().GetMethod("Deserialize")!;
        var D=new DynamicMethod("",typeof(object),DeserializeTypes){InitLocals=false};
        var I=D.GetILGenerator();
        I.Ldarg_0();
        I.Ldarg_1();
        I.Ldarg_2();
        I.Callvirt(Method);
        I.Box(Method.ReturnType);
        I.Ret();
        var Del=(DeserializeDelegate)D.CreateDelegate(typeof(DeserializeDelegate));
        var Result=Del(Formatter,ref reader,options);
        return Result;
    }
    public void Serialize(ref MessagePackWriter writer,Serializer value,MessagePackSerializerOptions options){
        throw new System.NotImplementedException();
    }
    public Serializer Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions options){
        throw new System.NotImplementedException();
    }
}