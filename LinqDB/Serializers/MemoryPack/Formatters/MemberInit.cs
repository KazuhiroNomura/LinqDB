﻿using System.Buffers;
using System.Diagnostics;
using Expressions=System.Linq.Expressions;
using MemoryPack;
using MemoryPack.Formatters;

namespace LinqDB.Serializers.MemoryPack.Formatters;


public class MemberInit:MemoryPackFormatter<Expressions.MemberInitExpression>{
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.MemberInitExpression? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal Expressions.MemberInitExpression DeserializeMemberInit(ref MemoryPackReader reader){
        Expressions.MemberInitExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    //private static readonly ReadOnlyCollectionFormatter<Expressions.MemberBinding>MemberBindings=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Expressions.MemberInitExpression? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        CustomSerializerMemoryPack.New.Serialize(ref writer,value.NewExpression);
        var Bindings=value.Bindings;
        CustomSerializerMemoryPack.MemberBindings.Serialize(ref writer,ref Bindings!);
        //this.Serialize(ref writer,value.Bindings);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.MemberInitExpression? value){
        var New=CustomSerializerMemoryPack.New.DeserializeNew(ref reader);
        var bindings=reader.ReadArray<Expressions.MemberBinding>();
        value=Expressions.Expression.MemberInit(New,bindings!);
    }
}
