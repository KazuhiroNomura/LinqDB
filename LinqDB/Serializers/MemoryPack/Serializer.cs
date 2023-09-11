//using System;
using System.Collections.Generic;
using MemoryPack;
using Expressions = System.Linq.Expressions;
using System.Reflection;
using static LinqDB.Reflection.Common;
using System.IO;
using LinqDB.Helpers;
using LinqDB.Serializers.MemoryPack.Formatters;
namespace LinqDB.Serializers.MemoryPack;
public class Serializer:Serializers.Serializer{
    public static readonly Serializer Instance=new();
    public static readonly MethodInfo Register=M(()=>MemoryPackFormatterProvider.Register(new Anonymous<int>()));
    private Serializer(){
        MemoryPackFormatterProvider.Register(Object.Instance);
        MemoryPackFormatterProvider.Register(Expression.Instance);
        MemoryPackFormatterProvider.Register(Binary.Instance);
        MemoryPackFormatterProvider.Register(Block.Instance);
        MemoryPackFormatterProvider.Register(Conditional.Instance);
        MemoryPackFormatterProvider.Register(Constant.Instance);
        MemoryPackFormatterProvider.Register(Default.Instance);
        MemoryPackFormatterProvider.Register(Goto.Instance);
        MemoryPackFormatterProvider.Register(Index.Instance);
        MemoryPackFormatterProvider.Register(Invocation.Instance);
        MemoryPackFormatterProvider.Register(Label.Instance);
        MemoryPackFormatterProvider.Register(Lambda.Instance);
        MemoryPackFormatterProvider.Register(ListInit.Instance);
        MemoryPackFormatterProvider.Register(Loop.Instance);
        MemoryPackFormatterProvider.Register(MemberAccess.Instance);
        MemoryPackFormatterProvider.Register(MethodCall.Instance);
        MemoryPackFormatterProvider.Register(New.Instance);
        MemoryPackFormatterProvider.Register(NewArray.Instance);
        MemoryPackFormatterProvider.Register(Parameter.Instance);
        MemoryPackFormatterProvider.Register(Switch.Instance);
        MemoryPackFormatterProvider.Register(Try.Instance);
        MemoryPackFormatterProvider.Register(TypeBinary.Instance);
        MemoryPackFormatterProvider.Register(Unary.Instance);
        MemoryPackFormatterProvider.Register(SwitchCase.Instance);
        MemoryPackFormatterProvider.Register(CatchBlock.Instance);
        MemoryPackFormatterProvider.Register(ElementInit.Instance);
        MemoryPackFormatterProvider.Register(MemberBinding.Instance);
        MemoryPackFormatterProvider.Register(MemberInit.Instance);
        //MemoryPackFormatterProvider.Register(LabelTarget.Instance);
        MemoryPackFormatterProvider.Register(Type.Instance);
        MemoryPackFormatterProvider.Register(Member.Instance);
        MemoryPackFormatterProvider.Register(Constructor.Instance);
        MemoryPackFormatterProvider.Register(Method.Instance);
        MemoryPackFormatterProvider.Register(Event.Instance);
        MemoryPackFormatterProvider.Register(Property.Instance);
        MemoryPackFormatterProvider.Register(Field.Instance);
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
    private static readonly object[] objects1 = new object[1];
    internal static void RegisterAnonymousDisplay(System.Type Type) {
        if(Type.IsDisplay()){
            if(DisplayClass.DictionarySerialize.ContainsKey(Type)) return;
            var FormatterType = typeof(DisplayClass<>).MakeGenericType(Type);
            var Instance=FormatterType.GetField(nameof(DisplayClass<int>.Instance))!;
            var Register = Serializer.Register.MakeGenericMethod(Type);
            objects1[0]=Instance.GetValue(null)!;// System.Activator.CreateInstance(FormatterType)!;
            Register.Invoke(null,objects1);
            //Register.Invoke(null,Array.Empty<object>());
        }else if(Type.IsGenericType) {
            if(Type.IsAnonymous()) {
                var FormatterType = typeof(Anonymous<>).MakeGenericType(Type);
                var Register = Serializer.Register.MakeGenericMethod(Type);
                var Instance=FormatterType.GetField(nameof(DisplayClass<int>.Instance))!;
                objects1[0]=Instance.GetValue(null)!;// System.Activator.CreateInstance(FormatterType)!;
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
        return MemoryPackSerializer.Serialize(value);
    }
    public void Serialize<T>(Stream stream,T? value){
        this.Clear();
        if(value is not null) RegisterAnonymousDisplay(value.GetType());
        var Task=MemoryPackSerializer.SerializeAsync(stream,value).AsTask();
        Task.Wait();
    }
    public T Deserialize<T>(byte[] bytes){
        this.Clear();
        return MemoryPackSerializer.Deserialize<T>(bytes)!;
    }
    public T Deserialize<T>(Stream stream){
        this.Clear();
        //var e=MemoryPackSerializer.Instance.DeserializeAsync<T>(stream);
        var Task=MemoryPackSerializer.DeserializeAsync<T>(stream).AsTask();
        Task.Wait();
        return Task.Result!;
    }
}