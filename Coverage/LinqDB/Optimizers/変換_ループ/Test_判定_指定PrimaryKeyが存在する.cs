﻿using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using static BackendClient.ExtendAggregate;

namespace CoverageCS.LinqDB.Optimizers.変換_ループ;

[TestClass]
public class Test_判定_指定PrimaryKeyが存在する : ATest
{
    [TestMethod]
    public void 実行()
    {
        //if(PrimaryKey!=null){
        this.実行結果が一致するか確認(() => KeySet変数().Select(p => new { p }).LongCount());
        //} else if(EntityParameter_Type.Name.IsAnonymous()) {
        this.実行結果が一致するか確認(a => SetN<int>(a).Select(
            p => new
            {
                f0 = p,
                f1 = p
            }).Select(p => new { p.f0 }).LongCount());
        //this._変数Cache.Execute(()=>a.Select(p=>new { p.f0,p.f1}).Count());
        //}
        this.実行結果が一致するか確認(a => SetN<int>(a).Select(p => new { p }).LongCount());
    }
    private class X
    {
        public X(int x)
        {

        }
    }
    [TestMethod]
    public void Traverse()
    {
        //case ExpressionType.Parameter:
        //    if(e==this._Parameter) {
        this.実行結果が一致するか確認(() => KeySet変数().Select(p => new { p }).LongCount());
        //case ExpressionType.New:
        //    if(e.Type.Name.IsAnonymous()) {
        this.実行結果が一致するか確認(() => KeySet変数().Select(p => new { p }).LongCount());
        //    }else{
        this.実行結果が一致するか確認(() => KeySet変数().Select(p => new X((int)p.ID1)).LongCount());
        //    }
        //    if(MemberExpression.Expression==this._EntityParameter){
        //        if(this._ParameterKey!=null&&MemberExpression.Member.MetadataToken==this._ParameterKey.MetadataToken){
        this.実行結果が一致するか確認(() => KeySet変数().Select(p => p.PrimaryKey).LongCount());
        this.実行結果が一致するか確認(() => KeySet変数().Select(p => p.ID1).LongCount());
        var AnonymousSet = new[] {
            new {a=1}
        }.ToSet();
        this.実行結果が一致するか確認(() => AnonymousSet.Select(p => p.a).LongCount());
        //case ExpressionType.MemberAccess:
        //        } else{
        this.実行結果が一致するか確認(() => KeySet変数().Select(p => p.ID1).LongCount());
        //        }
        //    }
        //default:
        this.実行結果が一致するか確認(() => KeySet変数().Select(p => p.ID1 + 1).LongCount());
    }
}