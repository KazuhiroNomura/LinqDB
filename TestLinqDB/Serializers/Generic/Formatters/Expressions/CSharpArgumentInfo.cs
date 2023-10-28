using RuntimeBinder = Microsoft.CSharp.RuntimeBinder;

namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class CSharpArgumentInfo<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected CSharpArgumentInfo():base(new AssertDefinition(new TSerializer())){}
    [Fact]
    public void Serialize(){
        this.AssertEqual(RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None,null));
    }
}
