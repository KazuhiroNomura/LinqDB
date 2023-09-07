using System;
using System.Diagnostics;
using System.Linq;
using LinqDB.Serializers.MessagePack;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using C=MessagePackCustomSerializer;

#pragma warning disable CA1052 // スタティック ホルダー型は Static または NotInheritable でなければなりません
/// <summary>
/// sealedではないクラスをシリアライズする
/// </summary>
public class Abstract{
#pragma warning restore CA1052 // スタティック ホルダー型は Static または NotInheritable でなければなりません
    protected static void GetInterface(ref Type Type){
        var Interface0=Type.GetInterface(typeof(ILookup<,>).FullName);
        if(Interface0 is not null){
            Type=Interface0;
        } else{
            var Interface1=Type.GetInterface(typeof(IGrouping<,>).FullName);
            if(Interface1 is not null) Type=Interface1;
        }
        //if(
        //    (Interface=Type.GetInterface(typeof(ILookup<,>).FullName)) is not null||
        //    (Interface=Type.GetInterface(typeof(IGrouping<,>).FullName)) is not null
        //) return true;
    }
}
public class AbstractMessagePackFormatter<T>:Abstract,IMessagePackFormatter<T>{
    private static object GetFormatter(MessagePackSerializerOptions options,Type type){
        if(typeof(Type).IsAssignableFrom(type)) type=typeof(Type);
        var Formatter=options.Resolver.GetFormatterDynamic(type)!;
        var Foramtter_Type=Formatter.GetType();
        if(Foramtter_Type.IsGenericType&&Foramtter_Type.GetGenericTypeDefinition()==typeof(AbstractMessagePackFormatter<>)){
            GetInterface(ref type);
            Formatter=options.Resolver.GetFormatterDynamic(type)!;
        }
        return Formatter;
    }
    public void Serialize(ref MessagePackWriter writer,T? value,MessagePackSerializerOptions options){
        if(value is null){
            writer.WriteNil();
            return;
        }
        writer.WriteArrayHeader(2);
        var type=value.GetType();
        writer.WriteType(type);
        C.DynamicSerialize(GetFormatter(options,type),ref writer,value,options);
    }
    public T Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions options){
        if(reader.TryReadNil()) return default!;
        var ArrayHeader=reader.ReadArrayHeader();
        Debug.Assert(ArrayHeader==2);
        var type=reader.ReadType();
        var value=(T)C.DynamicDeserialize(GetFormatter(options,type),ref reader,options);
        return value;
    }
}
