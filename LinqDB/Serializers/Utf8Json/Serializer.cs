using System.IO;


using Utf8Json;

namespace LinqDB.Serializers.Utf8Json;
using O=IJsonFormatterResolver;
using Writer=JsonWriter;
using Reader=JsonReader;
public class Serializer:Serializers.Serializer,IJsonFormatter<Serializer>{
    private readonly FormatterResolver Resolver=new();
    
    private readonly O IResolver;
    public Serializer(){
        var formatters=new IJsonFormatter[]{
            this,

            Formatters.Others.Object.Instance,
            ////Formatters.Others.Delegate.Instance,

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
            Formatters.CSharpArgumentInfo.Instance,

            Formatters.Reflection.Type.Instance,
            Formatters.Reflection.Member.Instance,
            Formatters.Reflection.Constructor.Instance,
            Formatters.Reflection.Method.Instance,
            Formatters.Reflection.Property.Instance,
            Formatters.Reflection.Event.Instance,
            Formatters.Reflection.Field.Instance,

            Formatters.Enumerables.IEnumerable.Instance,
            Formatters.Sets.IEnumerable.Instance,
        };
        var resovers=new[]{
            this.Resolver,
            global::Utf8Json.Resolvers.BuiltinResolver.Instance,//int,byte,double[],List<>,よく使う型
            global::Utf8Json.Resolvers.DynamicGenericResolver.Instance,//配列、主にジェネリックコレクション
            global::Utf8Json.Resolvers.EnumResolver.Default,
            global::Utf8Json.Resolvers.AttributeFormatterResolver.Instance,
            //global::Utf8Json.Resolvers.StandardResolver..,
            //global::Utf8Json.Resolvers.StandardResolver.AllowPrivate,//privateメンバーをシリアライズ。これを使うとDisplayClassのthisのようなシリアライズしたくないオブジェクトも対象になる
            //global::Utf8Json.Resolvers.DynamicObjectResolver.AllowPrivate,//.Default,//Anonymous
        };
        this.IResolver=global::Utf8Json.Resolvers.CompositeResolver.Create(
            formatters,
            resovers
        );
    }
    private void Clear(){
        this.ProtectedClear();
        this.Resolver.Clear();
    }
    public override byte[] Serialize<T>(T value){
        this.Clear();
        
        return JsonSerializer.Serialize(value,this.IResolver);
    }
    public override void Serialize<T>(Stream stream,T value){
        this.Clear();
        JsonSerializer.Serialize(stream,value,this.IResolver);
    }
    public override T Deserialize<T>(byte[] bytes){
        this.Clear();
        return JsonSerializer.Deserialize<T>(bytes,this.IResolver);
    }
    public override T Deserialize<T>(Stream stream){
        this.Clear();
        return JsonSerializer.Deserialize<T>(stream,this.IResolver);
    }

    public void Serialize(ref Writer writer,Serializer value,O formatterResolver){
        throw new System.NotImplementedException();
    }
    public Serializer Deserialize(ref Reader reader,O formatterResolver){
        throw new System.NotImplementedException();
    }
}