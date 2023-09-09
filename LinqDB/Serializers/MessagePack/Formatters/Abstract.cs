using System;
using System.Diagnostics;
using System.Linq;
using LinqDB.Serializers.MessagePack;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
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
    private const int ArrayHeader=2;
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
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions options){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(ArrayHeader);
        var type=value!.GetType();
        writer.WriteType(type);
        MessagePack.Serializer.DynamicSerialize(GetFormatter(options,type),ref writer,value,options);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions options){
        //if(reader.TryReadNil()) return default!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        var type=reader.ReadType();
        var value=(T)MessagePack.Serializer.DynamicDeserialize(GetFormatter(options,type),ref reader,options);
        return value;
    }
}
