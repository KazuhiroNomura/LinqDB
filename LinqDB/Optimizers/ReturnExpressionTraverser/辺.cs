using System.Linq;
using Generic=System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using LinqDB.Optimizers.Comparer;
using LinqDB.Serializers.Utf8Json.Formatters.Enumerables;
namespace LinqDB.Optimizers.ReturnExpressionTraverser;
public class 辺(ExpressionEqualityComparer ExpressionEqualityComparer){
    internal int 辺番号;
    //internal readonly Generic.HashSet<Expression> _節祖先でAssignしたExpressions=new(ExpressionEqualityComparer);
    //internal Generic.HashSet<Expression> 節祖先でAssignしたExpressions{
    //    get{
    //        if(this.探索済みか) return;
    //        this.探索済みか=true;
    //        var r=new Generic.HashSet<Expression>(ExpressionEqualityComparer);
    //        using var Enumerator=this.List親辺.GetEnumerator();
    //        if(Enumerator.MoveNext()){
    //            //祖先でAssignしたExpressions.UnionWith(Enumerator.Current.祖先でAssignしたExpressions);
    //            r.UnionWith(Enumerator.Current.節祖先でAssignしたExpressions);
    //            while(Enumerator.MoveNext()){
    //                //祖先でAssignしたExpressions.IntersectWith(Enumerator.Current.祖先でAssignしたExpressions);
    //                r.IntersectWith(Enumerator.Current.節祖先でAssignしたExpressions);
    //            }
    //        }
    //        r.UnionWith(this.節二度出現Expressions);
    //        return r;
    //    }
    //}
    /// <summary>
    /// 作成_二度出現したExpression,節二度出現Expressions除去で使われる
    /// </summary>
    private readonly Generic.HashSet<Expression> 祖先二度出現Expressions=new(ExpressionEqualityComparer);
    /// <summary>
    /// 作成_二度出現したExpressionで使われる
    /// </summary>
    internal readonly Generic.HashSet<Expression> 節一度出現Expressions=new(ExpressionEqualityComparer);
    internal readonly Generic.HashSet<Expression> 節二度出現Expressions=new(ExpressionEqualityComparer);
    /// <summary>
    /// 変換_二度出現したExpressionで使われる
    /// </summary>
    internal readonly Generic.HashSet<Expression> 節祖先二度出現Expressions=new(ExpressionEqualityComparer);
    private readonly Generic.HashSet<Expression> 節子孫一度出現Expressions=new(ExpressionEqualityComparer);
    /// <summary>
    /// 取得_二度出現したExpressionで使われる
    /// </summary>
    internal readonly Generic.HashSet<Expression> 節子孫二度出現Expressions=new(ExpressionEqualityComparer);
    internal readonly Generic.List<辺>List親辺=new();
    internal readonly Generic.List<辺>List子辺=new();
    internal Expression? 二度出現したExpression;
    public string 親コメント{get;set;}="";
    public string 子コメント{get;set;}="";
    public bool デッドコードか=false;
    public 辺(ExpressionEqualityComparer ExpressionEqualityComparer,辺 辺,int 辺番号):this(ExpressionEqualityComparer){
        this.辺番号=辺番号;
        辺.List子辺.Add(this);
        this.List親辺.Add(辺);
    }
    internal bool 探索済みか;
    internal bool Assign辺ではない;
    public static void 接続(辺 親,辺 子){
        親.List子辺.Add(子);
        子.List親辺.Add(親);
    }
    //public bool この辺に存在するか(Expression Expression)=>this.節一度出現Expressions.Contains(Expression);
    public void Clear(){
        this.二度出現したExpression=null;
        this.節子孫一度出現Expressions.Clear();
        this.節子孫二度出現Expressions.Clear();
        this.節一度出現Expressions.Clear();
        this.節二度出現Expressions.Clear();
        this.List親辺.Clear();
        this.List子辺.Clear();
    }
    /// <summary>
    /// "節子孫一度出現Expressions"を子辺の"節子孫一度出現Expressions"と自身の"節一度出現Expressions"から作る
    /// "節子孫二度出現Expressions"を子辺の"節子孫二度出現Expressions"と自身の"節二度出現Expressions"から作る
    /// </summary>
    public void 節子孫二度出現Expressions作成0(){
        if(this.探索済みか) return;
        this.探索済みか=true;
        foreach(var 子辺 in this.List子辺)
            子辺.節子孫二度出現Expressions作成0();
        var 節子孫一度出現Expressions=this.節子孫一度出現Expressions;
        var 節子孫二度出現Expressions=this.節子孫二度出現Expressions;
        using var Enumerator=this.List子辺.GetEnumerator();
        if(Enumerator.MoveNext()){
            節子孫一度出現Expressions.UnionWith(Enumerator.Current.節子孫一度出現Expressions);
            節子孫二度出現Expressions.UnionWith(Enumerator.Current.節子孫二度出現Expressions);
            while(Enumerator.MoveNext()){
                節子孫一度出現Expressions.IntersectWith(Enumerator.Current.節子孫一度出現Expressions);
                節子孫二度出現Expressions.IntersectWith(Enumerator.Current.節子孫二度出現Expressions);
            }
        }
        var 節一度出現Expressions=this.節一度出現Expressions;
        節子孫二度出現Expressions.UnionWith(節子孫一度出現Expressions.Intersect(節一度出現Expressions,ExpressionEqualityComparer));
        節子孫一度出現Expressions.UnionWith(節一度出現Expressions);
        節子孫二度出現Expressions.IntersectWith(節一度出現Expressions);
        節子孫二度出現Expressions.UnionWith(this.節二度出現Expressions);
    }
    /// <summary>
    /// "祖先二度出現Expressions"を親辺の共通"節祖先二度出現Expressions"から作る。
    /// "祖先二度出現Expressions","二度出現Expression"をUnionして"節祖先二度出現Expressions"を作る
    /// </summary>
    public void 祖先二度出現Expressions作成1(){
        if(this.探索済みか) return;
        this.探索済みか=true;
        var 祖先二度出現Expressions=this.祖先二度出現Expressions;
        var 最初か=true;
        //var List子辺=this.List子辺;
        foreach(var 親辺 in this.List親辺){
            if(親辺.List親辺.Count==0) continue;
            if(最初か){
                最初か=false;
                祖先二度出現Expressions.UnionWith(親辺.節祖先二度出現Expressions);
            } else{
                祖先二度出現Expressions.IntersectWith(親辺.節祖先二度出現Expressions);
            }
        }
        var 節祖先二度出現Expressions = this.節祖先二度出現Expressions;
        節祖先二度出現Expressions.UnionWith(祖先二度出現Expressions);
        節祖先二度出現Expressions.UnionWith(this.節二度出現Expressions);
        //節祖先二度出現Expressions.UnionWith(this.節子孫二度出現Expressions);
        foreach(var 子辺 in this.List子辺)
            子辺.祖先二度出現Expressions作成1();
    }
    //public void 祖先二度出現Expressions作成(){
    //    if(this.探索済みか) return;
    //    this.探索済みか=true;
    //    var 祖先二度出現Expressions=this.祖先二度出現Expressions;
    //    var 最初か=true;
    //    foreach(var 親辺 in this.List親辺){
    //        親辺.祖先二度出現Expressions作成();
    //        if(最初か){
    //            if(親辺.節祖先二度出現Expressions.Count==0) continue;
    //            最初か=false;
    //            祖先二度出現Expressions.UnionWith(親辺.節祖先二度出現Expressions);
    //        } else{
    //            祖先二度出現Expressions.IntersectWith(親辺.節祖先二度出現Expressions);
    //        }
    //    }
    //    var 節祖先二度出現Expressions = this.節祖先二度出現Expressions;
    //    節祖先二度出現Expressions.UnionWith(祖先二度出現Expressions);
    //    節祖先二度出現Expressions.UnionWith(this.節子孫二度出現Expressions);
    //}
    /// <summary>
    /// 親辺の共通"節祖先二度出現Expressions"から"祖先二度出現Expressions"を作成
    /// 親辺"祖先二度出現Expressions".IntersectWith("祖先二度出現Expressions")
    /// </summary>
    public void 親節祖先二度出現Expressions除去2() {
        //親辺.祖先二度出現Expressions.IntersectWith(祖先二度出現Expressions)
        if(this.探索済みか) return;
        this.探索済みか=true;
        var 祖先二度出現Expressions=this.祖先二度出現Expressions;
        祖先二度出現Expressions.Clear();
        var List親辺=this.List親辺;
        using var Enumerator = List親辺.GetEnumerator();
        if(Enumerator.MoveNext()) {
            祖先二度出現Expressions.UnionWith(Enumerator.Current.節祖先二度出現Expressions);
            while(Enumerator.MoveNext()) {
                祖先二度出現Expressions.IntersectWith(Enumerator.Current.節祖先二度出現Expressions);
            }
        }
        foreach(var 親辺 in List親辺)
            親辺.祖先二度出現Expressions.IntersectWith(祖先二度出現Expressions);
        var 節祖先二度出現Expressions=this.節祖先二度出現Expressions;
        節祖先二度出現Expressions.UnionWith(祖先二度出現Expressions);
        節祖先二度出現Expressions.UnionWith(this.節二度出現Expressions);
        foreach(var 子辺 in this.List子辺)
            子辺.親節祖先二度出現Expressions除去2();
    }
    //public void 親節祖先二度出現Expressions除去() {
    //    //親辺.祖先二度出現Expressions.IntersectWith(祖先二度出現Expressions)
    //    if(this.探索済みか) return;
    //    this.探索済みか=true;
    //    var List子辺=this.List子辺;
    //    var 祖先二度出現Expressions=this.祖先二度出現Expressions;
    //    祖先二度出現Expressions.Clear();
    //    var List親辺=this.List親辺;
    //    using var Enumerator = List親辺.GetEnumerator();
    //    if(Enumerator.MoveNext()) {
    //        祖先二度出現Expressions.UnionWith(Enumerator.Current.節祖先二度出現Expressions);
    //        while(Enumerator.MoveNext()) {
    //            祖先二度出現Expressions.IntersectWith(Enumerator.Current.節祖先二度出現Expressions);
    //        }
    //    }
    //    foreach(var 親辺 in List親辺){
    //        親辺.節子孫二度出現Expressions.IntersectWith(祖先二度出現Expressions);
    //        親辺.親節祖先二度出現Expressions除去();
    //    }
    //    //this.節子孫二度出現Expressions.ExceptWith(祖先二度出現Expressions);
    //    //this.節祖先二度出現Expressions.UnionWith(this.節二度出現Expressions);
    //    //祖先二度出現Expressions.UnionWith(this.節二度出現Expressions);
    //    //this.節二度出現Expressions.ExceptWith(祖先二度出現Expressions);
    //    //foreach(var 子辺 in List子辺)
    //    //    子辺.親節祖先二度出現Expressions除去();
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
        //this.親でAssignしたか=true;
        foreach(var a in this.List子辺)
            a.親でAssignしたExpressionを設定();
    }
    public override string ToString(){
        return$"辺番号{this.辺番号} {this.親コメント}～{this.子コメント}";
    }
}
//295 20240119