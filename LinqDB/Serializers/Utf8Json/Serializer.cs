using System.IO;

using Utf8Json;

namespace LinqDB.Serializers.Utf8Json;
public class Serializer:Serializers.Serializer,IJsonFormatter<Serializer>{
    internal readonly System.Collections.Generic.Dictionary<System.Type,IJsonFormatter> TypeFormatter=new();
    private readonly FormatterResolver Resolver=new();
    private readonly IJsonFormatterResolver IResolver;
    public Serializer(){
        var formatters=new IJsonFormatter[]{

            Formatters.Others.Object.Instance,

            Formatters.Binary.Instance,
            Formatters.Block.Instance,
            Formatters.CatchBlock.Instance,
            Formatters.Conditional.Instance,
            Formatters.Constant.Instance,
            Formatters.DebugInfo.Instance,
            Formatters.Default.Instance,
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
            this
        };
        var resovers=new[]{
            this.Resolver,
            global::Utf8Json.Resolvers.BuiltinResolver.Instance,//int,byte,double[],List<>,よく使う型
            global::Utf8Json.Resolvers.DynamicGenericResolver.Instance,//主にジェネリックコレクション
            global::Utf8Json.Resolvers.EnumResolver.Default,
            global::Utf8Json.Resolvers.AttributeFormatterResolver.Instance,
            global::Utf8Json.Resolvers.DynamicObjectResolver.AllowPrivate,//.Default,//Anonymous
        };
        this.IResolver=global::Utf8Json.Resolvers.CompositeResolver.Create(
            formatters,
            resovers
        );
    }


    private void Clear(){
        this.ProtectedClear();
        this.Resolver.Clear();
        this.TypeFormatter.Clear();
    }
    public override byte[] Serialize<T>(T value){
        this.Clear();
        return JsonSerializer.Serialize<object>(value,this.IResolver);
    }
    public override void Serialize<T>(Stream stream,T value){
        this.Clear();
        JsonSerializer.Serialize<object>(stream,value,this.IResolver);
    }
    public override T Deserialize<T>(byte[] bytes){
        this.Clear();
        return (T)JsonSerializer.Deserialize<object>(bytes,this.IResolver);
    }
    public override T Deserialize<T>(Stream stream){
        this.Clear();
        return (T)JsonSerializer.Deserialize<object>(stream,this.IResolver);
    }

    public void Serialize(ref JsonWriter writer,Serializer value,IJsonFormatterResolver formatterResolver){
        throw new System.NotImplementedException();
    }
    public Serializer Deserialize(ref JsonReader reader,IJsonFormatterResolver formatterResolver){
        throw new System.NotImplementedException();
    }
}