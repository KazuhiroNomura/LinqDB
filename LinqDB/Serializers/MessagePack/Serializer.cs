using System.IO;
using System.Reflection.Emit;
using LinqDB.Helpers;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack;
using O=MessagePackSerializerOptions;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
public class Serializer:Serializers.Serializer,IMessagePackFormatter<Serializer>{//IMessagePackFormatter<Serializer>を継承する理由はFormatterでResolverを経由でSerializer情報を取得するため
    private readonly FormatterResolver Resolver=new();
    
    private readonly O Options;
    public Serializer(){
        var formatters=new IMessagePackFormatter[]{
            this,
            //Formatters.Others.Action.Instance,
            Formatters.Others.Object.Instance,
            //Formatters.Others.Delegate.Instance,

            Formatters.Binary.Instance,
            Formatters.Block.Instance,
            Formatters.CatchBlock.Instance,
            Formatters.Conditional.Instance,
            Formatters.Constant.Instance,
            Formatters.Default.Instance,
            Formatters.DebugInfo.Instance,
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
        var resolvers=new IFormatterResolver[]{
            this.Resolver,
            global::MessagePack.Resolvers.StandardResolverAllowPrivate.Instance,
            global::MessagePack.Resolvers.BuiltinResolver.Instance,
            //global::MessagePack.Resolvers.DynamicGenericResolver.Instance,//GenericEnumerableFormatter
            //global::MessagePack.Resolvers.DynamicObjectResolverAllowPrivate.Instance,//MessagePackObjectAttribute
        };
        this.Options=O.Standard.WithResolver(
            global::MessagePack.Resolvers.CompositeResolver.Create(
                formatters,
                resolvers
            )
        );
    }
    
    private void Clear(){
        this.ProtectedClear();
        this.Resolver.Clear();
    }
    public override byte[] Serialize<T>(T value){
        this.Clear();
        
        return MessagePackSerializer.Serialize(value,this.Options);
    }
    public override void Serialize<T>(Stream stream,T value){
        this.Clear();
        MessagePackSerializer.Serialize(stream,value,this.Options);
    }
    public override T Deserialize<T>(byte[] bytes){
        this.Clear();
        return MessagePackSerializer.Deserialize<T>(bytes,this.Options);
    }
    public override T Deserialize<T>(Stream stream){
        this.Clear();
        return MessagePackSerializer.Deserialize<T>(stream,this.Options);
    }
    public void Serialize(ref Writer writer,Serializer value,O options){
        throw new System.NotImplementedException();
    }
    public Serializer Deserialize(ref Reader reader,O options){
        throw new System.NotImplementedException();
    }
}