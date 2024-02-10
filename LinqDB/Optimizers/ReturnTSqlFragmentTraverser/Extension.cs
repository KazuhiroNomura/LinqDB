using Microsoft.SqlServer.TransactSql.ScriptDom;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDB.Optimizers.ReturnTSqlFragmentTraverser;
internal static class Extension{
    internal static string Name取得(this SchemaObjectName x){
        if(x.SchemaIdentifier is null) 
            return "dbo";
        else 
            return x.SchemaIdentifier.Value;
    }
}
