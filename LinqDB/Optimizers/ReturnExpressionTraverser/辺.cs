using System.Diagnostics;
using System.Linq;
using Generic=System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using LinqDB.Optimizers.Comparer;
using LinqDB.Optimizers.VoidExpressionTraverser;
//using LinqDB.Sets;

namespace LinqDB.Optimizers.ReturnExpressionTraverser;
using static Common;
public class 辺(ExpressionEqualityComparer ExpressionEqualityComparer){
    //internal readonly E属性 属性;
    internal int 辺番号;
    //internal readonly Generic.HashSet<Expression> 節一度節子孫一度出現Expressions=new(ExpressionEqualityComparer);
    //internal readonly Generic.HashSet<Expression> 節一度節祖先一度出現Expressions=new(ExpressionEqualityComparer);
    internal readonly Generic.HashSet<Expression> 節子孫一度出現Expressions=new(ExpressionEqualityComparer);
    //internal readonly Generic.HashSet<Expression> 節祖先一度出現Expressions=new(ExpressionEqualityComparer);
    //internal readonly Generic.HashSet<Expression> 子孫一度出現Expressions=new(ExpressionEqualityComparer);
    //internal readonly Generic.HashSet<Expression> 祖先一度出現Expressions=new(ExpressionEqualityComparer);

    //internal readonly Generic.HashSet<Expression> 親でAssignした節一度出現Expressions=new(ExpressionEqualityComparer);
    internal readonly Generic.HashSet<Expression> 節一度出現Expressions=new(ExpressionEqualityComparer);
    internal readonly Generic.HashSet<Expression> 節二度出現Expressions=new(ExpressionEqualityComparer);
    internal readonly Generic.List<辺>List親辺=new();
    internal readonly Generic.List<辺>List子辺=new();
    internal bool 親でAssignしたか;
    internal Expression? 二度出現したExpression;
    public string 親コメント{get;set;}="";
    public string 子コメント{get;set;}="";
    public 辺(ExpressionEqualityComparer ExpressionEqualityComparer,辺 辺):this(ExpressionEqualityComparer){
        辺.List子辺.Add(this);
        this.List親辺.Add(辺);
    }
    internal bool 探索済みか;
    public static void 接続(辺 親,辺 子){
        親.List子辺.Add(子);
        子.List親辺.Add(親);
    }
    //public bool この辺に存在するか(Expression Expression)=>this.節一度出現Expressions.Contains(Expression);
    public void Clear(){
        this.二度出現したExpression=null;
        //this.節祖先_一度出現したExpressions.Clear();
        //this.子孫_一度出現したExpressions.Clear();
        this.節一度出現Expressions.Clear();
        this.節二度出現Expressions.Clear();
        //this.節一度節祖先一度出現したExpressions.Clear();
        //this.節一度節祖先一度出現Expressions.Clear();
        //this.節一度節子孫一度出現Expressions.Clear();
        //this.節祖先一度出現Expressions.Clear();
        //this.節子孫一度出現Expressions.Clear();
        //this.祖先一度出現Expressions.Clear();
        //this.子孫一度出現Expressions.Clear();
        //this.節一度節子孫一度出現Expressions.Clear();
        //this.節二度出現Expressions.Clear();
        this.List親辺.Clear();
        this.List子辺.Clear();
    }
    //public void 節祖先一度出現Expressions追加(){
    //    if(this.探索済みか) return;
    //    this.探索済みか=true;
    //    var 節祖先一度出現Expressions=this.節祖先一度出現Expressions;
    //    節祖先一度出現Expressions.UnionWith(this.節一度出現Expressions);
    //    foreach(var 親辺 in this.List親辺){
    //        親辺.節祖先一度出現Expressions追加();
    //        節祖先一度出現Expressions.IntersectWith(親辺.節一度出現Expressions);
    //    }
    //}
    /// <summary>
    /// this.節一度出現Expressionsと
    /// </summary>
    public void 節二度出現Expressions追加(){
        if(this.探索済みか) return;
        this.探索済みか=true;
        foreach(var 子辺 in this.List子辺)
            子辺.節二度出現Expressions追加();
        //var h0=new Generic.HashSet<Expression>(ExpressionEqualityComparer);
        //var h1=new Generic.HashSet<Expression>(ExpressionEqualityComparer);
        //using var Enumerator = this.List子辺.GetEnumerator();
        //if(Enumerator.MoveNext()){
        //    h0.UnionWith(Enumerator.Current.節祖先一度出現Expressions);
        //    //h.UnionWith(Enumerator.Current.節一度出現Expressions);
        //    while(Enumerator.MoveNext()) {
        //        h0.IntersectWith(Enumerator.Current.節祖先一度出現Expressions);
        //        //h.IntersectWith(Enumerator.Current.節一度出現Expressions);
        //    }
        //}
        //h0.IntersectWith(this.節一度出現Expressions);
        var 節子孫一度出現Expressions=new Generic.HashSet<Expression>(ExpressionEqualityComparer);
        using var Enumerator = this.List子辺.GetEnumerator();
        if(Enumerator.MoveNext()){
            節子孫一度出現Expressions.UnionWith(Enumerator.Current.節子孫一度出現Expressions);
            while(Enumerator.MoveNext())
                節子孫一度出現Expressions.IntersectWith(Enumerator.Current.節子孫一度出現Expressions);
        }
        節子孫一度出現Expressions.IntersectWith(this.節一度出現Expressions);
        this.節二度出現Expressions.UnionWith(節子孫一度出現Expressions);
    }

    /// <summary>
    /// 親で定義された節二度出現Expressionsはthisには必要ないので削除
    /// true?1m+1m:1m→true?(t=1m)+t:1mであるべきがtrue?(t=1m)+t:tになってしまう
    /// これはifFalse辺の一度出現ExpressionsからifTrue辺の二度出現Expressionsをした式からを引く必要がある
    /// </summary>
    public void 節二度出現Expressions除去(){
        if(this.探索済みか) return;
        this.探索済みか=true;
        var List親辺=this.List親辺;
        if(List親辺.Count>0){
            var 親節二度出現Expressions=new Generic.HashSet<Expression>(ExpressionEqualityComparer);
            if(List親辺.Count==1){
                using var Enumerator=this.List親辺.GetEnumerator();
                if(Enumerator.MoveNext()){
                    親節二度出現Expressions.UnionWith(Enumerator.Current.節二度出現Expressions);
                    Enumerator.Current.節二度出現Expressions除去();
                }
                this.節二度出現Expressions.ExceptWith(親節二度出現Expressions);
            } else{
                var 親節一度出現Expressions=new Generic.HashSet<Expression>(ExpressionEqualityComparer);
                using var Enumerator=this.List親辺.GetEnumerator();
                if(Enumerator.MoveNext()){
                    親節一度出現Expressions.UnionWith(Enumerator.Current.節一度出現Expressions);
                    親節二度出現Expressions.UnionWith(Enumerator.Current.節二度出現Expressions);
                    Enumerator.Current.節二度出現Expressions除去();
                    while(Enumerator.MoveNext()){
                        親節一度出現Expressions.SymmetricExceptWith(Enumerator.Current.節一度出現Expressions);
                        親節二度出現Expressions.IntersectWith(Enumerator.Current.節二度出現Expressions);
                        Enumerator.Current.節二度出現Expressions除去();
                    }
                }
                this.節一度出現Expressions.ExceptWith(親節一度出現Expressions);
                this.節二度出現Expressions.ExceptWith(親節二度出現Expressions);
                //一度出現Expressionsに存在しない二度出現Expressionsは存在しない
                this.節二度出現Expressions.ExceptWith(親節一度出現Expressions);
            }
        }
        //var index=0;
        //foreach(var 親辺 in List親辺){
        //    index++;
        //}
        //using var Enumerator = this.List親辺.GetEnumerator();
        //if(Enumerator.MoveNext()){
        //    親節一度出現Expressions.UnionWith(Enumerator.Current.節一度出現Expressions);
        //    親節二度出現Expressions.UnionWith(Enumerator.Current.節二度出現Expressions);
        //    //this.節一度出現Expressions.ExceptWith(Enumerator.Current.節二度出現Expressions);
        //    Enumerator.Current.節二度出現Expressions除去();
        //    while(Enumerator.MoveNext()) {
        //        親節一度出現Expressions.SymmetricExceptWith(Enumerator.Current.節一度出現Expressions);
        //        親節二度出現Expressions.IntersectWith(Enumerator.Current.節二度出現Expressions);
        //        //this.節一度出現Expressions.ExceptWith(Enumerator.Current.節二度出現Expressions);
        //        Enumerator.Current.節二度出現Expressions除去();
        //    }
        //}
        ////var 節二度出現Expressions=this.節一度出現Expressions;
        ////this.節一度出現Expressions.IntersectWith(親節二度出現Expressions);
        ////親節一度出現Expressions.ExceptWith(親節二度出現Expressions);
        //this.節一度出現Expressions.ExceptWith(親節一度出現Expressions);
        //this.節二度出現Expressions.ExceptWith(親節二度出現Expressions);
        //this.節二度出現Expressions.ExceptWith(h1);
        //foreach(var 親辺 in this.List親辺)
        //    foreach(var 節二度出現Expression in 親辺.節二度出現Expressions)
        //        節二度出現Expressions.Remove(節二度出現Expression);
    }
    /// <summary>
    /// 読み取り:節一度出現Expressions
    /// 書き込み:節祖先一度出現Expressions
    ///          祖先一度出現Expressions
    ///          節子孫一度出現Expressions
    ///          子孫一度出現Expressions
    /// </summary>
    public void 作成1() {
        if(this.探索済みか) return;
        this.探索済みか=true;
        {
            //var 節一度出現Expressions=this.節一度出現Expressions;
            //var 節祖先一度出現Expressions=this.節祖先一度出現Expressions;
            //節祖先一度出現Expressions.Clear();
            ////節祖先一度出現Expressions.UnionWith(節一度出現Expressions);
            ////祖先一度出現Expressions.UnionWith(節一度出現Expressions);
            ////var List親辺=this.List親辺;
            ////foreach(var 親辺 in List親辺){
            ////    親辺.作成1();
            ////    節祖先一度出現Expressions.IntersectWith(親辺.節祖先一度出現Expressions);
            ////}
            ////var 祖先一度出現Expressions=this.祖先一度出現Expressions;
            ////var 親二度出現Expressions=new Generic.HashSet<Expression>(ExpressionEqualityComparer);
            //using var Enumerator=this.List親辺.GetEnumerator();
            //if(Enumerator.MoveNext()){
            //    Enumerator.Current.作成1();
            //    節祖先一度出現Expressions.UnionWith(Enumerator.Current.節祖先一度出現Expressions);
            //    while(Enumerator.MoveNext()){
            //        Enumerator.Current.作成1();
            //        節祖先一度出現Expressions.IntersectWith(Enumerator.Current.節祖先一度出現Expressions);
            //    }
            //}
            //節祖先一度出現Expressions.UnionWith(節一度出現Expressions);
            //var 祖先一度出現Expressions=this.祖先一度出現Expressions;
            //祖先一度出現Expressions.Clear();
            //祖先一度出現Expressions.UnionWith(節祖先一度出現Expressions);
            //祖先一度出現Expressions.ExceptWith(節一度出現Expressions);
            //this.節二度出現Expressions.ExceptWith(祖先一度出現Expressions);
            //this.節二度出現Expressions.ExceptWith(節祖先一度出現Expressions);
        }
        //foreach(var 子辺 in this.List子辺)
        //    子辺.作成1();
        //this.節二度出現Expressions追加(this.節一度出現Expressions,this.節二度出現Expressions);
        //foreach(var 親辺 in this.List親辺)
        //    親辺.作成1();
        //{
        //    var 節一度出現Expressions=this.節一度出現Expressions;
        //    var 節子孫一度出現Expressions=this.節子孫一度出現Expressions;
        //    節子孫一度出現Expressions.UnionWith(節一度出現Expressions);
        //    foreach(var 子辺 in this.List子辺){
        //        子辺.作成1();
        //        節子孫一度出現Expressions.IntersectWith(子辺.節子孫一度出現Expressions);
        //    }
        //    var 子孫一度出現Expressions=this.子孫一度出現Expressions;
        //    子孫一度出現Expressions.UnionWith(節子孫一度出現Expressions);
        //    子孫一度出現Expressions.ExceptWith(節一度出現Expressions);
        //}
    }
    ///// <summary>
    ///// 読み取り:節一度出現Expressions
    ///// 書き込み:節一度節祖先一度出現Expressions
    /////          節一度節子孫一度出現Expressions
    ///// </summary>
    //public void 作成2() {
    //    if(this.探索済みか) return;
    //    this.探索済みか=true;
    //    var 節一度出現Expressions=this.節一度出現Expressions;
    //    var 節二度出現Expressions=this.節二度出現Expressions;
    //    //節二度出現Expressions.ExceptWith(this.節祖先一度出現Expressions);
    //    {
    //        var 節一度節祖先一度出現Expressions=this.節一度節祖先一度出現Expressions;
    //        節一度節祖先一度出現Expressions.Clear();
    //        Debug.Assert(節一度節祖先一度出現Expressions.Count==0);
    //        節一度節祖先一度出現Expressions.UnionWith(節一度出現Expressions);
    //        foreach(var 親辺 in this.List親辺){
    //            親辺.作成2();
    //            節一度節祖先一度出現Expressions.IntersectWith(親辺.節一度節祖先一度出現Expressions);
    //        }
    //    }
    //    {
    //        var 節一度節子孫一度出現Expressions=this.節一度節子孫一度出現Expressions;
    //        節一度節子孫一度出現Expressions.Clear();
    //        Debug.Assert(節一度節子孫一度出現Expressions.Count==0);
    //        節一度節子孫一度出現Expressions.UnionWith(節一度出現Expressions);
    //        foreach(var 子辺 in this.List子辺){
    //            子辺.作成2();
    //            節一度節子孫一度出現Expressions.IntersectWith(子辺.節一度節子孫一度出現Expressions);
    //        }
    //    }
    //}
    //public void 祖先_一度出現したExpressions作成() {
    //    if(this.探索済みか) return;
    //    this.探索済みか=true;
    //    var 祖先_一度出現したExpressions=this.節一度節祖先一度出現したExpressions;
    //    祖先_一度出現したExpressions.Clear();
    //    祖先_一度出現したExpressions.UnionWith(this.節一度出現Expressions);
    //    foreach(var a in this.List親辺){
    //        a.祖先_一度出現したExpressions作成();
    //        祖先_一度出現したExpressions.UnionWith(a.節一度節祖先一度出現したExpressions);
    //    }
    //}
    //public void 子孫_一度出現したExpressions作成() {
    //    if(this.探索済みか) return;
    //    this.探索済みか=true;
    //    var 子孫_一度出現したExpressions=this.節一度節子孫一度出現Expressions;
    //    子孫_一度出現したExpressions.Clear();
    //    子孫_一度出現したExpressions.UnionWith(this.節一度出現Expressions);
    //    foreach(var a in this.List子辺){
    //        a.子孫_一度出現したExpressions作成();
    //        子孫_一度出現したExpressions.UnionWith(a.節一度節子孫一度出現Expressions);
    //    }
    //}
    //public void 節一度節子孫一度出現Expressions作成0() {
    //    if(this.探索済みか) return;
    //    this.探索済みか=true;
    //    var 節一度節子孫一度出現Expressions=this.節一度節子孫一度出現Expressions;
    //    節一度節子孫一度出現Expressions.Clear();
    //    using var Enumerator=this.List子辺.GetEnumerator();
    //    if(Enumerator.MoveNext()){
    //        Enumerator.Current.節一度節子孫一度出現Expressions作成0();
    //        節一度節子孫一度出現Expressions.UnionWith(Enumerator.Current.節一度節子孫一度出現Expressions);
    //        while(Enumerator.MoveNext()){
    //            Enumerator.Current.節一度節子孫一度出現Expressions作成0();
    //            節一度節子孫一度出現Expressions.IntersectWith(Enumerator.Current.節一度節子孫一度出現Expressions);
    //        }
    //    }
    //    節一度節子孫一度出現Expressions.UnionWith(this.節一度出現Expressions);
    //}
    //public void 節祖先_一度出現したExpressions作成0() {
    //    if(this.探索済みか) return;
    //    this.探索済みか=true;
    //    var 節祖先_一度出現したExpressions=this.節祖先_一度出現したExpressions;
    //    節祖先_一度出現したExpressions.Clear();
    //    {
    //        using var Enumerator=this.List親辺.GetEnumerator();
    //        if(Enumerator.MoveNext()){
    //            Enumerator.Current.節祖先_一度出現したExpressions作成0();
    //            節祖先_一度出現したExpressions.UnionWith(Enumerator.Current.節祖先_一度出現したExpressions);
    //            while(Enumerator.MoveNext()){
    //                Enumerator.Current.節祖先_一度出現したExpressions作成0();
    //                節祖先_一度出現したExpressions.IntersectWith(Enumerator.Current.節祖先_一度出現したExpressions);
    //            }
    //        }
    //    }
    //    節祖先_一度出現したExpressions.UnionWith(this.節一度出現Expressions);
    //}
    //public void 節一度子孫一度出現Expression作成0() {
    //    if(this.探索済みか) return;
    //    this.探索済みか=true;
    //    var 節一度子孫一度出現Expression=this.節一度子孫一度出現Expression;
    //    節一度子孫一度出現Expression.Clear();
    //    if(this.List子辺.Count>0){
    //        節一度子孫一度出現Expression.UnionWith(this.節一度出現Expressions);
    //        foreach(var a in this.List子辺){
    //            a.節一度子孫一度出現Expression作成0();
    //            節一度子孫一度出現Expression.IntersectWith(a.節一度節子孫一度出現Expressions);
    //        }
    //    }
    //    節一度子孫一度出現Expression.UnionWith(this.節二度出現Expressions);
    //}
    //public void 節一度祖先一度出現Expression作成0() {
    //    if(this.探索済みか) return;
    //    this.探索済みか=true;
    //    var 節一度祖先一度出現Expression=this.節一度祖先一度出現Expression;
    //    節一度祖先一度出現Expression.Clear();
    //    if(this.List子辺.Count>0){
    //        節一度祖先一度出現Expression.UnionWith(this.節一度出現Expressions);
    //        foreach(var a in this.List親辺){
    //            a.節一度祖先一度出現Expression作成0();
    //            節一度祖先一度出現Expression.IntersectWith(a.節祖先_一度出現したExpressions);
    //        }
    //    }
    //    節一度祖先一度出現Expression.UnionWith(this.節二度出現Expressions);
    //}
    //public Generic.IEnumerable<Expression> 正規化1() {
    //    if(this.探索済みか) return this.部分木_一度出現したExpressions;
    //    this.探索済みか=true;
    //    var 部分木_一度出現したExpressions = this.部分木_一度出現したExpressions;
    //    部分木_一度出現したExpressions.Clear();
    //    Debug.Assert(部分木_一度出現したExpressions.Count==0);
    //    var List子辺=this.List子辺;
    //    using var Enumerator = List子辺.GetEnumerator();
    //    if(Enumerator.MoveNext()) {
    //        部分木_一度出現したExpressions.UnionWith(Enumerator.Current.正規化1());
    //        while(Enumerator.MoveNext())
    //            部分木_一度出現したExpressions.IntersectWith(Enumerator.Current.正規化1());
    //    }
    //    //仮想一度出現したExpressions.UnionWith(this.実体一度出現したExpressions);
    //    //var 節と節以外の部分木でそれぞれ一度づつ出現することで二度出現したとみなすExpressions=部分木_一度出現したExpressions.Intersect(this.節一度出現Expressions,this.ExpressionEqualityComparer);
    //    var 部分木_二度出現したExpressions=this.部分木_二度出現したExpressions;
    //    部分木_二度出現したExpressions.UnionWith(
    //        部分木_一度出現したExpressions.Intersect(this.節一度出現Expressions,ExpressionEqualityComparer)
    //    );
    //    //部分木_一度出現したExpressions.UnionWith(this.節一度出現Expressions);
    //    //foreach(var a in 部分木_二度出現したExpressions)
    //    //    foreach(var b in List子辺)
    //    //        b.
    //    //    a.部分木_二度出現したExpressions.Remove()
    //    foreach(var a in 部分木_二度出現したExpressions)
    //        foreach(var b in List子辺){
    //            //var r=b.部分木_二度出現したExpressions.Remove(a);
    //        }
    //    return 部分木_一度出現したExpressions;
    //}
    /// <summary>
    /// 取得_二度出現したExpression2で使う
    /// 最もルートに近い辺の式を返す
    /// </summary>
    /// <returns></returns>
    public (Expression?Expression,辺?辺に関する情報) 二度出現したExpressionと辺(){
        if(this.探索済みか) return(null,this);
        this.探索済みか=true;
        if(this.二度出現したExpression is not null)
            return(this.二度出現したExpression,this);
        foreach(var a in this.List子辺){
            var (Expression,辺に関する情報)=a.二度出現したExpressionと辺();
            if(Expression is not null)
                return(Expression,辺に関する情報);
        }
        return(null,this);
    }
    public void 親でAssignしたExpressionを設定(){
        if(this.探索済みか) return;
        this.探索済みか=true;
        this.親でAssignしたか=true;
        foreach(var a in this.List子辺)
            a.親でAssignしたExpressionを設定();
    }
    public override string ToString(){
        return$"辺番号{this.辺番号} {this.親コメント}～{this.子コメント}";
    }
}
