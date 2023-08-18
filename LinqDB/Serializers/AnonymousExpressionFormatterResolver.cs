using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LinqDB.Helpers;
using LinqDB.Serializers.Formatters;
using LinqDB.Sets;
using MessagePack;
using MessagePack.Formatters;
//using MessagePack.Formatters;

using Utf8Json;
//using Utf8Json.Formatters;
namespace LinqDB.Serializers;
public sealed class AnonymousExpressionJsonFormatterResolver:IJsonFormatterResolver{
        private static readonly Dictionary<Type, Type> FormatterMap = new Dictionary<Type, Type>()
        {
              { typeof(List<>), typeof(Utf8Json.Formatters.ListFormatter<>) },
              { typeof(LinkedList<>),typeof(Utf8Json.Formatters.LinkedListFormatter<>) },
              { typeof(Stack<>),typeof(Utf8Json.Formatters.StackFormatter<>) },
              { typeof(HashSet<>),typeof(Utf8Json.Formatters.HashSetFormatter<>) },
              { typeof(ReadOnlyCollection<>),typeof(Utf8Json.Formatters.ReadOnlyCollectionFormatter<>) },
              { typeof(IEnumerable<>),typeof(Utf8Json.Formatters.InterfaceEnumerableFormatter<>) },
              { typeof(Dictionary<,>),typeof(Utf8Json.Formatters.DictionaryFormatter<,>) },
              { typeof(IDictionary<,>),typeof(Utf8Json.Formatters.InterfaceDictionaryFormatter<,>) },
              { typeof(SortedDictionary<,>),typeof(Utf8Json.Formatters.SortedDictionaryFormatter<,>) },
              { typeof(SortedList<,>),typeof(Utf8Json.Formatters.SortedListFormatter<,>) },
              { typeof(ILookup<,>),typeof(Utf8Json.Formatters.InterfaceLookupFormatter<,>) },
              { typeof(IGrouping<,>),typeof(Utf8Json.Formatters.InterfaceGroupingFormatter<,>) },
              { typeof(ObservableCollection<>),typeof(Utf8Json.Formatters.ObservableCollectionFormatter<>) },
              { typeof(ReadOnlyObservableCollection<>),typeof(Utf8Json.Formatters.ReadOnlyObservableCollectionFormatter<>) },
              { typeof(IReadOnlyList<>),typeof(Utf8Json.Formatters.InterfaceReadOnlyListFormatter<>) },
              { typeof(IReadOnlyCollection<>),typeof(Utf8Json.Formatters.InterfaceReadOnlyCollectionFormatter<>) },
              { typeof(System.Collections.Concurrent.ConcurrentBag<>),typeof(Utf8Json.Formatters.ConcurrentBagFormatter<>) },
              { typeof(System.Collections.Concurrent.ConcurrentQueue<>),typeof(Utf8Json.Formatters.ConcurrentQueueFormatter<>) },
              { typeof(System.Collections.Concurrent.ConcurrentStack<>),typeof(Utf8Json.Formatters.ConcurrentStackFormatter<>) },
              { typeof(ReadOnlyDictionary<,>),typeof(Utf8Json.Formatters.ReadOnlyDictionaryFormatter<,>) },
              { typeof(IReadOnlyDictionary<,>),typeof(Utf8Json.Formatters.InterfaceReadOnlyDictionaryFormatter<,>) },
              { typeof(System.Collections.Concurrent.ConcurrentDictionary<,>),typeof(Utf8Json.Formatters.ConcurrentDictionaryFormatter<,>) },
              { typeof(Lazy<>),typeof(Utf8Json.Formatters.LazyFormatter<>) },
        };
    //public static readonly AnonymousExpressionJsonFormatterResolver Instance=new();
    public readonly ExpressionJsonFormatter ExpressionFormatter = new();
    private readonly Dictionary<Type,IJsonFormatter> Dictionary_Type_Formatter = new();
    private readonly Type[] GenericArguments=new Type[1];
    public IJsonFormatter<T> GetFormatter<T>(){
        if(this.Dictionary_Type_Formatter.TryGetValue(typeof(T),out var IJsonFormatter))return (IJsonFormatter<T>)IJsonFormatter;
        //if(typeof(Type).IsAssignableFrom(typeof(T))) return (IJsonFormatter<T>)(IJsonFormatter)Utf8Json.Formatters.TypeFormatter.Default;
        //if(typeof(Type).IsAssignableFrom(typeof(T))) return null!;
        if(typeof(Expression).IsAssignableFrom(typeof(T))){
            if(!typeof(T).IsSubclassOf(typeof(LambdaExpression))) return Return(this.ExpressionFormatter);
            var GenericArguments=this.GenericArguments;
            GenericArguments[0]=typeof(T);
            return Return(Activator.CreateInstance(typeof(ExpressionJsonFormatter<>).MakeGenericType(GenericArguments),this.ExpressionFormatter)!);
            //if(typeof(T).IsSubclassOf(typeof(LambdaExpression))){
            //    return Return(Activator.CreateInstance(typeof(ExpressionJsonFormatter<>).MakeGenericType(typeof(T)),this.ExpressionFormatter)!);
            //} else{
            //    return Return(this.ExpressionFormatter);
            //}
            //return Return(typeof(T).IsSubclassOf(typeof(LambdaExpression))
            //    ?Activator.CreateInstance(typeof(ExpressionJsonFormatter<>).MakeGenericType(typeof(T)),
            //        this.ExpressionFormatter)!
            //    :this.ExpressionFormatter);
        }
        if(
            typeof(MemberBinding).IsAssignableFrom(typeof(T))||
            typeof(T)==typeof(MethodInfo)||
            typeof(T)==typeof(MemberInfo)||
            typeof(T)==typeof(CatchBlock)||
            typeof(T)==typeof(SwitchCase)||
            typeof(T)==typeof(ElementInit))
            return Return(this.ExpressionFormatter);
        //else if(typeof(T)==typeof(ConstantExpression))Formatter=(IJsonFormatter<T>)new ConstantFormatter();
        //else if(typeof(T)==typeof(DefaultExpression ))Formatter=(IJsonFormatter<T>)new DefaultFormatter();
        //else if(typeof(T).IsSubclassOf(typeof(LambdaExpression)))Formatter=(IJsonFormatter<T>)Activator.CreateInstance(typeof(ExpressionFormatter<>).MakeGenericType(typeof(T)),ExpressionFormatter.Instance)!;
        //else if(typeof(T)==typeof(DefaultExpression))
        //    Formatter=(IJsonFormatter<T>)new DefaultFormatter();
        //else if(typeof(T)==typeof(NewArrayExpression))Formatter=(IJsonFormatter<T>)new NewArrayFormatter();
        //else if(typeof(T).IsAnonymous())
        //    Formatter=new AbstractFormatter<T>();
        if(typeof(T).IsAnonymous())return Return(new AnonymousJsonFormatter<T>());
        //if(typeof(ImmutableSet).IsAssignableFrom(typeof(T))){
        //    if(typeof(Set<>)==typeof(T).GetGenericTypeDefinition()){
        //        return Return(typeof(Set<>.JsonFormatter).MakeGenericType(typeof(T).GetGenericArguments()).GetField("Instance").GetValue(null));
        //    }
        //}
        //return Return(new AbstractJsonFormatter<T>());
        //if(!typeof(T).IsValueType&&!typeof(T).IsSealed)return Return(new AbstractJsonFormatter<T>());
        var Formatter=GetFormatter(typeof(T));
        if(Formatter!=null) Return(Formatter);
        return Return(new AbstractJsonFormatter<T>());
        //var type=typeof(T);
        //var GenericTypeDefinition = typeof(T).GetGenericTypeDefinition();
        //var f=GetFormatter(type);
        //if (FormatterMap.TryGetValue(GenericTypeDefinition, out var FormatterType))
        //{
        //    return Return(Activator.CreateInstance(FormatterType.MakeGenericType(GenericTypeDefinition.GetGenericArguments())));
        //}
        ////return Return(new AbstractJsonFormatter<T>());
        //return null!;
        IJsonFormatter<T> Return(object Formatter){
            var result=(IJsonFormatter<T>)Formatter;
            this.Dictionary_Type_Formatter.Add(typeof(T),result);
            return result;
        }
        //else if(typeof(T).IsValueType||typeof(T).IsSealed||typeof(ITuple).IsAssignableFrom(typeof(T)))
        //    return null!;
        //else
        //    return this.Return<T>(this.AbstractFormatter);
    }
    internal static object? GetFormatter(Type t)
    {
        TypeInfo ti = t.GetTypeInfo();

        if (t.IsArray)
        {
            var rank = t.GetArrayRank();
            if (rank == 1)
            {
                if (t.GetElementType() == typeof(byte))
                {
                    // byte[] is also supported in builtin formatter.
                    return ByteArrayFormatter.Instance;
                }

                return Activator.CreateInstance(typeof(ArrayFormatter<>).MakeGenericType(t.GetElementType()!));
            }
            else if (rank == 2)
            {
                return Activator.CreateInstance(typeof(TwoDimensionalArrayFormatter<>).MakeGenericType(t.GetElementType()!));
            }
            else if (rank == 3)
            {
                return Activator.CreateInstance(typeof(ThreeDimensionalArrayFormatter<>).MakeGenericType(t.GetElementType()!));
            }
            else if (rank == 4)
            {
                return Activator.CreateInstance(typeof(FourDimensionalArrayFormatter<>).MakeGenericType(t.GetElementType()!));
            }
            else
            {
                return null; // not supported built-in
            }
        }
        else if (ti.IsGenericType)
        {
            Type genericType = ti.GetGenericTypeDefinition();
            TypeInfo genericTypeInfo = genericType.GetTypeInfo();
            var isNullable = genericTypeInfo.IsNullable();
            Type? nullableElementType = isNullable ? ti.GenericTypeArguments[0] : null;

            if (genericType == typeof(KeyValuePair<,>))
            {
                return CreateInstance(typeof(KeyValuePairFormatter<,>), ti.GenericTypeArguments);
            }

            // Tuple
            else if (ti.FullName?.StartsWith("System.Tuple") is true)
            {
                Type? tupleFormatterType = null;
                switch (ti.GenericTypeArguments.Length)
                {
                    case 1:
                        tupleFormatterType = typeof(TupleFormatter<>);
                        break;
                    case 2:
                        tupleFormatterType = typeof(TupleFormatter<,>);
                        break;
                    case 3:
                        tupleFormatterType = typeof(TupleFormatter<,,>);
                        break;
                    case 4:
                        tupleFormatterType = typeof(TupleFormatter<,,,>);
                        break;
                    case 5:
                        tupleFormatterType = typeof(TupleFormatter<,,,,>);
                        break;
                    case 6:
                        tupleFormatterType = typeof(TupleFormatter<,,,,,>);
                        break;
                    case 7:
                        tupleFormatterType = typeof(TupleFormatter<,,,,,,>);
                        break;
                    case 8:
                        tupleFormatterType = typeof(TupleFormatter<,,,,,,,>);
                        break;
                    default:
                        throw new MessagePackSerializationException("Unsupported arity for Tuple generic type: " + ti.Name);
                }

                return CreateInstance(tupleFormatterType, ti.GenericTypeArguments);
            }

            // ValueTuple
            else if (ti.FullName?.StartsWith("System.ValueTuple") is true)
            {
                Type? tupleFormatterType = null;
                switch (ti.GenericTypeArguments.Length)
                {
                    case 1:
                        tupleFormatterType = typeof(ValueTupleFormatter<>);
                        break;
                    case 2:
                        tupleFormatterType = typeof(ValueTupleFormatter<,>);
                        break;
                    case 3:
                        tupleFormatterType = typeof(ValueTupleFormatter<,,>);
                        break;
                    case 4:
                        tupleFormatterType = typeof(ValueTupleFormatter<,,,>);
                        break;
                    case 5:
                        tupleFormatterType = typeof(ValueTupleFormatter<,,,,>);
                        break;
                    case 6:
                        tupleFormatterType = typeof(ValueTupleFormatter<,,,,,>);
                        break;
                    case 7:
                        tupleFormatterType = typeof(ValueTupleFormatter<,,,,,,>);
                        break;
                    case 8:
                        tupleFormatterType = typeof(ValueTupleFormatter<,,,,,,,>);
                        break;
                    default:
                        throw new MessagePackSerializationException("Unsupported arity for ValueTuple generic type: " + ti.Name);
                }

                return CreateInstance(tupleFormatterType, ti.GenericTypeArguments);
            }

            // ArraySegment
            else if (genericType == typeof(ArraySegment<>))
            {
                if (ti.GenericTypeArguments[0] == typeof(byte))
                {
                    return ByteArraySegmentFormatter.Instance;
                }
                else
                {
                    return CreateInstance(typeof(ArraySegmentFormatter<>), ti.GenericTypeArguments);
                }
            }

            // Memory
            else if (genericType == typeof(Memory<>))
            {
                if (ti.GenericTypeArguments[0] == typeof(byte))
                {
                    return ByteMemoryFormatter.Instance;
                }
                else
                {
                    return CreateInstance(typeof(MemoryFormatter<>), ti.GenericTypeArguments);
                }
            }

            // ReadOnlyMemory
            else if (genericType == typeof(ReadOnlyMemory<>))
            {
                if (ti.GenericTypeArguments[0] == typeof(byte))
                {
                    return ByteReadOnlyMemoryFormatter.Instance;
                }
                else
                {
                    return CreateInstance(typeof(ReadOnlyMemoryFormatter<>), ti.GenericTypeArguments);
                }
            }

            // ReadOnlySequence
            else if (genericType == typeof(ReadOnlySequence<>))
            {
                if (ti.GenericTypeArguments[0] == typeof(byte))
                {
                    return ByteReadOnlySequenceFormatter.Instance;
                }
                else
                {
                    return CreateInstance(typeof(ReadOnlySequenceFormatter<>), ti.GenericTypeArguments);
                }
            }

            // Standard Nullable
            else if (isNullable)
            {
                return CreateInstance(typeof(NullableFormatter<>), new[] { nullableElementType! });
            }

            // Mapped formatter
            else
            {
                if (FormatterMap.TryGetValue(genericType, out Type? formatterType))
                {
                    return CreateInstance(formatterType, ti.GenericTypeArguments);
                }
            }
        }
        else if (ti.IsEnum)
        {
            return CreateInstance(typeof(GenericEnumFormatter<>), new[] { t });
        }
        else
        {
            // NonGeneric Collection
            if (t == typeof(IEnumerable))
            {
                return NonGenericInterfaceEnumerableFormatter.Instance;
            }
            else if (t == typeof(ICollection))
            {
                return NonGenericInterfaceCollectionFormatter.Instance;
            }
            else if (t == typeof(IList))
            {
                return NonGenericInterfaceListFormatter.Instance;
            }
            else if (t == typeof(IDictionary))
            {
                return NonGenericInterfaceDictionaryFormatter.Instance;
            }

            if (typeof(IList).GetTypeInfo().IsAssignableFrom(ti) && ti.DeclaredConstructors.Any(x => x.GetParameters().Length == 0))
            {
                return Activator.CreateInstance(typeof(NonGenericListFormatter<>).MakeGenericType(t));
            }
            else if (typeof(IDictionary).GetTypeInfo().IsAssignableFrom(ti) && ti.DeclaredConstructors.Any(x => x.GetParameters().Length == 0))
            {
                return Activator.CreateInstance(typeof(NonGenericDictionaryFormatter<>).MakeGenericType(t));
            }
        }

        // check inherited types(e.g. Foo : ICollection<>, Bar<T> : ICollection<T>)
        {
            // generic dictionary
            var dictionaryDef = ti.ImplementedInterfaces.FirstOrDefault(x => x.GetTypeInfo().IsConstructedGenericType() && x.GetGenericTypeDefinition() == typeof(IDictionary<,>));
            if (dictionaryDef != null && ti.DeclaredConstructors.Any(x => x.GetParameters().Length == 0))
            {
                Type keyType = dictionaryDef.GenericTypeArguments[0];
                Type valueType = dictionaryDef.GenericTypeArguments[1];
                return CreateInstance(typeof(GenericDictionaryFormatter<,,>), new[] { keyType, valueType, t });
            }

            // generic dictionary with collection ctor
            var dictionaryInterfaceDef = ti.ImplementedInterfaces.FirstOrDefault(x => x.GetTypeInfo().IsConstructedGenericType() &&
                (x.GetGenericTypeDefinition() == typeof(IDictionary<,>) || x.GetGenericTypeDefinition() == typeof(IReadOnlyDictionary<,>)));
            if (dictionaryInterfaceDef != null)
            {
                Type keyType = dictionaryInterfaceDef.GenericTypeArguments[0];
                Type valueType = dictionaryInterfaceDef.GenericTypeArguments[1];
                Type[] allowedParameterTypes = new Type[]
                {
                    typeof(IDictionary<,>).MakeGenericType(keyType, valueType),
                    typeof(IReadOnlyDictionary<,>).MakeGenericType(keyType, valueType),
                    typeof(IEnumerable<>).MakeGenericType(typeof(KeyValuePair<,>).MakeGenericType(keyType, valueType)),
                };
                foreach (var constructor in ti.DeclaredConstructors)
                {
                    ParameterInfo[] parameters = constructor.GetParameters();
                    if (parameters.Length == 1 &&
                        allowedParameterTypes.Any(allowedType => parameters[0].ParameterType.IsAssignableFrom(allowedType)))
                    {
                        return CreateInstance(typeof(GenericReadOnlyDictionaryFormatter<,,>), new[] { keyType, valueType, t });
                    }
                }
            }

            // generic collection
            var collectionDef = ti.ImplementedInterfaces.FirstOrDefault(x => x.GetTypeInfo().IsConstructedGenericType() && x.GetGenericTypeDefinition() == typeof(ICollection<>));
            if (collectionDef != null && ti.DeclaredConstructors.Any(x => x.GetParameters().Length == 0))
            {
                Type elemType = collectionDef.GenericTypeArguments[0];
                return CreateInstance(typeof(GenericCollectionFormatter<,>), new[] { elemType, t });
            }

            // generic IEnumerable collection
            // looking for combination of IEnumerable<T> and constructor that takes
            // enumeration of the same type
            foreach (var enumerableCollectionDef in ti.ImplementedInterfaces.Where(x => x.GetTypeInfo().IsConstructedGenericType() && x.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
            {
                Type elemType = enumerableCollectionDef.GenericTypeArguments[0];
                Type paramInterface = typeof(IEnumerable<>).MakeGenericType(elemType);
                foreach (var constructor in ti.DeclaredConstructors)
                {
                    var parameters = constructor.GetParameters();
                    if (parameters.Length == 1 && parameters[0].ParameterType.IsAssignableFrom(paramInterface))
                    {
                        return CreateInstance(typeof(Utf8Json.Formatters.GenericCollectionFormatter<,>), new[] { elemType, t });
                        //return CreateInstance(typeof(Utf8Json.Formatters.GenericCollectionFormatter<> GenericEnumerableFormatter<,>), new[] { elemType, t });
                    }
                }
            }
        }

        return null;
    }

    private static object? CreateInstance(Type genericType, Type[] genericTypeArguments, params object?[] arguments)
    {
        return Activator.CreateInstance(genericType.MakeGenericType(genericTypeArguments), arguments);
    }
    public void Clear() {
        this.ExpressionFormatter.Clear();
        //this.Dictionary_Type_Formatter.Clear();
    }
}
public sealed class AnonymousExpressionMessagePackFormatterResolver:IFormatterResolver {
    //public static readonly AnonymousExpressionMessagePackFormatterResolver Instance=new();
    //public static readonly IJsonFormatterResolver Instance=new Resolver();
    //private readonly AbstractMessagePackFormatter AbstractFormatter= new();
    private readonly ExpressionMessagePackFormatter ExpressionFormatter=new();
    private readonly Dictionary<Type,IMessagePackFormatter> Dictionary_Type_Formatter = new();
    private readonly Type[] GenericArguments=new Type[1];
    public IMessagePackFormatter<T> GetFormatter<T>() {
        //if(typeof(T).IsSerializable) return null!;
        if(this.Dictionary_Type_Formatter.TryGetValue(typeof(T),out var IMessagePackFormatter))return (IMessagePackFormatter<T>)IMessagePackFormatter;
        //if(typeof(Type).IsAssignableFrom(typeof(T))){
        //    var GenericArguments=this.GenericArguments;
        //    GenericArguments[0]=typeof(T);
        //    return (IMessagePackFormatter<T>)typeof(TypeFormatter<>).MakeGenericType(GenericArguments).GetField("Instance").GetValue(null);
        //}
        if(typeof(Expression).IsAssignableFrom(typeof(T))){
            //if(typeof(LambdaExpression).IsAssignableFrom(typeof(T)))
            //    Formatter=(IMessagePackFormatter<T>)Activator.CreateInstance(typeof(ExpressionFormatter<>).MakeGenericType(typeof(T)),ExpressionFormatter.Instance)!;
            if(!typeof(T).IsSubclassOf(typeof(LambdaExpression))) return Return(this.ExpressionFormatter);
            var GenericArguments=this.GenericArguments;
            GenericArguments[0]=typeof(T);
            return Return(Activator.CreateInstance(typeof(ExpressionMessagePackFormatter<>).MakeGenericType(GenericArguments),this.ExpressionFormatter)!);
        }
        if(typeof(Expression).IsAssignableFrom(typeof(T))){
            return Return(typeof(T).IsSubclassOf(typeof(LambdaExpression))?Activator.CreateInstance(typeof(ExpressionMessagePackFormatter<>).MakeGenericType(typeof(T)),this.ExpressionFormatter)!:this.ExpressionFormatter);
        }
        if(
            typeof(MemberBinding).IsAssignableFrom(typeof(T))||
            typeof(T)==typeof(MethodInfo)||
            typeof(T)==typeof(MemberInfo)||
            typeof(T)==typeof(CatchBlock)||
            typeof(T)==typeof(SwitchCase)||
            typeof(T)==typeof(ElementInit))
            return Return(this.ExpressionFormatter);
        if(typeof(T).IsAnonymous())return Return(new AnonymousMessagePackFormatter<T>());
        if(!typeof(T).IsValueType&&!typeof(T).IsSealed)return Return(new AbstractMessagePackFormatter<T>());
        return null!;
        //else if(typeof(T).IsValueType||typeof(T).IsSealed||typeof(ITuple).IsAssignableFrom(typeof(T)))
        //    return null!;
        //else
        //    Formatter=this.AbstractFormatter;
        //else if(global::MessagePack.Resolvers.StandardResolver.Instance.GetFormatterDynamic(typeof(T))is not null)
        //    return null!;
        //else if(!typeof(T).IsValueType&&!typeof(T).IsSealed)
          //  Formatter=new AbstractFormatter<T>();
        //Formatter=global::MessagePack.Resolvers.StandardResolverAllowPrivate.Instance.GetFormatter<T>();// new AbstractFormatter<T>();
        //else//  throw new NotSupportedException(typeof(T).FullName);
            //return null!;//MessagePack.Resolver.Instance.GetFormatter<T>();
        //this.Dictionary_Type_IMessagePackFormatter.Add(typeof(T),Formatter);
        //return (IMessagePackFormatter<T>)Formatter;
        IMessagePackFormatter<T> Return(object Formatter){
            var result=(IMessagePackFormatter<T>)Formatter;
            this.Dictionary_Type_Formatter.Add(typeof(T),result);
            return result;
        }
    }
    public void Clear() {
        this.ExpressionFormatter.Clear();
        //this.Dictionary_Type_Formatter.Clear();
    }
    //public IJsonFormatter<T> GetFormatter<T>(){
    //    //if(typeof(LambdaExpression).IsSubclassOf(typeof(T))){
    //    //    return (IJsonFormatter<T>)new ExpressionFormatter();
    //    //}
    //    if(typeof(T).IsSubclassOf(typeof(LambdaExpression))){
    //        return (IJsonFormatter<T>)Activator.CreateInstance(typeof(ExpressionFormatter<>).MakeGenericType(typeof(T)),ExpressionFormatter.Instance)!;
    //    }
    //    return null!;
    //}
}
