using MessagePack;
//using System.Reflection.Emit;
using System.IO;
using Expressions=System.Linq.Expressions;
using LinqDB.Helpers;
using Emit=System.Reflection.Emit;
using LinqDB.Serializers.Formatters;
using MessagePack.Formatters;
using System.Collections.Generic;
using System.Reflection;

namespace LinqDB.Serializers.MessagePack;
using Formatters;
//using MessagePack;
////using System.Reflection.Emit;
//using System.IO;
//using Expressions=System.Linq.Expressions;
//using LinqDB.Helpers;
//using Emit=System.Reflection.Emit;
//using MessagePack.Formatters;
//namespace LinqDB.Serializers.MessagePack;
////using Formatters;
//using LinqDB.Serializers.MemoryPack.Formatters;

public partial class Serializer:Serializers.Serializer{
    public static readonly Serializer Instance=new();
    //private readonly FormatterResolver Resolver=new();
    public MessagePackSerializerOptions Options;
    private Serializer(){
        this.Options=MessagePackSerializerOptions.Standard.WithResolver(
            global::MessagePack.Resolvers.CompositeResolver.Create(
                new IMessagePackFormatter[]{
                    Object.Instance,
                    Binary.Instance,
                    Block.Instance,
                    Conditional.Instance,
                    Constant.Instance,
                    Default.Instance,
                    Expression.Instance,
                    Goto.Instance,
                    Index.Instance,
                    Invocation.Instance,
                    Label.Instance,
                    LabelTarget.Instance,
                    Lambda.Instance,
                    ListInit.Instance,
                    Loop.Instance,
                    MemberAccess.Instance,
                    MethodCall.Instance,
                    New.Instance,
                    NewArray.Instance,
                    Parameter.Instance,
                    Switch.Instance,
                    Try.Instance,
                    TypeBinary.Instance,
                    Unary.Instance,
                    SwitchCase.Instance,
                    CatchBlock.Instance,
                    ElementInit.Instance,
                    MemberBinding.Instance,
                    MemberInit.Instance,
                    Type.Instance,
                    Member.Instance,
                    Constructor.Instance,
                    Method.Instance,
                    Property.Instance,
                    Event.Instance,
                    Field.Instance,
                },
                new IFormatterResolver[]{
                    //this.AnonymousExpressionMessagePackFormatterResolver,//先頭に無いと匿名型やシリアライズ可能型がDictionaryになってしまう
                    global::MessagePack.Resolvers.BuiltinResolver.Instance,
                    global::MessagePack.Resolvers.DynamicGenericResolver.Instance,//GenericEnumerableFormatter
                    //MessagePack.Resolvers.DynamicEnumAsStringResolver.Instance,
                    //MessagePack.Resolvers.DynamicEnumResolver.Instance,
                    //MessagePack.Resolvers.DynamicObjectResolver.Instance,//MessagePackObjectAttribute
                    global::MessagePack.Resolvers.DynamicObjectResolverAllowPrivate.Instance,//MessagePackObjectAttribute
                    FormatterResolver.Instance
                    //this.Resolver,
                    //MessagePack.Resolvers.StandardResolver.Instance,
                    //MessagePack.Resolvers.StandardResolverAllowPrivate.Instance,
                    //MessagePack.Resolvers.StandardResolver.Instance,//
                    //this.AnonymousExpressionMessagePackFormatterResolver,
                }
            )
        );
        //var e0=this.Options.Resolver.GetFormatter<Expressions.Expression>();
        //var e1=this.Options.Resolver.GetFormatter<Expressions.UnaryExpression>();
        //var e2=this.Options.Resolver.GetFormatter<Expressions.SwitchCase>();
        var e3=this.Options.Resolver.GetFormatter<Expressions.SwitchExpression>();
    }
    //private static readonly object[] objects1 = new object[1];
    internal static void RegisterAnonymousDisplay(System.Type Type,MessagePackSerializerOptions Options) {
        if(Type.IsDisplay()){
            //あらかじめ設定してあるResolverに設定する
            var FormatterType = typeof(DisplayClass<>).MakeGenericType(Type);
            var Instance=FormatterType.GetField(nameof(DisplayClass<int>.Instance))!;
            Options.WithResolver(
                global::MessagePack.Resolvers.CompositeResolver.Create(
                    new IMessagePackFormatter[]{(IMessagePackFormatter)Instance.GetValue(null)!},
                    new IFormatterResolver[]{Serializer.Instance.Options.Resolver}
                )
            );
            //Serializer.Instance.Options=MessagePackSerializerOptions.Standard.WithResolver(
            //    global::MessagePack.Resolvers.CompositeResolver.Create(
            //        new IMessagePackFormatter[]{(IMessagePackFormatter)Instance.GetValue(null)!},
            //        new IFormatterResolver[]{Serializer.Instance.Options.Resolver}
            //    )
            //);

            //this.Options=MessagePackSerializerOptions.Standard.WithResolver()
            //var Register = Serializer.Register.MakeGenericMethod(Type);
            //objects1[0]=Instance.GetValue(null)!;// System.Activator.CreateInstance(FormatterType)!;
            //Register.Invoke(null,objects1);
            //Register.Invoke(null,Array.Empty<object>());
        //}else if(Type.IsGenericType) {
        //    if(Type.IsAnonymous()) {
        //        var FormatterType = typeof(Anonymous<>).MakeGenericType(Type);
        //        var Register = Serializer.Register.MakeGenericMethod(Type);
        //        var Instance=FormatterType.GetField(nameof(DisplayClass<int>.Instance))!;
        //        objects1[0]=Instance.GetValue(null)!;// System.Activator.CreateInstance(FormatterType)!;
        //        Register.Invoke(null,objects1);
        //        //Register.Invoke(null,Array.Empty<object>());
        //    }
        //    foreach(var GenericArgument in Type.GetGenericArguments()) RegisterAnonymousDisplay(GenericArgument);
        }
    }
    //internal readonly List<Expressions.ParameterExpression> ListParameter=new();
    //internal readonly Dictionary<Expressions.LabelTarget,int> Dictionary_LabelTarget_int=new();
    //internal readonly List<Expressions.LabelTarget> LabelTargets=new();
    //internal readonly Dictionary<System.Type,int> DictionaryTypeIndex=new();
    //internal readonly List<System.Type> Types=new();
    //internal readonly Dictionary<System.Type,MemberInfo[]> TypeMembers=new();
    //internal readonly Dictionary<System.Type,ConstructorInfo[]> TypeConstructors=new();
    //internal readonly Dictionary<System.Type,MethodInfo[]> TypeMethods=new();
    //internal readonly Dictionary<System.Type,FieldInfo[]> TypeFields=new();
    //internal readonly Dictionary<System.Type,PropertyInfo[]> TypeProperties=new();
    //internal readonly Dictionary<System.Type,EventInfo[]> TypeEvents=new();
    private void Clear(){
        this.ProtectedClear();
        //this.ListParameter.Clear();
        //this.Dictionary_LabelTarget_int.Clear();
        //this.LabelTargets.Clear();
        //this.DictionaryTypeIndex.Clear();
        //this.Types.Clear();
        //this.TypeMembers.Clear();
        //this.TypeConstructors.Clear();
        //this.TypeMethods.Clear();
        //this.TypeFields.Clear();
        //this.TypeProperties.Clear();
        //this.TypeEvents.Clear();
    }
    public byte[] Serialize<T>(T value){
        this.Clear();
        return MessagePackSerializer.Serialize(value,this.Options);
    }
    public void Serialize<T>(Stream stream,T value){
        this.Clear();
        MessagePackSerializer.Serialize(stream,value,this.Options);
    }
    public T Deserialize<T>(byte[] bytes){
        this.Clear();
        return MessagePackSerializer.Deserialize<T>(bytes,this.Options);
    }
    public T Deserialize<T>(Stream stream){
        this.Clear();
        return MessagePackSerializer.Deserialize<T>(stream,this.Options);
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
        var D=new Emit.DynamicMethod("",typeof(void),SerializeTypes){InitLocals=false};
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
        var D=new Emit.DynamicMethod("",typeof(object),DeserializeTypes){InitLocals=false};
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
}