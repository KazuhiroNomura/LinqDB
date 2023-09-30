using System.IO;


using Utf8Json;



namespace LinqDB.Serializers.Utf8Json;
using Formatters;
using Formatters.Others;
using LinqDB.Serializers.Utf8Json.Formatters.Reflection;
public class Serializer:Serializers.Serializer,IJsonFormatter<Serializer>{
    private readonly FormatterResolver Resolver=new();
    private readonly IJsonFormatterResolver IResolver;



    public Serializer(){
        var Formatters=new IJsonFormatter[]{

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
            SymbolDocumentInfo.Instance,
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
            Delegate.Instance,

            CSharpArgumentInfo.Instance,
            this
        };
        var Resolvers=new[]{
            this.Resolver,
            global::Utf8Json.Resolvers.BuiltinResolver.Instance,//int,byte,double[],List<>,よく使う型
            global::Utf8Json.Resolvers.DynamicGenericResolver.Instance,//主にジェネリックコレクション
            global::Utf8Json.Resolvers.EnumResolver.Default,
            global::Utf8Json.Resolvers.AttributeFormatterResolver.Instance,
            global::Utf8Json.Resolvers.DynamicObjectResolver.AllowPrivate,//.Default,//Anonymous
        };
        this.IResolver=global::Utf8Json.Resolvers.CompositeResolver.Create(
            Formatters,
            Resolvers
        );
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    private void Clear(){
        this.ProtectedClear();
        this.Resolver.Clear();
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