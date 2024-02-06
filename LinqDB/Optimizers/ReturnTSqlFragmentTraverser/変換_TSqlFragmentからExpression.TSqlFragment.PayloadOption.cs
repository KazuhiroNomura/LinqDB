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
    private e.Expression AuthenticationPayloadOption(AuthenticationPayloadOption x){throw this.単純NotSupportedException(x);}
    private e.Expression CharacterSetPayloadOption(CharacterSetPayloadOption x){throw this.単純NotSupportedException(x);}
    private e.Expression EnabledDisabledPayloadOption(EnabledDisabledPayloadOption x){throw this.単純NotSupportedException(x);}
    private e.Expression EncryptionPayloadOption(EncryptionPayloadOption x){throw this.単純NotSupportedException(x);}
    private e.Expression LiteralPayloadOption(LiteralPayloadOption x){throw this.単純NotSupportedException(x);}
    private e.Expression LoginTypePayloadOption(LoginTypePayloadOption x){throw this.単純NotSupportedException(x);}
    private e.Expression RolePayloadOption(RolePayloadOption x){throw this.単純NotSupportedException(x);}
    private e.Expression SchemaPayloadOption(SchemaPayloadOption x){throw this.単純NotSupportedException(x);}
    private e.Expression SessionTimeoutPayloadOption(SessionTimeoutPayloadOption x){throw this.単純NotSupportedException(x);}
    private e.Expression SoapMethod(SoapMethod x){throw this.単純NotSupportedException(x);}
    private e.Expression WsdlPayloadOption(WsdlPayloadOption x){throw this.単純NotSupportedException(x);}
}
