using System.Linq;
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
    private e.Expression ExecuteOption(ExecuteOption x)=>x switch{
        ResultSetsExecuteOption y=>this.ResultSetsExecuteOption(y),
        _=>throw this.単純NotSupportedException(x)
    };
    private e.Expression MultiPartIdentifierCallTarget(MultiPartIdentifierCallTarget x){
        var Identifiers=x.MultiPartIdentifier.Identifiers;
        //string Schema_Object;
        ////e.MemberExpression Schema;
        //if(Identifiers.Count==1){
        //    Schema_Object=Identifiers[0].Value;
        //    //Schema=this.List_Schema.Find(p=>p.Member.Name==Key);
        //} else {
        //    Debug.Assert(Identifiers.Count==2);
        //    //データベース.スキーマ.オブジェクトだが現状データベースを検索する方法はない
        //    //var Key0=Identifiers[0].Value;
        //    //Schema_Object=Identifiers[0].Value+'.'+Identifiers[1].Value;
        //    Schema_Object=Identifiers[1].Value;
        //    //Schema=this.List_Schema.Find(p=>p.Member.DeclaringType.Name==Key0&&p.Member.Name==Key);
        //}
        //var Schema0=this.ContainerType.GetProperty(Identifiers[Identifiers.Count-2].Value,BindingFlags.Instance|BindingFlags.Public|BindingFlags.DeclaredOnly);
        var Schema_Name= Identifiers.Count switch{
            1=>Identifiers[0].Value,
            2=> Identifiers[0].Value+'.'+Identifiers[1].Value,
            _=>throw new NotSupportedException()
        };
        var Schema = this.FindSchema(Schema_Name);
        //var Schema = this.List_Schema.Find(p => string.Equals(p.Member.Name,Schema_Name,StringComparison.OrdinalIgnoreCase));
        if(Schema is not null)return Schema;
        Debug.Assert(this.RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression.ContainsKey(Schema_Name));
        return this.RefPeek.Dictionary_DatabaseSchemaTable_ColumnExpression[Schema_Name]!;
    }
    private e.Expression UserDefinedTypeCallTarget(UserDefinedTypeCallTarget x){
        throw this.単純NotSupportedException(x);
    }
}
