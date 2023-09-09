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
public class Serializer:Serializers.Serializer{
    public static readonly Serializer Instance=new();
    private sealed class FormatterResolver:IFormatterResolver{
        private readonly Expression ExpressionFormatter=new();
        public readonly Dictionary<System.Type,IMessagePackFormatter> DictionaryTypeFormatter=new();
        public IMessagePackFormatter<T> GetFormatter<T>(){
            if(typeof(T).IsDefined(typeof(MessagePackSerializer),true)) return null!;
            //if(Attribute.IsDefined(typeof(T),typeof(MessagePackSerializer))) return null!;
            if(this.DictionaryTypeFormatter.TryGetValue(typeof(T),out var IMessagePackFormatter))
                return(IMessagePackFormatter<T>)IMessagePackFormatter;
            if(typeof(Expressions.Expression).IsAssignableFrom(typeof(T))) return Return(this.ExpressionFormatter);
            if(
                typeof(T)==typeof(System.Type)||
                typeof(T)==typeof(MemberInfo)||
                typeof(T)==typeof(MethodInfo)||
                typeof(T)==typeof(FieldInfo)||
                typeof(T)==typeof(PropertyInfo)||
                typeof(T)==typeof(EventInfo)||
                typeof(Expressions.MemberBinding).IsAssignableFrom(typeof(T))||
                typeof(T)==typeof(Expressions.CatchBlock)||
                typeof(T)==typeof(Expressions.SwitchCase)||
                typeof(T)==typeof(Expressions.ElementInit))
                return null!;//Return(this.ExpressionFormatter);
            if(typeof(T).IsDisplay()) return Return(new DisplayClassMessagePackFormatter<T>());
            if(typeof(T).IsAnonymous()) return Return(new Anonymous<T>());
            return Return(new AbstractMessagePackFormatter<T>());
            //if(!typeof(T).IsValueType&&!typeof(T).IsSealed)return Return(new AbstractMessagePackFormatter<T>());
            //return null!;
            IMessagePackFormatter<T> Return(object Formatter){
                var result=(IMessagePackFormatter<T>)Formatter;
                this.DictionaryTypeFormatter.Add(typeof(T),result);
                return result;
            }
        }
        public void Clear(){
            this.DictionaryTypeFormatter.Clear();
        }
    }
    private readonly FormatterResolver Resolver=new();
    public IMessagePackFormatter<T>? GetFormatter<T>()=>this.Resolver.DictionaryTypeFormatter.TryGetValue(typeof(T),out var value)?(IMessagePackFormatter<T>?)value:null;
    public IMessagePackFormatter? GetFormatter(System.Type Type)=>this.Resolver.DictionaryTypeFormatter.TryGetValue(Type,out var value)?(IMessagePackFormatter?)value:null;
    public readonly MessagePackSerializerOptions Options;
    private Serializer(){
        this.Options=MessagePackSerializerOptions.Standard.WithResolver(
            global::MessagePack.Resolvers.CompositeResolver.Create(
                new IMessagePackFormatter[]{
                    Object.Instance,
                    Binary.Instance,
                    Block.Instance,
                    CatchBlock.Instance,
                    Conditional.Instance,
                    Constant.Instance,
                    Constructor.Instance,
                    Default.Instance,
                    ElementInit.Instance,
                    Event.Instance,
                    Expression.Instance,
                    Field.Instance,
                    Goto.Instance,
                    Index.Instance,
                    Invocation.Instance,
                    Label.Instance,
                    LabelTarget.Instance,
                    Lambda.Instance,
                    ListInit.Instance,
                    Loop.Instance,
                    Member.Instance,
                    MemberAccess.Instance,
                    MemberBinding.Instance,
                    MemberInit.Instance,
                    Method.Instance,
                    MethodCall.Instance,
                    New.Instance,
                    NewArray.Instance,
                    //Object.Instance,
                    Parameter.Instance,
                    Property.Instance,
                    Switch.Instance,
                    SwitchCase.Instance,
                    Try.Instance,
                    Type.Instance,
                    TypeBinary.Instance,
                    Unary.Instance,
                },
                new IFormatterResolver[]{
                    //this.AnonymousExpressionMessagePackFormatterResolver,//先頭に無いと匿名型やシリアライズ可能型がDictionaryになってしまう
                    global::MessagePack.Resolvers.BuiltinResolver.Instance,
                    global::MessagePack.Resolvers.DynamicGenericResolver.Instance,//GenericEnumerableFormatter
                    //MessagePack.Resolvers.DynamicEnumAsStringResolver.Instance,
                    //MessagePack.Resolvers.DynamicEnumResolver.Instance,
                    //MessagePack.Resolvers.DynamicObjectResolver.Instance,//MessagePackObjectAttribute
                    global::MessagePack.Resolvers.DynamicObjectResolverAllowPrivate
                        .Instance,//MessagePackObjectAttribute
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
        base.ProtectedClear();
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
    public static void DynamicSerialize(object Formatter,ref MessagePackWriter writer,object value,
        MessagePackSerializerOptions options){
        var Formatter_Serialize=Formatter.GetType().GetMethod("Serialize")!;
        var D=new Emit.DynamicMethod("",typeof(void),SerializeTypes){InitLocals=false};
        var I=D.GetILGenerator();
        I.Ldarg_0();
        I.Ldarg_1();
        I.Ldarg_2();
        I.Unbox_Any(Formatter_Serialize.GetParameters()[1].ParameterType);
        I.Ldarg_3();
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