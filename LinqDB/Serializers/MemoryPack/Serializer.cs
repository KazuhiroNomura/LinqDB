using System;
using System.IO;
using Sets=LinqDB.Sets;
using Generic=System.Collections.Generic;
using MemoryPack;
using MemoryPack.Formatters;
using Microsoft.Build.Utilities;
namespace LinqDB.Serializers.MemoryPack;
using O=MemoryPackSerializerOptions;


public class Serializer:Serializers.Serializer,System.IServiceProvider{
    public static readonly System.Reflection.MethodInfo Register=Reflection.Common.M(()=>MemoryPackFormatterProvider.Register(Formatters.Others.Object.Instance));
    public object GetService(System.Type serviceType)=>throw new System.NotImplementedException();
    private readonly O Options;
    static Serializer(){
        //MemoryPackFormatterProvider.Register(CharArrayFormatter.Instance);


        //MemoryPackFormatterProvider.Register(Formatters.Others.Action.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Others.Object.Instance);
        //MemoryPackFormatterProvider.Register(Formatters.Others.Delegate.Instance);

        MemoryPackFormatterProvider.Register(Formatters.Binary.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Block.Instance);
        MemoryPackFormatterProvider.Register(Formatters.CatchBlock.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Conditional.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Constant.Instance);
        MemoryPackFormatterProvider.Register(Formatters.DebugInfo.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Default.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Dynamic.Instance);
        MemoryPackFormatterProvider.Register(Formatters.ElementInit.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Expression.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Goto.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Index.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Invocation.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Label.Instance);
        MemoryPackFormatterProvider.Register(Formatters.LabelTarget.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Lambda.Instance);
        MemoryPackFormatterProvider.Register(Formatters.ListInit.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Loop.Instance);
        MemoryPackFormatterProvider.Register(Formatters.MemberAccess.Instance);
        MemoryPackFormatterProvider.Register(Formatters.MemberBinding.Instance);
        MemoryPackFormatterProvider.Register(Formatters.MemberAssignment.Instance);
        MemoryPackFormatterProvider.Register(Formatters.MemberListBinding.Instance);
        MemoryPackFormatterProvider.Register(Formatters.MemberMemberBinding.Instance);
        MemoryPackFormatterProvider.Register(Formatters.MemberInit.Instance);
        MemoryPackFormatterProvider.Register(Formatters.MethodCall.Instance);
        MemoryPackFormatterProvider.Register(Formatters.New.Instance);
        MemoryPackFormatterProvider.Register(Formatters.NewArray.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Parameter.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Switch.Instance);
        MemoryPackFormatterProvider.Register(Formatters.SwitchCase.Instance);
        MemoryPackFormatterProvider.Register(Formatters.SymbolDocumentInfo.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Try.Instance);
        MemoryPackFormatterProvider.Register(Formatters.TypeBinary.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Unary.Instance);
        MemoryPackFormatterProvider.Register(Formatters.CSharpArgumentInfo.Instance);

        MemoryPackFormatterProvider.Register(Formatters.Reflection.Type.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Reflection.Member.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Reflection.Constructor.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Reflection.Method.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Reflection.Event.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Reflection.Property.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Reflection.Field.Instance);

        MemoryPackFormatterProvider.Register(Formatters.Enumerables.IEnumerable.Instance);
        MemoryPackFormatterProvider.Register(Formatters.Sets.IEnumerable.Instance);
        //MemoryPackFormatterProvider.Register(new ArrayFormatter<char>());
    }
    public Serializer(){
        this.Options=new(){ServiceProvider=this,StringEncoding = StringEncoding.Utf16};
    }
    
    
    
    
    
    
    
    
    
    
    

    private void Clear(){
         this.ProtectedClear();

    }
    public override byte[] Serialize<T>(T value){
        this.Clear();
        FormatterResolver.GetFormatterDynamic<T>();
        return MemoryPackSerializer.Serialize(value,this.Options);
    }
    public override void Serialize<T>(Stream stream,T value){
        this.Clear();
        FormatterResolver.GetFormatterDynamic<T>();
        var Task=MemoryPackSerializer.SerializeAsync(stream,value,this.Options).AsTask();
        Task.Wait();
    }
    public override T Deserialize<T>(byte[] bytes){
        this.Clear();
        FormatterResolver.GetFormatterDynamic<T>();
        return MemoryPackSerializer.Deserialize<T>(bytes,this.Options)!;
    }
    public override T Deserialize<T>(Stream stream){
        this.Clear();
        FormatterResolver.GetFormatterDynamic<T>();
        var Task=MemoryPackSerializer.DeserializeAsync<T>(stream,this.Options).AsTask();
        Task.Wait();
        return Task.Result!;
    }
}