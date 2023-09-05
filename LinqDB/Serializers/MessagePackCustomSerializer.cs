using MessagePack;
using System.Reflection.Emit;
using System.IO;
using LinqDB.Helpers;
using MemoryPack_Formatters=LinqDB.Serializers.MemoryPack.Formatters;
using LinqDB.Serializers.Formatters;
using MessagePack.Formatters;
using System.Collections.Generic;
using Expressions=System.Linq.Expressions;
using MessagePackSerializer=MessagePack.MessagePackSerializer;
namespace LinqDB.Serializers;
//public abstract class ACustomSerializer{
//    //public abstract void Clear();
//    public abstract byte[]Serialize<T>(T value);
//    public abstract void Serialize<T>(Stream stream,T value);
//    public abstract T Deserialize<T>(byte[]bytes);
//    public abstract T Deserialize<T>(Stream stream);
//}
public static class MessagePackCustomSerializer{
    private sealed class FormatterResolver:IFormatterResolver{
        private readonly ExpressionMessagePackFormatter ExpressionFormatter=new();
        private readonly Dictionary<System.Type,IMessagePackFormatter> Dictionary_Type_Formatter = new();
        public IMessagePackFormatter<T> GetFormatter<T>(){
            if(typeof(T).IsDefined(typeof(MessagePackSerializer),true)) return null!;
            //if(Attribute.IsDefined(typeof(T),typeof(MessagePackSerializer))) return null!;
            if(this.Dictionary_Type_Formatter.TryGetValue(typeof(T),out var IMessagePackFormatter))
                return(IMessagePackFormatter<T>)IMessagePackFormatter;
            if(typeof(Expressions.Expression).IsAssignableFrom(typeof(T))) return Return(this.ExpressionFormatter);
            if(
                typeof(T)==typeof(System.Type)||
                typeof(T)==typeof(System.Reflection.MemberInfo)||
                typeof(T)==typeof(System.Reflection.MethodInfo)||
                typeof(T)==typeof(System.Reflection.FieldInfo)||
                typeof(T)==typeof(System.Reflection.PropertyInfo)||
                typeof(T)==typeof(System.Reflection.EventInfo)||
                typeof(Expressions.MemberBinding).IsAssignableFrom(typeof(T))||
                typeof(T)==typeof(Expressions.CatchBlock)||
                typeof(T)==typeof(Expressions.SwitchCase)||
                typeof(T)==typeof(Expressions.ElementInit))
                return Return(this.ExpressionFormatter);
            if(typeof(T).IsDisplay()) return Return(new DisplayClassMessagePackFormatter<T>());
            if(typeof(T).IsAnonymous()) return Return(new AnonymousMessagePackFormatter<T>());
            return Return(new AbstractMessagePackFormatter<T>());
            //if(!typeof(T).IsValueType&&!typeof(T).IsSealed)return Return(new AbstractMessagePackFormatter<T>());
            //return null!;
            IMessagePackFormatter<T> Return(object Formatter){
                var result=(IMessagePackFormatter<T>)Formatter;
                this.Dictionary_Type_Formatter.Add(typeof(T),result);
                return result;
            }
        }
        public void Clear() {
            this.ExpressionFormatter.Clear();
            this.Dictionary_Type_Formatter.Clear();
        }
    }
    private static readonly FormatterResolver Resolver=new();
    private static readonly MessagePackSerializerOptions Options=MessagePackSerializerOptions.Standard.WithResolver(
        MessagePack.Resolvers.CompositeResolver.Create(
            new IFormatterResolver[]{
                //this.AnonymousExpressionMessagePackFormatterResolver,//先頭に無いと匿名型やシリアライズ可能型がDictionaryになってしまう
                MessagePack.Resolvers.BuiltinResolver.Instance,
                MessagePack.Resolvers.DynamicGenericResolver.Instance,//GenericEnumerableFormatter
                //MessagePack.Resolvers.DynamicEnumAsStringResolver.Instance,
                //MessagePack.Resolvers.DynamicEnumResolver.Instance,
                //MessagePack.Resolvers.DynamicObjectResolver.Instance,//MessagePackObjectAttribute
                MessagePack.Resolvers.DynamicObjectResolverAllowPrivate.Instance,//MessagePackObjectAttribute
                Resolver,
                //MessagePack.Resolvers.StandardResolver.Instance,
                //MessagePack.Resolvers.StandardResolverAllowPrivate.Instance,
                //MessagePack.Resolvers.StandardResolver.Instance,//
                //this.AnonymousExpressionMessagePackFormatterResolver,
            }
        )
    );
    //public static byte[] Serialize<T>(T value){
    //    Clear();
    //    return MessagePackSerializer.Serialize(value);
    //}
    //public static void Serialize<T>(Stream stream,T? value){
    //    Clear();
    //    MessagePackSerializer.Serialize(stream,value);
    //}
    //public static T Deserialize<T>(byte[] bytes){
    //    Clear();
    //    return MessagePackSerializer.Deserialize<T>(bytes)!;
    //}
    //public static T Deserialize<T>(Stream stream){
    //    Clear();
    //    return MessagePackSerializer.Deserialize<T>(stream);
    //}
    private static void Clear()=>Resolver.Clear();
    public static byte[] Serialize<T>(T value){
        Clear();
        return MessagePackSerializer.Serialize(value,Options);
    }
    public static void Serialize<T>(Stream stream,T value){
        Clear();
        MessagePackSerializer.Serialize(stream,value,Options);
    }
    public static T Deserialize<T>(byte[] bytes){
        Clear();
        return MessagePackSerializer.Deserialize<T>(bytes,Options);
    }
    public static T Deserialize<T>(Stream stream){
        Clear();
        return MessagePackSerializer.Deserialize<T>(stream,Options);
    }
    private delegate void SerializeDelegate(object Formatter,ref MessagePackWriter writer,object value,MessagePackSerializerOptions options);
    private static readonly System.Type[] SerializeTypes={typeof(object),typeof(MessagePackWriter).MakeByRefType(),typeof(object),typeof(MessagePackSerializerOptions)};
    public static void DynamicSerialize(object Formatter,ref MessagePackWriter writer,object value,MessagePackSerializerOptions options) {
        var Formatter_Serialize = Formatter.GetType().GetMethod("Serialize")!;
        var D = new DynamicMethod("",typeof(void),SerializeTypes) {
            InitLocals=false
        };
        var I = D.GetILGenerator();
        I.Ldarg_0();
        I.Ldarg_1();
        I.Ldarg_2();
        I.Unbox_Any(Formatter_Serialize.GetParameters()[1].ParameterType);
        I.Ldarg_3();
        I.Callvirt(Formatter_Serialize);
        I.Ret();
        ((SerializeDelegate)D.CreateDelegate(typeof(SerializeDelegate)))(Formatter,ref writer,value,options);
    }
    private delegate object DeserializeDelegate            (object Formatter,ref MessagePackReader reader          ,       MessagePackSerializerOptions options);
    private static readonly System.Type[] DeserializeTypes={typeof(object),      typeof(MessagePackReader).MakeByRefType(),typeof(MessagePackSerializerOptions)};
    public static object DynamicDeserialize(object Formatter,ref MessagePackReader reader,MessagePackSerializerOptions options){
        var Method=Formatter.GetType().GetMethod("Deserialize")!;
        var D=new DynamicMethod("",typeof(object),DeserializeTypes){
            InitLocals=false
        };
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
