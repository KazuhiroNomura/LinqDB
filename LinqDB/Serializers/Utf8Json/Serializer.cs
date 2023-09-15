
using System.Collections.Generic;
using System.IO;
using Utf8Json;

namespace LinqDB.Serializers.Utf8Json;
using Formatters;


public class Serializer:Serializers.Serializer,IJsonFormatter<Serializer>{
    //public static readonly Serializer Instance=new();
    private readonly FormatterResolver Resolver=new();
    private readonly IJsonFormatterResolver IResolver;
    
    public Serializer(){
        this.IResolver=global::Utf8Json.Resolvers.CompositeResolver.Create(
            new IJsonFormatter[]{
                Object.Instance,

                Binary.Instance,
                Block.Instance,
                CatchBlock.Instance,
                Conditional.Instance,
                Constant.Instance,
                DebugInfo.Instance,
                Default.Instance,
                Dynamic.Instance,
                ElementInit.Instance,
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
                MemberBinding.Instance,
                MemberInit.Instance,
                MethodCall.Instance,
                New.Instance,
                NewArray.Instance,
                Parameter.Instance,
                Switch.Instance,
                SwitchCase.Instance,
                Try.Instance,
                TypeBinary.Instance,
                Unary.Instance,

                Type.Instance,
                Member.Instance,
                Constructor.Instance,
                Method.Instance,
                Property.Instance,
                Event.Instance,
                Field.Instance,
                this
            },
            new IJsonFormatterResolver[]{
                //this,
                this.Resolver,
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
                global::Utf8Json.Resolvers.EnumResolver.Default,
                global::Utf8Json.Resolvers.AttributeFormatterResolver.Instance,
                //Utf8Json.Resolvers.StandardResolver.Default,

                global::Utf8Json.Resolvers.DynamicObjectResolver.AllowPrivate,//.Default,//Anonymous
                //Utf8Json.Resolvers.CompositeResolver.Instance,






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
    public readonly Dictionary<System.Type,object> DictionaryTypeFormatter = new();
    //internal object? RegisterDisplay(System.Type Type) {
    //    if(Type.IsDisplay()){
    //        if(this.DictionaryDisplayFormatter.TryGetValue(Type,out var Formatter)) return Formatter;
    //        var FormatterType = typeof(DisplayClass<>).MakeGenericType(Type);
    //        var Instance=FormatterType.GetField(nameof(DisplayClass<int>.Instance))!;
    //        Formatter=Instance.GetValue(null)!;
    //        this.DictionaryDisplayFormatter.Add(Type,Formatter);
    //        return Formatter;
    //    }
    //    return null;
    //}
    private void Clear(){
        this.Resolver.Clear();
        this.ProtectedClear();
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

    public IJsonFormatter<T> GetFormatter<T>(){
        return (IJsonFormatter<T>)this;
    }
    public void Serialize(ref JsonWriter writer,Serializer value,IJsonFormatterResolver formatterResolver){
        throw new System.NotImplementedException();
    }
    public Serializer Deserialize(ref JsonReader reader,IJsonFormatterResolver formatterResolver){
        throw new System.NotImplementedException();
    }
}