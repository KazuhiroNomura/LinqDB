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
    private e.Expression AuthenticationEndpointProtocolOption(AuthenticationEndpointProtocolOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression CompressionEndpointProtocolOption(CompressionEndpointProtocolOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression ListenerIPEndpointProtocolOption(ListenerIPEndpointProtocolOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression LiteralEndpointProtocolOption(LiteralEndpointProtocolOption x){
        throw this.単純NotSupportedException(x);
    }
    private e.Expression PortsEndpointProtocolOption(PortsEndpointProtocolOption x){
        throw this.単純NotSupportedException(x);
    }
}
