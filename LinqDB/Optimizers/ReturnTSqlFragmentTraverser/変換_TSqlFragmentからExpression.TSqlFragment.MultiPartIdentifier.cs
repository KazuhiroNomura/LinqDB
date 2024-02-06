﻿using System.Linq;
using LinqDB.Sets;
using System.Diagnostics;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Reflection;
using System.Xml.Linq;
using e = System.Linq.Expressions;
using AssemblyName = Microsoft.SqlServer.TransactSql.ScriptDom.AssemblyName;
using System.Globalization;
using LinqDB.Optimizers.ReturnExpressionTraverser;
using LinqDB.Helpers;
namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
using static Common;
internal partial class 変換_TSqlFragmentからExpression{
    private (PropertyInfo Schema,MemberInfo Member)SchemaObjectName(SchemaObjectName x){
        switch(x){
            case ChildObjectName y:
                return this.ChildObjectName(y);
            case SchemaObjectNameSnippet y:
                return this.SchemaObjectNameSnippet(y);
            default:
                PropertyInfo? Schema;
                if(x.SchemaIdentifier is not null){
                    Schema=this.ContainerType.GetProperty(x.SchemaIdentifier.Value);
                    if(Schema is null) throw new NotSupportedException($"{x.SchemaIdentifier.Value}スキーマは存在しない");
                }else{
                    Schema=this.ContainerType.GetProperty("dbo");
                    if(Schema is null) throw new NotSupportedException($"dboスキーマは存在しない");
                }
                var Property=Schema.PropertyType.GetProperty(x.BaseIdentifier.Value);
                if(Property is not null) return (Schema,Property);
                var Method=Schema.PropertyType.GetMethod(x.BaseIdentifier.Value);
                if(Method is not null) return (Schema,Method);
                throw new NotSupportedException($"{Schema.Name}.{x.BaseIdentifier.Value}は存在しない");
        }
    }
    private (PropertyInfo Schema,MemberInfo Member) ChildObjectName(ChildObjectName x){
        throw this.単純NotSupportedException(x);
    }
    private (PropertyInfo Schema,MemberInfo Member) SchemaObjectNameSnippet(SchemaObjectNameSnippet x){
        throw this.単純NotSupportedException(x);
    }
}
