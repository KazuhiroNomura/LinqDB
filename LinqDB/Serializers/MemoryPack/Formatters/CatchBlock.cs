﻿using System;
using System.Diagnostics;
using MemoryPack;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;

using Reader = MemoryPackReader;
using T = Expressions.CatchBlock;
public class CatchBlock:MemoryPackFormatter<T> {
    public static readonly CatchBlock Instance=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        if(value.Variable is null){
            if(value.Filter is null) {

                writer.WriteVarInt(0);

                writer.WriteType(value.Test);
                
                Expression.Write(ref writer,value.Body);
            } else {
                
                writer.WriteVarInt(1);

                writer.WriteType(value.Test);

                Expression.Write(ref writer,value.Body);

                Expression.Write(ref writer,value.Filter);
            }
        } else{
            var ListParameter= writer.Serializer().Parameters;
            ListParameter.Add(value.Variable);
            if(value.Filter is null) {
                
                writer.WriteVarInt(2);
                
                writer.WriteType(value.Test);

                writer.WriteString(value.Variable.Name);
                
                Expression.Write(ref writer,value.Body);
            } else {

                writer.WriteVarInt(3);

                writer.WriteType(value.Test);

                writer.WriteString(value.Variable.Name);

                Expression.Write(ref writer,value.Body);
                
                Expression.Write(ref writer,value.Filter);
            }
            ListParameter.RemoveAt(ListParameter.Count-1);
        }

    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){


        
        var id=reader.ReadVarIntInt32();
        
        var test=reader.ReadType();

        switch(id){
            case 0:{
                var body=Expression.Read(ref reader);
                value=Expressions.Expression.Catch(test,body);
                break;
            }
            case 1:{
                var body=Expression.Read(ref reader);
                
                var filter=Expression.Read(ref reader);
                value=Expressions.Expression.Catch(test,body,filter);
                break;
            }
            case 2:{
                var name=reader.ReadString();
                var Variable=Expressions.Expression.Parameter(test,name);
                var ListParameter=reader.Serializer().Parameters;
                ListParameter.Add(Variable);
                
                var body=Expression.Read(ref reader);
                ListParameter.RemoveAt(ListParameter.Count-1);
                value=Expressions.Expression.Catch(Variable,body);
                break;
            }
            case 3:{
                var name=reader.ReadString();
                var Variable=Expressions.Expression.Parameter(test,name);
                var ListParameter=reader.Serializer().Parameters;
                ListParameter.Add(Variable);
                var body=Expression.Read(ref reader);
                var filter=Expression.Read(ref reader);
                ListParameter.RemoveAt(ListParameter.Count-1);
                value=Expressions.Expression.Catch(Variable,body,filter);
                break;
            }
            default:throw new NotSupportedException($"CatchBlock id{id}は不正");
        }
        
        
    }
}
