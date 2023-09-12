
//using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
//using MemoryPack_Formatters = LinqDB.Serializers.MemoryPack.Formatters;
using Utf8Json;
using Utf8Json.Resolvers;
//using LinqDB.Serializers.Formatters;

namespace LinqDB.Serializers.Utf8Json;
using Formatters;

using LinqDB.Helpers;
//using LinqDB.Serializers.MemoryPack.Formatters;

using System.Runtime.Serialization;

//using LinqDB.Serializers.MemoryPack.Formatters;

public class Serializer:Serializers.Serializer{
    public static readonly Serializer Instance=new();
    private readonly FormatterResolver Resolver=new();
    internal IJsonFormatterResolver IResolver;
    //public IJsonFormatter<T>? GetFormatter<T>()=>(IJsonFormatter<T>?)this.GetFormatterDynamic(typeof(T));
    //private readonly System.Type[]Types1=new System.Type[1];
    //public IJsonFormatter? GetFormatterDynamic(System.Type Type){
    //    var DictionaryTypeFormatter=this.Resolver.DictionaryTypeFormatter;
    //    if(DictionaryTypeFormatter.TryGetValue(Type,out var value)) return value;
    //    if(Type.IsAnonymous()){
    //        var Types1=this.Types1;
    //        Types1[0]=Type;
    //        value=(IJsonFormatter)Activator.CreateInstance(typeof(Anonymous<>).MakeGenericType(Types1))!;
    //        DictionaryTypeFormatter.Add(
    //            Type,
    //            value
    //        );
    //        this.IResolver=global::Utf8Json.Resolvers.CompositeResolver.Create(
    //            new IJsonFormatter[]{value},
    //            new []{this.IResolver}
    //        );
    //    }
    //    return value;
    //}
    //public void Register(Type type){
    //    var DictionaryTypeFormatter=this.Resolver.DictionaryTypeFormatter;
    //    if(DictionaryTypeFormatter.)
    //}
    private Serializer(){
        this.IResolver=global::Utf8Json.Resolvers.CompositeResolver.Create(
            new IJsonFormatter[]{
                Object.Instance,
                Binary.Instance,
                Block.Instance,
                Conditional.Instance,
                Constant.Instance,
                Default.Instance,
                Dynamic.Instance,
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

                global::Utf8Json.Resolvers.DynamicObjectResolver.AllowPrivate,//.Default,//Anonymous
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
    //private void Clear(){
    //    this.Resolver.Clear();
    //    this.ListParameter.Clear();
    //    this.Dictionary_LabelTarget_int.Clear();
    //    this.LabelTargets.Clear();
    //    this.DictionaryTypeIndex.Clear();
    //    this.Types.Clear();
    //    this.TypeMembers.Clear();
    //    this.TypeConstructors.Clear();
    //    this.TypeMethods.Clear();
    //    this.TypeFields.Clear();
    //    this.TypeProperties.Clear();
    //    this.TypeEvents.Clear();
    //}
    //private static readonly IJsonFormatter[] IJsonFormatters = new IJsonFormatter[1];
    //private static readonly IJsonFormatterResolver[] IJsonFormatterResolvers= new IJsonFormatterResolver[1];
    //private static readonly object[] objects1 = new object[1];
    public readonly Dictionary<System.Type,object> DictionarySerialize = new();
    internal object? RegisterDisplay(System.Type Type) {
        if(Type.IsDisplay()){
            if(this.DictionarySerialize.TryGetValue(Type,out var Formatter)) return Formatter;
            var FormatterType = typeof(DisplayClass<>).MakeGenericType(Type);
            var Instance=FormatterType.GetField(nameof(DisplayClass<int>.Instance))!;
            try{
                Formatter=Instance.GetValue(null)!;
                this.DictionarySerialize.Add(Type,Formatter);
                return Formatter;
            } catch(Exception ex){
                Console.Write(ex);
                throw;
            }
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
        return null;
    }
    private void Clear(){
        this.Resolver.Clear();
        this.ProtectedClear();
        //    this.ListParameter.Clear();
        //    this.Dictionary_LabelTarget_int.Clear();
        //    this.LabelTargets.Clear();
        //    this.DictionaryTypeIndex.Clear();
        //    this.Types.Clear();
        //    this.TypeMembers.Clear();
        //    this.TypeConstructors.Clear();
        //    this.TypeMethods.Clear();
        //    this.TypeFields.Clear();
        //    this.TypeProperties.Clear();
        //    this.TypeEvents.Clear();
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
    //public static void Serialize_Type(ref JsonWriter writer,System.Type value){
    //    writer.WriteString(value.AssemblyQualifiedName);
    //}
}