//using System;
using MemoryPack;
using Expressions = System.Linq.Expressions;
using System.Reflection;
using static LinqDB.Reflection.Common;
using System.IO;
using LinqDB.Helpers;
using LinqDB.Serializers.MemoryPack.Formatters;
using Index=LinqDB.Serializers.MemoryPack.Formatters.Index;
using Object=LinqDB.Serializers.MemoryPack.Formatters.Object;
using Type=LinqDB.Serializers.MemoryPack.Formatters.Type;
namespace LinqDB.Serializers.MemoryPack;
public class Serializer:Serializers.Serializer,System.IServiceProvider{
    //public static readonly Serializer Instance=new();
    public static readonly MethodInfo Register=M(()=>MemoryPackFormatterProvider.Register(Object.Instance));
    public object? GetService(System.Type serviceType){
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
    //public readonly Dictionary<System.Type,object> DictionaryTypeFormatter = new();
    private static readonly object[] objects1 = new object[1];
    internal static void RegisterAnonymousDisplay(System.Type Type) {
        if(Type.IsDisplay()){
            //if(DisplayClass.DictionarySerialize.ContainsKey(Type)) return;
            var FormatterType = typeof(DisplayClass<>).MakeGenericType(Type);
            var Instance=FormatterType.GetField("Instance")!;
            objects1[0]=Instance.GetValue(null)!;// System.Activator.CreateInstance(FormatterType)!;
            var Register = Serializer.Register.MakeGenericMethod(Type);
            Register.Invoke(null,objects1);
            //Register.Invoke(null,Array.Empty<object>());
        }else if(Type.IsGenericType) {
            if(Type.IsAnonymous()) {
                var FormatterType = typeof(Anonymous<>).MakeGenericType(Type);
                var Instance=FormatterType.GetField("Instance")!;
                objects1[0]=Instance.GetValue(null)!;// System.Activator.CreateInstance(FormatterType)!;
                var Register = Serializer.Register.MakeGenericMethod(Type);
                Register.Invoke(null,objects1);
                //Register.Invoke(null,Array.Empty<object>());
            }
            foreach(var GenericArgument in Type.GetGenericArguments()) RegisterAnonymousDisplay(GenericArgument);
        }
    }
    private void Clear(){
         this.ProtectedClear();
        //this.ListParameter.Clear();
        //this.Dictionary_LabelTarget_int.Clear();
        //this.LabelTargets.Clear();
        //this.DictionaryTypeIndex.Clear();
        //this.Types.Clear();
        //this.TypeMembers.Clear();
        //this.TypeConstructors.Clear();
        //this.TypeMethods.Clear();
        //this.TypeFields.Clear();
        //this.TypeProperties.Clear();
        //this.TypeEvents.Clear();
    }
    public byte[] Serialize<T>(T? value){
        this.Clear();
        if(value is not null) RegisterAnonymousDisplay(value.GetType());
        var Type=typeof(T);
        if(typeof(Expressions.LambdaExpression).IsAssignableFrom(Type)){
            var FormatterType = typeof(ExpressionT<>).MakeGenericType(Type);
            var Instance=FormatterType.GetField("Instance")!;
            objects1[0]=Instance.GetValue(null)!;// System.Activator.CreateInstance(FormatterType)!;
            var Register = Serializer.Register.MakeGenericMethod(Type);
            Register.Invoke(null,objects1);
        }
        return MemoryPackSerializer.Serialize(value,this.Options);
    }
    public void Serialize<T>(Stream stream,T? value){
        this.Clear();
        if(value is not null) RegisterAnonymousDisplay(value.GetType());
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