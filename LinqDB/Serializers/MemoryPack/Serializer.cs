using System.IO;

using LinqDB.Helpers;
using LinqDB.Sets;
using MemoryPack;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack;
using Formatters;
using Formatters.Others;
using LinqDB.Serializers.MemoryPack.Formatters.Reflection;
public class Serializer:Serializers.Serializer,System.IServiceProvider{
    public static readonly System.Reflection.MethodInfo Register=Reflection.Common.M(()=>MemoryPackFormatterProvider.Register(Object.Instance));
    public object GetService(System.Type serviceType){
        throw new System.NotImplementedException();
    }
    private readonly MemoryPackSerializerOptions Options;
    public Serializer(){
        this.Options=new(){ServiceProvider=this};

        MemoryPackFormatterProvider.Register(Object.Instance);

        MemoryPackFormatterProvider.Register(Binary.Instance);
        MemoryPackFormatterProvider.Register(Block.Instance);
        MemoryPackFormatterProvider.Register(CatchBlock.Instance);
        MemoryPackFormatterProvider.Register(Conditional.Instance);
        MemoryPackFormatterProvider.Register(Constant.Instance);
        MemoryPackFormatterProvider.Register(DebugInfo.Instance);
        MemoryPackFormatterProvider.Register(Default.Instance);
        MemoryPackFormatterProvider.Register(Dynamic.Instance);
        MemoryPackFormatterProvider.Register(ElementInit.Instance);
        MemoryPackFormatterProvider.Register(Expression.Instance);
        MemoryPackFormatterProvider.Register(Goto.Instance);
        MemoryPackFormatterProvider.Register(Index.Instance);
        MemoryPackFormatterProvider.Register(Invocation.Instance);
        MemoryPackFormatterProvider.Register(Label.Instance);
        MemoryPackFormatterProvider.Register(LabelTarget.Instance);
        MemoryPackFormatterProvider.Register(Lambda.Instance);
        MemoryPackFormatterProvider.Register(ListInit.Instance);
        MemoryPackFormatterProvider.Register(Loop.Instance);
        MemoryPackFormatterProvider.Register(MemberAccess.Instance);
        MemoryPackFormatterProvider.Register(MemberBinding.Instance);
        MemoryPackFormatterProvider.Register(MemberInit.Instance);
        MemoryPackFormatterProvider.Register(MethodCall.Instance);
        MemoryPackFormatterProvider.Register(New.Instance);
        MemoryPackFormatterProvider.Register(NewArray.Instance);
        MemoryPackFormatterProvider.Register(Parameter.Instance);
        MemoryPackFormatterProvider.Register(Switch.Instance);
        MemoryPackFormatterProvider.Register(SwitchCase.Instance);
        MemoryPackFormatterProvider.Register(Try.Instance);
        MemoryPackFormatterProvider.Register(TypeBinary.Instance);
        MemoryPackFormatterProvider.Register(Unary.Instance);
        
        MemoryPackFormatterProvider.Register(Type.Instance);
        MemoryPackFormatterProvider.Register(Member.Instance);
        MemoryPackFormatterProvider.Register(Constructor.Instance);
        MemoryPackFormatterProvider.Register(Method.Instance);
        MemoryPackFormatterProvider.Register(Event.Instance);
        MemoryPackFormatterProvider.Register(Property.Instance);
        MemoryPackFormatterProvider.Register(Field.Instance);
        MemoryPackFormatterProvider.Register(Delegate.Instance);

        MemoryPackFormatterProvider.Register(CSharpArgumentInfo.Instance);

    }
    private readonly object[] objects1 = new object[1];
    internal void RegisterAnonymousDisplay(System.Type Type) {
        if(Type.IsDisplay()){
            //if(DisplayClass.DictionarySerialize.ContainsKey(Type)) return;
            var FormatterType = typeof(DisplayClass<>).MakeGenericType(Type);
            var Instance=FormatterType.GetField("Instance")!;
            var objects1=this.objects1;
            objects1[0]=Instance.GetValue(null)!;// System.Activator.CreateInstance(FormatterType)!;
            var Register = Serializer.Register.MakeGenericMethod(Type);
            Register.Invoke(null,objects1);
        }else if(Type.IsGenericType) {
            if(Type.IsAnonymous()) {
                var FormatterType = typeof(Anonymous<>).MakeGenericType(Type);
                var Instance=FormatterType.GetField("Instance")!;
                var objects1=this.objects1;
                objects1[0]=Instance.GetValue(null)!;// System.Activator.CreateInstance(FormatterType)!;
                var Register = Serializer.Register.MakeGenericMethod(Type);
                Register.Invoke(null,objects1);
            } else{
                var 検索されるType=Type;
                while(typeof(object)!=検索されるType){
                    if(検索されるType.IsGenericType){
                        if(GetValue(typeof(Set<,,>),typeof(Formatters.Sets.Set<,,>)))break;
                        if(GetValue(typeof(Set<, >),typeof(Formatters.Sets.Set<, >)))break;
                        if(GetValue(typeof(Set<  >),typeof(Formatters.Sets.Set<  >)))break;
                    }
                    検索されるType=検索されるType.BaseType!;
                    bool GetValue(System.Type 検索したいキーTypeDifinition,System.Type FormatterTypeDifinition){
                        if(検索されるType.GetGenericTypeDefinition()==検索したいキーTypeDifinition){
                            var FormatterType=FormatterTypeDifinition.MakeGenericType(Type.GetGenericArguments());
                            var Instance=FormatterType.GetField("Instance")!;
                            var objects1=this.objects1;
                            objects1[0]=Instance.GetValue(null)!;
                            var Register=Serializer.Register.MakeGenericMethod(Type);
                            Register.Invoke(null,objects1);
                            return true;
                        }
                        return false;
                    }
                }
            }
            foreach(var GenericArgument in Type.GetGenericArguments()) this.RegisterAnonymousDisplay(GenericArgument);
        }
    }
    private void Clear(){
         this.ProtectedClear();
         
    }
    public byte[] Serialize<T>(T? value){
        this.Clear();
        this.RegisterAnonymousDisplay(typeof(T));
        var Type=typeof(T);
        if(typeof(Expressions.LambdaExpression).IsAssignableFrom(Type)) {
            //この処理はExpression,objectでは使われない。Expression<T>はここでのみ必要。
            var FormatterType = typeof(ExpressionT<>).MakeGenericType(Type);
            var Instance = FormatterType.GetField("Instance")!;
            var objects1 = this.objects1;
            objects1[0]=Instance.GetValue(null)!;// System.Activator.CreateInstance(FormatterType)!;
            var Register = Serializer.Register.MakeGenericMethod(Type);
            Register.Invoke(null,objects1);
        }
        return MemoryPackSerializer.Serialize(value,this.Options);
    }
    public void Serialize<T>(Stream stream,T? value){
        this.Clear();
        this.RegisterAnonymousDisplay(typeof(T));
        var Task=MemoryPackSerializer.SerializeAsync(stream,value,this.Options).AsTask();
        Task.Wait();
    }
    public T Deserialize<T>(byte[] bytes){
        this.Clear();
        return MemoryPackSerializer.Deserialize<T>(bytes,this.Options)!;
    }
    public T Deserialize<T>(Stream stream){
        this.Clear();
        //var e=MemoryPackSerializer.DeserializeAsync<T>(stream);
        var Task=MemoryPackSerializer.DeserializeAsync<T>(stream,this.Options).AsTask();
        Task.Wait();
        return Task.Result!;
    }
}