using Utf8Json;
using LinqDB.Helpers;
using System;
using System.Diagnostics;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using C=Serializer;
public class Type:IJsonFormatter<System.Type>{
    public static readonly Type Instance=new();
    //private void PrivateSerialize<TBufferWriter>(ref JsonWriter writer,System.Type value){
    //    if(Utf8JsonCustomSerializer.Instance.Type.di.TryGetValue(value,out var index)){
    //        writer.WriteVarInt(index);
    //    } else{
    //        var DictionaryTypeIndex=MemoryPackCustomSerializer.Instance.DictionaryTypeIndex;
    //        index=DictionaryTypeIndex.Count;
    //        writer.WriteVarInt(index);
    //        DictionaryTypeIndex.Add(value,index);
    //        if(value.IsGenericType){
    //            var GenericTypeDifinition=value.GetGenericTypeDefinition();
    //            writer.WriteString(GenericTypeDifinition.AssemblyQualifiedName);
    //            foreach(var GenericArgument in value.GetGenericArguments()) this.PrivateSerialize(ref writer,GenericArgument);
    //            if(value.IsAnonymous()) this.Register(value);
    //        } else{
    //            writer.WriteString(value.AssemblyQualifiedName);
    //        }
    //        MemoryPackCustomSerializer.Instance.Types.Add(value);
    //    }
    //}
    private readonly System.Type[]Types1=new System.Type[1];
    public void Serialize(ref Writer writer,System.Type? value,IJsonFormatterResolver Resolver){
        //if(value is null){
        //    writer.WriteNull();
        //    return;
        //}
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteType(value);
        if(value.IsAnonymous()){
            var Types1=this.Types1;
            Types1[0]=value;
            var Formatter=(IJsonFormatter)Activator.CreateInstance(typeof(Anonymous<>).MakeGenericType(Types1))!;
            C.Instance.IResolver=global::Utf8Json.Resolvers.CompositeResolver.Create(
                new IJsonFormatter[]{Formatter},
                new []{C.Instance.IResolver}
            );
        }
    }
    public System.Type Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        return reader.ReadType();
    }
}
