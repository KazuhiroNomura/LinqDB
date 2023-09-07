
//using System;
using System;
using Expressions = System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using LinqDB.Helpers;
//using MemoryPack_Formatters = LinqDB.Serializers.MemoryPack.Formatters;
using Utf8Json;
using Utf8Json.Formatters;
//using LinqDB.Serializers.Formatters;

namespace LinqDB.Serializers.Utf8Json;
using Formatters;


//public abstract class ACustomSerializer{
//    //public abstract void Clear();
//    public abstract byte[]Serialize<T>(T value);
//    public abstract void Serialize<T>(Stream stream,T value);
//    public abstract T Deserialize<T>(byte[]bytes);
//    public abstract T Deserialize<T>(Stream stream);
//}
public class Utf8JsonCustomSerializer{
    public static readonly Utf8JsonCustomSerializer Instance=new();
    //internal readonly Binary Binary=new();
    //internal readonly Block Block=new();
    //internal readonly CatchBlock CatchBlock=new();
    //internal readonly Conditional Conditional=new();
    //internal readonly Constant Constant=new();
    //internal readonly Constructor Constructor=new();
    //internal readonly Default Default=new();
    //internal readonly ElementInit ElementInit=new();
    //internal readonly Expression Expression=new();
    //internal readonly Field Field=new();
    //internal readonly Goto Goto=new();
    //internal readonly Index Index=new();
    //internal readonly Invocation Invocation=new();
    //internal readonly Label Label=new();
    //internal readonly LabelTarget LabelTarget=new();
    //internal readonly Lambda Lambda=new();
    //internal readonly ListInit ListInit=new();
    //internal readonly Loop Loop=new();
    //internal readonly Member Member=new();
    //internal readonly MemberAccess MemberAccess=new();
    //internal readonly MemberBinding MemberBinding=new();
    //internal readonly MemberInit MemberInit=new();
    //internal readonly Method Method=new();
    //internal readonly MethodCall MethodCall=new();
    //internal readonly New New=new();
    //internal readonly NewArray NewArray=new();
    //internal readonly Object Object=new();
    //internal readonly Parameter Parameter=new();
    //internal readonly Property Property=new();
    //internal readonly Switch Switch=new();
    //internal readonly SwitchCase SwitchCase=new();
    //internal readonly Try Try=new();
    //internal readonly Type Type=new();
    //internal readonly TypeBinary TypeBinary=new();
    //internal readonly Unary Unary=new();
    //private readonly Expression ExpressionFormatter=new();

    //private readonly ExpressionJsonFormatter ExpressionFormatter=new();
    private sealed class Utf8JsonCustomFormatterResolver:IJsonFormatterResolver{
        //private readonly Type[] GenericArguments=new Type[1];
        public readonly Dictionary<System.Type,IJsonFormatter> DictionaryTypeFormatter=new();
        public IJsonFormatter<T> GetFormatter<T>(){
            if(this.DictionaryTypeFormatter.TryGetValue(typeof(T),out var IJsonFormatter))
                return(IJsonFormatter<T>)IJsonFormatter;
            if(typeof(Expressions.Expression).IsAssignableFrom(typeof(T))){
                //return Return(Instance.ExpressionFormatter);
                return null!;
            }
            if(
                typeof(T)==typeof(System.Type)||
                typeof(T)==typeof(MemberInfo)||
                typeof(T)==typeof(MethodInfo)||
                typeof(T)==typeof(FieldInfo)||
                typeof(T)==typeof(PropertyInfo)||
                typeof(T)==typeof(EventInfo)||
                typeof(Expressions.MemberBinding).IsAssignableFrom(typeof(T))||
                typeof(T)==typeof(MethodInfo)||
                typeof(T)==typeof(MemberInfo)||
                typeof(T)==typeof(Expressions.CatchBlock)||
                typeof(T)==typeof(Expressions.SwitchCase)||
                typeof(T)==typeof(Expressions.ElementInit))
                return null!;
                //return Return(Instance.ExpressionFormatter);
            if(typeof(T).IsDisplay()){
                var Formatter=new DisplayClass<T>();
                Instance.IResolver=global::Utf8Json.Resolvers.CompositeResolver.Create(
                    new IJsonFormatter[]{Formatter},
                    new []{Instance.IResolver}
                );
                return Formatter;
            }
            if(typeof(T).IsAnonymous()){
                var Formatter=new Anonymous<T>();
                Instance.IResolver=global::Utf8Json.Resolvers.CompositeResolver.Create(
                    new IJsonFormatter[]{Formatter},
                    new []{Instance.IResolver}
                );
                return Formatter;
            }
            //var Formatter=GetFormatter(typeof(T));
            //if(Formatter!=null) Return(Formatter);
            return Return(new Abstract<T>());
            IJsonFormatter<T> Return(object Formatter){
                var result=(IJsonFormatter<T>)Formatter;
                this.DictionaryTypeFormatter.Add(typeof(T),result);
                return result;
            }
        }
        public void Clear(){
            //this.ExpressionFormatter.Clear();
            this.DictionaryTypeFormatter.Clear();
        }
    }
    private readonly Utf8JsonCustomFormatterResolver Resolver=new();
    public IJsonFormatterResolver IResolver;
    //public IJsonFormatter<T>? GetFormatter<T>()=>(IJsonFormatter<T>?)this.GetFormatterDynamic(typeof(T));
    private readonly System.Type[]Types1=new System.Type[1];
    public IJsonFormatter? GetFormatterDynamic(System.Type Type){
        var DictionaryTypeFormatter=this.Resolver.DictionaryTypeFormatter;
        if(DictionaryTypeFormatter.TryGetValue(Type,out var value)) return value;
        if(Type.IsAnonymous()){
            var Types1=this.Types1;
            Types1[0]=Type;
            value=(IJsonFormatter)Activator.CreateInstance(typeof(Anonymous<>).MakeGenericType(Types1))!;
            DictionaryTypeFormatter.Add(
                Type,
                value
            );
            this.IResolver=global::Utf8Json.Resolvers.CompositeResolver.Create(
                new IJsonFormatter[]{value},
                new []{this.IResolver}
            );
        }
        return value;
    }
    //public void Register(Type type){
    //    var DictionaryTypeFormatter=this.Resolver.DictionaryTypeFormatter;
    //    if(DictionaryTypeFormatter.)
    //}
    private Utf8JsonCustomSerializer(){
        this.IResolver=global::Utf8Json.Resolvers.CompositeResolver.Create(
            //System.Array.Empty<IJsonFormatter>(),
            new IJsonFormatter[]{
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
            new IJsonFormatterResolver[]{
                //    this.AnonymousExpressionJsonFormatterResolver,//先頭に無いと匿名型やシリアライズ可能型がDictionaryになってしまう
                //    short
                //    int
                //    byte[]
                //    Nil
                //    Nil?
                //    List<short>
                //    List<int>
                global::Utf8Json.Resolvers.BuiltinResolver.Instance,//よく使う型
                //    Lazy<>
                global::Utf8Json.Resolvers.DynamicGenericResolver.Instance,//主にジェネリックコレクション
                global::Utf8Json.Resolvers.EnumResolver.Default,global::Utf8Json.Resolvers.AttributeFormatterResolver.Instance,
                //Utf8Json.Resolvers.StandardResolver.Default,

                global::Utf8Json.Resolvers.DynamicObjectResolver.Default,
                //Utf8Json.Resolvers.CompositeResolver.Instance,



                this.Resolver,



                //Utf8Json.Resolvers.DynamicObjectResolver.Default,//これが存在するとStackOverflowする
                //Utf8Json.Resolvers.DynamicObjectResolver.AllowPrivate,//これが存在するとTypeがシリアライズできない

                //BuiltinResolver.Instance,
                //EnumResolver.Default,
                //DynamicGenericResolver.Instance,
                //AttributeFormatterResolver.Instance
                //Utf8Json.Resolvers.EnumResolver.Default,
                //Utf8Json.Resolvers.DynamicGenericResolver.Instance,
                //Utf8Json.Resolvers.AttributeFormatterResolver.Instance,
                //Utf8Json.Resolvers.StandardResolver.AllowPrivate,//いくつかのリゾルバをまとめてある
                //Utf8Json.Resolvers.StandardResolver.Default,
                //this.AnonymousExpressionJsonFormatterResolver,
            }
        );
    }
    internal readonly List<Expressions.ParameterExpression> ListParameter=new();
    internal readonly Dictionary<Expressions.LabelTarget,int> Dictionary_LabelTarget_int=new();
    internal readonly List<Expressions.LabelTarget> ListLabelTarget=new();
    internal readonly Dictionary<System.Type,int> DictionaryTypeIndex=new();
    internal readonly List<System.Type> Types=new();
    internal readonly Dictionary<System.Type,MemberInfo[]> TypeMembers=new();
    internal readonly Dictionary<System.Type,MethodInfo[]> TypeMethods=new();
    internal readonly Dictionary<System.Type,FieldInfo[]> TypeFields=new();
    internal readonly Dictionary<System.Type,PropertyInfo[]> TypeProperties=new();
    internal readonly Dictionary<System.Type,EventInfo[]> TypeEvents=new();
    private void Clear(){
        this.Resolver.Clear();
        this.ListParameter.Clear();
        this.Dictionary_LabelTarget_int.Clear();
        this.ListLabelTarget.Clear();
        this.DictionaryTypeIndex.Clear();
        this.Types.Clear();
        this.TypeMethods.Clear();
        this.TypeFields.Clear();
        this.TypeProperties.Clear();
        this.TypeEvents.Clear();
    }
    public byte[] Serialize<T>(T value){
        this.Clear();
        return JsonSerializer.Serialize(value,this.IResolver);
    }
    public void Serialize<T>(Stream stream,T value){
        this.Clear();
        JsonSerializer.Serialize(stream,value,this.IResolver);
    }
    public T Deserialize<T>(byte[] bytes){
        this.Clear();
        return JsonSerializer.Deserialize<T>(bytes,this.IResolver);
    }
    public T Deserialize<T>(Stream stream){
        this.Clear();
        return JsonSerializer.Deserialize<T>(stream,this.IResolver);
    }
    public void SerializeReadOnlyCollection<T>(ref JsonWriter writer,T value)=>
        this.Resolver.GetFormatter<T>().Serialize(ref writer,value,this.Resolver);
    public static void Serialize_Type(ref JsonWriter writer,System.Type value){
        writer.WriteString(value.AssemblyQualifiedName);
    }
    public T Deserialize_T<T>(ref JsonReader reader)=>
        this.Resolver.GetFormatter<T>().Deserialize(ref reader,this.Resolver);
    //private static class StaticReadOnlyCollectionFormatter<T>{
    //    public static readonly ReadOnlyCollectionFormatter<T> Formatter=new();
    //}
    //internal static void SerializeReadOnlyCollection<T>(ref JsonWriter writer,
    //    ReadOnlyCollection<T>? value,IJsonFormatterResolver Resolver) =>
    //    StaticReadOnlyCollectionFormatter<T>.Formatter.Serialize(ref writer,value!,Resolver);
    //private static class StaticArrayFormatter<T>{
    //    public static readonly ArrayFormatter<T> Formatter=new();
    //}
    //internal static T[] DeserializeArray<T>(ref JsonReader reader,IJsonFormatterResolver Resolver) =>
    //    StaticArrayFormatter<T>.Formatter.Deserialize(ref reader,Resolver)!;
    //public static System.Type Deserialize_Type(ref JsonReader reader,IJsonFormatterResolver Resolver){
    //    var AssemblyQualifiedName=reader.ReadString();
    //    return System.Type.GetType(AssemblyQualifiedName)!;
    //}
    //internal static void Serialize宣言Parameters(ref JsonWriter writer,ReadOnlyCollection<Expressions.ParameterExpression> value,
    //    IJsonFormatterResolver Resolver){
    //    writer.WriteBeginArray();
    //    var Count=value.Count;
    //    if(Count>0){
    //        for(var a=0;;a++){
    //            var Parameter=value[a];
    //            writer.WriteBeginObject();
    //            writer.WriteString(Parameter.Name);
    //            writer.WriteNameSeparator();
    //            Instance.Type.Serialize(ref writer,Parameter.Type,Resolver);
    //            writer.WriteEndObject();
    //            if(a==Count-1) break;
    //            writer.WriteValueSeparator();
    //        }
    //    }
    //    writer.WriteEndArray();
    //}
    //internal static List<Expressions.ParameterExpression> Deserialize宣言Parameters(ref JsonReader reader,
    //    IJsonFormatterResolver Resolver){
    //    var List=new List<Expressions.ParameterExpression>();
    //    //var t=reader;
    //    reader.ReadIsBeginArrayWithVerify();
    //    while(reader.ReadIsBeginObject()){
    //        var name=reader.ReadString();
    //        reader.ReadIsNameSeparatorWithVerify();
    //        var type=reader.ReadType()
    //        List.Add(Expressions.Expression.Parameter(type,name));
    //        reader.ReadIsEndObjectWithVerify();
    //        //var count=0;
    //        //if(!t.ReadIsEndObjectWithSkipValueSeparator(ref count)) break;
    //        if(!reader.ReadIsValueSeparator()) break;
    //    }
    //    reader.ReadIsEndArrayWithVerify();
    //    return List;
    //}
}