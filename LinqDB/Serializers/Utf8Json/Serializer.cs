using System.IO;


using Utf8Json;


namespace LinqDB.Serializers.Utf8Json;
using Formatters;
public class Serializer:Serializers.Serializer,IJsonFormatter<Serializer>{
    private readonly FormatterResolver Resolver=new();
    private readonly IJsonFormatterResolver IResolver;
#pragma warning disable CA2211
    public static int instancecount;
#pragma warning restore CA2211
    public Serializer(){
        instancecount++;
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

                CSharpArgumentInfo.Instance,
                this
            },
            new IJsonFormatterResolver[]{
                //this,
                this.Resolver,
                global::Utf8Json.Resolvers.BuiltinResolver.Instance,//int,byte,double[],List<>,よく使う型
                global::Utf8Json.Resolvers.DynamicGenericResolver.Instance,//主にジェネリックコレクション
                global::Utf8Json.Resolvers.EnumResolver.Default,
                global::Utf8Json.Resolvers.AttributeFormatterResolver.Instance,
                //Utf8Json.Resolvers.StandardResolver.Default,

                global::Utf8Json.Resolvers.DynamicObjectResolver.AllowPrivate,//.Default,//Anonymous
            }
        );
    }
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